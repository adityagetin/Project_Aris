using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography.X509Certificates;

namespace Project_Aris
{
    public partial class ProjProposal : System.Web.UI.Page
    {
        int SID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            SID= Convert.ToInt32(Session["ID"]);
            if (!IsPostBack)
            {
                BindProposalData();
            }
        }
        protected void BindProposalData()
        {
            
            
            string connectionString = "Data Source=ADITYA-PAL\\SQLEXPRESS;Initial Catalog=Project_Aris;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ProjProposal WHERE ScientID="+SID+"";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ScientID", SID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                rptProposals.DataSource = dataTable;
                rptProposals.DataBind();
            }
        }

        protected void rptProposals_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Handle any specific command events for each proposal if needed
        }

        
    
}
}