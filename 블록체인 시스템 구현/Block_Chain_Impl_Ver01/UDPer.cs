
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Block_Chain_Impl_Ver01
{
    public class UDPer
    {
        private static string ip_string = "192.168.0.255";  // 서브넷 마스크 자리를 제외한 나머지 비트를 모두 1로 한 것이 브로드캐스트용 ip가 됨

        private UdpClient udp { get; set; }
        public Timer _timer { get; set; }
        IPEndPoint ip { get; set; }

        private int PORT_NUMBER;
        private int TYPE;    // MSG_UDPER = 0;   BKCHAIN_UDPER = 1; READYQUE_UDPER = 2; INV_UDPER = 3;

        private string target_hash { get; set; } 

        // message(string) port : 15000, 
        public UDPer(int port, int type)
        {
            PORT_NUMBER = port;
            TYPE = type;
            _timer = new System.Timers.Timer();
            _timer.Interval = 1000; // 1초 후 함수 호출
            _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            _timer.AutoReset = false;
        }

        public void Start()
        {
            if (udp != null)
            {
                throw new Exception("Alread started, stop first");
            }
            udp = new UdpClient();

            IPEndPoint localEp = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
            udp.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udp.ExclusiveAddressUse = false;

            udp.Client.Bind(localEp);
            ip = localEp;

            StartListening(TYPE);
        }

        public void Stop()
        {
            try
            {
                if (udp != null)
                {
                    udp.Close();
                    udp = null;
                }
            }

            catch { }
        }

        private void StartListening(int type)
        {
            if (type == Readonly.MSG_UDPER)
                udp.BeginReceive(new AsyncCallback(Receive), null);

            else if (type == Readonly.BKCHAIN_UDPER)
                udp.BeginReceive(new AsyncCallback(Bkchain_Receive), null);

            else if (type == Readonly.READYQUE_UDPER)
                udp.BeginReceive(new AsyncCallback(Ready_Que_Receive), null);

            else if (type == Readonly.BUFFER_UDPER)
                udp.BeginReceive(new AsyncCallback(Buffer_Receive), null);

            else if (type == Readonly.INV_UDPER)
                udp.BeginReceive(new AsyncCallback(Inv_Receive), null);

        }

        // buffer에서 채굴시간이 가장 빠른 Datagram을 선정함
        private static Datagram get_target(string _target_hash)
        {
            Debug.Assert(User.account.buffer.Count != 0);
            List<Datagram> buffer = User.account.buffer;

            // ready_queue 맨 앞 블록의 해시 
            Datagram target = buffer[0];
            string temp = target.mining_time_stamp; temp.Replace(',', ' ');
            DateTime target_datetime = Convert.ToDateTime(temp);

            for (int i = 1; i < buffer.Count; ++i)
            {
                Datagram cur = buffer[i];
                if (cur.is_valid(_target_hash))
                {
                    temp = cur.mining_time_stamp; temp.Replace(',', ' ');
                    DateTime mining_time = Convert.ToDateTime(temp);

                    if (DateTime.Compare(mining_time, target_datetime) < 0)
                    {
                        target = cur;
                        target_datetime = mining_time;
                    }
                }
            }

            User.account.add_block_chain(target);
            return target;
        }

        // 버퍼에서 target_hash를 가지는 모든 데이터그램을 삭제한다.
        private static void clear_buffer(string _target_hash)
        {
            for (int i = 0; i < User.account.buffer.Count; ++i)
            {
                Datagram cur = User.account.buffer[i];
                if (cur.is_valid(_target_hash))
                {
                    User.account.buffer.RemoveAt(i);
                }
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // version message를 위한 timer. true 일 경우에만 ack_version을 받음
            if (CommField.exchange_inv_mode)
            {
                CommField.version_timer = false;
                int best_height = -1;
                string best_user = "";

                if (MainForm.version_buffer.Count == 1 && MainForm.version_buffer.First().Key.Equals(User.account.id))
                {
                    CommField.exchange_inv_mode = false;
                    CommField.best_height = new KeyValuePair<string, int>(User.account.id, User.account.block_chain.Count);
                    string msg = "네트워크에 블록체인이 없다고 판단합니다.\n";
                    CommField.found_best_height = true;
                    CommField.buffer_received = CommField.bkchain_received = CommField.readyque_received = true;

                    CommField.there_is_no_anyinv = true;
                    if (OnReceiveMessage != null)
                        OnReceiveMessage(msg);

                    return;
                }

                // 현재까지 받은 version buffer를 검사하여 best_height 유저를 파악
                foreach (var pair in MainForm.version_buffer)
                {
                    string _sender = pair.Key;
                    int _height = pair.Value;

                    if (_height > best_height)
                    {
                        best_height = _height;
                        best_user = _sender;
                    }
                }

                CommField.best_height = new KeyValuePair<string, int>(best_user, best_height);
                CommField.found_best_height = true;

            }

            else if (CommField.verificate_inv_mode)
            {
                CommField.invtimer = false;
               
                List<string> my_inv_hashs = User.account.get_inv_hashs(CommField.earlier_height, CommField.best_height.Value);
                foreach (List<string> inv_hashs in MainForm.inv_buffer)
                {
                    if (!my_inv_hashs.SequenceEqual(inv_hashs))
                    {
                        CommField.succeed_verificate_inv = false;
                    }
                }

                CommField.succeed_verificate_inv = true;
                CommField.verificate_inv_mode = false;
            }

            // propagate bk 을 위한 timer
            else
            {
                Datagram target = get_target(target_hash);
                clear_buffer(target_hash);
                CommField.buftimer = false;
                string to_on_receive = target.miner_id + "가 #" + target.block_number + " 블록을 채굴하였습니다(" + target.mining_time_stamp + ")\n";
                if (OnReceiveMessage != null)
                    OnReceiveMessage(to_on_receive);
            }
        }

        /// <summary>
        /// 여기서부터 server부분
        /// </summary>
        public void Send(string message)
        {
            UdpClient client = new UdpClient();
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(ip_string), PORT_NUMBER);
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            client.Send(bytes, bytes.Length, ip);
           
            client.Close();
            if (OnSendMessage != null)
                OnSendMessage(message);
        }

        // 
        public void Send(List<Block> blockchain)
        {
            UdpClient client = new UdpClient();
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(ip_string), PORT_NUMBER);
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            byte[] bytes = SerializationUnit.SerializeObject(blockchain);
            //The use of synchronous transmission
            client.Send(bytes, bytes.Length, ip);

            client.Close();
            if (OnSendMessage != null)
                OnSendMessage("");
        }

        public void Send(Queue<Block> ready_queue)
        {
            UdpClient client = new UdpClient();
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(ip_string), PORT_NUMBER);
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            byte[] bytes = SerializationUnit.SerializeObject(ready_queue);
            //The use of synchronous transmission
            client.Send(bytes, bytes.Length, ip);

            client.Close();
            if (OnSendMessage != null)
                OnSendMessage("");
        }

        public void Send(List<Datagram> buffer)
        {
            UdpClient client = new UdpClient();
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(ip_string), PORT_NUMBER);
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            byte[] bytes = SerializationUnit.SerializeObject(buffer);
            //The use of synchronous transmission
            client.Send(bytes, bytes.Length, ip);

            client.Close();
            if (OnSendMessage != null)
                OnSendMessage("");
        }

        // 인벤토리 메시지용 send
        public void Send(List<string> inv_list)
        {
            UdpClient client = new UdpClient();
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(ip_string), PORT_NUMBER);
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            byte[] bytes = SerializationUnit.SerializeObject(inv_list);
            //The use of synchronous transmission
            client.Send(bytes, bytes.Length, ip);

            client.Close();
            if (OnSendMessage != null)
                OnSendMessage("");
        }

        public delegate void SendMessageHandler(string message);
        public event SendMessageHandler OnSendMessage;
      
        private void Bkchain_Receive(IAsyncResult ar)
        {
            if (!CommField.exchange_inv_mode)
                return;

            try
            {
                if (udp == null)
                    return;

                IPEndPoint ip = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
                byte[] bytes = udp.EndReceive(ar, ref ip);

                List<Block> temp = (List<Block>)SerializationUnit.DeserializeObject(bytes);
                User.account.block_chain.AddRange(temp);
                CommField.bkchain_received = true;

              
            }
            catch (SocketException se)
            {
                Trace.WriteLine(string.Format("SocketException : {0}", se.Message));

            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception : {0}", ex.Message));
            }
        }


        private void Ready_Que_Receive(IAsyncResult ar)
        {
            if (!CommField.exchange_inv_mode)
                return;
            try
            {
                if (udp == null)
                    return;

                IPEndPoint ip = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
                byte[] bytes = udp.EndReceive(ar, ref ip);

                User.account.ready_queue = (Queue<Block>)SerializationUnit.DeserializeObject(bytes);
                CommField.readyque_received = true;

            
            }
            catch (SocketException se)
            {
                Trace.WriteLine(string.Format("SocketException : {0}", se.Message));

            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception : {0}", ex.Message));
            }
        }

        private void Buffer_Receive(IAsyncResult ar)
        {
            if (!CommField.exchange_inv_mode)
                return;

            try
            {
                if (udp == null)
                    return;

                IPEndPoint ip = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
                byte[] bytes = udp.EndReceive(ar, ref ip);
                User.account.buffer = (List<Datagram>)SerializationUnit.DeserializeObject(bytes);
                CommField.buffer_received = true;
                   
            }
            catch (SocketException se)
            {
                Trace.WriteLine(string.Format("SocketException : {0}", se.Message));

            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception : {0}", ex.Message));
            }
        }

        private void Inv_Receive(IAsyncResult ar)
        {
            if (!CommField.verificate_inv_mode)
                return;

            try
            {
                if (udp == null)
                    return;

                if (!CommField.invtimer)
                {
                    CommField.invtimer = true;

                    IPEndPoint ip = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
                    byte[] bytes = udp.EndReceive(ar, ref ip);
                    List<string> inv = (List<string>)SerializationUnit.DeserializeObject(bytes);
                    MainForm.inv_buffer.Add(inv);
                    _timer.Start();
                }

                else
                {
                    IPEndPoint ip = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
                    byte[] bytes = udp.EndReceive(ar, ref ip);
                    List<string> inv = (List<string>)SerializationUnit.DeserializeObject(bytes);
                    MainForm.inv_buffer.Add(inv);
                }
           
            }
            catch (SocketException se)
            {
                Trace.WriteLine(string.Format("SocketException : {0}", se.Message));

            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception : {0}", ex.Message));
            }
        }

        private void Receive(IAsyncResult ar)
        {
            string to_on_receive = "";

            try
            {
                if (udp == null)
                    return;

                IPEndPoint ip = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
                byte[] bytes = udp.EndReceive(ar, ref ip);
                string message = Encoding.UTF8.GetString(bytes);
                StartListening(TYPE);
                string[] strlist = message.Split('\x020');
                string receiver = strlist[1];

                if (!receiver.Equals(User.account.id) && !receiver.Equals("all"))
                    return;

                // #req_version 메시지 수신
                if (strlist[0].Equals(Message.REQ_VERSION))
                {
                    string _sender = strlist[2];
                    int my_best_height = User.account.block_chain.Count;
                    CommField.udper.Send(Message.ack_version(Message.ACK_VERSION, _sender, User.account.id, my_best_height.ToString()));
                }

                // #ack_version 메시지 수신. version_timer가 true일 때만 ack_version을 받아 버퍼에 저장함
                else if (CommField.version_timer && strlist[0].Equals(Message.ACK_VERSION))
                {
                    string _sender = strlist[2];
                    int _height = Convert.ToInt32(strlist[3]);
                    MainForm.version_buffer.Add(new KeyValuePair<string, int>(_sender, _height));
                }

                // # req_inv 메시지 수신.  원래는 특정 ip로 전송해야 하나 단일 pc에서 테스트 하므로 그냥 broadcast 한다.
                else if (strlist[0].Equals(Message.REQ_INVENTORY))
                {
                    //public static string req_inventory(string comm, string recevier, string sender, string eariler_height, string best_height, string bk_hash)
                    int _eariler_height = Convert.ToInt32(strlist[3]);
                    int _best_height = Convert.ToInt32(strlist[4]);
                    string _earier_hash = strlist[5];

                    int my_best_height = User.account.block_chain.Count;
                    string my_eariler_hash = Algorithms.SHA256Hash(User.account.block_chain[_eariler_height - 1].header.get_header());

                    if (_best_height <= my_best_height && _earier_hash.Equals(my_eariler_hash))
                    {
                        List<string> inv_list = User.account.get_inv_hashs(_eariler_height, _best_height);
                        CommField.inv_udper.Send(inv_list);
                    }
                }

                // # req_bk 메시지 수신. 원래는 특정 ip로 전송해야 하나 단일 pc에서 테스트 하므로 그냥 broadcast 한다.
                else if (strlist[0].Equals(Message.REQ_BKCHAIN))
                {
                    int receiver_height = Convert.ToInt32(strlist[3]);
                    string recevier_top_bkhash = strlist[4];
                    string my_hash = Algorithms.SHA256Hash(User.account.block_chain[receiver_height - 1].header.get_header());

                    Debug.Assert(receiver_height >= 1);

                    // 해시가 일치하면
                    if (my_hash.Equals(recevier_top_bkhash))
                    {
                        List<Block> sub_bkchain = new List<Block>();
                        for (int i = receiver_height; i < User.account.block_chain.Count; i++)
                        {
                            sub_bkchain.Add(User.account.block_chain[i]);
                        }

                        CommField.bkchain_udper.Send(sub_bkchain);
                    }

                    else
                    {
                        CommField.udper.Send(Message.error_msg(Message.ERROR_MSG, strlist[2], User.account.id));
                    }
                }


                // # req_readque 메시지 수신. 원래는 특정 ip로 전송해야 하나 단일 pc에서 테스트 하므로 그냥 broadcast 한다.
                else if (strlist[0].Equals(Message.REQ_READYQUE))
                {
                    CommField.ready_queue_udper.Send(User.account.ready_queue);
                }


                // # req_buffer 메시지 수신. 원래는 특정 ip로 전송해야 하나 단일 pc에서 테스트 하므로 그냥 broadcast 한다.
                else if (strlist[0].Equals(Message.REQ_BUFFER))
                {
                    CommField.buffer_udper.Send(User.account.buffer);
                }

                // exchange_inv mode가 아닐때만 작동하는 기능
                if (CommField.exchange_inv_mode)
                    return;

                // #receive mining all
                else if (User.account.type.Equals(Readonly.TYPE_MINER) && strlist[0].Equals(Message.MINING))
                {
                    to_on_receive = Message.MINING;
                    if (OnReceiveMessage != null)
                        OnReceiveMessage(to_on_receive);
                }

                // #propage tx
                else if (strlist[0].Equals(Message.PROPAGAT_TX))
                {
                    string user_id = strlist[2];
                    string data = strlist[3];
                    string time_stamp = strlist[4];

                    User.account.add_transaction(data, user_id, time_stamp);
                    to_on_receive = user_id + "가 \"" + data + "\" 트랜잭션을을 생성하였습니다(" + time_stamp + ")\n";

                    if (OnReceiveMessage != null)
                        OnReceiveMessage(to_on_receive);
                }

                // #propagat bk
                else if (strlist[0].Equals(Message.PROPAGATE_BK))
                {
                    string miner_id = strlist[2];
                    string block_number = strlist[3];
                    string mining_time_stamp = strlist[4];
                    string header_version = strlist[5];
                    string header_prev_block_hash = strlist[6];
                    string header_merkle_root_hash = strlist[7];
                    string header_time_stamp = strlist[8];
                    string header_diff_bits = strlist[9];
                    string header_nonce = strlist[10];

                    Header from_header = new Header(header_version, header_prev_block_hash, header_merkle_root_hash, header_time_stamp, header_diff_bits);
                    from_header.nonce = header_nonce;

                    Header my_header = new Header(User.account.ready_queue.First().header);
                    Block last_block_chain = User.account.block_chain.Last();
                    my_header.prev_block_hash = Algorithms.SHA256Hash(last_block_chain.header.get_header());
                    my_header.nonce = header_nonce;

                    // validation 검사
                    string my_hash = Algorithms.SHA256Hash(my_header.get_header());
                    string from_hash = Algorithms.SHA256Hash(from_header.get_header());

                    // 유효조건 1.해시 일치 2.난이도 일치
                    if (my_hash.Equals(from_hash) && Algorithms.proof_of_difficulty(from_header, Convert.ToInt32(from_header.nonce), Readonly.diff_bits))
                    {
                        CommField.in_pow = false;
                        Datagram from_datagram;

                        // 블록체인 추가 후 최초로 블록이 전송되면 타이머 작동
                        if (!CommField.buftimer)
                        {
                            CommField.buftimer = true;
                            target_hash = my_hash;  // target_hash는 블록체인에 추가해야할 블록의 해시이다. 
                            from_datagram = new Datagram(from_header, mining_time_stamp, miner_id, block_number);
                            User.account.buffer.Add(from_datagram);

                            _timer.Start(); // 타이머는 비동기식으로 작동
                        }

                        else
                        {
                            from_datagram = new Datagram(from_header, mining_time_stamp, miner_id, block_number);
                            User.account.buffer.Add(from_datagram);
                        }
                    }

                    else if (my_hash.Equals(from_hash) && !Algorithms.proof_of_difficulty(from_header, Convert.ToInt32(from_header.nonce), Readonly.diff_bits))
                    {
                        to_on_receive = miner_id + "가 생성한 블록이 난이도를 충족하지 못합니다.\n";

                        if (OnReceiveMessage != null)
                            OnReceiveMessage(to_on_receive);
                    }

                    else
                    {
                        to_on_receive = miner_id + "가 생성한 블록의 해시가 잘못되었습니다. " +
                          "채굴한 블록의 해시 : " + from_hash +
                          "현배 블록의 해시 : " + my_hash + "\n";

                        if (OnReceiveMessage != null)
                            OnReceiveMessage(to_on_receive);

                    }

                    // propagate bk의 메시지는 timer에서 작동 됨
                } // propagate bk



            } // try
            catch (SocketException se)
            {
                Trace.WriteLine(string.Format("SocketException : {0}", se.Message));

            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception : {0}", ex.Message));
            }

        } // receive 함수

        public delegate void ReceiveMessageHandler(string message);
        public event ReceiveMessageHandler OnReceiveMessage;
    } // class
}
