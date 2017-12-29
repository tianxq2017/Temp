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
    public partial class BizCerNo : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                ShowBizInfo(m_ObjID);
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
        /// �鿴��ϸ��Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void ShowBizInfo(string bizID)
        {
            string bizName, initName, bizCode = string.Empty;
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT * FROM BIZ_Contents WHERE BizID=" + bizID;
                /*
SELECT [
BizID,BizCode,BizName,CurrentStep,CurrentStepNa,PersonID,PersonPhotos,PersonCidA,PersonCidB,AddressID,ContactTelA,ContactTelB,
RegAreaCodeA,RegAreaCodeB,RegAreaNameA,RegAreaNameB,CurAreaCodeA,CurAreaCodeB,CurAreaNameA,CurAreaNameB,SelAreaCode,SelAreaName,
Initiator,InitDirection,StartDate,FinalDate,Attribs,
Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19,Fileds20,Fileds21,Fileds22,Fileds23,Fileds24,Fileds25,Fileds26,Fileds27,Fileds28,Fileds29,Fileds30,Fileds31,Fileds32,Fileds33,Fileds34,Fileds35,Fileds36,Fileds37,Fileds38,Fileds39,Fileds40,Fileds41,Fileds42,Fileds43,Fileds44,Fileds45,Fileds46,Fileds47,Fileds48,Fileds49,Fileds50
                 * ] FROM [YN_ChuXiong_OnlineCertificate_DB].[dbo].[BIZ_Contents] 
                 */
                /*
                 */
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    bizName = sdr["BizName"].ToString();
                    initName = sdr["Fileds01"].ToString() + "��" + sdr["Fileds08"].ToString();
                    bizCode = sdr["BizCode"].ToString();

                    GetWorkFlows(bizID, bizName, initName, bizCode);

                    if (m_ActionName == "priint") SetCertificateLog(bizID, sdr["BizCode"].ToString(), sdr["BizName"].ToString(), sdr["Fileds01"].ToString(), sdr["PersonCidA"].ToString()); //�����ӡ��֤��
                }
                sdr.Close();

            }
            catch { if (sdr != null) sdr.Close(); }
        }

        /// <summary>
        /// �����ӡ��֤����¼ 1�����; 2֤��; 3�ռ���ִ��; 4����֪ͨ��;5����֤֪ͨ
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="BizCode"></param>
        /// <param name="BizName"></param>
        /// <param name="pName"></param>
        /// <param name="pCid"></param>
        private void SetCertificateLog(string bizID, string BizCode, string BizName, string pName, string pCid)
        {
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT TOP 1 AreaCode,AreaName,DeptName,Approval,CertificateNoA,CertificateNoB,Attribs,CertificateDateStart,CertificateDateEnd FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep DESC";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    string CertificateNo = sdr["CertificateNoA"].ToString();
                    string CertificateName = "���跢֤֪ͨ��";
                    string CertificateGovName = sdr["AreaName"].ToString();
                    string StartDate = sdr["CertificateDateStart"].ToString();
                    string AreaCode = sdr["AreaCode"].ToString();

                    m_SqlParams = "SELECT COUNT(*) FROM BIZ_Certificates WHERE CertificateType=5 AND BizCode='" + BizCode + "' AND BizID=" + bizID ;
                    if (CommPage.GetSingleVal(m_SqlParams) == "0") {
                        m_SqlParams = "INSERT INTO BIZ_Certificates (";
                        m_SqlParams += "BizID,BizCode,BizName,CertificateNo,CertificateName,AreaCode,";
                        m_SqlParams += "PersonName,PersonCid,CertificateGovName,StartDate,CertificateType";
                        m_SqlParams += ") VALUES(";
                        m_SqlParams += "" + bizID + ",'" + BizCode + "','" + BizName + "','-','" + CertificateName + "','" + AreaCode + "',";
                        m_SqlParams += "'" + pName + "','" + pCid + "','" + CertificateGovName + "','" + StartDate + "',5";
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

        private string GetBzFlowKeyWords(string bizCode)
        {
            string cYear = "", nYear = "", flowNo = "";
            cYear = DateTime.Now.ToString("yyyy");
            nYear = DateTime.Now.AddYears(1).ToString("yyyy");
            flowNo = CommPage.GetSingleVal("SELECT COUNT(*)+1 FROM BIZ_Certificates WHERE BizCode='" + bizCode + "' AND CertificateType=5 AND CreateDate >'" + cYear + "/01/01 00:00:00' AND CreateDate <'" + nYear + "/01/01 00:00:00'");
            return "��" + cYear + "��" + flowNo.PadLeft(3, '0') + " ��";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="curAreaName"></param>
        /// <returns></returns>
        private void GetWorkFlows(string bizID, string bizName, string appUserName, string bizCode)
        {
            string AreaCode = "",areName=string.Empty;
            int bizStep = 0;
            StringBuilder s = new StringBuilder();
            DataTable dt = new DataTable();
            try
            {
                // // Attribs: 0,δͨ��1,��˳ɹ�
                //m_SqlParams = "SELECT DeptName,BizStepTotal,BizStep,Approval,Signature,OprateDate,CertificateNoA,CertificateNoB,CertificateMemo FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep";
                m_SqlParams = "SELECT TOP 1 AreaCode,DeptName,BizStepTotal,BizStep,Approval,Signature,OprateDate,CertificateNoA,CertificateNoB,CertificateMemo,Attribs FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep DESC";
                dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //bizStepTotal = dt.Rows[0]["BizStepTotal"].ToString();
                    //bizStep = int.Parse(bizStepTotal) - 1;
                    AreaCode = dt.Rows[0]["AreaCode"].ToString();
                    areName = CommPage.GetSingleValue("SELECT AreaName,ParentCode FROM AreaDetailCN WHERE AreaCode='" + AreaCode + "'");
                    s.Append("<div class=\"title\">");
                    s.Append("<div class=\"a1\">" + dt.Rows[0]["DeptName"].ToString() + "</div>");
                    s.Append("<div class=\"a2\">���跢֤֪ͨ��</div>");
                    s.Append("<div class=\"a3\">" + GetBzFlowKeyWords(bizCode) + "</div>");//���
                    s.Append("</div>");
                    s.Append("<div class=\"content\">");
                    s.Append("<div class=\"name\"><span>" + appUserName + "</span>�� ��</div>");
                    s.Append("<div class=\"sum\">");
                    s.Append("<p>�������span>" + bizName + "</span>ҵ�����������Ϥ������飬��򸾲����ϡ����ɹ��������˿���ƻ������������涨���������������ݡ����ɹ�������<����֤>����취���Ĺ涨���������跢֤��</p>");
                    s.Append("<p>������������������յ���֪ͨ��֮����60������<!--<span style=\"min-width:100px;\"></span>-->" + areName + "����������ͨ���������ͼƻ�����ίԱ�������������飬Ҳ�������յ���֪ͨ��֮��������������<!--<span style=\"min-width:100px;\"></span>-->" + areName + "����Ժ�����������ϡ�</p>");
                    s.Append("<p>�ش�֪ͨ</p>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"bottom\">");
                    s.Append("<div class=\"official\">" + dt.Rows[0]["DeptName"].ToString() + "�����£�</div>");
                    s.Append("<div class=\"time\">" + DateTime.Now.ToString("yyyy��MM��dd��") + "</div>");
                    s.Append("</div>");
                }
                else
                {
                    s.Append("<div class=\"tr_07\"><br/>���̽ڵ����Ԥ�ƴ���<br/><br/></div>");
                }
                dt = null;
            }
            catch
            {
                s.Append("<div class=\"tr_07\"><br/>��ȡ���̽ڵ������Ϣʱ��������<br/><br/></div>");
            }

            this.LiteralBizInfo.Text = s.ToString(); ;
        }

        private string GetDateFormat(string inStr)
        {
            string returnVal = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(inStr))
                {
                    returnVal = DateTime.Parse(inStr).ToString("yyyy��MM��dd��");
                }
            }
            catch { returnVal = inStr; }
            return returnVal;
        }


        #endregion
    }
}

