using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectA_2022_CS_45_.Advisor
{
    public partial class AddAdvisor : Form
    {
        public AddAdvisor()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var connection = Configuration.getInstance().getConnection();

            // Check if required fields are empty
            if (string.IsNullOrWhiteSpace(AdvisorName_textBox.Text) || // First name
                designation_comboBox.SelectedItem == null) // Designation
            {
                MessageBox.Show("Please fill all the required fields.");
                return;
            }

            // Validate contact number (11 digits)
            if (!string.IsNullOrWhiteSpace(advisorContact_textBox.Text) && (advisorContact_textBox.Text.Length != 11 || !advisorContact_textBox.Text.All(char.IsDigit)))
            {
                MessageBox.Show("Contact number should be 11 digits.");
                return;
            }

            // Validate email (should contain "@gmail.com")
            if (!string.IsNullOrWhiteSpace(advisorEmail_textBox.Text) && !Regex.IsMatch(advisorEmail_textBox.Text, @"@gmail\.com$"))
            {
                MessageBox.Show("Email should end with '@gmail.com'.");
                return;
            }

            // Fetch the ID from the Lookup table based on the selected gender
            int? genderId = null;
            if (MaleButton.Checked || FemaleButton.Checked)
            {
                string genderQuery = "SELECT Id FROM Lookup WHERE Category = 'GENDER' AND Value = @Gender";
                SqlCommand genderCommand = new SqlCommand(genderQuery, connection);
                string gender = MaleButton.Checked ? "Male" : "Female";
                genderCommand.Parameters.AddWithValue("@Gender", gender);
                genderId = Convert.ToInt32(genderCommand.ExecuteScalar());
            }

            // Insert data into the Person table
            string personQuery = "INSERT INTO Person (FirstName, LastName, Contact, Email, Gender, DateOfBirth) " +
                                 "VALUES (@FirstName, @LastName, @Contact, @Email, @GenderId, @DateOfBirth); SELECT SCOPE_IDENTITY();";
            SqlCommand personCommand = new SqlCommand(personQuery, connection);
            personCommand.Parameters.AddWithValue("@FirstName", AdvisorName_textBox.Text);
            personCommand.Parameters.AddWithValue("@LastName", string.IsNullOrWhiteSpace(AdvisorLastname_textBox.Text) ? DBNull.Value : (object)AdvisorLastname_textBox.Text);
            personCommand.Parameters.AddWithValue("@Contact", string.IsNullOrWhiteSpace(advisorContact_textBox.Text) ? DBNull.Value : (object)advisorContact_textBox.Text);
            personCommand.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(advisorEmail_textBox.Text) ? DBNull.Value : (object)advisorEmail_textBox.Text);
            personCommand.Parameters.AddWithValue("@GenderId", genderId.HasValue ? (object)genderId.Value : DBNull.Value); // Use the retrieved gender ID or null
            personCommand.Parameters.AddWithValue("@DateOfBirth", advisor_dateTimePicker.Checked ? (object)advisor_dateTimePicker.Value : DBNull.Value);

            // Execute the command and retrieve the generated ID
            int personId = Convert.ToInt32(personCommand.ExecuteScalar());

            // Fetch the designation ID based on the selected designation
            int? designationId = null;
            if (designation_comboBox.SelectedItem != null)
            {
                string designationQuery = "SELECT Id FROM Lookup WHERE Category = 'DESIGNATION' AND Value = @Designation";
                SqlCommand designationCommand = new SqlCommand(designationQuery, connection);
                designationCommand.Parameters.AddWithValue("@Designation", designation_comboBox.SelectedItem.ToString());
                designationId = Convert.ToInt32(designationCommand.ExecuteScalar());
            }

            // Insert data into the Advisor table
            string advisorInsertQuery = "INSERT INTO Advisor (Id, Designation, Salary) VALUES (@Id, @Designation, @Salary)";
            SqlCommand advisorInsertCommand = new SqlCommand(advisorInsertQuery, connection);
            advisorInsertCommand.Parameters.AddWithValue("@Id", personId); // Use the generated Person Id
            advisorInsertCommand.Parameters.AddWithValue("@Designation", designationId.HasValue ? (object)designationId.Value : DBNull.Value);

            // Check if Salary is not empty and valid
            if (!string.IsNullOrWhiteSpace(Salary_textBox.Text))
            {
                if (decimal.TryParse(Salary_textBox.Text, out decimal salary))
                {
                    advisorInsertCommand.Parameters.AddWithValue("@Salary", salary);
                }
                else
                {
                    MessageBox.Show("Please enter a valid salary.");
                    return;
                }
            }
            else
            {
                advisorInsertCommand.Parameters.AddWithValue("@Salary", DBNull.Value);
            }

            advisorInsertCommand.ExecuteNonQuery();

            MessageBox.Show("Successfully added");

            // Reload data
            Advisor_Load(sender, e);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            var connection = Configuration.getInstance().getConnection();

            // Get the selected row index
            int selectedRowIndex = AdvisorList.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = AdvisorList.Rows[selectedRowIndex];
            DataRowView selectedDataRow = (DataRowView)selectedRow.DataBoundItem;
            int personId = Convert.ToInt32(selectedDataRow["Id"]);

            // Fetch the designation ID based on the selected designation from the ComboBox
            int newDesignationId = -1;
            if (designation_comboBox.SelectedItem != null)
            {
                string designationQuery = "SELECT Id FROM Lookup WHERE Category = 'DESIGNATION' AND Value = @Designation";
                SqlCommand designationCommand = new SqlCommand(designationQuery, connection);
                designationCommand.Parameters.AddWithValue("@Designation", designation_comboBox.SelectedItem.ToString());
                newDesignationId = Convert.ToInt32(designationCommand.ExecuteScalar());
            }

            // Fetch the gender ID based on the selected gender
            int? genderId = null;
            if (MaleButton.Checked || FemaleButton.Checked)
            {
                string genderQuery = "SELECT Id FROM Lookup WHERE Category = 'GENDER' AND Value = @Gender";
                SqlCommand genderCommand = new SqlCommand(genderQuery, connection);
                string gender = MaleButton.Checked ? "Male" : "Female";
                genderCommand.Parameters.AddWithValue("@Gender", gender);
                genderId = Convert.ToInt32(genderCommand.ExecuteScalar());
            }

            // Update the DataRowView with the changes from textboxes, radio buttons, and datetime picker
            selectedDataRow["FirstName"] = AdvisorName_textBox.Text;
            selectedDataRow["LastName"] = AdvisorLastname_textBox.Text;
            selectedDataRow["Contact"] = advisorContact_textBox.Text;
            selectedDataRow["Email"] = advisorEmail_textBox.Text;
            selectedDataRow["Gender"] = MaleButton.Checked ? "Male" : "Female";
            selectedDataRow["DateOfBirth"] = advisor_dateTimePicker.Value;
            selectedDataRow["Designation"] = designation_comboBox.SelectedItem.ToString();
            selectedDataRow["Salary"] = Salary_textBox.Text;

            // Construct the SQL UPDATE command for updating personal information
            string updatePersonalInfoQuery = "UPDATE Person SET FirstName = @FirstName, LastName = @LastName, " +
                                              "Contact = @Contact, Email = @Email, Gender = @GenderId, DateOfBirth = @DateOfBirth " +
                                              "WHERE Id = @PersonId";

            SqlCommand updatePersonalInfoCommand = new SqlCommand(updatePersonalInfoQuery, connection);
            updatePersonalInfoCommand.Parameters.AddWithValue("@FirstName", AdvisorName_textBox.Text);
            updatePersonalInfoCommand.Parameters.AddWithValue("@LastName", AdvisorLastname_textBox.Text);
            updatePersonalInfoCommand.Parameters.AddWithValue("@Contact", advisorContact_textBox.Text);
            updatePersonalInfoCommand.Parameters.AddWithValue("@Email", advisorEmail_textBox.Text);
            updatePersonalInfoCommand.Parameters.AddWithValue("@GenderId", genderId.HasValue ? (object)genderId.Value : DBNull.Value);
            updatePersonalInfoCommand.Parameters.AddWithValue("@DateOfBirth", advisor_dateTimePicker.Value);
            updatePersonalInfoCommand.Parameters.AddWithValue("@PersonId", personId);

            // Execute the UPDATE command for updating personal information
            updatePersonalInfoCommand.ExecuteNonQuery();

            // Construct the SQL UPDATE command for updating advisor's designation and salary
            string updateAdvisorQuery = "UPDATE Advisor SET Designation = @Designation, Salary = @Salary WHERE Id = @PersonId";
            SqlCommand updateAdvisorCommand = new SqlCommand(updateAdvisorQuery, connection);
            updateAdvisorCommand.Parameters.AddWithValue("@Designation", newDesignationId);
            updateAdvisorCommand.Parameters.AddWithValue("@Salary", Salary_textBox.Text);
            updateAdvisorCommand.Parameters.AddWithValue("@PersonId", personId);

            // Execute the UPDATE command for updating advisor's designation and salary
            updateAdvisorCommand.ExecuteNonQuery();

            MessageBox.Show("Successfully updated");

            // Refresh the DataGridView
            Advisor_Load(sender, e);

            connection.Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

            var connection = Configuration.getInstance().getConnection();

            if (AdvisorList.SelectedRows.Count > 0)
            {
                int selectedRowIndex = AdvisorList.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = AdvisorList.Rows[selectedRowIndex];

                // Assuming the DataTable is bound directly to the DataGridView
                DataRowView selectedDataRow = (DataRowView)selectedRow.DataBoundItem;

                // Assuming the DataTable has a column named "Id" for the primary key
                int personId = Convert.ToInt32(selectedDataRow["Id"]);

                // Instead of deleting, update the first name by appending #
                string updateFirstNameQuery = "UPDATE Person SET FirstName = CONCAT(FirstName, '#') WHERE Id = @PersonId";
                SqlCommand updateFirstNameCommand = new SqlCommand(updateFirstNameQuery, connection);
                updateFirstNameCommand.Parameters.AddWithValue("@PersonId", personId);
                updateFirstNameCommand.ExecuteNonQuery();

                MessageBox.Show("Successfully Delete!");

                // Reload data
                Advisor_Load(sender, e);

                connection.Close();
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }

        }

        private void Advisor_Load(object sender, EventArgs e)
        {

            var connection = Configuration.getInstance().getConnection();

            string query = "SELECT d.Value AS Designation, a.Salary, " +
               "p.FirstName, p.LastName, p.Contact, l.Value AS Gender, p.Email, p.DateOfBirth, p.Id " +
               "FROM Advisor a " +
               "JOIN Person p ON p.Id = a.Id " +
               "JOIN Lookup l ON p.Gender = l.Id " +
               "JOIN Lookup d ON a.Designation = d.Id " +
               "WHERE p.FirstName NOT LIKE '%#%'"; // Filter out records with "#" in first name

            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            // Bind the DataTable to the DataGridView
            AdvisorList.DataSource = dt;

            AddEditButtonColumn();
        }

        private void AddEditButtonColumn()
        {
            // Remove existing edit button column if it exists
            foreach (DataGridViewColumn column in AdvisorList.Columns)
            {
                if (column.HeaderText == "Edit" && column is DataGridViewButtonColumn)
                {
                    AdvisorList.Columns.Remove(column);
                    break;
                }
            }

            // Add a new edit button column
            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
            editButtonColumn.HeaderText = "Edit";
            editButtonColumn.Name = "EditButtonColumn";
            editButtonColumn.Text = "Edit";
            editButtonColumn.UseColumnTextForButtonValue = true;
            AdvisorList.Columns.Insert(0, editButtonColumn); // Insert at the beginning
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.ShowDialog();
        }

        private void AdvisorList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == AdvisorList.Columns["EditButtonColumn"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = AdvisorList.Rows[e.RowIndex];
                AdvisorName_textBox.Text = selectedRow.Cells["FirstName"].Value.ToString();
                AdvisorLastname_textBox.Text = selectedRow.Cells["LastName"].Value.ToString();
                advisorContact_textBox.Text = selectedRow.Cells["Contact"].Value.ToString();
                advisorEmail_textBox.Text = selectedRow.Cells["Email"].Value.ToString();

                // Assuming you have RadioButton for gender
                if (selectedRow.Cells["Gender"].Value.ToString() == "Male")
                    MaleButton.Checked = true;
                else if (selectedRow.Cells["Gender"].Value.ToString() == "Female")
                    FemaleButton.Checked = true;

                // Assuming you have a DateTimePicker for date of birth
                advisor_dateTimePicker.Value = Convert.ToDateTime(selectedRow.Cells["DateOfBirth"].Value);

                // Assuming you have a ComboBox for designation
                string designation = selectedRow.Cells["Designation"].Value.ToString();
                designation_comboBox.SelectedItem = designation;

                // Assuming you have a TextBox for salary
                Salary_textBox.Text = selectedRow.Cells["Salary"].Value.ToString();
            }
        }
    }
}
