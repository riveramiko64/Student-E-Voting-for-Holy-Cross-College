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
    public partial class frm_studentMain : Form
    {
        
        public string studId { get; set; }
        public string StudentId
        {
            get { return txt_studId.Text; }
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
                    conn.ConnectionString = "datasource=localhost;username=root;password=;database=election";
                    conn.Open(); // Attempt to open the connection
                    // Your logic for loading data goes here
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
        public frm_studentMain()
        {
            InitializeComponent();
            conn = new MySqlConnection(connString); // Initialize the conn variable
            LoadData();
           
        }

        private void guna2CirclePictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void btn_StudLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_studPass.Text) || string.IsNullOrEmpty(txt_studPass.Text))
            {
                MessageBox.Show("Missing Required Field!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                try
                {
                    conn.Open();

                    MySqlCommand adminCmd = new MySqlCommand("SELECT * FROM `tbl_user` WHERE `username`=@username AND `password` =@password", conn);
                    adminCmd.Parameters.AddWithValue("@username", txt_studId.Text);
                    adminCmd.Parameters.AddWithValue("@password", txt_studPass.Text);

                    MySqlDataReader adminReader = adminCmd.ExecuteReader();
                    if (adminReader.Read())
                    {
                        string username = adminReader["username"].ToString();
                        string password = adminReader["password"].ToString();
                        txt_studId.Clear();
                        txt_studPass.Clear();

                        frm_adminMain adminMainForm = new frm_adminMain();
                        adminMainForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        adminReader.Close();

                        MySqlCommand studentCmd = new MySqlCommand("SELECT stuid, stupass, status FROM tbl_student WHERE stuid=@stuid AND stupass=@stupass", conn);
                        studentCmd.Parameters.AddWithValue("@stuid", txt_studId.Text);
                        studentCmd.Parameters.AddWithValue("@stupass", txt_studPass.Text);

                        MySqlDataReader studentReader = studentCmd.ExecuteReader();
                        if (studentReader.Read())
                        {
                            string studentId = studentReader["stuid"].ToString();
                            string studentPassword = studentReader["stupass"].ToString();
                            string status = studentReader["status"].ToString(); // Fetch status from database
                            studentReader.Close();

                            frm_studentDashboard studentDashboardForm = new frm_studentDashboard(studentId, txt_studPass.Text, status); // Pass status to the form
                            studentDashboardForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Username or Password is Incorrect. Please try again", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }


        }

        private void frm_studentMain_Load(object sender, EventArgs e)
        { 
            LoadData();
        }

        private void txt_studId_TextChanged(object sender, EventArgs e)
        {

        }
        public void ClearStudentId()
        {
            txt_studId.Clear();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void txt_studPass_TextChanged(object sender, EventArgs e)
        {
         

        }
    }
}
