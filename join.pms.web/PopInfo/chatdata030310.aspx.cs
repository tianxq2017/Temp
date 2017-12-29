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
    public partial class chatdata030310 : System.Web.UI.Page
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
                this.ddlFileds01.SelectedValue = DateTime.Now.Year.ToString();
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
                m_NavTitle = "农村合作医疗汇总分析基础数据填报";
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
                    this.txtReportDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
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
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">起始页</a> &gt;&gt; 基础数据填报 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
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

                    this.ddlFileds01.SelectedValue = PageValidate.GetTrim(sdr["Fileds01"].ToString());
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
                    this.txtFileds13.Text = PageValidate.GetTrim(sdr["Fileds13"].ToString());
                    this.txtFileds14.Text = PageValidate.GetTrim(sdr["Fileds14"].ToString());
                    this.txtFileds15.Text = PageValidate.GetTrim(sdr["Fileds15"].ToString());
                    this.txtFileds16.Text = PageValidate.GetTrim(sdr["Fileds16"].ToString());
                    this.txtFileds17.Text = PageValidate.GetTrim(sdr["Fileds17"].ToString());
                    this.txtFileds18.Text = PageValidate.GetTrim(sdr["Fileds18"].ToString());
                    //this.txtFileds18.Value = PageValidate.GetTrim(sdr["Fileds18"].ToString());
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

        /*
         020102	0701 人口计划情况汇总表 0701 
        020103	0702 农村合作医疗统计表 
        020104	0703 农村、城镇独生子女保健费统计
        020105	0704 独生子女证办理统计表
        020106	0705 人口计生“三查”统计表
        020107	0706 免费技术服务情况统计表
        020108	0707 流动人口统计表
         */

        private string GetAnalysisCode(string funcNo)
        {
            string returnVa = string.Empty;
            switch (funcNo)
            {
                case "020102":
                    returnVa = "0701";
                    break;
                case "020103":
                    returnVa = "0702";
                    break;
                case "020104":
                    returnVa = "0703";
                    break;
                case "020105":
                    returnVa = "0704";
                    break;
                case "020106":
                    returnVa = "0705";
                    break;
                case "020107":
                    returnVa = "0706";
                    break;
                case "020108":
                    returnVa = "0707";
                    break;
                case "020109":
                    returnVa = "0708";
                    break;
                case "020110":
                    returnVa = "0709";
                    break;
                case "020111":
                    returnVa = "0710";
                    break;
                default:
                    returnVa = "0000";
                    break;
            }
            return returnVa;
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;

            string ReportDate = PageValidate.GetTrim(this.txtReportDate.Value);
            string AreaCode = PageValidate.GetTrim(this.UcDataAreaSel1.GetAreaCode());
            string AreaName = PageValidate.GetTrim(this.UcDataAreaSel1.GetAreaName());
            //string analysisCode = GetAnalysisCode(m_FuncCode);

            string Fileds01 = PageValidate.GetTrim(this.ddlFileds01.SelectedValue);
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
            string Fileds13 = PageValidate.GetTrim(this.txtFileds13.Text);
            string Fileds14 = PageValidate.GetTrim(this.txtFileds14.Text);
            string Fileds15 = PageValidate.GetTrim(this.txtFileds15.Text);
            string Fileds16 = PageValidate.GetTrim(this.txtFileds16.Text);
            string Fileds17 = PageValidate.GetTrim(this.txtFileds17.Text);
            string Fileds18 = PageValidate.GetTrim(this.txtFileds18.Text);

            if (String.IsNullOrEmpty(ReportDate)) strErr += "请选择数据日期！\\n";
            if (string.IsNullOrEmpty(AreaCode)) { strErr += "请选择数据单位！\\n"; }
            else
            {
                //if (AreaCode.Substring(9) == "000") strErr += "请选择到村/社区一级进行填报！\\n"; 
            }

            if (String.IsNullOrEmpty(Fileds01) || !PageValidate.IsNumber(Fileds01)) strErr += "年度不能为空且必须为数字格式！\\n";
            //if (String.IsNullOrEmpty(Fileds02) || !PageValidate.IsNumber(Fileds02)) strErr += "合计户数不能为空且必须为数字格式！\\n";
            //if (String.IsNullOrEmpty(Fileds03) || !PageValidate.IsNumber(Fileds03)) strErr += "合计人数不能为空且必须为数字格式！\\n";
            if (String.IsNullOrEmpty(Fileds04) || !PageValidate.IsNumber(Fileds04)) strErr += "国扶新增人数不能为空且必须为数字格式！\\n";
            if (String.IsNullOrEmpty(Fileds05) || !PageValidate.IsNumber(Fileds05)) strErr += "国扶退出人数不能为空且必须为数字格式！\\n";
            if (String.IsNullOrEmpty(Fileds06) || !PageValidate.IsNumber(Fileds06)) strErr += "国扶全部人数不能为空且必须为数字格式！\\n";
            if (String.IsNullOrEmpty(Fileds07) || !PageValidate.IsNumber(Fileds07)) strErr += "独女新增人数不能为空且必须为数字格式！\\n";
            if (String.IsNullOrEmpty(Fileds08) || !PageValidate.IsNumber(Fileds08)) strErr += "独女退出人数不能为空且必须为数字格式！\\n";
            if (String.IsNullOrEmpty(Fileds09) || !PageValidate.IsNumber(Fileds09)) strErr += "独女全部人数不能为空且必须为数字格式！\\n";

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }


            // CommID,OprateUserID,OprateDate,ReportDate,FuncNo,UnitNo,
            if (m_ActionName == "add")
            {
                //根据数据日期与数据单位判断是否已经添加过数据
                DataTable table = DbHelperSQL.Query("select * from PIS_BaseInfo where FuncNo = '030310' and ReportDate = '" + ReportDate + "' and AreaCode = '" + AreaCode + "'").Tables[0];
                if (table.Rows.Count > 0)
                {
                    MessageBox.Show(this, "该信息已经存在，同一个时间的数据，不能同时添加两次！");
                    return;
                }
                m_SqlParams = "INSERT INTO [PIS_BaseInfo](";
                m_SqlParams += "OprateUserID,FuncNo,";
                m_SqlParams += "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,";
                m_SqlParams += "Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,";
                m_SqlParams += "ReportDate,AreaCode,AreaName";
                m_SqlParams += ") VALUES(";
                m_SqlParams += "" + m_UserID + ",'" + m_FuncCode + "',";
                m_SqlParams += "'" + Fileds01 + "','" + Fileds02 + "','" + Fileds03 + "','" + Fileds04 + "','" + Fileds05 + "','" + Fileds06 + "','" + Fileds07 + "','" + Fileds08 + "','" + Fileds09 + "','" + Fileds10 + "',";
                m_SqlParams += "'" + Fileds11 + "','" + Fileds12 + "','" + Fileds13 + "','" + Fileds14 + "','" + Fileds15 + "','" + Fileds16 + "','" + Fileds17 + "','" + Fileds18 + "',";
                m_SqlParams += "'" + ReportDate + "','" + AreaCode + "','" + AreaName + "'";
                m_SqlParams += ")";
            }
            else if (m_ActionName == "edit")
            {
                m_SqlParams = "UPDATE PIS_BaseInfo SET ";
                m_SqlParams += "Fileds01 ='" + Fileds01 + "',Fileds02 ='" + Fileds02 + "',Fileds03 ='" + Fileds03 + "',Fileds04 ='" + Fileds04 + "',Fileds05 ='" + Fileds05 + "',";
                m_SqlParams += "Fileds06 ='" + Fileds06 + "',Fileds07 ='" + Fileds07 + "',Fileds08 ='" + Fileds08 + "',Fileds09 ='" + Fileds09 + "',Fileds10 ='" + Fileds10 + "',";
                m_SqlParams += "Fileds11 ='" + Fileds11 + "',Fileds12 ='" + Fileds12 + "',Fileds13 ='" + Fileds13 + "',Fileds14 ='" + Fileds14 + "', Fileds15 ='" + Fileds15 + "',Fileds16 ='" + Fileds16 + "',Fileds17 ='" + Fileds17 + "',Fileds18 ='" + Fileds18 + "',";
                m_SqlParams += "ReportDate ='" + ReportDate + "',AreaCode ='" + AreaCode + "',AreaName ='" + AreaName + "' ";
                m_SqlParams += " WHERE CommID=" + m_ObjID;
            }

            try
            {
                // 记录修改日志
                //if (m_ActionName == "edit")
                //{
                //    // 分析修改的字段、记录修改日志
                //    string[] updateVal = { Fileds01, Fileds02.ToString(), Fileds03.ToString(), Fileds04, Fileds05, Fileds06, Fileds07, Fileds08, Fileds09 };
                //    string configFile = Server.MapPath("/includes/DataGrid.config");
                //    AreWeb.CertificateInOne.Biz.CommPage cp = new AreWeb.CertificateInOne.Biz.CommPage();
                //    string returnVal = cp.AnalysisFields(m_FuncCode, m_ObjID, configFile, updateVal);
                //    cp = null;
                //    if (!string.IsNullOrEmpty(returnVal))
                //    {
                //        string opContents = "用户ID[" + m_UserID + "]于 " + DateTime.Now.ToString() + " 修改了基础数据填报下的<农村合作医疗汇总分析基础数据填报>：" + returnVal;
                //        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                //        list.Add("UPDATE PIS_BaseInfo SET AuditFlag=2 WHERE CommID=" + m_ObjID);
                //        list.Add("INSERT INTO [SYS_Log]([OprateUserID], [OprateContents], [OprateModel], [OprateUserIP]) VALUES(" + m_UserID + ", '" + opContents + "', '数据修改', '" + Request.UserHostAddress + "')");
                //        DbHelperSQL.ExecuteSqlTran(list);
                //        list = null;
                //    }
                //}
                DbHelperSQL.ExecuteSql(m_SqlParams);
                Response.Write(" <script>alert('农村合作医疗汇总分析基础数据填报操作成功！') ;window.location.href='" + m_TargetUrl + "'</script>");
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


