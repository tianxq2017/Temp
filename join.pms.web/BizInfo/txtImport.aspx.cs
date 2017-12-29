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
using System.Text;
using System.IO;

using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.dalInfo
{
    public partial class txtImport : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        public string m_TargetUrl;
        protected string m_FuncCode;

        private string m_UserID; // ��ǰ��¼�Ĳ����û���� SiteAreaName
        private DataSet m_Ds;
        private DataTable m_Dt;
        private string m_SqlParams;

        private string m_FuncInfo;
        private string m_Titles;
        private string m_Fields;
        private string m_Format;

        private string m_UpFileName;
        private string m_SiteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
        private string m_SiteAreaName = System.Configuration.ConfigurationManager.AppSettings["SiteAreaName"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            //this.butImport.Attributes.Add("onclick", "SetImportBut()");
            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                SetOpration(m_FuncCode);
                //SetReportArea("");
                //this.txtReportDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/loginTemp.aspx';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            if (!string.IsNullOrEmpty(m_SourceUrl))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");

            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }
        }
       
        /// <summary>
        /// �������ݵ�λ/����������λ �������򡭡�
        /// </summary>
        /// <param name="areaCode">Ĭ��ֵ</param>
        private void SetReportArea(string areaCode)
        {

            //string siteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
            //m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + siteArea + "' ORDER BY AreaCode";
            //DataTable tmpDt = new DataTable();
            //tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            //DDLReportArea.DataSource = tmpDt;
            //DDLReportArea.DataTextField = "AreaName";
            //DDLReportArea.DataValueField = "AreaCode";
            //DDLReportArea.DataBind();
            //this.DDLReportArea.Items.Insert(0, new ListItem("ȫ��������������", ""));
            //tmpDt = null;
            //if (!string.IsNullOrEmpty(areaCode))
            //{
            //    this.DDLReportArea.SelectedValue = areaCode;
            //}
        }

        private void SetOpration(string funcNo)
        {
            string funcName = GetFuncName();

            this.LiteralNav.Text = join.pms.dal.CommPage.GetAllTreeName(funcNo, true) + "���ݵ��룺";
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



        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butUpLoad_Click(object sender, EventArgs e)
        {
            // �ϴ��ļ�
            if (!String.IsNullOrEmpty(this.upFiles.FileName))
            {
                string fileName = this.upFiles.PostedFile.FileName;
                if (String.IsNullOrEmpty(fileName) || fileName.Length < 1 || !IsTxtFile(fileName))
                {
                    this.LabelMsg.Text = "������ѡ�����Ҫ����� *.txt �ı��ļ��������ʽ������ȷ�ϵ����ݸ�ʽΪ׼��";
                    return;
                }

                try
                {
                    string filePath = this.GetFilePhysicalPath() + this.GetPicPhysicalFileName(fileName);
                    this.upFiles.PostedFile.SaveAs(filePath);
                    Session["FileName"] = m_UpFileName;
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    Session["sourceFile"] = fileName; //"D:\\Temp\\20080827111448115.xls"
                    this.LabelMsg.Text = "�ļ�[ " + fileName + " ]�ϴ��ɹ���[" + filePath + "]��<br/>�������밴ťִ�е��롭��";
                }
                catch (System.Exception ex)
                {
                    this.LabelMsg.Text = ex.Message;
                }
            }
            else { this.LabelMsg.Text = "��ѡ���ļ�"; }
        }

        private bool m_IsAreaSel;
        /// <summary>
        /// ���ݵ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butImport_Click(object sender, EventArgs e)
        {
            this.butImport.Enabled = false; // ���ð�ť,��ֹ�ظ�����

            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGrid.config");
            GetConfigParams(this.m_FuncCode, configFile, ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                 MessageBox.ShowAndRedirect(this.Page, "������ʾ����ȡ�����ļ�ʧ�ܣ�", m_TargetUrl, true);
                this.butImport.Enabled = true;
            }
            string[] a_FuncInfo = this.m_FuncInfo.Split(',');
            string funcName = string.Empty;

            string reportDate = DateTime.Today.ToString("yyyy-MM-dd"); //PageValidate.GetTrim(this.txtReportDate.Value);

            if (Session["FileName"] != null)
            {
                m_UpFileName = GetFilePhysicalPath() + Session["FileName"].ToString(); //GetFilePhysicalPath() + xlsFile

                try
                {
                    funcName = GetFuncName();
                    SetTxtFileImport(m_FuncCode, funcName, reportDate, m_UpFileName);

                }
                catch (Exception ex)
                {
                    this.LiteralMsg.Text = "����ʧ�ܣ�<br/><br/>" + ex.Message; // " + m_SqlParams + "
                    this.butImport.Enabled = true;
                }
            }
            else
            {
                this.LiteralMsg.Text = "����ʧ�ܣ���ѡ���ļ��ϴ����ٵ�������롱��ť��";
                this.butImport.Enabled = true;
            }
            this.LiteralNav.Text = "������ҳ  &gt;&gt; " + funcName + "��";
        }

        private void SetTxtFileImport(string funcNo,string funcName,string reportDate,string txtFile)
        {
            string beg = DateTime.Now.ToLongTimeString();
            string reportArea = string.Empty,tmpCid=string.Empty;
            string areaCode = "", areaName = string.Empty;
            string[] a_Format = this.m_Format.Split(',');
            StringBuilder sHtml = new StringBuilder();
            int nRowPer = 500000; //ÿ�ε�����������  �����ڴ�

            try
            {
                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString()))
                {
                    using (SqlBulkCopy bulkCtrl = new SqlBulkCopy(con))
                    {
                        DataTable dt = new DataTable();
                        // Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11
                        dt.Columns.Add(new DataColumn("Fileds01", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("Fileds02", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("Fileds03", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("Fileds04", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("Fileds05", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("Fileds06", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("Fileds07", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("Fileds08", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("Fileds09", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("Fileds10", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("Fileds11", System.Type.GetType("System.String")));

                        dt.Columns.Add(new DataColumn("OprateUserID", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("FuncNo", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("ReportDate", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("AreaCode", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("AreaName", System.Type.GetType("System.String")));

                        bulkCtrl.DestinationTableName = "PIS_BaseInfo";

                        bulkCtrl.ColumnMappings.Add(0, "Fileds01"); 
                        bulkCtrl.ColumnMappings.Add(1, "Fileds02"); 
                        bulkCtrl.ColumnMappings.Add(2, "Fileds03"); 
                        bulkCtrl.ColumnMappings.Add(3, "Fileds04");
                        bulkCtrl.ColumnMappings.Add(4, "Fileds05");
                        bulkCtrl.ColumnMappings.Add(5, "Fileds06");
                        bulkCtrl.ColumnMappings.Add(6, "Fileds07");
                        bulkCtrl.ColumnMappings.Add(7, "Fileds08"); 
                        bulkCtrl.ColumnMappings.Add(8, "Fileds09");
                        bulkCtrl.ColumnMappings.Add(9, "Fileds10");
                        bulkCtrl.ColumnMappings.Add(10, "Fileds11");

                        bulkCtrl.ColumnMappings.Add(11, "OprateUserID");
                        bulkCtrl.ColumnMappings.Add(12, "FuncNo");
                        bulkCtrl.ColumnMappings.Add(13, "ReportDate");
                        bulkCtrl.ColumnMappings.Add(14, "AreaCode");
                        bulkCtrl.ColumnMappings.Add(15, "AreaName");
                        
                        bulkCtrl.BulkCopyTimeout = int.MaxValue;
                        sHtml.Append(DateTime.Now.ToString("hh:mm:ss") + "�����ݼ�������ϣ���ȡTxt�ļ����ݡ���<br/>");
                        StreamReader read = new StreamReader(txtFile, Encoding.Default);
                        con.Open();
                        int nRow = 0;
                        int nCnt = 0;
                        int i = 0;
                        int errType=0;
                        string line = read.ReadLine();
                        while (line != null)
                        {
                            // ���Ե�һ����ͷ
                            if (i > 0) {
                                nRow = dt.Rows.Count;
                                if (nRow > nRowPer) //����ļ�̫�����ռ���ڴ�̫��
                                {
                                    bulkCtrl.BatchSize = nRow;
                                    bulkCtrl.WriteToServer(dt);
                                    //bulkCtrl.Close();
                                    dt.Rows.Clear();
                                    //SetRichTextBox(DateTime.Now.ToString("hh:mm:ss") + "���Ѿ�����[500000]�����ݣ���ʼ��һ������");
                                    nCnt += nRow;
                                }
                                string[] str = line.Split('\t'); // �Ʊ���ָ�
                                if (funcNo == "020401")
                                {
                                    tmpCid = "Fileds03='" + StrTrim(str[2]) + "'";
                                    reportArea = StrTrim(str[9]);//���ɳ������������� Fileds03='" + cid.Trim() + "'
                                    if (!IsDataExist(funcNo, tmpCid, reportArea, ref errType))
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr[0] = StrTrim(str[0]);
                                        dr[1] = StrTrim(str[1]);
                                        dr[2] = StrTrim(str[2]);
                                        dr[3] = DateStrTrim(StrTrim(str[3]));
                                        dr[4] = StrTrim(str[4]);
                                        dr[5] = StrTrim(str[5]);
                                        dr[6] = StrTrim(str[6]);
                                        dr[7] = StrTrim(str[7]);
                                        dr[8] = StrTrim(str[8]);
                                        dr[9] = StrTrim(str[9]);
                                        dr[10] = StrTrim(str[10]);
                                        
                                        GetAreaInfo(reportArea, ref areaCode, ref areaName);

                                        dr[11] = m_UserID;
                                        dr[12] = funcNo;
                                        dr[13] = reportDate;
                                        dr[14] = areaCode;
                                        dr[15] = areaName;
                                        dt.Rows.Add(dr); // areaCode = "", areaName
                                    }
                                    else
                                    {
                                        if (errType == 0) { sHtml.Append("����Ϊ[ " + str[0] + " ]�����֤��Ϊ[ " + str[2] + " ]�ĳ����ϻ���Ϣ�����ظ�������������ԡ���<br/>"); }
                                        else { sHtml.Append("����Ϊ[ " + str[0] + " ]�����֤��Ϊ[ " + str[2] + " ]�ĳ����ϻ���Ϣ���ݽṹ����ȷ�������ԡ���<br/>"); }
                                    }
                                }
                                else
                                {
                                    if (funcNo == "020402") { reportArea = StrTrim(str[3]); tmpCid = "Fileds02='" + StrTrim(str[1]) + "'"; }
                                    else { reportArea = StrTrim(str[7]); tmpCid = "Fileds03='" + StrTrim(str[2]) + "'"; }
                                    if (!IsDataExist(funcNo, tmpCid, reportArea, ref errType))
                                    {
                                        DataRow dr = dt.NewRow();
                                        if (funcNo == "020402") {
                                            dr[0] = StrTrim(str[0]);
                                            dr[1] = StrTrim(str[1]);
                                            dr[2] = StrTrim(str[2]);
                                            dr[3] = StrTrim(str[3]);
                                            dr[4] = StrTrim(str[4]);
                                            dr[5] = DateStrTrim(StrTrim(str[5]));
                                            dr[6] = StrTrim(str[6]);
                                            dr[7] = StrTrim(str[7]);
                                            dr[8] = StrTrim(str[8]);
                                        } 
                                        else {
                                            dr[0] = StrTrim(str[0]);
                                            dr[1] = StrTrim(str[1]);
                                            dr[2] = StrTrim(str[2]);
                                            dr[3] = DateStrTrim(StrTrim(str[3]));
                                            dr[4] = StrTrim(str[4]);
                                            dr[5] = StrTrim(str[5]);
                                            dr[6] = StrTrim(str[6]);
                                            dr[7] = StrTrim(str[7]);
                                            dr[8] = StrTrim(str[8]);
                                        }
                                       
                                        dr[9] = "";
                                        dr[10] = "";

                                        
                                        GetAreaInfo(reportArea, ref areaCode, ref areaName);

                                        dr[11] = m_UserID;
                                        dr[12] = funcNo;
                                        dr[13] = reportDate;
                                        dr[14] = areaCode;
                                        dr[15] = areaName;
                                        dt.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        if (funcNo == "020402") {
                                            if (errType == 0) { sHtml.Append("����Ϊ[ " + str[0] + " ]�����֤��Ϊ[ " + str[1] + " ]��Ǩ����Ա��Ϣ�����ظ�������������ԡ���<br/>"); }
                                            else { sHtml.Append("����Ϊ[ " + str[0] + " ]�����֤��Ϊ[ " + str[1] + " ]��Ǩ����Ա��Ϣ���ݽṹ����ȷ�������ԡ���<br/>"); }
                                        } 
                                        else {
                                            if (errType == 0) { sHtml.Append("����Ϊ[ " + str[0] + " ]�����֤��Ϊ[ " + str[2] + " ]��Ǩ����Ա��Ϣ�����ظ�������������ԡ���<br/>"); }
                                            else { sHtml.Append("����Ϊ[ " + str[0] + " ]�����֤��Ϊ[ " + str[2] + " ]��Ǩ����Ա��Ϣ���ݽṹ����ȷ�������ԡ���<br/>"); }
                                        }
                                        
                                    }
                                    
                                }
                            }
                            i++;
                            line = read.ReadLine();
                        }

                        nRow = dt.Rows.Count;

                        if (nRow > 0)
                        {
                            nCnt += nRow;
                            bulkCtrl.BatchSize = nRow;
                            if (con.State != ConnectionState.Open) con.Open();
                            bulkCtrl.WriteToServer(dt);
                        }
                        bulkCtrl.Close();
                        con.Close();
                        dt.Clear();
                        dt = null;

                        sHtml.Append("<br/>" + string.Format(DateTime.Now.ToString("hh:mm:ss") + "���������\r\n��ʼʱ��:{0} ��\r\n����ʱ��:{1} ��\r\n�����������{2}\r\n", beg, DateTime.Now.ToLongTimeString(), nCnt));
                        sHtml.Append("<br/>" + funcName + "������ϣ�������[ " + nCnt.ToString() + "]��������<br/>");
                        GC.Collect();
                    }
                }
            }
            catch (Exception ex)
            {
                //SetRichTextBox(string.Format("����ʧ��: [StackTrace]{0}[Message]{1}\r\n", ex.StackTrace, ex.Message));
                sHtml.Append("<br/>" + string.Format("����ʧ��: [StackTrace]{0}[Message]{1}\r\n", ex.StackTrace, ex.Message)+"<br/>");
            }
            this.LiteralMsg.Text = sHtml.ToString();
        }
        /// <summary>
        /// �Ƿ�����ظ����� errType:0,�ظ� 1,���ݽṹ����
        /// </summary>
        /// <param name="funcNo"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        private bool IsDataExist(string funcNo, string filterSQL, string address,ref int errType)
        {
            if (string.IsNullOrEmpty(address)) {
                errType = 1;
                return true; 
            } 
            else {
                if (address.IndexOf("�ɳ���") < 0)
                {
                    errType = 1; // ���ݽṹ������
                    return true; 
                }
                else {
                    errType = 0;
                    string sqlParams = "SELECT COUNT(*) FROM PIS_BaseInfo WHERE FuncNo='" + funcNo + "' AND " + filterSQL;
                    if (join.pms.dal.CommPage.GetSingleVal(sqlParams) == "0")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                
            }
        }

        /// <summary>
        /// �����ɳ�����ȡ������������
        /// </summary>
        /// <param name="txtAreaInfo"></param>
        /// <param name="areaCode"></param>
        /// <param name="areaName"></param>
        /// <returns></returns>
        private void GetAreaInfo(string txtAreaInfo, ref string areaCode, ref string areaName)
        {
            if (!string.IsNullOrEmpty(txtAreaInfo)) {
                switch (txtAreaInfo.Trim())
                {
                    case "�ǹ��ɳ���":
                        areaCode = "610521100000";
                        areaName = "������";
                        break;
                    case "�����ɳ���":
                        areaCode = "610521101000";
                        areaName = "������";
                        break;
                    case "��ˮ�ɳ���":
                        areaCode = "610521102000";
                        areaName = "��ˮ��";
                        break;
                    case "��ɳ���":
                        areaCode = "610521102000";
                        areaName = "��ˮ��";
                        break;
                    case "�����ɳ���":
                        areaCode = "610521103000";
                        areaName = "������";
                        break;
                    case "�����ɳ���":
                        areaCode = "610521103000";
                        areaName = "������";
                        break;
                    case "�����ɳ���":
                        areaCode = "610521104000";
                        areaName = "������";
                        break;
                    case "�����ɳ���":
                        areaCode = "610521105000";
                        areaName = "������";
                        break;
                    case "�������ɳ���":
                        areaCode = "610521106000";
                        areaName = "��������";
                        break;
                    case "��֦�ɳ���":
                        areaCode = "610521107000";
                        areaName = "��֦��";
                        break;
                    case "�����ɳ���":
                        areaCode = "610521108000";
                        areaName = "������";
                        break;
                    case "����ɳ���":
                        areaCode = "610521109000";
                        areaName = "�����";
                        break;
                    default:
                        areaCode = m_SiteArea;
                        areaName = m_SiteAreaName;
                        break;
                    /*
�ǹ��ɳ��� --> ������
��ˮ�ɳ��� -->��ˮ��
�����ɳ���
�����ɳ��� --������
�����ɳ���  ->������
�����ɳ���
��ɳ��� --��ˮ��
����ɳ���
�������ɳ���
��֦�ɳ���
�����ɳ���
�����ɳ���
                     * 
                     * 610521100000	������
610521101000	������
610521102000	��ˮ�� + ��ׯ��
610521103000	������ + ������
610521104000	������ + �����
610521105000	������
610521106000	��������
610521107000	��֦�� + �ϼ���
610521108000	������
610521109000	�����
                     */
                }
            } 
            else {
                areaCode = m_SiteArea;
                areaName = m_SiteAreaName;
            }
        }

        private string GetFuncName()
        {
            string funcName = string.Empty;
            switch (this.m_FuncCode)
            {
                case "020401":
                    funcName = "�������ϻ���Ϣ";
                    break;
                case "020402":
                    funcName = "Ǩ����Ա��Ϣ";
                    break;
                case "020403":
                    funcName = "Ǩ����Ա��Ϣ";
                    break;
                default:
                    funcName = "";
                    break;
            }
            return funcName;
        }

        

        #region ���ݵ�����س�Ա����
        /// <summary>
        /// �ж��Ƿ�ΪTxt�ļ�
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool IsTxtFile(string fileName)
        {
            int intExt = fileName.LastIndexOf(".");
            if (intExt == -1)
            {
                return false;
            }

            string strExt = fileName.Substring(intExt).ToLower();
            if (strExt == ".txt")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GetPicPhysicalFileName(string strName)
        {
            int intExt = strName.LastIndexOf(".");
            string strExt = strName.Substring(intExt);
            strName = Guid.NewGuid().ToString() + strExt;
            this.m_UpFileName = strName;
            return strName;
        }
        // TempPath
        private string GetFilePhysicalPath()
        {
            string xlsFile = Server.MapPath("/") + "Temp/" + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + "/";//��ǰ�����ϴ�Ŀ¼
            if (!Directory.Exists(xlsFile)) Directory.CreateDirectory(xlsFile);
            return xlsFile;
        }
        
        private string StrTrim(string inStr)
        {
            if (!String.IsNullOrEmpty(inStr))
            {
                return inStr.Trim();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// ʱ���������
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        private string ToDateTime(string inStr)
        {
            if (!String.IsNullOrEmpty(inStr) && PageValidate.IsDateTime(inStr))
            {
                return inStr.Trim();
            }
            else
            {
                return "1900-01-01";
            }
        }

        private string DateStrTrim(string inStr)
        {
            if (!String.IsNullOrEmpty(inStr))
            {
                if (PageValidate.IsNumber(inStr) && inStr.Length>7) {
                    return inStr.Substring(0, 4) + "-" + inStr.Substring(4, 2) + "-" + inStr.Substring(6, 2); 
                }
                else { return "1900-01-01"; }
            }
            else
            {
                return "1900-01-01";
            }
        }
        private string NumberStrTrim(string inStr)
        {
            if (!String.IsNullOrEmpty(inStr) && PageValidate.IsNumber(inStr))
            {
                return inStr.Trim();
            }
            else
            {
                return "0";
            }
        }
        #endregion

    }
}
