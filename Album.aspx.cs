using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
public partial class Album : System.Web.UI.Page
{
    protected int g_album_id;
    protected string g_username;
    protected int g_user_id;
    protected string g_album_title;

    protected string Get_Username()
    {
        return g_username;
    }
    protected int Get_UserID()
    {
        return g_user_id;
    }
    protected string Get_Album_Title()
    {
        return g_album_title;
    }

    protected void Upload_new_photo(object sender, EventArgs e)
    {
        if(Page.IsPostBack)
        {
            if (upload_photo.HasFile)
            {
                try
                {
                    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
                    int v_album_id = Convert.ToInt32(Page.RouteData.Values["id"]); ;
                    using (con)
                    {
                        con.Open();
                        
                        // name of the photo to be uploaded
                        string photo_filename = Path.GetFileName(upload_photo.FileName);
                        // check if it is already on server for that album exists a photo with that path 
                        string photo_exists_q = "SELECT user_id FROM dbo.Album_Photos JOIN dbo.Photos ON Photos.photo_id = Album_Photos.photo_id WHERE album_id = @albumid AND location_path = @photo_path";
                        SqlCommand comm_photo_exists = new SqlCommand(photo_exists_q, con);
                        comm_photo_exists.Parameters.AddWithValue("@albumid", v_album_id);
                        comm_photo_exists.Parameters.AddWithValue("@photo_path", "images/" + photo_filename);
                        int exists = Convert.ToInt32(comm_photo_exists.ExecuteScalar());
                        if (exists > 0)
                        {
                            AddPhotoErrors.Text = "Photo already in album!";
                            con.Close();
                            return;
                        }
                        con.Close();

                        // the photo is not in album , will be uploaded on server and links to db
                        upload_photo.SaveAs(Server.MapPath("~/images/") + photo_filename);

                        // select albums owner
                        con.Open();
                        string select_user_id = "select user_id from Albums where album_id = @albumid";
                        SqlCommand select_user_id_comm = new SqlCommand(select_user_id, con);
                        select_user_id_comm.Parameters.AddWithValue("@albumid", v_album_id);
                        int v_user_id = Convert.ToInt32(select_user_id_comm.ExecuteScalar());
                        con.Close();

                        //insert into photos
                        con.Open();
                        string insert_photo_q = "insert into Photos(user_id,title,location_path,upload_datetime) values (@userid, @title, @locationpath, GETDATE())";
                        SqlCommand insert_photo_comm = new SqlCommand(insert_photo_q, con);
                        insert_photo_comm.Parameters.AddWithValue("@userid", v_user_id);
                        insert_photo_comm.Parameters.AddWithValue("@title", AddPhotoTitle.Text);
                        insert_photo_comm.Parameters.AddWithValue("@locationpath", "images/" + photo_filename);
                        insert_photo_comm.ExecuteNonQuery();
                        con.Close();

                        //select photo_id of uploaded photo
                        con.Open();
                        string select_photo_id_q = "select TOP 1 photo_id from Photos where user_id = @userid and location_path = @locationpath order by upload_datetime desc ";
                        SqlCommand select_photo_id_comm = new SqlCommand(select_photo_id_q, con);
                        select_photo_id_comm.Parameters.AddWithValue("@userid", v_user_id);
                        select_photo_id_comm.Parameters.AddWithValue("@locationpath", "images/" + photo_filename);
                        int v_photo_id = Convert.ToInt32(select_photo_id_comm.ExecuteScalar());
                        con.Close();

                        //insert into Album_Photos
                        con.Open();
                        string insert_album_photos = "insert into Album_Photos (photo_id,album_id) values (@photoid,@albumid)";
                        SqlCommand insert_album_photos_comm = new SqlCommand(insert_album_photos, con);
                        insert_album_photos_comm.Parameters.AddWithValue("@photoid", v_photo_id);
                        insert_album_photos_comm.Parameters.AddWithValue("@albumid", g_album_id);
                        insert_album_photos_comm.ExecuteNonQuery();
                        con.Close();
                    }

                }
                catch (SqlException ex)
                {
                    AddPhotoErrors.Text = ex.Message;
                }
                
                HttpContext.Current.Response.RedirectToRoute("Album", new { id = Convert.ToInt32(Page.RouteData.Values["id"]) });
            }
            else
            {
                AddPhotoErrors.Text = "No photo loaded";
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        g_album_id = Convert.ToInt32(Page.RouteData.Values["id"]);
        AddPhotoErrors.Text = "";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");

        using (con)
        {
            con.Open();
            string album_photos_query = "SELECT Photos.photo_id AS photo_id, location_path AS location_path, album_title AS album_title FROM dbo.Photos JOIN dbo.Album_Photos ON Album_Photos.photo_id = Photos.photo_id JOIN dbo.Albums ON Albums.album_id = Album_Photos.album_id WHERE Albums.album_id = @albumid order by upload_datetime desc";
            SqlDataAdapter dataAdapter_album_photos = new SqlDataAdapter(album_photos_query, con);
            dataAdapter_album_photos.SelectCommand.Parameters.AddWithValue("@albumid", g_album_id);
            DataSet dataSet_album_photos = new DataSet();
            dataAdapter_album_photos.Fill(dataSet_album_photos);
            explorerAlbumPage.DataSource = dataSet_album_photos;
            explorerAlbumPage.DataBind();
            con.Close();

            con.Open();
            string get_album_title = "SELECT first_name + ' ' +  last_name AS user_name, Albums.user_id AS user_id, album_title AS album_title FROM dbo.Albums  JOIN dbo.Users ON Users.user_id = Albums.user_id WHERE album_id = @albumid";

            SqlCommand command_info = new SqlCommand(get_album_title, con);
            command_info.Parameters.AddWithValue("@albumid", g_album_id);
            SqlDataReader sdr = command_info.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Read();
                g_album_title = (string)sdr["album_title"];
                g_username = (string)sdr["user_name"];
                g_user_id = (int)sdr["user_id"];
            }
            con.Close();
        }

    }



}