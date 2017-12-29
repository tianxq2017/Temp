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
    public partial class bizprt_0_2 : System.Web.UI.Page
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

                    s.Append("<div class=\"print_nv_02\">");
                    s.Append("<div class=\"print_table\">");
                    s.Append("<div class=\"print_table_bg\">");
                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<ul>");
                    string xueya = hqyxjc.XueYa.Replace("-", "/").Replace(",", "/").Replace("，", "/").Trim();
                    if (xueya.IndexOf("/") == -1)
                    {
                        xueya += "/";
                    }
                    s.Append("<li class=\"a1\">" + xueya.Split('/')[0] + "</li>");
                    s.Append("<li class=\"a2\">" + xueya.Split('/')[1] + "</li>");
                    string tt = "无,有";
                    string[] arr_tt = new string[2];
                    for (int i = 0; i < tt.Split(',').Length; i++)
                    {
                        if (tt.Split(',')[i] == hqyxjc.TeSheTiTai.Trim())
                        {
                            arr_tt[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_tt[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<li class=\"tick a3\">" + arr_tt[0] + "</li>");
                    s.Append("<li class=\"tick a4\">" + arr_tt[1] + "</li>");
                    s.Append("<li class=\"a5\">" + hqyxjc.TeSheTiTai_other + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    string jszt = "正常,异常";
                    string[] arr_jszt = new string[2];
                    for (int i = 0; i < jszt.Split(',').Length; i++)
                    {
                        if (jszt.Split(',')[i] == hqyxjc.Jingshenzhuangtai.Trim())
                        {
                            arr_jszt[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_jszt[i] = "<p>√</p>";
                        }
                    }
                    string tsmr = "无,有";
                    string[] arr_tsmr = new string[2];
                    for (int i = 0; i < tsmr.Split(',').Length; i++)
                    {
                        if (tsmr.Split(',')[i] == hqyxjc.TeshuMianrong.Trim())
                        {
                            arr_tsmr[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_tsmr[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_jszt[0] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_jszt[1] + "</li>");
                    s.Append("<li class=\"a3\">" + hqyxjc.TeSheTiTai_other + "</li>");
                    s.Append("<li class=\"tick a4\">" + arr_tsmr[0] + "</li>");
                    s.Append("<li class=\"tick a5\">" + arr_tsmr[1] + "</li>");
                    s.Append("<li class=\"a6\">" + hqyxjc.TeshuMianrong_other + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    string zhili = "正常,异常,常识,判断,记忆,计算";
                    string[] arr_zhili = new string[6];
                    for (int i = 0; i < zhili.Split(',').Length; i++)
                    {
                        if (zhili.Split(',')[i] == hqyxjc.ZhiLi.Trim())
                        {
                            arr_zhili[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_zhili[i] = "<p>√</p>";
                        }
                    }
                    string maofa = "正常,异常";
                    string[] arr_maofa = new string[2];
                    for (int i = 0; i < maofa.Split(',').Length; i++)
                    {
                        if (maofa.Split(',')[i] == hqyxjc.Pifumaofa.Trim())
                        {
                            arr_maofa[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_maofa[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_03 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_zhili[0] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_zhili[1] + "</li>");
                    s.Append("<li class=\"tick a3\">" + arr_zhili[2] + "</li>");
                    s.Append("<li class=\"tick a3\">" + arr_zhili[3] + "</li>");
                    s.Append("<li class=\"tick a3\">" + arr_zhili[4] + "</li>");
                    s.Append("<li class=\"tick a3\">" + arr_zhili[5] + "</li>");
                    s.Append("<li class=\"tick a4\">" + arr_maofa[0] + "</li>");
                    s.Append("<li class=\"tick a5\">" + arr_maofa[1] + "</li>");
                    s.Append("<li class=\"a6\">" + hqyxjc.Pifumaofa_other + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");

                    string wg = "正常,异常";
                    string[] arr_wg = new string[2];
                    for (int i = 0; i < wg.Split(',').Length; i++)
                    {
                        if (wg.Split(',')[i] == hqyxjc.Wuguan.Trim())
                        {
                            arr_wg[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_wg[i] = "<p>√</p>";
                        }
                    }
                    string jzx = "正常,异常";
                    string[] arr_jzx = new string[2];
                    for (int i = 0; i < jzx.Split(',').Length; i++)
                    {
                        if (jzx.Split(',')[i] == hqyxjc.Jiazhuangxian.Trim())
                        {
                            arr_jzx[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_jzx[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_04 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_wg[0] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_wg[1] + "</li>");
                    s.Append("<li class=\"a3\">" + hqyxjc.Wuguan_other + "</li>");
                    s.Append("<li class=\"tick a4\">" + arr_jzx[0] + "</li>");
                    s.Append("<li class=\"tick a5\">" + arr_jzx[1] + "</li>");
                    s.Append("<li class=\"a6\">" + hqyxjc.Jiazhuangxian_other + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    string zy = "无,有";
                    string[] arr_zy = new string[2];
                    for (int i = 0; i < zy.Split(',').Length; i++)
                    {
                        if (zy.Split(',')[i] == hqyxjc.ZaYin.Trim())
                        {
                            arr_zy[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_zy[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_05 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">" + hqyxjc.Xinlv1 + "</li>");
                    s.Append("<li class=\"a2\">" + hqyxjc.Xinlv2 + "</li>");
                    s.Append("<li class=\"tick a3\">" + arr_zy[0] + "</li>");
                    s.Append("<li class=\"tick a4\">" + arr_zy[1] + "</li>");
                    s.Append("<li class=\"a5\">" + hqyxjc.ZaYin_other + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    string fei = "正常,异常";
                    string[] arr_fei = new string[2];
                    for (int i = 0; i < fei.Split(',').Length; i++)
                    {
                        if (fei.Split(',')[i] == hqyxjc.Fei.Trim())
                        {
                            arr_fei[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_fei[i] = "<p>√</p>";
                        }
                    }
                    string gan = "未及,可及";
                    string[] arr_gan = new string[2];
                    for (int i = 0; i < gan.Split(',').Length; i++)
                    {
                        if (gan.Split(',')[i] == hqyxjc.Gan.Trim())
                        {
                            arr_gan[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_gan[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_06 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_fei[0] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_fei[1] + "</li>");
                    s.Append("<li class=\"a3\">" + hqyxjc.Fei_other + "</li>");
                    s.Append("<li class=\"tick a4\">" + arr_gan[0] + "</li>");
                    s.Append("<li class=\"tick a5\">" + arr_gan[1] + "</li>");
                    s.Append("<li class=\"a6\">" + hqyxjc.Gan_other + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    string sz = "正常,异常";
                    string[] arr_sz = new string[2];
                    for (int i = 0; i < sz.Split(',').Length; i++)
                    {
                        if (sz.Split(',')[i] == hqyxjc.SizhiJizhu.Trim())
                        {
                            arr_sz[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_sz[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_07 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_sz[0] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_sz[1] + "</li>");
                    s.Append("<li class=\"a3\">" + hqyxjc.SizhiJizhu_other + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_08\">");
                    s.Append("<div class=\"sum\">" + hqyxjc.Qita1 + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_09\">");
                    s.Append("<ul>");
                    s.Append("<li>" + hqyxjc.Doctor2 + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");


                    string ym = "正常,稀少,无";
                    string[] arr_ym = new string[3];
                    for (int i = 0; i < ym.Split(',').Length; i++)
                    {
                        if (ym.Split(',')[i] == hqyxjc.Yinmao.Trim())
                        {
                            arr_ym[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_ym[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_10 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_ym[0] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_ym[1] + "</li>");
                    s.Append("<li class=\"tick a3\">" + arr_ym[2] + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    string rf = "正常,异常";
                    string[] arr_rf = new string[2];
                    for (int i = 0; i < rf.Split(',').Length; i++)
                    {
                        if (rf.Split(',')[i] == hqyxjc.Rufang.Trim())
                        {
                            arr_rf[i] = "<p class=\"ok\">√</p>";
                        }
                        else
                        {
                            arr_rf[i] = "<p>√</p>";
                        }
                    }
                    s.Append("<div class=\"tr_11 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"tick a1\">" + arr_ym[0] + "</li>");
                    s.Append("<li class=\"tick a2\">" + arr_ym[1] + "</li>");
                    s.Append("<li class=\"a3\">" + hqyxjc.Rufang_other + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_12 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">" + hqyxjc.WaiYin + "</li>");
                    s.Append("<li class=\"a2\">" + hqyxjc.FenMiWu + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_12 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">" + hqyxjc.ZiGong + "</li>");
                    s.Append("<li class=\"a2\">" + hqyxjc.FuJian + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_13 clearfix\">");
                    s.Append("<ul>");
                    s.Append("<li class=\"a1\">" + hqyxjc.WaiYin_by + "</li>");
                    s.Append("<li class=\"a2\">" + hqyxjc.YinDao_by + "</li>");
                    s.Append("<li class=\"a3\">" + hqyxjc.Gongjing_by + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_14\">" + hqyxjc.ZiGong_by + "</div>");
                    s.Append("<div class=\"tr_14\">" + hqyxjc.FuJian_bu + "</div>");
                    s.Append("<div class=\"tr_15\">" + hqyxjc.Qita2 + "</div>");
                    s.Append("<div class=\"tr_16\">");
                    s.Append("<ul>");
                    s.Append("<li>" + hqyxjc.Doctor3 + "</li>");
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("</div>");
                    s.Append("</div>");


                    if (m_ActionName == "priint")
                    {
                        CommPage.SetBizLog(bizID, m_UserID, "业务打印", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 打印了女性婚前检查表二");
                        //保存打印的证件
                        //join.pms.dal.BizWorkFlows log = new join.pms.dal.BizWorkFlows();
                        //log.BizID = bizID;
                        //log.BizCode = biz.BizCode;
                        //log.SetCertificateLog(biz.BizName, biz.Fileds01, biz.PersonCidA, "女性婚前检查表二");
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

