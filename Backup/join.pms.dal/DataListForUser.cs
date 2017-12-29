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

        #region  PageData 成员属性
        // 排序
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
        // 页地址参数 TabPage
        private string _Url;
        public string Url
        {
            set { _Url = value; }
            get { return _Url; }
        }
        // 搜索条件
        private string _SearchWhere;
        public string SearchWhere
        {
            set { _SearchWhere = value; }
            get { return _SearchWhere; }
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
        // 扩展名
        private string _FileExt;
        public string FileExt
        {
            set { _FileExt = value; }
            get { return _FileExt; }
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
        // 操作名称
        private string m_OperName;
        // 操作链接
        private string m_OperLink;
        // 操作显示
        private string m_OperVisible;
        // 数据行单员格超级链接地址
        private string m_RowLink;

        // ===========================================
        private DataSet m_Ds;

        private string m_SvrUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];
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
                this.m_OperName = dr[0][8].ToString();
                this.m_OperLink = dr[0][9].ToString();
                this.m_OperVisible = dr[0][10].ToString();

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
                //排序
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

            pagestr = "页码：第[" + PageNo + "]页/共[" + allpage + "]页&nbsp;&nbsp;";
            pagestr += PageNo > 1 ? "<a href=\"" + this.Url + "&p=1\">首页</a>&nbsp;&nbsp;<a href=\"" + this.Url + "&p=" + pre + "\">上一页</a>" : "首页 上一页";
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += PageNo == i ? "&nbsp;&nbsp;<font color=\"#ff0000\">" + i + "</font>" : "&nbsp;&nbsp;<a href=\"" + this.Url + "&p=" + i + "\">" + i + "</a>";
            }
            pagestr += PageNo != allpage ? "&nbsp;&nbsp;<a href=\"" + this.Url + "&p=" + next + "\">下一页</a>&nbsp;&nbsp;<a href=\"" + this.Url + "&p=" + allpage + "\">末页</a>" : " 下一页 末页";
            pagestr += "&nbsp;&nbsp;记录数：本页[ " + pageRec + " ]/总[ " + this.TotalRec + " ]";

            return pagestr;
        }
        /// <summary>
        /// 伪静态分页
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
            //pagestr += "</span><span class=\"a2\">转到第<input name=\"txtPageNo\" type=\"text\" id=\"txtPageNo\" style=\"width:30px\" size=\"3\" maxlength=\"3\" onkeyup=\"window.location.href='" + this.Url + "'+this.value+'" + "." + this.FileExt + "'\"/>页</span>";
            pagestr += "</span><span class=\"a2\">转到第<select onchange=\"javascript:window.location=this.value\">" + selectstr + "</select>页</span>";
            /*<select onchange="javascript:window.location=this.value">
<option selected="" value="/in2.asp?Page=1&amp;CID=400">1</option></select>*/
            pagestr += "</div>";
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
            string[] a_OperName = this.m_OperName.Split(',');
            string[] a_OperLink = this.m_OperLink.Split(',');
            string[] a_OperVisible = this.m_OperVisible.Split(',');

            string objID = string.Empty;
            string objFieldsValue = string.Empty;
            string objCheckBoxParams = string.Empty;
            string pageCheckBox = a_FuncInfo[3];
            string objStatus = string.Empty;
            string objChangeType = string.Empty;

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
                    int rowsOrderNo = 0; // 顺序号
                    int rowsLen = 0;
                    pageNav = GetPageNavStr(dr.Length, true);
                    //<!--数据列表 -->
                    sb.Append("<div class=\"user_list\">");
                    sb.Append("<div class=\"user_list_title2\">" + this.FuncName + "</div>");
                    sb.Append("<div class=\"user_list_c\">");
                    sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    sb.Append("<tr>");
                    if (pageCheckBox == "IsCheck") { sb.Append("<th width=\"30\"><input id=\"0\" onclick=\"SelectAll(this);\" type=\"checkbox\"  name=\"itemsi\"/></th>"); }
                    else { sb.Append("<th width=\"30\">序号</th>"); }
                    for (int k = 0; k < colsNum; k++)
                    {
                        sb.Append("<th>" + a_Titles[k] + "</th>");
                    }
                    if (a_OperVisible[0] == "1" || a_OperVisible[1] == "1" || a_OperVisible[2] == "1" || a_OperVisible[3] == "1" || a_OperVisible[4] == "1" || a_OperVisible[5] == "1") sb.Append("<th>操作</th>");
                    sb.Append("</tr>");

                    if (rowsNum > 0)
                    {
                        // ==================== 数据输出  =============================                  
                        for (int i = 0; i < rowsNum; i++)
                        {
                            rowsOrderNo = (this.PageNo - 1) * this.PageSize + i + 1;
                            objID = dr[i]["" + this.PrimaryKey + ""].ToString();
                            if (pageCheckBox == "IsCheck") objCheckBoxParams = "SetCheckBoxClick(" + objID + ");";// 限制cbx只支持单选 objCheckBoxParams = "SetCheckBoxClear(" + objID + ");";
                            sb.Append("<tr>");
                            // 复选框及顺序号
                            if (pageCheckBox == "IsCheck") { sb.Append("<td><input name=\"ItemChk\" type=checkbox id=" + objID + " value=" + objID + " onclick=\"" + objCheckBoxParams + "\"></td>"); }
                            else { sb.Append("<td>" + rowsOrderNo.ToString() + "</td>"); }

                            //数据开始
                            for (int j = 0; j < colsNum; j++)
                            {
                                objFieldsValue = FormatString(dr[i]["" + a_Fields[j] + ""].ToString(), a_Format[j]); // 格式化数据 

                                if (FuncNo == "0502" && j == 0)
                                {
                                    sb.Append("<td  align=\"" + FormatAlign(a_Align[j]) + "\" title=\"" + objFieldsValue + "\"><a href=\"" + m_SvrUrl + dr[i]["DocsPath"].ToString() + "\" rel=\"lightbox[zj]\" title=\"查看证件：" + objFieldsValue + "\">" + objFieldsValue + "</a></td>");
                                }
                                else if (FuncNo == "0503" && j == 0)
                                {
                                    sb.Append("<td  align=\"" + FormatAlign(a_Align[j]) + "\" title=\"" + objFieldsValue + "\"><a href=\"" + m_SvrUrl + dr[i]["DocsPath"].ToString() + "\" title=\"下载附件：" + objFieldsValue + "\">" + objFieldsValue + "</a></td>");
                                }
                                else
                                {
                                    sb.Append("<td  align=\"" + FormatAlign(a_Align[j]) + "\" title=\"" + objFieldsValue + "\">" + FormatLink(objFieldsValue, objID, a_Link[j], objStatus) + "</td>");
                                }
                            }

                            //OperName: 查看,编辑,删除,xx,xx,xx
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
                        //数据结束
                    }
                    else
                    {
                        //没有数据时默认显示一条空信息（以便使添加按钮显示）
                        sb.Append("<tr>");
                        sb.Append("<td>&nbsp;</td>");
                        for (int k = 0; k < colsNum; k++)
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                        if (a_OperVisible[0] == "1" && a_OperName[0] == "新增")
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
                    //<!--页码 -->
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

        #region  根据配置参数对显示数据进行控制
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
                        return DateTime.Parse(InStr).ToString("yyyy/MM/dd"); //string.Format("{0:yyyy-MM-dd}",InStr); 

                    case "2": // 数字
                        return int.Parse(InStr).ToString("N2"); // string.Format("{0:N2}", InStr); //  %格式：{0:P2}

                    case "8": // 身份证号
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
        /// 对齐方式控制
        /// </summary>
        /// <param name="sType">对齐类型</param>
        /// <returns></returns>
        private string FormatAlign(string sType)
        {
            switch (sType)
            {
                case "0": // 左
                    return "left";
                case "1": // 中
                    return "center";
                case "2": // 右
                    return "right";
                default:
                    return "left";
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

