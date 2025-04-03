using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace student_e_voting.ADMIN
{
    public partial class frm_ManageUsers : Form
    {
        public string connString = "datasource=localhost;username=root;password=;database=election";
        MySqlConnection conn;
        MySqlCommand cmd;
        int i;

        public void LoadData()
        {
            try
            {
                conn = new MySqlConnection(connString); // Use the class-level conn variable
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

        public frm_ManageUsers()
        {
            InitializeComponent();
            LoadData();
        }

        private void pbx_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if all fields are filled
                if (string.IsNullOrWhiteSpace(txt_name.Text) ||
                    string.IsNullOrWhiteSpace(txt_username.Text) ||
                    string.IsNullOrWhiteSpace(txt_Password.Text) ||
                    cbo_role.SelectedIndex == -1)
                {
                    throw new Exception("Please fill in all the credentials to register a new user.");
                }

                LoadData();
                conn.Open();
                cmd = new MySqlCommand("INSERT INTO `tbl_user`(`name`, `username`, `password`, `role`) VALUES (@name,@username,@password,@role)", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@name", txt_name.Text);
                cmd.Parameters.AddWithValue("@username", txt_username.Text);
                cmd.Parameters.AddWithValue("@password", txt_Password.Text);
                cmd.Parameters.AddWithValue("@role", cbo_role.Text);
                i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    MessageBox.Show("New User Register Success !", "VOTE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("New User Register Failed !", "VOTE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                txt_name.Clear();
                txt_Password.Clear();
                txt_username.Clear();
                cbo_role.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn != null) // Check if conn is not null before closing it
                {
                    conn.Close();
                }
            }
        }

        private void frm_ManageUsers_Load(object sender, EventArgs e)
        {

        }
    }
}
