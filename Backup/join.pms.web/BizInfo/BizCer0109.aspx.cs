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
    public partial class BizCer0109 : System.Web.UI.Page
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
        }
        #endregion

        /// <summary>
        /// �鿴��ϸ��Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void ShowBizInfo(string bizID)
        {
            string workFlowsID = "", CertificateNum = "", Approval = "", CertificateAddress = "", ApprovalSignName = string.Empty;
            string Signature = "", GovTel = "", QRCodeFiles = string.Empty;
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
                    ApprovalSignName = bwf.ApprovalSignName;
                    CertificateAddress = bwf.AreaName;
                    Signature = bwf.Signature;
                    GovTel = bwf.ApprovalUserTel;
                    workFlowsID = bwf.WorkFlowsID;
                    QRCodeFiles = bwf.QRCodeFiles;
                    //���ɶ�ά��
                    if (!string.IsNullOrEmpty(workFlowsID) && string.IsNullOrEmpty(QRCodeFiles))
                    {
                        string optateDate = bwf.OprateDate;
                        if (!string.IsNullOrEmpty(optateDate)) { optateDate = DateTime.Parse(optateDate).ToString("yyyy��MM��dd��"); }
                        else { optateDate = DateTime.Now.ToString("yyyy��MM��dd��"); }
                        string qrInfo = "�����˿ڻ���֤������֤���������֤�ţ�" + biz.Fileds01 + "(" + biz.PersonCidA + ")����ż����" + biz.Fileds08 + "��������ڣ�" + optateDate;
                        QRCode qr = new QRCode();
                        if (qr.SetQrCode(qrInfo, Server.MapPath("/"), ref QRCodeFiles))
                        {
                            bwf.SettWorkFlowsQrCode(workFlowsID, QRCodeFiles);
                        }
                        qr = null;
                    }
                    bwf = null;
                    //�ǼǱ�
                    s.Append("<div class=\"paper_print_0109a\">");
                    s.Append("<div class=\"title\">��֤��¼</div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg clearfix\">");
                    s.Append("<div class=\"tr_01\">");
                    s.Append("<p>֤�ţ�<b>" + CertificateNum + "</b></p>");
                    s.Append("<p>��֤��λ�����£���</p>");
                    s.Append("<p>��֤���ص�ַ��" + CertificateAddress + "</p>");
                    s.Append("<p>�����ˣ�" + ApprovalSignName + "</p>");
                    s.Append("<p>��֤���ڣ�" + GetBuDaInfo(bizID, biz.Attribs) + "</p>");
                    s.Append("<p>��Ч�ڣ���" + DateTime.Now.AddYears(3).ToString("yyyy-MM-dd") + "ֹ</p>");
                    s.Append("<p>�ʱࣺ028200&nbsp;&nbsp;&nbsp;&nbsp;�绰��" + GovTel + "</p>");
                    s.Append("<p>��֤�˻����ؼ������ŵ绰���򼶣���" + GetTelByArea(biz.RegAreaCodeA) + "</p>");
                    s.Append("<p>��ż�����ؼ������ŵ绰���򼶣���" + GetTelByArea(biz.RegAreaCodeB) + "</p>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02\">");
                    s.Append("<div class=\"photo\"><img src=\"" + biz.PersonPhotos + "\" /></div>");
                    s.Append("<div class=\"code\"><img src=\"" + QRCodeFiles + "\" /><p>�ֻ�ɨ���ά��</p></div>");
                    s.Append("<div class=\"official\"><img src=\"" + Signature + "\"  height=\"165\"  /></div>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<p><img src=\"/BizInfo/images/info/paper_0109a.gif\" /></p>");
                    s.Append("</div>");
                    //�������
                    string boyNum = "", girlNum = "";
                    string birthTotal = GetChildNum(bizID, ref boyNum, ref girlNum);
                    s.Append("<div class=\"paper_print_0109b\">");
                    s.Append("<div class=\"title\">�������</div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<div class=\"tr_01 clearfix\" style=\"padding-top:49px;\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds01 + "</div>");//��֤������
                    s.Append("<div class=\"td_02\">" + BIZ_Common.FormatString(biz.Fileds32, "2") + "</div>");//����
                    s.Append("<div class=\"td_03\">" + GetSexByID(biz.PersonCidA) + "</div>");//�Ա�
                    s.Append("<div class=\"td_04\">" + GetAgeByID(biz.PersonCidA) + "</div>");//����
                    s.Append("<div class=\"td_05\">" + biz.Fileds33 + "</div>");//����״��
                    s.Append("<div class=\"td_06\">" + BIZ_Common.FormatString(biz.Fileds34, "2") + "</div>");//�������
                    s.Append("<div class=\"td_07\">" + biz.PersonCidA + "</div>");//���֤��
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02\">");
                    s.Append("<div class=\"td_01\">" + biz.RegAreaNameA + "</div>");//������
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds08 + "</div>");//��ż����
                    s.Append("<div class=\"td_02\">" + biz.RegAreaNameB + "</div>");//��ż������
                    s.Append("</div>");
                    s.Append("<div class=\"tr_04\">");
                    s.Append("<div class=\"td_01\">" + boyNum + "�� " + girlNum + "Ů</div>");
                    s.Append("<div class=\"td_02\">" + biz.Fileds33 + "</div>");
                    s.Append("<div class=\"td_03\">" + boyNum + "�� " + girlNum + "Ů</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_05\">");
                    s.Append("<div class=\"td_01\">" + biz.Fileds45 + "</div>");//���д�ʩ
                    s.Append("<div class=\"td_02\">" + CommPage.GetSingleVal("SELECT TOP 1 Memos FROM BIZ_PersonChildren WHERE BizID=" + bizID + " ORDER BY CommID") + "</div>");//������
                    s.Append("</div>");

                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<p><img src=\"/BizInfo/images/info/paper_0109b.gif\" /></p>");
                    s.Append("</div>");

                    s.Append("<div class=\"paper_print_0109c\">");
                    s.Append("<div class=\"title\">�־�ס�����������������������������¼</div>");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li>");
                    s.Append("<p class=\"td_01\">");
                    s.Append("���������<br />");
                    s.Append("���������<br />");
                    s.Append("���������<br />");
                    s.Append("���鵥λ�������£�<br />");
                    s.Append("�����ˣ�</p>");
                    s.Append("<p class=\"td_02\">��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</p>");
                    s.Append("</li>");
                     s.Append("<li>");
                    s.Append("<p class=\"td_01\">");
                    s.Append("���������<br />");
                    s.Append("���������<br />");
                    s.Append("���������<br />");
                    s.Append("���鵥λ�������£�<br />");
                    s.Append("�����ˣ�</p>");
                    s.Append("<p class=\"td_02\">��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</p>");
                    s.Append("</li>");
                     s.Append("<li>");
                    s.Append("<p class=\"td_01\">");
                    s.Append("���������<br />");
                    s.Append("���������<br />");
                    s.Append("���������<br />");
                    s.Append("���鵥λ�������£�<br />");
                    s.Append("�����ˣ�</p>");
                    s.Append("<p class=\"td_02\">��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</p>");
                    s.Append("</li>");
                     s.Append("<li>");
                    s.Append("<p class=\"td_01\">");
                    s.Append("���������<br />");
                    s.Append("���������<br />");
                    s.Append("���������<br />");
                    s.Append("���鵥λ�������£�<br />");
                    s.Append("�����ˣ�</p>");
                    s.Append("<p class=\"td_02\">��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</p>");
                    s.Append("</li>");
                     s.Append("<li>");
                    s.Append("<p class=\"td_01\">");
                    s.Append("���������<br />");
                    s.Append("���������<br />");
                    s.Append("���������<br />");
                    s.Append("���鵥λ�������£�<br />");
                    s.Append("�����ˣ�</p>");
                    s.Append("<p class=\"td_02\">��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</p>");
                    s.Append("</li>");
                     s.Append("<li>");
                    s.Append("<p class=\"td_01\">");
                    s.Append("���������<br />");
                    s.Append("���������<br />");
                    s.Append("���������<br />");
                    s.Append("���鵥λ�������£�<br />");
                    s.Append("�����ˣ�</p>");
                    s.Append("<p class=\"td_02\">��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��</p>");
                    s.Append("</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<p><img src=\"/BizInfo/images/info/paper_0109c.gif\" /></p>");
                    s.Append("</div>");
                }
                else
                {
                    s.Append("<div class=\"paper_print_0109a\"><p><img src=\"/BizInfo/images/info/paper_0109a.gif\" /></p></div>");
                }
                biz = null;
            }
            catch { }

            this.LiteralBizInfo.Text = s.ToString();
        }

        /// <summary>
        /// ��ȡ�������ػ����绰
        /// </summary>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        private string GetTelByArea(string areaCode) {
            try {
                if (!string.IsNullOrEmpty(areaCode))
                {
                    areaCode = areaCode.Substring(0, 9) + "000";
                    return CommPage.GetSingleVal("SELECT UserTel FROM USER_BaseInfo WHERE UserAreaCode='" + areaCode + "'");
                }
                else { return ""; }
            }
            catch {
                return "";
            }
        }

        #region ���֤����Ϣ����

        private string GetChildNum(string bizID,ref string boyNum,ref string girlNum) {
            string totalNum = CommPage.GetSingleVal("SELECT Count(*) FROM BIZ_PersonChildren WHERE BizID=" + bizID);
            boyNum = CommPage.GetSingleVal("SELECT Count(*) FROM BIZ_PersonChildren WHERE BizID=" + bizID + " AND ChildSex='��'");
            girlNum = (int.Parse(totalNum) - int.Parse(boyNum)).ToString();
            return totalNum;
        }

        /// <summary>
        /// ͨ�����֤�Ż�ȡ����
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static int GetAgeByID(string cid)
        {
            string uBirth = cid.Substring(6, 8).Insert(4, "-").Insert(7, "-");	//��ȡ����������
            TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(uBirth));
            return ts.Days / 365;
        }
        /// <summary>
        /// ͨ�����֤�Ż�ȡ����
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static string GetBirthdayByID(string cid)
        {
            //�Ա����Ϊż����Ů������Ϊ����
            string returnVal = string.Empty;
            if (cid.Length == 15)
            {
                returnVal = "19" + cid.Substring(6, 2) + "-" + cid.Substring(8, 2) + "-" + cid.Substring(10, 2);
            }
            else if (cid.Length == 18)
            {
                returnVal = cid.Substring(6, 8).Insert(4, "-").Insert(7, "-");
                //birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);
            }
            else
            {
                returnVal = "";
            }

            return returnVal;
        }
        /// <summary>
        /// ͨ�����֤�Ż�ȡ�Ա�
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static string GetSexByID(string cid)
        {
            string returnVal = string.Empty;
            if (cid.Length == 15)
            {
                returnVal = cid.Substring(12, 3);
                if (int.Parse(returnVal) % 2 == 0) { returnVal = "Ů"; }
                else { returnVal = "��"; }
            }
            else if (cid.Length == 18)
            {
                returnVal = cid.Substring(14, 3);
                if (int.Parse(returnVal) % 2 == 0) { returnVal = "Ů"; }
                else { returnVal = "��"; }
            }
            else
            {
                returnVal = "";
            }

            return returnVal;
        }
        /// <summary>
        /// ͨ�����֤�Ż�ȡ���պ��Ա�
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="userSex"></param>
        /// <returns></returns>
        public static string GetBirthdayAndSex(string cid, ref string userSex)
        {
            string returnVal = string.Empty;
            if (cid.Length == 15)
            {
                returnVal = "19" + cid.Substring(6, 2) + "-" + cid.Substring(8, 2) + "-" + cid.Substring(10, 2);
                userSex = cid.Substring(12, 3);
                if (int.Parse(userSex) % 2 == 0) { userSex = "Ů"; }
                else { userSex = "��"; }
            }
            else if (cid.Length == 18)
            {
                returnVal = cid.Substring(6, 8).Insert(4, "-").Insert(7, "-");
                //birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);
                userSex = cid.Substring(14, 3);
                if (int.Parse(userSex) % 2 == 0) { userSex = "Ů"; }
                else { userSex = "��"; }
            }
            else
            {
                returnVal = "";
                userSex = "";
            }

            return returnVal;
        }
        #endregion
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


