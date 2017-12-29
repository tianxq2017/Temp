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
using System.Text;

namespace join.pms.web.SysAdmin
{
    public partial class CmsAnnex : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_ActionName;
        private string m_ObjID;
        private string m_SourceUrlDec;
        private string m_FuncCode;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_SqlParams;
        private DataTable m_Dt;
        private string m_TargetUrl;

        private string m_DelID;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                if (PageValidate.IsNumber(m_ObjID))
                {
                    if (m_ActionName == "del" && !string.IsNullOrEmpty(m_DelID)) DelAnnex(m_DelID);

                    GetCmsDocs(m_ObjID);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ����¼�������Զ�ѡ��", m_TargetUrl, true);
                }
            }
        }

        #region
        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);
            m_DelID = PageValidate.GetFilterSQL(Request.QueryString["DelID"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");

                this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">���ݹ���</a> &gt;&gt;���ݸ�����";
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
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

        private void GetCmsDocs(string objID)
        {
            /*
sHtml.Append("<table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"0\" class=\"lvtCol\">");
sHtml.Append("<tr>");
sHtml.Append("<td width=\"260\" height=\"25\" align=\"right\" class=\"lvtCol\">��������</td>");
sHtml.Append("<td width=\"*\" align=\"left\">���ܲ���</td>");
sHtml.Append("</tr>");
sHtml.Append("<tr>");
sHtml.Append("<td align=\"right\" height=\"25\" class=\"lvtCol\"></td>");
sHtml.Append("sHtml.Append("<td align=\"left\" width=\"*\"></td>");
sHtml.Append("</tr>");
sHtml.Append("</table>");
             siteadmin/CmsAnnex.aspx?action=del&k=104&DelID=&sourceUrl=2C936D780CBDA1FF682B8182779BCCA630DBA734086694BF
             */
            string dosID = string.Empty;
            string docsType = string.Empty;
            string docsPath = string.Empty;
            string sourceFile = string.Empty; // ԭʼ�ļ���
            StringBuilder sHtml = new StringBuilder();
            DataTable dt = new DataTable();
            m_SqlParams = "SELECT [CommID], [CmsID], [DocsType], [DocsPath], [SourceName] FROM [CMS_Docs] WHERE CmsID=" + objID;
            dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            sHtml.Append("<table width=\"100%\" border=\"0\" cellpadding=\"5\" cellspacing=\"0\" class=\"lvtCol\">");
            sHtml.Append("<tr>");
            sHtml.Append("<td width=\"260\" height=\"25\" align=\"right\" class=\"lvtCol\">��������</td>");
            sHtml.Append("<td width=\"*\" align=\"left\">&nbsp;&nbsp;���ܲ���</td>");
            sHtml.Append("</tr>");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dosID = dt.Rows[i]["CommID"].ToString();
                    docsType = dt.Rows[i]["DocsType"].ToString();
                    docsPath = dt.Rows[i]["DocsPath"].ToString();
                    sourceFile = dt.Rows[i]["SourceName"].ToString();

                    sHtml.Append("<tr>");
                    if (docsType == ".jpg" || docsType == ".bmp")
                    {
                        sHtml.Append("<td align=\"right\" height=\"25\" class=\"lvtCol\"><img src=\"" + docsPath + "\" alt=\"" + sourceFile + "\" width=\"100\" height=\"50\" /></td>");
                    }
                    else
                    {
                        sHtml.Append("<td align=\"right\" height=\"25\" class=\"lvtCol\">" + sourceFile + " </td>");
                    }
                    sHtml.Append("<td align=\"left\" width=\"*\">&nbsp;&nbsp;<a href=\"CmsAnnex.aspx?action=del&k=" + objID + "&DelID=" + dosID + "&sourceUrl=" + m_SourceUrl + "\">ɾ��</a></td>");
                    sHtml.Append("</tr>");
                }
            }

            sHtml.Append("</table>");
            this.LiteralData.Text = sHtml.ToString();
            sHtml = null;
        }

       
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void DelAnnex(string objID)
        {
            try
            {
                m_SqlParams = "DELETE FROM CMS_Docs WHERE CommID =" + objID;
                DbHelperSQL.ExecuteSql(m_SqlParams);
            }
            catch (Exception ex)
            {
                //MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
        }

        #endregion

       
    }
}