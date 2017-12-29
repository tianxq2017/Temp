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

using System.Text;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;
namespace join.pms.wap.SceneSvrs
{
    public partial class Biz0110 : UNV.Comm.Web.PageBase
    {
        #region
        private BIZ_Persons m_PerA;//�����з���Ϣ
        private BIZ_Persons m_PerB;//����Ů����Ϣ  
        private BIZ_Contents m_BizC;
        private BIZ_PersonChildren m_Children;//���溢����Ϣ

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
                GetBizPersonsInfo();
                //������Ĭ�ϵ�ǰѡ�����
                this.UcAreaSelRegB.SetAreaCode(m_AreaCode);
                this.UcAreaSelRegB.SetAreaName(BIZ_Common.GetAreaName(m_AreaCode, "1"));
                //ȡ֤�ص�
                this.UcAreaSelXian1.SetAreaCode(m_AreaCode);
            }
            //GetParam(this.txtHNationsA.Value, this.txtHNationsB.Value);
            GetParam(PageValidate.GetTrim(Request["txtNationsA"]), PageValidate.GetTrim(Request["txtNationsB"]));
        }
        #region ��ȡ���ò��� �磺�����
        /// <summary>
        /// ��ȡ���ò��� �磺�����
        /// </summary>
        private void GetParam(string nationsA, string nationsB)
        {
            BIZ_Common.GetNations(this.LiteralNationsA, "txtNationsA", nationsA);
            BIZ_Common.GetNations(this.LiteralNationsB, "txtNationsB", nationsB);
        }
        #endregion
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
                this.m_BizCode = "0110";
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
        /// �����֤
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

                this.txtPersonNameA.Text = bizPer.PersonName;
                this.txtHNationsA.Value = bizPer.Nations;
                this.txtPersonCidA.Text = bizPer.PersonCardID;
                this.txtContactTelA.Text = bizPer.PersonTel;
                this.ddlMarryTypeA.SelectedValue = bizPer.MarryType;
                this.UcAreaSelCurA.SetAreaCode(bizPer.CurrentAreaCode);
                this.UcAreaSelCurA.SetAreaName(bizPer.CurrentAreaName);

                //��ż��Ϣ
                this.txtPersonCidB.Text = bizPer.MateCardID;
                this.txtPersonNameB.Text = bizPer.MateName;

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
            string app = string.Empty;

            #region ����

            /*���ˣ��������Ա����塢�������ʡ����֤�š���ϵ�绰������״����������ַ����ס��ַ���������¡��ʼ�*/
            this.m_PerA.PersonName = PageValidate.GetTrim(this.txtPersonNameA.Text);
            this.m_PerA.Nations = PageValidate.GetTrim(Request["txtNationsA"]);
            this.m_PerA.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeA.Text);
            this.m_PerA.PersonCardID = PageValidate.GetTrim(this.txtPersonCidA.Text);
            this.m_PerA.PersonSex = CommBiz.GetSexByID(this.txtPersonCidA.Text);
            this.m_PerA.PersonTel = PageValidate.GetTrim(this.txtContactTelA.Text);
            this.m_PerA.MarryType = PageValidate.GetTrim(this.ddlMarryTypeA.SelectedValue);
            this.m_PerA.RegisterAreaCode = this.txtAreaSelRegCodeA.Value;
            this.m_PerA.RegisterAreaName = this.txtAreaSelRegNameA.Text;
            this.m_PerA.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
            this.m_PerA.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaName());
            this.m_PerA.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerA.PersonCardID);
            string perMail = CommBiz.GetTrim(this.txtMail.Text);
            this.m_PerA.BirthNum = PageValidate.GetTrim(this.ddlBirthNum.SelectedValue);

            if (this.m_PerA.MarryType == "����" || this.m_PerA.MarryType == "�ٻ�")
            {
                /*��ż���������Ա����塢�������ʡ����֤�š���ϵ�绰������״����������ַ����ס��ַ����������*/
                this.m_PerB.PersonName = PageValidate.GetTrim(this.txtPersonNameB.Text);
                this.m_PerB.Nations = PageValidate.GetTrim(Request["txtNationsB"]);
                this.m_PerB.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeB.Text);
                this.m_PerB.PersonCardID = PageValidate.GetTrim(this.txtPersonCidB.Text);
                this.m_PerB.PersonSex = CommBiz.GetSexByID(this.txtPersonCidB.Text);
                this.m_PerB.PersonTel = PageValidate.GetTrim(this.txtContactTelB.Text);
                this.m_PerB.MarryType = PageValidate.GetTrim(this.ddlMarryType.SelectedValue);
                this.m_PerB.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaCode());
                this.m_PerB.RegisterAreaName = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaName());
                this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
                this.m_PerB.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaName());
                this.m_PerB.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerB.PersonCardID);
            }
            else
            {
                this.m_PerB.PersonName = "";
                this.m_PerB.Nations = "";
                this.m_PerB.RegisterType = "";
                this.m_PerB.PersonCardID = "";
                this.m_PerB.PersonSex = "";
                this.m_PerB.PersonTel = "";
                this.m_PerB.MarryType = "";
                this.m_PerB.RegisterAreaCode = "";
                this.m_PerB.RegisterAreaName = "";
                this.m_PerB.CurrentAreaCode = "";
                this.m_PerB.CurrentAreaName = "";
                this.m_PerB.PersonBirthday = "";
            }

            //==========================��֤����Ϣ start========================================== 
            if (String.IsNullOrEmpty(this.m_PerA.PersonName)) { strErr += "�������֤��������\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.Nations)) { strErr += "�������֤�����壡\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.PersonCardID)) { strErr += "�������֤�����֤�ţ�\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerA.PersonCardID)) { strErr += "��֤�����֤�����󣡣�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.PersonTel)) { strErr += "�������֤����ϵ�绰��\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.MarryType)) { strErr += "��ѡ���֤�˻���״����\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerA.RegisterAreaCode, "0")) { strErr += "��ѡ���֤�˻����أ�\\n"; }
            if (!String.IsNullOrEmpty(perMail))
            {
                if (!PageValidate.IsEmail(perMail)) { strErr += "��������ȷ�������ַ��\\n"; }
            }
            if (!CommPage.IsAreaCode(this.m_PerA.CurrentAreaCode, "0")) { strErr += "��ѡ���֤���־�ס�أ�\\n"; }

            if (this.m_PerA.MarryType == "����" || this.m_PerA.MarryType == "�ٻ�")
            {
                //==========================��ż��Ϣ start========================================== 
                if (String.IsNullOrEmpty(this.m_PerB.PersonName)) { strErr += "��������ż������\\n"; }
                if (String.IsNullOrEmpty(this.m_PerB.Nations)) { strErr += "��������ż���壡\\n"; }
                if (String.IsNullOrEmpty(this.m_PerB.PersonCardID)) { strErr += "��������ż���֤�ţ�\\n"; }
                if (!ValidIDCard.VerifyIDCard(this.m_PerB.PersonCardID)) { strErr += "��ż���֤�����󣡣�\\n"; }
                if (String.IsNullOrEmpty(this.m_PerB.PersonTel)) { strErr += "��������ż��ϵ�绰��\\n"; }
                if (String.IsNullOrEmpty(this.m_PerB.MarryType)) { strErr += "��ѡ����ż����״����\\n"; }
                if (!CommPage.IsAreaCode(this.m_PerB.RegisterAreaCode, "0")) { strErr += "��ѡ����ż�����أ�\\n"; }
                if (!CommPage.IsAreaCode(this.m_PerB.CurrentAreaCode, "0")) { strErr += "��ѡ����ż�־�ס�أ�\\n"; }

                if (this.m_PerA.RegisterAreaCode != m_AreaCode && this.m_PerA.CurrentAreaCode != m_AreaCode && this.m_PerB.RegisterAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "��Ů˫�������ػ��־�ס������Ҫ��һ������ѡ���һ�£�\\n"; }
            }
            else
            {
                if (this.m_PerA.RegisterAreaCode != m_AreaCode && this.m_PerA.CurrentAreaCode != m_AreaCode) { strErr += "��֤�˻����ػ��־�ס������Ҫ��һ������ѡ���һ�£�\\n"; }
            }


            //��Ů��Ϣ
            int iBirthNum = int.Parse(this.m_PerA.BirthNum);
            #region ��Ů��Ϣ
            string ChildName1 = PageValidate.GetTrim(this.txtChildName1.Text);
            string ChildSex1 = PageValidate.GetTrim(this.ddlChildSex1.SelectedValue);
            string ChildBirthday1 = CommBiz.GetTrim(this.txtChildBirthday1.Value);
            string ChildPolicy1 = PageValidate.GetTrim(this.ddlChildPolicy1.Text);
            string ChildCardID1 = PageValidate.GetTrim(this.txtChildCardID1.Text);
            string ChildSource1 = PageValidate.GetTrim(this.ddlChildSource1.Text);
            string ChildMemos1 = PageValidate.GetTrim(this.ddlMemos1.Text);
            string ChildNo1 = "1";

            string ChildName2 = PageValidate.GetTrim(this.txtChildName2.Text);
            string ChildSex2 = PageValidate.GetTrim(this.ddlChildSex2.SelectedValue);
            string ChildBirthday2 = CommBiz.GetTrim(this.txtChildBirthday2.Value);
            string ChildPolicy2 = PageValidate.GetTrim(this.ddlChildPolicy2.Text);
            string ChildCardID2 = PageValidate.GetTrim(this.txtChildCardID2.Text);
            string ChildSource2 = PageValidate.GetTrim(this.ddlChildSource2.Text);
            string ChildMemos2 = PageValidate.GetTrim(this.ddlMemos2.Text);
            string ChildNo2 = "2";

            string ChildName3 = PageValidate.GetTrim(this.txtChildName3.Text);
            string ChildSex3 = PageValidate.GetTrim(this.ddlChildSex3.SelectedValue);
            string ChildBirthday3 = CommBiz.GetTrim(this.txtChildBirthday3.Value);
            string ChildPolicy3 = PageValidate.GetTrim(this.ddlChildPolicy3.Text);
            string ChildCardID3 = PageValidate.GetTrim(this.txtChildCardID3.Text);
            string ChildSource3 = PageValidate.GetTrim(this.ddlChildSource3.Text);
            string ChildMemos3 = PageValidate.GetTrim(this.ddlMemos3.Text);
            string ChildNo3 = "3";

            if (iBirthNum == 0)
            {
                this.m_PerB.BirthNum = "0";
                ChildName1 = ""; ChildName2 = ""; ChildName3 = "";
            }
            else if (iBirthNum == 1)
            {
                ChildName2 = ""; ChildSex2 = ""; ChildBirthday2 = ""; ChildPolicy2 = ""; ChildCardID2 = ""; ChildSource2 = ""; ChildMemos2 = "";
                ChildName3 = ""; ChildSex3 = ""; ChildBirthday3 = ""; ChildPolicy3 = ""; ChildCardID3 = ""; ChildSource3 = ""; ChildMemos3 = "";
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }
            }
            else if (iBirthNum == 2)
            {
                ChildName3 = ""; ChildSex3 = ""; ChildBirthday3 = ""; ChildPolicy3 = ""; ChildCardID3 = ""; ChildSource3 = ""; ChildMemos3 = "";

                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "��������Ů2������\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "��ѡ����Ů2�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "��ѡ����Ů2�������£�\\n"; }

                if (ChildName1 == ChildName2) { strErr += "��Ů�����ظ���\\n"; }
            }
            else if (iBirthNum == 3)
            {
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "��������Ů2������\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "��ѡ����Ů2�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "��ѡ����Ů2�������£�\\n"; }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "��������Ů3������\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "��ѡ����Ů3�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "��ѡ����Ů3�������£�\\n"; }

                if (ChildName1 == ChildName2 || ChildName1 == ChildName3 || ChildName2 == ChildName3) { strErr += "��Ů�����ظ���\\n"; }
            }
            this.m_Children.ChildName1 = ChildName1;
            this.m_Children.ChildSex1 = ChildSex1;
            this.m_Children.ChildBirthday1 = ChildBirthday1;
            this.m_Children.ChildPolicy1 = ChildPolicy1;
            this.m_Children.ChildCardID1 = ChildCardID1;
            this.m_Children.ChildSource1 = ChildSource1;
            this.m_Children.Memos1 = ChildMemos1;
            this.m_Children.ChildNo1 = ChildNo1;

            this.m_Children.ChildName2 = ChildName2;
            this.m_Children.ChildSex2 = ChildSex2;
            this.m_Children.ChildBirthday2 = ChildBirthday2;
            this.m_Children.ChildPolicy2 = ChildPolicy2;
            this.m_Children.ChildCardID2 = ChildCardID2;
            this.m_Children.ChildSource2 = ChildSource2;
            this.m_Children.Memos2 = ChildMemos2;
            this.m_Children.ChildNo2 = ChildNo2;

            this.m_Children.ChildName3 = ChildName3;
            this.m_Children.ChildSex3 = ChildSex3;
            this.m_Children.ChildBirthday3 = ChildBirthday3;
            this.m_Children.ChildPolicy3 = ChildPolicy3;
            this.m_Children.ChildCardID3 = ChildCardID3;
            this.m_Children.ChildSource3 = ChildSource3;
            this.m_Children.Memos3 = ChildMemos3;
            this.m_Children.ChildNo3 = ChildNo3;
            #endregion

            /*�������*/
            string Fileds18 = PageValidate.GetTrim(this.txtFileds18.Text);
            if (this.m_PerA.MarryType == "δ��" || this.m_PerA.MarryType == "����")
            {
            }
            else
            {
                if (String.IsNullOrEmpty(Fileds18)) { strErr += "���������������\\n"; }
            }
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
                this.m_BizStep = this.ddlBizStep.SelectedValue;
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

                /*��֤�ˣ��������Ա����塢�������ʡ����֤�š���ϵ�绰������״����������ַ����ס��ַ���������¡��ʼ�*/
                this.m_BizC.Fileds01 = this.m_PerA.PersonName;
                this.m_BizC.Fileds02 = this.m_PerA.PersonSex;
                this.m_BizC.Fileds03 = this.m_PerA.Nations;
                this.m_BizC.Fileds04 = this.m_PerA.RegisterType;
                this.m_BizC.PersonCidA = this.m_PerA.PersonCardID;
                this.m_BizC.ContactTelA = this.m_PerA.PersonTel;
                this.m_BizC.Fileds33 = this.m_PerA.MarryType;
                this.m_BizC.RegAreaCodeA = this.m_PerA.RegisterAreaCode;
                this.m_BizC.RegAreaNameA = this.m_PerA.RegisterAreaName;
                this.m_BizC.CurAreaCodeA = this.m_PerA.CurrentAreaCode;
                this.m_BizC.CurAreaNameA = this.m_PerA.CurrentAreaName;
                this.m_BizC.Fileds32 = this.m_PerA.PersonBirthday;
                this.m_BizC.Fileds45 = perMail;

                //��Ů��
                this.m_BizC.Fileds07 = this.m_PerB.BirthNum;

                /*��ż���������Ա����塢�������ʡ����֤�š���ϵ�绰������״����������ַ����ס��ַ����������*/
                this.m_BizC.Fileds08 = this.m_PerB.PersonName;
                this.m_BizC.Fileds09 = this.m_PerB.PersonSex;
                this.m_BizC.Fileds10 = this.m_PerB.Nations;
                this.m_BizC.Fileds11 = this.m_PerB.RegisterType;
                this.m_BizC.PersonCidB = this.m_PerB.PersonCardID;
                this.m_BizC.ContactTelB = this.m_PerB.PersonTel;
                this.m_BizC.Fileds13 = this.m_PerB.MarryType;
                this.m_BizC.RegAreaCodeB = this.m_PerB.RegisterAreaCode;
                this.m_BizC.RegAreaNameB = this.m_PerB.RegisterAreaName;
                this.m_BizC.CurAreaCodeB = this.m_PerB.CurrentAreaCode;
                this.m_BizC.CurAreaNameB = this.m_PerB.CurrentAreaName;
                this.m_BizC.Fileds31 = this.m_PerB.PersonBirthday;

                //����״�� ��������
                this.m_BizC.Fileds18 = Fileds18;
                this.m_BizC.Fileds19 = DateTime.Now.ToString();

                string objID = m_BizC.Insert();
                m_BizC = null;
                if (!string.IsNullOrEmpty(objID) && PageValidate.IsNumber(objID))
                {
                    //ҵ����־
                    CommPage.SetBizLog(objID, m_UserID, "ҵ����", "Ⱥ���û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " �����ˡ�" + this.m_BizName + "���������");

                    /*���£�1.�ж�Ⱥ�ڻ��������Ƿ���ڣ������ڲ��룬���ڸ���  --�ж����ݣ����֤��
                            2.�жϻ��������Ƿ���ڸ��û���Ϣ�������ڲ��룬���ڲ�����  --�ж����ݣ�Ⱥ�ڱ��
                            3.�ж���Ů�����Ƿ���ڸ��û����м�ͥ��Ů��Ϣ�������ڲ��룬���ڲ�����  --�ж����ݣ�Ⱥ�ڱ��
                     */
                    //#region һ�����⴦��
                    //string perAAreaCode = string.Empty;
                    //string perBAreaCode = string.Empty;
                    //if (this.m_PerA.RegisterAreaCode.Substring(0, 3) != "150")
                    //{
                    //    perAAreaCode = this.m_PerB.RegisterAreaCode;
                    //    perBAreaCode = this.m_PerB.RegisterAreaCode;
                    //}
                    //else if (this.m_PerB.RegisterAreaCode.Substring(0, 3) != "150")
                    //{
                    //    perAAreaCode = this.m_PerA.RegisterAreaCode;
                    //    perBAreaCode = this.m_PerA.RegisterAreaCode;
                    //}
                    //else
                    //{
                    //    perAAreaCode = this.m_PerA.RegisterAreaCode;
                    //    perBAreaCode = this.m_PerB.RegisterAreaCode;
                    //}

                    //#endregion
                    if (this.m_BizStep != BIZ_Common.InsetBizWorkFlows(this.m_BizCode, this.m_BizStep, objID, "", "", m_AreaCode))
                    {
                        MessageBox.Show(this, "�������̱�ʧ�ܣ�����ϵϵͳ����Ա��");
                        return;
                    }
                    #region �������
                    //========��֤�� 
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
                    tMsg = uName + "�����ύ��[" + this.m_BizName + "]�������������¼ƽ̨���û���Ϣ������鿴������ȡ�"; //Ⱥ�ڷ���
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
