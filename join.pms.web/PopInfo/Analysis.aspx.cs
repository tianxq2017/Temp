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

using System.Data.SqlClient;
using UNV.Comm.DataBase;
using UNV.Comm.Web;
using System.Text;
using System.Xml;
using System.IO;

namespace AreWeb.OnlineCertificate.PopInfo
{
    public partial class Analysis : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        public string m_TargetUrl;
        private string m_FuncCode;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_SqlParams;

        private string m_StatusVal;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            //ValidateParams();
            SetOpration(m_FuncCode);

            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                
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
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_SourceUrl = Request.QueryString["sourceUrl"];
            if (!string.IsNullOrEmpty(m_SourceUrl))
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
        }
        // -
        private void SetOpration(string funcNo)
        {
            string funcName = "�������ݷ����ȶ�";
            try
            {

                //��ȡ��ǰִ��״̬ ���ݷ����ȶԣ�0,Ĭ�ϣ�1,ִ��
                m_SqlParams = "SELECT ParaValue FROM SYS_Params WHERE ParaCate=7 AND ParaCode='7001'";
                m_StatusVal = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                if (m_StatusVal == "0") {
                    this.LabelMsg.Text = "��ǰ״̬�������С���"; 
                } 
                else {
                    this.LabelMsg.Text = "��ǰ״̬������ִ�����ݷ����ȶԡ���"; 
                }
            }
            catch 
            {
                this.LabelMsg.Text = "����ʧ�ܣ���δ�������ݱȶԲ�����";
                return;
            }

            this.LiteralNav.Text = "������ҳ  &gt;&gt; " + funcName + "��";
        }

        protected void butAnalysis_Click(object sender, EventArgs e)
        {
            if (m_StatusVal == "0")
            {
                this.LabelMsg.Text = "��ǰ״̬����ʼִ�бȶԷ�������";
                try
                {

                    //��ȡ��ǰִ��״̬ ���ݷ����ȶԣ�0,Ĭ�ϣ�1,ִ��
                    m_SqlParams = "UPDATE SYS_Params SET ParaValue='1' WHERE ParaCate=7 AND ParaCode='7001'";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    this.LabelMsg.Text = "��ǰ״̬���ɹ�����������ͱȶԷ���ָ��� <br/>������������С���������̽���������������Сʱ���Һ󡭡�<br/>�����Ժȱ��������Ϣһ���ٷ���ϵͳ�鿴���������";
                }
                catch
                {
                    this.LabelMsg.Text = "����ʧ�ܣ�����������ͷ���ָ�����";
                }
            }
            else
            {
                this.LabelMsg.Text = "��ǰ״̬������ִ�����ݷ����ȶԡ��� �ڱ��η���δ���֮ǰ����ֹ�����ظ�������";
            }

        }

        
    }
}
