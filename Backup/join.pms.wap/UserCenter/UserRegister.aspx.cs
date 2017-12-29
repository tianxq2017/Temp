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
            this.Uc_PageTop1.GetSysMenu("群众注册");
        }
        #region 设置并保存用户登陆信息
        /// <summary>
        /// 设置并保存用户登陆信息
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

        #region 群众注册
        /// <summary>
        /// 群众注册
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

            if (string.IsNullOrEmpty(PersonAcc)) { strErr += "请输入登录名称！\\n"; }
            if (string.IsNullOrEmpty(PersonName)) { strErr += "请输入姓名！\\n"; }
            if (string.IsNullOrEmpty(PersonPwd)) { strErr += "登录密码不能为空！\\n"; }
            if (string.IsNullOrEmpty(PersonPwdRe)) { strErr += "请输入确认密码！\\n"; }
            if (!string.IsNullOrEmpty(PersonPwd) && !string.IsNullOrEmpty(PersonPwdRe))
            {
                if (PersonPwd != PersonPwdRe) strErr += "您两次输入的密码不一致，请重新输入！\\n";
            }
            if (string.IsNullOrEmpty(PersonName))
            {
                strErr += "请输入姓名！\\n";
            }
            if (string.IsNullOrEmpty(PersonCardID))
            {
                strErr += "请输入您的身份证号！\\n";
            }
            else
            {
                if (!ValidIDCard.VerifyIDCard(PersonCardID)) { strErr += "身份证号有误！！\\n"; }
                //if (DbHelperSQL.GetSingle("SELECT COUNT(*) FROM BIZ_Persons WHERE PersonCardID='" + PersonCardID + "'").ToString() != "0")
                //{
                //    strErr += "已经存在同样的个人用户身份证号[ " + PersonCardID + " ]，请直接登录！\\n";
                //}
            }
            if (string.IsNullOrEmpty(PersonTel))
            {
                strErr += "请输入联系电话！\\n";
            }
            //string identityCode = PageValidate.GetTrim(Request["txtCheckCode"]);
            //if (identityCode == "")
            //{
            //    strErr += "请输入验证码！\\n";
            //}
            //if (String.IsNullOrEmpty(identityCode) || Session["CheckCode"] == null || identityCode.Trim().ToLower() != Session["CheckCode"].ToString().ToLower())
            //{
            //    strErr += "验证码输入错误，请重新输入！\\n";
            //}
            #region 手机验证
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
                strErr += "手机验证码输入错误，请重新输入！\\n";
            }
            #endregion
            if (cbOk.Checked == false)
            {
                strErr += "请阅读并同意《用户注册许可协议》！\\n";
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

                // 插入前检测是否存在同名帐户
                if (DbHelperSQL.GetSingle("SELECT COUNT(*) FROM BIZ_Persons WHERE PersonAcc='" + PersonAcc + "'").ToString() == "0")
                {
                    string PersonID = CommPage.GetSingleVal("SELECT PersonID FROM BIZ_Persons WHERE PersonCardID='" + PersonCardID + "'");
                    // 插入前检测是否存在重复身份证号
                    if (String.IsNullOrEmpty(PersonID))
                    {
                        // 插入前检测是否存在重复手机号
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
                            MessageBox.ShowAndRedirect(this.Page, "提示信息：手机号[ " + PersonTel + " ]已经存在，请到登录页面采用“使用手机号码”登录！", "/YslXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdxx." + m_FileExt, false, true);
                        }
                    }
                    else
                    {

                        // 插入前检测是否存在重复手机号
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
                            MessageBox.ShowAndRedirect(this.Page, "提示信息：手机号[ " + PersonTel + " ]已经存在，请到登录页面采用“使用手机号码”登录！", "/YslXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdxx." + m_FileExt, false, true);
                        }
                        //MessageBox.Show(this, "提示信息：身份证号[ " + PersonCardID + " ]已经存在，禁止添加身份证号相同的用户！");
                        //return;
                    }
                }
                else
                {
                    MessageBox.Show(this, "提示信息：登录名[ " + PersonAcc + " ]已经存在，禁止添加登录名相同的用户！");
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
