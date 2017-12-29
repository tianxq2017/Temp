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
    public partial class chatdata0701 : System.Web.UI.Page
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
                m_NavTitle = "�˿ڼƻ�������ܷ�����������";
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

                    this.txtFileds01.Text = PageValidate.GetTrim(sdr["Fileds01"].ToString());
;
                    this.txtFileds05.Text = PageValidate.GetTrim(sdr["Fileds05"].ToString());
                    this.txtFileds06.Text = PageValidate.GetTrim(sdr["Fileds06"].ToString());
                    this.txtFileds07.Text = PageValidate.GetTrim(sdr["Fileds07"].ToString());
                    this.txtFileds08.Text = PageValidate.GetTrim(sdr["Fileds08"].ToString());
                    this.txtFileds09.Text = PageValidate.GetTrim(sdr["Fileds09"].ToString());
                    this.txtFileds10.Text = PageValidate.GetTrim(sdr["Fileds10"].ToString());
                    this.txtFileds11.Text = PageValidate.GetTrim(sdr["Fileds11"].ToString());
                    this.txtFileds12.Text = PageValidate.GetTrim(sdr["Fileds12"].ToString());
                    this.txtFileds13.Text = PageValidate.GetTrim(sdr["Fileds13"].ToString());
                    this.txtFileds14.Text = PageValidate.GetTrim(sdr["Fileds14"].ToString());
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

        private string GetAnalysisCode(string funcNo) {
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

            string Fileds01 = PageValidate.GetTrim(this.txtFileds01.Text);
            int Fileds02 = 0;
            int Fileds03 = 0;
            int Fileds04 = 0;
            string Fileds05 = PageValidate.GetTrim(this.txtFileds05.Text);
            string Fileds06 = PageValidate.GetTrim(this.txtFileds06.Text);
            string Fileds07 = PageValidate.GetTrim(this.txtFileds07.Text);
            string Fileds08 = PageValidate.GetTrim(this.txtFileds08.Text);
            string Fileds09 = PageValidate.GetTrim(this.txtFileds09.Text);
            string Fileds10 = PageValidate.GetTrim(this.txtFileds10.Text);
            string Fileds11 = PageValidate.GetTrim(this.txtFileds11.Text);
            string Fileds12 = PageValidate.GetTrim(this.txtFileds12.Text);
            string Fileds13 = PageValidate.GetTrim(this.txtFileds13.Text);
            string Fileds14 = PageValidate.GetTrim(this.txtFileds14.Text);
            float Fileds15 = 0;
            float Fileds16 = 0;
            float Fileds17 = 0;

            if (String.IsNullOrEmpty(ReportDate)) strErr += "��ѡ���������ڣ�\\n";
            if (string.IsNullOrEmpty(AreaCode)){strErr += "��ѡ�����ݹ���������������\\n";}
            else {
                //if (AreaCode.Substring(9) == "000") strErr += "��ѡ�񵽴�/����һ���������\\n";
            }

            if (String.IsNullOrEmpty(Fileds01) || !PageValidate.IsNumber(Fileds01)) strErr += "��ĩ�˿�������Ϊ���ұ���Ϊ���ָ�ʽ��\\n";
            if (String.IsNullOrEmpty(Fileds05) || !PageValidate.IsNumber(Fileds05)) strErr += "�����˿ڵ�һ����������Ϊ���ұ���Ϊ���ָ�ʽ��\\n";
            if (String.IsNullOrEmpty(Fileds06) || !PageValidate.IsNumber(Fileds06)) strErr += "�����˿ڵ�һ�����Բ���Ϊ���ұ���Ϊ���ָ�ʽ��\\n";
            if (String.IsNullOrEmpty(Fileds07) || !PageValidate.IsNumber(Fileds07)) strErr += "�����˿ڵ�һ��Ů�Բ���Ϊ���ұ���Ϊ���ָ�ʽ��\\n";
            if (String.IsNullOrEmpty(Fileds08) || !PageValidate.IsNumber(Fileds08)) strErr += "�����˿ڵĶ�����������Ϊ���ұ���Ϊ���ָ�ʽ��\\n";
            if (String.IsNullOrEmpty(Fileds09) || !PageValidate.IsNumber(Fileds09)) strErr += "�����˿ڵĶ������Բ���Ϊ���ұ���Ϊ���ָ�ʽ��\\n";
            if (String.IsNullOrEmpty(Fileds10) || !PageValidate.IsNumber(Fileds10)) strErr += "�����˿ڵĶ���Ů�Բ���Ϊ���ұ���Ϊ���ָ�ʽ��\\n";
            if (String.IsNullOrEmpty(Fileds11) || !PageValidate.IsNumber(Fileds11)) strErr += "�����˿ڵĶຢ��������Ϊ���ұ���Ϊ���ָ�ʽ��\\n";
            if (String.IsNullOrEmpty(Fileds12) || !PageValidate.IsNumber(Fileds12)) strErr += "�����˿ڵĶຢ���Բ���Ϊ���ұ���Ϊ���ָ�ʽ��\\n";
            if (String.IsNullOrEmpty(Fileds13) || !PageValidate.IsNumber(Fileds13)) strErr += "�����˿ڵĶຢŮ�Բ���Ϊ���ұ���Ϊ���ָ�ʽ��\\n";
            if (String.IsNullOrEmpty(Fileds14) || !PageValidate.IsNumber(Fileds14)) strErr += "������������Ϊ���ұ���Ϊ���ָ�ʽ��\\n";
            
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            Fileds02 = int.Parse(Fileds05) + int.Parse(Fileds08) + int.Parse(Fileds11);
            Fileds03 = int.Parse(Fileds06) + int.Parse(Fileds09) + int.Parse(Fileds12);
            Fileds04 = int.Parse(Fileds07) + int.Parse(Fileds10) + int.Parse(Fileds13);
            Fileds15 = (float.Parse(Fileds02.ToString()) / float.Parse(Fileds01))*1000 ;//������
            Fileds16 = (float.Parse(Fileds14.ToString()) / float.Parse(Fileds01))*1000;//������
            Fileds17 = Fileds15 - Fileds16;//������
            // CommID,OprateUserID,OprateDate,ReportDate,FuncNo,UnitNo,
            if (m_ActionName == "add")
            {
                m_SqlParams = "INSERT INTO [PIS_BaseInfo](";
                m_SqlParams += "OprateUserID,FuncNo,";
                m_SqlParams += "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,";
                m_SqlParams += "Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,";
                m_SqlParams += "ReportDate,AreaCode,AreaName";
                m_SqlParams += ") VALUES(";
                m_SqlParams += "" + m_UserID + ",'" + analysisCode + "',";
                m_SqlParams += "'" + Fileds01 + "','" + Fileds02 + "','" + Fileds03 + "','" + Fileds04 + "','" + Fileds05 + "','" + Fileds06 + "','" + Fileds07 + "','" + Fileds08 + "','" + Fileds09 + "','" + Fileds10 + "',";
                m_SqlParams += "'" + Fileds11 + "','" + Fileds12 + "','" + Fileds13 + "','" + Fileds14 + "','" + Fileds15.ToString("F2") + "','" + Fileds16.ToString("F2") + "','" + Fileds17.ToString("F2") + "',";
                m_SqlParams += "'" + ReportDate + "','" + AreaCode + "','" + AreaName + "'";
                m_SqlParams += ")";
            }
            else if (m_ActionName == "edit")
            {
                m_SqlParams = "UPDATE PIS_BaseInfo SET ";
                m_SqlParams += "Fileds01 ='" + Fileds01 + "',Fileds02 ='" + Fileds02 + "',Fileds03 ='" + Fileds03 + "',Fileds04 ='" + Fileds04 + "',Fileds05 ='" + Fileds05 + "',";
                m_SqlParams += "Fileds06 ='" + Fileds06 + "',Fileds07 ='" + Fileds07 + "',Fileds08 ='" + Fileds08 + "',Fileds09 ='" + Fileds09 + "',Fileds10 ='" + Fileds10 + "',";
                m_SqlParams += "Fileds11 ='" + Fileds11 + "',Fileds12 ='" + Fileds12 + "',Fileds13 ='" + Fileds13 + "',Fileds14 ='" + Fileds14 + "', Fileds15 ='" + Fileds15.ToString("F2") + "',Fileds16 ='" + Fileds16.ToString("F2") + "',Fileds17 ='" + Fileds17.ToString("F2") + "',";
                m_SqlParams += "ReportDate ='" + ReportDate + "',AreaCode ='" + AreaCode + "',AreaName ='" + AreaName + "' ";
                m_SqlParams += " WHERE CommID=" + m_ObjID;
            }

            try
            {
                // ��¼�޸���־
                //if (m_ActionName == "edit")
                //{
                //    // �����޸ĵ��ֶΡ���¼�޸���־
                //    string[] updateVal = { Fileds01, Fileds02.ToString(), Fileds03.ToString(), Fileds04.ToString(), Fileds05, Fileds06, Fileds07, Fileds08, Fileds09, Fileds10, Fileds11, Fileds12, Fileds13, Fileds14, Fileds15.ToString("F0"), Fileds16.ToString("F0"), Fileds17.ToString() };
                //    string configFile = Server.MapPath("/includes/DataGrid.config");
                //    AreWeb.CertificateInOne.Biz.CommPage cp = new AreWeb.CertificateInOne.Biz.CommPage();
                //    string returnVal = cp.AnalysisFields(m_FuncCode, m_ObjID, configFile, updateVal);
                //    cp = null;
                //    if (!string.IsNullOrEmpty(returnVal))
                //    {
                //        string opContents = "�û�ID[" + m_UserID + "]�� " + DateTime.Now.ToString() + " �޸����˿ڼ������µ�<�˿ڼƻ�������ܷ�����������>��" + returnVal;
                //        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                //        list.Add("UPDATE PIS_BaseInfo SET AuditFlag=2 WHERE CommID=" + m_ObjID);
                //        list.Add("INSERT INTO [SYS_Log]([OprateUserID], [OprateContents], [OprateModel], [OprateUserIP]) VALUES(" + m_UserID + ", '" + opContents + "', '�����޸�', '" + Request.UserHostAddress + "')");
                //        DbHelperSQL.ExecuteSqlTran(list);
                //        list = null;
                //    }
                //}
                DbHelperSQL.ExecuteSql(m_SqlParams);
                Response.Write(" <script>alert('�˿ڼƻ�������ܷ����������ݲ����ɹ���') ;window.location.href='" + m_TargetUrl + "'</script>");
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

