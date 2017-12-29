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

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

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
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        #region

        /// <summary>
        /// �����֤
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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// ��֤���ܵĲ���
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
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        /// <summary>
        /// �������ݷ���
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
                    m_NavTitle = "��Ϣ�������� >> " + PageValidate.GetTrim(sdr["CmsCName"].ToString());
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }
        }

        /// <summary>
        /// ���ò�����Ϊ
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("�Ƿ����ʣ���������ֹ��");
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
                case "add": // ����
                    funcName = "����";
                    this.txtOprateDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
                    //this.LiteralCmsClass.Text = CustomerControls.CreateSelCtrl("txtCmsClass", "", "", "", "SELECT [CmsCID], [CmsCName] FROM [CMS_Class]");
                    break;
                case "edit": // �༭
                    funcName = "�༭";
                    IsAllowed(m_ObjID);
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // ɾ��
                    funcName = "ɾ��";
                    IsAllowed(m_ObjID);
                    DelInfo(m_ObjID);
                    break;
                case "view": // �鿴
                    funcName = "�鿴����";
                    ShowModInfo(m_ObjID);
                    break;
                case "audit": // ���
                    funcName = "�������";
                    AuditInfo(m_ObjID);
                    break;
                case "hide": // ����
                    funcName = "�������";
                    HideInfo(m_ObjID);
                    break;
                case "pub": // �����Ƽ�
                    funcName = "�Ƽ�";
                    RecommInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">��ʼҳ</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }

        
        /// <summary>
        /// �޸�
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

                    GetCmsDocs(m_ObjID); // ��ȡ���ݸ���
                }
                sdr.Close();
                // �Ƿ�ɱ༭
                if (!isEdit)
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ����ͨ����ˡ������򹫿�����Ϣ������Ϣ������༭��ֻ�д���δ���״̬�����ݲ��ܹ��༭��", m_TargetUrl, true);
                }
            }
            catch { if (sdr != null) sdr.Close(); }
        }

        /// <summary>
        /// ��ȡ���ݸ���
        /// </summary>
        /// <param name="objID"></param>
        private void GetCmsDocs(string objID)
        {
            //string docsType = string.Empty;
            //string docsPath = string.Empty;
            //string sourceFile = string.Empty; // ԭʼ�ļ���

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
            //            sHtml.Append("<a href=\"javascript:SetCKeditorDocs('" + sourceFile + "','" + docsPath + "','" + docsType + "','ist')\"><img src=\"" + docsPath + "\" alt=\"�뵥�����뵽�༭����\" width=\"100\" height=\"50\" /></a> ");
            //        }
            //        else
            //        {
            //            sHtml.Append("<a href=\"javascript:SetCKeditorDocs('" + sourceFile + "','" + docsPath + "','" + docsType + "','ist')\" title=\"�뵥�����뵽�༭����\"><b><font color=red size=3>" + sourceFile + "</font></b></a> ");
            //        }
            //    }
            //}
            //this.LiteralDocs.Text = sHtml.ToString();
            //sHtml = null;
        }

        /// <summary>
        /// �жϲ���Ȩ��
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
                //Response.Write(" <script>alert('����ʧ�ܣ���ϵͳ����Ա֮�⣬��ֻ�ܲ������˷�������Ϣ��') ;window.location.href='" + m_TargetUrl + "'</script>");
                //Response.End();
                MessageBox.ShowAndRedirect(this.Page, "������ʾ����ϵͳ����Ա֮�⣬��ֻ�ܲ������˷�������Ϣ��", m_TargetUrl, true);
            }
        }
        /// <summary>
        /// ɾ����Ϣ
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
                    //CmsAttrib:0 Ĭ��;1 ���; 3 ����; 4 ɾ��; 9 ����
                   
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ���ɹ�ɾ����", m_TargetUrl, true);
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
                }
            }
            else
            {             
                MessageBox.ShowAndRedirect(this.Page, "������ʾ������ѡ������ݱ�����˻򹫿�����Ϣ����ֹ������", m_TargetUrl, true);
            }
        }

        /// <summary>
        /// ɾ��ǰ���
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        public static bool CheckDelFlag(string objID)
        {
            // 0 Ĭ��;1 ���;2,�޸�  3 ����; 4 ɾ��; 9 ����
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
        /// �����Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void AuditInfo(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                try
                {
                    // CmsAttrib:0 Ĭ��;1 ���; 3 ����; 4 ɾ��; 9 ����
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
            
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ���/ȡ����˲����ɹ���", m_TargetUrl, true);
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ����¼�������Զ�ѡ��", m_TargetUrl, true);
            }
        }

        //ȡ�����
        private void HideInfo(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                try
                {
                    // CmsAttrib:0 Ĭ��;1 ���; 3 ����; 4 ɾ��; 9 ����
                    string cmsAttrib = DbHelperSQL.GetSingle("SELECT [CmsAttrib] FROM [CMS_Contents] WHERE CmsID=" + objID).ToString();
                    if (cmsAttrib == "3"){
                        m_SqlParams = "UPDATE CMS_Contents SET CmsAttrib=0 WHERE [CmsID] IN(" + objID + ")";
                    }
                    else{
                        m_SqlParams = "UPDATE CMS_Contents SET CmsAttrib=3 WHERE [CmsID] IN(" + objID + ")";
                    }
                    DbHelperSQL.ExecuteSql(m_SqlParams);

                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ����/ȡ�����β����ɹ���", m_TargetUrl, true);
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                 MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ����¼�������Զ�ѡ��", m_TargetUrl, true);
            }
        }

        /// <summary>
        /// �Ƽ���Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void RecommInfo(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                try
                {
                    // CmsAttrib:0 Ĭ��;1 ���; 3 ����; 4 ɾ��; 9 ����
                    string cmsAttrib = DbHelperSQL.GetSingle("SELECT [CmsAttrib] FROM [CMS_Contents] WHERE CmsID=" + objID).ToString();
                    if (!string.IsNullOrEmpty(cmsAttrib) && cmsAttrib == "1")
                    {
                        m_SqlParams = "UPDATE CMS_Contents SET CmsAttrib=9 WHERE [CmsID] IN(" + objID + ")";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                       
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ���Ƽ�������", m_TargetUrl, true);
                    }
                    else
                    {
                         MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ���Ѿ���˵���Ϣ���в�����", m_TargetUrl, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ����¼�������Զ�ѡ��", m_TargetUrl, true);
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
            //    strErr += "��ѡ�����ݷ��࣡\\n";
            //}
            if (String.IsNullOrEmpty(CmsTitle))
            {
                strErr += "���������ݱ��⣡\\n";
            }
            if (String.IsNullOrEmpty(CmsBody))
            {
                strErr += "�������������ģ�\\n";
            }
            //if (!PageValidate.IsNumber(CmsCID))
            //{
            //    strErr += "���ݷ������\\n";
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
                    //�����ϴ���ͼƬ����
                    DbHelperSQL.ExecuteSql("UPDATE CMS_Docs SET CmsID=" + objID + " WHERE UserID = " + m_UserID + " AND CmsID IS NULL");

                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��[" + CmsTitle + "]�����ɹ���", m_TargetUrl, true);
                    GetCmsDocs(objID);
                }
                catch (Exception ex) { MessageBox.Show(this, ex.Message); }
            }
            else if (m_ActionName == "edit")
            {
                
                try{
                    m_SqlParams = "UPDATE CMS_Contents SET CmsTitle='" + CmsTitle + "',CmsBody='" + CmsBody + "',CmsKeys='" + CmsKeys + "',OprateDate='" + OprateDate + "' WHERE CmsID=" + m_ObjID;
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    // �����ϴ���ͼƬ���� UserID=" + m_UserID + ",
                    DbHelperSQL.ExecuteSql("UPDATE CMS_Docs SET CmsID=" + m_ObjID + " WHERE UserID = " + m_UserID + " AND CmsID IS NULL");
                   
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��[" + CmsTitle + "]�༭�ɹ���", m_TargetUrl, true);
                    GetCmsDocs(m_ObjID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                    return;
                }
            }
             MessageBox.ShowAndRedirect(this.Page, "������ʾ�������ɹ���", m_TargetUrl, true);
        }



        #region �ļ��ϴ�

       

        /// <summary>
        /// ���ϴ��ļ���Ϣ��¼���ݿ�
        /// </summary>
        /// <param name="docsType">�ļ�����</param>
        /// <param name="docsPath">�ļ�·��</param>
        /// <param name="sourceName">�ļ�����</param>
        private void SaveFileToDB(string docsType, string docsPath, string sourceName)
        {
            m_SqlParams = "INSERT INTO [CMS_Docs]([DocsType], [DocsPath], [SourceName], [UserID]) ";
            m_SqlParams += "VALUES( '" + docsType + "', '" + docsPath + "', '" + sourceName + "'," + m_UserID + ")";
            DbHelperSQL.ExecuteSql(m_SqlParams);
        }
        /// <summary>
        /// �����Ƿ��������ϴ����ļ�����
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
        /// ��ȡ����������·��
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

