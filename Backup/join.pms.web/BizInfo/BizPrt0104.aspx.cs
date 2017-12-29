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
    public partial class BizPrt0104 : System.Web.UI.Page
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
                    // ��ȡҵ�����֤����Ϣ
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

                    CommPage.SetBizLog(bizID, m_UserID, "ҵ����", "�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " ������ҵ��(�ƻ�������ͥ�ر��������������˱�)��������");

                    MessageBox.ShowAndRedirect(this.Page, "������ʾ������ѡ���ҵ���������ɹ���", m_TargetUrl, true, true);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ���Ѿ���ʼ��˵�ҵ�����ݽ�ֹ������������ѡ�񡰲�����������", m_TargetUrl, true, true);
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

                    CommPage.SetBizLog(bizID, m_UserID, "ҵ��ע��", "�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " ������ҵ��(�ƻ�������ͥ�ر��������������˱�)ע������");

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
        /*

         */

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

                    qrInfo = "�ƻ�������ͥ�ر��������������˱������˼���ż���������֤�ţ�" + biz.Fileds01 + "(" + biz.PersonCidA + ")��" + biz.Fileds08 + "(" + biz.PersonCidB + ")��";
                    workFlowInfo = GetWorkFlows(bizID, biz.CurAreaNameA, biz.Attribs, ref cerNo, ref qrFiles, qrInfo, biz.SelAreaCode, biz.StartDate);
                    this.LiteralAreaName.Text = "<div class=\"add\">" + BIZ_Common.GetAreaName(biz.SelAreaCode, "1") + "</div>";

                    s.Append("<div class=\"print_table_bg\">");
                    if (!string.IsNullOrEmpty(qrFiles)) s.Append("<div class=\"code\"><img src=\"" + qrFiles + "\" /></div>");

                    //һ����Ƭ
                    s.Append("<div class=\"photo\">" + photos + "</div>");

                    //������Ϣ
                    s.Append("<div class=\"tr_01 clearfix\" style=\"padding-top:42px;\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds01 + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.PersonCidA + "</div>");
                    s.Append("<div class=\"td_03\">" + biz.Fileds02 + "</div>");
                    s.Append("<div class=\"td_04\">" + GetDateFormat(biz.Fileds32, "") + "</div>");
                    s.Append("<div class=\"td_05\">" + biz.Fileds04 + "</div>");
                    s.Append("<div class=\"td_06\">" + biz.Fileds33 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds08 + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.PersonCidB + "</div>");
                    s.Append("<div class=\"td_03\">" + biz.Fileds09 + "</div>");
                    s.Append("<div class=\"td_04\">" + GetDateFormat(biz.Fileds31, "") + "</div>");
                    s.Append("<div class=\"td_05\">" + biz.Fileds11 + "</div>");
                    s.Append("<div class=\"td_06\">" + biz.Fileds13 + "</div>");
                    s.Append("</div>");

                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds20 + "�� " + biz.Fileds21 + "Ů</div>");
                    s.Append("<div class=\"td_02\">" + biz.Fileds37 + "�� " + biz.Fileds38 + "Ů</div>");
                    s.Append("<div class=\"td_03\">" + biz.Fileds22 + "</div>");
                    s.Append("</div>");

                    //��Ů��Ϣ
                    BIZ_PersonChildren children = new BIZ_PersonChildren();
                    children.Select(" ChildSource!='�Ǳ��˼���ż����' AND BizID=" + bizID);

                    s.Append("<div class=\"tr_03 clearfix\" style=\"padding-top:32px;\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName1 + "</div>");
                    s.Append("<div class=\"td_02\">" + children.ChildSex1 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(children.ChildBirthday1, "") + "</div>");
                    s.Append("<div class=\"td_04\" >" + children.ChildSource1 + "</div>");
                    s.Append("<div class=\"td_05\">" + children.ChildSurvivalStatus1 + "</div>");
                    s.Append("<div class=\"td_06\">" + GetDateFormat(children.ChildDeathday1, "") + "</div>");
                    s.Append("<div class=\"td_07\">" + children.ChildDeathAudit1 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03 clearfix\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName2 + "</div>");
                    s.Append("<div class=\"td_02\">" + children.ChildSex2 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(children.ChildBirthday2, "") + "</div>");
                    s.Append("<div class=\"td_04\">" + children.ChildSource2 + "</div>");
                    s.Append("<div class=\"td_05\">" + children.ChildSurvivalStatus2 + "</div>");
                    s.Append("<div class=\"td_06\">" + GetDateFormat(children.ChildDeathday2, "") + "</div>");
                    s.Append("<div class=\"td_07\">" + children.ChildDeathAudit2 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03 clearfix\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName3 + "</div>");
                    s.Append("<div class=\"td_02\">" + children.ChildSex3 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(children.ChildBirthday3, "") + "</div>");
                    s.Append("<div class=\"td_04\">" + children.ChildSource3 + "</div>");
                    s.Append("<div class=\"td_05\">" + children.ChildSurvivalStatus3 + "</div>");
                    s.Append("<div class=\"td_06\">" + GetDateFormat(children.ChildDeathday3, "") + "</div>");
                    s.Append("<div class=\"td_07\">" + children.ChildDeathAudit3 + "</div>");
                    s.Append("</div>");
                    children = null;

                    children = new BIZ_PersonChildren();
                    children.Select(" ChildSource='�Ǳ��˼���ż����' AND BizID=" + bizID);
                    s.Append("<div class=\"tr_03 clearfix\" style=\"padding-top:32px;\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName1 + "</div>");
                    s.Append("<div class=\"td_02\">" + children.ChildSex1 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(children.ChildBirthday1, "") + "</div>");
                    s.Append("<div class=\"td_04\">" + children.ChildSource1 + "</div>");
                    s.Append("<div class=\"td_05\">" + children.ChildSurvivalStatus1 + "</div>");
                    s.Append("<div class=\"td_06\">" + GetDateFormat(children.ChildDeathday1, "") + "</div>");
                    s.Append("<div class=\"td_07\">" + children.ChildDeathAudit1 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03 clearfix\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName2 + "</div>");
                    s.Append("<div class=\"td_02\">" + children.ChildSex2 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(children.ChildBirthday2, "") + "</div>");
                    s.Append("<div class=\"td_04\">" + children.ChildSource2 + "</div>");
                    s.Append("<div class=\"td_05\">" + children.ChildSurvivalStatus2 + "</div>");
                    s.Append("<div class=\"td_06\">" + GetDateFormat(children.ChildDeathday2, "") + "</div>");
                    s.Append("<div class=\"td_07\">" + children.ChildDeathAudit2 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03 clearfix\">");
                    s.Append("<div class=\"td_01\">" + children.ChildName3 + "</div>");
                    s.Append("<div class=\"td_02\">" + children.ChildSex3 + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(children.ChildBirthday3, "") + "</div>");
                    s.Append("<div class=\"td_04\">" + children.ChildSource3 + "</div>");
                    s.Append("<div class=\"td_05\">" + children.ChildSurvivalStatus3 + "</div>");
                    s.Append("<div class=\"td_06\">" + GetDateFormat(children.ChildDeathday3, "") + "</div>");
                    s.Append("<div class=\"td_07\">" + children.ChildDeathAudit3 + "</div>");
                    s.Append("</div>");
                    children = null;

                    //�˲���Ϣ
                    s.Append("<div class=\"tr_04 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds23 + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.Fileds24 + "</div>");
                    s.Append("<div class=\"td_03\">" + biz.Fileds26 + "</div>");
                    s.Append("</div>");


                    s.Append("<div class=\"tr_05 clearfix\">");
                    s.Append("<div class=\"td_01\" style=\"margin-top:-7px;\">" + biz.RegAreaNameA + "</div>");
                    s.Append("<div class=\"td_02\">" + biz.ContactTelA + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_05 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds18 + "</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(biz.Fileds19, "") + "</div>");
                    s.Append("</div>");

                    //�������
                    s.Append(workFlowInfo);

                    s.Append("</div>");

                    if (m_ActionName == "print")
                    {
                        CommPage.SetBizLog(bizID, m_UserID, "ҵ���ӡ", "�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " ��ӡ��ũ�岿�ּƻ�������ͥ�������������걨���");
                        //�����ӡ��֤��
                        join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                        log.BizID = bizID;
                        log.BizCode = biz.BizCode;
                        log.SetCertificateLog(biz.BizName, biz.Fileds01, biz.PersonCidA, "ũ�岿�ּƻ�������ͥ�������������걨���");
                        log = null;
                    }
                }
                biz = null;
            }
            catch { }

            this.LiteralBizInfo.Text = s.ToString(); ;
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
            string sign1 = "", sign2 = "", sign22 = "", sign3 = "", sign33 = string.Empty;
            string validDateEnd = string.Empty;
            StringBuilder b = new StringBuilder();
            DataTable dt = new DataTable();
            try
            {
                m_SqlParams = "SELECT AreaName,Comments,Approval,ApprovalUserTel,Signature,OprateDate,CertificateNoA,CertificateNoB,CertificateMemo,QRCodeFiles,AuditUser,ApprovalSignPath,AuditUserSignPath  FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep";
                dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (dt.Rows.Count == 3)
                {
                    seal1 = dt.Rows[0]["Signature"].ToString();
                    seal2 = dt.Rows[1]["Signature"].ToString();
                    seal3 = dt.Rows[2]["Signature"].ToString();
                    if (!string.IsNullOrEmpty(seal1)) seal1 = "<img src=\"" + seal1 + "\"   height=\"165\"/>";
                    if (!string.IsNullOrEmpty(seal2)) seal2 = "<img src=\"" + seal2 + "\"   height=\"165\"/>";
                    if (!string.IsNullOrEmpty(seal3)) seal3 = "<img src=\"" + seal3 + "\"   height=\"165\"/>";

                    sign1 = dt.Rows[0]["ApprovalSignPath"].ToString();
                    sign2 = dt.Rows[1]["ApprovalSignPath"].ToString();
                    sign22 = dt.Rows[1]["AuditUserSignPath"].ToString();
                    sign3 = dt.Rows[2]["ApprovalSignPath"].ToString();
                    sign33 = dt.Rows[2]["AuditUserSignPath"].ToString();
                    if (!string.IsNullOrEmpty(sign1)) sign1 = "<img src=\"" + sign1 + "\"  height=\"25\" />";
                    if (!string.IsNullOrEmpty(sign2)) sign2 = "<img src=\"" + sign2 + "\"  height=\"25\" />";
                    if (!string.IsNullOrEmpty(sign22)) sign22 = "<img src=\"" + sign22 + "\"  height=\"25\" />";
                    if (!string.IsNullOrEmpty(sign3)) sign3 = "<img src=\"" + sign3 + "\"  height=\"25\" />";
                    if (!string.IsNullOrEmpty(sign33)) sign33 = "<img src=\"" + sign33 + "\"  height=\"25\" />";

                    cerNo = dt.Rows[2]["CertificateNoA"].ToString();

                    //��
                    b.Append("<div class=\"tr_06 clearfix\">");
                    b.Append("<div class=\"tr_l\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[0]["Comments"].ToString() + "</div>");
                    //���ݿ���ϵͳ
                    if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">�����ˣ�" + dt.Rows[0]["Approval"].ToString() + "<br/>" + GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "") + "</div>"); }
                    else { b.Append("<div class=\"td_02\">�����ˣ�" + sign1 + "<br/>" + GetDateFormat(dt.Rows[0]["OprateDate"].ToString(), "") + "</div>"); }
                    
                    b.Append("<div class=\"official\">" + seal1 + "</div>");
                    b.Append("</div>");
                    //��
                    b.Append(" <div class=\"tr_r\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[1]["Comments"].ToString() + "</div>");
                    //���ݿ���ϵͳ
                    if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">�����ˣ�" + dt.Rows[1]["Approval"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;�����ˣ�" + dt.Rows[1]["AuditUser"].ToString() + "<br/>" + GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "") + "</div>"); }
                    else { b.Append("<div class=\"td_02\">�����ˣ�" + sign2 + "<br/>�����ˣ�" + sign22 + "<br/>" + GetDateFormat(dt.Rows[1]["OprateDate"].ToString(), "") + "</div>"); }
                    
                    b.Append("<div class=\"official\">" + seal2 + "</div>");
                    b.Append("</div>");
                    b.Append("</div>");
                    //��
                    //if (attribs == "2" || attribs == "9")
                    //{
                    b.Append("<div class=\"tr_07\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[2]["Comments"].ToString() + "��</div>");
                    //���ݿ���ϵͳ
                    if (int.Parse(bizID) < 3099 && SelAreaCode.Substring(0, 6) == "150524") { b.Append("<div class=\"td_02\">�����ˣ�" + dt.Rows[2]["Approval"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;�����ˣ�" + dt.Rows[2]["AuditUser"].ToString() + "<br/>" + GetDateFormat(dt.Rows[2]["OprateDate"].ToString(), "") + "</div>"); }
                    else { b.Append("<div class=\"td_02\">�����ˣ�" + sign3 + "&nbsp;&nbsp;&nbsp;&nbsp;�����ˣ�" + sign33 + "<br/>" + GetDateFormat(dt.Rows[2]["OprateDate"].ToString(), "") + "</div>"); }
                    
                    b.Append(" <div class=\"official\">" + seal3 + "</div>");
                    b.Append("</div>");
                    //}
                    //else
                    //{
                    //    b.Append("<div class=\"tr_07\">");
                    //    b.Append("<div class=\"td_01\">&nbsp;&nbsp;&nbsp;&nbsp;</div>");
                    //    b.Append("<div class=\"td_02\">�����ˣ�&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�����ˣ�&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>");
                    //    b.Append(" <div class=\"official\">&nbsp;</div>");
                    //    b.Append("</div>");
                    //}

                    b.Append("<div class=\"apply\">");
                    b.Append("<div class=\"td_01\">���������Ƿ���˫����Ը����ʵ�����ǳ�ŵ����������ɺ��ṩ�Ĳ�����ʵ����Ը��е���Ӧ�ķ������Ρ�</div>");

                    b.Append("<div class=\"clr10\"></div>");
                    b.Append("<div class=\"td_03\" style=\"float:right;pading-left:20px;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;</div>");
                    b.Append("<div class=\"td_02\" style=\"float:right;\">�����ˣ�ǩ������<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>");
                    b.Append("<div class=\"clr5\"></div>");

                    b.Append("</div>");

                    b.Append("<div class=\"tr_08\">");
                    b.Append("<div class=\"td_01\"></div>");
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

