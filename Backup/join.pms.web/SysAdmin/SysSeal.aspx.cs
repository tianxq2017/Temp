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
    public partial class SysSeal : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_UserID; // 当前登录的操作用户编号
        private string m_SqlParams;
        private DataTable m_Dt;

        protected string m_TargetUrl;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetOpratetionAction("公章管理");
            }
        }

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
            if (!String.IsNullOrEmpty(m_FuncCode.Substring(0, 2)))
            {
                switch (m_ActionName)
                {
                    case "add": // 新增
                        funcName = "新建公章";
                        break;
                    case "edit": // 编辑
                        funcName = "修改公章信息";
                        ShowModify();
                        break;
                    case "del": // 删除
                        funcName = "删除用户";
                        DelInfo(m_ObjID);
                        break;
                    case "initpwd": // 重置密码
                        funcName = "重置密码";
                        ResetUserPwd(m_ObjID);
                        break;
                    case "4": // 审核
                        funcName = "审核";
                        break;
                    case "5": // 重置密码
                        funcName = "重置密码";
                        break;
                    case "6": // 分配角色
                        funcName = "给用户分配角色";
                        break;
                    default:
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                        break;
                }
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }

        /// <summary>
        /// 获取
        /// </summary>
        private void ShowModify()
        {
            if (!String.IsNullOrEmpty(m_ObjID) && PageValidate.IsNumber(m_ObjID))
            {
                m_Dt = new DataTable();
                try
                {
                    m_SqlParams = "SELECT * FROM [SYS_Seal] WHERE SealID=" + m_ObjID;
                    m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                    if (m_Dt.Rows.Count == 1)
                    {
                        this.txtSealName.Text = m_Dt.Rows[0]["SealName"].ToString();
                        this.txtSealPath.Text = m_Dt.Rows[0]["SealPath"].ToString();
                        //this.txtSealPass.Text = m_Dt.Rows[0]["SealPass"].ToString();
                        this.txtUserPwd.Value = m_Dt.Rows[0]["SealPass"].ToString();

                        this.txtUserID.Value = m_Dt.Rows[0]["SealUserID"].ToString();
                        this.txtUserName.Value = m_Dt.Rows[0]["SealUserName"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：" + ex.Message, m_TargetUrl, true);
                }
            }
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            try
            {
                m_SqlParams = "DELETE FROM SYS_Seal WHERE SealID IN(" + objID + ")";
                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "操作提示：该用户被成功删除！", m_TargetUrl, true);
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
            Response.End();
        } 
        /// <summary>
        /// 重置签章密码
        /// </summary>
        /// <param name="objID"></param>
        private void ResetUserPwd(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                m_SqlParams = "UPDATE SYS_Seal Set SealPass='76586E8A7D72FD3F' WHERE SealID=" + objID;

                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：成功将用户密码初始化为“111111”！", m_TargetUrl, true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：考虑到系统安全，此操作每次只能选择一个用户，不可以多选！", m_TargetUrl, true);
            }

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                string strErr = string.Empty;
                string SealName = PageValidate.GetTrim(this.txtSealName.Text);
                string SealPath = PageValidate.GetTrim(this.txtSealPath.Text);
                string SealPass = PageValidate.GetTrim(this.txtSealPass.Text);
                string SealUserID = PageValidate.GetTrim(this.txtUserID.Value);
                string SealUserName = PageValidate.GetTrim(this.txtUserName.Value);

                if (string.IsNullOrEmpty(SealName))
                {
                    strErr += "公章名称不能为空！\\n";
                }
                if (string.IsNullOrEmpty(SealPath))
                {
                    strErr += "路径不能为空！\\n";
                }
                if (string.IsNullOrEmpty(SealPass))
                {
                    strErr += "签章密码不能为空！\\n";
                }
                if (string.IsNullOrEmpty(SealUserID))
                {
                    strErr += "请选择掌握公章的对象！\\n";
                }
                if (!PageValidate.IsNumber(SealUserID))
                {
                    strErr += "每个公章只能分配一个人！\\n";
                }

                if (strErr != "")
                {
                    MessageBox.Show(this, strErr);
                    return;
                }
                /*
                 A.SealID, A.SealName, A.SealPath, 
      A.SealPass,A.SealUserID,A.SealUserName,A.SealAttrib,A.CreateDate, 
                 * 
                 */
                SealPass = DESEncrypt.Encrypt(SealPass);
                if (m_ActionName == "add")
                {
                    m_SqlParams = "SELECT COUNT(*) FROM SYS_Seal WHERE SealUserID=" + SealUserID;
                    if (DbHelperSQL.GetSingle(m_SqlParams).ToString() != "0")
                    {                        
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：该用户已经分配公章，一个用户只能使用一个公章！", m_TargetUrl, true);
                    }
                    else
                    {
                        if (SealPass != this.txtUserPwd.Value)
                        {
                            SealPass = DESEncrypt.Encrypt(SealPass);
                        }
                        m_SqlParams = "INSERT INTO [SYS_Seal](SealName,SealPath,SealPass,SealUserID,SealUserName) VALUES('" + SealName + "','" + SealPath + "','" + SealPass + "'," + SealUserID + ",'" + SealUserName + "')";
                    }
                }
                else if (m_ActionName == "edit")
                {
                    m_SqlParams = "UPDATE [SYS_Seal] SET [SealName]='" + SealName + "',[SealPath]='" + SealPath + "', [SealPass]='" + SealPass + "', SealUserID=" + SealUserID + ",SealUserName='" + SealUserName + "'";
                    m_SqlParams += " WHERE SealID=" + m_ObjID;
                }
                else { 
                MessageBox.ShowAndRedirect(this.Page, "操作提示：参数丢失，请重新操作！", m_TargetUrl, true);
            }
                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "操作提示：操作成功！", m_TargetUrl, true);
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：" + ex.Message, m_TargetUrl, true);
            }
        }

    }
}

