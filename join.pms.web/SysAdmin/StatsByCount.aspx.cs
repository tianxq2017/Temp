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
    public partial class StatsByCount : System.Web.UI.Page
    {
        private string m_SqlParams;
        private DataTable m_Dt;
        private string m_UserID; // 当前登录的操作用户编号

        /// <summary>
        /// 页面初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();

            if (!IsPostBack)
            {
                this.LiteralNav.Text = "统计分析 > 用户使用情况按条件统计 ";
                SetUIParams("", "", "", "");
                GetDataList("", "", "", "", "", "");
            }
        }

        /// <summary>
        /// 身份验证
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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/loginTemp.aspx';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 设置界面参数,年月选择
        /// </summary>
        private void SetUIParams(string startDate, string endDate, string startBJDate, string endBJDate)
        {
            if (string.IsNullOrEmpty(startDate))
            {
                startDate = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
            }
            if (string.IsNullOrEmpty(endDate))
            {
                endDate = DateTime.Now.ToString("yyyy/MM/dd");
            }
            if (string.IsNullOrEmpty(startBJDate))
            {
                startBJDate = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
            }
            if (string.IsNullOrEmpty(endBJDate))
            {
                endBJDate = DateTime.Now.ToString("yyyy/MM/dd");
            }
            this.txtStartTime.Value = startDate;
            this.txtEndTime.Value = endDate;
            this.txtBJStartTime.Value = startBJDate;
            this.txtBJEndTime.Value = endBJDate;
        }

        /// <summary>
        /// 字符串去空格
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        private string FloatFormat(object inStr)
        {
            if (inStr == null) { return "0"; }
            else { return inStr.ToString().Trim(); }
        }

        /// <summary> 
        /// 获取数据
        /// </summary>
        private void GetDataList(string startDate, string endDate, string startBJDate, string endBJDate, string strBZAttribs, string strBJType)
        {
            string maxVal = "0";
            string curVal = "0";
            string searchStr = "";
            float imgWidth = 0;

            //and Attribs = 2 and BizCode = '0101' and (StartDate >='' and StartDate <='') and (FinalDate >='' and FinalDate <='')

            if (string.IsNullOrEmpty(startDate))
            {
                startDate = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
            }
            if (string.IsNullOrEmpty(endDate))
            {
                endDate = DateTime.Now.ToString("yyyy/MM/dd");
            }
            searchStr = " AND ( StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59') ";

            if (string.IsNullOrEmpty(startBJDate))
            {
                startBJDate = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
            }
            if (string.IsNullOrEmpty(endBJDate))
            {
                endBJDate = DateTime.Now.ToString("yyyy/MM/dd");
            }
            searchStr = searchStr + " AND ( FinalDate>='" + startBJDate + " 00:00:00' AND FinalDate<'" + endBJDate + " 23:59:59') ";
            searchStr = searchStr + " AND Attribs='" + strBZAttribs + "'";
            searchStr = searchStr + " AND BizCode='" + strBJType + "'";

            StringBuilder sHtml = new StringBuilder();
            try
            {
                m_SqlParams = "select BizCode,BizName,count(*) numcount from BIZ_Contents where 1=1 " + searchStr + " group by BizCode,BizName order by BizCode,BizName";
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                sHtml.Append("系统应用情况统计如下：<br/>");
                sHtml.Append("<table width=\"900\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" bgcolor=\"#666666\"><tr>");
                sHtml.Append("<td width=\"150\" height=\"30\" align=\"center\" bgcolor=\"#cccccc\" ><strong>序号</strong></td>");
                sHtml.Append("<td width=\"250\" class=\"fb01\" bgcolor=\"#cccccc\" >&nbsp;<strong>业务编号</strong></td>");
                sHtml.Append("<td width=\"350\" class=\"fb01\" bgcolor=\"#cccccc\" >&nbsp<strong>业务名称</strong></td>");
                sHtml.Append("<td width=\"150\" class=\"fb01\" bgcolor=\"#cccccc\" align=\"center\" >&nbsp;<strong>统计数</strong></td>");
                sHtml.Append("</tr></table>");
                if (m_Dt.Rows.Count > 0)
                {
                    maxVal = m_Dt.Rows[0][2].ToString();
                    sHtml.Append("<table width=\"900\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" bgcolor=\"#666666\">");
                    // <img src=\"/images/LineGray.gif\" width=\"16\" height=\"20\" />
                    for (int k = 0; k < m_Dt.Rows.Count; k++)
                    {
                        sHtml.Append("<tr onmouseover=\"this.className='lvtColDataHover'\" onmouseout=\"this.className='lvtColData'\">");
                        sHtml.Append("<td width=\"150\" height=\"20\" align=\"center\" bgcolor=\"#FFFFFF\" >" + (k + 1).ToString() + "</td>");
                        sHtml.Append("<td width=\"250\"  align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][0].ToString() + "</td>");
                        sHtml.Append("<td width=\"350\" align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][1].ToString() + "</td>");
                        sHtml.Append("<td width=\"150\" align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][2].ToString() + "</td>");
                        sHtml.Append("</tr>");
                    }
                    sHtml.Append("</table>");
                }
                else { sHtml.Append("没有匹配的数据！"); }
                m_Dt = null;

            }
            catch (Exception ex) { sHtml.Append(ex.Message); }
            this.LiteralResults.Text = sHtml.ToString();

        }

        /// <summary>
        /// 取得月份
        /// </summary>
        /// <param name="inDate"></param>
        /// <returns></returns>
        private string GetDaysInMonth(DateTime inDate)
        {
            return DateTime.DaysInMonth(inDate.Year, inDate.Month).ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// 检索事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string startDate = PageValidate.GetTrim(Request["txtStartTime"]);
            string endDate = PageValidate.GetTrim(Request["txtEndTime"]);

            string startBJDate = PageValidate.GetTrim(Request["txtBJStartTime"]);
            string endBJDate = PageValidate.GetTrim(Request["txtBJEndTime"]);

            if (string.IsNullOrEmpty(startDate))
            {
                strErr += "请选择申请起始日期！\\n";
            }
            if (string.IsNullOrEmpty(endDate))
            {
                strErr += "请选择申请截止日期！\\n";
            }
            if (string.IsNullOrEmpty(startBJDate))
            {
                strErr += "请选择办证起始日期！\\n";
            }
            if (string.IsNullOrEmpty(endBJDate))
            {
                strErr += "请选择办证截止日期！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string txtBZAttribs = this.ddlBZAttribs.SelectedValue;
            string txtBJType = this.ddlBJType.SelectedValue;

            GetDataList(startDate, endDate, startBJDate, endBJDate, txtBZAttribs, txtBJType);
            SetUIParams(startDate, endDate, startBJDate, endBJDate);
        }

        //protected void butExport_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string columnName = "序号,用户(登录名),归属部门,登录次数";
        //        string strErr = string.Empty;
        //        string startDate = PageValidate.GetTrim(Request["txtStartTime"]);
        //        string endDate = PageValidate.GetTrim(Request["txtEndTime"]);
        //        string searchStr = string.Empty;

        //        if (string.IsNullOrEmpty(startDate))
        //        {
        //            strErr += "请选择起始日期！\\n";
        //        }
        //        if (string.IsNullOrEmpty(endDate))
        //        {
        //            strErr += "请选择截止日期！\\n";
        //        }
        //        if (strErr != "")
        //        {
        //            MessageBox.Show(this, strErr);
        //            return;
        //        }

        //        searchStr = " OprateDate>='" + startDate + " 00:00:00' AND OprateDate<'" + endDate + " 23:59:59'";

        //        SetUIParams(startDate, endDate);
        //        GetDataList(startDate, endDate);

        //        m_SqlParams = "SELECT '' As XuHao,UserAccount,UserName,COUNT(*) As GroupNum FROM v_SysLogs WHERE OprateModel='登录' AND " + searchStr + " GROUP BY UserAccount,UserName ORDER BY GroupNum DESC";
        //        // m_SqlParams = "SELECT UserAccount,UserName,COUNT(*) As GroupNum FROM v_SysLogs WHERE OprateModel='登录' AND " + searchStr + " GROUP BY UserAccount,UserName ORDER BY GroupNum DESC";
        //        ExportDsToXls("LoginStatsByTime", columnName, DbHelperSQL.Query(m_SqlParams));
        //    }
        //    catch (Exception ex) { }
        //}

        #region 导出数据的方法
        /// <summary>
        /// 导出数据集为Excel
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="columnName">表头列名：列1,列2,……</param>
        /// <param name="ds"></param>
        private void ExportDsToXls(string fileName, string columnName, DataSet ds)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "GB2312";
            //page.Response.Charset = "UTF-8";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + System.DateTime.Now.ToString("_yyyyMMdd_hhmm") + ".xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文
            Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
            EnableViewState = false;
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(columnName) || ds.Tables[0].Rows.Count < 1)
            {
                Response.Write("操作失败：参数错误或源数据为空！");
            }
            else
            {
                Response.Write(ExportTable(columnName, ds));
            }

            ds.Dispose();
            Response.End();
        }
        /// <summary>
        /// 导出数据表格
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
                //写出列名 
                sb.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
                for (int i = 0; i < aryColumnName.Length; i++)
                {
                    sb.AppendLine("<td>" + aryColumnName[i] + "</td>");
                }
                sb.AppendLine("</tr>");

                //写出数据  
                foreach (DataRow row in tb.Rows)
                {
                    count++;
                    sb.Append("<tr>");
                    foreach (DataColumn column in tb.Columns)
                    {
                        if (column.ColumnName.Equals("BizCode") || column.ColumnName.Equals("numcount"))
                        {
                            sb.Append("<td style=\"vnd.ms-excel.numberformat:@\">" + row[column].ToString() + "</td>");// 强制数字格式
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
        /// 格式化日期
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
