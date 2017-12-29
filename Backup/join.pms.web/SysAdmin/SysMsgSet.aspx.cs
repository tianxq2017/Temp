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
    public partial class SysMsgSet : System.Web.UI.Page
    {
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_SqlParams;
        private string m_ActionName;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();

            this.LiteralNav.Text = "ϵͳ��ʾ���ã�";
            object closeMsg = DbHelperSQL.GetSingle("SELECT ParaValue FROM [SYS_Params] WHERE ParaCate=6 AND UserID=" + m_UserID + " AND ParaCode='0'");
            if (closeMsg != null && closeMsg.ToString() == "1") this.cbxCloseMsg.Checked = true;
            SetPageStyle(m_UserID);

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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Default.shtml?action=closewindow';</script>");
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
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        // �޸�����
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string Err = "";
            string clearMsg = PageValidate.GetTrim(Request["cbxClearMsg"]);
            string closeMsg = PageValidate.GetTrim(Request["cbxCloseMsg"]);

            try
            {
                // SELECT ParaValue FROM [SYS_Params] WHERE ParaCate=6 AND ParaCode='0'
                if (!string.IsNullOrEmpty(clearMsg) && clearMsg == "1") {
                    DbHelperSQL.ExecuteSql("UPDATE SYS_Msg SET MsgState=1 WHERE TargetUserID= " + m_UserID + " AND MsgState!=1");
                }
                if (string.IsNullOrEmpty(closeMsg)) closeMsg = "0";

                object closeMsgInit = DbHelperSQL.GetSingle("SELECT ParaValue FROM [SYS_Params] WHERE ParaCate=6 AND UserID=" + m_UserID + " AND ParaCode='0'");
                if (closeMsgInit == null) { m_SqlParams = "INSERT INTO [SYS_Params](ParaCode,ParaValue,ParaCate,UserID,ParaMemo) VALUES('0', '" + closeMsg + "', 6, " + m_UserID + ", 'ϵͳ��ʾ����')"; }
                else { m_SqlParams = "UPDATE [SYS_Params] SET [ParaValue]='" + closeMsg + "' WHERE ParaCate=6 AND UserID=" + m_UserID + " AND ParaCode='0'"; }
                // 0��ʾ,1,����ʾ
                
                if (DbHelperSQL.ExecuteSql(m_SqlParams) == 1)
                {
                    Response.Write(" <script>alert('��ʾ��Ϣ��ϵͳ��ʾ���óɹ��޸ģ�') ;window.location.href='/MainDesk.aspx';</script>");
                }
                else
                {
                    Response.Write(" <script>alert('����ʧ�ܣ�ϵͳ��ʾ����ʧ�ܣ�') ;</script>");
                }
            }
            catch { Response.Write(" <script>alert('����ʧ�ܣ�ϵͳ��ʾ����ʧ�ܣ�') ;</script>"); }
        }
    }
}
