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
    public partial class UserRegister : UNV.Comm.Web.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Uc_PageTop1.GetSysMenu("Ⱥ��ע��");
        }
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
                cookie.Expires = System.DateTime.Now.AddHours(4);
            }
            else
            {
                Session["UserID"] = userInfo;
            }
        }
        #endregion

        #region Ⱥ��ע��
        /// <summary>
        /// Ⱥ��ע��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReg_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string targetUrl = string.Empty;
            string PersonAcc = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserAccount.Text));// CommPage.GetUserNo();
            string PersonPwd = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserPwd.Text));
            string PersonPwdRe = PageValidate.GetTrim(this.txtUserPwdRe.Text);
            string PersonName = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserName.Text));
            string PersonCardID = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserCardID.Text));
            string PersonTel = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserMobile.Text));
            string PhonePass = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserSmsCode.Value));

            if (string.IsNullOrEmpty(PersonAcc)) { strErr += "�������¼���ƣ�\\n"; }
            if (string.IsNullOrEmpty(PersonName)) { strErr += "������������\\n"; }
            if (string.IsNullOrEmpty(PersonPwd)) { strErr += "��¼���벻��Ϊ�գ�\\n"; }
            if (string.IsNullOrEmpty(PersonPwdRe)) { strErr += "������ȷ�����룡\\n"; }
            if (!string.IsNullOrEmpty(PersonPwd) && !string.IsNullOrEmpty(PersonPwdRe))
            {
                if (PersonPwd != PersonPwdRe) strErr += "��������������벻һ�£����������룡\\n";
            }
            if (string.IsNullOrEmpty(PersonName))
            {
                strErr += "������������\\n";
            }
            if (string.IsNullOrEmpty(PersonCardID))
            {
                strErr += "�������������֤�ţ�\\n";
            }
            else
            {
                if (!ValidIDCard.VerifyIDCard(PersonCardID)) { strErr += "���֤�����󣡣�\\n"; }
                //if (DbHelperSQL.GetSingle("SELECT COUNT(*) FROM BIZ_Persons WHERE PersonCardID='" + PersonCardID + "'").ToString() != "0")
                //{
                //    strErr += "�Ѿ�����ͬ���ĸ����û����֤��[ " + PersonCardID + " ]����ֱ�ӵ�¼��\\n";
                //}
            }
            if (string.IsNullOrEmpty(PersonTel))
            {
                strErr += "��������ϵ�绰��\\n";
            }
            //string identityCode = PageValidate.GetTrim(Request["txtCheckCode"]);
            //if (identityCode == "")
            //{
            //    strErr += "��������֤�룡\\n";
            //}
            //if (String.IsNullOrEmpty(identityCode) || Session["CheckCode"] == null || identityCode.Trim().ToLower() != Session["CheckCode"].ToString().ToLower())
            //{
            //    strErr += "��֤������������������룡\\n";
            //}
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
            if (cbOk.Checked == false)
            {
                strErr += "���Ķ���ͬ�⡶�û�ע�����Э�顷��\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            try
            {
                string PersonSex = string.Empty;
                string PersonBirthday = CommBiz.GetBirthdayAndSex(PersonCardID, ref PersonSex);

                // ����ǰ����Ƿ����ͬ���ʻ�
                if (DbHelperSQL.GetSingle("SELECT COUNT(*) FROM BIZ_Persons WHERE PersonAcc='" + PersonAcc + "'").ToString() == "0")
                {
                    string PersonID = CommPage.GetSingleVal("SELECT PersonID FROM BIZ_Persons WHERE PersonCardID='" + PersonCardID + "'");
                    // ����ǰ����Ƿ�����ظ����֤��
                    if (String.IsNullOrEmpty(PersonID))
                    {
                        // ����ǰ����Ƿ�����ظ��ֻ���
                        if (DbHelperSQL.GetSingle("SELECT COUNT(*) FROM BIZ_Persons WHERE PersonTel='" + PersonTel + "'").ToString() == "0")
                        {
                            PersonPwd = DESEncrypt.GetMD5_32(PersonPwd);
                            string sqlParams = "INSERT INTO BIZ_Persons(PersonAcc,PersonPwd,PersonTel,PersonName,PersonCardID,PersonSex,PersonBirthday,Attribs)";
                            sqlParams += "VALUES('" + PersonAcc + "','" + PersonPwd + "','" + PersonTel + "','" + PersonName + "','" + PersonCardID + "','" + PersonSex + "','" + PersonBirthday + "',1) ";
                            sqlParams += "SELECT SCOPE_IDENTITY()";
                            string userID = DbHelperSQL.GetSingle(sqlParams).ToString();
                            SetUserLoginIn(userID);
                            MessageBox.ShowAndRedirect(this.Page, "", "/OC." + m_FileExt, false, true);
                        }
                        else
                        {
                            MessageBox.ShowAndRedirect(this.Page, "��ʾ��Ϣ���ֻ���[ " + PersonTel + " ]�Ѿ����ڣ��뵽��¼ҳ����á�ʹ���ֻ����롱��¼��", "/YslXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdxx." + m_FileExt, false, true);
                        }
                    }
                    else
                    {

                        // ����ǰ����Ƿ�����ظ��ֻ���
                        if (DbHelperSQL.GetSingle("SELECT COUNT(*) FROM BIZ_Persons WHERE PersonTel='" + PersonTel + "' AND PersonID=" + PersonID).ToString() == "0")
                        {
                            PersonPwd = DESEncrypt.GetMD5_32(PersonPwd);
                            string sqlParams = "UPDATE BIZ_Persons SET PersonAcc='" + PersonAcc + "',PersonPwd='" + PersonPwd + "',PersonTel='" + PersonTel + "',PersonName='" + PersonName + "',Attribs=1 WHERE PersonID=" + PersonID;
                            DbHelperSQL.ExecuteSql(sqlParams).ToString();

                            SetUserLoginIn(PersonID);
                            MessageBox.ShowAndRedirect(this.Page, "", "/OC." + m_FileExt, false, true);
                        }
                        else
                        {
                            MessageBox.ShowAndRedirect(this.Page, "��ʾ��Ϣ���ֻ���[ " + PersonTel + " ]�Ѿ����ڣ��뵽��¼ҳ����á�ʹ���ֻ����롱��¼��", "/YslXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdxx." + m_FileExt, false, true);
                        }
                        //MessageBox.Show(this, "��ʾ��Ϣ�����֤��[ " + PersonCardID + " ]�Ѿ����ڣ���ֹ������֤����ͬ���û���");
                        //return;
                    }
                }
                else
                {
                    MessageBox.Show(this, "��ʾ��Ϣ����¼��[ " + PersonAcc + " ]�Ѿ����ڣ���ֹ��ӵ�¼����ͬ���û���");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, strErr);
                return;
            }
        }
        #endregion
    }
}
