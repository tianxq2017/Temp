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

namespace AreWeb.CertificateInOne.PopInfo
{
    public partial class chatdata030103 : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                this.txtReportDate.Value = DateTime.Now.ToString("yyyy-MM-dd");    //数据日期
                this.dd_Year.SelectedValue = DateTime.Now.Year.ToString();   //年份
                this.dd_Month.SelectedValue = DateTime.Now.Month.ToString();    //月份
                SetPageStyle(m_UserID);
                SetOpratetionAction(m_NavTitle);
            }
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
                    funcName = "编辑";
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // 删除
                    funcName = "删除";
                    DelInfo(m_ObjID);
                    break;
                case "view": // 查看
                    funcName = "查看内容";
                    ShowModInfo(m_ObjID);
                    break;
                case "audit": // 审核
                    funcName = "审核内容";
                    AuditInfo(m_ObjID);
                    break;
                case "pub": // 公开
                    funcName = "公开";
                    PubInfo(m_ObjID);
                    break;
                default:
                    Response.Write(" <script>alert('操作失败：参数错误！') ;window.location.href='" + m_TargetUrl + "'</script>");
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; 人口计生报表 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }

        /// <summary>
        /// 审核信息
        /// </summary>
        /// <param name="objID"></param>
        private void AuditInfo(string objID)
        {
            try
            {
                string cmsAttrib = string.Empty;
                if (AreWeb.CertificateInOne.Biz.CommPage.CheckAuditFlag(objID, ref cmsAttrib))
                {
                    m_SqlParams = "UPDATE PIS_BaseInfo SET AuditFlag=" + cmsAttrib + " WHERE CommID IN(" + objID + ") ";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    Response.Write(" <script>alert('操作成功：您选择的信息审核/取消审核操作成功！') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
                else
                {
                    Response.Write(" <script>alert('操作失败：您选择的信息包含尚未通过审核或者不是同一类信息！') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
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
                if (AreWeb.CertificateInOne.Biz.CommPage.CheckDelFlag(objID))
                {
                    m_SqlParams = "DELETE FROM PIS_BaseInfo WHERE CommID IN(" + objID + ")";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    Response.Write(" <script>alert('操作成功：您选择的信息删除成功！') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
                else
                {
                    Response.Write(" <script>alert('操作失败：您选择的信息包含通过审核的信息；审核公开的信息不允许删除！') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
        }

        /// <summary>
        /// 公开信息
        /// </summary>
        /// <param name="objID"></param>
        private void PubInfo(string objID)
        {
            try
            {
                // CmsAttrib:0 默认;1 审核; 3 屏蔽; 4 删除; 9 公开
                string cmsAttrib = string.Empty;
                if (AreWeb.CertificateInOne.Biz.CommPage.CheckPubFlag(objID, ref cmsAttrib))
                {
                    m_SqlParams = "UPDATE PIS_BaseInfo SET AuditFlag=" + cmsAttrib + " WHERE CommID IN(" + objID + ") ";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    Response.Write(" <script>alert('操作成功：您选择的信息审核/取消审核操作成功！') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
                else
                {
                    Response.Write(" <script>alert('操作失败：您选择的信息包含尚未通过审核或者不是同一类信息！') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
        }

        /// <summary>
        /// 修改 UcAreaSe08
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            bool isEdit = true;
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT * FROM [PIS_BaseInfo] WHERE CommID=" + m_ObjID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    if (sdr["AuditFlag"].ToString() != "0" && m_ActionName == "edit")
                    {
                        isEdit = false;
                        break;
                    }
                    this.UcDataAreaSel1.SetAreaName(PageValidate.GetTrim(sdr["AreaName"].ToString()));
                    this.UcDataAreaSel1.SetAreaCode(PageValidate.GetTrim(sdr["AreaCode"].ToString()));
                    this.txtReportDate.Value = DateTime.Parse(sdr["ReportDate"].ToString()).ToString("yyyy-MM-dd");

                    this.dd_Year.Text = PageValidate.GetTrim(sdr["Fileds01"].ToString());   //年份
                    this.dd_Month.Text = PageValidate.GetTrim(sdr["Fileds02"].ToString());  //月份

                    this.txtFileds03.Text = PageValidate.GetTrim(sdr["Fileds03"].ToString());  //组别
                    this.txtFileds04.Text = PageValidate.GetTrim(sdr["Fileds04"].ToString());  //妇女姓名
                    this.txtFileds05.Text = PageValidate.GetTrim(sdr["Fileds05"].ToString());  //身份证号
                    this.txtFileds06.Text = PageValidate.GetTrim(sdr["Fileds06"].ToString());  //丈夫姓名
                    this.txtFileds07.Text = PageValidate.GetTrim(sdr["Fileds07"].ToString());   //孩次
                    this.txtFileds08.Text = PageValidate.GetTrim(sdr["Fileds08"].ToString());   //孩子姓名
                    this.DDFileds09.SelectedValue = PageValidate.GetTrim(sdr["Fileds09"].ToString());  //性别
                    this.txtFileds10.Value = PageValidate.GetTrim(sdr["Fileds10"].ToString());  //出生日期
                    this.txtFileds11.Text = PageValidate.GetTrim(sdr["Fileds11"].ToString());   //政策属性
                    this.txtFileds12.Text = PageValidate.GetTrim(sdr["Fileds12"].ToString());  //健康状况
                    this.txtFileds13.Text = PageValidate.GetTrim(sdr["Fileds13"].ToString());  //血缘关系
                    this.txtFileds14.Text = PageValidate.GetTrim(sdr["Fileds14"].ToString());   //负责人
                    this.txtFileds15.Text = PageValidate.GetTrim(sdr["Fileds15"].ToString());   //填表人

                    if (m_ActionName == "view") this.btnAdd.Visible = false;
                }
                sdr.Close();
                // 是否可编辑 SetAreaCode
                if (!isEdit)
                {
                    Response.Write(" <script>alert('操作失败：您选择的信息包含通过审核、修正或公开的信息；该信息不允许编辑！') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
            }
            catch { if (sdr != null) sdr.Close(); }


        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;

            string ReportDate = PageValidate.GetTrim(this.txtReportDate.Value);    //添加日期 
            string AreaCode = PageValidate.GetTrim(this.UcDataAreaSel1.GetAreaCode());    //地区编码
            string AreaName = PageValidate.GetTrim(this.UcDataAreaSel1.GetAreaName());    //地区

            string Fileds01 = PageValidate.GetTrim(this.dd_Year.SelectedValue);    //年份
            string Fileds02 = PageValidate.GetTrim(this.dd_Month.SelectedValue);   //月份
            string Fileds03 = PageValidate.GetTrim(this.txtFileds03.Text);   //组别
            string Fileds04 = PageValidate.GetTrim(this.txtFileds04.Text);  
            string Fileds05 = PageValidate.GetTrim(this.txtFileds05.Text);   
            string Fileds06 = PageValidate.GetTrim(this.txtFileds06.Text);   
            string Fileds07 = PageValidate.GetTrim(this.txtFileds07.Text);   
            string Fileds08 = PageValidate.GetTrim(this.txtFileds08.Text);
            string DDFileds09 = PageValidate.GetTrim(this.DDFileds09.SelectedValue);
            string Fileds10 = PageValidate.GetTrim(this.txtFileds10.Value);  
            string Fileds11 = PageValidate.GetTrim(this.txtFileds11.Text);
            string Fileds12 = PageValidate.GetTrim(this.txtFileds12.Text);  
            string Fileds13 = PageValidate.GetTrim(this.txtFileds13.Text);  
            string Fileds14 = PageValidate.GetTrim(this.txtFileds14.Text);  
            string Fileds15 = PageValidate.GetTrim(this.txtFileds15.Text);  
            
            if (String.IsNullOrEmpty(AreaCode) || String.IsNullOrEmpty(AreaName)) { strErr += "请选择请选择地区！\\n"; }
            if (String.IsNullOrEmpty(Fileds01)) { strErr += "请选择年份！\\n"; }
            if (String.IsNullOrEmpty(Fileds02)) { strErr += "请选择月份！\\n"; }
            if (String.IsNullOrEmpty(Fileds03)) { strErr += "请输入组别！\\n"; }
            if (String.IsNullOrEmpty(Fileds04)) { strErr += "请输入妇女姓名！\\n"; }
            if (String.IsNullOrEmpty(Fileds05)) { strErr += "请输入身份证号码！\\n"; }
            if (String.IsNullOrEmpty(Fileds06)) { strErr += "请输入丈夫姓名！\\n"; }
            if (String.IsNullOrEmpty(Fileds07)) { strErr += "请输入孩次！\\n"; }
            if (String.IsNullOrEmpty(Fileds08)) { strErr += "请输入孩子姓名！\\n"; }
            if (String.IsNullOrEmpty(DDFileds09)) { strErr += "请选择性别！\\n"; }
            if (String.IsNullOrEmpty(Fileds10)) { strErr += "请选择出生日期\\n"; }
            if (String.IsNullOrEmpty(Fileds11)) { strErr += "请输入政策属性！\\n"; }
            if (String.IsNullOrEmpty(Fileds12)) { strErr += "请输入健康状况！\\n"; }
            if (String.IsNullOrEmpty(Fileds13)) { strErr += "请输入血缘关系！\\n"; }
            if (String.IsNullOrEmpty(Fileds14)) { strErr += "请输入负责人！\\n"; }
            if (String.IsNullOrEmpty(Fileds15)) { strErr += "请输入填表人！\\n"; }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            if (m_ActionName == "add")
            {
                m_SqlParams = "INSERT INTO [PIS_BaseInfo](";
                m_SqlParams += "OprateUserID,FuncNo,";
                m_SqlParams += "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,";
                m_SqlParams += "ReportDate,AreaCode,AreaName";
                m_SqlParams += ") VALUES(";
                m_SqlParams += "" + m_UserID + ",'" + m_FuncCode + "',";
                m_SqlParams += "'" + Fileds01 + "','" + Fileds02 + "','" + Fileds03 + "','" + Fileds04 + "','" + Fileds05 + "','" + Fileds06 + "','" + Fileds07 + "','" + Fileds08 + "','" + DDFileds09 + "','" + Fileds10 + "','" + Fileds11 + "','" + Fileds12 + "','" + Fileds13 + "','" + Fileds14 + "','" + Fileds15 + "',";
                m_SqlParams += "'" + ReportDate + "','" + AreaCode + "','" + AreaName + "'";
                m_SqlParams += ")";
            }
            else if (m_ActionName == "edit")
            {
                m_SqlParams = "UPDATE PIS_BaseInfo SET ";
                m_SqlParams += "Fileds01 ='" + Fileds01 + "',Fileds02 ='" + Fileds02 + "',Fileds03 ='" + Fileds03 + "',Fileds04 ='" + Fileds04 + "',Fileds05 ='" + Fileds05 + "',";
                m_SqlParams += "Fileds06 ='" + Fileds06 + "',Fileds07 ='" + Fileds07 + "',Fileds08 ='" + Fileds08 + "',Fileds09 ='" + DDFileds09 + "',Fileds10 ='" + Fileds10 + "',Fileds11 ='" + Fileds11 + "',";
                m_SqlParams += "Fileds12 ='" + Fileds12 + "',Fileds13 ='" + Fileds13 + "',Fileds14 ='" + Fileds14 + "',Fileds15 ='" + Fileds15 + "',";
                m_SqlParams += "ReportDate ='" + ReportDate + "',AreaCode ='" + AreaCode + "',AreaName ='" + AreaName + "' ";
                m_SqlParams += " WHERE CommID=" + m_ObjID;
            }
            try
            {
                DbHelperSQL.ExecuteSql(m_SqlParams);
                Response.Write(" <script>alert('人口动态信息报告单<二>（生育情况）数据操作成功！') ;window.location.href='" + m_TargetUrl + "'</script>");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
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
                HttpCookie loginCookie = Request.Cookies["AKS_PISS_USER_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["AKS_PISS_USERID"] != null && !String.IsNullOrEmpty(Session["AKS_PISS_USERID"].ToString())) { returnVa = true; m_UserID = Session["AKS_PISS_USERID"].ToString(); }
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
            m_ActionName = Request.QueryString["action"];
            m_SourceUrl = Request.QueryString["sourceUrl"];
            m_ObjID = Request.QueryString["k"];

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                m_NavTitle = "人口动态信息报告单<二>（生育情况）";
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = Request.QueryString["RID"];
        }
    }
}
