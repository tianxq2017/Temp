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

namespace join.pms.web
{
    public partial class UnvCart : System.Web.UI.Page
    {
        protected string m_FuncCode;
        private string m_FuncName;
        private string m_SiteArea;
        private string m_SiteAreaNa;
        private string m_UserID;

        private string m_SqlParams;
        private DataSet m_Ds;
        private DataTable m_Dt;

        protected string m_UrlParams;

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateParams();
            AuthenticateUser();

            if (!IsPostBack)
            {
                SetPageStyle(m_UserID);
                SetAreaList("");


                string startDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-01";
                string endDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01").AddDays(-1).ToString("yyyy-MM-dd");

                if(m_FuncCode=="0702" || m_FuncCode=="0707") SetUIParams(startDate, endDate);
                SetChartInit(m_SiteArea, m_SiteAreaNa, startDate, endDate);
            }
        }

        /// <summary>
        /// ��֤���ܵĲ���
        /// </summary>
        private void ValidateParams()
        {
            m_FuncCode = Request.QueryString["FuncCode"];
            m_FuncName = Request.QueryString["FuncNa"];
            m_UrlParams = Request.Url.Query;
            m_SiteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
            m_SiteAreaNa = System.Configuration.ConfigurationManager.AppSettings["SiteAreaName"];
            if (!string.IsNullOrEmpty(m_FuncCode))
            {
                this.LiteralNav.Text = "��ǰλ�ã�" + CommPage.GetAllTreeName(this.m_FuncCode, true);
            }
            else
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }
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

        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "";// DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
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
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string startDate = PageValidate.GetTrim(Request["txtStartTime"]);
            string endDate =  PageValidate.GetTrim(Request["txtEndTime"]);

            string areaCode = this.DDLArea.SelectedItem.Value;
            string areaName = this.DDLArea.SelectedItem.Text;
            string dispName = string.Empty, dispFileds = string.Empty;

            if (m_FuncCode == "0701" || m_FuncCode == "0706")
            {
            }
            else
            {
                if (string.IsNullOrEmpty(startDate))
                {
                    strErr += "��ѡ����ʼ���ڣ�\\n";
                }
                if (string.IsNullOrEmpty(endDate))
                {
                    strErr += "��ѡ���ֹ���ڣ�\\n";
                }
                if (strErr != "")
                {
                    MessageBox.Show(this, strErr);
                    return;
                }
            }
            

            SetChartInit(areaCode, areaName, startDate, endDate);

            SetUIParams(startDate, endDate);
        }
        

        /// <summary>
        /// ��������ѡ��
        /// </summary>
        /// <param name="areaCode"></param>
        private void SetAreaList(string areaCode)
        {
            try
            {
                string m_UserAreaCode = string.Empty;
                string m_UserAreaName = string.Empty;
                string m_UserDept = join.pms.dal.CommPage.GetUnitCodeByUser(m_UserID, ref m_UserAreaCode, ref m_UserAreaName);
                string m_RoleID = join.pms.dal.CommPage.GetSingleVal("SELECT RoleID FROM [v_UserList] WHERE UserID=" + m_UserID);
                if (m_RoleID != "1") {
                    m_SiteArea = m_UserAreaCode;
                    m_SiteAreaNa = m_UserAreaName;
                }

                m_SqlParams = "SELECT AreaCode, AreaName FROM [AreaDetailCN] WHERE ParentCode = '" + m_SiteArea + "' ORDER BY AreaCode";
                DataTable tmpDt = new DataTable();
                tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                DDLArea.DataSource = tmpDt;
                DDLArea.DataTextField = "AreaName";
                DDLArea.DataValueField = "AreaCode";
                DDLArea.DataBind();
                DDLArea.Items.Insert(0, new ListItem(m_SiteAreaNa, m_SiteArea));
                tmpDt = null;
                if (!string.IsNullOrEmpty(areaCode))
                {
                    this.DDLArea.SelectedValue = areaCode;
                }
            }
            catch { }
        }

        /// <summary>
        /// ͼ���ʼ��
        /// </summary>
        /// <param name="areaCode"></param>
        /// <param name="areaName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        private void SetChartInit(string areaCode, string areaName, string startDate, string endDate)
        {
            try {
                string dispName = string.Empty, dispFileds = string.Empty;
                if (m_FuncCode == "0701")
                {
                    this.DDLShape.Text = "Pie";
                    dispName = "��λ,ҵ������";
                    dispFileds = "AreaName,Fileds01";
                    SetChart(areaCode, areaName, startDate, endDate, dispName, dispFileds);
                }
                else if (m_FuncCode == "0702")
                {
                    this.DDLShape.Text = "Pie";
                    dispName = "��λ,ҵ������";
                    dispFileds = "AreaName,Fileds01";
                    SetChart(areaCode, areaName, startDate, endDate, dispName, dispFileds);
                }
                else if (m_FuncCode == "0706")
                {
                    dispName = "��λ,ҵ����������,�����,���ͨ��,��˲���,�鵵";
                    dispFileds = "AreaName,Fileds01,Fileds02,Fileds03,Fileds04,Fileds05";
                    SetChart(areaCode, areaName, startDate, endDate, dispName, dispFileds);
                }
                else if (m_FuncCode == "0707")
                {
                    dispName = "��λ,ҵ����������,�����,���ͨ��,��˲���,�鵵";
                    dispFileds = "AreaName,Fileds01,Fileds02,Fileds03,Fileds04,Fileds05";
                    SetChart(areaCode, areaName, startDate, endDate, dispName, dispFileds);
                }
                else if (m_FuncCode == "0711")
                {
                    dispName = "��λ,�˿�����";
                    dispFileds = "AreaName,Fileds03";
                    SetChart(areaCode, areaName, startDate, endDate, dispName, dispFileds);
                }
                else if (m_FuncCode == "0712")
                {
                    dispName = "��λ,20�����˿�,20~55��,55~70��,70�꼰����";
                    dispFileds = "AreaName,Fileds04,Fileds04,Fileds04,Fileds04";
                    SetChart(areaCode, areaName, startDate, endDate, dispName, dispFileds);
                }
                else
                {
                    dispName = "���,ѧ��ǰ�˿�,�Ͷ����˿�,�����˿�";
                    dispFileds = "AreaName,Fileds04,Fileds04,Fileds04";
                    SetChart(areaCode, areaName, startDate, endDate, dispName, dispFileds);
                } 
            }
            catch(Exception ex){
                Response.Write(ex.Message);
            }
            
        }

        /// <summary>
        /// ���ò�չʾͼ��
        /// </summary>
        /// <param name="areaCode"></param>
        /// <param name="areaName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dispName"></param>
        /// <param name="dispFileds"></param>
        private void SetChart(string areaCode, string areaName, string startDate, string endDate, string dispName, string dispFileds)
        {
            //string statTitle = areaName + "��" + startDate + "��" + endDate + " " + m_FuncName + "��";
            string searchStr = "StartDate>='" + startDate + " 00:00:00' AND StartDate<'" + endDate + " 23:59:59'";
            string statTitle = areaName + "��" + m_FuncName + "��";
            string dbAreaCode = string.Empty, dbAreaName = string.Empty;
            
            string[] aFileds = dispFileds.Split(',');
            string[] aNames = dispName.Split(',');
            string sumFileds = string.Empty;
            if (m_FuncCode == "0701" || m_FuncCode == "0706") searchStr = "1=1";
            // ����չʾ���ݼ�  SUM(CAST(IsNull(Fileds,0) as int))
            m_Ds = new DataSet();
            m_Ds.Tables.Add("Stats");
            // ���Ե�λ�ֶ�,������ͷ
            for (int i = 0; i < aFileds.Length; i++)
            {
                m_Ds.Tables[0].Columns.Add(aNames[i], typeof(string));
                //if (i > 0) sumFileds += "SUM(CAST(IsNull(" + aFileds[i] + ",0) as float)),";

            }
            //if (!string.IsNullOrEmpty(sumFileds)) sumFileds = sumFileds.Substring(0, sumFileds.Length - 1);
            if (m_FuncCode == "0701" || m_FuncCode == "0702")
            {
                sumFileds = "COUNT(*)";
                //searchStr = "OprateDate>='" + startDate + " 00:00:00' AND OprateDate<'" + endDate + " 23:59:59'";
                #region ��ȡ����
                m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + areaCode + "' ORDER BY AreaCode";
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (m_Dt.Rows.Count > 0)
                {
                    for (int k = 0; k < m_Dt.Rows.Count; k++)
                    {
                        dbAreaCode = m_Dt.Rows[k][0].ToString();
                        dbAreaName = m_Dt.Rows[k][1].ToString();
                        DataRow dr = m_Ds.Tables[0].NewRow();
                        dr[0] = dbAreaName;

                        //���ݵ���ʱ������������Ҫ����ʱ����ѡ��ȷ���������������嵽�塢����,�����޷�ͳ��
                        if (dbAreaCode.Substring(6) == "000000")
                        {
                            m_SqlParams = "SELECT " + sumFileds + " FROM BIZ_Contents WHERE  SelAreaCode LIKE '" + dbAreaCode.Substring(0, 6) + "%' AND " + searchStr + " ";
                        }
                        else if (dbAreaCode.Substring(9) == "000")
                        {
                            m_SqlParams = "SELECT " + sumFileds + " FROM BIZ_Contents WHERE  SelAreaCode LIKE '" + dbAreaCode.Substring(0, 9) + "%' AND " + searchStr + " ";
                        }
                        else
                        {
                            m_SqlParams = "SELECT " + sumFileds + " FROM BIZ_Contents WHERE  SelAreaCode='" + dbAreaCode + "' AND " + searchStr + " ";
                        }
                        SqlDataReader sdr = null;
                        try
                        {
                            sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                            while (sdr.Read())
                            {
                                for (int j = 0; j < aFileds.Length - 1; j++)
                                {
                                    dr[j + 1] = IntFormat(sdr[j].ToString());
                                }
                            }
                            sdr.Close();
                        }
                        catch
                        {
                            if (sdr != null) sdr.Close();
                        }
                        m_Ds.Tables[0].Rows.Add(dr);
                    }
                }
                #endregion
            }
            else {

                m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + areaCode + "' ORDER BY AreaCode";
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (m_Dt.Rows.Count > 0)
                {
                    for (int k = 0; k < m_Dt.Rows.Count; k++)
                    {
                        dbAreaCode = m_Dt.Rows[k][0].ToString();
                        dbAreaName = m_Dt.Rows[k][1].ToString();
                        DataRow dr = m_Ds.Tables[0].NewRow();
                        dr[0] = dbAreaName;
                        #region ��ȡ����λ����
                        // dispName = "��λ,ҵ����������,�����,���ͨ��,��˲���,�鵵";
                        //BIZ_Contents --> Attribs: 0,��ʼ�ύ;1,����� 2,ͨ�� 3,���� 4,���� 5,ע�� 6,�ȴ����,9,�鵵
                        if (m_FuncCode == "0706" || m_FuncCode == "0707") {
                            if (dbAreaCode.Substring(6) == "000000")
                            {
                                // ����
                                m_SqlParams = "SELECT TOP 1 ";
                                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE  SelAreaCode LIKE '" + dbAreaCode.Substring(0, 6) + "%' AND " + searchStr + ") As totalBiz,";
                                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE  Attribs IN(0,1,6) AND SelAreaCode LIKE '" + dbAreaCode.Substring(0, 6) + "%' AND " + searchStr + ") As a,";
                                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE  Attribs=2 AND SelAreaCode LIKE '" + dbAreaCode.Substring(0, 6) + "%' AND " + searchStr + ") As b,";
                                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE  Attribs IN (3,4,5) AND SelAreaCode LIKE '" + dbAreaCode.Substring(0, 6) + "%' AND " + searchStr + ") As c,";
                                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE  Attribs=9 AND SelAreaCode LIKE '" + dbAreaCode.Substring(0, 6) + "%' AND " + searchStr + ") As d ";
                                m_SqlParams += "FROM BIZ_Contents";
                            }
                            else if (dbAreaCode.Substring(9) == "000")
                            {
                                // ���
                                m_SqlParams = "SELECT TOP 1 ";
                                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE  SelAreaCode LIKE '" + dbAreaCode.Substring(0, 9) + "%' AND " + searchStr + ") As totalBiz,";
                                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE  Attribs IN(0,1,6) AND SelAreaCode LIKE '" + dbAreaCode.Substring(0, 9) + "%' AND " + searchStr + ") As a,";
                                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE  Attribs=2 AND SelAreaCode LIKE '" + dbAreaCode.Substring(0, 9) + "%' AND " + searchStr + ") As b,";
                                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE  Attribs IN (3,4,5) AND SelAreaCode LIKE '" + dbAreaCode.Substring(0, 9) + "%' AND " + searchStr + ") As c,";
                                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE  Attribs=9 AND SelAreaCode LIKE '" + dbAreaCode.Substring(0, 9) + "%' AND " + searchStr + ") As d ";
                                m_SqlParams += "FROM BIZ_Contents";
                            }
                            else
                            {
                                m_SqlParams = "SELECT TOP 1 ";
                                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE  SelAreaCode='" + dbAreaCode + "' AND " + searchStr + ") As totalBiz ,";
                                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE  Attribs IN(0,1,6) AND SelAreaCode='" + dbAreaCode + "' AND " + searchStr + ") As a ,";
                                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE  Attribs=2 AND SelAreaCode='" + dbAreaCode + "' AND " + searchStr + ") As b,";
                                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE  Attribs IN (3,4,5) AND SelAreaCode='" + dbAreaCode + "' AND " + searchStr + ") As c,";
                                m_SqlParams += "(SELECT COUNT(*) FROM BIZ_Contents WHERE  Attribs=9 AND SelAreaCode='" + dbAreaCode + "' AND " + searchStr + ") As d  ";
                                m_SqlParams += "FROM BIZ_Contents";
                            }
                        }
                        else
                        {
                            m_SqlParams="";
                        }
                        #endregion
                        SqlDataReader sdr = null;
                        try
                        {
                            sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                            while (sdr.Read())
                            {
                                for (int j = 0; j < aFileds.Length - 1; j++)
                                {
                                    dr[j + 1] = IntFormat(sdr[j].ToString());
                                }
                            }
                            sdr.Close();
                        }
                        catch
                        {
                            if (sdr != null) sdr.Close();
                        }
                        m_Ds.Tables[0].Rows.Add(dr);
                    }
                }
                else
                {
                    //sHtml.Append("û��ƥ������ݣ�"); 
                }
                m_Dt = null;
            }
            
            //---------------------------------------------------
            try {
                #region ���������ʽ Bar_2D_Breeze_NoCrystal_Glow_NoBorder
                string appStyle = this.DDLStyle.SelectedValue;
                string shape = this.DDLShape.SelectedValue;
                string weidu = "";// this.DDLDimension.SelectedValue;
                
                if(m_FuncCode=="0701" || m_FuncCode=="0702"){shape="Pie";}
                else{shape="Bar";}

                appStyle = "Bar_2D_Breeze_NoCrystal_Glow_NoBorder";

                if (appStyle == "Line_2D_StarryNight_ThickRound_NoGlow_NoBorder")
                {
                    Chartlet1.AppearanceStyle = FanG.Chartlet.AppearanceStyles.Line_2D_StarryNight_ThickRound_NoGlow_NoBorder;
                }
                else if (appStyle == "Bar_2D_Breeze_NoCrystal_Glow_NoBorder")
                {
                    Chartlet1.AppearanceStyle = FanG.Chartlet.AppearanceStyles.Bar_2D_Breeze_NoCrystal_Glow_NoBorder;
                }
                else
                {
                    Chartlet1.AppearanceStyle = FanG.Chartlet.AppearanceStyles.Pie_3D_Breeze_NoCrystal_NoGlow_NoBorder;
                }
                if (weidu == "Chart3D") { Chartlet1.Dimension = FanG.Chartlet.ChartDimensions.Chart3D; }
                else { Chartlet1.Dimension = FanG.Chartlet.ChartDimensions.Chart2D; }

                switch (shape)
                {
                    case "Bar":
                        Chartlet1.ChartType = FanG.Chartlet.ChartTypes.Bar;
                        break;
                    case "Line":
                        Chartlet1.ChartType = FanG.Chartlet.ChartTypes.Line;
                        break;
                    case "Pie":
                        Chartlet1.ChartType = FanG.Chartlet.ChartTypes.Pie;
                        break;
                    case "Stack":
                        Chartlet1.ChartType = FanG.Chartlet.ChartTypes.Stack;
                        break;
                    case "HBar":
                        Chartlet1.ChartType = FanG.Chartlet.ChartTypes.HBar;
                        break;
                    case "Trend":
                        Chartlet1.ChartType = FanG.Chartlet.ChartTypes.Trend;
                        break;
                    case "Bubble":
                        Chartlet1.ChartType = FanG.Chartlet.ChartTypes.Bubble;
                        break;
                    case "FloatBar":
                        Chartlet1.ChartType = FanG.Chartlet.ChartTypes.FloatBar;
                        break;
                    case "Linear":
                        Chartlet1.ChartType = FanG.Chartlet.ChartTypes.Linear;
                        break;
                    case "Histogram":
                        Chartlet1.ChartType = FanG.Chartlet.ChartTypes.Histogram;
                        break;
                    case "BoxPlot":
                        Chartlet1.ChartType = FanG.Chartlet.ChartTypes.BoxPlot;
                        break;
                    default:
                        Chartlet1.ChartType = FanG.Chartlet.ChartTypes.Line;
                        break;
                }
                #endregion
                //�����Ҫ�ı���ɫ����ȡ������һ�е�ע�ͣ��������޸Ĳ�����ֵ 0 �� 12 (Ԥ����12�ֵ�����ɫ)
                Chartlet1.Fill.ShiftStep = 5; // 10
                if (shape == "Pie") { Chartlet1.Tips.Show = true; } else { Chartlet1.Tips.Show = false; }


                Chartlet1.Crystal.Contraction = 0;
                Chartlet1.Shadow.Distance = 3;
                Chartlet1.Shadow.Alpha = 200;
                Chartlet1.ChartTitle.ForeColor = System.Drawing.Color.DarkGreen;
                Chartlet1.ChartTitle.Text = statTitle;
                //�������ݵ�λ��������Ӧ��� by Ysl 2015/06/06
                if (areaCode == "150524100000") { Chartlet1.Width = 1500; }
                else if (areaCode == "150524101000" || areaCode == "150524102000" || areaCode == "150524103000" || areaCode == "150524104000") { Chartlet1.Width = 1200; }
                else { Chartlet1.Width = 950; }
                Chartlet1.Height = 500;
                Chartlet1.XLabels.UnitText = "��λ";
                Chartlet1.YLabels.UnitText = "����";
                Chartlet1.Tips.Show = true;
                //if (m_FuncCode == "0713") { 
                //    Chartlet1.Tips.Show = true;
                //    Chartlet1.XLabels.UnitText = "���";
                //}
                //Chartlet1.RootPath = "/Chartlet/Chartlet/";
                //�ڶ�������һ������Դ
                m_SqlParams = "";
                Chartlet1.BindChartData(m_Ds);
            }
            catch(Exception ex){}
            

            m_Ds.Dispose();
            m_Ds = null;

        }

        /// <summary>
        /// ���ý������,����ѡ��
        /// </summary>
        private void SetUIParams(string startDate, string endDate)
        {
            if (string.IsNullOrEmpty(startDate))
            {
                startDate = DateTime.Now.AddDays(-10).ToString("yyyy/MM/dd");
            }
            if (string.IsNullOrEmpty(endDate))
            {
                endDate = DateTime.Now.ToString("yyyy/MM/dd");
            }
            this.txtStartTime.Value = startDate;
            this.txtEndTime.Value = endDate;
        }

        private string FloatFormat(object inStr)
        {
            if (inStr == null) { return "0"; }
            else { return inStr.ToString().Trim(); }
        }

        private string IntFormat(object inStr)
        {
            if (inStr == null) { return "0"; }
            else
            {
                if (PageValidate.IsNumberFormat(inStr.ToString().Trim()))
                {
                    return float.Parse(inStr.ToString().Trim()).ToString("F0");
                }
                else { return "0"; }
            }
        }

        /// <summary>
        /// ��ʽ������
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static string GetFormatDate(string inputData)
        {
            if (String.IsNullOrEmpty(inputData))
            {
                return inputData;
            }
            else
            {
                if (PageValidate.IsDateTime(inputData.Trim()))
                {
                    return DateTime.Parse(inputData.Trim()).ToString("yyyy/MM/dd");
                }
                else { return inputData.Trim(); }
            }
        }
    }
}
