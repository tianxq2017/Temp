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

using System.IO;
using System.Text;
using System.Data.SqlClient;
using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace AreWeb.CertificateInOne.PopInfo
{
    public partial class editMarriage : System.Web.UI.Page
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

        #region
        /// <summary>
        /// �����֤
        /// </summary>
        private void AuthenticateUser()
        {
            bool returnVa = false;
            if (Request.Browser.Cookies){
                HttpCookie loginCookie = Request.Cookies["AKS_PISS_USER_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else{
                if (Session["AKS_PISS_USERID"] != null && !String.IsNullOrEmpty(Session["AKS_PISS_USERID"].ToString())) { returnVa = true; m_UserID = Session["AKS_PISS_USERID"].ToString(); }
            }

            if (!returnVa){
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
            
            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName)){
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                m_NavTitle = "�����Ǽ���Ϣ";
            }
            else{
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = Request.QueryString["RID"];
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
                    SetReportArea("");
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
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; ������ &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }
        /// <summary>
        /// �������ݵ�λ/����������λ
        /// </summary>
        /// <param name="areaCode">Ĭ��ֵ</param>
        private void SetReportArea(string areaCode)
        {

            string siteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
            m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + siteArea + "' ORDER BY AreaCode";
            DataTable tmpDt = new DataTable();
            tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            DDLReportArea.DataSource = tmpDt;
            DDLReportArea.DataTextField = "AreaName";
            DDLReportArea.DataValueField = "AreaCode";
            DDLReportArea.DataBind();
            tmpDt = null;
            if (!string.IsNullOrEmpty(areaCode))
            {
                this.DDLReportArea.SelectedValue = areaCode;
            }
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
                m_SqlParams = "SELECT * FROM [PIS_BaseInfo] WHERE CommID=" + m_ObjID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    if (sdr["AuditFlag"].ToString() != "0" && m_ActionName == "edit")
                    {
                        isEdit = false;
                        break;
                    }
                    this.txtReportDate.Value = DateTime.Parse(sdr["ReportDate"].ToString()).ToString("yyyy-MM-dd");
                    SetReportArea(PageValidate.GetTrim(sdr["AreaCode"].ToString()));

                    this.DDLFileds01.Text = PageValidate.GetTrim(sdr["Fileds01"].ToString());
                    this.txtFileds02.Text = PageValidate.GetTrim(sdr["Fileds02"].ToString());
                    this.txtFileds03.Value = PageValidate.GetTrim(sdr["Fileds03"].ToString());

                    this.txtFileds04.Text = PageValidate.GetTrim(sdr["Fileds04"].ToString());
                    this.txtFileds05.Text = PageValidate.GetTrim(sdr["Fileds05"].ToString());
                    this.DDLFileds06.Text = PageValidate.GetTrim(sdr["Fileds06"].ToString());
                    this.UcAreaSel07.SetAreaName(PageValidate.GetTrim(sdr["Fileds07"].ToString()));
                    this.DDLFileds08.Text = PageValidate.GetTrim(sdr["Fileds08"].ToString());
                    this.DDLFileds09.Text = PageValidate.GetTrim(sdr["Fileds09"].ToString());
                    this.DDLFileds10.Text = PageValidate.GetTrim(sdr["Fileds10"].ToString());
                    this.txtFileds11.Value = PageValidate.GetTrim(sdr["Fileds11"].ToString());

                    this.txtFileds12.Text = PageValidate.GetTrim(sdr["Fileds12"].ToString());
                    this.txtFileds13.Text = PageValidate.GetTrim(sdr["Fileds13"].ToString());
                    this.DDLFileds14.Text = PageValidate.GetTrim(sdr["Fileds14"].ToString());
                    this.UcAreaSel15.SetAreaName(PageValidate.GetTrim(sdr["Fileds15"].ToString()));
                    this.DDLFileds16.Text = PageValidate.GetTrim(sdr["Fileds16"].ToString());
                    this.DDLFileds17.Text = PageValidate.GetTrim(sdr["Fileds17"].ToString());
                    this.DDLFileds18.Text = PageValidate.GetTrim(sdr["Fileds18"].ToString());
                    this.txtFileds19.Value = PageValidate.GetTrim(sdr["Fileds19"].ToString());

                    /*
ҵ������,֤������,�Ǽ�ʱ��,
Fileds01,Fileds02,Fileds03,
�з�����,���֤��,ְҵ,��ͥ��ַ,����,�Ļ��̶�,����״��,��������,
Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,
Ů������,���֤��,ְҵ,��ͥ��ַ,����,�Ļ��̶�,����״��,������λ
Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19
             */
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
                if (AreWeb.CertificateInOne.Biz.CommPage.CheckDelFlag(objID))
                {
                    m_SqlParams = "DELETE FROM PIS_BaseInfo WHERE CommID IN(" + objID + ")";
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
                if (AreWeb.CertificateInOne.Biz.CommPage.CheckAuditFlag(objID, ref cmsAttrib))
                {
                    m_SqlParams = "UPDATE PIS_BaseInfo SET AuditFlag=" + cmsAttrib + " WHERE CommID IN(" + objID + ") ";
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
                if (AreWeb.CertificateInOne.Biz.CommPage.CheckPubFlag(objID, ref cmsAttrib))
                {
                    m_SqlParams = "UPDATE PIS_BaseInfo SET AuditFlag=" + cmsAttrib + " WHERE CommID IN(" + objID + ") ";
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

        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string ReportDate = PageValidate.GetTrim(this.txtReportDate.Value);
            string Fileds01 = PageValidate.GetTrim(this.DDLFileds01.SelectedValue);
            string Fileds02 = PageValidate.GetTrim(this.txtFileds02.Text);
            string Fileds03 = PageValidate.GetTrim(this.txtFileds03.Value);

            string Fileds04 = PageValidate.GetTrim(this.txtFileds04.Text);
            string Fileds05 = PageValidate.GetTrim(this.txtFileds05.Text);
            string Fileds06 = PageValidate.GetTrim(this.DDLFileds06.Text);
            string Fileds07 = PageValidate.GetTrim(this.UcAreaSel07.GetAreaName());
            string Fileds08 = PageValidate.GetTrim(this.DDLFileds08.Text);
            string Fileds09 = PageValidate.GetTrim(this.DDLFileds09.Text);
            string Fileds10 = PageValidate.GetTrim(this.DDLFileds10.Text);
            string Fileds11 = PageValidate.GetTrim(this.txtFileds11.Value);

            string Fileds12 = PageValidate.GetTrim(this.txtFileds12.Text);
            string Fileds13 = PageValidate.GetTrim(this.txtFileds13.Text);
            string Fileds14 = PageValidate.GetTrim(this.DDLFileds14.Text);
            string Fileds15 = PageValidate.GetTrim(this.UcAreaSel15.GetAreaName());
            string Fileds16 = PageValidate.GetTrim(this.DDLFileds16.Text);
            string Fileds17 = PageValidate.GetTrim(this.DDLFileds17.Text);
            string Fileds18 = PageValidate.GetTrim(this.DDLFileds18.Text);
            string Fileds19 = PageValidate.GetTrim(this.txtFileds19.Value);
            /*
             * ҵ������,֤������,�Ǽ�ʱ��,�з�����,���֤��,ְҵ,��ͥ��ַ,����,�Ļ��̶�,����״��,��������,Ů������,���֤��,ְҵ,��ͥ��ַ,����,�Ļ��̶�,����״��,��������
             * Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19
             ҵ������,֤������,�Ǽ�ʱ��,
Fileds01,Fileds02,Fileds03,
�з�����,���֤��,ְҵ,��ͥ��ַ,����,�Ļ��̶�,����״��,��������
Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,
Ů������,���֤��,ְҵ,��ͥ��ַ,����,�Ļ��̶�,����״��,��������
Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19
             */
            string AreaCode = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Value);
            string AreaName = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Text);
            if (string.IsNullOrEmpty(AreaCode))
            {
                strErr += "��ѡ�����ݹ���������������\\n";
            }
            if (String.IsNullOrEmpty(Fileds01))
            {
                strErr += "��ѡ��ҵ�����ͣ�\\n";
            }
            if (String.IsNullOrEmpty(Fileds02))
            {
                strErr += "������֤�����룡\\n";
            }
            if (String.IsNullOrEmpty(Fileds04))
            {
                strErr += "�������з�������\\n";
            }
            if (String.IsNullOrEmpty(Fileds05))
            {
                strErr += "�������з����֤���룡\\n";
            }
            if (!ValidIDCard.VerifyIDCard(Fileds05))
            {
                strErr += "�з����֤�������󣡣�\\n";
            }
            if (String.IsNullOrEmpty(Fileds12))
            {
                strErr += "������Ů��������\\n";
            }
            if (String.IsNullOrEmpty(Fileds13))
            {
                strErr += "������Ů�����֤���룡\\n";
            }
            if (!ValidIDCard.VerifyIDCard(Fileds13))
            {
                strErr += "Ů�����֤�������󣡣�\\n";
            }
            if (String.IsNullOrEmpty(Fileds07))
            {
                strErr += "��ѡ��������з���ϸ��ַ��\\n";
            }
            if (String.IsNullOrEmpty(Fileds15))
            {
                strErr += "��ѡ�������Ů����ϸ��ַ��\\n";
            }
            if (String.IsNullOrEmpty(Fileds03))
            {
                strErr += "��ѡ��Ǽ����ڣ�\\n";
            }
            if (String.IsNullOrEmpty(ReportDate))
            {
                strErr += "��ѡ���������ڣ�\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            // CommID,OprateUserID,OprateDate,ReportDate,FuncNo,UnitNo,
            if (m_ActionName == "add")
            {
                m_SqlParams = "INSERT INTO [PIS_BaseInfo](";
                m_SqlParams += "OprateUserID,FuncNo,";
                m_SqlParams += "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,";
                m_SqlParams += "Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19,ReportDate,AreaCode,AreaName";
                m_SqlParams += ") VALUES(";
                m_SqlParams += "" + m_UserID + ",'" + m_FuncCode + "',";
                m_SqlParams += "'" + Fileds01 + "','" + Fileds02 + "','" + Fileds03 + "','" + Fileds04 + "','" + Fileds05 + "','" + Fileds06 + "','" + Fileds07 + "','" + Fileds08 + "','" + Fileds09 + "','" + Fileds10 + "',";
                m_SqlParams += "'" + Fileds11 + "','" + Fileds12 + "','" + Fileds13 + "','" + Fileds14 + "','" + Fileds15 + "','" + Fileds16 + "','" + Fileds17 + "','" + Fileds18 + "','" + Fileds19 + "','" + ReportDate + "','" + AreaCode + "','" + AreaName + "'";
                m_SqlParams += ")";
            }
            else if (m_ActionName == "edit")
            {
                m_SqlParams = "UPDATE PIS_BaseInfo SET ";
                m_SqlParams += "Fileds01 ='" + Fileds01 + "',Fileds02 ='" + Fileds02 + "',Fileds03 ='" + Fileds03 + "',Fileds04 ='" + Fileds04 + "',Fileds05 ='" + Fileds05 + "',";
                m_SqlParams += "Fileds06 ='" + Fileds06 + "',Fileds07 ='" + Fileds07 + "',Fileds08 ='" + Fileds08 + "',Fileds09 ='" + Fileds09 + "',Fileds10 ='" + Fileds10 + "',";
                m_SqlParams += "Fileds11 ='" + Fileds11 + "',Fileds12 ='" + Fileds12 + "',Fileds13 ='" + Fileds13 + "',Fileds14 ='" + Fileds14 + "',Fileds15 ='" + Fileds15 + "',";
                m_SqlParams += "Fileds16 ='" + Fileds16 + "',Fileds17 ='" + Fileds17 + "',Fileds18 ='" + Fileds18 + "',Fileds19 ='" + Fileds19 + "',ReportDate ='" + ReportDate + "',AreaCode ='" + AreaCode + "',AreaName ='" + AreaName + "' ";
                m_SqlParams += " WHERE CommID=" + m_ObjID;
            }

            try
            {
                if (m_ActionName == "edit")
                {
                    // �����޸ĵ��ֶΡ���¼�޸���־
                    string[] updateVal = { Fileds01, Fileds02, Fileds03, Fileds04, Fileds05, Fileds06, Fileds07, Fileds08, Fileds09, Fileds10, Fileds11, Fileds12, Fileds13, Fileds14, Fileds15, Fileds16, Fileds17, Fileds18, Fileds19 };
                    string configFile = Server.MapPath("/includes/DataGrid.config");
                    AreWeb.CertificateInOne.Biz.CommPage cp = new AreWeb.CertificateInOne.Biz.CommPage();
                    string returnVal = cp.AnalysisFields(m_FuncCode, m_ObjID, configFile, updateVal);
                    cp = null;
                    if (!string.IsNullOrEmpty(returnVal))
                    {
                        string opContents = "�û�ID[" + m_UserID + "]�� " + DateTime.Now.ToString() + " �޸����������µ�<�����Ǽ���Ϣ>��" + returnVal;
                        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                        list.Add("UPDATE PIS_BaseInfo SET AuditFlag=2 WHERE CommID=" + m_ObjID);
                        list.Add("INSERT INTO [SYS_Log]([OprateUserID], [OprateContents], [OprateModel], [OprateUserIP]) VALUES(" + m_UserID + ", '" + opContents + "', '�����޸�', '" + Request.UserHostAddress + "')");
                        DbHelperSQL.ExecuteSqlTran(list);
                        list = null;
                    }
                }
                DbHelperSQL.ExecuteSql(m_SqlParams);
                Response.Write(" <script>alert('�����Ǽ���Ϣ�����ɹ���') ;window.location.href='" + m_TargetUrl + "'</script>");
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

