using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;

public partial class Edit_Profile : System.Web.UI.Page
{
    protected int g_user_id;
    protected string g_avatar_path;
    //protected string g_first_name;
    //protected string g_last_name;
    //protected string g_current_city;
    //protected string g_country;

    protected string Get_Avatar_Path()
    {
        return g_avatar_path;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Edit_Errors.Text = "";
        if (HttpContext.Current.Session["ID"] == null)
        {
            HttpContext.Current.Response.RedirectToRoute("DefaultRoute");
            return;
        }

        if (!Page.IsPostBack)
        {
            g_user_id = Convert.ToInt32(HttpContext.Current.Session["ID"]);

            string first_name = "";
            string last_name = "";
            string current_city = "";
            string country = "";
            string avatar_path = "";

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
            using (con)
            {
                con.Open();
                string get_info_query = "SELECT first_name AS first_name, last_name AS last_name , current_city AS current_city, country AS country, SUBSTRING(dbo.Profile_Photos.path_location,2,LEN(dbo.Profile_Photos.path_location)) AS avatar_path FROM dbo.Users JOIN dbo.Profile_Details ON Profile_Details.user_id = Users.user_id JOIN dbo.Profile_Photos ON Profile_Photos.user_id = Users.user_id WHERE Users.user_id = @userid";

                SqlCommand command_get_info = new SqlCommand(get_info_query, con);
                command_get_info.Parameters.AddWithValue("@userid", g_user_id);
                SqlDataReader sdr = command_get_info.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    first_name = (string)sdr["first_name"];
                    last_name = (string)sdr["last_name"];
                    current_city = (string)sdr["current_city"];
                    country = (string)sdr["country"];
                    avatar_path = (string)sdr["avatar_path"];
                }
                con.Close();
            }
            edit_first_name.Text = first_name;
            edit_last_name.Text = last_name;
            edit_current_city.Text = current_city;
            edit_country.Text = country;

            g_avatar_path = avatar_path;
            //g_first_name = first_name;
            //g_last_name = last_name;
            //g_current_city = current_city;
            //g_country = country;
        }
        else
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
            using (con)
            {
                con.Open();
                string get_photo_path = "SELECT SUBSTRING(dbo.Profile_Photos.path_location,2,LEN(dbo.Profile_Photos.path_location)) FROM Profile_Photos WHERE USER_ID = @userid";
                SqlCommand command_path = new SqlCommand(get_photo_path, con);
                command_path.Parameters.AddWithValue("@userid", Convert.ToInt32(HttpContext.Current.Session["ID"]));
                g_avatar_path = Convert.ToString(command_path.ExecuteScalar());
                con.Close();
            }

        }
        

    }
    protected void Edit_Save_Button_Click(object sender, EventArgs e)
    {
        if(Page.IsPostBack)
        {
            string new_first_name = edit_first_name.Text;
            string new_last_name = edit_last_name.Text;
            string new_current_city = edit_current_city.Text;
            string new_country = edit_country.Text;
            string new_avatar_path = g_avatar_path;

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");

            if (edit_profile_upload.HasFile)
            {
                try
                {
                    SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");

                    string filename = Path.GetFileName(edit_profile_upload.FileName);
                    edit_profile_upload.SaveAs(Server.MapPath("~/profile_photos/") + filename);

                    using (con1)
                    {
                        con1.Open();
                        // get current avatar path
                        string get_current_path = "SELECT path_location from Profile_Photos where user_id = @userid";
                        SqlCommand command_get_current_path = new SqlCommand(get_current_path, con1);
                        command_get_current_path.Parameters.AddWithValue("@userid", Convert.ToInt32(HttpContext.Current.Session["ID"]));
                        string v_current_path = Convert.ToString(command_get_current_path.ExecuteScalar());
                        con1.Close();
                   
                        // delete prev pic if it is not the default photo
                        if(!(v_current_path.Equals("~/profile_photos/facebook-default-no-profile-pic-300x300.jpg")))
                        {
                            var filePath = Server.MapPath(v_current_path);
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                        }
                        

                        con1.Open();
                        string update_avatar_path = "update Profile_Photos set  path_location = @pathlocation where user_id = @userid";
                        SqlCommand update_avatar_comm = new SqlCommand(update_avatar_path, con1);
                        update_avatar_comm.Parameters.AddWithValue("@pathlocation", "~/profile_photos/" + filename);
                        update_avatar_comm.Parameters.AddWithValue("@userid", Convert.ToInt32(HttpContext.Current.Session["ID"]));
                        update_avatar_comm.ExecuteNonQuery();

                        con1.Close();
                    }
                    
                }
                catch (Exception ex)
                {
                    
                }
            }


            try
            {
                 using (con)
                 {
                        con.Open();
                        string update_users_query = "update Users set  first_name = @firstname, last_name = @lastname where user_id = @userid";
                        SqlCommand update_user = new SqlCommand(update_users_query, con);
                        update_user.Parameters.AddWithValue("@firstname", new_first_name);
                        update_user.Parameters.AddWithValue("@lastname", new_last_name);
                        update_user.Parameters.AddWithValue("@userid", Convert.ToInt32(HttpContext.Current.Session["ID"]));
                        update_user.ExecuteNonQuery();
                        con.Close();
                  
                        con.Open();
                        string update_users_details_query = "update Profile_Details set  current_city = @currentcity, country = @country where user_id = @userid";
                        SqlCommand update_users_details = new SqlCommand(update_users_details_query, con);
                        update_users_details.Parameters.AddWithValue("@currentcity", new_current_city);
                        update_users_details.Parameters.AddWithValue("@country", new_country);
                        update_users_details.Parameters.AddWithValue("@userid", Convert.ToInt32(HttpContext.Current.Session["ID"]));
                        update_users_details.ExecuteNonQuery();
                        con.Close();
                 }
                
            }
            catch (SqlException ex)
            {
                Edit_Errors.Text = ex.Message;
            }
            HttpContext.Current.Response.RedirectToRoute("Profile", new { id = HttpContext.Current.Session["id"] });
        }

    }
    protected void Edit_Cancel_Button_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.RedirectToRoute("EditProfile", new { id = HttpContext.Current.Session["id"] });
    }
}