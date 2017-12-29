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
namespace join.pms.wap.UserCenter
{
    public partial class BizView : UNV.Comm.Web.PageBase
    {
        #region 自定义变量
        private string m_UserID;
        private string m_PersonCardID;

        private string m_UrlPageNo;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        protected string m_TargetUrl;
        protected string m_NavTitle;

        private BIZ_Contents m_BizC;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();
            if (!IsPostBack)
            {
                // 编辑删除功能的返回地址               
                if (String.IsNullOrEmpty(m_ActionName))
                {
                    Response.Write("非法访问，操作被终止！");
                    Response.End();
                }
                else
                {
                    SetOpratetionAction(this.m_NavTitle);
                }
            }
        }
        #region 设置页头信息及导航\验证参数\验证用户等
        /// <summary>
        /// 验证接受的参数
        /// </summary>
        private void ValidateParams()
        {
            m_ActionName = Request.QueryString["action"];
            m_UrlPageNo = Request.QueryString["p"];
            m_FuncCode = Request.QueryString["oCode"];
            m_NavTitle = Request.QueryString["oNa"];
            m_ObjID = Request.QueryString["k"];
            if (string.IsNullOrEmpty(m_UrlPageNo)) m_UrlPageNo = "1"; // 页码默认为第一页
            if (!string.IsNullOrEmpty(m_ActionName) && m_ActionName == "view" && string.IsNullOrEmpty(m_ObjID))
            {
                m_ObjID = Request.QueryString["RID"];
            }
            if (!string.IsNullOrEmpty(m_FuncCode) && m_FuncCode.Length > 1 && PageValidate.IsNumber(m_FuncCode))
            {
                m_TargetUrl = "/OC/" + m_FuncCode + "-" + m_UrlPageNo + "." + this.m_FileExt;

                this.Uc_PageTop1.GetSysMenu(this.m_NavTitle);
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
                Response.Write("<script language='javascript'>alert(\"操作提示：请登录后再试！\");parent.location.href='/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh." + m_FileExt + "';</script>");
                Response.End();
            }
            else { m_PersonCardID = BIZ_Common.GetPersonCardID(this.m_UserID); }
        }
        #endregion

        #region 设置操作行为
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
                case "view":
                    funcName = "修改";
                    ShowModInfo(m_ObjID);
                    break;
                case "chexiao":
                    funcName = "撤销";
                    DelBiz(m_ObjID);
                    break;
                case "cui":
                    funcName = "催办";
                    CuiBan(m_ObjID);
                    break;
                default:
                    //Response.Write("<script language='javascript'>alert('操作提示：参数错误！');parent.location.href='" + m_TargetUrl + "';</script>");
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, false);
                    break;
            }
        }
        /// <summary>
        /// 清理业务,撤销业务
        /// </summary>
        /// <param name="bizID"></param>
        private void DelBiz(string bizID)
        {
            this.m_BizC = new BIZ_Contents();
            this.m_BizC.UserID = m_UserID;
            this.m_BizC.PersonCardID = m_PersonCardID;
            this.m_BizC.BizID = bizID;
            string msg = this.m_BizC.Delete();
            m_BizC = null;
            MessageBox.ShowAndRedirect(this.Page, msg, m_TargetUrl, false);
        }
        /// <summary>
        /// 催办
        /// </summary>
        /// <param name="bizID"></param>
        private void CuiBan(string bizID)
        {
            this.m_BizC = new BIZ_Contents();
            this.m_BizC.UserID = m_UserID;
            this.m_BizC.PersonCardID = m_PersonCardID;
            this.m_BizC.BizID = bizID;
            string msg = this.m_BizC.CuiBan();
            m_BizC = null;
            MessageBox.ShowAndRedirect(this.Page, msg, m_TargetUrl, false);
        }
        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {

            StringBuilder sHtml = new StringBuilder();
            this.m_BizC = new BIZ_Contents();

            BIZ_PersonChildren children = new BIZ_PersonChildren();
            children.Select("", objID);

            try
            {
                /*查询语句，前置条件--必填*/
                this.m_BizC.UserID = m_UserID;
                this.m_BizC.PersonCardID = m_PersonCardID;
                this.m_BizC.BizID = objID;
                this.m_BizC.SelectAll(true);

                sHtml.Append("<div class=\"part_name\">" + this.m_BizC.BizName + "</div>");

                switch (this.m_BizC.BizCode)
                {
                    case "0101":
                        #region 一孩《生育服务证》申请表
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>女方基本信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        //sHtml.Append("<li>");
                        //sHtml.Append("<p class=\"title\">是否是独生子女</p>");
                        //sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds16 + "</p>");
                        //sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">单位</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds12 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>男方基本信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        //sHtml.Append("<li>");
                        //sHtml.Append("<p class=\"title\">是否是独生子女</p>");
                        //sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds17 + "</p>");
                        //sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">单位</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds05 + "</p>");
                        sHtml.Append("</li>"); ;
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>2寸红底免冠夫妇合影照片</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">2寸红底免冠夫妇合影照片</p>");
                        sHtml.Append("<p class=\"text_con\">");
                        if (!string.IsNullOrEmpty(this.m_BizC.PersonPhotos))
                        { sHtml.Append("<img src=\"" + this.m_BizC.PersonPhotos + "\" width=\"50\" height=\"50\">"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>申请理由</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请理由</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>子女情况</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1身份证号或出生医学证明或收养证号</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2身份证号或出生医学证明或收养证号</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3身份证号或出生医学证明或收养证号</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>预领证地点</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">预领证地点</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        #endregion
                        break;
                    case "0102":
                        #region 二孩生育登记
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>女方基本信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">是否是独生子女：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds16 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">单位：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds12 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>男方基本信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">是否是独生子女：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds17 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">单位：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds05 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>2寸红底免冠夫妇合影照片</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">2寸红底免冠夫妇合影照片：</p>");
                        sHtml.Append("<p class=\"text_con\">");
                        if (!string.IsNullOrEmpty(this.m_BizC.PersonPhotos))
                        { sHtml.Append("<img src=\"" + this.m_BizC.PersonPhotos + "\" width=\"50\" height=\"50\">"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>申请理由</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请理由：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>子女情况</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1身份证号或出生医学证明或收养证号</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1父亲姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.FatherName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1母亲姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.MotherName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1政策属性</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1其他情况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2身份证号或出生医学证明或收养证号</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2父亲姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.FatherName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2母亲姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.MotherName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2政策属性</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2其他情况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3身份证号或出生医学证明或收养证号</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3父亲姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.FatherName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3母亲姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.MotherName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3政策属性</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3其他情况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>预领证地点</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">预领证地点</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        #endregion
                        break;
                    case "0103":
                        #region 奖励扶助
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>申请人基本信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">性别</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds02 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚变日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");


                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>配偶信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">性别</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds09 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚变日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");


                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>生育子女数</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">夫妇曾经生育子女数</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds20 + "&nbsp;男&nbsp;" + this.m_BizC.Fileds21 + "&nbsp;女</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">夫妇现有存活子女（含收养）</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds37 + "&nbsp;男&nbsp;" + this.m_BizC.Fileds38 + "&nbsp;女</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>申请理由</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请理由</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>子女情况</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1死亡日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2死亡日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3死亡日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName4))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4死亡日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource4 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName5))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5死亡日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource5 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName6))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6死亡日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource6 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>预领证地点</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">预领证地点</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                      
                        #endregion
                        break;
                    case "0104":
                        #region 特别奖扶
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>申请人基本信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">性别</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds02 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚变日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");


                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>配偶信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">性别</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds09 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚变日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");


                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>生育子女数</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">夫妇曾经生育子女数</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds20 + "&nbsp;男&nbsp;" + this.m_BizC.Fileds21 + "&nbsp;女</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">夫妇现有存活子女（含收养）</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds37 + "&nbsp;男&nbsp;" + this.m_BizC.Fileds38 + "&nbsp;女</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">是否领取独生子女证</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds22 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>伤残信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">残疾证号码</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds23 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">残疾证类型</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds24 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">残疾等级</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds26 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>申请理由</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请理由</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>子女情况</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1存活状况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1死/残亡日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1死/残确认单位</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathAudit1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2存活状况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2死/残亡日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2死/残确认单位</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathAudit2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3存活状况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3死/残亡日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3死/残确认单位</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathAudit3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName4))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4存活状况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4死/残亡日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4死/残确认单位</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathAudit4 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName5))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5存活状况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5死/残亡日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5死/残确认单位</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathAudit5 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName6))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6存活状况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6死/残亡日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6死/残确认单位</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathAudit6 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>预领证地点</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">预领证地点</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        #endregion
                        break;
                    case "0105":
                        #region 少生快富
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>配偶信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>育龄妇女丈夫信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>生育子女数</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">按政策可生育子女数</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds40 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现有子女数（含收养）</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds37 + "&nbsp;男&nbsp;" + this.m_BizC.Fileds38 + "&nbsp;女</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>联系方式</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">2寸红底免冠夫妇合影照片</p>");
                        sHtml.Append("<p class=\"text_con\">" );
                        if (!string.IsNullOrEmpty(this.m_BizC.PersonPhotos))
                        { sHtml.Append("<img src=\"" + this.m_BizC.PersonPhotos + "\" width=\"50\" height=\"50\">"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>避孕状况</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">育龄妇女</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds45 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">育龄妇女丈夫</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds46 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">避孕开始日期</p>");
                        sHtml.Append("<p class=\"text_con\">" +BIZ_Common.FormatString(this.m_BizC.Fileds44, "2")+ "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>申请理由</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请理由</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请日期</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>子女情况</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1存活状况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2存活状况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3存活状况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName4))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4存活状况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女4是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource4 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName5))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5存活状况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女5是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource5 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName6))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6存活状况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女6是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource6 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>预办理地点</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">预办理地点</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                       
                        #endregion
                        break;
                    case "0106":
                        #region 结扎奖励
                        sHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"2\">申请人基本信息</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">性别：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds02 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居地址：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"2\">配偶信息</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">性别：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds09 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"2\">申请理由</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请理由：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"2\">结扎术情况</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">手术医院：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds27 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">手术日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds28, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</table>");
                        sHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"5\">子女情况</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<th height=\"32px\">子女姓名</p><p class=\"title\">性别</p><p class=\"title\">出生年月</p><p class=\"title\">死亡日期</p><p class=\"title\">是否亲生</p>");
                        sHtml.Append("</li>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday1 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday2 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday3 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName4))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName4 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex4 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday4 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday4 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource4 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName5))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName5 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex5 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday5 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday5 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource5 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName6))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName6 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex6 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday6 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday6 + "</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource6 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</table>");
                        #endregion
                        break;
                    case "0107":
                        #region 一杯奶
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>女方信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">怀孕胎次：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds41 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">末次月经时间：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds42 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">确认怀孕时间：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds43 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">确认方式：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds29 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">确认单位：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds30 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>男方基本信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>结婚时间</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">结婚时间：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>服务证号</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">服务证号：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds21 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>申请理由</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请理由：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>预办理地点</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">预办理地点</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        #endregion
                        break;
                    case "0108":
                        #region 独生子女父母光荣证
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>母亲信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">单位：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds12 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>父亲信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">单位：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds05 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>独生子女与父母合影2寸红底免冠照片</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">独生子女与父母合影2寸红底免冠照片：</p>");
                        sHtml.Append("<p class=\"text_con\">");
                        if (!string.IsNullOrEmpty(this.m_BizC.PersonPhotos))
                        { sHtml.Append("<img src=\"" + this.m_BizC.PersonPhotos + "\" width=\"50\" height=\"50\">"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>子女信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds16 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">性别：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds17 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds40 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds44 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds20 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>申请理由</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请理由：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>预领证地点</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">预领证地点</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        #endregion
                        break;
                    case "0109":
                        #region 《流动人口婚育证明》办理
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>持证人</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻变动日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>配偶</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻变动日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>避孕措施</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">避孕措施：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds45 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>本人近期一寸红底免冠正面照片</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">本人近期一寸红底免冠正面照片：</p>");
                        sHtml.Append("<p class=\"text_con\">");
                        if (!string.IsNullOrEmpty(this.m_BizC.PersonPhotos))
                        { sHtml.Append("<img src=\"" + this.m_BizC.PersonPhotos + "\" width=\"50\" height=\"50\">"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>承诺</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">承诺：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>生育情况</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1身份证号</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1出生日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1政策属性</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1社会抚养费征收情况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2身份证号</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2出生日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2政策属性</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2社会抚养费征收情况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3身份证号</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3出生日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3政策属性</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3社会抚养费征收情况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>预办理地点</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">预办理地点</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        #endregion
                        break;
                    case "0110":
                        #region 婚育情况证明
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>本人信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">性别：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds02 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">电子邮箱：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds45 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>配偶信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">性别：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds09 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>其他情况</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">其他情况：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>生育情况</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1出生日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1政策属性</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1身份证号</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1社会抚养费征收情况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2出生日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2政策属性</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2身份证号</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2社会抚养费征收情况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3出生日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3政策属性</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3身份证号</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3是否亲生</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3社会抚养费征收情况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>预办理地点</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">预办理地点</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        #endregion
                        break;
                    case "0111":
                        #region 终止妊娠审核
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>育龄妇女信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">年龄：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds29 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">末次月经日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds42 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">孕周：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds43 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">政策属性：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds30 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>丈夫信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>申请理由</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请理由：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>双方生育子女情况</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<th height=\"32px\">子女姓名</p><p class=\"title\">性别</p><p class=\"title\">出生年月</p><p class=\"title\">政策属性</p><p class=\"title\">备注</p>");
                        sHtml.Append("</li>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1出生日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1政策属性</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1备注</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2出生日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2政策属性</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2备注</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3出生日期</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3政策属性</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3备注</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        #endregion
                        break;
                    case "0122":
                        #region 再生育审批
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>女方基本信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">是否是独生子女：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds16 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">单位：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds12 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>男方基本信息</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">出生日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号码：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">是否是独生子女：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds17 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地址：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">单位：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds05 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>2寸红底免冠夫妇合影照片</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">2寸红底免冠夫妇合影照片：</p>");
                        sHtml.Append("<p class=\"text_con\">");
                        if (!string.IsNullOrEmpty(this.m_BizC.PersonPhotos))
                        { sHtml.Append("<img src=\"" + this.m_BizC.PersonPhotos + "\" width=\"50\" height=\"50\">"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>申请理由</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请理由：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">申请日期：</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>子女情况</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1身份证号或出生医学证明或收养证号</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1父亲姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.FatherName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1母亲姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.MotherName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1政策属性</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女1其他情况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2身份证号或出生医学证明或收养证号</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2父亲姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.FatherName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2母亲姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.MotherName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2政策属性</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女2其他情况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3性别</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3出生年月</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3身份证号或出生医学证明或收养证号</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3父亲姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.FatherName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3母亲姓名</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.MotherName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3政策属性</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">子女3其他情况</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">∨</span>预领证地点</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">预领证地点</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        #endregion
                        break;
                    default:
                        #region 其他业务申请B
                        sHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"2\">男方基本信息</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        if (this.m_BizC.Fileds04 == "农转城")
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">转城时间：</p>");
                            sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds49 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">工作单位：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds36 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"2\">女方基本信息</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">姓名：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">身份证号：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">联系电话：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">民族：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户口性质：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        if (this.m_BizC.Fileds11 == "农转城")
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">转城时间：</p>");
                            sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds50 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">户籍地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">居住地：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">工作单位：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds12 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">生育状况：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds06 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">现有家庭子女数：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds07 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻状况：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">婚姻时间：</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">再婚时间</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds15 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</table>");
                        sHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"5\">子女情况</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">子女姓名</p><p class=\"title\">性别</p><p class=\"title\">出生年月</p><p class=\"title\">来源</p><p class=\"title\">是否政策内</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds16 + "</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds17 + "</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds19 + "</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds20 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds21 + "</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds22 + "</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds23 + "</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds24 + "</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds25 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds26 + "</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds27 + "</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds28 + "</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds29 + "</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds30 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</table>");
                        #endregion
                        break;
                }
            }
            catch
            {
                sHtml = new StringBuilder(); sHtml.Append("获取数据信息出错…");
            }
            this.LiteralBizView.Text = sHtml.ToString();
            this.m_BizC = null;
            children = null;

            sHtml = null;
        }

        #endregion
    }
}


