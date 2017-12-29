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
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Drawing;
namespace join.pms.web.Disk
{
    public partial class NewAddDirectory : System.Web.UI.Page
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
                    GetDirList(m_FuncNo, m_ObjID);
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
            //新增文件夹  ClassCode='" + this.m_FuncNo + "'
            string strErr = string.Empty;
            string selectDir = Request["selDir"];
            string userDirName = PageValidate.GetTrim(this.txtDirectoryName.Text);
            string sysDirName = m_UserID + StringProcess.GetCurDateTimeStr(16);
            string subDirNum = DbHelperSQL.GetSingle("SELECT COUNT(*) FROM UserHD_Directory WHERE ParentDirID=" + selectDir + " AND DirStatus!=4 AND OprateUserID=" + m_UserID + " AND ClassCode='" + this.m_FuncNo + "'").ToString();

            if (String.IsNullOrEmpty(userDirName))
            {
                strErr += "文件夹名称不能为空！ \\n";
            }
            if (int.Parse(subDirNum) > 99)
            {
                strErr += "对不起，您的子文件夹个数已经超过系统最大！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                GetDirList(m_FuncNo, m_ObjID);
                return;
            }

            m_SqlParams = "INSERT INTO UserHD_Directory(ParentDirID, UserDirName, SysDirName, OprateUserID, ClassCode,DirCode) ";
            m_SqlParams += "VALUES(" + selectDir + ",'" + userDirName + "','" + sysDirName + "'," + m_UserID + ",'" + m_FuncNo + "','" + GetClassCode(selectDir) + "')";
            try
            {
                DbHelperSQL.ExecuteSql(m_SqlParams);
                Response.Write("<script language='javascript'>window.close();window.returnValue=\"xx\";</script>");
            }
            catch
            {

            }
        }

        /// <summary>
        /// 获取新的编码字符
        /// </summary>
        /// <param name="rootCode"></param>
        /// <returns></returns>
        private string GetClassCode(string directoryID)
        {
            string newCode, rootCode = string.Empty;
            m_SqlParams = "SELECT DirCode FROM UserHD_Directory WHERE DirID="+directoryID;
            string dirCode, sonNodeMaxCode = string.Empty;
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query("SELECT DirCode FROM UserHD_Directory WHERE (DirID =" + directoryID + " OR ParentDirID=" + directoryID + ")  ORDER BY DirCode").Tables[0];
            if (m_Dt.Rows.Count > 1) 
            {
                dirCode = PageValidate.GetTrim(m_Dt.Rows[0][0].ToString());
                sonNodeMaxCode = PageValidate.GetTrim(m_Dt.Rows[m_Dt.Rows.Count - 1][0].ToString());
            } 
            else 
            {
                dirCode = PageValidate.GetTrim(m_Dt.Rows[0][0].ToString());
                sonNodeMaxCode = "";
            }
            if (directoryID == "1") { rootCode = ""; } else { rootCode = dirCode; }
            if (String.IsNullOrEmpty(sonNodeMaxCode))
            {
                newCode = rootCode + "01";
            }
            else
            {
                sonNodeMaxCode = sonNodeMaxCode.Substring(sonNodeMaxCode.Length - 2);
                int sonCode = int.Parse(sonNodeMaxCode);
                sonCode = sonCode + 1;
                if (sonCode > 9)
                {
                    newCode = rootCode + sonCode.ToString();
                }
                else
                {
                    newCode = rootCode + sonCode.ToString().PadLeft(2, '0');
                }
            }
            return newCode;
        }

    }
}
