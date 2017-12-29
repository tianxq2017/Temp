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
    public partial class chatdata03010101 : System.Web.UI.Page
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

        #region ������Ϣ
        public string str_RptTime = "";   //����·�
        public string str_AreaName = "";   //����λ
        public string str_AreaCode = "";    //���λ����
        public string str_SldHeader = "";    //������
        public string str_SldLeader = "";   //�����
        public string str_OprateDate = "";   //�����/��������
        #endregion

        public string IsReported = "0";   //�ж��Ƿ��ϱ�  0����ʾδ�ϱ� 1����ʾ�ϱ�
        public string url = "";

        protected string js_value = "";

        #region ͳ�ƺϼ�
        public int[] arrNum = new int[18];
        public int[] arrNum2 = new int[18];
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();


            url = Request.RawUrl;
            if (!IsPostBack)
            {
                Rpt_headType(m_ObjID);//��ͷ����
                AreaCode_Fun(m_ObjID);
                DataBind(m_ObjID);
                SetPageStyle(m_UserID);
                SetOpratetionAction(m_NavTitle);
                this.lbl_DataAreaSel.Text = str_AreaName;
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
                string sql_basisInfo = "select * from RPT_Basis where Attribs!=4 and  RptID = '" + m_Id + "' ";
                try
                {
                    sdr = DbHelperSQL.ExecuteReader(sql_basisInfo);
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            this.txt_RptTime.Value = sdr["RptTime"].ToString();
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
                }
                catch { }

                this.rep_Data.DataSource = table;
                this.rep_Data.DataBind();
                table = null;
            }
        }

        /// <summary>
        /// �޸� �����˿���ϢUcAreaSe08
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
                    //this.dr_DataAreaSel.SelectedValue = PageValidate.GetTrim(sdr["AreaCode"].ToString());

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
            this.txt_RptTime.Value = DateTime.Now.ToString("yyyy��MM��");

            //�жϱ�ͷ�Ƿ����
            if (m_ActionName != "edit" && m_ActionName != "del")
            {
                if (url.IndexOf("IsClick=1") < 0)
                {
                    string Is_Exist = join.pms.dal.CommPage.GetSingleVal("select RptID from RPT_Basis where FuncNo = '" + m_FuncCode + "' and RptID!='" + m_ObjID + "' and RptTime = '" + DateTime.Now.ToString("yyyy��MM��") + "'  and Attribs != 4");
                    if (Is_Exist != "")
                    {
                        MessageBox.ShowAndRedirect(this.Page, "����Ϣ�Ѿ����ڣ�ͬһ���·����ݣ�����ͬʱ������Σ�", m_TargetUrl, true, true);
                        return;
                    }
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
                string sql_Code = "select AreaName,Areacode from AreaDetailCN where ParentCode = '" + str_AreaCode + "' and Areacode not in(" + not_Area + ") ";
                try
                {
                    table_Code = DbHelperSQL.Query(sql_Code).Tables[0];
                    if (table_Code.Rows.Count > 0)
                    {
                        str_AreaName = table_Code.Rows[0]["AreaName"].ToString();
                        str_AreaCode = table_Code.Rows[0]["Areacode"].ToString();
                    }
                    table_Code = null;
                }
                catch { }
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
                if (CommPage.GetSingleVal("select Attribs from RPT_Basis where RptID = '" + m_ObjID + "'") != "0")
                {
                    //0.δ�ϱ� 1.���ϱ� 2.δ��� 3.����� 4.��Ч  9.�鵵
                    this.txt_SldHeader.Enabled = false;
                    this.txt_SldLeader.Enabled = false;
                    this.txt_OprateDate.Disabled = true;
                    MessageBox.ShowAndRedirect(this.Page, "����Ϣ�Ѿ��ϱ������ܲ�����", "'/UnvCommList.aspx?1=1&FuncCode=" + m_FuncCode + "&FuncNa=" + m_NavTitle + "'", true, true);
                    return;
                }
            }
        }

        /// <summary>
        /// ɾ����ͷ��Ϣ
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
        /// ɾ����Ϣ �����˿���Ϣ
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

            try
            {
                if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
                {
                    m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                    m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                    //m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
                    m_FuncCode = "030101";
                    m_NavTitle = "�˿ڶ�̬��Ϣ���浥<һ>";
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
            catch 
            { }
        }

        /// <summary>
        /// ������Ǩ�ơ���������ύ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;

            //��ͷֵ��Ϣ
            string txt_RptTime = PageValidate.GetTrim(this.txt_RptTime.Value);
            string txt_SldHeader = PageValidate.GetTrim(this.txt_SldHeader.Text);
            string txt_SldLeader = PageValidate.GetTrim(this.txt_SldLeader.Text);
            string txt_OprateDate = PageValidate.GetTrim(this.txt_OprateDate.Value);

            string RptID = m_ObjID;
            //string AreaCode = PageValidate.GetTrim(this.UcDataAreaSel1.GetAreaCode());
            //string AreaName = PageValidate.GetTrim(this.UcDataAreaSel1.GetAreaName());

            //string AreaCode = join.pms.dal.CommPage.GetSingleVal("select UserAreaCode from USER_BaseInfo where UserName = '" + this.lbl_DataAreaSel.Text + "'");
            string AreaCode = join.pms.dal.CommPage.GetSingleVal("select UserAreaCode from USER_BaseInfo where UserAccount = '" + str_AreaCode + "'");
            string AreaName = PageValidate.GetTrim(this.lbl_DataAreaSel.Text);

            string Fileds01 = PageValidate.GetTrim(this.txtFileds01.Text);
            string Fileds02 = PageValidate.GetTrim(this.txtFileds02.Text);
            string Fileds03 = PageValidate.GetTrim(this.txtFileds03.Text);
            string Fileds04 = PageValidate.GetTrim(this.txtFileds04.Text);
            string Fileds05 = PageValidate.GetTrim(this.txtFileds05.SelectedValue);
            string Fileds06 = PageValidate.GetTrim(this.txtFileds06.SelectedValue);
            string Fileds07 = PageValidate.GetTrim(this.txtFileds07.SelectedValue);
            string Fileds08 = PageValidate.GetTrim(this.txtFileds08.SelectedValue);
            string Fileds09 = PageValidate.GetTrim(this.txtFileds09.SelectedValue);
            string Fileds10 = PageValidate.GetTrim(this.txtFileds10.Value);
            string Fileds11 = PageValidate.GetTrim(this.txtFileds11.Text);
            string Fileds12 = PageValidate.GetTrim(this.txtFileds12.Text);
            string Fileds13 = PageValidate.GetTrim(this.txtFileds13.Text);
            string Fileds14 = PageValidate.GetTrim(this.txtFileds14.Text);
            string Fileds15 = PageValidate.GetTrim(this.txtFileds15.SelectedValue);
            string Fileds16 = PageValidate.GetTrim(this.txtFileds16.Value);
            string Fileds17 = PageValidate.GetTrim(this.txtFileds17.Value);
            string Fileds18 = PageValidate.GetTrim(this.txtFileds18.Text);

            string F_strErr = "", F_id = "";
            int arr_num = 18;
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
                        //arr_Fileds[i] = CommPage.replaceNUM(PageValidate.GetTrim(Request.Form["txtFileds" + F_id]));
                        arr_Fileds[i] = PageValidate.GetTrim(Request.Form["txtFileds" + F_id]);
                        if (arr_Labeltype[i] == "0")
                        {
                            //0������֤
                        }
                        else if (arr_Labeltype[i] == "1")
                        {
                            //1 ���ֻ���������Ϊ��
                            if (i < 13)
                            {
                                if (String.IsNullOrEmpty(arr_Fileds[i]))
                                {
                                    F_strErr += arr_Label[i] + "����Ϊ�գ�\\n";
                                }
                            }
                        }
                    }
                }
                else
                {
                    strErr += "���ô������飡";
                }
            }
            if (Fileds05 == "��ѡ��")
            {
                strErr += "��ѡ��䶯���ͣ�";
            }
            strErr += F_strErr;
            if (strErr != "")
            {
                arr_Fileds = null;
                arr_Label = null;
                MessageBox.Show(this, strErr);
                return;
            }

            if (this.hd_IsUp.Value == "1")
            {
                m_ActionName = "tb_ldedit";
            }

            //�жϱ�ͷ�Ƿ����
            if (m_ActionName == "tb_ldadd")
            {
                if (m_UserName != "")
                {
                    m_ObjID = CommPage.GetSingleVal("select RptID from RPT_Basis where AreaCode = '" + str_AreaCode + "' and Attribs!=4  and FuncNo = '" + m_FuncCode + "' and RptTime = '" + this.txt_RptTime.Value + "'");
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

                            //string RCount = DbHelperSQL.GetSingle("select count(1) from RPT_Basis where substring(AreaCode,1,6)='" + str_AreaCode.Substring(0, 6) + "'").ToString();
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
                    m_SqlParams = "UPDATE RPT_Basis SET ";
                    m_SqlParams += "AreaCode='" + str_AreaCode + "',AreaName='" + str_AreaName + "',RptTime ='" + txt_RptTime + "',SldHeader ='" + txt_SldHeader + "',SldLeader ='" + txt_SldLeader + "',OprateDate ='" + txt_OprateDate + "'";
                    m_SqlParams += " WHERE Attribs=0 and RptID='" + m_ObjID + "'";

                    DbHelperSQL.ExecuteSql(m_SqlParams);
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
                    DataBind();
                    this.hd_upId.Value = "";
                    MessageBox.ShowAndRedirect(this.Page, m_NavTitle + "�ı�ͷ�������Ϣ���³ɹ���", editurl, true, true);
                    return;
                }
                else
                {
                    //���ݴ����ж��Ƿ�ô��Ѿ��������Ϣ
                    string Is_AreaName = CommPage.GetSingleVal("select RptID from RPT_Contents where RptID = '" + m_ObjID + "' and  Attribs != 4 and and AreaCode = '" + str_AreaCode + "'");
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
                            if (arr_Fileds[i] == "��ѡ��")
                            {
                                arr_Fileds[i] = "";
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
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
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
    }
}
