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
using System.Text;
using System.IO;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;

namespace AreWeb.JdcMmc.DataGather.RptInfo
{
    public partial class XlsImport : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;

        public string m_TargetUrl;

        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号
        private DataSet m_Ds;
        private DataTable m_Dt;
        private string m_SqlParams;

        private string m_UpFileName;

        private string m_NavTitle;
        private string m_RptTime;

        private string m_Fields;
        private int m_IgnoreRows = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            //this.butImport.Attributes.Add("onclick", "SetImportBut()");
            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                SetOpration(m_NavTitle);
            }
        }

        #region  身份验证、参数校验等

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
            m_ActionName = Request.QueryString["action"];
            m_SourceUrl = Request.QueryString["sourceUrl"];
            m_ObjID = Request.QueryString["k"];

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                m_NavTitle = CommPage.GetSingleVal("SELECT FuncName FROM SYS_Function WHERE FuncCode='" + m_FuncCode + "'");
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }
        }

        private void SetOpration(string oprateName)
        {
            this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">管理首页</a> &gt;&gt; 数据采集 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; 从已经填报的Excel文件中导入数据：";
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

        /// <summary>
        /// 数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butImport_Click(object sender, EventArgs e)
        {
            this.butImport.Enabled = false; // 禁用按钮,防止重复导入
            string errMsg = string.Empty;

            if (Session["FileName"] != null)
            {
                m_UpFileName = Session["FileName"].ToString();

                try
                {
                    GetParamsByCode(m_FuncCode); // 设置报表参数

                    ImportXlsData(this.m_FuncCode, "RPT_Contents", " RptID=" + m_ObjID + " ", ReadXlsToDs(m_UpFileName));
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
        }

        /// <summary>
        /// 设置具体导入的报表参数
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetParamsByCode(string funcNo)
        {
            string returnVa = string.Empty;
            switch (funcNo)
            {
                case "0201":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08";
                    m_IgnoreRows = 2;
                    break;
                case "0202":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19";
                    m_IgnoreRows = 2;
                    break;
                case "0203":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17";
                    m_IgnoreRows = 2;
                    break;
                case "0204":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07";
                    m_IgnoreRows = 2;
                    break;
                case "0205":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13";
                    m_IgnoreRows = 2;
                    break;
                case "0206":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07";
                    m_IgnoreRows = 2;
                    break;
                case "0207":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07";
                    m_IgnoreRows = 2;
                    break;
                case "0208":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05";
                    m_IgnoreRows = 2;
                    break;
                case "0209":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06";
                    m_IgnoreRows = 2;
                    break;
                case "0210":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15";
                    m_IgnoreRows = 2;
                    break;
                case "0211":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04";
                    m_IgnoreRows = 2;
                    break;
                case "0212":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06";
                    m_IgnoreRows = 2;
                    break;
                case "0213":
                    m_Fields = "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13";
                    m_IgnoreRows = 2;
                    break;
                case "020111":
                    m_Fields = "";
                    m_IgnoreRows = 2;
                    break;
                default:
                    m_Fields = "";
                    break;
            }
            /*
0201	工业总产值及主要产品产量月报
0202	主要生产技术经济指标表
0203	主要工业产品销售量、库存、订货
0204	分行业工业总产值
0205	有色金属企业主要经营指标
0206	原材料、燃料、动力购进价格调查月报表
0207	工业品出厂价格月报
0208	钼产品主要客户销售情况
0209	出口配额获授情况
0210	主要产品生产、销售、库存完成情况
0211	财务状况
0212	人数统计报表
0213	金钼股份矿业分公司生产快报
*/
   
            return returnVa;
        }
        

        #region 数据导入方法

        /// <summary>
        /// 导入Excel数据
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportXlsData(string funcNo, string tableName, string filterSQL, DataSet xlsDs)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null || xlsDs.Tables[0].Columns.Count < 1)
            {
                sHtml.Append("操作失败：获取 Excel 数据集错误！<br>");
                sHtml.Append("解决办法：请确保您想要导入的Excel文件符合导入数据的标准格式，然后再试。<br>");
                sHtml.Append("如果是系统自动生成的Excel文件，请用Excel打开后，另存为“Microsoft Office Excel 工作薄(*.xls)”格式，然后再试。<br>");
                sHtml.Append("注：导入数据的 Excel表头的标准格式为系统导出的Excel文件的表头格式。");
                this.LiteralMsg.Text = sHtml.ToString();
                return;
            }

            // 开始导入
            int debugRow = 0;

            try
            {
                string totalCount = xlsDs.Tables[0].Rows.Count.ToString(); // 总数
                int iCol = xlsDs.Tables[0].Columns.Count;
                int iPassed = 0; // 成功数   
                int iUnPass = 0; // 失败数
                int iHuLue = 0; // 忽略数
                int iRe = 0;//重复数
                // 忽略前二行
                for (int i = 0; i < xlsDs.Tables[0].Rows.Count; i++)
                {
                    debugRow++;

                    // 判断某条记录是否重复,存在重复的忽略
                    StringBuilder selSql = new StringBuilder();
                    selSql.Append("SELECT COUNT(*) FROM " + tableName + " WHERE " + filterSQL + " ");
                    selSql.Append("AND Fileds01=@Fileds01 AND Fileds02=@Fileds02 AND Fileds03=@Fileds03 AND Fileds04=@Fileds04 AND Fileds05=@Fileds05 ");
                    SqlParameter[] sParams = {
					new SqlParameter("@Fileds01", SqlDbType.VarChar,100),
                    new SqlParameter("@Fileds02", SqlDbType.VarChar,100),
					new SqlParameter("@Fileds03", SqlDbType.VarChar,100),
					new SqlParameter("@Fileds04", SqlDbType.VarChar,60),
					new SqlParameter("@Fileds05", SqlDbType.VarChar,60)
                    };
                    sParams[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString());
                    sParams[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString());
                    sParams[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString());
                    sParams[3].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString());
                    if (iCol > 4) { 
                        sParams[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()); } 
                    else { 
                        sParams[4].Value = ""; 
                    }
                    // INSERT 记录
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("INSERT INTO RPT_Contents(");
                    strSql.Append("RptID,UserID,");
                    strSql.Append("Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19,Fileds20,Fileds21,Fileds22,Fileds23,Fileds24,Fileds25");
                    strSql.Append(") VALUES(");
                    strSql.Append("@RptID,@UserID,");
                    strSql.Append("@Fileds01,@Fileds02,@Fileds03,@Fileds04,@Fileds05,@Fileds06,@Fileds07,@Fileds08,@Fileds09,@Fileds10,@Fileds11,@Fileds12,@Fileds13,@Fileds14,@Fileds15,@Fileds16,@Fileds17,@Fileds18,@Fileds19,@Fileds20,@Fileds21,@Fileds22,@Fileds23,@Fileds24,@Fileds25");
                    strSql.Append(");select @@IDENTITY");
                    SqlParameter[] parameters = {
					new SqlParameter("@RptID", SqlDbType.Int,4),
                    new SqlParameter("@UserID", SqlDbType.Int,4),

					new SqlParameter("@Fileds01", SqlDbType.VarChar,100),
                    new SqlParameter("@Fileds02", SqlDbType.VarChar,100),
					new SqlParameter("@Fileds03", SqlDbType.VarChar,100),
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
                    new SqlParameter("@Fileds25", SqlDbType.VarChar,60)
                   };
                    parameters[0].Value = this.m_ObjID;
                    parameters[1].Value = this.m_UserID;

                    parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString());
                    parameters[3].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString());
                    parameters[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString());
                    parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString());
                    parameters[6].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString());
                    parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString());
                    if (iCol > 6) { parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][6].ToString()); } else { parameters[8].Value = ""; }
                    if (iCol > 7) { parameters[9].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); } else { parameters[9].Value = ""; }
                    if (iCol > 8) { parameters[10].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); } else { parameters[10].Value = ""; }
                    if (iCol > 9) { parameters[11].Value = StrTrim(xlsDs.Tables[0].Rows[i][9].ToString()); } else { parameters[11].Value = ""; }

                    if (iCol > 10) { parameters[12].Value = StrTrim(xlsDs.Tables[0].Rows[i][10].ToString()); } else { parameters[12].Value = ""; }
                    if (iCol > 11) { parameters[13].Value = StrTrim(xlsDs.Tables[0].Rows[i][11].ToString()); } else { parameters[13].Value = ""; }
                    if (iCol > 12) { parameters[14].Value = StrTrim(xlsDs.Tables[0].Rows[i][12].ToString()); } else { parameters[14].Value = ""; }
                    if (iCol > 13) { parameters[15].Value = StrTrim(xlsDs.Tables[0].Rows[i][13].ToString()); } else { parameters[15].Value = ""; }
                    if (iCol > 14) { parameters[16].Value = StrTrim(xlsDs.Tables[0].Rows[i][14].ToString()); } else { parameters[16].Value = ""; }
                    if (iCol > 15) { parameters[17].Value = StrTrim(xlsDs.Tables[0].Rows[i][15].ToString()); } else { parameters[17].Value = ""; }
                    if (iCol > 16) { parameters[18].Value = StrTrim(xlsDs.Tables[0].Rows[i][16].ToString()); } else { parameters[18].Value = ""; }
                    if (iCol > 17) { parameters[19].Value = StrTrim(xlsDs.Tables[0].Rows[i][17].ToString()); } else { parameters[19].Value = ""; }
                    if (iCol > 18) { parameters[20].Value = StrTrim(xlsDs.Tables[0].Rows[i][18].ToString()); } else { parameters[20].Value = ""; }
                    if (iCol > 19) { parameters[21].Value = StrTrim(xlsDs.Tables[0].Rows[i][19].ToString()); } else { parameters[21].Value = ""; }

                    if (iCol > 20) { parameters[22].Value = StrTrim(xlsDs.Tables[0].Rows[i][20].ToString()); } else { parameters[22].Value = ""; }
                    if (iCol > 21) { parameters[23].Value = StrTrim(xlsDs.Tables[0].Rows[i][21].ToString()); } else { parameters[23].Value = ""; }
                    if (iCol > 22) { parameters[24].Value = StrTrim(xlsDs.Tables[0].Rows[i][22].ToString()); } else { parameters[24].Value = ""; }
                    if (iCol > 23) { parameters[25].Value = StrTrim(xlsDs.Tables[0].Rows[i][23].ToString()); } else { parameters[25].Value = ""; }
                    if (iCol > 24) { parameters[26].Value = StrTrim(xlsDs.Tables[0].Rows[i][24].ToString()); } else { parameters[26].Value = ""; }
                    //if (iCol > 25) { parameters[27].Value = StrTrim(xlsDs.Tables[0].Rows[i][25].ToString()); } else { parameters[27].Value = ""; }

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
                if (inStr.Trim().Length > 50) { return inStr.Trim().Substring(0, 50); } else { return inStr.Trim(); }
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
