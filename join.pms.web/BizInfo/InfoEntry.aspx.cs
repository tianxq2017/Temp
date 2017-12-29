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
    public partial class InfoEntry : System.Web.UI.Page
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
                SetProjectUseUnit();
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
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">管理首页</a> &gt;&gt; 资金管理 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }

        #endregion

        #region 得到使用单位
        /// <summary>
        /// 得到使用单位
        /// </summary>
        public void SetProjectUseUnit()
        {
            m_SqlParams = "SELECT RoleName FROM SYS_Roles where RoleID > 1";
            DataTable m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            #region 项目文件
            dr_UseUnit_1.DataSource = m_Dt;
            dr_UseUnit_1.DataTextField = "RoleName";
            dr_UseUnit_1.DataValueField = "RoleName";
            dr_UseUnit_1.DataBind();
            dr_UseUnit_1.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_2.DataSource = m_Dt;
            dr_UseUnit_2.DataTextField = "RoleName";
            dr_UseUnit_2.DataValueField = "RoleName";
            dr_UseUnit_2.DataBind();
            dr_UseUnit_2.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_3.DataSource = m_Dt;
            dr_UseUnit_3.DataTextField = "RoleName";
            dr_UseUnit_3.DataValueField = "RoleName";
            dr_UseUnit_3.DataBind();
            dr_UseUnit_3.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_4.DataSource = m_Dt;
            dr_UseUnit_4.DataTextField = "RoleName";
            dr_UseUnit_4.DataValueField = "RoleName";
            dr_UseUnit_4.DataBind();
            dr_UseUnit_4.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_5.DataSource = m_Dt;
            dr_UseUnit_5.DataTextField = "RoleName";
            dr_UseUnit_5.DataValueField = "RoleName";
            dr_UseUnit_5.DataBind();
            dr_UseUnit_5.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_6.DataSource = m_Dt;
            dr_UseUnit_6.DataTextField = "RoleName";
            dr_UseUnit_6.DataValueField = "RoleName";
            dr_UseUnit_6.DataBind();
            dr_UseUnit_6.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_7.DataSource = m_Dt;
            dr_UseUnit_7.DataTextField = "RoleName";
            dr_UseUnit_7.DataValueField = "RoleName";
            dr_UseUnit_7.DataBind();
            dr_UseUnit_7.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_8.DataSource = m_Dt;
            dr_UseUnit_8.DataTextField = "RoleName";
            dr_UseUnit_8.DataValueField = "RoleName";
            dr_UseUnit_8.DataBind();
            dr_UseUnit_8.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_9.DataSource = m_Dt;
            dr_UseUnit_9.DataTextField = "RoleName";
            dr_UseUnit_9.DataValueField = "RoleName";
            dr_UseUnit_9.DataBind();
            dr_UseUnit_9.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_10.DataSource = m_Dt;
            dr_UseUnit_10.DataTextField = "RoleName";
            dr_UseUnit_10.DataValueField = "RoleName";
            dr_UseUnit_10.DataBind();
            dr_UseUnit_10.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_11.DataSource = m_Dt;
            dr_UseUnit_11.DataTextField = "RoleName";
            dr_UseUnit_11.DataValueField = "RoleName";
            dr_UseUnit_11.DataBind();
            dr_UseUnit_11.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_12.DataSource = m_Dt;
            dr_UseUnit_12.DataTextField = "RoleName";
            dr_UseUnit_12.DataValueField = "RoleName";
            dr_UseUnit_12.DataBind();
            dr_UseUnit_12.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_13.DataSource = m_Dt;
            dr_UseUnit_13.DataTextField = "RoleName";
            dr_UseUnit_13.DataValueField = "RoleName";
            dr_UseUnit_13.DataBind();
            dr_UseUnit_13.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_14.DataSource = m_Dt;
            dr_UseUnit_14.DataTextField = "RoleName";
            dr_UseUnit_14.DataValueField = "RoleName";
            dr_UseUnit_14.DataBind();
            dr_UseUnit_14.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_15.DataSource = m_Dt;
            dr_UseUnit_15.DataTextField = "RoleName";
            dr_UseUnit_15.DataValueField = "RoleName";
            dr_UseUnit_15.DataBind();
            dr_UseUnit_15.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_16.DataSource = m_Dt;
            dr_UseUnit_16.DataTextField = "RoleName";
            dr_UseUnit_16.DataValueField = "RoleName";
            dr_UseUnit_16.DataBind();
            dr_UseUnit_16.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_17.DataSource = m_Dt;
            dr_UseUnit_17.DataTextField = "RoleName";
            dr_UseUnit_17.DataValueField = "RoleName";
            dr_UseUnit_17.DataBind();
            dr_UseUnit_17.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_18.DataSource = m_Dt;
            dr_UseUnit_18.DataTextField = "RoleName";
            dr_UseUnit_18.DataValueField = "RoleName";
            dr_UseUnit_18.DataBind();
            dr_UseUnit_18.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_19.DataSource = m_Dt;
            dr_UseUnit_19.DataTextField = "RoleName";
            dr_UseUnit_19.DataValueField = "RoleName";
            dr_UseUnit_19.DataBind();
            dr_UseUnit_19.Items.Insert(0, new ListItem("请选择", ""));

            dr_UseUnit_20.DataSource = m_Dt;
            dr_UseUnit_20.DataTextField = "RoleName";
            dr_UseUnit_20.DataValueField = "RoleName";
            dr_UseUnit_20.DataBind();
            dr_UseUnit_20.Items.Insert(0, new ListItem("请选择", ""));
            #endregion

            #region 支付计划
            dr_UnitOfUse.DataSource = m_Dt;
            dr_UnitOfUse.DataTextField = "RoleName";
            dr_UnitOfUse.DataValueField = "RoleName";
            dr_UnitOfUse.DataBind();
            dr_UnitOfUse.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_1.DataSource = m_Dt;
            dr_UnitOfUse_1.DataTextField = "RoleName";
            dr_UnitOfUse_1.DataValueField = "RoleName";
            dr_UnitOfUse_1.DataBind();
            dr_UnitOfUse_1.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_2.DataSource = m_Dt;
            dr_UnitOfUse_2.DataTextField = "RoleName";
            dr_UnitOfUse_2.DataValueField = "RoleName";
            dr_UnitOfUse_2.DataBind();
            dr_UnitOfUse_2.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_3.DataSource = m_Dt;
            dr_UnitOfUse_3.DataTextField = "RoleName";
            dr_UnitOfUse_3.DataValueField = "RoleName";
            dr_UnitOfUse_3.DataBind();
            dr_UnitOfUse_3.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_4.DataSource = m_Dt;
            dr_UnitOfUse_4.DataTextField = "RoleName";
            dr_UnitOfUse_4.DataValueField = "RoleName";
            dr_UnitOfUse_4.DataBind();
            dr_UnitOfUse_4.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_5.DataSource = m_Dt;
            dr_UnitOfUse_5.DataTextField = "RoleName";
            dr_UnitOfUse_5.DataValueField = "RoleName";
            dr_UnitOfUse_5.DataBind();
            dr_UnitOfUse_5.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_6.DataSource = m_Dt;
            dr_UnitOfUse_6.DataTextField = "RoleName";
            dr_UnitOfUse_6.DataValueField = "RoleName";
            dr_UnitOfUse_6.DataBind();
            dr_UnitOfUse_6.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_7.DataSource = m_Dt;
            dr_UnitOfUse_7.DataTextField = "RoleName";
            dr_UnitOfUse_7.DataValueField = "RoleName";
            dr_UnitOfUse_7.DataBind();
            dr_UnitOfUse_7.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_8.DataSource = m_Dt;
            dr_UnitOfUse_8.DataTextField = "RoleName";
            dr_UnitOfUse_8.DataValueField = "RoleName";
            dr_UnitOfUse_8.DataBind();
            dr_UnitOfUse_8.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_9.DataSource = m_Dt;
            dr_UnitOfUse_9.DataTextField = "RoleName";
            dr_UnitOfUse_9.DataValueField = "RoleName";
            dr_UnitOfUse_9.DataBind();
            dr_UnitOfUse_9.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_10.DataSource = m_Dt;
            dr_UnitOfUse_10.DataTextField = "RoleName";
            dr_UnitOfUse_10.DataValueField = "RoleName";
            dr_UnitOfUse_10.DataBind();
            dr_UnitOfUse_10.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_11.DataSource = m_Dt;
            dr_UnitOfUse_11.DataTextField = "RoleName";
            dr_UnitOfUse_11.DataValueField = "RoleName";
            dr_UnitOfUse_11.DataBind();
            dr_UnitOfUse_11.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_12.DataSource = m_Dt;
            dr_UnitOfUse_12.DataTextField = "RoleName";
            dr_UnitOfUse_12.DataValueField = "RoleName";
            dr_UnitOfUse_12.DataBind();
            dr_UnitOfUse_12.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_13.DataSource = m_Dt;
            dr_UnitOfUse_13.DataTextField = "RoleName";
            dr_UnitOfUse_13.DataValueField = "RoleName";
            dr_UnitOfUse_13.DataBind();
            dr_UnitOfUse_13.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_14.DataSource = m_Dt;
            dr_UnitOfUse_14.DataTextField = "RoleName";
            dr_UnitOfUse_14.DataValueField = "RoleName";
            dr_UnitOfUse_14.DataBind();
            dr_UnitOfUse_14.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_15.DataSource = m_Dt;
            dr_UnitOfUse_15.DataTextField = "RoleName";
            dr_UnitOfUse_15.DataValueField = "RoleName";
            dr_UnitOfUse_15.DataBind();
            dr_UnitOfUse_15.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_16.DataSource = m_Dt;
            dr_UnitOfUse_16.DataTextField = "RoleName";
            dr_UnitOfUse_16.DataValueField = "RoleName";
            dr_UnitOfUse_16.DataBind();
            dr_UnitOfUse_16.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_17.DataSource = m_Dt;
            dr_UnitOfUse_17.DataTextField = "RoleName";
            dr_UnitOfUse_17.DataValueField = "RoleName";
            dr_UnitOfUse_17.DataBind();
            dr_UnitOfUse_17.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_18.DataSource = m_Dt;
            dr_UnitOfUse_18.DataTextField = "RoleName";
            dr_UnitOfUse_18.DataValueField = "RoleName";
            dr_UnitOfUse_18.DataBind();
            dr_UnitOfUse_18.Items.Insert(0, new ListItem("请选择", ""));

            dr_UnitOfUse_19.DataSource = m_Dt;
            dr_UnitOfUse_19.DataTextField = "RoleName";
            dr_UnitOfUse_19.DataValueField = "RoleName";
            dr_UnitOfUse_19.DataBind();
            dr_UnitOfUse_19.Items.Insert(0, new ListItem("请选择", ""));
            #endregion

            m_Dt = null;
        }
        #endregion

        #region 提交申请信息
        /// <summary>
        /// 提交申请信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string CmsPhotos = "";
            string txtDocsName = Request["txtDocsName"];
            if (txtDocsName != "")
            {
                CmsPhotos = txtDocsName.Substring(txtDocsName.IndexOf('.'), 4);
                if (CmsPhotos == ".gif" || CmsPhotos == ".jpg" || CmsPhotos == ".jpe" || CmsPhotos == ".bmp" || CmsPhotos == ".png")
                {
                    if (CmsPhotos == ".jpe")
                    {
                        CmsPhotos = ".jpeg";
                    }
                }
                else
                {
                    CmsPhotos = "";
                }
            }
            string incDocsID = this.txtDocsID.Value;
            string strErr = string.Empty;

            string P_Name = this.txt_FileName.Text;   //文件名称
            string P_SourceFunds = this.txt_SourceFunds.Text;   //资金来源
            string P_AllMoney = this.txt_TotalAmount.Text;  //资金总额
            string P_PaymentPlan = this.txt_PaymentPlan.Text;   //支付计划

            #region 项目内容-使用单位-金额明细
            //项目内容-使用单位
            string U_Name_1 = this.dr_UseUnit_1.SelectedValue;
            string U_Name_2 = this.dr_UseUnit_2.SelectedValue;
            string U_Name_3 = this.dr_UseUnit_3.SelectedValue;
            string U_Name_4 = this.dr_UseUnit_4.SelectedValue;
            string U_Name_5 = this.dr_UseUnit_5.SelectedValue;
            string U_Name_6 = this.dr_UseUnit_6.SelectedValue;
            string U_Name_7 = this.dr_UseUnit_7.SelectedValue;
            string U_Name_8 = this.dr_UseUnit_8.SelectedValue;
            string U_Name_9 = this.dr_UseUnit_9.SelectedValue;
            string U_Name_10 = this.dr_UseUnit_10.SelectedValue;
            string U_Name_11 = this.dr_UseUnit_11.SelectedValue;
            string U_Name_12 = this.dr_UseUnit_12.SelectedValue;
            string U_Name_13 = this.dr_UseUnit_13.SelectedValue;
            string U_Name_14 = this.dr_UseUnit_14.SelectedValue;
            string U_Name_15 = this.dr_UseUnit_15.SelectedValue;
            string U_Name_16 = this.dr_UseUnit_16.SelectedValue;
            string U_Name_17 = this.dr_UseUnit_17.SelectedValue;
            string U_Name_18 = this.dr_UseUnit_18.SelectedValue;
            string U_Name_19 = this.dr_UseUnit_19.SelectedValue;
            string U_Name_20 = this.dr_UseUnit_20.SelectedValue;
            //项目内容-金额明细
            string U_MoneyInfo_1 = this.txt_MoneyInfo_1.Text;
            string U_MoneyInfo_2 = this.txt_MoneyInfo_2.Text;
            string U_MoneyInfo_3 = this.txt_MoneyInfo_3.Text;
            string U_MoneyInfo_4 = this.txt_MoneyInfo_4.Text;
            string U_MoneyInfo_5 = this.txt_MoneyInfo_5.Text;
            string U_MoneyInfo_6 = this.txt_MoneyInfo_6.Text;
            string U_MoneyInfo_7 = this.txt_MoneyInfo_7.Text;
            string U_MoneyInfo_8 = this.txt_MoneyInfo_8.Text;
            string U_MoneyInfo_9 = this.txt_MoneyInfo_9.Text;
            string U_MoneyInfo_10 = this.txt_MoneyInfo_10.Text;
            string U_MoneyInfo_11 = this.txt_MoneyInfo_11.Text;
            string U_MoneyInfo_12 = this.txt_MoneyInfo_12.Text;
            string U_MoneyInfo_13 = this.txt_MoneyInfo_13.Text;
            string U_MoneyInfo_14 = this.txt_MoneyInfo_14.Text;
            string U_MoneyInfo_15 = this.txt_MoneyInfo_15.Text;
            string U_MoneyInfo_16 = this.txt_MoneyInfo_16.Text;
            string U_MoneyInfo_17 = this.txt_MoneyInfo_17.Text;
            string U_MoneyInfo_18 = this.txt_MoneyInfo_18.Text;
            string U_MoneyInfo_19 = this.txt_MoneyInfo_19.Text;
            string U_MoneyInfo_20 = this.txt_MoneyInfo_20.Text;
            #endregion

            #region 支付计划-时间-使用单位-金额
            //时间
            string DateTime_0 = this.txt_DateTime.Value;
            string DateTime_1 = this.txt_DateTime_1.Value;
            string DateTime_2 = this.txt_DateTime_2.Value;
            string DateTime_3 = this.txt_DateTime_3.Value;
            string DateTime_4 = this.txt_DateTime_4.Value;
            string DateTime_5 = this.txt_DateTime_5.Value;
            string DateTime_6 = this.txt_DateTime_6.Value;
            string DateTime_7 = this.txt_DateTime_7.Value;
            string DateTime_8 = this.txt_DateTime_8.Value;
            string DateTime_9 = this.txt_DateTime_9.Value;
            string DateTime_10 = this.txt_DateTime_10.Value;
            string DateTime_11 = this.txt_DateTime_11.Value;
            string DateTime_12 = this.txt_DateTime_12.Value;
            string DateTime_13 = this.txt_DateTime_13.Value;
            string DateTime_14 = this.txt_DateTime_14.Value;
            string DateTime_15 = this.txt_DateTime_15.Value;
            string DateTime_16 = this.txt_DateTime_16.Value;
            string DateTime_17 = this.txt_DateTime_17.Value;
            string DateTime_18 = this.txt_DateTime_18.Value;
            string DateTime_19 = this.txt_DateTime_19.Value;

            //使用单位
            string UnitOfUse_0 = this.dr_UnitOfUse.Text;
            string UnitOfUse_1 = this.dr_UnitOfUse_1.Text;
            string UnitOfUse_2 = this.dr_UnitOfUse_2.Text;
            string UnitOfUse_3 = this.dr_UnitOfUse_3.Text;
            string UnitOfUse_4 = this.dr_UnitOfUse_4.Text;
            string UnitOfUse_5 = this.dr_UnitOfUse_5.Text;
            string UnitOfUse_6 = this.dr_UnitOfUse_6.Text;
            string UnitOfUse_7 = this.dr_UnitOfUse_7.Text;
            string UnitOfUse_8 = this.dr_UnitOfUse_8.Text;
            string UnitOfUse_9 = this.dr_UnitOfUse_9.Text;
            string UnitOfUse_10 = this.dr_UnitOfUse_10.Text;
            string UnitOfUse_11 = this.dr_UnitOfUse_11.Text;
            string UnitOfUse_12 = this.dr_UnitOfUse_12.Text;
            string UnitOfUse_13 = this.dr_UnitOfUse_13.Text;
            string UnitOfUse_14 = this.dr_UnitOfUse_14.Text;
            string UnitOfUse_15 = this.dr_UnitOfUse_15.Text;
            string UnitOfUse_16 = this.dr_UnitOfUse_16.Text;
            string UnitOfUse_17 = this.dr_UnitOfUse_17.Text;
            string UnitOfUse_18 = this.dr_UnitOfUse_18.Text;
            string UnitOfUse_19 = this.dr_UnitOfUse_19.Text;

            //金额
            string AmountOfMoney_0 = this.txt_Money.Text;
            string AmountOfMoney_1 = this.txt_Money_1.Text;
            string AmountOfMoney_2 = this.txt_Money_2.Text;
            string AmountOfMoney_3 = this.txt_Money_3.Text;
            string AmountOfMoney_4 = this.txt_Money_4.Text;
            string AmountOfMoney_5 = this.txt_Money_5.Text;
            string AmountOfMoney_6 = this.txt_Money_6.Text;
            string AmountOfMoney_7 = this.txt_Money_7.Text;
            string AmountOfMoney_8 = this.txt_Money_8.Text;
            string AmountOfMoney_9 = this.txt_Money_9.Text;
            string AmountOfMoney_10 = this.txt_Money_10.Text;
            string AmountOfMoney_11 = this.txt_Money_11.Text;
            string AmountOfMoney_12 = this.txt_Money_12.Text;
            string AmountOfMoney_13 = this.txt_Money_13.Text;
            string AmountOfMoney_14 = this.txt_Money_14.Text;
            string AmountOfMoney_15 = this.txt_Money_15.Text;
            string AmountOfMoney_16 = this.txt_Money_16.Text;
            string AmountOfMoney_17 = this.txt_Money_17.Text;
            string AmountOfMoney_18 = this.txt_Money_18.Text;
            string AmountOfMoney_19 = this.txt_Money_19.Text;
            #endregion

            //检查是否重复提交
            //string msg = string.Empty;
            //if (CommPage.IsHasBiz(this.m_FuncCode, this.m_PerA.PersonCardID, this.m_PerB.PersonCardID, ref msg)) { strErr += msg; }

            //if (strErr != "")
            //{
            //    MessageBox.Show(this, strErr);
            //    return;
            //}
            try
            {
                //插入项目信息表
                if (m_ActionName == "add")
                {
                    m_SqlParams = "INSERT INTO [BIZ_ProjectInfo](";
                    m_SqlParams += "FuncNo,P_Name,P_SourceFunds,P_AllMoney,P_PaymentPlan,UserID";
                    // m_SqlParams += "";
                    m_SqlParams += ") VALUES(";
                    m_SqlParams += "'" + m_FuncCode + "','" + P_Name + "','" + P_SourceFunds + "','" + P_AllMoney + "','" + P_PaymentPlan + "'," + m_UserID + ")";
                    m_SqlParams += " SELECT SCOPE_IDENTITY()";
                    m_ObjID = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                    if (!string.IsNullOrEmpty(m_ObjID))
                    {
                        //更新上传的图片归属
                        if (!string.IsNullOrEmpty(incDocsID))
                        {
                            incDocsID = incDocsID.Substring(0, incDocsID.Length - 1);
                            m_SqlParams = "UPDATE BIZ_ProjectInfo_Docs SET P_ID=" + m_ObjID + ",UserID = " + m_UserID + "  WHERE D_ID IN (" + incDocsID + ")";
                            DbHelperSQL.ExecuteSql(m_SqlParams);
                        }
                        Response.Write(" <script>alert('操作成功：标题为[ " + P_Name + " ]的资金信息采集成功！') ;window.location.href='" + m_TargetUrl + "'</script>");
                    }
                    else
                    {
                        Response.Write(" <script>alert('操作提示：信息发布失败！') ;window.location.href='" + m_TargetUrl + "'</script>");
                    }

                    //插入项目内容的使用信息
                    for (int i = 0; i < 20; i++)
                    {
                        if (i == 0 && U_Name_1 != "" && U_MoneyInfo_1 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_1 + "','" + U_MoneyInfo_1 + "')");
                        }
                        if (i == 1 && U_Name_2 != "" && U_MoneyInfo_2 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_2 + "','" + U_MoneyInfo_2 + "')");
                        }
                        if (i == 2 && U_Name_3 != "" && U_MoneyInfo_3 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_3 + "','" + U_MoneyInfo_3 + "')");
                        }
                        if (i == 3 && U_Name_4 != "" && U_MoneyInfo_4 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_4 + "','" + U_MoneyInfo_4 + "')");
                        }
                        if (i == 4 && U_Name_5 != "" && U_MoneyInfo_5 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_5 + "','" + U_MoneyInfo_5 + "')");
                        }
                        if (i == 5 && U_Name_6 != "" && U_MoneyInfo_6 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_6 + "','" + U_MoneyInfo_6 + "')");
                        }
                        if (i == 6 && U_Name_7 != "" && U_MoneyInfo_7 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_7 + "','" + U_MoneyInfo_7 + "')");
                        }
                        if (i == 7 && U_Name_8 != "" && U_MoneyInfo_8 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_8 + "','" + U_MoneyInfo_8 + "')");
                        }
                        if (i == 8 && U_Name_9 != "" && U_MoneyInfo_9 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_9 + "','" + U_MoneyInfo_9 + "')");
                        }
                        if (i == 9 && U_Name_10 != "" && U_MoneyInfo_10 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_10 + "','" + U_MoneyInfo_10 + "')");
                        }
                        if (i == 10 && U_Name_11 != "" && U_MoneyInfo_11 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_11 + "','" + U_MoneyInfo_11 + "')");
                        }
                        if (i == 11 && U_Name_12 != "" && U_MoneyInfo_12 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_12 + "','" + U_MoneyInfo_12 + "')");
                        }
                        if (i == 12 && U_Name_13 != "" && U_MoneyInfo_13 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_13 + "','" + U_MoneyInfo_13 + "')");
                        }
                        if (i == 13 && U_Name_14 != "" && U_MoneyInfo_14 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_14 + "','" + U_MoneyInfo_14 + "')");
                        }
                        if (i == 14 && U_Name_15 != "" && U_MoneyInfo_15 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_15 + "','" + U_MoneyInfo_15 + "')");
                        }
                        if (i == 15 && U_Name_16 != "" && U_MoneyInfo_16 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_16 + "','" + U_MoneyInfo_16 + "')");
                        }
                        if (i == 16 && U_Name_17 != "" && U_MoneyInfo_17 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_17 + "','" + U_MoneyInfo_17 + "')");
                        }
                        if (i == 17 && U_Name_18 != "" && U_MoneyInfo_18 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_18 + "','" + U_MoneyInfo_18 + "')");
                        }
                        if (i == 18 && U_Name_19 != "" && U_MoneyInfo_19 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_19 + "','" + U_MoneyInfo_19 + "')");
                        }
                        if (i == 19 && U_Name_20 != "" && U_MoneyInfo_20 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectUseUnit](FuncNo,P_ID,U_Name,U_MoneyInfo) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + U_Name_20 + "','" + U_MoneyInfo_20 + "')");
                        }
                    }

                    //插入支付计划信息
                    for (int k = 0; k < 20; k++)
                    {
                        if (k == 0 && DateTime_0 != "" && UnitOfUse_0 != "" && AmountOfMoney_0 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_0 + "','" + UnitOfUse_0 + "','" + AmountOfMoney_0 + "')");
                        }
                        if (k == 1 && DateTime_1 != "" && UnitOfUse_1 != "" && AmountOfMoney_1 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_1 + "','" + UnitOfUse_1 + "','" + AmountOfMoney_1 + "')");
                        }
                        if (k == 2 && DateTime_2 != "" && UnitOfUse_2 != "" && AmountOfMoney_2 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_2 + "','" + UnitOfUse_2 + "','" + AmountOfMoney_2 + "')");
                        }
                        if (k == 3 && DateTime_3 != "" && UnitOfUse_3 != "" && AmountOfMoney_3 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_3 + "','" + UnitOfUse_3 + "','" + AmountOfMoney_3 + "')");
                        }
                        if (k == 4 && DateTime_4 != "" && UnitOfUse_4 != "" && AmountOfMoney_4 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_4 + "','" + UnitOfUse_4 + "','" + AmountOfMoney_4 + "')");
                        }
                        if (k == 5 && DateTime_5 != "" && UnitOfUse_5 != "" && AmountOfMoney_5 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_5 + "','" + UnitOfUse_5 + "','" + AmountOfMoney_5 + "')");
                        }
                        if (k == 6 && DateTime_6 != "" && UnitOfUse_6 != "" && AmountOfMoney_6 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_6 + "','" + UnitOfUse_6 + "','" + AmountOfMoney_6 + "')");
                        }
                        if (k == 7 && DateTime_7 != "" && UnitOfUse_7 != "" && AmountOfMoney_7 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_7 + "','" + UnitOfUse_7 + "','" + AmountOfMoney_7 + "')");
                        }
                        if (k == 8 && DateTime_8 != "" && UnitOfUse_8 != "" && AmountOfMoney_8 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_8 + "','" + UnitOfUse_8 + "','" + AmountOfMoney_8 + "')");
                        }
                        if (k == 9 && DateTime_9 != "" && UnitOfUse_9 != "" && AmountOfMoney_9 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_9 + "','" + UnitOfUse_9 + "','" + AmountOfMoney_9 + "')");
                        }
                        if (k == 10 && DateTime_10 != "" && UnitOfUse_10 != "" && AmountOfMoney_10 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_10 + "','" + UnitOfUse_10 + "','" + AmountOfMoney_10 + "')");
                        }
                        if (k == 11 && DateTime_11 != "" && UnitOfUse_11 != "" && AmountOfMoney_11 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_11 + "','" + UnitOfUse_11 + "','" + AmountOfMoney_11 + "')");
                        }
                        if (k == 12 && DateTime_12 != "" && UnitOfUse_12 != "" && AmountOfMoney_12 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_12 + "','" + UnitOfUse_12 + "','" + AmountOfMoney_12 + "')");
                        }
                        if (k == 13 && DateTime_13 != "" && UnitOfUse_13 != "" && AmountOfMoney_13 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_13 + "','" + UnitOfUse_13 + "','" + AmountOfMoney_13 + "')");
                        }
                        if (k == 14 && DateTime_14 != "" && UnitOfUse_14 != "" && AmountOfMoney_14 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_14 + "','" + UnitOfUse_14 + "','" + AmountOfMoney_14 + "')");
                        }
                        if (k == 15 && DateTime_15 != "" && UnitOfUse_15 != "" && AmountOfMoney_15 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_15 + "','" + UnitOfUse_15 + "','" + AmountOfMoney_15 + "')");
                        }
                        if (k == 16 && DateTime_16 != "" && UnitOfUse_16 != "" && AmountOfMoney_16 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_16 + "','" + UnitOfUse_16 + "','" + AmountOfMoney_16 + "')");
                        }
                        if (k == 17 && DateTime_17 != "" && UnitOfUse_17 != "" && AmountOfMoney_17 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_17 + "','" + UnitOfUse_17 + "','" + AmountOfMoney_17 + "')");
                        }
                        if (k == 18 && DateTime_18 != "" && UnitOfUse_18 != "" && AmountOfMoney_18 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_18 + "','" + UnitOfUse_18 + "','" + AmountOfMoney_18 + "')");
                        }
                        if (k == 19 && DateTime_19 != "" && UnitOfUse_19 != "" && AmountOfMoney_19 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,DateTime,UnitOfUse,AmountOfMoney) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + DateTime_19 + "','" + UnitOfUse_19 + "','" + AmountOfMoney_19 + "')");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
        }
        #endregion
    }
}

