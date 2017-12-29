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
using join.pms.dal;

namespace join.pms.web
{
    public partial class MainLeft : System.Web.UI.Page
    {
        private string m_SqlParams;
        protected string m_MenuType;

        private DataTable m_TreeDt;

        private string m_UserID; // 当前登录的操作用户编号

        private string m_UnitCode;
        protected string m_RootMenuNum;

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateParams();
            AuthenticateUser();

            if (!IsPostBack)
            {
                try
                {
                    SetPageStyle(m_UserID);
                    InitializeSysMenu("");
                }
                catch
                {
                    this.LiteralSysMenu.Text = "加载系统菜单时遇到障碍，请刷新后再试……";
                }
            }
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
        /// 验证接受的参数
        /// </summary>
        private void ValidateParams()
        {
            m_MenuType = PageValidate.GetTrim(Request.QueryString["c"]);
            if (!string.IsNullOrEmpty(m_MenuType))
            {
            }
            else
            {
                m_MenuType = "00";
            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "";//DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
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
        /// 初始化系统菜单
        /// </summary>
        /// <param name="funcNo"></param>
        private void InitializeSysMenu(string funcNo)
        {
            string FuncCode = string.Empty;
            string FuncName = string.Empty;
            string FuncURL = string.Empty;
            string FuncIMG = string.Empty;
            string FuncOrder = string.Empty;


            string linkTarget = string.Empty;
            int sonCount = 0;
            int nodeSonCount = 0;

            int nodeFir = 0;
            int nodeFirSonCount = 0;
            int nodeSec = 0;
            int nodeSecSonCount = 0;
            int nodeThr = 0;

            string userPowers = DbHelperSQL.GetUserPower(m_UserID);
            string userBizPowers = CommPage.GetUserBizPower(m_UserID);//业务权限 
            //m_UnitCode = join.pms.web.Biz.CommPage.GetUnitCodeByUser(m_UserID);
            if (string.IsNullOrEmpty(userPowers))
            {
                Session["AREWEB_OC_USERID"] = null;
                Session.Clear();
                Session.Abandon();

                HttpCookie loginCookie = Request.Cookies["AREWEB_OC_USER_YSL"];
                if (loginCookie != null)
                {
                    loginCookie.Values["UserID"] = null;
                    loginCookie.Expires = DateTime.Now.AddDays(-1);
                    loginCookie = null;
                }

                Response.Cookies["AREWEB_OC_USERID"].Expires = DateTime.Now.AddDays(-1);
                Response.Write("<script language='javascript'>alert('操作失败：该用户归属角色集没有设置操作权限，请联系系统管理员解决此问题！');parent.location.href='/';</script>");
                return;
            }
            GetRootMenuCount(userPowers);//计算根节点数量
            //m_RootMenuNum = "9";

            StringBuilder sHtml = new StringBuilder();
            // AND FuncCode IN(" + userPowers + ")
            //if (m_MenuType == "02")
            //{
            //    m_SqlParams = "SELECT BizCode,BizNameAB,BizUrl,BizImg,(SELECT COUNT(*) FROM BIZ_Categories WHERE Attribs=0 AND SubString(BizCode,1,Len(BizCode)-2) = A.BizCode AND BizCode IN(" + userBizPowers + ")) AS SonCount,OrderCode FROM BIZ_Categories A WHERE Attribs=0 AND BizCode IN(" + userBizPowers + ") ORDER BY OrderCode";
            //}
            //else
            //{
            //    m_SqlParams = "SELECT FuncCode, FuncName, FuncURL,FuncIMG,(SELECT COUNT(*) FROM SYS_Function WHERE FuncStatus=0 AND SubString(FuncCode,1,Len(FuncCode)-2) = A.FuncCode AND FuncCode IN(" + userPowers + ")) AS SonCount,FuncOrder FROM [SYS_Function] A WHERE FuncStatus=0 AND FuncCode LIKE '" + m_MenuType + "%' AND FuncCode != '" + m_MenuType + "' AND FuncCode IN(" + userPowers + ")";
            //}
            if (m_MenuType == "00")
            {
                m_SqlParams = "SELECT FuncCode, FuncName, FuncURL,FuncIMG,(SELECT COUNT(*) FROM SYS_Function WHERE FuncStatus=0 AND SubString(FuncCode,1,Len(FuncCode)-2) = A.FuncCode) AS SonCount,FuncOrder FROM [SYS_Function] A WHERE FuncStatus=0 AND (FuncCode LIKE '01%' OR FuncCode LIKE '02%' OR FuncCode LIKE '03%')";
            }
            else
            {
                //m_SqlParams = "SELECT FuncCode, FuncName, FuncURL,FuncIMG,(SELECT COUNT(*) FROM SYS_Function WHERE FuncStatus=0 AND SubString(FuncCode,1,Len(FuncCode)-2) = A.FuncCode AND FuncCode IN(" + userPowers + ")) AS SonCount,FuncOrder FROM [SYS_Function] A WHERE FuncStatus=0 AND FuncCode LIKE '" + m_MenuType + "%' AND FuncCode != '" + m_MenuType + "' AND FuncCode IN(" + userPowers + ")";
                m_SqlParams = "SELECT FuncCode, FuncName, FuncURL,FuncIMG,(SELECT COUNT(*) FROM SYS_Function WHERE FuncStatus=0 AND SubString(FuncCode,1,Len(FuncCode)-2) = A.FuncCode ) AS SonCount,FuncOrder FROM [SYS_Function] A WHERE FuncStatus=0 AND FuncCode LIKE '" + m_MenuType + "%' AND FuncCode != '" + m_MenuType + "'";
            }
            m_TreeDt = new DataTable();
            m_TreeDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            if (m_TreeDt.Rows.Count > 0)
            {
                sHtml.Append("<input name=\"txtRootNum\" type=\"hidden\" id=\"txtRootNum\" value=\"" + m_RootMenuNum + "\" />");
                sHtml.Append("<!--------------------------------------------------->\r\n");
                sHtml.Append("<table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" border=\"0\"><tbody>");

                for (int i = 0; i < m_TreeDt.Rows.Count; i++)
                {

                    FuncCode = PageValidate.GetTrim(m_TreeDt.Rows[i][0].ToString());
                    FuncName = PageValidate.GetTrim(m_TreeDt.Rows[i][1].ToString());
                    FuncURL = PageValidate.GetTrim(m_TreeDt.Rows[i][2].ToString());
                    FuncIMG = PageValidate.GetTrim(m_TreeDt.Rows[i][3].ToString());
                    sonCount = int.Parse(m_TreeDt.Rows[i][4].ToString());
                    FuncOrder = PageValidate.GetTrim(m_TreeDt.Rows[i][5].ToString());
                    if (String.IsNullOrEmpty(FuncURL))
                    {
                        FuncURL = "#";
                        linkTarget = "_self";
                    }
                    else
                    {
                        if (FuncURL.IndexOf("?") < 3) FuncURL = FuncURL + "?1=1";
                        FuncURL = FuncURL + "&FuncCode=" + FuncCode + "&FuncNa=" + Server.UrlEncode(FuncName);
                        linkTarget = "mainFrame";
                    }
                    // 一级节点 onmouseover=\"this.className='lm_yi lm_yi_hover'\" onmouseout=\"this.className='lm_yi'\"
                    if ((FuncCode.Length == 4 && m_MenuType != "00") || (FuncCode.Length == 2 && m_MenuType == "00"))
                    {
                        nodeFir++;
                        nodeFirSonCount = sonCount;
                        //适用网格化一级菜单
                        //if (FuncCode.Substring(0, 2) == "03")
                        //{
                        //    sHtml.Append("<tr><td id=\"root" + nodeFir + "\" align=\"left\"  class=\"lm_yi\" onclick=\"javascript:ShowSubMenu(" + nodeFir + ")\" onmouseover=\"this.style.cursor='hand'\"><img src=\"images/" + FuncIMG + "\" width=\"16\" height=\"16\"> <a title=\"" + FuncName + "\" href=\"" + FuncURL + "\" target=\"" + linkTarget + "\" onclick=\"HideRightFrm();\" > " + FuncName + "</a></td></tr>");
                        //}
                        //else
                        //{
                        //    sHtml.Append("<tr><td id=\"root" + nodeFir + "\" align=\"left\"  class=\"lm_yi\" onclick=\"javascript:ShowSubMenu(" + nodeFir + ")\" onmouseover=\"this.style.cursor='hand'\"><img src=\"images/" + FuncIMG + "\" width=\"16\" height=\"16\"> <a title=\"" + FuncName + "\" href=\"" + FuncURL + "\" target=\"" + linkTarget + "\" > " + FuncName + "</a></td></tr>");
                        //}
                        sHtml.Append("<tr><td id=\"root" + nodeFir + "\" align=\"left\"  class=\"lm_yi\" onclick=\"javascript:ShowSubMenu(" + nodeFir + ")\" onmouseover=\"this.style.cursor='hand'\"><a title=\"" + FuncName + "\" href=\"" + FuncURL + "\" target=\"" + linkTarget + "\"> <img src=\"images/" + FuncIMG + "\" width=\"16\" height=\"16\"> " + FuncName + "</a></td></tr>");
                        if (sonCount > 0)
                        {
                            sHtml.Append("<tr id=\"menu" + nodeFir + "\" style=\"display: none;padding-left:20px\"><td class=\"lm_er_t\" >");
                            sHtml.Append("<table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" border=\"0\" align=\"left\"><tbody>");
                        }
                        nodeSec = 0;
                    }
                    // 二级节点
                    if ((FuncCode.Length == 6 && m_MenuType != "00") || (FuncCode.Length == 4 && m_MenuType == "00"))
                    {
                        nodeSec++;
                        nodeSecSonCount = sonCount;

                        if (sonCount > 0)
                        {
                            sHtml.Append("<tr><td class=\"lm_er_a\" onclick=\"javascript:ShowSubMenu(" + nodeFir.ToString() + nodeSec.ToString() + "0)\" ><a title=\"" + FuncName + "\" href=\"" + FuncURL + "\" target=\"" + linkTarget + "\" onclick=\"HideRightFrm();\"> " + FuncName + "</a></td></tr>");
                            sHtml.Append("<tr id=\"menu" + nodeFir.ToString() + nodeSec.ToString() + "0\" style=\"display: none\"><td class=\"lm_san_a\">");
                            sHtml.Append("<table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" border=\"0\" align=\"left\"><tbody>");
                        }
                        else
                        {
                            sHtml.Append("<tr><td class=\"lm_er_b\" onclick=\"javascript:ShowSubMenu(" + nodeFir.ToString() + nodeSec.ToString() + "0)\" ><a title=\"" + FuncName + "\" href=\"" + FuncURL + "\" target=\"" + linkTarget + "\" onclick=\"HideRightFrm();\"> " + FuncName + "</a></td></tr>");
                        }
                        // 结束子节点
                        if (nodeFirSonCount == nodeSec && sonCount == 0)
                        {
                            sHtml.Append("</tbody></table>");
                            sHtml.Append("</td></tr>");
                        }
                        nodeThr = 0;
                    }
                    // 三级节点 01010110
                    if ((FuncCode.Length == 8 && m_MenuType != "00") || (FuncCode.Length == 6 && m_MenuType == "00"))
                    {
                        nodeThr++;
                        if (FuncCode.Substring(0, 4) == "0102")
                        {
                            //差异数据
                            if (DbHelperSQL.GetSingle("SELECT COUNT(*) FROM v_PIS WHERE AnalysisFlag=2 AND FuncNo='02" + FuncCode.Substring(2) + "'").ToString() == "0" && FuncCode != "060101")
                            {
                                sHtml.Append("<tr><td class=\"lm_san\"><a title=\"" + FuncName + "\" href=\"" + FuncURL + "\" target=\"" + linkTarget + "\" onclick=\"HideRightFrm();\"> " + FuncName + "</a> </td></tr>");
                            }
                            else
                            {
                                sHtml.Append("<tr><td class=\"lm_san\"><a title=\"" + FuncName + "\" href=\"" + FuncURL + "\" target=\"" + linkTarget + "\" class=\"color_02\" onclick=\"HideRightFrm();\"> " + FuncName + "</a> </td></tr>");
                            }
                        }
                        // 01010116,01010117,01010118,01010120
                        else if (FuncCode == "01010101" || FuncCode == "01010102" || FuncCode == "01010103" || FuncCode == "01010104" || FuncCode == "01010105" || FuncCode == "01010106" || FuncCode == "01010122" || FuncCode == "01010123" || FuncCode == "01010124" || FuncCode == "01010125" || FuncCode == "01010126" || FuncCode == "01010127" || FuncCode == "01010128" || FuncCode == "01010129" || FuncCode == "01010130" || FuncCode == "01010131" || FuncCode == "01010116" || FuncCode == "01010117" || FuncCode == "01010118" || FuncCode == "01010120")
                        {
                            // 全员查询数据
                            sHtml.Append("<tr><td class=\"lm_san\"><a title=\"" + FuncName + "\" href=\"" + FuncURL + "\" target=\"" + linkTarget + "\" class=\"color_02\" onclick=\"HideRightFrm();\"> " + FuncName + "</a> </td></tr>");
                        }
                        else
                        {

                            sHtml.Append("<tr><td class=\"lm_san\"><a title=\"" + FuncName + "\" href=\"" + FuncURL + "\" target=\"" + linkTarget + "\" onclick=\"HideRightFrm();\"> " + FuncName + "</a> </td></tr>");
                        }

                        if (nodeSecSonCount == nodeThr)
                        {
                            sHtml.Append("</tbody></table>"); // <tr><td style=\"padding-left: 40px\" height=\"10\" align=\"left\">&nbsp;</td></tr>
                            sHtml.Append("</td></tr>");
                            if (nodeFirSonCount == nodeSec) sHtml.Append("</tbody></table></td></tr>");
                        }
                    }
                    /*
                    if (FuncCode.Length == 4)
                    {
                        nodeSec++;
                        nodeSecSonCount = sonCount;
                        if (sonCount > 0) { subBg = "add.gif"; } else { subBg = "icon03.gif"; }
                        sHtml.Append("<tr><td class=\"lnav02\" align=\"left\" onclick=\"javascript:ShowSubMenu(" + nodeFir.ToString() + nodeSec.ToString() + "0)\" onmouseover=\"this.style.cursor='hand'\"><img  src=\"images/" + subBg + "\" width=\"5\" height=\"3\" align=\"absmiddle\" /> <a title=\"" + FuncName + "\" href=\"" + FuncURL + "\" target=\"" + linkTarget + "\"> " + FuncName + "</a></td></tr>");
                        if (sonCount > 0)
                        {
                            sHtml.Append("<tr><td height=\"3\" align=\"right\" background=\"background\" id=\"menu" + nodeFir.ToString() + nodeSec.ToString() + "0\" style=\"DISPLAY: none\"><table cellspacing=\"0\" cellpadding=\"0\" width=\"90%\" border=\"0\"><tbody>");
                        }

                        if (nodeFirSonCount == nodeSec && sonCount == 0) sHtml.Append("</tbody></table></div></td></tr>");
                        nodeThr = 0;
                    }
                    // 三级节点
                    if (FuncCode.Length == 6)
                    {
                        nodeThr++;
                        sHtml.Append("<tr><td height=\"23\" align=\"left\" class=\"lnav02\"><img height=\"3\" src=\"images/icon03.gif\" width=\"5\" align=\"absmiddle\" /> <a title=\"" + FuncName + "\" href=\"" + FuncURL + "\" target=\"" + linkTarget + "\"> " + FuncName + "</a> </td></tr>");
                        if (nodeSecSonCount == nodeThr)
                        {
                            sHtml.Append("</tbody></table></td></tr>");
                            if (nodeFirSonCount == nodeSec) sHtml.Append("</tbody></table></td></tr>");
                        }
                    }*/
                }
                if (m_MenuType == "00") { sHtml.Append("</tbody></table></td></tr>"); }
                sHtml.Append("</tbody></table>");
                sHtml.Append("<!--------------------------------------------------->\r\n");
            }

            m_TreeDt = null;

            this.LiteralSysMenu.Text = sHtml.ToString();
        }

        /// <summary>
        /// 获取根节点数量
        /// </summary>
        /// <param name="userPowers"></param>
        private void GetRootMenuCount(string userPowers)
        {
            string[] aryMenus = userPowers.Split(',');
            int menuNum = 0;
            for (int i = 0; i < aryMenus.Length; i++)
            {
                if (aryMenus[i].Length == 4) menuNum++;
            }

            m_RootMenuNum = menuNum.ToString();

        }
    }
}
