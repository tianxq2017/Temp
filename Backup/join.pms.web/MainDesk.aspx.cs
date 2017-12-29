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

using System.Text;
using System.Data.SqlClient;
using System.Globalization;

using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.web
{
    public partial class MainDesk : System.Web.UI.Page
    {
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_UserRoleID;
        private string m_UserDeptCode;
        private string m_UserDeptArea;
        public string none1, none2, none3 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            m_UserRoleID = DbHelperSQL.GetSingle("SELECT TOP 1 RoleID FROM SYS_UserRoles WHERE UserID=" + m_UserID).ToString();
            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                if (m_UserRoleID == "1")
                {
                    GetBizInfo();//ҵ���������
                    GetNHSInfo1();//��ŮԤ����Ϣ
                    GetNHSInfo2();//��ͯԤ����Ϣ
                    GetPublicCms();
                    GetTJdata2();
                    GetStats();
                }
                if (int.Parse(m_UserRoleID) >= 2 && int.Parse(m_UserRoleID) <= 6)
                {
                    GetBizInfo();//ҵ���������
                    GetTJdata2();
                    GetPublicCms();
                    GetStats();
                }
                if ((int.Parse(m_UserRoleID) >= 7 && int.Parse(m_UserRoleID) <= 11) || int.Parse(m_UserRoleID) == 16)
                {
                    //�����ɫ  //��ͯ���߽�ɫ
                    GetPublicCms();
                    GetTJdata2();
                    GetStats();
                }
                if (int.Parse(m_UserRoleID) == 14)
                {
                    //�ؼ����ױ�����ɫ
                    GetBizInfo();//ҵ���������
                    GetNHSInfo1();//��ŮԤ����Ϣ
                    GetNHSInfo2();//��ͯԤ����Ϣ
                    none1 = "none";
                    none2 = "none";
                    none3 = "none";
                }
                if (int.Parse(m_UserRoleID) == 15)
                {
                    //�򼶸��ױ�����ɫ
                    GetNHSInfo1();//��ŮԤ����Ϣ
                    GetNHSInfo2();//��ͯԤ����Ϣ
                    none1 = "none";
                    none2 = "none";
                    none3 = "none";
                }
                //GetTJdata();
            }

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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }


        private void SetPageStyle(string userID)
        {
            try
            {
                //string cssFile = DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                //if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";
                string cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        private void GetPublicCms()
        {
            StringBuilder sHtml = new StringBuilder();
            sHtml.Append("<div class=\"part_a\">");
            sHtml.Append("<div class=\"part_12_02a\">" + GetCmsInfo("0401", "֪ͨ����", "", "") + "</div>");
            sHtml.Append("</div>");
            sHtml.Append("<div class=\"part_a part_b\">");
            sHtml.Append("<div class=\"part_12_02a\">" + GetCmsInfo("0402", "���߷���", "", "") + "</div>");
            sHtml.Append("</div>");
            sHtml.Append("<div class=\"part_a part_c\">");
            sHtml.Append("<div class=\"part_12_02a\">" + GetCmsInfo("0403", "�����Ϣ", "", "") + "</div>");
            sHtml.Append("</div>");
            sHtml.Append("<div class=\"clr\"></div>");
            this.LiteralCms.Text = sHtml.ToString();

        }

        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="cmsCode"></param>
        /// <param name="cmsName"></param>
        private string GetCmsInfo(string cmsCode, string cmsName, string ctrlName, string cssStyle)
        {
            string objID = string.Empty;
            string objTitle = string.Empty, objName = "";
            string objDate = string.Empty;
            string navUrl = string.Empty, sqlParams = string.Empty;
            StringBuilder sHtml = new StringBuilder();
            SqlDataReader sdr = null;

            try
            {
                //sqlParams = "SELECT TOP 5 CmsID,CmsTitle,OprateDate,DeptName FROM v_CmsList WHERE CmsCode LIKE '" + cmsCode + "%' AND CmsAttrib=9 ORDER BY OprateDate DESC";
                sqlParams = "SELECT TOP 5 CmsID,CmsTitle FROM v_CmsList WHERE CmsCode LIKE '" + cmsCode + "%' AND CmsAttrib IN(1,9) ORDER BY CmsID DESC";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                sHtml.Append("<div class=\"part_title\"><span class=\"more\"><a href=\"UnvCommList.aspx?&FuncCode=" + cmsCode + "&FuncNa=" + Server.UrlEncode(cmsName) + "\">����>></a></span><p>" + cmsName + "</p></div>");
                sHtml.Append("<div class=\"list\">");
                sHtml.Append("<ul>");
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        objID = sdr["CmsID"].ToString();
                        objTitle = sdr["CmsTitle"].ToString();
                        //objDate = sdr["OprateDate"].ToString();
                        //objName = sdr["DeptName"].ToString();
                        sHtml.Append("<li><a href=\"CmsView.aspx?RID=" + objID + "\">" + objTitle + "</a></li>");
                    }
                }
                else
                {
                    sHtml.Append("<li>û�й�������������</li>");
                }

                sdr.Close();
                sHtml.Append("</ul>");
                sHtml.Append("</div>");
            }
            catch
            {
                if (sdr != null) sdr.Close();
            }
            return sHtml.ToString();
        }
        private void GetTJdata2()
        {
            string sqlParams = string.Empty;
            //StringBuilder sHtml = new StringBuilder();
            //sHtml.Append("<div class=\"part_t\">2016��8��1�������Ѱ���ҵ��</div>");
            //sHtml.Append("<div class=\"part_c\">");
            //sHtml.Append("<ul>");
            //sHtml.Append("<li>��ԤԼ��<b>" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents ").ToString() + "</b>��</li>");
            //sHtml.Append("<li>�Ѱ�᣺<b>" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE  Attribs IN(2,8,9) ").ToString() + "</b>��</li>");
            //sHtml.Append("<li>��ĸ�ӽ����ֲᡷ��<b>" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE BizCode='0150' ").ToString() + "</b>��</li>");
            //sHtml.Append("</ul>");
            //sHtml.Append("</div>");
            //sHtml.Append("<div class=\"part_b\">");
            //sHtml.Append("<p>");
            //sHtml.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"4\">");
            //sHtml.Append("  <tr>");
            //sHtml.Append("    <td height=\"24\" align=\"left\" valign=\"middle\">�ǹ���" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '610922100%' OR RegAreaCodeB LIKE'610922100%' OR SelAreaCode LIKE '610922100%' OR CurAreaCodeA LIKE '610922100%' OR CurAreaCodeB LIKE '610922100%') and BizCode!='00000' ").ToString() + "</td>");
            //sHtml.Append("    <td align=\"left\" valign=\"middle\">�ķ���" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '610922101%' OR RegAreaCodeB LIKE'610922101%' OR SelAreaCode LIKE '610922101%' OR CurAreaCodeA LIKE '610922101%' OR CurAreaCodeB LIKE '610922101%') and BizCode!='00000' ").ToString() + "</td>");
            //sHtml.Append("    <td align=\"left\" valign=\"middle\">������" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '610922102%' OR RegAreaCodeB LIKE'610922102%' OR SelAreaCode LIKE '610922102%' OR CurAreaCodeA LIKE '610922102%' OR CurAreaCodeB LIKE '610922102%') and BizCode!='00000' ").ToString() + "</td>");
            //sHtml.Append("        <td align=\"left\" valign=\"middle\">ӭ����" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '610922103%' OR RegAreaCodeB LIKE'610922103%' OR SelAreaCode LIKE '610922103%' OR CurAreaCodeA LIKE '610922103%' OR CurAreaCodeB LIKE '610922103%') and BizCode!='00000' ").ToString() + "</td>");
            //sHtml.Append("  </tr>");
            //sHtml.Append("  <tr>");
            //sHtml.Append("   <td height=\"24\"  align=\"left\" valign=\"middle\">�غ���" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '610922104%' OR RegAreaCodeB LIKE'610922104%' OR SelAreaCode LIKE '610922104%' OR CurAreaCodeA LIKE '610922104%' OR CurAreaCodeB LIKE '610922104%') and BizCode!='00000' ").ToString() + "</td>");
            //sHtml.Append("   <td align=\"left\" valign=\"middle\">������" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '610922105%' OR RegAreaCodeB LIKE'610922105%' OR SelAreaCode LIKE '610922105%' OR CurAreaCodeA LIKE '610922105%' OR CurAreaCodeB LIKE '610922105%') and BizCode!='00000' ").ToString() + "</td>");
            //sHtml.Append("   <td align=\"left\" valign=\"middle\">ϲ����" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '610922106%' OR RegAreaCodeB LIKE'610922106%' OR SelAreaCode LIKE '610922106%' OR CurAreaCodeA LIKE '610922106%' OR CurAreaCodeB LIKE '610922106%') and BizCode!='00000' ").ToString() + "</td>");
            //sHtml.Append("   <td align=\"left\" valign=\"middle\">�ٶ���" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '610922107%' OR RegAreaCodeB LIKE'610922107%' OR SelAreaCode LIKE '610922107%' OR CurAreaCodeA LIKE '610922107%' OR CurAreaCodeB LIKE '610922107%') and BizCode!='00000' ").ToString() + "</td>");
            // sHtml.Append(" </tr>");
            // sHtml.Append(" <tr>");
            // sHtml.Append("   <td height=\"24\"  align=\"left\" valign=\"middle\">��Ϫ��" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '610922109%' OR RegAreaCodeB LIKE'610922109%' OR SelAreaCode LIKE '610922109%' OR CurAreaCodeA LIKE '610922109%' OR CurAreaCodeB LIKE '610922109%') and BizCode!='00000' ").ToString() + "</td>");
            // sHtml.Append("   <td align=\"left\" valign=\"middle\">�г���" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '610922110%' OR RegAreaCodeB LIKE'610922110%' OR SelAreaCode LIKE '610922110%' OR CurAreaCodeA LIKE '610922110%' OR CurAreaCodeB LIKE '610922110%') and BizCode!='00000' ").ToString() + "</td>");
            // sHtml.Append("   <td align=\"left\" valign=\"middle\">����ɽ��" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '610922108%' OR RegAreaCodeB LIKE'610922108%' OR SelAreaCode LIKE '610922108%' OR CurAreaCodeA LIKE '610922108%' OR CurAreaCodeB LIKE '610922108%') and BizCode!='00000' ").ToString() + "</td>");
            // sHtml.Append("   <td align=\"left\" valign=\"middle\"></td>");
            // sHtml.Append(" </tr>");
            //sHtml.Append("</table>");
            //sHtml.Append("</p>");
            //sHtml.Append("</div>");
            //this.LiteralTJdata.Text = sHtml.ToString();
            //sHtml = null;

            string Arestr = "ϵͳ";
            string Codestr = "150500";//����
            if ((m_UserRoleID == "2" || m_UserRoleID == "3"))
            {
                //Codestr = m_UserDeptArea.Substring(0, 6);
            }
            else if (m_UserRoleID == "4")
            {
                Arestr = "�����";
                Codestr = m_UserDeptArea.Substring(0, 9);//��
            }
            else if (m_UserRoleID == "5")
            {
                Arestr = "��Ͻ��";
                Codestr = m_UserDeptArea;//��
            }
            this.LiteraAre.Text = Arestr;


            this.litBJ015001.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0150' and  Attribs IN(2,8,9) ").ToString();
            this.litBJ015002.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0150' ").ToString();

            this.litBJ010101.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0101' and  Attribs IN(2,8,9) ").ToString();
            this.litBJ010102.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0101' ").ToString();
            this.litBJ010201.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0102' and  Attribs IN(2,8,9) ").ToString();
            this.litBJ010202.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0102' ").ToString();
            this.litBJ010301.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0103' and  Attribs IN(2,8,9) ").ToString();
            this.litBJ010302.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0103' ").ToString();
            this.litBJ010401.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0104' and  Attribs IN(2,8,9) ").ToString();
            this.litBJ010402.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0104' ").ToString();
            this.litBJ010501.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0105' and  Attribs IN(2,8,9) ").ToString();
            this.litBJ010502.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0105' ").ToString();
            this.litBJ010601.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0106' and  Attribs IN(2,8,9) ").ToString();
            this.litBJ010602.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0106' ").ToString();
            this.litBJ010701.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0107' and  Attribs IN(2,8,9) ").ToString();
            this.litBJ010702.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0107' ").ToString();


            this.litBJ010801.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0108' and  Attribs IN(2,8,9) ").ToString();
            this.litBJ010802.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0108' ").ToString();


            this.litBJ010901.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0109' and  Attribs IN(2,8,9) ").ToString();
            this.litBJ010902.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0109' ").ToString();

            this.litBJ011001.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0110' and  Attribs IN(2,8,9) ").ToString();
            this.litBJ011002.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0110' ").ToString();


            this.litBJ011101.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0111' and  Attribs IN(2,8,9) ").ToString();
            this.litBJ011102.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0111' ").ToString();


            this.litBJ012201.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0122'  and  Attribs IN(2,8,9) ").ToString();
            this.litBJ012202.Text = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE (RegAreaCodeA LIKE '" + Codestr + "%' OR RegAreaCodeB LIKE'" + Codestr + "%' OR SelAreaCode LIKE '" + Codestr + "%' OR CurAreaCodeA LIKE '" + Codestr + "%' OR CurAreaCodeB LIKE '" + Codestr + "%') and BizCode='0122' ").ToString();

          



            //StringBuilder sHtml2 = new StringBuilder();
            //sHtml2.Append("<div class=\"part_t\">ҵ�񱨱�</div>");
            //sHtml2.Append("<div class=\"part_c\">");
            //sHtml2.Append("<ul>");
            //sHtml2.Append("<li class=\"a1\"><a href=\"/MainLeft.aspx?c=03\" target=\"leftFrame\" ><span>�˿ڼ�������</span><i>" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM PIS_BaseInfo WHERE FuncNo LIKE '0301%' ").ToString() + "</i></a></li>");
            //sHtml2.Append("<li class=\"a1\"><a href=\"/MainLeft.aspx?c=03\" target=\"leftFrame\" ><span>������������</span><i>" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM PIS_BaseInfo WHERE FuncNo LIKE '0302%' ").ToString() + "</i></a></li>");
            //sHtml2.Append("<li class=\"a1\"><a href=\"/MainLeft.aspx?c=03\" target=\"leftFrame\" ><span>���������</span><i>" + DbHelperSQL.GetSingle("SELECT COUNT(0) FROM PIS_BaseInfo WHERE FuncNo LIKE '0303%' ").ToString() + "</i></a></li>");
            //sHtml2.Append("</ul>");
            //sHtml2.Append("</div>");
            //this.LiteralBaobiao.Text = sHtml2.ToString();
            //sHtml2 = null;
        }
        private void GetTJdata()
        {
            string sqlParams = string.Empty;
            StringBuilder sHtml = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                sqlParams = "SELECT TOP 1 FILEDS01,FILEDS02,FILEDS03,FILEDS04,FILEDS05,FILEDS06,FILEDS07,FILEDS08,FILEDS09,FILEDS10 FROM PIS_BaseInfo WHERE FuncNo = '01010601' ORDER BY FILEDS01 DESC, CommID DESC";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        sHtml.Append("<div class=\"part_t\">��" + sdr["FILEDS01"].ToString() + "��ͳ������</div>");
                        sHtml.Append("<div class=\"part_c\">");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>�ܻ�����<b>" + sdr["FILEDS02"].ToString() + "</b></li>");
                        sHtml.Append("<li>���˿ڣ�<b>" + sdr["FILEDS03"].ToString() + "</b></li>");
                        sHtml.Append("<li>Ů���˿ڣ�<b>" + sdr["FILEDS04"].ToString() + "</b></li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        sHtml.Append("<div class=\"part_b\">");
                        sHtml.Append("<p>");
                        sHtml.Append("<span>ũҵ�˿ڣ�" + sdr["FILEDS05"].ToString() + "</span>");
                        sHtml.Append("<span>�����˿ڣ�" + sdr["FILEDS06"].ToString() + "</span>");
                        sHtml.Append("<span>��ҵ��Ա��" + sdr["FILEDS07"].ToString() + "</span><br />");
                        sHtml.Append("<span>�ڸ�ְ����" + sdr["FILEDS08"].ToString() + "</span>");
                        sHtml.Append("<span>ƽ�����ʣ�" + sdr["FILEDS09"].ToString() + "</span>");
                        sHtml.Append("<span>������ֵGDP��" + sdr["FILEDS10"].ToString() + "</span>");
                        sHtml.Append("</p>");
                        sHtml.Append("</div>");

                        //objID = sdr["CmsID"].ToString();
                    }
                }
                else
                {
                    sHtml.Append("<div class=\"part_t\">ͳ������</div>");
                    sHtml.Append("<div class=\"part_c\">");
                    sHtml.Append("<ul>");
                    sHtml.Append("<li>�������ݣ�<b>0</b></li>");
                    sHtml.Append("<li>�������ݣ�<b>0</b></li>");
                    sHtml.Append("<li>��׼���ݣ�<b>0</b></li>");
                    sHtml.Append("</ul>");
                    sHtml.Append("</div>");
                    sHtml.Append("<div class=\"part_b\">");
                    sHtml.Append("<p>");
                    sHtml.Append("<span>�����ƾ֡�</span>");
                    sHtml.Append("<span>�������֡�</span>");
                    sHtml.Append("<span>�������֡�</span><br />");
                    sHtml.Append("<span>������֡�</span>");
                    sHtml.Append("<span>������֡�</span>");
                    sHtml.Append("<span>��ͳ�ƾ֡�</span>");
                    sHtml.Append("</p>");
                    sHtml.Append("</div>");
                }

                sdr.Close();
            }
            catch
            {
                sHtml.Append("<div class=\"part_t\">ͳ������</div>");
                sHtml.Append("<div class=\"part_c\">");
                sHtml.Append("<ul>");
                sHtml.Append("<li>�������ݣ�<b>0</b></li>");
                sHtml.Append("<li>�������ݣ�<b>0</b></li>");
                sHtml.Append("<li>��׼���ݣ�<b>0</b></li>");
                sHtml.Append("</ul>");
                sHtml.Append("</div>");
                sHtml.Append("<div class=\"part_b\">");
                sHtml.Append("<p>");
                sHtml.Append("<span>�����ƾ֡�</span>");
                sHtml.Append("<span>�������֡�</span>");
                sHtml.Append("<span>�������֡�</span><br />");
                sHtml.Append("<span>������֡�</span>");
                sHtml.Append("<span>������֡�</span>");
                sHtml.Append("<span>��ͳ�ƾ֡�</span>");
                sHtml.Append("</p>");
                sHtml.Append("</div>");
                if (sdr != null) sdr.Close();
            }
            //this.LiteralTJdata.Text = sHtml.ToString();
        }


        /// <summary>
        /// ��ȡ����ҵ����Ϣ
        /// </summary>
        private void GetBizInfo()
        {
            int nums = 0;
            string bizID = "", bizCode = "", bizName = "", InitDirectionCN = "", Fileds01 = "", Fileds08 = "", AttribsCN = "-", bizDate = string.Empty;
            string sourceUrl = "", viewLink = "", auditLink = string.Empty;
            string bizCount = "0";
            string sqlParams = string.Empty;
            StringBuilder s = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                m_UserDeptCode = join.pms.dal.CommPage.GetUnitCodeByUser(m_UserID, ref m_UserDeptArea);
                string filterSQL = GetBizFilterByUser();
                // CmsAttrib:0 Ĭ��;1 ���; 3 ����; 4 ɾ��; 9 ��
                // Attribs: 0��ʼ�ύ; 1�����; 2,ͨ�� 3����; 4ɾ��; 9,�鵵

                if (m_UserRoleID == "14")
                {
                    filterSQL = filterSQL + " AND  BizCode='0131' ";
                }
                sqlParams = "SELECT COUNT(*) FROM v_BizList WHERE " + filterSQL + " ";
                bizCount = join.pms.dal.CommPage.GetSingleValue(sqlParams);
                sqlParams = "SELECT TOP 6 BizID,BizCode,BizName,Fileds01,Fileds08,Initiator,CurrentStepNa,StartDate,AttribsCN,InitDirectionCN FROM v_BizList WHERE " + filterSQL + " ORDER BY StartDate desc";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                //��������:bizCount
                //s.Append("<div class=\"index_t\"><img src=\"/images/2013_12/ico_15.gif\" /></div>");
                s.Append("<div class=\"index_b\">");
                s.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                s.Append("<tr>");
                s.Append("    <th>ҵ����</th>");
                s.Append("    <th>ҵ������</th>");
                s.Append("    <th>��Դ</th>");
                s.Append("    <th>������</th>");
                s.Append("    <th>����ʱ��</th>");
                s.Append("    <th>��ǰ״̬</th>");
                s.Append("     <th>����</th>");
                s.Append("</tr>");
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        nums += 1;
                        bizID = sdr["BizID"].ToString();
                        bizCode = sdr["BizCode"].ToString();
                        bizName = sdr["BizName"].ToString();
                        bizDate = sdr["StartDate"].ToString();
                        Fileds01 = sdr["Fileds01"].ToString();
                        Fileds08 = sdr["Fileds08"].ToString();
                        if (!string.IsNullOrEmpty(Fileds08)) { Fileds01 += "," + Fileds08; }
                        InitDirectionCN = sdr["InitDirectionCN"].ToString();
                        AttribsCN = sdr["AttribsCN"].ToString();
                        if (!string.IsNullOrEmpty(bizDate)) bizDate = DateTime.Parse(bizDate).ToString("yyyy-MM-dd");
                        sourceUrl = "pSearch=&UserID=" + m_UserID + "&FuncUser=&FuncCode=" + bizCode + "&FuncNa=" + Server.UrlEncode(bizName) + "&p=1";
                        viewLink = GetBizLinkByFuncNo(bizCode, ref auditLink);
                        s.Append("<tr>");
                        s.Append("    <td>" + bizID + "</td>");
                        s.Append("    <td><a href=\"" + viewLink + "&k=" + bizID + "&sourceUrl=" + DESEncrypt.Encrypt(sourceUrl) + "\">" + bizName + "</a></td>");
                        s.Append("    <td>" + InitDirectionCN + "</td>");
                        s.Append("    <td>" + Fileds01 + "</td>");
                        s.Append("    <td>" + bizDate + "</td>");
                        s.Append("    <td>" + AttribsCN + "</td>");
                        s.Append("    <td><a href=\"" + viewLink + "&k=" + bizID + "&sourceUrl=" + DESEncrypt.Encrypt(sourceUrl) + "\">�鿴</a></td>");
                        s.Append("</tr>");
                        if (m_UserRoleID != "1")
                        {
                            //s.Append("<a href=\"" + auditLink + "&k=" + bizID + "&sourceUrl=" + DESSecurity.Encrypt(sourceUrl) + "\" >���</a>");
                        }
                    }
                }
                else
                {
                    s.Append("<!--tr><td>����</td></tr-->");
                }
            }
            catch
            {
                if (sdr != null) sdr.Close();
            }
            if (nums == 0) { s.Append("<tr><td>����</td><td></td><td></td><td></td><td></td><td></td><td></td></tr>"); }
            s.Append("</table>");
            s.Append("</div>");

            this.LiteralBiz.Text = "<div class=\"block_index\"><div class=\"index_05\"><div class=\"index_t\"><span>���� <b><font color=red>" + bizCount + "</font></b> �����������账����� <b><font color=red>" + nums + "</font></b> ��</span><b>ҵ�����</b></div>" + s.ToString() + "</div><div class=\"clr15\"></div></div>";
        }

        /// <summary>
        /// ��ŮԤ����Ϣ
        /// </summary>
        private void GetNHSInfo1()
        {
            int nums = 0;
            string UnvID = "", FnName = "", FnCID = "", FnTel = "", FnAddress = "", LastVisitItems = "", LastVisitDate = "";
            string sourceUrl = "", viewLink = "", auditLink = string.Empty;
            string bizCount = "0";
            string sqlParams = string.Empty;
            StringBuilder s = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                m_UserDeptCode = join.pms.dal.CommPage.GetUnitCodeByUser(m_UserID, ref m_UserDeptArea);
                string filterSQL = " 0=0 ";
                if (m_UserDeptArea.Substring(6) == "000000")
                {
                    filterSQL = "  YCFUserAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%'  ";
                }
                else if (m_UserDeptArea.Substring(9) == "000")
                {
                    filterSQL = "  YCFUserAreaCode LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' ";
                }
                else
                {
                    filterSQL = "  YCFUserAreaCode = '" + m_UserDeptArea + "' ";
                }
                filterSQL = filterSQL + "AND LastVisitItems is not NULL AND  LastVisitDate is not NULL AND  LastVisitDate >='" + DateTime.Now.AddDays(-7).Date.ToString("yyyy-MM-dd") + " 00:00:00' AND LastVisitDate<='" + DateTime.Now.AddDays(10).Date.ToString("yyyy-MM-dd") + " 23:59:59' and unvid not in(select a.UnvID from NHS_YCF_CQFS_2_13 a inner join NHS_YCF_CHFS b on a.unvid=b.unvid where  a.VisitItems=13  and b.VisitItems=542)";
                sqlParams = "SELECT COUNT(*) FROM v_NHS_YCFList WHERE " + filterSQL;
                bizCount = join.pms.dal.CommPage.GetSingleValue(sqlParams);
                sqlParams = "SELECT TOP 6 * FROM v_NHS_YCFList WHERE " + filterSQL + "  ORDER BY LastVisitDate asc ";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                //��������:bizCount
                //s.Append("<div class=\"index_t\"><img src=\"/images/2013_12/ico_15.gif\" /></div>");
                s.Append("<div class=\"index_b\">");
                s.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                if (sdr.HasRows)
                {
                    s.Append("<tr>");
                    s.Append("    <th>�в�������</th>");
                    s.Append("    <th>���֤��</th>");
                    s.Append("    <th>�绰</th>");
                    s.Append("    <th>��ַ</th>");
                    s.Append("    <th>��ǰ���Ԥ��</th>");
                    s.Append("    <th>�������Ԥ��</th>");
                    s.Append("    <th>����������</th>");
                    s.Append("    <th>����������</th>");
                    s.Append("</tr>");
                    while (sdr.Read())
                    {
                        nums += 1;
                        UnvID = sdr["UnvID"].ToString();
                        FnName = sdr["FnName"].ToString();
                        FnCID = sdr["FnCID"].ToString();
                        FnTel = sdr["FnTel"].ToString();
                        FnAddress = sdr["FnAddress"].ToString().Replace("����ʡ������ʯȪ��", "");
                        LastVisitItems = sdr["LastVisitItems2"].ToString();
                        if (!string.IsNullOrEmpty(sdr["LastVisitDate"].ToString())) LastVisitDate = DateTime.Parse(sdr["LastVisitDate"].ToString()).ToString("yyyy-MM-dd");
                        s.Append("<tr>");
                        s.Append("    <td>" + FnName + "</td>");
                        s.Append("    <td>" + FnCID + "</td>");
                        s.Append("    <td>" + FnTel + "</td>");
                        s.Append("    <td>" + FnAddress + "</td>");


                        //CQNextVisitDate, 
                        //CQVisitCount AS COUNT01,
                        //CHNextVisitDate
                        //CHVisitCount AS COUNT02, 
                        //dbo.NHS_YCF_Base.FMVCount, dbo.NHS_YCF_Base.QcfwzBm, 
                        //dbo.NHS_YCF_Base.LastVisitItems, dbo.NHS_YCF_Base.LastVisitDate
                        string alt = "--";
                        DateTime t1 = DateTime.Now;
                        if (string.IsNullOrEmpty(sdr["COUNT02"].ToString()) || sdr["COUNT02"].ToString() == "0")
                        {
                            DateTime t2 = Convert.ToDateTime(sdr["CQNextVisitDate"].ToString());
                            TimeSpan ts = t2 - t1;
                            int d = ts.Days;
                            if (d == 0) { alt = "���������1��"; }
                            else if (d > 0) { alt = "����" + d + "��"; }
                            else if (d < 0) { alt = "�ѹ���" + -d + "��"; }
                            s.Append("    <td style=\"color:red\">" + alt + "</td>");
                            s.Append("    <td>--</td>");
                        }
                        else
                        {
                            DateTime t2 = Convert.ToDateTime(sdr["CHNextVisitDate"].ToString());
                            TimeSpan ts = t2 - t1;
                            int d = ts.Days;
                            if (d == 0) { alt = "���������1��"; }
                            else if (d > 0) { alt = "����" + d + "��"; }
                            else if (d < 0) { alt = "�ѹ���" + -d + "��"; }
                            s.Append("    <td>--</td>");
                            s.Append("    <td style=\"color:red\">" + alt + "</td>");
                        }

                        s.Append("    <td>" + LastVisitItems + "</td>");
                        s.Append("    <td>" + LastVisitDate + "</td>");
                        s.Append("</tr>");
                    }
                }
                else
                {
                    s.Append("<tr><td>����</td></tr>");
                }
                s.Append("</table>");
                s.Append("</div>");
            }
            catch
            {
                if (sdr != null) sdr.Close();
            }

            this.LiteralNHS1.Text = "<div class=\"block_index\"><div class=\"index_05\"><div class=\"index_t\"><span>��ŮԤ������ <b><font color=red>" + bizCount + "</font></b> ��������Ԥ���� <b><font color=red>" + nums + "</font></b> ��</span><b>��ŮԤ����Ϣ</b></div>" + s.ToString() + "</div><div class=\"clr15\"></div></div>";
        }
        /// <summary>
        /// ��ͯԤ����Ϣ
        /// </summary>
        private void GetNHSInfo2()
        {
            int nums = 0;
            string UnvID = "", FnName = "", FnCID = "", FnTel = "", FnAddress = "", LastVisitItems = "", LastVisitDate = "";
            string sourceUrl = "", viewLink = "", auditLink = string.Empty;
            string bizCount = "0";
            string sqlParams = string.Empty;
            StringBuilder s = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                m_UserDeptCode = join.pms.dal.CommPage.GetUnitCodeByUser(m_UserID, ref m_UserDeptArea);
                string filterSQL = " 0=0 ";
                if (m_UserDeptArea.Substring(6) == "000000")
                {
                    filterSQL = "  ChildUserAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%'  ";
                }
                else if (m_UserDeptArea.Substring(9) == "000")
                {
                    filterSQL = "  ChildUserAreaCode LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' ";
                }
                else
                {
                    filterSQL = "  ChildUserAreaCode = '" + m_UserDeptArea + "' ";
                }
                filterSQL = filterSQL + "AND LastVisitItems is not NULL AND  LastVisitDate is not NULL AND  LastVisitDate >='" + DateTime.Now.AddDays(-7).Date.ToString("yyyy-MM-dd") + " 00:00:00' AND LastVisitDate<='" + DateTime.Now.AddDays(10).Date.ToString("yyyy-MM-dd") + " 23:59:59' ";
                sqlParams = "SELECT COUNT(*) FROM v_NHS_Child_Base WHERE " + filterSQL;
                bizCount = join.pms.dal.CommPage.GetSingleValue(sqlParams);
                sqlParams = "SELECT TOP 6 * FROM v_NHS_Child_Base WHERE " + filterSQL + "  ORDER BY NextVisitDate asc ";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                //��������:bizCount
                //s.Append("<div class=\"index_t\"><img src=\"/images/2013_12/ico_15.gif\" /></div>");
                s.Append("<div class=\"index_b\">");
                s.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                if (sdr.HasRows)
                {
                    s.Append("<tr>");
                    s.Append("    <th>�в�������</th>");
                    s.Append("    <th>���֤��</th>");
                    s.Append("    <th>�绰</th>");
                    s.Append("    <th>��ַ</th>");
                    s.Append("    <th>��ͯ����</th>");
                    s.Append("    <th>�������Ԥ��</th>");
                    s.Append("    <th>����������</th>");
                    s.Append("    <th>����������</th>");
                    s.Append("</tr>");
                    while (sdr.Read())
                    {
                        nums += 1;
                        UnvID = sdr["UnvID"].ToString();
                        FnName = sdr["MotherName"].ToString();
                        FnCID = sdr["MotherCID"].ToString();
                        FnTel = sdr["MotherTel"].ToString();
                        FnAddress = sdr["FnAddress"].ToString().Replace("����ʡ������ʯȪ��", "");
                        LastVisitItems = sdr["LastVisitItems2"].ToString();
                        if (!string.IsNullOrEmpty(sdr["LastVisitDate"].ToString())) LastVisitDate = DateTime.Parse(sdr["LastVisitDate"].ToString()).ToString("yyyy-MM-dd");
                        s.Append("<tr>");
                        s.Append("    <td>" + FnName + "</td>");
                        s.Append("    <td>" + FnCID + "</td>");
                        s.Append("    <td>" + FnTel + "</td>");
                        s.Append("    <td>" + FnAddress + "</td>");
                        s.Append("    <td>" + sdr["ChildName"].ToString() + "</td>");
                        //CQNextVisitDate, 
                        //CQVisitCount AS COUNT01,
                        //CHNextVisitDate
                        //CHVisitCount AS COUNT02, 
                        //dbo.NHS_YCF_Base.FMVCount, dbo.NHS_YCF_Base.QcfwzBm, 
                        //dbo.NHS_YCF_Base.LastVisitItems, dbo.NHS_YCF_Base.LastVisitDate
                        string alt = "--";
                        DateTime t1 = DateTime.Now;
                        if (!string.IsNullOrEmpty(sdr["COUNT01"].ToString()))
                        {
                            DateTime t2 = Convert.ToDateTime(sdr["NextVisitDate"].ToString());
                            TimeSpan ts = t2 - t1;
                            int d = ts.Days;
                            if (d == 0) { alt = "���������1��"; }
                            else if (d > 0) { alt = "����" + d + "��"; }
                            else if (d < 0) { alt = "�ѹ���" + -d + "��"; }
                            s.Append("    <td style=\"color:red\">" + alt + "</td>");
                        }
                        else
                        {
                            s.Append("    <td>--</td>");
                        }
                        s.Append("    <td>" + LastVisitItems + "</td>");
                        s.Append("    <td>" + LastVisitDate + "</td>");
                        s.Append("</tr>");
                    }
                }
                else
                {
                    s.Append("<tr><td>����</td></tr>");
                }
                s.Append("</table>");
                s.Append("</div>");
            }
            catch
            {
                if (sdr != null) sdr.Close();
            }

            this.LiteralNHS2.Text = "<div class=\"block_index\"><div class=\"index_05\"><div class=\"index_t\"><span>��ͯԤ������ <b><font color=red>" + bizCount + "</font></b> ��������Ԥ���� <b><font color=red>" + nums + "</font></b> ��</span><b>��ͯԤ����Ϣ</b></div>" + s.ToString() + "</div><div class=\"clr15\"></div></div>";
        }
        // SELECT DISTINCT BizID FROM BIZ_WorkFlows WHERE AreaCode
        /*
1	ϵͳ����Ա
2	ҵ�����-����
3	ҵ����-����
4	ҵ����-���
5	ҵ����-����/��
6	ҵ����-ҽԺ
        */
        private string GetBizFilterByUser()
        {
            string returnVa = string.Empty;
            if (m_UserRoleID == "1") { returnVa = " Attribs IN(0,1,6) "; }
            else
            {
                if (m_UserDeptArea.Substring(6) == "000000")
                {
                    returnVa = "  BizID IN(SELECT DISTINCT BizID FROM BIZ_WorkFlows WHERE AreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "___000') AND Attribs IN(0,1,6) ";
                }
                else if (m_UserDeptArea.Substring(9) == "000")
                {
                    returnVa = "  BizID IN(SELECT DISTINCT BizID FROM BIZ_WorkFlows WHERE AreaCode LIKE '" + m_UserDeptArea.Substring(0, 9) + "___') AND Attribs IN(0,1,6) ";
                }
                else
                {
                    returnVa = "  BizID IN(SELECT DISTINCT BizID FROM BIZ_WorkFlows WHERE AreaCode = '" + m_UserDeptArea + "') AND Attribs IN(0,1,6) ";
                }
            }
            return returnVa;
        }
        /// <summary>
        /// ��ȡҵ�������ַ
        /// </summary>
        /// <returns></returns>
        private string GetBizLinkByFuncNo(string funcNo, ref string auditLink)
        {
            string viewLink = string.Empty;
            switch (funcNo)
            {
                case "0150":
                    viewLink = "/BizInfo/BizPrt0150.aspx?action=viewDetails";
                    auditLink = "/BizInfo/BizFlowAudit.aspx?action=audit";
                    break;
                case "0101":
                    viewLink = "/BizInfo/BizPrt0101.aspx?action=viewDetails";
                    auditLink = "/BizInfo/BizFlowAudit.aspx?action=audit";
                    break;
                case "0102":
                    viewLink = "/BizInfo/BizPrt0102.aspx?action=viewDetails";
                    auditLink = "/BizInfo/BizFlowAudit.aspx?action=audit";
                    break;
                case "0103":
                    viewLink = "/BizInfo/BizPrt0103.aspx?action=viewDetails";
                    auditLink = "/BizInfo/BizFlowAudit.aspx?action=audit";
                    break;
                case "0104":
                    viewLink = "/BizInfo/BizPrt0104.aspx?action=viewDetails";
                    auditLink = "/BizInfo/BizFlowAudit.aspx?action=audit";
                    break;
                case "0105":
                    viewLink = "/BizInfo/BizPrt0105.aspx?action=viewDetails";
                    auditLink = "/BizInfo/BizFlowAudit.aspx?action=audit";
                    break;
                case "0106":
                    viewLink = "/BizInfo/BizPrt0106.aspx?action=viewDetails";
                    auditLink = "/BizInfo/BizFlowAudit.aspx?action=audit";
                    break;
                case "0107":
                    viewLink = "/BizInfo/BizPrt0107.aspx?action=viewDetails";
                    auditLink = "/BizInfo/BizFlowAudit.aspx?action=audit";
                    break;
                case "0108":
                    viewLink = "/BizInfo/BizPrt0108.aspx?action=viewDetails";
                    auditLink = "/BizInfo/BizFlowAudit.aspx?action=audit";
                    break;
                case "0109":
                    viewLink = "/BizInfo/BizPrt0109.aspx?action=viewDetails";
                    auditLink = "/BizInfo/BizFlowAudit.aspx?action=audit";
                    break;
                case "0110":
                    viewLink = "/BizInfo/BizPrt0110.aspx?action=viewDetails";
                    auditLink = "/BizInfo/BizFlowAudit.aspx?action=audit";
                    break;
                case "0111":
                    viewLink = "/BizInfo/BizPrt0111.aspx?action=viewDetails";
                    auditLink = "/BizInfo/BizFlowAudit.aspx?action=audit";
                    break;
                case "0122":
                    viewLink = "/BizInfo/BizPrt0122.aspx?action=viewDetails";
                    auditLink = "/BizInfo/BizFlowAudit.aspx?action=audit";
                    break;
                case "0131":
                    viewLink = "/BizInfo/BizPrt0131.aspx?action=viewDetails";
                    auditLink = "/BizInfo/BizFlowAudit.aspx?action=audit";
                    break;
                default: // Ĭ����ʾ��ǰ������������OprateModel ='�����޸�' OCAPP
                    viewLink = "/BizInfo/BizWorkFlows.aspx?action=2";
                    auditLink = "/BizInfo/BizFlowApp.aspx?action=audit";
                    break;
            }
            return viewLink;
        }


        protected string m_StatsAup;
        protected string m_StatsAnalysis;
        protected string m_StatsDif;
        protected string m_StatsNo;
        protected string m_CurDate;

        private void GetStats()
        {
            string sqlParams = "", funcNa = "", recNum = string.Empty;
            string funcNo = string.Empty;
            int aupNum = 0;
            StringBuilder sHtml = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                //ͳ�ƹ�������
                sqlParams = "SELECT FuncCode,FuncName,(SELECT COUNT(*) FROM PIS_BaseInfo WHERE FuncNo=A.FuncCode) As recNum FROM SYS_Function A WHERE FuncStatus=0 AND FuncCode LIKE '0101%' AND LEN(FuncCode)=8 AND TemplateID=0 ORDER BY FuncCode";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        funcNo = sdr[0].ToString();
                        funcNa = sdr[1].ToString();
                        recNum = sdr[2].ToString();

                        aupNum += StrIntTrim(recNum);
                        sHtml.Append("<span>" + funcNa + "��<b>" + recNum + "</b></span>");
                    }
                    sHtml.Append("<span>С�ƣ�<b>" + aupNum.ToString() + "</b></span><br/><br/>");
                }
                else
                {
                    sHtml.Append("<li><span><b></b></span>û�й�������������</li>");
                }
                sdr.Close();
                //ͳ�Ʋ�ѯ����
                sqlParams = "SELECT FuncCode,FuncName,(SELECT COUNT(*) FROM PIS_QYK WHERE FuncNo=A.FuncCode) As recNum FROM SYS_Function A WHERE FuncStatus=0 AND FuncCode LIKE '0101%' AND LEN(FuncCode)=8 AND TemplateID=1 ORDER BY FuncCode";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        funcNo = sdr[0].ToString();
                        funcNa = sdr[1].ToString();
                        recNum = sdr[2].ToString();

                        aupNum += StrIntTrim(recNum);
                        sHtml.Append("<span class=\"color_01\">" + funcNa + "��<b>" + recNum + "</b></span>");
                    }

                }
                else
                {
                    sHtml.Append("<li><span><b></b></span>û�й�������������</li>");
                }
                sdr.Close();
                sHtml.Append("<span>С�ƣ�<b>" + aupNum.ToString() + "</b></span>");

                //sqlParams = "SELECT COUNT(*) FROM PIS_BaseInfo WHERE OprateUserID=" + m_UserID;
                //m_StatsAup = DbHelperSQL.GetSingle(sqlParams).ToString(); // ¼������
                //sqlParams = "SELECT COUNT(*) FROM PIS_BaseInfo WHERE AnalysisFlag=1 AND OprateUserID=" + m_UserID;
                //m_StatsAnalysis = DbHelperSQL.GetSingle(sqlParams).ToString(); //�Ѿ�����
                //sqlParams = "SELECT COUNT(*) FROM PIS_BaseInfo WHERE AnalysisFlag=2 AND OprateUserID=" + m_UserID;
                //m_StatsDif = DbHelperSQL.GetSingle(sqlParams).ToString(); //��������

                //m_StatsNo = (int.Parse(m_StatsAup) - int.Parse(m_StatsAnalysis) - int.Parse(m_StatsDif)).ToString();

                //// ��ǰ������ʾ
                //string TmpStr = "������";
                //TmpStr += DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) + "��";
                //TmpStr += DateTime.Now.Month.ToString("D2", CultureInfo.InvariantCulture) + "��";
                //TmpStr += DateTime.Now.Day.ToString("D2", CultureInfo.InvariantCulture) + "��";
                //TmpStr += " " + CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                //m_CurDate = TmpStr;
            }
            catch
            {
                if (sdr != null) sdr.Close();
            }

            this.LiteralStats.Text = sHtml.ToString();
        }

        private void GetTotalStats()
        {
            /*
             * SELECT COUNT(*) FROM PIS_QYK WHERE FuncNo='0501'

SELECT COUNT(*) FROM PIS_QYK WHERE FuncNo='0501' AND Attribs IN(2,3)
             * 
             <li><p><b>���˿���Ϣ</b><span>2,215,905</span></p></li>
      <li><p><b>ȫԱ�����������Ϣ</b><span>15,905</span></p></li>
      <li><p><b>�����˿�����Ǽ���Ϣ</b><span>5,905</span></p></li>
             */
            int pTotal = 0;
            int pPass = 0;
            int pUnPass = 0;

            pTotal = int.Parse(DbHelperSQL.GetSingle("SELECT COUNT(*) FROM PIS_QYK WHERE FuncNo='01010101'").ToString());
            pUnPass = int.Parse(DbHelperSQL.GetSingle("SELECT COUNT(*) FROM PIS_QYK WHERE FuncNo='01010101' AND Attribs IN(2,3)").ToString());
            pPass = pTotal - pUnPass;

            StringBuilder sHtml = new StringBuilder();
            sHtml.Append("<li><p><b>���˿���Ϣ</b><span>" + pTotal.ToString() + "</span></p></li>");
            sHtml.Append("<li><p><b>���ڲ����˿���</b><span>" + pUnPass.ToString() + "</span></p></li>");
            sHtml.Append("<li><p><b>�����˿�����Ǽ���Ϣ</b><span>" + pPass.ToString() + "</span></p></li>");
            this.LiteralTotalStats.Text = sHtml.ToString();
        }

        private int StrIntTrim(string instr)
        {
            if (!string.IsNullOrEmpty(instr) && PageValidate.IsNumber(instr))
            {
                return int.Parse(instr);
            }
            else
            {
                return 0;
            }
        }
    }
}

