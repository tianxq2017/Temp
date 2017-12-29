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

using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.web.SysMsg
{
    public partial class MsgEdit : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_SqlParams;
        private DataTable m_Dt;

        protected string m_TargetUrl;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;
        protected string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                if (String.IsNullOrEmpty(m_FuncCode) || !PageValidate.IsNumber(m_FuncCode))
                {
                    Response.Write("�Ƿ����ʣ���������ֹ��");
                    Response.End();
                }
                else
                {
                    SetOpratetionAction("ϵͳ��Ѷ");
                }
            }
        }

        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                // "pSearch=E31E58FB2A760DEA&UserID=1&FuncUser=&FuncCode=0901&FuncNa=%e5%8f%91%e4%bb%b6%e7%ae%b1&p=1"
                // http://localhost:3481/SysMsg/MsgEdit.aspx?action=add&BizID=&sourceUrl=21294AC877F04FA8A12CB4CCC5BF96DEDED85D64BB57D77921AB69D73233AE8AEA531F74C16B64FF51FFC00172A0C9B1AC42342848D256BCBF324ECAADFEAE10FAE87D559C6F9759176DC1204C469C487C91699C2A221898AA4119DBAFABE0ABFA5BADA99521031B
                // http://localhost:3481/UnvCommList.aspx?1=1&FuncCode=0901&FuncNa=%e5%8f%91%e4%bb%b6%e7%ae%b1
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                this.txtFileUrl.Value = m_SvrsUrl;
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
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
        }

        #region ҳ���״μ���ʱ��ʾ����Ϣ
        /// <summary>
        /// ���ò�����Ϊ add����,edit�༭,delɾ��,4�鿴,5���,6�����ɫ ��
        /// </summary>
        /// <param name="oprateName">��������</param>
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
                    funcName = "������Ѷ";
                    //ShowAddInfo();
                    break;
                case "view": // �鿴
                    funcName = "��Ϣ�鿴";
                    //ShowModInfo(m_ObjID);
                    break;
                case "edit": // �༭
                    funcName = "�޸��û���Ϣ";
                    //ShowModInfo(m_ObjID);
                    break;
                case "del": // ɾ��
                    funcName = "ɾ����Ϣ";
                    DelInfo(m_ObjID);
                    break;
                default:
                    if (Request.UrlReferrer != null) Response.Redirect(Request.UrlReferrer.ToString());
                    break;
            }

            //this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
            this.LiteralNav.Text = "������ҳ &gt;&gt; " + oprateName + " &gt;&gt; " + funcName + "��";
        }

        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            try
            {
                //0601	������ SourceDel 0,���ͷ�ɾ�� TargetDel 1,���շ�ɾ�� SourceDel TargetDel
                m_SqlParams = string.Empty;
                if (m_FuncCode == "0902") { m_SqlParams = "UPDATE SYS_Msg SET TargetDel=1 WHERE MsgID IN(" + objID + ")"; }
                else if (m_FuncCode == "0901") { m_SqlParams = "UPDATE SYS_Msg SET SourceDel=1 WHERE MsgID IN(" + objID + ")"; }
                else { m_SqlParams = "DELETE FROM SYS_Msg WHERE MsgID IN(" + objID + ") AND MsgType!=0"; }

                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "������ʾ�������ɹ���", m_TargetUrl, true);

            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��" + ex.Message, m_TargetUrl, true);
            }
            Response.End();
        }
        #endregion

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string MsgTitle = PageValidate.GetTrim(this.txtMsgTitle.Text);
            string MsgBody = PageValidate.Encode(join.pms.dal.CommBiz.GetTrim(this.objCKeditor.Text));
            string TargetUserIDs = PageValidate.GetTrim(this.txtExecUserID.Value); ;
            string incDocsID = "";

            if (MsgTitle == "")
            {
                strErr += "��Ϣͷ����Ϊ�գ�\\n";
            }
            if (MsgBody == "")
            {
                strErr += "��Ϣ�岻��Ϊ�գ�\\n";
            }
            if (TargetUserIDs == "")
            {
                strErr += "��ѡ������ˣ�\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            try
            {
                System.Collections.Generic.List<string> list = null; ;
                string[] aryUsers = TargetUserIDs.Split(',');
                list = new System.Collections.Generic.List<string>(aryUsers.Length + 2);
                for (int i = 0; i < aryUsers.Length; i++)
                {
                    list.Add("INSERT INTO [SYS_Msg]([SourceUserID], [TargetUserID], [MsgTitle], [MsgBody], [MsgType],[DocID]) VALUES(" + m_UserID + ", " + aryUsers[i] + ", '" + MsgTitle + "', '" + MsgBody + "', 2,'" + incDocsID + "')");
                }
                //UpdateIncDocs(incDocsID, reValue.ToString());
                if (DbHelperSQL.ExecuteSqlTran(list) > 0)
                {
                    list = null;
                    //Response.Redirect(txtUrlParams.Value);
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ͳɹ���", m_TargetUrl, true,true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, ex.Message, m_TargetUrl, true, true);
            }
        }

    }
}
