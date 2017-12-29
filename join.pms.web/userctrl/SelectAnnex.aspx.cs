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

using System.Globalization;
using System.IO;
using System.Text;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.web.userctrl
{
    public partial class SelectAnnex : System.Web.UI.Page
    {
        public string txt_ID = "";
        public string txt_nameId = "";

        private DataTable m_Dt;
        private string m_SvrsUrl = System.Configuration.ConfigurationManager.AppSettings["SvrUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            txt_ID = PageValidate.GetFilterSQL(Request.QueryString["txt_ID"]);
            txt_nameId = PageValidate.GetFilterSQL(Request.QueryString["txt_nameId"]);
            //业务编号
            string bizID = PageValidate.GetFilterSQL(Request.QueryString["bizID"]);
            string nan_PersonID = "0,";  //男方个人信息id
            string nv_PersonID = "0,";  //女方个人信息id
            string txtbizids= "0,";  //身份证相关的所有业务id

            if (!IsPostBack)
            {
                if (bizID != "")
                {
                    //根据业务编号得到男女双方的身份证号 
                    //SqlDataReader sdr = null;
                    try
                    {
                        m_Dt = new DataTable();

                        string sql = "select PersonCidA,PersonCidB from BIZ_Contents where bizID=" + bizID;
                        m_Dt = DbHelperSQL.Query(sql).Tables[0];
                        if (m_Dt.Rows.Count > 0)
                        {
                            foreach (DataRow sdr in m_Dt.Rows)
                            {
                                //男方身份证号 
                                string nan_num = sdr["PersonCidA"].ToString();
                                //根据身份证号得到个人信息ID
                                if (nan_num != "")
                                {
                                    string sql_nan = "select PersonID from BIZ_Persons where PersonCardID = '" + nan_num + "'";
                                    try
                                    {
                                        m_Dt = new DataTable();
                                        m_Dt = DbHelperSQL.Query(sql_nan).Tables[0];
                                        if (m_Dt.Rows.Count > 0)
                                        {
                                            foreach (DataRow sdr_1 in m_Dt.Rows)
                                            {
                                                nan_PersonID += sdr_1["PersonID"].ToString() + ",";
                                            }
                                        }
                                        m_Dt = null;
                                    }
                                    catch
                                    {
                                        m_Dt = null;
                                    }
                                    
                                    m_Dt = new DataTable();

                                    string sql_biz1 = "select BizID from BIZ_Contents where PersonCidA = '" + nan_num + "' or  PersonCidB = '" + nan_num + "'";
                                    try
                                    {
                                        m_Dt = DbHelperSQL.Query(sql_nan).Tables[0];
                                        if (m_Dt.Rows.Count > 0)
                                        {
                                            foreach (DataRow sdr_2 in m_Dt.Rows)
                                            {
                                                txtbizids += sdr_2["BizID"].ToString() + ",";
                                            }
                                        }
                                        m_Dt = null;
                                    }
                                    catch
                                    {
                                        m_Dt = null;
                                    }

                                }

                                //女方身份证号
                                string nv_num = sdr["PersonCidB"].ToString();
                                //根据身份证号得到个人信息ID
                                //根据身份证号得到个人信息ID
                                if (nv_num != "")
                                {
                                    m_Dt = new DataTable();

                                    string sql_nan = "select PersonID from BIZ_Persons where PersonCardID = '" + nv_num + "'";
                                    try
                                    {
                                        m_Dt = DbHelperSQL.Query(sql_nan).Tables[0];
                                        if (m_Dt.Rows.Count > 0)
                                        {
                                            foreach (DataRow sdr_2 in m_Dt.Rows)
                                            {
                                                nv_PersonID += sdr_2["PersonID"].ToString() + ",";
                                            }
                                        }
                                        m_Dt = null;
                                    }
                                    catch
                                    {
                                        m_Dt = null;
                                    }

                                    m_Dt = new DataTable();
                                    string sql_biz2 = "select BizID from BIZ_Contents where PersonCidA = '" + nv_num + "' or  PersonCidB = '" + nv_num + "'";
                                    try
                                    {
                                        m_Dt = DbHelperSQL.Query(sql_nan).Tables[0];

                                        if (m_Dt.Rows.Count > 0)
                                        {
                                            foreach (DataRow sdr_2 in m_Dt.Rows)
                                            {
                                                txtbizids += sdr_2["BizID"].ToString() + ",";
                                            }
                                        }
                                        m_Dt = null;
                                    }
                                    catch
                                    {
                                        m_Dt = null;
                                    }
                                }
                            }
                        }
                        m_Dt = null;
                    }
                    catch
                    {
                        m_Dt = null;
                    }

                    m_Dt = new DataTable();
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        string sql = "select * from BIZ_Docs where PersonID in (" + nan_PersonID.TrimEnd(',') + ") OR PersonID in (" + nv_PersonID.TrimEnd(',') + ") or BizID IN (" + txtbizids + bizID + ")  order by CommID desc ";
                        m_Dt = DbHelperSQL.Query(sql).Tables[0];
                        if (m_Dt.Rows.Count > 0)
                        {
                            int ii = 0;
                            foreach (DataRow item in m_Dt.Rows)
                            {
                                ii = ii + 1;
                                sb.Append(ii + ". <a href=\"javascript:void()\" onclick=\"selectImg('" + item["CommID"].ToString() + "','" + item["DocsName"].ToString() + "','" + m_SvrsUrl + item["DocsPath"].ToString() + "','" + item["DocsType"].ToString() + "','" + item["DocsPath"].ToString() + "','" + txt_ID + "','" + txt_nameId + "')\">" + item["DocsName"].ToString() + "</a>　<font size='2' color='#cccccc'>" + Convert.ToDateTime(item["OprateDate"].ToString()).ToString("yyyy-MM-dd") + "</font><br />");
                            }
                            this.ltr_a_Img.Text = sb.ToString();

                        }
                        m_Dt = null;
                    }
                    catch
                    {
                        m_Dt = null;
                    }
                }
            }
            else
            {

            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {

        }
    }
}
