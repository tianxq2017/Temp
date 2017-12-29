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
    public partial class BizPrt01505 : System.Web.UI.Page
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
            sqlParams = "SELECT TOP 1 Fileds01,Fileds08,PersonCidA, PersonCidB,RegAreaNameB FROM v_BizList WHERE   BizCode='0150' and BizID=" + bizID + " AND Attribs IN(2,8,9) ORDER BY  bizID DESC";
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
                    sqlParams = "select top 1 UnvID,QcfwzBm,ChildBm,ChildName,ChildSex,BirthDay,BirthCerNo,ChildCID,CurAreaName,CurAreaCode,RegAreaName,RegAreaCode,FatherName,FatherCID,FatherTel,FatherJob,MotherName,MotherCID,MotherTel,MotherJob,UserID,JzUnit,CreateDate,BornYY,WeightBorn,History_Gm,Jzjj,JzCompy,JzTel  FROM NHS_Child_Base WHERE   MotherCID in(" + CidArry + ") AND QcfwzBm='" + m_Qcfwzh + "' order  by  UnvID desc  ";
                    sdr0 = DbHelperSQL.ExecuteReader(sqlParams);
                    if (sdr0.HasRows)
                    {
                        while (sdr0.Read())
                        {
                            s.Append("<div class=\"print_table\">");
                            s.Append("<div class=\"print_table_bg\">");
                            s.Append("<div class=\"print_l\">");
                            s.Append("<div class=\"table_08\">");
                            s.Append("<div class=\"title\">儿童保健服务</div>");
                            s.Append("<div class=\"tr_01 tr_01a clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">母亲姓名</li>");
                            s.Append("<li class=\"a2\">" + sdr0["MotherName"].ToString() + "</li>");
                            s.Append("<li class=\"a3\">身份证号</li>");
                            s.Append("<li class=\"a4\">" + sdr0["MotherCID"].ToString() + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">父亲姓名</li>");
                            s.Append("<li class=\"a2\">" + sdr0["FatherName"].ToString() + "</li>");
                            s.Append("<li class=\"a3\">身份证号</li>");
                            s.Append("<li class=\"a4\">" + sdr0["FatherCID"].ToString() + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_02\">健康检查记录</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\"></li>");
                            s.Append("<li class=\"a2\">日期</li>");
                            s.Append("<li class=\"a3\">下次检查日期</li>");
                            s.Append("<li class=\"a4\">医师签名</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            SqlDataReader sdr1 = null;
                            sqlParams = "select top 1 CommID,UnvID,VisitDate,NextVisitDate,Doctor   FROM [NHS_Child_XsrFs] WHERE   UnvID='" + sdr0["UnvID"].ToString() + "' ORDER BY  UnvID desc ";
                            sdr1 = DbHelperSQL.ExecuteReader(sqlParams);
                            if (sdr1.HasRows)
                            {
                                while (sdr1.Read())
                                {
                                    s.Append("<div class=\"tr_01 clearfix\">");
                                    s.Append("<ul>");
                                    s.Append("<li class=\"a1\">新生儿</li>");
                                    s.Append("<li class=\"a2\">" + GetDateFormat(sdr1["VisitDate"].ToString(), "") + "</li>");
                                    s.Append("<li class=\"a3\">" + GetDateFormat(sdr1["NextVisitDate"].ToString(), "") + "</li>");
                                    s.Append("<li class=\"a4\">" + sdr1["Doctor"].ToString() + "</li>");
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
                            sqlParams = "select CommID,UnvID,VisitItems,VisitDate,wyfs,NextVisitDate,Doctor  FROM [NHS_Child_JKJC] WHERE   UnvID='" + sdr0["UnvID"].ToString() + "' ORDER BY  VisitItems asc  ";
                            sdr2 = DbHelperSQL.ExecuteReader(sqlParams);
                            if (sdr2.HasRows)
                            {
                                while (sdr2.Read())
                                {
                                    s.Append("<div class=\"tr_01 clearfix\">");
                                    s.Append("<ul>");
                                    string jc_type = sdr2["VisitItems"].ToString();
                                    switch (jc_type)
                                    {
                                        case "1":
                                            jc_type = "满月";
                                            break;
                                        case "3":
                                            jc_type = "3月龄";
                                            break;
                                        case "6":
                                            jc_type = "6月龄";
                                            break;
                                        case "8":
                                            jc_type = "8月龄";
                                            break;
                                        case "12":
                                            jc_type = "12月龄";
                                            break;
                                        case "18":
                                            jc_type = "18月龄";
                                            break;
                                        case "24":
                                            jc_type = "24月龄";
                                            break;
                                        case "30":
                                            jc_type = "30月龄";
                                            break;
                                        case "36":
                                            jc_type = "3岁";
                                            break;
                                        case "48":
                                            jc_type = "4岁";
                                            break;
                                        case "60":
                                            jc_type = "5岁";
                                            break;
                                        case "72":
                                            jc_type = "6岁";
                                            break;
                                        case "99":
                                            jc_type = "其他";
                                            break;
                                        default:
                                            jc_type = "";
                                            break;
                                    }

                                    s.Append("<li class=\"a1\">" + jc_type + "</li>");
                                    s.Append("<li class=\"a2\">" + GetDateFormat(sdr2["VisitDate"].ToString(), "") + "</li>");
                                    s.Append("<li class=\"a3\">" + GetDateFormat(sdr2["NextVisitDate"].ToString(), "") + "</li>");
                                    s.Append("<li class=\"a4\">" + sdr2["Doctor"].ToString() + "</li>");
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

                            s.Append("<div class=\"print_r\">");
                            s.Append("<div class=\"table_09\">");
                            s.Append("<div class=\"title\">儿童预防接种记录</div>");
                            s.Append("<div class=\"tr_01 tr_01a clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">儿童编码</li>");
                            s.Append("<li class=\"a5\">" + sdr0["ChildBm"].ToString() + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">身份证号</li>");
                            s.Append("<li class=\"a5\">" + sdr0["ChildCID"].ToString() + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">出生证号</li>");
                            s.Append("<li class=\"a5\">" + sdr0["BirthCerNo"].ToString() + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">儿童姓名</li>");
                            s.Append("<li class=\"a2\">" + sdr0["ChildCID"].ToString() + "</li>");
                            s.Append("<li class=\"a3\">性别</li>");
                            string ChildSex = "";
                            if (sdr0["ChildSex"].ToString() == "0") { ChildSex = "女"; }
                            if (sdr0["ChildSex"].ToString() == "1") { ChildSex = "男"; }
                            s.Append("<li class=\"a4\">" + ChildSex + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">出生日期</li>");
                            s.Append("<li class=\"a5\">" + GetDateFormat(sdr0["BirthDay"].ToString(), "") + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">出生医院</li>");
                            s.Append("<li class=\"a2\">" + sdr0["BornYY"].ToString() + "</li>");
                            s.Append("<li class=\"a3\">出生体重</li>");
                            s.Append("<li class=\"a4\">" + sdr0["WeightBorn"].ToString() + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">监护人姓名</li>");
                            s.Append("<li class=\"a2\">" + sdr0["MotherName"].ToString() + "</li>");
                            s.Append("<li class=\"a3\">与儿童关系</li>");
                            s.Append("<li class=\"a4\">母子</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">家庭住址</li>");
                            s.Append("<li class=\"a5\">" + sdr0["CurAreaName"].ToString().Replace("&", "&nbsp;") + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">户籍地址</li>");
                            s.Append("<li class=\"a5\">" + sdr0["RegAreaName"].ToString().Replace("&", "&nbsp;") + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a1\">过敏史</li>");
                            s.Append("<li class=\"a2\">" + sdr0["History_Gm"].ToString() + "</li>");
                            s.Append("<li class=\"a3\">接种禁忌</li>");
                            s.Append("<li class=\"a4\">" + sdr0["Jzjj"].ToString() + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");
                            s.Append("<div class=\"tr_01 clearfix\">");
                            s.Append("<ul>");
                            s.Append("<li class=\"a6\">接种单位<br />联系电话</li>");
                            s.Append("<li class=\"a5\">" + sdr0["JzCompy"].ToString()+ "<br>" + sdr0["JzTel"].ToString() + "</li>");
                            s.Append("</ul>");
                            s.Append("</div>");

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
                    CommPage.SetBizLog(bizID, m_UserID, "业务打印", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 打印了01505");
                    //保存打印的证件
                    join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                    log.BizID = bizID;
                    log.BizCode = "0150";
                    log.SetCertificateLog("01505", helpbiz_Fileds01, PersonCids, "打印了01505");
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

