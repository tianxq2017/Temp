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

namespace join.pms.web.SysAdmin
{
    public partial class SysUserRoles : System.Web.UI.Page
    {
        private string m_SourceUrl;
        private string m_SourceUrlDec;
        private string m_ActionName;
        private string m_ObjID;

        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        private string m_SqlParams;
        private DataTable m_Dt;
        protected string m_TargetUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();

            if (!IsPostBack)
            {
                if (PageValidate.IsNumber(m_ObjID))
                {
                    SetOpratetionAction("�ʺŹ���");
                    GetRoles();
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ�����ǵ�ϵͳ��ȫ���˲���ÿ��ֻ��ѡ��һ���ʺţ������Զ�ѡ��", m_TargetUrl, true);
                }
            }
        }

        #region

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
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }

            if (m_ActionName == "view") m_ObjID = PageValidate.GetFilterSQL(Request.QueryString["RID"]);
        }

        /// <summary>
        /// �����֤
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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/loginTemp.aspx';</script>");
                Response.End();
            }
        }

        /// <summary>
        /// ���ò�����Ϊ
        /// </summary>
        /// <param name="oprateName"></param>
        private void SetOpratetionAction(string oprateName)
        {
            string funcName = string.Empty;

            if (String.IsNullOrEmpty(m_ObjID))
            {
                if (m_ActionName != "add")
                {
                    Response.Write("�Ƿ����ʣ���������ֹ��");
                    Response.End();
                }
            }
            else
            {
                if (!PageValidate.IsNumber(m_ObjID))
                {
                    m_ObjID = m_ObjID.Replace("s", ",");
                }
            }
            switch (m_ActionName)
            {
                case "add": // ����
                    funcName = "����";
                    break;
                case "edit": // �༭
                    funcName = "�޸�";
                    //ShowModInfo(m_ObjID);
                    break;
                case "del": // ɾ��
                    funcName = "ɾ������";
                    //DelInfo(m_ObjID);
                    break;
                case "view": // �鿴
                    funcName = "�鿴����";
                    break;
                case "5": // ���
                    funcName = "�������";
                    break;
                case "6": // �Ƽ�
                    funcName = "�����ɫ";
                    //recommend(m_ObjID);
                    // Response.Redirect(txtUrlParams.Value);
                    break;
                default:
                    MessageBox.ShowAndRedirect(this.Page, "������ʾ����������", m_TargetUrl, true);
                    break;
            }
            this.LiteralNav.Text = "<a href=\"/MainDesk.aspx\">������ҳ</a> &gt;&gt; <a href=\"" + m_TargetUrl + "\">" + oprateName + "</a> &gt;&gt; " + funcName + "��";
        }

        #endregion



        private void GetRoles()
        {
            m_SqlParams = "SELECT [RoleID], [RoleName]+'--'+[RoleNotes] As RoleName  FROM [SYS_Roles] ";
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            this.RadioButtonList1.DataSource = m_Dt;
            this.RadioButtonList1.DataTextField = "RoleName";
            this.RadioButtonList1.DataValueField = "RoleID";
            this.RadioButtonList1.DataBind();


            m_Dt = DbHelperSQL.Query("SELECT [RoleID] FROM [SYS_UserRoles] WHERE UserID=" + m_ObjID).Tables[0];
            if (m_Dt.Rows.Count == 1)
            {
                this.RadioButtonList1.Items.FindByValue(m_Dt.Rows[0][0].ToString()).Selected = true;
            }
        }


        /// <summary>
        /// ������
        /// </summary>
        /// <param name="funcNo"></param>
        public void InitializeTree(string objID)
        {
            m_SqlParams = "SELECT [RoleID], [RoleName]+'--'+[RoleNotes] As RoleName  FROM [SYS_Roles]";

           //TreeView1.Nodes.Clear();

            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            if (m_Dt.Rows.Count > 0)
            {
                AddTree("", "", (TreeNode)null);
            }

            m_Dt = null;


        }
        protected void AddTree(string funcNo, string ParentID, TreeNode pNode)
        {
            string roleID = string.Empty;
            string roleTitle = string.Empty;

            DataView Dv = new DataView(m_Dt);

            foreach (DataRowView Row in Dv)
            {
                roleID = Row["RoleID"].ToString();
                roleTitle = Row["RoleName"].ToString();

                TreeNode Node = new TreeNode();
                Node.ShowCheckBox = true;

                if (IsRoleExist(roleID)) Node.Checked = true;
                Node.Text = roleTitle;
                Node.Value = roleID;
              //  TreeView1.Nodes.Add(Node);
                Node.Expanded = true;
            }
        }
        /// <summary>
        /// �ж��û��Ƿ��Ѿ������ɫ
        /// </summary>
        /// <param name="menuCode"></param>
        /// <returns></returns>
        private bool IsRoleExist(string roleID)
        {
            string strSql = "select RoleID From SYS_UserRoles where UserID=" + m_ObjID;
            DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
            bool returnVal = false;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (roleID == dt.Rows[i]["RoleID"].ToString())
                    {
                        returnVal = true;
                        break;
                    }
                }
            }
            return returnVal;
        }
        private int m_SelRoles = 0;
        /// <summary>
        /// ��ȡѡ�еĽڵ㣬����ɫ����Ȩ��
        /// </summary>
        /// <param name="tnc"></param>
        private void GetAllCheckedNode(TreeNodeCollection pNode, string userID)
        {

            foreach (TreeNode node in pNode)
            {
                if (node.Checked == true)
                {
                    m_SelRoles++;
                    if (m_SelRoles > 1) 
                    {
                        break;
                    } 
                    else 
                    {
                        // INSERT INTO [SYS_UserRoles]([RoleID], [UserID])
                        m_SqlParams = "INSERT INTO [SYS_UserRoles]([RoleID], [UserID]) VALUES(" + node.Value + ",'" + m_ObjID + "')";
                        DbHelperSQL.ExecuteSql(m_SqlParams);
                    }
                }
                if (node.ChildNodes.Count > 0)
                {
                    GetAllCheckedNode(node.ChildNodes, userID);
                }
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(m_ObjID) && !String.IsNullOrEmpty(RadioButtonList1.SelectedValue))
            {
                m_SqlParams = "SELECT COUNT(*) FROM SYS_UserRoles WHERE UserID=" + m_ObjID;
                string rblValue = RadioButtonList1.SelectedValue;
                object objCount = DbHelperSQL.GetSingle(m_SqlParams);
                if (objCount == null || objCount.ToString() == "0")
                {
                    m_SqlParams = "INSERT INTO [SYS_UserRoles]([RoleID], [UserID]) VALUES(" + rblValue + "," + m_ObjID + ")";
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                }
                else if (objCount.ToString() == "1")
                {
                    m_SqlParams = "UPDATE [SYS_UserRoles] SET [RoleID]=" + rblValue + " WHERE UserID=" + m_ObjID;
                    DbHelperSQL.ExecuteSql(m_SqlParams);
                }
                string UserLevel = string.Empty;
                //0,Ĭ��,5,����,9,����
               // if (rblValue == "1") { UserLevel = "9"; }
              
               // else { UserLevel = "1"; }

                m_SqlParams = "UPDATE USER_BaseInfo SET ValidFlag=1,UserLevel=9 WHERE UserID=" + m_ObjID; 
                DbHelperSQL.ExecuteSql(m_SqlParams);
                MessageBox.Show(this, "����ɹ�");
            }
        }
    }
}