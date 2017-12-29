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

namespace AreWeb.OnlineCertificate.PopInfo
{
    public partial class popView : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_FuncNa;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_SqlParams;
        public string m_TargetUrl;

        DataSet m_Ds;
        private string m_FuncInfo;
        private string m_Titles;
        private string m_Fields;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                SetOpratetionAction("");
                ShowInfo(m_ObjID);
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
            string ona = Request.QueryString["oNa"];
            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                if (m_FuncCode.Length > 2) m_FuncCode = "04";
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
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; �鿴������Ϣ ��";
        }
        /// <summary 
        /// �޸�
        /// </summary>
        /// <param name="objID"></param>
        private void ShowInfo(string objID)
        {
            string errMsg = string.Empty;
            string configFile = Server.MapPath("/includes/DataGrid.config");
            string P_CardID = string.Empty;
            SqlDataReader sdr = null;
            StringBuilder sHtml = new StringBuilder();
            sHtml.Append("��׼�����ִ�ĸ�����Ϣ���£�<br/>");
            sHtml.Append("<table width=\"1024\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" bgcolor=\"#008981\">");
            try
            {
                GetConfigParams(this.m_FuncCode, configFile, ref errMsg);
                string[] a_FuncInfo = this.m_FuncInfo.Split(',');
                string[] a_Titles = this.m_Titles.Split(',');
                string[] a_Fields = this.m_Fields.Split(',');

                
                sHtml.Append("<tr>");
                for (int i = 0; i < 14; i++)
                {
                    sHtml.Append("<td class=\"fb01\" bgcolor=\"#99d0d0\" style=\"color:#033;\"><strong>" + a_Titles[i] + "</strong></td>");
                }
                sHtml.Append("</tr>");
                m_SqlParams = "SELECT * FROM PIS_Persons WHERE P_ID=" + objID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    P_CardID = PageValidate.GetTrim(sdr["P_CardID"].ToString());
                    sHtml.Append("<tr onmouseover=\"this.className='lvtColDataHover'\" onmouseout=\"this.className='lvtColData'\">");
                    for (int j = 0; j < 14; j++) {

                        sHtml.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + PageValidate.GetTrim(sdr[a_Fields[j]].ToString()) + "</td>");
                    }
                    sHtml.Append("</tr>");
                }
                
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }
            sHtml.Append("</table><br/>");
            /*
----------------------------------
�ɹ�����CID��Ϣ:
------------------------------------------
010101	���ƾ�
01010101	ȫԱ�˿ڸ���������Ϣ FILEDS04

01010118	�ر���������������� Fileds02
01010119	��Ů������������������ Fileds02
01010120	���ҽ���������������  Fileds02
             * 
01010111	����Ӥ��ʵ���Ǽ���Ϣ FILEDS03 , FILEDS10
01010112	����ҽѧ֤��������Ϣ FILEDS10 ,FILEDS15
01010113	��ͯ�ƻ����ߵǼ���Ϣ Fileds08,Fileds13
01010114	���񽡿�����������Ϣ Fileds03,
             * 
01010132	����ҽ�ƶ������ Fileds02
01010134	��ͯ����������Ϣ Fileds02
01010135	�в�������������Ϣ Fileds02
01010136	��ǰ����ҵ����Ϣ   Fileds06,Fileds09
010102	������
01010201	�����Ǽ���Ϣ Fileds05,Fileds13
01010202	������Ů��Ϣ Fileds02,Fileds04
01010205	���ܳ���ͱ���Ա��Ϣ Fileds02
01010206	����ũ��ͱ���Ա��Ϣ Fileds02
01010207	����ũ���屣��Ա��Ϣ Fileds02
010103	������
01010301	��ס�˿ڸ���������Ϣ Fileds05
01010302	�������仧��Ϣ Fileds01,Fileds08,Fileds10
01010303	Ǩ����Ա��Ϣ Fileds01
01010304	Ǩ����Ա��Ϣ Fileds01
010104	�����
01010406	��ҵ�ٽ���-��ҵ������Ա�籣������Ϣ Fileds04
01010408	��ҵ�ٽ���-����ʡ��ҵ����ҵ����ѵ��Ա�Ǽ���Ϣ Fileds08
01010410	���ϰ�-������ҵ��λ��ְ��Ա������ϲα���Ϣ Fileds03
01010411	������ҵ��λ������Աһ���Ը�����ɥ���������Ϣ Fileds11
01010412	����ҽ����-�α���Ա�Ǽ���Ϣ Fileds02
01010413	ʧҵ���չ���칫��-ְ��ʧҵ���ղα���Ϣ Fileds03
01010415	ũ����-��ũ��ת�� Fileds01
01010416	ũ����-�������������ϱ���ɥ�Ჹ���𷢷ű� Fileds03
010105	�����
01010501	ѧ����ѧ�Ǽ���Ϣ Fileds03
             */
            //����ȫԱ��Ϣ
            sHtml.Append(GetFuncInfo("01010101", P_CardID, "Fileds04='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010118", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010119", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010120", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            //������Ϣ
            sHtml.Append(GetFuncInfo("01010111", P_CardID, "(Fileds03='" + P_CardID + "' OR Fileds10='" + P_CardID + "')", configFile));
            sHtml.Append(GetFuncInfo("01010112", P_CardID, "(Fileds10='" + P_CardID + "' OR Fileds15='" + P_CardID + "')", configFile));
            sHtml.Append(GetFuncInfo("01010113", P_CardID, "(Fileds08='" + P_CardID + "' OR Fileds13='" + P_CardID + "')", configFile));
            sHtml.Append(GetFuncInfo("01010114", P_CardID, "Fileds03='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010132", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010134", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010135", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010136", P_CardID, "(Fileds06='" + P_CardID + "' OR Fileds09='" + P_CardID + "')", configFile));
            //����������Ϣ
            sHtml.Append(GetFuncInfo("01010201", P_CardID, "(Fileds05='" + P_CardID + "' OR Fileds13='" + P_CardID + "')", configFile));
            sHtml.Append(GetFuncInfo("01010202", P_CardID, "(Fileds02='" + P_CardID + "' OR Fileds04='" + P_CardID + "')", configFile));
            sHtml.Append(GetFuncInfo("01010205", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010206", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010207", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            //����������Ϣ
            sHtml.Append(GetFuncInfo("01010301", P_CardID, "Fileds05='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010302", P_CardID, "(Fileds01='" + P_CardID + "' OR Fileds08='" + P_CardID + "' OR Fileds10='" + P_CardID + "')", configFile));
            sHtml.Append(GetFuncInfo("01010303", P_CardID, "Fileds01='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010304", P_CardID, "Fileds01='" + P_CardID + "'", configFile));
            //���粿����Ϣ
            sHtml.Append(GetFuncInfo("01010406", P_CardID, "Fileds04='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010408", P_CardID, "Fileds08='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010410", P_CardID, "Fileds03='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010411", P_CardID, "Fileds11='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010412", P_CardID, "Fileds02='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010413", P_CardID, "Fileds03='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010415", P_CardID, "Fileds01='" + P_CardID + "'", configFile));
            sHtml.Append(GetFuncInfo("01010416", P_CardID, "Fileds03='" + P_CardID + "'", configFile));
            //�����
            sHtml.Append(GetFuncInfo("01010501", P_CardID, "Fileds03='" + P_CardID + "'", configFile));
            this.LiteralData.Text = sHtml.ToString();
        }

        /// <summary>
        /// ������Ϣ�Ĺ�����Ϣ
        /// </summary>
        /// <param name="funcNo"></param>
        /// <returns></returns>
        private string GetFuncInfo(string funcNo, string CID, string filterSQL, string configFile)
        {
            string errMsg = string.Empty;
            SqlDataReader sdr = null;
            StringBuilder sHtml = new StringBuilder();
            try
            {
                GetConfigParams(funcNo, configFile, ref errMsg);
                string[] a_FuncInfo = this.m_FuncInfo.Split(',');
                string[] a_Titles = this.m_Titles.Split(',');
                string[] a_Fields = this.m_Fields.Split(',');


                m_SqlParams = "SELECT * FROM " + a_FuncInfo[0] + " WHERE FuncNo='" + funcNo + "' AND " + filterSQL + " ";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows) {
                    sHtml.Append(a_FuncInfo[2] + " ���£�<br/>");
                    sHtml.Append("<table width=\"1024\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" bgcolor=\"#008981\">");
                    sHtml.Append("<tr>");
                    for (int i = 0; i < a_Fields.Length - 1; i++)
                    {
                        sHtml.Append("<td class=\"fb01\" bgcolor=\"#99d0d0\" style=\"color:#033;\">&nbsp;<strong>" + a_Titles[i] + "</strong></td>");
                    }
                    sHtml.Append("</tr>");
                    while (sdr.Read())
                    {
                        sHtml.Append("<tr onmouseover=\"this.className='lvtColDataHover'\" onmouseout=\"this.className='lvtColData'\">");
                        for (int j = 0; j < a_Fields.Length - 1; j++)
                        {

                            sHtml.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + PageValidate.GetTrim(sdr[a_Fields[j]].ToString()) + "</td>");
                        }
                        sHtml.Append("</tr>");
                    }
                    sHtml.Append("</table><br/>");
                } 
                else { 
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            return sHtml.ToString();
        }

        

        #endregion
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



