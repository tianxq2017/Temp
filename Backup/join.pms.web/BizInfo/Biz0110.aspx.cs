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
    public partial class Biz0110 : System.Web.UI.Page
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
        private string m_BizStep;
        private string m_AreaCode;
        private string m_AreaName;
        private BIZ_Persons m_PerA;//储存男方信息
        private BIZ_Persons m_PerB;//储存女方信息
        private BIZ_Contents m_BizC;//业务信息
        private BIZ_PersonChildren m_Children;//子女信息

        /// <summary>
        /// 页面初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();
            if (!IsPostBack)
            {
                //户籍地默认当前选择地区
                this.txtAreaSelRegCodeA.Value = m_AreaCode;
                this.txtAreaSelRegNameA.Text = BIZ_Common.GetAreaName(m_AreaCode, "1");
                this.UcAreaSelRegB.SetAreaCode(m_AreaCode);
                this.UcAreaSelRegB.SetAreaName(BIZ_Common.GetAreaName(m_AreaCode, "1"));
                //取证地点
                this.UcAreaSelXian1.SetAreaCode(m_AreaCode);
                //SetPageStyle(m_UserID);
                SetOpratetionAction(m_NavTitle);
            }
            //GetParam(this.txtHNationsA.Value, this.txtHNationsB.Value);
            GetParam(PageValidate.GetTrim(Request["txtNationsA"]), PageValidate.GetTrim(Request["txtNationsB"]));
        }
        /// <summary>
        /// 页面样式
        /// </summary>
        /// <param name="userID"></param>
        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "";//  DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/inidex.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        #region 获取内置参数 如：民族等
        /// <summary>
        /// 获取内置参数 如：民族等
        /// </summary>
        private void GetParam(string nationsA, string nationsB)
        {
            BIZ_Common.GetNations(this.LiteralNationsA, "txtNationsA", nationsA);
            BIZ_Common.GetNations(this.LiteralNationsB, "txtNationsB", nationsB);
        }
        #endregion

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
                m_TargetUrl = "/BizInfo/UnvBizList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                //m_NavTitle = HttpUtility.UrlDecode(StringProcess.AnalysisParas(m_SourceUrlDec, "FuncNa"));
                m_NavTitle = CommPage.GetSingleVal("SELECT BizNameFull FROM BIZ_Categories WHERE BizCode='" + m_FuncCode + "'");
                m_BizStep = CommPage.GetSingleVal("SELECT BizStep FROM BIZ_Categories WHERE BizCode='" + m_FuncCode + "'");
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
            this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">管理首页</a> &gt;&gt; 业务办理 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }
        #endregion

        #region 提交申请信息
        /// <summary>
        /// 提交申请信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;

            this.m_PerA = new BIZ_Persons();
            this.m_PerB = new BIZ_Persons();
            this.m_BizC = new BIZ_Contents();
            this.m_Children = new BIZ_PersonChildren();
            string app = string.Empty;

            #region 参数

            /*本人：姓名、性别、民族、户籍性质、身份证号、联系电话、婚姻状况、户籍地址、居住地址、出生年月、邮件*/
            this.m_PerA.PersonName = PageValidate.GetTrim(this.txtPersonNameA.Text);
            this.m_PerA.Nations = PageValidate.GetTrim(Request["txtNationsA"]);
            this.m_PerA.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeA.Text);
            this.m_PerA.PersonCardID = PageValidate.GetTrim(this.txtPersonCidA.Text);
            this.m_PerA.PersonSex = CommBiz.GetSexByID(this.txtPersonCidA.Text);
            this.m_PerA.PersonTel = PageValidate.GetTrim(this.txtContactTelA.Text);
            this.m_PerA.MarryType = PageValidate.GetTrim(this.ddlMarryTypeA.SelectedValue);
            this.m_PerA.RegisterAreaName = this.txtAreaSelRegNameA.Text;
            this.m_PerA.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaName());
            this.m_PerA.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerA.PersonCardID);
            string perMail = CommBiz.GetTrim(this.txtMail.Text);
            this.m_PerA.BirthNum = PageValidate.GetTrim(this.ddlBirthNum.SelectedValue);

            if (this.m_PerA.RegisterAreaName == "海外")
            {
                this.m_PerA.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
                this.m_PerA.RegisterAreaCode = this.m_PerA.CurrentAreaCode;
            }
            else if (this.m_PerA.CurrentAreaName == "海外")
            {
                this.m_PerA.RegisterAreaCode = this.txtAreaSelRegCodeA.Value;
                this.m_PerA.CurrentAreaCode = this.m_PerA.RegisterAreaCode;
            }
            else
            {
                this.m_PerA.RegisterAreaCode = this.txtAreaSelRegCodeA.Value;
                this.m_PerA.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
            }




            if (this.m_PerA.MarryType == "初婚" || this.m_PerA.MarryType == "再婚")
            {
                /*配偶：姓名、性别、民族、户籍性质、身份证号、联系电话、婚姻状况、户籍地址、居住地址、出生年月*/
                this.m_PerB.PersonName = PageValidate.GetTrim(this.txtPersonNameB.Text);
                this.m_PerB.Nations = PageValidate.GetTrim(Request["txtNationsB"]);
                this.m_PerB.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeB.Text);
                this.m_PerB.PersonCardID = PageValidate.GetTrim(this.txtPersonCidB.Text);
                this.m_PerB.PersonSex = CommBiz.GetSexByID(this.txtPersonCidB.Text);
                this.m_PerB.PersonTel = PageValidate.GetTrim(this.txtContactTelB.Text);
                this.m_PerB.MarryType = PageValidate.GetTrim(this.ddlMarryType.SelectedValue);
                //this.m_PerB.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaCode());
                this.m_PerB.RegisterAreaName = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaName());
                //this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
                this.m_PerB.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaName());
                this.m_PerB.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerB.PersonCardID);


                if (this.m_PerB.RegisterAreaName == "海外")
                {
                    this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
                    this.m_PerB.RegisterAreaCode = this.m_PerB.CurrentAreaCode;
                }
                else if (this.m_PerB.CurrentAreaName == "海外")
                {
                    this.m_PerB.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaCode());
                    this.m_PerB.CurrentAreaCode = this.m_PerB.RegisterAreaCode;
                }
                else
                {
                    this.m_PerB.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegB.GetAreaCode());
                    this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
                }
            }
            else
            {
                this.m_PerB.PersonName = "";
                this.m_PerB.Nations = "";
                this.m_PerB.RegisterType = "";
                this.m_PerB.PersonCardID = "";
                this.m_PerB.PersonSex = "";
                this.m_PerB.PersonTel = "";
                this.m_PerB.MarryType = "";
                this.m_PerB.RegisterAreaCode = "";
                this.m_PerB.RegisterAreaName = "";
                this.m_PerB.CurrentAreaCode = "";
                this.m_PerB.CurrentAreaName = "";
                this.m_PerB.PersonBirthday = "";
            }

            //==========================持证人信息 start========================================== 
            if (String.IsNullOrEmpty(this.m_PerA.PersonName)) { strErr += "请输入持证人姓名！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.Nations)) { strErr += "请输入持证人民族！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.PersonCardID)) { strErr += "请输入持证人身份证号！\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerA.PersonCardID)) { strErr += "持证人身份证号有误！！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.PersonTel)) { strErr += "请输入持证人联系电话！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.MarryType)) { strErr += "请选择持证人婚姻状况！\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerA.RegisterAreaCode, "0")) { strErr += "请选择持证人户籍地！\\n"; }
            if (!String.IsNullOrEmpty(perMail))
            {
                if (!PageValidate.IsEmail(perMail)) { strErr += "请输入正确的邮箱地址！\\n"; }
            }
            if (!CommPage.IsAreaCode(this.m_PerA.CurrentAreaCode, "0")) { strErr += "请选择持证人现居住地！\\n"; }

            if (this.m_PerA.MarryType == "初婚" || this.m_PerA.MarryType == "再婚")
            {
                //==========================配偶信息 start========================================== 
                if (String.IsNullOrEmpty(this.m_PerB.PersonName)) { strErr += "请输入配偶姓名！\\n"; }
                if (String.IsNullOrEmpty(this.m_PerB.Nations)) { strErr += "请输入配偶民族！\\n"; }
                if (String.IsNullOrEmpty(this.m_PerB.PersonCardID)) { strErr += "请输入配偶身份证号！\\n"; }
                if (!ValidIDCard.VerifyIDCard(this.m_PerB.PersonCardID)) { strErr += "配偶身份证号有误！！\\n"; }
                if (String.IsNullOrEmpty(this.m_PerB.MarryType)) { strErr += "请选择配偶婚姻状况！\\n"; }
                if (!CommPage.IsAreaCode(this.m_PerB.RegisterAreaCode, "0")) { strErr += "请选择配偶户籍地！\\n"; }
                if (!CommPage.IsAreaCode(this.m_PerB.CurrentAreaCode, "0")) { strErr += "请选择配偶现居住地！\\n"; }

                if (this.m_PerA.RegisterAreaCode != m_AreaCode && this.m_PerA.CurrentAreaCode != m_AreaCode && this.m_PerB.RegisterAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "男女双方户籍地或现居住地至少要有一方与所选择地一致！\\n"; }
            }
            else
            {
                if (this.m_PerA.RegisterAreaCode != m_AreaCode && this.m_PerA.CurrentAreaCode != m_AreaCode) { strErr += "持证人户籍地或现居住地至少要有一处与所选择地一致！\\n"; }
            }
            //子女信息
            int iBirthNum = int.Parse(this.m_PerA.BirthNum);
            #region 子女信息
            string ChildName1 = PageValidate.GetTrim(this.txtChildName1.Text);
            string ChildSex1 = PageValidate.GetTrim(this.ddlChildSex1.SelectedValue);
            string ChildBirthday1 = CommBiz.GetTrim(this.txtChildBirthday1.Value);
            string ChildPolicy1 = PageValidate.GetTrim(this.ddlChildPolicy1.Text);
            string ChildCardID1 = PageValidate.GetTrim(this.txtChildCardID1.Text);
            string ChildSource1 = PageValidate.GetTrim(this.ddlChildSource1.Text);
            string ChildMemos1 = PageValidate.GetTrim(this.ddlMemos1.Text);
            string ChildNo1 = "1";

            string ChildName2 = PageValidate.GetTrim(this.txtChildName2.Text);
            string ChildSex2 = PageValidate.GetTrim(this.ddlChildSex2.SelectedValue);
            string ChildBirthday2 = CommBiz.GetTrim(this.txtChildBirthday2.Value);
            string ChildPolicy2 = PageValidate.GetTrim(this.ddlChildPolicy2.Text);
            string ChildCardID2 = PageValidate.GetTrim(this.txtChildCardID2.Text);
            string ChildSource2 = PageValidate.GetTrim(this.ddlChildSource2.Text);
            string ChildMemos2 = PageValidate.GetTrim(this.ddlMemos2.Text);
            string ChildNo2 = "2";

            string ChildName3 = PageValidate.GetTrim(this.txtChildName3.Text);
            string ChildSex3 = PageValidate.GetTrim(this.ddlChildSex3.SelectedValue);
            string ChildBirthday3 = CommBiz.GetTrim(this.txtChildBirthday3.Value);
            string ChildPolicy3 = PageValidate.GetTrim(this.ddlChildPolicy3.Text);
            string ChildCardID3 = PageValidate.GetTrim(this.txtChildCardID3.Text);
            string ChildSource3 = PageValidate.GetTrim(this.ddlChildSource3.Text);
            string ChildMemos3 = PageValidate.GetTrim(this.ddlMemos3.Text);
            string ChildNo3 = "3";

            if (iBirthNum == 0)
            {
                this.m_PerB.BirthNum = "0";
                ChildName1 = ""; ChildName2 = ""; ChildName3 = "";
            }
            else if (iBirthNum == 1)
            {
                ChildName2 = ""; ChildSex2 = ""; ChildBirthday2 = ""; ChildPolicy2 = ""; ChildCardID2 = ""; ChildSource2 = ""; ChildMemos2 = "";
                ChildName3 = ""; ChildSex3 = ""; ChildBirthday3 = ""; ChildPolicy3 = ""; ChildCardID3 = ""; ChildSource3 = ""; ChildMemos3 = "";
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "请输入子女1姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "请选择子女1性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "请选择子女1出生年月！\\n"; }
            }
            else if (iBirthNum == 2)
            {
                ChildName3 = ""; ChildSex3 = ""; ChildBirthday3 = ""; ChildPolicy3 = ""; ChildCardID3 = ""; ChildSource3 = ""; ChildMemos3 = "";

                if (String.IsNullOrEmpty(ChildName1)) { strErr += "请输入子女1姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "请选择子女1性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "请选择子女1出生年月！\\n"; }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "请输入子女2姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "请选择子女2性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "请选择子女2出生年月！\\n"; }

                if (ChildName1 == ChildName2) { strErr += "子女姓名重复！\\n"; }
            }
            else if (iBirthNum == 3)
            {
                if (String.IsNullOrEmpty(ChildName1)) { strErr += "请输入子女1姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex1)) { strErr += "请选择子女1性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday1)) { strErr += "请选择子女1出生年月！\\n"; }

                if (String.IsNullOrEmpty(ChildName2)) { strErr += "请输入子女2姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex2)) { strErr += "请选择子女2性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday2)) { strErr += "请选择子女2出生年月！\\n"; }

                if (String.IsNullOrEmpty(ChildName3)) { strErr += "请输入子女3姓名！\\n"; }
                if (String.IsNullOrEmpty(ChildSex3)) { strErr += "请选择子女3性别！\\n"; }
                if (String.IsNullOrEmpty(ChildBirthday3)) { strErr += "请选择子女3出生年月！\\n"; }

                if (ChildName1 == ChildName2 || ChildName1 == ChildName3 || ChildName2 == ChildName3) { strErr += "子女姓名重复！\\n"; }
            }
            this.m_Children.ChildName1 = ChildName1;
            this.m_Children.ChildSex1 = ChildSex1;
            this.m_Children.ChildBirthday1 = ChildBirthday1;
            this.m_Children.ChildPolicy1 = ChildPolicy1;
            this.m_Children.ChildCardID1 = ChildCardID1;
            this.m_Children.ChildSource1 = ChildSource1;
            this.m_Children.Memos1 = ChildMemos1;
            this.m_Children.ChildNo1 = ChildNo1;

            this.m_Children.ChildName2 = ChildName2;
            this.m_Children.ChildSex2 = ChildSex2;
            this.m_Children.ChildBirthday2 = ChildBirthday2;
            this.m_Children.ChildPolicy2 = ChildPolicy2;
            this.m_Children.ChildCardID2 = ChildCardID2;
            this.m_Children.ChildSource2 = ChildSource2;
            this.m_Children.Memos2 = ChildMemos2;
            this.m_Children.ChildNo2 = ChildNo2;

            this.m_Children.ChildName3 = ChildName3;
            this.m_Children.ChildSex3 = ChildSex3;
            this.m_Children.ChildBirthday3 = ChildBirthday3;
            this.m_Children.ChildPolicy3 = ChildPolicy3;
            this.m_Children.ChildCardID3 = ChildCardID3;
            this.m_Children.ChildSource3 = ChildSource3;
            this.m_Children.Memos3 = ChildMemos3;
            this.m_Children.ChildNo3 = ChildNo3;
            #endregion

            /*其他情况*/
            string Fileds18 = PageValidate.GetTrim(this.txtFileds18.Text);
            if (this.m_PerA.MarryType == "未婚" || this.m_PerA.MarryType == "初婚")
            {
            }
            else
            {
                if (String.IsNullOrEmpty(Fileds18)) { strErr += "请输入其他情况！\\n"; }
            }
            #endregion

            //取证地点
            string GetAreaCode = PageValidate.GetTrim(this.UcAreaSelXian1.GetAreaCode());
            string GetAreaName = PageValidate.GetTrim(this.UcAreaSelXian1.GetAreaName());

            //检查是否重复提交
            string msg = string.Empty;
            if (CommPage.IsHasBiz(this.m_FuncCode, this.m_PerA.PersonCardID, this.m_PerB.PersonCardID, ref msg)) { strErr += msg; }

            if (cbOk.Checked == false)
            {
                strErr += "请确认承诺！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            try
            {
                #region insert
                this.m_BizStep = this.ddlBizStep.SelectedValue;
                this.m_BizC.BizCode = this.m_FuncCode;
                this.m_BizC.BizName = this.m_NavTitle;
                this.m_BizC.BizStep = this.m_BizStep;
                this.m_BizC.CurrentStep = "1";
                this.m_BizC.AdminUserID = this.m_UserID;
                this.m_BizC.AddressID = "0";
                this.m_BizC.Initiator = this.m_UserName;
                this.m_BizC.InitDirection = "1";
                this.m_BizC.SelAreaCode = this.m_AreaCode;
                this.m_BizC.SelAreaName = this.m_AreaName;
                this.m_BizC.StartDate = DateTime.Now.ToString();
                this.m_BizC.Attribs = "0";

                //取证地点
                this.m_BizC.GetAreaCode = GetAreaCode;
                this.m_BizC.GetAreaName = GetAreaName;

                /*持证人：姓名、性别、民族、户籍性质、身份证号、联系电话、婚姻状况、户籍地址、居住地址、出生年月、邮件*/
                this.m_BizC.Fileds01 = this.m_PerA.PersonName;
                this.m_BizC.Fileds02 = this.m_PerA.PersonSex;
                this.m_BizC.Fileds03 = this.m_PerA.Nations;
                this.m_BizC.Fileds04 = this.m_PerA.RegisterType;
                this.m_BizC.PersonCidA = this.m_PerA.PersonCardID;
                this.m_BizC.ContactTelA = this.m_PerA.PersonTel;
                this.m_BizC.Fileds33 = this.m_PerA.MarryType;
                this.m_BizC.RegAreaCodeA = this.m_PerA.RegisterAreaCode;
                this.m_BizC.RegAreaNameA = this.m_PerA.RegisterAreaName;
                this.m_BizC.CurAreaCodeA = this.m_PerA.CurrentAreaCode;
                this.m_BizC.CurAreaNameA = this.m_PerA.CurrentAreaName;
                this.m_BizC.Fileds32 = this.m_PerA.PersonBirthday;
                this.m_BizC.Fileds45 = perMail;

                //子女数
                this.m_BizC.Fileds07 = this.m_PerB.BirthNum;

                /*配偶：姓名、性别、民族、户籍性质、身份证号、联系电话、婚姻状况、户籍地址、居住地址、出生年月*/
                this.m_BizC.Fileds08 = this.m_PerB.PersonName;
                this.m_BizC.Fileds09 = this.m_PerB.PersonSex;
                this.m_BizC.Fileds10 = this.m_PerB.Nations;
                this.m_BizC.Fileds11 = this.m_PerB.RegisterType;
                this.m_BizC.PersonCidB = this.m_PerB.PersonCardID;
                this.m_BizC.ContactTelB = this.m_PerB.PersonTel;
                this.m_BizC.Fileds13 = this.m_PerB.MarryType;
                this.m_BizC.RegAreaCodeB = this.m_PerB.RegisterAreaCode;
                this.m_BizC.RegAreaNameB = this.m_PerB.RegisterAreaName;
                this.m_BizC.CurAreaCodeB = this.m_PerB.CurrentAreaCode;
                this.m_BizC.CurAreaNameB = this.m_PerB.CurrentAreaName;
                this.m_BizC.Fileds31 = this.m_PerB.PersonBirthday;

                //其他状况 申请日期
                this.m_BizC.Fileds18 = Fileds18;
                this.m_BizC.Fileds19 = DateTime.Now.ToString();

                string objID = m_BizC.Insert();
                m_BizC = null;
                if (!string.IsNullOrEmpty(objID) && PageValidate.IsNumber(objID))
                {
                    //业务日志
                    CommPage.SetBizLog(objID, m_UserID, "业务发起", "管理员用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 进行了《" + this.m_NavTitle + "》发起操作");

                    /*以下：1.判断群众基础表中是否存在，不存在插入，存在更新  --判断依据：身份证号
                            2.判断婚姻表中是否存在该用户信息，不存在插入，存在不跟新  --判断依据：群众编号
                            3.判断子女表中是否存在该用户现有家庭子女信息，不存在插入，存在不更新  --判断依据：群众编号
                     */
                    //#region 一方旗外处理
                    //string perAAreaCode = string.Empty;
                    //string perBAreaCode = string.Empty;
                    //if (this.m_PerA.RegisterAreaCode.Substring(0, 6) != "150524")
                    //{
                    //    perAAreaCode = this.m_PerB.RegisterAreaCode;
                    //    perBAreaCode = this.m_PerB.RegisterAreaCode;
                    //}
                    //else if (this.m_PerB.RegisterAreaCode.Substring(0, 6) != "150524")
                    //{
                    //    perAAreaCode = this.m_PerA.RegisterAreaCode;
                    //    perBAreaCode = this.m_PerA.RegisterAreaCode;
                    //}
                    //else
                    //{
                    //    perAAreaCode = this.m_PerA.RegisterAreaCode;
                    //    perBAreaCode = this.m_PerB.RegisterAreaCode;
                    //}

                    //#endregion
                    if (this.m_BizStep != BIZ_Common.InsetBizWorkFlows(this.m_FuncCode, this.m_BizStep, objID, "", "", m_AreaCode))
                    {
                        MessageBox.Show(this, "插入流程表失败，请联系系统管理员！");
                        return;
                    }
                    #region 具体操作
                    //========持证人 
                    this.m_PerA.MateName = this.m_PerB.PersonName;
                    this.m_PerA.MateCardID = this.m_PerB.PersonCardID;
                    string PersonIDA = this.m_PerA.ExecBizPersons();
                    //========配偶
                    this.m_PerB.MateName = this.m_PerA.PersonName;
                    this.m_PerB.MateCardID = this.m_PerA.PersonCardID;
                    string PersonIDB = this.m_PerB.ExecBizPersons();
                    //发送短讯--start--
                    // 某某,您提交的[业务名称]申请已受理。
                    string uTel, uName, tMsg = string.Empty;
                    if (!string.IsNullOrEmpty(this.m_PerB.PersonTel)) { uTel = this.m_PerB.PersonTel; uName = this.m_PerB.PersonName; } else { uTel = this.m_PerA.PersonTel; uName = this.m_PerA.PersonName; }
                    tMsg = uName + "，您提交的[" + this.m_NavTitle + "]申请已受理。"; //后台发起
                    if (!string.IsNullOrEmpty(uTel) && !string.IsNullOrEmpty(tMsg))
                    {
                        SendMsg.SendMsgByModem(uTel, tMsg);
                    }
                    //发送短讯--end--
                    m_PerA = null;
                    m_PerB = null;

                    //插入子女信息
                    this.m_Children.Inser(PersonIDA, objID, iBirthNum.ToString());
                    #endregion
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：<" + this.m_NavTitle + ">基础信息提交成功，请继续提交证件完成申请！", "BizDocs.aspx?action=add&k=" + objID + "&PersonIDA=" + PersonIDA + "&sourceUrl=" + m_SourceUrl, true, true);
                }
                else
                {
                    MessageBox.Show(this, "操作提示：操作失败，请联系系统管理员！");
                    return;
                }
                #endregion
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
