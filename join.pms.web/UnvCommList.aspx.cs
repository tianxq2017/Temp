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

namespace join.pms.web
{
    public partial class UnvCommList : System.Web.UI.Page
    {
        private string m_FuncCode;
        private string m_FuncName;
        private string m_FuncUser;
        private string m_TabPageNo;
        private string m_PageSearch;
        private string m_RangeSearch;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_UserDeptCode;//���ű���
        private string m_UserDeptName;//��������
        private string m_UserAreaCode;//��������
        private string m_UserAreaName;//��������
        private string m_RoleID;
        private string m_FuncTreeName;

        private string m_AreaNo = System.Configuration.ConfigurationManager.AppSettings["AreaNo"];
        private string m_AreaVal = System.Configuration.ConfigurationManager.AppSettings["AreaVal"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            SetPageStyle(m_UserID);
            ValidatePageParams();
        }


        /// <summary>
        /// �����֤
        /// </summary>


        /// ���ò������û���½��Ϣ
        /// </summary>
        /// <param name="userID"></param>


        #region �����֤������У���
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
                        //Response.Write("<script language='javascript'>parent.location.href='" + pageUrl + "&action=relogin';</script>");
                        //Response.End();
                        MessageBox.ShowAndRedirect(this.Page, "", pageUrl, true);
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/loginTemp.aspx';</script>");
                    Response.End();
                }
            }
        }
        /// <summary>
        /// ҳ�������֤����
        /// </summary>
        private void ValidatePageParams()
        {
            m_FuncCode = Request.QueryString["FuncCode"];// ���ʴ������б�Ĺ��ܱ��
            m_FuncName = Request.QueryString["FuncNa"];//��������
            m_FuncUser = Request.QueryString["FuncUser"];
            m_TabPageNo = Request.QueryString["TabPage"];
            m_PageSearch = Request.QueryString["pSearch"];//ͨ������
            m_RangeSearch = Request.QueryString["searchRange"];// ��Χ����
            string pageNo = Request.QueryString["p"];
            string searchKey = Request.QueryString["k"];
            string userSearchKey = Request.QueryString["searchKey"];

            string urlParams = "FuncUser=" + m_FuncUser + "&FuncCode=" + m_FuncCode + "&FuncNa=" + Server.UrlEncode(m_FuncName);


            if (String.IsNullOrEmpty(m_TabPageNo)) m_TabPageNo = "0";
            if (String.IsNullOrEmpty(pageNo)) pageNo = "1";
            if (String.IsNullOrEmpty(m_FuncCode) || !PageValidate.IsNumber(pageNo))
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

                GetUserInfo();
                GetDataGrid(searchKey, m_FuncCode, pageNo, urlParams);
                // �鿴��־
                DbHelperSQL.SetSysLog(m_UserID, Request.UserHostAddress, "���ݲ鿴", "�û��� " + DateTime.Now.ToString() + " �鿴��[ " + m_FuncTreeName + " ]������");
            }
        }

        private void GetUserInfo()
        {
            string[] aryUserInfo = new string[10];
            // RoleID,UserAccount,UserName,DeptCode,DeptName,UserUnitName,UserAreaCode,UserAreaName,UserTel,UserWeiXinNo
            if (CommPage.GetUserInfoByID(m_UserID, ref aryUserInfo))
            {
                m_RoleID = aryUserInfo[0];
                m_UserDeptCode = aryUserInfo[3];
                m_UserDeptName = aryUserInfo[4];
                m_UserAreaCode = aryUserInfo[6];
                m_UserAreaName = aryUserInfo[7];
            }
            else
            {
                Response.Write("�Ƿ����ʣ�δ��ȡ���û���Ϣ���ݣ���������ֹ��");
                Response.End();
            }
            /*
            1	ϵͳ����Ա, 2ҵ�����-����, 3ҵ����-����
            4	ҵ����-���
            5	ҵ����-����/��,6	ҵ����-ҽԺ
            */
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
                string cssFile = "";// DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        #endregion


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

            string configFile = Server.MapPath(GetConfigFileName(funcNo));
            string pageSize = System.Configuration.ConfigurationManager.AppSettings["GridPageSize"]; // ÿҳ��ʾ�ļ�¼��
            string funcPowers = string.Empty;//  // ���ܵ�Ȩ�޼� 

            if (String.IsNullOrEmpty(funcNo) || string.IsNullOrEmpty(m_UserAreaCode))
            {
                LiteralDataList.Text = "����ʧ�ܣ���������!";
                return;
            }
            //�����û����������жϲ���Ȩ��
            funcPowers = DbHelperSQL.GetFuncPower(m_UserID, funcNo);
            //funcPowers = "1,1,1,1,1,1,1,1,1"; //��ʼ�ſ����й���Ȩ��
            // �ж�������Ϣ��Ȩ��:�����û��������õ�Ȩ��,�����û�������Ա��ֻ�ܲ鿴
            //if (int.Parse(m_UserID)>3 && funcNo.Length > 2)
            //{
            //    if (funcNo.Substring(0, 2) == "03")
            //    {
            //        if (m_UserDeptCodeCode == funcNo) { funcPowers = DbHelperSQL.GetFuncPower(m_UserID, funcNo); }
            //        else {funcPowers = "0,0,0,0,0,0,0,0,0";}
            //    }
            //    else { funcPowers = DbHelperSQL.GetFuncPower(m_UserID, funcNo); }
            //}
            //else { funcPowers = DbHelperSQL.GetFuncPower(m_UserID, funcNo); }

            //=================
            searchSQL = GetSearchByFuncNo(funcNo, m_UserID);

            if (funcNo != "")
            {
                //�����������Լ��ı���
                if (funcNo.Substring(0, 4) == "0301" || funcNo.Substring(0, 4) == "0302")
                {
                    searchSQL += " and AreaCode = '" + m_UserAreaCode + "' and FuncNo ='" + funcNo + "'";
                }
            }


            join.pms.dal.DataList pageList = new join.pms.dal.DataList();

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
                    // ========== ͨ������ Start ===========>
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
                    pageList.Url = "UnvCommList.aspx?1=1&pSearch=" + DESEncrypt.Encrypt(pageSearch) + "&UserID=" + m_UserID + "&" + urlParams;
                    //this.txtUrlParams.Value = DESEncrypt.Encrypt("pSearch=" + DESEncrypt.Encrypt(pageSearch) + "&UserID=" + m_UserID + "&" + urlParams);

                    pageList.SearchKeys = Server.UrlEncode(searchKeys);
                    pageList.SearchType = "";
                    pageList.SearchWhere = searchSQL;
                    // ��ҳ����
                    this.LiteralDataList.Text = pageList.GetList();
                    this.m_FuncTreeName = pageList.FuncTreeName;

                    if (!String.IsNullOrEmpty(urlParams)) this.txtUrlParams.Value = DESEncrypt.Encrypt("pSearch=" + DESEncrypt.Encrypt(pageSearch) + "&UserID=" + m_UserID + "&" + urlParams + "&p=" + pageNo);//���ֲ������ҳ��
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
                //if (funcNo.Substring(0, 2) == "01")
                //{
                //    returnVa = "/includes/DataGridShare.config";     // ���ݹ��������ļ�
                //}
                //else if (funcNo.Substring(0, 2) == "02")
                //{
                //    returnVa = "/includes/DataGridBiz.config";  //ҵ����������ļ�
                //}
                //else
                //{
                returnVa = "/includes/DataGrid.config";
                //}
            }
            else
            {
                Server.Transfer("errors.aspx");
            }
            return returnVa;
        }

        /// <summary>
        /// ��ȡ���õ����ýڱ���
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetConfigCode(string funcNo)
        {
            string returnVa = string.Empty;
            if (!string.IsNullOrEmpty(funcNo) && funcNo.Length > 2)
            {
                if (funcNo.Substring(0, 2) == "04")
                {
                    returnVa = funcNo.Substring(0, 2);
                }
                else if (funcNo.Substring(0, 4) == "1004")
                {
                    returnVa = "NOTES";
                }
                else if (funcNo.Length > 3 && funcNo.Substring(0, 4) == "0103")
                {
                    returnVa = "0103";//��׼����
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
        /*
         
1	ϵͳ����Ա	���Ȩ�ޣ���ʹϵͳ������
2	�ճ�����	�ճ������ɫ
3	���ƾ�	�����Ž�ɫ
4	������	�����Ž�ɫ
5	������	�����Ž�ɫ
6	�����	�����Ž�ɫ
7	�����	�����Ž�ɫ
8	ͳ�ƾ�	�����Ž�ɫ
9	����û�	����/�ְ��û�
10	����û�	����/�弶�û�

         */
        /// <summary>
        /// ���ݹ��ܱ����ʾ����
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetSearchByFuncNo(string funcNo, string userID)
        {
            string returnVa = string.Empty;
            if (funcNo.Length > 2)
            {
                //if (funcNo.Substring(0, 4) == "0102")
                //{
                //    //��Ϣ����
                //    returnVa = "FuncNo='" + funcNo + "'";
                //    if (funcNo == "01020208")
                //    {

                //        returnVa = "FuncNo='01010208' AND AnalysisFlag=2 AND CommMemo='����������'";//���ܳ���ͱ�������Ա
                //    }
                //    else if (funcNo == "01020209")
                //    {

                //        returnVa = "FuncNo='01010209' AND AnalysisFlag=2 AND CommMemo='����������'";
                //    }
                //    else if (funcNo == "01020210")
                //    {

                //        returnVa = "FuncNo='01010210' AND AnalysisFlag=2 AND CommMemo='����������'";
                //    }
                //    else
                //    {
                //        // �����û�ֻ���������������
                //        if (m_UserDeptCode.Substring(0, 2) == "03")
                //        {
                //            returnVa = "AnalysisFlag=2 AND FuncNo='0101" + funcNo.Substring(4, 4) + "' AND " + GetXiangZhenFilter(m_UserDeptCode, m_UserDeptName, "AreaName");
                //            //returnVa = "FuncNo='" + funcNo + "' AND " + GetXiangZhenFilter(m_UserDeptCode, m_UserDeptName, "AreaName");
                //        }
                //        else
                //        {
                //            returnVa = "AnalysisFlag=2 AND FuncNo='0101" + funcNo.Substring(4, 4) + "'";
                //            //returnVa = "FuncNo='" + funcNo + "'";
                //        }
                //    }
                //}
                //else if (funcNo.Substring(0, 4) == "0103")
                //{
                //    //��׼���ݷ����
                //    returnVa = GetAreaCodeByFuncNo(funcNo, m_FuncName, m_UserAreaCode, "AreaName");
                //}
                //else if (funcNo.Substring(0, 2) == "02")
                //{
                //    //ҵ��������� �ڵ�����ҵ������Ŀ¼��ʵ��
                //    returnVa = "";
                //}
                //else if (funcNo.Substring(0, 2) == "03")
                //{
                //    //ҵ��������� �ڵ�����ҵ������Ŀ¼��ʵ��
                //    if (funcNo == "0306" || funcNo == "0307" || funcNo == "0308" || funcNo == "0309")
                //    {
                //        if (m_RoleID == "5")
                //        {
                //            returnVa = "FuncNo='" + funcNo + "' and AreaCode='" + m_UserAreaCode + "'";
                //        }
                //        else if (m_RoleID == "4")
                //        {
                //            returnVa = "FuncNo='" + funcNo + "' and AreaCode LIKE '" + m_UserAreaCode.Substring(0, 9) + "%'";
                //        }
                //        else
                //        {
                //            returnVa = "FuncNo='" + funcNo + "'";
                //        }
                //    }
                //    else
                //    {
                //        returnVa = "FuncNo='" + funcNo + "'";
                //    }
                //}
                //else if (funcNo.Substring(0, 2) == "04")
                //{
                //    //������Ϣ
                //    if (m_RoleID == "1")
                //    {
                //        if (funcNo == "0403") { returnVa = " CmsCode LIKE '" + funcNo + "%' AND CmsAttrib!=4"; }
                //        else { returnVa = " CmsCode='" + funcNo + "' AND CmsAttrib!=4"; }
                //    }
                //    else
                //    {
                //        if (funcNo == "0403") { returnVa = " CmsCode LIKE '" + funcNo + "%' AND CmsAttrib!=4"; }
                //        else { returnVa = " CmsCode='" + funcNo + "' AND CmsAttrib!=4"; }
                //        //�����û����������жϲ���Ȩ��
                //        //if (m_UserDeptCode == funcNo)
                //        //{
                //        //    returnVa = " CmsCode='" + funcNo + "' AND CmsAttrib!=4";
                //        //}
                //        //else
                //        //{
                //        //    returnVa = " (CmsCode='" + funcNo + "' AND CmsAttrib=9) OR (CmsCode='" + funcNo + "' AND UserID=" + m_UserID + " AND CmsAttrib!=4) ";
                //        //}
                //    }
                //}
                //else if (funcNo == "xx")
                //{
                //    if (m_UserDeptCode.Substring(0, 2) == "03")
                //    {
                //        //returnVa = "P_Addres LIKE '%" + m_UserDeptCodeName + "%'"; ��׼����
                //        returnVa = GetXiangZhenFilter(m_UserDeptCode, m_UserDeptName, "AreaName");
                //    }
                //    else
                //    {
                //        returnVa = "";
                //    }
                //}
                //else
                //{
                    switch (funcNo)
                    {
                        case "04":  // ������Ϣ CmsAttrib:0 Ĭ��;1 ���; 3 ����; 4 ɾ��; 9 ���� P_Addres
                            returnVa = " CmsCode LIKE '01%' AND CmsAttrib=9";
                            break;
                        case "0403": // ������Ϣ
                            returnVa = " CmsCode LIKE '0403%' AND CmsAttrib=9";
                            break;
                        case "060301": // ������
                            returnVa = " SourceDel=0 AND SourceUserID=" + m_UserID + " "; // (DelFlag<>0 OR DelFlag IS NULL)
                            break;
                        case "060302": // �ռ���
                            returnVa = " TargetDel=0 AND TargetUserID=" + m_UserID + " "; //
                            break;
                        case "060303": // ����վ
                            returnVa = "(SourceUserID=" + m_UserID + " OR TargetUserID=" + m_UserID + ") AND (SourceDel!=0 OR TargetDel!=0)";
                            break;
                        case "060105": // ϵͳ��־
                            returnVa = " OprateModel !='�����޸�'";
                            break;
                        case "060106": // �޸���־
                            returnVa = " OprateModel ='�����޸�'";
                            break;
                        case "0102": // �޸���־
                            returnVa = " ";
                            break;
                        default: // Ĭ����ʾ��ǰ������������OprateModel ='�����޸�'
                            returnVa = GetXlsFilter(funcNo);
                            break;
                    }
               //}
            }
            else { returnVa = ""; }

            return returnVa;
        }

        /// <summary>
        /// ���ݹ��ܺŻ�ȡ�����������룬��������������Ĺ��ܲ˵�
        /// </summary>
        /// <returns></returns>
        private string GetAreaCodeByFuncNo(string funcNo, string funcNa, string deptCode, string keyFiled)
        {
            string returnVa = string.Empty;
            /*
610922100000	�ǹ���
610922101000	�ķ���
610922102000	������
610922103000	ӭ����
610922104000	�غ���
610922105000	������
610922106000	ϲ����
610922107000	�ٶ���
610922108000	����ɽ��
610922109000	��Ϫ��
610922110000	�г���

             */
            switch (funcNo)
            {
                case "0103":
                    returnVa = "";
                    break;
                case "010301":
                    returnVa = "(AreaCode LIKE '610922100%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010302":
                    returnVa = "(AreaCode LIKE '610922101%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010303":
                    returnVa = "(AreaCode LIKE '610922102%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010304":
                    returnVa = "(AreaCode LIKE '610922103%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010305":
                    returnVa = "(AreaCode LIKE '610922104%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010306":
                    returnVa = "(AreaCode LIKE '610922105%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010307":
                    returnVa = "(AreaCode LIKE '610922106%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010308":
                    returnVa = "(AreaCode LIKE '610922107%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010309":
                    returnVa = "(AreaCode LIKE '610922108%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010310":
                    returnVa = "(AreaCode LIKE '610922109%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                case "010311":
                    returnVa = "(AreaCode LIKE '610922110%' OR " + keyFiled + " LIKE '%" + funcNa.Substring(0, 2) + "%' )";
                    break;
                default:
                    returnVa = "";
                    break;
            }
            return returnVa;
        }

        /// <summary>
        /// �����������Ʊ�������� 2013/07/30 by ysl
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="deptName"></param>
        /// <param name="keyFiled"></param>
        /// <returns></returns>
        private string GetXiangZhenFilter(string deptCode, string deptName, string keyFiled)
        {
            //2013/10/10 by Ysl ��ѯ��������ַƥ��
            if (this.m_FuncCode == "020201" || this.m_FuncCode == "0506") { keyFiled = "Fileds14"; }
            else if (this.m_FuncCode == "0502") { keyFiled = "Fileds10"; }
            else if (this.m_FuncCode == "0503" || this.m_FuncCode == "0504" || this.m_FuncCode == "0505") { keyFiled = "Fileds13"; }
            else if (this.m_FuncCode == "0513" || this.m_FuncCode == "0514") { keyFiled = "Fileds05"; }
            else if (this.m_FuncCode == "0515" || this.m_FuncCode == "0516") { keyFiled = "Fileds03"; }
            else { }
            /*
            if (deptCode == "0303")
            {
                return " (" + keyFiled + " LIKE '%��ˮ%' OR " + keyFiled + " LIKE '%��ׯ%') ";
            }
            else if (deptCode == "0304")
            {
                return " (" + keyFiled + " LIKE '%����%' OR " + keyFiled + " LIKE '%����%') ";
            }
            else if (deptCode == "0305")
            {
                return " (" + keyFiled + " LIKE '%����%' OR " + keyFiled + " LIKE '%���%') ";
            }
            else if (deptCode == "0308")
            {
                return " (" + keyFiled + " LIKE '%��֦%' OR " + keyFiled + " LIKE '%�ϼ�%') ";
            }
            else
            {
                if (deptName.Length > 3) { return " ( " + join.pms.web.Biz.CommPage.GetAreaMatch(m_AreaNo, m_AreaVal, m_FuncCode, deptName) + " OR " + keyFiled + " LIKE '%" + deptName.Substring(0, 3) + "%' ) "; }
                else { return " (" + join.pms.web.Biz.CommPage.GetAreaMatch(m_AreaNo, m_AreaVal, m_FuncCode, deptName) + " OR " + keyFiled + " LIKE '%" + deptName + "%' ) "; }
            }*/

            if (deptName.Length > 3) { return " ( " + join.pms.dal.CommPage.GetAreaMatch(m_AreaNo, m_AreaVal, m_FuncCode, deptName) + " OR " + keyFiled + " LIKE '%" + deptName.Substring(0, 3) + "%' ) "; }
            else { return " (" + join.pms.dal.CommPage.GetAreaMatch(m_AreaNo, m_AreaVal, m_FuncCode, deptName) + " OR " + keyFiled + " LIKE '%" + deptName + "%' ) "; }

        }

        private string GetXlsFilter(string funcNo)
        {

            if (funcNo.Substring(0, 2) == "01")
            {
                if (m_RoleID == "1")
                {
                    //return " FuncNo='" + funcNo + "' ";
                    return "";
                }
                else
                {
                    //�����û����������жϲ���Ȩ�� Fileds15
                    if (m_UserDeptCode == funcNo.Substring(0, 4))
                    {
                        if (m_UserDeptCode.Substring(0, 2) == "03")
                        {
                            return " FuncNo='" + funcNo + "' AND (AreaCode LIKE '" + m_UserAreaCode.Substring(0, 9) + "%' OR " + GetXiangZhenFilter(m_UserDeptCode, m_UserDeptName, "AreaName") + ")  ";
                        }
                        else
                        {
                            return " FuncNo='" + funcNo + "' ";
                        }
                    }
                    else
                    {
                        if (m_UserDeptCode.Substring(0, 2) == "03")
                        {
                            return " FuncNo='" + funcNo + "' AND (AreaCode LIKE '" + m_UserAreaCode.Substring(0, 9) + "%' OR " + GetXiangZhenFilter(m_UserDeptCode, m_UserDeptName, "AreaName") + ") ";
                        }
                        else
                        {
                            return " FuncNo='" + funcNo + "'";// return " FuncNo='" + funcNo + "' AND AuditFlag=9 ";
                        }
                    }
                }
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// �ӹ��ܱ����ȡCMS����
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetCmsCode(string funcNo)
        {
            string returnVa = string.Empty;
            if (!string.IsNullOrEmpty(funcNo) && funcNo.Length > 2)
            {
                // 010402	���ݷ���
                if (funcNo.Substring(0, 2) == "04") returnVa = "04";// funcNo.Replace("0104", "");
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
                    case "range_all": // ���пɼ���
                        returnVa = "ClassCode LIKE '" + m_FuncCode + "__'";
                        break;
                    case "range_mark": // �Ҹ����
                        returnVa = "ParaCate=11";
                        break;
                    case "range_creator": // �Ҵ�����
                        returnVa = "ForumCode LIKE '" + m_FuncCode + "%'";
                        break;
                    case "range_underling": // ������
                        returnVa = "ForumCode LIKE '" + m_FuncCode + "%'";
                        break;
                    case "range_dept": // �Ҳ��ŵ�
                        returnVa = "ForumCode LIKE '" + m_FuncCode + "%'";
                        break;
                    case "range_share": // ������ҵ�
                        returnVa = "ForumCode LIKE '" + m_FuncCode + "%'";
                        break;
                    default:
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

