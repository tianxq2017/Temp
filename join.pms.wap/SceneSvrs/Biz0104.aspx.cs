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
    public partial class Biz0104 : UNV.Comm.Web.PageBase
    {
        #region
        private BIZ_Persons m_PerA;//储存申请人信息
        private BIZ_Persons m_PerB;//储存配偶信息
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

            this.txtAreaSelRegCodeA.Value = m_AreaCode;
            this.txtAreaSelRegNameA.Text = BIZ_Common.GetAreaName(m_AreaCode, "1");
            this.txtAreaSelRegNameA.ReadOnly = true;

            if (!IsPostBack)
            {
                GetBizPersonsInfo();    //取证地点
                this.UcAreaSelXian1.SetAreaCode(m_AreaCode);

            }
        }

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
                this.m_BizCode = "0104";
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

                this.txtPersonCidA.Text = bizPer.PersonCardID;
                this.txtPersonNameA.Text = bizPer.PersonName;
                this.ddlMarryTypeA.SelectedValue = bizPer.MarryType;
                this.txtMarryDateA.Value = bizPer.MarryDate;
                this.ddlRegisterTypeA.SelectedValue = bizPer.RegisterType;
                this.txtContactTelA.Text = bizPer.PersonTel;

                //配偶信息
                this.txtPersonCidB.Text = bizPer.MateCardID;
                this.txtPersonNameB.Text = bizPer.MateName;
                this.txtMarryDateB.Value = bizPer.MarryDate;

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

            /*申请人：身份证号、姓名、性别、出生年月、婚姻状况、婚姻时间、联系电话、户籍性质、户籍地址*/
            this.m_PerA.PersonCardID = PageValidate.GetTrim(this.txtPersonCidA.Text);
            this.m_PerA.PersonName = PageValidate.GetTrim(this.txtPersonNameA.Text);
            this.m_PerA.PersonSex = CommBiz.GetSexByID(this.m_PerA.PersonCardID);
            this.m_PerA.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerA.PersonCardID);
            this.m_PerA.MarryType = PageValidate.GetTrim(this.ddlMarryTypeA.SelectedValue);
            this.m_PerA.MarryDate = CommBiz.GetTrim(this.txtMarryDateA.Value);
            this.m_PerA.PersonTel = PageValidate.GetTrim(this.txtContactTelA.Text);
            this.m_PerA.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeA.Text);
            this.m_PerA.RegisterAreaCode = PageValidate.GetTrim(this.txtAreaSelRegCodeA.Value);
            this.m_PerA.RegisterAreaName = PageValidate.GetTrim(this.txtAreaSelRegNameA.Text);

            //string CurrentAreaCodeA = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
            //string CurrentAreaNameA = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaName());
            //string WorkUnitA = PageValidate.GetTrim(this.txtWorkUnitA.Text);

            /*配偶：身份证号、姓名、性别、出生年月、婚姻状况、婚姻时间、联系电话、户籍性质*/
            this.m_PerB.PersonCardID = PageValidate.GetTrim(this.txtPersonCidB.Text);
            this.m_PerB.PersonName = PageValidate.GetTrim(this.txtPersonNameB.Text);
            this.m_PerB.PersonSex = CommBiz.GetSexByID(this.m_PerB.PersonCardID);
            this.m_PerB.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerB.PersonCardID);
            this.m_PerB.MarryType = PageValidate.GetTrim(this.ddlMarryTypeB.SelectedValue);
            this.m_PerB.MarryDate = CommBiz.GetTrim(this.txtMarryDateB.Value);
            this.m_PerB.PersonTel = PageValidate.GetTrim(this.txtContactTelB.Text);
            this.m_PerB.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeB.Text);
            //this.m_PerB.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaCode());
            //this.m_PerB.RegisterAreaName = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaName());
            //this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
            //this.m_PerB.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaName());
            //this.m_PerB.WorkUnit = PageValidate.GetTrim(this.txtWorkUnitB.Text);

            /*子女数*/
            string Fileds20 = PageValidate.GetTrim(this.ddlFileds20.SelectedValue);
            string Fileds21 = PageValidate.GetTrim(this.ddlFileds21.SelectedValue);
            this.m_PerA.BirthNum = (int.Parse(Fileds20) + int.Parse(Fileds21)).ToString();
            string Fileds37 = PageValidate.GetTrim(this.ddlFileds37.SelectedValue);
            string Fileds38 = PageValidate.GetTrim(this.ddlFileds38.SelectedValue);
            string Fileds22 = PageValidate.GetTrim(this.ddlFileds22.SelectedValue);

            /*伤残信息*/
            string Fileds23 = PageValidate.GetTrim(this.txtFileds23.Text);
            string Fileds24 = PageValidate.GetTrim(this.ddlFileds24.SelectedValue);
            string Fileds26 = PageValidate.GetTrim(this.ddlFileds26.SelectedValue);


            //====================================================================    
            //==========================申请人信息 start========================================== 
            if (String.IsNullOrEmpty(this.m_PerA.PersonCardID)) { strErr += "请输入申请人身份证号！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.PersonName)) { strErr += "请输入申请人姓名！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.MarryType)) { strErr += "请选择申请人婚姻状况！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.MarryDate)) { strErr += "请选择申请人婚姻时间！\\n"; }
            //if (String.IsNullOrEmpty(this.m_PerA.Nations)) { strErr += "请输入申请人民族！\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerA.RegisterAreaCode, "0")) { strErr += "请选择申请人户籍地！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.RegisterType)) { strErr += "请选择申请人户籍性质！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.PersonTel)) { strErr += "请输入申请人联系电话！\\n"; }

             if (this.m_PerA.MarryType != "未婚" && this.m_PerA.MarryType != "离婚" && this.m_PerA.MarryType != "丧偶")
            {
            //==========================配偶信息 start========================================== 
            if (String.IsNullOrEmpty(this.m_PerB.PersonCardID)) { strErr += "请输入配偶身份证号！\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerB.PersonCardID)) { strErr += "配偶身份证号有误！！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonName)) { strErr += "请输入配偶姓名！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryType)) { strErr += "请选择配偶婚姻状况！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryDate)) { strErr += "请选择配偶婚姻时间！\\n"; }
            //if (!CommPage.IsAreaCode(this.m_PerB.RegisterAreaCode, "0")) { strErr += "请选择配偶户籍地！\\n"; }
            //if (!CommPage.IsAreaCode(this.m_PerB.CurrentAreaCode, "0")) { strErr += "请选择配偶现居住地！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.RegisterType)) { strErr += "请选择配偶户籍性质！\\n"; }

            if (this.m_PerA.PersonCardID == this.m_PerB.PersonCardID) { strErr += "申请人和配偶身份证号不允许一样！\\n"; }

            if (this.m_PerA.PersonSex == "女")
            {
                if (CommBiz.GetAgeByID(this.m_PerA.PersonCardID) < 49) { strErr += "女方必须年满49周岁才可以申请！"; }
            }
            else if (this.m_PerB.PersonSex == "女")
            {
                if (CommBiz.GetAgeByID(this.m_PerB.PersonCardID) < 49) { strErr += "女方必须年满49周岁才可以申请！"; }
            }
            else { }
        }
        else
        {
            this.m_PerB.PersonCardID = "";
            this.m_PerB.PersonName = "";
            this.m_PerB.PersonSex = "";
            this.m_PerB.PersonBirthday = "";
            this.m_PerB.MarryType = "";
            this.m_PerB.MarryDate = "";
            this.m_PerB.PersonTel = "";
            this.m_PerB.RegisterType = "";
            if (this.m_PerA.PersonSex == "女")
            {
                if (CommBiz.GetAgeByID(this.m_PerA.PersonCardID) < 49) { strErr += "女方必须年满49周岁才可以申请！"; }
            }
        }
        int iBirthNumCH = int.Parse(Fileds37) + int.Parse(Fileds38);
        if (iBirthNumCH > 0)
        {
            if (String.IsNullOrEmpty(Fileds23)) { strErr += "请输入残疾证号码！\\n"; }
            if (String.IsNullOrEmpty(Fileds24)) { strErr += "请选择残疾类型！\\n"; }
            if (String.IsNullOrEmpty(Fileds26)) { strErr += "请选择残疾等级！\\n"; }
        }
        else { Fileds23 = ""; Fileds24 = ""; Fileds26 = ""; }

            //子女信息
            int iBirthNum = int.Parse(this.m_PerA.BirthNum);
            #region 子女信息
            string ChildName1 = PageValidate.GetTrim(this.txtChildName1.Text);
            string ChildSex1 = PageValidate.GetTrim(this.ddlChildSex1.SelectedValue);
            string ChildBirthday1 = CommBiz.GetTrim(this.txtChildBirthday1.Value);
            string ChildSource1 = PageValidate.GetTrim(this.ddlChildSource1.SelectedValue);
            string ChildSurvivalStatus1 = PageValidate.GetTrim(this.ddlChildSurvivalStatus1.SelectedValue);
            string ChildDeathday1 = PageValidate.GetTrim(this.txtChildDeathday1.Value);
            string ChildDeathAudit1 = PageValidate.GetTrim(this.ddlChildDeathAudit1.SelectedValue);
            string ChildNo1 = "1";

            string ChildName2 = PageValidate.GetTrim(this.txtChildName2.Text);
            string ChildSex2 = PageValidate.GetTrim(this.ddlChildSex2.SelectedValue);
            string ChildBirthday2 = CommBiz.GetTrim(this.txtChildBirthday2.Value);
            string ChildSource2 = PageValidate.GetTrim(this.ddlChildSource2.SelectedValue);
            string ChildSurvivalStatus2 = PageValidate.GetTrim(this.ddlChildSurvivalStatus2.SelectedValue);
            string ChildDeathday2 = PageValidate.GetTrim(this.txtChildDeathday2.Value);
            string ChildDeathAudit2 = PageValidate.GetTrim(this.ddlChildDeathAudit2.SelectedValue);
            string ChildNo2 = "2";

            string ChildName3 = PageValidate.GetTrim(this.txtChildName3.Text);
            string ChildSex3 = PageValidate.GetTrim(this.ddlChildSex3.SelectedValue);
            string ChildBirthday3 = CommBiz.GetTrim(this.txtChildBirthday3.Value);
            string ChildSource3 = PageValidate.GetTrim(this.ddlChildSource3.SelectedValue);
            string ChildSurvivalStatus3 = PageValidate.GetTrim(this.ddlChildSurvivalStatus3.SelectedValue);
            string ChildDeathday3 = PageValidate.GetTrim(this.txtChildDeathday3.Value);
            string ChildDeathAudit3 = PageValidate.GetTrim(this.ddlChildDeathAudit3.SelectedValue);
            string ChildNo3 = "3";

            string ChildName4 = PageValidate.GetTrim(this.txtChildName4.Text);
            string ChildSex4 = PageValidate.GetTrim(this.ddlChildSex4.SelectedValue);
            string ChildBirthday4 = CommBiz.GetTrim(this.txtChildBirthday4.Value);
            string ChildSource4 = PageValidate.GetTrim(this.ddlChildSource4.SelectedValue);
            string ChildSurvivalStatus4 = PageValidate.GetTrim(this.ddlChildSurvivalStatus4.SelectedValue);
            string ChildDeathday4 = PageValidate.GetTrim(this.txtChildDeathday4.Value);
            string ChildDeathAudit4 = PageValidate.GetTrim(this.ddlChildDeathAudit4.SelectedValue);
            string ChildNo4 = "4";

            string ChildName5 = PageValidate.GetTrim(this.txtChildName5.Text);
            string ChildSex5 = PageValidate.GetTrim(this.ddlChildSex5.SelectedValue);
            string ChildBirthday5 = CommBiz.GetTrim(this.txtChildBirthday5.Value);
            string ChildSource5 = PageValidate.GetTrim(this.ddlChildSource5.SelectedValue);
            string ChildSurvivalStatus5 = PageValidate.GetTrim(this.ddlChildSurvivalStatus5.SelectedValue);
            string ChildDeathday5 = PageValidate.GetTrim(this.txtChildDeathday5.Value);
            string ChildDeathAudit5 = PageValidate.GetTrim(this.ddlChildDeathAudit5.SelectedValue);
            string ChildNo5 = "5";

            string ChildName6 = PageValidate.GetTrim(this.txtChildName6.Text);
            string ChildSex6 = PageValidate.GetTrim(this.ddlChildSex6.SelectedValue);
            string ChildBirthday6 = CommBiz.GetTrim(this.txtChildBirthday6.Value);
            string ChildSource6 = PageValidate.GetTrim(this.ddlChildSource6.SelectedValue);
            string ChildSurvivalStatus6 = PageValidate.GetTrim(this.ddlChildSurvivalStatus6.SelectedValue);
            string ChildDeathday6 = PageValidate.GetTrim(this.txtChildDeathday6.Value);
            string ChildDeathAudit6 = PageValidate.GetTrim(this.ddlChildDeathAudit6.SelectedValue);
            string ChildNo6 = "6";

            if (iBirthNum == 0)
            {
                this.m_PerB.BirthNum = "0";
                ChildName1 = ""; ChildName2 = ""; ChildName3 = ""; ChildName4 = ""; ChildName5 = ""; ChildName6 = "";
            }
            else if (iBirthNum == 1)
            {
                ChildName2 = ""; ChildName3 = ""; ChildName4 = ""; ChildName5 = ""; ChildName6 = "";
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "请输入子女1姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "请选择子女1性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "请选择子女1出生年月！\\n"; }
                if (ChildDeathAudit1 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday1)) { strErr += "请选择子女1死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday1)) { if (String.IsNullOrEmpty(ChildDeathAudit1)) { strErr += "请选择子女1死/残确认单位！\\n"; } }
                else { ChildDeathAudit1 = ""; }
            }
            else if (iBirthNum == 2)
            {
                ChildName3 = ""; ChildName4 = ""; ChildName5 = ""; ChildName6 = "";

                if (String.IsNullOrEmpty(ChildName1)) { strErr += "请输入子女1姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "请选择子女1性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "请选择子女1出生年月！\\n"; }
                if (ChildDeathAudit1 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday1)) { strErr += "请选择子女1死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday1)) { if (String.IsNullOrEmpty(ChildDeathAudit1)) { strErr += "请选择子女1死/残确认单位！\\n"; } }
                else { ChildDeathAudit1 = ""; }


                if (String.IsNullOrEmpty(ChildName2)) { strErr += "请输入子女2姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "请选择子女2性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "请选择子女2出生年月！\\n"; }
                if (ChildDeathAudit2 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday2)) { strErr += "请选择子女2死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday2)) { if (String.IsNullOrEmpty(ChildDeathAudit2)) { strErr += "请选择子女2死/残确认单位！\\n"; } }

                if (ChildName1 == ChildName2) { strErr += "子女姓名重复！\\n"; }
            }
            else if (iBirthNum == 3)
            {
                ChildName4 = ""; ChildName5 = ""; ChildName6 = "";

                if (String.IsNullOrEmpty(ChildName1)) { strErr += "请输入子女1姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "请选择子女1性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "请选择子女1出生年月！\\n"; }
                if (ChildDeathAudit1 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday1)) { strErr += "请选择子女1死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday1)) { if (String.IsNullOrEmpty(ChildDeathAudit1)) { strErr += "请选择子女1死/残确认单位！\\n"; } }
                else { ChildDeathAudit1 = ""; }


                if (String.IsNullOrEmpty(ChildName2)) { strErr += "请输入子女2姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "请选择子女2性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "请选择子女2出生年月！\\n"; }
                if (ChildDeathAudit2 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday2)) { strErr += "请选择子女2死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday2)) { if (String.IsNullOrEmpty(ChildDeathAudit2)) { strErr += "请选择子女2死/残确认单位！\\n"; } }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "请输入子女3姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "请选择子女3性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "请选择子女3出生年月！\\n"; }

                if (ChildName1 == ChildName2 || ChildName1 == ChildName3 || ChildName2 == ChildName3) { strErr += "子女姓名重复！\\n"; }
            }
            else if (iBirthNum == 4)
            {
                ChildName5 = ""; ChildName6 = "";

                if (String.IsNullOrEmpty(ChildName1)) { strErr += "请输入子女1姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "请选择子女1性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "请选择子女1出生年月！\\n"; }
                if (ChildDeathAudit1 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday1)) { strErr += "请选择子女1死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday1)) { if (String.IsNullOrEmpty(ChildDeathAudit1)) { strErr += "请选择子女1死/残确认单位！\\n"; } }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "请输入子女2姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "请选择子女2性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "请选择子女2出生年月！\\n"; }
                if (ChildDeathAudit2 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday2)) { strErr += "请选择子女2死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday2)) { if (String.IsNullOrEmpty(ChildDeathAudit2)) { strErr += "请选择子女2死/残确认单位！\\n"; } }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "请输入子女3姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "请选择子女3性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "请选择子女3出生年月！\\n"; }
                if (ChildDeathAudit3 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday3)) { strErr += "请选择子女3死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday3)) { if (String.IsNullOrEmpty(ChildDeathAudit3)) { strErr += "请选择子女3死/残确认单位！\\n"; } }

                if (String.IsNullOrEmpty(ChildName4)) { strErr += "请输入子女4姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex4)) { strErr += "请选择子女4性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday4)) { strErr += "请选择子女4出生年月！\\n"; }

                if (ChildName1 == ChildName2 || ChildName1 == ChildName3 || ChildName1 == ChildName4 || ChildName2 == ChildName3 || ChildName2 == ChildName4 || ChildName3 == ChildName4) { strErr += "子女姓名重复！\\n"; }
            }
            else if (iBirthNum == 5)
            {
                ChildName6 = "";

                if (String.IsNullOrEmpty(ChildName1)) { strErr += "请输入子女1姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "请选择子女1性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "请选择子女1出生年月！\\n"; }
                if (ChildDeathAudit1 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday1)) { strErr += "请选择子女1死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday1)) { if (String.IsNullOrEmpty(ChildDeathAudit1)) { strErr += "请选择子女1死/残确认单位！\\n"; } }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "请输入子女2姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "请选择子女2性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "请选择子女2出生年月！\\n"; }
                if (ChildDeathAudit2 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday2)) { strErr += "请选择子女2死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday2)) { if (String.IsNullOrEmpty(ChildDeathAudit2)) { strErr += "请选择子女2死/残确认单位！\\n"; } }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "请输入子女3姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "请选择子女3性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "请选择子女3出生年月！\\n"; }
                if (ChildDeathAudit3 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday3)) { strErr += "请选择子女3死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday3)) { if (String.IsNullOrEmpty(ChildDeathAudit3)) { strErr += "请选择子女3死/残确认单位！\\n"; } }

                if (String.IsNullOrEmpty(ChildName4)) { strErr += "请输入子女4姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex4)) { strErr += "请选择子女4性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday4)) { strErr += "请选择子女4出生年月！\\n"; }
                if (ChildDeathAudit4 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday4)) { strErr += "请选择子女4死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday4)) { if (String.IsNullOrEmpty(ChildDeathAudit4)) { strErr += "请选择子女4死/残确认单位！\\n"; } }

                if (String.IsNullOrEmpty(ChildName5)) { strErr += "请输入子女5姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex5)) { strErr += "请选择子女5性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday5)) { strErr += "请选择子女5出生年月！\\n"; }
                if (ChildDeathAudit5 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday5)) { strErr += "请选择子女5死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday5)) { if (String.IsNullOrEmpty(ChildDeathAudit5)) { strErr += "请选择子女5死/残确认单位！\\n"; } }

                if (ChildName1 == ChildName2 || ChildName1 == ChildName3 || ChildName1 == ChildName4 || ChildName1 == ChildName5 || ChildName2 == ChildName3 || ChildName2 == ChildName4 || ChildName2 == ChildName5 || ChildName3 == ChildName4 || ChildName3 == ChildName5 || ChildName4 == ChildName5) { strErr += "子女姓名重复！\\n"; }
            }
            else if (iBirthNum == 6)
            {
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "请输入子女1姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "请选择子女1性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "请选择子女1出生年月！\\n"; }
                if (ChildDeathAudit1 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday1)) { strErr += "请选择子女1死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday1)) { if (String.IsNullOrEmpty(ChildDeathAudit1)) { strErr += "请选择子女1死/残确认单位！\\n"; } }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "请输入子女2姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "请选择子女2性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "请选择子女2出生年月！\\n"; }
                if (ChildDeathAudit2 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday2)) { strErr += "请选择子女2死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday2)) { if (String.IsNullOrEmpty(ChildDeathAudit2)) { strErr += "请选择子女2死/残确认单位！\\n"; } }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "请输入子女3姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "请选择子女3性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "请选择子女3出生年月！\\n"; }
                if (ChildDeathAudit3 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday3)) { strErr += "请选择子女3死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday3)) { if (String.IsNullOrEmpty(ChildDeathAudit3)) { strErr += "请选择子女3死/残确认单位！\\n"; } }

                if (String.IsNullOrEmpty(ChildName4)) { strErr += "请输入子女4姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex4)) { strErr += "请选择子女4性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday4)) { strErr += "请选择子女4出生年月！\\n"; }
                if (ChildDeathAudit4 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday4)) { strErr += "请选择子女4死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday4)) { if (String.IsNullOrEmpty(ChildDeathAudit4)) { strErr += "请选择子女4死/残确认单位！\\n"; } }

                if (String.IsNullOrEmpty(ChildName5)) { strErr += "请输入子女5姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex5)) { strErr += "请选择子女5性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday5)) { strErr += "请选择子女5出生年月！\\n"; }
                if (ChildDeathAudit5 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday5)) { strErr += "请选择子女5死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday5)) { if (String.IsNullOrEmpty(ChildDeathAudit5)) { strErr += "请选择子女5死/残确认单位！\\n"; } }

                if (String.IsNullOrEmpty(ChildName6)) { strErr += "请输入子女6姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex6)) { strErr += "请选择子女6性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday6)) { strErr += "请选择子女6出生年月！\\n"; }
                if (ChildDeathAudit6 == "死亡") { if (String.IsNullOrEmpty(ChildDeathday6)) { strErr += "请选择子女6死亡日期！\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday6)) { if (String.IsNullOrEmpty(ChildDeathAudit6)) { strErr += "请选择子女6死/残确认单位！\\n"; } }

                if (ChildName1 == ChildName2 || ChildName1 == ChildName3 || ChildName1 == ChildName4 || ChildName1 == ChildName5 || ChildName1 == ChildName6 || ChildName2 == ChildName3 || ChildName2 == ChildName4 || ChildName2 == ChildName5 || ChildName2 == ChildName6 || ChildName3 == ChildName4 || ChildName3 == ChildName5 || ChildName3 == ChildName6 || ChildName4 == ChildName5 || ChildName4 == ChildName6 || ChildName5 == ChildName6) { strErr += "子女姓名重复！\\n"; }
            }

            this.m_Children.ChildName1 = ChildName1;
            this.m_Children.ChildSex1 = ChildSex1;
            this.m_Children.ChildBirthday1 = ChildBirthday1;
            this.m_Children.ChildSource1 = ChildSource1;
            this.m_Children.ChildSurvivalStatus1 = ChildSurvivalStatus1;
            this.m_Children.ChildDeathday1 = ChildDeathday1;
            this.m_Children.ChildDeathAudit1 = ChildDeathAudit1;
            this.m_Children.ChildNo1 = ChildNo1;

            this.m_Children.ChildName2 = ChildName2;
            this.m_Children.ChildSex2 = ChildSex2;
            this.m_Children.ChildBirthday2 = ChildBirthday2;
            this.m_Children.ChildSource2 = ChildSource2;
            this.m_Children.ChildSurvivalStatus2 = ChildSurvivalStatus2;
            this.m_Children.ChildDeathday2 = ChildDeathday2;
            this.m_Children.ChildDeathAudit2 = ChildDeathAudit2;
            this.m_Children.ChildNo2 = ChildNo2;

            this.m_Children.ChildName3 = ChildName3;
            this.m_Children.ChildSex3 = ChildSex3;
            this.m_Children.ChildBirthday3 = ChildBirthday3;
            this.m_Children.ChildSource3 = ChildSource3;
            this.m_Children.ChildSurvivalStatus3 = ChildSurvivalStatus3;
            this.m_Children.ChildDeathday3 = ChildDeathday3;
            this.m_Children.ChildDeathAudit3 = ChildDeathAudit3;
            this.m_Children.ChildNo3 = ChildNo3;

            this.m_Children.ChildName4 = ChildName4;
            this.m_Children.ChildSex4 = ChildSex4;
            this.m_Children.ChildBirthday4 = ChildBirthday4;
            this.m_Children.ChildSource4 = ChildSource4;
            this.m_Children.ChildSurvivalStatus4 = ChildSurvivalStatus4;
            this.m_Children.ChildDeathday4 = ChildDeathday4;
            this.m_Children.ChildDeathAudit4 = ChildDeathAudit4;
            this.m_Children.ChildNo4 = ChildNo4;

            this.m_Children.ChildName5 = ChildName5;
            this.m_Children.ChildSex5 = ChildSex5;
            this.m_Children.ChildBirthday5 = ChildBirthday5;
            this.m_Children.ChildSource5 = ChildSource5;
            this.m_Children.ChildSurvivalStatus5 = ChildSurvivalStatus5;
            this.m_Children.ChildDeathday5 = ChildDeathday5;
            this.m_Children.ChildDeathAudit5 = ChildDeathAudit5;
            this.m_Children.ChildNo5 = ChildNo5;

            this.m_Children.ChildName6 = ChildName6;
            this.m_Children.ChildSex6 = ChildSex6;
            this.m_Children.ChildBirthday6 = ChildBirthday6;
            this.m_Children.ChildSource6 = ChildSource6;
            this.m_Children.ChildSurvivalStatus6 = ChildSurvivalStatus6;
            this.m_Children.ChildDeathday6 = ChildDeathday6;
            this.m_Children.ChildDeathAudit6 = ChildDeathAudit6;
            this.m_Children.ChildNo6 = ChildNo6;
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

                /*申请人：身份证号、姓名、性别、出生年月、婚姻状况、婚姻时间、联系电话、户籍性质、户籍地址*/
                this.m_BizC.PersonCidA = this.m_PerA.PersonCardID;
                this.m_BizC.Fileds01 = this.m_PerA.PersonName;
                this.m_BizC.Fileds02 = this.m_PerA.PersonSex;
                this.m_BizC.Fileds32 = this.m_PerA.PersonBirthday;
                this.m_BizC.Fileds33 = this.m_PerA.MarryType;
                this.m_BizC.Fileds34 = this.m_PerA.MarryDate;
                this.m_BizC.ContactTelA = this.m_PerA.PersonTel;
                this.m_BizC.Fileds04 = this.m_PerA.RegisterType;
                this.m_BizC.RegAreaCodeA = this.m_PerA.RegisterAreaCode;
                this.m_BizC.RegAreaNameA = this.m_PerA.RegisterAreaName;
                //this.m_BizC.CurAreaCodeA = this.m_PerA.CurrentAreaCode;
                //this.m_BizC.CurAreaNameA = this.m_PerA.CurrentAreaName;
                //this.m_BizC.Fileds05 = this.m_PerA.WorkUnit; //子女数
                this.m_BizC.Fileds07 = this.m_PerA.BirthNum;

                /*配偶：身份证号、姓名、性别、出生年月、婚姻状况、婚姻时间、联系电话、户籍性质*/
                this.m_BizC.PersonCidB = this.m_PerB.PersonCardID;
                this.m_BizC.Fileds08 = this.m_PerB.PersonName;
                this.m_BizC.Fileds09 = this.m_PerB.PersonSex;
                this.m_BizC.Fileds31 = this.m_PerB.PersonBirthday;
                this.m_BizC.Fileds13 = this.m_PerB.MarryType;
                this.m_BizC.Fileds14 = this.m_PerB.MarryDate;
                this.m_BizC.ContactTelB = this.m_PerB.PersonTel;
                this.m_BizC.Fileds11 = this.m_PerB.RegisterType;
                this.m_BizC.RegAreaCodeB = this.m_PerB.RegisterAreaCode;
                this.m_BizC.RegAreaNameB = this.m_PerB.RegisterAreaName;
                //this.m_BizC.CurAreaCodeB = this.m_PerB.CurrentAreaCode;
                //this.m_BizC.CurAreaNameB = this.m_PerB.CurrentAreaName;
                //this.m_BizC.Fileds12 = this.m_PerB.WorkUnit;

                /*子女数*/
                this.m_BizC.Fileds20 = Fileds20;
                this.m_BizC.Fileds21 = Fileds21;
                this.m_BizC.Fileds37 = Fileds37;
                this.m_BizC.Fileds38 = Fileds38;

                this.m_BizC.Fileds22 = Fileds22;

                /*伤残信息*/
                this.m_BizC.Fileds23 = Fileds23;
                this.m_BizC.Fileds24 = Fileds24;
                this.m_BizC.Fileds26 = Fileds26;

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
                    //========申请人                                   
                    this.m_PerA.MateName = this.m_PerB.PersonName;
                    this.m_PerA.MateCardID = this.m_PerB.PersonCardID;
                    string PersonIDA = this.m_PerA.ExecBizPersons();
                    //========配偶
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

                    //插入子女信息
                    this.m_Children.Inser(PersonIDA, objID, iBirthNum.ToString());
                    #endregion

                    MessageBox.ShowAndRedirect(this.Page, "", "/Svrs-BizDocs/" + m_BizCode + "-" + objID + "." + m_FileExt, false, true);
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
