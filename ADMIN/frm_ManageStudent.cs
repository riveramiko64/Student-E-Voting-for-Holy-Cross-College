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


namespace student_e_voting.ADMIN
{
    public partial class frm_ManageStudent : Form
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
        public frm_ManageStudent()
        {
            InitializeComponent();
            conn = new MySqlConnection(connString); // Initialize the conn variable
            LoadData();
            AutoStudentID();
            LoadStudentData();
        }

        private void frm_ManageStudent_Load(object sender, EventArgs e)
        {
            LoadData();
            AutoStudentID();
            LoadStudentData();
        }

        private void LoadStudentData()
        {
            dataGridView1.Rows.Clear();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbl_student", conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, dr["stuid"], dr["name"], dr["course"], dr["year"], dr["status"], dr["stupass"]);
                }
                dr.Close(); // Close the data reader after use
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbl_student WHERE stuid LIKE '%" + txt_search.Text + "%' OR name LIKE '%" + txt_search.Text + "%' OR course LIKE '%" + txt_search.Text + "%' OR year LIKE '%" + txt_search.Text + "%' OR status LIKE '%" + txt_search.Text + "%'", conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, dr["stuid"], dr["name"], dr["course"], dr["year"], dr["status"], dr["stupass"]);
                }
                dr.Close(); // Close the data reader after use
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

        private void clear()
        {
            //txt_studentID.Clear();
            txt_studentName.Clear();
            txt_studentPass.Clear();
            cbo_course.SelectedIndex = -1;
            cbo_year.SelectedIndex = -1;
        }

        private void AutoStudentID()
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbl_student ORDER BY id DESC", conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr.HasRows)
                    {
                        // Assuming your id column in the database is named "id"
                        txt_studentID.Text = (Convert.ToInt32(dr["stuid"]) + 1).ToString();
                    }
                    else
                    {
                        txt_studentID.Text = DateTime.Now.ToString("yyyy") + "001";
                    }
                }
                dr.Close();
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


        private void btn_register_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO tbl_student (stuid, stupass, name, course, year, status) VALUES (@stuid, @stupass, @name, @course, @year, @status)", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@stuid", txt_studentID.Text);
                cmd.Parameters.AddWithValue("@stupass", txt_studentPass.Text);
                cmd.Parameters.AddWithValue("@name", txt_studentName.Text);
                cmd.Parameters.AddWithValue("@course", cbo_course.Text);
                cmd.Parameters.AddWithValue("@year", cbo_year.Text);
                cmd.Parameters.AddWithValue("@status", "UN-VOTED");
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Student Register Success!", "VOTE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Student Register Failed!", "VOTE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                clear();
                AutoStudentID();
                LoadStudentData();
            }
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_studentPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
