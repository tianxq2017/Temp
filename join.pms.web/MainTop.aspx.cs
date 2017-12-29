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

using System.Globalization;
using System.Data.SqlClient;
using UNV.Comm.DataBase;

namespace join.pms.web
{
    public partial class MainTop : System.Web.UI.Page
    {
         protected string m_UserID; // ��ǰ��¼�Ĳ����û����
        protected string m_CssFile;

        protected void Page_Load(object sender, EventArgs e)
        {
             AuthenticateUser();

            string sParams = Request.QueryString["action"];
            if (!String.IsNullOrEmpty(sParams))
            {
                if (sParams == "Logout") 
                {
                    LogoutSys();
                }
                else if (sParams == "SignIN")
                {
                    //UserSignIN();
                    GetUserInfo(m_UserID);
                }
                else
                { 
                    GetUserInfo(m_UserID); 
                }
            }
            else 
            {
                GetUserInfo(m_UserID);
            }
        }
        /// <summary>
        /// �����֤
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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/loginTemp.aspx';</script>");
                Response.End();
            }
        }
        /// <summary>
        /// �˳�ϵͳ
        /// </summary>
        private void LogoutSys()
        {
            DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "�˳�", "�û��� " + DateTime.Now.ToString() + " �˳�ϵͳ");

            Session["AREWEB_OC_USERID"] = null;
            Session.Clear();
            Session.Abandon();

            HttpCookie loginCookie = Request.Cookies["AREWEB_OC_USER_YSL"];
            if (loginCookie != null)
            {
                loginCookie.Values["UserID"] = null;
                loginCookie.Expires = DateTime.Now.AddDays(-1);
                loginCookie = null;
            }

            Response.Cookies["AREWEB_OC_USER_YSL"].Expires = DateTime.Now.AddDays(-1);
            Response.Write("<script language='javascript'>alert('�ɹ��˳�ϵͳ��Ϊȷ����ȫ�˳�����ر����д򿪵�ϵͳ���ڣ�');parent.location.href='/loginTemp.aspx';</script>");
            Response.End();
        }

        /// <summary>
        /// ��ȡ����ʾ��ǰ��¼���û���Ϣ
        /// </summary>
        /// <param name="userID"></param>
        private void GetUserInfo(string userID)
        {

            // ��ǰ�û���Ϣ 
            string sqlParams = "SELECT [UserAccount], [UserName], [DeptName],[CssStyle] FROM [v_UserList] WHERE UserID=" + userID;
            System.Data.SqlClient.SqlDataReader sdr = null;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    SetPageStyle(sdr["CssStyle"].ToString());
                    this.LiteralUserInfo.Text = "<li>��ǰ��¼�ʺ�[ " + sdr["UserAccount"].ToString() + " ]&nbsp;&nbsp;&nbsp;�û�[ " + sdr["UserName"].ToString() + " ]&nbsp;&nbsp;&nbsp;��������[ " + sdr["DeptName"].ToString() + " ]</li>";
                }
                sdr.Close();
                // ��ȡ������Ϣ
                sqlParams = "SELECT COUNT(*) FROM SYS_Msg WHERE TargetUserID=" + userID + " AND MsgState=0";
                string msgCount = DbHelperSQL.GetSingle(sqlParams).ToString();
            }
            catch { if (sdr != null) sdr.Close(); }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/index.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

    }
}
