using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class _Photo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string photo_id = Page.RouteData.Values["id"] as string;
        
        try
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
            using (connection)
            {
                connection.Open();
                string get_photo_query = "SELECT * FROM Photos WHERE photo_id = @photo_id";
                SqlCommand command = new SqlCommand(get_photo_query, connection);

                command.Parameters.AddWithValue("@photo_id", Int32.Parse(photo_id));

                SqlDataReader sdr = command.ExecuteReader();
                if (sdr.Read())
                {
                    imagePage.ImageUrl = (string)sdr["location_path"];


                }
                connection.Close();
            }
        }
        catch (SqlException exception)
        {

        }
    }
}