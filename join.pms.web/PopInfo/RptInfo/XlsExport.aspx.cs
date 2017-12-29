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

using System.IO;
using System.Text;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;

namespace AreWeb.JdcMmc.DataGather.RptInfo
{
    public partial class XlsExport : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_SqlParams;
        public string m_TargetUrl;

        private string m_NavTitle;
        private string m_RptTime;

        private string m_Fields;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                SetOpratetionAction(m_NavTitle);
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

        #region �����֤����������У��
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
            m_ActionName = Request.QueryString["action"];
            m_SourceUrl = Request.QueryString["sourceUrl"];
            m_ObjID = Request.QueryString["k"];

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                m_NavTitle = CommPage.GetSingleVal("SELECT FuncName FROM SYS_Function WHERE FuncCode='" + m_FuncCode + "'");
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
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
                case "exp": // �༭
                    funcName = "����ѡ��������ΪExcel�ļ�";
                    SetExcelByFuncNo(m_FuncCode, m_ObjID);
                    break;
                default:
                    Response.Write(" <script>alert('����ʧ�ܣ���������') ;window.location.href='" + m_TargetUrl + "'</script>");
                    break;
            }
            this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">������ҳ</a> &gt;&gt; ���ݲɼ� &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }

        #endregion

        /// <summary>
        /// ���ò�����excel�ļ�
        /// </summary>
        /// <param name="funcNo"></param>
        /// <param name="rptID"></param>
        private void SetExcelByFuncNo(string funcNo, string rptID)
        {
            string[] aryTitles = null;
            string clos = GetRptColumns(m_FuncCode); //��ͷ����
            StringBuilder sb = new StringBuilder();//��������
            StringBuilder sHeader = new StringBuilder();//��ͷ
            StringBuilder sFooter = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                aryTitles = m_Fields.Split(',');
                //������Ϣ����ͷ��ҳ��
                m_SqlParams = "SELECT * FROM RPT_Basis WHERE RptID=" + rptID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    m_RptTime = sdr["RptTime"].ToString();
                    sHeader.Append("<table width=\"950\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    if (m_FuncCode == "0212")
                    {
                        sHeader.Append("<tr><td height=\"26\" colspan=\"" + aryTitles.Length.ToString() + "\" align=\"center\" style=\"font-weight: bold;font-size:24px;white-space: nowrap;\">" + m_RptTime + PageValidate.GetTrim(sdr["RptName"].ToString()) + "</td></tr>");
                    }
                    else
                    {
                        sHeader.Append("<tr><td height=\"26\" colspan=\"" + aryTitles.Length.ToString() + "\" align=\"center\" style=\"font-weight: bold;font-size:24px;white-space: nowrap;\">" + PageValidate.GetTrim(sdr["RptName"].ToString()) + "</td></tr>");
                    }
                    sHeader.Append("<tr><td width=\"50%\" height=\"22\" colspan=\"" + (aryTitles.Length / 2).ToString() + "\">��λ���ƣ�" + PageValidate.GetTrim(sdr["UnitName"].ToString()) + "</td>");
                    if (m_FuncCode == "0211")
                    {
                        sHeader.Append("<td>" + m_RptTime + "</td>");
                        sHeader.Append("<td align=\"right\">������λ��ǧԪ</td>");
                    }
                    else { sHeader.Append("<td colspan=\"" + (aryTitles.Length / 2).ToString() + "\">" + m_RptTime + "</td>"); }
                    sHeader.Append("</tr></table>");

                    sFooter.Append("<table width=\"950\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
                    sFooter.Append("<td width=\"25%\" height=\"22\">��λ�����ˣ�" + PageValidate.GetTrim(sdr["UnitHeader"].ToString()) + "</td>");
                    sFooter.Append("<td width=\"25%\">ͳ�Ƹ����ˣ�" + sdr["StatsHeader"].ToString() + "</td>");
                    sFooter.Append("<td width=\"25%\">����ˣ�" + sdr["RptUserName"].ToString() + "</td>");
                    sFooter.Append("<td width=\"25%\">�ʱ�䣺" + DateTime.Parse(sdr["OprateDate"].ToString()).ToString("yyyy-MM-dd") + "</td>");
                    sFooter.Append("</tr></table>");
                }
                sdr.Close();

                //��������
                sb.Append(sHeader.ToString()); //�������
                sb.Append(clos); //��ͷ����

                m_SqlParams = "SELECT " + m_Fields + " FROM RPT_Contents WHERE RptID=" + rptID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    sb.Append("<tr>");
                    for (int i = 0; i < aryTitles.Length; i++)
                    {
                        if (i == 0)
                        {
                            sb.Append("<td height=\"22\">&nbsp;" + PageValidate.GetTrim(sdr[i].ToString()) + "</td>");
                        }
                        else
                        {
                            sb.Append("<td align=\"center\">&nbsp;" + PageValidate.GetTrim(sdr[i].ToString()) + "</td>");
                        }
                    }
                    sb.Append("<tr/>");
                }
                sdr.Close();
                sb.AppendLine("</table>");
                sb.Append(sFooter.ToString());//����ҳ��
            }
            catch { if (sdr != null) sdr.Close(); }

            SetXlsFiles(sb.ToString()); // ����Ϊxls�ļ�

            sHeader = null;
            sFooter = null;
            sb = null;

            /*
            //��ͷ
            sb.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\" >");
            sb.AppendLine("<tr style=\"font-weight: bold;font-size:22px;white-space: nowrap;\">");
            sb.AppendLine("<td colspan=\"8\" align=\"center\" height=\"35\">" + XlsName + "</td>");
            sb.AppendLine("</tr>");
            // ����
            sb.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
            sb.AppendLine("<td colspan=\"4\" align=\"left\">" + XlsUnit + "</td><td colspan=\"4\" align=\"left\">" + XlsUnit + "</td>");
            sb.AppendLine("</tr>");
            //colspan="5"
            //��ͷ 
            sb.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
            for (int i = 0; i < aryFields.Length; i++)
            {
                sb.AppendLine("<td>" + aryTitles[i] + "</td>");
            }
            sb.AppendLine("</tr>");
            // ����
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                sb.Append("<tr>");
                sb.Append("<td style=\"vnd.ms-excel.numberformat:@\">" + dt.Rows[j][aryFields[k]].ToString() + "</td>");// ǿ�����ָ�ʽ
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</table>");
            */
        }

        /// <summary>
        ///  ��ȡ������ͷ(��ͷ)
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetRptColumns(string funcNo)
        {
            StringBuilder clos = new StringBuilder();
            /*
02	���ݲɼ� 
------------------------------------
0201	��ҵ�ܲ�ֵ����Ҫ��Ʒ�����±�
0202	��Ҫ������������ָ���
0203	��Ҫ��ҵ��Ʒ����������桢����
0204	����ҵ��ҵ�ܲ�ֵ
0205	��ɫ������ҵ��Ҫ��Ӫָ��
0206	ԭ���ϡ�ȼ�ϡ����������۸�����±���
0207	��ҵƷ�����۸��±�
0208	���Ʒ��Ҫ�ͻ��������
0209	�������������
0210	��Ҫ��Ʒ���������ۡ����������
0211	����״��
0212	����ͳ�Ʊ���
0213	����ɷݿ�ҵ�ֹ�˾�����챨
*/
            switch (funcNo)
            {
                case "0201":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08";
                    #region ��ͷ
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    clos.Append("<tr>");
                    clos.Append("<td rowspan=\"2\" align=\"center\" >&nbsp;��ֵ���Ʒ(ָ��)����</td>");
                    clos.Append("<td rowspan=\"2\" align=\"center\" >&nbsp;���㵥λ</td>");
                    clos.Append("<td height=\"22\" colspan=\"2\" align=\"center\" >�ƻ�</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\" >����ʵ��</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\" >ȥ��ʵ��</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\" >����</td>");
                    clos.Append("<td width=\"100\" align=\"center\" >����</td>");
                    clos.Append("<td width=\"100\" align=\"center\" >����</td>");
                    clos.Append("<td width=\"100\" align=\"center\" >����ֹ�ۼ�</td>");
                    clos.Append("<td width=\"100\" align=\"center\" >ͬ��</td>");
                    clos.Append("<td width=\"100\" align=\"center\" >ͬ��ֹ�ۼ�</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    clos.Append("<tr>");
                    clos.Append("<td height=\"23\" align=\"center\" >��</td>");
                    clos.Append("<td align=\"center\" >��</td>");
                    clos.Append("<td align=\"center\" >1</td>");
                    clos.Append("<td align=\"center\" >2</td>");
                    clos.Append("<td align=\"center\" >3</td>");
                    clos.Append("<td align=\"center\" >4</td>");
                    clos.Append("<td align=\"center\" >5</td>");
                    clos.Append("<td align=\"center\" >6</td>");
                    clos.Append("</tr>");
                    #endregion
                    break;
                case "0202":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19";
                    #region ��ͷ
                    clos.Append("<table width=\"2000\" border=\"1\" cellpadding=\"2\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"200\" rowspan=\"2\" align=\"center\">ָ������</td>");
                    clos.Append("<td colspan=\"3\" align=\"center\">���㵥λ</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">����ƻ�</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">����ָ��</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">����ֹ�ۼ�ָ��</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">ȥ��ͬ���ۼ�ָ��</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">��ȥ��(��%)</td>");
                    clos.Append("<td colspan=\"4\" align=\"center\">����</td>");
                    clos.Append("<td colspan=\"4\" align=\"center\">ĸ��</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">��ʷ���ˮƽ</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\">���㵥λ</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ĸ��</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����ֹ�ۼ�</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����ֹ�ۼ�</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ȥ��ͬ���ۼ�</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����ֹ�ۼ�</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����ֹ�ۼ�</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ȥ��ͬ���ۼ�</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ָ��</td>");
                    clos.Append("<td width=\"100\" align=\"center\">���</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0203":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17";
                    #region ��ͷ
                    clos.Append("<table width=\"1900\" border=\"1\" cellpadding=\"2\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"200\" rowspan=\"2\" align=\"center\">��Ʒ����</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">������λ</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">����</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">��������</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">�ۼ�������</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">�ۼ�������</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">��ҵ���ü�����</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">��ĩ�����</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">1-�����ۼƶ�����</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">1-�����ۼƶ�����(ǧԪ)</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ȥ��ͬ��</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ȥ��ͬ��</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ȥ��ͬ��</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ȥ��ͬ��</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ȥ��ͬ��</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ȥ��ͬ��</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ȥ��ͬ��</td>");
                    clos.Append("</tr> ");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    clos.Append("<tr>");
                    clos.Append("<td height=\"23\" align=\"center\">��</td>");
                    clos.Append("<td align=\"center\">��</td>");
                    clos.Append("<td align=\"center\">��</td>");
                    clos.Append("<td align=\"center\">1</td>");
                    clos.Append("<td align=\"center\"></td>");
                    clos.Append("<td align=\"center\"></td>");
                    clos.Append("<td align=\"center\"></td>");
                    clos.Append("<td align=\"center\">2</td>");
                    clos.Append("<td align=\"center\"></td>");
                    clos.Append("<td align=\"center\">3</td>");
                    clos.Append("<td align=\"center\"></td>");
                    clos.Append("<td align=\"center\">4</td>");
                    clos.Append("<td align=\"center\"></td>");
                    clos.Append("<td align=\"center\"></td>");
                    clos.Append("<td align=\"center\"></td>");
                    clos.Append("<td align=\"center\"></td>");
                    clos.Append("<td align=\"center\"></td>");
                    clos.Append("</tr>");
                    #endregion
                    break;
                case "0204":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07";
                    #region ��ͷ
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"2\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"200\" rowspan=\"2\" align=\"center\">ָ������</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">���㵥λ</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" colspan=\"2\" align=\"center\">����ʵ��</td>");
                    clos.Append("<td width=\"100\" colspan=\"2\" align=\"center\">ȥ��ʵ��</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����ֹ�ۼ�</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ȥ��ͬ��</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����ֹ�ۼ�</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0205":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13";
                    #region ��ͷ
                    clos.Append("<table width=\"1400\" border=\"1\" cellpadding=\"0\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"200\" rowspan=\"2\" align=\"center\">ָ������</td>");
                    clos.Append("<td colspan=\"4\" align=\"center\">�������֣�</td>");
                    clos.Append("<td colspan=\"4\" align=\"center\">�������֣�</td>");
                    clos.Append("<td colspan=\"4\" align=\"center\">��˰�۸񣨶�/Ԫ��</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\">���²���</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">�ۼƲ���</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ͬ��</td>");
                    clos.Append("<td width=\"100\" align=\"center\">��������</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">�ۼ�����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ͬ��</td>");
                    clos.Append("<td width=\"100\" align=\"center\">�������ۼ۸�</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">�ۼ����ۼ۸�</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ͬ��</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0206":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07";
                    #region ��ͷ
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"0\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"200\" rowspan=\"2\" align=\"center\">��Ʒ����</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">���㵥λ</td>");
                    clos.Append("<td width=\"100\" colspan=\"3\" align=\"center\">�����ڵ���</td>");
                    clos.Append("<td width=\"100\" colspan=\"2\" align=\"center\">���ڵ���</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\">5�յ���</td>");
                    clos.Append("<td width=\"100\" align=\"center\">20�յ���</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ƽ������</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����ͬ��ƽ����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">���µ���</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0207":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07";
                    #region ��ͷ
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"0\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"200\" rowspan=\"2\" align=\"center\">��Ʒ����</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">���㵥λ</td>");
                    clos.Append("<td width=\"100\" colspan=\"3\" align=\"center\">�����ڵ���</td>");
                    clos.Append("<td width=\"100\" colspan=\"2\" align=\"center\">���ڵ���</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\">5�յ���</td>");
                    clos.Append("<td width=\"100\" align=\"center\">20�յ���</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ƽ������</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����ͬ��ƽ����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">���µ���</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0208":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05";
                    #region ��ͷ
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"0\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"200\" align=\"center\">�ͻ�����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">��Ʒ</td>");
                    clos.Append("<td width=\"100\" align=\"center\">��������(��)</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����ֹ����(��)</td>");
                    clos.Append("<td width=\"100\" align=\"center\">���(��Ԫ)</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0209":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06";
                    #region ��ͷ
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"0\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">Ʒ��</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����������(��)</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����������(��)</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����ֹʹ�����(��)</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����ֹʹ�����ռ��(%)</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0210":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15";
                    #region ��ͷ
                    clos.Append("<table width=\"2000\" border=\"1\" cellpadding=\"2\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">���</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">��Ʒ����</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">��������</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">�ۼ�������</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">�ۼ�������</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">�ۼ���������(Ԫ)(��˰)</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">�������ۼ۸�(Ԫ/�ֱ���)</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\"align=\"center\">�ۼ����ۼ۸�(Ԫ/�ֱ���)</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">�ۼ���ҵ���ü�����</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">��ĩ�����</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ʵ����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ʵ����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ʵ����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ʵ����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ʵ����</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0211":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04";
                    #region ��ͷ
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"0\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" align=\"center\">ָ������</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">1-����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����ͬ��</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0212":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06";
                    #region ��ͷ
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"0\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" align=\"center\">��λ����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">��λ����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">��λ����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0213":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15";
                    #region ��ͷ
                    clos.Append("<table width=\"2000\" border=\"1\" cellpadding=\"2\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" align=\"center\">���</td>");
                    clos.Append("<td width=\"200\" align=\"center\">ָ������</td>");
                    clos.Append("<td width=\"100\" align=\"center\">������λ</td>");
                    clos.Append("<td width=\"100\" align=\"center\">�¼ƻ�</td>");
                    clos.Append("<td width=\"100\" align=\"center\">��ʵ��</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ȥ��ͬ��</td>");
                    clos.Append("<td width=\"100\" align=\"center\">��ƻ��Ƚ�����</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ͬ������</td>");
                    clos.Append("<td width=\"100\" align=\"center\">��ƻ�</td>");
                    clos.Append("<td width=\"100\" align=\"center\">����ֹ�ۼ�</td>");
                    clos.Append("<td width=\"100\" align=\"center\">�����ƻ���%������ƻ��Ƚϡ�%</td>");
                    clos.Append("<td width=\"100\" align=\"center\">ȥ��ͬ��ֹ�ۼ�</td>");
                    clos.Append("<td width=\"100\" align=\"center\">��ȥ��ͬ��ֹ�ۼƱȽ�����</td>");
                    clos.Append("</tr> ");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                default:
                    Response.Write(" <script>alert('����ʧ�ܣ���������') ;window.location.href='" + m_TargetUrl + "'</script>");
                    break;
            }
            return clos.ToString();
        }

        /// <summary>
        /// ��xls��ʽ���ݱ���Ϊ�ļ�
        /// </summary>
        /// <param name="xlsBody"></param>
        private void SetXlsFiles(string xlsBody)
        {
            string msgTxt = string.Empty;
            string serverPath = Server.MapPath("/");
            string configPath = System.Configuration.ConfigurationManager.AppSettings["FCKeditor:UserFilesPath"];//�ļ����·��
            string virtualPath = configPath + m_FuncCode + "/" + StringProcess.GetCurDateTimeStr(6) + "/";
            string savePath = serverPath + virtualPath;
            string saveFiles = savePath + System.DateTime.Now.ToString("yyyyMMdd-hhmm") + ".xls";
            string filePath = string.Empty;
            try
            {
                if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

                StreamWriter sw = new StreamWriter(saveFiles, false, System.Text.Encoding.Default);
                sw.Write(xlsBody.ToString());
                sw.Close();

                filePath = saveFiles.Replace(serverPath, "");
                msgTxt += " �ɹ�����Excel�ļ���<a href=\"" + filePath + "\" target='_blank' > " + m_RptTime + m_NavTitle + "���ݣ������˴����ص�����</a><br/>";
                SetFileToHD(m_RptTime + m_NavTitle + "����", filePath);
                msgTxt += "�ĵ��Ѿ�ͬ��������[ ����Ӳ�� >> �ҵ����� >> ]��Ŀ¼�¡���";
            }
            catch (Exception ex)
            {
                msgTxt += ex.Message;
            }
            xlsBody = null;

            this.LiteralMsg.Text = msgTxt;
        }


        private void SetFileToHD(string fileName, string filePath)
        {
            // 
            m_SqlParams = "INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES('" + fileName + "','" + filePath + "','.xls','0802'," + m_UserID + ",1)";
            DbHelperSQL.ExecuteSql(m_SqlParams);
        }
    }
}

