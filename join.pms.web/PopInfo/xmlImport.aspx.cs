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
using UNV.Comm.DataBase;
using UNV.Comm.Web;
using System.Text;
using System.Xml;
using System.IO;

namespace AreWeb.OnlineCertificate.PopInfo
{
    public partial class xmlImport : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        public string m_TargetUrl;
        private string m_FuncCode;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private DataSet m_Ds;
        private DataTable m_Dt;
        private string m_SqlParams;

        private string m_FuncInfo;
        private string m_Titles;
        private string m_Fields;
        private string m_UpFileName;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                SetOpration(m_FuncCode);
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
        // -
        private void SetOpration(string funcNo)
        {
            string funcName = string.Empty;
            try
            {
                switch (funcNo)
                {

                    case "020301":
                        funcName = "���һ����Ǽ���Ϣ�ɼ�";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                this.LabelMsg.Text = "����ʧ�ܣ�<br/><br/>" + ex.Message; // " + m_SqlParams + "
            }

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
                    this.LabelMsg.Text = "������ѡ�����Ҫ�ɼ��Ľ�ѹ����Ļ����Ǽ�xml�����ļ���";
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
            else { this.LabelMsg.Text = "��ѡ���ļ���"; }
        }

        private DataTable m_AreaDt;

        /// <summary>
        /// ���ݵ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butImport_Click(object sender, EventArgs e)
        {
            // �����ļ�
            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGridShare.config");
            GetConfigParams(this.m_FuncCode, configFile, ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                Response.Write(" <script>alert('��ȡ�����ļ�ʧ�ܣ�') ;window.location.href='" + m_TargetUrl + "'</script>");
            }
            string[] a_FuncInfo = this.m_FuncInfo.Split(',');
            string funcName = string.Empty;

            if (Session["FileName"] != null)
            {
                m_UpFileName = Session["FileName"].ToString();

                try
                {
                    switch (this.m_FuncCode)
                    {
                        case "020301":
                            funcName = "�����Ǽ���Ϣ";
                            break;
                        default:
                            break;
                    }

                    //��ȡ������������
                    string siteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
                    m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + siteArea + "' ORDER BY AreaCode";
                    m_AreaDt = new DataTable();
                    m_AreaDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                    if (m_AreaDt.Rows.Count > 0) {
                        ImportXMLData(this.m_FuncCode, a_FuncInfo[0], " FuncNo='" + this.m_FuncCode + "' ", ReadXmlToDs(m_UpFileName));
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
            this.LiteralNav.Text = "������ҳ  &gt;&gt; " + funcName + "��";
        }


        #region ���ݵ���

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportXMLData(string funcNo, string tableName, string filterSQL, DataSet xlsDs)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null)
            {
                sHtml.Append("����ʧ�ܣ���ȡXML���ݼ�����<br>");
                sHtml.Append("����취���뽫�������ı�׼���ݽ�ѹ����,�ϴ�xml�ļ���<br>");
                sHtml.Append("ϵͳĿǰ��֧�ֹ��һ����Ǽ���Ϣ�Ĳɼ�......<br>");
                this.LabelMsg.Text = sHtml.ToString();
                return;
            }
            string ReportDate = PageValidate.GetTrim(this.txtReportDate.Value);
            //if (String.IsNullOrEmpty(ReportDate))
            //{
            //    this.LabelMsg.Text = "��ѡ���������ڣ�";
            //    return;
            //}

            // �Ѿ����ڵļ�¼
            //m_SqlParams = "SELECT * FROM " + tableName + " WHERE " + filterSQL;
            //m_Dt = new DataTable();
            //m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // ��ʼ����
            DataTable impDt = new DataTable();
            if (xlsDs.Tables.Count > 1)
            {
                if (xlsDs.Tables[0].Rows.Count > 2)
                {
                    impDt = xlsDs.Tables[0];
                }
                else if (xlsDs.Tables[1].Rows.Count > 2)
                {
                    impDt = xlsDs.Tables[1];
                }
                else { impDt = null; }
            }
            else { impDt = xlsDs.Tables[0]; }

            string totalCount = impDt.Rows.Count.ToString(); // ����
            string bizType = string.Empty;
            string certNo = string.Empty,selSql=string.Empty;
            int iCol = impDt.Columns.Count;
            int iPassed = 0; // �ɹ���   
            int iUnPass = 0; // ʧ����
            int iHuLue = 0; // ������
            // ����ǰ����
            for (int i = 2; i < impDt.Rows.Count; i++)
            {
                /*
                 <option value="IA">--���Ǽ�--</option>
<option value="IB">--���Ǽ�--</option>
<option value="ICA">--�������Ǽ�֤--</option>
<option value="ICB">--�������Ǽ�֤--</option>
<option value="ID">--���������Ǽ�--</option>
                 */
                if (StrTrim(impDt.Rows[i][0].ToString()) == "IA" || StrTrim(impDt.Rows[i][0].ToString()) == "ICA") { bizType = "���Ǽ�"; }
                else { bizType = "���Ǽ�"; }
                // �ж��Ƿ��ظ�����,����֤�ݺ��� 2013/05/20 by Ysl
                certNo = StrTrim(impDt.Rows[i]["CERT_NO"].ToString());
                selSql = "SELECT COUNT(*) FROM PIS_BaseInfo WHERE FuncNo='" + funcNo + "' AND Fileds02='" + certNo + "'";

                StringBuilder strSql = new StringBuilder();
                // INSERT ��¼
                strSql.Append("INSERT INTO PIS_BaseInfo(");
                strSql.Append("OprateUserID,FuncNo,AuditFlag,ReportDate,");
                strSql.Append("Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19,Fileds20,AreaCode,AreaName");
                strSql.Append(") VALUES(");
                strSql.Append("@OprateUserID,@FuncNo,@AuditFlag,@ReportDate,");
                strSql.Append("@Fileds01,@Fileds02,@Fileds03,@Fileds04,@Fileds05,@Fileds06,@Fileds07,@Fileds08,@Fileds09,@Fileds10,@Fileds11,@Fileds12,@Fileds13,@Fileds14,@Fileds15,@Fileds16,@Fileds17,@Fileds18,@Fileds19,@Fileds20,@AreaCode,@AreaName");
                strSql.Append(");select @@IDENTITY");
                //if (String.IsNullOrEmpty(CommID))
                //{
                //}
                //else
                //{
                //    // update ��¼
                //    strSql.Append("UPDATE Sys_QueueBiz SET ");
                //    strSql.Append("NationalName=@NationalName,NationalSex=@NationalSex,NationalID=@NationalID, NationalBirthDay=@NationalBirthDay,  ");
                //    strSql.Append("NationalWorkUint=@NationalWorkUint, CurrentAddress=@CurrentAddress,MarraigeType=@MarraigeType,");
                //    strSql.Append("NationalBY=@NationalBY,YZName=@YZName,YZTel=@YZTel,RelationType=@RelationType,WifeName=@WifeName,");
                //    strSql.Append("WifeSex=@WifeSex,WifeID=@WifeID,WifeBirthDay=@WifeBirthDay,WifeBY=@WifeBY,BoysNum=@BoysNum,GirlsNum=@GirlsNum,MarraigeID=@MarraigeID");
                //    strSql.Append(" WHERE CommID=" + CommID);
                //}

                SqlParameter[] parameters = {
					new SqlParameter("@OprateUserID", SqlDbType.Int,4),
					new SqlParameter("@FuncNo", SqlDbType.VarChar,20),
                    new SqlParameter("@AuditFlag", SqlDbType.TinyInt,1),
                    new SqlParameter("@ReportDate", SqlDbType.SmallDateTime,4),
					new SqlParameter("@Fileds01", SqlDbType.VarChar,60),
                    new SqlParameter("@Fileds02", SqlDbType.VarChar,60),//5
					new SqlParameter("@Fileds03", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds04", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds05", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds06", SqlDbType.VarChar,60),
                  	new SqlParameter("@Fileds07", SqlDbType.VarChar,60), //10
                    new SqlParameter("@Fileds08", SqlDbType.VarChar,60), 
                    new SqlParameter("@Fileds09", SqlDbType.VarChar,60), 
                    new SqlParameter("@Fileds10", SqlDbType.VarChar,60), 
                    new SqlParameter("@Fileds11", SqlDbType.VarChar,60),
                    new SqlParameter("@Fileds12", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds13", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds14", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds15", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds16", SqlDbType.VarChar,60),
                  	new SqlParameter("@Fileds17", SqlDbType.VarChar,60), //20
                    new SqlParameter("@Fileds18", SqlDbType.VarChar,60), 
                    new SqlParameter("@Fileds19", SqlDbType.VarChar,60), 
                    new SqlParameter("@Fileds20", SqlDbType.VarChar,60),
                    new SqlParameter("@AreaCode", SqlDbType.VarChar,60),
                    new SqlParameter("@AreaName", SqlDbType.VarChar,60)
                   };
                parameters[0].Value = this.m_UserID;
                parameters[1].Value = funcNo;
                parameters[2].Value = 0;
                parameters[3].Value = ToDateTime(impDt.Rows[i]["OP_DATE"].ToString()); // ToDateTime(impDt.Rows[i][45].ToString());
                /*
ҵ������,֤������,�Ǽ�ʱ��,�з�����,���֤��,ְҵ,��ͥ��ַ,����,�Ļ��̶�,����״��,��������,Ů������,���֤��,ְҵ,��ͥ��ַ,����,�Ļ��̶�,����״��,��������
                */
                parameters[4].Value = bizType;
                parameters[5].Value = certNo;
                parameters[6].Value = StrTrim(impDt.Rows[i]["OP_DATE"].ToString());
                parameters[7].Value = StrTrim(impDt.Rows[i][3].ToString());
                parameters[8].Value = StrTrim(impDt.Rows[i][9].ToString());
                // ְҵ,��ͥ��ַ,����,�Ļ��̶�,����״��,
                parameters[9].Value = GetJobName(StrTrim(impDt.Rows[i]["JOB_MAN"].ToString()));
                parameters[10].Value = StrTrim(impDt.Rows[i]["REG_DETAIL_MAN"].ToString());
                parameters[11].Value = GetFolkName(StrTrim(impDt.Rows[i]["FOLK_MAN"].ToString())); // ����
                parameters[12].Value = GetEduName(StrTrim(impDt.Rows[i]["DEGREE_MAN"].ToString()));
                parameters[13].Value = GetMarryNa(StrTrim(impDt.Rows[i]["MARRY_STATUS_MAN"].ToString()));
                // ��������,Ů������,���֤��,ְҵ,��ͥ��ַ
                parameters[14].Value = StrTrim(impDt.Rows[i]["BIRTH_MAN"].ToString());
                parameters[15].Value = StrTrim(impDt.Rows[i][4].ToString());
                parameters[16].Value = StrTrim(impDt.Rows[i][10].ToString());
                parameters[17].Value = GetJobName(StrTrim(impDt.Rows[i]["JOB_WOMAN"].ToString()));
                parameters[18].Value = StrTrim(impDt.Rows[i]["REG_DETAIL_WOMAN"].ToString());
                //if (string.IsNullOrEmpty(StrTrim(impDt.Rows[i][28].ToString()))) { parameters[18].Value = StrTrim(impDt.Rows[i]["REG_DETAIL_WOMAN"].ToString()); }
                //else { parameters[18].Value = StrTrim(impDt.Rows[i][28].ToString()); }
                
                // ����,�Ļ��̶�,����״��,��������,xx
                parameters[19].Value = GetFolkName(StrTrim(impDt.Rows[i]["FOLK_WOMAN"].ToString()));
                parameters[20].Value = GetEduName(StrTrim(impDt.Rows[i]["DEGREE_WOMAN"].ToString()));
                parameters[21].Value = GetMarryNa(StrTrim(impDt.Rows[i]["MARRY_STATUS_WOMAN"].ToString()));
                parameters[22].Value = StrTrim(impDt.Rows[i]["BIRTH_WOMAN"].ToString());
                parameters[23].Value = "xml";// StrTrim(impDt.Rows[i][28].ToString());
                parameters[24].Value = GetAreaCode(StrTrim(impDt.Rows[i]["REG_DETAIL_MAN"].ToString()));
                parameters[25].Value = StrTrim(impDt.Rows[i]["REG_DETAIL_MAN"].ToString());
                // ִ�в���
                try
                {
                    if (DbHelperSQL.GetSingle(selSql).ToString() == "0")
                    {
                        if (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0)
                        {
                            iPassed++;
                        }
                        else
                        {
                            iUnPass++;
                        }
                    }
                    else { iHuLue++; }
                }
                catch { 
                    iUnPass++; 
                }
                
            }
            impDt = null;

            selSql = null;

            xlsDs.Dispose();
            sHtml.Append("<br><br>�����ļ�����" + totalCount + "��Ϣ���ɹ�������" + iPassed.ToString() + "����Ϣ��ʧ����" + iUnPass.ToString() + "����Ϣ��ֱ�Ӻ��Ե���Ϣ " + iHuLue + "����");
            this.LabelMsg.Text = sHtml.ToString();
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="xmlArea"></param>
        /// <returns></returns>
        private string GetAreaCode(string xmlArea)
        {
            string areaName = "", areaCode="";
            for (int k = 0; k < m_AreaDt.Rows.Count; k++)
            {
                areaName = m_AreaDt.Rows[k][1].ToString();
                if (xmlArea.IndexOf(areaName) > 0)
                {
                    areaCode = m_AreaDt.Rows[k][0].ToString();
                    break;
                }
            }

            return areaCode;
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string GetFolkName(string code)
        {

            try {
                return DbHelperSQL.GetSingle("select mz_Name from PIS_FOLK where mz_Code='" + code + "'").ToString();
            }
            catch {
                return code;
            }
        }

        /// <summary>
        /// ��ȡְҵ��
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private string GetJobName(string jobCode) {
            string reVal="";
            switch (jobCode)
            {
                case "0":
                    reVal = "���һ��ء���Ⱥ��֯����ҵ����ҵ��λ������";
                    break;
                case "1/2":
                    reVal = "רҵ������Ա";
                    break;
                case "3":
                    reVal = "������Ա���й���Ա";
                    break;
                case "4":
                    reVal = "��ҵ������ҵ��Ա";
                    break;
                case "5":
                    reVal = "ũ���֡������桢ˮ��ҵ������Ա";
                    break;
                case "6/7/8/9":
                    reVal = "�����������豸������Ա���й���Ա";
                    break;
                case "X":
                    reVal = "����";
                    break;
                case "Y":
                    reVal = "��������������ҵ��Ա";
                    break;
                default:
                    reVal = jobCode;
                    break;
            }
            return reVal;
        }

        /// <summary>
        /// ��ȡ�Ļ��̶�
        /// </summary>
        /// <param name="jobCode"></param>
        /// <returns></returns>
        private string GetEduName(string code)
        {
            string reVal = "";
            switch (code)
            {
                case "10":
                    reVal = "�о�������";
                    break;
                case "20/30":
                    reVal = "��ѧ����/ר�ƽ���";
                    break;
                case "40":
                    reVal = "�е�ְҵ����";
                    break;
                case "60":
                    reVal = "��ͨ�߼���ѧ����";
                    break;
                case "70":
                    reVal = "������ѧ����";
                    break;
                case "80":
                    reVal = "Сѧ����";
                    break;
                case "90":
                    reVal = "����";
                    break;
                default:
                    reVal = code;
                    break;
            }
            return reVal;
        }
        /*
<option value='10' >δ��</option><option value='30' >ɥż</option><option value='40' >���</option>
        */
        /// <summary>
        /// ��ȡ����״��
        /// </summary>
        /// <param name="jobCode"></param>
        /// <returns></returns>
        private string GetMarryNa(string code)
        {
            string reVal = "";
            switch (code)
            {
                case "10":
                    reVal = "δ��";
                    break;
                case "30":
                    reVal = "ɥż";
                    break;
                case "40":
                    reVal = "���";
                    break;
                default:
                    reVal = code;
                    break;
            }
            return reVal;
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

            if (strExt == ".xml")
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
        private DataSet ReadXmlToDs(string xmlFile)
        {
            try
            {
               


                if (Session["sourceFile"] == null)
                {
                    Response.Write("������ʱ�������µ�¼�����ԣ�");
                    return null;
                }
                else
                {
                    xmlFile = GetFilePhysicalPath() + xmlFile;
                    string sourceFile = Session["sourceFile"].ToString();
                    DataSet ds = new DataSet();
                    ds.ReadXml(xmlFile);

                    return ds;
                    //dt = ds.Tables[1];

                    //if (ds.Tables.Count > 1)
                    //{
                    //    if (ds.Tables[0].Rows.Count > 2)
                    //    {

                    //    }
                    //    else if (ds.Tables[0].Rows.Count > 2)
                    //    {
                    //    }
                    //    else { return null; }
                    //}
                }
            }
            catch (Exception ex)
            {
                return null;
            }
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

    }
}
