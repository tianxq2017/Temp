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

using join.pms.dal;
using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.web.BizInfo
{
    public partial class BizCategories : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号

        private string m_SqlParams;
        protected string m_TargetUrl;

        private string m_NavTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                if (String.IsNullOrEmpty(m_SourceUrl) || String.IsNullOrEmpty(m_ActionName))
                {
                    Response.Write("非法访问，操作被终止！");
                    Response.End();
                }
                else
                {
                    SetOpratetionAction(m_NavTitle);
                }
            }
        }
        #region

        /// <summary>
        /// 验证接受的参数
        /// </summary>
        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);
            if (!string.IsNullOrEmpty(m_ActionName) && m_ActionName == "view" && string.IsNullOrEmpty(m_ObjID))
            {
                m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
            }

            if (!String.IsNullOrEmpty(m_SourceUrl))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");

                m_NavTitle = "业务事项信息";
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
                case "edit": // 编辑
                    funcName = "修改";
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // 删除
                    funcName = "";
                    DelInfo(m_ObjID);
                    break;
                case "audit": // 审核
                    funcName = "";
                    Audit(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx.aspx\">起始页</a> &gt;&gt;基础设置 &gt;&gt;" + oprateName + " ：";
        }
        //Attribs: 0默认正常;1隐藏
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="objID"></param>
        private void Audit(string objID)
        {
            if (PageValidate.IsNumber(objID))
            {
                try
                {
                    //Attribs: 0默认正常;1隐藏
                    string cmsAttrib = DbHelperSQL.GetSingle("SELECT Attribs FROM BIZ_Categories WHERE BizCode='" + objID + "'").ToString();
                    if (!string.IsNullOrEmpty(cmsAttrib))
                    {
                        if (cmsAttrib == "0") { m_SqlParams = "UPDATE BIZ_Categories SET Attribs=1 WHERE BizCode='" + objID + "'"; }
                        else if (cmsAttrib == "1") { m_SqlParams = "UPDATE BIZ_Categories SET Attribs=0 WHERE BizCode='" + objID + "'"; }
                        else
                        {
                            MessageBox.ShowAndRedirect(this.Page, "操作提示：无法完成操作！", m_TargetUrl, true);
                            return;
                        }

                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：您选择的信息隐藏/取消隐藏操作成功！", m_TargetUrl, true);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：意外错误！", m_TargetUrl, true);
                    }
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

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string bizCode)
        {
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT BizNameAB,BizNameFull,BizStep,BizStepNames,BizTemplates FROM BIZ_Categories WHERE BizCode='" + bizCode + "'";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    //this.txtBizNameAB.Text = sdr["BizNameAB"].ToString();
                    //this.txtBizNameFull.Text = sdr["BizNameFull"].ToString();
                    //this.txtBizStep.Text = sdr["BizStep"].ToString();
                    //this.txtBizStepNames.Text = sdr["BizStepNames"].ToString();
                    this.LiteralBiz.Text = sdr["BizNameFull"].ToString();
                    this.objCKeditor.Text = PageValidate.Decode(sdr["BizTemplates"].ToString());

                   // GetCmsDocs(m_ObjID); // 获取内容附件
                }
                sdr.Close(); sdr.Dispose();
            }
            catch
            {
                if (sdr != null) sdr.Close(); sdr.Dispose();
            }
        }
        
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            if (PageValidate.IsNumber(objID))
            {
                try
                {
                    //Attribs: 0默认正常;1隐藏
                    if (!string.IsNullOrEmpty(objID))
                    {
                        m_SqlParams = "UPDATE BIZ_Categories SET Attribs=1 WHERE BizCode='" + objID + "'";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        MessageBox.ShowAndRedirect(this.Page, "操作提示：删除成功！", m_TargetUrl, true);
                    }
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
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="objID"></param>
        private void BatchDelInfo(string objID)
        {
            try
            {
                m_SqlParams = "UPDATE BIZ_Categories SET Attribs=1 WHERE BizCode='" + objID + "'";
                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "操作提示：删除成功！", m_TargetUrl, true);
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
        }

        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            //string BizNameAB = PageValidate.GetTrim(this.txtBizNameAB.Text);
            //string BizNameFull = PageValidate.GetTrim(this.txtBizNameFull.Text);
            //string BizStep = PageValidate.GetTrim(this.txtBizStep.Text);
            //string BizStepNames = PageValidate.GetTrim(this.txtBizStepNames.Text);
            string BizTemplates = PageValidate.Encode(CommBiz.GetTrim(this.objCKeditor.Text));
            if (string.IsNullOrEmpty(BizTemplates)) strErr += "请输入业务内容！\\n";
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            try
            {
                if (m_ActionName == "add")
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：业务事项规定为预置操作，无需添加！", m_TargetUrl, true);
                }
                else
                {
                    m_SqlParams = "UPDATE BIZ_Categories SET BizTemplates='" + BizTemplates + "' WHERE BizCode='" + m_ObjID + "'";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：业务事项编辑成功！", m_TargetUrl, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }

        }

    }
}

