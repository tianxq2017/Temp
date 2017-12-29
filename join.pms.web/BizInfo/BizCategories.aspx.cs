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
using System.Globalization;
using System.IO;
using System.Text;
using System.Data.SqlClient;

using join.pms.dal;
using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.web.BizInfo
{
    public partial class BizCategories : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_SqlParams;
        protected string m_TargetUrl;

        private string m_NavTitle;

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
                    SetOpratetionAction(m_NavTitle);
                }
            }
        }
        #region

        /// <summary>
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);
            if (!string.IsNullOrEmpty(m_ActionName) && m_ActionName == "view" && string.IsNullOrEmpty(m_ObjID))
            {
                m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
            }

            if (!String.IsNullOrEmpty(m_SourceUrl))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");

                m_NavTitle = "ҵ��������Ϣ";
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
                    funcName = "����";
                    break;
                case "edit": // �༭
                    funcName = "�޸�";
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // ɾ��
                    funcName = "";
                    DelInfo(m_ObjID);
                    break;
                case "audit": // ���
                    funcName = "";
                    Audit(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx.aspx\">��ʼҳ</a> &gt;&gt;�������� &gt;&gt;" + oprateName + " ��";
        }
        //Attribs: 0Ĭ������;1����
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="objID"></param>
        private void Audit(string objID)
        {
            if (PageValidate.IsNumber(objID))
            {
                try
                {
                    //Attribs: 0Ĭ������;1����
                    string cmsAttrib = DbHelperSQL.GetSingle("SELECT Attribs FROM BIZ_Categories WHERE BizCode='" + objID + "'").ToString();
                    if (!string.IsNullOrEmpty(cmsAttrib))
                    {
                        if (cmsAttrib == "0") { m_SqlParams = "UPDATE BIZ_Categories SET Attribs=1 WHERE BizCode='" + objID + "'"; }
                        else if (cmsAttrib == "1") { m_SqlParams = "UPDATE BIZ_Categories SET Attribs=0 WHERE BizCode='" + objID + "'"; }
                        else
                        {
                            MessageBox.ShowAndRedirect(this.Page, "������ʾ���޷���ɲ�����", m_TargetUrl, true);
                            return;
                        }

                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ����/ȡ�����ز����ɹ���", m_TargetUrl, true);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ���������", m_TargetUrl, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                 MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ����¼�������Զ�ѡ��", m_TargetUrl, true);
            }
        }

        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string bizCode)
        {
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT BizNameAB,BizNameFull,BizStep,BizStepNames,BizTemplates FROM BIZ_Categories WHERE BizCode='" + bizCode + "'";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    //this.txtBizNameAB.Text = sdr["BizNameAB"].ToString();
                    //this.txtBizNameFull.Text = sdr["BizNameFull"].ToString();
                    //this.txtBizStep.Text = sdr["BizStep"].ToString();
                    //this.txtBizStepNames.Text = sdr["BizStepNames"].ToString();
                    this.LiteralBiz.Text = sdr["BizNameFull"].ToString();
                    this.objCKeditor.Text = PageValidate.Decode(sdr["BizTemplates"].ToString());

                   // GetCmsDocs(m_ObjID); // ��ȡ���ݸ���
                }
                sdr.Close(); sdr.Dispose();
            }
            catch
            {
                if (sdr != null) sdr.Close(); sdr.Dispose();
            }
        }
        
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            if (PageValidate.IsNumber(objID))
            {
                try
                {
                    //Attribs: 0Ĭ������;1����
                    if (!string.IsNullOrEmpty(objID))
                    {
                        m_SqlParams = "UPDATE BIZ_Categories SET Attribs=1 WHERE BizCode='" + objID + "'";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ��ɾ���ɹ���", m_TargetUrl, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                 MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ����¼�������Զ�ѡ��", m_TargetUrl, true);
            }
        }
        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="objID"></param>
        private void BatchDelInfo(string objID)
        {
            try
            {
                m_SqlParams = "UPDATE BIZ_Categories SET Attribs=1 WHERE BizCode='" + objID + "'";
                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��ɾ���ɹ���", m_TargetUrl, true);
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
        }

        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            //string BizNameAB = PageValidate.GetTrim(this.txtBizNameAB.Text);
            //string BizNameFull = PageValidate.GetTrim(this.txtBizNameFull.Text);
            //string BizStep = PageValidate.GetTrim(this.txtBizStep.Text);
            //string BizStepNames = PageValidate.GetTrim(this.txtBizStepNames.Text);
            string BizTemplates = PageValidate.Encode(CommBiz.GetTrim(this.objCKeditor.Text));
            if (string.IsNullOrEmpty(BizTemplates)) strErr += "������ҵ�����ݣ�\\n";
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            try
            {
                if (m_ActionName == "add")
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��ҵ������涨ΪԤ�ò�����������ӣ�", m_TargetUrl, true);
                }
                else
                {
                    m_SqlParams = "UPDATE BIZ_Categories SET BizTemplates='" + BizTemplates + "' WHERE BizCode='" + m_ObjID + "'";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��ҵ������༭�ɹ���", m_TargetUrl, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }

        }

    }
}

