using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectA_2022_CS_45_.Reports
{
    public partial class GenerateReportsForm : Form
    {
        public GenerateReportsForm()
        {
            InitializeComponent();
        }

        private void GenerateReportsForm_Load(object sender, EventArgs e)
        {

        }

        private void GeneratePDFReport(string fileName, string query, Configuration config)
        {
            try
            {
                // Create new PDF document
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
                document.Open();

                // Execute query to fetch data from the database
                SqlConnection con = config.getConnection();
                bool connectionOpenedHere = false;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                    connectionOpenedHere = true;
                }
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                // Create a table to hold the data
                PdfPTable table = new PdfPTable(reader.FieldCount);
                table.WidthPercentage = 100;

                // Add table headers
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(reader.GetName(i)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = new BaseColor(128, 128, 128);
                    table.AddCell(cell);
                }

                // Add data rows
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(reader[i].ToString()));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }

                // Add table to the document
                document.Add(table);

                // Close connections and document
                reader.Close();
                if (connectionOpenedHere)
                    con.Close();
                document.Close();
                writer.Close();

                // Open the generated PDF file
                MessageBox.Show("Report generated successfully!");
                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = @"SELECT 
                              P.Title AS [Project Title], 
                              CONCAT(Per.FirstName, ' ', Per.LastName) AS [Advisor Name], 
                              L.Value AS [Advisor Role]
                          FROM 
                              Project P
                          INNER JOIN 
                              ProjectAdvisor PA ON P.Id = PA.ProjectId
                          INNER JOIN 
                              Person Per ON PA.AdvisorId = Per.Id
                          INNER JOIN 
                              Lookup L ON PA.AdvisorRole = L.Id
                          WHERE 
                              L.Category = 'ADVISOR_ROLE'";

            Configuration config = Configuration.getInstance();
            GeneratePDFReport("ProjectAdvisoryBoardReport.pdf", query, config);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string query = @"SELECT 
                        G.Id AS [Group ID],
                        P.FirstName + ' ' + P.LastName AS [Student Name],
                        S.RegistrationNo AS [Registration No]
                    FROM 
                        [Group] G
                    INNER JOIN 
                        GroupStudent GS ON G.Id = GS.GroupId
                    INNER JOIN 
                        Student S ON GS.StudentId = S.Id
                    INNER JOIN 
                        Person P ON S.Id = P.Id";

            Configuration config = Configuration.getInstance();
            GeneratePDFReport("StudentListReport.pdf", query, config);
        }

        private void MarkSheet_button_Click(object sender, EventArgs e)
        {
            string query = @"SELECT 
                        P.Title AS [Project Title], 
                        CONCAT(PA.FirstName, ' ', PA.LastName) AS [Member Name], 
                        E.Name AS [Evaluation Name],
                        E.TotalMarks AS [Total Marks],
                        GE.ObtainedMarks AS [Obtained Marks]
                    FROM 
                        Project P
                    INNER JOIN 
                        GroupProject GP ON P.Id = GP.ProjectId
                    INNER JOIN 
                        [Group] G ON GP.GroupId = G.Id
                    INNER JOIN 
                        GroupEvaluation GE ON G.Id = GE.GroupId
                    INNER JOIN 
                        Evaluation E ON GE.EvaluationId = E.Id
                    INNER JOIN 
                        GroupStudent GS ON G.Id = GS.GroupId
                    INNER JOIN 
                        Student S ON GS.StudentId = S.Id
                    INNER JOIN 
                        Person PA ON S.Id = PA.Id
                    ORDER BY 
                        P.Title, PA.LastName, E.Name";

            Configuration config = Configuration.getInstance();
            GeneratePDFReport("MarksSheetReport.pdf", query, config);
        }

        private void advisorsinfo_button_Click(object sender, EventArgs e)
        {
            string query = @"SELECT 
                            Adv.Id AS [Advisor ID],
                            CONCAT(Per.FirstName, ' ', Per.LastName) AS [Advisor Name],
                            L.Value AS [Designation],
                            Adv.Salary
                        FROM 
                            Advisor Adv
                        INNER JOIN 
                            Lookup L ON Adv.Designation = L.Id
                        INNER JOIN 
                            Person Per ON Adv.Id = Per.Id";

            Configuration config = Configuration.getInstance();
            GeneratePDFReport("AdvisorInformationReport.pdf", query, config);
        }

        private void evaluationsummary_button_Click(object sender, EventArgs e)
        {
            string query = @"SELECT 
                        P.Title AS [Project Title],
                        E.Name AS [Evaluation Name],
                        AVG(GE.ObtainedMarks) AS [Average Marks]
                    FROM 
                        Project P
                    INNER JOIN 
                        GroupProject GP ON P.Id = GP.ProjectId
                    INNER JOIN 
                        GroupEvaluation GE ON GP.GroupId = GE.GroupId
                    INNER JOIN 
                        Evaluation E ON GE.EvaluationId = E.Id
                    GROUP BY 
                        P.Title, E.Name";

            Configuration config = Configuration.getInstance();
            GeneratePDFReport("ProjectEvaluationSummaryReport.pdf", query, config);
        }

        private void statusreport_button_Click(object sender, EventArgs e)
        {
            string query = @"SELECT 
                            G.Id AS [Group ID],
                            CONCAT_WS(', ', CONCAT(Per.FirstName, ' ', Per.LastName), Stu.RegistrationNo) AS [Student Name],
                            L.Value AS [Status],
                            GS.AssignmentDate
                        FROM 
                            GroupStudent GS
                        INNER JOIN 
                            [Group] G ON GS.GroupId = G.Id
                        INNER JOIN 
                            Student Stu ON GS.StudentId = Stu.Id
                        INNER JOIN 
                            Person Per ON Stu.Id = Per.Id
                        INNER JOIN 
                            Lookup L ON GS.Status = L.Id";

            Configuration config = Configuration.getInstance();
            GeneratePDFReport("GroupStudentStatusReport.pdf", query, config);
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mainmenu = new MainMenu();
            mainmenu.Show();
        }
    }
}
