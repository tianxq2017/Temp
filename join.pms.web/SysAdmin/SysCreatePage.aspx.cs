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

using join.pms.dal;

using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.web.SysAdmin
{
    public partial class SysCreatePage : System.Web.UI.Page
    {
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_SqlParams;
        private string m_SiteUrl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"];


        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();


            if (!IsPostBack)
            {
                this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">��ʼҳ</a> &gt;&gt; ��������&gt;&gt; ��̬ҳ�����ɣ�";

                GetPageName();
                //if (!string.IsNullOrEmpty(m_SiteUrl)) { 
                //    GetPageName(); 
                //} 
                //else {
                //    this.LiteralInfo.Text = "û������վ���ַ,������ web.config �ļ� appSettings���µ� SiteUrl ֵΪʵ�ʵ�վ���ַ ";
                //}
            }
        }

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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/loginTemp.aspx';</script>");
                Response.End();
            }
        }


        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="objID"></param>
        private void GetPageName()
        {
            string objID, pName, pSourceUrl, pTargetUrl = string.Empty;
            StringBuilder sHtml = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                // ������Ŀ��ѡ����
                m_SqlParams = "SELECT CommID,PageNames,PageSourceUrl,PageTargetUrl FROM SYS_StaticPage ";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                sHtml.Append("<select size=\"13\" id=\"SelPageNames\" name=\"SelPageNames\" style=\"width:420px\">");
                while (sdr.Read())
                {
                    objID = sdr[0].ToString();
                    pName = sdr[1].ToString();
                    pSourceUrl = sdr[2].ToString();
                    pTargetUrl = sdr[3].ToString();

                    sHtml.Append("<option value=\"" + objID + "," + pName + "," + pSourceUrl + "," + pTargetUrl + "\">" + pName + " &nbsp;&nbsp; \r &nbsp;&nbsp; " + pSourceUrl + " &nbsp;&nbsp; ---> \t &nbsp;&nbsp; " + pTargetUrl + " </option>");
                }
                sHtml.Append("select");
                sdr.Close();
            }
            catch
            {
                if (sdr != null) sdr.Close();
            }

            this.LiteralItems.Text = sHtml.ToString();
            sHtml = null;
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string selectVal = PageValidate.GetTrim(Request["SelPageNames"]); // "2,�߽���ƽ,/index-xingping.aspx,/index-xingping.shtml"
            string[] aryPages = null;
            string objID = "", pName = "", pSourceUrl = "", pTargetUrl = "";
            string url = Request.Url.Authority; // "localhost:2908"
            // by ysl 2013/07/10 ������ֻ��������ַ
            if (url.IndexOf(":") > 0)
            {
                url = "http://127.0.0.1" + url.Substring(url.IndexOf(":"));
            }
            else
            {
                url = "http://127.0.0.1";
            }

            if (String.IsNullOrEmpty(selectVal))
            {
                this.LiteralInfo.Text = "��ѡ��Ҫ���ɵ�ҳ��\\n";
                return;
            }

            try
            {
                aryPages = selectVal.Split(',');
                if (aryPages.Length == 4)
                {
                    objID = aryPages[0];
                    pName = aryPages[1];
                    pSourceUrl = url + aryPages[2];
                    pTargetUrl = Server.MapPath("/") + aryPages[3];

                    if (CommBiz.SetHtmlByUrl(pSourceUrl, pTargetUrl))
                    {
                        strErr = pName + "-����[" + pTargetUrl + "]�ɹ�...<br/>";
                        DbHelperSQL.ExecuteSql("UPDATE SYS_StaticPage SET OprateDate=GetDate() WHERE CommID=" + objID);
                    }
                    else { strErr = "����[" + pName + "]ʧ��...<br/>"; }
                }
                else { }

            }
            catch (Exception ex)
            {
                strErr += ex.Message + "<br/>" + pSourceUrl;
            }
            this.LiteralInfo.Text = strErr;
        }

        protected void btnCreateAll_Click(object sender, EventArgs e)
        {
            // ����ȫ��
            string objID, pName, pSourceUrl, pTargetUrl = string.Empty;
            string url = Request.Url.Authority;
            // by ysl 2013/07/10 ������ֻ��������ַ
            if (url.IndexOf(":") > 0)
            {
                url = "http://127.0.0.1" + url.Substring(url.IndexOf(":"));
            }
            else
            {
                url = "http://127.0.0.1";
            }
            StringBuilder sHtml = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                // ������Ŀ��ѡ����
                m_SqlParams = "SELECT CommID,PageNames,PageSourceUrl,PageTargetUrl FROM SYS_StaticPage ";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    objID = sdr[0].ToString();
                    pName = sdr[1].ToString();
                    pSourceUrl = url + sdr[2].ToString();
                    pTargetUrl = Server.MapPath("/") + sdr[3].ToString();

                    if (CommBiz.SetHtmlByUrl(pSourceUrl, pTargetUrl))
                    {
                        sHtml.Append(pName + "-����[" + pTargetUrl + "]�ɹ�...<br/>");
                        DbHelperSQL.ExecuteSql("UPDATE SYS_StaticPage SET OprateDate=GetDate() WHERE CommID=" + objID);
                    }
                    else { sHtml.Append("����[" + pName + "]ʧ��...<br/>"); }
                }
                sdr.Close();
            }
            catch (Exception ex)
            {
                sHtml.Append(ex.Message);
                if (sdr != null) sdr.Close();
            }
            this.LiteralInfo.Text = sHtml.ToString();
        }
    }
}


