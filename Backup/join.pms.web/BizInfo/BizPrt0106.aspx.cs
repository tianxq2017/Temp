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
    public partial class BizPrt0106 : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号

        private string m_SqlParams;
        public string m_TargetUrl;
        public string m_SiteName = System.Configuration.ConfigurationManager.AppSettings["SiteName"];
        private string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                if (m_ActionName == "chexiao")
                {
                    // 清理删除,撤销 
                    if (!PageValidate.IsNumber(m_ObjID))
                    {
                        m_ObjID = m_ObjID.Replace("s", ",");
                    }
                    DelBiz(m_ObjID);
                }
                else if (m_ActionName == "logoff")
                {
                    if (!PageValidate.IsNumber(m_ObjID))
                    {
                        m_ObjID = m_ObjID.Replace("s", ",");
                    }
                    SetBizLogOff(m_ObjID);
                }
                else
                {
                    ShowBizInfo(m_ObjID);
                    // 获取业务电子证照信息
                    if (m_ActionName == "view" || m_ActionName == "viewDetails")
                    {
                        BIZ_Docs doc = new BIZ_Docs();
                        this.LiteralDocs.Text = doc.GetBizDocsForView(m_ObjID);
                        doc = null;
                    }
                }
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

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
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
        /// 清理业务,撤销业务
        /// </summary>
        /// <param name="bizID"></param>
        private void DelBiz(string bizID)
        {
            try
            {
                // BIZ_Contents --> Attribs: 0,初始提交;1,审核中 2,通过 3,补正 4,撤销,撤销 5,注销 9,归档
                if (CommPage.CheckBizDelAttribs("BizID IN(" + bizID + ")"))
                {
                    m_SqlParams = "UPDATE BIZ_Contents SET Attribs=4 WHERE BizID IN(" + bizID + ")";
                    DbHelperSQL.ExecuteSql(m_SqlParams);

                    CommPage.SetBizLog(bizID, m_UserID, "业务撤销", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 进行了业务(内蒙古自治区政策内二孩和双女（蒙古族三女）结扎家庭奖励申请审核表)撤销操作");

                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您所选择的业务撤销操作成功！", m_TargetUrl, true, true);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：已经开始审核的业务数据禁止撤销操作，可选择“补正”操作！", m_TargetUrl, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：" + ex.Message, m_TargetUrl, true, true);
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="bizID"></param>
        private void SetBizLogOff(string bizID)
        {
            try
            {
                if (CommPage.CheckLogOffAttribs("BizID IN(" + bizID + ")"))
                {
                    System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                    list.Add("UPDATE BIZ_Certificates SET Attribs=1 WHERE BizID IN(" + bizID + ")");
                    list.Add("UPDATE BIZ_Contents SET Attribs=5 WHERE BizID IN(" + bizID + ")");
                    DbHelperSQL.ExecuteSqlTran(list);
                    list = null;

                    CommPage.SetBizLog(bizID, m_UserID, "业务注销", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 进行了业务(内蒙古自治区政策内二孩和双女（蒙古族三女）结扎家庭奖励申请审核表)注销操作");

                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您所选择的业务注销操作成功！", m_TargetUrl, true, true);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：只有审核完毕的业务才可以执行“注销”操作！", m_TargetUrl, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：" + ex.Message, m_TargetUrl, true, true);
            }
        }
        /*

         */

        /// <summary>
        /// 查看详细信息
        /// </summary>
        /// <param name="objID"></param>
        private void ShowBizInfo(string bizID)
        {
            string cerNo = "", workFlowInfo = string.Empty;
            string qrInfo = "", qrFiles = string.Empty;
            StringBuilder s = new StringBuilder();
            try
            {
                BIZ_Contents biz = new BIZ_Contents();
                biz.BizID = bizID;
                biz.SearchWhere = "BizID=" + bizID;
                biz.SelectAll(false);
                if (!string.IsNullOrEmpty(biz.BizCode))
                {
                    qrInfo = "内蒙古自治区政策内二孩和双女（蒙古族三女）结扎家庭奖励申请表；申请人及配偶姓名、身份证号：" + biz.Fileds01 + "(" + biz.PersonCidA + ")、" + biz.Fileds08 + "(" + biz.PersonCidB + ")。";
                    workFlowInfo = GetWorkFlows(bizID, biz.CurAreaNameA, biz.Attribs, ref cerNo, ref qrFiles, qrInfo, biz.SelAreaCode, biz.StartDate);
                    //workFlowInfo = GetWorkFlows(bizID, biz.CurAreaNameA, biz.Attribs, ref cerNo);
                    this.LiteralAreaName.Text = "<div class=\"add\">" + BIZ_Common.GetAreaName(biz.SelAreaCode, "1") + "</div>";
                    s.Append("<div class=\"print_table_bg\">");
                    if (!string.IsNullOrEmpty(qrFiles)) s.Append("<div class=\"code\"><img src=\"" + qrFiles + "\" /></div>");
                    //本人信息
                    s.Append("<div class=\"tr_01 clearfix\" style=\"padding-top:10px;\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds01 + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.Fileds02 + "</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(biz.Fileds32, "") + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.Fileds04 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds33 + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.Fileds34 + "</div>");
                    s.Append("<div class=\"td_03\">" + biz.PersonCidA + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_04\">" + biz.RegAreaNameA + "</div>");
                    s.Append("<div class=\"td_05\">" + biz.CurAreaNameA + "</div>");
                    s.Append("</div>");

                    //配偶信息
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds08 + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.Fileds09 + "</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(biz.Fileds31, "") + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.Fileds11 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds13 + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.Fileds14 + "</div>");
                    s.Append("<div class=\"td_03\">" + biz.PersonCidB + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_04\">" + biz.RegAreaNameB + "</div>");
                    s.Append("<div class=\"td_05\">" + biz.CurAreaNameB + "</div>");
                    s.Append("</div>");                   

                    //子女信息
                    BIZ_PersonChildren children = new BIZ_PersonChildren();
                    children.Select("", bizID);

                    s.Append("<div class=\"tr_02 clearfix\" style=\"padding-top:32px;\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName1 + "</div>");
                    s.Append("<div class=\"td_02\">" + children.ChildSex1 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(children.ChildBirthday1, "") + "</div>");
                    s.Append("<div class=\"td_04\">" + GetDateFormat(children.ChildDeathday1, "") + "</div>");
                    s.Append("<div class=\"td_05\">" + children.ChildSource1 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName2 + "</div>");
                    s.Append("<div class=\"td_02\">" + children.ChildSex2 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(children.ChildBirthday2, "") + "</div>");
                    s.Append("<div class=\"td_04\">" + GetDateFormat(children.ChildDeathday2, "") + "</div>");
                    s.Append("<div class=\"td_05\">" + children.ChildSource2 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName3 + "</div>");
                    s.Append("<div class=\"td_02\">" + children.ChildSex3 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(children.ChildBirthday3, "") + "</div>");
                    s.Append("<div class=\"td_04\">" + GetDateFormat(children.ChildDeathday3, "") + "</div>");
                    s.Append("<div class=\"td_05\">" + children.ChildSource3 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName4 + "</div>");
                    s.Append("<div class=\"td_02\">" + children.ChildSex4 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(children.ChildBirthday4, "") + "</div>");
                    s.Append("<div class=\"td_04\">" + GetDateFormat(children.ChildDeathday4, "") + "</div>");
                    s.Append("<div class=\"td_05\">" + children.ChildSource4 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName5 + "</div>");
                    s.Append("<div class=\"td_02\">" + children.ChildSex5 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(children.ChildBirthday5, "") + "</div>");
                    s.Append("<div class=\"td_04\">" + GetDateFormat(children.ChildDeathday5, "") + "</div>");
                    s.Append("<div class=\"td_05\">" + children.ChildSource5 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName6 + "</div>");
                    s.Append("<div class=\"td_02\">" + children.ChildSex6 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(children.ChildBirthday6, "") + "</div>");
                    s.Append("<div class=\"td_04\">" + GetDateFormat(children.ChildDeathday6, "") + "</div>");
                    s.Append("<div class=\"td_05\">" + children.ChildSource6 + "</div>");
                    s.Append("</div>");
                    children = null;

                    //申报理由
                    s.Append("<div class=\"tr_03 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds18 + "</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(biz.Fileds19, "") + "</div>");
                    s.Append("<div class=\"td_03\">" + biz.ContactTelA + "</div>");
                    s.Append("</div>");
                    //申报理由
                    s.Append("<div class=\"tr_04 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds27 + "</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(biz.Fileds28, "") + "</div>");
                    s.Append("</div>");

                    //审核流程
                    s.Append(workFlowInfo);

                    s.Append("</div>");

                    if (m_ActionName == "priint")
                    {
                        CommPage.SetBizLog(bizID, m_UserID, "业务打印", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 打印了农村部分计划生育家庭奖励扶助对象申报审核");
                        //保存打印的证件
                        join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                        log.BizID = bizID;
                        log.BizCode = biz.BizCode;
                        log.SetCertificateLog(biz.BizName, biz.Fileds01, biz.PersonCidA, "农村部分计划生育家庭奖励扶助对象申报审核");
                        log = null;
                    }
                }
                biz = null;
            }
            catch { }

            this.LiteralBizInfo.Text = s.ToString(); ;
        }


        /// <summary>
        /// Attribs: 0初始提交; 1审核中; 2,通过 3驳回; 4删除; 9,归档
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="curAreaName"></param>
        /// <returns></returns>
        private string GetWorkFlows(string bizID, string curAreaName, string attribs, ref string cerNo, ref string qrFiles, string qrInfo, string SelAreaCode, string StartDate)
        {
            string seal1 = "", seal2 = "", seal3 = "", seal4 = string.Empty;
            string sign1 = "", sign2 = "", sign3 = "", sign33 = "", sign4 = "", sign44 = string.Empty;
            string validDateEnd = string.Empty;
            StringBuilder b = new StringBuilder();
            DataTable dt = new DataTable();
            try
            {
                m_SqlParams = "SELECT AreaName,Comments,Approval,ApprovalUserTel,Signature,OprateDate,CertificateNoA,CertificateNoB,CertificateMemo,AuditUser,ApprovalSignPath,AuditUserSignPath  FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep";
                dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (dt.Rows.Count == 4)
                {
                    seal1 = dt.Rows[0]["Signature"].ToString();
                    seal2 = dt.Rows[1]["Signature"].ToString();
                    seal3 = dt.Rows[2]["Signature"].ToString();
                    seal4 = dt.Rows[3]["Signature"].ToString();
                    if (!string.IsNullOrEmpty(seal1)) seal1 = "<img src=\"" + seal1 + "\"   height=\"165\"/>";
                    if (!string.IsNullOrEmpty(seal2)) seal2 = "<img src=\"" + seal2 + "\"   height=\"165\"/>";
                    if (!string.IsNullOrEmpty(seal3)) seal3 = "<img src=\"" + seal3 + "\"   height=\"165\"/>";
                    if (!string.IsNullOrEmpty(seal4)) seal4 = "<img src=\"" + seal4 + "\"   height=\"165\"/>";

                    sign1 = dt.Rows[0]["ApprovalSignPath"].ToString();
                    sign2 = dt.Rows[1]["ApprovalSignPath"].ToString();
                    sign3 = dt.Rows[2]["ApprovalSignPath"].ToString();
                    sign33 = dt.Rows[2]["AuditUserSignPath"].ToString();
                    sign4 = dt.Rows[3]["ApprovalSignPath"].ToString();
                    sign44 = dt.Rows[3]["AuditUserSignPath"].ToString();


                    if (!string.IsNullOrEmpty(sign1)) sign1 = "<img src=\"" + sign1 + "\"  height=\"25\" />";
                    if (!string.IsNullOrEmpty(sign2)) sign2 = "<img src=\"" + sign2 + "\"  height=\"25\" />";
                    if (!string.IsNullOrEmpty(sign3)) sign3 = "<img src=\"" + sign3 + "\"  height=\"25\" />";
                    if (!string.IsNullOrEmpty(sign33)) sign33 = "<img src=\"" + sign33 + "\"  height=\"25\" />";
                    if (!string.IsNullOrEmpty(sign4)) sign3 = "<img src=\"" + sign3 + "\"  height=\"25\" />";
                    if (!string.IsNullOrEmpty(sign44)) sign44 = "<img src=\"" + sign44 + "\"  height=\"25\" />";

                    cerNo = dt.Rows[3]["CertificateNoA"].ToString();

                    //本人村
                    b.Append("<div class=\"tr_05 clearfix\">");
                    b.Append("<div class=\"tr_05_l\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[0]["Comments"].ToString() + "</div>");
                    //兼容库伦系统
                    if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">经办人：" + dt.Rows[0]["Approval"].ToString() + "<br/>" + GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "") + "</div>"); }
                    else { b.Append("<div class=\"td_02\">经办人：" + sign1 + "<br/>" + GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "") + "</div>"); }
                    
                    b.Append("<div class=\"official\">" + seal1 + "</div>");
                    b.Append("</div>");

                    //配偶村
                    b.Append("<div class=\"tr_05_r\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[1]["Comments"].ToString() + "</div>");
                    //兼容库伦系统
                    if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">经办人：" + dt.Rows[1]["Approval"].ToString() + "<br/>" + GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "") + "</div>"); }
                    else { b.Append("<div class=\"td_02\">经办人：" + sign2 + "<br/>" + GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "") + "</div>"); }
                    
                    b.Append("<div class=\"official\">" + seal2 + "</div>");
                    b.Append("</div>");
                    b.Append("</div>");
                    //本人镇
                    b.Append("<div class=\"tr_05 clearfix\">");
                    b.Append("<div class=\"tr_05_l\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[2]["Comments"].ToString() + "</div>");
                    //兼容库伦系统
                    if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">经办人：" + dt.Rows[2]["Approval"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;责任人：" + dt.Rows[2]["AuditUser"].ToString() + "<br/>" + GetDateFormat(dt.Rows[2]["OprateDate"].ToString(), "") + "</div>"); }
                    else { b.Append("<div class=\"td_02\">经办人：" + sign3 + "&nbsp;&nbsp;&nbsp;&nbsp;责任人：" + sign33 + "<br/>" + GetDateFormat(dt.Rows[2]["OprateDate"].ToString(), "") + "</div>"); }
                    
                    b.Append("<div class=\"official\">" + seal3 + "</div>");
                    b.Append("</div>");

                    

                    b.Append("<div class=\"tr_05_r\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[3]["Comments"].ToString() + "</div>");
                    //兼容库伦系统
                    if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">经办人：" + dt.Rows[3]["Approval"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;责任人：" + dt.Rows[3]["AuditUser"].ToString() + "<br/>" + GetDateFormat(dt.Rows[3]["OprateDate"].ToString(), "") + "</div>"); }
                    else { b.Append("<div class=\"td_02\">经办人：" + sign4 + "&nbsp;&nbsp;&nbsp;&nbsp;责任人：" + sign44 + "<br/>" + GetDateFormat(dt.Rows[3]["OprateDate"].ToString(), "") + "</div>"); }
                    
                    b.Append("<div class=\"official\">" + seal4 + "</div>");
                    b.Append("</div>");


                    b.Append("</div>");

                    b.Append("<div class=\"apply\">");
                    b.Append("<div class=\"td_01\">以上是我们夫妻双方意愿的真实表达。我们承诺所申请的理由和提供的材料真实，并愿意承担相应的法律责任。</div>");

                    b.Append("<div class=\"clr10\"></div>");
                    b.Append("<div class=\"td_03\" style=\"float:right;pading-left:20px;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;</div>");
                    b.Append("<div class=\"td_02\" style=\"float:right;\">申请人（签名）：<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>");
                    b.Append("<div class=\"clr5\"></div>");

                    b.Append("</div>");

                    b.Append("<div class=\"tr_06\">");
                    b.Append("<div class=\"td_01\"></div>");
                    b.Append("</div>");

                    // == 生成二维码 start====
                    string workFlowsID = string.Empty;
                    join.pms.dal.BizWorkFlows bwf = new join.pms.dal.BizWorkFlows();
                    bwf.BizID = bizID;
                    bwf.FilterSQL = "BizID=" + bizID + " ORDER BY BizStep";
                    bwf.GetWorkFlowsByBizID();
                    workFlowsID = bwf.WorkFlowsID;
                    qrFiles = bwf.QRCodeFiles;
                    if (!string.IsNullOrEmpty(workFlowsID) && string.IsNullOrEmpty(qrFiles))
                    {
                        QRCode qr = new QRCode();
                        if (qr.SetQrCode(qrInfo, Server.MapPath("/"), ref qrFiles))
                        {
                            bwf.SettWorkFlowsQrCode(workFlowsID, qrFiles);
                        }
                        qr = null;
                    }
                    bwf = null;
                    // == end=========
                }
                else
                {
                    b.Append("<div class=\"tr_04\"><br/>流程节点参数预制错误！<br/><br/></div>");
                }
                dt = null;
            }
            catch
            {
                b.Append("<div class=\"tr_04\"><br/>获取流程节点审核信息时发生错误！<br/><br/></div>");
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

