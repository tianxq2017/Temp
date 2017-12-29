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
        private BIZ_Persons m_PerA;//������������Ϣ
        private BIZ_Persons m_PerB;//������ż��Ϣ
        private BIZ_Contents m_BizC;//ҵ����Ϣ
        private BIZ_PersonChildren m_Children;//��Ů��Ϣ

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
            SetPageHeader("��������");

            this.txtAreaSelRegCodeA.Value = m_AreaCode;
            this.txtAreaSelRegNameA.Text = BIZ_Common.GetAreaName(m_AreaCode, "1");
            this.txtAreaSelRegNameA.ReadOnly = true;

            if (!IsPostBack)
            {
                GetBizPersonsInfo();    //ȡ֤�ص�
                this.UcAreaSelXian1.SetAreaCode(m_AreaCode);

            }
        }

        #region ����ҳͷ��Ϣ������\��֤����\��֤�û���
        //����ҳͷ��Ϣ��������
        private void SetPageHeader(string objTitles)
        {
            try
            {
                this.Page.Header.Title = this.m_SiteName;
                base.AddMetaTag("keywords", Server.HtmlEncode(this.m_SiteName + "," + objTitles + "," + this.m_BizName + "," + this.m_SiteKeyWord));
                base.AddMetaTag(this.m_SiteName);
                base.AddMetaTag("description", Server.HtmlEncode(this.m_BizName + "," + m_SiteDescription));
                base.AddMetaTag("copyright", Server.HtmlEncode("��ҳ��Ȩ�� �������ǿƼ���չ���޹�˾ ���С�All Rights Reserved"));
            }
            catch
            {
                Server.Transfer("~/errors.aspx");
            }
        }
        /// <summary>
        /// ��֤���ܵĲ���
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
        /// ������֤
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

        #region ��ȡ��ǰ��¼�û���Ϣ
        /// <summary>
        /// ��ȡ��ǰ��¼�û���Ϣ
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

                //��ż��Ϣ
                this.txtPersonCidB.Text = bizPer.MateCardID;
                this.txtPersonNameB.Text = bizPer.MateName;
                this.txtMarryDateB.Value = bizPer.MarryDate;

                bizPer = null;
            }
            catch { }
        }
        #endregion

        #region �ύ������Ϣ
        /// <summary>
        /// �ύ������Ϣ
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

            #region ����

            /*�����ˣ�����֤�š��������Ա𡢳������¡�����״��������ʱ�䡢��ϵ�绰���������ʡ�������ַ*/
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

            /*��ż������֤�š��������Ա𡢳������¡�����״��������ʱ�䡢��ϵ�绰����������*/
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

            /*��Ů��*/
            string Fileds20 = PageValidate.GetTrim(this.ddlFileds20.SelectedValue);
            string Fileds21 = PageValidate.GetTrim(this.ddlFileds21.SelectedValue);
            this.m_PerA.BirthNum = (int.Parse(Fileds20) + int.Parse(Fileds21)).ToString();
            string Fileds37 = PageValidate.GetTrim(this.ddlFileds37.SelectedValue);
            string Fileds38 = PageValidate.GetTrim(this.ddlFileds38.SelectedValue);
            string Fileds22 = PageValidate.GetTrim(this.ddlFileds22.SelectedValue);

            /*�˲���Ϣ*/
            string Fileds23 = PageValidate.GetTrim(this.txtFileds23.Text);
            string Fileds24 = PageValidate.GetTrim(this.ddlFileds24.SelectedValue);
            string Fileds26 = PageValidate.GetTrim(this.ddlFileds26.SelectedValue);


            //====================================================================    
            //==========================��������Ϣ start========================================== 
            if (String.IsNullOrEmpty(this.m_PerA.PersonCardID)) { strErr += "����������������֤�ţ�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.PersonName)) { strErr += "������������������\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.MarryType)) { strErr += "��ѡ�������˻���״����\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.MarryDate)) { strErr += "��ѡ�������˻���ʱ�䣡\\n"; }
            //if (String.IsNullOrEmpty(this.m_PerA.Nations)) { strErr += "���������������壡\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerA.RegisterAreaCode, "0")) { strErr += "��ѡ�������˻����أ�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.RegisterType)) { strErr += "��ѡ�������˻������ʣ�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.PersonTel)) { strErr += "��������������ϵ�绰��\\n"; }

             if (this.m_PerA.MarryType != "δ��" && this.m_PerA.MarryType != "���" && this.m_PerA.MarryType != "ɥż")
            {
            //==========================��ż��Ϣ start========================================== 
            if (String.IsNullOrEmpty(this.m_PerB.PersonCardID)) { strErr += "��������ż����֤�ţ�\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerB.PersonCardID)) { strErr += "��ż����֤�����󣡣�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonName)) { strErr += "��������ż������\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryType)) { strErr += "��ѡ����ż����״����\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryDate)) { strErr += "��ѡ����ż����ʱ�䣡\\n"; }
            //if (!CommPage.IsAreaCode(this.m_PerB.RegisterAreaCode, "0")) { strErr += "��ѡ����ż�����أ�\\n"; }
            //if (!CommPage.IsAreaCode(this.m_PerB.CurrentAreaCode, "0")) { strErr += "��ѡ����ż�־�ס�أ�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.RegisterType)) { strErr += "��ѡ����ż�������ʣ�\\n"; }

            if (this.m_PerA.PersonCardID == this.m_PerB.PersonCardID) { strErr += "�����˺���ż����֤�Ų�����һ����\\n"; }

            if (this.m_PerA.PersonSex == "Ů")
            {
                if (CommBiz.GetAgeByID(this.m_PerA.PersonCardID) < 49) { strErr += "Ů����������49����ſ������룡"; }
            }
            else if (this.m_PerB.PersonSex == "Ů")
            {
                if (CommBiz.GetAgeByID(this.m_PerB.PersonCardID) < 49) { strErr += "Ů����������49����ſ������룡"; }
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
            if (this.m_PerA.PersonSex == "Ů")
            {
                if (CommBiz.GetAgeByID(this.m_PerA.PersonCardID) < 49) { strErr += "Ů����������49����ſ������룡"; }
            }
        }
        int iBirthNumCH = int.Parse(Fileds37) + int.Parse(Fileds38);
        if (iBirthNumCH > 0)
        {
            if (String.IsNullOrEmpty(Fileds23)) { strErr += "������м�֤���룡\\n"; }
            if (String.IsNullOrEmpty(Fileds24)) { strErr += "��ѡ��м����ͣ�\\n"; }
            if (String.IsNullOrEmpty(Fileds26)) { strErr += "��ѡ��м��ȼ���\\n"; }
        }
        else { Fileds23 = ""; Fileds24 = ""; Fileds26 = ""; }

            //��Ů��Ϣ
            int iBirthNum = int.Parse(this.m_PerA.BirthNum);
            #region ��Ů��Ϣ
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
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }
                if (ChildDeathAudit1 == "����") { if (String.IsNullOrEmpty(ChildDeathday1)) { strErr += "��ѡ����Ů1�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday1)) { if (String.IsNullOrEmpty(ChildDeathAudit1)) { strErr += "��ѡ����Ů1��/��ȷ�ϵ�λ��\\n"; } }
                else { ChildDeathAudit1 = ""; }
            }
            else if (iBirthNum == 2)
            {
                ChildName3 = ""; ChildName4 = ""; ChildName5 = ""; ChildName6 = "";

                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }
                if (ChildDeathAudit1 == "����") { if (String.IsNullOrEmpty(ChildDeathday1)) { strErr += "��ѡ����Ů1�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday1)) { if (String.IsNullOrEmpty(ChildDeathAudit1)) { strErr += "��ѡ����Ů1��/��ȷ�ϵ�λ��\\n"; } }
                else { ChildDeathAudit1 = ""; }


                if (String.IsNullOrEmpty(ChildName2)) { strErr += "��������Ů2������\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "��ѡ����Ů2�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "��ѡ����Ů2�������£�\\n"; }
                if (ChildDeathAudit2 == "����") { if (String.IsNullOrEmpty(ChildDeathday2)) { strErr += "��ѡ����Ů2�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday2)) { if (String.IsNullOrEmpty(ChildDeathAudit2)) { strErr += "��ѡ����Ů2��/��ȷ�ϵ�λ��\\n"; } }

                if (ChildName1 == ChildName2) { strErr += "��Ů�����ظ���\\n"; }
            }
            else if (iBirthNum == 3)
            {
                ChildName4 = ""; ChildName5 = ""; ChildName6 = "";

                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }
                if (ChildDeathAudit1 == "����") { if (String.IsNullOrEmpty(ChildDeathday1)) { strErr += "��ѡ����Ů1�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday1)) { if (String.IsNullOrEmpty(ChildDeathAudit1)) { strErr += "��ѡ����Ů1��/��ȷ�ϵ�λ��\\n"; } }
                else { ChildDeathAudit1 = ""; }


                if (String.IsNullOrEmpty(ChildName2)) { strErr += "��������Ů2������\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "��ѡ����Ů2�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "��ѡ����Ů2�������£�\\n"; }
                if (ChildDeathAudit2 == "����") { if (String.IsNullOrEmpty(ChildDeathday2)) { strErr += "��ѡ����Ů2�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday2)) { if (String.IsNullOrEmpty(ChildDeathAudit2)) { strErr += "��ѡ����Ů2��/��ȷ�ϵ�λ��\\n"; } }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "��������Ů3������\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "��ѡ����Ů3�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "��ѡ����Ů3�������£�\\n"; }

                if (ChildName1 == ChildName2 || ChildName1 == ChildName3 || ChildName2 == ChildName3) { strErr += "��Ů�����ظ���\\n"; }
            }
            else if (iBirthNum == 4)
            {
                ChildName5 = ""; ChildName6 = "";

                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }
                if (ChildDeathAudit1 == "����") { if (String.IsNullOrEmpty(ChildDeathday1)) { strErr += "��ѡ����Ů1�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday1)) { if (String.IsNullOrEmpty(ChildDeathAudit1)) { strErr += "��ѡ����Ů1��/��ȷ�ϵ�λ��\\n"; } }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "��������Ů2������\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "��ѡ����Ů2�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "��ѡ����Ů2�������£�\\n"; }
                if (ChildDeathAudit2 == "����") { if (String.IsNullOrEmpty(ChildDeathday2)) { strErr += "��ѡ����Ů2�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday2)) { if (String.IsNullOrEmpty(ChildDeathAudit2)) { strErr += "��ѡ����Ů2��/��ȷ�ϵ�λ��\\n"; } }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "��������Ů3������\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "��ѡ����Ů3�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "��ѡ����Ů3�������£�\\n"; }
                if (ChildDeathAudit3 == "����") { if (String.IsNullOrEmpty(ChildDeathday3)) { strErr += "��ѡ����Ů3�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday3)) { if (String.IsNullOrEmpty(ChildDeathAudit3)) { strErr += "��ѡ����Ů3��/��ȷ�ϵ�λ��\\n"; } }

                if (String.IsNullOrEmpty(ChildName4)) { strErr += "��������Ů4������\\n"; }
                if (String.IsNullOrEmpty(ChildSex4)) { strErr += "��ѡ����Ů4�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday4)) { strErr += "��ѡ����Ů4�������£�\\n"; }

                if (ChildName1 == ChildName2 || ChildName1 == ChildName3 || ChildName1 == ChildName4 || ChildName2 == ChildName3 || ChildName2 == ChildName4 || ChildName3 == ChildName4) { strErr += "��Ů�����ظ���\\n"; }
            }
            else if (iBirthNum == 5)
            {
                ChildName6 = "";

                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }
                if (ChildDeathAudit1 == "����") { if (String.IsNullOrEmpty(ChildDeathday1)) { strErr += "��ѡ����Ů1�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday1)) { if (String.IsNullOrEmpty(ChildDeathAudit1)) { strErr += "��ѡ����Ů1��/��ȷ�ϵ�λ��\\n"; } }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "��������Ů2������\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "��ѡ����Ů2�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "��ѡ����Ů2�������£�\\n"; }
                if (ChildDeathAudit2 == "����") { if (String.IsNullOrEmpty(ChildDeathday2)) { strErr += "��ѡ����Ů2�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday2)) { if (String.IsNullOrEmpty(ChildDeathAudit2)) { strErr += "��ѡ����Ů2��/��ȷ�ϵ�λ��\\n"; } }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "��������Ů3������\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "��ѡ����Ů3�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "��ѡ����Ů3�������£�\\n"; }
                if (ChildDeathAudit3 == "����") { if (String.IsNullOrEmpty(ChildDeathday3)) { strErr += "��ѡ����Ů3�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday3)) { if (String.IsNullOrEmpty(ChildDeathAudit3)) { strErr += "��ѡ����Ů3��/��ȷ�ϵ�λ��\\n"; } }

                if (String.IsNullOrEmpty(ChildName4)) { strErr += "��������Ů4������\\n"; }
                if (String.IsNullOrEmpty(ChildSex4)) { strErr += "��ѡ����Ů4�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday4)) { strErr += "��ѡ����Ů4�������£�\\n"; }
                if (ChildDeathAudit4 == "����") { if (String.IsNullOrEmpty(ChildDeathday4)) { strErr += "��ѡ����Ů4�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday4)) { if (String.IsNullOrEmpty(ChildDeathAudit4)) { strErr += "��ѡ����Ů4��/��ȷ�ϵ�λ��\\n"; } }

                if (String.IsNullOrEmpty(ChildName5)) { strErr += "��������Ů5������\\n"; }
                if (String.IsNullOrEmpty(ChildSex5)) { strErr += "��ѡ����Ů5�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday5)) { strErr += "��ѡ����Ů5�������£�\\n"; }
                if (ChildDeathAudit5 == "����") { if (String.IsNullOrEmpty(ChildDeathday5)) { strErr += "��ѡ����Ů5�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday5)) { if (String.IsNullOrEmpty(ChildDeathAudit5)) { strErr += "��ѡ����Ů5��/��ȷ�ϵ�λ��\\n"; } }

                if (ChildName1 == ChildName2 || ChildName1 == ChildName3 || ChildName1 == ChildName4 || ChildName1 == ChildName5 || ChildName2 == ChildName3 || ChildName2 == ChildName4 || ChildName2 == ChildName5 || ChildName3 == ChildName4 || ChildName3 == ChildName5 || ChildName4 == ChildName5) { strErr += "��Ů�����ظ���\\n"; }
            }
            else if (iBirthNum == 6)
            {
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }
                if (ChildDeathAudit1 == "����") { if (String.IsNullOrEmpty(ChildDeathday1)) { strErr += "��ѡ����Ů1�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday1)) { if (String.IsNullOrEmpty(ChildDeathAudit1)) { strErr += "��ѡ����Ů1��/��ȷ�ϵ�λ��\\n"; } }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "��������Ů2������\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "��ѡ����Ů2�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "��ѡ����Ů2�������£�\\n"; }
                if (ChildDeathAudit2 == "����") { if (String.IsNullOrEmpty(ChildDeathday2)) { strErr += "��ѡ����Ů2�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday2)) { if (String.IsNullOrEmpty(ChildDeathAudit2)) { strErr += "��ѡ����Ů2��/��ȷ�ϵ�λ��\\n"; } }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "��������Ů3������\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "��ѡ����Ů3�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "��ѡ����Ů3�������£�\\n"; }
                if (ChildDeathAudit3 == "����") { if (String.IsNullOrEmpty(ChildDeathday3)) { strErr += "��ѡ����Ů3�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday3)) { if (String.IsNullOrEmpty(ChildDeathAudit3)) { strErr += "��ѡ����Ů3��/��ȷ�ϵ�λ��\\n"; } }

                if (String.IsNullOrEmpty(ChildName4)) { strErr += "��������Ů4������\\n"; }
                if (String.IsNullOrEmpty(ChildSex4)) { strErr += "��ѡ����Ů4�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday4)) { strErr += "��ѡ����Ů4�������£�\\n"; }
                if (ChildDeathAudit4 == "����") { if (String.IsNullOrEmpty(ChildDeathday4)) { strErr += "��ѡ����Ů4�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday4)) { if (String.IsNullOrEmpty(ChildDeathAudit4)) { strErr += "��ѡ����Ů4��/��ȷ�ϵ�λ��\\n"; } }

                if (String.IsNullOrEmpty(ChildName5)) { strErr += "��������Ů5������\\n"; }
                if (String.IsNullOrEmpty(ChildSex5)) { strErr += "��ѡ����Ů5�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday5)) { strErr += "��ѡ����Ů5�������£�\\n"; }
                if (ChildDeathAudit5 == "����") { if (String.IsNullOrEmpty(ChildDeathday5)) { strErr += "��ѡ����Ů5�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday5)) { if (String.IsNullOrEmpty(ChildDeathAudit5)) { strErr += "��ѡ����Ů5��/��ȷ�ϵ�λ��\\n"; } }

                if (String.IsNullOrEmpty(ChildName6)) { strErr += "��������Ů6������\\n"; }
                if (String.IsNullOrEmpty(ChildSex6)) { strErr += "��ѡ����Ů6�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday6)) { strErr += "��ѡ����Ů6�������£�\\n"; }
                if (ChildDeathAudit6 == "����") { if (String.IsNullOrEmpty(ChildDeathday6)) { strErr += "��ѡ����Ů6�������ڣ�\\n"; } }
                if (!String.IsNullOrEmpty(ChildDeathday6)) { if (String.IsNullOrEmpty(ChildDeathAudit6)) { strErr += "��ѡ����Ů6��/��ȷ�ϵ�λ��\\n"; } }

                if (ChildName1 == ChildName2 || ChildName1 == ChildName3 || ChildName1 == ChildName4 || ChildName1 == ChildName5 || ChildName1 == ChildName6 || ChildName2 == ChildName3 || ChildName2 == ChildName4 || ChildName2 == ChildName5 || ChildName2 == ChildName6 || ChildName3 == ChildName4 || ChildName3 == ChildName5 || ChildName3 == ChildName6 || ChildName4 == ChildName5 || ChildName4 == ChildName6 || ChildName5 == ChildName6) { strErr += "��Ů�����ظ���\\n"; }
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

            //�������� ��������
            string Fileds18 = PageValidate.GetTrim(this.txtFileds18.Text);
            //string Fileds19 = CommBiz.GetTrim(this.txtFileds19.Value);
            if (String.IsNullOrEmpty(Fileds18)) { strErr += "�������������ɣ�\\n"; }
            // if (String.IsNullOrEmpty(Fileds19)) { strErr += "��ѡ���������ڣ�\\n"; }
            #endregion

            //ȡ֤�ص�
            string GetAreaCode = PageValidate.GetTrim(this.UcAreaSelXian1.GetAreaCode());
            string GetAreaName = PageValidate.GetTrim(this.UcAreaSelXian1.GetAreaName());

            //����Ƿ��ظ��ύ
            string msg = string.Empty;
            if (CommPage.IsHasBiz(this.m_BizCode, this.m_PerA.PersonCardID, this.m_PerB.PersonCardID, ref msg)) { strErr += msg; }

            if (cbOk.Checked == false)
            {
                strErr += "��ȷ�ϳ�ŵ��\\n";
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
                this.m_BizC.Initiator = this.m_PersonName + "by�ֻ�";
                this.m_BizC.InitDirection = "0";
                this.m_BizC.SelAreaCode = this.m_AreaCode;
                this.m_BizC.SelAreaName = this.m_AreaName;
                this.m_BizC.StartDate = DateTime.Now.ToString();
                this.m_BizC.Attribs = "0";
                //ȡ֤�ص�
                this.m_BizC.GetAreaCode = GetAreaCode;
                this.m_BizC.GetAreaName = GetAreaName;

                /*�����ˣ�����֤�š��������Ա𡢳������¡�����״��������ʱ�䡢��ϵ�绰���������ʡ�������ַ*/
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
                //this.m_BizC.Fileds05 = this.m_PerA.WorkUnit; //��Ů��
                this.m_BizC.Fileds07 = this.m_PerA.BirthNum;

                /*��ż������֤�š��������Ա𡢳������¡�����״��������ʱ�䡢��ϵ�绰����������*/
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

                /*��Ů��*/
                this.m_BizC.Fileds20 = Fileds20;
                this.m_BizC.Fileds21 = Fileds21;
                this.m_BizC.Fileds37 = Fileds37;
                this.m_BizC.Fileds38 = Fileds38;

                this.m_BizC.Fileds22 = Fileds22;

                /*�˲���Ϣ*/
                this.m_BizC.Fileds23 = Fileds23;
                this.m_BizC.Fileds24 = Fileds24;
                this.m_BizC.Fileds26 = Fileds26;

                //�������� ��������
                this.m_BizC.Fileds18 = Fileds18;
                this.m_BizC.Fileds19 = DateTime.Now.ToString();

                string objID = m_BizC.Insert();
                m_BizC = null;
                if (!string.IsNullOrEmpty(objID) && PageValidate.IsNumber(objID))
                {
                    //ҵ����־
                    CommPage.SetBizLog(objID, m_UserID, "ҵ����", "Ⱥ���û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " �����ˡ�" + this.m_BizName + "���������");

                    /*���£�1.�ж�Ⱥ�ڻ��������Ƿ���ڣ������ڲ��룬���ڸ���  --�ж����ݣ�����֤��
                                     2.�жϻ��������Ƿ���ڸ��û���Ϣ�������ڲ��룬���ڲ�����  --�ж����ݣ�Ⱥ�ڱ��
                                     3.�ж���Ů�����Ƿ���ڸ��û����м�ͥ��Ů��Ϣ�������ڲ��룬���ڲ�����  --�ж����ݣ�Ⱥ�ڱ��
                              */
                    if (this.m_BizStep != BIZ_Common.InsetBizWorkFlows(this.m_BizCode, this.m_BizStep, objID, "", "", this.m_AreaCode))
                    {
                        MessageBox.Show(this, "�������̱�ʧ�ܣ�����ϵϵͳ����Ա��");
                        return;
                    }
                    #region �������
                    //========������                                   
                    this.m_PerA.MateName = this.m_PerB.PersonName;
                    this.m_PerA.MateCardID = this.m_PerB.PersonCardID;
                    string PersonIDA = this.m_PerA.ExecBizPersons();
                    //========��ż
                    this.m_PerB.MateName = this.m_PerA.PersonName;
                    this.m_PerB.MateCardID = this.m_PerA.PersonCardID;
                    string PersonIDB = this.m_PerB.ExecBizPersons();
                    //���Ͷ�Ѷ--start--
                    string uTel, uName, tMsg = string.Empty;
                    if (!string.IsNullOrEmpty(this.m_PerB.PersonTel)) { uTel = this.m_PerB.PersonTel; uName = this.m_PerB.PersonName; } else { uTel = this.m_PerA.PersonTel; uName = this.m_PerA.PersonName; }
                    tMsg = uName + "�����ύ��[" + this.m_BizName + "]���������������¼ƽ̨���û���Ϣ������鿴�������ȡ�"; //Ⱥ�ڷ���
                    if (!string.IsNullOrEmpty(uTel) && !string.IsNullOrEmpty(tMsg))
                    {
                        SendMsg.SendMsgByModem(uTel, tMsg);
                        if (!string.IsNullOrEmpty(this.m_PerA.RegisterAreaCode) && this.m_PerA.RegisterAreaCode.Substring(9) != "000") BIZ_Common.SetMsgToAuditer(this.m_BizName, uName, this.m_PerA.RegisterAreaCode);//�з���ר��
                        if (!string.IsNullOrEmpty(this.m_PerB.RegisterAreaCode) && this.m_PerB.RegisterAreaCode.Substring(9) != "000") BIZ_Common.SetMsgToAuditer(this.m_BizName, uName, this.m_PerB.RegisterAreaCode);//Ů����ר��
                    }
                    //���Ͷ�Ѷ--end--
                    m_PerA = null;
                    m_PerB = null;

                    //������Ů��Ϣ
                    this.m_Children.Inser(PersonIDA, objID, iBirthNum.ToString());
                    #endregion

                    MessageBox.ShowAndRedirect(this.Page, "", "/Svrs-BizDocs/" + m_BizCode + "-" + objID + "." + m_FileExt, false, true);
                }
                else
                {
                    MessageBox.Show(this, "������ʾ������ʧ�ܣ�����ϵϵͳ����Ա��");
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