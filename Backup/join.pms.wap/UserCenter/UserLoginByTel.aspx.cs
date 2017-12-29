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
    public partial class UserLoginByTel : UNV.Comm.Web.PageBase
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

             string PersonTel = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserMobile.Text));
             string PhonePass = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserSmsCode.Value));

            string strErr = string.Empty;

            if (String.IsNullOrEmpty(PersonTel))
            {
                strErr += "�������ֻ����룡\\n";
            }
            if (!CommPage.IsAllowLogin(PersonTel, loginIP)) { strErr += "�����Ե�½�Ĵ����������ƴ���������ϵϵͳ����Ա��\\n"; }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            #region �ֻ���֤
            string strUserMobile = "", strPhonePass = string.Empty;
            if (Request.Browser.Cookies)
            {
                HttpCookie reMobileInfo = Request.Cookies["Re_MobileInfo_Web"];
                if (reMobileInfo != null && !String.IsNullOrEmpty(reMobileInfo.Values["Re_Mobile"].ToString()))
                {
                    strUserMobile = reMobileInfo.Values["Re_Mobile"].ToString();
                    strPhonePass = reMobileInfo.Values["Re_Pass"].ToString();
                }
            }
            else
            {
                if (Session["Re_Mobile"] != null && !String.IsNullOrEmpty(Session["Re_Mobile"].ToString()))
                {
                    strUserMobile = Session["Re_Mobile"].ToString();
                    strPhonePass = Session["Re_Pass"].ToString();
                }
            }
            if (String.IsNullOrEmpty(PhonePass) || strPhonePass == null || PersonTel != strUserMobile || PhonePass != strPhonePass)
            {
                strErr += "�ֻ���֤������������������룡\\n";
            }
            #endregion

            SqlDataReader sdr = null;

            try
            {
                sqlParams = "SELECT PersonID,PersonAcc FROM BIZ_Persons WHERE PersonTel='" + PersonTel + "' AND Attribs!=0";
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
                    CommPage.SetSysTryLogin(PersonTel, loginIP, "ʧ��");
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
