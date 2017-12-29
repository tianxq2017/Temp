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
using System.Xml;
using System.IO;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;

namespace join.pms.web.BizInfo
{
    public partial class BizWorkFlows : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        public string m_TargetUrl;

        private string m_FuncCode;
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_ObjID;

        private DataTable m_Dt;
        private string m_SqlParams;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                SetOpration(m_FuncCode);
            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                //string cssFile = DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                //if (string.IsNullOrEmpty(cssFile)) 
                string cssFile = "/css/inidex.css";

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

        /// <summary>
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            if (!string.IsNullOrEmpty(m_SourceUrl))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }
        }

        private void SetOpration(string funcNo)
        {
            if (!string.IsNullOrEmpty(m_ObjID)) {
                if (!PageValidate.IsNumber(m_ObjID))
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ���ù��ܲ�֧�ֶ�ѡ����,��ѡ������ҵ������鿴�������̣�", m_TargetUrl, true);
                }
                else 
                {
                    this.LiteralNav.Text = "��ʼҳ  &gt;&gt; " + CommPage.GetAllBizName(m_FuncCode) + " &gt;�������̲鿴 ��";

                    //GetBizFlows(m_ObjID);

                    join.pms.dal.BizWorkFlows flows = new join.pms.dal.BizWorkFlows();
                    flows.BizID = m_ObjID;
                    flows.BizCode = this.m_FuncCode;
                    this.LiteralFlows.Text = flows.GetBizFlows();
                    flows = null;
                }
            }
        }

    }
}

