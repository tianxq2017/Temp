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
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;

namespace join.pms.web.BizInfo
{
    public partial class BizLogoff : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号

        private string m_SqlParams;
        public string m_TargetUrl;

        private string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                SetOpratetionAction("");
            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "";// DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                //if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";
                cssFile = "/css/inidex.css";
                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        #region 页面验证、参数过滤
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
        /// 验证接受的参数
        /// </summary>
        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        #endregion

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
                case "logoff":
                    funcName = "注销";
                    if (!PageValidate.IsNumber(m_ObjID))
                    {
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：每次只能选择一条业务进行操作！", m_TargetUrl, true,true);
                    }
                    else
                    {
                        SetLogoffItems(m_FuncCode);
                    }

                    break;
                case "view": // 查看
                    funcName = "查看内容";
                    //ShowModInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true, true);
                    break;
            }
            this.LiteralNav.Text = "起始页  &gt;&gt; " + CommPage.GetAllBizName(m_FuncCode) + " &gt;" + funcName + "：";
        }
        /*
         0101	3	一孩生育登记
0102	4	二孩生育登记
0103	3	计生家庭奖励扶助对象申报审核
0104	3	计生家庭特别扶助对象申请审核
0105	3	“少生快富”工程申请审核
0106	4	结扎家庭奖励申请审核
0107	3	“一杯奶”受益对象申请审核
0108	3	《独生子女父母光荣证》审核
0109	3	《流动人口婚育证明》申请审核
0110	3	婚育情况证明申请
0111	6	终止妊娠申请审核
         */
        private void SetLogoffItems(string bizCode)
        {
            StringBuilder s = new StringBuilder();
            s.Append("<select name=\"selAuditComments\" id=\"selAuditComments\" onchange=\" SetTxtCommVal(this.options[this.options.selectedIndex].value)\">");
            s.Append("<option value=\"\">请选择注销意见……</option>");
            switch (bizCode)
            {
                case "0101":
                    s.Append("<option value=\"已生育或者收养二个或二个以上子女\">已生育或者收养二个或二个以上子女</option>");
                    s.Append("<option value=\"其他情况\">其他情况</option>");
                    break;
                case "0102":
                    s.Append("<option value=\"因非医学需要选择性别终止妊娠；\">因非医学需要选择性别终止妊娠；</option>");
                    s.Append("<option value=\"规避法律、法规将自己所生育的子女送别人抚养；\">规避法律、法规将自己所生育的子女送别人抚养；</option>");
                    s.Append("<option value=\"领取《再生育证》生育一个子女后，弃婴、溺婴或者遗弃、虐待、拒绝抚养子女；\">领取《再生育证》生育一个子女后，弃婴、溺婴或者遗弃、虐待、拒绝抚养子女；</option>");
                    s.Append("<option value=\"弄虚作假，隐瞒相关事实；\">弄虚作假，隐瞒相关事实；</option>");
                    s.Append("<option value=\"其他情况\">其他情况</option>");
                    break;
                case "0103":
                    s.Append("<option value=\"弄虚作假，隐瞒相关事实\">弄虚作假，隐瞒相关事实</option>");
                    s.Append("<option value=\"其他情况\">其他情况</option>");
                    break;
                case "0104":
                    s.Append("<option value=\"弄虚作假，隐瞒相关事实\">弄虚作假，隐瞒相关事实</option>");
                    s.Append("<option value=\"其他情况\">其他情况</option>");
                    break;
                case "0105":
                    s.Append("<option value=\"弄虚作假，隐瞒相关事实\">弄虚作假，隐瞒相关事实</option>");
                    s.Append("<option value=\"其他情况\">其他情况</option>");
                    break;
                case "0106":
                    s.Append("<option value=\"弄虚作假，隐瞒相关事实\">弄虚作假，隐瞒相关事实</option>");
                    s.Append("<option value=\"其他情况\">其他情况</option>");
                    break;
                case "0107":
                    s.Append("<option value=\"弄虚作假，隐瞒相关事实\">弄虚作假，隐瞒相关事实</option>");
                    s.Append("<option value=\"其他情况\">其他情况</option>");
                    break;
                case "0108"://《独生子女父母光荣证》
                    s.Append("<option value=\"隐瞒真实情况，申领《独生子女父母光荣证》时实际已生育或收养二个或二个以上子女；\">隐瞒真实情况，申领《独生子女父母光荣证》时实际已生育或收养二个或二个以上子女；</option>");
                    s.Append("<option value=\"领取《独生子女父母光荣证》后，符合再生育条件，已经申报再生育；\">领取《独生子女父母光荣证》后，符合再生育条件，已经申报再生育；</option>");
                    s.Append("<option value=\"领取《独生子女父母光荣证》后违法再生育或收养子女\">领取《独生子女父母光荣证》后违法再生育或收养子女</option>");
                    s.Append("<option value=\"其他情况\">其他情况</option>");
                    break;
                case "0109":
                    s.Append("<option value=\"婚育证明过期（有效期3年）\">婚育证明过期（有效期3年）</option>");
                    s.Append("<option value=\"弄虚作假，隐瞒相关事实\">弄虚作假，隐瞒相关事实</option>");
                    s.Append("<option value=\"其他情况\">其他情况</option>");
                    break;
                case "0110":
                    s.Append("<option value=\"弄虚作假，隐瞒相关事实\">弄虚作假，隐瞒相关事实</option>");
                    s.Append("<option value=\"其他情况\">其他情况</option>");
                    break;
                case "0111":
                    s.Append("<option value=\"弄虚作假，隐瞒相关事实\">弄虚作假，隐瞒相关事实</option>");
                    s.Append("<option value=\"其他情况\">其他情况</option>");
                    break;
                case "0122":
                    s.Append("<option value=\"因非医学需要选择性别终止妊娠；\">因非医学需要选择性别终止妊娠；</option>");
                    s.Append("<option value=\"规避法律、法规将自己所生育的子女送别人抚养；\">规避法律、法规将自己所生育的子女送别人抚养；</option>");
                    s.Append("<option value=\"领取《再生育证》生育一个子女后，弃婴、溺婴或者遗弃、虐待、拒绝抚养子女；\">领取《再生育证》生育一个子女后，弃婴、溺婴或者遗弃、虐待、拒绝抚养子女；</option>");
                    s.Append("<option value=\"弄虚作假，隐瞒相关事实；\">弄虚作假，隐瞒相关事实；</option>");
                    s.Append("<option value=\"其他情况\">其他情况</option>");
                    break;
                default:
                    s.Append("<option value=\"\">该业务审核意见不存在</option>");
                    break;
            }
            s.Append("</select>");

            this.LiteralComments.Text = s.ToString();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // SELECT BizID,BizStep,AreaCode,AreaName,Comments,Approval,Signature,CreateDate,OprateDate FROM BIZ_WorkFlows  txtApprovalUserTel
            string strErr = string.Empty;
            string bizID = m_ObjID;
            string Attribs = string.Empty;
            string Comments = SQLFilter.GetFilterSQL(PageValidate.GetTrim(this.txtOtherDocs.Text));

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            try
            {
                if (CommPage.CheckLogOffAttribs("BizID IN(" + m_ObjID + ")"))
                {
                    System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                    list.Add("UPDATE BIZ_Certificates SET Attribs=1 WHERE BizID IN(" + bizID + ")");
                    list.Add("UPDATE BIZ_Contents SET Attribs=5,Comments='" + Comments + "' WHERE BizID IN(" + bizID + ")");
                    DbHelperSQL.ExecuteSqlTran(list);
                    list = null;

                    CommPage.SetBizLog(bizID, m_UserID, "业务注销", "用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 进行了业务（业务编号=" + bizID + "）注销操作");

                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您所选择的业务注销操作成功！", m_TargetUrl, true, true);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：只有审核完毕的业务才可以执行“注销”操作！", m_TargetUrl, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：" + ex.Message, m_TargetUrl, true, true);
            }
        }
    }
}
