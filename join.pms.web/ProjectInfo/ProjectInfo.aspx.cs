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
    public partial class ProjectInfo : System.Web.UI.Page
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
                SetProjectTypes();
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
                    Is_Show = "1";
                    //AuditInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">管理首页</a> &gt;&gt; 资金管理 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }
        #endregion

        /// <summary>
        /// 查看、修改
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            //修改之前判断该信息是否已经开始审核（如果审核的话则不能修改）
            if (DbHelperSQL.GetSingle("select P_State from BIZ_ProjectInfo where P_ID = " + m_ObjID).ToString() != "0")
            {
                Response.Write(" <script>alert('操作提示：该项目已经开始审核，不能修改！') ;window.location.href='" + m_TargetUrl + "'</script>");
                return;
            }
            else
            {
                string sql_SelID = "select * from BIZ_ProjectPaymentPlan where P_ID = " + objID + " And FuncNo = '0201'";
                DataTable dt = new DataTable();
                try
                {
                    dt = DbHelperSQL.Query(sql_SelID).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                this.hd_ID_1.Value = dt.Rows[i]["ID"].ToString();
                                this.txt_AmountOfMoney_1.Text = dt.Rows[i]["AmountOfMoney"].ToString();
                                this.txt_DateTime_1.Value = dt.Rows[i]["DateTime"].ToString();
                                this.txt_UnitOfUse_1.Text = dt.Rows[i]["UnitOfUse"].ToString();
                            }
                            if (i == 1)
                            {
                                this.hd_ID_2.Value = dt.Rows[i]["ID"].ToString();
                                this.txt_AmountOfMoney_2.Text = dt.Rows[i]["AmountOfMoney"].ToString();
                                this.txt_DateTime_2.Value = dt.Rows[i]["DateTime"].ToString();
                                this.txt_UnitOfUse_2.Text = dt.Rows[i]["UnitOfUse"].ToString();
                            }
                            if (i == 2)
                            {
                                this.hd_ID_3.Value = dt.Rows[i]["ID"].ToString();
                                this.txt_AmountOfMoney_3.Text = dt.Rows[i]["AmountOfMoney"].ToString();
                                this.txt_DateTime_3.Value = dt.Rows[i]["DateTime"].ToString();
                                this.txt_UnitOfUse_3.Text = dt.Rows[i]["UnitOfUse"].ToString();
                            }
                            if (i == 3)
                            {
                                this.hd_ID_4.Value = dt.Rows[i]["ID"].ToString();
                                this.txt_AmountOfMoney_4.Text = dt.Rows[i]["AmountOfMoney"].ToString();
                                this.txt_DateTime_4.Value = dt.Rows[i]["DateTime"].ToString();
                                this.txt_UnitOfUse_4.Text = dt.Rows[i]["UnitOfUse"].ToString();
                            }
                            if (i == 4)
                            {
                                this.hd_ID_5.Value = dt.Rows[i]["ID"].ToString();
                                this.txt_AmountOfMoney_5.Text = dt.Rows[i]["AmountOfMoney"].ToString();
                                this.txt_DateTime_5.Value = dt.Rows[i]["DateTime"].ToString();
                                this.txt_UnitOfUse_5.Text = dt.Rows[i]["UnitOfUse"].ToString();
                            }
                            if (i == 5)
                            {
                                this.hd_ID_6.Value = dt.Rows[i]["ID"].ToString();
                                this.txt_AmountOfMoney_6.Text = dt.Rows[i]["AmountOfMoney"].ToString();
                                this.txt_DateTime_6.Value = dt.Rows[i]["DateTime"].ToString();
                                this.txt_UnitOfUse_6.Text = dt.Rows[i]["UnitOfUse"].ToString();
                            }
                            if (i == 6)
                            {
                                this.hd_ID_7.Value = dt.Rows[i]["ID"].ToString();
                                this.txt_AmountOfMoney_7.Text = dt.Rows[i]["AmountOfMoney"].ToString();
                                this.txt_DateTime_7.Value = dt.Rows[i]["DateTime"].ToString();
                                this.txt_UnitOfUse_7.Text = dt.Rows[i]["UnitOfUse"].ToString();
                            }
                            if (i == 7)
                            {
                                this.hd_ID_8.Value = dt.Rows[i]["ID"].ToString();
                                this.txt_AmountOfMoney_8.Text = dt.Rows[i]["AmountOfMoney"].ToString();
                                this.txt_DateTime_8.Value = dt.Rows[i]["DateTime"].ToString();
                                this.txt_UnitOfUse_8.Text = dt.Rows[i]["UnitOfUse"].ToString();
                            }
                            if (i == 8)
                            {
                                this.hd_ID_9.Value = dt.Rows[i]["ID"].ToString();
                                this.txt_AmountOfMoney_9.Text = dt.Rows[i]["AmountOfMoney"].ToString();
                                this.txt_DateTime_9.Value = dt.Rows[i]["DateTime"].ToString();
                                this.txt_UnitOfUse_9.Text = dt.Rows[i]["UnitOfUse"].ToString();
                            }
                            if (i == 9)
                            {
                                this.hd_ID_10.Value = dt.Rows[i]["ID"].ToString();
                                this.txt_AmountOfMoney_10.Text = dt.Rows[i]["AmountOfMoney"].ToString();
                                this.txt_DateTime_10.Value = dt.Rows[i]["DateTime"].ToString();
                                this.txt_UnitOfUse_10.Text = dt.Rows[i]["UnitOfUse"].ToString();
                            }
                        }
                    }
                }
                catch
                { }

                string sql_proInfo = "select * from BIZ_ProjectInfo where P_ID =" + objID;
                SqlDataReader sdr = null;
                DataTable m_Dt_3 = null;
                try
                {
                    sdr = DbHelperSQL.ExecuteReader(sql_proInfo);
                    while (sdr.Read())
                    {
                        this.txt_P_UnitInfo.Text = sdr["P_UnitInfo"].ToString();
                        this.txt_P_CreationTime.Value = sdr["P_CreationTime"].ToString();
                        this.dr_P_ProjectType.SelectedValue = sdr["P_ProjectType"].ToString();
                        this.txt_P_Name.Text = sdr["P_Name"].ToString();
                        this.txt_P_SpecialNumber.Text = sdr["P_SpecialNumber"].ToString();
                        this.txt_P_IndicatorsNumber_1.Text = sdr["P_IndicatorsNumber_1"].ToString();
                        this.txt_P_CumulativeNumber_1.Text = sdr["P_CumulativeNumber_1"].ToString();
                        this.txt_P_Balance_1.Text = sdr["P_Balance_1"].ToString();
                        this.txt_P_ApplicationsNumber_1.Text = sdr["P_ApplicationsNumber_1"].ToString();
                        this.txt_P_UseFunds.Text = sdr["P_UseFunds"].ToString();
                        this.txt_P_PaymentMethod.Text = sdr["P_PaymentMethod"].ToString();
                        this.txt_P_PayeeName.Text = sdr["P_PayeeName"].ToString();
                        this.txt_P_AccountNumber.Text = sdr["P_AccountNumber"].ToString();
                        this.txt_P_BankAddress.Text = sdr["P_BankAddress"].ToString();

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

        #region 得到项目分类
        /// <summary>
        /// 得到项目分类
        /// </summary>
        public void SetProjectTypes()
        {
            m_SqlParams = "SELECT T_Name FROM BIZ_ProjectTypes";
            DataTable m_Dt = new DataTable();
            try
            {
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                dr_P_ProjectType.DataSource = m_Dt;
                dr_P_ProjectType.DataTextField = "T_Name";
                dr_P_ProjectType.DataValueField = "T_Name";
                dr_P_ProjectType.DataBind();
                dr_P_ProjectType.Items.Insert(0, new ListItem("请选择", ""));
            }
            catch
            { }
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
            if (txtDocsName != null && txtDocsName != "")
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

            string incDocsID = this.txtDocsID.Value;   //附件ID
            string strErr = string.Empty;

            string P_UnitInfo = this.txt_P_UnitInfo.Text;   //资金申报单位
            string P_CreationTime = this.txt_P_CreationTime.Value;   //申报日期
            string P_ProjectType = this.dr_P_ProjectType.SelectedValue;  //项目分类
            string P_Name = this.txt_P_Name.Text;   //项目名称
            string P_SpecialNumber = this.txt_P_SpecialNumber.Text;   //专款文号
            string P_IndicatorsNumber_1 = this.txt_P_IndicatorsNumber_1.Text;   //下达指标数
            string P_CumulativeNumber_1 = this.txt_P_CumulativeNumber_1.Text;   //累计执行数
            string P_Balance_1 = this.txt_P_Balance_1.Text;   //余额
            string P_ApplicationsNumber_1 = this.txt_P_ApplicationsNumber_1.Text;   //本次申请数
            string P_UseFunds = this.txt_P_UseFunds.Text;   //资金用途
            string P_PaymentMethod = this.txt_P_PaymentMethod.Text;   //支付方式
            string P_PayeeName = this.txt_P_PayeeName.Text;   //收款人账户名称
            string P_AccountNumber = this.txt_P_AccountNumber.Text;   //账号
            string P_BankAddress = this.txt_P_BankAddress.Text;   //开户银行

            //支付金额
            string AmountOfMoney_1 = this.txt_AmountOfMoney_1.Text;   //1
            string AmountOfMoney_2 = this.txt_AmountOfMoney_2.Text;   //2
            string AmountOfMoney_3 = this.txt_AmountOfMoney_3.Text;   //3
            string AmountOfMoney_4 = this.txt_AmountOfMoney_4.Text;   //4
            string AmountOfMoney_5 = this.txt_AmountOfMoney_5.Text;   //5
            string AmountOfMoney_6 = this.txt_AmountOfMoney_6.Text;   //6
            string AmountOfMoney_7 = this.txt_AmountOfMoney_7.Text;   //7
            string AmountOfMoney_8 = this.txt_AmountOfMoney_8.Text;   //8
            string AmountOfMoney_9 = this.txt_AmountOfMoney_9.Text;   //9
            string AmountOfMoney_10 = this.txt_AmountOfMoney_10.Text;   //10

            //资金到位时间
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

            //预计完工量
            string UnitOfUse_1 = this.txt_UnitOfUse_1.Text;
            string UnitOfUse_2 = this.txt_UnitOfUse_2.Text;
            string UnitOfUse_3 = this.txt_UnitOfUse_3.Text;
            string UnitOfUse_4 = this.txt_UnitOfUse_4.Text;
            string UnitOfUse_5 = this.txt_UnitOfUse_5.Text;
            string UnitOfUse_6 = this.txt_UnitOfUse_6.Text;
            string UnitOfUse_7 = this.txt_UnitOfUse_7.Text;
            string UnitOfUse_8 = this.txt_UnitOfUse_8.Text;
            string UnitOfUse_9 = this.txt_UnitOfUse_9.Text;
            string UnitOfUse_10 = this.txt_UnitOfUse_10.Text;

            //修改是的变量ID
            string ID_1 = this.hd_ID_1.Value;
            string ID_2 = this.hd_ID_2.Value;
            string ID_3 = this.hd_ID_3.Value;
            string ID_4 = this.hd_ID_4.Value;
            string ID_5 = this.hd_ID_5.Value;
            string ID_6 = this.hd_ID_6.Value;
            string ID_7 = this.hd_ID_7.Value;
            string ID_8 = this.hd_ID_8.Value;
            string ID_9 = this.hd_ID_9.Value;
            string ID_10 = this.hd_ID_10.Value;

            //检查是否重复提交
            try
            {
                //插入项目信息表
                if (m_ActionName == "add")
                {
                    m_SqlParams = "INSERT INTO [BIZ_ProjectInfo](";
                    m_SqlParams += "FuncNo,P_Name,P_SpecialNumber,P_IndicatorsNumber_1,P_CumulativeNumber_1,P_Balance_1,P_ApplicationsNumber_1,P_UseFunds,P_PaymentMethod,P_PayeeName,P_AccountNumber,P_BankAddress,UserID,P_ProjectType,P_UnitInfo";
                    m_SqlParams += ") VALUES(";
                    m_SqlParams += "'" + m_FuncCode + "','" + P_Name + "','" + P_SpecialNumber + "','" + P_IndicatorsNumber_1 + "','" + P_CumulativeNumber_1 + "','" + P_Balance_1 + "','" + P_ApplicationsNumber_1 + "','" + P_UseFunds + "','" + P_PaymentMethod + "','" + P_PayeeName + "','" + P_AccountNumber + "','" + P_BankAddress + "'," + m_UserID + ",'" + P_ProjectType + "','" + P_UnitInfo + "')";
                    m_SqlParams += " SELECT SCOPE_IDENTITY()";
                    m_ObjID = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                    if (!string.IsNullOrEmpty(m_ObjID))
                    {
                        //更新上传的图片归属
                        //if (!string.IsNullOrEmpty(incDocsID))
                        if (!string.IsNullOrEmpty(incDocsID))
                        {
                            incDocsID = incDocsID.Substring(0, incDocsID.Length - 1);
                            //m_SqlParams = "UPDATE BIZ_ProjectInfo_Docs SET P_ID=" + m_ObjID + ",UserID = " + m_UserID + "  WHERE D_ID IN (" + incDocsID + ")";
                            m_SqlParams = "UPDATE BIZ_ProjectInfo_Docs SET P_ID=" + m_ObjID + ",UserID = " + m_UserID + "  WHERE P_ID IN (" + incDocsID + ")";
                            DbHelperSQL.ExecuteSql(m_SqlParams);
                        }
                        Response.Write(" <script>alert('操作成功：[ " + P_Name + " ]项目信息提交成功！') ;window.location.href='" + m_TargetUrl + "'</script>");
                    }
                    else
                    {
                        Response.Write(" <script>alert('操作提示：信息发布失败！') ;window.location.href='" + m_TargetUrl + "'</script>");
                    }

                    //插入项目内容的使用信息
                    for (int i = 0; i < 10; i++)
                    {
                        if (i == 0 && AmountOfMoney_1 != "" && DateTime_1 != "" && UnitOfUse_1 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_1 + "','" + DateTime_1 + "','" + UnitOfUse_1 + "')");
                        }
                        if (i == 1 && AmountOfMoney_2 != "" && DateTime_2 != "" && UnitOfUse_2 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_2 + "','" + DateTime_2 + "','" + UnitOfUse_2 + "')");
                        }
                        if (i == 2 && AmountOfMoney_3 != "" && DateTime_3 != "" && UnitOfUse_3 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_3 + "','" + DateTime_3 + "','" + UnitOfUse_3 + "')");
                        }
                        if (i == 3 && AmountOfMoney_4 != "" && DateTime_4 != "" && UnitOfUse_4 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_4 + "','" + DateTime_4 + "','" + UnitOfUse_4 + "')");
                        }
                        if (i == 4 && AmountOfMoney_5 != "" && DateTime_5 != "" && UnitOfUse_5 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_5 + "','" + DateTime_5 + "','" + UnitOfUse_5 + "')");
                        }
                        if (i == 5 && AmountOfMoney_6 != "" && DateTime_6 != "" && UnitOfUse_6 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_6 + "','" + DateTime_6 + "','" + UnitOfUse_6 + "')");
                        }
                        if (i == 6 && AmountOfMoney_7 != "" && DateTime_7 != "" && UnitOfUse_7 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_7 + "','" + DateTime_7 + "','" + UnitOfUse_7 + "')");
                        }
                        if (i == 7 && AmountOfMoney_8 != "" && DateTime_8 != "" && UnitOfUse_8 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_8 + "','" + DateTime_8 + "','" + UnitOfUse_8 + "')");
                        }
                        if (i == 8 && AmountOfMoney_9 != "" && DateTime_9 != "" && UnitOfUse_9 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_9 + "','" + DateTime_9 + "','" + UnitOfUse_9 + "')");
                        }
                        if (i == 9 && AmountOfMoney_10 != "" && DateTime_10 != "" && UnitOfUse_10 != "")
                        {
                            DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_10 + "','" + DateTime_10 + "','" + UnitOfUse_10 + "')");
                        }
                    }
                }

                if (m_ActionName == "edit")    //修改
                {
                    m_SqlParams = "UPDATE BIZ_ProjectInfo SET P_Name='" + P_Name + "',P_SpecialNumber='" + P_SpecialNumber + "',P_IndicatorsNumber_1='" + P_IndicatorsNumber_1 + "',P_CumulativeNumber_1='" + P_CumulativeNumber_1 + "',P_Balance_1='" + P_Balance_1 + "',P_ApplicationsNumber_1='" + P_ApplicationsNumber_1 + "',P_UseFunds='" + P_UseFunds + "',P_PaymentMethod='" + P_PaymentMethod + "',P_PayeeName='" + P_PayeeName + "',P_AccountNumber='" + P_AccountNumber + "',P_BankAddress='" + P_BankAddress + "',P_ProjectType='" + P_ProjectType + "',P_UnitInfo='" + P_UnitInfo + "' WHERE P_ID=" + m_ObjID;
                    if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                    {
                        if (!string.IsNullOrEmpty(incDocsID))
                        {
                            incDocsID = incDocsID.Substring(0, incDocsID.Length - 1);
                            m_SqlParams = "UPDATE BIZ_ProjectInfo_Docs SET P_ID=" + m_ObjID + ",UserID = " + m_UserID + "  WHERE D_ID IN (" + incDocsID + ")";
                            DbHelperSQL.ExecuteSql(m_SqlParams);
                        }
                        Response.Write(" <script>alert('操作成功：[ " + P_Name + " ]信息修改成功！') ;window.location.href='" + m_TargetUrl + "'</script>");
                    }
                    else
                    {
                        Response.Write(" <script>alert('操作提示：信息修改操作失败！') ;window.location.href='" + m_TargetUrl + "'</script>");
                    }

                    //修改项目内容的使用信息
                    //判断之前是否有相应的数据

                    if (DbHelperSQL.GetSingle("select COUNT(*) from BIZ_ProjectPaymentPlan where P_ID = " + m_ObjID + " And FuncNo = '0201'").ToString() != "0")
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (i == 0 && AmountOfMoney_1 != "" && DateTime_1 != "" && UnitOfUse_1 != "" && ID_1 != "")
                            {
                                DbHelperSQL.ExecuteSql("UPDATE BIZ_ProjectPaymentPlan SET AmountOfMoney='" + AmountOfMoney_1 + "',DateTime='" + DateTime_1 + "',UnitOfUse='" + UnitOfUse_1 + "' WHERE ID=" + ID_1);
                            }
                            if (i == 1 && AmountOfMoney_2 != "" && DateTime_2 != "" && UnitOfUse_2 != "" && ID_2 != "")
                            {
                                DbHelperSQL.ExecuteSql("UPDATE BIZ_ProjectPaymentPlan SET AmountOfMoney='" + AmountOfMoney_2 + "',DateTime='" + DateTime_2 + "',UnitOfUse='" + UnitOfUse_2 + "' WHERE ID=" + ID_2);
                            }
                            if (i == 2 && AmountOfMoney_3 != "" && DateTime_3 != "" && UnitOfUse_3 != "" && ID_3 != "")
                            {
                                DbHelperSQL.ExecuteSql("UPDATE BIZ_ProjectPaymentPlan SET AmountOfMoney='" + AmountOfMoney_3 + "',DateTime='" + DateTime_3 + "',UnitOfUse='" + UnitOfUse_3 + "' WHERE ID=" + ID_3);
                            }
                            if (i == 3 && AmountOfMoney_4 != "" && DateTime_4 != "" && UnitOfUse_4 != "" && ID_4 != "")
                            {
                                DbHelperSQL.ExecuteSql("UPDATE BIZ_ProjectPaymentPlan SET AmountOfMoney='" + AmountOfMoney_4 + "',DateTime='" + DateTime_4 + "',UnitOfUse='" + UnitOfUse_4 + "' WHERE ID=" + ID_4);
                            }
                            if (i == 4 && AmountOfMoney_5 != "" && DateTime_5 != "" && UnitOfUse_5 != "" && ID_5 != "")
                            {
                                DbHelperSQL.ExecuteSql("UPDATE BIZ_ProjectPaymentPlan SET AmountOfMoney='" + AmountOfMoney_5 + "',DateTime='" + DateTime_5 + "',UnitOfUse='" + UnitOfUse_5 + "' WHERE ID=" + ID_5);
                            }
                            if (i == 5 && AmountOfMoney_6 != "" && DateTime_6 != "" && UnitOfUse_6 != "" && ID_6 != "")
                            {
                                DbHelperSQL.ExecuteSql("UPDATE BIZ_ProjectPaymentPlan SET AmountOfMoney='" + AmountOfMoney_6 + "',DateTime='" + DateTime_6 + "',UnitOfUse='" + UnitOfUse_6 + "' WHERE ID=" + ID_6);
                            }
                            if (i == 6 && AmountOfMoney_7 != "" && DateTime_7 != "" && UnitOfUse_7 != "" && ID_7 != "")
                            {
                                DbHelperSQL.ExecuteSql("UPDATE BIZ_ProjectPaymentPlan SET AmountOfMoney='" + AmountOfMoney_7 + "',DateTime='" + DateTime_7 + "',UnitOfUse='" + UnitOfUse_7 + "' WHERE ID=" + ID_7);
                            }
                            if (i == 7 && AmountOfMoney_8 != "" && DateTime_8 != "" && UnitOfUse_8 != "" && ID_8 != "")
                            {
                                DbHelperSQL.ExecuteSql("UPDATE BIZ_ProjectPaymentPlan SET AmountOfMoney='" + AmountOfMoney_8 + "',DateTime='" + DateTime_8 + "',UnitOfUse='" + UnitOfUse_8 + "' WHERE ID=" + ID_8);
                            }
                            if (i == 8 && AmountOfMoney_9 != "" && DateTime_9 != "" && UnitOfUse_9 != "" && ID_9 != "")
                            {
                                DbHelperSQL.ExecuteSql("UPDATE BIZ_ProjectPaymentPlan SET AmountOfMoney='" + AmountOfMoney_9 + "',DateTime='" + DateTime_9 + "',UnitOfUse='" + UnitOfUse_9 + "' WHERE ID=" + ID_9);
                            }
                            if (i == 9 && AmountOfMoney_10 != "" && DateTime_10 != "" && UnitOfUse_10 != "" && ID_10 != "")
                            {
                                DbHelperSQL.ExecuteSql("UPDATE BIZ_ProjectPaymentPlan SET AmountOfMoney='" + AmountOfMoney_10 + "',DateTime='" + DateTime_10 + "',UnitOfUse='" + UnitOfUse_10 + "' WHERE ID=" + ID_10);
                            }
                        }
                    }
                    else 
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (i == 0 && AmountOfMoney_1 != "" && DateTime_1 != "" && UnitOfUse_1 != "")
                            {
                                DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_1 + "','" + DateTime_1 + "','" + UnitOfUse_1 + "')");
                            }
                            if (i == 1 && AmountOfMoney_2 != "" && DateTime_2 != "" && UnitOfUse_2 != "")
                            {
                                DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_2 + "','" + DateTime_2 + "','" + UnitOfUse_2 + "')");
                            }
                            if (i == 2 && AmountOfMoney_3 != "" && DateTime_3 != "" && UnitOfUse_3 != "")
                            {
                                DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_3 + "','" + DateTime_3 + "','" + UnitOfUse_3 + "')");
                            }
                            if (i == 3 && AmountOfMoney_4 != "" && DateTime_4 != "" && UnitOfUse_4 != "")
                            {
                                DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_4 + "','" + DateTime_4 + "','" + UnitOfUse_4 + "')");
                            }
                            if (i == 4 && AmountOfMoney_5 != "" && DateTime_5 != "" && UnitOfUse_5 != "")
                            {
                                DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_5 + "','" + DateTime_5 + "','" + UnitOfUse_5 + "')");
                            }
                            if (i == 5 && AmountOfMoney_6 != "" && DateTime_6 != "" && UnitOfUse_6 != "")
                            {
                                DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_6 + "','" + DateTime_6 + "','" + UnitOfUse_6 + "')");
                            }
                            if (i == 6 && AmountOfMoney_7 != "" && DateTime_7 != "" && UnitOfUse_7 != "")
                            {
                                DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_7 + "','" + DateTime_7 + "','" + UnitOfUse_7 + "')");
                            }
                            if (i == 7 && AmountOfMoney_8 != "" && DateTime_8 != "" && UnitOfUse_8 != "")
                            {
                                DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_8 + "','" + DateTime_8 + "','" + UnitOfUse_8 + "')");
                            }
                            if (i == 8 && AmountOfMoney_9 != "" && DateTime_9 != "" && UnitOfUse_9 != "")
                            {
                                DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_9 + "','" + DateTime_9 + "','" + UnitOfUse_9 + "')");
                            }
                            if (i == 9 && AmountOfMoney_10 != "" && DateTime_10 != "" && UnitOfUse_10 != "")
                            {
                                DbHelperSQL.ExecuteSql("INSERT INTO [BIZ_ProjectPaymentPlan](FuncNo,P_ID,AmountOfMoney,DateTime,UnitOfUse) VALUES ('" + m_FuncCode + "'," + m_ObjID + ",'" + AmountOfMoney_10 + "','" + DateTime_10 + "','" + UnitOfUse_10 + "')");
                            }
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

