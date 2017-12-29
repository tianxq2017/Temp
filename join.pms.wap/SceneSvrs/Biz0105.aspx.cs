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

namespace join.pms.wap.SceneSvrs
{
    public partial class Biz0105 : UNV.Comm.Web.PageBase
    {
        #region
        private BIZ_Persons m_PerA;//储存男方信息
        private BIZ_Persons m_PerB;//储存女方信息
        private BIZ_Contents m_BizC;//业务信息
        private BIZ_PersonChildren m_Children;//子女信息

        private string m_UserID;
        private string m_PersonName;

        private string m_AreaCode;
        private string m_AreaName;
        private string m_BizCode;
        private string m_BizName;
        private string m_BizStep;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();
            SetPageHeader("在线申请");

            this.txtAreaSelRegCodeB.Value = m_AreaCode;
            this.txtAreaSelRegNameB.Text = BIZ_Common.GetAreaName(m_AreaCode, "1");
            this.txtAreaSelRegNameB.ReadOnly = true;
            if (!IsPostBack)
            {
                GetBizPersonsInfo();
                //取证地点
                this.UcAreaSelXian1.SetAreaCode(m_AreaCode);
            }
            //GetParam(this.txtHNationsA.Value, this.txtHNationsB.Value);
            GetParam(PageValidate.GetTrim(Request["txtNationsA"]), PageValidate.GetTrim(Request["txtNationsB"]));
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

        #region 设置页头信息及导航\验证参数\验证用户等
        //设置页头信息及导航等
        private void SetPageHeader(string objTitles)
        {
            try
            {
                this.Page.Header.Title = this.m_SiteName;
                base.AddMetaTag("keywords", Server.HtmlEncode(this.m_SiteName + "," + objTitles + "," + this.m_BizName + "," + this.m_SiteKeyWord));
                base.AddMetaTag(this.m_SiteName);
                base.AddMetaTag("description", Server.HtmlEncode(this.m_BizName + "," + m_SiteDescription));
                base.AddMetaTag("copyright", Server.HtmlEncode("本页版权归 西安网是科技发展有限公司 所有。All Rights Reserved"));
            }
            catch
            {
                Server.Transfer("~/errors.aspx");
            }
        }
        /// <summary>
        /// 验证接受的参数
        /// </summary>
        private void ValidateParams()
        {
            m_AreaCode = PageValidate.GetTrim(Request.QueryString["x"]);
            m_AreaName = BIZ_Common.GetAreaName(m_AreaCode, "0");
            if (!string.IsNullOrEmpty(m_AreaName) && !string.IsNullOrEmpty(m_AreaCode) && PageValidate.IsNumber(m_AreaCode))
            {
                this.m_BizCode = "0105";
                Biz_Categories bizCateg = new Biz_Categories();
                bizCateg.SelectSingle(m_BizCode);
                 this.m_BizName = bizCateg.BizNameAB;
                this.m_BizStep = bizCateg.BizStep;
                bizCateg = null;
                this.Uc_PageTop1.GetSysMenu(this.m_BizName);
            }
            else
            {
                Server.Transfer("/errors.aspx");
            }
        }
        /// <summary>
        /// 身份验证
        /// </summary>
        private void AuthenticateUser()
        {
            bool returnVa = false;
            if (Request.Browser.Cookies)
            {
                HttpCookie loginCookie = Request.Cookies["AREWEB_OC_PUBSVRS_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["UserID"] != null && !String.IsNullOrEmpty(Session["UserID"].ToString())) { returnVa = true; m_UserID = Session["UserID"].ToString(); }
            }
            if (!returnVa)
            {
                Response.Write("<script language='javascript'>parent.location.href='/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh." + m_FileExt + "';</script>");
                Response.End();
            }
            else { m_PersonName = BIZ_Common.GetPersonFullName(this.m_UserID); }
        }
        #endregion

        #region 获取当前登录用户信息
        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        private void GetBizPersonsInfo()
        {
            try
            {
                BIZ_Persons bizPer = new BIZ_Persons();
                bizPer.GetPersonsInfo(this.m_UserID);
                string PersonSex = bizPer.PersonSex;

                if (PersonSex == "男")
                {
                    this.txtPersonCidA.Text = bizPer.PersonCardID;
                    this.txtPersonNameA.Text = bizPer.PersonName;
                    this.ddlMarryTypeA.SelectedValue = bizPer.MarryType;
                    this.txtMarryDateA.Value = bizPer.MarryDate;
                    this.txtHNationsA.Value = bizPer.Nations;
                    this.ddlRegisterTypeA.SelectedValue = bizPer.RegisterType;

                    //配偶信息
                    this.txtPersonCidB.Text = bizPer.MateCardID;
                    this.txtPersonNameB.Text = bizPer.MateName;
                    this.txtMarryDateB.Value = bizPer.MarryDate;
                }
                else
                {
                    this.txtPersonCidB.Text = bizPer.PersonCardID;
                    this.txtPersonNameB.Text = bizPer.PersonName;
                    this.ddlMarryTypeB.SelectedValue = bizPer.MarryType;
                    this.txtMarryDateB.Value = bizPer.MarryDate;
                    this.txtHNationsB.Value = bizPer.Nations;
                    this.ddlRegisterTypeB.SelectedValue = bizPer.RegisterType;
                    this.txtContactTelB.Text = bizPer.PersonTel;

                    //配偶信息
                    this.txtPersonCidA.Text = bizPer.MateCardID;
                    this.txtPersonNameA.Text = bizPer.MateName;
                    this.txtMarryDateA.Value = bizPer.MarryDate;
                }
                bizPer = null;
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
            if (!CommPage.IsAreaCode(this.m_PerB.RegisterAreaCode, "0")) { strErr += "请选择女方户籍地！\\n"; }
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

            //检查是否重复提交
            string msg = string.Empty;
            if (CommPage.IsHasBiz(this.m_BizCode, this.m_PerA.PersonCardID, this.m_PerB.PersonCardID, ref msg)) { strErr += msg; }

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
                this.m_BizC.BizCode = this.m_BizCode;
                this.m_BizC.BizName = this.m_BizName;
                this.m_BizC.BizStep = this.m_BizStep;
                this.m_BizC.CurrentStep = "1";
                this.m_BizC.PersonID = this.m_UserID;
                this.m_BizC.AddressID = "0";
                this.m_BizC.Initiator = this.m_PersonName + "by手机";
                this.m_BizC.InitDirection = "0";
                this.m_BizC.SelAreaCode = this.m_AreaCode;
                this.m_BizC.SelAreaName = this.m_AreaName;
                this.m_BizC.StartDate = DateTime.Now.ToString();
                this.m_BizC.Attribs = "0";

                //取证地点
                this.m_BizC.GetAreaCode = GetAreaCode;
                this.m_BizC.GetAreaName = GetAreaName;

                //this.m_BizC.PersonPhotos = PersonPhotos[1];//2寸红底免冠夫妇合影照片

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
                this.m_BizC.RegAreaCodeB = this.m_PerB.RegisterAreaCode;
                this.m_BizC.RegAreaNameB = this.m_PerB.RegisterAreaName;

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
                this.m_BizC.Fileds19 = DateTime.Now.ToString();

                string objID = m_BizC.Insert();
                m_BizC = null;
                if (!string.IsNullOrEmpty(objID) && PageValidate.IsNumber(objID))
                {
                    //业务日志
                    CommPage.SetBizLog(objID, m_UserID, "业务发起", "群众用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 进行了《" + this.m_BizName + "》发起操作");

                    /*以下：1.判断群众基础表中是否存在，不存在插入，存在更新  --判断依据：身份证号
                                     2.判断婚姻表中是否存在该用户信息，不存在插入，存在不跟新  --判断依据：群众编号
                                     3.判断子女表中是否存在该用户现有家庭子女信息，不存在插入，存在不更新  --判断依据：群众编号
                              */
                    if (this.m_BizStep != BIZ_Common.InsetBizWorkFlows(this.m_BizCode, this.m_BizStep, objID, "", "", this.m_AreaCode))
                    {
                        MessageBox.Show(this, "插入流程表失败，请联系系统管理员！");
                        return;
                    }
                    #region 具体操作
                    //========男方                                   
                    this.m_PerA.MateName = this.m_PerB.PersonName;
                    this.m_PerA.MateCardID = this.m_PerB.PersonCardID;
                    string PersonIDA = this.m_PerA.ExecBizPersons();
                    //========女方
                    this.m_PerB.MateName = this.m_PerA.PersonName;
                    this.m_PerB.MateCardID = this.m_PerA.PersonCardID;
                    string PersonIDB = this.m_PerB.ExecBizPersons();
                    //发送短讯--start--
                    string uTel, uName, tMsg = string.Empty;
                    if (!string.IsNullOrEmpty(this.m_PerB.PersonTel)) { uTel = this.m_PerB.PersonTel; uName = this.m_PerB.PersonName; } else { uTel = this.m_PerA.PersonTel; uName = this.m_PerA.PersonName; }
                    tMsg = uName + "，您提交的[" + this.m_BizName + "]申请已受理，请登录平台“用户信息”界面查看办理进度。"; //群众发起
                    if (!string.IsNullOrEmpty(uTel) && !string.IsNullOrEmpty(tMsg))
                    {
                        SendMsg.SendMsgByModem(uTel, tMsg);
                        if (!string.IsNullOrEmpty(this.m_PerA.RegisterAreaCode) && this.m_PerA.RegisterAreaCode.Substring(9) != "000") BIZ_Common.SetMsgToAuditer(this.m_BizName, uName, this.m_PerA.RegisterAreaCode);//男方村专干
                        if (!string.IsNullOrEmpty(this.m_PerB.RegisterAreaCode) && this.m_PerB.RegisterAreaCode.Substring(9) != "000") BIZ_Common.SetMsgToAuditer(this.m_BizName, uName, this.m_PerB.RegisterAreaCode);//女方村专干
                    }
                    //发送短讯--end--
                    m_PerA = null;
                    m_PerB = null;

                    this.m_Children.Inser(PersonIDB, objID, iBirthNum.ToString());
                    #endregion
                    //if (!String.IsNullOrEmpty(PersonPhotos[1])) { BIZ_Common.InsetBizDocs(objID, this.m_UserID, "2寸红底免冠夫妇合影照片", PersonPhotos, "0"); }

                    MessageBox.ShowAndRedirect(this.Page, "", "/Svrs-BizDocs/" + m_BizCode + "-" + objID + "." + m_FileExt, false,true);
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
                return;
            }
        }
        #endregion
    }
}
