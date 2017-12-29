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
using System.IO;
using System.Text;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;
namespace join.pms.web.BizInfo
{
    public partial class BasisDocument : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号
        private string m_UserName;

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;
        private string m_AreaCode;
        private string m_AreaName;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetOpratetionAction(m_NavTitle);
                //得到项目的名称
                try
                {
                    this.ltr_FileName.Text = DbHelperSQL.GetSingle("select P_Name from BIZ_ProjectInfo where P_ID = " + m_ObjID).ToString();
                }
                catch
                {
                    this.ltr_FileName.Text = "";
                }
            }
        }

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "";// DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        #region 身份验证
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
            else { GetUserInfo(); }
        }
        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        private void GetUserInfo()
        {
            SqlDataReader sdr = null;
            try
            {
                string sqlParams = "SELECT UserAccount,UserName,UserAreaCode FROM USER_BaseInfo WHERE UserID=" + m_UserID;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        m_UserName = sdr["UserAccount"].ToString() + "(" + sdr["UserName"].ToString() + ")";
                        //m_AreaCode = sdr["UserAreaCode"].ToString();
                        //m_AreaName = GetAreaName(m_AreaCode, "0");
                    }
                }
            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }
        }

        /// <summary>
        /// 验证接受的参数
        /// </summary>
        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetTrim(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetTrim(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetTrim(Request.QueryString["k"]);

            m_AreaCode = PageValidate.GetTrim(Request.QueryString["x"]);
            m_AreaName = BIZ_Common.GetAreaName(m_AreaCode, "0");
            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "/UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                //m_NavTitle = CommPage.GetSingleVal("SELECT BizNameFull FROM BIZ_Categories WHERE BizCode='" + m_FuncCode + "'");
                m_NavTitle = "资金信息采集";
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
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
                    funcName = "新增";
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">管理首页</a> &gt;&gt; 资金管理 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }

        #endregion

        #region 提交
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string CmsPhotos = "";
            string txtDocsName = Request["txtDocsName"];
            if (txtDocsName != "")
            {
                CmsPhotos = txtDocsName.Substring(txtDocsName.IndexOf('.'), 4);
                if (CmsPhotos == ".gif" || CmsPhotos == ".jpg" || CmsPhotos == ".jpe" || CmsPhotos == ".bmp" || CmsPhotos == ".png")
                {
                    if (CmsPhotos == ".jpe")
                    {
                        CmsPhotos = ".jpeg";
                    }
                }
                else
                {
                    CmsPhotos = "";
                }
            }
            string incDocsID = this.txtDocsID.Value;
            string strErr = string.Empty;
            
            string P_ID = m_ObjID;
            string B_Province_Symbol = this.txt_B_Province_Symbol.Text;
            string B_Province_Money_Symbol = this.txt_B_Province_Money_Symbol.Text;
            string B_City_Symbol = this.txt_B_City_Symbol.Text;
            string B_City_Money_Symbol = this.txt_B_City_Money_Symbol.Text;
            string B_IssueDate_1 = this.txt_B_IssueDate_1.Value;
            string B_IssueDamount_1 = this.txt_B_IssueDamount_1.Text;
            string B_County_Symbol = this.txt_B_County_Symbol.Text;
            string B_County_Money_Symbol = this.txt_B_County_Money_Symbol.Text;
            string B_IssueDate_2 = this.txt_B_IssueDate_2.Value;
            string B_IssueDamount_2 = this.txt_B_IssueDamount_2.Text;
            string B_Remarks = this.txt_B_Remarks.Text;

            try
            {
                //插入项目信息表
                if (m_ActionName == "add")
                {
                    m_SqlParams = "INSERT INTO [BIZ_ProBasisDocument](";
                    m_SqlParams += "P_ID,B_Province_Symbol,B_Province_Money_Symbol,B_City_Symbol,B_City_Money_Symbol,B_IssueDate_1,B_IssueDamount_1,B_County_Symbol,B_County_Money_Symbol,B_IssueDate_2,B_IssueDamount_2,B_Remarks";
                    // m_SqlParams += "";
                    m_SqlParams += ") VALUES(";
                    m_SqlParams += "" + P_ID + ",'" + B_Province_Symbol + "','" + B_Province_Money_Symbol + "','" + B_City_Symbol + "','" + B_City_Money_Symbol + "','" + B_IssueDate_1 + "','" + B_IssueDamount_1 + "','" + B_County_Symbol + "','" + B_County_Money_Symbol + "','" + B_IssueDate_2 + "','" + B_IssueDamount_2 + "','" + B_Remarks + "')";
                    m_SqlParams += " SELECT SCOPE_IDENTITY()";
                    m_ObjID = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                    if (!string.IsNullOrEmpty(m_ObjID))
                    {
                        //更新上传的图片归属
                        if (!string.IsNullOrEmpty(incDocsID))
                        {
                            incDocsID = incDocsID.Substring(0, incDocsID.Length - 1);
                            m_SqlParams = "UPDATE BIZ_ProjectInfo_Docs SET P_ID=" + m_ObjID + ",UserID = " + m_UserID + "  WHERE D_ID IN (" + incDocsID + ")";
                            DbHelperSQL.ExecuteSql(m_SqlParams);
                        }
                        Response.Write(" <script>alert('操作成功：标题为[ " + this.ltr_FileName.Text + " ]的依据文件发布成功！') ;window.location.href='" + m_TargetUrl + "'</script>");
                    }
                    else
                    {
                        Response.Write(" <script>alert('操作提示：信息发布失败！') ;window.location.href='" + m_TargetUrl + "'</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
        }
        #endregion
    }
}

