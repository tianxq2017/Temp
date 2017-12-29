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
using System.Text;

namespace join.pms.web.SysAdmin
{
    public partial class SysRolesPower : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // 当前登录的操作用户编号

        private string m_SqlParams;
        private DataTable m_PowerDt;
        protected string m_TargetUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                SetOpratetionAction("角色权限分配");
                InitializeFuncTable();
            }
        }

        #region 身份验证及参数过滤

        private void ValidateParams()
        {
            m_ActionName = PageValidate.GetFilterSQL(Request.QueryString["action"]);
            m_SourceUrl = PageValidate.GetFilterSQL(Request.QueryString["sourceUrl"]);
            m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["k"]);

            if (!string.IsNullOrEmpty(m_SourceUrl) && !string.IsNullOrEmpty(m_ActionName))
            {
                m_SourceUrlDec = DESEncrypt.Decrypt(m_SourceUrl);
                m_TargetUrl = "../UnvCommList.aspx?" + m_SourceUrlDec;
                //m_FuncCode = StringProcess.AnalysisParas(m_SourceUrlDec, "FuncCode");
            }
            else
            {
                Response.Write("非法访问，操作被终止！");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/loginTemp.aspx';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 设置操作行为
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("非法访问：操作被终止！");
                    Response.End();
                }
            }
            else
            {
                if (!PageValidate.IsNumber(m_ObjID))
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作提示：考虑到系统安全，分配焦色权限操作每次只能选择一个焦色，不可以多选！", m_TargetUrl, true); 
                    return;
                }
            }
            switch (m_ActionName)
            {
                case "add": // 新增
                    funcName = "新增";
                    break;
                case "edit": // 编辑
                    funcName = "修改";
                    //ShowModInfo(m_ObjID);
                    break;
                case "del": // 删除
                    funcName = "删除内容";
                    //DelInfo(m_ObjID);
                    break;
                case "view": // 查看
                    funcName = "查看内容";
                    break;
                case "5": // 审核
                    funcName = "审核内容";
                    break;
                case "6": // 推荐
                    funcName = "分配权限";
                    //recommend(m_ObjID);
                    // Response.Redirect(txtUrlParams.Value);
                    break;
                default:
                     MessageBox.ShowAndRedirect(this.Page, "操作提示：参数错误！", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">管理首页</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "：";
            this.LiteralRoleName.Text = "当前角色对象："+DbHelperSQL.GetSingle("SELECT RoleName FROM SYS_Roles WHERE RoleID=" + m_ObjID).ToString();
        }

        #endregion

        #region 初始化权限
        /// <summary>
        /// 初始化功能列表
        /// </summary>
        private void InitializeFuncTable()
        {
            string FuncCode = string.Empty;
            string FuncUrl = string.Empty;
            string FuncName = string.Empty;
            string bgColor = string.Empty;
            bool hasPower = false;
            string funcPowers = string.Empty;
            string[] aryConfig = null;
            string[] aryPowers = null;
            StringBuilder sHtml = new StringBuilder();
            m_SqlParams = "SELECT FuncCode,FuncURL,(CASE LEN(FuncCode) WHEN 2 THEN '|--'+FuncName WHEN 4 THEN '|--+--'+FuncName WHEN 6 THEN '|--+--+--'+FuncName ELSE '-' END) AS FuncName FROM SYS_Function WHERE FuncStatus=0 ORDER BY FuncCode";
            m_PowerDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            if (m_PowerDt.Rows.Count > 0)
            {
                GetConfigData(); // 获取配置文件数据
                sHtml.Append("<table width=\"960\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\"><tr bgcolor=\"#D7F2E3\" class=\"fb01\">");
                sHtml.Append("<td height=\"30\"><input name=\"selectAll\" type=\"checkbox\" onclick=\"SelectAllFunc()\" /></td>");
                sHtml.Append("<td width=\"250\"><strong>模块名称/编码</strong></td><td><input name=\"selectAll0\" type=\"checkbox\" onclick=\"SelectColsFunc('0')\" /><strong>全选</strong></td><td><input name=\"selectAll1\" type=\"checkbox\" onclick=\"SelectColsFunc('1')\" /><strong>全选</strong></td><td><input name=\"selectAll2\" type=\"checkbox\" onclick=\"SelectColsFunc('2')\" /><strong>全选</strong></td>");
                sHtml.Append("<td><input name=\"selectAll3\" type=\"checkbox\" onclick=\"SelectColsFunc('3')\" /><strong>全选</strong></td><td><input name=\"selectAll4\" type=\"checkbox\" onclick=\"SelectColsFunc('4')\" /><strong>全选</strong></td><td><input name=\"selectAll5\" type=\"checkbox\" onclick=\"SelectColsFunc('5')\" /><strong>全选</strong></td><td><input name=\"selectAll6\" type=\"checkbox\" onclick=\"SelectColsFunc('6')\" /><strong>全选</strong></td><td><input name=\"selectAll7\" type=\"checkbox\" onclick=\"SelectColsFunc('7')\" /><strong>全选</strong></td><td><input name=\"selectAll8\" type=\"checkbox\" onclick=\"SelectColsFunc('8')\" /><strong>全选</strong></td></tr>");
                for (int i = 0; i < m_PowerDt.Rows.Count; i++)
                {
                    if (i % 2 == 0) { bgColor = "#F2F8F8"; } else { bgColor = "#EDEFEC"; }
                    FuncCode = PageValidate.GetTrim(m_PowerDt.Rows[i]["FuncCode"].ToString());
                    FuncUrl = PageValidate.GetTrim(m_PowerDt.Rows[i]["FuncURL"].ToString());
                    FuncName = PageValidate.GetTrim(m_PowerDt.Rows[i]["FuncName"].ToString());
                    aryConfig = GetConfigParams(FuncCode); // 获取该功能的配置参数
                    hasPower = IsPowerExist(FuncCode, ref funcPowers);
                    sHtml.Append("<tr bgcolor=\"" + bgColor + "\" onmouseenter=\"this.style.backgroundColor='#CCFF99'\"  onmouseout=\"this.style.backgroundColor='#FFFFFF'\">");

                    if (hasPower)
                    {
                        aryPowers = funcPowers.Split(',');
                        sHtml.Append("<td><input name=\"cbxSel\" type=\"checkbox\" value=\"" + FuncCode + "\" checked=\"checked\" /></td>");
                    }
                    else { sHtml.Append("<td><input name=\"cbxSel\" type=\"checkbox\" value=\"" + FuncCode + "\" /></td>"); }

                    sHtml.Append("<td>" + FuncName + "（" + FuncCode + "）</td>");
                    // 功能操作
                    for (int j = 0; j < 9; j++)
                    {
                        if (hasPower)
                        {
                            if (string.IsNullOrEmpty(FuncUrl))
                            {
                                if (aryPowers[j] == "1") { sHtml.Append("<td></td>"); }
                                else { sHtml.Append("<td></td>"); }
                            }
                            else
                            {
                                if (aryPowers[j] == "1") { sHtml.Append("<td><input name=\"cbx" + j + "\" type=\"checkbox\" value=\"" + FuncCode + "\" checked=\"checked\" />" + aryConfig[j] + "</td>"); }
                                else { sHtml.Append("<td><input name=\"cbx" + j + "\" type=\"checkbox\" value=\"" + FuncCode + "\" />" + aryConfig[j] + "</td>"); }
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(FuncUrl)) { sHtml.Append("<td></td>"); }
                            else { sHtml.Append("<td><input name=\"cbx" + j + "\" type=\"checkbox\" value=\"" + FuncCode + "\" />" + aryConfig[j] + "</td>"); }
                        }
                    }
                    sHtml.Append("</tr>");
                }
                sHtml.Append("</table>");
                m_Ds = null;
            }

            this.LiteralFuncTable.Text = sHtml.ToString();
        }

        private DataSet m_Ds;
        private void ConfigDataSet()
        {
            m_Ds = new DataSet();
            m_Ds.Locale = System.Globalization.CultureInfo.InvariantCulture;
        }
        /// <summary>
        /// 获取配置文件信息
        /// </summary>
        private void GetConfigData()
        {
            try
            {
                ConfigDataSet();
                string configFile = Server.MapPath("/includes/DataGrid.Config");
                m_Ds.ReadXml(configFile, XmlReadMode.ReadSchema);
            }
            catch (Exception ex)
            {
                //errorMsg = "获取配置文件参数失败：" + ex.Message;
            }
        }
        /// <summary>
        /// 获取配置文件参数
        /// </summary>
        /// <param name="funcNo">功能号</param>
        /// <param name="configFile">配置文件路径</param>
        /// <returns></returns>
        private string[] GetConfigParams(string funcNo)
        {
            string[] aryNames = new string[9];
            try
            {
                DataRow[] dr = m_Ds.Tables[0].Select("FuncNo='" + funcNo + "'");
                if (dr.Length != 0)
                {
                    aryNames[0] = "新增";
                    aryNames[1] = "编辑";
                    aryNames[2] = "删除";
                    string[] aryVisible = dr[0][13].ToString().Split(',');
                    string[] aryButNames = dr[0][11].ToString().Split(',');
                    for (int i = 0; i < aryVisible.Length; i++)
                    {
                        if (aryVisible[i] != "1") { aryNames[i + 3] = ""; }
                        else { aryNames[i + 3] = aryButNames[i]; }
                    }
                }
            }
            catch (Exception ex)
            {
                //errorMsg = "获取配置文件参数失败：" + ex.Message;
            }
            return aryNames;
        }

        /// <summary>
        /// 判断用户是否已经分配角色
        /// </summary>
        /// <param name="funcCode"></param>
        /// <returns></returns>
        private bool IsPowerExist(string funcCode, ref string funcPowers)
        {
            bool returnVal = false;
            m_SqlParams = "select FuncCode,FuncPowers From SYS_RolesFunction where RoleID=" + m_ObjID;
            DataTable dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    funcPowers = dt.Rows[i]["FuncPowers"].ToString();
                    if (funcCode == dt.Rows[i]["FuncCode"].ToString())
                    {
                        returnVal = true;
                        break;
                    }
                }
            }
            return returnVal;
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string returnMsg = string.Empty;
            if (!String.IsNullOrEmpty(m_ObjID))
            {
                string cbxSelect = PageValidate.GetTrim(Request["cbxSel"]);
                string cbx0 = PageValidate.GetTrim(Request["cbx0"]);
                string cbx1 = PageValidate.GetTrim(Request["cbx1"]);
                string cbx2 = PageValidate.GetTrim(Request["cbx2"]);
                string cbx3 = PageValidate.GetTrim(Request["cbx3"]);
                string cbx4 = PageValidate.GetTrim(Request["cbx4"]);
                string cbx5 = PageValidate.GetTrim(Request["cbx5"]);
                string cbx6 = PageValidate.GetTrim(Request["cbx6"]);
                string cbx7 = PageValidate.GetTrim(Request["cbx7"]);
                string cbx8 = PageValidate.GetTrim(Request["cbx8"]);

                if (!string.IsNullOrEmpty(cbxSelect))
                {
                    // 操作之前删除已存在的所有数据
                    m_SqlParams = "DELETE FROM SYS_RolesFunction WHERE RoleID=" + m_ObjID;
                    DbHelperSQL.ExecuteSql(m_SqlParams);

                    string[] aryFunc = cbxSelect.Split(',');
                    string funcCode = string.Empty;
                    string funcPowers = string.Empty;
                    for (int i = 0; i < aryFunc.Length; i++)
                    {
                        funcCode = aryFunc[i];
                        funcPowers = "";
                        funcPowers += GetFuncPowers(cbx0, funcCode) + ",";
                        funcPowers += GetFuncPowers(cbx1, funcCode) + ",";
                        funcPowers += GetFuncPowers(cbx2, funcCode) + ",";
                        funcPowers += GetFuncPowers(cbx3, funcCode) + ",";
                        funcPowers += GetFuncPowers(cbx4, funcCode) + ",";
                        funcPowers += GetFuncPowers(cbx5, funcCode) + ",";
                        funcPowers += GetFuncPowers(cbx6, funcCode) + ",";
                        funcPowers += GetFuncPowers(cbx7, funcCode) + ",";
                        funcPowers += GetFuncPowers(cbx8, funcCode);
                        SetRolesPower(funcCode, funcPowers);
                        returnMsg = "操作成功：您所选择的权限被成功设置到角色！";
                    }
                }
                else
                {
                    returnMsg = "操作失败：请选择功能设置权限！";
                }

                // 刷新页面数据
                InitializeFuncTable();
            }
            else { returnMsg = "操作失败：参数丢失！"; }

            MessageBox.Show(this, returnMsg);
        }

        /// <summary>
        /// 获取选中的节点，给角色分配权限
        /// </summary>
        /// <param name="tnc"></param>
        private void SetRolesPower(string funcCode, string funcPowers)
        {
            if (!string.IsNullOrEmpty(funcCode))
            {
                m_SqlParams = "INSERT INTO SYS_RolesFunction(RoleID,FuncCode,FuncPowers) VALUES(" + m_ObjID + ",'" + funcCode + "','" + funcPowers + "')";
                DbHelperSQL.ExecuteSql(m_SqlParams);
            }
        }

        /// <summary>
        /// 根据功能号获取设置的对应权限
        /// </summary>
        /// <param name="keyValues"></param>
        /// <param name="funcCode"></param>
        /// <returns></returns>
        private string GetFuncPowers(string keyValues, string funcCode)
        {
            string returnVa = "0";
            if (!string.IsNullOrEmpty(keyValues))
            {
                string[] aryKey = keyValues.Split(',');
                for (int i = 0; i < aryKey.Length; i++)
                {
                    if (aryKey[i] == funcCode)
                    {
                        returnVa = "1";
                        break;
                    }
                }
            }
            return returnVa;
        }
    }
}