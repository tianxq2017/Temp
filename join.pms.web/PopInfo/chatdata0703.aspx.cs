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
    public partial class chatdata0703 : System.Web.UI.Page
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
                this.ddlFileds01.SelectedValue = DateTime.Now.Year.ToString();
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
        /// ������֤
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
                m_NavTitle = "ʯȪ��ũ�塢���������Ů������";
            }
            else
            {
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
                    this.txtReportDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
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
            this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">������ҳ</a> &gt;&gt; �˿ڼ����� &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }

        /// <summary>
        /// �޸� UcAreaSe08
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
                    this.UcAreaSel1.SetAreaName(PageValidate.GetTrim(sdr["AreaName"].ToString()));
                    this.UcAreaSel1.SetAreaCode(PageValidate.GetTrim(sdr["AreaCode"].ToString()));
                    this.txtReportDate.Value = DateTime.Parse(sdr["ReportDate"].ToString()).ToString("yyyy-MM-dd");

                    this.ddlFileds01.SelectedValue = PageValidate.GetTrim(sdr["Fileds01"].ToString());
                    //this.txtFileds02.Text = PageValidate.GetTrim(sdr["Fileds02"].ToString());
                    this.txtFileds03.Text = PageValidate.GetTrim(sdr["Fileds03"].ToString());
                    this.txtFileds04.Text = PageValidate.GetTrim(sdr["Fileds04"].ToString());
                    ;
                    //this.txtFileds18.Value = PageValidate.GetTrim(sdr["Fileds18"].ToString());
                    if (m_ActionName == "view") this.btnAdd.Enabled = false;
                }
                sdr.Close();
                // �Ƿ�ɱ༭ SetAreaCode
                if (!isEdit)
                {
                    Response.Write(" <script>alert('����ʧ�ܣ���ѡ�����Ϣ����ͨ����ˡ������򹫿�����Ϣ������Ϣ�������༭��') ;window.location.href='" + m_TargetUrl + "'</script>");
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

        /*
         020102	0701 �˿ڼƻ�������ܱ� 0701 
        020103	0702 ũ�����ҽ��ͳ�Ʊ� 
        020104	0703 ũ�塢���������Ů������ͳ��
        020105	0704 ������Ů֤����ͳ�Ʊ�
        020106	0705 �˿ڼ��������顱ͳ�Ʊ�
        020107	0706 ��Ѽ����������ͳ�Ʊ�
        020108	0707 �����˿�ͳ�Ʊ�
         */

        private string GetAnalysisCode(string funcNo)
        {
            string returnVa = string.Empty;
            switch (funcNo)
            {
                case "020102":
                    returnVa = "0701";
                    break;
                case "020103":
                    returnVa = "0702";
                    break;
                case "020104":
                    returnVa = "0703";
                    break;
                case "020105":
                    returnVa = "0704";
                    break;
                case "020106":
                    returnVa = "0705";
                    break;
                case "020107":
                    returnVa = "0706";
                    break;
                case "020108":
                    returnVa = "0707";
                    break;
                case "020109":
                    returnVa = "0708";
                    break;
                case "020110":
                    returnVa = "0709";
                    break;
                default:
                    returnVa = "0000";
                    break;
            }
            return returnVa;
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;

            string ReportDate = PageValidate.GetTrim(this.txtReportDate.Value);
            string AreaCode = PageValidate.GetTrim(this.UcAreaSel1.GetAreaCode());
            string AreaName = PageValidate.GetTrim(this.UcAreaSel1.GetAreaName());
            string analysisCode = GetAnalysisCode(m_FuncCode);

            int Fileds02 = 0;
            string Fileds01 = PageValidate.GetTrim(this.ddlFileds01.SelectedValue);
            //string Fileds02 = PageValidate.GetTrim(this.txtFileds02.Text);
            string Fileds03 = PageValidate.GetTrim(this.txtFileds03.Text);
            string Fileds04 = PageValidate.GetTrim(this.txtFileds04.Text);


            if (String.IsNullOrEmpty(ReportDate)) strErr += "��ѡ���������ڣ�\\n";
            if (string.IsNullOrEmpty(AreaCode)) { strErr += "��ѡ�����ݹ���������������\\n"; }
            else { 
                //if (AreaCode.Substring(9) == "000") strErr += "��ѡ�񵽴�/����һ���������\\n"; 
            }

            if (String.IsNullOrEmpty(Fileds01) || !PageValidate.IsNumber(Fileds01)) strErr += "��Ȳ���Ϊ���ұ���Ϊ���ָ�ʽ��\\n";
            //if (String.IsNullOrEmpty(Fileds02) || !PageValidate.IsNumber(Fileds02)) strErr += "����������Ϊ���ұ���Ϊ���ָ�ʽ��\\n";
            if (String.IsNullOrEmpty(Fileds03) || !PageValidate.IsNumber(Fileds03)) strErr += "���в���Ϊ���ұ���Ϊ���ָ�ʽ��\\n";
            if (String.IsNullOrEmpty(Fileds04) || !PageValidate.IsNumber(Fileds04)) strErr += "��Ů����Ϊ���ұ���Ϊ���ָ�ʽ��\\n";
          
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            Fileds02 = int.Parse(Fileds03) + int.Parse(Fileds04);
            // CommID,OprateUserID,OprateDate,ReportDate,FuncNo,UnitNo,
            if (m_ActionName == "add")
            {
                m_SqlParams = "INSERT INTO [PIS_BaseInfo](";
                m_SqlParams += "OprateUserID,FuncNo,";
                m_SqlParams += "Fileds01,Fileds02,Fileds03,Fileds04,";
                m_SqlParams += "ReportDate,AreaCode,AreaName";
                m_SqlParams += ") VALUES(";
                m_SqlParams += "" + m_UserID + ",'" + analysisCode + "',";
                m_SqlParams += "'" + Fileds01 + "','" + Fileds02 + "','" + Fileds03 + "','" + Fileds04 + "',";
                m_SqlParams += "'" + ReportDate + "','" + AreaCode + "','" + AreaName + "'";
                m_SqlParams += ")";
            }
            else if (m_ActionName == "edit")
            {
                m_SqlParams = "UPDATE PIS_BaseInfo SET ";
                m_SqlParams += "Fileds01 ='" + Fileds01 + "',Fileds02 ='" + Fileds02 + "',Fileds03 ='" + Fileds03 + "',Fileds04 ='" + Fileds04 + "',";
                m_SqlParams += "ReportDate ='" + ReportDate + "',AreaCode ='" + AreaCode + "',AreaName ='" + AreaName + "' ";
                m_SqlParams += " WHERE CommID=" + m_ObjID;
            }

            try
            {
                // ��¼�޸���־
                //if (m_ActionName == "edit")
                //{
                //    // �����޸ĵ��ֶΡ���¼�޸���־
                //    string[] updateVal = { Fileds01, Fileds02.ToString(), Fileds03, Fileds04 };
                //    string configFile = Server.MapPath("/includes/DataGrid.config");
                //    AreWeb.CertificateInOne.Biz.CommPage cp = new AreWeb.CertificateInOne.Biz.CommPage();
                //    string returnVal = cp.AnalysisFields(m_FuncCode, m_ObjID, configFile, updateVal);
                //    cp = null;
                //    if (!string.IsNullOrEmpty(returnVal))
                //    {
                //        string opContents = "�û�ID[" + m_UserID + "]�� " + DateTime.Now.ToString() + " �޸����˿ڼ������µ�<ʯȪ��ũ�塢���������Ů������>��" + returnVal;
                //        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                //        list.Add("UPDATE PIS_BaseInfo SET AuditFlag=2 WHERE CommID=" + m_ObjID);
                //        list.Add("INSERT INTO [SYS_Log]([OprateUserID], [OprateContents], [OprateModel], [OprateUserIP]) VALUES(" + m_UserID + ", '" + opContents + "', '�����޸�', '" + Request.UserHostAddress + "')");
                //        DbHelperSQL.ExecuteSqlTran(list);
                //        list = null;
                //    }
                //}
                DbHelperSQL.ExecuteSql(m_SqlParams);
                Response.Write(" <script>alert('ʯȪ��ũ�塢���������Ů�����Ѳ����ɹ���') ;window.location.href='" + m_TargetUrl + "'</script>");
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

