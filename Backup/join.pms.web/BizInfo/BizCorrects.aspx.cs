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
    public partial class BizCorrects : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;

        private string m_BizID;
        private string m_FlowIDdec;
        private string m_FlowID;
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

        #region 身份验证及参数过滤
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
            m_BizID = PageValidate.GetFilterSQL(Request.QueryString["BizID"]);
            m_FlowIDdec = PageValidate.GetFilterSQL(Request.QueryString["FlowID"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName) && !string.IsNullOrEmpty(m_BizID) && PageValidate.IsNumber(m_BizID) && !string.IsNullOrEmpty(m_FlowIDdec))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                //取得最后一个驳回的流程ID
                m_FlowID = DESEncrypt.Decrypt(m_FlowIDdec);
                if (m_FlowID.IndexOf(",") > 0) m_FlowID = m_FlowID.Substring(m_FlowID.IndexOf(",") + 1);

                // "/BizInfo/BizCorrects.apsx?action=bz&sourceUrl=" + m_SourceUrl + "&BizID=" + m_ObjID + "&FlowID=" + DESEncrypt.Encrypt(updateID);
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
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
            switch (m_ActionName)
            {
                case "bz":
                    funcName = "确定补正材料";
                    ShowBzInfo(m_BizID, m_FlowID);
                    break;
                case "view": // 查看
                    funcName = "查看内容";
                    //ShowModInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "起始页  &gt;&gt; " + CommPage.GetAllBizName(m_FuncCode) + " &gt;" + funcName + "：";
        }


        /// <summary>
        /// 显示待选择信息
        /// </summary>
        /// <param name="objID"></param>
        private void ShowBzInfo(string bizID, string flowID)
        {
            string attribs = string.Empty;
            StringBuilder s = new StringBuilder();
            SqlDataReader sdr = null;
            /*
0101	流动人口婚育证明办理	2	社区/村,发证部门
0102	一孩《生育服务证》审核	5	女方村/居,男方村/居,女方镇办,男方镇办,发证机关
0103	再生育《生育服务证》审核	5	女方村/居,男方村/居,女方镇办,男方镇办,发证机关
0104	《独生子女父母光荣证》审核	5	女方村/居,男方村/居,女方镇办,男方镇办,发证机关
xx   0112	婚育情况证明	2	社区/村,镇办 
             */
            try
            {
                int i = 0;
                string licenseName = string.Empty;
                string bizCode = CommPage.GetSingleVal("SELECT BizCode FROM BIZ_Contents WHERE BizID=" + bizID);
                string sqlParams = "SELECT [BizCode], [LicenseName], [LicenseType] FROM BIZ_CategoryLicense WHERE LicenseType!=4 AND BizCode='" + bizCode + "' order by LicenseType";
                // SELECT [BizCode], [LicenseName], [LicenseType] FROM BIZ_CategoryLicense WHERE BizCode='0101' AND LicenseType!=4 order by BizCode
                this.txtBzDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                s.Append("<table width=\"880\" border=\"0\"  cellpadding=\"3\" cellspacing=\"1\">");
                s.Append("<tr class=\"zhengwen\">");
                s.Append("<td width=\"120\"  align=\"right\" class=\"zhengwenjiacu\">资料明细：</td>");
                s.Append("<td align=\"left\">");
                s.Append("<br/>");

                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        i++;
                        licenseName = sdr["LicenseName"].ToString();
                        if (i == 1) { s.Append("<input type=\"checkbox\" name=\"txtBizChkDocs\" value=\"" + licenseName + "\" />" + i + "." + licenseName); }
                        else { s.Append("<br><input type=\"checkbox\" name=\"txtBizChkDocs\" value=\"" + licenseName + "\" />" + i + "." + licenseName); }                       
                    }
                }
                sdr.Close(); sdr.Dispose();              
                s.Append("<br/>");
                s.Append("</td>");
                s.Append("</tr>");
                s.Append("</table>");

                this.LiteralSelDocs.Text = s.ToString();

                GetBizDocs(bizID);
            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                MessageBox.ShowAndRedirect(this.Page, "操作提示：选择补正材料时出现错误！", m_TargetUrl, true);
            }

        }

        /// <summary>
        /// 获取业务证明附件材料
        /// </summary>
        /// <param name="bizID"></param>
        private void GetBizDocs(string bizID)
        {
            string navUrl = "", BizComments = "", DocsType = string.Empty;
            StringBuilder s = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                BizComments = CommPage.GetSingleVal("SELECT Comments FROM BIZ_Contents WHERE BizID=" + bizID);
                if (string.IsNullOrEmpty(BizComments)) BizComments = "……";

                m_SqlParams = "SELECT DocsType,DocsPath,SourceName,DocsName,OprateDate,IsInnerArea FROM BIZ_Docs WHERE BizID=" + bizID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                s.Append("<table width=\"880\" border=\"0\"  cellpadding=\"3\" cellspacing=\"1\">");
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        navUrl = m_SvrsUrl + sdr["DocsPath"].ToString();
                        DocsType = sdr["DocsType"].ToString();

                        s.Append("<tr class=\"zhengwen\">");
                        s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">材料附件：</td>");

                        if (DocsType == ".jpg" || DocsType == ".gif" || DocsType == ".png" || DocsType == ".bmp")
                        {
                            s.Append("<td align=\"left\"><a href=\"" + navUrl + "\" rel=\"lightbox[zj]\" title=\"证照：" + sdr["DocsName"].ToString() + "\">" + sdr["DocsName"].ToString() + "(" + DateTime.Parse(sdr["OprateDate"].ToString()).ToString("yyyy/MM/dd") + ")</a></td>");
                        }
                        else
                        {
                            s.Append("<td align=\"left\"><a href=\"" + navUrl + "\" target=\"_blank\">" + sdr["DocsName"].ToString() + "</a></td>");
                        }
                        s.Append("</tr>");
                    }
                    sdr.Close();
                    s.Append("<tr class=\"zhengwen\">");
                    s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">现场提交：</td>");
                    s.Append("<td align=\"left\">" + BizComments + "</td>");
                    s.Append("</tr>");
                }
                else
                {
                    s.Append("<tr class=\"zhengwen\">");
                    s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">材料附件：</td>");
                    s.Append("<td align=\"left\">当前业务申请事项没有提交任何电子证照材料……</td>");
                    s.Append("</tr>");
                    s.Append("<tr class=\"zhengwen\">");
                    s.Append("<td width=\"120\"  height=\"25\" align=\"right\" class=\"zhengwenjiacu\">现场提交：</td>");
                    s.Append("<td align=\"left\">" + BizComments + "</td>");
                    s.Append("</tr>");
                }
                s.Append("</table>");
            }
            catch
            {
                if (sdr != null) sdr.Close();
                MessageBox.ShowAndRedirect(this.Page, "操作提示：获取证明材料时出现错误！", m_TargetUrl, true);

            }
            this.LiteralDocs.Text = s.ToString();
        }

        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // txtBizChkDocs
            string strErr = string.Empty;
            string tmpDocs = PageValidate.GetTrim(this.txtOtherDocs.Text);
            string bizDate = this.txtBzDate.Value;
            // 获取选择的项目
            string replyKey = string.Empty;
            string replyTxt = string.Empty;
            for (int i = 0; i < Request.Form.Count; i++)
            {
                if (Request.Form.GetKey(i).IndexOf("xtBizChkDocs") > 0)
                {
                    replyKey += Request.Form.GetKey(i) + ",";
                    replyTxt += PageValidate.Encode(Request.Form[i].ToString()) + ",";
                }
            }
            //if (!string.IsNullOrEmpty(replyTxt)) replyTxt = replyTxt.Substring(0, replyTxt.Length - 1);
            if (!string.IsNullOrEmpty(replyTxt))
            {
                if (!string.IsNullOrEmpty(tmpDocs)) { replyTxt += tmpDocs; }
                else { replyTxt = replyTxt.Substring(0, replyTxt.Length - 1); }
            }
            else
            {
                if (!string.IsNullOrEmpty(tmpDocs)) { replyTxt += tmpDocs; }
            }

            //if (string.IsNullOrEmpty(replyTxt))
            //{
            //    MessageBox.ShowAndRedirect(this.Page, "操作提示：请选择需要补充的证明材料项目，谢谢！", "/BizInfo/BizCorrects.aspx?action=bz&sourceUrl=" + m_SourceUrl + "&BizID=" + m_BizID + "&FlowID=" + m_FlowIDdec, true);
            //    return;
            //}

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            try
            {

                m_SqlParams = "UPDATE BIZ_WorkFlows SET ";
                m_SqlParams += "CommMemo ='" + replyTxt + "',CertificateDateEnd='" + bizDate + "',Attribs=0 ";
                m_SqlParams += "WHERE CommID =" + m_FlowID;

                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：补正材料操作成功！", m_TargetUrl, true);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：业务补正材料操作失败！", m_TargetUrl, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
        }
    }
}


