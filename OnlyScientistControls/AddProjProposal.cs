using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Aris
{
    public partial class AddProjProposal : System.Web.UI.Page
    {
        string connectionString = "Data Source=ADITYA-PAL\\SQLEXPRESS;Initial Catalog=Project_Aris;Integrated Security=True;";

        int scientID;
        int Supervisor;
        string selectedItems = string.Empty;
        string selectedValues= string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            scientID = Convert.ToInt32(Session["ID"].ToString());
            Supervisor = int.Parse(Session["SupervioserID"].ToString());

            if (!IsPostBack) {
                
                
                int s = Supervisor;
                Console.WriteLine(s);
                BindDropdownItems();
                BindDomain();
                PopulateScientistsDropDown();
                SetDivIDFromDatabase(scientID);
                txtPropSubDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //Scid.Text = scientID;
                //Suid.Text = Supervisor;
            }
                

        }

        private void BindDomain() 
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Domain FROM [Domains]", con))
                {
                    con.Open();
                    txtPropUnderDomain.DataSource = cmd.ExecuteReader();
                    txtPropUnderDomain.DataTextField = "Domain";
                    txtPropUnderDomain.DataValueField = "Domain";
                    txtPropUnderDomain.DataBind();
                    con.Close();
                }
            }
        }

        private void BindDropdownItems()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT CONCAT(FName, ' ', ScientID) AS ScientistName, ScientID FROM [Scientist] ";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ddlItems.DataSource = reader;
                        ddlItems.DataTextField = "ScientistName";
                        ddlItems.DataValueField = "ScientID";
                        ddlItems.DataBind();
                    }
                }
            }
        }

        protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            selectedValues = hiddenSelectedValues.Value;
           
        }


        private void PopulateScientistsDropDown()
        {
            // Your query to fetch the scientist names and IDs from the database
            string query = "SELECT CONCAT(FName, ' ', ScientID) AS ScientistName, ScientID FROM [Scientist] ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string scientistName = reader["ScientistName"].ToString();
                            string scientistID = reader["ScientID"].ToString();
                            ListItem item = new ListItem(scientistName, scientistID);
                            ddlScientists.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void SetDivIDFromDatabase(int scientistID)
        {
            string query = "SELECT DivID FROM Scientist WHERE ScientID = @ScientistID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ScientistID", scientistID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int divID = Convert.ToInt32(reader["DivID"]);
                            txtDivID.Text = divID.ToString();
                        }
                        else
                        {
                            // Handle the case when the scientist with the given ID is not found in the database.
                            txtDivID.Text = "Scientist not found in the database";
                        }
                    }
                }
            }
        }



        protected void btnAddProposal_Click(object sender, EventArgs e)
        {
            // Get the values from the form fields
            int scientistID = scientID ;
            int divisionID = Convert.ToInt32(txtDivID.Text);
            string domain = txtPropUnderDomain.Text;
            string propType = txtPropType.Text;
            string propTitle = txtPropTitle.Text;
            string propSubAgency = txtPropSubAgency.Text;
            string propSubDate = Convert.ToString(txtPropSubDate.Text);
            string propNature = txtPropNature.Text;
            string propSummary = txtPropSummary.Text;
            decimal propFundEstimate = Convert.ToDecimal(txtPropFundEstimate.Text);
            string propPresentStatus = txtPropPresentStatus.Text;
            string propPI = ddlScientists.Text;
            Console.WriteLine(ddlScientists.Text);

            string propCoPIs = selectedValues;
            string propAttachment = string.Empty;

            if (fileAttachment.HasFile)
            {
                propAttachment = SaveAttachment(fileAttachment);
            }

            int suervioser = Supervisor;

            // Insert the data into the ProjProposal table
            InsertProposalData( scientistID, divisionID, domain, propType, propTitle, propSubAgency, propSubDate, propNature, propSummary, propFundEstimate, propPresentStatus, propPI, propCoPIs, propAttachment,suervioser);

            // Clear the form fields
            ClearFormFields();

            // Show success message
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "alert('Proposal added successfully.');", true);
        }

        private string SaveAttachment(FileUpload fileUpload)
        {
            string attachmentFileName = Path.GetFileName(fileUpload.FileName);
            string attachmentFilePath = Server.MapPath(attachmentFileName);

            //fileUpload.SaveAs(attachmentFilePath);

            return attachmentFileName;
        }

        private void InsertProposalData(int scientistID, int divisionID, string domain, string propType, string propTitle, string propSubAgency, string propSubDate, string propNature, string propSummary, decimal propFundEstimate, string propPresentStatus, string propPI, string propCoPIs, string propAttachment,int Supervisor)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO ProjProposal (ScientID, DivID, PropUnderDomain, PropType, PropTitle, " +
                    "PropSubAgency, PropSubDate, PropNature, PropSummary, PropFundEstimate, PropPresentStatus, PropPI," +
                    " PropCoPIs, PropAttachment,SupervisoerID) VALUES (@ScientID, @DivID, @PropUnderDomain, @PropType, " +
                    "@PropTitle, @PropSubAgency, @PropSubDate, @PropNature, @PropSummary, @PropFundEstimate, @PropPresentStatus," +
                    " @PropPI, @PropCoPIs, @PropAttachment,@SupervisoerID)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ScientID", scientistID);
                command.Parameters.AddWithValue("@DivID", divisionID);
                command.Parameters.AddWithValue("@PropUnderDomain", domain);
                command.Parameters.AddWithValue("@PropType", propType);
                command.Parameters.AddWithValue("@PropTitle", propTitle);
                command.Parameters.AddWithValue("@PropSubAgency", propSubAgency);
                command.Parameters.AddWithValue("@PropSubDate", propSubDate);
                command.Parameters.AddWithValue("@PropNature", propNature);
                command.Parameters.AddWithValue("@PropSummary", propSummary);
                command.Parameters.AddWithValue("@PropFundEstimate", propFundEstimate);
                command.Parameters.AddWithValue("@PropPresentStatus", propPresentStatus);
                command.Parameters.AddWithValue("@PropPI", propPI);
                command.Parameters.AddWithValue("@PropCoPIs", propCoPIs);
                command.Parameters.AddWithValue("@PropAttachment", propAttachment);
                command.Parameters.AddWithValue("@SupervisoerID", Supervisor);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void ClearFormFields()
        {

            txtPropTitle.Text=null;
            txtPropSubAgency.Text = null;
            txtPropSummary.Text = null;
            txtPropFundEstimate.Text = null;
            txtPropPresentStatus.Text = null;
         
        }
    }
}