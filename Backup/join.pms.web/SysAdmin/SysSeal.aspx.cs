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
    public partial class SysSeal : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_SqlParams;
        private DataTable m_Dt;

        protected string m_TargetUrl;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetOpratetionAction("���¹���");
            }
        }

        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Default.shtml?action=closewindow';</script>");
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
            if (!String.IsNullOrEmpty(m_FuncCode.Substring(0, 2)))
            {
                switch (m_ActionName)
                {
                    case "add": // ����
                        funcName = "�½�����";
                        break;
                    case "edit": // �༭
                        funcName = "�޸Ĺ�����Ϣ";
                        ShowModify();
                        break;
                    case "del": // ɾ��
                        funcName = "ɾ���û�";
                        DelInfo(m_ObjID);
                        break;
                    case "initpwd": // ��������
                        funcName = "��������";
                        ResetUserPwd(m_ObjID);
                        break;
                    case "4": // ���
                        funcName = "���";
                        break;
                    case "5": // ��������
                        funcName = "��������";
                        break;
                    case "6": // �����ɫ
                        funcName = "���û������ɫ";
                        break;
                    default:
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                        break;
                }
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }

        /// <summary>
        /// ��ȡ
        /// </summary>
        private void ShowModify()
        {
            if (!String.IsNullOrEmpty(m_ObjID) && PageValidate.IsNumber(m_ObjID))
            {
                m_Dt = new DataTable();
                try
                {
                    m_SqlParams = "SELECT * FROM [SYS_Seal] WHERE SealID=" + m_ObjID;
                    m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                    if (m_Dt.Rows.Count == 1)
                    {
                        this.txtSealName.Text = m_Dt.Rows[0]["SealName"].ToString();
                        this.txtSealPath.Text = m_Dt.Rows[0]["SealPath"].ToString();
                        //this.txtSealPass.Text = m_Dt.Rows[0]["SealPass"].ToString();
                        this.txtUserPwd.Value = m_Dt.Rows[0]["SealPass"].ToString();

                        this.txtUserID.Value = m_Dt.Rows[0]["SealUserID"].ToString();
                        this.txtUserName.Value = m_Dt.Rows[0]["SealUserName"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��" + ex.Message, m_TargetUrl, true);
                }
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
                m_SqlParams = "DELETE FROM SYS_Seal WHERE SealID IN(" + objID + ")";
                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "������ʾ�����û����ɹ�ɾ����", m_TargetUrl, true);
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
            Response.End();
        } 
        /// <summary>
        /// ����ǩ������
        /// </summary>
        /// <param name="objID"></param>
        private void ResetUserPwd(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                m_SqlParams = "UPDATE SYS_Seal Set SealPass='76586E8A7D72FD3F' WHERE SealID=" + objID;

                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ���ɹ����û������ʼ��Ϊ��111111����", m_TargetUrl, true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ���û��������Զ�ѡ��", m_TargetUrl, true);
            }

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                string strErr = string.Empty;
                string SealName = PageValidate.GetTrim(this.txtSealName.Text);
                string SealPath = PageValidate.GetTrim(this.txtSealPath.Text);
                string SealPass = PageValidate.GetTrim(this.txtSealPass.Text);
                string SealUserID = PageValidate.GetTrim(this.txtUserID.Value);
                string SealUserName = PageValidate.GetTrim(this.txtUserName.Value);

                if (string.IsNullOrEmpty(SealName))
                {
                    strErr += "�������Ʋ���Ϊ�գ�\\n";
                }
                if (string.IsNullOrEmpty(SealPath))
                {
                    strErr += "·������Ϊ�գ�\\n";
                }
                if (string.IsNullOrEmpty(SealPass))
                {
                    strErr += "ǩ�����벻��Ϊ�գ�\\n";
                }
                if (string.IsNullOrEmpty(SealUserID))
                {
                    strErr += "��ѡ�����չ��µĶ���\\n";
                }
                if (!PageValidate.IsNumber(SealUserID))
                {
                    strErr += "ÿ������ֻ�ܷ���һ���ˣ�\\n";
                }

                if (strErr != "")
                {
                    MessageBox.Show(this, strErr);
                    return;
                }
                /*
                 A.SealID, A.SealName, A.SealPath, 
      A.SealPass,A.SealUserID,A.SealUserName,A.SealAttrib,A.CreateDate, 
                 * 
                 */
                SealPass = DESEncrypt.Encrypt(SealPass);
                if (m_ActionName == "add")
                {
                    m_SqlParams = "SELECT COUNT(*) FROM SYS_Seal WHERE SealUserID=" + SealUserID;
                    if (DbHelperSQL.GetSingle(m_SqlParams).ToString() != "0")
                    {                        
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ�����û��Ѿ����乫�£�һ���û�ֻ��ʹ��һ�����£�", m_TargetUrl, true);
                    }
                    else
                    {
                        if (SealPass != this.txtUserPwd.Value)
                        {
                            SealPass = DESEncrypt.Encrypt(SealPass);
                        }
                        m_SqlParams = "INSERT INTO [SYS_Seal](SealName,SealPath,SealPass,SealUserID,SealUserName) VALUES('" + SealName + "','" + SealPath + "','" + SealPass + "'," + SealUserID + ",'" + SealUserName + "')";
                    }
                }
                else if (m_ActionName == "edit")
                {
                    m_SqlParams = "UPDATE [SYS_Seal] SET [SealName]='" + SealName + "',[SealPath]='" + SealPath + "', [SealPass]='" + SealPass + "', SealUserID=" + SealUserID + ",SealUserName='" + SealUserName + "'";
                    m_SqlParams += " WHERE SealID=" + m_ObjID;
                }
                else { 
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��������ʧ�������²�����", m_TargetUrl, true);
            }
                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "������ʾ�������ɹ���", m_TargetUrl, true);
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��" + ex.Message, m_TargetUrl, true);
            }
        }

    }
}

