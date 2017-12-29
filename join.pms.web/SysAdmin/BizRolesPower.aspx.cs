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

using UNV.Comm.DataBase;
using UNV.Comm.Web;

using System.Text;

namespace join.pms.web.SysAdmin
{
    public partial class BizRolesPower : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_ActionName;
        private string m_ObjID;
        private string m_SourceUrlDec;
        private string m_FuncCode;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_SqlParams;
        private DataTable m_PowerDt;
        protected string m_TargetUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetOpratetionAction("��ɫȨ�޷���");
                InitializeFuncTable();
            }
        }

        #region �����֤��������Ϊ����

        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
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
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ�����佹ɫȨ�޲���ÿ��ֻ��ѡ��һ����ɫ�������Զ�ѡ��", m_TargetUrl, true);
                    return;
                }
            }
            switch (m_ActionName)
            {
                case "add": // ����
                    funcName = "����";
                    break;
                case "2": // �༭
                    funcName = "ҵ�����Ȩ�޷���";
                    //ShowModInfo(m_ObjID);
                    break;
                case "del": // ɾ��
                    funcName = "ɾ������";
                    //DelInfo(m_ObjID);
                    break;
                case "view": // �鿴
                    funcName = "�鿴����";
                    break;
                case "5": // ���
                    funcName = "�������";
                    break;
                case "6": // �Ƽ�
                    funcName = "����Ȩ��";
                    //recommend(m_ObjID);
                    // Response.Redirect(txtUrlParams.Value);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
            this.LiteralRoleName.Text = "��ǰ��ɫ����" + DbHelperSQL.GetSingle("SELECT RoleName FROM SYS_Roles WHERE RoleID=" + m_ObjID).ToString();
        }

        #endregion

        #region ��ʼ��Ȩ��
        /// <summary>
        /// ��ʼ�������б�
        /// </summary>
        private void InitializeFuncTable()
        {
            string FuncCode = string.Empty;
            string FuncUrl = string.Empty;
            string FuncName = string.Empty;
            string bgColor = string.Empty;
            bool hasPower = false;
            string funcPowers = string.Empty;
            string[] aryConfig = null;
            string[] aryPowers = null;
            StringBuilder sHtml = new StringBuilder();
            try {
                m_SqlParams = "SELECT BizCode,BizUrl,(CASE LEN(BizCode) WHEN 2 THEN '|--'+BizNameAB WHEN 4 THEN '|--+--'+BizNameAB WHEN 6 THEN '|--+--+--'+BizNameAB ELSE '-' END) AS BizName FROM BIZ_Categories WHERE Attribs=0 AND BizType=0 ORDER BY BizCode";
                m_PowerDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (m_PowerDt.Rows.Count > 0)
                {
                    GetConfigData(); // ��ȡ�����ļ�����
                    sHtml.Append("<table width=\"960\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\"><tr bgcolor=\"#D7F2E3\" class=\"fb01\">");
                    sHtml.Append("<td height=\"30\"><input name=\"selectAll\" type=\"checkbox\" onclick=\"SelectAllFunc()\" /></td>");
                    sHtml.Append("<td width=\"250\"><strong>ģ������/����</strong></td>");
                    sHtml.Append("<td><input name=\"selectAll0\" type=\"checkbox\" onclick=\"SelectColsFunc('0')\" /><strong>ȫѡ</strong></td>");
                    sHtml.Append("<td><input name=\"selectAll1\" type=\"checkbox\" onclick=\"SelectColsFunc('1')\" /><strong>ȫѡ</strong></td>");
                    sHtml.Append("<td><input name=\"selectAll2\" type=\"checkbox\" onclick=\"SelectColsFunc('2')\" /><strong>ȫѡ</strong></td>");
                    sHtml.Append("<td><input name=\"selectAll3\" type=\"checkbox\" onclick=\"SelectColsFunc('3')\" /><strong>ȫѡ</strong></td>");
                    sHtml.Append("<td><input name=\"selectAll4\" type=\"checkbox\" onclick=\"SelectColsFunc('4')\" /><strong>ȫѡ</strong></td>");
                    sHtml.Append("<td><input name=\"selectAll5\" type=\"checkbox\" onclick=\"SelectColsFunc('5')\" /><strong>ȫѡ</strong></td>");
                    sHtml.Append("<td><input name=\"selectAll6\" type=\"checkbox\" onclick=\"SelectColsFunc('6')\" /><strong>ȫѡ</strong></td>");
                    sHtml.Append("<td><input name=\"selectAll7\" type=\"checkbox\" onclick=\"SelectColsFunc('7')\" /><strong>ȫѡ</strong></td>");
                    sHtml.Append("<td><input name=\"selectAll8\" type=\"checkbox\" onclick=\"SelectColsFunc('8')\" /><strong>ȫѡ</strong></td>");
                    sHtml.Append("<td><input name=\"selectAll9\" type=\"checkbox\" onclick=\"SelectColsFunc('9')\" /><strong>ȫѡ</strong></td>");
                    sHtml.Append("<td><input name=\"selectAll10\" type=\"checkbox\" onclick=\"SelectColsFunc('10')\" /><strong>ȫѡ</strong></td>");
                    sHtml.Append("<td><input name=\"selectAll11\" type=\"checkbox\" onclick=\"SelectColsFunc('11')\" /><strong>ȫѡ</strong></td>");
                    sHtml.Append("<td><input name=\"selectAll12\" type=\"checkbox\" onclick=\"SelectColsFunc('12')\" /><strong>ȫѡ</strong></td>");
                    sHtml.Append("</tr>");
                    for (int i = 0; i < m_PowerDt.Rows.Count; i++)
                    {
                        if (i % 2 == 0) { bgColor = "#F2F8F8"; } else { bgColor = "#EDEFEC"; }
                        FuncCode = PageValidate.GetTrim(m_PowerDt.Rows[i]["BizCode"].ToString());
                        FuncUrl = PageValidate.GetTrim(m_PowerDt.Rows[i]["BizUrl"].ToString());
                        FuncName = PageValidate.GetTrim(m_PowerDt.Rows[i]["BizName"].ToString());
                        aryConfig = GetConfigParams(FuncCode); // ��ȡ�ù��ܵ����ò��� ���ӵ�12��
                        hasPower = IsPowerExist(FuncCode, ref funcPowers);
                        sHtml.Append("<tr bgcolor=\"" + bgColor + "\" onmouseenter=\"this.style.backgroundColor='#CCFF99'\"  onmouseout=\"this.style.backgroundColor='#FFFFFF'\">");

                        if (hasPower)
                        {
                            aryPowers = funcPowers.Split(',');
                            sHtml.Append("<td><input name=\"cbxSel\" type=\"checkbox\" value=\"" + FuncCode + "\" checked=\"checked\" /></td>");
                        }
                        else { sHtml.Append("<td><input name=\"cbxSel\" type=\"checkbox\" value=\"" + FuncCode + "\" /></td>"); }

                        sHtml.Append("<td>" + FuncName + "��" + FuncCode + "��</td>");
                        // ���ܲ���
                        for (int j = 0; j < 13; j++)
                        {
                            if (hasPower)
                            {
                                if (string.IsNullOrEmpty(FuncUrl))
                                {
                                    if (aryPowers[j] == "1") { sHtml.Append("<td></td>"); }
                                    else { sHtml.Append("<td></td>"); }
                                }
                                else
                                {
                                    if (aryPowers[j] == "1") { sHtml.Append("<td><input name=\"cbx" + j + "\" type=\"checkbox\" value=\"" + FuncCode + "\" checked=\"checked\" />" + aryConfig[j] + "</td>"); }
                                    else { sHtml.Append("<td><input name=\"cbx" + j + "\" type=\"checkbox\" value=\"" + FuncCode + "\" />" + aryConfig[j] + "</td>"); }
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(FuncUrl)) { sHtml.Append("<td></td>"); }
                                else { sHtml.Append("<td><input name=\"cbx" + j + "\" type=\"checkbox\" value=\"" + FuncCode + "\" />" + aryConfig[j] + "</td>"); }
                            }
                        }
                        sHtml.Append("</tr>");
                    }
                    sHtml.Append("</table>");
                }
                m_Ds = null;
                m_PowerDt = null;
            }
            catch (Exception ex) { 
                sHtml.Append(ex.Message); 
            }
            

            this.LiteralFuncTable.Text = sHtml.ToString();
        }

        private DataSet m_Ds;
        private void ConfigDataSet()
        {
            m_Ds = new DataSet();
            m_Ds.Locale = System.Globalization.CultureInfo.InvariantCulture;
        }
        /// <summary>
        /// ��ȡ�����ļ���Ϣ
        /// </summary>
        private void GetConfigData()
        {
            try
            {
                ConfigDataSet();
                string configFile = Server.MapPath("/includes/DataGridBiz.Config");
                m_Ds.ReadXml(configFile, XmlReadMode.ReadSchema);
            }
            catch (Exception ex)
            {
                //errorMsg = "��ȡ�����ļ�����ʧ�ܣ�" + ex.Message;
            }
        }
        /// <summary>
        /// ��ȡ�����ļ�����
        /// </summary>
        /// <param name="funcNo">���ܺ�</param>
        /// <param name="configFile">�����ļ�·��</param>
        /// <returns></returns>
        private string[] GetConfigParams(string funcNo)
        {
            string[] aryNames = new string[13];
            try
            {
                DataRow[] dr = m_Ds.Tables[0].Select("FuncNo='" + funcNo + "'");
                if (dr.Length != 0)
                {
                    aryNames[0] = "����";
                    aryNames[1] = "�޸�";
                    aryNames[2] = "���";
                    aryNames[3] = "�鿴";
                    string[] aryVisible = dr[0][13].ToString().Split(',');
                    string[] aryButNames = dr[0][11].ToString().Split(',');
                    for (int i = 0; i < aryVisible.Length; i++)
                    {
                        //if (aryVisible[i] != "1") { aryNames[i + 4] = ""; }
                        //else { aryNames[i + 4] = aryButNames[i]; }
                        aryNames[i + 4] = aryButNames[i];
                    }
                }
            }
            catch (Exception ex)
            {
                //errorMsg = "��ȡ�����ļ�����ʧ�ܣ�" + ex.Message;
            }
            return aryNames;
        }

        /// <summary>
        /// �ж��û��Ƿ��Ѿ������ɫ
        /// </summary>
        /// <param name="funcCode"></param>
        /// <returns></returns>
        private bool IsPowerExist(string funcCode, ref string funcPowers)
        {
            bool returnVal = false;
            // SELECT [CommID], [RoleID], [BizCode], [BizPowers] FROM [BIZ_RolesPower]
            // "0,0,0,0,0,0,0,0,0"
            m_SqlParams = "select BizCode,BizPowers From BIZ_RolesPower where RoleID=" + m_ObjID;
            DataTable dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    funcPowers = dt.Rows[i]["BizPowers"].ToString();
                    if (funcCode == dt.Rows[i]["BizCode"].ToString())
                    {
                        returnVal = true;
                        break;
                    }
                }
            }
            return returnVal;
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string returnMsg = string.Empty;
            if (!String.IsNullOrEmpty(m_ObjID))
            {
                string cbxSelect = PageValidate.GetTrim(Request["cbxSel"]);
                string cbx0 = PageValidate.GetTrim(Request["cbx0"]);
                string cbx1 = PageValidate.GetTrim(Request["cbx1"]);
                string cbx2 = PageValidate.GetTrim(Request["cbx2"]);
                string cbx3 = PageValidate.GetTrim(Request["cbx3"]);
                string cbx4 = PageValidate.GetTrim(Request["cbx4"]);
                string cbx5 = PageValidate.GetTrim(Request["cbx5"]);
                string cbx6 = PageValidate.GetTrim(Request["cbx6"]);
                string cbx7 = PageValidate.GetTrim(Request["cbx7"]);
                string cbx8 = PageValidate.GetTrim(Request["cbx8"]);
                string cbx9 = PageValidate.GetTrim(Request["cbx9"]);
                string cbx10 = PageValidate.GetTrim(Request["cbx10"]);
                string cbx11 = PageValidate.GetTrim(Request["cbx11"]);
                string cbx12 = PageValidate.GetTrim(Request["cbx12"]);

                if (!string.IsNullOrEmpty(cbxSelect))
                {
                    try 
                    {
                        // ����֮ǰɾ���Ѵ��ڵ���������
                        m_SqlParams = "DELETE FROM BIZ_RolesPower WHERE RoleID=" + m_ObjID;
                        DbHelperSQL.ExecuteSql(m_SqlParams);

                        string[] aryFunc = cbxSelect.Split(',');
                        string funcCode = string.Empty;
                        string funcPowers = string.Empty;
                        for (int i = 0; i < aryFunc.Length; i++)
                        {
                            funcCode = aryFunc[i];
                            funcPowers = "";
                            funcPowers += GetFuncPowers(cbx0, funcCode) + ",";
                            funcPowers += GetFuncPowers(cbx1, funcCode) + ",";
                            funcPowers += GetFuncPowers(cbx2, funcCode) + ",";
                            funcPowers += GetFuncPowers(cbx3, funcCode) + ",";
                            funcPowers += GetFuncPowers(cbx4, funcCode) + ",";
                            funcPowers += GetFuncPowers(cbx5, funcCode) + ",";
                            funcPowers += GetFuncPowers(cbx6, funcCode) + ",";
                            funcPowers += GetFuncPowers(cbx7, funcCode) + ",";
                            funcPowers += GetFuncPowers(cbx8, funcCode) + ",";
                            funcPowers += GetFuncPowers(cbx9, funcCode) + ",";
                            funcPowers += GetFuncPowers(cbx10, funcCode) + ",";
                            funcPowers += GetFuncPowers(cbx11, funcCode) + ",";
                            funcPowers += GetFuncPowers(cbx12, funcCode);
                            SetRolesPower(funcCode, funcPowers);
                            returnMsg = "�����ɹ�������ѡ���Ȩ�ޱ��ɹ����õ���ɫ��";
                        }
                    }
                    catch(Exception ex){
                        returnMsg = ex.Message;
                    }
                }
                else
                {
                    returnMsg = "����ʧ�ܣ���ѡ��������Ȩ�ޣ�";
                }

                // ˢ��ҳ������
                InitializeFuncTable();
            }
            else { returnMsg = "����ʧ�ܣ�������ʧ��"; }

            MessageBox.Show(this, returnMsg);
        }

        /// <summary>
        /// ��ȡѡ�еĽڵ㣬����ɫ����Ȩ��
        /// </summary>
        /// <param name="tnc"></param>
        private void SetRolesPower(string funcCode, string funcPowers)
        {
            if (!string.IsNullOrEmpty(funcCode))
            {
                // SELECT [CommID], [RoleID], [BizCode], [BizPowers] FROM [BIZ_RolesPower]
                m_SqlParams = "INSERT INTO BIZ_RolesPower(RoleID,BizCode,BizPowers) VALUES(" + m_ObjID + ",'" + funcCode + "','" + funcPowers + "')";
                DbHelperSQL.ExecuteSql(m_SqlParams);
            }
        }

        /// <summary>
        /// ���ݹ��ܺŻ�ȡ���õĶ�ӦȨ��
        /// </summary>
        /// <param name="keyValues"></param>
        /// <param name="funcCode"></param>
        /// <returns></returns>
        private string GetFuncPowers(string keyValues, string funcCode)
        {
            string returnVa = "0";
            if (!string.IsNullOrEmpty(keyValues))
            {
                string[] aryKey = keyValues.Split(',');
                for (int i = 0; i < aryKey.Length; i++)
                {
                    if (aryKey[i] == funcCode)
                    {
                        returnVa = "1";
                        break;
                    }
                }
            }
            return returnVa;
        }
    }
}