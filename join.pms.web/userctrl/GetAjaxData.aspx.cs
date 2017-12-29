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
using System.Globalization;

using System.Data.SqlClient;
//using System.Xml.Linq;
using System.Collections.Generic;
//using Newtonsoft.Json;
using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.web.userctrl
{
    public partial class GetAjaxData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            StringBuilder jsonStr = new StringBuilder();
            jsonStr.Append("{'data':[");
            int start = Convert.ToInt32(Request.Params["start"].Trim());
            int limit = Convert.ToInt32(Request.Params["limit"].Trim());

            SqlDataReader sdr = null;
            try
            {
                string sqlParams = "select top " + limit + " CommID,OprateModel,OprateContents,OprateUserIP,OprateDate FROM SYS_Log WHERE CommID NOT IN(SELECT TOP " + start + " CommID FROM SYS_Log ORDER BY CommID) ORDER BY CommID";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    jsonStr.Append("{");
                    jsonStr.Append("'CommID':" + sdr.GetInt32(0).ToString() + ","); //sdr["CommID"].ToString()
                    jsonStr.Append("'OprateModel':'" + sdr.GetString(1) + "',");
                    jsonStr.Append("'OprateContents':'" + sdr.GetString(2) + "',");
                    jsonStr.Append("'OprateUserIP':'" + sdr.GetString(3) + "',");
                    jsonStr.Append("'OprateDate':" + sdr.GetDateTime(4).ToString("yyyy/MM/dd") + "");
                    jsonStr.Append("},");

                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            jsonStr.Remove(jsonStr.Length - 1, 1);
            jsonStr.Append("],'totalCount':988}");

            Response.Write(jsonStr); 
             * */
            /*
            #region ·ÖÒ³
            int pagesize = 20;
            int start = 1;
            string field, asc_desc;
            if (string.IsNullOrEmpty(Request["sort"]))
            {
                field = "EmployeeID";
                asc_desc = "asc";
            }
            else
            {
                field = Request["sort"];
                asc_desc = Request["dir"];
            }
            if (!string.IsNullOrEmpty(Request["limit"]))
            {
                pagesize = int.Parse(Request["limit"]);
                start = int.Parse(Request["start"]);
            }
            start = start / pagesize;
            start += 1;
            #endregion
            string strSql = string.Format("select EmployeeID, LastName,FirstName,BirthDate from Employees where EmployeeID between ({0}-1)*{1}+1 and {0}*{1} order by {2} {3} ", start, pagesize, field, asc_desc);
            string strConnection = "Data Source=.;Initial Catalog=Northwind;User ID=sa;password=sa";
            strConnection = "Data Source=127.0.0.1;Initial Catalog=Northwind;User ID=sa;Password=111111;Persist Security Info=False;Connect Timeout=500;";
            SqlConnection con = new SqlConnection(strConnection);
            SqlDataAdapter da = new SqlDataAdapter(strSql, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Employees");
            string json = "";
            IList<Hashtable> mList = new List<Hashtable>();
            try
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Hashtable ht = new Hashtable();
                    foreach (DataColumn col in ds.Tables[0].Columns)
                    {
                        ht.Add(col.ColumnName, row[col.ColumnName]);
                    }
                    mList.Add(ht);
                }
                //json = JavaScriptConvert.SerializeObject(mList);
            }
            catch (Exception ee)
            {
                string error = ee.Message;
            }
            // int count = ds.Tables[0].Rows.Count; 
            int count = 9;
            json = "{totalCount:" + count + ",root:" + json + "}";
            Response.Write(json);
            Response.End(); 
            */
        }
    }
}
