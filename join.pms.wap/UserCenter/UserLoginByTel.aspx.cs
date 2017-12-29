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
using join.pms.dal;

using System.Text;
using System.Data.SqlClient;
namespace join.pms.wap.UserCenter
{
    public partial class UserLoginByTel : UNV.Comm.Web.PageBase
    {
        private string m_RawUrl; // 返回的地址
        private string m_PersonID;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateParams();
            this.Uc_PageTop1.GetSysMenu("群众登录");
            if (AuthenticateUser())
            {
                //string targetUrl = string.Empty;
                //if (this.m_UserType == "1")
                //{ targetUrl = "/stu/index." + this.m_FileExt + ""; }
                //else { targetUrl = "/com/index." + this.m_FileExt + ""; }
                MessageBox.ShowAndRedirect(this.Page, "", "/OC." + this.m_FileExt, false, true);
            }
            else
            {
                // 加载验证用户输入的脚本方法  this.ButtonOK.Attributes.Add("onclick", "return ValidateUsers();");             
            }
        }
        #region 身份验证 验证接受的参数 设置页头信息
        /// <summary>
        /// 身份验证
        /// </summary>
        private bool AuthenticateUser()
        {
            bool returnVa = false;
            if (Request.Browser.Cookies)
            {
                HttpCookie loginCookie = Request.Cookies["AREWEB_OC_PUBSVRS_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_PersonID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["UserID"] != null && !String.IsNullOrEmpty(Session["UserID"].ToString())) { returnVa = true; m_PersonID = Session["UserID"].ToString(); }
            }
            return returnVa;
        }
        /// <summary>
        /// 验证接受的参数
        /// </summary>
        private void ValidateParams()
        {
            m_RawUrl = PageValidate.GetTrim(Request.QueryString["c"]);
            if (!string.IsNullOrEmpty(m_RawUrl))
            {
                m_RawUrl = DESEncrypt.Decrypt(m_RawUrl);
            }
        }

        #endregion

        #region 设置并保存用户登陆信息
        /// <summary>
        /// 设置并保存用户登陆信息
        /// </summary>
        /// <param name="userID"></param>
        private void SetUserLoginIn(string userInfo)
        {
            if (Request.Browser.Cookies)
            {
                HttpCookie cookie = new HttpCookie("AREWEB_OC_PUBSVRS_YSL");
                cookie.Values.Add("UserID", userInfo);
                Response.AppendCookie(cookie);
                cookie.Expires = System.DateTime.Now.AddHours(150000);
            }
            else
            {
                Session["UserID"] = userInfo;
            }
        }
        #endregion

        #region 群众登陆 by账户
        /// <summary>
        /// 群众登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string personID = string.Empty;
            string sqlParams = string.Empty;
            string loginIP = Request.UserHostAddress;
            string targetUrl = string.Empty;

             string PersonTel = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserMobile.Text));
             string PhonePass = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserSmsCode.Value));

            string strErr = string.Empty;

            if (String.IsNullOrEmpty(PersonTel))
            {
                strErr += "请输入手机号码！\\n";
            }
            if (!CommPage.IsAllowLogin(PersonTel, loginIP)) { strErr += "您尝试登陆的次数超过限制次数，请联系系统管理员！\\n"; }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            #region 手机验证
            string strUserMobile = "", strPhonePass = string.Empty;
            if (Request.Browser.Cookies)
            {
                HttpCookie reMobileInfo = Request.Cookies["Re_MobileInfo_Web"];
                if (reMobileInfo != null && !String.IsNullOrEmpty(reMobileInfo.Values["Re_Mobile"].ToString()))
                {
                    strUserMobile = reMobileInfo.Values["Re_Mobile"].ToString();
                    strPhonePass = reMobileInfo.Values["Re_Pass"].ToString();
                }
            }
            else
            {
                if (Session["Re_Mobile"] != null && !String.IsNullOrEmpty(Session["Re_Mobile"].ToString()))
                {
                    strUserMobile = Session["Re_Mobile"].ToString();
                    strPhonePass = Session["Re_Pass"].ToString();
                }
            }
            if (String.IsNullOrEmpty(PhonePass) || strPhonePass == null || PersonTel != strUserMobile || PhonePass != strPhonePass)
            {
                strErr += "手机验证码输入错误，请重新输入！\\n";
            }
            #endregion

            SqlDataReader sdr = null;

            try
            {
                sqlParams = "SELECT PersonID,PersonAcc FROM BIZ_Persons WHERE PersonTel='" + PersonTel + "' AND Attribs!=0";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        personID = sdr["PersonID"].ToString();
                    }
                    SetUserLoginIn(personID);
                    //DbHelperSQL.SetSysLog(personID, Request.UserHostAddress, "登录", "内部平台-用户[" + userAcc + "]于 " + DateTime.Now.ToString() + " 登录系统");
                    DbHelperSQL.ExecuteSql("UPDATE BIZ_Persons SET UserLastLoginTime=GetDate(),UserLoginNum=UserLoginNum+1,UserOnlineStatus=1 WHERE PersonID=" + personID);

                    if (!string.IsNullOrEmpty(this.m_RawUrl))
                    {
                        targetUrl = this.m_RawUrl;
                    }
                    else
                    { targetUrl = "/OC." + this.m_FileExt; }
                    MessageBox.ShowAndRedirect(this.Page, "", targetUrl, false, true);
                }
                else
                {
                    // 记录尝试登陆次数
                    CommPage.SetSysTryLogin(PersonTel, loginIP, "失败");
                    MessageBox.Show(this, "登录失败，请检查您的帐号是否有效或咨询网站管理员！");
                }
                sdr.Close(); sdr.Dispose();
            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                MessageBox.Show(this, "登录失败，请联系系统管理员！");
            }
        }
        #endregion
    }
}
