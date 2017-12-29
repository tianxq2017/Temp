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
    public partial class Biz0105 : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_UserName;

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
            this.txtAreaSelRegCodeB.Value = m_AreaCode;
            this.txtAreaSelRegNameB.Text = BIZ_Common.GetAreaName(m_AreaCode, "1");
            this.txtAreaSelRegNameB.ReadOnly = true;

            if (!IsPostBack)
            {
                //ȡ֤�ص�
                this.UcAreaSelXian1.SetAreaCode(m_AreaCode);
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

            this.m_PerA = new BIZ_Persons();
            this.m_PerB = new BIZ_Persons();
            this.m_BizC = new BIZ_Contents();
            this.m_Children = new BIZ_PersonChildren();

            #region ����

            /*Ů��������֤�š��������Ա𡢳������¡�����״��������ʱ�䡢���塢��ϵ�绰���������ʡ�������ַ*/
            this.m_PerB.PersonCardID = PageValidate.GetTrim(this.txtPersonCidB.Text);
            this.m_PerB.PersonName = PageValidate.GetTrim(this.txtPersonNameB.Text);
            this.m_PerB.PersonSex = "Ů";
            this.m_PerB.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerB.PersonCardID);
            this.m_PerB.MarryType = PageValidate.GetTrim(this.ddlMarryTypeB.SelectedValue);
            this.m_PerB.MarryDate = CommBiz.GetTrim(this.txtMarryDateB.Value);
            this.m_PerB.Nations = PageValidate.GetTrim(Request["txtNationsB"]);
            this.m_PerB.PersonTel = PageValidate.GetTrim(this.txtContactTelB.Text);
            this.m_PerB.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeB.Text);
            this.m_PerB.RegisterAreaCode = PageValidate.GetTrim(this.txtAreaSelRegCodeB.Value);
            this.m_PerB.RegisterAreaName = PageValidate.GetTrim(this.txtAreaSelRegNameB.Text);

            /*�з�������֤�š��������Ա𡢳������¡�����״��������ʱ�䡢���塢��ϵ�绰���������ʡ�������ַ*/
            this.m_PerA.PersonCardID = PageValidate.GetTrim(this.txtPersonCidA.Text);
            this.m_PerA.PersonName = PageValidate.GetTrim(this.txtPersonNameA.Text);
            this.m_PerA.PersonSex = "��";
            this.m_PerA.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerA.PersonCardID);
            this.m_PerA.MarryType = PageValidate.GetTrim(this.ddlMarryTypeA.SelectedValue);
            this.m_PerA.MarryDate = CommBiz.GetTrim(this.txtMarryDateA.Value);
            this.m_PerA.Nations = PageValidate.GetTrim(Request["txtNationsA"]);
            this.m_PerA.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeA.Text);


            //===========================��Ů��Ϣ=========================================  
            string Fileds40 = PageValidate.GetTrim(this.ddlFileds40.SelectedValue);
            string Fileds37 = PageValidate.GetTrim(this.ddlFileds37.SelectedValue);
            string Fileds38 = PageValidate.GetTrim(this.ddlFileds38.SelectedValue);
            this.m_PerB.BirthNum = (int.Parse(Fileds37) + int.Parse(Fileds38)).ToString();

            //===========================������Ϣ========================================= 
            string Fileds45 = PageValidate.GetTrim(this.ddlFileds45.SelectedValue);
            string Fileds46 = PageValidate.GetTrim(this.ddlFileds46.SelectedValue);
            string Fileds44 = CommBiz.GetTrim(this.txtFileds44.Value);


            //==========================Ů����Ϣ start========================================== 
            if (String.IsNullOrEmpty(this.m_PerB.PersonCardID)) { strErr += "������Ů������֤�ţ�\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerB.PersonCardID)) { strErr += "Ů������֤�����󣡣�\\n"; }
            if (CommBiz.GetAgeByID(this.m_PerB.PersonCardID) > 49) { strErr += "Ů��������49�����ڲſ������룡"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonName)) { strErr += "������Ů��������\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryType)) { strErr += "��ѡ��Ů������״����\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.MarryDate)) { strErr += "��ѡ��Ů������ʱ�䣡\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.Nations)) { strErr += "������Ů�����壡\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.RegisterType)) { strErr += "��ѡ��Ů���������ʣ�\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerB.RegisterAreaCode, "0")) { strErr += "��ѡ��Ů�������أ�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonTel)) { strErr += "������Ů����ϵ�绰��\\n"; }

            //==========================�з���Ϣ start========================================== 
            if (String.IsNullOrEmpty(this.m_PerA.PersonCardID)) { strErr += "�������з�����֤�ţ�\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerA.PersonCardID)) { strErr += "�з�����֤�����󣡣�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.PersonName)) { strErr += "�������з�������\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.MarryType)) { strErr += "��ѡ���з�����״����\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.MarryDate)) { strErr += "��ѡ���з�����ʱ�䣡\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.Nations)) { strErr += "�������з����壡\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.RegisterType)) { strErr += "��ѡ���з��������ʣ�\\n"; }


            //===========================��Ů��Ϣ=========================================  
            if (String.IsNullOrEmpty(Fileds40)) { strErr += "��ѡ�����߿�������Ů����\\n"; }
            if (String.IsNullOrEmpty(Fileds37)) { strErr += "��ѡ��������Ů�к�������\\n"; }
            if (String.IsNullOrEmpty(Fileds38)) { strErr += "��ѡ��������ŮŮ��������\\n"; }

            //===========================������Ϣ========================================= 
            if (String.IsNullOrEmpty(Fileds45) && String.IsNullOrEmpty(Fileds46)) { strErr += "��ѡ�����״����\\n"; }
            if (String.IsNullOrEmpty(Fileds44)) { strErr += "��ѡ����п�ʼ���ڣ�\\n"; }

            //��Ů��Ϣ
            int iBirthNum = int.Parse(this.m_PerB.BirthNum);
            #region ��Ů��Ϣ
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
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "��������Ů1������\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "��ѡ����Ů1�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "��ѡ����Ů1�������£�\\n"; }
            }
            else if (iBirthNum == 2)
            {
                ChildName3 = ""; ChildName4 = "";

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
                ChildName4 = "";
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
            else if (iBirthNum == 4)
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

                if (String.IsNullOrEmpty(ChildName4)) { strErr += "��������Ů4������\\n"; }
                if (String.IsNullOrEmpty(ChildSex4)) { strErr += "��ѡ����Ů4�Ա�\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday4)) { strErr += "��ѡ����Ů4�������£�\\n"; }

                if (ChildName1 == ChildName2 || ChildName1 == ChildName3 || ChildName1 == ChildName4 || ChildName2 == ChildName3 || ChildName2 == ChildName4 || ChildName3 == ChildName4) { strErr += "��Ů�����ظ���\\n"; }
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

                /*�з�������֤�š��������Ա𡢳������¡�����״��������ʱ�䡢���塢��ϵ�绰����������*/
                this.m_BizC.PersonCidA = this.m_PerA.PersonCardID;
                this.m_BizC.Fileds01 = this.m_PerA.PersonName;
                this.m_BizC.Fileds02 = this.m_PerA.PersonSex;
                this.m_BizC.Fileds32 = this.m_PerA.PersonBirthday;
                this.m_BizC.Fileds33 = this.m_PerA.MarryType;
                this.m_BizC.Fileds34 = this.m_PerA.MarryDate;
                this.m_BizC.Fileds03 = this.m_PerA.Nations;
                this.m_BizC.ContactTelA = this.m_PerA.PersonTel;
                this.m_BizC.Fileds04 = this.m_PerA.RegisterType;

                /*Ů��������֤�š��������Ա𡢳������¡�����״��������ʱ�䡢���塢��ϵ�绰���������ʡ�������ַ*/
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

                //===========================��Ů��Ϣ=========================================  
                this.m_BizC.Fileds40 = Fileds40;
                this.m_BizC.Fileds37 = Fileds37;
                this.m_BizC.Fileds38 = Fileds38;
                this.m_BizC.Fileds07 = this.m_PerB.BirthNum;


                //===========================������Ϣ========================================= 
                this.m_BizC.Fileds45 = Fileds45;
                this.m_BizC.Fileds46 = Fileds46;
                this.m_BizC.Fileds44 = Fileds44;

                //�������� ��������
                this.m_BizC.Fileds18 = Fileds18;
                this.m_BizC.Fileds19 = DateTime.Now.ToString();

                string objID = m_BizC.Insert();
                m_BizC = null;
                if (!string.IsNullOrEmpty(objID) && PageValidate.IsNumber(objID))
                {
                    CommPage.SetBizLog(objID, m_UserID, "ҵ����", "����Ա�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " �����ˡ�" + this.m_NavTitle + "���������");

                    if (this.m_BizStep != BIZ_Common.InsetBizWorkFlows(this.m_FuncCode, this.m_BizStep, objID, "", "", this.m_AreaCode))
                    {
                        MessageBox.Show(this, "�������̱�ʧ�ܣ�����ϵϵͳ����Ա��");
                        return;
                    }
                    /*���£�1.�ж�Ⱥ�ڻ��������Ƿ���ڣ������ڲ��룬���ڸ���  --�ж����ݣ�����֤��
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

                    //������Ů��Ϣ

                    this.m_Children.Inser(PersonIDB, objID, iBirthNum.ToString());
                    #endregion

                    //if (!String.IsNullOrEmpty(PersonPhotos[1])) { BIZ_Common.InsetBizDocs(objID, PersonIDB, "2������ڷ򸾺�Ӱ��Ƭ", PersonPhotos, "1"); }

                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��<" + this.m_NavTitle + ">������Ϣ�ύ�ɹ���������ύ֤��������룡", "BizDocs.aspx?action=add&k=" + objID + "&PersonIDA=" + PersonIDA + "&PersonIDB=" + PersonIDB + "&sourceUrl=" + m_SourceUrl, true, true);
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
