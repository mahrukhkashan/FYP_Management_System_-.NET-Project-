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

namespace ProjectA_2022_CS_45_.Evaluation
{
    public partial class ViewEvaluations : Form
    {
        public ViewEvaluations()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        private void Addbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddEvaluation addEvaluation = new AddEvaluation();
            addEvaluation.Show();
        }

        private void LoadEvaluationData()
        {
            var connection = Configuration.getInstance().getConnection();

            // Fetch evaluation data from the database
            string query = "SELECT Name, TotalMarks, TotalWeightage FROM Evaluation";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            // Bind the DataTable to the DataGridView
            EvaluationDataGridView.DataSource = dt;
        }

       

        private void UpdateEvaluationInDatabase(string name, int totalMarks, int totalWeightage)
        {
            var connection = Configuration.getInstance().getConnection();

            // Update the evaluation in the database
            string updateQuery = "UPDATE Evaluation SET TotalMarks = @TotalMarks, TotalWeightage = @TotalWeightage WHERE Name = @Name";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {
                updateCommand.Parameters.AddWithValue("@Name", name);
                updateCommand.Parameters.AddWithValue("@TotalMarks", totalMarks);
                updateCommand.Parameters.AddWithValue("@TotalWeightage", totalWeightage);

                try
                {
                    connection.Open();
                    updateCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating evaluation: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void ViewEvaluations_Load(object sender, EventArgs e)
        {
            LoadEvaluationData();
        }

        
        
    }
}
