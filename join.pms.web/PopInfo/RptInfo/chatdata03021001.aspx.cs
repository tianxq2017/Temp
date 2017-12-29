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
    public partial class chatdata03021001 : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        public string m_ActionName;
        private string m_ObjID;
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_RoleID; // ��ǰ��¼�Ĳ����û��Ľ�ɫ

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;
        private string m_UserName;

        public string IsReported = "0";   //�ж��Ƿ��ϱ�  0����ʾδ�ϱ� 1����ʾ�ϱ�

        #region ������Ϣ
        public string str_RptTime = "";   //����·�
        public string str_AreaName = "";   //����λ
        public string str_AreaCode = "";    //���λ����
        public string str_SldHeader = "";    //������
        public string str_SldLeader = "";   //�����
        public string str_OprateDate = "";   //�����/��������
        #endregion

        public string url = "";
        protected string js_value = "";
        protected string js_value2 = "";

        #region ͳ�ƺϼ�
        public int[] arrNum = new int[22];
        public int[] arrNum2 = new int[22];
        public string[] arrStr = new string[22];
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();
            url = Request.RawUrl;
            m_RoleID = CommPage.GetSingleVal("SELECT TOP 1 RoleID FROM SYS_UserRoles WHERE UserID=" + m_UserID);
            if (!IsPostBack)
            {
                Rpt_headType(m_ObjID);//��ͷ����
                AreaCode_Fun(m_ObjID);
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
                case "del": // ɾ��
                    funcName = "ɾ��";
                    DelBasisInfo(m_ObjID);
                    break;
                case "edit": // �鿴
                    funcName = "�鿴����";
                    EditRPTInfo(m_ObjID);
                    break;
                default:
                    Response.Write(" <script>alert('����ʧ�ܣ���������') ;window.location.href='" + m_TargetUrl + "'</script>");
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; ���ױ������� &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
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
                string sql_basisInfo = "select * from RPT_Basis where RptID = '" + m_Id + "' ";
                try
                {
                    sdr = DbHelperSQL.ExecuteReader(sql_basisInfo);
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            this.txt_RptTime.SelectedValue = sdr["RptTime"].ToString();
                            str_AreaName = sdr["AreaName"].ToString();
                            this.txt_SldHeader.Text = sdr["SldHeader"].ToString();
                            this.txt_SldLeader.Text = sdr["SldLeader"].ToString();
                            this.txt_OprateDate.Value = DateTime.Parse(sdr["OprateDate"].ToString()).ToString("yyyy-MM-dd");
                            this.lbl_DataAreaSel.Text = str_AreaName;
                        }
                    }
                }
                catch { }
                finally
                {
                    if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                }

                //����Id�õ��˿���������Ϣ
                string sqlall = "";
                sqlall = "select * from RPT_Contents where Content_Type=1 and RptID ='" + m_Id + "' order by CreateDate desc";
                try
                {
                    table = DbHelperSQL.Query(sqlall).Tables[0];

                    if (table.Rows.Count > 0)
                    {
                        Rep_num(table);//ͳ��
                    }

                }
                catch { }

                this.rep_Data.DataSource = table;
                this.rep_Data.DataBind();
                table = null;
            }
        }

        /// <summary>
        /// ��Ҫ�޸ı��������
        /// </summary>
        /// <param name="objID"></param>
        public void ShowModInfo(string objID)
        {
            this.hd_upId.Value = objID;
            bool isEdit = true;
            SqlDataReader sdr = null;
            try
            {
                StringBuilder jssb = new StringBuilder();
                m_SqlParams = "SELECT * FROM [RPT_Contents] WHERE CommID=" + objID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    AreaCode_Fun(m_ObjID);//���±�������
                    //this.UcDataAreaSel1.SetAreaName(PageValidate.GetTrim(sdr["AreaName"].ToString()));
                    //this.UcDataAreaSel1.SetAreaCode(PageValidate.GetTrim(sdr["AreaCode"].ToString()));
                    this.dr_DataAreaSel.SelectedValue = PageValidate.GetTrim(sdr["AreaCode"].ToString());
                    jssb.Append("<script>");
                    for (int i = 0; i < arrNum.Length; i++)
                    {
                        if (i < 9)
                        {
                            jssb.Append("document.getElementById(\"txtFileds0" + (i + 1) + "\").value=\"" + PageValidate.GetTrim(sdr["Fileds0" + (i + 1)].ToString()) + "\";");
                        }
                        else
                        {
                            jssb.Append("document.getElementById(\"txtFileds" + (i + 1) + "\").value=\"" + PageValidate.GetTrim(sdr["Fileds" + (i + 1)].ToString()) + "\";");
                        }
                    }
                    jssb.Append("</script>");
                }
                js_value = jssb.ToString();
                jssb = null;
                sdr.Close();
                // �Ƿ�ɱ༭ SetAreaCode
                if (!isEdit)
                {
                    Response.Write(" <script>alert('����ʧ�ܣ���ѡ�����Ϣ����ͨ����ˡ������򹫿�����Ϣ������Ϣ������༭��') ;window.location.href='/UnvCommList.aspx?1=1&FuncCode=" + m_FuncCode + "&FuncNa=" + m_NavTitle + "'</script>");
                }
            }
            catch { if (sdr != null) sdr.Close(); }
        }



        /// <summary>
        /// ��ͷ����
        /// </summary>
        /// <param name="m_ObjID"></param>
        private void Rpt_headType(string m_ObjID)
        {

            //�����꼾��
            int Rptnum = int.Parse(DateTime.Now.ToString("yyyy"));
            int ii = 0;
            for (int i = 0; i < 5; i++)
            {
                txt_RptTime.Items.Insert(ii, (Rptnum - i).ToString() + "��");
            }
            //�趨txt_RptTimeѡ����
            if (m_ObjID != null)
            {
                txt_RptTime.SelectedValue = CommPage.GetSingleVal("select RptTime from RPT_Basis where Attribs != 4 and RptID = '" + m_ObjID + "'");
                DataTable table_RptTime = new DataTable();
                table_RptTime = DbHelperSQL.Query("select top 15 RptTime from RPT_Basis where Attribs != 4 and AreaCode = '" + str_AreaCode + "' and  FuncNo = '" + m_FuncCode + "' and RptID!='" + m_ObjID + "' order by  OprateDate  desc ").Tables[0];
                if (table_RptTime.Rows.Count > 0)
                {
                    foreach (DataRow item in table_RptTime.Rows)
                    {
                        txt_RptTime.Items.Remove(item[0].ToString());
                    }
                }
                table_RptTime = null;
            }
            else
            {
                //˵���������Ƴ��Ѵ��ڵļ���
                if (str_AreaCode != "" && m_FuncCode != "")
                {
                    DataTable table_RptTime = new DataTable();
                    table_RptTime = DbHelperSQL.Query("select top 15 RptTime from RPT_Basis where Attribs != 4 and AreaCode = '" + str_AreaCode + "'  and FuncNo = '" + m_FuncCode + "' order by  OprateDate  desc ").Tables[0];
                    if (table_RptTime.Rows.Count > 0)
                    {
                        foreach (DataRow item in table_RptTime.Rows)
                        {
                            txt_RptTime.Items.Remove(item[0].ToString());
                        }
                    }
                    table_RptTime = null;
                }
            }
        }

        /// <summary>
        /// ��ǰ�����¼�����
        /// </summary>
        /// <param name="m_ObjID"></param>
        private void AreaCode_Fun(string m_ObjID)
        {
            DataTable table_Code = null;
            if (str_AreaCode != "")
            {
                //�����Ѵ��ڵ�����
                string not_Area = "'000000000000'";
                if (m_ObjID != null)
                {
                    DataTable table_not_Area = new DataTable();
                    table_not_Area = DbHelperSQL.Query("select AreaCode from RPT_Contents where RptID = '" + m_ObjID + "'").Tables[0];
                    if (table_not_Area.Rows.Count > 0)
                    {
                        foreach (DataRow item in table_not_Area.Rows)
                        {
                            not_Area += ",'" + item[0].ToString() + "'";
                        }
                    }
                    table_not_Area = null;
                }
                if (!string.IsNullOrEmpty(this.hd_upId.Value))
                {
                    //�༭����ʱ��Ҫװ���������е���������
                    not_Area = not_Area.Replace(CommPage.GetSingleVal("SELECT AreaCode FROM [RPT_Contents] WHERE CommID=" + this.hd_upId.Value), "000000000000");
                }
                //1	 ϵͳ����Ա
                //14 ��
                //15 ��
                if ((m_RoleID == "1" || m_RoleID == "14"))
                {
                    string sql_Code = "select AreaName,Areacode from AreaDetailCN where ParentCode = '" + str_AreaCode.Substring(0, 6) + "000000" + "' and Areacode not in(" + not_Area + ") ";
                    try
                    {
                        table_Code = DbHelperSQL.Query(sql_Code).Tables[0];
                        if (table_Code.Rows.Count > 0)
                        {
                            this.dr_DataAreaSel.DataSource = table_Code;
                            this.dr_DataAreaSel.DataTextField = "AreaName";
                            this.dr_DataAreaSel.DataValueField = "Areacode";
                            this.dr_DataAreaSel.DataBind();
                            this.dr_DataAreaSel.Items.Insert(0, new ListItem("--��ѡ��--"));
                        }
                        else
                        {
                            this.dr_DataAreaSel.Items.Insert(0, new ListItem("--��--"));
                        }
                        table_Code = null;
                    }
                    catch { }
                }
                else if (m_RoleID == "15")
                {
                    //��ֻ���Լ���һ����Ϣ
                    this.dr_DataAreaSel.Items.Insert(0, new ListItem(str_AreaName, str_AreaCode.Substring(0, 9) + "000"));
                    this.dr_DataAreaSel.Items.Insert(0, new ListItem("--��ѡ��--"));
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "�ñ���ֻ�����ؼ����ױ���Ժ������", m_TargetUrl, true, true);
                    return;
                
                }

            }


        }
        /// <summary>
        /// �༭ʱ�ж��Ƿ����ϱ�
        /// </summary>
        /// <param name="objID"></param>
        private void EditRPTInfo(string m_ObjID)
        {
            if (!string.IsNullOrEmpty(m_ObjID))
            {
                //�жϸ������Ƿ��ϱ������Ѿ��ϱ����������༭
                if (CommPage.GetSingleVal("select Attribs from RPT_Basis where RptID = '" + m_ObjID + "'") == "3")
                {
                    //0.δ�ϱ� 1.���ϱ� 2.δ��� 3.����� 4.��Ч  9.�鵵
                    this.txt_SldHeader.Enabled = false;
                    this.txt_SldLeader.Enabled = false;
                    this.txt_OprateDate.Disabled = true;
                    MessageBox.ShowAndRedirect(this.Page, "����Ϣ�Ѿ���ˣ����ܲ�����", "'/UnvCommList.aspx?1=1&FuncCode=" + m_FuncCode + "&FuncNa=" + m_NavTitle + "'", true, true);
                    return;
                }
            }
        }

        /// <summary>
        /// ɾ����ͷ��Ϣ�����¼�ֵ
        /// </summary>
        /// <param name="objID"></param>
        private void DelBasisInfo(string objID)
        {
            try
            {
                try
                {
                    if (CommPage.GetSingleVal("select Attribs from RPT_Basis where RptID = '" + m_ObjID + "'") == "3")
                    {
                        MessageBox.ShowAndRedirect(this.Page, "����Ϣ�Ѿ���ˣ����ܲ�����", m_TargetUrl, true, true);
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
        /// ɾ����Ϣ����������Ϣ������ɾ��
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            try
            {
                try
                {
                    //�ж��������ڱ��Ƿ��ϱ�
                    if (CommPage.GetSingleVal("select Attribs from RPT_Basis where RptID = '" + m_ObjID + "'") == "0")
                    {
                        m_SqlParams = "DELETE FROM RPT_Contents WHERE CommID IN(" + objID + ") and Attribs=0  ";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        MessageBox.ShowAndRedirect(this.Page, "�����ɹ�����ѡ�����Ϣɾ���ɹ���", m_TargetUrl, true, true);
                        return;
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "����Ϣ�Ѿ���ˣ�����ɾ��������", m_TargetUrl, true, true);
                        return;
                    }
                }
                catch { }
            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this, ex.Message, m_TargetUrl, true);
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
            else
            {
                //�����û�Id�õ��û���Ϣ
                str_AreaCode = CommPage.GetSingleVal("select UserAreaCode from USER_BaseInfo where UserID = " + m_UserID).Replace(" ","");
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
            
            if (m_ActionName == "view")
            {
                m_ObjID = Request.QueryString["RID"];
            }
            if (m_ActionName == "tb_ldadd" && !string.IsNullOrEmpty(m_ObjID))
            {
                //����ʱѡ������Ӧ�ı���ֱ�ӽ��б༭
                m_ActionName = "edit";
            }
            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                m_FuncCode = "030210";
                m_NavTitle = "��ǰ��������걨��";
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
                    Response.Write("<script language='javascript'>alert('����Ϣ�Ѿ���ˣ�');;window.location.href='/UnvCommList.aspx?1=1&FuncCode=" + m_FuncCode + "&FuncNa=" + m_NavTitle + "'</script>");
                    Response.End();
                }
            }
        }

        /// <summary>
        /// �����ύ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;

            //��ͷֵ��Ϣ
            string txt_RptTime = PageValidate.GetTrim(this.txt_RptTime.SelectedValue);
            string txt_SldHeader = PageValidate.GetTrim(this.txt_SldHeader.Text);
            string txt_SldLeader = PageValidate.GetTrim(this.txt_SldLeader.Text);
            string txt_OprateDate = PageValidate.GetTrim(this.txt_OprateDate.Value);

            //������ֵ����
            string AreaCode = PageValidate.GetTrim(this.dr_DataAreaSel.SelectedValue);
            string AreaName = BIZ_Common.GetAreaName(AreaCode, "");

            if (String.IsNullOrEmpty(txt_SldHeader) || String.IsNullOrEmpty(txt_SldLeader)) { strErr += "�����븺���˺�����ˣ�\\n"; }
            //if (String.IsNullOrEmpty(AreaCode) || String.IsNullOrEmpty(AreaName)) { strErr += "��ѡ��λ��\\n"; }

            string F_strErr = "", F_id = "";
            int arr_num = 22;
            string[] arr_Fileds = new string[arr_num];
            string[] arr_Label = new string[arr_num];
            string[] arr_Labeltype = new string[arr_num];

            if (!String.IsNullOrEmpty(AreaCode) && !String.IsNullOrEmpty(AreaName))
            {
                if (LFileds_type.Text.Split(',').Length == arr_num && LFileds_txt.Text.Split(',').Length == arr_num)
                {
                    for (int i = 0; i < LFileds_type.Text.Split(',').Length; i++)
                    {
                        arr_Label[i] = LFileds_txt.Text.Split(',')[i];
                        arr_Labeltype[i] = LFileds_type.Text.Split(',')[i];
                    }
                    for (int i = 0; i < arr_num; i++)
                    {
                        F_id = (i + 1).ToString();
                        if (i < 9) { F_id = "0" + (i + 1); }
                        arr_Fileds[i] = CommPage.replaceNUM(PageValidate.GetTrim(Request.Form["txtFileds" + F_id]));
                        if (arr_Labeltype[i] == "0")
                        {
                            //0������֤
                        }
                        else if (arr_Labeltype[i] == "1")
                        {
                            //1 ���ֻ���������Ϊ��
                            if (String.IsNullOrEmpty(arr_Fileds[i]) || !PageValidate.IsNumber(PageValidate.GetTrim(arr_Fileds[i])))
                            {
                                F_strErr += arr_Label[i] + "����Ϊ�ղ��������ָ�ʽ��\\n";
                            }
                        }
                    }
                }
                else
                {

                    strErr += "���ô������飡";
                }
            }
            strErr += F_strErr;
            if (strErr != "")
            {
                arr_Fileds = null;
                arr_Label = null;
                MessageBox.Show(this, strErr);
                return;
            }

            //�жϱ�ͷ�Ƿ����
            if (m_ActionName == "tb_ldadd")
            {
                //�����������
                if (m_UserName != "")
                {
                    m_ObjID = CommPage.GetSingleVal("select RptID from RPT_Basis where Attribs != 4 and AreaCode = '" + str_AreaCode + "' and FuncNo = '" + m_FuncCode + "' and RptTime = '" + txt_RptTime + "'");
                    if (string.IsNullOrEmpty(m_ObjID))
                    {
                        if (!string.IsNullOrEmpty(txt_OprateDate)) { txt_OprateDate = DateTime.Now.ToString("yyyy-MM-dd"); }
                        try
                        {
                            string RCount = "0";
                            string RCounts = CommPage.GetSingleVal("select top 1 RptID from RPT_Basis where substring(AreaCode,1,6)='" + str_AreaCode.Substring(0, 6) + "' order by cast(rptid as float) desc ").ToString();
                            if (!string.IsNullOrEmpty(RCounts))
                            {
                                RCount = RCounts.Substring(6);
                            }
                            m_ObjID = str_AreaCode.Substring(0, 6) + ((Convert.ToInt32(RCount) + 1).ToString());
                            m_SqlParams = "INSERT INTO [RPT_Basis](";
                            m_SqlParams += "RptID,FuncNo,AreaCode,AreaName,RptName,RptTime,SldHeader,SldLeader,UserID,CreateDate,OprateDate,Attribs";
                            m_SqlParams += ") VALUES(";
                            m_SqlParams += "'" + m_ObjID + "','" + m_FuncCode + "','" + str_AreaCode + "','" + str_AreaName + "','" + m_NavTitle + "','" + txt_RptTime + "','" + txt_SldHeader + "','" + txt_SldLeader + "','" + m_UserID + "','" + DateTime.Now + "','" + txt_OprateDate + "',0";
                            m_SqlParams += ")";
                            DbHelperSQL.ExecuteSql(m_SqlParams);
                        }
                        catch
                        { }
                    }
                    else
                    {
                        string Attribs = CommPage.GetSingleVal("select Attribs from RPT_Basis where RptID = '" + m_ObjID + "'");
                        if (Attribs == "0")
                        {
                            MessageBox.ShowAndRedirect(this.Page, m_NavTitle + "(" + txt_RptTime + ")�Ѵ���,���һ�δ�ϱ���", "chatdata" + m_FuncCode + "01.aspx?action=edit&k=" + m_ObjID + "&sourceUrl=" + m_SourceUrl + "", true, true);
                            return;
                        }
                        else
                        {
                            MessageBox.ShowAndRedirect(this.Page, m_NavTitle + "(" + txt_RptTime + ")�Ѵ��ڣ�", "/UnvCommList.aspx?1=1&FuncCode=" + m_FuncCode + "&FuncNa=" + m_NavTitle + "", true, true);
                            return;
                        }
                    }
                }
            }
            else
            {
                try
                {
                    string Is_Exist = CommPage.GetSingleVal("select RptID from RPT_Basis where FuncNo = '" + m_FuncCode + "' and RptID!='" + m_ObjID + "' and RptTime = '" + txt_RptTime + "'  and AreaCode = '" + str_AreaCode + "' and Attribs != 4");
                    if (Is_Exist != "")
                    {
                        MessageBox.ShowAndRedirect(this.Page, m_NavTitle + "(" + txt_RptTime + ")�Ѵ��ڣ�", "/UnvCommList.aspx?1=1&FuncCode=" + m_FuncCode + "&FuncNa=" + m_NavTitle + "", true, true);
                        return;
                    }
                    else
                    {
                        m_SqlParams = "UPDATE RPT_Basis SET ";
                        m_SqlParams += "AreaCode='" + str_AreaCode + "',AreaName='" + str_AreaName + "',RptTime ='" + txt_RptTime + "',SldHeader ='" + txt_SldHeader + "',SldLeader ='" + txt_SldLeader + "',OprateDate ='" + txt_OprateDate + "'";
                        m_SqlParams += " WHERE Attribs=0 and RptID='" + m_ObjID + "'";

                        DbHelperSQL.ExecuteSql(m_SqlParams);
                    }
                }
                catch
                { }
            }


            if (this.hd_IsUp.Value == "1")
            {
                m_ActionName = "tb_ldedit";
            }
            if (m_ActionName == "tb_ldadd" || m_ActionName == "edit")
            {
                string editurl = "chatdata" + m_FuncCode + "01.aspx?action=edit&k=" + m_ObjID + "&sourceUrl=" + m_SourceUrl + "";
                if (String.IsNullOrEmpty(AreaCode) || String.IsNullOrEmpty(AreaName))
                {
                    //�����������ݣ����������ͷ����
                    //DataBind();
                    this.hd_upId.Value = "";
                    MessageBox.ShowAndRedirect(this.Page, m_NavTitle + "�ı�ͷ�������Ϣ���³ɹ���", editurl, true, true);
                    return;
                }
                else
                {
                    //���ݴ����ж��Ƿ�ô��Ѿ��������Ϣ
                    string Is_AreaName = CommPage.GetSingleVal("select RptID from RPT_Contents where RptID = '" + m_ObjID + "' and Attribs=0 and AreaCode = '" + this.dr_DataAreaSel.SelectedValue + "'");
                    if (Is_AreaName != "")
                    {
                        MessageBox.Show(this, m_NavTitle + "�С�" + AreaName + "������Ϣ�Ѵ��ڣ�");
                        return;
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
                            m_SqlPvalue += "'" + arr_Fileds[i] + "',";
                        }
                        m_SqlParams += "CreaterID,CreateDate,Content_Type,Attribs";
                        m_SqlParams += ") VALUES(";
                        m_SqlParams += "'" + m_ObjID + "','" + AreaCode + "','" + AreaName + "',";
                        m_SqlParams += m_SqlPvalue;
                        m_SqlParams += "" + m_UserID + ",'" + DateTime.Now + "',1,0";
                        m_SqlParams += ")";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        DataBind();
                        this.hd_upId.Value = "";
                        MessageBox.ShowAndRedirect(this.Page, m_NavTitle + "�С�" + AreaName + "����������ӳɹ���", editurl, true, true);
                        return;
                    }
                }
            }
            else if (m_ActionName == "tb_ldedit")
            {
                if (!string.IsNullOrEmpty(this.hd_upId.Value))
                {
                    m_SqlParams = "UPDATE RPT_Contents SET ";
                    m_SqlParams += "AreaCode='" + AreaCode + "',AreaName='" + AreaName + "',";

                    for (int i = 0; i < arrNum.Length; i++)
                    {
                        if (i < 9)
                        {
                            m_SqlParams += "Fileds0" + (i + 1) + "='" + arr_Fileds[i] + "',";
                        }
                        else
                        {
                            m_SqlParams += "Fileds" + (i + 1) + "='" + arr_Fileds[i] + "',";
                        }
                    }
                    m_SqlParams += "UpdaterID=" + m_UserID + ",UpdateDate='" + DateTime.Now + "'";
                    m_SqlParams += " WHERE Attribs=0 and CommID=" + this.hd_upId.Value;
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    DataBind();
                    this.hd_upId.Value = "";
                    MessageBox.ShowAndRedirect(this.Page, m_NavTitle + "�С�" + AreaName + "�������ݸ��³ɹ���", url, true, true);
                    return;
                }
            }

        }
        /// <summary>
        /// ת���ϱ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUp_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(m_ObjID))
            {
                Response.Write("<script language='javascript'>window.location.href='chatdata" + m_FuncCode + "02.aspx?action=view&RID=" + m_ObjID + "&sourceUrl=" + m_SourceUrl + "';</script>");
                Response.End();
            }
            //��ͷֵ��Ϣ
        }
        /// <summary>
        /// �����˿���Ϣ�༭��ɾ���¼�
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rep_Data_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string Id = e.CommandArgument.ToString();

            if (e.CommandName == "Update")
            {
                ShowModInfo(Id);
                this.hd_IsUp.Value = "1";
            }
            if (e.CommandName == "Delete")
            {
                DelInfo(Id);
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


        /// <summary>
        //ͳ�ƣ���ͬ����ͳ�Ʋ�ͬ
        /// </summary>
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


        /// <summary>
        //���ݹ���
        /// </summary>
        protected void Rep_AutoData(string RptTime, string FuncCode, string AreaCode)
        {
            try
            {
                if (AreaCode.IndexOf("��ѡ��") == -1 && !string.IsNullOrEmpty(AreaCode) && !string.IsNullOrEmpty(RptTime) && !string.IsNullOrEmpty(FuncCode))
                {

                    if (AreaCode.Substring(6) == "000000")
                    {
                        //�ؼ�
                        AreaCode = AreaCode.Substring(0,6);
                    }
                    else if (AreaCode.Substring(9) == "000")
                    {
                        //��
                        AreaCode = AreaCode.Substring(0,9);
                    }
                    //��һ����ֵ
                    //���Ǽ� ��0Ů1
                    arrStr[0] = "��Դ������ > �����Ǽ���Ϣ��" + CommPage.GetSingleVal(" select count(0) from V_PIS where AreaCode like'" + AreaCode + "%'  and Fileds03 like '" + RptTime.Replace("��", "") + "' ");
                    arrStr[1] = arrStr[0];
                    //��ǰҽѧ��� ��0Ů1
                    arrStr[2] = "��Դҵ������ > ��ǰҽѧ��飺" + CommPage.GetSingleVal(" select count(0) from v_BizList where BizCode='0131' and Fileds02 like'%��%' and SelAreaCode like'" + AreaCode + "%'  and  CONVERT(varchar(100), FinalDate, 23)>='" + RptTime.Replace("��", "") + "-01-01' AND CONVERT(varchar(100), FinalDate, 23)<='" + RptTime.Replace("��", "") + "-12-31' ");
                    arrStr[3] = "��Դҵ������ > ��ǰҽѧ��飺" + CommPage.GetSingleVal(" select count(0) from v_BizList where BizCode='0131' and Fileds02 like'%Ů%' and SelAreaCode like'" + AreaCode + "%'  and  CONVERT(varchar(100), FinalDate, 23)>='" + RptTime.Replace("��", "") + "-01-01' AND CONVERT(varchar(100), FinalDate, 23)<='" + RptTime.Replace("��", "") + "-12-31' ");

                    //��ǰҽѧ�������Ӱ�����������ҽѧָ����� ��0Ů1
                    arrStr[20] = "��Դҵ������ > ��ǰҽѧ��飺" + CommPage.GetSingleVal(" select count(0) from v_BizList where BizCode='0131' and Fileds02 like'%��%' and SelAreaCode like'" + AreaCode + "%'  and  CONVERT(varchar(100), FinalDate, 23)>='" + RptTime.Replace("��", "") + "-01-01' AND CONVERT(varchar(100), FinalDate, 23)<='" + RptTime.Replace("��", "") + "-12-31' and Fileds24 in('1','2','3','5')  ");
                    arrStr[21] = "��Դҵ������ > ��ǰҽѧ��飺" + CommPage.GetSingleVal(" select count(0) from v_BizList where BizCode='0131' and Fileds02 like'%Ů%' and SelAreaCode like'" + AreaCode + "%'  and  CONVERT(varchar(100), FinalDate, 23)>='" + RptTime.Replace("��", "") + "-01-01' AND CONVERT(varchar(100), FinalDate, 23)<='" + RptTime.Replace("��", "") + "-12-31' and Fileds24 in('1','2','3','5') ");


                    //��һ��js���
                    StringBuilder jssb = new StringBuilder();
                    jssb.Append("<script>");
                    for (int i = 0; i < arrStr.Length; i++)
                    {
                        if (i < 9)
                        {
                            jssb.Append("document.getElementById(\"txtLabel0" + (i + 1) + "\").innerHTML=\"" + arrStr[i] + "\";");
                        }
                        else
                        {
                            jssb.Append("document.getElementById(\"txtLabel" + (i + 1) + "\").innerHTML=\"" + arrStr[i] + "\";");
                        }
                    }
                    jssb.Append("</script>");
                    js_value2 = jssb.ToString();
                    jssb = null;
                }
            }
            catch { }

        }
        /// <summary>
        //����������
        /// </summary>
        protected void dr_DataAreaSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string RptTime = this.txt_RptTime.SelectedValue;
                string RptAreaSe = this.dr_DataAreaSel.SelectedValue;
                Rep_AutoData(RptTime, m_FuncCode, RptAreaSe);
            }
            catch { }
        }
    }
}
