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
using System.Globalization;

using UNV.Comm.Web;
using UNV.Comm.DataBase;

namespace join.pms.web.userctrl
{
    public partial class SetDiskUploadCompts : System.Web.UI.Page
    {
        private string m_SqlParams;
        private string m_UserID;
        private string m_DocsName;
        private string m_FuncNo;
        private string m_DirID;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();
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
                HttpCookie loginCookie = Request.Cookies["AREWEB_HCS_USER_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["AREWEB_HCS_USERID"] != null && !String.IsNullOrEmpty(Session["AREWEB_HCS_USERID"].ToString())) { returnVa = true; m_UserID = Session["AREWEB_HCS_USERID"].ToString(); }
            }

            if (!returnVa)
            {
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        private void ValidateParams()
        {
            m_FuncNo = PageValidate.GetTrim(Request.QueryString["FuncNo"]);
            m_DirID = PageValidate.GetTrim(Request.QueryString["SelDir"]);

        }


        protected void ButUploadFile_Click(object sender, EventArgs e)
        {
            string docsType, fileContentType = string.Empty;
            string sourceFile = string.Empty; // 原始文件名
            string saveFile = string.Empty; // 保存的文件
            long docsSize = 0;

            string configPath = System.Configuration.ConfigurationManager.AppSettings["FCKeditor:UserFilesPath"];
            string siteimgWidth = System.Configuration.ConfigurationManager.AppSettings["SiteImgWidth"];
            string targetFile = StringProcess.GetCurDateTimeStr(16); // 数据库保存的文件名
            string fullPath = configPath +"UserHD_Files/" + m_UserID + "/" + StringProcess.GetCurDateTimeStr(6) + "/";
            string serverPath = Server.MapPath("/");
            string dispMsg = string.Empty;
            string sourceImg = "", targetImg = "", newsavePath = "";

            //客户端上传的文件
            System.Web.HttpFileCollection _file = System.Web.HttpContext.Current.Request.Files;
            if (_file.Count > 0)
            {
                try
                {
                    docsSize = _file[0].ContentLength;
                    fileContentType = _file[0].ContentType;
                    sourceFile = _file[0].FileName; // "C:\\Users\\ysl\\Pictures\\1-130922034538-lp.jpg"
                    docsType = System.IO.Path.GetExtension(sourceFile); //扩展名

                    if (fileContentType == "image/pjpeg" || fileContentType == "image/gif" || fileContentType == "image/jpeg" || fileContentType == "application/x-jpg" || fileContentType == "image/x-png")
                    {
                        long fileSize = docsSize / 1024;
                        if (fileSize < 10240)
                        {
                            saveFile = fullPath + targetFile + docsType;
                            newsavePath = saveFile;
                            //获取文件流
                            System.IO.Stream stream = _file[0].InputStream;
                            //保存文件
                            _file[0].SaveAs(serverPath + saveFile);
                            //压缩图片 SetMicroImages
                            sourceImg = serverPath + saveFile;
                            targetImg = sourceImg.Insert(sourceImg.LastIndexOf("."), "_Zip");
                            targetImg = SetMicroImages.GetMicroImage(sourceImg, targetImg, "W", int.Parse(siteimgWidth), 0);
                            if (!String.IsNullOrEmpty(targetImg))
                            {
                                targetImg = targetImg.Replace(serverPath, "");
                                newsavePath = targetImg;
                            }
                            //保存到数据库
                            SaveFileToDB(docsType, newsavePath, sourceFile);

                            this.txtSourceFile.Value = sourceFile;
                            this.txtSaveFile.Value = newsavePath;// saveFile;
                            this.txtFileType.Value = docsType;

                            dispMsg = "<div id=\"oprateUpFiles\">文件[ " + sourceFile + " ]上传成功！<br/><b>The file size:</b>" + docsSize.ToString() + "字节<br/><b>请点击确定按钮将附件插入到编辑器中。";
                        }
                        else
                        {
                            dispMsg = "操作失败：图片文件过大，不能超过10M，请使用图像处理软件处理后再上传！" + docsSize.ToString();
                        }
                    }
                    else
                    {
                        long fileSize = docsSize / 1024;
                        if (fileSize < 20480)
                        {
                            saveFile = fullPath + targetFile + docsType;
                            newsavePath = saveFile;
                            //获取文件流
                            System.IO.Stream stream = _file[0].InputStream;
                            //保存文件
                            _file[0].SaveAs(serverPath + saveFile);
                            //保存到数据库
                            SaveFileToDB(docsType, newsavePath, sourceFile);

                            this.txtSourceFile.Value = sourceFile;
                            this.txtSaveFile.Value = newsavePath;// saveFile;
                            this.txtFileType.Value = docsType;

                            dispMsg = "<div id=\"oprateUpFiles\">文件[ " + sourceFile + " ]上传成功！<br/><b>The file size:</b>" + docsSize.ToString() + "字节<br/><b>请点击确定按钮将附件插入到编辑器中。";
                        }
                        else
                        {
                            dispMsg = "操作失败：文件过大，不能超过20M，请打包压缩处理后再上传！" + docsSize.ToString();
                        }
                    }
                }
                catch (Exception ex) { dispMsg = ex.Message; }
            }
            else
            {
                dispMsg = "<font color=red>请选择要上传的附件！</font>";
            }

            this.LabelMsg.Text = dispMsg;
        }

        /// <summary>
        /// 将上传文件信息记录数据库,根据情况修改
        /// </summary>
        /// <param name="docsType"></param>
        /// <param name="docsPath"></param>
        /// <param name="sourceName"></param>
        private void SaveFileToDB(string docsType, string docsPath, string sourceName)
        {
            try
            {
                string classCode = string.Empty;
                string configPath = System.Configuration.ConfigurationManager.AppSettings["ServerURL"];//文件存放路径

                m_SqlParams = "INSERT INTO \"UserHD_Files\" (\"FileName\",\"FilePath\",\"FileType\",\"ClassCode\",\"OprateUserID\",\"DirID\") ";
                m_SqlParams += "VALUES('" + sourceName + "','" + docsPath + "','" + docsType + "','" + this.m_FuncNo + "'," + m_UserID + "," + m_DirID + ")";

                DbHelperSQL.ExecuteSql(m_SqlParams);
                //this.txtFileID.Value = KingBaseHelper.GetSingle("SELECT MAX(\"CommID\") FROM \"CMS_Docs\" WHERE \"UserID\"=" + m_UserID).ToString();
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
            string[] allowFiles ={ ".gif", ".bmp", ".jpg", ".png", ".zip", ".rar", ".doc", ".docx", ".xls", ".txt", ".pdf", ".swf", ".asf", ".wmv", ".flv", ".mp3", ".mp4", ".3gp", };
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
