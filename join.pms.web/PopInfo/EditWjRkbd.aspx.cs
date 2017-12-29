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

using join.pms.dal;
using UNV.Comm.DataBase;
using UNV.Comm.Web;
using System.Data.SqlClient;

namespace AreWeb.OnlineCertificate.PopInfo
{
    public partial class EditWjRkbd : System.Web.UI.Page
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
                m_NavTitle = "ȫԱ�˿ڱ䶯������Ϣ";
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
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">��ʼҳ</a> &gt;&gt; ���ƾ� &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }

        #region Action ������Ϊ����

        /// <summary>
        /// �������ݵ�λ/����������λ
        /// </summary>
        /// <param name="areaCode">Ĭ��ֵ</param>
        private void SetReportArea(string areaCode)
        {

            string siteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
            m_SqlParams = "SELECT AREACODE,AREANAME FROM AreaDetailCN WHERE PARENTCODE = '" + siteArea + "' ORDER BY AREACODE";
            DataTable tmpDt = new DataTable();
            tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            DDLReportArea.DataSource = tmpDt;
            DDLReportArea.DataTextField = "AREANAME";
            DDLReportArea.DataValueField = "AREACODE";
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
                m_SqlParams = "SELECT * FROM PIS_QYK WHERE COMMID=" + m_ObjID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    if (sdr["Attribs"].ToString() != "0" && m_ActionName == "edit")
                    {
                        isEdit = false;
                        break;
                    }
                    this.txtReportDate.Value = DateTime.Parse(sdr["OprateDate"].ToString()).ToString("yyyy-MM-dd");
                    SetReportArea(PageValidate.GetTrim(sdr["AREACODE"].ToString()));
                    // FILEDS01
                    this.txtFileds01.Text = PageValidate.GetTrim(sdr["FILEDS01"].ToString());
                    this.txtFileds02.Text = PageValidate.GetTrim(sdr["FILEDS02"].ToString());
                    this.DDLFileds03.Text = PageValidate.GetTrim(sdr["FILEDS03"].ToString());

                    this.txtFileds04.Text = PageValidate.GetTrim(sdr["FILEDS04"].ToString());
                    this.txtFileds05.Text = PageValidate.GetTrim(sdr["FILEDS05"].ToString());
                    this.txtFileds06.Text = PageValidate.GetTrim(sdr["FILEDS06"].ToString());
                    this.txtFileds07.Value = PageValidate.GetTrim(sdr["FILEDS07"].ToString());
                    this.DDLFileds08.Text = PageValidate.GetTrim(sdr["FILEDS08"].ToString());
                    this.txtFileds09.Text = PageValidate.GetTrim(sdr["FILEDS09"].ToString());
                    this.txtFileds10.Text = PageValidate.GetTrim(sdr["FILEDS10"].ToString());

                    this.txtFileds11.Text = PageValidate.GetTrim(sdr["FILEDS11"].ToString());
                    this.txtFileds12.Text = PageValidate.GetTrim(sdr["FILEDS12"].ToString());
                    this.txtFileds13.Text = PageValidate.GetTrim(sdr["FILEDS13"].ToString());
                    this.txtFileds14.Text = PageValidate.GetTrim(sdr["FILEDS14"].ToString());
                    this.txtFileds15.Text = PageValidate.GetTrim(sdr["FILEDS15"].ToString());
                    this.txtFileds16.Text = PageValidate.GetTrim(sdr["FILEDS16"].ToString());
                    this.txtFileds17.Text = PageValidate.GetTrim(sdr["FILEDS17"].ToString());
                    this.txtFileds18.Value = PageValidate.GetTrim(sdr["FILEDS18"].ToString());
                    this.txtFileds19.Text = PageValidate.GetTrim(sdr["FILEDS19"].ToString());

                    if (m_ActionName == "view") this.btnAdd.Visible = false;

                }
                sdr.Close();
                // �Ƿ�ɱ༭
                if (!isEdit)
                {
                    Response.Write(" <script>alert('����ʧ�ܣ���ѡ�����Ϣ����ͨ������������֤��У��ʧ�ܵ���Ϣ������Ϣ������༭��') ;window.location.href='" + m_TargetUrl + "'</script>");
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
                    m_SqlParams = "DELETE FROM PIS_QYK WHERE COMMID IN(" + objID + ")";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    Response.Write(" <script>alert('�����ɹ�����ѡ�����Ϣɾ���ɹ���') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
                else
                {
                    Response.Write(" <script>alert('����ʧ�ܣ���ѡ�����Ϣ����ͨ������������֤��У��ʧ�ܵ���Ϣ������ɾ����') ;window.location.href='" + m_TargetUrl + "'</script>");
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
                    m_SqlParams = "UPDATE PIS_QYK SET Attribs=" + cmsAttrib + " WHERE COMMID IN(" + objID + ") ";
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
                    m_SqlParams = "UPDATE PIS_QYK SET Attribs=" + cmsAttrib + " WHERE COMMID IN(" + objID + ") ";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    Response.Write(" <script>alert('�����ɹ�����ѡ�����Ϣ��������/ȡ���������������ɹ���') ;window.location.href='" + m_TargetUrl + "'</script>");
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
            string Fileds01 = PageValidate.GetTrim(this.txtFileds01.Text);
            string Fileds02 = PageValidate.GetTrim(this.txtFileds02.Text);
            string Fileds03 = PageValidate.GetTrim(this.DDLFileds03.Text);

            string Fileds04 = PageValidate.GetTrim(this.txtFileds04.Text);
            string Fileds05 = PageValidate.GetTrim(this.txtFileds05.Text);
            string Fileds06 = PageValidate.GetTrim(this.txtFileds06.Text);
            string Fileds07 = PageValidate.GetTrim(this.txtFileds07.Value);
            string Fileds08 = PageValidate.GetTrim(this.DDLFileds08.Text);
            string Fileds09 = PageValidate.GetTrim(this.txtFileds09.Text);
            string Fileds10 = PageValidate.GetTrim(this.txtFileds10.Text);

            string Fileds11 = PageValidate.GetTrim(this.txtFileds11.Text);
            string Fileds12 = PageValidate.GetTrim(this.txtFileds12.Text);
            string Fileds13 = PageValidate.GetTrim(this.txtFileds13.Text);
            string Fileds14 = PageValidate.GetTrim(this.txtFileds14.Text);
            string Fileds15 = PageValidate.GetTrim(this.txtFileds15.Text);
            string Fileds16 = PageValidate.GetTrim(this.txtFileds16.Text);
            string Fileds17 = PageValidate.GetTrim(this.txtFileds17.Text);
            string Fileds18 = PageValidate.GetTrim(this.txtFileds18.Value);
            string Fileds19 = PageValidate.GetTrim(this.txtFileds19.Text);
            /*
          ���˱��,����,�Ա�,���֤��,С����������,
Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,
���ƺ�,��������,����,��������,�뻧����ϵ,
Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,
�������ڵ�,�־�ס��,��������,���׹�����ݺ���,ĸ������,
Fileds11,Fileds12,Fileds13,Fileds14,Fileds15
ĸ�׹�����ݺ���,��ס״��,�˳�ϵͳ����,�˳�ϵͳԭ��
Fileds16,Fileds17,Fileds18,Fileds19
             */
            string AREACODE = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Value);
            string AreaName = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Text);
            if (string.IsNullOrEmpty(AREACODE))
            {
                strErr += "��ѡ�����ݹ���������������\\n";
            }
            if (String.IsNullOrEmpty(Fileds01))
            {
                strErr += "�����뻧�˱�ţ�\\n";
            }
            if (String.IsNullOrEmpty(Fileds02))
            {
                strErr += "������������\\n";
            }
            if (String.IsNullOrEmpty(Fileds04))
            {
                strErr += "���������֤�ţ�\\n";
            }
            if (!ValidIDCard.VerifyIDCard(Fileds04))
            {
                strErr += "���֤�����󣡣�\\n";
            }
            if (String.IsNullOrEmpty(Fileds05))
            {
                strErr += "������С��/������\\n";
            }
            if (String.IsNullOrEmpty(Fileds06))
            {
                strErr += "���������ƺţ�\\n";
            }
            if (String.IsNullOrEmpty(Fileds07))
            {
                strErr += "��ѡ��������ڣ�\\n";
            }
            if (String.IsNullOrEmpty(Fileds09))
            {
                strErr += "�������뻧����ϵ��\\n";
            }
            if (String.IsNullOrEmpty(Fileds11))
            {
                strErr += "�����뻧�����ڵأ�\\n";
            }
            if (String.IsNullOrEmpty(Fileds12))
            {
                strErr += "�������־�ס�أ�\\n";
            }
            //if (String.IsNullOrEmpty(Fileds13))
            //{
            //    strErr += "�����븸��������\\n";
            //}
            //if (String.IsNullOrEmpty(Fileds14))
            //{
            //    strErr += "�����븸�����֤�ţ�\\n";
            //}
            //if (!ValidIDCard.VerifyIDCard(Fileds14))
            //{
            //    strErr += "�������֤�����󣡣�\\n";
            //}
            //if (String.IsNullOrEmpty(Fileds15))
            //{
            //    strErr += "������ĸ��������\\n";
            //}
            //if (String.IsNullOrEmpty(Fileds16))
            //{
            //    strErr += "������ĸ�����֤�ţ�\\n";
            //}
            //if (!ValidIDCard.VerifyIDCard(Fileds16))
            //{
            //    strErr += "ĸ�����֤�����󣡣�\\n";
            //} 
            //if (String.IsNullOrEmpty(Fileds17))
            //{
            //    strErr += "�������ס״����\\n";
            //} 
            if (String.IsNullOrEmpty(ReportDate))
            {
                strErr += "��ѡ���������ڣ�\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            // COMMID,OprateUserID,OprateDate,ReportDate,FuncNo,UnitNo,
            if (m_ActionName == "add")
            {
                m_SqlParams = "INSERT INTO PIS_QYK(";
                m_SqlParams += "OPRATEUSERID,FUNCNO,";
                m_SqlParams += "FILEDS01,FILEDS02,FILEDS03,FILEDS04,FILEDS05,FILEDS06,FILEDS07,FILEDS08,FILEDS09,FILEDS10,";
                m_SqlParams += "FILEDS11,FILEDS12,FILEDS13,FILEDS14,FILEDS15,FILEDS16,FILEDS17,FILEDS18,FILEDS19,";
                m_SqlParams += "REPORTDATE,AREACODE,AREANAME";
                m_SqlParams += ") VALUES(";
                m_SqlParams += "" + m_UserID + ",'" + m_FuncCode + "',";
                m_SqlParams += "'" + Fileds01 + "','" + Fileds02 + "','" + Fileds03 + "','" + Fileds04 + "','" + Fileds05 + "','" + Fileds06 + "','" + Fileds07 + "','" + Fileds08 + "','" + Fileds09 + "','" + Fileds10 + "',";
                m_SqlParams += "'" + Fileds11 + "','" + Fileds12 + "','" + Fileds13 + "','" + Fileds14 + "','" + Fileds15 + "','" + Fileds16 + "','" + Fileds17 + "','" + Fileds18 + "','" + Fileds19 + "',";
                m_SqlParams += "'" + ReportDate + "','" + AREACODE + "','" + AreaName + "'";
                m_SqlParams += ")";
            }
            else if (m_ActionName == "edit")
            {
                m_SqlParams = "UPDATE PIS_QYK SET ";
                m_SqlParams += "FILEDS01 ='" + Fileds01 + "',FILEDS02 ='" + Fileds02 + "',FILEDS03 ='" + Fileds03 + "',FILEDS04 ='" + Fileds04 + "',FILEDS05 ='" + Fileds05 + "',";
                m_SqlParams += "FILEDS06 ='" + Fileds06 + "',FILEDS07 ='" + Fileds07 + "',FILEDS08 ='" + Fileds08 + "',FILEDS09 ='" + Fileds09 + "',FILEDS10 ='" + Fileds10 + "',";
                m_SqlParams += "FILEDS11 ='" + Fileds11 + "',FILEDS12 ='" + Fileds12 + "',FILEDS13 ='" + Fileds13 + "',FILEDS14 ='" + Fileds14 + "',FILEDS15 ='" + Fileds15 + "',";
                m_SqlParams += "FILEDS16 ='" + Fileds16 + "',FILEDS17 ='" + Fileds17 + "',FILEDS18 ='" + Fileds18 + "',FILEDS19 ='" + Fileds19 + "',";
                m_SqlParams += "REPORTDATE ='" + ReportDate + "',AREACODE ='" + AREACODE + "',AREANAME ='" + AreaName + "' ";
                m_SqlParams += " WHERE COMMID=" + m_ObjID;
            }
            /*
             COMMID,COMMMEMO,OPRATEUSERID,OPRATEDATE,AREACODE,AREANAME,REPORTDATE,FUNCNO,UNITNO,AUDITUSERID,AUDITFLAG,ANALYSISFLAG,ANALYSISDATE,
FILEDS01,FILEDS02,FILEDS03,FILEDS04,FILEDS05,FILEDS06,FILEDS07,FILEDS08,FILEDS09,FILEDS10,FILEDS11,FILEDS12,FILEDS13,FILEDS14,FILEDS15,FILEDS16,FILEDS17,FILEDS18,FILEDS19,FILEDS20
,FILEDS21,FILEDS22,FILEDS23,FILEDS24,FILEDS25,FILEDS26,FILEDS27,FILEDS28,FILEDS29,FILEDS30,FILEDS31,FILEDS32,FILEDS33,FILEDS34,FILEDS35 
             */
            try
            {
                if (m_ActionName == "edit")
                {
                    // �����޸ĵ��ֶΡ���¼�޸���־
                    string[] updateVal = { Fileds01, Fileds02, Fileds03, Fileds04, Fileds05, Fileds06, Fileds07, Fileds08, Fileds09, Fileds10, Fileds11, Fileds12, Fileds13, Fileds14, Fileds15, Fileds16, Fileds17, Fileds18, Fileds19 };
                    string configFile = Server.MapPath("/includes/DataGrid.config");
                    CommPage cp = new CommPage();
                    string returnVal = cp.AnalysisFields(m_FuncCode, m_ObjID, configFile, updateVal);
                    cp = null;
                    if (!string.IsNullOrEmpty(returnVal))
                    {
                        string opContents = "�û�ID[" + m_UserID + "]�� " + DateTime.Now.ToString() + " �޸������ƾ��µ�<ȫԱ�˿ڱ䶯������Ϣ>��" + returnVal;
                        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                        //list.Add("UPDATE PIS_QYK SET AUDITFLAG=2 WHERE COMMID=" + m_ObjID);
                        list.Add("INSERT INTO \"SYS_Log\"(\"OprateUserID\", \"OprateContents\", \"OprateModel\", \"OprateUserIP\") VALUES(" + m_UserID + ", '" + opContents + "', '�����޸�', '" + Request.UserHostAddress + "')");
                        DbHelperSQL.ExecuteSqlTran(list);
                        list = null;
                    }
                }
                DbHelperSQL.ExecuteSql(m_SqlParams);
                Response.Write(" <script>alert('ȫԱ�˿ڱ䶯������Ϣ�����ɹ���') ;window.location.href='" + m_TargetUrl + "'</script>");
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

