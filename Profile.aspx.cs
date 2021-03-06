﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class UserProfile : System.Web.UI.Page
{
    protected int g_user_id;
    protected string g_first_name;
    protected string g_last_name;
    protected string g_current_city;
    protected string g_country;
    protected string g_join_date;
    protected string g_avatar_path;
    protected int g_nr_photos;
    protected int g_nr_favs;
    protected int g_nr_faved;
    
    protected string Get_Username()
    {
        return g_first_name + " " + g_last_name;
    }
    protected string Get_Current_City()
    {
        return g_current_city;
    }
    protected string Get_Country()
    {
        return g_country;
    }
    protected string Get_Join_Date()
    {
        return g_join_date;
    }
    protected string Get_Avatar_Path()
    {
        return g_avatar_path;
    }
    protected int Get_Favs_Number()
    {
        return g_nr_favs;
    }
    protected int Get_Photos_Number()
    {
        return g_nr_photos;
    }
    protected int Get_Faved_Number()
    {
        return g_nr_faved;
    }

    protected void Create_new_album(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
            int v_user_id = Convert.ToInt32(HttpContext.Current.Session["ID"]);
            string v_album_title = AddAlbumTitle.Text;
            using (con)
            {
                // check if user already has an album with that title
                con.Open();
                string check_album_title_q = "select * from Albums where user_id = @userid and lower(album_title) = @albumtitle";
                SqlCommand check_album_title_c = new SqlCommand(check_album_title_q, con);
                check_album_title_c.Parameters.AddWithValue("@userid", v_user_id);
                check_album_title_c.Parameters.AddWithValue("@albumtitle", v_album_title.ToLower());
                int result = Convert.ToInt32(check_album_title_c.ExecuteScalar());
                if (result > 0)
                {
                    AddAlbumErrors.Text = "You already have an album with that title";
                    con.Close();
                    return;
                }
                con.Close();
                //insert new album
                con.Open();
                string insert_album_query = "insert into Albums (user_id,album_title) values (@userid, @albumtitle)";
                SqlCommand insert_album_comm = new SqlCommand(insert_album_query, con);
                insert_album_comm.Parameters.AddWithValue("@userid", v_user_id);
                insert_album_comm.Parameters.AddWithValue("@albumtitle", v_album_title);
                insert_album_comm.ExecuteNonQuery();
                AddAlbumErrors.Text = "";
                con.Close();
            }
            HttpContext.Current.Response.RedirectToRoute("Profile", new { id = v_user_id });

        }
    }

    protected void Page_Load(object sender,EventArgs e)
    {
        AddAlbumErrors.Text = "";
        //SERVER/Profile/{id}

        // user_id of the loaded page
        string id = Page.RouteData.Values["id"] as string;
        g_user_id = Convert.ToInt32(id);

        // concection string
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");



        using(con)
        {
            con.Open();
            // query - gets- first_name, last_name, current_city, country, join_date, avatar_path
            string get_info_query = "SELECT dbo.Users.first_name AS first_name, dbo.Users.last_name AS last_name, Profile_Details.current_city AS current_city, Profile_Details.country AS country, CONVERT(VARCHAR(12),Users.creation_date,107) AS join_date,SUBSTRING(dbo.Profile_Photos.path_location,2,LEN(dbo.Profile_Photos.path_location)) AS avatar_path FROM Profile_Details JOIN dbo.Users ON dbo.Users.USER_ID = Profile_Details.user_id JOIN dbo.Profile_Photos ON Profile_Photos.user_id = Users.user_id WHERE dbo.Users.USER_ID = @userid";
            SqlCommand command_info = new SqlCommand(get_info_query, con);
            command_info.Parameters.AddWithValue("@userid", g_user_id);
            SqlDataReader sdr = command_info.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Read();
                g_first_name = (string)sdr["first_name"];
                g_last_name = (string)sdr["last_name"];
                g_current_city = (string)sdr["current_city"];
                g_country = (string)sdr["country"];
                g_join_date = (string)sdr["join_date"];
                g_avatar_path = (string)sdr["avatar_path"];

            }
            con.Close();


            // get the number of photos
            con.Open();
            string get_photos_nr_query = "SELECT COUNT(photo_id) FROM dbo.Photos WHERE USER_ID = @userid";
            SqlCommand command_photos = new SqlCommand(get_photos_nr_query, con);
            command_photos.Parameters.AddWithValue("@userid", g_user_id);
            g_nr_photos = Convert.ToInt32(command_photos.ExecuteScalar());
            con.Close();

            // get the number of favs
            con.Open();
            string get_favs_nr_query = "SELECT COUNT(Favs.user_id) FROM dbo.Favs JOIN dbo.Photos ON Photos.photo_id = Favs.photo_id  WHERE Photos.user_id = @userid GROUP BY Photos.user_id";
            SqlCommand command_favs = new SqlCommand(get_favs_nr_query, con);
            command_favs.Parameters.AddWithValue("@userid", g_user_id);
            g_nr_favs = Convert.ToInt32(command_favs.ExecuteScalar());
            con.Close();

            // get the number of favs
            con.Open();
            string get_faved_nr_query = "SELECT COUNT(photo_id) FROM dbo.Favs WHERE USER_ID = @userid";
            SqlCommand command_faved = new SqlCommand(get_faved_nr_query, con);
            command_faved.Parameters.AddWithValue("@userid", g_user_id);
            g_nr_faved = Convert.ToInt32(command_faved.ExecuteScalar());
            con.Close();

            //for favs photo gallery
            con.Open();
            string favs_query = "SELECT Photos.photo_id AS photo_id, Photos.location_path AS location_path,Photos.user_id AS owner_user_id FROM dbo.Photos JOIN dbo.Favs ON Favs.photo_id = Photos.photo_id WHERE Favs.user_id = @userid";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(favs_query, con);
            dataAdapter.SelectCommand.Parameters.AddWithValue("@userid", g_user_id);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            explorerFavs.DataSource = dataSet;
            explorerFavs.DataBind();
            con.Close();

            // for albums gallery
            con.Open();
            string user_albums_query = "SELECT albs.album_title AS album_title, albs.location_path AS location_path, albs.album_id AS album_id,albs.photo_id AS photo_id FROM (SELECT ROW_NUMBER() OVER(PARTITION BY Album_Photos.album_id ORDER BY Album_Photos.photo_id) AS rownum, ISNULL(dbo.Photos.photo_id, 0)  AS photo_id, ISNULL(dbo.Photos.location_path, 'images/empty-album.png') AS location_path, dbo.Albums.album_id AS album_id, dbo.Albums.album_title AS album_title FROM dbo.Albums LEFT JOIN dbo.Album_Photos ON Album_Photos.album_id = Albums.album_id LEFT JOIN dbo.Photos ON Photos.photo_id = Album_Photos.photo_id WHERE Albums.USER_ID = @userid) albs WHERE albs.rownum = 1";
            SqlDataAdapter dataAdapter_albums = new SqlDataAdapter(user_albums_query, con);
            dataAdapter_albums.SelectCommand.Parameters.AddWithValue("@userid", g_user_id);
            DataSet dataSet_albums = new DataSet();
            dataAdapter_albums.Fill(dataSet_albums);
            explorerAlbums.DataSource = dataSet_albums;
            explorerAlbums.DataBind();
            con.Close();

        }

    }



}