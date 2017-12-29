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
using UNV.Comm.Web;
using System.IO;
namespace join.pms.web.Disk
{
    public partial class NetWorkInfo : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_ActionName;
        private string m_ObjID;
        private string m_FuncNo;
        private string m_FuncUser;

        private string m_TargetUrl;
        private string m_SqlParams;
        private DataTable m_Dt;
        private string m_UserID; // 当前登录的操作用户编号

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateParams();
            AuthenticateUser();

            if (!IsPostBack)
            {
                this.txtUrlParams.Value = m_TargetUrl;
                if (String.IsNullOrEmpty(m_ActionName))
                {
                    Response.Write("非法访问：操作被终止！");
                    Response.End();
                }
                else
                {
                    SetOpratetionAction("");
                }
            }
        }

        private void ValidateParams()
        {
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]); 
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]); 

            if (!string.IsNullOrEmpty(m_ActionName) && m_ActionName == "view" && string.IsNullOrEmpty(m_ObjID))
            {
                m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
            }
            m_SourceUrl = DESEncrypt.Decrypt(m_SourceUrl);
            if (!String.IsNullOrEmpty(m_SourceUrl))
            {
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrl;
                m_FuncNo = StringProcess.AnalysisParas(m_SourceUrl, "FuncNo");
            }
        }

        /// <summary>
        /// 身份验证
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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 设置操作行为 add新增,edit编辑,del删除,4查看,5审核,6分配角色 等
        /// </summary>
        /// <param name="oprateName">操作名称</param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("非法访问：操作被终止！");
                    Response.End();
                }
            }
            else
            {
                if (!PageValidate.IsNumber(m_ObjID))
                {
                    m_ObjID = m_ObjID.Replace("s", ",");
                }
            }

            switch (m_ActionName)
            {
                case "add": // 新增
                    funcName = "发布短讯";
                    //ShowAddInfo();
                    break;
                case "view": // 查看
                    funcName = "消息查看";
                    ShowModInfo(m_ObjID);
                    break;
                case "edit": // 编辑
                    funcName = "修改用户信息";
                    //ShowModInfo(m_ObjID);
                    break;
                case "del": // 删除
                    funcName = "删除消息";
                    DelInfo(m_ObjID);
                    break;
                default:
                    if (Request.UrlReferrer != null) Response.Redirect(Request.UrlReferrer.ToString());
                    break;
            }
        }
        #region
        private void ShowModInfo(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                m_SqlParams = "SELECT FilePath FROM UserHD_Files WHERE FileID=" + objID;
                string userFiles = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                if (!string.IsNullOrEmpty(userFiles))
                {
                    Response.Write("<script>var objWindow = window.open('" + userFiles + "'); if(objWindow){ }else{window.location.href = '" + userFiles + "';}</script>");
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您所查看的文件不存在！", txtUrlParams.Value, true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：查看文件不可以多选！", txtUrlParams.Value, true);
            }
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            try
            {
                bool isNormal = true;
                m_SqlParams = "SELECT FileStatus FROM UserHD_Files WHERE FileID IN(" + objID + ")";
                m_Dt = new DataTable();
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                for (int i = 0; i < m_Dt.Rows.Count; i++)
                {
                    if (m_Dt.Rows[i][0].ToString() == "0")
                    {
                        isNormal = false;
                        break;
                    }
                }
                if (isNormal)
                {
                    m_SqlParams = "DELETE FROM UserHD_Files WHERE FileID IN(" + objID + ")";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    //System.IO.Directory.Delete("../" + m_Dt.Rows[0]["FileName"] + "");//删除服务器上的文件
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：删除成功！", txtUrlParams.Value, true);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您选定的内容包含正常的图片，只有用户删除后的图片才可以清理！", txtUrlParams.Value, true);
                }

            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, txtUrlParams.Value, true);
            }
        }
        #endregion

    }
}
