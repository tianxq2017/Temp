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

namespace join.pms.wap
{
    public partial class errors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Uc_PageTop1.GetSysMenu("首页");
            HttpContext.Current.Request.ValidateInput();

            string errors = Request.QueryString["k"];
            if (!string.IsNullOrEmpty(errors))
            {
                this.LiteralMsg.Text = errors;
            }
            else
            {
                errors = "<p class=\"title\">非常抱歉，当前请求的页面地址无法访问，可能已经删除、更名或暂时不可用。</p>";
                errors += "<p class=\"list\">";
                errors += "<b>请尝试以下操作：</b><br />1、请确保浏览器地址栏中显示的网站地址拼写格式正确。<br />2、如果通过单击链接而到达了该页面，请与网站管理员联系。<br />3、重新浏览网页 <a href=\"/\">返回首页</a>";
                errors += "</p>";
                this.LiteralMsg.Text = errors;
            }

        }
    }
}
