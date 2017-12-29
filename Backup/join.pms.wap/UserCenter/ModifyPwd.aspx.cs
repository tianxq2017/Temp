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
namespace join.pms.wap.UserCenter
{
    public partial class ModifyPwd : UNV.Comm.Web.PageBase
    {
        #region �Զ������
        private string m_UserID;

        private string m_SqlParams;
        protected string m_TargetUrl;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            this.Uc_PageTop1.GetSysMenu("�޸�����");
            if (!IsPostBack)
            { }
        }

        #region ����ҳͷ��Ϣ������\��֤����\��֤�û���
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
                Response.Write("<script language='javascript'>alert(\"������ʾ�����¼�����ԣ�\");parent.location.href='/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh." + m_FileExt + "';</script>");
                Response.End();
            }
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
            #region ����
            string oldPwd = PageValidate.GetTrim(this.txtUserPasswordOld.Text);
            string userPwd = PageValidate.GetTrim(this.txtUserPassword.Text);
            string userPwdRe = PageValidate.GetTrim(this.txtUserPasswordRe.Text);
            if (String.IsNullOrEmpty(oldPwd))
            {
                strErr += "����������룡\\n";
            }
            if (String.IsNullOrEmpty(userPwd))
            {
                strErr += "�����벻��Ϊ�գ�\\n";
            }
            if (String.Compare(userPwd, userPwdRe) != 0)
            {
                strErr += "�����������ȷ�����벻һ�£����������룡\\n";
            }
            #endregion

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            try
            {
                oldPwd = DESEncrypt.GetMD5_32(oldPwd);
                userPwd = DESEncrypt.GetMD5_32(userPwd);
                strErr = CommPage.GetSingleValue("SELECT PersonPwd FROM BIZ_Persons WHERE PersonID=" + m_UserID);
                if (oldPwd == strErr)
                {
                    m_SqlParams = "UPDATE BIZ_Persons SET PersonPwd='" + userPwd + "' WHERE PersonID=" + m_UserID;
                    if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                    {
                        Response.Write(" <script>alert('������ʾ�������ɹ�,���˳�ϵͳ��ʹ�����������µ�¼��');window.location.href='/USERSIGNOUT." + m_FileExt + "';</script>");
                    }
                    else
                    {
                        Response.Write(" <script>alert('������ʾ������ʧ�ܣ�����ϵϵͳ����Ա��');window.location.href='/OC/ModifyPwd." + m_FileExt + "';</script>");
                    }
                }
                else
                {
                    Response.Write(" <script>alert('������ʾ������ʧ�ܣ�ԭ��������������');window.location.href='/OC/ModifyPwd." + m_FileExt + "';</script>");
                }
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
