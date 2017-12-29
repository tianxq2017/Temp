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
using join.pms.dal;

namespace AreWeb.OnlineCertificate.PopInfo
{
    public partial class XlsChildBaseImp : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        public string m_TargetUrl;
        protected string m_FuncCode;

        private string m_UserID; // 当前登录的操作用户编号
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
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 验证接受的参数
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
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }
        }

        /// <summary>
        /// 设置数据单位/行政区划单位 所有乡镇……
        /// </summary>
        /// <param name="areaCode">默认值</param>
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
            //this.DDLReportArea.Items.Insert(0, new ListItem("全部行政区划……", ""));
            //tmpDt = null;
            //if (!string.IsNullOrEmpty(areaCode))
            //{
            //    this.DDLReportArea.SelectedValue = areaCode;
            //}
        }

        private void SetOpration(string funcNo)
        {
            string funcName = GetFuncName();

            this.LiteralNav.Text = join.pms.dal.CommPage.GetAllTreeName(funcNo, true) + "数据导入：";
        }

        #region  获取配置文件参数
        /// <summary>
        /// 配置数据集
        /// </summary>
        private void ConfigDataSet()
        {
            m_Ds = new DataSet();
            m_Ds.Locale = System.Globalization.CultureInfo.InvariantCulture;
        }
        /// <summary>
        /// 获取配置文件参数
        /// </summary>
        /// <param name="funcNo">功能号</param>
        /// <param name="configFile">配置文件路径</param>
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
                errorMsg = "获取配置文件参数失败：" + ex.Message;
                return false;
            }
        }
        #endregion



        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butUpLoad_Click(object sender, EventArgs e)
        {
            // 上传文件
            if (!String.IsNullOrEmpty(this.upFiles.FileName))
            {
                string fileName = this.upFiles.PostedFile.FileName;
                if (String.IsNullOrEmpty(fileName) || fileName.Length < 1 || !IsXlsFile(fileName))
                {
                    this.LabelMsg.Text = "请首先选择浏览要导入的 Excel 数据文件！导入格式以系统导出的格式为准。";
                    return;
                }

                try
                {
                    string filePath = this.GetFilePhysicalPath() + this.GetPicPhysicalFileName(fileName);
                    this.upFiles.PostedFile.SaveAs(filePath);
                    Session["FileName"] = m_UpFileName;
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    Session["sourceFile"] = fileName;//"D:\\Temp\\20080827111448115.xls"
                    this.LabelMsg.Text = "文件[ " + fileName + " ]上传成功至[" + filePath + "]！<br/>请点击导入按钮执行导入……";
                }
                catch (System.Exception ex)
                {
                    this.LabelMsg.Text = ex.Message;
                }
            }
            else { this.LabelMsg.Text = "清选择文件"; }
        }

        private bool m_IsAreaSel;
        /// <summary>
        /// 数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butImport_Click(object sender, EventArgs e)
        {
            this.butImport.Enabled = false; // 禁用按钮,防止重复导入

            string errMsg = string.Empty;
            string funcName = string.Empty;
            string ReportDate = PageValidate.GetTrim(this.txtReportDate.Value);            
            if (String.IsNullOrEmpty(ReportDate))
            {
                this.LiteralMsg.Text = "请选择数据日期！";
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
                    ImportXlsData(ReadXlsToDs(m_UpFileName),ReportDate);
                }
                catch (Exception ex)
                {
                    this.LiteralMsg.Text = "操作失败：<br/><br/>" + ex.Message; // " + m_SqlParams + "
                }
            }
            else
            {
                this.LiteralMsg.Text = "操作失败：请选择文件上传后，再点击“导入”按钮！";
                this.butImport.Enabled = true;
            }
            this.LiteralNav.Text = "管理首页  &gt;&gt; " + funcName + "：";
        }
        private string GetFuncName()
        {
            string funcName = string.Empty;
            switch (this.m_FuncCode)
            {
                case "070201":
                    funcName = "儿童基础信息";
                    break;
                default:
                    funcName = "";
                    break;
            }
            return funcName;
        }
        #region 数据导入

        /// <summary>
        /// 导入QYK数据
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportXlsData(DataSet xlsDs, string oprateDate)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null || xlsDs.Tables[0].Columns.Count < 1)
            {
                sHtml.Append("操作失败：获取 Excel 数据集错误！<br>");
                sHtml.Append("解决办法：请确保您想要导入的Excel文件符合导入数据的标准格式，然后再试。<br>");
                sHtml.Append("如果是系统自动生成的Excel文件，请用Excel打开后，另存为“Microsoft Office Excel 工作薄(*.xls)”格式，然后再试。<br>");
                sHtml.Append("导入数据的 Excel表头的标准格式为系统导出的相应文件的格式。");
                this.LiteralMsg.Text = sHtml.ToString();
                return;
            }

            // 已经存在的记录 OprateDate,AttribsCN,CommMemo
            //m_SqlParams = "SELECT * FROM " + tableName + " WHERE " + filterSQL;
            //m_Dt = new DataTable();
            //m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // 开始导入
            int debugRow = 0;

            try
            {
                string totalCount = xlsDs.Tables[0].Rows.Count.ToString(); // 总数
                int iCol = xlsDs.Tables[0].Columns.Count;
                int iPassed = 0; // 成功数   
                int iUnPass = 0; // 失败数
                int iHuLue = 0; // 忽略数
                int iRe = 0;
                // 忽略前二行
                for (int i = 0; i < xlsDs.Tables[0].Rows.Count; i++)
                {
                    debugRow++;

                    // 判断某条记录是否重复,存在重复的忽略
                    StringBuilder selSql = new StringBuilder();
                    selSql.Append("SELECT COUNT(*) FROM NHS_Child_Base WHERE ChildBm=@ChildBm ");
                    SqlParameter[] sParams = {
					new SqlParameter("@ChildBm", SqlDbType.VarChar,50)
                    };
                    sParams[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString());

                    // INSERT 记录
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("INSERT INTO NHS_Child_Base(");
                    strSql.Append("ChildBm,ChildName,");
                    strSql.Append("ChildSex,BirthDay,CurAreaName,FatherTel,HomeAddress,MotherTel,FatherName,MotherName,PostalAddress,InCase,LiveType,RecordDate,CreateDate,UnvID");
                    strSql.Append(") VALUES(");
                    strSql.Append("@ChildBm,@ChildName,");
                    strSql.Append("@ChildSex,@BirthDay,@CurAreaName,@FatherTel,@HomeAddress,@MotherTel,@FatherName,@MotherName,@PostalAddress,@InCase,@LiveType,@RecordDate,@CreateDate,@UnvID");
                    strSql.Append(");select @@IDENTITY");

                    SqlParameter[] parameters = {
					new SqlParameter("@ChildBm", SqlDbType.VarChar,50),
					new SqlParameter("@ChildName", SqlDbType.VarChar,20),
                    new SqlParameter("@ChildSex", SqlDbType.VarChar,10),
					new SqlParameter("@BirthDay", SqlDbType.DateTime),
                    new SqlParameter("@CurAreaName", SqlDbType.VarChar,50),
                    new SqlParameter("@FatherTel", SqlDbType.VarChar,50),
                    new SqlParameter("@HomeAddress", SqlDbType.VarChar,200),
                    new SqlParameter("@MotherTel", SqlDbType.VarChar,50),
					new SqlParameter("@FatherName", SqlDbType.VarChar,50),
					new SqlParameter("@MotherName", SqlDbType.VarChar,50),  
                    new SqlParameter("@PostalAddress", SqlDbType.VarChar,200),                    
                    new SqlParameter("@InCase", SqlDbType.VarChar,50), 
                    new SqlParameter("@LiveType", SqlDbType.VarChar,20), 
                    new SqlParameter("@RecordDate", SqlDbType.DateTime),					
                    new SqlParameter("@CreateDate", SqlDbType.SmallDateTime,4),
                    new SqlParameter("@UnvID", SqlDbType.VarChar,50)
                   };
                    parameters[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString());
                    parameters[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString());
                    parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString());
                    parameters[3].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString());
                    parameters[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString());
                    parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString());
                    parameters[6].Value = StrTrim(xlsDs.Tables[0].Rows[i][6].ToString());
                    parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString());
                    parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString());
                    parameters[9].Value = StrTrim(xlsDs.Tables[0].Rows[i][10].ToString());
                    parameters[10].Value = StrTrim(xlsDs.Tables[0].Rows[i][12].ToString());
                    parameters[11].Value = StrTrim(xlsDs.Tables[0].Rows[i][13].ToString());
                    parameters[12].Value = StrTrim(xlsDs.Tables[0].Rows[i][14].ToString());
                    parameters[13].Value = StrTrim(xlsDs.Tables[0].Rows[i][15].ToString());
                    parameters[14].Value = oprateDate;
                    parameters[15].Value = GetUnvID();
                    // 执行操作
                    if (DbHelperSQL.GetSingle(selSql.ToString(), sParams).ToString() == "0")
                    {
                        //无重复数据,导入
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
                    // 执行比对 -- 在后台程序实现
                }
                xlsDs.Dispose();
                sHtml.Append("<br><br>数据文件共有" + totalCount + "信息；成功导入了" + iPassed.ToString() + "条信息，失败了" + iUnPass.ToString() + "条信息，忽略的重复信息 " + iRe + " 条。");

            }
            catch (Exception ex)
            {
                sHtml.Append("<br/>发生错误的数据行大致为[" + debugRow + "]<br/>");
                sHtml.Append(ex.Message);
            }

            this.LiteralMsg.Text = sHtml.ToString();
        }
        /// <summary>
        /// 获取儿童编号
        /// </summary>
        /// <returns></returns>
        private string GetUnvID()
        {
            string UnvID = string.Empty;
            try
            {
                string Pefix = "610630" + DateTime.Now.Year.ToString();
                m_SqlParams = "select right('000000'+cast(isnull(max(right(UnvID,6)) + 1,1) as varchar(6)),6) AS UnvID from NHS_Child_Base where substring(UnvID,1,6)+cast(year(getdate()) as varchar(4))='" + Pefix + "'";
                UnvID = "610630" + DateTime.Now.Year.ToString() + CommPage.GetSingleVal(m_SqlParams);
            }
            catch { }

            return UnvID;

        }
        private DataTable m_AreaDt;

        /// <summary>
        /// 获取行政区划的字段索引值,从1开始
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

                    //获取行政区划数据
                    string siteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
                    m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + siteArea + "' ORDER BY AreaCode";
                    m_AreaDt = new DataTable();
                    m_AreaDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                    //检测所有数据是否地址单位存在
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
                        //m_AreaDt = null; 留待后续分析编码时使用
                        xlsDt = null;
                    }
                }
                else
                {
                    checkOK = true; // 不包含在地址检测配置范围内的忽略地址检测
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

        #region 数据导入相关成员方法
        /// <summary>
        /// 判断是否为xls文件
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
            string xlsFile = Server.MapPath("/") + "Temp/" + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + "/";//当前日期上传目录
            if (!Directory.Exists(xlsFile)) Directory.CreateDirectory(xlsFile);
            return xlsFile;
        }

        /// <summary>
        /// 读取 Excel 文件到数据集中
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
                    Response.Write("操作超时，请重新登录后再试！");
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
        /// 时间参数过滤
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

