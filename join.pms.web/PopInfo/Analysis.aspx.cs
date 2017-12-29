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

using System.Data.SqlClient;
using UNV.Comm.DataBase;
using UNV.Comm.Web;
using System.Text;
using System.Xml;
using System.IO;

namespace AreWeb.OnlineCertificate.PopInfo
{
    public partial class Analysis : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        public string m_TargetUrl;
        private string m_FuncCode;

        private string m_UserID; // 当前登录的操作用户编号
        private string m_SqlParams;

        private string m_StatusVal;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            //ValidateParams();
            SetOpration(m_FuncCode);

            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                
            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

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
            m_SourceUrl = Request.QueryString["sourceUrl"];
            if (!string.IsNullOrEmpty(m_SourceUrl))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }
        }
        // -
        private void SetOpration(string funcNo)
        {
            string funcName = "共享数据分析比对";
            try
            {

                //读取当前执行状态 数据分析比对：0,默认；1,执行
                m_SqlParams = "SELECT ParaValue FROM SYS_Params WHERE ParaCate=7 AND ParaCode='7001'";
                m_StatusVal = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                if (m_StatusVal == "0") {
                    this.LabelMsg.Text = "当前状态：空闲中……"; 
                } 
                else {
                    this.LabelMsg.Text = "当前状态：正在执行数据分析比对……"; 
                }
            }
            catch 
            {
                this.LabelMsg.Text = "操作失败：尚未设置数据比对参数！";
                return;
            }

            this.LiteralNav.Text = "管理首页  &gt;&gt; " + funcName + "：";
        }

        protected void butAnalysis_Click(object sender, EventArgs e)
        {
            if (m_StatusVal == "0")
            {
                this.LabelMsg.Text = "当前状态：开始执行比对分析……";
                try
                {

                    //读取当前执行状态 数据分析比对：0,默认；1,执行
                    m_SqlParams = "UPDATE SYS_Params SET ParaValue='1' WHERE ParaCate=7 AND ParaCode='7001'";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    this.LabelMsg.Text = "当前状态：成功向服务器发送比对分析指令…… <br/>根据数据量大小，分析过程将持续几分钟至半小时左右后……<br/>您可以喝杯茶或者休息一会再返回系统查看分析结果。";
                }
                catch
                {
                    this.LabelMsg.Text = "操作失败：向服务器发送分析指令出错！";
                }
            }
            else
            {
                this.LabelMsg.Text = "当前状态：正在执行数据分析比对…… 在本次分析未完成之前，禁止进行重复操作！";
            }

        }

        
    }
}
