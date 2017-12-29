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

using UNV.Comm.DataBase;
using UNV.Comm.Web;

using System.Text;
using System.Data.SqlClient;

namespace join.pms.web
{
    public partial class PhotoView : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;

        public string m_TargetUrl;

        private string m_FuncCode;
        private string m_ObjID;
        private string m_ActionName;
        

        private string m_UserID; // 当前登录的操作用户编号

        private string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateParams();

            if (!IsPostBack)
            {
                GetPhotosView(m_ObjID);
            }
        }

        /// <summary>
        /// 验证接受的参数
        /// </summary>
        private void ValidateParams()
        {
            // http://localhost:3481/PhotoView.aspx?action=view&RID=37&sourceUrl=21294AC877F04FA8A12CB4CCC5BF96DEDED85D64BB57D77921AB69D73233AE8AEA531F74C16B64FF51FFC00172A0C9B11E32F64843CD8B16AAD5AAF7B39DF94BDCC33E8B696F57DCD4E9CCC2CB6B1F9BDFE33ED76C81920F7BFAE6D7ADEF9D8CA3FB2D005A051DC674D03B4721DE4017AD4DA660F1EC748408579B698AC20E650FBB58EE6FE90D620D5F2920B97CFC46
            // &oNa=%e7%be%a4%e4%bc%97%e7%94%b5%e5%ad%90%e8%af%81%e7%85%a7%e4%bf%a1%e6%81%af
            m_ActionName = PageValidate.GetTrim(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetTrim(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetTrim(Request.QueryString["k"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "/PhotoList.aspx?" + m_SourceUrlDec;
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }


        /// <summary>
        /// 获取图片信息
        /// </summary>
        /// <param name="objID"></param>
        private void GetPhotosView(string objID)
        {
            string sqlParams = string.Empty;
            string docType = "", docPath = "", docName = "", oprateDate = string.Empty;
            StringBuilder s = new StringBuilder();
            SqlDataReader sdr = null;
            /*
s.Append("<p class=\"a1\">申请人身份证原件</p>");
s.Append("<p class=\"a2\">姓名：王鸿婉&nbsp;&nbsp;电话：18888888888&nbsp;&nbsp;证照类型：.jpg&nbsp;&nbsp;文件名：cid.jpg&nbsp;&nbsp;提交日期：2015-04-18</p>");
s.Append("<p class=\"a3\" style=\"text-align:center; padding:5px 0 15px; min-height:50px;\">图</p>");
             */

            // PersonName,PersonTel,DocsType,DocsName,SourceName,OprateDate
            try
            {
                s.Append("<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tr><td valign=\"top\" class=\"right_02\">");
                sqlParams = "SELECT DocsType,DocsPath,DocsName,DocsType,OprateDate,PersonName,PersonTel FROM v_BizDocs WHERE CommID=" + objID;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    docType = sdr[0].ToString();
                    docPath = sdr[1].ToString();
                    docName = sdr[2].ToString();

                    s.Append("<p class=\"a1\">" + docName + "</p>");
                    s.Append("<p class=\"a2\">姓名：" + sdr["PersonName"].ToString() + "&nbsp;&nbsp;电话：" + sdr["PersonTel"].ToString() + "&nbsp;&nbsp;证照类型：" + docType + "&nbsp;&nbsp;提交日期：" + sdr["OprateDate"].ToString() + "</p>");
                    s.Append("<p class=\"a3\" style=\"text-align:center; padding:5px 0 15px; min-height:50px;\"><img alt=\"" + docName + "\" src=\"" + m_SvrsUrl + docPath + "\" /></p>");
                }
                sdr.Close();
                s.Append("<p class=\"a4\" style=\"text-align:center;\"><input type=\"button\" name=\"ButBackPage\" value=\"· 返回 ·\" id=\"ButBackPage\" onclick=\"javascript:window.location.href='" + m_TargetUrl + "';\" class=\"submit6\" /></p>");
                s.Append("</td></tr></table>");
            }
            catch { if (sdr != null) sdr.Close(); }

            this.LiteralData.Text = s.ToString();
            s = null;
        }

        
    }
}
