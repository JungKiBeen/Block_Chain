using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block_Chain_Impl_Ver01
{
    public class CommField
    {

        /// <summary>
        /// upder 필드
        /// </summary>
        public static UDPer udper = null;
        public static UDPer bkchain_udper = null;
        public static UDPer ready_queue_udper = null;
        public static UDPer buffer_udper = null;
        public static UDPer inv_udper = null;

        /// <summary>
        /// flag
        /// </summary>
        public static bool exchange_inv_mode = true;  // true 일 경우, bk propagate 거부, mining all 거부, tx propagtate 거부
        public static bool verificate_inv_mode = false;  // true 일 경우, hash 리스트를 수신함
        public static bool in_pow = true;   // 다른 채굴자가 먼저 블록을 채굴할 경우 in_pow를 통해 채굴을 종료한다.

        public static bool buftimer = false;   // true 일 경우, 1초 동안 buf에 Datagram을 받는다
        public static bool invtimer = false;   // true 일 경우, 1초 동안 buf에 Datagram을 받는다
        public static bool version_timer = false;   // version_timer 작동. 1초동안 ack_version 메시지를 받음


        public static bool found_best_height = false;   // best_height를 찾으면 true

        public static bool bkchain_received = false;    // 아래 3필드가 다 true이면 exchange_inv_mode 를 false로 설정(종료)
        public static bool readyque_received = false;
        public static bool buffer_received = false;

        public static bool succeed_verificate_inv = false;
        public static bool there_is_no_anyinv = false;
        /// <summary>
        /// static
        /// </summary>
        public static int earlier_height = 1;
        public static KeyValuePair<string, int> best_height;
    }
}
