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
    public partial class SysSginInfo : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_ActionName;
        private string m_ObjID;
        private string m_UserDept;//�û����ű���
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_UserIDs; // ���
        private string m_UserDeptArea;//�������
        private string m_RoleID;
        private string m_SqlParams;
        private string m_SqlParamsSign;
        private string m_SqlParamsSign2;
        private string m_SqlParamsSign3;
        private string m_SqlParamsSign4;
        private string m_SqlParamsSign5;
        private string m_TargetUrl;

        private DataTable m_Dt;
        private DataTable m_DtSign;
        private DataTable m_DtSign2;
        private DataTable m_DtSign3;
        private DataTable m_DtSign4;
        private DataTable m_DtSign5;
        private string m_FuncCode;

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            //ValidateParams();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                SetOpratetionAction("�û���Ϣ");
                if (m_UserID == "1")
                {
                    ShowModInfo("610725000000");
                }
                else
                {
                    m_UserDept = join.pms.dal.CommPage.GetUnitCodeByUser(m_UserID, ref m_UserDeptArea);
                    m_RoleID = DbHelperSQL.GetSingle("SELECT TOP 1 RoleID FROM SYS_UserRoles WHERE UserID=" + m_UserID).ToString();

                    string UserAccounts="";
                    //1	ϵͳ����Ա
                    //2	ҵ�����-����
                    //3	ҵ����-����
                    //4	ҵ����-���
                    if (m_RoleID == "1")
                    {
                        ShowModInfo("610725000000");
                    }
                    else if (m_RoleID == "2")
                    {
                        UserAccounts = m_UserDeptArea.Substring(0, 6) + "000000";
                    }
                    else if (m_RoleID == "3")
                    {
                        UserAccounts = m_UserDeptArea.Substring(0, 6) + "000000";
                    }
                    else if (m_RoleID == "4")
                    {
                        UserAccounts = m_UserDeptArea.Substring(0, 9) + "000";
                    }
                    ShowModInfo(UserAccounts);
                }
            }
        }

        /// <summary>
        /// ����ȡ��
        /// </summary>
        private void ValidateParams()
        {
            //m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            ////m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            //m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);
            //if(m_ActionName=="tel")
            //{

            //}
            //else if (m_ActionName == "wx")
            //{

            //}
            //if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            //{
            //    m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
            //    m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
            //    m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
            //}
            //else
            //{
            //    Response.Write("�Ƿ����ʣ���������ֹ��");
            //    Response.End();
            //}

            //if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        /// <summary>
        /// ��ʽȡ��
        /// </summary>
        /// <param name="userID"></param>
        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        #region �����֤��

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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// ���ò�����Ϊ
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            //string funcName = string.Empty;

            //if (String.IsNullOrEmpty(m_ObjID))
            //{
            //    if (m_ActionName != "add")
            //    {
            //        Response.Write("�Ƿ����ʣ���������ֹ��");
            //        Response.End();
            //    }
            //}
            //else
            //{
            //    if (!PageValidate.IsNumber(m_ObjID))
            //    {
            //        m_ObjID = m_ObjID.Replace("s", ",");
            //    }
            //}
            //switch (m_ActionName)
            //{
            //    case "add": // ����
            //        funcName = "";
            //        break;
            //    case "view": // ��Ϣ�鿴
            //        funcName = "";
            //        ShowModInfo(m_ObjID);
            //        break;
            //    case "del": // ɾ��
            //        funcName = "ɾ������";
            //        DelInfo(m_ObjID);
            //        break;
            //    default:
            //        MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
            //        break;
            //}
            //this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">ǩ���빫�¹���</a> &gt;&gt; ";
        }
        ///// <summary>
        ///// �ֻ���Ϣ
        ///// </summary>
        ///// <param name="objID"></param>
        //private void telInfo(string objID)
        //{

        //    try
        //    {
        //        m_SqlParams = " UPDATE SYS_Sign SET  SignPhone = CmsStats+1 WHERE CmsID=" + objID;

        //        DbHelperSQL.ExecuteSql(m_SqlParams);
        //        MessageBox.ShowAndRedirect(this.Page, "������ʾ��ɾ���ɹ���", m_TargetUrl, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
        //    }
        //    Response.End();

        //}
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {

            try
            {
                m_SqlParams = "DELETE FROM [SMS] WHERE SysNo IN(" + m_ObjID + ")";

                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��ɾ���ɹ���", m_TargetUrl, true);
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
            Response.End();

        }
        /// <summary>
        /// ��ϸ��Ϣȡ��
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string UserAccount)
        {
           m_UserDept = join.pms.dal.CommPage.GetUnitCodeByUser(m_UserID, ref m_UserDeptArea);
           m_RoleID = DbHelperSQL.GetSingle("SELECT TOP 1 RoleID FROM SYS_UserRoles WHERE UserID=" + m_UserID).ToString();
            //1	ϵͳ����Ա
            //2	ҵ�����-����
            //3	ҵ����-����
            //4	ҵ����-���
            if (m_RoleID == "1")
            {
                //UserAccount = UserAccount;
            }
            else if (m_RoleID == "2")
            {
                if (UserAccount.IndexOf(m_UserDeptArea.Substring(0, 6)) > -1)
                {
                    //UserAccount = UserAccount;
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱȨ�ޣ�������ѡ��');history.go(-1)</script>");
                    Response.End();
                }
            }
            else if (m_RoleID == "3")
            {
                if (UserAccount.IndexOf(m_UserDeptArea.Substring(0, 6)) > -1)
                {
                    //UserAccount = m_UserDeptArea.Substring(0, 6) + "000000";
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱȨ�ޣ�������ѡ��');history.go(-1)</script>");
                    Response.End();
                }
            }
            else if (m_RoleID == "4")
            {
                if (UserAccount.IndexOf(m_UserDeptArea.Substring(0, 9)) > -1)
                {
                    //UserAccount = m_UserDeptArea.Substring(0, 9) + "000";
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱȨ�ޣ�������ѡ��');history.go(-1)</script>");
                    Response.End();
                }
            }
            else
            {
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱȨ�ޣ�������ѡ��');history.go(-1)</script>");
                Response.End();
            }


            if (!String.IsNullOrEmpty(UserAccount))
            {
                //�˻�ȡ��
                m_UserIDs = CommPage.GetSingleValue("SELECT UserID FROM USER_BaseInfo WHERE UserAccount='" + UserAccount + "'");

                string jbrzrrflg = "1";
                if(!string.IsNullOrEmpty(UserAccount))
                {
                    if( UserAccount.Length > 12 )
                    {
                        jbrzrrflg = "0";
                        UserAccount = UserAccount.Substring(4,12);
                    }
                }     
                //������Ϣ
                m_Dt = new DataTable();
                m_DtSign = new DataTable();
                m_DtSign2 = new DataTable();
                m_DtSign3 = new DataTable();
                StringBuilder sHtml = new StringBuilder();
                StringBuilder sHtmlSign = new StringBuilder();
                m_SqlParams = "SELECT [SealID], [SealName], [SealPath], [SealUserID], [SealUserName] FROM [SYS_Seal] WHERE SealUserID=" + m_UserIDs;
                m_SqlParamsSign = "SELECT [SignID], [SignQYName], [SignCode], [SignPath], [SignName], [SignPhone] , [SignWX] FROM [SYS_Sign] WHERE SignCode=" + UserAccount;
                m_SqlParamsSign2 = "SELECT * FROM AreaDetailCN WHERE ParentCode='" + UserAccount + "'";
                m_SqlParamsSign3 = "";
                if ( jbrzrrflg == "0" )
                {
                    m_SqlParamsSign = m_SqlParamsSign + " AND SignType='0'";
                }
                try
                {
                    m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                    sHtml.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"table_public_02\">");
                    sHtml.Append("<tbody>");
                    if (m_Dt.Rows.Count == 1)
                    {
                        sHtml.Append("<tr>");
                        sHtml.Append("<td class=\"title\" colspan=\"3\"><b>������Ϣ</b></td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"100\">����ȫ�ƣ�</th>");
                        sHtml.Append("<td>" + m_Dt.Rows[0]["SealName"].ToString() + "</td>");
                        sHtml.Append("<td rowspan=\"4\" align=\"left\"><img src=\"" + m_Dt.Rows[0]["SealPath"].ToString() + "\"  height=\"200\" /></td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th>�������룺</th>");
                        sHtml.Append("<td>" + UserAccount + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th>�������ƣ�</th>");
                        sHtml.Append("<td>" + m_Dt.Rows[0]["SealUserName"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th>������</th>");
                        sHtml.Append("<td><input type=\"button\" name=\"Butqzpwd\" value=\" �������� \" id=\"Butqzpwd\" class=\"submit6\" onClick=\"window.location.href='SysSealPwd.aspx?m_sid=" + m_Dt.Rows[0]["SealID"].ToString() + "&m_stype=1'\" /></td>");
                        sHtml.Append("</tr>");
                    }
                    else {

                        sHtml.Append("<tr>");
                        sHtml.Append("<td width=\"100\" align=\"left\" class=\"zhengwenjiacu\">������Ϣ</td>");
                        sHtml.Append("<td>&nbsp;&nbsp;</td>");
                        sHtml.Append("<td>��</td>");
                        sHtml.Append("</tr>");
                    }
                    sHtml.Append("</tbody>");
                    sHtml.Append("</table>");
                    m_Dt = null;
                    m_DtSign = DbHelperSQL.Query(m_SqlParamsSign).Tables[0];
                    //<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
                    //sHtml.Append("<table width=\"100%\" border=\"0\"  cellpadding=\"0\" cellspacing=\"0\"><tr><td height=\"4\" background=\"/images/xuxian.gif\" ></td></tr></table>");
                    sHtml.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"table_public_02\">");
                    sHtml.Append("<tbody>");
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"title\" colspan=\"2\"><b>ǩ����Ϣ</b></td>");
                    sHtml.Append("</tr>");
                    sHtml.Append("<tr>");
                    sHtml.Append("<td colspan=\"2\" style=\"padding:0;\">");
                    sHtml.Append("<div class=\"list\"><ul>");
                    for (int i = 0; i < m_DtSign.Rows.Count;i++ )
                    {
                        int j = 0;
                        if (i % 2 == 0)
                        {
                            j = 2;
                        }
                        else
                        {
                            j = 0;
                        }
                        sHtml.Append("<li><div class=\"list_x a" + j + "\"><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td width=\"100\" align=\"right\">����ȫ�ƣ�</td>");
                        sHtml.Append("<td>" + m_DtSign.Rows[i]["SignQYName"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td align=\"right\">�������룺</td>");
                        sHtml.Append("<td>" + m_DtSign.Rows[i]["SignCode"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td align=\"right\">������</td>");
                        sHtml.Append("<td>" + m_DtSign.Rows[i]["SignName"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td align=\"right\">ǩ�֣�</td>");
                        sHtml.Append("<td><img src=\"" + m_DtSign.Rows[i]["SignPath"].ToString() + "\"  height=\"25\" /></td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td align=\"right\">�ֻ��ţ�</td>");
                        sHtml.Append("<td>" + m_DtSign.Rows[i]["SignPhone"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;<a style=\"color:red;font-weight:bold;\" href=\"/SysAdmin/SysUserUpd.aspx?action=tel&k=" + m_DtSign.Rows[i]["SignID"].ToString() + "\">����</a></td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td align=\"right\">΢�ţ�</td>");
                        sHtml.Append("<td>" + m_DtSign.Rows[i]["SignWX"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;<a  style=\"color:red;font-weight:bold;\"  href=\"/SysAdmin/SysUserUpd.aspx?action=wx&k=" + m_DtSign.Rows[i]["SignID"].ToString() + "\">����</a></td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td align=\"right\">������</td>");
                        sHtml.Append("<td><input type=\"button\" name=\"Butqzpwd\" value=\" �������� \" id=\"Butqzpwd\" class=\"submit6\" onClick=\"window.location.href='SysSealPwd.aspx?m_sid=" + m_DtSign.Rows[i]["SignID"].ToString() + "&m_stype=2'\" /></td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("</table></div></li>");
                    }
                    sHtml.Append("</ul><div class=\"clr\"></div></div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                    sHtml.Append("</tbody>");
                    sHtml.Append("</table>");
                    //sHtml.Append("<table width=\"100%\" border=\"0\"  cellpadding=\"0\" cellspacing=\"0\"><tr><td height=\"4\" background=\"/images/xuxian.gif\" ></td></tr></table>");
                    m_DtSign2 = DbHelperSQL.Query(m_SqlParamsSign2).Tables[0];
                    sHtml.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"table_public_02\">");
                    sHtml.Append("<tbody>");
                    sHtml.Append("<tr>");
                    sHtml.Append("<td align=\"left\" class=\"title\" colspan=\"5\"><b>�졢�򡢴�(������/������/����)����Ϣ</b>&nbsp;&nbsp;&nbsp;<input type=\"button\" name=\"Butqz\" value=\" ��ʾ���� \" id=\"Butqz\" class=\"submit6\" /></td>");
                    sHtml.Append("</tr>");
                    sHtml.Append("<tr>");
                    sHtml.Append("<th width=\"110\">����</th>");
                    sHtml.Append("<th width=\"100\">��������</th>");
                    sHtml.Append("<th>");
                    sHtml.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                    sHtml.Append("<tr>");
                    sHtml.Append("<td width=\"25%\" style=\"text-align:left\">����</td>");
                    sHtml.Append("<td width=\"25%\" style=\"text-align:left\">ǩ��</td>");
                    sHtml.Append("<td style=\"text-align:left\">�ֻ���/΢��</td>");
                    sHtml.Append("<td width=\"80\" style=\"text-align:left\">����</td>");
                    sHtml.Append("</tr>");
                    sHtml.Append("</table>");
                    sHtml.Append("<script type=\"text/javascript\">");
                    sHtml.Append("var btn = document.getElementById('Butqz');");
                    sHtml.Append("btn.onclick=function(){");
                    sHtml.Append("if(btn.value.indexOf(' ���ع��� ')){");
                    sHtml.Append("btn.value = \" ���ع��� \";");
                    sHtml.Append("for (i = 0; i <= " + m_DtSign2.Rows.Count + "; i++) {document.getElementById(\"qz_\" + i).style.display = \"\";}");
                    sHtml.Append("}else{");
                    sHtml.Append("btn.value = \" ��ʾ���� \";");
                    sHtml.Append("for (i = 0; i <= " + m_DtSign2.Rows.Count + "; i++) {document.getElementById(\"qz_\" + i).style.display = \"none\";}");
                    sHtml.Append("}}");
                    sHtml.Append("</script>");
                    sHtml.Append("</th>");
                    sHtml.Append("<th width=\"200\">�˺�</th>");
                    sHtml.Append("<th width=\"200\">����</th>");
                    sHtml.Append("</tr>");
                    for (int i = 0; i < m_DtSign2.Rows.Count; i++)
                    {
                        sHtml.Append("<tr>");
                        sHtml.Append("<td>" + m_DtSign2.Rows[i]["AreaName"].ToString() + "</td>");
                        sHtml.Append("<td>" + m_DtSign2.Rows[i]["AreaCode"].ToString() + "</td>");
                        sHtml.Append("<td><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                        m_DtSign3 = DbHelperSQL.Query("SELECT [SignID], [SignQYName], [SignCode], [SignPath], [SignName], [SignPhone] , [SignWX] FROM [SYS_Sign] WHERE SignCode='" + m_DtSign2.Rows[i]["AreaCode"].ToString() + "' ").Tables[0];
                        for (int ii = 0; ii < m_DtSign3.Rows.Count; ii++)
                        {
                            sHtml.Append("<tr><td width=\"25%\">" + m_DtSign3.Rows[ii]["SignName"].ToString() + "</td>");
                            sHtml.Append("<td width=\"25%\"><img src=\"" + m_DtSign3.Rows[ii]["SignPath"].ToString() + "\"  height=\"25\" /></td>");
                            sHtml.Append("<td>�ֻ���" + m_DtSign3.Rows[ii]["SignPhone"].ToString() + "<br>΢�ţ�" + m_DtSign3.Rows[ii]["SignWX"].ToString() + "</td>");
                            sHtml.Append("<td width=\"80\"><input type=\"button\" name=\"Butqzpwd\" value=\" �������� \" id=\"Butqzpwd\" class=\"submit6\" onClick=\"window.location.href='SysSealPwd.aspx?m_sid=" + m_DtSign3.Rows[ii]["SignID"].ToString() + "&m_stype=2'\" /></td></tr>");
                        }

                        sHtml.Append("</table></td>");
                        sHtml.Append("<td>");
                        m_DtSign4 = DbHelperSQL.Query("SELECT [UserName], [UserLastLoginTime], [UserLoginNum] FROM [USER_BaseInfo] WHERE UserAccount='" + m_DtSign2.Rows[i]["AreaCode"].ToString() + "' ").Tables[0];
                        if (m_DtSign4.Rows.Count > 0)
                        {
                            sHtml.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"0\" class=\"zhengwen\">");
                            sHtml.Append("<tr><td>" + m_DtSign4.Rows[0]["UserName"].ToString() + "</td></tr>");
                            sHtml.Append("<tr><td>���һ�ε�½��" + Convert.ToDateTime(m_DtSign4.Rows[0]["UserLastLoginTime"].ToString()).ToString("yyyy-MM-dd") + "</td></tr>");
                            sHtml.Append("<tr><td>����½��" + m_DtSign4.Rows[0]["UserLoginNum"].ToString() + "����</td></tr>");
                            sHtml.Append("</table>");
                        }
                        else {
                            sHtml.Append("<div align=\"center\">���ݿ���δ¼���˺���Ϣ</div>");
                        }
                        sHtml.Append("</td>");
                        sHtml.Append("<td style=\"padding:8px;\">");
                        m_DtSign5 = DbHelperSQL.Query("SELECT [SealID], [SealName], [SealPath] FROM [SYS_Seal] WHERE SealPath like '%" + m_DtSign2.Rows[i]["AreaCode"].ToString() + "%' ").Tables[0];
                        if (m_DtSign5.Rows.Count > 0)
                        {
                            sHtml.Append("<div id=qz_" + i + " style=\"display:none\"><img src=\"" + m_DtSign5.Rows[0]["SealPath"].ToString()+ "\"  height=\"200\" /></div>");
                            sHtml.Append("<div align=\"center\">" + m_DtSign5.Rows[0]["SealName"].ToString() + "</div>");
                            sHtml.Append("<div align=\"center\"><input type=\"button\" name=\"Butqzpwd\" value=\" �������� \" id=\"Butqzpwd\" class=\"submit6\" onClick=\"window.location.href='SysSealPwd.aspx?m_sid=" + m_DtSign5.Rows[0]["SealID"].ToString() + "&m_stype=1'\" /></div>");
                        }
                        else
                        {
                            sHtml.Append("<div align=\"center\">���ݿ���δ¼�빫����Ϣ���޷��޸Ĺ�������</div>");
                        }
                        m_DtSign5 = null;
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");

                    }

                    sHtml.Append("</tbody>");
                    sHtml.Append("</table>");
                    m_DtSign2 = null;
                    m_DtSign = null;

                    this.LiteralData.Text = sHtml.ToString();
                }
                catch (Exception ex)
                {
                    m_Dt = null;
                    this.LiteralData.Text = "����ʧ�ܣ�" + ex.Message + "'";
                }
                sHtml = null;
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��", m_TargetUrl, true);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string UserAreaCode = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.UcAreaSel1.GetAreaCode()));
            ShowModInfo(UserAreaCode); 
        }
        #endregion

    }
}
