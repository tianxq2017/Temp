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
    public partial class BizFlowApp : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号

        private string m_SqlParams;
        public string m_TargetUrl;

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
                case "Guidang": // 归档
                    funcName = "删除";
                    GuiDang(m_ObjID);
                    break;
                case "view": // 查看
                    funcName = "查看内容";
                    ShowModInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "起始页  &gt;&gt; " + CommPage.GetAllBizName(m_FuncCode) + " &gt;" + funcName + "：";
        }

        /// <summary>
        /// 审核信息 BizID=" + m_ObjID;
        /// </summary>
        /// <param name="objID"></param>
        private void GuiDang(string objID)
        {
            try
            {
                // Attribs: 0初始提交; 1审核中; 2,通过 3驳回; 4删除; 9,归档
                string attribs = string.Empty;
                if (join.pms.dal.CommPage.CheckAttribs("BIZ_Contents", "Attribs", "BizID IN(" + objID + ")", "", ref attribs))
                {
                    if (attribs == "2")
                    {
                        m_SqlParams = "UPDATE BIZ_Contents SET Attribs=9 WHERE BizID IN(" + objID + ") ";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                          MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的事项记录归档操作成功！", m_TargetUrl, true);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息包括未处理的事项,只有流程终结的事宜才能归档！", m_TargetUrl, true);
                    }
                }
                else
                {
                      MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息包含尚不是同一类信息！", m_TargetUrl, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
        }


        /// <summary>
        /// 显示待处理信息
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            string userAreaCode = "", useSealPath = "", useSealPass = "", userNames = "", userDepts = string.Empty;
            string appUserID = string.Empty;
            string bizStepTotal = "", bizStepCur = string.Empty;
            StringBuilder s = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                // 1,调取当前审核用户信息
                m_SqlParams = "SELECT UserAccount,UserName,UserAreaCode,(SELECT DeptName FROM USER_Department WHERE DeptCode=A.DeptCode) As DeptName FROM USER_BaseInfo A WHERE UserID=" + m_UserID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        userAreaCode = sdr["UserAreaCode"].ToString();
                        userNames = sdr["UserName"].ToString();// +"(" + userAreaCode + ")";
                        userDepts = sdr["DeptName"].ToString();
                    }
                    sdr.Close();
                    // 2,分析是否具有审核权限
                    if (!string.IsNullOrEmpty(userAreaCode))
                    {
                        m_SqlParams = "SELECT CommID,BizStepTotal,BizStep,AreaCode,AreaName,Comments,ApprovalUserID,Approval,Signature,Attribs,CreateDate,OprateDate FROM BIZ_WorkFlows WHERE AreaCode='" + userAreaCode + "' AND BizID=" + m_ObjID;
                        sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                        if (sdr.HasRows)
                        {
                            this.txtSealPath.Value = useSealPath;
                            this.txtSealPass.Value = useSealPass;
                            while (sdr.Read())
                            {
                                bizStepTotal = PageValidate.GetTrim(sdr["BizStepTotal"].ToString());
                                bizStepCur = PageValidate.GetTrim(sdr["BizStep"].ToString());

                                this.txtBizFlowID.Value = sdr["CommID"].ToString();
                                this.txtBizStep.Value = bizStepCur;
                                this.txtBizStepTotal.Value = bizStepTotal;
                                this.LiteralBizAppDate.Text = sdr["CreateDate"].ToString();
                                this.txtAreaName.Text = userDepts;
                                this.txtApproval.Text = userNames;
                                this.txtBizStepNames.Value = sdr["AreaName"].ToString() + "审核";
                                this.txtOprateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                                appUserID = sdr["ApprovalUserID"].ToString();
                                if (!string.IsNullOrEmpty(appUserID))
                                {
                                    this.btnAdd.Enabled = false;
                                    MessageBox.ShowAndRedirect(this.Page, "操作提示：该流程节点已经审核处理，请点选申请事项，点击“工作流程”按钮查阅流程详情！", m_TargetUrl, true);
                                }
                            }
                            sdr.Close();
                        }
                        else
                        {
                            sdr.Close();     
                            MessageBox.ShowAndRedirect(this.Page, "操作提示：很抱歉,您不具有预置流程节点的处理权限！", m_TargetUrl, true);
                        }
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：请联系管理员授权！", m_TargetUrl, true);
                }
            }
            catch
            {
                if (sdr != null) sdr.Close();
                MessageBox.ShowAndRedirect(this.Page, "操作提示：申请处理时出现错误！", m_TargetUrl, true);
            }

        }

        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // SELECT BizID,BizStep,AreaCode,AreaName,Comments,Approval,Signature,CreateDate,OprateDate FROM BIZ_WorkFlows
            string strErr = string.Empty;
            string updateID = this.txtBizFlowID.Value;
            string BizStepTotal = "1";// this.txtBizStepTotal.Value;
            string curStep = "1";// this.txtBizStep.Value;
            string curStepNa = this.txtBizStepNames.Value;
            string SealPath = this.txtSealPath.Value;
            string SealPass = this.txtSealPass.Value;

            string Attribs = this.DDLPass.SelectedValue;
            string DeptName = this.txtAreaName.Text;
            string Comments = PageValidate.GetTrim(this.txtComments.Text);
            string Approval = PageValidate.GetTrim(this.txtApproval.Text);
            string Signature = SealPath;
            string OprateDate = PageValidate.GetTrim(this.txtOprateDate.Value);

            string CertificateNoA = PageValidate.GetTrim(this.txtCertificateNoA.Text);
            string CertificateMemo = PageValidate.GetTrim(this.txtCertificateMemo.Text);

            if (String.IsNullOrEmpty(BizStepTotal)) strErr += "参数错误！\\n";
            if (String.IsNullOrEmpty(curStep)) strErr += "参数错误！\\n";
            if (String.IsNullOrEmpty(updateID)) strErr += "审核节点参数丢失！\\n";
            if (String.IsNullOrEmpty(Comments)) strErr += "请输入审核意见！\\n";
            if (String.IsNullOrEmpty(Approval)) strErr += "审核人不能为空！\\n";

            if (string.IsNullOrEmpty(OprateDate)) { OprateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }
            else { OprateDate = DateTime.Parse(OprateDate).ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss"); }

            

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            //  SealPass = DESEncrypt.Encrypt(SealPass);
            try
            {
                //SealNewPwd = DESEncrypt.Encrypt(SealNewPwd);
                // Comments,ApprovalUserID,Approval,Signature,OprateDate
                m_SqlParams = "UPDATE BIZ_WorkFlows SET ";
                m_SqlParams += "DeptName ='" + DeptName + "',Comments ='" + Comments + "',ApprovalUserID =" + m_UserID + ",Approval ='" + Approval + "',OprateDate ='" + OprateDate + "',Attribs=" + Attribs + " ";
                m_SqlParams += "WHERE CommID=" + updateID;
                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                    // BIZ_WorkFlows Attribs:0,未通过1,审核成功
                    // BIZ_Contents Attribs: 0初始提交; 1审核中; 2,通过 3驳回; 4删除; 9,归档
                    string opContents = "用户ID[" + m_UserID + "-" + Approval + "]于 " + DateTime.Now.ToString() + " 进行了业务审核操作.";
                    System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                    if (Attribs == "0")
                    {
                        list.Add("UPDATE BIZ_Contents SET CurrentStepNa='" + curStepNa + "',Attribs=3 WHERE BizID=" + m_ObjID);
                        //插入短讯消息提示
                        string userMobile = CommPage.GetSingleVal("SELECT ContactTelA FROM BIZ_Contents WHERE BizID=" + m_ObjID);
                        SendMsg.SendMsgByModem(userMobile, "您所申请的申请事项经审核，不符合政策要求，不予办理！");
                    }
                    else
                    {
                        list.Add("UPDATE BIZ_Contents SET Attribs=2 WHERE BizID=" + m_ObjID);
                        //插入短讯消息提示
                        string userMobile = CommPage.GetSingleVal("SELECT ContactTelA FROM BIZ_Contents WHERE BizID=" + m_ObjID);
                        SendMsg.SendMsgByModem(userMobile, "您所申请的申请事项已经通过，请携带相关材料，根据提示信息进行办理。");
                    }

                    list.Add("INSERT INTO [SYS_Log]([OprateUserID], [OprateContents], [OprateModel], [OprateUserIP]) VALUES(" + m_UserID + ", '" + opContents + "', '业务申请', '" + Request.UserHostAddress + "')");
                    DbHelperSQL.ExecuteSqlTran(list);
                    list = null;

                    MessageBox.ShowAndRedirect(this.Page, "操作提示：业务审核操作成功！", m_TargetUrl, true);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：业务审核操作失败！", m_TargetUrl, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
        }
    }
}

