using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block_Chain_Impl_Ver01
{
    public class Readonly
    {
        public static readonly string TYPE_MINER = "MINER";
        public static readonly string TYPE_SPV = "SPV";
        public static readonly string TYPE_FULLNODES = "FULLNODES";

        public static readonly int MSG_UDPER = 0;
        public static readonly int BKCHAIN_UDPER = 1;
        public static readonly int READYQUE_UDPER = 2;
        public static readonly int BUFFER_UDPER = 3;
        public static readonly int INV_UDPER = 4;

        public static readonly int tx_unit = 3;
        public static readonly int diff_bits = 18;
        public static readonly string version = "01000000";
        public static readonly int max_nonce = 64;
    }

}
