using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace join.pms.web
{
    public partial class errors : System.Web.UI.Page
    {
        //protected TextBox butLogin;
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Request.ValidateInput();  
            string errors = Request.QueryString["k"];
            if (!string.IsNullOrEmpty(errors)) {
                Response.Write(errors);
                Response.Write("<br/>");
                //Response.Write(Server.UrlDecode(errors));
                //Response.Write("<br/>");
                //Response.Write(Server.HtmlDecode(errors));
                //Response.Write("<br/>");
                //Response.Write(Uri.UnescapeDataString(errors));
            }
            //if(!IsPostBack){
            //    butLogin = new TextBox();
            //    butLogin.Text = "abc°¡°¡";
            //    butLogin.ReadOnly = true;
            //}
            
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    Response.Write(butLogin.Text);
        //}

    }
}
