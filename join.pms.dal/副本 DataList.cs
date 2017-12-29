using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Web;

using UNV.Comm.DataBase;

namespace join.pms.dal
{
    public class 副本DataList
    {
        #region  PageData 成员属性
        // 是否在框架内显示
        private bool _IsInFrame;
        public bool IsInFrame
        {
            set { _IsInFrame = value; }
            get { return _IsInFrame; }
        }
        // 页地址参数 TabPage
        private string _Url;
        public string Url
        {
            set { _Url = value; }
            get { return _Url; }
        }
        // 选项卡编号
        private string _TabPageNo;
        public string TabPageNo
        {
            set { _TabPageNo = value; }
            get { return _TabPageNo; }
        }
        // 搜索关键词
        private string _SearchKeys;
        public string SearchKeys
        {
            set { _SearchKeys = value; }
            get { return _SearchKeys; }
        }
        // 搜索类型
        private string _SearchType;
        public string SearchType
        {
            set { _SearchType = value; }
            get { return _SearchType; }
        }
        // 搜索条件
        private string _SearchWhere;
        public string SearchWhere
        {
            set { _SearchWhere = value; }
            get { return _SearchWhere; }
        }
        // 搜索字段
        private string _SearchFields;
        public string SearchFields
        {
            set { _SearchFields = value; }
            get { return _SearchFields; }
        }
        // =======================================
        // 表名
        private string _TableName;
        public string TableName
        {
            set { _TableName = value; }
            get { return _TableName; }
        }
        // 主键字段名
        private string _PrimaryKey;
        public string PrimaryKey
        {
            set { _PrimaryKey = value; }
            get { return _PrimaryKey; }
        }
        // 每页显示的记录数
        private int _PageSize;
        public int PageSize
        {
            set { _PageSize = value; }
            get { return _PageSize; }
        }
        // 页码
        private int _PageNo;
        public int PageNo
        {
            set { _PageNo = value; }
            get { return _PageNo; }
        }
        // 总记录数
        private int _TotalRec;
        public int TotalRec
        {
            set { _TotalRec = value; }
            get { return _TotalRec; }
        }
        // =======================================
        // 业务编号
        private string _BizID;
        public string BizID
        {
            set { _BizID = value; }
            get { return _BizID; }
        }
        // 功能编号
        private string _FuncNo;
        public string FuncNo
        {
            set { _FuncNo = value; }
            get { return _FuncNo; }
        }
        // 功能名称
        private string _FuncName;
        public string FuncName
        {
            set { _FuncName = value; }
            get { return _FuncName; }
        }
        // 功能权限
        private string _FuncPowers;
        public string FuncPowers
        {
            set { _FuncPowers = value; }
            get { return _FuncPowers; }
        }
        // 扩展名
        private string _FileExt;
        public string FileExt
        {
            set { _FileExt = value; }
            get { return _FileExt; }
        }
        // 选项卡编号
        private string _FuncTreeName;
        public string FuncTreeName
        {
            set { _FuncTreeName = value; }
            get { return _FuncTreeName; }
        }
        // 功能信息
        private string m_FuncInfo;
        // 标题
        private string m_Titles;
        // 字段
        private string m_Fields;
        public string FieldsName
        {
            set { m_Fields = value; }
            get { return m_Fields; }
        }
        // 列宽
        private string m_Width;
        // 列对齐方式
        private string m_Align;
        // 字符格式
        private string m_Format;
        public string FieldsFormat
        {
            set { m_Format = value; }
            get { return m_Format; }
        }
        // TAB页名称
        private string m_TabName;
        // TAB页链接
        private string m_TabLink;
        // TAB页显示
        private string m_TabVisible;
        // 按钮名称
        private string m_ButName;
        // 按钮链接
        private string m_ButLink;
        // 按钮显示
        private string m_ButVisible;
        // 通用搜索
        private string m_Query;
        // 关键数据标识
        private string m_Analys;
        // 数据行单员格超级链接地址
        private string m_RowLink;
        //数据行图片链接
        private string m_PicSvrUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];
        private string m_PicLink;

        // ===========================================
        private DataSet m_Ds;

        #endregion

        #region  PageData 成员方法

        #region  获取配置文件参数
        /// <summary>
        /// 配置数据集
        /// </summary>
        private void ConfigDataSet()
        {
            m_Ds = new DataSet();
            m_Ds.Locale = System.Globalization.CultureInfo.InvariantCulture;
        }
        /// <summary>
        /// 获取配置文件参数
        /// </summary>
        /// <param name="funcNo">功能号</param>
        /// <param name="configFile">配置文件路径</param>
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
                //if (funcNo.Substring(0, 2) == "01")
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
                errorMsg = "获取配置文件参数失败：" + ex.Message;
                return false;
            }
        }
        #endregion

        #region  获取分页数据

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <returns></returns>
        public int GetTotalRowsNum(string[] funcInfo)
        {
            // 0表名,1主键名,2功能名称,3是否显示checkbox,4新增,5url,6编辑,7url,8删除,9url -->
            this.TableName = funcInfo[0];
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
        /// 分页获取数据列表
        /// </summary>
        private bool GetPageDataSet(ref string errlrMsg)
        {
            /*
            @tblName varchar(255), -- 表名 
	        @fldName varchar(255), -- 主键字段名 
	        @PageSize int = 10, -- 页尺寸 
	        @PageIndex int = 1, -- 页码 
	        @IsReCount bit = 0, -- 返回记录总数, 非 0 值则返回 
	        @OrderType bit = 0, -- 设置排序类型, 非 0 值则降序 
	        @strWhere varchar(1000) = '' -- 查询条件 (注意: 不要加 where) 
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
        /// 获取页码导航信息
        /// </summary>
        /// <param name="pageRec">本页显示的记录数</param>
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
            pagestr += PageNo > 1 ? "<a href=\"" + this.Url + "&p=1&k=" + HttpUtility.UrlEncode(this.SearchKeys) + "\">首页</a><a href=\"" + this.Url + "&k=" + HttpUtility.UrlEncode(SearchKeys) + "&p=" + pre + "\">上一页</a>" : "<span class=\"off\">首页</span><span class=\"off\">上一页</span>";
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += PageNo == i ? "<span class=\"on\">" + i + "</span>" : "<a href=\"" + this.Url + "&k=" + HttpUtility.UrlEncode(SearchKeys) + "&p=" + i + "\">" + i + "</a>";
            }
            pagestr += PageNo != allpage ? "<a href=\"" + this.Url + "&p=" + next + "&k=" + HttpUtility.UrlEncode(SearchKeys) + "\">下一页</a><a href=\"" + this.Url + "&k=" + HttpUtility.UrlEncode(SearchKeys) + "&p=" + allpage + "\">末页</a>" : "<span class=\"off\">下一页</span><span class=\"off\">末页</span>";
            //pagestr += "&nbsp;&nbsp;页码：第[" + PageNo + "]页/共[" + allpage + "]页&nbsp;&nbsp;记录数：本页[ " + pageRec + " ]/总[ " + this.TotalRec + " ]";
            pagestr += "<span class=\"amount\">&nbsp;&nbsp;页码：<b>" + PageNo + "</b> / " + allpage + "&nbsp;&nbsp;记录数：<b>" + pageRec + "</b> / " + this.TotalRec + "</span>";

            return pagestr;
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <returns>根据属性返回相应页码的数据</returns>
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
            string objFieldsValue = string.Empty;
            string objCheckBoxParams = string.Empty;
            string pageCheckBox = a_FuncInfo[3];
            string objStatus = string.Empty;

            // 获取总记录数
            this.TotalRec = GetTotalRowsNum(a_FuncInfo);
            // 获取本页数据
            if (GetPageDataSet(ref errorMsg))
            {
                try
                {
                    // this.FuncNo 2013/06/24 变更为只显示前10列的数据 02,06,11 
                    //if (this.FuncNo == "xxxxxx") { }
                    //else
                    //{
                    //    if (this.FuncNo.Substring(0, 2) == "01")
                    //    {
                    //        a_Analys = this.m_Analys.Split(',');
                    //        int c = a_Fields.Length - 13; // 数据日期,比对日期,状态
                    //        string[] aryNew = new string[a_Fields.Length - c];
                    //        if (c > 0)
                    //        {
                    //            Array.Copy(a_Titles, aryNew, 10);
                    //            Array.Copy(a_Titles, 10 + c, aryNew, 10, 3);
                    //            a_Titles = aryNew;
                    //            aryNew = null;

                    //            aryNew = new string[13];
                    //            Array.Copy(a_Fields, aryNew, 10);
                    //            Array.Copy(a_Fields, 10 + c, aryNew, 10, 3);
                    //            a_Fields = aryNew;
                    //            aryNew = null;

                    //            aryNew = new string[13];
                    //            Array.Copy(a_Width, aryNew, 10);
                    //            Array.Copy(a_Width, 10 + c, aryNew, 10, 3);
                    //            a_Width = aryNew;
                    //            aryNew = null;

                    //            aryNew = new string[13];
                    //            Array.Copy(a_Align, aryNew, 10);
                    //            Array.Copy(a_Align, 10 + c, aryNew, 10, 3);
                    //            a_Align = aryNew;
                    //            aryNew = null;

                    //            aryNew = new string[13];
                    //            Array.Copy(a_Format, aryNew, 10);
                    //            Array.Copy(a_Format, 10 + c, aryNew, 10, 3);
                    //            a_Format = aryNew;
                    //            aryNew = null;

                    //            aryNew = new string[13];
                    //            Array.Copy(a_Link, aryNew, 10);
                    //            Array.Copy(a_Link, 10 + c, aryNew, 10, 3);
                    //            a_Link = aryNew;
                    //            aryNew = null;
                    //        }
                    //    }
                    //    else if (this.FuncNo.Substring(0, 2) == "11xx")
                    //    {
                    //        int c = a_Fields.Length - 12; // 数据日期,状态
                    //        string[] aryNew = new string[a_Fields.Length - c];
                    //        if (c > 0)
                    //        {
                    //            Array.Copy(a_Titles, aryNew, 10);
                    //            Array.Copy(a_Titles, 10 + c, aryNew, 10, 2);
                    //            a_Titles = aryNew;
                    //            aryNew = null;

                    //            aryNew = new string[12];
                    //            Array.Copy(a_Fields, aryNew, 10);
                    //            Array.Copy(a_Fields, 10 + c, aryNew, 10, 2);
                    //            a_Fields = aryNew;
                    //            aryNew = null;

                    //            aryNew = new string[12];
                    //            Array.Copy(a_Width, aryNew, 10);
                    //            Array.Copy(a_Width, 10 + c, aryNew, 10, 2);
                    //            a_Width = aryNew;
                    //            aryNew = null;

                    //            aryNew = new string[12];
                    //            Array.Copy(a_Align, aryNew, 10);
                    //            Array.Copy(a_Align, 10 + c, aryNew, 10, 2);
                    //            a_Align = aryNew;
                    //            aryNew = null;

                    //            aryNew = new string[12];
                    //            Array.Copy(a_Format, aryNew, 10);
                    //            Array.Copy(a_Format, 10 + c, aryNew, 10, 2);
                    //            a_Format = aryNew;
                    //            aryNew = null;

                    //            aryNew = new string[12];
                    //            Array.Copy(a_Link, aryNew, 10);
                    //            Array.Copy(a_Link, 10 + c, aryNew, 10, 2);
                    //            a_Link = aryNew;
                    //            aryNew = null;
                    //        }
                    //    }
                    //    else { }
                    //}



                    DataRow[] dr = m_Ds.Tables[0].Select();
                    int rowsNum = dr.Length;
                    int colsNum = a_Fields.Length;
                    int rowsOrderNo = 0; // 顺序号
                    int rowsLen = 0;
                    string tableWidth = GetTableWidth(a_Width);
                    this.FuncTreeName = CommPage.GetAllTreeName(this.FuncNo, true);
                    pageNav = GetPageNavStr(dr.Length);
                    // 导航信息 当前位置： <div class="mbx">当前位置：</div>
                    sb.Append("<div class=\"mbx\">");
                    sb.Append("当前位置：" + FuncTreeName + "(" + a_FuncInfo[2] + ")");
                    sb.Append("</div>");
                    //通用查询
                    //if (this.FuncNo.Substring(0, 2) == "02" || this.FuncNo.Substring(0, 2) == "05" || this.FuncNo.Substring(0, 2) == "06")
                    //{
                    //    sb.Append("<div class=\"mbx\">");
                    //    sb.Append(GetCommPageSearch(a_Titles, a_Fields, a_Format, this.m_Query.Split(',')));
                    //    sb.Append("</div>");
                    //}

                    // 操作按钮
                    sb.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\"><tr><td height=\"25\" class=\"butCss_01\"> ");
                    sb.Append(OprateButtonInfo(a_FuncInfo, a_FuncPowers));
                    sb.Append("</td></tr></table>");
                    //sb.Append("<table width=\"100%\" height=\"5\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td>&nbsp;</td></tr></table>");
                    sb.Append("");
                    // 列头
                    sb.Append("<!--list start-->");
                    sb.Append(GetPageSearch(a_Titles, a_Fields, a_Format));// 页面搜索
                    // 2013/01/16 by ysl 增加拖动表头和排序的功能 class=\"admintable\" class=\"adminth\"
                    // 2013/06/24 变更为只显示前10列的数据
                    // ===================  表头  =======================================================
                    //sb.Append("<table width=\"" + tableWidth + "\" class=\"admintable\" cellpadding=\"0\" cellspacing=\"0\"  id=\"DargTableByYsl\" border=\"1\"><thead><tr>");
                    sb.Append("<table width=\"" + tableWidth + "\" cellpadding=\"0\" cellspacing=\"0\"  id=\"DargTableByYsl\" border=\"0\"><thead><tr>");
                    if (pageCheckBox == "IsCheck") { sb.Append("<th width=\"50\" >&nbsp;<input name=\"itemsi\" id=\"0\" onclick=\"SelectAll();\" type=\"checkbox\" /></th>"); }
                    else { sb.Append("<th width=\"50\">序号</th>"); }
                    // class=\"adminth order\" order样式为排序用 width=\"" + a_Width[k] + "\" 
                    //if (this.FuncNo.Substring(0, 2) == "01")
                    //{
                    //    for (int k = 0; k < colsNum; k++)
                    //    {
                    //        if (a_Analys[k] != "null") { sb.Append("<th width=\"" + a_Width[k] + "\" class=\"color_01\">" + a_Titles[k] + "</th>"); }
                    //        else { sb.Append("<th width=\"" + a_Width[k] + "\">" + a_Titles[k] + "</th>"); }
                    //    }
                    //}
                    //else
                    //{
                    //    for (int k = 0; k < colsNum; k++)
                    //    {
                    //        sb.Append("<th width=\"" + a_Width[k] + "\">" + a_Titles[k] + "</th>");
                    //    }
                    //}

                    if (a_FuncInfo[11] == "IsEdit" || a_FuncInfo[12] == "IsDel") sb.Append("<th width=\"120\">操作</th>");
                    sb.Append("</tr></thead>");
                    // ==================== 数据输出<tbody>  ================================================================
                    for (int i = 0; i < rowsNum; i++)
                    {
                        rowsOrderNo = (this.PageNo - 1) * this.PageSize + i + 1;
                        objID = dr[i]["" + this.PrimaryKey + ""].ToString();

                        if (pageCheckBox == "IsCheck") objCheckBoxParams = "SetCheckBoxClick('" + objID + "');";// 限制cbx只支持单选 objCheckBoxParams = "SetCheckBoxClear(" + objID + ");";
                        //if (this.FuncNo == "020101" || this.FuncNo == "020404" || FuncNo == "020701" || FuncNo == "020702")
                        //{
                        //    if (dr[i]["" + a_Fields[0] + ""].ToString().IndexOf("合计")>-1)
                        //    {
                        //        sb.Append("<tr style=\"background:#ffccff;\" onmouseover=\"this.style.backgroundColor='#FFFFCC'\" onmouseout=\"this.style.backgroundColor='#FFFFFF'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                        //    }
                        //    else {
                        //        sb.Append("<tr style=\"background:#FFFFFF;\" onmouseover=\"this.style.backgroundColor='#FFFFCC'\" onmouseout=\"this.style.backgroundColor='#FFFFFF'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                        //    }
                        //}
                        //else 
                        //{
                        //    sb.Append("<tr style=\"background:#FFFFFF;\" onmouseover=\"this.style.backgroundColor='#FFFFCC'\" onmouseout=\"this.style.backgroundColor='#FFFFFF'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                        //}
                        //sb.Append("<tr style=\"background:#FFFFFF;\" onmouseover=\"this.style.backgroundColor='#FFFFCC'\" onmouseout=\"this.style.backgroundColor='#FFFFFF'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                        if (i % 2 == 0)
                        {
                            sb.Append("<tr class=\"rowBai\"  onmouseover=\"this.className='rowHover'\" onmouseout=\"this.className='rowBai'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                        }
                        else
                        {
                            sb.Append("<tr class=\"rowHui\"  onmouseover=\"this.className='rowHover'\" onmouseout=\"this.className='rowHui'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                        }
                        // 复选框及顺序号 align=\"center\"
                        if (pageCheckBox == "IsCheck") { sb.Append("<td > &nbsp;<input name=\"ItemChk\" type=\"checkbox\" id=\"" + objID + "\" value=\"" + objID + "\" onclick=\"" + objCheckBoxParams + "\"></td>"); }
                        else { sb.Append("<td>&nbsp;" + rowsOrderNo.ToString() + "</td>"); }
                        // 数据 width=\"" + a_Width[j] + "\"
                        for (int j = 0; j < colsNum; j++)
                        {
                            objFieldsValue = FormatString(dr[i]["" + a_Fields[j] + ""].ToString(), a_Format[j]).Trim(); // 格式化数据 title=\"" + objFieldsValue + "\"
                            if (a_Width[j] != "*")
                            {
                                if (a_Titles[j].IndexOf("身份证号") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("证号") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("号码") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("账号") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("编号") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("编码") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("名称") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("电话") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("日期") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("时间") > -1) { rowsLen = int.Parse(a_Width[j]); }
                                else if (a_Titles[j].IndexOf("用户") > -1) { rowsLen = int.Parse(a_Width[j]); }
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
                                    if (a_Titles[j].IndexOf("地址") > -1 || a_Titles[j].IndexOf("住址") > -1 || a_Titles[j].IndexOf("单位") > -1)
                                    {
                                        if (objFieldsValue.Length > 20)
                                        {
                                            sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, 20), objID, a_Link[j], objStatus) + "…</td>");
                                        }
                                        else
                                        {
                                            sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue, objID, a_Link[j], objStatus) + "…</td>");
                                        }

                                    }
                                    else
                                    {
                                        if (objFieldsValue.Length > 50)
                                        {
                                            sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, 50), objID, a_Link[j], objStatus) + "…</td>");
                                        }
                                        //else if (objFieldsValue.Length > 6 && objFieldsValue.Length < 10)
                                        //{
                                        //    sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, 5), objID, a_Link[j], objStatus) + "…</td>");
                                        //}
                                        //else if (objFieldsValue.Length > 3 && objFieldsValue.Length < 7)
                                        //{
                                        //    sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, 4), objID, a_Link[j], objStatus) + "…</td>");
                                        //}
                                        else
                                        {
                                            sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue, objID, a_Link[j], objStatus) + "</td>");
                                        }

                                    }
                                }
                                else
                                {
                                    if (a_Titles[j].IndexOf("地址") > -1 || a_Titles[j].IndexOf("住址") > -1 || a_Titles[j].IndexOf("单位") > -1)
                                    {
                                        if (objFieldsValue.Length > 20)
                                        {
                                            sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, 20), objID, a_Link[j], objStatus) + "…</td>");
                                        }
                                        else
                                        {
                                            sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue, objID, a_Link[j], objStatus) + "</td>");
                                        }
                                    }
                                    else
                                    {
                                        if (a_Titles[j].IndexOf("上报状态") > -1 || a_Titles[j].IndexOf("审核状态") > -1)
                                        {
                                            if (objFieldsValue == "0")
                                            {
                                                objFieldsValue = "未上报";
                                            }
                                            if (objFieldsValue == "1")
                                            {
                                                objFieldsValue = "已上报";
                                            }
                                            if (objFieldsValue == "2")
                                            {
                                                objFieldsValue = "未审核";
                                            }
                                            if (objFieldsValue == "3")
                                            {
                                                objFieldsValue = "已审核";
                                            }
                                            if (objFieldsValue == "4")
                                            {
                                                objFieldsValue = "无效";
                                            }
                                        }
                                        sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + ">" + FormatLink(objFieldsValue, objID, a_Link[j], objStatus) + "</td>");
                                    }
                                }
                            }
                            //-------------------------
                        }
                        // 新增 编辑 删除 功能1 功能2 功能3 功能4 功能5 功能6 
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
                                    if (a_FuncPowers[1] == "1" && a_FuncInfo[11] == "IsEdit") sb.Append("<a class=\"edit\" title=\"编辑\" href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[7] + "&k=" + objID + "','','edit');\"></a>");
                                    if (a_FuncPowers[2] == "1" && a_FuncInfo[12] == "IsDel") sb.Append("<a class=\"del\" title=\"删除\" href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[9] + "&k=" + objID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','del');\"></a></td>");
                                    sb.Append("</td>");
                                }
                                else { sb.Append("<td>&nbsp;</td>"); }
                            }
                            else { }
                        }
                        else
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                    //sb.Append("</tbody></table>");
                    sb.Append("</table>");
                    // 页码信息
                    sb.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"10\">");
                    sb.Append("<tr><td><div align=\"left\" class=\"page\">" + pageNav + "</div></td>");
                    sb.Append("</tr></table>");
                    /* 2013/06/24保存变更前代码
                     // ===================  表头  =======================================================
                    //sb.Append("<table width=\"" + tableWidth + "\" class=\"admintable\" cellpadding=\"0\" cellspacing=\"0\"  id=\"DargTableByYsl\" border=\"1\"><thead><tr>");
                    sb.Append("<table width=\"" + tableWidth + "\" cellpadding=\"0\" cellspacing=\"0\"  id=\"DargTableByYsl\" border=\"0\"><thead><tr>");
                    if (pageCheckBox == "IsCheck") { sb.Append("<th width=\"50\"><input name=\"itemsi\" id=\"0\" onclick=\"SelectAll();\" type=\"checkbox\" /></th>"); }
                    else { sb.Append("<th width=\"50\">序号</th>"); }
                    // class=\"adminth order\" order样式为排序用 width=\"" + a_Width[k] + "\"
                    for (int k = 0; k < colsNum; k++)
                    {
                        sb.Append("<th width=\"" + a_Width[k] + "\">" + a_Titles[k] + "</th>");
                    }
                    if (a_FuncInfo[11] == "IsEdit" || a_FuncInfo[12] == "IsDel") sb.Append("<th width=\"120\">操作</th>");
                    sb.Append("</tr></thead>");
                    // ==================== 数据输出<tbody>  =============================
                    for (int i = 0; i < rowsNum; i++)
                    {
                        rowsOrderNo = (this.PageNo - 1) * this.PageSize + i + 1;
                        objID = dr[i]["" + this.PrimaryKey + ""].ToString();

                        if (pageCheckBox == "IsCheck") objCheckBoxParams = "SetCheckBoxClick('" + objID + "');";// 限制cbx只支持单选 objCheckBoxParams = "SetCheckBoxClear(" + objID + ");";
                        if (this.FuncNo == "020101" || this.FuncNo == "020404" || FuncNo == "020701" || FuncNo == "020702")
                        {
                            if (dr[i]["" + a_Fields[0] + ""].ToString().IndexOf("合计")>-1)
                            {
                                sb.Append("<tr style=\"background:#ffccff;\" onmouseover=\"this.style.backgroundColor='#FFFFCC'\" onmouseout=\"this.style.backgroundColor='#FFFFFF'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                            }
                            else {
                                sb.Append("<tr style=\"background:#FFFFFF;\" onmouseover=\"this.style.backgroundColor='#FFFFCC'\" onmouseout=\"this.style.backgroundColor='#FFFFFF'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                            }
                        }
                        else 
                        {
                            sb.Append("<tr style=\"background:#FFFFFF;\" onmouseover=\"this.style.backgroundColor='#FFFFCC'\" onmouseout=\"this.style.backgroundColor='#FFFFFF'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                        }
                        
                        // 复选框及顺序号
                        if (pageCheckBox == "IsCheck") { sb.Append("<td align=\"center\"><input name=\"ItemChk\" type=checkbox id=" + objID + " value=" + objID + " onclick=\"" + objCheckBoxParams + "\"></td>"); }
                        else { sb.Append("<td>" + rowsOrderNo.ToString() + "</td>"); }
                        // 数据 width=\"" + a_Width[j] + "\"
                        for (int j = 0; j < colsNum; j++)
                        {
                            objFieldsValue = FormatString(dr[i]["" + a_Fields[j] + ""].ToString(), a_Format[j]); // 格式化数据
                            //if (a_Width[j] != "*") { rowsLen = int.Parse(a_Width[j])/10; } else { rowsLen = 350; } title=\"" + objFieldsValue + "\"
                            if (a_Width[j] != "*") { rowsLen = int.Parse(a_Width[j]); } else { rowsLen = 350; }
                            
                            if (objFieldsValue.Length > rowsLen + 4)
                            {
                                sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + ">" + FormatLink(objFieldsValue.Substring(0, rowsLen), objID, a_Link[j], objStatus) + "...</td>");
                            }
                            else
                            {
                                sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + ">" + FormatLink(objFieldsValue, objID, a_Link[j], objStatus) + "</td>");
                            }
                        }
                        // 新增 编辑 删除 功能1 功能2 功能3 功能4 功能5 功能6 
                        if (a_FuncPowers != null && a_FuncPowers.Length > 2)
                        {
                            if (a_FuncInfo[11] == "IsEdit" || a_FuncInfo[12] == "IsDel")
                            {
                                if (a_FuncPowers[1] == "1" || a_FuncPowers[2] == "1")
                                {
                                    sb.Append("<td align=\"center\">");
                                    if (a_FuncPowers[1] == "1" && a_FuncInfo[11] == "IsEdit") sb.Append("<img src=\"images/icon-edit.gif\"/><a href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[7] + "&k=" + objID + "','','edit');\">" + a_FuncInfo[6] + "</a> ");
                                    //if (a_FuncPowers[1] == "1" && a_FuncPowers[2] == "1" && a_FuncInfo[11] == "IsEdit" && a_FuncInfo[12] == "IsDel") sb.Append(" | ");
                                    if (a_FuncPowers[2] == "1" && a_FuncInfo[12] == "IsDel") sb.Append("<img src=\"images/icon-del.gif\"/><a href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[9] + "&k=" + objID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','del');\">" + a_FuncInfo[8] + "</a></td>");
                                    sb.Append("</td>");
                                }
                                else { sb.Append("<td>&nbsp;</td>"); }
                            }else {  }
                        }
                        else
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                    //sb.Append("</tbody></table>");
                    sb.Append("</table>");
                    // 页码信息
                    sb.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"10\">");
                    sb.Append("<tr><td><div align=\"left\" class=\"zhengwen\">" + pageNav + "</div></td>");
                    sb.Append("</tr></table>");
                     */
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

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <returns>根据属性返回相应页码的数据</returns>
        public string GetList020101()
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
            string objFieldsValue = string.Empty;
            string objCheckBoxParams = string.Empty;
            string pageCheckBox = a_FuncInfo[3];
            string objStatus = string.Empty;

            // 获取总记录数
            this.TotalRec = GetTotalRowsNum(a_FuncInfo);
            // 获取本页数据
            if (GetPageDataSet(ref errorMsg))
            {
                try
                {
                    // this.FuncNo 2013/06/24 变更为只显示前10列的数据 02,06,11 020102

                    a_Analys = this.m_Analys.Split(',');



                    DataRow[] dr = m_Ds.Tables[0].Select();
                    int rowsNum = dr.Length;
                    int colsNum = a_Fields.Length;
                    int rowsOrderNo = 0; // 顺序号
                    int rowsLen = 0;
                    string tableWidth = GetTableWidth(a_Width);
                    this.FuncTreeName = CommPage.GetAllTreeName(this.FuncNo, true);
                    pageNav = GetPageNavStr(dr.Length);
                    // 导航信息 当前位置： <div class="mbx">当前位置：</div>
                    sb.Append("<div class=\"mbx\">");
                    sb.Append("当前位置：" + FuncTreeName + "(" + a_FuncInfo[2] + ")");
                    sb.Append("</div>");
                    //通用查询
                    //if (this.FuncNo.Substring(0, 2) == "02" || this.FuncNo.Substring(0, 2) == "05" || this.FuncNo.Substring(0, 2) == "06")
                    //{
                    //    sb.Append("<div class=\"mbx\">");
                    //    sb.Append(GetCommPageSearch(a_Titles, a_Fields, a_Format, this.m_Query.Split(',')));
                    //    sb.Append("</div>");
                    //}

                    // 操作按钮
                    sb.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\"><tr><td height=\"25\" class=\"butCss_01\"> ");
                    sb.Append(OprateButtonInfo(a_FuncInfo, a_FuncPowers));
                    sb.Append("</td></tr></table>");
                    //sb.Append("<table width=\"100%\" height=\"5\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td>&nbsp;</td></tr></table>");
                    sb.Append("");
                    // 列头
                    sb.Append("<!--list start-->");
                    sb.Append(GetPageSearch(a_Titles, a_Fields, a_Format));// 页面搜索
                    // 2013/01/16 by ysl 增加拖动表头和排序的功能 class=\"admintable\" class=\"adminth\"
                    // 2013/06/24 变更为只显示前10列的数据
                    // ===================  表头  =======================================================
                    //sb.Append("<table width=\"" + tableWidth + "\" class=\"admintable\" cellpadding=\"0\" cellspacing=\"0\"  id=\"DargTableByYsl\" border=\"1\"><thead><tr>");
                    sb.Append("<table width=\"" + tableWidth + "\" cellpadding=\"0\" cellspacing=\"0\"  id=\"DargTableByYsl\" border=\"0\"><thead><tr>");
                    if (pageCheckBox == "IsCheck") { sb.Append("<th width=\"50\"><input name=\"itemsi\" id=\"0\" onclick=\"SelectAll();\" type=\"checkbox\" /></th>"); }
                    else { sb.Append("<th width=\"50\">序号</th>"); }
                    // class=\"adminth order\" order样式为排序用 width=\"" + a_Width[k] + "\" 

                    string strC = string.Empty;
                    int intLen = 0;
                    sb.Append("<th width=\"" + a_Width[0] + "\"><div style=\"width:" + a_Width[0] + "px;\">" + a_Titles[0] + "</div></th>");

                    for (int k = 1; k < colsNum; k++)
                    {
                        strC += "<th width=\"" + a_Width[k] + "\"><div style=\"width:" + a_Width[k] + "px;\">" + a_Titles[k] + "</div></th>";
                        intLen += int.Parse(a_Width[k]);
                    }
                    sb.Append("<th width=\"" + intLen.ToString() + "\" colspan=\"" + (colsNum - 1) + "\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tr>");
                    sb.Append("<th width=\"100\"><div style=\"width:100px;\">数据类型</div></th>");
                    sb.Append(strC);
                    sb.Append("</tr></table></th>");


                    if (a_FuncInfo[11] == "IsEdit" || a_FuncInfo[12] == "IsDel") sb.Append("<th width=\"120\"><div style=\"width:120px;\">操作</div></th>");
                    sb.Append("</tr></thead>");
                    // ==================== 数据输出<tbody>  =============================
                    for (int i = 0; i < rowsNum; i++)
                    {
                        rowsOrderNo = (this.PageNo - 1) * this.PageSize + i + 1;
                        objID = dr[i]["" + this.PrimaryKey + ""].ToString();

                        if (pageCheckBox == "IsCheck") objCheckBoxParams = "SetCheckBoxClick('" + objID + "');";// 限制cbx只支持单选 objCheckBoxParams = "SetCheckBoxClear(" + objID + ");";
                        //if (this.FuncNo == "020101" || this.FuncNo == "020404" || FuncNo == "020701" || FuncNo == "020702")
                        //{
                        //    if (dr[i]["" + a_Fields[0] + ""].ToString().IndexOf("合计")>-1)
                        //    {
                        //        sb.Append("<tr style=\"background:#ffccff;\" onmouseover=\"this.style.backgroundColor='#FFFFCC'\" onmouseout=\"this.style.backgroundColor='#FFFFFF'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                        //    }
                        //    else {
                        //        sb.Append("<tr style=\"background:#FFFFFF;\" onmouseover=\"this.style.backgroundColor='#FFFFCC'\" onmouseout=\"this.style.backgroundColor='#FFFFFF'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                        //    }
                        //}
                        //else 
                        //{
                        //    sb.Append("<tr style=\"background:#FFFFFF;\" onmouseover=\"this.style.backgroundColor='#FFFFCC'\" onmouseout=\"this.style.backgroundColor='#FFFFFF'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                        //}
                        sb.Append("<tr style=\"background:#FFFFFF;\" onmouseover=\"this.style.backgroundColor='#FFFFCC'\" onmouseout=\"this.style.backgroundColor='#FFFFFF'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");

                        // 复选框及顺序号
                        if (pageCheckBox == "IsCheck") { sb.Append("<td align=\"center\"><input name=\"ItemChk\" type=\"checkbox\" id=\"" + objID + "\" value=\"" + objID + "\" onclick=\"" + objCheckBoxParams + "\"></td>"); }
                        else { sb.Append("<td>" + rowsOrderNo.ToString() + "</td>"); }
                        // 数据 width=\"" + a_Width[j] + "\"

                        //与全员库比较 start
                        objFieldsValue = FormatString(dr[i]["" + a_Fields[0] + ""].ToString(), a_Format[0]); // 格式化数据 title=\"" + objFieldsValue + "\"
                        sb.Append("<td " + SetAlignAndColor(a_Align[0], objFieldsValue) + ">" + FormatLink(objFieldsValue, objID, a_Link[0], objStatus) + "</td>");
                        sb.Append("<td colspan=\"" + (colsNum - 1) + "\" style=\"padding: 0\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\">");
                        sb.Append("<tr >");
                        sb.Append("<td align=\"left\" width=\"100\" bgcolor=\"#CCFFFF\"><div style=\"width:100px;\">填报数据</div></td>");
                        for (int j = 1; j < colsNum; j++)
                        {
                            objFieldsValue = FormatString(dr[i]["" + a_Fields[j] + ""].ToString(), a_Format[j]); // 格式化数据 title=\"" + objFieldsValue + "\"
                            //if (a_Width[j] != "*")
                            //{ rowsLen = int.Parse(a_Width[j]) / 10; }
                            //else { rowsLen = 350; }
                            sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " width=\"" + a_Width[j] + "\" ><div style=\"width:" + a_Width[j] + "px;\">" + FormatLink(objFieldsValue, objID, a_Link[j], objStatus) + "</div></td>");

                        }
                        sb.Append("</tr>");
                        sb.Append("<tr class=\"trClass_t\" ><td align=\"left\" width=\"100\" bgcolor=\"#FFFFCC\"><div style=\"width:100px;\">全员库数据</div></td>" + GetPIS_QYK(dr[i]["" + a_Fields[0] + ""].ToString()) + "</tr>");
                        sb.Append("</table></td>");
                        // 新增 编辑 删除 功能1 功能2 功能3 功能4 功能5 功能6 
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
                                    if (a_FuncPowers[1] == "1" && a_FuncInfo[11] == "IsEdit") sb.Append("<a class=\"edit\" title=\"编辑\" href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[7] + "&k=" + objID + "','','edit');\"></a>");
                                    if (a_FuncPowers[2] == "1" && a_FuncInfo[12] == "IsDel") sb.Append("<a class=\"del\" title=\"删除\" href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[9] + "&k=" + objID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','del');\"></a></td>");
                                    sb.Append("</td>");
                                }
                                else { sb.Append("<td>&nbsp;</td>"); }
                            }
                            else { }
                        }
                        else
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }

                        sb.Append("</tr>");
                    }
                    //与全员库比较 end

                    //sb.Append("</tbody></table>");
                    sb.Append("</table>");
                    // 页码信息
                    sb.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"10\">");
                    sb.Append("<tr><td><div align=\"left\" class=\"page\">" + pageNav + "</div></td>");
                    sb.Append("</tr></table>");
                    /* 2013/06/24保存变更前代码
                     // ===================  表头  =======================================================
                    //sb.Append("<table width=\"" + tableWidth + "\" class=\"admintable\" cellpadding=\"0\" cellspacing=\"0\"  id=\"DargTableByYsl\" border=\"1\"><thead><tr>");
                    sb.Append("<table width=\"" + tableWidth + "\" cellpadding=\"0\" cellspacing=\"0\"  id=\"DargTableByYsl\" border=\"0\"><thead><tr>");
                    if (pageCheckBox == "IsCheck") { sb.Append("<th width=\"50\"><input name=\"itemsi\" id=\"0\" onclick=\"SelectAll();\" type=\"checkbox\" /></th>"); }
                    else { sb.Append("<th width=\"50\">序号</th>"); }
                    // class=\"adminth order\" order样式为排序用 width=\"" + a_Width[k] + "\"
                    for (int k = 0; k < colsNum; k++)
                    {
                        sb.Append("<th width=\"" + a_Width[k] + "\">" + a_Titles[k] + "</th>");
                    }
                    if (a_FuncInfo[11] == "IsEdit" || a_FuncInfo[12] == "IsDel") sb.Append("<th width=\"120\">操作</th>");
                    sb.Append("</tr></thead>");
                    // ==================== 数据输出<tbody>  =============================
                    for (int i = 0; i < rowsNum; i++)
                    {
                        rowsOrderNo = (this.PageNo - 1) * this.PageSize + i + 1;
                        objID = dr[i]["" + this.PrimaryKey + ""].ToString();

                        if (pageCheckBox == "IsCheck") objCheckBoxParams = "SetCheckBoxClick('" + objID + "');";// 限制cbx只支持单选 objCheckBoxParams = "SetCheckBoxClear(" + objID + ");";
                        if (this.FuncNo == "020101" || this.FuncNo == "020404" || FuncNo == "020701" || FuncNo == "020702")
                        {
                            if (dr[i]["" + a_Fields[0] + ""].ToString().IndexOf("合计")>-1)
                            {
                                sb.Append("<tr style=\"background:#ffccff;\" onmouseover=\"this.style.backgroundColor='#FFFFCC'\" onmouseout=\"this.style.backgroundColor='#FFFFFF'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                            }
                            else {
                                sb.Append("<tr style=\"background:#FFFFFF;\" onmouseover=\"this.style.backgroundColor='#FFFFCC'\" onmouseout=\"this.style.backgroundColor='#FFFFFF'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                            }
                        }
                        else 
                        {
                            sb.Append("<tr style=\"background:#FFFFFF;\" onmouseover=\"this.style.backgroundColor='#FFFFCC'\" onmouseout=\"this.style.backgroundColor='#FFFFFF'\" id=\"YGVtr\" onclick =\"document.getElementById('selectRowID').value='" + objID + "';" + objCheckBoxParams + "\" >");
                        }
                        
                        // 复选框及顺序号
                        if (pageCheckBox == "IsCheck") { sb.Append("<td align=\"center\"><input name=\"ItemChk\" type=checkbox id=" + objID + " value=" + objID + " onclick=\"" + objCheckBoxParams + "\"></td>"); }
                        else { sb.Append("<td>" + rowsOrderNo.ToString() + "</td>"); }
                        // 数据 width=\"" + a_Width[j] + "\"
                        for (int j = 0; j < colsNum; j++)
                        {
                            objFieldsValue = FormatString(dr[i]["" + a_Fields[j] + ""].ToString(), a_Format[j]); // 格式化数据
                            //if (a_Width[j] != "*") { rowsLen = int.Parse(a_Width[j])/10; } else { rowsLen = 350; } title=\"" + objFieldsValue + "\"
                            if (a_Width[j] != "*") { rowsLen = int.Parse(a_Width[j]); } else { rowsLen = 350; }
                            
                            if (objFieldsValue.Length > rowsLen + 4)
                            {
                                sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + ">" + FormatLink(objFieldsValue.Substring(0, rowsLen), objID, a_Link[j], objStatus) + "...</td>");
                            }
                            else
                            {
                                sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + ">" + FormatLink(objFieldsValue, objID, a_Link[j], objStatus) + "</td>");
                            }
                        }
                        // 新增 编辑 删除 功能1 功能2 功能3 功能4 功能5 功能6 
                        if (a_FuncPowers != null && a_FuncPowers.Length > 2)
                        {
                            if (a_FuncInfo[11] == "IsEdit" || a_FuncInfo[12] == "IsDel")
                            {
                                if (a_FuncPowers[1] == "1" || a_FuncPowers[2] == "1")
                                {
                                    sb.Append("<td align=\"center\">");
                                    if (a_FuncPowers[1] == "1" && a_FuncInfo[11] == "IsEdit") sb.Append("<img src=\"images/icon-edit.gif\"/><a href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[7] + "&k=" + objID + "','','edit');\">" + a_FuncInfo[6] + "</a> ");
                                    //if (a_FuncPowers[1] == "1" && a_FuncPowers[2] == "1" && a_FuncInfo[11] == "IsEdit" && a_FuncInfo[12] == "IsDel") sb.Append(" | ");
                                    if (a_FuncPowers[2] == "1" && a_FuncInfo[12] == "IsDel") sb.Append("<img src=\"images/icon-del.gif\"/><a href=\"#\" onclick=\"javascript:SetBizUrl('" + a_FuncInfo[9] + "&k=" + objID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','del');\">" + a_FuncInfo[8] + "</a></td>");
                                    sb.Append("</td>");
                                }
                                else { sb.Append("<td>&nbsp;</td>"); }
                            }else {  }
                        }
                        else
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        sb.Append("</tr>");
                    }
                    //sb.Append("</tbody></table>");
                    sb.Append("</table>");
                    // 页码信息
                    sb.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"10\">");
                    sb.Append("<tr><td><div align=\"left\" class=\"zhengwen\">" + pageNav + "</div></td>");
                    sb.Append("</tr></table>");
                     */
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
        DataTable dt;
        private string GetPIS_QYK(string strFileds01)
        {
            dt = new DataTable();
            StringBuilder shtml = new StringBuilder();
            string sqlParams = "SELECT TOP 1 Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,OprateDate FROM PIS_QYK WHERE Fileds01 LIKE '%" + strFileds01 + "%' ORDER BY OprateDate DESC";
            dt = DbHelperSQL.Query(sqlParams).Tables[0];

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Rows.Count == 1)
                {
                    if (i == (dt.Columns.Count - 1))
                    {
                        shtml.Append("<td>" + DateTime.Parse(dt.Rows[0][i].ToString()).ToString("yyyy-MM-dd") + "</td>");
                    }
                    else
                    {
                        shtml.Append("<td>" + dt.Rows[0][i] + "</td>");
                    }
                }
                else { shtml.Append("<td>&nbsp;</td>"); }
            }
            dt = null;
            return shtml.ToString();
        }

        public string GetPhotosList()
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
            string objFieldsValue = string.Empty;
            string objCheckBoxParams = string.Empty;
            string pageCheckBox = a_FuncInfo[3];
            string objStatus = string.Empty;

            // 获取总记录数
            this.TotalRec = GetTotalRowsNum(a_FuncInfo);
            // 获取本页数据
            if (GetPageDataSet(ref errorMsg))
            {
                try
                {
                    DataRow[] dr = m_Ds.Tables[0].Select();
                    int rowsNum = dr.Length;
                    int colsNum = a_Fields.Length;
                    string docType = "", docPath = "", docName = "", docDate = "", personNa = string.Empty;
                    this.FuncTreeName = CommPage.GetAllTreeName(this.FuncNo, true);
                    pageNav = GetPageNavStr(dr.Length);
                    // 导航信息 当前位置： <div class="mbx">当前位置：</div>
                    sb.Append("<div class=\"mbx\">");
                    sb.Append("当前位置：" + FuncTreeName + "(" + a_FuncInfo[2] + ")");
                    sb.Append("</div>");
                    // 操作按钮
                    //sb.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\"><tr><td height=\"25\" class=\"butCss_01\"> ");
                    //sb.Append(OprateButtonInfo(a_FuncInfo, a_FuncPowers));
                    //sb.Append("</td></tr></table>");
                    sb.Append(GetPageSearch(a_Titles, a_Fields, a_Format));// 页面搜索

                    sb.Append("<div class=\"photo_list\">");
                    sb.Append("<ul>");


                    for (int i = 0; i < rowsNum; i++)
                    {
                        //rowsOrderNo = (this.PageNo - 1) * this.PageSize + i + 1;
                        objID = dr[i]["" + this.PrimaryKey + ""].ToString();
                        docType = dr[i]["DocsType"].ToString();
                        docPath = dr[i]["DocsPath"].ToString();
                        docName = dr[i]["DocsName"].ToString();
                        docDate = dr[i]["OprateDate"].ToString();
                        personNa = dr[i]["PersonName"].ToString();
                        if (!string.IsNullOrEmpty(docDate)) docDate = DateTime.Parse(docDate).ToString("yyyy/MM/dd");
                        if (!string.IsNullOrEmpty(docPath)) docPath = docPath.Insert(docPath.LastIndexOf("/") + 1, "micro-");
                        // DocsType,DocsPath,DocsName,OprateDate
                        // PersonName,PersonTel,DocsType,DocsName,SourceName,OprateDate
                        // /Files/2015/06/201506021907214640.jpg
                        // Files/2015/06micro-/20150603161933991.gif

                        sb.Append("<li>");
                        sb.Append("<p class=\"pic\"><a href=\"#\" onclick=\"JavaScript:ChangeUrl('PhotoView.aspx?action=view&RID=" + objID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','view');\"><span class=\"pic_center\"><i></i><img src=\"" + m_PicSvrUrl + docPath + "\" alt=\"" + docName + "\" /></span></a></p>");
                        sb.Append("<p class=\"title\"><a href=\"#\" onclick=\"JavaScript:ChangeUrl('PhotoView.aspx?action=view&RID=" + objID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','view');\">" + docName + "</a></p>");
                        sb.Append("<p class=\"name\"><span>" + docDate + "</span><b>" + personNa + "</b></p>");
                        sb.Append("</li>");
                        //  return "<a href=\"" + m_PicSvrUrl + docPath + "\" targer=\"_blank\">" + inStr + "</a>";
                        //  return "<a href=\"#\" onclick=\"JavaScript:ChangeUrl('" + rowLink + "&RID=" + objID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','view');\">" + inStr + "</a>";
                    }
                    sb.Append("</ul>");
                    sb.Append("</div>");
                    // 页码信息
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

        #region  根据配置参数对显示数据进行控制

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
        /// 页面导航信息
        /// </summary>
        /// <returns></returns>
        private string GetPageTopNavInfo(string[] funcInfo)
        {
            // 0表名,1主键名,2功能名称,3是否显示checkbox,4新增,5url,6编辑,7url,8删除,9url,10 IsAdd,11 IsEdit,12 IsDel -->
            string addIcon = "images/btnL3Add.gif";
            if (funcInfo[10] == "NoAdd") addIcon = "images/btnL3Add-Faded.gif";
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"level2Bg\"><tbody><tr>");
            sb.Append("<td width=\"1\" height=\"30\">&nbsp;</td>");
            sb.Append("<td align=\"left\" style=\"padding-left: 10px; padding-right: 50px;\" class=\"moduleName\" nowrap=\"nowrap\">");
            switch (this.FuncNo)
            {
                case "1": //BizType 1,待反馈协查信息 2,成功办理的业务 3 不符合条件未予办理 ； 计生： 6,等待处理 7,已经处理
                    sb.Append("待反馈协查信息&nbsp;&nbsp;&nbsp;&nbsp;");
                    break;
                case "2":
                    sb.Append("成功办理的业务&nbsp;&nbsp;&nbsp;&nbsp;");
                    break;
                case "3":
                    sb.Append("不符合条件未予办理&nbsp;&nbsp;&nbsp;&nbsp;");
                    break;
                case "6":
                    sb.Append("等待处理的业务&nbsp;&nbsp;&nbsp;&nbsp;");
                    break;
                case "7":
                    sb.Append("已经处理的业务&nbsp;&nbsp;&nbsp;&nbsp;");
                    break;
                default:
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
                    break;
            }
            sb.Append("</td><td width=\"3\">&nbsp;</td></tr></tbody></table>");

            return sb.ToString();
        }

        /// <summary>
        /// 操作按钮信息
        /// </summary>
        /// <returns></returns>
        private string OprateButtonInfo(string[] funcInfo, string[] a_FuncPowers)
        {
            // 新增 编辑 删除 功能1 功能2 功能3 功能4 功能5 功能6 
            string[] a_ButName = this.m_ButName.Split(',');
            string[] a_ButLink = this.m_ButLink.Split(',');
            string[] a_ButVisible = this.m_ButVisible.Split(',');

            StringBuilder sb = new StringBuilder();
            // // 0表名,1主键名,2功能名称,3是否显示checkbox,4新增,5url,6编辑,7url,8删除,9url -->
            //操作按钮显示：0,隐藏；1,显示；    新增,编辑,删除,查看,审核,分配角色 getQueryData(document.getElementById('searchKey').value,'" + this.Url + "')
            // <input name=\"Submit\" type=\"button\" class=\"submit6\" value=\" 保 存 \">
            try
            {
                sb.Append("功能操作：");
                // 新增按钮
                if (a_FuncPowers[0] == "1" && funcInfo[10] == "IsAdd") { sb.Append("<input class=\"submit6 butGreen\" value=\"  " + funcInfo[4] + "  \" onclick=\"JavaScript:ChangeUrl('" + funcInfo[5] + "&BizID=" + BizID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','add');\" type=\"button\"/>&nbsp;"); }
                // 查询按钮
                sb.Append("<input class=\"submit6\" value=\"  查找  \" onclick=\"moveMe('advSearch');showSearchDiv('advSearch');\" type=\"button\"/>&nbsp;");
                /*
                 07 保健 批量删除,屏蔽,审核,公开,x,附件
01 共享 批量删除,导入,导出,审核,公开,数据采集,x,x,x
03 报表 填报,批量删除,导入,导出,审核,公开,x
04 信息 批量删除,屏蔽,审核,公开,x,附件
06 系统管理 >删除,审核帐号,重置密码,分配角色
                 */
                // 按钮样式
                string[] arybutCss = null;
                if (FuncNo.Substring(0, 2) == "01") { arybutCss = new string[] { "butRed", "butGreen", "butDefault", "butBlue", "butOrange", "butDefault", "butDefault", "butGreen", "butGreen" }; }
                else if (FuncNo.Substring(0, 2) == "03") { arybutCss = new string[] { "butBlue", "butRed", "butGreen", "butDefault", "butOrange", "butDefault", "butDefault", "butGreen", "butGreen" }; }
                else if (FuncNo.Substring(0, 2) == "04") { arybutCss = new string[] { "butRed", "butOrange", "butBlue", "butGreen", "butDefault", "butDefault", "butDefault", "butGreen", "butGreen" }; }
                else if (FuncNo.Substring(0, 2) == "07") { arybutCss = new string[] { "butRed", "butOrange", "butBlue", "butGreen", "butDefault", "butDefault", "butDefault", "butGreen", "butGreen" }; }
                else { arybutCss = new string[] { "butRed", "butOrange", "butBlue", "butGreen", "butDefault", "butDefault", "butDefault", "butGreen", "butGreen" }; }
                for (int i = 0; i < a_ButName.Length; i++)
                {
                    // 2009/07/11 增加设置权限集显示按钮 1,1,1,0,0,0,0,0,0
                    if (a_ButVisible[i].Trim() == "1" && a_FuncPowers[i + 3] == "1")
                    {
                        if (i == 0) { sb.Append("<input class=\"submit6 " + arybutCss[i] + "\" value=\"  " + a_ButName[i] + "  \" onclick=\"JavaScript:ChangeUrlMultCheck('" + a_ButLink[i] + "','" + HttpUtility.UrlEncode(this.FuncName) + "');\" type=\"button\"/>&nbsp;"); }
                        else { sb.Append("<input class=\"submit6 " + arybutCss[i] + "\" value=\"  " + a_ButName[i] + "  \" onclick=\"JavaScript:ChangeUrlMultCheck('" + a_ButLink[i] + "','" + HttpUtility.UrlEncode(this.FuncName) + "');\" type=\"button\"/>&nbsp;"); }
                    }
                }
            }
            catch (Exception ex)
            {
                sb.Append("获取操作按钮信息失败：" + ex.Message);
            }

            a_ButName = null;
            a_ButLink = null;
            a_ButVisible = null;
            return sb.ToString();
        }

        /// <summary>
        /// 输出的字符格式
        /// </summary>
        /// <param name="InStr">传入的字符</param>
        /// <param name="sType">字符类型</param>
        /// <returns></returns>
        private string FormatString(string InStr, string sType)
        {
            if (String.IsNullOrEmpty(InStr)) return "&nbsp;";

            try
            {
                switch (sType)
                {
                    case "0": // 文本
                        InStr = InStr.Replace("\r", "");
                        InStr = InStr.Replace("\n", "<br>");
                        return InStr;
                    case "1": // 日期
                        if (UNV.Comm.Web.PageValidate.IsDateTime(InStr))
                        {
                            return DateTime.Parse(InStr).ToString("yyyy/MM/dd"); //string.Format("{0:yyyy-MM-dd}",InStr); 
                        }
                        else { return InStr; }

                    case "2": // 数字
                        return InStr.Replace(" ", "");
                    //return int.Parse(InStr).ToString("N2"); // string.Format("{0:N2}", InStr); //  %格式：{0:P2}
                    case "3": // True、fasle 
                        return InStr.ToUpper() == "TRUE" ? "是" : "否";
                    case "4": // 1、0
                        return InStr == "1" ? "是" : "否";
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
        /// 对齐方式控制
        /// </summary>
        /// <param name="sType">对齐类型</param>
        /// <returns></returns>
        private string FormatAlign(string sType)
        {
            switch (sType)
            {
                case "0": // 左
                    return "align=\"left\"";
                case "1": // 中
                    return "align=\"center\"";
                case "2": // 右
                    return "align=\"right\"";
                case "9": // 敏感信息
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
                case "0": // 左
                    return "align=\"left\"";
                case "1": // 中
                    return "align=\"center\"";
                case "2": // 右
                    return "align=\"right\"";
                case "9": // 敏感信息 孩次>1 
                    if (FuncNo == "060303" || FuncNo == "060304" || FuncNo == "060305" || FuncNo == "060603" || FuncNo == "061001")
                    {
                        if (sValue == "此人已死亡") { return "bgcolor=\"#FFCCCC\""; } else { return "align=\"left\""; }
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
        /// 数据列表中的超级链接显示控制
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
                    //母子健康手册详细信息查看
                    return "<a href=\"#\" onclick=\"JavaScript:ChangeUrl('" + rowLink + "&mp=1&fwzh=" + inStr + "','" + HttpUtility.UrlEncode(this.FuncName) + "','view');\">" + inStr + "</a>";
                }
                else
                {
                    return "<a href=\"#\" onclick=\"JavaScript:ChangeUrl('" + rowLink + "&RID=" + objID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','view');\">" + inStr + "</a>";
                }
            }

        }

        #endregion

        #region 搜索设置
        /// <summary>
        /// 通用搜索信息设置
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
            sb.Append("<td class=\"searchUIName small\" align=\"left\" nowrap=\"nowrap\"><span class=\"moduleName\">搜索：请选择相应的条件，键入关键字开始搜索 ……</span></td><td class=\"small\" nowrap=\"nowrap\"></td>");
            sb.Append("<td class=\"small\" onmouseover=\"this.style.cursor='pointer';\" onclick=\"moveMe('advSearch');showSearchDiv('advSearch')\" valign=\"top\" align=\"right\">[关闭]</td>");
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

                sb.Append("<td width=\"100\" align=\"right\">" + aTitles[i] + "：</td>");
                sb.Append("<td width=\"70\" align=\"left\">");
                // 字符格式 0 文本,1 日期,2 数字
                if (aFormat[i].Trim() == "0")
                {
                    sb.Append(GetFilterStr("sel" + aFields[i], true));
                }
                else if (aFormat[i].Trim() == "1") { sb.Append("选择范围"); }
                else
                {
                    sb.Append(GetFilterStr("sel" + aFields[i], false));
                }
                sb.Append("</td>");
                if (aFormat[i].Trim() == "1")
                {
                    // onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:false})"
                    // onclick=\"JTC.setday({format:'yyyy-MM-dd', readOnly:false})\"
                    sb.Append("<td width=\"220\" align=\"left\"><input name=\"txt" + aFields[i] + "Start\" class=\"detailedViewTextBox\" type=\"text\" title=\"选择开始日期\" readonly=\"readonly\" size=\"8\" onclick=\"JTC.setday({format:'yyyy-MM-dd', readOnly:false})\" >=>");
                    sb.Append("<input name=\"txt" + aFields[i] + "End\" class=\"detailedViewTextBox\" type=\"text\" title=\"选择截止日期\" readonly=\"readonly\" size=\"8\" onclick=\"JTC.setday({format:'yyyy-MM-dd', readOnly:false})\"></td><td></td>");
                }
                else
                {
                    sb.Append("<td width=\"220\" align=\"left\"><input name=\"txt" + aFields[i] + "\" class=\"detailedViewTextBox\" type=\"text\" title=\"关键字\"></td><td></td>");
                }
            }
            sb.Append("<td>&nbsp;</td></tr>");
            sb.Append("</tbody></table></td></tr></tbody></table></div></td></tr></tbody></table>");

            sb.Append("<table class=\"searchUIAdv3 small\" width=\"80%\" align=\"center\" border=\"0\" cellpadding=\"5\" cellspacing=\"0\"><tbody><tr>");
            sb.Append("<td width=\"60%\" align=\"left\"><input class=\"submit6\" value=\" 立即搜索 \" type=\"submit\"/></td>");// onclick=\"totalnoofrows();callSearch('Advanced');\"
            sb.Append("<td class=\"small\" align=\"left\"></td></tr></tbody></table></div>");

            return sb.ToString();
        }
        /// <summary>
        /// 简单搜索
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
            sb.Append("<td width=\"60\" >数据单位：</td>");
            sb.Append("<td width=\"120\" align=\"left\"><input name=\"txtAreaName\" class=\"detailedViewTextBox\" type=\"text\" title=\"关键字\">；</td><td width=\"20\">&nbsp;</td>");
            sb.Append("<td width=\"65\" align=\"right\">数据日期：</td>");
            sb.Append("<td width=\"200\" align=\"left\">");
            sb.Append("<input name=\"txtReportDateStart\" class=\"detailedViewTextBox\" type=\"text\" title=\"选择开始日期\" readonly=\"readonly\" size=\"8\" onclick=\"PopCalendar(txtReportDateStart);return false;\"> 到： ");
            sb.Append("<input name=\"txtReportDateEnd\" class=\"detailedViewTextBox\" type=\"text\" title=\"选择截止日期\" readonly=\"readonly\" size=\"8\" onclick=\"PopCalendar(txtReportDateEnd);return false;\">；</td><td width=\"20\">&nbsp;</td>");
            int j = 0;
            for (int i = 0; i < aQuery.Length; i++)
            {
                if (aQuery[i].Trim() == "1")
                {
                    sb.Append("<td width=\"100\" align=\"right\">" + aTitles[i] + "：</td>");
                    if (aFormat[i].Trim() == "1")
                    {
                        sb.Append("<td width=\"200\" align=\"left\"><input name=\"txt" + aFields[i] + "Start\" class=\"detailedViewTextBox\" type=\"text\" title=\"选择开始日期\" readonly=\"readonly\" size=\"8\" onclick=\"PopCalendar(txt" + aFields[i] + "Start);return false;\"> 到： ");
                        sb.Append("<input name=\"txt" + aFields[i] + "End\" class=\"detailedViewTextBox\" type=\"text\" title=\"选择截止日期\" readonly=\"readonly\" size=\"8\" onclick=\"JTC.setday({format:'yyyy-MM-dd', readOnly:false})\">；</td><td width=\"20\">&nbsp;</td>");
                    }
                    else
                    {
                        sb.Append("<td width=\"120\" align=\"left\"><input name=\"txt" + aFields[i] + "\" class=\"detailedViewTextBox\" type=\"text\" title=\"关键字\">；</td><td width=\"20\">&nbsp;</td>");
                    }
                    j++;
                }
            }
            sb.Append("<td width=\"20\">&nbsp;</td>");
            sb.Append("<td width=\"100\" align=\"left\"><input value=\" 立即搜索 \" type=\"submit\"/></td>");
            sb.Append("<td>&nbsp;</td></tr>");
            sb.Append("</tbody></table>");

            return sb.ToString();
        }

        private string GetFilterStr(string htmlCtrlName, bool isCharacter)
        {
            string sHtml = string.Empty;
            sHtml += "<select id=\"" + htmlCtrlName + "\" name=\"" + htmlCtrlName + "\" class=\"detailedViewTextBox\">";
            sHtml += "<option value=\"like\" selected>模糊</option>";
            sHtml += "<option value=\"=\">精确</option>";
            sHtml += "</select>";

            return sHtml;
        }
        // 
        #endregion

        #endregion
    }
}
