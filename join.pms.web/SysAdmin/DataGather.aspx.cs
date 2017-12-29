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

namespace join.pms.web.SysAdmin
{
    public partial class DataGather : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        public string m_TargetUrl;
        private string m_FuncCode;

        private string m_UserID; // 当前登录的操作用户编号
        private DataSet m_Ds;
        private DataTable m_Dt;
        private string m_SqlParams;

        private string m_FuncInfo;
        private string m_Titles;
        private string m_Fields;
        private string m_UpFileName;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                SetOpration(m_FuncCode);
                m_TargetUrl = "/UnvCommList.aspx?1=1&FuncCode=0501&FuncNa=%e7%be%a4%e4%bc%97%e5%9f%ba%e6%9c%ac%e4%bf%a1%e6%81%af";
                this.LabelMsg.Text = "已经开启数据采集窗体……<br/>采集完毕，请点击“返回”按钮操作其它功能……";

            }
        }

        //private static string[] Remove(string[] array, int index)
        //{
        //    int length = array.Length;
        //    string[] result = new string[length - 1];
        //    Array.Copy(array, result, index);
        //    Array.Copy(array, index + 1, result, index, length - index - 1);
        //    return result;
        //}

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile =  "/css/inidex.css";

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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/loginTemp.aspx';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 验证接受的参数
        /// </summary>
        private void ValidateParams()
        {
            //m_SourceUrl = Request.QueryString["sourceUrl"];
            //if (!string.IsNullOrEmpty(m_SourceUrl))
            //{
            //    m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
            //    m_TargetUrl = "/UnvCommList.aspx?" + m_SourceUrlDec;
            //    m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
            //}
            //else
            //{
            //    Response.Write("非法访问，操作被终止！");
            //    Response.End();
            //}
            m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
        }
        // ------
        private void SetOpration(string funcNo)
        {
            string funcName = string.Empty;
            try
            {
                switch (funcNo)
                {

                    case "020301":
                        funcName = "国家婚姻登记信息采集";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                this.LabelMsg.Text = "操作失败：<br/><br/>" + ex.Message; // " + m_SqlParams + "
            }

            this.LiteralNav.Text = "管理首页  &gt;&gt; 数据采集 ：";
        }



    }
}


