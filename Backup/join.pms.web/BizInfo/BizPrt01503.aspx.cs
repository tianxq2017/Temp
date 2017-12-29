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
    public partial class BizPrt01503 : System.Web.UI.Page
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

                        //左边开始
                        SqlDataReader sdr0 = null;
                        sqlParams = "SELECT  UnvID,FnName,FnCID,TbYz,YunCi, ChanCi_Yd,ChanCi_Pg,ZfName,ZfCID,MoCiYj,YuChanQi,History_Jw,History_Jz,History_Gr,History_Fkss,History_Yc,History_Jw_other,History_Jz_other,History_Gr_other,History_Fkss_other,History_Yc_other,CreateDate  FROM NHS_YCF_Base WHERE   FnCID in(" + CidArry + ") AND QcfwzBm='" + m_Qcfwzh + "' ORDER BY  UnvID desc ";
                        sdr0 = DbHelperSQL.ExecuteReader(sqlParams);
                        if (sdr0.HasRows)
                        {
                            while (sdr0.Read())
                            {
                                s.Append("<div class=\"print_l\">");
                                s.Append("<div class=\"table_04\">");
                                s.Append("<div class=\"title\">孕产妇保健服务</div>");
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
                                s.Append("<li class=\"a1\">丈夫姓名</li>");
                                s.Append("<li class=\"a2\">" + sdr0["ZfName"].ToString() + "</li>");
                                s.Append("<li class=\"a3\">身份证号</li>");
                                s.Append("<li class=\"a4\">" + sdr0["ZfCID"].ToString() + "</li>");
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
                                s.Append("<li class=\"a1\">建档孕周</li>");
                                s.Append("<li class=\"a2\">" + sdr0["TbYz"].ToString() + "周</li>");
                                s.Append("<li class=\"a3\">孕次</li>");
                                s.Append("<li class=\"a4\">第" + sdr0["YunCi"].ToString() + "次</li>");
                                s.Append("</ul>");
                                s.Append("</div>");
                                s.Append("<div class=\"tr_01 clearfix\">");
                                s.Append("<ul>");
                                s.Append("<li class=\"a1\">产次</li>");
                                s.Append("<li class=\"a5\">阴道分娩<span>" + sdr0["ChanCi_Yd"].ToString() + "</span>次 ，刮宫产<span>" + sdr0["ChanCi_Pg"].ToString() + "</span>次</li>");
                                s.Append("</ul>");
                                s.Append("</div>");
                                s.Append("<div class=\"tr_01 clearfix\">");
                                s.Append("<ul>");

                                string History_Jw = "无";
                                if (!string.IsNullOrEmpty(sdr0["History_Jw"].ToString()) && sdr0["History_Jw"].ToString() != "0")
                                {
                                    History_Jw = sdr0["History_Jw"].ToString().Replace("1", "心脏病").Replace("2", "肾脏疾病").Replace("3", "肝脏疾病").Replace("4", "高血压").Replace("5", "贫血").Replace("6", "糖尿病").Replace("7", sdr0["History_Jw_other"].ToString());
                                }
                                s.Append("<li class=\"a1\">既往史</li>");
                                s.Append("<li class=\"a5\">" + History_Jw + "</li>");
                                s.Append("</ul>");
                                s.Append("</div>");
                                s.Append("<div class=\"tr_01 clearfix\">");
                                s.Append("<ul>");

                                string History_Jz = "无";
                                if (!string.IsNullOrEmpty(sdr0["History_Jz"].ToString()) && sdr0["History_Jz"].ToString() != "0")
                                {
                                    History_Jz = sdr0["History_Jz"].ToString().Replace("1", "遗传性疾病史").Replace("2", "精神疾病史").Replace("3", sdr0["History_Jz_other"].ToString());
                                }
                                s.Append("<li class=\"a1\">家族史</li>");
                                s.Append("<li class=\"a5\">" + History_Jz + "</li>");
                                s.Append("</ul>");
                                s.Append("</div>");
                                s.Append("<div class=\"tr_01 clearfix\">");
                                s.Append("<ul>");

                                string History_Gr = "无";
                                if (!string.IsNullOrEmpty(sdr0["History_Gr"].ToString()) && sdr0["History_Gr"].ToString() != "0")
                                {
                                    History_Gr = sdr0["History_Gr"].ToString().Replace("1", "吸烟").Replace("2", "饮酒").Replace("3", "服用药物").Replace("4", "接触有毒有害物质").Replace("5", "接触放射线").Replace("6", sdr0["History_Gr_other"].ToString());
                                }
                                s.Append("<li class=\"a1\">个人史</li>");
                                s.Append("<li class=\"a5\">" + History_Gr + "</li>");
                                s.Append("</ul>");
                                s.Append("</div>");
                                s.Append("<div class=\"tr_01 clearfix\">");
                                s.Append("<ul>");
                                string History_Fkss = "无";
                                if (!string.IsNullOrEmpty(sdr0["History_Fkss"].ToString()) && sdr0["History_Fkss"].ToString() != "0")
                                {
                                    History_Fkss = sdr0["History_Fkss"].ToString().Replace("1", "有") + "<span>" + sdr0["History_Fkss_other"].ToString() + "</span>";
                                }
                                s.Append("<li class=\"a1\">妇科手术</li>");
                                s.Append("<li class=\"a5\">" + History_Fkss + "</li>");
                                s.Append("</ul>");
                                s.Append("</div>");
                                s.Append("<div class=\"tr_01 clearfix\">");
                                s.Append("<ul>");
                                s.Append("<li class=\"a1\">孕产史</li>");
                                s.Append("<li class=\"a5\">");
                                string History_Yc = sdr0["History_Yc"].ToString();
                                if (string.IsNullOrEmpty(History_Yc))
                                {
                                    History_Yc = "0,0,0,0,0";
                                }
                                for (int i = 0; i < History_Yc.Split(',').Length; i++)
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            s.Append("流产:" + History_Yc.Split(',')[i].ToString());
                                            break;
                                        case 1:
                                            s.Append(",死胎:" + History_Yc.Split(',')[i].ToString());
                                            break;
                                        case 2:
                                            s.Append(",死产:" + History_Yc.Split(',')[i].ToString());
                                            break;
                                        case 3:
                                            s.Append(",新生儿死亡:" + History_Yc.Split(',')[i].ToString());
                                            break;
                                        case 4:
                                            s.Append(",出生儿缺陷:" + History_Yc.Split(',')[i].ToString());
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                s.Append("</li>");
                                s.Append("</ul>");
                                s.Append("</div>");
                                s.Append("</div>");
                                s.Append("</div>");
                                //右边开始
                                int listid = 0;
                                s.Append("<div class=\"print_r\">");
                                s.Append("<div class=\"table_05\">");
                                s.Append("<div class=\"title\">随访记录</div>");
                                s.Append("<div class=\"tr_01 tr_01a clearfix\">");
                                s.Append("<ul>");
                                s.Append("<li class=\"a1\">次数</li>");
                                s.Append("<li class=\"a2\">访视日期</li>");
                                s.Append("<li class=\"a3\">下次访视日期</li>");
                                s.Append("<li class=\"a4\">是否高危</li>");
                                s.Append("<li class=\"a5\">医生签名</li>");
                                s.Append("</ul>");
                                s.Append("</div>");

                                SqlDataReader sdr1 = null;
                                sqlParams = "SELECT [CommID],[NextVisitDate],[Doctor],[CurVisitDate],[SFJG]  FROM [NHS_YCF_CQFS_1] WHERE    UnvID=" + sdr0["UnvID"].ToString() + " ORDER BY  CurVisitDate asc ";
                                sdr1 = DbHelperSQL.ExecuteReader(sqlParams);
                                if (sdr1.HasRows)
                                {
                                    while (sdr1.Read())
                                    {
                                        listid += 1;
                                        s.Append("<div class=\"tr_01 clearfix\">");
                                        s.Append("<ul>");
                                        s.Append("<li class=\"a1\">" + listid + "</li>");
                                        s.Append("<li class=\"a2\">" + GetDateFormat(sdr1["CurVisitDate"].ToString(), "") + "</li>");
                                        s.Append("<li class=\"a3\">" + GetDateFormat(sdr1["NextVisitDate"].ToString(), "") + "</li>");
                                        s.Append("<li class=\"a4\">" + sdr1["SFJG"].ToString() + "</li>");
                                        s.Append("<li class=\"a5\">" + sdr1["Doctor"].ToString() + "</li>");
                                        s.Append("</ul>");
                                        s.Append("</div>");
                                    }
                                }
                                if (sdr1 != null)
                                {
                                    sdr1.Close();
                                    sdr1.Dispose();
                                }
                                SqlDataReader sdr2 = null;
                                sqlParams = "SELECT [CommID],[NextVisitDate],[Doctor],[VisitDate]  FROM [NHS_YCF_CQFS_2_13] WHERE  UnvID=" + sdr0["UnvID"].ToString() + " ORDER BY  VisitDate asc ";
                                sdr2 = DbHelperSQL.ExecuteReader(sqlParams);
                                if (sdr2.HasRows)
                                {
                                    while (sdr2.Read())
                                    {
                                        listid += 1;
                                        s.Append("<div class=\"tr_01 clearfix\">");
                                        s.Append("<ul>");
                                        s.Append("<li class=\"a1\">" + listid + "</li>");
                                        s.Append("<li class=\"a2\">" + GetDateFormat(sdr2["VisitDate"].ToString(), "") + "</li>");
                                        s.Append("<li class=\"a3\">" + GetDateFormat(sdr2["NextVisitDate"].ToString(), "") + "</li>");
                                        s.Append("<li class=\"a4\"></li>");
                                        s.Append("<li class=\"a5\">" + sdr2["Doctor"].ToString() + "</li>");
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
                                s.Append("</div>");
                                s.Append("<div class=\"clr\"></div>");
                                s.Append("</div>");
                                s.Append("</div>");


                            }
                        }
                        if (sdr0 != null)
                        {
                            sdr0.Close();
                            sdr0.Dispose();
                        }
                    }
                    catch { }
                    if (m_ActionName == "priint")
                    {
                        CommPage.SetBizLog(bizID, m_UserID, "业务打印", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 打印了01503");
                        //保存打印的证件
                        join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                        log.BizID = bizID;
                        log.BizCode = "0150";
                        log.SetCertificateLog("01503", helpbiz_Fileds01, PersonCids, "打印了01503");
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

