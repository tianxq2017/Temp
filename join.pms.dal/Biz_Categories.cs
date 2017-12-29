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
        #region �ֶ�
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
        /// ��ȡָ��ҵ������ҵ����Ϣ
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
        /// ��ȡ��ǰҵ����Ϣҳͷ
        /// </summary>
        /// <param name="bizCode"></param>
        /// <param name="bizCode"></param>
        /// <returns></returns>
        public string GetCategories(string bizCode, string favUrl)
        {
            string str = string.Empty;
            str = "<div class=\"column_r\">";
            str += "<ul>";
            //str += "<li><a href=\"javascript:void(0);\" onclick=\"AddFavorites('" + BizNameFull + "','" + favUrl + "','" + bizCode + "','0');\">��ӵ��ղ�</a></li>";
            //str += "<li class=\"share\"><div class=\"bsync-custom icon-blue\"><a title=\"һ����������΢�����罻����\" class=\"bshare-bsync\" onclick=\"javascript:bSync.share(event)\">һ������</a><span class=\"BSHARE_COUNT bshare-share-count\">0</span></div></li>";
            //if (!string.IsNullOrEmpty(BizImg))
            //{
            //    str += "<li><a href=\"" + m_SvrUrl + BizImg + "\" rel=\"lightbox[zj]\" title=\"�鿴����֤����" + BizNameFull + "\">�鿴����֤��</a></li>";
            //}
            str += "</ul>";
            str += "</div>";
            //str += "<p class=\"column_title\"><span><img src=\"/images/ico/biz" + bizCode + ".png\" /></span><b>" + BizNameFull + "</b></p>";
            //���´���ǰλ��
            str += "<p class=\"column_title\">";
            str += "<span class=\"ico\"><img src=\"/images/ico/biz" + bizCode + ".png\" /></span>";
            str += "<span class=\"title_bg\">";
            str += "<span class=\"title\">" + BizNameFull + "</span>";
            str += "<span class=\"crumb\"><a href=\"/\">��ҳ</a><b>&gt;</b>ҵ������</span>";
            str += "</span>";
            str += "</p>";
            return str;
        }
        /// <summary>
        /// ��ȡ����ҵ������֤��
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
                    sHtml.Append("<td class=\"form_b_title\">��<br />��<br />֤<br />��</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- ������ָ��� Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"0\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- ������ָ��� End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        sHtml.Append("<input type=\"button\" value=\"�Ӹ�����ɨ��\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        // <input type="button" value="�Ӹ�����ɨ��" class="button" onclick="SelecDocsByGpy('txtDocsID3','txtSourceName3','%e5%a4%ab%e5%a6%87%e5%8f%8c%e6%96%b9%e7%bb%93%e5%a9%9a%e8%af%81')"/>
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"��֤����ѡ��\" class=\"button\" />");
                        // sHtml.Append("<input type=\"checkbox\" id=\"cbBiz" + i + "\" name=\"cbBiz" + i + "\" class=\"cbx\" alt=\"��ԭ������֤�����ύ\"/>֤�������鿴");
                        //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">���ص��ӱ��</a>"); }
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





                //������ϵ�֤��
                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=5";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">֤<br />��</td>");
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
                        sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- ������ָ��� Start --> 
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- ������ָ��� End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        sHtml.Append("<input type=\"button\" value=\"�Ӹ�����ɨ��\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
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
                    sHtml.Append("<td class=\"form_b_title\">��<br />��<br />֤<br />��</td>");
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
                        sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- ������ָ��� Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- ������ָ��� End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        sHtml.Append("<input type=\"button\" value=\"�Ӹ�����ɨ��\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"��֤����ѡ��\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">���ص��ӱ��</a>"); }
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

                //��ȡ��ǰ���������Ů�����ر���
                GetBizContents(bizID);

                int h = 0;
                //if (bizCode == "0103" || bizCode == "0104" || bizCode == "0105" || bizCode == "0110")
                //{
                //    //����
                //    if (!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)){IsInnerArea = true;}
                //}
                //else
                //{
                //    //˫��
                //    if ((!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)) || (!string.IsNullOrEmpty(this.RegAreaCodeB) && IsThisAreaCode(this.RegAreaCodeB))){ IsInnerArea = true; }

                //}
                IsInnerArea = false;
                if (IsInnerArea)
                {
                    LicenseName = HttpUtility.UrlEncode("Ͻ����ֽ��֤��");
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">Ͻ����ֽ��֤��</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    sHtml.Append("<tr>");
                    sHtml.Append("<th width=\"260\">Ͻ�������ṩֽ��֤����</th>");
                    sHtml.Append("<td class=\"text\"><p><span>");
                    //<!-- ������ָ��� Start -->
                    sHtml.Append("<input id=\"txtDocsIDIs\" name=\"txtDocsIDIs\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtDocsNameIs\" name=\"txtDocsNameIs\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtSourceNameIs\" name=\"txtSourceNameIs\" readonly=\"readonly\"  style=\"width:300px\"/>");
                    //<!-- ������ָ��� End -->
                    sHtml.Append("</span></p>");
                    sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsIDIs','txtSourceNameIs','" + LicenseName + "')\"/>");
                    sHtml.Append("<input type=\"button\" value=\"�Ӹ�����ɨ��\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsIDIs','txtSourceNameIs','" + LicenseName + "')\"/>");
                    
                    //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"��֤����ѡ��\" class=\"button\" />");
                    sHtml.Append("<input type=\"checkbox\" name=\"cbBizIs\" id=\"cbBizIs\"  class=\"cbx\" alt=\"��ԭ������֤�����ύ\"/>֤�������鿴");
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
                if (sdr != null) { sdr.Close(); sdr.Dispose(); sHtml = new StringBuilder(); sHtml.Append("��ȡ������Ϣ����"); }
            }
            BizCNum = k.ToString();
            BizGNum = i.ToString();
            return sHtml.ToString();
        }

        #region �����ϴ����أ���������ϵͳ�ı�ʶ ϵͳ���ࣺ1,����ƽ̨��2,Ⱥ��ƽ̨
        /// <summary>
        /// ��ȡҵ�񸽼��������ϴ�
        /// </summary>
        /// <param name="bizCode"></param>
        /// <param name="bizID"></param>
        /// <param name="sysCate">ϵͳ���ࣺ1,����ƽ̨��2,Ⱥ��ƽ̨</param>
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
                    sHtml.Append("<td class=\"form_b_title\">��<br />��<br />֤<br />��</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- ������ָ��� Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"0\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- ������ָ��� End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"�Ӹ�����ɨ��\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        // <input type="button" value="�Ӹ�����ɨ��" class="button" onclick="SelecDocsByGpy('txtDocsID3','txtSourceName3','%e5%a4%ab%e5%a6%87%e5%8f%8c%e6%96%b9%e7%bb%93%e5%a9%9a%e8%af%81')"/>
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"��֤����ѡ��\" class=\"button\" />");
                        // sHtml.Append("<input type=\"checkbox\" id=\"cbBiz" + i + "\" name=\"cbBiz" + i + "\" class=\"cbx\" alt=\"��ԭ������֤�����ύ\"/>֤�������鿴");
                        //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">���ص��ӱ��</a>"); }
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

                



                //������ϵ�֤��
                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=5";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">֤<br />��</td>");
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
                        sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- ������ָ��� Start --> 
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- ������ָ��� End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"�Ӹ�����ɨ��\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
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
                    sHtml.Append("<td class=\"form_b_title\">��<br />��<br />֤<br />��</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    while (sdr.Read())
                    {
                        LicenseID = sdr["LicenseID"].ToString();
                        LicenseName = sdr["LicenseName"].ToString();
                        LicenseTemplates = sdr["LicenseTemplates"].ToString();

                        sHtml.Append("<tr>");
                        sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- ������ָ��� Start -->
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- ������ָ��� End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"�Ӹ�����ɨ��\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"��֤����ѡ��\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">���ص��ӱ��</a>"); }
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

                //��ȡ��ǰ���������Ů�����ر���
                GetBizContents(bizID);

                int h = 0;
                //if (bizCode == "0103" || bizCode == "0104" || bizCode == "0105" || bizCode == "0110")
                //{
                //    //����
                //    if (!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)){IsInnerArea = true;}
                //}
                //else
                //{
                //    //˫��
                //    if ((!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)) || (!string.IsNullOrEmpty(this.RegAreaCodeB) && IsThisAreaCode(this.RegAreaCodeB))){ IsInnerArea = true; }

                //}
                IsInnerArea = false;
                if (IsInnerArea)
                {
                    LicenseName = HttpUtility.UrlEncode("Ͻ����ֽ��֤��");
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">Ͻ����ֽ��֤��</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    sHtml.Append("<tr>");
                    sHtml.Append("<th width=\"260\">Ͻ�������ṩֽ��֤����</th>");
                    sHtml.Append("<td class=\"text\"><p><span>");
                    //<!-- ������ָ��� Start -->
                    sHtml.Append("<input id=\"txtDocsIDIs\" name=\"txtDocsIDIs\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtDocsNameIs\" name=\"txtDocsNameIs\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtSourceNameIs\" name=\"txtSourceNameIs\" readonly=\"readonly\"  style=\"width:300px\"/>");
                    //<!-- ������ָ��� End -->
                    sHtml.Append("</span></p>");
                    sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsIDIs','txtSourceNameIs','" + LicenseName + "')\"/>");
                    if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"�Ӹ�����ɨ��\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                    //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"��֤����ѡ��\" class=\"button\" />");
                    sHtml.Append("<input type=\"checkbox\" name=\"cbBizIs\" id=\"cbBizIs\"  class=\"cbx\" alt=\"��ԭ������֤�����ύ\"/>֤�������鿴");
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
                if (sdr != null) { sdr.Close(); sdr.Dispose(); sHtml = new StringBuilder(); sHtml.Append("��ȡ������Ϣ����"); }
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
                    sHtml.Append("<td class=\"form_b_title\">��<br />��<br />֤<br />��</td>");
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
                        sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- ������ָ��� Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- ������ָ��� End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"�Ӹ�����ɨ��\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"��֤����ѡ��\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">���ص��ӱ��</a>"); }
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
                    //������ϵ�֤��
                    m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=5";
                    sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                    if (sdr.HasRows)
                    {
                        sHtml.Append("<tr>");
                        sHtml.Append("<td class=\"form_b_title\">֤<br />��</td>");
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
                            sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                            sHtml.Append("<td class=\"text\"><p><span>");
                            LicenseName = HttpUtility.UrlEncode(LicenseName);
                            //<!-- ������ָ��� Start --> 
                            sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                            sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                            sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                            sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                            //<!-- ������ָ��� End -->
                            sHtml.Append("</span></p>");
                            sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                            if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"�Ӹ�����ɨ��\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                            //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
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
                        //�ռ���ִ��
                        m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=3";
                        sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                        if (sdr.HasRows)
                        {
                            sHtml.Append("<tr>");
                            sHtml.Append("<td class=\"form_b_title\">��<br />ִ<br />��</td>");
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
                                sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                                sHtml.Append("<td class=\"text\"><p><span>");
                                LicenseName = HttpUtility.UrlEncode(LicenseName);
                                //<!-- ������ָ��� Start -->
                                sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                                sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                                sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                                sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                                //<!-- ������ָ��� End -->
                                sHtml.Append("</span></p>");
                                sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                                if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"�Ӹ�����ɨ��\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                                //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
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
                    sHtml.Append("<td class=\"form_b_title\">��<br />��<br />֤<br />��</td>");
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
                        sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- ������ָ��� Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- ������ָ��� End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"�Ӹ�����ɨ��\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"��֤����ѡ��\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">���ص��ӱ��</a>"); }
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


                //��ȡ��ǰ���������Ů�����ر���
                GetBizContents(bizID);

                //if (bizCode == "0103" || bizCode == "0104" || bizCode == "0105" || bizCode == "0110")
                //{
                //    //����
                //    if (!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA))
                //    { IsInnerArea = true; }
                //}
                //else
                //{
                //    //˫��
                //    if ((!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)) || (!string.IsNullOrEmpty(this.RegAreaCodeB) && IsThisAreaCode(this.RegAreaCodeB)))
                //    { IsInnerArea = true; }
                //}
                IsInnerArea = false;
                if (IsInnerArea)
                {
                    LicenseName = HttpUtility.UrlEncode("Ͻ����ֽ��֤��");

                    string docsPath = GetBizDocsOld("Ͻ����ֽ��֤��", i, bizID);

                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">Ͻ����ֽ��֤��</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    sHtml.Append("<tr>");
                    sHtml.Append("<th width=\"260\">Ͻ�������ṩֽ��֤����</th>");
                    sHtml.Append("<td class=\"text\"><p><span>");
                    //<!-- ������ָ��� Start -->
                    sHtml.Append("<input id=\"txtDocsIDIs\" name=\"txtDocsIDIs\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtDocsNameIs\" name=\"txtDocsNameIs\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtSourceNameIs\" name=\"txtSourceNameIs\" readonly=\"readonly\"  style=\"width:300px\"/>");
                    //<!-- ������ָ��� End -->
                    sHtml.Append("</span></p>");
                    sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsIDIs','txtSourceNameIs','" + LicenseName + "')\"/>");
                    if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"�Ӹ�����ɨ��\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                    //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"��֤����ѡ��\" class=\"button\" />");
                    //sHtml.Append("<input type=\"checkbox\" name=\"cbBizIs\" class=\"cbx\" alt=\"��ԭ������֤�����ύ\"/>֤�������鿴");
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
                if (sdr != null) { sdr.Close(); sdr.Dispose(); sHtml = new StringBuilder(); sHtml.Append("��ȡ������Ϣ����"); }
            }
            BizCNum = k.ToString();
            BizGNum = i.ToString();
            return sHtml.ToString();
        }
        #endregion

        /// <summary>
        /// ��ȡ����ҵ������֤��
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
                    sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>����֤��</div>");
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
                        sHtml.Append("<p class=\"text\"><input type=\"file\" name=\"txtUploadFiles" + i + "\" id=\"txtUploadFiles" + i + "\" title=\"�ϴ�\" onchange=\"getPhotoSize(this)\"/></p>");
                       // sHtml.Append("<p class=\"scene\"><input type=\"checkbox\" id=\"cbBiz" + i + "\" name=\"cbBiz" + i + "\" class=\"cbx\" alt=\"��ԭ������֤�����ύ\"/>&nbsp;֤�������鿴");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">���ص��ӱ��</a>"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");

                        i++;
                        k++;
                    }
                    sHtml.Append("</ul>");
                    sHtml.Append("</div>");
                }
                sdr.Close();




                //������ϵ�֤��
                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=5";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">֤<br />��</td>");
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
                        sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- ������ָ��� Start --> 
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- ������ָ��� End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"�Ӹ�����ɨ��\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
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
                    sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>����֤��</div>");
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
                        sHtml.Append("<p class=\"text\"><input type=\"file\" name=\"txtUploadFiles" + i + "\" id=\"txtUploadFiles" + i + "\" title=\"�ϴ�\" onchange=\"getPhotoSize(this)\"/></p>");
                        //sHtml.Append("<p class=\"scene\"><input type=\"checkbox\" id=\"cbBiz" + i + "\" name=\"cbBiz" + i + "\" class=\"cbx\" alt=\"��ԭ������֤�����ύ\"/>&nbsp;֤�������鿴");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">���ص��ӱ��</a>"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");

                        i++;
                    }
                    sHtml.Append("</ul>");
                    sHtml.Append("</div>");
                }
                sdr.Close(); sdr.Dispose();

                //��ȡ��ǰ���������Ů�����ر���
                GetBizContents(bizID);

                //if (bizCode == "0103" || bizCode == "0104" || bizCode == "0105" || bizCode == "0110")
                //{
                //    //����
                //    if (!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA))
                //    { IsInnerArea = true; }
                //}
                //else
                //{
                //    //˫��
                //    if ((!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)) || (!string.IsNullOrEmpty(this.RegAreaCodeB) && IsThisAreaCode(this.RegAreaCodeB)))
                //    { IsInnerArea = true; }
                //}
                IsInnerArea = false;
                if (IsInnerArea)
                {
                    sHtml.Append("<div class=\"part_form\">");
                    sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>Ͻ����ֽ��֤��</div>");
                    sHtml.Append("<ul>");
                    sHtml.Append("<li>");
                    LicenseName = "Ͻ����ֽ��֤��";
                    sHtml.Append("<p class=\"title\">" + LicenseName + "</p>");
                    LicenseName = HttpUtility.UrlEncode("Ͻ����ֽ��֤��");
                    sHtml.Append("<input id=\"txtDocsNameIs\" name=\"txtDocsNameIs\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                    sHtml.Append("<p class=\"text\"><input type=\"file\" name=\"txtUploadFilesIs\" id=\"txtUploadFilesIs\" title=\"�ϴ�\" onchange=\"getPhotoSize(this)\"/></p>");
                    //sHtml.Append("<p class=\"scene\"><input type=\"checkbox\" id=\"cbBizIs\" name=\"cbBizIs\" class=\"cbx\" alt=\"��ԭ������֤�����ύ\"/>&nbsp;֤�������鿴");
                    sHtml.Append("</p>");
                    sHtml.Append("</li>");
                    sHtml.Append("</ul>");
                    sHtml.Append("</div>");
                    i++;
                }
            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); sHtml = new StringBuilder(); sHtml.Append("��ȡ������Ϣ����"); }
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
                    sHtml.Append("<td class=\"form_b_title\">��<br />��<br />֤<br />��</td>");
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
                        sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- ������ָ��� Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- ������ָ��� End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"��֤����ѡ��\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">���ص��ӱ��</a>"); }
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




                //������ϵ�֤��
                m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=5";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">֤<br />��</td>");
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
                        sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- ������ָ��� Start --> 
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- ������ָ��� End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //if (sysCate == "1") sHtml.Append("<input type=\"button\" value=\"�Ӹ�����ɨ��\" class=\"button\" onclick=\"SelecDocsByGpy('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
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
                    sHtml.Append("<td class=\"form_b_title\">��<br />��<br />֤<br />��</td>");
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
                        sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- ������ָ��� Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- ������ָ��� End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"��֤����ѡ��\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">���ص��ӱ��</a>"); }
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

                //��ȡ��ǰ���������Ů�����ر���
                GetBizContents(bizID);

                //if (bizCode == "0103" || bizCode == "0104" || bizCode == "0105" || bizCode == "0110")
                //{
                //    //����
                //    if (!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA))
                //    { IsInnerArea = true; }
                //}
                //else
                //{
                //    //˫��
                //    if ((!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)) || (!string.IsNullOrEmpty(this.RegAreaCodeB) && IsThisAreaCode(this.RegAreaCodeB)))
                //    { IsInnerArea = true; }
                //}
                IsInnerArea = false;
                if (IsInnerArea)
                {
                    LicenseName = HttpUtility.UrlEncode("Ͻ����ֽ��֤��");

                    string docsPath = GetBizDocsOld("Ͻ����ֽ��֤��", i, bizID);

                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">Ͻ����ֽ��֤��</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    sHtml.Append("<tr>");
                    sHtml.Append("<th width=\"260\">Ͻ�������ṩֽ��֤����</th>");
                    sHtml.Append("<td class=\"text\"><p><span>");
                    //<!-- ������ָ��� Start -->
                    sHtml.Append("<input id=\"txtDocsIDIs\" name=\"txtDocsIDIs\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtDocsNameIs\" name=\"txtDocsNameIs\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtSourceNameIs\" name=\"txtSourceNameIs\" readonly=\"readonly\"  style=\"width:300px\"/>");
                    //<!-- ������ָ��� End -->
                    sHtml.Append("</span></p>");
                    sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsIDIs','txtSourceNameIs','" + LicenseName + "')\"/>");
                    //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"��֤����ѡ��\" class=\"button\" />");
                    //sHtml.Append("<input type=\"checkbox\" name=\"cbBizIs\" class=\"cbx\" alt=\"��ԭ������֤�����ύ\"/>֤�������鿴");
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
                if (sdr != null) { sdr.Close(); sdr.Dispose(); sHtml = new StringBuilder(); sHtml.Append("��ȡ������Ϣ����"); }
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
                    sHtml.Append("<td class=\"form_b_title\">��<br />��<br />֤<br />��</td>");
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
                        sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- ������ָ��� Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- ������ָ��� End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"��֤����ѡ��\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">���ص��ӱ��</a>"); }
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
                    //������ϵ�֤��
                    m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=5";
                    sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                    if (sdr.HasRows)
                    {
                        sHtml.Append("<tr>");
                        sHtml.Append("<td class=\"form_b_title\">֤<br />��</td>");
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
                            sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                            sHtml.Append("<td class=\"text\"><p><span>");
                            LicenseName = HttpUtility.UrlEncode(LicenseName);
                            //<!-- ������ָ��� Start --> 
                            sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                            sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                            sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                            sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                            //<!-- ������ָ��� End -->
                            sHtml.Append("</span></p>");
                            sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                            //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
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
                        //�ռ���ִ��
                        m_SqlParams = "SELECT LicenseID,LicenseName,LicenseType,LicenseTemplates FROM BIZ_CategoryLicense WHERE BizCode like '" + bizCode + "%' AND LicenseType=3";
                        sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                        if (sdr.HasRows)
                        {
                            sHtml.Append("<tr>");
                            sHtml.Append("<td class=\"form_b_title\">��<br />ִ<br />��</td>");
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
                                sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                                sHtml.Append("<td class=\"text\"><p><span>");
                                LicenseName = HttpUtility.UrlEncode(LicenseName);
                                //<!-- ������ָ��� Start -->
                                sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                                sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                                sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                                sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                                //<!-- ������ָ��� End -->
                                sHtml.Append("</span></p>");
                                sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                                //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
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
                    sHtml.Append("<td class=\"form_b_title\">��<br />��<br />֤<br />��</td>");
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
                        sHtml.Append("<th width=\"260\">" + LicenseName + "��</th>");
                        sHtml.Append("<td class=\"text\"><p><span>");
                        LicenseName = HttpUtility.UrlEncode(LicenseName);
                        //<!-- ������ָ��� Start -->
                        sHtml.Append("<input id=\"txtType" + i + "\" name=\"txtType" + i + "\" value=\"" + LicenseType + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsID" + i + "\" name=\"txtDocsID" + i + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtDocsName" + i + "\" name=\"txtDocsName" + i + "\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                        sHtml.Append("<input id=\"txtSourceName" + i + "\" name=\"txtSourceName" + i + "\" readonly=\"readonly\"  style=\"width:300px\"/>");
                        //<!-- ������ָ��� End -->
                        sHtml.Append("</span></p>");
                        sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsID" + i + "','txtSourceName" + i + "','" + LicenseName + "')\"/>");
                        //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"��֤����ѡ��\" class=\"button\" />");
                        //sHtml.Append("<a href=\"#\">֤�������鿴</a>");
                        if (!string.IsNullOrEmpty(LicenseTemplates)) { sHtml.Append("&nbsp;&nbsp;&nbsp;<a href=\"" + m_SvrUrl + LicenseTemplates + "\">���ص��ӱ��</a>"); }
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


                //��ȡ��ǰ���������Ů�����ر���
                GetBizContents(bizID);

                //if (bizCode == "0103" || bizCode == "0104" || bizCode == "0105" || bizCode == "0110")
                //{
                //    //����
                //    if (!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA))
                //    { IsInnerArea = true; }
                //}
                //else
                //{
                //    //˫��
                //    if ((!string.IsNullOrEmpty(this.RegAreaCodeA) && IsThisAreaCode(this.RegAreaCodeA)) || (!string.IsNullOrEmpty(this.RegAreaCodeB) && IsThisAreaCode(this.RegAreaCodeB)))
                //    { IsInnerArea = true; }
                //}
                IsInnerArea = false;
                if (IsInnerArea)
                {
                    LicenseName = HttpUtility.UrlEncode("Ͻ����ֽ��֤��");

                    string docsPath = GetBizDocsOld("Ͻ����ֽ��֤��", i, bizID);

                    sHtml.Append("<tr>");
                    sHtml.Append("<td class=\"form_b_title\">Ͻ����ֽ��֤��</td>");
                    sHtml.Append("<td class=\"form_b_c\">");
                    sHtml.Append("<div class=\"form_table\">");
                    sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    sHtml.Append("<tr>");
                    sHtml.Append("<th width=\"260\">Ͻ�������ṩֽ��֤����</th>");
                    sHtml.Append("<td class=\"text\"><p><span>");
                    //<!-- ������ָ��� Start -->
                    sHtml.Append("<input id=\"txtDocsIDIs\" name=\"txtDocsIDIs\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtDocsNameIs\" name=\"txtDocsNameIs\" value=\"" + LicenseName + "\" type=\"hidden\" />");
                    sHtml.Append("<input id=\"txtSourceNameIs\" name=\"txtSourceNameIs\" readonly=\"readonly\"  style=\"width:300px\"/>");
                    //<!-- ������ָ��� End -->
                    sHtml.Append("</span></p>");
                    sHtml.Append("<input type=\"button\" value=\"�ϴ�\" class=\"button\" onclick=\"SelecDocs('txtDocsIDIs','txtSourceNameIs','" + LicenseName + "')\"/>");
                    //sHtml.Append("<input type=\"submit\" name=\"Submit\" value=\"��֤����ѡ��\" class=\"button\" />");
                    //sHtml.Append("<input type=\"checkbox\" name=\"cbBizIs\" class=\"cbx\" alt=\"��ԭ������֤�����ύ\"/>֤�������鿴");
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
                if (sdr != null) { sdr.Close(); sdr.Dispose(); sHtml = new StringBuilder(); sHtml.Append("��ȡ������Ϣ����"); }
            }
            BizCNum = k.ToString();
            BizGNum = i.ToString();
            return sHtml.ToString();
        }

        #region ������ز���
        /// <summary>
        /// �ж��Ƿ��Ѿ����ϴ��������������ظ��� micro-  micro
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

                if (docsName == "Ͻ����ֽ��֤��") { BizDocsIDIsOld = commID; }
            }
            if (!string.IsNullOrEmpty(returnVal)) returnVal = returnVal.Insert(returnVal.LastIndexOf("/") + 1, "micro-");
            return returnVal;
        }
        #endregion

        #region Ͻ���⴦��
        /// <summary>
        /// �ж��Ƿ���Ͻ����
        /// </summary>
        /// <param name="areaCode"></param>
        /// <returns>true Ͻ���⣻false Ͻ����</returns>
        public static bool IsThisAreaCode(string areaCode)
        {
            bool returnVal = true;
            if (m_SiteArea.Substring(0, 6) == areaCode.Substring(0, 6))
            { returnVal = false; }
            return returnVal;
        }
        /// <summary>
        /// �ж��Ƿ���Ͻ����
        /// </summary>
        /// <param name="areaCode"></param>
        /// <param name="type">0,�أ�1�У�2ʡ</param>
        /// <returns>true Ͻ���⣻false Ͻ����</returns>
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
        /// ��ȡ��ǰ�������Ϣ
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
                            this.RegAreaCodeA = sdr["CurAreaCodeA"].ToString();//��
                            this.RegAreaCodeB = sdr["CurAreaCodeB"].ToString();//Ů
                        }
                        else
                        {
                            this.RegAreaCodeA = sdr["RegAreaCodeA"].ToString();//��
                            this.RegAreaCodeB = sdr["RegAreaCodeB"].ToString();//Ů
                        }
                    }
                }
                sdr.Close(); sdr.Dispose();
            }
            catch { if (sdr != null) { sdr.Close(); sdr.Dispose(); } }
        }
        /// <summary>
        /// Ͻ�����Զ��������̱� ����
        /// </summary>
        /// <param name="sex">0:�У�1:Ů</param>
        public static void UpdateBizWorkFlows(string bizID, string sex, string DocsIDIs)
        {
            DocsIDIs = DocsIDIs.Substring(0, DocsIDIs.Length - 1);
            string DocsPath = CommPage.GetSingleVal("SELECT DocsPath FROM BIZ_Docs WHERE CommID=" + DocsIDIs);
            string Comments = "<a href=\"" + m_SvrUrl + DocsPath + "\" rel=\"lightbox[zj]\">���ύ֤������</a>";
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE (BizStep=2 OR BizStep=4) AND BizID=" + bizID); }
            else { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE (BizStep=1 OR BizStep=3) AND BizID=" + bizID); }

            if (DbHelperSQL.ExecuteSqlTran(list) > 0) { }
            list = null;
        }
        /// <summary>
        /// Ͻ�����Զ��������̱� ����
        /// </summary>
        /// <param name="sex">0:�У�1:Ů</param>
        public static void UpdateBizWorkFlows(string bizID, string bizCode, string sex, string DocsIDIs)
        {
            DocsIDIs = DocsIDIs.Substring(0, DocsIDIs.Length - 1);
            string DocsPath = CommPage.GetSingleVal("SELECT DocsPath FROM BIZ_Docs WHERE CommID=" + DocsIDIs);
            string Comments = "<a href=\"" + m_SvrUrl + DocsPath + "\" rel=\"lightbox[zj]\">���ύ֤������</a>";
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            if (bizCode == "0101")
            {
                if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=2 AND BizID=" + bizID); }
                if (sex == "1") { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=1 AND BizID=" + bizID); }
            }
            else if (bizCode == "0102" || bizCode == "0107" || bizCode == "0108" || bizCode == "0122")
            {
                if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=2 AND BizID=" + bizID); }
                else { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=1) AND BizID=" + bizID); }
            }
            else if (bizCode == "0106" || bizCode == "0109")
            {
                if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=1 AND BizID=" + bizID); }
                else { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=2 AND BizID=" + bizID); }
            }
            else
            { }
            if (DbHelperSQL.ExecuteSqlTran(list) > 0) { }
            list = null;
        }
        /// <summary>
        /// Ͻ�����Զ��������̱� ����
        /// </summary>
        /// <param name="sex">0:�У�1:Ů</param>
        public static void UpdateBizWorkFlowsWap(string bizID, string sex, string docsPath)
        {
            string Comments = "<a href=\"" + m_SvrUrl + docsPath + "\" rel=\"lightbox[zj]\">���ύ֤������</a>";
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE (BizStep=2 OR BizStep=4) AND BizID=" + bizID); }
            else { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE (BizStep=1 OR BizStep=3) AND BizID=" + bizID); }

            if (DbHelperSQL.ExecuteSqlTran(list) > 0) { }
            list = null;
        }
        /// <summary>
        /// Ͻ�����Զ��������̱� ����
        /// </summary>
        /// <param name="sex">0:�У�1:Ů</param>
        public static void UpdateBizWorkFlowsWap(string bizID, string bizCode, string sex, string docsPath)
        {
            string Comments = "<a href=\"" + m_SvrUrl + docsPath + "\" rel=\"lightbox[zj]\">���ύ֤������</a>";
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            if (bizCode == "0101")
            {
                if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=2 AND BizID=" + bizID); }
                if (sex == "1") { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=1 AND BizID=" + bizID); }
            }
            else if (bizCode == "0102" || bizCode == "0107" || bizCode == "0108" || bizCode == "0122")
            {
                if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=2 AND BizID=" + bizID); }
                else { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=1) AND BizID=" + bizID); }
            }
            else if (bizCode == "0106" || bizCode == "0109")
            {
                if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=1 AND BizID=" + bizID); }
                else { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE BizStep=2 AND BizID=" + bizID); }
            }
            else
            {
                if (sex == "0") { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE (BizStep=2 OR BizStep=4) AND BizID=" + bizID); }
                else { list.Add("UPDATE BIZ_WorkFlows SET Approval='��ؼ�������',Comments='" + Comments + "',IsInnerArea=1,Attribs=1,OprateDate=getdate() WHERE (BizStep=1 OR BizStep=3) AND BizID=" + bizID); }
            }
            if (DbHelperSQL.ExecuteSqlTran(list) > 0) { }
            list = null;
        }
        #endregion
    }
}
