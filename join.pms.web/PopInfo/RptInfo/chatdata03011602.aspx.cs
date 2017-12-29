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
    public partial class chatdata03011602 : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        private string m_FuncCode_on;
        public string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        public string IsReported = "0";   //�ж��Ƿ��ϱ�  0����ʾδ�ϱ� 1����ʾ�ϱ�

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;
        private string m_UserName;
        public string countyparam = "";   //�ؼ���������Ĳ���

        #region ������Ϣ
        public string str_RptTime = "";   //����·�
        public string str_AreaName = "";   //����λ
        public string str_AreaCode = "";    //���λ����
        public string str_SldHeader = "";    //������
        public string str_SldLeader = "";   //�����
        public string str_OprateDate = "";   //�����/��������
        #endregion

        public string url = "";

        #region ͳ�ƺϼ�
        public int[] arrNum = new int[13];
        #endregion

        #region ��ʾ��Ϣ���õı���
        public string village_all_num = "";   //������ȫ���������
        public string reported_num = "";   //���ϱ�������
        public string no_reported_num = "";   //δ�ϱ�������
        public string no_reported_name = "";   //δ�ϱ��������
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();
            url = Request.RawUrl;
            if (!IsPostBack)
            {
                AreaData_TJ(m_ObjID);


                //0.δ�ϱ� 1.���ϱ� 2.δ��� 3.����� 4.��Ч  9.�鵵
                IsReported = CommPage.GetSingleVal("select Attribs from RPT_Basis where RptID = '" + m_ObjID + "'");
                if (IsReported == "0")
                {

                    this.ck_IsCheck.Visible = true;
                    this.btnUPSH.Visible = true;
                    this.btnEdit.Visible = true;
                }
                else if (IsReported == "3")
                {
                    this.ck_IsCheck.Enabled = false;
                    this.btnUPSH.Visible = false;
                    this.btnEdit.Visible = false;
                }
                DataBind(m_ObjID);
                SetPageStyle(m_UserID);
                SetOpratetionAction(m_NavTitle);
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
                    //Response.Write(" <script>alert('����ʧ�ܣ���������') ;window.location.href='" + m_TargetUrl + "'</script>");
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; �˿ڼ������� &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
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
            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = "030116";
                m_FuncCode_on = "030146";//��Ӧ���ϼ��������
                m_NavTitle = "�������ʡ�ƻ�������ͥ���ͳ�Ʊ�(����)";
            }
            else
            {
                //Response.Write("�Ƿ����ʣ���������ֹ��");
                //Response.End();
            }
            if (m_ActionName == "view") m_ObjID = Request.QueryString["RID"];
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="m_Id"></param>
        private void DataBind(string m_Id)
        {
            string Rpt_Attribs = "0";
            DataTable table = new DataTable();
            SqlDataReader sdr = null;
            if (m_Id != "")
            {
                //����id�õ�������Ϣ
                string sql_basisInfo = "";
                sql_basisInfo = "select * from RPT_Basis where Attribs != 4 and RptID = '" + m_Id + "'";

                try
                {
                    sdr = DbHelperSQL.ExecuteReader(sql_basisInfo);
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Rpt_Attribs = sdr["Attribs"].ToString();
                            this.txt_RptTime.Text = sdr["RptTime"].ToString();
                            str_AreaName = sdr["AreaName"].ToString();
                            str_AreaCode = sdr["AreaCode"].ToString();
                            this.txt_SldHeader.Text = sdr["SldHeader"].ToString();
                            this.txt_SldLeader.Text = sdr["SldLeader"].ToString();
                            this.txt_OprateDate.Text = DateTime.Parse(sdr["OprateDate"].ToString()).ToString("yyyy-MM-dd");
                        }
                    }
                }
                catch
                { }
                finally
                {
                    if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                }

                //����Id�õ��˿���������Ϣ
                string sqlall = "";
                sqlall = "select * from RPT_Contents where Content_Type=1 and RptID ='" + m_Id + "' order by RPTC_status desc,  CreateDate desc";
                try
                {
                    table = DbHelperSQL.Query(sqlall).Tables[0];

                    Rep_num(table);
                }
                catch
                { }

                this.rep_Data.DataSource = table;
                this.rep_Data.DataBind();
                table = null;

                //�ϼƲ������ϼ�Ӧ������
                AreaData_TJ(m_Id, Rpt_Attribs, str_AreaCode);
            }
        }

        /// <summary>
        /// �ϼ�
        /// </summary>
        /// <param name="m_ObjID"></param>
        private void AreaData_TJ(string m_ObjID, string Attribs, string AreaCode)
        {
            DataTable table_a = new DataTable();
            string sql = "select * from RPT_Contents where RptID = '" + m_ObjID + "'";
            try
            {
                table_a = DbHelperSQL.Query(sql).Tables[0];
                if (table_a.Rows.Count > 0)
                {
                    if (Attribs == "0")
                    {
                        //�ж��Ƿ��Ѵ����ϼ���Ϣ
                        //return;//���ϼ�ʱ
                        string Is_AreaName = CommPage.GetSingleVal("select CommID from RPT_Contents where RptID = '" + m_ObjID + "' and  Attribs != 4 and AreaCode = '" + AreaCode + "' and  RPTC_status=1 ");
                        if (Is_AreaName != "")
                        {
                            m_SqlParams = "UPDATE RPT_Contents SET ";
                            for (int i = 0; i < arrNum.Length; i++)
                            {
                                if (i < 9)
                                {
                                    m_SqlParams += "Fileds0" + (i + 1) + "='" + arrNum[i] + "',";
                                }
                                else
                                {
                                    m_SqlParams += "Fileds" + (i + 1) + "='" + arrNum[i] + "',";
                                }
                            }
                            m_SqlParams += "UpdateDate='" + DateTime.Now + "'";
                            m_SqlParams += " WHERE Attribs=0 and CommID=" + Is_AreaName;
                            DbHelperSQL.ExecuteSql(m_SqlParams);
                        }
                        else
                        {
                            m_SqlParams = "INSERT INTO [RPT_Contents](";
                            m_SqlParams += "RptID,AreaCode,AreaName,";
                            string m_SqlPvalue = "";
                            for (int i = 0; i < arrNum.Length; i++)
                            {
                                if (i < 9)
                                {
                                    m_SqlParams += "Fileds0" + (i + 1) + ",";
                                }
                                else
                                {
                                    m_SqlParams += "Fileds" + (i + 1) + ",";
                                }
                                m_SqlPvalue += "'" + arrNum[i] + "',";
                            }
                            m_SqlParams += "CreaterID,CreateDate,RPTC_status,Attribs";
                            m_SqlParams += ") VALUES(";
                            m_SqlParams += "'" + m_ObjID + "','" + str_AreaCode + "','" + str_AreaName + "',";
                            m_SqlParams += m_SqlPvalue;
                            m_SqlParams += "" + m_UserID + ",'" + DateTime.Now + "',1,0";
                            m_SqlParams += ")";
                            DbHelperSQL.ExecuteSql(m_SqlParams);
                        }
                    }
                }
            }
            catch
            { }
            table_a = null;
        }
        /// <summary>
        /// ͳ�Ʒ�������
        /// </summary>
        /// <param name="m_ObjID"></param>
        private void AreaData_TJ(string m_ObjID)
        {
            if (url.IndexOf("param=") > -1)
            {
                //���ؼ��鿴�弶����
            }
            else
            {

                //�õ�ȫ����ӵ�����
                try
                {
                    if (!string.IsNullOrEmpty(m_ObjID))
                    {
                        string townshipNum = CommPage.GetSingleVal("select AreaCode from RPT_Basis where Attribs != 4 and RptID = '" + m_ObjID + "'");
                        //ȫ���Ĵ�����
                        string sql_all = "select count(0) from AreaDetailCN where ParentCode = '" + townshipNum + "'";
                        village_all_num = CommPage.GetSingleVal(sql_all);


                        //���ϱ��Ĵ�����
                        DataTable table_reported = null;
                        string sql_reported = "select * from RPT_Contents where RptID = '" + m_ObjID + "'";
                        table_reported = DbHelperSQL.Query(sql_reported).Tables[0];
                        reported_num = table_reported.Rows.Count.ToString();
                        //δ�ϱ��Ĵ������
                        no_reported_num = Convert.ToString((Convert.ToInt32(village_all_num) - Convert.ToInt32(reported_num)));
                        //δ�ϱ��Ĵ�����
                        string reported_Code = "";
                        if (table_reported.Rows.Count > 0)
                        {
                            foreach (DataRow item in table_reported.Rows)
                            {
                                reported_Code += item["AreaCode"].ToString() + ",";
                            }
                        }
                        string sql_reported_name = "select * from AreaDetailCN where ParentCode = '" + townshipNum + "' and AreaCode not in (" + reported_Code.TrimEnd(',') + ")";
                        DataTable table_reported_name = null;
                        table_reported_name = DbHelperSQL.Query(sql_reported_name).Tables[0];
                        if (table_reported_name.Rows.Count > 0)
                        {
                            foreach (DataRow item in table_reported_name.Rows)
                            {
                                no_reported_name += item["AreaName"] + "    ";
                            }
                        }
                        table_reported = null;
                        table_reported = null;
                    }
                }
                catch
                { }
            }
        }
        /// <summary>
        /// �����ϱ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUPSH_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(m_ObjID))
            {
                //0.δ�ϱ� 1.���ϱ� 2.δ��� 3.����� 4.��Ч  9.�鵵
                //�жϸ������Ƿ��ϱ������Ѿ��ϱ��������ϱ�
                string Attribs_UP = CommPage.GetSingleVal("select Attribs from RPT_Basis where  Attribs in(0) and RptID = '" + m_ObjID + "'");
                int EAttribs = 1;//������ɵĴ���
                if (PageValidate.IsNumber(Attribs_UP))
                {
                    string strErr = string.Empty;
                    if (ck_IsCheck.Checked == false)
                    {
                        strErr += "��ȷ���ϱ����ϱ�������������ݽ������������޸ģ�\\n";
                    }
                    //�޸ı�ͷ��Ϣ
                    string txt_SldHeader = PageValidate.GetTrim(this.txt_SldHeader.Text);
                    string txt_SldLeader = PageValidate.GetTrim(this.txt_SldLeader.Text);
                    string txt_OprateDate = PageValidate.GetTrim(this.txt_OprateDate.Text);
                    if (String.IsNullOrEmpty(txt_SldHeader) || String.IsNullOrEmpty(txt_SldLeader) || string.IsNullOrEmpty(txt_OprateDate)) { strErr += "�����Ʊ������˺�����˵���Ϣ�����ύ��\\n"; }
                    if (strErr != "")
                    {
                        MessageBox.Show(this, strErr);
                        return;
                    }
                    try
                    {
                        try
                        {
                            //0.δ�ϱ� 1.���ϱ� 2.δ��� 3.����� 4.��Ч  9.�鵵
                            //�޸ı�ͷ��Ϣ
                            m_SqlParams = "UPDATE RPT_Basis SET ";
                            m_SqlParams += "Attribs=" + EAttribs + "";
                            m_SqlParams += " WHERE RptID='" + m_ObjID + "'";
                            DbHelperSQL.ExecuteSql(m_SqlParams);

                            //�ϱ��������Ϣ
                            string sql = "UPDATE RPT_Contents SET Attribs=" + EAttribs + " WHERE RptID='" + m_ObjID + "'";
                            DbHelperSQL.ExecuteSql(sql);

                            //������һ����˱�ͷ
                            //���ϱ�ʱ�����ؼ������ͷ
                            string on_str_AreaCode = str_AreaCode.Substring(0, 6) + "000000";
                            string on_str_AreaName = BIZ_Common.GetAreaName(on_str_AreaCode, "");
                            string txt_RptTime = CommPage.GetSingleVal("select RptTime from RPT_Basis where  Attribs =" + EAttribs + " and RptID = '" + m_ObjID + "'");
                            if (string.IsNullOrEmpty(CommPage.GetSingleVal("select RptID from RPT_Basis where AreaCode = '" + on_str_AreaCode + "' and Attribs!=4  and FuncNo = '" + m_FuncCode_on + "' and RptTime = '" + txt_RptTime + "'")))
                            {
                                try
                                {
                                    string RCount = "0";
                                    string RCounts = CommPage.GetSingleVal("select top 1 RptID from RPT_Basis where substring(AreaCode,1,6)='" + str_AreaCode.Substring(0, 6) + "' order by cast(rptid as float) desc ").ToString();
                                    if (!string.IsNullOrEmpty(RCounts))
                                    {
                                        RCount = RCounts.Substring(6);
                                    }
                                    string on_ObjID = str_AreaCode.Substring(0, 6) + ((Convert.ToInt32(RCount) + 1).ToString());
                                    m_SqlParams = "INSERT INTO [RPT_Basis](";
                                    m_SqlParams += "RptID,FuncNo,AreaCode,AreaName,RptName,RptTime,SldHeader,SldLeader,UserID,CreateDate,OprateDate,Attribs";
                                    m_SqlParams += ") VALUES(";
                                    m_SqlParams += "'" + on_ObjID + "','" + m_FuncCode_on + "','" + on_str_AreaCode + "','" + on_str_AreaName + "','" + m_NavTitle + "','" + txt_RptTime + "','','','" + m_UserID + "','" + DateTime.Now + "','" + txt_OprateDate + "',0";
                                    m_SqlParams += ")";
                                    DbHelperSQL.ExecuteSql(m_SqlParams);
                                }
                                catch
                                { }
                            }


                            Response.Write(" <script>alert('" + m_NavTitle + "�����ϱ��ɹ���') ;window.location.href='/UnvCommList.aspx?1=1&FuncCode=" + m_FuncCode + "&FuncNa=" + m_NavTitle + "'</script>");
                        }
                        catch
                        {
                            Response.Write(" <script>alert('" + m_NavTitle + "�����ϱ�ʧ�ܣ�') ;window.location.href='/UnvCommList.aspx?1=1&FuncCode=" + m_FuncCode + "&FuncNa=" + m_NavTitle + "'</script>");
                            return;
                        }
                    }
                    catch
                    { }
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "����Ϣ�����ϱ���", url, true, true);
                    return;
                }
            }
        }
        /// <summary>
        /// ת��༭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(m_ObjID))
            {
                //�жϸ������Ƿ��ϱ������Ѿ��ϱ����������༭
                if (CommPage.GetSingleVal("select Attribs from RPT_Basis where RptID = '" + m_ObjID + "'") == "0")
                {
                    Response.Write("<script language='javascript'>window.location.href='chatdata" + m_FuncCode + "01.aspx?action=edit&k=" + m_ObjID + "&sourceUrl=" + m_SourceUrl + "';</script>");
                    Response.End();
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "����Ϣ�Ѿ��ϱ�����ˣ����ܱ༭��", "'/UnvCommList.aspx?1=1&FuncCode=" + m_FuncCode + "&FuncNa=" + m_NavTitle + "'", true, true);
                    return;
                }
            }
        }


        /// <summary>
        /// �кϼ�
        /// </summary>
        /// <param name="col1"></param>
        /// <param name="col2"></param>
        /// <param name="col3"></param>
        /// <param name="col4"></param>
        /// <param name="col5"></param>
        public int GetNumByCol(string coll)
        {
            int num1 = 0;
            for (int i = 0; i < coll.Split(',').Length; i++)
            {
                num1 += int.Parse(coll.Split(',')[i]);
            }
            return num1;
        }


        //ͳ�ƣ���ͬ����ͳ�Ʋ�ͬ
        protected void Rep_num(DataTable table)
        {
            try
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow item in table.Rows)
                    {
                        for (int i = 0; i < arrNum.Length; i++)
                        {
                            if (i < 9)
                            {
                                arrNum[i] += Convert.ToInt32(item["Fileds0" + (i + 1).ToString()].ToString());
                            }
                            else
                            {
                                arrNum[i] += Convert.ToInt32(item["Fileds" + (i + 1).ToString()].ToString());
                            }
                        }

                    }
                }
            }
            catch { }

        }
    }
}
