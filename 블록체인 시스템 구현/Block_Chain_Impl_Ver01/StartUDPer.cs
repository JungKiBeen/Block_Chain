
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Block_Chain_Impl_Ver01
{
    class StartUDPer
    {
        private const int PORT_NUMBER = 15000;
        private UdpClient udp = null;
        IAsyncResult ar_ = null;

        /// <summary>
        /// 여기서부터 server부분
        /// </summary>
        public void Send(string message)
        {
            UdpClient client = new UdpClient();
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.2.255"), PORT_NUMBER);
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            client.Send(bytes, bytes.Length, ip);
            client.Close();
            if (OnSendMessage != null)
                OnSendMessage(message);
        }
        public delegate void SendMessageHandler(string message);
        public event SendMessageHandler OnSendMessage;

        /// <summary>
        /// 여기서부터 client 부분
        /// </summary>
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
            StartListening();
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

        private void StartListening()
        {
            ar_ = udp.BeginReceive(Receive, new object());
        }

        private void Receive(IAsyncResult ar)
        {
            try
            {
                if (udp != null)
                {
                    IPEndPoint ip = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
                    byte[] bytes = udp.EndReceive(ar, ref ip);

                    string message = Encoding.UTF8.GetString(bytes);
                    StartListening();
                    if (OnReceiveMessage != null)

                        OnReceiveMessage(message);

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
        public delegate void ReceiveMessageHandler(string message);
        public event ReceiveMessageHandler OnReceiveMessage;


    }
}
