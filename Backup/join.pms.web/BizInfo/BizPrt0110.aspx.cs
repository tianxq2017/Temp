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
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_SqlParams;
        public string m_TargetUrl;
        public string m_SiteName = System.Configuration.ConfigurationManager.AppSettings["SiteName"];
        private string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];
        /// <summary>
        /// ҳ����ڻ�
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
                    // ����ɾ��,���� 
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
                        // ��ȡҵ�����֤����Ϣ
                        BIZ_Docs doc = new BIZ_Docs();
                        this.LiteralDocs.Text = doc.GetBizDocsForView(m_ObjID);
                        doc = null;
                    }
                }
            }
        }

        #region
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

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");

            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }
        /// <summary>
        /// ����ҵ��,����ҵ��
        /// </summary>
        /// <param name="bizID"></param>
        private void DelBiz(string bizID)
        {
            try
            {
                // BIZ_Contents --> Attribs: 0,��ʼ�ύ;1,����� 2,ͨ�� 3,���� 4,����,���� 5,ע�� 9,�鵵
                if (CommPage.CheckBizDelAttribs("BizID IN(" + bizID + ")"))
                {
                    m_SqlParams = "UPDATE BIZ_Contents SET Attribs=4 WHERE BizID IN(" + bizID + ")";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    CommPage.SetBizLog(bizID, m_UserID, "ҵ����", "�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " ������ҵ��(һ�������Ǽ�)��������");

                    MessageBox.ShowAndRedirect(this.Page, "������ʾ������ѡ���ҵ���������ɹ���", m_TargetUrl, true, true);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����Ⱥ�ڷ����ҵ������������Ѿ���ʼ��˵�ҵ�����ݽ�ֹ������������ѡ�񡰲�����������", m_TargetUrl, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��" + ex.Message, m_TargetUrl, true, true);
            }
        }
        /// <summary>
        /// ע��
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

                    CommPage.SetBizLog(bizID, m_UserID, "ҵ��ע��", "�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " ������ҵ��(һ�������Ǽ�)ע������");

                    MessageBox.ShowAndRedirect(this.Page, "������ʾ������ѡ���ҵ��ע�������ɹ���", m_TargetUrl, true, true);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��ֻ�������ϵ�ҵ��ſ���ִ�С�ע����������", m_TargetUrl, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��" + ex.Message, m_TargetUrl, true, true);
            }
        }
        /// <summary>
        /// �鿴��ϸ��Ϣ
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
                    qrInfo = "�������֤���������˼���ż���������֤�ţ�" + biz.Fileds01 + "(" + biz.PersonCidA + ")��" + biz.Fileds08 + "(" + biz.PersonCidB + ")��";
                    workFlowInfo = GetWorkFlows(bizID, biz.CurAreaNameA, biz.Attribs, ref cerNo, biz.BizStep, ref qrFiles, qrInfo, biz.SelAreaCode, biz.StartDate);
                    //workFlowInfo = GetWorkFlows(bizID, biz.CurAreaNameA, biz.Attribs, ref cerNo,biz.BizStep);

                    //s.Append("<div class=\"number\">��ţ�" + cerNo + "</div>");
                    s.Append("<div class=\"number\"></div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    if (!string.IsNullOrEmpty(qrFiles)) s.Append("<div class=\"code\"><img src=\"" + qrFiles + "\" /></div>");
                    //������Ϣ
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

                    //��ż��Ϣ
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

                    //��Ů��Ϣ
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
                        s.Append("<div class=\"td_04\">1��</div>");
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
                        s.Append("<div class=\"td_04\">2��</div>");
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
                        s.Append("<div class=\"td_04\">3��</div>");
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

                    //�������
                    s.Append(workFlowInfo);

                    s.Append("<div class=\"apply\">");
                    s.Append("<div class=\"td_01\">���������Ƿ���˫����Ը����ʵ�����ǳ�ŵ����������ɺ��ṩ�Ĳ�����ʵ����Ը��е���Ӧ�ķ������Ρ�</div>");

                    s.Append("<div class=\"clr10\"></div>");
                    s.Append("<div class=\"td_03\" style=\"float:right;pading-left:20px;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;</div>");
                    s.Append("<div class=\"td_02\" style=\"float:right;\">�����ˣ�ǩ������<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>");
                    s.Append("<div class=\"clr5\"></div>");

                    s.Append("</div>");

                    s.Append(" </div>");
                    s.Append(" </div>");
                    if (m_ActionName == "priint")
                    {
                        CommPage.SetBizLog(bizID, m_UserID, "ҵ���ӡ", "�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " ��ӡ�˻������֤��");
                        //�����ӡ��֤��
                        join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                        log.BizID = bizID;
                        log.BizCode = biz.BizCode;
                        log.SetCertificateLog(biz.BizName, biz.Fileds01, biz.PersonCidA, "��ӡ�˻������֤��");
                        log = null;
                    }
                }
                biz = null;
            }
            catch { }

            this.LiteralBizInfo.Text = s.ToString();
        }
        /// <summary>
        /// ȡ�û���״��
        /// </summary>
        /// <param name="instr"></param>
        /// <returns></returns>
        private string GetMarryStats(string instr)
        {

            if (string.IsNullOrEmpty(instr))
            {
                return "�ٻ�";
            }
            else
            {
                if (instr == "����") { return "�ٻ�"; }
                else { return instr; }
            }
        }
        /// <summary>
        /// ��ȡ������λ��סַ
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
        /// Attribs: 0��ʼ�ύ; 1�����; 2,ͨ�� 3����; 4ɾ��; 9,�鵵
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
                        //��
                        b.Append("<div class=\"tr_05 clearfix\">");
                        b.Append("<div class=\"tr_l\">");
                        b.Append("<div class=\"td_01\">" + dt.Rows[0]["Comments"].ToString() + "</div>");
                        //���ݿ���ϵͳ
                        if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">�����ˣ�" + dt.Rows[0]["Approval"].ToString() + "<br />��ϵ�绰��" + dt.Rows[0]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "1") + "</div>"); }
                        else { b.Append("<div class=\"td_02\">�����ˣ�" + sign1 + "<br />��ϵ�绰��" + dt.Rows[0]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "1") + "</div>"); }
                        //
                        
                        b.Append("<div class=\"official\">" + seal1 + "</div>");
                        b.Append("</div>");
                        //��
                        b.Append(" <div class=\"tr_c\">");
                        b.Append("<div class=\"td_01\">" + dt.Rows[1]["Comments"].ToString() + "</div>");
                        //���ݿ���ϵͳ
                        if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">�����ˣ�" + dt.Rows[1]["Approval"].ToString() + "<br />��ϵ�绰��" + dt.Rows[1]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "1") + "</div>"); }
                        else { b.Append("<div class=\"td_02\">�����ˣ�" + sign2 + "<br />��ϵ�绰��" + dt.Rows[1]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "1") + "</div>"); }
                        //
                        
                        b.Append("<div class=\"official\">" + seal2 + "</div>");
                        b.Append("</div>");

                        //��
                        //if (attribs == "2" || attribs == "9")
                        //{
                        b.Append("<div class=\"tr_r\">");
                        b.Append("<div class=\"td_01\">" + dt.Rows[2]["Comments"].ToString() + "</div>");
                        //���ݿ���ϵͳ
                        if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">�����ˣ�" + dt.Rows[2]["Approval"].ToString() + "<br />��ϵ�绰��" + dt.Rows[2]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[2]["OprateDate"].ToString(), "1") + "</div>"); }
                        else { b.Append("<div class=\"td_02\">�����ˣ�" + sign3 + "<br />��ϵ�绰��" + dt.Rows[2]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[2]["OprateDate"].ToString(), "1") + "</div>"); }
                        //
                        
                        b.Append(" <div class=\"official\">" + seal3 + "</div>");
                        b.Append("</div>");
                        //}
                        //else
                        //{
                        //    b.Append("<div class=\"tr_r\">");
                        //    b.Append("<div class=\"td_01\">&nbsp;&nbsp;&nbsp;&nbsp;</div>");
                        //    b.Append("<div class=\"td_02\">�����ˣ�<br />��ϵ�绰��</div>");
                        //    b.Append(" <div class=\"official\">&nbsp;</div>");
                        //    b.Append("</div>");
                        //}
                        b.Append("</div>");
                    }
                    else
                    {
                        b.Append("<div class=\"tr_04\"><br/>���̽ڵ����Ԥ�ƴ���<br/><br/></div>");
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

                        //��
                        b.Append("<div class=\"tr_05 clearfix\">");
                        b.Append("<div class=\"tr_l\">");
                        b.Append("<div class=\"td_01\">" + dt.Rows[0]["Comments"].ToString() + "</div>");
                        //���ݿ���ϵͳ
                        if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">�����ˣ�" + dt.Rows[0]["Approval"].ToString() + "<br />��ϵ�绰��" + dt.Rows[0]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "1") + "</div>"); }
                        else { b.Append("<div class=\"td_02\">�����ˣ�" + sign1 + "<br />��ϵ�绰��" + dt.Rows[0]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "1") + "</div>"); }
                        //
                        
                        b.Append("<div class=\"official\">" + seal1 + "</div>");
                        b.Append("</div>");
                        //��
                        b.Append(" <div class=\"tr_c\">");
                        b.Append("<div class=\"td_01\">" + dt.Rows[1]["Comments"].ToString() + "</div>");
                        //���ݿ���ϵͳ
                        if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">�����ˣ�" + dt.Rows[1]["Approval"].ToString() + "<br />��ϵ�绰��" + dt.Rows[1]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "1") + "</div>"); }
                        else { b.Append("<div class=\"td_02\">�����ˣ�" + sign2 + "<br />��ϵ�绰��" + dt.Rows[1]["ApprovalUserTel"].ToString() + "<br />" + GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "1") + "</div>"); }
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
                        b.Append("<div class=\"tr_04\"><br/>���̽ڵ����Ԥ�ƴ���<br/><br/></div>");
                    }
                }

                dt = null;

                // == ���ɶ�ά�� start====
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
                b.Append("<div class=\"tr_04\"><br/>��ȡ���̽ڵ������Ϣʱ��������<br/><br/></div>");
            }
            return b.ToString();
        }
        /// <summary>
        /// ��ʽ������
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
                    if (type == "1") { returnVal = DateTime.Parse(inStr).ToString("yyyy��MM��dd��"); }
                    else { returnVal = DateTime.Parse(inStr).ToString("yyyy-MM-dd"); }
                }
            }
            catch { returnVal = inStr; }
            return returnVal;
        }

        #endregion
    }
}
