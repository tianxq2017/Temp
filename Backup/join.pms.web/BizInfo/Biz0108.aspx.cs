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
    public partial class Biz0108 : System.Web.UI.Page
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
                this.UcAreaSelRegA.SetAreaCode(m_AreaCode);
                this.UcAreaSelRegA.SetAreaName(BIZ_Common.GetAreaName(m_AreaCode, "1"));
                this.txtAreaSelRegCodeB.Value = m_AreaCode;
                this.txtAreaSelRegNameB.Text = BIZ_Common.GetAreaName(m_AreaCode, "1");
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
            BIZ_Common.GetNations(this.LiteralFileds40, "txtFileds40", "");
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

            #region 参数
            //if (String.IsNullOrEmpty(PageValidate.GetTrim(this.txtfwzh.Text))) { strErr += "母子健康手册证号不能为空！\\n"; }
            //if (String.IsNullOrEmpty(CommPage.GetSingleVal(" select top 1  QcfwzBm from BIZ_Contents  WHERE BizCode='0150' and QcfwzBm!='' and  Attribs IN(2,8,9) " + PageValidate.GetTrim(this.txtfwzh.Text) + " order by BizID desc "))) { strErr += "母子健康手册证号不存在！\\n"; }

            /*女方：身份证号、姓名、性别、民族、联系电话、户籍性质、户籍地址、居住地址、工作单位*/
            this.m_PerB.PersonCardID = PageValidate.GetTrim(this.txtPersonCidB.Text);
            this.m_PerB.PersonName = PageValidate.GetTrim(this.txtPersonNameB.Text);
            this.m_PerB.PersonSex = "女";
            this.m_PerB.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerB.PersonCardID);
            this.m_PerB.Nations = PageValidate.GetTrim(Request["txtNationsB"]);
            this.m_PerB.PersonTel = PageValidate.GetTrim(this.txtContactTelB.Text);
            this.m_PerB.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeB.Text);
            
            this.m_PerB.RegisterAreaName = this.txtAreaSelRegNameB.Text;
            
            this.m_PerB.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaName());


            if (this.m_PerB.RegisterAreaName == "海外")
            {
                this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
                this.m_PerB.RegisterAreaCode = this.m_PerB.CurrentAreaCode;
            }
            else if (this.m_PerB.CurrentAreaName == "海外")
            {
                this.m_PerB.RegisterAreaCode = this.txtAreaSelRegCodeB.Value;
                this.m_PerB.CurrentAreaCode = this.m_PerB.RegisterAreaCode;
            }
            else
            {
                this.m_PerB.RegisterAreaCode = this.txtAreaSelRegCodeB.Value;
                this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
            }

            this.m_PerB.WorkUnit = PageValidate.GetTrim(this.txtWorkUnitB.Text);



            /*男方：身份证号、姓名、性别、民族、联系电话、户籍性质、户籍地址、居住地址、工作单位*/
            this.m_PerA.PersonCardID = PageValidate.GetTrim(this.txtPersonCidA.Text);
            this.m_PerA.PersonName = PageValidate.GetTrim(this.txtPersonNameA.Text);
            this.m_PerA.PersonSex = "男";
            this.m_PerA.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerA.PersonCardID);
            //this.m_PerA.MarryType = PageValidate.GetTrim(this.ddlMarryTypeA.SelectedValue);
            //this.m_PerA.MarryDate = CommBiz.GetTrim(this.txtMarryDateA.Value);
            this.m_PerA.Nations = PageValidate.GetTrim(Request["txtNationsA"]);
            this.m_PerA.PersonTel = PageValidate.GetTrim(this.txtContactTelA.Text);
            this.m_PerA.RegisterType = PageValidate.GetTrim(this.ddlRegisterTypeA.Text);
            this.m_PerA.RegisterAreaName = PageValidate.GetTrim(this.UcAreaSelRegA.GetAreaName());
            this.m_PerA.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaName());

            if (this.m_PerA.RegisterAreaName == "海外")
            {
                this.m_PerA.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
                this.m_PerA.RegisterAreaCode = this.m_PerA.CurrentAreaCode;
            }
            else if (this.m_PerA.CurrentAreaName == "海外")
            {
                this.m_PerA.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegA.GetAreaCode());
                this.m_PerA.CurrentAreaCode = this.m_PerA.RegisterAreaCode;
            }
            else
            {
                this.m_PerA.RegisterAreaCode = PageValidate.GetTrim(this.UcAreaSelRegA.GetAreaCode());
                this.m_PerA.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
            }



            this.m_PerA.WorkUnit = PageValidate.GetTrim(this.txtWorkUnitA.Text);

            /*子女信息：姓名、性别、民族、现居住地、出生日期*/
            string Fileds16 = PageValidate.GetTrim(this.txtFileds16.Text);
            string Fileds17 = PageValidate.GetTrim(this.ddlFileds17.SelectedValue);
            string Fileds40 = PageValidate.GetTrim(Request["txtFileds40"]);
            string Fileds44 = PageValidate.GetTrim(this.UcAreaSelCurC.GetAreaName());
            string Fileds20 = CommBiz.GetTrim(this.txtFileds20.Value);

            //==========================女方信息 start========================================== 
            if (String.IsNullOrEmpty(this.m_PerB.PersonCardID)) { strErr += "请输入女方身份证号！\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerB.PersonCardID)) { strErr += "女方身份证号有误！！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonName)) { strErr += "请输入女方姓名！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.Nations)) { strErr += "请选择女方民族！\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerB.RegisterAreaCode, "0")) { strErr += "请选择女方户籍地！\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerB.CurrentAreaCode, "0")) { strErr += "请选择女方现居住地！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonTel)) { strErr += "请输入女方联系电话！\\n"; }

            //==========================男方信息 start========================================== 
            if (String.IsNullOrEmpty(this.m_PerA.PersonCardID)) { strErr += "请输入男方身份证号！\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerA.PersonCardID)) { strErr += "男方身份证号有误！！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.PersonName)) { strErr += "请输入男方姓名！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.Nations)) { strErr += "请选择男方民族！\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerA.RegisterAreaCode, "0")) { strErr += "请选择男方户籍地！\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerA.CurrentAreaCode, "0")) { strErr += "请选择男方现居住地！\\n"; }
            if (this.m_PerA.RegisterAreaCode != m_AreaCode && this.m_PerA.CurrentAreaCode != m_AreaCode && this.m_PerB.RegisterAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "男女双方户籍地或现居住地至少要有一方与所选择地一致！\\n"; }

            //==========================子女信息 start========================================== 
            if (String.IsNullOrEmpty(Fileds16)) { strErr += "请输入子女姓名！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.Nations)) { strErr += "请选择子女民族！\\n"; }
            if (String.IsNullOrEmpty(Fileds44)) { strErr += "请选择子女现居住址！\\n"; }
            if (String.IsNullOrEmpty(Fileds20)) { strErr += "请选择子女出生日期！\\n"; }

            /*申请理由：条例 款项 */
            string Fileds18 = PageValidate.GetTrim(this.txtFileds18.Text);
            if (String.IsNullOrEmpty(Fileds18)) { strErr += "请输入申请理由！\\n"; }

            #endregion

            //取证地点
            string GetAreaCode = PageValidate.GetTrim(this.UcAreaSelXian1.GetAreaCode());
            string GetAreaName = PageValidate.GetTrim(this.UcAreaSelXian1.GetAreaName());

            //夫妻双方合影
            //string strMsg = string.Empty;
            //string[] PersonPhotos = new string[3];
            //if (this.txtPersonPhotos.HasFile)
            //{
            //    if (!BIZ_UpFhotos.UploadPhotos(this.txtPersonPhotos, ref PersonPhotos, "0", ref strMsg))
            //    {
            //        strErr += "独生子女与父母合影2寸红底免冠照片上传失败！\\n";
            //    }
            //    else { strErr += strMsg; }
            //}

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
                this.m_BizC.QcfwzBm = PageValidate.GetTrim(this.txtfwzh.Text);

                //this.m_BizC.PersonPhotos = PersonPhotos[1];//独生子女与父母合影2寸红底免冠照片

                /*男方：身份证号、姓名、性别、民族、联系电话、户籍性质、户籍地址、居住地址、工作单位*/
                this.m_BizC.PersonCidA = this.m_PerA.PersonCardID;
                this.m_BizC.Fileds01 = this.m_PerA.PersonName;
                this.m_BizC.Fileds02 = this.m_PerA.PersonSex;
                this.m_BizC.Fileds32 = this.m_PerA.PersonBirthday;
                this.m_BizC.Fileds03 = this.m_PerA.Nations;
                this.m_BizC.ContactTelA = this.m_PerA.PersonTel;
                this.m_BizC.Fileds04 = this.m_PerA.RegisterType;
                this.m_BizC.RegAreaCodeA = this.m_PerA.RegisterAreaCode;
                this.m_BizC.RegAreaNameA = this.m_PerA.RegisterAreaName;
                this.m_BizC.CurAreaCodeA = this.m_PerA.CurrentAreaCode;
                this.m_BizC.CurAreaNameA = this.m_PerA.CurrentAreaName;
                this.m_BizC.Fileds05 = this.m_PerA.WorkUnit;

                /*女方：身份证号、姓名、性别、民族、联系电话、户籍性质、户籍地址、居住地址、工作单位*/
                this.m_BizC.PersonCidB = this.m_PerB.PersonCardID;
                this.m_BizC.Fileds08 = this.m_PerB.PersonName;
                this.m_BizC.Fileds09 = this.m_PerB.PersonSex;
                this.m_BizC.Fileds31 = this.m_PerB.PersonBirthday;
                this.m_BizC.Fileds10 = this.m_PerB.Nations;
                this.m_BizC.ContactTelB = this.m_PerB.PersonTel;
                this.m_BizC.Fileds11 = this.m_PerB.RegisterType;
                this.m_BizC.RegAreaCodeB = this.m_PerB.RegisterAreaCode;
                this.m_BizC.RegAreaNameB = this.m_PerB.RegisterAreaName;
                this.m_BizC.CurAreaCodeB = this.m_PerB.CurrentAreaCode;
                this.m_BizC.CurAreaNameB = this.m_PerB.CurrentAreaName;
                this.m_BizC.Fileds12 = this.m_PerB.WorkUnit;

                /*子女信息：姓名、性别、民族、现居住地址、出生日期*/
                this.m_BizC.Fileds16 = Fileds16;
                this.m_BizC.Fileds17 = Fileds17;
                this.m_BizC.Fileds40 = Fileds40;
                this.m_BizC.Fileds44 = Fileds44;
                this.m_BizC.Fileds20 = Fileds20;

                //申请理由 申请日期
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
                    #region 一方旗外处理
                    string perAAreaCode = string.Empty;
                    string perBAreaCode = string.Empty;
                    if (this.m_PerA.RegisterAreaCode.Substring(0, 4) != "1505")
                    {
                        perAAreaCode = this.m_PerB.RegisterAreaCode;
                        perBAreaCode = this.m_PerB.RegisterAreaCode;
                    }
                    else if (this.m_PerB.RegisterAreaCode.Substring(0, 4) != "1505")
                    {
                        perAAreaCode = this.m_PerA.RegisterAreaCode;
                        perBAreaCode = this.m_PerA.RegisterAreaCode;
                    }
                    else
                    {
                        perAAreaCode = this.m_PerA.RegisterAreaCode;
                        perBAreaCode = this.m_PerB.RegisterAreaCode;
                    }

                    #endregion
                    if (this.m_BizStep != BIZ_Common.InsetBizWorkFlows(this.m_FuncCode, this.m_BizStep, objID, perAAreaCode, perBAreaCode, m_AreaCode))
                    {
                        MessageBox.Show(this, "插入流程表失败，请联系系统管理员！");
                        return;
                    }
                    #region 具体操作
                    //========男方 
                    this.m_PerA.MateName = this.m_PerB.PersonName;
                    this.m_PerA.MateCardID = this.m_PerB.PersonCardID;
                    string PersonIDA = this.m_PerA.ExecBizPersons();
                    //========女方
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

                    #endregion
                    //if (!String.IsNullOrEmpty(PersonPhotos[1])) { BIZ_Common.InsetBizDocs(objID, PersonIDB, "独生子女与父母合影2寸红底免冠照片", PersonPhotos, "1"); }

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
