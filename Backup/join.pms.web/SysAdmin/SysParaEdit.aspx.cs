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
    public partial class SysParaEdit : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;

        private string m_ActionName;
        private string m_ObjID;
        private string m_UserID; // 当前登录的操作用户编号
        private string m_SqlParams;
        private string m_TargetUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();

            if (!IsPostBack)
            {
                SetOpratetionAction("系统参数管理");
            }
        }

        #region

        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                //m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
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
                    funcName = "新增参数";

                    break;
                case "edit": // 编辑
                    funcName = "参数修改";
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // 删除
                    funcName = "删除内容";
                    DelInfo(m_ObjID);
                    break;
                case "4": // 查看
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
            m_SqlParams = "SELECT [CommID], [ParaCode], [ParaValue], [ParaMemo],[ParaCate]  FROM SYS_Params where CommID=" + m_ObjID;
            try
            {
                tempDT = DbHelperSQL.Query(m_SqlParams).Tables[0];
                this.txtParaValue.Text = tempDT.Rows[0]["ParaValue"].ToString();
                this.txtParaSM.Text = tempDT.Rows[0]["ParaMemo"].ToString();
                this.txtCate.Text = tempDT.Rows[0]["ParaCate"].ToString();
                this.txtCode.Text = tempDT.Rows[0]["ParaCode"].ToString();
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
            try
            {
                m_SqlParams = "DELETE FROM SYS_Params WHERE [CommID] IN(" + objID + ")";
                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "操作提示：删除成功！", m_TargetUrl, true);

            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
            Response.End();
        }

        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string paraValue = this.txtParaValue.Text.Trim();
            string paraSM = this.txtParaSM.Text.Trim();
            string paraCode = this.txtCode.Text.Trim();
            string paraCate = this.txtCate.Text.Trim();

            
            if (String.IsNullOrEmpty(paraValue))
            {
                strErr += "参数值不能为空！\\n";
            }
            if (String.IsNullOrEmpty(paraSM))
            {
                strErr += "参数说明不能为空！\\n";
            }
            if (!PageValidate.IsNumber(paraCate))
            {
                strErr += "参数分类不是数字！\\n"; 
            }
            if (strErr != "")
            {
                MessageBox.Show(this,strErr);
                return;
            }
            if (m_ActionName == "add")
            {
                m_SqlParams = "insert into SYS_Params(ParaValue,ParaMemo,ParaCate,ParaCode)  ";
                m_SqlParams += "values('" + paraValue + "','" + paraSM + "'," + paraCate + ",'" + paraCode + "')";
                try { DbHelperSQL.ExecuteSql(m_SqlParams); }
                catch (Exception ex) { MessageBox.Show(this, ex.Message); }
            }
            else if (m_ActionName == "edit")
            {
                m_SqlParams = "update SYS_Params set ParaCate=" + paraCate + ",paraCode='" + paraCode + "',ParaValue='" + paraValue + "',ParaMemo='" + paraSM + "' where CommID=" + m_ObjID;
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
