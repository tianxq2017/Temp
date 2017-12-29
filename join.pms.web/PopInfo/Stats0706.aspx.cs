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

using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace AreWeb.CertificateInOne.PopInfo
{
    public partial class Stats0706 : System.Web.UI.Page
    {
        public string m_TargetUrl;
        private string m_FuncCode;
        private string m_FuncName;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_SqlParams;

        private string m_StatusVal;

        private DataSet m_Ds;
        private DataTable m_Dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                SetAreaList("");
                this.LiteralNav.Text = "������ҳ  &gt;&gt; " + m_FuncName + "��";
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
                HttpCookie loginCookie = Request.Cookies["AKS_PISS_USER_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["AKS_PISS_USERID"] != null && !String.IsNullOrEmpty(Session["AKS_PISS_USERID"].ToString())) { returnVa = true; m_UserID = Session["AKS_PISS_USERID"].ToString(); }
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
            m_FuncCode = Request.QueryString["FuncCode"];
            m_FuncName = Request.QueryString["FuncNa"];
            // FuncCode=0706&FuncNa=%e4%ba%ba%e5%8f%a3%e5%87%ba%e7%94%9f%e6%83%85%e5%86%b5%e7%bb%9f%e8%ae%a1%e6%8a%a5%e8%a1%a8
            if (!string.IsNullOrEmpty(m_FuncCode))
            {

            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string startDate = PageValidate.GetTrim(Request["txtStartTime"]);
            string endDate = PageValidate.GetTrim(Request["txtEndTime"]);

            string areaCode = this.DDLArea.SelectedItem.Value;
            string areaName = this.DDLArea.SelectedItem.Text;

            if (string.IsNullOrEmpty(startDate))
            {
                strErr += "��ѡ����ʼ���ڣ�\\n";
            }
            if (string.IsNullOrEmpty(endDate))
            {
                strErr += "��ѡ���ֹ���ڣ�\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            GetDataList(areaCode, areaName,startDate, endDate,false);
            SetUIParams(startDate, endDate);
        }

        
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butExport_Click(object sender, EventArgs e)
        {
            try
            {
                string columnName = "cols01,cols02,cols03,cols04,cols05,cols06,cols07,cols08,cols09,cols10,cols11,cols12";
                string strErr = string.Empty;
                string startDate = PageValidate.GetTrim(Request["txtStartTime"]);
                string endDate = PageValidate.GetTrim(Request["txtEndTime"]);
                string areaCode = this.DDLArea.SelectedItem.Value;
                string areaName = this.DDLArea.SelectedItem.Text;

                string searchStr = "AuditFlag!=4 AND CustomerOpenTime>='" + startDate + " 00:00:00' AND CustomerOpenTime<'" + endDate + " 23:59:59'";
                if (string.IsNullOrEmpty(startDate))
                {
                    strErr += "��ѡ����ʼ���ڣ�\\n";
                }
                if (string.IsNullOrEmpty(endDate))
                {
                    strErr += "��ѡ���ֹ���ڣ�\\n";
                }
                if (strErr != "")
                {
                    MessageBox.Show(this, strErr);
                    return;
                }

                m_Ds = new DataSet();
                m_Ds.Tables.Add("Stats");

                GetDataList(areaCode, areaName, startDate, endDate,true);
                SetUIParams(startDate, endDate);

                ExportDsToXls("pissStars", areaCode, areaName, startDate, endDate, columnName, m_Ds);
            }
            catch (Exception ex) { }
        }

        private void SetAreaList(string areaCode)
        {
            try
            {
                string siteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
                m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + siteArea + "' ORDER BY AreaCode";
                DataTable tmpDt = new DataTable();
                tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                DDLArea.DataSource = tmpDt;
                DDLArea.DataTextField = "AreaName";
                DDLArea.DataValueField = "AreaCode";
                DDLArea.DataBind();
                DDLArea.Items.Insert(0, new ListItem("�˴����˿ںͼƻ�������", siteArea));
                tmpDt = null;
                if (!string.IsNullOrEmpty(areaCode))
                {
                    this.DDLArea.SelectedValue = areaCode;
                }
            }
            catch { }
             

        }

        /// <summary> 
        /// ��ȡ����,Ĭ��Ϊһ��
        /// </summary>
        private void GetDataList(string areaCode,string areaName,string startDate, string endDate,bool IsCreateDs)
        {
            string dbAreaCode = string.Empty, dbAreaName = string.Empty;
            if (string.IsNullOrEmpty(startDate))
            {
                startDate = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
            }
            if (string.IsNullOrEmpty(endDate))
            {
                endDate = DateTime.Now.ToString("yyyy/MM/dd");
            }
            StringBuilder sHtml = new StringBuilder();

            string statInfo = areaName + "��" + startDate + "��" + endDate + "�˿ڳ��������";
            string searchStr = "AuditFlag!=4 AND CustomerOpenTime>='" + startDate + " 00:00:00' AND CustomerOpenTime<'" + endDate + " 23:59:59'";

            m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + areaCode + "' ORDER BY AreaCode";
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            sHtml.Append(statInfo + "<br/>");
            /*
    sHtml.Append("<table width=\"800\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\"> <tr>");
    sHtml.Append("<td rowspan=\"2\" align=\"center\">��λ</td>");
    sHtml.Append("<td rowspan=\"2\" align=\"center\">�����ϼ�</td>");
    sHtml.Append("<td colspan=\"3\" align=\"center\">һ������</td>");
    sHtml.Append("<td colspan=\"3\">��������</td>");
    sHtml.Append("<td colspan=\"3\">�ຢ����</td>");
    sHtml.Append("<td rowspan=\"2\">����</td>");
    sHtml.Append("</tr><tr>");
    sHtml.Append("<td>��</td>");
    sHtml.Append("<td>Ů</td>");
    sHtml.Append("<td>������</td>");
    sHtml.Append("<td>��</td>");
    sHtml.Append("<td>Ů</td>");
    sHtml.Append("<td>������</td>");
    sHtml.Append("<td>��</td>");
    sHtml.Append("<td>Ů</td>");
    sHtml.Append("<td>������</td>");
    sHtml.Append("</tr>");
**
  <tr>
    sHtml.Append("<td bgcolor=\"#FFFFCC\">��</td>");
    sHtml.Append("<td>1</td>");
    sHtml.Append("<td>2</td>");
    sHtml.Append("<td>3</td>");
    sHtml.Append("<td>4</td>");
    sHtml.Append("<td>5</td>");
    sHtml.Append("<td>6</td>");
    sHtml.Append("<td>7</td>");
    sHtml.Append("<td>8</td>");
    sHtml.Append("<td>9</td>");
    sHtml.Append("<td>10</td>");
    sHtml.Append("<td>11</td>");
  </tr>
</table>
             */

            //==============================
            sHtml.Append("<table width=\"860\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
            sHtml.Append("<td width=\"50%\" height=\"22\">���ݵ�λ��" + areaName + "</td>");
            sHtml.Append("<td>ͳ��ʱ�Σ�"+startDate + " �� " + endDate+"</td>");
            sHtml.Append("</tr></table>");
            // ��ͷ
            sHtml.Append("<table width=\"860\" border=\"0\" cellspacing=\"1\" cellpadding=\"2\" bgcolor=\"#999999\"><tr class=\"tableTitle\">");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" rowspan=\"2\" align=\"center\">��λ</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" rowspan=\"2\" align=\"center\">�����ϼ�</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" colspan=\"3\" align=\"center\" height=\"20\">һ������</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" colspan=\"3\">��������</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" colspan=\"3\">�ຢ����</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" rowspan=\"2\">����</td>");
            sHtml.Append("</tr><tr class=\"tableTitle\">");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" height=\"22\">��</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" >Ů</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" >������</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" >��</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" >Ů</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" >������</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" >��</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" >Ů</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" >������</td>");
            sHtml.Append("</tr>");
            //�����б�ʶ
            /*
            sHtml.Append("<tr>");
            sHtml.Append("<td bgcolor=\"#FFFFCC\">��</td>");
            sHtml.Append("<td>1</td>");
            sHtml.Append("<td>2</td>");
            sHtml.Append("<td>3</td>");
            sHtml.Append("<td>4</td>");
            sHtml.Append("<td>5</td>");
            sHtml.Append("<td>6</td>");
            sHtml.Append("<td>7</td>");
            sHtml.Append("<td>8</td>");
            sHtml.Append("<td>9</td>");
            sHtml.Append("<td>10</td>");
            sHtml.Append("<td>11</td>");
            sHtml.Append("</tr>");
             * */
            // �����������ݼ�
            if (IsCreateDs) {
                m_Ds.Tables[0].Columns.Add("cols01", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols02", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols03", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols04", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols05", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols06", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols07", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols08", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols09", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols10", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols11", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols12", typeof(string));
            }
            // ����
            int aupPerson = 0;
            int tmpAup=0;
            if (m_Dt.Rows.Count > 0)
            {
                for (int k = 0; k < m_Dt.Rows.Count; k++)
                {
                    dbAreaCode = m_Dt.Rows[k][0].ToString();
                    dbAreaName = m_Dt.Rows[k][1].ToString();
                    
                    sHtml.Append("<tr bgcolor=\"#FFFFFF\" onmouseover=\"this.style.backgroundColor='#CCFFFF'\"  onmouseout=\"this.style.backgroundColor='#FFFFFF'\">");
                    sHtml.Append("<td bgcolor=\"#FFFFCC\" height=\"22\" width=\"200\">" + dbAreaName + "</td>");
                    
                    
                    //���ݵ���ʱ������������Ҫ����ʱ����ѡ��ȷ����������,�����޷�ͳ��
                    // ��,Ů,������ string[] a = new string[] { };
                    m_SqlParams = "";
                    // ��ȡ��ǰ����Ͻ��1���������
                    if (dbAreaCode.Substring(9) == "000") { m_SqlParams = "SELECT SUM(CAST(IsNull(Fileds09,0) as int)),SUM(CAST(IsNull(Fileds10,0) as int)),'' FROM PIS_QYK WHERE FuncNo='0505' AND Fileds11='1' AND AreaCode LIKE '" + dbAreaCode.Substring(0,9) + "%'"; }
                    else { m_SqlParams = "SELECT SUM(CAST(IsNull(Fileds09,0) as int)),SUM(CAST(IsNull(Fileds10,0) as int)),'' FROM PIS_QYK WHERE FuncNo='0505' AND Fileds11='1' AND AreaCode='" + dbAreaCode + "'"; }
                    
                    string[] aryVal1 = new string[] {"0","0","0"};
                    string baby1 = GetAreaData(m_SqlParams, ref tmpAup,ref aryVal1);
                    aupPerson = tmpAup;
                    // ��ȡ��ǰ����Ͻ��2���������
                    if (dbAreaCode.Substring(9) == "000") { m_SqlParams = "SELECT SUM(CAST(IsNull(Fileds09,0) as int)),SUM(CAST(IsNull(Fileds10,0) as int)),'' FROM PIS_QYK WHERE FuncNo='0505' AND Fileds11='2' AND AreaCode LIKE '" + dbAreaCode.Substring(0, 9) + "%'"; }
                    else { m_SqlParams = "SELECT SUM(CAST(IsNull(Fileds09,0) as int)),SUM(CAST(IsNull(Fileds10,0) as int)),'' FROM PIS_QYK WHERE FuncNo='0505' AND Fileds11='2' AND AreaCode='" + dbAreaCode + "'"; }

                    string[] aryVal2 = new string[] { "0", "0", "0" };
                    string baby2 = GetAreaData(m_SqlParams, ref tmpAup, ref aryVal2);
                    aupPerson += tmpAup;
                    // ��ȡ��ǰ����Ͻ���ຢ�������
                    if (dbAreaCode.Substring(9) == "000") { m_SqlParams = "SELECT SUM(CAST(IsNull(Fileds09,0) as int)),SUM(CAST(IsNull(Fileds10,0) as int)),'' FROM PIS_QYK WHERE FuncNo='0505' AND Fileds11='2' AND AreaCode LIKE '" + dbAreaCode.Substring(0, 9) + "%'"; }
                    else { m_SqlParams = "SELECT SUM(CAST(IsNull(Fileds09,0) as int)),SUM(CAST(IsNull(Fileds10,0) as int)),'' FROM PIS_QYK WHERE FuncNo='0505' AND Fileds11='2' AND AreaCode='" + dbAreaCode + "'"; }

                    string[] aryVal3 = new string[] { "0", "0", "0" };
                    string babyN = GetAreaData(m_SqlParams, ref tmpAup, ref aryVal3);
                    aupPerson += tmpAup;

                    if (IsCreateDs) {
                        DataRow dr = m_Ds.Tables[0].NewRow();
                        dr[0] = dbAreaName;
                        dr[1] = aupPerson;
                        dr[2] = aryVal1[0];
                        dr[3] = aryVal1[1];
                        dr[4] = aryVal1[2];
                        dr[5] = aryVal2[0];
                        dr[6] = aryVal2[1];
                        dr[7] = aryVal2[2];
                        dr[8] = aryVal3[0];
                        dr[9] = aryVal3[1];
                        dr[10] = aryVal3[2];
                        dr[11] = "";
                        m_Ds.Tables[0].Rows.Add(dr);
                    }
                    sHtml.Append("<td>" + aupPerson + "</td>");
                    sHtml.Append(baby1);
                    sHtml.Append(baby2);
                    sHtml.Append(babyN);
                    sHtml.Append("<td>&nbsp;</td>"); // ������

                }
                /*
                sHtml.Append("<tr>");
                sHtml.Append("<td bgcolor=\"#FFFFCC\" height=\"20\">�ϼ�</td>");
                sHtml.Append("<td bgcolor=\"#FFFFCC\">&nbsp;</td>");
                sHtml.Append("<td bgcolor=\"#FFFFCC\">&nbsp;</td>");
                sHtml.Append("<td bgcolor=\"#FFFFCC\">&nbsp;" + sumA.ToString() + "</td>");//" + sumA.ToString("F2") + "(" + sumB.ToString("F2") + ")
                sHtml.Append("<td bgcolor=\"#FFFFCC\">&nbsp;" + sumB.ToString() + "</td>");
                sHtml.Append("<td bgcolor=\"#FFFFCC\">&nbsp;" + sumC.ToString() + "</td>");
                sHtml.Append("<td bgcolor=\"#FFFFFF\">&nbsp;</td>");
                sHtml.Append("</tr>");*/
                sHtml.Append("</table>");
            }
            else { sHtml.Append("û��ƥ������ݣ�"); }
            m_Dt = null;
            this.LiteralResults.Text = sHtml.ToString();
        }

        /// <summary>
        /// ��ȡ���ε�ͳ������
        /// </summary>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        private string GetAreaData(string sqlParams,ref int aup,ref string[] aryVal)
        {
            string returnVal = "<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>";

            SqlDataReader sdr = null;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    aryVal[0] = IntFormat(sdr[0].ToString());
                    aryVal[1] = IntFormat(sdr[1].ToString());
                    aryVal[2] = IntFormat(sdr[2].ToString());
                    aup = int.Parse(aryVal[0]) + int.Parse(aryVal[1]);
                    returnVal = "<td>" + aryVal[0] + "</td><td>" + IntFormat(sdr[1].ToString()) + "</td><td>" + aryVal[2] + "</td>";
                }
                sdr.Close();
            }
            catch
            {
                if (sdr != null) sdr.Close();
            }
            return returnVal;
        }

        #region �������ݵķ���
        /// <summary>
        /// �������ݼ�ΪExcel
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="columnName">��ͷ��������1,��2,����</param>
        /// <param name="ds"></param>
        private void ExportDsToXls(string fileName, string areaCode, string areaName, string startDate, string endDate, string columnName, DataSet ds)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "GB2312";
            //page.Response.Charset = "UTF-8";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + System.DateTime.Now.ToString("_yyyyMMdd_hhmm") + ".xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//���������Ϊ��������
            Response.ContentType = "application/ms-excel";//��������ļ�����Ϊexcel�ļ��� 
            EnableViewState = false;
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(columnName) || ds.Tables[0].Rows.Count < 1)
            {
                Response.Write("����ʧ�ܣ����������Դ����Ϊ�գ�");
            }
            else
            {
                Response.Write(ExportTable(columnName,areaCode, areaName, startDate, endDate, ds));
            }

            ds.Dispose();
            Response.End();
        }
        /// <summary>
        /// �������ݱ��
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        private string ExportTable(string columnName, string areaCode, string areaName, string startDate, string endDate, DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            string[] aryColumnName = columnName.Split(',');
            foreach (DataTable tb in ds.Tables)
            {
                sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">");
                sb.Append("<table width=\"860\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
                sb.Append("<td colspan=\"12\" align=\"center\" height=\"30\" style=\"font-size:18pt;font-weight: bold; white-space: nowrap;\">�˿ڳ������ͳ�Ʊ���</td>");
                sb.Append("</tr></table>");
                // ��ͷ��λ
                sb.Append("<table width=\"860\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
                sb.Append("<td colspan=\"6\" width=\"50%\" height=\"22\">���ݵ�λ��" + areaName + "</td>");
                sb.Append("<td colspan=\"6\">ͳ��ʱ�Σ�" + startDate + " �� " + endDate + "</td>");
                sb.Append("</tr></table>");
                sb.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                //��ͷ�ֶ�
                sb.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
                sb.Append("<td bgcolor=\"#DDDDDD\" rowspan=\"2\" align=\"center\">��λ</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" rowspan=\"2\" align=\"center\">�����ϼ�</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" colspan=\"3\" align=\"center\" height=\"20\">һ������</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" colspan=\"3\">��������</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" colspan=\"3\">�ຢ����</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" rowspan=\"2\">����</td>");
                sb.Append("</tr><tr class=\"tableTitle\">");
                sb.Append("<td bgcolor=\"#DDDDDD\" height=\"22\">��</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" >Ů</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" >������</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" >��</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" >Ů</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" >������</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" >��</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" >Ů</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" >������</td>");
                sb.Append("</tr>");

                //д������  
                foreach (DataRow row in tb.Rows)
                {
                    count++;
                    sb.Append("<tr>");
                    foreach (DataColumn column in tb.Columns)
                    {
                        if (column.ColumnName.IndexOf("cols") > -1 && !column.ColumnName.Equals("cols01") )
                        {
                            sb.Append("<td height=\"22\" style=\"vnd.ms-excel.numberformat:@\">" + row[column].ToString() + "</td>");// ǿ�����ָ�ʽ
                        }
                        else if (column.ColumnName.Equals("XuHao"))
                        {
                            sb.Append("<td>" + count.ToString() + "</td>");
                        }
                        else if (column.ColumnName.Equals("CustomerOpenTime") || column.ColumnName.Equals("CustomerActiveTime"))
                        {
                            sb.Append("<td>" + GetFormatDate(row[column].ToString()) + "</td>");
                        }
                        else
                        {
                            sb.Append("<td>" + row[column].ToString() + "</td>");
                        }
                    }
                    sb.AppendLine("</tr>");

                }
                sb.AppendLine("</table>");
            }
            return sb.ToString();
        }

        /// <summary>
        /// ���ý������,����ѡ��
        /// </summary>
        private void SetUIParams(string startDate, string endDate)
        {
            if (string.IsNullOrEmpty(startDate))
            {
                startDate = DateTime.Now.AddDays(-10).ToString("yyyy/MM/dd");
            }
            if (string.IsNullOrEmpty(endDate))
            {
                endDate = DateTime.Now.ToString("yyyy/MM/dd");
            }
            this.txtStartTime.Value = startDate;
            this.txtEndTime.Value = endDate;
        }

        private string FloatFormat(object inStr)
        {
            if (inStr == null) { return "0"; }
            else { return inStr.ToString().Trim(); }
        }

        private string IntFormat(object inStr)
        {
            if (inStr == null) { return "0"; }
            else {
                if (PageValidate.IsNumber(inStr.ToString().Trim()))
                {
                    return inStr.ToString().Trim(); 
                }
                else { return  "0"; }
                
            }
        }
        
        /// <summary>
        /// ��ʽ������
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static string GetFormatDate(string inputData)
        {
            if (String.IsNullOrEmpty(inputData))
            {
                return inputData;
            }
            else
            {
                if (PageValidate.IsDateTime(inputData.Trim()))
                {
                    return DateTime.Parse(inputData.Trim()).ToString("yyyy/MM/dd");
                }
                else { return inputData.Trim(); }
            }
        }
        #endregion
    }
}
