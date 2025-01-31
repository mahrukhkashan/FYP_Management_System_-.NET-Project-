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
    public partial class ViewAlreadyCreatedGroups : Form
    {
        public ViewAlreadyCreatedGroups()
        {
            InitializeComponent();
        }

        private void ViewAlreadyCreatedGroups_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            var connection = Configuration.getInstance().getConnection();

            try
            {
                string query = @"SELECT 
                            gs.GroupId, 
                            g.Created_On AS CreatedOnDate, 
                            STRING_AGG(s.RegistrationNo, ', ') AS RegistrationNumbers,
                            STRING_AGG(CONCAT(ps.FirstName, ' ', ps.LastName), ', ') AS StudentNames,
                            COALESCE(gp.ProjectId, 0) AS ProjectId,
                            COALESCE(p.Title, 'Waiting') AS ProjectTitle,
                            COALESCE(CONVERT(VARCHAR(10), gp.AssignmentDate, 101), 'Waiting') AS AssignmentDate,
                            CASE WHEN gp.ProjectId IS NULL THEN 'Waiting' ELSE 'Active' END AS Status
                        FROM 
                            [dbo].[Group] g
                        LEFT JOIN 
                            [dbo].[GroupStudent] gs ON g.Id = gs.GroupId
                        LEFT JOIN 
                            [dbo].[Student] s ON gs.StudentId = s.Id
                        LEFT JOIN 
                            [dbo].[Person] ps ON s.Id = ps.Id
                        LEFT JOIN 
                            [dbo].[GroupProject] gp ON g.Id = gp.GroupId
                        LEFT JOIN 
                            [dbo].[Project] p ON gp.ProjectId = p.Id
                        GROUP BY 
                            gs.GroupId, g.Created_On, gp.ProjectId, p.Title, gp.AssignmentDate";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Change column names to match the desired output
                dataTable.Columns["RegistrationNumbers"].ColumnName = "Registration Numbers";
                dataTable.Columns["StudentNames"].ColumnName = "Student Names";

                GroupdataGridView.DataSource = dataTable;

                // Optional: Print DataTable to Console for Debugging
                PrintDataTable(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void PrintDataTable(DataTable dataTable)
        {
            Console.WriteLine("DataTable Rows:");
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn col in dataTable.Columns)
                {
                    Console.Write(row[col] + "\t");
                }
                Console.WriteLine();
            }
        }

        private void Addbutton_Click(object sender, EventArgs e)
        {
            //var connection = Configuration.getInstance().getConnection();

            //try
            //{
            //    string insertQuery = "INSERT INTO [dbo].[Group] (Created_On) VALUES (@CreatedOn); SELECT SCOPE_IDENTITY();";
            //    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
            //    insertCommand.Parameters.AddWithValue("@CreatedOn", DateTime.Today);

            //    connection.Open();
            //    int groupId = Convert.ToInt32(insertCommand.ExecuteScalar());

            //    MessageBox.Show($"Group with ID {groupId} has been created.");

            //    // Refresh data
            //    LoadData();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("An error occurred while creating a new group: " + ex.Message);
            //}
            //finally
            //{
            //    connection.Close();
            //}


            //this.Hide();
            //NewGroupsForm NewGroupsForm = new NewGroupsForm();
            //NewGroupsForm.ShowDialog();

            this.Hide();
            CreateGroup creategroups = new CreateGroup();
            creategroups.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AssignProject assignProject = new AssignProject();
            assignProject.Show();
        }

        private void GroupsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

