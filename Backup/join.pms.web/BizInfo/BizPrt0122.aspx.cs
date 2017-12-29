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
    public partial class BizPrt0122 : System.Web.UI.Page
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
            StringBuilder s = new StringBuilder();
            try
            {
                BIZ_Contents biz = new BIZ_Contents();
                biz.BizID = bizID;
                biz.SearchWhere = "BizID=" + bizID;
                biz.SelectAll(false);
                if (!string.IsNullOrEmpty(biz.BizCode))
                {
                    // ��ȡ�з�Ů����Ƭ
                    photos = biz.PersonPhotos;
                    if (string.IsNullOrEmpty(photos))
                    {
                        //�����ƬΪ��
                        object DocsPhoto = DbHelperSQL.GetSingle("SELECT TOP 1 DocsPath FROM BIZ_Docs WHERE DocsName like '%��Ƭ%' and BizID =" + bizID + " order by CommID desc  ");
                        if (DocsPhoto != null)
                        {
                            string DocsPhotos = DocsPhoto.ToString();
                            photos = m_SvrsUrl + DocsPhotos;
                            m_SqlParams = "UPDATE BIZ_Contents SET PersonPhotos='" + DocsPhotos + "' WHERE BizID =" + bizID + "";
                            DbHelperSQL.ExecuteSql(m_SqlParams);
                        }
                    }
                    if (!string.IsNullOrEmpty(photos)) photos = "<img src=\"" + photos + "\" />";

                    workFlowInfo = GetWorkFlows(bizID, biz.CurAreaNameA, biz.Attribs, ref cerNo, biz.Fileds36, biz.SelAreaCode, biz.StartDate);

                    s.Append("<div class=\"number\">��ţ�" + cerNo + "</div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<!--<div class=\"code\"><img src=\"images/ls/photo_0104.gif\" /></div> -->");
                    s.Append("<div class=\"photo\">" + photos + "</div>");
                    //Ů����Ϣ
                    s.Append("<div class=\"tr_01 clearfix\" style=\"padding-top:47px;\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds08 + "</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(biz.Fileds31, "") + "</div>");
                    s.Append("<div class=\"td_03\">" + biz.Fileds10 + "</div>");
                    s.Append("<div class=\"td_04\">" + biz.PersonCidB + "</div>");
                    s.Append("<div class=\"td_05\">" + biz.Fileds13 + "</div>");
                    s.Append("<div class=\"td_06\">" + GetDateFormat(biz.Fileds14, "") + "</div>");
                    s.Append("</div>");
                    //�з���Ϣ
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds01 + "</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(biz.Fileds32, "") + "</div>");
                    s.Append("<div class=\"td_03\">" + biz.Fileds03 + "</div>");
                    s.Append("<div class=\"td_04\">" + biz.PersonCidA + "</div>");
                    s.Append("<div class=\"td_05\">" + biz.Fileds33 + "</div>");
                    s.Append("<div class=\"td_06\">" + GetDateFormat(biz.Fileds34, "") + "</div>");
                    s.Append("</div>");
                    //Ů��λ��
                    s.Append("<div class=\"tr_02 clearfix\" style=\"padding-top:35px;\">");
                    s.Append("<div class=\"td_01\">&nbsp;" + biz.Fileds12 + "</div>");
                    s.Append("<div class=\"td_02\">&nbsp;" + biz.RegAreaNameB + "</div>");
                    s.Append("<div class=\"td_03\">&nbsp;" + biz.Fileds11 + "</div>");
                    s.Append("<div class=\"td_04\">&nbsp;" + biz.Fileds16 + "</div>");
                    s.Append("</div>");
                    //�е�λ��
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">&nbsp;" + biz.Fileds05 + "</div>");
                    s.Append("<div class=\"td_02\">&nbsp;" + biz.RegAreaNameA + "</div>");
                    s.Append("<div class=\"td_03\">&nbsp;" + biz.Fileds04 + "</div>");
                    s.Append("<div class=\"td_04\">&nbsp;" + biz.Fileds17 + "</div>");
                    s.Append("</div>");

                    s.Append("<div class=\"tr_03 clearfix\">");
                    s.Append("<div class=\"td_01\">&nbsp;" + biz.CurAreaNameB + "</div>");
                    s.Append("<div class=\"td_02\">&nbsp;" + biz.ContactTelB + "</div>");
                    s.Append("</div>");

                    //��Ů��Ϣ
                    BIZ_PersonChildren children = new BIZ_PersonChildren();
                    children.Select("", bizID);

                    s.Append("<div class=\"tr_04 clearfix\" style=\"padding-top:36px;\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName1 + "</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(children.ChildBirthday1, "") + "</div>");
                    s.Append("<div class=\"td_03\">" + children.ChildSex1 + "</div>");
                    s.Append("<div class=\"td_04\">" + children.ChildCardID1 + "</div>");
                    s.Append("<div class=\"td_05\">" + children.FatherName1 + "</div>");
                    s.Append("<div class=\"td_06\">" + children.MotherName1 + "</div>");
                    s.Append("<div class=\"td_07\">" + children.ChildPolicy1 + "</div>");
                    s.Append("<div class=\"td_08\">" + children.Memos1 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_04 clearfix\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName2 + "</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(children.ChildBirthday2, "") + "</div>");
                    s.Append("<div class=\"td_03\">" + children.ChildSex2 + "</div>");
                    s.Append("<div class=\"td_04\">" + children.ChildCardID2 + "</div>");
                    s.Append("<div class=\"td_05\">" + children.FatherName2 + "</div>");
                    s.Append("<div class=\"td_06\">" + children.MotherName2 + "</div>");
                    s.Append("<div class=\"td_07\">" + children.ChildPolicy2 + "</div>");
                    s.Append("<div class=\"td_08\">" + children.Memos2 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_04 clearfix\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName3 + "</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(children.ChildBirthday3, "") + "</div>");
                    s.Append("<div class=\"td_03\">" + children.ChildSex3 + "</div>");
                    s.Append("<div class=\"td_04\">" + children.ChildCardID3 + "</div>");
                    s.Append("<div class=\"td_05\">" + children.FatherName3 + "</div>");
                    s.Append("<div class=\"td_06\">" + children.MotherName3 + "</div>");
                    s.Append("<div class=\"td_07\">" + children.ChildPolicy3 + "</div>");
                    s.Append("<div class=\"td_08\">" + children.Memos3 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_04 clearfix\" >");
                    s.Append("<div class=\"td_01\">" + children.ChildName4 + "</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(children.ChildBirthday4, "") + "</div>");
                    s.Append("<div class=\"td_03\">" + children.ChildSex4 + "</div>");
                    s.Append("<div class=\"td_04\">" + children.ChildCardID4 + "</div>");
                    s.Append("<div class=\"td_05\">" + children.FatherName4 + "</div>");
                    s.Append("<div class=\"td_06\">" + children.MotherName4 + "</div>");
                    s.Append("<div class=\"td_07\">" + children.ChildPolicy4 + "</div>");
                    s.Append("<div class=\"td_08\">" + children.Memos4 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_04 clearfix\" >");
                    s.Append("<div class=\"td_01\">" + children.ChildName5 + "</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(children.ChildBirthday5, "") + "</div>");
                    s.Append("<div class=\"td_03\">" + children.ChildSex5 + "</div>");
                    s.Append("<div class=\"td_04\">" + children.ChildCardID5 + "</div>");
                    s.Append("<div class=\"td_05\">" + children.FatherName5 + "</div>");
                    s.Append("<div class=\"td_06\">" + children.MotherName5 + "</div>");
                    s.Append("<div class=\"td_07\">" + children.ChildPolicy5 + "</div>");
                    s.Append("<div class=\"td_08\">" + children.Memos5 + "</div>");
                    s.Append("</div>");
                    children = null;

                    s.Append("<div class=\"tr_03 clearfix\">");
                    s.Append("<div class=\"td_01\">���ϡ����ɹ��������˿���ƻ������������涨������������" + biz.Fileds18 + "����</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(biz.Fileds19, "") + "</div>");
                    s.Append("</div>");

                    //�������
                    s.Append(workFlowInfo);

                    s.Append(" </div>");
                    s.Append(" </div>");
                    if (m_ActionName == "priint")
                    {
                        CommPage.SetBizLog(bizID, m_UserID, "ҵ���ӡ", "�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " ��ӡ��" + biz.Fileds18 + "���������ǼǱ�");
                        //�����ӡ��֤��
                        join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                        log.BizID = bizID;
                        log.BizCode = biz.BizCode;
                        log.SetCertificateLog(biz.BizName, biz.Fileds01, biz.PersonCidA, "��ӡ��" + biz.Fileds18 + "���������ǼǱ�");
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
        /// ȡ������ǩ������
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="curAreaName"></param>
        /// <returns></returns>
        private string GetWorkFlows(string bizID, string curAreaName, string attribs, ref string cerNo, string yjDate, string SelAreaCode, string StartDate)
        {
            string OprateDate1 = "", OprateDate2 = "", OprateDate3 = "", OprateDate4 = string.Empty;
            string seal1 = "", seal2 = "", seal3 = "", seal4 = string.Empty;
            string sign1 = "", sign2 = "", sign3 = "", sign33="",sign4="",sign44 = string.Empty;
            string CertificateDateStart, CertificateDateEnd = string.Empty;
            StringBuilder b = new StringBuilder();
            DataTable dt = new DataTable();
            try
            {
                m_SqlParams = "SELECT AreaName,Comments,Approval,AuditUser,Signature,OprateDate,CertificateNoA,CertificateNoB,CertificateMemo,CertificateDateStart,CertificateDateEnd,ApprovalSignPath,AuditUserSignPath FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep";
                dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (dt.Rows.Count == 4)
                {
                    OprateDate1 = GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "");
                    OprateDate2 = GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "");
                    OprateDate3 = GetDateFormat(dt.Rows[2]["OprateDate"].ToString(), "");
                    OprateDate4 = GetDateFormat(dt.Rows[3]["OprateDate"].ToString(), "");
                    seal1 = dt.Rows[0]["Signature"].ToString();
                    seal2 = dt.Rows[1]["Signature"].ToString();
                    seal3 = dt.Rows[2]["Signature"].ToString();
                    seal4 = dt.Rows[3]["Signature"].ToString();
                    if (!string.IsNullOrEmpty(seal1)) seal1 = "<img src=\"" + seal1 + "\"  height=\"165\" />";
                    if (!string.IsNullOrEmpty(seal2)) seal2 = "<img src=\"" + seal2 + "\"  height=\"165\" />";
                    if (!string.IsNullOrEmpty(seal3)) seal3 = "<img src=\"" + seal3 + "\"  height=\"165\" />";
                    if (!string.IsNullOrEmpty(seal4)) seal4 = "<img src=\"" + seal4 + "\"  height=\"165\" />";

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
                    if (!string.IsNullOrEmpty(sign4)) sign4 = "<img src=\"" + sign4 + "\"  height=\"25\" />";
                    if (!string.IsNullOrEmpty(sign44)) sign44 = "<img src=\"" + sign44 + "\"  height=\"25\" />";

                    cerNo = dt.Rows[3]["CertificateNoA"].ToString();

                    CertificateDateStart = GetDateFormat(dt.Rows[3]["CertificateDateStart"].ToString(), "");
                    CertificateDateEnd = GetDateFormat(dt.Rows[3]["CertificateDateEnd"].ToString(), "");

                    b.Append("<div class=\"tr_05 clearfix\">");
                    b.Append("<div class=\"tr_05_l\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[0]["Comments"].ToString() + "</div>");
                    //���ݿ���ϵͳ
                    if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">�����ˣ�" + dt.Rows[0]["Approval"].ToString() + "<br />" + OprateDate1 + "</div>"); }
                    else { b.Append("<div class=\"td_02\">�����ˣ�" + sign1 + "<br />" + OprateDate1 + "</div>"); }
                    //
                    
                    b.Append("<div class=\"official\">" + seal1 + "</div>");
                    b.Append("</div>");
                    b.Append("<div class=\"tr_05_r\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[1]["Comments"].ToString() + "</div>");
                    //���ݿ���ϵͳ
                    if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">�����ˣ�" + dt.Rows[1]["Approval"].ToString() + "<br />" + OprateDate2 + "</div>"); }
                    else { b.Append("<div class=\"td_02\">�����ˣ�" + sign2 + "<br />" + OprateDate2 + "</div>"); }
                    //
                    
                    b.Append("<div class=\"official\">" + seal2 + "</div>");
                    b.Append("</div>");
                    b.Append("</div>");
                    //������
                    b.Append("<div class=\"tr_05 tr_05b clearfix\">");
                    b.Append("<div class=\"tr_05_l\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[2]["Comments"].ToString() + "</div>");
                    //���ݿ���ϵͳ
                    if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">�����ˣ�" + dt.Rows[2]["Approval"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;�����ˣ�" + dt.Rows[2]["AuditUser"].ToString() + "<br />" + OprateDate3 + "</div>"); }
                    else { b.Append("<div class=\"td_02\">�����ˣ�" + sign3 + " <br /> �����ˣ�" + sign33 + " <br />" + OprateDate3 + "</div>"); }
                    //
                    
                    b.Append("<div class=\"official\">" + seal3 + "</div>");
                    b.Append("</div>");

                    //��
                    //if (attribs == "2" || attribs == "9")
                    //{
                    b.Append("<div class=\"tr_05_r\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[3]["Comments"].ToString() + "</div>");
                    //���ݿ���ϵͳ
                    if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">�����ˣ�" + dt.Rows[3]["Approval"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;�����ˣ�" + dt.Rows[3]["AuditUser"].ToString() + "<br />" + OprateDate4 + "</div>"); }
                    else { b.Append("<div class=\"td_02\">�����ˣ�" + sign4 + " <br /> �����ˣ�" + sign44 + "<br />" + OprateDate4 + "</div>"); }
                    
                    b.Append("<div class=\"official\">" + seal4 + "</div>");
                    b.Append("</div>");
                    //}
                    //else
                    //{
                    //    b.Append("<div class=\"tr_05_r\">");
                    //    b.Append("<div class=\"td_01\">&nbsp;&nbsp;&nbsp;&nbsp;</div>");
                    //    b.Append("<div class=\"td_02\">�����ˣ�&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�����ˣ�&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>");
                    //    b.Append("<div class=\"official\">&nbsp;</div>");
                    //    b.Append("</div>");
                    //}

                    b.Append("</div>");

                    b.Append("<div class=\"apply\">");
                    b.Append("<div class=\"td_01\">���������Ƿ���˫����Ը����ʵ�����ǳ�ŵ����������ɺ��ṩ�Ĳ�����ʵ����Ը��е���Ӧ�ķ������Ρ�</div>");

                    b.Append("<div class=\"clr10\"></div>");
                    b.Append("<div class=\"td_03\" style=\"float:right;pading-left:20px;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;</div>");
                    b.Append("<div class=\"td_02\" style=\"float:right;\">�����ˣ�ǩ������<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>");
                    b.Append("<div class=\"clr5\"></div>");
                    
                    b.Append("</div>");

                    if (!String.IsNullOrEmpty(CertificateDateStart) && !String.IsNullOrEmpty(CertificateDateEnd))
                    {
                        b.Append("<div class=\"tr_06\"><div class=\"td_01\">�������ޣ�" + CertificateDateStart + "��" + CertificateDateEnd + "</div></div>");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(yjDate)) { b.Append("<div class=\"tr_06\"><div class=\"td_01\">xx</div></div>"); }
                        else { b.Append("<div class=\"tr_06\"><div class=\"td_01\">ĩ���¾����ڣ�" + yjDate + "</div></div>"); }
                        
                    }
                }
                else
                {
                    b.Append("<div class=\"tr_05 clearfix\"><br/>���̽ڵ����Ԥ�ƴ���<br/><br/></div>");
                }
                dt = null;
            }
            catch
            {
                b.Append("<div class=\"tr_05 clearfix\"><br/>��ȡ���̽ڵ������Ϣʱ��������<br/><br/></div>");
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

