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
    /// 数据导入
    /// </summary>
    public partial class DataImport : System.Web.UI.Page
    {
        StringBuilder toop = new StringBuilder();
        private string m_SourceUrl;
        private string m_UserID; // 当前登录的操作用户编号

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
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }
            else
            {
                SetOpratetionAction("信息导入");
            }
            //}
        }

        #region

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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Ysl9lcXf6JuzBkRL1yQD48cmhxCD5exHudvJr7ExPl6SnOYhiJLFhhdlZx1OzuA1vCf.shtml';</script>");
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
        /// 设置操作行为
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            switch (getRealFun(m_FuncCode))
            {
                case "020101": // 民政部门婚姻登记信息表导入
                    funcName = "民政部门婚姻登记信息表导入";
                    break;
                case "020102": // 卫生部门住院分娩实名登记信息表导入
                    funcName = "卫生部门住院分娩实名登记信息表导入";
                    break;
                case "020103": // 公安部门新生人口报户人员登记信息导入
                    funcName = "公安部门新生人口报户人员登记信息导入";
                    break;
                case "020104": // 公安部门办理居住证人员登记信息表导入
                    funcName = "公安部门办理居住证人员登记信息表导入";
                    break;
                case "020105": // 公安部门迁移人员登记信息表导入
                    funcName = "公安部门迁移人员登记信息表导入";
                    break;
                case "020106": // 计生部门人口基本情况统计信息表导入
                    funcName = "计生部门人口基本情况统计信息表导入";
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = oprateName + " &gt;&gt; " + funcName + "：";
        }

        /// <summary>
        /// 显示提示信息
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
                    this.LabelMsg.Text = "请首先选择浏览要导入的 Excel 数据文件！导入格式以到处的格式为准。";
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
            // 导入文件
            string oprateDate = string.Empty;
            if (Session["FileName"] != null)
            {
                m_UpFileName = Session["FileName"].ToString();

                try
                {
                    switch (getRealFun(this.m_FuncCode))
                    {
                        case "020101"://民政部门婚姻登记信息表
                            ImportHYDJ("020101", ReadXlsToDs(m_UpFileName)); // 人员基本信息导入
                            DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "数据导入", "用户于 " + DateTime.Now.ToString() + " 导入人员基本信息");
                            break;
                        case "020102"://卫生部门住院分娩实名登记信息表 
                            ImportFMDJ("020102", ReadXlsToDs(m_UpFileName)); // 人员基本信息导入
                            DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "数据导入", "用户于 " + DateTime.Now.ToString() + " 导入人员基本信息");
                            break;
                        case "020103"://公安部门新生人口报户人员登记信息表
                            ImportXSBH("020103", ReadXlsToDs(m_UpFileName)); // 人员基本信息导入
                            DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "数据导入", "用户于 " + DateTime.Now.ToString() + " 导入人员基本信息");
                            break;
                        case "020104"://公安部门办理居住证人员登记信息表
                            ImportJZDJ("020104", ReadXlsToDs(m_UpFileName)); // 人员基本信息导入
                            DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "数据导入", "用户于 " + DateTime.Now.ToString() + " 导入人员基本信息");
                            break;
                        case "020105"://公安部门办理居住证人员登记信息表
                            ImportQYDJ("020105", ReadXlsToDs(m_UpFileName)); // 人员基本信息导入
                            DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "数据导入", "用户于 " + DateTime.Now.ToString() + " 导入人员基本信息");
                            break;
                        case "020106"://计生部门人口基本情况统计信息表
                            ImportRKTJ("020106", ReadXlsToDs(m_UpFileName)); // 人员基本信息导入
                            DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "数据导入", "用户于 " + DateTime.Now.ToString() + " 导入人员基本信息");
                            break;
                        default:

                            break;
                    }
                }
                catch (Exception ex)
                {
                    this.LabelMsg.Text = "操作失败：<br/><br/>" + ex.Message; // " + m_SqlParams + "
                }
            }
            else
            {
                this.LabelMsg.Text = "操作失败：操作超时，请点击“导入”按钮重试！";
            }
        }

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
        /// <summary>
        /// 婚姻状况
        /// </summary>
        /// <param name="strMarrTypeCN"></param>
        /// <returns></returns>
        private string getRealMarrType(string strMarrTypeCN)
        {
            string strReturn = string.Empty;
            switch (strMarrTypeCN)
            {
                case "未婚":
                    strReturn = "10";
                    break;
                case "已婚":
                    strReturn = "20";
                    break;
                case "初婚":
                    strReturn = "21";
                    break;
                case "再婚":
                    strReturn = "22";
                    break;
                case "复婚":
                    strReturn = "23";
                    break;
                case "离婚":
                    strReturn = "40";
                    break;
                default:
                    strReturn = "90";
                    break;
            }
            return strReturn;
        }
        /// <summary>
        /// 亲属关系
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
        ///迁入  迁出
        /// </summary>
        /// <param name="strMarrTypeCN"></param>
        /// <returns></returns>
        private string getMoveType(string strMoveTypeCN)
        {
            string strReturn = string.Empty;
            switch (strMoveTypeCN)
            {
                case "迁入":
                    strReturn = "0";
                    break;
                case "迁出":
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

        #region 数据导入
        /// <summary>
        /// 民政部门婚姻登记信息表导入
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportHYDJ(string strFunNo, DataSet xlsDs)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null || xlsDs.Tables[0].Columns.Count < 1)
            {
                sHtml.Append("操作失败：获取 Excel 数据集错误！<br>");
                sHtml.Append("解决办法：请确保您想要导入的Excel文件符合导入数据的标准格式，然后再试。<br>");
                sHtml.Append("如果是系统自动生成的Excel文件，请用Excel打开后，另存为“Microsoft Office Excel 工作薄(*.xls)”格式，然后再试。<br>");
                sHtml.Append("导入数据的 Excel表头的标准格式为系统导出的相应文件的格式。");
                this.LabelMsg.Text = sHtml.ToString();
                return;
            }
            // 已经存在的记录
            m_SqlParams = "SELECT [CommID], [NationalID] FROM [Sys_QueueBiz]";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // 开始导入
            string NationalID = string.Empty;
            string userName = string.Empty;
            string totalCount = xlsDs.Tables[0].Rows.Count.ToString(); // 总数
            int iPassed = 0; // 成功数   
            int iUnPass = 0; // 失败数
            int iHuLue = 0; // 忽略数
            for (int i = 2; i < xlsDs.Tables[0].Rows.Count; i++)
            {
                NationalID = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString());
                userName = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString());
                if (!String.IsNullOrEmpty(StrTrim(xlsDs.Tables[0].Rows[i][1].ToString()))) //验证年龄列是否为数字及空值
                {
                    StringBuilder strSql = new StringBuilder();
                    string CommID = CheckHasData(strFunNo, NationalID);
                    if (String.IsNullOrEmpty(CommID))
                    {
                        // INSERT 记录
                        strSql.Append("INSERT INTO Sys_QueueBiz(");
                        strSql.Append("FunNo,ResponseUserID, BabyDateSH, [NationalName], [NationalID],  NationalBirthDay, [MarraigeType],CurrentAddress,NationalWorkUint,WifeName,WifeID, WifeBirthDay,WifeMarraigeType,WifeAddress,WifeWorkUint)");
                        strSql.Append(" VALUES(");
                        strSql.Append("'" + strFunNo + "',"+this.m_UserID+",@BabyDateSH,@NationalName,@NationalID,@NationalBirthDay,@MarraigeType,@CurrentAddress,@NationalWorkUint,@WifeName,@WifeID,@WifeBirthDay,@WifeMarraigeType,@WifeAddress,@WifeWorkUint)");
                        strSql.Append(";select @@IDENTITY");
                    }
                    else
                    {
                        // update 记录
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
                    parameters[0].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][0].ToString())); // 登记日期
                    parameters[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString()); // 男方姓名
                    parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString()); // 身份证号
                    parameters[3].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][3].ToString())); // 出生日期
                    parameters[4].Value = getRealMarrType(StrTrim(xlsDs.Tables[0].Rows[i][4].ToString())); // 婚姻状况
                    parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString()); // 家庭地址
                    parameters[6].Value = StrTrim(xlsDs.Tables[0].Rows[i][6].ToString()); // 工作单位
                    parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); // 女方姓名
                    parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); // 身份证号
                    parameters[9].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][9].ToString())); // 出生日期
                    parameters[10].Value = getRealMarrType(StrTrim(xlsDs.Tables[0].Rows[i][10].ToString())); // 婚姻状况
                    parameters[11].Value = StrTrim(xlsDs.Tables[0].Rows[i][11].ToString()); // 家庭地址
                    parameters[12].Value = StrTrim(xlsDs.Tables[0].Rows[i][12].ToString()); // 工作单位
                    // 执行操作
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
                ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + toop.ToString() + "'+'员工的信息被忽略导入');</script>");
            }
            m_Dt.Dispose();
            xlsDs.Dispose();
            sHtml.Append("<br><br>数据文件共有" + totalCount + "信息；成功导入了" + iPassed.ToString() + "条信息，失败了" + iUnPass.ToString() + "条信息，直接忽略的信息 " + iHuLue + "条。");
            this.LabelMsg.Text = sHtml.ToString();
        }

        /// <summary>
        /// 卫生部门住院分娩实名登记信息表导入
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportFMDJ(string strFunNo, DataSet xlsDs)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null || xlsDs.Tables[0].Columns.Count < 1)
            {
                sHtml.Append("操作失败：获取 Excel 数据集错误！<br>");
                sHtml.Append("解决办法：请确保您想要导入的Excel文件符合导入数据的标准格式，然后再试。<br>");
                sHtml.Append("如果是系统自动生成的Excel文件，请用Excel打开后，另存为“Microsoft Office Excel 工作薄(*.xls)”格式，然后再试。<br>");
                sHtml.Append("导入数据的 Excel表头的标准格式为系统导出的相应文件的格式。");
                this.LabelMsg.Text = sHtml.ToString();
                return;
            }
            // 已经存在的记录
            m_SqlParams = "SELECT [CommID], [NationalID] FROM [Sys_QueueBiz]";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // 开始导入
            string NationalID = string.Empty;
            string userName = string.Empty;
            string totalCount = xlsDs.Tables[0].Rows.Count.ToString(); // 总数
            int iPassed = 0; // 成功数   
            int iUnPass = 0; // 失败数
            int iHuLue = 0; // 忽略数
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
                        // INSERT 记录
                        strSql.Append("INSERT INTO Sys_QueueBiz(");
                        strSql.Append("FunNo, ResponseUserID,[NationalName], NationalWorkUint, CurrentAddress,[NationalID],WifeName,WifeWorkUint,WifeAddress,WifeID,BabyIndex,BabySex, [BabyBirthDay],BabyName,BabyBirthID, [BabyDate] )");
                        strSql.Append(" VALUES(");
                        strSql.Append("'" + strFunNo + "',"+this.m_UserID+",@NationalName, @NationalWorkUint, @CurrentAddress,@NationalID,@WifeName,@WifeWorkUint,@WifeAddress,@WifeID,@BabyIndex,@BabySex, @BabyBirthDay,@BabyName,@BabyBirthID, @BabyDate )");
                        strSql.Append(";select @@IDENTITY");
                    }
                    else
                    {
                        // update 记录
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
                    parameters[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString()); // 丈夫姓名
                    parameters[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString()); // 工作单位
                    parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString()); // 家庭住址
                    parameters[3].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString()); // 身份证号
                    parameters[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()); // 产妇姓名
                    parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString()); // 工作单位
                    parameters[6].Value = StrTrim(xlsDs.Tables[0].Rows[i][6].ToString()); // 家庭住址
                    parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); // 身份证号
                    parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); // 新生儿 胎次
                    parameters[9].Value = StrTrim(xlsDs.Tables[0].Rows[i][9].ToString()); // 性别
                    parameters[10].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][10].ToString())); // 出生日期
                    parameters[11].Value = StrTrim(xlsDs.Tables[0].Rows[i][11].ToString()); // 姓名
                    parameters[12].Value = StrTrim(xlsDs.Tables[0].Rows[i][12].ToString()); // 证号
                    parameters[13].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][13].ToString())); // 办证日期
                    // 执行操作
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
                ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + toop.ToString() + "'+'员工的信息被忽略导入');</script>");
            }
            m_Dt.Dispose();
            xlsDs.Dispose();
            sHtml.Append("<br><br>数据文件共有" + totalCount + "信息；成功导入了" + iPassed.ToString() + "条信息，失败了" + iUnPass.ToString() + "条信息，直接忽略的信息 " + iHuLue + "条。");
            this.LabelMsg.Text = sHtml.ToString();
        }


        /// <summary>
        /// 公安部门新生人口报户人员登记信息表导入
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportXSBH(string strFunNo, DataSet xlsDs)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null || xlsDs.Tables[0].Columns.Count < 1)
            {
                sHtml.Append("操作失败：获取 Excel 数据集错误！<br>");
                sHtml.Append("解决办法：请确保您想要导入的Excel文件符合导入数据的标准格式，然后再试。<br>");
                sHtml.Append("如果是系统自动生成的Excel文件，请用Excel打开后，另存为“Microsoft Office Excel 工作薄(*.xls)”格式，然后再试。<br>");
                sHtml.Append("导入数据的 Excel表头的标准格式为系统导出的相应文件的格式。");
                this.LabelMsg.Text = sHtml.ToString();
                return;
            }
            // 已经存在的记录
            m_SqlParams = "SELECT [CommID], [NationalID] FROM [Sys_QueueBiz]";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // 开始导入
            string NationalID = string.Empty;
            string userName = string.Empty;
            string totalCount = xlsDs.Tables[0].Rows.Count.ToString(); // 总数
            int iPassed = 0; // 成功数   
            int iUnPass = 0; // 失败数
            int iHuLue = 0; // 忽略数
            for (int i = 3; i < xlsDs.Tables[0].Rows.Count; i++)
            {
                NationalID = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString());
                userName = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString());
                if (!String.IsNullOrEmpty(StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()))) //验证年龄列是否为数字及空值
                {
                    StringBuilder strSql = new StringBuilder();
                    string CommID = CheckHasData(strFunNo, NationalID);
                    if (String.IsNullOrEmpty(CommID))
                    {
                        // INSERT 记录
                        strSql.Append("INSERT INTO Sys_QueueBiz(");
                        strSql.Append("FunNo,ResponseUserID, BabyDateSH,NationalName, NationalWorkUint, CurrentAddress,NationalID,WifeName,WifeWorkUint,WifeAddress,WifeID,BabyName,BabySex,BabyAddress,BabyID,BabyBirthDay)");
                        strSql.Append(" VALUES(");
                        strSql.Append("'" + strFunNo + "',"+this.m_UserID+",@BabyDateSH,@NationalName, @NationalWorkUint, @CurrentAddress,@NationalID,@WifeName,@WifeWorkUint,@WifeAddress,@WifeID,@BabyName,@BabySex,@BabyAddress,@BabyID,@BabyBirthDay)");
                        strSql.Append(";select @@IDENTITY");
                    }
                    else
                    {
                        // update 记录
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
                    parameters[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString()); // 丈夫姓名
                    parameters[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString()); // 工作单位
                    parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString()); // 家庭住址
                    parameters[3].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString()); // 身份证号
                    parameters[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()); // 产妇姓名
                    parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString()); // 工作单位
                    parameters[6].Value = StrTrim(xlsDs.Tables[0].Rows[i][6].ToString()); // 家庭住址
                    parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); // 身份证号
                    parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); // 新生儿 胎次
                    parameters[9].Value = StrTrim(xlsDs.Tables[0].Rows[i][9].ToString()); // 性别
                    parameters[10].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][10].ToString())); // 出生日期
                    parameters[11].Value = StrTrim(xlsDs.Tables[0].Rows[i][11].ToString()); // 姓名
                    parameters[12].Value = StrTrim(xlsDs.Tables[0].Rows[i][12].ToString()); // 证号
                    parameters[13].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][13].ToString())); // 办证日期
                    // 执行操作
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
                ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + toop.ToString() + "'+'员工的信息被忽略导入');</script>");
            }
            m_Dt.Dispose();
            xlsDs.Dispose();
            sHtml.Append("<br><br>数据文件共有" + totalCount + "信息；成功导入了" + iPassed.ToString() + "条信息，失败了" + iUnPass.ToString() + "条信息，直接忽略的信息 " + iHuLue + "条。");
            this.LabelMsg.Text = sHtml.ToString();
        }

        /// <summary>
        /// 公安部门办理居住证人员登记信息表导入
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportJZDJ(string strFunNo, DataSet xlsDs)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null || xlsDs.Tables[0].Columns.Count < 1)
            {
                sHtml.Append("操作失败：获取 Excel 数据集错误！<br>");
                sHtml.Append("解决办法：请确保您想要导入的Excel文件符合导入数据的标准格式，然后再试。<br>");
                sHtml.Append("如果是系统自动生成的Excel文件，请用Excel打开后，另存为“Microsoft Office Excel 工作薄(*.xls)”格式，然后再试。<br>");
                sHtml.Append("导入数据的 Excel表头的标准格式为系统导出的相应文件的格式。");
                this.LabelMsg.Text = sHtml.ToString();
                return;
            }
            // 已经存在的记录
            m_SqlParams = "SELECT [CommID], [NationalID] FROM [Sys_QueueBiz]";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // 开始导入
            string NationalID = string.Empty;
            string userName = string.Empty;
            string totalCount = xlsDs.Tables[0].Rows.Count.ToString(); // 总数
            int iPassed = 0; // 成功数   
            int iUnPass = 0; // 失败数
            int iHuLue = 0; // 忽略数
            for (int i = 3; i < xlsDs.Tables[0].Rows.Count; i++)
            {
                NationalID = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString());
                userName = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString());
                if (!String.IsNullOrEmpty(StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()))) //验证年龄列是否为数字及空值
                {
                    StringBuilder strSql = new StringBuilder();
                    string CommID = CheckHasData(strFunNo, NationalID);
                    if (String.IsNullOrEmpty(CommID))
                    {
                        // INSERT 记录
                        strSql.Append("INSERT INTO Sys_QueueBiz(");
                        strSql.Append("FunNo, ResponseUserID,NationalName,NationalSex,NationalID, NationalBirthDay, NationalWorkUint, CurrentAddress,MarraigeType,NationalBY,YZName,YZTel,RelationType,WifeName,WifeSex,WifeID,WifeBirthDay,WifeBY,BoysNum,GirlsNum,MarraigeID)");
                        strSql.Append(" VALUES(");
                        strSql.Append("'" + strFunNo + "',"+this.m_UserID+",@NationalName,@NationalSex,@NationalID, @NationalBirthDay, @NationalWorkUint, @CurrentAddress,@MarraigeType,@NationalBY,@YZName,@YZTel,@RelationType,@WifeName,@WifeSex,@WifeID,@WifeBirthDay,@WifeBY,@BoysNum,@GirlsNum,@MarraigeID)");
                        strSql.Append(";select @@IDENTITY");
                    }
                    else
                    {
                        // update 记录
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
                    parameters[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString()); // 登记者姓名
                    parameters[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString()); // 性别
                    parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString()); // 身份证号
                    parameters[3].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][3].ToString())); // 出生年月
                    parameters[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()); // 户籍地址
                    parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString()); // 现居住地址
                    parameters[6].Value = getRealMarrType(StrTrim(xlsDs.Tables[0].Rows[i][6].ToString())); // 婚姻状况
                    parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); // 采取避孕措施情况
                    parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); // 出租业主姓名
                    parameters[9].Value = StrTrim(xlsDs.Tables[0].Rows[i][9].ToString()); // 出租业主联系电话
                    parameters[10].Value = getRelationType(StrTrim(xlsDs.Tables[0].Rows[i][10].ToString())); // 与本人关系
                    parameters[11].Value = StrTrim(xlsDs.Tables[0].Rows[i][11].ToString()); // 姓名
                    parameters[12].Value = StrTrim(xlsDs.Tables[0].Rows[i][12].ToString()); // 性别
                    parameters[13].Value = StrTrim(xlsDs.Tables[0].Rows[i][13].ToString()); // 身份证号码
                    parameters[14].Value = ToDateTime(StrTrim(xlsDs.Tables[0].Rows[i][14].ToString())); // 出生年月
                    parameters[15].Value = StrTrim(xlsDs.Tables[0].Rows[i][15].ToString()); // 采取避孕措施情况
                    parameters[16].Value = NumberStrTrim(StrTrim(xlsDs.Tables[0].Rows[i][16].ToString())); // 现有家庭子女数  男
                    parameters[17].Value = NumberStrTrim(StrTrim(xlsDs.Tables[0].Rows[i][17].ToString())); // 现有家庭子女数  女
                    parameters[18].Value = StrTrim(xlsDs.Tables[0].Rows[i][18].ToString()); // 婚育证明编号

                    // 执行操作
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
                ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + toop.ToString() + "'+'员工的信息被忽略导入');</script>");
            }
            m_Dt.Dispose();
            xlsDs.Dispose();
            sHtml.Append("<br><br>数据文件共有" + totalCount + "信息；成功导入了" + iPassed.ToString() + "条信息，失败了" + iUnPass.ToString() + "条信息，直接忽略的信息 " + iHuLue + "条。");
            this.LabelMsg.Text = sHtml.ToString();
        }


        /// <summary>
        /// 公安部门办理居住证人员登记信息表导入
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportQYDJ(string strFunNo, DataSet xlsDs)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null || xlsDs.Tables[0].Columns.Count < 1)
            {
                sHtml.Append("操作失败：获取 Excel 数据集错误！<br>");
                sHtml.Append("解决办法：请确保您想要导入的Excel文件符合导入数据的标准格式，然后再试。<br>");
                sHtml.Append("如果是系统自动生成的Excel文件，请用Excel打开后，另存为“Microsoft Office Excel 工作薄(*.xls)”格式，然后再试。<br>");
                sHtml.Append("导入数据的 Excel表头的标准格式为系统导出的相应文件的格式。");
                this.LabelMsg.Text = sHtml.ToString();
                return;
            }
            // 已经存在的记录
            m_SqlParams = "SELECT [CommID], [MoveInCardID] FROM [Sys_Move]";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // 开始导入
            string MoveInCardID, MoveOutCardID = string.Empty;
            string userName = string.Empty;
            string totalCount = xlsDs.Tables[0].Rows.Count.ToString(); // 总数
            int iPassed = 0; // 成功数   
            int iUnPass = 0; // 失败数
            int iHuLue = 0; // 忽略数
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
                        // INSERT 记录
                        strSql.Append("INSERT INTO Sys_Move(");
                        strSql.Append("[MoveInDate], [MoveInName], [MoveInSex], [MoveInCardID], [MoveInAddress], [MoveInAddressTo], [MoveOutDate], [MoveOutName], [MoveOutSex],[MoveOutCardID], [MoveOutAddress], [MoveOutAddressTo])");
                        strSql.Append(" VALUES(");
                        strSql.Append("@MoveInDate, @MoveInName, @MoveInSex, @MoveInCardID, @MoveInAddress, @MoveInAddressTo, @MoveOutDate, @MoveOutName, @MoveOutSex,@MoveOutCardID, @MoveOutAddress, @MoveOutAddressTo)");
                        strSql.Append(";select @@IDENTITY");
                    }
                    else
                    {
                        // update 记录
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
                    parameters[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString()); // 迁入 日期
                    parameters[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString()); // 姓名
                    parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString()); // 性别
                    parameters[3].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString()); // 身份证号
                    parameters[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()); // 迁出地址
                    parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString()); // 迁入地址
                    parameters[6].Value = StrTrim(xlsDs.Tables[0].Rows[i][6].ToString()); // 迁入 日期
                    parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); // 姓名
                    parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); // 性别
                    parameters[9].Value = StrTrim(xlsDs.Tables[0].Rows[i][9].ToString()); // 身份证号
                    parameters[10].Value = StrTrim(xlsDs.Tables[0].Rows[i][10].ToString()); // 迁出地址
                    parameters[11].Value = StrTrim(xlsDs.Tables[0].Rows[i][11].ToString()); // 迁入地址

                    // 执行操作
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
                ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + toop.ToString() + "'+'员工的信息被忽略导入');</script>");
            }
            m_Dt.Dispose();
            xlsDs.Dispose();
            sHtml.Append("<br><br>数据文件共有" + totalCount + "信息；成功导入了" + iPassed.ToString() + "条信息，失败了" + iUnPass.ToString() + "条信息，直接忽略的信息 " + iHuLue + "条。");
            this.LabelMsg.Text = sHtml.ToString();
        }

        /// <summary>
        /// 计生部门人口基本情况统计信息表
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportRKTJ(string strFunNo, DataSet xlsDs)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null || xlsDs.Tables[0].Columns.Count < 1)
            {
                sHtml.Append("操作失败：获取 Excel 数据集错误！<br>");
                sHtml.Append("解决办法：请确保您想要导入的Excel文件符合导入数据的标准格式，然后再试。<br>");
                sHtml.Append("如果是系统自动生成的Excel文件，请用Excel打开后，另存为“Microsoft Office Excel 工作薄(*.xls)”格式，然后再试。<br>");
                sHtml.Append("导入数据的 Excel表头的标准格式为系统导出的相应文件的格式。");
                this.LabelMsg.Text = sHtml.ToString();
                return;
            }
            // 已经存在的记录
            m_SqlParams = "SELECT [CommID], [UintName] FROM [Sys_PeopleStat]";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // 开始导入
            string CommID = string.Empty;
            string UintName = string.Empty;
            string totalCount = xlsDs.Tables[0].Rows.Count.ToString(); // 总数
            int iPassed = 0; // 成功数   
            int iUnPass = 0; // 失败数
            int iHuLue = 0; // 忽略数
            for (int i = 3; i < xlsDs.Tables[0].Rows.Count; i++)
            {
                UintName = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString());
                CommID = CheckHasData_P(UintName);
                if (!String.IsNullOrEmpty(StrTrim(xlsDs.Tables[0].Rows[i][0].ToString())) || !String.IsNullOrEmpty(StrTrim(xlsDs.Tables[0].Rows[i][2].ToString()))) //验证部门字段是否为空
                {
                    StringBuilder strSql = new StringBuilder();
                    if (String.IsNullOrEmpty(CommID))
                    {
                        // INSERT 记录
                        strSql.Append("INSERT INTO Sys_PeopleStat(");
                        strSql.Append("UintName,CZRenKou,YLFunNv,YHFunNv,BYRenShu,CSRenKou,SWRenKou,LCRenKou,LRRenKou,Remarks)");
                        strSql.Append(" VALUES(");
                        strSql.Append("@UintName,@CZRenKou,@YLFunNv,@YHFunNv,@BYRenShu,@CSRenKou,@SWRenKou,@LCRenKou,@LRRenKou,@Remarks)");
                        strSql.Append(";select @@IDENTITY");
                    }
                    else
                    {
                        // update 记录
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
                    parameters[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][0].ToString()); // 单位名称
                    parameters[1].Value = StrTrim(xlsDs.Tables[0].Rows[i][1].ToString()); // 期末常住人口
                    parameters[2].Value = StrTrim(xlsDs.Tables[0].Rows[i][2].ToString()); // 期末孕龄妇女人数
                    parameters[3].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString()); // 期已孕龄妇女人数
                    parameters[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString()); // 采取各种避孕措施人数
                    parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString()); // 出生人数
                    parameters[6].Value = StrTrim(xlsDs.Tables[0].Rows[i][6].ToString()); // 死亡人数
                    parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); // 流出人口
                    parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); // 流入人口
                    parameters[9].Value = StrTrim(xlsDs.Tables[0].Rows[i][9].ToString()); // 备注

                    // 执行操作
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
                ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + toop.ToString() + "'+'员工的信息被忽略导入');</script>");
            }
            m_Dt.Dispose();
            xlsDs.Dispose();
            sHtml.Append("<br><br>数据文件共有" + totalCount + "信息；成功导入了" + iPassed.ToString() + "条信息，失败了" + iUnPass.ToString() + "条信息，直接忽略的信息 " + iHuLue + "条。");
            this.LabelMsg.Text = sHtml.ToString();
        }
        #endregion

        /// <summary>
        /// 检测迁移库中是否存在该登记者信息
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
        /// 检测人口统计库中是否存在该登记者信息
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
        /// 检测库中是否存在该登记者信息
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
        /// 获取用户ID
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
