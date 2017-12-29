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

        private string m_UserID; // 当前登录的操作用户编号


        private string m_SqlParams;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            if (!IsPostBack)
            {
                string roleID = CommPage.GetSingleVal("SELECT [RoleID] FROM [SYS_UserRoles] WHERE UserID=" + this.m_UserID);
                if (String.IsNullOrEmpty(roleID) || roleID != "1")
                {
                    Response.Write("<script language='javascript'>alert('操作失败：您不具有业务强制撤销的权限！');parent.location.href='/MainFrame.aspx';</script>");
                    Response.End();
                }
                this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">起始页</a> &gt;&gt; 业务强制撤销：";
            }
        }
        #region
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
                strErr += "请输入业务编号！\\n";
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
                    sHtml.Append("<p class=\"form_title\"><b>申请办理的业务：" + sdr["BizName"].ToString() + "，申请人信息如下：</b></p>");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"tdcolumns\">	");
                    sHtml.Append("<tr><th>性别</th><th>姓名</th><th>身份证号</th><th>户籍地</th><th>现居住地</th></tr>");

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
                    sHtml.Append("<td>女</td>");
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
                sHtml.Append("操作失败：获取信息时出现错误！");
            }

            this.LiteralBizData.Text = sHtml.ToString();

        }

        protected void btnCheXiao_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string bizID = PageValidate.GetTrim(this.txtBizIDH.Value);
            if (String.IsNullOrEmpty(bizID))
            {
                strErr += "当前业务记录为空，不必撤销！\\n";
            }
            else
            {
                if (!PageValidate.IsNumber(bizID)) { strErr += "请输入正确的业务记录编号！\\n"; }
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            try
            {
                // Attribs: 0,初始提交;1,审批中 2,通过 3,补正 4,撤销 5,注销, 6,等待审批,8,已出证,9,归档*/
                m_SqlParams = "UPDATE BIZ_Contents SET Attribs=4 WHERE BizID =" + bizID;
                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                    //业务日志
                    CommPage.SetBizLog(bizID, m_UserID, "业务强制撤销", "管理员用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 进行了业务强制撤销操作");
                    Response.Write(" <script>alert('操作提示：业务强制撤销成功！') ;window.location.href='BizCheXiao.aspx'</script>");
                }
                else
                {
                    MessageBox.Show(this, "操作提示：业务强制撤销失败！");
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


