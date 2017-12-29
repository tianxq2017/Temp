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
    public partial class BizHuizhi : System.Web.UI.Page
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
            string bizName = string.Empty;
            string personNames="",personA = string.Empty;
            string personB = string.Empty;
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
                  ] FROM [YN_ChuXiong_OnlineCertificate_DB].[dbo].[BIZ_Contents] 
                 */
                
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    bizName = sdr["BizName"].ToString();
                    personA = sdr["Fileds01"].ToString();
                    personB = sdr["Fileds08"].ToString();
                    if (!string.IsNullOrEmpty(personA)) {
                        personNames = personA;
                        if (!string.IsNullOrEmpty(personB)) personNames = personA + "、" + personB;
                    } 
                    else {
                        personNames = personB;
                    }

                    GetWorkFlows(bizID, bizName, sdr["BizCode"].ToString(), personNames, sdr["Fileds18"].ToString());

                    if (m_ActionName == "priint") SetCertificateLog(bizID, sdr["BizCode"].ToString(), bizName, personA, sdr["PersonCidA"].ToString()); //保存打印的证件
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }
        }

        /// <summary>
        /// 保存打印的证件记录
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
                    string CertificateNo = sdr["CertificateNoA"].ToString();
                    string CertificateName = "收件回执单";
                    string CertificateGovName = sdr["AreaName"].ToString();
                    string StartDate = sdr["CertificateDateStart"].ToString();
                    string AreaCode = sdr["AreaCode"].ToString();

                    m_SqlParams = "SELECT COUNT(*) FROM BIZ_Certificates WHERE CertificateType=3 AND BizCode='" + BizCode + "' AND BizID=" + bizID ;
                    if (CommPage.GetSingleVal(m_SqlParams) == "0") {
                        m_SqlParams = "INSERT INTO BIZ_Certificates (";
                        m_SqlParams += "BizID,BizCode,BizName,CertificateNo,CertificateName,AreaCode,";
                        m_SqlParams += "PersonName,PersonCid,CertificateGovName,StartDate,CertificateType";
                        m_SqlParams += ") VALUES(";
                        m_SqlParams += "" + bizID + ",'" + BizCode + "','" + BizName + "','-','" + CertificateName + "','" + AreaCode + "',";
                        m_SqlParams += "'" + pName + "','" + pCid + "','" + CertificateGovName + "','" + StartDate + "',3";
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
     <div class=\"content\">
	  <div class=\"sum\">
	    <p>兹收到<span style=\"min-width:250px;\">办证机关名称办证机关名称</span>发给的<span style=\"min-width:200px;\">独生子女父母光荣证</span>一本。</p>
		<p>此据。</p>
	  </div>
	</div>
	<div class=\"bottom fr\">
	  <div class=\"name\">收件人（签名）：<span style=\"min-width:100px;\"></span></div>
	  <div class=\"time\">收件日期：<span></span>年<span></span>月<span></span>日</div>
	</div>
                 */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="curAreaName"></param>
        /// <returns></returns>
        private void GetWorkFlows(string bizID,string bizName,string bizCode,string personName,string childNum)
        {
            string bizStepTotal = "";
            int bizStep = 0;
            StringBuilder s = new StringBuilder();
            DataTable dt = new DataTable();
            try
            {
                if (bizCode == "0101") { bizName = "一孩计划生育服务证"; }
                else if (bizCode == "0102") { bizName =  "二孩计划生育服务证"; }
                else if (bizCode == "0103") { bizName = "农村部分计划生育家庭奖励扶助光荣证"; }
                else if (bizCode == "0108") { bizName = "独生子女父母关荣证"; }
                else if (bizCode == "0109") { bizName = "流动人口婚育证明"; }
                else if (bizCode == "0122") { bizName = childNum + "孩计划生育服务证"; }
                else { }
                // Attribs: 0,未通过1,审核成功
                m_SqlParams = "SELECT TOP 1 DeptName,BizStepTotal,BizStep,Approval,Signature,OprateDate,CertificateNoA,CertificateNoB,CertificateMemo,Attribs FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep DESC";
                dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (dt.Rows.Count > 0) {
                    //bizStepTotal = dt.Rows[0]["BizStepTotal"].ToString();
                    //bizStep = int.Parse(bizStepTotal) -1;
                    // <p>本人承诺保证以上情况及提供的相关材料属实。如有不实，愿意承担由此引起的相应法律责任。</p>
                    s.Append("<div class=\"content\"><div class=\"sum\">");
                    s.Append("<p>兹收到<span style=\"min-width:250px;\">" + dt.Rows[0]["DeptName"].ToString() + "</span>发给<span style=\"min-width:120px;\">" + personName + "</span>的<span style=\"min-width:200px;\">《" + bizName + "》</span>一本。</p>");
                    s.Append("<p>本人承诺申请办理<span style=\"min-width:200px;\">《" + bizName + "》</span>时所提交的所有电子证照及相关证件材料均属实。如有不实，愿意承担由此引起的相应法律责任。</p>");
                    s.Append("<p>此据。</p>");
                    s.Append("</div></div><div class=\"bottom fr\">");
                    s.Append("<div class=\"name\">收件人（签名）：<span style=\"min-width:100px;\"></span></div>");
                    //s.Append("<div class=\"time\">收件日期：<span>" + DateTime.Now.ToString("yyyy") + "</span>年<span>" + DateTime.Now.ToString("MM") + "</span>月<span>" + DateTime.Now.ToString("dd") + "</span>日</div>
                    s.Append("<div class=\"time\">收件日期：" + DateTime.Now.ToString("yyyy年MM月dd日") + "</div>");
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

