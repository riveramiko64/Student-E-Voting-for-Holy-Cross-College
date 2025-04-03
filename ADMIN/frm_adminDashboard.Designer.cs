namespace student_e_voting.ADMIN
{
    partial class frm_adminDashboard
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
            this.components = new System.ComponentModel.Container();
            this.Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lbl_votedAccounts = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbl_unvotedAccounts = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbl_totalAccounts = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel6 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbl_date = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbl_time = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Panel1.Controls.Add(this.lbl_votedAccounts);
            this.Panel1.Controls.Add(this.lbl_unvotedAccounts);
            this.Panel1.Controls.Add(this.lbl_totalAccounts);
            this.Panel1.Controls.Add(this.guna2HtmlLabel6);
            this.Panel1.Controls.Add(this.guna2HtmlLabel5);
            this.Panel1.Controls.Add(this.guna2HtmlLabel4);
            this.Panel1.Controls.Add(this.lbl_date);
            this.Panel1.Controls.Add(this.lbl_time);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Margin = new System.Windows.Forms.Padding(2);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(772, 404);
            this.Panel1.TabIndex = 14;
            // 
            // lbl_votedAccounts
            // 
            this.lbl_votedAccounts.BackColor = System.Drawing.Color.Transparent;
            this.lbl_votedAccounts.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_votedAccounts.Location = new System.Drawing.Point(342, 97);
            this.lbl_votedAccounts.Name = "lbl_votedAccounts";
            this.lbl_votedAccounts.Size = new System.Drawing.Size(19, 34);
            this.lbl_votedAccounts.TabIndex = 7;
            this.lbl_votedAccounts.Text = "1";
            this.lbl_votedAccounts.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_votedAccounts.Click += new System.EventHandler(this.lbl_votedAccounts_Click_1);
            // 
            // lbl_unvotedAccounts
            // 
            this.lbl_unvotedAccounts.BackColor = System.Drawing.Color.Transparent;
            this.lbl_unvotedAccounts.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_unvotedAccounts.Location = new System.Drawing.Point(581, 97);
            this.lbl_unvotedAccounts.Name = "lbl_unvotedAccounts";
            this.lbl_unvotedAccounts.Size = new System.Drawing.Size(19, 34);
            this.lbl_unvotedAccounts.TabIndex = 6;
            this.lbl_unvotedAccounts.Text = "1";
            this.lbl_unvotedAccounts.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_unvotedAccounts.Click += new System.EventHandler(this.lbl_unvotedAccounts_Click_1);
            // 
            // lbl_totalAccounts
            // 
            this.lbl_totalAccounts.BackColor = System.Drawing.Color.Transparent;
            this.lbl_totalAccounts.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_totalAccounts.Location = new System.Drawing.Point(94, 97);
            this.lbl_totalAccounts.Name = "lbl_totalAccounts";
            this.lbl_totalAccounts.Size = new System.Drawing.Size(19, 34);
            this.lbl_totalAccounts.TabIndex = 5;
            this.lbl_totalAccounts.Text = "1";
            this.lbl_totalAccounts.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_totalAccounts.Click += new System.EventHandler(this.lbl_totalAccounts_Click_1);
            // 
            // guna2HtmlLabel6
            // 
            this.guna2HtmlLabel6.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel6.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel6.Location = new System.Drawing.Point(538, 55);
            this.guna2HtmlLabel6.Name = "guna2HtmlLabel6";
            this.guna2HtmlLabel6.Size = new System.Drawing.Size(109, 26);
            this.guna2HtmlLabel6.TabIndex = 4;
            this.guna2HtmlLabel6.Text = "UNVOTED";
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(314, 55);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(77, 26);
            this.guna2HtmlLabel5.TabIndex = 3;
            this.guna2HtmlLabel5.Text = "VOTED";
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(17, 55);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(166, 26);
            this.guna2HtmlLabel4.TabIndex = 2;
            this.guna2HtmlLabel4.Text = "TOTAL VOTERS";
            this.guna2HtmlLabel4.Click += new System.EventHandler(this.guna2HtmlLabel4_Click);
            // 
            // lbl_date
            // 
            this.lbl_date.BackColor = System.Drawing.Color.Transparent;
            this.lbl_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_date.Location = new System.Drawing.Point(17, 251);
            this.lbl_date.Name = "lbl_date";
            this.lbl_date.Size = new System.Drawing.Size(3, 2);
            this.lbl_date.TabIndex = 1;
            this.lbl_date.Click += new System.EventHandler(this.lbl_date_Click_1);
            // 
            // lbl_time
            // 
            this.lbl_time.BackColor = System.Drawing.Color.Transparent;
            this.lbl_time.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_time.Location = new System.Drawing.Point(17, 211);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(3, 2);
            this.lbl_time.TabIndex = 0;
            this.lbl_time.Click += new System.EventHandler(this.lbl_time_Click_1);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // frm_adminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 404);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_adminDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_adminDashboard";
            this.Load += new System.EventHandler(this.frm_adminDashboard_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_votedAccounts;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_unvotedAccounts;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_totalAccounts;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel6;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_date;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_time;
        private System.Windows.Forms.Timer timer1;
    }
}