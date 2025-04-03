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
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using student_e_voting.ADMIN;
using System.Windows.Input;
using System.Xml.Linq;

namespace student_e_voting.STUDENT
{
    public partial class frm_VoteCandidate : Form
    {
        private frm_studentMain studentMainForm;

        private frm_studentDashboard studentDashboardForm;
        //set yung picture to circular
        private string studId;
        public class CircularPictureBox : PictureBox
        {
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(ClientRectangle);
                this.Region = new Region(path);
            }
        }

        public string connString = "datasource=localhost;username=root;password=;database=election";
        MySqlConnection conn;
        MySqlCommand cmd;
        int i;
        private Panel pan = new Panel();
        private CircularPictureBox pic_candi = new CircularPictureBox();
        private Label namelbl = new Label();
        private MySqlDataReader dr;



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
        public frm_VoteCandidate(frm_studentDashboard studentDashboardForm, string studId, string studentId)
        {
            InitializeComponent();
            this.studentDashboardForm = studentDashboardForm;
            this.studId = studId;
            conn = new MySqlConnection(connString); // Initialize the conn variable
            LoadData();
            Update_studentVoteList();
            Load_PresidentList();
            Load_VPresList();
            Load_Secretary();
            Load_Treasurer();
            Load_Auditor();
            Load_PIO();
            Load_1Year();
            Load_2Year();
            Load_3Year();
            Load_4Year();
            this.studId = studId;
        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void frm_VoteCandidate_Load(object sender, EventArgs e)
        {
            LoadData();
            Update_studentVoteList();
            Load_PresidentList();
            Load_VPresList();
            Load_Secretary();
            Load_Treasurer();
            Load_Auditor();
            Load_PIO();
            Load_1Year();
            Load_2Year();
            Load_3Year();
            Load_4Year();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Load_controls(string name, string course, string year)
        {
            long len = dr.GetBytes(0, 0, null, 0, 0);
            byte[] array = new byte[len];
            dr.GetBytes(0, 0, array, 0, (int)len);

            pan = new Panel();
            pan.Width = 110;
            pan.Height = 120;
            pan.BackColor = Color.White;
            pan.Tag = name;

            pic_candi = new CircularPictureBox();
            pic_candi.Height = 70;
            pic_candi.BackgroundImageLayout = ImageLayout.Stretch;
            pic_candi.Dock = DockStyle.Bottom; // Dock the circular picture box to the bottom of the panel
            pic_candi.Tag = name;

            namelbl = new Label();
            namelbl.ForeColor = Color.Black;
            namelbl.BackColor = Color.White;
            namelbl.TextAlign = ContentAlignment.MiddleCenter;
            namelbl.Font = new Font("Segoe UI", 7, FontStyle.Bold);
            namelbl.Dock = DockStyle.Top; // Dock the label to the top of the panel
            namelbl.Tag = name;
            namelbl.Text = $"{name}\n{course}, {year}";

            // Load image from database
            using (MemoryStream ms = new MemoryStream(array))
            {
                Bitmap bitmap = new Bitmap(ms);
                pic_candi.BackgroundImage = bitmap;
                pic_candi.Click += Selectimg_Click;
                namelbl.Click += Selectimg_Click;
            }
            pan.Controls.Add(namelbl); // Add the label to the panel
            pan.Controls.Add(pic_candi); // Add the circular picture box to the panel

            // Add the panel to the appropriate panel control based on the position
            switch (dr["position"].ToString())
            {
                case "PRESIDENT":
                    panel_pres.Controls.Add(pan);
                    break;
                case "VICE PRESIDENT":
                    panel_vpres.Controls.Add(pan);
                    break;
                case "SECRETARY":
                    panel_secretary.Controls.Add(pan);
                    break;
                case "TREASURER":
                    panel_treasurer.Controls.Add(pan);
                    break;
                case "AUDITOR":
                    panel_auditor.Controls.Add(pan);
                    break;
                case "P.I.O":
                    panel_pio.Controls.Add(pan);
                    break;
                case "1ST YEAR REPRESENTATIVE":
                    panel_1st.Controls.Add(pan);
                    break;
                case "2ND YEAR REPRESENTATIVE":
                    panel_2nd.Controls.Add(pan);
                    break;
                case "3RD YEAR REPRESENTATIVE":
                    panel_3rd.Controls.Add(pan);
                    break;
                case "4TH YEAR REPRESENTATIVE":
                    panel_4th.Controls.Add(pan);
                    break;
                default:
                    // Handle other positions here
                    break;
            }

        }


        private void Selectimg_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbl_candidate WHERE name like '%" + ((Control)sender).Tag.ToString() + "%'", conn);
                MySqlDataReader dr = cmd.ExecuteReader();

                bool exist = false; // Initialize the flag outside the loop

                while (dr.Read())
                {
                    exist = false; // Reset the flag for each record

                    foreach (DataGridViewRow itm in DataGridView1.Rows)
                    {
                        if (itm.Cells[3].Value != null && itm.Cells[3].Value.ToString() == dr["position"].ToString())
                        {
                            exist = true;
                            break;
                        }
                    }

                    if (!exist)
                    {
                        DataGridView1.Rows.Add(dr["name"], dr["course"], dr["year"], dr["position"]);
                    }
                }

                if (exist)
                {
                    MessageBox.Show("Already Selected!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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




        private void Load_PresidentList()
        {
            panel_pres.Controls.Clear();
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT `img`, `name`, `course`, `year`, `position` FROM `tbl_candidate` WHERE `position`='PRESIDENT'", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Load_controls(dr["name"].ToString(), dr["course"].ToString(), dr["year"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        private void Load_VPresList()
        {
            panel_vpres.Controls.Clear();
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT `img`, `name`, `course`, `year`, `position` FROM `tbl_candidate` WHERE `position`='VICE PRESIDENT'", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Load_controls(dr["name"].ToString(), dr["course"].ToString(), dr["year"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        private void Load_Secretary()
        {
            panel_secretary.Controls.Clear();
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT `img`, `name`, `course`, `year`, `position` FROM `tbl_candidate` WHERE `position`='SECRETARY'", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Load_controls(dr["name"].ToString(), dr["course"].ToString(), dr["year"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        private void Load_Treasurer()
        {
            panel_treasurer.Controls.Clear();
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT `img`, `name`, `course`, `year`, `position` FROM `tbl_candidate` WHERE `position`='TREASURER'", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Load_controls(dr["name"].ToString(), dr["course"].ToString(), dr["year"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        private void Load_Auditor()
        {
            panel_auditor.Controls.Clear();
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT `img`, `name`, `course`, `year`, `position` FROM `tbl_candidate` WHERE `position`='AUDITOR'", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Load_controls(dr["name"].ToString(), dr["course"].ToString(), dr["year"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        private void Load_PIO()
        {
            panel_pio.Controls.Clear();
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT `img`, `name`, `course`, `year`, `position` FROM `tbl_candidate` WHERE `position`='P.I.O'", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Load_controls(dr["name"].ToString(), dr["course"].ToString(), dr["year"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        private void Load_1Year()
        {
            panel_1st.Controls.Clear();
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT `img`, `name`, `course`, `year`, `position` FROM `tbl_candidate` WHERE `position`='1ST YEAR REPRESENTATIVE'", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Load_controls(dr["name"].ToString(), dr["course"].ToString(), dr["year"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        private void Load_2Year()
        {
            panel_2nd.Controls.Clear();
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT `img`, `name`, `course`, `year`, `position` FROM `tbl_candidate` WHERE `position`='2ND YEAR REPRESENTATIVE'", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Load_controls(dr["name"].ToString(), dr["course"].ToString(), dr["year"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        private void Load_3Year()
        {
            panel_3rd.Controls.Clear();
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT `img`, `name`, `course`, `year`, `position` FROM `tbl_candidate` WHERE `position`='3RD YEAR REPRESENTATIVE'", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Load_controls(dr["name"].ToString(), dr["course"].ToString(), dr["year"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        private void Load_4Year()
        {
            panel_4th.Controls.Clear();
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT `img`, `name`, `course`, `year`, `position` FROM `tbl_candidate` WHERE `position`='4TH YEAR REPRESENTATIVE'", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Load_controls(dr["name"].ToString(), dr["course"].ToString(), dr["year"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_vote_Click(object sender, EventArgs e)
        {
            if (DataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Please select candidates to vote for before proceeding.", "No Candidates Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conn.Open();
                for (int j = 0; j < DataGridView1.Rows.Count; j++)
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO `tbl_vote`(`stuid`, `votedatetime`, `candidatename`, `course`, `year`, `position`) VALUES (@stuid,@votedatetime,@candidatename,@course,@year,@position)", conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@stuid", studId); // Access studId directly from frm_VoteCandidate
                    cmd.Parameters.AddWithValue("@votedatetime", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));
                    cmd.Parameters.AddWithValue("@candidatename", DataGridView1.Rows[j].Cells[0].Value);
                    cmd.Parameters.AddWithValue("@course", DataGridView1.Rows[j].Cells[1].Value);
                    cmd.Parameters.AddWithValue("@year", DataGridView1.Rows[j].Cells[2].Value);
                    cmd.Parameters.AddWithValue("@position", DataGridView1.Rows[j].Cells[3].Value);

                    int i = cmd.ExecuteNonQuery();
                    if (i <= 0)
                    {
                        MessageBox.Show("Warning: Voting Some Failure!", "VOTE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                studentDashboardForm.UpdateStatus("VOTED");
                MessageBox.Show("Thanks for Voting!", "VOTE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                DataGridView1.Rows.Clear();
                Update_studentVoteList();
                this.Close();
                LoadData();
                studentDashboardForm.Show();
                LoadData();
            }
            LoadData();
        }
        private void Update_studentVoteList()
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE tbl_student SET status=@status WHERE stuid=@stuid", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@status", "VOTED");
                cmd.Parameters.AddWithValue("@stuid", studId);
                int i = cmd.ExecuteNonQuery();
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

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {

        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            // Clear the DataGridView
            DataGridView1.Rows.Clear();

            // Clear the selected image
            foreach (Control control in Controls)
            {
                if (control is Panel panel)
                {
                    foreach (Control subControl in panel.Controls)
                    {
                        if (subControl is PictureBox pictureBox)
                        {
                            pictureBox.BackgroundImage = null;
                        }
                    }
                }
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void btn_exit_Click_1(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure you want to go back?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE tbl_student SET status=@status WHERE stuid=@stuid", conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@status", "UN-VOTED");
                    cmd.Parameters.AddWithValue("@stuid", studId);
                    int i = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                // Close the current form
                this.Close();

                // Show the studentDashboardForm without updating the status
                studentDashboardForm.Show();
            }

            
        }
        
    }
}
