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

using System.IO;
using System.Text;
using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace AreWeb.OnlineCertificate.PopInfo
{
    /// <summary>
    /// 共享数据导出
    /// </summary>
    public partial class xlsExport : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        public string m_TargetUrl;

        protected string m_FuncCode;
        private string m_PageSearch;

        private string m_UserID; // 当前登录的操作用户编号
        private string m_UserDept;//用户部门编码
        private string m_UserDeptArea;//镇办区划
        private string m_UserDeptName;//部门名称

        private DataSet m_Ds;
        private string m_SqlParams;

        private string m_FuncInfo;
        private string m_Titles;
        private string m_Fields;
        private string m_Format;

        private string m_AreaCode;

        private ExportXls m_Xls;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                SetUIByFuncNo(m_FuncCode);
                SetReportArea(m_AreaCode);
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
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl); // "FuncCode=04&p=1" FuncUser
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                //继承查询条件
                m_PageSearch = StringProcess.AnalysisParas(m_SourceUrlDec, "pSearch");

                m_UserDept = join.pms.dal.CommPage.GetUnitCodeByUser(m_UserID);
                m_UserDeptName = join.pms.dal.CommPage.GetUnitNameByCode(m_UserDept, ref m_UserDeptArea);
                //m_UserDeptArea = AreWeb.OnlineCertificate.Biz.CommPage.GetUnitNameByCode(m_UserDept);
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }
        }

        /// <summary>
        /// 设置操作行为
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetUIByFuncNo(string funcNo)
        {

            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGridShare.config");
            if (GetConfigParams(funcNo, configFile, ref errMsg))
            {
                m_Xls = new ExportXls();
                string[] a_FuncInfo = this.m_FuncInfo.Split(',');
                //
                this.m_Fields = this.m_Fields.Replace(",ReportDate,AuditFlagCN", "");
                m_AreaCode = "";
                /*
0101	共享数据
0102	差异数据  
0103	标准数据
                 */
                if (m_FuncCode.Substring(0, 4) == "0103")
                {
                    this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">起始页</a> &gt;&gt; 标准数据导出 &gt;&gt;" + a_FuncInfo[2] + "：";
                    try { m_AreaCode = DbHelperSQL.GetSingle("SELECT FuncTarget FROM SYS_Function WHERE FuncCode='" + m_FuncCode + "'").ToString(); }
                    catch { m_AreaCode = ""; }
                }
                else if (m_FuncCode.Substring(0, 4) == "0102") { this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">起始页</a> &gt;&gt; 差异数据导出 &gt;&gt; " + a_FuncInfo[2] + "："; }
                else { this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">起始页</a> &gt;&gt; 职能部门共享信息导出 &gt;&gt; " + a_FuncInfo[2] + "："; }
            }
            else
            {
                Response.Write(" <script>alert('读取[" + funcNo + "]配置文件失败！') ;window.location.href='" + m_TargetUrl + "'</script>");
                Response.End();
            }
            //SetDataToXls(a_FuncInfo[0], funcNo, " FuncNo='" + funcNo + "' ", a_FuncInfo[2], a_FuncInfo[2]);

            // INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES(FileName,FilePath,FileType,'0501',1,7)
        }

        /// <summary>
        /// 设置数据单位/行政区划单位 所有乡镇……
        /// </summary>
        /// <param name="areaCode">默认值</param>
        private void SetReportArea(string areaCode)
        {
            // 乡镇用户只允许操作本乡镇数据 by Ysl 2013/08/28
            if (m_UserDept.Substring(0, 2) == "03")
            {
                this.DDLReportArea.Items.Clear();
                this.DDLReportArea.Items.Insert(0, new ListItem(m_UserDeptName, m_UserDeptArea));
                this.DDLReportArea.SelectedValue = m_UserDept;
            }
            else
            {
                string siteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
                m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + siteArea + "' ORDER BY AreaCode";
                DataTable tmpDt = new DataTable();
                tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                DDLReportArea.DataSource = tmpDt;
                DDLReportArea.DataTextField = "AreaName";
                DDLReportArea.DataValueField = "AreaCode";
                DDLReportArea.DataBind();
                this.DDLReportArea.Items.Insert(0, new ListItem("全部行政区划……", ""));
                tmpDt = null;
                if (!string.IsNullOrEmpty(areaCode))
                {
                    this.DDLReportArea.SelectedValue = areaCode;
                }
            }
        }
        /*
0101	共享数据
0102	差异数据  
0103	标准数据
 */
        private void SetExcelByFuncNo(string funcNo, string filterSQL, string fileDesc)
        {
            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGridShare.config");
            string tmpFuncNo = string.Empty;
            if (GetConfigParams(funcNo, configFile, ref errMsg))
            {
                string[] a_FuncInfo = this.m_FuncInfo.Split(',');
                // ReportDate,AnalysisDate,AuditFlagCN
                if (funcNo.Substring(0, 2) == "11")
                {
                    this.m_Fields = this.m_Fields.Replace(",ReportDate,AuditFlagCN", "");
                }
                else if (funcNo.Substring(0, 4) == "0103" )
                {
                    // 全部导出
                }
                else
                {
                    this.m_Fields = this.m_Fields.Replace(",ReportDate,AnalysisDate,AuditFlagCN", "");
                }

                //ReportDate,AuditFlagCN
                m_Xls = new ExportXls();
                m_Xls.FuncNo = funcNo; // 合计用
                m_Xls.XlsName = a_FuncInfo[2];

                //======兼容差异数据导出 by ysl 2013/07/30
                tmpFuncNo = funcNo;
                //if (funcNo.Substring(0, 2) == "06" && funcNo!="060101") tmpFuncNo = "02" + funcNo.Substring(2);
                if (funcNo.Substring(0, 4) == "0101") tmpFuncNo = funcNo.Substring(0, 6);
                if (funcNo.Substring(0, 4) == "0102") tmpFuncNo = "0101" + funcNo.Substring(4, 2);
                //====================
                switch (tmpFuncNo)
                {
                    //数据共享-共享数据（卫计局/民政局/公安局/人社局/教体局/统计局数据导出）
                    case "010101":
                        m_Xls.XlsUnit = "数据来源：卫计局";
                        m_Xls.Formats = this.m_Format;
                        break;
                    case "010102":
                        m_Xls.XlsUnit = "数据来源：民政局";
                        m_Xls.Formats = this.m_Format;
                        break;
                    case "010103":
                        m_Xls.XlsUnit = "数据来源：公安局";
                        m_Xls.Formats = this.m_Format;
                        break;
                    case "010104":
                        m_Xls.XlsUnit = "数据来源：人社局";
                        m_Xls.Formats = this.m_Format;
                        break;
                    case "010105":
                        m_Xls.XlsUnit = "数据来源：教体局";
                        m_Xls.Formats = this.m_Format;
                        break;
                    case "010106":
                        m_Xls.XlsUnit = "数据来源：统计局";
                        m_Xls.Formats = this.m_Format;
                        break;
                    default:
                        Response.Write(" <script>alert('操作失败：参数错误！') ;window.location.href='" + m_TargetUrl + "'</script>");
                        break;
                }

                m_Xls.Fields = this.m_Fields;
                m_Xls.Titles = this.m_Titles;
                SetDataToXls(a_FuncInfo[0], funcNo, filterSQL, a_FuncInfo[2], fileDesc);
            }
            else
            {
                Response.Write(" <script>alert('读取配置文件失败！') ;window.location.href='" + m_TargetUrl + "'</script>");
                Response.End();
            }

            // INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES(FileName,FilePath,FileType,'0501',1,7)
        }


        /// <summary>
        /// 编码转换
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetAnalysisCode(string funcNo)
        {
            string returnVa = string.Empty;
            if (funcNo.Substring(0, 4) == "0102")
            {
                returnVa = "0101" + funcNo.Substring(4, funcNo.Length - 4);
            }
            else
            {
                returnVa = funcNo;
            }
            return returnVa;
        }
        private bool _isXiangZhen = false;
        private string _areaName;
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string strErr = string.Empty;
                string startDate = this.txtStartDate.Value;
                string endDate = this.txtEndDate.Value;
                string expAtt = Request["ExpAttribs"];
                string searchStr = string.Empty;
                string unitSearch = string.Empty;
                string fileDesc = string.Empty;
                string pageFilter = string.Empty;


                string areaCode = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Value);
                string areaName = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Text);
                //if (string.IsNullOrEmpty(areaCode))
                //{
                //    strErr += "请选择数据归属的行政区划！\\n";
                //}
                if (String.IsNullOrEmpty(startDate))
                {
                    strErr += "请选择数据开始日期！\\n";
                }
                if (String.IsNullOrEmpty(endDate))
                {
                    strErr += "请选择数据终止日期！\\n";
                }
                if (String.IsNullOrEmpty(expAtt))
                {
                    //strErr += "请选择要导出的数据类型！\\n";
                    expAtt = "rb1";
                }


                if (strErr != "")
                {
                    MessageBox.Show(this, strErr);
                    return;
                }

                // 列表界面的查询条件
                if (!string.IsNullOrEmpty(m_PageSearch)) pageFilter = DESEncrypt.Decrypt(m_PageSearch);
                if (string.IsNullOrEmpty(pageFilter)) { pageFilter = "1=1"; }

                if (string.IsNullOrEmpty(areaCode))
                {
                    fileDesc = "所有部门；"; unitSearch = "";
                }
                else
                {
                    fileDesc = areaName + "；";
                    unitSearch = " AreaCode LIKE '" + areaCode.Substring(0, 9) + "%' "; 
                    //" AreaCode LIKE '%" + areaCode + "%' ";  [CreateDate] ReportDate,AuditFlagCN
                }
                if (string.IsNullOrEmpty(unitSearch))
                {
                    _isXiangZhen = false;
                    unitSearch = " 1=1 ";
                }
                else
                {
                    _isXiangZhen = true;
                    // 根据地址匹配乡镇导出
                    string areaNo = System.Configuration.ConfigurationManager.AppSettings["AreaNo"];
                    string areaVal = System.Configuration.ConfigurationManager.AppSettings["AreaVal"];
                    //if (m_FuncCode.Substring(0, 2) != "05") unitSearch = " (" + AreWeb.OnlineCertificate.Biz.CommPage.GetAreaMatch(areaNo, areaVal, m_FuncCode, areaName) + " OR AreaCode LIKE '" + areaCode.Substring(0, 9) + "%') ";
                }
                _areaName = areaName;
                /*
0101	共享数据
0102	差异数据  
0103	标准数据
*/
                fileDesc += "从" + startDate + "到" + endDate + "的";
                if (m_FuncCode.Substring(0, 4) == "0103")
                {
                    fileDesc += "标准数据"; // 标准数据
                    searchStr = unitSearch + " AND  CreateDate>='" + startDate + " 00:00:00' AND CreateDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                else if (m_FuncCode == "01010101" || m_FuncCode == "01010102" || m_FuncCode == "01010103" || m_FuncCode == "01010104" || m_FuncCode == "01010105" || m_FuncCode == "01010106" || m_FuncCode == "01010122" || m_FuncCode == "01010123" || m_FuncCode == "01010124" || m_FuncCode == "01010125" || m_FuncCode == "01010126" || m_FuncCode == "01010127" || m_FuncCode == "01010128" || m_FuncCode == "01010129" || m_FuncCode == "01010130" || m_FuncCode == "01010131" || m_FuncCode == "01010116" || m_FuncCode == "01010117" || m_FuncCode == "01010118" || m_FuncCode == "01010120")
                {
                    fileDesc += "全员差异数据";
                    searchStr = unitSearch + " AND  FuncNo='" + m_FuncCode + "' AND Attribs IN(2,3) AND  OprateDate>='" + startDate + " 00:00:00' AND OprateDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                //else if (m_FuncCode.Substring(0, 2) == "05")
                //{
                //    fileDesc += "身份证校验失败";
                //    searchStr = unitSearch + " AND  FuncNo='" + m_FuncCode + "' AND Attribs=3 AND  OprateDate>='" + startDate + " 00:00:00' AND OprateDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                //}
                else
                {                    
                    // rb5
                    if (expAtt == "rb1")
                    {
                        fileDesc += "全部";
                        searchStr = unitSearch + " AND  FuncNo='" + GetAnalysisCode(m_FuncCode) + "' AND ReportDate>='" + startDate + " 00:00:00' AND ReportDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                    }
                    else if (expAtt == "rb2")
                    {
                        fileDesc += "未公开的";
                        searchStr = unitSearch + " AND  FuncNo='" + GetAnalysisCode(m_FuncCode) + "' AND AuditFlag=0 AND ReportDate>='" + startDate + " 00:00:00' AND ReportDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                    }
                    else if (expAtt == "rb3")
                    {
                        fileDesc += "已经审核的";
                        searchStr = unitSearch + " AND  FuncNo='" + GetAnalysisCode(m_FuncCode) + "' AND AuditFlag>2 AND AuditFlag!=4 AND ReportDate>='" + startDate + " 00:00:00' AND ReportDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                    }
                    else if (expAtt == "rb4")
                    {
                        fileDesc += "已经公开的";
                        searchStr = unitSearch + " AND  FuncNo='" + GetAnalysisCode(m_FuncCode) + "' AND AuditFlag=9 AND ReportDate>='" + startDate + " 00:00:00' AND ReportDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                    }
                    else
                    {
                        fileDesc += "存在差异的";
                        searchStr = unitSearch + " AND  FuncNo='" + GetAnalysisCode(m_FuncCode) + "' AND AnalysisFlag=2 AND ReportDate>='" + startDate + " 00:00:00' AND ReportDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                    }
                }

                if (m_FuncCode.Substring(0, 4) == "0103") { SetExcelByFuncNo("0103", searchStr, fileDesc); }
                else { SetExcelByFuncNo(m_FuncCode, searchStr, fileDesc); }

            }
            catch (Exception ex)
            {
                this.LiteralFiles.Text = ex.Message;
            }
            //SetDataToXls(a_FuncInfo[0], funcNo, " FuncNo='" + funcNo + "' ", a_FuncInfo[2], a_FuncInfo[2]);
        }

        /// <summary>
        /// 获取区划匹配字段
        /// </summary>
        /// <param name="areaNo"></param>
        /// <param name="areaVal"></param>
        /// <returns></returns>
        private string GetAreaMatch(string areaNo, string areaVal, string areaName)
        {
            string returnVal = "1=1";
            string[] aryNo = areaNo.Split(',');
            string[] aryVal = areaVal.Split(',');
            if (areaName.Length > 2) areaName = areaName.Substring(0, 2);
            for (int i = 0; i < aryNo.Length; i++)
            {
                if (m_FuncCode == aryNo[i].Trim())
                {
                    returnVal = aryVal[i].Trim() + " LIKE '%" + areaName + "%'";
                    break;
                }
            }
            return returnVal;
        }

        /// <summary>
        /// 保存excel
        /// </summary>
        private void SetDataToXls(string tableName, string funcNo, string filterSQL, string fileName, string descInfo)
        {
            string errMsg = string.Empty;
            string serverPath = Server.MapPath("/");
            string configPath = System.Configuration.ConfigurationManager.AppSettings["FCKeditor:UserFilesPath"];//文件存放路径
            string virtualPath = configPath + funcNo + "/" + StringProcess.GetCurDateTimeStr(6) + "/";
            // if (m_FuncCode == "04")
            string sqlParams = "SELECT " + this.m_Fields + " FROM " + tableName + " WHERE " + filterSQL;
            string savePath = serverPath + virtualPath;
            string saveFiles = savePath + System.DateTime.Now.ToString("yyyyMMdd-hhmm") + ".xls";
            StringBuilder sHtml = new StringBuilder();
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

            if (m_Xls.ExportDsToXls(saveFiles, DbHelperSQL.Query(sqlParams), ref errMsg))
            {
                if (DbHelperSQL.Query(sqlParams).Tables[0].Rows.Count > 0)
                {
                    //SetRichTextBox(descInfo + "成功导出：");
                    m_Xls = null;
                    errMsg = errMsg.Replace(serverPath, "");
                    errMsg = errMsg.Substring(0, errMsg.Length - 1);
                    string[] aryFiles = errMsg.Split(',');
                    for (int i = 0; i < aryFiles.Length; i++)
                    {
                        sHtml.Append("<a href=\"" + aryFiles[i] + "\" target='_blank' > " + descInfo + fileName + "数据</a><br/>");
                        SetFileToHD(descInfo + fileName + "数据", aryFiles[i]);
                        sHtml.Append("文档已经同步发布到[ 系统管理  >> 网络硬盘 >> 我的网盘 >> ]目录下……");
                        if (_isXiangZhen)
                        {
                            // 数据拷贝一份到乡镇信息中去
                            SetFileToXiangZhen(descInfo + fileName + "数据", aryFiles[i]);
                            sHtml.Append("文档也同时发布到 本乡镇信息节点下……");
                        }
                    }
                    this.LiteralFiles.Text = sHtml.ToString();
                    //Response.Write(" <script>alert('" + descInfo + " --成功导出！') ;window.location.href='" + m_TargetUrl + "';window.location.href='" + saveFiles + "'</script>");
                }
                else
                {
                    this.LiteralFiles.Text = "没有符合条件的数据！";
                }
            }
            else
            {
                //SetRichTextBox(descInfo + "导出失败，详细信息如下：");
                this.LiteralFiles.Text = errMsg;
            }
        }
        //SELECT CmsCID, CmsCode, CmsCName FROM CMS_Class WHERE CmsCode LIKE '03__' AND CmsCName=
        private void SetFileToXiangZhen(string fileName, string filePath)
        {
            // 
            /*
040301	城关镇
040302	饶峰镇
040303	两河镇
040304	迎丰镇
040305	池河镇
040306	后柳镇
040307	喜河镇
040308	熨斗镇
040309	云雾山镇
040310	曾溪镇
040311	中池镇
*/
            string cmsCID = "", cmsCode = "", cmsBody = string.Empty;
            DataTable tmpDt = new DataTable();
            m_SqlParams = "SELECT CmsCID, CmsCode, CmsCName FROM CMS_Class WHERE CmsCode LIKE '0403__' AND CmsCName='" + _areaName + "'";
            tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            if (tmpDt.Rows.Count == 1)
            {
                cmsCID = tmpDt.Rows[0][0].ToString();
                cmsCode = tmpDt.Rows[0][1].ToString();
            }
            tmpDt = null;
            cmsBody = "&lt;a&nbsp;href=&quot;" + filePath + "&quot;&nbsp;target=&quot;_blank&quot;&gt;" + fileName + "&lt;/a&gt;";
            m_SqlParams = "INSERT INTO [CMS_Contents](CmsTitle,CmsBody,CmsCID,UserID,CmsCode)  VALUES('" + fileName + "','" + cmsBody + "'," + cmsCID + "," + m_UserID + ",'" + cmsCode + "')";
            DbHelperSQL.ExecuteSql(m_SqlParams);
        }

        private void SetFileToHD(string fileName, string filePath)
        {
            // 
            m_SqlParams = "INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES('" + fileName + "','" + filePath + "','.xls','060202'," + m_UserID + ",1)";
            DbHelperSQL.ExecuteSql(m_SqlParams);
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
                if (funcNo.Substring(0, 2) == "04")
                {
                    dr = m_Ds.Tables[0].Select("FuncNo='04'");
                }



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
    }
}
