using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

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
    protected void Page_Load(object sender, EventArgs e)
    {
        g_album_id = Convert.ToInt32(Page.RouteData.Values["id"]);

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");

        using (con)
        {
            con.Open();
            string album_photos_query = "SELECT Photos.photo_id AS photo_id, location_path AS location_path, album_title AS album_title FROM dbo.Photos JOIN dbo.Album_Photos ON Album_Photos.photo_id = Photos.photo_id JOIN dbo.Albums ON Albums.album_id = Album_Photos.album_id WHERE Albums.album_id = @albumid";
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