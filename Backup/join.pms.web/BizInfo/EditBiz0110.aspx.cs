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
    public partial class EditBiz0110 : System.Web.UI.Page
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
        private string m_AreaName;
        private BIZ_Persons m_PerA;//�����з���Ϣ
        private BIZ_Persons m_PerB;//����Ů����Ϣ
        private BIZ_Contents m_BizC;//ҵ����Ϣ
        private BIZ_PersonChildren m_Children;//��Ů��Ϣ
        /// <summary>
        /// ҳ����ڻ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();
            //this.txtAreaSelRegNameA.ReadOnly = true;
            //this.txtAreaSelRegNameB.ReadOnly = true;
            if (!IsPostBack)
            {

                SetOpratetionAction(m_NavTitle);
            }
        }
        /// <summary>
        /// ҳ����ʽ
        /// </summary>
        /// <param name="userID"></param>
        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "";//  DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
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
                case "view": // �鿴
                    funcName = "�鿴����";
                    ShowModInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">������ҳ</a> &gt;&gt; ҵ����� &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }
        /// <summary>
        /// �޸�
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
                    /*���ˣ��������Ա����塢�������ʡ����֤�š���ϵ�绰������״����������ַ����ס��ַ���������¡��ʼ�*/
                    this.txtPersonNameA.Text = this.m_BizC.Fileds01;
                    string NationsA = this.m_BizC.Fileds03;
                    this.ddlMarryTypeA.SelectedValue = this.m_BizC.Fileds33;
                    this.txtPersonCidA.Text = this.m_BizC.PersonCidA;
                    this.txtContactTelA.Text = this.m_BizC.ContactTelA;
                    this.ddlRegisterTypeA.SelectedValue = this.m_BizC.Fileds04;
                    this.txtAreaSelRegNameA.Text = this.m_BizC.RegAreaNameA;
                    this.txtAreaSelRegCodeA.Value = this.m_BizC.RegAreaCodeA;
                    this.UcAreaSelCurA.SetAreaName(this.m_BizC.CurAreaNameA);
                    this.UcAreaSelCurA.SetAreaCode(this.m_BizC.CurAreaCodeA);
                    this.txtMail.Text = this.m_BizC.Fileds45;

                    if (this.m_BizC.Fileds33 == "����" || this.m_BizC.Fileds33 == "�ٻ�")
                    {
                        /*��ż���������Ա����塢�������ʡ����֤�š���ϵ�绰������״����������ַ����ס��ַ����������*/
                        this.txtPersonNameB.Text = this.m_BizC.Fileds08;
                        string NationsB = this.m_BizC.Fileds10;
                        this.txtPersonCidB.Text = this.m_BizC.PersonCidB;
                        this.txtContactTelB.Text = this.m_BizC.ContactTelB;
                        this.ddlMarryTypeB.SelectedValue = this.m_BizC.Fileds13;
                        this.ddlRegisterTypeB.SelectedValue = this.m_BizC.Fileds11;
                        this.UcAreaSelRegB.SetAreaName(this.m_BizC.RegAreaNameB);
                        this.UcAreaSelRegB.SetAreaCode(this.m_BizC.RegAreaCodeB);
                        this.UcAreaSelCurB.SetAreaName(this.m_BizC.CurAreaNameB);
                        this.UcAreaSelCurB.SetAreaCode(this.m_BizC.CurAreaCodeB);
                        GetParam(NationsA, NationsB);
                    }
                    GetParam(NationsA, "");

                    //�������� ��������
                    this.txtFileds18.Text = this.m_BizC.Fileds18;

                    //��Ů��Ϣ
                    string BirthNum = this.m_BizC.Fileds07;
                    this.ddlBirthNum.SelectedValue = BirthNum;

                    this.m_Children.Select("", objID);

                    this.txtChildName1.Text = this.m_Children.ChildName1;
                    this.ddlChildSex1.SelectedValue = this.m_Children.ChildSex1;
                    this.txtChildBirthday1.Value = this.m_Children.ChildBirthday1;
                    this.ddlChildPolicy1.SelectedValue = this.m_Children.ChildPolicy1;
                    this.txtChildCardID1.Text = this.m_Children.ChildCardID1;
                    this.ddlChildSource1.SelectedValue = this.m_Children.ChildSource1;
                    this.ddlMemos1.SelectedValue = this.m_Children.Memos1;
                    this.txtChildID1.Value = this.m_Children.CommID1;

                    this.txtChildName2.Text = this.m_Children.ChildName2;
                    this.ddlChildSex2.SelectedValue = this.m_Children.ChildSex2;
                    this.txtChildBirthday2.Value = this.m_Children.ChildBirthday2;
                    this.txtChildCardID2.Text = this.m_Children.ChildCardID2;
                    this.ddlChildPolicy2.SelectedValue = this.m_Children.ChildPolicy2;
                    this.ddlChildSource2.SelectedValue = this.m_Children.ChildSource2;
                    this.ddlMemos2.SelectedValue = this.m_Children.Memos2;
                    this.txtChildID2.Value = this.m_Children.CommID2;

                    this.txtChildName3.Text = this.m_Children.ChildName3;
                    this.ddlChildSex3.SelectedValue = this.m_Children.ChildSex3;
                    this.txtChildBirthday3.Value = this.m_Children.ChildBirthday3;
                    this.txtChildCardID3.Text = this.m_Children.ChildCardID3;
                    this.ddlChildPolicy3.SelectedValue = this.m_Children.ChildPolicy3;
                    this.ddlChildSource3.SelectedValue = this.m_Children.ChildSource3;
                    this.ddlMemos3.SelectedValue = this.m_Children.Memos3;
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

            this.m_PerA = new BIZ_Persons();
            this.m_PerB = new BIZ_Persons();
            this.m_BizC = new BIZ_Contents();
            this.m_Children = new BIZ_PersonChildren();
            string app = string.Empty;

            string bizAttribs = this.txtAttribs.Value;
            if (String.IsNullOrEmpty(bizAttribs)) strErr += "ҵ��״̬ȱʧ�������ԣ�\\n";
            #region ����

            /*���ˣ��������Ա����塢�������ʡ����֤�š���ϵ�绰������״����������ַ����ס��ַ���������¡��ʼ�*/
            this.m_PerA.PersonName = PageValidate.GetTrim(this.txtPersonNameA.Text);
            this.m_PerA.Nations = PageValidate.GetTrim(Request["txtNationsA"]);
            this.m_PerA.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeA.Text);
            this.m_PerA.PersonCardID = PageValidate.GetTrim(this.txtPersonCidA.Text);
            this.m_PerA.PersonSex = CommBiz.GetSexByID(this.txtPersonCidA.Text);
            this.m_PerA.PersonTel = PageValidate.GetTrim(this.txtContactTelA.Text);
            this.m_PerA.MarryType = PageValidate.GetTrim(this.ddlMarryTypeA.SelectedValue);
            //this.m_PerA.RegisterAreaCode = this.txtAreaSelRegCodeA.Value;
            this.m_PerA.RegisterAreaName = this.txtAreaSelRegNameA.Text;
            //this.m_PerA.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
            this.m_PerA.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaName());

            if (this.m_PerA.RegisterAreaName == "����")
            {
                this.m_PerA.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
                this.m_PerA.RegisterAreaCode = this.m_PerA.CurrentAreaCode;
            }
            else if (this.m_PerA.CurrentAreaName == "����")
            {
                this.m_PerA.RegisterAreaCode = this.txtAreaSelRegCodeA.Value;
                this.m_PerA.CurrentAreaCode = this.m_PerA.RegisterAreaCode;
            }
            else
            {
                this.m_PerA.RegisterAreaCode = this.txtAreaSelRegCodeA.Value;
                this.m_PerA.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
            }


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
                this.m_PerB.MarryType = PageValidate.GetTrim(this.ddlMarryTypeB.SelectedValue);
                //this.m_PerB.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaCode());
                this.m_PerB.RegisterAreaName = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaName());
                //this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
                this.m_PerB.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaName());
                this.m_PerB.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerB.PersonCardID);

                if (this.m_PerB.RegisterAreaName == "����")
                {
                    this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
                    this.m_PerB.RegisterAreaCode = this.m_PerB.CurrentAreaCode;
                }
                else if (this.m_PerB.CurrentAreaName == "����")
                {
                    this.m_PerB.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaCode());
                    this.m_PerB.CurrentAreaCode = this.m_PerB.RegisterAreaCode;
                }
                else
                {
                    this.m_PerB.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaCode());
                    this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
                }

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
                #region update
                this.m_BizC.BizID = this.m_ObjID;

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
                int cm = this.m_BizC.Update();
                this.m_BizC = null;

                string objID = this.m_ObjID;
                if (cm > 0)
                {
                    //ҵ����־
                    CommPage.SetBizLog(objID, m_UserID, "ҵ���޸�", "����Ա�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " �����ˡ�" + this.m_NavTitle + "���޸Ĳ���");

                    /*���£�1.�ж�Ⱥ�ڻ��������Ƿ���ڣ������ڲ��룬���ڸ���  --�ж����ݣ����֤��
                            2.�жϻ��������Ƿ���ڸ��û���Ϣ�������ڲ��룬���ڲ�����  --�ж����ݣ�Ⱥ�ڱ��
                            3.�ж���Ů�����Ƿ���ڸ��û����м�ͥ��Ů��Ϣ�������ڲ��룬���ڲ�����  --�ж����ݣ�Ⱥ�ڱ��
                     */
                    #region �������
                    //========��֤�� 
                    this.m_PerA.MateName = this.m_PerB.PersonName;
                    this.m_PerA.MateCardID = this.m_PerB.PersonCardID;
                    string PersonIDA = this.m_PerA.ExecBizPersons();
                    //========��ż
                    this.m_PerB.MateName = this.m_PerA.PersonName;
                    this.m_PerB.MateCardID = this.m_PerA.PersonCardID;
                    string PersonIDB = this.m_PerB.ExecBizPersons();

                    m_PerA = null;
                    m_PerB = null;

                    //������Ů��Ϣ
                    this.m_Children.Inser(PersonIDA, objID, iBirthNum.ToString());
                    #endregion
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
                return;
            }
        }
                #endregion
    }
}
