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

namespace join.pms.web.SysAdmin
{
    /// <summary>
    /// siteadmin ��־����
    /// </summary>
    public partial class xlsExport : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        public string m_TargetUrl;
        private string m_FuncCode;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private DataSet m_Ds;
        private string m_SqlParams;

        private string m_FuncInfo;
        private string m_Titles;
        private string m_Fields;

        private string m_SiteName = System.Configuration.ConfigurationManager.AppSettings["SiteName"];

        private ExportXls m_Xls;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
            }
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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/loginTemp.aspx';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_SourceUrl = Request.QueryString["sourceUrl"];
            if (!string.IsNullOrEmpty(m_SourceUrl))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                // "FuncCode=060101&p=1"
                //m_FuncCode = "02" + m_FuncCode.Substring(2);
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }
        }




        private void SetExcelByFuncNo(string funcNo, string filterSQL, string fileDesc)
        {

            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGrid.config");
            GetConfigParams(funcNo, configFile, ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ����ȡ�����ļ�ʧ�ܣ�", m_TargetUrl, true);
            }

            string[] a_FuncInfo = this.m_FuncInfo.Split(',');
            // UserAccount,OprateUserIP,OprateModel,OprateContents,OprateDate
            m_Xls = new ExportXls();
            m_Xls.FuncNo = funcNo; // �ϼ���
            m_Xls.XlsName = a_FuncInfo[2];
            m_Xls.Fields = this.m_Fields;
            m_Xls.Titles = this.m_Titles;

            switch (funcNo)
            {
                case "1101":
                    m_Xls.XlsUnit = "������Դ��" + m_SiteName;
                    m_Xls.Formats = "0,1,1,1,1,1,1,1,1,1,1,1,1,0,2,0"; // 1,���� ; 2,����
                    break;
                case "1103":
                    m_Xls.XlsUnit = "������Դ��" + m_SiteName;
                    //m_Xls.Formats = "0,1,0,0,0,1,0,0,0,0,0,2,0,1,2,0,2,0"; // 1,���� ; 2,����
                    m_Xls.Formats = "0,0,2,1,0,1,1,1,0,1,0,0,1,0,0,0,0,0,0,1,1,2,2,0";
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                    break;
            }

            SetDataToXls(a_FuncInfo[0], funcNo, filterSQL, a_FuncInfo[2], fileDesc);

            // INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES(FileName,FilePath,FileType,'0501',1,7)
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string strErr = string.Empty;
                string startDate = this.txtStartDate.Value;
                string endDate = this.txtEndDate.Value;
                string searchStr = string.Empty;
                string fileDesc = string.Empty;

                if (String.IsNullOrEmpty(startDate))
                {
                    strErr += "��ѡ�����ݿ�ʼ���ڣ�\\n";
                }
                if (String.IsNullOrEmpty(endDate))
                {
                    strErr += "��ѡ��������ֹ���ڣ�\\n";
                }

                if (strErr != "")
                {
                    MessageBox.Show(this, strErr);
                    return;
                }

                if (m_FuncCode == "1006") { 
                    fileDesc = "��" + startDate + "��" + endDate + "��";
                    searchStr = " OprateModel ='�����޸�' AND OprateDate>='" + startDate + " 00:00:00' AND OprateDate<'" + endDate + " 23:59:59'";
                }
                else { 
                    fileDesc = "��" + startDate + "��" + endDate + "��";
                    searchStr = " OprateModel !='�����޸�' AND OprateDate>='" + startDate + " 00:00:00' AND OprateDate<'" + endDate + " 23:59:59'";
                }

                SetExcelByFuncNo(m_FuncCode, searchStr, fileDesc);
            }
            catch (Exception ex)
            {
                this.LiteralFiles.Text = ex.Message;
            }


            //SetDataToXls(a_FuncInfo[0], funcNo, " FuncNo='" + funcNo + "' ", a_FuncInfo[2], a_FuncInfo[2]);

        }

        /// <summary>
        /// ����excel
        /// </summary>
        private void SetDataToXls(string tableName, string funcNo, string filterSQL, string fileName, string descInfo)
        {
            string errMsg = string.Empty;
            string serverPath = Server.MapPath("/");
            string configPath = System.Configuration.ConfigurationManager.AppSettings["FCKeditor:UserFilesPath"];//�ļ����·��
            string virtualPath = configPath + funcNo + "/" + StringProcess.GetCurDateTimeStr(6) + "/";

            string sqlParams = "SELECT " + this.m_Fields + " FROM " + tableName + " WHERE " + filterSQL;
            string savePath = serverPath + virtualPath;
            string saveFiles = savePath + System.DateTime.Now.ToString("yyyyMMdd-hhmm") + ".xls";
            StringBuilder sHtml = new StringBuilder();
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

            if (m_Xls.ExportDsToXls(saveFiles, DbHelperSQL.Query(sqlParams), ref errMsg))
            {
                //SetRichTextBox(descInfo + "�ɹ�������"); UserAccount,OprateUserIP,OprateModel,OprateContents,OprateDate
                m_Xls = null;
                errMsg = errMsg.Replace(serverPath, "");
                errMsg = errMsg.Substring(0, errMsg.Length - 1);
                string[] aryFiles = errMsg.Split(',');
                for (int i = 0; i < aryFiles.Length; i++)
                {
                    sHtml.Append("<a href=\"" + aryFiles[i] + "\" target='_blank' > " + descInfo + fileName + "����</a><br/>");
                    SetFileToHD(descInfo + fileName + "����", aryFiles[i]);
                    sHtml.Append("�ĵ��Ѿ�ͬ��������[ ����Ӳ�� >> �ҵ����� >> ]��Ŀ¼�¡���<br/>");
                    
                }
                //Response.Write(" <script>alert('" + descInfo + " --�ɹ�������') ;window.location.href='" + m_TargetUrl + "';window.location.href='" + saveFiles + "'</script>");
                sqlParams = "DELETE FROM SYS_Log WHERE " + filterSQL;
                DbHelperSQL.ExecuteSql(sqlParams);
                sHtml.Append("���������������ݿ����Ѿ�������ɾ������");
                this.LiteralFiles.Text = sHtml.ToString();
            }
            else
            {
                //SetRichTextBox(descInfo + "����ʧ�ܣ���ϸ��Ϣ���£�");
            }
        }

        private void SetFileToHD(string fileName, string filePath)
        {
            // 
            m_SqlParams = "INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES('" + fileName + "','" + filePath + "','.xls','0802'," + m_UserID + ",1)";
            DbHelperSQL.ExecuteSql(m_SqlParams);
        }

        #region  ��ȡ�����ļ�����
        /// <summary>
        /// �������ݼ�
        /// </summary>
        private void ConfigDataSet()
        {
            m_Ds = new DataSet();
            m_Ds.Locale = System.Globalization.CultureInfo.InvariantCulture;
        }
        /// <summary>
        /// ��ȡ�����ļ�����
        /// </summary>
        /// <param name="funcNo">���ܺ�</param>
        /// <param name="configFile">�����ļ�·��</param>
        /// <returns></returns>
        public bool GetConfigParams(string funcNo, string configFile, ref string errorMsg)
        {
            try
            {
                ConfigDataSet();

                m_Ds.ReadXml(configFile, XmlReadMode.ReadSchema);
                DataRow[] dr = m_Ds.Tables[0].Select("FuncNo='" + funcNo + "'");

                this.m_FuncInfo = dr[0][1].ToString();
                this.m_Titles = dr[0][2].ToString();
                this.m_Fields = dr[0][3].ToString();

                m_Ds = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = "��ȡ�����ļ�����ʧ�ܣ�" + ex.Message;
                return false;
            }
        }
        #endregion
    }
}

