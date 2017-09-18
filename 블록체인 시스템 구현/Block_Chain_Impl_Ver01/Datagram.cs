using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block_Chain_Impl_Ver01
{
    [Serializable]
    public class Datagram
    {
        public Header header { get; set; }
        public string mining_time_stamp { get; set; }
        public string miner_id { get; set; }
        public string block_number { get; set; }

        public Datagram(Header header, string mining_time_stamp, string miner_id, string block_number)
        {
            this.header = header;
            this.mining_time_stamp = mining_time_stamp;
            this.miner_id = miner_id;
            this.block_number = block_number;
        }

        // ready_queue 맨 앞 블록의 해시 값과 일치하는지 확인
        public bool is_valid(string hash)
        {
            string my_hash = Algorithms.SHA256Hash(header.get_header());
            return my_hash.Equals(hash);
        }
    }
}
