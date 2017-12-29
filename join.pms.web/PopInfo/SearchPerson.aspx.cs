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
using System.Data.SqlClient;
using UNV.Comm.Web;
using UNV.Comm.DataBase;

namespace AreWeb.OnlineCertificate.PopInfo
{
    public partial class SearchPerson : System.Web.UI.Page
    {
        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_FuncCode;
        private string m_SqlParams;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();

            if (!IsPostBack)
            {
                this.btnAdd.Attributes.Add("onclick", "show_query_hint('query_hint')");
                this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">��ʼҳ</a> &gt;&gt; �������� &gt;&gt;  �������� &gt;&gt; �˿���Ϣ�ۺϲ�ѯ��";
            }
        }

        #region �����֤����ʼ����Ϣ����
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
        #endregion

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string strErr = string.Empty;
            SqlDataReader sdr = null;
            StringBuilder s = new StringBuilder();
            string configFile = Server.MapPath("/includes/DataGridShare.config");
            string filterSQL = "FUNCNO='01010101'";

            string pName = PageValidate.GetTrim(this.txtName.Text);
            string pCid = PageValidate.GetTrim(this.txtCid.Text);            

            if (string.IsNullOrEmpty(pName) && string.IsNullOrEmpty(pCid))
            {
                MessageBox.Show(this, "�������ѯ������");
                return;
            }
            // AREACODE  000

            //if (!string.IsNullOrEmpty(areaCode))
            //{
            //    areaCode = areaCode.Replace("000", "");
            //    filterSQL += " AND AREACODE LIKE '" + areaCode + "%'";
            //}
            if (!string.IsNullOrEmpty(pName))
            {
                filterSQL += " AND FILEDS02 LIKE '%" + pName + "%'";
            }
            if (!string.IsNullOrEmpty(pCid))
            {
                filterSQL += " AND FILEDS04 LIKE '%" + pCid + "%'";
            }            
            try
            {
                GetConfigParams("01010101", configFile, ref strErr);
                string[] a_FuncInfo = this.m_FuncInfo.Split(',');
                string[] a_Titles = this.m_Titles.Split(',');
                string[] a_Fields = this.m_Fields.Split(',');
                string[] a_Width = this.m_Width.Split(',');
                string[] a_Align = this.m_Align.Split(',');
                string[] a_Format = this.m_Format.Split(',');
                //��ͷ ��ͷ
                s.Append("<table width=\"2600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                s.Append("<tr bgcolor=\"#258577\" style=\"color:#FFFFFF\">");
                for (int i = 0; i < a_Fields.Length; i++)
                {
                    s.Append("<th height=\"30\" width=\"" + a_Width[i] + "\">" + a_Titles[i] + "</th>");
                }
                s.Append("</tr>");

                m_SqlParams = "SELECT top 12 * FROM v_PisQyk WHERE " + filterSQL + " ORDER BY COMMID DESC";
                //����
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                //s.Append("<p><b>&nbsp;&nbsp;��פ�˿ڻ�����Ϣ��</b></p>");

                if (sdr.HasRows)
                {
                    int count = 0;
                    while (sdr.Read())
                    {
                        count++;
                        if (count % 2 != 0) { s.Append("<tr bgcolor=\"#FFFFFF\" onmouseover=\"this.style.backgroundColor='#E7F0F7'\" onmouseout=\"this.style.backgroundColor='#FFFFFF'\">"); }
                        else { s.Append("<tr bgcolor=\"#E6E6E6\" onmouseover=\"this.style.backgroundColor='#E7F0F7'\" onmouseout=\"this.style.backgroundColor='#E6E6E6'\">"); }
                        for (int j = 0; j < a_Fields.Length - 1; j++)
                        {
                            if (j == 3)
                            {
                                //ͨ�����֤�ż���,Ĭ����ʾ��һ�е�cid��Ӧ����ϲ
                                s.Append("<td " + GetAlignType(a_Align[j]) + " class=\"td1\" height=\"40\" width=\"" + a_Width[j] + "\"><a href='#' onclick=\"GePopInfoByCid('" + sdr[a_Fields[j]].ToString() + "')\">" + GetFormatType(a_Format[j], sdr[a_Fields[j]].ToString()) + "</a></td>");
                            }
                            else { s.Append("<td " + GetAlignType(a_Align[j]) + " class=\"td1\" height=\"40\" width=\"" + a_Width[j] + "\">" + GetFormatType(a_Format[j], sdr[a_Fields[j]].ToString()) + "</td>"); }
                        }
                        s.Append("</tr>");
                    }
                    sdr.Close(); sdr.Dispose();
                }
                else
                {
                    s.Append("<tr><td class=\"td1\" colspan=\"" + a_Titles.Length + "\"><br/><br/>δ�ҵ�����������������Ϣ������Ĳ�ѯ���������ԡ���<br/><br/></td></tr>");
                }
            }
            catch (Exception ex)
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }

                s.Append("��ȡ��Ϣ����" + strErr);
            }
            s.Append("</table>");
            this.LiteralData.Text = s.ToString();
            s = null;
        }



        #region  ��ȡ�����ļ����������뷽ʽ��

        /// <summary>
        /// ���뷽ʽ
        /// </summary>
        /// <param name="alignVal"></param>
        /// <returns></returns>
        private string GetAlignType(string alignVal)
        {
            string returnVal = string.Empty;
            switch (alignVal)
            {
                case "1":
                    returnVal = "align=\"center\"";
                    break;
                case "2":
                    returnVal = "align=\"right\"";
                    break;
                default:
                    returnVal = "align=\"left\"";
                    break;
            }
            return returnVal;
        }

        /// <summary>
        /// ��ʽ����
        /// </summary>
        /// <param name="formatVal"></param>
        /// <returns></returns>
        private string GetFormatType(string formatVal, string ioVal)
        {
            string returnVal = string.Empty;
            switch (formatVal)
            {
                case "1":
                    returnVal = String.Format("{0:d}", Convert.ToDateTime(ioVal));
                    break;
                case "2":
                    returnVal = String.Format("{0:F2}", Convert.ToDouble(ioVal));
                    break;
                default:
                    returnVal = PageValidate.GetTrim(ioVal);
                    break;
            }
            return returnVal;
        }

        private DataSet m_Ds;
        private string m_FuncInfo;
        private string m_Titles;
        private string m_Fields;
        private string m_Width;
        private string m_Align;
        private string m_Format;

        /// <summary>
        /// �������ݼ�
        /// </summary>
        private void ConfigDataSet()
        {
            m_Ds = new DataSet();
            m_Ds.Locale = System.Globalization.CultureInfo.InvariantCulture;
        }
        /// <summary>
        /// ��ȡ�����ļ�����
        /// </summary>
        /// <param name="funcNo">���ܺ�</param>
        /// <param name="configFile">�����ļ�·��</param>
        /// <returns></returns>
        public bool GetConfigParams(string funcNo, string configFile, ref string errorMsg)
        {
            try
            {
                ConfigDataSet();

                m_Ds.ReadXml(configFile, XmlReadMode.ReadSchema);
                DataRow[] dr = m_Ds.Tables[0].Select("FuncNo='" + funcNo + "'");

                this.m_FuncInfo = dr[0][1].ToString();
                this.m_Titles = dr[0][2].ToString();
                this.m_Fields = dr[0][3].ToString();
                this.m_Width = dr[0][4].ToString();
                this.m_Align = dr[0][5].ToString();
                this.m_Format = dr[0][6].ToString();

                m_Ds = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = "��ȡ�����ļ�����ʧ�ܣ�" + ex.Message;
                return false;
            }
        }

        #endregion
    }
}

