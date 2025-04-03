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
using Guna.UI2.WinForms;

namespace student_e_voting.ADMIN
{
    public partial class frm_result : Form
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
        public frm_result()
        {
            InitializeComponent();
            conn = new MySqlConnection(connString); // Initialize the conn variable
            LoadData();
        }

        private void frm_result_Load(object sender, EventArgs e)
        {
            LoadData();
            DataGridView1.RowTemplate.Height = 35;
            //Load_result();
            Load_ovresult();
        }

        /**private void Load_result()
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT `candidatename` as NAME, `position` as POSITION,COUNT(*)AS VOTE FROM `tbl_vote` GROUP BY `candidatename` HAVING (COUNT(*)>=1) ORDER BY `position`", conn);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd); // Corrected error: Need to instantiate MySqlDataAdapter
            da.Fill(dt);
            DataGridView1.DataSource = dt;

            conn.Close();
        }**/

        private void Load_ovresult()
        {
            try
            {
                conn.Open(); // Open the connection

                MySqlCommand cmd = new MySqlCommand("SELECT `candidatename` as NAME, `position` as POSITION, COUNT(*) AS VOTE FROM `tbl_vote` GROUP BY `candidatename`, `position` HAVING (COUNT(*) >= 1)", conn);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Define position order
                Dictionary<string, int> positionOrder = new Dictionary<string, int>
        {
            { "PRESIDENT", 1 },
            { "VICE PRESIDENT", 2 },
            { "SECRETARY", 3 },
            { "TREASURER", 4 },
            { "AUDITOR", 5 },
            { "P.I.O", 6 },
            { "1ST YEAR REPRESENTATIVE", 7 },
            { "2ND YEAR REPRESENTATIVE", 8 },
            { "3RD YEAR REPRESENTATIVE", 9 },
            { "4TH YEAR REPRESENTATIVE", 10 }
        };

                // Add a new column to store the numeric order
                dt.Columns.Add("PositionOrder", typeof(int));

                // Set the numeric order for each position
                foreach (DataRow row in dt.Rows)
                {
                    string position = row["POSITION"].ToString().ToUpper();
                    int order = positionOrder.ContainsKey(position) ? positionOrder[position] : int.MaxValue;
                    row["PositionOrder"] = order;
                }

                // Sort the DataTable based on the position order and vote count
                DataView dv = dt.DefaultView;
                dv.Sort = "PositionOrder ASC, VOTE DESC";
                DataTable sortedDt = dv.ToTable();

                // Remove the temporary PositionOrder column
                sortedDt.Columns.Remove("PositionOrder");

                // Create a new DataTable to hold the sorted data with blank rows
                DataTable newData = new DataTable();
                foreach (DataColumn col in sortedDt.Columns)
                {
                    newData.Columns.Add(col.ColumnName, col.DataType);
                }

                // Insert blank rows between positions
                string currentPos = "";
                foreach (DataRow row in sortedDt.Rows)
                {
                    string position = row["POSITION"].ToString().ToUpper();
                    if (currentPos != position)
                    {
                        newData.Rows.Add(""); // Insert a blank row
                        currentPos = position;
                    }
                    newData.ImportRow(row);
                }

                DataGridView1.DataSource = newData;

                foreach (DataGridViewColumn column in DataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close(); // Close the connection
                }
            }
        }



        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
