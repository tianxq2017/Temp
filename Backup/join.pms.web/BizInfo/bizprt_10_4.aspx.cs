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
    public partial class bizprt_10_4 : System.Web.UI.Page
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
        private string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                ShowBizInfo(m_ObjID);
                if (m_ActionName == "view" || m_ActionName == "viewDetails")
                {
                    //BIZ_Docs doc = new BIZ_Docs();
                    //this.LiteralDocs.Text = doc.GetBizDocsForView(m_ObjID);
                    //doc = null;
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
            string photos = string.Empty;
            string cerNo = "", workFlowInfo = string.Empty;
            StringBuilder s = new StringBuilder();
            try
            {
                NHS_Hqyxjc hqyxjc = new NHS_Hqyxjc(bizID);
                if (!string.IsNullOrEmpty(hqyxjc.VisitBH))
                {

                    //s.Append("<!--男方 女方相同 -->");
                    s.Append("<div class=\"print_04\">");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<ul>");
                    //处理管理员所在地
                    s.Append("<li class=\"a1\">" + BIZ_Common.GetAreaName(hqyxjc.UserAreaCode.Substring(0, 4) + "00000000", "") + "</li>");
                    s.Append("<li class=\"a2\">" + BIZ_Common.GetAreaName(hqyxjc.UserAreaCode.Substring(0, 6) + "000000", "") + "</li>");
                    s.Append("<li class=\"a3\">" + hqyxjc.VisitBH + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">" + hqyxjc.NameB + "</li>");
                    s.Append("<li class=\"a2\">" + GetDateFormat(hqyxjc.BirthdayB.ToString(), "1").Replace("年", "<i></i>").Replace("月", "<b></b>").Replace("日", "") + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">"+CommBiz.GetSexByID(hqyxjc.CIDB)+"</li>");
                    s.Append("<li class=\"a2\">" + hqyxjc.MinZuB + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_04\">" + hqyxjc.CIDB + "</div>");
                    s.Append("<div class=\"tr_05\">" + hqyxjc.WorkUnitB + "　　" + CommPage.GetSingleVal("SELECT [ParaValue]  FROM [dbo].[SYS_Params] where ParaCode='" + hqyxjc.ZhiYeB + "'") + "</div>");
                    s.Append("<div class=\"tr_05\">" + hqyxjc.CurrentAreaNameB + "</div>");
                    s.Append("<div class=\"tr_06\">" + hqyxjc.NameA + "</div>");
                    string xueyuan = "无,有";
                    string[] arr_xy = new string[2];
                    for (int i = 0; i < xueyuan.Split(',').Length; i++)
                    {
                        if (xueyuan.Split(',')[i] == hqyxjc.XueYuan.Trim())
                        {
                            arr_xy[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_xy[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_07 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_xy[0] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_xy[1] + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    string visitjieguo = "";
                    if ("异常情况" == hqyxjc.VisitJieGuo.Trim())
                    {
                        if (!string.IsNullOrEmpty(hqyxjc.YiChang))
                        {
                            visitjieguo = ":" + hqyxjc.YiChang;
                        }
                    }
                    if (!string.IsNullOrEmpty(hqyxjc.JibingZhenduan))
                    {
                        visitjieguo += "<br>疾病诊断：" + hqyxjc.JibingZhenduan;
                    }
                    s.Append("<div class=\"tr_08 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li>" + hqyxjc.VisitJieGuo.Trim() + visitjieguo + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    string yij = "1,2,3,4,5";
                    string[] arr_yij = new string[5];
                    for (int i = 0; i < yij.Split(',').Length; i++)
                    {
                        if (yij.Split(',')[i] == hqyxjc.YiXueYiJian.Trim())
                        {
                            arr_yij[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_yij[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_09 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_yij[0] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_yij[1] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_yij[2] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_yij[3] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_yij[4] + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_10\">" + hqyxjc.Doctor4 + "</div>");
                    s.Append("<div class=\"tr_11 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">" + GetDateFormat(hqyxjc.CheckDate.ToString(), "1").Replace("年", "<i></i>").Replace("月", "<b></b>").Replace("日", "") + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("</div>");


                    if (m_ActionName == "priint")
                    {
                        CommPage.SetBizLog(bizID, m_UserID, "业务打印", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 打印了男/女性婚前检查证明表四");
                        //保存打印的证件
                        //join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                        //log.BizID = bizID;
                        //log.BizCode = biz.BizCode;
                        //log.SetCertificateLog(biz.BizName, biz.Fileds01, biz.PersonCidA, "男/女性婚前检查证明表四");
                        //log = null;
                    }
                }
                hqyxjc = null;
            }
            catch { }

            this.LiteralBizInfo.Text = s.ToString(); ;
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

