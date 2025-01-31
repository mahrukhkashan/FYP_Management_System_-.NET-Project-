using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjectA_2022_CS_45_.GroupFormation
{
    public partial class AssignProject : Form
    {
        private SqlConnection connection;
        public AssignProject()
        {
            InitializeComponent();
            connection = Configuration.getInstance().getConnection();
        }

        private void AssignProject_Load(object sender, EventArgs e)
        {
            PopulateGroupIDsComboBox();
            PopulateProjectTitlesComboBox();
          
        }

        private void PopulateProjectTitlesComboBox()
        {

            ProjectTitle_comboBox.Items.Clear();

            try
            {
                OpenConnection(); // Open the connection
                string query = "SELECT Title FROM Project WHERE Id NOT IN (SELECT ProjectId FROM GroupProject)";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string projectTitle = reader.GetString(0);
                            ProjectTitle_comboBox.Items.Add(projectTitle);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while populating project titles: " + ex.Message);
            }
            finally
            {
                CloseConnection(); // Close the connection
            }
        }

        private void PopulateGroupIDsComboBox()
        {

            GroupID_comboBox.Items.Clear();
            try
            {
                OpenConnection(); // Open the connection

                string query = @"
                    SELECT gs.GroupId
                    FROM GroupStudent gs
                    LEFT JOIN GroupProject gp ON gs.GroupId = gp.GroupId
                    WHERE gp.GroupId IS NULL
                    GROUP BY gs.GroupId";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int groupId = reader.GetInt32(0);
                            GroupID_comboBox.Items.Add(groupId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while populating group IDs: " + ex.Message);
            }
            finally
            {
                CloseConnection(); // Close the connection
            }
        }

        private void assign_button_Click(object sender, EventArgs e)
        {
            if (ProjectTitle_comboBox.SelectedItem == null || GroupID_comboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select both a project title and a group ID.");
                return;
            }

            string selectedProjectTitle = ProjectTitle_comboBox.SelectedItem.ToString();
            int selectedGroupId = Convert.ToInt32(GroupID_comboBox.SelectedItem);

            UpdateDatabase(selectedProjectTitle, selectedGroupId);

            // Hide the current form
            this.Hide();

            // Show the current form again
            this.ShowDialog(); // Use ShowDialog to keep the form modal
        }

        private void UpdateDatabase(string projectTitle, int groupId)
        { 

            int projectId = GetProjectId(projectTitle);

            try
            {
                OpenConnection(); // Open the connection

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    string insertGroupProjectQuery = "INSERT INTO GroupProject (GroupId, ProjectId, AssignmentDate) VALUES (@GroupId, @ProjectId, @AssignmentDate)";

                    using (SqlCommand insertGroupProjectCommand = new SqlCommand(insertGroupProjectQuery, connection, transaction))
                    {
                        insertGroupProjectCommand.Parameters.AddWithValue("@GroupId", groupId);
                        insertGroupProjectCommand.Parameters.AddWithValue("@ProjectId", projectId);
                        insertGroupProjectCommand.Parameters.AddWithValue("@AssignmentDate", DateTime.Now);
                        insertGroupProjectCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Project assigned to group successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred during update: {ex.Message}");
                MessageBox.Show("An error occurred while updating the database. Please check the logs for details.");
            }
            finally
            {
                CloseConnection(); // Close the connection
            }
        }

        private int GetProjectId(string projectTitle)
        {

            int projectId = -1;

            try
            {
                OpenConnection(); // Open the connection

                string query = "SELECT Id FROM Project WHERE Title = @Title";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", projectTitle);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        projectId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while getting project ID: " + ex.Message);
            }
            finally
            {
                CloseConnection(); // Close the connection
            }

            return projectId;
        }

        private void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        private void CloseConnection()
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }


        private void back_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewAlreadyCreatedGroups viewAlreadyCreatedGroups = new ViewAlreadyCreatedGroups();
            viewAlreadyCreatedGroups.Show();
        }
    }
}
