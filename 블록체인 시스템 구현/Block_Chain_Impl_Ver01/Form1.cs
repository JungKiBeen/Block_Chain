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
    public enum UserType
    {
        SPV, FULL_NODES, MINER
    }

    public partial class Form1 : Form
    {
        public UserType user_type { get; set; }

        public Form1()
        {
            InitializeComponent();
            user_type = UserType.SPV;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e){}

        private void spv_radio_btn_Click(object sender, EventArgs e)
        {
            user_type = UserType.SPV;
        }

        private void fullnodes_radio_btn_Click(object sender, EventArgs e)
        {
            user_type = UserType.FULL_NODES;
        }

        private void miner_radio_btn_Click(object sender, EventArgs e)
        {
            user_type = UserType.MINER;
        }

        private void account_enter_btn_Click(object sender, EventArgs e)
        {
            string id = id_txtbx.Text.ToString();

            switch (user_type)
            {
                case UserType.SPV:
                    User.account = new SPV(id, Readonly.TYPE_SPV);
                    break;

                case UserType.FULL_NODES:
                    User.account = new FullNodes(id, Readonly.TYPE_FULLNODES);

                    break;

                case UserType.MINER:
                    User.account = new Miner(id, Readonly.TYPE_MINER);
                    break;

                default:
                    MessageBox.Show("유저 타입을 다시 선택해 주세요");
                    break;
            }
            

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void miner_radio_btn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void fullnodes_radio_btn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void account_group_bx_Enter(object sender, EventArgs e)
        {

        }
    }
}
