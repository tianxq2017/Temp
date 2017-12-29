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

namespace AreWeb.OnlineCertificate.PopInfo
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
            string configFile = Server.MapPath("/includes/DataGridShare.config");
            GetConfigParams(this.m_FuncCode, configFile, ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                Response.Write(" <script>alert('��ȡ�����ļ�ʧ�ܣ�') ;window.location.href='" + m_TargetUrl + "'</script>");
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
                areaCode = "610630000000";
                areaName = "����ʡ�Ӱ������˴���";
                //this.LiteralMsg.Text = "��ѡ�����ݵ�λ��";
                //this.butImport.Enabled = true;
                //return;
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
        /*
01010101	ȫԱ�˿ڸ���������Ϣ
01010102	�˿ڱ䶯������Ϣ
01010103	�˿ڱ䶯������Ϣ
01010104	�˿ڱ䶯Ǩ���˿�
01010105	�˿ڱ䶯Ǩ����Ϣ
01010106	���举Ů��Ϣ
01010107	������������Ϣ
* 
01010116	�����˿�����Ǽ���Ϣ
01010117	�����˿������Ǽ���Ϣ
01010118	�ر����������������
01010119	��Ů������������������
01010120	���ҽ���������������
01010121	�˿ڻ������ͳ�Ʋ�ѯ��Ϣ
01010122	ȫԱ�������Ů��Ϣ
01010123	һ֤ͨ������Ů��Ϣ
01010124	ȫԱ�����������Ϣ
01010125	һ֤ͨ����������Ϣ
01010126	ȫԱ�������������������
01010127	һ֤ͨ������������������
01010128	ȫԱ��ũ�������ͥ����֤��Ϣ
01010129	һ֤ͨũ�������ͥ����֤��Ϣ
01010130	ȫԱ��ũ���Ů����ͥ������Ϣ
01010131	һ֤ͨũ���Ů����ͥ������Ϣ


*/
        private string GetFuncName()
        {
            string funcName = string.Empty;
            switch (this.m_FuncCode)
            {
                case "01010101":
                    funcName = "�˿ڻ���������Ϣ";
                    break;
                case "01010102":
                    funcName = "�˿ڱ䶯������Ϣ";
                    break;
                case "01010103":
                    funcName = "�˿ڱ䶯������Ϣ";
                    break;
                case "01010104":
                    funcName = "�˿ڱ䶯Ǩ���˿�";
                    break;
                case "01010105":
                    funcName = "�˿ڱ䶯Ǩ����Ϣ";
                    break;
                case "01010106":
                    funcName = "���举Ů��Ϣ";
                    break;
                case "01010107":
                    funcName = "������������Ϣ";
                    break;
                case "01010116":
                    funcName = "�����˿�����Ǽ���Ϣ";
                    break;
                case "01010117":
                    funcName = "�����˿������Ǽ���Ϣ";
                    break;
                case "01010118":
                    funcName = "�ر����������������";
                    break;
                case "01010119":
                    funcName = "��Ů������������������";
                    break;
                case "01010120":
                    funcName = "���ҽ���������������";
                    break;
                case "01010121":
                    funcName = "�˿ڻ������ͳ�Ʋ�ѯ��Ϣ";
                    break;
                case "01010122":
                    funcName = "ȫԱ�������Ů��Ϣ";
                    break;
                case "01010123":
                    funcName = "һ֤ͨ������Ů��Ϣ";
                    break;
                case "01010124":
                    funcName = "ȫԱ��������������Ϣ";
                    break;
                case "01010125":
                    funcName = "һ֤ͨ��������������Ϣ";
                    break;
                case "01010126":
                    funcName = "ȫԱ�������������������";
                    break;
                case "01010127":
                    funcName = "һ֤ͨ������������������";
                    break;
                case "01010128":
                    funcName = "ȫԱ��ũ�������ͥ����֤��Ϣ";
                    break;
                case "01010129":
                    funcName = "һ֤ͨũ�������ͥ����֤��Ϣ";
                    break;
                case "01010130":
                    funcName = "ȫԱ��ũ���Ů����ͥ������Ϣ";
                    break;
                case "01010131":
                    funcName = "һ֤ͨũ���Ů����ͥ������Ϣ";
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
                    StringBuilder selSql = new StringBuilder();
                    selSql.Append("SELECT COUNT(*) FROM PIS_QYK WHERE FuncNo=@FuncNo ");
                    selSql.Append("AND Fileds01=@Fileds01 AND Fileds02=@Fileds02 AND Fileds03=@Fileds03 AND Fileds04=@Fileds04 AND Fileds05=@Fileds05 ");
                    SqlParameter[] sParams = {
					new SqlParameter("@FuncNo", SqlDbType.VarChar,20),
					new SqlParameter("@Fileds01", SqlDbType.VarChar,60),
                    new SqlParameter("@Fileds02", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds03", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds04", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds05", SqlDbType.VarChar,60)
                    };
                    sParams[0].Value = funcNo;
                    sParams[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString());
                    sParams[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString());
                    sParams[3].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString());
                    sParams[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString());
                    if (iCol > 4) { sParams[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()); } else { sParams[5].Value = ""; }

                    // INSERT ��¼
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("INSERT INTO PIS_QYK(");
                    strSql.Append("OprateUserID,FuncNo,P_CardID,");
                    strSql.Append("Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19,Fileds20,Fileds21,Fileds22,Fileds23,Fileds24,Fileds25,AreaCode,AreaName,Attribs,OprateDate");
                    strSql.Append(") VALUES(");
                    strSql.Append("@OprateUserID,@FuncNo,@P_CardID,");
                    strSql.Append("@Fileds01,@Fileds02,@Fileds03,@Fileds04,@Fileds05,@Fileds06,@Fileds07,@Fileds08,@Fileds09,@Fileds10,@Fileds11,@Fileds12,@Fileds13,@Fileds14,@Fileds15,@Fileds16,@Fileds17,@Fileds18,@Fileds19,@Fileds20,@Fileds21,@Fileds22,@Fileds23,@Fileds24,@Fileds25,@AreaCode,@AreaName,@Attribs,@OprateDate");
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
                    new SqlParameter("@P_CardID", SqlDbType.VarChar,50),
					new SqlParameter("@Fileds01", SqlDbType.VarChar,60),
                    new SqlParameter("@Fileds02", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds03", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds04", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds05", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds06", SqlDbType.VarChar,60),
                  	new SqlParameter("@Fileds07", SqlDbType.VarChar,60), 

                    new SqlParameter("@Fileds08", SqlDbType.VarChar,60), 
                    new SqlParameter("@Fileds09", SqlDbType.VarChar,60), 
                    new SqlParameter("@Fileds10", SqlDbType.VarChar,60), 
                    new SqlParameter("@Fileds11", SqlDbType.VarChar,60),
                    new SqlParameter("@Fileds12", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds13", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds14", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds15", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds16", SqlDbType.VarChar,60),
                  	new SqlParameter("@Fileds17", SqlDbType.VarChar,60), 
                    new SqlParameter("@Fileds18", SqlDbType.VarChar,60), 

                    new SqlParameter("@Fileds19", SqlDbType.VarChar,60), 
                    new SqlParameter("@Fileds20", SqlDbType.VarChar,60),
                    new SqlParameter("@Fileds21", SqlDbType.VarChar,60),
                    new SqlParameter("@Fileds22", SqlDbType.VarChar,60),
                    new SqlParameter("@Fileds23", SqlDbType.VarChar,60),
                    new SqlParameter("@Fileds24", SqlDbType.VarChar,60),
                    new SqlParameter("@Fileds25", SqlDbType.VarChar,60),
                    new SqlParameter("@AreaCode", SqlDbType.VarChar,50),
                    new SqlParameter("@AreaName", SqlDbType.VarChar,50),
                    new SqlParameter("@Attribs", SqlDbType.TinyInt,1),
                    new SqlParameter("@OprateDate", SqlDbType.SmallDateTime,4)
                   };
                    parameters[0].Value = this.m_UserID;
                    parameters[1].Value = funcNo;
                    /*
01010101	ȫԱ�˿ڸ���������Ϣ FILEDS04
01010102	�˿ڱ䶯������Ϣ FILEDS04
01010103	�˿ڱ䶯������Ϣ FILEDS04
01010104	�˿ڱ䶯Ǩ���˿� FILEDS04
01010105	�˿ڱ䶯Ǩ����Ϣ Fileds04
01010106	���举Ů��Ϣ  Fileds04
01010107	������������Ϣ FILEDS02
* 
01010116	�����˿�����Ǽ���Ϣ Fileds02
01010117	�����˿������Ǽ���Ϣ Fileds02
01010118	�ر���������������� Fileds02
01010119	��Ů������������������ Fileds02
01010120	���ҽ���������������  Fileds02
01010121	�˿ڻ������ͳ�Ʋ�ѯ��Ϣ x
01010122	ȫԱ�������Ů��Ϣ Fileds02
01010123	һ֤ͨ������Ů��Ϣ Fileds02
01010124	ȫԱ�����������Ϣ Fileds02
01010125	һ֤ͨ����������Ϣ Fileds02
01010126	ȫԱ������������������� Fileds02
01010127	һ֤ͨ������������������ Fileds02
01010128	ȫԱ��ũ�������ͥ����֤��Ϣ Fileds04
01010129	һ֤ͨũ�������ͥ����֤��Ϣ Fileds04
01010130	ȫԱ��ũ���Ů����ͥ������Ϣ Fileds04
01010131	һ֤ͨũ���Ů����ͥ������Ϣ Fileds04

*/
                    // ���֤��
                    if (funcNo == "01010121")
                    {
                        parameters[2].Value = ""; // 01010121	�˿ڻ������ͳ�Ʋ�ѯ��Ϣ x
                    }
                    else if (funcNo == "01010101" || funcNo == "01010102" || funcNo == "01010103" || funcNo == "01010104" || funcNo == "01010105" || funcNo == "01010106" || funcNo == "01010128" || funcNo == "01010129" || funcNo == "01010130" || funcNo == "01010131")
                    {
                        parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString()); // Fileds04
                    }
                    else
                    {
                        parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString()); // Fileds02
                    }

                    parameters[3].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString());
                    parameters[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString());
                    parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString());
                    parameters[6].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString());
                    parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString());

                    if (iCol > 5) { parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString()); } else { parameters[8].Value = ""; }
                    if (iCol > 6) { parameters[9].Value = StrTrim(xlsDs.Tables[0].Rows[i][6].ToString()); } else { parameters[9].Value = ""; }
                    if (iCol > 7) { parameters[10].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); } else { parameters[10].Value = ""; }
                    if (iCol > 8) { parameters[11].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); } else { parameters[11].Value = ""; }
                    if (iCol > 9) { parameters[12].Value = StrTrim(xlsDs.Tables[0].Rows[i][9].ToString()); } else { parameters[12].Value = ""; }
                    if (iCol > 10) { parameters[13].Value = StrTrim(xlsDs.Tables[0].Rows[i][10].ToString()); } else { parameters[13].Value = ""; }
                    if (iCol > 11) { parameters[14].Value = StrTrim(xlsDs.Tables[0].Rows[i][11].ToString()); } else { parameters[14].Value = ""; }
                    if (iCol > 12) { parameters[15].Value = StrTrim(xlsDs.Tables[0].Rows[i][12].ToString()); } else { parameters[15].Value = ""; }
                    if (iCol > 13) { parameters[16].Value = StrTrim(xlsDs.Tables[0].Rows[i][13].ToString()); } else { parameters[16].Value = ""; }
                    if (iCol > 14) { parameters[17].Value = StrTrim(xlsDs.Tables[0].Rows[i][14].ToString()); } else { parameters[17].Value = ""; }
                    if (iCol > 15) { parameters[18].Value = StrTrim(xlsDs.Tables[0].Rows[i][15].ToString()); } else { parameters[18].Value = ""; }
                    if (iCol > 16) { parameters[19].Value = StrTrim(xlsDs.Tables[0].Rows[i][16].ToString()); } else { parameters[19].Value = ""; }
                    if (iCol > 17) { parameters[20].Value = StrTrim(xlsDs.Tables[0].Rows[i][17].ToString()); } else { parameters[20].Value = ""; }
                    if (iCol > 18) { parameters[21].Value = StrTrim(xlsDs.Tables[0].Rows[i][18].ToString()); } else { parameters[21].Value = ""; }
                    if (iCol > 19) { parameters[22].Value = StrTrim(xlsDs.Tables[0].Rows[i][19].ToString()); } else { parameters[22].Value = ""; }
                    if (iCol > 20) { parameters[23].Value = StrTrim(xlsDs.Tables[0].Rows[i][20].ToString()); } else { parameters[23].Value = ""; }
                    if (iCol > 21) { parameters[24].Value = StrTrim(xlsDs.Tables[0].Rows[i][21].ToString()); } else { parameters[24].Value = ""; }
                    if (iCol > 22) { parameters[25].Value = StrTrim(xlsDs.Tables[0].Rows[i][22].ToString()); } else { parameters[25].Value = ""; }
                    if (iCol > 23) { parameters[26].Value = StrTrim(xlsDs.Tables[0].Rows[i][23].ToString()); } else { parameters[26].Value = ""; }
                    if (iCol > 24) { parameters[27].Value = StrTrim(xlsDs.Tables[0].Rows[i][24].ToString()); } else { parameters[27].Value = ""; }

                    parameters[28].Value = areaCode;
                    parameters[29].Value = areaName;
                    //���֤��У�� Attribs:0,Ĭ�� 3,���֤У��ʧ��
                    if (!ValidIDCard.VerifyIDCard(parameters[2].Value.ToString()))
                    {
                        parameters[30].Value = 3;
                        iHuLue++;
                    }
                    else
                    {
                        parameters[30].Value = 0;
                    }
                    parameters[31].Value = oprateDate;

                    // ִ�в���
                    //if (DbHelperSQL.GetSingle(selSql.ToString(), sParams).ToString() == "0")
                    //{
                    //    //���ظ�����,����
                    //    if (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0)
                    //    {
                    //        iPassed++;
                    //    }
                    //    else
                    //    {
                    //        iUnPass++;
                    //    }
                    //}
                    //else { iRe++; }


                    //���ظ�����,����
                    if (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0)
                    {
                        iPassed++;
                    }
                    else
                    {
                        iUnPass++;
                    }
                    // ִ�бȶ� -- �ں�̨����ʵ��
                }
                xlsDs.Dispose();
                //sHtml.Append("<br><br>�����ļ�����" + totalCount + "��Ϣ���ɹ�������" + iPassed.ToString() + "����Ϣ��ʧ����" + iUnPass.ToString() + "����Ϣ�����֤��У��ʧ�� " + iHuLue + "�������Ե��ظ���Ϣ " + iRe + " ����");
                sHtml.Append("<br><br>�����ļ�����" + totalCount + "��Ϣ���ɹ�������" + iPassed.ToString() + "����Ϣ��ʧ����" + iUnPass.ToString() + "����Ϣ�����֤��У��ʧ�� " + iHuLue + "����");

            }
            catch (Exception ex)
            {
                sHtml.Append("<br/>��������������д���Ϊ[" + debugRow + "]<br/>");
                sHtml.Append(ex.Message);
            }

            this.LiteralMsg.Text = sHtml.ToString();
        }

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

