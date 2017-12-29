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
    public partial class StatsByBanJie : System.Web.UI.Page
    {
        private string m_SqlParams;
        private DataTable m_Dt;
        private string m_UserID; // 当前登录的操作用户编号

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();

            if (!IsPostBack)
            {
                this.LiteralNav.Text = "统计分析 > 用户办结、超期统计 ";
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
            string maxVal = "0";
            string curVal = "0";
            float imgWidth = 0;
            StringBuilder sHtml = new StringBuilder();
            try {
                // SELECT MAX(UserLoginNum) FROM v_UserList
                m_SqlParams = "select [AreaCode],[AreaName],(select COUNT(*) from v_BJCount where A.AreaCode=AreaCode AND OprateDate is not null),(select COUNT(*) from v_BJCount where A.AreaCode=AreaCode AND OprateDate is not null AND dateadd(day,10,CreateDate)<OprateDate) from AreaDetailCN A where A.AreaCode like '1505%' group by [AreaCode],[AreaName] order by AreaCode";
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                sHtml.Append("截至当前时间，各旗镇村办结、超期数统计如下：<br/>");
                sHtml.Append("<table width=\"800\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" bgcolor=\"#666666\"><tr>");
                sHtml.Append("<td width=\"50\" height=\"30\" align=\"center\" bgcolor=\"#cccccc\" ><strong>序号</strong></td>");
                sHtml.Append("<td width=\"200\" class=\"fb01\" bgcolor=\"#cccccc\" >&nbsp;<strong>各区划</strong></td>");
                sHtml.Append("<td width=\"250\" class=\"fb01\" bgcolor=\"#cccccc\" >&nbsp<strong>各部门</strong></td>");
                sHtml.Append("<td class=\"150\" class=\"fb01\" bgcolor=\"#cccccc\" align=\"center\" >&nbsp;<strong>办结数</strong></td>");
                sHtml.Append("<td width=\"150\" class=\"fb01\" bgcolor=\"#cccccc\" align=\"center\" >&nbsp;<strong>超期数</strong></td>");
                sHtml.Append("</tr></table>");
                if (m_Dt.Rows.Count > 0)
                {
                    sHtml.Append("<table width=\"800\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" bgcolor=\"#666666\">");
                    // <img src=\"/images/LineGray.gif\" width=\"16\" height=\"20\" />
                    for (int k = 0; k < m_Dt.Rows.Count; k++)
                    {
                        sHtml.Append("<tr onmouseover=\"this.className='lvtColDataHover'\" onmouseout=\"this.className='lvtColData'\">");
                        sHtml.Append("<td width=\"50\" height=\"20\" align=\"center\" bgcolor=\"#FFFFFF\" >" + (k + 1).ToString() + "</td>");
                        sHtml.Append("<td width=\"200\" align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][0].ToString() + "</td>");
                        sHtml.Append("<td width=\"250\" align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][1].ToString() + "</td>");
                        sHtml.Append("<td width=\"150\" align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][2].ToString() + "</td>");
                        sHtml.Append("<td width=\"150\" align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][3].ToString() + "</td>");
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

        private string GetDaysInMonth(DateTime inDate)
        {
            return DateTime.DaysInMonth(inDate.Year, inDate.Month).ToString().PadLeft(2, '0');
        }

        protected void butExport_Click(object sender, EventArgs e)
        {
            try
            {
                string columnName = "序号,各区划,各部门,办结数,超期数";


                GetDataList("", "");
                // m_SqlParams = "SELECT UserName+'('+UserAccount+')',DeptName,UserLoginNum FROM v_UserList WHERE ValidFlag=1 ORDER BY UserLoginNum DESC";
                m_SqlParams = "select [AreaCode],[AreaName],(select COUNT(*) from v_BJCount where A.AreaCode=AreaCode AND OprateDate is not null) numbj,(select COUNT(*) from v_BJCount where A.AreaCode=AreaCode AND OprateDate is not null AND dateadd(day,10,CreateDate)<OprateDate) numyc from AreaDetailCN A where A.AreaCode like '1505%' group by [AreaCode],[AreaName] order by AreaCode";
                ExportDsToXls("BanJieStats", columnName, DbHelperSQL.Query(m_SqlParams));
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
                        if (column.ColumnName.Equals("AreaCode") || column.ColumnName.Equals("numbj") || column.ColumnName.Equals("numyc"))
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