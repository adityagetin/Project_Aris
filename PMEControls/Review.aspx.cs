using System;
using System.IO;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.Services.Description;

namespace Project_Aris
{
    public partial class Review : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
            ProposalID.Text = Session["Pid"].ToString();
        }

        protected void ScheduleReview_Click(object sender, EventArgs e)
        {
            
            string pid = ProposalID.Text;
            string date = Mdate.Text;
            string Action = ReviewAction.Text;
            string Comm = Comment.Text;
            string fileName = pid + ".pdf";
            byte[] fileData;

            if (fileUpload.HasFile)
            {
                try
                {
                    using (Stream fileStream = fileUpload.PostedFile.InputStream)
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            fileStream.CopyTo(memoryStream);
                            fileData = memoryStream.ToArray();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return;
                }
            }
            else
            {
                string myString = "file not uploaded";
                fileData = new byte[myString.Length];

                // Copy each character of the string to the byte array
                for (int i = 0; i < myString.Length; i++)
                {
                    fileData[i] = (byte)myString[i];
                }
            }

            string connectionString = "Data Source=ADITYA-PAL\\SQLEXPRESS;Initial Catalog=Project_Aris;Integrated Security=True;";
            string insertQuery = "INSERT INTO PropReviewComm (ProposalID, ReviewMeetingDt, ReviewAction, ReviewComent, FileName, ProceedingAttchment) " +
                                 "VALUES (@ProposalID, @ReviewMeetingDt, @ReviewAction, @ReviewComent, @FileName, @ProceedingAttachment)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProposalID", pid);
                    command.Parameters.AddWithValue("@ReviewMeetingDt", date);
                    command.Parameters.AddWithValue("@ReviewAction", Action);
                    command.Parameters.AddWithValue("@ReviewComent", Comm);
                    command.Parameters.AddWithValue("@FileName", fileName);
                    command.Parameters.AddWithValue("@ProceedingAttachment", fileData);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        // Do something after successful insertion

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Review Meeting is Scheduled on"+date+"');", true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Error:"+ex.Message.ToString()+"');", true);
                    }
                }
            }

        }
    }
}
