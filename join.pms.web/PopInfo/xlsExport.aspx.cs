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

namespace AreWeb.OnlineCertificate.PopInfo
{
    /// <summary>
    /// �������ݵ���
    /// </summary>
    public partial class xlsExport : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        public string m_TargetUrl;

        protected string m_FuncCode;
        private string m_PageSearch;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_UserDept;//�û����ű���
        private string m_UserDeptArea;//�������
        private string m_UserDeptName;//��������

        private DataSet m_Ds;
        private string m_SqlParams;

        private string m_FuncInfo;
        private string m_Titles;
        private string m_Fields;
        private string m_Format;

        private string m_AreaCode;

        private ExportXls m_Xls;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                SetUIByFuncNo(m_FuncCode);
                SetReportArea(m_AreaCode);
            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Default.shtml?action=closewindow';</script>");
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
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl); // "FuncCode=04&p=1" FuncUser
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                //�̳в�ѯ����
                m_PageSearch = StringProcess.AnalysisParas(m_SourceUrlDec, "pSearch");

                m_UserDept = join.pms.dal.CommPage.GetUnitCodeByUser(m_UserID);
                m_UserDeptName = join.pms.dal.CommPage.GetUnitNameByCode(m_UserDept, ref m_UserDeptArea);
                //m_UserDeptArea = AreWeb.OnlineCertificate.Biz.CommPage.GetUnitNameByCode(m_UserDept);
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }
        }

        /// <summary>
        /// ���ò�����Ϊ
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetUIByFuncNo(string funcNo)
        {

            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGridShare.config");
            if (GetConfigParams(funcNo, configFile, ref errMsg))
            {
                m_Xls = new ExportXls();
                string[] a_FuncInfo = this.m_FuncInfo.Split(',');
                //
                this.m_Fields = this.m_Fields.Replace(",ReportDate,AuditFlagCN", "");
                m_AreaCode = "";
                /*
0101	��������
0102	��������  
0103	��׼����
                 */
                if (m_FuncCode.Substring(0, 4) == "0103")
                {
                    this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">��ʼҳ</a> &gt;&gt; ��׼���ݵ��� &gt;&gt;" + a_FuncInfo[2] + "��";
                    try { m_AreaCode = DbHelperSQL.GetSingle("SELECT FuncTarget FROM SYS_Function WHERE FuncCode='" + m_FuncCode + "'").ToString(); }
                    catch { m_AreaCode = ""; }
                }
                else if (m_FuncCode.Substring(0, 4) == "0102") { this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">��ʼҳ</a> &gt;&gt; �������ݵ��� &gt;&gt; " + a_FuncInfo[2] + "��"; }
                else { this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">��ʼҳ</a> &gt;&gt; ְ�ܲ��Ź�����Ϣ���� &gt;&gt; " + a_FuncInfo[2] + "��"; }
            }
            else
            {
                Response.Write(" <script>alert('��ȡ[" + funcNo + "]�����ļ�ʧ�ܣ�') ;window.location.href='" + m_TargetUrl + "'</script>");
                Response.End();
            }
            //SetDataToXls(a_FuncInfo[0], funcNo, " FuncNo='" + funcNo + "' ", a_FuncInfo[2], a_FuncInfo[2]);

            // INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES(FileName,FilePath,FileType,'0501',1,7)
        }

        /// <summary>
        /// �������ݵ�λ/����������λ �������򡭡�
        /// </summary>
        /// <param name="areaCode">Ĭ��ֵ</param>
        private void SetReportArea(string areaCode)
        {
            // �����û�ֻ����������������� by Ysl 2013/08/28
            if (m_UserDept.Substring(0, 2) == "03")
            {
                this.DDLReportArea.Items.Clear();
                this.DDLReportArea.Items.Insert(0, new ListItem(m_UserDeptName, m_UserDeptArea));
                this.DDLReportArea.SelectedValue = m_UserDept;
            }
            else
            {
                string siteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
                m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + siteArea + "' ORDER BY AreaCode";
                DataTable tmpDt = new DataTable();
                tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                DDLReportArea.DataSource = tmpDt;
                DDLReportArea.DataTextField = "AreaName";
                DDLReportArea.DataValueField = "AreaCode";
                DDLReportArea.DataBind();
                this.DDLReportArea.Items.Insert(0, new ListItem("ȫ��������������", ""));
                tmpDt = null;
                if (!string.IsNullOrEmpty(areaCode))
                {
                    this.DDLReportArea.SelectedValue = areaCode;
                }
            }
        }
        /*
0101	��������
0102	��������  
0103	��׼����
 */
        private void SetExcelByFuncNo(string funcNo, string filterSQL, string fileDesc)
        {
            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGridShare.config");
            string tmpFuncNo = string.Empty;
            if (GetConfigParams(funcNo, configFile, ref errMsg))
            {
                string[] a_FuncInfo = this.m_FuncInfo.Split(',');
                // ReportDate,AnalysisDate,AuditFlagCN
                if (funcNo.Substring(0, 2) == "11")
                {
                    this.m_Fields = this.m_Fields.Replace(",ReportDate,AuditFlagCN", "");
                }
                else if (funcNo.Substring(0, 4) == "0103" )
                {
                    // ȫ������
                }
                else
                {
                    this.m_Fields = this.m_Fields.Replace(",ReportDate,AnalysisDate,AuditFlagCN", "");
                }

                //ReportDate,AuditFlagCN
                m_Xls = new ExportXls();
                m_Xls.FuncNo = funcNo; // �ϼ���
                m_Xls.XlsName = a_FuncInfo[2];

                //======���ݲ������ݵ��� by ysl 2013/07/30
                tmpFuncNo = funcNo;
                //if (funcNo.Substring(0, 2) == "06" && funcNo!="060101") tmpFuncNo = "02" + funcNo.Substring(2);
                if (funcNo.Substring(0, 4) == "0101") tmpFuncNo = funcNo.Substring(0, 6);
                if (funcNo.Substring(0, 4) == "0102") tmpFuncNo = "0101" + funcNo.Substring(4, 2);
                //====================
                switch (tmpFuncNo)
                {
                    //���ݹ���-�������ݣ����ƾ�/������/������/�����/�����/ͳ�ƾ����ݵ�����
                    case "010101":
                        m_Xls.XlsUnit = "������Դ�����ƾ�";
                        m_Xls.Formats = this.m_Format;
                        break;
                    case "010102":
                        m_Xls.XlsUnit = "������Դ��������";
                        m_Xls.Formats = this.m_Format;
                        break;
                    case "010103":
                        m_Xls.XlsUnit = "������Դ��������";
                        m_Xls.Formats = this.m_Format;
                        break;
                    case "010104":
                        m_Xls.XlsUnit = "������Դ�������";
                        m_Xls.Formats = this.m_Format;
                        break;
                    case "010105":
                        m_Xls.XlsUnit = "������Դ�������";
                        m_Xls.Formats = this.m_Format;
                        break;
                    case "010106":
                        m_Xls.XlsUnit = "������Դ��ͳ�ƾ�";
                        m_Xls.Formats = this.m_Format;
                        break;
                    default:
                        Response.Write(" <script>alert('����ʧ�ܣ���������') ;window.location.href='" + m_TargetUrl + "'</script>");
                        break;
                }

                m_Xls.Fields = this.m_Fields;
                m_Xls.Titles = this.m_Titles;
                SetDataToXls(a_FuncInfo[0], funcNo, filterSQL, a_FuncInfo[2], fileDesc);
            }
            else
            {
                Response.Write(" <script>alert('��ȡ�����ļ�ʧ�ܣ�') ;window.location.href='" + m_TargetUrl + "'</script>");
                Response.End();
            }

            // INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES(FileName,FilePath,FileType,'0501',1,7)
        }


        /// <summary>
        /// ����ת��
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetAnalysisCode(string funcNo)
        {
            string returnVa = string.Empty;
            if (funcNo.Substring(0, 4) == "0102")
            {
                returnVa = "0101" + funcNo.Substring(4, funcNo.Length - 4);
            }
            else
            {
                returnVa = funcNo;
            }
            return returnVa;
        }
        private bool _isXiangZhen = false;
        private string _areaName;
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string strErr = string.Empty;
                string startDate = this.txtStartDate.Value;
                string endDate = this.txtEndDate.Value;
                string expAtt = Request["ExpAttribs"];
                string searchStr = string.Empty;
                string unitSearch = string.Empty;
                string fileDesc = string.Empty;
                string pageFilter = string.Empty;


                string areaCode = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Value);
                string areaName = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Text);
                //if (string.IsNullOrEmpty(areaCode))
                //{
                //    strErr += "��ѡ�����ݹ���������������\\n";
                //}
                if (String.IsNullOrEmpty(startDate))
                {
                    strErr += "��ѡ�����ݿ�ʼ���ڣ�\\n";
                }
                if (String.IsNullOrEmpty(endDate))
                {
                    strErr += "��ѡ��������ֹ���ڣ�\\n";
                }
                if (String.IsNullOrEmpty(expAtt))
                {
                    //strErr += "��ѡ��Ҫ�������������ͣ�\\n";
                    expAtt = "rb1";
                }


                if (strErr != "")
                {
                    MessageBox.Show(this, strErr);
                    return;
                }

                // �б����Ĳ�ѯ����
                if (!string.IsNullOrEmpty(m_PageSearch)) pageFilter = DESEncrypt.Decrypt(m_PageSearch);
                if (string.IsNullOrEmpty(pageFilter)) { pageFilter = "1=1"; }

                if (string.IsNullOrEmpty(areaCode))
                {
                    fileDesc = "���в��ţ�"; unitSearch = "";
                }
                else
                {
                    fileDesc = areaName + "��";
                    unitSearch = " AreaCode LIKE '" + areaCode.Substring(0, 9) + "%' "; 
                    //" AreaCode LIKE '%" + areaCode + "%' ";  [CreateDate] ReportDate,AuditFlagCN
                }
                if (string.IsNullOrEmpty(unitSearch))
                {
                    _isXiangZhen = false;
                    unitSearch = " 1=1 ";
                }
                else
                {
                    _isXiangZhen = true;
                    // ���ݵ�ַƥ�����򵼳�
                    string areaNo = System.Configuration.ConfigurationManager.AppSettings["AreaNo"];
                    string areaVal = System.Configuration.ConfigurationManager.AppSettings["AreaVal"];
                    //if (m_FuncCode.Substring(0, 2) != "05") unitSearch = " (" + AreWeb.OnlineCertificate.Biz.CommPage.GetAreaMatch(areaNo, areaVal, m_FuncCode, areaName) + " OR AreaCode LIKE '" + areaCode.Substring(0, 9) + "%') ";
                }
                _areaName = areaName;
                /*
0101	��������
0102	��������  
0103	��׼����
*/
                fileDesc += "��" + startDate + "��" + endDate + "��";
                if (m_FuncCode.Substring(0, 4) == "0103")
                {
                    fileDesc += "��׼����"; // ��׼����
                    searchStr = unitSearch + " AND  CreateDate>='" + startDate + " 00:00:00' AND CreateDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                else if (m_FuncCode == "01010101" || m_FuncCode == "01010102" || m_FuncCode == "01010103" || m_FuncCode == "01010104" || m_FuncCode == "01010105" || m_FuncCode == "01010106" || m_FuncCode == "01010122" || m_FuncCode == "01010123" || m_FuncCode == "01010124" || m_FuncCode == "01010125" || m_FuncCode == "01010126" || m_FuncCode == "01010127" || m_FuncCode == "01010128" || m_FuncCode == "01010129" || m_FuncCode == "01010130" || m_FuncCode == "01010131" || m_FuncCode == "01010116" || m_FuncCode == "01010117" || m_FuncCode == "01010118" || m_FuncCode == "01010120")
                {
                    fileDesc += "ȫԱ��������";
                    searchStr = unitSearch + " AND  FuncNo='" + m_FuncCode + "' AND Attribs IN(2,3) AND  OprateDate>='" + startDate + " 00:00:00' AND OprateDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                //else if (m_FuncCode.Substring(0, 2) == "05")
                //{
                //    fileDesc += "���֤У��ʧ��";
                //    searchStr = unitSearch + " AND  FuncNo='" + m_FuncCode + "' AND Attribs=3 AND  OprateDate>='" + startDate + " 00:00:00' AND OprateDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                //}
                else
                {                    
                    // rb5
                    if (expAtt == "rb1")
                    {
                        fileDesc += "ȫ��";
                        searchStr = unitSearch + " AND  FuncNo='" + GetAnalysisCode(m_FuncCode) + "' AND ReportDate>='" + startDate + " 00:00:00' AND ReportDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                    }
                    else if (expAtt == "rb2")
                    {
                        fileDesc += "δ������";
                        searchStr = unitSearch + " AND  FuncNo='" + GetAnalysisCode(m_FuncCode) + "' AND AuditFlag=0 AND ReportDate>='" + startDate + " 00:00:00' AND ReportDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                    }
                    else if (expAtt == "rb3")
                    {
                        fileDesc += "�Ѿ���˵�";
                        searchStr = unitSearch + " AND  FuncNo='" + GetAnalysisCode(m_FuncCode) + "' AND AuditFlag>2 AND AuditFlag!=4 AND ReportDate>='" + startDate + " 00:00:00' AND ReportDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                    }
                    else if (expAtt == "rb4")
                    {
                        fileDesc += "�Ѿ�������";
                        searchStr = unitSearch + " AND  FuncNo='" + GetAnalysisCode(m_FuncCode) + "' AND AuditFlag=9 AND ReportDate>='" + startDate + " 00:00:00' AND ReportDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                    }
                    else
                    {
                        fileDesc += "���ڲ����";
                        searchStr = unitSearch + " AND  FuncNo='" + GetAnalysisCode(m_FuncCode) + "' AND AnalysisFlag=2 AND ReportDate>='" + startDate + " 00:00:00' AND ReportDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                    }
                }

                if (m_FuncCode.Substring(0, 4) == "0103") { SetExcelByFuncNo("0103", searchStr, fileDesc); }
                else { SetExcelByFuncNo(m_FuncCode, searchStr, fileDesc); }

            }
            catch (Exception ex)
            {
                this.LiteralFiles.Text = ex.Message;
            }
            //SetDataToXls(a_FuncInfo[0], funcNo, " FuncNo='" + funcNo + "' ", a_FuncInfo[2], a_FuncInfo[2]);
        }

        /// <summary>
        /// ��ȡ����ƥ���ֶ�
        /// </summary>
        /// <param name="areaNo"></param>
        /// <param name="areaVal"></param>
        /// <returns></returns>
        private string GetAreaMatch(string areaNo, string areaVal, string areaName)
        {
            string returnVal = "1=1";
            string[] aryNo = areaNo.Split(',');
            string[] aryVal = areaVal.Split(',');
            if (areaName.Length > 2) areaName = areaName.Substring(0, 2);
            for (int i = 0; i < aryNo.Length; i++)
            {
                if (m_FuncCode == aryNo[i].Trim())
                {
                    returnVal = aryVal[i].Trim() + " LIKE '%" + areaName + "%'";
                    break;
                }
            }
            return returnVal;
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
            // if (m_FuncCode == "04")
            string sqlParams = "SELECT " + this.m_Fields + " FROM " + tableName + " WHERE " + filterSQL;
            string savePath = serverPath + virtualPath;
            string saveFiles = savePath + System.DateTime.Now.ToString("yyyyMMdd-hhmm") + ".xls";
            StringBuilder sHtml = new StringBuilder();
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

            if (m_Xls.ExportDsToXls(saveFiles, DbHelperSQL.Query(sqlParams), ref errMsg))
            {
                if (DbHelperSQL.Query(sqlParams).Tables[0].Rows.Count > 0)
                {
                    //SetRichTextBox(descInfo + "�ɹ�������");
                    m_Xls = null;
                    errMsg = errMsg.Replace(serverPath, "");
                    errMsg = errMsg.Substring(0, errMsg.Length - 1);
                    string[] aryFiles = errMsg.Split(',');
                    for (int i = 0; i < aryFiles.Length; i++)
                    {
                        sHtml.Append("<a href=\"" + aryFiles[i] + "\" target='_blank' > " + descInfo + fileName + "����</a><br/>");
                        SetFileToHD(descInfo + fileName + "����", aryFiles[i]);
                        sHtml.Append("�ĵ��Ѿ�ͬ��������[ ϵͳ����  >> ����Ӳ�� >> �ҵ����� >> ]Ŀ¼�¡���");
                        if (_isXiangZhen)
                        {
                            // ���ݿ���һ�ݵ�������Ϣ��ȥ
                            SetFileToXiangZhen(descInfo + fileName + "����", aryFiles[i]);
                            sHtml.Append("�ĵ�Ҳͬʱ������ ��������Ϣ�ڵ��¡���");
                        }
                    }
                    this.LiteralFiles.Text = sHtml.ToString();
                    //Response.Write(" <script>alert('" + descInfo + " --�ɹ�������') ;window.location.href='" + m_TargetUrl + "';window.location.href='" + saveFiles + "'</script>");
                }
                else
                {
                    this.LiteralFiles.Text = "û�з������������ݣ�";
                }
            }
            else
            {
                //SetRichTextBox(descInfo + "����ʧ�ܣ���ϸ��Ϣ���£�");
                this.LiteralFiles.Text = errMsg;
            }
        }
        //SELECT CmsCID, CmsCode, CmsCName FROM CMS_Class WHERE CmsCode LIKE '03__' AND CmsCName=
        private void SetFileToXiangZhen(string fileName, string filePath)
        {
            // 
            /*
040301	�ǹ���
040302	�ķ���
040303	������
040304	ӭ����
040305	�غ���
040306	������
040307	ϲ����
040308	�ٶ���
040309	����ɽ��
040310	��Ϫ��
040311	�г���
*/
            string cmsCID = "", cmsCode = "", cmsBody = string.Empty;
            DataTable tmpDt = new DataTable();
            m_SqlParams = "SELECT CmsCID, CmsCode, CmsCName FROM CMS_Class WHERE CmsCode LIKE '0403__' AND CmsCName='" + _areaName + "'";
            tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            if (tmpDt.Rows.Count == 1)
            {
                cmsCID = tmpDt.Rows[0][0].ToString();
                cmsCode = tmpDt.Rows[0][1].ToString();
            }
            tmpDt = null;
            cmsBody = "&lt;a&nbsp;href=&quot;" + filePath + "&quot;&nbsp;target=&quot;_blank&quot;&gt;" + fileName + "&lt;/a&gt;";
            m_SqlParams = "INSERT INTO [CMS_Contents](CmsTitle,CmsBody,CmsCID,UserID,CmsCode)  VALUES('" + fileName + "','" + cmsBody + "'," + cmsCID + "," + m_UserID + ",'" + cmsCode + "')";
            DbHelperSQL.ExecuteSql(m_SqlParams);
        }

        private void SetFileToHD(string fileName, string filePath)
        {
            // 
            m_SqlParams = "INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES('" + fileName + "','" + filePath + "','.xls','060202'," + m_UserID + ",1)";
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
                if (funcNo.Substring(0, 2) == "04")
                {
                    dr = m_Ds.Tables[0].Select("FuncNo='04'");
                }



                this.m_FuncInfo = dr[0][1].ToString();
                this.m_Titles = dr[0][2].ToString();
                this.m_Fields = dr[0][3].ToString();
                this.m_Format = dr[0][6].ToString();

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
