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
    public partial class chatdata03020302 : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_FuncCode;
        public string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号
        public string IsReported = "0";   //判断是否上报  0、表示未上报 1、表示上报

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;
        private string m_UserName;
        public string countyparam = "";   //县级点击镇名的参数

        #region 基础信息
        public string str_RptTime = "";   //年份月份
        public string str_UnitName = "";   //报表单位
        public string str_SldHeader = "";    //负责人
        public string str_SldLeader = "";   //填表人
        public string str_OprateDate = "";   //填报日期/报出日期
        #endregion

        public string url = "";

        #region 统计合计
        public int num1 = 0;    //出生人数
        public int num2 = 0;    //出生计划内一孩人数
        public int num3 = 0;    //出生计划内二孩人数
        public int num4 = 0;    //出生计划外一孩人数
        public int num5 = 0;    //出生计划外二孩人数
        public int num6 = 0;    //出生计划外多孩人数
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

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();


            url = Request.RawUrl;

            if (!IsPostBack)
            {
                if (url.IndexOf("param=") > -1)
                {
                    this.txt_RptTime.Disabled = false;
                    this.txt_SldHeader.Enabled = false;
                    this.txt_SldLeader.Enabled = false;
                    this.txt_OprateDate.Disabled = true;
                }
                else 
                {
                    //得到全镇村子的数量
                    //DataTable table_rural = "";

                }

                IsReported = join.pms.dal.CommPage.GetSingleVal("select Fileds29 from RPT_Contents where RptID = '" + m_ObjID + "'");
                if (IsReported == "1")
                {
                    this.txt_RptTime.Disabled = false;
                    this.txt_SldHeader.Enabled = false;
                    this.txt_SldLeader.Enabled = false;
                    this.txt_OprateDate.Disabled = true;
                }

                this.txt_RptTime.Value = DateTime.Now.Month.ToString();
                DataBind(m_ObjID);
                SetPageStyle(m_UserID);
                SetOpratetionAction(m_NavTitle);

                DataTable table = new DataTable();
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
                { }
                table = null;
            }
            else
            {
                //修改表头信息
                string txt_RptTime = PageValidate.GetTrim(this.txt_RptTime.Value);    //月份
                string txt_SldHeader = PageValidate.GetTrim(this.txt_SldHeader.Text);
                string txt_SldLeader = PageValidate.GetTrim(this.txt_SldLeader.Text);
                string txt_OprateDate = PageValidate.GetTrim(this.txt_OprateDate.Value);

                try
                {
                    //修改表头信息
                    m_SqlParams = "UPDATE RPT_Basis SET ";
                    m_SqlParams += "RptTime='" + txt_RptTime + "',SldHeader='" + txt_SldHeader + "',SldLeader ='" + txt_SldLeader + "',OprateDate ='" + txt_OprateDate + "'";
                    m_SqlParams += " WHERE RptID='" + m_ObjID + "'";
                    DbHelperSQL.ExecuteSql(m_SqlParams);

                    //创建县级表头
                    string sqlArea = "select * from RPT_Basis where AreaCode = '610922000000' and FuncNo='030105'";
                    DataTable dt = DbHelperSQL.Query(sqlArea).Tables[0];
                    //根据区划查询县级表头是否创建
                    if (dt.Rows.Count <= 0)
                    {
                        //根据区划编码得到区划名称
                        string subAreaName = join.pms.dal.CommPage.GetSingleVal("select AreaName from AreaDetailCN where AreaCode = '610922000000'");
                        //创建县级的表头
                        m_SqlParams = "INSERT INTO [RPT_Basis](";
                        m_SqlParams += "FuncNo,AreaCode,AreaName,RptName,UnitName,RptTime,SldHeader,SldLeader,UserID,RptUserName,CreateDate,OprateDate";
                        m_SqlParams += ") VALUES(";
                        m_SqlParams += "'030190','610922000000','" + subAreaName + "','" + m_NavTitle + "','" + subAreaName + "','" + DateTime.Now.Month + "','','','" + m_UserID + "','" + m_UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                        m_SqlParams += ")";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                    }

                    //上报信息
                    string sql = "UPDATE RPT_Contents SET Fileds29='1' WHERE RptID='" + m_ObjID+"'" ;
                    if (this.ck_IsCheck.Checked)
                    {
                        try
                        {
                            if (txt_SldHeader == "" || txt_SldLeader == "")
                            {
                                MessageBox.Show(this, "请完善负责人或者填表人信息！");
                                return;
                            }
                            else
                            {
                                DbHelperSQL.ExecuteSql(sql);
                                Response.Write(" <script>alert('计划生育月报表数据上报成功！') ;window.location.href='" + m_TargetUrl + "'</script>");
                            }
                        }
                        catch
                        {
                            Response.Write(" <script>alert('计划生育月报表数据上报失败！') ;window.location.href='" + m_TargetUrl + "'</script>");
                            return;
                        }
                    }
                    else
                    {
                        Response.Write(" <script>alert('请确认上报！') ;</script>");
                        return;
                    }
                }
                catch
                { }
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
                default:
                    //Response.Write(" <script>alert('操作失败：参数错误！') ;window.location.href='" + m_TargetUrl + "'</script>");
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
                //根据id得到基础信息
                string sql_basisInfo = "";

                if (countyparam == "" || countyparam == null)
                {
                    sql_basisInfo = "select * from RPT_Basis where RptID = '" + m_Id + "'";
                }
                else
                {
                    sql_basisInfo = "select * from RPT_Basis where RptID = '" + countyparam + "'";
                }

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

                //根据Id得到人口流动的信息
                string sqlall = "";
                if (countyparam == "" || countyparam == null)
                {
                    sqlall = "select * from RPT_Contents where Content_Type=1 and RptID ='" + m_Id + "' order by CreateDate desc";
                }
                else
                {
                    sqlall = "select * from RPT_Contents where Content_Type=1 and RptID ='" + countyparam + "' order by CreateDate desc";
                }
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
        }

        /// <summary>
        /// 验证接受的参数
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
                m_NavTitle = "计划生育月报表";
            }
            else
            {
                //Response.Write("非法访问，操作被终止！");
                //Response.End();
            }

            if (m_ActionName == "view") m_ObjID = Request.QueryString["RID"];
        }

        /// <summary>
        /// 统计出生、怀孕
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
        /// 统计手术
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
