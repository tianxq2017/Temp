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

using UNV.Comm.DataBase;
namespace join.pms.wap.userctrl
{
    public partial class Uc_PageTop : System.Web.UI.UserControl
    {
        private string m_FileExt = ConfigurationManager.AppSettings["FileExtension"];

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void GetSysMenu(string menuName)
        {
            StringBuilder sHtml = new StringBuilder();  
            sHtml.Append("<div class=\"header_top\">");
            sHtml.Append("<div class=\"header\">");
            if (menuName == "��ҳ")
            {
                sHtml.Append("<div class=\"logo\"><a href=\"/\"><img src=\"/images/logo.png\" alt=\"��ҳ\" /></a></div>");
                sHtml.Append(GetDH());
            }
            else
            {
                sHtml.Append("<div class=\"header_l\"><a href=\"javascript:void(0)\" onclick=\"javascript:history.back(-1);\"></a></div>");
                sHtml.Append(GetDH());
                sHtml.Append("<span class=\"header_c\">" + menuName + "</span>");

            }
            sHtml.Append("<div class=\"clr\"></div>");
            sHtml.Append("</div>");
            sHtml.Append("</div>");
            this.LiteralMenu.Text = sHtml.ToString();
            sHtml = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetDH()
        {
            string reurnVal = string.Empty;
            reurnVal = "<div class=\"header_r header_r_hover\">";
            reurnVal += "<select id=\"selectdd\" style=\"display:none;\">";
            reurnVal += "<option value=\"/\">��ҳ</option>";
            reurnVal += "<option value=\"/Svrs-BizCategories/0101-1." + this.m_FileExt + "\">һ�������Ǽ�</option>";
            reurnVal += "<option value=\"/Svrs-BizCategories/0102-1." + this.m_FileExt + "\">���������Ǽ�</option>";
            reurnVal += "<option value=\"/Svrs-BizCategories/0122-1." + this.m_FileExt + "\">����������</option>";
            reurnVal += "<option value=\"/Svrs-BizCategories/0103-1." + this.m_FileExt + "\">��������</option>";
            reurnVal += "<option value=\"/Svrs-BizCategories/0109-1." + this.m_FileExt + "\">�����˿ڻ���֤��</option>";
            reurnVal += "<option value=\"/UserCenter/BizWorkFlows.aspx?action=view\">���Ȳ鿴</option>";
            reurnVal += "<option value=\"/YslXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdxx." + this.m_FileExt + "\">�˻�ע��</option>";
            reurnVal += "<option value=\"/OC." + this.m_FileExt + "\">�û�����</option>";
            reurnVal += "<option value=\"/USERSIGNOUT." + this.m_FileExt + "\">��ȫ�˳�</option>";
            reurnVal += "</select>";
            reurnVal += "</div>";
            return reurnVal;
        }
    }
}