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
    public partial class BizPrt0112 : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号

        private string m_SqlParams;
        public string m_TargetUrl;
        public string m_SiteName = System.Configuration.ConfigurationManager.AppSettings["SiteName"];
        private string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                if (m_ActionName == "chexiao")
                {
                    // 清理删除,撤销 
                    if (!PageValidate.IsNumber(m_ObjID))
                    {
                        m_ObjID = m_ObjID.Replace("s", ",");
                    }
                    DelBiz(m_ObjID);
                }
                else if (m_ActionName == "logoff")
                {
                    if (!PageValidate.IsNumber(m_ObjID))
                    {
                        m_ObjID = m_ObjID.Replace("s", ",");
                    }
                    SetBizLogOff(m_ObjID);
                }
                else
                {
                    ShowBizInfo(m_ObjID);
                    if (m_ActionName == "view" || m_ActionName == "viewDetails")
                    {
                        BIZ_Docs doc = new BIZ_Docs();
                        this.LiteralDocs.Text = doc.GetBizDocsForView(m_ObjID);
                        doc = null;
                    }
                }
            }
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

        /// <summary>
        /// 清理业务,撤销业务
        /// </summary>
        /// <param name="bizID"></param>
        private void DelBiz(string bizID)
        {
            try
            {
                // BIZ_Contents --> Attribs: 0,初始提交;1,审核中 2,通过 3,补正 4,撤销,撤销 5,注销 9,归档
                if (CommPage.CheckBizDelAttribs("BizID IN(" + bizID + ")"))
                {
                    m_SqlParams = "UPDATE BIZ_Contents SET Attribs=4 WHERE BizID IN(" + bizID + ")";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您所选择的业务撤销操作成功！", m_TargetUrl, true);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：由群众发起的业务申请或者是已经开始审核的业务数据禁止撤销操作，可选择“补正”操作！", m_TargetUrl, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：" + ex.Message, m_TargetUrl, true);
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="bizID"></param>
        private void SetBizLogOff(string bizID)
        {
            try
            {
                if (CommPage.CheckLogOffAttribs("BizID IN(" + bizID + ")"))
                {
                    System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                    list.Add("UPDATE BIZ_Certificates SET Attribs=1 WHERE BizID IN(" + bizID + ")");
                    list.Add("UPDATE BIZ_Contents SET Attribs=5 WHERE BizID IN(" + bizID + ")");
                    DbHelperSQL.ExecuteSqlTran(list);
                    list = null;

                    MessageBox.ShowAndRedirect(this.Page, "操作提示：您所选择的业务注销操作成功！", m_TargetUrl, true);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：只有审核完毕的业务才可以执行“注销”操作！", m_TargetUrl, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, "操作提示：" + ex.Message, m_TargetUrl, true);
            }
        }


        /// <summary>
        /// 查看详细信息
        /// </summary>
        /// <param name="objID"></param>
        private void ShowBizInfo(string bizID)
        {
            string tmpDate1 = "", tmpDate2 = "", appPhotos = string.Empty;
            StringBuilder s = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT * FROM BIZ_Contents WHERE BizID=" + bizID;
                /*
SELECT [
BizID,BizCode,BizName,CurrentStep,CurrentStepNa,PersonID,PersonPhotos,PersonCidA,PersonCidB,AddressID,ContactTelA,ContactTelB,
RegAreaCodeA,RegAreaCodeB,RegAreaNameA,RegAreaNameB,CurAreaCodeA,CurAreaCodeB,CurAreaNameA,CurAreaNameB,SelAreaCode,SelAreaName,
Initiator,InitDirection,StartDate,FinalDate,Attribs,
Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19,Fileds20,Fileds21,Fileds22,Fileds23,Fileds24,Fileds25,Fileds26,Fileds27,Fileds28,Fileds29,Fileds30,Fileds31,Fileds32,Fileds33,Fileds34,Fileds35,Fileds36,Fileds37,Fileds38,Fileds39,Fileds40,Fileds41,Fileds42,Fileds43,Fileds44,Fileds45,Fileds46,Fileds47,Fileds48,Fileds49,Fileds50
                 * ] FROM [YN_ChuXiong_OnlineCertificate_DB].[dbo].[BIZ_Contents] 
                 
                 */
                /*


                 
                 */
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    tmpDate1 = GetDateFormat(sdr["Fileds31"].ToString());
                    tmpDate2 = GetDateFormat(sdr["Fileds32"].ToString());

                    appPhotos = sdr["PersonPhotos"].ToString();
                    if (string.IsNullOrEmpty(appPhotos)) appPhotos = "<img src=\"" + m_SvrsUrl + appPhotos + "\" />";

                    s.Append("<div class=\"tr_01 clearfix\">");
                    s.Append("<div class=\"td_01\">" + sdr["Fileds01"].ToString() + "</div>");
                    s.Append("<div class=\"td_02\">" + sdr["Fileds02"].ToString() + "</div>");
                    s.Append("<div class=\"td_03\">" + sdr["Fileds03"].ToString() + "</div>");
                    s.Append("<div class=\"td_04\">" + sdr["Fileds04"].ToString() + "</div>");
                    //s.Append("<p>√</p>");
                    //s.Append("<p>√</p>");
                    //s.Append("</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_02 clearfix\">");
                    s.Append("<div class=\"td_01\">" + GetDateFormat(sdr["Fileds32"].ToString()) + "</div>");
                    s.Append("<div class=\"td_02\">" + sdr["PersonCidA"].ToString() + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_03\">" + sdr["RegAreaNameA"].ToString() + "(" + sdr["Fileds05"].ToString() + ")</div>");//户籍地址和单位
                    s.Append("<div class=\"tr_04 clearfix\">");
                    s.Append(GetMarryInfo(sdr["PersonCidA"].ToString()));//婚育状况

                    s.Append("<div class=\"tr_04_03\">");
                    if (sdr["Fileds06"].ToString() == "已生育") {
                        s.Append("<p>&nbsp;</p>");
                        s.Append("<p>√</p>");
                    }
                    else
                    {
                        s.Append("<p>√</p>");
                        s.Append("<p>&nbsp;</p>");
                    }
                    
                    s.Append("</div><div class=\"tr_04_04\">" + sdr["Fileds07"].ToString() + "</div></div>");//现有家庭子女数
                    s.Append("<div class=\"tr_05 clearfix\">");
                    s.Append("<div class=\"td_01\">" + sdr["Fileds16"].ToString() + "</div>");//子女1
                    s.Append("<div class=\"td_02\">" + sdr["Fileds17"].ToString() + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(sdr["Fileds18"].ToString()) + "</div>");
                    s.Append("<div class=\"td_04\">" + sdr["Fileds19"].ToString() + "</div>");
                    s.Append("<div class=\"td_05\">" + sdr["Fileds20"].ToString() + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_05 clearfix\">");
                    s.Append("<div class=\"td_01\">" + sdr["Fileds21"].ToString() + "</div>");//子女2
                    s.Append("<div class=\"td_02\">" + sdr["Fileds22"].ToString() + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(sdr["Fileds23"].ToString()) + "</div>");
                    s.Append("<div class=\"td_04\">" + sdr["Fileds24"].ToString() + "</div>");
                    s.Append("<div class=\"td_05\">" + sdr["Fileds25"].ToString() + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_05 clearfix\">");
                    s.Append("<div class=\"td_01\">" + sdr["Fileds26"].ToString() + "</div>");//子女3
                    s.Append("<div class=\"td_02\">" + sdr["Fileds27"].ToString() + "</div>");
                    s.Append("<div class=\"td_03\">" + GetDateFormat(sdr["Fileds28"].ToString()) + "</div>");
                    s.Append("<div class=\"td_04\">" + sdr["Fileds29"].ToString() + "</div>");
                    s.Append("<div class=\"td_05\">" + sdr["Fileds30"].ToString() + "</div>");
                    s.Append("</div>");
                    s.Append("<div class=\"tr_06\">" + sdr["Fileds33"].ToString() + "</div>");
                    s.Append("<div class=\"tr_07\">");
                    s.Append("<div class=\"td_01\">本人声明：<br />&nbsp;&nbsp;&nbsp;&nbsp;上述填写的各项内容均为实际情况，如有隐瞒虚报，愿承担所带来的一切法律责任，同时本人已经清楚人口和计划生育的有关政策和法规，保证自觉遵守。</div>");
                    s.Append("<div class=\"td_02\">本人签名：    &nbsp;&nbsp;&nbsp;&nbsp;" + GetDateFormat(sdr["StartDate"].ToString()) + "</div>");
                    //s.Append("<div class=\"official\"><img src=\"images/1.gif\" /></div>");
                    s.Append("</div>");

                    s.Append(GetWorkFlows(bizID));

                    if (m_ActionName == "priint") SetCertificateLog(bizID, sdr["BizCode"].ToString(), sdr["BizName"].ToString(), sdr["Fileds01"].ToString(), sdr["PersonCidA"].ToString()); //保存打印的证件
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            this.LiteralBizInfo.Text = s.ToString(); ;
        }

        /// <summary>
        /// 保存打印的证件记录
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="BizCode"></param>
        /// <param name="BizName"></param>
        /// <param name="pName"></param>
        /// <param name="pCid"></param>
        private void SetCertificateLog(string bizID, string BizCode, string BizName, string pName, string pCid)
        {
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT TOP 1 AreaCode,AreaName,DeptName,Approval,CertificateNoA,CertificateNoB,Attribs,CertificateDateStart,CertificateDateEnd FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep DESC";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    string CertificateNo = sdr["CertificateNoA"].ToString();
                    string CertificateName = "婚育情况证明";
                    string CertificateGovName = sdr["DeptName"].ToString();
                    string StartDate = sdr["CertificateDateStart"].ToString();
                    string AreaCode = sdr["AreaCode"].ToString();

                    m_SqlParams = "SELECT COUNT(*) FROM BIZ_Certificates WHERE CertificateType=1 AND BizCode='0112' AND BizID=" + bizID + "";
                    if (CommPage.GetSingleVal(m_SqlParams) == "0") {
                        m_SqlParams = "INSERT INTO BIZ_Certificates (";
                        m_SqlParams += "BizID,BizCode,BizName,CertificateNo,CertificateName,AreaCode,";
                        m_SqlParams += "PersonName,PersonCid,CertificateGovName,StartDate,CertificateType";
                        m_SqlParams += ") VALUES(";
                        m_SqlParams += "" + bizID + ",'" + BizCode + "','" + BizName + "','-','" + CertificateName + "','" + AreaCode + "',";
                        m_SqlParams += "'" + pName + "','" + pCid + "','" + CertificateGovName + "','" + StartDate + "',1";
                        m_SqlParams += ")";

                        DbHelperSQL.ExecuteSql(m_SqlParams);
                    }
                }
                sdr.Close();
            }
            catch
            {
                if (sdr != null) sdr.Close();
            }
        }

       
        /// <summary>
        /// 婚姻史
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        private string GetMarryInfo(string pID) {
            // SELECT MarryType,MarryDate FROM BIZ_PersonMarryRec WHERE PersonID=(SELECT PersonID FROM BIZ_Persons WHERE PersonCardID=)
            SqlDataReader sdr = null;
            StringBuilder s = new StringBuilder();
            string[] aryMarry = new string[] { "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;" };
            string[] aryDate = new string[] { "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;" };
            try
            {
                m_SqlParams = "SELECT MarryType,MarryDate FROM BIZ_PersonMarryRec WHERE PersonID=(SELECT PersonID FROM BIZ_Persons WHERE PersonCardID='" + pID + "')";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (sdr[0].ToString() == "未婚") { aryMarry[0] = "√"; }
                        if (sdr[0].ToString() == "初婚") { aryMarry[1] = "√"; aryDate[0] = GetDateFormat(sdr[1].ToString()); }
                        if (sdr[0].ToString() == "再婚") { aryMarry[2] = "√"; aryDate[2] = GetDateFormat(sdr[1].ToString()); }
                        if (sdr[0].ToString() == "离婚") { aryMarry[3] = "√"; aryDate[1] = GetDateFormat(sdr[1].ToString()); }
                        if (sdr[0].ToString() == "丧偶") { aryMarry[4] = "√"; aryDate[3] = GetDateFormat(sdr[1].ToString()); }
                    }
                }
                else { aryMarry[0] = "√"; }
               
                sdr.Close();

                s.Append("<div class=\"tr_04_01\">");
                for (int i = 0; i < aryMarry.Length;i++ ) {
                    s.Append("<p>" + aryMarry[i] + "</p>");
                }
                s.Append("</div>");
                s.Append("<div class=\"tr_04_02\">");
                for (int k = 0; k < aryDate.Length; k++)
                {
                    s.Append("<p>" + aryDate[k] + "</p>");
                }
                s.Append("</div>");
            }
            catch { if (sdr != null) sdr.Close(); }
            return s.ToString();
        }

        /// <summary>
        /// 生育史
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        private string GetBoysInfo(string pID)
        {
            // SELECT MarryType,MarryDate FROM BIZ_PersonMarryRec WHERE PersonID=(SELECT PersonID FROM BIZ_Persons WHERE PersonCardID=)
            SqlDataReader sdr = null;
            StringBuilder s = new StringBuilder();
            try
            {
                m_SqlParams = "SELECT ChildName,ChildSex,ChildBirthday,ChildSource,ChildPolicy FROM BIZ_PersonChildren WHERE PersonID==(SELECT PersonID FROM BIZ_Persons WHERE PersonCardID=" + pID + ")";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {

                }
                sdr.Close();

               
            }
            catch { if (sdr != null) sdr.Close(); }
            return s.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="curAreaName"></param>
        /// <returns></returns>
        private string GetWorkFlows(string bizID)
        {
            string OprateDate1 = "", OprateDate2 =  string.Empty;
            string seal1 = "", seal2 = string.Empty;
            string attribsCN = string.Empty;
            StringBuilder b = new StringBuilder();
            DataTable dt = new DataTable();
            try
            {
                m_SqlParams = "SELECT AreaName,Comments,Approval,Signature,OprateDate,CertificateNoA,CertificateNoB,CertificateMemo,Attribs FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep";
                dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (dt.Rows.Count == 2)
                {
                    OprateDate1 = GetDateFormat(dt.Rows[0]["OprateDate"].ToString());
                    OprateDate2 = GetDateFormat(dt.Rows[1]["OprateDate"].ToString());

                    seal1 = dt.Rows[0]["Signature"].ToString();
                    seal2 = dt.Rows[1]["Signature"].ToString();
                    if (!string.IsNullOrEmpty(seal1)) seal1 = "<img src=\"" + seal1 + "\" />";
                    if (!string.IsNullOrEmpty(seal2)) seal2 = "<img src=\"" + seal2 + "\" />";

                    b.Append("<div class=\"tr_07 tr_07a\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[0]["Comments"].ToString() + "</div>");
                    b.Append("<div class=\"td_02\">经办人签名：" + dt.Rows[0]["Approval"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;" + OprateDate1 + "</div>");
                    b.Append("<div class=\"official\">" + seal1 + "</div>");
                    b.Append("</div>");
                    b.Append("<div class=\"tr_07\">");
                    b.Append("<div class=\"td_01\">" + dt.Rows[1]["Comments"].ToString() + "</div>");
                    b.Append("<div class=\"td_02\">经办人签名：" + dt.Rows[1]["Approval"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;" + OprateDate2 + "</div>");
                    b.Append("<div class=\"official\">" + seal2 + "</div>");
                    b.Append("</div>");
                }
                else
                {
                    b.Append("<div class=\"tr_07\"><br/>流程节点参数预制错误！<br/><br/></div>");
                }
                dt = null;
            }
            catch
            {
                b.Append("<div class=\"tr_07\"><br/>获取流程节点审核信息时发生错误！<br/><br/></div>");
            }
            return b.ToString();
        }

        private string GetDateFormat(string inStr)
        {
            string returnVal = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(inStr))
                {
                    returnVal = DateTime.Parse(inStr).ToString("yyyy-MM-dd");
                }
            }
            catch { returnVal = inStr; }
            return returnVal;
        }


        #endregion
    }
}


