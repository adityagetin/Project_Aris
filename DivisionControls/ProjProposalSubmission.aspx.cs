using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace Project_Aris.DivisionControls
{
    public partial class ProjProposalSubmission : System.Web.UI.Page
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
                string query = "SELECT * FROM ProjProposal  WHERE ScientID = @Sid";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Sid", ID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                rptProposals.DataSource = dataTable;
                rptProposals.DataBind();
            }
        }

        protected void rptSubmissions_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpdateSubmission")
            {
                string[] values = e.CommandArgument.ToString().Split(',');
                DropDownList ddlStatus = (DropDownList)e.Item.FindControl("DropDownList1");
                string newStatus = ddlStatus.SelectedValue;
                string proposalID = values[0];
                string scientID = values[1];
                string supervisorID = values[2];

                if (newStatus == "Approved") { 
                    InsertSubmission(proposalID, scientID,supervisorID);
                }

                UpdateStatus(proposalID, newStatus);
                BindProposalData();
            }
        }

        protected void UpdateStatus(string proposalID, string newStatus)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE ProjProposal SET SubmissionStatus = @SubmissionStatus WHERE [ProposalID] = @ID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SubmissionStatus", newStatus);
                    command.Parameters.AddWithValue("@ID", proposalID);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Data.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", $"alert('{error}');", true);
                Thread.Sleep(10000);
                Response.Redirect("Default.aspx");
            }
        }

        protected void InsertSubmission(string proposalID, string scientID, string supervioserID) { 

            string status = "Approved by:"+supervioserID+"";
            string date = DateTime.Now.ToString();
            using(SqlConnection connection = new SqlConnection(connectionString)) 
            {
                string query = "INSERT INTO [ProjProposalSubmission] ([ProposalID],[ScientID],[SupervisorID],[SubmissionStatus],[SubmissionDate],[Comment]) VALUES (" + proposalID + "," + scientID + "," + supervioserID + "," + status + "," + date + "," + status + ") ";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
            }

        }

    }
}