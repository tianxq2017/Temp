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

using System.IO;
using System.Text;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;

namespace join.pms.web.BizInfo
{
    public partial class BizPhotos : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;

        private string m_ObjID;
        private string m_UserID; // 当前登录的操作用户编号
        private string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                ShowBizInfo(m_ObjID);
            }
        }

        #region 页码验证信息

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
                //m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }
        }
        #endregion

        /// <summary>
        /// 查看详细信息
        /// </summary>
        /// <param name="objID"></param>
        private void ShowBizInfo(string bizID)
        {
            string sqlParams = string.Empty;
            string bizName = "", navUrl = "", DocsType = string.Empty;

            StringBuilder s = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                bizName = CommPage.GetSingleVal("SELECT BizNameFull FROM BIZ_Categories WHERE BizCode='" + m_FuncCode + "'");
                sqlParams = "SELECT TOP 6 DocsType,DocsPath,SourceName,DocsName,OprateDate,IsInnerArea FROM BIZ_Docs WHERE BizID=" + bizID;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                s.Append("<div class=\"paper_pic_0101\">");
                s.Append("<div class=\"pic_title\">" + bizName + "</div>");
                s.Append("<div class=\"list clearfix\">");
                s.Append("<ul>");

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        navUrl = m_SvrsUrl + sdr["DocsPath"].ToString();
                        DocsType = sdr["DocsType"].ToString();

                        if (DocsType == ".jpg" || DocsType == ".gif" || DocsType == ".png" || DocsType == ".bmp")
                        {
                            s.Append("<li>");
                            s.Append("<p class=\"pic\"><span class=\"pic_center\"><i></i><img src=\"" + navUrl + "\" alt=\"" + sdr["DocsName"].ToString() + "\" /></span></p>");
                            s.Append("</li>");
                        }
                        else
                        {
                            //s.Append("<td align=\"left\"><a href=\"" + navUrl + "\" target=\"_blank\">" + sdr["DocsName"].ToString() + "</a></td>");
                        }
                    }
                    sdr.Close();
                    
                    s.Append("</ul>");
                    s.Append("</div>");
                    s.Append("</div>");
                }
            }
            catch { }

            this.LiteralDocs.Text = s.ToString();
        }
    }
}

