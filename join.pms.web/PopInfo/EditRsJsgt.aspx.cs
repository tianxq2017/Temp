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

using AreWeb.CertificateInOne.Biz;
using UNV.Comm.DataBase;
using UNV.Comm.Web;
using System.Data.SqlClient;

namespace AreWeb.CertificateInOne.PopInfo
{
    public partial class EditRsJsgt : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                SetOpratetionAction(m_NavTitle);
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
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        #region �����֤����������
        /// <summary>
        /// �����֤
        /// </summary>
        private void AuthenticateUser()
        {
            bool returnVa = false;
            if (Request.Browser.Cookies)
            {
                HttpCookie loginCookie = Request.Cookies["AKS_PISS_USER_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["AKS_PISS_USERID"] != null && !String.IsNullOrEmpty(Session["AKS_PISS_USERID"].ToString())) { returnVa = true; m_UserID = Session["AKS_PISS_USERID"].ToString(); }
            }

            if (!returnVa)
            {
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_ActionName = Request.QueryString["action"];
            m_SourceUrl = Request.QueryString["sourceUrl"];
            m_ObjID = Request.QueryString["k"];

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                m_NavTitle = "������ҵ��λ��ְ��ͬ�ƹ���(�ɲ�)������Ϣ";
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = Request.QueryString["RID"];
        }
        #endregion

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
                    //this.LiteralCmsClass.Text = CustomerControls.CreateSelCtrl("txtCmsClass", "", "", "", "SELECT [CmsCID], [CmsCName] FROM [CMS_Class]");
                    break;
                case "edit": // �༭
                    funcName = "�༭";
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // ɾ��
                    funcName = "ɾ��";
                    DelInfo(m_ObjID);
                    break;
                case "view": // �鿴
                    funcName = "�鿴����";
                    ShowModInfo(m_ObjID);
                    break;
                case "audit": // ���
                    funcName = "�������";
                    AuditInfo(m_ObjID);
                    break;
                case "pub": // ����
                    funcName = "����";
                    PubInfo(m_ObjID);
                    break;
                default:
                    Response.Write(" <script>alert('����ʧ�ܣ���������') ;window.location.href='" + m_TargetUrl + "'</script>");
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">��ʼҳ</a> &gt;&gt; ������ҵ��λ������ϱ��չ���칫�� &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }

        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            bool isEdit = true;
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT * FROM PIS_BASEINFO WHERE COMMID=" + m_ObjID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    if (sdr["AUDITFLAG"].ToString() != "0" && m_ActionName == "edit")
                    {
                        isEdit = false;
                        break;
                    }
                    this.txtReportDate.Value = DateTime.Parse(sdr["REPORTDATE"].ToString()).ToString("yyyy-MM-dd");
                    // FILEDS01
                    this.txtFileds01.Text = PageValidate.GetTrim(sdr["FILEDS01"].ToString());
                    this.txtFileds02.Text = PageValidate.GetTrim(sdr["FILEDS02"].ToString());
                    this.txtFileds03.Text = PageValidate.GetTrim(sdr["FILEDS03"].ToString());
                    this.txtFileds04.Text = PageValidate.GetTrim(sdr["FILEDS04"].ToString());

                    if (m_ActionName == "view") this.btnAdd.Visible = false;

                }
                sdr.Close();
                // �Ƿ�ɱ༭
                if (!isEdit)
                {
                    Response.Write(" <script>alert('����ʧ�ܣ���ѡ�����Ϣ����ͨ����ˡ������򹫿�����Ϣ������Ϣ������༭��') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
            }
            catch { if (sdr != null) sdr.Close(); }
        }

        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            try
            {
                if (CommPage.CheckDelFlag(objID))
                {
                    m_SqlParams = "DELETE FROM PIS_BASEINFO WHERE COMMID IN(" + objID + ")";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    Response.Write(" <script>alert('�����ɹ�����ѡ�����Ϣɾ���ɹ���') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
                else
                {
                    Response.Write(" <script>alert('����ʧ�ܣ���ѡ�����Ϣ����ͨ����˵���Ϣ����˹�������Ϣ������ɾ����') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
        }

        /// <summary>
        /// �����Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void AuditInfo(string objID)
        {
            try
            {
                string cmsAttrib = string.Empty;
                if (CommPage.CheckAuditFlag(objID, ref cmsAttrib))
                {
                    m_SqlParams = "UPDATE PIS_BASEINFO SET AUDITFLAG=" + cmsAttrib + " WHERE COMMID IN(" + objID + ") ";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    Response.Write(" <script>alert('�����ɹ�����ѡ�����Ϣ���/ȡ����˲����ɹ���') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
                else
                {
                    Response.Write(" <script>alert('����ʧ�ܣ���ѡ�����Ϣ������δͨ����˻��߲���ͬһ����Ϣ��') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void PubInfo(string objID)
        {
            try
            {
                // CmsAttrib:0 Ĭ��;1 ���; 3 ����; 4 ɾ��; 9 ����
                string cmsAttrib = string.Empty;
                if (CommPage.CheckPubFlag(objID, ref cmsAttrib))
                {
                    m_SqlParams = "UPDATE PIS_BASEINFO SET AUDITFLAG=" + cmsAttrib + " WHERE COMMID IN(" + objID + ") ";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    Response.Write(" <script>alert('�����ɹ�����ѡ�����Ϣ���/ȡ����˲����ɹ���') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
                else
                {
                    Response.Write(" <script>alert('����ʧ�ܣ���ѡ�����Ϣ������δͨ����˻��߲���ͬһ����Ϣ��') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string ReportDate = PageValidate.GetTrim(this.txtReportDate.Value);
            string Fileds01 = PageValidate.GetTrim(this.txtFileds01.Text);
            string Fileds02 = PageValidate.GetTrim(this.txtFileds02.Text);
            string Fileds03 = PageValidate.GetTrim(this.txtFileds03.Text);
            string Fileds04 = PageValidate.GetTrim(this.txtFileds04.Text);
            /*
             * ����,�Ա�,��ҵԺУ,��ҵʱ��,ѧ�����,���֤����,��ϵ��ʽ,��ͥסַ,��ҵ״��
             * Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09
             */
            if (String.IsNullOrEmpty(ReportDate))
            {
                strErr += "��ѡ���������ڣ�\\n";
            }
            if (String.IsNullOrEmpty(Fileds01))
            {
                strErr += "�����뵥λ��\\n";
            }
            if (String.IsNullOrEmpty(Fileds02))
            {
                strErr += "�������ţ�\\n";
            }
            if (String.IsNullOrEmpty(Fileds03))
            {
                strErr += "������������\\n";
            }
            if (String.IsNullOrEmpty(Fileds04))
            {
                strErr += "������������\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            // COMMID,OprateUserID,OprateDate,ReportDate,FuncNo,UnitNo,
            if (m_ActionName == "add")
            {
                m_SqlParams = "INSERT INTO \"PIS_BASEINFO\"(";
                m_SqlParams += "\"OPRATEUSERID\",\"FUNCNO\",";
                m_SqlParams += "\"FILEDS01\",\"FILEDS02\",\"FILEDS03\",\"FILEDS04\",\"REPORTDATE\"";
                m_SqlParams += ") VALUES(";
                m_SqlParams += "" + m_UserID + ",'" + m_FuncCode + "',";
                m_SqlParams += "'" + Fileds01 + "','" + Fileds02 + "','" + Fileds03 + "','" + Fileds04 + "','" + ReportDate + "'";
                m_SqlParams += ")";
            }
            else if (m_ActionName == "edit")
            {
                m_SqlParams = "UPDATE \"PIS_BASEINFO\" SET ";
                m_SqlParams += "\"FILEDS01\" ='" + Fileds01 + "',\"FILEDS02\" ='" + Fileds02 + "',\"FILEDS03\" ='" + Fileds03 + "',\"FILEDS04\" ='" + Fileds04 + "',\"REPORTDATE\" ='" + ReportDate + "' ";
                m_SqlParams += " WHERE \"COMMID\"=" + m_ObjID;
            }
            try
            {
                if (m_ActionName == "edit")
                {
                    // �����޸ĵ��ֶΡ���¼�޸���־
                    string[] updateVal = { Fileds01, Fileds02, Fileds03, Fileds04 };
                    string configFile = Server.MapPath("/includes/DataGrid.config");
                    CommPage cp = new CommPage();
                    string returnVal = cp.AnalysisFields(m_FuncCode, m_ObjID, configFile, updateVal);
                    cp = null;
                    if (!string.IsNullOrEmpty(returnVal))
                    {
                        string opContents = "�û�ID[" + m_UserID + "]�� " + DateTime.Now.ToString() + " �޸��˻�����ҵ��λ������ϱ��չ���칫���µ�<������ҵ��λ��ְ��ͬ�ƹ���(�ɲ�)������Ϣ>��" + returnVal;
                        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                        list.Add("UPDATE \"PIS_BASEINFO\" SET \"AUDITFLAG\"=2 WHERE \"COMMID\"=" + m_ObjID);
                        list.Add("INSERT INTO \"SYS_Log\"(\"OprateUserID\", \"OprateContents\", \"OprateModel\", \"OprateUserIP\",\"OprateDate\") VALUES(" + m_UserID + ", '" + opContents + "', '�����޸�', '" + Request.UserHostAddress + "',getdate())");
                        DbHelperSQL.ExecuteSqlTran(list);
                        list = null;
                    }
                }
                DbHelperSQL.ExecuteSql(m_SqlParams);
                Response.Write(" <script>alert('������ҵ��λ��ְ��ͬ�ƹ���(�ɲ�)������Ϣ�����ɹ���') ;window.location.href='" + m_TargetUrl + "'</script>");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                //Response.Write(" <script>alert('����ʧ�ܣ�" + ex.Message + "') ;</script>");
                return;
            }
        }
    }
}

