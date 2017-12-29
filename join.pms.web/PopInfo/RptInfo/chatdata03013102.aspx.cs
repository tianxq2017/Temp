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

        public string IsReported = "0";   //判断是否上报  0、表示未上报 1、表示上报

        private string m_UserID; // 当前登录的操作用户编号

        private string m_SqlParams;
        public string m_TargetUrl;
        private string m_NavTitle;
        private string m_UserName;
        public string countyparam = "";   //点击镇名的参数

        #region 基础信息
        public string str_RptTime = "";   //年份月份
        public string str_UnitName = "";   //报表单位
        public string str_SldHeader = "";    //负责人
        public string str_SldLeader = "";   //填表人
        public string str_OprateDate = "";   //填报日期/报出日期
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

            //根据编码得到表头的Id
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

                //根据Id得到信息是否审核
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
                this.txt_RptTime.Value = DateTime.Now.ToString("yyyy年MM月");
            }
            else
            {
                //修改表头信息
                string txt_RptTime = PageValidate.GetTrim(this.txt_RptTime.Value);    //月份
                string txt_SldHeader = PageValidate.GetTrim(this.txt_SldHeader.Text);
                string txt_SldLeader = PageValidate.GetTrim(this.txt_SldLeader.Text);
                string txt_OprateDate = PageValidate.GetTrim(this.txt_OprateDate.Value);


                string AreaCode = join.pms.dal.CommPage.GetSingleVal("select AreaCode from RPT_Basis where AreaName = '" + str_UnitName + "'");

                string subAreaCode = AreaCode.Substring(0, 9) + "000";
                try
                {
                    //修改表头信息
                    m_SqlParams = "UPDATE RPT_Basis SET ";
                    m_SqlParams += "RptTime='" + txt_RptTime + "',SldHeader='" + txt_SldHeader + "',SldLeader ='" + txt_SldLeader + "',OprateDate ='" + txt_OprateDate + "'";
                    m_SqlParams += " WHERE RptID=" + m_ObjID;
                    DbHelperSQL.ExecuteSql(m_SqlParams);


                    string sqlArea = "select * from RPT_Basis where AreaCode = '" + subAreaCode + "'";
                    DataTable dt = DbHelperSQL.Query(sqlArea).Tables[0];
                    //根据区划查询镇级表头是否创建
                    if (dt.Rows.Count <= 0)
                    {
                        //根据去换编码得到区划名称
                        string subAreaName = join.pms.dal.CommPage.GetSingleVal("select AreaName from AreaDetailCN where AreaCode = '" + subAreaCode + "'");
                        //创建镇级的表头
                        m_SqlParams = "INSERT INTO [RPT_Basis](";
                        m_SqlParams += "FuncNo,AreaCode,AreaName,RptName,UnitName,RptTime,SldHeader,SldLeader,UserID,RptUserName,CreateDate,OprateDate";
                        m_SqlParams += ") VALUES(";
                        m_SqlParams += "'030131','" + subAreaCode + "','" + subAreaName + "','" + m_NavTitle + "','" + subAreaName + "','" + Convert.ToDateTime(DateTime.Now).ToString("yyyy年MM月") + "','','','" + m_UserID + "','" + m_UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                        m_SqlParams += ")";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                    }

                    //修改表头中的信息
                    string sql_status = "UPDATE RPT_Basis SET RPT_status=1 WHERE RptID=" + m_ObjID;

                    //上报信息
                    string sql = "UPDATE RPT_Contents SET Fileds29='1' WHERE RptID=" + m_ObjID; ;
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
                                DbHelperSQL.ExecuteSql(sql_status);
                                DbHelperSQL.ExecuteSql(sql);
                                Response.Write(" <script>alert('人口动态信息报告单<一>审核成功成功！') ;window.location.href='" + m_TargetUrl + "'</script>");
                            }
                        }
                        catch
                        {
                            Response.Write(" <script>alert('人口动态信息报告单<一>数据审核失败！') ;window.location.href='" + m_TargetUrl + "'</script>");
                            return;
                        }
                    }
                    else
                    {
                        Response.Write(" <script>alert('请确认审核！') ;</script>");
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
                //根据id得到基础信息
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

                //根据编码得到表头的Id
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

                //根据Id得到人口流动的信息
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
                m_NavTitle = "人口动态信息报告单<一>";
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = Request.QueryString["RID"];
        }
    }
}
