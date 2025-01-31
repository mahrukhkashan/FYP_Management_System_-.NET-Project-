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
    public partial class ViewProjects : Form
    {
        private DataTable projectDataTable;
        public ViewProjects()
        {
            InitializeComponent();
        }

        private void ViewProjects_Load(object sender, EventArgs e)
        {
            LoadProjectData();
        }

        private void Addbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddProjects AddProjects = new AddProjects();
            AddProjects.ShowDialog();
        }

        private void LoadProjectData()
        {
            var connection = Configuration.getInstance().getConnection();

            // Fetch project data from the database
            string query = "SELECT Title, Description FROM Project";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            projectDataTable = new DataTable();
            adapter.Fill(projectDataTable);

            // Bind the DataTable to the DataGridView
            ProjectDataGridView.DataSource = projectDataTable;

            // Increase the width of the first column
            ProjectDataGridView.Columns["Title"].Width = 200;
            ProjectDataGridView.Columns["Description"].Width = 300;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.ShowDialog();
        }

        private void ProjectDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

    }
}
