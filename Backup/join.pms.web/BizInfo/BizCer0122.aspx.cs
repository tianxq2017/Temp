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
    public partial class BizCer0122 : System.Web.UI.Page
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
            string workFlowsID = "", CertificateNum = "", Approval = "", ApprovalSignName = string.Empty;
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
                    ApprovalSignName = bwf.ApprovalSignName;
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
                        string qrInfo = "再生育登记；夫妻姓名及身份证号：" + biz.Fileds01 + "(" + biz.PersonCidA + ")、" + biz.Fileds08 + "(" + biz.PersonCidB + ")；登记日期：" + optateDate;
                        QRCode qr = new QRCode();
                        if (qr.SetQrCode(qrInfo, Server.MapPath("/"), ref QRCodeFiles))
                        {
                            bwf.SettWorkFlowsQrCode(workFlowsID, QRCodeFiles);
                        }
                        qr = null;
                    }
                    
                    //登记表
                    s.Append("<div class=\"paper_print_0122a\">");
                    s.Append("<div class=\"title\">发证记录</div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg clearfix\">");
                    s.Append("<div class=\"tr_01\">");
                    s.Append("<p>服务证编号：<b>" + CertificateNum + "</b></p>");
                    s.Append("<p>" + Memos + "</p>");
                    s.Append("<p>经办人：" + ApprovalSignName + "</p>");
                    s.Append("<p>登记单位（盖章）：</p>");
                    s.Append("<p>发证日期：" + GetBuDaInfo(bizID, biz.Attribs) + "</p>");
                    s.Append("<p>公民身份证号码（夫）：" + biz.PersonCidA + "</p>");
                    s.Append("<p>公民身份证号码（妻）：" + biz.PersonCidB + "</p>");
                    s.Append("<p>准生期限：" + bwf.CertificateDateStart + "-" + bwf.CertificateDateEnd + "</p>");
                    s.Append("</div>");
                    bwf = null;
                    s.Append("<div class=\"tr_02\">");
                    s.Append(" <div class=\"photo\"><img src=\"" + biz.PersonPhotos + "\" /></div>");//夫妻照
                    s.Append("<div class=\"code\"><img src=\"" + QRCodeFiles + "\" /><p>手机扫描二维码</p></div>");//二维码
                    s.Append("<div class=\"official\"><img src=\"" + Signature + "\" height=\"165\" /></div>"); //公章
                    s.Append("</div>");

                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<p><img src=\"/BizInfo/images/info/paper_0122a.gif\" /></p>");
                    s.Append("</div>");
                    //基本情况
                    /*
                     <div class="paper_print_0102b">
  <div class="title">基本情况</div>
  <div class="print_table">
  <div class="print_table_bg">
	<div class="tr_01 clearfix" style="padding-top:49px;">
	  <div class="td_01">欧阳麦麦提</div>
	  <div class="td_02">2014-10-10</div>
	  <div class="td_03">乌兹别克族</div>
	  <div class="td_04">农业户口</div>
	  <div class="td_05">初婚</div>
	  <div class="td_06">2014-10-10</div>
	  <div class="td_07">是</div>
	  <div class="td_08">相关信息啊相关信息啊相关信息啊相关信息啊相关信息啊相关信息啊</div>
	</div>
	<div class="tr_01 clearfix" style="height: auto;">
	  <div class="td_01">欧阳麦麦提</div>
	  <div class="td_02">2014-10-10</div>
	  <div class="td_03">乌兹别克族</div>
	  <div class="td_04">农业户口</div>
	  <div class="td_05">初婚</div>
	  <div class="td_06">2014-10-10</div>
	  <div class="td_07">是</div>
	  <div class="td_08">相关信息啊相关信息啊相关信息啊相关信息啊相关信息啊相关信息啊</div>
	</div>
	
  </div>
  </div>
  <p><img src="images/info/paper_0102b.gif" /></p>
                     */
                    s.Append("<div class=\"paper_print_0122b\">");
                    s.Append("<div class=\"title\">基本情况</div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<div class=\"tr_01 clearfix\" style=\"padding-top:49px;\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds01 + "</div>");//男
                    s.Append("<div class=\"td_02\">" + BIZ_Common.FormatString(biz.Fileds32, "2") + "</div>");//生日
                    s.Append("<div class=\"td_03\">" + biz.Fileds03 + "</div>");//民族
                    s.Append("<div class=\"td_04\">" + biz.Fileds04 + "</div>");//户口
                    s.Append("<div class=\"td_05\">" + biz.Fileds33 + "</div>");//婚姻状况
                    s.Append("<div class=\"td_06\">" + BIZ_Common.FormatString(biz.Fileds34, "2") + "</div>");//结婚日期
                    s.Append("<div class=\"td_07\">" + biz.Fileds17 + "</div>");//是否独子
                    s.Append("<div class=\"td_08\">" + biz.RegAreaNameA + "</div>");//户籍
                    s.Append("</div>");

                    s.Append("<div class=\"tr_01 clearfix\" style=\"height: auto;\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds08 + "</div>");//女
                    s.Append("<div class=\"td_02\">" + BIZ_Common.FormatString(biz.Fileds31, "2") + "</div>");//生日
                    s.Append("<div class=\"td_03\">" + biz.Fileds10 + "</div>");//民族
                    s.Append("<div class=\"td_04\">" + biz.Fileds11 + "</div>");//户口
                    s.Append("<div class=\"td_05\">" + biz.Fileds13 + "</div>");//婚姻状况
                    s.Append("<div class=\"td_06\">" + BIZ_Common.FormatString(biz.Fileds14, "2") + "</div>");//结婚日期
                    s.Append("<div class=\"td_07\">" + biz.Fileds16 + "</div>");//是否独子
                    s.Append("<div class=\"td_08\">" + biz.RegAreaNameB + "</div>");//户籍
                    s.Append("</div>");

                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<p><img src=\"/BizInfo/images/info/paper_0122b.gif\" /></p>");
                    
                    //生育情况
                    s.Append("<div class=\"title\" style=\"padding:6px 0 5px;\">生育情况</div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    BIZ_PersonChildren bizChild = new BIZ_PersonChildren();
                    bizChild.Select("", bizID);
                    s.Append("<div class=\"tr_02 clearfix\" style=\"padding-top:30px;\">");
                    s.Append("<div class=\"td_01\">" + bizChild.ChildName1 + "</div>");
                    s.Append("<div class=\"td_02\">" + bizChild.ChildBirthday1 + "</div>");
                    s.Append("<div class=\"td_03\">" + bizChild.ChildSex1 + "</div>");
                    s.Append("<div class=\"td_04\">" + bizChild.ChildPolicy1 + "</div>");
                    s.Append("<div class=\"td_05\">" + bizChild.Memos1 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + bizChild.ChildName2 + "</div>");
                    s.Append("<div class=\"td_02\">" + bizChild.ChildBirthday2 + "</div>");
                    s.Append("<div class=\"td_03\">" + bizChild.ChildSex2 + "</div>");
                    s.Append("<div class=\"td_04\">" + bizChild.ChildPolicy2 + "</div>");
                    s.Append("<div class=\"td_05\">" + bizChild.Memos2 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + bizChild.ChildName3 + "</div>");
                    s.Append("<div class=\"td_02\">" + bizChild.ChildBirthday3 + "</div>");
                    s.Append("<div class=\"td_03\">" + bizChild.ChildSex3 + "</div>");
                    s.Append("<div class=\"td_04\">" + bizChild.ChildPolicy3 + "</div>");
                    s.Append("<div class=\"td_05\">" + bizChild.Memos3 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + bizChild.ChildName4 + "</div>");
                    s.Append("<div class=\"td_02\">" + bizChild.ChildBirthday4 + "</div>");
                    s.Append("<div class=\"td_03\">" + bizChild.ChildSex4 + "</div>");
                    s.Append("<div class=\"td_04\">" + bizChild.ChildPolicy4 + "</div>");
                    s.Append("<div class=\"td_05\">" + bizChild.Memos4 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + bizChild.ChildName5 + "</div>");
                    s.Append("<div class=\"td_02\">" + bizChild.ChildBirthday5 + "</div>");
                    s.Append("<div class=\"td_03\">" + bizChild.ChildSex5 + "</div>");
                    s.Append("<div class=\"td_04\">" + bizChild.ChildPolicy5 + "</div>");
                    s.Append("<div class=\"td_05\">" + bizChild.Memos5 + "</div>");
                    s.Append("</div>");

                    //s.Append("<div class=\"tr_03 clearfix\">");
                    //s.Append("<div class=\"td_01\">&nbsp;</div>");//备注
                    //s.Append("</div>");

                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<p><img src=\"/BizInfo/images/info/paper_0122b_2.gif\" /></p>");
                    s.Append("</div>");
                    //服务证副页
                    s.Append("<div class=\"paper_print_0102c\">");
                    s.Append("<div class=\"title\">《生育服务证》副页</div>");
                    s.Append("<div class=\"number\">编号：" + CertificateNum + "</div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<div class=\"tr_01 clearfix\" style=\"padding-top:48px;\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds01 + "</div>");//男
                    s.Append("<div class=\"td_02\">" + BIZ_Common.FormatString(biz.Fileds32, "2") + "</div>");//生日
                    s.Append("<div class=\"td_03\">" + biz.Fileds03 + "</div>");//民族
                    s.Append("<div class=\"td_04\">" + biz.Fileds04 + "</div>");//户口
                    s.Append("<div class=\"td_05\">" + biz.Fileds33 + "</div>");//婚姻状况
                    s.Append("<div class=\"td_06\">" + BIZ_Common.FormatString(biz.Fileds34, "2") + "</div>");//结婚日期
                    s.Append("<div class=\"td_07\">" + biz.Fileds17 + "</div>");//是否独子
                    s.Append("<div class=\"td_08\">" + biz.RegAreaNameA + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01 clearfix\" style=\"height:35px;\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds08 + "</div>");//女
                    s.Append("<div class=\"td_02\">" + BIZ_Common.FormatString(biz.Fileds31, "2") + "</div>");//生日
                    s.Append("<div class=\"td_03\">" + biz.Fileds10 + "</div>");//民族
                    s.Append("<div class=\"td_04\">" + biz.Fileds11 + "</div>");//户口
                    s.Append("<div class=\"td_05\">" + biz.Fileds13 + "</div>");//婚姻状况
                    s.Append("<div class=\"td_06\">" + BIZ_Common.FormatString(biz.Fileds14, "2") + "</div>");//结婚日期
                    s.Append("<div class=\"td_07\">" + biz.Fileds16 + "</div>");//是否独子
                    s.Append("<div class=\"td_08\">" + biz.RegAreaNameB + "</div>");
                    s.Append("</div>");

                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01 f_bold\">新生儿姓名</div>");
                    s.Append("<div class=\"td_02 f_bold\">出生日期</div>");
                    s.Append("<div class=\"td_03 f_bold\">性别</div>");
                    s.Append("<div class=\"td_04 f_bold\">政策属性</div>");
                    s.Append("<div class=\"td_05 f_bold\">其他情况</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + bizChild.ChildName1 + "</div>");
                    s.Append("<div class=\"td_02\">" + bizChild.ChildBirthday1 + "</div>");
                    s.Append("<div class=\"td_03\">" + bizChild.ChildSex1 + "</div>");
                    s.Append("<div class=\"td_04\">" + bizChild.ChildPolicy1 + "</div>");
                    s.Append("<div class=\"td_05\">" + bizChild.Memos1 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + bizChild.ChildName2 + "</div>");
                    s.Append("<div class=\"td_02\">" + bizChild.ChildBirthday2 + "</div>");
                    s.Append("<div class=\"td_03\">" + bizChild.ChildSex2 + "</div>");
                    s.Append("<div class=\"td_04\">" + bizChild.ChildPolicy2 + "</div>");
                    s.Append("<div class=\"td_05\">" + bizChild.Memos2 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + bizChild.ChildName3 + "</div>");
                    s.Append("<div class=\"td_02\">" + bizChild.ChildBirthday3 + "</div>");
                    s.Append("<div class=\"td_03\">" + bizChild.ChildSex3 + "</div>");
                    s.Append("<div class=\"td_04\">" + bizChild.ChildPolicy3 + "</div>");
                    s.Append("<div class=\"td_05\">" + bizChild.Memos3 + "</div>");
                    s.Append("</div>");
                    bizChild = null;
                    s.Append("</div>");
                    s.Append("<div class=\"official\"><img src=\"" + Signature + "\" height=\"165\" /></div>");//公章 下面
                    s.Append("<div class=\"official official2\"><img src=\"" + Signature + "\" height=\"165\" /></div>");//公章 中间
                    s.Append("</div>");
                    s.Append("<p><img src=\"/BizInfo/images/info/paper_0122c.gif\" /></p>");
                    s.Append("<div class=\"bottom\">");
                    s.Append("<span class=\"a3\">登记人：" + ApprovalSignName + "</span>");
                    s.Append("<span class=\"a1\">登记日期：" + BIZ_Common.FormatString(biz.StartDate, "2") + "</span>");
                    s.Append("<span class=\"a2\">登记单位（盖章）</span>");
                    s.Append("</div>");
                    s.Append("</div>");
                }
                else
                {
                    s.Append("<div class=\"paper_print_0101a\"><p><img src=\"/BizInfo/images/info/paper_0101a.jpg\" /></p></div>");
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
