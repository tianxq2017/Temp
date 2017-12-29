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

using System.Text;
using System.Data.SqlClient;
using System.Globalization;

using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.web
{
    public partial class MainRight : System.Web.UI.Page
    {
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        protected string m_AD;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticateUser();

            if (!IsPostBack)
            {
                //SetPageStyle(m_UserID);
                GetUserInfo(m_UserID);
                GetStats();
                //GetUserMsg(m_UserID);

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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/loginTemp.aspx';</script>");
                Response.End();
            }
        }


        private void SetPageStyle(string userID)
        {
            try
            {
                string cssFile = "";// DbHelperSQL.GetSingle("SELECT CssStyle FROM v_UserList WHERE UserID=" + userID).ToString();
                if (string.IsNullOrEmpty(cssFile)) cssFile = "/css/index.css";

                HtmlLink cssLink = new HtmlLink();
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("href", cssFile);//urlΪcss·�� 
                this.Page.Header.Controls.Add(cssLink);
            }
            catch { }
        }

        /// <summary>
        /// �û���ʾ��Ϣ
        /// </summary>
        /// <param name="userID"></param>
        private void GetUserMsg(string userID)
        {
            // ��ǰ�û���Ϣ 
            string sqlParams = "", msgTitle, msgBody="", msgTime = string.Empty;
            System.Data.SqlClient.SqlDataReader sdr = null;
            try
            {
                sqlParams = "SELECT TOP 1 MsgTitle,MsgBody,MsgSendTime FROM SYS_Msg WHERE TargetDel=0 AND TargetUserID=" + userID + " AND MsgState=0 ORDER BY MsgID DESC";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    msgTitle = sdr[0].ToString();
                    msgBody = sdr[1].ToString();
                    msgTime = sdr[2].ToString();
                    if (msgBody.Length > 64) msgBody = msgBody.Substring(0, 62) + "��";

                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            m_AD = msgBody + "<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + DateTime.Parse(msgTime).ToString("yyyy��MM��dd��");
        }




        private string m_RoleID;
        /// <summary>
        /// ��ȡ����ʾ��ǰ��¼���û���Ϣ
        /// </summary>
        /// <param name="userID"></param>
        private void GetUserInfo(string userID)
        {
            // ��ǰ������ʾ

            // ��ǰ�û���Ϣ 
            string sqlParams = "SELECT [UserAccount], [UserName], [DeptName],[CssStyle],RoleID,[UserUnitName] FROM [v_UserList] WHERE UserID=" + userID;
            System.Data.SqlClient.SqlDataReader sdr = null;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    //SetPageStyle(sdr["CssStyle"].ToString());
                    // 
                    m_RoleID = sdr["RoleID"].ToString();
                    //this.LiteralUserInfo.Text = "��ǰ�˺ţ�" + sdr["UserAccount"].ToString() + "<br />��&nbsp;&nbsp;&nbsp;&nbsp;����" + sdr["UserName"].ToString() + "<br />����������" + sdr["DeptName"].ToString() + "";
                    //this.LiteralUserInfo.Text = "��ǰ�˺ţ�" + sdr["UserAccount"].ToString() + "<br />�á�������" + sdr["UserUnitName"].ToString() + "<br />�� �� Ա��" + sdr["UserName"].ToString() + "<br/>����������" + sdr["DeptName"].ToString() + "";
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

        }

        protected string m_StatsAup;
        protected string m_StatsPass;
        protected string m_StatsBak;
        protected string m_StatsAudit;
        protected string m_CurDate;
        

        private void GetStats()
        {
            string sqlParams = "", funcNa = "", recNum = string.Empty;
            string funcNo = string.Empty;
            int aupNum = 0;
            StringBuilder sHtml = new StringBuilder();
            SqlDataReader sdr = null;
            try
            {
                
                //ͳ�Ʋ�ѯ����
                // Attribs: 0,��ʼ�ύ;1,����� 2,ͨ�� 3,���� 4,���� 5,ע�� 6,�ȴ����,9,�鵵
                sqlParams = "SELECT COUNT(*) FROM BIZ_Contents WHERE 1=1 " + GetFilter();
                m_StatsAup = DbHelperSQL.GetSingle(sqlParams).ToString(); // �ύ����
                sqlParams = "SELECT COUNT(*) FROM BIZ_Contents WHERE Attribs IN(2,8,9) " + GetFilter();
                m_StatsPass = DbHelperSQL.GetSingle(sqlParams).ToString(); //���ͨ��
                sqlParams = "SELECT COUNT(*) FROM BIZ_Contents WHERE Attribs IN(4,5) " + GetFilter();
                m_StatsBak = DbHelperSQL.GetSingle(sqlParams).ToString(); //��˳���
                sqlParams = "SELECT COUNT(*) FROM BIZ_Contents WHERE Attribs IN(0,1,3,6) " + GetFilter();
                m_StatsAudit = DbHelperSQL.GetSingle(sqlParams).ToString(); //������

                // ��ǰ������ʾ
                string TmpStr = "";
                TmpStr += DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) + "��";
                TmpStr += DateTime.Now.Month.ToString("D2", CultureInfo.InvariantCulture) + "��";
                TmpStr += DateTime.Now.Day.ToString("D2", CultureInfo.InvariantCulture) + "��";
                TmpStr += " " + CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                m_CurDate = TmpStr;
            }
            catch
            {
                if (sdr != null) sdr.Close();
            }

        }
        /*
         1	ϵͳ����Ա
2	ҵ�����-����
3	ҵ����-����
4	ҵ����-���
5	ҵ����-����/��
6	ҵ����-ҽԺ

         */
        private string GetFilter()
        {
            string returnVa = string.Empty;// RegAreaCodeA RegAreaCodeB
            string m_UserDeptArea = string.Empty;
            string m_UserDept = join.pms.dal.CommPage.GetUnitCodeByUser(m_UserID, ref m_UserDeptArea);
            if (m_RoleID == "1" || m_RoleID == "2" || m_RoleID == "3")
            {
                returnVa = "AND 1=1";
            }
            else if (m_RoleID == "4")
            {
                //returnVa = "AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 6) + "%')";
                returnVa = "AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 9) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 9) + "%') ";
            }
            else if (m_RoleID == "5") 
            {
                returnVa = "AND (RegAreaCodeA= '" + m_UserDeptArea + "' OR RegAreaCodeB = '" + m_UserDeptArea + "' ) ";
            }
            else
            {
                //ҽԺ
                returnVa = "AND BizCode='0111' AND (SelAreaCode LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeA LIKE '" + m_UserDeptArea.Substring(0, 6) + "%' OR RegAreaCodeB LIKE '" + m_UserDeptArea.Substring(0, 6) + "%')";
            }
            return returnVa;
        }

        private int StrIntTrim(string instr)
        {
            if (!string.IsNullOrEmpty(instr) && PageValidate.IsNumber(instr))
            {
                return int.Parse(instr);
            }
            else
            {
                return 0;
            }
        }
    }
}
