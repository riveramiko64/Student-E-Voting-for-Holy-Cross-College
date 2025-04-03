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
    public partial class frm_adminMain : Form
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
        public frm_adminMain()
        {
            InitializeComponent();
            conn = new MySqlConnection(connString);
            LoadData();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            frm_ManageUsers manageUsersForm = new frm_ManageUsers();
            manageUsersForm.TopLevel = false;
            Panel1.Controls.Add(manageUsersForm);
            manageUsersForm.BringToFront();
            manageUsersForm.Show();
        }

        private void btn_ManageCandidates_Click(object sender, EventArgs e)
        {
            frm_ManageCandidates manageCandidatesForm = new frm_ManageCandidates();
            manageCandidatesForm.TopLevel = false;
            Panel1.Controls.Add(manageCandidatesForm);
            manageCandidatesForm.BringToFront();
            manageCandidatesForm.Show();
        }

        private void btn_ManageStudent_Click(object sender, EventArgs e)
        {
            frm_ManageStudent manageStudentForm = new frm_ManageStudent();
            manageStudentForm.TopLevel = false;
            Panel1.Controls.Add(manageStudentForm);
            manageStudentForm.BringToFront();
            manageStudentForm.Show();
        }

        private void btn_result_Click(object sender, EventArgs e)
        {
            frm_result manageResultForm = new frm_result();
            manageResultForm.TopLevel = false;
            Panel1.Controls.Add(manageResultForm);
            manageResultForm.BringToFront();
            manageResultForm.Show();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                this.Close();
                frm_studentMain studentMainForm = new frm_studentMain();
                studentMainForm.Show();
                

            }
        }

        private void frm_adminMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            lbl_time.Text = DateTime.Now.ToString("hh:mm:ss tt"); 
            lbl_date.Text = DateTime.Now.ToString("ddd, yyyy-MM-dd");
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            frm_adminDashboard manageAdminDashboard = new frm_adminDashboard();
            manageAdminDashboard.TopLevel = false;
            Panel1.Controls.Add(manageAdminDashboard);
            manageAdminDashboard.BringToFront();
            manageAdminDashboard.Show();

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbl_totalAccounts_Click(object sender, EventArgs e)
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
    }
}
