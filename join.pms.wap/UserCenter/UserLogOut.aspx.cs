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

using UNV.Comm.DataBase;
using UNV.Comm.Web;
namespace join.pms.wap.UserCenter
{
    public partial class UserLogOut : UNV.Comm.Web.PageBase
    {
        private string m_UserID;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();

            if (!string.IsNullOrEmpty(m_UserID))
            {
                LogoutSys();
            }
        }

        /// <summary>
        /// 身份验证 
        /// </summary>
        private void AuthenticateUser()
        {
            bool returnVa = false;
            if (Request.Browser.Cookies)
            {
                HttpCookie loginCookie = Request.Cookies["AREWEB_OC_PUBSVRS_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["UserID"] != null && !String.IsNullOrEmpty(Session["UserID"].ToString())) { returnVa = true; m_UserID = Session["UserID"].ToString(); }
            }

            if (!returnVa)
            {
                Response.Write("<script language='javascript'>parent.location.href='/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh." + m_FileExt + "';</script>");
                Response.End();
            }
        }

        private void LogoutSys()
        {
            //DbHelperSQL.SetSysLog(aryUser[0], Request.UserHostAddress, "退出", "普通会员用户[" + aryUser[3] + "-" + aryUser[2] + "]于 " + DateTime.Now.ToString() + " 退出系统");
            Session["UserID"] = null;
            Session.Clear();
            Session.Abandon();

            HttpCookie loginCookie = Request.Cookies["AREWEB_OC_PUBSVRS_YSL"];
            if (loginCookie != null)
            {
                loginCookie.Values["UserID"] = null;
                loginCookie.Expires = DateTime.Now.AddYears(-10);
                loginCookie = null;
            }

            Response.Cookies["AREWEB_OC_PUBSVRS_YSL"].Expires = DateTime.Now.AddYears(-10);

            string refUrl = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : string.Empty;

            Response.Write("<script language='javascript'>parent.location.href='/'</script>");
            Response.End();

        }

    }
}

