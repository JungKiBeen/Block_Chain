using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Block_Chain_Impl_Ver01
{
    public partial class DBConnector : Form
    {
        private MySqlConnection connection;
        private BindingSource bindingSource1 = new BindingSource();
        DataGridView dataGridView1;

        private string user_id;

        private string server;
        private string database;
        private string db_id;
        private string password;

        //Constructor
        public DBConnector(string id)
        {
            InitializeComponent();
            user_id = id;
            create_db();
            Initialize();   // set connection

            create_table();
            set_thisform();
            set_datagridview();
            this.StartPosition = FormStartPosition.Manual;
        }

        private void set_thisform()
        {
            this.Width = 820;
            this.Height = 400;
            this.AutoScroll = true;
            this.BackColor = Color.White;
            this.Text = "트랜잭션 검증 (" + user_id + ")";
            this.StartPosition = FormStartPosition.Manual;
            this.Location = MainForm.main_form.Location;
        }

        private void set_datagridview()
        {
            dataGridView1 = new DataGridView();
            this.dataGridView1.Left = 10;
            this.dataGridView1.Top = 80;
            this.dataGridView1.BackgroundColor = Color.White;
            this.dataGridView1.Width = 780;
            this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            this.dataGridView1.BorderStyle = BorderStyle.None;
            this.CenterToParent();
            this.Controls.Add(dataGridView1); // DataGridView의 컬럼 갯수를 5개로 설정합니다. 
            this.dataGridView1.AutoSize = true;
        }

        public void MySQL_ToDatagridview()
        {
            string query = "SELECT * from " + user_id + "info";

            if (this.OpenConnection() == true)
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }

                this.CloseConnection();
            }

            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 210;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[0].Width = 120;
        }


        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "BlockChain";
            db_id = "code_zero";
            password = "1759mysql!";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + db_id + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }


        //Initialize values
        private void create_db()
        {
            string cs = "server=localhost;userid=code_zero;password=1759mysql!";
            MySqlCommand cmd;

            try
            {
                connection = new MySqlConnection(cs);
                connection.Open();

                string s0 = "CREATE DATABASE IF NOT EXISTS BlockChain;";
                cmd = new MySqlCommand(s0, connection);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 여기서부터 DML
        /// </summary>
        private void delete_table_if_exist()
        {
            string query = "DROP TABLE IF EXISTS " + user_id + "info";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //테이블 생성
        private void create_table()
        {
            delete_table_if_exist();
            string query = "CREATE TABLE " + user_id + "info (tx_id Integer, data VarChar(50), from_user VarChar(50), time_stamp VarChar(50),"
                + " bk_num Integer, Primary key(tx_id))";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        public List<string>[] select(string tx_id)
        {
            string query = "SELECT * FROM " + user_id + "info WHERE tx_id = '" + tx_id + "'";

            //Create a list to store the result
            List<string>[] list = new List<string>[5];
            list[0] = new List<string>();   // tx_id
            list[1] = new List<string>();   // data
            list[2] = new List<string>();   // time_stamp
            list[3] = new List<string>();   // tx_id
            list[4] = new List<string>();
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["tx_id"] + "");
                    list[1].Add(dataReader["data"] + "");
                    list[2].Add(dataReader["from_user"] + "");
                    list[3].Add(dataReader["time_stamp"] + "");
                    list[4].Add(dataReader["bk_num"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
                return list;
        }


        //Insert statement
        public void insert(Transaction tx, int idx, int bk_num)
        {
            int tx_id = idx + ((bk_num-1) * Readonly.tx_unit);
            string query = "INSERT INTO " + user_id + "info (tx_id, data, from_user, time_stamp, bk_num) VALUES('" + tx_id.ToString() + "','" + tx.data + "','" + tx.from_user + "','" + tx.time_stamp + "','" +  bk_num.ToString()+ "')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        /*
        //Update statement
        public void update(Transaction tx, int idx, int bk_num)
        {
            int tx_id = idx + (bk_num * User.tx_unit);
            string query = "UPDATE " + user_id + "info SET data='" + tx.data + "', from_user='" + tx.from_user + "' WHERE tx_id='" + tx_id.ToString() + "'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }
        */

        /// <summary>
        /// 여기까지 DML
        /// </summary>
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void DBConnector_Load(object sender, EventArgs e)
        {

        }

        // 시현용 프로토타입이므로 폼 종료시 데이터베이스가 남아 있지 않도록 한다.
        public void DBConnector_FormClosed(object sender, FormClosedEventArgs e)
        {
            delete_table_if_exist();
        }

        // 컨트롤이 closed 되지 못하도록 막음
        private void DBConnector_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.main_form.Show();
            if (e.CloseReason == CloseReason.UserClosing) // 윈도우 종료가 아닐때만 프로그램 종료 안되게
            {
                this.Hide(); // 현재 창 숨기기
                e.Cancel = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private static bool is_int_check(string data)
        {
            int temp = 0;
            return int.TryParse(data, out temp);
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            string input = search_txtbx.Text;       // input은 tx_id이다.
            if (!is_int_check(input))
            {
                MessageBox.Show("Transaction ID 형식이 잘못되었습니다.");
                return;
            }

            List<string>[] records = select(input);
            if (records[0].Count == 0)
            {
                MessageBox.Show("검색된 트랜잭션이 존재하지 않습니다.");
                return;
            }

            List<string> tx_id_list = records[0];
            List<string> data_list = records[1];
            List<string> from_user_list = records[2];
            List<string> time_stamp_list = records[3];
            List<string> bk_num_list = records[4];

            string temp = tx_id_list[0];
            int tx_id = Convert.ToInt32(temp);
            temp = bk_num_list[0];
            int bk_num = Convert.ToInt32(temp);
            int idx = tx_id - ((bk_num-1) * Readonly.tx_unit);

            // 아직 블록이 생성되지 않은 트랜잭션일 경우 종료. 접근 가능한 bk_num은 블록체인.Count 보다 1이상 작아야 한다
            if (bk_num >= User.account.block_chain.Count)   
            {
                MessageBox.Show("아직 블록이 생성되지 않은 블록입니다.");
                return;
            }

            Block searched_bk = User.account.block_chain[bk_num];
            Transaction searched_tx = new Transaction(data_list[0], from_user_list[0], time_stamp_list[0]);
           
            // 다음 3개의 필드를 비교함
            string header_merkle_hash = searched_bk.header.merkle_root_hash;        // 기존 블록 헤더의 merkle_hash
            string bk_merkle_hash = Algorithms.Get_Merkle_Root_Hash(searched_bk.merkle_root, idx, searched_bk.tx_merkles);    // 기존의 블록 idx 위치의 트랜잭션으로 생성한 merkle_hash
            string save_merkle_hash = searched_bk.tx_merkles[idx].hash;       // 기존 idx 위치의 트랜잭션 더블해시 저장    
            searched_bk.tx_merkles[idx].hash = Algorithms.SHA256Double(searched_tx.get_transaction());

            string database_merkle_hash = Algorithms.Get_Merkle_Root_Hash(searched_bk.merkle_root, idx, searched_bk.tx_merkles);    // 데이터베이스 트랜잭션으로 생성한 merkle_hash

            searched_bk.tx_merkles[idx].hash = save_merkle_hash; // 복구

            // 유효 할 때
            if (header_merkle_hash.Equals(bk_merkle_hash) && header_merkle_hash.Equals(database_merkle_hash))
            {
                MessageBox.Show("트랜잭션이 유효합니다.\n\n 블록 머클리 해시 : " + bk_merkle_hash + "\n데이터베이스 머클리 해시 : " + database_merkle_hash);
                BlockForm bkform = new BlockForm(searched_bk, User.account.id, this);
                bkform.Show();
                bkform.highlight_datagridview(idx);
            }

            else if(header_merkle_hash.Equals(bk_merkle_hash) && !header_merkle_hash.Equals(database_merkle_hash))
            {
                MessageBox.Show("데이터베이스의 트랜잭션이 유효하지 않습니다.\n\n 블록 헤더의 머클리 해시 : " + header_merkle_hash + "\n데이터베이스 머클리 해시 : " + database_merkle_hash);
            }

            else
            {
                MessageBox.Show("블록의 트랜잭션이 유효하지 않습니다.\n\n 블록 헤더의 머클리 해시 : " + header_merkle_hash + "\n블록의 머클리 해시 : " + bk_merkle_hash);
            }
        }
    }
}

