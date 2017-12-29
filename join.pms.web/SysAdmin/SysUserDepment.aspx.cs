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
using UNV.Comm.DataBase;
using UNV.Comm.Web;
namespace join.pms.web.SysAdmin
{
    public partial class SysUserDepment : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_SqlParams;
        private DataTable m_Dt;

        protected string m_TargetUrl;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetOpratetionAction("��֯��������");
            }
        }

        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Ysl9lcXf6JuzBkRL1yQD48cmhxCD5exHudvJr7ExPl6SnOYhiJLFhhdlZx1OzuA1vCf.shtml';</script>");
                Response.End();
            }
        }
        /// <summary>
        /// ���ò�����Ϊ
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;
            if (!String.IsNullOrEmpty(m_FuncCode.Substring(0, 2)))
            {
                switch (m_ActionName)
                {
                    case "add": // ���´浵��Ϣ����
                        funcName = "����¿���";
                        break;
                    case "edit": // ��Ա������Ϣ����
                        funcName = "�༭��������";
                        ShowModify();
                        break;
                    case "del": // ɾ��
                        funcName = "ɾ������";
                        DelDept();
                        break;
                    case "value": // �鿴
                        funcName = "�鿴����";
                        break;
                    case "5": // ���
                        funcName = "�������";
                        break;
                    case "6": // �Ƽ�
                        funcName = "�Ƽ�";
                        break;
                    default:
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                        break;
                }
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        private void ShowModify()
        {
            if (!String.IsNullOrEmpty(m_ObjID) && PageValidate.IsNumber(m_ObjID))
            {
                m_Dt = new DataTable();
                try
                {
                    m_SqlParams = "SELECT DeptCode,DeptName,DeptAddress,DeptTel FROM USER_Department WHERE  CommID=" + m_ObjID;
                    m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                    if (m_Dt.Rows.Count == 1)
                    {
                        this.txtDeptCode.Value = m_Dt.Rows[0][0].ToString();
                        this.txtDeptName.Value = m_Dt.Rows[0][1].ToString();
                        this.txtDeptAddress.Value = m_Dt.Rows[0][2].ToString();
                        this.txtDeptTel.Value = m_Dt.Rows[0][3].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��" + ex.Message, m_TargetUrl, true);
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try 
            {
                // DeptCode,DeptName,DeptAddress,DeptTel
                string strErr = string.Empty;
                string DeptCode = PageValidate.GetTrim(this.txtDeptCode.Value);
                string DeptName = PageValidate.GetTrim(this.txtDeptName.Value);
                string DeptAddress = PageValidate.GetTrim(this.txtDeptAddress.Value);
                string DeptTel = PageValidate.GetTrim(this.txtDeptTel.Value);
                if (String.IsNullOrEmpty(DeptCode))
                {
                    strErr += "�������벻��Ϊ�գ�\\n";
                }
                if (String.IsNullOrEmpty(DeptName))
                {
                    strErr += "������������ƣ�\\n";
                }
                if (strErr != "")
                {
                    MessageBox.Show(this, strErr);
                    return;
                }

                if (m_ActionName == "add")
                {
                    m_SqlParams = "INSERT INTO USER_Department(DeptCode,DeptName,DeptAddress,DeptTel,OprateUserID) VALUES('" + DeptCode + "','" + DeptName + "','" + DeptAddress + "','" + DeptTel + "'," + m_UserID + ")";
                }
                else if (m_ActionName == "edit")
                {
                    m_SqlParams = "UPDATE USER_Department SET DeptCode='" + DeptCode + "',DeptName='" + DeptName + "',DeptAddress='" + DeptAddress + "',DeptTel='" + DeptTel + "' WHERE CommID=" + m_ObjID;
                }
                else { 
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��������ʧ�������²�����", m_TargetUrl, true);
            }
                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "������ʾ�������ɹ���", m_TargetUrl, true);
            }
            catch(Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��" + ex.Message, m_TargetUrl, true);
            }
        }
        /// <summary>
        /// ����ɾ��
        /// </summary>
        private void DelDept()
        { 
            if (!String.IsNullOrEmpty(m_ObjID) && PageValidate.IsNumber(m_ObjID))
            {
                try
                {
                    DbHelperSQL.ExecuteSql("delete from USER_Department where CommID=" + m_ObjID);
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��ɾ���ɹ���", m_TargetUrl, true);
                }
                catch(Exception ex)
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��" + ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��ɾ������ֻ����ѡ��һ����¼" , m_TargetUrl, true);
            }
        }
        
    }
}
