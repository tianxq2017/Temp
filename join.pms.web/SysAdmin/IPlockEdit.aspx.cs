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

namespace join.pms.web.SysAdmin
{
    public partial class IPlockEdit : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_UserID; // 当前登录的操作用户编号
        private string m_SqlParams;
        private DataTable m_Dt;

        protected string m_TargetUrl;

        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetOpratetionAction("科室管理");
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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/loginTemp.aspx';</script>");
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
            if (!String.IsNullOrEmpty(m_FuncCode.Substring(0, 2)))
            {
                switch (m_ActionName)
                {
                    case "add": // 新增
                        funcName = "新增";
                        break;
                    case "edit": // 编辑
                        funcName = "编辑";
                        ShowModInfo(m_ObjID);
                        break;
                    case "del": // 删除
                        funcName = "删除";
                        DelInfo(m_ObjID);
                        break;
                    case "on": // 启用/取消启用
                        funcName = "查看内容";
                        SetIpOn(m_ObjID);
                        break;
                    case "view": // 查看
                        funcName = "查看内容";
                        ShowModInfo(m_ObjID);
                        break;
                    default:
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                        break;
                }
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            m_SqlParams = "SELECT ipName,ipType, ipStart, ipEnd FROM [SYS_IP_Off] WHERE CommID=" + m_ObjID;
            try
            {
                m_Dt = new DataTable();
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (m_Dt.Rows.Count == 1)
                {
                    this.txtIpName.Text = m_Dt.Rows[0]["ipName"].ToString();

                    string ipType = m_Dt.Rows[0]["ipType"].ToString();
                    if (ipType == "0") { this.rdSingle.Checked = true; }
                    else { this.rdMore.Checked = true; this.trIpEnd.Style.Add("display", "block"); }

                    this.txtIpStart.Text = m_Dt.Rows[0]["ipStart"].ToString();
                    this.txtIpEnd.Text = m_Dt.Rows[0]["ipEnd"].ToString();
                    if (m_ActionName == "view") this.btnAdd.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
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
                //Response.Write("<script language='javascript'>alert('提示信息：考虑到帐号安全，此功能被禁用，请使用冻结功能操作！');window.location.href='" + m_TargetUrl + "';</script>");
                //return;
                m_SqlParams = "DELETE FROM SYS_IP_Off WHERE [CommID] IN(" + objID + ")";
                DbHelperSQL.ExecuteSql(m_SqlParams);
                
                MessageBox.ShowAndRedirect(this.Page, "操作提示：删除成功！", m_TargetUrl, true);

            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
            Response.End();
        }
        /// <summary>
        /// 审核信息
        /// </summary>
        /// <param name="objID"></param>
        private void SetIpOn(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                try
                {
                    // Attribs:0 默认;1 审核; 3 屏蔽; 4 删除; 9 推荐
                    string Attribs = DbHelperSQL.GetSingle("SELECT [Attribs] FROM [SYS_IP_Off] WHERE CommID=" + objID).ToString();
                    if (!string.IsNullOrEmpty(Attribs) && Attribs == "0")
                    {
                        m_SqlParams = "UPDATE SYS_IP_Off SET Attribs=1 WHERE [CommID] IN(" + objID + ")";
                    }
                    else
                    {
                        m_SqlParams = "UPDATE SYS_IP_Off SET Attribs=0 WHERE [CommID] IN(" + objID + ")";
                    }
                    DbHelperSQL.ExecuteSql(m_SqlParams);

                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息启用/取消启用操作成功！", m_TargetUrl, true);
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                 MessageBox.ShowAndRedirect(this.Page, "操作提示：考虑到系统安全，此操作每次只能选择一条记录，不可以多选！", m_TargetUrl, true);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                string strErr = string.Empty;
                string ipType = "0";
                string ipName = PageValidate.GetTrim(this.txtIpName.Text);
                string ipStart = PageValidate.GetTrim(this.txtIpStart.Text);
                string ipEnd = PageValidate.GetTrim(this.txtIpEnd.Text);

                if (string.IsNullOrEmpty(ipName)) strErr += "名称描述不能为空！\\n";
                if (!this.rdSingle.Checked) { ipType = "1"; }
                else { ipType = "0"; }

                if (String.IsNullOrEmpty(ipStart))
                {
                    strErr += "起始IP不能为空！\\n";
                }
                else if (!join.pms.dal.CommBiz.IsValidIPAddress(ipStart))
                {
                    strErr += "起始IP不是有效的IP地址！\\n";
                }
                if (ipType == "1")
                {
                    if (String.IsNullOrEmpty(ipEnd))
                    {
                        strErr += "结尾IP不能为空！\\n";
                    }
                    else if (!join.pms.dal.CommBiz.IsValidIPAddress(ipEnd))
                    {
                        strErr += "结尾IP不是有效的IP地址！\\n";
                    }
                    if (!validateIP(ipStart, ipEnd))
                    { strErr += "起始IP不能大于结尾IP！\\n"; }
                }
                if (strErr != "")
                {
                    if (ipType == "1") { this.trIpEnd.Style.Add("display", "block"); }
                    MessageBox.Show(this, strErr);
                    return;
                }
                if (m_ActionName == "add")
                {
                    m_SqlParams = "INSERT INTO [SYS_IP_Off](ipName,ipType, ipStart, ipEnd) VALUES('" + ipName + "'," + ipType + ", '" + ipStart + "', '" + ipEnd + "')";
                    try
                    { DbHelperSQL.GetSingle(m_SqlParams).ToString(); }
                    catch (Exception ex) { MessageBox.Show(this, ex.Message); }
                }
                else if (m_ActionName == "edit")
                {
                    m_SqlParams = "UPDATE SYS_IP_Off SET ipName='" + ipName + "',ipType=" + ipType + ",ipStart='" + ipStart + "',ipEnd='" + ipEnd + "' WHERE CommID=" + m_ObjID;

                    try
                    { DbHelperSQL.ExecuteSql(m_SqlParams); }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message);
                        return;
                    }
                }
              
                MessageBox.ShowAndRedirect(this.Page, "操作提示：操作成功！", m_TargetUrl, true);
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：" + ex.Message, m_TargetUrl, true);
            }
        }
        /// <summary>
        /// 确保结束ip大于开始ip
        /// </summary>
        private bool validateIP(string startIP, string endIP)
        {
            // 分离出ip中的四个数字位
            string[] startIPArray = startIP.Split('.');
            string[] endIPArray = endIP.Split('.');

            // 取得各个数字
            long[] startIPNum = new long[4];
            long[] endIPNum = new long[4];
            for (int i = 0; i < 4; i++)
            {
                startIPNum[i] = long.Parse(startIPArray[i].Trim());
                endIPNum[i] = long.Parse(endIPArray[i].Trim());
            }

            // 各个数字乘以相应的数量级
            long startIPNumTotal = startIPNum[0] * 256 * 256 * 256 + startIPNum[1] * 256 * 256 + startIPNum[2] * 256 + startIPNum[3];
            long endIPNumTotal = endIPNum[0] * 256 * 256 * 256 + endIPNum[1] * 256 * 256 + endIPNum[2] * 256 + endIPNum[3];

            if (startIPNumTotal > endIPNumTotal)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}

