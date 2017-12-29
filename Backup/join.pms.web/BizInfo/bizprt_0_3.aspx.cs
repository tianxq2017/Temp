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
    public partial class bizprt_0_3 : System.Web.UI.Page
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
                    string jcjg = "未见异常,异常情况";
                    string[] arr_jcjg = new string[2];
                    for (int i = 0; i < jcjg.Split(',').Length; i++)
                    {
                        if (jcjg.Split(',')[i] == hqyxjc.VisitJieGuo.Trim())
                        {
                            arr_jcjg[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_jcjg[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"print_nv_03\">");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_jcjg[0] + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01 tr_01b clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_jcjg[1] + "</li>");
                    s.Append("<li class=\"a2\">" + hqyxjc.YiChang + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_01 tr_01c clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\"></li>");
                    s.Append("<li class=\"a2\">" + hqyxjc.JibingZhenduan + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    string yij = "4,3,2,1,5";
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
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_yij[0] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_yij[1] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_yij[2] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_yij[3] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_yij[4] + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">" + hqyxjc.HunQianZIXun + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    string zdyj = "1,2";
                    string[] arr_zdyj = new string[2];
                    for (int i = 0; i < zdyj.Split(',').Length; i++)
                    {
                        if (zdyj.Split(',')[i] == hqyxjc.ZhiDaoJieGuo.Trim())
                        {
                            arr_zdyj[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_zdyj[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_04 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_zdyj[0] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_zdyj[1] + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_05 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">" + hqyxjc.ZhuanZhengYiYuan + "</li>");
                    s.Append("<li class=\"a2\">" + GetDateFormat(hqyxjc.ZhuanZhengDate.ToString(), "1").Replace("年", "<i></i>").Replace("月", "<b></b>").Replace("日", "") + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_06\">" + GetDateFormat(hqyxjc.YuYueDate.ToString(), "1").Replace("年", "<i></i>").Replace("月", "<b></b>").Replace("日", "") + "</div>");
                    s.Append("<div class=\"tr_07\">" + GetDateFormat(hqyxjc.ChuJuDate.ToString(), "1").Replace("年", "<i></i>").Replace("月", "<b></b>").Replace("日", "") + "</div>");
                    s.Append("<div class=\"tr_08 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li>" + hqyxjc.Doctor4 + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("</div>");


                    if (m_ActionName == "priint")
                    {
                        CommPage.SetBizLog(bizID, m_UserID, "业务打印", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 打印了女性婚前检查表三");
                        //保存打印的证件
                        //join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                        //log.BizID = bizID;
                        //log.BizCode = biz.BizCode;
                        //log.SetCertificateLog(biz.BizName, biz.Fileds01, biz.PersonCidA, "女性婚前检查表三");
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

