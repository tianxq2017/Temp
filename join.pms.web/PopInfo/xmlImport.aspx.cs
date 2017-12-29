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
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
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
                        funcName = "国家婚姻登记信息采集";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                this.LabelMsg.Text = "操作失败：<br/><br/>" + ex.Message; // " + m_SqlParams + "
            }

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
                    this.LabelMsg.Text = "请首先选择浏览要采集的解压缩后的婚姻登记xml数据文件！";
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
            else { this.LabelMsg.Text = "请选择文件！"; }
        }

        private DataTable m_AreaDt;

        /// <summary>
        /// 数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void butImport_Click(object sender, EventArgs e)
        {
            // 导入文件
            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGridShare.config");
            GetConfigParams(this.m_FuncCode, configFile, ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                Response.Write(" <script>alert('读取配置文件失败！') ;window.location.href='" + m_TargetUrl + "'</script>");
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
                            funcName = "婚姻登记信息";
                            break;
                        default:
                            break;
                    }

                    //获取行政区划数据
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
                    this.LabelMsg.Text = "操作失败：<br/><br/>" + ex.Message; // " + m_SqlParams + "
                }
            }
            else
            {
                this.LabelMsg.Text = "操作失败：操作超时，请点击“导入”按钮重试！";
            }
            this.LiteralNav.Text = "管理首页  &gt;&gt; " + funcName + "：";
        }


        #region 数据导入

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="strFunNo"></param>
        /// <param name="xlsDs"></param>
        private void ImportXMLData(string funcNo, string tableName, string filterSQL, DataSet xlsDs)
        {
            StringBuilder sHtml = new StringBuilder();

            if (xlsDs == null)
            {
                sHtml.Append("操作失败：获取XML数据集错误！<br>");
                sHtml.Append("解决办法：请将您导出的标准数据解压缩后,上传xml文件。<br>");
                sHtml.Append("系统目前仅支持国家婚姻登记信息的采集......<br>");
                this.LabelMsg.Text = sHtml.ToString();
                return;
            }
            string ReportDate = PageValidate.GetTrim(this.txtReportDate.Value);
            //if (String.IsNullOrEmpty(ReportDate))
            //{
            //    this.LabelMsg.Text = "请选择数据日期！";
            //    return;
            //}

            // 已经存在的记录
            //m_SqlParams = "SELECT * FROM " + tableName + " WHERE " + filterSQL;
            //m_Dt = new DataTable();
            //m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            // 开始导入
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

            string totalCount = impDt.Rows.Count.ToString(); // 总数
            string bizType = string.Empty;
            string certNo = string.Empty,selSql=string.Empty;
            int iCol = impDt.Columns.Count;
            int iPassed = 0; // 成功数   
            int iUnPass = 0; // 失败数
            int iHuLue = 0; // 忽略数
            // 忽略前三行
            for (int i = 2; i < impDt.Rows.Count; i++)
            {
                /*
                 <option value="IA">--结婚登记--</option>
<option value="IB">--离婚登记--</option>
<option value="ICA">--补发结婚登记证--</option>
<option value="ICB">--补发离婚登记证--</option>
<option value="ID">--撤销婚姻登记--</option>
                 */
                if (StrTrim(impDt.Rows[i][0].ToString()) == "IA" || StrTrim(impDt.Rows[i][0].ToString()) == "ICA") { bizType = "结婚登记"; }
                else { bizType = "离婚登记"; }
                // 判断是否重复数据,根据证据号码 2013/05/20 by Ysl
                certNo = StrTrim(impDt.Rows[i]["CERT_NO"].ToString());
                selSql = "SELECT COUNT(*) FROM PIS_BaseInfo WHERE FuncNo='" + funcNo + "' AND Fileds02='" + certNo + "'";

                StringBuilder strSql = new StringBuilder();
                // INSERT 记录
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
                //    // update 记录
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
业务类型,证件号码,登记时间,男方姓名,身份证号,职业,家庭地址,民族,文化程度,婚姻状况,出生日期,女方姓名,身份证号,职业,家庭地址,民族,文化程度,婚姻状况,出生日期
                */
                parameters[4].Value = bizType;
                parameters[5].Value = certNo;
                parameters[6].Value = StrTrim(impDt.Rows[i]["OP_DATE"].ToString());
                parameters[7].Value = StrTrim(impDt.Rows[i][3].ToString());
                parameters[8].Value = StrTrim(impDt.Rows[i][9].ToString());
                // 职业,家庭地址,民族,文化程度,婚姻状况,
                parameters[9].Value = GetJobName(StrTrim(impDt.Rows[i]["JOB_MAN"].ToString()));
                parameters[10].Value = StrTrim(impDt.Rows[i]["REG_DETAIL_MAN"].ToString());
                parameters[11].Value = GetFolkName(StrTrim(impDt.Rows[i]["FOLK_MAN"].ToString())); // 民族
                parameters[12].Value = GetEduName(StrTrim(impDt.Rows[i]["DEGREE_MAN"].ToString()));
                parameters[13].Value = GetMarryNa(StrTrim(impDt.Rows[i]["MARRY_STATUS_MAN"].ToString()));
                // 出生日期,女方姓名,身份证号,职业,家庭地址
                parameters[14].Value = StrTrim(impDt.Rows[i]["BIRTH_MAN"].ToString());
                parameters[15].Value = StrTrim(impDt.Rows[i][4].ToString());
                parameters[16].Value = StrTrim(impDt.Rows[i][10].ToString());
                parameters[17].Value = GetJobName(StrTrim(impDt.Rows[i]["JOB_WOMAN"].ToString()));
                parameters[18].Value = StrTrim(impDt.Rows[i]["REG_DETAIL_WOMAN"].ToString());
                //if (string.IsNullOrEmpty(StrTrim(impDt.Rows[i][28].ToString()))) { parameters[18].Value = StrTrim(impDt.Rows[i]["REG_DETAIL_WOMAN"].ToString()); }
                //else { parameters[18].Value = StrTrim(impDt.Rows[i][28].ToString()); }
                
                // 民族,文化程度,婚姻状况,出生日期,xx
                parameters[19].Value = GetFolkName(StrTrim(impDt.Rows[i]["FOLK_WOMAN"].ToString()));
                parameters[20].Value = GetEduName(StrTrim(impDt.Rows[i]["DEGREE_WOMAN"].ToString()));
                parameters[21].Value = GetMarryNa(StrTrim(impDt.Rows[i]["MARRY_STATUS_WOMAN"].ToString()));
                parameters[22].Value = StrTrim(impDt.Rows[i]["BIRTH_WOMAN"].ToString());
                parameters[23].Value = "xml";// StrTrim(impDt.Rows[i][28].ToString());
                parameters[24].Value = GetAreaCode(StrTrim(impDt.Rows[i]["REG_DETAIL_MAN"].ToString()));
                parameters[25].Value = StrTrim(impDt.Rows[i]["REG_DETAIL_MAN"].ToString());
                // 执行操作
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
            sHtml.Append("<br><br>数据文件共有" + totalCount + "信息；成功导入了" + iPassed.ToString() + "条信息，失败了" + iUnPass.ToString() + "条信息，直接忽略的信息 " + iHuLue + "条。");
            this.LabelMsg.Text = sHtml.ToString();
        }

        /// <summary>
        /// 获取区划编码
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
        /// 获取民族信息
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
        /// 获取职业名
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private string GetJobName(string jobCode) {
            string reVal="";
            switch (jobCode)
            {
                case "0":
                    reVal = "国家机关、党群组织、企业、事业单位负责人";
                    break;
                case "1/2":
                    reVal = "专业技术人员";
                    break;
                case "3":
                    reVal = "办事人员和有关人员";
                    break;
                case "4":
                    reVal = "商业、服务业人员";
                    break;
                case "5":
                    reVal = "农、林、牧、渔、水利业生产人员";
                    break;
                case "6/7/8/9":
                    reVal = "生产、运输设备操作人员及有关人员";
                    break;
                case "X":
                    reVal = "军人";
                    break;
                case "Y":
                    reVal = "不便分类的其他从业人员";
                    break;
                default:
                    reVal = jobCode;
                    break;
            }
            return reVal;
        }

        /// <summary>
        /// 获取文化程度
        /// </summary>
        /// <param name="jobCode"></param>
        /// <returns></returns>
        private string GetEduName(string code)
        {
            string reVal = "";
            switch (code)
            {
                case "10":
                    reVal = "研究生教育";
                    break;
                case "20/30":
                    reVal = "大学本科/专科教育";
                    break;
                case "40":
                    reVal = "中等职业教育";
                    break;
                case "60":
                    reVal = "普通高级中学教育";
                    break;
                case "70":
                    reVal = "初级中学教育";
                    break;
                case "80":
                    reVal = "小学教育";
                    break;
                case "90":
                    reVal = "其他";
                    break;
                default:
                    reVal = code;
                    break;
            }
            return reVal;
        }
        /*
<option value='10' >未婚</option><option value='30' >丧偶</option><option value='40' >离婚</option>
        */
        /// <summary>
        /// 获取婚姻状况
        /// </summary>
        /// <param name="jobCode"></param>
        /// <returns></returns>
        private string GetMarryNa(string code)
        {
            string reVal = "";
            switch (code)
            {
                case "10":
                    reVal = "未婚";
                    break;
                case "30":
                    reVal = "丧偶";
                    break;
                case "40":
                    reVal = "离婚";
                    break;
                default:
                    reVal = code;
                    break;
            }
            return reVal;
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
            string xlsFile = Server.MapPath("/") + "Temp/" + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + "/";//当前日期上传目录
            if (!Directory.Exists(xlsFile)) Directory.CreateDirectory(xlsFile);
            return xlsFile;
        }

        /// <summary>
        /// 读取 Excel 文件到数据集中
        /// </summary>
        /// <param name="xlsFile"></param>
        private DataSet ReadXmlToDs(string xmlFile)
        {
            try
            {
               


                if (Session["sourceFile"] == null)
                {
                    Response.Write("操作超时，请重新登录后再试！");
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
