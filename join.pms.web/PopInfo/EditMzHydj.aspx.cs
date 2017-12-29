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

using AreWeb.CertificateInOne.Biz;
using UNV.Comm.DataBase;
using UNV.Comm.Web;
using System.Data.SqlClient;

namespace AreWeb.CertificateInOne.PopInfo
{
    public partial class EditMzHydj : System.Web.UI.Page
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

        #region 身份验证、参数过滤
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
                m_NavTitle = "婚姻登记信息";
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = Request.QueryString["RID"];
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
                case "add": // 新增
                    funcName = "新增";
                    SetReportArea("");
                    //this.LiteralCmsClass.Text = CustomerControls.CreateSelCtrl("txtCmsClass", "", "", "", "SELECT [CmsCID], [CmsCName] FROM [CMS_Class]");
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
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">起始页</a> &gt;&gt; 民政局 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }

        #region Action 操作行为处理

        /// <summary>
        /// 设置数据单位/行政区划单位
        /// </summary>
        /// <param name="areaCode">默认值</param>
        private void SetReportArea(string areaCode)
        {

            string siteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
            m_SqlParams = "SELECT AREACODE,AREANAME FROM \"AreaDetailCN\" WHERE PARENTCODE = '" + siteArea + "' ORDER BY AREACODE";
            DataTable tmpDt = new DataTable();
            tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            DDLReportArea.DataSource = tmpDt;
            DDLReportArea.DataTextField = "AREANAME";
            DDLReportArea.DataValueField = "AREACODE";
            DDLReportArea.DataBind();
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
                m_SqlParams = "SELECT * FROM PIS_BASEINFO WHERE COMMID=" + m_ObjID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    if (sdr["AUDITFLAG"].ToString() != "0" && m_ActionName == "edit")
                    {
                        isEdit = false;
                        break;
                    }
                    this.txtReportDate.Value = DateTime.Parse(sdr["REPORTDATE"].ToString()).ToString("yyyy-MM-dd");
                    SetReportArea(PageValidate.GetTrim(sdr["AREACODE"].ToString()));
                    // FILEDS01
                    this.DDLFileds01.Text = PageValidate.GetTrim(sdr["FILEDS01"].ToString());
                    this.txtFileds02.Text = PageValidate.GetTrim(sdr["FILEDS02"].ToString());
                    this.txtFileds03.Value = PageValidate.GetTrim(sdr["FILEDS03"].ToString());

                    this.txtFileds04.Text = PageValidate.GetTrim(sdr["FILEDS04"].ToString());
                    this.txtFileds05.Text = PageValidate.GetTrim(sdr["FILEDS05"].ToString());
                    this.DDLFileds06.Text = PageValidate.GetTrim(sdr["FILEDS06"].ToString());
                    this.txtFileds07.Text = PageValidate.GetTrim(sdr["FILEDS07"].ToString());
                    this.DDLFileds08.Text = PageValidate.GetTrim(sdr["FILEDS08"].ToString());
                    this.DDLFileds09.Text = PageValidate.GetTrim(sdr["FILEDS09"].ToString());
                    this.DDLFileds10.Text = PageValidate.GetTrim(sdr["FILEDS10"].ToString());
                    this.txtFileds11.Value = PageValidate.GetTrim(sdr["FILEDS11"].ToString());

                    this.txtFileds12.Text = PageValidate.GetTrim(sdr["FILEDS12"].ToString());
                    this.txtFileds13.Text = PageValidate.GetTrim(sdr["FILEDS13"].ToString());
                    this.DDLFileds14.Text = PageValidate.GetTrim(sdr["FILEDS14"].ToString());
                    this.txtFileds15.Text = PageValidate.GetTrim(sdr["FILEDS15"].ToString());
                    this.DDLFileds16.Text = PageValidate.GetTrim(sdr["FILEDS16"].ToString());
                    this.DDLFileds17.Text = PageValidate.GetTrim(sdr["FILEDS17"].ToString());
                    this.DDLFileds18.Text = PageValidate.GetTrim(sdr["FILEDS18"].ToString());
                    this.txtFileds19.Value = PageValidate.GetTrim(sdr["FILEDS19"].ToString());

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
                if (CommPage.CheckDelFlag(objID))
                {
                    m_SqlParams = "DELETE FROM PIS_BASEINFO WHERE COMMID IN(" + objID + ")";
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
                if (CommPage.CheckAuditFlag(objID, ref cmsAttrib))
                {
                    m_SqlParams = "UPDATE PIS_BASEINFO SET AUDITFLAG=" + cmsAttrib + " WHERE COMMID IN(" + objID + ") ";
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
                if (CommPage.CheckPubFlag(objID, ref cmsAttrib))
                {
                    m_SqlParams = "UPDATE PIS_BASEINFO SET AUDITFLAG=" + cmsAttrib + " WHERE COMMID IN(" + objID + ") ";
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
            string strErr = string.Empty;
            string ReportDate = PageValidate.GetTrim(this.txtReportDate.Value);
            string Fileds01 = PageValidate.GetTrim(this.DDLFileds01.SelectedValue);
            string Fileds02 = PageValidate.GetTrim(this.txtFileds02.Text);
            string Fileds03 = PageValidate.GetTrim(this.txtFileds03.Value);

            string Fileds04 = PageValidate.GetTrim(this.txtFileds04.Text);
            string Fileds05 = PageValidate.GetTrim(this.txtFileds05.Text);
            string Fileds06 = PageValidate.GetTrim(this.DDLFileds06.Text);
            string Fileds07 = PageValidate.GetTrim(this.txtFileds07.Text);
            string Fileds08 = PageValidate.GetTrim(this.DDLFileds08.Text);
            string Fileds09 = PageValidate.GetTrim(this.DDLFileds09.Text);
            string Fileds10 = PageValidate.GetTrim(this.DDLFileds10.Text);
            string Fileds11 = PageValidate.GetTrim(this.txtFileds11.Value);

            string Fileds12 = PageValidate.GetTrim(this.txtFileds12.Text);
            string Fileds13 = PageValidate.GetTrim(this.txtFileds13.Text);
            string Fileds14 = PageValidate.GetTrim(this.DDLFileds14.Text);
            string Fileds15 = PageValidate.GetTrim(this.txtFileds15.Text);
            string Fileds16 = PageValidate.GetTrim(this.DDLFileds16.Text);
            string Fileds17 = PageValidate.GetTrim(this.DDLFileds17.Text);
            string Fileds18 = PageValidate.GetTrim(this.DDLFileds18.Text);
            string Fileds19 = PageValidate.GetTrim(this.txtFileds19.Value);
            /*
             * 业务类型,证件号码,登记时间,男方姓名,身份证号,职业,家庭地址,民族,文化程度,婚姻状况,出生日期,女方姓名,身份证号,职业,家庭地址,民族,文化程度,婚姻状况,出生日期
             * Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19
             业务类型,证件号码,登记时间,
Fileds01,Fileds02,Fileds03,
男方姓名,身份证号,职业,家庭地址,民族,文化程度,婚姻状况,出生日期
Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,
女方姓名,身份证号,职业,家庭地址,民族,文化程度,婚姻状况,出生日期
Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19
             */
            string AreaCode = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Value);
            string AreaName = PageValidate.GetTrim(this.DDLReportArea.SelectedItem.Text);
            if (string.IsNullOrEmpty(AreaCode))
            {
                strErr += "请选择数据归属的行政区划！\\n";
            }
            if (String.IsNullOrEmpty(Fileds01))
            {
                strErr += "请选择业务类型！\\n";
            }
            if (String.IsNullOrEmpty(Fileds02))
            {
                strErr += "请输入证件号码！\\n";
            }
            if (String.IsNullOrEmpty(Fileds04))
            {
                strErr += "请输入男方姓名！\\n";
            }
            if (String.IsNullOrEmpty(Fileds05))
            {
                strErr += "请输入男方身份证号码！\\n";
            }
            if (!ValidIDCard.VerifyIDCard(Fileds05))
            {
                strErr += "男方身份证号码有误！！\\n";
            }
            if (String.IsNullOrEmpty(Fileds12))
            {
                strErr += "请输入女方姓名！\\n";
            }
            if (String.IsNullOrEmpty(Fileds13))
            {
                strErr += "请输入女方身份证号码！\\n";
            }
            if (!ValidIDCard.VerifyIDCard(Fileds13))
            {
                strErr += "女方身份证号码有误！！\\n";
            }
            if (String.IsNullOrEmpty(Fileds07))
            {
                strErr += "请输入男方详细地址！\\n";
            }
            if (String.IsNullOrEmpty(Fileds15))
            {
                strErr += "请输入女方详细地址！\\n";
            }
            if (String.IsNullOrEmpty(Fileds03))
            {
                strErr += "请选择登记日期！\\n";
            }
            if (String.IsNullOrEmpty(ReportDate))
            {
                strErr += "请选择数据日期！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            // COMMID,OprateUserID,OprateDate,ReportDate,FuncNo,UnitNo,
            if (m_ActionName == "add")
            {
                m_SqlParams = "INSERT INTO PIS_BASEINFO(";
                m_SqlParams += "OPRATEUSERID,FUNCNO,";
                m_SqlParams += "FILEDS01,FILEDS02,FILEDS03,FILEDS04,FILEDS05,FILEDS06,FILEDS07,FILEDS08,FILEDS09,FILEDS10,";
                m_SqlParams += "FILEDS11,FILEDS12,FILEDS13,FILEDS14,FILEDS15,FILEDS16,FILEDS17,FILEDS18,FILEDS19,REPORTDATE,AREACODE,AREANAME";
                m_SqlParams += ") VALUES(";
                m_SqlParams += "" + m_UserID + ",'" + m_FuncCode + "',";
                m_SqlParams += "'" + Fileds01 + "','" + Fileds02 + "','" + Fileds03 + "','" + Fileds04 + "','" + Fileds05 + "','" + Fileds06 + "','" + Fileds07 + "','" + Fileds08 + "','" + Fileds09 + "','" + Fileds10 + "',";
                m_SqlParams += "'" + Fileds11 + "','" + Fileds12 + "','" + Fileds13 + "','" + Fileds14 + "','" + Fileds15 + "','" + Fileds16 + "','" + Fileds17 + "','" + Fileds18 + "','" + Fileds19 + "','" + ReportDate + "','" + AreaCode + "','" + AreaName + "'";
                m_SqlParams += ")";
            }
            else if (m_ActionName == "edit")
            {
                m_SqlParams = "UPDATE PIS_BASEINFO SET ";
                m_SqlParams += "FILEDS01 ='" + Fileds01 + "',FILEDS02 ='" + Fileds02 + "',FILEDS03 ='" + Fileds03 + "',FILEDS04 ='" + Fileds04 + "',FILEDS05 ='" + Fileds05 + "',";
                m_SqlParams += "FILEDS06 ='" + Fileds06 + "',FILEDS07 ='" + Fileds07 + "',FILEDS08 ='" + Fileds08 + "',FILEDS09 ='" + Fileds09 + "',FILEDS10 ='" + Fileds10 + "',";
                m_SqlParams += "FILEDS11 ='" + Fileds11 + "',FILEDS12 ='" + Fileds12 + "',FILEDS13 ='" + Fileds13 + "',FILEDS14 ='" + Fileds14 + "',FILEDS15 ='" + Fileds15 + "',";
                m_SqlParams += "FILEDS16 ='" + Fileds16 + "',FILEDS17 ='" + Fileds17 + "',FILEDS18 ='" + Fileds18 + "',FILEDS19 ='" + Fileds19 + "',REPORTDATE ='" + ReportDate + "',AREACODE ='" + AreaCode + "',AREANAME ='" + AreaName + "' ";
                m_SqlParams += " WHERE COMMID=" + m_ObjID;
            }
            /*
             COMMID,COMMMEMO,OPRATEUSERID,OPRATEDATE,AREACODE,AREANAME,REPORTDATE,FUNCNO,UNITNO,AUDITUSERID,AUDITFLAG,ANALYSISFLAG,ANALYSISDATE,
FILEDS01,FILEDS02,FILEDS03,FILEDS04,FILEDS05,FILEDS06,FILEDS07,FILEDS08,FILEDS09,FILEDS10,FILEDS11,FILEDS12,FILEDS13,FILEDS14,FILEDS15,FILEDS16,FILEDS17,FILEDS18,FILEDS19,FILEDS20
,FILEDS21,FILEDS22,FILEDS23,FILEDS24,FILEDS25,FILEDS26,FILEDS27,FILEDS28,FILEDS29,FILEDS30,FILEDS31,FILEDS32,FILEDS33,FILEDS34,FILEDS35 
             */
            try
            {
                if (m_ActionName == "edit")
                {
                    // 分析修改的字段、记录修改日志
                    string[] updateVal = { Fileds01, Fileds02, Fileds03, Fileds04, Fileds05, Fileds06, Fileds07, Fileds08, Fileds09, Fileds10, Fileds11, Fileds12, Fileds13, Fileds14, Fileds15, Fileds16, Fileds17, Fileds18, Fileds19 };
                    string configFile = Server.MapPath("/includes/DataGrid.config");
                    CommPage cp = new CommPage();
                    string returnVal = cp.AnalysisFields(m_FuncCode, m_ObjID, configFile, updateVal);
                    cp = null;
                    if (!string.IsNullOrEmpty(returnVal))
                    {
                        string opContents = "用户ID[" + m_UserID + "]于 " + DateTime.Now.ToString() + " 修改了民政局下的<婚姻登记信息>：" + returnVal;
                        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                        list.Add("UPDATE PIS_BASEINFO SET AUDITFLAG=2 WHERE COMMID=" + m_ObjID);
                        list.Add("INSERT INTO \"SYS_Log\"(\"OprateUserID\", \"OprateContents\", \"OprateModel\", \"OprateUserIP\") VALUES(" + m_UserID + ", '" + opContents + "', '数据修改', '" + Request.UserHostAddress + "')");
                        DbHelperSQL.ExecuteSqlTran(list);
                        list = null;
                    }
                }
                DbHelperSQL.ExecuteSql(m_SqlParams);
                Response.Write(" <script>alert('婚姻登记信息操作成功！') ;window.location.href='" + m_TargetUrl + "'</script>");
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

