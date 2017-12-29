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

using join.pms.dal;
using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.web.BizInfo
{
    public partial class UnvBizList : System.Web.UI.Page
    {
        private string m_FuncCode;
        private string m_FuncName;
        private string m_FuncUser;
        private string m_TabPageNo;
        private string m_PageSearch;
        private string m_RangeSearch;

        private string m_UserID; // 当前登录的操作用户编号
        private string m_UserDept;//用户部门编码
        private string m_UserDeptName;//部门名称
        private string m_UserDeptArea;//镇办区划 
        private string m_RoleID;
        private string m_FuncTreeName;

        private string m_AreaNo = System.Configuration.ConfigurationManager.AppSettings["AreaNo"];
        private string m_AreaVal = System.Configuration.ConfigurationManager.AppSettings["AreaVal"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            SetPageStyle(m_UserID);
            ValidateParams();
        }

        private void ValidateParams() {
            m_FuncCode = PageValidate.GetFilterSQL(Request.QueryString["FuncCode"]);// 访问此数据列表的功能编号
            m_FuncName = PageValidate.GetFilterSQL(Request.QueryString["FuncNa"]);//功能名称
            m_FuncUser = PageValidate.GetFilterSQL(Request.QueryString["FuncUser"]);
            m_TabPageNo = PageValidate.GetFilterSQL(Request.QueryString["TabPage"]);
            m_PageSearch = PageValidate.GetFilterSQL(Request.QueryString["pSearch"]);//通用搜索
            m_RangeSearch = PageValidate.GetFilterSQL(Request.QueryString["searchRange"]);// 范围搜索,流程节点
            string pageNo = PageValidate.GetFilterSQL(Request.QueryString["p"]);
            string searchKey = PageValidate.GetFilterSQL(Request.QueryString["k"]);
            string userSearchKey = PageValidate.GetFilterSQL(Request.QueryString["searchKey"]);
            //string urlTemp = Request.Url.ToString();
            string urlParams = "FuncUser=" + m_FuncUser + "&FuncCode=" + m_FuncCode + "&FuncNa=" + Server.UrlEncode(m_FuncName);
            if (String.IsNullOrEmpty(m_TabPageNo)) m_TabPageNo = "0";

            if (String.IsNullOrEmpty(pageNo)) pageNo = "1";
            if (String.IsNullOrEmpty(m_FuncCode) || !PageValidate.IsNumber(pageNo) || string.IsNullOrEmpty(m_UserID))
            {
                Response.Write("非法访问：操作被终止！");
                Response.End();
            }
            else
            {
                if (!String.IsNullOrEmpty(userSearchKey))
                {
                    searchKey = userSearchKey;
                }
                this.tbFuncNo.Value = m_FuncCode;
                m_RoleID = DbHelperSQL.GetSingle("SELECT TOP 1 RoleID FROM SYS_UserRoles WHERE UserID=" + m_UserID).ToString();
                //// 默认管理员和区县级显示所有业务列表
                //if (m_RoleID != "5")
                //{
                //    if (string.IsNullOrEmpty(m_RangeSearch)) m_RangeSearch = "range_all";
                //}
                //else
                //{
                //    if (string.IsNullOrEmpty(m_RangeSearch)) m_RangeSearch = "range_audit";
                //}
                if (string.IsNullOrEmpty(m_RangeSearch)) m_RangeSearch = "range_all";
                GetDataGrid(searchKey, m_FuncCode, pageNo, urlParams);
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
                string pageUrl = Request.RawUrl;
                if (!string.IsNullOrEmpty(pageUrl))
                {
                    if (pageUrl.IndexOf("FuncUser") > 5)
                    {
                        m_UserID = DESEncrypt.Decrypt(m_FuncUser);
                        SetUserLoginInfo(m_UserID);
                    }
                    else if (pageUrl.IndexOf("action=relogin") > 5)
                    {
                        Response.Write("<script language='javascript'>parent.location.href='/loginTemp.aspx';</script>");
                        Response.End();
                    }
                    else
                    {
                        //MessageBox.ShowAndRedirect(this.Page, "", pageUrl, true);
                        Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/loginTemp.aspx';</script>");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/loginTemp.aspx';</script>");
                    Response.End();
                }
            }
            else {
                
            }
        }

        /// 设置并保存用户登陆信息
        /// </summary>
        /// <param name="userID"></param>
        private void SetUserLoginInfo(string userID)
        {
            //设置用户登陆信息cookie
            if (Request.Browser.Cookies)
            {
                HttpCookie cookie = new HttpCookie("AREWEB_OC_USER_YSL");
                cookie.Values.Add("UserID", userID);
                Response.AppendCookie(cookie);
                cookie.Expires = DateTime.Now.AddHours(4); //cookie过期时间
            }
            else
            {
                Session["AREWEB_OC_USERID"] = userID;
            }
        }

        /// <summary>
        /// 设置页面样式
        /// </summary>
        /// <param name="userID"></param>
        private void SetPageStyle(string userID)
        {
            try
            {
                //string cssFile = DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                //if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";
                string cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        /// <summary>
        /// 获取数据列表及数据列表组件调用示例
        /// 杨胜灵 2008/08/30 
        /// </summary>
        /// <param name="searchKeys"></param>
        /// <param name="funcNo"></param>
        /// <param name="pageNo"></param>
        private void GetDataGrid(string searchKeys, string funcNo, string pageNo, string urlParams)
        {
            string errorMsg = string.Empty;
            string searchSQL = string.Empty;
            string pageSearch = string.Empty;

            string configFile = Server.MapPath("/includes/DataGridBiz.config");
            string pageSize = System.Configuration.ConfigurationManager.AppSettings["GridPageSize"]; // 每页显示的记录数
            string funcPowers = string.Empty;//  // 改功能的权限集 
            //m_UserDeptName = join.pms.dal.CommPage.GeUserAreaInfo(m_UserID, ref m_UserDeptArea);
            m_UserDept = join.pms.dal.CommPage.GetUnitCodeByUser(m_UserID, ref m_UserDeptArea);
            m_UserDeptName = join.pms.dal.CommPage.GetUnitNameByCode(m_UserDept);
            if (String.IsNullOrEmpty(funcNo) || string.IsNullOrEmpty(m_UserDept))
            {
                LiteralDataList.Text = "操作失败：参数错误!";
                return;
            }
            //根据用户归属部门判断操作权限 CommPage.GetUserBizPower(m_UserID)
            funcPowers = CommPage.GetUserBizPower(m_UserID, funcNo);

            //=================
            searchSQL = GetSearchByFuncNo(funcNo, m_UserID);

            join.pms.dal.DataListBiz pageList = new DataListBiz();

            if (pageList.GetConfigParams(GetConfigCode(funcNo), configFile, ref errorMsg))
            {
                if (!String.IsNullOrEmpty(searchKeys) && !String.IsNullOrEmpty(pageList.SearchFields))
                {
                    if (!String.IsNullOrEmpty(searchSQL)) { searchSQL += " AND " + pageList.SearchFields + " LIKE '%" + searchKeys + "%' "; }
                    else { searchSQL += " " + pageList.SearchFields + " LIKE '%" + searchKeys + "%' "; }
                }
                try
                {
                    pageList.TabPageNo = m_TabPageNo;
                    pageList.PageSize = int.Parse(pageSize);
                    pageList.PageNo = int.Parse(pageNo);
                    pageList.FuncPowers = funcPowers;
                    pageList.FuncNo = funcNo;
                    pageList.UserAreaCode = m_UserDeptArea;
                    //pageList.m_UserRoleID = m_RoleID;
                    // ========== 通用搜索 Start ===========> GetRangeSearch
                    if (!String.IsNullOrEmpty(Request["searchAction"]) && Request["searchAction"] == "advSearch")
                    {
                        pageSearch = GetPageSearch(pageList.FieldsName, pageList.FieldsFormat);
                        m_PageSearch = "";
                    }
                    else if (!String.IsNullOrEmpty(m_PageSearch))
                    {
                        pageSearch = DESEncrypt.Decrypt(m_PageSearch);
                    }
                    if (!String.IsNullOrEmpty(pageSearch))
                    {
                        if (string.IsNullOrEmpty(searchSQL)) { searchSQL += pageSearch; }
                        else { searchSQL += " AND " + pageSearch; }
                    }
                    // ========== 通用搜索 End ===========>
                    // BizInfo/UnvBizList.aspx?1=1&pSearch=E31E58FB2A760DEA&UserID=1&FuncUser=&FuncCode=0102&FuncNa=&p=1&searchRange=range_all
                    // &searchRange=" + m_RangeSearch + "
                    pageList.Url = "/BizInfo/UnvBizList.aspx?1=1&pSearch=" + DESEncrypt.Encrypt(pageSearch) + "&UserID=" + m_UserID + "&" + urlParams;

                    pageList.SearchKeys = Server.UrlEncode(searchKeys);
                    pageList.SearchType = "";
                    pageList.RangeKey = m_RangeSearch;
                    pageList.SearchWhere = searchSQL;
                    // 分页数据
                    this.LiteralDataList.Text = pageList.GetList();
                    this.m_FuncTreeName = pageList.FuncTreeName;

                    if (!String.IsNullOrEmpty(urlParams)) this.txtUrlParams.Value = DESEncrypt.Encrypt("pSearch=" + DESEncrypt.Encrypt(pageSearch) + "&searchRange=" + m_RangeSearch + "&UserID=" + m_UserID + "&" + urlParams + "&p=" + pageNo);//保持操作后的页码 + "&p=" + pageNo
                    // 查看日志
                    DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "数据查看", "用户于 " + DateTime.Now.ToString() + " 查看了[ " + m_FuncTreeName + " ]的数据");
                }
                catch (Exception ex)
                {
                    LiteralDataList.Text = ex.Message;
                }
            }
            else
            {
                LiteralDataList.Text = errorMsg;
            }
            pageList = null;
        }

        /// <summary>
        /// 根据功能号获取配置文件
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetConfigFileName(string funcNo)
        {
            string returnVa = string.Empty;
            if (!string.IsNullOrEmpty(funcNo) && funcNo.Length > 1 && PageValidate.IsNumber(funcNo))
            {
                //funcNo = funcNo.Substring(0, 2);
                //funcNo = funcNo.Substring(0, 2);
                //if (funcNo == "01") returnVa = "/includes/DataGridAdmin.config";     // 系统后台管理配置
                //if (funcNo == "02") returnVa = "/includes/DataGrid.config";      // xx的管理配置

                returnVa = "/includes/DataGrid.config";
            }
            else
            {
                Server.Transfer("errors.aspx");
            }
            return returnVa;
        }

        /// <summary>
        /// 获取调用的配置节编码 0101-0104  0112
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetConfigCode(string funcNo)
        {
            string returnVa = string.Empty;
            if (!string.IsNullOrEmpty(funcNo) && funcNo.Length > 2)
            {
                if (funcNo == "0112" || funcNo == "0113" || funcNo == "0114")
                {
                    returnVa = "OCAPP";
                }
                else
                {
                    returnVa = funcNo;
                }
            }
            else
            {
                returnVa = funcNo;
            }
            return returnVa;
        }

        /// <summary>
        /// 根据功能编号显示内容
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetSearchByFuncNo(string funcNo, string userID)
        {
            string returnVa = string.Empty;
            switch (funcNo)
            {
                case "0101xxx":  
                    returnVa = " BizCode = '0101xx' AND Attribs!=4 ";
                    break;
                default: // 默认显示当前功能所有内容OprateModel ='数据修改'
                    returnVa = GetXlsFilter(funcNo);
                    break;
            }
            return returnVa;
        }
        /*
 1	系统管理员
2	业务管理-旗县
3	业务处理-旗县
4	业务处理-镇办
5	业务处理-社区/村
6	业务处理-医院
         */
        private string GetXlsFilter(string funcNo)
        {
            string returnVa = string.Empty;// RegAreaCodeA RegAreaCodeB
            // BIZ_Contents --> Attribs: 0,初始提交;1,审核中 2,通过 3,补正 4,撤销 5,注销 6,等待审核,9,归档
            if (m_RoleID == "1")
            {
                if (!string.IsNullOrEmpty(m_RangeSearch)) {
                    returnVa = GetRangeSearch() +"AND BizCode ='" + funcNo + "' AND Attribs!=4";
                }
                else { returnVa = " Attribs IN(0,1,3,6) AND BizCode ='" + funcNo + "' AND Attribs!=4"; }
            }
            else if (m_RoleID == "2")
            {
                if (!string.IsNullOrEmpty(m_RangeSearch))
                {
                    returnVa = GetRangeSearch() + "AND BizCode ='" + funcNo + "' AND Attribs!=4 AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR GetAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%')";
                }
                else { returnVa = " Attribs IN(0,1,3,6) AND BizCode ='" + funcNo + "' AND Attribs!=4 AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR GetAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%')"; }
            }
            else if (m_RoleID == "3")
            {
                if (!string.IsNullOrEmpty(m_RangeSearch))
                {
                    returnVa = GetRangeSearch() + "AND BizCode ='" + funcNo + "' AND Attribs!=4 AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR GetAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%')";
                }
                else { returnVa = GetFilterByRole(m_RoleID, funcNo) + " Attribs IN(0,1,3,6) AND BizCode ='" + funcNo + "' AND Attribs!=4 AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR GetAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%')"; }
            }
            else if (m_RoleID == "4" )
            {
                // 业务处理-镇办
                if (!string.IsNullOrEmpty(m_RangeSearch))
                {
                    returnVa = GetRangeSearch() + "AND BizCode='" + funcNo + "' AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' OR GetAreaCode LIKE '" + m_UserDeptArea.Substring(0, 9) + "%') AND Attribs!=4";
                }
                else
                {
                    returnVa = GetFilterByRole(m_RoleID, funcNo) + "AND Attribs IN(0,1,3,6) AND BizCode='" + funcNo + "' AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 9) + "%') AND Attribs!=4";
                }
            }
            else if (m_RoleID == "5")
            {
                // 业务处理-社区/村
                if (!string.IsNullOrEmpty(m_RangeSearch))
                {
                    returnVa = GetRangeSearch() + "AND BizCode='" + funcNo + "' AND (RegAreaCodeA= '" + m_UserDeptArea + "' OR RegAreaCodeB = '" + m_UserDeptArea + "' OR SelAreaCode='" + m_UserDeptArea + "')  AND Attribs!=4";//村居以户籍地为准
                
                   // returnVa = GetRangeSearch() + "AND BizCode='" + funcNo + "' AND (((RegAreaCodeA= '" + m_UserDeptArea + "' OR RegAreaCodeB = '" + m_UserDeptArea + "')  AND Fileds51<>'1') OR (( CurAreaCodeA= '" + m_UserDeptArea + "' OR CurAreaCodeB = '" + m_UserDeptArea + "')  AND Fileds51='1') OR SelAreaCode='" + m_UserDeptArea + "')  AND Attribs!=4";//村居以户籍地为准、//Fileds51为1，以现住地审核
                }
                else
                {
                    returnVa = GetFilterByRole(m_RoleID, funcNo) + "AND Attribs IN(0,1,3,6) AND BizCode='" + funcNo + "' AND (RegAreaCodeA= '" + m_UserDeptArea + "' OR RegAreaCodeB = '" + m_UserDeptArea + "' OR SelAreaCode='" + m_UserDeptArea + "') AND Attribs!=4";
                }
            }
            else
            {
                //业务处理-医院
                if (!string.IsNullOrEmpty(m_RangeSearch)) {
                    returnVa = GetRangeSearch() + " AND BizCode ='" + funcNo + "' AND Attribs!=4";
                }
                else { returnVa = GetFilterByRole(m_RoleID, funcNo) + " AND Attribs IN(0,1,3,6) AND BizCode ='" + funcNo + "' AND Attribs!=4"; }
                
            }
            return returnVa;
        }

        private string GetFilterByRole(string roleID,string funcNo) {
            /*
1	系统管理员
2	业务管理-旗县
3	业务处理-旗县
4	业务处理-镇办
5	业务处理-社区/村
6	业务处理-医院
             * 
0101	3	女方村/居,男方村/居,镇办	一孩生育登记
0102	4	女方村/居,男方村/居,镇办,旗县	二孩生育登记
0103	3	村/居,镇办,旗县	农村部分计划生育家庭奖励扶助对象申报审核
0104	3	村/居,镇办,旗县	计划生育家庭特别扶助对象申请审核
0105	3	村/居,镇办,旗县	计划生育“少生快富”工程申请审核
0106	4	女方村/居,男方村/居,镇办,旗县	内蒙古自治区政策内二孩和双女（蒙古族三女）结扎家庭奖励申请审核
0107	3	女方村/居,男方村/居,镇办	“一杯奶”受益对象申请审核
0108	3	女方村/居,男方村/居,镇办	独生子女父母光荣证申请审核
0109	3	女方村/居,男方村/居,镇办	《流动人口婚育证明》申请审核
0110	3	村/居,镇办,旗县	婚育情况证明申请
0111	6	村/居,镇办,门诊,儿科,妇产科,旗县	终止妊娠申请审核
             * 
Attribs: 0,初始提交;1,审核中 2,通过 3,补正 4,撤销 5,注销 6,等待审核,9,归档 
             */
            string returnVa = string.Empty;
            switch (funcNo)
            {
                case "0101":
                    if (roleID == "4") { returnVa = " CurrentStep =3 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1,2) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0102":
                    if (roleID == "3") { returnVa = " CurrentStep =4 "; }
                    else if (roleID == "4") { returnVa = "CurrentStep =3 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1,2) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0103":
                    if (roleID == "3") { returnVa = " CurrentStep =3 "; }
                    else if (roleID == "4") { returnVa = "CurrentStep =2 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0104":
                    if (roleID == "3") { returnVa = " CurrentStep =3 "; }
                    else if (roleID == "4") { returnVa = "CurrentStep =2 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0105":
                    if (roleID == "3") { returnVa = " CurrentStep =3 "; }
                    else if (roleID == "4") { returnVa = "CurrentStep =2 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0106":
                    if (roleID == "3") { returnVa = " CurrentStep =4 "; }
                    else if (roleID == "4") { returnVa = "CurrentStep =3 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1,2) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0107":
                    if (roleID == "4") { returnVa = " CurrentStep =3 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1,2) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0108":
                    if (roleID == "4") { returnVa = " CurrentStep =3 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1,2) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0109":
                    if (roleID == "4") { returnVa = "CurrentStep=3"; }
                    else if (roleID == "5") { returnVa = "CurrentStep IN(0,1,2)"; }
                    else { returnVa = " 1=1 "; }
                    break;
                case "0110":
                    if (roleID == "3") { returnVa = "CurrentStep=3"; }
                    else if (roleID == "4") { returnVa = "CurrentStep=2"; }
                    else if (roleID == "5") { returnVa = "CurrentStep IN(0,1)"; }
                    else { returnVa = " 1=1 "; }
                    break;
                case "0111":
                    if (roleID == "3") { returnVa = "CurrentStep=6"; }
                    else if (roleID == "4") { returnVa = "CurrentStep=2"; }
                    else if (roleID == "5") { returnVa = "CurrentStep IN(0,1)"; }
                    else if (roleID == "6") { returnVa = "CurrentStep IN(3,4,5)"; }
                    else { returnVa = " 1=1 "; }
                    break;
                case "0122":
                    if (roleID == "3") { returnVa = " CurrentStep =4 "; }
                    else if (roleID == "4") { returnVa = "CurrentStep =3 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1,2) "; }
                    else { returnVa = " 1=1"; }
                    break;
                default: // 待办为空 
                    returnVa = "CurrentStep=0";
                    break;
            }
            return returnVa;
        }


        /// <summary>
        /// 查找范围 
        /// </summary>
        /// <returns></returns>
        private string GetRangeSearch()
        {
            string returnVa = string.Empty;
            if (!String.IsNullOrEmpty(m_RangeSearch))
            {
                switch (m_RangeSearch)
                {
                    case "range_audit": // 待审核 
                        returnVa = GetFilterByRole(m_RoleID, m_FuncCode) + "AND Attribs IN(0,1,3,6) ";
                        break;
                    case "range_ok": // 审核通过,可发证 
                        returnVa = " Attribs=2 ";
                        break;
                    case "range_no": // 驳回
                        returnVa = " Attribs=3 ";
                        break;
                    case "range_all": // 所有可见的 
                        returnVa = " 1=1 ";
                        break;
                    case "range_save": // 归档
                        returnVa = " Attribs=9 ";
                        break;
                    case "range_1": // 流程节点
                        returnVa = " CurrentStep=1 ";
                        break;
                    case "range_2":
                        returnVa = " CurrentStep=2";
                        break;
                    case "range_3":
                        returnVa = " CurrentStep=3 ";
                        break;
                    case "range_4":
                        returnVa = " CurrentStep=4 ";
                        break;
                    case "range_5":
                        returnVa = " CurrentStep=5 ";
                        break;
                    case "range_6":
                        returnVa = " CurrentStep=6 ";
                        break;
                    default:
                        returnVa = "";
                        break;
                }
            }

            return returnVa;
        }

        /// <summary>
        /// 页面通用搜索
        /// </summary>
        /// <param name="Fields"></param>
        /// <param name="Format"></param>
        /// <returns></returns>
        private string GetPageSearch(string Fields, string Format)
        {
            string[] a_Fields = Fields.Split(',');
            string[] a_Format = Format.Split(',');

            string returnSQL = string.Empty;
            string objSelValue = string.Empty;
            string objTxtValue = string.Empty;
            for (int i = 0; i < a_Fields.Length; i++)
            {
                objSelValue = Request["sel" + a_Fields[i]];

                if (a_Format[i].Trim() == "1") { objTxtValue = PageValidate.GetTrim(Request["txt" + a_Fields[i] + "Start"]); }
                else { objTxtValue = PageValidate.GetTrim(Request["txt" + a_Fields[i]]); }
                if (!String.IsNullOrEmpty(objTxtValue))
                {
                    if (objTxtValue.ToLower() == "null") objTxtValue = "";
                    // 字符格式 0 文本,1 日期,2 数字
                    if (a_Format[i].Trim() == "0")
                    {
                        if (objSelValue.ToLower().IndexOf("like") > -1) { returnSQL += a_Fields[i] + " " + objSelValue + " '%" + objTxtValue + "%' AND "; }
                        else { returnSQL += a_Fields[i] + " " + objSelValue + " '" + objTxtValue + "' AND "; }
                    }
                    else if (a_Format[i].Trim() == "1")
                    {
                        string startDate = Request["txt" + a_Fields[i] + "Start"];
                        string endDate = Request["txt" + a_Fields[i] + "End"];
                        if (!string.IsNullOrEmpty(startDate)) startDate = DateTime.Parse(startDate).AddDays(-1).ToString("yyyy-MM-dd");
                        if (!string.IsNullOrEmpty(endDate)) endDate = DateTime.Parse(endDate).AddDays(1).ToString("yyyy-MM-dd");
                        //returnSQL += a_Fields[i] + " > '" + startDate + "' AND " + a_Fields[i] + " < '" + endDate + "' AND ";
                        returnSQL += "ISDATE(" + a_Fields[i] + ")=1 AND CAST(" + a_Fields[i] + " As smalldatetime) > '" + startDate + "' AND CAST(" + a_Fields[i] + " As smalldatetime) < '" + endDate + "' AND ";
                    }
                    else { returnSQL += a_Fields[i] + " " + objSelValue + " " + objTxtValue + " AND "; }
                }
            }
            if (!String.IsNullOrEmpty(returnSQL) && returnSQL.IndexOf("AND") > 0) returnSQL = returnSQL.Substring(0, returnSQL.Length - 4);
            return returnSQL;
        }
    }
}
