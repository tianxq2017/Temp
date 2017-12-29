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
    public partial class chatdata03010701 : System.Web.UI.Page
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
        public string str_AreaName = "";   //����λ
        public string str_AreaCode = "";    //���λ����
        public string str_SldHeader = "";    //������
        public string str_SldLeader = "";   //�����
        public string str_OprateDate = "";   //�����/��������
        #endregion

        public string url = "";
        protected string js_value = "";

        #region ͳ�ƺϼ�
        public int[] arrNum = new int[18];
        public int[] arrNum2 = new int[18];
        #endregion

        #region ҳ���е���Ϣ��Դ����ʾ����
        public string lbl_txtFileds01 = "0";
        public string lbl_txtFileds02 = "0";
        public string lbl_txtFileds03 = "0";
        public string lbl_txtFileds05 = "0";
        public string lbl_txtFileds06 = "0";
        public string lbl_txtFileds08 = "0";
        public string lbl_txtFileds09 = "0";
        public string lbl_txtFileds11 = "0";
        public string lbl_txtFileds12 = "0";
        public string lbl_txtFileds15 = "0";
        public string lbl_txtFileds16 = "0";
        public string lbl_txtFileds17 = "0";
        public string lbl_txtFileds18 = "0";
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

                int quarter = GetQuarterByDate(DateTime.Now);
                int year = DateTime.Now.Year;
                string rptTime = "";
                int jiequYear = DateTime.Now.Year;
                if (quarter == 1)
                {
                    rptTime = (year - 1) + "���ϰ���";
                    jiequYear = year - 1;
                }
                if (quarter == 2)
                {
                    rptTime = year + "���°���";
                    jiequYear = year;
                }

                try
                {
                    lbl_txtFileds01 = DbHelperSQL.GetSingle("select Count(*) from dbo.RPT_Basis where FuncNo = '030106' and AreaName = '" + str_AreaName + "' and RptTime='" + rptTime + "'").ToString();
                }
                catch
                {
                    lbl_txtFileds01 = "0";
                }

                //��ĩ��ס�˿���
                try
                {
                    lbl_txtFileds03 = DbHelperSQL.GetSingle("select count(*) from V_PIS where FuncNo = '01010201' and  AreaCode = '" + this.dr_DataAreaSel.SelectedValue + "' and Fileds10 != '���'").ToString();
                }
                catch
                {
                    lbl_txtFileds03 = "0";
                }
                //���ڳ�������-һ������-��
                int int_Fileds05 = 0;
                DataTable tableFileds05 = null;
                if (quarter == 1)
                {
                    tableFileds05 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-07-01' and Fileds05 <= '" + jiequYear + "-12-31' and Fileds12 = '��' and Fileds04='1'");
                }
                else
                {
                    tableFileds05 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-01-01' and Fileds05 <= '" + jiequYear + "-06-30' and Fileds12 = '��' and Fileds04='1'");
                }

                if (tableFileds05 != null)
                {
                    foreach (DataRow item_5 in tableFileds05.Rows)
                    {
                        int_Fileds05 += Convert.ToInt32(item_5["Fileds04"].ToString());  //����-����
                    }
                    lbl_txtFileds05 = int_Fileds05.ToString();    //����-����
                }

                //���ڳ�������-һ������-Ů
                int int_Fileds06 = 0;
                DataTable tableFileds06 = null;
                if (quarter == 1)
                {
                    tableFileds06 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-07-01' and Fileds05 <= '" + jiequYear + "-12-31' and Fileds12 = 'Ů' and Fileds04='1'");
                }
                else
                {
                    tableFileds06 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-01-01' and Fileds05 <= '" + jiequYear + "-06-30' and Fileds12 = 'Ů' and Fileds04='1'");
                }

                if (tableFileds06 != null)
                {
                    foreach (DataRow item_6 in tableFileds06.Rows)
                    {
                        int_Fileds06 += Convert.ToInt32(item_6["Fileds04"].ToString());  //����-����
                    }
                    lbl_txtFileds06 = int_Fileds06.ToString();    //����-����
                }


                //���ڳ�������-��������-��
                int int_Fileds08 = 0;
                DataTable tableFileds08 = null;
                if (quarter == 1)
                {
                    tableFileds08 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-07-01' and Fileds05 <= '" + jiequYear + "-12-31' and Fileds12 = '��' and Fileds04='2'");
                }
                else
                {
                    tableFileds08 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-01-01' and Fileds05 <= '" + jiequYear + "-06-30' and Fileds12 = '��' and Fileds04='2'");
                }

                if (tableFileds08 != null)
                {
                    foreach (DataRow item_8 in tableFileds08.Rows)
                    {
                        int_Fileds08 += Convert.ToInt32(item_8["Fileds04"].ToString());  //����-����
                    }
                    lbl_txtFileds08 = int_Fileds08.ToString();    //����-����
                }

                //���ڳ�������-��������-Ů
                int int_Fileds09 = 0;
                DataTable tableFileds09 = null;
                if (quarter == 1)
                {
                    tableFileds09 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-07-01' and Fileds05 <= '" + jiequYear + "-12-31' and Fileds12 = 'Ů' and Fileds04='2'");
                }
                else
                {
                    tableFileds09 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-01-01' and Fileds05 <= '" + jiequYear + "-06-30' and Fileds12 = 'Ů' and Fileds04='2'");
                }

                if (tableFileds09 != null)
                {
                    foreach (DataRow item_9 in tableFileds09.Rows)
                    {
                        int_Fileds09 += Convert.ToInt32(item_9["Fileds04"].ToString());  //����-����
                    }
                    lbl_txtFileds09 = int_Fileds09.ToString();    //����-����
                }


                //���ڳ�������-�ຢ����-��
                int int_Fileds11 = 0;
                DataTable tableFileds11 = null;
                if (quarter == 1)
                {
                    tableFileds11 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-07-01' and Fileds05 <= '" + jiequYear + "-12-31' and Fileds12 = '��' and (Fileds04!='1' and Fileds04!='2')");
                }
                else
                {
                    tableFileds11 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-01-01' and Fileds05 <= '" + jiequYear + "-06-30' and Fileds12 = '��' and (Fileds04!='1' and Fileds04!='2')");
                }

                if (tableFileds11 != null)
                {
                    foreach (DataRow item_11 in tableFileds11.Rows)
                    {
                        int_Fileds11 += Convert.ToInt32(item_11["Fileds04"].ToString());  //����-����
                    }
                    lbl_txtFileds11 = int_Fileds11.ToString();    //����-����
                }

                //���ڳ�������-�ຢ����-Ů
                int int_Fileds12 = 0;
                DataTable tableFileds12 = null;
                if (quarter == 1)
                {
                    tableFileds12 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-07-01' and Fileds05 <= '" + jiequYear + "-12-31' and Fileds12 = 'Ů' and (Fileds04!='1' and Fileds04!='2')");
                }
                else
                {
                    tableFileds12 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-01-01' and Fileds05 <= '" + jiequYear + "-06-30' and Fileds12 = 'Ů' and (Fileds04!='1' and Fileds04!='2')");
                }

                if (tableFileds12 != null)
                {
                    foreach (DataRow item_12 in tableFileds12.Rows)
                    {
                        int_Fileds12 += Convert.ToInt32(item_12["Fileds04"].ToString());  //����-����
                    }
                    lbl_txtFileds12 = int_Fileds12.ToString();    //����-����
                }

                //Ů�Գ�������-����
                int fileds15 = 0;
                string int_Fileds15 = "";
                DataTable tableFileds15 = null;
                if (quarter == 1)
                {
                    tableFileds15 = GetComputeNum("select Fileds13 from V_PIS where FuncNo = '01010201' and Fileds10 = '����'");
                }
                else
                {
                    tableFileds15 = GetComputeNum("select Fileds13 from V_PIS where FuncNo = '01010201' and Fileds10 = '����'");
                }

                if (tableFileds15 != null)
                {
                    foreach (DataRow item_2 in tableFileds15.Rows)
                    {
                        int_Fileds15 = item_2["Fileds13"].ToString();
                        string yearDate = DateTime.Now.AddYears(-19).ToString("yyyy");
                        string jiequ6_8 = int_Fileds15.Substring(6, 4);
                        if (Convert.ToInt32(yearDate) < Convert.ToInt32(jiequ6_8))
                        {
                            fileds15++;
                        }
                    }
                }
                lbl_txtFileds15 = fileds15.ToString();

                //Ů�Գ�������-����ʮ�������½����
                int fileds16 = 0;
                string int_Fileds16 = "";
                DataTable tableFileds16 = null;
                if (quarter == 1)
                {
                    tableFileds16 = GetComputeNum("select Fileds13 from V_PIS where FuncNo = '01010201' and Fileds10 = '����' and Fileds03 >= '" + jiequYear + "-07-01' and Fileds03 <= '" + jiequYear + "-12-31'");
                }
                else 
                {
                    tableFileds16 = GetComputeNum("select Fileds13 from V_PIS where FuncNo = '01010201' and Fileds10 = '����' and Fileds03 >= '" + jiequYear + "-01-01' and Fileds03 <= '" + jiequYear + "-06-30'");
                }
                
                if (tableFileds16 != null)
                {
                    foreach (DataRow item_2 in tableFileds16.Rows)
                    {
                        int_Fileds16 = item_2["Fileds13"].ToString();
                        string yearDate = DateTime.Now.AddYears(-19).ToString("yyyy");
                        string jiequ6_8 = int_Fileds16.Substring(6, 4);
                        if (Convert.ToInt32(yearDate) < Convert.ToInt32(jiequ6_8))
                        {
                            fileds16++;
                        }
                    }
                }
                lbl_txtFileds16 = fileds16.ToString();

                //Ů�Գ�������-���ж�ʮ�������Ͻ����
                int fileds17 = 0;
                string int_Fileds17 = "";
                DataTable tableFileds17 = null;
                if (quarter == 1)
                {
                    try
                    {
                        int_Fileds17 = GetComputeNum("select Fileds13 from V_PIS where FuncNo = '01010201' and Fileds10 = '����' and Fileds03 >= '" + jiequYear + "-07-01' and Fileds03 <= '" + jiequYear + "-12-31'").ToString();
                    }
                    catch
                    {
                        int_Fileds17 = "0";
                    }
                    
                }
                else
                {
                    try
                    {
                        int_Fileds17 = GetComputeNum("select Fileds13 from V_PIS where FuncNo = '01010201' and Fileds10 = '����' and Fileds03 >= '" + jiequYear + "-01-01' and Fileds03 <= '" + jiequYear + "-06-30'").ToString();
                    }
                    catch 
                    {
                        int_Fileds17 = "0";
                    }
                }
                if (tableFileds17 != null)
                {
                    foreach (DataRow item_2 in tableFileds17.Rows)
                    {
                        int_Fileds17 = item_2["Fileds13"].ToString();
                        string yearDate = DateTime.Now.AddYears(-23).ToString("yyyy");
                        string jiequ6_8 = int_Fileds17.Substring(6, 4);
                        if (Convert.ToInt32(jiequ6_8) < Convert.ToInt32(yearDate))
                        {
                            fileds17++;
                        }
                    }
                }
                lbl_txtFileds17 = fileds17.ToString();

                //����-����
                if (quarter == 1)
                {
                    try
                    {
                        lbl_txtFileds18 = DbHelperSQL.GetSingle("select count(*) from v_PisQyk where FuncNo = '01010103' and Fileds23 >= '" + jiequYear + "-07-01' and Fileds23 <= '" + jiequYear + "-12-31'").ToString();   
                    }
                    catch
                    {
                        lbl_txtFileds18 = "0";
                    }
                }
                else 
                {
                    try
                    {
                        lbl_txtFileds18 = DbHelperSQL.GetSingle("select count(*) from v_PisQyk where FuncNo = '01010103' and Fileds23 >= '" + jiequYear + "-01-01' and Fileds23 <= '" + jiequYear + "-06-30'").ToString();   
                    }
                    catch
                    {
                        lbl_txtFileds18 = "0";
                    }
                }
            }
            else
            {
                //��ĩ���˿�-�����ڱ����
                lbl_txtFileds02 = DbHelperSQL.GetSingle("select Count(*) from v_PisQyk where FuncNo = '01010101' and  AreaCode = '" + this.dr_DataAreaSel.SelectedValue + "'").ToString();
            }
        }

        /// <summary>
        /// ������ʾ������
        /// </summary>
        /// <param name="where">sql���</param>
        /// <returns></returns>
        private DataTable GetComputeNum(string where)
        {
            try
            {
                return DbHelperSQL.Query(where).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// �õ���ǰ����
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetQuarterByDate(DateTime dt)
        {
            int m = dt.Month;
            if (m >= 1 && m <= 6)
            {
                return 1;
            }
            else if (m >= 7 && m <= 12)
            {
                return 2;
            }
            else
            {
                return 1;
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
                    Rep_num(table);//ͳ��
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
            for (int i = 0; i < 3; i++)
            {
                txt_RptTime.Items.Insert(ii, (Rptnum - i).ToString() + "���ϰ���");
                ii += 1;
                txt_RptTime.Items.Insert(ii, (Rptnum - i).ToString() + "���°���");
                ii += 1;
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
                    table_RptTime = DbHelperSQL.Query("select top 15 RptTime from RPT_Basis where Attribs != 4 and AreaCode = '" + str_AreaCode + "' and FuncNo = '" + m_FuncCode + "' order by  OprateDate  desc ").Tables[0];
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
                string sql_Code = "select AreaName,Areacode from AreaDetailCN where ParentCode = '" + str_AreaCode + "' and Areacode not in(" + not_Area + ") ";
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
        /// ɾ����ͷ��Ϣ�����¼�ֵ
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
                m_FuncCode = "030107";
                m_NavTitle = "(��)����˿���������";
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

        /// <summary>
        /// ������Ǩ�ơ���������ύ
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
                    DataBind();
                    this.hd_upId.Value = "";
                    MessageBox.ShowAndRedirect(this.Page, m_NavTitle + "�ı�ͷ�������Ϣ���³ɹ���", editurl, true, true);
                    return;
                }
                else
                {
                    //���ݴ����ж��Ƿ�ô��Ѿ��������Ϣ
                    string Is_AreaName = CommPage.GetSingleVal("select RptID from RPT_Contents where RptID = '" + m_ObjID + "' and  Attribs != 4 and and AreaCode = '" + this.dr_DataAreaSel.SelectedValue + "'");
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

        protected void dr_DataAreaSel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
