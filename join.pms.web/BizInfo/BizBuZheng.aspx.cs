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
    public partial class BizBuZheng : System.Web.UI.Page
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
                if (PageValidate.IsNumber(m_ObjID)) { ShowBizInfo(m_ObjID); }
                else
                {
                    Response.Write("非法访问，操作被终止！");
                    Response.End();
                }
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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/loginTemp.aspx';</script>");
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
            string bizName, initName = string.Empty;
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT * FROM BIZ_Contents WHERE BizID=" + bizID;

                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    bizName = sdr["BizName"].ToString();
                    initName = sdr["Fileds01"].ToString();
                    GetWorkFlows(bizID, bizName, initName, sdr["BizCode"].ToString());

                    if (m_ActionName == "priint") SetCertificateLog(bizID, sdr["BizCode"].ToString(), sdr["BizName"].ToString(), sdr["Fileds01"].ToString(), sdr["PersonCidA"].ToString()); //保存打印的证件
                }
                sdr.Close();

            }
            catch { if (sdr != null) sdr.Close(); }
        }

        private string GetBzFlowKeyWords(string bizCode)
        {
            string cYear = "", nYear = "", flowNo = "";
            cYear = DateTime.Now.ToString("yyyy");
            nYear = DateTime.Now.AddYears(1).ToString("yyyy");
            flowNo = CommPage.GetSingleVal("SELECT COUNT(*)+1 FROM BIZ_Certificates WHERE BizCode='" + bizCode + "' AND CertificateType=4 AND CreateDate >'" + cYear + "/01/01 00:00:00' AND CreateDate <'" + nYear + "/01/01 00:00:00'");
            return "【" + cYear + "】" + flowNo.PadLeft(3, '0') + " 号";
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
                m_SqlParams = "SELECT TOP 1 AreaCode,AreaName,Approval,CertificateNoA,CertificateNoB,Attribs,CertificateDateStart,CertificateDateEnd FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep DESC";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    string CertificateNo = GetBzFlowKeyWords(BizCode);
                    string CertificateName = "补正通知书";
                    string CertificateGovName = sdr["AreaName"].ToString();
                    string StartDate = sdr["CertificateDateStart"].ToString();
                    string AreaCode = sdr["AreaCode"].ToString();

                     m_SqlParams = "SELECT COUNT(*) FROM BIZ_Certificates WHERE CertificateType=4 AND BizCode='" + BizCode + "' AND BizID=" + bizID;
                     if (CommPage.GetSingleVal(m_SqlParams) == "0") {
                         m_SqlParams = "INSERT INTO BIZ_Certificates (";
                         m_SqlParams += "BizID,BizCode,BizName,CertificateNo,CertificateName,AreaCode,";
                         m_SqlParams += "PersonName,PersonCid,CertificateGovName,StartDate,CertificateType";
                         m_SqlParams += ") VALUES(";
                         m_SqlParams += "" + bizID + ",'" + BizCode + "','" + BizName + "','" + CertificateNo + "','" + CertificateName + "','" + AreaCode + "',";
                         m_SqlParams += "'" + pName + "','" + pCid + "','" + CertificateGovName + "','" + StartDate + "',4";
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

        /*

         
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="curAreaName"></param>
        /// <returns></returns>
        private void GetWorkFlows(string bizID, string bizName, string appUserName, string bizCode)
        {
            string govName="",correctDocs="",modDate = string.Empty;
            string[] aryDocs = null;
            StringBuilder s = new StringBuilder();
            DataTable dt = new DataTable();
            try
            {
                // // Attribs: 0,未通过1,审核成功
                //m_SqlParams = "SELECT DeptName,BizStepTotal,BizStep,Approval,Signature,OprateDate,CertificateNoA,CertificateNoB,CertificateMemo FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep";
                m_SqlParams = "SELECT TOP 1 AreaName,Approval,Signature,OprateDate,CertificateNoA,CertificateNoB,CertificateMemo,Attribs,CommMemo,CertificateDateEnd FROM BIZ_WorkFlows WHERE BizID=" + bizID + " AND Attribs!=9 ORDER BY BizStep DESC";
                dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    govName = dt.Rows[0]["AreaName"].ToString();
                    correctDocs = dt.Rows[0]["CommMemo"].ToString();
                    modDate = dt.Rows[0]["CertificateDateEnd"].ToString();
                    if (!string.IsNullOrEmpty(modDate)) { 
                        modDate = DateTime.Parse(modDate).ToString("yyyy年MM月dd日"); } 
                    else {
                        modDate = "<span></span>年<span></span>月<span></span>日";
                    }
                    
                    s.Append("<div class=\"title\">");
                    s.Append("<div class=\"a1\">" + govName + "</div>");
                    s.Append("<div class=\"a2\">" + bizName + "<br />补正通知书</div>");
                    s.Append("<div class=\"a3\">补通字 " + GetBzFlowKeyWords(bizCode) + "</div>");//编号生成
                    s.Append("</div>");
                    s.Append("<div class=\"content\">");
                    s.Append("<div class=\"name\"><span>" + appUserName + "</span> ：</div>");
                    s.Append("<div class=\"sum\">你向本机关递交的<span style=\"min-width:250px;\">" + bizName + "</span>申请及有关材料已收悉。经审查，你提供的申请材料不齐，请你于" + modDate + "前补齐，逾期未补齐的视为未申请。需补充提交的材料如下：</div>");
                    if (!string.IsNullOrEmpty(correctDocs)) {
                        aryDocs = correctDocs.Split(',');
                        for (int k = 0; k < aryDocs.Length ;k++ ) 
                        {
                            s.Append("<div class=\"data\">" + (k+1).ToString() + "、" + aryDocs[k] + "；</div>");
                        }
                    }
                    //s.Append("<div class=\"data\">1、<span></span>（  份）；</div>");
                    //s.Append("<div class=\"data\">2、<span></span>（  份）；</div>");
                    //s.Append("<div class=\"data\">3、<span></span>（  份）；</div>");
                    //s.Append("<div class=\"data\">4、<span></span>（  份）；</div>");
                    //s.Append("<div class=\"data\">5、<span></span>（  份）；</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"bottom\">");
                    s.Append("<div class=\"official\">" + govName + "（盖章）</div>");
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
                s.Append("<div class=\"tr_07\"><br/>获取补正信息时发生错误！<br/><br/></div>");
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

