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

        private string m_UserID; // 当前登录的操作用户编号

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
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        #region 身份验证，参数接收校验
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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 验证接受的参数
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
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }
        }

        /// <summary>
        /// 设置操作行为
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("非法访问：操作被终止！");
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
                case "exp": // 编辑
                    funcName = "导出选定的数据为Excel文件";
                    SetExcelByFuncNo(m_FuncCode, m_ObjID);
                    break;
                default:
                    Response.Write(" <script>alert('操作失败：参数错误！') ;window.location.href='" + m_TargetUrl + "'</script>");
                    break;
            }
            this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">管理首页</a> &gt;&gt; 数据采集 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }

        #endregion

        /// <summary>
        /// 设置并生成excel文件
        /// </summary>
        /// <param name="funcNo"></param>
        /// <param name="rptID"></param>
        private void SetExcelByFuncNo(string funcNo, string rptID)
        {
            string[] aryTitles = null;
            string clos = GetRptColumns(m_FuncCode); //列头数据
            StringBuilder sb = new StringBuilder();//报表数据
            StringBuilder sHeader = new StringBuilder();//表头
            StringBuilder sFooter = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                aryTitles = m_Fields.Split(',');
                //报表信息：表头及页脚
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
                    sHeader.Append("<tr><td width=\"50%\" height=\"22\" colspan=\"" + (aryTitles.Length / 2).ToString() + "\">单位名称：" + PageValidate.GetTrim(sdr["UnitName"].ToString()) + "</td>");
                    if (m_FuncCode == "0211")
                    {
                        sHeader.Append("<td>" + m_RptTime + "</td>");
                        sHeader.Append("<td align=\"right\">计量单位：千元</td>");
                    }
                    else { sHeader.Append("<td colspan=\"" + (aryTitles.Length / 2).ToString() + "\">" + m_RptTime + "</td>"); }
                    sHeader.Append("</tr></table>");

                    sFooter.Append("<table width=\"950\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
                    sFooter.Append("<td width=\"25%\" height=\"22\">单位负责人：" + PageValidate.GetTrim(sdr["UnitHeader"].ToString()) + "</td>");
                    sFooter.Append("<td width=\"25%\">统计负责人：" + sdr["StatsHeader"].ToString() + "</td>");
                    sFooter.Append("<td width=\"25%\">填表人：" + sdr["RptUserName"].ToString() + "</td>");
                    sFooter.Append("<td width=\"25%\">填报时间：" + DateTime.Parse(sdr["OprateDate"].ToString()).ToString("yyyy-MM-dd") + "</td>");
                    sFooter.Append("</tr></table>");
                }
                sdr.Close();

                //报表数据
                sb.Append(sHeader.ToString()); //报表标题
                sb.Append(clos); //列头数据

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
                sb.Append(sFooter.ToString());//报表页脚
            }
            catch { if (sdr != null) sdr.Close(); }

            SetXlsFiles(sb.ToString()); // 保存为xls文件

            sHeader = null;
            sFooter = null;
            sb = null;

            /*
            //表头
            sb.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\" >");
            sb.AppendLine("<tr style=\"font-weight: bold;font-size:22px;white-space: nowrap;\">");
            sb.AppendLine("<td colspan=\"8\" align=\"center\" height=\"35\">" + XlsName + "</td>");
            sb.AppendLine("</tr>");
            // 空行
            sb.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
            sb.AppendLine("<td colspan=\"4\" align=\"left\">" + XlsUnit + "</td><td colspan=\"4\" align=\"left\">" + XlsUnit + "</td>");
            sb.AppendLine("</tr>");
            //colspan="5"
            //列头 
            sb.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
            for (int i = 0; i < aryFields.Length; i++)
            {
                sb.AppendLine("<td>" + aryTitles[i] + "</td>");
            }
            sb.AppendLine("</tr>");
            // 数据
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                sb.Append("<tr>");
                sb.Append("<td style=\"vnd.ms-excel.numberformat:@\">" + dt.Rows[j][aryFields[k]].ToString() + "</td>");// 强制数字格式
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</table>");
            */
        }

        /// <summary>
        ///  获取报表列头(表头)
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetRptColumns(string funcNo)
        {
            StringBuilder clos = new StringBuilder();
            /*
02	数据采集 
------------------------------------
0201	工业总产值及主要产品产量月报
0202	主要生产技术经济指标表
0203	主要工业产品销售量、库存、订货
0204	分行业工业总产值
0205	有色金属企业主要经营指标
0206	原材料、燃料、动力购进价格调查月报表
0207	工业品出厂价格月报
0208	钼产品主要客户销售情况
0209	出口配额获授情况
0210	主要产品生产、销售、库存完成情况
0211	财务状况
0212	人数统计报表
0213	金钼股份矿业分公司生产快报
*/
            switch (funcNo)
            {
                case "0201":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08";
                    #region 表头
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    clos.Append("<tr>");
                    clos.Append("<td rowspan=\"2\" align=\"center\" >&nbsp;产值或产品(指标)名称</td>");
                    clos.Append("<td rowspan=\"2\" align=\"center\" >&nbsp;计算单位</td>");
                    clos.Append("<td height=\"22\" colspan=\"2\" align=\"center\" >计划</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\" >本年实际</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\" >去年实际</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\" >本年</td>");
                    clos.Append("<td width=\"100\" align=\"center\" >本月</td>");
                    clos.Append("<td width=\"100\" align=\"center\" >本月</td>");
                    clos.Append("<td width=\"100\" align=\"center\" >本月止累计</td>");
                    clos.Append("<td width=\"100\" align=\"center\" >同月</td>");
                    clos.Append("<td width=\"100\" align=\"center\" >同月止累计</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    clos.Append("<tr>");
                    clos.Append("<td height=\"23\" align=\"center\" >甲</td>");
                    clos.Append("<td align=\"center\" >乙</td>");
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
                    #region 表头
                    clos.Append("<table width=\"2000\" border=\"1\" cellpadding=\"2\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"200\" rowspan=\"2\" align=\"center\">指标名称</td>");
                    clos.Append("<td colspan=\"3\" align=\"center\">计算单位</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">本年计划</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">本月指标</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">本月止累计指标</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">去年同期累计指标</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">比去年(±%)</td>");
                    clos.Append("<td colspan=\"4\" align=\"center\">子项</td>");
                    clos.Append("<td colspan=\"4\" align=\"center\">母项</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">历史最好水平</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\">计算单位</td>");
                    clos.Append("<td width=\"100\" align=\"center\">子项</td>");
                    clos.Append("<td width=\"100\" align=\"center\">母项</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本月</td>");
                    clos.Append("<td width=\"100\" align=\"center\">上月止累计</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本月止累计</td>");
                    clos.Append("<td width=\"100\" align=\"center\">去年同期累计</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本月</td>");
                    clos.Append("<td width=\"100\" align=\"center\">上月止累计</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本月止累计</td>");
                    clos.Append("<td width=\"100\" align=\"center\">去年同期累计</td>");
                    clos.Append("<td width=\"100\" align=\"center\">指标</td>");
                    clos.Append("<td width=\"100\" align=\"center\">年份</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0203":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17";
                    #region 表头
                    clos.Append("<table width=\"1900\" border=\"1\" cellpadding=\"2\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"200\" rowspan=\"2\" align=\"center\">产品名称</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">计量单位</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">代码</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">年初库存量</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">累计生产量</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">累计销售量</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">企业自用及其他</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">期末库存量</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">1-本季累计订货量</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">1-本季累计订货额(千元)</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\">本期</td>");
                    clos.Append("<td width=\"100\" align=\"center\">去年同期</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本期</td>");
                    clos.Append("<td width=\"100\" align=\"center\">去年同期</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本期</td>");
                    clos.Append("<td width=\"100\" align=\"center\">去年同期</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本期</td>");
                    clos.Append("<td width=\"100\" align=\"center\">去年同期</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本期</td>");
                    clos.Append("<td width=\"100\" align=\"center\">去年同期</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本期</td>");
                    clos.Append("<td width=\"100\" align=\"center\">去年同期</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本期</td>");
                    clos.Append("<td width=\"100\" align=\"center\">去年同期</td>");
                    clos.Append("</tr> ");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    clos.Append("<tr>");
                    clos.Append("<td height=\"23\" align=\"center\">甲</td>");
                    clos.Append("<td align=\"center\">乙</td>");
                    clos.Append("<td align=\"center\">丙</td>");
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
                    #region 表头
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"2\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"200\" rowspan=\"2\" align=\"center\">指标名称</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">计算单位</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">代码</td>");
                    clos.Append("<td width=\"100\" colspan=\"2\" align=\"center\">本年实际</td>");
                    clos.Append("<td width=\"100\" colspan=\"2\" align=\"center\">去年实际</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\">本月</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本月止累计</td>");
                    clos.Append("<td width=\"100\" align=\"center\">去年同月</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本月止累计</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0205":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13";
                    #region 表头
                    clos.Append("<table width=\"1400\" border=\"1\" cellpadding=\"0\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"200\" rowspan=\"2\" align=\"center\">指标名称</td>");
                    clos.Append("<td colspan=\"4\" align=\"center\">产量（吨）</td>");
                    clos.Append("<td colspan=\"4\" align=\"center\">销量（吨）</td>");
                    clos.Append("<td colspan=\"4\" align=\"center\">含税价格（吨/元）</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\">当月产量</td>");
                    clos.Append("<td width=\"100\" align=\"center\">环比</td>");
                    clos.Append("<td width=\"100\" align=\"center\">累计产量</td>");
                    clos.Append("<td width=\"100\" align=\"center\">同比</td>");
                    clos.Append("<td width=\"100\" align=\"center\">当月销售</td>");
                    clos.Append("<td width=\"100\" align=\"center\">环比</td>");
                    clos.Append("<td width=\"100\" align=\"center\">累计销售</td>");
                    clos.Append("<td width=\"100\" align=\"center\">同比</td>");
                    clos.Append("<td width=\"100\" align=\"center\">当月销售价格</td>");
                    clos.Append("<td width=\"100\" align=\"center\">环比</td>");
                    clos.Append("<td width=\"100\" align=\"center\">累计销售价格</td>");
                    clos.Append("<td width=\"100\" align=\"center\">同比</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0206":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07";
                    #region 表头
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"0\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"200\" rowspan=\"2\" align=\"center\">产品名称</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">计算单位</td>");
                    clos.Append("<td width=\"100\" colspan=\"3\" align=\"center\">报告期单价</td>");
                    clos.Append("<td width=\"100\" colspan=\"2\" align=\"center\">基期单价</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\">5日单价</td>");
                    clos.Append("<td width=\"100\" align=\"center\">20日单价</td>");
                    clos.Append("<td width=\"100\" align=\"center\">平均单价</td>");
                    clos.Append("<td width=\"100\" align=\"center\">上年同月平均价</td>");
                    clos.Append("<td width=\"100\" align=\"center\">上月单价</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0207":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07";
                    #region 表头
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"0\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"200\" rowspan=\"2\" align=\"center\">产品名称</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">计算单位</td>");
                    clos.Append("<td width=\"100\" colspan=\"3\" align=\"center\">报告期单价</td>");
                    clos.Append("<td width=\"100\" colspan=\"2\" align=\"center\">基期单价</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\">5日单价</td>");
                    clos.Append("<td width=\"100\" align=\"center\">20日单价</td>");
                    clos.Append("<td width=\"100\" align=\"center\">平均单价</td>");
                    clos.Append("<td width=\"100\" align=\"center\">上年同月平均价</td>");
                    clos.Append("<td width=\"100\" align=\"center\">上月单价</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0208":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05";
                    #region 表头
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"0\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"200\" align=\"center\">客户名称</td>");
                    clos.Append("<td width=\"100\" align=\"center\">产品</td>");
                    clos.Append("<td width=\"100\" align=\"center\">上年销量(吨)</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本月止销量(吨)</td>");
                    clos.Append("<td width=\"100\" align=\"center\">金额(万元)</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0209":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06";
                    #region 表头
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"0\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" align=\"center\">分类</td>");
                    clos.Append("<td width=\"100\" align=\"center\">品种</td>");
                    clos.Append("<td width=\"100\" align=\"center\">上年获授配额(吨)</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本年获授配额(吨)</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本月止使用配额(吨)</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本月止使用配额占比(%)</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0210":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15";
                    #region 表头
                    clos.Append("<table width=\"2000\" border=\"1\" cellpadding=\"2\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">序号</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">产品名称</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">年初库存量</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">累计生产量</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">累计销售量</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">累计销售收入(元)(无税)</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\" align=\"center\">本月销售价格(元/吨标量)</td>");
                    clos.Append("<td width=\"100\" rowspan=\"2\"align=\"center\">累计销售价格(元/吨标量)</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">累计企业自用及其他</td>");
                    clos.Append("<td colspan=\"2\" align=\"center\">期末库存量</td>");
                    clos.Append("</tr>");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" height=\"22\" align=\"center\">标量</td>");
                    clos.Append("<td width=\"100\" align=\"center\">实物量</td>");
                    clos.Append("<td width=\"100\" align=\"center\">标量</td>");
                    clos.Append("<td width=\"100\" align=\"center\">实物量</td>");
                    clos.Append("<td width=\"100\" align=\"center\">标量</td>");
                    clos.Append("<td width=\"100\" align=\"center\">实物量</td>");
                    clos.Append("<td width=\"100\" align=\"center\">标量</td>");
                    clos.Append("<td width=\"100\" align=\"center\">实物量</td>");
                    clos.Append("<td width=\"100\" align=\"center\">标量</td>");
                    clos.Append("<td width=\"100\" align=\"center\">实物量</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0211":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04";
                    #region 表头
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"0\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" align=\"center\">指标名称</td>");
                    clos.Append("<td width=\"100\" align=\"center\">代码</td>");
                    clos.Append("<td width=\"100\" align=\"center\">1-本月</td>");
                    clos.Append("<td width=\"100\" align=\"center\">上年同期</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0212":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06";
                    #region 表头
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"0\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" align=\"center\">单位名称</td>");
                    clos.Append("<td width=\"100\" align=\"center\">人数</td>");
                    clos.Append("<td width=\"100\" align=\"center\">单位名称</td>");
                    clos.Append("<td width=\"100\" align=\"center\">人数</td>");
                    clos.Append("<td width=\"100\" align=\"center\">单位名称</td>");
                    clos.Append("<td width=\"100\" align=\"center\">人数</td>");
                    clos.Append("</tr>");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                case "0213":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15";
                    #region 表头
                    clos.Append("<table width=\"2000\" border=\"1\" cellpadding=\"2\" cellspacing=\"1\">");
                    clos.Append("<tr>");
                    clos.Append("<td width=\"100\" align=\"center\">序号</td>");
                    clos.Append("<td width=\"200\" align=\"center\">指标名称</td>");
                    clos.Append("<td width=\"100\" align=\"center\">计量单位</td>");
                    clos.Append("<td width=\"100\" align=\"center\">月计划</td>");
                    clos.Append("<td width=\"100\" align=\"center\">月实际</td>");
                    clos.Append("<td width=\"100\" align=\"center\">去年同月</td>");
                    clos.Append("<td width=\"100\" align=\"center\">与计划比较增减</td>");
                    clos.Append("<td width=\"100\" align=\"center\">同比增减</td>");
                    clos.Append("<td width=\"100\" align=\"center\">年计划</td>");
                    clos.Append("<td width=\"100\" align=\"center\">本月止累计</td>");
                    clos.Append("<td width=\"100\" align=\"center\">完成年计划的%或与年计划比较±%</td>");
                    clos.Append("<td width=\"100\" align=\"center\">去年同期止累计</td>");
                    clos.Append("<td width=\"100\" align=\"center\">与去年同期止累计比较增减</td>");
                    clos.Append("</tr> ");
                    clos.Append("</table>");
                    clos.Append("<table width=\"950\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" >");
                    #endregion
                    break;
                default:
                    Response.Write(" <script>alert('操作失败：参数错误！') ;window.location.href='" + m_TargetUrl + "'</script>");
                    break;
            }
            return clos.ToString();
        }

        /// <summary>
        /// 将xls格式数据保存为文件
        /// </summary>
        /// <param name="xlsBody"></param>
        private void SetXlsFiles(string xlsBody)
        {
            string msgTxt = string.Empty;
            string serverPath = Server.MapPath("/");
            string configPath = System.Configuration.ConfigurationManager.AppSettings["FCKeditor:UserFilesPath"];//文件存放路径
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
                msgTxt += " 成功生成Excel文件：<a href=\"" + filePath + "\" target='_blank' > " + m_RptTime + m_NavTitle + "数据，请点击此处下载到本地</a><br/>";
                SetFileToHD(m_RptTime + m_NavTitle + "数据", filePath);
                msgTxt += "文档已经同步发布到[ 网络硬盘 >> 我的网盘 >> ]根目录下……";
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

