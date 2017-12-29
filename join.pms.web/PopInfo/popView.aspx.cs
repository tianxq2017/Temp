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

namespace AreWeb.OnlineCertificate.PopInfo
{
    public partial class popView : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_FuncNa;
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
                ShowInfo(m_ObjID);
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
            m_ActionName = Request.QueryString["action"];
            m_SourceUrl = Request.QueryString["sourceUrl"];
            m_ObjID = Request.QueryString["k"];
            string ona = Request.QueryString["oNa"];
            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                if (m_FuncCode.Length > 2) m_FuncCode = "04";
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = Request.QueryString["RID"];
        }

        /// <summary>
        /// 设置操作行为
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; 查看个案信息 ：";
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
            sHtml.Append("标准库中现存的个案信息如下：<br/>");
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
                    for (int j = 0; j < 14; j++) {

                        sHtml.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + PageValidate.GetTrim(sdr[a_Fields[j]].ToString()) + "</td>");
                    }
                    sHtml.Append("</tr>");
                }
                
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }
            sHtml.Append("</table><br/>");
            /*
----------------------------------
可关联的CID信息:
------------------------------------------
010101	卫计局
01010101	全员人口个案基础信息 FILEDS04

01010118	特别扶助奖励对象花名册 Fileds02
01010119	独女户扶助奖励对象花名册 Fileds02
01010120	国家奖励扶助对象花名册  Fileds02
             * 
01010111	出生婴儿实名登记信息 FILEDS03 , FILEDS10
01010112	出生医学证明办理信息 FILEDS10 ,FILEDS15
01010113	儿童计划免疫登记信息 Fileds08,Fileds13
01010114	居民健康档案基础信息 Fileds03,
             * 
01010132	合作医疗兑现情况 Fileds02
01010134	儿童健康档案信息 Fileds02
01010135	孕产妇保健档案信息 Fileds02
01010136	孕前优生业务信息   Fileds06,Fileds09
010102	民政局
01010201	婚姻登记信息 Fileds05,Fileds13
01010202	收养子女信息 Fileds02,Fileds04
01010205	享受城镇低保人员信息 Fileds02
01010206	享受农村低保人员信息 Fileds02
01010207	享受农村五保人员信息 Fileds02
010103	公安局
01010301	常住人口个案基础信息 Fileds05
01010302	新生儿落户信息 Fileds01,Fileds08,Fileds10
01010303	迁出人员信息 Fileds01
01010304	迁入人员信息 Fileds01
010104	人社局
01010406	就业促进局-就业困难人员社保补贴信息 Fileds04
01010408	就业促进局-陕西省就业（创业）培训人员登记信息 Fileds08
01010410	养老办-机关事业单位在职人员社会养老参保信息 Fileds03
01010411	机关事业单位退休人员一次性抚恤金、丧葬费申请信息 Fileds11
01010412	居民医保办-参保人员登记信息 Fileds02
01010413	失业保险管理办公室-职工失业保险参保信息 Fileds03
01010415	农保办-新农保转移 Fileds01
01010416	农保办-城乡居民基本养老保险丧葬补助金发放表 Fileds03
010105	教体局
01010501	学生入学登记信息 Fileds03
             */
            //计生全员信息
            sHtml.Append(GetFuncInfo("01010101", P_CardID, "Fileds04='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010118", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010119", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010120", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            //卫生信息
            sHtml.Append(GetFuncInfo("01010111", P_CardID, "(Fileds03='" + P_CardID + "' OR Fileds10='" + P_CardID + "')", configFile));
            sHtml.Append(GetFuncInfo("01010112", P_CardID, "(Fileds10='" + P_CardID + "' OR Fileds15='" + P_CardID + "')", configFile));
            sHtml.Append(GetFuncInfo("01010113", P_CardID, "(Fileds08='" + P_CardID + "' OR Fileds13='" + P_CardID + "')", configFile));
            sHtml.Append(GetFuncInfo("01010114", P_CardID, "Fileds03='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010132", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010134", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010135", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010136", P_CardID, "(Fileds06='" + P_CardID + "' OR Fileds09='" + P_CardID + "')", configFile));
            //民政部门信息
            sHtml.Append(GetFuncInfo("01010201", P_CardID, "(Fileds05='" + P_CardID + "' OR Fileds13='" + P_CardID + "')", configFile));
            sHtml.Append(GetFuncInfo("01010202", P_CardID, "(Fileds02='" + P_CardID + "' OR Fileds04='" + P_CardID + "')", configFile));
            sHtml.Append(GetFuncInfo("01010205", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010206", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010207", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            //公安部门信息
            sHtml.Append(GetFuncInfo("01010301", P_CardID, "Fileds05='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010302", P_CardID, "(Fileds01='" + P_CardID + "' OR Fileds08='" + P_CardID + "' OR Fileds10='" + P_CardID + "')", configFile));
            sHtml.Append(GetFuncInfo("01010303", P_CardID, "Fileds01='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010304", P_CardID, "Fileds01='" + P_CardID + "'", configFile));
            //人社部门信息
            sHtml.Append(GetFuncInfo("01010406", P_CardID, "Fileds04='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010408", P_CardID, "Fileds08='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010410", P_CardID, "Fileds03='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010411", P_CardID, "Fileds11='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010412", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010413", P_CardID, "Fileds03='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010415", P_CardID, "Fileds01='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010416", P_CardID, "Fileds03='" + P_CardID + "'", configFile));
            //教体局
            sHtml.Append(GetFuncInfo("01010501", P_CardID, "Fileds03='" + P_CardID + "'", configFile));
            this.LiteralData.Text = sHtml.ToString();
        }

        /// <summary>
        /// 个案信息的关联信息
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetFuncInfo(string funcNo, string CID, string filterSQL, string configFile)
        {
            string errMsg = string.Empty;
            SqlDataReader sdr = null;
            StringBuilder sHtml = new StringBuilder();
            try
            {
                GetConfigParams(funcNo, configFile, ref errMsg);
                string[] a_FuncInfo = this.m_FuncInfo.Split(',');
                string[] a_Titles = this.m_Titles.Split(',');
                string[] a_Fields = this.m_Fields.Split(',');


                m_SqlParams = "SELECT * FROM " + a_FuncInfo[0] + " WHERE FuncNo='" + funcNo + "' AND " + filterSQL + " ";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows) {
                    sHtml.Append(a_FuncInfo[2] + " 如下：<br/>");
                    sHtml.Append("<table width=\"1024\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" bgcolor=\"#008981\">");
                    sHtml.Append("<tr>");
                    for (int i = 0; i < a_Fields.Length - 1; i++)
                    {
                        sHtml.Append("<td class=\"fb01\" bgcolor=\"#99d0d0\" style=\"color:#033;\">&nbsp;<strong>" + a_Titles[i] + "</strong></td>");
                    }
                    sHtml.Append("</tr>");
                    while (sdr.Read())
                    {
                        sHtml.Append("<tr onmouseover=\"this.className='lvtColDataHover'\" onmouseout=\"this.className='lvtColData'\">");
                        for (int j = 0; j < a_Fields.Length - 1; j++)
                        {

                            sHtml.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + PageValidate.GetTrim(sdr[a_Fields[j]].ToString()) + "</td>");
                        }
                        sHtml.Append("</tr>");
                    }
                    sHtml.Append("</table><br/>");
                } 
                else { 
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            return sHtml.ToString();
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



