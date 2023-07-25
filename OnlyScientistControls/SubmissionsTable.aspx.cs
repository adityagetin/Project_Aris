using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Project_Aris
{
    public partial class SubmissionsTable : System.Web.UI.Page
    {
        string connectionString = "Data Source=ADITYA-PAL\\SQLEXPRESS;Initial Catalog=Project_Aris;Integrated Security=True;";
        protected void Page_Load(object sender, EventArgs e)
        {   
            if (!IsPostBack)
            {
                BindSubmissionData();
            }
        }

        protected void BindSubmissionData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string SId= Session["ID"].ToString();
                string query = "SELECT * FROM ProjProposalSubmission WHERE [SupervisorID]="+SId+"";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                rptSubmissions.DataSource = dataTable;
                rptSubmissions.DataBind();
            }
        }

        protected void rptSubmissions_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpdateSubmission")
            {
                string[] values = e.CommandArgument.ToString().Split(',');
                DropDownList ddlStatus = (DropDownList)e.Item.FindControl("DropDownList1");
                string newStatus = ddlStatus.SelectedValue;
                string submissionID = values[0];
                string proposalID = values[1];
                string scientID = values[2];
                string supervisorID = values[3];
                string comment = values[6];
                string submissionDate = values[5];


                UpdateSubmissionStatus(submissionID, newStatus);

                InsertIntoProjProposalApprovalProcess(submissionID,supervisorID,newStatus,submissionDate,comment);


                // Refresh the page to reflect the updated status
                BindSubmissionData();
            }
        }

        protected void UpdateSubmissionStatus(string submissionID, string newStatus)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE ProjProposalSubmission SET SubmissionStatus = @SubmissionStatus WHERE SubmissionID = @SubmissionID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SubmissionStatus", newStatus);
                    command.Parameters.AddWithValue("@SubmissionID", submissionID);
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
        protected void InsertIntoProjProposalApprovalProcess(string submissionID, string supervisorID, string newStatus, string submissionDate, string comment) {

            try {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ProjProposalApprovalProcess (SubmissionID, SupervisorID, SubmissionStatus, SubmissionDate, Comment)VALUES (@SubmissionID, @SupervisorID, @SubmissionStatus, @SubmissionDate, @Comment);";
                    SqlCommand command = new SqlCommand(query, connection);
                    int sID=int.Parse(submissionID);
                    command.Parameters.AddWithValue("@SubmissionID",sID);
                    int SuID =int.Parse(supervisorID);
                    command.Parameters.AddWithValue("@SupervisorID", SuID);
                    command.Parameters.AddWithValue("@SubmissionStatus",newStatus);
                    string dateString = submissionDate;
                    DateTime convertedDate;

                    if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out  convertedDate))
                    {
                    }
                    else
                    {   
                        convertedDate = DateTime.Now;
                    }
                    command.Parameters.AddWithValue("@SubmissionDate", convertedDate);
                    command.Parameters.AddWithValue("@Comment", comment);
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
    }
}
