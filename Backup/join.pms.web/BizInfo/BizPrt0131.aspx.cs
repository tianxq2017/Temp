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
    public partial class BizPrt0131 : System.Web.UI.Page
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
                    if (m_ActionName == "print" && PageValidate.IsNumber(m_ObjID))
                    {
                        object m_QcfwzBm = DbHelperSQL.GetSingle("SELECT QcfwzBm FROM BIZ_Contents WHERE  Attribs IN(2,8,9) and BizID=" + m_ObjID + " ");

                        if (m_QcfwzBm != null)
                        {
                            Response.Redirect("/BizInfo/BizPrt01502.aspx?action=view21&mp=1&fwzh=" + m_QcfwzBm.ToString() + "&sourceUrl=" + m_SourceUrl);
                        }
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
                    if (!string.IsNullOrEmpty(photos)) photos = "<img src=\"" + photos + "\" width=\"120\" heigth=\"170\"  />";

                    qrInfo = "婚育情况证明；申请人及配偶姓名、身份证号：" + biz.Fileds01 + "(" + biz.PersonCidA + ")、" + biz.Fileds08 + "(" + biz.PersonCidB + ")。";
                    workFlowInfo = GetWorkFlows(bizID, biz.CurAreaNameA, biz.Attribs, ref cerNo, biz.BizStep,ref qrFiles, qrInfo);
                    //workFlowInfo = GetWorkFlows(bizID, biz.CurAreaNameA, biz.Attribs, ref cerNo,biz.BizStep);

                    //本人信息 
                    s.Append("<div class=\"number\"><span>编号：" + cerNo + "</span>" + biz.RegAreaNameA.Replace("陕西省", "").Replace("安康市", "").Replace("&", " ") + "</div> ");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<!--<div class=\"code\"><img src=\"images/ls/code.gif\" /></div> -->");
                    s.Append("<div class=\"photo\">" + photos + "</div>");
                    s.Append("<div class=\"tr_01 tr_01a clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds01 + "</div>");
                    //从身份证号码中提取出生日期
                    s.Append("<div class=\"td_02\">" + CommBiz.GetBirthdayByID(biz.PersonCidA) + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    //从身份证号中获取性别
                    s.Append("<div class=\"td_01\">" + CommBiz.GetSexByID(biz.PersonCidA) + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.Fileds03 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02\">");
                    s.Append("<div class=\"td_01\">" + biz.PersonCidA + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds05 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02\">");
                    s.Append("<div class=\"td_01\">" + biz.CurAreaNameA.Replace("陕西省", "").Replace("安康市", "").Replace("&", " ") + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds08 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03\">");
                    s.Append("<div class=\"td_01\">直系、三代内旁系血亲关系：<U>" + (Convert.ToInt16(biz.Fileds23) + 1) + "</U>　　　　　　　　　　　　　　　　①无&nbsp;&nbsp;②有</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_04\">");
                    s.Append("<div class=\"td_01\">婚前医学检查结果：" + biz.Fileds25 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"seal_01\">");
                    s.Append("<div class=\"td_01\">医学意见：<U>" + biz.Fileds24 + "</U> <BR><div style=\"font-size:12px;\">&nbsp;&nbsp;①建议不宜结婚&nbsp;&nbsp;②建议不宜生育&nbsp;&nbsp;③建议暂缓结婚<BR>&nbsp;&nbsp;④未发现医学上不宜结婚的情形&nbsp;&nbsp;&nbsp;&nbsp;⑤建议采取医学措施，尊重受检者意愿</div></div>");
                    //审核流程
                    s.Append(workFlowInfo);
                    s.Append("</div> ");
                    s.Append("</div>");
                    s.Append("</div>");
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
        private string GetWorkFlows(string bizID, string curAreaName, string attribs, ref string cerNo, string bizStep, ref string qrFiles, string qrInfo)
        {
            string OprateDate1 = string.Empty;
            string seal1 = "", seal2 = "", seal3 = string.Empty;
            string sign1 = "", sign2 = "", sign3 = "", sign33 = string.Empty;
            string tel1 = "", tel2 = "", tel3 =string.Empty;
            string validDateEnd = string.Empty;
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

                    b.Append("<div class=\"td_02\">检查单位专用章<br />主检医师签字：" + sign1 + "&nbsp;" + OprateDate1 + "<br />电话：" + tel1 + "</div>");
                    b.Append("<div class=\"official\">" + seal1 + "</div>");
                }
                else
                {
                    b.Append("<div class=\"td_01\">&nbsp;</div>");
                    b.Append("<div class=\"official\">&nbsp;&nbsp;</div>");
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
