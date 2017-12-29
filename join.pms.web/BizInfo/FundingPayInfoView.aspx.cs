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

using System.Globalization;
using System.IO;
using System.Text;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;
namespace join.pms.web.BizInfo
{
    public partial class FundingPayInfoView : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号
        private string m_UserName;

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;
        private string m_BizStep;
        private string m_AreaCode;
        private string m_AreaName;
        private BIZ_Persons m_PerA;//储存男方信息
        private BIZ_Persons m_PerB;//储存女方信息
        private BIZ_Contents m_BizC;//业务信息
        private BIZ_PersonChildren m_Children;//子女信息

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetOpratetionAction(m_NavTitle);
            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "";// DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
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
            else { GetUserInfo(); }
        }
        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        private void GetUserInfo()
        {
            SqlDataReader sdr = null;
            try
            {
                string sqlParams = "SELECT UserAccount,UserName,UserAreaCode FROM USER_BaseInfo WHERE UserID=" + m_UserID;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        m_UserName = sdr["UserAccount"].ToString() + "(" + sdr["UserName"].ToString() + ")";
                        //m_AreaCode = sdr["UserAreaCode"].ToString();
                        //m_AreaName = GetAreaName(m_AreaCode, "0");
                    }
                }
            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }
        }

        /// <summary>
        /// 验证接受的参数
        /// </summary>
        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetTrim(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetTrim(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetTrim(Request.QueryString["k"]);

            m_AreaCode = PageValidate.GetTrim(Request.QueryString["x"]);
            m_AreaName = BIZ_Common.GetAreaName(m_AreaCode, "0");
            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "/UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                //m_NavTitle = CommPage.GetSingleVal("SELECT BizNameFull FROM BIZ_Categories WHERE BizCode='" + m_FuncCode + "'");
                m_NavTitle = "资金信息采集";
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        /// <summary>
        /// 设置操作行为
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            switch (m_ActionName)
            {
                case "add": // 新增
                    funcName = "新增";
                    break;
                case "view": // 查看
                    funcName = "查看";
                    ShowModInfo(m_ObjID);
                    break;
                case "cb": // 催办
                    funcName = "催办";
                    SetReminder(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">管理首页</a> &gt;&gt; 资金管理 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }

        #endregion

        /// <summary>
        /// 催办
        /// </summary>
        /// <param name="objID"></param>
        private void SetReminder(string objID)
        {

        }

        /// <summary>
        /// 查看、修改
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            string ItemPhotos = string.Empty;
            SqlDataReader sdr = null;
            DataTable m_Dt_1 = null;
            DataTable m_Dt_2 = null;
            DataTable m_Dt_3 = null;
            try
            {
                m_SqlParams = "SELECT * FROM v_ProjectInfo_ProjectFundingPayInfo WHERE P_ID=" + objID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    this.ltr_P_Name.Text = PageValidate.Decode(PageValidate.GetTrim(sdr["P_Name"].ToString()));
                    try
                    {
                        this.ltr_F_PayDateTime.Text = PageValidate.Decode(PageValidate.GetTrim(sdr["F_PayDateTime"].ToString()));
                    }
                    catch
                    {
                        this.ltr_F_PayDateTime.Text = "";
                    }
                    
                    this.ltr_F_PayAmount.Text = PageValidate.Decode(PageValidate.GetTrim(sdr["F_PayAmount"].ToString()));

                    //根据项目Id得到项目内容
                    StringBuilder sb_1 = new StringBuilder();
                    string sql_1 = "SELECT * FROM BIZ_ProjectUseUnit WHERE P_ID=" + PageValidate.GetTrim(sdr["P_ID"].ToString()) + " And FuncNo = '" + m_FuncCode + "'";
                    m_Dt_1 = new DataTable();
                    m_Dt_1 = DbHelperSQL.Query(sql_1).Tables[0];
                    sb_1.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    for (int i = 0; i < m_Dt_1.Rows.Count; i++)
                    {
                        sb_1.Append("<tr>");
                        if (i == 0)
                        {
                            sb_1.Append("<th style=\"width: 98px\">使用单位：</th>");
                        }
                        else
                        {
                            sb_1.Append("<th style=\"width: 98px\">使用单位" + i + "：</th>");
                        }
                        sb_1.Append("<td class=\"text\">");
                        sb_1.Append(m_Dt_1.Rows[i]["U_Name"].ToString());
                        sb_1.Append("</td>");
                        if (i == 0)
                        {
                            sb_1.Append("<th style=\"width: 98px\">金额明细：</th>");
                        }
                        else
                        {
                            sb_1.Append("<th style=\"width: 98px\">金额明细" + i + "：</th>");
                        }
                        sb_1.Append("<td class=\"text\">");
                        sb_1.Append(String.Format("{0:N}", m_Dt_1.Rows[i]["U_MoneyInfo"].ToString()));
                        sb_1.Append("</td>");
                        sb_1.Append("</tr>");
                    }
                    sb_1.Append("</table>");
                    this.ltr_ProjectInfo.Text = sb_1.ToString();

                    //支付计划
                    StringBuilder sb_2 = new StringBuilder();
                    string sql_2 = "SELECT * FROM BIZ_ProjectPaymentPlan WHERE P_ID=" + PageValidate.GetTrim(sdr["P_ID"].ToString()) + " And FuncNo = '" + m_FuncCode + "'";
                    m_Dt_2 = new DataTable();
                    m_Dt_2 = DbHelperSQL.Query(sql_2).Tables[0];
                    sb_2.Append("<table width=\"900px\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    sb_2.Append("<tr>");
                    sb_2.Append("<th style=\"width: 100px\"><span class=\"xing\">*</span>支付计划：</th>");
                    sb_2.Append("<td class=\"text\" colspan=\"6\">" + PageValidate.Decode(PageValidate.GetTrim(sdr["F_PaymentPlan"].ToString())) + "</td>");
                    sb_2.Append("</tr>");
                    for (int k = 0; k < m_Dt_2.Rows.Count; k++)
                    {
                        sb_2.Append("<tr>");
                        if (k == 0)
                        {
                            sb_2.Append("<th style=width: 80px>时间：</th>");
                        }
                        else
                        {
                            sb_2.Append("<th style=width: 80px>时间" + k + "：</th>");
                        }
                        sb_2.Append("<td class=\"text\">");
                        sb_2.Append(Convert.ToDateTime(m_Dt_2.Rows[k]["DateTime"].ToString()).ToString("yyyy-MM-dd"));
                        sb_2.Append("</td>");
                        sb_2.Append("<th style=\"width: 120px\">使用单位：</th>");
                        sb_2.Append("<td class=\"text\">");
                        sb_2.Append(m_Dt_2.Rows[k]["UnitOfUse"].ToString());
                        sb_2.Append("</td>");
                        if (k == 0)
                        {
                            sb_2.Append("<th style=\"width: 70px\">金额：</th>");
                        }
                        else
                        {
                            sb_2.Append("<th style=\"width: 70px\">金额" + k + "：</th>");
                        }
                        sb_2.Append("<td class=\"text\">");
                        sb_2.Append(String.Format("{0:N}", m_Dt_2.Rows[k]["AmountOfMoney"].ToString()));
                        sb_2.Append("</td>");
                        sb_2.Append("</tr>");
                    }
                    sb_2.Append("</table>");
                    this.ltr_PaymentPlan.Text = sb_2.ToString();

                    //依据文件 
                    StringBuilder sb_3 = new StringBuilder();
                    string sql_3 = "SELECT top 3 * FROM BIZ_ProjectInfo_Docs WHERE P_ID=" + PageValidate.GetTrim(sdr["P_ID"].ToString()) + " Order by OprateDate desc";
                    m_Dt_3 = new DataTable();
                    m_Dt_3 = DbHelperSQL.Query(sql_3).Tables[0];
                    for (int j = 0; j < m_Dt_3.Rows.Count; j++)
                    {
                        sb_3.Append("<li style=\"width:30%\"><a href=\"" + m_Dt_3.Rows[j]["DocsPath"].ToString() + "\"><img width=\"300px\" height=\"180px\" src=\"" + m_Dt_3.Rows[j]["DocsPath"].ToString() + "\"/></a></li>");
                    }
                    this.ltr_Img.Text = sb_3.ToString();
                }
                sdr.Close();
            }
            catch
            {
                if (sdr != null) sdr.Close();
            }
        }

        private void GetCmsDocs(string objID, string defaultPic)
        {
            m_SqlParams = "SELECT DocsPath,SourceName FROM [CMS_Docs] WHERE CmsID=" + objID + " AND (DocsType='.jpg' OR DocsType='.gif')";
            //this.LiteralDocs.Text = CommDal.CreateSelCtrl("txtSelCmsPic", defaultPic, "", m_SqlParams);
        }
    }
}

