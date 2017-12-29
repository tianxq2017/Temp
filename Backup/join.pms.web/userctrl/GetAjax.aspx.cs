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
using System.Globalization;
using System.Text;
using Jayrock.Json;

using System.Data.SqlClient;
using join.pms.dal;
namespace join.pms.web.userctrl
{
    public partial class GetAjax : UNV.Comm.Web.PageBase
    {
        private string m_UserID;
        private string m_CheckCodeTimeOut;
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            CommAjax ajax = new CommAjax();
            ajax.CAUserID = this.m_UserID;
            ajax.CAType = PageValidate.GetTrim(Request.Params["type"]);
            ajax.CAValue01 = PageValidate.GetTrim(HttpUtility.UrlDecode(Request.Params["value01"]));
            ajax.CAValue02 = PageValidate.GetTrim(HttpUtility.UrlDecode(Request.Params["value02"]));
            ajax.CAValue03 = PageValidate.GetTrim(HttpUtility.UrlDecode(Request.Params["value03"]));
            ajax.CAValue04 = PageValidate.GetTrim(HttpUtility.UrlDecode(Request.Params["value04"]));
            ajax.CAValue05 = PageValidate.GetTrim(HttpUtility.UrlDecode(Request.Params["value05"]));
            string str = ajax.GetJson();

            if (ajax.CAType.ToLower() == "sendcheckcode" || ajax.CAType.ToLower() == "sendcheckcoded")
            {
                m_CheckCodeTimeOut = ajax.RetValue02;
                if (!string.IsNullOrEmpty(m_CheckCodeTimeOut))
                {
                    SetSendMsgInfo(ajax.CAValue01, ajax.RetValue01);
                }
            }
            ajax = null;

            Response.Write(str);
            Response.End();
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
        }
        #endregion

        #region 设置并保存注册用户手机及验证码信息
        /// <summary>
        /// 设置并保存注册用户手机及验证码信息
        /// </summary>
        /// <param name="userMobile"></param>
        /// <param name="mobilePass"></param>
        private void SetSendMsgInfo(string userMobile, string mobilePass)
        {
            if (Request.Browser.Cookies)
            {
                HttpCookie cookie = new HttpCookie("Re_MobileInfo_Web");
                cookie.Values.Add("Re_Mobile", userMobile);
                cookie.Values.Add("Re_Pass", mobilePass);

                Response.AppendCookie(cookie);
                cookie.Expires = DateTime.Now.AddSeconds(double.Parse(m_CheckCodeTimeOut));
            }
            else
            {
                Session["Re_Mobile"] = userMobile;
                Session["Re_Pass"] = mobilePass;
                Session.Timeout = int.Parse(m_CheckCodeTimeOut) / 60;
            }
        #endregion
        }
    }
}
