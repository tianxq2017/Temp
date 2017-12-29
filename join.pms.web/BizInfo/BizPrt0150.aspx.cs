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
    public partial class BizPrt0150 : System.Web.UI.Page
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
                    //if (m_ActionName == "print" && PageValidate.IsNumber(m_ObjID))
                    //{
                    //    object m_QcfwzBm = DbHelperSQL.GetSingle("SELECT CertificateNoA FROM BIZ_WorkFlows WHERE  CertificateNoA !='' and  Attribs IN(1) and BizID=" + m_ObjID + " ");

                    //    if (m_QcfwzBm != null)
                    //    {
                    //        Response.Redirect("/BizInfo/BizPrt01501.aspx?action=view1&mp=1&fwzh=" + m_QcfwzBm.ToString() + "&sourceUrl=" + m_SourceUrl);
                    //    }
                    //}
                    //else
                    //{
                        ShowBizInfo(m_ObjID);
                        if (m_ActionName == "view" || m_ActionName == "viewDetails")
                        {
                            // ��ȡҵ�����֤����Ϣ
                            BIZ_Docs doc = new BIZ_Docs();
                            this.LiteralDocs.Text = doc.GetBizDocsForView(m_ObjID);
                            doc = null;
                        }
                    //}
                }
            }
        }

        #region
        /// <summary>
        /// ������֤
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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/loginTemp.aspx';</script>");
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

                    workFlowInfo = GetWorkFlows(bizID, biz.CurAreaNameA, biz.Attribs, ref cerNo, biz.Fileds36);

                    //s.Append("<div class=\"number\">��ţ�" + cerNo + "</div>");
                    s.Append("<div class=\"print_table\">");
                    //s.Append("<div class=\"tr_01 tr_01a clearfix\">");
                    //s.Append("<!--<div class=\"code\"><img src=\"images/ls/photo_0104.gif\" /></div> -->");
                    //s.Append("<div class=\"photo\">" + photos + "</div>");
                    //Ů����Ϣ
                    s.Append("<div class=\"tr_01 tr_01a clearfix\" style=\"padding-top:68px;\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds08 + "</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(biz.Fileds31, "") + "</div>");
                    s.Append("<div class=\"td_03\">" + biz.PersonCidB + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds10 + "</div>");
                    s.Append("<div class=\"td_02 td_left\">" + biz.Fileds12 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03\">");
                    s.Append("<div class=\"td_01 td_left\">" + biz.RegAreaNameB.Replace("&", " ") + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03\">");
                    s.Append("<div class=\"td_01 td_left\">" + biz.CurAreaNameB.Replace("&", " ") + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03\">");
                    s.Append("<div class=\"td_01 td_left\">" + biz.Fileds13 + "</div>");
                    s.Append("</div>");

                    //�з���Ϣ
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds01 + "</div>");
                    s.Append("<div class=\"td_02\">" + GetDateFormat(biz.Fileds32, "") + "</div>");
                    s.Append("<div class=\"td_03\">" + biz.PersonCidA + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds03 + "</div>");
                    s.Append("<div class=\"td_02 td_left\">" + biz.Fileds05 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03\">");
                    s.Append("<div class=\"td_01 td_left\">" + biz.RegAreaNameA.Replace("&", " ") + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03\">");
                    s.Append("<div class=\"td_01 td_left\">" + biz.CurAreaNameA.Replace("&", " ") + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03\">");
                    s.Append("<div class=\"td_01 td_left\">" + biz.Fileds33 + "</div>");
                    s.Append("</div>");

                    //�������в���Ϣ
                    s.Append("<div class=\"tr_04 clearfix\">");
                    s.Append("<div class=\"td_01 td_left\">" + biz.Fileds47 + "</div>");
                    s.Append("<div class=\"td_02 td_left\">" + GetDateFormat(biz.Fileds14, "") + "</div>");
                    s.Append("</div>");

                    //�������
                    s.Append(workFlowInfo);

                    s.Append("<div class=\"tr_05 clearfix\">");
                    s.Append("<div class=\"td_01 td_left\">" + cerNo + "</div>");
                    //if (m_ActionName == "priint")
                    //{
                        s.Append("<div class=\"td_02 td_left\">" + GetDateFormat(DateTime.Now.ToString(), "1") + " </div>");
                    //}
                    //else
                    //{
                        //s.Append("<div class=\"td_02\"></div>");
                    //}
                    s.Append("</div>");

                    s.Append(" </div>");
                    if (m_ActionName == "priint")
                    {
                        CommPage.SetBizLog(bizID, m_UserID, "ҵ���ӡ", "�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " ��ӡ�˶��������ǼǱ�");
                        //�����ӡ��֤��
                        join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                        log.BizID = bizID;
                        log.BizCode = biz.BizCode;
                        log.SetCertificateLog(biz.BizName, biz.Fileds01, biz.PersonCidA, "��ӡ�˶��������ǼǱ�");
                        log = null;
                    }
                }
                biz = null;
            }
            catch { }

            this.LiteralBizInfo.Text = s.ToString();
        }

        /// <summary>
        /// ȡ������ǩ������
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="curAreaName"></param>
        /// <returns></returns>
        private string GetWorkFlows(string bizID, string curAreaName, string attribs, ref string cerNo, string yjDate)
        {
            string OprateDate1 = string.Empty;
            string seal1 = string.Empty;
            string tel1 = string.Empty;
            string sign1 = string.Empty;
            string CertificateDateStart, CertificateDateEnd = string.Empty;
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

                    b.Append("<div class=\"seal_02 clearfix\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[0]["Comments"].ToString() + "</div>");
                    b.Append("<div class=\"td_02\">�����ˣ�" + sign1 + "&nbsp;" + OprateDate1 + "<br />�绰��" + tel1 + "</div>");
                    b.Append("<div class=\"official\">" + seal1 + "</div>");
                    b.Append("</div>");
                }
                else
                {
                    b.Append("<div class=\"seal_02 clearfix\">");
                    b.Append("<div class=\"td_01\">&nbsp;</div>");
                    b.Append("<div class=\"td_02\">�����ˣ�&nbsp;<br />�绰��</div>");
                    b.Append("<div class=\"official\">&nbsp;&nbsp;</div>");
                    b.Append("</div>");
                }
                dt = null;
            }
            catch
            {
                b.Append("<div class=\"tr_05 clearfix\"><br/>��ȡ���̽ڵ������Ϣʱ��������<br/><br/></div>");
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
