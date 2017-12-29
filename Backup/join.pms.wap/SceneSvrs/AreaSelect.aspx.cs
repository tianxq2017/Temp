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

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;
namespace join.pms.wap.SceneSvrs
{
    public partial class AreaSelect : UNV.Comm.Web.PageBase
    {
        private string m_UserID;
        protected string m_BizCode;
        private string m_BizType;

        private string m_SiteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            this.Uc_PageTop1.GetSysMenu("业务办理");
            if (!IsPostBack)
            {
                SetPageHeader("业务办理");

                BIZ_Common.GetAreaData(this.LiteralIII, m_SiteArea, "3");
            }
        }
        #region 设置页头信息及导航\验证参数\验证用户等
        /// <summary>
        /// 验证接受的参数
        /// </summary>
        private void ValidateParams()
        {
            m_BizCode = PageValidate.GetTrim(Request.QueryString["c"]);
            m_BizType = PageValidate.GetTrim(Request.QueryString["t"]);
            if (!string.IsNullOrEmpty(m_BizCode) && PageValidate.IsNumber(m_BizCode) && !string.IsNullOrEmpty(m_BizType) && PageValidate.IsNumber(m_BizType))
            {
                this.txtW.Value = m_BizCode;
                this.txtBizType.Value = m_BizType;
            }
            else
            {
                Server.Transfer("~/errors.aspx");
            }
        }
        //设置页头信息及导航等
        private void SetPageHeader(string objTitles)
        {
            try
            {
                this.Page.Header.Title = this.m_SiteName;
                base.AddMetaTag("keywords", Server.HtmlEncode(this.m_SiteName + "," + objTitles + "," + this.m_SiteKeyWord));
                base.AddMetaTag(this.m_SiteName);
                base.AddMetaTag("description", Server.HtmlEncode(m_SiteDescription));
                base.AddMetaTag("copyright", Server.HtmlEncode("本页版权归 西安网是科技发展有限公司 所有。All Rights Reserved"));
            }
            catch
            {
                Server.Transfer("~/errors.aspx");
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
        #endregion
    }
}
