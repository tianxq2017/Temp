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

namespace join.pms.web.SysAdmin
{
    /// <summary>
    /// ���ݵ���
    /// </summary>
    public partial class DataImport : System.Web.UI.Page
    {
        StringBuilder toop = new StringBuilder();
        private string m_SourceUrl;
        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_UpFileName;
        private string m_SqlParams;
        private string m_TargetUrl;

        private DataTable m_Dt;
        private string m_FuncCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();

            m_SourceUrl = Request.QueryString["sourceUrl"];

            if (!String.IsNullOrEmpty(m_SourceUrl))
            {
                m_SourceUrl = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrl;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrl, "FuncCode");
            }
            //if (!IsPostBack)
            //{
            if (String.IsNullOrEmpty(m_FuncCode) || !PageValidate.IsNumber(m_FuncCode))
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }
            else
            {
                SetOpratetionAction("��Ϣ����");
            }
            //}
        }

        #region

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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Ysl9lcXf6JuzBkRL1yQD48cmhxCD5exHudvJr7ExPl6SnOYhiJLFhhdlZx1OzuA1vCf.shtml';</script>");
                Response.End();
            }
        }
        private string getRealFun(string funNo)
        {
            string strReturn = string.Empty;
            switch (funNo)
            {
                case "010501":
                    strReturn = "020101";
                    break;
                case "010502":
                    strReturn = "020102";
                    break;
                case "010503":
                    strReturn = "020103";
                    break;
                case "010504":
                    strReturn = "020104";
                    break;
                case "010505":
                    strReturn = "020105";
                    break;
                case "010506":
                    strReturn = "020106";
                    break;
                default:
                    strReturn = funNo;
                    break;
            }
            return strReturn;
        }
        /// <summary>
        /// ���ò�����Ϊ
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            switch (getRealFun(m_FuncCode))
            {
                case "020101": // �������Ż����Ǽ���Ϣ����
                    funcName = "�������Ż����Ǽ���Ϣ����";
                    break;
                case "020102": // ��������סԺ����ʵ���Ǽ���Ϣ����
                    funcName = "��������סԺ����ʵ���Ǽ���Ϣ����";
                    break;
                case "020103": // �������������˿ڱ�����Ա�Ǽ���Ϣ����
                    funcName = "�������������˿ڱ�����Ա�Ǽ���Ϣ����";
                    break;
                case "020104": // �������Ű����ס֤��Ա�Ǽ���Ϣ����
                    funcName = "�������Ű����ס֤��Ա�Ǽ���Ϣ����";
                    break;
                case "020105": // ��������Ǩ����Ա�Ǽ���Ϣ����
                    funcName = "��������Ǩ����Ա�Ǽ���Ϣ����";
                    break;
                case "020106": // ���������˿ڻ������ͳ����Ϣ����
                    funcName = "���������˿ڻ������ͳ����Ϣ����";
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = oprateName + " &gt;&gt; " + funcName + "��";
        }

        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="errorMsg"></param>
        private void ShowClientMsg(string errorMsg)
        {
            if (!String.IsNullOrEmpty(errorMsg))
            {
                errorMsg = errorMsg.Replace("<br>", "\\n");
                string jsHtml = "<script language='javascript'>";
                jsHtml += "alert('" + errorMsg + "');";
                jsHtml += "</script>";

                Response.Write(jsHtml);
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
                    this.LabelMsg.Text = "������ѡ�����Ҫ����� Excel �����ļ��������ʽ�Ե����ĸ�ʽΪ׼��";
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

        /// <summary>
        /// ���ݵ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butImport_Click(object sender, EventArgs e)
        {
            // �����ļ�
            string oprateDate = string.Empty;
            if (Session["FileName"] != null)
            {
                m_UpFileName = Session["FileName"].ToString();

                try
                {
                    switch (getRealFun(this.m_FuncCode))
                    {
                        case "020101"://�������Ż����Ǽ���Ϣ��
                            ImportHYDJ("020101", ReadXlsToDs(m_UpFileName)); // ��Ա������Ϣ����
                            DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "���ݵ���", "�û��� " + DateTime.Now.ToString() + " ������Ա������Ϣ");
                            break;
                        case "020102"://��������סԺ����ʵ���Ǽ���Ϣ�� 
                            ImportFMDJ("020102", ReadXlsToDs(m_UpFileName)); // ��Ա������Ϣ����
                            DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "���ݵ���", "�û��� " + DateTime.Now.ToString() + " ������Ա������Ϣ");
                            break;
                        case "020103"://�������������˿ڱ�����Ա�Ǽ���Ϣ��
                            ImportXSBH("020103", ReadXlsToDs(m_UpFileName)); // ��Ա������Ϣ����
                            DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "���ݵ���", "�û��� " + DateTime.Now.ToString() + " ������Ա������Ϣ");
                            break;
                        case "020104"://�������Ű����ס֤��Ա�Ǽ���Ϣ��
                            ImportJZDJ("020104", ReadXlsToDs(m_UpFileName)); // ��Ա������Ϣ����
                            DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "���ݵ���", "�û��� " + DateTime.Now.ToString() + " ������Ա������Ϣ");
                            break;
                        case "020105"://�������Ű����ס֤��Ա�Ǽ���Ϣ��
                            ImportQYDJ("020105", ReadXlsToDs(m_UpFileName)); // ��Ա������Ϣ����
                            DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "���ݵ���", "�û��� " + DateTime.Now.ToString() + " ������Ա������Ϣ");
                            break;
                        case "020106"://���������˿ڻ������ͳ����Ϣ��
                            ImportRKTJ("020106", ReadXlsToDs(m_UpFileName)); // ��Ա������Ϣ����
                            DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "���ݵ���", "�û��� " + DateTime.Now.ToString() + " ������Ա������Ϣ");
                            break;
                        default:

                            break;
                    }
                }
                catch (Exception ex)
                {
                    this.LabelMsg.Text = "����ʧ�ܣ�<br/><br/>" + ex.Message; // " + m_SqlParams + "
                }
            }
            else
            {
                this.LabelMsg.Text = "����ʧ�ܣ�������ʱ�����������롱��ť���ԣ�";
            }
        }

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
        /// <summary>
        /// ����״��
        /// </summary>
        /// <param name="strMarrTypeCN"></param>
        /// <returns></returns>
        private string getRealMarrType(string strMarrTypeCN)
        {
            string strReturn = string.Empty;
            switch (strMarrTypeCN)
            {
                case "δ��":
                    strReturn = "10";
                    break;
                case "�ѻ�":
                    strReturn = "20";
                    break;
                case "����":
                    strReturn = "21";
                    break;
                case "�ٻ�":
                    strReturn = "22";
                    break;
                case "����":
                    strReturn = "23";
                    break;
                case "���":
                    strReturn = "40";
                    break;
                default:
                    strReturn = "90";
                    break;
            }
            return strReturn;
        }
        /// <summary>
        /// ������ϵ
        /// </summary>
        /// <param name="strMarrTypeCN"></param>
        /// <returns></returns>
        private string getRelationType(string strRelationTypeCN)
        {
            string strReturn = string.Empty;
            DataTable dt = DbHelperSQL.Query("SELECT CODE FROM BAS_Relation WHERE NAME='" + strRelationTypeCN + "'").Tables[0];
            if (dt.Rows.Count == 1)
            { strReturn = dt.Rows[0][0].ToString(); }
            else
            { strReturn = "97"; }
            return strReturn;
        }
        /// <summary>
        ///Ǩ��  Ǩ��
        /// </summary>
        /// <param name="strMarrTypeCN"></param>
        /// <returns></returns>
        private string getMoveType(string strMoveTypeCN)
        {
            string strReturn = string.Empty;
            switch (strMoveTypeCN)
            {
                case "Ǩ��":
                    strReturn = "0";
                    break;
                case "Ǩ��":
                    strReturn = "1";
                    break;
                default:
                    strReturn = "0";
                    break;
            }
            return strReturn;
        }

        private string DateStrTrim(string inStr)
        {
            if (!String.IsNullOrEmpty(inStr))
            {
                if (PageValidate.IsDateTime(inStr)) { return DateTime.Parse(inStr.Trim()).ToString("yyyy/MM/dd"); }
                else { return DateTime.Parse("1900/01/01").ToString("yyyy/MM/dd"); }
            }
            else
            {
                return DateTime.Parse("1900/01/01").ToString("yyyy/MM/dd");
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

        #region ���ݵ���
        /// <summary>
        /// �������Ż����Ǽ���Ϣ����
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportHYDJ(string strFunNo, DataSet xlsDs)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null || xlsDs.Tables[0].Columns.Count < 1)
            {
                sHtml.Append("����ʧ�ܣ���ȡ Excel ���ݼ�����<br>");
                sHtml.Append("����취����ȷ������Ҫ�����Excel�ļ����ϵ������ݵı�׼��ʽ��Ȼ�����ԡ�<br>");
                sHtml.Append("�����ϵͳ�Զ����ɵ�Excel�ļ�������Excel�򿪺����Ϊ��Microsoft Office Excel ������(*.xls)����ʽ��Ȼ�����ԡ�<br>");
                sHtml.Append("�������ݵ� Excel��ͷ�ı�׼��ʽΪϵͳ��������Ӧ�ļ��ĸ�ʽ��");
                this.LabelMsg.Text = sHtml.ToString();
                return;
            }
            // �Ѿ����ڵļ�¼
            m_SqlParams = "SELECT [CommID], [NationalID] FROM [Sys_QueueBiz]";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // ��ʼ����
            string NationalID = string.Empty;
            string userName = string.Empty;
            string totalCount = xlsDs.Tables[0].Rows.Count.ToString(); // ����
            int iPassed = 0; // �ɹ���   
            int iUnPass = 0; // ʧ����
            int iHuLue = 0; // ������
            for (int i = 2; i < xlsDs.Tables[0].Rows.Count; i++)
            {
                NationalID = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString());
                userName = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString());
                if (!String.IsNullOrEmpty(StrTrim(xlsDs.Tables[0].Rows[i][1].ToString()))) //��֤�������Ƿ�Ϊ���ּ���ֵ
                {
                    StringBuilder strSql = new StringBuilder();
                    string CommID = CheckHasData(strFunNo, NationalID);
                    if (String.IsNullOrEmpty(CommID))
                    {
                        // INSERT ��¼
                        strSql.Append("INSERT INTO Sys_QueueBiz(");
                        strSql.Append("FunNo,ResponseUserID, BabyDateSH, [NationalName], [NationalID],  NationalBirthDay, [MarraigeType],CurrentAddress,NationalWorkUint,WifeName,WifeID, WifeBirthDay,WifeMarraigeType,WifeAddress,WifeWorkUint)");
                        strSql.Append(" VALUES(");
                        strSql.Append("'" + strFunNo + "',"+this.m_UserID+",@BabyDateSH,@NationalName,@NationalID,@NationalBirthDay,@MarraigeType,@CurrentAddress,@NationalWorkUint,@WifeName,@WifeID,@WifeBirthDay,@WifeMarraigeType,@WifeAddress,@WifeWorkUint)");
                        strSql.Append(";select @@IDENTITY");
                    }
                    else
                    {
                        // update ��¼
                        strSql.Append("UPDATE Sys_QueueBiz SET ");
                        strSql.Append("BabyDateSH=@BabyDateSH, [NationalName]=@NationalName, [NationalID]=@NationalID, ");
                        strSql.Append("NationalBirthDay=@NationalBirthDay, [MarraigeType]=@MarraigeType,CurrentAddress=@CurrentAddress,");
                        strSql.Append("NationalWorkUint=@NationalWorkUint,WifeName=@WifeName,WifeID=@WifeID, WifeBirthDay=@WifeBirthDay,");
                        strSql.Append("WifeMarraigeType=@WifeMarraigeType,WifeAddress=@WifeAddress,WifeWorkUint=@WifeWorkUint");
                        strSql.Append(" WHERE CommID=" + CommID);
                    }

                    SqlParameter[] parameters = {
					new SqlParameter("@BabyDateSH", SqlDbType.SmallDateTime,4),
					new SqlParameter("@NationalName", SqlDbType.VarChar,20),
					new SqlParameter("@NationalID", SqlDbType.VarChar,20),
                    new SqlParameter("@NationalBirthDay", SqlDbType.SmallDateTime,4),
					new SqlParameter("@MarraigeType", SqlDbType.Char,3),
					new SqlParameter("@CurrentAddress", SqlDbType.VarChar,100),
					new SqlParameter("@NationalWorkUint", SqlDbType.VarChar,80),
					new SqlParameter("@WifeName",  SqlDbType.VarChar,20),
					new SqlParameter("@WifeID",  SqlDbType.VarChar,18),
					new SqlParameter("@WifeBirthDay",  SqlDbType.SmallDateTime,4),
					new SqlParameter("@WifeMarraigeType",  SqlDbType.Char,3),
                  	new SqlParameter("@WifeAddress", SqlDbType.VarChar,100), // 
                    new SqlParameter("@WifeWorkUint", SqlDbType.VarChar,80)};
                    parameters[0].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][0].ToString())); // �Ǽ�����
                    parameters[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString()); // �з�����
                    parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString()); // ���֤��
                    parameters[3].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][3].ToString())); // ��������
                    parameters[4].Value = getRealMarrType(StrTrim(xlsDs.Tables[0].Rows[i][4].ToString())); // ����״��
                    parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString()); // ��ͥ��ַ
                    parameters[6].Value = StrTrim(xlsDs.Tables[0].Rows[i][6].ToString()); // ������λ
                    parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); // Ů������
                    parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); // ���֤��
                    parameters[9].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][9].ToString())); // ��������
                    parameters[10].Value = getRealMarrType(StrTrim(xlsDs.Tables[0].Rows[i][10].ToString())); // ����״��
                    parameters[11].Value = StrTrim(xlsDs.Tables[0].Rows[i][11].ToString()); // ��ͥ��ַ
                    parameters[12].Value = StrTrim(xlsDs.Tables[0].Rows[i][12].ToString()); // ������λ
                    // ִ�в���
                    if (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0)
                    {
                        iPassed++;
                    }
                    else
                    {
                        iUnPass++;
                    }
                }
                else
                {

                    iHuLue++;
                    toop.Append(userName + ",");


                }
            }
            if (toop.ToString() != string.Empty)
            {
                ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + toop.ToString() + "'+'Ա������Ϣ�����Ե���');</script>");
            }
            m_Dt.Dispose();
            xlsDs.Dispose();
            sHtml.Append("<br><br>�����ļ�����" + totalCount + "��Ϣ���ɹ�������" + iPassed.ToString() + "����Ϣ��ʧ����" + iUnPass.ToString() + "����Ϣ��ֱ�Ӻ��Ե���Ϣ " + iHuLue + "����");
            this.LabelMsg.Text = sHtml.ToString();
        }

        /// <summary>
        /// ��������סԺ����ʵ���Ǽ���Ϣ����
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportFMDJ(string strFunNo, DataSet xlsDs)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null || xlsDs.Tables[0].Columns.Count < 1)
            {
                sHtml.Append("����ʧ�ܣ���ȡ Excel ���ݼ�����<br>");
                sHtml.Append("����취����ȷ������Ҫ�����Excel�ļ����ϵ������ݵı�׼��ʽ��Ȼ�����ԡ�<br>");
                sHtml.Append("�����ϵͳ�Զ����ɵ�Excel�ļ�������Excel�򿪺����Ϊ��Microsoft Office Excel ������(*.xls)����ʽ��Ȼ�����ԡ�<br>");
                sHtml.Append("�������ݵ� Excel��ͷ�ı�׼��ʽΪϵͳ��������Ӧ�ļ��ĸ�ʽ��");
                this.LabelMsg.Text = sHtml.ToString();
                return;
            }
            // �Ѿ����ڵļ�¼
            m_SqlParams = "SELECT [CommID], [NationalID] FROM [Sys_QueueBiz]";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // ��ʼ����
            string NationalID = string.Empty;
            string userName = string.Empty;
            string totalCount = xlsDs.Tables[0].Rows.Count.ToString(); // ����
            int iPassed = 0; // �ɹ���   
            int iUnPass = 0; // ʧ����
            int iHuLue = 0; // ������
            for (int i = 2; i < xlsDs.Tables[0].Rows.Count; i++)
            {
                NationalID = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString());
                userName = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString());
                if (!String.IsNullOrEmpty(NationalID)) //
                {
                    StringBuilder strSql = new StringBuilder();
                    string CommID = CheckHasData(strFunNo, NationalID);
                    if (String.IsNullOrEmpty(CommID))
                    {
                        // INSERT ��¼
                        strSql.Append("INSERT INTO Sys_QueueBiz(");
                        strSql.Append("FunNo, ResponseUserID,[NationalName], NationalWorkUint, CurrentAddress,[NationalID],WifeName,WifeWorkUint,WifeAddress,WifeID,BabyIndex,BabySex, [BabyBirthDay],BabyName,BabyBirthID, [BabyDate] )");
                        strSql.Append(" VALUES(");
                        strSql.Append("'" + strFunNo + "',"+this.m_UserID+",@NationalName, @NationalWorkUint, @CurrentAddress,@NationalID,@WifeName,@WifeWorkUint,@WifeAddress,@WifeID,@BabyIndex,@BabySex, @BabyBirthDay,@BabyName,@BabyBirthID, @BabyDate )");
                        strSql.Append(";select @@IDENTITY");
                    }
                    else
                    {
                        // update ��¼
                        strSql.Append("UPDATE Sys_QueueBiz SET ");
                        strSql.Append("NationalName=@NationalName, NationalWorkUint=@NationalWorkUint, CurrentAddress=@CurrentAddress,NationalID=@NationalID,WifeName=@WifeName,WifeWorkUint=@WifeWorkUint,");
                        strSql.Append("WifeAddress=@WifeAddress,WifeID=@WifeID,BabyIndex=@BabyIndex,BabySex=@BabySex, BabyBirthDay=@BabyBirthDay,BabyName=@BabyName,BabyBirthID=@BabyBirthID, BabyDate=@BabyDate ");
                        strSql.Append(" WHERE CommID=" + CommID);
                    }

                    SqlParameter[] parameters = {
					new SqlParameter("@NationalName", SqlDbType.VarChar,20),
					new SqlParameter("@NationalWorkUint", SqlDbType.VarChar,80),
					new SqlParameter("@CurrentAddress", SqlDbType.VarChar,100),
                    new SqlParameter("@NationalID", SqlDbType.VarChar,20),
					new SqlParameter("@WifeName", SqlDbType.VarChar,20),
					new SqlParameter("@WifeWorkUint",  SqlDbType.VarChar,80),
					new SqlParameter("@WifeAddress",  SqlDbType.VarChar,100),
					new SqlParameter("@WifeID",  SqlDbType.VarChar,18),
                  	new SqlParameter("@BabyIndex", SqlDbType.VarChar,10), 
                    new SqlParameter("@BabySex", SqlDbType.VarChar,10), 
                    new SqlParameter("@BabyBirthDay", SqlDbType.SmallDateTime,4), 
                    new SqlParameter("@BabyName", SqlDbType.VarChar,20), 
                    new SqlParameter("@BabyBirthID", SqlDbType.VarChar,18), 
                    new SqlParameter("@BabyDate", SqlDbType.SmallDateTime,4)};
                    parameters[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString()); // �ɷ�����
                    parameters[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString()); // ������λ
                    parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString()); // ��ͥסַ
                    parameters[3].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString()); // ���֤��
                    parameters[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()); // ��������
                    parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString()); // ������λ
                    parameters[6].Value = StrTrim(xlsDs.Tables[0].Rows[i][6].ToString()); // ��ͥסַ
                    parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); // ���֤��
                    parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); // ������ ̥��
                    parameters[9].Value = StrTrim(xlsDs.Tables[0].Rows[i][9].ToString()); // �Ա�
                    parameters[10].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][10].ToString())); // ��������
                    parameters[11].Value = StrTrim(xlsDs.Tables[0].Rows[i][11].ToString()); // ����
                    parameters[12].Value = StrTrim(xlsDs.Tables[0].Rows[i][12].ToString()); // ֤��
                    parameters[13].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][13].ToString())); // ��֤����
                    // ִ�в���
                    if (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0)
                    {
                        iPassed++;
                    }
                    else
                    {
                        iUnPass++;
                    }
                }
                else
                {

                    iHuLue++;
                    toop.Append(userName + ",");


                }
            }
            if (toop.ToString() != string.Empty)
            {
                ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + toop.ToString() + "'+'Ա������Ϣ�����Ե���');</script>");
            }
            m_Dt.Dispose();
            xlsDs.Dispose();
            sHtml.Append("<br><br>�����ļ�����" + totalCount + "��Ϣ���ɹ�������" + iPassed.ToString() + "����Ϣ��ʧ����" + iUnPass.ToString() + "����Ϣ��ֱ�Ӻ��Ե���Ϣ " + iHuLue + "����");
            this.LabelMsg.Text = sHtml.ToString();
        }


        /// <summary>
        /// �������������˿ڱ�����Ա�Ǽ���Ϣ����
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportXSBH(string strFunNo, DataSet xlsDs)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null || xlsDs.Tables[0].Columns.Count < 1)
            {
                sHtml.Append("����ʧ�ܣ���ȡ Excel ���ݼ�����<br>");
                sHtml.Append("����취����ȷ������Ҫ�����Excel�ļ����ϵ������ݵı�׼��ʽ��Ȼ�����ԡ�<br>");
                sHtml.Append("�����ϵͳ�Զ����ɵ�Excel�ļ�������Excel�򿪺����Ϊ��Microsoft Office Excel ������(*.xls)����ʽ��Ȼ�����ԡ�<br>");
                sHtml.Append("�������ݵ� Excel��ͷ�ı�׼��ʽΪϵͳ��������Ӧ�ļ��ĸ�ʽ��");
                this.LabelMsg.Text = sHtml.ToString();
                return;
            }
            // �Ѿ����ڵļ�¼
            m_SqlParams = "SELECT [CommID], [NationalID] FROM [Sys_QueueBiz]";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // ��ʼ����
            string NationalID = string.Empty;
            string userName = string.Empty;
            string totalCount = xlsDs.Tables[0].Rows.Count.ToString(); // ����
            int iPassed = 0; // �ɹ���   
            int iUnPass = 0; // ʧ����
            int iHuLue = 0; // ������
            for (int i = 3; i < xlsDs.Tables[0].Rows.Count; i++)
            {
                NationalID = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString());
                userName = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString());
                if (!String.IsNullOrEmpty(StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()))) //��֤�������Ƿ�Ϊ���ּ���ֵ
                {
                    StringBuilder strSql = new StringBuilder();
                    string CommID = CheckHasData(strFunNo, NationalID);
                    if (String.IsNullOrEmpty(CommID))
                    {
                        // INSERT ��¼
                        strSql.Append("INSERT INTO Sys_QueueBiz(");
                        strSql.Append("FunNo,ResponseUserID, BabyDateSH,NationalName, NationalWorkUint, CurrentAddress,NationalID,WifeName,WifeWorkUint,WifeAddress,WifeID,BabyName,BabySex,BabyAddress,BabyID,BabyBirthDay)");
                        strSql.Append(" VALUES(");
                        strSql.Append("'" + strFunNo + "',"+this.m_UserID+",@BabyDateSH,@NationalName, @NationalWorkUint, @CurrentAddress,@NationalID,@WifeName,@WifeWorkUint,@WifeAddress,@WifeID,@BabyName,@BabySex,@BabyAddress,@BabyID,@BabyBirthDay)");
                        strSql.Append(";select @@IDENTITY");
                    }
                    else
                    {
                        // update ��¼
                        strSql.Append("UPDATE Sys_QueueBiz SET ");
                        strSql.Append("BabyDateSH=@BabyDateSH,NationalName=@NationalName, NationalWorkUint=@NationalWorkUint, CurrentAddress=@CurrentAddress,NationalID=@NationalID,WifeName=@WifeName, ");
                        strSql.Append("WifeWorkUint=@WifeWorkUint,WifeAddress=@WifeAddress,WifeID=@WifeID,BabyName=@BabyName,BabySex=@BabySex,BabyAddress=@BabyAddress,BabyID=@BabyID,BabyBirthDay=@BabyBirthDay");
                        strSql.Append(" WHERE CommID=" + CommID);
                    }

                    SqlParameter[] parameters = {
					new SqlParameter("@BabyDateSH", SqlDbType.SmallDateTime,4),
					new SqlParameter("@NationalName", SqlDbType.VarChar,20),
					new SqlParameter("@NationalWorkUint", SqlDbType.VarChar,80),
                    new SqlParameter("@CurrentAddress", SqlDbType.VarChar,100),
					new SqlParameter("@NationalID", SqlDbType.VarChar,20),
					new SqlParameter("@WifeName",  SqlDbType.VarChar,20),
					new SqlParameter("@WifeWorkUint",  SqlDbType.VarChar,80),
					new SqlParameter("@WifeAddress",  SqlDbType.VarChar,100),
                  	new SqlParameter("@WifeID", SqlDbType.VarChar,18), 
                    new SqlParameter("@BabyName", SqlDbType.VarChar,20), 
                    new SqlParameter("@BabySex", SqlDbType.VarChar,10), 
                    new SqlParameter("@BabyAddress", SqlDbType.VarChar,100), 
                    new SqlParameter("@BabyID", SqlDbType.VarChar,18), 
                    new SqlParameter("@BabyBirthDay", SqlDbType.SmallDateTime,4)};
                    parameters[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString()); // �ɷ�����
                    parameters[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString()); // ������λ
                    parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString()); // ��ͥסַ
                    parameters[3].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString()); // ���֤��
                    parameters[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()); // ��������
                    parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString()); // ������λ
                    parameters[6].Value = StrTrim(xlsDs.Tables[0].Rows[i][6].ToString()); // ��ͥסַ
                    parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); // ���֤��
                    parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); // ������ ̥��
                    parameters[9].Value = StrTrim(xlsDs.Tables[0].Rows[i][9].ToString()); // �Ա�
                    parameters[10].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][10].ToString())); // ��������
                    parameters[11].Value = StrTrim(xlsDs.Tables[0].Rows[i][11].ToString()); // ����
                    parameters[12].Value = StrTrim(xlsDs.Tables[0].Rows[i][12].ToString()); // ֤��
                    parameters[13].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][13].ToString())); // ��֤����
                    // ִ�в���
                    if (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0)
                    {
                        iPassed++;
                    }
                    else
                    {
                        iUnPass++;
                    }
                }
                else
                {

                    iHuLue++;
                    toop.Append(userName + ",");


                }
            }
            if (toop.ToString() != string.Empty)
            {
                ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + toop.ToString() + "'+'Ա������Ϣ�����Ե���');</script>");
            }
            m_Dt.Dispose();
            xlsDs.Dispose();
            sHtml.Append("<br><br>�����ļ�����" + totalCount + "��Ϣ���ɹ�������" + iPassed.ToString() + "����Ϣ��ʧ����" + iUnPass.ToString() + "����Ϣ��ֱ�Ӻ��Ե���Ϣ " + iHuLue + "����");
            this.LabelMsg.Text = sHtml.ToString();
        }

        /// <summary>
        /// �������Ű����ס֤��Ա�Ǽ���Ϣ����
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportJZDJ(string strFunNo, DataSet xlsDs)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null || xlsDs.Tables[0].Columns.Count < 1)
            {
                sHtml.Append("����ʧ�ܣ���ȡ Excel ���ݼ�����<br>");
                sHtml.Append("����취����ȷ������Ҫ�����Excel�ļ����ϵ������ݵı�׼��ʽ��Ȼ�����ԡ�<br>");
                sHtml.Append("�����ϵͳ�Զ����ɵ�Excel�ļ�������Excel�򿪺����Ϊ��Microsoft Office Excel ������(*.xls)����ʽ��Ȼ�����ԡ�<br>");
                sHtml.Append("�������ݵ� Excel��ͷ�ı�׼��ʽΪϵͳ��������Ӧ�ļ��ĸ�ʽ��");
                this.LabelMsg.Text = sHtml.ToString();
                return;
            }
            // �Ѿ����ڵļ�¼
            m_SqlParams = "SELECT [CommID], [NationalID] FROM [Sys_QueueBiz]";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // ��ʼ����
            string NationalID = string.Empty;
            string userName = string.Empty;
            string totalCount = xlsDs.Tables[0].Rows.Count.ToString(); // ����
            int iPassed = 0; // �ɹ���   
            int iUnPass = 0; // ʧ����
            int iHuLue = 0; // ������
            for (int i = 3; i < xlsDs.Tables[0].Rows.Count; i++)
            {
                NationalID = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString());
                userName = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString());
                if (!String.IsNullOrEmpty(StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()))) //��֤�������Ƿ�Ϊ���ּ���ֵ
                {
                    StringBuilder strSql = new StringBuilder();
                    string CommID = CheckHasData(strFunNo, NationalID);
                    if (String.IsNullOrEmpty(CommID))
                    {
                        // INSERT ��¼
                        strSql.Append("INSERT INTO Sys_QueueBiz(");
                        strSql.Append("FunNo, ResponseUserID,NationalName,NationalSex,NationalID, NationalBirthDay, NationalWorkUint, CurrentAddress,MarraigeType,NationalBY,YZName,YZTel,RelationType,WifeName,WifeSex,WifeID,WifeBirthDay,WifeBY,BoysNum,GirlsNum,MarraigeID)");
                        strSql.Append(" VALUES(");
                        strSql.Append("'" + strFunNo + "',"+this.m_UserID+",@NationalName,@NationalSex,@NationalID, @NationalBirthDay, @NationalWorkUint, @CurrentAddress,@MarraigeType,@NationalBY,@YZName,@YZTel,@RelationType,@WifeName,@WifeSex,@WifeID,@WifeBirthDay,@WifeBY,@BoysNum,@GirlsNum,@MarraigeID)");
                        strSql.Append(";select @@IDENTITY");
                    }
                    else
                    {
                        // update ��¼
                        strSql.Append("UPDATE Sys_QueueBiz SET ");
                        strSql.Append("NationalName=@NationalName,NationalSex=@NationalSex,NationalID=@NationalID, NationalBirthDay=@NationalBirthDay,  ");
                        strSql.Append("NationalWorkUint=@NationalWorkUint, CurrentAddress=@CurrentAddress,MarraigeType=@MarraigeType,");
                        strSql.Append("NationalBY=@NationalBY,YZName=@YZName,YZTel=@YZTel,RelationType=@RelationType,WifeName=@WifeName,");
                        strSql.Append("WifeSex=@WifeSex,WifeID=@WifeID,WifeBirthDay=@WifeBirthDay,WifeBY=@WifeBY,BoysNum=@BoysNum,GirlsNum=@GirlsNum,MarraigeID=@MarraigeID");
                        strSql.Append(" WHERE CommID=" + CommID);
                    }

                    SqlParameter[] parameters = {
					new SqlParameter("@NationalName", SqlDbType.VarChar,20),
					new SqlParameter("@NationalSex", SqlDbType.VarChar,10),
					new SqlParameter("@NationalID", SqlDbType.VarChar,20),
                    new SqlParameter("@NationalBirthDay", SqlDbType.SmallDateTime,4),
					new SqlParameter("@NationalWorkUint", SqlDbType.VarChar,80),
					new SqlParameter("@CurrentAddress",  SqlDbType.VarChar,100),
					new SqlParameter("@MarraigeType",  SqlDbType.Char,3),
					new SqlParameter("@NationalBY",  SqlDbType.VarChar,50),
                  	new SqlParameter("@YZName", SqlDbType.VarChar,20), 
                    new SqlParameter("@YZTel", SqlDbType.VarChar,50), 
                    new SqlParameter("@RelationType", SqlDbType.Char,3), 
                    new SqlParameter("@WifeName", SqlDbType.VarChar,20), 
                    new SqlParameter("@WifeSex", SqlDbType.VarChar,10), 
                    new SqlParameter("@WifeID", SqlDbType.VarChar,18), 
                    new SqlParameter("@WifeBirthDay", SqlDbType.SmallDateTime,4), 
                    new SqlParameter("@WifeBY", SqlDbType.VarChar,50), 
                    new SqlParameter("@BoysNum", SqlDbType.Int,4), 
                    new SqlParameter("@GirlsNum", SqlDbType.Int,4), 
                    new SqlParameter("@MarraigeID", SqlDbType.VarChar,30)};
                    parameters[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString()); // �Ǽ�������
                    parameters[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString()); // �Ա�
                    parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString()); // ���֤��
                    parameters[3].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][3].ToString())); // ��������
                    parameters[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()); // ������ַ
                    parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString()); // �־�ס��ַ
                    parameters[6].Value = getRealMarrType(StrTrim(xlsDs.Tables[0].Rows[i][6].ToString())); // ����״��
                    parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); // ��ȡ���д�ʩ���
                    parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); // ����ҵ������
                    parameters[9].Value = StrTrim(xlsDs.Tables[0].Rows[i][9].ToString()); // ����ҵ����ϵ�绰
                    parameters[10].Value = getRelationType(StrTrim(xlsDs.Tables[0].Rows[i][10].ToString())); // �뱾�˹�ϵ
                    parameters[11].Value = StrTrim(xlsDs.Tables[0].Rows[i][11].ToString()); // ����
                    parameters[12].Value = StrTrim(xlsDs.Tables[0].Rows[i][12].ToString()); // �Ա�
                    parameters[13].Value = StrTrim(xlsDs.Tables[0].Rows[i][13].ToString()); // ���֤����
                    parameters[14].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][14].ToString())); // ��������
                    parameters[15].Value = StrTrim(xlsDs.Tables[0].Rows[i][15].ToString()); // ��ȡ���д�ʩ���
                    parameters[16].Value = NumberStrTrim(StrTrim(xlsDs.Tables[0].Rows[i][16].ToString())); // ���м�ͥ��Ů��  ��
                    parameters[17].Value = NumberStrTrim(StrTrim(xlsDs.Tables[0].Rows[i][17].ToString())); // ���м�ͥ��Ů��  Ů
                    parameters[18].Value = StrTrim(xlsDs.Tables[0].Rows[i][18].ToString()); // ����֤�����

                    // ִ�в���
                    if (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0)
                    {
                        iPassed++;
                    }
                    else
                    {
                        iUnPass++;
                    }
                }
                else
                {

                    iHuLue++;
                    toop.Append(userName + ",");


                }
            }
            if (toop.ToString() != string.Empty)
            {
                ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + toop.ToString() + "'+'Ա������Ϣ�����Ե���');</script>");
            }
            m_Dt.Dispose();
            xlsDs.Dispose();
            sHtml.Append("<br><br>�����ļ�����" + totalCount + "��Ϣ���ɹ�������" + iPassed.ToString() + "����Ϣ��ʧ����" + iUnPass.ToString() + "����Ϣ��ֱ�Ӻ��Ե���Ϣ " + iHuLue + "����");
            this.LabelMsg.Text = sHtml.ToString();
        }


        /// <summary>
        /// �������Ű����ס֤��Ա�Ǽ���Ϣ����
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportQYDJ(string strFunNo, DataSet xlsDs)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null || xlsDs.Tables[0].Columns.Count < 1)
            {
                sHtml.Append("����ʧ�ܣ���ȡ Excel ���ݼ�����<br>");
                sHtml.Append("����취����ȷ������Ҫ�����Excel�ļ����ϵ������ݵı�׼��ʽ��Ȼ�����ԡ�<br>");
                sHtml.Append("�����ϵͳ�Զ����ɵ�Excel�ļ�������Excel�򿪺����Ϊ��Microsoft Office Excel ������(*.xls)����ʽ��Ȼ�����ԡ�<br>");
                sHtml.Append("�������ݵ� Excel��ͷ�ı�׼��ʽΪϵͳ��������Ӧ�ļ��ĸ�ʽ��");
                this.LabelMsg.Text = sHtml.ToString();
                return;
            }
            // �Ѿ����ڵļ�¼
            m_SqlParams = "SELECT [CommID], [MoveInCardID] FROM [Sys_Move]";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // ��ʼ����
            string MoveInCardID, MoveOutCardID = string.Empty;
            string userName = string.Empty;
            string totalCount = xlsDs.Tables[0].Rows.Count.ToString(); // ����
            int iPassed = 0; // �ɹ���   
            int iUnPass = 0; // ʧ����
            int iHuLue = 0; // ������
            for (int i = 2; i < xlsDs.Tables[0].Rows.Count; i++)
            {
                MoveInCardID = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString());
                MoveOutCardID = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString());

                userName = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString());
                if (!String.IsNullOrEmpty(StrTrim(xlsDs.Tables[0].Rows[i][1].ToString())) || !String.IsNullOrEmpty(StrTrim(xlsDs.Tables[0].Rows[i][7].ToString())))
                {
                    string CommID = CheckHasData_M(MoveInCardID, MoveOutCardID);
                    StringBuilder strSql = new StringBuilder();
                    if (String.IsNullOrEmpty(CommID))
                    {
                        // INSERT ��¼
                        strSql.Append("INSERT INTO Sys_Move(");
                        strSql.Append("[MoveInDate], [MoveInName], [MoveInSex], [MoveInCardID], [MoveInAddress], [MoveInAddressTo], [MoveOutDate], [MoveOutName], [MoveOutSex],[MoveOutCardID], [MoveOutAddress], [MoveOutAddressTo])");
                        strSql.Append(" VALUES(");
                        strSql.Append("@MoveInDate, @MoveInName, @MoveInSex, @MoveInCardID, @MoveInAddress, @MoveInAddressTo, @MoveOutDate, @MoveOutName, @MoveOutSex,@MoveOutCardID, @MoveOutAddress, @MoveOutAddressTo)");
                        strSql.Append(";select @@IDENTITY");
                    }
                    else
                    {
                        // update ��¼
                        strSql.Append("UPDATE Sys_Move SET ");
                        strSql.Append("MoveInDate=@MoveInDate, MoveInName=@MoveInName, MoveInSex=@MoveInSex, MoveInCardID=@MoveInCardID, ");
                        strSql.Append("MoveInAddress=@MoveInAddress, MoveInAddressTo=@MoveInAddressTo, MoveOutDate=@MoveOutDate,  ");
                        strSql.Append("MoveOutName=@MoveOutName, MoveOutSex=@MoveOutSex,MoveOutCardID=@MoveOutCardID, MoveOutAddress=@MoveOutAddress, MoveOutAddressTo=@MoveOutAddressTo");
                        strSql.Append(" WHERE CommID=" + CommID);
                    }

                    SqlParameter[] parameters = {
					new SqlParameter("@MoveInDate", SqlDbType.VarChar,10),
					new SqlParameter("@MoveInName", SqlDbType.VarChar,20),
					new SqlParameter("@MoveInSex", SqlDbType.VarChar,10),
                    new SqlParameter("@MoveInCardID", SqlDbType.VarChar,20),
					new SqlParameter("@MoveInAddress", SqlDbType.VarChar,80),
					new SqlParameter("@MoveInAddressTo",  SqlDbType.VarChar,80),
					new SqlParameter("@MoveOutDate", SqlDbType.VarChar,10),
					new SqlParameter("@MoveOutName", SqlDbType.VarChar,20),
					new SqlParameter("@MoveOutSex", SqlDbType.VarChar,10),
                    new SqlParameter("@MoveOutCardID", SqlDbType.VarChar,20),
					new SqlParameter("@MoveOutAddress", SqlDbType.VarChar,80),
					new SqlParameter("@MoveOutAddressTo",  SqlDbType.VarChar,80)};
                    parameters[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString()); // Ǩ�� ����
                    parameters[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString()); // ����
                    parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString()); // �Ա�
                    parameters[3].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString()); // ���֤��
                    parameters[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()); // Ǩ����ַ
                    parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString()); // Ǩ���ַ
                    parameters[6].Value = StrTrim(xlsDs.Tables[0].Rows[i][6].ToString()); // Ǩ�� ����
                    parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); // ����
                    parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); // �Ա�
                    parameters[9].Value = StrTrim(xlsDs.Tables[0].Rows[i][9].ToString()); // ���֤��
                    parameters[10].Value = StrTrim(xlsDs.Tables[0].Rows[i][10].ToString()); // Ǩ����ַ
                    parameters[11].Value = StrTrim(xlsDs.Tables[0].Rows[i][11].ToString()); // Ǩ���ַ

                    // ִ�в���
                    if (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0)
                    {
                        iPassed++;
                    }
                    else
                    {
                        iUnPass++;
                    }
                }
                else
                {

                    iHuLue++;
                    toop.Append(userName + ",");


                }
            }
            if (toop.ToString() != string.Empty)
            {
                ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + toop.ToString() + "'+'Ա������Ϣ�����Ե���');</script>");
            }
            m_Dt.Dispose();
            xlsDs.Dispose();
            sHtml.Append("<br><br>�����ļ�����" + totalCount + "��Ϣ���ɹ�������" + iPassed.ToString() + "����Ϣ��ʧ����" + iUnPass.ToString() + "����Ϣ��ֱ�Ӻ��Ե���Ϣ " + iHuLue + "����");
            this.LabelMsg.Text = sHtml.ToString();
        }

        /// <summary>
        /// ���������˿ڻ������ͳ����Ϣ��
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportRKTJ(string strFunNo, DataSet xlsDs)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null || xlsDs.Tables[0].Columns.Count < 1)
            {
                sHtml.Append("����ʧ�ܣ���ȡ Excel ���ݼ�����<br>");
                sHtml.Append("����취����ȷ������Ҫ�����Excel�ļ����ϵ������ݵı�׼��ʽ��Ȼ�����ԡ�<br>");
                sHtml.Append("�����ϵͳ�Զ����ɵ�Excel�ļ�������Excel�򿪺����Ϊ��Microsoft Office Excel ������(*.xls)����ʽ��Ȼ�����ԡ�<br>");
                sHtml.Append("�������ݵ� Excel��ͷ�ı�׼��ʽΪϵͳ��������Ӧ�ļ��ĸ�ʽ��");
                this.LabelMsg.Text = sHtml.ToString();
                return;
            }
            // �Ѿ����ڵļ�¼
            m_SqlParams = "SELECT [CommID], [UintName] FROM [Sys_PeopleStat]";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // ��ʼ����
            string CommID = string.Empty;
            string UintName = string.Empty;
            string totalCount = xlsDs.Tables[0].Rows.Count.ToString(); // ����
            int iPassed = 0; // �ɹ���   
            int iUnPass = 0; // ʧ����
            int iHuLue = 0; // ������
            for (int i = 3; i < xlsDs.Tables[0].Rows.Count; i++)
            {
                UintName = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString());
                CommID = CheckHasData_P(UintName);
                if (!String.IsNullOrEmpty(StrTrim(xlsDs.Tables[0].Rows[i][0].ToString())) || !String.IsNullOrEmpty(StrTrim(xlsDs.Tables[0].Rows[i][2].ToString()))) //��֤�����ֶ��Ƿ�Ϊ��
                {
                    StringBuilder strSql = new StringBuilder();
                    if (String.IsNullOrEmpty(CommID))
                    {
                        // INSERT ��¼
                        strSql.Append("INSERT INTO Sys_PeopleStat(");
                        strSql.Append("UintName,CZRenKou,YLFunNv,YHFunNv,BYRenShu,CSRenKou,SWRenKou,LCRenKou,LRRenKou,Remarks)");
                        strSql.Append(" VALUES(");
                        strSql.Append("@UintName,@CZRenKou,@YLFunNv,@YHFunNv,@BYRenShu,@CSRenKou,@SWRenKou,@LCRenKou,@LRRenKou,@Remarks)");
                        strSql.Append(";select @@IDENTITY");
                    }
                    else
                    {
                        // update ��¼
                        strSql.Append("UPDATE Sys_PeopleStat SET ");
                        strSql.Append("UintName=@UintName,CZRenKou=@CZRenKou,YLFunNv=@YLFunNv,YHFunNv=@YHFunNv,BYRenShu=@BYRenShu, ");
                        strSql.Append("CSRenKou=@CSRenKou,SWRenKou=@SWRenKou,LCRenKou=@LCRenKou,LRRenKou=@LRRenKou,Remarks=@Remarks");
                        strSql.Append(" WHERE CommID=" + CommID);
                    }

                    SqlParameter[] parameters = {
					new SqlParameter("@UintName", SqlDbType.VarChar,80),
					new SqlParameter("@CZRenKou", SqlDbType.VarChar,20),
					new SqlParameter("@YLFunNv", SqlDbType.VarChar,20),
                    new SqlParameter("@YHFunNv", SqlDbType.VarChar,20),
					new SqlParameter("@BYRenShu", SqlDbType.VarChar,20),
					new SqlParameter("@CSRenKou",  SqlDbType.VarChar,20),
					new SqlParameter("@SWRenKou",  SqlDbType.VarChar,20),
					new SqlParameter("@LCRenKou",  SqlDbType.VarChar,20),
					new SqlParameter("@LRRenKou",  SqlDbType.VarChar,20),
					new SqlParameter("@Remarks",  SqlDbType.VarChar,800)};
                    parameters[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString()); // ��λ����
                    parameters[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString()); // ��ĩ��ס�˿�
                    parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString()); // ��ĩ���举Ů����
                    parameters[3].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString()); // �������举Ů����
                    parameters[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()); // ��ȡ���ֱ��д�ʩ����
                    parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString()); // ��������
                    parameters[6].Value = StrTrim(xlsDs.Tables[0].Rows[i][6].ToString()); // ��������
                    parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); // �����˿�
                    parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); // �����˿�
                    parameters[9].Value = StrTrim(xlsDs.Tables[0].Rows[i][9].ToString()); // ��ע

                    // ִ�в���
                    if (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0)
                    {
                        iPassed++;
                    }
                    else
                    {
                        iUnPass++;
                    }
                }
                else
                {

                    iHuLue++;
                    toop.Append(UintName + ",");


                }
            }
            if (toop.ToString() != string.Empty)
            {
                ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + toop.ToString() + "'+'Ա������Ϣ�����Ե���');</script>");
            }
            m_Dt.Dispose();
            xlsDs.Dispose();
            sHtml.Append("<br><br>�����ļ�����" + totalCount + "��Ϣ���ɹ�������" + iPassed.ToString() + "����Ϣ��ʧ����" + iUnPass.ToString() + "����Ϣ��ֱ�Ӻ��Ե���Ϣ " + iHuLue + "����");
            this.LabelMsg.Text = sHtml.ToString();
        }
        #endregion

        /// <summary>
        /// ���Ǩ�ƿ����Ƿ���ڸõǼ�����Ϣ
        /// </summary>
        /// <param name="FunNo"></param>
        /// <param name="NationalID"></param>
        private string CheckHasData_M(string MoveInCardID, string MoveOutCardID)
        {
            string returnVa = string.Empty;
            string sqlParams = "SELECT CommID FROM Sys_Move WHERE MoveInCardID='" + MoveInCardID + "' OR MoveOutCardID='" + MoveOutCardID + "'";
            try
            {
                DataTable dt0 = DbHelperSQL.Query(sqlParams).Tables[0];
                if (dt0.Rows.Count > 0) { returnVa = dt0.Rows[0]["CommID"].ToString(); }
                else { returnVa = ""; }
            }
            catch { returnVa = ""; }
            return returnVa;
        }
        /// <summary>
        /// ����˿�ͳ�ƿ����Ƿ���ڸõǼ�����Ϣ
        /// </summary>
        /// <param name="FunNo"></param>
        /// <param name="NationalID"></param>
        private string CheckHasData_P(string UintName)
        {
            string returnVa = string.Empty;
            string sqlParams = "SELECT CommID FROM Sys_PeopleStat WHERE UintName='" + UintName + "'";
            try
            {
                DataTable dt0 = DbHelperSQL.Query(sqlParams).Tables[0];
                if (dt0.Rows.Count > 0) { returnVa = dt0.Rows[0]["CommID"].ToString(); }
                else { returnVa = ""; }
            }
            catch { returnVa = ""; }
            return returnVa;
        }
        /// <summary>
        /// �������Ƿ���ڸõǼ�����Ϣ
        /// </summary>
        /// <param name="FunNo"></param>
        /// <param name="NationalID"></param>
        private string CheckHasData(string FunNo, string NationalID)
        {
            string returnVa = string.Empty;
            string sqlParams = "SELECT CommID FROM Sys_QueueBiz WHERE FunNo='" + FunNo + "' AND NationalID='" + NationalID + "'";
            try
            {
                DataTable dt0 = DbHelperSQL.Query(sqlParams).Tables[0];
                if (dt0.Rows.Count == 1) { returnVa = dt0.Rows[0]["CommID"].ToString(); }
                else { returnVa = ""; }
            }
            catch { returnVa = ""; }
            return returnVa;
        }
        /// <summary>
        /// ��ȡ�û�ID
        /// </summary>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        private string GetEmployeeID(string workNo)
        {
            try
            {
                return DbHelperSQL.GetSingle("select EmployeeID from USER_Employee WHERE WorkNo = '" + workNo.Trim() + "'").ToString();
            }
            catch
            {
                return "0";
            }
        }
    }
}
