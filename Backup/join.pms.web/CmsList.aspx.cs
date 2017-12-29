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

using System.Text;
using UNV.Comm.DataBase;
using join.pms.dal;
using UNV.Comm.Web;

namespace join.pms.web
{
    public partial class CmsList : UNV.Comm.Web.PageBase
    {
        private string m_UserID;

        private string m_SqlParams;
        private DataTable m_Dt;

        private string m_FuncNo;
        private string m_FuncNa;

        private string m_PageNo;
        private string m_SearchKeys;
        private string m_PageSearch;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();

            ValidatePageParams();

            GetNavInfo();
            // CmsList.aspx?1=1&FuncNo=0204
            m_PageSearch = Request.QueryString["pSearch"];//通用搜索
            string urlParams = "FuncCode=" + m_FuncNo;
            if (!String.IsNullOrEmpty(urlParams)) this.txtUrlParams.Value = DESEncrypt.Encrypt(urlParams);
            GetDataList(m_FuncNo, m_PageNo, m_SearchKeys, urlParams);
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
        private void ValidatePageParams()
        {
            m_FuncNo = PageValidate.GetTrim(Request.QueryString["FuncCode"]);
            m_PageNo = PageValidate.GetTrim(Request.QueryString["p"]);
            if (string.IsNullOrEmpty(m_PageNo)) m_PageNo = "1"; // 页码默认为第一页
            if (!string.IsNullOrEmpty(m_FuncNo) && PageValidate.IsNumber(m_FuncNo) && PageValidate.IsNumber(m_PageNo))
            {
                
            }
            else
            {
                Server.Transfer("/errors.aspx");
            }
        }
        /// <summary>
        /// 导航
        /// </summary>
        private void GetNavInfo()
        {
            StringBuilder sHtml = new StringBuilder();
            try {
                m_FuncNa = DbHelperSQL.GetSingle("SELECT FuncName FROM [SYS_Function] WHERE FuncCode='"+m_FuncNo+"'").ToString();
                sHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
                sHtml.Append("<td width=\"300\" height=\"34\" class=\"f16\">");
                sHtml.Append("<span  style=\"font-size:16px;color:3f762c;\"><strong>" + m_FuncNa + "</strong></span>");
                sHtml.Append("</td>");
                sHtml.Append("<td align=\"right\" >&nbsp;</td>"); 
                sHtml.Append("<td width=\"480\" align=\"right\">");
                sHtml.Append("&nbsp;");
                sHtml.Append("</td></tr></table>");

            }catch(Exception ex){
                Response.Write(ex.Message);
                return;
            }
            
            this.LiteralNav.Text = sHtml.ToString();
            sHtml = null;
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="funcNo"></param>
        /// <param name="pageNo"></param>
        /// <param name="searchKeys"></param>
        private void GetDataList(string funcNo, string pageNo, string searchKeys, string urlParams)
        {
            string errorMsg = string.Empty;
            string searchSQL = string.Empty;
            string pageSearch = string.Empty;
            string configFile = Server.MapPath("includes/DataGrid.config");
            string pageSize = System.Configuration.ConfigurationManager.AppSettings["CmsPageSize"];

            searchSQL = GetFilterByFuncNo(funcNo);

            join.pms.dal.DataList pageList = new join.pms.dal.DataList();
            pageList.BizID = "0";
            pageList.FuncNo = funcNo;
            //pageList.FileExt = this.m_FileExt;
            pageList.FuncPowers = "1,1,1,1,1,1,1,1,1";

            if (pageList.GetConfigParams(funcNo, configFile, ref errorMsg))
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
                    pageList.SearchKeys = Server.UrlEncode(searchKeys);
                    pageList.SearchType = "";
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
                        if (!String.IsNullOrEmpty(searchSQL)) { searchSQL += " AND " + pageSearch; }
                        else { searchSQL += pageSearch; }
                    }
                    // ========== 通用搜索 End ===========>
                    //pageList.Url = "UnvCommList.aspx?1=1&pSearch=" + DESEncrypt.Encrypt(pageSearch) + "&UserID=" + m_UserID + "&" + urlParams;
                    pageList.Url = "CmsList.aspx??1=1&pSearch=" + DESEncrypt.Encrypt(pageSearch) + "&" + urlParams;
                    pageList.SearchWhere = searchSQL;
                    this.LiteralDataList.Text = pageList.GetList();
                    //if (!String.IsNullOrEmpty(urlParams)) this.txtUrlParams.Value = DESEncrypt.Encrypt(urlParams + "&p=" + pageNo);//保持操作后的页码
                }
                catch (Exception ex)
                {
                    this.LiteralDataList.Text = ex.Message;
                }
            }
            else
            {
                this.LiteralDataList.Text = errorMsg;
            }
            pageList = null;
        }

        /// <summary>
        /// 根据显示功能过滤
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetFilterByFuncNo(string funcNo)
        {
            string returnVa = string.Empty;
            switch (funcNo)
            {
                case "020201": // 发件箱 CmsAttrib
                    returnVa = " (DelFlag<>0 OR DelFlag IS NULL) AND SourceUserID=" + m_UserID + " ";
                    break;
                case "020202": // 收件箱
                    returnVa = " (DelFlag<>1 OR DelFlag IS NULL) AND TargetUserID=" + m_UserID + " ";
                    break;
                case "020203": // 回收站
                    returnVa = "(SourceUserID=" + m_UserID + " OR TargetUserID=" + m_UserID + ") AND (DelFlag=0 OR DelFlag=1)";
                    break;
                case "0203": // 工作信息
                    returnVa = "(CmsAttrib=1 OR CmsAttrib=2 OR CmsAttrib=9) AND CmsCode='02'";
                    break;
                case "0205": // 系统帮助
                    returnVa = "(CmsAttrib=1 OR CmsAttrib=2 OR CmsAttrib=9) AND CmsCode='03'";
                    break;
                default:
                    break;
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
                objTxtValue = PageValidate.GetTrim(Request["txt" + a_Fields[i]]);
                if (!String.IsNullOrEmpty(objTxtValue))
                {
                    // 字符格式 0 文本,1 日期,2 数字
                    if (a_Format[i].Trim() == "0")
                    {
                        if (objSelValue.ToLower().IndexOf("like") > -1) { returnSQL += a_Fields[i] + " " + objSelValue + " '%" + objTxtValue + "%' AND "; }
                        else { returnSQL += a_Fields[i] + " " + objSelValue + " '" + objTxtValue + "' AND "; }
                    }
                    else { returnSQL += a_Fields[i] + " " + objSelValue + " " + objTxtValue + " AND "; }
                }
            }
            if (!String.IsNullOrEmpty(returnSQL) && returnSQL.IndexOf("AND") > 0) returnSQL = returnSQL.Substring(0, returnSQL.Length - 4);
            return returnSQL;
        }

    }
}
