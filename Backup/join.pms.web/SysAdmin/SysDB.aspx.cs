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

using System.IO;

namespace join.pms.web.SysAdmin
{
    public partial class SysDB : System.Web.UI.Page
    {
        private string m_UserID;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();

            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">起始页</a> &gt;&gt; 系统安全&gt;&gt; 系统数据库备份：";
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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        protected void ButtonDbBak_Click(object sender, EventArgs e)
        {
            string sqlParams = string.Empty;
            string dbName = "NMG_TLS_KLQ_OC_DB";

            //string configPath = System.Configuration.ConfigurationManager.AppSettings["FCKeditor:UserFilesPath"];
            string bakPath = Server.MapPath("/") + "Files/DataBaseBak/" + StringProcess.GetCurDateTimeStr(6) + "/";
            string bakFiles = bakPath + StringProcess.GetCurDateTimeStr(16) + ".bak"; // 数据库保存的文件名


            try
            {
                if (!Directory.Exists(bakPath)) Directory.CreateDirectory(bakPath);
                //备份数据库
                sqlParams = "BACKUP DATABASE " + dbName + " TO DISK='" + bakFiles + "'";
                DbHelperSQL.ExecuteSql(sqlParams);

                this.LiteralMsg.Text = "系统数据库成功备份至[ " + bakFiles + " ]";
            }
            catch (Exception ex) { this.LiteralMsg.Text = ex.Message; }
            /*
            //-------------------杀掉所有连接 db_PWMS 数据库的进程-------------- 
                 string strSQL = "select spid from master..sysprocesses where dbid=db_id( '数据库名') "; 
                 SqlDataAdapter Da = new SqlDataAdapter(strSQL, conn); 
                 DataTable spidTable = new DataTable(); 
                 Da.Fill(spidTable); 
                 SqlCommand Cmd = new SqlCommand(); 
                 Cmd.CommandType = CommandType.Text; 
                 Cmd.Connection = conn; 
                 if (spidTable.Rows.Count > 1) 
                 {//强行关闭非本程序使用的所有用户进程 
                     for (int iRow = 0; iRow < spidTable.Rows.Count - 1; iRow++) 
                     { 
                         Cmd.CommandText = "kill " + spidTable.Rows[iRow][0].ToString();   //强行关闭用户进程 
                         Cmd.ExecuteNonQuery(); 
                     } 
                 } 
                 conn.Close(); 
                 conn.Dispose(); 
------------------------------------------------
            //恢复数据库
string str = "use master restore database 数据库名 from Disk='" + txtPath.Text.Trim() + "'"; 
             */

        }
    }
}

