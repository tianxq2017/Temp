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
    public partial class chatdata03011902 : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        public string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;
        private string m_UserName;

        public string IsReported = "0";   //�ж��Ƿ����  0����ʾδ��� 1����ʾ���

        #region ������Ϣ
        public string str_RptTime = "";   //����·�
        public string str_UnitName = "";   //����λ
        public string str_SldHeader = "";    //������
        public string str_SldLeader = "";   //�����
        public string str_OprateDate = "";   //�����/��������
        #endregion

        public string url = "";
        public string rptID = "";

        #region ͳ�ƺϼ�
        public int num1 = 0;    //��������
        public int num2 = 0;    //�����ƻ���һ������
        public int num3 = 0;    //�����ƻ��ڶ�������
        public int num4 = 0;    //�����ƻ���һ������
        public int num5 = 0;    //�����ƻ����������
        public int num6 = 0;    //�����ƻ���ຢ����
        public int num7 = 0;
        public int num8 = 0;
        public int num9 = 0;
        public int num10 = 0;
        public int num11 = 0;
        public int num12 = 0;
        public int num13 = 0;
        public int num14 = 0;
        public int num15 = 0;
        public int num16 = 0;
        public int num17 = 0;
        public int num18 = 0;
        public int num19 = 0;
        public int num20 = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();


            url = Request.RawUrl;
            //���ݱ���õ���ͷ��Id
            DataTable table_1 = DbHelperSQL.Query("select RptID from RPT_Basis where FuncNo='030118'").Tables[0];

            if (table_1.Rows.Count > 0)
            {
                foreach (DataRow item_1 in table_1.Rows)
                {
                    rptID += item_1["RptID"].ToString() + ",";
                }
            }

            if (!IsPostBack)
            {
                //����Id�õ���Ϣ�Ƿ����
                DataTable table_2 = DbHelperSQL.Query("select Fileds28 from RPT_Contents where RptID in (" + rptID.TrimEnd(',') + ")").Tables[0];
                string fileds28 = "";
                if (table_2.Rows.Count > 0)
                {
                    foreach (DataRow item_2 in table_2.Rows)
                    {
                        fileds28 += item_2["Fileds28"].ToString();
                    }
                }
                if (fileds28.IndexOf("0") > -1)
                {
                    IsReported = "0";
                }
                else
                {
                    IsReported = "1";
                }

                if (IsReported == "1")
                {
                    this.dr_RptTime.Enabled = false;
                    this.txt_SldHeader.Enabled = false;
                    this.txt_SldLeader.Enabled = false;
                    this.txt_OprateDate.Disabled = true;
                }

                this.dr_RptTime.SelectedValue = DateTime.Now.Month.ToString();
                DataBind(m_ObjID);
                SetPageStyle(m_UserID);
                SetOpratetionAction(m_NavTitle);

                DataTable table = new DataTable();
                foreach (string str_rptID in rptID.TrimEnd(',').Split(','))
                {
                    //string sql = "select * from RPT_Contents where RptID = '" + objID + "'";
                    string sql = "select * from RPT_Contents where RptID = '" + str_rptID+"'";
                    try
                    {
                        table = DbHelperSQL.Query(sql).Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            foreach (DataRow item in table.Rows)
                            {
                                num1 += Convert.ToInt32(item["Fileds01"].ToString()) + Convert.ToInt32(item["Fileds02"].ToString());
                                num2 += Convert.ToInt32(item["Fileds01"].ToString());
                                num3 += Convert.ToInt32(item["Fileds02"].ToString());
                                num4 += Convert.ToInt32(item["Fileds03"].ToString()) + Convert.ToInt32(item["Fileds04"].ToString());
                                num5 += Convert.ToInt32(item["Fileds03"].ToString());
                                num6 += Convert.ToInt32(item["Fileds04"].ToString());
                                num7 += Convert.ToInt32(item["Fileds05"].ToString()) + Convert.ToInt32(item["Fileds06"].ToString());
                                num8 += Convert.ToInt32(item["Fileds05"].ToString());
                                num9 += Convert.ToInt32(item["Fileds06"].ToString());
                                num10 += Convert.ToInt32(item["Fileds07"].ToString()) + Convert.ToInt32(item["Fileds08"].ToString()) + Convert.ToInt32(item["Fileds09"].ToString()) + Convert.ToInt32(item["Fileds10"].ToString());
                                num11 += Convert.ToInt32(item["Fileds07"].ToString());
                                num12 += Convert.ToInt32(item["Fileds08"].ToString());
                                num13 += Convert.ToInt32(item["Fileds09"].ToString());
                                num14 += Convert.ToInt32(item["Fileds10"].ToString());
                                num15 += Convert.ToInt32(item["Fileds11"].ToString());
                                num16 += Convert.ToInt32(item["Fileds12"].ToString());
                                num17 += Convert.ToInt32(item["Fileds13"].ToString());
                                num18 += Convert.ToInt32(item["Fileds14"].ToString());
                                num19 += Convert.ToInt32(item["Fileds15"].ToString());
                                num20 += Convert.ToInt32(item["Fileds16"].ToString());
                            }
                        }
                    }
                    catch
                    { }
                    table = null;
                }
            }
            else
            {
                //�޸ı�ͷ��Ϣ
                string txt_RptTime = PageValidate.GetTrim(this.dr_RptTime.SelectedValue);    //�·�
                string txt_SldHeader = PageValidate.GetTrim(this.txt_SldHeader.Text);
                string txt_SldLeader = PageValidate.GetTrim(this.txt_SldLeader.Text);
                string txt_OprateDate = PageValidate.GetTrim(this.txt_OprateDate.Value);

                try
                {
                    //�޸ı�ͷ��Ϣ
                    m_SqlParams = "UPDATE RPT_Basis SET ";
                    m_SqlParams += "RptTime='" + txt_RptTime + "',SldHeader='" + txt_SldHeader + "',SldLeader ='" + txt_SldLeader + "',OprateDate ='" + txt_OprateDate + "'";
                    m_SqlParams += " WHERE RptID='" + m_ObjID + "'";
                    DbHelperSQL.ExecuteSql(m_SqlParams);

                    //�����Ϣ
                    //string sql = "UPDATE RPT_Contents SET Fileds29='1' WHERE RptID='" + m_ObjID+"'" ;
                    string sql_update = "UPDATE RPT_Contents SET Fileds28='1' WHERE RptID in (" + rptID.TrimEnd(',') + ")";
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
                                DbHelperSQL.ExecuteSql(sql_update);
                                Response.Write(" <script>alert('����ʡʵʩȫ�������������举Ů�������ͳ�Ʊ�������˳ɹ���') ;window.location.href='" + m_TargetUrl + "'</script>");
                            }
                        }
                        catch
                        {
                            Response.Write(" <script>alert('����ʡʵʩȫ�������������举Ů�������ͳ�Ʊ��������ʧ�ܣ�') ;window.location.href='" + m_TargetUrl + "'</script>");
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
                string sql_basisInfo = "select * from RPT_Basis where RptID = '" + m_Id+"'";
                try
                {
                    sdr = DbHelperSQL.ExecuteReader(sql_basisInfo);
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            this.dr_RptTime.SelectedValue = sdr["RptTime"].ToString();
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
                    DataTable table_1 = DbHelperSQL.Query("select RptID from RPT_Basis where FuncNo='030118'").Tables[0];
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

                StringBuilder sb = new StringBuilder();
                int fileds01 = 0;
                int fileds02 = 0;
                int fileds03 = 0;
                int fileds04 = 0;
                int fileds05 = 0;
                int fileds06 = 0;
                int fileds07 = 0;
                int fileds08 = 0;
                int fileds09 = 0;
                int fileds10 = 0;
                int fileds11 = 0;
                int fileds12 = 0;
                int fileds13 = 0;
                int fileds14 = 0;
                int fileds15 = 0;
                int fileds16 = 0;

                foreach (string str_rptID in rptID.TrimEnd(',').Split(','))
                {
                    string sqlall = "select * from RPT_Contents where Content_Type=1 and RptID = '" + str_rptID + "' order by CreateDate desc";
                    table = DbHelperSQL.Query(sqlall).Tables[0];
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow item in table.Rows)
                        {
                            fileds01 += Convert.ToInt32(item["Fileds01"].ToString());
                            fileds02 += Convert.ToInt32(item["Fileds02"].ToString());
                            fileds03 += Convert.ToInt32(item["Fileds03"].ToString());
                            fileds04 += Convert.ToInt32(item["Fileds04"].ToString());
                            fileds05 += Convert.ToInt32(item["Fileds05"].ToString());
                            fileds06 += Convert.ToInt32(item["Fileds06"].ToString());
                            fileds07 += Convert.ToInt32(item["Fileds07"].ToString());
                            fileds08 += Convert.ToInt32(item["Fileds08"].ToString());
                            fileds09 += Convert.ToInt32(item["Fileds09"].ToString());
                            fileds10 += Convert.ToInt32(item["Fileds10"].ToString());
                            fileds11 += Convert.ToInt32(item["Fileds11"].ToString());
                            fileds12 += Convert.ToInt32(item["Fileds12"].ToString());
                            fileds13 += Convert.ToInt32(item["Fileds13"].ToString());
                            fileds14 += Convert.ToInt32(item["Fileds14"].ToString());
                            fileds15 += Convert.ToInt32(item["Fileds15"].ToString());
                            fileds16 += Convert.ToInt32(item["Fileds16"].ToString());
                        }

                        //������������õ�����
                        string townCode = table.Rows[0]["Areacode"].ToString();
                        string str_code = townCode.Substring(0, 9) + "000";
                        string townName = join.pms.dal.CommPage.GetSingleVal("select AreaName from AreaDetailCN where Areacode = '" + str_code + "'");

                        sb.Append("<tr style=\"text-align:center;\">");
                        sb.Append("<td style=\"background-color:#ccffff\" height=\"22\"><a href=\"/RptInfo/chatdata03011802.aspx?param=" + str_rptID + "\">" + townName + "</a></td>");
                        sb.Append("<td style=\"background-color:#ccffcc\">" + (fileds01 + fileds02) + "</td>");
                        sb.Append("<td>" + fileds01 + "</td>");
                        sb.Append("<td>" + fileds02 + "</td>");
                        sb.Append("<td style=\"background-color:#ccffcc\">" + (fileds03 + fileds04) + "</td>");
                        sb.Append("<td>" + fileds03 + "</td>");
                        sb.Append("<td>" + fileds04 + "</td>");
                        sb.Append("<td style=\"background-color:#ccffcc\">" + (fileds05 + fileds06) + "</td>");
                        sb.Append("<td>" + fileds05 + "</td>");
                        sb.Append("<td>" + fileds06 + "</td>");
                        sb.Append("<td style=\"background-color:#ccffcc\">" + (fileds07 + fileds08 + fileds09 + fileds10) + "</td>");
                        sb.Append("<td>" + fileds07 + "</td>");
                        sb.Append("<td>" + fileds08 + "</td>");
                        sb.Append("<td>" + fileds09 + "</td>");
                        sb.Append("<td>" + fileds10 + "</td>");
                        sb.Append("<td>" + fileds11 + "</td>");
                        sb.Append("<td>" + fileds12 + "</td>");
                        sb.Append("<td>" + fileds13 + "</td>");
                        sb.Append("<td>" + fileds14 + "</td>");
                        sb.Append("<td>" + fileds14 + "</td>");
                        sb.Append("<td>" + fileds15 + "</td>");
                        sb.Append("<td>" + fileds16 + "</td>");
                        sb.Append("</tr>");
                    }
                }

                this.ltr_Content.Text = sb.ToString();


                ////����Id�õ��˿���������Ϣ
                //string sqlall = "";
                //sqlall = "select * from RPT_Contents where Content_Type=1 and RptID ='" + m_Id + "' order by CreateDate desc";
                //try
                //{
                //    table = DbHelperSQL.Query(sqlall).Tables[0];
                //}
                //catch
                //{ }

                //this.rep_Data.DataSource = table;
                //this.rep_Data.DataBind();
                //table = null;
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

            //�����û�Id�õ��û���Ϣ
            if (m_UserID != "")
            {
                str_UnitName = join.pms.dal.CommPage.GetSingleVal("select UserName from USER_BaseInfo where UserID = " + m_UserID);
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

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                m_NavTitle = "����ʡʵʩȫ�������������举Ů�������ͳ�Ʊ�";
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = Request.QueryString["RID"];
        }

        /// <summary>
        /// ͳ��
        /// </summary>
        /// <param name="col1"></param>
        /// <param name="col2"></param>
        public int GetNumByCol(object col1, object col2)
        {
            int num1 = 0;
            int num2 = 0;

            if (col1 != null && col1.ToString() != "")
            {
                try
                {
                    num1 = Convert.ToInt32(col1);
                }
                catch
                { }
            }
            if (col2 != null && col2.ToString() != "")
            {
                try
                {
                    num2 = Convert.ToInt32(col2);
                }
                catch
                { }
            }

            return num1 + num2;
        }

        /// <summary>
        /// ͳ��
        /// </summary>
        /// <param name="col1"></param>
        /// <param name="col2"></param>
        /// <param name="col3"></param>
        /// <param name="col4"></param>
        public int GetNumByCol(object col1, object col2, object col3, object col4)
        {
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;

            if (col1 != null && col1.ToString() != "")
            {
                try
                {
                    num1 = Convert.ToInt32(col1);
                }
                catch
                { }
            }
            if (col2 != null && col2.ToString() != "")
            {
                try
                {
                    num2 = Convert.ToInt32(col2);
                }
                catch
                { }
            }
            if (col3 != null && col3.ToString() != "")
            {
                try
                {
                    num3 = Convert.ToInt32(col3);
                }
                catch
                { }
            }
            if (col4 != null && col4.ToString() != "")
            {
                try
                {
                    num4 = Convert.ToInt32(col4);
                }
                catch
                { }
            }
            return num1 + num2 + num3 + num4;
        }
    }
}
