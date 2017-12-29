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
using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.web
{
    public partial class UnvCommMsg : System.Web.UI.Page
    {
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_ActionName;

        private string m_ObjID;//�ظ�MSGID
        private string m_TargetUserID;

        private string m_SqlParams;
        private DataTable m_Dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            SetPageStyle(m_UserID);

            m_ObjID = Request.QueryString["oID"];
            m_TargetUserID = Request.QueryString["uID"];
            m_ActionName = Request["action"]; // viewĬ�ϲ鿴;reply�ظ�
            if (!String.IsNullOrEmpty(m_ActionName)) 
            {
                SetExecMothods();
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

        /// <summary>
        /// ����ִ�з���
        /// </summary>
        private void SetExecMothods() 
        {
            if (m_ActionName == "view") // �鿴
            {
                this.txtMsgTitle.ReadOnly = true;
                this.txtMsgBody.ReadOnly = true;

                GetTopMsg();
            }
            if (m_ActionName == "reply") // �ظ�
            {
                this.txtMsgID.Value = m_ObjID;
                this.txtTargetUserID.Value = m_TargetUserID;

                this.txtMsgTitle.Text = "";
                this.txtMsgBody.Text = "";
                this.LiteralMsgTitle.Text = GetUserNames(m_TargetUserID);
            }
            if (m_ActionName == "add") // ������Ϣ
            {
                this.txtMsgID.Value = m_ObjID;
                this.txtTargetUserID.Value = m_TargetUserID;

                this.txtMsgTitle.Text = "";
                this.txtMsgBody.Text = "";
                this.LiteralMsgTitle.Text = "";
                this.LiteralMsgHead.Text = "��Ϣ";
                this.msgButRight.Visible = false;
            }
        }

        /// <summary>
        /// ��ȡδ�����ϵͳ��Ϣ,�����ϵ���Ϣ��ʼ
        /// </summary>
        private void GetTopMsg() 
        {
            string msgBody = string.Empty;

            m_Dt = new DataTable();
            m_SqlParams = "SELECT TOP 1 * FROM [v_SysMsg] WHERE MsgState=0 AND TargetUserID=" + m_UserID + " ORDER BY MsgID";
            try
            {
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (m_Dt.Rows.Count == 1 && m_Dt.Columns.Count > 5)
                {
                    msgBody = m_Dt.Rows[0]["MsgBody"].ToString();
                    msgBody = msgBody.Replace("<br>", "\r");
                    msgBody = msgBody.Replace("<br/>", "\r"); // [FuncNo],[FuncNa]
                    this.txtMsgID.Value = m_Dt.Rows[0]["MsgID"].ToString();
                    this.txtTargetUserID.Value = m_Dt.Rows[0]["SourceUserID"].ToString();
                    this.LiteralMsgHead.Text = m_Dt.Rows[0]["MsgTypeCN"].ToString();
                    this.LiteralMsgTitle.Text = m_Dt.Rows[0]["SourceUserName"].ToString() + "/" + m_Dt.Rows[0]["MsgTypeCN"].ToString() + "�� ʱ�䣺" + m_Dt.Rows[0]["MsgSendTime"].ToString();
                    this.txtMsgTitle.Text = m_Dt.Rows[0]["MsgTitle"].ToString();
                    this.txtMsgBody.Text = msgBody;
                    if (m_Dt.Rows[0]["MsgType"].ToString() == "2")
                    {
                        this.msgButRight.Disabled = false;
                    }
                    else { this.msgButRight.Disabled = true; }
                }
            }
            catch (Exception ex)
            {
                this.LiteralMsgTitle.Text = ex.Message;
                this.LiteralMsgHead.Text = "��Ϣ";
            }

            m_Dt = null;
        }
        /// <summary>
        /// ��ȡ�û���
        /// </summary>
        /// <param name="userIDs"></param>
        /// <returns></returns>
        private string GetUserNames(string userIDs)
        {
            StringBuilder sb = new StringBuilder();
            m_SqlParams = "SELECT [UserName] FROM [USER_BaseInfo] WHERE UserID IN(" + userIDs + ")";
            DataTable dt = new DataTable();
            dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append(dt.Rows[i]["UserName"].ToString() + "");
            }
            dt = null;
            return sb.ToString();
        }
        // �ظ�/������Ϣ
        protected void msgButLeft_ServerClick(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string MsgTitle = PageValidate.GetTrim(Request["txtMsgTitle"]);
            string MsgBody = PageValidate.Encode(PageValidate.GetTrim(Request["txtMsgBody"])); //PageValidate.GetTrim(this.txtMsgBody.Text);
            string TargetUserIDs = string.Empty;
            if (m_ActionName == "reply") { TargetUserIDs = m_TargetUserID; }
            else if (m_ActionName == "add") { TargetUserIDs = PageValidate.GetTrim(this.txtExecUserID.Value); }
            else 
            {
                m_SqlParams = "UPDATE SYS_Msg SET MsgState=1 WHERE MsgID=" + this.txtMsgID.Value;
                DbHelperSQL.ExecuteSql(m_SqlParams);
                Response.Write("<script language='javascript'>window.close();</script>");
                Response.End();
            }
            

            if (MsgTitle == "")
            {
                strErr += "��Ϣͷ����Ϊ�գ�\\n";
            }
            if (MsgBody == "")
            {
                strErr += "��Ϣ�岻��Ϊ�գ�\\n";
            }
            if (TargetUserIDs == "")
            {
                strErr += "��ѡ������ˣ�\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            try
            {
                System.Collections.Generic.List<string> list = null; ;
                string[] aryUsers = TargetUserIDs.Split(',');
                list = new System.Collections.Generic.List<string>(aryUsers.Length + 1);
                for (int i = 0; i < aryUsers.Length; i++)
                {
                    list.Add("INSERT INTO [SYS_Msg]([SourceUserID], [TargetUserID], [MsgTitle], [MsgBody], [MsgType]) VALUES(" + m_UserID + ", " + aryUsers[i] + ", '" + MsgTitle + "', '" + MsgBody + "', 2)");
                    // ������Ϣ��ʾ
                }
                if (m_ActionName == "reply") 
                {
                    list.Add("UPDATE SYS_Msg SET MsgState=1 WHERE MsgID=" + m_ObjID);
                }

                if (DbHelperSQL.ExecuteSqlTran(list) > 0)
                {
                    list = null;
                    //Response.Redirect(txtUrlParams.Value);
                    Response.Write("<script language='javascript'>window.close();</script>");//var k = new Array();k[0]=k1;k[1]=k2;window.returnValue=k;
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
        }
    }
}
