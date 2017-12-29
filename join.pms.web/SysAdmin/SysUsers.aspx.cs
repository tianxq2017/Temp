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

namespace join.pms.web.SysAdmin
{
    public partial class SysUsers : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号

        private string m_SqlParams;
        private DataTable m_Dt;
        protected string m_TargetUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetOpratetionAction("系统登录名管理");
            }
        }

        #region 

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
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
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
                case "add": // 新增
                    funcName = "新增参数";
                    ShowAddInfo();
                    break;
                case "edit": // 编辑
                    funcName = "参数修改";
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // 删除
                    funcName = "删除内容";
                    DelInfo(m_ObjID);
                    break;
                case "initpwd": // 重置密码
                    funcName = "重置密码";
                    ResetUserPwd(m_ObjID);
                    break;
                case "audit": // 审核
                    funcName = "审核内容";
                    AuditUser(m_ObjID);
                    break;
                case "freez": // 冻结
                    funcName = "冻结";
                    SetUserFreez(m_ObjID);
                    break;
                case "reset": // 解冻
                    funcName = "解冻";
                    SetUserFreezReset(m_ObjID);
                    break;
                case "auditshop": // 解冻auditshop
                    funcName = "通过审核";
                    SetUserAuditShop(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }

        private void ShowAddInfo() 
        {
            this.LiteralOrg.Text = CustomerControls.CreateSelCtrl("txtDeptCode", "", "", "", "SELECT DeptCode,(CASE WHEN Len(DeptCode) = 2 THEN ''+[DeptName]  WHEN Len(DeptCode) = 4 THEN '|--'+[DeptName] ELSE '|--+--'+[DeptName] END) As DeptName FROM USER_Department ORDER BY DeptCode");//组织机构
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            // UserAccount,UserPassword,UserQestion,UserKeys,UserName,UserIDCard,UserMail,UserTel,UserAddress,UserJob,UserEdu,UserAreaCode,Birthday,CommMemo
            string areaCode, userJob, userEdu,userDeptID = string.Empty;
            m_SqlParams = "SELECT UserAccount,UserQestion,UserKeys,UserName,UserIDCard,UserMail,UserTel,UserAddress,UserUnitName,UserJob,UserEdu,UserAreaCode,UserAreaName,Birthday,DeptCode,CommMemo FROM USER_BaseInfo WHERE UserID=" + m_ObjID;
            try
            {
                m_Dt = new DataTable();
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (m_Dt.Rows.Count==1) 
                {
                    this.txtUserAccount.Text = PageValidate.GetTrim(m_Dt.Rows[0]["UserAccount"].ToString());
                    this.txtUserName.Text = PageValidate.GetTrim(m_Dt.Rows[0]["UserName"].ToString());
                    //this.txtUserIDCard.Text = PageValidate.GetTrim(m_Dt.Rows[0]["UserIDCard"].ToString());
                    this.txtUserMail.Text = PageValidate.GetTrim(m_Dt.Rows[0]["UserMail"].ToString());
                    this.txtUserTel.Text = PageValidate.GetTrim(m_Dt.Rows[0]["UserTel"].ToString());
                    this.txtUserAddress.Text = PageValidate.GetTrim(m_Dt.Rows[0]["UserAddress"].ToString());
                    this.txtUserUnitName.Text = PageValidate.GetTrim(m_Dt.Rows[0]["UserUnitName"].ToString());
                    // 修改禁用
                    this.txtUserAccount.Enabled = false;
                    this.txtUserPwd.Enabled = false;
                    this.txtUserPwdRe.Enabled = false;

                    areaCode = PageValidate.GetTrim(m_Dt.Rows[0]["UserAreaCode"].ToString());
                    userDeptID = PageValidate.GetTrim(m_Dt.Rows[0]["DeptCode"].ToString());

                    if (string.IsNullOrEmpty(userDeptID) || userDeptID == "0") { this.LiteralOrg.Text = CustomerControls.CreateSelCtrl("txtDeptCode", "", "", "", "SELECT DeptCode,(CASE WHEN Len(DeptCode) = 2 THEN ''+[DeptName]  WHEN Len(DeptCode) = 4 THEN '|--'+[DeptName] ELSE '|--+--'+[DeptName] END) As DeptName FROM USER_Department ORDER BY DeptCode"); }
                    else { this.LiteralOrg.Text = CustomerControls.CreateSelCtrl("txtDeptCode", "", userDeptID, "", "SELECT DeptCode,(CASE WHEN Len(DeptCode) = 2 THEN ''+[DeptName]  WHEN Len(DeptCode) = 4 THEN '|--'+[DeptName] ELSE '|--+--'+[DeptName] END) As DeptName FROM USER_Department ORDER BY DeptCode"); }
                    if (!string.IsNullOrEmpty(areaCode)) { 
                        this.UcAreaSel1.SetAreaCode(areaCode);
                        this.UcAreaSel1.SetAreaName(PageValidate.GetTrim(m_Dt.Rows[0]["UserAreaName"].ToString()));
                    }
                }
                m_Dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
        }
        /// <summary>
        /// 通过商铺审核
        /// </summary>
        /// <param name="objID"></param>
        private void SetUserAuditShop(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                // 判断改会员是否有效
                m_SqlParams = "UPDATE USER_BaseInfo SET ValidFlag=1,UserLevel=5 WHERE UserID =" + objID;
                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                     MessageBox.ShowAndRedirect(this.Page, "操作提示：你所选择的帐号已经被审核为商铺会员！", m_TargetUrl, true);
                }
            }
            else
            {
                 MessageBox.ShowAndRedirect(this.Page, "操作提示：考虑到系统安全，此操作每次只能选择一个用户，不可以多选！", m_TargetUrl, true);
            }
        }
        /// <summary>
        /// 冻结帐号
        /// </summary>
        /// <param name="objID"></param>
        private void SetUserFreez(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                m_SqlParams = "UPDATE USER_BaseInfo SET ValidFlag=2 WHERE UserID =" + objID;
                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                     MessageBox.ShowAndRedirect(this.Page, "操作提示：你所选择的帐号已经被冻结！", m_TargetUrl, true);
                }
            }
            else
            {
                  MessageBox.ShowAndRedirect(this.Page, "操作提示：考虑到系统安全，此操作每次只能选择一个用户，不可以多选！", m_TargetUrl, true);
            }
        }
        /// <summary>
        /// 解冻帐号
        /// </summary>
        /// <param name="objID"></param>
        private void SetUserFreezReset(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                m_SqlParams = "UPDATE USER_BaseInfo SET ValidFlag=0 WHERE UserID =" + objID;
                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：你所选择的帐号已经成功解冻，但需要审核后才能登录系统！", m_TargetUrl, true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：考虑到系统安全，此操作每次只能选择一个用户，不可以多选！", m_TargetUrl, true);
            }
        }

        /// <summary>
        /// 审核帐号
        /// </summary>
        /// <param name="objID"></param>
        private void AuditUser(string objID) 
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                try
                {
                   
                    m_SqlParams = "SELECT ValidFlag FROM USER_BaseInfo WHERE UserID=" + objID;
                    string validFlag = DbHelperSQL.GetSingle(m_SqlParams).ToString();

                    if (validFlag == "0") { 
                        m_SqlParams = "UPDATE USER_BaseInfo Set ValidFlag=1 WHERE UserID=" + objID; 
                    }
                    else { 
                        m_SqlParams = "UPDATE USER_BaseInfo Set ValidFlag=0 WHERE UserID=" + objID; 
                    }

                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的帐号审核/取消审核操作成功！", m_TargetUrl, true);
                }
                catch (Exception ex)
                {
                    //MessageBox.ShowAndRedirect(this, ex.Message, txtUrlParams.Value, true);
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：" + ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                 MessageBox.ShowAndRedirect(this.Page, "操作提示：考虑到系统安全，此操作每次只能选择一个用户，不可以多选！", m_TargetUrl, true);
            }


        }
        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="objID"></param>
        private void ResetUserPwd(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                m_SqlParams = "UPDATE USER_BaseInfo Set UserPassword='76586E8A7D72FD3F' WHERE UserID=" + objID;

                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：成功将用户密码初始化为“111111！", m_TargetUrl, true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：考虑到系统安全，此操作每次只能选择一个用户，不可以多选！", m_TargetUrl, true);
            }

        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            try
            {
                Response.Write("<script language='javascript'>alert('提示信息：考虑到帐号安全，此功能被禁用，请使用冻结功能操作！');window.location.href='" + m_TargetUrl + "';</script>");
                return;
                m_SqlParams = "DELETE FROM USER_BaseInfo WHERE [UserID] IN(" + objID + ")";
                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "操作提示：删除成功", m_TargetUrl, true);
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
            string UserAccount = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserAccount.Text));
            string UserPassword = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserPwd.Text));
            string UserPasswordRe = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserPwdRe.Text));
            string UserName = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserName.Text));
            //string UserIDCard = "";// PageValidate.GetTrim(this.txtUserIDCard.Text);
            string UserMail = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserMail.Text));
            string UserTel = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserTel.Text));
            string UserAddress = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserAddress.Text));
            string UserUnitName = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtUserUnitName.Text));

            string UserAreaCode = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.UcAreaSel1.GetAreaCode()));
            string UserAreaName = PageValidate.GetTrim(this.UcAreaSel1.GetAreaName());
            string DeptCode = PageValidate.GetTrim(Request["txtDeptCode"]);

            if (m_ActionName == "add")
            {
                if (m_ActionName == "add" && string.IsNullOrEmpty(UserAccount))
                {
                    strErr += "请输入登录名称！\\n";
                }
                if (UserAccount.Length < 6 || UserAccount.Length > 16)
                {
                    strErr += "登录帐号6-16个字符！\\n";
                }
                if (string.IsNullOrEmpty(UserPassword))
                {
                    strErr += "登录密码不能为空！\\n";
                }
                if (UserPassword.Length < 6 || UserPassword.Length > 16)
                {
                    strErr += "登录密码6-16个字符！\\n";
                }

                if (string.IsNullOrEmpty(UserPasswordRe))
                {
                    strErr += "请输入确认密码！\\n";
                }
                if (UserPassword != UserPasswordRe)
                {
                    strErr += "您两次输入的密码不一致，请重新输入！\\n";
                }
                
            }
            if (string.IsNullOrEmpty(UserAreaCode)) strErr += "您选择归属区划！\\n";
            if (string.IsNullOrEmpty(UserAreaName)) strErr += "您选择或输入归属区划名称！\\n";
            //if (string.IsNullOrEmpty(UserMail))
            //{
            //    strErr += "邮件地址不能为空！\\n";
            //}
           
            //if (!PageValidate.IsEmail(UserMail))
            //{
            //    strErr += "不正确地邮件地址！\\n";
            //}
            if (string.IsNullOrEmpty(UserName))
            {
                strErr += "请输入操作员姓名！\\n";
            }
            if (string.IsNullOrEmpty(UserTel))
            {
                strErr += "请输入联系电话！\\n";
            }
            if (string.IsNullOrEmpty(DeptCode))
            {
                strErr += "请选择归属部门！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            if (m_ActionName == "add")
            {
                UserPassword = DESEncrypt.Encrypt(UserPassword);
                m_SqlParams = "INSERT INTO [USER_BaseInfo](UserUnitName,DeptCode,[UserAccount], [UserName], [UserPassword], [UserMail], [UserTel], [UserAddress],UserAreaCode,UserAreaName) ";
                m_SqlParams += "VALUES('" + UserUnitName + "','" + DeptCode + "','" + UserAccount + "', '" + UserName + "', '" + UserPassword + "',  '" + UserMail + "',  '" + UserTel + "', '" + UserAddress + "','" + UserAreaCode + "','" + UserAreaName + "')";
                try { DbHelperSQL.ExecuteSql(m_SqlParams); }
                catch (Exception ex) { MessageBox.Show(this, ex.Message); }
            }
            else if (m_ActionName == "edit")
            {
                m_SqlParams = "UPDATE USER_BaseInfo SET UserUnitName='" + UserUnitName + "', DeptCode='" + DeptCode + "',UserName='" + UserName + "',UserAreaCode='" + UserAreaCode + "',UserAreaName='" + UserAreaName + "',UserMail='" + UserMail + "',UserTel='" + UserTel + "',UserAddress='" + UserAddress + "' WHERE UserID=" + m_ObjID;
                try
                {
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                    return;
                }
            }
            MessageBox.ShowAndRedirect(this.Page, "操作提示：操作成功", m_TargetUrl, true);
        }
    }
}
