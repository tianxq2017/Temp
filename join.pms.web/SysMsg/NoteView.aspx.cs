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

        private string m_UserID; // ��ǰ��¼�Ĳ����û����
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
                // �༭ɾ�����ܵķ��ص�ַ

                if (String.IsNullOrEmpty(m_SourceUrl) || String.IsNullOrEmpty(m_ActionName))
                {
                    Response.Write("�Ƿ����ʣ���������ֹ��");
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
        /// ��֤���ܵĲ���
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
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        /// <summary>
        /// �����֤
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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/loginTemp.aspx';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// ���ò�����Ϊ
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("�Ƿ����ʣ���������ֹ��");
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
                    funcName = "�ظ�";
                    ShowModInfo(m_ObjID);
                    break;
                case "view":
                    funcName = "�鿴";
                    this.PanelRe.Visible = false;
                    this.btnAdd.Enabled = false;
                    this.btnAdd.Visible = false;
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // ɾ��
                    funcName = "ɾ������";
                    DelInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                    break;
            }
            //this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">��ʼҳ</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">��ʼҳ</a> &gt;&gt; ���񻥶� &gt;&gt;������Ϣ���Ĵ���";
        }

        /// <summary>
        /// �鿴
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
                    sHtml.Append("<tr><td>���Ա��⣺" + sdr["MsgTitle"].ToString() + "(" + sdr["MsgIP"].ToString() + ")</td><td width=\"160\">" + sdr["OprateDate"].ToString() + "</td>");
                    sHtml.Append("</tr></table>");
                    sHtml.Append("<table class=\"message_reply_a\" width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    sHtml.Append("<tr><td width=\"80\" height=\"30\" align=\"center\">��ѯ���ݣ�</td><td>" + PageValidate.Decode(sdr["MsgBody"].ToString()) + "</td></tr>");
                    sHtml.Append("</table>");
                }
                sdr.Close();

                //����Ƿ���ڻظ�
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
                        sHtml.Append("<tr><td width=\"80\" align=\"center\">" + i.ToString() + "¥�ظ���</td><td>" + PageValidate.Decode(sdr["ReplyBody"].ToString()) + "</td><td width=\"120\"  valign=\"middle\">" + sdr["OprateDate"].ToString() + "</td>");
                        if (m_ActionName == "view") {
                            sHtml.Append("<td width=\"120\" valign=\"middle\">&nbsp;</td></tr>");
                        }
                        else { 
                            sHtml.Append("<td width=\"120\" valign=\"middle\"><img src=\"/images/icon-del.gif\" width=\"14\" height=\"14\" align=\"absbottom\" /><a href=\"NoteReView.aspx?action=del&k=" + sdr["CommID"].ToString() + "&reUrl=" + m_RawUrl + "\" >ɾ��</a> </td></tr>"); 
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
                sHtml.Append("����ʧ�ܣ���ȡ��Ϣʱ���ִ���");
            }

            this.LiteralNotes.Text = sHtml.ToString();
        }

        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            try
            {
                m_SqlParams = "DELETE FROM MSG_NotesRe WHERE [CommID] IN(" + objID + ")";
                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��ɾ���ɹ���", m_TargetUrl, true);

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
            if (string.IsNullOrEmpty(ReplyBody)) strErr += "������ظ����ݣ�\\n";
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
                Response.Write(" <script>alert('�����ɹ���') ;window.location.href='" + m_RawUrl + "';document.getElementById('txtReplyBody').value='';</script>");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }

        }

    }
}


