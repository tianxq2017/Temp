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
    public partial class chatdata03020601 : System.Web.UI.Page
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

        public string IsReported = "0";   //�ж��Ƿ��ϱ�  0����ʾδ�ϱ� 1����ʾ�ϱ�

        #region ������Ϣ
        public string str_RptTime = "";   //����·�
        public string str_UnitName = "";   //����λ
        public string str_UnitCode = "";    //���λ����
        public string str_SldHeader = "";    //������
        public string str_SldLeader = "";   //�����
        public string str_OprateDate = "";   //�����/��������
        #endregion

        public string url = "";

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
        public int num21 = 0;
        public int num22 = 0;
        public int num23 = 0;
        public int num24 = 0;
        #endregion

        public string pub_Data_Code = "030105";

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();


            url = Request.RawUrl;

            if (!IsPostBack)
            {
                this.txt_RptTime.Value = DateTime.Now.ToString("yyyy��MM��");

                //�жϱ�ͷ�Ƿ����
                if (m_ActionName != "edit" && m_ActionName != "del")
                {
                    if (url.IndexOf("IsClick=1") < 0)
                    {
                        if (str_UnitName != "")
                        {
                            this.lbl_DataAreaSel.Text = str_UnitName;
                            string Is_Exist = join.pms.dal.CommPage.GetSingleVal("select RptID from RPT_Basis where UnitName = '" + str_UnitName + "' and FuncNo = '" + pub_Data_Code + "' and RptTime = '" + DateTime.Now.Month + "'");
                            if (Is_Exist != "")
                            {
                                MessageBox.ShowAndRedirect(this.Page, "����Ϣ�Ѿ����ڣ�ͬһ���·����ݣ�����ͬʱ������Σ�", m_TargetUrl, true, true);
                                return;
                            }
                        }
                    }
                }


                DataBind(m_ObjID);
                SetPageStyle(m_UserID);
                SetOpratetionAction(m_NavTitle);

                //���ݵ�ǰ��õ���ǰ���µĴ�
                DataTable table_Code = null;
                if (str_UnitCode != "")
                {
                    string sql_Code = "select * from AreaDetailCN where ParentCode = '" + str_UnitCode + "'";
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
                        table_Code = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ShowAndRedirect(this.Page, ex.Message, "", true, true);
                    }
                }

                DataTable table = new DataTable();
                if (m_ObjID != null)
                {
                    string sql = "select * from RPT_Contents where RptID = '" + m_ObjID + "'";
                    try
                    {
                        table = DbHelperSQL.Query(sql).Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            foreach (DataRow item in table.Rows)
                            {
                                num1 += Convert.ToInt32(item["Fileds01"].ToString()) + Convert.ToInt32(item["Fileds02"].ToString()) + Convert.ToInt32(item["Fileds03"].ToString()) + Convert.ToInt32(item["Fileds04"].ToString()) + Convert.ToInt32(item["Fileds05"].ToString());
                                num2 += Convert.ToInt32(item["Fileds01"].ToString());
                                num3 += Convert.ToInt32(item["Fileds02"].ToString());
                                num4 += Convert.ToInt32(item["Fileds03"].ToString());
                                num5 += Convert.ToInt32(item["Fileds04"].ToString());
                                num6 += Convert.ToInt32(item["Fileds05"].ToString());
                                num7 += Convert.ToInt32(item["Fileds06"].ToString());
                                num8 += Convert.ToInt32(item["Fileds07"].ToString()) + Convert.ToInt32(item["Fileds08"].ToString()) + Convert.ToInt32(item["Fileds09"].ToString()) + Convert.ToInt32(item["Fileds10"].ToString()) + Convert.ToInt32(item["Fileds11"].ToString());
                                num9 += Convert.ToInt32(item["Fileds07"].ToString());
                                num10 += Convert.ToInt32(item["Fileds08"].ToString());
                                num11 += Convert.ToInt32(item["Fileds09"].ToString());
                                num12 += Convert.ToInt32(item["Fileds10"].ToString());
                                num13 += Convert.ToInt32(item["Fileds11"].ToString());
                                num14 += Convert.ToInt32(item["Fileds12"].ToString()) + Convert.ToInt32(item["Fileds13"].ToString()) + Convert.ToInt32(item["Fileds14"].ToString()) + Convert.ToInt32(item["Fileds15"].ToString()) + Convert.ToInt32(item["Fileds16"].ToString()) + Convert.ToInt32(item["Fileds17"].ToString());
                                num15 += Convert.ToInt32(item["Fileds12"].ToString());
                                num16 += Convert.ToInt32(item["Fileds13"].ToString());
                                num17 += Convert.ToInt32(item["Fileds14"].ToString());
                                num18 += Convert.ToInt32(item["Fileds14"].ToString());
                                num19 += Convert.ToInt32(item["Fileds15"].ToString());
                                num20 += Convert.ToInt32(item["Fileds16"].ToString());
                                num21 += Convert.ToInt32(item["Fileds17"].ToString());
                                num22 += Convert.ToInt32(item["Fileds18"].ToString());
                                num23 += Convert.ToInt32(item["Fileds19"].ToString());
                                num24 += Convert.ToInt32(item["Fileds19"].ToString());
                            }
                        }
                    }
                    catch
                    {

                    }
                    table = null;
                }
                
                if (m_ActionName == "edit")
                {
                    //�жϸ������Ƿ��ϱ������Ѿ��ϱ����������༭
                    IsReported = join.pms.dal.CommPage.GetSingleVal("select Fileds29 from RPT_Contents where RptID = '" + m_ObjID + "'");
                    if (IsReported == "1")
                    {
                        this.txt_RptTime.Disabled = false;
                        this.txt_SldHeader.Enabled = false;
                        this.txt_SldLeader.Enabled = false;
                        this.txt_OprateDate.Disabled = true;

                        MessageBox.ShowAndRedirect(this.Page, "����Ϣ�Ѿ��ϱ������ܲ�����", m_TargetUrl, true, true);
                        return;
                    }
                }
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
                string Is_NotNull = join.pms.dal.CommPage.GetSingleVal("select RptID from RPT_Basis where UnitName = '" + str_UnitName + "' and FuncNo = '" + pub_Data_Code + "' ");
                if (Is_NotNull != "")
                {
                    m_Id = Is_NotNull;
                    m_ObjID = Is_NotNull;
                }
                if (m_Id != null)
                {
                    string sql_basisInfo = "select * from RPT_Basis where RptID = '" + m_Id + "'";
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
                                this.lbl_DataAreaSel.Text = str_UnitName;
                            }
                        }
                    }
                    catch
                    { }
                    finally
                    {
                        if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                    }
                }

                //����Id�õ��˿���������Ϣ
                string sqlall = "";
                if (m_Id != null)
                {
                    sqlall = "select * from RPT_Contents where Content_Type=1 and RptID ='" + m_Id + "' order by CreateDate desc";
                    try
                    {
                        table = DbHelperSQL.Query(sqlall).Tables[0];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ShowAndRedirect(this.Page, ex.Message, m_TargetUrl, true, true);
                    }

                    this.rep_Data.DataSource = table;
                    this.rep_Data.DataBind();
                    table = null;
                }
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
                m_SqlParams = "SELECT * FROM [RPT_Contents] WHERE CommID=" + objID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    //this.UcDataAreaSel1.SetAreaName(PageValidate.GetTrim(sdr["AreaName"].ToString()));
                    //this.UcDataAreaSel1.SetAreaCode(PageValidate.GetTrim(sdr["AreaCode"].ToString()));
                    this.dr_DataAreaSel.SelectedValue = PageValidate.GetTrim(sdr["AreaCode"].ToString());
                    this.txtFileds01.Text = PageValidate.GetTrim(sdr["Fileds01"].ToString());
                    this.txtFileds02.Text = PageValidate.GetTrim(sdr["Fileds02"].ToString());
                    this.txtFileds03.Text = PageValidate.GetTrim(sdr["Fileds03"].ToString());
                    this.txtFileds04.Text = PageValidate.GetTrim(sdr["Fileds04"].ToString());
                    this.txtFileds05.Text = PageValidate.GetTrim(sdr["Fileds05"].ToString());
                    this.txtFileds06.Text = PageValidate.GetTrim(sdr["Fileds06"].ToString());
                    this.txtFileds07.Text = PageValidate.GetTrim(sdr["Fileds07"].ToString());
                    this.txtFileds08.Text = PageValidate.GetTrim(sdr["Fileds08"].ToString());
                    this.txtFileds09.Text = PageValidate.GetTrim(sdr["Fileds09"].ToString());
                    this.txtFileds10.Text = PageValidate.GetTrim(sdr["Fileds10"].ToString());
                    this.txtFileds11.Text = PageValidate.GetTrim(sdr["Fileds11"].ToString());
                    this.txtFileds12.Text = PageValidate.GetTrim(sdr["Fileds12"].ToString());
                    this.txtFileds13.Text = PageValidate.GetTrim(sdr["Fileds13"].ToString());
                    this.txtFileds14.Text = PageValidate.GetTrim(sdr["Fileds14"].ToString());
                    this.txtFileds15.Text = PageValidate.GetTrim(sdr["Fileds15"].ToString());
                    this.txtFileds16.Text = PageValidate.GetTrim(sdr["Fileds16"].ToString());
                    this.txtFileds17.Text = PageValidate.GetTrim(sdr["Fileds17"].ToString());
                    this.txtFileds18.Text = PageValidate.GetTrim(sdr["Fileds18"].ToString());
                    this.txtFileds19.Text = PageValidate.GetTrim(sdr["Fileds19"].ToString());
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
        /// ɾ����ͷ��Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void DelBasisInfo(string objID)
        {
            try
            {
                //�жϸñ�ͷ�µ������Ƿ��Ѿ��ϱ�������Ѿ��ϱ�����ɾ��
                string sql = "select * from RPT_Contents where RptID = '" + objID + "'";
                string Is_ExistAll = "";
                DataTable table = DbHelperSQL.Query(sql).Tables[0];
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow item in table.Rows)
                    {
                        Is_ExistAll += item["Fileds29"].ToString();
                    }
                }
                table = null;
                if (Is_ExistAll.IndexOf("1") > -1)
                {
                    MessageBox.ShowAndRedirect(this.Page, "����Ϣ�Ѿ��ϱ������ܲ�����", m_TargetUrl, true, true);
                    return;
                }
                else
                {
                    //ɾ����ͷ��Ϣ
                    m_SqlParams = "DELETE FROM RPT_Basis WHERE RptID IN(" + objID + ")";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    //ɾ��������Ϣ
                    m_SqlParams = "DELETE FROM RPT_Contents WHERE RptID =" + objID + "";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                    Response.Write(" <script>alert('�����ɹ�����ѡ�����Ϣɾ���ɹ���') ;window.location.href='" + m_TargetUrl + "'</script>");
                }


            }
            catch (Exception ex)
            {
                MessageBox.ShowAndRedirect(this.Page, ex.Message, m_TargetUrl, true, true);
            }
        }

        /// <summary>
        /// ɾ����Ϣ �����˿���Ϣ
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            try
            {
                if (objID != "")
                {
                    try
                    {
                        m_SqlParams = "DELETE FROM RPT_Contents WHERE CommID IN(" + objID + ")";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        Response.Write(" <script>alert('�����ɹ�����ѡ�����Ϣɾ���ɹ���') ;window.location.href='" + url + "'</script>");
                    }
                    catch
                    { }
                }
                else
                {
                    Response.Write(" <script>alert('����ʧ�ܣ���ѡ�����Ϣ����ͨ����˵���Ϣ����˹�������Ϣ������ɾ����') ;window.location.href='" + url + "'</script>");
                }
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

            //�����û�Id�õ��û���Ϣ
            if (m_UserID != "")
            {
                str_UnitName = join.pms.dal.CommPage.GetSingleVal("select UserName from USER_BaseInfo where UserID = " + m_UserID);
                str_UnitCode = join.pms.dal.CommPage.GetSingleVal("select UserAreaCode from USER_BaseInfo where UserID = " + m_UserID);
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
                m_NavTitle = "�ƻ������±���";
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = Request.QueryString["RID"];
        }

        /// <summary>
        /// ������Ǩ�ơ���������ύ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;

            //�жϱ�ͷ�Ƿ����
            if (m_ActionName == "tb_ldadd")
            {
                if (str_UnitName != "")
                {
                    this.lbl_DataAreaSel.Text = str_UnitName;
                    string Is_Exist = join.pms.dal.CommPage.GetSingleVal("select RptID from RPT_Basis where UnitName = '" + str_UnitName + "' and FuncNo = '" + pub_Data_Code + "' and RptTime = '" + DateTime.Now.Month + "'");
                    if (Is_Exist == "")
                    {
                        //����
                        string AreaCode_a = join.pms.dal.CommPage.GetSingleVal("select AreaCode from AreaDetailCN where AreaName ='" + str_UnitName + "' and ParentCode = '610922000000'");
                        string AreaName_a = str_UnitName;
                        string txt_RptTime = DateTime.Now.Month.ToString();
                        string txt_UnitName = str_UnitName;
                        string txt_SldHeader = this.txt_SldHeader.Text;
                        string txt_SldLeader = this.txt_SldLeader.Text;
                        string txt_OprateDate = DateTime.Now.ToString("yyyy-MM-dd");

                        try
                        {
                            m_SqlParams = "INSERT INTO [RPT_Basis](";
                            m_SqlParams += "FuncNo,AreaCode,AreaName,RptName,UnitName,RptTime,SldHeader,SldLeader,UserID,RptUserName,CreateDate,OprateDate";
                            m_SqlParams += ") VALUES(";
                            m_SqlParams += "'" + m_FuncCode + "','" + AreaCode_a + "','" + AreaName_a + "','" + m_NavTitle + "','" + txt_UnitName + "','" + txt_RptTime + "','" + txt_SldHeader + "','" + txt_SldLeader + "','" + m_UserID + "','" + m_UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + txt_OprateDate + "'";
                            m_SqlParams += ")";

                            DbHelperSQL.ExecuteSql(m_SqlParams);
                        }
                        catch
                        { }
                    }
                }
            }
            else
            {
                //�޸ı�ͷ��Ϣ
                string AreaCode_b = join.pms.dal.CommPage.GetSingleVal("select UserAreaCode from USER_BaseInfo where UserName ='" + str_UnitName + "'");
                string AreaName_b = str_UnitName;
                string txt_RptTime = PageValidate.GetTrim(this.txt_RptTime.Value);
                string txt_UnitName = str_UnitName;
                string txt_SldHeader = PageValidate.GetTrim(this.txt_SldHeader.Text);
                string txt_SldLeader = PageValidate.GetTrim(this.txt_SldLeader.Text);
                string txt_OprateDate = PageValidate.GetTrim(this.txt_OprateDate.Value);

                try
                {
                    m_SqlParams = "UPDATE RPT_Basis SET ";
                    m_SqlParams += "AreaCode='" + AreaCode_b + "',AreaName='" + AreaName_b + "',UnitName ='" + txt_UnitName + "',RptTime ='" + txt_RptTime + "',SldHeader ='" + txt_SldHeader + "',SldLeader ='" + txt_SldLeader + "',OprateDate ='" + txt_OprateDate + "'";
                    m_SqlParams += " WHERE RptID='" + join.pms.dal.CommPage.GetSingleVal("select RptID from RPT_Basis where UnitName = '" + str_UnitName + "' and FuncNo = '" + pub_Data_Code + "'")+"'";

                    DbHelperSQL.ExecuteSql(m_SqlParams);
                }
                catch
                { }
            }

            string RptID = m_ObjID;
            //string AreaCode = PageValidate.GetTrim(this.UcDataAreaSel1.GetAreaCode());
            //string AreaName = PageValidate.GetTrim(this.UcDataAreaSel1.GetAreaName());

            string AreaCode = PageValidate.GetTrim(this.dr_DataAreaSel.SelectedValue);
            string AreaName = join.pms.dal.CommPage.GetSingleVal("select AreaName from AreaDetailCN where Areacode = '" + AreaCode + "'");

            string Fileds01 = PageValidate.GetTrim(this.txtFileds01.Text);
            string Fileds02 = PageValidate.GetTrim(this.txtFileds02.Text);
            string Fileds03 = PageValidate.GetTrim(this.txtFileds03.Text);
            string Fileds04 = PageValidate.GetTrim(this.txtFileds04.Text);
            string Fileds05 = PageValidate.GetTrim(this.txtFileds05.Text);
            string Fileds06 = PageValidate.GetTrim(this.txtFileds06.Text);
            string Fileds07 = PageValidate.GetTrim(this.txtFileds07.Text);
            string Fileds08 = PageValidate.GetTrim(this.txtFileds08.Text);
            string Fileds09 = PageValidate.GetTrim(this.txtFileds09.Text);
            string Fileds10 = PageValidate.GetTrim(this.txtFileds10.Text);
            string Fileds11 = PageValidate.GetTrim(this.txtFileds11.Text);
            string Fileds12 = PageValidate.GetTrim(this.txtFileds12.Text);
            string Fileds13 = PageValidate.GetTrim(this.txtFileds13.Text);
            string Fileds14 = PageValidate.GetTrim(this.txtFileds14.Text);
            string Fileds15 = PageValidate.GetTrim(this.txtFileds15.Text);
            string Fileds16 = PageValidate.GetTrim(this.txtFileds16.Text);
            string Fileds17 = PageValidate.GetTrim(this.txtFileds17.Text);
            string Fileds18 = PageValidate.GetTrim(this.txtFileds18.Text);
            string Fileds19 = PageValidate.GetTrim(this.txtFileds19.Text);


            if (String.IsNullOrEmpty(AreaCode) || String.IsNullOrEmpty(AreaName)) { strErr += "��ѡ��λ��\\n"; }
            if (String.IsNullOrEmpty(Fileds01) || !PageValidate.IsNumber(Fileds01)) { strErr += "����������ƻ���һ������,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds02) || !PageValidate.IsNumber(Fileds02)) { strErr += "����������ƻ��ڶ�������,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds03) || !PageValidate.IsNumber(Fileds03)) { strErr += "����������ƻ���һ������,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds04) || !PageValidate.IsNumber(Fileds04)) { strErr += "����������ƻ����������,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds05) || !PageValidate.IsNumber(Fileds05)) { strErr += "����������ƻ���ຢ����,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds06) || !PageValidate.IsNumber(Fileds06)) { strErr += "��������������,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds07) || !PageValidate.IsNumber(Fileds07)) { strErr += "����������ƻ���һ̥����,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds08) || !PageValidate.IsNumber(Fileds08)) { strErr += "����������ƻ��ڶ�̥����,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds09) || !PageValidate.IsNumber(Fileds09)) { strErr += "����������ƻ���һ̥����,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds10) || !PageValidate.IsNumber(Fileds11)) { strErr += "����������ƻ����̥����,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds11) || !PageValidate.IsNumber(Fileds11)) { strErr += "����������ƻ����̥����,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds12) || !PageValidate.IsNumber(Fileds12)) { strErr += "��������������,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds13) || !PageValidate.IsNumber(Fileds13)) { strErr += "������Ů��������\\n"; }
            if (String.IsNullOrEmpty(Fileds14) || !PageValidate.IsNumber(Fileds14)) { strErr += "����������һ���ϻ�����,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds15) || !PageValidate.IsNumber(Fileds15)) { strErr += "������ȡ������,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds16) || !PageValidate.IsNumber(Fileds16)) { strErr += "��������������,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds17) || !PageValidate.IsNumber(Fileds17)) { strErr += "��������������,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds18) || !PageValidate.IsNumber(Fileds18)) { strErr += "��������֤����,���������ָ�ʽ��\\n"; }
            if (String.IsNullOrEmpty(Fileds19) || !PageValidate.IsNumber(Fileds19)) { strErr += "���������ж�ʮ�������Ͻ��������,���������ָ�ʽ��\\n"; }

            //���ݵ���������������Ƶõ�����
            if (this.hd_IsUp.Value == "1")
            {
                m_ActionName = "tb_ldedit";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            if (m_ActionName == "tb_ldadd" || m_ActionName == "edit")
            {
                //���ݴ����ж��Ƿ�ô��Ѿ��������Ϣ
                string Is_AreaName = join.pms.dal.CommPage.GetSingleVal("select * from RPT_Contents where RptID = '" + join.pms.dal.CommPage.GetSingleVal("select RptID from RPT_Basis where UnitName = '" + str_UnitName + "' and FuncNo = '" + pub_Data_Code + "'") + "' and AreaCode = '" + this.dr_DataAreaSel.SelectedValue + "'");
                if (Is_AreaName != "")
                {
                    MessageBox.ShowAndRedirect(this.Page, "�ô���Ϣ�Ѿ����ڣ�ͬһ���·����ݣ�����ͬʱ������Σ�", url, true, true);
                    return;
                }

                m_SqlParams = "INSERT INTO [RPT_Contents](";
                m_SqlParams += "RptID,AreaCode,AreaName,";
                m_SqlParams += "Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19,";
                m_SqlParams += "CreaterID,CreateDate,Content_Type";

                m_SqlParams += ") VALUES(";
                m_SqlParams += "" + join.pms.dal.CommPage.GetSingleVal("select RptID from RPT_Basis where UnitName = '" + str_UnitName + "' and FuncNo = '" + pub_Data_Code + "'") + ",'" + AreaCode + "','" + AreaName + "',";
                m_SqlParams += "'" + Fileds01 + "','" + Fileds02 + "','" + Fileds03 + "','" + Fileds04 + "','" + Fileds05 + "','" + Fileds06 + "','" + Fileds07 + "','" + Fileds08 + "','" + Fileds09 + "','" + Fileds10 + "','" + Fileds11 + "','" + Fileds12 + "','" + Fileds13 + "','" + Fileds14 + "','" + Fileds15 + "','" + Fileds16 + "','" + Fileds17 + "','" + Fileds18 + "','" + Fileds19 + "',";
                m_SqlParams += "" + m_UserID + ",'" + DateTime.Now + "',1";
                m_SqlParams += ")";
            }
            else if (m_ActionName == "tb_ldedit")
            {
                m_SqlParams = "UPDATE RPT_Contents SET ";
                m_SqlParams += "AreaCode='" + AreaCode + "',AreaName='" + AreaName + "',Fileds01='" + Fileds01 + "',Fileds02='" + Fileds02 + "',Fileds03='" + Fileds03 + "',Fileds04='" + Fileds04 + "',Fileds05='" + Fileds05 + "',Fileds06='" + Fileds06 + "',Fileds07='" + Fileds07 + "',Fileds08='" + Fileds08 + "',Fileds09='" + Fileds09 + "',Fileds10='" + Fileds10 + "',Fileds11='" + Fileds11 + "',Fileds12='" + Fileds12 + "',Fileds13='" + Fileds13 + "',Fileds14='" + Fileds14 + "',Fileds15='" + Fileds15 + "',Fileds16='" + Fileds16 + "',Fileds17='" + Fileds17 + "',Fileds18='" + Fileds18 + "',Fileds19='" + Fileds19 + "',";
                m_SqlParams += "UpdaterID=" + m_UserID + ",UpdateDate='" + DateTime.Now + "'";
                m_SqlParams += " WHERE CommID=" + this.hd_upId.Value;
            }
            try
            {
                DbHelperSQL.ExecuteSql(m_SqlParams);

                DataBind(m_ObjID);
                Response.Write(" <script>alert('�ƻ������±������ݲ����ɹ���') ;window.location.href='" + url + "&IsClick=1'</script>");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }
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
        /// ͳ�Ƴ���������
        /// </summary>
        /// <param name="col1"></param>
        /// <param name="col2"></param>
        /// <param name="col3"></param>
        /// <param name="col4"></param>
        /// <param name="col5"></param>
        public int GetNumByCol(object col1, object col2, object col3, object col4, object col5)
        {
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;

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
            if (col5 != null && col5.ToString() != "")
            {
                try
                {
                    num5 = Convert.ToInt32(col5);
                }
                catch
                { }
            }

            return num1 + num2 + num3 + num4 + num5;
        }

        /// <summary>
        /// ͳ������
        /// </summary>
        /// <param name="col1"></param>
        /// <param name="col2"></param>
        /// <param name="col3"></param>
        /// <param name="col4"></param>
        /// <param name="col5"></param>
        public int GetNumByCol(object col1, object col2, object col3, object col4, object col5, object col6)
        {
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;

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
            if (col5 != null && col5.ToString() != "")
            {
                try
                {
                    num5 = Convert.ToInt32(col5);
                }
                catch
                { }
            }
            if (col6 != null && col6.ToString() != "")
            {
                try
                {
                    num6 = Convert.ToInt32(col6);
                }
                catch
                { }
            }

            return num1 + num2 + num3 + num4 + num5 + num6;
        }
    }
}
