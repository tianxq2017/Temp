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

namespace join.pms.web.SysAdmin
{
    public partial class SysUserPwd : System.Web.UI.Page
    {
        private string m_UserID; // 当前登录的操作用户编号
        private string m_SqlParams;
        private string m_ActionName;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            m_ActionName = PageValidate.GetFilterSQL(Request["Action"]);
            this.LiteralNav.Text = "帐号密码设置：";
            SetPageStyle(m_UserID);

        }

        /// <summary>
        /// 身份验证 
        /// </summary>
        private void AuthenticateUser()
        {
            bool returnVa = false;
            if (Request.Browser.Cookies)
            {
                HttpCookie loginCookie = Request.Cookies["AREWEB_OC_USER_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["AREWEB_OC_USERID"] != null && !String.IsNullOrEmpty(Session["AREWEB_OC_USERID"].ToString())) { returnVa = true; m_UserID = Session["AREWEB_OC_USERID"].ToString(); }
            }

            if (!returnVa)
            {
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        // 修改密码
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string Err = "";
            string oldPwd = PageValidate.GetTrim(this.txOldPwd.Text);
            string userPwd = PageValidate.GetTrim(this.txtUserPwd.Text);
            string userPwdRe = PageValidate.GetTrim(this.txtUserPwdRe.Text);
            if (String.IsNullOrEmpty(oldPwd))
            {
                Err += "请输入旧密码！\\n";
            }
            if (String.IsNullOrEmpty(userPwd))
            {
                Err += "新密码不能为空！\\n";
            }
            if (String.Compare(userPwd, userPwdRe) != 0)
            {
                Err += "您的新密码和确认密码不一致，请重新输入！\\n";
            }
            if (Err != "")
            {
                MessageBox.Show(this, Err);
                return;
            }

            try
            {
                oldPwd = DESEncrypt.Encrypt(oldPwd);
                userPwd = DESEncrypt.Encrypt(userPwd);
                Err = join.pms.dal.CommPage.GetSingleValue("SELECT UserPassword FROM USER_BaseInfo WHERE UserID=" + m_UserID);
                if (oldPwd == Err)
                {
                    m_SqlParams = "UPDATE [USER_BaseInfo] SET [UserPassword]='" + userPwd + "' WHERE UserID= " + m_UserID;
                    if (DbHelperSQL.ExecuteSql(m_SqlParams) == 1)
                    {
                        if (!string.IsNullOrEmpty(m_ActionName) && m_ActionName == "Ysl")
                        {
                            Response.Write(" <script>alert('提示信息：您的登录密码成功修改，请退出系统，用新密码重新登录！') ;window.location.href='/MainDesk.aspx';</script>");
                        }
                        else
                        {
                            Response.Write(" <script>alert('提示信息：您的登录密码成功修改，请退出系统，用新密码重新登录！') ;window.location.href='/MainDesk.aspx';</script>");
                        }
                    }
                    else
                    {
                        Response.Write(" <script>alert('操作失败：修改密码出错！') ;</script>");
                        MessageBox.Show(this, "操作失败：修改密码出错！");
                    }
                }
                else
                {
                    Response.Write(" <script>alert('操作提示：操作失败，原有密码输入有误！');window.location.href='/MainDesk.aspx';</script>");
                }
            }
            catch { MessageBox.Show(this, "操作失败：修改密码出错！"); }
        }
    }
}
