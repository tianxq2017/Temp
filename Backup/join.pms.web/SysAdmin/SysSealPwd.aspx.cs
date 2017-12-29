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

namespace join.pms.web.SysAdmin
{
    public partial class SysSealPwd : System.Web.UI.Page
    {
        private string m_UserID; // 当前登录的操作用户编号
        private string m_SqlParams;
        private string m_ActionName;
        private string m_sid;
        private string m_stype;
        private string m_stypename;
        private string m_UserDept;//用户部门编码
        private string m_UserDeptArea;//镇办区划
        private string m_RoleID;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            m_ActionName = PageValidate.GetFilterSQL(Request["Action"]);
            m_sid = PageValidate.GetFilterSQL(Request["m_sid"]);
            m_stype = PageValidate.GetFilterSQL(Request["m_stype"]);
            if (PageValidate.IsNumber(m_sid) || PageValidate.IsNumber(m_stype))
            {
                if (m_stype == "1") { m_stypename = "公章"; }
                else if (m_stype == "2") { m_stypename = "签名"; }
                else
                {
                    MessageBox.Show(this, "操作失败：您没有足够的操作权限！");
                    return;
                    Response.End();
                }
            }
            else
            {
                MessageBox.Show(this, "操作失败！");
                return;
                Response.End();
            
            }
            this.LiteralNav.Text = m_stypename+"密码设置：";
            SetPageStyle(m_UserID);

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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

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

        // 修改密码
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string Err = "";
            //string oldPwd = PageValidate.GetTrim(this.txOldPwd.Text);
            string userPwd = PageValidate.GetTrim(this.txtUserPwd.Text);
            string userPwdRe = PageValidate.GetTrim(this.txtUserPwdRe.Text);
            //if (String.IsNullOrEmpty(oldPwd))
            //{
                //Err += "请输入旧密码！\\n";
            //}
            if (String.IsNullOrEmpty(userPwd))
            {
                Err += "新密码不能为空！\\n";
            }
            if (String.Compare(userPwd, userPwdRe) != 0)
            {
                Err += "您的新密码和确认密码不一致，请重新输入！\\n";
            }

            m_UserDept = join.pms.dal.CommPage.GetUnitCodeByUser(m_UserID, ref m_UserDeptArea);
            m_RoleID = DbHelperSQL.GetSingle("SELECT TOP 1 RoleID FROM SYS_UserRoles WHERE UserID=" + m_UserID).ToString();
            //1	系统管理员
            //2	业务管理-旗县
            //3	业务处理-旗县
            //4	业务处理-镇办
            
            string UserAcc_w1 = "",UserAcc_w2 = "";
            if (m_RoleID == "1")
            {
                //UserAccount = UserAccount;
            }
            else if (m_RoleID == "2")
            {
                UserAcc_w1 = " SealPath like '%" + m_UserDeptArea.Substring(0, 6) + "%' And ";
                UserAcc_w2 = " SignPath like '%" + m_UserDeptArea.Substring(0, 6) + "%' And ";
            }
            else if (m_RoleID == "3")
            {
                UserAcc_w1 = " SealPath like '%" + m_UserDeptArea.Substring(0, 6) + "%' And ";
                UserAcc_w2 = " SignPath like '%" + m_UserDeptArea.Substring(0, 6) + "%' And ";
            }
            else if (m_RoleID == "4")
            {
                UserAcc_w1 = " SealPath like '%" + m_UserDeptArea.Substring(0, 9) + "%' And ";
                UserAcc_w2 = " SignPath like '%" + m_UserDeptArea.Substring(0, 9) + "%' And ";
            }
            else
            {
                Err += "操作失败：超时权限！\\n";
            }




            if (Err != "")
            {
                MessageBox.Show(this, Err);
                return;
            }

            try
            {
                //oldPwd = DESEncrypt.Encrypt(oldPwd);
                userPwd = DESEncrypt.Encrypt(userPwd);
                //Err = join.pms.dal.CommPage.GetSingleValue("SELECT SealPass FROM SYS_Seal WHERE SealID=" + m_sid);
                //if (oldPwd == Err)
                //{

                if (m_stype == "1") {
                    m_SqlParams = "UPDATE SYS_Seal SET [SealPass]='" + userPwd + "' WHERE " + UserAcc_w1 + " SealID= " + m_sid; 
                }
                else if (m_stype == "2")
                {
                    m_SqlParams = "UPDATE SYS_Sign SET [SignPass]='" + userPwd + "' WHERE " + UserAcc_w2 + " SignID= " + m_sid; 
                }
                    if (DbHelperSQL.ExecuteSql(m_SqlParams) == 1)
                    {
                        if (!string.IsNullOrEmpty(m_ActionName) && m_ActionName == "Ysl")
                        {
                            Response.Write(" <script>alert('提示信息：您的" + m_stypename + "密码成功修改！') ;window.location.href='/SysAdmin/SysSginInfo.aspx';</script>");
                        }
                        else
                        {
                            Response.Write(" <script>alert('提示信息：您的" + m_stypename + "密码成功修改！') ;window.location.href='/SysAdmin/SysSginInfo.aspx';</script>");
                        }
                    }
                    else
                    {
                        Response.Write(" <script>alert('操作失败：修改密码出错或请联系管理员处理！') ;</script>");
                        MessageBox.Show(this, "操作失败：修改密码出错！");
                    }
                //}
                //else
                //{
                //    Response.Write(" <script>alert('操作提示：操作失败，原有密码输入有误！');window.location.href='/MainDesk.aspx';</script>");
                //}
            }
            catch { MessageBox.Show(this, "操作失败：修改密码出错！"); }
        }
    }
}
