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
        private string m_UserID; // 当前登录的操作用户编号

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;
        private string m_UserName;

        public string IsReported = "0";   //判断是否上报  0、表示未上报 1、表示上报

        #region 基础信息
        public string str_RptTime = "";   //年份月份
        public string str_AreaName = "";   //报表单位
        public string str_AreaCode = "";    //填报单位编码
        public string str_SldHeader = "";    //负责人
        public string str_SldLeader = "";   //填表人
        public string str_OprateDate = "";   //填报日期/报出日期
        #endregion

        public string url = "";
        protected string js_value = "";

        #region 统计合计
        public int[] arrNum = new int[18];
        public int[] arrNum2 = new int[18];
        #endregion

        #region 页面中的信息来源的提示参数
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
                Rpt_headType(m_ObjID);//表头年月
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
                    rptTime = (year - 1) + "年上半年";
                    jiequYear = year - 1;
                }
                if (quarter == 2)
                {
                    rptTime = year + "年下半年";
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

                //期末常住人口数
                try
                {
                    lbl_txtFileds03 = DbHelperSQL.GetSingle("select count(*) from V_PIS where FuncNo = '01010201' and  AreaCode = '" + this.dr_DataAreaSel.SelectedValue + "' and Fileds10 != '离婚'").ToString();
                }
                catch
                {
                    lbl_txtFileds03 = "0";
                }
                //本期出生人数-一孩出生-男
                int int_Fileds05 = 0;
                DataTable tableFileds05 = null;
                if (quarter == 1)
                {
                    tableFileds05 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-07-01' and Fileds05 <= '" + jiequYear + "-12-31' and Fileds12 = '男' and Fileds04='1'");
                }
                else
                {
                    tableFileds05 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-01-01' and Fileds05 <= '" + jiequYear + "-06-30' and Fileds12 = '男' and Fileds04='1'");
                }

                if (tableFileds05 != null)
                {
                    foreach (DataRow item_5 in tableFileds05.Rows)
                    {
                        int_Fileds05 += Convert.ToInt32(item_5["Fileds04"].ToString());  //出生-人数
                    }
                    lbl_txtFileds05 = int_Fileds05.ToString();    //出生-人数
                }

                //本期出生人数-一孩出生-女
                int int_Fileds06 = 0;
                DataTable tableFileds06 = null;
                if (quarter == 1)
                {
                    tableFileds06 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-07-01' and Fileds05 <= '" + jiequYear + "-12-31' and Fileds12 = '女' and Fileds04='1'");
                }
                else
                {
                    tableFileds06 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-01-01' and Fileds05 <= '" + jiequYear + "-06-30' and Fileds12 = '女' and Fileds04='1'");
                }

                if (tableFileds06 != null)
                {
                    foreach (DataRow item_6 in tableFileds06.Rows)
                    {
                        int_Fileds06 += Convert.ToInt32(item_6["Fileds04"].ToString());  //出生-人数
                    }
                    lbl_txtFileds06 = int_Fileds06.ToString();    //出生-人数
                }


                //本期出生人数-二孩出生-男
                int int_Fileds08 = 0;
                DataTable tableFileds08 = null;
                if (quarter == 1)
                {
                    tableFileds08 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-07-01' and Fileds05 <= '" + jiequYear + "-12-31' and Fileds12 = '男' and Fileds04='2'");
                }
                else
                {
                    tableFileds08 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-01-01' and Fileds05 <= '" + jiequYear + "-06-30' and Fileds12 = '男' and Fileds04='2'");
                }

                if (tableFileds08 != null)
                {
                    foreach (DataRow item_8 in tableFileds08.Rows)
                    {
                        int_Fileds08 += Convert.ToInt32(item_8["Fileds04"].ToString());  //出生-人数
                    }
                    lbl_txtFileds08 = int_Fileds08.ToString();    //出生-人数
                }

                //本期出生人数-二孩出生-女
                int int_Fileds09 = 0;
                DataTable tableFileds09 = null;
                if (quarter == 1)
                {
                    tableFileds09 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-07-01' and Fileds05 <= '" + jiequYear + "-12-31' and Fileds12 = '女' and Fileds04='2'");
                }
                else
                {
                    tableFileds09 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-01-01' and Fileds05 <= '" + jiequYear + "-06-30' and Fileds12 = '女' and Fileds04='2'");
                }

                if (tableFileds09 != null)
                {
                    foreach (DataRow item_9 in tableFileds09.Rows)
                    {
                        int_Fileds09 += Convert.ToInt32(item_9["Fileds04"].ToString());  //出生-人数
                    }
                    lbl_txtFileds09 = int_Fileds09.ToString();    //出生-人数
                }


                //本期出生人数-多孩出生-男
                int int_Fileds11 = 0;
                DataTable tableFileds11 = null;
                if (quarter == 1)
                {
                    tableFileds11 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-07-01' and Fileds05 <= '" + jiequYear + "-12-31' and Fileds12 = '男' and (Fileds04!='1' and Fileds04!='2')");
                }
                else
                {
                    tableFileds11 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-01-01' and Fileds05 <= '" + jiequYear + "-06-30' and Fileds12 = '男' and (Fileds04!='1' and Fileds04!='2')");
                }

                if (tableFileds11 != null)
                {
                    foreach (DataRow item_11 in tableFileds11.Rows)
                    {
                        int_Fileds11 += Convert.ToInt32(item_11["Fileds04"].ToString());  //出生-人数
                    }
                    lbl_txtFileds11 = int_Fileds11.ToString();    //出生-人数
                }

                //本期出生人数-多孩出生-女
                int int_Fileds12 = 0;
                DataTable tableFileds12 = null;
                if (quarter == 1)
                {
                    tableFileds12 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-07-01' and Fileds05 <= '" + jiequYear + "-12-31' and Fileds12 = '女' and (Fileds04!='1' and Fileds04!='2')");
                }
                else
                {
                    tableFileds12 = GetComputeNum("select Fileds04 from V_PIS where FuncNo = '01010111' and Fileds05 >= '" + jiequYear + "-01-01' and Fileds05 <= '" + jiequYear + "-06-30' and Fileds12 = '女' and (Fileds04!='1' and Fileds04!='2')");
                }

                if (tableFileds12 != null)
                {
                    foreach (DataRow item_12 in tableFileds12.Rows)
                    {
                        int_Fileds12 += Convert.ToInt32(item_12["Fileds04"].ToString());  //出生-人数
                    }
                    lbl_txtFileds12 = int_Fileds12.ToString();    //出生-人数
                }

                //女性初婚人数-人数
                int fileds15 = 0;
                string int_Fileds15 = "";
                DataTable tableFileds15 = null;
                if (quarter == 1)
                {
                    tableFileds15 = GetComputeNum("select Fileds13 from V_PIS where FuncNo = '01010201' and Fileds10 = '初婚'");
                }
                else
                {
                    tableFileds15 = GetComputeNum("select Fileds13 from V_PIS where FuncNo = '01010201' and Fileds10 = '初婚'");
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

                //女性初婚人数-其中十九岁以下结婚数
                int fileds16 = 0;
                string int_Fileds16 = "";
                DataTable tableFileds16 = null;
                if (quarter == 1)
                {
                    tableFileds16 = GetComputeNum("select Fileds13 from V_PIS where FuncNo = '01010201' and Fileds10 = '初婚' and Fileds03 >= '" + jiequYear + "-07-01' and Fileds03 <= '" + jiequYear + "-12-31'");
                }
                else 
                {
                    tableFileds16 = GetComputeNum("select Fileds13 from V_PIS where FuncNo = '01010201' and Fileds10 = '初婚' and Fileds03 >= '" + jiequYear + "-01-01' and Fileds03 <= '" + jiequYear + "-06-30'");
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

                //女性初婚人数-其中二十三岁以上结婚数
                int fileds17 = 0;
                string int_Fileds17 = "";
                DataTable tableFileds17 = null;
                if (quarter == 1)
                {
                    try
                    {
                        int_Fileds17 = GetComputeNum("select Fileds13 from V_PIS where FuncNo = '01010201' and Fileds10 = '初婚' and Fileds03 >= '" + jiequYear + "-07-01' and Fileds03 <= '" + jiequYear + "-12-31'").ToString();
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
                        int_Fileds17 = GetComputeNum("select Fileds13 from V_PIS where FuncNo = '01010201' and Fileds10 = '初婚' and Fileds03 >= '" + jiequYear + "-01-01' and Fileds03 <= '" + jiequYear + "-06-30'").ToString();
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

                //死亡-人数
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
                //期末总人口-户籍在本村的
                lbl_txtFileds02 = DbHelperSQL.GetSingle("select Count(*) from v_PisQyk where FuncNo = '01010101' and  AreaCode = '" + this.dr_DataAreaSel.SelectedValue + "'").ToString();
            }
        }

        /// <summary>
        /// 计算提示的数量
        /// </summary>
        /// <param name="where">sql语句</param>
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
        /// 得到当前季度
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
        /// 设置操作行为
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;
            switch (m_ActionName)
            {
                case "tb_ldadd": // 新增
                    funcName = "新增";
                    break;
                case "view": // 查看
                    funcName = "查看内容";
                    break;
                case "del": // 删除
                    funcName = "删除";
                    DelBasisInfo(m_ObjID);
                    break;
                case "edit": // 查看
                    funcName = "查看内容";
                    EditRPTInfo(m_ObjID);
                    break;
                default:
                    Response.Write(" <script>alert('操作失败：参数错误！') ;window.location.href='" + m_TargetUrl + "'</script>");
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; 人口计生报表 &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
        }

        /// <summary>
        /// 绑定数据
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

                //根据Id得到人口流动的信息
                string sqlall = "";
                sqlall = "select * from RPT_Contents where Content_Type=1 and RptID ='" + m_Id + "' order by CreateDate desc";
                try
                {
                    table = DbHelperSQL.Query(sqlall).Tables[0];
                    Rep_num(table);//统计
                }
                catch { }

                this.rep_Data.DataSource = table;
                this.rep_Data.DataBind();
                table = null;
            }
        }

        /// <summary>
        /// 修改 流动人口信息UcAreaSe08
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
                    AreaCode_Fun(m_ObjID);//重新遍历区划
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
                // 是否可编辑 SetAreaCode
                if (!isEdit)
                {
                    Response.Write(" <script>alert('操作失败：您选择的信息包含通过审核、修正或公开的信息；该信息不允许编辑！') ;window.location.href='/UnvCommList.aspx?1=1&FuncCode=" + m_FuncCode + "&FuncNa=" + m_NavTitle + "'</script>");
                }
            }
            catch { if (sdr != null) sdr.Close(); }
        }
        /// <summary>
        /// 表头年月
        /// </summary>
        /// <param name="m_ObjID"></param>
        private void Rpt_headType(string m_ObjID)
        {

            //近三年季度
            int Rptnum = int.Parse(DateTime.Now.ToString("yyyy"));
            int ii = 0;
            for (int i = 0; i < 3; i++)
            {
                txt_RptTime.Items.Insert(ii, (Rptnum - i).ToString() + "年上半年");
                ii += 1;
                txt_RptTime.Items.Insert(ii, (Rptnum - i).ToString() + "年下半年");
                ii += 1;
            }
            //设定txt_RptTime选中项
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
                //说明新增，移出已存在的季度
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
        /// 当前区划下级区划
        /// </summary>
        /// <param name="m_ObjID"></param>
        private void AreaCode_Fun(string m_ObjID)
        {
            DataTable table_Code = null;
            if (str_AreaCode != "")
            {
                //过滤已存在的区划
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
                    //编辑表行时需要装将库中已有的区划列入
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
                        this.dr_DataAreaSel.Items.Insert(0, new ListItem("--请选择--"));
                    }
                    else
                    {
                        this.dr_DataAreaSel.Items.Insert(0, new ListItem("--无--"));
                    }
                    table_Code = null;
                }
                catch { }
            }


        }
        /// <summary>
        /// 编辑时判断是否已上报
        /// </summary>
        /// <param name="objID"></param>
        private void EditRPTInfo(string m_ObjID)
        {
            if (!string.IsNullOrEmpty(m_ObjID))
            {
                //判断该数据是否上报，如已经上报则不能新增编辑
                if (CommPage.GetSingleVal("select Attribs from RPT_Basis where RptID = '" + m_ObjID + "'") != "0")
                {
                    //0.未上报 1.已上报 2.未审核 3.已审核 4.无效  9.归档
                    this.txt_SldHeader.Enabled = false;
                    this.txt_SldLeader.Enabled = false;
                    this.txt_OprateDate.Disabled = true;
                    MessageBox.ShowAndRedirect(this.Page, "该信息已经上报，不能操作！", "'/UnvCommList.aspx?1=1&FuncCode=" + m_FuncCode + "&FuncNa=" + m_NavTitle + "'", true, true);
                    return;
                }
            }
        }
        /// <summary>
        /// 删除表头信息，更新键值
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
                        MessageBox.ShowAndRedirect(this.Page, "该信息已经上报，不能操作！", m_TargetUrl, true, true);
                        return;
                    }
                    else
                    {
                        m_SqlParams = "UPDATE RPT_Basis SET Attribs=4 WHERE RptID='" + objID + "'";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        MessageBox.ShowAndRedirect(this.Page, "操作成功，您选择的信息删除成功！", m_TargetUrl, true, true);
                        return;
                    }
                }
                catch { }
            }
            catch { }
        }

        /// <summary>
        /// 删除信息表行数据信息，物理删除
        /// </summary>
        /// <param name="objID"></param>
        private void DelInfo(string objID)
        {
            try
            {
                try
                {
                    //判断已行所在表是否上报
                    if (CommPage.GetSingleVal("select Attribs from RPT_Basis where RptID = '" + m_ObjID + "'") == "0")
                    {
                        m_SqlParams = "DELETE FROM RPT_Contents WHERE CommID IN(" + objID + ") and Attribs=0  ";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                        MessageBox.ShowAndRedirect(this.Page, "操作成功，您选择的信息删除成功！", m_TargetUrl, true, true);
                        return;
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "该信息已经审核，不能删除操作！", m_TargetUrl, true, true);
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
                cssLink.Attributes.Add("href", cssFile);//url为css路径 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        /// <summary>
        /// 身份验证
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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
            else
            {
                //根据用户Id得到用户信息
                str_AreaCode = CommPage.GetSingleVal("select UserAreaCode from USER_BaseInfo where UserID = " + m_UserID);
                str_AreaName = BIZ_Common.GetAreaName(str_AreaCode, "");
            }
        }

        /// <summary>
        /// 验证接受的参数
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
                m_NavTitle = "(半)年度人口与出生情况";
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
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
                    //已删除
                    Response.Write("<script language='javascript'>alert('该信息已删除，不能进行操作！');window.location.href='" + m_TargetUrl + "';</script>");
                    Response.End();
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('该信息已经上报！');;window.location.href='/UnvCommList.aspx?1=1&FuncCode=" + m_FuncCode + "&FuncNa=" + m_NavTitle + "'</script>");
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 婚姻、迁移、流动情况提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;

            //表头值信息
            string txt_RptTime = PageValidate.GetTrim(this.txt_RptTime.SelectedValue);
            string txt_SldHeader = PageValidate.GetTrim(this.txt_SldHeader.Text);
            string txt_SldLeader = PageValidate.GetTrim(this.txt_SldLeader.Text);
            string txt_OprateDate = PageValidate.GetTrim(this.txt_OprateDate.Value);

            //表行列值数据
            string AreaCode = PageValidate.GetTrim(this.dr_DataAreaSel.SelectedValue);
            string AreaName = BIZ_Common.GetAreaName(AreaCode, "");

            if (String.IsNullOrEmpty(txt_SldHeader) || String.IsNullOrEmpty(txt_SldLeader)) { strErr += "请输入负责人和填表人！\\n"; }
            //if (String.IsNullOrEmpty(AreaCode) || String.IsNullOrEmpty(AreaName)) { strErr += "请选择单位！\\n"; }

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
                            //0无需验证
                        }
                        else if (arr_Labeltype[i] == "1")
                        {
                            //1 数字或数并不能为空
                            if (String.IsNullOrEmpty(arr_Fileds[i]) || !PageValidate.IsNumber(PageValidate.GetTrim(arr_Fileds[i])))
                            {
                                F_strErr += arr_Label[i] + "不能为空并且是数字格式！\\n";
                            }
                        }
                    }
                }
                else
                {

                    strErr += "配置错误，请检查！";
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
            //判断表头是否存在
            if (m_ActionName == "tb_ldadd")
            {
                //如果是新增表
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
                            MessageBox.ShowAndRedirect(this.Page, m_NavTitle + "(" + txt_RptTime + ")已存在,并且还未上报！", "chatdata" + m_FuncCode + "01.aspx?action=edit&k=" + m_ObjID + "&sourceUrl=" + m_SourceUrl + "", true, true);
                            return;
                        }
                        else
                        {
                            MessageBox.ShowAndRedirect(this.Page, m_NavTitle + "(" + txt_RptTime + ")已存在！", "/UnvCommList.aspx?1=1&FuncCode=" + m_FuncCode + "&FuncNa=" + m_NavTitle + "", true, true);
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
                        MessageBox.ShowAndRedirect(this.Page, m_NavTitle + "(" + txt_RptTime + ")已存在！", "/UnvCommList.aspx?1=1&FuncCode=" + m_FuncCode + "&FuncNa=" + m_NavTitle + "", true, true);
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
                    //无新增行数据，进行上面表头更新
                    DataBind();
                    this.hd_upId.Value = "";
                    MessageBox.ShowAndRedirect(this.Page, m_NavTitle + "的表头及填表信息更新成功！", editurl, true, true);
                    return;
                }
                else
                {
                    //根据村名判断是否该村已经添加了信息
                    string Is_AreaName = CommPage.GetSingleVal("select RptID from RPT_Contents where RptID = '" + m_ObjID + "' and  Attribs != 4 and and AreaCode = '" + this.dr_DataAreaSel.SelectedValue + "'");
                    if (Is_AreaName != "")
                    {
                        MessageBox.Show(this, m_NavTitle + "中“" + AreaName + "”的信息已存在！");
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
                        MessageBox.ShowAndRedirect(this.Page, m_NavTitle + "中“" + AreaName + "”的数据添加成功！", editurl, true, true);
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
                    MessageBox.ShowAndRedirect(this.Page, m_NavTitle + "中“" + AreaName + "”的数据更新成功！", url, true, true);
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
        /// 转向上报
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
            //表头值信息
        }
        /// <summary>
        /// 流动人口信息编辑、删除事件
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
        /// 列合计
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


        //统计，不同报表统计不同
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
