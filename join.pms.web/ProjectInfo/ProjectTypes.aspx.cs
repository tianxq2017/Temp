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
    public partial class ProjectTypes : System.Web.UI.Page
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
                SetOpratetionAction("项目分类管理");
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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Ysl9lcXf6JuzBkRL1yQD48cmhxCD5exHudvJr7ExPl6SnOYhiJLFhhdlZx1OzuA1vCf.shtml';</script>");
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
                    case "add": // 添加分类
                        funcName = "项目分类添加";
                        break;
                    case "edit": // 人员工资信息导入
                        funcName = "项目分类编辑";
                        ShowModify();
                        break;
                    case "del": // 删除
                        funcName = "项目分类删除";
                        DelDept();
                        break;
                    case "value": // 查看
                        funcName = "项目分类查看";
                        break;
                    default:
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                        break;
                }
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }

        /// <summary>
        /// 获取机构名称
        /// </summary>
        private void ShowModify()
        {
            if (!String.IsNullOrEmpty(m_ObjID) && PageValidate.IsNumber(m_ObjID))
            {
                m_Dt = new DataTable();
                try
                {
                    m_SqlParams = "SELECT * FROM BIZ_ProjectTypes WHERE  T_ID=" + m_ObjID;
                    m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                    if (m_Dt.Rows.Count == 1)
                    {
                        this.txt_TypeName.Value = m_Dt.Rows[0]["T_Name"].ToString();
                        this.txt_TypeCode.Value = m_Dt.Rows[0]["T_Code"].ToString();
                        this.txt_TypeDescription.Value = m_Dt.Rows[0]["T_Description"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：" + ex.Message, m_TargetUrl, true);
                }
            }
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try 
            {
                string strErr = string.Empty;
                string TypeName = PageValidate.GetTrim(this.txt_TypeName.Value);
                string TypeCode = PageValidate.GetTrim(this.txt_TypeCode.Value);
                string TypeDescription = PageValidate.GetTrim(this.txt_TypeDescription.Value);

                if (String.IsNullOrEmpty(TypeName))
                {
                    strErr += "分类名称不能为空！\\n";
                }
                if (String.IsNullOrEmpty(TypeCode))
                {
                    strErr += "分类编码不能为空！\\n";
                }
                if (strErr != "")
                {
                    MessageBox.Show(this, strErr);
                    return;
                }

                if (m_ActionName == "add")
                {
                    m_SqlParams = "INSERT INTO BIZ_ProjectTypes(T_Name,T_Code,T_Description) VALUES('" + TypeName + "','" + TypeCode + "','" + TypeDescription + "')";
                }
                else if (m_ActionName == "edit")
                {
                    m_SqlParams = "UPDATE BIZ_ProjectTypes SET T_Name='" + TypeName + "',T_Code='" + TypeCode + "',T_Description='" + TypeDescription + "' WHERE T_ID=" + m_ObjID;
                }
                else { 
                MessageBox.ShowAndRedirect(this.Page, "操作提示：参数丢失，请重新操作！", m_TargetUrl, true);
            }
                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "操作提示：操作成功！", m_TargetUrl, true);
            }
            catch(Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：" + ex.Message, m_TargetUrl, true);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void DelDept()
        { 
            if (!String.IsNullOrEmpty(m_ObjID) && PageValidate.IsNumber(m_ObjID))
            {
                try
                {
                    DbHelperSQL.ExecuteSql("delete from BIZ_ProjectTypes where T_ID=" + m_ObjID);
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：删除成功！", m_TargetUrl, true);
                }
                catch(Exception ex)
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：" + ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：删除操作只允许选择一条记录" , m_TargetUrl, true);
            }
        }
        
    }
}
