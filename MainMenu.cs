using ProjectA_2022_CS_45_.Advisor;
using ProjectA_2022_CS_45_.Evaluation;
using ProjectA_2022_CS_45_.GroupFormation;
using ProjectA_2022_CS_45_.Project;
using ProjectA_2022_CS_45_.Reports;
using ProjectA_2022_CS_45_.Student;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectA_2022_CS_45_
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ManageStudents_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddStudents AddStudents = new AddStudents();
            AddStudents.ShowDialog();
        }

        private void ManageAdvisors_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddAdvisor AddAdvisor = new AddAdvisor();
            AddAdvisor.ShowDialog();

        }

        private void ManageProjects_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewProjects viewProjects = new ViewProjects();
            viewProjects.ShowDialog();

        }

        private void StudentsGroup_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewAlreadyCreatedGroups viewAlreadyCreatedGroups = new ViewAlreadyCreatedGroups();
            viewAlreadyCreatedGroups.Show();

        }

        private void ProjectToStudents_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewAlreadyCreatedGroups viewAlreadyCreatedGroups = new ViewAlreadyCreatedGroups();
            viewAlreadyCreatedGroups.Show();
        }

        private void AdvisorsToProjects_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            AssignAdvisors assignadvisors = new AssignAdvisors();
            assignadvisors.ShowDialog();
        }

        private void ManageEvaluation_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewEvaluations viewEvaluations = new ViewEvaluations();
            viewEvaluations.ShowDialog();
        }

        private void MarkEvaluation_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            MarkEvaluation markevaluation = new MarkEvaluation();
            markevaluation.ShowDialog();
        }

        private void GenerateReports_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            GenerateReportsForm generatereportform = new GenerateReportsForm();
            generatereportform.ShowDialog();
        }

        private void EXIT_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void assignProjects_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewAlreadyCreatedGroups viewAlreadyCreatedGroups = new ViewAlreadyCreatedGroups();
            viewAlreadyCreatedGroups.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
