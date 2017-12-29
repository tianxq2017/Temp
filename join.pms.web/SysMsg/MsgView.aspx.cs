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


namespace join.pms.web.SysMsg
{
    public partial class MsgView : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_ActionName;
        private string m_ObjID;
        private string m_FuncNo;

        private string m_TargetUrl;
        private string m_SqlParams;
        private DataTable m_Dt;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                SetOpratetionAction("ϵͳ��Ѷ");
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
                //m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                m_FuncNo = StringProcess.AnalysisParas(m_SourceUrl, "FuncNo");
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

        #region ҳ���״μ���ʱ��ʾ����Ϣ
        /// <summary>
        /// ���ò�����Ϊ add����,edit�༭,delɾ��,4�鿴,5���,6�����ɫ ��
        /// </summary>
        /// <param name="oprateName">��������</param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID) || !PageValidate.IsNumber(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("�Ƿ����ʣ���������ֹ��");
                    Response.End();
                }
            }

            switch (m_ActionName)
            {
                case "view": // �鿴
                    funcName = "��Ϣ�鿴";
                    ShowModInfo(m_ObjID);
                    break;
                case "edit": // �༭
                    funcName = "�޸��û���Ϣ";
                    //ShowModInfo(m_ObjID);
                    break;
                default:
                    if (Request.UrlReferrer != null) Response.Redirect(Request.UrlReferrer.ToString());
                    break;
            }

            this.LiteralNav.Text = "������ҳ &gt;&gt; " + oprateName + " &gt;&gt; " + funcName + "��";
        }

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            string targetUserID = string.Empty;
            StringBuilder sHtml = new StringBuilder();
            try
            {
                m_SqlParams = "SELECT * FROM [v_SysMsg] WHERE MsgID=" + objID;
                m_Dt = new DataTable();
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                
                if (m_Dt.Rows.Count == 1)
                {
                    targetUserID = m_Dt.Rows[0]["SourceUserID"].ToString();
                        // ���� 
                    sHtml.Append("<table width=\"95%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
                    sHtml.Append("<td width=\"20\" height=\"30\" align=\"left\">&nbsp;</td>");
                    sHtml.Append("<td width=\"30\" align=\"center\" bgcolor=\"#F4FAFA\" class=\"fb\"><img src=\"/images/chakan.gif\"  /></td>");
                    sHtml.Append("<td align=\"left\" bgcolor=\"#F4FAFA\" class=\"page\"><span class=\"fb\"><strong>" + m_Dt.Rows[0]["MsgTitle"].ToString() + "</strong></span></td>");
                    sHtml.Append("</tr></table>");

                    // ���ߡ�����ʱ�䡢ҳ�湦��
                    sHtml.Append("<table width=\"95%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" ><tr >");
                    sHtml.Append("<td width=\"20\" height=\"30\" align=\"left\">&nbsp;</td>");
                    sHtml.Append("<td align=\"left\"  style=\"border-top:1px solid #cccccc;\" >");
                    sHtml.Append("�����壺<a class=\"fh \" href=\"javascript:fontZoom(16)\">��</a>&nbsp;<a class=\"fh \" href=\"javascript:fontZoom(14)\">��</a>&nbsp;<a class=\"fh \" href=\"javascript:fontZoom(12)\">С</a>�� ��<a class=\"fh \" href=\"javascript:this.print()\">��ӡ</a>��</td>");
                    sHtml.Append("</tr></table><br/>");
                    // ����
                    sHtml.Append("<table width=\"95%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" ><tr>");
                    sHtml.Append("<td width=\"20\" align=\"left\">&nbsp;</td>");
                    sHtml.Append("<td align=\"left\" class=\"fb\" id=\"fontzoom\" height=\"280\" valign=\"top\">");

                    if (m_FuncNo == "0302") { sHtml.Append("�����ˣ�" + m_Dt.Rows[0]["SourceUserName"].ToString() + "<br>"); }
                    else { sHtml.Append("�����ˣ�" + m_Dt.Rows[0]["TargetUserName"].ToString() + "<br>"); }
                    sHtml.Append("��Ϣͷ��" + m_Dt.Rows[0]["MsgTitle"].ToString() + "<br>");
                    sHtml.Append("��Ϣ�壺<br/>" + PageValidate.Decode(m_Dt.Rows[0]["MsgBody"].ToString()) + "<br/>");
                    //sHtml.Append("��Ϣ������" + GetMsgDocs(m_Dt.Rows[0]["DocID"].ToString()) + "<br>");
                    //sHtml.Append("��Ϣ���ͣ�" + m_Dt.Rows[0]["MsgTypeCN"].ToString() + "<br>");
                    //sHtml.Append("��Ϣ״̬��" + m_Dt.Rows[0]["MsgStateCN"].ToString() + "<br>");
                    sHtml.Append("ʱ�䣺" + m_Dt.Rows[0]["MsgSendTime"].ToString() + "<br>");
                    sHtml.Append("<br><div align=\"left\"><input type=\"button\" name=\"ButBackPage\" value=\"�� ���� ��\" id=\"ButBackPage\" onclick=\"javascript:window.location.href='" + m_TargetUrl + "';\" class=\"submit6\" /></div>");

                    sHtml.Append("</td></tr></table><br/><br/>");
   
                    m_Dt = null;
                }
                m_Dt = null;

                if (targetUserID == m_UserID) {
                    DbHelperSQL.ExecuteSql("UPDATE SYS_Msg SET MsgState=1 WHERE MsgID=" + objID);
                }
                
            }
            catch (Exception ex)
            {
                sHtml.Append(ex.Message);
            }
            this.LiteralMsgView.Text = sHtml.ToString();
        }

        /// <summary>
        /// ��ȡ��Ϣ����
        /// </summary>
        /// <returns></returns>
        private string GetMsgDocs(string docsID) {
            StringBuilder sHtml = new StringBuilder();
            if (!string.IsNullOrEmpty(docsID))
            {
                docsID = docsID.Replace("s",",");
                m_SqlParams = "SELECT DocsPath, SourceName FROM SYS_MsgDocs WHERE DocID IN(" + docsID+")";
               DataTable m_Dt = new DataTable();
                
                try
                {
                    m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                    for (int i = 0; i < m_Dt.Rows.Count; i++) {
                        sHtml.Append("<a href=\"" + m_Dt.Rows[i][0].ToString() + "\" target=\"_blank\">" + m_Dt.Rows[i][1].ToString() + "</a> &nbsp;&nbsp;&nbsp;&nbsp;");
                    }
                    
                }
                catch (Exception ex)
                {
                    sHtml.Append(ex.Message);
                }
                m_Dt = null;
            }
            else { sHtml.Append("û�и���"); }

            return sHtml.ToString();
        }
        #endregion
    }
}
