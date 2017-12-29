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
    public partial class Stats0708 : System.Web.UI.Page
    {
        public string m_TargetUrl;
        private string m_FuncCode;
        private string m_FuncName;

        private string m_UserID; // 当前登录的操作用户编号
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
                this.LiteralNav.Text = "管理首页  &gt;&gt; " + m_FuncName + "：";
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
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        /// <summary>
        /// 身份验证
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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 验证接受的参数
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
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }
        }

        /// <summary>
        /// 查询
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

            GetDataList(areaCode, areaName, startDate, endDate, false);
            SetUIParams(startDate, endDate);
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butExport_Click(object sender, EventArgs e)
        {
            try
            {
                string columnName = "cols01,cols02,cols03,cols04,cols05,cols06,cols07,cols08";
                string strErr = string.Empty;
                string startDate = PageValidate.GetTrim(Request["txtStartTime"]);
                string endDate = PageValidate.GetTrim(Request["txtEndTime"]);
                string areaCode = this.DDLArea.SelectedItem.Value;
                string areaName = this.DDLArea.SelectedItem.Text;

                string searchStr = " Fileds05>='" + startDate + " 00:00:00' AND Fileds05<'" + endDate + " 23:59:59'";
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
                string siteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
                m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + siteArea + "' ORDER BY AreaCode";
                DataTable tmpDt = new DataTable();
                tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                DDLArea.DataSource = tmpDt;
                DDLArea.DataTextField = "AreaName";
                DDLArea.DataValueField = "AreaCode";
                DDLArea.DataBind();
                DDLArea.Items.Insert(0, new ListItem("宜川县人口和计划生育局", siteArea));
                tmpDt = null;
                if (!string.IsNullOrEmpty(areaCode))
                {
                    this.DDLArea.SelectedValue = areaCode;
                }
            }
            catch { }


        }

        /// <summary> 
        /// 获取数据,默认为一月
        /// </summary>
        private void GetDataList(string areaCode, string areaName, string startDate, string endDate, bool IsCreateDs)
        {
            string dbAreaCode = string.Empty, dbAreaName = string.Empty;
            if (string.IsNullOrEmpty(startDate))
            {
                startDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(endDate))
            {
                endDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            StringBuilder sHtml = new StringBuilder();

            string statInfo = areaName + "；" + startDate + "至" + endDate + "人口出生情况：";
            string searchStr = " Fileds05>='" + startDate + " 00:00:00' AND Fileds05<'" + endDate + " 23:59:59'";

            m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + areaCode + "' ORDER BY AreaCode";
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            sHtml.Append(statInfo + "<br/>");
            /*
    sHtml.Append("<table width=\"800\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\"> <tr>");
    sHtml.Append("<td rowspan=\"2\" align=\"center\">单位</td>");
    sHtml.Append("<td rowspan=\"2\" align=\"center\">出生合计</td>");
    sHtml.Append("<td colspan=\"3\" align=\"center\">一孩出生</td>");
    sHtml.Append("<td colspan=\"3\">二孩出生</td>");
    sHtml.Append("<td colspan=\"3\">多孩出生</td>");
    sHtml.Append("<td rowspan=\"2\">死亡</td>");
    sHtml.Append("</tr><tr>");
    sHtml.Append("<td>男</td>");
    sHtml.Append("<td>女</td>");
    sHtml.Append("<td>政策内</td>");
    sHtml.Append("<td>男</td>");
    sHtml.Append("<td>女</td>");
    sHtml.Append("<td>政策内</td>");
    sHtml.Append("<td>男</td>");
    sHtml.Append("<td>女</td>");
    sHtml.Append("<td>政策内</td>");
    sHtml.Append("</tr>");
**
  <tr>
    sHtml.Append("<td bgcolor=\"#FFFFCC\">单位</td>");
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
    sHtml.Append("<td>12</td>");
  </tr>
</table>
             */

            //==============================
            sHtml.Append("<table width=\"860\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
            sHtml.Append("<td width=\"50%\" height=\"22\">数据单位：" + areaName + "</td>");
            sHtml.Append("<td>统计时段：" + startDate + " 至 " + endDate + "</td>");
            sHtml.Append("</tr></table>");
            // 表头
            sHtml.Append("<table width=\"860\" border=\"0\" cellspacing=\"1\" cellpadding=\"2\" bgcolor=\"#999999\"><tr class=\"tableTitle\">");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" >单位</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\"  height=\"20\">合计</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" >男扎</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" >女扎</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" >上环</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" >取环</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" >皮埋</td>");
            sHtml.Append("<td bgcolor=\"#DDDDDD\" >药具</td>");
            sHtml.Append("</tr>");
            // 创建导出数据集
            if (IsCreateDs)
            {
                m_Ds.Tables[0].Columns.Add("cols01", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols02", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols03", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols04", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols05", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols06", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols07", typeof(string));
                m_Ds.Tables[0].Columns.Add("cols08", typeof(string));
            }// 数据
            int count = 0;
            if (m_Dt.Rows.Count > 0)
            {
                for (int k = 0; k < m_Dt.Rows.Count; k++)
                {
                    dbAreaCode = m_Dt.Rows[k][0].ToString();
                    dbAreaName = m_Dt.Rows[k][1].ToString();

                    sHtml.Append("<tr bgcolor=\"#FFFFFF\" onmouseover=\"this.style.backgroundColor='#CCFFFF'\"  onmouseout=\"this.style.backgroundColor='#FFFFFF'\">");
                    sHtml.Append("<td bgcolor=\"#FFFFCC\" height=\"22\" width=\"200\">" + dbAreaName + "</td>");


                    //依据导入时的行政区划，要求导入时必须选择精确的行政区划,否则无法统计
                    // 
                    m_SqlParams = "";
                    // 男扎 人数
                    if (dbAreaCode.Substring(9) == "000") { m_SqlParams = "SELECT COUNT(0) FROM PIS_BaseInfo WHERE " + searchStr + " AND FuncNo='020202' AND  Fileds04 ='男性绝育'  AND AreaCode LIKE '" + dbAreaCode.Substring(0, 9) + "%'"; }
                    else { m_SqlParams = "SELECT COUNT(0) FROM PIS_BaseInfo WHERE " + searchStr + " AND FuncNo='020202' AND Fileds04 ='男性绝育' AND AreaCode='" + dbAreaCode + "'"; }
                    string count1 = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                    count = int.Parse(count1);

                    // 女扎
                    if (dbAreaCode.Substring(9) == "000") { m_SqlParams = "SELECT COUNT(0) FROM PIS_BaseInfo WHERE " + searchStr + " AND FuncNo='020202' AND Fileds04 ='女性绝育' AND AreaCode LIKE '" + dbAreaCode.Substring(0, 9) + "%'"; }
                    else { m_SqlParams = "SELECT COUNT(0) FROM PIS_BaseInfo WHERE " + searchStr + " AND FuncNo='020202' AND Fileds04 ='女性绝育' AND AreaCode='" + dbAreaCode + "'"; }
                    string count2 = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                    count += int.Parse(count2);

                    // 上环
                    if (dbAreaCode.Substring(9) == "000") { m_SqlParams = "SELECT COUNT(0) FROM PIS_BaseInfo WHERE " + searchStr + " AND FuncNo='020202' AND Fileds04 ='放置宫内节育器' AND AreaCode LIKE '" + dbAreaCode.Substring(0, 9) + "%'"; }
                    else { m_SqlParams = "SELECT COUNT(0) FROM PIS_BaseInfo WHERE " + searchStr + " AND FuncNo='020202' AND Fileds04 ='放置宫内节育器' AND AreaCode='" + dbAreaCode + "'"; }
                    string count3 = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                    count += int.Parse(count3);

                    // 取环
                    if (dbAreaCode.Substring(9) == "000") { m_SqlParams = "SELECT COUNT(0) FROM PIS_BaseInfo WHERE " + searchStr + " AND FuncNo='020202' AND Fileds04 ='取出宫内节育器'  AND AreaCode LIKE '" + dbAreaCode.Substring(0, 9) + "%'"; }
                    else { m_SqlParams = "SELECT COUNT(0) FROM PIS_BaseInfo WHERE " + searchStr + " AND FuncNo='020202' AND Fileds04 ='取出宫内节育器' AND AreaCode='" + dbAreaCode + "'"; }
                    string count4 = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                    count += int.Parse(count4);

                    // 皮埋
                    if (dbAreaCode.Substring(9) == "000") { m_SqlParams = "SELECT COUNT(0) FROM PIS_BaseInfo WHERE " + searchStr + " AND FuncNo='020202' AND Fileds04 ='放置皮下埋置剂'  AND AreaCode LIKE '" + dbAreaCode.Substring(0, 9) + "%'"; }
                    else { m_SqlParams = "SELECT COUNT(0) FROM PIS_BaseInfo WHERE " + searchStr + " AND FuncNo='020202' AND Fileds04 ='放置皮下埋置剂' AND AreaCode='" + dbAreaCode + "'"; }
                    string count5 = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                    count += int.Parse(count5);

                    // 药具
                    if (dbAreaCode.Substring(9) == "000") { m_SqlParams = "SELECT COUNT(0) FROM PIS_BaseInfo WHERE " + searchStr + " AND FuncNo='020202' AND Fileds04 ='药具'  AND AreaCode LIKE '" + dbAreaCode.Substring(0, 9) + "%'"; }
                    else { m_SqlParams = "SELECT COUNT(0) FROM PIS_BaseInfo WHERE " + searchStr + " AND FuncNo='020202' AND Fileds04 ='药具' AND AreaCode='" + dbAreaCode + "'"; }
                    string count6 = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                    count += int.Parse(count6);

                    if (IsCreateDs)
                    {
                        DataRow dr = m_Ds.Tables[0].NewRow();
                        dr[0] = dbAreaName;
                        dr[1] = count.ToString();
                        dr[2] = count1;
                        dr[3] = count2;
                        dr[4] = count3;
                        dr[5] = count4;
                        dr[6] = count5;
                        dr[7] = count6;
                        m_Ds.Tables[0].Rows.Add(dr);
                    }
                    sHtml.Append("<td>" + count + "</td>");
                    sHtml.Append("<td>" + count1 + "</td>");
                    sHtml.Append("<td>" + count2 + "</td>");
                    sHtml.Append("<td>" + count3 + "</td>");
                    sHtml.Append("<td>" + count4 + "</td>");
                    sHtml.Append("<td>" + count5 + "</td>");
                    sHtml.Append("<td>" + count6 + "</td>");                  
                }
                /*
                sHtml.Append("<tr>");
                sHtml.Append("<td bgcolor=\"#FFFFCC\" height=\"20\">合计</td>");
                sHtml.Append("<td bgcolor=\"#FFFFCC\">&nbsp;</td>");
                sHtml.Append("<td bgcolor=\"#FFFFCC\">&nbsp;</td>");
                sHtml.Append("<td bgcolor=\"#FFFFCC\">&nbsp;" + sumA.ToString() + "</td>");//" + sumA.ToString("F2") + "(" + sumB.ToString("F2") + ")
                sHtml.Append("<td bgcolor=\"#FFFFCC\">&nbsp;" + sumB.ToString() + "</td>");
                sHtml.Append("<td bgcolor=\"#FFFFCC\">&nbsp;" + sumC.ToString() + "</td>");
                sHtml.Append("<td bgcolor=\"#FFFFFF\">&nbsp;</td>");
                sHtml.Append("</tr>");*/
                sHtml.Append("</table>");
            }
            else { sHtml.Append("没有匹配的数据！"); }
            m_Dt = null;
            this.LiteralResults.Text = sHtml.ToString();
        }

        /// <summary>
        /// 获取孩次的统计数据
        /// </summary>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        private string GetAreaData(string sqlParams, ref int aup)
        {
            string returnVal = "<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>";
            SqlDataReader sdr = null;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    aup = int.Parse(IntFormat(sdr[0].ToString())) + int.Parse(IntFormat(sdr[1].ToString()));
                    returnVal = "<td>" + sdr[0].ToString() + "</td><td>" + sdr[1].ToString() + "</td><td>" + sdr[2].ToString() + "</td>";
                }
                sdr.Close();
            }
            catch
            {
                if (sdr != null) sdr.Close();
            }
            return returnVal;
        }

        #region 导出数据的方法
        /// <summary>
        /// 导出数据集为Excel
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="columnName">表头列名：列1,列2,……</param>
        /// <param name="ds"></param>
        private void ExportDsToXls(string fileName, string areaCode, string areaName, string startDate, string endDate, string columnName, DataSet ds)
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
                Response.Write(ExportTable(columnName, areaCode, areaName, startDate, endDate, ds));
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
        private string ExportTable(string columnName, string areaCode, string areaName, string startDate, string endDate, DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            string dbAreaCode = string.Empty;
            string[] aryColumnName = columnName.Split(',');
            foreach (DataTable tb in ds.Tables)
            {
                sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">");
                sb.Append("<table width=\"860\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
                sb.Append("<td colspan=\"8\" align=\"center\" height=\"30\" style=\"font-size:18pt;font-weight: bold; white-space: nowrap;\">人口手术情况统计报表</td>");
                sb.Append("</tr></table>");
                // 表头单位
                sb.Append("<table width=\"860\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
                sb.Append("<td colspan=\"3\" width=\"50%\" height=\"22\">数据单位：" + areaName + "</td>");
                sb.Append("<td colspan=\"5\">统计时段：" + startDate + " 至 " + endDate + "</td>");
                sb.Append("</tr></table>");
                sb.AppendLine("<table cellspacing=\"0\" cellpadding=\"8\" rules=\"all\" border=\"1\">");
                //表头字段
                sb.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
                sb.Append("<td bgcolor=\"#DDDDDD\" >单位</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\"  height=\"20\">合计</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" >男扎</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" >女扎</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" >上环</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" >取环</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" >皮埋</td>");
                sb.Append("<td bgcolor=\"#DDDDDD\" >药具</td>");
                sb.Append("</tr>");
                //写出数据  
                foreach (DataRow row in tb.Rows)
                {
                    count++;
                    sb.Append("<tr>");
                    foreach (DataColumn column in tb.Columns)
                    {
                        if (column.ColumnName.IndexOf("cols") > -1 && !column.ColumnName.Equals("cols01"))
                        {
                            sb.Append("<td height=\"22\" style=\"vnd.ms-excel.numberformat:@\">" + row[column].ToString() + "</td>");// 强制数字格式
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
        /// 设置界面参数,日期选择
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
            else { return inStr.ToString().Trim(); }
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
