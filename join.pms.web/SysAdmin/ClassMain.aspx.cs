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

using UNV.Comm.Web;

namespace join.pms.web.SysAdmin
{
    public partial class ClassMain : System.Web.UI.Page
    {
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        protected string m_FuncCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_FuncCode = PageValidate.GetFilterSQL(Request.QueryString["FuncCode"]);
            this.labTitle.Text = Request.QueryString["FuncName"];

            AuthenticateUser();
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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='../default.aspx';</script>");
                Response.End();
            }
        }
    }
}
