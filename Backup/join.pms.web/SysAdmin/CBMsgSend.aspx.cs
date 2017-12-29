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
using join.pms.dal;

using System.Text;
using System.Data.SqlClient;

namespace join.pms.web.SysAdmin
{
    public partial class CBMsgSend : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;

        private string m_ActionName;
        private string m_ObjID;
        private string m_UserID; // 当前登录的操作用户编号
        private string m_SqlParams;
        private string m_TargetUrl;
        private DataTable m_Dt;
        private string m_FuncCode;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();
            if (!IsPostBack)
            {
                CuiBan();
                //SetPageStyle(m_UserID);
                if (String.IsNullOrEmpty(m_ActionName))
                {
                    Response.Write("非法访问，操作被终止！");
                    Response.End();
                }
                else
                {
                    SetOpratetionAction("发送信息");
                }
            }
        }

        /// <summary>
        /// 传递值获取
        /// </summary>
        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }
            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        /// <summary>
        /// 获取用户样式
        /// </summary>
        /// <param name="userID"></param>
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

        #region 身份验证等

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
        /// 设置操作行为
        /// </summary>
        /// <param name="oprateName"></param>
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
                    funcName = "";
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }

        /// <summary>
        /// 催办信息取得
        /// </summary>
        /// <param name="bizID"></param>
        public string CuiBan()
        {
            string retutnVal = string.Empty;
            string UserAccount="", UserName = "", UserTel = string.Empty;
            string BUserTel = "", BizName = "", BizMName = "", BizWName = "", MsgBody = string.Empty;
            SqlDataReader sdr = null;
            try
            {
                string sqlParams = "SELECT UserAccount,UserName,UserTel FROM USER_BaseInfo WHERE UserID=" + this.m_UserID;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        UserAccount = sdr["UserAccount"].ToString();
                        UserName = sdr["UserName"].ToString();
                        UserTel = sdr["UserTel"].ToString();
                    }
                }
                sdr.Close();

                // BIZ_Contents --> Attribs: 0,初始提交;1,审核中 2,通过 3,补正 4,撤销, 5,注销 9,归档
                string Attribs = CommPage.GetSingleValue("SELECT Attribs FROM BIZ_Contents WHERE BizID=" + this.m_ObjID);
                if (!String.IsNullOrEmpty(Attribs) && (Attribs == "1" || Attribs == "6"))
                {
                    sqlParams = " SELECT C.BizName,B.UserTel,B.UserName,C.Fileds01,C.Fileds08 FROM BIZ_WorkFlows A left join USER_BaseInfo B on A.AreaCode = B.UserAccount left join BIZ_Contents C on A.BizID = C.BizID WHERE A.Attribs=9 AND A.AreaCode <> '" + UserAccount + "' AND A.BizID='" + this.m_ObjID + "' GROUP BY C.BizName,B.UserTel,B.UserName,C.Fileds01,C.Fileds08";
                    sdr = DbHelperSQL.ExecuteReader(sqlParams);
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            BUserTel = BUserTel + sdr["UserTel"].ToString() + ",";
                            BizName = sdr["BizName"].ToString();
                            BizMName = sdr["Fileds01"].ToString();
                            BizWName = sdr["Fileds08"].ToString();
                        }
                        MsgBody = "编号[" + this.m_ObjID + "]，男方[" + BizMName + "]，女方[" + BizWName + "]的[" + BizName + "]请尽快办理！催办人：" + UserName + "  联系电话：" + UserTel;
                        this.txtMsgMobile.Text = BUserTel;
                        this.txtMsgBody.Text = MsgBody;
                    }
                    sdr.Close(); 
                    sdr.Dispose();
                }
                else
                {
                    retutnVal = "操作提示：仅待审核或审核中的业务才可以执行催办操作！";
                }
            }
            catch (Exception ex)
            {
                if (sdr != null) 
                { 
                    sdr.Close(); 
                    sdr.Dispose(); 
                }
                retutnVal = "操作提示：" + ex.Message;
            }
            return retutnVal;
        }
        #endregion

        /// <summary>
        /// 短信息新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string returnMsg = string.Empty;
            string msgBody = string.Empty; // txtMsgBody
            string strErr = string.Empty;
            string ContactTel = PageValidate.GetTrim(this.txtMsgMobile.Text);
            StringBuilder msgInfo = new StringBuilder();
            msgBody = PageValidate.GetTrim(this.txtMsgBody.Text);

            if (String.IsNullOrEmpty(ContactTel))
            {
                strErr += "接收人手机号码不能为空！\\n";
            }

            ContactTel = ContactTel.Replace("，", ","); // 替换全角符号
            ContactTel = ContactTel.Replace(" ", "");//替换空格
            ContactTel = ContactTel.Replace(";", ",");//替换;
            string[] aryTel = ContactTel.Split(',');
            if (aryTel.Length > 20)
            {
                strErr += "最多只支持20个号码群发！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            returnMsg = SendMsg.SendMsgByModem(ContactTel, msgBody);
            if (!string.IsNullOrEmpty(returnMsg) && int.Parse(returnMsg) > 0)
            {
                msgInfo.Append("发送到[ " + ContactTel + " ]信息成功插入到发送队列，发送内容为：[ " + msgBody + " ]<br/>");
            }
            else
            {
                msgInfo.Append("发送到[ " + ContactTel + " ]失败：" + returnMsg + "！<br/>");
            }
            this.LiteralResults.Text = msgInfo.ToString();
        }

    }
}
