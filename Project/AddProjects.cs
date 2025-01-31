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

namespace ProjectA_2022_CS_45_.Project
{
    public partial class AddProjects : Form
    {
        public AddProjects()
        {
            InitializeComponent();
        }

        private void AddProjects_Load(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var connection = Configuration.getInstance().getConnection();


            // Insert data into the Project table
            string projectInsertQuery = "INSERT INTO Project (Description, Title) VALUES (@Description, @Title); SELECT SCOPE_IDENTITY();";
            SqlCommand projectInsertCommand = new SqlCommand(projectInsertQuery, connection);
            projectInsertCommand.Parameters.AddWithValue("@Description", Description_textBox.Text);
            projectInsertCommand.Parameters.AddWithValue("@Title", Title_textBox.Text);

            // Execute the command and retrieve the generated ID
            int projectId = Convert.ToInt32(projectInsertCommand.ExecuteScalar());

            MessageBox.Show("Project successfully added with ID: " + projectId);

        }

        private void Back_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewProjects viewProjects = new ViewProjects();
            viewProjects.Show();
        }
    }
}
