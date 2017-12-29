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

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_SqlParams;
        private DataTable m_Dt;
        protected string m_TargetUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetOpratetionAction("ϵͳ��¼������");
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
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/loginTemp.aspx';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// ���ò�����Ϊ
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("�Ƿ����ʣ���������ֹ��");
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
                case "add": // ����
                    funcName = "��������";
                    ShowAddInfo();
                    break;
                case "edit": // �༭
                    funcName = "�����޸�";
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // ɾ��
                    funcName = "ɾ������";
                    DelInfo(m_ObjID);
                    break;
                case "initpwd": // ��������
                    funcName = "��������";
                    ResetUserPwd(m_ObjID);
                    break;
                case "audit": // ���
                    funcName = "�������";
                    AuditUser(m_ObjID);
                    break;
                case "freez": // ����
                    funcName = "����";
                    SetUserFreez(m_ObjID);
                    break;
                case "reset": // �ⶳ
                    funcName = "�ⶳ";
                    SetUserFreezReset(m_ObjID);
                    break;
                case "auditshop": // �ⶳauditshop
                    funcName = "ͨ�����";
                    SetUserAuditShop(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }

        private void ShowAddInfo() 
        {
            this.LiteralOrg.Text = CustomerControls.CreateSelCtrl("txtDeptCode", "", "", "", "SELECT DeptCode,(CASE WHEN Len(DeptCode) = 2 THEN ''+[DeptName]  WHEN Len(DeptCode) = 4 THEN '|--'+[DeptName] ELSE '|--+--'+[DeptName] END) As DeptName FROM USER_Department ORDER BY DeptCode");//��֯����
        }
        /// <summary>
        /// �޸�
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
                    // �޸Ľ���
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
        /// ͨ���������
        /// </summary>
        /// <param name="objID"></param>
        private void SetUserAuditShop(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                // �жϸĻ�Ա�Ƿ���Ч
                m_SqlParams = "UPDATE USER_BaseInfo SET ValidFlag=1,UserLevel=5 WHERE UserID =" + objID;
                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                     MessageBox.ShowAndRedirect(this.Page, "������ʾ������ѡ����ʺ��Ѿ������Ϊ���̻�Ա��", m_TargetUrl, true);
                }
            }
            else
            {
                 MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ���û��������Զ�ѡ��", m_TargetUrl, true);
            }
        }
        /// <summary>
        /// �����ʺ�
        /// </summary>
        /// <param name="objID"></param>
        private void SetUserFreez(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                m_SqlParams = "UPDATE USER_BaseInfo SET ValidFlag=2 WHERE UserID =" + objID;
                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                     MessageBox.ShowAndRedirect(this.Page, "������ʾ������ѡ����ʺ��Ѿ������ᣡ", m_TargetUrl, true);
                }
            }
            else
            {
                  MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ���û��������Զ�ѡ��", m_TargetUrl, true);
            }
        }
        /// <summary>
        /// �ⶳ�ʺ�
        /// </summary>
        /// <param name="objID"></param>
        private void SetUserFreezReset(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                m_SqlParams = "UPDATE USER_BaseInfo SET ValidFlag=0 WHERE UserID =" + objID;
                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ������ѡ����ʺ��Ѿ��ɹ��ⶳ������Ҫ��˺���ܵ�¼ϵͳ��", m_TargetUrl, true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ���û��������Զ�ѡ��", m_TargetUrl, true);
            }
        }

        /// <summary>
        /// ����ʺ�
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
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ����ʺ����/ȡ����˲����ɹ���", m_TargetUrl, true);
                }
                catch (Exception ex)
                {
                    //MessageBox.ShowAndRedirect(this, ex.Message, txtUrlParams.Value, true);
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��" + ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                 MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ���û��������Զ�ѡ��", m_TargetUrl, true);
            }


        }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <param name="objID"></param>
        private void ResetUserPwd(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                m_SqlParams = "UPDATE USER_BaseInfo Set UserPassword='76586E8A7D72FD3F' WHERE UserID=" + objID;

                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ���ɹ����û������ʼ��Ϊ��111111��", m_TargetUrl, true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ���û��������Զ�ѡ��", m_TargetUrl, true);
            }

        }
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            try
            {
                Response.Write("<script language='javascript'>alert('��ʾ��Ϣ�����ǵ��ʺŰ�ȫ���˹��ܱ����ã���ʹ�ö��Ṧ�ܲ�����');window.location.href='" + m_TargetUrl + "';</script>");
                return;
                m_SqlParams = "DELETE FROM USER_BaseInfo WHERE [UserID] IN(" + objID + ")";
                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��ɾ���ɹ�", m_TargetUrl, true);
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
                    strErr += "�������¼���ƣ�\\n";
                }
                if (UserAccount.Length < 6 || UserAccount.Length > 16)
                {
                    strErr += "��¼�ʺ�6-16���ַ���\\n";
                }
                if (string.IsNullOrEmpty(UserPassword))
                {
                    strErr += "��¼���벻��Ϊ�գ�\\n";
                }
                if (UserPassword.Length < 6 || UserPassword.Length > 16)
                {
                    strErr += "��¼����6-16���ַ���\\n";
                }

                if (string.IsNullOrEmpty(UserPasswordRe))
                {
                    strErr += "������ȷ�����룡\\n";
                }
                if (UserPassword != UserPasswordRe)
                {
                    strErr += "��������������벻һ�£����������룡\\n";
                }
                
            }
            if (string.IsNullOrEmpty(UserAreaCode)) strErr += "��ѡ�����������\\n";
            if (string.IsNullOrEmpty(UserAreaName)) strErr += "��ѡ�����������������ƣ�\\n";
            //if (string.IsNullOrEmpty(UserMail))
            //{
            //    strErr += "�ʼ���ַ����Ϊ�գ�\\n";
            //}
           
            //if (!PageValidate.IsEmail(UserMail))
            //{
            //    strErr += "����ȷ���ʼ���ַ��\\n";
            //}
            if (string.IsNullOrEmpty(UserName))
            {
                strErr += "���������Ա������\\n";
            }
            if (string.IsNullOrEmpty(UserTel))
            {
                strErr += "��������ϵ�绰��\\n";
            }
            if (string.IsNullOrEmpty(DeptCode))
            {
                strErr += "��ѡ��������ţ�\\n";
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
            MessageBox.ShowAndRedirect(this.Page, "������ʾ�������ɹ�", m_TargetUrl, true);
        }
    }
}
