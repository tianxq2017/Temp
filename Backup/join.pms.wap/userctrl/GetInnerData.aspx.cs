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
using System.Globalization;
using UNV.Comm.DataBase;
using UNV.Comm.Web;
using System.Text;
using join.pms.dal;
namespace join.pms.wap.userctrl
{
    public partial class GetInnerData : System.Web.UI.Page
    {
        private string m_UserID; // 当前登录的操作用户编号
        private string m_FileExt = ConfigurationManager.AppSettings["FileExtension"];
        protected void Page_Load(object sender, EventArgs e)
        {

            AuthenticateUser();

            string funcNo = PageValidate.GetTrim(Request["FuncNo"]);
            string objID = PageValidate.GetTrim(Request["oID"]);
            string objName = PageValidate.GetTrim(Request["oNa"]);

            if (!String.IsNullOrEmpty(funcNo))
            {
                try
                {
                    SetExecMethods(funcNo, objID, objName);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        private void AuthenticateUser()
        {
            bool returnVa = false;
            if (Request.Browser.Cookies)
            {
                HttpCookie loginCookie = Request.Cookies["AREWEB_OC_PUBSVRS_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["UserID"] != null && !String.IsNullOrEmpty(Session["PersonID"].ToString())) { returnVa = true; m_UserID = Session["PersonID"].ToString(); }
            }

            if (!returnVa)
            {
                Response.Write("操作失败：请登录后再试！");
                Response.End();
            }
        }

        /// <summary>
        /// 设置执行的方法
        /// </summary>
        /// <param name="identityKeys"></param>
        private void SetExecMethods(string funcNo, string objID, string objName)
        {
            string returnMsg = string.Empty;

            switch (funcNo)
            {
                case "8888": // 行政区划选择
                    Response.Write(BIZ_Common.GetAreaData(objID, objName));
                    break;
                default:
                    Response.Write("调用失败：无此接口！");
                    break;
            }
        }


    }
}