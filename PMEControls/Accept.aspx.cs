using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Aris
{
    public partial class Accept : System.Web.UI.Page
    {

        protected int ProposalID = 0;
        protected string propTitle = string.Empty;
        protected string propAttachment = null;

        string connectionString = "Data Source=ADITYA-PAL\\SQLEXPRESS;Initial Catalog=Project_Aris;Integrated Security=True;";
        protected void Page_Load(object sender, EventArgs e)
        {   

            string Submission = Request.QueryString["Sid"].ToString();
            Console.WriteLine(Submission);
            GetPropoalID(Submission);
            GetData(ProposalID);
            txtProposalID.Text = ProposalID.ToString();
            txtProjTitle.Text = propTitle;


        }

        public void GetData(int ProposalID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create the SQL command with parameters
                string sql = "SELECT PropTitle, PropAttachment FROM ProjProposal WHERE ProposalID = @ProposalID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProposalID", ProposalID);

                // Execute the query and read the results
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // Retrieve the values from the reader
                    propTitle = reader.GetString(0);
                    propAttachment = reader.GetString(1);
                    
                }

                reader.Close();
            }
        }




        public void GetPropoalID(string SubmissionID)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ProposalID FROM ProjProposalSubmission WHERE SubmissionID = @SubmissionID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SubmissionID", SubmissionID);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        ProposalID = Convert.ToInt32(result);
                    }

                }
            }
            catch (Exception ex)
            {
                string error = ex.Data.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", $"alert('{error}');", true);

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Retrieve form field values
            int proposalID = Convert.ToInt32(txtProposalID.Text);
            string projTitle = txtProjTitle.Text;
            string approvedTerm = txtApprovedTerm.Text;
            DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime completionDate = Convert.ToDateTime(txtCompletionDate.Text);
            decimal approvedBudget = Convert.ToDecimal(txtApprovedBudget.Text);

            // Read the file data for Approval Letter
            HttpPostedFile approvalLetterFile = fileApprovalLetter.PostedFile;
            byte[] approvalLetterData = null;
            if (approvalLetterFile != null && approvalLetterFile.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(approvalLetterFile.InputStream))
                {
                    approvalLetterData = binaryReader.ReadBytes(approvalLetterFile.ContentLength);
                }
            }

            // Read the file data for Approved Proposal Attachment
            string approvedProposalAttchFile = propAttachment;
            



            // Create connection and command objects
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Define SQL query with parameterized values
                string query = @"INSERT INTO ApprovedProject (ProposalID, ProjTitle, ApprovedTerm, StartDate, CompletionDate, ApprovedBudget, ApprovalLetter, ApprovedProposalAttch)
                         VALUES (@ProposalID, @ProjTitle, @ApprovedTerm, @StartDate, @CompletionDate, @ApprovedBudget, @ApprovalLetter, @ApprovedProposalAttch)";

                // Create command and parameters
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProposalID", proposalID);
                    command.Parameters.AddWithValue("@ProjTitle", projTitle);
                    command.Parameters.AddWithValue("@ApprovedTerm", approvedTerm);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@CompletionDate", completionDate);
                    command.Parameters.AddWithValue("@ApprovedBudget", approvedBudget);
                    command.Parameters.AddWithValue("@ApprovalLetter", approvalLetterData);
                    command.Parameters.AddWithValue("@ApprovedProposalAttch", approvedProposalAttchFile);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }

        }
    }
}