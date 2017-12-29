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
    public partial class BizPrt01502 : System.Web.UI.Page
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
            if (!string.IsNullOrEmpty(m_Qcfwzh))
            {
                m_ObjID = DbHelperSQL.GetSingle("SELECT top 1 BizID FROM BIZ_WorkFlows WHERE  CertificateNoA='" + m_Qcfwzh + "' ").ToString();
                ShowBizInfo(m_ObjID);
            }
            else
            {
                //如果没有该业务
                Response.Write("<script language='javascript'>alert(\"操作提示：请登录后再试！\");parent.location.href='/';</script>");

            }
            if (!IsPostBack)
            {
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
                HttpCookie loginCookie = Request.Cookies["AREWEB_OC_PUBSVRS_YSL"];
                //后台
                HttpCookie loginCookiem = Request.Cookies["AREWEB_OC_USER_YSL"];
                if (Request.QueryString["mp"] == "1")
                {
                    if (loginCookiem != null && !String.IsNullOrEmpty(loginCookiem.Values["UserID"].ToString()))
                    {
                        returnVa = true;
                        m_UserID = "m_" + loginCookiem.Values["UserID"].ToString();
                    }
                }
                else if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString()))
                {
                    returnVa = true;
                    m_UserID = loginCookie.Values["UserID"].ToString();
                }
            }
            else
            {
                if (Request.QueryString["mp"] == "1")
                {
                    if (!String.IsNullOrEmpty(Session["AREWEB_OC_USERID"].ToString()))
                    {
                        returnVa = true;
                        m_UserID = "m_" + Session["AREWEB_OC_USERID"].ToString();
                    }
                }
                else if (!String.IsNullOrEmpty(Session["UserID"].ToString()))
                {
                    returnVa = true;
                    m_UserID = Session["UserID"].ToString();
                }
            }

            if (!returnVa)
            {
                if (!String.IsNullOrEmpty(mp))
                {
                    //后台
                    Response.Write("<script language='javascript'>alert(\"操作提示：请登录后再试！\");parent.location.href='/';</script>");
                }
                else
                {
                    //前台
                    Response.Write("<div  style=\"text-align:center;font-size:14px;padding:50px;\">操作提示：请登录后再试！</div>");
                }
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
            mp = Request.QueryString["mp"];
            m_Qcfwzh = Request.QueryString["fwzh"];
        }
        
        /// <summary>
        /// 查看详细信息
        /// </summary>
        /// <param name="objID"></param>
        private void ShowBizInfo(string bizID)
        {

            string sqlParams, sqlParams0 = "";
            string helpbiz_Fileds01 = string.Empty;
            string helpbiz_Fileds08 = string.Empty;
            string PersonCids = string.Empty;
            string CidArry = "'00000000000000000'";
            SqlDataReader sdr = null;
                sqlParams = "SELECT TOP 1 Fileds01,Fileds08,PersonCidA, PersonCidB FROM v_BizList WHERE   BizCode='0150' and BizID=" + bizID + " AND Attribs IN(2,8,9) ORDER BY  bizID DESC";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (sdr["PersonCidA"].ToString() != "" || sdr["PersonCidA"].ToString() != null)
                        {
                            PersonCids = sdr["PersonCidA"].ToString();
                            CidArry += ",'" + sdr["PersonCidA"].ToString() + "'";
                        }
                        if (sdr["PersonCidB"].ToString() != "" || sdr["PersonCidB"].ToString() != null)
                        {
                            PersonCids = sdr["PersonCidB"].ToString();
                            CidArry += ",'" + sdr["PersonCidB"].ToString() + "'";
                        }
                        helpbiz_Fileds01 = sdr["Fileds01"].ToString();
                        helpbiz_Fileds08 = sdr["Fileds08"].ToString();
                    }
                }
                if (sdr != null)
                {
                    sdr.Close();
                    sdr.Dispose();
                }
                CidArry = CidArry.Replace(" ", "").Replace(",,", ",");
                if (CidArry.Length > 20)
                {
                    string photos = string.Empty;
                    string cerNo = "", workFlowInfo = string.Empty;
                    StringBuilder s = new StringBuilder();
                    try
                    {
                        s.Append("<div class=\"print_table\">");
                        s.Append("<div class=\"print_table_bg\">");
                        s.Append("<div class=\"print_l\">");
                        s.Append("<div class=\"table_02\">");
                        s.Append("<div class=\"title\">婚前医学健康检查证明</div>");
                        s.Append("<div class=\"tr_01\">");
                        SqlDataReader sdr0 = null;
                        sqlParams = "SELECT  BizID,BizCode,Fileds01,Fileds08,PersonCidA, PersonCidB,Fileds43,Fileds18,Fileds24,StartDate  FROM v_BizList WHERE   BizCode in (0131) and (PersonCidA in(" + CidArry + ")  OR PersonCidB in(" + CidArry + ")) AND Attribs IN(2,8,9) ORDER BY  BizCode asc,BizID asc ";
                        sdr0 = DbHelperSQL.ExecuteReader(sqlParams);
                        if (sdr0.HasRows)
                        {
                            while (sdr0.Read())
                            {

                                s.Append("<ul>");
                                s.Append("<li class=\"a1\">兹证明<span>" + helpbiz_Fileds01 + "</span> / <span>" + helpbiz_Fileds08 + "</span>于<span>" + GetDateFormat(sdr0["StartDate"].ToString(), "1") + "</span>(日期)到我处参加婚前优生健康检查。</li>");
                                s.Append("<li class=\"a2\">特此证明。</li>");
                                s.Append("<li class=\"a3\">单位名称（盖章有效）：</li>");
                                s.Append("<li class=\"a4\">石泉县妇幼保健计划生育服务中心</li>");
                                s.Append("<li class=\"a4\">日期：<span>2015年10月10日</span></li>");
                                s.Append("</ul>");

                                //审核流程
                                //s.Append(workFlowInfo);
                            }
                        }
                        if (sdr0 != null)
                        {
                            sdr0.Close();
                            sdr0.Dispose();
                        }
                        s.Append("</div>");
                        s.Append("<div class=\"title\">孕前优生健康检查证明</div>");
                        s.Append("<div class=\"tr_01\">");

                        SqlDataReader sdr00 = null;
                        sqlParams0 = "SELECT top 1 Results,Advice,CheckDate  FROM NHS_YqysJkjc WHERE  WifeCID in(" + CidArry + ")  OR ManCID in(" + CidArry + ")  ORDER BY  UnvID DESC   ";
                        sdr00 = DbHelperSQL.ExecuteReader(sqlParams0);
                        if (sdr00.HasRows)
                        {
                            while (sdr00.Read())
                            {
                                s.Append("<ul>");
                                s.Append("<li class=\"a1\">兹证明该夫妇（<span>" + helpbiz_Fileds01 + "</span> / <span>" + helpbiz_Fileds08 + "</span>）于<span>" + GetDateFormat(sdr00["CheckDate"].ToString(), "1") + "</span>(日期)到我处参加国家免费孕前优生健康检查。</li>");
                                s.Append("<li class=\"a2\">特此证明。</li>");
                                s.Append("<li class=\"a3\">单位名称（盖章有效）：</li>");
                                s.Append("<li class=\"a4\">石泉县妇幼保健计划生育服务中心</li>");
                                s.Append("<li class=\"a4\">日期：<span>2015年10月10日</span></li>");
                                s.Append("</ul>");
                            }
                        }
                        if (sdr00 != null)
                        {
                            sdr00.Close();
                            sdr00.Dispose();
                        }
                        s.Append("</div>");
                        s.Append("</div>");
                        s.Append("</div>");
                        s.Append("<div class=\"print_r\">");
                        s.Append("<div class=\"table_03\">");
                        s.Append("<div class=\"title\">生育登记和再生育审批记录</div>");
                        s.Append("</div>");
                        s.Append("</div>");
                        s.Append("<div class=\"clr\"></div>");
                        s.Append("</div>");
                        s.Append("</div>");
                    }
                    catch { }
                    if (m_ActionName == "priint")
                    {
                        CommPage.SetBizLog(bizID, m_UserID, "业务打印", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 打印了01502");
                        //保存打印的证件
                        join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                        log.BizID = bizID;
                        log.BizCode = "0150";
                        log.SetCertificateLog("01502", helpbiz_Fileds01, PersonCids, "打印了01502");
                        log = null;
                    }
                    this.LiteralBizInfo.Text = s.ToString();
                }
        }

        /// <summary>
        /// 取得流程签名、章
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
                    b.Append("<div class=\"td_02\">审批人：" + sign1 + "&nbsp;" + OprateDate1 + "<br />电话：" + tel1 + "</div>");
                    b.Append("<div class=\"official\">" + seal1 + "</div>");
                    b.Append("</div>");
                }
                else
                {
                    b.Append("<div class=\"seal_02 clearfix\">");
                    b.Append("<div class=\"td_01\">&nbsp;</div>");
                    b.Append("<div class=\"td_02\">审批人：&nbsp;<br />电话：</div>");
                    b.Append("<div class=\"official\">&nbsp;&nbsp;</div>");
                    b.Append("</div>");
                }
                dt = null;
            }
            catch
            {
                b.Append("<div class=\"tr_05 clearfix\"><br/>获取流程节点审核信息时发生错误！<br/><br/></div>");
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

