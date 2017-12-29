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
using join.pms.dal;

namespace join.pms.dalInfo
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
        private string m_UserAreaCode;//当前用户行政区划

        private DataSet m_Ds;
        private string m_SqlParams;

        private string m_FuncInfo;
        private string m_Titles;
        private string m_Fields;

        private string m_AreaCode;

        private string m_SiteName = System.Configuration.ConfigurationManager.AppSettings["SiteName"];

        private ExportXls m_Xls;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                SetUIByFuncNo(m_FuncCode);
                SetReportArea(m_AreaCode);
            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "/css/inidex.css";  //DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                //if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";

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
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl); // "FuncCode=04&p=1" FuncUser
                m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                //继承查询条件
                m_PageSearch = StringProcess.AnalysisParas(m_SourceUrlDec, "pSearch");
                // "FuncCode=060101&p=1"
                //m_FuncCode = "02" + m_FuncCode.Substring(2);
                m_UserDept = join.pms.dal.CommPage.GetUnitCodeByUser(m_UserID);
                // m_UserDeptName = join.pms.dal.CommPage.GetUnitNameByCode(m_UserDept, ref m_UserDeptArea);
                //m_UserDeptArea = join.pms.dal.CommPage.GetUnitNameByCode(m_UserDept);
                m_UserAreaCode = join.pms.dal.CommPage.GetSingleVal("SELECT UserAreaCode FROM USER_BaseInfo WHERE UserID=" + m_UserID);
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
            string configFile = Server.MapPath("/includes/DataGridBiz.config");
            if (GetConfigParams(GetConfigCode(funcNo), configFile, ref errMsg))
            {
                m_Xls = new ExportXls();
                string[] a_FuncInfo = this.m_FuncInfo.Split(',');
                //
                this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">起始页</a> &gt;&gt; +" + CommPage.GetAllBizName(m_FuncCode) + "+ &gt;&gt; 数据导出：";
            }
            else
            {
                //Response.Write(" <script>alert('读取[" + funcNo + "]配置文件失败！') ;window.location.href='" + m_TargetUrl + "'</script>");
                //Response.End();
                MessageBox.ShowAndRedirect(this.Page, "操作提示：读取[" + funcNo + "]配置文件失败！", m_TargetUrl, true);

            }
            //SetDataToXls(a_FuncInfo[0], funcNo, " FuncNo='" + funcNo + "' ", a_FuncInfo[2], a_FuncInfo[2]);

            // INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES(FileName,FilePath,FileType,'0501',1,7)
        }

        private string GetConfigCode(string funcNo)
        {
            string returnVa = string.Empty;
            if (!string.IsNullOrEmpty(funcNo) && funcNo.Length > 2)
            {
                returnVa = funcNo;
                //if (funcNo.Substring(0, 4) == "0101" || funcNo.Substring(0, 4) == "0102" || funcNo.Substring(0, 4) == "0103" || funcNo.Substring(0, 4) == "0104" || funcNo.Substring(0, 4) == "0112")
                //{
                //    returnVa = funcNo;
                //}
                //else if (funcNo.Substring(0, 2) == "04")
                //{
                //    returnVa = "OCAPP";//P_Addres
                //}
                //else
                //{
                //    returnVa = "OCAPP";
                //}
            }
            else
            {
                returnVa = funcNo;
            }
            return returnVa;
        }

        /// <summary>
        /// 设置数据单位/行政区划单位 所有乡镇……
        /// </summary>
        /// <param name="bizCode">默认值</param>
        private void SetReportArea(string areaCode)
        {
            string siteArea = m_UserAreaCode;// System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
            // m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + siteArea + "' ORDER BY AreaCode";
            m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE AreaCode='" + siteArea + "' OR ParentCode = '" + siteArea + "' ORDER BY AreaCode";
            DataTable tmpDt = new DataTable();
            tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            DDLReportArea.DataSource = tmpDt;
            DDLReportArea.DataTextField = "AreaName";
            DDLReportArea.DataValueField = "AreaCode";
            DDLReportArea.DataBind();
            //DDLReportArea.Items.Insert(0, new ListItem("其他行政区", "000000000000"));
            tmpDt = null;
            if (!string.IsNullOrEmpty(areaCode))
            {
                this.DDLReportArea.SelectedValue = areaCode;
            }
        }

        private void SetExcelByFuncNo(string funcNo, string filterSQL, string fileDesc)
        {
            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGridBiz.config");
            string tmpFuncNo = string.Empty;
            if (GetConfigParams(GetConfigCode(funcNo), configFile, ref errMsg))
            {
                string[] a_FuncInfo = this.m_FuncInfo.Split(',');
                m_Xls = new ExportXls();
                m_Xls.FuncNo = funcNo; // 合计用

                switch (funcNo)
                {
                    case "0101":
                        m_Xls.XlsName = "一孩生育登记";
                        m_Xls.XlsUnit = "数据来源：" + m_SiteName;
                        // 编号,状态,申请方向,申请时间,女方姓名,身份证号,联系电话,婚姻状况,户籍地址,现居住地,男方姓名,身份证号,联系电话,婚姻状况,办结时间
                        m_Xls.Formats = "1,0,0,2,0,1,1,0,0,0,0,1,1,0,2"; // 1,数字 ; 2,日期
                        break;
                    case "0102":
                        m_Xls.XlsName = "二孩生育登记";
                        m_Xls.XlsUnit = "数据来源：" + m_SiteName;
                        // 编号,状态,申请方向,申请时间,女方姓名,身份证号,联系电话,婚姻状况,男方姓名,身份证号,联系电话,户籍地址,婚姻状况,工作单位,现居住地,办结时间
                        m_Xls.Formats = "1,0,0,2,0,1,1,0,0,1,1,0,0,0,0,2"; // 1,数字 ; 2,日期
                        break;
                    case "010299":
                        m_Xls.XlsName = "二孩生育花名册导出";
                        m_Xls.XlsUnit = "数据来源：" + m_SiteName;
                        // 姓名	民族 户口性质 身份证号 婚姻状况	户籍地 姓名	民族 户口性质 身份证号 婚姻状况	户籍地 孩次 符合条例生育政策 审核时间 操作时间
                        m_Xls.Formats = "0,0,0,1,0,0,0,0,0,1,0,0,0,0,1,1"; // 1,数字 ; 2,日期
                        break;
                    case "0103":
                        m_Xls.XlsName = "农村部分计划生育家庭奖励扶助对象申报审核";
                        m_Xls.XlsUnit = "数据来源：" + m_SiteName;
                        // 编号,状态,申请方向,申请时间,本人姓名,身份证号,婚姻状况,联系电话,户籍地址,存活男孩,存活女孩,配偶姓名,身份证号,婚姻状况,联系电话,办结时间
                        m_Xls.Formats = "1,0,0,2,0,1,0,1,0,1,1,0,1,0,1,2"; // 1,数字 ; 2,日期
                        break;
                    case "0104":
                        m_Xls.XlsName = "计划生育家庭特别扶助对象申请审核";
                        m_Xls.XlsUnit = "数据来源：" + m_SiteName;
                        // 编号,状态,申请方向,申请时间,本人姓名,身份证号,婚姻状况,联系电话,户籍地址,存活男孩,存活女孩,配偶姓名,身份证号,婚姻状况,联系电话,办结时间
                        m_Xls.Formats = "0,0,2,4,0,1,1,0,0,0,0,1,0,2,0,0"; // 1,数字 ; 2,日期
                        break;
                    case "0105":
                        m_Xls.XlsName = "计划生育“少生快富”工程申请审核";
                        m_Xls.XlsUnit = "数据来源：" + m_SiteName;
                        // 编号,状态,申请方向,申请时间,本人姓名,身份证号,婚姻状况,联系电话,户籍地址,存活男孩,存活女孩,配偶姓名,身份证号,婚姻状况,联系电话,办结时间
                        m_Xls.Formats = "1,0,0,2,0,1,0,1,0,1,1,0,1,0,1,2"; // 1,数字 ; 2,日期
                        break;
                    case "0106":
                        m_Xls.XlsName = "内蒙古自治区政策内二孩和双女（蒙古族三女）结扎家庭奖励申请审核";
                        m_Xls.XlsUnit = "数据来源：" + m_SiteName;
                        // 编号,状态,申请方向,申请时间,本人姓名,身份证号,婚姻状况,联系电话,户籍地址,存活男孩,存活女孩,配偶姓名,身份证号,婚姻状况,联系电话,办结时间
                        m_Xls.Formats = "1,0,0,2,0,1,0,1,0,1,1,0,1,0,1,2"; // 1,数字 ; 2,日期
                        break;
                    case "0107":
                        m_Xls.XlsName = "“一杯奶”受益对象申请审核";
                        m_Xls.XlsUnit = "数据来源：" + m_SiteName;
                        // 编号,状态,申请方向,申请时间,女方姓名,身份证号,联系电话,怀孕胎次,确认时间,现居住地,男方姓名,身份证号,联系电话,现居住地,办结时间
                        m_Xls.Formats = "1,0,0,2,0,1,1,1,2,0,0,1,1,0,2";
                        break;
                    case "0108":
                        m_Xls.XlsName = "独生子女父母光荣证";
                        m_Xls.XlsUnit = "数据来源：" + m_SiteName;
                        // 编号,状态,申请方向,申请时间,母亲姓名,身份证号,联系电话,户籍地址,父亲姓名,身份证号,联系电话,户籍地址,子女姓名,出生日期,办结时间
                        m_Xls.Formats = "1,0,0,2,0,1,1,0,0,1,1,0,0,2,2";
                        break;
                    case "0109":
                        m_Xls.XlsName = "流动人口婚育证明";
                        m_Xls.XlsUnit = "数据来源：" + m_SiteName;
                        // 编号,状态,申请方向,申请时间,持证人,身份证号,联系电话,婚姻状况,户籍地址,配偶姓名,身份证号,联系电话,婚姻状况,户籍地址,办结时间
                        m_Xls.Formats = "1,0,0,2,0,1,1,0,0,0,1,1,0,0,2";
                        break;
                    case "0110":
                        m_Xls.XlsName = "婚育情况证明";
                        m_Xls.XlsUnit = "数据来源：" + m_SiteName;
                        // 编号,状态,申请方向,申请时间,本人姓名,身份证号,婚姻状况,联系电话,户籍地址,配偶姓名,身份证号,婚姻状况,联系电话,户籍地址,办结时间
                        m_Xls.Formats = "1,0,0,2,0,1,0,1,0,0,1,0,0,1,0,2";
                        break;
                    case "0111":
                        m_Xls.XlsName = "终止妊娠申请审核";
                        m_Xls.XlsUnit = "数据来源：" + m_SiteName;
                        // 编号,状态,申请方向,申请时间,妇女姓名,身份证号,婚姻状况,联系电话,政策属性,孕周,丈夫姓名,身份证号,联系电话,户籍地址,办结时间
                        m_Xls.Formats = "1,0,0,2,0,1,0,1,0,1,0,1,1,0,2";
                        break;
                    case "0122":
                        m_Xls.XlsName = "再生育审批";
                        m_Xls.XlsUnit = "数据来源：" + m_SiteName;
                        // 编号,状态,申请方向,申请时间,女方姓名,身份证号,联系电话,婚姻状况,男方姓名,身份证号,联系电话,户籍地址,婚姻状况,工作单位,现居住地,办结时间
                        m_Xls.Formats = "1,0,0,2,0,1,1,0,0,1,1,0,0,0,0,2"; // 1,数字 ; 2,日期
                        break;
                    default:
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                        break;
                }

                m_Xls.Fields = this.m_Fields;
                m_Xls.Titles = this.m_Titles;
                SetDataToXls(a_FuncInfo[0], funcNo, filterSQL, a_FuncInfo[2], fileDesc);
            }
            else
            {
                //Response.Write(" <script>alert('读取配置文件失败！') ;window.location.href='" + m_TargetUrl + "'</script>");
                //Response.End();
                MessageBox.ShowAndRedirect(this.Page, "操作提示：读取配置文件失败！", m_TargetUrl, true);
            }

            // INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES(FileName,FilePath,FileType,'0501',1,7)
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
                string expAtt = PageValidate.GetFilterSQL(Request["ExpAttribs"]);
                string searchStr = string.Empty;
                string unitSearch = string.Empty;
                string fileDesc = string.Empty;
                string pageFilter = string.Empty;


                string areaCode = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Value);
                string areaName = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Text);
                string areaCodeFilter = string.Empty;
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
                    if (areaCode.Substring(4) == "00000000") { areaCodeFilter = areaCode.Substring(0, 4); }
                    else if (areaCode.Substring(6) == "000000" && areaCode.Substring(4) != "00000000") { areaCodeFilter = areaCode.Substring(0, 6); }
                    else if (areaCode.Substring(9) == "000" && areaCode.Substring(6) != "000000") { areaCodeFilter = areaCode.Substring(0, 9); }
                    else { }
                    unitSearch = " (RegAreaCodeA LIKE '" + areaCodeFilter + "%' OR RegAreaCodeB LIKE '" + areaCodeFilter + "%' OR SelAreaCode LIKE '" + areaCodeFilter + "%')";
                }

                _areaName = areaName;

                fileDesc += "从" + startDate + "到" + endDate + "的";
                if (expAtt == "rb1")
                {
                    fileDesc += "全部";
                    // BIZ_Contents --> Attribs: 0,初始提交;1,审核中 2,通过 3,补正 4,撤销 5,注销 6,等待审核,9,归档
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                else if (expAtt == "rb2")
                {
                    fileDesc += "审核中(包括待审核)";
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND Attribs IN(0,1,6) AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                else if (expAtt == "rb3")
                {
                    fileDesc += "审核通过";
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND Attribs IN(2,9)  AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                else 
                {
                    fileDesc += "不予办理(包括无效申请)";
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND Attribs IN(3,4,5) AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
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
        /// 花名导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn010299_Click(object sender, EventArgs e)
        {
            try
            {
                string strErr = string.Empty;
                string startDate = this.txtStartDate.Value;
                string endDate = this.txtEndDate.Value;
                string expAtt = PageValidate.GetFilterSQL(Request["ExpAttribs"]);
                string searchStr = string.Empty;
                string unitSearch = string.Empty;
                string fileDesc = string.Empty;
                string pageFilter = string.Empty;


                string areaCode = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Value);
                string areaName = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Text);
                string areaCodeFilter = string.Empty;
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
                //if (!string.IsNullOrEmpty(m_PageSearch)) pageFilter = DESEncrypt.Decrypt(m_PageSearch);
                if (string.IsNullOrEmpty(pageFilter)) { pageFilter = "1=1"; }

                if (string.IsNullOrEmpty(areaCode))
                {
                    fileDesc = "所有部门；"; unitSearch = "";
                }
                else
                {
                    fileDesc = areaName + "；";
                    if (areaCode.Substring(4) == "00000000") { areaCodeFilter = areaCode.Substring(0, 4); }
                    else if (areaCode.Substring(6) == "000000" && areaCode.Substring(4) != "00000000") { areaCodeFilter = areaCode.Substring(0, 6); }
                    else if (areaCode.Substring(9) == "000" && areaCode.Substring(6) != "000000") { areaCodeFilter = areaCode.Substring(0, 9); }
                    else { }
                    unitSearch = " (RegAreaCodeA LIKE '" + areaCodeFilter + "%' OR RegAreaCodeB LIKE '" + areaCodeFilter + "%' OR SelAreaCode LIKE '" + areaCodeFilter + "%')";
                }

                _areaName = areaName;

                fileDesc += "从" + startDate + "到" + endDate + "的";
                if (expAtt == "rb1")
                {
                    fileDesc += "全部";
                    // BIZ_Contents --> Attribs: 0,初始提交;1,审核中 2,通过 3,补正 4,撤销 5,注销 6,等待审核,9,归档
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                else if (expAtt == "rb2")
                {
                    fileDesc += "审核中(包括待审核)";
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND Attribs IN(0,1,6) AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                else if (expAtt == "rb3")
                {
                    fileDesc += "审核通过";
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND Attribs IN(2,9)  AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                else
                {
                    fileDesc += "不予办理(包括无效申请)";
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND Attribs IN(3,4,5) AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }

                //二孩生育花名册
                m_FuncCode = "010299";
                SetUIByFuncNo(m_FuncCode);
                SetExcelByFuncNo(m_FuncCode, searchStr, fileDesc);
                m_FuncCode = "0102";
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
                //SetRichTextBox(descInfo + "成功导出：");
                m_Xls = null;
                errMsg = errMsg.Replace(serverPath, "");
                errMsg = errMsg.Substring(0, errMsg.Length - 1);
                string[] aryFiles = errMsg.Split(',');
                for (int i = 0; i < aryFiles.Length; i++)
                {
                    sHtml.Append("<a href=\"" + aryFiles[i] + "\" target='_blank' > " + descInfo + fileName + "数据</a><br/>");
                    SetFileToHD(descInfo + fileName + "数据", aryFiles[i]);
                    sHtml.Append("文档已经同步发布到[ 网络硬盘 >> 我的网盘 >> ]根目录下……");
                    if (_isXiangZhen)
                    {
                        // 数据拷贝一份到乡镇信息中去
                        //SetFileToXiangZhen(descInfo + fileName + "数据", aryFiles[i]);
                        sHtml.Append("文档也同时发布到 本乡镇信息节点下……");
                    }
                }
                this.LiteralFiles.Text = sHtml.ToString();
                //Response.Write(" <script>alert('" + descInfo + " --成功导出！') ;window.location.href='" + m_TargetUrl + "';window.location.href='" + saveFiles + "'</script>");
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
            string cmsCID = "", cmsCode = "", cmsBody = string.Empty;
            DataTable tmpDt = new DataTable();
            m_SqlParams = "SELECT CmsCID, CmsCode, CmsCName FROM CMS_Class WHERE CmsCode LIKE '03__' AND CmsCName='" + _areaName + "'";
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
                if (funcNo.Substring(0, 2) == "04")
                {
                    dr = m_Ds.Tables[0].Select("FuncNo='04'");
                }



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
