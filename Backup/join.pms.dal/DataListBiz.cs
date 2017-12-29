using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Web;

using UNV.Comm.DataBase;

namespace join.pms.dal
{
    public class DataListBiz
    {
        #region  PageData ��Ա����
        // �Ƿ��ڿ������ʾ
        private bool _IsInFrame;
        public bool IsInFrame
        {
            set { _IsInFrame = value; }
            get { return _IsInFrame; }
        }
        // ҳ��ַ���� TabPage
        private string _Url;
        public string Url
        {
            set { _Url = value; }
            get { return _Url; }
        }
        // ѡ����
        private string _TabPageNo;
        public string TabPageNo
        {
            set { _TabPageNo = value; }
            get { return _TabPageNo; }
        }
        // �����ؼ���
        private string _SearchKeys;
        public string SearchKeys
        {
            set { _SearchKeys = value; }
            get { return _SearchKeys; }
        }
        // ��������
        private string _SearchType;
        public string SearchType
        {
            set { _SearchType = value; }
            get { return _SearchType; }
        }
        // ��Χ����
        private string _RangeKey;
        public string RangeKey
        {
            set { _RangeKey = value; }
            get { return _RangeKey; }
        }
        // �������� 
        private string _SearchWhere;
        public string SearchWhere
        {
            set { _SearchWhere = value; }
            get { return _SearchWhere; }
        }
        // �����ֶ�
        private string _SearchFields;
        public string SearchFields
        {
            set { _SearchFields = value; }
            get { return _SearchFields; }
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
        // ҵ����
        private string _BizID;
        public string BizID
        {
            set { _BizID = value; }
            get { return _BizID; }
        }
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
        // ����Ȩ��
        private string _FuncPowers;
        public string FuncPowers
        {
            set { _FuncPowers = value; }
            get { return _FuncPowers; }
        }
        // ��չ��
        private string _FileExt;
        public string FileExt
        {
            set { _FileExt = value; }
            get { return _FileExt; }
        }
        // ѡ����
        private string _FuncTreeName;
        public string FuncTreeName
        {
            set { _FuncTreeName = value; }
            get { return _FuncTreeName; }
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
        // TABҳ����
        private string m_TabName;
        // TABҳ����
        private string m_TabLink;
        // TABҳ��ʾ
        private string m_TabVisible;
        // ��ť����
        private string m_ButName;
        // ��ť����
        private string m_ButLink;
        // ��ť��ʾ
        private string m_ButVisible;
        // ͨ������
        private string m_Query;
        // �ؼ����ݱ�ʶ
        private string m_Analys;
        // �����е�Ա�񳬼����ӵ�ַ
        private string m_RowLink;
        // ��ǰ�����û���ɫ
        private string m_UserRoleID;

        //������ַȥ��
        private string m_AreaFilter = System.Configuration.ConfigurationManager.AppSettings["AreaFilter"];
        // ��ǰ�û���������
        private string _UserAreaCode;
        public string UserAreaCode
        {
            set { _UserAreaCode = value; }
            get { return _UserAreaCode; }
        }
        // ===========================================
        private DataSet m_Ds;


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
                this.m_TabName = dr[0][8].ToString();
                this.m_TabLink = dr[0][9].ToString();
                this.m_TabVisible = dr[0][10].ToString();
                this.m_ButName = dr[0][11].ToString();
                this.m_ButLink = dr[0][12].ToString();
                this.m_ButVisible = dr[0][13].ToString();
                //if (funcNo.Substring(0, 2) == "02" || funcNo.Substring(0, 2) == "05" || funcNo.Substring(0, 2) == "06")
                //{
                //    this.m_Analys = dr[0][14].ToString();
                //    this.m_Query = dr[0][15].ToString();
                //}

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
            if (this.RangeKey == "range_auditccccc") { this.TableName = "v_BizListByFlows"; }
            else { this.TableName = funcInfo[0]; }

            this.PrimaryKey = funcInfo[1];
            this.FuncName = funcInfo[2];

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
                //��ȷ���ƴ������� by ysl 2015/01/23
                if (this.RangeKey == "range_auditccccc")
                {
                    parameters[0].Value = "v_BizListByFlows";
                }
                else
                {
                    parameters[0].Value = this.TableName;
                }
                parameters[0].Value = this.TableName;
                parameters[1].Value = this.PrimaryKey;
                parameters[2].Value = this.PageSize;
                parameters[3].Value = this.PageNo;
                parameters[4].Value = 0;
                parameters[5].Value = 1;
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

            pagestr = "";
            pagestr += PageNo > 1 ? "<a href=\"" + this.Url + "&searchRange=" + this.RangeKey + "&p=1&k=" + HttpUtility.UrlEncode(this.SearchKeys) + "\">��ҳ</a><a href=\"" + this.Url + "&k=" + HttpUtility.UrlEncode(SearchKeys) + "&searchRange=" + this.RangeKey + "&p=" + pre + "\">��һҳ</a>" : "<span class=\"off\">��ҳ</span><span class=\"off\">��һҳ</span>";
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += PageNo == i ? "<span class=\"on\">" + i + "</span>" : "<a href=\"" + this.Url + "&k=" + HttpUtility.UrlEncode(SearchKeys) + "&searchRange=" + this.RangeKey + "&p=" + i + "\">" + i + "</a>";
            }
            pagestr += PageNo != allpage ? "<a href=\"" + this.Url + "&searchRange=" + this.RangeKey + "&p=" + next + "&k=" + HttpUtility.UrlEncode(SearchKeys) + "\">��һҳ</a><a href=\"" + this.Url + "&k=" + HttpUtility.UrlEncode(SearchKeys) + "&searchRange=" + this.RangeKey + "&p=" + allpage + "\">ĩҳ</a>" : "<span class=\"off\">��һҳ</span><span class=\"off\">ĩҳ</span>";
            //pagestr += "&nbsp;&nbsp;ҳ�룺��[" + PageNo + "]ҳ/��[" + allpage + "]ҳ&nbsp;&nbsp;��¼������ҳ[ " + pageRec + " ]/��[ " + this.TotalRec + " ]";
            pagestr += "<span class=\"amount\">&nbsp;&nbsp;ҳ�룺<b>" + PageNo + "</b> / " + allpage + "&nbsp;&nbsp;��¼����<b>" + pageRec + "</b> / " + this.TotalRec + "</span>";

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
            string[] a_FuncPowers = this.FuncPowers.Split(',');
            string[] a_Analys = null;

            string objID = string.Empty;
            string objAttribs = string.Empty;
            string objFieldsValue = string.Empty;
            string objCheckBoxParams = string.Empty;
            string pageCheckBox = a_FuncInfo[3];
            string objStatus = string.Empty;

            // ��ȡ�ܼ�¼��
            this.TotalRec = GetTotalRowsNum(a_FuncInfo);
            // ��ȡ��ҳ����
            if (GetPageDataSet(ref errorMsg))
            {
                try
                {
                    #region this.FuncNo 2013/06/24 ���Ϊֻ��ʾǰ10�е����� 02,06,11
                    if (this.FuncNo == "xxxx") { }
                    else
                    {
                        if (this.FuncNo.Substring(0, 2) == "aaaa")
                        {
                            a_Analys = this.m_Analys.Split(',');
                            int c = a_Fields.Length - 13; // ��������,�ȶ�����,״̬
                            string[] aryNew = new string[a_Fields.Length - c];
                            if (c > 0)
                            {
                                Array.Copy(a_Titles, aryNew, 10);
                                Array.Copy(a_Titles, 10 + c, aryNew, 10, 3);
                                a_Titles = aryNew;
                                aryNew = null;

                                aryNew = new string[13];
                                Array.Copy(a_Fields, aryNew, 10);
                                Array.Copy(a_Fields, 10 + c, aryNew, 10, 3);
                                a_Fields = aryNew;
                                aryNew = null;

                                aryNew = new string[13];
                                Array.Copy(a_Width, aryNew, 10);
                                Array.Copy(a_Width, 10 + c, aryNew, 10, 3);
                                a_Width = aryNew;
                                aryNew = null;

                                aryNew = new string[13];
                                Array.Copy(a_Align, aryNew, 10);
                                Array.Copy(a_Align, 10 + c, aryNew, 10, 3);
                                a_Align = aryNew;
                                aryNew = null;

                                aryNew = new string[13];
                                Array.Copy(a_Format, aryNew, 10);
                                Array.Copy(a_Format, 10 + c, aryNew, 10, 3);
                                a_Format = aryNew;
                                aryNew = null;

                                aryNew = new string[13];
                                Array.Copy(a_Link, aryNew, 10);
                                Array.Copy(a_Link, 10 + c, aryNew, 10, 3);
                                a_Link = aryNew;
                                aryNew = null;
                            }
                        }
                        else if (this.FuncNo.Substring(0, 2) == "99")
                        {
                            int c = a_Fields.Length - 12; // ��������,״̬
                            string[] aryNew = new string[a_Fields.Length - c];
                            if (c > 0)
                            {
                                Array.Copy(a_Titles, aryNew, 10);
                                Array.Copy(a_Titles, 10 + c, aryNew, 10, 2);
                                a_Titles = aryNew;
                                aryNew = null;

                                aryNew = new string[12];
                                Array.Copy(a_Fields, aryNew, 10);
                                Array.Copy(a_Fields, 10 + c, aryNew, 10, 2);
                                a_Fields = aryNew;
                                aryNew = null;

                                aryNew = new string[12];
                                Array.Copy(a_Width, aryNew, 10);
                                Array.Copy(a_Width, 10 + c, aryNew, 10, 2);
                                a_Width = aryNew;
                                aryNew = null;

                                aryNew = new string[12];
                                Array.Copy(a_Align, aryNew, 10);
                                Array.Copy(a_Align, 10 + c, aryNew, 10, 2);
                                a_Align = aryNew;
                                aryNew = null;

                                aryNew = new string[12];
                                Array.Copy(a_Format, aryNew, 10);
                                Array.Copy(a_Format, 10 + c, aryNew, 10, 2);
                                a_Format = aryNew;
                                aryNew = null;

                                aryNew = new string[12];
                                Array.Copy(a_Link, aryNew, 10);
                                Array.Copy(a_Link, 10 + c, aryNew, 10, 2);
                                a_Link = aryNew;
                                aryNew = null;
                            }
                        }
                        else { }
                    }
                    #endregion


                    DataRow[] dr = m_Ds.Tables[0].Select();
                    int rowsNum = dr.Length;
                    int colsNum = a_Fields.Length;
                    int rowsOrderNo = 0; // ˳���
                    int rowsLen = 0;
                    string tableWidth = GetTableWidth(a_Width);
                    this.FuncTreeName = CommPage.GetAllBizName(this.FuncNo);
                    pageNav = GetPageNavStr(dr.Length);
                    // ������Ϣ
                    sb.Append("<div class=\"mbx\">");
                    sb.Append("��ǰλ�ã�" + FuncTreeName + "(" + a_FuncInfo[2] + ")");
                    sb.Append("</div>");
                    // ������ť
                    sb.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\"><tr><td height=\"25\" class=\"butCss_01\"> ");
                    sb.Append(OprateButtonInfo(a_FuncInfo, a_FuncPowers));
                    sb.Append("</td></tr></table>");

                    //����Table��������
                    //sb.Append("<table width=\"98%\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tbody>");
                    //sb.Append("<tr><td class=\"small\" colspan=\"3\" height=\"23\">��ʾ��ͨ����ѡ������ʵ����������������ܰ�ť�ɽ�����Ӧ�Ĳ����������������ҡ��ɲ��Ҽ�¼ ����</td></tr>");//if (a_FuncInfo[10] == "IsAdd") 
                    //sb.Append("<tr><td colspan=\"3\">");
                    //sb.Append(GetTabPageInfo(a_FuncInfo)); // Tab Pages
                    //sb.Append("</td></tr>");
                    //sb.Append("<tr><td valign=\"top\"><img src=\"images/showPanelTopLeft.gif\"></td><td width=\"100%\" valign=\"top\" class=\"showPanelBg\" style=\"padding: 10px;\">");
                    //sb.Append("<div id=\"ListViewContents\" class=\"small\" style=\"width: 100%; position: relative;\">");
                    //sb.Append("<table class=\"lvtBg\" width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\"><tbody><tr><td>");
                    //sb.Append(OprateButtonInfo(a_FuncInfo, a_FuncPowers));// ҳͷ��ť��ҳ����Ϣ
                    //sb.Append("<div>");

                    // ��ͷ
                    sb.Append("<!--list start-->");
                    sb.Append(GetPageSearch(a_Titles, a_Fields, a_Format));// ҳ������
                    // 2013/01/16 by ysl �����϶���ͷ������Ĺ��� class=\"admintable\" class=\"adminth\"
                    // ===================  ��ͷ  =======================================================
                    //sb.Append("<table width=\"" + tableWidth + "\" class=\"admintable\" cellpadding=\"0\" cellspacing=\"0\"  id=\"DargTableByYsl\" border=\"1\"><thead><tr>");
                    sb.Append("<table width=\"" + tableWidth + "\" cellpadding=\"0\" cellspacing=\"0\"  id=\"DargTableByYsl\" border=\"0\"><thead><tr>");
                    if (pageCheckBox == "IsCheck") { sb.Append("<th width=\"50\" >&nbsp;<input name=\"itemsi\" id=\"0\" onclick=\"SelectAll();\" type=\"checkbox\" /></th>"); }
                    else { sb.Append("<th width=\"50\">���</th>"); }
                    // class=\"adminth order\" order��ʽΪ������ width=\"" + a_Width[k] + "\" 
                    if (a_FuncInfo[13] == "IsEdit" || a_FuncInfo[14] == "IsDel") sb.Append("<th width=\"120\">����</th>");
                    if (this.FuncNo == "xxxx")
                    {
                        for (int k = 0; k < colsNum; k++)
                        {
                            if (a_Analys[k] != "null") { sb.Append("<th width=\"" + a_Width[k] + "\" class=\"color_01\">" + a_Titles[k] + "</th>"); }
                            else { sb.Append("<th width=\"" + a_Width[k] + "\">" + a_Titles[k] + "</th>"); }
                        }
                    }
                    else
                    {
                        for (int k = 0; k < colsNum; k++)
                        {
                            sb.Append("<th width=\"" + a_Width[k] + "\">" + a_Titles[k] + "</th>");
                        }
                    }

                    //if (a_FuncInfo[11] == "IsEdit" || a_FuncInfo[12] == "IsDel") sb.Append("<th width=\"130\">����</th>");
                    sb.Append("</tr></thead>");
                    // ==================== �������<tbody>  ================================================================
                    for (int i = 0; i < rowsNum; i++)
                    {
                        rowsOrderNo = (this.PageNo - 1) * this.PageSize + i + 1;
                        objID = dr[i]["" + this.PrimaryKey + ""].ToString();
                        objAttribs = dr[i]["Attribs"].ToString();

                        if (pageCheckBox == "IsCheck") objCheckBoxParams = "SetCheckBoxClick('" + objID + "');";// ����cbxֻ֧�ֵ�ѡ objCheckBoxParams = "SetCheckBoxClear(" + objID + ");";
                        if (i % 2 == 0)
                        {
                            sb.Append("<tr class=\"rowBai\"  onmouseover=\"this.className='rowHover'\" onmouseout=\"this.className='rowBai'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                        }
                        else
                        {
                            sb.Append("<tr class=\"rowHui\"  onmouseover=\"this.className='rowHover'\" onmouseout=\"this.className='rowHui'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                        }
                        // ��ѡ��˳��� align=\"center\"
                        if (pageCheckBox == "IsCheck") { sb.Append("<td > &nbsp;<input name=\"ItemChk\" type=\"checkbox\" id=\"" + objID + "\" value=\"" + objID + "\" onclick=\"" + objCheckBoxParams + "\"></td>"); }
                        else { sb.Append("<td>&nbsp;" + rowsOrderNo.ToString() + "</td>"); }
                        //��������ǰ�� 2014/12/12 by Ysl
                        if (a_FuncPowers != null && a_FuncPowers.Length > 2)
                        {
                            // v_BizList,BizID,����ԤԼ,IsCheck,4����,#?action=add,6�޸�,#?action=edit,8ɾ��,#?action=del,10�鿴,#?action=view,12 NoAdd,13 IsEdit,14 IsDel,15 IsView
                            // IsAdd,IsEdit,IsDel,IsView
                            if (a_FuncInfo[13] == "IsEdit" || a_FuncInfo[14] == "IsDel" || a_FuncInfo[15] == "IsView")
                            {
                                if (a_FuncPowers[1] == "1" || a_FuncPowers[2] == "1")
                                {
                                    sb.Append("<td>");//align=\"center\"

                                    if (a_FuncPowers[1] == "1" && a_FuncInfo[13] == "IsEdit") sb.Append("<a class=\"edit\" title=\"�޸ġ���\" href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[7] + "&k=" + objID + "','','edit');\"></a>");
                                    if (objAttribs == "1" || objAttribs == "6")
                                    {
                                        if (a_FuncPowers[2] == "1" && a_FuncInfo[14] == "IsDel" && IsMyBiz(objID, UserAreaCode)) sb.Append("<a class=\"but_audit\" title=\"��ˡ�\" href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[9] + "&k=" + objID + "','','edit');\"></a>");
                                    }
                                    if (a_FuncInfo[15] == "IsView")
                                    {
                                        sb.Append("<a class=\"but_view\" title=\"�鿴��ϸ����\" href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[11] + "&k=" + objID + "','','edit');\"></a>");
                                    }
                                    //sb.Append("<a class=\"but_data\" title=\"���ݶԱȡ���\" href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[11] + "&k=" + objID + "','','data');\"></a>");              
                                    sb.Append("</td>");

                                }
                                else
                                {
                                    sb.Append("<td>");
                                    if (a_FuncInfo[15] == "IsView") sb.Append("<a class=\"but_view\" title=\"�鿴��ϸ����\" href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[11] + "&k=" + objID + "','','edit');\"></a>");
                                    sb.Append("&nbsp;</td>");
                                }
                            }
                            else { }
                        }
                        else
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        // ����
                        for (int j = 0; j < colsNum; j++)
                        {
                            objFieldsValue = FormatString(dr[i]["" + a_Fields[j] + ""].ToString(), a_Format[j]).Trim(); // ��ʽ������ title=\"" + objFieldsValue + "\"
                            if (a_Width[j] != "*")
                            {
                                if (a_Titles[j].IndexOf("���֤") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("֤��") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("����") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("���") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("����") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("����") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("����") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("�绰") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("����") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("ʱ��") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("״̬") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("IP") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else { rowsLen = int.Parse(a_Width[j]) / 10; }
                            }
                            else { rowsLen = 350; }
                            //if (a_Width[j] != "*") { rowsLen = int.Parse(a_Width[j]); } else { rowsLen = 350; }
                            if (objFieldsValue == "&nbsp;")
                            {
                                sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + ">" + objFieldsValue + "</td>");
                            }
                            else
                            {
                                if (objFieldsValue.Length > rowsLen - 5)
                                {
                                    // ������ַ �־�ס�� m_AreaFilter
                                    if (a_Titles[j].IndexOf("��ַ") > -1 || a_Titles[j].IndexOf("��ס��") > -1 || a_Titles[j].IndexOf("��λ") > -1)
                                    {
                                        objFieldsValue = objFieldsValue.Replace(m_AreaFilter, "");
                                        if (objFieldsValue.Length > 18)
                                        {
                                            sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, 18), objID, a_Link[j], objStatus) + "��</td>");
                                        }
                                        else
                                        {
                                            sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue, objID, a_Link[j], objStatus) + "��</td>");
                                        }

                                    }
                                    else
                                    {
                                        if (objFieldsValue.Length > 20)
                                        {
                                            sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, 20), objID, a_Link[j], objStatus) + "��</td>");
                                        }
                                        //else if (objFieldsValue.Length > 6 && objFieldsValue.Length < 10)
                                        //{
                                        //    sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, 5), objID, a_Link[j], objStatus) + "��</td>");
                                        //}
                                        //else if (objFieldsValue.Length > 3 && objFieldsValue.Length < 7)
                                        //{
                                        //    sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, 4), objID, a_Link[j], objStatus) + "��</td>");
                                        //}
                                        else
                                        {
                                            sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue, objID, a_Link[j], objStatus) + "</td>");
                                        }

                                    }
                                }
                                else
                                {
                                    if (a_Titles[j].IndexOf("��ַ") > -1 || a_Titles[j].IndexOf("סַ") > -1 || a_Titles[j].IndexOf("��λ") > -1)
                                    {
                                        if (objFieldsValue.Length > 20)
                                        {
                                            sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, 20), objID, a_Link[j], objStatus) + "��</td>");
                                        }
                                        else
                                        {
                                            sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue, objID, a_Link[j], objStatus) + "</td>");
                                        }
                                    }
                                    else
                                    {
                                        sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + ">" + FormatLink(objFieldsValue, objID, a_Link[j], objStatus) + "</td>");
                                    }
                                }
                            }
                            //-------------------------
                        }
                        // ��������
                        /*
                        if (a_FuncPowers != null && a_FuncPowers.Length > 2)
                        {
                            if (a_FuncInfo[11] == "IsEdit" || a_FuncInfo[12] == "IsDel")
                            {
                                if (a_FuncPowers[1] == "1" || a_FuncPowers[2] == "1")
                                {
                                    sb.Append("<td align=\"center\">");
                                    //if (a_FuncPowers[1] == "1" && a_FuncInfo[11] == "IsEdit") sb.Append("<img src=\"images/icon-edit.gif\"/><a href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[7] + "&k=" + objID + "','','edit');\">" + a_FuncInfo[6] + "</a> ");
                                    //if (a_FuncPowers[1] == "1" && a_FuncPowers[2] == "1" && a_FuncInfo[11] == "IsEdit" && a_FuncInfo[12] == "IsDel") sb.Append(" | ");
                                    //if (a_FuncPowers[2] == "1" && a_FuncInfo[12] == "IsDel") sb.Append("<img src=\"images/icon-del.gif\"/><a href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[9] + "&k=" + objID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','del');\">" + a_FuncInfo[8] + "</a></td>");
                                    if (a_FuncPowers[1] == "1" && a_FuncInfo[11] == "IsEdit") sb.Append("<a class=\"edit\" title=\"�༭\" href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[7] + "&k=" + objID + "','','edit');\"></a>");
                                    if (a_FuncPowers[2] == "1" && a_FuncInfo[12] == "IsDel") sb.Append("<a class=\"del\" title=\"ɾ��\" href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[9] + "&k=" + objID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','del');\"></a></td>");
                                    sb.Append("</td>");
                                }
                                else { sb.Append("<td>&nbsp;</td>"); }
                            }
                            else { }
                        }
                        else
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }*/
                        sb.Append("</tr>");
                    }
                    //sb.Append("</tbody></table>");
                    sb.Append("</table>");
                    // ҳ����Ϣ
                    sb.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"10\">");
                    sb.Append("<tr><td><div align=\"left\" class=\"page\">" + pageNav + "</div></td>");
                    sb.Append("</tr></table>");

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

        private bool IsMyBiz(string bizID, string userAaea)
        {
            // SELECT COUNT(*) FROM BIZ_WorkFlows WHERE BizID= AND AreaCode= AND Attribs=9
            string countBiz = string.Empty;
            try
            {
                countBiz = DbHelperSQL.GetSingle("SELECT COUNT(*) FROM BIZ_WorkFlows WHERE BizID=" + bizID + " AND AreaCode='" + userAaea + "' AND Attribs=9").ToString();
                if (countBiz != "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }


        #region  �������ò�������ʾ���ݽ��п���

        private string GetTableWidth(string[] aryWidth)
        {
            string reVal = string.Empty;
            int w = 0;
            try
            {
                for (int i = 0; i < aryWidth.Length; i++)
                {
                    w += int.Parse(aryWidth[i]);
                }
                w += aryWidth.Length + 2;
                reVal = w.ToString();
            }
            catch { reVal = "100%"; }

            return w.ToString();
        }

        /// <summary>
        /// ��ȡѡ�������Ϣ
        /// </summary>
        /// <returns></returns>
        private string GetTabPageInfo(string[] funcInfo)
        {
            string className = string.Empty; ;
            string[] a_TabName = this.m_TabName.Split(',');
            string[] a_TabLink = this.m_TabLink.Split(',');
            string[] a_TabVisible = this.m_TabVisible.Split(',');
            string addIcon = "images/btnL3Add.gif";
            if (funcInfo[10] == "NoAdd") addIcon = "images/btnL3Add-Faded.gif";
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("<!-- Tab Page Start -->");
                sb.Append("<table class=\"small\" width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tbody><tr>");

                for (int i = 0; i < a_TabName.Length; i++)
                {
                    if (a_TabVisible[i].Trim() == "1")
                    {
                        if (this.TabPageNo == i.ToString()) { className = "dvtSelectedCell"; } else { className = "dvtUnSelectedCell"; }

                        sb.Append("<td class=\"dvtTabCache\" style=\"width: 10px;\" >&nbsp;</td>");
                        sb.Append("<td id=\"view_24\" class=\"" + className + "\" align=\"center\" nowrap=\"nowrap\"><a href=\"" + a_TabLink[i] + "\">" + a_TabName[i] + "</a></td>");
                    }
                }
                sb.Append("<td class=\"dvtTabCache\" style=\"width: 10px;\" >&nbsp;</td>");
                sb.Append("<td style=\"width: 100%;\" class=\"dvtTabCache\" nowrap=\"nowrap\">&nbsp;&nbsp;&nbsp;&nbsp;");
                //sb.Append("<a href=\"#\" onclick=\"JavaScript:ChangeUrl('" + funcInfo[5] + "','" + HttpUtility.UrlEncode(this.FuncName) + "','add');\"><img src=\"" + addIcon + "\" alt=\"" + funcInfo[4] + " " + this.FuncName + "...\" title=\"" + funcInfo[4] + " " + this.FuncName + "...\" border=\"0\" align=\"absmiddle\"></a>&nbsp;&nbsp;");
                //sb.Append("<a href=\"#\" onclick=\"moveMe('advSearch');showSearchDiv('advSearch')\"><img src=\"images/btnL3Search.gif\" alt=\"���� " + this.FuncName + "...\" title=\"���� " + this.FuncName + "...\" border=\"0\" align=\"absmiddle\"></a>");
                sb.Append("</td>");
                sb.Append("</tr></tbody></table><!-- Tab Page End -->");
            }
            catch (Exception ex)
            {
                sb.Append("��ȡ Tab Pages ��Ϣʧ�ܣ�" + ex.Message);
            }

            return sb.ToString();
        }


        /// <summary>
        /// ҳ�浼����Ϣ
        /// </summary>
        /// <returns></returns>
        private string GetPageTopNavInfo(string[] funcInfo)
        {
            // 0����,1������,2��������,3�Ƿ���ʾcheckbox,4����,5url,6�༭,7url,8ɾ��,9url,10 IsAdd,11 IsEdit,12 IsDel -->
            string addIcon = "images/btnL3Add.gif";
            if (funcInfo[10] == "NoAdd") addIcon = "images/btnL3Add-Faded.gif";
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"level2Bg\"><tbody><tr>");
            sb.Append("<td width=\"1\" height=\"30\">&nbsp;</td>");
            sb.Append("<td align=\"left\" style=\"padding-left: 10px; padding-right: 50px;\" class=\"moduleName\" nowrap=\"nowrap\">");
            switch (this.FuncNo)
            {
                case "1": //BizType 1,������Э����Ϣ 2,�ɹ������ҵ�� 3 ����������δ����� �� ������ 6,�ȴ����� 7,�Ѿ�����
                    sb.Append("������Э����Ϣ&nbsp;&nbsp;&nbsp;&nbsp;");
                    break;
                case "2":
                    sb.Append("�ɹ������ҵ��&nbsp;&nbsp;&nbsp;&nbsp;");
                    break;
                case "3":
                    sb.Append("����������δ�����&nbsp;&nbsp;&nbsp;&nbsp;");
                    break;
                case "6":
                    sb.Append("�ȴ������ҵ��&nbsp;&nbsp;&nbsp;&nbsp;");
                    break;
                case "7":
                    sb.Append("�Ѿ������ҵ��&nbsp;&nbsp;&nbsp;&nbsp;");
                    break;
                default:
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
                    break;
            }
            sb.Append("</td><td width=\"3\">&nbsp;</td></tr></tbody></table>");

            return sb.ToString();
        }
        /* 
        /// <summary>
        /// ������ť��Ϣ
        /// </summary>
        /// <returns></returns>
        private string OprateButtonInfo(string[] funcInfo, string[] a_FuncPowers)
        {
            // ���� �༭ ɾ�� ����1 ����2 ����3 ����4 ����5 ����6 
            string[] a_ButName = this.m_ButName.Split(',');
            string[] a_ButLink = this.m_ButLink.Split(',');
            string[] a_ButVisible = this.m_ButVisible.Split(',');
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("���ܲ�����");
                // ������ť
                if (a_FuncPowers[0] == "1" && funcInfo[10] == "IsAdd") { sb.Append("<input class=\"submit6\" value=\"  " + funcInfo[4] + "  \" onclick=\"JavaScript:ChangeUrl('" + funcInfo[5] + "&BizID=" + BizID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','add');\" type=\"button\"/>&nbsp;"); }
                // ��ѯ��ť
                sb.Append("<input class=\"submit6\" value=\"  ����  \" onclick=\"moveMe('advSearch');showSearchDiv('advSearch');\" type=\"button\"/>&nbsp;");
                for (int i = 0; i < a_ButName.Length; i++)
                {
                    if (a_ButVisible[i].Trim() == "1" && a_FuncPowers[i + 3] == "1")
                    {
                        if (i == 0) { sb.Append("<input class=\"submit6\" value=\"  " + a_ButName[i] + "  \" onclick=\"JavaScript:ChangeUrlMultCheck('" + a_ButLink[i] + "','" + HttpUtility.UrlEncode(this.FuncName) + "');\" type=\"button\"/>&nbsp;"); }
                        else { sb.Append("<input class=\"submit6\" value=\"  " + a_ButName[i] + "  \" onclick=\"JavaScript:ChangeUrlMultCheck('" + a_ButLink[i] + "','" + HttpUtility.UrlEncode(this.FuncName) + "');\" type=\"button\"/>&nbsp;"); }
                    }
                }
            }
            catch (Exception ex)
            {
                sb.Append("��ȡ������ť��Ϣʧ�ܣ�" + ex.Message);
            }

            a_ButName = null;
            a_ButLink = null;
            a_ButVisible = null;
            return sb.ToString();
        }*/

        /// <summary>
        /// ������ť��Ϣ,��������
        /// </summary>
        /// <returns></returns>
        private string OprateButtonInfo(string[] funcInfo, string[] a_FuncPowers)
        {
            // ���� �༭ ɾ�� ����1 ����2 ����3 ����4 ����5 ����6 
            string[] a_ButName = this.m_ButName.Split(',');
            string[] a_ButLink = this.m_ButLink.Split(',');
            string[] a_ButVisible = this.m_ButVisible.Split(',');
            StringBuilder sb = new StringBuilder();
            try
            {
                string stepNames = string.Empty;
                string bizSteps = CommPage.GeBizSteps(this.FuncNo, ref stepNames);
                string[] aryNas;
                // Ů����/��,�з���/��,Ů�����,�з����,��֤����
                sb.Append("ѡ�����̽ڵ㣺<select name=\"viewRange\" id=\"viewRange\" class=\"small\" onchange=\"getQueryData(document.getElementById('viewRange').value,'" + this.Url + "')\">");

                if (RangeKey == "range_audit") { sb.Append("<option value=\"range_audit\" selected=\"selected\">���ڴ�����</option>"); }
                else { sb.Append("<option value=\"range_audit\">���ڴ�����</option>"); }

                if (RangeKey == "range_ok") { sb.Append("<option value=\"range_ok\" selected=\"selected\">���ͨ��(�ɷ�֤)</option>"); }
                else { sb.Append("<option value=\"range_ok\">���ͨ��(�ɷ�֤)</option>"); }

                if (RangeKey == "range_no") { sb.Append("<option value=\"range_no\" selected=\"selected\">��˲���</option>"); }
                else { sb.Append("<option value=\"range_no\">��˲���</option>"); }

                if (RangeKey == "range_all") { sb.Append("<option value=\"range_all\" selected=\"selected\">����ҵ������¼</option>"); }
                else { sb.Append("<option value=\"range_all\">����ҵ������¼</option>"); }

                if (RangeKey == "range_save") { sb.Append("<option value=\"range_save\" selected=\"selected\">�����ѹ鵵��¼</option>"); }
                else { sb.Append("<option value=\"range_save\">�����ѹ鵵��¼</option>"); }

                if (!string.IsNullOrEmpty(bizSteps) && int.Parse(bizSteps) > 0)
                {
                    aryNas = stepNames.Split(',');
                    for (int k = 1; k < int.Parse(bizSteps) + 1; k++)
                    {
                        if (RangeKey == "range_" + k.ToString() + "") { sb.Append("<option value=\"range_" + k.ToString() + "\" selected=\"selected\">" + aryNas[k - 1] + "</option>"); }
                        else { sb.Append("<option value=\"range_" + k.ToString() + "\" >" + aryNas[k - 1] + "</option>"); }
                    }
                }

                sb.Append("</select>&nbsp;���ܲ�����");
                // v_BizList,BizID,����ԤԼ,IsCheck,����,#?action=add,�޸�,#,���,#,�鿴,#,NoAdd,IsEdit,IsDel,IsView
                if (a_FuncPowers[0] == "1" && funcInfo[12] == "IsAdd") { sb.Append("<input class=\"submit6 butGreen\" value=\"  " + funcInfo[4] + "  \" onclick=\"JavaScript:ChangeUrl('" + funcInfo[5] + "&BizID=" + BizID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','add');\" type=\"button\"/>&nbsp;"); }
                // ��ѯ��ť
                sb.Append("<input class=\"submit6\" value=\"  ����  \" onclick=\"moveMe('advSearch');showSearchDiv('advSearch');\" type=\"button\"/>&nbsp;");
                string[] arybutCss = new string[] { "butOrange", "butBlue", "butRed", "butBlue", "butRed", "butDefault", "butDefault", "butDefault", "butGreen" };
                for (int i = 0; i < a_ButName.Length; i++)
                {
                    if (a_ButVisible[i].Trim() == "1" && a_FuncPowers[i + 4] == "1")
                    {
                        if (i == 0) { sb.Append("<input class=\"submit6 " + arybutCss[i] + "\" value=\"  " + a_ButName[i] + "  \" onclick=\"JavaScript:ChangeUrlMultCheck('" + a_ButLink[i] + "','" + HttpUtility.UrlEncode(this.FuncName) + "');\" type=\"button\"/>&nbsp;"); }
                        else { sb.Append("<input class=\"submit6 " + arybutCss[i] + "\" value=\"  " + a_ButName[i] + "  \" onclick=\"JavaScript:ChangeUrlMultCheck('" + a_ButLink[i] + "','" + HttpUtility.UrlEncode(this.FuncName) + "');\" type=\"button\"/>&nbsp;"); }
                    }
                }
                // �߰찴ť
                sb.Append("<input class=\"submit6 butOrange\" value=\"  �߰�  \" onclick=\"JavaScript:ChangeUrl('/siteadmin/CBMsgSend.aspx?action=add&BizID=" + BizID + "','SMS%e7%ae%a1%e7%90%86','');\" type=\"button\"/>&nbsp;");
            }
            catch (Exception ex)
            {
                sb.Append("��ȡ������ť��Ϣʧ�ܣ�" + ex.Message);
            }

            a_ButName = null;
            a_ButLink = null;
            a_ButVisible = null;
            return sb.ToString();
        }

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
                        if (UNV.Comm.Web.PageValidate.IsDateTime(InStr))
                        {
                            return DateTime.Parse(InStr).ToString("yyyy/MM/dd"); //string.Format("{0:yyyy-MM-dd}",InStr); 
                        }
                        else { return InStr; }

                    case "2": // ����
                        return InStr.Replace(" ", "");
                    //return int.Parse(InStr).ToString("N2"); // string.Format("{0:N2}", InStr); //  %��ʽ��{0:P2}
                    case "3": // True��fasle 
                        return InStr.ToUpper() == "TRUE" ? "��" : "��";
                    case "4": // 1��0
                        return InStr == "1" ? "��" : "��";
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
                    return "align=\"left\"";
                case "1": // ��
                    return "align=\"center\"";
                case "2": // ��
                    return "align=\"right\"";
                case "9": // ������Ϣ
                    return "bgcolor=\"#FFCCCC\"";
                default:
                    return "align=\"left\"";
            }

            // align=\"left\"
        }

        private string SetAlignAndColor(string sType, string sValue)
        {
            switch (sType)
            {
                case "0": // ��
                    return "align=\"left\"";
                case "1": // ��
                    return "align=\"center\"";
                case "2": // ��
                    return "align=\"right\"";
                case "9": // ������Ϣ ����>1 
                    if (FuncNo == "060303" || FuncNo == "060304" || FuncNo == "060305" || FuncNo == "060603" || FuncNo == "061001")
                    {
                        if (sValue == "����������") { return "bgcolor=\"#FFCCCC\""; } else { return "align=\"left\""; }
                    }
                    else
                    {
                        if (UNV.Comm.Web.PageValidate.IsNumber(sValue))
                        {
                            if (int.Parse(sValue) > 1) { return "bgcolor=\"#FFCCCC\""; }
                            else { return "align=\"left\""; }
                        }
                        else { return "align=\"left\""; }
                    }
                default:
                    return "align=\"left\"";
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
            if (String.IsNullOrEmpty(inStr)) return "";
            if (String.IsNullOrEmpty(rowLink) || rowLink.Trim() == "#")
            {
                return inStr;
            }
            else
            {
                if (rowLink.IndexOf("helpbiz0") > -1)
                {
                    //ĸ�ӽ����ֲ���ϸ��Ϣ�鿴
                    return "<a href=\"#\" onclick=\"JavaScript:ChangeUrl('" + rowLink + "&mp=1&fwzh=" + inStr + "','" + HttpUtility.UrlEncode(this.FuncName) + "','view');\">" + inStr + "</a>";
                }
                else
                {
                    return "<a href=\"#\" onclick=\"JavaScript:ChangeUrl('" + rowLink + "&RID=" + objID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','view');\">" + inStr + "</a>";
                }
            }

        }

        #endregion

        #region ��������
        /// <summary>
        /// ͨ��������Ϣ����
        /// </summary>
        /// <param name="aTitles"></param>
        /// <param name="aFields"></param>
        /// <param name="aFormat"></param>
        /// <returns></returns>
        private string GetPageSearch(string[] aTitles, string[] aFields, string[] aFormat)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id=\"advSearch\" style=\"display: none;\"><input name=\"searchAction\" value=\"advSearch\" type=\"hidden\"/>");
            sb.Append("<table class=\"searchUIAdv1 small\" width=\"80%\" align=\"center\" border=\"0\" cellpadding=\"5\" cellspacing=\"0\"><tbody><tr>");
            sb.Append("<td class=\"searchUIName small\" align=\"left\" nowrap=\"nowrap\"><span class=\"moduleName\">��������ѡ����Ӧ������������ؼ��ֿ�ʼ���� ����</span></td><td class=\"small\" nowrap=\"nowrap\"></td>");
            sb.Append("<td class=\"small\" onmouseover=\"this.style.cursor='pointer';\" onclick=\"moveMe('advSearch');showSearchDiv('advSearch')\" valign=\"top\" align=\"right\">[�ر�]</td>");
            sb.Append("</tr></tbody></table>");

            sb.Append("<table class=\"searchUIAdv2 small\" width=\"80%\" align=\"center\" border=\"0\" cellpadding=\"2\" cellspacing=\"0\"><tbody><tr>");
            sb.Append("<td class=\"small\" width=\"90%\" align=\"center\">");
            sb.Append(" <div id=\"fixed\" style=\"border: 1px solid rgb(204, 204, 204); padding: 0px; overflow: auto; position: relative; width: 95%; height: 80px; background-color: rgb(255, 255, 255);\" class=\"small\">");
            sb.Append("<table width=\"95%\" border=\"0\"><tbody><tr><td align=\"left\">");
            sb.Append("<table id=\"adSrc\" width=\"100%\" align=\"left\" border=\"0\" cellpadding=\"2\" cellspacing=\"0\"><tbody>");
            for (int i = 0; i < aFields.Length; i++)
            {
                if (i % 2 == 0)
                {
                    if (i > 0) sb.Append("<td>&nbsp;</td></tr>");
                    sb.Append("<tr>");
                }

                sb.Append("<td width=\"100\" align=\"right\">" + aTitles[i] + "��</td>");
                sb.Append("<td width=\"70\" align=\"left\">");
                // �ַ���ʽ 0 �ı�,1 ����,2 ����
                if (aFormat[i].Trim() == "0")
                {
                    sb.Append(GetFilterStr("sel" + aFields[i], true));
                }
                else if (aFormat[i].Trim() == "1") { sb.Append("ѡ��Χ"); }
                else
                {
                    sb.Append(GetFilterStr("sel" + aFields[i], false));
                }
                sb.Append("</td>");
                if (aFormat[i].Trim() == "1")
                {
                    // size=\"29\" onclick=\"PopCalendar(txtReportDate);return false;\" 
                    sb.Append("<td width=\"220\" align=\"left\"><input name=\"txt" + aFields[i] + "Start\" class=\"detailedViewTextBox\" type=\"text\" title=\"ѡ��ʼ����\" readonly=\"readonly\" size=\"8\" onclick=\"JTC.setday({format:'yyyy-MM-dd', readOnly:false})\">=>");
                    sb.Append("<input name=\"txt" + aFields[i] + "End\" class=\"detailedViewTextBox\" type=\"text\" title=\"ѡ���ֹ����\" readonly=\"readonly\" size=\"8\" onclick=\"JTC.setday({format:'yyyy-MM-dd', readOnly:false})\"></td><td></td>");
                }
                else
                {
                    sb.Append("<td width=\"220\" align=\"left\"><input name=\"txt" + aFields[i] + "\" class=\"detailedViewTextBox\" type=\"text\" title=\"�ؼ���\"></td><td></td>");
                }
            }
            sb.Append("<td>&nbsp;</td></tr>");
            sb.Append("</tbody></table></td></tr></tbody></table></div></td></tr></tbody></table>");

            sb.Append("<table class=\"searchUIAdv3 small\" width=\"80%\" align=\"center\" border=\"0\" cellpadding=\"5\" cellspacing=\"0\"><tbody><tr>");
            sb.Append("<td width=\"60%\" align=\"left\"><input class=\"submit6\" value=\" �������� \" type=\"submit\"/></td>");// onclick=\"totalnoofrows();callSearch('Advanced');\"
            sb.Append("<td class=\"small\" align=\"left\"></td></tr></tbody></table></div>");

            return sb.ToString();
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="aTitles"></param>
        /// <param name="aFields"></param>
        /// <param name="aFormat"></param>
        /// <param name="aQuery"></param>
        /// <returns></returns>
        private string GetCommPageSearch(string[] aTitles, string[] aFields, string[] aFormat, string[] aQuery)
        {
            StringBuilder sb = new StringBuilder();


            sb.Append("<table width=\"100%\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tbody>");
            sb.Append("<tr>");
            sb.Append("<td width=\"60\" >���ݵ�λ��</td>");
            sb.Append("<td width=\"120\" align=\"left\"><input name=\"txtAreaName\" class=\"detailedViewTextBox\" type=\"text\" title=\"�ؼ���\">��</td><td width=\"20\">&nbsp;</td>");
            sb.Append("<td width=\"65\" align=\"right\">�������ڣ�</td>");
            sb.Append("<td width=\"200\" align=\"left\">");
            sb.Append("<input name=\"txtReportDateStart\" class=\"detailedViewTextBox\" type=\"text\" title=\"ѡ��ʼ����\" readonly=\"readonly\" size=\"8\" onclick=\"PopCalendar(txtReportDateStart);return false;\"> ���� ");
            sb.Append("<input name=\"txtReportDateEnd\" class=\"detailedViewTextBox\" type=\"text\" title=\"ѡ���ֹ����\" readonly=\"readonly\" size=\"8\" onclick=\"PopCalendar(txtReportDateEnd);return false;\">��</td><td width=\"20\">&nbsp;</td>");
            int j = 0;
            for (int i = 0; i < aQuery.Length; i++)
            {
                if (aQuery[i].Trim() == "1")
                {
                    sb.Append("<td width=\"100\" align=\"right\">" + aTitles[i] + "��</td>");
                    if (aFormat[i].Trim() == "1")
                    {
                        sb.Append("<td width=\"200\" align=\"left\"><input name=\"txt" + aFields[i] + "Start\" class=\"detailedViewTextBox\" type=\"text\" title=\"ѡ��ʼ����\" readonly=\"readonly\" size=\"8\" onclick=\"JTC.setday({format:'yyyy-MM-dd', readOnly:false})\"> ���� ");
                        sb.Append("<input name=\"txt" + aFields[i] + "End\" class=\"detailedViewTextBox\" type=\"text\" title=\"ѡ���ֹ����\" readonly=\"readonly\" size=\"8\" onclick=\"JTC.setday({format:'yyyy-MM-dd', readOnly:false})\">��</td><td width=\"20\">&nbsp;</td>");
                    }
                    else
                    {
                        sb.Append("<td width=\"120\" align=\"left\"><input name=\"txt" + aFields[i] + "\" class=\"detailedViewTextBox\" type=\"text\" title=\"�ؼ���\">��</td><td width=\"20\">&nbsp;</td>");
                    }
                    j++;
                }
            }
            sb.Append("<td width=\"20\">&nbsp;</td>");
            sb.Append("<td width=\"100\" align=\"left\"><input value=\" �������� \" type=\"submit\"/></td>");
            sb.Append("<td>&nbsp;</td></tr>");
            sb.Append("</tbody></table>");

            return sb.ToString();
        }

        private string GetFilterStr(string htmlCtrlName, bool isCharacter)
        {
            string sHtml = string.Empty;
            sHtml += "<select id=\"" + htmlCtrlName + "\" name=\"" + htmlCtrlName + "\" class=\"detailedViewTextBox\">";
            sHtml += "<option value=\"like\" selected>ģ��</option>";
            sHtml += "<option value=\"=\">��ȷ</option>";
            sHtml += "</select>";

            return sHtml;
        }
        // 
        #endregion

        #endregion
    }
}
