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

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using System.Text;
using join.pms.dal;

namespace join.pms.web.SysAdmin
{
    public partial class SysPeopleStatEdit : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_ActionName;
        private string m_ObjID;
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_SqlParams;
        private string m_TargetUrl;

        private DataTable m_Dt;
        private string m_FuncCode;
        private string m_FuncName;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetOpratetionAction(m_FuncName);
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
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                m_FuncName = CommPage.GetFuncName(m_FuncCode);//�������
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        #region ҳ���״μ���ʱ��ʾ����Ϣ


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
                    funcName = "�޸�";
                    ShowModInfo(m_ObjID);
                    break;
                case "del": // ɾ��
                    funcName = "ɾ������";
                    DelInfo(m_ObjID);
                    break;
                case "view": // �鿴
                    funcName = "�鿴����";
                    break;
                case "audit": // ���
                    funcName = "�������";
                    break;
                default:
                     MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = GetPageTopNavInfo(oprateName, funcName);
            this.LiteralTitle.Text = oprateName;
            this.LiteralFuncName.Text = funcName;
        }
        /// <summary>
        /// ��ȡҳ�浼����Ϣ
        /// </summary>
        /// <param name="oprateName"></param>
        /// <param name="funcName"></param>
        /// <returns></returns>
        private string GetPageTopNavInfo(string oprateName, string funcName)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"rnavbg\">");
            sb.Append("<tr><td width=\"20\" style=\"background-image:url(/images/CMS/right_topbg.jpg);border:0px;background-repeat:repeat-x;\">&nbsp;</td><td class=\"rnav01\" style=\"background-image:url(/images/CMS/right_topbg.jpg);border:0px;background-repeat:repeat-x;\">" + CommPage.GetAllTreeName(this.m_FuncCode) + oprateName + " &gt; " + funcName + "</td></tr>");
            sb.Append("</table>");
            return sb.ToString();
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


        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="objID"></param>
        private void ShowModInfo(string objID)
        {
            DataTable tempDT = new DataTable();
            m_SqlParams = "SELECT [CommID], [UintName], [CZRenKou], [YLFunNv], [YHFunNv], [BYRenShu], [CSRenKou], [SWRenKou], [LCRenKou], [LRRenKou], [Remarks] FROM [Sys_PeopleStat] WHERE CommID=" + m_ObjID;
            try
            {
                tempDT = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (tempDT.Rows.Count == 1)
                {
                    this.txtUintName.Text = tempDT.Rows[0]["UintName"].ToString();
                    this.txtCZRenKou.Text = tempDT.Rows[0]["CZRenKou"].ToString();
                    this.txtYLFunNv.Text = tempDT.Rows[0]["YLFunNv"].ToString();
                    this.txtYHFunNv.Text = tempDT.Rows[0]["YHFunNv"].ToString();
                    this.txtBYRenShu.Text = tempDT.Rows[0]["BYRenShu"].ToString();
                    this.txtCSRenKou.Text = tempDT.Rows[0]["CSRenKou"].ToString();
                    this.txtSWRenKou.Text = tempDT.Rows[0]["SWRenKou"].ToString();
                    this.txtLCRenKou.Text = tempDT.Rows[0]["LCRenKou"].ToString();
                    this.txtLRRenKou.Text = tempDT.Rows[0]["LRRenKou"].ToString();
                    this.txtRemarks.Text = PageValidate.Decode(tempDT.Rows[0]["Remarks"].ToString());

                }
                if (m_ActionName == "view") this.btnAdd.Enabled = false;
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
            m_SqlParams = null;
            tempDT = null;
        }
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            if (PageValidate.IsNumber(m_ObjID))
            {
                try
                {
                    // 0,Ĭ��,δ��� 1,ϵͳ��������ֹɾ�� 2,���ͨ�� 3,��˲��� 4,ɾ�� 9,�Ƽ�
                    m_SqlParams = "DELETE FROM Sys_PeopleStat WHERE [CommID]=" + objID + "";
                    DbHelperSQL.ExecuteSql(m_SqlParams);                 
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����ѡ�����Ϣ���ɹ�ɾ����", m_TargetUrl, true);
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

        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //����
            string strErr = string.Empty;
            string UintName = PageValidate.GetTrim(this.txtUintName.Text);
            string CZRenKou = PageValidate.GetTrim(this.txtCZRenKou.Text);
            string YLFunNv = PageValidate.GetTrim(this.txtYLFunNv.Text);
            string YHFunNv = PageValidate.GetTrim(this.txtYHFunNv.Text);
            string BYRenShu = PageValidate.GetTrim(this.txtBYRenShu.Text);
            string CSRenKou = PageValidate.GetTrim(this.txtCSRenKou.Text);
            string SWRenKou = PageValidate.GetTrim(this.txtSWRenKou.Text);
            string LCRenKou = PageValidate.GetTrim(this.txtLCRenKou.Text);
            string LRRenKou = PageValidate.GetTrim(this.txtLRRenKou.Text);
            string Remarks = PageValidate.Encode(this.txtRemarks.Text);
            if (String.IsNullOrEmpty(UintName))
            {
                strErr += "��λ���Ʋ���Ϊ�գ�\\n";
            }
            if (String.IsNullOrEmpty(CZRenKou))
            {
                strErr += "��ĩ��ס�˿ڲ���Ϊ�գ�\\n";
            }
            if (String.IsNullOrEmpty(YLFunNv))
            {
                strErr += "��ĩ���举Ů��������Ϊ�գ�\\n";
            }
            if (String.IsNullOrEmpty(YHFunNv))
            {
                strErr += "��ĩ�ѻ����举Ů������Ϊ�գ�\\n";
            }
            if (String.IsNullOrEmpty(BYRenShu))
            {
                strErr += "��ȡ���ֱ��д�ʩ��������Ϊ�գ�\\n";
            }
            if (String.IsNullOrEmpty(CSRenKou))
            {
                strErr += "������������Ϊ�գ�\\n";
            }
            if (String.IsNullOrEmpty(SWRenKou))
            {
                strErr += "������������Ϊ�գ�\\n";
            }
            if (String.IsNullOrEmpty(LCRenKou))
            {
                strErr += "�����˿ڲ���Ϊ�գ�\\n";
            }
            if (String.IsNullOrEmpty(LRRenKou))
            {
                strErr += "�����˿ڲ���Ϊ�գ�\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            if (String.IsNullOrEmpty(m_UserID))
            {
                MessageBox.Show(this, "������ʱ,���˳�ϵͳ���µ�¼�����ԣ�");
                return;
            }
            m_SqlParams = null;

            if (m_ActionName == "add")
            {
                try
                {
                    m_SqlParams = "INSERT INTO [Sys_PeopleStat]([UintName], [CZRenKou], [YLFunNv],";
                    m_SqlParams += "[YHFunNv], [BYRenShu], [CSRenKou], [SWRenKou], ";
                    m_SqlParams += "[LCRenKou], [LRRenKou], [Remarks], [UserID])";
                    m_SqlParams += "VALUES('" + UintName + "','" + CZRenKou + "','" + YLFunNv + "',";
                    m_SqlParams += "'" + YHFunNv + "','" + BYRenShu + "','" + CSRenKou + "','" + SWRenKou + "',";
                    m_SqlParams += "'" + LCRenKou + "','" + LRRenKou + "','" + Remarks + "'," + this.m_UserID + ")";
                   
                    m_SqlParams += "SELECT SCOPE_IDENTITY()";
                    string objID = DbHelperSQL.GetSingle(m_SqlParams).ToString();

                    MessageBox.ShowAndRedirect(this.Page, "������ʾ��[" + UintName + "]�˿ڻ�����Ϣ�����ɹ���", txtUrlParams.Value, true);

                }
                catch (Exception ex) { MessageBox.Show(this, ex.Message); }
            }
            else if (m_ActionName == "edit")
            {
                try
                {
                    m_SqlParams = "UPDATE Sys_PeopleStat SET [UintName]='" + UintName + "' , [CZRenKou]='" + CZRenKou + "' , [YLFunNv]='" + YLFunNv + "' ,[YHFunNv]='" + YHFunNv + "' , [BYRenShu]='" + BYRenShu + "' , [CSRenKou]='" + CSRenKou + "' , [SWRenKou]='" + SWRenKou + "' ,[LCRenKou]='" + LCRenKou + "' , [LRRenKou]='" + LRRenKou + "' , [Remarks]='" + Remarks + "'  WHERE CommID=" + m_ObjID;

                    DbHelperSQL.ExecuteSql(m_SqlParams);
                     MessageBox.ShowAndRedirect(this.Page, "������ʾ��[" + UintName + "]�˿ڻ�����Ϣ�༭�ɹ���", m_TargetUrl, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                    return;
                }
                Response.Redirect(txtUrlParams.Value);
            }
        }
    }
}
