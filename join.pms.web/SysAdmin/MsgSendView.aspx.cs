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

using System.Text;

namespace join.pms.web.SysAdmin
{
    public partial class MsgSendView : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_ActionName;
        private string m_ObjID;
        private string m_UserID; // 当前登录的操作用户编号
        private string m_SqlParams;
        private string m_TargetUrl;

        private DataTable m_Dt;
        private string m_FuncCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                SetOpratetionAction("发送信息");
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

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        #region 身份验证等

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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/loginTemp.aspx';</script>");
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
                    funcName = "";
                    break;
                case "view": // 查看短消息
                    funcName = "";
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // 删除
                    funcName = "删除内容";
                    DelInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {

            try
            {
                m_SqlParams = "DELETE FROM [SMS] WHERE SysNo IN(" + m_ObjID + ")";

                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "操作提示：删除成功！", m_TargetUrl, true);
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
            Response.End();

        }
        private void ShowModInfo(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                m_Dt = new DataTable();
                StringBuilder sHtml = new StringBuilder();
                m_SqlParams = "SELECT [SysNo], [CellNumber], [SMSContent], [Priority], [SendNum], [CreateTime], [HandleTime], [StatusCN], [ToCom], [SendTime] FROM [v_SMS] WHERE SysNo=" + m_ObjID;
                try
                {
                    m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                    sHtml.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"table_public_01\">");
                    sHtml.Append("<tbody>");
                    sHtml.Append("<tr>");
                    sHtml.Append("<th width=\"100\" class=\"center\">参数</th>");
                    sHtml.Append("<th>值</th>");
                    sHtml.Append("</tr>");
                    if (m_Dt.Rows.Count == 1)
                    {
                        sHtml.Append("<tr>");
                        sHtml.Append("<th class=\"right\">系统自动编号：</th>");
                        sHtml.Append("<td>" + m_Dt.Rows[0]["SysNo"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th class=\"right\">目标手机号码：</th>");
                        sHtml.Append("<td>" + m_Dt.Rows[0]["CellNumber"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th class=\"right\">短信内容：</th>");
                        sHtml.Append("<td>" + m_Dt.Rows[0]["SMSContent"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th class=\"right\">短信分条数：</th>");
                        sHtml.Append("<td>" + m_Dt.Rows[0]["SendNum"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th class=\"right\">短信创建时间：</th>");
                        sHtml.Append("<td>" + m_Dt.Rows[0]["CreateTime"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th class=\"right\">短信发送时间：</th>");
                        sHtml.Append("<td>" + m_Dt.Rows[0]["HandleTime"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th class=\"right\">短信发送状态：</th>");
                        sHtml.Append("<td>" + m_Dt.Rows[0]["StatusCN"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                    }
                    sHtml.Append("</tbody>");
                    sHtml.Append("</table>");
                    m_Dt = null;
                    this.LiteralData.Text = sHtml.ToString();
                }
                catch (Exception ex)
                {
                    m_Dt = null;
                    this.LiteralData.Text = "操作失败：" + ex.Message + "'";
                }
                sHtml = null;
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：考虑到系统安全，此操作每次只能选择一条信息，不可以多选！", m_TargetUrl, true);
            }
        }
    
        #endregion

    }
}
