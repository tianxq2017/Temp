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
        #region �Զ������
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
                // �༭ɾ�����ܵķ��ص�ַ               
                if (String.IsNullOrEmpty(m_ActionName))
                {
                    Response.Write("�Ƿ����ʣ���������ֹ��");
                    Response.End();
                }
                else
                {
                    SetOpratetionAction(this.m_NavTitle);
                }
            }
        }
        #region ����ҳͷ��Ϣ������\��֤����\��֤�û���
        /// <summary>
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_ActionName = Request.QueryString["action"];
            m_UrlPageNo = Request.QueryString["p"];
            m_FuncCode = Request.QueryString["oCode"];
            m_NavTitle = Request.QueryString["oNa"];
            m_ObjID = Request.QueryString["k"];
            if (string.IsNullOrEmpty(m_UrlPageNo)) m_UrlPageNo = "1"; // ҳ��Ĭ��Ϊ��һҳ
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
        /// �����֤ 
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
                Response.Write("<script language='javascript'>alert(\"������ʾ�����¼�����ԣ�\");parent.location.href='/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh." + m_FileExt + "';</script>");
                Response.End();
            }
            else { m_PersonCardID = BIZ_Common.GetPersonCardID(this.m_UserID); }
        }
        #endregion

        #region ���ò�����Ϊ
        /// <summary>
        /// ���ò�����Ϊ
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("�Ƿ����ʣ���������ֹ��");
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
                case "add": // ����
                    funcName = "����";
                    break;
                case "view":
                    funcName = "�޸�";
                    ShowModInfo(m_ObjID);
                    break;
                case "chexiao":
                    funcName = "����";
                    DelBiz(m_ObjID);
                    break;
                case "cui":
                    funcName = "�߰�";
                    CuiBan(m_ObjID);
                    break;
                default:
                    //Response.Write("<script language='javascript'>alert('������ʾ����������');parent.location.href='" + m_TargetUrl + "';</script>");
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, false);
                    break;
            }
        }
        /// <summary>
        /// ����ҵ��,����ҵ��
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
        /// �߰�
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
        /// �鿴
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
                /*��ѯ��䣬ǰ������--����*/
                this.m_BizC.UserID = m_UserID;
                this.m_BizC.PersonCardID = m_PersonCardID;
                this.m_BizC.BizID = objID;
                this.m_BizC.SelectAll(true);

                sHtml.Append("<div class=\"part_name\">" + this.m_BizC.BizName + "</div>");

                switch (this.m_BizC.BizCode)
                {
                    case "0101":
                        #region һ������������֤�������
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>Ů��������Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        //sHtml.Append("<li>");
                        //sHtml.Append("<p class=\"title\">�Ƿ��Ƕ�����Ů</p>");
                        //sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds16 + "</p>");
                        //sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��λ</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds12 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>�з�������Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        //sHtml.Append("<li>");
                        //sHtml.Append("<p class=\"title\">�Ƿ��Ƕ�����Ů</p>");
                        //sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds17 + "</p>");
                        //sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��λ</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds05 + "</p>");
                        sHtml.Append("</li>"); ;
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>2������ڷ򸾺�Ӱ��Ƭ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">2������ڷ򸾺�Ӱ��Ƭ</p>");
                        sHtml.Append("<p class=\"text_con\">");
                        if (!string.IsNullOrEmpty(this.m_BizC.PersonPhotos))
                        { sHtml.Append("<img src=\"" + this.m_BizC.PersonPhotos + "\" width=\"50\" height=\"50\">"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��������</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��Ů���</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1���֤�Ż����ҽѧ֤��������֤��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2���֤�Ż����ҽѧ֤��������֤��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3���֤�Ż����ҽѧ֤��������֤��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>Ԥ��֤�ص�</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">Ԥ��֤�ص�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        #endregion
                        break;
                    case "0102":
                        #region ���������Ǽ�
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>Ů��������Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���壺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�Ƿ��Ƕ�����Ů��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds16 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��λ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds12 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>�з�������Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���壺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�Ƿ��Ƕ�����Ů��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds17 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��λ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds05 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>2������ڷ򸾺�Ӱ��Ƭ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">2������ڷ򸾺�Ӱ��Ƭ��</p>");
                        sHtml.Append("<p class=\"text_con\">");
                        if (!string.IsNullOrEmpty(this.m_BizC.PersonPhotos))
                        { sHtml.Append("<img src=\"" + this.m_BizC.PersonPhotos + "\" width=\"50\" height=\"50\">"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��������</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ɣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��Ů���</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1���֤�Ż����ҽѧ֤��������֤��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.FatherName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1ĸ������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.MotherName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2���֤�Ż����ҽѧ֤��������֤��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.FatherName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2ĸ������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.MotherName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3���֤�Ż����ҽѧ֤��������֤��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.FatherName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3ĸ������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.MotherName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>Ԥ��֤�ص�</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">Ԥ��֤�ص�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        #endregion
                        break;
                    case "0103":
                        #region ��������
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>�����˻�����Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�Ա�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds02 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");


                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��ż��Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�Ա�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds09 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");


                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>������Ů��</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������������Ů��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds20 + "&nbsp;��&nbsp;" + this.m_BizC.Fileds21 + "&nbsp;Ů</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�����д����Ů����������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds37 + "&nbsp;��&nbsp;" + this.m_BizC.Fileds38 + "&nbsp;Ů</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��������</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��Ů���</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName4))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource4 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName5))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource5 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName6))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource6 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>Ԥ��֤�ص�</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">Ԥ��֤�ص�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                      
                        #endregion
                        break;
                    case "0104":
                        #region �ر𽱷�
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>�����˻�����Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�Ա�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds02 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");


                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��ż��Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�Ա�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds09 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");


                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>������Ů��</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������������Ů��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds20 + "&nbsp;��&nbsp;" + this.m_BizC.Fileds21 + "&nbsp;Ů</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�����д����Ů����������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds37 + "&nbsp;��&nbsp;" + this.m_BizC.Fileds38 + "&nbsp;Ů</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�Ƿ���ȡ������Ů֤</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds22 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>�˲���Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�м�֤����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds23 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�м�֤����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds24 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�м��ȼ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds26 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��������</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��Ů���</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1���״��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��/��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��/��ȷ�ϵ�λ</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathAudit1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2���״��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��/��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��/��ȷ�ϵ�λ</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathAudit2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3���״��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��/��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��/��ȷ�ϵ�λ</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathAudit3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName4))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4���״��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4��/��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4��/��ȷ�ϵ�λ</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathAudit4 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName5))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5���״��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5��/��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5��/��ȷ�ϵ�λ</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathAudit5 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName6))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6���״��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6��/��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathday6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6��/��ȷ�ϵ�λ</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildDeathAudit6 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>Ԥ��֤�ص�</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">Ԥ��֤�ص�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        #endregion
                        break;
                    case "0105":
                        #region �����츻
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��ż��Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>���举Ů�ɷ���Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>������Ů��</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�����߿�������Ů��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds40 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������Ů������������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds37 + "&nbsp;��&nbsp;" + this.m_BizC.Fileds38 + "&nbsp;Ů</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��ϵ��ʽ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">2������ڷ򸾺�Ӱ��Ƭ</p>");
                        sHtml.Append("<p class=\"text_con\">" );
                        if (!string.IsNullOrEmpty(this.m_BizC.PersonPhotos))
                        { sHtml.Append("<img src=\"" + this.m_BizC.PersonPhotos + "\" width=\"50\" height=\"50\">"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>����״��</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���举Ů</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds45 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���举Ů�ɷ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds46 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���п�ʼ����</p>");
                        sHtml.Append("<p class=\"text_con\">" +BIZ_Common.FormatString(this.m_BizC.Fileds44, "2")+ "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��������</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��������</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��Ů���</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1���״��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2���״��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3���״��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName4))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4���״��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus4 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů4�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource4 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName5))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5���״��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus5 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů5�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource5 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName6))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6���״��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSurvivalStatus6 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů6�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource6 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>Ԥ����ص�</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">Ԥ����ص�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                       
                        #endregion
                        break;
                    case "0106":
                        #region ��������
                        sHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"2\">�����˻�����Ϣ</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�Ա�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds02 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־ӵ�ַ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"2\">��ż��Ϣ</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�Ա�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds09 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"2\">��������</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ɣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"2\">���������</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����ҽԺ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds27 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds28, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</table>");
                        sHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"5\">��Ů���</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<th height=\"32px\">��Ů����</p><p class=\"title\">�Ա�</p><p class=\"title\">��������</p><p class=\"title\">��������</p><p class=\"title\">�Ƿ�����</p>");
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
                        #region һ����
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>Ů����Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���壺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����̥�Σ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds41 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">ĩ���¾�ʱ�䣺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds42 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">ȷ�ϻ���ʱ�䣺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds43 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">ȷ�Ϸ�ʽ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds29 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">ȷ�ϵ�λ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds30 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>�з�������Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���壺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>���ʱ��</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���ʱ�䣺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>����֤��</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����֤�ţ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds21 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��������</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ɣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>Ԥ����ص�</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">Ԥ����ص�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        #endregion
                        break;
                    case "0108":
                        #region ������Ů��ĸ����֤
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>ĸ����Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���壺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��λ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds12 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>������Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���壺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��λ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds05 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>������Ů�븸ĸ��Ӱ2���������Ƭ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������Ů�븸ĸ��Ӱ2���������Ƭ��</p>");
                        sHtml.Append("<p class=\"text_con\">");
                        if (!string.IsNullOrEmpty(this.m_BizC.PersonPhotos))
                        { sHtml.Append("<img src=\"" + this.m_BizC.PersonPhotos + "\" width=\"50\" height=\"50\">"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��Ů��Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds16 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�Ա�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds17 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���壺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds40 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds44 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds20 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��������</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ɣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>Ԥ��֤�ص�</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">Ԥ��֤�ص�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        #endregion
                        break;
                    case "0109":
                        #region �������˿ڻ���֤��������
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��֤��</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�����䶯���ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��ż</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�����䶯���ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>���д�ʩ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���д�ʩ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds45 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>���˽���һ�������������Ƭ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���˽���һ�������������Ƭ��</p>");
                        sHtml.Append("<p class=\"text_con\">");
                        if (!string.IsNullOrEmpty(this.m_BizC.PersonPhotos))
                        { sHtml.Append("<img src=\"" + this.m_BizC.PersonPhotos + "\" width=\"50\" height=\"50\">"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��ŵ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ŵ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>�������</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1���֤��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��ḧ�����������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2���֤��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��ḧ�����������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3���֤��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��ḧ�����������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>Ԥ����ص�</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">Ԥ����ص�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        #endregion
                        break;
                    case "0110":
                        #region �������֤��
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>������Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�Ա�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds02 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���壺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������䣺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds45 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��ż��Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�Ա�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds09 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���壺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>�������</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>�������</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1���֤��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��ḧ�����������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2���֤��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��ḧ�����������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3���֤��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�Ƿ�����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSource3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��ḧ�����������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>Ԥ����ص�</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">Ԥ����ص�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        #endregion
                        break;
                    case "0111":
                        #region ��ֹ�������
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>���举Ů��Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���壺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���䣺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds29 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">ĩ���¾����ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds42 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���ܣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds43 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ԣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds30 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>�ɷ���Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���壺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��������</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ɣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>˫��������Ů���</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<th height=\"32px\">��Ů����</p><p class=\"title\">�Ա�</p><p class=\"title\">��������</p><p class=\"title\">��������</p><p class=\"title\">��ע</p>");
                        sHtml.Append("</li>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��ע</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��ע</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��ע</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        #endregion
                        break;
                    case "0122":
                        #region ����������
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>Ů��������Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds31 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���壺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�Ƿ��Ƕ�����Ů��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds16 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��λ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds12 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>�з�������Ϣ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds32 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���壺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤���룺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds33 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds34 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�Ƿ��Ƕ�����Ů��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds17 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������ַ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�־�ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��λ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds05 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>2������ڷ򸾺�Ӱ��Ƭ</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">2������ڷ򸾺�Ӱ��Ƭ��</p>");
                        sHtml.Append("<p class=\"text_con\">");
                        if (!string.IsNullOrEmpty(this.m_BizC.PersonPhotos))
                        { sHtml.Append("<img src=\"" + this.m_BizC.PersonPhotos + "\" width=\"50\" height=\"50\">"); }
                        sHtml.Append("</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��������</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ɣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds18 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ڣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + BIZ_Common.FormatString(this.m_BizC.Fileds19, "2") + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");

                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>��Ů���</div>");
                        sHtml.Append("<ul>");
                        if (!string.IsNullOrEmpty(children.ChildName1))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1���֤�Ż����ҽѧ֤��������֤��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.FatherName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1ĸ������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.MotherName1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy1 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů1�������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos1 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName2))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2���֤�Ż����ҽѧ֤��������֤��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.FatherName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2ĸ������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.MotherName2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy2 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů2�������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos2 + "</p>");
                            sHtml.Append("</li>");
                        }
                        if (!string.IsNullOrEmpty(children.ChildName3))
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3����</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�Ա�</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildSex3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildBirthday3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3���֤�Ż����ҽѧ֤��������֤��</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildCardID3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.FatherName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3ĸ������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.MotherName3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3��������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.ChildPolicy3 + "</p>");
                            sHtml.Append("</li>");
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">��Ů3�������</p>");
                            sHtml.Append("<p class=\"text_con\">" + children.Memos3 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        sHtml.Append("<div class=\"part_form\">");
                        sHtml.Append("<div class=\"part_title\"><span class=\"fr\">��</span>Ԥ��֤�ص�</div>");
                        sHtml.Append("<ul>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">Ԥ��֤�ص�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.GetAreaName + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</ul>");
                        sHtml.Append("</div>");
                        #endregion
                        break;
                    default:
                        #region ����ҵ������B
                        sHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"2\">�з�������Ϣ</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds01 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤�ţ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���壺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds03 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds04 + "</p>");
                        sHtml.Append("</li>");
                        if (this.m_BizC.Fileds04 == "ũת��")
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">ת��ʱ�䣺</p>");
                            sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds49 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�����أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameA + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������λ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds36 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"2\">Ů��������Ϣ</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds08 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���֤�ţ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.PersonCidB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ϵ�绰��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.ContactTelB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���壺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds10 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�������ʣ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds11 + "</p>");
                        sHtml.Append("</li>");
                        if (this.m_BizC.Fileds11 == "ũת��")
                        {
                            sHtml.Append("<li>");
                            sHtml.Append("<p class=\"title\">ת��ʱ�䣺</p>");
                            sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds50 + "</p>");
                            sHtml.Append("</li>");
                        }
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�����أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.RegAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��ס�أ�</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.CurAreaNameB + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">������λ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds12 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds06 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">���м�ͥ��Ů����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds07 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����״����</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds13 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">����ʱ�䣺</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds14 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">�ٻ�ʱ��</p>");
                        sHtml.Append("<p class=\"text_con\">" + this.m_BizC.Fileds15 + "</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("</table>");
                        sHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        sHtml.Append("<tr class=\"trtitle\">");
                        sHtml.Append("<td colspan=\"5\">��Ů���</p>");
                        sHtml.Append("</li>");
                        sHtml.Append("<li>");
                        sHtml.Append("<p class=\"title\">��Ů����</p><p class=\"title\">�Ա�</p><p class=\"title\">��������</p><p class=\"title\">��Դ</p><p class=\"title\">�Ƿ�������</p>");
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
                sHtml = new StringBuilder(); sHtml.Append("��ȡ������Ϣ����");
            }
            this.LiteralBizView.Text = sHtml.ToString();
            this.m_BizC = null;
            children = null;

            sHtml = null;
        }

        #endregion
    }
}


