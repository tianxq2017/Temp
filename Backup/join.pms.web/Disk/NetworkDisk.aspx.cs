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
using System.Text;
using UNV.Comm.DataBase;
using UNV.Comm.Web;
using UNV.Comm.Upload;
using join.pms.dal;

namespace join.pms.web.Disk
{
    public partial class NetworkDisk : System.Web.UI.Page
    {
        private string m_UserID;
        private string m_FuncNo;
        private string m_FuncUser;

        private string m_DirID;
        private string m_Action;
        private string m_TargetUrl;

        private string m_SqlParams;
        private DataTable m_Dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateParams();
            AuthenticateUser();

            if (!IsPostBack)
            {
                if (String.IsNullOrEmpty(m_FuncNo) || !PageValidate.IsNumber(m_FuncNo))
                {
                    Response.Write("非法访问：操作被终止！");
                    Response.End();
                }
                else
                {
                    m_TargetUrl = "NetworkDisk.aspx?FuncCode=" + m_FuncNo + "&FuncUser=" + m_FuncUser;
                    this.txtUrlParams.Value = m_TargetUrl;
                    this.txtFuncNo.Value = m_FuncNo;
                    this.txtFuncUser.Value = m_FuncUser;

                    this.LiteralNav.Text = GetPageTopNavInfo("网络硬盘", "");
                    this.LiteralTitle.Text = "网络硬盘";
                    this.LiteralFuncName.Text = "查看网络硬盘列表 &nbsp;&nbsp;&nbsp;&nbsp; " + GetUserFileSize();//this.FileUpload1.PostedFile.ContentLength
                    CommPage cp = new CommPage();
                    this.txtFuncPowers.Value = cp.GetFuncPower(m_UserID, m_FuncNo); // 改功能的权限集

                    if (!string.IsNullOrEmpty(m_DirID) && PageValidate.IsNumber(m_DirID) && m_Action == "changeDir")
                    {
                        GetDirList(m_FuncNo, m_DirID);
                        GetFileList(m_FuncNo, m_DirID);
                    }
                    else
                    {
                        // && PageValidate.IsNumber(m_DirID)
                        if (!string.IsNullOrEmpty(m_Action) && !string.IsNullOrEmpty(m_DirID))
                        {
                            DeleteSelectObj(m_DirID, m_Action);
                        }
                        GetDirList(m_FuncNo, "");
                        GetFileList(m_FuncNo, "1");// 默认显示用户根目录下的内容
                    }
                }
            }
        }

        private void ValidateParams()
        {
            m_FuncUser = PageValidate.GetFilterSQL(Request.QueryString["FuncUser"]);
            m_FuncNo = PageValidate.GetFilterSQL(Request.QueryString["FuncCode"]);
            m_DirID = PageValidate.GetFilterSQL(Request.QueryString["ID"]);//目录标识
            m_Action = PageValidate.GetFilterSQL(Request.QueryString["action"]);// changeDir
            if (!string.IsNullOrEmpty(m_DirID)) {
                if (m_DirID.Substring(m_DirID.Length - 1) == ",") m_DirID = m_DirID.Substring(0, m_DirID.Length - 1);
            }
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <returns></returns>
        private string GetUserFileSize() {
            string returnVa = string.Empty;
            try
            {
                string maxFileSize = DbHelperSQL.GetSingle("SELECT MaxFileSize FROM USER_BaseInfo WHERE UserID=" + m_UserID).ToString();
                string useFileSize = DbHelperSQL.GetSingle("SELECT SUM(FileSize) FROM UserHD_Files WHERE OprateUserID=" + m_UserID).ToString();
                if (string.IsNullOrEmpty(maxFileSize) || maxFileSize == "0") {
                    returnVa = "";
                } 
                else {
                    int useRate = (int.Parse(useFileSize) / int.Parse(maxFileSize)) * 100;
                    if (useRate > 100)
                    {
                        this.ButUploadFile.Enabled = false;
                    }
                    returnVa = "当前用户最大网盘容量为[ " + maxFileSize + " ]字节；已使用[ " + useFileSize + " ]字节；空间使用率 " + useRate.ToString() + "% ";
                }
            }
            catch { }
            return returnVa;
        }

        #region 页面导航信息

        /// <summary>
        /// 获取页面导航信息
        /// </summary>
        /// <param name="oprateName"></param>
        /// <param name="funcName"></param>
        /// <returns></returns>
        private string GetPageTopNavInfo(string oprateName, string funcName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" &gt; " + oprateName + " &gt; " + funcName + "");
            return sb.ToString();
        }
        #endregion
        #region 获取数据列表


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
                string pageUrl = Request.RawUrl;//&FuncCode=" + FuncCode + "&FuncUser
                if (!string.IsNullOrEmpty(pageUrl))
                {
                    if (pageUrl.IndexOf("FuncUser") > 5)
                    {

                        m_UserID = DESEncrypt.Decrypt(m_FuncUser);
                        SetUserLoginInfo(m_UserID);
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
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
        /// 条件过滤
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetFilterSQL(string funcNo)
        {
            string returnVa = string.Empty;
            // 0801 公共网盘;0802 我的网盘
            if (funcNo == "0801")
            {
                returnVa = "1=1";
            }
            else
            {
                returnVa = "OprateUserID="+m_UserID+" AND ClassCode=" + this.m_FuncNo;
            }
            return returnVa;
        }

        /// <summary>
        /// 获取目录列表, OprateUserID
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private void GetDirList(string funcNo, string directoryID)
        {
            string filterSQL = GetFilterSQL(funcNo);
            StringBuilder sbDirs = new StringBuilder();
            m_SqlParams = "SELECT [DirID], [ParentDirID], [DirCode],[UserDirName], [SysDirName], [OprateUserID], [CreateDate], [DirStatus], [ClassCode],(CASE LEN(DirCode) WHEN 1 THEN UserDirName WHEN 2 THEN '|--'+UserDirName WHEN 4 THEN '|--+--'+UserDirName WHEN 6 THEN '|--+--'+UserDirName ELSE '|--+--+--'+UserDirName END) AS UserDirNameCN FROM [UserHD_Directory] WHERE (" + filterSQL + " AND DirStatus!=4 AND ClassCode='" + this.m_FuncNo + "') OR DirID=1 ORDER BY DirID";
            m_Dt = new DataTable();
            try
            {
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                sbDirs.Append("<select id=\"selDir\" name=\"selDir\" onchange=\"gets('" + this.m_FuncNo + "',document.getElementById('selDir').value)\">");
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
            this.LiteralDirList.Text = sbDirs.ToString();
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="Code"></param>
        private void GetFileList(string funcNo, string directoryID)
        {
            string objID = string.Empty;
            string filterSQL = GetFilterSQL(funcNo);
            StringBuilder sbFiles = new StringBuilder();
            m_SqlParams = "SELECT [DirID], [ParentDirID], [UserDirName], [UserName],[CreateDate] FROM [v_UserHD_Directory] WHERE " + filterSQL + " AND ParentDirID=" + directoryID + " AND ClassCode='" + this.m_FuncNo + "' AND DirStatus!=4 ORDER BY DirCode";
            m_Dt = new DataTable();
            try
            {
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                sbFiles.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%;\">");
                sbFiles.Append("<tr style=\"background-color:#e5e5e5;color:#000000\">");
                sbFiles.Append("<td height=\"30\" width=\"40\"><input id=\"0\" name=\"itemsi\" onclick=\"javascript:SelectAll();\" type=\"checkbox\" /></td>");
                sbFiles.Append("<td>名称</td><td width=\"60\">类型</td><td width=\"80\">创建人</td><td>创建时间</td>");
                sbFiles.Append("</tr>");
                for (int i = 0; i < m_Dt.Rows.Count; i++)
                {
                    objID = m_Dt.Rows[i]["DirID"].ToString();
                    sbFiles.Append("<tr onclick=\"SetCheckBoxClear(" + objID + ")\" height=\"25\" onmouseover=\"this.className='lvtColDataHover'\" onmouseout=\"this.className='lvtColData'\">");
                    sbFiles.Append("<td><input value=\"" + objID + "\" id=\"" + i + "\" name=\"itemsCheck\" type=\"checkbox\" /><input id=\"txtHidden\" name=\"txtHidden\" value=\"1\" type=\"hidden\" /></td>");
                    sbFiles.Append("<td><img src=\"../images/NetHD_Folder.gif\" alt=\"文件夹\" align=\"texttop\" > <a href=\"NetworkDisk.aspx?FuncCode=" + this.m_FuncNo + "&FuncUser=" + m_FuncUser + "&ID=" + objID + "&action=changeDir\">" + m_Dt.Rows[i]["UserDirName"] + "</a></td>");
                    sbFiles.Append("<td>文件夹</td>");
                    sbFiles.Append("<td>" + m_Dt.Rows[i]["UserName"] + "</td>");
                    sbFiles.Append("<td>" + m_Dt.Rows[i]["CreateDate"] + "</td>");
                    sbFiles.Append("</tr>");
                }
                m_Dt = null;
                m_SqlParams = "SELECT [FileID], [FileName], [FilePath], [FileType],[CreateDate], [DirID], [DirCode], [UserDirName], [SysDirName], [UserName] FROM [v_UserHD_Files] WHERE " + filterSQL + " AND ClassCode='" + this.m_FuncNo + "' AND FileStatus!=4 AND DirID=" + directoryID + " ORDER BY CreateDate ASC";
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                for (int j = 0; j < m_Dt.Rows.Count; j++)
                {
                    objID = m_Dt.Rows[j]["FileID"].ToString();
                    sbFiles.Append("<tr onclick=\"SetCheckBoxClick(" + objID + ")\" height=\"22\" onmouseover=\"this.className='lvtColDataHover'\" onmouseout=\"this.className='lvtColData'\">");
                    sbFiles.Append("<td><input value=\"" + objID + "\" id=\"" + j + "\" onclick=\"SetCheckBoxClick(" + objID + ")\" name=\"itemsCheck\" type=\"checkbox\" /><input id=\"txtHidden\" name=\"txtHidden\" value=\"2\" type=\"hidden\" /></td>");
                    sbFiles.Append("<td><a target=\"_blank\" href=\"" + m_Dt.Rows[j]["FilePath"] + "\">" + m_Dt.Rows[j]["FileName"] + "</a></td>");
                    sbFiles.Append("<td>" + m_Dt.Rows[j]["FileType"] + "</td>");
                    sbFiles.Append("<td>" + m_Dt.Rows[j]["UserName"] + "</td>");
                    sbFiles.Append("<td>" + m_Dt.Rows[j]["CreateDate"] + "</td>");
                    sbFiles.Append("</tr>");
                }
                m_Dt = null;
                sbFiles.Append("</table>");
            }
            catch (Exception ex)
            {
                sbFiles.Append(ex.Message);
            }
            m_Dt = null;
            this.LiteralList.Text = sbFiles.ToString();
        }
        #endregion

        #region 调用方法
        
        /// <summary>
        /// 删除所选目录以及文件
        /// </summary>
        /// <param name="dirID"></param>
        private void DeleteSelectObj(string objID,string oprateType)
        {

            string sql = string.Empty;
            if (oprateType=="2")
            {
                //objID = objID.Substring(0, objID.Length-1);
                m_SqlParams = "update UserHD_Files set FileStatus=4 where FileID IN("+objID+")";
            }
            else 
            {
                if (GetSubDirNum(objID) > 1) 
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您所选择的目录下包含多个对象，请逐一删除后再删除此目录！", "/Disk/NetworkDisk.aspx?FuncCode=" + this.m_FuncNo + "&FuncUser=" + m_FuncUser + "&ID=" + objID + "&action=changeDir", true);
                    return;
                }
                m_SqlParams = "update UserHD_Directory set DirStatus=4 where DirID=" + objID; 
            }

            DbHelperSQL.ExecuteSql(m_SqlParams);
            // Disk/NetworkDisk.aspx?1=1&FuncNo=0907
            //Response.Write(" <script>alert('操作成功：成功删除所选定的对象！') ;window.location.href='/Disk/NetworkDisk.aspx?FuncCode=" + this.m_FuncNo + "&FuncUser=" + m_FuncUser + "&MoveFile'</script>");
            MessageBox.ShowAndRedirect(this.Page, "操作提示：成功删除所选定的对象！", "/Disk/NetworkDisk.aspx?FuncCode=" + this.m_FuncNo + "&FuncUser=" + m_FuncUser + "&MoveFile", true);
        }
        /// <summary>
        /// 获取目录下的对象数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private int GetSubDirNum(string directoryID)
        {
            int items = 0;
            m_SqlParams = "SELECT COUNT(*) FROM UserHD_Files WHERE FileStatus!=4 AND DirID=" + directoryID;
            items = int.Parse(DbHelperSQL.GetSingle(m_SqlParams).ToString());
            items += int.Parse(DbHelperSQL.GetSingle("SELECT COUNT(*) FROM UserHD_Directory WHERE ParentDirID=" + directoryID + " AND DirStatus!=4").ToString());
            return items;
        }
        
        #endregion

        #region 文件上传
        protected void ButUploadFile_Click(object sender, EventArgs e)
        {
            string dirID = Request["selDir"];
            string changeUrl = "NetworkDisk.aspx?FuncCode=" + this.m_FuncNo + "&FuncUser=" + m_FuncUser + "&ID=" + dirID + "&action=changeDir";
            try
            {
                string docsType = string.Empty;
                string sourceFile = string.Empty; // 原始文件名
                string configPath = System.Configuration.ConfigurationManager.AppSettings["FCKeditor:UserFilesPath"];//文件存放路径
                string selectedPath = DbHelperSQL.GetSingle("SELECT SysDirName FROM UserHD_Directory WHERE DirID=" + dirID).ToString();
                string userRootPath = configPath + "UserHD_Files/" + m_UserID + "/" + selectedPath + "/" + StringProcess.GetCurDateTimeStr(6) + "/";
                string targetFile = StringProcess.GetCurDateTimeStr(16); // 数据库保存的文件名
                string serverPath = Server.MapPath("/"); // // "D:\\YslWorks\\OA\\FamilyPlanning\\UNV.OA\\UNV.OA\\"
                string dispMsg = string.Empty;
                WebUpload m_upload = new WebUpload();
                UploadFile m_file = m_upload.GetUploadFile("UploadFiles");
                if (m_file != null)
                {
                    sourceFile = Path.GetFileName(m_file.FullPathOnClient);//文件名称
                    docsType = System.IO.Path.GetExtension(sourceFile).ToLower();//文件类型（扩展名）
                    //filesize = m_file.FileSize;
                    if (IsAllowedFiles(docsType, serverPath + userRootPath))
                    {
                        try
                        {
                            m_file.MoveTo(serverPath + userRootPath + targetFile + docsType);//Files/文件夹名称
                            SaveFileToDB(docsType, userRootPath + targetFile + docsType, sourceFile);
                            //Response.Write(" <script>window.location.href='" + changeUrl + "'</script>");
                            MessageBox.ShowAndRedirect(this.Page, "", changeUrl, true);
                        }
                        catch (Exception ex)
                        {
                            dispMsg = "文件上传失败：" + ex.Message;
                            MessageBox.ShowAndRedirect(this.Page, dispMsg, changeUrl, true);
                        }
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：文件类型不正确，请确认文件类型！", changeUrl, true);

                    }
                }
                else { 
                MessageBox.ShowAndRedirect(this.Page, "操作提示：请选择要上传得文件！", changeUrl, true);
            }
            }
            catch (Exception ex) {
            MessageBox.ShowAndRedirect(this.Page, "操作提示：根目录下禁止上传文件，请选择或者新建子目录！", changeUrl, true);
        }
            
            //if (string.IsNullOrEmpty(dirID) || dirID == "1")
            //{
            //    Response.Write(" <script>alert('根目录下禁止上传文件，请选择或者新建子目录！') ;window.location.href='" + changeUrl + "'</script>");
            //}
            //else {
                
            //}
            
            
        }            
       
        protected void ButUploadFile_Load(object sender, EventArgs e)
        {
            Button m_button = sender as Button;
            UNV.Comm.Upload.WebUpload m_upload = new WebUpload();
            m_upload.RegisterProgressBar(m_button, false);
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
            string dirID = Request["selDir"];
            string configPath = System.Configuration.ConfigurationManager.AppSettings["ServerURL"];//文件存放路径

            m_SqlParams = "INSERT INTO [UserHD_Files] (FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) ";
            m_SqlParams += "VALUES('" + sourceName + "','" + docsPath + "','" + docsType + "','" + this.m_FuncNo + "'," + m_UserID + "," + dirID + ")";
            try { DbHelperSQL.ExecuteSql(m_SqlParams); }
            catch { ;}
        }
        /// <summary>
        /// 检验是否是允许上传的文件类型
        /// </summary>
        /// <param name="uploadFileType"></param>
        /// <returns></returns>
        private bool IsAllowedFiles(string uploadFileType, string fullPath)
        {
            bool isValidFile = false;
            string[] allowFiles ={ ".gif", ".bmp", ".jpg", ".zip", ".rar", ".doc", ".xls", ".txt",".pdf", ".swf", ".wmv", ".exe", ".pdf", ".mp3", ".rm", "rmvb", "mp4", ".bak","htm","html" };
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
        private string GeServerPath()
        {
            string serverPath = Server.MapPath("/"); 

            if (serverPath.Length > 5)
            {
                if (serverPath.ToLower().Substring(serverPath.Length - 6) == "admin/")
                {
                    serverPath = serverPath.Substring(0, serverPath.Length - 6);
                }
            }
            return serverPath;
        }
        #endregion
       
    }
}
