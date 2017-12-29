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
using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.web.userctrl
{
    public partial class UserSelect : System.Web.UI.Page
    {
        private string m_ObjID;
        private string m_SqlParams;
        private DataTable m_Dt;

        protected string m_StrDept;
        protected string m_StrName;
        protected string m_StrIDs;
        protected string m_Multiple;
        protected bool IsSelOne = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_ObjID = Request.QueryString["IDs"];
            if (Request.QueryString["SelOne"] == "1") 
            { 
                m_Multiple = "multiple";
                IsSelOne = true;
            }

            if (!IsPostBack) 
            {
                GetInitValues();
            }
        }

        private void GetInitValues() 
        {
            string deptCode = string.Empty;
            string parentCode = string.Empty;
            m_StrDept = "";
            // SELECT [DeptCode], SUBSTRING(DeptCode,0,LEN(DeptCode)-1),(CASE WHEN Len(DeptCode) = 2 THEN ''+[DeptName] ELSE '|---'+[DeptName] END) As DeptName FROM [USER_Department]
            m_SqlParams = "SELECT [DeptCode], (CASE WHEN Len(DeptCode) = 2 THEN ''+[DeptName] ELSE '|---'+[DeptName] END) As DeptName FROM [USER_Department]";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            for (int i = 0; i < m_Dt.Rows.Count;i++ )
            {
                deptCode = m_Dt.Rows[i][0].ToString();
                parentCode = deptCode.Substring(0, deptCode.Length - 2);
                if (String.IsNullOrEmpty(parentCode)) parentCode = "0";
                m_StrDept += deptCode + "," + parentCode + "," + m_Dt.Rows[i][1].ToString() + ",";
            }
            if (!String.IsNullOrEmpty(m_StrDept)) m_StrDept = m_StrDept.Substring(0, m_StrDept.Length-1);
            m_Dt = null;
            // ÓÃ»§
            m_StrName = "";
            m_SqlParams = "SELECT [UserID],[DeptCode],UserName,(CASE LEN(DeptCode) WHEN 2 THEN '|--'+UserName WHEN 4 THEN '|--+--'+UserName WHEN 6 THEN '|--+--+--'+UserName ELSE '-' END) AS UserName  FROM [USER_BaseInfo] WHERE ValidFlag=1 ";
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            for (int j = 0; j < m_Dt.Rows.Count; j++)
            {
                m_StrName += m_Dt.Rows[j][0].ToString() + "," + m_Dt.Rows[j][1].ToString() + "," + m_Dt.Rows[j][2].ToString() + ",";
            }
            if (!String.IsNullOrEmpty(m_StrName)) m_StrName = m_StrName.Substring(0, m_StrName.Length - 1);
            m_Dt = null;
            m_StrIDs = m_ObjID;
        }

    }
}
