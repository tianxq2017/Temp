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
using System.Text;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;

namespace join.pms.wap.SceneSvrs
{
    public partial class Biz0107 : UNV.Comm.Web.PageBase
    {
        #region
        private BIZ_Persons m_PerA;//储存男方信息
        private BIZ_Persons m_PerB;//储存女方信息  
        private BIZ_Contents m_BizC;

        private string m_UserID;
        private string m_PersonName;

        private string m_AreaCode;
        private string m_AreaName;
        private string m_BizCode;
        private string m_BizName;
        private string m_BizStep;
        #endregion
        /// <summary>
        /// 页面初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();
            SetPageHeader("在线申请");
            this.UcAreaSelCurA.SetAreaCode(m_AreaCode);
            this.UcAreaSelCurA.SetAreaName(BIZ_Common.GetAreaName(m_AreaCode, "1"));
            if (!IsPostBack)
            {
                GetBizPersonsInfo();
                //居住地默认当前选择地区
                this.UcAreaSelCurB.SetAreaCode(m_AreaCode);
                this.UcAreaSelCurB.SetAreaName(BIZ_Common.GetAreaName(m_AreaCode, "1"));
                //取证地点
                this.UcAreaSelXian1.SetAreaCode(m_AreaCode);
            }
            //GetParam(this.txtHNationsA.Value, this.txtHNationsB.Value);
            GetParam(PageValidate.GetTrim(Request["txtNationsA"]), PageValidate.GetTrim(Request["txtNationsB"]));
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

        #region 设置页头信息及导航\验证参数\验证用户等
        //设置页头信息及导航等
        private void SetPageHeader(string objTitles)
        {
            try
            {
                this.Page.Header.Title = this.m_SiteName;
                base.AddMetaTag("keywords", Server.HtmlEncode(this.m_SiteName + "," + objTitles + "," + this.m_BizName + "," + this.m_SiteKeyWord));
                base.AddMetaTag(this.m_SiteName);
                base.AddMetaTag("description", Server.HtmlEncode(this.m_BizName + "," + m_SiteDescription));
                base.AddMetaTag("copyright", Server.HtmlEncode("本页版权归 西安网是科技发展有限公司 所有。All Rights Reserved"));
            }
            catch
            {
                Server.Transfer("~/errors.aspx");
            }
        }
        /// <summary>
        /// 验证接受的参数
        /// </summary>
        private void ValidateParams()
        {
            m_AreaCode = PageValidate.GetTrim(Request.QueryString["x"]);
            m_AreaName = BIZ_Common.GetAreaName(m_AreaCode, "0");
            if (!string.IsNullOrEmpty(m_AreaName) && !string.IsNullOrEmpty(m_AreaCode) && PageValidate.IsNumber(m_AreaCode))
            {
                this.m_BizCode = "0107";
                Biz_Categories bizCateg = new Biz_Categories();
                bizCateg.SelectSingle(m_BizCode);
                 this.m_BizName = bizCateg.BizNameAB;
                this.m_BizStep = bizCateg.BizStep;
                bizCateg = null;
                this.Uc_PageTop1.GetSysMenu(this.m_BizName);
            }
            else
            {
                Server.Transfer("/errors.aspx");
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
                HttpCookie loginCookie = Request.Cookies["AREWEB_OC_PUBSVRS_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["UserID"] != null && !String.IsNullOrEmpty(Session["UserID"].ToString())) { returnVa = true; m_UserID = Session["UserID"].ToString(); }
            }
            if (!returnVa)
            {
                Response.Write("<script language='javascript'>parent.location.href='/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh." + m_FileExt + "';</script>");
                Response.End();
            }
            else { m_PersonName = BIZ_Common.GetPersonFullName(this.m_UserID); }
        }
        #endregion

        #region 获取当前登录用户信息
        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        private void GetBizPersonsInfo()
        {
            try
            {
                BIZ_Persons bizPer = new BIZ_Persons();
                bizPer.GetPersonsInfo(this.m_UserID);
                string PersonSex = bizPer.PersonSex;
                if (PersonSex == "男")
                {
                    this.txtPersonCidA.Text = bizPer.PersonCardID;
                    this.txtPersonNameA.Text = bizPer.PersonName;
                    this.txtHNationsA.Value = bizPer.Nations;
                    this.UcAreaSelCurA.SetAreaCode(bizPer.CurrentAreaCode);
                    this.UcAreaSelCurA.SetAreaName(bizPer.CurrentAreaName);

                    //配偶信息
                    this.txtPersonNameB.Text = bizPer.MateName;
                    this.txtPersonCidB.Text = bizPer.MateCardID;
                }
                else
                {
                    this.txtPersonCidB.Text = bizPer.PersonCardID;
                    this.txtPersonNameB.Text = bizPer.PersonName;
                    this.txtHNationsB.Value = bizPer.Nations;
                    this.UcAreaSelCurB.SetAreaCode(bizPer.CurrentAreaCode);
                    this.UcAreaSelCurB.SetAreaName(bizPer.CurrentAreaName);
                    this.txtContactTelB.Text = bizPer.PersonTel;

                    //配偶信息
                    this.txtPersonNameA.Text = bizPer.MateName;
                    this.txtPersonCidA.Text = bizPer.MateCardID;
                }
                bizPer = null;
            }
            catch { }
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

            /*女方：姓名、民族、出生年月、居住地址、怀孕胎次、末次月经时间、确认怀孕时间、确认方式、确认单位、电话*/
            this.m_PerB.PersonCardID = PageValidate.GetTrim(this.txtPersonCidB.Text);
            this.m_PerB.PersonName = PageValidate.GetTrim(this.txtPersonNameB.Text);
            this.m_PerB.PersonSex = "女";
            this.m_PerB.Nations = PageValidate.GetTrim(Request["txtNationsB"]);
            this.m_PerB.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerB.PersonCardID);
            this.m_PerB.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaCode());
            this.m_PerB.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurB.GetAreaName());
            string Fileds41 = PageValidate.GetTrim(this.ddlFileds41.Text);
            string Fileds42 = CommBiz.GetTrim(this.txtFileds42.Value);
            string Fileds43 = CommBiz.GetTrim(this.txtFileds43.Value);
            string Fileds29 = PageValidate.GetTrim(this.ddlFileds29.SelectedValue);
            string Fileds30 = PageValidate.GetTrim(this.ddlFileds30.SelectedValue);
            this.m_PerB.PersonTel = PageValidate.GetTrim(this.txtContactTelB.Text);

            /*男方：姓名、民族、出生年月、居住地址*/
            this.m_PerA.PersonCardID = PageValidate.GetTrim(this.txtPersonCidA.Text);
            this.m_PerA.PersonName = PageValidate.GetTrim(this.txtPersonNameA.Text);
            this.m_PerA.PersonSex = "男";
            this.m_PerA.Nations = PageValidate.GetTrim(Request["txtNationsA"]);
            this.m_PerA.PersonBirthday = CommBiz.GetBirthdayByID(this.m_PerA.PersonCardID);
            this.m_PerA.CurrentAreaCode = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaCode());
            this.m_PerA.CurrentAreaName = PageValidate.GetTrim(this.UcAreaSelCurA.GetAreaName());

            //==========================女方信息 start========================================== 
            if (String.IsNullOrEmpty(this.m_PerB.PersonCardID)) { strErr += "请输入女方身份证号！\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerB.PersonCardID)) { strErr += "女方身份证号有误！！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonName)) { strErr += "请输入女方姓名！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.Nations)) { strErr += "请输入女方民族！\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerB.CurrentAreaCode, "0")) { strErr += "请选择女方现居住地！\\n"; }
            if (String.IsNullOrEmpty(Fileds41)) { strErr += "请输入怀孕胎次！\\n"; }
            if (String.IsNullOrEmpty(Fileds42)) { strErr += "请输入末次月经时间！\\n"; }
            if (String.IsNullOrEmpty(Fileds43)) { strErr += "请输入确认怀孕时间！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerB.PersonTel)) { strErr += "请输入女方联系电话！\\n"; }

            //==========================男方信息 start========================================== 
            if (String.IsNullOrEmpty(this.m_PerA.PersonCardID)) { strErr += "请输入男方身份证号！\\n"; }
            if (!ValidIDCard.VerifyIDCard(this.m_PerA.PersonCardID)) { strErr += "男方身份证号有误！！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.PersonName)) { strErr += "请输入男方姓名！\\n"; }
            if (String.IsNullOrEmpty(this.m_PerA.Nations)) { strErr += "请输入男方民族！\\n"; }
            if (!CommPage.IsAreaCode(this.m_PerA.CurrentAreaCode, "0")) { strErr += "请选择男方现居住地！\\n"; }
            if (this.m_PerA.CurrentAreaCode != m_AreaCode && this.m_PerB.CurrentAreaCode != m_AreaCode) { strErr += "男女双方现居住地至少要有一方与所选择地一致！\\n"; }

            /*结婚时间 */
            string Fileds14 = CommBiz.GetTrim(this.txtFileds14.Value);
            if (String.IsNullOrEmpty(Fileds14)) { strErr += "请输入结婚时间！\\n"; }

            /*服务证号 */
            string Fileds21 = PageValidate.GetTrim(this.txtFileds21.Text);
            if (String.IsNullOrEmpty(Fileds21)) { strErr += "请输入服务证号！\\n"; }

            /*申请理由：条例 款项 */
            string Fileds18 = PageValidate.GetTrim(this.txtFileds18.Text);
            if (String.IsNullOrEmpty(Fileds18)) { strErr += "请输入申请理由！\\n"; }

            #endregion

            //取证地点
            string GetAreaCode = PageValidate.GetTrim(this.UcAreaSelXian1.GetAreaCode());
            string GetAreaName = PageValidate.GetTrim(this.UcAreaSelXian1.GetAreaName());

            //检查是否重复提交
            string msg = string.Empty;
            if (CommPage.IsHasBiz(this.m_BizCode, this.m_PerA.PersonCardID, this.m_PerB.PersonCardID, ref msg)) { strErr += msg; }

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
                this.m_BizC.BizCode = this.m_BizCode;
                this.m_BizC.BizName = this.m_BizName;
                this.m_BizC.BizStep = this.m_BizStep;
                this.m_BizC.CurrentStep = "1";
                this.m_BizC.PersonID = this.m_UserID;
                this.m_BizC.AddressID = "0";
                this.m_BizC.Initiator = this.m_PersonName + "by手机";
                this.m_BizC.InitDirection = "0";
                this.m_BizC.SelAreaCode = this.m_AreaCode;
                this.m_BizC.SelAreaName = this.m_AreaName;
                this.m_BizC.StartDate = DateTime.Now.ToString();
                this.m_BizC.Attribs = "0";

                //取证地点
                this.m_BizC.GetAreaCode = GetAreaCode;
                this.m_BizC.GetAreaName = GetAreaName;

                /*男方：姓名、民族、出生年月、居住地址*/
                this.m_BizC.PersonCidA = this.m_PerA.PersonCardID;
                this.m_BizC.Fileds01 = this.m_PerA.PersonName;
                this.m_BizC.Fileds02 = this.m_PerA.PersonSex;
                this.m_BizC.Fileds03 = this.m_PerA.Nations;
                this.m_BizC.Fileds32 = this.m_PerA.PersonBirthday;
                this.m_BizC.CurAreaCodeA = this.m_PerA.CurrentAreaCode;
                this.m_BizC.CurAreaNameA = this.m_PerA.CurrentAreaName;

                /*女方：姓名、民族、出生年月、居住地址、怀孕胎次、末次月经时间、确认怀孕时间、确认方式、确认单位、联系电话*/
                this.m_BizC.PersonCidB = this.m_PerB.PersonCardID;
                this.m_BizC.Fileds08 = this.m_PerB.PersonName;
                this.m_BizC.Fileds09 = this.m_PerB.PersonSex;
                this.m_BizC.Fileds10 = this.m_PerB.Nations;
                this.m_BizC.Fileds31 = this.m_PerB.PersonBirthday;
                this.m_BizC.CurAreaCodeB = this.m_PerB.CurrentAreaCode;
                this.m_BizC.CurAreaNameB = this.m_PerB.CurrentAreaName;
                this.m_BizC.Fileds41 = Fileds41;
                this.m_BizC.Fileds42 = Fileds42;
                this.m_BizC.Fileds43 = Fileds43;
                this.m_BizC.Fileds29 = Fileds29;
                this.m_BizC.Fileds30 = Fileds30;
                this.m_BizC.ContactTelB = this.m_PerB.PersonTel;

                //结婚时间
                this.m_BizC.Fileds14 = Fileds14;

                /*服务证号 */
                this.m_BizC.Fileds21 = Fileds21;

                //申请理由 申请日期
                this.m_BizC.Fileds18 = Fileds18;
                this.m_BizC.Fileds19 = DateTime.Now.ToString();

                string objID = m_BizC.Insert();
                m_BizC = null;
                if (!string.IsNullOrEmpty(objID) && PageValidate.IsNumber(objID))
                {
                    //业务日志
                    CommPage.SetBizLog(objID, m_UserID, "业务发起", "群众用户ID[" + m_UserID + "-]于 " + DateTime.Now.ToString() + " 进行了《" + this.m_BizName + "》发起操作");

                    /*以下：1.判断群众基础表中是否存在，不存在插入，存在更新  --判断依据：身份证号
                            2.判断婚姻表中是否存在该用户信息，不存在插入，存在不跟新  --判断依据：群众编号
                            3.判断子女表中是否存在该用户现有家庭子女信息，不存在插入，存在不更新  --判断依据：群众编号
                     */
                    if (this.m_BizStep != BIZ_Common.InsetBizWorkFlows(this.m_BizCode, this.m_BizStep, objID, this.m_PerA.CurrentAreaCode, this.m_PerB.CurrentAreaCode, m_AreaCode))
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
                    string uTel, uName, tMsg = string.Empty;
                    if (!string.IsNullOrEmpty(this.m_PerB.PersonTel)) { uTel = this.m_PerB.PersonTel; uName = this.m_PerB.PersonName; } else { uTel = this.m_PerA.PersonTel; uName = this.m_PerA.PersonName; }
                    tMsg = uName + "，您提交的[" + this.m_BizName + "]申请已受理，请登录平台“用户信息”界面查看办理进度。"; //群众发起
                    if (!string.IsNullOrEmpty(uTel) && !string.IsNullOrEmpty(tMsg))
                    {
                        SendMsg.SendMsgByModem(uTel, tMsg);
                        if (!string.IsNullOrEmpty(this.m_PerA.RegisterAreaCode) && this.m_PerA.RegisterAreaCode.Substring(9) != "000") BIZ_Common.SetMsgToAuditer(this.m_BizName, uName, this.m_PerA.RegisterAreaCode);//男方村专干
                        if (!string.IsNullOrEmpty(this.m_PerB.RegisterAreaCode) && this.m_PerB.RegisterAreaCode.Substring(9) != "000") BIZ_Common.SetMsgToAuditer(this.m_BizName, uName, this.m_PerB.RegisterAreaCode);//女方村专干
                    }
                    //发送短讯--end--
                    m_PerA = null;
                    m_PerB = null;

                    #endregion

                    MessageBox.ShowAndRedirect(this.Page, "", "/Svrs-BizDocs/" + m_BizCode + "-" + objID + "." + m_FileExt, false, true);
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
