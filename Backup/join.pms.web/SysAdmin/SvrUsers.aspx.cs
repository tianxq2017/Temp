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

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;

namespace join.pms.web.SysAdmin
{
    public partial class SvrUsers : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号

        private string m_SqlParams;
        private DataTable m_Dt;
        protected string m_TargetUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();

            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);
            m_SourceUrl = DESEncrypt.Decrypt(m_SourceUrl);
            if (!String.IsNullOrEmpty(m_SourceUrl)) m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrl;
            if (!IsPostBack)
            {
                if (String.IsNullOrEmpty(m_SourceUrl) || String.IsNullOrEmpty(m_ActionName))
                {
                    Response.Write("非法访问，操作被终止！");
                    Response.End();
                }
                else
                {
                    SetOpratetionAction("用户信息");
                }
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

        /// <summary>
        /// 设置操作行为
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("非法访问：操作被终止！");
                    Response.End();
                }
            }
            else
            {
                if (!PageValidate.IsNumber(m_ObjID))
                {
                    m_ObjID = m_ObjID.Replace("s", ",");
                }
            }
            switch (m_ActionName)
            {
                case "add": // 新增
                    funcName = "新增参数";
                    //ShowAddInfo();
                    break;
                case "edit": // 编辑
                    funcName = "参数修改";
                    break;
                case "del": // 删除
                    funcName = "删除内容";
                    break;
                case "initpwd": // 重置密码
                    funcName = "重置密码";
                    ResetUserPwd(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误", m_TargetUrl, true, true);
                    break;
            }
            //this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }


        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="objID"></param>
        private void ResetUserPwd(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                string PersonPwd = DESEncrypt.GetMD5_32("111111");
                m_SqlParams = "UPDATE BIZ_Persons Set PersonPwd='" + PersonPwd + "' WHERE PersonID=" + objID;
                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：成功将用户密码初始化为“111111”", m_TargetUrl, true,true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：考虑到系统安全，此操作每次只能选择一个用户，不可以多选", m_TargetUrl, true, true);
            }
        }
        
    }
}
