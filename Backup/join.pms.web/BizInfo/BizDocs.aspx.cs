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
    public partial class BizDocs : System.Web.UI.Page
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


        private string m_PersonIDA;
        private string m_PersonIDB;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                GetBizCategoryLicense();
                this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">管理首页</a> &gt;&gt; 业务办理 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + m_NavTitle + "</a> &gt;&gt; 上传附件：";
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

        #region
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
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);
            m_PersonIDA = PageValidate.GetFilterSQL(Request.QueryString["PersonIDA"]);
            m_PersonIDB = PageValidate.GetFilterSQL(Request.QueryString["PersonIDB"]);
            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                m_NavTitle = HttpUtility.UrlDecode(StringProcess.AnalysisParas(m_SourceUrlDec, "FuncNa"));
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        #endregion

        #region 获取该项业务所需证件
        /// <summary>
        /// 
        /// </summary>
        private void GetBizCategoryLicense()
        {
            try
            {
                Biz_Categories bizCateg = new Biz_Categories();
                this.LiteralBizCategoryLicense.Text = bizCateg.GetBizCategoryLicense(this.m_FuncCode, this.m_ObjID, "1");

                this.txtRegAreaCodeA.Value = bizCateg.RegAreaCodeA;
                this.txtRegAreaCodeB.Value = bizCateg.RegAreaCodeB;
                if (bizCateg.IsInnerArea) { this.txtIsInnerArea.Value = "1"; }
                this.txtBizCNum.Value = bizCateg.BizCNum;
                this.txtBizGNum.Value = bizCateg.BizGNum;
                bizCateg = null;

                //保持高拍仪上传文件路径存在
                string webSvrsPath = System.Configuration.ConfigurationManager.AppSettings["SvrsWebPath"];
                string fullPath = "/Files/GpyFile/" + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) + "/" + DateTime.Now.Month.ToString("D2", CultureInfo.InvariantCulture) + "/";
                if (!Directory.Exists(webSvrsPath + fullPath)) Directory.CreateDirectory(webSvrsPath + fullPath);
            }
            catch { }
        }
        #endregion

        #region 提交附件信息
        /// <summary>
        /// 提交附件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string Comments = "", DocsName = string.Empty;
            string incDocsID = string.Empty;
            string bizGNum = this.txtBizGNum.Value;
            string personPhotos = string.Empty;//申请表上的证照
            for (int i = 0; i < int.Parse(bizGNum); i++)
            {
                string DocsID = Request["txtDocsID" + i];
                string cbBiz = Request["cbBiz" + i];
                string LicenseType = Request["txtType" + i];
                DocsName = HttpUtility.UrlDecode(Request["txtDocsName" + i]);
                if (!String.IsNullOrEmpty(DocsID))
                {
                    incDocsID += DocsID;
                    if (LicenseType == "5") { personPhotos = CommPage.GetSingleVal("SELECT DocsPath FROM BIZ_Docs WHERE CommID=" + DocsID.Substring(0, DocsID.Length - 1)); }
                }
                else
                {
                    if (LicenseType == "0")
                    {
                        //if (string.IsNullOrEmpty(DocsID)) { strErr += "请上传" + DocsName + "证件！\\n"; }
                    }

                }

                //if (cbBiz == "on" && String.IsNullOrEmpty(DocsID)) { Comments += HttpUtility.UrlDecode(Request["txtDocsName" + i]) + "<br/>"; }
                if (String.IsNullOrEmpty(DocsID)) { Comments += DocsName + "<br/>"; }
            }
            string IsInnerArea = this.txtIsInnerArea.Value;
            string DocsIDIs = Request["txtDocsIDIs"];

            if (IsInnerArea == "1")
            {

                if (!string.IsNullOrEmpty(DocsIDIs))
                {
                    incDocsID = incDocsID + DocsIDIs;
                }
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            try
            {
                if (!string.IsNullOrEmpty(incDocsID))
                {
                    //BIZ_Docs bizDocs = new BIZ_Docs();
                    //bizDocs.BizID = this.m_ObjID;
                    //bizDocs.PersonID = this.m_PersonIDA;
                    //bizDocs.SysNo = "0";
                    //if (IsInnerArea == "1")
                    //{
                    //    if (!string.IsNullOrEmpty(DocsIDIs))
                    //    {
                    //        bizDocs.IsInnerArea = "1";
                    //        incDocsID = incDocsID + DocsIDIs;
                    //    }
                    //}
                    //else
                    //{
                    //    bizDocs.IsInnerArea = "0";
                    //}
                    incDocsID = incDocsID.Substring(0, incDocsID.Length - 1);
                    //string execuN = bizDocs.IsBizDocsNeedUpdate(incDocsID);
                    m_SqlParams = "UPDATE BIZ_Docs SET BizID=" + m_ObjID + ",PersonID = " + m_PersonIDA + "   WHERE CommID IN (" + incDocsID + ")";


                    // if (int.Parse(execuN) > 0 && IsInnerArea == "1")
                    if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                    {
                        if (IsInnerArea == "1")
                        {
                            string RegAreaCodeA = this.txtRegAreaCodeA.Value;
                            string RegAreaCodeB = this.txtRegAreaCodeB.Value;
                            //if (this.m_FuncCode == "0104" && !string.IsNullOrEmpty(RegAreaCodeA) && !string.IsNullOrEmpty(RegAreaCodeB))
                            //{
                            //    //独子证 双方办理 特殊处理
                            //    string SelAreaCode = this.txtAreaCode.Value;
                            //    if (RegAreaCodeA == SelAreaCode)
                            //    {
                            //        //申请人是男方
                            //        if (Biz_Categories.IsThisAreaCode(RegAreaCodeA)) { Biz_Categories.UpdateBizWorkFlows(this.m_ObjID, this.m_FuncCode, "1", DocsIDIs); }
                            //        if (Biz_Categories.IsThisAreaCode(RegAreaCodeB)) { Biz_Categories.UpdateBizWorkFlows(this.m_ObjID, this.m_FuncCode, "0", DocsIDIs); }
                            //    }
                            //    else
                            //    {
                            //        //申请人是女方
                            //        if (Biz_Categories.IsThisAreaCode(RegAreaCodeA)) { Biz_Categories.UpdateBizWorkFlows(this.m_ObjID, this.m_FuncCode, "0", DocsIDIs); }
                            //        if (Biz_Categories.IsThisAreaCode(RegAreaCodeB)) { Biz_Categories.UpdateBizWorkFlows(this.m_ObjID, this.m_FuncCode, "1", DocsIDIs); }
                            //    }
                            //}
                            //else
                            //{
                            if (Biz_Categories.IsThisAreaCode(RegAreaCodeA)) { Biz_Categories.UpdateBizWorkFlows(this.m_ObjID, this.m_FuncCode, "0", DocsIDIs); }
                            if (Biz_Categories.IsThisAreaCode(RegAreaCodeB)) { Biz_Categories.UpdateBizWorkFlows(this.m_ObjID, this.m_FuncCode, "1", DocsIDIs); }
                            //}
                        }
                    }
                }
                //BIZ_Contents m_BizC = new BIZ_Contents();
                //m_BizC.BizID = this.m_ObjID;
                //m_BizC.Comments = Comments;
                //m_BizC.UpdateAttribs();
                //m_BizC = null;
                string sql = string.Empty;
                if (!String.IsNullOrEmpty(personPhotos)) { sql = " ,PersonPhotos='" + personPhotos + "' "; }

                DbHelperSQL.ExecuteSql("UPDATE BIZ_Contents SET Comments='" + Comments + "',Attribs=6   " + sql + " WHERE BizID=" + m_ObjID);

                CommPage.SetBizLog(m_ObjID, m_UserID, "业务证照提交", "管理员用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 进行了《" + this.m_NavTitle + "》证照提交操作");

                //BIZ_Common.SendMsgToPerson(m_ObjID, "0",false); //短讯发送置前，插入信息后发送

                MessageBox.ShowAndRedirect(this.Page, "操作提示：<" + this.m_NavTitle + ">基础信息提交成功，完成预约！", m_TargetUrl, true, true);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                //Response.Write(" <script>alert('操作失败：" + ex.Message + "') ;</script>");
                return;
            }
        }
        #endregion


    }
}

