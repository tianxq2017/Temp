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
    public partial class BizCer0103 : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;

        private string m_ObjID;
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private iSvrs.DALSvrs m_DALSvrs;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                ShowBizInfo(m_ObjID);
            }
        }

        #region ҳ����֤��Ϣ
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
                //m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                //m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
                //m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }
            //if (m_ActionName == "view") m_ObjID = Request.QueryString["RID"];
        }
        #endregion

        /// <summary>
        /// �鿴��ϸ��Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void ShowBizInfo(string bizID)
        {
            string workFlowsID = "", CertificateNum = "", Approval  = string.Empty;
            string Signature = "", QRCodeFiles = "", Memos = string.Empty;
            StringBuilder s = new StringBuilder();
            try
            {
                join.pms.dal.BIZ_Contents biz = new join.pms.dal.BIZ_Contents();
                biz.SearchWhere = "BizID=" + bizID;
                biz.SelectAll(false);
                if (!string.IsNullOrEmpty(biz.BizID))
                {
                    join.pms.dal.BizWorkFlows bwf = new join.pms.dal.BizWorkFlows();
                    bwf.BizID = bizID;
                    bwf.FilterSQL = "BizID=" + bizID + " ORDER BY BizStep";
                    bwf.GetWorkFlowsByBizID();
                    CertificateNum = bwf.CertificateNoA;
                    Approval = bwf.Approval;
                    Signature = bwf.Signature;
                    workFlowsID = bwf.WorkFlowsID;
                    QRCodeFiles = bwf.QRCodeFiles;
                    Memos = bwf.Comments;
                    //���ɶ�ά��
                    if (!string.IsNullOrEmpty(workFlowsID) && string.IsNullOrEmpty(QRCodeFiles))
                    {
                        string optateDate = bwf.OprateDate;
                        if (!string.IsNullOrEmpty(optateDate)) { optateDate = DateTime.Parse(optateDate).ToString("yyyy��MM��dd��"); }
                        else { optateDate = DateTime.Now.ToString("yyyy��MM��dd��"); }
                        string qrInfo = "��֤��������" + biz.Fileds01 + "�����֤�ţ�" + biz.PersonCidA + "��������ڣ�" + optateDate;
                        QRCode qr = new QRCode();
                        if (qr.SetQrCode(qrInfo, Server.MapPath("/"), ref QRCodeFiles))
                        {
                            bwf.SettWorkFlowsQrCode(workFlowsID, QRCodeFiles);
                        }
                        qr = null;
                    }
                    bwf = null;
                    //�ǼǱ�
                    s.Append("<div class=\"paper_print_0103a\">");
                    s.Append("<div class=\"title\">ũ�岿�ּƻ�������ͥ������������֤</div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<div class=\"tr_02\">");
                    s.Append("<div class=\"photo\"><img src=\"" + biz.PersonPhotos + "\" /></div>");//������
                    s.Append("<div class=\"code\"><img src=\"" + QRCodeFiles + "\" /><p>�ֻ�ɨ���ά��</p></div>");//��ά��
                    s.Append("<div class=\"official\"><img src=\"" + Signature + "\"  height=\"165\" /></div>");//����
                    s.Append("</div>");

                    s.Append("<div class=\"tr_01 clearfix\" style=\"padding-top:28px;\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds01 + "</div>");//����
                    s.Append("<div class=\"td_02\">" + biz.Fileds02 + "</div>");//�Ա�
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds03 + "</div>");//����
                    s.Append("<div class=\"td_02\">" + BIZ_Common.FormatString(biz.Fileds32, "2") + "</div>");//����
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01\">");
                    s.Append("<div class=\"td_03\">" + biz.PersonCidA + "</div>");//cid
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01\">");
                    s.Append("<div class=\"td_03\">" + biz.RegAreaNameA + "</div>");//������
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<p><img src=\"/BizInfo/images/info/paper_0103a.gif\" /></p>");
                    s.Append("<div class=\"bottom\">");
                    s.Append("<span class=\"a3\">֤����ţ�" + CertificateNum + "</span>");
                    s.Append("<span class=\"a1\">��֤���ڣ�" + GetBuDaInfo(bizID, biz.Attribs) + "</span>");
                    s.Append("<span class=\"a2\">��֤��λ�����£�</span>");
                    s.Append("</div>");
                    s.Append("</div>");

                    s.Append("<div class=\"paper_print_0103b\">");
                    s.Append("<div class=\"title\">�����¼</div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<p><img src=\"/BizInfo/images/info/paper_0103b.gif\" /></p>");
                    s.Append("</div>");

                    s.Append("<div class=\"paper_print_0103b paper_print_0103c\">");
                    s.Append("<div class=\"title\">�����¼</div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">����Ǽǣ�ר���£���</p>");
                    s.Append("<p class=\"td_02\"><span>��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</span>�����ˣ�</p>");
                    s.Append("</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<p><img src=\"/BizInfo/images/info/paper_0103b.gif\" /></p>");
                    s.Append("</div>");

                }
                else
                {
                    s.Append("<div class=\"paper_print_0103a\"><p><img src=\"/BizInfo/images/info/paper_0103a.jpg\" /></p></div>");
                }
                biz = null;
            }
            catch { }

            this.LiteralBizInfo.Text = s.ToString();
        }
        /// <summary>
        /// ����֤��
        /// </summary>
        /// <param name="bizID"></param>
        /// <returns></returns>
        public string GetBuDaInfo(string bizID, string Attribs)
        {
            string returnVal = string.Empty;
            string printC = CommPage.GetSingleVal("SELECT COUNT(0) FROM BIZ_Certificates WHERE CertificateType=2 AND BizID=" + bizID);
            string createDate = CommPage.GetSingleVal("SELECT CreateDate FROM BIZ_Certificates WHERE CertificateType=2 AND BizID=" + bizID);
            if (int.Parse(printC) > 0 && Attribs == "8") { returnVal = BIZ_Common.FormatString(createDate, "2") + "����" + DateTime.Now.ToString("yyyy-MM-dd") + "��"; }
            else { returnVal = DateTime.Now.ToString("yyyy-MM-dd"); }
            return returnVal;
        }
    }
}
