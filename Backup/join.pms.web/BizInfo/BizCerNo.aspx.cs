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

        private string m_UserID; // 当前登录的操作用户编号

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
        /// 查看详细信息
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
                    initName = sdr["Fileds01"].ToString() + "和" + sdr["Fileds08"].ToString();
                    bizCode = sdr["BizCode"].ToString();

                    GetWorkFlows(bizID, bizName, initName, bizCode);

                    if (m_ActionName == "priint") SetCertificateLog(bizID, sdr["BizCode"].ToString(), sdr["BizName"].ToString(), sdr["Fileds01"].ToString(), sdr["PersonCidA"].ToString()); //保存打印的证件
                }
                sdr.Close();

            }
            catch { if (sdr != null) sdr.Close(); }
        }

        /// <summary>
        /// 保存打印的证件记录 1申请表; 2证件; 3收件回执单; 4补正通知书;5不发证通知
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
                    string CertificateName = "不予发证通知书";
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
            return "【" + cYear + "】" + flowNo.PadLeft(3, '0') + " 号";
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
                // // Attribs: 0,未通过1,审核成功
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
                    s.Append("<div class=\"a2\">不予发证通知书</div>");
                    s.Append("<div class=\"a3\">" + GetBzFlowKeyWords(bizCode) + "</div>");//编号
                    s.Append("</div>");
                    s.Append("<div class=\"content\">");
                    s.Append("<div class=\"name\"><span>" + appUserName + "</span>夫妇 ：</div>");
                    s.Append("<div class=\"sum\">");
                    s.Append("<p>你夫妇申请span>" + bizName + "</span>业务的申请已收悉。经审查，你夫妇不符合《内蒙古自治区人口与计划生育条例》规定的生育条件，根据《内蒙古自治区<生育证>管理办法》的规定，决定不予发证。</p>");
                    s.Append("<p>你夫妇若不服，可以自收到本通知书之日起60日内向<!--<span style=\"min-width:100px;\"></span>-->" + areName + "人民政府或通辽市卫生和计划生育委员会申请行政复议，也可以自收到本通知书之日起三个月内向<!--<span style=\"min-width:100px;\"></span>-->" + areName + "人民法院提起行政诉讼。</p>");
                    s.Append("<p>特此通知</p>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"bottom\">");
                    s.Append("<div class=\"official\">" + dt.Rows[0]["DeptName"].ToString() + "（盖章）</div>");
                    s.Append("<div class=\"time\">" + DateTime.Now.ToString("yyyy年MM月dd日") + "</div>");
                    s.Append("</div>");
                }
                else
                {
                    s.Append("<div class=\"tr_07\"><br/>流程节点参数预制错误！<br/><br/></div>");
                }
                dt = null;
            }
            catch
            {
                s.Append("<div class=\"tr_07\"><br/>获取流程节点审核信息时发生错误！<br/><br/></div>");
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
                    returnVal = DateTime.Parse(inStr).ToString("yyyy年MM月dd日");
                }
            }
            catch { returnVal = inStr; }
            return returnVal;
        }


        #endregion
    }
}

