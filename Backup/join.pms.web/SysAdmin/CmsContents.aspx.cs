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
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;


namespace join.pms.web.SysAdmin
{
    public partial class CmsContents : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;
        protected string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();
           
            if (!IsPostBack) 
            {
                SetPageStyle(m_UserID);
                SetCmsClass(m_FuncCode);
                SetOpratetionAction(m_NavTitle);

            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "";// DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                //if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";
                cssFile = "/css/inidex.css";
                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        #region

        /// <summary>
        /// 身份验证
        /// </summary>
        private void AuthenticateUser()
        {
            bool returnVa = false;
            if (Request.Browser.Cookies){
                HttpCookie loginCookie = Request.Cookies["AREWEB_OC_USER_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else{
                if (Session["AREWEB_OC_USERID"] != null && !String.IsNullOrEmpty(Session["AREWEB_OC_USERID"].ToString())) { returnVa = true; m_UserID = Session["AREWEB_OC_USERID"].ToString(); }
            }

            if (!returnVa){
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 验证接受的参数
        /// </summary>
        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);
            
            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName)){
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");

                this.txtFileUrl.Value = m_SvrsUrl;
            }
            else{
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        /// <summary>
        /// 设置内容分类
        /// </summary>
        private void SetCmsClass(string cmsCode)
        {
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT CmsCID,CmsCName FROM CMS_Class WHERE CmsCode='" + cmsCode + "'";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    //this.txtCmsCID.Value = PageValidate.GetTrim(sdr["CmsCID"].ToString());
                    m_NavTitle = "信息发布管理 >> " + PageValidate.GetTrim(sdr["CmsCName"].ToString());
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }
        }

        /// <summary>
        /// 设置操作行为
        /// </summary>
        /// <param name="oprateName"></param>
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
                    funcName = "新增";
                    this.txtOprateDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
                    //this.LiteralCmsClass.Text = CustomerControls.CreateSelCtrl("txtCmsClass", "", "", "", "SELECT [CmsCID], [CmsCName] FROM [CMS_Class]");
                    break;
                case "edit": // 编辑
                    funcName = "编辑";
                    IsAllowed(m_ObjID);
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // 删除
                    funcName = "删除";
                    IsAllowed(m_ObjID);
                    DelInfo(m_ObjID);
                    break;
                case "view": // 查看
                    funcName = "查看内容";
                    ShowModInfo(m_ObjID);
                    break;
                case "audit": // 审核
                    funcName = "审核内容";
                    AuditInfo(m_ObjID);
                    break;
                case "hide": // 屏蔽
                    funcName = "审核内容";
                    HideInfo(m_ObjID);
                    break;
                case "pub": // 公开推荐
                    funcName = "推荐";
                    RecommInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">起始页</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }

        
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            bool isEdit = true;
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT CmsTitle, CmsBody, CmsKeys, CmsAttrib,OprateDate FROM [CMS_Contents] WHERE CmsID=" + m_ObjID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    if (sdr["CmsAttrib"].ToString() != "0" && m_ActionName == "edit")
                    {
                        isEdit = false;
                        break;
                    }
                    this.txtCmsTitle.Text = PageValidate.Decode(PageValidate.GetTrim(sdr["CmsTitle"].ToString()));
                    this.objCKeditor.Text = PageValidate.Decode(CommBiz.GetTrim(sdr["CmsBody"].ToString()));
                    //this.txtCmsKeys.Text = PageValidate.Decode(PageValidate.GetTrim(sdr["CmsKeys"].ToString()));
                    if (!string.IsNullOrEmpty(sdr["OprateDate"].ToString())) this.txtOprateDate.Value = DateTime.Parse(sdr["OprateDate"].ToString()).ToString("yyyy-MM-dd");
                    
                    if (m_ActionName == "view") this.btnAdd.Enabled = false;

                    GetCmsDocs(m_ObjID); // 获取内容附件
                }
                sdr.Close();
                // 是否可编辑
                if (!isEdit)
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息包含通过审核、修正或公开的信息；该信息不允许编辑，只有处于未审核状态的数据才能够编辑！", m_TargetUrl, true);
                }
            }
            catch { if (sdr != null) sdr.Close(); }
        }

        /// <summary>
        /// 获取内容附件
        /// </summary>
        /// <param name="objID"></param>
        private void GetCmsDocs(string objID)
        {
            //string docsType = string.Empty;
            //string docsPath = string.Empty;
            //string sourceFile = string.Empty; // 原始文件名

            //StringBuilder sHtml = new StringBuilder();
            //DataTable dt = new DataTable();
            //m_SqlParams = "SELECT CommID,CmsID,DocsType,DocsPath,SourceName FROM [CMS_Docs] WHERE CmsID=" + objID;
            //dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        docsType = dt.Rows[i]["DocsType"].ToString();
            //        docsPath = dt.Rows[i]["DocsPath"].ToString();
            //        sourceFile = dt.Rows[i]["SourceName"].ToString();
            //        if (docsType == ".jpg" || docsType == ".bmp")
            //        {
            //            sHtml.Append("<a href=\"javascript:SetCKeditorDocs('" + sourceFile + "','" + docsPath + "','" + docsType + "','ist')\"><img src=\"" + docsPath + "\" alt=\"请单击插入到编辑器中\" width=\"100\" height=\"50\" /></a> ");
            //        }
            //        else
            //        {
            //            sHtml.Append("<a href=\"javascript:SetCKeditorDocs('" + sourceFile + "','" + docsPath + "','" + docsType + "','ist')\" title=\"请单击插入到编辑器中\"><b><font color=red size=3>" + sourceFile + "</font></b></a> ");
            //        }
            //    }
            //}
            //this.LiteralDocs.Text = sHtml.ToString();
            //sHtml = null;
        }

        /// <summary>
        /// 判断操作权限
        /// </summary>
        /// <param name="objID"></param>
        private void IsAllowed(string objID) {
            string roleID = string.Empty;
            bool returnVal = true;
            SqlDataReader sdr = null;
            try
            {
                roleID = DbHelperSQL.GetSingle("SELECT TOP 1 RoleID FROM SYS_UserRoles WHERE UserID="+m_UserID).ToString();
                if (!string.IsNullOrEmpty(roleID))
                {
                    if (roleID == "1") { returnVal = true; }
                    else
                    {
                        m_SqlParams = "SELECT UserID,CmsAttrib FROM CMS_Contents WHERE CmsID  IN(" + objID + ")";

                        sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                        while (sdr.Read())
                        {
                            if (sdr[0].ToString() != m_UserID)
                            {
                                returnVal = false;
                                break;
                            }
                        }
                        sdr.Close();
                    }
                }
                else { returnVal = false; }
            }
            catch { 
                if (sdr != null) sdr.Close();
                returnVal = false;
            }

            if (!returnVal) { 
                //Response.Write(" <script>alert('操作失败：除系统管理员之外，您只能操作本人发布的信息！') ;window.location.href='" + m_TargetUrl + "'</script>");
                //Response.End();
                MessageBox.ShowAndRedirect(this.Page, "操作提示：除系统管理员之外，您只能操作本人发布的信息！", m_TargetUrl, true);
            }
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            if (CheckDelFlag(m_ObjID))
            {
                try
                {
                    System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                    list.Add("DELETE FROM CMS_Docs WHERE CmsID IN(" + objID + ")");
                    list.Add("DELETE FROM CMS_Contents WHERE CmsID IN(" + objID + ")");
                    DbHelperSQL.ExecuteSqlTran(list);
                    list = null;
                    //CmsAttrib:0 默认;1 审核; 3 屏蔽; 4 删除; 9 公开
                   
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息被成功删除！", m_TargetUrl, true);
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
                }
            }
            else
            {             
                MessageBox.ShowAndRedirect(this.Page, "操作提示：您所选择的数据保护审核或公开的信息，禁止操作！", m_TargetUrl, true);
            }
        }

        /// <summary>
        /// 删除前检测
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        public static bool CheckDelFlag(string objID)
        {
            // 0 默认;1 审核;2,修改  3 屏蔽; 4 删除; 9 公开
            string auditFlag = string.Empty;
            string sqlParams = string.Empty;
            bool isChecked = true;
            SqlDataReader sdr = null;
            int i = 0;
            try
            {
                sqlParams = "SELECT CmsAttrib FROM [CMS_Contents] WHERE CmsID IN(" + objID + ")";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    auditFlag = sdr[0].ToString();
                    if (auditFlag == "1" || auditFlag == "9") { isChecked = false; break; }
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            return isChecked;
        }

        /// <summary>
        /// 审核信息
        /// </summary>
        /// <param name="objID"></param>
        private void AuditInfo(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                try
                {
                    // CmsAttrib:0 默认;1 审核; 3 屏蔽; 4 删除; 9 公开
                    string cmsAttrib = DbHelperSQL.GetSingle("SELECT [CmsAttrib] FROM [CMS_Contents] WHERE CmsID=" + objID).ToString();
                    if (!string.IsNullOrEmpty(cmsAttrib) && cmsAttrib == "0")
                    {
                        m_SqlParams = "UPDATE CMS_Contents SET CmsAttrib=1 WHERE [CmsID] IN(" + objID + ")";
                    }
                    else
                    {
                        m_SqlParams = "UPDATE CMS_Contents SET CmsAttrib=0 WHERE [CmsID] IN(" + objID + ")";
                    }
                    DbHelperSQL.ExecuteSql(m_SqlParams);
            
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息审核/取消审核操作成功！", m_TargetUrl, true);
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：考虑到系统安全，此操作每次只能选择一条记录，不可以多选！", m_TargetUrl, true);
            }
        }

        //取消审核
        private void HideInfo(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                try
                {
                    // CmsAttrib:0 默认;1 审核; 3 屏蔽; 4 删除; 9 公开
                    string cmsAttrib = DbHelperSQL.GetSingle("SELECT [CmsAttrib] FROM [CMS_Contents] WHERE CmsID=" + objID).ToString();
                    if (cmsAttrib == "3"){
                        m_SqlParams = "UPDATE CMS_Contents SET CmsAttrib=0 WHERE [CmsID] IN(" + objID + ")";
                    }
                    else{
                        m_SqlParams = "UPDATE CMS_Contents SET CmsAttrib=3 WHERE [CmsID] IN(" + objID + ")";
                    }
                    DbHelperSQL.ExecuteSql(m_SqlParams);

                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息屏蔽/取消屏蔽操作成功！", m_TargetUrl, true);
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                 MessageBox.ShowAndRedirect(this.Page, "操作提示：考虑到系统安全，此操作每次只能选择一条记录，不可以多选！", m_TargetUrl, true);
            }
        }

        /// <summary>
        /// 推荐信息
        /// </summary>
        /// <param name="objID"></param>
        private void RecommInfo(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                try
                {
                    // CmsAttrib:0 默认;1 审核; 3 屏蔽; 4 删除; 9 公开
                    string cmsAttrib = DbHelperSQL.GetSingle("SELECT [CmsAttrib] FROM [CMS_Contents] WHERE CmsID=" + objID).ToString();
                    if (!string.IsNullOrEmpty(cmsAttrib) && cmsAttrib == "1")
                    {
                        m_SqlParams = "UPDATE CMS_Contents SET CmsAttrib=9 WHERE [CmsID] IN(" + objID + ")";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                       
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息被推荐公开！", m_TargetUrl, true);
                    }
                    else
                    {
                         MessageBox.ShowAndRedirect(this.Page, "操作提示：请选择已经审核的信息进行操作！", m_TargetUrl, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：考虑到系统安全，此操作每次只能选择一条记录，不可以多选！", m_TargetUrl, true);
            }
        }

        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;//objCKeditor
            string CmsCID = PageValidate.GetTrim(this.txtCmsCID.Value);
            string CmsTitle = PageValidate.Encode(PageValidate.GetTrim(this.txtCmsTitle.Text));
            string CmsBody = PageValidate.Encode(CommBiz.GetTrim(this.objCKeditor.Text));// this.objCKeditor.value Request["objCKeditor"]
            string CmsKeys = "";// PageValidate.Encode(PageValidate.GetTrim(this.txtCmsKeys.Text));
            string OprateDate = PageValidate.GetTrim(this.txtOprateDate.Value);
    

            //if (String.IsNullOrEmpty(CmsCID))
            //{
            //    strErr += "请选择内容分类！\\n";
            //}
            if (String.IsNullOrEmpty(CmsTitle))
            {
                strErr += "请输入内容标题！\\n";
            }
            if (String.IsNullOrEmpty(CmsBody))
            {
                strErr += "请输入内容正文！\\n";
            }
            //if (!PageValidate.IsNumber(CmsCID))
            //{
            //    strErr += "内容分类错误！\\n";
            //}
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            bool isFilter = false;
            CmsTitle = CommPage.SetFilter(CmsTitle, ref isFilter);
            //CmsKeys = CommPage.SetFilter(CmsKeys, ref isFilter);
            CmsBody = CommPage.SetFilter(CmsBody, ref isFilter);
            // [CmsTitle], [CmsBody], [CmsKeys], [CmsCID], [UserID] 
            if (m_ActionName == "add")
            {
                try{
                    m_SqlParams = "INSERT INTO [CMS_Contents]([CmsTitle], [CmsBody], [CmsKeys], [UserID],CmsCode,OprateDate) VALUES('" + CmsTitle + "', '" + CmsBody + "', '" + CmsKeys + "', " + m_UserID + ",'" + m_FuncCode + "','" + OprateDate + "')";
                    m_SqlParams += "SELECT SCOPE_IDENTITY()";
                    string objID = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                    //更新上传的图片归属
                    DbHelperSQL.ExecuteSql("UPDATE CMS_Docs SET CmsID=" + objID + " WHERE UserID = " + m_UserID + " AND CmsID IS NULL");

                    MessageBox.ShowAndRedirect(this.Page, "操作提示：[" + CmsTitle + "]发布成功！", m_TargetUrl, true);
                    GetCmsDocs(objID);
                }
                catch (Exception ex) { MessageBox.Show(this, ex.Message); }
            }
            else if (m_ActionName == "edit")
            {
                
                try{
                    m_SqlParams = "UPDATE CMS_Contents SET CmsTitle='" + CmsTitle + "',CmsBody='" + CmsBody + "',CmsKeys='" + CmsKeys + "',OprateDate='" + OprateDate + "' WHERE CmsID=" + m_ObjID;
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    // 更新上传的图片归属 UserID=" + m_UserID + ",
                    DbHelperSQL.ExecuteSql("UPDATE CMS_Docs SET CmsID=" + m_ObjID + " WHERE UserID = " + m_UserID + " AND CmsID IS NULL");
                   
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：[" + CmsTitle + "]编辑成功！", m_TargetUrl, true);
                    GetCmsDocs(m_ObjID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                    return;
                }
            }
             MessageBox.ShowAndRedirect(this.Page, "操作提示：操作成功！", m_TargetUrl, true);
        }



        #region 文件上传

       

        /// <summary>
        /// 将上传文件信息记录数据库
        /// </summary>
        /// <param name="docsType">文件类型</param>
        /// <param name="docsPath">文件路径</param>
        /// <param name="sourceName">文件名称</param>
        private void SaveFileToDB(string docsType, string docsPath, string sourceName)
        {
            m_SqlParams = "INSERT INTO [CMS_Docs]([DocsType], [DocsPath], [SourceName], [UserID]) ";
            m_SqlParams += "VALUES( '" + docsType + "', '" + docsPath + "', '" + sourceName + "'," + m_UserID + ")";
            DbHelperSQL.ExecuteSql(m_SqlParams);
        }
        /// <summary>
        /// 检验是否是允许上传的文件类型
        /// </summary>
        /// <param name="uploadFileType"></param>
        /// <returns></returns>
        private bool IsAllowedFiles(string uploadFileType)
        {
            // f (fileContentType == "image/pjpeg" || fileContentType == "image/gif" || fileContentType == "image/jpeg" || fileContentType == "application/x-jpg")
            bool isValidFile = false;
            string[] allowFiles ={ ".gif", ".bmp", ".jpg", ".zip", ".rar", ".doc", ".docx", ".xls", ".xlsx", ".txt", ".swf", ".wmv", ".pdf", ".asf", ".mp3", ".rm", "rmvb", "mp4", ".bak", "htm", "html" };
            for (int i = 0; i < allowFiles.Length; i++)
            {
                if (uploadFileType == allowFiles[i])
                {
                    isValidFile = true;
                    break;
                }
            }
            return isValidFile;
        }
        /// <summary>
        /// 获取服务器保存路径
        /// </summary>
        /// <returns></returns>
        private string SetServerSavePath(string virtualPath)
        {
            string serverPath = Server.MapPath("/") + virtualPath;
            if (!System.IO.Directory.Exists(serverPath))
            {
                System.IO.Directory.CreateDirectory(serverPath);
            }
            return serverPath;
        }

        #endregion
    }
}

