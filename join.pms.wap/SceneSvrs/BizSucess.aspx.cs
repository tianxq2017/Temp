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

namespace join.pms.wap.SceneSvrs
{
    public partial class BizSucess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Uc_PageTop1.GetSysMenu("�ύ�ɹ�");
        }
    }
}
