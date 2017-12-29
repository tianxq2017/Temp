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

        private string m_UserID; // 当前登录的操作用户编号 
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
            string configFile = Server.MapPath("/includes/DataGrid.config");
            GetConfigParams(this.m_FuncCode, configFile, ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：读取配置文件失败！", m_TargetUrl, true);
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
                //    this.LiteralMsg.Text = "全员库查询数据导入要求具体到社区/村一级，请按最底层行政区划单位导入！";
                //    this.butImport.Enabled = true;
                //    return;
                //}
            }
            else {
                areaCode = m_SiteArea;
                areaName = m_SiteAreaNa; // SiteAreaName
                this.LiteralMsg.Text = "请选择数据单位！";
                this.butImport.Enabled = true;
                return;
            }
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
                    ImportXlsData(this.m_FuncCode, a_FuncInfo[0], " FuncNo='" + this.m_FuncCode + "' ", ReadXlsToDs(m_UpFileName),ReportDate, areaCode, areaName);
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
                case "1011":
                    funcName = "人口基础个案信息";
                    break;
                case "0502":
                    funcName = "人口变动出生信息";
                    break;
                case "0503":
                    funcName = "人口变动死亡信息";
                    break;
                case "0504":
                    funcName = "人口变动迁入人口";
                    break;
                case "0505":
                    funcName = "人口变动迁出信息";
                    break;
                case "0513":
                    funcName = "全员库独生子女信息";
                    break;
                case "0514":
                    funcName = "一证通独生子女信息";
                    break;
                case "0515":
                    funcName = "全员二孩生育审核信息";
                    break;
                case "0516":
                    funcName = "一证通二孩生育审核信息";
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
        private void ImportXlsData(string funcNo, string tableName, string filterSQL, DataSet xlsDs, string oprateDate, string areaCode, string areaName)
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
            StringBuilder selSql = null;
            StringBuilder strSql = null;

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
                    selSql = new StringBuilder();
                    selSql.Append("SELECT COUNT(*) FROM QYK_Persons WHERE PersonCID=@PersonCID ");
                    SqlParameter[] sParams = {
					new SqlParameter("@PersonCID", SqlDbType.VarChar,20)
                    };
                    sParams[0].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString());

                    // INSERT 记录
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
                    parameters[3].Value = StrTrim(xlsDs.Tables[0].Rows[i][3].ToString()); // 身份证号
                    parameters[4].Value = StrTrim(xlsDs.Tables[0].Rows[i][4].ToString());

                    if (iCol > 5) { parameters[5].Value = StrTrim(xlsDs.Tables[0].Rows[i][5].ToString()); } else { parameters[5].Value = ""; }
                    if (iCol > 6) { parameters[6].Value = DateStrTrim(xlsDs.Tables[0].Rows[i][6].ToString()); } else { parameters[6].Value = ""; }
                    if (iCol > 7) { parameters[7].Value = StrTrim(xlsDs.Tables[0].Rows[i][7].ToString()); } else { parameters[7].Value = ""; }
                    if (iCol > 8) { parameters[8].Value = StrTrim(xlsDs.Tables[0].Rows[i][8].ToString()); } else { parameters[8].Value = ""; }
                    if (iCol > 9) { parameters[9].Value = StrTrim(xlsDs.Tables[0].Rows[i][9].ToString()); } else { parameters[9].Value = ""; }
                    // 户人编号,姓名,性别,身份证号,小区或组名,门牌号,出生日期,民族,文化程度,户口性质,
                    // 婚姻状况,初婚日期,与户主关系,户籍所在地,现居住地,配偶姓名,配偶身份证号,父亲姓名,父亲身份证号,母亲姓名,
                    // 母亲身份证号,居住状况,落户地区
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

                    //身份证号校验 Attribs:0,默认 3,身份证校验失败
                    //if (!ValidIDCard.VerifyIDCard(parameters[3].Value.ToString()))
                    //{
                    //    parameters[30].Value = 3;
                    //    iHuLue++;
                    //}
                    //else
                    //{
                    //    parameters[30].Value = 0;
                    //}
  
                    // 执行操作
                    /*
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
                    */
                    // 不检测重复数据导入
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
                    // 执行比对 -- 在后台程序实现
                }
                xlsDs.Dispose();
                sHtml.Append("<br><br>数据文件共有" + totalCount + "信息；成功导入了" + iPassed.ToString() + "条信息，重复的信息[ " + iRe.ToString() + " ]条，失败了" + iUnPass.ToString() + "条信息。");
            }
            catch (Exception ex)
            {
                sHtml.Append("<br/>发生错误的数据行大致为[" + debugRow + "]<br/>");
                sHtml.Append(ex.Message);
            }

            this.LiteralMsg.Text = sHtml.ToString();
        }

        #endregion

        #region 区划处理

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

