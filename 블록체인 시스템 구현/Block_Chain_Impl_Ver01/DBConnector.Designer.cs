namespace Block_Chain_Impl_Ver01
{
    partial class DBConnector
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
            this.label1 = new System.Windows.Forms.Label();
            this.search_txtbx = new System.Windows.Forms.TextBox();
            this.search_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(127, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Transaction ID";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // search_txtbx
            // 
            this.search_txtbx.Location = new System.Drawing.Point(226, 33);
            this.search_txtbx.Name = "search_txtbx";
            this.search_txtbx.Size = new System.Drawing.Size(342, 21);
            this.search_txtbx.TabIndex = 1;
            // 
            // search_btn
            // 
            this.search_btn.Location = new System.Drawing.Point(574, 34);
            this.search_btn.Name = "search_btn";
            this.search_btn.Size = new System.Drawing.Size(85, 23);
            this.search_btn.TabIndex = 2;
            this.search_btn.Text = "search";
            this.search_btn.UseVisualStyleBackColor = true;
            this.search_btn.Click += new System.EventHandler(this.search_btn_Click);
            // 
            // DBConnector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 494);
            this.Controls.Add(this.search_btn);
            this.Controls.Add(this.search_txtbx);
            this.Controls.Add(this.label1);
            this.Name = "DBConnector";
            this.Text = "Verificate Transactions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DBConnector_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DBConnector_FormClosed);
            this.Load += new System.EventHandler(this.DBConnector_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox search_txtbx;
        private System.Windows.Forms.Button search_btn;
    }
}