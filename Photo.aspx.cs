using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class _Photo : System.Web.UI.Page
{
    protected string g_user_avatar;
    protected string g_owner_avatar;
    protected string g_owner_username;
    protected string g_photo_title;
    protected string g_upload_date;
    protected string g_album_title;
    protected int g_album_id;
    protected int g_nr_favs;
    protected int g_nr_comms;
    protected int g_owner_id;
    protected int g_photo_id;
    protected int g_user_id;
    protected string GetUserAvatar()
    {
       
        return g_user_avatar;
    }
    protected int GetNrComments()
    {
        return g_nr_comms;
    }
    protected int GetNrFavs()
    {
        return g_nr_favs;
    }
    protected string GetOwnerAvatar()
    {
        return g_owner_avatar;
    }
    protected string GetOwnerUsername()
    {
        return g_owner_username;
    }
    protected string GetPhotoTitle()
    {
        return g_photo_title;
    }
    protected string GetUploadDate()
    {
        return g_upload_date;
    }
    protected string GetAlbumTitle()
    {
        return g_album_title;
    }
    protected int GetAlbumId()
    {
        return g_album_id;
    }

    protected void CloseCommBtn(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            System.Web.UI.HtmlControls.HtmlButton btn = (System.Web.UI.HtmlControls.HtmlButton)sender;
            string t = btn.InnerHtml;
            int v_comm_id = Int32.Parse(Regex.Match(t, @"\d+").Value);
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
                using (con)
                {
                    // check if the user who deletes is  admin
                    con.Open();
                    string get_role = "SELECT role_id FROM dbo.User_Roles WHERE USER_ID = @userid";
                    SqlCommand get_role_comm = new SqlCommand(get_role, con);
                    get_role_comm.Parameters.AddWithValue("@userid", g_user_id);
                    int role = Convert.ToInt32(get_role_comm.ExecuteScalar());
                    con.Close();

                    // check if the user who deletes is  admin
                    con.Open();
                    string get_comm_owner = "SELECT user_id FROM Comments WHERE comment_id = @commid";
                    SqlCommand get_comm_com = new SqlCommand(get_comm_owner, con);
                    get_comm_com.Parameters.AddWithValue("@commid", v_comm_id);
                    int v_comm_owner_id = Convert.ToInt32(get_comm_com.ExecuteScalar());
                    con.Close();

                    if (role != 3)
                    {
                        if (g_user_id != v_comm_owner_id)
                        {
                            CommErrors.Text = "You do not have the rights to delete this photo!";
                            return;
                        }
                    }

                    //if not owner or admin

                    con.Open();
                    string delete_comm_query = "delete from Comments where comment_id = @commid";
                    SqlCommand delete_comm_com = new SqlCommand(delete_comm_query, con);
                    delete_comm_com.Parameters.AddWithValue("@commid", v_comm_id);
                    delete_comm_com.ExecuteNonQuery();
                    con.Close();
                }

                HttpContext.Current.Response.RedirectToRoute("Photo", new { id = g_photo_id });
            }
            catch (SqlException ex)
            {
                CommErrors.Text = ex.Message;
            }

            return;

        }
    }
    protected void Delete_Photo_Button_Click(object sender, EventArgs e)
    {
        if(Page.IsPostBack)
        {
            
            try
            {
                

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
                using (con)
                {
                    // check if the user who deletes is  owner
                    con.Open();
                    string get_role = "SELECT role_id FROM dbo.User_Roles WHERE USER_ID = @userid";
                    SqlCommand get_role_comm = new SqlCommand(get_role, con);
                    get_role_comm.Parameters.AddWithValue("@userid", g_user_id);
                    int role = Convert.ToInt32(get_role_comm.ExecuteScalar());
                    con.Close();
                    if(role != 3)
                    {
                        if (g_user_id != g_owner_id)
                        {
                            DeletePhotoLabel.Text = "You do not have the rights to delete this photo!";
                            return;
                        }
                    }
                    
                    //if not owner or admin
                    


                    con.Open();
                    string delete_photo_query = "delete from Photos where photo_id = @photoid";
                    SqlCommand delete_photo_comm = new SqlCommand(delete_photo_query, con);
                    delete_photo_comm.Parameters.AddWithValue("@photoid", g_photo_id);
                    delete_photo_comm.ExecuteNonQuery();
                    con.Close();
                }

                HttpContext.Current.Response.RedirectToRoute("DefaultRoute");
            }
            catch(SqlException ex)
            {

            }
        }
        return;
    }
    protected void Add_Comment_Button_Click(object sender, EventArgs e)
    {
        if(Page.IsPostBack)
        {
            string comment = CommInput.Text;
            if (comment.Equals(""))
            {
                CommErrors.Text = "You can not add an empty comment!";
                return;
            }

            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
                using (con)
                {
                    con.Open();
                    string insert_comm_query = "insert into Comments (user_id, photo_id,description,creation_date) values (@userid, @photoid, @description, CONVERT(DATE,GETDATE()))";
                    SqlCommand insert_comment_com = new SqlCommand(insert_comm_query, con);
                    insert_comment_com.Parameters.AddWithValue("@userid", g_user_id);
                    insert_comment_com.Parameters.AddWithValue("@photoid", g_photo_id);
                    insert_comment_com.Parameters.AddWithValue("@description", CommInput.Text);
                    insert_comment_com.ExecuteNonQuery();
                    con.Close();
                }                
                HttpContext.Current.Response.RedirectToRoute("Photo", new { id = g_photo_id });
            }
            catch(SqlException ex)
            {
                CommErrors.Text = ex.Message;
            }
        }
    }
    protected void Like_Button_Click(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {

            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
                using (con)
                {
                    con.Open();
                    string check_like = "select fav_id from Favs where user_id = @userid and photo_id = @photoid";
                    SqlCommand check_like_comm = new SqlCommand(check_like, con);
                    check_like_comm.Parameters.AddWithValue("@userid", g_user_id);
                    check_like_comm.Parameters.AddWithValue("@photoid", g_photo_id);
                    int fav_id = Convert.ToInt32(check_like_comm.ExecuteScalar());
                    con.Close();
                    if (fav_id > 0)
                    {
                        // exista, deci nu fac nimic
                        return;

                    }
                    // nu exista , inseram in favs
                    con.Open();
                    string like_query = "insert into Favs (user_id, photo_id) values (@userid, @photoid)";
                    SqlCommand like_query_comm = new SqlCommand(like_query, con);
                    like_query_comm.Parameters.AddWithValue("@userid", g_user_id);
                    like_query_comm.Parameters.AddWithValue("@photoid", g_photo_id);
                    like_query_comm.ExecuteNonQuery();
                    con.Close();
                    HttpContext.Current.Response.RedirectToRoute("Photo", new { id = g_photo_id });


                }
            }
            catch(SqlException ex)
            {

            }
        }
    }

    protected void Unlike_Button_Click(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
                using (con)
                {
                    con.Open();
                    string check_like = "select fav_id from Favs where user_id = @userid and photo_id = @photoid";
                    SqlCommand check_like_comm = new SqlCommand(check_like, con);
                    check_like_comm.Parameters.AddWithValue("@userid", g_user_id);
                    check_like_comm.Parameters.AddWithValue("@photoid", g_photo_id );
                    int fav_id = Convert.ToInt32(check_like_comm.ExecuteScalar());
                    con.Close();
                    if (fav_id > 0)
                    {
                        // exista, deci il scoatem
                        con.Open();
                        string unlike_query = "delete from Favs where fav_id = @favid";
                        SqlCommand unlike_query_comm = new SqlCommand(unlike_query, con);
                        unlike_query_comm.Parameters.AddWithValue("@favid", fav_id);
                        unlike_query_comm.ExecuteNonQuery();
                        con.Close();
                        HttpContext.Current.Response.RedirectToRoute("Photo", new { id = g_photo_id });

                    }
                    return;
                    
                }
            }
            catch (SqlException ex)
            {

            }
        }
    }

    protected void Add_Tag_Button_Click(object sender,EventArgs e)
    {
        if(Page.IsPostBack)
        {
            string tag = AddTagInput.Text;
            if (tag.Equals(""))
            {
                AddTag_Errors.Text = "Please fill a new tag name!";
                return;
            }
            if (tag.IndexOf(' ') != -1)
            {
                AddTag_Errors.Text = "Tag must be a single word!";
                return;
            }
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
                using (con)
                {
                    con.Open();
                    int photo_id = g_photo_id;
                    // verific daca tagul este deja in tags
                    string tag_exists_query = "SELECT tag_id FROM dbo.Tags WHERE title = LOWER(@tagtitle)";
                    SqlCommand tag_exists_comm = new SqlCommand(tag_exists_query, con);
                    tag_exists_comm.Parameters.AddWithValue("@tagtitle", AddTagInput.Text);
                    int tag_id = Convert.ToInt32(tag_exists_comm.ExecuteScalar());
                    con.Close();
                    if(tag_id > 0) // exista deja deci doar facem legatura
                    {
                        // verificam daca este facuta legatura
                        con.Open();
                        string check_tag = "select * from Photo_Tags where tag_id = @tagid and photo_id = @photoid";
                        SqlCommand check_tag_comm = new SqlCommand(check_tag, con);
                        check_tag_comm.Parameters.AddWithValue("@tagid", tag_id);
                        check_tag_comm.Parameters.AddWithValue("@photoid", photo_id);
                        int exists = Convert.ToInt32(check_tag_comm.ExecuteScalar());
                        con.Close();
                        if(exists > 0)
                        {
                            AddTag_Errors.Text = "Photo already has this tag!";
                            return;
                        }

                        // introduc in photo_tags
                        con.Open();
                        string insert_tag_query = "insert into Photo_Tags (tag_id,photo_id) values (@tagid, @photoid)";
                        SqlCommand insert_tag_comm = new SqlCommand(insert_tag_query, con);
                        insert_tag_comm.Parameters.AddWithValue("@tagid", tag_id);
                        insert_tag_comm.Parameters.AddWithValue("@photoid", photo_id);
                        insert_tag_comm.ExecuteNonQuery();
                        con.Close();

                    }
                    else
                    {
                        // trebuie introdus in tabela de taguri
                        con.Open();
                        string insert_tag_query = "insert into Tags (title) values (lower(@tagtitle))";
                        SqlCommand insert_tag_comm = new SqlCommand(insert_tag_query, con);
                        insert_tag_comm.Parameters.AddWithValue("@tagtitle", AddTagInput.Text);
                        insert_tag_comm.ExecuteNonQuery();
                        con.Close();
                        // selectam tag_id introdus
                        con.Open();
                        string select_tag_id = "select tag_id from Tags where title = lower(@tagtitle)";
                        SqlCommand select_tag_id_comm = new SqlCommand(select_tag_id, con);
                        select_tag_id_comm.Parameters.AddWithValue("@tagtitle", AddTagInput.Text);
                        int tagid = Convert.ToInt32(select_tag_id_comm.ExecuteScalar());
                        con.Close();

                        // inseram si in Photo_tags
                        con.Open();
                        string insert_tag_querys = "insert into Photo_Tags (tag_id,photo_id) values (@tagid, @photoid)";
                        SqlCommand insert_tag_comms = new SqlCommand(insert_tag_querys, con);
                        insert_tag_comms.Parameters.AddWithValue("@tagid", tagid);
                        insert_tag_comms.Parameters.AddWithValue("@photoid", photo_id);
                        insert_tag_comms.ExecuteNonQuery();
                        con.Close();
                    }

                }
            }
            catch (SqlException ex)
            {
                AddTag_Errors.Text = ex.Message;
            }
            HttpContext.Current.Response.RedirectToRoute("Photo", new { id = g_photo_id });
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        string photo_id = Page.RouteData.Values["id"] as string;
        g_photo_id = Int32.Parse(photo_id);
        if (HttpContext.Current.Session["ID"] != null)
        {
            g_user_id = (int)HttpContext.Current.Session["ID"];
        }
        
        CommErrors.Text = "";
        DeletePhotoLabel.Text = "";

        try
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
            using (connection)
            {
                connection.Open();
                string get_photo_query = "SELECT Albums.user_id as owner_id, Photos.location_path AS location_path, SUBSTRING(path_location,2,LEN(path_location)) AS owner_path, title AS photo_title, CONVERT(VARCHAR(13),upload_datetime) AS upload_date, Album_Photos.album_id AS album_id, album_title AS album_title,first_name + ' ' + last_name AS username  FROM dbo.Photos JOIN dbo.Album_Photos ON Album_Photos.photo_id = Photos.photo_id JOIN dbo.Albums ON Albums.album_id = Album_Photos.album_id JOIN dbo.Users ON Users.user_id = Albums.user_id JOIN dbo.Profile_Photos ON Profile_Photos.user_id = Users.user_id  WHERE Photos.photo_id = @photo_id";
                SqlCommand command = new SqlCommand(get_photo_query, connection);

                command.Parameters.AddWithValue("@photo_id", Int32.Parse(photo_id));

                SqlDataReader sdr = command.ExecuteReader();
                if (sdr.Read())
                {
                    imagePage.ImageUrl = (string)sdr["location_path"];
                    g_owner_avatar = (string)sdr["owner_path"];
                    g_owner_username = (string)sdr["username"];
                    g_photo_title = (string)sdr["photo_title"];
                    g_upload_date = (string)sdr["upload_date"];
                    g_album_title = (string)sdr["album_title"];
                    g_album_id = (int)sdr["album_id"];
                    g_owner_id = (int)sdr["owner_id"];


                }
                connection.Close();

                //get user avatar;
                connection.Open();
                string user_avatar_q = "SELECT SUBSTRING(path_location,2,LEN(path_location)) AS user_avatar FROM dbo.Profile_Photos WHERE USER_ID = @userid";
                SqlCommand user_avatar_comm = new SqlCommand(user_avatar_q, connection);
                user_avatar_comm.Parameters.AddWithValue("@userid", g_user_id);
               
                g_user_avatar = Convert.ToString(user_avatar_comm.ExecuteScalar());
                connection.Close();

                // get the number of favs
                connection.Open();
                string get_favs_nr_query = "SELECT COUNT(user_id) FROM Favs where photo_id = @photoid";
                SqlCommand command_favs = new SqlCommand(get_favs_nr_query, connection);
                command_favs.Parameters.AddWithValue("@photoid", Int32.Parse(photo_id));
                g_nr_favs = Convert.ToInt32(command_favs.ExecuteScalar());
                connection.Close();

                // get the number of comments
                connection.Open();
                string get_comm_nr_query = "SELECT COUNT(user_id) FROM Comments where photo_id = @photoid";
                SqlCommand command_comms = new SqlCommand(get_comm_nr_query, connection);
                command_comms.Parameters.AddWithValue("@photoid", Int32.Parse(photo_id));
                g_nr_comms = Convert.ToInt32(command_comms.ExecuteScalar());
                connection.Close();
            }

            SqlConnection connection1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
            string get_comms_query = "SELECT description AS comment_text, CONVERT(VARCHAR(13),creation_date,107) AS comm_date, SUBSTRING(path_location,2,LEN(path_location)) AS commenter_avatar FROM dbo.Comments JOIN dbo.Profile_Photos ON Profile_Photos.user_id = Comments.user_id where dbo.Comments.photo_id = @photoid";
            // SqlCommand command = new SqlCommand(get_photos, connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT dbo.Comments.comment_id AS comm_id, description AS comment_text, CONVERT(VARCHAR(13),creation_date,107) AS comm_date, SUBSTRING(path_location,2,LEN(path_location)) AS commenter_avatar FROM dbo.Comments JOIN dbo.Profile_Photos ON Profile_Photos.user_id = Comments.user_id where dbo.Comments.photo_id = @photoid", connection1);
            dataAdapter.SelectCommand.Parameters.AddWithValue("@photoid", Int32.Parse(photo_id));
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            explorerComments.DataSource = dataSet;
            explorerComments.DataBind();

            SqlConnection connection2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
            SqlDataAdapter dataAdapter2 = new SqlDataAdapter("SELECT title AS tag_title FROM dbo.Tags JOIN dbo.Photo_Tags ON Photo_Tags.tag_id = Tags.tag_id WHERE photo_id = @photoid", connection2);
            dataAdapter2.SelectCommand.Parameters.AddWithValue("@photoid", Int32.Parse(photo_id));
            DataSet dataSet2 = new DataSet();
            dataAdapter2.Fill(dataSet2);
            TagsExplorer.DataSource = dataSet2;
            TagsExplorer.DataBind();

        }
        catch (SqlException exception)
        {

        }
    }


}