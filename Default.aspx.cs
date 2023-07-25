using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Server;

namespace Project_Aris
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login(object sender, EventArgs e)
        {
            string connectionString = "Data Source=ADITYA-PAL\\SQLEXPRESS;Initial Catalog=Project_Aris;Integrated Security=True;";


            if (Login_UserID.Text == "" || Login_Pass.Text=="")
            {
                error.Text = "Kindly Fill You ID & Password !";


            }
            else
            {
                
                int inputUserID = int.Parse(Login_UserID.Text);
                string inputPassword = Login_Pass.Text;
                DateTime inputDate = DateTime.Now;



                // Create a SqlConnection object
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        // Open the connection
                        connection.Open();

                        

                        // Query to fetch the UserID and Password from the database
                        string query = "SELECT UserID, Password, RoleID, FirstName, SupervisorID FROM [User] WHERE UserID = @UserID";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            
                            // Add parameter for the input UserID
                            command.Parameters.AddWithValue("@UserID", inputUserID);

                            // Execute the query and process the results
                            SqlDataReader reader = command.ExecuteReader();
                            


                            if (reader.Read())
                            {   
                                int u = reader.GetInt32(0);

                                
                                Console.WriteLine(u);
                                int storedUserID = u;
                                string storedPassword = reader.GetString(1);
                                int roleID = reader.GetInt32(2);
                                string firstName = reader.GetString(3);
                                string supervisor = reader.GetInt32(4).ToString();

                                Session["ID"] = storedUserID;
                                Session["Name"] = firstName;
                                Session["SupervioserID"]=supervisor;

                                if ((storedUserID == inputUserID )&& (storedPassword == inputPassword))
                                {
                                    
                                    string role = Convert.ToString(roleID);
                                    if (role != null)
                                    {
                                        // Redirect users based on their RoleID
                                        switch (role)
                                        {  
                                            case "1":

                                                Response.Redirect("AdminControls/Welcome_Admin.aspx");
                                                break;
                                            default:
                                                Response.Redirect("Home.aspx");
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        // Invalid UserID, handle appropriately (e.g., display error message)
                                    }
                                }
                                else
                                {
                                    // Invalid credentials, display error message
                                    error.Text = "Invalid UserID or Password.";
                                }
                            }
                            else
                            {
                                // User not found, display error message
                                error.Text = "User not found.";
                            }
                            reader.Close();
                            connection.Close();
                        }


                        string lastlogin = "UPDATE [User]SET LastLogin = @LastLogin WHERE UserID = @UserID;";
                        using (SqlCommand command1 = new SqlCommand(lastlogin, connection))
                        {
                            try {
                                connection.Open();
                                command1.Parameters.AddWithValue("@UserID", inputUserID);
                                command1.Parameters.AddWithValue("@LastLogin", inputDate);


                                command1.ExecuteNonQuery();
                                connection.Close();


                            } catch (Exception ex)
                            {  
                                error.Text = ex.ToString();

                            }
                            
                        }




                        // Close the connection
                        
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that occurred during the process
                        // Display or log the error message
                        Response.Write("An error occurred: " + ex.Message);
                    }
                }

            }
        }

        private string GetRoleIDFromDatabase(string userID)
        {
            string roleID = null;

            string connectionString = "Data Source=ADITYA-PAL\\SQLEXPRESS;Initial Catalog=Project_Aris;Integrated Security=True;";



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT RoleID FROM Users WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    roleID = reader["RoleID"].ToString();
                }
                reader.Close();
            }
            return roleID;
        }
        
            }


}