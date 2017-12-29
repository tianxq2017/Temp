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
    public partial class BizPrt0107 : System.Web.UI.Page
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
                    qrInfo = "��һ���̡������������������������֤�ţ�" + biz.Fileds01 + "(" + biz.PersonCidA + ")��" + biz.Fileds08 + "(" + biz.PersonCidB + ")��";
                    workFlowInfo = GetWorkFlows(bizID, biz.CurAreaNameA, biz.Attribs, ref cerNo, ref qrFiles, qrInfo, biz.SelAreaCode, biz.StartDate);
                    //workFlowInfo = GetWorkFlows(bizID, biz.CurAreaNameA, biz.Attribs, ref cerNo);

                    this.LiteralAreaName.Text = "<div class=\"add\">" + BIZ_Common.GetAreaName(biz.SelAreaCode, "1") + "</div>";
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    if (!string.IsNullOrEmpty(qrFiles)) s.Append("<div class=\"code\"><img src=\"" + qrFiles + "\" /></div>");
                    //Ů����Ϣ
                    s.Append("<div class=\"tr_01 clearfix\" style=\"padding-top:14px;\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds08 + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.Fileds10 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(biz.Fileds31, "") + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_04\">&nbsp;" + biz.CurAreaNameB + "</div>");
	                s.Append("</div>");
	                s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds41 + "̥</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(biz.Fileds42, "") + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(biz.Fileds43, "") + "</div>");
	                s.Append("</div>");
	                s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds29 + "</div>");
                    s.Append("<div class=\"td_05\">" + biz.Fileds30 + "</div>");
                    s.Append("</div>");
                    //�з���Ϣ
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds01 + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.Fileds03 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(biz.Fileds32, "") + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_04\">&nbsp;" + biz.CurAreaNameA + "</div>");
	                s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds21 + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.ContactTelB + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(biz.Fileds14, "") + "</div>");
                    s.Append("</div>");
                    //��������
                    s.Append("<div class=\"tr_03 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds18 + "</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(biz.Fileds19, "") + "</div>");
                    s.Append("</div>");

                    //�������
                    s.Append(workFlowInfo);

                    s.Append(" </div>");
                    s.Append(" </div>");
                    if (m_ActionName == "priint")
                    {
                        CommPage.SetBizLog(bizID, m_UserID, "ҵ���ӡ", "�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " ��ӡ�ˡ�һ���̡�������������");
                        //�����ӡ��֤��
                        join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                        log.BizID = bizID;
                        log.BizCode = biz.BizCode;
                        log.SetCertificateLog(biz.BizName, biz.Fileds01, biz.PersonCidA, "��ӡ�ˡ�һ���̡�������������");
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
        private string GetWorkFlows(string bizID, string curAreaName, string attribs, ref string cerNo, ref string qrFiles, string qrInfo, string SelAreaCode, string StartDate)
        {
            string seal1 = "", seal2 = "", seal3 = string.Empty;
            string sign1 = "", sign2 = "", sign22 = string.Empty;
            string validDateEnd = string.Empty;
            StringBuilder b = new StringBuilder();
            DataTable dt = new DataTable();
            try
            {
                m_SqlParams = "SELECT AreaName,Comments,Approval,AuditUser,ApprovalUserTel,Signature,OprateDate,CertificateNoA,CertificateNoB,CertificateMemo,ApprovalSignPath,AuditUserSignPath FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep";
                dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (dt.Rows.Count == 2)
                {
                    seal1 = dt.Rows[0]["Signature"].ToString();
                    seal2 = dt.Rows[1]["Signature"].ToString();
                    //seal3 = dt.Rows[2]["Signature"].ToString();
                    if (!string.IsNullOrEmpty(seal1)) seal1 = "<img src=\"" + seal1 + "\"   height=\"165\"/>";
                    if (!string.IsNullOrEmpty(seal2)) seal2 = "<img src=\"" + seal2 + "\"   height=\"165\"/>";
                    //if (!string.IsNullOrEmpty(seal3)) seal3 = "<img src=\"" + seal3 + "\" />";
                    sign1 = dt.Rows[0]["ApprovalSignPath"].ToString();
                    sign2 = dt.Rows[1]["ApprovalSignPath"].ToString();
                    sign22 = dt.Rows[1]["AuditUserSignPath"].ToString();

                    if (!string.IsNullOrEmpty(sign1)) sign1 = "<img src=\"" + sign1 + "\"  height=\"25\" />";
                    if (!string.IsNullOrEmpty(sign2)) sign2 = "<img src=\"" + sign2 + "\"  height=\"25\" />";
                    if (!string.IsNullOrEmpty(sign22)) sign22 = "<img src=\"" + sign22 + "\"  height=\"25\" />";


                    cerNo = dt.Rows[1]["CertificateNoA"].ToString();
                    //Ů��
                    b.Append("<div class=\"tr_05 clearfix\">");
                    //b.Append("<div class=\"tr_l\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[0]["Comments"].ToString() + "</div>");
                    //���ݿ���ϵͳ
                    if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">�����ˣ�" + dt.Rows[0]["Approval"].ToString() + "<br />" + GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "1") + "</div>"); }
                    else { b.Append("<div class=\"td_02\">�����ˣ�" + sign1 + "<br />" + GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "1") + "</div>"); }
                    //
                    
                    b.Append("<div class=\"official\">" + seal1 + "</div>");
                    b.Append("</div>");

                    ////�д�
                    //b.Append("<div class=\"tr_r\">");
                    //b.Append("<div class=\"td_01\">" + dt.Rows[1]["Comments"].ToString() + "</div>");
                    //b.Append("<div class=\"td_02\">�����ˣ�" + dt.Rows[1]["Approval"].ToString() + "<br />" + GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "1") + "</div>");
                    //b.Append("<div class=\"official\">" + seal2 + "</div>");
                    //b.Append("</div>");
                    //b.Append("</div>");

                    //��
                    //if (attribs == "2" || attribs == "9")
                    //{
                    b.Append("<div class=\"tr_06\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[1]["Comments"].ToString() + "</div>");
                    //���ݿ���ϵͳ
                    if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">�����ˣ�" + dt.Rows[1]["Approval"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;�����ˣ�" + dt.Rows[1]["AuditUser"].ToString() + "<br />" + GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "1") + "</div>"); }
                    else { b.Append("<div class=\"td_02\">�����ˣ�" + sign2 + "&nbsp;&nbsp;&nbsp;&nbsp;�����ˣ�" + sign22 + "<br />" + GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "1") + "</div>"); }
                    //
                    
                    b.Append("<div class=\"official\">" + seal2 + "</div>");
                    b.Append("</div>");


                    b.Append("<div class=\"apply\">");
                    b.Append("<div class=\"td_01\">���������Ƿ���˫����Ը����ʵ�����ǳ�ŵ����������ɺ��ṩ�Ĳ�����ʵ����Ը��е���Ӧ�ķ������Ρ�</div>");

                    b.Append("<div class=\"clr10\"></div>");
                    b.Append("<div class=\"td_03\" style=\"float:right;pading-left:20px;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;</div>");
                    b.Append("<div class=\"td_02\" style=\"float:right;\">�����ˣ�ǩ������<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>");
                    b.Append("<div class=\"clr5\"></div>");

                    b.Append("</div>");

                    b.Append("<div class=\"tr_07\">");
                    b.Append("<div class=\"td_01\">&nbsp;</div>");
                    b.Append("</div>");

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
                else
                {
                    b.Append("<div class=\"tr_04\"><br/>���̽ڵ����Ԥ�ƴ���<br/><br/></div>");
                }
                dt = null;
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
