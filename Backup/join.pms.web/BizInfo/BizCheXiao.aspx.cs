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

namespace join.pms.web.BizInfo
{
    public partial class BizCheXiao : System.Web.UI.Page
    {

        private string m_UserID; // ��ǰ��¼�Ĳ����û����


        private string m_SqlParams;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            if (!IsPostBack)
            {
                string roleID = CommPage.GetSingleVal("SELECT [RoleID] FROM [SYS_UserRoles] WHERE UserID=" + this.m_UserID);
                if (String.IsNullOrEmpty(roleID) || roleID != "1")
                {
                    Response.Write("<script language='javascript'>alert('����ʧ�ܣ���������ҵ��ǿ�Ƴ�����Ȩ�ޣ�');parent.location.href='/MainFrame.aspx';</script>");
                    Response.End();
                }
                this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">��ʼҳ</a> &gt;&gt; ҵ��ǿ�Ƴ�����";
            }
        }
        #region
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

        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            StringBuilder sHtml = new StringBuilder();
            SqlDataReader sdr = null;
            string strErr = string.Empty;
            string personCidA, personCidB, sex = string.Empty;
            string bizID = PageValidate.GetTrim(this.txtBizID.Text);
            if (String.IsNullOrEmpty(bizID))
            {
                strErr += "������ҵ���ţ�\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            try
            {
                m_SqlParams = "SELECT Fileds01,Fileds08,PersonCidA,PersonCidB,RegAreaNameA,RegAreaNameB,CurAreaNameA,CurAreaNameB,BizName FROM BIZ_Contents WHERE BizID=" + bizID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    this.txtBizIDH.Value = bizID;
                    sHtml.Append("<div class=\"form_a\">");
                    sHtml.Append("<p class=\"form_title\"><b>��������ҵ��" + sdr["BizName"].ToString() + "����������Ϣ���£�</b></p>");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"tdcolumns\">	");
                    sHtml.Append("<tr><th>�Ա�</th><th>����</th><th>���֤��</th><th>������</th><th>�־�ס��</th></tr>");

                    personCidA = sdr["PersonCidA"].ToString();
                    personCidB = sdr["PersonCidB"].ToString();
                    if (!string.IsNullOrEmpty(personCidA)) { sex = CommBiz.GetSexByID(personCidA); }
                    
                    sHtml.Append("<tr>");
                    sHtml.Append("<td>" + sex + "</td>");
                    sHtml.Append("<td>" + sdr["Fileds01"].ToString() + "</td>");
                    sHtml.Append("<td>" + personCidA + "</td>");
                    sHtml.Append("<td>" + sdr["RegAreaNameA"].ToString() + "</td>");
                    sHtml.Append("<td>" + sdr["CurAreaNameA"].ToString() + "</td>");
                    sHtml.Append("</tr>");


                    if (!string.IsNullOrEmpty(personCidB)) { sex = CommBiz.GetSexByID(personCidB); }
                    sHtml.Append("<tr>");
                    sHtml.Append("<td>Ů</td>");
                    sHtml.Append("<td>" + sdr["Fileds08"].ToString() + "</td>");
                    sHtml.Append("<td>" + sex + "</td>");
                    sHtml.Append("<td>" + sdr["RegAreaNameB"].ToString() + "</td>");
                    sHtml.Append("<td>" + sdr["CurAreaNameB"].ToString() + "</td>");
                    sHtml.Append("</tr>");

                    sHtml.Append("</table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</div>");
                }
                sdr.Close();

            }
            catch
            {
                if (sdr != null) sdr.Close();
                sHtml.Append("����ʧ�ܣ���ȡ��Ϣʱ���ִ���");
            }

            this.LiteralBizData.Text = sHtml.ToString();

        }

        protected void btnCheXiao_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string bizID = PageValidate.GetTrim(this.txtBizIDH.Value);
            if (String.IsNullOrEmpty(bizID))
            {
                strErr += "��ǰҵ���¼Ϊ�գ����س�����\\n";
            }
            else
            {
                if (!PageValidate.IsNumber(bizID)) { strErr += "��������ȷ��ҵ���¼��ţ�\\n"; }
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            try
            {
                // Attribs: 0,��ʼ�ύ;1,������ 2,ͨ�� 3,���� 4,���� 5,ע��, 6,�ȴ�����,8,�ѳ�֤,9,�鵵*/
                m_SqlParams = "UPDATE BIZ_Contents SET Attribs=4 WHERE BizID =" + bizID;
                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                    //ҵ����־
                    CommPage.SetBizLog(bizID, m_UserID, "ҵ��ǿ�Ƴ���", "����Ա�û�ID[" + m_UserID + "-]�� " + DateTime.Now.ToString() + " ������ҵ��ǿ�Ƴ�������");
                    Response.Write(" <script>alert('������ʾ��ҵ��ǿ�Ƴ����ɹ���') ;window.location.href='BizCheXiao.aspx'</script>");
                }
                else
                {
                    MessageBox.Show(this, "������ʾ��ҵ��ǿ�Ƴ���ʧ�ܣ�");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
        }

    }
}


