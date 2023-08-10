using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

namespace Project_Aris
{
    public partial class Review : System.Web.UI.Page
    {
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
                    ErrorLabel.Text = "Error: File upload failed.";
                    return;
                }
            }
            else
            {
                string myString = "file not uploaded";
                fileData = new byte[myString.Length];

                for (int i = 0; i < myString.Length; i++)
                {
                    fileData[i] = (byte)myString[i];
                }
            }

            string connectionString = "YourConnectionStringHere";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string insertQuery = "INSERT INTO PropReviewComm (ProposalID, ReviewMeetingDt, ReviewAction, ReviewComent, FileName, ProceedingAttchment) " +
                                             "VALUES (@ProposalID, @ReviewMeetingDt, @ReviewAction, @ReviewComent, @FileName, @ProceedingAttachment)";

                        using (SqlCommand command = new SqlCommand(insertQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ProposalID", pid);
                            command.Parameters.AddWithValue("@ReviewMeetingDt", date);
                            command.Parameters.AddWithValue("@ReviewAction", Action);
                            command.Parameters.AddWithValue("@ReviewComent", Comm);
                            command.Parameters.AddWithValue("@FileName", fileName);
                            command.Parameters.AddWithValue("@ProceedingAttachment", fileData);

                            command.ExecuteNonQuery();
                        }

                        string ProjProposalQuery = "UPDATE [ProjProposal] SET [PropPresentStatus] = @Status WHERE [ProposalID] = @ID";
                        using (SqlCommand command2 = new SqlCommand(ProjProposalQuery, connection, transaction))
                        {
                            command2.Parameters.AddWithValue("@Status", "UNDER REVIEW");
                            command2.Parameters.AddWithValue("@ID", pid);
                            command2.ExecuteNonQuery();
                        }

                        string ProjProposalSubmissionQuery = "UPDATE [ProjProposalSubmission] SET [SubmissionStatus] = @Status WHERE [ProposalID] = @ID";
                        using (SqlCommand command3 = new SqlCommand(ProjProposalSubmissionQuery, connection, transaction))
                        {
                            command3.Parameters.AddWithValue("@Status", "UNDER REVIEW");
                            command3.Parameters.AddWithValue("@ID", pid);
                            command3.ExecuteNonQuery();
                        }

                        string GetSubmissonIDQuery = "SELECT [SubmissionID] FROM [ProjProposalSubmission] WHERE [ProposalID] = @ID";
                        string ProjProposalApprovalQuery = "UPDATE [ProjProposalApprovalProcess] SET [SubmissionStatus] = @Status WHERE [SubmissionID] = @Sid";

                        string Sid;
                        using (SqlCommand command4 = new SqlCommand(GetSubmissonIDQuery, connection, transaction))
                        {
                            command4.Parameters.AddWithValue("@ID", pid);
                            SqlDataReader reader = command4.ExecuteReader();
                            if (reader.Read())
                            {
                                Sid = reader.GetInt32(0).ToString();
                            }
                            else
                            {
                                reader.Close();
                                ErrorLabel.Text = "Error: Submission ID not found.";
                                return;
                            }
                            reader.Close();
                        }

                        using (SqlCommand cmd = new SqlCommand(ProjProposalApprovalQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Status", "UNDER REVIEW");
                            cmd.Parameters.AddWithValue("@Sid", Sid);
                            cmd.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();

                        // Show success message to user
                        SuccessLabel.Text = "Review Meeting is scheduled on " + date;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());

                        // Rollback the transaction in case of any error
                        transaction.Rollback();

                        // Handle the error and show a message to the user
                        ErrorLabel.Text = "Error: " + ex.Message;
                    }
                }
            }
        }
    }
}
