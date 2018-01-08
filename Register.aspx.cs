using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Register_Result.Text = "";
        if (HttpContext.Current.Session["ID"] != null)
        {
            HttpContext.Current.Response.RedirectToRoute("Profile", new { username = HttpContext.Current.Session["id"] });
        }
    }
    protected void Register_button_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine(user_reg_email.Text == "");
        System.Diagnostics.Debug.WriteLine(user_reg_firstname.Text == "");
        System.Diagnostics.Debug.WriteLine(user_reg_lastname.Text == "");
        System.Diagnostics.Debug.WriteLine(user_reg_password.Text == "");



        if( user_reg_email.Text == "" || user_reg_firstname.Text == "" || user_reg_lastname.Text == "" || user_reg_password.Text == "")
            System.Diagnostics.Debug.WriteLine("Did not register!");
        else
        {
            try
            {
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
               

                using (connection)
                {
                    connection.Open();
                    //check if the email already exists in database
                    string user_exists_query = "select * from Users where email = @emailaddress";
                    SqlCommand test_user_exits = new SqlCommand(user_exists_query, connection);
                    string email_adr = user_reg_email.Text;
                    test_user_exits.Parameters.AddWithValue("@emailaddress", email_adr);
                    int user_id = Convert.ToInt32(test_user_exits.ExecuteScalar());
                    if (user_id > 0)
                    {
                        Register_Result.Text = "User already exists!";
                        connection.Close();
                        return;
                    }
                    connection.Close();

                    connection.Open();
                    // insert new user in users table 
                    string insert_user_query = "insert into Users (first_name, last_name, email, password, creation_date, pass_last_upd) values(@firstname,@lastname,@email,@password,CONVERT (date, SYSDATETIME()), CONVERT (date, SYSDATETIME()))";
                    SqlCommand insert_user = new SqlCommand(insert_user_query, connection);
                    insert_user.Parameters.AddWithValue("@firstname", user_reg_firstname.Text);
                    insert_user.Parameters.AddWithValue("@lastname", user_reg_lastname.Text);
                    insert_user.Parameters.AddWithValue("@email", user_reg_email.Text);
                    insert_user.Parameters.AddWithValue("@password", user_reg_password.Text);
                    insert_user.ExecuteNonQuery();
                    connection.Close();

                    Register_Result.Text = "User added succesfully!";
                    Register_Result.ForeColor = System.Drawing.Color.Green;

                    // log in the new user
                    connection.Open();
                    SqlCommand log_in_command = new SqlCommand("select * from Users WHERE email = @email1", connection);
                    log_in_command.Parameters.AddWithValue("@email1", user_reg_email.Text);
                    SqlDataReader sdr = log_in_command.ExecuteReader();
                    string last_name = "";
                    string first_name = "";
                    string email_address = "";
                    int user = 0;
                    string path_location = "";
                    if (sdr.Read())
                    {
                        HttpContext.Current.Session["ID"] = (int)sdr["user_id"];
                        //HttpContext.Current.Session.Clear();
                        System.Diagnostics.Debug.WriteLine(HttpContext.Current.Session["ID"]);

                        last_name = (string)sdr["last_name"];
                        first_name = (string)sdr["first_name"];
                        email_address = (string)sdr["email"];
                        user = (int)sdr["user_id"];                     
                    }
                    connection.Close();

                    //insert into Profile_Photos
                    connection.Open();
                    string insert_default_photo_q = "insert into Profile_Photos (user_id, path_location) values (@userid, '~/profile_photos/facebook-default-no-profile-pic-300x300.jpg')";
                    SqlCommand insert_default_photo_comm = new SqlCommand(insert_default_photo_q, connection);
                    insert_default_photo_comm.Parameters.AddWithValue("@userid", user);
                    insert_default_photo_comm.ExecuteNonQuery();                    
                    connection.Close();

                    //insert into Profile_Details
                    connection.Open();
                    string insert_details_q = "insert into Profile_Details (user_id, current_city,country) values (@userid,'unknown','unknown')";
                    SqlCommand insert_details_comm = new SqlCommand(insert_details_q, connection);
                    insert_details_comm.Parameters.AddWithValue("@userid", user);
                    insert_details_comm.ExecuteNonQuery();
                    connection.Close();

                    // insert into User_Roles
                    connection.Open();
                    string insert_user_roles_q = "insert into User_Roles (user_id, role_id) values (@userid, 2)";
                    SqlCommand insert_user_roles_comm = new SqlCommand(insert_user_roles_q, connection);
                    insert_user_roles_comm.Parameters.AddWithValue("@userid", user);
                    insert_user_roles_comm.ExecuteNonQuery();
                    connection.Close();

                    Register_Result.Text = "";
                    HttpContext.Current.Session["profile_path"] = "~/profile_photos/facebook-default-no-profile-pic-300x300.jpg";
                    HttpContext.Current.Session["email"] = email_address;
                    HttpContext.Current.Session["username"] = last_name + first_name;
                    HttpContext.Current.Response.RedirectToRoute("Profile", new { id = user });
                    
                    
                   
                }
                    
            }
            catch (SqlException ex)
            {
                Register_Result.Text = ex.Message;
                
            }
            finally
            {
                user_reg_email.Text = "";
                user_reg_firstname.Text = "";
                user_reg_lastname.Text = "";
                user_reg_password.Text = "";

            }
        }

    }
}