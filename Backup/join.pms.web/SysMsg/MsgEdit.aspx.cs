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

using System.IO;
using System.Text;

using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.web.SysMsg
{
    public partial class MsgEdit : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_UserID; // 当前登录的操作用户编号
        private string m_SqlParams;
        private DataTable m_Dt;

        protected string m_TargetUrl;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;
        protected string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                if (String.IsNullOrEmpty(m_FuncCode) || !PageValidate.IsNumber(m_FuncCode))
                {
                    Response.Write("非法访问，操作被终止！");
                    Response.End();
                }
                else
                {
                    SetOpratetionAction("系统短讯");
                }
            }
        }

        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                // "pSearch=E31E58FB2A760DEA&UserID=1&FuncUser=&FuncCode=0901&FuncNa=%e5%8f%91%e4%bb%b6%e7%ae%b1&p=1"
                // http://localhost:3481/SysMsg/MsgEdit.aspx?action=add&BizID=&sourceUrl=21294AC877F04FA8A12CB4CCC5BF96DEDED85D64BB57D77921AB69D73233AE8AEA531F74C16B64FF51FFC00172A0C9B1AC42342848D256BCBF324ECAADFEAE10FAE87D559C6F9759176DC1204C469C487C91699C2A221898AA4119DBAFABE0ABFA5BADA99521031B
                // http://localhost:3481/UnvCommList.aspx?1=1&FuncCode=0901&FuncNa=%e5%8f%91%e4%bb%b6%e7%ae%b1
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                this.txtFileUrl.Value = m_SvrsUrl;
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
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

        #region 页面首次加载时显示的信息
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
                    //ShowModInfo(m_ObjID);
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

            //this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
            this.LiteralNav.Text = "管理首页 &gt;&gt; " + oprateName + " &gt;&gt; " + funcName + "：";
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            try
            {
                //0601	发件箱 SourceDel 0,发送方删除 TargetDel 1,接收方删除 SourceDel TargetDel
                m_SqlParams = string.Empty;
                if (m_FuncCode == "0902") { m_SqlParams = "UPDATE SYS_Msg SET TargetDel=1 WHERE MsgID IN(" + objID + ")"; }
                else if (m_FuncCode == "0901") { m_SqlParams = "UPDATE SYS_Msg SET SourceDel=1 WHERE MsgID IN(" + objID + ")"; }
                else { m_SqlParams = "DELETE FROM SYS_Msg WHERE MsgID IN(" + objID + ") AND MsgType!=0"; }

                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "操作提示：操作成功！", m_TargetUrl, true);

            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：" + ex.Message, m_TargetUrl, true);
            }
            Response.End();
        }
        #endregion

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string MsgTitle = PageValidate.GetTrim(this.txtMsgTitle.Text);
            string MsgBody = PageValidate.Encode(join.pms.dal.CommBiz.GetTrim(this.objCKeditor.Text));
            string TargetUserIDs = PageValidate.GetTrim(this.txtExecUserID.Value); ;
            string incDocsID = "";

            if (MsgTitle == "")
            {
                strErr += "消息头不能为空！\\n";
            }
            if (MsgBody == "")
            {
                strErr += "消息体不能为空！\\n";
            }
            if (TargetUserIDs == "")
            {
                strErr += "请选择接收人！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            try
            {
                System.Collections.Generic.List<string> list = null; ;
                string[] aryUsers = TargetUserIDs.Split(',');
                list = new System.Collections.Generic.List<string>(aryUsers.Length + 2);
                for (int i = 0; i < aryUsers.Length; i++)
                {
                    list.Add("INSERT INTO [SYS_Msg]([SourceUserID], [TargetUserID], [MsgTitle], [MsgBody], [MsgType],[DocID]) VALUES(" + m_UserID + ", " + aryUsers[i] + ", '" + MsgTitle + "', '" + MsgBody + "', 2,'" + incDocsID + "')");
                }
                //UpdateIncDocs(incDocsID, reValue.ToString());
                if (DbHelperSQL.ExecuteSqlTran(list) > 0)
                {
                    list = null;
                    //Response.Redirect(txtUrlParams.Value);
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：发送成功！", m_TargetUrl, true,true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, ex.Message, m_TargetUrl, true, true);
            }
        }

    }
}
