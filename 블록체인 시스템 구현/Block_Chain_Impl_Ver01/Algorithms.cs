using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Globalization;
using System.Numerics;
using System.Diagnostics;

namespace Block_Chain_Impl_Ver01
{
    public class Algorithms
    {
        // Exchange Invetory 후 블록 검증. my_height ~ best_height 의 bk_hash를 수집후 
        public static bool Verificate_Bkchain(List<List<string> > inv_list, int my_height)
        {
            List<Block> bk_chain = User.account.block_chain;
            for (int i = 0; i < inv_list[0].Count; ++i)
            {
                for (int j = 0; j < inv_list.Count; ++j)
                {
                    Header cur_header = bk_chain[++my_height].header;
                    bool is_right = inv_list[j][i].Equals(Algorithms.SHA256Double(cur_header.get_header()));
                    if (!is_right) return false;
                }
            }

            return true;
        }

        public static string SHA256Double(string data)
        {
            return SHA256Hash(SHA256Hash(data));
        }

        public static string SHA256Hash(string data)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));


            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        // 해시를 byte[]로 반환
        public static byte[] SHA256ByteArr(string data)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));

            return hash;
        }

        public static BigInteger proof_of_work(string header, int diff_bits, int exponent)
        {
            CommField.in_pow = true;

            BigInteger max_nonce = BigInteger.Pow(2, exponent);     // 2^32
            BigInteger nonce;
            for (nonce = 0; nonce <= max_nonce; ++nonce)
            {
                string text = header + nonce.ToString();
                byte[] hash_res = SHA256ByteArr(text);

                bool is_hashed = true;
                string binary = null;
                int checker = 7;
                int idx = hash_res.Length - 1; // abcdefgh string에서 a는 배열 가장 끝에 저장된다. 

                for (int i = 0; i < diff_bits; i++)
                {
                    if (i % 8 == 0)
                    {
                        binary = Convert.ToString(hash_res[idx--], 2).PadLeft(8, '0');
                        checker = 7;
                    }

                    if (binary[checker--] != '0')
                    {
                        is_hashed = false;
                        break;
                    }
                }

                if (!CommField.in_pow)
                    return BigInteger.MinusOne;

                if(is_hashed)
                    return nonce;
            }

            return BigInteger.MinusOne;
        }


        public static bool proof_of_difficulty(Header header, int nonce, int diff_bits)
        {
            byte[] hash_res = SHA256ByteArr(header.get_pow_header() + nonce.ToString());

            string binary = null;
            int checker = 7;
            int idx = hash_res.Length - 1; // abcdefgh string에서 a는 배열 가장 끝에 저장된다. 

            for (int i = 0; i < diff_bits; i++)
            {
                if (i % 8 == 0)
                {
                    binary = Convert.ToString(hash_res[idx--], 2).PadLeft(8, '0');
                    checker = 7;
                }

                if (binary[checker--] != '0')
                {
                    return false;
                }
            }

            return true;
        }


        /*
        // merkle는 블록 트랜잭션의 더블해쉬 값을 가진 리스트이다.
        public static string build_merkle(List<string> merkle)
        {
            if (merkle.Count == 0)
                return default(string);

            else if (merkle.Count == 1)
                return merkle[0];

            while (merkle.Count > 1)
            {
                if (merkle.Count % 2 != 0)
                    merkle.Add(merkle[merkle.Count - 1]);

                Debug.Assert(merkle.Count % 2 == 0);
                List<string> new_merkle = new List<string>();

                for (int i = 0; i < merkle.Count; i += 2)
                {
                    string a = merkle[i];
                    string b = merkle[i + 1];
                    string new_node = a + b;
                    new_merkle.Add(SHA256Double(new_node));
                }
                merkle = new_merkle;
            }
            return merkle[0];
        }
        */

        // merkle는 블록 트랜잭션의 더블해쉬 값을 가진 리스트이다.
        public static Merkle build_merkle(List<Merkle> merkle)
        {
            int not_meaning = merkle.Count;
            if (merkle.Count == 0)
                return default(Merkle);

            else if (merkle.Count == 1)
                return merkle[0];

            while (merkle.Count > 1)
            {
                if (merkle.Count % 2 != 0)
                {
                    Merkle copy = new Merkle(null, null, merkle[merkle.Count - 1].hash, not_meaning);
                    merkle.Add(copy);
                }
                  

                Debug.Assert(merkle.Count % 2 == 0);
                List<Merkle> new_merkle = new List<Merkle>();

                for (int i = 0; i < merkle.Count; i += 2)
                {
                    string hash = SHA256Double(string.Concat(merkle[i].hash,merkle[i + 1].hash)); 
                    Merkle temp = new Merkle(merkle[i], merkle[i + 1], hash, merkle[i + 1].right_most_ch_idx);      // 오른쪽 자식노드의 right_most_ch_idx 필드를 복사
                    new_merkle.Add(temp);
                }
                merkle = new_merkle;
            }
            return merkle[0];
        }

        public static string Get_Merkle_Root_Hash(Merkle root, int tx_idx, List<Merkle> tx_merkle_list)
        {
            // if (root.hash.Equals(tx_merkle_list[tx_idx].hash) || (root.left_child == null && root.right_child == null))
            if ((root.left_child == null && root.right_child == null))
                return root.hash;

            if (root.right_most_ch_idx <= tx_idx)
            {
                string temp = string.Concat(root.left_child.hash, Get_Merkle_Root_Hash(root.right_child, tx_idx, tx_merkle_list));
                return SHA256Double(temp);
            }

            else
            {
                string temp = string.Concat(Get_Merkle_Root_Hash(root.left_child, tx_idx, tx_merkle_list),root.right_child.hash);
                return SHA256Double(temp);
            }
        }
    }

  
}
