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
    public partial class SysUserUpd : System.Web.UI.Page
    {
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_SqlParams;
        private string m_ActionName;
        private string m_ObjID;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);
            this.LiteralNav.Text = "�û���Ϣ����";
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

        // �޸���Ϣ
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string Err = "";
            string userInfo = PageValidate.GetTrim(this.txtInfo.Text);
            if (String.IsNullOrEmpty(userInfo))
            {
                Err += "��������Ϣ��\\n";
            }
            if (m_ActionName == "tel" && System.Text.RegularExpressions.Regex.IsMatch(userInfo, @"^[0-9]*$") == false)
            {
                Err += "��������ȷ�ĵ绰���룬ע�����뷨�л��Ƿ���ȷ��\\n";
            }
            //else if (m_ActionName == "wx" && System.Text.RegularExpressions.Regex.IsMatch(userInfo, @"^[a-zA-Z_-][A-Za-z0-9_-]*$") == false)
            //{
            //    Err += "��������ȷ��΢�źţ�����ĸ��ͷ������ĸ�����֡��»��߼�������ɣ�\\n";
            //}
            else if (m_ActionName == "wx" && string.IsNullOrEmpty(userInfo))
            {
                Err += "������΢�źţ�\\n";//΢��Ҳ�������ֻ��š�QQ�ŵȣ���һ��������ĸ��ͷ by ysl 2016/02/05
            }
            if (Err != "")
            {
                MessageBox.Show(this, Err);
                return;
            }

            try
            {
                if (m_ActionName == "tel" && System.Text.RegularExpressions.Regex.IsMatch(userInfo, @"^[0-9]*$"))
                {

                    m_SqlParams = " UPDATE SYS_Sign SET  SignPhone = '" + userInfo + "' WHERE SignID=" + m_ObjID;
                }
                else if (m_ActionName == "wx")
                {
                    m_SqlParams = " UPDATE SYS_Sign SET  SignWX = '" + userInfo + "' WHERE SignID=" + m_ObjID;
                }
                if (DbHelperSQL.ExecuteSql(m_SqlParams) == 1)
                {

                    Response.Write(" <script>alert('��ʾ��Ϣ��������Ϣ�޸ĳɹ���') ;window.location.href='/SysAdmin/SysUserInfo.aspx';</script>");

                }
                else
                {
                    Response.Write(" <script>alert('����ʧ�ܣ��޸��������') ;</script>");
                    MessageBox.Show(this, "����ʧ�ܣ��޸��������");
                }
            }
            catch { MessageBox.Show(this, "����ʧ�ܣ��޸ĳ���"); }
        }
    }
}
