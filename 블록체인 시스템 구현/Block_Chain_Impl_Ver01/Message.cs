using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block_Chain_Impl_Ver01
{


    public class Message
    {
        public static readonly string ERROR_MSG = "error_msg";
        public static readonly string MINING = "mining";
        public static readonly string PROPAGATE_BK = "propagate_bk";
        public static readonly string PROPAGAT_TX = "propagate_tx";
        public static readonly string REQ_VERSION = "req_version";
        public static readonly string ACK_VERSION = "ack_version";
        public static readonly string REQ_BKCHAIN = "req_bkchain";
        public static readonly string REQ_READYQUE = "req_readyque";
        public static readonly string REQ_BUFFER = "req_buffer";

        public static readonly string REQ_INVENTORY = "req_inventory";


        public static string error_msg(string comm, string receiver, string sender)
        {
            return comm + " " + receiver + " " + sender;
        }

        public static string command(string comm, string receiver, string sender)
        {
            return comm + " " + receiver + " " + sender;
        }

        public static string propagate_bk(string comm, string receiver, string miner, string bk_num, string mining_stamp,
            string version, string prev_hash, string merkle_root_hash, string bk_stamp, string diff_bits, string nonce)
        {
            return comm + " " + receiver + " " + miner + " " + bk_num + " " + mining_stamp + " " + version + " " + prev_hash + " " + merkle_root_hash +
                " " + bk_stamp + " " + diff_bits + " " + nonce;
        }

        public static string propagate_tx(string comm, string receiver, string user_id, string data, string tx_stamp)
        {
            return comm + " " + receiver + " " + user_id +  " " + data  + " " + tx_stamp;
        }

        // 자신의 height보다 긴 블록체인을 가진 블록에게 your_height을 보내달라고 한다
        public static string req_version(string comm, string recevier, string sender, string my_height)
        {
            return comm + " " + recevier + " " + sender + " " + my_height;
        }

        // req_version 수신 시 자신의 height을 전송한다
        public static string ack_version(string comm, string recevier, string sender, string your_height)
        {
            return comm + " " + recevier + " " + sender + " " + your_height;
        }

        // bk_chain은 전체 블록체인 중 없는 부분만 요구한다.
        public static string req_bkchain(string comm, string recevier, string sender, string my_height, string top_bkhash)
        {
            return comm + " " + recevier + " " + sender + " " + my_height + " " + top_bkhash;
        }

        // ready_que와 buffer는 전체를 요구한다.
        public static string req_readyque(string comm, string recevier, string sender)
        {
            return comm + " " + recevier + " " + sender;
        }

        public static string req_buffer(string comm, string recevier, string sender)
        {
            return comm + " " + recevier + " " + sender;
        }

        // bk_hash는 eariler_hegiht 블록의 해시
        public static string req_inventory(string comm, string recevier, string sender, string eariler_height, string best_height, string bk_hash)
        {
            return comm + " " + recevier + " " + sender + " " + eariler_height + " " + best_height + " " + bk_hash;
        }
    }
}
