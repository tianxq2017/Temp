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

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;
namespace join.pms.wap.SceneSvrs
{
    public partial class BizCategories : UNV.Comm.Web.PageBase
    {
        private string m_BizCode;
        private string m_BizType;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateParams();
            SetPageHeader("业务介绍");
            this.Uc_PageTop1.GetSysMenu("业务介绍");
            if (!IsPostBack)
            { GetCategories(); }
        }
        #region 设置页头信息及导航\验证参数\验证用户等
        //设置页头信息及导航等
        private void SetPageHeader(string objTitles)
        {
            try
            {
                this.Page.Header.Title = this.m_SiteName;
                base.AddMetaTag("keywords", Server.HtmlEncode(this.m_SiteName + "," + objTitles + "," + this.m_SiteKeyWord));
                base.AddMetaTag(this.m_SiteName);
                base.AddMetaTag("description", Server.HtmlEncode(m_SiteDescription));
                base.AddMetaTag("copyright", Server.HtmlEncode("本页版权归 西安网是科技发展有限公司 所有。All Rights Reserved"));

            }
            catch
            {
                Server.Transfer("~/errors.aspx");
            }
        }
        /// <summary>
        /// 验证接受的参数
        /// </summary>
        private void ValidateParams()
        {
            m_BizCode = PageValidate.GetTrim(Request.QueryString["c"]);
            m_BizType = PageValidate.GetTrim(Request.QueryString["t"]);
            if (!string.IsNullOrEmpty(m_BizCode) && PageValidate.IsNumber(m_BizCode) && !string.IsNullOrEmpty(m_BizType) && PageValidate.IsNumber(m_BizType))
            { }
            else
            {
                Server.Transfer("~/errors.aspx");
            }
        }

        /// <summary>
        /// 获取当前业务信息
        /// </summary>
        private void GetCategories()
        {
            StringBuilder sHtml = new StringBuilder();
            Biz_Categories bizCateg = new Biz_Categories();
            bizCateg.SelectSingle(m_BizCode);

            sHtml.Append("<div class=\"part_form\">");
            sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>办理须知</div>");
            sHtml.Append("<div class=\"part_content\">" + bizCateg.BizTemplates + "</div>");
            //if (m_BizCode == "0112" || m_BizCode == "0113" || m_BizCode == "0114")
            //{
            //}
            //else
            //{
            //    sHtml.Append("<div class=\"rules\"><p class=\"rules_checkbox\"><input onclick=\"ShowSQ();\" id=\"ckTL\" name=\"ckTL\" type=\"checkbox\" /> 符合办理条件，选择后进行申请办理</p></div>");
            //}
            
            sHtml.Append("</div>");
            m_BizType = "1";
            //sHtml.Append("<div class=\"part_button\" id=\"divSQ\"><a href=\"/Svrs-Area/" + m_BizCode + "-" + m_BizType + "." + m_FileExt + "\">我要申请</a></div>");
     
            if (m_BizCode == "0112" || m_BizCode == "0113" || m_BizCode == "0114")
            {
            }
            else
            {
                Session["BizCode"] = m_BizCode;
                sHtml.Append("<div class=\"part_button\" id=\"divSQ\"><a href=\"/Svrs-Area/" + m_BizCode + "-" + m_BizType + "." + m_FileExt + "\">我要申请</a></div>");
            }
            //if (m_BizType == "1")
            //{
            //    sHtml.Append("<div class=\"part_button\" id=\"divSQ\" style=\"display:none\"><a href=\"/Svrs-Area/" + m_BizCode + "-" + m_BizType + "." + m_FileExt + "\">我要申请</a></div>");
            //}           
            bizCateg = null;
            this.LiteralCategoriesInfo.Text = sHtml.ToString();
            sHtml = null;
        }
        #endregion
    }
}
