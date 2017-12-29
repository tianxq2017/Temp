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
    public partial class ClassEdit : System.Web.UI.Page
    {
        private string m_SqlParams;
        private DataTable m_Dt;
        private string m_UserID; // ��ǰ��¼�Ĳ����û����

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();
            ValidateParams();
            
        }

        private void ValidateParams()
        {
            string cID = PageValidate.GetFilterSQL(Request.QueryString["cID"]);
            string pID = PageValidate.GetFilterSQL(Request.QueryString["pID"]);
            string cCode = PageValidate.GetFilterSQL(Request.QueryString["cCode"]);
            string cNa = PageValidate.GetFilterSQL(Request.QueryString["cNa"]);
            string subCount = PageValidate.GetFilterSQL(Request.QueryString["subCount"]);
            if (String.IsNullOrEmpty(cID) || !PageValidate.IsNumber(cID) || String.IsNullOrEmpty(pID) || !PageValidate.IsNumber(pID) || String.IsNullOrEmpty(cCode) || !PageValidate.IsNumber(cCode))
            {
                Response.Write("�Ƿ����ʣ���������ֹ��");
                Response.End();
            }
            else
            {
                this.txtMenuID.Value = cID;
                this.txtMenuCount.Value = subCount;
                this.txtMenuCode.Value = cCode;

                this.LabelSelectNode.Text = "֦�ڵ�,(������ɾ����ֻ��ɾ���˽ڵ��������ӽڵ������ɾ��)";
                if (subCount == "0") { this.LabelSelectNode.Text = "Ҷ�ڵ�"; }

                ShowNodeInfo(cID);
                ShowSubNodeInfo(cID);
            }
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
                //Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='../default.aspx';</script>");
                Response.Write("'����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�");
                Response.End();
            }
        }

        /// <summary>
        /// ��ʾѡ���Ľڵ���Ϣ
        /// </summary>
        /// <param name="classID"></param>
        private void ShowNodeInfo(string classID)
        {
            m_SqlParams = "SELECT [ClassID], [ParentClassID], [TradeCode], [ClassIndex], [ClassName],[ClassEName],[ClassFlag] FROM [TradeClass] WHERE ClassID=" + classID;
            m_Dt = new DataTable();
            try
            {
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (m_Dt.Rows.Count==1) 
                {
                    this.txtClassName.Text = m_Dt.Rows[0]["ClassName"].ToString();
                    this.txtClassEName.Text = m_Dt.Rows[0]["ClassEName"].ToString();
                    this.txtUpdateClassIndex.Text = m_Dt.Rows[0]["ClassIndex"].ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            m_Dt = null;
        }

        /// <summary>
        /// ��ʾѡ���Ľڵ���¼��ڵ���Ϣ
        /// </summary>
        /// <param name="classID"></param>
        private void ShowSubNodeInfo(string classID)
        {
            // [ClassID], [ParentClassID], [ClassCode], [ClassIndex], [ClassName] ClassFlag
            m_SqlParams = "SELECT [ClassID], [ParentClassID], [TradeCode], [ClassIndex], [ClassName],[ClassEName],ClassFlag FROM [TradeClass] WHERE ParentClassID=" + classID + " ORDER BY TradeCode";
            m_Dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            try
            {
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                for (int i = 0; i < m_Dt.Rows.Count; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td height=\"25\" class=\"tdLine\">" + m_Dt.Rows[i][0].ToString() + "</td>");
                    sb.Append("<td class=\"tdLine\">" + m_Dt.Rows[i][1].ToString() + "</td>");
                    sb.Append("<td class=\"tdLine\">" + m_Dt.Rows[i][2].ToString() + "</td>");
                    sb.Append("<td class=\"tdLine\">" + m_Dt.Rows[i][3].ToString() + "</td>");
                    sb.Append("<td class=\"tdLine\">" + m_Dt.Rows[i][4].ToString() + "</td>");
                    sb.Append("<td class=\"tdLine\">" + m_Dt.Rows[i][5].ToString() + "</td>");
                    sb.Append("<td class=\"tdLine\">" + m_Dt.Rows[i][6].ToString() + "</td>");
                    sb.Append("</tr>");
                }
                if (m_Dt.Rows.Count > 0) this.txtMaxCode.Value = m_Dt.Rows[m_Dt.Rows.Count - 1][2].ToString();
            }
            catch (Exception ex)
            {
                sb.Append(ex.Message);
            }
            m_Dt = null;

            this.LiteralArea.Text = sb.ToString();
        }
        // ɾ��
        protected void ButDelete_Click(object sender, System.EventArgs e)
        {
            if (this.txtMenuCount.Value != "0")
            {
                Response.Write("<script language=JavaScript>alert('֦�ڵ㲻����ɾ��������');</script>");
                return;
            }
            if (int.Parse(this.txtMenuID.Value)<28)
            {
                Response.Write("<script language=JavaScript>alert('�����ڵ㲻����ɾ��������');</script>");
                return;
            }
            m_SqlParams = "DELETE FROM TradeClass WHERE ClassID = '" + this.txtMenuID.Value + "'";
            try 
            {
                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                    ButAddNew.Enabled = false;
                    Response.Write("<script language=JavaScript>alert('�ɹ�ɾ���ڵ���=" + this.txtMenuID.Value + "��Ҷ�ڵ㣡');</script>");
                    Response.Write("<script language=JavaScript>parent.treeLeft.location.reload();window.location.href='about:blank';</script>");
                }
            }
            catch
            {
                Response.Write("����ʧ�ܣ��������´��ڷ�������Ʒʱ�����಻����ɾ����");
            }
        }
        // ���� txtUpdateClassIndex
        protected void ButUpdate_Click(object sender, System.EventArgs e)
        {
            
            string classID = this.txtMenuID.Value;
            string className = PageValidate.GetTrim(Request["txtClassName"]);
            string ClassEName = PageValidate.GetTrim(Request["txtClassEName"]);
            string classIndex = PageValidate.GetTrim(Request["txtUpdateClassIndex"]);
            // [A-Z]��
            if (String.IsNullOrEmpty(classID) || String.IsNullOrEmpty(className) || String.IsNullOrEmpty(classIndex) || String.IsNullOrEmpty(ClassEName))
            {
                Response.Write("<script language=JavaScript>alert('�ڵ����ƺ���������Ϊ��ֵ���ұ�����ϱ���Ҫ��');</script>");
                return;
            }
            if (!PageValidate.RexIsMatch("[A-Z]", classIndex)) 
            {
                Response.Write("<script language=JavaScript>alert('�ڵ����������Ǵ�д����ĸ��');</script>");
                return;
            }

            m_SqlParams = "UPDATE TradeClass SET ClassName='" + className + "',ClassEName='" + ClassEName + "',ClassIndex='" + classIndex + "' WHERE ClassID=" + classID;
            try
            {
                if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                {
                    ShowNodeInfo(classID);
                    ShowSubNodeInfo(classID);
                }
            }
            catch
            {
                Response.Write("����ʧ�ܣ�����������������ԣ�");
            }
            

            Response.Write("<script language=JavaScript>parent.treeLeft.location.reload();</script>");
        }

        protected void ButAddNew_Click(object sender, System.EventArgs e)
        {
            string classID = this.txtMenuID.Value;
            string className = PageValidate.GetTrim(this.txtAddClassName.Text); // 
            string ClassEName = PageValidate.GetTrim(this.txtAddClassEName.Text); 
            string classCode = GetClassCode(this.txtMenuCode.Value);// PageValidate.GetTrim(this.txtAddClassCode.Text);
            string classIndex = PageValidate.GetTrim(this.txtAddClassIndex.Text);

            if (String.IsNullOrEmpty(classID) || String.IsNullOrEmpty(className) || String.IsNullOrEmpty(classCode) || !PageValidate.IsNumber(classID) || !PageValidate.IsNumber(classCode) || !PageValidate.RexIsMatch("[A-Z]", classIndex))
            {
                Response.Write("<script language=JavaScript>alert('�ڵ����ƺ���������Ϊ��ֵ���ұ�����ϱ���Ҫ��');</script>");
                return;
            }
            else
            {
                m_SqlParams = "INSERT INTO TradeClass(ParentClassID,TradeCode,ClassIndex,ClassName,ClassEName) VALUES(" + classID + ",'" + classCode + "','" + classIndex + "','" + className + "','" + ClassEName + "')";
                try
                {
                    if (DbHelperSQL.ExecuteSql(m_SqlParams) > 0)
                    {
                        ShowNodeInfo(classID);
                        ShowSubNodeInfo(classID);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script language=JavaScript>alert('" + ex.Message + "��');</script>");
                }
                finally
                {
                    ShowSubNodeInfo(classID);
                }
            }

            Response.Write("<script language=JavaScript>parent.treeLeft.location.reload();</script>");
        }

        /// <summary>
        /// ��ȡ�µı����ַ�
        /// </summary>
        /// <param name="rootCode"></param>
        /// <returns></returns>
        private string GetClassCode(string rootCode) 
        {
            string newCode = string.Empty;
            string sonNodeMaxCode = PageValidate.GetTrim(this.txtMaxCode.Value);
            if (String.IsNullOrEmpty(sonNodeMaxCode))
            {
                newCode = rootCode + "01";
            }
            else 
            {
                sonNodeMaxCode = sonNodeMaxCode.Substring(sonNodeMaxCode.Length - 2);
                int sonCode = int.Parse(sonNodeMaxCode);
                sonCode = sonCode + 1;
                if (sonCode > 9) 
                {
                    newCode = rootCode + sonCode.ToString();
                } 
                else 
                {
                    newCode = rootCode + sonCode.ToString().PadLeft(2,'0');
                }
            }
            return newCode;
        }
    }
}
