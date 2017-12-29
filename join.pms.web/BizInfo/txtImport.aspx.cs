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

        private string m_UserID; // 当前登录的操作用户编号 SiteAreaName
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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/loginTemp.aspx';</script>");
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
                this.m_Format = dr[0][6].ToString();

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
                if (String.IsNullOrEmpty(fileName) || fileName.Length < 1 || !IsTxtFile(fileName))
                {
                    this.LabelMsg.Text = "请首先选择浏览要导入的 *.txt 文本文件！导入格式以最终确认的数据格式为准。";
                    return;
                }

                try
                {
                    string filePath = this.GetFilePhysicalPath() + this.GetPicPhysicalFileName(fileName);
                    this.upFiles.PostedFile.SaveAs(filePath);
                    Session["FileName"] = m_UpFileName;
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    Session["sourceFile"] = fileName; //"D:\\Temp\\20080827111448115.xls"
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
                    this.LiteralMsg.Text = "操作失败：<br/><br/>" + ex.Message; // " + m_SqlParams + "
                    this.butImport.Enabled = true;
                }
            }
            else
            {
                this.LiteralMsg.Text = "操作失败：请选择文件上传后，再点击“导入”按钮！";
                this.butImport.Enabled = true;
            }
            this.LiteralNav.Text = "管理首页  &gt;&gt; " + funcName + "：";
        }

        private void SetTxtFileImport(string funcNo,string funcName,string reportDate,string txtFile)
        {
            string beg = DateTime.Now.ToLongTimeString();
            string reportArea = string.Empty,tmpCid=string.Empty;
            string areaCode = "", areaName = string.Empty;
            string[] a_Format = this.m_Format.Split(',');
            StringBuilder sHtml = new StringBuilder();
            int nRowPer = 500000; //每次导入的最大行数  控制内存

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
                        sHtml.Append(DateTime.Now.ToString("hh:mm:ss") + "：数据集构造完毕，读取Txt文件数据……<br/>");
                        StreamReader read = new StreamReader(txtFile, Encoding.Default);
                        con.Open();
                        int nRow = 0;
                        int nCnt = 0;
                        int i = 0;
                        int errType=0;
                        string line = read.ReadLine();
                        while (line != null)
                        {
                            // 忽略第一行列头
                            if (i > 0) {
                                nRow = dt.Rows.Count;
                                if (nRow > nRowPer) //规避文件太大程序占用内存太多
                                {
                                    bulkCtrl.BatchSize = nRow;
                                    bulkCtrl.WriteToServer(dt);
                                    //bulkCtrl.Close();
                                    dt.Rows.Clear();
                                    //SetRichTextBox(DateTime.Now.ToString("hh:mm:ss") + "：已经导入[500000]条数据，开始下一批……");
                                    nCnt += nRow;
                                }
                                string[] str = line.Split('\t'); // 制表符分隔
                                if (funcNo == "020401")
                                {
                                    tmpCid = "Fileds03='" + StrTrim(str[2]) + "'";
                                    reportArea = StrTrim(str[9]);//以派出所来区分乡镇 Fileds03='" + cid.Trim() + "'
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
                                        if (errType == 0) { sHtml.Append("姓名为[ " + str[0] + " ]，身份证号为[ " + str[2] + " ]的出生上户信息存在重复的情况，被忽略……<br/>"); }
                                        else { sHtml.Append("姓名为[ " + str[0] + " ]，身份证号为[ " + str[2] + " ]的出生上户信息数据结构不正确，被忽略……<br/>"); }
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
                                            if (errType == 0) { sHtml.Append("姓名为[ " + str[0] + " ]，身份证号为[ " + str[1] + " ]的迁出人员信息存在重复的情况，被忽略……<br/>"); }
                                            else { sHtml.Append("姓名为[ " + str[0] + " ]，身份证号为[ " + str[1] + " ]的迁出人员信息数据结构不正确，被忽略……<br/>"); }
                                        } 
                                        else {
                                            if (errType == 0) { sHtml.Append("姓名为[ " + str[0] + " ]，身份证号为[ " + str[2] + " ]的迁入人员信息存在重复的情况，被忽略……<br/>"); }
                                            else { sHtml.Append("姓名为[ " + str[0] + " ]，身份证号为[ " + str[2] + " ]的迁入人员信息数据结构不正确，被忽略……<br/>"); }
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

                        sHtml.Append("<br/>" + string.Format(DateTime.Now.ToString("hh:mm:ss") + "：导入完毕\r\n开始时间:{0} ；\r\n结束时间:{1} ；\r\n导入的行数：{2}\r\n", beg, DateTime.Now.ToLongTimeString(), nCnt));
                        sHtml.Append("<br/>" + funcName + "导入完毕，共导入[ " + nCnt.ToString() + "]条）……<br/>");
                        GC.Collect();
                    }
                }
            }
            catch (Exception ex)
            {
                //SetRichTextBox(string.Format("操作失败: [StackTrace]{0}[Message]{1}\r\n", ex.StackTrace, ex.Message));
                sHtml.Append("<br/>" + string.Format("操作失败: [StackTrace]{0}[Message]{1}\r\n", ex.StackTrace, ex.Message)+"<br/>");
            }
            this.LiteralMsg.Text = sHtml.ToString();
        }
        /// <summary>
        /// 是否存在重复数据 errType:0,重复 1,数据结构错误
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
                if (address.IndexOf("派出所") < 0)
                {
                    errType = 1; // 数据结构不整齐
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
        /// 根据派出所获取乡镇行政区划
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
                    case "城关派出所":
                        areaCode = "610521100000";
                        areaName = "华州镇";
                        break;
                    case "杏林派出所":
                        areaCode = "610521101000";
                        areaName = "杏林镇";
                        break;
                    case "赤水派出所":
                        areaCode = "610521102000";
                        areaName = "赤水镇";
                        break;
                    case "侯坊派出所":
                        areaCode = "610521102000";
                        areaName = "赤水镇";
                        break;
                    case "东阳派出所":
                        areaCode = "610521103000";
                        areaName = "高塘镇";
                        break;
                    case "高塘派出所":
                        areaCode = "610521103000";
                        areaName = "高塘镇";
                        break;
                    case "大明派出所":
                        areaCode = "610521104000";
                        areaName = "大明镇";
                        break;
                    case "瓜坡派出所":
                        areaCode = "610521105000";
                        areaName = "瓜坡镇";
                        break;
                    case "莲花寺派出所":
                        areaCode = "610521106000";
                        areaName = "莲花寺镇";
                        break;
                    case "柳枝派出所":
                        areaCode = "610521107000";
                        areaName = "柳枝镇";
                        break;
                    case "下庙派出所":
                        areaCode = "610521108000";
                        areaName = "下庙镇";
                        break;
                    case "金堆派出所":
                        areaCode = "610521109000";
                        areaName = "金堆镇";
                        break;
                    default:
                        areaCode = m_SiteArea;
                        areaName = m_SiteAreaName;
                        break;
                    /*
城关派出所 --> 华州镇
赤水派出所 -->赤水镇
大明派出所
东阳派出所 --高塘镇
高塘派出所  ->高塘镇
瓜坡派出所
侯坊派出所 --赤水镇
金堆派出所
莲花寺派出所
柳枝派出所
下庙派出所
杏林派出所
                     * 
                     * 610521100000	华州镇
610521101000	杏林镇
610521102000	赤水镇 + 辛庄乡
610521103000	高塘镇 + 东阳乡
610521104000	大明镇 + 金惠乡
610521105000	瓜坡镇
610521106000	莲花寺镇
610521107000	柳枝镇 + 毕家乡
610521108000	下庙镇
610521109000	金堆镇
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
                    funcName = "新生儿上户信息";
                    break;
                case "020402":
                    funcName = "迁出人员信息";
                    break;
                case "020403":
                    funcName = "迁入人员信息";
                    break;
                default:
                    funcName = "";
                    break;
            }
            return funcName;
        }

        

        #region 数据导入相关成员方法
        /// <summary>
        /// 判断是否为Txt文件
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
            string xlsFile = Server.MapPath("/") + "Temp/" + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + "/";//当前日期上传目录
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
