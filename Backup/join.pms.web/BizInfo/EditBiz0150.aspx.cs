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

        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_UserName;

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;
        private string m_BizStep;
        private string m_AreaCode;

        private BIZ_Persons m_PerA;//�����з���Ϣ
        private BIZ_Persons m_PerB;//����Ů����Ϣ
        private BIZ_Contents m_BizC;//ҵ����Ϣ
        private BIZ_PersonChildren m_Children;//��Ů��Ϣ

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
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
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

        #region
        /// <summary>
        /// ������֤
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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
            else { GetUserInfo(); }
        }
        /// <summary>
        /// ��ȡ��ǰ��¼�û���Ϣ
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
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }
        }

        /// <summary>
        /// ��֤���ܵĲ���
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
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        /// <summary>
        /// ���ò�����Ϊ
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("�Ƿ����ʣ���������ֹ��");
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
                case "add": // ����
                    funcName = "����";
                    break;
                case "edit": // �༭
                    funcName = "�༭";
                    ShowModInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">������ҳ</a> &gt;&gt; ҵ����� &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }

        /// <summary>
        /// �޸� UcAreaSe08
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            try
            {
                this.m_BizC = new BIZ_Contents();
                this.m_Children = new BIZ_PersonChildren();

                /*��ѯ��䣬ǰ������--����*/
                this.m_BizC.SearchWhere = "BizID=" + objID;
                this.m_BizC.SelectAll(false);

                string Attribs = this.m_BizC.Attribs;
                this.txtAttribs.Value = Attribs;
                if (Attribs != "0" && Attribs != "3" && Attribs != "6" && m_ActionName == "edit")
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��ֻ�С���ʼ�ύ���͡���������ҵ��������༭��", m_TargetUrl, true, true);
                }
                else
                {
                    this.m_AreaCode = this.m_BizC.SelAreaCode;
                    /*Ů�����������������¡�����֤�š����塢������λ��������ַ���־�ס�ء�����״������ϵ�绰*/
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

                    /*�з����������������¡�����֤�š����塢������λ��������ַ���־�ס�ء�����״������ϵ�绰*/
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

                    //˫������(����)��Ů��
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
                    //    this.ddlIsBirth.SelectedValue = "��";
                    //    this.panelBirth.Attributes["style"] = "display:block";
                    //}
                    //if (!String.IsNullOrEmpty(this.m_BizC.Fileds43))
                    //{
                    //    this.ddlIsHY.SelectedValue = "��";
                    //    this.panelHY.Attributes["style"] = "display:block";
                    //}
                }
                this.m_BizC = null;
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
            string CidTypeA = string.Empty;
            string CidTypeB = string.Empty;
            this.m_PerA = new BIZ_Persons();
            this.m_PerB = new BIZ_Persons();
            this.m_BizC = new BIZ_Contents();
            this.m_Children = new BIZ_PersonChildren();
            string app = string.Empty;

            string bizAttribs = this.txtAttribs.Value;
            if (String.IsNullOrEmpty(bizAttribs)) strErr += "ҵ��״̬ȱʧ�������ԣ�\\n";
            #region ����

            string Fileds14 = CommBiz.GetTrim(this.txtFileds14.Value);
            string Fileds47 = CommBiz.GetTrim(this.ddlFileds47.Text);
            string Fileds23 = CommBiz.GetTrim(this.ddlFileds23.SelectedValue);
            string Fileds24 = CommBiz.GetTrim(this.ddlFileds24.SelectedValue);
            if (String.IsNullOrEmpty(Fileds14)) { strErr += "��ѡ����ʱ�䣡\\n"; }
            if (String.IsNullOrEmpty(Fileds47)) { strErr += "��ѡ����֤�ţ�\\n"; }
            /*Ů�����������������¡�����֤�š����塢������λ��������ַ���־�ס�ء�����״������ϵ�绰*/

            this.m_PerB.PersonName = PageValidate.GetTrim(this.txtPersonNameB.Text);
            this.m_PerB.PersonSex = "Ů";
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

            /*�з����������������¡�����֤�š����塢������λ��������ַ���־�ס�ء�����״������ϵ�绰*/
            string PersonNameA = PageValidate.GetTrim(this.txtPersonNameA.Text);
            string PersonSexA = "��";
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

            //==========================Ů����Ϣ start========================================== 
            if (String.IsNullOrEmpty(this.m_PerB.PersonName)) { strErr += "������Ů��������\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonCardID)) { strErr += "������Ů��֤�����룡\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerB.PersonCardID) && CidTypeB == "1") { strErr += "Ů������֤�����󣡣�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.Nations)) { strErr += "��ѡ��Ů�����壡\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.WorkUnit)) { strErr += "������Ů��������λ��\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerB.RegisterAreaCode, "0")) { strErr += "��ѡ��Ů�������أ�\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerB.CurrentAreaCode, "0")) { strErr += "��ѡ��Ů���־�ס�أ�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryType)) { strErr += "��ѡ��Ů������״����\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonTel)) { strErr += "��������ϵ�绰��\\n"; }

            //==========================�з���Ϣ start========================================== 
            if (this.m_PerB.MarryType == "����" || this.m_PerB.MarryType == "�ٻ�")
            {
                /*�з����������������¡�����֤�š����塢������λ��������ַ���־�ס�ء�����״������ϵ�绰*/
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

                if (String.IsNullOrEmpty(this.m_PerA.PersonName)) { strErr += "�������з�������\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.PersonCardID)) { strErr += "�������з�֤�����룡\\n"; }
                if (!ValidIDCard.VerifyIDCard(this.m_PerA.PersonCardID) && CidTypeB == "1") { strErr += "�з�����֤�����󣡣�\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.Nations)) { strErr += "��ѡ���з����壡\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.WorkUnit)) { strErr += "�������з�������λ��\\n"; }
                if (!CommPage.IsAreaCode(this.m_PerA.RegisterAreaCode, "0")) { strErr += "��ѡ���з������أ�\\n"; }
                if (!CommPage.IsAreaCode(this.m_PerA.CurrentAreaCode, "0")) { strErr += "��ѡ���з��־�ס�أ�\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.MarryType)) { strErr += "��ѡ���з�����״����\\n"; }
                //if (String.IsNullOrEmpty(this.m_PerA.PersonTel)) { strErr += "�������з���ϵ�绰��\\n"; }
                //if (this.m_PerA.RegisterAreaCode != m_AreaCode && this.m_PerA.CurrentAreaCode != m_AreaCode && this.m_PerB.RegisterAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "��Ů˫�������ػ��־�ס������Ҫ��һ������ѡ���һ�£�\\n"; }

                if (this.m_PerB.RegisterAreaCode == m_AreaCode || this.m_PerB.CurrentAreaCode == m_AreaCode) { app = "B"; }
                else { app = "A"; }
            }
            else
            {
                this.m_BizStep = "2";
                app = "B";
                //if (this.m_PerB.RegisterAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "Ů˫�������ػ��־�ס������Ҫ��һ������ѡ���һ�£�\\n"; }

                ////������λָ��
                //areaCodeSL = this.m_PerB.CurrentAreaCode;
            }

            /*�������в���Ϣ�����ʱ�䡢����ʱ�䡢Ԥ�����ڡ��������ڡ������Ա𡢳���ҽѧ֤�����*/
            //if (String.IsNullOrEmpty(this.txtFileds14.Value)) { strErr += "��ѡ����ʱ�䣡\\n"; }
            //if (this.ddlIsHY.SelectedValue == "��" && String.IsNullOrEmpty(this.txtFileds43.Value)) { strErr += "��ѡ����ʱ�䣡\\n"; }
            ////if (String.IsNullOrEmpty(this.txtFileds44.Value)) { strErr += "��ѡ��Ԥ�����ڣ�\\n"; }
            //if (this.ddlIsBirth.SelectedValue == "��" && String.IsNullOrEmpty(this.txtFileds18.Value)) { strErr += "��ѡ���ӳ������ڣ�\\n"; }
            //if (this.ddlIsBirth.SelectedValue == "��" && this.ddlChildSex.SelectedValue == "") { strErr += "��ѡ�����Ա�\\n"; }
            //if (String.IsNullOrEmpty(this.txtFileds45.Text)) { strErr += "���������ҽѧ֤����ţ�\\n"; }

            #endregion      

            if (cbOk.Checked == false)
            {
                strErr += "��ȷ�ϳ�ŵ��\\n";
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
                //ȡ֤�ص�
                this.m_BizC.GetAreaCode = "";
                this.m_BizC.GetAreaName = "";

                /*�з����������������¡�����֤�š����塢������λ��������ַ���־�ס�ء�����״������ϵ�绰*/
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

                /*Ů�����������������¡�����֤�š����塢������λ��������ַ���־�ס�ء�����״������ϵ�绰*/
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

                /*�������в���Ϣ�����ʱ�䡢����ʱ�䡢Ԥ�����ڡ��������ڡ������Ա𡢳���ҽѧ֤�����*/
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
                    CommPage.SetBizLog(objID, m_UserID, "ҵ���޸�", "����Ա�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " �����ˡ�" + this.m_NavTitle + "���޸Ĳ���");
                    MessageBox.ShowAndRedirect(this.Page, "", "EditBizDocs.aspx?action=edit&k=" + objID + "&sourceUrl=" + m_SourceUrl, true, true);
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
                //Response.Write(" <script>alert('����ʧ�ܣ�" + ex.Message + "') ;</script>");
                return;
            }
        }
        #endregion
    }
}
