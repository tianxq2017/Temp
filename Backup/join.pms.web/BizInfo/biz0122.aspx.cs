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
    public partial class biz0122 : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                //������Ĭ�ϵ�ǰѡ�����
                this.UcAreaSelRegA.SetAreaCode(m_AreaCode);
                this.UcAreaSelRegA.SetAreaName(BIZ_Common.GetAreaName(m_AreaCode, "1"));
                this.UcAreaSelRegB.SetAreaCode(m_AreaCode);
                this.UcAreaSelRegB.SetAreaName(BIZ_Common.GetAreaName(m_AreaCode, "1"));
                //ȡ֤�ص�
                this.UcAreaSelXian1.SetAreaCode(m_AreaCode);

                //SetPageStyle(m_UserID);
                SetOpratetionAction(m_NavTitle);
            }
            //GetParam(this.txtHNationsA.Value, this.txtHNationsB.Value);
            GetParam(PageValidate.GetTrim(Request["txtNationsA"]), PageValidate.GetTrim(Request["txtNationsB"]));
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

        #region ������֤����������
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
                //m_NavTitle = HttpUtility.UrlDecode(StringProcess.AnalysisParas(m_SourceUrlDec, "FuncNa"));
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
            string QcfwzBm_f = CommPage.QcfwzBm_fun(PageValidate.GetTrim(this.txtfwzh.Text));
            if (!string.IsNullOrEmpty(QcfwzBm_f)) { strErr += QcfwzBm_f; }
            /*Ů��������֤�š��������Ա𡢳������¡�����״��������ʱ�䡢���塢��ϵ�绰���������ʡ�������ַ����ס��ַ��������λ���Ƿ��Ƕ�����Ů*/
            this.m_PerB.PersonCardID = PageValidate.GetTrim(this.txtPersonCidB.Text);
            this.m_PerB.PersonName = PageValidate.GetTrim(this.txtPersonNameB.Text);
            this.m_PerB.PersonSex = "Ů";
            this.m_PerB.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerB.PersonCardID);
            this.m_PerB.MarryType = PageValidate.GetTrim(this.ddlMarryType.SelectedValue);
            this.m_PerB.MarryDate = CommBiz.GetTrim(this.txtMarryDate.Value);
            this.m_PerB.Nations = PageValidate.GetTrim(Request["txtNationsB"]);
            this.m_PerB.PersonTel = PageValidate.GetTrim(this.txtContactTelB.Text);
            this.m_PerB.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeB.Text);
            //this.m_PerB.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaCode());
            this.m_PerB.RegisterAreaName = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaName());
            //this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
            this.m_PerB.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaName());

            CidTypeA = PageValidate.GetTrim(this.ddlCidTypeA.SelectedValue);
            CidTypeB = PageValidate.GetTrim(this.ddlCidTypeB.SelectedValue);
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



            this.m_PerB.WorkUnit = PageValidate.GetTrim(this.txtWorkUnitB.Text);
            string Fileds16 = PageValidate.GetTrim(this.ddlFileds16.SelectedValue);
            this.m_PerB.BirthNum = PageValidate.GetTrim(this.ddlBirthNum.SelectedValue);

            /*�з�������֤�š��������Ա𡢳������¡�����״��������ʱ�䡢���塢��ϵ�绰���������ʡ�������ַ����ס��ַ��������λ���Ƿ��Ƕ�����Ů*/
            this.m_PerA.PersonCardID = PageValidate.GetTrim(this.txtPersonCidA.Text);
            this.m_PerA.PersonName = PageValidate.GetTrim(this.txtPersonNameA.Text);
            this.m_PerA.PersonSex = "��";
            this.m_PerA.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerA.PersonCardID);
            this.m_PerA.MarryType = PageValidate.GetTrim(this.ddlMarryTypeA.SelectedValue);
            this.m_PerA.MarryDate = CommBiz.GetTrim(this.txtMarryDateA.Value);
            this.m_PerA.Nations = PageValidate.GetTrim(Request["txtNationsA"]);
            this.m_PerA.PersonTel = PageValidate.GetTrim(this.txtContactTelA.Text);
            this.m_PerA.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeA.Text);
            //this.m_PerA.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegA.GetAreaCode());
            this.m_PerA.RegisterAreaName = PageValidate.GetTrim(this.UcAreaSelRegA.GetAreaName());
            //this.m_PerA.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
            this.m_PerA.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaName());
            CidTypeA = PageValidate.GetTrim(this.ddlCidTypeA.SelectedValue);
            CidTypeB = PageValidate.GetTrim(this.ddlCidTypeB.SelectedValue);
            if (this.m_PerA.RegisterAreaName == "����")
            {
                this.m_PerA.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
                this.m_PerA.RegisterAreaCode = this.m_PerA.CurrentAreaCode;
            }
            else if (this.m_PerA.CurrentAreaName == "����")
            {
                this.m_PerA.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegA.GetAreaCode());
                this.m_PerA.CurrentAreaCode = this.m_PerA.RegisterAreaCode;
            }
            else
            {
                this.m_PerA.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegA.GetAreaCode());
                this.m_PerA.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
            }


            this.m_PerA.WorkUnit = PageValidate.GetTrim(this.txtWorkUnitA.Text);
            string Fileds17 = PageValidate.GetTrim(this.ddlFileds17.SelectedValue);
            string Fileds36 = CommBiz.GetTrim(this.txtFinalYjDate.Value);

            //==========================Ů����Ϣ start========================================== 
            if (String.IsNullOrEmpty(this.m_PerB.PersonCardID)) { strErr += "������Ů������֤�ţ�\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerB.PersonCardID) && CidTypeB == "1") { strErr += "Ů������֤�����󣡣�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonName)) { strErr += "������Ů��������\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryType)) { strErr += "��ѡ��Ů������״����\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryDate)) { strErr += "��ѡ��Ů������ʱ�䣡\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.Nations)) { strErr += "������Ů�����壡\\n"; }
            //if (String.IsNullOrEmpty(Fileds16)) { strErr += "��ѡ��Ů���Ƿ��Ƕ�����Ů��\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerB.RegisterAreaCode, "0")) { strErr += "��ѡ��Ů�������أ�\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerB.CurrentAreaCode, "0")) { strErr += "��ѡ��Ů���־�ס�أ�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonTel)) { strErr += "������Ů����ϵ�绰��\\n"; }

            //==========================�з���Ϣ start========================================== 
            if (String.IsNullOrEmpty(this.m_PerA.PersonCardID)) { strErr += "�������з�����֤�ţ�\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerA.PersonCardID) && CidTypeA == "1") { strErr += "�з�����֤�����󣡣�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.PersonName)) { strErr += "�������з�������\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.MarryType)) { strErr += "��ѡ���з�����״����\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.MarryDate)) { strErr += "��ѡ���з�����ʱ�䣡\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.Nations)) { strErr += "�������з����壡\\n"; }
            //if (String.IsNullOrEmpty(Fileds17)) { strErr += "��ѡ���з��Ƿ��Ƕ�����Ů��\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerA.RegisterAreaCode, "0")) { strErr += "��ѡ���з������أ�\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerA.CurrentAreaCode, "0")) { strErr += "��ѡ���з��־�ס�أ�\\n"; }
            if (this.m_PerA.RegisterAreaCode != m_AreaCode && this.m_PerA.CurrentAreaCode != m_AreaCode && this.m_PerB.RegisterAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "��Ů˫�������ػ��־�ס������Ҫ��һ������ѡ���һ�£�\\n"; }

            //��Ů��Ϣ
            int iBirthNum = int.Parse(this.m_PerB.BirthNum);
            #region ��Ů��Ϣ
            string ChildName1 = PageValidate.GetTrim(this.txtChildName1.Text);
            string ChildSex1 = PageValidate.GetTrim(this.ddlChildSex1.SelectedValue);
            string ChildBirthday1 = CommBiz.GetTrim(this.txtChildBirthday1.Value);
            string ChildCardID1 = PageValidate.GetTrim(this.txtChildCardID1.Text);
            string ChildFatherName1 = PageValidate.GetTrim(this.txtFatherName1.Text);
            string ChildMatherName1 = PageValidate.GetTrim(this.txtMotherName1.Text);
            string ChildPolicy1 = PageValidate.GetTrim(this.ddlChildPolicy1.Text);
            string ChildMemos1 = PageValidate.GetTrim(this.txtMemos1.Text);
            string ChildNo1 = "1";

            string ChildName2 = PageValidate.GetTrim(this.txtChildName2.Text);
            string ChildSex2 = PageValidate.GetTrim(this.ddlChildSex2.SelectedValue);
            string ChildBirthday2 = CommBiz.GetTrim(this.txtChildBirthday2.Value);
            string ChildCardID2 = PageValidate.GetTrim(this.txtChildCardID2.Text);
            string ChildFatherName2 = PageValidate.GetTrim(this.txtFatherName2.Text);
            string ChildMatherName2 = PageValidate.GetTrim(this.txtMotherName2.Text);
            string ChildPolicy2 = PageValidate.GetTrim(this.ddlChildPolicy2.Text);
            string ChildMemos2 = PageValidate.GetTrim(this.txtMemos2.Text);
            string ChildNo2 = "2";

            string ChildName3 = PageValidate.GetTrim(this.txtChildName3.Text);
            string ChildSex3 = PageValidate.GetTrim(this.ddlChildSex3.SelectedValue);
            string ChildBirthday3 = CommBiz.GetTrim(this.txtChildBirthday3.Value);
            string ChildCardID3 = PageValidate.GetTrim(this.txtChildCardID3.Text);
            string ChildFatherName3 = PageValidate.GetTrim(this.txtFatherName3.Text);
            string ChildMatherName3 = PageValidate.GetTrim(this.txtMotherName3.Text);
            string ChildPolicy3 = PageValidate.GetTrim(this.ddlChildPolicy3.Text);
            string ChildMemos3 = PageValidate.GetTrim(this.txtMemos3.Text);
            string ChildNo3 = "3";

            string ChildName4 = PageValidate.GetTrim(this.txtChildName4.Text);
            string ChildSex4 = PageValidate.GetTrim(this.ddlChildSex4.SelectedValue);
            string ChildBirthday4 = CommBiz.GetTrim(this.txtChildBirthday4.Value);
            string ChildCardID4 = PageValidate.GetTrim(this.txtChildCardID4.Text);
            string ChildFatherName4 = PageValidate.GetTrim(this.txtFatherName4.Text);
            string ChildMatherName4 = PageValidate.GetTrim(this.txtMotherName4.Text);
            string ChildPolicy4 = PageValidate.GetTrim(this.ddlChildPolicy4.Text);
            string ChildMemos4 = PageValidate.GetTrim(this.txtMemos4.Text);
            string ChildNo4 = "4";

            string ChildName5 = PageValidate.GetTrim(this.txtChildName5.Text);
            string ChildSex5 = PageValidate.GetTrim(this.ddlChildSex5.SelectedValue);
            string ChildBirthday5 = CommBiz.GetTrim(this.txtChildBirthday5.Value);
            string ChildCardID5 = PageValidate.GetTrim(this.txtChildCardID5.Text);
            string ChildFatherName5 = PageValidate.GetTrim(this.txtFatherName5.Text);
            string ChildMatherName5 = PageValidate.GetTrim(this.txtMotherName5.Text);
            string ChildPolicy5 = PageValidate.GetTrim(this.ddlChildPolicy5.Text);
            string ChildMemos5 = PageValidate.GetTrim(this.txtMemos5.Text);
            string ChildNo5 = "5";

            string ChildName6 = PageValidate.GetTrim(this.txtChildName6.Text);
            string ChildSex6 = PageValidate.GetTrim(this.ddlChildSex6.SelectedValue);
            string ChildBirthday6 = CommBiz.GetTrim(this.txtChildBirthday6.Value);
            string ChildCardID6 = PageValidate.GetTrim(this.txtChildCardID6.Text);
            string ChildFatherName6 = PageValidate.GetTrim(this.txtFatherName6.Text);
            string ChildMatherName6 = PageValidate.GetTrim(this.txtMotherName6.Text);
            string ChildPolicy6 = PageValidate.GetTrim(this.ddlChildPolicy6.Text);
            string ChildMemos6 = PageValidate.GetTrim(this.txtMemos6.Text);
            string ChildNo6 = "6";
            ArrayList ChildName = new ArrayList();
            string[] ChildNames = null;
            if (iBirthNum == 1)
            {
                ChildName2 = ""; ChildSex2 = ""; ChildBirthday2 = ""; ChildCardID2 = ""; ChildFatherName2 = ""; ChildMatherName2 = ""; ChildPolicy2 = ""; ChildMemos2 = "";
                ChildName3 = ""; ChildSex3 = ""; ChildBirthday3 = ""; ChildCardID3 = ""; ChildFatherName3 = ""; ChildMatherName3 = ""; ChildPolicy3 = ""; ChildMemos3 = "";
                ChildName4 = ""; ChildSex4 = ""; ChildBirthday4 = ""; ChildCardID4 = ""; ChildFatherName4 = ""; ChildMatherName4 = ""; ChildPolicy4 = ""; ChildMemos4 = "";
                ChildName5 = ""; ChildSex5 = ""; ChildBirthday5 = ""; ChildCardID5 = ""; ChildFatherName5 = ""; ChildMatherName5 = ""; ChildPolicy5 = ""; ChildMemos5 = "";
                ChildName6 = ""; ChildSex6 = ""; ChildBirthday6 = ""; ChildCardID6 = ""; ChildFatherName6 = ""; ChildMatherName6 = ""; ChildPolicy6 = ""; ChildMemos6 = "";
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName1)) { strErr += "��ѡ����Ů1����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName1)) { strErr += "��ѡ����Ů1ĸ��������\\n"; }
            }
            else if (iBirthNum == 2)
            {
                ChildName3 = ""; ChildSex3 = ""; ChildBirthday3 = ""; ChildCardID3 = ""; ChildFatherName3 = ""; ChildMatherName3 = ""; ChildPolicy3 = ""; ChildMemos3 = "";
                ChildName4 = ""; ChildSex4 = ""; ChildBirthday4 = ""; ChildCardID4 = ""; ChildFatherName4 = ""; ChildMatherName4 = ""; ChildPolicy4 = ""; ChildMemos4 = "";
                ChildName5 = ""; ChildSex5 = ""; ChildBirthday5 = ""; ChildCardID5 = ""; ChildFatherName5 = ""; ChildMatherName5 = ""; ChildPolicy5 = ""; ChildMemos5 = "";
                ChildName6 = ""; ChildSex6 = ""; ChildBirthday6 = ""; ChildCardID6 = ""; ChildFatherName6 = ""; ChildMatherName6 = ""; ChildPolicy6 = ""; ChildMemos6 = "";

                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName1)) { strErr += "��ѡ����Ů1����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName1)) { strErr += "��ѡ����Ů1ĸ��������\\n"; }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "��������Ů2������\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "��ѡ����Ů2�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "��ѡ����Ů2�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName2)) { strErr += "��ѡ����Ů2����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName2)) { strErr += "��ѡ����Ů2ĸ��������\\n"; }


                ChildName.Add(ChildName1);
                ChildName.Add(ChildName2);
                ChildNames = (string[])ChildName.ToArray(typeof(string));
                if (CommPage.IsRepeat(ChildNames)) { strErr += "��Ů��������Ϊ�ջ��ظ���\\n"; }
            }
            else if (iBirthNum == 3)
            {
                ChildName4 = ""; ChildSex4 = ""; ChildBirthday4 = ""; ChildCardID4 = ""; ChildFatherName4 = ""; ChildMatherName4 = ""; ChildPolicy4 = ""; ChildMemos4 = "";
                ChildName5 = ""; ChildSex5 = ""; ChildBirthday5 = ""; ChildCardID5 = ""; ChildFatherName5 = ""; ChildMatherName5 = ""; ChildPolicy5 = ""; ChildMemos5 = "";
                ChildName6 = ""; ChildSex6 = ""; ChildBirthday6 = ""; ChildCardID6 = ""; ChildFatherName6 = ""; ChildMatherName6 = ""; ChildPolicy6 = ""; ChildMemos6 = "";
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName1)) { strErr += "��ѡ����Ů1����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName1)) { strErr += "��ѡ����Ů1ĸ��������\\n"; }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "��������Ů2������\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "��ѡ����Ů2�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "��ѡ����Ů2�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName2)) { strErr += "��ѡ����Ů2����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName2)) { strErr += "��ѡ����Ů2ĸ��������\\n"; }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "��������Ů3������\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "��ѡ����Ů3�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "��ѡ����Ů3�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName3)) { strErr += "��ѡ����Ů3����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName3)) { strErr += "��ѡ����Ů3ĸ��������\\n"; }

                ChildName.Add(ChildName1);
                ChildName.Add(ChildName2);
                ChildName.Add(ChildName3);
                ChildNames = (string[])ChildName.ToArray(typeof(string));
                if (CommPage.IsRepeat(ChildNames)) { strErr += "��Ů��������Ϊ�ջ��ظ���\\n"; }
            }
            else if (iBirthNum == 4)
            {
                ChildName5 = ""; ChildSex5 = ""; ChildBirthday5 = ""; ChildCardID5 = ""; ChildFatherName5 = ""; ChildMatherName5 = ""; ChildPolicy5 = ""; ChildMemos5 = "";
                ChildName6 = ""; ChildSex6 = ""; ChildBirthday6 = ""; ChildCardID6 = ""; ChildFatherName6 = ""; ChildMatherName6 = ""; ChildPolicy6 = ""; ChildMemos6 = "";
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName1)) { strErr += "��ѡ����Ů1����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName1)) { strErr += "��ѡ����Ů1ĸ��������\\n"; }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "��������Ů2������\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "��ѡ����Ů2�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "��ѡ����Ů2�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName2)) { strErr += "��ѡ����Ů2����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName2)) { strErr += "��ѡ����Ů2ĸ��������\\n"; }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "��������Ů3������\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "��ѡ����Ů3�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "��ѡ����Ů3�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName3)) { strErr += "��ѡ����Ů3����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName3)) { strErr += "��ѡ����Ů3ĸ��������\\n"; }

                if (String.IsNullOrEmpty(ChildName4)) { strErr += "��������Ů4������\\n"; }
                if (String.IsNullOrEmpty(ChildSex4)) { strErr += "��ѡ����Ů4�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday4)) { strErr += "��ѡ����Ů4�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName4)) { strErr += "��ѡ����Ů4����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName4)) { strErr += "��ѡ����Ů4ĸ��������\\n"; }

                ChildName.Add(ChildName1);
                ChildName.Add(ChildName2);
                ChildName.Add(ChildName3);
                ChildName.Add(ChildName4);
                ChildNames = (string[])ChildName.ToArray(typeof(string));
                if (CommPage.IsRepeat(ChildNames)) { strErr += "��Ů��������Ϊ�ջ��ظ���\\n"; }
            }
            else if (iBirthNum == 5)
            {
                ChildName6 = ""; ChildSex6 = ""; ChildBirthday6 = ""; ChildCardID6 = ""; ChildFatherName6 = ""; ChildMatherName6 = ""; ChildPolicy6 = ""; ChildMemos6 = "";
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName1)) { strErr += "��ѡ����Ů1����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName1)) { strErr += "��ѡ����Ů1ĸ��������\\n"; }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "��������Ů2������\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "��ѡ����Ů2�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "��ѡ����Ů2�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName2)) { strErr += "��ѡ����Ů2����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName2)) { strErr += "��ѡ����Ů2ĸ��������\\n"; }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "��������Ů3������\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "��ѡ����Ů3�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "��ѡ����Ů3�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName3)) { strErr += "��ѡ����Ů3����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName3)) { strErr += "��ѡ����Ů3ĸ��������\\n"; }

                if (String.IsNullOrEmpty(ChildName4)) { strErr += "��������Ů4������\\n"; }
                if (String.IsNullOrEmpty(ChildSex4)) { strErr += "��ѡ����Ů4�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday4)) { strErr += "��ѡ����Ů4�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName4)) { strErr += "��ѡ����Ů4����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName4)) { strErr += "��ѡ����Ů4ĸ��������\\n"; }

                if (String.IsNullOrEmpty(ChildName5)) { strErr += "��������Ů5������\\n"; }
                if (String.IsNullOrEmpty(ChildSex5)) { strErr += "��ѡ����Ů5�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday5)) { strErr += "��ѡ����Ů5�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName5)) { strErr += "��ѡ����Ů5����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName5)) { strErr += "��ѡ����Ů5ĸ��������\\n"; }

                ChildName.Add(ChildName1);
                ChildName.Add(ChildName2);
                ChildName.Add(ChildName3);
                ChildName.Add(ChildName4);
                ChildName.Add(ChildName5);
                ChildNames = (string[])ChildName.ToArray(typeof(string));
                if (CommPage.IsRepeat(ChildNames)) { strErr += "��Ů��������Ϊ�ջ��ظ���\\n"; }
            }
            else if (iBirthNum == 6)
            {
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName1)) { strErr += "��ѡ����Ů1����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName1)) { strErr += "��ѡ����Ů1ĸ��������\\n"; }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "��������Ů2������\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "��ѡ����Ů2�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "��ѡ����Ů2�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName2)) { strErr += "��ѡ����Ů2����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName2)) { strErr += "��ѡ����Ů2ĸ��������\\n"; }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "��������Ů3������\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "��ѡ����Ů3�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "��ѡ����Ů3�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName3)) { strErr += "��ѡ����Ů3����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName3)) { strErr += "��ѡ����Ů3ĸ��������\\n"; }

                if (String.IsNullOrEmpty(ChildName4)) { strErr += "��������Ů4������\\n"; }
                if (String.IsNullOrEmpty(ChildSex4)) { strErr += "��ѡ����Ů4�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday4)) { strErr += "��ѡ����Ů4�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName4)) { strErr += "��ѡ����Ů4����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName4)) { strErr += "��ѡ����Ů4ĸ��������\\n"; }

                if (String.IsNullOrEmpty(ChildName5)) { strErr += "��������Ů5������\\n"; }
                if (String.IsNullOrEmpty(ChildSex5)) { strErr += "��ѡ����Ů5�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday5)) { strErr += "��ѡ����Ů5�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName5)) { strErr += "��ѡ����Ů5����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName5)) { strErr += "��ѡ����Ů5ĸ��������\\n"; }

                if (String.IsNullOrEmpty(ChildName6)) { strErr += "��������Ů6������\\n"; }
                if (String.IsNullOrEmpty(ChildSex6)) { strErr += "��ѡ����Ů6�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday6)) { strErr += "��ѡ����Ů6�������£�\\n"; }
                if (String.IsNullOrEmpty(ChildFatherName6)) { strErr += "��ѡ����Ů6����������\\n"; }
                if (String.IsNullOrEmpty(ChildMatherName6)) { strErr += "��ѡ����Ů6ĸ��������\\n"; }

                ChildName.Add(ChildName1);
                ChildName.Add(ChildName2);
                ChildName.Add(ChildName3);
                ChildName.Add(ChildName4);
                ChildName.Add(ChildName5);
                ChildName.Add(ChildName6);
                ChildNames = (string[])ChildName.ToArray(typeof(string));
                if (CommPage.IsRepeat(ChildNames)) { strErr += "��Ů��������Ϊ�ջ��ظ���\\n"; }
            }
            this.m_Children.ChildName1 = ChildName1;
            this.m_Children.ChildSex1 = ChildSex1;
            this.m_Children.ChildBirthday1 = ChildBirthday1;
            this.m_Children.ChildCardID1 = ChildCardID1;
            this.m_Children.FatherName1 = ChildFatherName1;
            this.m_Children.MotherName1 = ChildMatherName1;
            this.m_Children.ChildPolicy1 = ChildPolicy1;
            this.m_Children.Memos1 = ChildMemos1;
            this.m_Children.ChildNo1 = ChildNo1;

            this.m_Children.ChildName2 = ChildName2;
            this.m_Children.ChildSex2 = ChildSex2;
            this.m_Children.ChildBirthday2 = ChildBirthday2;
            this.m_Children.ChildCardID2 = ChildCardID2;
            this.m_Children.FatherName2 = ChildFatherName2;
            this.m_Children.MotherName2 = ChildMatherName2;
            this.m_Children.ChildPolicy2 = ChildPolicy2;
            this.m_Children.Memos2 = ChildMemos2;
            this.m_Children.ChildNo2 = ChildNo2;

            this.m_Children.ChildName3 = ChildName3;
            this.m_Children.ChildSex3 = ChildSex3;
            this.m_Children.ChildBirthday3 = ChildBirthday3;
            this.m_Children.ChildCardID3 = ChildCardID3;
            this.m_Children.FatherName3 = ChildFatherName3;
            this.m_Children.MotherName3 = ChildMatherName3;
            this.m_Children.ChildPolicy3 = ChildPolicy3;
            this.m_Children.Memos3 = ChildMemos3;
            this.m_Children.ChildNo3 = ChildNo3;

            this.m_Children.ChildName4 = ChildName4;
            this.m_Children.ChildSex4 = ChildSex4;
            this.m_Children.ChildBirthday4 = ChildBirthday4;
            this.m_Children.ChildCardID4 = ChildCardID4;
            this.m_Children.FatherName4 = ChildFatherName4;
            this.m_Children.MotherName4 = ChildMatherName4;
            this.m_Children.ChildPolicy4 = ChildPolicy4;
            this.m_Children.Memos4 = ChildMemos4;
            this.m_Children.ChildNo4 = ChildNo4;

            this.m_Children.ChildName5 = ChildName5;
            this.m_Children.ChildSex5 = ChildSex5;
            this.m_Children.ChildBirthday5 = ChildBirthday5;
            this.m_Children.ChildCardID5 = ChildCardID5;
            this.m_Children.FatherName5 = ChildFatherName5;
            this.m_Children.MotherName5 = ChildMatherName5;
            this.m_Children.ChildPolicy5 = ChildPolicy5;
            this.m_Children.Memos5 = ChildMemos5;
            this.m_Children.ChildNo5 = ChildNo5;

            this.m_Children.ChildName6 = ChildName6;
            this.m_Children.ChildSex6 = ChildSex6;
            this.m_Children.ChildBirthday6 = ChildBirthday6;
            this.m_Children.ChildCardID6 = ChildCardID6;
            this.m_Children.FatherName6 = ChildFatherName6;
            this.m_Children.MotherName6 = ChildMatherName6;
            this.m_Children.ChildPolicy6 = ChildPolicy6;
            this.m_Children.Memos6 = ChildMemos6;
            this.m_Children.ChildNo6 = ChildNo6;
            #endregion


            /*�������ɣ����� ���� */
            string Fileds18 = PageValidate.GetTrim(this.ddlFileds18.Value);
            if (String.IsNullOrEmpty(Fileds18)) { strErr += "�������������ɣ�\\n"; }

            #endregion

            //ȡ֤�ص�
            string GetAreaCode = PageValidate.GetTrim(this.UcAreaSelXian1.GetAreaCode());
            string GetAreaName = PageValidate.GetTrim(this.UcAreaSelXian1.GetAreaName());
            this.m_BizC.QcfwzBm = PageValidate.GetTrim(this.txtfwzh.Text);

            //2������ڷ򸾺�Ӱ��Ƭ
            //string strMsg = string.Empty;
            //string[] PersonPhotos = new string[3];
            //if (this.txtPersonPhotos.HasFile)
            //{
            //    if (!BIZ_UpFhotos.UploadPhotos(this.txtPersonPhotos,ref PersonPhotos, "0", ref strMsg))
            //    {
            //        strErr += "2������ڷ򸾺�Ӱ��Ƭ�ϴ�ʧ�ܣ�\\n";
            //    }
            //    //else { strErr += strMsg; }
            //}

            //����Ƿ��ظ��ύ
            string msg = string.Empty;
            if (CommPage.IsHasBiz(this.m_FuncCode, this.m_PerA.PersonCardID, this.m_PerB.PersonCardID, ref msg)) { strErr += msg; }

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

                //ȡ֤�ص�
                this.m_BizC.GetAreaCode = GetAreaCode;
                this.m_BizC.GetAreaName = GetAreaName;

                //this.m_BizC.PersonPhotos = PersonPhotos[1];//2������ڷ򸾺�Ӱ��Ƭ

                /*�з�������֤�š��������Ա𡢳������¡�����״��������ʱ�䡢���塢��ϵ�绰���������ʡ�������ַ����ס��ַ��������λ���Ƿ��Ƕ�����Ů*/
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

                /*Ů��������֤�š��������Ա𡢳������¡�����״��������ʱ�䡢���塢��ϵ�绰���������ʡ�������ַ����ס��ַ��������λ���Ƿ��Ƕ�����Ů*/
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
                this.m_BizC.Fileds36 = Fileds36; //ĩ���¾�
                //��Ů�� txtFinalYjDate
                this.m_BizC.Fileds07 = this.m_PerB.BirthNum;

                //�������� �������� txtFinalYjDate
                this.m_BizC.Fileds18 = Fileds18;
                this.m_BizC.Fileds19 = DateTime.Now.ToString();

                this.m_BizC.Fileds49 = CidTypeA;
                this.m_BizC.Fileds50 = CidTypeB;


                string objID = m_BizC.Insert();
                m_BizC = null;
                if (!string.IsNullOrEmpty(objID) && PageValidate.IsNumber(objID))
                {
                    //ҵ����־
                    CommPage.SetBizLog(objID, m_UserID, "ҵ����", "����Ա�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " �����ˡ�" + this.m_NavTitle + "���������");

                    /*���£�1.�ж�Ⱥ�ڻ��������Ƿ���ڣ������ڲ��룬���ڸ���  --�ж����ݣ�����֤��
                            2.�жϻ��������Ƿ���ڸ��û���Ϣ�������ڲ��룬���ڲ�����  --�ж����ݣ�Ⱥ�ڱ��
                            3.�ж���Ů�����Ƿ���ڸ��û����м�ͥ��Ů��Ϣ�������ڲ��룬���ڲ�����  --�ж����ݣ�Ⱥ�ڱ��
                     */
                    #region һ�����⴦��
                    string perAAreaCode = string.Empty;
                    string perBAreaCode = string.Empty;
                    if (this.m_PerA.RegisterAreaCode.Substring(0, 4) != "1505")
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
                        MessageBox.Show(this, "�������̱�ʧ�ܣ�����ϵϵͳ����Ա��");
                        return;
                    }
                    #region �������
                    //========�з� 
                    this.m_PerA.MateName = this.m_PerB.PersonName;
                    this.m_PerA.MateCardID = this.m_PerB.PersonCardID;
                    string PersonIDA = this.m_PerA.ExecBizPersons();
                    //========Ů��
                    this.m_PerB.MateName = this.m_PerA.PersonName;
                    this.m_PerB.MateCardID = this.m_PerA.PersonCardID;
                    string PersonIDB = this.m_PerB.ExecBizPersons();
                    //���Ͷ�Ѷ--start--
                    // ĳĳ,���ύ��[ҵ������]������������
                    string uTel, uName, tMsg = string.Empty;
                    if (!string.IsNullOrEmpty(this.m_PerB.PersonTel)) { uTel = this.m_PerB.PersonTel; uName = this.m_PerB.PersonName; } else { uTel = this.m_PerA.PersonTel; uName = this.m_PerA.PersonName; }
                    tMsg = uName + "�����ύ��[" + this.m_NavTitle + "]������������"; //��̨����
                    if (!string.IsNullOrEmpty(uTel) && !string.IsNullOrEmpty(tMsg))
                    {
                        SendMsg.SendMsgByModem(uTel, tMsg);
                    }
                    //���Ͷ�Ѷ--end--
                    m_PerA = null;
                    m_PerB = null;

                    this.m_Children.Inser(PersonIDB, objID, iBirthNum.ToString());
                    #endregion
                    //if (!String.IsNullOrEmpty(PersonPhotos[1])) { BIZ_Common.InsetBizDocs(objID, PersonIDB, "2������ڷ򸾺�Ӱ��Ƭ", PersonPhotos, "1"); }

                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��<" + this.m_NavTitle + ">������Ϣ�ύ�ɹ���������ύ֤��������룡", "BizDocs.aspx?action=add&k=" + objID + "&PersonIDA=" + PersonIDA + "&sourceUrl=" + m_SourceUrl, true, true);
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
