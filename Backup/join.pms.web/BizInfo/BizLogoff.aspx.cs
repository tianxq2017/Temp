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

using System.IO;
using System.Text;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;

namespace join.pms.web.BizInfo
{
    public partial class BizLogoff : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_SqlParams;
        public string m_TargetUrl;

        private string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                SetOpratetionAction("");
            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "";// DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                //if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";
                cssFile = "/css/inidex.css";
                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        #region ҳ����֤����������
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
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        #endregion

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
                case "logoff":
                    funcName = "ע��";
                    if (!PageValidate.IsNumber(m_ObjID))
                    {
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ��ÿ��ֻ��ѡ��һ��ҵ����в�����", m_TargetUrl, true,true);
                    }
                    else
                    {
                        SetLogoffItems(m_FuncCode);
                    }

                    break;
                case "view": // �鿴
                    funcName = "�鿴����";
                    //ShowModInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true, true);
                    break;
            }
            this.LiteralNav.Text = "��ʼҳ  &gt;&gt; " + CommPage.GetAllBizName(m_FuncCode) + " &gt;" + funcName + "��";
        }
        /*
         0101	3	һ�������Ǽ�
0102	4	���������Ǽ�
0103	3	������ͥ�������������걨���
0104	3	������ͥ�ر���������������
0105	3	�������츻�������������
0106	4	������ͥ�����������
0107	3	��һ���̡���������������
0108	3	��������Ů��ĸ����֤�����
0109	3	�������˿ڻ���֤�����������
0110	3	�������֤������
0111	6	��ֹ�����������
         */
        private void SetLogoffItems(string bizCode)
        {
            StringBuilder s = new StringBuilder();
            s.Append("<select name=\"selAuditComments\" id=\"selAuditComments\" onchange=\" SetTxtCommVal(this.options[this.options.selectedIndex].value)\">");
            s.Append("<option value=\"\">��ѡ��ע���������</option>");
            switch (bizCode)
            {
                case "0101":
                    s.Append("<option value=\"�����������������������������Ů\">�����������������������������Ů</option>");
                    s.Append("<option value=\"�������\">�������</option>");
                    break;
                case "0102":
                    s.Append("<option value=\"���ҽѧ��Ҫѡ���Ա���ֹ���\">���ҽѧ��Ҫѡ���Ա���ֹ���</option>");
                    s.Append("<option value=\"��ܷ��ɡ����潫�Լ�����������Ů�ͱ��˸�����\">��ܷ��ɡ����潫�Լ�����������Ů�ͱ��˸�����</option>");
                    s.Append("<option value=\"��ȡ��������֤������һ����Ů����Ӥ����Ӥ����������Ű�����ܾ�������Ů��\">��ȡ��������֤������һ����Ů����Ӥ����Ӥ����������Ű�����ܾ�������Ů��</option>");
                    s.Append("<option value=\"Ū�����٣����������ʵ��\">Ū�����٣����������ʵ��</option>");
                    s.Append("<option value=\"�������\">�������</option>");
                    break;
                case "0103":
                    s.Append("<option value=\"Ū�����٣����������ʵ\">Ū�����٣����������ʵ</option>");
                    s.Append("<option value=\"�������\">�������</option>");
                    break;
                case "0104":
                    s.Append("<option value=\"Ū�����٣����������ʵ\">Ū�����٣����������ʵ</option>");
                    s.Append("<option value=\"�������\">�������</option>");
                    break;
                case "0105":
                    s.Append("<option value=\"Ū�����٣����������ʵ\">Ū�����٣����������ʵ</option>");
                    s.Append("<option value=\"�������\">�������</option>");
                    break;
                case "0106":
                    s.Append("<option value=\"Ū�����٣����������ʵ\">Ū�����٣����������ʵ</option>");
                    s.Append("<option value=\"�������\">�������</option>");
                    break;
                case "0107":
                    s.Append("<option value=\"Ū�����٣����������ʵ\">Ū�����٣����������ʵ</option>");
                    s.Append("<option value=\"�������\">�������</option>");
                    break;
                case "0108"://��������Ů��ĸ����֤��
                    s.Append("<option value=\"������ʵ��������졶������Ů��ĸ����֤��ʱʵ�����������������������������Ů��\">������ʵ��������졶������Ů��ĸ����֤��ʱʵ�����������������������������Ů��</option>");
                    s.Append("<option value=\"��ȡ��������Ů��ĸ����֤���󣬷����������������Ѿ��걨��������\">��ȡ��������Ů��ĸ����֤���󣬷����������������Ѿ��걨��������</option>");
                    s.Append("<option value=\"��ȡ��������Ů��ĸ����֤����Υ����������������Ů\">��ȡ��������Ů��ĸ����֤����Υ����������������Ů</option>");
                    s.Append("<option value=\"�������\">�������</option>");
                    break;
                case "0109":
                    s.Append("<option value=\"����֤�����ڣ���Ч��3�꣩\">����֤�����ڣ���Ч��3�꣩</option>");
                    s.Append("<option value=\"Ū�����٣����������ʵ\">Ū�����٣����������ʵ</option>");
                    s.Append("<option value=\"�������\">�������</option>");
                    break;
                case "0110":
                    s.Append("<option value=\"Ū�����٣����������ʵ\">Ū�����٣����������ʵ</option>");
                    s.Append("<option value=\"�������\">�������</option>");
                    break;
                case "0111":
                    s.Append("<option value=\"Ū�����٣����������ʵ\">Ū�����٣����������ʵ</option>");
                    s.Append("<option value=\"�������\">�������</option>");
                    break;
                case "0122":
                    s.Append("<option value=\"���ҽѧ��Ҫѡ���Ա���ֹ���\">���ҽѧ��Ҫѡ���Ա���ֹ���</option>");
                    s.Append("<option value=\"��ܷ��ɡ����潫�Լ�����������Ů�ͱ��˸�����\">��ܷ��ɡ����潫�Լ�����������Ů�ͱ��˸�����</option>");
                    s.Append("<option value=\"��ȡ��������֤������һ����Ů����Ӥ����Ӥ����������Ű�����ܾ�������Ů��\">��ȡ��������֤������һ����Ů����Ӥ����Ӥ����������Ű�����ܾ�������Ů��</option>");
                    s.Append("<option value=\"Ū�����٣����������ʵ��\">Ū�����٣����������ʵ��</option>");
                    s.Append("<option value=\"�������\">�������</option>");
                    break;
                default:
                    s.Append("<option value=\"\">��ҵ��������������</option>");
                    break;
            }
            s.Append("</select>");

            this.LiteralComments.Text = s.ToString();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // SELECT BizID,BizStep,AreaCode,AreaName,Comments,Approval,Signature,CreateDate,OprateDate FROM BIZ_WorkFlows  txtApprovalUserTel
            string strErr = string.Empty;
            string bizID = m_ObjID;
            string Attribs = string.Empty;
            string Comments = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtOtherDocs.Text));

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            try
            {
                if (CommPage.CheckLogOffAttribs("BizID IN(" + m_ObjID + ")"))
                {
                    System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                    list.Add("UPDATE BIZ_Certificates SET Attribs=1 WHERE BizID IN(" + bizID + ")");
                    list.Add("UPDATE BIZ_Contents SET Attribs=5,Comments='" + Comments + "' WHERE BizID IN(" + bizID + ")");
                    DbHelperSQL.ExecuteSqlTran(list);
                    list = null;

                    CommPage.SetBizLog(bizID, m_UserID, "ҵ��ע��", "�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " ������ҵ��ҵ����=" + bizID + "��ע������");

                    MessageBox.ShowAndRedirect(this.Page, "������ʾ������ѡ���ҵ��ע�������ɹ���", m_TargetUrl, true, true);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��ֻ�������ϵ�ҵ��ſ���ִ�С�ע����������", m_TargetUrl, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��" + ex.Message, m_TargetUrl, true, true);
            }
        }
    }
}
