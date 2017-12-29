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
    public partial class ucAreaSel : System.Web.UI.UserControl
    {
        private string m_SqlParams;
        private DataTable m_Dt;

        private string m_SiteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtSelectArea.ReadOnly = true;
            if (!IsPostBack)
            {
                SetAreaList(this.DropDownListSheng, "000000000000", m_SiteArea.Substring(0, 2) + "0000000000"); // 省
                SetAreaList(this.DropDownListDiShi, m_SiteArea.Substring(0, 2) + "0000000000", m_SiteArea); // 地市
                SetAreaList(this.DropDownListQuXian, m_SiteArea, ""); //区县
                SetAreaList(this.DropDownListXiang, "", ""); //乡镇
            }
        }

        // 省
        protected void DropDownListSheng_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListSheng.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListDiShi, areaCode, "");
                this.txtSelectArea.Text = this.DropDownListSheng.SelectedItem.Text;
                this.txtSelAreaCode.Value = this.DropDownListSheng.SelectedItem.Value;
            }
        }

        // 地市
        protected void DropDownListDiShi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListDiShi.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListQuXian, areaCode, "");
                this.txtSelectArea.Text = this.DropDownListSheng.SelectedItem.Text + this.DropDownListDiShi.SelectedItem.Text;
                this.txtSelAreaCode.Value = this.DropDownListDiShi.SelectedItem.Value;
            }
        }

        // 区县
        protected void DropDownListQuXian_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListQuXian.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListXiang, areaCode, "");
                this.txtSelectArea.Text = this.DropDownListSheng.SelectedItem.Text + this.DropDownListDiShi.SelectedItem.Text + this.DropDownListQuXian.SelectedItem.Text;
                this.txtSelAreaCode.Value = this.DropDownListQuXian.SelectedItem.Value;
            }
        }

        // 乡镇
        protected void DropDownListXiang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListXiang.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListCun, areaCode, "");
                this.txtSelectArea.Text = this.DropDownListSheng.SelectedItem.Text + this.DropDownListDiShi.SelectedItem.Text + this.DropDownListQuXian.SelectedItem.Text + this.DropDownListXiang.SelectedItem.Text;
                this.txtSelAreaCode.Value = this.DropDownListXiang.SelectedItem.Value;
            }
        }
        // 社区/村
        protected void DropDownListCun_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListCun.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                this.txtSelectArea.Text = this.DropDownListSheng.SelectedItem.Text + this.DropDownListDiShi.SelectedItem.Text + this.DropDownListQuXian.SelectedItem.Text + this.DropDownListXiang.SelectedItem.Text + this.DropDownListCun.SelectedItem.Text;
                this.txtSelAreaCode.Value = this.DropDownListCun.SelectedItem.Value;
            }
        }
        /// <summary>
        /// 地区选择
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="areaCode"></param>
        public void SetAreaList(DropDownList ctrl, string areaCode, string defaultVal)
        {
            m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + areaCode + "' ORDER BY AreaCode";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            ctrl.DataSource = m_Dt;
            ctrl.DataTextField = "AreaName";
            ctrl.DataValueField = "AreaCode";
            ctrl.DataBind();
            ctrl.Items.Insert(0, new ListItem("请选择", "请选择"));
            if (!String.IsNullOrEmpty(defaultVal))
            {
                ctrl.SelectedValue = defaultVal;
            }
            m_Dt = null;
        }

        /// <summary>
        /// 获取选择的最终行政区划 村/乡/县 DropDownListXiangJie DropDownListQuXian
        /// </summary>
        /// <returns></returns>
        public string GetAreaName()
        {
            string areaName = this.txtSelectArea.Text;
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
            string areaCode = this.txtSelAreaCode.Value;
            if (!String.IsNullOrEmpty(areaCode))
            {
                return areaCode;
            }
            else
            {
                return "";
            }
        }

        public void SetAreaName(string areaName)
        {
            if (!String.IsNullOrEmpty(areaName) && areaName != "请选择")
            {
                this.txtSelectArea.Text = areaName;
            }
        }

        public void SetAreaCode(string areaCode)
        {
            if (!String.IsNullOrEmpty(areaCode) && areaCode != "请选择")
            {
                this.txtSelAreaCode.Value = areaCode;
            }
        }


    }
}