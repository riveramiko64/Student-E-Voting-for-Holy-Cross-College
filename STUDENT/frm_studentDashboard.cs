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
using System.Runtime.InteropServices.ComTypes;
using student_e_voting.ADMIN;


namespace student_e_voting.STUDENT
{
    public partial class frm_studentDashboard : Form
    {
        public string studentId { get; set; }
        public string studentPass { get; set; }
        public string studentStatus { get; set; }

        

        public frm_studentDashboard(string stuid, string stupass, string status)
        {
            InitializeComponent();
            this.studentId = studentId;
            conn = new MySqlConnection(connString);
            LoadData(); ;
            studentId = stuid;
            studentPass = stupass;
            try
            {
                studentStatus = status;
                // Display the values in labels
                lbl_studentId.Text = studentId;
                lbl_studentPass.Text = studentPass;
                lbl_studentStatus.Text = studentStatus;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateStatus(string status)
        {
            studentStatus = status;
            lbl_studentStatus.Text = studentStatus; 
        }



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
                    conn.Open(); // Attempt to open the connection
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
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close(); // Always close the connection, even if an exception occurs
                }
            }
        }

        private void FetchStudentDetails()
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT stuid, name FROM tbl_student WHERE stuid = @stuid", conn);
                cmd.Parameters.AddWithValue("@stuid", studentId);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txt_studentID.Text = reader["stuid"].ToString();
                    txt_studentName.Text = reader["name"].ToString();
                }
                else
                {
                    MessageBox.Show("Student details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching student details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }



        private void frm_studentDashboard_Load(object sender, EventArgs e)
        {
            LoadData();
            FetchStudentDetails();

        }

        private void btn_voteCandidate_Click(object sender, EventArgs e)
        {
            LoadData();
            if (studentStatus == "VOTED")
            {
                MessageBox.Show("You have already voted.", "Already Voted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                frm_VoteCandidate voteCandidateForm = new frm_VoteCandidate(this, studentId, studentId);
                voteCandidateForm.Show();
                this.Hide();
            }
        }

        private void UpdateStudentPassword()
        {
            try
            {
                // Check if the password TextBox is empty
                if (string.IsNullOrEmpty(txt_studentPass.Text))
                {
                    MessageBox.Show("Please enter a new password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Show a confirmation dialog
                DialogResult result = MessageBox.Show("Are you sure you want to change your password?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Check if the user confirmed the action
                if (result == DialogResult.Yes)
                {
                    LoadData();
                    conn.Open();

                    // Execute the UPDATE command to update the password
                    cmd = new MySqlCommand("UPDATE `tbl_student` SET `stupass` = @password WHERE `stuid` = @stuid", conn);
                    cmd.Parameters.AddWithValue("@password", txt_studentPass.Text); // Replace newPassword with the new password value
                    cmd.Parameters.AddWithValue("@stuid", studentId); // Access studentId directly from frm_studentDashboard

                    i = cmd.ExecuteNonQuery();

                    if (i > 0)
                    {
                        MessageBox.Show("Password updated successfully!", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Clear the txt_studentPass TextBox
                        txt_studentPass.Text = "";
                        txt_studentPass.Enabled = false;
                        btn_register.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Failed to update password.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (result == DialogResult.No)
                {
                    // Clear the txt_studentPass TextBox
                    txt_studentPass.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            
            if (result == DialogResult.Yes)
            {
                // Navigate back to studentMain form
                frm_studentMain studentMainForm = new frm_studentMain();
                studentMainForm.Show();
                this.Close();
            }

        }

        private void txt_studentID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_studentName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            UpdateStudentPassword();
           
        }
    }
}
