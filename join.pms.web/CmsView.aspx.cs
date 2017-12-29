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

namespace join.pms.web
{
    public partial class CmsView : UNV.Comm.Web.PageBase
    {
        private string m_SqlParams;
        private DataTable m_Dt;

        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_TargetUrl;
        private string m_CmsID;

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateParams();
            if (!IsPostBack) 
            {
                GetCmsView(m_CmsID);
            }
        }

        /// <summary>
        /// ��֤���ܵĲ��� 
        /// </summary>
        private void ValidateParams()
        {
            m_CmsID = PageValidate.GetTrim(Request.QueryString["RID"]);
            m_SourceUrl = Request.QueryString["sourceUrl"];
            if (!string.IsNullOrEmpty(m_SourceUrl))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
            }
            else
            {
                m_TargetUrl = "/MainDesk.aspx";
            }
            if (!string.IsNullOrEmpty(m_CmsID) && PageValidate.IsNumber(m_CmsID))
            {
                //m_CmsID = DESEncrypt.Decrypt(m_CmsID); // �ݲ�����
                //if (!PageValidate.IsNumber(m_CmsID)) Server.Transfer("~/errors.aspx");
            }
            else
            {
                Server.Transfer("/errors.aspx");
            }
        }

        private void GetCmsView(string cmsID) 
        {
            string cmsName,cmsCode = string.Empty;
            m_SqlParams = "SELECT [CmsID], [CmsTitle], [CmsBody], [CmsKeys], [CmsStats], [CmsCode], [OprateDate], [CmsCName] FROM [v_CmsContents] WHERE CmsID=" + cmsID;
            m_Dt = new DataTable();
            StringBuilder sHtml = new StringBuilder();
            try
            {
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];

                if (m_Dt.Rows.Count==1)
                {
                    cmsName = m_Dt.Rows[0]["CmsCName"].ToString();
                    cmsCode = m_Dt.Rows[0]["CmsCode"].ToString();

                    if (cmsCode.Length > 2) { this.LiteralNav.Text = join.pms.dal.CommPage.GetAllTreeName(cmsCode) + cmsName; }
                    else { this.LiteralNav.Text = cmsName; }

                    sHtml.Append("<p class=\"a1\">" + m_Dt.Rows[0]["CmsTitle"].ToString() + "</p>");

                    sHtml.Append("<p class=\"a2\">����ʱ�䣺" + DateTime.Parse(m_Dt.Rows[0]["OprateDate"].ToString()).ToString("yyyy/MM/dd") + "  �����" + m_Dt.Rows[0]["CmsStats"].ToString() + "(��)</p>");
                    sHtml.Append("<span id=\"fontzoom\" class=\"a3\">");
                    sHtml.Append(PageValidate.Decode(m_Dt.Rows[0]["CmsBody"].ToString()));
                    sHtml.Append("</span>");
                    sHtml.Append("<p class=\"a4\">ҳ�湦�ܣ������壺<a href=\"javascript:fontZoom(16)\">��</a> <a href=\"javascript:fontZoom(14)\">��</a> <a href=\"javascript:fontZoom(12)\">С</a>����<a href=\"javascript:this.print()\">��ӡ</a>����<a href=\"#top\">����</a>�� ��<a href=\"#\" onclick=\"javascript:window.location.href='" + m_TargetUrl + "';\">����</a>��</p>");

                    DbHelperSQL.ExecuteSql("UPDATE CMS_Contents SET  CmsStats=CmsStats+1 WHERE CmsID=" + cmsID);

                    m_Dt = null;
                }
            }
            catch (Exception ex) 
            { 
                sHtml.Append(ex.Message); 
            }
            m_Dt = null;

            this.LiteralData.Text = sHtml.ToString();
            sHtml = null;
        }

        private string GetNextCms(string cmsID,bool isNext) 
        {
            /*
             * -- navUrl = "/INFO/" + cmsCID + "/" + cmsID + "." + this.m_FileExt;
             --��һ��
SELECT TOP 1 [CmsID], [CmsTitle], [CmsCID] FROM [v_CmsList] WHERE (CmsAttrib=1 OR CmsAttrib=2 OR CmsAttrib=9) AND CmsID> ORDER BY CmsID DESC
--��һ��
SELECT TOP 1 [CmsID], [CmsTitle], [CmsCID] FROM [v_CmsList] WHERE (CmsAttrib=1 OR CmsAttrib=2 OR CmsAttrib=9) AND CmsID< ORDER BY CmsID DESC
             */
            string objTitle = string.Empty;
            string navUrl = string.Empty;
            string returnVa = string.Empty;
            DataTable tmpDt = new DataTable();
            
            if (isNext) {
                returnVa = "��һ����";
                m_SqlParams = "SELECT TOP 1 [CmsID], [CmsTitle] FROM [v_CmsList] WHERE (CmsAttrib=1 OR CmsAttrib=2 OR CmsAttrib=9) AND CmsID<" + cmsID + " ORDER BY CmsID DESC"; 
            }
            else {
                returnVa = "��һ����";
                m_SqlParams = "SELECT TOP 1 [CmsID], [CmsTitle] FROM [v_CmsList] WHERE (CmsAttrib=1 OR CmsAttrib=2 OR CmsAttrib=9) AND CmsID>" + cmsID + " ORDER BY CmsID DESC"; 
            }
            try {
                tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (tmpDt.Rows.Count == 1) {
                    //navUrl = "/INFO/" + tmpDt.Rows[0]["CmsCID"].ToString() + "/" + tmpDt.Rows[0]["CmsID"].ToString() + "." + this.m_FileExt;
                    navUrl = "/INFO/" + tmpDt.Rows[0]["CmsID"].ToString() + "." + this.m_FileExt;
                    returnVa += "<a href=\"" + navUrl + "\" target=\"_blank\">" + tmpDt.Rows[0]["CmsTitle"].ToString() + "</a>";
                }
            }
            catch { }
            tmpDt = null;

            return returnVa;

        }
    }
}
