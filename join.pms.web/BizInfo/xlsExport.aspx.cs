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

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;

namespace join.pms.dalInfo
{
    /// <summary>
    /// �������ݵ���
    /// </summary>
    public partial class xlsExport : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        public string m_TargetUrl;

        protected string m_FuncCode;
        private string m_PageSearch;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_UserDept;//�û����ű���
        private string m_UserDeptArea;//�������
        private string m_UserDeptName;//��������
        private string m_UserAreaCode;//��ǰ�û���������

        private DataSet m_Ds;
        private string m_SqlParams;

        private string m_FuncInfo;
        private string m_Titles;
        private string m_Fields;

        private string m_AreaCode;

        private string m_SiteName = System.Configuration.ConfigurationManager.AppSettings["SiteName"];

        private ExportXls m_Xls;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                SetUIByFuncNo(m_FuncCode);
                SetReportArea(m_AreaCode);
            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "/css/inidex.css";  //DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                //if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
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
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            if (!string.IsNullOrEmpty(m_SourceUrl))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl); // "FuncCode=04&p=1" FuncUser
                m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                //�̳в�ѯ����
                m_PageSearch = StringProcess.AnalysisParas(m_SourceUrlDec, "pSearch");
                // "FuncCode=060101&p=1"
                //m_FuncCode = "02" + m_FuncCode.Substring(2);
                m_UserDept = join.pms.dal.CommPage.GetUnitCodeByUser(m_UserID);
                // m_UserDeptName = join.pms.dal.CommPage.GetUnitNameByCode(m_UserDept, ref m_UserDeptArea);
                //m_UserDeptArea = join.pms.dal.CommPage.GetUnitNameByCode(m_UserDept);
                m_UserAreaCode = join.pms.dal.CommPage.GetSingleVal("SELECT UserAreaCode FROM USER_BaseInfo WHERE UserID=" + m_UserID);
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }
        }

        /// <summary>
        /// ���ò�����Ϊ
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetUIByFuncNo(string funcNo)
        {

            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGridBiz.config");
            if (GetConfigParams(GetConfigCode(funcNo), configFile, ref errMsg))
            {
                m_Xls = new ExportXls();
                string[] a_FuncInfo = this.m_FuncInfo.Split(',');
                //
                this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">��ʼҳ</a> &gt;&gt; +" + CommPage.GetAllBizName(m_FuncCode) + "+ &gt;&gt; ���ݵ�����";
            }
            else
            {
                //Response.Write(" <script>alert('��ȡ[" + funcNo + "]�����ļ�ʧ�ܣ�') ;window.location.href='" + m_TargetUrl + "'</script>");
                //Response.End();
                MessageBox.ShowAndRedirect(this.Page, "������ʾ����ȡ[" + funcNo + "]�����ļ�ʧ�ܣ�", m_TargetUrl, true);

            }
            //SetDataToXls(a_FuncInfo[0], funcNo, " FuncNo='" + funcNo + "' ", a_FuncInfo[2], a_FuncInfo[2]);

            // INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES(FileName,FilePath,FileType,'0501',1,7)
        }

        private string GetConfigCode(string funcNo)
        {
            string returnVa = string.Empty;
            if (!string.IsNullOrEmpty(funcNo) && funcNo.Length > 2)
            {
                returnVa = funcNo;
                //if (funcNo.Substring(0, 4) == "0101" || funcNo.Substring(0, 4) == "0102" || funcNo.Substring(0, 4) == "0103" || funcNo.Substring(0, 4) == "0104" || funcNo.Substring(0, 4) == "0112")
                //{
                //    returnVa = funcNo;
                //}
                //else if (funcNo.Substring(0, 2) == "04")
                //{
                //    returnVa = "OCAPP";//P_Addres
                //}
                //else
                //{
                //    returnVa = "OCAPP";
                //}
            }
            else
            {
                returnVa = funcNo;
            }
            return returnVa;
        }

        /// <summary>
        /// �������ݵ�λ/����������λ �������򡭡�
        /// </summary>
        /// <param name="bizCode">Ĭ��ֵ</param>
        private void SetReportArea(string areaCode)
        {
            string siteArea = m_UserAreaCode;// System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
            // m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + siteArea + "' ORDER BY AreaCode";
            m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE AreaCode='" + siteArea + "' OR ParentCode = '" + siteArea + "' ORDER BY AreaCode";
            DataTable tmpDt = new DataTable();
            tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            DDLReportArea.DataSource = tmpDt;
            DDLReportArea.DataTextField = "AreaName";
            DDLReportArea.DataValueField = "AreaCode";
            DDLReportArea.DataBind();
            //DDLReportArea.Items.Insert(0, new ListItem("����������", "000000000000"));
            tmpDt = null;
            if (!string.IsNullOrEmpty(areaCode))
            {
                this.DDLReportArea.SelectedValue = areaCode;
            }
        }

        private void SetExcelByFuncNo(string funcNo, string filterSQL, string fileDesc)
        {
            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGridBiz.config");
            string tmpFuncNo = string.Empty;
            if (GetConfigParams(GetConfigCode(funcNo), configFile, ref errMsg))
            {
                string[] a_FuncInfo = this.m_FuncInfo.Split(',');
                m_Xls = new ExportXls();
                m_Xls.FuncNo = funcNo; // �ϼ���

                switch (funcNo)
                {
                    case "0101":
                        m_Xls.XlsName = "һ�������Ǽ�";
                        m_Xls.XlsUnit = "������Դ��" + m_SiteName;
                        // ���,״̬,���뷽��,����ʱ��,Ů������,���֤��,��ϵ�绰,����״��,������ַ,�־�ס��,�з�����,���֤��,��ϵ�绰,����״��,���ʱ��
                        m_Xls.Formats = "1,0,0,2,0,1,1,0,0,0,0,1,1,0,2"; // 1,���� ; 2,����
                        break;
                    case "0102":
                        m_Xls.XlsName = "���������Ǽ�";
                        m_Xls.XlsUnit = "������Դ��" + m_SiteName;
                        // ���,״̬,���뷽��,����ʱ��,Ů������,���֤��,��ϵ�绰,����״��,�з�����,���֤��,��ϵ�绰,������ַ,����״��,������λ,�־�ס��,���ʱ��
                        m_Xls.Formats = "1,0,0,2,0,1,1,0,0,1,1,0,0,0,0,2"; // 1,���� ; 2,����
                        break;
                    case "010299":
                        m_Xls.XlsName = "�������������ᵼ��";
                        m_Xls.XlsUnit = "������Դ��" + m_SiteName;
                        // ����	���� �������� ���֤�� ����״��	������ ����	���� �������� ���֤�� ����״��	������ ���� ���������������� ���ʱ�� ����ʱ��
                        m_Xls.Formats = "0,0,0,1,0,0,0,0,0,1,0,0,0,0,1,1"; // 1,���� ; 2,����
                        break;
                    case "0103":
                        m_Xls.XlsName = "ũ�岿�ּƻ�������ͥ�������������걨���";
                        m_Xls.XlsUnit = "������Դ��" + m_SiteName;
                        // ���,״̬,���뷽��,����ʱ��,��������,���֤��,����״��,��ϵ�绰,������ַ,����к�,���Ů��,��ż����,���֤��,����״��,��ϵ�绰,���ʱ��
                        m_Xls.Formats = "1,0,0,2,0,1,0,1,0,1,1,0,1,0,1,2"; // 1,���� ; 2,����
                        break;
                    case "0104":
                        m_Xls.XlsName = "�ƻ�������ͥ�ر���������������";
                        m_Xls.XlsUnit = "������Դ��" + m_SiteName;
                        // ���,״̬,���뷽��,����ʱ��,��������,���֤��,����״��,��ϵ�绰,������ַ,����к�,���Ů��,��ż����,���֤��,����״��,��ϵ�绰,���ʱ��
                        m_Xls.Formats = "0,0,2,4,0,1,1,0,0,0,0,1,0,2,0,0"; // 1,���� ; 2,����
                        break;
                    case "0105":
                        m_Xls.XlsName = "�ƻ������������츻�������������";
                        m_Xls.XlsUnit = "������Դ��" + m_SiteName;
                        // ���,״̬,���뷽��,����ʱ��,��������,���֤��,����״��,��ϵ�绰,������ַ,����к�,���Ů��,��ż����,���֤��,����״��,��ϵ�绰,���ʱ��
                        m_Xls.Formats = "1,0,0,2,0,1,0,1,0,1,1,0,1,0,1,2"; // 1,���� ; 2,����
                        break;
                    case "0106":
                        m_Xls.XlsName = "���ɹ������������ڶ�����˫Ů���ɹ�����Ů��������ͥ�����������";
                        m_Xls.XlsUnit = "������Դ��" + m_SiteName;
                        // ���,״̬,���뷽��,����ʱ��,��������,���֤��,����״��,��ϵ�绰,������ַ,����к�,���Ů��,��ż����,���֤��,����״��,��ϵ�绰,���ʱ��
                        m_Xls.Formats = "1,0,0,2,0,1,0,1,0,1,1,0,1,0,1,2"; // 1,���� ; 2,����
                        break;
                    case "0107":
                        m_Xls.XlsName = "��һ���̡���������������";
                        m_Xls.XlsUnit = "������Դ��" + m_SiteName;
                        // ���,״̬,���뷽��,����ʱ��,Ů������,���֤��,��ϵ�绰,����̥��,ȷ��ʱ��,�־�ס��,�з�����,���֤��,��ϵ�绰,�־�ס��,���ʱ��
                        m_Xls.Formats = "1,0,0,2,0,1,1,1,2,0,0,1,1,0,2";
                        break;
                    case "0108":
                        m_Xls.XlsName = "������Ů��ĸ����֤";
                        m_Xls.XlsUnit = "������Դ��" + m_SiteName;
                        // ���,״̬,���뷽��,����ʱ��,ĸ������,���֤��,��ϵ�绰,������ַ,��������,���֤��,��ϵ�绰,������ַ,��Ů����,��������,���ʱ��
                        m_Xls.Formats = "1,0,0,2,0,1,1,0,0,1,1,0,0,2,2";
                        break;
                    case "0109":
                        m_Xls.XlsName = "�����˿ڻ���֤��";
                        m_Xls.XlsUnit = "������Դ��" + m_SiteName;
                        // ���,״̬,���뷽��,����ʱ��,��֤��,���֤��,��ϵ�绰,����״��,������ַ,��ż����,���֤��,��ϵ�绰,����״��,������ַ,���ʱ��
                        m_Xls.Formats = "1,0,0,2,0,1,1,0,0,0,1,1,0,0,2";
                        break;
                    case "0110":
                        m_Xls.XlsName = "�������֤��";
                        m_Xls.XlsUnit = "������Դ��" + m_SiteName;
                        // ���,״̬,���뷽��,����ʱ��,��������,���֤��,����״��,��ϵ�绰,������ַ,��ż����,���֤��,����״��,��ϵ�绰,������ַ,���ʱ��
                        m_Xls.Formats = "1,0,0,2,0,1,0,1,0,0,1,0,0,1,0,2";
                        break;
                    case "0111":
                        m_Xls.XlsName = "��ֹ�����������";
                        m_Xls.XlsUnit = "������Դ��" + m_SiteName;
                        // ���,״̬,���뷽��,����ʱ��,��Ů����,���֤��,����״��,��ϵ�绰,��������,����,�ɷ�����,���֤��,��ϵ�绰,������ַ,���ʱ��
                        m_Xls.Formats = "1,0,0,2,0,1,0,1,0,1,0,1,1,0,2";
                        break;
                    case "0122":
                        m_Xls.XlsName = "����������";
                        m_Xls.XlsUnit = "������Դ��" + m_SiteName;
                        // ���,״̬,���뷽��,����ʱ��,Ů������,���֤��,��ϵ�绰,����״��,�з�����,���֤��,��ϵ�绰,������ַ,����״��,������λ,�־�ס��,���ʱ��
                        m_Xls.Formats = "1,0,0,2,0,1,1,0,0,1,1,0,0,0,0,2"; // 1,���� ; 2,����
                        break;
                    default:
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                        break;
                }

                m_Xls.Fields = this.m_Fields;
                m_Xls.Titles = this.m_Titles;
                SetDataToXls(a_FuncInfo[0], funcNo, filterSQL, a_FuncInfo[2], fileDesc);
            }
            else
            {
                //Response.Write(" <script>alert('��ȡ�����ļ�ʧ�ܣ�') ;window.location.href='" + m_TargetUrl + "'</script>");
                //Response.End();
                MessageBox.ShowAndRedirect(this.Page, "������ʾ����ȡ�����ļ�ʧ�ܣ�", m_TargetUrl, true);
            }

            // INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES(FileName,FilePath,FileType,'0501',1,7)
        }


        private bool _isXiangZhen = false;
        private string _areaName;
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string strErr = string.Empty;
                string startDate = this.txtStartDate.Value;
                string endDate = this.txtEndDate.Value;
                string expAtt = PageValidate.GetFilterSQL(Request["ExpAttribs"]);
                string searchStr = string.Empty;
                string unitSearch = string.Empty;
                string fileDesc = string.Empty;
                string pageFilter = string.Empty;


                string areaCode = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Value);
                string areaName = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Text);
                string areaCodeFilter = string.Empty;
                //if (string.IsNullOrEmpty(areaCode))
                //{
                //    strErr += "��ѡ�����ݹ���������������\\n";
                //}
                if (String.IsNullOrEmpty(startDate))
                {
                    strErr += "��ѡ�����ݿ�ʼ���ڣ�\\n";
                }
                if (String.IsNullOrEmpty(endDate))
                {
                    strErr += "��ѡ��������ֹ���ڣ�\\n";
                }
                if (String.IsNullOrEmpty(expAtt))
                {
                    //strErr += "��ѡ��Ҫ�������������ͣ�\\n";
                    expAtt = "rb1";
                }


                if (strErr != "")
                {
                    MessageBox.Show(this, strErr);
                    return;
                }

                // �б����Ĳ�ѯ����
                if (!string.IsNullOrEmpty(m_PageSearch)) pageFilter = DESEncrypt.Decrypt(m_PageSearch);
                if (string.IsNullOrEmpty(pageFilter)) { pageFilter = "1=1"; }

                if (string.IsNullOrEmpty(areaCode))
                {
                    fileDesc = "���в��ţ�"; unitSearch = "";
                }
                else
                {
                    fileDesc = areaName + "��";
                    if (areaCode.Substring(4) == "00000000") { areaCodeFilter = areaCode.Substring(0, 4); }
                    else if (areaCode.Substring(6) == "000000" && areaCode.Substring(4) != "00000000") { areaCodeFilter = areaCode.Substring(0, 6); }
                    else if (areaCode.Substring(9) == "000" && areaCode.Substring(6) != "000000") { areaCodeFilter = areaCode.Substring(0, 9); }
                    else { }
                    unitSearch = " (RegAreaCodeA LIKE '" + areaCodeFilter + "%' OR RegAreaCodeB LIKE '" + areaCodeFilter + "%' OR SelAreaCode LIKE '" + areaCodeFilter + "%')";
                }

                _areaName = areaName;

                fileDesc += "��" + startDate + "��" + endDate + "��";
                if (expAtt == "rb1")
                {
                    fileDesc += "ȫ��";
                    // BIZ_Contents --> Attribs: 0,��ʼ�ύ;1,����� 2,ͨ�� 3,���� 4,���� 5,ע�� 6,�ȴ����,9,�鵵
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                else if (expAtt == "rb2")
                {
                    fileDesc += "�����(���������)";
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND Attribs IN(0,1,6) AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                else if (expAtt == "rb3")
                {
                    fileDesc += "���ͨ��";
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND Attribs IN(2,9)  AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                else 
                {
                    fileDesc += "�������(������Ч����)";
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND Attribs IN(3,4,5) AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }


                SetExcelByFuncNo(m_FuncCode, searchStr, fileDesc);

            }
            catch (Exception ex)
            {
                this.LiteralFiles.Text = ex.Message;
            }
            //SetDataToXls(a_FuncInfo[0], funcNo, " FuncNo='" + funcNo + "' ", a_FuncInfo[2], a_FuncInfo[2]);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn010299_Click(object sender, EventArgs e)
        {
            try
            {
                string strErr = string.Empty;
                string startDate = this.txtStartDate.Value;
                string endDate = this.txtEndDate.Value;
                string expAtt = PageValidate.GetFilterSQL(Request["ExpAttribs"]);
                string searchStr = string.Empty;
                string unitSearch = string.Empty;
                string fileDesc = string.Empty;
                string pageFilter = string.Empty;


                string areaCode = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Value);
                string areaName = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Text);
                string areaCodeFilter = string.Empty;
                //if (string.IsNullOrEmpty(areaCode))
                //{
                //    strErr += "��ѡ�����ݹ���������������\\n";
                //}
                if (String.IsNullOrEmpty(startDate))
                {
                    strErr += "��ѡ�����ݿ�ʼ���ڣ�\\n";
                }
                if (String.IsNullOrEmpty(endDate))
                {
                    strErr += "��ѡ��������ֹ���ڣ�\\n";
                }
                if (String.IsNullOrEmpty(expAtt))
                {
                    //strErr += "��ѡ��Ҫ�������������ͣ�\\n";
                    expAtt = "rb1";
                }
                if (strErr != "")
                {
                    MessageBox.Show(this, strErr);
                    return;
                }

                // �б����Ĳ�ѯ����
                //if (!string.IsNullOrEmpty(m_PageSearch)) pageFilter = DESEncrypt.Decrypt(m_PageSearch);
                if (string.IsNullOrEmpty(pageFilter)) { pageFilter = "1=1"; }

                if (string.IsNullOrEmpty(areaCode))
                {
                    fileDesc = "���в��ţ�"; unitSearch = "";
                }
                else
                {
                    fileDesc = areaName + "��";
                    if (areaCode.Substring(4) == "00000000") { areaCodeFilter = areaCode.Substring(0, 4); }
                    else if (areaCode.Substring(6) == "000000" && areaCode.Substring(4) != "00000000") { areaCodeFilter = areaCode.Substring(0, 6); }
                    else if (areaCode.Substring(9) == "000" && areaCode.Substring(6) != "000000") { areaCodeFilter = areaCode.Substring(0, 9); }
                    else { }
                    unitSearch = " (RegAreaCodeA LIKE '" + areaCodeFilter + "%' OR RegAreaCodeB LIKE '" + areaCodeFilter + "%' OR SelAreaCode LIKE '" + areaCodeFilter + "%')";
                }

                _areaName = areaName;

                fileDesc += "��" + startDate + "��" + endDate + "��";
                if (expAtt == "rb1")
                {
                    fileDesc += "ȫ��";
                    // BIZ_Contents --> Attribs: 0,��ʼ�ύ;1,����� 2,ͨ�� 3,���� 4,���� 5,ע�� 6,�ȴ����,9,�鵵
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                else if (expAtt == "rb2")
                {
                    fileDesc += "�����(���������)";
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND Attribs IN(0,1,6) AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                else if (expAtt == "rb3")
                {
                    fileDesc += "���ͨ��";
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND Attribs IN(2,9)  AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }
                else
                {
                    fileDesc += "�������(������Ч����)";
                    searchStr = unitSearch + " AND  BizCode='" + m_FuncCode + "' AND Attribs IN(3,4,5) AND StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59' AND " + pageFilter;
                }

                //��������������
                m_FuncCode = "010299";
                SetUIByFuncNo(m_FuncCode);
                SetExcelByFuncNo(m_FuncCode, searchStr, fileDesc);
                m_FuncCode = "0102";
            }
            catch (Exception ex)
            {
                this.LiteralFiles.Text = ex.Message;
            }
            //SetDataToXls(a_FuncInfo[0], funcNo, " FuncNo='" + funcNo + "' ", a_FuncInfo[2], a_FuncInfo[2]);
        }

        /// <summary>
        /// ��ȡ����ƥ���ֶ�
        /// </summary>
        /// <param name="areaNo"></param>
        /// <param name="areaVal"></param>
        /// <returns></returns>
        private string GetAreaMatch(string areaNo, string areaVal, string areaName)
        {
            string returnVal = "1=1";
            string[] aryNo = areaNo.Split(',');
            string[] aryVal = areaVal.Split(',');
            if (areaName.Length > 2) areaName = areaName.Substring(0, 2);
            for (int i = 0; i < aryNo.Length; i++)
            {
                if (m_FuncCode == aryNo[i].Trim())
                {
                    returnVal = aryVal[i].Trim() + " LIKE '%" + areaName + "%'";
                    break;
                }
            }
            return returnVal;
        }

        /// <summary>
        /// ����excel
        /// </summary>
        private void SetDataToXls(string tableName, string funcNo, string filterSQL, string fileName, string descInfo)
        {
            string errMsg = string.Empty;
            string serverPath = Server.MapPath("/");
            string configPath = System.Configuration.ConfigurationManager.AppSettings["FCKeditor:UserFilesPath"];//�ļ����·��
            string virtualPath = configPath + funcNo + "/" + StringProcess.GetCurDateTimeStr(6) + "/";
            // if (m_FuncCode == "04")
            string sqlParams = "SELECT " + this.m_Fields + " FROM " + tableName + " WHERE " + filterSQL;
            string savePath = serverPath + virtualPath;
            string saveFiles = savePath + System.DateTime.Now.ToString("yyyyMMdd-hhmm") + ".xls";
            StringBuilder sHtml = new StringBuilder();
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

            if (m_Xls.ExportDsToXls(saveFiles, DbHelperSQL.Query(sqlParams), ref errMsg))
            {
                //SetRichTextBox(descInfo + "�ɹ�������");
                m_Xls = null;
                errMsg = errMsg.Replace(serverPath, "");
                errMsg = errMsg.Substring(0, errMsg.Length - 1);
                string[] aryFiles = errMsg.Split(',');
                for (int i = 0; i < aryFiles.Length; i++)
                {
                    sHtml.Append("<a href=\"" + aryFiles[i] + "\" target='_blank' > " + descInfo + fileName + "����</a><br/>");
                    SetFileToHD(descInfo + fileName + "����", aryFiles[i]);
                    sHtml.Append("�ĵ��Ѿ�ͬ��������[ ����Ӳ�� >> �ҵ����� >> ]��Ŀ¼�¡���");
                    if (_isXiangZhen)
                    {
                        // ���ݿ���һ�ݵ�������Ϣ��ȥ
                        //SetFileToXiangZhen(descInfo + fileName + "����", aryFiles[i]);
                        sHtml.Append("�ĵ�Ҳͬʱ������ ��������Ϣ�ڵ��¡���");
                    }
                }
                this.LiteralFiles.Text = sHtml.ToString();
                //Response.Write(" <script>alert('" + descInfo + " --�ɹ�������') ;window.location.href='" + m_TargetUrl + "';window.location.href='" + saveFiles + "'</script>");
            }
            else
            {
                //SetRichTextBox(descInfo + "����ʧ�ܣ���ϸ��Ϣ���£�");
                this.LiteralFiles.Text = errMsg;
            }
        }
        //SELECT CmsCID, CmsCode, CmsCName FROM CMS_Class WHERE CmsCode LIKE '03__' AND CmsCName=
        private void SetFileToXiangZhen(string fileName, string filePath)
        {
            // 
            string cmsCID = "", cmsCode = "", cmsBody = string.Empty;
            DataTable tmpDt = new DataTable();
            m_SqlParams = "SELECT CmsCID, CmsCode, CmsCName FROM CMS_Class WHERE CmsCode LIKE '03__' AND CmsCName='" + _areaName + "'";
            tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            if (tmpDt.Rows.Count == 1)
            {
                cmsCID = tmpDt.Rows[0][0].ToString();
                cmsCode = tmpDt.Rows[0][1].ToString();
            }
            tmpDt = null;
            cmsBody = "&lt;a&nbsp;href=&quot;" + filePath + "&quot;&nbsp;target=&quot;_blank&quot;&gt;" + fileName + "&lt;/a&gt;";
            m_SqlParams = "INSERT INTO [CMS_Contents](CmsTitle,CmsBody,CmsCID,UserID,CmsCode)  VALUES('" + fileName + "','" + cmsBody + "'," + cmsCID + "," + m_UserID + ",'" + cmsCode + "')";
            DbHelperSQL.ExecuteSql(m_SqlParams);
        }

        private void SetFileToHD(string fileName, string filePath)
        {
            // 
            m_SqlParams = "INSERT INTO UserHD_Files(FileName,FilePath,FileType,ClassCode,OprateUserID,DirID) VALUES('" + fileName + "','" + filePath + "','.xls','0802'," + m_UserID + ",1)";
            DbHelperSQL.ExecuteSql(m_SqlParams);
        }

        #region  ��ȡ�����ļ�����
        /// <summary>
        /// �������ݼ�
        /// </summary>
        private void ConfigDataSet()
        {
            m_Ds = new DataSet();
            m_Ds.Locale = System.Globalization.CultureInfo.InvariantCulture;
        }
        /// <summary>
        /// ��ȡ�����ļ�����
        /// </summary>
        /// <param name="funcNo">���ܺ�</param>
        /// <param name="configFile">�����ļ�·��</param>
        /// <returns></returns>
        public bool GetConfigParams(string funcNo, string configFile, ref string errorMsg)
        {
            try
            {
                ConfigDataSet();

                m_Ds.ReadXml(configFile, XmlReadMode.ReadSchema);
                DataRow[] dr = m_Ds.Tables[0].Select("FuncNo='" + funcNo + "'");
                if (funcNo.Substring(0, 2) == "04")
                {
                    dr = m_Ds.Tables[0].Select("FuncNo='04'");
                }



                this.m_FuncInfo = dr[0][1].ToString();
                this.m_Titles = dr[0][2].ToString();
                this.m_Fields = dr[0][3].ToString();

                m_Ds = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = "��ȡ�����ļ�����ʧ�ܣ�" + ex.Message;
                return false;
            }
        }
        #endregion
    }
}
