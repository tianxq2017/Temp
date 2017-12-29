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
    public partial class SysUserDepment : System.Web.UI.Page
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
                SetOpratetionAction("组织机构管理");
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
                    case "add": // 人事存档信息导入
                        funcName = "添加新科室";
                        break;
                    case "edit": // 人员工资信息导入
                        funcName = "编辑科室名称";
                        ShowModify();
                        break;
                    case "del": // 删除
                        funcName = "删除内容";
                        DelDept();
                        break;
                    case "value": // 查看
                        funcName = "查看内容";
                        break;
                    case "5": // 审核
                        funcName = "审核内容";
                        break;
                    case "6": // 推荐
                        funcName = "推荐";
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
                    m_SqlParams = "SELECT DeptCode,DeptName,DeptAddress,DeptTel FROM USER_Department WHERE  CommID=" + m_ObjID;
                    m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                    if (m_Dt.Rows.Count == 1)
                    {
                        this.txtDeptCode.Value = m_Dt.Rows[0][0].ToString();
                        this.txtDeptName.Value = m_Dt.Rows[0][1].ToString();
                        this.txtDeptAddress.Value = m_Dt.Rows[0][2].ToString();
                        this.txtDeptTel.Value = m_Dt.Rows[0][3].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：" + ex.Message, m_TargetUrl, true);
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try 
            {
                // DeptCode,DeptName,DeptAddress,DeptTel
                string strErr = string.Empty;
                string DeptCode = PageValidate.GetTrim(this.txtDeptCode.Value);
                string DeptName = PageValidate.GetTrim(this.txtDeptName.Value);
                string DeptAddress = PageValidate.GetTrim(this.txtDeptAddress.Value);
                string DeptTel = PageValidate.GetTrim(this.txtDeptTel.Value);
                if (String.IsNullOrEmpty(DeptCode))
                {
                    strErr += "机构编码不能为空！\\n";
                }
                if (String.IsNullOrEmpty(DeptName))
                {
                    strErr += "请输入机构名称！\\n";
                }
                if (strErr != "")
                {
                    MessageBox.Show(this, strErr);
                    return;
                }

                if (m_ActionName == "add")
                {
                    m_SqlParams = "INSERT INTO USER_Department(DeptCode,DeptName,DeptAddress,DeptTel,OprateUserID) VALUES('" + DeptCode + "','" + DeptName + "','" + DeptAddress + "','" + DeptTel + "'," + m_UserID + ")";
                }
                else if (m_ActionName == "edit")
                {
                    m_SqlParams = "UPDATE USER_Department SET DeptCode='" + DeptCode + "',DeptName='" + DeptName + "',DeptAddress='" + DeptAddress + "',DeptTel='" + DeptTel + "' WHERE CommID=" + m_ObjID;
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
        /// 科室删除
        /// </summary>
        private void DelDept()
        { 
            if (!String.IsNullOrEmpty(m_ObjID) && PageValidate.IsNumber(m_ObjID))
            {
                try
                {
                    DbHelperSQL.ExecuteSql("delete from USER_Department where CommID=" + m_ObjID);
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
