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
    public partial class BizRating : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号

        private string m_SqlParams;
        public string m_TargetUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                SetOpratetionAction("");
            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "";// DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                //if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";
                cssFile = "/css/inidex.css";
                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
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
                m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
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
                case "audit":
                    funcName = "服务评价";
                    //ShowModInfo(m_ObjID);
                    break;
                case "view": // 查看
                    funcName = "服务评价查看";
                    ShowModInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "起始页  &gt;&gt; " + CommPage.GetAllBizName(m_FuncCode) + " &gt;" + funcName + "：";
        }


        /// <summary>
        /// 显示待处理信息
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            string StarLevel = string.Empty;
            StringBuilder s = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                // 1,调取当前审核用户信息
                m_SqlParams = "SELECT PersonID,StarLevel,Comments,CreateDate FROM BIZ_Critic WHERE BizID=" + m_ObjID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        s.Append("<tr class=\"zhengwen\">");
                        s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">评价星级：</td>");
                        s.Append("<td align=\"left\">" + GetStarLevel(sdr["StarLevel"].ToString()) + "</td>");
                        s.Append("</tr>");
                        s.Append("<tr class=\"zhengwen\">");
                        s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">评价内容：</td>");
                        s.Append("<td align=\"left\">" + sdr["Comments"].ToString() + "</td>");
                        s.Append("</tr>");
                        s.Append("<tr class=\"zhengwen\">");
                        s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">评价时间：</td>");
                        s.Append("<td align=\"left\">" + sdr["CreateDate"].ToString() + "</td>");
                        s.Append("</tr> ");
                    }
                    sdr.Close();
                }
                else
                {
                    s.Append("<tr class=\"zhengwen\">");
                    s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">评价星级：</td>");
                    s.Append("<td align=\"left\">&nbsp;</td>");
                    s.Append("</tr>");
                    s.Append("<tr class=\"zhengwen\">");
                    s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">评价内容：</td>");
                    s.Append("<td align=\"left\">&nbsp;</td>");
                    s.Append("</tr>");
                    s.Append("<tr class=\"zhengwen\">");
                    s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">评价时间：</td>");
                    s.Append("<td align=\"left\">&nbsp;</td>");
                    s.Append("</tr> ");
                }
            }
            catch
            {
                if (sdr != null) sdr.Close();

                s.Append("<tr class=\"zhengwen\">");
                s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">评价星级：</td>");
                s.Append("<td align=\"left\">&nbsp;</td>");
                s.Append("</tr>");
                s.Append("<tr class=\"zhengwen\">");
                s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">评价内容：</td>");
                s.Append("<td align=\"left\">&nbsp;</td>");
                s.Append("</tr>");
                s.Append("<tr class=\"zhengwen\">");
                s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">评价时间：</td>");
                s.Append("<td align=\"left\">&nbsp;</td>");
                s.Append("</tr> ");
                //Response.Write(" <script>alert('操作失败：审核签章时出现错误！') ;window.location.href='" + m_TargetUrl + "'</script>");
            }

            this.LiteralSvrsRating.Text = s.ToString();
        }

        private string GetStarLevel(string starLevel) {
            string returnVa = string.Empty;
            switch (starLevel)
            {
                case "1":  
                    returnVa = "<font color=\"red\">☆</font>☆☆☆☆";
                    break;
                case "2":
                    returnVa = "<font color=\"red\">☆☆</font>☆☆☆";
                    break;
                case "3":
                    returnVa = "<font color=\"red\">☆☆☆</font>☆☆";
                    break;
                case "4":
                    returnVa = "<font color=\"red\">☆☆☆☆</font>☆";
                    break;
                case "5":
                    returnVa = "<font color=\"red\">☆☆☆☆☆</font>";
                    break;
                default:
                    returnVa = "☆☆☆☆☆";
                    break;
            }
            return returnVa;
        }

        #endregion

       
    }
}


