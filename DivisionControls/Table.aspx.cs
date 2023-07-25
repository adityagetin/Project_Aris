using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Aris
{
    public partial class Table : System.Web.UI.Page
    {
        string connectionString = "Data Source=ADITYA-PAL\\SQLEXPRESS;Initial Catalog=Project_Aris;Integrated Security=True;";
        int scientistID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProposalData();
            }
        }

        protected void BindProposalData()
        {
            string ID = Session["ID"].ToString();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM [ProjProposal] WHERE SupervisoerID=@SupervisoerID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SupervisoerID", ID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                rptProposals.DataSource = dataTable;
                rptProposals.DataBind();
            }
        }

        protected void rptProposals_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView proposalData = (DataRowView)e.Item.DataItem;
                Label lblPresentStatus = (Label)e.Item.FindControl("lblPresentStatus");
                if (lblPresentStatus == null)
                {
                    return;
                }
                string presentStatus = proposalData["PropPresentStatus"].ToString();
                lblPresentStatus.Text = presentStatus;
            }
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            Button btnUpdateStatus = (Button)sender;
            RepeaterItem repeaterItem = (RepeaterItem)btnUpdateStatus.NamingContainer;
            DropDownList ddlStatus = (DropDownList)repeaterItem.FindControl("ddlStatus");
            int proposalID = Convert.ToInt32(btnUpdateStatus.CommandArgument);
            string newStatus = ddlStatus.SelectedValue;
   
            // Update the PropPresentStatus in the database
            UpdateStatus(proposalID, newStatus);

            // Insert into ProjProposalSubmission table
             

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ScientID FROM ProjProposal WHERE ProposalID = @ProposalID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProposalID", proposalID);

                    connection.Open();
                    object result = command.ExecuteScalar();
                   

                    if (result != null)
                    {
                        scientistID = Convert.ToInt32(result);
                        Session["ScientID"]=scientistID;
                    }  
                }
            }
            catch (Exception ex)
            {
                string error = ex.Data.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", $"alert('{error}');", true);

            }

            int supervisorID = Convert.ToInt32(Session["SupervioserID"]);
            // Method to retrieve the supervisor ID based on scientist ID
            DateTime submissionDate = DateTime.Now;
            string comment = ""+newStatus+"by:"+supervisorID;

            InsertSubmission( proposalID, scientistID, supervisorID, newStatus, submissionDate, comment);




            // Refresh the data
            BindProposalData();

            // Show success message
            string message = $"Proposal ID {proposalID} updated to {newStatus}.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", $"alert('{message}');", true);
        }

        protected void UpdateStatus(int proposalID, string newStatus)
        {   

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE [ProjProposal] SET PropPresentStatus = @PropPresentStatus WHERE ProposalID = @ProposalID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PropPresentStatus", newStatus);
                command.Parameters.AddWithValue("@ProposalID", proposalID);

    

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        protected bool CheckSubmissionExists(int proposalID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM ProjProposalSubmission WHERE SubmissionID = @SubmissionID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SubmissionID", proposalID);

                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());

                return count > 0;
            }
        }
         protected void InsertSubmission(int proposalID, int scientistID, int supervisorID, string status, DateTime submissionDate, string comment)
        {
            bool isSubmissionPresent = CheckSubmissionExists(proposalID);

            if (!isSubmissionPresent)
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ProjProposalSubmission (SubmissionID,ProposalID, ScientID, SupervisorID, SubmissionStatus, SubmissionDate, Comment) VALUES (@Sid, @ProposalID, @ScientID, @SupervisorID, @SubmissionStatus, @SubmissionDate, @Comment)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Sid", proposalID);
                    command.Parameters.AddWithValue("@ProposalID", proposalID);
                    command.Parameters.AddWithValue("@ScientID", scientistID);
                    command.Parameters.AddWithValue("@SupervisorID", supervisorID);
                    command.Parameters.AddWithValue("@SubmissionStatus", "Pending");
                    command.Parameters.AddWithValue("@SubmissionDate", submissionDate);
                    command.Parameters.AddWithValue("@Comment", comment);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

        }

        protected void Meating_Click(object sender, EventArgs e)
        {
            Button meeting = (Button)sender;
            RepeaterItem repeaterItem = (RepeaterItem)meeting.NamingContainer;
            int id = Convert.ToInt32(meeting.CommandArgument);
            Meating(id);
        }

        protected void Meating(int id) {
            Session["Pid"] = id.ToString();
            Response.Redirect("Review.aspx");

        }
    }
}
