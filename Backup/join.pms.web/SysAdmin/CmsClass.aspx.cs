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
    public partial class CmsClass : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_SqlParams;
        private string m_TargetUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();
            
            if (!IsPostBack)
            {
                if (String.IsNullOrEmpty(m_SourceUrl) || String.IsNullOrEmpty(m_ActionName))
                {
                    Response.Write("�Ƿ����ʣ���������ֹ��");
                    Response.End();
                }
                else
                {
                    SetOpratetionAction("���ݷ������");
                }
            }
        }

        #region �����֤����������

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
                    funcName = "����";
                    break;
                case "edit": // �༭
                    funcName = "����";
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // ɾ��
                    funcName = "ɾ��";
                    DelInfo(m_ObjID);
                    break;
                case "4": // �鿴
                    funcName = "�鿴����";
                    break;
                case "5": // ���
                    funcName = "�������";
                    break;
                case "6": // �Ƽ�
                    funcName = "�Ƽ�";
                    //recommend(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }
        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            DataTable tempDT = new DataTable();
            m_SqlParams = "SELECT [CmsCID], [CmsCName],[CmsEName] FROM [CMS_Class] WHERE CmsCID=" + m_ObjID;
            try
            {
                tempDT = DbHelperSQL.Query(m_SqlParams).Tables[0];
                this.txtCmsCName.Text = PageValidate.GetTrim(tempDT.Rows[0]["CmsCName"].ToString());
                this.txtCmsEName.Text = PageValidate.GetTrim(tempDT.Rows[0]["CmsEName"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
            m_SqlParams = null;
            tempDT = null;
        }
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            try
            {
                m_SqlParams = "DELETE FROM CMS_Class WHERE [CmsCID] IN(" + objID + ")";
                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��ɾ���ɹ���", m_TargetUrl, true);
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
            string CmsCName = PageValidate.GetTrim(this.txtCmsCName.Text);
            string CmsEName = PageValidate.GetTrim(this.txtCmsEName.Text);


            if (String.IsNullOrEmpty(CmsCName))
            {
                strErr += "��������/���Ĳ���Ϊ�գ�\\n";
            }
            if (String.IsNullOrEmpty(CmsEName))
            {
                strErr += "��������/Ӣ�Ĳ���Ϊ�գ�\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            if (m_ActionName == "add")
            {
                m_SqlParams = "INSERT INTO [CMS_Class]([CmsCName],[CmsEName]) VALUES( '" + CmsCName + "','" + CmsEName + "')";
                try { DbHelperSQL.ExecuteSql(m_SqlParams); }
                catch (Exception ex) { MessageBox.Show(this, ex.Message); }
            }
            else if (m_ActionName == "edit")
            {
                m_SqlParams = "UPDATE CMS_Class SET CmsCName='" + CmsCName + "',CmsEName='" + CmsEName + "' WHERE CmsCID=" + m_ObjID;
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
            MessageBox.ShowAndRedirect(this.Page, "������ʾ�������ɹ���", m_TargetUrl, true);
        }
    }
}

