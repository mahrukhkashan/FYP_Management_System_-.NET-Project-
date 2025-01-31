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

namespace ProjectA_2022_CS_45_.Advisor
{
    public partial class AssignAdvisors : Form
    {
        private SqlConnection connection;


        public AssignAdvisors()
        {
            InitializeComponent();
            connection = Configuration.getInstance().getConnection();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if (ProjectTitle_comboBox.SelectedItem == null ||
                MainAdvisor_comboBox.SelectedItem == null ||
                CoAdvisor_comboBox.SelectedItem == null ||
                IndustryAdvisor_comoboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a Project and all Advisors.");
                return;
            }

            SaveAdvisorAssignment();
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        private void AssignAdvisors_Load(object sender, EventArgs e)
        {
            PopulateProjectTitlesComboBox();
            PopulateAdvisorsComboBox();
        }

        private void PopulateProjectTitlesComboBox()
        {
            ProjectTitle_comboBox.Items.Clear();
            try
            {
                OpenConnection();
                string query = "SELECT P.Title FROM Project P INNER JOIN GroupProject GP ON P.Id = GP.ProjectId";

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
                MessageBox.Show("An error occurred while populating Project Titles: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        private void PopulateAdvisorsComboBox()
        {
            MainAdvisor_comboBox.Items.Clear();
            CoAdvisor_comboBox.Items.Clear();
            IndustryAdvisor_comoboBox.Items.Clear();
            try
            {
                OpenConnection();
                string query = @"
            SELECT 
                P.FirstName, 
                P.LastName, 
                L.Value AS Designation
            FROM 
                Advisor A
            INNER JOIN 
                Person P ON A.Id = P.Id
            INNER JOIN 
                Lookup L ON A.Designation = L.Id
            WHERE 
                L.Category = 'DESIGNATION'";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string advisorName = $"{reader.GetString(0)} {reader.GetString(1)}";
                            string designation = reader.GetString(2);
                            switch (designation)
                            {
                                case "Professor":
                                case "Associate Professor":
                                    MainAdvisor_comboBox.Items.Add(advisorName);
                                    break;
                                case "Assisstant Professor":
                                case "Lecturer":
                                    CoAdvisor_comboBox.Items.Add(advisorName);
                                    break;
                                case "Industry Professional":
                                    IndustryAdvisor_comoboBox.Items.Add(advisorName);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while populating Advisors: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        private void SaveAdvisorAssignment()
        {
            try
            {
                OpenConnection();
                int projectId = GetProjectId(ProjectTitle_comboBox.SelectedItem.ToString());
                int mainAdvisorId = GetAdvisorId(MainAdvisor_comboBox.SelectedItem.ToString());
                int coAdvisorId = GetAdvisorId(CoAdvisor_comboBox.SelectedItem.ToString());
                int industryAdvisorId = GetAdvisorId(IndustryAdvisor_comoboBox.SelectedItem.ToString());
                DateTime assignmentDate = DateTime.Now;

                string query = "INSERT INTO ProjectAdvisor (AdvisorId, ProjectId, AdvisorRole, AssignmentDate) VALUES (@MainAdvisorId, @ProjectId, @MainAdvisorRole, @AssignmentDate), " +
                               "(@CoAdvisorId, @ProjectId, @CoAdvisorRole, @AssignmentDate), " +
                               "(@IndustryAdvisorId, @ProjectId, @IndustryAdvisorRole, @AssignmentDate)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MainAdvisorId", mainAdvisorId);
                    command.Parameters.AddWithValue("@CoAdvisorId", coAdvisorId);
                    command.Parameters.AddWithValue("@IndustryAdvisorId", industryAdvisorId);
                    command.Parameters.AddWithValue("@ProjectId", projectId);
                    command.Parameters.AddWithValue("@MainAdvisorRole", (int)AdvisorRole.MainAdvisor);
                    command.Parameters.AddWithValue("@CoAdvisorRole", (int)AdvisorRole.CoAdvisor);
                    command.Parameters.AddWithValue("@IndustryAdvisorRole", (int)AdvisorRole.IndustryAdvisor);
                    command.Parameters.AddWithValue("@AssignmentDate", assignmentDate);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Advisors assigned successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while assigning Advisors: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }


        private int GetProjectId(string projectTitle)
        {
            int projectId = -1;
            try
            {
                OpenConnection();
                string query = "SELECT P.Id FROM Project P INNER JOIN GroupProject GP ON P.Id = GP.ProjectId WHERE P.Title = @Title";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", projectTitle);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        projectId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving Project ID: " + ex.Message);
            }
            return projectId;
        }

        private int GetAdvisorId(string advisorName)
        {
            int advisorId = -1;
            try
            {
                string[] names = advisorName.Split(' ');
                string firstName = names[0];
                string lastName = names[1];
                string query = "SELECT A.Id FROM Advisor A JOIN Person P ON A.Id = P.Id WHERE P.FirstName = @FirstName AND P.LastName = @LastName";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        advisorId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving Advisor ID: " + ex.Message);
            }
            return advisorId;
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
        enum AdvisorRole
        {
            MainAdvisor = 11,
            CoAdvisor = 12,
            IndustryAdvisor = 14
        }
    
    }
}
