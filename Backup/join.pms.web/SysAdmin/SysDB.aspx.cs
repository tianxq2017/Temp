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

            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">��ʼҳ</a> &gt;&gt; ϵͳ��ȫ&gt;&gt; ϵͳ���ݿⱸ�ݣ�";
        }

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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        protected void ButtonDbBak_Click(object sender, EventArgs e)
        {
            string sqlParams = string.Empty;
            string dbName = "NMG_TLS_KLQ_OC_DB";

            //string configPath = System.Configuration.ConfigurationManager.AppSettings["FCKeditor:UserFilesPath"];
            string bakPath = Server.MapPath("/") + "Files/DataBaseBak/" + StringProcess.GetCurDateTimeStr(6) + "/";
            string bakFiles = bakPath + StringProcess.GetCurDateTimeStr(16) + ".bak"; // ���ݿⱣ����ļ���


            try
            {
                if (!Directory.Exists(bakPath)) Directory.CreateDirectory(bakPath);
                //�������ݿ�
                sqlParams = "BACKUP DATABASE " + dbName + " TO DISK='" + bakFiles + "'";
                DbHelperSQL.ExecuteSql(sqlParams);

                this.LiteralMsg.Text = "ϵͳ���ݿ�ɹ�������[ " + bakFiles + " ]";
            }
            catch (Exception ex) { this.LiteralMsg.Text = ex.Message; }
            /*
            //-------------------ɱ���������� db_PWMS ���ݿ�Ľ���-------------- 
                 string strSQL = "select spid from master..sysprocesses where dbid=db_id( '���ݿ���') "; 
                 SqlDataAdapter Da = new SqlDataAdapter(strSQL, conn); 
                 DataTable spidTable = new DataTable(); 
                 Da.Fill(spidTable); 
                 SqlCommand Cmd = new SqlCommand(); 
                 Cmd.CommandType = CommandType.Text; 
                 Cmd.Connection = conn; 
                 if (spidTable.Rows.Count > 1) 
                 {//ǿ�йرշǱ�����ʹ�õ������û����� 
                     for (int iRow = 0; iRow < spidTable.Rows.Count - 1; iRow++) 
                     { 
                         Cmd.CommandText = "kill " + spidTable.Rows[iRow][0].ToString();   //ǿ�йر��û����� 
                         Cmd.ExecuteNonQuery(); 
                     } 
                 } 
                 conn.Close(); 
                 conn.Dispose(); 
------------------------------------------------
            //�ָ����ݿ�
string str = "use master restore database ���ݿ��� from Disk='" + txtPath.Text.Trim() + "'"; 
             */

        }
    }
}

