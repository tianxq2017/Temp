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

namespace join.pms.web.SysAdmin
{
    public partial class StatsByTime : System.Web.UI.Page
    {
        private string m_SqlParams;
        private DataTable m_Dt;
        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_FuncNo;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();

            m_FuncNo = PageValidate.GetFilterSQL(Request.QueryString["FuncCode"]);
            if (string.IsNullOrEmpty(m_FuncNo)) Server.Transfer("/errors.aspx");

            if (!IsPostBack)
            {
                SetUIParams("","");
                GetDataList("", "");
            }
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
        /// ���ý������,����ѡ��
        /// </summary>
        private void SetUIParams(string startDate, string endDate)
        {
            if (string.IsNullOrEmpty(startDate))
            {
                startDate = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
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
        /// <summary> 
        /// ��ȡ����,Ĭ��Ϊ��ǰ���� OprateTime
        /// </summary>
        private void GetDataList(string startDate, string endDate)
        {
            if (string.IsNullOrEmpty(startDate))
            {
                startDate = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
            }
            if (string.IsNullOrEmpty(endDate))
            {
                endDate = DateTime.Now.ToString("yyyy/MM/dd");
            }
            string statInfo = startDate + "��" + endDate;
            string searchStr = " ReportDate>='" + startDate + " 00:00:00' AND ReportDate<'" + endDate + " 23:59:59'";
            StringBuilder sHtml = new StringBuilder();
            this.LiteralNav.Text = "ͳ�Ʒ��� > �����Ź�����Ϣ������ʱ��ͳ�� ";

            m_SqlParams = "SELECT FuncName,COUNT(*) As GroupNum FROM v_PisStatsByFunc WHERE " + searchStr + " GROUP BY FuncName ORDER BY GroupNum DESC";
            sHtml.Append(statInfo + "�������Ź�����Ϣ����ͳ�����£�<br/>");
            
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];

            string maxVal = "0";
            string curVal = "0";
            float imgWidth = 0;
            // ���,����(��������)����,��Ϣ��������
            sHtml.Append("<table width=\"800\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" bgcolor=\"#008981\"><tr>");
            sHtml.Append("<td width=\"50\" height=\"30\" align=\"center\" bgcolor=\"#99d0d0\" style=\"color:#033;\"><strong>����</strong></td>");
            sHtml.Append("<td width=\"160\" class=\"fb01\" bgcolor=\"#99d0d0\" style=\"color:#033;\">&nbsp;<strong>���ݹ�������</strong></td>");
            sHtml.Append("<td width=\"60\" class=\"fb01\" bgcolor=\"#99d0d0\" style=\"color:#033;\">&nbsp;<strong>����</strong></td>");
            sHtml.Append("<td class=\"fb01\" bgcolor=\"#99d0d0\" style=\"color:#033;\">&nbsp;<strong>ͼʾ</strong></td>");
            sHtml.Append("</tr></table>");
            if (m_Dt.Rows.Count > 0)
            {
                maxVal = m_Dt.Rows[0][1].ToString();
                sHtml.Append("<table width=\"800\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" bgcolor=\"#008981\">");
                float sumA = 0;
                for (int k = 0; k < m_Dt.Rows.Count; k++)
                {
                    curVal = m_Dt.Rows[k][1].ToString();
                    imgWidth = (float.Parse(curVal) / float.Parse(maxVal)) * 200;

                    sumA += float.Parse(m_Dt.Rows[k][1].ToString());
                    sHtml.Append("<tr onmouseover=\"this.className='lvtColDataHover'\" onmouseout=\"this.className='lvtColData'\">");
                    sHtml.Append("<td width=\"50\" height=\"20\" align=\"center\" bgcolor=\"#FFFFFF\" >" + (k + 1).ToString() + "</td>");
                    sHtml.Append("<td width=\"160\"  align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][0].ToString() + "</td>");
                    sHtml.Append("<td width=\"60\" align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][1].ToString() + "</td>");
                    sHtml.Append("<td align=\"left\" bgcolor=\"#FFFFFF\"><img src=\"/images/LineGray.gif\" width=\"" + imgWidth.ToString("F0") + "\" height=\"20\" /></td>");
                    sHtml.Append("</tr>");
                }
                sHtml.Append("<tr onmouseover=\"this.className='lvtColDataHover'\" onmouseout=\"this.className='lvtColData'\">");
                sHtml.Append("<td height=\"20\" align=\"center\" bgcolor=\"#ffff99\" >�ϼ�</td>");
                sHtml.Append("<td align=\"left\" bgcolor=\"#ffff99\">&nbsp;</td>");
                sHtml.Append("<td align=\"left\" bgcolor=\"#ffff99\">&nbsp;" + sumA.ToString("F0") + "</td>");
                sHtml.Append("<td bgcolor=\"#ffff99\">&nbsp;</td>");
                sHtml.Append("</tr>");
                sHtml.Append("</table>");
            }
            else { sHtml.Append("û��ƥ������ݣ�"); }
            m_Dt = null;
            this.LiteralResults.Text = sHtml.ToString();
        }

        private string GetDaysInMonth(DateTime inDate)
        {
            return DateTime.DaysInMonth(inDate.Year, inDate.Month).ToString().PadLeft(2, '0');
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string startDate = PageValidate.GetTrim(Request["txtStartTime"]);
            string endDate = PageValidate.GetTrim(Request["txtEndTime"]);

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

            GetDataList(startDate, endDate);
            SetUIParams(startDate, endDate);
        }

        protected void butExport_Click(object sender, EventArgs e)
        {
            try
            {
                string columnName = "����,���Ź�����������,��Ϣ��������";
                string strErr = string.Empty;
                string startDate = PageValidate.GetTrim(Request["txtStartTime"]);
                string endDate = PageValidate.GetTrim(Request["txtEndTime"]);
                string searchStr = string.Empty;

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
                //���,����(��������)����,��Ϣ��������
                SetUIParams(startDate, endDate);
                GetDataList(startDate, endDate);

                searchStr = " OprateDate>='" + startDate + " 00:00:00' AND OprateDate<'" + endDate + " 23:59:59'";
                //m_SqlParams = "SELECT  '' As XuHao,CmsCName,COUNT(*) As ShareNum FROM v_CmsList WHERE CmsCode LIKE '02__' AND " + searchStr + " GROUP BY CmsCName ORDER BY ShareNum DESC";
                //�ж�ͳ������
                if (m_FuncNo == "0402")
                {
                    m_SqlParams = "SELECT  '' As XuHao,CmsCName,COUNT(*) As ShareNum FROM v_CmsList WHERE CmsCode LIKE '02__' AND " + searchStr + " GROUP BY CmsCName ORDER BY ShareNum DESC";
                }
                else if (m_FuncNo == "0403")
                {
                    m_SqlParams = "SELECT  '' As XuHao,CmsCName,COUNT(*) As ShareNum FROM v_CmsList WHERE CmsCode LIKE '03__' AND " + searchStr + " GROUP BY CmsCName ORDER BY ShareNum DESC";
                }
                else
                {
                    //m_SqlParams = "SELECT  '' As XuHao,CmsCName,COUNT(*) As ShareNum FROM v_CmsList WHERE (CmsCode LIKE '02__' OR CmsCode LIKE '03__') AND " + searchStr + " GROUP BY CmsCName ORDER BY ShareNum DESC";
                    m_SqlParams = "SELECT '' As XuHao,FuncNa,COUNT(*) As sNum FROM v_PIS_ForStats WHERE " + searchStr + " GROUP BY FuncNo,FuncNa ORDER BY sNum DESC";
                }
                ExportDsToXls("UnitStats", columnName, DbHelperSQL.Query(m_SqlParams));
            }
            catch (Exception ex) {  }
        }

        #region �������ݵķ���
        /// <summary>
        /// �������ݼ�ΪExcel
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="columnName">��ͷ��������1,��2,����</param>
        /// <param name="ds"></param>
        private void ExportDsToXls(string fileName, string columnName, DataSet ds)
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
                Response.Write(ExportTable(columnName, ds));
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
        private string ExportTable(string columnName, DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            string[] aryColumnName = columnName.Split(',');
            foreach (DataTable tb in ds.Tables)
            {
                sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">");
                sb.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                //д������ 
                sb.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
                for (int i = 0; i < aryColumnName.Length; i++)
                {
                    sb.AppendLine("<td>" + aryColumnName[i] + "</td>");
                }
                sb.AppendLine("</tr>");

                //д������  
                foreach (DataRow row in tb.Rows)
                {
                    count++;
                    sb.Append("<tr>");
                    foreach (DataColumn column in tb.Columns)
                    {
                        if (column.ColumnName.Equals("CustomerIDC") || column.ColumnName.Equals("CustomerMobile") || column.ColumnName.Equals("OutletsSerialNo"))
                        {
                            sb.Append("<td style=\"vnd.ms-excel.numberformat:@\">" + row[column].ToString() + "</td>");// ǿ�����ָ�ʽ
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