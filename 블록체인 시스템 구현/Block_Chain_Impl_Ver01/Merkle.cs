using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block_Chain_Impl_Ver01
{
    [Serializable]
    public class Merkle
    {
        public Merkle left_child;

        public Merkle right_child;

        public string hash;

        public int right_most_ch_idx;   // leaf merkle는 자신의 idx로 초기화

        
        public Merkle(Merkle left, Merkle right, string hash, int right_most_ch_idx)
        {
            left_child = left;
            right_child = right;
            this.hash = hash;
            this.right_most_ch_idx = right_most_ch_idx;
        }

    }
}
