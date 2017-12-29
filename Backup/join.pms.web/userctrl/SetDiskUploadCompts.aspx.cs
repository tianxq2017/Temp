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
            this.LabelMsg.Text = "�ϴ���ʾ��Ϣ����";
        }

        /// <summary>
        /// �����֤
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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Default.shtml?action=closewindow';</script>");
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
            string sourceFile = string.Empty; // ԭʼ�ļ���
            string saveFile = string.Empty; // ������ļ�
            long docsSize = 0;

            string configPath = System.Configuration.ConfigurationManager.AppSettings["FCKeditor:UserFilesPath"];
            string siteimgWidth = System.Configuration.ConfigurationManager.AppSettings["SiteImgWidth"];
            string targetFile = StringProcess.GetCurDateTimeStr(16); // ���ݿⱣ����ļ���
            string fullPath = configPath +"UserHD_Files/" + m_UserID + "/" + StringProcess.GetCurDateTimeStr(6) + "/";
            string serverPath = Server.MapPath("/");
            string dispMsg = string.Empty;
            string sourceImg = "", targetImg = "", newsavePath = "";

            //�ͻ����ϴ����ļ�
            System.Web.HttpFileCollection _file = System.Web.HttpContext.Current.Request.Files;
            if (_file.Count > 0)
            {
                try
                {
                    docsSize = _file[0].ContentLength;
                    fileContentType = _file[0].ContentType;
                    sourceFile = _file[0].FileName; // "C:\\Users\\ysl\\Pictures\\1-130922034538-lp.jpg"
                    docsType = System.IO.Path.GetExtension(sourceFile); //��չ��

                    if (fileContentType == "image/pjpeg" || fileContentType == "image/gif" || fileContentType == "image/jpeg" || fileContentType == "application/x-jpg" || fileContentType == "image/x-png")
                    {
                        long fileSize = docsSize / 1024;
                        if (fileSize < 10240)
                        {
                            saveFile = fullPath + targetFile + docsType;
                            newsavePath = saveFile;
                            //��ȡ�ļ���
                            System.IO.Stream stream = _file[0].InputStream;
                            //�����ļ�
                            _file[0].SaveAs(serverPath + saveFile);
                            //ѹ��ͼƬ SetMicroImages
                            sourceImg = serverPath + saveFile;
                            targetImg = sourceImg.Insert(sourceImg.LastIndexOf("."), "_Zip");
                            targetImg = SetMicroImages.GetMicroImage(sourceImg, targetImg, "W", int.Parse(siteimgWidth), 0);
                            if (!String.IsNullOrEmpty(targetImg))
                            {
                                targetImg = targetImg.Replace(serverPath, "");
                                newsavePath = targetImg;
                            }
                            //���浽���ݿ�
                            SaveFileToDB(docsType, newsavePath, sourceFile);

                            this.txtSourceFile.Value = sourceFile;
                            this.txtSaveFile.Value = newsavePath;// saveFile;
                            this.txtFileType.Value = docsType;

                            dispMsg = "<div id=\"oprateUpFiles\">�ļ�[ " + sourceFile + " ]�ϴ��ɹ���<br/><b>The file size:</b>" + docsSize.ToString() + "�ֽ�<br/><b>����ȷ����ť���������뵽�༭���С�";
                        }
                        else
                        {
                            dispMsg = "����ʧ�ܣ�ͼƬ�ļ����󣬲��ܳ���10M����ʹ��ͼ���������������ϴ���" + docsSize.ToString();
                        }
                    }
                    else
                    {
                        long fileSize = docsSize / 1024;
                        if (fileSize < 20480)
                        {
                            saveFile = fullPath + targetFile + docsType;
                            newsavePath = saveFile;
                            //��ȡ�ļ���
                            System.IO.Stream stream = _file[0].InputStream;
                            //�����ļ�
                            _file[0].SaveAs(serverPath + saveFile);
                            //���浽���ݿ�
                            SaveFileToDB(docsType, newsavePath, sourceFile);

                            this.txtSourceFile.Value = sourceFile;
                            this.txtSaveFile.Value = newsavePath;// saveFile;
                            this.txtFileType.Value = docsType;

                            dispMsg = "<div id=\"oprateUpFiles\">�ļ�[ " + sourceFile + " ]�ϴ��ɹ���<br/><b>The file size:</b>" + docsSize.ToString() + "�ֽ�<br/><b>����ȷ����ť���������뵽�༭���С�";
                        }
                        else
                        {
                            dispMsg = "����ʧ�ܣ��ļ����󣬲��ܳ���20M������ѹ����������ϴ���" + docsSize.ToString();
                        }
                    }
                }
                catch (Exception ex) { dispMsg = ex.Message; }
            }
            else
            {
                dispMsg = "<font color=red>��ѡ��Ҫ�ϴ��ĸ�����</font>";
            }

            this.LabelMsg.Text = dispMsg;
        }

        /// <summary>
        /// ���ϴ��ļ���Ϣ��¼���ݿ�,��������޸�
        /// </summary>
        /// <param name="docsType"></param>
        /// <param name="docsPath"></param>
        /// <param name="sourceName"></param>
        private void SaveFileToDB(string docsType, string docsPath, string sourceName)
        {
            try
            {
                string classCode = string.Empty;
                string configPath = System.Configuration.ConfigurationManager.AppSettings["ServerURL"];//�ļ����·��

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
        /// ��ȡ������·��
        /// </summary>
        /// <returns></returns>
        private string GeServerPath()
        {
            string serverPath = Server.MapPath("/") + "/";

            return serverPath;
        }

        /// <summary>
        /// �����Ƿ��������ϴ����ļ�����
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
