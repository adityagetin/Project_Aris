using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Aris
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                name.Text = Session["Name"].ToString();
                LoadReviewMeetingsData();

            }

        }

        private void LoadReviewMeetingsData()
        {
            string connectionString = "Data Source=ADITYA-PAL\\SQLEXPRESS;Initial Catalog=Project_Aris;Integrated Security=True;";

            string query = "SELECT [ProposalID],[ReviewMeetingDt] ,[ReviewAction] ,[ReviewComent],[ProceedingAttachment] FROM [Project_Aris].[dbo].[ProReviewComm]";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    rptReviewMeetings.DataSource = dt;
                    rptReviewMeetings.DataBind();
                }
            }
        }

        protected void lnkViewAttachment_Click(object sender, EventArgs e)
        {
            LinkButton lnkViewAttachment = (LinkButton)sender;
            string attachmentId = lnkViewAttachment.CommandArgument;

        }
    }
}