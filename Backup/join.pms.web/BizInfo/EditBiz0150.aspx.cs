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
    public partial class EditBiz0150 : System.Web.UI.Page
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
                if (Attribs != "0" && Attribs != "3" && Attribs != "6" && m_ActionName == "edit")
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：只有“初始提交”和“补正”的业务才允许编辑！", m_TargetUrl, true, true);
                }
                else
                {
                    this.m_AreaCode = this.m_BizC.SelAreaCode;
                    /*女方：姓名、出生年月、身份证号、民族、工作单位、户籍地址、现居住地、婚姻状况、联系电话*/
                    this.txtPersonNameB.Text = this.m_BizC.Fileds08;
                    this.txtPersonCidB.Text = this.m_BizC.PersonCidB;
                    string NationsB = this.m_BizC.Fileds10;
                    this.txtWorkUnitB.Text = this.m_BizC.Fileds12;
                    this.UcAreaSelRegB.SetAreaName(this.m_BizC.RegAreaNameB);
                    this.UcAreaSelRegB.SetAreaCode (this.m_BizC.RegAreaCodeB);
                    this.UcAreaSelCurB.SetAreaName(this.m_BizC.CurAreaNameB);
                    this.UcAreaSelCurB.SetAreaCode(this.m_BizC.CurAreaCodeB);
                    this.ddlMarryTypeB.SelectedValue = this.m_BizC.Fileds13;
                    this.ddlCidTypeB.SelectedValue = this.m_BizC.Fileds50;
                    this.ddlRegisterTypeB.SelectedValue = this.m_BizC.Fileds11;
                    this.ddlFileds23.SelectedValue = this.m_BizC.Fileds23;
                    //this.txtContactTelB.Text = this.m_BizC.ContactTelB;

                    /*男方：姓名、出生年月、身份证号、民族、工作单位、户籍地址、现居住地、婚姻状况、联系电话*/
                    this.txtPersonNameA.Text = this.m_BizC.Fileds01;
                    this.txtPersonCidA.Text = this.m_BizC.PersonCidA;
                    string NationsA = this.m_BizC.Fileds03;
                    this.txtWorkUnitA.Text = this.m_BizC.Fileds05;
                    this.UcAreaSelRegA.SetAreaName(this.m_BizC.RegAreaNameA);
                    this.UcAreaSelRegA.SetAreaCode(this.m_BizC.RegAreaCodeA);
                    this.UcAreaSelCurA.SetAreaName(this.m_BizC.CurAreaNameA);
                    this.UcAreaSelCurA.SetAreaCode(this.m_BizC.CurAreaCodeA);
                    this.ddlMarryTypeA.SelectedValue = this.m_BizC.Fileds33;
                    this.txtContactTelA.Text = this.m_BizC.ContactTelA;
                    this.ddlFileds24.SelectedValue = this.m_BizC.Fileds24;
                    this.ddlCidTypeA.SelectedValue = this.m_BizC.Fileds49;
                    this.ddlRegisterTypeA.SelectedValue = this.m_BizC.Fileds04;

                    GetParam(NationsA, NationsB);

                    //双方生育(收养)子女数
                    //string BirthNum = this.m_BizC.Fileds37;
                    //this.ddlBirthNum.SelectedValue = BirthNum;

                    //this.m_Children.Select("", objID);

                    //this.txtChildName1.Text = this.m_Children.ChildName1;
                    //this.ddlChildSex1.SelectedValue = this.m_Children.ChildSex1;
                    //this.txtChildBirthday1.Value = this.m_Children.ChildBirthday1;
                    //this.txtChildCardID1.Text = this.m_Children.ChildCardID1;
                    //this.txtFatherName1.Text = this.m_Children.FatherName1;
                    //this.txtMotherName1.Text = this.m_Children.MotherName1;
                    //this.ddlChildPolicy1.SelectedValue = this.m_Children.ChildPolicy1;
                    //this.txtChildSource1.Text = this.m_Children.ChildSource1;
                    //this.txtChildID1.Value = this.m_Children.CommID1;

                    //this.txtChildName2.Text = this.m_Children.ChildName2;
                    //this.ddlChildSex2.SelectedValue = this.m_Children.ChildSex2;
                    //this.txtChildBirthday2.Value = this.m_Children.ChildBirthday2;
                    //this.txtChildCardID2.Text = this.m_Children.ChildCardID2;
                    //this.txtFatherName2.Text = this.m_Children.FatherName2;
                    //this.txtMotherName2.Text = this.m_Children.MotherName2;
                    //this.ddlChildPolicy2.SelectedValue = this.m_Children.ChildPolicy2;
                    //this.txtChildSource2.Text = this.m_Children.ChildSource2;
                    //this.txtChildID2.Value = this.m_Children.CommID2;

                    //this.txtChildName3.Text = this.m_Children.ChildName3;
                    //this.ddlChildSex3.SelectedValue = this.m_Children.ChildSex3;
                    //this.txtChildBirthday3.Value = this.m_Children.ChildBirthday3;
                    //this.txtChildCardID3.Text = this.m_Children.ChildCardID3;
                    //this.txtFatherName3.Text = this.m_Children.FatherName3;
                    //this.txtMotherName3.Text = this.m_Children.MotherName3;
                    //this.ddlChildPolicy3.SelectedValue = this.m_Children.ChildPolicy3;
                    //this.txtChildSource3.Text = this.m_Children.ChildSource3;
                    //this.txtChildID3.Value = this.m_Children.CommID3;
                    ////this.txtFileds45.Text = this.m_BizC.Fileds45;



                    this.txtFileds14.Value = this.m_BizC.Fileds14;
                    this.ddlFileds47.Text = this.m_BizC.Fileds47;
                    ////this.txtFileds44.Value = this.m_BizC.Fileds44;
                    //this.txtFileds18.Value = this.m_BizC.Fileds18;
                    //this.ddlChildSex.SelectedValue = this.m_BizC.Fileds17;
                    //if (!String.IsNullOrEmpty(this.m_BizC.Fileds17))
                    //{
                    //    this.ddlIsBirth.SelectedValue = "是";
                    //    this.panelBirth.Attributes["style"] = "display:block";
                    //}
                    //if (!String.IsNullOrEmpty(this.m_BizC.Fileds43))
                    //{
                    //    this.ddlIsHY.SelectedValue = "是";
                    //    this.panelHY.Attributes["style"] = "display:block";
                    //}
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
            string CidTypeA = string.Empty;
            string CidTypeB = string.Empty;
            this.m_PerA = new BIZ_Persons();
            this.m_PerB = new BIZ_Persons();
            this.m_BizC = new BIZ_Contents();
            this.m_Children = new BIZ_PersonChildren();
            string app = string.Empty;

            string bizAttribs = this.txtAttribs.Value;
            if (String.IsNullOrEmpty(bizAttribs)) strErr += "业务状态缺失，请重试！\\n";
            #region 参数

            string Fileds14 = CommBiz.GetTrim(this.txtFileds14.Value);
            string Fileds47 = CommBiz.GetTrim(this.ddlFileds47.Text);
            string Fileds23 = CommBiz.GetTrim(this.ddlFileds23.SelectedValue);
            string Fileds24 = CommBiz.GetTrim(this.ddlFileds24.SelectedValue);
            if (String.IsNullOrEmpty(Fileds14)) { strErr += "请选择结婚时间！\\n"; }
            if (String.IsNullOrEmpty(Fileds47)) { strErr += "请选择结婚证号！\\n"; }
            /*女方：姓名、出生年月、身份证号、民族、工作单位、户籍地址、现居住地、婚姻状况、联系电话*/

            this.m_PerB.PersonName = PageValidate.GetTrim(this.txtPersonNameB.Text);
            this.m_PerB.PersonSex = "女";
            this.m_PerB.PersonCardID = PageValidate.GetTrim(this.txtPersonCidB.Text);
            this.m_PerB.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerB.PersonCardID);
            this.m_PerB.Nations = PageValidate.GetTrim(Request["txtNationsB"]);
            this.m_PerB.WorkUnit = PageValidate.GetTrim(this.txtWorkUnitB.Text);
            this.m_PerB.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaCode());
            this.m_PerB.RegisterAreaName = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaName());
            this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
            this.m_PerB.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaName());
            this.m_PerB.MarryType = PageValidate.GetTrim(this.ddlMarryTypeB.SelectedValue);
            this.m_PerB.MarryDate = Fileds14;
            this.m_PerB.MarryCardID = Fileds47;
            this.m_PerB.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeB.SelectedValue);
            this.m_PerB.PersonTel = CommPage.replaceNUM(PageValidate.GetTrim(this.txtContactTelA.Text));

            /*男方：姓名、出生年月、身份证号、民族、工作单位、户籍地址、现居住地、婚姻状况、联系电话*/
            string PersonNameA = PageValidate.GetTrim(this.txtPersonNameA.Text);
            string PersonSexA = "男";
            string PersonCardIDA = PageValidate.GetTrim(this.txtPersonCidA.Text);
            string NationsA = PageValidate.GetTrim(Request["txtNationsA"]);
            string WorkUnitA = PageValidate.GetTrim(this.txtWorkUnitA.Text);
            string RegisterAreaCodeA = PageValidate.GetTrim(this.UcAreaSelRegA.GetAreaCode());
            string RegisterAreaNameA = PageValidate.GetTrim(this.UcAreaSelRegA.GetAreaName());
            string CurrentAreaCodeA = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
            string CurrentAreaNameA = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaName());
            string MarryTypeA = PageValidate.GetTrim(this.ddlMarryTypeA.SelectedValue);
            string PersonTelA = this.m_PerB.PersonTel;
            this.m_PerA.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeA.SelectedValue);
            CidTypeB = PageValidate.GetTrim(this.ddlCidTypeB.SelectedValue);
            CidTypeA = PageValidate.GetTrim(this.ddlCidTypeA.SelectedValue);

            //====================================================================    

            //==========================女方信息 start========================================== 
            if (String.IsNullOrEmpty(this.m_PerB.PersonName)) { strErr += "请输入女方姓名！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonCardID)) { strErr += "请输入女方证件号码！\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerB.PersonCardID) && CidTypeB == "1") { strErr += "女方身份证号有误！！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.Nations)) { strErr += "请选择女方民族！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.WorkUnit)) { strErr += "请输入女方工作单位！\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerB.RegisterAreaCode, "0")) { strErr += "请选择女方户籍地！\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerB.CurrentAreaCode, "0")) { strErr += "请选择女方现居住地！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryType)) { strErr += "请选择女方婚姻状况！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonTel)) { strErr += "请输入联系电话！\\n"; }

            //==========================男方信息 start========================================== 
            if (this.m_PerB.MarryType == "初婚" || this.m_PerB.MarryType == "再婚")
            {
                /*男方：姓名、出生年月、身份证号、民族、工作单位、户籍地址、现居住地、婚姻状况、联系电话*/
                this.m_PerA.PersonName = PersonNameA;
                this.m_PerA.PersonSex = PersonSexA;
                this.m_PerA.PersonCardID = PersonCardIDA;
                this.m_PerA.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerA.PersonCardID);
                this.m_PerA.Nations = NationsA;
                this.m_PerA.WorkUnit = WorkUnitA;
                this.m_PerA.RegisterAreaCode = RegisterAreaCodeA;
                this.m_PerA.RegisterAreaName = RegisterAreaNameA;
                this.m_PerA.CurrentAreaCode = CurrentAreaCodeA;
                this.m_PerA.CurrentAreaName = CurrentAreaNameA;
                this.m_PerA.MarryType = MarryTypeA;
                this.m_PerA.MarryDate = Fileds14;
                this.m_PerA.MarryCardID = Fileds47;
                this.m_PerA.PersonTel = PersonTelA;

                if (String.IsNullOrEmpty(this.m_PerA.PersonName)) { strErr += "请输入男方姓名！\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.PersonCardID)) { strErr += "请输入男方证件号码！\\n"; }
                if (!ValidIDCard.VerifyIDCard(this.m_PerA.PersonCardID) && CidTypeB == "1") { strErr += "男方身份证号有误！！\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.Nations)) { strErr += "请选择男方民族！\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.WorkUnit)) { strErr += "请输入男方工作单位！\\n"; }
                if (!CommPage.IsAreaCode(this.m_PerA.RegisterAreaCode, "0")) { strErr += "请选择男方户籍地！\\n"; }
                if (!CommPage.IsAreaCode(this.m_PerA.CurrentAreaCode, "0")) { strErr += "请选择男方现居住地！\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.MarryType)) { strErr += "请选择男方婚姻状况！\\n"; }
                //if (String.IsNullOrEmpty(this.m_PerA.PersonTel)) { strErr += "请输入男方联系电话！\\n"; }
                //if (this.m_PerA.RegisterAreaCode != m_AreaCode && this.m_PerA.CurrentAreaCode != m_AreaCode && this.m_PerB.RegisterAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "男女双方户籍地或现居住地至少要有一方与所选择地一致！\\n"; }

                if (this.m_PerB.RegisterAreaCode == m_AreaCode || this.m_PerB.CurrentAreaCode == m_AreaCode) { app = "B"; }
                else { app = "A"; }
            }
            else
            {
                this.m_BizStep = "2";
                app = "B";
                //if (this.m_PerB.RegisterAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "女双方户籍地或现居住地至少要有一处与所选择地一致！\\n"; }

                ////受理单位指定
                //areaCodeSL = this.m_PerB.CurrentAreaCode;
            }

            /*婚姻及孕产信息：结婚时间、怀孕时间、预产日期、出生日期、孩子性别、出生医学证明编号*/
            //if (String.IsNullOrEmpty(this.txtFileds14.Value)) { strErr += "请选择结婚时间！\\n"; }
            //if (this.ddlIsHY.SelectedValue == "是" && String.IsNullOrEmpty(this.txtFileds43.Value)) { strErr += "请选择怀孕时间！\\n"; }
            ////if (String.IsNullOrEmpty(this.txtFileds44.Value)) { strErr += "请选择预产日期！\\n"; }
            //if (this.ddlIsBirth.SelectedValue == "是" && String.IsNullOrEmpty(this.txtFileds18.Value)) { strErr += "请选择孩子出生日期！\\n"; }
            //if (this.ddlIsBirth.SelectedValue == "是" && this.ddlChildSex.SelectedValue == "") { strErr += "请选择孩子性别！\\n"; }
            //if (String.IsNullOrEmpty(this.txtFileds45.Text)) { strErr += "请输入出生医学证明编号！\\n"; }

            #endregion      

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
                this.m_BizC.GetAreaCode = "";
                this.m_BizC.GetAreaName = "";

                /*男方：姓名、出生年月、身份证号、民族、工作单位、户籍地址、现居住地、婚姻状况、联系电话*/
                this.m_BizC.Fileds01 = this.txtPersonNameA.Text;
                this.m_BizC.PersonCidA = this.txtPersonCidA.Text;
                this.m_BizC.Fileds32 = this.m_PerA.PersonBirthday;
                this.m_BizC.Fileds03 = Request["txtNationsA"];
                this.m_BizC.Fileds04 = this.m_PerA.RegisterType;
                this.m_BizC.Fileds05 = this.txtWorkUnitA.Text;
                this.m_BizC.RegAreaCodeA = this.m_PerA.RegisterAreaCode;
                this.m_BizC.RegAreaNameA = this.m_PerA.RegisterAreaName;
                this.m_BizC.CurAreaCodeA = this.m_PerA.CurrentAreaCode;
                this.m_BizC.CurAreaNameA = this.m_PerA.CurrentAreaName;
                this.m_BizC.Fileds33 = this.ddlMarryTypeA.SelectedValue;
                this.m_BizC.ContactTelA = this.m_PerB.PersonTel;
                this.m_BizC.Fileds24 = Fileds24;
                //this.m_BizC.Fileds17 = Fileds17;

                /*女方：姓名、出生年月、身份证号、民族、工作单位、户籍地址、现居住地、婚姻状况、联系电话*/
                this.m_BizC.Fileds08 = this.txtPersonNameB.Text;
                this.m_BizC.PersonCidB = this.txtPersonCidB.Text;
                this.m_BizC.Fileds31 = this.m_PerB.PersonBirthday;
                this.m_BizC.Fileds10 = Request["txtNationsB"];
                this.m_BizC.Fileds11 = this.m_PerB.RegisterType;
                this.m_BizC.Fileds12 = this.txtWorkUnitB.Text;
                this.m_BizC.RegAreaCodeB = this.m_PerB.RegisterAreaCode;
                this.m_BizC.RegAreaNameB = this.m_PerB.RegisterAreaName;
                this.m_BizC.CurAreaCodeB = this.m_PerB.CurrentAreaCode;
                this.m_BizC.CurAreaNameB = this.m_PerB.CurrentAreaName;
                this.m_BizC.Fileds13 = this.ddlMarryTypeB.SelectedValue;
                this.m_BizC.ContactTelB = this.m_PerB.PersonTel;
                this.m_BizC.Fileds23 = Fileds23;

                /*婚姻及孕产信息：结婚时间、怀孕时间、预产日期、出生日期、孩子性别、出生医学证明编号*/
                this.m_BizC.Fileds14 = Fileds14;
                //this.m_BizC.Fileds43 = this.txtFileds43.Value;
                //this.m_BizC.Fileds44 = this.txtFileds44.Value;
                this.m_BizC.Fileds44 = "";
                //this.m_BizC.Fileds18 = this.txtFileds18.Value;
                //this.m_BizC.Fileds17 = this.ddlChildSex.SelectedValue;
                //this.m_BizC.Fileds45 = this.txtFileds45.Text;
                this.m_BizC.Fileds45 = "";
                this.m_BizC.Fileds47 = Fileds47;
                this.m_BizC.Fileds49 = CidTypeA;
                this.m_BizC.Fileds50 = CidTypeB;

                int cm = this.m_BizC.Update();
                this.m_BizC = null;

                string objID = this.m_ObjID;
                if (cm > 0)
                {
                    CommPage.SetBizLog(objID, m_UserID, "业务修改", "管理员用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 进行了《" + this.m_NavTitle + "》修改操作");
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

