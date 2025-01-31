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
    public partial class AddEvaluation : Form
    {
        public AddEvaluation()
        {
            InitializeComponent();
        }

        private void AddEvaluation_Load(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var connection = Configuration.getInstance().getConnection();

            // Calculate the sum of existing weightages
            string sumQuery = "SELECT SUM(TotalWeightage) FROM Evaluation";
            SqlCommand sumCommand = new SqlCommand(sumQuery, connection);
            object result = sumCommand.ExecuteScalar();
            int currentTotalWeightage = 0;
            if (result != DBNull.Value)
            {
                currentTotalWeightage = Convert.ToInt32(result);
            }

            // Calculate the weightage of the new evaluation
            int newEvaluationWeightage = Convert.ToInt32(totalWeightageNumericUpDown.Value);
            int totalWeightageWithNewEvaluation = currentTotalWeightage + newEvaluationWeightage;

            // Check if adding the new evaluation will exceed 100% weightage
            if (totalWeightageWithNewEvaluation > 100)
            {
                MessageBox.Show("The total weightage is already 100%. You cannot add more evaluations.");
                return; // Exit the method
            }

            // Insert data into the Evaluation table
            string evaluationInsertQuery = "INSERT INTO Evaluation (Name, TotalMarks, TotalWeightage) VALUES (@Name, @TotalMarks, @TotalWeightage); SELECT SCOPE_IDENTITY();";
            SqlCommand evaluationInsertCommand = new SqlCommand(evaluationInsertQuery, connection);
            evaluationInsertCommand.Parameters.AddWithValue("@Name", EvaluationName_textBox.Text);
            evaluationInsertCommand.Parameters.AddWithValue("@TotalMarks", Convert.ToInt32(totalMarksNumericUpDown.Value));
            evaluationInsertCommand.Parameters.AddWithValue("@TotalWeightage", newEvaluationWeightage);

            // Execute the command and retrieve the generated ID
            int evaluationId = Convert.ToInt32(evaluationInsertCommand.ExecuteScalar());

            MessageBox.Show("Evaluation successfully added with ID: " + evaluationId);

        }

        private void Back_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewEvaluations viewEvaluations = new ViewEvaluations();
            viewEvaluations.Show();
        }
    }
}
