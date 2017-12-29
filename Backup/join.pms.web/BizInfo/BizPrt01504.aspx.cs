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
    public partial class BizPrt01504 : System.Web.UI.Page
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
            string FnregAddress = "";
            
            SqlDataReader sdr = null;
            //民族

            string NationsA = string.Empty;
            string NationsB = string.Empty;
            sqlParams = "SELECT  TOP 1 Fileds01,Fileds08,Fileds03,Fileds10,PersonCidA, PersonCidB,RegAreaNameB FROM v_BizList WHERE   BizCode='0150' and BizID=" + bizID + " AND Attribs IN(2,8,9) ORDER BY  bizID DESC";
            sdr = DbHelperSQL.ExecuteReader(sqlParams);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (sdr["PersonCidA"].ToString() != "" || sdr["PersonCidA"].ToString() != null)
                    {
                        CidArry += ",'" + sdr["PersonCidA"].ToString() + "'";
                    }
                    if (sdr["PersonCidB"].ToString() != "" || sdr["PersonCidB"].ToString() != null)
                    {
                        CidArry += ",'" + sdr["PersonCidB"].ToString() + "'";
                    }
                    helpbiz_Fileds01 = sdr["Fileds01"].ToString();
                    helpbiz_Fileds08 = sdr["Fileds08"].ToString();
                    NationsA = sdr["Fileds03"].ToString();
                    NationsB = sdr["Fileds10"].ToString();
                    FnregAddress = sdr["RegAreaNameB"].ToString();
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
                    //左边开始
                    SqlDataReader sdr0 = null;
                    sqlParams = "SELECT  CommID, UnvID, QcfwzBm, FnName, FnCID, FnDeptment, FnAddress, FnAge,FnTel, ZfName, ZfAge, ZfTel, ZfCID, TaiCi,YuChanQi, MoCiYj, sholiYJ, sholiDate, sholiJG, UserID, UserName, CreateDate  FROM v_NHS_YCF_FMK WHERE   FnCID in(" + CidArry + ") AND QcfwzBm='" + m_Qcfwzh + "' order  by  CommID desc  ";
                    sdr0 = DbHelperSQL.ExecuteReader(sqlParams);
                    if (sdr0.HasRows)
                    {
                        while (sdr0.Read())
                        {
                            s.Append("<div class=\"print_table\">");
                            s.Append("<div class=\"print_table_bg\">");
                            s.Append("<div class=\"print_l\">");
                            s.Append("<div class=\"table_06\">");
                            s.Append("<div class=\"title\">孕产妇住院分娩卡</div>");
                            s.Append("<div class=\"tr_01 tr_01a clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">孕妇姓名</li>");
                            s.Append("<li class=\"a2\">" + sdr0["FnName"].ToString() + "</li>");
                            s.Append("<li class=\"a3\">身份证号</li>");
                            s.Append("<li class=\"a4\">" + sdr0["FnCID"].ToString() + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">民族</li>");
                            s.Append("<li class=\"a2\">" + NationsB + "</li>");
                            s.Append("<li class=\"a3\">出生年月</li>");
                            s.Append("<li class=\"a4\">" + CommBiz.GetBirthdayByID(sdr0["FnCID"].ToString()) + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">丈夫姓名</li>");
                            s.Append("<li class=\"a2\">" + sdr0["ZfName"].ToString() + "</li>");
                            s.Append("<li class=\"a3\">身份证号</li>");
                            s.Append("<li class=\"a4\">" + sdr0["ZfCID"].ToString() + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">现居住地</li>");
                            s.Append("<li class=\"a5\">" + sdr0["FnAddress"].ToString().Replace("&", "&nbsp;") + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">户籍地址</li>");
                            s.Append("<li class=\"a5\">" + FnregAddress.Replace("&", "&nbsp;") + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">末次月经</li>");
                            s.Append("<li class=\"a2\">" + GetDateFormat(sdr0["MoCiYj"].ToString(), "") + "</li>");
                            s.Append("<li class=\"a3\">预产期</li>");
                            s.Append("<li class=\"a4\">" + GetDateFormat(sdr0["YuChanQi"].ToString(), "") + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">胎次</li>");
                            s.Append("<li class=\"a2\">" + sdr0["TaiCi"].ToString() + "</li>");
                            s.Append("<li class=\"a3\">联系电话</li>");
                            s.Append("<li class=\"a4\">" + sdr0["FnTel"].ToString() + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">受理机构名称</li>");
                            s.Append("<li class=\"a5\">" + sdr0["sholiJG"].ToString() + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">受理日期</li>");
                            s.Append("<li class=\"a5\">" + GetDateFormat(sdr0["sholiDate"].ToString(), "") + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_02 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">受理机构意见</li>");
                            s.Append("<li class=\"a2\">" + sdr0["sholiYJ"].ToString() + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("</div>");
                            s.Append("</div>");
                        }
                    }
                    if (sdr0 != null)
                    {
                        sdr0.Close();
                        sdr0.Dispose();
                    }
                    //右边开始
                    int listid = 0;

                    s.Append("<div class=\"print_r\">");
                    s.Append("<div class=\"table_07\">");
                    s.Append("<div class=\"title\">育龄妇女健康检查</div>");

                    SqlDataReader sdr1 = null;
                    sqlParams = "SELECT top 1 [CommID],[FnName],[FnCID],[FnTel],[FnHyzk],[JHAge] ,[MoCiYj] ,[Yun] ,[Chan] ,[Bycs] ,[HomeAddress]   FROM [NHS_YlfnJkjc] WHERE    FnCID in(" + CidArry + ") AND QcfwzBm='" + m_Qcfwzh + "' ORDER BY  CheckDate desc ";
                    sdr1 = DbHelperSQL.ExecuteReader(sqlParams);
                    if (sdr1.HasRows)
                    {
                        while (sdr1.Read())
                        {
                            s.Append("<div class=\"tr_01 tr_01a clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">姓名</li>");
                            s.Append("<li class=\"a2\">" + sdr1["FnName"].ToString() + "</li>");
                            s.Append("<li class=\"a3\">身份证号</li>");
                            s.Append("<li class=\"a4\">" + sdr1["FnCID"].ToString() + "</li>");
                            s.Append("<li class=\"a5\">联系电话</li>");
                            s.Append("<li class=\"a6\">" + sdr1["FnTel"].ToString() + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">婚姻状况</li>");
                            s.Append("<li class=\"a2\">" + sdr1["FnHyzk"].ToString() + "</li>");
                            s.Append("<li class=\"a3\">结婚年龄</li>");
                            s.Append("<li class=\"a4\">" + sdr1["JHAge"].ToString() + "</li>");
                            s.Append("<li class=\"a5\">末次月经</li>");
                            s.Append("<li class=\"a6\">" + GetDateFormat(sdr1["MoCiYj"].ToString(), "") + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">孕次</li>");
                            s.Append("<li class=\"a2\">" + sdr1["Yun"].ToString() + "</li>");
                            s.Append("<li class=\"a3\">产次</li>");
                            s.Append("<li class=\"a4\">" + sdr1["Chan"].ToString() + "</li>");
                            s.Append("<li class=\"a5\">避孕措施</li>");

                            s.Append("<li class=\"a6\">");
                             string bycs = sdr1["Bycs"].ToString();
                                if (string.IsNullOrEmpty(bycs))
                                {
                                    bycs = "0";
                                }
                                switch (bycs)
                                {
                                    case "0":
                                        s.Append("无");
                                        break;
                                    case "1":
                                        s.Append("避孕套");
                                        break;
                                    case "2":
                                        s.Append("避孕针");
                                        break;
                                    case "3":
                                        s.Append("子宫环");
                                        break;
                                    case "4":
                                        s.Append("避孕帖");
                                        break;
                                    case "5":
                                        s.Append("计算排卵期");
                                        break;
                                    default:
                                        break;
                                }


                            s.Append("</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">现居住地</li>");
                            s.Append("<li class=\"a7\">" + sdr1["HomeAddress"].ToString().Replace("&", "&nbsp;") + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">户籍地址</li>");
                            s.Append("<li class=\"a7\">" + FnregAddress.Replace("&", "&nbsp;") + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_02\">检查记录</div>");
                            s.Append("<div class=\"tr_03 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">检查单位名称</li>");
                            s.Append("<li class=\"a2\">检查日期</li>");
                            s.Append("<li class=\"a3\">检查医师</li>");
                            s.Append("<li class=\"a4\">下次检查日期</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                        }
                    }
                    if (sdr1 != null)
                    {
                        sdr1.Close();
                        sdr1.Dispose();
                    }
                    s.Append("<div class=\"tr_04\">");
                    SqlDataReader sdr2 = null;
                    sqlParams = "SELECT [CommID],[CheckDate],[JC_Doctor6],[CheckJG]  FROM [NHS_YlfnJkjc] WHERE   FnCID in(" + CidArry + ") AND QcfwzBm='" + m_Qcfwzh + "' ORDER BY  CheckDate asc ";
                    sdr2 = DbHelperSQL.ExecuteReader(sqlParams);
                    if (sdr2.HasRows)
                    {
                        while (sdr2.Read())
                        {
                            listid += 1;
                            s.Append("<div class=\"list clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">" + sdr2["CheckJG"].ToString() + "</li>");
                            s.Append("<li class=\"a2\">" + GetDateFormat(sdr2["CheckDate"].ToString(), "") + "</li>");
                            s.Append("<li class=\"a3\">" + sdr2["JC_Doctor6"].ToString() + "</li>");
                            s.Append("<li class=\"a4\"></li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                        }
                    }
                    if (sdr2 != null)
                    {
                        sdr2.Close();
                        sdr2.Dispose();
                    }
                    s.Append("</div>");

                    s.Append("<div class=\"tr_05\">避孕措施知情选择服务记录</div>");
                    s.Append("<div class=\"tr_06 tr_06a clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">序号</li>");
                    s.Append("<li class=\"a2\">检查时间</li>");
                    s.Append("<li class=\"a3\">检查结论</li>");
                    s.Append("<li class=\"a4\">诊断结论</li>");
                    s.Append("<li class=\"a5\">转诊建议</li>");
                    s.Append("<li class=\"a6\">备注</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<!--div class=\"tr_06 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">1</li>");
                    s.Append("<li class=\"a2\">2014-10-10</li>");
                    s.Append("<li class=\"a3\">相关内容</li>");
                    s.Append("<li class=\"a4\">相关内容</li>");
                    s.Append("<li class=\"a5\">相关内容</li>");
                    s.Append("<li class=\"a6\">相关内容</li>");
                    s.Append("</ul>");
                    s.Append("</div-->");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"clr\"></div>");
                    s.Append("</div>");
                    s.Append("</div>");
                }
                catch { }
                if (m_ActionName == "priint")
                {
                    CommPage.SetBizLog(bizID, m_UserID, "业务打印", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 打印了01504");
                    //保存打印的证件
                    join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                    log.BizID = bizID;
                    log.BizCode = "0150";
                    log.SetCertificateLog("01504", helpbiz_Fileds01, PersonCids, "打印了01504");
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

