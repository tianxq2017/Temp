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
    public partial class ProjectAuditInfo : System.Web.UI.Page
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
        private string m_AreaCode;
        private string m_AreaName;

        public string Is_Show = "0";

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

        #region 身份验证
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

            if (String.IsNullOrEmpty(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("非法访问：操作被终止！");
                    Response.End();
                }
            }
            else
            {
                if (!PageValidate.IsNumber(m_ObjID))
                {
                    m_ObjID = m_ObjID.Replace("s", ",");
                }
            }
            switch (m_ActionName)
            {
                case "add": // 新增
                    funcName = "新增";
                    break;
                case "edit": // 修改（未审核之前能修改，审核之后则不能修改）
                    funcName = "修改";
                    ShowModInfo(m_ObjID);
                    break;
                case "audit": // 审核
                    funcName = "审核";
                    ShowModInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">管理首页</a> &gt;&gt; 资金管理 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }
        #endregion

        /// <summary>
        /// 审核信息
        /// </summary>
        /// <param name="objID"></param>
        private void AuditInfo(string objID)
        {
            try
            {
                //0.初始提交 1.审核通过 2.未通过审核 3.修改
                string cmsAttrib = string.Empty;
                if (CheckAttribs(" CmsID IN(" + objID + ") ", "BIZ_ProjectInfo", ref cmsAttrib))
                {
                    m_SqlParams = "UPDATE CMS_Contents SET Attribs=" + cmsAttrib + " WHERE CmsID IN(" + objID + ") ";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    Response.Write(" <script>alert('操作成功：您选择的信息“审核/取消审核”操作成功！') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
                else
                {
                    Response.Write(" <script>alert('提示信息：请选择未审核的信息进行操作！') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
        }

        /// <summary>
        /// 审核前检测
        /// </summary>
        /// <param name="strSql">sql条件</param>
        /// <param name="strTable">查询表</param>
        /// <param name="curFlag">当前选中的属性值</param>   
        public static bool CheckAttribs(string strSql, string strTable, ref string curFlag)
        {
            // 0 默认;1 审核; 3 屏蔽; 4 删除; 9 推荐
            string auditFlag = string.Empty;
            string sqlParams = string.Empty;
            bool isChecked = true;
            SqlDataReader sdr = null;
            int i = 0;
            try
            {
                sqlParams = "SELECT P_State FROM " + strTable + " WHERE " + strSql;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    i++;
                    auditFlag = sdr[0].ToString();
                    if (i == 1) curFlag = auditFlag;
                    if (auditFlag != "0" && auditFlag != "2") { isChecked = false; break; }
                    if (auditFlag != curFlag) { isChecked = false; break; }
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            if (curFlag == "0") { curFlag = "2"; } else { curFlag = "0"; }

            return isChecked;
        }

        /// <summary>
        /// 查看、修改
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            //修改之前判断该信息是否已经开始审核（如果审核的话则不能修改）
            if (DbHelperSQL.GetSingle("select P_State from BIZ_ProjectInfo where P_ID = " + m_ObjID).ToString() != "0")
            {
                Response.Write(" <script>alert('操作提示：该项目已经审核，不能修改！') ;window.location.href='" + m_TargetUrl + "'</script>");
                return;
            }
            else
            {
                string sql_proInfo = "select * from BIZ_ProjectInfo where P_ID =" + objID;
                SqlDataReader sdr = null;
                DataTable m_Dt_3 = null;
                try
                {
                    sdr = DbHelperSQL.ExecuteReader(sql_proInfo);
                    while (sdr.Read())
                    {
                        this.ltr_P_UnitInfo.Text = sdr["P_UnitInfo"].ToString();
                        this.ltr_P_CreationTime.Text = sdr["P_CreationTime"].ToString();
                        this.ltr_P_ProjectType.Text = sdr["P_ProjectType"].ToString();
                        this.ltr_P_Name.Text = sdr["P_Name"].ToString();
                        this.ltr_P_SpecialNumber.Text = sdr["P_SpecialNumber"].ToString();
                        this.ltr_P_IndicatorsNumber_1.Text = sdr["P_IndicatorsNumber_1"].ToString();
                        this.ltr_P_CumulativeNumber_1.Text = sdr["P_CumulativeNumber_1"].ToString();
                        this.ltr_P_Balance_1.Text = sdr["P_Balance_1"].ToString();
                        this.ltr_P_ApplicationsNumber_1.Text = sdr["P_ApplicationsNumber_1"].ToString();
                        this.ltr_P_UseFunds.Text = sdr["P_UseFunds"].ToString();
                        this.ltr_P_PaymentMethod.Text = sdr["P_PaymentMethod"].ToString();
                        this.ltr_P_PayeeName.Text = sdr["P_PayeeName"].ToString();
                        this.ltr_P_AccountNumber.Text = sdr["P_AccountNumber"].ToString();
                        this.ltr_P_BankAddress.Text = sdr["P_BankAddress"].ToString();

                        //依据文件 
                        StringBuilder sb_3 = new StringBuilder();
                        string sql_3 = "SELECT top 3 * FROM BIZ_ProjectInfo_Docs WHERE P_ID=" + PageValidate.GetTrim(sdr["P_ID"].ToString()) + " Order by OprateDate desc";
                        m_Dt_3 = new DataTable();
                        m_Dt_3 = DbHelperSQL.Query(sql_3).Tables[0];
                        if (m_Dt_3.Rows.Count > 0)
                        {
                            sb_3.Append("<div style=\"padding-left: 25px\">");
                            sb_3.Append("<table width=\"900px\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            sb_3.Append("<tr>");
                            sb_3.Append("<td>");
                            sb_3.Append("<div class=\"container\" style=\"width: 1000px; height: 180px;\">");
                            sb_3.Append("<ul class=\"gallery\" style=\"padding-left: 25px; width: 1000px\">");
                            for (int j = 0; j < m_Dt_3.Rows.Count; j++)
                            {
                                sb_3.Append("<li style=\"width:30%\"><a href=\"" + m_Dt_3.Rows[j]["DocsPath"].ToString() + "\"><img width=\"300px\" height=\"180px\" src=\"" + m_Dt_3.Rows[j]["DocsPath"].ToString() + "\"/></a></li>");
                            }
                            sb_3.Append("</ul>");
                            sb_3.Append("</div>");
                            sb_3.Append("</td>");
                            sb_3.Append("</tr>");
                            sb_3.Append("</table>");
                            sb_3.Append("</div>");
                        }
                        this.ltr_Img.Text = sb_3.ToString();
                    }
                }
                catch
                {

                }
            }
        }

        #region 提交申请信息
        /// <summary>
        /// 提交申请信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string P_State = this.ddl_P_State.SelectedValue;   //审核状态
            string P_AuditInfo = this.txt_P_AuditInfo.Text;  //审核情况
            string P_SuperfineSignature = this.txt_P_SuperfineSignature.Text;  //上级专款文号
            string P_ThisLeveSymboll = this.txt_P_ThisLeveSymboll.Text;   //本级转款文号：
            string P_IndicatorsNumber_2 = this.txt_P_IndicatorsNumber_2.Text;   //下达指标数
            string P_CumulativeNumber_2 = this.txt_P_CumulativeNumber_2.Text;   //累计已执行数
            string P_Balance_2 = this.txt_P_Balance_2.Text;   //余额
            string P_ApplicationsNumber_2 = this.txt_P_ApplicationsNumber_2.Text;   //本次申请数
            string P_Remarks = this.txt_P_Remarks.Text;   //备注

            //是否审核
            if (DbHelperSQL.GetSingle("select P_State from BIZ_ProjectInfo where P_ID = " + m_ObjID).ToString() != "0")
            {
                Response.Write(" <script>alert('操作提示：该项目已经审核，不能修改！') ;window.location.href='" + m_TargetUrl + "'</script>");
                return;
            }
            else
            {
                try
                {
                    //审核项目信息表
                    if (m_ActionName == "audit")
                    {
                        m_SqlParams = "UPDATE BIZ_ProjectInfo SET P_State=" + P_State + ",P_AuditInfo='" + P_AuditInfo + "',P_SuperfineSignature='" + P_SuperfineSignature + "',P_ThisLeveSymboll='" + P_ThisLeveSymboll + "',P_IndicatorsNumber_2='" + P_IndicatorsNumber_2 + "',P_CumulativeNumber_2='" + P_CumulativeNumber_2 + "',P_Balance_2='" + P_Balance_2 + "',P_ApplicationsNumber_2='" + P_ApplicationsNumber_2 + "',P_Remarks='" + P_Remarks + "' WHERE P_ID=" + m_ObjID;
                        if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                        {
                            Response.Write(" <script>alert('操作成功：[ " + this.ltr_P_Name.Text + " ]信息审核成功！') ;window.location.href='" + m_TargetUrl + "'</script>");
                        }
                        else
                        {
                            Response.Write(" <script>alert('操作提示：信息修改操作失败！') ;window.location.href='" + m_TargetUrl + "'</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                    return;
                }
            }
        }
        #endregion
    }
}

