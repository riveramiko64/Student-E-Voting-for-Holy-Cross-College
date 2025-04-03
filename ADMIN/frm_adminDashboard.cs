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
using System.IO;
using MySqlX.XDevAPI.Common;
using student_e_voting.STUDENT;

namespace student_e_voting.ADMIN
{
    public partial class frm_adminDashboard : Form
    {

        public string connString = "datasource=localhost;username=root;password=;database=election";
        MySqlConnection conn;
        MySqlCommand cmd;
        int i;
        public void LoadData()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.ConnectionString = "datasource=localhost;username=root;password=;database=election";
                    conn.Open(); // Attempt to open the connection

                    // Retrieve the count of registered accounts
                    MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) FROM tbl_student", conn);
                    int totalCount = Convert.ToInt32(countCmd.ExecuteScalar());

                    MySqlCommand countVotedCmd = new MySqlCommand("SELECT COUNT(*) FROM tbl_student WHERE status = 'VOTED'", conn);
                    int votedCount = Convert.ToInt32(countVotedCmd.ExecuteScalar());

                    MySqlCommand countUnvotedCmd = new MySqlCommand("SELECT COUNT(*) FROM tbl_student WHERE status = 'UN-VOTED'", conn);
                    int unvotedCount = Convert.ToInt32(countUnvotedCmd.ExecuteScalar());

                    // Display the total count in the label
                    lbl_totalAccounts.Text = totalCount.ToString();
                    lbl_unvotedAccounts.Text = unvotedCount.ToString();
                    lbl_votedAccounts.Text = votedCount.ToString();
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1042) // XAMPP is not running (error code 1042)
                {
                    MessageBox.Show("XAMPP is not running. Please start XAMPP and try again.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                conn.Close(); // Always close the connection, even if an exception occurs
            }
        }
        public frm_adminDashboard()
        {
            InitializeComponent();
            conn = new MySqlConnection(connString);
            LoadData();
        }

        private void frm_adminDashboard_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time_date();
        }
        public void time_date()
        {

            lbl_time.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lbl_date.Text = DateTime.Now.ToString("ddd, yyyy-MM-dd");
        }

        private void lbl_time_Click(object sender, EventArgs e)
        {
            time_date();
        }

        private void lbl_date_Click(object sender, EventArgs e)
        {
            time_date();
        }

        private void lbl_totalAccounts_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void lbl_votedAccounts_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void lbl_unvotedAccounts_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {
           
        }

        private void lbl_totalAccounts_Click_1(object sender, EventArgs e)
        {
            LoadData();
        }

        private void lbl_votedAccounts_Click_1(object sender, EventArgs e)
        {
            LoadData();
        }

        private void lbl_unvotedAccounts_Click_1(object sender, EventArgs e)
        {
            LoadData();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            time_date();
        }

        private void lbl_time_Click_1(object sender, EventArgs e)
        {
            time_date();
        }

        private void lbl_date_Click_1(object sender, EventArgs e)
        {
            time_date();
        }
    }
}
