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
    public partial class EditBiz0105 : System.Web.UI.Page
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

        private BIZ_Persons m_PerA;//储存男方信息
        private BIZ_Persons m_PerB;//储存女方信息
        private BIZ_Contents m_BizC;//业务信息
        private BIZ_PersonChildren m_Children;//子女信息

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            //户籍地默认当前选择地区
            this.txtAreaSelRegNameB.ReadOnly = true;
            if (!IsPostBack)
            {

                //SetPageStyle(m_UserID);
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

        #region 获取内置参数 如：民族等
        /// <summary>
        /// 获取内置参数 如：民族等
        /// </summary>
        private void GetParam(string nationsA, string nationsB)
        {
            BIZ_Common.GetNations(this.LiteralNationsA, "txtNationsA", nationsA);
            BIZ_Common.GetNations(this.LiteralNationsB, "txtNationsB", nationsB);
        }
        #endregion

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
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                m_NavTitle = CommPage.GetSingleVal("SELECT BizNameFull FROM BIZ_Categories WHERE BizCode='" + m_FuncCode + "'");
                m_BizStep = CommPage.GetSingleVal("SELECT BizStep FROM BIZ_Categories WHERE BizCode='" + m_FuncCode + "'");

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
                case "edit": // 编辑
                    funcName = "编辑";
                    ShowModInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">管理首页</a> &gt;&gt; 业务办理 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }

        /// <summary>
        /// 修改 UcAreaSe08
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            try
            {
                this.m_BizC = new BIZ_Contents();
                this.m_Children = new BIZ_PersonChildren();

                /*查询语句，前置条件--必填*/
                this.m_BizC.SearchWhere = "BizID=" + objID;
                this.m_BizC.SelectAll(false);

                string Attribs = this.m_BizC.Attribs;
                this.txtAttribs.Value = Attribs;
                //库伦数据临时可编辑 2016/03/30 by Ysl
                bool isKuLunEdit = false;
                if (int.Parse(this.m_BizC.BizID) < 3099 && this.m_BizC.SelAreaCode.Substring(0, 6) == "150524") isKuLunEdit = true;
                if (Attribs != "0" && Attribs != "3" && Attribs != "6" && m_ActionName == "edit" && !isKuLunEdit)
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：只有“初始提交”和“补正”的业务才允许编辑！", m_TargetUrl, true, true);
                }
                else
                {
                    this.m_AreaCode = this.m_BizC.SelAreaCode;
                    //取证地点
                    this.UcAreaSelXian1.SetAreaCode(this.m_BizC.GetAreaCode, true);
                    /*女方：身份证号、姓名、婚姻状况、婚姻时间、民族、联系电话、户籍性质、户籍地址*/
                    this.txtPersonCidB.Text = this.m_BizC.PersonCidB;
                    this.txtPersonNameB.Text = this.m_BizC.Fileds08;
                    this.ddlMarryTypeB.SelectedValue = this.m_BizC.Fileds13;
                    this.txtMarryDateB.Value = this.m_BizC.Fileds14;
                    string NationsB = this.m_BizC.Fileds10;
                    this.txtContactTelB.Text = this.m_BizC.ContactTelB;
                    this.ddlRegisterTypeB.SelectedValue = this.m_BizC.Fileds11;
                    this.txtAreaSelRegNameB.Text = this.m_BizC.RegAreaNameB;
                    this.txtAreaSelRegCodeB.Value = this.m_BizC.RegAreaCodeB;


                    /*男方：身份证号、姓名、婚姻状况、婚姻时间、民族、联系电话、户籍性质*/
                    this.txtPersonCidA.Text = this.m_BizC.PersonCidA;
                    this.txtPersonNameA.Text = this.m_BizC.Fileds01;
                    this.ddlMarryTypeA.SelectedValue = this.m_BizC.Fileds33;
                    this.txtMarryDateA.Value = this.m_BizC.Fileds34;
                    string NationsA = this.m_BizC.Fileds03;                   
                    this.ddlRegisterTypeA.SelectedValue = this.m_BizC.Fileds04;

                    GetParam(NationsA, NationsB);

                    //===========================子女信息=========================================                    
                    this.ddlFileds40.SelectedValue = this.m_BizC.Fileds40;
                    this.ddlFileds37.SelectedValue = this.m_BizC.Fileds37;
                    this.ddlFileds38.SelectedValue = this.m_BizC.Fileds38;


                    //===========================避孕信息========================================= 
                    this.ddlFileds45.SelectedValue = this.m_BizC.Fileds45;
                    this.ddlFileds46.SelectedValue = this.m_BizC.Fileds46;
                    this.txtFileds44.Value = this.m_BizC.Fileds44;

                    //申请理由 申请日期
                    this.txtFileds18.Text = this.m_BizC.Fileds18;

                    //if (!String.IsNullOrEmpty(this.m_BizC.PersonPhotos))
                    //{
                    //    this.txtIsHasPhotos.Value = "1";
                    //    this.LiteralPersonPhotos.Text = "<tr><th>已传照片：</th><td><img src=\"" + this.m_BizC.PersonPhotos + "\" width=\"50\" height=\"50\"></td></tr>";
                    //}
                    //子女信息
                    string BirthNum = this.m_BizC.Fileds07;                   

                    this.m_Children.Select("", objID);

                    this.txtChildName1.Text = this.m_Children.ChildName1;
                    this.ddlChildSex1.SelectedValue = this.m_Children.ChildSex1;
                    this.txtChildBirthday1.Value = this.m_Children.ChildBirthday1;
                    this.ddlChildSurvivalStatus1.SelectedValue = this.m_Children.ChildSurvivalStatus1;
                    this.ddlChildSource1.SelectedValue = this.m_Children.ChildSource1;
                    this.txtChildID1.Value = this.m_Children.CommID1;

                    this.txtChildName2.Text = this.m_Children.ChildName2;
                    this.ddlChildSex2.SelectedValue = this.m_Children.ChildSex2;
                    this.txtChildBirthday2.Value = this.m_Children.ChildBirthday2;
                    this.ddlChildSurvivalStatus2.SelectedValue = this.m_Children.ChildSurvivalStatus2;
                    this.ddlChildSource2.SelectedValue = this.m_Children.ChildSource2;
                    this.txtChildID2.Value = this.m_Children.CommID2;

                    this.txtChildName3.Text = this.m_Children.ChildName3;
                    this.ddlChildSex3.SelectedValue = this.m_Children.ChildSex3;
                    this.txtChildBirthday3.Value = this.m_Children.ChildBirthday3;
                    this.ddlChildSurvivalStatus3.SelectedValue = this.m_Children.ChildSurvivalStatus3;
                    this.ddlChildSource3.SelectedValue = this.m_Children.ChildSource3;
                    this.txtChildID3.Value = this.m_Children.CommID3;

                    this.txtChildName4.Text = this.m_Children.ChildName4;
                    this.ddlChildSex4.SelectedValue = this.m_Children.ChildSex4;
                    this.txtChildBirthday4.Value = this.m_Children.ChildBirthday4;
                    this.ddlChildSurvivalStatus4.SelectedValue = this.m_Children.ChildSurvivalStatus4;
                    this.ddlChildSource4.SelectedValue = this.m_Children.ChildSource4;
                    this.txtChildID4.Value = this.m_Children.CommID4;
                }
                this.m_BizC = null;
            }
            catch { }
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
            string strErr = string.Empty;
            this.m_PerA = new BIZ_Persons();
            this.m_PerB = new BIZ_Persons();
            this.m_BizC = new BIZ_Contents();
            this.m_Children = new BIZ_PersonChildren();
            string app = string.Empty;

            string bizAttribs = this.txtAttribs.Value;
            if (String.IsNullOrEmpty(bizAttribs)) strErr += "业务状态缺失，请重试！\\n";
            #region 参数

            /*女方：身份证号、姓名、性别、出生年月、婚姻状况、婚姻时间、民族、联系电话、户籍性质、户籍地址*/
            this.m_PerB.PersonCardID = PageValidate.GetTrim(this.txtPersonCidB.Text);
            this.m_PerB.PersonName = PageValidate.GetTrim(this.txtPersonNameB.Text);
            this.m_PerB.PersonSex = "女";
            this.m_PerB.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerB.PersonCardID);
            this.m_PerB.MarryType = PageValidate.GetTrim(this.ddlMarryTypeB.SelectedValue);
            this.m_PerB.MarryDate = CommBiz.GetTrim(this.txtMarryDateB.Value);
            this.m_PerB.Nations = PageValidate.GetTrim(Request["txtNationsB"]);
            this.m_PerB.PersonTel = PageValidate.GetTrim(this.txtContactTelB.Text);
            this.m_PerB.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeB.Text);
            this.m_PerB.RegisterAreaCode = PageValidate.GetTrim(this.txtAreaSelRegCodeB.Value);
            this.m_PerB.RegisterAreaName = PageValidate.GetTrim(this.txtAreaSelRegNameB.Text);

            /*男方：身份证号、姓名、性别、出生年月、婚姻状况、婚姻时间、民族、联系电话、户籍性质、户籍地址*/
            this.m_PerA.PersonCardID = PageValidate.GetTrim(this.txtPersonCidA.Text);
            this.m_PerA.PersonName = PageValidate.GetTrim(this.txtPersonNameA.Text);
            this.m_PerA.PersonSex = "男";
            this.m_PerA.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerA.PersonCardID);
            this.m_PerA.MarryType = PageValidate.GetTrim(this.ddlMarryTypeA.SelectedValue);
            this.m_PerA.MarryDate = CommBiz.GetTrim(this.txtMarryDateA.Value);
            this.m_PerA.Nations = PageValidate.GetTrim(Request["txtNationsA"]);
            this.m_PerA.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeA.Text);


            //===========================子女信息=========================================  
            string Fileds40 = PageValidate.GetTrim(this.ddlFileds40.SelectedValue);
            string Fileds37 = PageValidate.GetTrim(this.ddlFileds37.SelectedValue);
            string Fileds38 = PageValidate.GetTrim(this.ddlFileds38.SelectedValue);
            this.m_PerB.BirthNum = (int.Parse(Fileds37) + int.Parse(Fileds38)).ToString();

            //===========================避孕信息========================================= 
            string Fileds45 = PageValidate.GetTrim(this.ddlFileds45.SelectedValue);
            string Fileds46 = PageValidate.GetTrim(this.ddlFileds46.SelectedValue);
            string Fileds44 = CommBiz.GetTrim(this.txtFileds44.Value);


            //==========================女方信息 start========================================== 
            if (String.IsNullOrEmpty(this.m_PerB.PersonCardID)) { strErr += "请输入女方身份证号！\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerB.PersonCardID)) { strErr += "女方身份证号有误！！\\n"; }
            if (CommBiz.GetAgeByID(this.m_PerB.PersonCardID) > 49) { strErr += "女方必须在49周岁内才可以申请！"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonName)) { strErr += "请输入女方姓名！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryType)) { strErr += "请选择女方婚姻状况！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryDate)) { strErr += "请选择女方婚姻时间！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.Nations)) { strErr += "请输入女方民族！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.RegisterType)) { strErr += "请选择女方户籍性质！\\n"; }
            //if (!CommPage.IsAreaCode(this.m_PerB.RegisterAreaCode, "0")) { strErr += "请选择女方户籍地！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonTel)) { strErr += "请输入女方联系电话！\\n"; }

            //==========================男方信息 start========================================== 
            if (String.IsNullOrEmpty(this.m_PerA.PersonCardID)) { strErr += "请输入男方身份证号！\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerA.PersonCardID)) { strErr += "男方身份证号有误！！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.PersonName)) { strErr += "请输入男方姓名！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.MarryType)) { strErr += "请选择男方婚姻状况！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.MarryDate)) { strErr += "请选择男方婚姻时间！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.Nations)) { strErr += "请输入男方民族！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.RegisterType)) { strErr += "请选择男方户籍性质！\\n"; }


            //===========================子女信息=========================================  
            if (String.IsNullOrEmpty(Fileds40)) { strErr += "请选择按政策可生育子女数！\\n"; }
            if (String.IsNullOrEmpty(Fileds37)) { strErr += "请选择现有子女男孩个数！\\n"; }
            if (String.IsNullOrEmpty(Fileds38)) { strErr += "请选择现有子女女孩个数！\\n"; }

            //===========================避孕信息========================================= 
            if (String.IsNullOrEmpty(Fileds45) && String.IsNullOrEmpty(Fileds46)) { strErr += "请选择避孕状况！\\n"; }
            if (String.IsNullOrEmpty(Fileds44)) { strErr += "请选择避孕开始日期！\\n"; }

            //子女信息
            int iBirthNum = int.Parse(this.m_PerB.BirthNum);
            #region 子女信息
            string ChildName1 = PageValidate.GetTrim(this.txtChildName1.Text);
            string ChildSex1 = PageValidate.GetTrim(this.ddlChildSex1.SelectedValue);
            string ChildBirthday1 = CommBiz.GetTrim(this.txtChildBirthday1.Value);
            string ChildSurvivalStatus1 = PageValidate.GetTrim(this.ddlChildSurvivalStatus1.Text);
            string ChildSource1 = PageValidate.GetTrim(this.ddlChildSource1.Text);
            string ChildNo1 = "1";

            string ChildName2 = PageValidate.GetTrim(this.txtChildName2.Text);
            string ChildSex2 = PageValidate.GetTrim(this.ddlChildSex2.SelectedValue);
            string ChildBirthday2 = CommBiz.GetTrim(this.txtChildBirthday2.Value);
            string ChildSurvivalStatus2 = PageValidate.GetTrim(this.ddlChildSurvivalStatus2.Text);
            string ChildSource2 = PageValidate.GetTrim(this.ddlChildSource2.Text);
            string ChildNo2 = "2";

            string ChildName3 = PageValidate.GetTrim(this.txtChildName3.Text);
            string ChildSex3 = PageValidate.GetTrim(this.ddlChildSex3.SelectedValue);
            string ChildBirthday3 = CommBiz.GetTrim(this.txtChildBirthday3.Value);
            string ChildSurvivalStatus3 = PageValidate.GetTrim(this.ddlChildSurvivalStatus3.Text);
            string ChildSource3 = PageValidate.GetTrim(this.ddlChildSource3.Text);
            string ChildNo3 = "3";

            string ChildName4 = PageValidate.GetTrim(this.txtChildName4.Text);
            string ChildSex4 = PageValidate.GetTrim(this.ddlChildSex4.SelectedValue);
            string ChildBirthday4 = CommBiz.GetTrim(this.txtChildBirthday4.Value);
            string ChildSurvivalStatus4 = PageValidate.GetTrim(this.ddlChildSurvivalStatus4.Text);
            string ChildSource4 = PageValidate.GetTrim(this.ddlChildSource4.Text);
            string ChildNo4 = "4";

            if (iBirthNum == 0)
            {
                this.m_PerB.BirthNum = "0";
                ChildName1 = ""; ChildName2 = ""; ChildName3 = ""; ChildName4 = "";
            }
            else if (iBirthNum == 1)
            {
                ChildName2 = ""; ChildName3 = ""; ChildName4 = "";
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "请输入子女1姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "请选择子女1性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "请选择子女1出生年月！\\n"; }
            }
            else if (iBirthNum == 2)
            {
                ChildName3 = ""; ChildName4 = "";

                if (String.IsNullOrEmpty(ChildName1)) { strErr += "请输入子女1姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "请选择子女1性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "请选择子女1出生年月！\\n"; }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "请输入子女2姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "请选择子女2性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "请选择子女2出生年月！\\n"; }

                if (ChildName1 == ChildName2) { strErr += "子女姓名重复！\\n"; }
            }
            else if (iBirthNum == 3)
            {
                ChildName4 = "";
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "请输入子女1姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "请选择子女1性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "请选择子女1出生年月！\\n"; }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "请输入子女2姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "请选择子女2性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "请选择子女2出生年月！\\n"; }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "请输入子女3姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "请选择子女3性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "请选择子女3出生年月！\\n"; }

                if (ChildName1 == ChildName2 || ChildName1 == ChildName3 || ChildName2 == ChildName3) { strErr += "子女姓名重复！\\n"; }
            }
            else if (iBirthNum == 4)
            {
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "请输入子女1姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "请选择子女1性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "请选择子女1出生年月！\\n"; }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "请输入子女2姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "请选择子女2性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "请选择子女2出生年月！\\n"; }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "请输入子女3姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "请选择子女3性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "请选择子女3出生年月！\\n"; }

                if (String.IsNullOrEmpty(ChildName4)) { strErr += "请输入子女4姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex4)) { strErr += "请选择子女4性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday4)) { strErr += "请选择子女4出生年月！\\n"; }

                if (ChildName1 == ChildName2 || ChildName1 == ChildName3 || ChildName1 == ChildName4 || ChildName2 == ChildName3 || ChildName2 == ChildName4 || ChildName3 == ChildName4) { strErr += "子女姓名重复！\\n"; }
            }
            this.m_Children.ChildName1 = ChildName1;
            this.m_Children.ChildSex1 = ChildSex1;
            this.m_Children.ChildBirthday1 = ChildBirthday1;
            this.m_Children.ChildSurvivalStatus1 = ChildSurvivalStatus1;
            this.m_Children.ChildSource1 = ChildSource1;
            this.m_Children.ChildNo1 = ChildNo1;

            this.m_Children.ChildName2 = ChildName2;
            this.m_Children.ChildSex2 = ChildSex2;
            this.m_Children.ChildBirthday2 = ChildBirthday2;
            this.m_Children.ChildSurvivalStatus2 = ChildSurvivalStatus2;
            this.m_Children.ChildSource2 = ChildSource2;
            this.m_Children.ChildNo2 = ChildNo2;

            this.m_Children.ChildName3 = ChildName3;
            this.m_Children.ChildSex3 = ChildSex3;
            this.m_Children.ChildBirthday3 = ChildBirthday3;
            this.m_Children.ChildSurvivalStatus3 = ChildSurvivalStatus3;
            this.m_Children.ChildSource3 = ChildSource3;
            this.m_Children.ChildNo3 = ChildNo3;

            this.m_Children.ChildName4 = ChildName4;
            this.m_Children.ChildSex4 = ChildSex4;
            this.m_Children.ChildBirthday4 = ChildBirthday4;
            this.m_Children.ChildSurvivalStatus4 = ChildSurvivalStatus4;
            this.m_Children.ChildSource4 = ChildSource4;
            this.m_Children.ChildNo4 = ChildNo4;
            #endregion

            //申请理由 申请日期
            string Fileds18 = PageValidate.GetTrim(this.txtFileds18.Text);
            //string Fileds19 = CommBiz.GetTrim(this.txtFileds19.Value);
            if (String.IsNullOrEmpty(Fileds18)) { strErr += "请输入申请理由！\\n"; }
            // if (String.IsNullOrEmpty(Fileds19)) { strErr += "请选择申请日期！\\n"; }
            #endregion

            //取证地点
            string GetAreaCode = PageValidate.GetTrim(this.UcAreaSelXian1.GetAreaCode());
            string GetAreaName = PageValidate.GetTrim(this.UcAreaSelXian1.GetAreaName());

            //2寸红底免冠夫妇合影照片
            //string strMsg = string.Empty;
            //string[] PersonPhotos = new string[3];
            //if (this.txtPersonPhotos.HasFile)
            //{
            //    if (!BIZ_UpFhotos.UploadPhotos(this.txtPersonPhotos, ref PersonPhotos, "0", ref strMsg))
            //    {
            //        strErr += "2寸红底免冠夫妇合影照片操作失败！\\n";
            //    }
            //}

            if (cbOk.Checked == false)
            {
                strErr += "请确认承诺！\\n";
            }
            if (strErr != "")
            {
                ShowModInfo(m_ObjID);
                MessageBox.Show(this, strErr);
                return;
            }
            try
            {
                #region update
                this.m_BizC.BizID = this.m_ObjID;

                //取证地点
                this.m_BizC.GetAreaCode = GetAreaCode;
                this.m_BizC.GetAreaName = GetAreaName;

                //2寸红底免冠夫妇合影照片
                //if (!String.IsNullOrEmpty(PersonPhotos[1])) { this.m_BizC.PersonPhotos = PersonPhotos[1]; }

                /*男方：身份证号、姓名、性别、出生年月、婚姻状况、婚姻时间、民族、联系电话、户籍性质*/
                this.m_BizC.PersonCidA = this.m_PerA.PersonCardID;
                this.m_BizC.Fileds01 = this.m_PerA.PersonName;
                this.m_BizC.Fileds02 = this.m_PerA.PersonSex;
                this.m_BizC.Fileds32 = this.m_PerA.PersonBirthday;
                this.m_BizC.Fileds33 = this.m_PerA.MarryType;
                this.m_BizC.Fileds34 = this.m_PerA.MarryDate;
                this.m_BizC.Fileds03 = this.m_PerA.Nations;
                this.m_BizC.ContactTelA = this.m_PerA.PersonTel;
                this.m_BizC.Fileds04 = this.m_PerA.RegisterType;

                /*女方：身份证号、姓名、性别、出生年月、婚姻状况、婚姻时间、民族、联系电话、户籍性质、户籍地址*/
                this.m_BizC.PersonCidB = this.m_PerB.PersonCardID;
                this.m_BizC.Fileds08 = this.m_PerB.PersonName;
                this.m_BizC.Fileds09 = this.m_PerB.PersonSex;
                this.m_BizC.Fileds31 = this.m_PerB.PersonBirthday;
                this.m_BizC.Fileds13 = this.m_PerB.MarryType;
                this.m_BizC.Fileds14 = this.m_PerB.MarryDate;
                this.m_BizC.Fileds10 = this.m_PerB.Nations;
                this.m_BizC.ContactTelB = this.m_PerB.PersonTel;
                this.m_BizC.Fileds11 = this.m_PerB.RegisterType;
                //this.m_BizC.RegAreaCodeB = this.m_PerB.RegisterAreaCode;
                //this.m_BizC.RegAreaNameB = this.m_PerB.RegisterAreaName;

                //===========================子女信息=========================================  
                this.m_BizC.Fileds40 = Fileds40;
                this.m_BizC.Fileds37 = Fileds37;
                this.m_BizC.Fileds38 = Fileds38;
                this.m_BizC.Fileds07 = this.m_PerB.BirthNum;


                //===========================避孕信息========================================= 
                this.m_BizC.Fileds45 = Fileds45;
                this.m_BizC.Fileds46 = Fileds46;
                this.m_BizC.Fileds44 = Fileds44;

                //申请理由 申请日期
                this.m_BizC.Fileds18 = Fileds18;
                int cm = this.m_BizC.Update();
                this.m_BizC = null;

                string objID = this.m_ObjID;
                if (cm > 0)
                {
                    CommPage.SetBizLog(objID, m_UserID, "业务修改", "管理员用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 进行了《" + this.m_NavTitle + "》修改操作");
                    /*以下：1.判断群众基础表中是否存在，不存在插入，存在更新  --判断依据：身份证号
                            2.判断婚姻表中是否存在该用户信息，不存在插入，存在不跟新  --判断依据：群众编号
                            3.判断子女表中是否存在该用户现有家庭子女信息，不存在插入，存在不更新  --判断依据：群众编号
                     */
                    #region 具体操作
                    //

                    this.m_PerA.MateName = this.m_PerB.PersonName;
                    this.m_PerA.MateCardID = this.m_PerB.PersonCardID;
                    string PersonIDA = this.m_PerA.ExecBizPersons();

                    this.m_PerB.MateName = this.m_PerA.PersonName;
                    this.m_PerB.MateCardID = this.m_PerA.PersonCardID;
                    string PersonIDB = this.m_PerB.ExecBizPersons();
                    //修改子女信息

                    this.m_Children.Inser(PersonIDB, objID, iBirthNum.ToString());
                    #endregion
                    //if (!String.IsNullOrEmpty(PersonPhotos[1]))
                    //{
                    //    if (!String.IsNullOrEmpty(this.txtIsHasPhotos.Value)) { BIZ_Common.UpdateBizDocs(objID, PersonIDB, "", "2寸红底免冠夫妇合影照片", PersonPhotos); }
                    //    else { BIZ_Common.InsetBizDocs(objID, PersonIDB, "2寸红底免冠夫妇合影照片", PersonPhotos, "1"); }
                    //}

                    MessageBox.ShowAndRedirect(this.Page, "", "EditBizDocs.aspx?action=edit&k=" + objID + "&sourceUrl=" + m_SourceUrl, true, true);
                }
                else
                {
                    MessageBox.Show(this, "操作提示：操作失败，请联系系统管理员！");
                    return;
                }
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                //Response.Write(" <script>alert('操作失败：" + ex.Message + "') ;</script>");
                return;
            }
        }
        #endregion
    }
}

