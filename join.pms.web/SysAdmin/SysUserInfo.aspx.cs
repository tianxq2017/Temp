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
    public partial class SysUserInfo : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_ActionName;
        private string m_ObjID;
        private string m_UserID; // 当前登录的操作用户编号
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
        /// 初始化
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
                SetOpratetionAction("用户信息");
                if (m_UserID != "1")
                {
                    ShowModInfo(m_UserID);
                }
            }
        }

        /// <summary>
        /// 参数取得
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
            //    Response.Write("非法访问，操作被终止！");
            //    Response.End();
            //}

            //if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        /// <summary>
        /// 样式取得
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
            //string funcName = string.Empty;

            //if (String.IsNullOrEmpty(m_ObjID))
            //{
            //    if (m_ActionName != "add")
            //    {
            //        Response.Write("非法访问：操作被终止！");
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
            //    case "add": // 新增
            //        funcName = "";
            //        break;
            //    case "view": // 信息查看
            //        funcName = "";
            //        ShowModInfo(m_ObjID);
            //        break;
            //    case "del": // 删除
            //        funcName = "删除内容";
            //        DelInfo(m_ObjID);
            //        break;
            //    default:
            //        MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
            //        break;
            //}
            //this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; ";
        }
        ///// <summary>
        ///// 手机信息
        ///// </summary>
        ///// <param name="objID"></param>
        //private void telInfo(string objID)
        //{

        //    try
        //    {
        //        m_SqlParams = " UPDATE SYS_Sign SET  SignPhone = CmsStats+1 WHERE CmsID=" + objID;

        //        DbHelperSQL.ExecuteSql(m_SqlParams);
        //        MessageBox.ShowAndRedirect(this.Page, "操作提示：删除成功！", m_TargetUrl, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
        //    }
        //    Response.End();

        //}
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
        /// <summary>
        /// 详细信息取得
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string m_UserID)
        {
            if (!String.IsNullOrEmpty(m_UserID))
            {
                //账户取得
                string UserAccount = CommPage.GetSingleValue("SELECT UserAccount FROM USER_BaseInfo WHERE UserID=" + m_UserID);


                string jbrzrrflg = "1";
                if(!string.IsNullOrEmpty(UserAccount))
                {
                    if( UserAccount.Length > 12 )
                    {
                        jbrzrrflg = "0";
                        UserAccount = UserAccount.Substring(4,12);
                    }
                }     
                //公章信息
                m_Dt = new DataTable();
                m_DtSign = new DataTable();
                m_DtSign2 = new DataTable();
                m_DtSign3 = new DataTable();
                StringBuilder sHtml = new StringBuilder();
                StringBuilder sHtmlSign = new StringBuilder();
                m_SqlParams = "SELECT [SealID], [SealName], [SealPath], [SealUserID], [SealUserName] FROM [SYS_Seal] WHERE SealUserID=" + m_UserID;
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
                    sHtml.Append("<table width=\"80%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"0\" class=\"zhengwen\">");
                    sHtml.Append("<tbody>");
                    if (m_Dt.Rows.Count == 1)
                    {
                        sHtml.Append("<tr>");
                        sHtml.Append("<td width=\"100\" align=\"left\" class=\"zhengwenjiacu\">签章信息</td>");
                        sHtml.Append("<td>&nbsp;&nbsp;</td>");
                        sHtml.Append("<td rowspan=\"5\" align=\"left\"><img src=\"" + m_Dt.Rows[0]["SealPath"].ToString() + "\"  height=\"200\" /></td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td width=\"100\" align=\"right\">地区全称：</td>");
                        sHtml.Append("<td>" + m_Dt.Rows[0]["SealName"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td align=\"right\" >区划代码：</td>");
                        sHtml.Append("<td>" + UserAccount + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td align=\"right\">区划名称：</td>");
                        sHtml.Append("<td>" + m_Dt.Rows[0]["SealUserName"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td align=\"right\">&nbsp;</td>");
                        sHtml.Append("<td>&nbsp;</td>");
                        sHtml.Append("</tr>");
                    }
                    else {

                        sHtml.Append("<tr>");
                        sHtml.Append("<td width=\"100\" align=\"left\" class=\"zhengwenjiacu\">签章信息</td>");
                        sHtml.Append("<td>&nbsp;&nbsp;</td>");
                        sHtml.Append("<td>无</td>");
                        sHtml.Append("</tr>");
                    }
                    sHtml.Append("</tbody>");
                    sHtml.Append("</table>");
                    m_Dt = null;
                    m_DtSign = DbHelperSQL.Query(m_SqlParamsSign).Tables[0];
                    //<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
                    sHtml.Append("<table width=\"100%\" border=\"0\"  cellpadding=\"0\" cellspacing=\"0\"><tr><td height=\"4\" background=\"/images/xuxian.gif\" ></td></tr></table>");
                    sHtml.Append("<table width=\"80%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"0\" class=\"zhengwen\">");
                    sHtml.Append("<tbody>");
                    sHtml.Append("<tr>");
                    sHtml.Append("<td width=\"100\" align=\"left\" class=\"zhengwenjiacu\">签字信息</td>");
                    sHtml.Append("<td>&nbsp;&nbsp;</td>");
                    sHtml.Append("</tr>");
                    sHtml.Append("<tr>");
                    sHtml.Append("<td colspan=\"2\">");
                     
                    for (int i = 0; i < m_DtSign.Rows.Count;i++ )
                    {
                        sHtml.Append("<div  style=\"width:320px; float:left\"><table width=\"95%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"0\" class=\"zhengwen\">");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td width=\"100\" align=\"right\">地区全称：</td>");
                        sHtml.Append("<td>" + m_DtSign.Rows[i]["SignQYName"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td align=\"right\">区划代码：</td>");
                        sHtml.Append("<td>" + m_DtSign.Rows[i]["SignCode"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td align=\"right\">姓名：</td>");
                        sHtml.Append("<td>" + m_DtSign.Rows[i]["SignName"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td align=\"right\">签字：</td>");
                        sHtml.Append("<td><img src=\"" + m_DtSign.Rows[i]["SignPath"].ToString() + "\"  height=\"25\" /></td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td align=\"right\">手机号：</td>");
                        sHtml.Append("<td>" + m_DtSign.Rows[i]["SignPhone"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;<a style=\"color:red;font-weight:bold;\" href=\"/SysAdmin/SysUserUpd.aspx?action=tel&k=" + m_DtSign.Rows[i]["SignID"].ToString() + "\">更新</a></td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<td align=\"right\">微信：</td>");
                        sHtml.Append("<td>" + m_DtSign.Rows[i]["SignWX"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;<a  style=\"color:red;font-weight:bold;\"  href=\"/SysAdmin/SysUserUpd.aspx?action=wx&k=" + m_DtSign.Rows[i]["SignID"].ToString() + "\">更新</a></td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("</table></div>");
                    }
                    sHtml.Append("<div style=\" clear:both\"></div></td>");
                    sHtml.Append("</tr>");
                    sHtml.Append("</tbody>");
                    sHtml.Append("</table>");
                    sHtml.Append("<table width=\"100%\" border=\"0\"  cellpadding=\"0\" cellspacing=\"0\"><tr><td height=\"4\" background=\"/images/xuxian.gif\" ></td></tr></table>");
                    m_DtSign2 = DbHelperSQL.Query(m_SqlParamsSign2).Tables[0];
                    sHtml.Append("<table width=\"80%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"0\" class=\"zhengwen\">");
                    sHtml.Append("<tbody>");
                    sHtml.Append("<tr>");
                    sHtml.Append("<td align=\"left\" class=\"zhengwenjiacu\" colspan=\"4\">旗、镇、村(经办人/责任人/签章)等信息&nbsp;&nbsp;&nbsp;<input type=\"button\" name=\"Butqz\" value=\" 显示签章 \" id=\"Butqz\" class=\"submit6\" /></td>");
                    sHtml.Append("</tr>");
                    sHtml.Append("<tr>");
                    sHtml.Append("<td>地区</td>");
                    sHtml.Append("<td>区划代码</td>");
                    sHtml.Append("<td>");
                    sHtml.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"0\" class=\"zhengwen\">");
                    sHtml.Append("<tr>");
                    sHtml.Append("<td width=\"25%\">姓名</td>");
                    sHtml.Append("<td width=\"25%\">签字</td>");
                    sHtml.Append("<td width=\"25%\">手机号</td>");
                    sHtml.Append("<td width=\"25%\">微信</td>");
                    sHtml.Append("</tr>");
                    sHtml.Append("</table>");
                    sHtml.Append("<script type=\"text/javascript\">");
                    sHtml.Append("var btn = document.getElementById('Butqz');");
                    sHtml.Append("btn.onclick=function(){");
                    sHtml.Append("if(btn.value.indexOf(' 隐藏签章 ')){");
                    sHtml.Append("btn.value = \" 隐藏签章 \";");
                    sHtml.Append("for (i = 0; i <= " + m_DtSign2.Rows.Count + "; i++) {document.getElementById(\"qz_\" + i).style.display = \"\";}");
                    sHtml.Append("}else{");
                    sHtml.Append("btn.value = \" 显示签章 \";");
                    sHtml.Append("for (i = 0; i <= " + m_DtSign2.Rows.Count + "; i++) {document.getElementById(\"qz_\" + i).style.display = \"none\";}");
                    sHtml.Append("}}");
                    sHtml.Append("</script>");
                    sHtml.Append("</td>");
                    sHtml.Append("<td>账号</td>");
                    sHtml.Append("<td align=\"center\" width=\"200\">签章</td>");
                    sHtml.Append("</tr>");
                    for (int i = 0; i < m_DtSign2.Rows.Count; i++)
                    {
                        sHtml.Append("<tr>");
                        sHtml.Append("<td>" + m_DtSign2.Rows[i]["AreaName"].ToString() + "</td>");
                        sHtml.Append("<td>" + m_DtSign2.Rows[i]["AreaCode"].ToString() + "</td>");
                        sHtml.Append("<td><table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"0\" class=\"zhengwen\">");
                        m_DtSign3 = DbHelperSQL.Query("SELECT [SignID], [SignQYName], [SignCode], [SignPath], [SignName], [SignPhone] , [SignWX] FROM [SYS_Sign] WHERE SignCode='" + m_DtSign2.Rows[i]["AreaCode"].ToString() + "' ").Tables[0];
                        for (int ii = 0; ii < m_DtSign3.Rows.Count; ii++)
                        {
                            sHtml.Append("<tr><td width=\"25%\">" + m_DtSign3.Rows[ii]["SignName"].ToString() + "</td>");
                            sHtml.Append("<td width=\"25%\"><img src=\"" + m_DtSign3.Rows[ii]["SignPath"].ToString() + "\"  height=\"25\" /></td>");
                            sHtml.Append("<td width=\"25%\">" + m_DtSign3.Rows[ii]["SignPhone"].ToString() + "</td>");
                            sHtml.Append("<td width=\"25%\">" + m_DtSign3.Rows[ii]["SignWX"].ToString() + "</td></tr>");
                        }

                        sHtml.Append("</table></td>");
                        sHtml.Append("<td>");
                        m_DtSign4 = DbHelperSQL.Query("SELECT [UserName], [UserLastLoginTime], [UserLoginNum] FROM [USER_BaseInfo] WHERE UserAccount='" + m_DtSign2.Rows[i]["AreaCode"].ToString() + "' ").Tables[0];
                        if (m_DtSign4.Rows.Count > 0)
                        {
                            sHtml.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"0\" class=\"zhengwen\">");
                            sHtml.Append("<tr><td>" + m_DtSign4.Rows[0]["UserName"].ToString() + "</td></tr>");
                            if (string.IsNullOrEmpty(m_DtSign4.Rows[0]["UserLastLoginTime"].ToString()))
                            {
                                sHtml.Append("<tr><td>NULL</td></tr>");
                                sHtml.Append("<tr><td>共登陆（" + m_DtSign4.Rows[0]["UserLoginNum"].ToString() + "）次</td></tr>");
                            }
                            else
                            {
                                sHtml.Append("<tr><td>最后一次登陆：" + Convert.ToDateTime(m_DtSign4.Rows[0]["UserLastLoginTime"].ToString()).ToString("yyyy-MM-dd") + "</td></tr>");
                                sHtml.Append("<tr><td>共登陆（" + m_DtSign4.Rows[0]["UserLoginNum"].ToString() + "）次</td></tr>");
                            }
                            sHtml.Append("</table>");
                        }
                        else
                        {
                            sHtml.Append("<div align=\"center\">数据库中未录入账号信息</div>");
                        }
                        sHtml.Append("</td>");
                        sHtml.Append("<td align=\"center\">");
                        sHtml.Append("<div id=qz_" + i + " style=\"display:none\"><img src=\"/images/sign/" + m_DtSign2.Rows[i]["AreaCode"].ToString().Substring(0, 6) + "/" + m_DtSign2.Rows[i]["AreaCode"].ToString() + ".png\"  height=\"200\" /></div>");
                        m_DtSign5 = DbHelperSQL.Query("SELECT [SealName], [SealPath] FROM [SYS_Seal] WHERE SealPath='/images/sign/" + m_DtSign2.Rows[i]["AreaCode"].ToString().Substring(0, 6) + "/" + m_DtSign2.Rows[i]["AreaCode"].ToString() + ".png' ").Tables[0];
                        if (m_DtSign5.Rows.Count > 0)
                        {
                            sHtml.Append("<div align=\"center\">" + m_DtSign5.Rows[0]["SealName"].ToString() + "</div>");
                        }
                        else
                        {
                            sHtml.Append("<div align=\"center\">数据库中未录入签章信息</div>");
                        }

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
                    this.LiteralData.Text = "操作失败：" + ex.Message + "'";
                }
                sHtml = null;
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：", m_TargetUrl, true);
            }
        }
    
        #endregion

    }
}
