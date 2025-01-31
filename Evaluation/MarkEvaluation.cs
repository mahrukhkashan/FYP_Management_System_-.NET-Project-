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
    public partial class MarkEvaluation : Form
    {
        private SqlConnection connection;
        public MarkEvaluation()
        {
            InitializeComponent();
            connection = Configuration.getInstance().getConnection();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void save_button_Click(object sender, EventArgs e)
        {
            //if (GroupID_comboBox.SelectedItem == null || Evaluation_comboBox.SelectedItem == null)
            //{
            //    MessageBox.Show("Please select both Group ID and Evaluation.");
            //    return;
            //}

            //int groupId = (int)GroupID_comboBox.SelectedItem;
            //var selectedEvaluation = (EvaluationItem)Evaluation_comboBox.SelectedItem;
            //int evaluationId = selectedEvaluation.Id;
            //int obtainedMarks = (int)ObtainedMarks_numericUpDown.Value;

            //SaveObtainedMarks(groupId, evaluationId, obtainedMarks);

            if (GroupID_comboBox.SelectedItem == null || Evaluation_comboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select both Group ID and Evaluation.");
                return;
            }

            int groupId = (int)GroupID_comboBox.SelectedItem;
            var selectedEvaluation = (EvaluationItem)Evaluation_comboBox.SelectedItem;
            int evaluationId = selectedEvaluation.Id;
            int obtainedMarks = (int)ObtainedMarks_numericUpDown.Value;
            int totalMarks = Convert.ToInt32(totalmarks_textBox.Text);

            // Validate obtained marks
            if (obtainedMarks > totalMarks)
            {
                MessageBox.Show("Obtained marks cannot exceed total marks.");
                return;
            }

            SaveObtainedMarks(groupId, evaluationId, obtainedMarks);
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        private void MarkEvaluation_Load(object sender, EventArgs e)
        {
            PopulateGroupIDsComboBox();
            PopulateEvaluationComboBox();

        }

        private void PopulateGroupIDsComboBox()
        {
            GroupID_comboBox.Items.Clear();
            try
            {
                OpenConnection();
                string query = "SELECT GroupId FROM GroupProject";

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
                MessageBox.Show("An error occurred while populating Group IDs: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        private void PopulateEvaluationComboBox()
        {
            Evaluation_comboBox.Items.Clear();
            try
            {
                OpenConnection();
                string query = "SELECT Id, Name FROM Evaluation";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int evaluationId = reader.GetInt32(0);
                            string evaluationName = reader.GetString(1);
                            Evaluation_comboBox.Items.Add(new EvaluationItem(evaluationId, evaluationName));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while populating Evaluations: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        private void Evaluation_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Evaluation_comboBox.SelectedItem != null)
            {
                var selectedEvaluation = (EvaluationItem)Evaluation_comboBox.SelectedItem;
                int evaluationId = selectedEvaluation.Id;

                // Fetch Total Marks from database based on the selected Evaluation
                int totalMarks = GetTotalMarks(evaluationId);
                totalmarks_textBox.Text = totalMarks.ToString();
            }
        }

        private int GetTotalMarks(int evaluationId)
        {
            int totalMarks = 0;
            try
            {
                OpenConnection();
                string query = "SELECT TotalMarks FROM Evaluation WHERE Id = @EvaluationId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EvaluationId", evaluationId);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        totalMarks = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving Total Marks: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return totalMarks;
        }

        private void SaveObtainedMarks(int groupId, int evaluationId, int obtainedMarks)
        {
            try
            {
                OpenConnection();
                string query = "INSERT INTO GroupEvaluation (GroupId, EvaluationId, ObtainedMarks, EvaluationDate) VALUES (@GroupId, @EvaluationId, @ObtainedMarks, @EvaluationDate)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GroupId", groupId);
                    command.Parameters.AddWithValue("@EvaluationId", evaluationId);
                    command.Parameters.AddWithValue("@ObtainedMarks", obtainedMarks);
                    command.Parameters.AddWithValue("@EvaluationDate", DateTime.Now);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Obtained marks saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving obtained marks: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
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

        public class EvaluationItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public EvaluationItem(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public override string ToString()
            {
                return Name;
            }

        }
    }
}
