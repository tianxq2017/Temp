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
    public partial class Biz0107 : System.Web.UI.Page
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
                this.UcAreaSelCurA.SetAreaCode(m_AreaCode);
                this.UcAreaSelCurA.SetAreaName(BIZ_Common.GetAreaName(m_AreaCode, "1"));
                this.UcAreaSelCurB.SetAreaCode(m_AreaCode);
                this.UcAreaSelCurB.SetAreaName(BIZ_Common.GetAreaName(m_AreaCode, "1"));
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

        #region �����֤����������
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

            this.m_PerA = new BIZ_Persons();
            this.m_PerB = new BIZ_Persons();
            this.m_BizC = new BIZ_Contents();

            #region ����

            /*Ů�������������塢�������¡���ס��ַ������̥�Ρ�ĩ���¾�ʱ�䡢ȷ�ϻ���ʱ�䡢ȷ�Ϸ�ʽ��ȷ�ϵ�λ*/
            this.m_PerB.PersonCardID = PageValidate.GetTrim(this.txtPersonCidB.Text);
            this.m_PerB.PersonName = PageValidate.GetTrim(this.txtPersonNameB.Text);
            this.m_PerB.PersonSex = "Ů";
            this.m_PerB.Nations = PageValidate.GetTrim(Request["txtNationsB"]);
            this.m_PerB.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerB.PersonCardID);
            this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
            this.m_PerB.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaName());
            string Fileds41 = PageValidate.GetTrim(this.ddlFileds41.Text);
            string Fileds42 = CommBiz.GetTrim(this.txtFileds42.Value);
            string Fileds43 = CommBiz.GetTrim(this.txtFileds43.Value);
            string Fileds29 = PageValidate.GetTrim(this.ddlFileds29.SelectedValue);
            string Fileds30 = PageValidate.GetTrim(this.ddlFileds30.SelectedValue);
            this.m_PerB.PersonTel = PageValidate.GetTrim(this.txtContactTelB.Text);

            /*�з������������塢�������¡���ס��ַ*/
            this.m_PerA.PersonCardID = PageValidate.GetTrim(this.txtPersonCidA.Text);
            this.m_PerA.PersonName = PageValidate.GetTrim(this.txtPersonNameA.Text);
            this.m_PerA.PersonSex = "��";
            this.m_PerA.Nations = PageValidate.GetTrim(Request["txtNationsA"]);
            this.m_PerA.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerA.PersonCardID);
            this.m_PerA.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
            this.m_PerA.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaName());

            //==========================Ů����Ϣ start========================================== 
            if (String.IsNullOrEmpty(this.m_PerB.PersonCardID)) { strErr += "������Ů�����֤�ţ�\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerB.PersonCardID)) { strErr += "Ů�����֤�����󣡣�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonName)) { strErr += "������Ů��������\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.Nations)) { strErr += "������Ů�����壡\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerB.CurrentAreaCode, "0")) { strErr += "��ѡ��Ů���־�ס�أ�\\n"; }
            if (String.IsNullOrEmpty(Fileds41)) { strErr += "�����뻳��̥�Σ�\\n"; }
            if (String.IsNullOrEmpty(Fileds42)) { strErr += "������ĩ���¾�ʱ�䣡\\n"; }
            if (String.IsNullOrEmpty(Fileds43)) { strErr += "������ȷ�ϻ���ʱ�䣡\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonTel)) { strErr += "������Ů����ϵ�绰��\\n"; }

            //==========================�з���Ϣ start========================================== 
            if (String.IsNullOrEmpty(this.m_PerA.PersonCardID)) { strErr += "�������з����֤�ţ�\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerA.PersonCardID)) { strErr += "�з����֤�����󣡣�\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.PersonName)) { strErr += "�������з�������\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.Nations)) { strErr += "�������з����壡\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerA.CurrentAreaCode, "0")) { strErr += "��ѡ���з��־�ס�أ�\\n"; }
            if (this.m_PerA.CurrentAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "��Ů˫���־�ס������Ҫ��һ������ѡ���һ�£�\\n"; }

            /*���ʱ�� */
            string Fileds14 = CommBiz.GetTrim(this.txtFileds14.Value);
            if (String.IsNullOrEmpty(Fileds14)) { strErr += "��������ʱ�䣡\\n"; }

            /*����֤�� */
            string Fileds21 = PageValidate.GetTrim(this.txtFileds21.Text);
            if (String.IsNullOrEmpty(Fileds21)) { strErr += "���������֤�ţ�\\n"; }

            /*�������ɣ����� ���� */
            string Fileds18 = PageValidate.GetTrim(this.txtFileds18.Text);
            if (String.IsNullOrEmpty(Fileds18)) { strErr += "�������������ɣ�\\n"; }

            #endregion

            //ȡ֤�ص�
            string GetAreaCode = PageValidate.GetTrim(this.UcAreaSelXian1.GetAreaCode());
            string GetAreaName = PageValidate.GetTrim(this.UcAreaSelXian1.GetAreaName());

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

                /*�з������������塢�������¡���ס��ַ*/
                this.m_BizC.PersonCidA = this.m_PerA.PersonCardID;
                this.m_BizC.Fileds01 = this.m_PerA.PersonName;
                this.m_BizC.Fileds02 = this.m_PerA.PersonSex;
                this.m_BizC.Fileds03 = this.m_PerA.Nations;
                this.m_BizC.Fileds32 = this.m_PerA.PersonBirthday;
                this.m_BizC.CurAreaCodeA = this.m_PerA.CurrentAreaCode;
                this.m_BizC.CurAreaNameA = this.m_PerA.CurrentAreaName;

                /*Ů�������������塢�������¡���ס��ַ������̥�Ρ�ĩ���¾�ʱ�䡢ȷ�ϻ���ʱ�䡢ȷ�Ϸ�ʽ��ȷ�ϵ�λ*/
                this.m_BizC.PersonCidB = this.m_PerB.PersonCardID;
                this.m_BizC.Fileds08 = this.m_PerB.PersonName;
                this.m_BizC.Fileds09 = this.m_PerB.PersonSex;
                this.m_BizC.Fileds10 = this.m_PerB.Nations;
                this.m_BizC.Fileds31 = this.m_PerB.PersonBirthday;
                this.m_BizC.CurAreaCodeB = this.m_PerB.CurrentAreaCode;
                this.m_BizC.CurAreaNameB = this.m_PerB.CurrentAreaName;
                this.m_BizC.Fileds41 = Fileds41;
                this.m_BizC.Fileds42 = Fileds42;
                this.m_BizC.Fileds43 = Fileds43;
                this.m_BizC.Fileds29 = Fileds29;
                this.m_BizC.Fileds30 = Fileds30;
                this.m_BizC.ContactTelB = this.m_PerB.PersonTel;

                //���ʱ��
                this.m_BizC.Fileds14 = Fileds14;

                /*����֤�� */
                this.m_BizC.Fileds21 = Fileds21;

                //�������� ��������
                this.m_BizC.Fileds18 = Fileds18;
                this.m_BizC.Fileds19 = DateTime.Now.ToString();

                string objID = m_BizC.Insert();
                m_BizC = null;
                if (!string.IsNullOrEmpty(objID) && PageValidate.IsNumber(objID))
                {
                    //ҵ����־
                    CommPage.SetBizLog(objID, m_UserID, "ҵ����", "����Ա�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " �����ˡ�" + this.m_NavTitle + "���������");

                    /*���£�1.�ж�Ⱥ�ڻ��������Ƿ���ڣ������ڲ��룬���ڸ���  --�ж����ݣ����֤��
                            2.�жϻ��������Ƿ���ڸ��û���Ϣ�������ڲ��룬���ڲ�����  --�ж����ݣ�Ⱥ�ڱ��
                            3.�ж���Ů�����Ƿ���ڸ��û����м�ͥ��Ů��Ϣ�������ڲ��룬���ڲ�����  --�ж����ݣ�Ⱥ�ڱ��
                     */
                    if (this.m_BizStep != BIZ_Common.InsetBizWorkFlows(this.m_FuncCode, this.m_BizStep, objID, this.m_PerA.CurrentAreaCode, this.m_PerB.CurrentAreaCode, m_AreaCode))
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

                    #endregion

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
