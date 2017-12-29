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

namespace join.pms.web.SysAdmin
{
    /// <summary>
    /// siteadmin 日志导出
    /// </summary>
    public partial class xlsExport : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        public string m_TargetUrl;
        private string m_FuncCode;

        private string m_UserID; // 当前登录的操作用户编号
        private DataSet m_Ds;
        private string m_SqlParams;

        private string m_FuncInfo;
        private string m_Titles;
        private string m_Fields;

        private string m_SiteName = System.Configuration.ConfigurationManager.AppSettings["SiteName"];

        private ExportXls m_Xls;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
            }
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
            m_SourceUrl = Request.QueryString["sourceUrl"];
            if (!string.IsNullOrEmpty(m_SourceUrl))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                // "FuncCode=060101&p=1"
                //m_FuncCode = "02" + m_FuncCode.Substring(2);
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }
        }




        private void SetExcelByFuncNo(string funcNo, string filterSQL, string fileDesc)
        {

            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGrid.config");
            GetConfigParams(funcNo, configFile, ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：读取配置文件失败！", m_TargetUrl, true);
            }

            string[] a_FuncInfo = this.m_FuncInfo.Split(',');
            // UserAccount,OprateUserIP,OprateModel,OprateContents,OprateDate
            m_Xls = new ExportXls();
            m_Xls.FuncNo = funcNo; // 合计用
            m_Xls.XlsName = a_FuncInfo[2];
            m_Xls.Fields = this.m_Fields;
            m_Xls.Titles = this.m_Titles;

            switch (funcNo)
            {
                case "1101":
                    m_Xls.XlsUnit = "数据来源：" + m_SiteName;
                    m_Xls.Formats = "0,1,1,1,1,1,1,1,1,1,1,1,1,0,2,0"; // 1,数字 ; 2,日期
                    break;
                case "1103":
                    m_Xls.XlsUnit = "数据来源：" + m_SiteName;
                    //m_Xls.Formats = "0,1,0,0,0,1,0,0,0,0,0,2,0,1,2,0,2,0"; // 1,数字 ; 2,日期
                    m_Xls.Formats = "0,0,2,1,0,1,1,1,0,1,0,0,1,0,0,0,0,0,0,1,1,2,2,0";
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                    break;
            }

            SetDataToXls(a_FuncInfo[0], funcNo, filterSQL, a_FuncInfo[2], fileDesc);

            // INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES(FileName,FilePath,FileType,'0501',1,7)
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string strErr = string.Empty;
                string startDate = this.txtStartDate.Value;
                string endDate = this.txtEndDate.Value;
                string searchStr = string.Empty;
                string fileDesc = string.Empty;

                if (String.IsNullOrEmpty(startDate))
                {
                    strErr += "请选择数据开始日期！\\n";
                }
                if (String.IsNullOrEmpty(endDate))
                {
                    strErr += "请选择数据终止日期！\\n";
                }

                if (strErr != "")
                {
                    MessageBox.Show(this, strErr);
                    return;
                }

                if (m_FuncCode == "1006") { 
                    fileDesc = "从" + startDate + "到" + endDate + "的";
                    searchStr = " OprateModel ='数据修改' AND OprateDate>='" + startDate + " 00:00:00' AND OprateDate<'" + endDate + " 23:59:59'";
                }
                else { 
                    fileDesc = "从" + startDate + "到" + endDate + "的";
                    searchStr = " OprateModel !='数据修改' AND OprateDate>='" + startDate + " 00:00:00' AND OprateDate<'" + endDate + " 23:59:59'";
                }

                SetExcelByFuncNo(m_FuncCode, searchStr, fileDesc);
            }
            catch (Exception ex)
            {
                this.LiteralFiles.Text = ex.Message;
            }


            //SetDataToXls(a_FuncInfo[0], funcNo, " FuncNo='" + funcNo + "' ", a_FuncInfo[2], a_FuncInfo[2]);

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

            string sqlParams = "SELECT " + this.m_Fields + " FROM " + tableName + " WHERE " + filterSQL;
            string savePath = serverPath + virtualPath;
            string saveFiles = savePath + System.DateTime.Now.ToString("yyyyMMdd-hhmm") + ".xls";
            StringBuilder sHtml = new StringBuilder();
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

            if (m_Xls.ExportDsToXls(saveFiles, DbHelperSQL.Query(sqlParams), ref errMsg))
            {
                //SetRichTextBox(descInfo + "成功导出："); UserAccount,OprateUserIP,OprateModel,OprateContents,OprateDate
                m_Xls = null;
                errMsg = errMsg.Replace(serverPath, "");
                errMsg = errMsg.Substring(0, errMsg.Length - 1);
                string[] aryFiles = errMsg.Split(',');
                for (int i = 0; i < aryFiles.Length; i++)
                {
                    sHtml.Append("<a href=\"" + aryFiles[i] + "\" target='_blank' > " + descInfo + fileName + "数据</a><br/>");
                    SetFileToHD(descInfo + fileName + "数据", aryFiles[i]);
                    sHtml.Append("文档已经同步发布到[ 网络硬盘 >> 我的网盘 >> ]根目录下……<br/>");
                    
                }
                //Response.Write(" <script>alert('" + descInfo + " --成功导出！') ;window.location.href='" + m_TargetUrl + "';window.location.href='" + saveFiles + "'</script>");
                sqlParams = "DELETE FROM SYS_Log WHERE " + filterSQL;
                DbHelperSQL.ExecuteSql(sqlParams);
                sHtml.Append("导出的数据在数据库中已经被清理删除……");
                this.LiteralFiles.Text = sHtml.ToString();
            }
            else
            {
                //SetRichTextBox(descInfo + "导出失败，详细信息如下：");
            }
        }

        private void SetFileToHD(string fileName, string filePath)
        {
            // 
            m_SqlParams = "INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES('" + fileName + "','" + filePath + "','.xls','0802'," + m_UserID + ",1)";
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
    }
}

