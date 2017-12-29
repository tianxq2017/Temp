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
using System.Text;
using UNV.Comm.DataBase;
using UNV.Comm.Web;
using UNV.Comm.Upload;
using System.IO;
namespace join.pms.web.Disk
{
    public partial class SharingDisk : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_ActionName;
        private string m_ObjID;
        private string m_FuncNo;

        private string m_SqlParams;
        private DataTable m_Dt;
        private string m_UserID; // 当前登录的操作用户编号

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateParams();
            AuthenticateUser();

            if (!IsPostBack)
            {
                if (String.IsNullOrEmpty(m_ActionName))
                {
                    Response.Write("非法访问：操作被终止！");
                    Response.End();
                }
                else
                {
                    SetOpratetionAction("上传文件");
                }
            }
        }

        private void ValidateParams()
        {
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]); 

            string decSourceUrl = DESEncrypt.Decrypt(m_SourceUrl);// 解密Url参数
            if (!String.IsNullOrEmpty(m_SourceUrl)) { 
                txtUrlParams.Value = "../UnvCommList.aspx?" + decSourceUrl;
                m_FuncNo = StringProcess.AnalysisUrlParams("FuncCode", decSourceUrl);//类别编号
            }
        }


        #region 通用方法
        /// <summary>
        /// 设置操作行为 add新增,edit编辑,del删除,4查看,5审核,6分配角色 等
        /// </summary>
        /// <param name="oprateName">操作名称</param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("非法访问：操作被终止！");
                    Response.End();
                }
            }
            else
            {
                if (!PageValidate.IsNumber(m_ObjID))
                {
                    m_ObjID = m_ObjID.Replace("s", ",");
                }
            }

            switch (m_ActionName)
            {
                case "add": // 新增
                    funcName = "上传资料";
                    //GetLastPlan("");
                    //ShowAddInfo();
                    break;
                case "view": // 查看
                    funcName = "消息查看";
                    //ShowModInfo(m_ObjID);
                    break;
                case "edit": // 编辑
                    funcName = "修改用户信息";
                    //GetLastPlan(m_ObjID);
                    //ShowModInfo(m_ObjID);
                    break;
                case "del": // 删除
                    funcName = "删除日志";
                   // DelInfo(m_ObjID);
                    break;
                case "passAudit": // 审核通过
                    funcName = "查看内容";
                    //AuditInfo(m_ObjID, true);
                    break;
                case "cancelAudit": // 驳回修改
                    funcName = "审核内容";
                    //AuditInfo(m_ObjID, false);
                    break;
                default:
                    if (Request.UrlReferrer != null) Response.Redirect(Request.UrlReferrer.ToString());
                    break;
            }

            this.LiteralNav.Text = GetPageTopNavInfo(oprateName, funcName);
        }
        /// <summary>
        /// 获取页面导航信息
        /// </summary>
        /// <param name="oprateName"></param>
        /// <param name="funcName"></param>
        /// <returns></returns>
        private string GetPageTopNavInfo(string oprateName, string funcName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class=\"small hhz f15\" width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"background:url(../images/sysBgTitle.gif) bottom left no-repeat;\"><tbody><tr><td style=\"height: 2px;\"></td></tr><tr>");
            sb.Append("<td style=\"padding-left: 10px; padding-right: 50px;\" height=\"40\" class=\"moduleName\" nowrap=\"nowrap\"><a class=\"hdrLink\" href=\"../MainDesk.aspx\">个人桌面</a> >> <a href=\"" + txtUrlParams.Value + "\">" + oprateName + "</a></td>");
            sb.Append("<td width=\"80%\" nowrap=\"nowrap\" align=\"left\">");
            sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tbody><tr>");
            sb.Append("<td class=\"sep1\" ></td><td class=\"small\">");//style=\"width: 1px;\"
            sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tbody><tr><td>");
            sb.Append("<table border=\"0\" cellpadding=\"5\" cellspacing=\"0\"><tbody><tr>");
            sb.Append("<td style=\"padding-right: 0px; padding-left: 10px;\"><a href=\"javascript:;\" ><img src=\"../images/btnL3Add-Faded.gif\" alt=\"" + funcName + "...\" title=\"" + funcName + "...\" border=\"0\"></a></td>");
            sb.Append("<td style=\"padding-right: 10px;\"><a href=\"javascript:;\"><img src=\"../images/btnL3Search-Faded.gif\" alt=\"查找 " + funcName + "...\" title=\"查找 " + funcName + "...\" border=\"0\"></a></td>");
            sb.Append("<td style=\"padding-right: 10px;\"><a href=\"javascript:;\" onclick='return window.open(\"../UnvCommMsg.aspx?r=\" + Math.random() + \"&action=add\",\"ChatAdd\",\"width=600,height=450,resizable=1,scrollbars=1\");');\"><img src=\"../images/tbarChat.gif\" alt=\"交流...\" title=\"交流...\" border=\"0\"></a></td>");
            sb.Append("<td style=\"padding-right: 0px; padding-left: 10px;\"><img src=\"../images/tbarImport-Faded.gif\" border=\"0\"></td>");
            sb.Append("<td style=\"padding-right: 10px;\"><img src=\"../images/tbarExport-Faded.gif\" border=\"0\"></td>");
            sb.Append("</tr></tbody></table>");
            sb.Append("</td></tr></tbody></table>");
            sb.Append("</td></tr></tbody></table>");
            sb.Append("</td></tr><tr><td style=\"height: 2px;\"></td></tr></tbody></table>");
            return sb.ToString();
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
        #endregion

        protected void ButUploadFile_Click(object sender, EventArgs e)
        {
            string docsType = string.Empty;
            string sourceFile = string.Empty; // 原始文件名
            string configPath = System.Configuration.ConfigurationManager.AppSettings["FCKeditor:UserFilesPath"];
            string targetFile = StringProcess.GetCurDateTimeStr(16); // 数据库保存的文件名
            string fullPath = configPath + StringProcess.GetCurDateTimeStr(6) + "/";
            string serverPath = GeServerPath();
            string dispMsg = string.Empty;

            WebUpload m_upload = new WebUpload();
            UploadFile m_file = m_upload.GetUploadFile("UploadFiles");
            if (m_file != null)
            {
                sourceFile = Path.GetFileName(m_file.FullPathOnClient);
                docsType = System.IO.Path.GetExtension(sourceFile).ToLower();
                if (IsAllowedFiles(docsType, serverPath + fullPath))
                {
                    try
                    {
                        m_file.MoveTo(serverPath + fullPath + targetFile + docsType);
                        SaveFileToDB(docsType, fullPath + targetFile + docsType, sourceFile);//".gif", ".bmp", ".jpg"
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：上传成功！", txtUrlParams.Value, true);
                    }
                    catch (Exception ex)
                    {
                        this.LiteralMsg.Text = "文件上传失败：" + ex.Message;
                    }
                }
                else
                {
                    this.LiteralMsg.Text = "<font color=red>文件格式不正确！</font>";
                }
            }
            else 
            {
                this.LiteralMsg.Text = "<font color=red>上传文件为空！</font>";
            }
        }



        protected void ButUploadFile_Load(object sender, EventArgs e)
        {
            Button m_button = sender as Button;
            UNV.Comm.Upload.WebUpload m_upload = new WebUpload();
            m_upload.RegisterProgressBar(m_button, false);
        }


        #region 上传文件
        /// <summary>
        /// 将上传文件信息记录数据库
        /// </summary>
        /// <param name="docsType">文件类型</param>
        /// <param name="docsPath">文件路径</param>
        /// <param name="sourceName">文件名称</param>
        private void SaveFileToDB(string docsType, string docsPath, string sourceName)
        {
            string classCode = string.Empty;
            m_SqlParams = "INSERT INTO [UserHD_Files] (FileName,FilePath,FileType,ClassCode,OprateUserID,CreateDate,FileStatus) ";
            m_SqlParams += "VALUES('" + sourceName + "','" + docsType + "','" + docsType + "','12345678'," + m_UserID + ",'" + DateTime.Now + "',5)";
            
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
            string[] allowFiles ={ ".gif", ".bmp", ".jpg", ".zip", ".rar", ".doc", ".xls", ".txt",".pdf", ".swf", ".wmv", ".exe", ".pdf", ".mp3", ".rm", "rmvb", "mp4", ".bak", ".drv", "htm", "html" };
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
            string serverPath = Server.MapPath("../Files") + "/";

            if (serverPath.Length > 5)
            {
                if (serverPath.ToLower().Substring(serverPath.Length - 5) == "Disk/")
                {
                    serverPath = serverPath.Substring(0, serverPath.Length - 5);
                }
            }
            return serverPath;
        }
        #endregion



    }
}
