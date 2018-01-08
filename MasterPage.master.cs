using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public string searchValueInput;


    protected void Page_Load(object sender, EventArgs e)
    {
     
        System.Diagnostics.Debug.WriteLine(HttpContext.Current.Session["ID"] == null);
        System.Diagnostics.Debug.WriteLine(HttpContext.Current.Session["email"] == null);
        System.Diagnostics.Debug.WriteLine(HttpContext.Current.Session["username"] == null);
        System.Diagnostics.Debug.WriteLine(HttpContext.Current.Session["profile_path"] == null);

        if(HttpContext.Current.Session["ID"] != null)
        {
            string ex = (string)HttpContext.Current.Session["username"];
            Profile_nav_img.ImageUrl = (string)HttpContext.Current.Session["profile_path"];
        }

       
    }

    protected void log_out_button_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Session["ID"] = null;
        HttpContext.Current.Session["username"] = null;
        HttpContext.Current.Session["email"] = null;
        HttpContext.Current.Session["profile_path"] = null;
        HttpContext.Current.Session.RemoveAll();
        HttpContext.Current.Response.RedirectToRoute("DefaultRoute");
    }
    protected void search_button(object sender, EventArgs e)
    {
        /*
        System.Diagnostics.Debug.WriteLine(e);
        string tagTitle = Page.Request.Form["search_input"];
        if (tagTitle != null)
        {
            HttpContext.Current.Response.RedirectToRoute("PhotosByTag");
        }
        */
    }

}
