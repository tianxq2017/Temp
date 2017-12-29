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
    public partial class SysUserPwd : System.Web.UI.Page
    {
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_SqlParams;
        private string m_ActionName;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            m_ActionName = PageValidate.GetFilterSQL(Request["Action"]);
            this.LiteralNav.Text = "�ʺ��������ã�";
            SetPageStyle(m_UserID);

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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        // �޸�����
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string Err = "";
            string oldPwd = PageValidate.GetTrim(this.txOldPwd.Text);
            string userPwd = PageValidate.GetTrim(this.txtUserPwd.Text);
            string userPwdRe = PageValidate.GetTrim(this.txtUserPwdRe.Text);
            if (String.IsNullOrEmpty(oldPwd))
            {
                Err += "����������룡\\n";
            }
            if (String.IsNullOrEmpty(userPwd))
            {
                Err += "�����벻��Ϊ�գ�\\n";
            }
            if (String.Compare(userPwd, userPwdRe) != 0)
            {
                Err += "�����������ȷ�����벻һ�£����������룡\\n";
            }
            if (Err != "")
            {
                MessageBox.Show(this, Err);
                return;
            }

            try
            {
                oldPwd = DESEncrypt.Encrypt(oldPwd);
                userPwd = DESEncrypt.Encrypt(userPwd);
                Err = join.pms.dal.CommPage.GetSingleValue("SELECT UserPassword FROM USER_BaseInfo WHERE UserID=" + m_UserID);
                if (oldPwd == Err)
                {
                    m_SqlParams = "UPDATE [USER_BaseInfo] SET [UserPassword]='" + userPwd + "' WHERE UserID= " + m_UserID;
                    if (DbHelperSQL.ExecuteSql(m_SqlParams) == 1)
                    {
                        if (!string.IsNullOrEmpty(m_ActionName) && m_ActionName == "Ysl")
                        {
                            Response.Write(" <script>alert('��ʾ��Ϣ�����ĵ�¼����ɹ��޸ģ����˳�ϵͳ�������������µ�¼��') ;window.location.href='/MainDesk.aspx';</script>");
                        }
                        else
                        {
                            Response.Write(" <script>alert('��ʾ��Ϣ�����ĵ�¼����ɹ��޸ģ����˳�ϵͳ�������������µ�¼��') ;window.location.href='/MainDesk.aspx';</script>");
                        }
                    }
                    else
                    {
                        Response.Write(" <script>alert('����ʧ�ܣ��޸��������') ;</script>");
                        MessageBox.Show(this, "����ʧ�ܣ��޸��������");
                    }
                }
                else
                {
                    Response.Write(" <script>alert('������ʾ������ʧ�ܣ�ԭ��������������');window.location.href='/MainDesk.aspx';</script>");
                }
            }
            catch { MessageBox.Show(this, "����ʧ�ܣ��޸��������"); }
        }
    }
}
