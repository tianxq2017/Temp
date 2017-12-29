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
    public partial class ucDataAreaSelShort : System.Web.UI.UserControl
    {
        private string m_SqlParams;
        private DataTable m_Dt;

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    this.txtSelectArea.ReadOnly = true;
        //    if (!IsPostBack)
        //    {
        //        SetAreaList(this.DropDownListSheng, "000000000000", "610000000000"); // 省
        //        SetAreaList(this.DropDownListDiShi, "610000000000", "610900000000"); // 地市
        //        SetAreaList(this.DropDownListQuXian, "610900000000", "610922000000"); //区县
        //        SetAreaList(this.DropDownListXiang, "610922000000", ""); //乡镇
        //    }
        //}

        private string m_SiteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtSelectArea.ReadOnly = true;
            if (!IsPostBack)
            {
                SetAreaList(this.DropDownListSheng, "000000000000", m_SiteArea.Substring(0, 2) + "0000000000"); // 省
                SetAreaList(this.DropDownListDiShi, m_SiteArea.Substring(0, 2) + "0000000000", m_SiteArea); // 地市
                SetAreaList(this.DropDownListQuXian, m_SiteArea.Substring(0, 4) + "00000000", txtSelQuCode.Value); //区县
                SetAreaList(this.DropDownListXiang, txtSelQuCode.Value, txtSelXiangCode.Value); //乡镇
                SetInoculationPointList(DropDownListCun, txtSelXiangCode.Value, txtSelPointCode.Value);
                this.SetCunList(this.CheckBoxList_Name, txtSelXiangCode.Value, txtSelIDs.Value);
            }
        }

        // 省
        protected void DropDownListSheng_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListSheng.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListDiShi, areaCode, "");
                this.txtSelectArea.Text = "";
                this.txtSelAreaCode.Value = "";
            }
        }

        // 地市
        protected void DropDownListDiShi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListDiShi.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListQuXian, areaCode, "");
                this.txtSelectArea.Text = "";
                this.txtSelAreaCode.Value = "";
            }
        }

        // 区县
        protected void DropDownListQuXian_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListQuXian.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListXiang, areaCode, "");
                this.txtSelectArea.Text = "";
                this.txtSelAreaCode.Value = "";
            }
        }


        // 乡镇
        protected void DropDownListXiang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListXiang.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetInoculationPointList(this.DropDownListCun, areaCode, "");
                this.SetCunList(this.CheckBoxList_Name, areaCode, "");
                this.txtSelectArea.Text = "";
                this.txtSelAreaCode.Value = "";
                
            }
        }
        // 社区/村
        protected void DropDownListCun_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListCun.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                this.txtSelectArea.Text = this.DropDownListCun.SelectedItem.Text;
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
        /// 地区选择
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="areaCode"></param>
        public void SetInoculationPointList(DropDownList ctrl, string areaCode, string defaultVal)
        {
            m_SqlParams = "SELECT [Commid], [Name] FROM [NHS_Child_InoculationPoint] WHERE areaCode = '" + areaCode + "' ORDER BY AreaCode";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            ctrl.DataSource = m_Dt;
            ctrl.DataTextField = "Name";
            ctrl.DataValueField = "Commid";
            ctrl.DataBind();
            ctrl.Items.Insert(0, new ListItem("请选择", "请选择"));
            if (!String.IsNullOrEmpty(defaultVal))
            {
                ctrl.SelectedValue = defaultVal;
            }
            m_Dt = null;
        }

        /// <summary>
        /// 地区点位
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="areaCode"></param>
        public void SetCunList(CheckBoxList ctrl, string areaCode, string defaultVal)
        {
            m_SqlParams = "SELECT [id], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + areaCode+"' ORDER BY id";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            ctrl.DataSource = m_Dt;
            ctrl.DataTextField = "AreaName";
            ctrl.DataValueField = "id";
            ctrl.DataBind();
            if (!String.IsNullOrEmpty(defaultVal))
            {
                SetChkList(defaultVal);
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

        public void SetQuAreaCode(string areaCode)
        {
            if (!String.IsNullOrEmpty(areaCode))
            {
                this.txtSelQuCode.Value = areaCode;
            }
        }

        public void SetXiangAreaCode(string areaCode)
        {
            if (!String.IsNullOrEmpty(areaCode))
            {
                this.txtSelXiangCode.Value = areaCode;
            }
        }

        public void SetPointAreaCode(string areaCode)
        {
            if (!String.IsNullOrEmpty(areaCode))
            {
                this.txtSelPointCode.Value = areaCode;
            }
        }

        public void SetIDs(string areaCode)
        {
            if (!String.IsNullOrEmpty(areaCode))
            {
                this.txtSelIDs.Value = areaCode;
            }
        }

        public string GetChkList()
        {
            string areaCode = "";

            for (int i = 0; i < CheckBoxList_Name.Items.Count; i++)
            {
                if (CheckBoxList_Name.Items[i].Selected)
                {
                    areaCode += CheckBoxList_Name.Items[i].Value + ",";
                }
            }
            if (!String.IsNullOrEmpty(areaCode))
            {
                return areaCode.TrimEnd(',');
            }
            else
            {
                return "";
            }
        }
        
        public void SetChkList(string areaCode)
        {
            for (int i = 0; i < areaCode.Split(',').Length; i++)
            {
                for (int j = 0; j < CheckBoxList_Name.Items.Count; j++)
                {
                    //值与CheckBok的Text相等时选择
                    if (areaCode.Split(',')[i] == CheckBoxList_Name.Items[j].Value)
                    {
                        this.CheckBoxList_Name.Items[j].Selected = true;
                    }
                }
            }
        }


        
    }
}