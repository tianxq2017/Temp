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
    public partial class BizPrt015023 : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;
        private string m_UserID; // 当前登录的操作用户编号
        private string m_SqlParams;
        public string m_TargetUrl;

        private string m_Qcfwzh; // 婚育健康服务证号
        private string mp; // 来路

        public string m_SiteName = System.Configuration.ConfigurationManager.AppSettings["SiteName"];
        private string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];
        /// <summary>
        /// 页面初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
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
            //mp = Request.QueryString["mp"];
            //m_Qcfwzh = Request.QueryString["fwzh"];
        }

        /// <summary>
        /// 查看详细信息
        /// </summary>
        /// <param name="objID"></param>
        private void ShowBizInfo(string bizID)
        {

            string sqlParams = "";
            string helpbiz_Fileds01 = string.Empty;
            string helpbiz_Fileds08 = string.Empty;
            string PersonCids = string.Empty;
            string CidArry = "'00000000000000000'";
            SqlDataReader sdr = null;

            string photos = string.Empty;
            string temp = "";
            string cerNo = "", workFlowInfo = string.Empty;

            string CertificateDateStart = "";
            string spjg = "";


            DataTable dt = new DataTable();
            try
            {
                m_SqlParams = "SELECT AreaName,Comments,Approval,AuditUser,AuditUserSealPath,OprateDate,CertificateNoA,CertificateNoB,CertificateMemo,CertificateDateStart,CertificateDateEnd,ApprovalSignPath,ApprovalUserTel,AuditUserSignPath,AreaCode FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep";
                dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    cerNo = dt.Rows[dt.Rows.Count - 1]["CertificateNoA"].ToString();
                    CertificateDateStart = GetDateFormat(dt.Rows[dt.Rows.Count - 1]["CertificateDateStart"].ToString(), "");
                    spjg = dt.Rows[dt.Rows.Count - 1]["AreaName"].ToString();
                }
            }
            catch { }



            StringBuilder s = new StringBuilder();
            try
            {
                s.Append("<div class=\"print_table\">");
                s.Append("<div class=\"print_table_bg\">");
                s.Append("<div class=\"print_l\">");
                s.Append("<div class=\"table_02\">");
                s.Append("<div class=\"title\">再生育审批</div>");
                s.Append("<div class=\"tr_01\">");
                sqlParams = "SELECT  BizID,BizCode,Fileds01,Fileds08,PersonCidA, PersonCidB,Fileds43,Fileds18,Fileds24,StartDate  FROM v_BizList WHERE   BizCode in (0123,0124,0125,0126) and (PersonCidA in(" + CidArry + ")  OR PersonCidB in(" + CidArry + ")) AND Attribs IN(2,8,9) ORDER BY  BizCode asc,BizID asc ";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        temp = sdr["Fileds41"].ToString();
                        switch (temp)
                        {
                            case "3":
                                temp = "夫妻生育两个子女中有子女经医学鉴定为病残儿或者因伤致残，医学上认为夫妻可以再生育的。";
                                break;
                            case "4":
                                temp = "再婚前双方合计生育两个以上子女，再婚后未生育子女的。";
                                break;
                            case "5":
                                temp = "再婚前一方生育过一个子女，另一方未生育过，再婚后已生育一个子女的。";
                                break;
                            case "6":
                                temp = "夫妻未生育依法收养两个子女后又怀孕的";
                                break;
                        }

                        s.Append("<ul>");
                        s.Append("<li class=\"a1\">女方姓名：" + sdr["Fileds08"].ToString() + "</li>");
                        s.Append("<li class=\"a2\">女方婚姻状况：" + sdr["Fileds13"].ToString() + "</li>");
                        s.Append("<li class=\"a3\">女方身份证号：" + sdr["PersonCidB"].ToString() + "</li>");
                        s.Append("<li class=\"a1\">男方姓名：" + sdr["Fileds01"].ToString() + "</li>");
                        s.Append("<li class=\"a2\">女方婚姻状况：" + sdr["Fileds33"].ToString() + "</li>");
                        s.Append("<li class=\"a3\">女方身份证号：" + sdr["PersonCidA"].ToString() + "</li>");
                        s.Append("<li class=\"a3\">结婚时间：" + GetDateFormat(sdr["Fileds34"].ToString(), "0") + "</li>");
                        s.Append("<li class=\"a3\">家庭现有子女数：" + sdr["Fileds37"].ToString() + "</li>");
                        s.Append("<li class=\"a3\">申请理由：" + temp + "</li>");


                        s.Append("<li class=\"a3\">发证日期：" + CertificateDateStart + "</li>");
                        s.Append("<li class=\"a3\">审批机构：" + spjg + "</li>");
                        s.Append("<li class=\"a3\">再生育证号：" + cerNo + "</li>");

                        s.Append("</ul>");
                    }
                }

                s.Append("</div>");

                s.Append("</div>");
                s.Append("</div>");
                s.Append("<div class=\"clr\"></div>");
                s.Append("</div>");
                s.Append("</div>");
            }
            catch { }
            finally
            {
                if (sdr != null)
                {
                    sdr.Close();
                    sdr.Dispose();
                }
            }
            if (m_ActionName == "priint")
            {
                CommPage.SetBizLog(bizID, m_UserID, "业务打印", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 打印了01502");
                //保存打印的证件
                join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                log.BizID = bizID;
                log.BizCode = "0150";
                log.SetCertificateLog("015023", helpbiz_Fileds01, PersonCids, "打印了015023");
                log = null;
            }
            this.LiteralBizInfo.Text = s.ToString();

        }

        private string GetDateFormat(string inStr, string type)
        {
            string returnVal = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(inStr))
                {
                    if (type == "1") { returnVal = DateTime.Parse(inStr).ToString("yyyy年MM月dd日"); }
                    else { returnVal = DateTime.Parse(inStr).ToString("yyyy-MM-dd"); }
                }
            }
            catch { returnVal = inStr; }
            return returnVal;
        }


        #endregion
    }
}

