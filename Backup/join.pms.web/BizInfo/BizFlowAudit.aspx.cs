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
    public partial class BizFlowAudit : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_FuncName;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号

        private string m_SqlParams;
        public string m_TargetUrl;

        private string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                SetOpratetionAction("");

            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "";// DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                //if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";
                cssFile = "/css/inidex.css";
                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        #region 身份验证与参数过滤
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
            string sss = Request.Url.ToString();
            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                m_FuncName = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncNa");
                if (!string.IsNullOrEmpty(m_FuncName)) m_FuncName = Server.UrlDecode(m_FuncName);
                // FuncNa
                this.LiteralValidDate.Text = BizAuditInfo.GetValidDate(m_FuncCode);
                // //BizInfo/BizCorrects.apsx?sourceUrl="+m_SourceUrl+"&BizID="+m_ObjID+"&FlowID=
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
                case "audit":
                    funcName = "业务审核";
                    ShowModInfo(m_ObjID);
                    break;
                case "printok": // 打印确认
                    funcName = "打印确认";
                    PrintOK(m_ObjID);
                    break;
                case "Guidang": // 归档
                    funcName = "归档";
                    GuiDang(m_ObjID);
                    break;
                case "YiAudit": // 归档
                    funcName = "删除";
                    Audit(m_ObjID);
                    break;
                case "view": // 查看
                    funcName = "查看内容";
                    //ShowModInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true, true);
                    break;
            }
            this.LiteralNav.Text = "起始页  &gt;&gt; " + CommPage.GetAllBizName(m_FuncCode) + " &gt;" + funcName + "：";
        }

        // txtCertificateNoA

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="objID"></param>
        private void PrintOK(string objID)
        {
            try
            {
                // Attribs: 0初始提交; 1审核中; 2,通过 3驳回; 4删除;8已打证; 9,归档
                string attribs = string.Empty;
                if (join.pms.dal.CommPage.CheckAttribs("BIZ_Contents", "Attribs", "BizID IN(" + objID + ")", "", ref attribs))
                {
                    if (attribs == "2")
                    {
                        m_SqlParams = "UPDATE BIZ_Contents SET Attribs='8' WHERE BizID IN(" + objID + ") ";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        CommPage.SetBizLog(objID, m_UserID, "打印确认", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 进行了打印确认操作");

                        MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的事项进行了打印确认操作！", m_TargetUrl, true, true);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息包括未处理结束的事项,只有审批通过的事宜才能执行“打印确认”操作！", m_TargetUrl, true, true);
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息包含尚不是同一类信息！", m_TargetUrl, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true, true);
            }
        }
        /// <summary>
        /// 归档
        /// </summary>
        /// <param name="objID"></param>
        private void GuiDang(string objID)
        {
            try
            {
                // Attribs: 0,初始提交;1,审批中 2,通过 3,补正 4,撤销 5,注销, 6,等待审批,8,已出证,9,归档
                string attribs = string.Empty;
                if (join.pms.dal.CommPage.CheckAttribs("BIZ_Contents", "Attribs", "BizID IN(" + objID + ")", "", ref attribs))
                {
                    if (attribs == "8")
                    {
                        m_SqlParams = "UPDATE BIZ_Contents SET Attribs='9' WHERE BizID IN(" + objID + ") ";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        CommPage.SetBizLog(objID, m_UserID, "业务归档", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 进行了业务归档操作");

                        MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的事项记录归档操作成功！", m_TargetUrl, true, true);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息包括未确认的事项,只有审批通过且经过“打印确认”的业务才能归档！", m_TargetUrl, true, true);
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息包含尚不是同一类信息！", m_TargetUrl, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true, true);
            }
        }

        /// <summary>
        /// 一孩审核 
        /// </summary>
        /// <param name="objID"></param>
        private void Audit(string objID)
        {
            try
            {
                // Attribs: 0,初始提交;1,审核中 2,通过 3,补正 4,撤销 5,注销, 6,等待审核,9,归档
                string attribs = string.Empty;
                if (join.pms.dal.CommPage.CheckAttribs("BIZ_Contents", "Attribs", "BizID IN(" + objID + ")", "", ref attribs))
                {
                    if (attribs == "0")
                    {
                        m_SqlParams = "UPDATE BIZ_Contents SET Attribs=2,FinalDate=GetDate() WHERE BizID IN(" + objID + ") ";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的事项记录审核操作成功！", m_TargetUrl, true, true);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息包括已审核和归档的事项,只有初始提交的业务才能审核！", m_TargetUrl, true, true);
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息包含尚不是同一类信息！", m_TargetUrl, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true, true);
            }
        }


        // this.txtCertificateNoA.Value=BizWorkFlows.GetCertificateNo();
        /// <summary>
        /// 显示待处理信息
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string bizID)
        {
            string userAreaCode = "", useSealPath = "", useSealPass = "", userNames = "", userAcc = "", userDeptName = string.Empty;
            string sealUserName = string.Empty;
            string appUserID = "", userRoleID = string.Empty;
            string bizStepTotal = "", bizStepCur = string.Empty;
            string approvalUserID = string.Empty;
            string nextID = string.Empty;
            string attribs = string.Empty;
            StringBuilder s = new StringBuilder();
            try
            {
                this.PanelApproval.Visible = true;
                attribs = CommPage.GetSingleVal("SELECT Attribs FROM BIZ_Contents WHERE BizID=" + bizID);
                if (attribs == "1" || attribs == "6")
                {
                    // 获取业务电子证照信息
                    BIZ_Docs doc = new BIZ_Docs();
                    this.LiteralDocs.Text = doc.GetBizDocs(bizID);
                    doc = null;
                    // 1,调取当前审核用户信息
                    SysSeal seal = new SysSeal();
                    seal.SealUserID = m_UserID;
                    seal.GetSealByUser();
                    /*
                    1	系统管理员
                    2	业务管理-旗县
                    3	业务处理-旗县
                    4	业务处理-镇办
                    5	业务处理-社区/村
                    6	业务处理-医院
                    */
                    userRoleID = CommPage.GetSingleVal("SELECT TOP 1 RoleID FROM SYS_UserRoles WHERE UserID=" + m_UserID);
                    this.txtUserRoleID.Value = userRoleID;
                    //if (!string.IsNullOrEmpty(seal.SealID) || userRoleID=="6")
                    if (!string.IsNullOrEmpty(seal.SealID))
                    {
                        userAreaCode = seal.UserAreaCode;
                        useSealPath = seal.SealPath;
                        useSealPass = seal.SealPass;
                        userNames = seal.SealName;
                        userAcc = seal.UserAccount;
                        userDeptName = seal.SealName;
                        sealUserName = seal.UserName;

                        SetApprovalList(userAreaCode);
                        SetAuditUserList(userAreaCode);

                        this.txtUserAcc.Value = userAcc;
                        this.txtUserAreaCode.Value = userAreaCode;
                        //this.txtApprovalUserTel.Text = seal.UserTel;//已改为签名表中的电话
                        this.txtApproval.Text = seal.UserName;// 经办人

                        seal = null;
                        // 2,分析是否具有审核权限
                        if (!string.IsNullOrEmpty(userAreaCode))
                        {
                            join.pms.dal.BizWorkFlows bwf = new join.pms.dal.BizWorkFlows();
                            bwf.BizID = bizID;
                            bwf.GetWorkFlowsByBizID();
                            if (!string.IsNullOrEmpty(bwf.WorkFlowsID))
                            {
                                bizStepTotal = bwf.BizStepTotal;
                                bizStepCur = bwf.BizStep;
                                approvalUserID = bwf.ApprovalUserID;
                                /*
                                    0101 一孩 镇办3
                                    0102 二孩 镇办3
                                    0107 一杯奶 镇办2
                                    0111 终止妊娠 旗6/旗3
                                     */
                                // 需要责任人出现在审核节点的
                                this.PanelAllowDate.Visible = false;
                                if (m_FuncCode == "0111")
                                {
                                    if (bizStepCur == "3" || bizStepCur == "6")
                                    {
                                        if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                                        {
                                            if (!string.IsNullOrEmpty(approvalUserID))
                                            {
                                                this.PanelAudit.Visible = true;
                                                this.PanelApproval.Visible = false;
                                                this.PanelAllowDate.Visible = false;
                                                this.PanelSeal.Visible = true;
                                                this.txtComments.Text = bwf.Comments;
                                                this.txtApproval.Text = bwf.Approval;
                                                this.txtAuditUser.Text = sealUserName;
                                            }
                                            else
                                            {
                                                MessageBox.ShowAndRedirect(this.Page, "操作提示：请经办人审核后，责任人再审核！", m_TargetUrl, true, true);
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            this.PanelAudit.Visible = false;
                                            this.PanelApproval.Visible = true;
                                            this.PanelAllowDate.Visible = false;
                                            this.PanelSeal.Visible = false;
                                            //if (!string.IsNullOrEmpty(bwf.Approval) && !string.IsNullOrEmpty(bwf.Comments))
                                            //{
                                            //    MessageBox.ShowAndRedirect(this.Page, "操作提示：该流程节点需要您处理的内容已经审核，无需重复操作！", m_TargetUrl, true, true);
                                            //    return;
                                            //}
                                        }
                                    }
                                }
                                if (m_FuncCode == "0107")
                                {
                                    if (bizStepCur == "2")
                                    {
                                        if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                                        {
                                            if (!string.IsNullOrEmpty(approvalUserID))
                                            {
                                                this.PanelAudit.Visible = true;
                                                this.PanelApproval.Visible = false;
                                                this.PanelSeal.Visible = true;
                                                this.txtComments.Text = bwf.Comments;
                                                this.txtApproval.Text = bwf.Approval;
                                                this.txtAuditUser.Text = sealUserName;
                                            }
                                            else
                                            {
                                                MessageBox.ShowAndRedirect(this.Page, "操作提示：请经办人审核后，责任人再审核！", m_TargetUrl, true, true);
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            this.PanelAudit.Visible = false;
                                            this.PanelApproval.Visible = true;
                                            this.PanelSeal.Visible = false;
                                            //if (!string.IsNullOrEmpty(bwf.Approval) && !string.IsNullOrEmpty(bwf.Comments))
                                            //{
                                            //    MessageBox.ShowAndRedirect(this.Page, "操作提示：该流程节点需要您处理的内容已经审核，无需重复操作！", m_TargetUrl, true, true);
                                            //    return;
                                            //}
                                        }
                                    }
                                }
                                /*
0103 奖扶 旗镇两级2,3
0104 特扶 旗镇两级2,3
0105 少生快富 旗镇两级 2,3
0106 结扎奖励  旗镇两级 3,4
0108 独子证 镇级 3
            */
                                if (m_FuncCode == "0101" || m_FuncCode == "0102" || m_FuncCode == "0106" || m_FuncCode == "0108" || m_FuncCode == "0122")
                                {
                                    if (bizStepCur == "3" || bizStepCur == "4")
                                    {
                                        if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                                        {
                                            if (!string.IsNullOrEmpty(approvalUserID))
                                            {
                                                this.PanelAudit.Visible = true;
                                                this.PanelApproval.Visible = false;
                                                this.PanelSeal.Visible = true;
                                                this.txtComments.Text = bwf.Comments;
                                                this.txtApproval.Text = bwf.Approval;
                                                this.txtAuditUser.Text = sealUserName;
                                            }
                                            else
                                            {
                                                MessageBox.ShowAndRedirect(this.Page, "操作提示：请经办人审核后，责任人再审核！", m_TargetUrl, true, true);
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            this.PanelAllowDate.Visible = false;
                                            this.PanelAudit.Visible = false;
                                            this.PanelApproval.Visible = true;
                                            this.PanelSeal.Visible = false;
                                            //只要流程节点属性未变更,即可重新审核
                                            if (!string.IsNullOrEmpty(bwf.Approval) && !string.IsNullOrEmpty(bwf.Comments))
                                            {
                                                MessageBox.ShowAndRedirect(this.Page, "操作提示：该流程节点需要您处理的内容已经审核，无需重复操作！", m_TargetUrl, true, true);
                                                return;
                                            }
                                        }

                                        if (m_FuncCode == "0102" && bizStepCur == "4") this.PanelAllowDate.Visible = true; // 旗级显示
                                    }

                                }
                                if (m_FuncCode == "0103" || m_FuncCode == "0104" || m_FuncCode == "0105" || m_FuncCode == "0109" || m_FuncCode == "0110")
                                {
                                    if (bizStepCur == "2" || bizStepCur == "3")
                                    {
                                        if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                                        {
                                            if (!string.IsNullOrEmpty(approvalUserID))
                                            {
                                                this.PanelAudit.Visible = true;
                                                this.PanelApproval.Visible = false;
                                                this.PanelSeal.Visible = true;
                                                this.txtComments.Text = bwf.Comments;
                                                this.txtApproval.Text = bwf.Approval;
                                                this.txtAuditUser.Text = sealUserName;
                                            }
                                            else
                                            {
                                                MessageBox.ShowAndRedirect(this.Page, "操作提示：请经办人审核后，责任人再审核！", m_TargetUrl, true, true);
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            this.PanelAudit.Visible = false;
                                            this.PanelApproval.Visible = true;
                                            this.PanelSeal.Visible = false;
                                            //if (!string.IsNullOrEmpty(bwf.Approval) && !string.IsNullOrEmpty(bwf.Comments))
                                            //{
                                            //    MessageBox.ShowAndRedirect(this.Page, "操作提示：该流程节点需要您处理的内容已经审核，无需重复操作！", m_TargetUrl, true, true);
                                            //    return;
                                            //}
                                        }
                                    }

                                }


                                //attribs = 
                                if (IsMyFlow(bizID, bizStepCur, userAreaCode, bwf.AreaCode, userRoleID) || IsMyhospital(bizID, bizStepCur, useSealPath, bizStepTotal, userRoleID))
                                {
                                    this.txtSealPath.Value = useSealPath;
                                    this.txtSealPass.Value = useSealPass;
                                    // 智能匹配审核意见
                                    BizAuditInfo bizAI = new BizAuditInfo();
                                    bizAI.BizCode = m_FuncCode;
                                    bizAI.BizStep = bizStepCur;
                                    this.LiteralComments.Text = bizAI.GetAuditInfo(bizStepTotal, bizID);
                                    bizAI = null;

                                    #region 根据业务编号处理审核节点
                                    /*
                                    0101	一孩生育登记 3 
                                    0102	二孩生育登记 4
                                    0103	奖励扶助 3
                                    0108	独生子女父母光荣证 3
                                    0109	《流动人口婚育证明》 3
                                     */
                                    // 合并审核 村级男女双方合并审核 (0101,0102,0106,0107,0108,0109)
                                    if (m_FuncCode == "0101" || m_FuncCode == "0102" || m_FuncCode == "0106" || m_FuncCode == "0107" || m_FuncCode == "0108" || m_FuncCode == "0109" || m_FuncCode == "0122")
                                    {
                                        if (bizStepCur == "1")
                                        {
                                            nextID = CommPage.GetSingleVal("SELECT CommID FROM BIZ_WorkFlows WHERE BizID=" + bizID + " AND AreaCode='" + userAreaCode + "' AND BizStep=2  AND Attribs=9 ORDER BY BizStep");
                                            if (!string.IsNullOrEmpty(nextID)) { this.txtBizFlowID.Value = bwf.WorkFlowsID + "," + nextID; }
                                            else { this.txtBizFlowID.Value = bwf.WorkFlowsID; }
                                        }
                                        else { this.txtBizFlowID.Value = bwf.WorkFlowsID; }
                                    }
                                    else { this.txtBizFlowID.Value = bwf.WorkFlowsID; }

                                    // 证件编号
                                    if (m_FuncCode == "0101" || m_FuncCode == "0102" || m_FuncCode == "0103" || m_FuncCode == "0108" || m_FuncCode == "0109" || m_FuncCode == "0122" || m_FuncCode == "0131" || m_FuncCode == "0150")
                                    {
                                        if (bizStepTotal == bizStepCur)
                                        {
                                            //this.txtCertificateNoA.Value = bwf.GetCertificateNo(m_FuncCode, userAreaCode, "");

                                            this.txtCertificateNoA.Value = "OK";
                                            this.PanelFaZheng.Visible = true;
                                        }
                                        else { this.PanelFaZheng.Visible = false; }
                                    }
                                    if (m_FuncCode == "0111" && bizStepTotal == "6")
                                    {
                                        if (bizStepCur == "3" || bizStepCur == "4" || bizStepCur == "5")
                                        {
                                            this.PanelSeal.Visible = false;
                                        }
                                    }

                                    #endregion

                                    appUserID = bwf.ApprovalUserID;

                                    this.txtBizStep.Value = bizStepCur;
                                    this.txtBizStepTotal.Value = bizStepTotal;
                                    this.LiteralBizAppDate.Text = bwf.CreateDate;
                                    this.txtDeptName.Text = bwf.AreaName;
                                    //this.txtApproval.Text = userNames;
                                    this.txtBizStepNames.Value = bwf.AreaName + "审核";
                                    this.txtOprateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                                    this.txtCertificateDateStart.Value = DateTime.Now.ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您不具有改流程节点的审核权限或者该流程节点已经审核，请点击“工作流程”按钮查阅流程详情！", m_TargetUrl, true, true);
                                }
                            }
                            else
                            {
                                MessageBox.ShowAndRedirect(this.Page, "操作提示：该流程节点已经审核完毕或者您不具有该流程节点的审核权限！", m_TargetUrl, true, true);
                            }
                            bwf = null;
                        }
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：您尚未分配签章权限,请联系管理员授权！", m_TargetUrl, true, true);
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：该流程节点尚未准备就绪，请修改上传电子证照，然后继续审核；查阅流程详情，请点击“流程”按钮查阅流程详情！", m_TargetUrl, true, true);
                }
            }
            catch
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：审核签章时出现错误！", m_TargetUrl, true, true);
            }
        }

        /// <summary>
        /// 判断是否当前可处理的流程
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="myStep"></param>
        /// <returns></returns>
        private bool IsMyFlow(string bizID, string myStep, string userArea, string curArea, string roleID)
        {
            // 
            bool returnVa = false;
            if (userArea == curArea && roleID != "6")
            {
                if (!string.IsNullOrEmpty(myStep))
                {
                    if (PageValidate.IsNumber(myStep))
                    {
                        int lastStep = int.Parse(myStep) - 1;//判断上一步是否处理
                        if (lastStep > 0)
                        {
                            m_SqlParams = "SELECT Attribs FROM BIZ_WorkFlows WHERE BizID=" + bizID + " AND BizStep=" + lastStep.ToString();
                            string objVal = CommPage.GetSingleVal(m_SqlParams);
                            if (objVal != "9") returnVa = true;
                        }
                        else { returnVa = true; }
                    }
                }
            }

            return returnVa;
        }

        /// <summary>
        /// 判断是否当前医院
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="myStep"></param>
        /// <param name="myHospital"></param>
        /// <param name="curArea"></param>
        /// <returns></returns>
        private bool IsMyhospital(string bizID, string myStep, string myHospital, string stepTotal, string roleID)
        {
            // 
            bool returnVa = false;
            if (stepTotal == "6" && roleID == "6")
            {
                if (myStep == "3" && myHospital == "mz")
                {
                    m_SqlParams = "SELECT Attribs FROM BIZ_WorkFlows WHERE BizID=" + bizID + " AND BizStep=2";
                    string objVal = CommPage.GetSingleVal(m_SqlParams);
                    if (objVal != "9") returnVa = true;
                }
                else if (myStep == "4" && myHospital == "ek")
                {
                    m_SqlParams = "SELECT Attribs FROM BIZ_WorkFlows WHERE BizID=" + bizID + " AND BizStep=3";
                    string objVal = CommPage.GetSingleVal(m_SqlParams);
                    if (objVal != "9") returnVa = true;
                }
                else if (myStep == "5" && myHospital == "fck")
                {
                    m_SqlParams = "SELECT Attribs FROM BIZ_WorkFlows WHERE BizID=" + bizID + " AND BizStep=4";
                    string objVal = CommPage.GetSingleVal(m_SqlParams);
                    if (objVal != "9") returnVa = true;
                }
                else { }
            }

            return returnVa;
        }

        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // SELECT BizID,BizStep,AreaCode,AreaName,Comments,Approval,Signature,CreateDate,OprateDate FROM BIZ_WorkFlows  txtApprovalUserTel
            string strErr = string.Empty;
            string updateID = this.txtBizFlowID.Value;
            string BizStepTotal = this.txtBizStepTotal.Value;
            string curStep = this.txtBizStep.Value;
            string curStepNa = this.txtBizStepNames.Value;
            string SealPath = this.txtSealPath.Value;
            string SealPass = this.txtSealPass.Value;
            string userRoleID = this.txtUserRoleID.Value;
            string userAcc = this.txtUserAcc.Value;
            string userAreaCode = this.txtUserAreaCode.Value;
            string pFlag = "0";//0,正常普通；1,双重审核中，非责任人方，即普通经办人；2,责任人
            string appDirection = CommPage.GetSingleVal("SELECT CAST(InitDirection As varchar)+'@'+Fileds01+' '+Fileds08 FROM BIZ_Contents WHERE BizID=" + m_ObjID); // InitDirection:0,前向;1,后向

            string Attribs = this.DDLPass.SelectedValue;
            string DeptName = SQLFilter.GetFilterSQL(this.txtDeptName.Text);
            string Comments = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtComments.Text));
            string Approval = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtApproval.Text));

            string txtApprovalPass = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtApprovalPass.Text));

            string ApprovalUserTel = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtApprovalUserTel.Text));
            string AuditUser = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtAuditUser.Text));

            string txtAuditUserPass = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtAuditUserPass.Text));

            string Signature = SealPath;
            string OprateDate = PageValidate.GetTrim(this.txtOprateDate.Value);
            string SealNewPwd = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtSealPwd.Text));
            // txtAuditUser
            string CertificateNoA = PageValidate.GetTrim(this.txtCertificateNoA.Value);
            string childNum = "";
            if (m_FuncCode == "0150")
            {
                string PersonCidA = "", PersonCidB = "";
                string sqlApp = "SELECT [PersonCidA], [PersonCidB] FROM [BIZ_Contents] WHERE WHERE BizID=" + m_ObjID;
                DataTable dtApp = new DataTable();
                try
                {
                    dtApp = DbHelperSQL.Query(sqlApp).Tables[0];
                    if (dtApp.Rows.Count == 1)
                    {
                        PersonCidA = dtApp.Rows[0]["PersonCidA"].ToString();
                        PersonCidB = dtApp.Rows[0]["PersonCidB"].ToString();
                        dtApp = null;
                    }
                }
                catch (Exception ex)
                {
                }
                dtApp = null;

                string QcfwzBm_where = "";
                if (!string.IsNullOrEmpty(PersonCidA))
                {
                    QcfwzBm_where = " and PersonCidA='" + PersonCidA + "' ";
                }
                if (!string.IsNullOrEmpty(PersonCidB))
                {
                    QcfwzBm_where = QcfwzBm_where + " and PersonCidB='" + PersonCidB + "' ";
                }
                childNum = CommPage.GetSingleVal("SELECT COUNT(*)+1 FROM BIZ_Contents  WHERE BizCode='0150' and QcfwzBm!='' and  Attribs IN(2,8,9) " + QcfwzBm_where);
            }      
            if (CertificateNoA == "OK")
            {
                join.pms.dal.BizWorkFlows bwf = new join.pms.dal.BizWorkFlows();
                CertificateNoA = bwf.GetCertificateNo(m_FuncCode, userAreaCode, childNum);
            }
            else { CertificateNoA = ""; }
            //string CertificateMemo = PageValidate.GetTrim(this.txtCertificateMemo.Text);
            // txtCertificateDateStart txtCertificateDateEnd
            string CertificateDateStart = PageValidate.GetTrim(this.txtCertificateDateStart.Value);

            string CertificateDateEnd = PageValidate.GetTrim(DateTime.Parse(CertificateDateStart).AddYears(1).ToString("yyyy/MM/dd"));
            //string CertificateDateEnd = PageValidate.GetTrim(this.txtCertificateDateEnd.Value);

            // 经办人相关信息取得
            string ApprovalPath = string.Empty;
            string ApprovalPass = string.Empty;
            string ApprovalSignName = string.Empty;
            string ApprovalID = PageValidate.GetTrim(this.DDLApproval.SelectedValue);
            if (ApprovalID.IndexOf(",") > 0)
            {
                ApprovalID = ApprovalID.Substring(0, ApprovalID.IndexOf(",") + 1).Replace(",", "");
            }
            if (!string.IsNullOrEmpty(ApprovalID))
            {
                string sqlApp = "SELECT [SignID], [SignName], [SignPath],[SignPass] FROM [SYS_Sign] WHERE SignID=" + ApprovalID;
                DataTable dtApp = new DataTable();
                try
                {
                    dtApp = DbHelperSQL.Query(sqlApp).Tables[0];
                    if (dtApp.Rows.Count == 1)
                    {
                        ApprovalPath = dtApp.Rows[0]["SignPath"].ToString();
                        ApprovalPass = dtApp.Rows[0]["SignPass"].ToString();
                        ApprovalSignName = dtApp.Rows[0]["SignName"].ToString();
                        dtApp = null;
                    }
                }
                catch (Exception ex)
                {
                }
                dtApp = null;
            }
            // 责任人相关信息取得
            string AuditUserPath = string.Empty;
            string AuditUserPass = string.Empty;
            string AuditUserSignName = string.Empty;
            string AuditUserID = PageValidate.GetTrim(this.DDLAuditUser.SelectedValue);
            if (AuditUserID.IndexOf(",") > 0)
            {
                AuditUserID = AuditUserID.Substring(0, AuditUserID.IndexOf(",") + 1).Replace(",", "");
            }
            if (!string.IsNullOrEmpty(AuditUserID))
            {
                string sqlAudit = "SELECT [SignID], [SignName], [SignPath],[SignPass] FROM [SYS_Sign] WHERE SignID=" + AuditUserID;
                DataTable dtAudit = new DataTable();
                try
                {
                    dtAudit = DbHelperSQL.Query(sqlAudit).Tables[0];
                    if (dtAudit.Rows.Count == 1)
                    {
                        AuditUserPath = dtAudit.Rows[0]["SignPath"].ToString();
                        AuditUserPass = dtAudit.Rows[0]["SignPass"].ToString();
                        AuditUserSignName = dtAudit.Rows[0]["SignName"].ToString();
                        dtAudit = null;
                    }
                }
                catch (Exception ex)
                {
                }
                dtAudit = null;
            }

            // 通知申请人补正
            if (String.IsNullOrEmpty(BizStepTotal)) strErr += "参数错误！\\n";
            if (String.IsNullOrEmpty(curStep)) strErr += "流程节点参数错误！\\n";
            if (String.IsNullOrEmpty(updateID)) strErr += "审核节点参数丢失！\\n";
            if (String.IsNullOrEmpty(Comments) || Comments == "请选择引用条款……" || Comments == "请选择引用条款……") { strErr += "请输入审核意见！\\n"; }
            else { }

            /*生育期限*/
            string txtStartDate = CommBiz.GetTrim(this.txtStartDate.Value);
            string txtEndDate = CommBiz.GetTrim(this.txtEndDate.Value);
            //if (m_FuncCode == "0102") CertificateDateStart = txtStartDate;
            //经办人、责任人签章密码
            if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
            {
                if (String.IsNullOrEmpty(txtAuditUserPass))
                {
                    strErr += "请输入责任人签名密码！\\n";
                }
                else
                {
                    txtAuditUserPass = DESEncrypt.Encrypt(txtAuditUserPass);
                    if (txtAuditUserPass != AuditUserPass)
                    {
                        strErr += "责任人签名密码输入错误！\\n";
                    }
                }
            }
            else
            {
                if (String.IsNullOrEmpty(Approval)) strErr += "请输入经办人姓名！\\n";

                if (String.IsNullOrEmpty(txtApprovalPass)) strErr += "请输入经办人签名密码！\\n";

                if (String.IsNullOrEmpty(ApprovalUserTel)) strErr += "请输入经办人联系电话！\\n";

                //if (String.IsNullOrEmpty(Approval)) strErr += "审核人不能为空！\\n";

                txtApprovalPass = DESEncrypt.Encrypt(txtApprovalPass);
                if (txtApprovalPass != ApprovalPass)
                {
                    strErr += "经办人签字密码输入错误！\\n";
                }
            }

            // =====责任人验证 start=======
            if (m_FuncCode == "0111")
            {
                if (BizStepTotal == "3" && curStep == "3")
                {
                    if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                    {
                        if (String.IsNullOrEmpty(AuditUser)) strErr += "请输入责任人！\\n";

                        if (String.IsNullOrEmpty(SealNewPwd)) strErr += "请输入签章密码！\\n";
                        pFlag = "2";
                    }
                    else { pFlag = "1"; }
                }
                else if (BizStepTotal == "6")
                {
                    if (curStep == "6")
                    {
                        if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                        {
                            if (String.IsNullOrEmpty(AuditUser)) strErr += "请输入责任人！\\n";
                            if (String.IsNullOrEmpty(SealNewPwd)) strErr += "请输入签章密码！\\n";
                            pFlag = "2";
                        }
                        else { pFlag = "1"; }
                    }
                    else if (curStep == "3" || curStep == "4" || curStep == "5")
                    {
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(SealNewPwd)) strErr += "请输入签章密码！\\n";
                    }

                }
                else { if (String.IsNullOrEmpty(SealNewPwd)) strErr += "请输入签章密码！\\n"; }
            }
            else if (m_FuncCode == "0107")
            {
                if (curStep == "2")
                {
                    if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                    {
                        if (String.IsNullOrEmpty(AuditUser)) strErr += "请输入责任人！\\n";
                        if (String.IsNullOrEmpty(SealNewPwd)) strErr += "请输入签章密码！\\n";
                        pFlag = "2";
                    }
                    else { pFlag = "1"; }
                }
                else { if (String.IsNullOrEmpty(SealNewPwd)) strErr += "请输入签章密码！\\n"; }
            }
            else if (m_FuncCode == "0103" || m_FuncCode == "0104" || m_FuncCode == "0105" || m_FuncCode == "0109" || m_FuncCode == "0110")
            {
                if (curStep == "2" || curStep == "3")
                {
                    if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                    {
                        if (String.IsNullOrEmpty(AuditUser)) strErr += "请输入责任人！\\n";
                        if (String.IsNullOrEmpty(SealNewPwd)) strErr += "请输入签章密码！\\n";
                        pFlag = "2";
                    }
                    else { pFlag = "1"; }
                }
                else { if (String.IsNullOrEmpty(SealNewPwd)) strErr += "请输入签章密码！\\n"; }
            }
            else if (m_FuncCode == "0101" || m_FuncCode == "0102" || m_FuncCode == "0106" || m_FuncCode == "0108" || m_FuncCode == "0122")
            {
                if (curStep == "3" || curStep == "4")
                {
                    if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                    {
                        if (String.IsNullOrEmpty(AuditUser)) strErr += "请输入责任人！\\n";
                        if (String.IsNullOrEmpty(SealNewPwd)) strErr += "请输入签章密码！\\n";
                        pFlag = "2";
                    }
                    else { pFlag = "1"; }
                }
                else { if (String.IsNullOrEmpty(SealNewPwd)) strErr += "请输入签章密码！\\n"; }
            }
            else { if (String.IsNullOrEmpty(SealNewPwd)) strErr += "请输入签章密码！\\n"; }
            // =====责任人验证 end=======

            if (string.IsNullOrEmpty(OprateDate)) { OprateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }
            else { OprateDate = DateTime.Parse(OprateDate).ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss"); }
            if (Attribs == "4")
            {
                strErr += "请选择本流程节点处理结论；是否能够通过！\\n";
            }
            else if (Attribs == "0")
            {
                //驳回
                if (Comments.IndexOf("，同意上报。") > 0) strErr += "选择“同意上报”的流程需要审核通过！\\n";
                // ，同意上报。
            }
            else
            {
                if (Comments.IndexOf("补正") > 0) strErr += "需要补正时；请在审核结论中选择不予通过！\\n";
            }
            // 5个打印发证的业务
            if (this.m_FuncCode == "0101" || this.m_FuncCode == "0102" || this.m_FuncCode == "0103" || this.m_FuncCode == "0108" || this.m_FuncCode == "0109" || this.m_FuncCode == "0122")
            {
                if (BizStepTotal == curStep)
                {
                    if (Attribs == "1")
                    {
                        if (String.IsNullOrEmpty(CertificateNoA)) strErr += "请输入证件编号！\\n";
                        //if (String.IsNullOrEmpty(CertificateMemo)) strErr += "请确定发证时间！\\n";
                    }
                }
            }



            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            //  SealPass = DESEncrypt.Encrypt(SealPass);
            try
            {

                //再次检测证号是否重复
                if (!string.IsNullOrEmpty(CertificateNoA))
                {

                    join.pms.dal.BizWorkFlows bwf2 = new join.pms.dal.BizWorkFlows();
                    CertificateNoA = bwf2.GetCertificateNos(CertificateNoA, 0);
                }

                /*
0103 奖扶 旗镇两级2,3
0104 特扶 旗镇两级2,3
0105 少生快富 旗镇两级 2,3
0106 结扎奖励  旗镇两级 3,4
0108 独子证 镇级 3
            */
                //签章密码处理
                if (m_FuncCode == "0111")
                {
                    if (curStep == "3" || curStep == "6")
                    {
                        if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                        {
                            SealNewPwd = DESEncrypt.Encrypt(SealNewPwd);
                        }
                        else { SealNewPwd = ""; SealPass = ""; }
                    }
                    else { SealNewPwd = DESEncrypt.Encrypt(SealNewPwd); }
                }
                else if (m_FuncCode == "0107")
                {
                    if (curStep == "2")
                    {
                        if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                        {
                            SealNewPwd = DESEncrypt.Encrypt(SealNewPwd);
                        }
                        else { SealNewPwd = ""; SealPass = ""; }
                    }
                    else { SealNewPwd = DESEncrypt.Encrypt(SealNewPwd); }
                }
                else if (m_FuncCode == "0103" || m_FuncCode == "0104" || m_FuncCode == "0105" || m_FuncCode == "0109" || m_FuncCode == "0110")
                {
                    if (curStep == "2" || curStep == "3")
                    {
                        if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                        {
                            SealNewPwd = DESEncrypt.Encrypt(SealNewPwd);
                        }
                        else { SealNewPwd = ""; SealPass = ""; }
                    }
                    else { SealNewPwd = DESEncrypt.Encrypt(SealNewPwd); }
                }
                else if (m_FuncCode == "0101" || m_FuncCode == "0102" || m_FuncCode == "0106" || m_FuncCode == "0108" || m_FuncCode == "0122")
                {
                    if (curStep == "3" || curStep == "4")
                    {
                        if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                        {
                            SealNewPwd = DESEncrypt.Encrypt(SealNewPwd);
                        }
                        else { SealNewPwd = ""; SealPass = ""; }
                    }
                    else { SealNewPwd = DESEncrypt.Encrypt(SealNewPwd); }
                }
                else { SealNewPwd = DESEncrypt.Encrypt(SealNewPwd); }


                //if (m_FuncCode == "0111" && BizStepTotal == "6")
                //{
                //    userRoleID = CommPage.GetSingleVal("SELECT TOP 1 RoleID FROM SYS_UserRoles WHERE UserID=" + m_UserID);
                //}
                //else { SealNewPwd = DESEncrypt.Encrypt(SealNewPwd); }

                if (SealNewPwd == SealPass || userRoleID == "6")
                {
                    string userMobile = CommPage.GetSingleVal("SELECT ContactTelA FROM BIZ_Contents WHERE BizID=" + m_ObjID);
                    if (string.IsNullOrEmpty(userMobile)) { userMobile = CommPage.GetSingleVal("SELECT ContactTelB FROM BIZ_Contents WHERE BizID=" + m_ObjID); }
                    // 自动同步操作员电话
                    SetAuditUserTel(ApprovalUserTel);

                    if (pFlag == "1")
                    {
                        m_SqlParams = "UPDATE BIZ_WorkFlows SET ";
                        m_SqlParams += "DeptName ='" + DeptName + "',Comments ='" + Comments + "',ApprovalUserID =" + m_UserID + ",Approval ='" + Approval + "',ApprovalUserTel ='" + ApprovalUserTel + "',OprateDate ='" + OprateDate + "',CertificateDateStart ='" + CertificateDateStart + "',CertificateDateEnd='" + CertificateDateEnd + "' ,ApprovalSignID='" + ApprovalID + "',ApprovalSignName='" + ApprovalSignName + "',ApprovalSignPath='" + ApprovalPath + "'";
                        m_SqlParams += "WHERE CommID IN(" + updateID + ")";
                        /* 通辽 修改内容20160517.doc
                         * 提交日期：2016年5月17日
                         * 提交人：通辽市卫生计生委  白文泉
                         * 接收人：西安网是          马莉娟
                         * 2、旗县的反映婚姻怀况证明(0110)，流程中有责任人，如果有把这过程去掉，有经办人就行。
                         * 处理方法  将当前经办人转换为责任人pFlag == "2"，同时处理公章信息
                         */
                        if (m_FuncCode == "0110")
                        {
                            DbHelperSQL.ExecuteSql(m_SqlParams);

                            pFlag = "2";
                            m_SqlParams = "UPDATE BIZ_WorkFlows SET ";
                            m_SqlParams += "DeptName ='" + DeptName + "',Comments ='" + Comments + "',AuditUser ='" + AuditUser + "',Signature ='" + Signature + "',OprateDate ='" + OprateDate + "',CertificateDateStart ='" + CertificateDateStart + "',CertificateDateEnd='" + CertificateDateEnd + "',Attribs=" + Attribs + ",CertificateNoA ='" + CertificateNoA + "',AuditUserSignID='" + AuditUserID + "',AuditUserSignName='" + AuditUserSignName + "',AuditUserSignPath='" + AuditUserPath + "' ";
                            m_SqlParams += "WHERE CommID IN(" + updateID + ")";
                        }

                    }
                    else if (pFlag == "2")
                    {
                        m_SqlParams = "UPDATE BIZ_WorkFlows SET ";
                        m_SqlParams += "DeptName ='" + DeptName + "',Comments ='" + Comments + "',AuditUser ='" + AuditUser + "',Signature ='" + Signature + "',OprateDate ='" + OprateDate + "',CertificateDateStart ='" + CertificateDateStart + "',CertificateDateEnd='" + CertificateDateEnd + "',Attribs=" + Attribs + ",CertificateNoA ='" + CertificateNoA + "',AuditUserSignID='" + AuditUserID + "',AuditUserSignName='" + AuditUserSignName + "',AuditUserSignPath='" + AuditUserPath + "' ";
                        m_SqlParams += "WHERE CommID IN(" + updateID + ")";
                    }
                    else
                    {
                        m_SqlParams = "UPDATE BIZ_WorkFlows SET ";
                        m_SqlParams += "DeptName ='" + DeptName + "',Comments ='" + Comments + "',ApprovalUserID =" + m_UserID + ",Approval ='" + Approval + "',AuditUser ='" + AuditUser + "',ApprovalUserTel ='" + ApprovalUserTel + "',Signature ='" + Signature + "',OprateDate ='" + OprateDate + "',CertificateDateStart ='" + CertificateDateStart + "',CertificateDateEnd='" + CertificateDateEnd + "',Attribs=" + Attribs + ",CertificateNoA ='" + CertificateNoA + "' ,ApprovalSignID='" + ApprovalID + "',ApprovalSignName='" + ApprovalSignName + "',ApprovalSignPath='" + ApprovalPath + "',AuditUserSignID='" + AuditUserID + "',AuditUserSignName='" + AuditUserSignName + "',AuditUserSignPath='" + AuditUserPath + "' ";
                        m_SqlParams += "WHERE CommID IN(" + updateID + ")";
                    }


                    if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                    {
                        // BIZ_WorkFlows Attribs:0,驳回；1,通过；9 默认未处理
                        // BIZ_Contents Attribs: 0,初始提交;1,审核中 2,通过 3,补正 4,撤销 5,注销 6,等待审核,9,归档
                        string opContents = "用户ID[" + m_UserID + "-" + Approval + "]于 " + DateTime.Now.ToString() + " 进行了业务审核操作.";
                        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                        if (Attribs == "0")
                        {
                            //list.Add("UPDATE BIZ_WorkFlows SET Approval ='',AuditUser ='',,ApprovalSignID='',ApprovalSignPath='',AuditUserSignID='',ApprovalPath=''  WHERE CommID IN(" + updateID + ")");
                            list.Add("UPDATE BIZ_Contents SET CurrentStepNa='" + curStepNa + "',Attribs=3,FinalDate=GetDate() WHERE BizID=" + m_ObjID);
                            string strMsg = this.txtBHMsg.Text;
                            //插入短讯消息提示
                            SendMsg.SendMsgByModem(userMobile, "您所申请的事项[" + m_FuncName + "]经审核，不符合政策要求，不予办理！" + strMsg + "");
                            CommPage.SetBizLog(m_ObjID, m_UserID, "审核驳回", "用户ID[" + m_UserID + "-" + Approval + "]于 " + DateTime.Now.ToString() + " 进行了业务审核驳回操作");
                        }
                        else
                        {
                            CommPage.SetBizLog(m_ObjID, m_UserID, "审核通过", opContents);
                            /*
                            0101 一孩 镇办3
                            0102 再生育 镇办3,旗4,
                            0107 一杯奶 镇办2
                            0111 终止妊娠 旗6/旗3
                             */
                            //1,经办人；2,责任人
                            //================================
                            if (pFlag == "1")
                            {

                                list.Add("UPDATE BIZ_Contents SET Attribs=1 WHERE BizID=" + m_ObjID);//双重审核经办人空执行
                            }
                            //else if (pFlag == "2")
                            //{
                            //}
                            else
                            {
                                //------------------
                                if (BizStepTotal == curStep)
                                {
                                    string QcfwzBmV = "";
                                    if (m_FuncCode == "0150")
                                    {
                                        QcfwzBmV = CommPage.GetSingleVal("SELECT COUNT(*)+1 FROM BIZ_Contents WHERE BizCode IN(0150)  AND  QcfwzBm!='' and QcfwzBmV!=''  ");
                                        QcfwzBmV = DateTime.Now.ToString("yyyy").Substring(3) + QcfwzBmV.PadLeft(5, '0');

                                    }
                                    list.Add("UPDATE BIZ_Contents SET QcfwzBm='" + CertificateNoA + "' ,QcfwzBmV='" + QcfwzBmV + "' WHERE BizCode='0150' and BizID=" + m_ObjID);
                                    list.Add("UPDATE BIZ_Contents SET Attribs=2,FinalDate=GetDate() WHERE BizID=" + m_ObjID);
                                    //插入短讯消息提示 某某,您提交的[业务名称]申请已办结。
                                    if (appDirection.Substring(0, 1) == "1") SendMsg.SendMsgByModem(userMobile, appDirection.Substring(2) + ",您提交的[" + m_FuncName + "]申请已办结。");
                                    //证件操作
                                    if (this.m_FuncCode == "0101" || this.m_FuncCode == "0102" || this.m_FuncCode == "0103" || this.m_FuncCode == "0108" || this.m_FuncCode == "0109" || this.m_FuncCode == "0122")
                                    {
                                        SetCertificateLog(m_ObjID, m_FuncCode); //保存证件记录
                                    }
                                }
                                else
                                {
                                    string nextUserTel = string.Empty;
                                    int msgStep = int.Parse(BizStepTotal) - 1;
                                    if (curStep == msgStep.ToString())
                                    {
                                        SendMsg.SendMsgByModem(userMobile, appDirection.Substring(2) + ",您提交的[" + m_FuncName + "]申请已初审通过，请持相关证件原件，正常工作日到约定地点办理相关手续。");
                                    }
                                    else
                                    {
                                        SetNextStepMsgByBizNo(this.m_FuncCode, int.Parse(BizStepTotal), int.Parse(curStep), appDirection.Substring(2));
                                    }
                                    // 某某，您提交的申请已初审，请持相关证件原件，正常工作日到约定地点办理相关手续。
                                    // 某某申请办理[业务名称]已初审,请审核。
                                    if (updateID.IndexOf(",") > 0)
                                    {
                                        list.Add("UPDATE BIZ_Contents SET CurrentStep=CurrentStep+2,CurrentStepNa='" + curStepNa + "',Attribs=1 WHERE BizID=" + m_ObjID);
                                    }
                                    else
                                    {
                                        //判断下一节点是否本州内
                                        if (IsNextFlowOut(m_ObjID, curStep)) { list.Add("UPDATE BIZ_Contents SET CurrentStep=" + curStep.Trim() + "+2,CurrentStepNa='" + curStepNa + "',Attribs=1 WHERE BizID=" + m_ObjID); }
                                        else { list.Add("UPDATE BIZ_Contents SET CurrentStep=" + curStep.Trim() + "+1,CurrentStepNa='" + curStepNa + "',Attribs=1 WHERE BizID=" + m_ObjID); }
                                    }
                                }
                                //------------------
                            }
                        }

                        list.Add("INSERT INTO [SYS_Log]([OprateUserID], [OprateContents], [OprateModel], [OprateUserIP]) VALUES(" + m_UserID + ", '" + opContents + "', '业务审核', '" + Request.UserHostAddress + "')");
                        DbHelperSQL.ExecuteSqlTran(list);
                        list = null;
                        // 业务日志

                        if (Attribs == "0")
                        {
                            // 审核驳回进入选择补正材料环节 aspx
                            string correctUrl = "/BizInfo/BizCorrects.aspx?action=bz&sourceUrl=" + m_SourceUrl + "&BizID=" + m_ObjID + "&FlowID=" + DESEncrypt.Encrypt(updateID);

                            MessageBox.ShowAndRedirect(this.Page, "操作提示：您驳回了该流程节点的事项申请，请“确定”后选择补正材料以生成补正通知书！", correctUrl, true);
                        }
                        else
                        {
                            MessageBox.ShowAndRedirect(this.Page, "操作提示：业务审核操作完毕！", m_TargetUrl, true, true);
                        }

                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：业务审核操作失败！", m_TargetUrl, true, true);
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您输入的签章密码错误！", m_TargetUrl, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true, true);
            }
        }

        /// <summary>
        /// 下一步流程短讯提示
        /// </summary>
        /// <param name="bizCode"></param>
        /// <param name="totalStep"></param>
        /// <param name="curStep"></param>
        /// <param name="appNames"></param>
        /// <returns></returns>
        private void SetNextStepMsgByBizNo(string bizCode, int totalStep, int curStep, string appNames)
        {
            int msgStep = 0;
            string nextUserTel = string.Empty;
            switch (bizCode)
            {
                case "0101":
                    if (curStep == totalStep - 1) { msgStep = totalStep; }
                    else { msgStep = 0; }
                    break;
                case "0102":
                    if (curStep == totalStep - 2) { msgStep = totalStep - 1; }
                    else if (curStep == totalStep - 1) { msgStep = totalStep; }
                    else { msgStep = 0; }
                    break;
                case "0103":
                    if (curStep == 1) { msgStep = 2; }
                    else if (curStep == 2) { msgStep = 3; }
                    else { msgStep = 0; }
                    break;
                case "0104":
                    if (curStep == 1) { msgStep = 2; }
                    else if (curStep == 2) { msgStep = 3; }
                    else { msgStep = 0; }
                    break;
                case "0105":
                    if (curStep == 1) { msgStep = 2; }
                    else if (curStep == 2) { msgStep = 3; }
                    else { msgStep = 0; }
                    break;
                case "0106":
                    if (curStep == totalStep - 2) { msgStep = totalStep - 1; }
                    else if (curStep == totalStep - 1) { msgStep = totalStep; }
                    else { msgStep = 0; }
                    break;
                case "0107":
                    if (curStep == 1) { msgStep = 2; }
                    else { msgStep = 0; }
                    break;
                case "0108":
                    if (curStep == totalStep - 1) { msgStep = totalStep; }
                    else { msgStep = 0; }
                    break;
                case "0109":
                    if (curStep == totalStep - 1) { msgStep = totalStep; }
                    else { msgStep = 0; }
                    break;
                case "0110":
                    if (curStep == 1) { msgStep = 2; }
                    else if (curStep == 2) { msgStep = 3; }
                    else { msgStep = 0; }
                    break;
                case "0111":
                    if (curStep == 1) { msgStep = 2; }
                    else if (curStep == totalStep - 1) { msgStep = totalStep; }
                    else { msgStep = 0; }
                    break;
                case "0122":
                    if (curStep == totalStep - 2) { msgStep = totalStep - 1; }
                    else if (curStep == totalStep - 1) { msgStep = totalStep; }
                    else { msgStep = 0; }
                    break;
                default: // 默认显示当前功能所有内容OprateModel ='数据修改'
                    msgStep = 0;
                    break;
            }
            if (msgStep > 0)
            {
                nextUserTel = CommPage.GetSingleVal("SELECT UserTel FROM USER_BaseInfo WHERE UserAccount=(SELECT AreaCode FROM BIZ_WorkFlows WHERE BizID=" + m_ObjID + " AND BizStep=" + msgStep.ToString() + ")");
                if (!string.IsNullOrEmpty(nextUserTel) && nextUserTel.Length == 11) SendMsg.SendMsgByModem(nextUserTel, appNames + "申请办理[" + m_FuncName + "]已初审,请审核。");
            }
        }

        /// <summary>
        /// 自动同步操作员联系电话
        /// </summary>
        /// <param name="userTel"></param>
        private void SetAuditUserTel(string userTel)
        {
            string oldTel = CommPage.GetSingleVal("SELECT UserTel FROM USER_BaseInfo WHERE UserID=" + m_UserID);
            if (string.IsNullOrEmpty(oldTel) || oldTel.Length < 7)
            {
                DbHelperSQL.ExecuteSql("UPDATE USER_BaseInfo SET UserTel='" + userTel + "' WHERE UserID=" + m_UserID);
            }
        }

        /// <summary>
        /// 保存打印的证件记录
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="BizCode"></param>
        /// <param name="BizName"></param>
        /// <param name="pName"></param>
        /// <param name="pCid"></param>
        private void SetCertificateLog(string bizID, string BizCode)
        {
            string BizName = string.Empty;
            string pName = string.Empty; ;
            string pCid = string.Empty;
            string CertificateName = this.m_FuncName;
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT BizName,Fileds01,PersonCidA FROM BIZ_Contents WHERE BizID=" + bizID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    BizName = sdr["BizName"].ToString();
                    pName = sdr["Fileds01"].ToString();
                    pCid = sdr["PersonCidA"].ToString();
                }
                sdr.Close();

                // 插入证件库
                m_SqlParams = "SELECT TOP 1 AreaCode,AreaName,DeptName,Approval,CertificateNoA,CertificateNoB,Attribs,CertificateDateStart,CertificateDateEnd FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep DESC";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    string CertificateNo = sdr["CertificateNoA"].ToString();
                    string CertificateGovName = sdr["DeptName"].ToString();
                    string StartDate = sdr["CertificateDateStart"].ToString();
                    string AreaCode = sdr["AreaCode"].ToString();

                    m_SqlParams = "SELECT COUNT(*) FROM BIZ_Certificates WHERE CertificateType=2 AND BizCode='" + BizCode + "' AND BizID=" + bizID + " AND CertificateNo='" + CertificateNo + "'";
                    if (CommPage.GetSingleVal(m_SqlParams) == "0")
                    {
                        m_SqlParams = "INSERT INTO BIZ_Certificates (";
                        m_SqlParams += "BizID,BizCode,BizName,CertificateNo,CertificateName,AreaCode,";
                        m_SqlParams += "PersonName,PersonCid,CertificateGovName,StartDate,CertificateType";
                        m_SqlParams += ") VALUES(";
                        m_SqlParams += "" + bizID + ",'" + BizCode + "','" + BizName + "','" + CertificateNo + "','" + CertificateName + "','" + AreaCode + "',";
                        m_SqlParams += "'" + pName + "','" + pCid + "','" + CertificateGovName + "','" + StartDate + "',2";
                        m_SqlParams += ")";

                        DbHelperSQL.ExecuteSql(m_SqlParams);
                    }
                }
                sdr.Close();
            }
            catch
            {
                if (sdr != null) sdr.Close();
            }
        }

        /// <summary>
        /// 流程节点是否涉及州外审核
        /// </summary>
        /// <param name="bizID">业务申请编号</param>
        /// <param name="curStep">当前处理步骤</param>
        /// <returns></returns>
        private bool IsNextFlowOut(string bizID, string curStep)
        {
            string Attribs = CommPage.GetSingleVal("SELECT Attribs FROM BIZ_WorkFlows WHERE BizID=" + bizID + " AND BizStep=" + curStep + " +1");
            if (Attribs == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 下拉取得经办人
        /// </summary>
        /// <param name="areaCode">区划</param>
        private void SetApprovalList(string areaCode)
        {
            m_SqlParams = "SELECT SignID, cast(SignID as varchar(255))+','+SignPhone as SignIDs, SignPath,SignName,SignPhone FROM [SYS_Sign] WHERE SignCode = '" + areaCode + "' AND SignPhone!='' AND  isnull(SignPhone,'')!=''  AND SignType='1' ORDER BY SignID";
            DataTable tmpDt = new DataTable();
            tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            //if (tmpDt.Rows.Count > 0)
            //{
            //    this.txtApprovalUserTel.Text = tmpDt.Rows[0]["SignPhone"].ToString();
            //}
            DDLApproval.DataSource = tmpDt;
            DDLApproval.DataTextField = "SignName";
            DDLApproval.DataValueField = "SignIDs";
            DDLApproval.DataBind();
            ListItem li = new ListItem("--请选择签名--", "");
            DDLApproval.Items.Insert(0, li);
            li = null;
            //DDLApproval.Items.Insert(0, new ListItem(m_SiteAreaNa, m_SiteArea));
            tmpDt = null;
        }
        /// <summary>
        /// 下拉取得责任人
        /// </summary>
        /// <param name="areaCode">区划</param>
        private void SetAuditUserList(string areaCode)
        {
            m_SqlParams = "SELECT  SignID, cast(SignID as varchar(255))+','+SignPhone as SignIDs, SignPath,SignName,SignPhone FROM [SYS_Sign] WHERE SignCode = '" + areaCode + "' AND  isnull(SignPhone,'')!='' AND SignType='0' ORDER BY SignID";
            DataTable tmpDt = new DataTable();
            tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            //if (tmpDt.Rows.Count > 0)
            //{
            //    this.txtApprovalUserTel.Text = tmpDt.Rows[0]["SignPhone"].ToString();
            //}
            DDLAuditUser.DataSource = tmpDt;
            DDLAuditUser.DataTextField = "SignName";
            DDLAuditUser.DataValueField = "SignIDs";
            DDLAuditUser.DataBind();
            ListItem li = new ListItem("--请选择签名--", "");
            DDLAuditUser.Items.Insert(0, li);
            li = null;
            //DDLApproval.Items.Insert(0, new ListItem(m_SiteAreaNa, m_SiteArea));
            tmpDt = null;
        }
    }
}

