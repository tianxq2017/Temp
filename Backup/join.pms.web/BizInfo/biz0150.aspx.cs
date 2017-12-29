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
    public partial class biz0150 : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            //������Ĭ�ϵ�ǰѡ�����
            //this.UcAreaSelRegA.SetAreaCode(m_AreaCode);
            //this.UcAreaSelRegA.SetAreaName(GetAreaName(m_AreaCode, "1"));
            //this.txtAreaSelRegCodeB.Value = m_AreaCode;
            //this.txtAreaSelRegNameB.Text = BIZ_Common.GetAreaName(m_AreaCode, "1");
            //this.txtAreaSelRegNameB.ReadOnly = true;

            if (!IsPostBack)
            {
                //������Ĭ�ϵ�ǰѡ�����
                this.UcAreaSelRegA.SetAreaCode(m_AreaCode);
                this.UcAreaSelRegA.SetAreaName(BIZ_Common.GetAreaName(m_AreaCode, "1"));
                this.UcAreaSelRegB.SetAreaCode(m_AreaCode);
                this.UcAreaSelRegB.SetAreaName(BIZ_Common.GetAreaName(m_AreaCode, "1"));
                //ȡ֤�ص�
                //this.UcAreaSelXian1.SetAreaCode(m_AreaCode);
                //SetPageStyle(m_UserID);
                SetOpratetionAction(m_NavTitle);
            }
            GetParam(this.txtHNationsA.Value, this.txtHNationsB.Value);
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
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">������ҳ</a> &gt;&gt; ҵ����� &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
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

            #region ����
            string Fileds14 = CommBiz.GetTrim(this.txtFileds14.Value);
            string Fileds47 = CommBiz.GetTrim(this.ddlFileds47.Text);
            string Fileds23 = CommBiz.GetTrim(this.ddlFileds23.SelectedValue);
            string Fileds24 = CommBiz.GetTrim(this.ddlFileds24.SelectedValue);
            if (String.IsNullOrEmpty(Fileds14)) { strErr += "��ѡ����ʱ�䣡\\n"; }
            if (String.IsNullOrEmpty(Fileds47)) { strErr += "��ѡ����֤�ţ�\\n"; }
            /*Ů�����������������¡����֤�š����塢������λ��������ַ���־�ס�ء�����״������ϵ�绰*/
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
            this.m_PerB.PersonTel = PageValidate.GetTrim(this.txtContactTelA.Text);

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
            /*�з����������������¡����֤�š����塢������λ��������ַ���־�ס�ء�����״������ϵ�绰*/
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
            CidTypeA = PageValidate.GetTrim(this.ddlCidTypeA.SelectedValue);
            CidTypeB = PageValidate.GetTrim(this.ddlCidTypeB.SelectedValue);
            if (RegisterAreaNameA == "����")
            {
                RegisterAreaCodeA = CurrentAreaCodeA;
            }
            else if (CurrentAreaNameA == "����")
            {
                CurrentAreaCodeA = RegisterAreaCodeA;
            }

            //==========================Ů����Ϣ start========================================== 
            if (String.IsNullOrEmpty(this.m_PerB.PersonName)) { strErr += "������Ů��������\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonCardID)) { strErr += "������Ů��֤�����룡\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerB.PersonCardID) && CidTypeB == "1") { strErr += "Ů�����֤�����󣡣�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.Nations)) { strErr += "��ѡ��Ů�����壡\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.WorkUnit)) { strErr += "������Ů��������λ��\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerB.RegisterAreaCode, "0")) { strErr += "��ѡ��Ů�������أ�\\n"; }
            //if (String.IsNullOrEmpty(PageValidate.GetTrim(Request["UcAreaSelRegB$txtArea"]))) { strErr += "������Ů����������ϸ��Ϣ,�����/���ƺţ�\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerB.CurrentAreaCode, "0")) { strErr += "��ѡ��Ů���־�ס�أ�\\n"; }
            //if (String.IsNullOrEmpty(PageValidate.GetTrim(Request["UcAreaSelCurB$txtArea"]))) { strErr += "������Ů����ס����ϸ��Ϣ,�����/���ƺţ�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryType)) { strErr += "��ѡ��Ů������״����\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonTel)) { strErr += "��������ϵ��ʽ��\\n"; }

            //==========================�з���Ϣ start========================================== 
            if (this.m_PerB.MarryType == "����" || this.m_PerB.MarryType == "�ٻ�" || this.m_PerB.MarryType == "����")
            {
                /*�з����������������¡����֤�š����塢������λ��������ַ���־�ס�ء�����״������ϵ�绰*/
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
                this.m_PerA.MarryDate = this.m_PerB.MarryDate;
                this.m_PerA.MarryCardID = Fileds47;
                this.m_PerA.PersonTel = PersonTelA;

                if (String.IsNullOrEmpty(this.m_PerA.PersonName)) { strErr += "�������з�������\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.PersonCardID)) { strErr += "�������з�֤�����룡\\n"; }
                if (!ValidIDCard.VerifyIDCard(this.m_PerA.PersonCardID) && CidTypeA == "1") { strErr += "�з����֤�����󣡣�\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.Nations)) { strErr += "��ѡ���з����壡\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.WorkUnit)) { strErr += "�������з�������λ��\\n"; }
                if (!CommPage.IsAreaCode(this.m_PerA.RegisterAreaCode, "0")) { strErr += "��ѡ���з������أ�\\n"; }
                //if (String.IsNullOrEmpty(PageValidate.GetTrim(Request["UcAreaSelRegA$txtArea"]))) { strErr += "�������з���������ϸ��Ϣ,�����/���ƺţ�\\n"; }
                if (!CommPage.IsAreaCode(this.m_PerA.CurrentAreaCode, "0")) { strErr += "��ѡ���з��־�ס�أ�\\n"; }
                //if (String.IsNullOrEmpty(PageValidate.GetTrim(Request["UcAreaSelCurA$txtArea"]))) { strErr += "�������з���ס����ϸ��Ϣ,�����/���ƺţ�\\n"; }
                if (String.IsNullOrEmpty(this.m_PerA.MarryType)) { strErr += "��ѡ���з�����״����\\n"; }
                //if (String.IsNullOrEmpty(this.m_PerA.PersonTel)) { strErr += "�������з���ϵ�绰��\\n"; }
                if (this.m_PerA.RegisterAreaCode != m_AreaCode && this.m_PerA.CurrentAreaCode != m_AreaCode && this.m_PerB.RegisterAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "��Ů˫�������ػ��־�ס������Ҫ��һ������ѡ���һ�£�\\n"; }

                if (this.m_PerB.RegisterAreaCode == m_AreaCode || this.m_PerB.CurrentAreaCode == m_AreaCode) { app = "B"; }
                else { app = "A"; }
            }
            else
            {
                //this.m_BizStep = "2"; Ŀǰֻ�����һ�� 2016/03/23 by ysl
                app = "B";
                if (this.m_PerB.RegisterAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "Ů�������ػ��־�ס������Ҫ��һ������ѡ���һ�£�\\n"; }
            }

            
            #endregion

            //����Ƿ��ظ��ύ
            string msg = string.Empty;
            if (CommPage.IsHasBiz(this.m_FuncCode, this.m_PerA.PersonCardID, this.m_PerB.PersonCardID, ref msg)) { strErr += msg; }

            //if (cbOk.Checked == false)
            //{
            //    strErr += "��ȷ�ϳ�ŵ��\\n";
            //}
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            try
            {
                this.m_BizC.BizCode = this.m_FuncCode;
                this.m_BizC.BizName = this.m_NavTitle;
                this.m_BizC.BizStep = this.m_BizStep;
                this.m_BizC.CurrentStep = "1";
                this.m_BizC.AdminUserID = this.m_UserID;
                this.m_BizC.AddressID = "0";
                this.m_BizC.Initiator = this.m_UserName;
                this.m_BizC.InitDirection = "1";

                string SelAreaCode0150 = DbHelperSQL.GetSingle("SELECT TOP 1 UserAreaCode FROM USER_BaseInfo WHERE UserID=" + m_UserID).ToString();
                string SelAreaName0150 = DbHelperSQL.GetSingle("SELECT TOP 1 UserAreaName FROM USER_BaseInfo WHERE UserID=" + m_UserID).ToString();

                this.m_BizC.SelAreaCode = SelAreaCode0150;
                this.m_BizC.SelAreaName = SelAreaName0150;
                this.m_BizC.StartDate = DateTime.Now.ToString();
                this.m_BizC.Attribs = "0";

                this.m_BizC.GetAreaCode = SelAreaCode0150;
                this.m_BizC.GetAreaName = SelAreaName0150;

                /*�з����������������¡����֤�š����塢������λ��������ַ���־�ס�ء�����״������ϵ�绰*/
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

                /*Ů�����������������¡����֤�š����塢������λ��������ַ���־�ס�ء�����״������ϵ�绰*/
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

                this.m_BizC.Fileds14 = this.txtFileds14.Value;


                this.m_BizC.Fileds49 = CidTypeA;
                this.m_BizC.Fileds50 = CidTypeB;

                ////˫������(����)��Ů��
                //this.m_BizC.Fileds37 = ddlBirthNum;
                ///*�������в���Ϣ�����ʱ�䡢����ʱ�䡢Ԥ�����ڡ��������ڡ������Ա𡢳���ҽѧ֤�����*/
                //this.m_BizC.Fileds43 = this.txtFileds43.Value;
                ////this.m_BizC.Fileds44 = this.txtFileds44.Value;
                //this.m_BizC.Fileds44 = "";
                //this.m_BizC.Fileds44 = "";
                //this.m_BizC.Fileds18 = this.txtFileds18.Value;
                //this.m_BizC.Fileds17 = this.ddlChildSex.SelectedValue;
                ////this.m_BizC.Fileds45 = this.txtFileds45.Text;
                //this.m_BizC.Fileds45 = "";

                //this.m_BizC.Fileds46 = Fileds46;
                this.m_BizC.Fileds47 = Fileds47;

                string objID = m_BizC.Insert();
                m_BizC = null;
                if (!string.IsNullOrEmpty(objID) && PageValidate.IsNumber(objID))
                {
                    CommPage.SetBizLog(objID, m_UserID, "ҵ����", "����Ա�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " �����ˡ�" + this.m_NavTitle + "���������");
                    #region һ��˫�����⴦��\һ�����⴦��
                    string perAAreaCode = string.Empty;
                    string perBAreaCode = string.Empty;
                    //if (this.m_PerA.RegisterAreaCode.Substring(0, 4) != "6105" && this.m_PerB.RegisterAreaCode.Substring(0, 4) != "6105")
                    //{
                    //    perAAreaCode = this.m_PerA.CurrentAreaCode;
                    //    perBAreaCode = this.m_PerB.CurrentAreaCode;
                    //}
                    //else if (this.m_PerA.RegisterAreaCode.Substring(0, 4) != "6105")
                    //{
                    //    perAAreaCode = this.m_PerB.RegisterAreaCode;
                    //    perBAreaCode = this.m_PerB.RegisterAreaCode;
                    //}
                    //else if (this.m_PerB.RegisterAreaCode.Substring(0, 4) != "6105")
                    //{
                    //    perAAreaCode = this.m_PerA.RegisterAreaCode;
                    //    perBAreaCode = this.m_PerA.RegisterAreaCode;
                    //}
                    //else
                    //{
                    //    perAAreaCode = this.m_PerA.RegisterAreaCode;
                    //    perBAreaCode = this.m_PerB.RegisterAreaCode;
                    //}
                    //2016/05/20  μ��һ������ֻ��һ������
                    perAAreaCode = this.m_AreaCode;
                    perBAreaCode = this.m_AreaCode;
                    #endregion


                    if (this.m_BizStep != BIZ_Common.InsetBizWorkFlows(this.m_FuncCode, this.m_BizStep, objID, SelAreaCode0150, SelAreaCode0150, SelAreaCode0150))
                    {
                        MessageBox.Show(this, "�������̱�ʧ�ܣ�����ϵϵͳ����Ա��");
                        return;
                    }
                    /*���£�1.�ж�Ⱥ�ڻ��������Ƿ���ڣ������ڲ��룬���ڸ���  --�ж����ݣ����֤��
                            2.�жϻ��������Ƿ���ڸ��û���Ϣ�������ڲ��룬���ڲ�����  --�ж����ݣ�Ⱥ�ڱ��
                            3.�ж���Ů�����Ƿ���ڸ��û����м�ͥ��Ů��Ϣ�������ڲ��룬���ڲ�����  --�ж����ݣ�Ⱥ�ڱ��
                     */
                    #region �������
                    //�з�
                    this.m_PerA.MateName = this.m_PerB.PersonName;
                    this.m_PerA.MateCardID = this.m_PerB.PersonCardID;
                    string PersonIDA = this.m_PerA.ExecBizPersons();
                    //Ů��
                    this.m_PerB.MateName = this.m_PerA.PersonName;
                    this.m_PerB.MateCardID = this.m_PerA.PersonCardID;
                    string PersonIDB = this.m_PerB.ExecBizPersons();
                    //���Ͷ�Ѷ--start--
                    // ĳĳ,���ύ��[ҵ������]����������
                    string uTel, uName, tMsg = string.Empty;
                    if (!string.IsNullOrEmpty(this.m_PerB.PersonTel)) { uTel = this.m_PerB.PersonTel; uName = this.m_PerB.PersonName; } else { uTel = this.m_PerA.PersonTel; uName = this.m_PerA.PersonName; }
                    tMsg = uName + "�����ύ��[" + this.m_NavTitle + "]����������"; //��̨����
                    if (!string.IsNullOrEmpty(uTel) && !string.IsNullOrEmpty(tMsg))
                    {
                        SendMsg.SendMsgByModem(uTel, tMsg);
                    }
                    //���Ͷ�Ѷ--end--
                    m_PerA = null;
                    m_PerB = null;
                    //������Ů��Ϣ
                    //if (!String.IsNullOrEmpty(PersonIDB))
                    //{
                    //    this.m_Children.Inser(PersonIDB, objID, iBirthNum.ToString());
                    //}
                    //else
                    //{

                    //    this.m_Children.Inser(PersonIDA, objID, iBirthNum.ToString());
                    //}

                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��<" + this.m_NavTitle + ">������Ϣ�ύ�ɹ���������ύ֤��������룡", "BizDocs.aspx?action=add&k=" + objID + "&PersonIDA=" + PersonIDB + "&sourceUrl=" + m_SourceUrl, true, true);
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

