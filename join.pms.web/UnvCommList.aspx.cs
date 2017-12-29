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
    public partial class UnvCommList : System.Web.UI.Page
    {
        private string m_FuncCode;
        private string m_FuncName;
        private string m_FuncUser;
        private string m_TabPageNo;
        private string m_PageSearch;
        private string m_RangeSearch;

        private string m_UserID; // 当前登录的操作用户编号
        private string m_UserDeptCode;//部门编码
        private string m_UserDeptName;//部门名称
        private string m_UserAreaCode;//区划编码
        private string m_UserAreaName;//区划名称
        private string m_RoleID;
        private string m_FuncTreeName;

        private string m_AreaNo = System.Configuration.ConfigurationManager.AppSettings["AreaNo"];
        private string m_AreaVal = System.Configuration.ConfigurationManager.AppSettings["AreaVal"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            SetPageStyle(m_UserID);
            ValidatePageParams();
        }


        /// <summary>
        /// 身份验证
        /// </summary>


        /// 设置并保存用户登陆信息
        /// </summary>
        /// <param name="userID"></param>


        #region 身份验证、参数校验等
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
                        //Response.Write("<script language='javascript'>parent.location.href='" + pageUrl + "&action=relogin';</script>");
                        //Response.End();
                        MessageBox.ShowAndRedirect(this.Page, "", pageUrl, true);
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/loginTemp.aspx';</script>");
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 页面参数验证处理
        /// </summary>
        private void ValidatePageParams()
        {
            m_FuncCode = Request.QueryString["FuncCode"];// 访问此数据列表的功能编号
            m_FuncName = Request.QueryString["FuncNa"];//功能名称
            m_FuncUser = Request.QueryString["FuncUser"];
            m_TabPageNo = Request.QueryString["TabPage"];
            m_PageSearch = Request.QueryString["pSearch"];//通用搜索
            m_RangeSearch = Request.QueryString["searchRange"];// 范围搜索
            string pageNo = Request.QueryString["p"];
            string searchKey = Request.QueryString["k"];
            string userSearchKey = Request.QueryString["searchKey"];

            string urlParams = "FuncUser=" + m_FuncUser + "&FuncCode=" + m_FuncCode + "&FuncNa=" + Server.UrlEncode(m_FuncName);


            if (String.IsNullOrEmpty(m_TabPageNo)) m_TabPageNo = "0";
            if (String.IsNullOrEmpty(pageNo)) pageNo = "1";
            if (String.IsNullOrEmpty(m_FuncCode) || !PageValidate.IsNumber(pageNo))
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

                GetUserInfo();
                GetDataGrid(searchKey, m_FuncCode, pageNo, urlParams);
                // 查看日志
                DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "数据查看", "用户于 " + DateTime.Now.ToString() + " 查看了[ " + m_FuncTreeName + " ]的数据");
            }
        }

        private void GetUserInfo()
        {
            string[] aryUserInfo = new string[10];
            // RoleID,UserAccount,UserName,DeptCode,DeptName,UserUnitName,UserAreaCode,UserAreaName,UserTel,UserWeiXinNo
            if (CommPage.GetUserInfoByID(m_UserID, ref aryUserInfo))
            {
                m_RoleID = aryUserInfo[0];
                m_UserDeptCode = aryUserInfo[3];
                m_UserDeptName = aryUserInfo[4];
                m_UserAreaCode = aryUserInfo[6];
                m_UserAreaName = aryUserInfo[7];
            }
            else
            {
                Response.Write("非法访问：未能取得用户信息数据，操作被终止！");
                Response.End();
            }
            /*
            1	系统管理员, 2业务管理-区县, 3业务处理-区县
            4	业务处理-镇办
            5	业务处理-社区/村,6	业务处理-医院
            */
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

        #endregion


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

            string configFile = Server.MapPath(GetConfigFileName(funcNo));
            string pageSize = System.Configuration.ConfigurationManager.AppSettings["GridPageSize"]; // 每页显示的记录数
            string funcPowers = string.Empty;//  // 功能的权限集 

            if (String.IsNullOrEmpty(funcNo) || string.IsNullOrEmpty(m_UserAreaCode))
            {
                LiteralDataList.Text = "操作失败：参数错误!";
                return;
            }
            //根据用户归属部门判断操作权限
            funcPowers = DbHelperSQL.GetFuncPower(m_UserID, funcNo);
            //funcPowers = "1,1,1,1,1,1,1,1,1"; //初始放开所有功能权限
            // 判断乡镇信息的权限:乡镇用户有其设置的权限,其它用户除管理员外只能查看
            //if (int.Parse(m_UserID)>3 && funcNo.Length > 2)
            //{
            //    if (funcNo.Substring(0, 2) == "03")
            //    {
            //        if (m_UserDeptCodeCode == funcNo) { funcPowers = DbHelperSQL.GetFuncPower(m_UserID, funcNo); }
            //        else {funcPowers = "0,0,0,0,0,0,0,0,0";}
            //    }
            //    else { funcPowers = DbHelperSQL.GetFuncPower(m_UserID, funcNo); }
            //}
            //else { funcPowers = DbHelperSQL.GetFuncPower(m_UserID, funcNo); }

            //=================
            searchSQL = GetSearchByFuncNo(funcNo, m_UserID);

            if (funcNo != "")
            {
                //各区划管理自己的报表
                if (funcNo.Substring(0, 4) == "0301" || funcNo.Substring(0, 4) == "0302")
                {
                    searchSQL += " and AreaCode = '" + m_UserAreaCode + "' and FuncNo ='" + funcNo + "'";
                }
            }


            join.pms.dal.DataList pageList = new join.pms.dal.DataList();

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
                    // ========== 通用搜索 Start ===========>
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
                    pageList.Url = "UnvCommList.aspx?1=1&pSearch=" + DESEncrypt.Encrypt(pageSearch) + "&UserID=" + m_UserID + "&" + urlParams;
                    //this.txtUrlParams.Value = DESEncrypt.Encrypt("pSearch=" + DESEncrypt.Encrypt(pageSearch) + "&UserID=" + m_UserID + "&" + urlParams);

                    pageList.SearchKeys = Server.UrlEncode(searchKeys);
                    pageList.SearchType = "";
                    pageList.SearchWhere = searchSQL;
                    // 分页数据
                    this.LiteralDataList.Text = pageList.GetList();
                    this.m_FuncTreeName = pageList.FuncTreeName;

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
                //if (funcNo.Substring(0, 2) == "01")
                //{
                //    returnVa = "/includes/DataGridShare.config";     // 数据共享配置文件
                //}
                //else if (funcNo.Substring(0, 2) == "02")
                //{
                //    returnVa = "/includes/DataGridBiz.config";  //业务办理配置文件
                //}
                //else
                //{
                returnVa = "/includes/DataGrid.config";
                //}
            }
            else
            {
                Server.Transfer("errors.aspx");
            }
            return returnVa;
        }

        /// <summary>
        /// 获取调用的配置节编码
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetConfigCode(string funcNo)
        {
            string returnVa = string.Empty;
            if (!string.IsNullOrEmpty(funcNo) && funcNo.Length > 2)
            {
                if (funcNo.Substring(0, 2) == "04")
                {
                    returnVa = funcNo.Substring(0, 2);
                }
                else if (funcNo.Substring(0, 4) == "1004")
                {
                    returnVa = "NOTES";
                }
                else if (funcNo.Length > 3 && funcNo.Substring(0, 4) == "0103")
                {
                    returnVa = "0103";//标准数据
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
        /*
         
1	系统管理员	最高权限，行使系统管理功能
2	日常管理	日常管理角色
3	卫计局	共享部门角色
4	民政局	共享部门角色
5	公安局	共享部门角色
6	人社局	共享部门角色
7	教体局	共享部门角色
8	统计局	共享部门角色
9	镇办用户	乡镇/街办用户
10	村居用户	社区/村级用户

         */
        /// <summary>
        /// 根据功能编号显示内容
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetSearchByFuncNo(string funcNo, string userID)
        {
            string returnVa = string.Empty;
            if (funcNo.Length > 2)
            {
                //if (funcNo.Substring(0, 4) == "0102")
                //{
                //    //信息共享
                //    returnVa = "FuncNo='" + funcNo + "'";
                //    if (funcNo == "01020208")
                //    {

                //        returnVa = "FuncNo='01010208' AND AnalysisFlag=2 AND CommMemo='此人已死亡'";//享受城镇低保死亡人员
                //    }
                //    else if (funcNo == "01020209")
                //    {

                //        returnVa = "FuncNo='01010209' AND AnalysisFlag=2 AND CommMemo='此人已死亡'";
                //    }
                //    else if (funcNo == "01020210")
                //    {

                //        returnVa = "FuncNo='01010210' AND AnalysisFlag=2 AND CommMemo='此人已死亡'";
                //    }
                //    else
                //    {
                //        // 乡镇用户只看本乡镇差异数据
                //        if (m_UserDeptCode.Substring(0, 2) == "03")
                //        {
                //            returnVa = "AnalysisFlag=2 AND FuncNo='0101" + funcNo.Substring(4, 4) + "' AND " + GetXiangZhenFilter(m_UserDeptCode, m_UserDeptName, "AreaName");
                //            //returnVa = "FuncNo='" + funcNo + "' AND " + GetXiangZhenFilter(m_UserDeptCode, m_UserDeptName, "AreaName");
                //        }
                //        else
                //        {
                //            returnVa = "AnalysisFlag=2 AND FuncNo='0101" + funcNo.Substring(4, 4) + "'";
                //            //returnVa = "FuncNo='" + funcNo + "'";
                //        }
                //    }
                //}
                //else if (funcNo.Substring(0, 4) == "0103")
                //{
                //    //标准数据分镇办
                //    returnVa = GetAreaCodeByFuncNo(funcNo, m_FuncName, m_UserAreaCode, "AreaName");
                //}
                //else if (funcNo.Substring(0, 2) == "02")
                //{
                //    //业务事项办理 在单独的业务事项目录中实现
                //    returnVa = "";
                //}
                //else if (funcNo.Substring(0, 2) == "03")
                //{
                //    //业务事项办理 在单独的业务事项目录中实现
                //    if (funcNo == "0306" || funcNo == "0307" || funcNo == "0308" || funcNo == "0309")
                //    {
                //        if (m_RoleID == "5")
                //        {
                //            returnVa = "FuncNo='" + funcNo + "' and AreaCode='" + m_UserAreaCode + "'";
                //        }
                //        else if (m_RoleID == "4")
                //        {
                //            returnVa = "FuncNo='" + funcNo + "' and AreaCode LIKE '" + m_UserAreaCode.Substring(0, 9) + "%'";
                //        }
                //        else
                //        {
                //            returnVa = "FuncNo='" + funcNo + "'";
                //        }
                //    }
                //    else
                //    {
                //        returnVa = "FuncNo='" + funcNo + "'";
                //    }
                //}
                //else if (funcNo.Substring(0, 2) == "04")
                //{
                //    //公共信息
                //    if (m_RoleID == "1")
                //    {
                //        if (funcNo == "0403") { returnVa = " CmsCode LIKE '" + funcNo + "%' AND CmsAttrib!=4"; }
                //        else { returnVa = " CmsCode='" + funcNo + "' AND CmsAttrib!=4"; }
                //    }
                //    else
                //    {
                //        if (funcNo == "0403") { returnVa = " CmsCode LIKE '" + funcNo + "%' AND CmsAttrib!=4"; }
                //        else { returnVa = " CmsCode='" + funcNo + "' AND CmsAttrib!=4"; }
                //        //根据用户归属部门判断操作权限
                //        //if (m_UserDeptCode == funcNo)
                //        //{
                //        //    returnVa = " CmsCode='" + funcNo + "' AND CmsAttrib!=4";
                //        //}
                //        //else
                //        //{
                //        //    returnVa = " (CmsCode='" + funcNo + "' AND CmsAttrib=9) OR (CmsCode='" + funcNo + "' AND UserID=" + m_UserID + " AND CmsAttrib!=4) ";
                //        //}
                //    }
                //}
                //else if (funcNo == "xx")
                //{
                //    if (m_UserDeptCode.Substring(0, 2) == "03")
                //    {
                //        //returnVa = "P_Addres LIKE '%" + m_UserDeptCodeName + "%'"; 标准数据
                //        returnVa = GetXiangZhenFilter(m_UserDeptCode, m_UserDeptName, "AreaName");
                //    }
                //    else
                //    {
                //        returnVa = "";
                //    }
                //}
                //else
                //{
                    switch (funcNo)
                    {
                        case "04":  // 公共信息 CmsAttrib:0 默认;1 审核; 3 屏蔽; 4 删除; 9 公开 P_Addres
                            returnVa = " CmsCode LIKE '01%' AND CmsAttrib=9";
                            break;
                        case "0403": // 乡镇信息
                            returnVa = " CmsCode LIKE '0403%' AND CmsAttrib=9";
                            break;
                        case "060301": // 发件箱
                            returnVa = " SourceDel=0 AND SourceUserID=" + m_UserID + " "; // (DelFlag<>0 OR DelFlag IS NULL)
                            break;
                        case "060302": // 收件箱
                            returnVa = " TargetDel=0 AND TargetUserID=" + m_UserID + " "; //
                            break;
                        case "060303": // 回收站
                            returnVa = "(SourceUserID=" + m_UserID + " OR TargetUserID=" + m_UserID + ") AND (SourceDel!=0 OR TargetDel!=0)";
                            break;
                        case "060105": // 系统日志
                            returnVa = " OprateModel !='数据修改'";
                            break;
                        case "060106": // 修改日志
                            returnVa = " OprateModel ='数据修改'";
                            break;
                        case "0102": // 修改日志
                            returnVa = " ";
                            break;
                        default: // 默认显示当前功能所有内容OprateModel ='数据修改'
                            returnVa = GetXlsFilter(funcNo);
                            break;
                    }
               //}
            }
            else { returnVa = ""; }

            return returnVa;
        }

        /// <summary>
        /// 依据功能号获取行政区划编码，近限于区分乡镇的功能菜单
        /// </summary>
        /// <returns></returns>
        private string GetAreaCodeByFuncNo(string funcNo, string funcNa, string deptCode, string keyFiled)
        {
            string returnVa = string.Empty;
            /*
610922100000	城关镇
610922101000	饶峰镇
610922102000	两河镇
610922103000	迎丰镇
610922104000	池河镇
610922105000	后柳镇
610922106000	喜河镇
610922107000	熨斗镇
610922108000	云雾山镇
610922109000	曾溪镇
610922110000	中池镇

             */
            switch (funcNo)
            {
                case "0103":
                    returnVa = "";
                    break;
                case "010301":
                    returnVa = "(AreaCode LIKE '610922100%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010302":
                    returnVa = "(AreaCode LIKE '610922101%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010303":
                    returnVa = "(AreaCode LIKE '610922102%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010304":
                    returnVa = "(AreaCode LIKE '610922103%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010305":
                    returnVa = "(AreaCode LIKE '610922104%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010306":
                    returnVa = "(AreaCode LIKE '610922105%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010307":
                    returnVa = "(AreaCode LIKE '610922106%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010308":
                    returnVa = "(AreaCode LIKE '610922107%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010309":
                    returnVa = "(AreaCode LIKE '610922108%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010310":
                    returnVa = "(AreaCode LIKE '610922109%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010311":
                    returnVa = "(AreaCode LIKE '610922110%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                default:
                    returnVa = "";
                    break;
            }
            return returnVa;
        }

        /// <summary>
        /// 兼容乡镇名称变更的问题 2013/07/30 by ysl
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="deptName"></param>
        /// <param name="keyFiled"></param>
        /// <returns></returns>
        private string GetXiangZhenFilter(string deptCode, string deptName, string keyFiled)
        {
            //2013/10/10 by Ysl 查询数据镇办地址匹配
            if (this.m_FuncCode == "020201" || this.m_FuncCode == "0506") { keyFiled = "Fileds14"; }
            else if (this.m_FuncCode == "0502") { keyFiled = "Fileds10"; }
            else if (this.m_FuncCode == "0503" || this.m_FuncCode == "0504" || this.m_FuncCode == "0505") { keyFiled = "Fileds13"; }
            else if (this.m_FuncCode == "0513" || this.m_FuncCode == "0514") { keyFiled = "Fileds05"; }
            else if (this.m_FuncCode == "0515" || this.m_FuncCode == "0516") { keyFiled = "Fileds03"; }
            else { }
            /*
            if (deptCode == "0303")
            {
                return " (" + keyFiled + " LIKE '%赤水%' OR " + keyFiled + " LIKE '%辛庄%') ";
            }
            else if (deptCode == "0304")
            {
                return " (" + keyFiled + " LIKE '%高塘%' OR " + keyFiled + " LIKE '%东阳%') ";
            }
            else if (deptCode == "0305")
            {
                return " (" + keyFiled + " LIKE '%大明%' OR " + keyFiled + " LIKE '%金惠%') ";
            }
            else if (deptCode == "0308")
            {
                return " (" + keyFiled + " LIKE '%柳枝%' OR " + keyFiled + " LIKE '%毕家%') ";
            }
            else
            {
                if (deptName.Length > 3) { return " ( " + join.pms.web.Biz.CommPage.GetAreaMatch(m_AreaNo, m_AreaVal, m_FuncCode, deptName) + " OR " + keyFiled + " LIKE '%" + deptName.Substring(0, 3) + "%' ) "; }
                else { return " (" + join.pms.web.Biz.CommPage.GetAreaMatch(m_AreaNo, m_AreaVal, m_FuncCode, deptName) + " OR " + keyFiled + " LIKE '%" + deptName + "%' ) "; }
            }*/

            if (deptName.Length > 3) { return " ( " + join.pms.dal.CommPage.GetAreaMatch(m_AreaNo, m_AreaVal, m_FuncCode, deptName) + " OR " + keyFiled + " LIKE '%" + deptName.Substring(0, 3) + "%' ) "; }
            else { return " (" + join.pms.dal.CommPage.GetAreaMatch(m_AreaNo, m_AreaVal, m_FuncCode, deptName) + " OR " + keyFiled + " LIKE '%" + deptName + "%' ) "; }

        }

        private string GetXlsFilter(string funcNo)
        {

            if (funcNo.Substring(0, 2) == "01")
            {
                if (m_RoleID == "1")
                {
                    //return " FuncNo='" + funcNo + "' ";
                    return "";
                }
                else
                {
                    //根据用户归属部门判断操作权限 Fileds15
                    if (m_UserDeptCode == funcNo.Substring(0, 4))
                    {
                        if (m_UserDeptCode.Substring(0, 2) == "03")
                        {
                            return " FuncNo='" + funcNo + "' AND (AreaCode LIKE '" + m_UserAreaCode.Substring(0, 9) + "%' OR " + GetXiangZhenFilter(m_UserDeptCode, m_UserDeptName, "AreaName") + ")  ";
                        }
                        else
                        {
                            return " FuncNo='" + funcNo + "' ";
                        }
                    }
                    else
                    {
                        if (m_UserDeptCode.Substring(0, 2) == "03")
                        {
                            return " FuncNo='" + funcNo + "' AND (AreaCode LIKE '" + m_UserAreaCode.Substring(0, 9) + "%' OR " + GetXiangZhenFilter(m_UserDeptCode, m_UserDeptName, "AreaName") + ") ";
                        }
                        else
                        {
                            return " FuncNo='" + funcNo + "'";// return " FuncNo='" + funcNo + "' AND AuditFlag=9 ";
                        }
                    }
                }
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 从功能编码获取CMS编码
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetCmsCode(string funcNo)
        {
            string returnVa = string.Empty;
            if (!string.IsNullOrEmpty(funcNo) && funcNo.Length > 2)
            {
                // 010402	内容发布
                if (funcNo.Substring(0, 2) == "04") returnVa = "04";// funcNo.Replace("0104", "");
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
                    case "range_all": // 所有可见的
                        returnVa = "ClassCode LIKE '" + m_FuncCode + "__'";
                        break;
                    case "range_mark": // 我负责的
                        returnVa = "ParaCate=11";
                        break;
                    case "range_creator": // 我创建的
                        returnVa = "ForumCode LIKE '" + m_FuncCode + "%'";
                        break;
                    case "range_underling": // 下属的
                        returnVa = "ForumCode LIKE '" + m_FuncCode + "%'";
                        break;
                    case "range_dept": // 我部门的
                        returnVa = "ForumCode LIKE '" + m_FuncCode + "%'";
                        break;
                    case "range_share": // 共享给我的
                        returnVa = "ForumCode LIKE '" + m_FuncCode + "%'";
                        break;
                    default:
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

