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
            this.Uc_PageTop1.GetSysMenu("��ҳ");
            HttpContext.Current.Request.ValidateInput();

            string errors = Request.QueryString["k"];
            if (!string.IsNullOrEmpty(errors))
            {
                this.LiteralMsg.Text = errors;
            }
            else
            {
                errors = "<p class=\"title\">�ǳ���Ǹ����ǰ�����ҳ���ַ�޷����ʣ������Ѿ�ɾ������������ʱ�����á�</p>";
                errors += "<p class=\"list\">";
                errors += "<b>�볢�����²�����</b><br />1����ȷ���������ַ������ʾ����վ��ַƴд��ʽ��ȷ��<br />2�����ͨ���������Ӷ������˸�ҳ�棬������վ����Ա��ϵ��<br />3�����������ҳ <a href=\"/\">������ҳ</a>";
                errors += "</p>";
                this.LiteralMsg.Text = errors;
            }

        }
    }
}
