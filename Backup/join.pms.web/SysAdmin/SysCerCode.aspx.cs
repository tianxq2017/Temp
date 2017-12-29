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

namespace join.pms.web.SysAdmin
{
    public partial class SysCerCode : System.Web.UI.Page
    {
        private string m_UserID; // 当前登录的操作用户编号
        private string m_SqlParams;
        private string m_ActionName;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            m_ActionName = Request["Action"];
            this.LiteralNav.Text = "临时认证码设置：";
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
            string oldSysCode = string.Empty;
            string oldCode = PageValidate.GetTrim(this.txOldCode.Text);
            string userCode = PageValidate.GetTrim(this.txtUserCode.Text);
            string userCodeRe = PageValidate.GetTrim(this.txtUserCodeRe.Text);
            if (String.IsNullOrEmpty(oldCode))
            {
                Err += "请输入旧认证码！\\n";
            }
            if (String.IsNullOrEmpty(userCode))
            {
                Err += "新认证码不能为空！\\n";
            }
            if (String.Compare(userCode, userCodeRe) != 0)
            {
                Err += "您的新认证码和确认认证码不一致，请重新输入！\\n";
            }
            if (Err != "")
            {
                MessageBox.Show(this, Err);
                return;
            }

            try
            {
                oldSysCode = DbHelperSQL.GetSingle("SELECT ParaValue FROM SYS_Params WHERE ParaCate=8 AND ParaCode='8001'").ToString();
                if (oldCode != oldSysCode) 
                {
                    MessageBox.Show(this, "您输入的旧认证码不正确；请输入正确的系统认证码才能进行变更操作！");
                } 
                else {
                    m_SqlParams = "UPDATE SYS_Params SET ParaValue='" + userCode + "' WHERE ParaCate=8 AND ParaCode='8001'";
                    if (DbHelperSQL.ExecuteSql(m_SqlParams) == 1)
                    {
                        Response.Write(" <script>alert('提示信息：您的认证码成功修改！') ;window.location.href='/MainDesk.aspx';</script>");
                    }
                    else
                    {
                        Response.Write(" <script>alert('操作失败：修改认证码出错！') ;</script>");
                    }
                }
            }
            catch { MessageBox.Show(this, "操作失败：修改认证码出错！"); }
        }

        
    }
}
