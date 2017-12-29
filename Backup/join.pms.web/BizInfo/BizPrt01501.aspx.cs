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
    public partial class BizPrt01501 : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;
        private string m_UserID; // 当前登录的操作用户编号
        private string m_SqlParams;
        public string m_TargetUrl;

        private string m_Qcfwzh; // 婚育健康服务证号
        private string mp; // 来路

        public string m_SiteName = System.Configuration.ConfigurationManager.AppSettings["SiteName"];
        private string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];
        /// <summary>
        /// 页面初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                ShowBizInfo(m_ObjID);
            }
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
            //mp = Request.QueryString["mp"];
            //m_Qcfwzh = Request.QueryString["fwzh"];
        }
        
        /// <summary>
        /// 查看详细信息
        /// </summary>
        /// <param name="objID"></param>
        private void ShowBizInfo(string bizID)
        {
            string photos = string.Empty;
            string cerNo = "", workFlowInfo = string.Empty;
            StringBuilder s = new StringBuilder();
            try
            {
                BIZ_Contents biz = new BIZ_Contents();
                biz.BizID = bizID;
                biz.SearchWhere = "BizID=" + bizID;
                biz.SelectAll(false);
                if (!string.IsNullOrEmpty(biz.BizCode))
                {
                    // 获取男方女方照片
                    photos = biz.PersonPhotos;
                    if (string.IsNullOrEmpty(photos))
                    {
                        //如果照片为空
                        object DocsPhoto = DbHelperSQL.GetSingle("SELECT TOP 1 DocsPath FROM BIZ_Docs WHERE DocsName like '%照片%' and BizID =" + bizID + " order by CommID desc  ");
                        if (DocsPhoto != null)
                        {
                            string DocsPhotos = DocsPhoto.ToString();
                            photos = m_SvrsUrl + DocsPhotos;
                            m_SqlParams = "UPDATE BIZ_Contents SET PersonPhotos='" + DocsPhotos + "' WHERE BizID =" + bizID + "";
                            DbHelperSQL.ExecuteSql(m_SqlParams);
                        }
                    }
                    if (!string.IsNullOrEmpty(photos)) photos = "<img src=\"" + photos + "\" />";


                    // == 生成二维码 start====
                    string workFlowsID = string.Empty;
                    string qrFiles = string.Empty;
                    string barFiles = string.Empty;
                    //join.pms.dal.BizWorkFlows bwf = new join.pms.dal.BizWorkFlows();
                    //bwf.BizID = bizID;
                    //bwf.FilterSQL = "BizID=" + bizID + " ORDER BY BizStep";
                    //bwf.GetWorkFlowsByBizID();
                    //workFlowsID = bwf.WorkFlowsID;
                    //qrFiles = bwf.QRCodeFiles;
                    //if (!string.IsNullOrEmpty(workFlowsID) && string.IsNullOrEmpty(qrFiles))
                    //{
                        QRCode qr = new QRCode();
                    //    if (qr.SetQrCode(biz.QcfwzBm, Server.MapPath("/"), ref qrFiles))
                    //    {
                    qr.SetQrCode(biz.QcfwzBm, Server.MapPath("/"), ref qrFiles);
                            //bwf.SettWorkFlowsQrCode(workFlowsID, qrFiles);
                        //}

                    //生成条形码
                    qr.SetBarCode(biz.QcfwzBm, Server.MapPath("/"), ref barFiles);

                        qr = null;
                    //}
                    //bwf = null;
                    // == end=========


                   // workFlowInfo = GetWorkFlows(bizID, biz.CurAreaNameA, biz.Attribs, ref cerNo, biz.Fileds36);
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<div class=\"print_l\"></div>");
                    s.Append("<div class=\"print_r\">");
                    s.Append("<div class=\"table_01\">");
                    s.Append("<div class=\"photo\">" + photos + "</div>");
                    s.Append("<div class=\"tr_04\">证件编号：" + biz.QcfwzBm + "</div>");

                   

                    s.Append("<div class=\"table_l\">");
                    s.Append("<div class=\"tr_00\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">女方<br />信息</li>");
                    s.Append("<li class=\"a2\">男方<br />信息</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"table_r\">");
                    s.Append("<div class=\"tr_01 tr_01a clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">姓名</li>");
                    s.Append("<li class=\"a2\">" + biz.Fileds08 + "</li>");
                    s.Append("<li class=\"a3\">民族</li>");
                    s.Append("<li class=\"a4\">" + biz.Fileds10 + "</li>");
                    s.Append("<li class=\"a5\">身份证号</li>");
                    s.Append("<li class=\"a6\"><p>" + biz.PersonCidB + "</p></li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">婚姻状况</li>");
                    s.Append("<li class=\"a2\">" + biz.Fileds13 + "</li>");
                    s.Append("<li class=\"a3\">出生日期</li>");
                    s.Append("<li class=\"a4\">" + GetDateFormat(biz.Fileds31, "") + "</li>");
                    s.Append("<li class=\"a5\">联系电话</li>");
                    s.Append("<li class=\"a6\">" + biz.ContactTelB + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">户籍地址</li>");
                    s.Append("<li class=\"a2\">" + biz.RegAreaNameB.Replace("&", " ") + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">现居住地</li>");
                    s.Append("<li class=\"a2\">" + biz.CurAreaNameB.Replace("&", " ") + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");

                    s.Append("<div class=\"tr_01 tr_01b clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">姓名</li>");
                    s.Append("<li class=\"a2\">" + biz.Fileds01 + "</li>");
                    s.Append("<li class=\"a3\">民族</li>");
                    s.Append("<li class=\"a4\">" + biz.Fileds03 + "</li>");
                    s.Append("<li class=\"a5\">身份证号</li>");
                    s.Append("<li class=\"a6\"><p>" + biz.PersonCidA + "</p></li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">婚姻状况</li>");
                    s.Append("<li class=\"a2\">" + biz.Fileds33 + "</li>");
                    s.Append("<li class=\"a3\">出生日期</li>");
                    s.Append("<li class=\"a4\">" + GetDateFormat(biz.Fileds32, "") + "</li>");
                    s.Append("<li class=\"a5\">联系电话</li>");
                    s.Append("<li class=\"a6\">" + biz.ContactTelA + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">户籍地址</li>");
                    s.Append("<li class=\"a2\">" + biz.RegAreaNameA.Replace("&", " ") + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">现居住地</li>");
                    s.Append("<li class=\"a2\">" + biz.CurAreaNameA.Replace("&", " ") + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"clr\"></div>");
                    s.Append("<div class=\"tr_03 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">结婚时间</li>");
                    s.Append("<li class=\"a2\">" + GetDateFormat(biz.Fileds14, "") + "</li>");
                    s.Append("<li class=\"a3\">结婚证号</li>");
                    s.Append("<li class=\"a4\">" + biz.Fileds47 + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_05 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">");
                    s.Append("<p class=\"txt\">扫描处理业务</p>");
                    //条形码
                    if (!string.IsNullOrEmpty(barFiles))
                        s.Append("<p><img src=\""+barFiles+"\" width=\"158\" height=\"48\" /></p>");
                    s.Append("</li>");
                    s.Append("<li class=\"a2\">");
                    s.Append("<p class=\"txt\">更多健康信息扫描</p>");
                    //二维码
                    if (!string.IsNullOrEmpty(qrFiles)) 
                    s.Append("<p><img src=\"" + qrFiles + "\"  width=\"90\" height=\"90\" /></p>");

                    s.Append("</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"clr\"></div>");
                    s.Append("</div>");
                    s.Append("</div>");
                    

                    //审核流程
                    //s.Append(workFlowInfo);
                    if (m_ActionName == "priint")
                    {
                        CommPage.SetBizLog(bizID, m_UserID, "业务打印", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 打印了证件基础信息");
                        //保存打印的证件
                        join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                        log.BizID = bizID;
                        log.BizCode = biz.BizCode;
                        log.SetCertificateLog(biz.BizName, biz.Fileds01, biz.PersonCidA, "打印了01501");
                        log = null;
                    }
                }
                biz = null;
            }
            catch { }

            this.LiteralBizInfo.Text = s.ToString();
        }

        /// <summary>
        /// 取得流程签名、章
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="curAreaName"></param>
        /// <returns></returns>
        private string GetWorkFlows(string bizID, string curAreaName, string attribs, ref string cerNo, string yjDate)
        {
            string OprateDate1 = string.Empty;
            string seal1 = string.Empty;
            string tel1 = string.Empty;
            string sign1 = string.Empty;
            string CertificateDateStart, CertificateDateEnd = string.Empty;
            StringBuilder b = new StringBuilder();
            DataTable dt = new DataTable();
            try
            {
                m_SqlParams = "SELECT AreaName,Comments,Approval,AuditUser,AuditUserSealPath,OprateDate,CertificateNoA,CertificateNoB,CertificateMemo,CertificateDateStart,CertificateDateEnd,ApprovalSignPath,ApprovalUserTel,AuditUserSignPath FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep";
                dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (dt.Rows.Count == 1)
                {
                    OprateDate1 = GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "1");
                    seal1 = dt.Rows[0]["AuditUserSealPath"].ToString();
                    tel1 = dt.Rows[0]["ApprovalUserTel"].ToString();
                    if (!string.IsNullOrEmpty(seal1)) seal1 = "<img src=\"" + seal1 + "\"     width=\"150\" height=\"150\"/>";
                    sign1 = dt.Rows[0]["ApprovalSignPath"].ToString();
                    if (!string.IsNullOrEmpty(sign1)) sign1 = "<img src=\"" + sign1 + "\"  />";

                    cerNo = dt.Rows[0]["CertificateNoA"].ToString();

                    b.Append("<div class=\"seal_02 clearfix\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[0]["Comments"].ToString() + "</div>");
                    b.Append("<div class=\"td_02\">审批人：" + sign1 + "&nbsp;" + OprateDate1 + "<br />电话：" + tel1 + "</div>");
                    b.Append("<div class=\"official\">" + seal1 + "</div>");
                    b.Append("</div>");
                }
                else
                {
                    b.Append("<div class=\"seal_02 clearfix\">");
                    b.Append("<div class=\"td_01\">&nbsp;</div>");
                    b.Append("<div class=\"td_02\">审批人：&nbsp;<br />电话：</div>");
                    b.Append("<div class=\"official\">&nbsp;&nbsp;</div>");
                    b.Append("</div>");
                }
                dt = null;
            }
            catch
            {
                b.Append("<div class=\"tr_05 clearfix\"><br/>获取流程节点审核信息时发生错误！<br/><br/></div>");
            }
            return b.ToString();
        }

        private string GetDateFormat(string inStr, string type)
        {
            string returnVal = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(inStr))
                {
                    if (type == "1") { returnVal = DateTime.Parse(inStr).ToString("yyyy年MM月dd日"); }
                    else { returnVal = DateTime.Parse(inStr).ToString("yyyy-MM-dd"); }
                }
            }
            catch { returnVal = inStr; }
            return returnVal;
        }


        #endregion
    }
}

