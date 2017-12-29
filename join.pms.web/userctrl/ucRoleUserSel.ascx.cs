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
    public partial class ucRoleUserSel : System.Web.UI.UserControl
    {
        private string m_SqlParams;
        private DataTable m_Dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtSelectArea.ReadOnly = true;
            if (!IsPostBack)
            {
                SetRoleList(this.DropDownListSheng, txtSelRoleID.Value);
                SetUserList(this.DropDownListDiShi, txtSelRoleID.Value, txtSelAreaCode.Value);
            }
        }

        // 角色
        protected void DropDownListSheng_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListSheng.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetUserList(this.DropDownListDiShi, areaCode, "");
                this.txtSelectArea.Text = "";
                this.txtSelAreaCode.Value = this.DropDownListSheng.SelectedItem.Value;
            }
        }

        // 用户
        protected void DropDownListDiShi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListDiShi.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                this.txtSelectArea.Text = this.DropDownListDiShi.SelectedItem.Text;
                this.txtSelAreaCode.Value = this.DropDownListDiShi.SelectedItem.Value;
                BindUser();
            }
        }

        private void BindUser()
        {
            m_SqlParams = "SELECT [UserID], [UserAccount],UserTel,UserUnitName FROM [USER_BaseInfo] WHERE userID = " + this.DropDownListDiShi.SelectedItem.Value + " ORDER BY UserID";
            DataTable dt = new DataTable();
            dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                litAccount.Text = dt.Rows[0]["UserAccount"].ToString();
                this.litContactTel.Text = dt.Rows[0]["UserTel"].ToString();
                this.litAddress.Text = dt.Rows[0]["UserUnitName"].ToString();
            }
        }

        /// <summary>
        /// 角色选择
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="areaCode"></param>
        public void SetRoleList(DropDownList ctrl, string defaultVal)
        {
            m_SqlParams = "SELECT [RoleID], [RoleName] FROM [SYS_Roles] WHERE RoleName like  '%预防接种%' ORDER BY RoleID";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            ctrl.DataSource = m_Dt;
            ctrl.DataTextField = "RoleName";
            ctrl.DataValueField = "RoleID";
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
            m_SqlParams = "SELECT b.[UserID], [UserName] FROM [SYS_UserRoles] a join USER_BaseInfo b on a.UserID=b.UserID  WHERE roleID = '" + RoleID + "' ORDER BY b.UserID";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            ctrl.DataSource = m_Dt;
            ctrl.DataTextField = "UserName";
            ctrl.DataValueField = "UserID";
            ctrl.DataBind();
            ctrl.Items.Insert(0, new ListItem("请选择", "请选择"));
            if (!String.IsNullOrEmpty(defaultVal))
            {
                ctrl.SelectedValue = defaultVal;
                BindUser();
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

        // txtSelAreaCode
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

        // txtSelAreaCode
        public void SetRoleCode(string areaCode)
        {
            if (!String.IsNullOrEmpty(areaCode))
            {
                this.txtSelRoleID.Value = areaCode;
            }
        }


    }
}