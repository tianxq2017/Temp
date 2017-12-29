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

using join.pms.dal;
using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.web
{
    public partial class UnvCommReport : System.Web.UI.Page
    {
        public string m_TargetUrl;
        protected string m_UrlParams;

        private string m_FuncCode;
        private string m_FuncName;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_SqlParams;

        private string m_SiteArea;
        private string m_SiteAreaNa;

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

                string startDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-01";
                string endDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01").AddDays(-1).ToString("yyyy-MM-dd");

                LoadingData(); // ���ر�������

                SetUIParams(startDate, endDate);
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
            m_FuncCode = Request.QueryString["FuncCode"];
            m_FuncName = Request.QueryString["FuncNa"];
            m_SiteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
            m_SiteAreaNa = System.Configuration.ConfigurationManager.AppSettings["SiteAreaName"];
            m_UrlParams = Request.Url.Query;
            if (!string.IsNullOrEmpty(m_FuncCode))
            {
                this.LiteralNav.Text = "��ǰλ�ã�" + CommPage.GetAllTreeName(this.m_FuncCode, true);
                m_UrlParams = "/UnvCommChart.aspx?" + m_UrlParams;
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }
        }

        #region  ��ȡ�����ļ�����
        // ������Ϣ
        private string m_FuncInfo;
        // ����
        private string m_Titles;
        // �ֶ�
        private string m_Fields;
        // �п�
        private string m_Width;
        // �ж��뷽ʽ
        private string m_Align;
        // �ַ���ʽ
        private string m_Format;
        // TABҳ����
        private string m_TabName;
        // TABҳ����
        private string m_TabLink;
        // TABҳ��ʾ
        private string m_TabVisible;
        // ��ť����
        private string m_ButName;
        // ��ť����
        private string m_ButLink;
        // ��ť��ʾ
        private string m_ButVisible;
        // ͨ������
        private string m_Query;
        // �ؼ����ݱ�ʶ
        private string m_Analys;

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
                //if (string.IsNullOrEmpty(this.FuncNo)) this.FuncNo = dr[0][0].ToString();
                this.m_FuncInfo = dr[0][1].ToString();
                this.m_Titles = dr[0][2].ToString();
                this.m_Fields = dr[0][3].ToString();
                this.m_Width = dr[0][4].ToString();
                this.m_Align = dr[0][5].ToString();
                this.m_Format = dr[0][6].ToString();
                //this.m_RowLink = dr[0][7].ToString();
                this.m_TabName = dr[0][8].ToString();
                this.m_TabLink = dr[0][9].ToString();
                this.m_TabVisible = dr[0][10].ToString();
                this.m_ButName = dr[0][11].ToString();
                this.m_ButLink = dr[0][12].ToString();
                this.m_ButVisible = dr[0][13].ToString();
                m_Ds = null;
                dr = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = "��ȡ�����ļ�����ʧ�ܣ�" + ex.Message;
                return false;
            }
        }
        #endregion

        private void LoadingData()
        {
            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGrid.config");
            if (GetConfigParams(GetBizCode(m_FuncCode), configFile, ref errMsg))
            {
                string startDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-01";
                string endDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01").AddDays(-1).ToString("yyyy-MM-dd");
                GetDataList(m_SiteArea, m_SiteAreaNa, startDate, endDate, false);
                SetUIParams(startDate, endDate);
            }
        }
       
        private string GetBizCode(string funcNo)
        {
            string returnVal = string.Empty;
            switch (funcNo)
            {
                case "0706":
                    returnVal = "0102";
                    break;
                case "0707":
                    returnVal = "0102";
                    break;
                case "0708":
                    returnVal = "0104";
                    break;
                case "0709":
                    returnVal = "0104";
                    break;
                case "0710":
                    returnVal = "0101";
                    break;
                case "0711":
                    returnVal = "0101";
                    break;
                default:
                    returnVal = "00";
                    break;
            }
            return returnVal;
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
            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGridBiz.config");
            try
            {
                if (GetConfigParams(GetBizCode(m_FuncCode), configFile, ref errMsg))
                {
                    GetDataList(areaCode, areaName, startDate, endDate, false);
                    SetUIParams(startDate, endDate);
                }
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message); }


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
                string columnName = "cols01,cols02";
                string strErr = string.Empty;
                string startDate = PageValidate.GetTrim(Request["txtStartTime"]);
                string endDate = PageValidate.GetTrim(Request["txtEndTime"]);
                string areaCode = this.DDLArea.SelectedItem.Value;
                string areaName = this.DDLArea.SelectedItem.Text;

                string searchStr = " CreateDate>='" + startDate + " 00:00:00' AND CreateDate<'" + endDate + " 23:59:59'";

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

                GetDataList(areaCode, areaName, startDate, endDate, true);
                SetUIParams(startDate, endDate);

                ExportDsToXls("pissStars", areaCode, areaName, startDate, endDate, columnName, m_Ds);
            }
            catch (Exception ex) { }
        }

        private void SetAreaList(string areaCode)
        {
            try
            {
                m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + m_SiteArea + "' ORDER BY AreaCode";
                DataTable tmpDt = new DataTable();
                tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                DDLArea.DataSource = tmpDt;
                DDLArea.DataTextField = "AreaName";
                DDLArea.DataValueField = "AreaCode";
                DDLArea.DataBind();
                DDLArea.Items.Insert(0, new ListItem(m_SiteAreaNa, m_SiteArea));
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
        private void GetDataList(string areaCode, string areaName, string startDate, string endDate, bool IsCreateDs)
        {
            string dbAreaCode = string.Empty, dbAreaName = string.Empty;
            string rowsData = string.Empty;
            if (string.IsNullOrEmpty(startDate))
            {
                startDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-01";
            }
            if (string.IsNullOrEmpty(endDate))
            {
                endDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01").AddDays(-1).ToString("yyyy-MM-dd");
            }
            StringBuilder sHtml = new StringBuilder();

            m_Titles = "����";
            m_Fields = "COUNT(0)";
            string[] aTitles = m_Titles.Split(',');
            string[] aFileds = m_Fields.Split(',');
            // OprateDate
            string sumFileds = m_Fields;
            string statInfo = areaName + "��" + startDate + "��" + endDate + "��";
            string searchStr = "CreateDate>='" + startDate + " 00:00:00' AND CreateDate<'" + endDate + " 23:59:59'";

            string bizCode = GetBizCode(m_FuncCode);

            if (bizCode == "0102") { searchStr += " AND (BizCode='0102' OR BizCode LIKE '0103%')"; }
            else { searchStr += " AND BizCode='" + bizCode + "' "; }


            //����ҵ������ȡָ��״̬��ҵ��Attribs: 0��Ч֤��; 1����֤��
            if (m_FuncCode == "0707" || m_FuncCode == "0709" || m_FuncCode == "0711") { searchStr += " AND Attribs=1"; }
            else { searchStr += " AND Attribs=0"; }

            // numeric(18,2)
            //if (!string.IsNullOrEmpty(sumFileds)) sumFileds = sumFileds.Substring(0, sumFileds.Length - 1);

            m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + areaCode + "' ORDER BY AreaCode";
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            sHtml.Append(statInfo + "<br/>");
            //==============================
            sHtml.Append("<table width=\"860\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
            sHtml.Append("<td width=\"50%\" height=\"22\">���ݵ�λ��" + areaName + "</td>");
            sHtml.Append("<td>ͳ��ʱ�Σ�" + startDate + " �� " + endDate + "</td>");
            sHtml.Append("</tr></table>");
            // ������ͷ
            sHtml.Append(GetTableCols(this.m_FuncCode, startDate, endDate));

            //�����б�ʶ

            // �����������ݼ�
            if (IsCreateDs)
            {
                for (int i = 0; i < aFileds.Length; i++)
                {
                    m_Ds.Tables[0].Columns.Add("cols" + i.ToString().PadLeft(2, '0'), typeof(string));
                }
            }
            // �������� 
            int aupPerson = 0;
            int tmpAup = 0;
            // �л��ܺϼ�
            int[] aryColAup = new int[aFileds.Length];
            for (int s = 0; s < aFileds.Length; s++)
            {
                aryColAup[s] = 0;
            }

            if (m_Dt.Rows.Count > 0)
            {
                for (int k = 0; k < m_Dt.Rows.Count; k++)
                {
                    dbAreaCode = m_Dt.Rows[k][0].ToString();
                    dbAreaName = m_Dt.Rows[k][1].ToString();

                    sHtml.Append("<tr bgcolor=\"#FFFFFF\" onmouseover=\"this.style.backgroundColor='#CCFFFF'\"  onmouseout=\"this.style.backgroundColor='#FFFFFF'\">");
                    sHtml.Append("<td bgcolor=\"#FFFFCC\" height=\"22\" width=\"200\">" + dbAreaName + "</td>");

                    //  SUM(CAST(IsNull(Fileds09,0) as int)),SUM(CAST(IsNull(Fileds10,0) as int)),''
                    //���ݵ���ʱ������������Ҫ����ʱ����ѡ��ȷ���������������嵽�塢����,�����޷�ͳ��
                    if (dbAreaCode.Substring(6) == "000000")
                    {
                        m_SqlParams = "SELECT " + sumFileds + " FROM v_BIZCertificates WHERE AreaCode LIKE '" + dbAreaCode.Substring(0, 6) + "%' AND " + searchStr + " ";
                    }
                    else if (dbAreaCode.Substring(9) == "000")
                    {
                        m_SqlParams = "SELECT " + sumFileds + " FROM v_BIZCertificates WHERE AreaCode LIKE '" + dbAreaCode.Substring(0, 9) + "%' AND " + searchStr + " ";
                    }
                    else
                    {
                        m_SqlParams = "SELECT " + sumFileds + " FROM v_BIZCertificates WHERE  AreaCode='" + dbAreaCode + "' AND " + searchStr + " ";
                    }

                    SqlDataReader sdr = null;
                    try
                    {
                        rowsData = "";
                        sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                        while (sdr.Read())
                        {
                            for (int j = 0; j < aFileds.Length; j++)
                            {
                                aryColAup[j] += int.Parse(IntFormat(sdr[j].ToString()));
                                rowsData += IntFormat(sdr[j].ToString()) + ",";
                                sHtml.Append("<td>&nbsp;" + IntFormatTo(sdr[j].ToString()) + "</td>");
                            }
                        }
                        sdr.Close();
                        if (!string.IsNullOrEmpty(rowsData)) rowsData = rowsData.Substring(0, rowsData.Length - 1);
                    }
                    catch
                    {
                        if (sdr != null) sdr.Close();
                    }
                    if (IsCreateDs)
                    {
                        string[] aVal = rowsData.Split(',');
                        DataRow dr = m_Ds.Tables[0].NewRow();
                        dr[0] = dbAreaName;
                        for (int j = 0; j < aFileds.Length; j++)
                        {
                            dr[j + 1] = aVal[j];
                        }
                        m_Ds.Tables[0].Rows.Add(dr);
                    }
                }

                sHtml.Append("<tr>");
                sHtml.Append("<td bgcolor=\"#CCFFFF\" height=\"22\">�ϼ�</td>");
                //if (this.m_FuncCode == "0701")
                //{
                //    // by ysl 2013/12/19 ���⴦��
                //    for (int l = 0; l < aFileds.Length - 3; l++)
                //    {
                //        sHtml.Append("<td bgcolor=\"#CCFFFF\">&nbsp;" + aryColAup[l].ToString() + "</td>");
                //    }
                //    float rateBirth = (float.Parse(aryColAup[1].ToString()) / float.Parse(aryColAup[0].ToString())) * 1000;//������
                //    float rateDeath = (float.Parse(aryColAup[13].ToString()) / float.Parse(aryColAup[0].ToString())) * 1000;//������
                //    float rateAdd = rateBirth - rateDeath;//������
                //    sHtml.Append("<td bgcolor=\"#CCFFFF\">&nbsp;" + rateBirth.ToString("F2") + "</td>");
                //    sHtml.Append("<td bgcolor=\"#CCFFFF\">&nbsp;" + rateDeath.ToString("F2") + "</td>");
                //    sHtml.Append("<td bgcolor=\"#CCFFFF\">&nbsp;" + rateAdd.ToString("F2") + "</td>");
                //}
                //else if (this.m_FuncCode == "0705")
                //{
                //    // by ysl 2013/12/24 ���⴦��
                //    for (int l = 0; l < aFileds.Length - 1; l++)
                //    {
                //        sHtml.Append("<td bgcolor=\"#CCFFFF\">&nbsp;" + aryColAup[l].ToString() + "</td>");
                //    }
                //    float svrBirth = (float.Parse(aryColAup[1].ToString()) / float.Parse(aryColAup[0].ToString())) * 100;//������
                //    sHtml.Append("<td bgcolor=\"#CCFFFF\">&nbsp;" + svrBirth.ToString("F1") + "</td>");
                //}

                //else
                //{
                    for (int l = 0; l < aFileds.Length; l++)
                    {
                        sHtml.Append("<td bgcolor=\"#CCFFFF\">&nbsp;" + aryColAup[l].ToString() + "</td>");
                    }
                //}

                sHtml.Append("</tr>");
                sHtml.Append("</table>");
            }
            else { sHtml.Append("û��ƥ������ݣ�"); }
            m_Dt = null;
            this.LiteralResults.Text = sHtml.ToString();
        }

        private string GetTableCols(string funcode, string startDate, string endDate)
        {
            StringBuilder sHtml = new StringBuilder();

            sHtml.Append("<table width=\"860\" border=\"0\" cellspacing=\"1\" cellpadding=\"2\" bgcolor=\"#999999\">");
            sHtml.Append("<tr class=\"tableTitle\">");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" align=\"center\">��λ</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" align=\"center\">����</td>");
            sHtml.Append("</tr>");

            return sHtml.ToString();
        }

        /// <summary>
        /// ��ȡ���ε�ͳ������
        /// </summary>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        private string GetAreaData(string sqlParams, ref int aup, ref string[] aryVal)
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
                Response.Write(ExportTable(columnName, areaCode, areaName, startDate, endDate, ds));
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

                GetTableCols(this.m_FuncCode, startDate, endDate);

                //д������  
                foreach (DataRow row in tb.Rows)
                {
                    count++;
                    sb.Append("<tr>");
                    foreach (DataColumn column in tb.Columns)
                    {
                        if (column.ColumnName.IndexOf("cols") > -1 && !column.ColumnName.Equals("cols01"))
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

        private string IntFormatTo(object inStr)
        {
            if (inStr == null) { return "0"; }
            else
            {
                if (PageValidate.IsNumberFormat(inStr.ToString().Trim()))
                {
                    if (inStr.ToString().Trim().IndexOf(".") > 0)
                    {
                        string newstr = inStr.ToString().Trim().Substring(inStr.ToString().Trim().IndexOf(".") + 1);
                        if (newstr.Length == 2) { return float.Parse(inStr.ToString().Trim()).ToString("F2"); }
                        else if (newstr.Length == 1) { return float.Parse(inStr.ToString().Trim()).ToString("F1"); }
                        else { return float.Parse(inStr.ToString().Trim()).ToString("F0"); }
                    }
                    else { return float.Parse(inStr.ToString().Trim()).ToString("F0"); }

                }
                else { return "0"; }
            }
        }

        private string IntFormat(object inStr)
        {
            if (inStr == null) { return "0"; }
            else
            {
                if (PageValidate.IsNumberFormat(inStr.ToString().Trim()))
                {
                    return float.Parse(inStr.ToString().Trim()).ToString("F0");
                }
                else { return "0"; }
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
