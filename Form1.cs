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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using student_e_voting.ADMIN;


namespace student_e_voting
{
    public partial class Form1 : Form
    {

        public string connString = "datasource=localhost;username=root;password=;database=election";
        MySqlConnection conn;
        MySqlCommand cmd;
        int i;

        public void LoadData()
        {
            conn = new MySqlConnection(connString); // Use the class-level conn variable
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
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
        public Form1()
        {
            InitializeComponent();
            LoadData();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Username.Text) || string.IsNullOrEmpty(txt_Password.Text))
            {
                MessageBox.Show("Missing Required Field!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM `tbl_user` WHERE `username`=@username AND `password` =@password", conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@username", txt_Username.Text);
                    cmd.Parameters.AddWithValue("@password", txt_Password.Text);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        string username = dr["username"].ToString(); // Corrected error: Item() -> []
                        string password = dr["password"].ToString(); // Corrected error: Item() -> []
                        txt_Username.Clear();
                        txt_Password.Clear();
                        
                        frm_adminMain adminMainForm = new frm_adminMain();
                        adminMainForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Username or Password is Incorrect. Please try again", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void btn_exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit(); // Corrected error: end -> Application.Exit()
            }
            else
            {
                return;
            }
        }

        private void btn_exit_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
