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
    public partial class BizPrt0110 : System.Web.UI.Page
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
                    if (m_ActionName == "view" || m_ActionName == "viewDetails")
                    {
                        // 获取业务电子证照信息
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
                    CommPage.SetBizLog(bizID, m_UserID, "业务撤销", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 进行了业务(一孩生育登记)撤销操作");

                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您所选择的业务撤销操作成功！", m_TargetUrl, true, true);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：由群众发起的业务申请或者是已经开始审核的业务数据禁止撤销操作，可选择“补正”操作！", m_TargetUrl, true, true);
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

                    CommPage.SetBizLog(bizID, m_UserID, "业务注销", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 进行了业务(一孩生育登记)注销操作");

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
        /// <summary>
        /// 查看详细信息
        /// </summary>
        /// <param name="objID"></param>
        private void ShowBizInfo(string bizID)
        {
            string photos = string.Empty;
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
                    qrInfo = "婚育情况证明；申请人及配偶姓名、身份证号：" + biz.Fileds01 + "(" + biz.PersonCidA + ")、" + biz.Fileds08 + "(" + biz.PersonCidB + ")。";
                    workFlowInfo = GetWorkFlows(bizID, biz.CurAreaNameA, biz.Attribs, ref cerNo, biz.BizStep, ref qrFiles, qrInfo, biz.SelAreaCode, biz.StartDate);
                    //workFlowInfo = GetWorkFlows(bizID, biz.CurAreaNameA, biz.Attribs, ref cerNo,biz.BizStep);

                    //s.Append("<div class=\"number\">编号：" + cerNo + "</div>");
                    s.Append("<div class=\"number\"></div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    if (!string.IsNullOrEmpty(qrFiles)) s.Append("<div class=\"code\"><img src=\"" + qrFiles + "\" /></div>");
                    //本人信息
                    s.Append("<div class=\"tr_01 clearfix\" style=\"padding-top:14px;\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds01 + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.Fileds02 + "</div>");
                    s.Append("<div class=\"td_03\">" + biz.Fileds03 + "</div>");
                    s.Append("<div class=\"td_04\">" + biz.Fileds04 + "</div>");
                    s.Append("</div>");

                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_05\">" + biz.PersonCidA + "</div>");
                    s.Append("<div class=\"td_03\">" + biz.ContactTelA + "</div>");
                    s.Append("<div class=\"td_04\">" + biz.Fileds33 + "</div>");
                    s.Append("</div>");

                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_07\">" + biz.RegAreaNameA + "</div>");
                    s.Append("<div class=\"td_08\">" + biz.Fileds45 + "</div>");
                    s.Append("</div>");

                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_06\">" + biz.CurAreaNameA + "</div>");
                    s.Append("</div>");

                    //配偶信息
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds08 + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.Fileds09 + "</div>");
                    s.Append("<div class=\"td_03\">" + biz.Fileds10 + "</div>");
                    s.Append("<div class=\"td_04\">" + biz.Fileds11 + "</div>");
                    s.Append("</div>");

                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_05\">" + biz.PersonCidB + "</div>");
                    s.Append("<div class=\"td_03\">" + biz.ContactTelB + "</div>");
                    s.Append("<div class=\"td_04\">" + biz.Fileds13 + "</div>");
                    s.Append("</div>");

                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_06\">" + biz.RegAreaNameB + "</div>");
                    s.Append("</div>");

                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_06\">" + biz.CurAreaNameB + "</div>");
                    s.Append("</div>");

                    //子女信息
                    BIZ_PersonChildren children = new BIZ_PersonChildren();
                    children.Select("", bizID);

                    s.Append("<div class=\"tr_02 clearfix\" style=\"padding-top:39px;\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName1 + "</div>");
                    s.Append("<div class=\"td_02\">" + children.ChildSex1 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(children.ChildBirthday1, "") + "</div>");
                    if (string.IsNullOrEmpty(children.ChildName1))
                    {
                        s.Append("<div class=\"td_04\"></div>");
                    }
                    else
                    {
                        s.Append("<div class=\"td_04\">1孩</div>");
                    }
                    s.Append("<div class=\"td_05\">" + children.ChildPolicy1 + "</div>");
                    s.Append("<div class=\"td_06\">" + children.ChildCardID1 + "</div>");
                    s.Append("<div class=\"td_07\">" + children.ChildSource1 + "</div>");
                    s.Append("<div class=\"td_08\">" + children.Memos1 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName2 + "</div>");
                    s.Append("<div class=\"td_02\">" + children.ChildSex2 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(children.ChildBirthday2, "") + "</div>");
                    if (string.IsNullOrEmpty(children.ChildName2))
                    {
                        s.Append("<div class=\"td_04\"></div>");
                    }
                    else
                    {
                        s.Append("<div class=\"td_04\">2孩</div>");
                    }
                    s.Append("<div class=\"td_05\">" + children.ChildPolicy2 + "</div>");
                    s.Append("<div class=\"td_06\">" + children.ChildCardID2 + "</div>");
                    s.Append("<div class=\"td_07\">" + children.ChildSource2 + "</div>");
                    s.Append("<div class=\"td_08\">" + children.Memos2 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName3 + "</div>");
                    s.Append("<div class=\"td_02\">" + children.ChildSex3 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(children.ChildBirthday3, "") + "</div>");
                    if (string.IsNullOrEmpty(children.ChildName3))
                    {
                        s.Append("<div class=\"td_04\"></div>");
                    }
                    else
                    {
                        s.Append("<div class=\"td_04\">3孩</div>");
                    }
                    s.Append("<div class=\"td_05\">" + children.ChildPolicy3 + "</div>");
                    s.Append("<div class=\"td_06\">" + children.ChildCardID3 + "</div>");
                    s.Append("<div class=\"td_07\">" + children.ChildSource3 + "</div>");
                    s.Append("<div class=\"td_08\">" + children.Memos3 + "</div>");
                    s.Append("</div>");
                    children = null;

                    s.Append("<div class=\"tr_03 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds18 + "</div>");
                    s.Append("</div>");

                    //审核流程
                    s.Append(workFlowInfo);

                    s.Append("<div class=\"apply\">");
                    s.Append("<div class=\"td_01\">以上是我们夫妻双方意愿的真实表达。我们承诺所申请的理由和提供的材料真实，并愿意承担相应的法律责任。</div>");

                    s.Append("<div class=\"clr10\"></div>");
                    s.Append("<div class=\"td_03\" style=\"float:right;pading-left:20px;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;</div>");
                    s.Append("<div class=\"td_02\" style=\"float:right;\">申请人（签名）：<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>");
                    s.Append("<div class=\"clr5\"></div>");

                    s.Append("</div>");

                    s.Append(" </div>");
                    s.Append(" </div>");
                    if (m_ActionName == "priint")
                    {
                        CommPage.SetBizLog(bizID, m_UserID, "业务打印", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 打印了婚育情况证明");
                        //保存打印的证件
                        join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                        log.BizID = bizID;
                        log.BizCode = biz.BizCode;
                        log.SetCertificateLog(biz.BizName, biz.Fileds01, biz.PersonCidA, "打印了婚育情况证明");
                        log = null;
                    }
                }
                biz = null;
            }
            catch { }

            this.LiteralBizInfo.Text = s.ToString();
        }
        /// <summary>
        /// 取得婚姻状况
        /// </summary>
        /// <param name="instr"></param>
        /// <returns></returns>
        private string GetMarryStats(string instr)
        {

            if (string.IsNullOrEmpty(instr))
            {
                return "再婚";
            }
            else
            {
                if (instr == "初婚") { return "再婚"; }
                else { return instr; }
            }
        }
        /// <summary>
        /// 获取工作单位或住址
        /// </summary>
        /// <returns></returns>
        private string GetAreaUnit(string unit, string area)
        {
            string returnVal = string.Empty;
            if (!string.IsNullOrEmpty(unit))
            {
                returnVal = area + "(" + unit + ")";
            }
            else
            {
                returnVal = area;
            }

            return returnVal;

        }
        /// <summary>
        /// Attribs: 0初始提交; 1审核中; 2,通过 3驳回; 4删除; 9,归档
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="curAreaName"></param>
        /// <returns></returns>
        private string GetWorkFlows(string bizID, string curAreaName, string attribs, ref string cerNo, string bizStep, ref string qrFiles, string qrInfo, string SelAreaCode, string StartDate)
        {
            string seal1 = "", seal2 = "", seal3 = string.Empty;
            string sign1 = "", sign2 = "", sign3 = "", sign33 = string.Empty;
            string validDateEnd = string.Empty;
            StringBuilder b = new StringBuilder();
            DataTable dt = new DataTable();
            try
            {
                m_SqlParams = "SELECT AreaName,Comments,Approval,ApprovalUserTel,Signature,OprateDate,CertificateNoA,CertificateNoB,CertificateMemo,BizStepTotal,ApprovalSignPath,AuditUserSignPath  FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep";
                dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (bizStep == "3")
                {
                    if (dt.Rows.Count == 3)
                    {
                        seal1 = dt.Rows[0]["Signature"].ToString();
                        seal2 = dt.Rows[1]["Signature"].ToString();
                        seal3 = dt.Rows[2]["Signature"].ToString();
                        if (!string.IsNullOrEmpty(seal1)) seal1 = "<img src=\"" + seal1 + "\"   height=\"165\" />";
                        if (!string.IsNullOrEmpty(seal2)) seal2 = "<img src=\"" + seal2 + "\"   height=\"165\" />";
                        if (!string.IsNullOrEmpty(seal3)) seal3 = "<img src=\"" + seal3 + "\"   height=\"165\" />";

                        sign1 = dt.Rows[0]["ApprovalSignPath"].ToString();
                        sign2 = dt.Rows[1]["ApprovalSignPath"].ToString();
                        sign3 = dt.Rows[2]["ApprovalSignPath"].ToString();
                        sign33 = dt.Rows[2]["AuditUserSignPath"].ToString();
                        if (!string.IsNullOrEmpty(sign1)) sign1 = "<img src=\"" + sign1 + "\"  height=\"25\" />";
                        if (!string.IsNullOrEmpty(sign2)) sign2 = "<img src=\"" + sign2 + "\"  height=\"25\" />";
                        if (!string.IsNullOrEmpty(sign3)) sign3 = "<img src=\"" + sign3 + "\"  height=\"25\" />";
                        if (!string.IsNullOrEmpty(sign33)) sign33 = "<img src=\"" + sign33 + "\"  height=\"25\" />";

                        if (dt.Rows[0]["BizStepTotal"].ToString() == "3") { cerNo = dt.Rows[2]["CertificateNoA"].ToString(); }
                        else { cerNo = dt.Rows[1]["CertificateNoA"].ToString(); }
                        //村
                        b.Append("<div class=\"tr_05 clearfix\">");
                        b.Append("<div class=\"tr_l\">");
                        b.Append("<div class=\"td_01\">" + dt.Rows[0]["Comments"].ToString() + "</div>");
                        //兼容库伦系统
                        if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">经办人：" + dt.Rows[0]["Approval"].ToString() + "<br />联系电话：" + dt.Rows[0]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "1") + "</div>"); }
                        else { b.Append("<div class=\"td_02\">经办人：" + sign1 + "<br />联系电话：" + dt.Rows[0]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "1") + "</div>"); }
                        //
                        
                        b.Append("<div class=\"official\">" + seal1 + "</div>");
                        b.Append("</div>");
                        //镇
                        b.Append(" <div class=\"tr_c\">");
                        b.Append("<div class=\"td_01\">" + dt.Rows[1]["Comments"].ToString() + "</div>");
                        //兼容库伦系统
                        if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">经办人：" + dt.Rows[1]["Approval"].ToString() + "<br />联系电话：" + dt.Rows[1]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "1") + "</div>"); }
                        else { b.Append("<div class=\"td_02\">经办人：" + sign2 + "<br />联系电话：" + dt.Rows[1]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "1") + "</div>"); }
                        //
                        
                        b.Append("<div class=\"official\">" + seal2 + "</div>");
                        b.Append("</div>");

                        //县
                        //if (attribs == "2" || attribs == "9")
                        //{
                        b.Append("<div class=\"tr_r\">");
                        b.Append("<div class=\"td_01\">" + dt.Rows[2]["Comments"].ToString() + "</div>");
                        //兼容库伦系统
                        if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">经办人：" + dt.Rows[2]["Approval"].ToString() + "<br />联系电话：" + dt.Rows[2]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[2]["OprateDate"].ToString(), "1") + "</div>"); }
                        else { b.Append("<div class=\"td_02\">经办人：" + sign3 + "<br />联系电话：" + dt.Rows[2]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[2]["OprateDate"].ToString(), "1") + "</div>"); }
                        //
                        
                        b.Append(" <div class=\"official\">" + seal3 + "</div>");
                        b.Append("</div>");
                        //}
                        //else
                        //{
                        //    b.Append("<div class=\"tr_r\">");
                        //    b.Append("<div class=\"td_01\">&nbsp;&nbsp;&nbsp;&nbsp;</div>");
                        //    b.Append("<div class=\"td_02\">经办人：<br />联系电话：</div>");
                        //    b.Append(" <div class=\"official\">&nbsp;</div>");
                        //    b.Append("</div>");
                        //}
                        b.Append("</div>");
                    }
                    else
                    {
                        b.Append("<div class=\"tr_04\"><br/>流程节点参数预制错误！<br/><br/></div>");
                    }
                }
                else
                {
                    if (dt.Rows.Count == 2)
                    {
                        seal1 = dt.Rows[0]["Signature"].ToString();
                        seal2 = dt.Rows[1]["Signature"].ToString();
                        if (!string.IsNullOrEmpty(seal1)) seal1 = "<img src=\"" + seal1 + "\"  height=\"165\" />";
                        if (!string.IsNullOrEmpty(seal2)) seal2 = "<img src=\"" + seal2 + "\"  height=\"165\" />";

                        sign1 = dt.Rows[0]["ApprovalSignPath"].ToString();
                        sign2 = dt.Rows[1]["ApprovalSignPath"].ToString();

                        if (!string.IsNullOrEmpty(sign1)) sign1 = "<img src=\"" + sign1 + "\"  height=\"25\" />";
                        if (!string.IsNullOrEmpty(sign2)) sign2 = "<img src=\"" + sign2 + "\"  height=\"25\" />";

                        //村
                        b.Append("<div class=\"tr_05 clearfix\">");
                        b.Append("<div class=\"tr_l\">");
                        b.Append("<div class=\"td_01\">" + dt.Rows[0]["Comments"].ToString() + "</div>");
                        //兼容库伦系统
                        if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">经办人：" + dt.Rows[0]["Approval"].ToString() + "<br />联系电话：" + dt.Rows[0]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "1") + "</div>"); }
                        else { b.Append("<div class=\"td_02\">经办人：" + sign1 + "<br />联系电话：" + dt.Rows[0]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "1") + "</div>"); }
                        //
                        
                        b.Append("<div class=\"official\">" + seal1 + "</div>");
                        b.Append("</div>");
                        //镇
                        b.Append(" <div class=\"tr_c\">");
                        b.Append("<div class=\"td_01\">" + dt.Rows[1]["Comments"].ToString() + "</div>");
                        //兼容库伦系统
                        if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">经办人：" + dt.Rows[1]["Approval"].ToString() + "<br />联系电话：" + dt.Rows[1]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "1") + "</div>"); }
                        else { b.Append("<div class=\"td_02\">经办人：" + sign2 + "<br />联系电话：" + dt.Rows[1]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "1") + "</div>"); }
                        //
                        
                        b.Append("<div class=\"official\">" + seal2 + "</div>");
                        b.Append("</div>");

                        b.Append("<div class=\"tr_r none\">");
                        b.Append("<div class=\"td_01\"></div>");
                        b.Append("<div class=\"td_02\"></div>");
                        b.Append(" <div class=\"official\"></div>");
                        b.Append("</div>");

                        b.Append("</div>");
                    }
                    else
                    {
                        b.Append("<div class=\"tr_04\"><br/>流程节点参数预制错误！<br/><br/></div>");
                    }
                }

                dt = null;

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
            catch
            {
                b.Append("<div class=\"tr_04\"><br/>获取流程节点审核信息时发生错误！<br/><br/></div>");
            }
            return b.ToString();
        }
        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
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
