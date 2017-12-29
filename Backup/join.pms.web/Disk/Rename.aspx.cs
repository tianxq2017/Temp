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
namespace join.pms.web.Disk
{
    public partial class Rename : System.Web.UI.Page
    {
        private string m_ObjID;
        private string m_FuncNo;
        private string m_FuncUser;

        private string m_UserID;
        private string m_Action;
        private string m_SqlParams;

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateParams();
            AuthenticateUser();

            if (!IsPostBack)
            {
                if (String.IsNullOrEmpty(m_ObjID) || String.IsNullOrEmpty(m_Action) )
                {
                    Response.Write("非法访问：操作被终止！");
                    Response.End();
                }
                else
                {
                    GetObjName();
                }
            }
        }

        private void ValidateParams()
        {
            m_FuncUser = PageValidate.GetFilterSQL(Request.QueryString["FuncUser"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["id"]);
            m_FuncNo = PageValidate.GetFilterSQL(Request.QueryString["FuncCode"]);
            m_Action = PageValidate.GetFilterSQL(Request.QueryString["action"]);
        }

        /// <summary>
        /// 身份验证
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
                string pageUrl = Request.RawUrl;
                if (!string.IsNullOrEmpty(pageUrl))
                {
                    if (pageUrl.IndexOf("FuncUser") > 5)
                    {

                        m_UserID = DESEncrypt.Decrypt(m_FuncUser);
                        SetUserLoginInfo(m_UserID);
                    }
                    else
                    {
                        Response.Write("操作失败：超时退出，请重新登录后再试！</script>");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("操作失败：超时退出，请重新登录后再试！</script>");
                    Response.End();
                }
            }
        }
        /// 设置并保存用户登陆信息
        /// </summary>
        /// <param name="userID"></param>
        private void SetUserLoginInfo(string userID)
        {
            //设置用户登陆信息cookie
            if (Request.Browser.Cookies)
            {
                HttpCookie cookie = new HttpCookie("AREWEB_OC_USER_YSL");
                cookie.Values.Add("UserID", userID);
                Response.AppendCookie(cookie);
                cookie.Expires = DateTime.Now.AddHours(4); //cookie过期时间
            }
            else
            {
                Session["AREWEB_OC_USERID"] = userID;
            }
        }

        /// <summary>
        ///  获取该文件在数据库中的名称
        /// </summary>
        /// <param name="status"></param>
        /// <param name="ID"></param>
        private void GetObjName()
        {
            string dataName = string.Empty;
            string sql = string.Empty;

            if (m_Action=="2")
            {
                m_SqlParams = "select FileName from UserHD_Files where FileID=" + m_ObjID;
            }
            else 
            {
                m_SqlParams = "select UserDirName from UserHD_Directory where DirID=" + m_ObjID; 
            }

            this.txtDirectoryName.Text = DbHelperSQL.GetSingle(m_SqlParams).ToString();
        }

        protected void butSubmit_Click(object sender, EventArgs e)
        {
            string strErr = "";
            string objName = PageValidate.GetTrim(this.txtDirectoryName.Text);
            if (String.IsNullOrEmpty(objName))
            {
                strErr += "文件名称不能为空！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string sql = string.Empty;
            if (m_Action == "1")
            {
                m_SqlParams = "update UserHD_Directory set UserDirName='" + objName + "' where DirID=" + m_ObjID;
            }
            else 
            { 
                m_SqlParams = "update UserHD_Files set FileName='" + objName + "' where FileID=" + m_ObjID; 
            }
            DbHelperSQL.ExecuteSql(m_SqlParams);
            Response.Write("<script>window.close();window.returnValue=\"xx\";</script>");
        }
    }
}
