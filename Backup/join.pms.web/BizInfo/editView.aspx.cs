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


namespace join.pms.dalInfo
{
    public partial class editView : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_SourceFuncCode;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号

        private string m_SqlParams;
        public string m_TargetUrl;

        DataSet m_Ds;
        private string m_FuncInfo;
        private string m_Titles;
        private string m_Fields;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                SetOpratetionAction("");
                GetFuncInfo(m_FuncCode,m_ObjID);
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
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                m_SourceFuncCode = m_FuncCode;
                //if (m_FuncCode.Length > 2) {
                //    if (m_FuncCode.Substring(0, 2) == "06") m_FuncCode = "02"+m_FuncCode.Substring(2);
                //}
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
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">起始页</a> &gt;&gt; 查看选定数据详细信息 ：";
        }
        /// <summary 
        /// 修改
        /// </summary>
        /// <param name="objID"></param>
        private void ShowInfo(string objID)
        {
            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGrid.config");
            string P_CardID = string.Empty;
            SqlDataReader sdr = null;
            StringBuilder sHtml = new StringBuilder();
            sHtml.Append("您所选定的数据详细信息如下：<br/>");
            sHtml.Append("<table width=\"1024\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" bgcolor=\"#008981\">");
            try
            {
                GetConfigParams(this.m_FuncCode, configFile, ref errMsg);
                string[] a_FuncInfo = this.m_FuncInfo.Split(',');
                string[] a_Titles = this.m_Titles.Split(',');
                string[] a_Fields = this.m_Fields.Split(',');


                sHtml.Append("<tr>");
                for (int i = 0; i < 14; i++)
                {
                    sHtml.Append("<td class=\"fb01\" bgcolor=\"#99d0d0\" style=\"color:#033;\"><strong>" + a_Titles[i] + "</strong></td>");
                }
                sHtml.Append("</tr>");
                m_SqlParams = "SELECT * FROM PIS_Persons WHERE P_ID=" + objID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    P_CardID = PageValidate.GetTrim(sdr["P_CardID"].ToString());
                    sHtml.Append("<tr onmouseover=\"this.className='lvtColDataHover'\" onmouseout=\"this.className='lvtColData'\">");
                    for (int j = 0; j < 14; j++)
                    {

                        sHtml.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + PageValidate.GetTrim(sdr[a_Fields[j]].ToString()) + "</td>");
                    }
                    sHtml.Append("</tr>");
                }

                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }
            sHtml.Append("</table><br/>");
            //sHtml.Append(GetFuncInfo("020401", P_CardID, "(Fileds05='" + P_CardID + "' OR Fileds10='" + P_CardID + "'  OR Fileds14='" + P_CardID + "')", configFile));
            //sHtml.Append(GetFuncInfo("020402", P_CardID, "Fileds05='" + P_CardID + "'", configFile));
            //sHtml.Append(GetFuncInfo("020403", P_CardID, "Fileds05='" + P_CardID + "'", configFile));
            //sHtml.Append(GetFuncInfo("020301", P_CardID, "(Fileds05='" + P_CardID + "' OR Fileds13='" + P_CardID + "')", configFile));
            this.LiteralData.Text = sHtml.ToString();
        }

        /// <summary>
        /// 个案信息的关联信息
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private void GetFuncInfo(string funcNo, string objID)
        {
            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGrid.config");
            SqlDataReader sdr = null;
            StringBuilder sHtml = new StringBuilder();
            try
            {
                GetConfigParams(funcNo, configFile, ref errMsg);
                string[] a_FuncInfo = this.m_FuncInfo.Split(',');
                string[] a_Titles = this.m_Titles.Split(',');
                string[] a_Fields = this.m_Fields.Split(',');

                if (funcNo == "0501" || funcNo == "060101") {
                    m_SqlParams = "SELECT * FROM [v_PisQyk] WHERE CommID=" + objID;
                }
                else { m_SqlParams = "SELECT * FROM [PIS_BaseInfo] WHERE CommID=" + objID; }
                
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append(a_FuncInfo[2] + " 如下：<br/>");
                    sHtml.Append("<table width=\"1024\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" bgcolor=\"#008981\">");
                    sHtml.Append("<tr>");
                    for (int i = 0; i < a_Fields.Length; i++)
                    {
                        sHtml.Append("<td class=\"fb01\" bgcolor=\"#99d0d0\" style=\"color:#033;\">&nbsp;<strong>" + a_Titles[i] + "</strong></td>");
                    }
                    sHtml.Append("</tr>");
                    while (sdr.Read())
                    {
                        sHtml.Append("<tr onmouseover=\"this.className='lvtColDataHover'\" onmouseout=\"this.className='lvtColData'\">");
                        for (int j = 0; j < a_Fields.Length ; j++)
                        {

                            sHtml.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + PageValidate.GetTrim(sdr[a_Fields[j]].ToString()) + "</td>");
                        }
                        sHtml.Append("</tr>");
                    }
                    sHtml.Append("</table><br/>");
                }
                else
                {
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            this.LiteralData.Text = sHtml.ToString();
        }



        #endregion
        #region  获取配置文件参数
        /// <summary>
        /// 配置数据集
        /// </summary>
        private void ConfigDataSet()
        {
            m_Ds = new DataSet();
            m_Ds.Locale = System.Globalization.CultureInfo.InvariantCulture;
        }
        /// <summary>
        /// 获取配置文件参数
        /// </summary>
        /// <param name="funcNo">功能号</param>
        /// <param name="configFile">配置文件路径</param>
        /// <returns></returns>
        public bool GetConfigParams(string funcNo, string configFile, ref string errorMsg)
        {
            try
            {
                ConfigDataSet();

                m_Ds.ReadXml(configFile, XmlReadMode.ReadSchema);
                DataRow[] dr = m_Ds.Tables[0].Select("FuncNo='" + funcNo + "'");

                this.m_FuncInfo = dr[0][1].ToString();
                this.m_Titles = dr[0][2].ToString();
                this.m_Fields = dr[0][3].ToString();

                m_Ds = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = "获取配置文件参数失败：" + ex.Message;
                return false;
            }
        }
        #endregion

    }
}
