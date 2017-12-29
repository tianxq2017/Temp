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
using System.Data.SqlClient;

using UNV.Comm.Web;
using UNV.Comm.DataBase;
using join.pms.dal;

namespace join.pms.wap.UserCenter
{
    public partial class DataList : UNV.Comm.Web.PageBase
    {
        private string m_UserID;
        private string m_PersonCardID;
        private string m_PersonTel;

        private string m_FuncCode;
        private string m_FuncName;
        private string m_PageNo;
        private string m_UrlParams;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateParams();
            AuthenticateUser();

            this.Uc_PageTop1.GetSysMenu("数据列表");
            if (!IsPostBack)
            {
                GetDataGrid(m_FuncCode, m_PageNo);
            }
        }

        #region 设置页头信息及导航\验证参数\验证用户等
        /// <summary>
        /// 验证接受的参数
        /// </summary>
        private void ValidateParams()
        {
            m_FuncCode = PageValidate.GetTrim(Request.QueryString["c"]);
            m_PageNo = PageValidate.GetTrim(Request.QueryString["p"]);
            if (string.IsNullOrEmpty(m_PageNo)) m_PageNo = "1"; // 页码默认为第一页
            if (!string.IsNullOrEmpty(m_FuncCode) && PageValidate.IsNumber(m_PageNo))
            {
                m_FuncName = GetFuncName(m_FuncCode);
                m_UrlParams = "/OC/" + m_FuncCode + "-" + m_PageNo + "." + m_FileExt;
                this.txtUrlPageNo.Value = m_PageNo;//保持操作后的页码
            }
            else
            {
                Server.Transfer("/errors.aspx");
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
                HttpCookie loginCookie = Request.Cookies["AREWEB_OC_PUBSVRS_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["UserID"] != null && !String.IsNullOrEmpty(Session["PersonID"].ToString())) { returnVa = true; m_UserID = Session["PersonID"].ToString(); }
            }
            if (!returnVa)
            {
                Response.Write("<script language='javascript'>alert(\"操作提示：请登录后再试！\");parent.location.href='/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh." + m_FileExt + "';</script>");
                Response.End();
            }
            else
            {
                GetUserInfo();
            }
        }
        #endregion

        #region 获取当前用户身份证号和手机号
        /// <summary>
        /// 获取当前用户身份证号和手机号
        /// </summary>
        private void GetUserInfo()
        {
            SqlDataReader sdr = null;
            string sqlParams = "SELECT PersonTel,PersonCardID FROM BIZ_Persons WHERE PersonID=" + this.m_UserID;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        this.m_PersonCardID = sdr["PersonCardID"].ToString();
                        this.m_PersonTel = sdr["PersonTel"].ToString();
                    }
                }
            }
            catch { if (sdr != null) { sdr.Close(); sdr.Dispose(); } }
        }
        #endregion

        #region 根据功能编号获取功能名称
        /// <summary>
        /// 根据功能编号获取功能名称
        /// </summary>
        /// <returns></returns>
        private string GetFuncName(string funcCode)
        {
            string returnVal = string.Empty;
            switch (funcCode)
            {
                case "02":
                    returnVal = "业务办理记录";
                    break;
                case "04":
                    returnVal = "业务提醒信息";
                    break;
                case "03":
                    returnVal = "服务收藏";
                    break;
                case "0502":
                    returnVal = "电子证照信息";
                    break;
                case "0503":
                    returnVal = "材料附件";
                    break;
                case "0601":
                    returnVal = "业务咨询";
                    break;
                case "0602":
                    returnVal = "服务投诉";
                    break;
                case "0603":
                    returnVal = "服务建议";
                    break;
                case "0604":
                    returnVal = "服务评价";
                    break;
                case "07":
                    returnVal = "个人子女信息";
                    break;
                case "08":
                    returnVal = "个人婚姻史";
                    break;
                default:
                    returnVal = "未指定栏目名称";
                    break;
            }
            return returnVal;
        }
        #endregion

        #region 获取数据列表及数据列表组件调用示例
        /// <summary>
        /// 获取数据列表及数据列表组件调用示例        
        /// </summary>
        /// <param name="funcNo"></param>
        /// <param name="pageNo"></param>
        private void GetDataGrid(string funcNo, string pageNo)
        {
            string errorMsg = string.Empty;
            string searchSQL = string.Empty;
            string configFile = Server.MapPath(GetConfigFileName(funcNo));
            string pageSize = System.Configuration.ConfigurationManager.AppSettings["GridPageSize"]; // 每页显示的记录数

            if (String.IsNullOrEmpty(funcNo))
            {
                LiteralDataList.Text = "操作失败：参数错误!";
                return;
            }

            searchSQL = GetSearchByFuncNo(funcNo, this.m_UserID);

            DataListForUserWap pageList = new DataListForUserWap();

            if (pageList.GetConfigParams(GetListCode(funcNo), configFile, ref errorMsg))
            {
                try
                {
                    pageList.PageSize = int.Parse(pageSize);
                    pageList.PageNo = int.Parse(pageNo);
                    pageList.FuncNo = funcNo;
                    pageList.FuncName = m_FuncName;
                    pageList.FileExt = this.m_FileExt;
                    pageList.OrderType = 1;
                    pageList.SearchWhere = searchSQL;
                    pageList.Url = "/OC/" + m_FuncCode + "-";

                    // 分页数据
                    LiteralDataList.Text = pageList.GetList();
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
                returnVa = "/includes/DataGridUser.config";
            }
            else
            {
                Server.Transfer("/errors.aspx");
            }
            return returnVa;
        }

        /// <summary>
        /// 通用CMS内容发布系统的编码 01
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetListCode(string funcNo)
        {
            string returnVa = string.Empty;
            if (funcNo.Length > 2)
            {
                if (funcNo.Substring(0, 2) == "06")
                {
                    returnVa = "NoteList";
                }
                else if (funcNo.Substring(0, 2) == "05")
                {
                    returnVa = "BizDocs";
                }
                else { returnVa = funcNo; }
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
            string fFuncNo = string.Empty;
            if (funcNo.Substring(0, 2) == "06") { fFuncNo = GetListCode(funcNo); }
            else { fFuncNo = funcNo; }
            switch (fFuncNo)
            {
                case "NoteList"://业务咨询 服务投诉 服务建议 服务评价
                    returnVa = "MsgCode='" + funcNo + "' AND Attribs!=4 AND MsgUserID=" + userID;
                    break;
                case "02"://业务办理记录
                    returnVa = "BizCode!='0106' AND BizCode!='0111' AND ((PersonID=" + m_UserID + " AND InitDirection=0) OR PersonCidA='" + m_PersonCardID + "' OR PersonCidB='" + m_PersonCardID + "') AND  Attribs!=4  ";
                    break;
                case "04"://业务提醒
                    returnVa = "CellNumber='" + m_PersonTel + "'";
                    break;
                case "03"://服务收藏
                    returnVa = "PersonID=" + m_UserID;
                    break;
                case "0502"://电子证照信息
                    returnVa = "DocsType IN('.jpg','.gif','.bmp','.png') AND PersonID=" + m_UserID;
                    break;
                case "0503"://材料附件
                    returnVa = "DocsType IN('.doc','.docx','.xls','.xlsx') AND PersonID=" + m_UserID;
                    break;
                case "07"://子女信息
                    returnVa = "PersonID=" + m_UserID;
                    break;
                case "08"://婚姻信息
                    returnVa = "PersonID=" + m_UserID;
                    break;
                default:
                    returnVa = "";
                    break;
            }

            return returnVa;
        }
        #endregion
    }
}
