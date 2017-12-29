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

using join.pms.dal;
using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.web.SysMsg
{
    public partial class NoteView : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号
        private string m_ComID;

        private string m_SqlParams;
        protected string m_TargetUrl;

        private string m_RawUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();
            // AdminSite/NoteReView.aspx?action=edit&RID=43&sourceUrl=2C936D780CBDA1FF1D549B27436C7D208F11DAF16A00769D&oNa=
            m_RawUrl = Request.RawUrl;
            if (!IsPostBack)
            {
                // 编辑删除功能的返回地址

                if (String.IsNullOrEmpty(m_SourceUrl) || String.IsNullOrEmpty(m_ActionName))
                {
                    Response.Write("非法访问，操作被终止！");
                    Response.End();
                }
                else
                {
                    SetOpratetionAction("");
                }
            }
        }
        #region

        /// <summary>
        /// 验证接受的参数
        /// </summary>
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
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrl, "code");
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
                case "edit":
                    funcName = "回复";
                    ShowModInfo(m_ObjID);
                    break;
                case "view":
                    funcName = "查看";
                    this.PanelRe.Visible = false;
                    this.btnAdd.Enabled = false;
                    this.btnAdd.Visible = false;
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
            //this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">起始页</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">起始页</a> &gt;&gt; 政民互动 &gt;&gt;互动信息审阅处理：";
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            StringBuilder sHtml = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT MsgTitle,MsgBody,MsgIP,OprateDate FROM MSG_Notes WHERE MsgID=" + objID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    sHtml.Append("<table class=\"message_reply\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"genHeaderSmall\">");
                    sHtml.Append("<tr><td>留言标题：" + sdr["MsgTitle"].ToString() + "(" + sdr["MsgIP"].ToString() + ")</td><td width=\"160\">" + sdr["OprateDate"].ToString() + "</td>");
                    sHtml.Append("</tr></table>");
                    sHtml.Append("<table class=\"message_reply_a\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    sHtml.Append("<tr><td width=\"80\" height=\"30\" align=\"center\">咨询内容：</td><td>" + PageValidate.Decode(sdr["MsgBody"].ToString()) + "</td></tr>");
                    sHtml.Append("</table>");
                }
                sdr.Close();

                //检查是否存在回复
                // [CommID], [MsgID], [ReplyTitle], [ReplyBody], [ReplyIP], [IsAudit], [UserID], [UserName], [OprateDate] 
                m_SqlParams = "SELECT CommID,ReplyBody,OprateDate FROM MSG_NotesRe WHERE MsgID=" + objID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    int i = 0;
                    sHtml.Append("<table class=\"message_reply_b\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        i++;
                        this.txtReplyBody.Text = PageValidate.Decode(sdr["ReplyBody"].ToString());
                        // m_ActionName
                        sHtml.Append("<tr><td width=\"80\" align=\"center\">" + i.ToString() + "楼回复：</td><td>" + PageValidate.Decode(sdr["ReplyBody"].ToString()) + "</td><td width=\"120\"  valign=\"middle\">" + sdr["OprateDate"].ToString() + "</td>");
                        if (m_ActionName == "view") {
                            sHtml.Append("<td width=\"120\" valign=\"middle\">&nbsp;</td></tr>");
                        }
                        else { 
                            sHtml.Append("<td width=\"120\" valign=\"middle\"><img src=\"/images/icon-del.gif\" width=\"14\" height=\"14\" align=\"absbottom\" /><a href=\"NoteReView.aspx?action=del&k=" + sdr["CommID"].ToString() + "&reUrl=" + m_RawUrl + "\" >删除</a> </td></tr>"); 
                        }
                        sHtml.Append("</tr>");
                    }
                    sHtml.Append("</table>");
                }
                sdr.Close();
            }
            catch
            {
                if (sdr != null) sdr.Close();
                sHtml.Append("操作失败：获取信息时出现错误！");
            }

            this.LiteralNotes.Text = sHtml.ToString();
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            try
            {
                m_SqlParams = "DELETE FROM MSG_NotesRe WHERE [CommID] IN(" + objID + ")";
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
            string ReplyBody = PageValidate.Encode(PageValidate.GetTrim(this.txtReplyBody.Text));
            string ReplyIP = Request.UserHostAddress;
            if (string.IsNullOrEmpty(ReplyBody)) strErr += "请输入回复内容！\\n";
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            try
            {
                // [CommID], [MsgID], [ReplyTitle], [ReplyBody], [ReplyIP], [IsAudit], [UserID], [UserName], [OprateDate] 
                m_SqlParams = "INSERT INTO MSG_NotesRe(ReplyBody,ReplyIP,MsgID) VALUES('" + ReplyBody + "','" + ReplyIP + "'," + m_ObjID + ")";

                DbHelperSQL.ExecuteSql(m_SqlParams);

                this.txtReplyBody.Text = "";
                Response.Write(" <script>alert('操作成功！') ;window.location.href='" + m_RawUrl + "';document.getElementById('txtReplyBody').value='';</script>");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }

        }

    }
}


