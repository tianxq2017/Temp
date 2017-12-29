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
    public partial class SysUserUpd : System.Web.UI.Page
    {
        private string m_UserID; // 当前登录的操作用户编号
        private string m_SqlParams;
        private string m_ActionName;
        private string m_ObjID;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);
            this.LiteralNav.Text = "用户信息设置";
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

        // 修改信息
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string Err = "";
            string userInfo = PageValidate.GetTrim(this.txtInfo.Text);
            if (String.IsNullOrEmpty(userInfo))
            {
                Err += "请输入信息！\\n";
            }
            if (m_ActionName == "tel" && System.Text.RegularExpressions.Regex.IsMatch(userInfo, @"^[0-9]*$") == false)
            {
                Err += "请输入正确的电话号码，注意输入法切换是否正确！\\n";
            }
            //else if (m_ActionName == "wx" && System.Text.RegularExpressions.Regex.IsMatch(userInfo, @"^[a-zA-Z_-][A-Za-z0-9_-]*$") == false)
            //{
            //    Err += "请输入正确的微信号，以字母开头，由字母和数字、下划线及减号组成！\\n";
            //}
            else if (m_ActionName == "wx" && string.IsNullOrEmpty(userInfo))
            {
                Err += "请输入微信号！\\n";//微信也可以是手机号、QQ号等，不一定都以字母开头 by ysl 2016/02/05
            }
            if (Err != "")
            {
                MessageBox.Show(this, Err);
                return;
            }

            try
            {
                if (m_ActionName == "tel" && System.Text.RegularExpressions.Regex.IsMatch(userInfo, @"^[0-9]*$"))
                {

                    m_SqlParams = " UPDATE SYS_Sign SET  SignPhone = '" + userInfo + "' WHERE SignID=" + m_ObjID;
                }
                else if (m_ActionName == "wx")
                {
                    m_SqlParams = " UPDATE SYS_Sign SET  SignWX = '" + userInfo + "' WHERE SignID=" + m_ObjID;
                }
                if (DbHelperSQL.ExecuteSql(m_SqlParams) == 1)
                {

                    Response.Write(" <script>alert('提示信息：您的信息修改成功！') ;window.location.href='/SysAdmin/SysUserInfo.aspx';</script>");

                }
                else
                {
                    Response.Write(" <script>alert('操作失败：修改密码出错！') ;</script>");
                    MessageBox.Show(this, "操作失败：修改密码出错！");
                }
            }
            catch { MessageBox.Show(this, "操作失败：修改出错！"); }
        }
    }
}
