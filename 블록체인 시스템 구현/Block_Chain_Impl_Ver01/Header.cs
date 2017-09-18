using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block_Chain_Impl_Ver01
{
    [Serializable]
    public class Header
    {
        // prev_block_hash, nonce만 블록체인에 추가할때 계산함
        public string version { get; set; }             // ver 01000000으로 초기화; 
        public string prev_block_hash { get; set; }     
        public string merkle_root_hash { get; set; }    // 블록체인에 블록을 
        public string time_stamp { get; set; }           // 블록이 생성된 시간 1970.01.01 이후, 트랜잭션의 time_stamp 이용; 
        public string diff_bits { get; set; }           
        public string nonce { get; set; }               // 블록체인에 블록을 추가할 때 마이너로부터 전달 받음; 

       
        public Header(string version, string prev_block_hash, string merkle_root_hash, string time_stamp, string diff_bits)
        {
            this.version = version;
            this.prev_block_hash = prev_block_hash;
            this.merkle_root_hash = merkle_root_hash;
            this.time_stamp = time_stamp;
            this.diff_bits = diff_bits;
        }

        public Header(Header temp)
        {
            version = temp.version;
            merkle_root_hash = temp.merkle_root_hash;
            time_stamp = temp.time_stamp;
            diff_bits = temp.diff_bits;
        }

            public string get_header()
        {
            return version + prev_block_hash + merkle_root_hash + time_stamp + diff_bits + nonce;
        }

        public string get_pow_header()
        {
            return version + prev_block_hash + merkle_root_hash + time_stamp + diff_bits;
        }
    }
}
