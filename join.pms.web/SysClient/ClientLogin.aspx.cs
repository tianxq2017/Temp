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

namespace join.pms.web.SysClient
{
    public partial class ClientLogin : UNV.Comm.Web.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // UID="+UserInfo.UserID+"&UNA
            ValidateParams();
        }

        /// <summary>
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            // UID="+UserInfo.UserID+"&UNA
            string encParams = PageValidate.GetTrim(Request.QueryString["p"]);
            string decParams = string.Empty;
            string userID = string.Empty, userAcc = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(encParams))
                {
                    decParams = DESEncrypt.Decrypt(encParams);
                    userID = StringProcess.AnalysisParas(decParams, "UID");
                    userAcc = StringProcess.AnalysisParas(decParams, "UNA");

                    if (!string.IsNullOrEmpty(userID))
                    {
                        if (!PageValidate.IsNumber(userID)) Server.Transfer("~/errors.aspx");

                        SetUserLoginInfo(userID);
                        DbHelperSQL.SetSysLog(userID, Request.UserHostAddress, "uKey��¼", "�û�[" + userAcc + "]�� " + DateTime.Now.ToString() + " ͨ���ͻ��˵�¼ϵͳ");
                        DbHelperSQL.ExecuteSql("UPDATE [USER_BaseInfo] SET [UserLastLoginTime]=GetDate(),UserLoginNum=UserLoginNum+1 WHERE UserID=" + userID);

                        Response.Redirect("/YslRyLxJKc9P8xcVA6KVWDqwusnGNxCIHvckPEBEr2Eched9eA0AQF6ryC6f7HWx0R." + this.m_FileExt, true);
                        /// http://localhost:9997/YslRyLxJKc9P8xcVA6KVWDqwusnGNxCIHvckPEBEr2Eched9eA0AQF6ryC6f7HWx0R.shtml
                    }
                    else { Response.Write("<script language='javascript'>alert('����ʧ�ܣ��Ƿ�������');window.close();</script>"); }
                }
            }
            catch { Response.Write("<script language='javascript'>alert('����ʧ�ܣ��Ƿ�������');window.close();</script>"); }
        }

        /// <summary>
        /// ���ò������û���½��Ϣ
        /// </summary>
        /// <param name="userID"></param>
        private void SetUserLoginInfo(string userID)
        {
            //�����û���½��Ϣcookie
            if (Request.Browser.Cookies)
            {
                HttpCookie cookie = new HttpCookie("AREWEB_OC_USER_YSL");
                cookie.Values.Add("UserID", userID);
                Response.AppendCookie(cookie);
                cookie.Expires = DateTime.Now.AddHours(2); //cookie����ʱ��
            }
            else
            {
                Session["AREWEB_OC_USERID"] = userID;
            }
        }
    }
}
