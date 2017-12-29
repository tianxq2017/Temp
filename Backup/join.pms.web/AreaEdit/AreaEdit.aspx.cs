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

using System.Globalization;
using System.IO;
using System.Text;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;
namespace join.pms.web.AreaEdit
{
    public partial class AreaEdit : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号
        private string m_UserName;

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;
        private string m_BizStep;
        private string m_AreaCode;
        private string m_AreaName;
        private BIZ_Persons m_PerA;//储存男方信息
        private BIZ_Persons m_PerB;//储存女方信息
        private BIZ_Contents m_BizC;//业务信息
        private BIZ_PersonChildren m_Children;//子女信息

        protected void Page_Load(object sender, EventArgs e)
        {

            AuthenticateUser();
            //ValidateParams();

            //户籍地默认当前选择地区
            //this.UcAreaSelRegA.SetAreaCode(m_AreaCode);
            //this.UcAreaSelRegA.SetAreaName(GetAreaName(m_AreaCode, "1"));
            //this.txtAreaSelRegCodeB.Value = m_AreaCode;
            //this.txtAreaSelRegNameB.Text = BIZ_Common.GetAreaName(m_AreaCode, "1");
            //this.txtAreaSelRegNameB.ReadOnly = true;
            if (!IsPostBack)
            {
                string roleID = join.pms.dal.CommPage.GetSingleVal("SELECT [RoleID] FROM [SYS_UserRoles] WHERE UserID=" + this.m_UserID);
                if (String.IsNullOrEmpty(roleID) || roleID != "1")
                {
                    Response.Write("<script language='javascript'>alert('操作失败：您不具有业务操作的权限！');parent.location.href='/MainFrame.aspx';</script>");
                    Response.End();
                }
                SetAreaList(this.DropDownListSheng, "000000000000", "610000000000"); // 省
                SetAreaList(this.DropDownListDiShi, "610000000000", ""); // 地市
                SetAreaList(this.DropDownListQuXian, "", ""); //区县
                SetAreaList(this.DropDownListXiang, "", ""); //乡镇
                SetAreaList(this.DropDownListCun, "", ""); //乡镇
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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        // 省
        protected void DropDownListSheng_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListSheng.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListDiShi, areaCode, "");
            }
        }

        // 地市
        protected void DropDownListDiShi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListDiShi.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListQuXian, areaCode, "");
            }
        }

        // 区县
        protected void DropDownListQuXian_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListQuXian.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListXiang, areaCode, "");
            }
        }

        // 乡镇
        protected void DropDownListXiang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListXiang.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListCun, areaCode, "");
            }
        }
        // 社区/村
        protected void DropDownListCun_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListCun.SelectedValue;
            
        }

        // <summary>
        /// 地区选择
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="areaCode"></param>
        public void SetAreaList(DropDownList ddl, string areaCode, string defaultVal)
        {
            m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + areaCode + "' ORDER BY AreaCode";
            DataTable  m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            ddl.DataSource = m_Dt;
            ddl.DataTextField = "AreaName";
            ddl.DataValueField = "AreaCode";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("请选择", ""));
            if (!String.IsNullOrEmpty(defaultVal))
            {
                ddl.SelectedValue = defaultVal;
            }
            m_Dt = null;
        }

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

       

        #region 提交申请信息
        /// <summary>
        /// 提交申请信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            
        }
        #endregion
    }
}

