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
using System.Data.SqlClient;
using System.IO;

using UNV.Comm.Web;
using UNV.Comm.DataBase;
using UNV.Comm.Upload;

namespace join.pms.web.userctrl
{
    public partial class SetUploadForCms : System.Web.UI.Page
    {
        private string m_SqlParams;
        private string m_UserID;
        private string m_UserName;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();

            this.LabelMsg.Text = "上传提示信息……";
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
            else { GetUserInfo(); }
        }

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        private void GetUserInfo()
        {
            SqlDataReader sdr = null;
            try
            {
                string sqlParams = "SELECT UserAccount,UserName,UserAreaCode FROM USER_BaseInfo WHERE UserID=" + m_UserID;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        m_UserName = sdr["UserAccount"].ToString() + "(" + sdr["UserName"].ToString() + ")";
                        //m_AreaCode = sdr["UserAreaCode"].ToString();
                        //m_AreaName = GetAreaName(m_AreaCode, "0");
                    }
                }
            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }
        }

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
            string saveFile = string.Empty; // 保存的文件
            string configPath = System.Configuration.ConfigurationManager.AppSettings["FCKeditor:UserFilesPath"];
            string siteimgWidth = System.Configuration.ConfigurationManager.AppSettings["SiteImgWidth"];
            string targetFile = StringProcess.GetCurDateTimeStr(16); // 数据库保存的文件名
            string fullPath = configPath + StringProcess.GetCurDateTimeStr(6) + "/";
            string serverPath = Server.MapPath("/");
            string dispMsg = string.Empty;
            string sourceImg = "", targetImg = "", newsavePath = "";
            
            
            long docsSize = 0;

            //WebUpload m_upload = new WebUpload();
            //UploadFile m_file = m_upload.GetUploadFile("UploadFiles");
            //UploadFile m_file = GetUploadFile("UploadFiles");
            System.Web.HttpFileCollection m_file = System.Web.HttpContext.Current.Request.Files;
            if (m_file != null)
            {
                //sourceFile = Path.GetFileName(m_file.FullPathOnClient);
                sourceFile = m_file[0].FileName;
                docsType = System.IO.Path.GetExtension(sourceFile).ToLower();
                docsSize = m_file[0].ContentLength;

                if (IsAllowedFiles(docsType, serverPath + fullPath))
                {
                    string errline = "0";
                    try
                    {
                        saveFile = fullPath + targetFile + docsType;
                        newsavePath = saveFile;
                        errline = "1";
                        //m_file.SaveAs(serverPath + saveFile, true);
                        m_file[0].SaveAs(serverPath + saveFile);
                        errline = "2";

                        //图片文件自动压缩裁剪
                        if (docsType == ".gif" || docsType == ".jpg" || docsType == ".jpeg" || docsType == ".bmp" || docsType == ".png")
                        {
                            // SetMicroImages
                            //if ((int.Parse(m_file.FileSize.ToString())/1024) > 800) {
                            long fileSize = docsSize / 1024;
                            if (fileSize < 10240)
                            {
                                sourceImg = serverPath + saveFile;
                                targetImg = sourceImg.Insert(sourceImg.LastIndexOf("."), "_Zip");
                                errline = "3";
                                //targetImg = SetMicroImages.GetMicroImage(sourceImg, targetImg, "W", int.Parse(siteimgWidth), 0);
                                targetImg = saveFile;
                                errline = "4";
                                if (!String.IsNullOrEmpty(targetImg))
                                {
                                    targetImg = targetImg.Replace(serverPath, "");
                                    newsavePath = targetImg;
                                }
                            }
                        }
                        errline = " 9x";
                        SaveFileToDB(docsType, newsavePath, sourceFile);//".gif", ".bmp", ".jpg" 
                        // SetFCKeditorDocs(sourceFile,saveFile,fileType,oprateType)
                        this.txtSourceFile.Value = sourceFile;
                        this.txtSaveFile.Value = newsavePath;// saveFile;
                        this.txtFileType.Value = docsType;
                        //dispMsg = "<div id=\"oprateUpFiles\">文件[ " + sourceFile + " ]上传成功！<br/>请<a href=\"javascript:SetFCKeditorDocs('" + sourceFile + "','" + saveFile + "','" + docsType + "','ist')\"><b><font color=red size=3>单击此处</font></b></a>插入到编辑器中；";
                        this.LabelMsg.Text = "<div id=\"oprateUpFiles\">文件[ " + sourceFile + " ]上传成功！<br/><b>The file size:</b>" + docsSize.ToString() + "字节<br/><b>请点击确定按钮将附件插入到编辑器中。";
                        //this.LabelMsg.Text = "<div id=\"oprateUpFiles\">文件[ " + sourceFile + " ]上传成功！<br/><b>The file size:</b>" + m_file.FileSize + "字节<br/><b>请点击确定按钮将附件插入到编辑器中。";
                        //this.labDetial.Text = "<b>The file Original path:</b>" + this.FileUpload1.PostedFile.FileName + "<br/><b>The file size:</b>" + this.FileUpload1.PostedFile.ContentLength + "字节<br/><b>The File Type:</b>" + this.FileUpload1.PostedFile.ContentType + "<br/>";
                    }
                    catch (Exception ex)
                    {
                        this.LabelMsg.Text = "文件上传失败：" + ex.Message + "-" + errline + "<br/>sourceImg=" + sourceImg + "<br/>targetImg=" + targetImg;
                    }
                }
                else
                {
                    this.LabelMsg.Text = "<font color=red>只能上传普通文档、压缩文档、图片资料！</font>";
                }
            }
            else
            {
                this.LabelMsg.Text = "<font color=red>请选择要上传的附件！</font>";
            }
        }

        /// <summary>
        /// 将上传文件信息记录数据库
        /// </summary>
        /// <param name="docsType"></param>
        /// <param name="docsPath"></param>
        /// <param name="sourceName"></param>
        private void SaveFileToDB(string docsType, string docsPath, string sourceName)
        {
            try
            {
                m_SqlParams = "INSERT INTO [BIZ_ProjectInfo_Docs]([DocsType], [DocsPath], [SourceName]) ";
                m_SqlParams += "VALUES( '" + docsType + "', '" + docsPath + "', '" + sourceName + "') ";
                m_SqlParams += "SELECT SCOPE_IDENTITY()"; // SQL2005:OUTPUT INSERTED.ID
                this.txtFileID.Value = DbHelperSQL.GetSingle(m_SqlParams).ToString();

                // SELECT CommID,CmsID,DocsType,DocsPath,SourceName,UserID,OprateDate FROM [CMS_Docs]
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        /// <summary>
        /// 获取服务器路径
        /// </summary>
        /// <returns></returns>
        private string GeServerPath()
        {
            string serverPath = Server.MapPath("/") + "/";

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
            string[] allowFiles ={ ".gif", ".bmp", ".jpg", ".png", ".zip", ".rar", ".doc", ".docx", ".xls", ".xlsx", ".ppt",".pptx",".txt", ".pdf", ".pdfx", ".swf", ".asf", ".wmv", ".flv", ".mp3", ".mp4", ".3gp", };
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
    }
}
