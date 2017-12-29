using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Web;

using System.Data;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
namespace join.pms.dal
{
    public class Biz_Categories
    {
        #region 字段
        public string BizNameAB;
        public string BizNameFull;
        public string BizStep;
        public string BizStepNames;
        public string BizImg;
        public string BizUrl;
        public string BizKeys;
        public string BizTemplates;
        #endregion

        public string RegAreaCodeA;
        public string RegAreaCodeB;
        public string BizCNum;
        public string BizGNum;
        public bool IsInnerArea = false;

        public string BizDocsIDOld;
        public string BizDocsIDiOld;
        public string BizDocsIDIsOld;


        public string PersonID;

        private string m_SqlParams;
        private static string m_SvrUrl = ConfigurationManager.AppSettings["SvrUrl"];
        private static string m_FileExt = ConfigurationManager.AppSettings["FileExtension"];
        private static string m_SiteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
        /// <summary>
        /// 获取指定业务编码的业务信息
        /// </summary>
        /// <param name="bizCode"></param>
        /// <returns></returns>
        public void SelectSingle(string bizCode)
        {

            SqlDataReader sdr = null;
            try
            {
                sdr = DbHelperSQL.ExecuteReader("SELECT BizNameAB,BizNameFull,BizStep,BizStepNames,BizImg,BizUrl,BizKeys,BizTemplates FROM BIZ_Categories WHERE BizCode='" + bizCode + "'");
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        BizNameAB = sdr["BizNameAB"].ToString();
                        BizNameFull = sdr["BizNameFull"].ToString();
                        BizStep = sdr["BizStep"].ToString();
                        BizStepNames = sdr["BizStepNames"].ToString();
                        BizTemplates = PageValidate.Decode(sdr["BizTemplates"].ToString());
                        if (BizTemplates.Contains("src=\"/Files/"))
                        {
                            BizTemplates = BizTemplates.Replace("src=\"/Files/", "src=\"" + m_SvrUrl + "/Files/");
                        }
                        if (BizTemplates.Contains("href=\"/Files/"))
                        {
                            BizTemplates = BizTemplates.Replace("href=\"/Files/", "href=\"" + m_SvrUrl + "/Files/");
                        }
                        BizImg = sdr["BizImg"].ToString();
                        BizUrl = sdr["BizUrl"].ToString();
                        BizKeys = sdr["BizKeys"].ToString();
                    }
                }
                sdr.Close(); sdr.Dispose();
            }
            catch { if (sdr != null) { sdr.Close(); sdr.Dispose(); } }

        }
        /// <summary>
        /// 获取当前业务信息页头
        /// </summary>
        /// <param name="bizCode"></param>
        /// <param name="bizCode"></param>
        /// <returns></returns>
        public string GetCategories(string bizCode, string favUrl)
        {
            string str = string.Empty;
            str = "<div class=\"column_r\">";
            str += "<ul>";
            //str += "<li><a href=\"javascript:void(0);\" onclick=\"AddFavorites('" + BizNameFull + "','" + favUrl + "','" + bizCode + "','0');\">添加到收藏</a></li>";
            //str += "<li class=\"share\"><div class=\"bsync-custom icon-blue\"><a title=\"一键分享到各大微博和社交网络\" class=\"bshare-bsync\" onclick=\"javascript:bSync.share(event)\">一键分享</a><span class=\"BSHARE_COUNT bshare-share-count\">0</span></div></li>";
            //if (!string.IsNullOrEmpty(BizImg))
            //{
            //    str += "<li><a href=\"" + m_SvrUrl + BizImg + "\" rel=\"lightbox[zj]\" title=\"查看样本证件：" + BizNameFull + "\">查看样本证件</a></li>";
            //}
            str += "</ul>";
            str += "</div>";
            //str += "<p class=\"column_title\"><span><img src=\"/images/ico/biz" + bizCode + ".png\" /></span><b>" + BizNameFull + "</b></p>";
            //以下带当前位置
            str += "<p class=\"column_title\">";
            str += "<span class=\"ico\"><img src=\"/images/ico/biz" + bizCode + ".png\" /></span>";
            str += "<span class=\"title_bg\">";
            str += "<span class=\"title\">" + BizNameFull + "</span>";
            str += "<span class=\"crumb\"><a href=\"/\">首页</a><b>&gt;</b>业务申请</span>";
            str += "</span>";
            str += "</p>";
            return str;
        }
        /// <summary>
        /// 获取该项业务所需证件
        /// </summary>
        /// <param name="bizCode"></param>
        /// <param name="bizCNum"></param>
        /// <param name="isInnerArea"></param>
        /// <returns></returns>
        public string GetBizCategoryLicense(string bizCode, string bizID)
        {
            SqlDataReader sdr = null;
            StringBuilder sHtml = new StringBuilder();
            string LicenseID, LicenseName,LicenseType, LicenseTemplates = string.Empty;
            int i = 0; int k = 0;
            try
            {
                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=0";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">所<br />需<br />证<br />件</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- 插入各种附件 Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"0\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- 插入各种附件 End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        sHtml.Append("<input type=\"button\" value=\"从高拍仪扫描\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        // <input type="button" value="从高拍仪扫描" class="button" onclick="SelecDocsByGpy('txtDocsID3','txtSourceName3','%e5%a4%ab%e5%a6%87%e5%8f%8c%e6%96%b9%e7%bb%93%e5%a9%9a%e8%af%81')"/>
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"从证件库选择\" class=\"button\" />");
                        // sHtml.Append("<input type=\"checkbox\" id=\"cbBiz" + i + "\" name=\"cbBiz" + i + "\" class=\"cbx\" alt=\"拿原件到办证大厅提交\"/>证件样本查看");
                        //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">下载电子表格</a>"); }
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                        i++;
                        k++;
                    }
                    sHtml.Append(" </table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                }
                sdr.Close();





                //申请表上的证照
                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=5";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">证<br />照</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseType = sdr["LicenseType"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        string docsPath = GetBizDocsOld(LicenseName, i, bizID);

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- 插入各种附件 Start --> 
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- 插入各种附件 End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        sHtml.Append("<input type=\"button\" value=\"从高拍仪扫描\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                        if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                        i++;
                        k++;
                    }
                    sHtml.Append(" </table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                }
                sdr.Close(); sdr.Dispose();

                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=1";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">其<br />它<br />证<br />件</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();
                        LicenseType = sdr["LicenseType"].ToString(); 

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- 插入各种附件 Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- 插入各种附件 End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        sHtml.Append("<input type=\"button\" value=\"从高拍仪扫描\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"从证件库选择\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">下载电子表格</a>"); }
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                        i++;
                    }
                    sHtml.Append(" </table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                }
                sdr.Close(); sdr.Dispose();

                //获取当前申请表中男女户籍地编码
                GetBizContents(bizID);

                int h = 0;
                //if (bizCode == "0103" || bizCode == "0104" || bizCode == "0105" || bizCode == "0110")
                //{
                //    //单方
                //    if (!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)){IsInnerArea = true;}
                //}
                //else
                //{
                //    //双方
                //    if ((!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)) || (!string.IsNullOrEmpty(this.RegAreaCodeB) && IsThisAreaCode(this.RegAreaCodeB))){ IsInnerArea = true; }

                //}
                IsInnerArea = false;
                if (IsInnerArea)
                {
                    LicenseName = HttpUtility.UrlEncode("辖区外纸质证明");
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">辖区外纸质证明</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    sHtml.Append("<tr>");
                    sHtml.Append("<th width=\"260\">辖区外需提供纸质证明：</th>");
                    sHtml.Append("<td class=\"text\"><p><span>");
                    //<!-- 插入各种附件 Start -->
                    sHtml.Append("<input id=\"txtDocsIDIs\" name=\"txtDocsIDIs\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtDocsNameIs\" name=\"txtDocsNameIs\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtSourceNameIs\" name=\"txtSourceNameIs\" readonly=\"readonly\"  style=\"width:300px\"/>");
                    //<!-- 插入各种附件 End -->
                    sHtml.Append("</span></p>");
                    sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsIDIs','txtSourceNameIs','" + LicenseName + "')\"/>");
                    sHtml.Append("<input type=\"button\" value=\"从高拍仪扫描\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsIDIs','txtSourceNameIs','" + LicenseName + "')\"/>");
                    
                    //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"从证件库选择\" class=\"button\" />");
                    sHtml.Append("<input type=\"checkbox\" name=\"cbBizIs\" id=\"cbBizIs\"  class=\"cbx\" alt=\"拿原件到办证大厅提交\"/>证件样本查看");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                    sHtml.Append("</table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");

                }
            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); sHtml = new StringBuilder(); sHtml.Append("获取数据信息出错…"); }
            }
            BizCNum = k.ToString();
            BizGNum = i.ToString();
            return sHtml.ToString();
        }

        #region 附件上传重载，增加区分系统的标识 系统分类：1,管理平台；2,群众平台
        /// <summary>
        /// 获取业务附件，进行上传
        /// </summary>
        /// <param name="bizCode"></param>
        /// <param name="bizID"></param>
        /// <param name="sysCate">系统分类：1,管理平台；2,群众平台</param>
        /// <returns></returns>
        public string GetBizCategoryLicense(string bizCode, string bizID,string sysCate)
        {
            SqlDataReader sdr = null;
            StringBuilder sHtml = new StringBuilder();
            string LicenseID, LicenseName,LicenseType, LicenseTemplates = string.Empty;
            int i = 0; int k = 0;
            try
            {
                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=0";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">所<br />需<br />证<br />件</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- 插入各种附件 Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"0\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- 插入各种附件 End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"从高拍仪扫描\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        // <input type="button" value="从高拍仪扫描" class="button" onclick="SelecDocsByGpy('txtDocsID3','txtSourceName3','%e5%a4%ab%e5%a6%87%e5%8f%8c%e6%96%b9%e7%bb%93%e5%a9%9a%e8%af%81')"/>
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"从证件库选择\" class=\"button\" />");
                        // sHtml.Append("<input type=\"checkbox\" id=\"cbBiz" + i + "\" name=\"cbBiz" + i + "\" class=\"cbx\" alt=\"拿原件到办证大厅提交\"/>证件样本查看");
                        //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">下载电子表格</a>"); }
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                        i++;
                        k++;
                    }
                    sHtml.Append(" </table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                }
                sdr.Close();

                



                //申请表上的证照
                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=5";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">证<br />照</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseType = sdr["LicenseType"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        string docsPath = GetBizDocsOld(LicenseName, i, bizID);

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- 插入各种附件 Start --> 
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- 插入各种附件 End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"从高拍仪扫描\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                        if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                        i++;
                        k++;
                    }
                    sHtml.Append(" </table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                }
                sdr.Close(); sdr.Dispose();

                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=1";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">其<br />它<br />证<br />件</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- 插入各种附件 Start -->
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- 插入各种附件 End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"从高拍仪扫描\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"从证件库选择\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">下载电子表格</a>"); }
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                        i++;
                    }
                    sHtml.Append(" </table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                }
                sdr.Close(); sdr.Dispose();

                //获取当前申请表中男女户籍地编码
                GetBizContents(bizID);

                int h = 0;
                //if (bizCode == "0103" || bizCode == "0104" || bizCode == "0105" || bizCode == "0110")
                //{
                //    //单方
                //    if (!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)){IsInnerArea = true;}
                //}
                //else
                //{
                //    //双方
                //    if ((!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)) || (!string.IsNullOrEmpty(this.RegAreaCodeB) && IsThisAreaCode(this.RegAreaCodeB))){ IsInnerArea = true; }

                //}
                IsInnerArea = false;
                if (IsInnerArea)
                {
                    LicenseName = HttpUtility.UrlEncode("辖区外纸质证明");
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">辖区外纸质证明</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    sHtml.Append("<tr>");
                    sHtml.Append("<th width=\"260\">辖区外需提供纸质证明：</th>");
                    sHtml.Append("<td class=\"text\"><p><span>");
                    //<!-- 插入各种附件 Start -->
                    sHtml.Append("<input id=\"txtDocsIDIs\" name=\"txtDocsIDIs\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtDocsNameIs\" name=\"txtDocsNameIs\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtSourceNameIs\" name=\"txtSourceNameIs\" readonly=\"readonly\"  style=\"width:300px\"/>");
                    //<!-- 插入各种附件 End -->
                    sHtml.Append("</span></p>");
                    sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsIDIs','txtSourceNameIs','" + LicenseName + "')\"/>");
                    if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"从高拍仪扫描\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                    //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"从证件库选择\" class=\"button\" />");
                    sHtml.Append("<input type=\"checkbox\" name=\"cbBizIs\" id=\"cbBizIs\"  class=\"cbx\" alt=\"拿原件到办证大厅提交\"/>证件样本查看");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                    sHtml.Append("</table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");

                }
            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); sHtml = new StringBuilder(); sHtml.Append("获取数据信息出错…"); }
            }
            BizCNum = k.ToString();
            BizGNum = i.ToString();
            return sHtml.ToString();
        }

        public string GetBizCategoryLicenseEdit(string bizCode, string bizID, string action, string sysCate)
        {
            SqlDataReader sdr = null;
            StringBuilder sHtml = new StringBuilder();
            string LicenseID, LicenseName, LicenseType = "", LicenseTemplates = string.Empty;
            int i = 0; int k = 0;
            try
            {
                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=0";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">所<br />需<br />证<br />件</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseType = sdr["LicenseType"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        string docsPath = GetBizDocsOld(LicenseName, i, bizID);

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- 插入各种附件 Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- 插入各种附件 End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"从高拍仪扫描\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"从证件库选择\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">下载电子表格</a>"); }
                        if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                        i++;
                        k++;
                    }
                    sHtml.Append(" </table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                }
                    //申请表上的证照
                    m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=5";
                    sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                    if (sdr.HasRows)
                    {
                        sHtml.Append("<tr>");
                        sHtml.Append("<td class=\"form_b_title\">证<br />照</td>");
                        sHtml.Append("<td class=\"form_b_c\">");
                        sHtml.Append("<div class=\"form_table\">");
                        sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        while (sdr.Read())
                        {
                            LicenseID = sdr["LicenseID"].ToString();
                            LicenseName = sdr["LicenseName"].ToString();
                            LicenseType = sdr["LicenseType"].ToString();
                            LicenseTemplates = sdr["LicenseTemplates"].ToString();

                            string docsPath = GetBizDocsOld(LicenseName, i, bizID);

                            sHtml.Append("<tr>");
                            sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                            sHtml.Append("<td class=\"text\"><p><span>");
                            LicenseName = HttpUtility.UrlEncode(LicenseName);
                            //<!-- 插入各种附件 Start --> 
                            sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                            sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                            sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                            sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                            //<!-- 插入各种附件 End -->
                            sHtml.Append("</span></p>");
                            sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                            if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"从高拍仪扫描\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                            //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                            if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }
                            sHtml.Append("</td>");
                            sHtml.Append("</tr>");
                            i++;
                            k++;
                        }
                        sHtml.Append(" </table>");
                        sHtml.Append("</div>");
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                    }
                    sdr.Close(); sdr.Dispose();

                    if (action == "Attach")
                    {
                        //收件回执单
                        m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=3";
                        sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                        if (sdr.HasRows)
                        {
                            sHtml.Append("<tr>");
                            sHtml.Append("<td class=\"form_b_title\">回<br />执<br />单</td>");
                            sHtml.Append("<td class=\"form_b_c\">");
                            sHtml.Append("<div class=\"form_table\">");
                            sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            while (sdr.Read())
                            {
                                LicenseID = sdr["LicenseID"].ToString();
                                LicenseName = sdr["LicenseName"].ToString();
                                LicenseType = sdr["LicenseType"].ToString();
                                LicenseTemplates = sdr["LicenseTemplates"].ToString();

                                string docsPath = GetBizDocsOld(LicenseName, i, bizID);

                                sHtml.Append("<tr>");
                                sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                                sHtml.Append("<td class=\"text\"><p><span>");
                                LicenseName = HttpUtility.UrlEncode(LicenseName);
                                //<!-- 插入各种附件 Start -->
                                sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                                sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                                sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                                sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                                //<!-- 插入各种附件 End -->
                                sHtml.Append("</span></p>");
                                sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                                if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"从高拍仪扫描\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                                //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                                if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\"> "); }
                                sHtml.Append("</td>");
                                sHtml.Append("</tr>");
                                i++;
                                k++;
                            }
                            sHtml.Append(" </table>");
                            sHtml.Append("</div>");
                            sHtml.Append("</td>");
                            sHtml.Append("</tr>");
                        }
                        sdr.Close(); sdr.Dispose();
                    }

                sdr.Close();

                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=1";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">其<br />它<br />证<br />件</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseType = sdr["LicenseType"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        string docsPath = GetBizDocsOld(LicenseName, i, bizID);

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- 插入各种附件 Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- 插入各种附件 End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"从高拍仪扫描\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"从证件库选择\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">下载电子表格</a>"); }
                        if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                        i++;
                    }
                    sHtml.Append(" </table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                }
                sdr.Close(); sdr.Dispose();


                //获取当前申请表中男女户籍地编码
                GetBizContents(bizID);

                //if (bizCode == "0103" || bizCode == "0104" || bizCode == "0105" || bizCode == "0110")
                //{
                //    //单方
                //    if (!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA))
                //    { IsInnerArea = true; }
                //}
                //else
                //{
                //    //双方
                //    if ((!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)) || (!string.IsNullOrEmpty(this.RegAreaCodeB) && IsThisAreaCode(this.RegAreaCodeB)))
                //    { IsInnerArea = true; }
                //}
                IsInnerArea = false;
                if (IsInnerArea)
                {
                    LicenseName = HttpUtility.UrlEncode("辖区外纸质证明");

                    string docsPath = GetBizDocsOld("辖区外纸质证明", i, bizID);

                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">辖区外纸质证明</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    sHtml.Append("<tr>");
                    sHtml.Append("<th width=\"260\">辖区外需提供纸质证明：</th>");
                    sHtml.Append("<td class=\"text\"><p><span>");
                    //<!-- 插入各种附件 Start -->
                    sHtml.Append("<input id=\"txtDocsIDIs\" name=\"txtDocsIDIs\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtDocsNameIs\" name=\"txtDocsNameIs\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtSourceNameIs\" name=\"txtSourceNameIs\" readonly=\"readonly\"  style=\"width:300px\"/>");
                    //<!-- 插入各种附件 End -->
                    sHtml.Append("</span></p>");
                    sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsIDIs','txtSourceNameIs','" + LicenseName + "')\"/>");
                    if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"从高拍仪扫描\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                    //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"从证件库选择\" class=\"button\" />");
                    //sHtml.Append("<input type=\"checkbox\" name=\"cbBizIs\" class=\"cbx\" alt=\"拿原件到办证大厅提交\"/>证件样本查看");
                    if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }

                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                    sHtml.Append("</table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");

                }

            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); sHtml = new StringBuilder(); sHtml.Append("获取数据信息出错…"); }
            }
            BizCNum = k.ToString();
            BizGNum = i.ToString();
            return sHtml.ToString();
        }
        #endregion

        /// <summary>
        /// 获取该项业务所需证件
        /// </summary>
        /// <param name="bizCode"></param>
        /// <param name="bizCNum"></param>
        /// <param name="isInnerArea"></param>
        /// <returns></returns>
        public string GetBizCategoryLicenseWap(string bizCode, string bizID)
        {
            SqlDataReader sdr = null;
            StringBuilder sHtml = new StringBuilder();
            string LicenseID, LicenseName,LicenseType, LicenseTemplates = string.Empty;
            int i = 0; int k = 0;
            try
            {
                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=0";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<div class=\"part_form\">");
                    sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>所需证件</div>");
                    sHtml.Append("<ul>");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">" + LicenseName + "</p>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"0\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<p class=\"text\"><input type=\"file\" name=\"txtUploadFiles" + i + "\" id=\"txtUploadFiles" + i + "\" title=\"上传\" onchange=\"getPhotoSize(this)\"/></p>");
                       // sHtml.Append("<p class=\"scene\"><input type=\"checkbox\" id=\"cbBiz" + i + "\" name=\"cbBiz" + i + "\" class=\"cbx\" alt=\"拿原件到办证大厅提交\"/>&nbsp;证件样本查看");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">下载电子表格</a>"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");

                        i++;
                        k++;
                    }
                    sHtml.Append("</ul>");
                    sHtml.Append("</div>");
                }
                sdr.Close();




                //申请表上的证照
                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=5";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">证<br />照</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseType = sdr["LicenseType"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        string docsPath = GetBizDocsOld(LicenseName, i, bizID);

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- 插入各种附件 Start --> 
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- 插入各种附件 End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"从高拍仪扫描\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                        if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                        i++;
                        k++;
                    }
                    sHtml.Append(" </table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                }
                sdr.Close(); sdr.Dispose();

                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=1";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<div class=\"part_form\">");
                    sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>其它证件</div>");
                    sHtml.Append("<ul>");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">" + LicenseName + "</p>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<p class=\"text\"><input type=\"file\" name=\"txtUploadFiles" + i + "\" id=\"txtUploadFiles" + i + "\" title=\"上传\" onchange=\"getPhotoSize(this)\"/></p>");
                        //sHtml.Append("<p class=\"scene\"><input type=\"checkbox\" id=\"cbBiz" + i + "\" name=\"cbBiz" + i + "\" class=\"cbx\" alt=\"拿原件到办证大厅提交\"/>&nbsp;证件样本查看");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">下载电子表格</a>"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");

                        i++;
                    }
                    sHtml.Append("</ul>");
                    sHtml.Append("</div>");
                }
                sdr.Close(); sdr.Dispose();

                //获取当前申请表中男女户籍地编码
                GetBizContents(bizID);

                //if (bizCode == "0103" || bizCode == "0104" || bizCode == "0105" || bizCode == "0110")
                //{
                //    //单方
                //    if (!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA))
                //    { IsInnerArea = true; }
                //}
                //else
                //{
                //    //双方
                //    if ((!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)) || (!string.IsNullOrEmpty(this.RegAreaCodeB) && IsThisAreaCode(this.RegAreaCodeB)))
                //    { IsInnerArea = true; }
                //}
                IsInnerArea = false;
                if (IsInnerArea)
                {
                    sHtml.Append("<div class=\"part_form\">");
                    sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>辖区外纸质证明</div>");
                    sHtml.Append("<ul>");
                    sHtml.Append("<li>");
                    LicenseName = "辖区外纸质证明";
                    sHtml.Append("<p class=\"title\">" + LicenseName + "</p>");
                    LicenseName = HttpUtility.UrlEncode("辖区外纸质证明");
                    sHtml.Append("<input id=\"txtDocsNameIs\" name=\"txtDocsNameIs\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                    sHtml.Append("<p class=\"text\"><input type=\"file\" name=\"txtUploadFilesIs\" id=\"txtUploadFilesIs\" title=\"上传\" onchange=\"getPhotoSize(this)\"/></p>");
                    //sHtml.Append("<p class=\"scene\"><input type=\"checkbox\" id=\"cbBizIs\" name=\"cbBizIs\" class=\"cbx\" alt=\"拿原件到办证大厅提交\"/>&nbsp;证件样本查看");
                    sHtml.Append("</p>");
                    sHtml.Append("</li>");
                    sHtml.Append("</ul>");
                    sHtml.Append("</div>");
                    i++;
                }
            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); sHtml = new StringBuilder(); sHtml.Append("获取数据信息出错…"); }
            }
            BizCNum = k.ToString();
            BizGNum = i.ToString();
            return sHtml.ToString();
        }
        public string GetBizCategoryLicenseEdit(string bizCode, string bizID)
        {
            SqlDataReader sdr = null;
            StringBuilder sHtml = new StringBuilder();
            string LicenseID, LicenseName, LicenseType, LicenseTemplates = string.Empty;
            int i = 0; int k = 0;
            try
            {
                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=0";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">所<br />需<br />证<br />件</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseType = sdr["LicenseType"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        string docsPath = GetBizDocsOld(LicenseName, i, bizID);
                        if (!string.IsNullOrEmpty(docsPath)) docsPath = docsPath.Insert(docsPath.LastIndexOf("/") + 1, "micro-");

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- 插入各种附件 Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- 插入各种附件 End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"从证件库选择\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">下载电子表格</a>"); }
                        if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                        i++;
                        k++;
                    }
                    sHtml.Append(" </table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                }
                sdr.Close();




                //申请表上的证照
                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=5";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">证<br />照</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseType = sdr["LicenseType"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        string docsPath = GetBizDocsOld(LicenseName, i, bizID);

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- 插入各种附件 Start --> 
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- 插入各种附件 End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"从高拍仪扫描\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                        if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                        i++;
                        k++;
                    }
                    sHtml.Append(" </table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                }
                sdr.Close(); sdr.Dispose();
                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=1";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">其<br />它<br />证<br />件</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseType = sdr["LicenseType"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        string docsPath = GetBizDocsOld(LicenseName, i, bizID);

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- 插入各种附件 Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- 插入各种附件 End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"从证件库选择\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">下载电子表格</a>"); }
                        if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                        i++;
                    }
                    sHtml.Append(" </table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                }
                sdr.Close(); sdr.Dispose();

                //获取当前申请表中男女户籍地编码
                GetBizContents(bizID);

                //if (bizCode == "0103" || bizCode == "0104" || bizCode == "0105" || bizCode == "0110")
                //{
                //    //单方
                //    if (!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA))
                //    { IsInnerArea = true; }
                //}
                //else
                //{
                //    //双方
                //    if ((!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)) || (!string.IsNullOrEmpty(this.RegAreaCodeB) && IsThisAreaCode(this.RegAreaCodeB)))
                //    { IsInnerArea = true; }
                //}
                IsInnerArea = false;
                if (IsInnerArea)
                {
                    LicenseName = HttpUtility.UrlEncode("辖区外纸质证明");

                    string docsPath = GetBizDocsOld("辖区外纸质证明", i, bizID);

                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">辖区外纸质证明</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    sHtml.Append("<tr>");
                    sHtml.Append("<th width=\"260\">辖区外需提供纸质证明：</th>");
                    sHtml.Append("<td class=\"text\"><p><span>");
                    //<!-- 插入各种附件 Start -->
                    sHtml.Append("<input id=\"txtDocsIDIs\" name=\"txtDocsIDIs\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtDocsNameIs\" name=\"txtDocsNameIs\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtSourceNameIs\" name=\"txtSourceNameIs\" readonly=\"readonly\"  style=\"width:300px\"/>");
                    //<!-- 插入各种附件 End -->
                    sHtml.Append("</span></p>");
                    sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsIDIs','txtSourceNameIs','" + LicenseName + "')\"/>");
                    //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"从证件库选择\" class=\"button\" />");
                    //sHtml.Append("<input type=\"checkbox\" name=\"cbBizIs\" class=\"cbx\" alt=\"拿原件到办证大厅提交\"/>证件样本查看");
                    if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }

                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                    sHtml.Append("</table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");

                }

            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); sHtml = new StringBuilder(); sHtml.Append("获取数据信息出错…"); }
            }
            BizCNum = k.ToString();
            BizGNum = i.ToString();
            return sHtml.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bizCode"></param>
        /// <param name="bizID"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public string GetBizCategoryLicenseEdit(string bizCode, string bizID, string action)
        {
            SqlDataReader sdr = null;
            StringBuilder sHtml = new StringBuilder();
            string LicenseID, LicenseName, LicenseType = "", LicenseTemplates = string.Empty;
            int i = 0; int k = 0;
            try
            {
                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=0";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">所<br />需<br />证<br />件</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseType = sdr["LicenseType"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        string docsPath = GetBizDocsOld(LicenseName, i, bizID);

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- 插入各种附件 Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- 插入各种附件 End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"从证件库选择\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">下载电子表格</a>"); }
                        if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                        i++;
                        k++;
                    }
                    sHtml.Append(" </table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                }
                    //申请表上的证照
                    m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=5";
                    sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                    if (sdr.HasRows)
                    {
                        sHtml.Append("<tr>");
                        sHtml.Append("<td class=\"form_b_title\">证<br />照</td>");
                        sHtml.Append("<td class=\"form_b_c\">");
                        sHtml.Append("<div class=\"form_table\">");
                        sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        while (sdr.Read())
                        {
                            LicenseID = sdr["LicenseID"].ToString();
                            LicenseName = sdr["LicenseName"].ToString();
                            LicenseType = sdr["LicenseType"].ToString();
                            LicenseTemplates = sdr["LicenseTemplates"].ToString();

                            string docsPath = GetBizDocsOld(LicenseName, i, bizID);

                            sHtml.Append("<tr>");
                            sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                            sHtml.Append("<td class=\"text\"><p><span>");
                            LicenseName = HttpUtility.UrlEncode(LicenseName);
                            //<!-- 插入各种附件 Start --> 
                            sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                            sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                            sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                            sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                            //<!-- 插入各种附件 End -->
                            sHtml.Append("</span></p>");
                            sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                            //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                            if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }
                            sHtml.Append("</td>");
                            sHtml.Append("</tr>");
                            i++;
                            k++;
                        }
                        sHtml.Append(" </table>");
                        sHtml.Append("</div>");
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                    }
                    sdr.Close(); sdr.Dispose();

                    if (action == "Attach")
                    {
                        //收件回执单
                        m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=3";
                        sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                        if (sdr.HasRows)
                        {
                            sHtml.Append("<tr>");
                            sHtml.Append("<td class=\"form_b_title\">回<br />执<br />单</td>");
                            sHtml.Append("<td class=\"form_b_c\">");
                            sHtml.Append("<div class=\"form_table\">");
                            sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            while (sdr.Read())
                            {
                                LicenseID = sdr["LicenseID"].ToString();
                                LicenseName = sdr["LicenseName"].ToString();
                                LicenseType = sdr["LicenseType"].ToString();
                                LicenseTemplates = sdr["LicenseTemplates"].ToString();

                                string docsPath = GetBizDocsOld(LicenseName, i, bizID);

                                sHtml.Append("<tr>");
                                sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                                sHtml.Append("<td class=\"text\"><p><span>");
                                LicenseName = HttpUtility.UrlEncode(LicenseName);
                                //<!-- 插入各种附件 Start -->
                                sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                                sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                                sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                                sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                                //<!-- 插入各种附件 End -->
                                sHtml.Append("</span></p>");
                                sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                                //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                                if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }
                                sHtml.Append("</td>");
                                sHtml.Append("</tr>");
                                i++;
                                k++;
                            }
                            sHtml.Append(" </table>");
                            sHtml.Append("</div>");
                            sHtml.Append("</td>");
                            sHtml.Append("</tr>");
                        }
                        sdr.Close(); sdr.Dispose();
                    }

                sdr.Close();

                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=1";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">其<br />它<br />证<br />件</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseType = sdr["LicenseType"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        string docsPath = GetBizDocsOld(LicenseName, i, bizID);

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "：</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- 插入各种附件 Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- 插入各种附件 End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"从证件库选择\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">证件样本查看</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">下载电子表格</a>"); }
                        if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }
                        sHtml.Append("</td>");
                        sHtml.Append("</tr>");
                        i++;
                    }
                    sHtml.Append(" </table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                }
                sdr.Close(); sdr.Dispose();


                //获取当前申请表中男女户籍地编码
                GetBizContents(bizID);

                //if (bizCode == "0103" || bizCode == "0104" || bizCode == "0105" || bizCode == "0110")
                //{
                //    //单方
                //    if (!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA))
                //    { IsInnerArea = true; }
                //}
                //else
                //{
                //    //双方
                //    if ((!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)) || (!string.IsNullOrEmpty(this.RegAreaCodeB) && IsThisAreaCode(this.RegAreaCodeB)))
                //    { IsInnerArea = true; }
                //}
                IsInnerArea = false;
                if (IsInnerArea)
                {
                    LicenseName = HttpUtility.UrlEncode("辖区外纸质证明");

                    string docsPath = GetBizDocsOld("辖区外纸质证明", i, bizID);

                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">辖区外纸质证明</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    sHtml.Append("<tr>");
                    sHtml.Append("<th width=\"260\">辖区外需提供纸质证明：</th>");
                    sHtml.Append("<td class=\"text\"><p><span>");
                    //<!-- 插入各种附件 Start -->
                    sHtml.Append("<input id=\"txtDocsIDIs\" name=\"txtDocsIDIs\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtDocsNameIs\" name=\"txtDocsNameIs\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtSourceNameIs\" name=\"txtSourceNameIs\" readonly=\"readonly\"  style=\"width:300px\"/>");
                    //<!-- 插入各种附件 End -->
                    sHtml.Append("</span></p>");
                    sHtml.Append("<input type=\"button\" value=\"上传\" class=\"button\" onclick=\"SelecDocs('txtDocsIDIs','txtSourceNameIs','" + LicenseName + "')\"/>");
                    //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"从证件库选择\" class=\"button\" />");
                    //sHtml.Append("<input type=\"checkbox\" name=\"cbBizIs\" class=\"cbx\" alt=\"拿原件到办证大厅提交\"/>证件样本查看");
                    if (!string.IsNullOrEmpty(docsPath)) { sHtml.Append(" <img src=\"" + m_SvrUrl + docsPath + "\" style=\"vertical-align:middle;\" width=\"50px\" height=\"50px\"  rel=\"lightbox[zj]\">"); }

                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");
                    sHtml.Append("</table>");
                    sHtml.Append("</div>");
                    sHtml.Append("</td>");
                    sHtml.Append("</tr>");

                }

            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); sHtml = new StringBuilder(); sHtml.Append("获取数据信息出错…"); }
            }
            BizCNum = k.ToString();
            BizGNum = i.ToString();
            return sHtml.ToString();
        }

        #region 其他相关操作
        /// <summary>
        /// 判断是否已经有上传过附件，并返回附件 micro-  micro
        /// </summary>
        /// <param name="docsName"></param>
        /// <returns></returns>
        public string GetBizDocsOld(string docsName, int i, string bizID)
        {
            string commID = "", returnVal = string.Empty;
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT CommID,DocsPath,PersonID FROM BIZ_Docs WHERE BizID=" + bizID + " AND DocsName='" + docsName + "'";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        returnVal = sdr["DocsPath"].ToString();
                        commID = sdr["CommID"].ToString();
                        this.PersonID = sdr["PersonID"].ToString();
                    }
                }
                sdr.Close(); sdr.Dispose();
            }
            catch { if (sdr != null) { sdr.Close(); sdr.Dispose(); } returnVal = ""; }
            if (!String.IsNullOrEmpty(commID))
            {
                if (String.IsNullOrEmpty(BizDocsIDOld)) { BizDocsIDOld = commID; }
                else { BizDocsIDOld += "," + commID; }
                if (String.IsNullOrEmpty(BizDocsIDiOld)) { BizDocsIDiOld = i.ToString(); }
                else { BizDocsIDiOld += "," + i.ToString(); }

                if (docsName == "辖区外纸质证明") { BizDocsIDIsOld = commID; }
            }
            if (!string.IsNullOrEmpty(returnVal)) returnVal = returnVal.Insert(returnVal.LastIndexOf("/") + 1, "micro-");
            return returnVal;
        }
        #endregion

        #region 辖区外处理
        /// <summary>
        /// 判断是否是辖区内
        /// </summary>
        /// <param name="areaCode"></param>
        /// <returns>true 辖区外；false 辖区内</returns>
        public static bool IsThisAreaCode(string areaCode)
        {
            bool returnVal = true;
            if (m_SiteArea.Substring(0, 6) == areaCode.Substring(0, 6))
            { returnVal = false; }
            return returnVal;
        }
        /// <summary>
        /// 判断是否是辖区内
        /// </summary>
        /// <param name="areaCode"></param>
        /// <param name="type">0,县，1市，2省</param>
        /// <returns>true 辖区外；false 辖区内</returns>
        public static bool IsThisAreaCode(string areaCode, string type)
        {
            bool returnVal = true;
            switch (type)
            {
                case "0":
                    if (m_SiteArea.Substring(0, 6) == areaCode.Substring(0, 6))
                    { returnVal = false; }
                    break;
                case "1":
                    if (m_SiteArea.Substring(0, 4) == areaCode.Substring(0, 4))
                    { returnVal = false; }
                    break;
                case "2":
                    if (m_SiteArea.Substring(0, 2) == areaCode.Substring(0, 2))
                    { returnVal = false; }
                    break;
                default:
                    break;
            }
            return returnVal;
        }
        /// <summary>
        /// 获取当前申请表信息
        /// </summary>
        private void GetBizContents(string bizID)
        {
            string bizCode = string.Empty;
            SqlDataReader sdr = null;
            try
            {
                string sqlParams = "SELECT BizCode,RegAreaCodeA,RegAreaCodeB,CurAreaCodeA,CurAreaCodeB FROM BIZ_Contents WHERE BizID=" + bizID;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        bizCode = sdr["BizCode"].ToString();
                        if (bizCode == "0107")
                        {
                            this.RegAreaCodeA = sdr["CurAreaCodeA"].ToString();//男
                            this.RegAreaCodeB = sdr["CurAreaCodeB"].ToString();//女
                        }
                        else
                        {
                            this.RegAreaCodeA = sdr["RegAreaCodeA"].ToString();//男
                            this.RegAreaCodeB = sdr["RegAreaCodeB"].ToString();//女
                        }
                    }
                }
                sdr.Close(); sdr.Dispose();
            }
            catch { if (sdr != null) { sdr.Close(); sdr.Dispose(); } }
        }
        /// <summary>
        /// 辖区外自动处理流程表 流程
        /// </summary>
        /// <param name="sex">0:男；1:女</param>
        public static void UpdateBizWorkFlows(string bizID, string sex, string DocsIDIs)
        {
            DocsIDIs = DocsIDIs.Substring(0, DocsIDIs.Length - 1);
            string DocsPath = CommPage.GetSingleVal("SELECT DocsPath FROM BIZ_Docs WHERE CommID=" + DocsIDIs);
            string Comments = "<a href=\"" + m_SvrUrl + DocsPath + "\" rel=\"lightbox[zj]\">已提交证明附件</a>";
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE (BizStep=2 OR BizStep=4) AND BizID=" + bizID); }
            else { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE (BizStep=1 OR BizStep=3) AND BizID=" + bizID); }

            if (DbHelperSQL.ExecuteSqlTran(list) > 0) { }
            list = null;
        }
        /// <summary>
        /// 辖区外自动处理流程表 流程
        /// </summary>
        /// <param name="sex">0:男；1:女</param>
        public static void UpdateBizWorkFlows(string bizID, string bizCode, string sex, string DocsIDIs)
        {
            DocsIDIs = DocsIDIs.Substring(0, DocsIDIs.Length - 1);
            string DocsPath = CommPage.GetSingleVal("SELECT DocsPath FROM BIZ_Docs WHERE CommID=" + DocsIDIs);
            string Comments = "<a href=\"" + m_SvrUrl + DocsPath + "\" rel=\"lightbox[zj]\">已提交证明附件</a>";
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            if (bizCode == "0101")
            {
                if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=2 AND BizID=" + bizID); }
                if (sex == "1") { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=1 AND BizID=" + bizID); }
            }
            else if (bizCode == "0102" || bizCode == "0107" || bizCode == "0108" || bizCode == "0122")
            {
                if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=2 AND BizID=" + bizID); }
                else { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=1) AND BizID=" + bizID); }
            }
            else if (bizCode == "0106" || bizCode == "0109")
            {
                if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=1 AND BizID=" + bizID); }
                else { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=2 AND BizID=" + bizID); }
            }
            else
            { }
            if (DbHelperSQL.ExecuteSqlTran(list) > 0) { }
            list = null;
        }
        /// <summary>
        /// 辖区外自动处理流程表 流程
        /// </summary>
        /// <param name="sex">0:男；1:女</param>
        public static void UpdateBizWorkFlowsWap(string bizID, string sex, string docsPath)
        {
            string Comments = "<a href=\"" + m_SvrUrl + docsPath + "\" rel=\"lightbox[zj]\">已提交证明附件</a>";
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE (BizStep=2 OR BizStep=4) AND BizID=" + bizID); }
            else { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE (BizStep=1 OR BizStep=3) AND BizID=" + bizID); }

            if (DbHelperSQL.ExecuteSqlTran(list) > 0) { }
            list = null;
        }
        /// <summary>
        /// 辖区外自动处理流程表 流程
        /// </summary>
        /// <param name="sex">0:男；1:女</param>
        public static void UpdateBizWorkFlowsWap(string bizID, string bizCode, string sex, string docsPath)
        {
            string Comments = "<a href=\"" + m_SvrUrl + docsPath + "\" rel=\"lightbox[zj]\">已提交证明附件</a>";
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            if (bizCode == "0101")
            {
                if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=2 AND BizID=" + bizID); }
                if (sex == "1") { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=1 AND BizID=" + bizID); }
            }
            else if (bizCode == "0102" || bizCode == "0107" || bizCode == "0108" || bizCode == "0122")
            {
                if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=2 AND BizID=" + bizID); }
                else { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=1) AND BizID=" + bizID); }
            }
            else if (bizCode == "0106" || bizCode == "0109")
            {
                if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=1 AND BizID=" + bizID); }
                else { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=2 AND BizID=" + bizID); }
            }
            else
            {
                if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE (BizStep=2 OR BizStep=4) AND BizID=" + bizID); }
                else { list.Add("UPDATE BIZ_WorkFlows SET Approval='外地计生机构',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE (BizStep=1 OR BizStep=3) AND BizID=" + bizID); }
            }
            if (DbHelperSQL.ExecuteSqlTran(list) > 0) { }
            list = null;
        }
        #endregion
    }
}
