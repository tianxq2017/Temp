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
    public partial class Uc_AreaDetailSelect : System.Web.UI.UserControl
    {
        private string m_SqlParams;
        private DataTable m_Dt;
        private string m_FuncCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetJieBanList();
                SetSheQuList("150524000000", "");
            }
        }

        /// <summary>
        /// 设置街道办信息 150524000000	宁陕县
        /// </summary>
        public void SetJieBanList()
        {
            m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '150524000000' ORDER BY AreaCode";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            this.DropDownListState.DataSource = m_Dt;
            this.DropDownListState.DataTextField = "AreaName";
            this.DropDownListState.DataValueField = "AreaCode";
            this.DropDownListState.DataBind();
            m_Dt = null;
        }
        /// <summary>
        /// 设置城市区划信息
        /// </summary>
        /// <param name="stateAreaCode"></param>
        public void SetSheQuList(string areaCode, string defaultValue)
        {
            m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + areaCode + "' ORDER BY AreaCode";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            this.DropDownListCity.DataSource = m_Dt;
            this.DropDownListCity.DataTextField = "AreaName";
            this.DropDownListCity.DataValueField = "AreaCode";
            this.DropDownListCity.DataBind();
            if (!String.IsNullOrEmpty(defaultValue) && !String.IsNullOrEmpty(defaultValue))
            {
                this.DropDownListCity.SelectedValue = defaultValue;
                this.DropDownListState.SelectedValue = areaCode;
            }
            m_Dt = null;
        }

        protected void DropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListState.SelectedValue;

            if (!String.IsNullOrEmpty(areaCode))
            {
                SetSheQuList(areaCode, "");
            }
        }

        /// <summary>
        /// 获取选择的地市
        /// </summary>
        /// <returns></returns>
        public string GetSelAreaCode()
        {
            return this.DropDownListCity.SelectedValue;
        }
        
    }
}