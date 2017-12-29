using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Web;
using UNV.Comm.DataBase;

namespace join.pms.dal
{
    public class DataListForUser
    {

        #region  PageData ��Ա����
        // ����
        private int _OrderType;
        public int OrderType
        {
            set { _OrderType = value; }
            get { return _OrderType; }
        }
        private string _OrderField;
        public string OrderField
        {
            set { _OrderField = value; }
            get { return _OrderField; }
        }
        // ҳ��ַ���� TabPage
        private string _Url;
        public string Url
        {
            set { _Url = value; }
            get { return _Url; }
        }
        // ��������
        private string _SearchWhere;
        public string SearchWhere
        {
            set { _SearchWhere = value; }
            get { return _SearchWhere; }
        }
        // =======================================
        // ����
        private string _TableName;
        public string TableName
        {
            set { _TableName = value; }
            get { return _TableName; }
        }
        // �����ֶ���
        private string _PrimaryKey;
        public string PrimaryKey
        {
            set { _PrimaryKey = value; }
            get { return _PrimaryKey; }
        }
        // ÿҳ��ʾ�ļ�¼��
        private int _PageSize;
        public int PageSize
        {
            set { _PageSize = value; }
            get { return _PageSize; }
        }
        // ҳ��
        private int _PageNo;
        public int PageNo
        {
            set { _PageNo = value; }
            get { return _PageNo; }
        }
        // �ܼ�¼��
        private int _TotalRec;
        public int TotalRec
        {
            set { _TotalRec = value; }
            get { return _TotalRec; }
        }
        // =======================================
        // ���ܱ��
        private string _FuncNo;
        public string FuncNo
        {
            set { _FuncNo = value; }
            get { return _FuncNo; }
        }
        // ��������
        private string _FuncName;
        public string FuncName
        {
            set { _FuncName = value; }
            get { return _FuncName; }
        }
        // ��չ��
        private string _FileExt;
        public string FileExt
        {
            set { _FileExt = value; }
            get { return _FileExt; }
        }
        // ������Ϣ
        private string m_FuncInfo;
        // ����
        private string m_Titles;
        // �ֶ�
        private string m_Fields;
        public string FieldsName
        {
            set { m_Fields = value; }
            get { return m_Fields; }
        }
        // �п�
        private string m_Width;
        // �ж��뷽ʽ
        private string m_Align;
        // �ַ���ʽ
        private string m_Format;
        public string FieldsFormat
        {
            set { m_Format = value; }
            get { return m_Format; }
        }
        // ��������
        private string m_OperName;
        // ��������
        private string m_OperLink;
        // ������ʾ
        private string m_OperVisible;
        // �����е�Ա�񳬼����ӵ�ַ
        private string m_RowLink;

        // ===========================================
        private DataSet m_Ds;

        private string m_SvrUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];
        #endregion

        #region  PageData ��Ա����

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
                if (string.IsNullOrEmpty(this.FuncNo)) this.FuncNo = dr[0][0].ToString();
                this.m_FuncInfo = dr[0][1].ToString();
                this.m_Titles = dr[0][2].ToString();
                this.m_Fields = dr[0][3].ToString();
                this.m_Width = dr[0][4].ToString();
                this.m_Align = dr[0][5].ToString();
                this.m_Format = dr[0][6].ToString();
                this.m_RowLink = dr[0][7].ToString();
                this.m_OperName = dr[0][8].ToString();
                this.m_OperLink = dr[0][9].ToString();
                this.m_OperVisible = dr[0][10].ToString();

                m_Ds = null;
                dr = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = "��ȡ�����ļ�����ʧ�ܣ�" + ex.Message;
                return false;
            }
        }
        #endregion

        #region  ��ȡ��ҳ����

        /// <summary>
        /// ��ȡ�ܼ�¼��
        /// </summary>
        /// <returns></returns>
        public int GetTotalRowsNum(string[] funcInfo)
        {
            // 0����,1������,2��������,3�Ƿ���ʾcheckbox,4����,5url,6�༭,7url,8ɾ��,9url -->
            this.TableName = funcInfo[0];
            this.PrimaryKey = funcInfo[1];
            if (String.IsNullOrEmpty(this.FuncName))
            {
                this.FuncName = funcInfo[2];
            }

            if (String.IsNullOrEmpty(this.SearchWhere))
            {
                return DbHelperSQL.GetTotalRows(this.TableName);
            }
            else
            {
                return DbHelperSQL.GetTotalRows(this.TableName + " WHERE " + this.SearchWhere);
            }

        }
        /// <summary>
        /// ��ҳ��ȡ�����б�
        /// </summary>
        private bool GetPageDataSet(ref string errlrMsg)
        {
            /*
            @tblName varchar(255), -- ���� 
	        @fldName varchar(255), -- �����ֶ��� 
	        @PageSize int = 10, -- ҳ�ߴ� 
	        @PageIndex int = 1, -- ҳ�� 
	        @IsReCount bit = 0, -- ���ؼ�¼����, �� 0 ֵ�򷵻� 
	        @OrderType bit = 0, -- ������������, �� 0 ֵ���� 
	        @strWhere varchar(1000) = '' -- ��ѯ���� (ע��: ��Ҫ�� where) 
             */
            ConfigDataSet();

            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
                parameters[0].Value = this.TableName;
                parameters[1].Value = this.PrimaryKey;
                parameters[2].Value = this.PageSize;
                parameters[3].Value = this.PageNo;
                parameters[4].Value = 0;
                //����
                if (OrderType == 0) { parameters[5].Value = 0; }
                else { parameters[5].Value = 1; }

                parameters[6].Value = this.SearchWhere;
                m_Ds = DbHelperSQL.RunProcedure("UP_GetRecordByPage", parameters, "ds");
                return true;
            }
            catch (Exception ex)
            {
                errlrMsg = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// ��ȡҳ�뵼����Ϣ
        /// </summary>
        /// <param name="pageRec">��ҳ��ʾ�ļ�¼��</param>
        /// <returns></returns>
        private string GetPageNavStr(int pageRec)
        {
            int allpage = 0;
            int next = 0;
            int pre = 0;
            int startcount = 0;
            int endcount = 0;
            string pagestr = string.Empty; ;
            if (PageNo < 1) { PageNo = 1; }

            if (PageSize != 0)
            {
                allpage = (TotalRec / PageSize);
                allpage = ((TotalRec % PageSize) != 0 ? allpage + 1 : allpage);
                allpage = (allpage == 0 ? 1 : allpage);
            }
            next = PageNo + 1;
            pre = PageNo - 1;
            startcount = (PageNo + 5) > allpage ? allpage - 9 : PageNo - 4;

            endcount = PageNo < 5 ? 10 : PageNo + 5;
            if (startcount < 1) { startcount = 1; }
            if (allpage < endcount) { endcount = allpage; }

            pagestr = "ҳ�룺��[" + PageNo + "]ҳ/��[" + allpage + "]ҳ&nbsp;&nbsp;";
            pagestr += PageNo > 1 ? "<a href=\"" + this.Url + "&p=1\">��ҳ</a>&nbsp;&nbsp;<a href=\"" + this.Url + "&p=" + pre + "\">��һҳ</a>" : "��ҳ ��һҳ";
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += PageNo == i ? "&nbsp;&nbsp;<font color=\"#ff0000\">" + i + "</font>" : "&nbsp;&nbsp;<a href=\"" + this.Url + "&p=" + i + "\">" + i + "</a>";
            }
            pagestr += PageNo != allpage ? "&nbsp;&nbsp;<a href=\"" + this.Url + "&p=" + next + "\">��һҳ</a>&nbsp;&nbsp;<a href=\"" + this.Url + "&p=" + allpage + "\">ĩҳ</a>" : " ��һҳ ĩҳ";
            pagestr += "&nbsp;&nbsp;��¼������ҳ[ " + pageRec + " ]/��[ " + this.TotalRec + " ]";

            return pagestr;
        }
        /// <summary>
        /// α��̬��ҳ
        /// </summary>
        /// <param name="pageRec"></param>
        /// <param name="staticPage"></param>
        /// <returns></returns>
        private string GetPageNavStr(int pageRec, bool staticPage)
        {
            int allpage = 0;
            int next = 0;
            int pre = 0;
            int startcount = 0;
            int endcount = 0;
            string pagestr = string.Empty; ;
            if (PageNo < 1) { PageNo = 1; }

            if (PageSize != 0)
            {
                allpage = (TotalRec / PageSize);
                allpage = ((TotalRec % PageSize) != 0 ? allpage + 1 : allpage);
                allpage = (allpage == 0 ? 1 : allpage);
            }
            next = PageNo + 1;
            pre = PageNo - 1;
            startcount = (PageNo + 5) > allpage ? allpage - 9 : PageNo - 4;

            endcount = PageNo < 5 ? 10 : PageNo + 5;
            if (startcount < 1) { startcount = 1; }
            if (allpage < endcount) { endcount = allpage; }

            string selectstr = string.Empty;
            for (int k = 1; k <= allpage; k++)
            {
                if (k == PageNo) { selectstr += "<option value=\"" + this.Url + k + "." + this.FileExt + "\" selected>" + k + "</option>"; }
                else { selectstr += "<option value=\"" + this.Url + k + "." + this.FileExt + "\">" + k + "</option>"; }

            }
            pagestr = "<div class=\"page\">";
            pagestr += "<span class=\"a1\">";
            pagestr += PageNo > 1 ? "<a href=\"" + this.Url + "1." + this.FileExt + "\">&lt;&lt;</a> <a href=\"" + this.Url + pre + "." + this.FileExt + "\">&lt;</a>" : " <b>&lt;&lt;</b> <b> &lt;</b> ";
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += PageNo == i ? " <span>" + i + "</span>" : " <a href=\"" + this.Url + i + "." + this.FileExt + "\">" + i + "</a>";
            }
            if (allpage > 10)
            {
                pagestr += "...<a href=\"" + this.Url + allpage + "." + this.FileExt + "\">" + allpage + "</a>";
            }
            pagestr += PageNo != allpage ? " <a href=\"" + this.Url + next + "." + this.FileExt + "\" >&gt;</a> <a href=\"" + this.Url + allpage + "." + this.FileExt + "\">&gt;&gt;</a>" : " <b>&gt;</b> <b>&gt;&gt;</b>";
            //pagestr += "</span><span class=\"a2\">ת����<input name=\"txtPageNo\" type=\"text\" id=\"txtPageNo\" style=\"width:30px\" size=\"3\" maxlength=\"3\" onkeyup=\"window.location.href='" + this.Url + "'+this.value+'" + "." + this.FileExt + "'\"/>ҳ</span>";
            pagestr += "</span><span class=\"a2\">ת����<select onchange=\"javascript:window.location=this.value\">" + selectstr + "</select>ҳ</span>";
            /*<select onchange="javascript:window.location=this.value">
<option selected="" value="/in2.asp?Page=1&amp;CID=400">1</option></select>*/
            pagestr += "</div>";
            return pagestr;
        }
        /// <summary>
        /// ������ʾ
        /// </summary>
        /// <returns>�������Է�����Ӧҳ�������</returns>
        public string GetList()
        {
            string errorMsg = string.Empty;
            string pageNav = string.Empty;
            StringBuilder sb = new StringBuilder();
            string[] a_FuncInfo = this.m_FuncInfo.Split(',');
            string[] a_Titles = this.m_Titles.Split(',');
            string[] a_Fields = this.m_Fields.Split(',');
            string[] a_Width = this.m_Width.Split(',');
            string[] a_Align = this.m_Align.Split(',');
            string[] a_Format = this.m_Format.Split(',');
            string[] a_Link = this.m_RowLink.Split(',');
            string[] a_OperName = this.m_OperName.Split(',');
            string[] a_OperLink = this.m_OperLink.Split(',');
            string[] a_OperVisible = this.m_OperVisible.Split(',');

            string objID = string.Empty;
            string objFieldsValue = string.Empty;
            string objCheckBoxParams = string.Empty;
            string pageCheckBox = a_FuncInfo[3];
            string objStatus = string.Empty;
            string objChangeType = string.Empty;

            // ��ȡ�ܼ�¼��
            this.TotalRec = GetTotalRowsNum(a_FuncInfo);
            // ��ȡ��ҳ����
            if (GetPageDataSet(ref errorMsg))
            {
                try
                {
                    DataRow[] dr = m_Ds.Tables[0].Select();
                    int rowsNum = dr.Length;
                    int colsNum = a_Fields.Length;
                    int rowsOrderNo = 0; // ˳���
                    int rowsLen = 0;
                    pageNav = GetPageNavStr(dr.Length, true);
                    //<!--�����б� -->
                    sb.Append("<div class=\"user_list\">");
                    sb.Append("<div class=\"user_list_title2\">" + this.FuncName + "</div>");
                    sb.Append("<div class=\"user_list_c\">");
                    sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    sb.Append("<tr>");
                    if (pageCheckBox == "IsCheck") { sb.Append("<th width=\"30\"><input id=\"0\" onclick=\"SelectAll(this);\" type=\"checkbox\"  name=\"itemsi\"/></th>"); }
                    else { sb.Append("<th width=\"30\">���</th>"); }
                    for (int k = 0; k < colsNum; k++)
                    {
                        sb.Append("<th>" + a_Titles[k] + "</th>");
                    }
                    if (a_OperVisible[0] == "1" || a_OperVisible[1] == "1" || a_OperVisible[2] == "1" || a_OperVisible[3] == "1" || a_OperVisible[4] == "1" || a_OperVisible[5] == "1") sb.Append("<th>����</th>");
                    sb.Append("</tr>");

                    if (rowsNum > 0)
                    {
                        // ==================== �������  =============================                  
                        for (int i = 0; i < rowsNum; i++)
                        {
                            rowsOrderNo = (this.PageNo - 1) * this.PageSize + i + 1;
                            objID = dr[i]["" + this.PrimaryKey + ""].ToString();
                            if (pageCheckBox == "IsCheck") objCheckBoxParams = "SetCheckBoxClick(" + objID + ");";// ����cbxֻ֧�ֵ�ѡ objCheckBoxParams = "SetCheckBoxClear(" + objID + ");";
                            sb.Append("<tr>");
                            // ��ѡ��˳���
                            if (pageCheckBox == "IsCheck") { sb.Append("<td><input name=\"ItemChk\" type=checkbox id=" + objID + " value=" + objID + " onclick=\"" + objCheckBoxParams + "\"></td>"); }
                            else { sb.Append("<td>" + rowsOrderNo.ToString() + "</td>"); }

                            //���ݿ�ʼ
                            for (int j = 0; j < colsNum; j++)
                            {
                                objFieldsValue = FormatString(dr[i]["" + a_Fields[j] + ""].ToString(), a_Format[j]); // ��ʽ������ 

                                if (FuncNo == "0502" && j == 0)
                                {
                                    sb.Append("<td  align=\"" + FormatAlign(a_Align[j]) + "\" title=\"" + objFieldsValue + "\"><a href=\"" + m_SvrUrl + dr[i]["DocsPath"].ToString() + "\" rel=\"lightbox[zj]\" title=\"�鿴֤����" + objFieldsValue + "\">" + objFieldsValue + "</a></td>");
                                }
                                else if (FuncNo == "0503" && j == 0)
                                {
                                    sb.Append("<td  align=\"" + FormatAlign(a_Align[j]) + "\" title=\"" + objFieldsValue + "\"><a href=\"" + m_SvrUrl + dr[i]["DocsPath"].ToString() + "\" title=\"���ظ�����" + objFieldsValue + "\">" + objFieldsValue + "</a></td>");
                                }
                                else
                                {
                                    sb.Append("<td  align=\"" + FormatAlign(a_Align[j]) + "\" title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue, objID, a_Link[j], objStatus) + "</td>");
                                }
                            }

                            //OperName: �鿴,�༭,ɾ��,xx,xx,xx
                            if (a_OperVisible[0] == "1" || a_OperVisible[1] == "1" || a_OperVisible[2] == "1" || a_OperVisible[3] == "1" || a_OperVisible[4] == "1" || a_OperVisible[5] == "1")
                            {
                                sb.Append("<td >");
                                int k = 0;
                                for (int n = 0; n < a_OperName.Length; n++)
                                {
                                    if (a_OperVisible[n] == "1")
                                    {
                                        if (a_OperLink[n].Contains("del")) { objChangeType = "del"; }
                                        else { objChangeType = ""; }
                                        if (k == 0) { sb.Append("<a href=\"javascript:void(0);\" onclick=\"JavaScript:ChangeUrl('" + a_OperLink[n] + "&k=" + objID + "&oCode=" + FuncNo + "','" + HttpUtility.UrlEncode(this.FuncName) + "','" + objChangeType + "');\" title=\"" + a_OperName[n] + "\">" + a_OperName[n] + "</a>"); }
                                        else { sb.Append(" | <a href=\"javascript:void(0);\" onclick=\"JavaScript:ChangeUrl('" + a_OperLink[n] + "&k=" + objID + "&oCode=" + FuncNo + "','" + HttpUtility.UrlEncode(this.FuncName) + "','" + objChangeType + "');\" title=\"" + a_OperName[n] + "\">" + a_OperName[n] + "</a>"); }
                                        k++;
                                    }
                                }
                                sb.Append("</td>");
                            }
                            sb.Append("</tr>");
                        }
                        //���ݽ���
                    }
                    else
                    {
                        //û������ʱĬ����ʾһ������Ϣ���Ա�ʹ��Ӱ�ť��ʾ��
                        sb.Append("<tr>");
                        sb.Append("<td>&nbsp;</td>");
                        for (int k = 0; k < colsNum; k++)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        if (a_OperVisible[0] == "1" && a_OperName[0] == "����")
                        {
                            sb.Append("<td >");
                            sb.Append("<a href=\"javascript:void(0);\" onclick=\"JavaScript:ChangeUrl('" + a_OperLink[0] + "&k=" + objID + "&oCode=" + FuncNo + "','" + HttpUtility.UrlEncode(this.FuncName) + "','" + objChangeType + "');\" title=\"" + a_OperName[0] + "\">" + a_OperName[0] + "</a>");
                            sb.Append("</td>");
                        }
                        sb.Append("</tr>");
                    }

                    sb.Append("</table>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    //<!--ҳ�� -->
                    sb.Append("<div class=\"clr20\"></div>");
                    sb.Append(pageNav);
                    sb.Append("<div class=\"clr20\"></div>");
                }
                catch (Exception ex) { sb.Append(ex.Message); }
                if (m_Ds != null) m_Ds.Clear();
                m_Ds = null;
                //=====================================
            }
            else
            {
                sb.Append(errorMsg);
            }

            return sb.ToString();
        }
        #endregion

        #region  �������ò�������ʾ���ݽ��п���
        /// <summary>
        /// ������ַ���ʽ
        /// </summary>
        /// <param name="InStr">������ַ�</param>
        /// <param name="sType">�ַ�����</param>
        /// <returns></returns>
        private string FormatString(string InStr, string sType)
        {
            if (String.IsNullOrEmpty(InStr)) return "&nbsp;";
            try
            {
                switch (sType)
                {
                    case "0": // �ı�
                        InStr = InStr.Replace("\r", "");
                        InStr = InStr.Replace("\n", "<br>");
                        return InStr;
                    case "1": // ����
                        return DateTime.Parse(InStr).ToString("yyyy/MM/dd"); //string.Format("{0:yyyy-MM-dd}",InStr); 

                    case "2": // ����
                        return int.Parse(InStr).ToString("N2"); // string.Format("{0:N2}", InStr); //  %��ʽ��{0:P2}

                    case "8": // ���֤��
                        if (!String.IsNullOrEmpty(InStr)) { InStr = InStr.Substring(0, 4) + "***********" + InStr.Substring(15, 3); }
                        return InStr;
                    default:
                        return InStr;
                }
            }
            catch
            {
                return InStr;
            }
        }

        /// <summary>
        /// ���뷽ʽ����
        /// </summary>
        /// <param name="sType">��������</param>
        /// <returns></returns>
        private string FormatAlign(string sType)
        {
            switch (sType)
            {
                case "0": // ��
                    return "left";
                case "1": // ��
                    return "center";
                case "2": // ��
                    return "right";
                default:
                    return "left";
            }
        }

        /// <summary>
        /// �����б��еĳ���������ʾ����
        /// </summary>
        /// <param name="inStr"></param>
        /// <param name="objID"></param>
        /// <param name="rowLink"></param>
        /// <param name="opStatus"></param>
        /// <returns></returns>
        private string FormatLink(string inStr, string objID, string rowLink, string opStatus)
        {
            if (String.IsNullOrEmpty(inStr)) return "&nbsp;";
            if (String.IsNullOrEmpty(rowLink) || rowLink.Trim() == "#")
            {
                return inStr;
            }
            else
            {
                return "<a href=\"JavaScript:void(0);\" onclick=\"JavaScript:ChangeUrl('" + rowLink + "&RID=" + objID + "&oCode=" + FuncNo + "','" + HttpUtility.UrlEncode(this.FuncName) + "','view');\">" + inStr + "</a>";
            }

        }
        #endregion

        #endregion
    }
}

