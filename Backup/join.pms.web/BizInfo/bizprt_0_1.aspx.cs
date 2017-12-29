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
    public partial class bizprt_0_1 : System.Web.UI.Page
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

                    s.Append("<div class=\"print_nv_01\">");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<div class=\"tr_01\">" + GetDateFormat(hqyxjc.TianxieDate.ToString(), "1").Replace("年", "<i></i>").Replace("月", "<b></b>").Replace("日", "") + "</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">" + hqyxjc.NameB + "</li>");
                    s.Append("<li class=\"a2\">" + GetDateFormat(hqyxjc.BirthdayB.ToString(), "1").Replace("年", "<i></i>").Replace("月", "<b></b>").Replace("日", "") + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03\">" + hqyxjc.CIDB + "</div>");
                    s.Append("<div class=\"tr_04 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">" + CommPage.GetSingleVal("SELECT [ParaValue]  FROM [dbo].[SYS_Params] where ParaCode='" + hqyxjc.ZhiYeB + "'") + "</li>");
                    s.Append("<li class=\"a2\">" + CommPage.GetSingleVal("SELECT [ParaValue]  FROM [dbo].[SYS_Params] where ParaCode='" + hqyxjc.WenhuaB + "'") + "</li>");
                    s.Append("<li class=\"a3\">"+hqyxjc.MinZuB+"</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_05 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">" + BIZ_Common.GetAreaName(hqyxjc.RegisterAreaCodeB.Substring(0, 2) + "0000000000", "") + "</li>");
                    s.Append("<li class=\"a2\">" + BIZ_Common.GetAreaName(hqyxjc.RegisterAreaCodeB.Substring(0, 4) + "00000000", "") + "</li>");
                    s.Append("<li class=\"a3\">" + BIZ_Common.GetAreaName(hqyxjc.RegisterAreaCodeB.Substring(0, 6) + "000000", "") + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_06\">" + BIZ_Common.GetAreaName(hqyxjc.RegisterAreaCodeB.Substring(0, 9) + "000", "") + "</div>");
                    s.Append("<div class=\"tr_07 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">" + hqyxjc.CurrentAreaNameB + "</li>");
                    s.Append("<li class=\"a2\">"+hqyxjc.CurrentAreaYB+"</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_07 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">" + hqyxjc.WorkUnitB + "</li>");
                    s.Append("<li class=\"a2\">" + hqyxjc.TelB + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_08 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">" + hqyxjc.NameA + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_09\">" + hqyxjc.VisitBH + "</div>");
                    s.Append("<div class=\"tr_10\">" + GetDateFormat(hqyxjc.CheckDate.ToString(), "1").Replace("年", "<i></i>").Replace("月", "<b></b>").Replace("日", "") + "</div>");

                    string xueyuan = "无,表,堂,其他";
                    string[] arr_xy = new string[4];
                    for (int i = 0; i < xueyuan.Split(',').Length; i++)
                    {
                        if (xueyuan.Split(',')[i]==hqyxjc.XueYuan.Trim())
                        {
                            arr_xy[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_xy[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_11 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_xy[0] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_xy[1] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_xy[2] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_xy[3] + "</li>");
                    s.Append("<li class=\"a3\">" + hqyxjc.XueYuan_other + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    string jws = "," + hqyxjc.History_Jw.Replace(" ", "") + ",";
                    string[] arr_jws = new string[13];
                    for (int i = 0; i < 13; i++)
                    {
                        if (jws.IndexOf("," + i + ",") != -1)
                        {
                            arr_jws[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_jws[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_12 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_jws[0] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_jws[1] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_jws[2] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_jws[3] + "</li>");
                    s.Append("<li class=\"tick a3\">" + arr_jws[4] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_jws[5] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_jws[6] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_jws[7] + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_13 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_jws[8] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_jws[9] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_jws[10] + "</li>");
                    s.Append("<li class=\"tick a3\">" + arr_jws[12] + "</li>");
                    s.Append("<li class=\"tick a4\">" + hqyxjc.History_Jw + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    string shoushu = "无,有";
                    string[] arr_ss = new string[2];
                    for (int i = 0; i < shoushu.Split(',').Length; i++)
                    {
                        if (shoushu.Split(',')[i] == hqyxjc.History_ss.Trim())
                        {
                            arr_ss[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_ss[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_14 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_ss[0] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_ss[1] + "</li>");
                    s.Append("<li class=\"a3\">" + hqyxjc.History_ss_other + "</li>");
                    s.Append("<li class=\"a4\">" + hqyxjc.History_other + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    string binshi = "无,有";
                    string[] arr_xbs = new string[2];
                    for (int i = 0; i < binshi.Split(',').Length; i++)
                    {
                        if (binshi.Split(',')[i] == hqyxjc.History_now.Trim())
                        {
                            arr_xbs[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_xbs[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_15 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_xbs[0] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_xbs[1] + "</li>");
                    s.Append("<li class=\"a3\">" + hqyxjc.History_now_other + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");

                    string yuejingliang = "多,中,少";
                    string[] arr_yjl = new string[3];
                    for (int i = 0; i < yuejingliang.Split(',').Length; i++)
                    {
                        if (yuejingliang.Split(',')[i] == hqyxjc.Yuejingliang.Trim())
                        {
                            arr_yjl[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_yjl[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_22 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">" + hqyxjc.ChuchaoAge + "</li>");
                    s.Append("<li class=\"a2\">" + hqyxjc.YuejingWeek + "</li>");
                    s.Append("<li class=\"tick a3\">" + arr_yjl[0] + "</li>");
                    s.Append("<li class=\"tick a4\">" + arr_yjl[1] + "</li>");
                    s.Append("<li class=\"tick a5\">" + arr_yjl[2] + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    string tongjing = "无,轻,中,重";
                    string[] arr_tj = new string[4];
                    for (int i = 0; i < tongjing.Split(',').Length; i++)
                    {
                        if (tongjing.Split(',')[i] == hqyxjc.TongJing.Trim())
                        {
                            arr_tj[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_tj[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_23 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_tj[0] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_tj[1] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_tj[2] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_tj[3] + "</li>");
                    s.Append("<li class=\"a3\">" + GetDateFormat(hqyxjc.MoCiYueJing.ToString(), "1").Replace("年", "<i></i>").Replace("月", "<b></b>").Replace("日", "") + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");

                    string Jwhy = "无,有,丧偶,离异";
                    string[] arr_Jwhy = new string[4];
                    for (int i = 0; i < Jwhy.Split(',').Length; i++)
                    {
                        if (Jwhy.Split(',')[i] == hqyxjc.History_Jwhy.Trim())
                        {
                            arr_Jwhy[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_Jwhy[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_16 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_Jwhy[0] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_Jwhy[1] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_Jwhy[2] + "</li>");
                    s.Append("<li class=\"tick a3\">" + arr_Jwhy[3] + "</li>");
                    string ZuYueZaoChanLiuChan = hqyxjc.ZuYueZaoChanLiuChan;
                    if (ZuYueZaoChanLiuChan.IndexOf(",") == -1)
                    {
                        ZuYueZaoChanLiuChan += ",,";
                    }
                    s.Append("<li class=\"a4\">" + ZuYueZaoChanLiuChan.Split(',')[0] + "</li>");
                    s.Append("<li class=\"a5\">" + ZuYueZaoChanLiuChan.Split(',')[1] + "</li>");
                    s.Append("<li class=\"a6\">" + ZuYueZaoChanLiuChan.Split(',')[2] + "</li>");
                    s.Append("<li class=\"a7\">" + (CommPage.replaceNUM(hqyxjc.Child_boynum.Trim()) + CommPage.replaceNUM(hqyxjc.Child_girlnum.Trim())) + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    string jzs = "," + hqyxjc.History_Jz.Replace(" ", "") + ",";
                    string[] arr_jzs = new string[10];
                    for (int i = 0; i < 10; i++)
                    {
                        if (jzs.IndexOf("," + i + ",") != -1)
                        {
                            arr_jzs[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_jzs[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_17 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_jzs[0] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_jzs[1] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_jzs[2] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_jzs[3] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_jzs[4] + "</li>");
                    s.Append("<li class=\"tick a3\">" + arr_jzs[5] + "</li>");
                    s.Append("<li class=\"tick a4\">" + arr_jzs[6] + "</li>");
                    s.Append("<li class=\"tick a5\">" + arr_jzs[7] + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_18 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_jzs[8] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_jzs[9] + "</li>");
                    s.Append("<li class=\"a3\">" + hqyxjc.History_Jz_other + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_19\">" + hqyxjc.HuanzheGX + "</div>");

                    string hunpei = "无,有,父母,祖父母,外祖父母";
                    string[] arr_hunpei = new string[5];
                    for (int i = 0; i < hunpei.Split(',').Length; i++)
                    {
                        if (hunpei.Split(',')[i] == hqyxjc.FamilyHunPei.Trim())
                        {
                            arr_hunpei[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_hunpei[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_20 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_hunpei[0] + "</li>");
                    s.Append("<li class=\"tick a1\">" + arr_hunpei[1] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_hunpei[2] + "</li>");
                    s.Append("<li class=\"tick a3\">" + arr_hunpei[3] + "</li>");
                    s.Append("<li class=\"tick a4\">" + arr_hunpei[4] + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_21 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\"></li>");
                    s.Append("<li class=\"a2\">" + hqyxjc.Doctor1 + "</li>");
                    s.Append("</ul>");
                    s.Append("</div> ");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("</div>");


                    if (m_ActionName == "priint")
                    {
                        CommPage.SetBizLog(bizID, m_UserID, "业务打印", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 打印了女性婚前检查表一");
                        ////保存打印的证件
                        //join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                        //log.BizID = bizID;
                        //log.BizCode = biz.BizCode;
                        //log.SetCertificateLog(biz.BizName, biz.Fileds01, biz.PersonCidA, "女性婚前检查表一");
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

