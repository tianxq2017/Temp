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
    public partial class EditBiz0101 : System.Web.UI.Page
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

            //������Ĭ�ϵ�ǰѡ�����
            this.txtAreaSelRegNameA.ReadOnly = true;
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
        /// �����֤
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
                //����������ʱ�ɱ༭ 2016/03/30 by Ysl
                bool isKuLunEdit = false;
                if (int.Parse(this.m_BizC.BizID) < 3099 && this.m_BizC.SelAreaCode.Substring(0, 6) == "150524") isKuLunEdit = true;
                if (Attribs != "0" && Attribs != "3" && Attribs != "6" && m_ActionName == "edit" && !isKuLunEdit)
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��ֻ�С���ʼ�ύ���͡���������ҵ�������༭��", m_TargetUrl, true, true);
                }
                else
                {
                    this.m_AreaCode = this.m_BizC.SelAreaCode;
                    //ȡ֤�ص�
                    this.UcAreaSelXian1.SetAreaCode(this.m_BizC.GetAreaCode, true);
                    /*Ů�������֤�š�����������״��������ʱ�䡢���塢��ϵ�绰���������ʡ�������ַ����ס��ַ��������λ���Ƿ��Ƕ�����Ů*/
                    this.txtPersonCidB.Text = this.m_BizC.PersonCidB;
                    this.txtPersonNameB.Text = this.m_BizC.Fileds08;
                    this.ddlMarryTypeB.SelectedValue = this.m_BizC.Fileds13;
                    this.ddlCidTypeB.SelectedValue = this.m_BizC.Fileds50;
                    this.txtMarryDateB.Value = this.m_BizC.Fileds14;
                    string NationsB = this.m_BizC.Fileds10;
                    this.txtContactTelB.Text = this.m_BizC.ContactTelB;
                    this.ddlRegisterTypeB.SelectedValue = this.m_BizC.Fileds11;
                    this.txtAreaSelRegNameB.Text = this.m_BizC.RegAreaNameB;
                    this.txtAreaSelRegCodeB.Value = this.m_BizC.RegAreaCodeB;
                    this.UcAreaSelCurB.SetAreaName(this.m_BizC.CurAreaNameB);
                    this.UcAreaSelCurB.SetAreaCode(this.m_BizC.CurAreaCodeB);
                    this.txtWorkUnitB.Text = this.m_BizC.Fileds12;
                    this.ddlFileds16.SelectedValue = ""; //this.m_BizC.Fileds16;


                    /*�з������֤�š�����������״��������ʱ�䡢���塢��ϵ�绰���������ʡ�������ַ����ס��ַ��������λ���Ƿ��Ƕ�����Ů*/
                    this.txtPersonCidA.Text = this.m_BizC.PersonCidA;
                    this.txtPersonNameA.Text = this.m_BizC.Fileds01;
                    this.ddlMarryTypeA.SelectedValue = this.m_BizC.Fileds33;
                    this.txtMarryDateA.Value = this.m_BizC.Fileds34;
                    string NationsA = this.m_BizC.Fileds03;
                    this.txtContactTelA.Text = this.m_BizC.ContactTelA;
                    this.ddlRegisterTypeA.SelectedValue = this.m_BizC.Fileds04;
                    this.txtAreaSelRegNameA.Text = this.m_BizC.RegAreaNameA;
                    this.txtAreaSelRegCodeA.Value = this.m_BizC.RegAreaCodeA;
                    this.UcAreaSelCurA.SetAreaName(this.m_BizC.CurAreaNameA);
                    this.UcAreaSelCurA.SetAreaCode(this.m_BizC.CurAreaCodeA);
                    this.txtWorkUnitA.Text = this.m_BizC.Fileds05; 
                    this.ddlCidTypeA.SelectedValue = this.m_BizC.Fileds49;
                    this.ddlFileds17.SelectedValue = ""; //this.m_BizC.Fileds17;

                    GetParam(NationsA, NationsB);

                    //�������� ��������
                    this.txtFileds18.Text = this.m_BizC.Fileds18;

                    //if (!String.IsNullOrEmpty(this.m_BizC.PersonPhotos))
                    //{
                    //    this.txtIsHasPhotos.Value = "1";
                    //    this.LiteralPersonPhotos.Text = "<tr><th>�Ѵ���Ƭ��</th><td><img src=\"" + this.m_BizC.PersonPhotos + "\" width=\"50\" height=\"50\"></td></tr>";
                    //}
                    //��Ů��Ϣ
                    string BirthNum = this.m_BizC.Fileds07;
                    this.ddlBirthNum.SelectedValue = BirthNum;

                    this.m_Children.Select("", objID);

                    this.txtChildName1.Text = this.m_Children.ChildName1;
                    this.ddlChildSex1.SelectedValue = this.m_Children.ChildSex1;
                    this.txtChildBirthday1.Value = this.m_Children.ChildBirthday1;
                    this.txtChildCardID1.Text = this.m_Children.ChildCardID1;
                    this.txtChildID1.Value = this.m_Children.CommID1;

                    this.txtChildName2.Text = this.m_Children.ChildName2;
                    this.ddlChildSex2.SelectedValue = this.m_Children.ChildSex2;
                    this.txtChildBirthday2.Value = this.m_Children.ChildBirthday2;
                    this.txtChildCardID2.Text = this.m_Children.ChildCardID2;
                    this.txtChildID2.Value = this.m_Children.CommID2;

                    this.txtChildName3.Text = this.m_Children.ChildName3;
                    this.ddlChildSex3.SelectedValue = this.m_Children.ChildSex3;
                    this.txtChildBirthday3.Value = this.m_Children.ChildBirthday3;
                    this.txtChildCardID3.Text = this.m_Children.ChildCardID3;
                    this.txtChildID3.Value = this.m_Children.CommID3;

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

            /*Ů�������֤�š��������Ա𡢳������¡�����״��������ʱ�䡢���塢��ϵ�绰���������ʡ�������ַ����ס��ַ��������λ���Ƿ��Ƕ�����Ů*/
            this.m_PerB.PersonCardID = PageValidate.GetTrim(this.txtPersonCidB.Text);
            this.m_PerB.PersonName = PageValidate.GetTrim(this.txtPersonNameB.Text);
            this.m_PerB.PersonSex = "Ů";
            this.m_PerB.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerB.PersonCardID);
            this.m_PerB.MarryType = PageValidate.GetTrim(this.ddlMarryTypeB.SelectedValue);
            this.m_PerB.MarryDate = CommBiz.GetTrim(this.txtMarryDateB.Value);
            this.m_PerB.Nations = PageValidate.GetTrim(Request["txtNationsB"]);
            this.m_PerB.PersonTel = PageValidate.GetTrim(this.txtContactTelB.Text);
            this.m_PerB.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeB.Text);
            //this.m_PerB.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaCode());
            //this.m_PerB.RegisterAreaName = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaName());
            this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
            this.m_PerB.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaName());
            this.m_PerB.WorkUnit = PageValidate.GetTrim(this.txtWorkUnitB.Text);
            string Fileds16 = "";//PageValidate.GetTrim(this.ddlFileds16.SelectedValue);
            this.m_PerB.BirthNum = PageValidate.GetTrim(this.ddlBirthNum.SelectedValue);
            CidTypeA = PageValidate.GetTrim(this.ddlCidTypeA.SelectedValue);
            CidTypeB = PageValidate.GetTrim(this.ddlCidTypeB.SelectedValue);
            /*�з������֤�š��������Ա𡢳������¡�����״��������ʱ�䡢���塢��ϵ�绰���������ʡ�������ַ����ס��ַ��������λ���Ƿ��Ƕ�����Ů*/
            string PersonCardIDA = PageValidate.GetTrim(this.txtPersonCidA.Text);
            string PersonNameA = PageValidate.GetTrim(this.txtPersonNameA.Text);
            string PersonSexA = "��";
            string PersonBirthdayA = CommBiz.GetBirthdayByID(PersonCardIDA);
            string MarryTypeA = PageValidate.GetTrim(this.ddlMarryTypeA.SelectedValue);
            string MarryDateA = CommBiz.GetTrim(this.txtMarryDateA.Value);
            string NationsA = PageValidate.GetTrim(Request["txtNationsA"]);
            string PersonTelA = PageValidate.GetTrim(this.txtContactTelA.Text);
            string RegisterTypeA = PageValidate.GetTrim(this.ddlRegisterTypeA.Text);
            //string RegisterAreaCodeA = PageValidate.GetTrim(this.UcAreaSelRegA.GetAreaCode());
            //string RegisterAreaNameA = PageValidate.GetTrim(this.UcAreaSelRegA.GetAreaName());
            string CurrentAreaCodeA = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
            string CurrentAreaNameA = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaName());
            string WorkUnitA = PageValidate.GetTrim(this.txtWorkUnitA.Text);
            //====================================================================    

            //==========================Ů����Ϣ start========================================== 
            if (String.IsNullOrEmpty(this.m_PerB.PersonCardID)) { strErr += "������Ů�����֤�ţ�\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerB.PersonCardID) && CidTypeB == "1") { strErr += "Ů�����֤�����󣡣�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonName)) { strErr += "������Ů��������\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryType)) { strErr += "��ѡ��Ů������״����\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryDate)) { strErr += "��ѡ��Ů������ʱ�䣡\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.Nations)) { strErr += "������Ů�����壡\\n"; }
            //if (String.IsNullOrEmpty(Fileds16)) { strErr += "��ѡ��Ů���Ƿ��Ƕ�����Ů��\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.RegisterType)) { strErr += "��ѡ��Ů���������ʣ�\\n"; }
            // if (!CommPage.IsAreaCode(this.m_PerB.RegisterAreaCode, "0")) { strErr += "��ѡ��Ů�������أ�\\n"; }
            //if (!CommPage.IsAreaCode(this.m_PerB.CurrentAreaCode, "0")) { strErr += "��ѡ��Ů���־�ס�أ�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonTel)) { strErr += "������Ů����ϵ�绰��\\n"; }

            //==========================�з���Ϣ start========================================== 
            if (this.m_PerB.MarryType == "����" || this.m_PerB.MarryType == "�ٻ�")
            {
                /*�з������֤�š��������Ա𡢳������¡�����״��������ʱ�䡢���塢��ϵ�绰���������ʡ�������ַ����ס��ַ��������λ���Ƿ��Ƕ�����Ů*/
                this.m_PerA.PersonCardID = PersonCardIDA;
                this.m_PerA.PersonName = PersonNameA;
                this.m_PerA.PersonSex = PersonSexA;
                this.m_PerA.PersonBirthday = PersonBirthdayA;
                this.m_PerA.MarryType = MarryTypeA;
                this.m_PerA.MarryDate = MarryDateA;
                this.m_PerA.Nations = NationsA;
                this.m_PerA.PersonTel = PersonTelA;
                this.m_PerA.RegisterType = RegisterTypeA;
                //this.m_PerA.RegisterAreaCode = RegisterAreaCodeA;
                //this.m_PerA.RegisterAreaName = RegisterAreaNameA;
                this.m_PerA.CurrentAreaCode = CurrentAreaCodeA;
                this.m_PerA.CurrentAreaName = CurrentAreaNameA;
                this.m_PerA.WorkUnit = WorkUnitA;
                //PageValidate.GetTrim(this.ddlFileds17.SelectedValue);

                if (String.IsNullOrEmpty(this.m_PerA.PersonCardID)) { strErr += "�������з����֤�ţ�\\n"; }
                if (!ValidIDCard.VerifyIDCard(this.m_PerA.PersonCardID) && CidTypeA == "1") { strErr += "�з����֤�����󣡣�\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.PersonName)) { strErr += "�������з�������\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.MarryType)) { strErr += "��ѡ���з�����״����\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.MarryDate)) { strErr += "��ѡ���з�����ʱ�䣡\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.Nations)) { strErr += "�������з����壡\\n"; }
                //if (String.IsNullOrEmpty(Fileds17)) { strErr += "��ѡ���з��Ƿ��Ƕ�����Ů��\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.RegisterType)) { strErr += "��ѡ���з��������ʣ�\\n"; }
                //if (!CommPage.IsAreaCode(this.m_PerA.RegisterAreaCode, "0")) { strErr += "��ѡ���з������أ�\\n"; }
                //if (!CommPage.IsAreaCode(this.m_PerA.CurrentAreaCode, "0")) { strErr += "��ѡ���з��־�ס�أ�\\n"; }
                if (this.m_PerA.RegisterAreaCode != m_AreaCode && this.m_PerA.CurrentAreaCode != m_AreaCode && this.m_PerB.RegisterAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "��Ů˫�������ػ��־�ס������Ҫ��һ������ѡ���һ�£�\\n"; }

                ////����λָ��
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
                if (this.m_PerB.RegisterAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "Ů˫�������ػ��־�ס������Ҫ��һ������ѡ���һ�£�\\n"; }

                ////����λָ��
                //areaCodeSL = this.m_PerB.CurrentAreaCode;
            }
            //��Ů��Ϣ
            int iBirthNum = int.Parse(this.m_PerB.BirthNum);
            #region ��Ů��Ϣ
            string CommID1 = PageValidate.GetTrim(this.txtChildID1.Value);
            string ChildName1 = PageValidate.GetTrim(this.txtChildName1.Text);
            string ChildSex1 = PageValidate.GetTrim(this.ddlChildSex1.SelectedValue);
            string ChildBirthday1 = CommBiz.GetTrim(this.txtChildBirthday1.Value);
            string ChildCardID1 = PageValidate.GetTrim(this.txtChildCardID1.Text);
            string ChildNo1 = "1";

            string CommID2 = PageValidate.GetTrim(this.txtChildID2.Value);
            string ChildName2 = PageValidate.GetTrim(this.txtChildName2.Text);
            string ChildSex2 = PageValidate.GetTrim(this.ddlChildSex2.SelectedValue);
            string ChildBirthday2 = CommBiz.GetTrim(this.txtChildBirthday2.Value);
            string ChildCardID2 = PageValidate.GetTrim(this.txtChildCardID2.Text);
            string ChildNo2 = "2";

            string CommID3 = PageValidate.GetTrim(this.txtChildID3.Value);
            string ChildName3 = PageValidate.GetTrim(this.txtChildName3.Text);
            string ChildSex3 = PageValidate.GetTrim(this.ddlChildSex3.SelectedValue);
            string ChildBirthday3 = CommBiz.GetTrim(this.txtChildBirthday3.Value);
            string ChildCardID3 = PageValidate.GetTrim(this.txtChildCardID3.Text);
            string ChildNo3 = "3";

            if (iBirthNum == 0)
            {
                this.m_PerB.BirthNum = "0";
                CommID1 = ""; ChildName1 = ""; ChildName2 = ""; ChildName3 = "";
            }
            else if (iBirthNum == 1)
            {
                CommID2 = ""; ChildName2 = ""; ChildName3 = "";
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }
            }
            else if (iBirthNum == 2)
            {
                CommID3 = ""; ChildName3 = "";

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
            this.m_Children.CommID1 = this.txtChildID1.Value;
            this.m_Children.ChildName1 = ChildName1;
            this.m_Children.ChildSex1 = ChildSex1;
            this.m_Children.ChildBirthday1 = ChildBirthday1;
            this.m_Children.ChildCardID1 = ChildCardID1;
            this.m_Children.ChildNo1 = ChildNo1;

            this.m_Children.CommID2 = this.txtChildID2.Value;
            this.m_Children.ChildName2 = ChildName2;
            this.m_Children.ChildSex2 = ChildSex2;
            this.m_Children.ChildBirthday2 = ChildBirthday2;
            this.m_Children.ChildCardID2 = ChildCardID2;
            this.m_Children.ChildNo2 = ChildNo2;

            this.m_Children.CommID3 = this.txtChildID3.Value;
            this.m_Children.ChildName3 = ChildName3;
            this.m_Children.ChildSex3 = ChildSex3;
            this.m_Children.ChildBirthday3 = ChildBirthday3;
            this.m_Children.ChildCardID3 = ChildCardID3;
            this.m_Children.ChildNo3 = ChildNo3;
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

            //2������ڷ򸾺�Ӱ��Ƭ
            //string strMsg = string.Empty;
            //string[] PersonPhotos = new string[3];
            //if (this.txtPersonPhotos.HasFile)
            //{
            //    if (!BIZ_UpFhotos.UploadPhotos(this.txtPersonPhotos, ref PersonPhotos, "0", ref strMsg))
            //    {
            //        strErr += "2������ڷ򸾺�Ӱ��Ƭ����ʧ�ܣ�\\n";
            //    }
            //}

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
                this.m_BizC.GetAreaCode = GetAreaCode;
                this.m_BizC.GetAreaName = GetAreaName;

                //2������ڷ򸾺�Ӱ��Ƭ
                //if (!String.IsNullOrEmpty(PersonPhotos[1])) { this.m_BizC.PersonPhotos = PersonPhotos[1]; }

                /*�з������֤�š��������Ա𡢳������¡�����״��������ʱ�䡢���塢��ϵ�绰���������ʡ�������ַ����ס��ַ��������λ���Ƿ��Ƕ�����Ů*/
                this.m_BizC.PersonCidA = this.m_PerA.PersonCardID;
                this.m_BizC.Fileds01 = this.m_PerA.PersonName;
                this.m_BizC.Fileds02 = this.m_PerA.PersonSex;
                this.m_BizC.Fileds32 = this.m_PerA.PersonBirthday;
                this.m_BizC.Fileds33 = this.m_PerA.MarryType;
                this.m_BizC.Fileds34 = this.m_PerA.MarryDate;
                this.m_BizC.Fileds03 = this.m_PerA.Nations;
                this.m_BizC.ContactTelA = this.m_PerA.PersonTel;
                this.m_BizC.Fileds04 = this.m_PerA.RegisterType;
                //this.m_BizC.RegAreaCodeA = this.m_PerA.RegisterAreaCode;
                //this.m_BizC.RegAreaNameA = this.m_PerA.RegisterAreaName;
                this.m_BizC.CurAreaCodeA = this.m_PerA.CurrentAreaCode;
                this.m_BizC.CurAreaNameA = this.m_PerA.CurrentAreaName;
                this.m_BizC.Fileds05 = this.m_PerA.WorkUnit;
                //this.m_BizC.Fileds17 = Fileds17;

                /*Ů�������֤�š��������Ա𡢳������¡�����״��������ʱ�䡢���塢��ϵ�绰���������ʡ�������ַ����ס��ַ��������λ���Ƿ��Ƕ�����Ů*/
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
                this.m_BizC.CurAreaCodeB = this.m_PerB.CurrentAreaCode;
                this.m_BizC.CurAreaNameB = this.m_PerB.CurrentAreaName;
                this.m_BizC.Fileds12 = this.m_PerB.WorkUnit;
                //this.m_BizC.Fileds16 = Fileds16;
                //��Ů��
                this.m_BizC.Fileds07 = this.m_PerB.BirthNum;

                this.m_BizC.Fileds49 = CidTypeA;
                this.m_BizC.Fileds50 = CidTypeB;
                //�������� ��������
                this.m_BizC.Fileds18 = Fileds18;
                int cm = this.m_BizC.Update();
                this.m_BizC = null;

                this.m_Children.Update(m_ObjID, iBirthNum.ToString());
                string objID = this.m_ObjID;
                if (cm > 0)
                {
                    CommPage.SetBizLog(objID, m_UserID, "ҵ���޸�", "����Ա�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " �����ˡ�" + this.m_NavTitle + "���޸Ĳ���");
                    /*���£�1.�ж�Ⱥ�ڻ��������Ƿ���ڣ������ڲ��룬���ڸ���  --�ж����ݣ����֤��
                            2.�жϻ��������Ƿ���ڸ��û���Ϣ�������ڲ��룬���ڲ�����  --�ж����ݣ�Ⱥ�ڱ��
                            3.�ж���Ů�����Ƿ���ڸ��û����м�ͥ��Ů��Ϣ�������ڲ��룬���ڲ�����  --�ж����ݣ�Ⱥ�ڱ��
                     */
                    #region �������
                    //

                    this.m_PerA.MateName = this.m_PerB.PersonName;
                    this.m_PerA.MateCardID = this.m_PerB.PersonCardID;
                    string PersonIDA = this.m_PerA.ExecBizPersons();

                    this.m_PerB.MateName = this.m_PerA.PersonName;
                    this.m_PerB.MateCardID = this.m_PerA.PersonCardID;
                    string PersonIDB = this.m_PerB.ExecBizPersons();
                    //�޸���Ů��Ϣ
                    //string personID = string.Empty;
                    //if (app == "A") { personID = PersonIDA; }
                    //else { personID = PersonIDB; }
                    this.m_Children.Inser(PersonIDB, objID, iBirthNum.ToString());
                    #endregion
                    //if (!String.IsNullOrEmpty(PersonPhotos[1]))
                    //{
                    //    if (!String.IsNullOrEmpty(this.txtIsHasPhotos.Value)) { BIZ_Common.UpdateBizDocs(objID, PersonIDB, "", "2������ڷ򸾺�Ӱ��Ƭ", PersonPhotos); }
                    //    else { BIZ_Common.InsetBizDocs(objID, PersonIDB, "2������ڷ򸾺�Ӱ��Ƭ", PersonPhotos, "1"); }
                    //}

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

