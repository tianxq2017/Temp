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

        private string m_UserID; // 当前登录的操作用户编号

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
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        #region
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
        /// 验证接受的参数
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
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
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
                case "print": // 打印
                    ShowModInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                    break;
            }
        }

        /// <summary>
        /// 修改
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
                            #region 一孩生育（怀孕）证明 农村
                            s.Append("<div class=\"dy1101\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("      <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">一孩生育（怀孕）证明</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" class=\"table_01\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("      <tr>");
                            s.Append("         <td width=\"60\">丈夫<br />姓名</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds01"].ToString() + "&nbsp;&nbsp;</td>");
                            s.Append("        <td width=\"60\">身份<br />证号</td>");
                            s.Append("        <td width=\"250\" colspan=\"3\">" + sdr["Fileds02"].ToString() + "&nbsp;&nbsp;</td>");
                            s.Append("        <td width=\"70\">户籍地</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds03"].ToString() + "&nbsp;&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td width=\"60\">妻子<br />姓名</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds04"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"60\">身份<br />证号</td>");
                            s.Append("        <td width=\"250\" colspan=\"3\">" + sdr["Fileds05"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"70\">户籍地</td>");
                            s.Append("       <td width=\"150\">" + sdr["Fileds06"].ToString() + "&nbsp;</td>");
                            s.Append("     </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td width=\"60\">初婚<br />年龄</td>");
                            s.Append("       <td width=\"150\">" + sdr["Fileds07"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"60\">现婚<br />月份</td>");
                            s.Append("       <td width=\"250\">" + sdr["Fileds08"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"60\">出生<br />年月</td>");
                            s.Append("       <td width=\"120\">" + sdr["Fileds09"].ToString() + "&nbsp;</td>");
                            s.Append("      <td width=\"70\">性别</td>");
                            s.Append("      <td width=\"150\">" + sdr["Fileds10"].ToString() + "&nbsp;</td>");
                            s.Append("    </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td colspan=\"8\" class=\"t_l font20\" style=\"line-height:28px;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;根据《内蒙古自治区人口与计划生育条例》第二十二条规定：实行生育第一胎子女登记制度。经核实：该夫妇属于政策内一胎怀孕/生育。</td>");
                            s.Append("    </tr>");
                            s.Append("	  <tr>");
                            s.Append("      <td colspan=\"8\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("  <tr>");
                            s.Append("    <td width=\"33%\" class=\"x_r t_l\" style=\"line-height:35px;\">");
                            s.Append("	<div style=\"padding:0 15px\">");
                            s.Append("	<p style=\"min-height:150px;\">" + sdr["Fileds11"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>村（居）委会意见<br />");
                            s.Append("	经办人：" + sdr["Fileds12"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds13"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("   <td width=\"33%\" class=\"x_r t_l\" style=\"line-height:35px;\">");
                            s.Append("	<div style=\"padding:0 15px\">");
                            s.Append("	<p style=\"min-height:150px;\">" + sdr["Fileds14"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>镇（区）计生办意见<br />");
                            s.Append("	经办人：" + sdr["Fileds15"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds16"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("   <td width=\"33%\" class=\"t_l\" style=\"line-height:35px;\">");
                            s.Append("	<div style=\"padding:0 15px\">");
                            s.Append("	<p style=\"min-height:150px;\">" + sdr["Fileds17"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>县卫生和计划生育局意见<br />");
                            s.Append("	经办人：" + sdr["Fileds18"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds19"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("  </tr>");
                            s.Append("</table></td>");
                            s.Append("     </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td width=\"60\">备注</td>");
                            s.Append("        <td colspan=\"7\" class=\"t_l\">" + sdr["Fileds20"].ToString() + "&nbsp;</td>");
                            s.Append("     </tr>");

                            s.Append("    </table></td>");
                            s.Append("      </tr>");
                            s.Append("</table>");
                            s.Append("</div>");
                            #endregion
                            break;
                        case "1102":
                            #region  一孩生育（怀孕）证明 城镇
                            s.Append("<div class=\"dy1102\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">一孩生育（怀孕）证明</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" class=\"table_01\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("      <tr>");
                            s.Append("        <td width=\"60\">丈夫<br />姓名</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds01"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"60\">身份<br />证号</td>");
                            s.Append("        <td width=\"250\" colspan=\"3\">" + sdr["Fileds02"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"70\">户籍地</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds03"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td width=\"60\">妻子<br />姓名</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds04"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"60\">身份<br />证号</td>");
                            s.Append("        <td width=\"250\" colspan=\"3\">" + sdr["Fileds05"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"70\">户籍地</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds06"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td width=\"60\">初婚<br />时间</td>");
                            s.Append("        <td width=\"150\">" + sdr["Fileds07"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"60\">现孕<br />月份</td>");
                            s.Append("        <td width=\"250\">" + sdr["Fileds08"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"60\">出生<br />年月</td>");
                            s.Append("        <td width=\"120\">" + sdr["Fileds09"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"70\">性别</td>");
                            s.Append("       <td width=\"150\">" + sdr["Fileds10"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td colspan=\"8\" class=\"t_l font20\" style=\"line-height:28px;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;根据《内蒙古自治区人口与计划生育条例》第二十二条规定：实行生育第一胎子女登记制度。经核实：该夫妇属于政策内一胎怀孕/生育。</td>");
                            s.Append("      </tr>");
                            s.Append("	  <tr>");
                            s.Append("        <td colspan=\"8\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("  <tr>");
                            s.Append("    <td width=\"50%\" class=\"x_r t_l\" style=\"line-height:35px;\">");
                            s.Append("	<div style=\"padding:0 50px\">");
                            s.Append("	<p style=\"min-height:150px;\">" + sdr["Fileds11"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>单&nbsp;&nbsp;位&nbsp;&nbsp;意&nbsp;&nbsp;见<br />");
                            s.Append("	经办人：" + sdr["Fileds12"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds13"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("    <td width=\"50%\" class=\"t_l\" style=\"line-height:35px;\">");
                            s.Append("	<div style=\"padding:0 50px\">");
                            s.Append("	<p style=\"min-height:150px;\">" + sdr["Fileds14"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>县卫生和计划生育局办意见<br />");
                            s.Append("	经办人：" + sdr["Fileds15"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds16"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("  </tr>");
                            s.Append("</table></td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td width=\"60\">备注</td>");
                            s.Append("        <td colspan=\"7\" class=\"t_l\">" + sdr["Fileds17"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");

                            s.Append("    </table></td>");
                            s.Append("        </tr>");
                            s.Append("</table>");
                            s.Append("</div>");
                            #endregion
                            break;
                        case "1103":
                            #region 终止妊娠手术审核表
                            s.Append("<div class=\"dy1103\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">终 止 妊 娠 手 术 审 批 表</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" class=\"table_01\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("      <tr>");
                            s.Append("        <td width=\"90\">手术者<br />姓名</td>");
                            s.Append("        <td width=\"100\">" + sdr["Fileds01"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"80\">身份证<br />号码</td>");
                            s.Append("        <td width=\"250\" colspan=\"2\">" + sdr["Fileds02"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"70\">结婚<br />时间</td>");
                            s.Append("        <td width=\"150\" class=\"t_r\">" + DateTime.Parse(sdr["Fileds03"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>配偶姓名</td>");
                            s.Append("        <td>" + sdr["Fileds04"].ToString() + "&nbsp;</td>");
                            s.Append("        <td>身份证<br />号码</td>");
                            s.Append("        <td colspan=\"2\">" + sdr["Fileds05"].ToString() + "&nbsp;</td>");
                            s.Append("        <td>胎次</td>");
                            s.Append("        <td>" + sdr["Fileds06"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>户籍地</td>");
                            s.Append("       <td colspan=\"6\" class=\"t_l\">" + sdr["Fileds07"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td>前一个<br />孩子</td>");
                            s.Append("       <td>姓名</td>");
                            s.Append("       <td>" + sdr["Fileds08"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"60\">出生<br />时间</td>");
                            s.Append("        <td width=\"190\">" + sdr["Fileds09"].ToString() + "&nbsp;</td>");
                            s.Append("        <td>性别</td>");
                            s.Append("        <td>" + sdr["Fileds10"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>终止妊娠<br />理由</td>");
                            s.Append("        <td colspan=\"6\" class=\"t_l\">" + sdr["Fileds11"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>村级意见</td>");
                            s.Append("        <td colspan=\"6\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("  <tr>");
                            s.Append("    <td width=\"50%\" class=\"x_r t_l\" style=\"line-height:35px; \">");
                            s.Append("	<div style=\"padding:0 5px\">");
                            s.Append("	<p style=\"min-height:50px;\">" + sdr["Fileds12"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>村负责人签字：" + sdr["Fileds13"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds14"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("    <td width=\"50%\" class=\"t_l\" style=\"line-height:35px; \">");
                            s.Append("	<div style=\"padding:0 5px\">");
                            s.Append("	<p style=\"min-height:50px;\">" + sdr["Fileds15"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>跟踪责任人签字：" + sdr["Fileds16"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds17"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("  </tr>");
                            s.Append("</table></td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>镇（区）<br />初审</td>");
                            s.Append("        <td colspan=\"6\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("  <tr>");
                            s.Append("    <td width=\"50%\" class=\"x_r t_l\" style=\"line-height:35px; \">");
                            s.Append("	<div style=\"padding:0 5px\">");
                            s.Append("	<p style=\"min-height:40px;\">" + sdr["Fileds18"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>主管领导签字、盖章：" + sdr["Fileds19"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds20"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("    <td width=\"50%\" class=\"t_l\" style=\"line-height:35px; \">");
                            s.Append("	<div style=\"padding:0 5px\">");
                            s.Append("	<p style=\"min-height:40px;\">" + sdr["Fileds21"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>跟踪责任人签字：" + sdr["Fileds22"].ToString() + "&nbsp;</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds23"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("  </tr>");
                            s.Append("</table></td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>医疗保健<br />机构意见</td>");
                            s.Append("        <td colspan=\"6\" class=\"t_l\">" + sdr["Fileds24"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>计生技术<br />鉴定领导<br />小组意见</td>");
                            s.Append("        <td colspan=\"6\" class=\"t_l\" style=\"padding:10px 0; \"><div style=\"padding:0 5px\">");
                            s.Append("	<p style=\"min-height:30px;\">" + sdr["Fileds25"].ToString() + "&nbsp;</p>");
                            s.Append("	<p>技术鉴定专家签名（三人以上）：" + sdr["Fileds26"].ToString() + "&nbsp;<br /><br />");
                            s.Append("	诊断单位（盖章）：</p>");
                            s.Append("	<p align=\"right\">" + DateTime.Parse(sdr["Fileds27"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</p>");
                            s.Append("	</div></td>");
                            s.Append("     </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>计生委<br />意见</td>");
                            s.Append("        <td colspan=\"6\" class=\"t_l\">" + sdr["Fileds28"].ToString() + "&nbsp;</td>");
                            s.Append("     </tr>");

                            s.Append("    </table></td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("         <td colspan=\"2\" class=\"bt_03\">说明：<br />");
                            s.Append("1、此表一式三份，计生委、受术单位、本人各一份<br />");
                            s.Append("2、终止妊娠手术者附医院和技术鉴定诊断证明</td>");
                            s.Append("        </tr>");
                            s.Append("</table>");
                            s.Append("</div>");
                            #endregion
                            break;
                        case "1104":
                            #region 终止妊娠通知单
                            s.Append("<div class=\"dy1104\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("    <tr>");
                            s.Append("      <td class=\"left\" valign=\"top\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">终止妊娠通知单</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" valign=\"top\" class=\"bt_02\">（存根）</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" class=\"table_01\" align=\"left\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append(" <tr>");
                            s.Append("    <td><span class=\"a1\">" + sdr["Fileds01"].ToString() + "&nbsp;</span>：</td>");
                            s.Append("  </tr>");
                            s.Append("  <tr>");
                            s.Append("    <td class=\"sj\">兹有<span class=\"a2\">" + sdr["Fileds02"].ToString() + "&nbsp;</span>，因<span class=\"a3\">" + sdr["Fileds03"].ToString() + "&nbsp;</span>，前来实施终止妊娠手术。</td>");
                            s.Append("  </tr>");
                            s.Append("  <tr>");
                            s.Append("    <td class=\"sj\">特此证明</td>");
                            s.Append("  </tr>");
                            s.Append("</table>");
                            s.Append("		  </td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"right\" class=\"bt_03\">人口和计划生育委员会</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"right\" class=\"bt_04\">" + DateTime.Parse(sdr["Fileds04"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</td>");
                            s.Append("        </tr>");
                            s.Append("      </table></td>");
                            s.Append("      <td valign=\"top\" class=\"right\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">终止妊娠通知单</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" valign=\"top\" class=\"bt_02\">&nbsp;</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" class=\"table_01\" align=\"left\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("  <tr>");
                            s.Append("    <td><span class=\"a1\">" + sdr["Fileds01"].ToString() + "&nbsp;</span>：</td>");
                            s.Append("  </tr>");
                            s.Append("  <tr>");
                            s.Append("    <td class=\"sj\">兹有<span class=\"a2\">" + sdr["Fileds02"].ToString() + "&nbsp;</span>，因<span class=\"a3\">" + sdr["Fileds03"].ToString() + "&nbsp;</span>，前来实施终止妊娠手术。</td>");
                            s.Append("  </tr>");
                            s.Append("  <tr>");
                            s.Append("    <td class=\"sj\">特此证明</td>");
                            s.Append("  </tr>");
                            s.Append("</table>");
                            s.Append("		  </td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"right\" class=\"bt_03\">人口和计划生育委员会</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"right\" class=\"bt_04\">" + DateTime.Parse(sdr["Fileds04"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</td>");
                            s.Append("        </tr>");
                            s.Append("     </table></td>");
                            s.Append("    </tr>");
                            s.Append("</table>");
                            s.Append("</div>");
                            #endregion
                            break;
                        case "1105":
                            #region 计划生育出生婴儿实名登记单
                            s.Append("<div class=\"dy1105\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("    <tr>");
                            s.Append("      <td class=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">" + areaName + "&nbsp;计划生育出生婴儿实名登记单介绍信</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr class=\"bt_02\">");
                            s.Append("          <td align=\"left\">" + areaName + "&nbsp;计划生育出生婴儿实名登记单</td>");
                            s.Append("         <td align=\"right\">监计育&nbsp;" + sdr["Fileds01"].ToString() + "&nbsp;&nbsp;号</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" class=\"table_01\">");
                            // 表格数据
                            s.Append("<table width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append(" <tr>");
                            s.Append("  <td width=\"38\">婴儿<br />姓名</td>");
                            s.Append("   <td width=\"100\" colspan=\"2\">" + sdr["Fileds02"].ToString() + "&nbsp;</td>");
                            s.Append("    <td width=\"55\">性别</td>");
                            s.Append("    <td width=\"75\">" + sdr["Fileds03"].ToString() + "&nbsp;</td>");
                            s.Append("   <td width=\"44\">出生<br />年月</td>");
                            s.Append("    <td width=\"60\">" + sdr["Fileds04"].ToString() + "&nbsp;</td>");
                            s.Append("    <td width=\"40\">胎次</td>");
                            s.Append("    <td width=\"35\">" + sdr["Fileds05"].ToString() + "&nbsp;</td>");
                            s.Append("  </tr>");
                            s.Append("  <tr>");
                            s.Append("    <td>父亲<br />姓名</td>");
                            s.Append("       <td colspan=\"2\">" + sdr["Fileds06"].ToString() + "&nbsp;</td>");
                            s.Append("        <td>身份<br />证号</td>");
                            s.Append("        <td>" + sdr["Fileds07"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>家庭住址或单位</td>");
                            s.Append("      <td colspan=\"3\">" + sdr["Fileds08"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("    <tr>");
                            s.Append("       <td>母亲<br />姓名</td>");
                            s.Append("       <td colspan=\"2\">" + sdr["Fileds09"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>身份<br />证号</td>");
                            s.Append("       <td>" + sdr["Fileds10"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>家庭住址或单位</td>");
                            s.Append("       <td colspan=\"3\">" + sdr["Fileds11"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td rowspan=\"2\">计划生育情况</td>");
                            s.Append("       <td>持证<br />年度</td>");
                            s.Append("       <td width=\"59\">" + sdr["Fileds12"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>缴纳社<br />会抚养<br />费情况</td>");
                            s.Append("      <td>" + sdr["Fileds13"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>落实措施情况</td>");
                            s.Append("       <td colspan=\"3\">" + sdr["Fileds14"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>出生<br />报告<br />单号<br />码</td>");
                            s.Append("       <td>" + sdr["Fileds15"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>经办人</td>");
                            s.Append("      <td>" + sdr["Fileds16"].ToString() + "&nbsp;</td>");
                            s.Append("      <td>主管领导签字</td>");
                            s.Append("      <td colspan=\"3\">" + sdr["Fileds17"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("  </table>");
                            //============================
                            s.Append("</td>");
                            s.Append("        </tr>");
                            s.Append("       <tr>");
                            s.Append("          <td colspan=\"2\" align=\"right\" class=\"bt_03\">" + areaName + "&nbsp;计生办</td>");
                            s.Append("       </tr>");
                            s.Append("       <tr>");
                            s.Append("         <td colspan=\"2\" align=\"right\" class=\"bt_04\">" + DateTime.Parse(sdr["Fileds18"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("     </table></td>");
                            s.Append("     <td class=\"right\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("       <tr>");
                            s.Append("         <td colspan=\"2\" align=\"center\" class=\"bt_01\">" + areaName + "&nbsp;计划生育出生婴儿实名登记单介绍信</td>");
                            s.Append("       </tr>");
                            s.Append("       <tr class=\"bt_02\">");
                            s.Append("        <td align=\"left\">" + areaName + "&nbsp;计划生育出生婴儿实名登记单</td>");
                            s.Append("        <td align=\"right\">监计育&nbsp;" + sdr["Fileds01"].ToString() + "&nbsp;&nbsp;号</td>");
                            s.Append("      </tr>");
                            s.Append("       <tr>");
                            s.Append("         <td colspan=\"2\" class=\"table_01\">");
                            //=============
                            s.Append("<table width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append(" <tr>");
                            s.Append("  <td width=\"38\">婴儿<br />姓名</td>");
                            s.Append("   <td width=\"100\" colspan=\"2\">" + sdr["Fileds02"].ToString() + "&nbsp;</td>");
                            s.Append("    <td width=\"55\">性别</td>");
                            s.Append("    <td width=\"75\">" + sdr["Fileds03"].ToString() + "&nbsp;</td>");
                            s.Append("   <td width=\"44\">出生<br />年月</td>");
                            s.Append("    <td width=\"60\">" + sdr["Fileds04"].ToString() + "&nbsp;</td>");
                            s.Append("    <td width=\"40\">胎次</td>");
                            s.Append("    <td width=\"35\">" + sdr["Fileds05"].ToString() + "&nbsp;</td>");
                            s.Append("  </tr>");
                            s.Append("  <tr>");
                            s.Append("    <td>父亲<br />姓名</td>");
                            s.Append("       <td colspan=\"2\">" + sdr["Fileds06"].ToString() + "&nbsp;</td>");
                            s.Append("        <td>身份<br />证号</td>");
                            s.Append("        <td>" + sdr["Fileds07"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>家庭住址或单位</td>");
                            s.Append("      <td colspan=\"3\">" + sdr["Fileds08"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("    <tr>");
                            s.Append("       <td>母亲<br />姓名</td>");
                            s.Append("       <td colspan=\"2\">" + sdr["Fileds09"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>身份<br />证号</td>");
                            s.Append("       <td>" + sdr["Fileds10"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>家庭住址或单位</td>");
                            s.Append("       <td colspan=\"3\">" + sdr["Fileds11"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td rowspan=\"2\">计划生育情况</td>");
                            s.Append("       <td>持证<br />年度</td>");
                            s.Append("       <td width=\"59\">" + sdr["Fileds12"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>缴纳社<br />会抚养<br />费情况</td>");
                            s.Append("      <td>" + sdr["Fileds13"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>落实措施情况</td>");
                            s.Append("       <td colspan=\"3\">" + sdr["Fileds14"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td>出生<br />报告<br />单号<br />码</td>");
                            s.Append("       <td>" + sdr["Fileds15"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>经办人</td>");
                            s.Append("      <td>" + sdr["Fileds16"].ToString() + "&nbsp;</td>");
                            s.Append("      <td>主管领导签字</td>");
                            s.Append("      <td colspan=\"3\">" + sdr["Fileds17"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("  </table>");
                            //=============
                            s.Append("   </td>");
                            s.Append("      </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td colspan=\"2\" align=\"right\" class=\"bt_03\">" + areaName + "&nbsp;计生办</td>");
                            s.Append("     </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td colspan=\"2\" align=\"right\" class=\"bt_04\">" + DateTime.Parse(sdr["Fileds18"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("    </table></td>");
                            s.Append("   </tr>");
                            s.Append("</table>");
                            s.Append("</div>");
                            #endregion
                            break;
                        case "1106":
                            #region 出生婴儿实名登记单(县局)
                            s.Append("<div class=\"dy1106\">");
                            s.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("    <tr>");
                            s.Append("      <td class=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">安康市高新区卫生和计划生育局<br />出生婴儿实名登记单（存根）</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr class=\"bt_02\">");
                            s.Append("          <td align=\"left\">&nbsp;</td>");
                            s.Append("          <td align=\"right\">永人第&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号</td>");
                            s.Append("        </tr>");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" class=\"table_01\"><table width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("      <tr>");
                            s.Append("        <td width=\"50\">婴儿<br />姓名</td>");
                            s.Append("        <td width=\"80\">" + sdr["Fileds02"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"50\">性别</td>");
                            s.Append("        <td width=\"80\">" + sdr["Fileds03"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"50\">出生<br />年月</td>");
                            s.Append("       <td width=\"80\">" + sdr["Fileds04"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"50\">胎次</td>");
                            s.Append("       <td width=\"80\">" + sdr["Fileds05"].ToString() + "&nbsp;</td>");
                            s.Append("     </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td>父亲<br />姓名</td>");
                            s.Append("       <td>" + sdr["Fileds06"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>身份<br />证号</td>");
                            s.Append("        <td>" + sdr["Fileds07"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>工作<br />单位</td>");
                            s.Append("       <td colspan=\"3\">" + sdr["Fileds08"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td>母亲<br />姓名</td>");
                            s.Append("       <td>" + sdr["Fileds09"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>身份<br />证号</td>");
                            s.Append("       <td>" + sdr["Fileds10"].ToString() + "&nbsp;</td>");
                            s.Append("        <td>工作<br />单位</td>");
                            s.Append("        <td colspan=\"3\">" + sdr["Fileds11"].ToString() + "&nbsp;</td>");
                            s.Append("        </tr>");
                            s.Append("      <tr>");
                            s.Append("        <td rowspan=\"2\">计划生育情况</td>");
                            s.Append("        <td>农业<br />户口</td>");
                            s.Append("       <td>非农<br />户口</td>");
                            s.Append("       <td>持证<br />情况</td>");
                            s.Append("       <td colspan=\"2\">缴纳社会抚<br />养费情况</td>");
                            s.Append("       <td colspan=\"2\">主办人<br />核实签名</td>");
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
                            s.Append("         <td colspan=\"2\" align=\"right\" class=\"bt_03\">" + DateTime.Parse(sdr["ReportDate"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("      </table></td>");
                            s.Append("      <td class=\"right\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("        <tr>");
                            s.Append("          <td colspan=\"2\" align=\"center\" class=\"bt_01\">安康市高新区卫生和计划生育局<br />出生婴儿实名登记单（存根）</td>");
                            s.Append("       </tr>");
                            s.Append("        <tr class=\"bt_02\">");
                            s.Append("          <td align=\"left\">&nbsp;</td>");
                            s.Append("         <td align=\"right\">永人第&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号</td>");
                            s.Append("        </tr>");
                            s.Append("       <tr>");
                            s.Append("         <td colspan=\"2\" class=\"table_01\"><table width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\">");
                            s.Append("     <tr>");
                            s.Append("       <td width=\"50\">婴儿<br />姓名</td>");
                            s.Append("       <td width=\"80\">" + sdr["Fileds02"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"50\">性别</td>");
                            s.Append("       <td width=\"80\">" + sdr["Fileds03"].ToString() + "&nbsp;</td>");
                            s.Append("       <td width=\"50\">出生<br />年月</td>");
                            s.Append("       <td width=\"80\">" + sdr["Fileds04"].ToString() + "&nbsp;</td>");
                            s.Append("        <td width=\"50\">胎次</td>");
                            s.Append("       <td width=\"80\">" + sdr["Fileds05"].ToString() + "&nbsp;</td>");
                            s.Append("     </tr>");
                            s.Append("     <tr>");
                            s.Append("      <td>父亲<br />姓名</td>");
                            s.Append("       <td>" + sdr["Fileds06"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>身份<br />证号</td>");
                            s.Append("      <td>" + sdr["Fileds07"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>工作<br />单位</td>");
                            s.Append("       <td colspan=\"3\">" + sdr["Fileds08"].ToString() + "&nbsp;</td>");
                            s.Append("      </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td>母亲<br />姓名</td>");
                            s.Append("      <td>" + sdr["Fileds09"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>身份<br />证号</td>");
                            s.Append("       <td>" + sdr["Fileds10"].ToString() + "&nbsp;</td>");
                            s.Append("       <td>工作<br />单位</td>");
                            s.Append("       <td colspan=\"3\">" + sdr["Fileds11"].ToString() + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("     <tr>");
                            s.Append("       <td rowspan=\"2\">计划生育情况</td>");                   
                            s.Append("       <td>农业<br />户口</td>");
                            s.Append("       <td>非农<br />户口</td>");
                            s.Append("       <td>持证<br />情况</td>");
                            s.Append("      <td colspan=\"2\">缴纳社会抚<br />养费情况</td>");
                            s.Append("       <td colspan=\"2\">主办人<br />核实签名</td>");
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
                            s.Append("         <td colspan=\"2\" align=\"right\" class=\"bt_03\">" + DateTime.Parse(sdr["ReportDate"].ToString()).ToString("yyyy年MM月dd日") + "&nbsp;</td>");
                            s.Append("       </tr>");
                            s.Append("     </table></td>");
                            s.Append("   </tr>");
                            s.Append("</table>");
                            s.Append("</div>");
                            #endregion
                            break;
                        default:
                            s.Append("获取数据信息出错");
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


