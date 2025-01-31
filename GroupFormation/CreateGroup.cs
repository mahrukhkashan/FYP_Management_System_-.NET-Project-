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

namespace ProjectA_2022_CS_45_.GroupFormation
{
    public partial class CreateGroup : Form
    {
        public CreateGroup()
        {
            InitializeComponent();
            PopulateComboBox(); // Call PopulateComboBox when the form loads

            // Add columns to the DataGridView
            dataGridViewMembers.Columns.Add("StudentId", "Student ID");
            dataGridViewMembers.Columns.Add("RegistrationNo", "Registration Number");
        }

        private void CreateGroup_Load(object sender, EventArgs e)
        {

        }

        private void PopulateComboBox()
        {
            var connection = Configuration.getInstance().getConnection();

            try
            {
                connection.Open(); // Open the connection before executing the command

                string query = "SELECT s.Id, s.RegistrationNo FROM Student s " +
                                "LEFT JOIN GroupStudent gs ON s.Id = gs.StudentId " +
                                "WHERE gs.GroupId IS NULL";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int studentId = reader.GetInt32(0);
                    string registrationNo = reader.GetString(1);
                    comboBoxStudents.Items.Add(new KeyValuePair<int, string>(studentId, registrationNo));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while populating the combo box: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close(); // Close the connection in the finally block
                }
            }
        }

        private void Add_button_Click(object sender, EventArgs e)
        {
            if (dataGridViewMembers.Rows.Count < 5 && comboBoxStudents.SelectedItem != null)
            {
                KeyValuePair<int, string> selectedStudent = (KeyValuePair<int, string>)comboBoxStudents.SelectedItem;
                dataGridViewMembers.Rows.Add(selectedStudent.Key, selectedStudent.Value);
            }
            else
            {
                MessageBox.Show("You can only add up to 4 members to the group.");
            }

        }

        private void create_button_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = Configuration.getInstance().getConnection())
                {
                    connection.Open();

                    // Get the next Group ID
                    int nextGroupId = GetNextGroupId(connection);

                    // Ensure that the GroupId exists in the Group table
                    if (!IsGroupIdValid(connection, nextGroupId))
                    {
                        MessageBox.Show("Invalid GroupId. The group may not have been created successfully.");
                        return;
                    }

                    string insertGroupQuery = "INSERT INTO [dbo].[Group] (Created_On) VALUES (@CreatedOn);";
                    using (SqlCommand insertGroupCommand = new SqlCommand(insertGroupQuery, connection))
                    {
                        insertGroupCommand.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                        insertGroupCommand.ExecuteNonQuery();
                    }

                    // Insert students into GroupStudent table
                    foreach (DataGridViewRow row in dataGridViewMembers.Rows)
                    {
                        int studentId = Convert.ToInt32(row.Cells["StudentId"].Value);

                        string insertGroupStudentQuery = "INSERT INTO [dbo].[GroupStudent] (GroupId, StudentId, Status, AssignmentDate) VALUES (@GroupId, @StudentId, @Status, @AssignmentDate)";
                        using (SqlCommand insertGroupStudentCommand = new SqlCommand(insertGroupStudentQuery, connection))
                        {
                            insertGroupStudentCommand.Parameters.AddWithValue("@GroupId", nextGroupId);
                            insertGroupStudentCommand.Parameters.AddWithValue("@StudentId", studentId);
                            insertGroupStudentCommand.Parameters.AddWithValue("@Status", GetInactiveStatusId(connection));
                            insertGroupStudentCommand.Parameters.AddWithValue("@AssignmentDate", DateTime.Now); // Set assignment date to current date
                            insertGroupStudentCommand.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Group created successfully!");

                    // After successfully creating the group, reload data in ViewAlreadyCreatedGroups
                    ViewAlreadyCreatedGroups viewAlreadyCreatedGroups = new ViewAlreadyCreatedGroups();
                    viewAlreadyCreatedGroups.LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private int GetInactiveStatusId(SqlConnection connection)
        {

            string query = "SELECT Id FROM Lookup WHERE Value = 'Inactive' AND Category = 'STATUS'";
            SqlCommand command = new SqlCommand(query, connection);
            int inactiveStatusId = Convert.ToInt32(command.ExecuteScalar());

            // No need to close the connection here as it's a shared connection and will be closed later

            return inactiveStatusId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an instance of ViewAlreadyCreatedGroups form
            ViewAlreadyCreatedGroups viewAlreadyCreatedGroups = new ViewAlreadyCreatedGroups();

            // Load data in ViewAlreadyCreatedGroups form
            viewAlreadyCreatedGroups.LoadData();

            // Show the ViewAlreadyCreatedGroups form
            viewAlreadyCreatedGroups.Show();

            // Hide the current form (CreateGroup)
            this.Hide();
        }

        private int GetNextGroupId(SqlConnection connection)
        {
            try
            {
                // Ensure the connection is open before executing the command
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string query = "SELECT ISNULL(MAX(Id), 0) + 1 FROM [Group]";
                SqlCommand command = new SqlCommand(query, connection);
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return 1; // If no groups found, start with GroupId = 1
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching the next group ID: " + ex.Message);
                return -1; // Return a default value or handle the error appropriately
            }
        }

        private bool IsGroupIdValid(SqlConnection connection, int groupId)
        {
            string query = "SELECT COUNT(*) FROM [dbo].[Group] WHERE Id = @GroupId";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@GroupId", groupId);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
    }
}


