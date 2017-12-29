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
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;
namespace join.pms.wap.UserCenter
{
    public partial class BizWorkFlows : UNV.Comm.Web.PageBase
    {
        #region �Զ������
        private string m_UserID;
        private string m_PersonCardID;

        private string m_UrlPageNo;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_SqlParams;
        protected string m_TargetUrl;
        protected string m_NavTitle;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            this.Uc_PageTop1.GetSysMenu("ҵ������");
            if (!IsPostBack)
            {
                // �༭ɾ�����ܵķ��ص�ַ               
                if (String.IsNullOrEmpty(m_ActionName))
                {
                    Response.Write("�Ƿ����ʣ���������ֹ��");
                    Response.End();
                }
                else
                {
                    SetOpratetionAction(this.m_NavTitle);
                }
            }
        }
        #region ����ҳͷ��Ϣ������\��֤����\��֤�û���
        /// <summary>
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_ActionName = Request.QueryString["action"];
            m_UrlPageNo = Request.QueryString["p"];
            m_FuncCode = Request.QueryString["oCode"];
            m_NavTitle = Request.QueryString["oNa"];
            m_ObjID = Request.QueryString["k"];
            if (string.IsNullOrEmpty(m_UrlPageNo)) m_UrlPageNo = "1"; // ҳ��Ĭ��Ϊ��һҳ
            if (!string.IsNullOrEmpty(m_ActionName) && m_ActionName == "view" && string.IsNullOrEmpty(m_ObjID))
            {
                m_ObjID = Request.QueryString["RID"];
            }
            if (!string.IsNullOrEmpty(m_FuncCode) && m_FuncCode.Length > 1 && PageValidate.IsNumber(m_FuncCode))
            {
                m_TargetUrl = "/OC/" + m_FuncCode + "-" + m_UrlPageNo + "." + this.m_FileExt;
            }
            else
            {
                m_ObjID = BIZ_Common.GetBizContentsFlowsID(this.m_UserID, this.m_PersonCardID);
                m_TargetUrl = "/UserCenter/BizWorkFlows.aspx?action=view";
            }

        }
        /// <summary>
        /// �����֤ 
        /// </summary>
        private void AuthenticateUser()
        {
            bool returnVa = false;
            if (Request.Browser.Cookies)
            {
                HttpCookie loginCookie = Request.Cookies["AREWEB_OC_PUBSVRS_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["UserID"] != null && !String.IsNullOrEmpty(Session["UserID"].ToString())) { returnVa = true; m_UserID = Session["UserID"].ToString(); }
            }

            if (!returnVa)
            {
                Response.Write("<script language='javascript'>alert(\"������ʾ�����¼�����ԣ�\");parent.location.href='/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh." + m_FileExt + "';</script>");
                Response.End();
            }
            else { m_PersonCardID = BIZ_Common.GetPersonCardID(this.m_UserID); }
        }
        #endregion

        #region ���ò�����Ϊ
        /// <summary>
        /// ���ò�����Ϊ
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID))
            {

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
                case "view":
                    funcName = "�鿴";
                    ShowModInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, false, true);
                    break;
            }
        }
        /// <summary>
        /// �鿴
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            this.LiteralFlows.Text = BIZ_Common.GetBizWorkFlowsWap(m_ObjID, m_UserID, m_PersonCardID);
        }

        #endregion
    }
}


