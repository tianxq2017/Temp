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
    public partial class ucDataYiMiaoInoculation : System.Web.UI.UserControl
    {
        private string m_SqlParams;
        private DataTable m_Dt;

        private string m_SiteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetYiMiaoPHList(this.ddlPiHao, "", "");
            }
        }

        protected void ddlPiHao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlPiHao.SelectedValue))
            {

            }
        }


        private void BindYiMiao()
        {
            m_SqlParams = @"SELECT [DrpBacZhushe], [DrpBacFangShi],Company FROM NHS_YiMiaoInfo WHERE DrpBacPHID = '" + this.ddlPiHao.SelectedItem.Value + "' AND  DrpBacName='"+this.txtDrpBacName.Value+"'  ";
            DataTable dt = new DataTable();
            dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                litCompany.Text = dt.Rows[0]["Company"].ToString();
                this.litDrpBacZhushe.Text = dt.Rows[0]["DrpBacZhushe"].ToString();
                this.litDrpBacFangShi.Text = dt.Rows[0]["DrpBacFangShi"].ToString();
            }
        }


        /// <summary>
        /// 疫苗批号
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="areaCode"></param>
        public void SetYiMiaoPHList(DropDownList ctrl, string DrpBacName, string defaultVal)
        {
            m_SqlParams = "SELECT [DrpBacPHID], [DrpBacPHID]+Company+DrpBacName1 as DrpBacDisplay FROM [NHS_YiMiaoInfo] WHERE DrpBacName = '" + DrpBacName + "' ORDER BY ShiXiaoDate";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            ctrl.DataSource = m_Dt;
            ctrl.DataTextField = "DrpBacDisplay";
            ctrl.DataValueField = "DrpBacPHID";
            ctrl.DataBind();
            ctrl.Items.Insert(0, new ListItem("请选择疫苗批号", ""));
            if (!String.IsNullOrEmpty(defaultVal))
            {
                ctrl.SelectedValue = defaultVal;
            }
            m_Dt = null;
        }

        /// <summary>
        /// 获取批号
        /// </summary>
        /// <returns></returns>
        public string GetYiMiaoPH()
        {
            string areaName = this.ddlPiHao.SelectedItem.Text;
            if (!String.IsNullOrEmpty(areaName))
            {
                return areaName;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取接种部位
        /// </summary>
        /// <returns></returns>
        public string GetInoculationPart()
        {
            string areaCode = this.ddlInoculationPart.SelectedItem.Text;
            if (!String.IsNullOrEmpty(areaCode))
            {
                return areaCode;
            }
            else
            {
                return "";
            }
        }        
    }
}