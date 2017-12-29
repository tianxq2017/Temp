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
    public partial class ucDataAreaSelVillage : System.Web.UI.UserControl
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
                SetInoculationPointList(this.DropDownListCun, txtSelXiangCode.Value, txtSelPointCode.Value);
                SetVillageList(ddlVillage, txtSelPointCode.Value, txtSelVillageCode.Value);
            }
        }

        // 省
        protected void DropDownListSheng_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListSheng.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListDiShi, areaCode,"");
                this.txtSelectArea.Text = "";
                this.txtSelAreaCode.Value = this.DropDownListSheng.SelectedItem.Value;
            }
        }

        // 地市
        protected void DropDownListDiShi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListDiShi.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListQuXian, areaCode,"");
                this.txtSelectArea.Text = "";
                this.txtSelAreaCode.Value = this.DropDownListDiShi.SelectedItem.Value;
            }
        }

        // 区县
        protected void DropDownListQuXian_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListQuXian.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListXiang, areaCode,"");
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
                this.txtSelectArea.Text = "";
                this.txtSelAreaCode.Value = "";
                this.txtAreaCode.Value = DropDownListXiang.SelectedItem.Value;
            }
        }
        // 接种点
        protected void DropDownListCun_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListCun.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetVillageList(this.ddlVillage, areaCode, "");
                //this.txtSelectArea.Text = this.DropDownListCun.SelectedItem.Text;
                //this.txtSelAreaCode.Value = this.DropDownListCun.SelectedItem.Value;
                this.txtInoculationPoint.Text = this.DropDownListCun.SelectedItem.Text;
                this.txtInoculationPointID.Value = this.DropDownListCun.SelectedItem.Value;
            }
        }

        //接种点管辖村
        protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.ddlVillage.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                this.txtSelectArea.Text = ddlVillage.SelectedItem.Text;
                this.txtSelAreaCode.Value = this.ddlVillage.SelectedItem.Value;
            }
        }

        /// <summary>
        /// 地区选择
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="areaCode"></param>
        public void SetAreaList(DropDownList ctrl, string areaCode,string defaultVal)
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
        /// 地区选择
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="areaCode"></param>
        public void SetVillageList(DropDownList ctrl, string areaCode, string defaultVal)
        {
            m_SqlParams = "SELECT [id], [AreaName] FROM [AreaDetailCN] where id in( select id from NHS_Village WHERE InoculationPointID = '" + areaCode + "') ORDER BY AreaCode";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            ctrl.DataSource = m_Dt;
            ctrl.DataTextField = "areaName";
            ctrl.DataValueField = "id";
            ctrl.DataBind();
            ctrl.Items.Insert(0, new ListItem("请选择", "请选择"));
            if (!String.IsNullOrEmpty(defaultVal))
            {
                ctrl.SelectedValue = defaultVal;
            }
            m_Dt = null;
        }


        #region 接种点
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetInoculationPointName()
        {
            string areaName = this.txtInoculationPoint.Text;
            if (!String.IsNullOrEmpty(areaName))
            {
                return areaName;
            }
            else
            {
                return ""; 
            }
        }
        public string GetInoculationPointCode()
        {
            string areaCode= this.txtInoculationPointID.Value;
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
        public void SetInoculationPointName(string areaName)
        {
            if (!String.IsNullOrEmpty(areaName) && areaName != "请选择")
            {
                this.txtInoculationPoint.Text = areaName;
            }
        }

        public void SetInoculationPointCode(string areaCode)
        {
            if (!String.IsNullOrEmpty(areaCode) && areaCode != "请选择")
            {
                this.txtInoculationPointID.Value = areaCode;
            }
        }

        

        #endregion

        #region 村庄
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

        public void SetVillageAreaCode(string areaCode)
        {
            if (!String.IsNullOrEmpty(areaCode))
            {
                this.txtSelVillageCode.Value = areaCode;
            }
        }



        public string GetFullAreaCode()
        {
            string areaCode = this.txtAreaCode.Value;
            if (!String.IsNullOrEmpty(areaCode))
            {
                return areaCode;
            }
            else
            {
                return "";
            }
        }
        
        public void SetFullAreaCode(string areaCode)
        {
            if (!String.IsNullOrEmpty(areaCode) && areaCode != "请选择")
            {
                this.txtAreaCode.Value = areaCode;
            }
        }
        #endregion


    }
}