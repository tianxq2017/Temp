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

namespace join.pms.web.BizInfo
{
    public partial class UnvBizList : System.Web.UI.Page
    {
        private string m_FuncCode;
        private string m_FuncName;
        private string m_FuncUser;
        private string m_TabPageNo;
        private string m_PageSearch;
        private string m_RangeSearch;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_UserDept;//�û����ű���
        private string m_UserDeptName;//��������
        private string m_UserDeptArea;//������� 
        private string m_RoleID;
        private string m_FuncTreeName;

        private string m_AreaNo = System.Configuration.ConfigurationManager.AppSettings["AreaNo"];
        private string m_AreaVal = System.Configuration.ConfigurationManager.AppSettings["AreaVal"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            SetPageStyle(m_UserID);
            ValidateParams();
        }

        private void ValidateParams() {
            m_FuncCode = PageValidate.GetFilterSQL(Request.QueryString["FuncCode"]);// ���ʴ������б�Ĺ��ܱ��
            m_FuncName = PageValidate.GetFilterSQL(Request.QueryString["FuncNa"]);//��������
            m_FuncUser = PageValidate.GetFilterSQL(Request.QueryString["FuncUser"]);
            m_TabPageNo = PageValidate.GetFilterSQL(Request.QueryString["TabPage"]);
            m_PageSearch = PageValidate.GetFilterSQL(Request.QueryString["pSearch"]);//ͨ������
            m_RangeSearch = PageValidate.GetFilterSQL(Request.QueryString["searchRange"]);// ��Χ����,���̽ڵ�
            string pageNo = PageValidate.GetFilterSQL(Request.QueryString["p"]);
            string searchKey = PageValidate.GetFilterSQL(Request.QueryString["k"]);
            string userSearchKey = PageValidate.GetFilterSQL(Request.QueryString["searchKey"]);
            //string urlTemp = Request.Url.ToString();
            string urlParams = "FuncUser=" + m_FuncUser + "&FuncCode=" + m_FuncCode + "&FuncNa=" + Server.UrlEncode(m_FuncName);
            if (String.IsNullOrEmpty(m_TabPageNo)) m_TabPageNo = "0";

            if (String.IsNullOrEmpty(pageNo)) pageNo = "1";
            if (String.IsNullOrEmpty(m_FuncCode) || !PageValidate.IsNumber(pageNo) || string.IsNullOrEmpty(m_UserID))
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }
            else
            {
                if (!String.IsNullOrEmpty(userSearchKey))
                {
                    searchKey = userSearchKey;
                }
                this.tbFuncNo.Value = m_FuncCode;
                m_RoleID = DbHelperSQL.GetSingle("SELECT TOP 1 RoleID FROM SYS_UserRoles WHERE UserID=" + m_UserID).ToString();
                //// Ĭ�Ϲ���Ա�����ؼ���ʾ����ҵ���б�
                //if (m_RoleID != "5")
                //{
                //    if (string.IsNullOrEmpty(m_RangeSearch)) m_RangeSearch = "range_all";
                //}
                //else
                //{
                //    if (string.IsNullOrEmpty(m_RangeSearch)) m_RangeSearch = "range_audit";
                //}
                if (string.IsNullOrEmpty(m_RangeSearch)) m_RangeSearch = "range_all";
                GetDataGrid(searchKey, m_FuncCode, pageNo, urlParams);
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
                string pageUrl = Request.RawUrl;
                if (!string.IsNullOrEmpty(pageUrl))
                {
                    if (pageUrl.IndexOf("FuncUser") > 5)
                    {
                        m_UserID = DESEncrypt.Decrypt(m_FuncUser);
                        SetUserLoginInfo(m_UserID);
                    }
                    else if (pageUrl.IndexOf("action=relogin") > 5)
                    {
                        Response.Write("<script language='javascript'>parent.location.href='/loginTemp.aspx';</script>");
                        Response.End();
                    }
                    else
                    {
                        //MessageBox.ShowAndRedirect(this.Page, "", pageUrl, true);
                        Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/loginTemp.aspx';</script>");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/loginTemp.aspx';</script>");
                    Response.End();
                }
            }
            else {
                
            }
        }

        /// ���ò������û���½��Ϣ
        /// </summary>
        /// <param name="userID"></param>
        private void SetUserLoginInfo(string userID)
        {
            //�����û���½��Ϣcookie
            if (Request.Browser.Cookies)
            {
                HttpCookie cookie = new HttpCookie("AREWEB_OC_USER_YSL");
                cookie.Values.Add("UserID", userID);
                Response.AppendCookie(cookie);
                cookie.Expires = DateTime.Now.AddHours(4); //cookie����ʱ��
            }
            else
            {
                Session["AREWEB_OC_USERID"] = userID;
            }
        }

        /// <summary>
        /// ����ҳ����ʽ
        /// </summary>
        /// <param name="userID"></param>
        private void SetPageStyle(string userID)
        {
            try
            {
                //string cssFile = DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                //if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";
                string cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        /// <summary>
        /// ��ȡ�����б������б��������ʾ��
        /// ��ʤ�� 2008/08/30 
        /// </summary>
        /// <param name="searchKeys"></param>
        /// <param name="funcNo"></param>
        /// <param name="pageNo"></param>
        private void GetDataGrid(string searchKeys, string funcNo, string pageNo, string urlParams)
        {
            string errorMsg = string.Empty;
            string searchSQL = string.Empty;
            string pageSearch = string.Empty;

            string configFile = Server.MapPath("/includes/DataGridBiz.config");
            string pageSize = System.Configuration.ConfigurationManager.AppSettings["GridPageSize"]; // ÿҳ��ʾ�ļ�¼��
            string funcPowers = string.Empty;//  // �Ĺ��ܵ�Ȩ�޼� 
            //m_UserDeptName = join.pms.dal.CommPage.GeUserAreaInfo(m_UserID, ref m_UserDeptArea);
            m_UserDept = join.pms.dal.CommPage.GetUnitCodeByUser(m_UserID, ref m_UserDeptArea);
            m_UserDeptName = join.pms.dal.CommPage.GetUnitNameByCode(m_UserDept);
            if (String.IsNullOrEmpty(funcNo) || string.IsNullOrEmpty(m_UserDept))
            {
                LiteralDataList.Text = "����ʧ�ܣ���������!";
                return;
            }
            //�����û����������жϲ���Ȩ�� CommPage.GetUserBizPower(m_UserID)
            funcPowers = CommPage.GetUserBizPower(m_UserID, funcNo);

            //=================
            searchSQL = GetSearchByFuncNo(funcNo, m_UserID);

            join.pms.dal.DataListBiz pageList = new DataListBiz();

            if (pageList.GetConfigParams(GetConfigCode(funcNo), configFile, ref errorMsg))
            {
                if (!String.IsNullOrEmpty(searchKeys) && !String.IsNullOrEmpty(pageList.SearchFields))
                {
                    if (!String.IsNullOrEmpty(searchSQL)) { searchSQL += " AND " + pageList.SearchFields + " LIKE '%" + searchKeys + "%' "; }
                    else { searchSQL += " " + pageList.SearchFields + " LIKE '%" + searchKeys + "%' "; }
                }
                try
                {
                    pageList.TabPageNo = m_TabPageNo;
                    pageList.PageSize = int.Parse(pageSize);
                    pageList.PageNo = int.Parse(pageNo);
                    pageList.FuncPowers = funcPowers;
                    pageList.FuncNo = funcNo;
                    pageList.UserAreaCode = m_UserDeptArea;
                    //pageList.m_UserRoleID = m_RoleID;
                    // ========== ͨ������ Start ===========> GetRangeSearch
                    if (!String.IsNullOrEmpty(Request["searchAction"]) && Request["searchAction"] == "advSearch")
                    {
                        pageSearch = GetPageSearch(pageList.FieldsName, pageList.FieldsFormat);
                        m_PageSearch = "";
                    }
                    else if (!String.IsNullOrEmpty(m_PageSearch))
                    {
                        pageSearch = DESEncrypt.Decrypt(m_PageSearch);
                    }
                    if (!String.IsNullOrEmpty(pageSearch))
                    {
                        if (string.IsNullOrEmpty(searchSQL)) { searchSQL += pageSearch; }
                        else { searchSQL += " AND " + pageSearch; }
                    }
                    // ========== ͨ������ End ===========>
                    // BizInfo/UnvBizList.aspx?1=1&pSearch=E31E58FB2A760DEA&UserID=1&FuncUser=&FuncCode=0102&FuncNa=&p=1&searchRange=range_all
                    // &searchRange=" + m_RangeSearch + "
                    pageList.Url = "/BizInfo/UnvBizList.aspx?1=1&pSearch=" + DESEncrypt.Encrypt(pageSearch) + "&UserID=" + m_UserID + "&" + urlParams;

                    pageList.SearchKeys = Server.UrlEncode(searchKeys);
                    pageList.SearchType = "";
                    pageList.RangeKey = m_RangeSearch;
                    pageList.SearchWhere = searchSQL;
                    // ��ҳ����
                    this.LiteralDataList.Text = pageList.GetList();
                    this.m_FuncTreeName = pageList.FuncTreeName;

                    if (!String.IsNullOrEmpty(urlParams)) this.txtUrlParams.Value = DESEncrypt.Encrypt("pSearch=" + DESEncrypt.Encrypt(pageSearch) + "&searchRange=" + m_RangeSearch + "&UserID=" + m_UserID + "&" + urlParams + "&p=" + pageNo);//���ֲ������ҳ�� + "&p=" + pageNo
                    // �鿴��־
                    DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "���ݲ鿴", "�û��� " + DateTime.Now.ToString() + " �鿴��[ " + m_FuncTreeName + " ]������");
                }
                catch (Exception ex)
                {
                    LiteralDataList.Text = ex.Message;
                }
            }
            else
            {
                LiteralDataList.Text = errorMsg;
            }
            pageList = null;
        }

        /// <summary>
        /// ���ݹ��ܺŻ�ȡ�����ļ�
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetConfigFileName(string funcNo)
        {
            string returnVa = string.Empty;
            if (!string.IsNullOrEmpty(funcNo) && funcNo.Length > 1 && PageValidate.IsNumber(funcNo))
            {
                //funcNo = funcNo.Substring(0, 2);
                //funcNo = funcNo.Substring(0, 2);
                //if (funcNo == "01") returnVa = "/includes/DataGridAdmin.config";     // ϵͳ��̨��������
                //if (funcNo == "02") returnVa = "/includes/DataGrid.config";      // xx�Ĺ�������

                returnVa = "/includes/DataGrid.config";
            }
            else
            {
                Server.Transfer("errors.aspx");
            }
            return returnVa;
        }

        /// <summary>
        /// ��ȡ���õ����ýڱ��� 0101-0104  0112
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetConfigCode(string funcNo)
        {
            string returnVa = string.Empty;
            if (!string.IsNullOrEmpty(funcNo) && funcNo.Length > 2)
            {
                if (funcNo == "0112" || funcNo == "0113" || funcNo == "0114")
                {
                    returnVa = "OCAPP";
                }
                else
                {
                    returnVa = funcNo;
                }
            }
            else
            {
                returnVa = funcNo;
            }
            return returnVa;
        }

        /// <summary>
        /// ���ݹ��ܱ����ʾ����
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetSearchByFuncNo(string funcNo, string userID)
        {
            string returnVa = string.Empty;
            switch (funcNo)
            {
                case "0101xxx":  
                    returnVa = " BizCode = '0101xx' AND Attribs!=4 ";
                    break;
                default: // Ĭ����ʾ��ǰ������������OprateModel ='�����޸�'
                    returnVa = GetXlsFilter(funcNo);
                    break;
            }
            return returnVa;
        }
        /*
 1	ϵͳ����Ա
2	ҵ�����-����
3	ҵ����-����
4	ҵ����-���
5	ҵ����-����/��
6	ҵ����-ҽԺ
         */
        private string GetXlsFilter(string funcNo)
        {
            string returnVa = string.Empty;// RegAreaCodeA RegAreaCodeB
            // BIZ_Contents --> Attribs: 0,��ʼ�ύ;1,����� 2,ͨ�� 3,���� 4,���� 5,ע�� 6,�ȴ����,9,�鵵
            if (m_RoleID == "1")
            {
                if (!string.IsNullOrEmpty(m_RangeSearch)) {
                    returnVa = GetRangeSearch() +"AND BizCode ='" + funcNo + "' AND Attribs!=4";
                }
                else { returnVa = " Attribs IN(0,1,3,6) AND BizCode ='" + funcNo + "' AND Attribs!=4"; }
            }
            else if (m_RoleID == "2")
            {
                if (!string.IsNullOrEmpty(m_RangeSearch))
                {
                    returnVa = GetRangeSearch() + "AND BizCode ='" + funcNo + "' AND Attribs!=4 AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR GetAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%')";
                }
                else { returnVa = " Attribs IN(0,1,3,6) AND BizCode ='" + funcNo + "' AND Attribs!=4 AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR GetAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%')"; }
            }
            else if (m_RoleID == "3")
            {
                if (!string.IsNullOrEmpty(m_RangeSearch))
                {
                    returnVa = GetRangeSearch() + "AND BizCode ='" + funcNo + "' AND Attribs!=4 AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR GetAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%')";
                }
                else { returnVa = GetFilterByRole(m_RoleID, funcNo) + " Attribs IN(0,1,3,6) AND BizCode ='" + funcNo + "' AND Attribs!=4 AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR GetAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%')"; }
            }
            else if (m_RoleID == "4" )
            {
                // ҵ����-���
                if (!string.IsNullOrEmpty(m_RangeSearch))
                {
                    returnVa = GetRangeSearch() + "AND BizCode='" + funcNo + "' AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' OR GetAreaCode LIKE '" + m_UserDeptArea.Substring(0, 9) + "%') AND Attribs!=4";
                }
                else
                {
                    returnVa = GetFilterByRole(m_RoleID, funcNo) + "AND Attribs IN(0,1,3,6) AND BizCode='" + funcNo + "' AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 9) + "%') AND Attribs!=4";
                }
            }
            else if (m_RoleID == "5")
            {
                // ҵ����-����/��
                if (!string.IsNullOrEmpty(m_RangeSearch))
                {
                    returnVa = GetRangeSearch() + "AND BizCode='" + funcNo + "' AND (RegAreaCodeA= '" + m_UserDeptArea + "' OR RegAreaCodeB = '" + m_UserDeptArea + "' OR SelAreaCode='" + m_UserDeptArea + "')  AND Attribs!=4";//����Ի�����Ϊ׼
                
                   // returnVa = GetRangeSearch() + "AND BizCode='" + funcNo + "' AND (((RegAreaCodeA= '" + m_UserDeptArea + "' OR RegAreaCodeB = '" + m_UserDeptArea + "')  AND Fileds51<>'1') OR (( CurAreaCodeA= '" + m_UserDeptArea + "' OR CurAreaCodeB = '" + m_UserDeptArea + "')  AND Fileds51='1') OR SelAreaCode='" + m_UserDeptArea + "')  AND Attribs!=4";//����Ի�����Ϊ׼��//Fileds51Ϊ1������ס�����
                }
                else
                {
                    returnVa = GetFilterByRole(m_RoleID, funcNo) + "AND Attribs IN(0,1,3,6) AND BizCode='" + funcNo + "' AND (RegAreaCodeA= '" + m_UserDeptArea + "' OR RegAreaCodeB = '" + m_UserDeptArea + "' OR SelAreaCode='" + m_UserDeptArea + "') AND Attribs!=4";
                }
            }
            else
            {
                //ҵ����-ҽԺ
                if (!string.IsNullOrEmpty(m_RangeSearch)) {
                    returnVa = GetRangeSearch() + " AND BizCode ='" + funcNo + "' AND Attribs!=4";
                }
                else { returnVa = GetFilterByRole(m_RoleID, funcNo) + " AND Attribs IN(0,1,3,6) AND BizCode ='" + funcNo + "' AND Attribs!=4"; }
                
            }
            return returnVa;
        }

        private string GetFilterByRole(string roleID,string funcNo) {
            /*
1	ϵͳ����Ա
2	ҵ�����-����
3	ҵ����-����
4	ҵ����-���
5	ҵ����-����/��
6	ҵ����-ҽԺ
             * 
0101	3	Ů����/��,�з���/��,���	һ�������Ǽ�
0102	4	Ů����/��,�з���/��,���,����	���������Ǽ�
0103	3	��/��,���,����	ũ�岿�ּƻ�������ͥ�������������걨���
0104	3	��/��,���,����	�ƻ�������ͥ�ر���������������
0105	3	��/��,���,����	�ƻ������������츻�������������
0106	4	Ů����/��,�з���/��,���,����	���ɹ������������ڶ�����˫Ů���ɹ�����Ů��������ͥ�����������
0107	3	Ů����/��,�з���/��,���	��һ���̡���������������
0108	3	Ů����/��,�з���/��,���	������Ů��ĸ����֤�������
0109	3	Ů����/��,�з���/��,���	�������˿ڻ���֤�����������
0110	3	��/��,���,����	�������֤������
0111	6	��/��,���,����,����,������,����	��ֹ�����������
             * 
Attribs: 0,��ʼ�ύ;1,����� 2,ͨ�� 3,���� 4,���� 5,ע�� 6,�ȴ����,9,�鵵 
             */
            string returnVa = string.Empty;
            switch (funcNo)
            {
                case "0101":
                    if (roleID == "4") { returnVa = " CurrentStep =3 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1,2) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0102":
                    if (roleID == "3") { returnVa = " CurrentStep =4 "; }
                    else if (roleID == "4") { returnVa = "CurrentStep =3 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1,2) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0103":
                    if (roleID == "3") { returnVa = " CurrentStep =3 "; }
                    else if (roleID == "4") { returnVa = "CurrentStep =2 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0104":
                    if (roleID == "3") { returnVa = " CurrentStep =3 "; }
                    else if (roleID == "4") { returnVa = "CurrentStep =2 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0105":
                    if (roleID == "3") { returnVa = " CurrentStep =3 "; }
                    else if (roleID == "4") { returnVa = "CurrentStep =2 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0106":
                    if (roleID == "3") { returnVa = " CurrentStep =4 "; }
                    else if (roleID == "4") { returnVa = "CurrentStep =3 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1,2) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0107":
                    if (roleID == "4") { returnVa = " CurrentStep =3 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1,2) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0108":
                    if (roleID == "4") { returnVa = " CurrentStep =3 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1,2) "; }
                    else { returnVa = " 1=1"; }
                    break;
                case "0109":
                    if (roleID == "4") { returnVa = "CurrentStep=3"; }
                    else if (roleID == "5") { returnVa = "CurrentStep IN(0,1,2)"; }
                    else { returnVa = " 1=1 "; }
                    break;
                case "0110":
                    if (roleID == "3") { returnVa = "CurrentStep=3"; }
                    else if (roleID == "4") { returnVa = "CurrentStep=2"; }
                    else if (roleID == "5") { returnVa = "CurrentStep IN(0,1)"; }
                    else { returnVa = " 1=1 "; }
                    break;
                case "0111":
                    if (roleID == "3") { returnVa = "CurrentStep=6"; }
                    else if (roleID == "4") { returnVa = "CurrentStep=2"; }
                    else if (roleID == "5") { returnVa = "CurrentStep IN(0,1)"; }
                    else if (roleID == "6") { returnVa = "CurrentStep IN(3,4,5)"; }
                    else { returnVa = " 1=1 "; }
                    break;
                case "0122":
                    if (roleID == "3") { returnVa = " CurrentStep =4 "; }
                    else if (roleID == "4") { returnVa = "CurrentStep =3 "; }
                    else if (roleID == "5") { returnVa = " CurrentStep IN(0,1,2) "; }
                    else { returnVa = " 1=1"; }
                    break;
                default: // ����Ϊ�� 
                    returnVa = "CurrentStep=0";
                    break;
            }
            return returnVa;
        }


        /// <summary>
        /// ���ҷ�Χ 
        /// </summary>
        /// <returns></returns>
        private string GetRangeSearch()
        {
            string returnVa = string.Empty;
            if (!String.IsNullOrEmpty(m_RangeSearch))
            {
                switch (m_RangeSearch)
                {
                    case "range_audit": // ����� 
                        returnVa = GetFilterByRole(m_RoleID, m_FuncCode) + "AND Attribs IN(0,1,3,6) ";
                        break;
                    case "range_ok": // ���ͨ��,�ɷ�֤ 
                        returnVa = " Attribs=2 ";
                        break;
                    case "range_no": // ����
                        returnVa = " Attribs=3 ";
                        break;
                    case "range_all": // ���пɼ��� 
                        returnVa = " 1=1 ";
                        break;
                    case "range_save": // �鵵
                        returnVa = " Attribs=9 ";
                        break;
                    case "range_1": // ���̽ڵ�
                        returnVa = " CurrentStep=1 ";
                        break;
                    case "range_2":
                        returnVa = " CurrentStep=2";
                        break;
                    case "range_3":
                        returnVa = " CurrentStep=3 ";
                        break;
                    case "range_4":
                        returnVa = " CurrentStep=4 ";
                        break;
                    case "range_5":
                        returnVa = " CurrentStep=5 ";
                        break;
                    case "range_6":
                        returnVa = " CurrentStep=6 ";
                        break;
                    default:
                        returnVa = "";
                        break;
                }
            }

            return returnVa;
        }

        /// <summary>
        /// ҳ��ͨ������
        /// </summary>
        /// <param name="Fields"></param>
        /// <param name="Format"></param>
        /// <returns></returns>
        private string GetPageSearch(string Fields, string Format)
        {
            string[] a_Fields = Fields.Split(',');
            string[] a_Format = Format.Split(',');

            string returnSQL = string.Empty;
            string objSelValue = string.Empty;
            string objTxtValue = string.Empty;
            for (int i = 0; i < a_Fields.Length; i++)
            {
                objSelValue = Request["sel" + a_Fields[i]];

                if (a_Format[i].Trim() == "1") { objTxtValue = PageValidate.GetTrim(Request["txt" + a_Fields[i] + "Start"]); }
                else { objTxtValue = PageValidate.GetTrim(Request["txt" + a_Fields[i]]); }
                if (!String.IsNullOrEmpty(objTxtValue))
                {
                    if (objTxtValue.ToLower() == "null") objTxtValue = "";
                    // �ַ���ʽ 0 �ı�,1 ����,2 ����
                    if (a_Format[i].Trim() == "0")
                    {
                        if (objSelValue.ToLower().IndexOf("like") > -1) { returnSQL += a_Fields[i] + " " + objSelValue + " '%" + objTxtValue + "%' AND "; }
                        else { returnSQL += a_Fields[i] + " " + objSelValue + " '" + objTxtValue + "' AND "; }
                    }
                    else if (a_Format[i].Trim() == "1")
                    {
                        string startDate = Request["txt" + a_Fields[i] + "Start"];
                        string endDate = Request["txt" + a_Fields[i] + "End"];
                        if (!string.IsNullOrEmpty(startDate)) startDate = DateTime.Parse(startDate).AddDays(-1).ToString("yyyy-MM-dd");
                        if (!string.IsNullOrEmpty(endDate)) endDate = DateTime.Parse(endDate).AddDays(1).ToString("yyyy-MM-dd");
                        //returnSQL += a_Fields[i] + " > '" + startDate + "' AND " + a_Fields[i] + " < '" + endDate + "' AND ";
                        returnSQL += "ISDATE(" + a_Fields[i] + ")=1 AND CAST(" + a_Fields[i] + " As smalldatetime) > '" + startDate + "' AND CAST(" + a_Fields[i] + " As smalldatetime) < '" + endDate + "' AND ";
                    }
                    else { returnSQL += a_Fields[i] + " " + objSelValue + " " + objTxtValue + " AND "; }
                }
            }
            if (!String.IsNullOrEmpty(returnSQL) && returnSQL.IndexOf("AND") > 0) returnSQL = returnSQL.Substring(0, returnSQL.Length - 4);
            return returnSQL;
        }
    }
}
