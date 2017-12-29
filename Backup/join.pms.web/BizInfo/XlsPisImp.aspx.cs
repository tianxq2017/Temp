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

using System.Data.OleDb;
using System.Data.SqlClient;
using UNV.Comm.DataBase;
using UNV.Comm.Web;
using System.Text;
using System.IO;

namespace join.pms.dalInfo
{
    public partial class XlsPisImp : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        public string m_TargetUrl;
        protected string m_FuncCode;

        private string m_UserID; // ��ǰ��¼�Ĳ����û���� 
        private DataSet m_Ds;
        private DataTable m_Dt;
        private string m_SqlParams;

        private string m_FuncInfo;
        private string m_Titles;
        private string m_Fields;
        private string m_UpFileName;

        private string m_SiteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
        private string m_SiteAreaNa = System.Configuration.ConfigurationManager.AppSettings["SiteAreaName"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            //this.butImport.Attributes.Add("onclick", "SetImportBut()");
            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                
                //SetReportArea("");
                this.txtReportDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "";// DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
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
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            if (!string.IsNullOrEmpty(m_SourceUrl))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                SetOpration(m_FuncCode);
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
                if (String.IsNullOrEmpty(fileName) || fileName.Length < 1 || !IsXlsFile(fileName))
                {
                    this.LabelMsg.Text = "������ѡ�����Ҫ����� Excel �����ļ��������ʽ��ϵͳ�����ĸ�ʽΪ׼��";
                    return;
                }

                try
                {
                    string filePath = this.GetFilePhysicalPath() + this.GetPicPhysicalFileName(fileName);
                    this.upFiles.PostedFile.SaveAs(filePath);
                    Session["FileName"] = m_UpFileName;
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    Session["sourceFile"] = fileName;//"D:\\Temp\\20080827111448115.xls"
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
            }
            string[] a_FuncInfo = this.m_FuncInfo.Split(',');
            string funcName = string.Empty;

            string ReportDate = PageValidate.GetTrim(this.txtReportDate.Value);
            string areaCode = PageValidate.GetTrim(this.UcAreaSel1.GetAreaCode());
            string areaName = PageValidate.GetTrim(this.UcAreaSel1.GetAreaName());
            if (!string.IsNullOrEmpty(areaCode))
            {

                //if (areaCode.Substring(9) == "000" && m_FuncCode.Substring(0,2)!="05")
                //{
                //    this.LiteralMsg.Text = "ȫԱ���ѯ���ݵ���Ҫ����嵽����/��һ�����밴��ײ�����������λ���룡";
                //    this.butImport.Enabled = true;
                //    return;
                //}
            }
            else {
                areaCode = m_SiteArea;
                areaName = m_SiteAreaNa; // SiteAreaName
                this.LiteralMsg.Text = "��ѡ�����ݵ�λ��";
                this.butImport.Enabled = true;
                return;
            }
            if (String.IsNullOrEmpty(ReportDate))
            {
                this.LiteralMsg.Text = "��ѡ���������ڣ�";
                this.butImport.Enabled = true;
                return;
            }


            if (Session["FileName"] != null)
            {
                m_UpFileName = Session["FileName"].ToString();

                try
                {
                    // OprateDate,AttribsCN,CommMemo
                    funcName = GetFuncName();
                    ImportXlsData(this.m_FuncCode, a_FuncInfo[0], " FuncNo='" + this.m_FuncCode + "' ", ReadXlsToDs(m_UpFileName),ReportDate, areaCode, areaName);
                }
                catch (Exception ex)
                {
                    this.LiteralMsg.Text = "����ʧ�ܣ�<br/><br/>" + ex.Message; // " + m_SqlParams + "
                }
            }
            else
            {
                this.LiteralMsg.Text = "����ʧ�ܣ���ѡ���ļ��ϴ����ٵ�������롱��ť��";
                this.butImport.Enabled = true;
            }
            this.LiteralNav.Text = "������ҳ  &gt;&gt; " + funcName + "��";
        }

        private string GetFuncName()
        {
            string funcName = string.Empty;
            switch (this.m_FuncCode)
            {
                case "1011":
                    funcName = "�˿ڻ���������Ϣ";
                    break;
                case "0502":
                    funcName = "�˿ڱ䶯������Ϣ";
                    break;
                case "0503":
                    funcName = "�˿ڱ䶯������Ϣ";
                    break;
                case "0504":
                    funcName = "�˿ڱ䶯Ǩ���˿�";
                    break;
                case "0505":
                    funcName = "�˿ڱ䶯Ǩ����Ϣ";
                    break;
                case "0513":
                    funcName = "ȫԱ�������Ů��Ϣ";
                    break;
                case "0514":
                    funcName = "һ֤ͨ������Ů��Ϣ";
                    break;
                case "0515":
                    funcName = "ȫԱ�������������Ϣ";
                    break;
                case "0516":
                    funcName = "һ֤ͨ�������������Ϣ";
                    break;
                default:
                    funcName = "";
                    break;
            }
            return funcName;
        }

        #region ���ݵ���

        /// <summary>
        /// ����QYK����
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportXlsData(string funcNo, string tableName, string filterSQL, DataSet xlsDs, string oprateDate, string areaCode, string areaName)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null || xlsDs.Tables[0].Columns.Count < 1)
            {
                sHtml.Append("����ʧ�ܣ���ȡ Excel ���ݼ�����<br>");
                sHtml.Append("����취����ȷ������Ҫ�����Excel�ļ����ϵ������ݵı�׼��ʽ��Ȼ�����ԡ�<br>");
                sHtml.Append("�����ϵͳ�Զ����ɵ�Excel�ļ�������Excel�򿪺����Ϊ��Microsoft Office Excel ������(*.xls)����ʽ��Ȼ�����ԡ�<br>");
                sHtml.Append("�������ݵ� Excel��ͷ�ı�׼��ʽΪϵͳ��������Ӧ�ļ��ĸ�ʽ��");
                this.LiteralMsg.Text = sHtml.ToString();
                return;
            }

            // �Ѿ����ڵļ�¼ OprateDate,AttribsCN,CommMemo
            //m_SqlParams = "SELECT * FROM " + tableName + " WHERE " + filterSQL;
            //m_Dt = new DataTable();
            //m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // ��ʼ����
            int debugRow = 0;
            StringBuilder selSql = null;
            StringBuilder strSql = null;

            try
            {
                string totalCount = xlsDs.Tables[0].Rows.Count.ToString(); // ����
                int iCol = xlsDs.Tables[0].Columns.Count;
                int iPassed = 0; // �ɹ���   
                int iUnPass = 0; // ʧ����
                int iHuLue = 0; // ������
                int iRe = 0;
                // ����ǰ����
                for (int i = 0; i < xlsDs.Tables[0].Rows.Count; i++)
                {
                    debugRow++;

                    // �ж�ĳ����¼�Ƿ��ظ�,�����ظ��ĺ���
                    selSql = new StringBuilder();
                    selSql.Append("SELECT COUNT(*) FROM QYK_Persons WHERE PersonCID=@PersonCID ");
                    SqlParameter[] sParams = {
					new SqlParameter("@PersonCID", SqlDbType.VarChar,20)
                    };
                    sParams[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString());

                    // INSERT ��¼
                    strSql = new StringBuilder();
                    strSql.Append("INSERT INTO QYK_Persons(");
                    strSql.Append("PersonRegNo,PersonName,PersonSex,PersonCID,PersonVillage,");
                    strSql.Append("PersonHouseMum,PersonBirthday,PersonNation,PersonEdu,PersonRegType,");
                    strSql.Append("MaritalStatus,WeddingDate,PersonNexus,RegAreaName,CurAreaName,");
                    strSql.Append("MateName,MateCID,FatherName,FatherCID,MatherName,");
                    strSql.Append("MatherCID,LivingState,RegisterArea,SelAreaCode,SelAreaName");
                    //strSql.Append("MatherCID,LivingState,RegisterArea,RegAreaCode,CurAreaCode");
                    strSql.Append(") VALUES(");
                    strSql.Append("@PersonRegNo,@PersonName,@PersonSex,@PersonCID,@PersonVillage,");
                    strSql.Append("@PersonHouseMum,@PersonBirthday,@PersonNation,@PersonEdu,@PersonRegType,");
                    strSql.Append("@MaritalStatus,@WeddingDate,@PersonNexus,@RegAreaName,@CurAreaName,");
                    strSql.Append("@MateName,@MateCID,@FatherName,@FatherCID,@MatherName,");
                    strSql.Append("@MatherCID,@LivingState,@RegisterArea,@SelAreaCode,@SelAreaName");
                    //strSql.Append("@MatherCID,@LivingState,@RegisterArea,@RegAreaCode,@CurAreaCode");
                    strSql.Append(");select @@IDENTITY");
                    SqlParameter[] parameters = {
					new SqlParameter("@PersonRegNo", SqlDbType.VarChar,50),
					new SqlParameter("@PersonName", SqlDbType.NVarChar,20),
                    new SqlParameter("@PersonSex", SqlDbType.NVarChar,10),
					new SqlParameter("@PersonCID", SqlDbType.VarChar,20),
                    new SqlParameter("@PersonVillage", SqlDbType.NVarChar,50),
                    // PersonRegNo,@PersonName,@PersonSex,@PersonCID,@PersonVillage
					new SqlParameter("@PersonHouseMum", SqlDbType.VarChar,50),
					new SqlParameter("@PersonBirthday", SqlDbType.SmallDateTime,4),
					new SqlParameter("@PersonNation", SqlDbType.NVarChar,50),
					new SqlParameter("@PersonEdu", SqlDbType.NVarChar,50),
                  	new SqlParameter("@PersonRegType", SqlDbType.NVarChar,50), 
                    // PersonHouseMum,@PersonBirthday,@PersonNation,@PersonEdu,@PersonRegType
                    new SqlParameter("@MaritalStatus", SqlDbType.NVarChar,50), 
                    new SqlParameter("@WeddingDate", SqlDbType.SmallDateTime,4), 
                    new SqlParameter("@PersonNexus", SqlDbType.NVarChar,50), 
                    new SqlParameter("@RegAreaName", SqlDbType.NVarChar,50),
                    new SqlParameter("@CurAreaName", SqlDbType.NVarChar,50),
                    // MaritalStatus,@WeddingDate,@PersonNexus,@RegAreaName,@CurAreaName
					new SqlParameter("@MateName", SqlDbType.NVarChar,20),
					new SqlParameter("@MateCID", SqlDbType.VarChar,20),
					new SqlParameter("@FatherName", SqlDbType.NVarChar,20),
					new SqlParameter("@FatherCID", SqlDbType.VarChar,20),
                  	new SqlParameter("@MatherName", SqlDbType.NVarChar,20), 
                    // MateName,@MateCID,@FatherName,@FatherCID,@MatherName
                    new SqlParameter("@MatherCID", SqlDbType.VarChar,20), 
                    new SqlParameter("@LivingState", SqlDbType.NVarChar,50), 
                    new SqlParameter("@RegisterArea", SqlDbType.VarChar,50),
                    new SqlParameter("@SelAreaCode", SqlDbType.VarChar,20),
					new SqlParameter("@SelAreaName", SqlDbType.NVarChar,50)
                   };
                    parameters[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString());
                    parameters[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString());
                    parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString()); 
                    parameters[3].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString()); // ���֤��
                    parameters[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString());

                    if (iCol > 5) { parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString()); } else { parameters[5].Value = ""; }
                    if (iCol > 6) { parameters[6].Value = DateStrTrim(xlsDs.Tables[0].Rows[i][6].ToString()); } else { parameters[6].Value = ""; }
                    if (iCol > 7) { parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); } else { parameters[7].Value = ""; }
                    if (iCol > 8) { parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); } else { parameters[8].Value = ""; }
                    if (iCol > 9) { parameters[9].Value = StrTrim(xlsDs.Tables[0].Rows[i][9].ToString()); } else { parameters[9].Value = ""; }
                    // ���˱��,����,�Ա�,���֤��,С��������,���ƺ�,��������,����,�Ļ��̶�,��������,
                    // ����״��,��������,�뻧����ϵ,�������ڵ�,�־�ס��,��ż����,��ż���֤��,��������,�������֤��,ĸ������,
                    // ĸ�����֤��,��ס״��,�仧����
                    if (iCol > 10) { parameters[10].Value = StrTrim(xlsDs.Tables[0].Rows[i][10].ToString()); } else { parameters[10].Value = ""; }
                    if (iCol > 11) { parameters[11].Value = DateStrTrim(xlsDs.Tables[0].Rows[i][11].ToString()); } else { parameters[11].Value = ""; }
                    if (iCol > 12) { parameters[12].Value = StrTrim(xlsDs.Tables[0].Rows[i][12].ToString()); } else { parameters[12].Value = ""; }
                    if (iCol > 13) { parameters[13].Value = StrTrim(xlsDs.Tables[0].Rows[i][13].ToString()); } else { parameters[13].Value = ""; }
                    if (iCol > 14) { parameters[14].Value = StrTrim(xlsDs.Tables[0].Rows[i][14].ToString()); } else { parameters[14].Value = ""; }
                    if (iCol > 15) { parameters[15].Value = StrTrim(xlsDs.Tables[0].Rows[i][15].ToString()); } else { parameters[15].Value = ""; }
                    if (iCol > 16) { parameters[16].Value = StrTrim(xlsDs.Tables[0].Rows[i][16].ToString()); } else { parameters[16].Value = ""; }
                    if (iCol > 17) { parameters[17].Value = StrTrim(xlsDs.Tables[0].Rows[i][17].ToString()); } else { parameters[17].Value = ""; }
                    if (iCol > 18) { parameters[18].Value = StrTrim(xlsDs.Tables[0].Rows[i][18].ToString()); } else { parameters[18].Value = ""; }
                    if (iCol > 19) { parameters[19].Value = StrTrim(xlsDs.Tables[0].Rows[i][19].ToString()); } else { parameters[19].Value = ""; }

                    if (iCol > 20) { parameters[20].Value = StrTrim(xlsDs.Tables[0].Rows[i][20].ToString()); } else { parameters[20].Value = ""; }
                    if (iCol > 21) { parameters[21].Value = StrTrim(xlsDs.Tables[0].Rows[i][21].ToString()); } else { parameters[21].Value = ""; }
                    if (iCol > 22) { parameters[22].Value = StrTrim(xlsDs.Tables[0].Rows[i][22].ToString()); } else { parameters[22].Value = ""; }
                    //if (iCol > 23) { parameters[23].Value = StrTrim(xlsDs.Tables[0].Rows[i][23].ToString()); } else { parameters[23].Value = ""; }
                    parameters[23].Value = areaCode;
                    parameters[24].Value = areaName;

                    //���֤��У�� Attribs:0,Ĭ�� 3,���֤У��ʧ��
                    //if (!ValidIDCard.VerifyIDCard(parameters[3].Value.ToString()))
                    //{
                    //    parameters[30].Value = 3;
                    //    iHuLue++;
                    //}
                    //else
                    //{
                    //    parameters[30].Value = 0;
                    //}
  
                    // ִ�в���
                    /*
                    if (DbHelperSQL.GetSingle(selSql.ToString(), sParams).ToString() == "0")
                    {
                        //���ظ�����,����
                        if (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0)
                        {
                            iPassed++;
                        }
                        else
                        {
                            iUnPass++;
                        }
                    }
                    else { iRe++; }
                    */
                    // ������ظ����ݵ���
                    if (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0)
                    {
                        iPassed++;
                    }
                    else
                    {
                        iUnPass++;
                    }
                    selSql = null;
                    strSql = null;
                    // ִ�бȶ� -- �ں�̨����ʵ��
                }
                xlsDs.Dispose();
                sHtml.Append("<br><br>�����ļ�����" + totalCount + "��Ϣ���ɹ�������" + iPassed.ToString() + "����Ϣ���ظ�����Ϣ[ " + iRe.ToString() + " ]����ʧ����" + iUnPass.ToString() + "����Ϣ��");
            }
            catch (Exception ex)
            {
                sHtml.Append("<br/>��������������д���Ϊ[" + debugRow + "]<br/>");
                sHtml.Append(ex.Message);
            }

            this.LiteralMsg.Text = sHtml.ToString();
        }

        #endregion

        #region ��������

        private DataTable m_AreaDt;

        /// <summary>
        /// ��ȡ�����������ֶ�����ֵ,��1��ʼ
        /// </summary>
        /// <param name="funcNo"></param>
        /// <param name="xlsDt"></param>
        /// <returns></returns>
        private int GetAreaIndex(string funcNo, DataTable xlsDt, ref bool checkOK)
        {
            int iFileds = 0;
            string areaIndex = "";
            string areaNo = System.Configuration.ConfigurationManager.AppSettings["AreaNo"];
            string areaVal = System.Configuration.ConfigurationManager.AppSettings["AreaVal"];
            try
            {
                string[] aryNo = areaNo.Split(',');
                string[] aryVal = areaVal.Split(',');
                string impArea = string.Empty, dbArea = string.Empty;
                bool isBreak = false;
                for (int i = 0; i < aryNo.Length; i++)
                {
                    if (funcNo == aryNo[i])
                    {
                        areaIndex = aryVal[i];// Fileds15
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(areaIndex))
                {
                    areaIndex = areaIndex.Replace("Fileds", "");
                    if (!string.IsNullOrEmpty(areaIndex)) iFileds = int.Parse(areaIndex);

                    //��ȡ������������
                    string siteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
                    m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + siteArea + "' ORDER BY AreaCode";
                    m_AreaDt = new DataTable();
                    m_AreaDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                    //������������Ƿ��ַ��λ����
                    for (int j = 2; j < xlsDt.Rows.Count; j++)
                    {
                        impArea = xlsDt.Rows[j][iFileds - 1].ToString();
                        for (int k = 0; k < m_AreaDt.Rows.Count; k++)
                        {
                            dbArea = m_AreaDt.Rows[k][1].ToString();
                            if (impArea.IndexOf(dbArea) > -1)
                            {
                                checkOK = true;
                                isBreak = true;
                                break;
                            }
                        }
                        if (isBreak) break;
                    }
                    if (isBreak)
                    {
                        //m_AreaDt = null; ����������������ʱʹ��
                        xlsDt = null;
                    }
                }
                else
                {
                    checkOK = true; // �������ڵ�ַ������÷�Χ�ڵĺ��Ե�ַ���
                }
            }
            catch { }
            return iFileds;
        }

        // GetAreaPara(xlsDs.Tables[0].Rows[i][iArea-1].ToString(), ref areaCode, ref areaName);
        private void GetAreaPara(string xlsArea, ref string areaCode, ref string areaName)
        {
            for (int k = 0; k < m_AreaDt.Rows.Count; k++)
            {
                areaName = m_AreaDt.Rows[k][1].ToString();
                if (xlsArea.IndexOf(areaName) > 0)
                {
                    areaCode = m_AreaDt.Rows[k][0].ToString();
                    break;
                }
            }
        }

        #endregion

        #region ���ݵ�����س�Ա����
        /// <summary>
        /// �ж��Ƿ�Ϊxls�ļ�
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool IsXlsFile(string fileName)
        {
            int intExt = fileName.LastIndexOf(".");
            if (intExt == -1)
            {
                return false;
            }
            string strExt = fileName.Substring(intExt).ToLower();

            if (strExt == ".xls")
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

        /// <summary>
        /// ��ȡ Excel �ļ������ݼ���
        /// </summary>
        /// <param name="xlsFile"></param>
        private DataSet ReadXlsToDs(string xlsFile)
        {
            try
            {
                string connStr = "Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + GetFilePhysicalPath() + xlsFile + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";
                // wsYsl
                if (Session["sourceFile"] == null)
                {
                    Response.Write("������ʱ�������µ�¼�����ԣ�");
                    return null;
                }
                else
                {
                    string sourceFile = Session["sourceFile"].ToString();
                    string xlsTable = string.Empty;
                    if (sourceFile.IndexOf("wsYsl") > -1)
                    {
                        xlsTable = "[Sheet1$]";
                    }
                    else
                    {
                        xlsTable = GetXlsSheetName(connStr);
                    }

                    DbHelperOleDb.connectionString = connStr;
                    return DbHelperOleDb.Query(" SELECT * FROM " + xlsTable + "");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string GetXlsSheetName(string connStr)
        {
            string sheetName = string.Empty;
            OleDbConnection objConn = new OleDbConnection(connStr);
            objConn.Open();
            DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            sheetName = "[" + dt.Rows[0]["TABLE_NAME"].ToString().Trim() + "]";
            dt.Clear();
            objConn.Close();
            return sheetName;
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
                try
                {
                    if (PageValidate.IsDateTime(inStr)) {
                        if (DateTime.Parse(inStr.Trim()) < DateTime.Parse("1900/01/01")) { return "1900-01-01"; }
                        if (DateTime.Parse(inStr.Trim()) > DateTime.Parse("2079/01/01")) { return "2079-01-01"; }
                        else { return DateTime.Parse(inStr.Trim()).ToString("yyyy/MM/dd"); }
                    }
                    else { return "1900-01-01"; }
                }
                catch { return "1900-01-01"; }
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

