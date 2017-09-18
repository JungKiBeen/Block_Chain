using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block_Chain_Impl_Ver01
{
    public class User
    {
        /* notice: 비트코인이 아닌 블록체인에 초점을 맞춘 구현이므로, balacne 개념은 아예 고려하지 않음. 
         * 실제 비트코인에서는 balance가 아닌 UTXO(unspent transaction output)개념을 통해 사용자의 wallet을 관리함 */
        public static User account { get; set; }
        public DBConnector my_database { get; set; }

        public string type { get; set; }
        public string id { get; set; }

        public List<Block> block_chain { get; set; }    // 실제 비트코인의 블록체인은 분기를 가지나, 여기서는 그렇지 않도록 구현함. SPV 유저의 경우, 블록 구조체를 가지되 데이터 파트-List<Transaction> 은 저장하지 않음.
        public Queue<Block> ready_queue { get; set; }   // Validation을 기다리는 List<Block>,  // SPV도 이 블록 리스트에 대해서는 데이터 파트 - List<Transaction>을 가진다. 즉 SPV노드도 유효성 검증에 참여함
        public List<Datagram> buffer { get; set; }      // 블록 합의를 위한 buffer
      

        public User()
        {
            block_chain = new List<Block>();
            ready_queue = new Queue<Block>();
            buffer = new List<Datagram>();
            block_chain.Add(Block.create_genesis(Readonly.tx_unit));
            my_database = new DBConnector(id);
           
        }

        public User(string id, string type)
        {
            this.id = id;
            this.type = type;
            block_chain = new List<Block>();
            ready_queue = new Queue<Block>();
            buffer = new List<Datagram>();
            block_chain.Add(Block.create_genesis(Readonly.tx_unit));
            my_database = new DBConnector(id);
        }

        public void add_transaction(string data, string id, string time_stamp)
        {
            if (ready_queue.Count == 0 || ready_queue.Last().tx_counter == Readonly.tx_unit)   // 큐가 비어있거나 마지막 블록이 꽉 찬 경우
            {
                // temporary_blocks이 비지 않았으면 merkle root 계산 
                string prev_merkle_hash = null;
                if (ready_queue.Count != 0)
                {
                    Block prev_block = ready_queue.Last();
                  
                    Merkle merkle_root = Algorithms.build_merkle(prev_block.tx_merkles);
                    prev_block.merkle_root = merkle_root;
                    prev_merkle_hash = merkle_root.hash;
                    Debug.Assert(prev_merkle_hash != null);

                    prev_block.header.merkle_root_hash = prev_merkle_hash;
                }
               
                Header header = new Header(Readonly.version, "", "",time_stamp, Readonly.diff_bits.ToString());
                Block new_block = new Block(header, block_chain.Count + ready_queue.Count, block_chain.Count + ready_queue.Count + 1) ;  // 블록 생성 후 ready_queue 끝에추가
                ready_queue.Enqueue(new_block);
            }

            Transaction tx = new Transaction(data, id, time_stamp);
            Block last_block = ready_queue.Last();
            last_block.transactions.Add(tx);         // ready_queue 끝 블록의 transactions의 끝에 tx 추가
            string tx_hash = Algorithms.SHA256Double(tx.get_transaction());
            Merkle new_merkle = new Merkle(null, null, tx_hash, last_block.tx_merkles.Count);

            last_block.tx_merkles.Add(new_merkle);

            // database 삽입; 현재는 모든 tx를 삽입함. 자신의 tx만 삽입하고 싶다면 user_id가 같을 때마나 삽입하면 됨
            int bk_num = last_block.block_number;     // gensis block이 블록넘버 #0이다. 
            int idx = last_block.transactions.Count - 1;
            my_database.insert(tx, idx, bk_num);

            last_block.tx_counter++;                 // counter 증가
        }

        public void add_block_chain(Datagram datagram)
        {
            // ready_queue 맨 앞 요소 삭제 후 datagram의 헤더와 그 외 필드를 추가
            Block new_block = ready_queue.Dequeue(); 
            new_block.header = datagram.header;
            new_block.from_miner = datagram.miner_id;
            new_block.mining_time_stamp = datagram.mining_time_stamp;

            block_chain.Last().next_block = new_block; // 블록체인에 next_block 필드를 통해 새로운 블록 연결
            block_chain.Add(new_block);
        }

        public List<string> get_inv_hashs(int from_height, int end_hegiht)
        {
            List<string> hashs = new List<string>();

            for (int i = from_height; i < end_hegiht; ++i)
            {
                string hash = Algorithms.SHA256Hash(block_chain[i].header.get_header());
                hashs.Add(hash);
            }

            return hashs;
        }

        public bool Verificate_Subchain(int from_idx)
        {
            Debug.Assert(from_idx > 0);

            for (int i = from_idx; i < block_chain.Count; ++i)
            {
                string cur_hash = Algorithms.SHA256Hash(block_chain[i].header.get_header());
                string _prev_hash = Algorithms.SHA256Hash(block_chain[i-1].header.get_header());
                string save_prev_hash = block_chain[i].header.prev_block_hash;

                block_chain[i].header.prev_block_hash = _prev_hash; //
                string temp_hash = Algorithms.SHA256Hash(block_chain[i].header.get_header());

                if (!temp_hash.Equals(cur_hash))
                {
                    return false;
                }

                block_chain[i].header.prev_block_hash = save_prev_hash; //
            }

            return true;
        }
    }
}
