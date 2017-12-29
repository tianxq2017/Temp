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

using join.pms.dal;
using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.web.SysMsg
{
    public partial class NoteEdit : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_SqlParams;
        protected string m_TargetUrl;

        private string m_NavTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetOpratetionAction(m_NavTitle);
            }
        }
        #region

        /// <summary>
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                if (m_FuncCode == "0602") { m_NavTitle = "����Ͷ��"; }
                else if (m_FuncCode == "0603") { m_NavTitle = "������"; }
                else { m_NavTitle = "ҵ����ѯ"; }
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/loginTemp.aspx';</script>");
                Response.End();
            }
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
                    this.txtMsgUserName.Text = CommPage.GetSingleValue("SELECT UserName+'('+UserAccount+')' FROM USER_BaseInfo WHERE UserID=" + m_UserID);
                    break;
                case "edit": // �༭
                    funcName = "�޸�";
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // ɾ��
                    funcName = "";
                    DelInfo(m_ObjID);
                    break;
                case "bdel": // ����ɾ��,���,�Ƽ�,��ΪVIP,�����ҵ��ϸ��Ϣ
                    funcName = "";
                    BatchDelInfo(m_ObjID);
                    break;
                case "audit": // ���
                    funcName = "";
                    Audit(m_ObjID);
                    break;
                case "mask": // ����
                    funcName = "";
                    Mask(m_ObjID);
                    break;
                case "recomm": // �Ƽ�
                    funcName = "�Ƽ��ö�";
                    RecommInfo(m_ObjID);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                    break;
            }
            //this.LiteralNav.Text = "<a href=\"indexDesk.aspx\">��ʼҳ</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx.aspx\">��ʼҳ</a> &gt;&gt;���񻥶� &gt;&gt;" + funcName + " ��";
        }
        // Attribs: 0,δ���,����; 1,���,��ʾ ; 2,xx 3,���� 4,ɾ��*/
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="objID"></param>
        private void Audit(string objID)
        {
            if (PageValidate.IsNumber(objID))
            {
                try
                {
                    // Attribs: 0,δ���,����; 1,���,��ʾ ; 2,xx 3,���� 4,ɾ��
                    string cmsAttrib = DbHelperSQL.GetSingle("SELECT Attribs FROM MSG_Notes WHERE MsgID=" + objID).ToString();
                    if (!string.IsNullOrEmpty(cmsAttrib))
                    {
                        if (cmsAttrib == "0" || cmsAttrib == "3") { m_SqlParams = "UPDATE MSG_Notes SET Attribs=1 WHERE MsgID=" + objID; }
                        else if (cmsAttrib == "1") { m_SqlParams = "UPDATE MSG_Notes SET Attribs=0 WHERE MsgID=" + objID; }
                        else
                        {
                            MessageBox.ShowAndRedirect(this.Page, "������ʾ���޷���ɲ�����", m_TargetUrl, true);
                            return;
                        }

                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ���/ȡ����˲����ɹ���", m_TargetUrl, true);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ���������", m_TargetUrl, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                 MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ����¼�������Զ�ѡ��", m_TargetUrl, true);
            }
        }

        private void Mask(string objID)
        {
            if (PageValidate.IsNumber(objID))
            {
                try
                {
                    // Attribs: 0,δ���,����; 1,���,��ʾ ; 2,xx 3,���� 4,ɾ��
                    string cmsAttrib = DbHelperSQL.GetSingle("SELECT Attribs FROM MSG_Notes WHERE MsgID=" + objID).ToString();
                    if (!string.IsNullOrEmpty(cmsAttrib))
                    {
                        if (cmsAttrib != "3") { m_SqlParams = "UPDATE MSG_Notes SET Attribs=3 WHERE MsgID=" + objID; }
                        else if (cmsAttrib == "3") { m_SqlParams = "UPDATE MSG_Notes SET Attribs=0 WHERE MsgID=" + objID; }
                        else
                        {
                            MessageBox.ShowAndRedirect(this.Page, "������ʾ���޷���ɲ�����", m_TargetUrl, true);
                            return;
                        }

                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ����/ȡ�����β����ɹ���", m_TargetUrl, true);
                    }
                    else
                    {
                         MessageBox.ShowAndRedirect(this.Page, "������ʾ���������", m_TargetUrl, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ����¼�������Զ�ѡ��", m_TargetUrl, true);
            }
        }
        /// <summary>
        /// �Ƽ��ö�
        /// </summary>
        /// <param name="objID"></param>
        private void RecommInfo(string objID)
        {
            if (PageValidate.IsNumber(objID))
            {
                try
                {
                    // Attribs: 0,δ���,����; 1,���,��ʾ ; 2,�Ƽ� 3,���� 4,ɾ��
                    string cmsAttrib = DbHelperSQL.GetSingle("SELECT Attribs FROM MSG_Notes WHERE MsgID=" + objID).ToString();
                    if (!string.IsNullOrEmpty(cmsAttrib))
                    {
                        if (cmsAttrib == "2") { m_SqlParams = "UPDATE MSG_Notes SET Attribs=0 WHERE MsgID=" + objID; }
                        else { m_SqlParams = "UPDATE MSG_Notes SET Attribs=2 WHERE MsgID=" + objID; }
                        //else
                        //{
                        //    Response.Write(" <script>alert('������ʾ���޷���ɲ�����') ;window.location.href='" + m_TargetUrl + "'</script>");
                        //    return;
                        //}

                        DbHelperSQL.ExecuteSql(m_SqlParams);
                         MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ�ö�/ȡ���ö������ɹ���", m_TargetUrl, true);
                    }
                    else
                    {
                         MessageBox.ShowAndRedirect(this.Page, "������ʾ���������", m_TargetUrl, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ����¼�������Զ�ѡ��", m_TargetUrl, true);
            }
        }

        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT MsgTitle,MsgBody FROM MSG_Notes WHERE MsgID=" + objID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    this.txtMsgTitle.Text = sdr["MsgTitle"].ToString();
                    this.objCKeditor.Text = PageValidate.Decode(sdr["MsgBody"].ToString());
                }
                sdr.Close();
            }
            catch
            {
                if (sdr != null) sdr.Close();
            }
        }

        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            try
            {
                //  Attribs: 0,δ���,����; 1,���,��ʾ ; 2,xx 3,���� 4,ɾ��
                if (!string.IsNullOrEmpty(objID))
                {
                    //string[] msgID = objID.Split(',');
                    //for (int i = 0; i < msgID.Length; i++)
                    //{
                    //ɾ�� �ظ�
                    //DbHelperSQL.ExecuteSql("DELETE FROM MSG_NotesRe WHERE [MsgID] in(" + msgID[i] + ")");
                    //m_SqlParams = "DELETE FROM MSG_Notes WHERE [MsgID] =(" + msgID[i] + ")";
                    //DbHelperSQL.ExecuteSql(m_SqlParams);
                    m_SqlParams = "UPDATE MSG_Notes SET Attribs=4 WHERE [MsgID] IN(" + objID + ")";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                     MessageBox.ShowAndRedirect(this.Page, "������ʾ��ɾ���ɹ���", m_TargetUrl, true);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
            }
        }
        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="objID"></param>
        private void BatchDelInfo(string objID)
        {
            try
            {
                m_SqlParams = "UPDATE MSG_Notes SET Attribs=4 WHERE MsgID IN(" + objID + ")";
                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.ShowAndRedirect(this.Page, "������ʾ��ɾ���ɹ���", m_TargetUrl, true);
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
            string MsgTitle = PageValidate.Encode(PageValidate.GetTrim(this.txtMsgTitle.Text));
            string MsgBody = PageValidate.Encode(CommBiz.GetTrim(this.objCKeditor.Text));
            string MsgUserName = PageValidate.Encode(PageValidate.GetTrim(this.txtMsgUserName.Text));
            string MsgIP = Request.UserHostAddress;
            //MsgTitle,MsgBody txtMsgUserName
            if (string.IsNullOrEmpty(MsgTitle)) strErr += "��������⣡\\n";
            if (string.IsNullOrEmpty(MsgBody)) strErr += "���������ݣ�\\n";

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            try
            {
                /*
                 
                 MsgID, MsgCode, MsgTitle, MsgBody, MsgUserID, MsgUserName, MsgIP, 
      MsgClick, Attribs, OprateDate,
                 */
                if (m_ActionName == "add")
                {
                    m_SqlParams = "INSERT INTO [MSG_Notes](MsgCode,MsgTitle,MsgBody,MsgUserName,MsgIP,MsgUserID) VALUES('" + this.m_FuncCode + "','" + MsgTitle + "', '" + MsgBody + "','" + MsgUserName + "','" + MsgIP + "'," + m_UserID + ")";

                }
                else
                {
                    m_SqlParams = "UPDATE MSG_Notes SET MsgTitle='" + MsgTitle + "',MsgUserName='" + MsgUserName + "',MsgBody='" + MsgBody + "' WHERE MsgID=" + m_ObjID;

                }

                DbHelperSQL.ExecuteSql(m_SqlParams);

                MessageBox.ShowAndRedirect(this.Page, "������ʾ��ɾ���ɹ���", m_TargetUrl, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }

        }

    }
}

