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

using System.Text;
using System.Data.SqlClient;

using UNV.Comm.Web;
using UNV.Comm.DataBase;
using join.pms.dal;
namespace join.pms.wap.UserCenter
{
    public partial class Default : UNV.Comm.Web.PageBase
    {
        private string m_UserID;
        private string m_PersonCardID;

        private string m_SqlParams;
        private string m_FileExt = ConfigurationManager.AppSettings["FileExtension"];
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            this.Uc_PageTop1.GetSysMenu("群众中心");
            if (!IsPostBack)
            {
                string personName = BIZ_Common.GetPersonFullName(this.m_UserID); 
                this.LiteralUserInfo.Text = "<p class=\"a1\">用户名</p><p class=\"a2\">" + personName + "</p>";
            }
        }

        #region 身份验证
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
                Response.Write("<script language='javascript'>alert(\"操作提示：请登录后再试！\");parent.location.href='/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh." + m_FileExt + "';</script>");
                Response.End();
            }
        }
        #endregion

    }
}
