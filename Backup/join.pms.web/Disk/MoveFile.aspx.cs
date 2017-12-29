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
using System.Text;

namespace join.pms.web.Disk
{
    public partial class MoveFile : System.Web.UI.Page
    {
        private string m_ObjID;
        private string m_FuncNo;
        private string m_FuncUser;

        private string m_UserID;
        private string m_Action;

        private string m_SqlParams;
        private DataTable m_Dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateParams();
            AuthenticateUser();

            
            if (!IsPostBack)
            {
                if (String.IsNullOrEmpty(m_ObjID) || String.IsNullOrEmpty(m_Action))
                {
                    Response.Write("非法访问：操作被终止！");
                    Response.End();
                }
                else
                {
                    GetDirList(m_FuncNo, "");
                }
            }
        }

        private void ValidateParams() {
            m_FuncUser = PageValidate.GetFilterSQL(Request.QueryString["FuncUser"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["id"]);
            m_FuncNo = PageValidate.GetFilterSQL(Request.QueryString["FuncCode"]);
            m_Action = PageValidate.GetFilterSQL(Request.QueryString["action"]);
        }

        /// <summary>
        /// 身份验证 PageValidate
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
        /// 获取目录列表,
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private void GetDirList(string funcNo, string directoryID)
        {
            StringBuilder sbDirs = new StringBuilder();
            m_SqlParams = "SELECT [DirID], [ParentDirID], [DirCode],[UserDirName], [SysDirName], [OprateUserID], [CreateDate], [DirStatus], [ClassCode],(CASE LEN(DirCode) WHEN 1 THEN UserDirName WHEN 2 THEN '|--'+UserDirName WHEN 4 THEN '|--+--'+UserDirName WHEN 6 THEN '|--+--'+UserDirName ELSE '|--+--+--'+UserDirName END) AS UserDirNameCN FROM [UserHD_Directory] WHERE (OprateUserID=" + m_UserID + " AND DirStatus!=4 AND ClassCode='" + this.m_FuncNo + "') OR DirID=1 ORDER BY DirID";
            m_Dt = new DataTable();
            try
            {
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                sbDirs.Append("<select id=\"selDir\" name=\"selDir\" >");
                for (int i = 0; i < m_Dt.Rows.Count; i++)
                {
                    if (m_Dt.Rows[i]["DirID"].ToString() == directoryID)
                    {
                        sbDirs.Append("<option value=\"" + m_Dt.Rows[i]["DirID"] + "\" selected=selected>" + m_Dt.Rows[i]["UserDirNameCN"] + "</option>");
                    }
                    else
                    {
                        sbDirs.Append("<option value=\"" + m_Dt.Rows[i]["DirID"] + "\">" + m_Dt.Rows[i]["UserDirNameCN"] + "</option>");
                    }

                }
                sbDirs.Append("</select>");
                
            }
            catch (Exception ex)
            {
                sbDirs.Append(ex.Message);
            }
            m_Dt = null;

            this.LiteralSel.Text = sbDirs.ToString();
        }

        protected void butSubmit_Click(object sender, EventArgs e)
        {
            string directoryID = Request["selDir"];
            if (!string.IsNullOrEmpty(directoryID))
            {
                if (m_Action == "1") // 移动文件夹
                {
                    m_SqlParams = "UPDATE UserHD_Directory SET ParentDirID=" + directoryID + " WHERE DirID=" + m_ObjID;
                } 
                else 
                {
                    m_SqlParams = "UPDATE UserHD_Files SET DirID=" + directoryID + " WHERE FileID=" + m_ObjID;
                }
                DbHelperSQL.ExecuteSql(m_SqlParams);
                Response.Write("<script language='javascript'>window.close();window.returnValue=\"xx\";</script>");
            }
            else 
            {
                MessageBox.Show(this,"请选择要移动到的文件夹名称！");
            }
        }
    }
}
