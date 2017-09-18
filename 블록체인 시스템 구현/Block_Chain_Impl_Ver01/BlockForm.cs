using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Block_Chain_Impl_Ver01
{
    public partial class BlockForm : Form
    {
        private Block block { get; set; }
        private Form parent { get; set; }
        private string id { get; set; }

        public BlockForm(Block block, string id, Form bkchain_form)
        {
            InitializeComponent();
            this.block = block;
            this.id = id;
            parent = bkchain_form;
            this.AutoScroll = true;
            this.BackColor = Color.White;
            this.Text = "#" + this.block.block_number + " 블록(" + id + ")" ;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = parent.Location;
            parent.Hide();
        }

        private void BlockForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            PopulateDataGridView();
            this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            //this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine;
            this.dataGridView1.BackgroundColor = Color.White;
            this.dataGridView1.Width = 720;
            this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            this.dataGridView1.BorderStyle = BorderStyle.None;
            //this.CenterToParent();
            this.dataGridView1.AutoSize = true;
            set_block_info();
        }

        private void set_block_info()
        {
            num_txtbx.Text = block.block_number.ToString();
            height_txtbx.Text = block.height.ToString();
            nonce_txtbx.Text = block.header.nonce;
            mintime_txtbx.Text = block.mining_time_stamp;
            difficulty_txtbx.Text = block.header.diff_bits;

            miner_txtbx.Text = block.from_miner;
            merkle_txtbx.Text = block.header.merkle_root_hash;
            hash_txtbx.Text = Algorithms.SHA256Hash(block.header.get_header());
            prev_txtbx.Text = block.header.prev_block_hash;
            txcount_txtbx.Text = block.tx_counter.ToString();
        }

        private void SetupDataGridView()
        {
            this.Controls.Add(dataGridView1); // DataGridView의 컬럼 갯수를 5개로 설정합니다. 
            dataGridView1.ColumnCount = 3; // DataGridView에 컬럼을 추가합니다. 
            dataGridView1.Columns[0].Name = "Data";
            dataGridView1.Columns[0].DisplayIndex = 0;
            dataGridView1.Columns[0].Width = 250;
            dataGridView1.Columns[1].Name = "From User";
            dataGridView1.Columns[1].DisplayIndex = 1;
            dataGridView1.Columns[1].Width = 120;

            dataGridView1.Columns[2].Name = "Time Stamp";
            dataGridView1.Columns[2].DisplayIndex = 2;
            dataGridView1.Columns[2].Width = 250;

        }

        public void highlight_datagridview(int row_idx)
        {
            dataGridView1.Rows[row_idx].DefaultCellStyle.BackColor = Color.RosyBrown;
        }


        private void PopulateDataGridView()
        {
            List<Transaction> tx_list = block.transactions;
            for (int i = 0; i < tx_list.Count; i++)
            {
                string data = tx_list[i].data;
                string from_user = tx_list[i].from_user;
                string time_stamp = tx_list[i].time_stamp;

                string[] temp = { data, from_user, time_stamp };
                dataGridView1.Rows.Add(temp);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void BlockForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.Show();
        }
    }
}
