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
using join.pms.dal;

namespace join.pms.web.SysAdmin
{
    public partial class SysPeopleStatEdit : System.Web.UI.Page
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
        private string m_FuncName;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetOpratetionAction(m_FuncName);
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
                m_FuncName = CommPage.GetFuncName(m_FuncCode);//类别名称
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        #region 页面首次加载时显示的信息


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
                    funcName = "发布";
                    break;
                case "edit": // 编辑
                    funcName = "修改";
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // 删除
                    funcName = "删除内容";
                    DelInfo(m_ObjID);
                    break;
                case "view": // 查看
                    funcName = "查看内容";
                    break;
                case "audit": // 审核
                    funcName = "审核内容";
                    break;
                default:
                     MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = GetPageTopNavInfo(oprateName, funcName);
            this.LiteralTitle.Text = oprateName;
            this.LiteralFuncName.Text = funcName;
        }
        /// <summary>
        /// 获取页面导航信息
        /// </summary>
        /// <param name="oprateName"></param>
        /// <param name="funcName"></param>
        /// <returns></returns>
        private string GetPageTopNavInfo(string oprateName, string funcName)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"rnavbg\">");
            sb.Append("<tr><td width=\"20\" style=\"background-image:url(/images/CMS/right_topbg.jpg);border:0px;background-repeat:repeat-x;\">&nbsp;</td><td class=\"rnav01\" style=\"background-image:url(/images/CMS/right_topbg.jpg);border:0px;background-repeat:repeat-x;\">" + CommPage.GetAllTreeName(this.m_FuncCode) + oprateName + " &gt; " + funcName + "</td></tr>");
            sb.Append("</table>");
            return sb.ToString();
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
        /// 修改
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            DataTable tempDT = new DataTable();
            m_SqlParams = "SELECT [CommID], [UintName], [CZRenKou], [YLFunNv], [YHFunNv], [BYRenShu], [CSRenKou], [SWRenKou], [LCRenKou], [LRRenKou], [Remarks] FROM [Sys_PeopleStat] WHERE CommID=" + m_ObjID;
            try
            {
                tempDT = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (tempDT.Rows.Count == 1)
                {
                    this.txtUintName.Text = tempDT.Rows[0]["UintName"].ToString();
                    this.txtCZRenKou.Text = tempDT.Rows[0]["CZRenKou"].ToString();
                    this.txtYLFunNv.Text = tempDT.Rows[0]["YLFunNv"].ToString();
                    this.txtYHFunNv.Text = tempDT.Rows[0]["YHFunNv"].ToString();
                    this.txtBYRenShu.Text = tempDT.Rows[0]["BYRenShu"].ToString();
                    this.txtCSRenKou.Text = tempDT.Rows[0]["CSRenKou"].ToString();
                    this.txtSWRenKou.Text = tempDT.Rows[0]["SWRenKou"].ToString();
                    this.txtLCRenKou.Text = tempDT.Rows[0]["LCRenKou"].ToString();
                    this.txtLRRenKou.Text = tempDT.Rows[0]["LRRenKou"].ToString();
                    this.txtRemarks.Text = PageValidate.Decode(tempDT.Rows[0]["Remarks"].ToString());

                }
                if (m_ActionName == "view") this.btnAdd.Enabled = false;
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
            m_SqlParams = null;
            tempDT = null;
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                try
                {
                    // 0,默认,未审核 1,系统保留，禁止删除 2,审核通过 3,审核驳回 4,删除 9,推荐
                    m_SqlParams = "DELETE FROM Sys_PeopleStat WHERE [CommID]=" + objID + "";
                    DbHelperSQL.ExecuteSql(m_SqlParams);                 
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息被成功删除！", m_TargetUrl, true);
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

        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //新增
            string strErr = string.Empty;
            string UintName = PageValidate.GetTrim(this.txtUintName.Text);
            string CZRenKou = PageValidate.GetTrim(this.txtCZRenKou.Text);
            string YLFunNv = PageValidate.GetTrim(this.txtYLFunNv.Text);
            string YHFunNv = PageValidate.GetTrim(this.txtYHFunNv.Text);
            string BYRenShu = PageValidate.GetTrim(this.txtBYRenShu.Text);
            string CSRenKou = PageValidate.GetTrim(this.txtCSRenKou.Text);
            string SWRenKou = PageValidate.GetTrim(this.txtSWRenKou.Text);
            string LCRenKou = PageValidate.GetTrim(this.txtLCRenKou.Text);
            string LRRenKou = PageValidate.GetTrim(this.txtLRRenKou.Text);
            string Remarks = PageValidate.Encode(this.txtRemarks.Text);
            if (String.IsNullOrEmpty(UintName))
            {
                strErr += "单位名称不能为空！\\n";
            }
            if (String.IsNullOrEmpty(CZRenKou))
            {
                strErr += "期末常住人口不能为空！\\n";
            }
            if (String.IsNullOrEmpty(YLFunNv))
            {
                strErr += "期末育龄妇女人数不能为空！\\n";
            }
            if (String.IsNullOrEmpty(YHFunNv))
            {
                strErr += "期末已婚育龄妇女数不能为空！\\n";
            }
            if (String.IsNullOrEmpty(BYRenShu))
            {
                strErr += "采取各种避孕措施人数不能为空！\\n";
            }
            if (String.IsNullOrEmpty(CSRenKou))
            {
                strErr += "出生人数不能为空！\\n";
            }
            if (String.IsNullOrEmpty(SWRenKou))
            {
                strErr += "死亡人数不能为空！\\n";
            }
            if (String.IsNullOrEmpty(LCRenKou))
            {
                strErr += "流出人口不能为空！\\n";
            }
            if (String.IsNullOrEmpty(LRRenKou))
            {
                strErr += "流入人口不能为空！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            if (String.IsNullOrEmpty(m_UserID))
            {
                MessageBox.Show(this, "操作超时,请退出系统重新登录后再试！");
                return;
            }
            m_SqlParams = null;

            if (m_ActionName == "add")
            {
                try
                {
                    m_SqlParams = "INSERT INTO [Sys_PeopleStat]([UintName], [CZRenKou], [YLFunNv],";
                    m_SqlParams += "[YHFunNv], [BYRenShu], [CSRenKou], [SWRenKou], ";
                    m_SqlParams += "[LCRenKou], [LRRenKou], [Remarks], [UserID])";
                    m_SqlParams += "VALUES('" + UintName + "','" + CZRenKou + "','" + YLFunNv + "',";
                    m_SqlParams += "'" + YHFunNv + "','" + BYRenShu + "','" + CSRenKou + "','" + SWRenKou + "',";
                    m_SqlParams += "'" + LCRenKou + "','" + LRRenKou + "','" + Remarks + "'," + this.m_UserID + ")";
                   
                    m_SqlParams += "SELECT SCOPE_IDENTITY()";
                    string objID = DbHelperSQL.GetSingle(m_SqlParams).ToString();

                    MessageBox.ShowAndRedirect(this.Page, "操作提示：[" + UintName + "]人口基本信息发布成功！", txtUrlParams.Value, true);

                }
                catch (Exception ex) { MessageBox.Show(this, ex.Message); }
            }
            else if (m_ActionName == "edit")
            {
                try
                {
                    m_SqlParams = "UPDATE Sys_PeopleStat SET [UintName]='" + UintName + "' , [CZRenKou]='" + CZRenKou + "' , [YLFunNv]='" + YLFunNv + "' ,[YHFunNv]='" + YHFunNv + "' , [BYRenShu]='" + BYRenShu + "' , [CSRenKou]='" + CSRenKou + "' , [SWRenKou]='" + SWRenKou + "' ,[LCRenKou]='" + LCRenKou + "' , [LRRenKou]='" + LRRenKou + "' , [Remarks]='" + Remarks + "'  WHERE CommID=" + m_ObjID;

                    DbHelperSQL.ExecuteSql(m_SqlParams);
                     MessageBox.ShowAndRedirect(this.Page, "操作提示：[" + UintName + "]人口基本信息编辑成功！", m_TargetUrl, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                    return;
                }
                Response.Redirect(txtUrlParams.Value);
            }
        }
    }
}
