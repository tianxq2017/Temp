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
using join.pms.dal;

namespace join.pms.web.SysAdmin
{
    public partial class StatsByShare : System.Web.UI.Page
    {
        private string m_SqlParams;
        private DataTable m_Dt;
        private string m_UserID; // 当前登录的操作用户编号

        //private string m_SiteArea;
        //private string m_SiteAreaNa;
        private string m_UserAreaName;

        protected void Page_Load(object sender, EventArgs e)
        {
            //m_SiteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
            //m_SiteAreaNa = System.Configuration.ConfigurationManager.AppSettings["SiteAreaName"];

            AuthenticateUser();

            if (!IsPostBack)
            {
                this.LiteralNav.Text = "统计分析 > 各类业务办理综合统计表 ";
                SetUIParams("", "");
                //SetAreaList("");
                GetDataList("", "");
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
        /// 获取数据
        /// </summary>
        private void GetDataList(string startDate, string endDate)
        {
            string statInfo = startDate + "至" + endDate;
            string searchStr = " StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59'" + GetFilter();
            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
            {
                statInfo = "截止目前";
                searchStr = "1=1";
            }

            StringBuilder sHtml = new StringBuilder();
            try
            {
                // SELECT MAX(UserLoginNum) FROM v_UserList
                //maxVal = DbHelperSQL.GetSingle("SELECT MAX(UserLoginNum) FROM v_UserList").ToString();
                m_SqlParams = "SELECT BizCode,(SELECT BizNameAB FROM BIZ_Categories WHERE BizCode=A.BizCode) As BizName,COUNT(*) As TotalNum,";
                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE Attribs IN(2,9) AND BizCode=A.BizCode AND " + searchStr + ") As bizPass,";
                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE Attribs IN(4,5) AND BizCode=A.BizCode AND " + searchStr + ") As bizBo,";
                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE Attribs IN(0,1,3,6) AND BizCode=A.BizCode AND " + searchStr + ") As bizWait ";
                m_SqlParams += " FROM BIZ_Contents A WHERE " + searchStr + " GROUP BY BizCode";

                /*
                 SELECT BizCode,BizName,COUNT(*) As TotalNum,
(SELECT COUNT(*) FROM BIZ_Contents WHERE Attribs IN(2,9) AND BizCode=A.BizCode) As bizPass,
(SELECT COUNT(*) FROM BIZ_Contents WHERE Attribs IN(4,5) AND BizCode=A.BizCode) As bizBo,
(SELECT COUNT(*) FROM BIZ_Contents WHERE Attribs IN(0,1,3,6) AND BizCode=A.BizCode) As bizWait 
 FROM BIZ_Contents A GROUP BY BizCode,BizName
                 */
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                sHtml.Append(statInfo + "，" + m_UserAreaName + "各类业务办理情况统计如下：<br/>");
                sHtml.Append("<table width=\"900\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" bgcolor=\"#666666\"><tr>");
                sHtml.Append("<td width=\"60\" height=\"30\" align=\"center\" bgcolor=\"#cccccc\" ><strong>序号</strong></td>");
                sHtml.Append("<td width=\"80\" class=\"fb01\" bgcolor=\"#cccccc\" >&nbsp;<strong>业务编号</strong></td>");
                sHtml.Append("<td class=\"fb01\" bgcolor=\"#cccccc\" >&nbsp;<strong>业务名称</strong></td>");
                sHtml.Append("<td width=\"80\" class=\"fb01\" bgcolor=\"#cccccc\" >&nbsp<strong>辖区总数</strong></td>");
                sHtml.Append("<td width=\"80\" class=\"fb01\" bgcolor=\"#cccccc\" >&nbsp;<strong>审核通过</strong></td>");
                sHtml.Append("<td width=\"80\" class=\"fb01\" bgcolor=\"#cccccc\" >&nbsp;<strong>撤销注销</strong></td>");
                sHtml.Append("<td width=\"80\" class=\"fb01\" bgcolor=\"#cccccc\" >&nbsp;<strong>正在办理</strong></td>");
                sHtml.Append("</tr></table>");
                if (m_Dt.Rows.Count > 0)
                {
                    sHtml.Append("<table width=\"900\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" bgcolor=\"#666666\">");
                    // <img src=\"/images/LineGray.gif\" width=\"16\" height=\"20\" />
                    for (int k = 0; k < m_Dt.Rows.Count; k++)
                    {
                        sHtml.Append("<tr onmouseover=\"this.className='lvtColDataHover'\" onmouseout=\"this.className='lvtColData'\">");
                        sHtml.Append("<td width=\"60\" height=\"20\" align=\"center\" bgcolor=\"#FFFFFF\" >" + (k + 1).ToString() + "</td>");
                        sHtml.Append("<td width=\"80\" bgcolor=\"#FFFFcc\">&nbsp;" + m_Dt.Rows[k][0].ToString() + "</td>");
                        sHtml.Append("<td bgcolor=\"#FFFFcc\">&nbsp;" + m_Dt.Rows[k][1].ToString() + "</td>");
                        sHtml.Append("<td width=\"80\" align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][2].ToString() + "</td>");
                        sHtml.Append("<td width=\"80\" align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][3].ToString() + "</td>");
                        sHtml.Append("<td width=\"80\" align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][4].ToString() + "</td>");
                        sHtml.Append("<td width=\"80\" align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][5].ToString() + "</td>");
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

        private string GetFilter()
        {
            string returnVa = string.Empty;// RegAreaCodeA RegAreaCodeB
            string m_UserDeptArea = string.Empty;
            string m_UserDept = join.pms.dal.CommPage.GetUnitCodeByUser(m_UserID, ref m_UserDeptArea, ref m_UserAreaName);
            string m_RoleID = CommPage.GetSingleVal("SELECT RoleID FROM [v_UserList] WHERE UserID=" + m_UserID);

            if (m_RoleID == "1")
            {
                returnVa = "AND 1=1";
            }
            else
            {
                //区县
                if (m_UserDept.Length == 2)
                {
                    returnVa = "AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 6) + "%')";
                }
                else if (m_UserDept.Length == 4)
                {
                    returnVa = "AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 9) + "%') ";
                }
                else if (m_UserDept.Length == 6)
                {

                    returnVa = "AND (RegAreaCodeA= '" + m_UserDeptArea + "' OR RegAreaCodeB = '" + m_UserDeptArea + "' ) ";
                }
                else
                {
                    returnVa = "AND 1=1";
                }
            }
            return returnVa;
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
                strErr += "请选择起始日期！\\n";
            }
            if (string.IsNullOrEmpty(endDate))
            {
                strErr += "请选择截止日期！\\n";
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
                //string columnName = "序号,用户(登录名),归属部门,登录次数";
                //string strErr = string.Empty;
                //string startDate = PageValidate.GetTrim(Request["txtStartTime"]);
                //string endDate = PageValidate.GetTrim(Request["txtEndTime"]);
                //string searchStr = string.Empty;

                //if (string.IsNullOrEmpty(startDate))
                //{
                //    strErr += "请选择起始日期！\\n";
                //}
                //if (string.IsNullOrEmpty(endDate))
                //{
                //    strErr += "请选择截止日期！\\n";
                //}
                //if (strErr != "")
                //{
                //    MessageBox.Show(this, strErr);
                //    return;
                //}

                //searchStr = " OprateDate>='" + startDate + " 00:00:00' AND OprateDate<'" + endDate + " 23:59:59'";

                //SetUIParams(startDate, endDate);
                //GetDataList(startDate, endDate);

                //m_SqlParams = "SELECT '' As XuHao,UserAccount,UserName,COUNT(*) As GroupNum FROM v_SysLogs WHERE OprateModel='登录' AND " + searchStr + " GROUP BY UserAccount,UserName ORDER BY GroupNum DESC";
                //ExportDsToXls("LoginStatsByTime", columnName, DbHelperSQL.Query(m_SqlParams));
            }
            catch (Exception ex) { }
        }

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
                        if (column.ColumnName.Equals("CustomerIDC") || column.ColumnName.Equals("CustomerMobile") || column.ColumnName.Equals("OutletsSerialNo"))
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
