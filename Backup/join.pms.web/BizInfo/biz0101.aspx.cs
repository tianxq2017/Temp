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
    public partial class biz0101 : System.Web.UI.Page
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

            //户籍地默认当前选择地区
            //this.UcAreaSelRegA.SetAreaCode(m_AreaCode);
            //this.UcAreaSelRegA.SetAreaName(GetAreaName(m_AreaCode, "1"));
            //this.txtAreaSelRegCodeB.Value = m_AreaCode;
            //this.txtAreaSelRegNameB.Text = BIZ_Common.GetAreaName(m_AreaCode, "1");
            //this.txtAreaSelRegNameB.ReadOnly = true;

            if (!IsPostBack)
            {
                //户籍地默认当前选择地区
                this.UcAreaSelRegA.SetAreaCode(m_AreaCode);
                this.UcAreaSelRegA.SetAreaName(BIZ_Common.GetAreaName(m_AreaCode, "1"));
                this.UcAreaSelRegB.SetAreaCode(m_AreaCode);
                this.UcAreaSelRegB.SetAreaName(BIZ_Common.GetAreaName(m_AreaCode, "1"));
                //取证地点
                this.UcAreaSelXian1.SetAreaCode(m_AreaCode);
                //SetPageStyle(m_UserID);
                SetOpratetionAction(m_NavTitle);
            }
            //GetParam(this.txtHNationsA.Value, this.txtHNationsB.Value);
            GetParam(PageValidate.GetTrim(Request["txtNationsA"]), PageValidate.GetTrim(Request["txtNationsB"]));
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
            m_ActionName = PageValidate.GetTrim(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetTrim(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetTrim(Request.QueryString["k"]);

            m_AreaCode = PageValidate.GetTrim(Request.QueryString["x"]);
            m_AreaName = BIZ_Common.GetAreaName(m_AreaCode, "0");
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
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">管理首页</a> &gt;&gt; 业务办理 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
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

            #region 参数
            string QcfwzBm_f = CommPage.QcfwzBm_fun(PageValidate.GetTrim(this.txtfwzh.Text));
            if (!string.IsNullOrEmpty(QcfwzBm_f)) { strErr += QcfwzBm_f; }
            
            
            /*女方：身份证号、姓名、性别、出生年月、婚姻状况、婚姻时间、民族、联系电话、户籍性质、户籍地址、居住地址、工作单位、是否是独生子女*/
            this.m_PerB.PersonCardID = PageValidate.GetTrim(this.txtPersonCidB.Text);
            this.m_PerB.PersonName = PageValidate.GetTrim(this.txtPersonNameB.Text);
            this.m_PerB.PersonSex = "女";
            this.m_PerB.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerB.PersonCardID);
            this.m_PerB.MarryType = PageValidate.GetTrim(this.ddlMarryTypeB.SelectedValue);
            this.m_PerB.MarryDate = CommBiz.GetTrim(this.txtMarryDateB.Value);
            this.m_PerB.Nations = PageValidate.GetTrim(Request["txtNationsB"]);
            this.m_PerB.PersonTel = PageValidate.GetTrim(this.txtContactTelB.Text);
            this.m_PerB.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeB.Text);
            this.m_PerB.RegisterAreaName = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaName());
            this.m_PerB.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaName());
            CidTypeA = PageValidate.GetTrim(this.ddlCidTypeA.SelectedValue);
            CidTypeB = PageValidate.GetTrim(this.ddlCidTypeB.SelectedValue);
            if (this.m_PerB.RegisterAreaName == "海外")
            {
                this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
                this.m_PerB.RegisterAreaCode = this.m_PerB.CurrentAreaCode;
            }
            else if (this.m_PerB.CurrentAreaName == "海外")
            {
                this.m_PerB.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaCode());
                this.m_PerB.CurrentAreaCode = this.m_PerB.RegisterAreaCode;
            }
            else
            {
                this.m_PerB.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaCode());
                this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
            }

            this.m_PerB.WorkUnit = PageValidate.GetTrim(this.txtWorkUnitB.Text);
            string Fileds16 = "";//PageValidate.GetTrim(this.ddlFileds16.SelectedValue);
            this.m_PerB.BirthNum = PageValidate.GetTrim(this.ddlBirthNum.SelectedValue);

            /*男方：身份证号、姓名、性别、出生年月、婚姻状况、婚姻时间、民族、联系电话、户籍性质、户籍地址、居住地址、工作单位、是否是独生子女*/
            string PersonCardIDA = "";
            if (string.IsNullOrEmpty(this.txtPersonCidA.Text))
            {
                PersonCardIDA = PageValidate.GetTrim(this.txtPersonCidWA.Text);
            }
            else
            {
                PersonCardIDA = PageValidate.GetTrim(this.txtPersonCidA.Text);
            }
            string PersonNameA = PageValidate.GetTrim(this.txtPersonNameA.Text);
            string PersonSexA = "男";
            string PersonBirthdayA = CommBiz.GetBirthdayByID(PersonCardIDA);
            string MarryTypeA = PageValidate.GetTrim(this.ddlMarryTypeA.SelectedValue);
            string MarryDateA = CommBiz.GetTrim(this.txtMarryDateA.Value);
            string NationsA = PageValidate.GetTrim(Request["txtNationsA"]);
            string PersonTelA = PageValidate.GetTrim(this.txtContactTelA.Text);
            string RegisterTypeA = PageValidate.GetTrim(this.ddlRegisterTypeA.Text);
            string RegisterAreaNameA = PageValidate.GetTrim(this.UcAreaSelRegA.GetAreaName());
            string CurrentAreaNameA = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaName());

            string RegisterAreaCodeA = PageValidate.GetTrim(this.UcAreaSelRegA.GetAreaCode());
            string CurrentAreaCodeA = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());

            if (RegisterAreaNameA == "海外")
            {
                RegisterAreaCodeA = CurrentAreaCodeA;
            }
            else if (CurrentAreaNameA == "海外")
            {
                CurrentAreaCodeA = RegisterAreaCodeA;
            }


            string WorkUnitA = PageValidate.GetTrim(this.txtWorkUnitA.Text);
            string Fileds17 = string.Empty;

            //====================================================================    

            //==========================女方信息 start========================================== 
            if (String.IsNullOrEmpty(this.m_PerB.PersonCardID)) { strErr += "请输入女方身份证号！\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerB.PersonCardID) && CidTypeB == "1") { strErr += "女方身份证号有误！！\\n"; }

            if (String.IsNullOrEmpty(this.m_PerB.PersonName)) { strErr += "请输入女方姓名！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryType)) { strErr += "请选择女方婚姻状况！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryDate)) { strErr += "请选择女方婚姻时间！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.Nations)) { strErr += "请输入女方民族！\\n"; }
            //if (String.IsNullOrEmpty(Fileds16)) { strErr += "请选择女方是否是独生子女！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.RegisterType)) { strErr += "请选择女方户籍性质！\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerB.RegisterAreaCode, "0")) { strErr += "请选择女方户籍地！\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerB.CurrentAreaCode, "0")) { strErr += "请选择女方现居住地！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonTel)) { strErr += "请输入女方联系电话！\\n"; }

            //==========================男方信息 start========================================== 
            if (this.m_PerB.MarryType == "初婚" || this.m_PerB.MarryType == "再婚" || this.m_PerB.MarryType == "复婚")
            {
                /*男方：身份证号、姓名、性别、出生年月、婚姻状况、婚姻时间、民族、联系电话、户籍性质、户籍地址、居住地址、工作单位、是否是独生子女*/
                this.m_PerA.PersonCardID = PersonCardIDA;
                this.m_PerA.PersonName = PersonNameA;
                this.m_PerA.PersonSex = PersonSexA;
                this.m_PerA.PersonBirthday = PersonBirthdayA;
                this.m_PerA.MarryType = MarryTypeA;
                this.m_PerA.MarryDate = MarryDateA;
                this.m_PerA.Nations = NationsA;
                this.m_PerA.PersonTel = PersonTelA;
                this.m_PerA.RegisterType = RegisterTypeA;
                this.m_PerA.RegisterAreaCode = RegisterAreaCodeA;
                this.m_PerA.RegisterAreaName = RegisterAreaNameA;
                this.m_PerA.CurrentAreaCode = CurrentAreaCodeA;
                this.m_PerA.CurrentAreaName = CurrentAreaNameA;
                this.m_PerA.WorkUnit = WorkUnitA;
                Fileds17 = ""; //PageValidate.GetTrim(this.ddlFileds17.SelectedValue);

                if (String.IsNullOrEmpty(this.m_PerA.PersonCardID)) { strErr += "请输入男方身份证号！\\n"; }
                if (!ValidIDCard.VerifyIDCard(this.m_PerA.PersonCardID) && CidTypeA == "1") { strErr += "男方身份证号有误！！\\n"; }


                if (String.IsNullOrEmpty(this.m_PerA.PersonName)) { strErr += "请输入男方姓名！\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.MarryType)) { strErr += "请选择男方婚姻状况！\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.MarryDate)) { strErr += "请选择男方婚姻时间！\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.Nations)) { strErr += "请输入男方民族！\\n"; }
                //if (String.IsNullOrEmpty(Fileds17)) { strErr += "请选择男方是否是独生子女！\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.RegisterType)) { strErr += "请选择男方户籍性质！\\n"; }
                if (!CommPage.IsAreaCode(this.m_PerA.RegisterAreaCode, "0")) { strErr += "请选择男方户籍地！\\n"; }
                if (!CommPage.IsAreaCode(this.m_PerA.CurrentAreaCode, "0")) { strErr += "请选择男方现居住地！\\n"; }
                if (this.m_PerA.RegisterAreaCode != m_AreaCode && this.m_PerA.CurrentAreaCode != m_AreaCode && this.m_PerB.RegisterAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "男女双方户籍地或现居住地至少要有一方与所选择地一致！\\n"; }

                ////受理单位指定
                //if (this.m_PerA.CurrentAreaCode == this.m_PerB.CurrentAreaCode)
                //{ areaCodeSL = this.m_PerA.CurrentAreaCode; }
                //else
                //{
                //    if (this.m_PerA.RegisterAreaCode == this.m_PerA.CurrentAreaCode && this.m_PerB.RegisterAreaCode == this.m_PerB.CurrentAreaCode)
                //    {
                //        if (this.m_PerB.RegisterAreaCode == this.m_AreaCode) { areaCodeSL = this.m_PerB.RegisterAreaCode; }
                //        else { areaCodeSL = this.m_PerA.RegisterAreaCode; }
                //    }
                //    else if (this.m_PerB.RegisterAreaCode == this.m_PerB.CurrentAreaCode) { areaCodeSL = this.m_PerB.RegisterAreaCode; }
                //    else if (this.m_PerA.RegisterAreaCode == this.m_PerA.CurrentAreaCode) { areaCodeSL = this.m_PerA.RegisterAreaCode; }
                //    else { areaCodeSL = this.m_AreaCode; }
                //}
                if (this.m_PerB.RegisterAreaCode == m_AreaCode || this.m_PerB.CurrentAreaCode == m_AreaCode) { app = "B"; }
                else { app = "A"; }
            }
            else
            {
                this.m_BizStep = "2";
                app = "B";
                if (this.m_PerB.RegisterAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "女双方户籍地或现居住地至少要有一处与所选择地一致！\\n"; }

                ////受理单位指定
                //areaCodeSL = this.m_PerB.CurrentAreaCode;
            }
            //子女信息
            int iBirthNum = int.Parse(this.m_PerB.BirthNum);
            #region 子女信息
            string ChildName1 = PageValidate.GetTrim(this.txtChildName1.Text);
            string ChildSex1 = PageValidate.GetTrim(this.ddlChildSex1.SelectedValue);
            string ChildBirthday1 = CommBiz.GetTrim(this.txtChildBirthday1.Value);
            string ChildCardID1 = PageValidate.GetTrim(this.txtChildCardID1.Text);
            string ChildNo1 = "1";

            string ChildName2 = PageValidate.GetTrim(this.txtChildName2.Text);
            string ChildSex2 = PageValidate.GetTrim(this.ddlChildSex2.SelectedValue);
            string ChildBirthday2 = CommBiz.GetTrim(this.txtChildBirthday2.Value);
            string ChildCardID2 = PageValidate.GetTrim(this.txtChildCardID2.Text);
            string ChildNo2 = "2";

            string ChildName3 = PageValidate.GetTrim(this.txtChildName3.Text);
            string ChildSex3 = PageValidate.GetTrim(this.ddlChildSex3.SelectedValue);
            string ChildBirthday3 = CommBiz.GetTrim(this.txtChildBirthday3.Value);
            string ChildCardID3 = PageValidate.GetTrim(this.txtChildCardID3.Text);
            string ChildNo3 = "3";

            if (iBirthNum == 0)
            {
                this.m_PerB.BirthNum = "0";
                ChildName1 = ""; ChildName2 = ""; ChildName3 = "";
            }
            else if (iBirthNum == 1)
            {
                ChildName2 = ""; ChildName3 = "";
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "请输入子女1姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "请选择子女1性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "请选择子女1出生年月！\\n"; }
            }
            else if (iBirthNum == 2)
            {
                ChildName3 = "";

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
            this.m_Children.ChildName1 = ChildName1;
            this.m_Children.ChildSex1 = ChildSex1;
            this.m_Children.ChildBirthday1 = ChildBirthday1;
            this.m_Children.ChildCardID1 = ChildCardID1;
            this.m_Children.ChildNo1 = ChildNo1;

            this.m_Children.ChildName2 = ChildName2;
            this.m_Children.ChildSex2 = ChildSex2;
            this.m_Children.ChildBirthday2 = ChildBirthday2;
            this.m_Children.ChildCardID2 = ChildCardID2;
            this.m_Children.ChildNo2 = ChildNo2;

            this.m_Children.ChildName3 = ChildName3;
            this.m_Children.ChildSex3 = ChildSex3;
            this.m_Children.ChildBirthday3 = ChildBirthday3;
            this.m_Children.ChildCardID3 = ChildCardID3;
            this.m_Children.ChildNo3 = ChildNo3;
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

            //检查是否重复提交
            string msg = string.Empty;
            if (CommPage.IsHasBiz(this.m_FuncCode, this.m_PerA.PersonCardID, this.m_PerB.PersonCardID, ref msg)) { strErr += msg; }

            if (cbOk.Checked == false)
            {
                strErr += "请确认承诺！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            try
            {
                #region insert
                this.m_BizC.BizCode = this.m_FuncCode;
                this.m_BizC.BizName = this.m_NavTitle;
                this.m_BizC.BizStep = this.m_BizStep;
                this.m_BizC.CurrentStep = "1";
                this.m_BizC.AdminUserID = this.m_UserID;
                this.m_BizC.AddressID = "0";
                this.m_BizC.Initiator = this.m_UserName;
                this.m_BizC.InitDirection = "1";
                this.m_BizC.SelAreaCode = this.m_AreaCode;
                this.m_BizC.SelAreaName = this.m_AreaName;
                this.m_BizC.StartDate = DateTime.Now.ToString();
                this.m_BizC.Attribs = "0";
                //取证地点
                this.m_BizC.GetAreaCode = GetAreaCode;
                this.m_BizC.GetAreaName = GetAreaName;
                this.m_BizC.QcfwzBm = PageValidate.GetTrim(this.txtfwzh.Text);

                //this.m_BizC.PersonPhotos = PersonPhotos[1];//2寸红底免冠夫妇合影照片

                /*男方：身份证号、姓名、性别、出生年月、婚姻状况、婚姻时间、民族、联系电话、户籍性质、户籍地址、居住地址、工作单位、是否是独生子女*/
                this.m_BizC.PersonCidA = this.m_PerA.PersonCardID;
                this.m_BizC.Fileds01 = this.m_PerA.PersonName;
                this.m_BizC.Fileds02 = this.m_PerA.PersonSex;
                this.m_BizC.Fileds32 = this.m_PerA.PersonBirthday;
                this.m_BizC.Fileds33 = this.m_PerA.MarryType;
                this.m_BizC.Fileds34 = this.m_PerA.MarryDate;
                this.m_BizC.Fileds03 = this.m_PerA.Nations;
                this.m_BizC.ContactTelA = this.m_PerA.PersonTel;
                this.m_BizC.Fileds04 = this.m_PerA.RegisterType;
                this.m_BizC.RegAreaCodeA = this.m_PerA.RegisterAreaCode;
                this.m_BizC.RegAreaNameA = this.m_PerA.RegisterAreaName;
                this.m_BizC.CurAreaCodeA = this.m_PerA.CurrentAreaCode;
                this.m_BizC.CurAreaNameA = this.m_PerA.CurrentAreaName;
                this.m_BizC.Fileds05 = this.m_PerA.WorkUnit;
                this.m_BizC.Fileds17 = Fileds17;

                /*女方：身份证号、姓名、性别、出生年月、婚姻状况、婚姻时间、民族、联系电话、户籍性质、户籍地址、居住地址、工作单位、是否是独生子女*/
                this.m_BizC.PersonCidB = this.m_PerB.PersonCardID;
                this.m_BizC.Fileds08 = this.m_PerB.PersonName;
                this.m_BizC.Fileds09 = this.m_PerB.PersonSex;
                this.m_BizC.Fileds31 = this.m_PerB.PersonBirthday;
                this.m_BizC.Fileds13 = this.m_PerB.MarryType;
                this.m_BizC.Fileds14 = this.m_PerB.MarryDate;
                this.m_BizC.Fileds10 = this.m_PerB.Nations;
                this.m_BizC.ContactTelB = this.m_PerB.PersonTel;
                this.m_BizC.Fileds11 = this.m_PerB.RegisterType;
                this.m_BizC.RegAreaCodeB = this.m_PerB.RegisterAreaCode;
                this.m_BizC.RegAreaNameB = this.m_PerB.RegisterAreaName;
                this.m_BizC.CurAreaCodeB = this.m_PerB.CurrentAreaCode;
                this.m_BizC.CurAreaNameB = this.m_PerB.CurrentAreaName;
                this.m_BizC.Fileds12 = this.m_PerB.WorkUnit;
                this.m_BizC.Fileds16 = Fileds16;
                //子女数
                this.m_BizC.Fileds07 = this.m_PerB.BirthNum;

                //申请理由 申请日期
                this.m_BizC.Fileds18 = Fileds18;
                this.m_BizC.Fileds19 = DateTime.Now.ToString();


                this.m_BizC.Fileds49 = CidTypeA;
                this.m_BizC.Fileds50 = CidTypeB;

                string objID = m_BizC.Insert();
                m_BizC = null;
                if (!string.IsNullOrEmpty(objID) && PageValidate.IsNumber(objID))
                {
                    CommPage.SetBizLog(objID, m_UserID, "业务发起", "管理员用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 进行了《" + this.m_NavTitle + "》发起操作");
                    #region 一孩双方旗外处理\一方旗外处理
                    string perAAreaCode = string.Empty;
                    string perBAreaCode = string.Empty;
                    if (this.m_PerA.RegisterAreaCode.Substring(0, 4) != "1505" && this.m_PerB.RegisterAreaCode.Substring(0, 4) != "1505")
                    {
                        perAAreaCode = this.m_PerA.CurrentAreaCode;
                        perBAreaCode = this.m_PerB.CurrentAreaCode;
                    }
                    else if (this.m_PerA.RegisterAreaCode.Substring(0, 4) != "1505")
                    {
                        perAAreaCode = this.m_PerB.RegisterAreaCode;
                        perBAreaCode = this.m_PerB.RegisterAreaCode;
                    }
                    else if (this.m_PerB.RegisterAreaCode.Substring(0, 4) != "1505")
                    {
                        perAAreaCode = this.m_PerA.RegisterAreaCode;
                        perBAreaCode = this.m_PerA.RegisterAreaCode;
                    }
                    else
                    {
                        perAAreaCode = this.m_PerA.RegisterAreaCode;
                        perBAreaCode = this.m_PerB.RegisterAreaCode;
                    }

                    #endregion
                    if (this.m_BizStep != BIZ_Common.InsetBizWorkFlows(this.m_FuncCode, this.m_BizStep, objID, perAAreaCode, perBAreaCode, this.m_AreaCode))
                    {
                        MessageBox.Show(this, "插入流程表失败，请联系系统管理员！");
                        return;
                    }
                    /*以下：1.判断群众基础表中是否存在，不存在插入，存在更新  --判断依据：身份证号
                            2.判断婚姻表中是否存在该用户信息，不存在插入，存在不跟新  --判断依据：群众编号
                            3.判断子女表中是否存在该用户现有家庭子女信息，不存在插入，存在不更新  --判断依据：群众编号
                     */
                    #region 具体操作
                    //男方
                    this.m_PerA.MateName = this.m_PerB.PersonName;
                    this.m_PerA.MateCardID = this.m_PerB.PersonCardID;
                    string PersonIDA = this.m_PerA.ExecBizPersons();
                    //女方
                    this.m_PerB.MateName = this.m_PerA.PersonName;
                    this.m_PerB.MateCardID = this.m_PerA.PersonCardID;
                    string PersonIDB = this.m_PerB.ExecBizPersons();
                    //发送短讯--start--
                    // 某某,您提交的[业务名称]申请已受理。
                    string uTel, uName, tMsg = string.Empty;
                    if (!string.IsNullOrEmpty(this.m_PerB.PersonTel)) { uTel = this.m_PerB.PersonTel; uName = this.m_PerB.PersonName; } else { uTel = this.m_PerA.PersonTel; uName = this.m_PerA.PersonName; }
                    tMsg = uName + "，您提交的[" + this.m_NavTitle + "]申请已受理。"; //后台发起
                    if (!string.IsNullOrEmpty(uTel) && !string.IsNullOrEmpty(tMsg))
                    {
                        SendMsg.SendMsgByModem(uTel, tMsg);
                        string m_UserAcc = DbHelperSQL.GetSingle("SELECT TOP 1 UserAreaCode FROM USER_BaseInfo WHERE UserID=" + m_UserID).ToString();//判断当前登陆账号自己审核的不用手机提醒

                        if (!string.IsNullOrEmpty(this.m_PerA.RegisterAreaCode) && this.m_PerA.RegisterAreaCode.Substring(9) != "000" && this.m_PerA.RegisterAreaCode != m_UserAcc)
                        {
                            BIZ_Common.SetMsgToAuditer(this.m_NavTitle, this.m_PerA.PersonName, this.m_PerA.RegisterAreaCode);//男方村专干
                        }
                        if (!string.IsNullOrEmpty(this.m_PerB.RegisterAreaCode) && this.m_PerB.RegisterAreaCode.Substring(9) != "000" && this.m_PerB.RegisterAreaCode != m_UserAcc)
                        {
                            BIZ_Common.SetMsgToAuditer(this.m_NavTitle, this.m_PerB.PersonName, this.m_PerB.RegisterAreaCode);//女方村专干
                        }
                    }
                    //发送短讯--end--
                    m_PerA = null;
                    m_PerB = null;

                    //插入子女信息
                    //string personID = string.Empty;
                    //if (app == "A") { personID = PersonIDA; }
                    //else { personID = PersonIDB; }
                    this.m_Children.Inser(PersonIDB, objID, iBirthNum.ToString());
                    #endregion

                    //if (!String.IsNullOrEmpty(PersonPhotos[1])) { BIZ_Common.InsetBizDocs(objID, PersonIDB, "2寸红底免冠夫妇合影照片", PersonPhotos, "1"); }

                    MessageBox.ShowAndRedirect(this.Page, "操作提示：<" + this.m_NavTitle + ">基础信息提交成功，请继续提交证件完成申请！", "BizDocs.aspx?action=add&k=" + objID + "&PersonIDA=" + PersonIDB + "&sourceUrl=" + m_SourceUrl, true, true);
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

