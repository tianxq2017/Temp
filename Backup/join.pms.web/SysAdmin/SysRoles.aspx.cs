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

namespace join.pms.web.SysAdmin
{
    public partial class SysRoles : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号
        private string m_FuncCode;

        private string m_SqlParams;
        protected string m_TargetUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetOpratetionAction("系统角色管理");
            }
        }

        #region 身份验证及参数过滤

        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
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
                    funcName = "新增";
                    break;
                case "edit": // 编辑
                    funcName = "修改";
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // 删除
                    funcName = "删除内容";
                    DelInfo(m_ObjID);
                    break;
                case "view": // 查看
                    funcName = "查看内容";
                    break;
                case "5": // 审核
                    funcName = "审核内容";
                    break;
                case "6": // 推荐
                    funcName = "推荐";
                    //recommend(m_ObjID);
                    // Response.Redirect(txtUrlParams.Value);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            DataTable tempDT = new DataTable();
            m_SqlParams = "SELECT [RoleName], [RoleNotes], [RoleAttrib] FROM [SYS_Roles] WHERE RoleID=" + m_ObjID;
            try
            {
                tempDT = DbHelperSQL.Query(m_SqlParams).Tables[0];
                this.txtRoleName.Text = PageValidate.GetTrim(tempDT.Rows[0]["RoleName"].ToString());
                this.txtRoleNotes.Text = (PageValidate.GetTrim(tempDT.Rows[0]["RoleNotes"].ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
            m_SqlParams = null;
            tempDT = null;
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            if (PageValidate.IsNumber(objID))
            {
                try
                {
                    System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(3);
                    list.Add("DELETE FROM SYS_RolesFunction WHERE RoleID=" + objID + "");
                    list.Add("DELETE FROM SYS_UserRoles WHERE RoleID=" + objID + "");
                    list.Add("DELETE FROM SYS_Roles WHERE RoleID = " + objID + "");
                    DbHelperSQL.ExecuteSqlTran(list);
                    list = null;
                    // 0,默认,未审核 1,系统保留，禁止删除 2,审核通过 3,审核驳回 4,删除 9,推荐
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的角色及其分配的权限被成功删除！", m_TargetUrl, true);
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
               MessageBox.ShowAndRedirect(this.Page, "操作提示：考虑到系统安全，此操作每次只能选择一条记录，不可以多选！", m_TargetUrl, true);
            }
        }

        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string RoleName = PageValidate.GetTrim(this.txtRoleName.Text);
            string RoleNotes = PageValidate.GetTrim(this.txtRoleNotes.Text);


            if (String.IsNullOrEmpty(RoleName))
            {
                strErr += "角色名称不能为空！\\n";
            }
            if (String.IsNullOrEmpty(RoleNotes))
            {
                strErr += "请输入角色描述信息！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            if (m_ActionName == "add")
            {
                m_SqlParams = "INSERT INTO [SYS_Roles]([RoleName], [RoleNotes]) VALUES('" + RoleName + "', '" + RoleNotes + "')";
                try { DbHelperSQL.ExecuteSql(m_SqlParams); }
                catch (Exception ex) { MessageBox.Show(this, ex.Message); }
            }
            else if (m_ActionName == "edit")
            {
                m_SqlParams = "UPDATE SYS_Roles SET RoleName='" + RoleName + "',RoleNotes='" + RoleNotes + "' WHERE RoleID=" + m_ObjID;
                try
                {
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                    return;
                }
            }
             MessageBox.ShowAndRedirect(this.Page, "操作提示：操作成功！", m_TargetUrl, true);
        }
    }
}
