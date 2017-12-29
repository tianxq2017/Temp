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

using UNV.Comm.Web;
using UNV.Comm.DataBase;
using join.pms.dal;

namespace join.pms.wap.UserCenter
{
    public partial class DataList : UNV.Comm.Web.PageBase
    {
        private string m_UserID;
        private string m_PersonCardID;
        private string m_PersonTel;

        private string m_FuncCode;
        private string m_FuncName;
        private string m_PageNo;
        private string m_UrlParams;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateParams();
            AuthenticateUser();

            this.Uc_PageTop1.GetSysMenu("�����б�");
            if (!IsPostBack)
            {
                GetDataGrid(m_FuncCode, m_PageNo);
            }
        }

        #region ����ҳͷ��Ϣ������\��֤����\��֤�û���
        /// <summary>
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_FuncCode = PageValidate.GetTrim(Request.QueryString["c"]);
            m_PageNo = PageValidate.GetTrim(Request.QueryString["p"]);
            if (string.IsNullOrEmpty(m_PageNo)) m_PageNo = "1"; // ҳ��Ĭ��Ϊ��һҳ
            if (!string.IsNullOrEmpty(m_FuncCode) && PageValidate.IsNumber(m_PageNo))
            {
                m_FuncName = GetFuncName(m_FuncCode);
                m_UrlParams = "/OC/" + m_FuncCode + "-" + m_PageNo + "." + m_FileExt;
                this.txtUrlPageNo.Value = m_PageNo;//���ֲ������ҳ��
            }
            else
            {
                Server.Transfer("/errors.aspx");
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
                if (Session["UserID"] != null && !String.IsNullOrEmpty(Session["PersonID"].ToString())) { returnVa = true; m_UserID = Session["PersonID"].ToString(); }
            }
            if (!returnVa)
            {
                Response.Write("<script language='javascript'>alert(\"������ʾ�����¼�����ԣ�\");parent.location.href='/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh." + m_FileExt + "';</script>");
                Response.End();
            }
            else
            {
                GetUserInfo();
            }
        }
        #endregion

        #region ��ȡ��ǰ�û����֤�ź��ֻ���
        /// <summary>
        /// ��ȡ��ǰ�û����֤�ź��ֻ���
        /// </summary>
        private void GetUserInfo()
        {
            SqlDataReader sdr = null;
            string sqlParams = "SELECT PersonTel,PersonCardID FROM BIZ_Persons WHERE PersonID=" + this.m_UserID;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        this.m_PersonCardID = sdr["PersonCardID"].ToString();
                        this.m_PersonTel = sdr["PersonTel"].ToString();
                    }
                }
            }
            catch { if (sdr != null) { sdr.Close(); sdr.Dispose(); } }
        }
        #endregion

        #region ���ݹ��ܱ�Ż�ȡ��������
        /// <summary>
        /// ���ݹ��ܱ�Ż�ȡ��������
        /// </summary>
        /// <returns></returns>
        private string GetFuncName(string funcCode)
        {
            string returnVal = string.Empty;
            switch (funcCode)
            {
                case "02":
                    returnVal = "ҵ������¼";
                    break;
                case "04":
                    returnVal = "ҵ��������Ϣ";
                    break;
                case "03":
                    returnVal = "�����ղ�";
                    break;
                case "0502":
                    returnVal = "����֤����Ϣ";
                    break;
                case "0503":
                    returnVal = "���ϸ���";
                    break;
                case "0601":
                    returnVal = "ҵ����ѯ";
                    break;
                case "0602":
                    returnVal = "����Ͷ��";
                    break;
                case "0603":
                    returnVal = "������";
                    break;
                case "0604":
                    returnVal = "��������";
                    break;
                case "07":
                    returnVal = "������Ů��Ϣ";
                    break;
                case "08":
                    returnVal = "���˻���ʷ";
                    break;
                default:
                    returnVal = "δָ����Ŀ����";
                    break;
            }
            return returnVal;
        }
        #endregion

        #region ��ȡ�����б������б��������ʾ��
        /// <summary>
        /// ��ȡ�����б������б��������ʾ��        
        /// </summary>
        /// <param name="funcNo"></param>
        /// <param name="pageNo"></param>
        private void GetDataGrid(string funcNo, string pageNo)
        {
            string errorMsg = string.Empty;
            string searchSQL = string.Empty;
            string configFile = Server.MapPath(GetConfigFileName(funcNo));
            string pageSize = System.Configuration.ConfigurationManager.AppSettings["GridPageSize"]; // ÿҳ��ʾ�ļ�¼��

            if (String.IsNullOrEmpty(funcNo))
            {
                LiteralDataList.Text = "����ʧ�ܣ���������!";
                return;
            }

            searchSQL = GetSearchByFuncNo(funcNo, this.m_UserID);

            DataListForUserWap pageList = new DataListForUserWap();

            if (pageList.GetConfigParams(GetListCode(funcNo), configFile, ref errorMsg))
            {
                try
                {
                    pageList.PageSize = int.Parse(pageSize);
                    pageList.PageNo = int.Parse(pageNo);
                    pageList.FuncNo = funcNo;
                    pageList.FuncName = m_FuncName;
                    pageList.FileExt = this.m_FileExt;
                    pageList.OrderType = 1;
                    pageList.SearchWhere = searchSQL;
                    pageList.Url = "/OC/" + m_FuncCode + "-";

                    // ��ҳ����
                    LiteralDataList.Text = pageList.GetList();
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
                returnVa = "/includes/DataGridUser.config";
            }
            else
            {
                Server.Transfer("/errors.aspx");
            }
            return returnVa;
        }

        /// <summary>
        /// ͨ��CMS���ݷ���ϵͳ�ı��� 01
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetListCode(string funcNo)
        {
            string returnVa = string.Empty;
            if (funcNo.Length > 2)
            {
                if (funcNo.Substring(0, 2) == "06")
                {
                    returnVa = "NoteList";
                }
                else if (funcNo.Substring(0, 2) == "05")
                {
                    returnVa = "BizDocs";
                }
                else { returnVa = funcNo; }
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
            string fFuncNo = string.Empty;
            if (funcNo.Substring(0, 2) == "06") { fFuncNo = GetListCode(funcNo); }
            else { fFuncNo = funcNo; }
            switch (fFuncNo)
            {
                case "NoteList"://ҵ����ѯ ����Ͷ�� ������ ��������
                    returnVa = "MsgCode='" + funcNo + "' AND Attribs!=4 AND MsgUserID=" + userID;
                    break;
                case "02"://ҵ������¼
                    returnVa = "BizCode!='0106' AND BizCode!='0111' AND ((PersonID=" + m_UserID + " AND InitDirection=0) OR PersonCidA='" + m_PersonCardID + "' OR PersonCidB='" + m_PersonCardID + "') AND  Attribs!=4  ";
                    break;
                case "04"://ҵ������
                    returnVa = "CellNumber='" + m_PersonTel + "'";
                    break;
                case "03"://�����ղ�
                    returnVa = "PersonID=" + m_UserID;
                    break;
                case "0502"://����֤����Ϣ
                    returnVa = "DocsType IN('.jpg','.gif','.bmp','.png') AND PersonID=" + m_UserID;
                    break;
                case "0503"://���ϸ���
                    returnVa = "DocsType IN('.doc','.docx','.xls','.xlsx') AND PersonID=" + m_UserID;
                    break;
                case "07"://��Ů��Ϣ
                    returnVa = "PersonID=" + m_UserID;
                    break;
                case "08"://������Ϣ
                    returnVa = "PersonID=" + m_UserID;
                    break;
                default:
                    returnVa = "";
                    break;
            }

            return returnVa;
        }
        #endregion
    }
}
