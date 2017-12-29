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

namespace join.pms.web.SysAdmin
{
    public partial class MsgSend : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;

        private string m_ActionName;
        private string m_ObjID;
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_SqlParams;
        private string m_TargetUrl;

        private DataTable m_Dt;
        private string m_FuncCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                if (String.IsNullOrEmpty(m_ActionName))
                {
                    Response.Write("�Ƿ����ʣ���������ֹ��");
                    Response.End();
                }
                else
                {
                    SetOpratetionAction("������Ϣ");
                }
            }
        }

        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
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

        #region �����֤��

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

        /// <summary>
        /// ���ò�����Ϊ
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("�Ƿ����ʣ���������ֹ��");
                    Response.End();
                }
            }
            else
            {
                if (!PageValidate.IsNumber(m_ObjID))
                {
                    m_ObjID = m_ObjID.Replace("s", ",");
                }
            }
            switch (m_ActionName)
            {
                case "add": // ����
                    funcName = "";
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }

    
        #endregion



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string returnMsg = string.Empty;
            string msgBody = string.Empty; // txtMsgBody
            string strErr = string.Empty;
            string ContactTel = PageValidate.GetTrim(this.txtMsgMobile.Text);
            StringBuilder msgInfo = new StringBuilder();
            msgBody = PageValidate.GetTrim(this.txtMsgBody.Text);

            if (String.IsNullOrEmpty(ContactTel))
            {
                strErr += "�������ֻ����벻��Ϊ�գ�\\n";
            }

            ContactTel = ContactTel.Replace("��", ","); // �滻ȫ�Ƿ���
            ContactTel = ContactTel.Replace(" ", "");//�滻�ո�
            ContactTel = ContactTel.Replace(";", ",");//�滻;
            string[] aryTel = ContactTel.Split(',');
            if (aryTel.Length > 20)
            {
                strErr += "���ֻ֧��20������Ⱥ����\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            returnMsg = SendMsg.SendMsgByModem(ContactTel, msgBody);
            if (!string.IsNullOrEmpty(returnMsg) && int.Parse(returnMsg) > 0)
            {
                msgInfo.Append("���͵�[ " + ContactTel + " ]��Ϣ�ɹ����뵽���Ͷ��У���������Ϊ��[ " + msgBody + " ]<br/>");
            }
            else
            {
                msgInfo.Append("���͵�[ " + ContactTel + " ]ʧ�ܣ�" + returnMsg + "��<br/>");
            }
            this.LiteralResults.Text = msgInfo.ToString();
        }

    }
}
