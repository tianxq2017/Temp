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
    public partial class editPopInfoQYK : System.Web.UI.Page
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

        #region
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
                m_NavTitle = "全员数据-人口基本情况统计";
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = Request.QueryString["RID"];
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
                    SetReportArea("");
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
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; 人口计生局 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }

        /// <summary>
        /// 设置数据单位/行政区划单位
        /// </summary>
        /// <param name="areaCode">默认值</param>
        private void SetReportArea(string areaCode)
        {
            string siteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
            m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + siteArea + "' ORDER BY AreaCode";
            //m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + siteArea + "' UNION SELECT [DeptCode] as AreaCode, [DeptName] as AreaName FROM [USER_Department] WHERE DeptCode LIKE '02__'";
            DataTable tmpDt = new DataTable();
            tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            DDLReportArea.DataSource = tmpDt;
            DDLReportArea.DataTextField = "AreaName";
            DDLReportArea.DataValueField = "AreaCode";
            DDLReportArea.DataBind();
            DDLReportArea.Items.Insert(0, new ListItem("其他行政区", "000000000000"));
            tmpDt = null;
            if (!string.IsNullOrEmpty(areaCode))
            {
                this.DDLReportArea.SelectedValue = areaCode;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            bool isEdit = true;
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT * FROM [PIS_QYK] WHERE CommID=" + objID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    SetReportArea(PageValidate.GetTrim(sdr["AreaCode"].ToString()));
                    this.txtReportDate.Value = DateTime.Parse(sdr["ReportDate"].ToString()).ToString("yyyy-MM-dd");
                    // Fileds01 为单位
                    this.txtFileds02.Text = PageValidate.GetTrim(sdr["Fileds02"].ToString());
                    this.txtFileds03.Text = PageValidate.GetTrim(sdr["Fileds03"].ToString());
                    this.txtFileds04.Text = PageValidate.GetTrim(sdr["Fileds04"].ToString());
                    this.txtFileds05.Text = PageValidate.GetTrim(sdr["Fileds05"].ToString());
                    this.txtFileds06.Text = PageValidate.GetTrim(sdr["Fileds06"].ToString());
                    this.txtFileds07.Text = PageValidate.GetTrim(sdr["Fileds07"].ToString());
                    this.txtFileds08.Text = PageValidate.GetTrim(sdr["Fileds08"].ToString());
                    this.txtFileds09.Text = PageValidate.GetTrim(sdr["Fileds09"].ToString());
                    this.txtFileds10.Text = PageValidate.GetTrim(sdr["Fileds10"].ToString());
                    this.txtFileds11.Text = PageValidate.GetTrim(sdr["Fileds11"].ToString());
                    this.txtFileds12.Text = PageValidate.GetTrim(sdr["Fileds12"].ToString());                   

                    if (m_ActionName == "view") this.btnAdd.Visible = false;

                }
                sdr.Close();
                // 是否可编辑
                if (!isEdit)
                {
                    Response.Write(" <script>alert('操作失败：您选择的信息包含通过审核、修正或公开的信息；该信息不允许编辑！') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
            }
            catch { if (sdr != null) sdr.Close(); }
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
                    m_SqlParams = "DELETE FROM PIS_QYK WHERE CommID IN(" + objID + ")";
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
                    m_SqlParams = "UPDATE PIS_QYK SET AuditFlag=" + cmsAttrib + " WHERE CommID IN(" + objID + ") ";
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
                    m_SqlParams = "UPDATE PIS_QYK SET AuditFlag=" + cmsAttrib + " WHERE CommID IN(" + objID + ") ";
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

        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty; // txtFileds05
            
            string Fileds02 = PageValidate.GetTrim(this.txtFileds02.Text);
            string Fileds03 = PageValidate.GetTrim(this.txtFileds03.Text);
            string Fileds04 = PageValidate.GetTrim(this.txtFileds04.Text);
            string Fileds05 = PageValidate.GetTrim(this.txtFileds05.Text);
            string Fileds06 = PageValidate.GetTrim(this.txtFileds06.Text);
            string Fileds07 = PageValidate.GetTrim(this.txtFileds07.Text);
            string Fileds08 = PageValidate.GetTrim(this.txtFileds08.Text);
            string Fileds09 = PageValidate.GetTrim(this.txtFileds09.Text);
            string Fileds10 = PageValidate.GetTrim(this.txtFileds10.Text);
            string Fileds11 = PageValidate.GetTrim(this.txtFileds11.Text);
            string Fileds12 = PageValidate.GetTrim(this.txtFileds12.Text);

            string ReportDate = PageValidate.GetTrim(this.txtReportDate.Value);
            string AreaCode = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Value);
            string AreaName = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Text);
            string Fileds01 = AreaName;
            if (AreaCode == "请选择")
            {
                strErr += "请选择数据归属的行政区划！\\n";
            }

            if (String.IsNullOrEmpty(Fileds01))
            {
                strErr += "请选择单位名称！\\n";
            }
            if (String.IsNullOrEmpty(ReportDate))
            {
                strErr += "请选择数据日期！\\n";
            }
            if (String.IsNullOrEmpty(Fileds02)) { strErr += "请输入期末常住人口！\\n"; }
            if (!PageValidate.IsNumber(Fileds02)) { strErr += "期末常住人口填写错误！\\n"; }
            if (String.IsNullOrEmpty(Fileds03)) { strErr += "请输入期末育龄妇女人数！\\n"; }
            if (!PageValidate.IsNumber(Fileds03)) { strErr += "期末育龄妇女人数填写错误！\\n"; }
            if (String.IsNullOrEmpty(Fileds04)) { strErr += "请输入期末已婚育龄妇女数！\\n"; }
            if (!PageValidate.IsNumber(Fileds04)) { strErr += "期末已婚育龄妇女数填写错误！\\n"; }
            if (String.IsNullOrEmpty(Fileds05)) { strErr += "请输入0孩数！\\n"; }
            if (!PageValidate.IsNumber(Fileds05)) { strErr += "0孩数填写错误！\\n"; }
            if (String.IsNullOrEmpty(Fileds06)) { strErr += "请输入1孩数！\\n"; }
            if (!PageValidate.IsNumber(Fileds06)) { strErr += "1孩数填写错误！\\n"; }
            if (String.IsNullOrEmpty(Fileds07)) { strErr += "请输入2孩数！\\n"; }
            if (!PageValidate.IsNumber(Fileds07)) { strErr += "2孩数填写错误！\\n"; }
            if (String.IsNullOrEmpty(Fileds08)) { strErr += "请输入多孩数！\\n"; }
            if (!PageValidate.IsNumber(Fileds08)) { strErr += "多孩数填写错误！\\n"; }
            if (String.IsNullOrEmpty(Fileds09)) { strErr += "请输入出生人数！\\n"; }
            if (!PageValidate.IsNumber(Fileds09)) { strErr += "出生人数填写错误！\\n"; }
            if (String.IsNullOrEmpty(Fileds10)){ strErr += "请填写死亡人数！\\n"; }
            if (!PageValidate.IsNumber(Fileds10)) { strErr += "死亡人数填写错误！\\n"; }
            if (String.IsNullOrEmpty(Fileds11)) { strErr += "请填写年内领取独生子女证人数！\\n"; }
            if (!PageValidate.IsNumber(Fileds11)) { strErr += "年内领取独生子女证人数填写错误！\\n"; }
            if (String.IsNullOrEmpty(Fileds12)) { strErr += "请填写累计领取独生子女人数！\\n"; }
            if (!PageValidate.IsNumber(Fileds12)) { strErr += "累计领取独生子女人数填写错误！\\n"; }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            // CommID,OprateUserID,OprateDate,ReportDate,FuncNo,UnitNo,
            if (m_ActionName == "add")
            {
                m_SqlParams = "SELECT COUNT(*) FROM PIS_QYK WHERE FuncNo='" + m_FuncCode + "' AND CONVERT(varchar(7),ReportDate,126)=CONVERT(varchar(7),'" + ReportDate + "',126) AND Fileds01='" + Fileds01 + "'";
                if (DbHelperSQL.GetSingle(m_SqlParams).ToString() == "0")
                {
                    m_SqlParams = "INSERT INTO [PIS_QYK](";
                    m_SqlParams += "OprateUserID,FuncNo,";
                    m_SqlParams += "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,";
                    m_SqlParams += "Fileds11,Fileds12,ReportDate,AreaCode,AreaName";
                    m_SqlParams += ") VALUES(";
                    m_SqlParams += "" + m_UserID + ",'" + m_FuncCode + "',";
                    m_SqlParams += "'" + Fileds01 + "','" + Fileds02 + "','" + Fileds03 + "','" + Fileds04 + "','" + Fileds05 + "','" + Fileds06 + "','" + Fileds07 + "','" + Fileds08 + "','" + Fileds09 + "','" + Fileds10 + "',";
                    m_SqlParams += "'" + Fileds11 + "','" + Fileds12 + "','" + ReportDate + "','" + AreaCode + "','" + AreaName + "'";
                    m_SqlParams += ")";
                }
                else {
                    m_SqlParams = "";
                    Response.Write(" <script>alert('" + Fileds01 + "已经存在本月数据；无需重复添加；需要修改请操作“编辑”功能！') ;window.location.href='" + m_TargetUrl + "'</script>");
                    return;
                }
                
            }
            else if (m_ActionName == "edit")
            {
                m_SqlParams = "UPDATE PIS_QYK SET ";
                m_SqlParams += "Fileds01 ='" + Fileds01 + "',Fileds02 ='" + Fileds02 + "',Fileds03 ='" + Fileds03 + "',Fileds04 ='" + Fileds04 + "',Fileds05 ='" + Fileds05 + "',";
                m_SqlParams += "Fileds06 ='" + Fileds06 + "',Fileds07 ='" + Fileds07 + "',Fileds08 ='" + Fileds08 + "',Fileds09 ='" + Fileds09 + "',Fileds10 ='" + Fileds10 + "',";
                m_SqlParams += "Fileds11 ='" + Fileds11 + "',Fileds12 ='" + Fileds12 + "',ReportDate ='" + ReportDate + "',AreaCode ='" + AreaCode + "',AreaName ='" + AreaName + "' ";
                m_SqlParams += " WHERE CommID=" + m_ObjID;
            }

            try
            {
                //if (m_ActionName == "edit")
                //{
                //    // 分析修改的字段、记录修改日志
                //    string[] updateVal = { Fileds01, Fileds02, Fileds03, Fileds04, Fileds05, Fileds06, Fileds07, Fileds08, Fileds09, Fileds10, Fileds11, Fileds12 };
                //    string configFile = Server.MapPath("/includes/DataGrid.config");
                //    AreWeb.CertificateInOne.Biz.CommPage cp = new AreWeb.CertificateInOne.Biz.CommPage();
                //    string returnVal = cp.AnalysisFields(m_FuncCode, m_ObjID, configFile, updateVal);
                //    cp = null;
                //    if (!string.IsNullOrEmpty(returnVal))
                //    {
                //        string opContents = "用户ID[" + m_UserID + "]于 " + DateTime.Now.ToString() + " 修改了人口计生局下的<人口基本情况统计表>：" + returnVal;
                //        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(3);
                //        list.Add("UPDATE PIS_QYK SET AuditFlag=2,AnalysisFlag=1 WHERE CommID=" + m_ObjID);
                //        list.Add("INSERT INTO [SYS_Log]([OprateUserID], [OprateContents], [OprateModel], [OprateUserIP]) VALUES(" + m_UserID + ", '" + opContents + "', '数据修改', '" + Request.UserHostAddress + "')");
                //        list.Add("INSERT INTO [SYS_Msg]([SourceUserID], [TargetUserID], [MsgTitle], [MsgBody], [MsgType],[DocID]) VALUES(" + m_UserID + ", " + AreWeb.CertificateInOne.Biz.CommPage.GetTargetUser(m_FuncCode) + ", '差异数据处理提示', '" + opContents + "', 2,'')");
                //        DbHelperSQL.ExecuteSqlTran(list);
                //        list = null;
                //    }
                //}
                DbHelperSQL.ExecuteSql(m_SqlParams);
                Response.Write(" <script>alert('全员数据-人口基本情况统计信息操作成功！') ;window.location.href='" + m_TargetUrl + "'</script>");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                //Response.Write(" <script>alert('操作失败：" + ex.Message + "') ;</script>");
                return;
            }

        }

    }
}
