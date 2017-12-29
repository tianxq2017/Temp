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

namespace join.pms.web
{
    public partial class PhotoList : System.Web.UI.Page
    {
        private string m_FuncCode;
        private string m_FuncName;
        private string m_FuncUser;
        private string m_BizID;

        private string m_UserID; // 当前登录的操作用户编号
        private string m_UserDept;//用户部门编码
        private string m_UserDeptName;//部门名称
        private string m_UserDeptArea;//镇办区划
        private string m_RoleID;


        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            //SetPageStyle(m_UserID);
            ValidatePageParams();

        }

        #region 身份验证和参数过滤

        private void ValidatePageParams() {
            string pageNo = "", searchKey = "", userSearchKey = "", urlParams = string.Empty;
            string[] aryUserInfo = new string[8];

            //m_FuncCode = Request.QueryString["FuncCode"];// 访问此数据列表的功能编号
            m_FuncCode = "0502";
            m_FuncName = Request.QueryString["FuncNa"];//功能名称
            m_FuncUser = Request.QueryString["FuncUser"];
            m_BizID = Request.QueryString["RID"];
            pageNo = Request.QueryString["p"];
            searchKey = Request.QueryString["k"];
            userSearchKey = Request.QueryString["searchKey"];
            urlParams = "FuncUser=" + m_FuncUser + "&FuncCode=" + m_FuncCode + "&FuncNa=" + Server.UrlEncode(m_FuncName)+"&BizID="+m_BizID;

            if (String.IsNullOrEmpty(pageNo)) pageNo = "1";
            if (String.IsNullOrEmpty(m_FuncCode) || !PageValidate.IsNumber(pageNo))
            {
                Response.Write("非法访问：操作被终止！");
                Response.End();
            }
            else
            {
                if (!String.IsNullOrEmpty(userSearchKey)) searchKey = userSearchKey;
                try
                {
                    if (CommPage.GetUserInfoByID(m_UserID, ref aryUserInfo))
                    {
                        // RoleID,UserAccount,UserName,DeptCode,DeptName,UserUnitName,UserAreaCode,UserAreaName
                        m_RoleID = aryUserInfo[0];
                        m_UserDept = aryUserInfo[3];
                        m_UserDeptName = aryUserInfo[4];
                        m_UserDeptArea = aryUserInfo[6];

                        if (String.IsNullOrEmpty(m_FuncCode) || string.IsNullOrEmpty(m_UserDept))
                        {
                            this.LiteralDataList.Text = "操作失败：参数错误!";
                        }
                        else
                        {
                            GetDataGrid(searchKey, m_FuncCode, pageNo, urlParams, m_BizID);
                            // 查看日志
                            DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "数据查看", "用户于 " + DateTime.Now.ToString() + " 查看了[ 电子证照 ]的数据");
                        }
                    }
                    else
                    {
                        this.LiteralDataList.Text = "操作提示：获取用户信息错误……";
                    }
                }
                catch { }
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
                        Response.Write("<script language='javascript'>parent.location.href='/Default.shtml?action=closewindow';</script>");
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "", pageUrl, true);
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
                    Response.End();
                }
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

        #endregion

        /// <summary>
        /// 获取数据列表及数据列表组件调用示例
        /// 杨胜灵 2008/08/30 
        /// </summary>
        /// <param name="searchKeys"></param>
        /// <param name="funcNo"></param>
        /// <param name="pageNo"></param>
        private void GetDataGrid(string searchKeys, string funcNo, string pageNo, string urlParams,string bizID)
        {
            string errorMsg = "", searchSQL = "", pageSearch = "", configFile = "", pageSize = "", funcPowers = string.Empty;
            
            configFile = Server.MapPath("/includes/DataGrid.config");
            pageSize = System.Configuration.ConfigurationManager.AppSettings["CerPageSize"]; // 每页显示的记录数
            
            //根据用户归属部门判断操作权限
            funcPowers = DbHelperSQL.GetFuncPower(m_UserID, funcNo);
            searchSQL = GetSearchByFuncNo(funcNo, m_UserID, bizID);

            join.pms.dal.DataList pageList = new join.pms.dal.DataList();

            if (pageList.GetConfigParams("05022", configFile, ref errorMsg))
            {
                if (!String.IsNullOrEmpty(searchKeys) && !String.IsNullOrEmpty(pageList.SearchFields))
                {
                    if (!String.IsNullOrEmpty(searchSQL)) { searchSQL += " AND " + pageList.SearchFields + " LIKE '%" + searchKeys + "%' "; }
                    else { searchSQL += " " + pageList.SearchFields + " LIKE '%" + searchKeys + "%' "; }
                }
                try
                {
                    pageList.PageSize = int.Parse(pageSize);
                    pageList.PageNo = int.Parse(pageNo);
                    pageList.FuncPowers = funcPowers;
                    pageList.FuncNo = funcNo;

                    if (!String.IsNullOrEmpty(pageSearch))
                    {
                        if (string.IsNullOrEmpty(searchSQL)) { searchSQL += pageSearch; }
                        else { searchSQL += " AND " + pageSearch; }
                    }
                    // ========== 通用搜索 End ===========>
                    pageList.Url = "PhotoList.aspx?1=1&pSearch=" + DESEncrypt.Encrypt(pageSearch) + "&UserID=" + m_UserID + "&" + urlParams;
                    //this.txtUrlParams.Value = DESEncrypt.Encrypt("pSearch=" + DESEncrypt.Encrypt(pageSearch) + "&UserID=" + m_UserID + "&" + urlParams);

                    pageList.SearchKeys = Server.UrlEncode(searchKeys);
                    pageList.SearchType = "";
                    pageList.SearchWhere = searchSQL;
                    // 分页数据
                    this.LiteralDataList.Text = pageList.GetPhotosList();

                    if (!String.IsNullOrEmpty(urlParams)) this.txtUrlParams.Value = DESEncrypt.Encrypt("pSearch=" + DESEncrypt.Encrypt(pageSearch) + "&UserID=" + m_UserID + "&" + urlParams + "&p=" + pageNo);//保持操作后的页码
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

        #region 数据列表辅助方法
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
        /// 根据功能编号显示内容
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetSearchByFuncNo(string funcNo, string userID,string BizID)
        {
            string returnVa = string.Empty;
            switch (funcNo)
            {
                case "0502": // 电子证照
                    if (m_RoleID == "1") { returnVa = " DocsType IN('.jpg','.gif','.png','.bmp')   AND BizID='" + BizID + "'"; }
                    else
                    {
                        if (m_UserDept.Length == 2) { returnVa = " DocsType IN('.jpg','.gif','.png','.bmp') AND RegisterAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' AND BizID='" + BizID + "'"; }
                        else if (m_UserDept.Length == 4) { returnVa = " DocsType IN('.jpg','.gif','.png','.bmp') AND RegisterAreaCode LIKE '" + m_UserDeptArea.Substring(0, 9) + "%'  AND BizID='" + BizID + "'"; }
                        else { returnVa = " DocsType IN('.jpg','.gif','.png','.bmp') AND RegisterAreaCode = '" + m_UserDeptArea + "'  AND BizID='" + BizID + "'"; }
                    }
                    break;
                case "0503": // 材料文档
                    if (m_RoleID == "1") { returnVa = " DocsType NOT IN('.jpg','.gif','.png','.bmp')  "; }
                    else
                    {
                        if (m_UserDept.Length == 2) { returnVa = " DocsType NOT IN('.jpg','.gif','.png','.bmp') AND RegisterAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' "; }
                        else if (m_UserDept.Length == 4) { returnVa = " DocsType NOT IN('.jpg','.gif','.png','.bmp') AND RegisterAreaCode LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' "; }
                        else { returnVa = " DocsType NOT IN('.jpg','.gif','.png','.bmp') AND RegisterAreaCode = '" + m_UserDeptArea + "' "; }
                    }
                    break;
                default: // 默认显示当前功能所有内容
                    returnVa = GetXlsFilter(funcNo);
                    break;
            }

            return returnVa;
        }


        private string GetXlsFilter(string funcNo)
        {

            return "";
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
        #endregion
    }
}
