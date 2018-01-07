using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class _Photos_By_Tag : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string tag_title = Page.RouteData.Values["tag"] as string;
        try
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
            
            string get_photos_query = "SELECT p.photo_id as photo_id, p.location_path as location_path FROM dbo.Photos p JOIN dbo.Photo_Tags pt ON pt.photo_id = p.photo_id JOIN dbo.Tags t ON t.tag_id = pt.tag_id WHERE t.title = @tag_title ORDER BY p.upload_datetime desc";
            // SqlCommand command = new SqlCommand(get_photos, connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(get_photos_query, connection);
            dataAdapter.SelectCommand.Parameters.AddWithValue("@tag_title", tag_title);

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            explorerImagesByTag.DataSource = dataSet;
            explorerImagesByTag.DataBind();

        }
        catch(SqlException ex)
        {

        }
    }
}