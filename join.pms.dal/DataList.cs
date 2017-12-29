using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Web;

using UNV.Comm.DataBase;

namespace join.pms.dal
{
    public class DataList
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
                //if (funcNo.Substring(0, 2) == "02" || funcNo.Substring(0, 2) == "05" || funcNo.Substring(0, 2) == "06")
                //{
                //    this.m_Analys = dr[0][14].ToString();
                //    this.m_Query = dr[0][15].ToString();
                //}
                this.m_Analys = dr[0][14].ToString();
                this.m_Query = dr[0][15].ToString();

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
            string[] a_Query = this.m_Query.Split(',');

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
                    if (this.FuncNo == "XXXX") { }
                    else
                    {
                        if (this.FuncNo.Substring(0, 2) == "99")
                        {
                            a_Analys = this.m_Analys.Split(',');
                            int c = a_Fields.Length - 13; // 数据日期,比对日期,状态
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
                        else if (this.FuncNo.Substring(0, 2) == "88")
                        {
                            int c = a_Fields.Length - 12; // 数据日期,状态
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
                    // 操作按钮
                    sb.Append("<div class=\"list_button\"><div class=\"list_button_bg\">");
                    sb.Append(OprateButtonInfo(a_FuncInfo, a_FuncPowers));
                    sb.Append("</div></div>");
                    // 列头
                    sb.Append("<!--list start-->");
                    sb.Append(GetPageSearch(a_Titles, a_Fields, a_Format, a_Query));// 页面搜索
                    // 2013/01/16 by ysl 增加拖动表头和排序的功能 class=\"admintable\" class=\"adminth\"
                    // 2013/06/24 变更为只显示前10列的数据
                    // ===================  表头  =======================================================
                    sb.Append("<table width=\"" + tableWidth + "\" cellpadding=\"0\" cellspacing=\"0\"  id=\"DargTableByYsl\" border=\"0\"><thead><tr>");
                    if (pageCheckBox == "IsCheck") { sb.Append("<th width=\"50\" >&nbsp;<input name=\"itemsi\" id=\"0\" onclick=\"SelectAll();\" type=\"checkbox\" /></th>"); }
                    else { sb.Append("<th width=\"50\">序号</th>"); }
                    // class=\"adminth order\" order样式为排序用 width=\"" + a_Width[k] + "\" 
                    if (this.FuncNo == "XXXX")
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

                    if (a_FuncInfo[11] == "IsEdit" || a_FuncInfo[12] == "IsDel") sb.Append("<th width=\"80\">操作</th>");
                    sb.Append("</tr></thead>");
                    // ==================== 数据输出<tbody>  ================================================================
                    for (int i = 0; i < rowsNum; i++)
                    {
                        rowsOrderNo = (this.PageNo - 1) * this.PageSize + i + 1;
                        objID = dr[i]["" + this.PrimaryKey + ""].ToString();
                        //if (this.FuncNo == "0502" || this.FuncNo == "0503") 
                        if (this.FuncNo == "0503")
                        {
                            m_PicLink = dr[i]["DocsPath"].ToString();
                        }

                        if (pageCheckBox == "IsCheck") objCheckBoxParams = "SetCheckBoxClick('" + objID + "');";// 限制cbx只支持单选 objCheckBoxParams = "SetCheckBoxClear(" + objID + ");";
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
                                if (a_Titles[j].IndexOf("支付状态") > -1 || a_Titles[j].IndexOf("资金文件") > -1)
                                {
                                    sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + ">未支付</td>");
                                }
                                else
                                {
                                    sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + ">" + objFieldsValue + "</td>");
                                }
                                //if (a_Titles[j].IndexOf("资金文件") > -1)
                                //{
                                //    sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + ">点击查看</td>");
                                //}
                                //else
                                //{
                                //    sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + ">" + objFieldsValue + "</td>");
                                //}
                            }
                            else
                            {
                                if (a_Titles[j].IndexOf("支付状态") > -1)
                                {
                                    DateTime dtr_Date = DateTime.Now;
                                    try
                                    {
                                        dtr_Date = Convert.ToDateTime(objFieldsValue);
                                        if (dtr_Date > DateTime.Now)
                                        {
                                            objFieldsValue = "未支付";
                                        }
                                        else
                                        {
                                            objFieldsValue = "已支付";
                                        }
                                    }
                                    catch
                                    {
                                        objFieldsValue = "未支付";
                                    }
                                    sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + ">" + objFieldsValue + "</td>");
                                }
                                else if (a_Titles[j].IndexOf("资金文件") > -1)
                                {
                                    sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + ">" + FormatLink("点击查看", objID, a_Link[j], objStatus) + "</td>");
                                }
                                else if (a_Titles[j].IndexOf("状态操作") > -1)
                                {
                                    if (objFieldsValue == "0")
                                    {
                                        objFieldsValue = "--";
                                    }
                                    else if (objFieldsValue == "1")
                                    {
                                        objFieldsValue = "催办";
                                    }
                                    else if (objFieldsValue == "2")
                                    {
                                        objFieldsValue = "反馈";
                                    }
                                    sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + ">" + objFieldsValue + "</td>");
                                }
                                else
                                {
                                    if (objFieldsValue.Length > rowsLen - 5)
                                    {
                                        if (a_Titles[j].IndexOf("地址") > -1 || a_Titles[j].IndexOf("住址") > -1 || a_Titles[j].IndexOf("单位") > -1)
                                        {
                                            if (objFieldsValue.Length > 17)
                                            {
                                                sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, 12), objID, a_Link[j], objStatus) + "…</td>");
                                            }
                                            else
                                            {
                                                sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, objFieldsValue.Length - 5), objID, a_Link[j], objStatus) + "…</td>");
                                            }

                                        }
                                        else
                                        {
                                            if (objFieldsValue.Length > 9)
                                            {
                                                sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, 9), objID, a_Link[j], objStatus) + "…</td>");
                                            }
                                            else if (objFieldsValue.Length > 6 && objFieldsValue.Length < 10)
                                            {
                                                sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, 5), objID, a_Link[j], objStatus) + "…</td>");
                                            }
                                            else if (objFieldsValue.Length > 3 && objFieldsValue.Length < 7)
                                            {
                                                sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, 4), objID, a_Link[j], objStatus) + "…</td>");
                                            }
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
                                            if (objFieldsValue.Length > 15)
                                            {
                                                sb.Append("<td " + SetAlignAndColor(a_Align[j], objFieldsValue) + " title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue.Substring(0, 15), objID, a_Link[j], objStatus) + "…</td>");
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
            string[] a_Query = this.m_Query.Split(',');

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
                    string docType = "", docPath = "", docorPath = "", docName = "", docDate = "", personID = "", personNa = string.Empty;
                    this.FuncTreeName = CommPage.GetAllTreeName(this.FuncNo, true);
                    pageNav = GetPageNavStr(dr.Length);
                    // 导航信息 当前位置： <div class="mbx">当前位置：</div>
                    //sb.Append("<div class=\"mbx\">");
                    //sb.Append("当前位置：" + FuncTreeName + "(" + a_FuncInfo[2] + ")");
                    //sb.Append("</div>");
                    // 操作按钮
                    //sb.Append("<div class=\"list_button\"><div class=\"list_button_bg\">");
                    //sb.Append(OprateButtonInfo(a_FuncInfo, a_FuncPowers));
                    //sb.Append("</div></div>");
                    sb.Append(GetPageSearch(a_Titles, a_Fields, a_Format, a_Query));// 页面搜索

                    sb.Append("<div class=\"photo_list\">");
                    sb.Append("<ul id=\"jq22\" class=\"clearfix\">");


                    for (int i = 0; i < rowsNum; i++)
                    {
                        //rowsOrderNo = (this.PageNo - 1) * this.PageSize + i + 1;
                        objID = dr[i]["" + this.PrimaryKey + ""].ToString();
                        docType = dr[i]["DocsType"].ToString();
                        docPath = dr[i]["DocsPath"].ToString();
                        docorPath = dr[i]["DocsPath"].ToString().Replace("micro", "microBig");
                        docName = dr[i]["DocsName"].ToString();
                        docDate = dr[i]["OprateDate"].ToString();
                        personNa = dr[i]["PersonName"].ToString();
                        personID = dr[i]["PersonID"].ToString();
                        if (!string.IsNullOrEmpty(docDate)) docDate = DateTime.Parse(docDate).ToString("yyyy/MM/dd");
                        if (!string.IsNullOrEmpty(docPath)) docPath = docPath.Insert(docPath.LastIndexOf("/") + 1, "micro-");
                        // DocsType,DocsPath,DocsName,OprateDate
                        // PersonName,PersonTel,DocsType,DocsName,SourceName,OprateDate
                        // /Files/2015/06/201506021907214640.jpg
                        // Files/2015/06micro-/20150603161933991.gif

                        sb.Append("<li>");
                        sb.Append("<p class=\"pic\"><a href=\"javascript:;\"><span class=\"pic_center\"><i></i><img data-original='" + m_PicSvrUrl + docorPath + "' src='" + m_PicSvrUrl + docPath + "' alt='" + docName + "' /></span></a></p>");
                        sb.Append("<p class=\"title\">" + docName + "</p>");
                        sb.Append("<p class=\"name\"><span>" + docDate + "</span><b><a href=\"PhotoView.aspx?id=" + personID + "\">" + personNa + "</a></b></p>");
                        //sb.Append("<p class=\"pic\"><a href=\"#\" onclick=\"JavaScript:ChangeUrl('PhotoView.aspx?action=view&RID=" + objID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','view');\"><span class=\"pic_center\"><i></i><img src=\"" + m_PicSvrUrl + docPath + "\" alt=\"" + docName + "\" /></span></a></p>");
                        //sb.Append("<p class=\"title\"><a href=\"#\" onclick=\"JavaScript:ChangeUrl('PhotoView.aspx?action=view&RID=" + objID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','view');\">" + docName + "</a></p>");
                        //sb.Append("<p class=\"name\"><span>" + docDate + "</span><b>" + personNa + "</b></p>");
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
                sb.Append("<ul>");
                sb.Append("<li>");
                sb.Append("<b>功能操作：</b>");
                sb.Append("<span>");
                // 新增按钮
                //if (a_FuncPowers[0] == "1" && funcInfo[10] == "IsAdd") { sb.Append("<input class=\"button_public button_blue\" value=\"" + funcInfo[4] + "\" onclick=\"JavaScript:ChangeUrl('" + funcInfo[5] + "&BizID=" + BizID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','add');\" type=\"button\"/>"); }
                if (funcInfo[10] == "IsAdd") { sb.Append("<input class=\"button_public button_blue\" value=\"" + funcInfo[4] + "\" onclick=\"JavaScript:ChangeUrl('" + funcInfo[5] + "&BizID=" + BizID + "','" + HttpUtility.UrlEncode(this.FuncName) + "','add');\" type=\"button\"/>"); }
                // 查询按钮
                sb.Append("<input class=\"button_public button_green\" value=\"查找\" onclick=\"moveMe('advSearch');showSearchDiv('advSearch');\" type=\"button\"/>");
                string[] arybutCss = new string[] { "button_red", "button_orange", "button_blue", "", "button_blue", "button_red", "button_orange", "", "button_orange" };
                for (int i = 0; i < a_ButName.Length; i++)
                {
                    // 2009/07/11 增加设置权限集显示按钮 1,1,1,0,0,0,0,0,0
                    if (a_ButVisible[i].Trim() == "1" && a_FuncPowers[i + 3] == "1")
                    {
                        if (i == 0) { sb.Append("<input class=\"button_public " + arybutCss[i] + "\" value=\"  " + a_ButName[i] + "  \" onclick=\"JavaScript:ChangeUrlMultCheck('" + a_ButLink[i] + "','" + HttpUtility.UrlEncode(this.FuncName) + "');\" type=\"button\"/>"); }
                        else { sb.Append("<input class=\"button_public " + arybutCss[i] + "\" value=\"  " + a_ButName[i] + "  \" onclick=\"JavaScript:ChangeUrlMultCheck('" + a_ButLink[i] + "','" + HttpUtility.UrlEncode(this.FuncName) + "');\" type=\"button\"/>"); }
                    }
                }
                sb.Append("</span>");
                sb.Append("</li>");
                sb.Append("</ul>");
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
                    if (FuncNo == "060303" || FuncNo == "060304" || FuncNo == "060305" || FuncNo == "0903" || FuncNo == "061001")
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
        /// 数据列表中的超级链接显示控制 IMG
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
            //else if (rowLink.Trim() == "IMG") {
            //    return "<a href=\"" + m_PicSvrUrl + m_PicLink + "\" targer=\"_blank\">" + inStr + "</a>";
            //}
            else if (rowLink.Trim() == "IMGLIST")
            {
                return "<a href=\"/PhotoList.aspx?RID=" + objID + "\" targer=\"_blank\">" + inStr + "</a>";
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
        private string GetPageSearch(string[] aTitles, string[] aFields, string[] aFormat, string[] aQuery)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id=\"advSearch\" style=\"display: none;\"><input name=\"searchAction\" value=\"advSearch\" type=\"hidden\"/>");
            //搜索标题
            sb.Append("<div class=\"searchUIAdv1 small\">");
            sb.Append("<p class=\"close\" onclick=\"moveMe('advSearch');showSearchDiv('advSearch')\">[关闭]</p>");
            sb.Append("<p class=\"title\">搜索：请选择相应的条件，键入关键字开始搜索 ……</p>");
            sb.Append("</div>");

            //搜索项
            sb.Append("<div class=\"searchUIAdv2 small\" id=\"fixed\">");
            sb.Append("<table id=\"adSrc\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            int x = 0;
            for (int i = 0; i < aFields.Length; i++)
            {
                if (aQuery[i] == "0")
                {
                    if (x % 2 == 0)
                    {
                        if (i > 0) sb.Append("<td>&nbsp;</td></tr>");
                        sb.Append("<tr>");
                    }

                    sb.Append("<th class=\"title\">" + aTitles[i] + "：</th>");
                    sb.Append("<td class=\"select\">");
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
                        // size=\"29\" onclick=\"PopCalendar(txtReportDate);return false;\" 
                        sb.Append("<td class=\"text_time\"><input name=\"txt" + aFields[i] + "Start\" class=\"detailedViewTextBox\" type=\"text\" title=\"选择开始日期\" readonly=\"readonly\" size=\"8\" onclick=\"JTC.setday({format:'yyyy-MM-dd', readOnly:false})\"> &gt; ");
                        sb.Append("<input name=\"txt" + aFields[i] + "End\" class=\"detailedViewTextBox\" type=\"text\" title=\"选择截止日期\" readonly=\"readonly\" size=\"8\" onclick=\"JTC.setday({format:'yyyy-MM-dd', readOnly:false})\"></td><td></td>");
                    }
                    else
                    {
                        sb.Append("<td class=\"text\"><input name=\"txt" + aFields[i] + "\" class=\"detailedViewTextBox\" type=\"text\" title=\"关键字\"></td><td></td>");
                    }
                    x++;
                }
            }
            sb.Append("<td>&nbsp;</td></tr>");
            sb.Append("</table>");
            sb.Append("</div>");

            //搜索按钮
            sb.Append("<div class=\"searchUIAdv3 small\">");
            sb.Append("<input class=\"submit6\" value=\"立即搜索\" type=\"submit\"/>"); // onclick=\"totalnoofrows();callSearch('Advanced');\"
            sb.Append("</div>");

            sb.Append("</div>");
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
                        sb.Append("<td width=\"200\" align=\"left\"><input name=\"txt" + aFields[i] + "Start\" class=\"detailedViewTextBox\" type=\"text\" title=\"选择开始日期\" readonly=\"readonly\" size=\"8\" onclick=\"JTC.setday({format:'yyyy-MM-dd', readOnly:false})\"> 到： ");
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
