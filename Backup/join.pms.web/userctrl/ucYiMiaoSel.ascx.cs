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
    public partial class ucYiMiaoSel : System.Web.UI.UserControl
    {
        private string m_SqlParams;
        private DataTable m_Dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetRoleList(this.DropDownListSheng, this.txtDrpBacClass.Text);
                SetUserList(this.DropDownListDiShi, this.txtDrpBacClass.Text, this.txtDrpBacClass1.Text);
            }
        }

        // 角色
        protected void DropDownListSheng_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListSheng.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetUserList(this.DropDownListDiShi, areaCode, "");
                this.txtDrpBacClass.Text = areaCode;
                this.txtDrpBacName.Text = this.DropDownListSheng.SelectedItem.Text;
            }
        }

        // 用户
        protected void DropDownListDiShi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListDiShi.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                this.txtDrpBacName1.Text = this.DropDownListDiShi.SelectedItem.Text;
                this.txtDrpBacClass1.Text = this.DropDownListDiShi.SelectedItem.Value;
            }
        }

        /// <summary>
        /// 角色选择
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="areaCode"></param>
        public void SetRoleList(DropDownList ctrl, string defaultVal)
        {
            m_SqlParams = "SELECT [DrpBacClass],[DrpBacClassBig],[DrpBacName]  FROM [NHS_YiMiao] Where [IsDel]=0 ORDER BY Commid";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            ctrl.DataSource = m_Dt;
            ctrl.DataTextField = "DrpBacName";
            ctrl.DataValueField = "DrpBacClass";
            ctrl.DataBind();
            ctrl.Items.Insert(0, new ListItem("请选择", "请选择"));
            if (!String.IsNullOrEmpty(defaultVal))
            {
                ctrl.SelectedValue = defaultVal;
            }
            m_Dt = null;
        }

        /// <summary>
        /// 用户选择
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="areaCode"></param>
        public void SetUserList(DropDownList ctrl, string RoleID, string defaultVal)
        {
            m_SqlParams = "SELECT [DrpBacClass],[DrpBacName]  FROM [dbo].[NHS_YiMiao] Where [IsDel]=0 and DrpBacClassBig = '" + RoleID + "' ORDER BY Commid";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            ctrl.DataSource = m_Dt;
            ctrl.DataTextField = "DrpBacName";
            ctrl.DataValueField = "DrpBacClass";
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
            string areaName = this.txtDrpBacName.Text;
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
            string areaCode = this.txtDrpBacClass.Text;
            if (!String.IsNullOrEmpty(areaCode))
            {
                return areaCode;
            }
            else
            {
                return "";
            }
        }
        public string GetAreaName1()
        {
            string areaName = this.txtDrpBacName1.Text;
            if (!String.IsNullOrEmpty(areaName))
            {
                return areaName;
            }
            else
            {
                return "";
            }
        }

        public string GetAreaCode1()
        {
            string areaCode = this.txtDrpBacClass1.Text;
            if (!String.IsNullOrEmpty(areaCode))
            {
                return areaCode;
            }
            else
            {
                return "";
            }
        }
        // txtSelAreaCode
        public void SetAreaName(string areaName)
        {
            if (!String.IsNullOrEmpty(areaName) && areaName != "请选择")
            {
                this.txtDrpBacName.Text = areaName;
            }
        }

        public void SetAreaCode(string areaCode)
        {
            if (!String.IsNullOrEmpty(areaCode) && areaCode != "请选择")
            {
                this.txtDrpBacClass.Text = areaCode;
                this.DropDownListSheng.SelectedValue = areaCode;
            }
        }
        public void SetAreaName1(string areaName)
        {
            if (!String.IsNullOrEmpty(areaName) && areaName != "请选择")
            {
                this.txtDrpBacName1.Text = areaName;
            }
        }

        public void SetAreaCode1(string areaCode)
        {
            if (!String.IsNullOrEmpty(areaCode) && areaCode != "请选择")
            {
                this.txtDrpBacClass1.Text = areaCode;
                this.DropDownListDiShi.SelectedValue = areaCode;
            }
        }
    }
}