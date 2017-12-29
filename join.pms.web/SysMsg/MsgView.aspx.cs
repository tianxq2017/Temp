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
using UNV.Comm.Web;


namespace join.pms.web.SysMsg
{
    public partial class MsgView : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_ActionName;
        private string m_ObjID;
        private string m_FuncNo;

        private string m_TargetUrl;
        private string m_SqlParams;
        private DataTable m_Dt;

        private string m_UserID; // 当前登录的操作用户编号

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                SetOpratetionAction("系统短讯");
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
                //m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                m_FuncNo = StringProcess.AnalysisParas(m_SourceUrl, "FuncNo");
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

        #region 页面首次加载时显示的信息
        /// <summary>
        /// 设置操作行为 add新增,edit编辑,del删除,4查看,5审核,6分配角色 等
        /// </summary>
        /// <param name="oprateName">操作名称</param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID) || !PageValidate.IsNumber(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("非法访问：操作被终止！");
                    Response.End();
                }
            }

            switch (m_ActionName)
            {
                case "view": // 查看
                    funcName = "消息查看";
                    ShowModInfo(m_ObjID);
                    break;
                case "edit": // 编辑
                    funcName = "修改用户信息";
                    //ShowModInfo(m_ObjID);
                    break;
                default:
                    if (Request.UrlReferrer != null) Response.Redirect(Request.UrlReferrer.ToString());
                    break;
            }

            this.LiteralNav.Text = "管理首页 &gt;&gt; " + oprateName + " &gt;&gt; " + funcName + "：";
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            string targetUserID = string.Empty;
            StringBuilder sHtml = new StringBuilder();
            try
            {
                m_SqlParams = "SELECT * FROM [v_SysMsg] WHERE MsgID=" + objID;
                m_Dt = new DataTable();
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                
                if (m_Dt.Rows.Count == 1)
                {
                    targetUserID = m_Dt.Rows[0]["SourceUserID"].ToString();
                        // 标题 
                    sHtml.Append("<table width=\"95%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
                    sHtml.Append("<td width=\"20\" height=\"30\" align=\"left\">&nbsp;</td>");
                    sHtml.Append("<td width=\"30\" align=\"center\" bgcolor=\"#F4FAFA\" class=\"fb\"><img src=\"/images/chakan.gif\"  /></td>");
                    sHtml.Append("<td align=\"left\" bgcolor=\"#F4FAFA\" class=\"page\"><span class=\"fb\"><strong>" + m_Dt.Rows[0]["MsgTitle"].ToString() + "</strong></span></td>");
                    sHtml.Append("</tr></table>");

                    // 作者、发布时间、页面功能
                    sHtml.Append("<table width=\"95%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" ><tr >");
                    sHtml.Append("<td width=\"20\" height=\"30\" align=\"left\">&nbsp;</td>");
                    sHtml.Append("<td align=\"left\"  style=\"border-top:1px solid #cccccc;\" >");
                    sHtml.Append("【字体：<a class=\"fh \" href=\"javascript:fontZoom(16)\">大</a>&nbsp;<a class=\"fh \" href=\"javascript:fontZoom(14)\">中</a>&nbsp;<a class=\"fh \" href=\"javascript:fontZoom(12)\">小</a>】 【<a class=\"fh \" href=\"javascript:this.print()\">打印</a>】</td>");
                    sHtml.Append("</tr></table><br/>");
                    // 正文
                    sHtml.Append("<table width=\"95%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" ><tr>");
                    sHtml.Append("<td width=\"20\" align=\"left\">&nbsp;</td>");
                    sHtml.Append("<td align=\"left\" class=\"fb\" id=\"fontzoom\" height=\"280\" valign=\"top\">");

                    if (m_FuncNo == "0302") { sHtml.Append("发送人：" + m_Dt.Rows[0]["SourceUserName"].ToString() + "<br>"); }
                    else { sHtml.Append("接收人：" + m_Dt.Rows[0]["TargetUserName"].ToString() + "<br>"); }
                    sHtml.Append("消息头：" + m_Dt.Rows[0]["MsgTitle"].ToString() + "<br>");
                    sHtml.Append("消息体：<br/>" + PageValidate.Decode(m_Dt.Rows[0]["MsgBody"].ToString()) + "<br/>");
                    //sHtml.Append("消息附件：" + GetMsgDocs(m_Dt.Rows[0]["DocID"].ToString()) + "<br>");
                    //sHtml.Append("消息类型：" + m_Dt.Rows[0]["MsgTypeCN"].ToString() + "<br>");
                    //sHtml.Append("消息状态：" + m_Dt.Rows[0]["MsgStateCN"].ToString() + "<br>");
                    sHtml.Append("时间：" + m_Dt.Rows[0]["MsgSendTime"].ToString() + "<br>");
                    sHtml.Append("<br><div align=\"left\"><input type=\"button\" name=\"ButBackPage\" value=\"· 返回 ·\" id=\"ButBackPage\" onclick=\"javascript:window.location.href='" + m_TargetUrl + "';\" class=\"submit6\" /></div>");

                    sHtml.Append("</td></tr></table><br/><br/>");
   
                    m_Dt = null;
                }
                m_Dt = null;

                if (targetUserID == m_UserID) {
                    DbHelperSQL.ExecuteSql("UPDATE SYS_Msg SET MsgState=1 WHERE MsgID=" + objID);
                }
                
            }
            catch (Exception ex)
            {
                sHtml.Append(ex.Message);
            }
            this.LiteralMsgView.Text = sHtml.ToString();
        }

        /// <summary>
        /// 获取消息附件
        /// </summary>
        /// <returns></returns>
        private string GetMsgDocs(string docsID) {
            StringBuilder sHtml = new StringBuilder();
            if (!string.IsNullOrEmpty(docsID))
            {
                docsID = docsID.Replace("s",",");
                m_SqlParams = "SELECT DocsPath, SourceName FROM SYS_MsgDocs WHERE DocID IN(" + docsID+")";
               DataTable m_Dt = new DataTable();
                
                try
                {
                    m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                    for (int i = 0; i < m_Dt.Rows.Count; i++) {
                        sHtml.Append("<a href=\"" + m_Dt.Rows[i][0].ToString() + "\" target=\"_blank\">" + m_Dt.Rows[i][1].ToString() + "</a> &nbsp;&nbsp;&nbsp;&nbsp;");
                    }
                    
                }
                catch (Exception ex)
                {
                    sHtml.Append(ex.Message);
                }
                m_Dt = null;
            }
            else { sHtml.Append("没有附件"); }

            return sHtml.ToString();
        }
        #endregion
    }
}
