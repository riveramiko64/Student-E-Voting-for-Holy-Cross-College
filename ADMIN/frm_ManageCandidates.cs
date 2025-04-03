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
using static Guna.UI2.WinForms.Helpers.GraphicsHelper;
using System.Windows.Input;
using System.Xml.Linq;

namespace student_e_voting.ADMIN
{
    public partial class frm_ManageCandidates : Form
    {
        public string connString = "datasource=localhost;username=root;password=;database=election";
        MySqlConnection conn;
        MySqlCommand cmd;
        int i;

        private byte[] RetrieveImageFromDatabase(string candidateName)
        {
            byte[] imageData = null;

            try
            {
                conn.Open();
                string query = "SELECT img FROM tbl_candidate WHERE name = @name";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", candidateName);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    imageData = (byte[])result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving image from database: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            return imageData;
        }


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
        public frm_ManageCandidates()
        {
            InitializeComponent();
            conn = new MySqlConnection(connString); // Initialize the conn variable
            LoadData();
            edit_candid();
            DataGridViewEdit.CellClick += DataGridViewEdit_CellContentClick;

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void pic_candidateImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                pic_candidateImg.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void clear()
        {
            txt_name.Clear();
            cbo_course.SelectedIndex = -1;
            cbo_position.SelectedIndex = -1;
            cbo_year.SelectedIndex = -1;
            pic_candidateImg.Image = null;
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            
        }

        private void edit_candid()
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT `ID`, `name` as NAME, `course` as COURSE, `year` as YEAR, `position` as POSITION FROM `tbl_candidate`", conn); // Include ID in the query
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

            // Sort the DataTable based on the position order and name
            DataView dv = dt.DefaultView;
            dv.Sort = "PositionOrder ASC, NAME ASC";
            DataTable sortedDt = dv.ToTable();

            // Remove the temporary PositionOrder column
            sortedDt.Columns.Remove("PositionOrder");

            // Create a new DataTable to hold the sorted data without blank rows
            DataTable newData = new DataTable();
            newData.Columns.Add("ID"); // Add ID column
            newData.Columns.Add("NAME");
            newData.Columns.Add("COURSE");
            newData.Columns.Add("YEAR");
            newData.Columns.Add("POSITION");
            newData.Columns.Add("IMAGE", typeof(Image));

            // Add rows with candidate information only
            foreach (DataRow row in sortedDt.Rows)
            {
                // Add candidate information
                newData.ImportRow(row);
            }

            DataGridViewEdit.DataSource = newData; // Set the data source to DataGridViewEdit
            DataGridViewEdit.Columns["IMAGE"].Visible = false;
            DataGridViewEdit.Columns["ID"].Visible = false;

            foreach (DataGridViewColumn column in DataGridViewEdit.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            conn.Close();
        }


        private void frm_ManageCandidates_Load(object sender, EventArgs e)
        {
            LoadData();
            edit_candid();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_editName_TextChanged(object sender, EventArgs e)
        {

        }

        private void pic_editImg_Click(object sender, EventArgs e)
        {
            
        }

        private void cbo_editPosition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DataGridViewEdit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < DataGridViewEdit.Rows.Count)
            {
                DataGridViewRow selectedRow = DataGridViewEdit.Rows[e.RowIndex];

                // Retrieve data from the selected row
                string id = selectedRow.Cells["ID"].Value.ToString(); // Retrieve ID
                string name = selectedRow.Cells["NAME"].Value.ToString();
                string course = selectedRow.Cells["COURSE"].Value.ToString();
                string year = selectedRow.Cells["YEAR"].Value.ToString();
                string position = selectedRow.Cells["POSITION"].Value.ToString();
                Image image = null;

                try
                {
                    // Check if the "IMAGE" cell value is DBNull
                    if (selectedRow.Cells["IMAGE"].Value != DBNull.Value)
                    {
                        byte[] imageData = (byte[])selectedRow.Cells["IMAGE"].Value;

                        // Convert byte array to image
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        // Retrieve image from the database using the candidate's name
                        string candidateName = selectedRow.Cells["NAME"].Value.ToString();
                        byte[] imageDataFromDB = RetrieveImageFromDatabase(candidateName);

                        if (imageDataFromDB != null)
                        {
                            using (MemoryStream ms = new MemoryStream(imageDataFromDB))
                            {
                                image = Image.FromStream(ms);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Image Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Display data in corresponding controls
                lbl_id.Text = id; // Display ID in a label or textbox
                txt_editName.Text = name;
                cbo_editCourse.Text = course;
                cbo_editYear.Text = year;
                cbo_editPosition.Text = position;
                pic_editImg.Image = image;
            }
        }

        private void update_candidate()
        {
            try
            {
                LoadData();
                conn.Open();

                // Retrieve the old candidate information from tbl_candidate
                string oldName = "";
                string oldCourse = "";
                string oldYear = "";
                string oldPosition = "";

                MySqlCommand selectCmd = new MySqlCommand("SELECT `name`, `course`, `year`, `position` FROM `tbl_candidate` WHERE `ID` = @id", conn);
                selectCmd.Parameters.AddWithValue("@id", lbl_id.Text);

                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        oldName = reader.GetString(0);
                        oldCourse = reader.GetString(1);
                        oldYear = reader.GetString(2);
                        oldPosition = reader.GetString(3);
                    }
                    else
                    {
                        MessageBox.Show("Candidate not found!", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Exit method if candidate not found
                    }
                }

                // Execute the UPDATE command for tbl_candidate
                string candidateUpdateQuery = "UPDATE `tbl_candidate` SET `name` = @name, `course` = @course, `year` = @year, `position` = @position WHERE `ID` = @id";

                MySqlCommand candidateUpdateCmd = new MySqlCommand(candidateUpdateQuery, conn);
                candidateUpdateCmd.Parameters.AddWithValue("@name", txt_editName.Text);
                candidateUpdateCmd.Parameters.AddWithValue("@course", cbo_editCourse.Text);
                candidateUpdateCmd.Parameters.AddWithValue("@year", cbo_editYear.Text);
                candidateUpdateCmd.Parameters.AddWithValue("@position", cbo_editPosition.Text);
                candidateUpdateCmd.Parameters.AddWithValue("@id", lbl_id.Text);

                int candidateRowsAffected = candidateUpdateCmd.ExecuteNonQuery();

                // If candidate information is updated successfully, update corresponding rows in tbl_vote if necessary
                if (candidateRowsAffected > 0)
                {
                    // Update matching rows in tbl_vote
                    MySqlCommand voteUpdateCmd = new MySqlCommand("UPDATE `tbl_vote` SET `candidatename` = @newName, `course` = @newCourse, `year` = @newYear, `position` = @newPosition WHERE `candidatename` = @oldName AND `course` = @oldCourse AND `year` = @oldYear AND `position` = @oldPosition", conn);
                    voteUpdateCmd.Parameters.AddWithValue("@newName", txt_editName.Text);
                    voteUpdateCmd.Parameters.AddWithValue("@newCourse", cbo_editCourse.Text);
                    voteUpdateCmd.Parameters.AddWithValue("@newYear", cbo_editYear.Text);
                    voteUpdateCmd.Parameters.AddWithValue("@newPosition", cbo_editPosition.Text);
                    voteUpdateCmd.Parameters.AddWithValue("@oldName", oldName);
                    voteUpdateCmd.Parameters.AddWithValue("@oldCourse", oldCourse);
                    voteUpdateCmd.Parameters.AddWithValue("@oldYear", oldYear);
                    voteUpdateCmd.Parameters.AddWithValue("@oldPosition", oldPosition);

                    int voteRowsAffected = voteUpdateCmd.ExecuteNonQuery();

                    // Debug messages to print out old and new values
                    MessageBox.Show($"Old Name: {oldName}, Old Course: {oldCourse}, Old Year: {oldYear}, Old Position: {oldPosition}");
                    MessageBox.Show($"New Name: {txt_editName.Text}, New Course: {cbo_editCourse.Text}, New Year: {cbo_editYear.Text}, New Position: {cbo_editPosition.Text}");

                    if (voteRowsAffected > 0)
                    {
                        MessageBox.Show("Candidate information updated successfully along with corresponding votes!", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Candidate information updated successfully!", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to update candidate information.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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





        private void delete_candid()
        {
            try
            {
                LoadData();
                conn.Open();

                // Retrieve candidate information before deletion
                string name = "";
                string course = "";
                string year = "";
                string position = "";

                MySqlCommand selectCmd = new MySqlCommand("SELECT `name`, `course`, `year`, `position` FROM `tbl_candidate` WHERE `ID` = @id", conn);
                selectCmd.Parameters.AddWithValue("@id", lbl_id.Text);

                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        name = reader.GetString(0);
                        course = reader.GetString(1);
                        year = reader.GetString(2);
                        position = reader.GetString(3);
                    }
                    else
                    {
                        MessageBox.Show("Candidate not found!", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Exit method if candidate not found
                    }
                }

                // Ask for confirmation before deleting
                DialogResult result = MessageBox.Show("Are you sure you want to delete this candidate?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Execute the DELETE command for tbl_candidate
                    MySqlCommand deleteCmd = new MySqlCommand("DELETE FROM `tbl_candidate` WHERE `ID` = @id", conn);
                    deleteCmd.Parameters.AddWithValue("@id", lbl_id.Text);
                    int rowsAffectedCandidate = deleteCmd.ExecuteNonQuery();

                    // Execute the DELETE command for matching rows in tbl_vote
                    MySqlCommand deleteVoteCmd = new MySqlCommand("DELETE FROM `tbl_vote` WHERE `candidatename` = @name AND `course` = @course AND `year` = @year AND `position` = @position", conn);
                    deleteVoteCmd.Parameters.AddWithValue("@name", name);
                    deleteVoteCmd.Parameters.AddWithValue("@course", course);
                    deleteVoteCmd.Parameters.AddWithValue("@year", year);
                    deleteVoteCmd.Parameters.AddWithValue("@position", position);
                    int rowsAffectedVote = deleteVoteCmd.ExecuteNonQuery();

                    if (rowsAffectedCandidate > 0)
                    {
                        MessageBox.Show("Candidate information deleted successfully!", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        clearControls();
                        // Remove the selected row from the DataGridViewEdit
                        DataGridViewRow selectedRow = DataGridViewEdit.SelectedRows[0];
                        DataGridViewEdit.Rows.Remove(selectedRow);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete candidate information.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void clearControls()
        {
            pic_editImg.Image = null;
            txt_editName.Clear();
            cbo_editCourse.SelectedIndex = -1;
            cbo_editYear.SelectedIndex = -1;
            cbo_editPosition.SelectedIndex = -1;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {

            update_candidate();
            edit_candid();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            delete_candid(); 
            edit_candid();
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pic_candidateImg_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                pic_candidateImg.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void txt_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_register_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Check if any of the required fields are blank
                if (string.IsNullOrWhiteSpace(txt_name.Text) ||
                    string.IsNullOrWhiteSpace(cbo_course.Text) ||
                    string.IsNullOrWhiteSpace(cbo_year.Text) ||
                    string.IsNullOrWhiteSpace(cbo_position.Text) ||
                    pic_candidateImg.Image == null)
                {
                    MessageBox.Show("Please fill in all the required fields.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without proceeding further
                }

                LoadData();
                conn.Open();
                cmd = new MySqlCommand("INSERT INTO `tbl_candidate`( `name`, `course`, `year`, `position`, `img`) VALUES (@name,@course,@year,@position,@img)", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@name", txt_name.Text);
                cmd.Parameters.AddWithValue("@course", cbo_course.Text);
                cmd.Parameters.AddWithValue("@year", cbo_year.Text);
                cmd.Parameters.AddWithValue("@position", cbo_position.Text);

                // Convert image to byte array
                byte[] picture;
                using (MemoryStream mstream = new MemoryStream())
                {
                    pic_candidateImg.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    picture = mstream.ToArray();
                }

                cmd.Parameters.AddWithValue("@img", picture);
                i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    MessageBox.Show("Candidate Register Success !", "VOTE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear(); // Clear the fields only if registration is successful

                }
                else
                {
                    MessageBox.Show("Candidate Register Failed !", "VOTE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
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

        private void cbo_course_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbo_year_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbo_position_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
