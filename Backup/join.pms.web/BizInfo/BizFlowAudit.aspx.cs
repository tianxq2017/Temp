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

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

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
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        #region �����֤���������
        /// <summary>
        /// �����֤
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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// ��֤���ܵĲ���
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
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        /// <summary>
        /// ���ò�����Ϊ
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("�Ƿ����ʣ���������ֹ��");
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
                    funcName = "ҵ�����";
                    ShowModInfo(m_ObjID);
                    break;
                case "printok": // ��ӡȷ��
                    funcName = "��ӡȷ��";
                    PrintOK(m_ObjID);
                    break;
                case "Guidang": // �鵵
                    funcName = "�鵵";
                    GuiDang(m_ObjID);
                    break;
                case "YiAudit": // �鵵
                    funcName = "ɾ��";
                    Audit(m_ObjID);
                    break;
                case "view": // �鿴
                    funcName = "�鿴����";
                    //ShowModInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true, true);
                    break;
            }
            this.LiteralNav.Text = "��ʼҳ  &gt;&gt; " + CommPage.GetAllBizName(m_FuncCode) + " &gt;" + funcName + "��";
        }

        // txtCertificateNoA

        /// <summary>
        /// ȷ��
        /// </summary>
        /// <param name="objID"></param>
        private void PrintOK(string objID)
        {
            try
            {
                // Attribs: 0��ʼ�ύ; 1�����; 2,ͨ�� 3����; 4ɾ��;8�Ѵ�֤; 9,�鵵
                string attribs = string.Empty;
                if (join.pms.dal.CommPage.CheckAttribs("BIZ_Contents", "Attribs", "BizID IN(" + objID + ")", "", ref attribs))
                {
                    if (attribs == "2")
                    {
                        m_SqlParams = "UPDATE BIZ_Contents SET Attribs='8' WHERE BizID IN(" + objID + ") ";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        CommPage.SetBizLog(objID, m_UserID, "��ӡȷ��", "�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " �����˴�ӡȷ�ϲ���");

                        MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����������˴�ӡȷ�ϲ�����", m_TargetUrl, true, true);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ����δ�������������,ֻ������ͨ�������˲���ִ�С���ӡȷ�ϡ�������", m_TargetUrl, true, true);
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ�����в���ͬһ����Ϣ��", m_TargetUrl, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true, true);
            }
        }
        /// <summary>
        /// �鵵
        /// </summary>
        /// <param name="objID"></param>
        private void GuiDang(string objID)
        {
            try
            {
                // Attribs: 0,��ʼ�ύ;1,������ 2,ͨ�� 3,���� 4,���� 5,ע��, 6,�ȴ�����,8,�ѳ�֤,9,�鵵
                string attribs = string.Empty;
                if (join.pms.dal.CommPage.CheckAttribs("BIZ_Contents", "Attribs", "BizID IN(" + objID + ")", "", ref attribs))
                {
                    if (attribs == "8")
                    {
                        m_SqlParams = "UPDATE BIZ_Contents SET Attribs='9' WHERE BizID IN(" + objID + ") ";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        CommPage.SetBizLog(objID, m_UserID, "ҵ��鵵", "�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " ������ҵ��鵵����");

                        MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ��������¼�鵵�����ɹ���", m_TargetUrl, true, true);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ����δȷ�ϵ�����,ֻ������ͨ���Ҿ�������ӡȷ�ϡ���ҵ����ܹ鵵��", m_TargetUrl, true, true);
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ�����в���ͬһ����Ϣ��", m_TargetUrl, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true, true);
            }
        }

        /// <summary>
        /// һ����� 
        /// </summary>
        /// <param name="objID"></param>
        private void Audit(string objID)
        {
            try
            {
                // Attribs: 0,��ʼ�ύ;1,����� 2,ͨ�� 3,���� 4,���� 5,ע��, 6,�ȴ����,9,�鵵
                string attribs = string.Empty;
                if (join.pms.dal.CommPage.CheckAttribs("BIZ_Contents", "Attribs", "BizID IN(" + objID + ")", "", ref attribs))
                {
                    if (attribs == "0")
                    {
                        m_SqlParams = "UPDATE BIZ_Contents SET Attribs=2,FinalDate=GetDate() WHERE BizID IN(" + objID + ") ";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ��������¼��˲����ɹ���", m_TargetUrl, true, true);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ��������˺͹鵵������,ֻ�г�ʼ�ύ��ҵ�������ˣ�", m_TargetUrl, true, true);
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ�����в���ͬһ����Ϣ��", m_TargetUrl, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true, true);
            }
        }


        // this.txtCertificateNoA.Value=BizWorkFlows.GetCertificateNo();
        /// <summary>
        /// ��ʾ��������Ϣ
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
                    // ��ȡҵ�����֤����Ϣ
                    BIZ_Docs doc = new BIZ_Docs();
                    this.LiteralDocs.Text = doc.GetBizDocs(bizID);
                    doc = null;
                    // 1,��ȡ��ǰ����û���Ϣ
                    SysSeal seal = new SysSeal();
                    seal.SealUserID = m_UserID;
                    seal.GetSealByUser();
                    /*
                    1	ϵͳ����Ա
                    2	ҵ�����-����
                    3	ҵ����-����
                    4	ҵ����-���
                    5	ҵ����-����/��
                    6	ҵ����-ҽԺ
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
                        //this.txtApprovalUserTel.Text = seal.UserTel;//�Ѹ�Ϊǩ�����еĵ绰
                        this.txtApproval.Text = seal.UserName;// ������

                        seal = null;
                        // 2,�����Ƿ�������Ȩ��
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
                                    0101 һ�� ���3
                                    0102 ���� ���3
                                    0107 һ���� ���2
                                    0111 ��ֹ���� ��6/��3
                                     */
                                // ��Ҫ�����˳�������˽ڵ��
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
                                                MessageBox.ShowAndRedirect(this.Page, "������ʾ���뾭������˺�����������ˣ�", m_TargetUrl, true, true);
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
                                            //    MessageBox.ShowAndRedirect(this.Page, "������ʾ�������̽ڵ���Ҫ������������Ѿ���ˣ������ظ�������", m_TargetUrl, true, true);
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
                                                MessageBox.ShowAndRedirect(this.Page, "������ʾ���뾭������˺�����������ˣ�", m_TargetUrl, true, true);
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
                                            //    MessageBox.ShowAndRedirect(this.Page, "������ʾ�������̽ڵ���Ҫ������������Ѿ���ˣ������ظ�������", m_TargetUrl, true, true);
                                            //    return;
                                            //}
                                        }
                                    }
                                }
                                /*
0103 ���� ��������2,3
0104 �ط� ��������2,3
0105 �����츻 �������� 2,3
0106 ��������  �������� 3,4
0108 ����֤ �� 3
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
                                                MessageBox.ShowAndRedirect(this.Page, "������ʾ���뾭������˺�����������ˣ�", m_TargetUrl, true, true);
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            this.PanelAllowDate.Visible = false;
                                            this.PanelAudit.Visible = false;
                                            this.PanelApproval.Visible = true;
                                            this.PanelSeal.Visible = false;
                                            //ֻҪ���̽ڵ�����δ���,�����������
                                            if (!string.IsNullOrEmpty(bwf.Approval) && !string.IsNullOrEmpty(bwf.Comments))
                                            {
                                                MessageBox.ShowAndRedirect(this.Page, "������ʾ�������̽ڵ���Ҫ������������Ѿ���ˣ������ظ�������", m_TargetUrl, true, true);
                                                return;
                                            }
                                        }

                                        if (m_FuncCode == "0102" && bizStepCur == "4") this.PanelAllowDate.Visible = true; // �켶��ʾ
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
                                                MessageBox.ShowAndRedirect(this.Page, "������ʾ���뾭������˺�����������ˣ�", m_TargetUrl, true, true);
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
                                            //    MessageBox.ShowAndRedirect(this.Page, "������ʾ�������̽ڵ���Ҫ������������Ѿ���ˣ������ظ�������", m_TargetUrl, true, true);
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
                                    // ����ƥ��������
                                    BizAuditInfo bizAI = new BizAuditInfo();
                                    bizAI.BizCode = m_FuncCode;
                                    bizAI.BizStep = bizStepCur;
                                    this.LiteralComments.Text = bizAI.GetAuditInfo(bizStepTotal, bizID);
                                    bizAI = null;

                                    #region ����ҵ���Ŵ�����˽ڵ�
                                    /*
                                    0101	һ�������Ǽ� 3 
                                    0102	���������Ǽ� 4
                                    0103	�������� 3
                                    0108	������Ů��ĸ����֤ 3
                                    0109	�������˿ڻ���֤���� 3
                                     */
                                    // �ϲ���� �弶��Ů˫���ϲ���� (0101,0102,0106,0107,0108,0109)
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

                                    // ֤�����
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
                                    this.txtBizStepNames.Value = bwf.AreaName + "���";
                                    this.txtOprateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                                    this.txtCertificateDateStart.Value = DateTime.Now.ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    MessageBox.ShowAndRedirect(this.Page, "������ʾ���������и����̽ڵ�����Ȩ�޻��߸����̽ڵ��Ѿ���ˣ��������������̡���ť�����������飡", m_TargetUrl, true, true);
                                }
                            }
                            else
                            {
                                MessageBox.ShowAndRedirect(this.Page, "������ʾ�������̽ڵ��Ѿ������ϻ����������и����̽ڵ�����Ȩ�ޣ�", m_TargetUrl, true, true);
                            }
                            bwf = null;
                        }
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ������δ����ǩ��Ȩ��,����ϵ����Ա��Ȩ��", m_TargetUrl, true, true);
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ�������̽ڵ���δ׼�����������޸��ϴ�����֤�գ�Ȼ�������ˣ������������飬���������̡���ť�����������飡", m_TargetUrl, true, true);
                }
            }
            catch
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǩ��ʱ���ִ���", m_TargetUrl, true, true);
            }
        }

        /// <summary>
        /// �ж��Ƿ�ǰ�ɴ��������
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
                        int lastStep = int.Parse(myStep) - 1;//�ж���һ���Ƿ���
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
        /// �ж��Ƿ�ǰҽԺ
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
            string pFlag = "0";//0,������ͨ��1,˫������У��������˷�������ͨ�����ˣ�2,������
            string appDirection = CommPage.GetSingleVal("SELECT CAST(InitDirection As varchar)+'@'+Fileds01+' '+Fileds08 FROM BIZ_Contents WHERE BizID=" + m_ObjID); // InitDirection:0,ǰ��;1,����

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

            // �����������Ϣȡ��
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
            // �����������Ϣȡ��
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

            // ֪ͨ�����˲���
            if (String.IsNullOrEmpty(BizStepTotal)) strErr += "��������\\n";
            if (String.IsNullOrEmpty(curStep)) strErr += "���̽ڵ��������\\n";
            if (String.IsNullOrEmpty(updateID)) strErr += "��˽ڵ������ʧ��\\n";
            if (String.IsNullOrEmpty(Comments) || Comments == "��ѡ�����������" || Comments == "��ѡ�����������") { strErr += "��������������\\n"; }
            else { }

            /*��������*/
            string txtStartDate = CommBiz.GetTrim(this.txtStartDate.Value);
            string txtEndDate = CommBiz.GetTrim(this.txtEndDate.Value);
            //if (m_FuncCode == "0102") CertificateDateStart = txtStartDate;
            //�����ˡ�������ǩ������
            if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
            {
                if (String.IsNullOrEmpty(txtAuditUserPass))
                {
                    strErr += "������������ǩ�����룡\\n";
                }
                else
                {
                    txtAuditUserPass = DESEncrypt.Encrypt(txtAuditUserPass);
                    if (txtAuditUserPass != AuditUserPass)
                    {
                        strErr += "������ǩ�������������\\n";
                    }
                }
            }
            else
            {
                if (String.IsNullOrEmpty(Approval)) strErr += "�����뾭����������\\n";

                if (String.IsNullOrEmpty(txtApprovalPass)) strErr += "�����뾭����ǩ�����룡\\n";

                if (String.IsNullOrEmpty(ApprovalUserTel)) strErr += "�����뾭������ϵ�绰��\\n";

                //if (String.IsNullOrEmpty(Approval)) strErr += "����˲���Ϊ�գ�\\n";

                txtApprovalPass = DESEncrypt.Encrypt(txtApprovalPass);
                if (txtApprovalPass != ApprovalPass)
                {
                    strErr += "������ǩ�������������\\n";
                }
            }

            // =====��������֤ start=======
            if (m_FuncCode == "0111")
            {
                if (BizStepTotal == "3" && curStep == "3")
                {
                    if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                    {
                        if (String.IsNullOrEmpty(AuditUser)) strErr += "�����������ˣ�\\n";

                        if (String.IsNullOrEmpty(SealNewPwd)) strErr += "������ǩ�����룡\\n";
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
                            if (String.IsNullOrEmpty(AuditUser)) strErr += "�����������ˣ�\\n";
                            if (String.IsNullOrEmpty(SealNewPwd)) strErr += "������ǩ�����룡\\n";
                            pFlag = "2";
                        }
                        else { pFlag = "1"; }
                    }
                    else if (curStep == "3" || curStep == "4" || curStep == "5")
                    {
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(SealNewPwd)) strErr += "������ǩ�����룡\\n";
                    }

                }
                else { if (String.IsNullOrEmpty(SealNewPwd)) strErr += "������ǩ�����룡\\n"; }
            }
            else if (m_FuncCode == "0107")
            {
                if (curStep == "2")
                {
                    if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                    {
                        if (String.IsNullOrEmpty(AuditUser)) strErr += "�����������ˣ�\\n";
                        if (String.IsNullOrEmpty(SealNewPwd)) strErr += "������ǩ�����룡\\n";
                        pFlag = "2";
                    }
                    else { pFlag = "1"; }
                }
                else { if (String.IsNullOrEmpty(SealNewPwd)) strErr += "������ǩ�����룡\\n"; }
            }
            else if (m_FuncCode == "0103" || m_FuncCode == "0104" || m_FuncCode == "0105" || m_FuncCode == "0109" || m_FuncCode == "0110")
            {
                if (curStep == "2" || curStep == "3")
                {
                    if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                    {
                        if (String.IsNullOrEmpty(AuditUser)) strErr += "�����������ˣ�\\n";
                        if (String.IsNullOrEmpty(SealNewPwd)) strErr += "������ǩ�����룡\\n";
                        pFlag = "2";
                    }
                    else { pFlag = "1"; }
                }
                else { if (String.IsNullOrEmpty(SealNewPwd)) strErr += "������ǩ�����룡\\n"; }
            }
            else if (m_FuncCode == "0101" || m_FuncCode == "0102" || m_FuncCode == "0106" || m_FuncCode == "0108" || m_FuncCode == "0122")
            {
                if (curStep == "3" || curStep == "4")
                {
                    if (userAcc.Length > 4 && userAcc.Substring(0, 4) == "zrr-")
                    {
                        if (String.IsNullOrEmpty(AuditUser)) strErr += "�����������ˣ�\\n";
                        if (String.IsNullOrEmpty(SealNewPwd)) strErr += "������ǩ�����룡\\n";
                        pFlag = "2";
                    }
                    else { pFlag = "1"; }
                }
                else { if (String.IsNullOrEmpty(SealNewPwd)) strErr += "������ǩ�����룡\\n"; }
            }
            else { if (String.IsNullOrEmpty(SealNewPwd)) strErr += "������ǩ�����룡\\n"; }
            // =====��������֤ end=======

            if (string.IsNullOrEmpty(OprateDate)) { OprateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }
            else { OprateDate = DateTime.Parse(OprateDate).ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss"); }
            if (Attribs == "4")
            {
                strErr += "��ѡ�����̽ڵ㴦����ۣ��Ƿ��ܹ�ͨ����\\n";
            }
            else if (Attribs == "0")
            {
                //����
                if (Comments.IndexOf("��ͬ���ϱ���") > 0) strErr += "ѡ��ͬ���ϱ�����������Ҫ���ͨ����\\n";
                // ��ͬ���ϱ���
            }
            else
            {
                if (Comments.IndexOf("����") > 0) strErr += "��Ҫ����ʱ��������˽�����ѡ����ͨ����\\n";
            }
            // 5����ӡ��֤��ҵ��
            if (this.m_FuncCode == "0101" || this.m_FuncCode == "0102" || this.m_FuncCode == "0103" || this.m_FuncCode == "0108" || this.m_FuncCode == "0109" || this.m_FuncCode == "0122")
            {
                if (BizStepTotal == curStep)
                {
                    if (Attribs == "1")
                    {
                        if (String.IsNullOrEmpty(CertificateNoA)) strErr += "������֤����ţ�\\n";
                        //if (String.IsNullOrEmpty(CertificateMemo)) strErr += "��ȷ����֤ʱ�䣡\\n";
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

                //�ٴμ��֤���Ƿ��ظ�
                if (!string.IsNullOrEmpty(CertificateNoA))
                {

                    join.pms.dal.BizWorkFlows bwf2 = new join.pms.dal.BizWorkFlows();
                    CertificateNoA = bwf2.GetCertificateNos(CertificateNoA, 0);
                }

                /*
0103 ���� ��������2,3
0104 �ط� ��������2,3
0105 �����츻 �������� 2,3
0106 ��������  �������� 3,4
0108 ����֤ �� 3
            */
                //ǩ�����봦��
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
                    // �Զ�ͬ������Ա�绰
                    SetAuditUserTel(ApprovalUserTel);

                    if (pFlag == "1")
                    {
                        m_SqlParams = "UPDATE BIZ_WorkFlows SET ";
                        m_SqlParams += "DeptName ='" + DeptName + "',Comments ='" + Comments + "',ApprovalUserID =" + m_UserID + ",Approval ='" + Approval + "',ApprovalUserTel ='" + ApprovalUserTel + "',OprateDate ='" + OprateDate + "',CertificateDateStart ='" + CertificateDateStart + "',CertificateDateEnd='" + CertificateDateEnd + "' ,ApprovalSignID='" + ApprovalID + "',ApprovalSignName='" + ApprovalSignName + "',ApprovalSignPath='" + ApprovalPath + "'";
                        m_SqlParams += "WHERE CommID IN(" + updateID + ")";
                        /* ͨ�� �޸�����20160517.doc
                         * �ύ���ڣ�2016��5��17��
                         * �ύ�ˣ�ͨ������������ί  ����Ȫ
                         * �����ˣ���������          �����
                         * 2�����صķ�ӳ��������֤��(0110)���������������ˣ�����а������ȥ�����о����˾��С�
                         * ������  ����ǰ������ת��Ϊ������pFlag == "2"��ͬʱ��������Ϣ
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
                        // BIZ_WorkFlows Attribs:0,���أ�1,ͨ����9 Ĭ��δ����
                        // BIZ_Contents Attribs: 0,��ʼ�ύ;1,����� 2,ͨ�� 3,���� 4,���� 5,ע�� 6,�ȴ����,9,�鵵
                        string opContents = "�û�ID[" + m_UserID + "-" + Approval + "]�� " + DateTime.Now.ToString() + " ������ҵ����˲���.";
                        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                        if (Attribs == "0")
                        {
                            //list.Add("UPDATE BIZ_WorkFlows SET Approval ='',AuditUser ='',,ApprovalSignID='',ApprovalSignPath='',AuditUserSignID='',ApprovalPath=''  WHERE CommID IN(" + updateID + ")");
                            list.Add("UPDATE BIZ_Contents SET CurrentStepNa='" + curStepNa + "',Attribs=3,FinalDate=GetDate() WHERE BizID=" + m_ObjID);
                            string strMsg = this.txtBHMsg.Text;
                            //�����Ѷ��Ϣ��ʾ
                            SendMsg.SendMsgByModem(userMobile, "�������������[" + m_FuncName + "]����ˣ�����������Ҫ�󣬲������" + strMsg + "");
                            CommPage.SetBizLog(m_ObjID, m_UserID, "��˲���", "�û�ID[" + m_UserID + "-" + Approval + "]�� " + DateTime.Now.ToString() + " ������ҵ����˲��ز���");
                        }
                        else
                        {
                            CommPage.SetBizLog(m_ObjID, m_UserID, "���ͨ��", opContents);
                            /*
                            0101 һ�� ���3
                            0102 ������ ���3,��4,
                            0107 һ���� ���2
                            0111 ��ֹ���� ��6/��3
                             */
                            //1,�����ˣ�2,������
                            //================================
                            if (pFlag == "1")
                            {

                                list.Add("UPDATE BIZ_Contents SET Attribs=1 WHERE BizID=" + m_ObjID);//˫����˾����˿�ִ��
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
                                    //�����Ѷ��Ϣ��ʾ ĳĳ,���ύ��[ҵ������]�����Ѱ�ᡣ
                                    if (appDirection.Substring(0, 1) == "1") SendMsg.SendMsgByModem(userMobile, appDirection.Substring(2) + ",���ύ��[" + m_FuncName + "]�����Ѱ�ᡣ");
                                    //֤������
                                    if (this.m_FuncCode == "0101" || this.m_FuncCode == "0102" || this.m_FuncCode == "0103" || this.m_FuncCode == "0108" || this.m_FuncCode == "0109" || this.m_FuncCode == "0122")
                                    {
                                        SetCertificateLog(m_ObjID, m_FuncCode); //����֤����¼
                                    }
                                }
                                else
                                {
                                    string nextUserTel = string.Empty;
                                    int msgStep = int.Parse(BizStepTotal) - 1;
                                    if (curStep == msgStep.ToString())
                                    {
                                        SendMsg.SendMsgByModem(userMobile, appDirection.Substring(2) + ",���ύ��[" + m_FuncName + "]�����ѳ���ͨ����������֤��ԭ�������������յ�Լ���ص�������������");
                                    }
                                    else
                                    {
                                        SetNextStepMsgByBizNo(this.m_FuncCode, int.Parse(BizStepTotal), int.Parse(curStep), appDirection.Substring(2));
                                    }
                                    // ĳĳ�����ύ�������ѳ���������֤��ԭ�������������յ�Լ���ص�������������
                                    // ĳĳ�������[ҵ������]�ѳ���,����ˡ�
                                    if (updateID.IndexOf(",") > 0)
                                    {
                                        list.Add("UPDATE BIZ_Contents SET CurrentStep=CurrentStep+2,CurrentStepNa='" + curStepNa + "',Attribs=1 WHERE BizID=" + m_ObjID);
                                    }
                                    else
                                    {
                                        //�ж���һ�ڵ��Ƿ�����
                                        if (IsNextFlowOut(m_ObjID, curStep)) { list.Add("UPDATE BIZ_Contents SET CurrentStep=" + curStep.Trim() + "+2,CurrentStepNa='" + curStepNa + "',Attribs=1 WHERE BizID=" + m_ObjID); }
                                        else { list.Add("UPDATE BIZ_Contents SET CurrentStep=" + curStep.Trim() + "+1,CurrentStepNa='" + curStepNa + "',Attribs=1 WHERE BizID=" + m_ObjID); }
                                    }
                                }
                                //------------------
                            }
                        }

                        list.Add("INSERT INTO [SYS_Log]([OprateUserID], [OprateContents], [OprateModel], [OprateUserIP]) VALUES(" + m_UserID + ", '" + opContents + "', 'ҵ�����', '" + Request.UserHostAddress + "')");
                        DbHelperSQL.ExecuteSqlTran(list);
                        list = null;
                        // ҵ����־

                        if (Attribs == "0")
                        {
                            // ��˲��ؽ���ѡ�������ϻ��� aspx
                            string correctUrl = "/BizInfo/BizCorrects.aspx?action=bz&sourceUrl=" + m_SourceUrl + "&BizID=" + m_ObjID + "&FlowID=" + DESEncrypt.Encrypt(updateID);

                            MessageBox.ShowAndRedirect(this.Page, "������ʾ���������˸����̽ڵ���������룬�롰ȷ������ѡ�������������ɲ���֪ͨ�飡", correctUrl, true);
                        }
                        else
                        {
                            MessageBox.ShowAndRedirect(this.Page, "������ʾ��ҵ����˲�����ϣ�", m_TargetUrl, true, true);
                        }

                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ��ҵ����˲���ʧ�ܣ�", m_TargetUrl, true, true);
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ���������ǩ���������", m_TargetUrl, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true, true);
            }
        }

        /// <summary>
        /// ��һ�����̶�Ѷ��ʾ
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
                default: // Ĭ����ʾ��ǰ������������OprateModel ='�����޸�'
                    msgStep = 0;
                    break;
            }
            if (msgStep > 0)
            {
                nextUserTel = CommPage.GetSingleVal("SELECT UserTel FROM USER_BaseInfo WHERE UserAccount=(SELECT AreaCode FROM BIZ_WorkFlows WHERE BizID=" + m_ObjID + " AND BizStep=" + msgStep.ToString() + ")");
                if (!string.IsNullOrEmpty(nextUserTel) && nextUserTel.Length == 11) SendMsg.SendMsgByModem(nextUserTel, appNames + "�������[" + m_FuncName + "]�ѳ���,����ˡ�");
            }
        }

        /// <summary>
        /// �Զ�ͬ������Ա��ϵ�绰
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
        /// �����ӡ��֤����¼
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

                // ����֤����
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
        /// ���̽ڵ��Ƿ��漰�������
        /// </summary>
        /// <param name="bizID">ҵ��������</param>
        /// <param name="curStep">��ǰ������</param>
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
        /// ����ȡ�þ�����
        /// </summary>
        /// <param name="areaCode">����</param>
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
            ListItem li = new ListItem("--��ѡ��ǩ��--", "");
            DDLApproval.Items.Insert(0, li);
            li = null;
            //DDLApproval.Items.Insert(0, new ListItem(m_SiteAreaNa, m_SiteArea));
            tmpDt = null;
        }
        /// <summary>
        /// ����ȡ��������
        /// </summary>
        /// <param name="areaCode">����</param>
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
            ListItem li = new ListItem("--��ѡ��ǩ��--", "");
            DDLAuditUser.Items.Insert(0, li);
            li = null;
            //DDLApproval.Items.Insert(0, new ListItem(m_SiteAreaNa, m_SiteArea));
            tmpDt = null;
        }
    }
}

