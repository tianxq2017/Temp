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

namespace join.pms.web.RptInfo
{
    public partial class chatdata03013102 : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        public string m_ActionName;
        private string m_ObjID;

        public string IsReported = "0";   //�ж��Ƿ��ϱ�  0����ʾδ�ϱ� 1����ʾ�ϱ�

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;
        private string m_UserName;
        public string countyparam = "";   //��������Ĳ���

        #region ������Ϣ
        public string str_RptTime = "";   //����·�
        public string str_UnitName = "";   //����λ
        public string str_SldHeader = "";    //������
        public string str_SldLeader = "";   //�����
        public string str_OprateDate = "";   //�����/��������
        #endregion

        public string url = "";
        public string rptID = "";
        public string pub_Data_Code = "030101";

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();


            url = Request.RawUrl;
            DataBind(m_ObjID);

            //���ݱ���õ���ͷ��Id
            DataTable table_1 = DbHelperSQL.Query("select RptID from RPT_Basis where FuncNo='" + pub_Data_Code + "' and RPT_status = 1").Tables[0];

            if (table_1.Rows.Count > 0)
            {
                foreach (DataRow item_1 in table_1.Rows)
                {
                    rptID += item_1["RptID"].ToString() + ",";
                }
            }
            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                SetOpratetionAction(m_NavTitle);
                if (url.IndexOf("param=") > -1)
                {
                    m_ObjID = Request.QueryString["param"];
                    this.txt_RptTime.Disabled = false;
                    this.txt_SldHeader.Enabled = false;
                    this.txt_SldLeader.Enabled = false;
                    this.txt_OprateDate.Disabled = true;
                }

                //����Id�õ���Ϣ�Ƿ����
                if (rptID != "")
                {
                    DataTable table_2 = DbHelperSQL.Query("select Fileds29 from RPT_Contents where RptID in (" + rptID.TrimEnd(',') + ")").Tables[0];
                    string fileds29 = "";
                    if (table_2.Rows.Count > 0)
                    {
                        foreach (DataRow item_2 in table_2.Rows)
                        {
                            fileds29 += item_2["Fileds29"].ToString();
                        }
                    }
                    if (fileds29.IndexOf("0") > -1)
                    {
                        IsReported = "0";
                    }
                    else
                    {
                        IsReported = "1";
                    }

                    if (IsReported == "1")
                    {
                        this.txt_RptTime.Disabled = false;
                        this.txt_SldHeader.Enabled = false;
                        this.txt_SldLeader.Enabled = false;
                        this.txt_OprateDate.Disabled = true;
                    }
                }
                this.txt_RptTime.Value = DateTime.Now.ToString("yyyy��MM��");
            }
            else
            {
                //�޸ı�ͷ��Ϣ
                string txt_RptTime = PageValidate.GetTrim(this.txt_RptTime.Value);    //�·�
                string txt_SldHeader = PageValidate.GetTrim(this.txt_SldHeader.Text);
                string txt_SldLeader = PageValidate.GetTrim(this.txt_SldLeader.Text);
                string txt_OprateDate = PageValidate.GetTrim(this.txt_OprateDate.Value);


                string AreaCode = join.pms.dal.CommPage.GetSingleVal("select AreaCode from RPT_Basis where AreaName = '" + str_UnitName + "'");

                string subAreaCode = AreaCode.Substring(0, 9) + "000";
                try
                {
                    //�޸ı�ͷ��Ϣ
                    m_SqlParams = "UPDATE RPT_Basis SET ";
                    m_SqlParams += "RptTime='" + txt_RptTime + "',SldHeader='" + txt_SldHeader + "',SldLeader ='" + txt_SldLeader + "',OprateDate ='" + txt_OprateDate + "'";
                    m_SqlParams += " WHERE RptID=" + m_ObjID;
                    DbHelperSQL.ExecuteSql(m_SqlParams);


                    string sqlArea = "select * from RPT_Basis where AreaCode = '" + subAreaCode + "'";
                    DataTable dt = DbHelperSQL.Query(sqlArea).Tables[0];
                    //����������ѯ�򼶱�ͷ�Ƿ񴴽�
                    if (dt.Rows.Count <= 0)
                    {
                        //����ȥ������õ���������
                        string subAreaName = join.pms.dal.CommPage.GetSingleVal("select AreaName from AreaDetailCN where AreaCode = '" + subAreaCode + "'");
                        //�����򼶵ı�ͷ
                        m_SqlParams = "INSERT INTO [RPT_Basis](";
                        m_SqlParams += "FuncNo,AreaCode,AreaName,RptName,UnitName,RptTime,SldHeader,SldLeader,UserID,RptUserName,CreateDate,OprateDate";
                        m_SqlParams += ") VALUES(";
                        m_SqlParams += "'030131','" + subAreaCode + "','" + subAreaName + "','" + m_NavTitle + "','" + subAreaName + "','" + Convert.ToDateTime(DateTime.Now).ToString("yyyy��MM��") + "','','','" + m_UserID + "','" + m_UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                        m_SqlParams += ")";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                    }

                    //�޸ı�ͷ�е���Ϣ
                    string sql_status = "UPDATE RPT_Basis SET RPT_status=1 WHERE RptID=" + m_ObjID;

                    //�ϱ���Ϣ
                    string sql = "UPDATE RPT_Contents SET Fileds29='1' WHERE RptID=" + m_ObjID; ;
                    if (this.ck_IsCheck.Checked)
                    {
                        try
                        {
                            if (txt_SldHeader == "" || txt_SldLeader == "")
                            {
                                MessageBox.Show(this, "�����Ƹ����˻����������Ϣ��");
                                return;
                            }
                            else
                            {
                                DbHelperSQL.ExecuteSql(sql_status);
                                DbHelperSQL.ExecuteSql(sql);
                                Response.Write(" <script>alert('�˿ڶ�̬��Ϣ���浥<һ>��˳ɹ��ɹ���') ;window.location.href='" + m_TargetUrl + "'</script>");
                            }
                        }
                        catch
                        {
                            Response.Write(" <script>alert('�˿ڶ�̬��Ϣ���浥<һ>�������ʧ�ܣ�') ;window.location.href='" + m_TargetUrl + "'</script>");
                            return;
                        }
                    }
                    else
                    {
                        Response.Write(" <script>alert('��ȷ����ˣ�') ;</script>");
                        return;
                    }
                }
                catch
                { }
            }
        }

        /// <summary>
        /// ���ò�����Ϊ
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;
            switch (m_ActionName)
            {
                case "tb_ldadd": // ����
                    funcName = "����";
                    break;
                case "view": // �鿴
                    funcName = "�鿴����";
                    break;
                default:
                    Response.Write(" <script>alert('����ʧ�ܣ���������') ;window.location.href='" + m_TargetUrl + "'</script>");
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; �˿ڼ������� &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="m_Id"></param>
        private void DataBind(string m_Id)
        {
            DataTable table = new DataTable();
            SqlDataReader sdr = null;
            if (m_Id != "")
            {
                //����id�õ�������Ϣ
                string sql_basisInfo = "select * from RPT_Basis where RptID = " + m_Id;
                try
                {
                    sdr = DbHelperSQL.ExecuteReader(sql_basisInfo);
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            this.txt_RptTime.Value = sdr["RptTime"].ToString();
                            str_UnitName = sdr["UnitName"].ToString();
                            this.txt_SldHeader.Text = sdr["SldHeader"].ToString();
                            this.txt_SldLeader.Text = sdr["SldLeader"].ToString();
                            this.txt_OprateDate.Value = DateTime.Parse(sdr["OprateDate"].ToString()).ToString("yyyy-MM-dd");
                        }
                    }
                }
                catch
                { }
                finally
                {
                    if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                }

                //���ݱ���õ���ͷ��Id
                string rptID = "";
                try
                {
                    DataTable table_1 = DbHelperSQL.Query("select RptID from RPT_Basis where FuncNo='" + pub_Data_Code + "'  and RPT_status = 1").Tables[0];
                    if (table_1.Rows.Count > 0)
                    {
                        foreach (DataRow item_1 in table_1.Rows)
                        {
                            rptID += item_1["RptID"].ToString() + ",";
                        }
                    }
                }
                catch
                { }

                //����Id�õ��˿���������Ϣ
                string sqlall = "select * from RPT_Contents where Content_Type=1 and RptID =" + rptID.TrimEnd(',') + " order by CreateDate desc";
                try
                {
                    table = DbHelperSQL.Query(sqlall).Tables[0];
                }
                catch
                { }

                this.rep_Data.DataSource = table;
                this.rep_Data.DataBind();
                table = null;
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
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_ActionName = Request.QueryString["action"];
            m_SourceUrl = Request.QueryString["sourceUrl"];
            m_ObjID = Request.QueryString["k"];

            countyparam = Request.QueryString["param"];

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                m_NavTitle = "�˿ڶ�̬��Ϣ���浥<һ>";
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = Request.QueryString["RID"];
        }
    }
}
