namespace Block_Chain_Impl_Ver01
{
    partial class BlockForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.num_txtbx = new System.Windows.Forms.TextBox();
            this.num_label = new System.Windows.Forms.Label();
            this.height_label = new System.Windows.Forms.Label();
            this.nonce_label = new System.Windows.Forms.Label();
            this.diffculty_label = new System.Windows.Forms.Label();
            this.merkle_txtbx = new System.Windows.Forms.TextBox();
            this.merkle_label = new System.Windows.Forms.Label();
            this.height_txtbx = new System.Windows.Forms.TextBox();
            this.hash_txtbx = new System.Windows.Forms.TextBox();
            this.hash_label = new System.Windows.Forms.Label();
            this.prev_txtbx = new System.Windows.Forms.TextBox();
            this.prev_label = new System.Windows.Forms.Label();
            this.nonce_txtbx = new System.Windows.Forms.TextBox();
            this.difficulty_txtbx = new System.Windows.Forms.TextBox();
            this.miner_txtbx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mintime_txtbx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txcount_txtbx = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(17, 235);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(667, 103);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // num_txtbx
            // 
            this.num_txtbx.Location = new System.Drawing.Point(108, 9);
            this.num_txtbx.Name = "num_txtbx";
            this.num_txtbx.ReadOnly = true;
            this.num_txtbx.Size = new System.Drawing.Size(234, 21);
            this.num_txtbx.TabIndex = 11;
            this.num_txtbx.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // num_label
            // 
            this.num_label.AutoSize = true;
            this.num_label.Location = new System.Drawing.Point(17, 15);
            this.num_label.Name = "num_label";
            this.num_label.Size = new System.Drawing.Size(57, 12);
            this.num_label.TabIndex = 10;
            this.num_label.Text = "블록 넘버";
            // 
            // height_label
            // 
            this.height_label.AutoSize = true;
            this.height_label.Location = new System.Drawing.Point(17, 42);
            this.height_label.Name = "height_label";
            this.height_label.Size = new System.Drawing.Size(40, 12);
            this.height_label.TabIndex = 12;
            this.height_label.Text = "Height";
            // 
            // nonce_label
            // 
            this.nonce_label.AutoSize = true;
            this.nonce_label.Location = new System.Drawing.Point(15, 69);
            this.nonce_label.Name = "nonce_label";
            this.nonce_label.Size = new System.Drawing.Size(42, 12);
            this.nonce_label.TabIndex = 15;
            this.nonce_label.Text = "Nonce";
            this.nonce_label.Click += new System.EventHandler(this.label4_Click);
            // 
            // diffculty_label
            // 
            this.diffculty_label.AutoSize = true;
            this.diffculty_label.Location = new System.Drawing.Point(359, 15);
            this.diffculty_label.Name = "diffculty_label";
            this.diffculty_label.Size = new System.Drawing.Size(52, 12);
            this.diffculty_label.TabIndex = 17;
            this.diffculty_label.Text = "Difficulty";
            this.diffculty_label.Click += new System.EventHandler(this.label5_Click);
            // 
            // merkle_txtbx
            // 
            this.merkle_txtbx.Location = new System.Drawing.Point(106, 124);
            this.merkle_txtbx.Name = "merkle_txtbx";
            this.merkle_txtbx.ReadOnly = true;
            this.merkle_txtbx.Size = new System.Drawing.Size(578, 21);
            this.merkle_txtbx.TabIndex = 20;
            this.merkle_txtbx.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // merkle_label
            // 
            this.merkle_label.AutoSize = true;
            this.merkle_label.Location = new System.Drawing.Point(15, 127);
            this.merkle_label.Name = "merkle_label";
            this.merkle_label.Size = new System.Drawing.Size(72, 12);
            this.merkle_label.TabIndex = 19;
            this.merkle_label.Text = "Merkle Root";
            this.merkle_label.Click += new System.EventHandler(this.label6_Click);
            // 
            // height_txtbx
            // 
            this.height_txtbx.Location = new System.Drawing.Point(108, 39);
            this.height_txtbx.Name = "height_txtbx";
            this.height_txtbx.ReadOnly = true;
            this.height_txtbx.Size = new System.Drawing.Size(234, 21);
            this.height_txtbx.TabIndex = 21;
            this.height_txtbx.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // hash_txtbx
            // 
            this.hash_txtbx.Location = new System.Drawing.Point(106, 151);
            this.hash_txtbx.Name = "hash_txtbx";
            this.hash_txtbx.ReadOnly = true;
            this.hash_txtbx.Size = new System.Drawing.Size(578, 21);
            this.hash_txtbx.TabIndex = 26;
            this.hash_txtbx.TextChanged += new System.EventHandler(this.textBox7_TextChanged);
            // 
            // hash_label
            // 
            this.hash_label.AutoSize = true;
            this.hash_label.Location = new System.Drawing.Point(15, 154);
            this.hash_label.Name = "hash_label";
            this.hash_label.Size = new System.Drawing.Size(57, 12);
            this.hash_label.TabIndex = 25;
            this.hash_label.Text = "블록 해시";
            this.hash_label.Click += new System.EventHandler(this.label7_Click);
            // 
            // prev_txtbx
            // 
            this.prev_txtbx.Location = new System.Drawing.Point(106, 178);
            this.prev_txtbx.Name = "prev_txtbx";
            this.prev_txtbx.ReadOnly = true;
            this.prev_txtbx.Size = new System.Drawing.Size(578, 21);
            this.prev_txtbx.TabIndex = 28;
            this.prev_txtbx.TextChanged += new System.EventHandler(this.textBox8_TextChanged);
            // 
            // prev_label
            // 
            this.prev_label.AutoSize = true;
            this.prev_label.Location = new System.Drawing.Point(15, 181);
            this.prev_label.Name = "prev_label";
            this.prev_label.Size = new System.Drawing.Size(85, 12);
            this.prev_label.TabIndex = 27;
            this.prev_label.Text = "이전 블록 해시";
            this.prev_label.Click += new System.EventHandler(this.label8_Click);
            // 
            // nonce_txtbx
            // 
            this.nonce_txtbx.Location = new System.Drawing.Point(108, 66);
            this.nonce_txtbx.Name = "nonce_txtbx";
            this.nonce_txtbx.ReadOnly = true;
            this.nonce_txtbx.Size = new System.Drawing.Size(234, 21);
            this.nonce_txtbx.TabIndex = 30;
            // 
            // difficulty_txtbx
            // 
            this.difficulty_txtbx.Location = new System.Drawing.Point(452, 12);
            this.difficulty_txtbx.Name = "difficulty_txtbx";
            this.difficulty_txtbx.ReadOnly = true;
            this.difficulty_txtbx.Size = new System.Drawing.Size(234, 21);
            this.difficulty_txtbx.TabIndex = 31;
            // 
            // miner_txtbx
            // 
            this.miner_txtbx.Location = new System.Drawing.Point(453, 39);
            this.miner_txtbx.Name = "miner_txtbx";
            this.miner_txtbx.ReadOnly = true;
            this.miner_txtbx.Size = new System.Drawing.Size(234, 21);
            this.miner_txtbx.TabIndex = 35;
            this.miner_txtbx.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(359, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 34;
            this.label2.Text = "채굴자";
            this.label2.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // mintime_txtbx
            // 
            this.mintime_txtbx.Location = new System.Drawing.Point(452, 66);
            this.mintime_txtbx.Name = "mintime_txtbx";
            this.mintime_txtbx.ReadOnly = true;
            this.mintime_txtbx.Size = new System.Drawing.Size(234, 21);
            this.mintime_txtbx.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(359, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 36;
            this.label1.Text = "채굴 시간";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 12);
            this.label3.TabIndex = 38;
            this.label3.Text = "*Transactions";
            // 
            // txcount_txtbx
            // 
            this.txcount_txtbx.Location = new System.Drawing.Point(108, 93);
            this.txcount_txtbx.Name = "txcount_txtbx";
            this.txcount_txtbx.ReadOnly = true;
            this.txcount_txtbx.Size = new System.Drawing.Size(234, 21);
            this.txcount_txtbx.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 12);
            this.label4.TabIndex = 39;
            this.label4.Text = "트랜잭션 수";
            // 
            // BlockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.txcount_txtbx);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mintime_txtbx);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.miner_txtbx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.difficulty_txtbx);
            this.Controls.Add(this.nonce_txtbx);
            this.Controls.Add(this.prev_txtbx);
            this.Controls.Add(this.prev_label);
            this.Controls.Add(this.hash_txtbx);
            this.Controls.Add(this.hash_label);
            this.Controls.Add(this.height_txtbx);
            this.Controls.Add(this.merkle_txtbx);
            this.Controls.Add(this.merkle_label);
            this.Controls.Add(this.diffculty_label);
            this.Controls.Add(this.nonce_label);
            this.Controls.Add(this.height_label);
            this.Controls.Add(this.num_txtbx);
            this.Controls.Add(this.num_label);
            this.Controls.Add(this.dataGridView1);
            this.Name = "BlockForm";
            this.Text = "7";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BlockForm_FormClosed);
            this.Load += new System.EventHandler(this.BlockForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox num_txtbx;
        private System.Windows.Forms.Label num_label;
        private System.Windows.Forms.Label height_label;
        private System.Windows.Forms.Label nonce_label;
        private System.Windows.Forms.Label diffculty_label;
        private System.Windows.Forms.TextBox merkle_txtbx;
        private System.Windows.Forms.Label merkle_label;
        private System.Windows.Forms.TextBox height_txtbx;
        private System.Windows.Forms.TextBox hash_txtbx;
        private System.Windows.Forms.Label hash_label;
        private System.Windows.Forms.TextBox prev_txtbx;
        private System.Windows.Forms.Label prev_label;
        private System.Windows.Forms.TextBox nonce_txtbx;
        private System.Windows.Forms.TextBox difficulty_txtbx;
        private System.Windows.Forms.TextBox miner_txtbx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mintime_txtbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txcount_txtbx;
        private System.Windows.Forms.Label label4;
    }
}