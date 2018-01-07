using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");

            // most used tags
            using (connection)
            {
                connection.Open();
                string top_tags_query = "SELECT lower(Tags.title) AS title FROM dbo.Tags JOIN (SELECT  TOP 5 tag_id, COUNT(photo_id) AS nr FROM dbo.Photo_Tags GROUP BY tag_id ORDER BY nr DESC) Top_tags ON(Tags.tag_id = Top_tags.tag_id)";
                SqlCommand top_tags_cmd = new SqlCommand(top_tags_query, connection);
                SqlDataReader sdr = top_tags_cmd.ExecuteReader();
                sdr.Read();
                Tag_1.Text = (string)sdr["title"];
                sdr.Read();
                Tag_2.Text = (string)sdr["title"];
                sdr.Read();
                Tag_3.Text = (string)sdr["title"];
                //sdr.Read();
                //Tag_4.Text = (string)sdr["title"];
                //sdr.Read();
               // Tag_5.Text = (string)sdr["title"];
                connection.Close();
            }




            SqlConnection connection1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\StefaniaPirvu\Desktop\YrmeApp\App_Data\Database.mdf;Integrated Security=True");
            string get_photos_query = "SELECT * FROM Photos ORDER BY upload_datetime DESC";
            // SqlCommand command = new SqlCommand(get_photos, connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(get_photos_query, connection1);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            explorerImages.DataSource = dataSet;
            explorerImages.DataBind();


        } catch ( SqlException exception )
        {

        } 
    }
}