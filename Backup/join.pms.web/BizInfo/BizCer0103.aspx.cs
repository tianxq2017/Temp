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


namespace join.pms.web.BizInfo
{
    public partial class BizCer0103 : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;

        private string m_ObjID;
        private string m_UserID; // 当前登录的操作用户编号
        private iSvrs.DALSvrs m_DALSvrs;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                ShowBizInfo(m_ObjID);
            }
        }

        #region 页面验证信息
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
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                //m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                //m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
                //m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }
            //if (m_ActionName == "view") m_ObjID = Request.QueryString["RID"];
        }
        #endregion

        /// <summary>
        /// 查看详细信息
        /// </summary>
        /// <param name="objID"></param>
        private void ShowBizInfo(string bizID)
        {
            string workFlowsID = "", CertificateNum = "", Approval  = string.Empty;
            string Signature = "", QRCodeFiles = "", Memos = string.Empty;
            StringBuilder s = new StringBuilder();
            try
            {
                join.pms.dal.BIZ_Contents biz = new join.pms.dal.BIZ_Contents();
                biz.SearchWhere = "BizID=" + bizID;
                biz.SelectAll(false);
                if (!string.IsNullOrEmpty(biz.BizID))
                {
                    join.pms.dal.BizWorkFlows bwf = new join.pms.dal.BizWorkFlows();
                    bwf.BizID = bizID;
                    bwf.FilterSQL = "BizID=" + bizID + " ORDER BY BizStep";
                    bwf.GetWorkFlowsByBizID();
                    CertificateNum = bwf.CertificateNoA;
                    Approval = bwf.Approval;
                    Signature = bwf.Signature;
                    workFlowsID = bwf.WorkFlowsID;
                    QRCodeFiles = bwf.QRCodeFiles;
                    Memos = bwf.Comments;
                    //生成二维码
                    if (!string.IsNullOrEmpty(workFlowsID) && string.IsNullOrEmpty(QRCodeFiles))
                    {
                        string optateDate = bwf.OprateDate;
                        if (!string.IsNullOrEmpty(optateDate)) { optateDate = DateTime.Parse(optateDate).ToString("yyyy年MM月dd日"); }
                        else { optateDate = DateTime.Now.ToString("yyyy年MM月dd日"); }
                        string qrInfo = "持证人姓名：" + biz.Fileds01 + "；身份证号：" + biz.PersonCidA + "；审核日期：" + optateDate;
                        QRCode qr = new QRCode();
                        if (qr.SetQrCode(qrInfo, Server.MapPath("/"), ref QRCodeFiles))
                        {
                            bwf.SettWorkFlowsQrCode(workFlowsID, QRCodeFiles);
                        }
                        qr = null;
                    }
                    bwf = null;
                    //登记表
                    s.Append("<div class=\"paper_print_0103a\">");
                    s.Append("<div class=\"title\">农村部分计划生育家庭奖励扶助光荣证</div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<div class=\"tr_02\">");
                    s.Append("<div class=\"photo\"><img src=\"" + biz.PersonPhotos + "\" /></div>");//夫妻照
                    s.Append("<div class=\"code\"><img src=\"" + QRCodeFiles + "\" /><p>手机扫描二维码</p></div>");//二维码
                    s.Append("<div class=\"official\"><img src=\"" + Signature + "\"  height=\"165\" /></div>");//公章
                    s.Append("</div>");

                    s.Append("<div class=\"tr_01 clearfix\" style=\"padding-top:28px;\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds01 + "</div>");//姓名
                    s.Append("<div class=\"td_02\">" + biz.Fileds02 + "</div>");//性别
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds03 + "</div>");//民族
                    s.Append("<div class=\"td_02\">" + BIZ_Common.FormatString(biz.Fileds32, "2") + "</div>");//生日
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01\">");
                    s.Append("<div class=\"td_03\">" + biz.PersonCidA + "</div>");//cid
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01\">");
                    s.Append("<div class=\"td_03\">" + biz.RegAreaNameA + "</div>");//户籍地
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<p><img src=\"/BizInfo/images/info/paper_0103a.gif\" /></p>");
                    s.Append("<div class=\"bottom\">");
                    s.Append("<span class=\"a3\">证件编号：" + CertificateNum + "</span>");
                    s.Append("<span class=\"a1\">发证日期：" + GetBuDaInfo(bizID, biz.Attribs) + "</span>");
                    s.Append("<span class=\"a2\">发证单位（盖章）</span>");
                    s.Append("</div>");
                    s.Append("</div>");

                    s.Append("<div class=\"paper_print_0103b\">");
                    s.Append("<div class=\"title\">年审记录</div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<p><img src=\"/BizInfo/images/info/paper_0103b.gif\" /></p>");
                    s.Append("</div>");

                    s.Append("<div class=\"paper_print_0103b paper_print_0103c\">");
                    s.Append("<div class=\"title\">年审记录</div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">年审登记（专用章）：</p>");
                    s.Append("<p class=\"td_02\"><span>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</span>经办人：</p>");
                    s.Append("</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<p><img src=\"/BizInfo/images/info/paper_0103b.gif\" /></p>");
                    s.Append("</div>");

                }
                else
                {
                    s.Append("<div class=\"paper_print_0103a\"><p><img src=\"/BizInfo/images/info/paper_0103a.jpg\" /></p></div>");
                }
                biz = null;
            }
            catch { }

            this.LiteralBizInfo.Text = s.ToString();
        }
        /// <summary>
        /// 补打证件
        /// </summary>
        /// <param name="bizID"></param>
        /// <returns></returns>
        public string GetBuDaInfo(string bizID, string Attribs)
        {
            string returnVal = string.Empty;
            string printC = CommPage.GetSingleVal("SELECT COUNT(0) FROM BIZ_Certificates WHERE CertificateType=2 AND BizID=" + bizID);
            string createDate = CommPage.GetSingleVal("SELECT CreateDate FROM BIZ_Certificates WHERE CertificateType=2 AND BizID=" + bizID);
            if (int.Parse(printC) > 0 && Attribs == "8") { returnVal = BIZ_Common.FormatString(createDate, "2") + "【补" + DateTime.Now.ToString("yyyy-MM-dd") + "】"; }
            else { returnVal = DateTime.Now.ToString("yyyy-MM-dd"); }
            return returnVal;
        }
    }
}
