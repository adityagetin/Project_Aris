using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace Project_Aris.Reports
{
    public partial class AllReport : System.Web.UI.Page
    {
        string connectionString = "Data Source=ADITYA-PAL\\SQLEXPRESS;Initial Catalog=Project_Aris;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateDropDownLists();
            }
        }

        private void PopulateDropDownLists()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                using (SqlCommand cmd = new SqlCommand("SELECT CONCAT(FName, ' ', ScientID) AS ScientistName, ScientID FROM [Scientist]", con))
                {
                    con.Open();
                    ddlScientist.DataSource = cmd.ExecuteReader();
                    ddlScientist.DataTextField = "ScientistName";
                    ddlScientist.DataValueField = "ScientID";
                    ddlScientist.DataBind();
                    con.Close();

                }

                using (SqlCommand cmd = new SqlCommand("SELECT ID, [Division] FROM [DivisionID]", con))
                {
                    con.Open();
                    ddlDivision.DataSource = cmd.ExecuteReader();
                    ddlDivision.DataTextField = "Division";
                    ddlDivision.DataValueField = "ID";
                    ddlDivision.DataBind();
                    con.Close();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT Domain FROM [Domains]", con))
                {
                    con.Open();
                    ddlDomain.DataSource = cmd.ExecuteReader();
                    ddlDomain.DataTextField = "Domain";
                    ddlDomain.DataValueField = "Domain";
                    ddlDomain.DataBind();
                    con.Close();
                }
            }
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            int selectedScientistId = Convert.ToInt32(ddlScientist.SelectedValue);
            int selectedDivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
            string selectedDomain = ddlDomain.SelectedValue;
            
            Data_Cards.Controls.Clear();

            if (selectedScientistId > 0)
            {
                GetScientistReport(selectedScientistId);
            }
            else if (selectedDivisionId > 0)
            {
                GetDivisionReport(selectedDivisionId);
            }
            else if (!string.IsNullOrEmpty(selectedDomain))
            {
                GetDomainReport(selectedDomain);
            }
        }

        private void GetScientistReport(int scientistId)
        {
            string query = "SELECT PP.[ScientID], PP.[DivID], PP.[PropUnderDomain], PP.[PropType], " +
                "PP.[PropTitle], PP.[PropSubAgency], PP.[PropSubDate], PP.[PropNature], PP.[PropSummary], " +
                " PP.[PropPI], PP.[PropCoPIs], PP.[PropAttachment], PP.[SupervisoerID], " +
                "AP.[ProjID], AP.[ApprovedTerm], AP.[StartDate], AP.[CompletionDate], AP.[ApprovedBudget], " +
                "AP.[ApprovalLetter], AP.[ApprovedProposalAttch] " +
                "FROM [ProjProposal] AS PP JOIN [ApprovedProject] AS AP ON PP.[ProposalID] = AP.[ProposalID] " +
                "WHERE PP.[ScientID] = @ScientistId;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ScientistId", scientistId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {   string id= reader["ProjID"].ToString();
                            string scientist = reader["ScientID"].ToString();
                            string Div = reader["DivID"].ToString();
                            string domain = reader["PropUnderDomain"].ToString(); 
                            string propTitle = reader["PropTitle"].ToString();
                            string supervioser = reader["SupervisoerID"].ToString();
                            string propSummary = reader["PropSummary"].ToString();
                            string propType = reader["PropType"].ToString();
                            string propSubAgency = reader["PropSubAgency"].ToString();
                            string propSubDate = reader["PropSubDate"].ToString();
                            string propNature = reader["PropNature"].ToString();
                            string propPI = reader["PropPI"].ToString();
                            string propCoPIs = reader["PropCoPIs"].ToString();
                            string sdate = reader["StartDate"].ToString();
                            string Cdate = reader["CompletionDate"].ToString();
                            string ApprovedBudget = reader["ApprovedBudget"].ToString();
                            string approvedTerm = reader["ApprovedTerm"].ToString();
                            string approvalletter = reader["ApprovalLetter"].ToString();
                            string ApprovedProposalAttch = reader["ApprovedProposalAttch"].ToString() ;



                            // Add other retrieved values here

                            Panel card = new Panel();
                            card.CssClass = "data-card";

                            Label titleLabel = new Label();
                            titleLabel.Text = "Proposal Title: " + propTitle;
                            card.Controls.Add(titleLabel);

                            Label PIDLabel = new Label();
                            PIDLabel.Text = "Proposal ID: " + id;
                            card.Controls.Add(PIDLabel);

                            Label stLabel = new Label();
                            stLabel.Text = "Scientist ID: " + scientist;
                            card.Controls.Add(stLabel);

                            Label DivLabel = new Label();
                            DivLabel.Text = "Division: " + Div;
                            card.Controls.Add(DivLabel);

                            Label summaryLabel = new Label();
                            summaryLabel.Text = "Summary: " + propSummary;
                            card.Controls.Add(summaryLabel);

                            Label typeLabel = new Label();
                            typeLabel.Text = "Type: " + propType;
                            card.Controls.Add(typeLabel);

                            Label DomainLabel = new Label();
                            DomainLabel.Text = "Domain: " + domain;
                            card.Controls.Add(DomainLabel);

                            Label subAgencyLabel = new Label();
                            subAgencyLabel.Text = "Submitting Agency: " + propSubAgency;
                            card.Controls.Add(subAgencyLabel);

                            Label subDateLabel = new Label();
                            subDateLabel.Text = "Submission Date: " + propSubDate;
                            card.Controls.Add(subDateLabel);

                            Label natureLabel = new Label();
                            natureLabel.Text = "Nature: " + propNature;
                            card.Controls.Add(natureLabel);

                            Label piLabel = new Label();
                            piLabel.Text = "Principal Investigator: " + propPI;
                            card.Controls.Add(piLabel);

                            Label coPIsLabel = new Label();
                            coPIsLabel.Text = "Co-Principal Investigators: " + propCoPIs;
                            card.Controls.Add(coPIsLabel);

                            Label SdateLabel = new Label();
                            SdateLabel.Text = "Start Date: " + sdate;
                            card.Controls.Add(SdateLabel);

                            Label CdateLabel = new Label();
                            CdateLabel.Text = "Completion Date: " + Cdate;
                            card.Controls.Add(CdateLabel);

                            Label AtLabel = new Label();
                            AtLabel.Text = "Approved Term: " + approvedTerm;
                            card.Controls.Add(AtLabel);

                            Label AbLabel = new Label();
                            AbLabel.Text = "Approved Budget: " + ApprovedBudget;
                            card.Controls.Add(AbLabel);

                            Label supervioserLabel = new Label();
                            supervioserLabel.Text = "Supervisor: " + supervioser;
                            card.Controls.Add(supervioserLabel);

                            Label AppLlabel = new Label();
                            AppLlabel.Text = "Approval Letter: " + approvalletter;
                            card.Controls.Add(AppLlabel);

                            Label proplabel = new Label();
                            proplabel.Text = "Approved Proposal Attachment: " + ApprovedProposalAttch;
                            card.Controls.Add(proplabel);

                            Data_Cards.Controls.Add(card);

                        }
                    }
                }
            }
        }

        // Logic for division-specific report
        private void GetDivisionReport(int divisionId)
        {
            string query = "SELECT PP.[ScientID], PP.[DivID], PP.[PropUnderDomain], PP.[PropType], " +
                "PP.[PropTitle], PP.[PropSubAgency], PP.[PropSubDate], PP.[PropNature], PP.[PropSummary], " +
                "PP.[PropFundEstimate], PP.[PropPI], PP.[PropCoPIs], PP.[PropAttachment], PP.[SupervisoerID], " +
                "AP.[ProjID], AP.[ApprovedTerm], AP.[StartDate], AP.[CompletionDate], AP.[ApprovedBudget], " +
                "AP.[ApprovalLetter], AP.[ApprovedProposalAttch] " +
                "FROM [ProjProposal] AS PP JOIN [ApprovedProject] AS AP ON PP.[ProposalID] = AP.[ProposalID] " +
                "WHERE PP.[DivID] = @DivisionId;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DivisionId", divisionId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["ProjID"].ToString();
                            string scientist = reader["ScientID"].ToString();
                            string Div = reader["DivID"].ToString();
                            string domain = reader["PropUnderDomain"].ToString();
                            string propTitle = reader["PropTitle"].ToString();
                            string supervioser = reader["SupervisoerID"].ToString();
                            string propSummary = reader["PropSummary"].ToString();
                            string propType = reader["PropType"].ToString();
                            string propSubAgency = reader["PropSubAgency"].ToString();
                            string propSubDate = reader["PropSubDate"].ToString();
                            string propNature = reader["PropNature"].ToString();
                            string propPI = reader["PropPI"].ToString();
                            string propCoPIs = reader["PropCoPIs"].ToString();
                            string sdate = reader["StartDate"].ToString();
                            string Cdate = reader["CompletionDate"].ToString();
                            string ApprovedBudget = reader["ApprovedBudget"].ToString();
                            string approvedTerm = reader["ApprovedTerm"].ToString();
                            string approvalletter = reader["ApprovalLetter"].ToString();
                            string ApprovedProposalAttch = reader["ApprovedProposalAttch"].ToString();



                            // Add other retrieved values here

                            Panel card = new Panel();
                            card.CssClass = "data-card";

                            Label titleLabel = new Label();
                            titleLabel.Text = "Proposal Title: " + propTitle;
                            card.Controls.Add(titleLabel);

                            Label PIDLabel = new Label();
                            PIDLabel.Text = "Proposal ID: " + id;
                            card.Controls.Add(PIDLabel);

                            Label stLabel = new Label();
                            stLabel.Text = "Scientist ID: " + scientist;
                            card.Controls.Add(stLabel);

                            Label DivLabel = new Label();
                            DivLabel.Text = "Division: " + Div;
                            card.Controls.Add(DivLabel);

                            Label summaryLabel = new Label();
                            summaryLabel.Text = "Summary: " + propSummary;
                            card.Controls.Add(summaryLabel);

                            Label typeLabel = new Label();
                            typeLabel.Text = "Type: " + propType;
                            card.Controls.Add(typeLabel);

                            Label DomainLabel = new Label();
                            DomainLabel.Text = "Domain: " + domain;
                            card.Controls.Add(DomainLabel);

                            Label subAgencyLabel = new Label();
                            subAgencyLabel.Text = "Submitting Agency: " + propSubAgency;
                            card.Controls.Add(subAgencyLabel);

                            Label subDateLabel = new Label();
                            subDateLabel.Text = "Submission Date: " + propSubDate;
                            card.Controls.Add(subDateLabel);

                            Label natureLabel = new Label();
                            natureLabel.Text = "Nature: " + propNature;
                            card.Controls.Add(natureLabel);

                            Label piLabel = new Label();
                            piLabel.Text = "Principal Investigator: " + propPI;
                            card.Controls.Add(piLabel);

                            Label coPIsLabel = new Label();
                            coPIsLabel.Text = "Co-Principal Investigators: " + propCoPIs;
                            card.Controls.Add(coPIsLabel);

                            Label SdateLabel = new Label();
                            SdateLabel.Text = "Start Date: " + sdate;
                            card.Controls.Add(SdateLabel);

                            Label CdateLabel = new Label();
                            CdateLabel.Text = "Completion Date: " + Cdate;
                            card.Controls.Add(CdateLabel);

                            Label AtLabel = new Label();
                            AtLabel.Text = "Approved Term: " + approvedTerm;
                            card.Controls.Add(AtLabel);

                            Label AbLabel = new Label();
                            AbLabel.Text = "Approved Budget: " + ApprovedBudget;
                            card.Controls.Add(AbLabel);

                            Label supervioserLabel = new Label();
                            supervioserLabel.Text = "Supervisor: " + supervioser;
                            card.Controls.Add(supervioserLabel);

                            Label AppLlabel = new Label();
                            AppLlabel.Text = "Approval Letter: " + approvalletter;
                            card.Controls.Add(AppLlabel);

                            Label proplabel = new Label();
                            proplabel.Text = "Approved Proposal Attachment: " + ApprovedProposalAttch;
                            card.Controls.Add(proplabel);

                            Data_Cards.Controls.Add(card);

                        }
                    }
                }
            }
        }


        // Logic for domain-specific report
        private void GetDomainReport(string domain)
        {
            string query = "SELECT PP.[ScientID], PP.[DivID], PP.[PropUnderDomain], PP.[PropType], " +
                "PP.[PropTitle], PP.[PropSubAgency], PP.[PropSubDate], PP.[PropNature], PP.[PropSummary], " +
                "PP.[PropFundEstimate], PP.[PropPI], PP.[PropCoPIs], PP.[PropAttachment], PP.[SupervisoerID], " +
                "AP.[ProjID], AP.[ApprovedTerm], AP.[StartDate], AP.[CompletionDate], AP.[ApprovedBudget], " +
                "AP.[ApprovalLetter], AP.[ApprovedProposalAttch] " +
                "FROM [ProjProposal] AS PP JOIN [ApprovedProject] AS AP ON PP.[ProposalID] = AP.[ProposalID] " +
                "WHERE PP.[PropUnderDomain] = @Domain;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Domain", domain);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["ProjID"].ToString();
                            string scientist = reader["ScientID"].ToString();
                            string Div = reader["DivID"].ToString();
                            string domains = reader["PropUnderDomain"].ToString();
                            string propTitle = reader["PropTitle"].ToString();
                            string supervioser = reader["SupervisoerID"].ToString();
                            string propSummary = reader["PropSummary"].ToString();
                            string propType = reader["PropType"].ToString();
                            string propSubAgency = reader["PropSubAgency"].ToString();
                            string propSubDate = reader["PropSubDate"].ToString();
                            string propNature = reader["PropNature"].ToString();
                            string propPI = reader["PropPI"].ToString();
                            string propCoPIs = reader["PropCoPIs"].ToString();
                            string sdate = reader["StartDate"].ToString();
                            string Cdate = reader["CompletionDate"].ToString();
                            string ApprovedBudget = reader["ApprovedBudget"].ToString();
                            string approvedTerm = reader["ApprovedTerm"].ToString();
                            string approvalletter = reader["ApprovalLetter"].ToString();
                            string ApprovedProposalAttch = reader["ApprovedProposalAttch"].ToString();



                            // Add other retrieved values here

                            Panel card = new Panel();
                            card.CssClass = "data-card";

                            Label titleLabel = new Label();
                            titleLabel.Text = "Proposal Title: " + propTitle;
                            card.Controls.Add(titleLabel);

                            Label PIDLabel = new Label();
                            PIDLabel.Text = "Proposal ID: " + id;
                            card.Controls.Add(PIDLabel);

                            Label stLabel = new Label();
                            stLabel.Text = "Scientist ID: " + scientist;
                            card.Controls.Add(stLabel);

                            Label DivLabel = new Label();
                            DivLabel.Text = "Division: " + Div;
                            card.Controls.Add(DivLabel);

                            Label summaryLabel = new Label();
                            summaryLabel.Text = "Summary: " + propSummary;
                            card.Controls.Add(summaryLabel);

                            Label typeLabel = new Label();
                            typeLabel.Text = "Type: " + propType;
                            card.Controls.Add(typeLabel);

                            Label DomainLabel = new Label();
                            DomainLabel.Text = "Domain: " + domains;
                            card.Controls.Add(DomainLabel);

                            Label subAgencyLabel = new Label();
                            subAgencyLabel.Text = "Submitting Agency: " + propSubAgency;
                            card.Controls.Add(subAgencyLabel);

                            Label subDateLabel = new Label();
                            subDateLabel.Text = "Submission Date: " + propSubDate;
                            card.Controls.Add(subDateLabel);

                            Label natureLabel = new Label();
                            natureLabel.Text = "Nature: " + propNature;
                            card.Controls.Add(natureLabel);

                            Label piLabel = new Label();
                            piLabel.Text = "Principal Investigator: " + propPI;
                            card.Controls.Add(piLabel);

                            Label coPIsLabel = new Label();
                            coPIsLabel.Text = "Co-Principal Investigators: " + propCoPIs;
                            card.Controls.Add(coPIsLabel);

                            Label SdateLabel = new Label();
                            SdateLabel.Text = "Start Date: " + sdate;
                            card.Controls.Add(SdateLabel);

                            Label CdateLabel = new Label();
                            CdateLabel.Text = "Completion Date: " + Cdate;
                            card.Controls.Add(CdateLabel);

                            Label AtLabel = new Label();
                            AtLabel.Text = "Approved Term: " + approvedTerm;
                            card.Controls.Add(AtLabel);

                            Label AbLabel = new Label();
                            AbLabel.Text = "Approved Budget: " + ApprovedBudget;
                            card.Controls.Add(AbLabel);

                            Label supervioserLabel = new Label();
                            supervioserLabel.Text = "Supervisor: " + supervioser;
                            card.Controls.Add(supervioserLabel);

                            Label AppLlabel = new Label();
                            AppLlabel.Text = "Approval Letter: " + approvalletter;
                            card.Controls.Add(AppLlabel);

                            Label proplabel = new Label();
                            proplabel.Text = "Approved Proposal Attachment: " + ApprovedProposalAttch;
                            card.Controls.Add(proplabel);

                            Data_Cards.Controls.Add(card);

                        }
                    }
                }
            }
        }
    }
}