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

namespace join.pms.dalInfo
{
    public partial class bizprt : System.Web.UI.Page
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

        #region
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
                case "print": // ��ӡ
                    ShowModInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                    break;
            }
        }

        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            string areaName = string.Empty;
            StringBuilder s = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT * FROM [PIS_BaseInfo] WHERE CommID=" + m_ObjID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);

                while (sdr.Read())
                {
                    areaName = sdr["AreaName"].ToString();
                    switch (this.m_FuncCode)
                    {
                        case "1101":
                            #region һ�����������У�֤�� ũ��
                            s.Append("<div class=\"dy1101\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("      <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">һ�����������У�֤��</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" class=\"table_01\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("      <tr>");
                            s.Append("         <td width=\"60\">�ɷ�<br />����</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds01"].ToString() + "&nbsp;&nbsp;</td>");
                            s.Append("        <td width=\"60\">���<br />֤��</td>");
                            s.Append("        <td width=\"250\" colspan=\"3\">" + sdr["Fileds02"].ToString() + "&nbsp;&nbsp;</td>");
                            s.Append("        <td width=\"70\">������</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds03"].ToString() + "&nbsp;&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td width=\"60\">����<br />����</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds04"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"60\">���<br />֤��</td>");
                            s.Append("        <td width=\"250\" colspan=\"3\">" + sdr["Fileds05"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"70\">������</td>");
                            s.Append("       <td width=\"150\">" + sdr["Fileds06"].ToString() + "&nbsp;</td>");
                            s.Append("     </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td width=\"60\">����<br />����</td>");
                            s.Append("       <td width=\"150\">" + sdr["Fileds07"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"60\">�ֻ�<br />�·�</td>");
                            s.Append("       <td width=\"250\">" + sdr["Fileds08"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"60\">����<br />����</td>");
                            s.Append("       <td width=\"120\">" + sdr["Fileds09"].ToString() + "&nbsp;</td>");
                            s.Append("      <td width=\"70\">�Ա�</td>");
                            s.Append("      <td width=\"150\">" + sdr["Fileds10"].ToString() + "&nbsp;</td>");
                            s.Append("    </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td colspan=\"8\" class=\"t_l font20\" style=\"line-height:28px;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;���ݡ����ɹ��������˿���ƻ������������ڶ�ʮ�����涨��ʵ��������һ̥��Ů�Ǽ��ƶȡ�����ʵ���÷�����������һ̥����/������</td>");
                            s.Append("    </tr>");
                            s.Append("	  <tr>");
                            s.Append("      <td colspan=\"8\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("  <tr>");
                            s.Append("    <td width=\"33%\" class=\"x_r t_l\" style=\"line-height:35px;\">");
                            s.Append("	<div style=\"padding:0 15px\">");
                            s.Append("	<p style=\"min-height:150px;\">" + sdr["Fileds11"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>�壨�ӣ�ί�����<br />");
                            s.Append("	�����ˣ�" + sdr["Fileds12"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds13"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("   <td width=\"33%\" class=\"x_r t_l\" style=\"line-height:35px;\">");
                            s.Append("	<div style=\"padding:0 15px\">");
                            s.Append("	<p style=\"min-height:150px;\">" + sdr["Fileds14"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>���������������<br />");
                            s.Append("	�����ˣ�" + sdr["Fileds15"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds16"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("   <td width=\"33%\" class=\"t_l\" style=\"line-height:35px;\">");
                            s.Append("	<div style=\"padding:0 15px\">");
                            s.Append("	<p style=\"min-height:150px;\">" + sdr["Fileds17"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>�������ͼƻ����������<br />");
                            s.Append("	�����ˣ�" + sdr["Fileds18"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds19"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("  </tr>");
                            s.Append("</table></td>");
                            s.Append("     </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td width=\"60\">��ע</td>");
                            s.Append("        <td colspan=\"7\" class=\"t_l\">" + sdr["Fileds20"].ToString() + "&nbsp;</td>");
                            s.Append("     </tr>");

                            s.Append("    </table></td>");
                            s.Append("      </tr>");
                            s.Append("</table>");
                            s.Append("</div>");
                            #endregion
                            break;
                        case "1102":
                            #region  һ�����������У�֤�� ����
                            s.Append("<div class=\"dy1102\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">һ�����������У�֤��</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" class=\"table_01\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("      <tr>");
                            s.Append("        <td width=\"60\">�ɷ�<br />����</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds01"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"60\">���<br />֤��</td>");
                            s.Append("        <td width=\"250\" colspan=\"3\">" + sdr["Fileds02"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"70\">������</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds03"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td width=\"60\">����<br />����</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds04"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"60\">���<br />֤��</td>");
                            s.Append("        <td width=\"250\" colspan=\"3\">" + sdr["Fileds05"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"70\">������</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds06"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td width=\"60\">����<br />ʱ��</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds07"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"60\">����<br />�·�</td>");
                            s.Append("        <td width=\"250\">" + sdr["Fileds08"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"60\">����<br />����</td>");
                            s.Append("        <td width=\"120\">" + sdr["Fileds09"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"70\">�Ա�</td>");
                            s.Append("       <td width=\"150\">" + sdr["Fileds10"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td colspan=\"8\" class=\"t_l font20\" style=\"line-height:28px;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;���ݡ����ɹ��������˿���ƻ������������ڶ�ʮ�����涨��ʵ��������һ̥��Ů�Ǽ��ƶȡ�����ʵ���÷�����������һ̥����/������</td>");
                            s.Append("      </tr>");
                            s.Append("	  <tr>");
                            s.Append("        <td colspan=\"8\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("  <tr>");
                            s.Append("    <td width=\"50%\" class=\"x_r t_l\" style=\"line-height:35px;\">");
                            s.Append("	<div style=\"padding:0 50px\">");
                            s.Append("	<p style=\"min-height:150px;\">" + sdr["Fileds11"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>��&nbsp;&nbsp;λ&nbsp;&nbsp;��&nbsp;&nbsp;��<br />");
                            s.Append("	�����ˣ�" + sdr["Fileds12"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds13"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("    <td width=\"50%\" class=\"t_l\" style=\"line-height:35px;\">");
                            s.Append("	<div style=\"padding:0 50px\">");
                            s.Append("	<p style=\"min-height:150px;\">" + sdr["Fileds14"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>�������ͼƻ������ְ����<br />");
                            s.Append("	�����ˣ�" + sdr["Fileds15"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds16"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("  </tr>");
                            s.Append("</table></td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td width=\"60\">��ע</td>");
                            s.Append("        <td colspan=\"7\" class=\"t_l\">" + sdr["Fileds17"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");

                            s.Append("    </table></td>");
                            s.Append("        </tr>");
                            s.Append("</table>");
                            s.Append("</div>");
                            #endregion
                            break;
                        case "1103":
                            #region ��ֹ����������˱�
                            s.Append("<div class=\"dy1103\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">�� ֹ �� �� �� �� �� �� ��</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" class=\"table_01\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("      <tr>");
                            s.Append("        <td width=\"90\">������<br />����</td>");
                            s.Append("        <td width=\"100\">" + sdr["Fileds01"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"80\">���֤<br />����</td>");
                            s.Append("        <td width=\"250\" colspan=\"2\">" + sdr["Fileds02"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"70\">���<br />ʱ��</td>");
                            s.Append("        <td width=\"150\" class=\"t_r\">" + DateTime.Parse(sdr["Fileds03"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>��ż����</td>");
                            s.Append("        <td>" + sdr["Fileds04"].ToString() + "&nbsp;</td>");
                            s.Append("        <td>���֤<br />����</td>");
                            s.Append("        <td colspan=\"2\">" + sdr["Fileds05"].ToString() + "&nbsp;</td>");
                            s.Append("        <td>̥��</td>");
                            s.Append("        <td>" + sdr["Fileds06"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>������</td>");
                            s.Append("       <td colspan=\"6\" class=\"t_l\">" + sdr["Fileds07"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td>ǰһ��<br />����</td>");
                            s.Append("       <td>����</td>");
                            s.Append("       <td>" + sdr["Fileds08"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"60\">����<br />ʱ��</td>");
                            s.Append("        <td width=\"190\">" + sdr["Fileds09"].ToString() + "&nbsp;</td>");
                            s.Append("        <td>�Ա�</td>");
                            s.Append("        <td>" + sdr["Fileds10"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>��ֹ����<br />����</td>");
                            s.Append("        <td colspan=\"6\" class=\"t_l\">" + sdr["Fileds11"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>�弶���</td>");
                            s.Append("        <td colspan=\"6\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("  <tr>");
                            s.Append("    <td width=\"50%\" class=\"x_r t_l\" style=\"line-height:35px; \">");
                            s.Append("	<div style=\"padding:0 5px\">");
                            s.Append("	<p style=\"min-height:50px;\">" + sdr["Fileds12"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>�帺����ǩ�֣�" + sdr["Fileds13"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds14"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("    <td width=\"50%\" class=\"t_l\" style=\"line-height:35px; \">");
                            s.Append("	<div style=\"padding:0 5px\">");
                            s.Append("	<p style=\"min-height:50px;\">" + sdr["Fileds15"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>����������ǩ�֣�" + sdr["Fileds16"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds17"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("  </tr>");
                            s.Append("</table></td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>������<br />����</td>");
                            s.Append("        <td colspan=\"6\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("  <tr>");
                            s.Append("    <td width=\"50%\" class=\"x_r t_l\" style=\"line-height:35px; \">");
                            s.Append("	<div style=\"padding:0 5px\">");
                            s.Append("	<p style=\"min-height:40px;\">" + sdr["Fileds18"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>�����쵼ǩ�֡����£�" + sdr["Fileds19"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds20"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("    <td width=\"50%\" class=\"t_l\" style=\"line-height:35px; \">");
                            s.Append("	<div style=\"padding:0 5px\">");
                            s.Append("	<p style=\"min-height:40px;\">" + sdr["Fileds21"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>����������ǩ�֣�" + sdr["Fileds22"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds23"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("  </tr>");
                            s.Append("</table></td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>ҽ�Ʊ���<br />�������</td>");
                            s.Append("        <td colspan=\"6\" class=\"t_l\">" + sdr["Fileds24"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>��������<br />�����쵼<br />С�����</td>");
                            s.Append("        <td colspan=\"6\" class=\"t_l\" style=\"padding:10px 0; \"><div style=\"padding:0 5px\">");
                            s.Append("	<p style=\"min-height:30px;\">" + sdr["Fileds25"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>��������ר��ǩ�����������ϣ���" + sdr["Fileds26"].ToString() + "&nbsp;<br /><br />");
                            s.Append("	��ϵ�λ�����£���</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds27"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("     </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>����ί<br />���</td>");
                            s.Append("        <td colspan=\"6\" class=\"t_l\">" + sdr["Fileds28"].ToString() + "&nbsp;</td>");
                            s.Append("     </tr>");

                            s.Append("    </table></td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("         <td colspan=\"2\" class=\"bt_03\">˵����<br />");
                            s.Append("1���˱�һʽ���ݣ�����ί��������λ�����˸�һ��<br />");
                            s.Append("2����ֹ���������߸�ҽԺ�ͼ����������֤��</td>");
                            s.Append("        </tr>");
                            s.Append("</table>");
                            s.Append("</div>");
                            #endregion
                            break;
                        case "1104":
                            #region ��ֹ����֪ͨ��
                            s.Append("<div class=\"dy1104\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("    <tr>");
                            s.Append("      <td class=\"left\" valign=\"top\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">��ֹ����֪ͨ��</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" valign=\"top\" class=\"bt_02\">�������</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" class=\"table_01\" align=\"left\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append(" <tr>");
                            s.Append("    <td><span class=\"a1\">" + sdr["Fileds01"].ToString() + "&nbsp;</span>��</td>");
                            s.Append("  </tr>");
                            s.Append("  <tr>");
                            s.Append("    <td class=\"sj\">����<span class=\"a2\">" + sdr["Fileds02"].ToString() + "&nbsp;</span>����<span class=\"a3\">" + sdr["Fileds03"].ToString() + "&nbsp;</span>��ǰ��ʵʩ��ֹ����������</td>");
                            s.Append("  </tr>");
                            s.Append("  <tr>");
                            s.Append("    <td class=\"sj\">�ش�֤��</td>");
                            s.Append("  </tr>");
                            s.Append("</table>");
                            s.Append("		  </td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"right\" class=\"bt_03\">�˿ںͼƻ�����ίԱ��</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"right\" class=\"bt_04\">" + DateTime.Parse(sdr["Fileds04"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</td>");
                            s.Append("        </tr>");
                            s.Append("      </table></td>");
                            s.Append("      <td valign=\"top\" class=\"right\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">��ֹ����֪ͨ��</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" valign=\"top\" class=\"bt_02\">&nbsp;</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" class=\"table_01\" align=\"left\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("  <tr>");
                            s.Append("    <td><span class=\"a1\">" + sdr["Fileds01"].ToString() + "&nbsp;</span>��</td>");
                            s.Append("  </tr>");
                            s.Append("  <tr>");
                            s.Append("    <td class=\"sj\">����<span class=\"a2\">" + sdr["Fileds02"].ToString() + "&nbsp;</span>����<span class=\"a3\">" + sdr["Fileds03"].ToString() + "&nbsp;</span>��ǰ��ʵʩ��ֹ����������</td>");
                            s.Append("  </tr>");
                            s.Append("  <tr>");
                            s.Append("    <td class=\"sj\">�ش�֤��</td>");
                            s.Append("  </tr>");
                            s.Append("</table>");
                            s.Append("		  </td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"right\" class=\"bt_03\">�˿ںͼƻ�����ίԱ��</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"right\" class=\"bt_04\">" + DateTime.Parse(sdr["Fileds04"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</td>");
                            s.Append("        </tr>");
                            s.Append("     </table></td>");
                            s.Append("    </tr>");
                            s.Append("</table>");
                            s.Append("</div>");
                            #endregion
                            break;
                        case "1105":
                            #region �ƻ���������Ӥ��ʵ���Ǽǵ�
                            s.Append("<div class=\"dy1105\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("    <tr>");
                            s.Append("      <td class=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">" + areaName + "&nbsp;�ƻ���������Ӥ��ʵ���Ǽǵ�������</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr class=\"bt_02\">");
                            s.Append("          <td align=\"left\">" + areaName + "&nbsp;�ƻ���������Ӥ��ʵ���Ǽǵ�</td>");
                            s.Append("         <td align=\"right\">�����&nbsp;" + sdr["Fileds01"].ToString() + "&nbsp;&nbsp;��</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" class=\"table_01\">");
                            // �������
                            s.Append("<table width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append(" <tr>");
                            s.Append("  <td width=\"38\">Ӥ��<br />����</td>");
                            s.Append("   <td width=\"100\" colspan=\"2\">" + sdr["Fileds02"].ToString() + "&nbsp;</td>");
                            s.Append("    <td width=\"55\">�Ա�</td>");
                            s.Append("    <td width=\"75\">" + sdr["Fileds03"].ToString() + "&nbsp;</td>");
                            s.Append("   <td width=\"44\">����<br />����</td>");
                            s.Append("    <td width=\"60\">" + sdr["Fileds04"].ToString() + "&nbsp;</td>");
                            s.Append("    <td width=\"40\">̥��</td>");
                            s.Append("    <td width=\"35\">" + sdr["Fileds05"].ToString() + "&nbsp;</td>");
                            s.Append("  </tr>");
                            s.Append("  <tr>");
                            s.Append("    <td>����<br />����</td>");
                            s.Append("       <td colspan=\"2\">" + sdr["Fileds06"].ToString() + "&nbsp;</td>");
                            s.Append("        <td>���<br />֤��</td>");
                            s.Append("        <td>" + sdr["Fileds07"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>��ͥסַ��λ</td>");
                            s.Append("      <td colspan=\"3\">" + sdr["Fileds08"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("    <tr>");
                            s.Append("       <td>ĸ��<br />����</td>");
                            s.Append("       <td colspan=\"2\">" + sdr["Fileds09"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>���<br />֤��</td>");
                            s.Append("       <td>" + sdr["Fileds10"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>��ͥסַ��λ</td>");
                            s.Append("       <td colspan=\"3\">" + sdr["Fileds11"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td rowspan=\"2\">�ƻ��������</td>");
                            s.Append("       <td>��֤<br />���</td>");
                            s.Append("       <td width=\"59\">" + sdr["Fileds12"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>������<br />�ḧ��<br />�����</td>");
                            s.Append("      <td>" + sdr["Fileds13"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>��ʵ��ʩ���</td>");
                            s.Append("       <td colspan=\"3\">" + sdr["Fileds14"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>����<br />����<br />����<br />��</td>");
                            s.Append("       <td>" + sdr["Fileds15"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>������</td>");
                            s.Append("      <td>" + sdr["Fileds16"].ToString() + "&nbsp;</td>");
                            s.Append("      <td>�����쵼ǩ��</td>");
                            s.Append("      <td colspan=\"3\">" + sdr["Fileds17"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("  </table>");
                            //============================
                            s.Append("</td>");
                            s.Append("        </tr>");
                            s.Append("       <tr>");
                            s.Append("          <td colspan=\"2\" align=\"right\" class=\"bt_03\">" + areaName + "&nbsp;������</td>");
                            s.Append("       </tr>");
                            s.Append("       <tr>");
                            s.Append("         <td colspan=\"2\" align=\"right\" class=\"bt_04\">" + DateTime.Parse(sdr["Fileds18"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("     </table></td>");
                            s.Append("     <td class=\"right\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("       <tr>");
                            s.Append("         <td colspan=\"2\" align=\"center\" class=\"bt_01\">" + areaName + "&nbsp;�ƻ���������Ӥ��ʵ���Ǽǵ�������</td>");
                            s.Append("       </tr>");
                            s.Append("       <tr class=\"bt_02\">");
                            s.Append("        <td align=\"left\">" + areaName + "&nbsp;�ƻ���������Ӥ��ʵ���Ǽǵ�</td>");
                            s.Append("        <td align=\"right\">�����&nbsp;" + sdr["Fileds01"].ToString() + "&nbsp;&nbsp;��</td>");
                            s.Append("      </tr>");
                            s.Append("       <tr>");
                            s.Append("         <td colspan=\"2\" class=\"table_01\">");
                            //=============
                            s.Append("<table width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append(" <tr>");
                            s.Append("  <td width=\"38\">Ӥ��<br />����</td>");
                            s.Append("   <td width=\"100\" colspan=\"2\">" + sdr["Fileds02"].ToString() + "&nbsp;</td>");
                            s.Append("    <td width=\"55\">�Ա�</td>");
                            s.Append("    <td width=\"75\">" + sdr["Fileds03"].ToString() + "&nbsp;</td>");
                            s.Append("   <td width=\"44\">����<br />����</td>");
                            s.Append("    <td width=\"60\">" + sdr["Fileds04"].ToString() + "&nbsp;</td>");
                            s.Append("    <td width=\"40\">̥��</td>");
                            s.Append("    <td width=\"35\">" + sdr["Fileds05"].ToString() + "&nbsp;</td>");
                            s.Append("  </tr>");
                            s.Append("  <tr>");
                            s.Append("    <td>����<br />����</td>");
                            s.Append("       <td colspan=\"2\">" + sdr["Fileds06"].ToString() + "&nbsp;</td>");
                            s.Append("        <td>���<br />֤��</td>");
                            s.Append("        <td>" + sdr["Fileds07"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>��ͥסַ��λ</td>");
                            s.Append("      <td colspan=\"3\">" + sdr["Fileds08"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("    <tr>");
                            s.Append("       <td>ĸ��<br />����</td>");
                            s.Append("       <td colspan=\"2\">" + sdr["Fileds09"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>���<br />֤��</td>");
                            s.Append("       <td>" + sdr["Fileds10"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>��ͥסַ��λ</td>");
                            s.Append("       <td colspan=\"3\">" + sdr["Fileds11"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td rowspan=\"2\">�ƻ��������</td>");
                            s.Append("       <td>��֤<br />���</td>");
                            s.Append("       <td width=\"59\">" + sdr["Fileds12"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>������<br />�ḧ��<br />�����</td>");
                            s.Append("      <td>" + sdr["Fileds13"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>��ʵ��ʩ���</td>");
                            s.Append("       <td colspan=\"3\">" + sdr["Fileds14"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>����<br />����<br />����<br />��</td>");
                            s.Append("       <td>" + sdr["Fileds15"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>������</td>");
                            s.Append("      <td>" + sdr["Fileds16"].ToString() + "&nbsp;</td>");
                            s.Append("      <td>�����쵼ǩ��</td>");
                            s.Append("      <td colspan=\"3\">" + sdr["Fileds17"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("  </table>");
                            //=============
                            s.Append("   </td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td colspan=\"2\" align=\"right\" class=\"bt_03\">" + areaName + "&nbsp;������</td>");
                            s.Append("     </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td colspan=\"2\" align=\"right\" class=\"bt_04\">" + DateTime.Parse(sdr["Fileds18"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("    </table></td>");
                            s.Append("   </tr>");
                            s.Append("</table>");
                            s.Append("</div>");
                            #endregion
                            break;
                        case "1106":
                            #region ����Ӥ��ʵ���Ǽǵ�(�ؾ�)
                            s.Append("<div class=\"dy1106\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("    <tr>");
                            s.Append("      <td class=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">�����и����������ͼƻ�������<br />����Ӥ��ʵ���Ǽǵ��������</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr class=\"bt_02\">");
                            s.Append("          <td align=\"left\">&nbsp;</td>");
                            s.Append("          <td align=\"right\">���˵�&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" class=\"table_01\"><table width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("      <tr>");
                            s.Append("        <td width=\"50\">Ӥ��<br />����</td>");
                            s.Append("        <td width=\"80\">" + sdr["Fileds02"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"50\">�Ա�</td>");
                            s.Append("        <td width=\"80\">" + sdr["Fileds03"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"50\">����<br />����</td>");
                            s.Append("       <td width=\"80\">" + sdr["Fileds04"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"50\">̥��</td>");
                            s.Append("       <td width=\"80\">" + sdr["Fileds05"].ToString() + "&nbsp;</td>");
                            s.Append("     </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td>����<br />����</td>");
                            s.Append("       <td>" + sdr["Fileds06"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>���<br />֤��</td>");
                            s.Append("        <td>" + sdr["Fileds07"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>����<br />��λ</td>");
                            s.Append("       <td colspan=\"3\">" + sdr["Fileds08"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td>ĸ��<br />����</td>");
                            s.Append("       <td>" + sdr["Fileds09"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>���<br />֤��</td>");
                            s.Append("       <td>" + sdr["Fileds10"].ToString() + "&nbsp;</td>");
                            s.Append("        <td>����<br />��λ</td>");
                            s.Append("        <td colspan=\"3\">" + sdr["Fileds11"].ToString() + "&nbsp;</td>");
                            s.Append("        </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td rowspan=\"2\">�ƻ��������</td>");
                            s.Append("        <td>ũҵ<br />����</td>");
                            s.Append("       <td>��ũ<br />����</td>");
                            s.Append("       <td>��֤<br />���</td>");
                            s.Append("       <td colspan=\"2\">������ḧ<br />�������</td>");
                            s.Append("       <td colspan=\"2\">������<br />��ʵǩ��</td>");
                            s.Append("       </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td>&nbsp;</td>");
                            s.Append("       <td>&nbsp;</td>");
                            s.Append("       <td>&nbsp;</td>");
                            s.Append("       <td colspan=\"2\">&nbsp;</td>");
                            s.Append("       <td colspan=\"2\">&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("   </table></td>");
                            s.Append("       </tr>");
                            s.Append("       <tr>");
                            s.Append("         <td colspan=\"2\" align=\"right\" class=\"bt_03\">" + DateTime.Parse(sdr["ReportDate"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("      </table></td>");
                            s.Append("      <td class=\"right\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">�����и����������ͼƻ�������<br />����Ӥ��ʵ���Ǽǵ��������</td>");
                            s.Append("       </tr>");
                            s.Append("        <tr class=\"bt_02\">");
                            s.Append("          <td align=\"left\">&nbsp;</td>");
                            s.Append("         <td align=\"right\">���˵�&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��</td>");
                            s.Append("        </tr>");
                            s.Append("       <tr>");
                            s.Append("         <td colspan=\"2\" class=\"table_01\"><table width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("     <tr>");
                            s.Append("       <td width=\"50\">Ӥ��<br />����</td>");
                            s.Append("       <td width=\"80\">" + sdr["Fileds02"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"50\">�Ա�</td>");
                            s.Append("       <td width=\"80\">" + sdr["Fileds03"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"50\">����<br />����</td>");
                            s.Append("       <td width=\"80\">" + sdr["Fileds04"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"50\">̥��</td>");
                            s.Append("       <td width=\"80\">" + sdr["Fileds05"].ToString() + "&nbsp;</td>");
                            s.Append("     </tr>");
                            s.Append("     <tr>");
                            s.Append("      <td>����<br />����</td>");
                            s.Append("       <td>" + sdr["Fileds06"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>���<br />֤��</td>");
                            s.Append("      <td>" + sdr["Fileds07"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>����<br />��λ</td>");
                            s.Append("       <td colspan=\"3\">" + sdr["Fileds08"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td>ĸ��<br />����</td>");
                            s.Append("      <td>" + sdr["Fileds09"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>���<br />֤��</td>");
                            s.Append("       <td>" + sdr["Fileds10"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>����<br />��λ</td>");
                            s.Append("       <td colspan=\"3\">" + sdr["Fileds11"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td rowspan=\"2\">�ƻ��������</td>");                   
                            s.Append("       <td>ũҵ<br />����</td>");
                            s.Append("       <td>��ũ<br />����</td>");
                            s.Append("       <td>��֤<br />���</td>");
                            s.Append("      <td colspan=\"2\">������ḧ<br />�������</td>");
                            s.Append("       <td colspan=\"2\">������<br />��ʵǩ��</td>");
                            s.Append("      </tr>");
                            s.Append("    <tr>");
                            s.Append("      <td>&nbsp;</td>");
                            s.Append("       <td>&nbsp;</td>");
                            s.Append("       <td>&nbsp;</td>");
                            s.Append("       <td colspan=\"2\">&nbsp;</td>");
                            s.Append("       <td colspan=\"2\">&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("   </table></td>");
                            s.Append("       </tr>");
                            s.Append("      <tr>");
                            s.Append("         <td colspan=\"2\" align=\"right\" class=\"bt_03\">" + DateTime.Parse(sdr["ReportDate"].ToString()).ToString("yyyy��MM��dd��") + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("     </table></td>");
                            s.Append("   </tr>");
                            s.Append("</table>");
                            s.Append("</div>");
                            #endregion
                            break;
                        default:
                            s.Append("��ȡ������Ϣ����");
                            break;
                    }                   
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            this.LiteralData.Text = s.ToString();
        }


        #endregion


    }
}


