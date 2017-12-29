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
using System.Data.SqlClient;
using UNV.Comm.DataBase;

namespace join.pms.web.AreaEdit
{
    public partial class AreaQuList : System.Web.UI.Page
    {
        private string m_UserID; // 当前登录的操作用户编号
        private string m_UserName;
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            if (!IsPostBack)
            {
                string roleID = join.pms.dal.CommPage.GetSingleVal("SELECT [RoleID] FROM [SYS_UserRoles] WHERE UserID=" + this.m_UserID);
                if (String.IsNullOrEmpty(roleID) || roleID != "1")
                {
                    Response.Write("<script language='javascript'>alert('操作失败：您不具有业务操作的权限！');parent.location.href='/MainFrame.aspx';</script>");
                    Response.End();
                }
                SetAreaList(this.DropDownListSheng, "000000000000", ""); // 省
                SetAreaList(this.DropDownListDiShi, "", ""); // 地市
                SetAreaList(this.DropDownListQuXian, "", ""); //区县
                SetAreaList(this.DropDownListXiang, "", ""); //乡镇
                SetAreaList(this.DropDownListCun, "", ""); //乡镇
                getDataTable();

                btnSave.Enabled = false;

            }
        }

        #region
        /// <summary>
        /// 身份验证
        /// </summary>
        private void AuthenticateUser()
        {
            bool returnVa = false;
            if (Request.Browser.Cookies)
            {
                HttpCookie loginCookie = Request.Cookies["AREWEB_OC_USER_YSL"];
                if (loginCookie != null && !String.IsNullOrEmpty(loginCookie.Values["UserID"].ToString())) { returnVa = true; m_UserID = loginCookie.Values["UserID"].ToString(); }
            }
            else
            {
                if (Session["AREWEB_OC_USERID"] != null && !String.IsNullOrEmpty(Session["AREWEB_OC_USERID"].ToString())) { returnVa = true; m_UserID = Session["AREWEB_OC_USERID"].ToString(); }
            }

            if (!returnVa)
            {
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }
        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        private void GetUserInfo()
        {
            SqlDataReader sdr = null;
            try
            {
                string sqlParams = "SELECT UserAccount,UserName,UserAreaCode FROM USER_BaseInfo WHERE UserID=" + m_UserID;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        m_UserName = sdr["UserAccount"].ToString() + "(" + sdr["UserName"].ToString() + ")";
                        //m_AreaCode = sdr["UserAreaCode"].ToString();
                        //m_AreaName = GetAreaName(m_AreaCode, "0");
                    }
                }
            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }
        }

       

        

        #endregion



        // 省
        protected void DropDownListSheng_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListSheng.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListDiShi, areaCode, "");
            }

            DropDownListQuXian.SelectedValue = "";
            DropDownListXiang.SelectedValue = "";
            DropDownListDiShi.SelectedValue = "";
            DropDownListCun.SelectedValue = "";
        }

        // 地市
        protected void DropDownListDiShi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListDiShi.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListQuXian, areaCode, "");
            }
        }

        // 区县
        protected void DropDownListQuXian_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListQuXian.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListXiang, areaCode, "");
            }
        }

        // 乡镇
        protected void DropDownListXiang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListXiang.SelectedValue;
            if (!String.IsNullOrEmpty(areaCode))
            {
                SetAreaList(this.DropDownListCun, areaCode, "");
            }
        }
        // 社区/村
        protected void DropDownListCun_SelectedIndexChanged(object sender, EventArgs e)
        {
            string areaCode = this.DropDownListCun.SelectedValue;

        }

        // <summary>
        /// 地区选择
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="areaCode"></param>
        public void SetAreaList(DropDownList ddl, string areaCode, string defaultVal)
        {

            string m_SqlParams = "SELECT [AreaCode], [AreaName] FROM [AreaDetailCN] WHERE ParentCode = '" + areaCode + "' and ParentCode!='' ORDER BY AreaCode";
            DataTable m_Dt = new DataTable();
            m_Dt = UNV.Comm.DataBase.DbHelperSQL.Query(m_SqlParams).Tables[0];
            ddl.DataSource = m_Dt;
            ddl.DataTextField = "AreaName";
            ddl.DataValueField = "AreaCode";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("请选择", ""));
            if (!String.IsNullOrEmpty(defaultVal))
            {
                ddl.SelectedValue = defaultVal;
            }
            m_Dt = null;
        }





        private void getDataTable()
        {
            string val = "";
            if (DropDownListCun.SelectedValue != "")
            {
                val = DropDownListCun.SelectedValue;
            }
            else if (DropDownListXiang.SelectedValue != "")
            {
                val = DropDownListXiang.SelectedValue;
            }
            else if (DropDownListQuXian.SelectedValue != "")
            {
                val = DropDownListQuXian.SelectedValue;
            }
            else if (DropDownListDiShi.SelectedValue != "")
            {
                val = DropDownListDiShi.SelectedValue;
            }
            else if (this.DropDownListSheng.SelectedValue != "")
            {
                val = DropDownListSheng.SelectedValue;
            }



            using (SqlConnection con = new SqlConnection(UNV.Comm.DataBase.PubConstant.ConnectionString))
            {
                SqlDataAdapter sda = new SqlDataAdapter("select * from AreaDetailCN where attribs=1 and  parentcode='" + val + "' and ParentCode!='' order by areaCode desc", con);
                DataSet ds = new DataSet();
                sda.Fill(ds, "AreaDetailCN");
                DataTable dt = new DataTable();
                if (ds.Tables.Contains("AreaDetailCN"))
                {
                    dt = ds.Tables["AreaDetailCN"];
                }

                ViewState["dt"] = dt;
                gv.DataSource = dt;
                gv.DataBind();


            }//end using block    
        }

        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string val = "";
            if (DropDownListCun.SelectedValue != "")
            {
                val = DropDownListCun.SelectedValue;
            }
            else if (DropDownListXiang.SelectedValue != "")
            {
                val = DropDownListXiang.SelectedValue;
            }
            else if (DropDownListQuXian.SelectedValue != "")
            {
                val = DropDownListQuXian.SelectedValue;
            }
            else if (DropDownListDiShi.SelectedValue != "")
            {
                val = DropDownListDiShi.SelectedValue;
            }
            else if (this.DropDownListSheng.SelectedValue != "")
            {
                val = DropDownListSheng.SelectedValue;
            }
            int iRowIndex = e.RowIndex;

            DataTable dt = ViewState["dt"] as DataTable;
            if (dt != null)
            {
                dt.Rows[iRowIndex]["Attribs"] = "4";
                ViewState["dt"] = dt;
            }

            dt = ViewState["dt"] as DataTable;
            if (dt != null)
            {
                using (SqlConnection con = new SqlConnection(UNV.Comm.DataBase.PubConstant.ConnectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter("select * from AreaDetailCN where parentcode='" + val + "'", con);
                    SqlCommandBuilder scb = new SqlCommandBuilder(sda);
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "AreaDetailCN");
                    sda.Update(dt);
                    ClientScript.RegisterStartupScript(GetType(), "success", "alert('删除成功!');", true);
                }//end using block                
            }
            getDataTable();
            //object val = gv.DataKeys[e.RowIndex].Value;
            //DataTable dt = ViewState["dt"] as DataTable;
            //if (dt != null)
            //{
            //    for (int i = dt.Rows.Count - 1; i > -1; i--)
            //    {
            //        if (dt.Rows[i][0].Equals(val))
            //        {
            //            dt.Rows[i].Delete();
            //            //dt.Rows.RemoveAt(i);                    
            //        }
            //    }
            //    ViewState["dt"] = dt;
            //    gv.DataSource = dt;
            //    gv.DataBind();
            //    //Response.Write("<script language='JavaScript'>window.location=location;</script>");  //防止刷新重复提交

            //}
        }
        protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv.EditIndex = e.NewEditIndex;
            DataTable dt = ViewState["dt"] as DataTable;
            if (dt != null)
            {
                gv.DataSource = dt;
                gv.DataBind();
            }
            btnSave.Enabled = false;
        }

        protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int iRowIndex = e.RowIndex;

            DataTable dt = ViewState["dt"] as DataTable;
            if (dt != null)
            {
                dt.Rows[iRowIndex]["AreaName"] = ((sender as GridView).Rows[iRowIndex].FindControl("txtName") as TextBox).Text;
                dt.Rows[iRowIndex]["AreaCode"] = ((sender as GridView).Rows[iRowIndex].FindControl("txtCode") as TextBox).Text;
                dt.Rows[iRowIndex]["AreaNameFull"] = ((sender as GridView).Rows[iRowIndex].FindControl("txtFullName") as TextBox).Text;
                dt.Rows[iRowIndex]["AreaCodeFull"] = ((sender as GridView).Rows[iRowIndex].FindControl("txtFullCode") as TextBox).Text;
                ViewState["dt"] = dt;
                (sender as GridView).EditIndex = -1;
                gv.DataSource = dt;
                gv.DataBind();
            }
            btnSave.Enabled = true;
        }
        protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            gv.EditIndex = -1;
            DataTable dt = ViewState["dt"] as DataTable;
            if (dt != null)
            {
                gv.DataSource = dt;
                gv.DataBind();
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.txtCode.Text.Trim() != "" && this.txtName.Text.Trim() != "")
            {
                DataTable dt = ViewState["dt"] as DataTable;
                if (dt != null)
                {
                    using (SqlConnection con = new SqlConnection(UNV.Comm.DataBase.PubConstant.ConnectionString))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter("select MAX(ID)+1 from AreaDetailCN ", con);
                        DataSet ds = new DataSet();
                        sda.Fill(ds, "MaxID");
                        if (ds.Tables.Contains("MaxID") && ds.Tables["MaxID"].Rows.Count > 0)
                        {
                            string fullName = "";
                            string parentCode = "";
                            if (this.DropDownListSheng.SelectedValue != "")
                            {
                                fullName += DropDownListSheng.SelectedItem.Text;
                                parentCode = "000000000000";
                            }
                            if (DropDownListDiShi.SelectedValue != "")
                            {
                                fullName += DropDownListDiShi.SelectedItem.Text;
                                parentCode = DropDownListDiShi.SelectedValue;
                            }
                            if (DropDownListQuXian.SelectedValue != "")
                            {
                                fullName += DropDownListQuXian.SelectedItem.Text;
                                parentCode = DropDownListQuXian.SelectedValue;
                            }
                            if (DropDownListXiang.SelectedValue != "")
                            {
                                fullName += DropDownListXiang.SelectedItem.Text;
                                parentCode = DropDownListXiang.SelectedValue;
                            }

                            DataRow dr = dt.NewRow();
                            
                            dr[0] = txtName.Text.Trim();
                            dr[1] = txtCode.Text;
                            dr[2] = parentCode;
                            dr[3] = fullName + txtName.Text.Trim();                            
                            dr[4] = txtCode.Text + "000";
                            dr[5] = 1;
                            dr[6] = ds.Tables["MaxID"].Rows[0][0];
                            dt.Rows.Add(dr);
                            ViewState["dt"] = dt;
                            gv.DataSource = dt;
                            gv.DataBind();
                        }
                    }//end using block                
                }
                btnSave.Enabled = true;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string val = "";
            if (DropDownListXiang.SelectedValue != "")
            {
                val = DropDownListXiang.SelectedValue;
            }
            else if (DropDownListQuXian.SelectedValue != "")
            {
                val = DropDownListQuXian.SelectedValue;
            }
            else if (DropDownListDiShi.SelectedValue != "")
            {
                val = DropDownListDiShi.SelectedValue;
            }
            else if (this.DropDownListSheng.SelectedValue != "")
            {
                val = DropDownListSheng.SelectedValue;
            }
            DataTable dt = ViewState["dt"] as DataTable;
            if (dt != null)
            {
                using (SqlConnection con = new SqlConnection(UNV.Comm.DataBase.PubConstant.ConnectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter("select * from AreaDetailCN where parentcode='" + val + "'", con);
                    SqlCommandBuilder scb = new SqlCommandBuilder(sda);
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "AreaDetailCN");
                    sda.Update(dt);
                    ClientScript.RegisterStartupScript(GetType(), "success", "alert('保存成功!');", true);
                }//end using block                
            }
            getDataTable();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getDataTable();
        }
    }
}
