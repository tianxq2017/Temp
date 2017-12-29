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
using System.IO;
using UNV.Comm.Web;
using UNV.Comm.DataBase;
using UNV.Comm.Upload;
using System.Text;
namespace join.pms.web.Disk
{
    public partial class uploadDisk : System.Web.UI.Page
    {
        private string m_SqlParams;
        private string m_ClassCode;
        private string m_UserID;
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            m_ClassCode=PageValidate.GetFilterSQL(Request.QueryString["code"]);
            if (!IsPostBack)
            {
                getDir();
                //DbProviderFactory factory = DbProviderFactories.GetFactory(_ProviderName); 

            }
        }

        #region 上传调用方法
        /// <summary>
        /// 绑定文件目录
        /// </summary>
        private void getDir()
        {
            StringBuilder sb = new StringBuilder();
            string sql = string.Empty;
            sql = "select * from UserHD_Directory where len(ClassCode)=8"; //根目录
            DataTable dt = new DataTable();
            DataTable dts = new DataTable();
            dt = DbHelperSQL.Query(sql).Tables[0];
            sb.Append("<select id=\"dir\" name=\"dir\" width=\"150px\">");
            sb.Append("<option selected=\"selected\" value=\"0\">--根目录--</option>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<option value=\"" + dt.Rows[i]["DirID"] + "\">" + dt.Rows[i]["UserDirName"] + "</option>");
                sql = "select * from UserHD_Directory where ";
                sql += "SubString(ClassCode,1,len('" + dt.Rows[i]["ClassCode"] + "'))='" + dt.Rows[i]["ClassCode"] + "' ";
                sql += "and len(ClassCode)<>len('" + dt.Rows[i]["ClassCode"] + "')"; //子目录
                dts = DbHelperSQL.Query(sql).Tables[0];
                for (int j = 0; j < dts.Rows.Count; j++)
                {
                    sb.Append("<option value=\"" + dts.Rows[j]["DirID"] + "\">");
                    sb.Append(dt.Rows[i]["UserDirName"].ToString() + " / " + dts.Rows[j]["UserDirName"].ToString());
                    sb.Append("</option>");
                }
            }
            sb.Append("</select>");
            this.dirList.Text = sb.ToString();
        }
        private string GeServerPath()
        {
            string serverPath = Server.MapPath(".") + "/";

            if (serverPath.Length > 5)
            {
                if (serverPath.ToLower().Substring(serverPath.Length - 6) == "admin/")
                {
                    serverPath = serverPath.Substring(0, serverPath.Length - 6);
                }
            }
            return serverPath;
        }
        /// <summary>
        /// 检验是否是允许上传的文件类型
        /// </summary>
        /// <param name="uploadFileType"></param>
        /// <returns></returns>
        private bool IsAllowedFiles(string uploadFileType, string fullPath)
        {
            bool isValidFile = false;
            string[] allowFiles ={ ".gif", ".bmp", ".jpg", ".zip", ".rar", ".doc", ".xls", ".txt",".pdf", ".swf", ".wmv", ".exe", ".pdf", ".mp3", ".rm" };
            for (int i = 0; i < allowFiles.Length; i++)
            {
                if (uploadFileType == allowFiles[i])
                {
                    isValidFile = true;
                    break;
                }
            }
            try
            {
                if (!System.IO.Directory.Exists(fullPath))
                {
                    System.IO.Directory.CreateDirectory(fullPath);
                }
            }
            catch
            {
                isValidFile = false;
            }

            return isValidFile;
        }
        /// <summary>
        /// 将上传文件信息记录数据库
        /// </summary>
        /// <param name="docsType">文件类型</param>
        /// <param name="docsPath">文件路径</param>
        /// <param name="sourceName">文件名称</param>
        private void SaveFileToDB(string docsType, string docsPath, string sourceName)
        {
            string classCode = string.Empty;
            string dirID = string.Empty;
            dirID = Request["dir"];
            classCode = DbHelperSQL.GetSingle("select ClassCode from UserHD_Directory where DirID=" + dirID).ToString();

            //int FileID = DbHelperSQL.GetMaxID("FileID", "UserHD_Files");
            m_SqlParams = "INSERT INTO [UserHD_Files] (FileName,FilePath,FileType,ClassCode,OprateUserID,CreateDate,FileStatus,DirID) ";
            m_SqlParams += "VALUES('" + sourceName + "','" + docsPath + "','" + docsType + "','" + classCode + "'," + m_UserID + ",'" + DateTime.Now + "',0," + dirID + ")";
            try { DbHelperSQL.ExecuteSql(m_SqlParams); }
            catch { ;}
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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/loginTemp.aspx';</script>");
                Response.End();
            }
        }
        #endregion

        protected void ButUploadFile_Load(object sender, EventArgs e)
        {
            Button m_button = sender as Button;
            UNV.Comm.Upload.WebUpload m_upload = new WebUpload();
            m_upload.RegisterProgressBar(m_button, false);
        }
        protected void ButUploadFile_Click(object sender, EventArgs e)
        {
            string docsType = string.Empty;
            string sourceFile = string.Empty; // 原始文件名
            //string configPath=System.Configuration.ConfigurationManager.AppSettings["FCKeditor:UserFilesPath"];//文件存放路径
            string dirID=string.Empty;
            string sql = string.Empty;
            dirID=Request["dir"];
            if (String.Compare(dirID, "0") == 0) 
            {
                MessageBox.Show(this,"对不起，根目录下不能上传文件，请选择字目录！");
                getDir();
                return;
            }
            sql = "select SysDirName from UserHD_Directory where DirID=" + dirID;
            string configPath=DbHelperSQL.GetSingle(sql).ToString();
            string targetFile = StringProcess.GetCurDateTimeStr(16); // 数据库保存的文件名
            string fullPath = configPath + StringProcess.GetCurDateTimeStr(6) + "/";
            string serverPath = GeServerPath();
            string dispMsg = string.Empty;

            WebUpload m_upload = new WebUpload();
            UploadFile m_file = m_upload.GetUploadFile("UploadFiles");
            sourceFile = Path.GetFileName(m_file.FullPathOnClient);
            docsType = System.IO.Path.GetExtension(sourceFile).ToLower();//文件类型（扩展名）
            if (IsAllowedFiles(docsType, serverPath + fullPath))
            {
                try
                {
                    m_file.MoveTo(serverPath + fullPath + targetFile + docsType);
                    SaveFileToDB(docsType, fullPath + targetFile + docsType, sourceFile);//".gif", ".bmp", ".jpg"
                    //dispMsg = "<div id=\"oprateUpFiles\">文件[ " + sourceFile + " ]上传成功！<br/>请<a href=\"javascript:SetFCKeditorDocs('" + sourceFile + "','" + fullPath + targetFile + docsType + "','" + docsType + "','ist')\"><b><font color=red size=3>单击此处</font></b></a>插入到编辑器中；";
                    //this.LiteralMsg.Text = dispMsg;
                    //this.labState.Text = "The File upload Sucess!";
                    //this.labDetial.Text = "<b>The file Original path:</b>" + this.FileUpload1.PostedFile.FileName + "<br/><b>The file size:</b>" + this.FileUpload1.PostedFile.ContentLength + "字节<br/><b>The File Type:</b>" + this.FileUpload1.PostedFile.ContentType + "<br/>";
                }
                catch (Exception ex)
                {
                    this.msg.Text = "文件上传失败：" + ex.Message;
                }
            }
            else
            {
                this.msg.Text = "<font color=red>文件类型不正确，请确认文件类型！</font>";
            }
        }
    }
}
