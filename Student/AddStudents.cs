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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace ProjectA_2022_CS_45_.Student
{
    public partial class AddStudents : Form
    {
        public AddStudents()
        {
            InitializeComponent();
        }

        private void AddStudents_Load(object sender, EventArgs e)
        {

            var connection = Configuration.getInstance().getConnection();
            string query = "SELECT s.RegistrationNo, p.FirstName, p.LastName, p.Contact, p.Email, l.Value AS Gender, p.DateOfBirth, s.Id " +
                           "FROM Student s  " +
                           "JOIN Person p ON p.Id = s.Id " +
                           "JOIN Lookup l ON p.Gender = l.Id " +
                           "WHERE p.FirstName NOT LIKE '%#%'"; // Filter out records with "#" in first name

            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            // Bind the DataTable to the DataGridView
            PersonList.DataSource = dt;

            AddEditButtonColumn();
        }

        private void AddEditButtonColumn()
        {
            // Remove existing edit button column if it exists
            foreach (DataGridViewColumn column in PersonList.Columns)
            {
                if (column.HeaderText == "Edit" && column is DataGridViewButtonColumn)
                {
                    PersonList.Columns.Remove(column);
                    break;
                }
            }

            // Add a new edit button column
            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
            editButtonColumn.HeaderText = "Edit";
            editButtonColumn.Name = "EditButtonColumn";
            editButtonColumn.Text = "Edit";
            editButtonColumn.UseColumnTextForButtonValue = true;
            PersonList.Columns.Insert(0, editButtonColumn); // Insert at the beginning
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var connection = Configuration.getInstance().getConnection();

            // Check if required fields are empty
            if (string.IsNullOrWhiteSpace(textBox2.Text) || // First name
                string.IsNullOrWhiteSpace(RegirationNo.Text) || // Registration number
                string.IsNullOrWhiteSpace(textBox4.Text) || // Contact number
                string.IsNullOrWhiteSpace(textBox5.Text) || // Email
                !Regex.IsMatch(textBox5.Text, @"@gmail\.com$")) // Validate email format
            {
                MessageBox.Show("Please fill all the required fields.");
                return;
            }

            // Validate contact number (11 digits)
            if (textBox4.Text.Length != 11 || !textBox4.Text.All(char.IsDigit))
            {
                MessageBox.Show("Contact number should be 11 digits.");
                return;
            }

            // Validate registration number format
            if (!Regex.IsMatch(RegirationNo.Text, @"^2022-CS-\d+$"))
            {
                MessageBox.Show("Registration number should be in the format '2022-CS-X' where X can be any integer.");
                return;
            }

            // Fetch the ID from the Lookup table based on the selected gender
            string genderQuery = "SELECT Id FROM Lookup WHERE Category = 'GENDER' AND Value = @Gender";
            SqlCommand genderCommand = new SqlCommand(genderQuery, connection);
            string gender = MaleButton.Checked ? "Male" : "Female";
            genderCommand.Parameters.AddWithValue("@Gender", gender);
            int genderId = Convert.ToInt32(genderCommand.ExecuteScalar());

            // Insert data into the Person table
            string personQuery = "INSERT INTO Person (FirstName, LastName, Contact, Email, Gender, DateOfBirth) VALUES (@FirstName, @LastName, @Contact, @Email, @GenderId, @DateOfBirth); SELECT SCOPE_IDENTITY();";
            SqlCommand personCommand = new SqlCommand(personQuery, connection);
            personCommand.Parameters.AddWithValue("@FirstName", textBox2.Text);
            personCommand.Parameters.AddWithValue("@LastName", string.IsNullOrWhiteSpace(textBox3.Text) ? DBNull.Value : (object)textBox3.Text);
            personCommand.Parameters.AddWithValue("@Contact", textBox4.Text);
            personCommand.Parameters.AddWithValue("@Email", textBox5.Text);
            personCommand.Parameters.AddWithValue("@GenderId", genderId); // Use the retrieved gender ID
            personCommand.Parameters.AddWithValue("@DateOfBirth", DBNull.Value); // Set DateOfBirth to null initially

            // Check if DateOfBirth is not null
            if (dateTimePicker1.Checked)
            {
                personCommand.Parameters["@DateOfBirth"].Value = dateTimePicker1.Value;
            }

            // Execute the command and retrieve the generated ID
            int personId = Convert.ToInt32(personCommand.ExecuteScalar());

            // Insert data into the Student table
            string studentQuery = "INSERT INTO Student (Id, RegistrationNo) VALUES (@Id, @RegistrationNo)";
            SqlCommand studentCommand = new SqlCommand(studentQuery, connection);
            studentCommand.Parameters.AddWithValue("@Id", personId); // Use the generated ID from Person table
            studentCommand.Parameters.AddWithValue("@RegistrationNo", RegirationNo.Text);
            studentCommand.ExecuteNonQuery();

            MessageBox.Show("Successfully saved");

            // Clear textboxes
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            // Reload the data in the DataGridView
            AddStudents_Load(sender, e); // Call the Student_Form_Load method to reload data

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {

            var connection = Configuration.getInstance().getConnection();

            // Check if any row is selected in the DataGridView
            if (PersonList.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = PersonList.SelectedRows[0];
                int personId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                // Fetch the ID from the Lookup table based on the selected gender
                string genderQuery = "SELECT Id FROM Lookup WHERE Category = 'GENDER' AND Value = @Gender";
                SqlCommand genderCommand = new SqlCommand(genderQuery, connection);
                string gender = MaleButton.Checked ? "Male" : "Female";
                genderCommand.Parameters.AddWithValue("@Gender", gender);
                int genderId = Convert.ToInt32(genderCommand.ExecuteScalar());

                // Construct the SQL UPDATE command for updating other details
                string updateQuery = "UPDATE Person SET FirstName = @FirstName, LastName = @LastName, " +
                                     "Contact = @Contact, Email = @Email, Gender = @GenderId, DateOfBirth = @DateOfBirth " +
                                     "WHERE Id = @PersonId";

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@FirstName", textBox2.Text);
                updateCommand.Parameters.AddWithValue("@LastName", string.IsNullOrWhiteSpace(textBox3.Text) ? DBNull.Value : (object)textBox3.Text);
                updateCommand.Parameters.AddWithValue("@Contact", textBox4.Text);
                updateCommand.Parameters.AddWithValue("@Email", textBox5.Text);
                updateCommand.Parameters.AddWithValue("@GenderId", genderId);
                updateCommand.Parameters.AddWithValue("@DateOfBirth", dateTimePicker1.Checked ? (object)dateTimePicker1.Value : DBNull.Value);
                updateCommand.Parameters.AddWithValue("@PersonId", personId);

                // Execute the UPDATE command
                updateCommand.ExecuteNonQuery();

                MessageBox.Show("Successfully updated");

                // Refresh the DataGridView after updating the database
                AddStudents_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }


        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var connection = Configuration.getInstance().getConnection();

            if (PersonList.SelectedRows.Count > 0)
            {
                int selectedRowIndex = PersonList.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = PersonList.Rows[selectedRowIndex];

                // Assuming the DataTable is bound directly to the DataGridView
                DataRowView selectedDataRow = (DataRowView)selectedRow.DataBoundItem;

                // Assuming the DataTable has a column named "Id" for the primary key
                int personId = Convert.ToInt32(selectedDataRow["Id"]);

                // Instead of deleting, update the first name by appending #
                string updateFirstNameQuery = "UPDATE Person SET FirstName = CONCAT(FirstName, '#') WHERE Id = @PersonId";
                SqlCommand updateFirstNameCommand = new SqlCommand(updateFirstNameQuery, connection);
                updateFirstNameCommand.Parameters.AddWithValue("@PersonId", personId);
                updateFirstNameCommand.ExecuteNonQuery();

                MessageBox.Show("Successfully updated");

                // Refresh the DataGridView after updating the first name
                AddStudents_Load(sender, e);

                connection.Close();
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.ShowDialog();
        }

        private void PersonList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == PersonList.Columns["EditButtonColumn"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = PersonList.Rows[e.RowIndex];
                textBox2.Text = selectedRow.Cells["FirstName"].Value.ToString();
                textBox3.Text = selectedRow.Cells["LastName"].Value.ToString();
                textBox4.Text = selectedRow.Cells["Contact"].Value.ToString();
                textBox5.Text = selectedRow.Cells["Email"].Value.ToString();

                // Assuming you have RadioButton for gender
                if (selectedRow.Cells["Gender"].Value.ToString() == "Male")
                    MaleButton.Checked = true;
                else if (selectedRow.Cells["Gender"].Value.ToString() == "Female")
                    FemaleButton.Checked = true;

                // Assuming you have a DateTimePicker for date of birth
                dateTimePicker1.Value = Convert.ToDateTime(selectedRow.Cells["DateOfBirth"].Value);
            }
        }
    }
}
