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

using UNV.Comm.Web;
using UNV.Comm.DataBase;

using join.pms.dal;

namespace join.pms.web
{
    public partial class loginTemp : UNV.Comm.Web.PageBase
    {
        private string m_SqlParams;
        private DataTable m_Dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Request.ValidateInput();  

        }

        // 登录系统 
        protected void ButLogin_Click(object sender, EventArgs e)
        {
            string returnVa = string.Empty;
            string userID = string.Empty;
            string validFlag = string.Empty;
            string userAcc = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.loginName.Value));
            string userPwd = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.loginPass.Value));
            string cerCode = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtCerCode.Value));
            // 验证码 Start
            string strErr = string.Empty;
            string identityCode = PageValidate.GetTrim(Request["txtCheckCode"]);
            if (string.IsNullOrEmpty(userAcc)) { 
                strErr += "请输入账号！\\n"; } 

            if (identityCode == "")
            {
                strErr += "请输入验证码！\\n";
            }
            if (String.IsNullOrEmpty(identityCode) || Session["CheckCode"] == null || identityCode.Trim().ToLower() != Session["CheckCode"].ToString().ToLower())
            {
                strErr += "验证码输入错误，请重新输入！\\n";
            }
            if (string.IsNullOrEmpty(cerCode))
            {
                strErr += "请输入临时认证码！\\n";
            }
            else {
                if (cerCode != GetCerCode()) strErr += "临时认证码错误！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            // 验证码 End 
            if (!String.IsNullOrEmpty(userAcc) && !String.IsNullOrEmpty(userPwd))
            {
                try
                {
                    /*
                    if (keyExistInfo == "uKeyCheckedBySys-H1lqB4DmsK4pQMqyfTMfaNBtxjQDahFF") {
                        //if (ReadKeyInfo()) {
                        //    m_SqlParams = "SELECT UserID FROM USER_BaseInfo WHERE ValidFlag=1 AND uKeyID='" + m_UkeyID + "' AND uKeyCode='"+m_UkeyVale+"'";
                        //    userID = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                        //    if (!String.IsNullOrEmpty(userID) && PageValidate.IsNumber(userID))
                        //    {
                        //        SetUserLoginInfo(userID);
                        //        DbHelperSQL.SetSysLog(userID, Request.UserHostAddress, "登录", "用户[" + userAcc + "]于 " + DateTime.Now.ToString() + " 使用UKey登录系统");
                        //        DbHelperSQL.ExecuteSql("UPDATE [USER_BaseInfo] SET [UserLastLoginTime]=GetDate(),UserLoginNum=UserLoginNum+1 WHERE UserID=" + userID);

                        //        Response.Redirect("/YslRyLxJKc9P8xcVA6KVWDqwusnGNxCIHvckPEBEr2Eched9eA0AQF6ryC6f7HWx0R." + this.m_FileExt, true);
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show(this, "登录失败，请检查您的帐号是否有效或咨询网站管理员！");
                        //    }
                        //}
                        
                    } 
                    else {
                        userPwd = DESEncrypt.Encrypt(userPwd);
                        m_SqlParams = "SELECT UserID FROM USER_BaseInfo WHERE UserAccount='" + userAcc + "' AND UserPassword='" + userPwd + "' AND ValidFlag=1";
                        userID = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                        if (!String.IsNullOrEmpty(userID) && PageValidate.IsNumber(userID))
                        {
                            SetUserLoginInfo(userID);
                            DbHelperSQL.SetSysLog(userID, Request.UserHostAddress, "登录", "用户[" + userAcc + "]于 " + DateTime.Now.ToString() + " 登录系统");
                            DbHelperSQL.ExecuteSql("UPDATE [USER_BaseInfo] SET [UserLastLoginTime]=GetDate(),UserLoginNum=UserLoginNum+1 WHERE UserID=" + userID);

                            Response.Redirect("/YslRyLxJKc9P8xcVA6KVWDqwusnGNxCIHvckPEBEr2Eched9eA0AQF6ryC6f7HWx0R." + this.m_FileExt, true);
                        }
                        else
                        {
                            MessageBox.Show(this, "登录失败，请检查您的帐号是否有效或咨询网站管理员！");
                        }
                    }
                    */
                    userPwd = DESEncrypt.Encrypt(userPwd);
                   
                    m_SqlParams = "SELECT UserID FROM USER_BaseInfo WHERE UserAccount='" + userAcc + "' AND UserPassword='" + userPwd + "' AND ValidFlag=1";
                    object uID = DbHelperSQL.GetSingle(m_SqlParams);
                    if (uID != null)
                    {
                        userID = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                        if (!String.IsNullOrEmpty(userID) && PageValidate.IsNumber(userID))
                        {
                            SetUserLoginInfo(userID);
                            DbHelperSQL.SetSysLog(userID, Request.UserHostAddress, "登录", "用户[" + userAcc + "]于 " + DateTime.Now.ToString() + " 登录系统");
                            DbHelperSQL.ExecuteSql("UPDATE [USER_BaseInfo] SET [UserLastLoginTime]=GetDate(),UserLoginNum=UserLoginNum+1 WHERE UserID=" + userID);

                            Response.Redirect("/MainFrame.aspx", true);
                        }
                        else
                        {
                            MessageBox.Show(this, "登录失败，请检查您的帐号是否有效或咨询网站管理员！");
                        }
                    }
                    else { MessageBox.Show(this, "登录失败，请确认您输入的信息存在！"); }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "登录失败：" + ex.Message + ex.Source + "<br>" + m_SqlParams);
                }
            }
            else
            {
                MessageBox.Show(this, "帐号或密码不能为空！");
            }
        }

        /// <summary>
        /// 获取临时认证码
        /// </summary>
        /// <returns></returns>
        private string GetCerCode()
        {
            try
            {
                m_SqlParams = "SELECT ParaValue FROM SYS_Params WHERE ParaCate=8 AND ParaCode='8001'";
               
                //return DbHelperSQL.GetSingle(m_SqlParams).ToString();
                return join.pms.dal.CommPage.GetSingleVal(m_SqlParams);
            }
            catch { return ""; }
        }

        /// <summary>
        /// 设置并保存用户登陆信息
        /// </summary>
        /// <param name="userID"></param>
        private void SetUserLoginInfo(string userID)
        {
            //设置用户登陆信息cookie
            if (Request.Browser.Cookies)
            {
                HttpCookie cookie = new HttpCookie("AREWEB_OC_USER_YSL");
                cookie.Expires = DateTime.Now.AddHours(4); //cookie过期时间
                cookie.Values.Add("UserID", userID);
                Response.AppendCookie(cookie);
            }
            else
            {
                Session["AREWEB_OC_USERID"] = userID;
            }
        }
    }
}
