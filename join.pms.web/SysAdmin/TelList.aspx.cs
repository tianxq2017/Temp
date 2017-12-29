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

namespace join.pms.web.SysAdmin
{
    public partial class TelList : System.Web.UI.Page
    {
        private string m_UserID; // 当前登录的操作用户编号

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();

            this.LiteralNav.Text = "通讯录";
            GetDataList("","");
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
        /// 获取数据
        /// </summary>
        private void GetDataList(string strbm, string stryh)
        {
            string searchStr = "";
            if (!string.IsNullOrEmpty(strbm))
            {
                //searchStr = " AND UserUnitName like '%" + strbm + "%'";
                searchStr = " AND SignQYName like '%" + strbm + "%'";
            }
            if (!string.IsNullOrEmpty(stryh))
            {
                //searchStr = searchStr + " AND UserName like '%" + stryh + "%'";
                searchStr = searchStr + " AND SignName like '%" + stryh + "%'";
            }

            StringBuilder sHtml = new StringBuilder();
            DataTable m_Dt = new DataTable();
            //string sqlParams = "SELECT UserUnitName,UserName,UserTel FROM v_UserList WHERE 1=1";
            string sqlParams = "SELECT SignQYName as UserUnitName,SignName as UserName,SignPhone as UserTel FROM SYS_Sign WHERE 1=1";
            if (string.IsNullOrEmpty(strbm) && string.IsNullOrEmpty(stryh))
            {
            }
            else
            {
                sqlParams = sqlParams + searchStr;
            }
            //sqlParams = sqlParams + " AND ValidFlag=1 AND UserID>3 ORDER BY UserID";
            sqlParams = sqlParams + " ORDER BY SignCode ";

            m_Dt = DbHelperSQL.Query(sqlParams).Tables[0];
            sHtml.Append("系统通讯录如下：<br/>");
            sHtml.Append("<table width=\"800\" border=\"0\" cellspacing=\"0\" cellpadding=\"1\" bgcolor=\"#FFFFFF\"><tr>");
            sHtml.Append("<td width=\"45\" height=\"30\" align=\"center\" bgcolor=\"#99d0d0\" style=\"color:#FFFFFF;\"><strong>序号</strong></td>");
            sHtml.Append("<td width=\"360\" class=\"fb01\" bgcolor=\"#99d0d0\" align=\"center\" style=\"color:#FFFFFF;\">&nbsp;<strong>部门</strong></td>");
            sHtml.Append("<td width=\"230\" class=\"fb01\" bgcolor=\"#99d0d0\" align=\"center\" style=\"color:#FFFFFF;\">&nbsp;<strong>用户名称</strong></td>");
            sHtml.Append("<td class=\"fb01\" bgcolor=\"#99d0d0\" align=\"center\" style=\"color:#FFFFFF;\">&nbsp;<strong>电话</strong></td>");
            sHtml.Append("</tr></table>");
            if (m_Dt.Rows.Count > 0)
            {
                sHtml.Append("<table width=\"800\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" bgcolor=\"#008981\">");

                for (int k = 0; k < m_Dt.Rows.Count; k++)
                {
                    sHtml.Append("<tr onmouseover=\"this.className='lvtColDataHover'\" onmouseout=\"this.className='lvtColData'\">");
                    sHtml.Append("<td width=\"45\" height=\"20\" align=\"center\" bgcolor=\"#FFFFFF\" >" + (k + 1).ToString() + "</td>");
                    sHtml.Append("<td width=\"360\"  align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][0].ToString().Replace("通辽市", "") + "</td>");
                    sHtml.Append("<td width=\"230\" align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][1].ToString() + "</td>");
                    sHtml.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">&nbsp;" + m_Dt.Rows[k][2].ToString().Replace("13988888888", "") + "</td>");
                    sHtml.Append("</tr>");
                }
                sHtml.Append("</table>");
            }
            else { sHtml.Append("没有匹配的数据！"); }
            m_Dt = null;
            this.LiteralResults.Text = sHtml.ToString();
        }
        /// <summary>
        /// 检索事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //string strErr = string.Empty;
            string strBM =  PageValidate.GetTrim(this.txtBM.Value);
            string strYH =  PageValidate.GetTrim(this.txtYH.Value);

            GetDataList(strBM, strYH);
            //SetUIParams(startDate, endDate, startBJDate, endBJDate);
        }
    }
}
