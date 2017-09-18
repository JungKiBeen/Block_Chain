namespace Block_Chain_Impl_Ver01
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.spv_radio_btn = new System.Windows.Forms.RadioButton();
            this.fullnodes_radio_btn = new System.Windows.Forms.RadioButton();
            this.miner_radio_btn = new System.Windows.Forms.RadioButton();
            this.account_group_bx = new System.Windows.Forms.GroupBox();
            this.account_enter_btn = new System.Windows.Forms.Button();
            this.id_txtbx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.account_group_bx.SuspendLayout();
            this.SuspendLayout();
            // 
            // spv_radio_btn
            // 
            this.spv_radio_btn.AutoSize = true;
            this.spv_radio_btn.Location = new System.Drawing.Point(17, 29);
            this.spv_radio_btn.Name = "spv_radio_btn";
            this.spv_radio_btn.Size = new System.Drawing.Size(47, 16);
            this.spv_radio_btn.TabIndex = 0;
            this.spv_radio_btn.TabStop = true;
            this.spv_radio_btn.Text = "SPV";
            this.spv_radio_btn.UseVisualStyleBackColor = true;
            this.spv_radio_btn.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            this.spv_radio_btn.Click += new System.EventHandler(this.spv_radio_btn_Click);
            // 
            // fullnodes_radio_btn
            // 
            this.fullnodes_radio_btn.AutoSize = true;
            this.fullnodes_radio_btn.Location = new System.Drawing.Point(17, 63);
            this.fullnodes_radio_btn.Name = "fullnodes_radio_btn";
            this.fullnodes_radio_btn.Size = new System.Drawing.Size(80, 16);
            this.fullnodes_radio_btn.TabIndex = 1;
            this.fullnodes_radio_btn.TabStop = true;
            this.fullnodes_radio_btn.Text = "FullNodes";
            this.fullnodes_radio_btn.UseVisualStyleBackColor = true;
            this.fullnodes_radio_btn.CheckedChanged += new System.EventHandler(this.fullnodes_radio_btn_CheckedChanged);
            this.fullnodes_radio_btn.Click += new System.EventHandler(this.fullnodes_radio_btn_Click);
            // 
            // miner_radio_btn
            // 
            this.miner_radio_btn.AutoSize = true;
            this.miner_radio_btn.Location = new System.Drawing.Point(17, 97);
            this.miner_radio_btn.Name = "miner_radio_btn";
            this.miner_radio_btn.Size = new System.Drawing.Size(55, 16);
            this.miner_radio_btn.TabIndex = 2;
            this.miner_radio_btn.TabStop = true;
            this.miner_radio_btn.Text = "Miner";
            this.miner_radio_btn.UseVisualStyleBackColor = true;
            this.miner_radio_btn.CheckedChanged += new System.EventHandler(this.miner_radio_btn_CheckedChanged);
            this.miner_radio_btn.Click += new System.EventHandler(this.miner_radio_btn_Click);
            // 
            // account_group_bx
            // 
            this.account_group_bx.Controls.Add(this.miner_radio_btn);
            this.account_group_bx.Controls.Add(this.fullnodes_radio_btn);
            this.account_group_bx.Controls.Add(this.spv_radio_btn);
            this.account_group_bx.Location = new System.Drawing.Point(43, 80);
            this.account_group_bx.Name = "account_group_bx";
            this.account_group_bx.Size = new System.Drawing.Size(213, 133);
            this.account_group_bx.TabIndex = 3;
            this.account_group_bx.TabStop = false;
            this.account_group_bx.Text = "UserType";
            this.account_group_bx.Enter += new System.EventHandler(this.account_group_bx_Enter);
            // 
            // account_enter_btn
            // 
            this.account_enter_btn.Location = new System.Drawing.Point(43, 237);
            this.account_enter_btn.Name = "account_enter_btn";
            this.account_enter_btn.Size = new System.Drawing.Size(213, 27);
            this.account_enter_btn.TabIndex = 4;
            this.account_enter_btn.Text = "Enter";
            this.account_enter_btn.UseVisualStyleBackColor = true;
            this.account_enter_btn.Click += new System.EventHandler(this.account_enter_btn_Click);
            // 
            // id_txtbx
            // 
            this.id_txtbx.Location = new System.Drawing.Point(43, 44);
            this.id_txtbx.Name = "id_txtbx";
            this.id_txtbx.Size = new System.Drawing.Size(213, 21);
            this.id_txtbx.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "User ID";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 286);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.id_txtbx);
            this.Controls.Add(this.account_enter_btn);
            this.Controls.Add(this.account_group_bx);
            this.Name = "Form1";
            this.Text = "Account Box";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.account_group_bx.ResumeLayout(false);
            this.account_group_bx.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton spv_radio_btn;
        private System.Windows.Forms.RadioButton fullnodes_radio_btn;
        private System.Windows.Forms.RadioButton miner_radio_btn;
        private System.Windows.Forms.GroupBox account_group_bx;
        private System.Windows.Forms.Button account_enter_btn;
        private System.Windows.Forms.TextBox id_txtbx;
        private System.Windows.Forms.Label label1;
    }
}

