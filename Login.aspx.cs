using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Result.Text = "";
        if (HttpContext.Current.Session["ID"] != null)
        {
            HttpContext.Current.Response.RedirectToRoute("Profile", new { username = HttpContext.Current.Session["username"] });
        }
    }

    protected void Login_button_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine(user_login_email.Text == "");
        System.Diagnostics.Debug.WriteLine(user_login_password.Text == "");

        if (user_login_email.Text == "" || user_login_password.Text == "")
            System.Diagnostics.Debug.WriteLine("Did not logged in!");
        else
        {

            try
            {
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");

                Boolean found_it = false;
                Result.Text = "Did not find an user with that email address!";
                string get_user_query = "select Users.user_id AS user_id, email AS email, password AS password, first_name ,last_name,path_location from Users JOIN dbo.Profile_Photos ON Profile_Photos.user_id = Users.user_id WHERE email = @emailaddress";
                SqlCommand command = new SqlCommand(get_user_query, connection);
                command.Parameters.AddWithValue("@emailaddress", user_login_email.Text);


                using (connection)
                {
                    connection.Open();
                    SqlDataReader sdr = command.ExecuteReader();
                    while (sdr.Read())
                    {
                        if (sdr["email"].Equals(user_login_email.Text))
                        {
                            string password_string = (string)sdr["password"];
                            if (password_string.Equals(user_login_password.Text))
                            {
                                string email_address = (string)sdr["email"];
                                int user_id = (int)sdr["user_id"];
                                string last_name = (string)sdr["last_name"];
                                string first_name = (string)sdr["first_name"];
                                string path_location = (string)sdr["path_location"];

                                HttpContext.Current.Session["ID"] = user_id;

                                //HttpContext.Current.Session.Clear();
                                System.Diagnostics.Debug.WriteLine(HttpContext.Current.Session["ID"]);

                                HttpContext.Current.Session["profile_path"] = path_location;
                                HttpContext.Current.Session["username"] = last_name + "  " + first_name;
                                HttpContext.Current.Session["email"] = email_address;
                                HttpContext.Current.Response.RedirectToRoute("Profile", new { id = user_id });

                                Result.Text = "";

                                break;
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Result.Text = " " + ex.Message;
            }
            finally
            {
                user_login_email.Text = "";
                user_login_password.Text = "";

            }
        }
    }
}