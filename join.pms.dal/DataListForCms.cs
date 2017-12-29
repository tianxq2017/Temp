using System;
using System.Collections.Generic;
using System.Text;

using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.dal
{
    public class DataListForCms
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
        // 页面路径
        private string _PagePath;
        public string PagePath
        {
            set { _PagePath = value; }
            get { return _PagePath; }
        }
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
        // 功能编号
        private string _FuncNo;
        public string FuncNo
        {
            set { _FuncNo = value; }
            get { return _FuncNo; }
        }
        // 分类ID
        private string _CmsCID;
        public string CmsCID
        {
            set { _CmsCID = value; }
            get { return _CmsCID; }
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
        // 数据行单员格超级链接地址
        private string m_RowLink;

        // ===========================================
        private DataSet m_Ds;
        private string m_TitleFormat;
        private string m_SiteUrl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"];
        private string m_SiteUrlWeb = System.Configuration.ConfigurationManager.AppSettings["SiteUrlWeb"];

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
            if (string.IsNullOrEmpty(this.FuncName)) this.FuncName = funcInfo[2];

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
                parameters[5].Value = this.OrderType;
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
            pagestr += PageNo > 1 ? "<a href=\"" + this.Url + "&p=1&k=" + HttpUtility.UrlEncode(this.SearchKeys) + "\" class=\"page\">首页</a>&nbsp;&nbsp;<a href=\"" + this.Url + "&k=" + HttpUtility.UrlEncode(SearchKeys) + "&p=" + pre + "\" class=\"page\">上一页</a>" : "首页 上一页";
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += PageNo == i ? "&nbsp;&nbsp;<font color=\"#ff0000\">" + i + "</font>" : "&nbsp;&nbsp;<a href=\"" + this.Url + "&k=" + HttpUtility.UrlEncode(SearchKeys) + "&p=" + i + "\" class=\"page\">" + i + "</a>";
            }
            pagestr += PageNo != allpage ? "&nbsp;&nbsp;<a href=\"" + this.Url + "&p=" + next + "&k=" + HttpUtility.UrlEncode(SearchKeys) + "\" class=\"page\">下一页</a>&nbsp;&nbsp;<a href=\"" + this.Url + "&k=" + HttpUtility.UrlEncode(SearchKeys) + "&p=" + allpage + "\" class=\"page\">末页</a>" : " 下一页 末页";
            pagestr += "&nbsp;&nbsp;记录数：本页[ " + pageRec + " ]/总[ " + this.TotalRec + " ]";

            return pagestr;
        }
        /// <summary>
        /// 伪静态分页
        /// </summary>
        /// <param name="pageRec"></param>
        /// <param name="staticPage"></param>
        /// <returns></returns>
        private string GetPageNavStr_Old(int pageRec, bool staticPage)
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

            pagestr = "页码：第[" + PageNo + "]页/共[" + allpage + "]页&nbsp;";
            pagestr += PageNo > 1 ? "<a href=\"" + this.Url + "1." + this.FileExt + "\" class=\"page\">首页</a>&nbsp;<a href=\"" + this.Url + pre + "." + this.FileExt + "\" class=\"page\">上一页</a>" : "首页 上一页";
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += PageNo == i ? "&nbsp;<font color=\"#ff0000\">" + i + "</font>" : "&nbsp;<a href=\"" + this.Url + i + "." + this.FileExt + "\" class=\"page\">" + i + "</a>";
            }
            pagestr += PageNo != allpage ? "&nbsp;<a href=\"" + this.Url + next + "." + this.FileExt + "\" class=\"page\">下一页</a>&nbsp;<a href=\"" + this.Url + allpage + "." + this.FileExt + "\" class=\"page\">末页</a>" : " 下一页 末页";
            pagestr += "&nbsp;记录数：本页[ " + pageRec + " ]/总[ " + this.TotalRec + " ]";

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
            pagestr += "</span><span class=\"a2\">本页[ " + pageRec + " ]/总[ " + this.TotalRec + " ]</span>";

            return pagestr;
        }
        /// <summary>
        /// 伪静态分页Wap
        /// </summary>
        /// <param name="pageRec"></param>
        /// <param name="staticPage"></param>
        /// <returns></returns>
        private string GetPageNavStrWap(int pageRec, bool staticPage)
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
            startcount = (PageNo + 2) > allpage ? allpage - 3 : PageNo - 1;

            endcount = PageNo < 2 ? 4 : PageNo + 2;
            if (startcount < 1) { startcount = 1; }
            if (allpage < endcount) { endcount = allpage; }

            string selectstr = string.Empty;
            for (int k = 1; k <= allpage; k++)
            {
                if (k == PageNo) { selectstr += "<option value=\"" + this.Url + k + "." + this.FileExt + "\" selected>" + k + "</option>"; }
                else { selectstr += "<option value=\"" + this.Url + k + "." + this.FileExt + "\">" + k + "</option>"; }

            }



            pagestr += "<span class=\"button\">";
            pagestr += PageNo > 1 ? "<a href=\"" + this.Url + "1." + this.FileExt + "\">&lt;&lt;</a> <a href=\"" + this.Url + pre + "." + this.FileExt + "\">&lt;</a>" : " <b>&lt;&lt;</b> <b> &lt;</b> ";
            //pagestr += " <span>" + PageNo + "</span>";
            //pagestr += " <input value=\"" + PageNo + "\" maxlength=\"4\" size=\"2\" name=\"page\" tyep=\"TEXT\"><input type=\"submit\" value=\"GO\" onclick=\"location.href='" + this.Url + pre + "." + this.FileExt + "'\">";
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += PageNo == i ? " <span>" + i + "</span>" : " <a href=\"" + this.Url + i + "." + this.FileExt + "\">" + i + "</a>";
            }
            //if (allpage > 4)
            //{
            //    pagestr += "...<a href=\"" + this.Url + allpage + "." + this.FileExt + "\">" + allpage + "</a>";
            //}
            pagestr += PageNo != allpage ? " <a href=\"" + this.Url + next + "." + this.FileExt + "\" >&gt;</a> <a href=\"" + this.Url + allpage + "." + this.FileExt + "\">&gt;&gt;</a>" : " <b>&gt;</b> <b>&gt;&gt;</b>";
            pagestr += "</span>";
            //pagestr += "<span class=\"input\">转到第<input name=\"txtPageNo\" type=\"text\" id=\"txtPageNo\" style=\"width:30px\" size=\"3\" maxlength=\"3\" onkeyup=\"window.location.href='" + this.Url + "'+this.value+'" + "." + this.FileExt + "'\"/>页</span>";
            pagestr += "<span class=\"select\">转到 <select onchange=\"javascript:window.location=this.value\">" + selectstr + "</select></span>";//转到第页
            pagestr += "<span class=\"number\">本页[ " + pageRec + " ]/总[ " + this.TotalRec + " ]</span>";

            return pagestr;
        }
        /// <summary>
        /// 英文分页样式
        /// </summary>
        /// <param name="pageRec"></param>
        /// <param name="staticPage"></param>
        /// <returns></returns>
        private string GetPageNavStrYS(int pageRec, bool staticPage)
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
            if (allpage < endcount) { endcount = allpage; }            //

            pagestr += PageNo > 1 ? "<a href=\"" + this.Url + "1." + this.FileExt + "\"><img src=\"/images/page_01.gif\" /></a><a href=\"" + this.Url + pre + "." + this.FileExt + "\"><img src=\"/images/page_02.gif\" /></a>" : "<img src=\"/images/page_01.gif\" /> <img src=\"/images/page_02.gif\" />";
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += PageNo == i ? " <span>" + i + "</span>" : "<a href=\"" + this.Url + i + "." + this.FileExt + "\" ><b>" + i + "</b></a>";
            }
            pagestr += PageNo != allpage ? " <a href=\"" + this.Url + next + "." + this.FileExt + "\"><img src=\"/images/page_03.gif\" /></a>&nbsp;<a href=\"" + this.Url + allpage + "." + this.FileExt + "\"><img src=\"/images/page_04.gif\" /></a>" : " <img src=\"/images/page_03.gif\" />&nbsp;<img src=\"/images/page_04.gif\" />";

            return pagestr;
        }
        /// <summary>
        /// 伪静态分页样式
        /// </summary>
        /// <param name="pageRec"></param>
        /// <param name="staticPage"></param>
        /// <returns></returns>
        private string GetPageNavStrS(int pageRec, bool staticPage)
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

            //pagestr = "页码：第[" + PageNo + "]页/共[" + allpage + "]页&nbsp;&nbsp;";
            pagestr += "<table width=\"720\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>";
            pagestr += "<tr>";
            //pagestr = "页码：第[" + PageNo + "]页/共[" + allpage + "]页&nbsp;&nbsp;";
            pagestr += "<td height=\"37\" align=\"right\">";
            pagestr += PageNo > 1 ? "<a href=\"" + this.Url + "1." + this.FileExt + "\" class=\"page\">首页</a><a href=\"" + this.Url + pre + "." + this.FileExt + "\" class=\"page\">上一页</a>" : "首页 上一页";
            pagestr += "</td><td  align=\"left\">";
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += PageNo == i ? " <span class=\"page02\">" + i + "</span>" : "<a href=\"" + this.Url + i + "." + this.FileExt + "\" ><span class=\"page01\">" + i + "</span></a>";
            }
            pagestr += "<span style=\" display:-moz-inline-box; display:inline-block;height:21px;line-height:21px; \">";
            pagestr += PageNo != allpage ? " <a href=\"" + this.Url + next + "." + this.FileExt + "\" class=\"page\">下一页</a>&nbsp;<a href=\"" + this.Url + allpage + "." + this.FileExt + "\" class=\"page\">末页</a>" : " 下一页&nbsp;末页";
            pagestr += "&nbsp;&nbsp;记录数：本页[ " + pageRec + " ]/总[ " + this.TotalRec + " ]";
            pagestr += "</span></td>";
            pagestr += "</tr>";
            pagestr += "</table>";
            return pagestr;
        }

        /// <summary>
        /// 获取内容信息列表-顶部推荐（1条）
        /// </summary>
        /// <returns></returns>
        public string GetCmsListHot()
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

            string cmsID, cmsTitle, cmsBody, cmsCName, cmsCode, createDate, targetUrl, navUrl = string.Empty;

            // 获取预览图相关
            string picStyle = string.Empty;
            string docsType, strLevelImg = string.Empty;
            string sourceImg = string.Empty;
            string targetImg = string.Empty;
            string strStyle = string.Empty;
            int IWidth = 0;
            int IHeight = 0;

            // 获取总记录数
            this.TotalRec = GetTotalRowsNum(a_FuncInfo);

            // 获取本页数据
            sb.Append("<div class=\"news_hot\">");
            sb.Append("<ul>");
            if (GetPageDataSet(ref errorMsg))
            {
                try
                {
                    DataRow[] dr = m_Ds.Tables[0].Select();
                    int rowsNum = dr.Length;
                    int colsNum = a_Fields.Length;

                    if (rowsNum > 0)
                    {
                        for (int i = 0; i < rowsNum; i++)
                        {
                            cmsID = dr[i]["" + this.PrimaryKey + ""].ToString();
                            cmsTitle = PageValidate.Decode(dr[i]["CmsTitle"].ToString());
                            cmsCode = FormatString(dr[i]["CmsCode"].ToString(), "0");
                            cmsCName = FormatString(dr[i]["CmsCName"].ToString(), "0");
                            createDate = FormatString(dr[i]["OprateDate"].ToString(), "4");
                            targetUrl = dr[i]["TargetUrl"].ToString();

                            cmsBody = PageValidate.Decode(dr[i]["CmsBody"].ToString());
                            //cmsBody = PageValidate.FilterHtml(cmsBody); //只保留文本，去掉其他标签、图片等
                            if (cmsBody.Length > 300)
                            { cmsBody = cmsBody.Substring(0, 300) + "…"; }

                            this.m_TitleFormat = cmsTitle;
                            if (!String.IsNullOrEmpty(targetUrl))
                            { navUrl = "href=\"" + targetUrl + "\" target=\"_blank\""; }
                            else
                            {
                                navUrl = "href=\"" + m_SiteUrl + "/Info/" + cmsID + "." + this._FileExt + "\"";
                            }

                            // -----MicroImage Start ------->
                            sourceImg = dr[i]["CmsPic"].ToString();
                            if (!String.IsNullOrEmpty(sourceImg))
                            {
                                docsType = System.IO.Path.GetExtension(sourceImg).ToLower();//文件类型（扩展名）
                                if (docsType == ".jpg" || docsType == ".bmp" || docsType == ".png" || docsType == ".gif")
                                {
                                    picStyle = "";
                                    sourceImg = this._PagePath + sourceImg;
                                    targetImg = sourceImg.Insert(sourceImg.LastIndexOf("."), "micrNewsList");
                                    targetImg = SetMicroImages.GetMicroImage(sourceImg, targetImg, "H", 350, 150);

                                    if (!String.IsNullOrEmpty(targetImg)) { targetImg = targetImg.Replace(_PagePath, ""); }
                                    else
                                    {
                                        picStyle = "pic_none";
                                        targetImg = "/images/zanwu_01.gif";
                                    }
                                }
                                else
                                {
                                    picStyle = "pic_none";
                                    targetImg = "/images/zanwu_01.gif";
                                }
                            }
                            else
                            {
                                picStyle = "pic_none";
                                targetImg = "/images/zanwu_01.gif";
                            }
                            // -----MicroImage End ------    

                            sb.Append("<li class=\"clearfix " + picStyle + "\">");
                            sb.Append("<div class=\"pic\"><a " + navUrl + "><img src=\"" + targetImg + "\" /></a></div>");
                            sb.Append("<div class=\"title_bg\">");
                            sb.Append("<div class=\"title\"><a " + navUrl + ">" + this.m_TitleFormat + "</a></div>");
                            sb.Append("<div class=\"time\">" + createDate + "</div>");
                            sb.Append("<div class=\"sum\">" + cmsBody + "</div>");
                            sb.Append("</div>");
                            sb.Append("</li>");
                        }
                        //分页
                        pageNav = GetPageNavStr(dr.Length, true);
                    }
                    else
                    {
                        sb.Append("<li>暂无信息</li>");
                    }
                }
                catch (Exception ex) { sb.Append("<li>获取内容数据错误...</li>"); }
                if (m_Ds != null) m_Ds.Clear();
                m_Ds = null;
                //=====================================
            }
            else { sb.Append("<li>获取内容数据错误...</li>"); }
            sb.Append("</ul>");
            sb.Append("</div>");
            sb.Append("<div class=\"page\">" + pageNav + "</div>");
            return sb.ToString();
        }

        /// <summary>
        /// 获取内容信息列表（15条）
        /// </summary>
        /// <returns></returns>
        public string GetCmsList(string funcNo)
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
            string Album, AlbumON = string.Empty;
            string CmsCName = string.Empty;
            string CmsFlag, navUrlclassify = string.Empty;
            string cmsID, cmsTitle, cmsCName, cmsCode, createDate, targetUrl, navUrl = string.Empty;
            // 获取总记录数
            this.TotalRec = GetTotalRowsNum(a_FuncInfo);
            // 获取本页数据  
            sb.Append("<div class=\"news_list\">");
            sb.Append("<ul>");
            sb.Append("<li class=\"news_title\"><span class=\"time\">发布日期</span><a href=\"javascript:;\">标题</a></li>");
            if (GetPageDataSet(ref errorMsg))
            {
                try
                {
                    DataRow[] dr = m_Ds.Tables[0].Select();
                    int rowsNum = dr.Length;
                    int colsNum = a_Fields.Length;

                    if (rowsNum > 0)
                    {
                        for (int i = 0; i < rowsNum; i++)
                        {
                            cmsID = dr[i]["" + this.PrimaryKey + ""].ToString();
                            cmsTitle = PageValidate.Decode(dr[i]["CmsTitle"].ToString());
                            cmsCode = FormatString(dr[i]["CmsCode"].ToString(), "0");
                            cmsCName = FormatString(dr[i]["CmsCName"].ToString(), "0");
                            createDate = FormatString(dr[i]["OprateDate"].ToString(), "4");
                            
                            CmsCName = DbHelperSQL.GetSingle("SELECT CmsCName FROM CMS_Class WHERE CmsCode=" + cmsCode + "").ToString(); //获取分类名
                            CmsFlag = DbHelperSQL.GetSingle("SELECT CmsFlag FROM CMS_Class WHERE CmsCode=" + cmsCode + "").ToString(); //获取栏目类型

                            

                            this.m_TitleFormat = cmsTitle;
                            navUrl = "href=\"" + m_SiteUrl + "/Info/" + cmsID + "." + this._FileExt + "\"";

                            //CmsFlag 0.-;1.单页;2.列表;3.图片;4.链接;5.弹出式相册
                            if (CmsFlag == "2") { navUrlclassify = this.m_SiteUrl + "/INFO/" + cmsCode + "-syskey-1.shtml"; }
                            else if (CmsFlag == "3") { navUrlclassify = this.m_SiteUrl + "/Photos/" + cmsCode + "-syskey-1.shtml"; }
                            else if (CmsFlag == "5") { navUrlclassify = this.m_SiteUrl + "/Photos/" + cmsCode + "-syskey-1.shtml"; }

                            //if (funcNo == "0000")//搜索列表显示分类名
                            //{
                            //    sb.Append("<li><span class=\"classify\"><a href=\"" + navUrlclassify + "\">[" + CmsCName + "]</a></span><span class=\"time\">" + createDate + "</span><a " + navUrl + ">" + this.m_TitleFormat + "</a></li>");
                            //}
                            //else
                            //{
                            //    sb.Append("<li><span class=\"time\">" + createDate + "</span><a " + navUrl + ">" + this.m_TitleFormat + "</a></li>");
                            //}

                            //if (i % 5 == 4)
                            //{
                            //    sb.Append("<li class=\"x\"></li>");
                            //}

                            if (i % 2 == 0)
                            {
                                sb.Append("<li><span class=\"time\">" + createDate + "</span><a " + navUrl + ">" + this.m_TitleFormat + "</a></li>");
                            }
                            else {
                                sb.Append("<li class=\"even\"><span class=\"time\">" + createDate + "</span><a " + navUrl + ">" + this.m_TitleFormat + "</a></li>");
                            }
                        }
                        //分页
                        pageNav = GetPageNavStr(dr.Length, true);
                    }
                    else
                    {
                        sb.Append("<li>暂无信息</li>");
                    }
                }
                catch (Exception ex) { sb.Append("<li>获取内容数据错误...</a></li>"); }
                if (m_Ds != null) m_Ds.Clear();
                m_Ds = null;
                //=====================================
            }
            else { sb.Append("<li>·获取内容数据错误...</a></li>"); }
            sb.Append(" </ul>");
            sb.Append("</div>");
            sb.Append("<div class=\"page\">" + pageNav + "</div>");
            return sb.ToString();
        }

        /// <summary>
        /// 获取信息列表-wap
        /// </summary>
        /// <returns></returns>
        public string GetCmsListWap()
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

            string cmsID, cmsTitle, cmsCode, cmsBody, createDate, targetUrl, navUrl = string.Empty;
            string picStyle = string.Empty;

            string docsType, strLevelImg = string.Empty;
            string sourceImg = string.Empty;
            string targetImg = string.Empty;
            string strStyle = string.Empty;
            int IWidth = 0;
            int IHeight = 0;
            // 获取总记录数
            this.TotalRec = GetTotalRowsNum(a_FuncInfo);
            // 获取本页数据
            sb.Append("<div class=\"product_01\">");
            sb.Append("<ul>");
            if (GetPageDataSet(ref errorMsg))
            {
                try
                {
                    DataRow[] dr = m_Ds.Tables[0].Select();
                    int rowsNum = dr.Length;
                    int colsNum = a_Fields.Length;
                    if (rowsNum > 0)
                    {
                        for (int i = 0; i < rowsNum; i++)
                        {
                            cmsID = dr[i]["" + this.PrimaryKey + ""].ToString();
                            cmsTitle = PageValidate.Decode(dr[i]["CmsTitle"].ToString());
                            cmsCode = FormatString(dr[i]["CmsCode"].ToString(), a_Format[0]);
                            createDate = FormatString(dr[i]["OprateDate"].ToString(), a_Format[4]);
                            targetUrl = dr[i]["TargetUrl"].ToString();
                            cmsBody = PageValidate.Decode(dr[i]["CmsBody"].ToString());
                            //cmsBody = PageValidate.FilterHtml(cmsBody);
                            if (cmsBody.Length > 300)
                            { cmsBody = cmsBody.Substring(0, 300) + "…"; }

                            if (!String.IsNullOrEmpty(targetUrl))
                            { navUrl = "href=\"" + targetUrl + "\" target=\"_blank\""; }
                            else
                            {
                                navUrl = "href=\"" + m_SiteUrl + "/info/" + cmsID + "." + this._FileExt + "\"";
                            }
                            //=========================================
                            // -----MicroImage Start ------->
                            sourceImg = dr[i]["CmsPic"].ToString();
                            if (!String.IsNullOrEmpty(sourceImg))
                            {
                                docsType = System.IO.Path.GetExtension(sourceImg).ToLower();//文件类型（扩展名）
                                if (docsType == ".jpg" || docsType == ".bmp" || docsType == ".png" || docsType == ".gif")
                                {
                                    picStyle = "";
                                    sourceImg = this.m_SiteUrlWeb + sourceImg;
                                    targetImg = sourceImg.Insert(sourceImg.LastIndexOf("."), "micrNewsList");
                                    //targetImg = SetMicroImages.GetMicroImage(sourceImg, targetImg, "H", 120, 90);
                                    if (!String.IsNullOrEmpty(targetImg)) { targetImg = targetImg.Replace(_PagePath, ""); }
                                    else
                                    {
                                        picStyle = "pic_none";
                                        targetImg = "/images/zanwu_01.gif";
                                    }
                                }
                                else
                                {
                                    picStyle = "pic_none";
                                    targetImg = "/images/zanwu_01.gif";
                                }
                            }
                            else
                            {
                                picStyle = "pic_none";
                                targetImg = "/images/zanwu_01.gif";
                            }
                            // -----MicroImage End ------    
                            //=========================================  

                            sb.Append("<li class=\"clearfix " + picStyle + "\">");
                            sb.Append("<a " + navUrl + " title=\"" + cmsTitle + "\">");
                            sb.Append("<p class=\"pic\"><img src=\"" + targetImg + "\" alt=\"" + cmsTitle + "\" width=\"84\" height=\"36\" /></p>");
                            sb.Append("<p class=\"title\">");
                            sb.Append("<b>" + cmsTitle + "</b>");
                            sb.Append(" <span class=\"content\">" + cmsBody + "</span>");
                            sb.Append("</p>");
                            sb.Append("</a>");
                            sb.Append("</li>");
                            sb.Append("<!--结束 -->");
                        }
                        //分页
                        pageNav = GetPageNavStrWap(dr.Length, true);
                    }
                    else
                    {
                        sb.Append("<!--循环开始 -->");
                        sb.Append("<li class=\"clearfix pic_none\">");
                        sb.Append("<p class=\"pic\"></p>");
                        sb.Append("<p class=\"title\">暂无数据</p>");
                        sb.Append("</li>");
                        sb.Append("<!--结束 -->");
                    }
                }
                catch (Exception ex) { sb.Append("<li>获取内容数据错误...</a></li>"); }
                if (m_Ds != null) m_Ds.Clear();
                m_Ds = null;
            }
            else { sb.Append("<li>获取内容数据错误...</a></li>"); }
            sb.Append("</ul>");
            sb.Append("</div>");
            sb.Append("<div class=\"page\">" + pageNav + "</div>");
            return sb.ToString();
        }

        /// <summary>
        /// 获取产品信息列表
        /// </summary>
        /// <returns></returns>
        public string GetCmsListPic(string m_CmsCode)
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
            string Album, AlbumON = string.Empty;

            string cmsID, cmsTitle, cmsCode, cmsBody, cmsPic, createDate, targetUrl, navUrl, style_h = string.Empty;
            string picStyle = string.Empty;

            string docsType, strLevelImg = string.Empty;
            string sourceImg = string.Empty;
            string targetImg = string.Empty;
            string strStyle = string.Empty;

            string cmsFlag = DbHelperSQL.GetSingle("SELECT CmsFlag FROM CMS_Class WHERE CmsCode=" + m_CmsCode + "").ToString();

            int IWidth = 0;
            int IHeight = 0;
            // 获取总记录数
            this.TotalRec = GetTotalRowsNum(a_FuncInfo);
            // 获取本页数据
            if (cmsFlag == "5")
            {
                // 弹出式相册
                sb.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"/scripts/pic_album/jquery.css\" media=\"screen\">");
                sb.Append("<script type=\"text/javascript\" src=\"/scripts/pic_album/jquery.js\"></script>");
                sb.Append("<script type=\"text/javascript\" src=\"/scripts/pic_album/jquery_002.js\"></script>");
                sb.Append("<div id=\"gallery\" class=\"pic_list\">");
            }
            else
            {
                sb.Append("<div class=\"pic_list\">");
            }
            sb.Append("<ul>");
            if (GetPageDataSet(ref errorMsg))
            {
                try
                {
                    DataRow[] dr = m_Ds.Tables[0].Select();
                    int rowsNum = dr.Length;
                    int colsNum = a_Fields.Length;
                    if (rowsNum > 0)
                    {
                        for (int i = 0; i < rowsNum; i++)
                        {
                            cmsID = dr[i]["" + this.PrimaryKey + ""].ToString();
                            cmsTitle = PageValidate.Decode(dr[i]["CmsTitle"].ToString());
                            cmsCode = FormatString(dr[i]["CmsCode"].ToString(), a_Format[0]);
                            createDate = FormatString(dr[i]["OprateDate"].ToString(), a_Format[4]);
                            targetUrl = dr[i]["TargetUrl"].ToString();
                            cmsPic = dr[i]["CmsPic"].ToString();
                            cmsBody = PageValidate.Decode(dr[i]["CmsBody"].ToString());
                            //cmsBody = PageValidate.FilterHtml(cmsBody);

                            Album = dr[i]["Album"].ToString();
                            if (Album == "1") { AlbumON = "【相册】"; }
                            else { AlbumON = ""; }

                            if (cmsCode == "0104")
                            {
                                //sb.Append("<style type=\"text/css\">");
                                //sb.Append(".pic_list li img {height: 300px;}");
                                //sb.Append("</style>");
                                style_h = "style=\"height: 300px;\"";
                            }

                            if (cmsBody.Length > 300)
                            { cmsBody = cmsBody.Substring(0, 300) + "…"; }

                            if (!String.IsNullOrEmpty(targetUrl))
                            { navUrl = "href=\"" + targetUrl + "\" target=\"_blank\""; }
                            else
                            {
                                navUrl = "href=\"" + m_SiteUrl + "/info/" + cmsID + "." + this._FileExt + "\"";
                            }
                            //=========================================
                            // -----MicroImage Start ------->
                            sourceImg = dr[i]["CmsPic"].ToString();
                            if (!String.IsNullOrEmpty(sourceImg))
                            {
                                docsType = System.IO.Path.GetExtension(sourceImg).ToLower();//文件类型（扩展名）
                                if (docsType == ".jpg" || docsType == ".bmp" || docsType == ".png" || docsType == ".gif")
                                {
                                    picStyle = "";
                                    sourceImg = this._PagePath + sourceImg;
                                    targetImg = sourceImg.Insert(sourceImg.LastIndexOf("."), "micrNewsList");
                                    targetImg = SetMicroImages.GetMicroImage(sourceImg, targetImg, "H", 204, 153); //如果缩略图在文件夹被删除，则刷新会自动生成

                                    if (!String.IsNullOrEmpty(targetImg)) { targetImg = targetImg.Replace(_PagePath, ""); }
                                    else
                                    {
                                        picStyle = "pic_none";
                                        targetImg = "/images/zanwu_01.gif";
                                    }
                                }
                                else
                                {
                                    picStyle = "pic_none";
                                    targetImg = "/images/zanwu_01.gif";
                                }
                            }
                            else
                            {
                                picStyle = "pic_none";
                                targetImg = "/images/zanwu_01.gif";
                            }
                            // -----MicroImage End ------    
                            //=========================================
                            if (cmsFlag == "5")
                            {
                                sb.Append("<li>");
                                sb.Append("<p class=\"pic\"><a href=\"" + cmsPic + "\" title=\"" + cmsTitle + "\"><img " + style_h + " src=\"" + targetImg + "\" alt=\"" + cmsTitle + "\" /></a></p>");
                                sb.Append("<p class=\"title\">");
                                sb.Append("" + cmsTitle + "");
                                sb.Append("</p>");
                                sb.Append("</li>");
                            }
                            else
                            {
                                sb.Append("<li>");
                                sb.Append("<p class=\"pic\"><a " + navUrl + " title=\"" + cmsTitle + "\"><img " + style_h + " src=\"" + targetImg + "\" alt=\"" + cmsTitle + "\" /></a></p>");
                                sb.Append("<p class=\"title\">");
                                sb.Append("<a " + navUrl + " title=\"" + cmsTitle + "\">" + cmsTitle + "</a>");
                                sb.Append("</p>");
                                sb.Append("</li>");
                            }
                        }
                        //分页
                        pageNav = GetPageNavStr(dr.Length, true);
                    }
                    else
                    {
                        sb.Append("<li>");
                        sb.Append("<p class=\"pic\"></p>");
                        sb.Append("<p class=\"title\">暂无数据</p>");
                        sb.Append("</li>");
                    }
                }
                catch (Exception ex) { sb.Append("<li>获取内容数据错误...</a></li>"); }
                if (m_Ds != null) m_Ds.Clear();
                m_Ds = null;
            }
            else { sb.Append("<li>获取内容数据错误...</a></li>"); }
            sb.Append("</ul><div class=\"clr\"></div>");
            sb.Append("</div>");
            sb.Append("<div class=\"page\">" + pageNav + "</div>");
            return sb.ToString();
        }

        /// <summary>
        /// 获取产品信息列表Wap
        /// </summary>
        /// <returns></returns>
        public string GetCmsListPicWap()
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

            string cmsID, cmsTitle, cmsCode, cmsBody, createDate, targetUrl, navUrl = string.Empty;
            string picStyle = string.Empty;

            string docsType, strLevelImg = string.Empty;
            string sourceImg = string.Empty;
            string targetImg = string.Empty;
            string strStyle = string.Empty;
            int IWidth = 0;
            int IHeight = 0;
            // 获取总记录数
            this.TotalRec = GetTotalRowsNum(a_FuncInfo);
            // 获取本页数据
            sb.Append("<div class=\"pic_list\">");
            sb.Append("<ul>");
            if (GetPageDataSet(ref errorMsg))
            {
                try
                {
                    DataRow[] dr = m_Ds.Tables[0].Select();
                    int rowsNum = dr.Length;
                    int colsNum = a_Fields.Length;
                    if (rowsNum > 0)
                    {
                        for (int i = 0; i < rowsNum; i++)
                        {
                            cmsID = dr[i]["" + this.PrimaryKey + ""].ToString();
                            cmsTitle = PageValidate.Decode(dr[i]["CmsTitle"].ToString());
                            cmsCode = FormatString(dr[i]["CmsCode"].ToString(), a_Format[0]);
                            createDate = FormatString(dr[i]["OprateDate"].ToString(), a_Format[4]);
                            targetUrl = dr[i]["TargetUrl"].ToString();
                            cmsBody = PageValidate.Decode(dr[i]["CmsBody"].ToString());
                            //cmsBody = PageValidate.FilterHtml(cmsBody);
                            if (cmsBody.Length > 300)
                            { cmsBody = cmsBody.Substring(0, 300) + "…"; }

                            if (!String.IsNullOrEmpty(targetUrl))
                            { navUrl = "href=\"" + targetUrl + "\" target=\"_blank\""; }
                            else
                            {
                                navUrl = "href=\"" + m_SiteUrl + "/info/" + cmsID + "." + this._FileExt + "\"";
                            }
                            //=========================================
                            // -----MicroImage Start ------->
                            sourceImg = dr[i]["CmsPic"].ToString();
                            if (!String.IsNullOrEmpty(sourceImg))
                            {
                                docsType = System.IO.Path.GetExtension(sourceImg).ToLower();//文件类型（扩展名）
                                if (docsType == ".jpg" || docsType == ".bmp" || docsType == ".png" || docsType == ".gif")
                                {
                                    picStyle = "";
                                    sourceImg = this.m_SiteUrlWeb + sourceImg;
                                    targetImg = sourceImg.Insert(sourceImg.LastIndexOf("."), "micrNewsList");
                                    //targetImg = SetMicroImages.GetMicroImage(sourceImg, targetImg, "H", 120, 90);
                                    if (!String.IsNullOrEmpty(targetImg)) { targetImg = targetImg.Replace(_PagePath, ""); }
                                    else
                                    {
                                        picStyle = "pic_none";
                                        targetImg = "/images/zanwu_01.gif";
                                    }
                                }
                                else
                                {
                                    picStyle = "pic_none";
                                    targetImg = "/images/zanwu_01.gif";
                                }
                            }
                            else
                            {
                                picStyle = "pic_none";
                                targetImg = "/images/zanwu_01.gif";
                            }
                            // -----MicroImage End ------    
                            //=========================================  


                            sb.Append("<li>");
                            sb.Append("<div class=\"bg\">");
                            sb.Append("<div class=\"pic\"><span class=\"pic_center\"><i></i><a " + navUrl + " title=\"" + cmsTitle + "\"><img src=\"" + targetImg + "\" alt=\"" + cmsTitle + "\" /></a></span></div>");
                            sb.Append("<div class=\"title_bg\">");
                            sb.Append("<p class=\"title\"><a " + navUrl + " title=\"" + cmsTitle + "\"><span>MORE</span>" + cmsTitle + "</a></p>");
                            sb.Append("</div>");
                            sb.Append("</div>");
                            sb.Append("</li>");
                        }
                        //分页
                        pageNav = GetPageNavStrWap(dr.Length, true);
                    }
                    else
                    {
                        sb.Append("<li>暂无数据</li>");
                    }
                }
                catch (Exception ex) { sb.Append("<li>获取内容数据错误...</li>"); }
                if (m_Ds != null) m_Ds.Clear();
                m_Ds = null;
            }
            else { sb.Append("<li>获取内容数据错误...</li>"); }
            sb.Append("</ul><div class=\"clr\"></div>");
            sb.Append("</div>");
            sb.Append("<div class=\"page\">" + pageNav + "</div>");
            return sb.ToString();
        }
        /// <summary>
        /// 获取留言信息列表
        /// </summary>
        /// <returns></returns>
        public string GetNotesList()
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

            string msgID, msgTitle, msgBody, oprateDate, isReplay, msgReBody = string.Empty;
            // 获取总记录数
            this.TotalRec = GetTotalRowsNum(a_FuncInfo);
            // 获取本页数据
            sb.Append("<table width=\"92%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"margin:0 auto;\">");
            if (GetPageDataSet(ref errorMsg))
            {
                try
                {
                    DataRow[] dr = m_Ds.Tables[0].Select();
                    int rowsNum = dr.Length;
                    int colsNum = a_Fields.Length;
                    if (rowsNum > 0)
                    {
                        for (int i = 0; i < rowsNum; i++)
                        {
                            msgID = dr[i]["" + this.PrimaryKey + ""].ToString();
                            msgTitle = FormatString(dr[i]["MsgTitle"].ToString(), a_Format[0]);
                            msgBody = FormatString(dr[i]["MsgBody"].ToString(), a_Format[0]);
                            msgBody = PageValidate.Decode(msgBody);
                            oprateDate = FormatString(dr[i]["OprateDate"].ToString(), a_Format[0]);
                            isReplay = FormatString(dr[i]["IsReplay"].ToString(), a_Format[0]);
                            if (isReplay == "1")
                            {
                                msgReBody = DbHelperSQL.GetSingle("SELECT ReplyBody FROM SYS_NotesRe WHERE MsgID=" + msgID + "").ToString();
                                msgReBody = PageValidate.Decode(msgReBody);
                            }
                            else { msgReBody = "正在回复，请耐心等待…"; }
                            //<!--循环开始 -->
                            sb.Append("<tr>");
                            sb.Append("<td valign=\"top\" align=\"center\" class=\"message\">");
                            sb.Append("<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\">");
                            sb.Append("<tr>");
                            sb.Append("<th width=\"12\" height=\"32\"></th>");
                            sb.Append("<th valign=\"middle\" align=\"left\" class=\"message_s\"><b>主题：" + msgTitle + "</b></th>");
                            sb.Append("<th valign=\"middle\" align=\"right\">" + oprateDate + "</th>");
                            sb.Append("<th width=\"10\"></th>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td></td>");
                            sb.Append("<td valign=\"top\" align=\"left\" style=\"padding:10px 0 10px 0;\" colspan=\"2\" class=\"hanggao22\"><span>留言内容：</span>" + msgBody + "</td>");
                            sb.Append("<td></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td></td>");
                            sb.Append("<td valign=\"top\" align=\"left\" style=\"padding:10px 0;\" class=\"message_x hanggao22\" colspan=\"2\"><span class=\"message_s\">回复：</span>" + msgReBody + " </td>");
                            sb.Append("<td></td>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td height=\"20\"></td>");
                            sb.Append("</tr>");
                            //<!--结束 -->  
                            //分页
                            pageNav = GetPageNavStr(dr.Length, true);
                        }
                    }
                    else
                    {
                        sb.Append("<tr>");
                        sb.Append("<td valign=\"top\" align=\"center\" class=\"message\" height=\"28\">暂无信息</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td height=\"20\"></td>");
                        sb.Append("</tr>");
                    }
                }
                catch (Exception ex)
                {
                    sb.Append("<tr>");
                    sb.Append("<td valign=\"top\" align=\"center\" class=\"message\" height=\"28\">获取数据信息出错…</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td height=\"20\"></td>");
                    sb.Append("</tr>");
                }
                if (m_Ds != null) m_Ds.Clear();
                m_Ds = null;
                //=====================================
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<td valign=\"top\" align=\"center\" class=\"message\" height=\"28\">获取数据信息出错…</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td height=\"20\"></td>");
                sb.Append("</tr>");
            }

            sb.Append("</table>");
            sb.Append("<div class=\"page\">" + pageNav + "</div>");
            return sb.ToString();
        }

        /// <summary>
        /// 获取留言_a信息列表
        /// </summary>
        /// <returns></returns>
        public string GetNotesList_a()
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

            string msgID, msgTitle, msgBody, oprateDate, isReplay, msgReBody = string.Empty;
            string txt01, txt02, txt03, txt04, txt05, txt06, txt07 = string.Empty;
            // 获取总记录数
            this.TotalRec = GetTotalRowsNum(a_FuncInfo);
            // 获取本页数据
            sb.Append("<div class=\"part_14\"><ul>");
            if (GetPageDataSet(ref errorMsg))
            {
                try
                {
                    DataRow[] dr = m_Ds.Tables[0].Select();
                    int rowsNum = dr.Length;
                    int colsNum = a_Fields.Length;
                    if (rowsNum > 0)
                    {
                        for (int i = 0; i < rowsNum; i++)
                        {
                            msgID = dr[i]["" + this.PrimaryKey + ""].ToString();
                            msgTitle = FormatString(dr[i]["MsgTitle"].ToString(), a_Format[0]);
                            msgBody = FormatString(dr[i]["MsgBody"].ToString(), a_Format[0]);
                            msgBody = PageValidate.Decode(msgBody);
                            oprateDate = FormatString(dr[i]["OprateDate"].ToString(), a_Format[0]);
                            isReplay = FormatString(dr[i]["IsReplay"].ToString(), a_Format[0]);
                            txt01 = FormatString(dr[i]["txt01"].ToString(), a_Format[0]);
                            txt02 = FormatString(dr[i]["txt02"].ToString(), a_Format[0]);
                            txt03 = FormatString(dr[i]["txt03"].ToString(), a_Format[0]);
                            txt04 = FormatString(dr[i]["txt04"].ToString(), a_Format[0]);
                            txt05 = FormatString(dr[i]["txt05"].ToString(), a_Format[0]);
                            txt06 = FormatString(dr[i]["txt06"].ToString(), a_Format[0]);
                            txt07 = FormatString(dr[i]["txt07"].ToString(), a_Format[0]);
                            if (isReplay == "1")
                            {
                                msgReBody = DbHelperSQL.GetSingle("SELECT ReplyBody FROM SYS_NotesRe_a WHERE MsgID=" + msgID + "").ToString();
                                msgReBody = PageValidate.Decode(msgReBody);
                            }
                            else { msgReBody = "正在回复，请耐心等待…"; }
                            //<!--循环开始 -->  " + msgReBody + "
                            sb.Append("<li class=\"list\">");
                            sb.Append("<p class=\"part_button\"><a href=\"" + "/JobApp-" + msgID + ".shtml\">我要申请</a></p>");
                            sb.Append("<p class=\"part_title\"><a href=\"" + "/JobApp-" + msgID + ".shtml\">" + msgTitle + "</a></p>");
                            sb.Append("<div class=\"part_sum\">");
                            sb.Append("<ul>");
                            sb.Append("<li class=\"field\"><b>职位月薪：</b><span>" + txt01 + "</span><b>工作地址：</b><span>" + txt02 + "</span></li>");
                            sb.Append("<li class=\"field\"><b>发布日期：</b><span>" + oprateDate + "</span><b>工作性质：</b><span>" + txt03 + "</span></li>");
                            sb.Append("<li class=\"field\"><b>工作经验：</b><span>" + txt04 + "</span><b>最低学历：</b><span>" + txt05 + "</span></li>");
                            sb.Append("<li class=\"field\"><b>招聘人数：</b><span>" + txt06 + "</span><b>公司规模：</b><span>" + txt07 + "</span></li>");
                            sb.Append("<li class=\"intro\">");
                            sb.Append("<p class=\"title\">任职要求：</p>");
                            sb.Append("<div class=\"sum\">" + msgBody + "</div>");
                            sb.Append("</li></ul></div></li>");
                            //<!--结束 -->  
                            //分页
                            pageNav = GetPageNavStr(dr.Length, true);
                        }
                    }
                    else
                    {
                        sb.Append("<li>暂无信息</li>");
                    }
                }
                catch (Exception ex)
                {
                    sb.Append("<li>获取数据信息出错…</li>");
                }
                if (m_Ds != null) m_Ds.Clear();
                m_Ds = null;
                //=====================================
            }
            else
            {
                sb.Append("<li>获取数据信息出错…</li>");
            }

            sb.Append("</ul></div>");
            sb.Append("<div class=\"page\">" + pageNav + "</div>");
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
                    case "4": // 日期
                        return DateTime.Parse(InStr).ToString("yyyy-MM-dd"); //string.Format("{0:yyyy-MM-dd}",InStr); 
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
        /// 
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public string Convert(string source)
        {
            string result;

            //remove line breaks,tabs
            result = source.Replace("\r", " ");
            result = result.Replace("\n", " ");
            result = result.Replace("\t", " ");

            //remove the header
            result = Regex.Replace(result, "(<head>).*(</head>)", string.Empty, RegexOptions.IgnoreCase);

            result = Regex.Replace(result, @"<( )*script([^>])*>", "<script>", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"(<script>).*(</script>)", string.Empty, RegexOptions.IgnoreCase);

            //remove all styles
            result = Regex.Replace(result, @"<( )*style([^>])*>", "<style>", RegexOptions.IgnoreCase); //clearing attributes
            result = Regex.Replace(result, "(<style>).*(</style>)", string.Empty, RegexOptions.IgnoreCase);

            //insert tabs in spaces of <td> tags
            result = Regex.Replace(result, @"<( )*td([^>])*>", " ", RegexOptions.IgnoreCase);

            //insert line breaks in places of <br> and <li> tags
            result = Regex.Replace(result, @"<( )*br( )*>", "\r", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"<( )*li( )*>", "\r", RegexOptions.IgnoreCase);

            //insert line paragraphs in places of <tr> and <p> tags
            result = Regex.Replace(result, @"<( )*tr([^>])*>", "\r\r", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"<( )*p([^>])*>", "\r\r", RegexOptions.IgnoreCase);

            //remove anything thats enclosed inside < >
            result = Regex.Replace(result, @"<[^>]*>", string.Empty, RegexOptions.IgnoreCase);

            //replace special characters:
            result = Regex.Replace(result, @"&amp;", "&", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&nbsp;", " ", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&lt;", "<", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&gt;", ">", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&(.{2,6});", string.Empty, RegexOptions.IgnoreCase);

            //remove extra line breaks and tabs
            result = Regex.Replace(result, @" ( )+", " ");
            result = Regex.Replace(result, "(\r)( )+(\r)", "");
            result = Regex.Replace(result, @"(\r\r)+", "");

            return result;
        }
        #endregion

        #endregion
    }
}


