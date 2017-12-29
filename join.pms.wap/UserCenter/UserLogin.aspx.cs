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
using join.pms.dal;

using System.Text;
using System.Data.SqlClient;
namespace join.pms.wap.UserCenter
{
    public partial class UserLogin : UNV.Comm.Web.PageBase
    {
        private string m_RawUrl; // ���صĵ�ַ
        private string m_PersonID;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateParams();
            this.Uc_PageTop1.GetSysMenu("Ⱥ�ڵ�¼");
            if (AuthenticateUser())
            {
                //string targetUrl = string.Empty;
                //if (this.m_UserType == "1")
                //{ targetUrl = "/stu/index." + this.m_FileExt + ""; }
                //else { targetUrl = "/com/index." + this.m_FileExt + ""; }
                MessageBox.ShowAndRedirect(this.Page, "", "/OC." + this.m_FileExt, false, true);
            }
            else
            {
                // ������֤�û�����Ľű�����  this.ButtonOK.Attributes.Add("onclick", "return ValidateUsers();");             
            }
        }
        #region �����֤ ��֤���ܵĲ��� ����ҳͷ��Ϣ
        /// <summary>
        /// �����֤
        /// </summary>
        private bool AuthenticateUser()
        {
            bool returnVa = false;
            if (Request.Browser.Cookies)
            {
                HttpCookie loginCookie = Request.Cookies["AREWEB_OC_PUBSVRS_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_PersonID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["UserID"] != null && !String.IsNullOrEmpty(Session["UserID"].ToString())) { returnVa = true; m_PersonID = Session["UserID"].ToString(); }
            }
            return returnVa;
        }
        /// <summary>
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_RawUrl = PageValidate.GetTrim(Request.QueryString["c"]);
            if (!string.IsNullOrEmpty(m_RawUrl))
            {
                m_RawUrl = DESEncrypt.Decrypt(m_RawUrl);
            }
        }

        #endregion

        #region ���ò������û���½��Ϣ
        /// <summary>
        /// ���ò������û���½��Ϣ
        /// </summary>
        /// <param name="userID"></param>
        private void SetUserLoginIn(string userInfo)
        {
            if (Request.Browser.Cookies)
            {
                HttpCookie cookie = new HttpCookie("AREWEB_OC_PUBSVRS_YSL");
                cookie.Values.Add("UserID", userInfo);
                Response.AppendCookie(cookie);
                cookie.Expires = System.DateTime.Now.AddHours(150000);
            }
            else
            {
                Session["UserID"] = userInfo;
            }
        }
        #endregion

        #region Ⱥ�ڵ�½ by�˻�
        /// <summary>
        /// Ⱥ�ڵ�½
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string personID = string.Empty;
            string sqlParams = string.Empty;
            string loginIP = Request.UserHostAddress;
            string targetUrl = string.Empty;
            
            string personAcc = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtPersonAcc.Text));
            string personPwd = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtPersonPwd.Text));


            // ��֤�� Start
            string strErr = string.Empty;
            string identityCode = PageValidate.GetTrim(Request["txtCheckCode"]);
            if (identityCode == "")
            {
                strErr += "��������֤�룡\\n";
            }
            if (String.IsNullOrEmpty(identityCode) || Session["CheckCode"] == null || identityCode.Trim().ToLower() != Session["CheckCode"].ToString().ToLower())
            {
                strErr += "��֤������������������룡\\n";
            }
            if (String.IsNullOrEmpty(personAcc))
            {
                strErr += "�������¼����\\n";
            }
            else
            {
                if (personAcc.Length > 20) personAcc = personAcc.Substring(0, 20);
            }
            if (String.IsNullOrEmpty(personPwd))
            {
                strErr += "�������¼���룡\\n";
            }
            if (!CommPage.IsAllowLogin(personAcc, loginIP)) { strErr += "�����Ե�½�Ĵ���������ȫ�������Ʒ�Χ������ϵ����Ա����\\n"; }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            // ��֤�� End 

            SqlDataReader sdr = null;
            personPwd = DESEncrypt.GetMD5_32(personPwd);
            try
            {

                // Ϊ����ǿ��ȫ��ȡ���˼������֤�ŵĵ�¼ģʽ 2015/03/26
                sqlParams = "SELECT PersonID,PersonAcc FROM BIZ_Persons WHERE (PersonAcc='" + personAcc + "' OR PersonCardID='" + personAcc + "') AND PersonPwd='" + personPwd + "'";
                //sqlParams = "SELECT PersonID,PersonAcc FROM BIZ_Persons WHERE PersonAcc='" + personAcc + "' AND PersonPwd='" + personPwd + "' AND Attribs!=0";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        personID = sdr["PersonID"].ToString();
                    }
                    SetUserLoginIn(personID);
                    //DbHelperSQL.SetSysLog(personID, Request.UserHostAddress, "��¼", "�ڲ�ƽ̨-�û�[" + userAcc + "]�� " + DateTime.Now.ToString() + " ��¼ϵͳ");
                    DbHelperSQL.ExecuteSql("UPDATE BIZ_Persons SET UserLastLoginTime=GetDate(),UserLoginNum=UserLoginNum+1,UserOnlineStatus=1 WHERE PersonID=" + personID);

                    if (!string.IsNullOrEmpty(this.m_RawUrl))
                    {
                        targetUrl = this.m_RawUrl;
                    }
                    else
                    { targetUrl = "/OC." + this.m_FileExt; }

                    MessageBox.ShowAndRedirect(this.Page, "", targetUrl, false, true);
                }
                else
                {
                    // ��¼���Ե�½����
                    CommPage.SetSysTryLogin(personAcc, loginIP, "ʧ��");

                    MessageBox.Show(this, "��¼ʧ�ܣ����������ʺ��Ƿ���Ч����ѯ��վ����Ա��");
                }
                sdr.Close(); sdr.Dispose();
            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                MessageBox.Show(this, "��¼ʧ�ܣ�����ϵϵͳ����Ա��");
            }
           
        }
        #endregion
    }
}
