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

namespace join.pms.web.RptInfo
{
    public partial class chatdata030141 : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;
        private string m_UserName;

        #region ������Ϣ
        public string str_RptTime = "";   //����·�
        public string str_AreaName = "";   //����λ
        public string str_AreaCode = "";    //���λ����
        public string str_SldHeader = "";    //������
        public string str_SldLeader = "";   //�����
        public string str_OprateDate = "";   //�����/��������
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();
            this.txt_RptTime.Attributes.Add("readonly", "true");

            if (!IsPostBack)
            {
                this.txt_RptTime.Text = DateTime.Now.ToString("yyyy��MM��");
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
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

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
                case "edit": // �༭
                    funcName = "�༭";
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // ɾ��
                    funcName = "ɾ��";
                    DelBasisInfo(m_ObjID);
                    break;
                case "view": // �鿴
                    funcName = "�鿴����";
                    ShowModInfo(m_ObjID);
                    break;
                default:
                    Response.Write(" <script>alert('����ʧ�ܣ���������') ;window.location.href='" + m_TargetUrl + "'</script>");
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; �˿ڼ������� &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }

        /// <summary>
        /// �ύ��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;

            string AreaCode = PageValidate.GetTrim(this.UcDataAreaSel1.GetAreaCode());
            string AreaName = PageValidate.GetTrim(this.UcDataAreaSel1.GetAreaName());
            string txt_RptTime = PageValidate.GetTrim(this.txt_RptTime.Text);
            string txt_UnitName = PageValidate.GetTrim(this.UcDataAreaSel1.GetAreaName());
            string txt_SldHeader = PageValidate.GetTrim(this.txt_SldHeader.Text);
            string txt_SldLeader = PageValidate.GetTrim(this.txt_SldLeader.Text);
            string txt_OprateDate = PageValidate.GetTrim(this.txt_OprateDate.Value);

            if (String.IsNullOrEmpty(txt_RptTime)) { strErr += "��ѡ�񱨱����·ݣ�\\n"; }
            if (String.IsNullOrEmpty(txt_UnitName)) { strErr += "���������λ��\\n"; }
            if (String.IsNullOrEmpty(txt_SldHeader)) { strErr += "�����븺���ˣ�\\n"; }
            if (String.IsNullOrEmpty(txt_SldLeader)) { strErr += "����������ˣ�\\n"; }
            if (String.IsNullOrEmpty(txt_OprateDate)) { strErr += "��ѡ�񱨳����ڣ�\\n"; }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            if (m_ActionName == "add")
            {
                m_SqlParams = "INSERT INTO [RPT_Basis](";
                m_SqlParams += "FuncNo,AreaCode,AreaName,RptName,UnitName,RptTime,SldHeader,SldLeader,UserID,RptUserName,CreateDate,OprateDate";
                m_SqlParams += ") VALUES(";
                m_SqlParams += "'" + m_FuncCode + "','" + AreaCode + "','" + AreaName + "','" + m_NavTitle + "','" + txt_UnitName + "','" + txt_RptTime + "','" + txt_SldHeader + "','" + txt_SldLeader + "','" + m_UserID + "','" + m_UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + txt_OprateDate + "'";
                m_SqlParams += ")";
            }
            else if (m_ActionName == "edit")
            {
                m_SqlParams = "UPDATE RPT_Basis SET ";
                m_SqlParams += "AreaCode='" + AreaCode + "',AreaName='" + AreaName + "',UnitName ='" + txt_UnitName + "',RptTime ='" + txt_RptTime + "',SldHeader ='" + txt_SldHeader + "',SldLeader ='" + txt_SldLeader + "',OprateDate ='" + txt_OprateDate + "'";
                m_SqlParams += " WHERE RptID='" + m_ObjID + "'";
            }
            try
            {
                DbHelperSQL.ExecuteSql(m_SqlParams);
                Response.Write(" <script>alert('"+m_NavTitle + "�ı�ͷ��Ϣ�����ɹ���') ;window.location.href='" + m_TargetUrl + "'</script>");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
        }

        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void DelBasisInfo(string objID)
        {
            try
            {
                try
                {
                    if (CommPage.GetSingleVal("select Attribs from RPT_Basis where RptID = '" + m_ObjID + "'") != "0")
                    {
                        MessageBox.ShowAndRedirect(this.Page, "����Ϣ�Ѿ��ϱ������ܲ�����", m_TargetUrl, true, true);
                        return;
                    }
                    else
                    {
                        m_SqlParams = "UPDATE RPT_Basis SET Attribs=4 WHERE RptID='" + objID + "'";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        MessageBox.ShowAndRedirect(this.Page, "�����ɹ�����ѡ�����Ϣɾ���ɹ���", m_TargetUrl, true, true);
                        return;
                    }
                }
                catch { }
            }
            catch { }
        }

        /// <summary>
        /// �޸� UcAreaSe08
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            bool isEdit = true;
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT * FROM [RPT_Basis] WHERE RptID='" + m_ObjID + "'";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    this.txt_RptTime.Text = PageValidate.GetTrim(sdr["RptTime"].ToString());
                    this.Areaname.Text = PageValidate.GetTrim(sdr["AreaName"].ToString());
                    this.UcDataAreaSel1.SetAreaName(PageValidate.GetTrim(sdr["AreaName"].ToString()));
                    this.UcDataAreaSel1.SetAreaCode(PageValidate.GetTrim(sdr["AreaCode"].ToString()));
                    this.txt_SldHeader.Text = sdr["SldHeader"].ToString();
                    this.txt_SldLeader.Text = sdr["SldLeader"].ToString();
                    this.txt_OprateDate.Value = DateTime.Parse(sdr["OprateDate"].ToString()).ToString("yyyy-MM-dd");

                    if (m_ActionName == "view") this.btnAdd.Visible = false;
                }
                sdr.Close();
                // �Ƿ�ɱ༭ SetAreaCode
                if (!isEdit)
                {
                    Response.Write(" <script>alert('����ʧ�ܣ���ѡ�����Ϣ����ͨ����ˡ������򹫿�����Ϣ������Ϣ������༭��') ;window.location.href='" + m_TargetUrl + "'</script>");
                }
            }
            catch { if (sdr != null) sdr.Close(); }
        }

        /// <summary>
        /// �����֤
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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
            else
            {
                //�����û�Id�õ��û���Ϣ
                str_AreaCode = CommPage.GetSingleVal("select UserAreaCode from USER_BaseInfo where UserID = " + m_UserID);
                str_AreaName = BIZ_Common.GetAreaName(str_AreaCode, "");
            }
        }


        /// <summary>
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_ActionName = Request.QueryString["action"];
            m_SourceUrl = Request.QueryString["sourceUrl"];
            m_ObjID = Request.QueryString["k"];
            if (m_ActionName == "view") m_ObjID = Request.QueryString["RID"];
            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = "030141";
                m_NavTitle = "���������߷������ͳ�Ʊ�";
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }
            if (!string.IsNullOrEmpty(m_ObjID))
            {
                string Attribs = CommPage.GetSingleVal("select Attribs from RPT_Basis where RptID = '" + m_ObjID + "'");
                if (Attribs == "0")
                {
                }
                else if (Attribs == "4")
                {
                    //��ɾ��
                    Response.Write("<script language='javascript'>alert('����Ϣ��ɾ�������ܽ��в�����');window.location.href='" + m_TargetUrl + "';</script>");
                    Response.End();
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('����Ϣ�Ѿ��ϱ���');;window.location.href='/UnvCommList.aspx?1=1&FuncCode=" + m_FuncCode + "&FuncNa=" + m_NavTitle + "'</script>");
                    Response.End();
                }
            }
        }
    }
}
