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
    public partial class BlockChainForm : Form
    {
        private List<Block> block_chain { get; set; }
        private Button[] button_list { get; set; }
        private string id { get; set; }

        public BlockChainForm(List<Block> block_chain, string id)
        {
            InitializeComponent();
            this.AutoSize = true ;
            this.block_chain = block_chain;
            this.id = id;
            this.BackColor = Color.White;
            this.AutoScroll = true;
            this.Text = "블록 체인(" + id + ")";

            this.StartPosition = FormStartPosition.Manual;
            this.Location = MainForm.main_form.Location;
            MainForm.main_form.Hide();
            make_controls();
        }

        

        private void BlockChainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void block_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idx = Convert.ToInt32(btn.Name);
            BlockForm bform = new BlockForm(block_chain[idx], id, this);
            bform.Show();
        }

        private void make_controls()
        {
            button_list = new Button[block_chain.Count];

            int left = 10;
            int top = 40;
            for (int i = 1; i < block_chain.Count; i++)
            {
                button_list[i] = new Button();
                button_list[i].Name = i.ToString();
                button_list[i].Text = "Block #" + block_chain[i].block_number.ToString();
                button_list[i].Left = left;
                button_list[i].Top = top;
                button_list[i].Width = 80;
                button_list[i].Height = 80;

                this.Controls.Add(button_list[i]);
                button_list[i].Click += block_click;
                left += 100; 
            }
        }

        private void BlockChainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.main_form.Show();
        }
    }
}
