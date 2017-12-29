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
    public partial class SetUploadByGpy : System.Web.UI.Page
    {
        private string m_SqlParams;
        private string m_UserID;
        private string m_DocsName;

        protected void Page_Load(object sender, EventArgs e)
        {
            //AuthenticateUser();
            //this.txtDocName.Value = HttpUtility.UrlDecode(PageValidate.GetTrim(Request.QueryString["docsname"]));
            //this.selRes1.SelectedIndex = 4;
            this.txtDocName.Value = PageValidate.GetTrim(Request.QueryString["docsname"]);
            
            this.LabelMsg.Text = "上传提示信息……";
            //加载自动拍照功能
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

        //protected void ButUploadFile_Click(object sender, EventArgs e)
        //{
        //    // Files/GpyFile/2015/12/指定文件名
        //    string webSvrsPath = System.Configuration.ConfigurationManager.AppSettings["SvrsWebPath"];
        //    string fullPath = "/Files/GpyFile/" + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) + "/" + DateTime.Now.Month.ToString("D2", CultureInfo.InvariantCulture) + "/";
        //    string fileName = this.txtTmpUpFiles.Value;
        //    string dispMsg = string.Empty;

        //    if (!string.IsNullOrEmpty(fileName)) {
        //        //保存到数据库
        //        SaveFileToDB(".jpg", fullPath + fileName, fileName);
        //        //返回值
        //        this.txtSourceFile.Value = fileName;
        //        this.txtSaveFile.Value = fullPath + fileName;
        //        this.txtFileType.Value = ".jpg";

        //        dispMsg = "<div id=\"oprateUpFiles\">操作提示：文件[ " + fullPath + fileName + " ]上传到服务器成功！<br/>请点击确定按钮返回继续其它操作……";
        //    }
        //    else
        //    {
        //        dispMsg = "<font color=red>操作提示：没有找到扫描的文件！</font>";
        //    }

        //    this.LabelMsg.Text = dispMsg;
        //}

        /// <summary>
        /// 将上传文件信息记录数据库,根据情况修改
        /// </summary>
        /// <param name="docsType"></param>
        /// <param name="docsPath"></param>
        /// <param name="sourceName"></param>
        //private void SaveFileToDB(string docsType, string docsPath, string sourceName)
        //{
        //    try
        //    {
        //        m_SqlParams = "INSERT INTO [BIZ_Docs](DocsName,DocsType,DocsPath,SourceName,SysNo) ";
        //        m_SqlParams += "VALUES('" + m_DocsName + "','" + docsType + "', '" + docsPath + "', '" + sourceName + "',1) ";
        //        m_SqlParams += "SELECT SCOPE_IDENTITY()"; // SQL2005:OUTPUT INSERTED.ID

        //        this.txtFileID.Value = DbHelperSQL.GetSingle(m_SqlParams).ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //}

    }
}
