using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Block_Chain_Impl_Ver01
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// static 필드
        /// </summary>
        public static List<KeyValuePair<string, int> > version_buffer { get; set; }  // version msg를 위한 buffer
        public static List<List<string> > inv_buffer { get; set; }  // Exchange_Inventory를 위한 buffer
        public static List<string> best_height_list { get; set; }  // Exchange_Inventory를 위한 buffer

        public static MainForm main_form { get; set; }


     
        public MainForm()
        {
            InitializeComponent();
            CommField.exchange_inv_mode = true;
            richTextBox1.Text = "";
            version_buffer = new List<KeyValuePair<string, int> >();
            inv_buffer = new List<List<string> >();
            best_height_list = new List<string>();
            main_form = this;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Form1 account_form = new Form1();
            if (account_form.ShowDialog() == DialogResult.OK) // 대화상자 확인버튼 클릭 시
                this.Text = User.account.id + "(" + User.account.type + ")";

            if (!User.account.type.Equals(Readonly.TYPE_MINER))
            {
                this.mining_all_btn.Enabled = false;
                this.mining_btn.Enabled = false;
            }

            this.StartPosition = FormStartPosition.Manual;      // 시작 위치를 AccountBox로 설정
            this.Location = account_form.Location;

            CommField.udper = new UDPer(15000, 0);
            CommField.udper.OnSendMessage += new UDPer.SendMessageHandler(udper_OnSendMessage);
            CommField.udper.OnReceiveMessage += new UDPer.ReceiveMessageHandler(udper_OnReceiveMessage);
            CommField.udper.Start();

            CommField.bkchain_udper = new UDPer(14000, 1);
            CommField.bkchain_udper.OnSendMessage += new UDPer.SendMessageHandler(udper_OnSendMessage);
            CommField.bkchain_udper.OnReceiveMessage += new UDPer.ReceiveMessageHandler(udper_OnReceiveMessage);
            CommField.bkchain_udper.Start();

            CommField.ready_queue_udper = new UDPer(13000, 2);
            CommField.ready_queue_udper.OnSendMessage += new UDPer.SendMessageHandler(udper_OnSendMessage);
            CommField.ready_queue_udper.OnReceiveMessage += new UDPer.ReceiveMessageHandler(udper_OnReceiveMessage);
            CommField.ready_queue_udper.Start();

            CommField.buffer_udper = new UDPer(12000, 3);
            CommField.buffer_udper.OnSendMessage += new UDPer.SendMessageHandler(udper_OnSendMessage);
            CommField.buffer_udper.OnReceiveMessage += new UDPer.ReceiveMessageHandler(udper_OnReceiveMessage);
            CommField.buffer_udper.Start();

            CommField.inv_udper = new UDPer(11000, 4);
            CommField.inv_udper.OnSendMessage += new UDPer.SendMessageHandler(udper_OnSendMessage);
            CommField.inv_udper.OnReceiveMessage += new UDPer.ReceiveMessageHandler(udper_OnReceiveMessage);
            CommField.inv_udper.Start();

           
            // 인벤토리 교환시 트랜잭션전파, 블로전파, MiningAll, 인벤토리전파를 수신하면 안됨. exchange_inv_mode = true;
            Exchange_Invetory();
            if (CommField.there_is_no_anyinv) return;

            if (Verificate_Inventory())
                set_richtxtbx("인벤토리가 유효합니다\n");

            else
            {
                set_richtxtbx("인벤토리가 잘못되었습니다\n");
                this.Close();
            }
        }

        private bool is_first_text = true;
        private int statr_selection;
        private int end_selection;
        private void set_richtxtbx(string msg)
        {

            if (!is_first_text)
            {
                richTextBox1.Select(statr_selection, end_selection);
                richTextBox1.SelectionColor = Color.Black;
            }

            statr_selection = richTextBox1.Text.Length;

            richTextBox1.Text += msg;
            end_selection = richTextBox1.Text.Length - 1;
            
            richTextBox1.SelectionStart = richTextBox1.Text.Length-1;//맨 마지막 선택...
            richTextBox1.ScrollToCaret();

            richTextBox1.Select(statr_selection, end_selection);
            richTextBox1.SelectionColor = Color.Red;
            is_first_text = false;
        }

        private void Exchange_Invetory()
        {
            CommField.earlier_height = User.account.block_chain.Count;

            CommField.udper.Send(Message.req_version(Message.REQ_VERSION, "all", User.account.id, CommField.earlier_height.ToString()));
            CommField.version_timer = true;
            CommField.udper._timer.Start();

            while (!CommField.found_best_height) ; // best_height을 찾을 때까지 기다린다.
            if (!CommField.exchange_inv_mode)
                return;

            int _height = User.account.block_chain.Count;  
            Header topbk_header = User.account.block_chain.Last().header;  // topbk_header
            string _receiver = CommField.best_height.Key;
            set_richtxtbx(_receiver + "로부터 블록을 전송받습니다(Best Heigt :" + CommField.best_height.Value.ToString() +")\n");
            richTextBox1.SelectionStart = richTextBox1.Text.Length;//맨 마지막 선택...
            richTextBox1.ScrollToCaret();
            CommField.udper.Send(Message.req_bkchain(Message.REQ_BKCHAIN, _receiver, User.account.id, _height.ToString(), Algorithms.SHA256Hash(topbk_header.get_header())));
            CommField.udper.Send(Message.req_readyque(Message.REQ_READYQUE, _receiver, User.account.id));
            CommField.udper.Send(Message.req_buffer(Message.REQ_BUFFER, _receiver, User.account.id));

            while (!CommField.bkchain_received || !CommField.readyque_received || !CommField.buffer_received) ;       // 블록체인 다운로드가 끝나면 exchange_inv_mode는 false로 설정된다. 그 때 반복문을 나간다.
            CommField.exchange_inv_mode = false;
            set_richtxtbx("블록체인 전송완료 FROM : " + CommField.best_height.Key + "\n");
        }


        private bool Verificate_Inventory()
        {
            if (!User.account.Verificate_Subchain(CommField.earlier_height))
            {
                CommField.succeed_verificate_inv = false;
                return CommField.succeed_verificate_inv;
            }

            string eariler_hash = Algorithms.SHA256Hash(User.account.block_chain[CommField.earlier_height - 1].header.get_header());

            CommField.verificate_inv_mode = true;
            CommField.udper.Send(Message.req_inventory(Message.REQ_INVENTORY, "all", User.account.id, CommField.earlier_height.ToString(),
                CommField.best_height.Value.ToString(), eariler_hash));

            while (CommField.verificate_inv_mode) ;

            return CommField.succeed_verificate_inv;
        }

        protected override void OnClosed(EventArgs e)
        {
            if (CommField.udper != null)
                CommField.udper.Stop();

            if (CommField.bkchain_udper != null)
                CommField.bkchain_udper.Stop();

            if (CommField.ready_queue_udper != null)
                CommField.ready_queue_udper.Stop();


            if (CommField.buffer_udper != null)
                CommField.buffer_udper.Stop();

            if (CommField.inv_udper != null)
                CommField.inv_udper.Stop();
            base.OnClosed(e);
        }

        /// <summary>
        /// 
        /// </summary>

        void udper_OnSendMessage(string message)
        {
          
            //Trace.WriteLine(string.Format("sent messge : {0}", message));
        }

        public void udper_OnReceiveMessage(string message)
        {  // mining all 시에만 mining 함수 호출

            Invoke((MethodInvoker)delegate
            {
                if (message == Message.MINING)
                {
                    mining();
                }

                else
                {
                    set_richtxtbx(message + "\n");
                }
            });
            //Trace.WriteLine(string.Format("received message : {0}", message));
        }

        /// <summary>
        /// 여기서 부터 주 클래스
        /// </summary>

        /* List<Block> block_chain 블록체인에서 자기가 생성한 트랜잭션만을 검색할 수 있음
           다른사람이 생성한 트랜잭션은 참조 불가 */
        private void search_btn_click(object sender, EventArgs e)
        {
            List<Block> block_chain = User.account.block_chain;
            string search_data = search_txtbx.Text.ToString();
            string user_id = User.account.id;

            for (int i = 0; i < block_chain.Count; i++)
            {
                Block cur_block = block_chain[i];
                List<Transaction> transactions = cur_block.transactions;

                for (int j = 0; j < transactions.Count; j++)
                {
                    if (transactions[j].Equals(search_data) && transactions[j].from_user.Equals(user_id))
                    {
                        MessageBox.Show(
                            "트랜잭션 : " + search_data + "\n" +
                            "블록 넘버 : " + cur_block.block_number.ToString() + "\n" +
                            "트랜잭션 해시 : " + Algorithms.SHA256Hash(transactions[j].get_transaction()) +
                            "생성 유저 : " + user_id + "\n" +
                            "트랜잭션 생성 시간 : " + transactions[j].time_stamp + "\n"
                            );
                    }
                }
            } // for

            MessageBox.Show("해당 트랜잭션을 찾을 수 없습니다.");
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void search_txtbx_TextChanged(object sender, EventArgs e)
        {

        }

        public void mining()
        {
            // ready_queue 가장 앞의 요소의 헤더를 pow 한다.
            if (User.account.ready_queue.Count < 2)
            {
                MessageBox.Show("채굴할 블록이 없습니다");
                return;
            }

            Block new_block = User.account.ready_queue.First();
            string block_number = new_block.block_number.ToString();
            Header header = new Header(new_block.header);
            Block last_block_chain = User.account.block_chain.Last();
            string prev_hash = Algorithms.SHA256Hash(last_block_chain.header.get_header());
            header.prev_block_hash = prev_hash;

            // POW : nonce를 제외한 header 필드를 합친 string 전달
            BigInteger temp = Algorithms.proof_of_work(header.get_pow_header(), Readonly.diff_bits, Readonly.max_nonce);

            if (temp.Equals(BigInteger.MinusOne))
            {
                richTextBox1.Text += "채굴 도중에 다른 채굴자가 먼저 블록을 채굴하였습니다.\n";
                return;
            }
           // richTextBox1.Text += "\nNonce : " + temp.ToString() + "\n\n";

            string nonce = temp.ToString();
            header.nonce = nonce;
            string mining_time_stamp = DateTime.Now.ToString().Replace(' ', ',');

            propagte_new_block(User.account.id, block_number, mining_time_stamp, header);
        }


        private void mining_btn_Click(object sender, EventArgs e)
        {
            mining();
        }

        private void show_btn_Click(object sender, EventArgs e)
        {
            BlockChainForm bcform = new BlockChainForm(User.account.block_chain, User.account.id);
            bcform.Show();
        }

       
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void db_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            User.account.my_database.Location = this.Location;
            User.account.my_database.MySQL_ToDatagridview();
            User.account.my_database.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            User.account.my_database.DBConnector_FormClosed(sender, e);
        }

        private void mining_all_btn_Click(object sender, EventArgs e)
        {
            string msg = Message.command(Message.MINING, "all", User.account.id);
            CommField.udper.Send(msg); 
        }

        private void propagte_new_block(string miner_id, string block_number, string mining_time_stamp, Header header)
        {
            string msg = Message.propagate_bk(Message.PROPAGATE_BK, "all", miner_id, block_number, mining_time_stamp, header.version, header.prev_block_hash,
                header.merkle_root_hash, header.time_stamp, header.diff_bits, header.nonce);
            CommField.udper.Send(msg);     
        }

        private void input_btn_click(object sender, EventArgs e)
        {
            string data = input_txtbx.Text;
            string time_stamp = DateTime.Now.ToString().Replace(' ', ',');

            string msg = Message.propagate_tx(Message.PROPAGAT_TX, "all", User.account.id, data, time_stamp);
            CommField.udper.Send(msg); //
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
