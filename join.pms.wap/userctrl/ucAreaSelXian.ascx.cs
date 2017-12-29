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

namespace join.pms.wap.userctrl
{
    public partial class ucAreaSelXian : System.Web.UI.UserControl
    {
        private string m_SqlParams;
        private DataTable m_Dt;

        private string m_SiteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //SetAreaList(this.DropDownListXiang, m_SiteArea);
            }
        }


        protected void DropDownListXiang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListXiang.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListCun, areaCode);
            }
        }

        /// <summary>
        /// 地区选择
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="areaCode"></param>
        public void SetAreaList(DropDownList ctrl, string areaCode)
        {
            m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + areaCode + "' ORDER BY AreaCode";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            ctrl.DataSource = m_Dt;
            ctrl.DataTextField = "AreaName";
            ctrl.DataValueField = "AreaCode";
            ctrl.DataBind();
            m_Dt = null;
        }

        public string GetAreaName()
        {
            string areaName = this.DropDownListCun.SelectedItem.Text;
            if (!String.IsNullOrEmpty(areaName))
            {
                return areaName;
            }
            else
            {
                return "";
            }
        }
        public string GetAreaCode()
        {
            string areaCode = this.DropDownListCun.SelectedItem.Value;
            if (!String.IsNullOrEmpty(areaCode))
            {
                return areaCode;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取选择的最终行政区划 村/乡/县 DropDownListXiangJie DropDownListQuXian
        /// </summary>
        /// <returns></returns>
        public string GetSelAreaCode()
        {
            string areaCode = this.DropDownListCun.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                return "汉中市勉县" + this.DropDownListXiang.SelectedItem.Text + this.DropDownListCun.SelectedItem.Text;
            }
            else
            {
                //areaCode = this.DropDownListXiangJie.SelectedValue;
                return "";
            }
        }
        public string GetSelAreaName()
        {
            string areaCode = this.DropDownListCun.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                return "汉中市勉县" + this.DropDownListXiang.SelectedItem.Text + this.DropDownListCun.SelectedItem.Text;
            }
            else
            {
                //areaCode = this.DropDownListXiangJie.SelectedValue;
                return "";
            }
        }
        public void SetAreaCode(string areaCode)
        {
            SetAreaList(this.DropDownListXiang, areaCode.Substring(0, 4) + "00000000");
            SetAreaList(this.DropDownListCun, areaCode.Substring(0, 6) + "000000");
            if (!String.IsNullOrEmpty(areaCode) && areaCode != "请选择")
            {
                this.DropDownListXiang.SelectedValue = areaCode.Substring(0, 6) + "000000";
                this.DropDownListCun.SelectedValue = areaCode.Substring(0, 9) + "000";
                //this.DropDownListXiang.SelectedItem.Value = areaCode.Substring(0, 6) + "000000";
                //this.DropDownListCun.SelectedItem.Value = areaCode.Substring(0, 6) + "000";
            }
        }

        public void SetAreaCode(string areaCode, bool isEnabled)
        {
            SetAreaList(this.DropDownListXiang, areaCode.Substring(0, 4) + "00000000");
            SetAreaList(this.DropDownListCun, areaCode.Substring(0, 6) + "000000");
            if (!String.IsNullOrEmpty(areaCode) && areaCode != "请选择")
            {
                this.DropDownListXiang.SelectedValue = areaCode.Substring(0, 6) + "000000";
                this.DropDownListCun.SelectedValue = areaCode.Substring(0, 9) + "000";
                //this.DropDownListXiang.SelectedItem.Value = areaCode.Substring(0, 6) + "000000";
                //this.DropDownListCun.SelectedItem.Value = areaCode.Substring(0, 9) + "000";
            }
            if (isEnabled) { this.DropDownListXiang.Enabled = false; this.DropDownListCun.Enabled = false; }
        }
    }
}