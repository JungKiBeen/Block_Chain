namespace Block_Chain_Impl_Ver01
{
    partial class MainForm
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
            this.input_txtbx = new System.Windows.Forms.TextBox();
            this.input_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.search_btn = new System.Windows.Forms.Button();
            this.search_txtbx = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.show_btn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.db_btn = new System.Windows.Forms.Button();
            this.mining_all_btn = new System.Windows.Forms.Button();
            this.mining_btn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // input_txtbx
            // 
            this.input_txtbx.Location = new System.Drawing.Point(6, 31);
            this.input_txtbx.Name = "input_txtbx";
            this.input_txtbx.Size = new System.Drawing.Size(186, 21);
            this.input_txtbx.TabIndex = 0;
            this.input_txtbx.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // input_btn
            // 
            this.input_btn.Location = new System.Drawing.Point(198, 31);
            this.input_btn.Name = "input_btn";
            this.input_btn.Size = new System.Drawing.Size(61, 21);
            this.input_btn.TabIndex = 1;
            this.input_btn.Text = "Enter";
            this.input_btn.UseVisualStyleBackColor = true;
            this.input_btn.Click += new System.EventHandler(this.input_btn_click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.input_btn);
            this.groupBox1.Controls.Add(this.input_txtbx);
            this.groupBox1.Location = new System.Drawing.Point(16, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 65);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "트랜잭션 입력";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // search_btn
            // 
            this.search_btn.Location = new System.Drawing.Point(198, 31);
            this.search_btn.Name = "search_btn";
            this.search_btn.Size = new System.Drawing.Size(63, 21);
            this.search_btn.TabIndex = 1;
            this.search_btn.Text = "Enter";
            this.search_btn.UseVisualStyleBackColor = true;
            this.search_btn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // search_txtbx
            // 
            this.search_txtbx.Location = new System.Drawing.Point(6, 31);
            this.search_txtbx.Name = "search_txtbx";
            this.search_txtbx.Size = new System.Drawing.Size(186, 21);
            this.search_txtbx.TabIndex = 0;
            this.search_txtbx.TextChanged += new System.EventHandler(this.search_txtbx_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.search_btn);
            this.groupBox2.Controls.Add(this.search_txtbx);
            this.groupBox2.Location = new System.Drawing.Point(16, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(271, 65);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "트랜잭션 검색";
            // 
            // show_btn
            // 
            this.show_btn.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.show_btn.Location = new System.Drawing.Point(6, 9);
            this.show_btn.Name = "show_btn";
            this.show_btn.Size = new System.Drawing.Size(255, 36);
            this.show_btn.TabIndex = 2;
            this.show_btn.Text = "블록체인 보기";
            this.show_btn.UseVisualStyleBackColor = true;
            this.show_btn.Click += new System.EventHandler(this.show_btn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.db_btn);
            this.panel1.Controls.Add(this.mining_all_btn);
            this.panel1.Controls.Add(this.mining_btn);
            this.panel1.Controls.Add(this.show_btn);
            this.panel1.Location = new System.Drawing.Point(16, 163);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(271, 194);
            this.panel1.TabIndex = 4;
            // 
            // db_btn
            // 
            this.db_btn.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.db_btn.Location = new System.Drawing.Point(6, 57);
            this.db_btn.Name = "db_btn";
            this.db_btn.Size = new System.Drawing.Size(255, 33);
            this.db_btn.TabIndex = 5;
            this.db_btn.Text = "트랜잭션 검증";
            this.db_btn.UseVisualStyleBackColor = true;
            this.db_btn.Click += new System.EventHandler(this.db_btn_Click);
            // 
            // mining_all_btn
            // 
            this.mining_all_btn.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mining_all_btn.Location = new System.Drawing.Point(6, 103);
            this.mining_all_btn.Name = "mining_all_btn";
            this.mining_all_btn.Size = new System.Drawing.Size(255, 33);
            this.mining_all_btn.TabIndex = 4;
            this.mining_all_btn.Text = "Mining All";
            this.mining_all_btn.UseVisualStyleBackColor = true;
            this.mining_all_btn.Click += new System.EventHandler(this.mining_all_btn_Click);
            // 
            // mining_btn
            // 
            this.mining_btn.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mining_btn.Location = new System.Drawing.Point(6, 152);
            this.mining_btn.Name = "mining_btn";
            this.mining_btn.Size = new System.Drawing.Size(255, 33);
            this.mining_btn.TabIndex = 3;
            this.mining_btn.Text = "Mining";
            this.mining_btn.UseVisualStyleBackColor = true;
            this.mining_btn.Click += new System.EventHandler(this.mining_btn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.richTextBox1.Location = new System.Drawing.Point(293, 21);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(337, 336);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(293, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "message";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 368);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Main Box";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox input_txtbx;
        private System.Windows.Forms.Button input_btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button search_btn;
        private System.Windows.Forms.TextBox search_txtbx;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button show_btn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button mining_all_btn;
        private System.Windows.Forms.Button mining_btn;
        private System.Windows.Forms.Button db_btn;
    }
}