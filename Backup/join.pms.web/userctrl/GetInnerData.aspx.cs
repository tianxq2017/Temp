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
using System.Globalization;
using System.Text;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
using join.pms.dal;

namespace join.pms.web.userctrl
{
    public partial class GetInnerData : System.Web.UI.Page
    {
        private string m_UserID; // ��ǰ��¼�Ĳ����û����
        private string m_SqlParams;
        private DataTable m_DT;
        private string m_FileExt = ConfigurationManager.AppSettings["FileExtension"];
        protected void Page_Load(object sender, EventArgs e)
        {

            AuthenticateUser();

            string funcNo = PageValidate.GetTrim(Request["FuncNo"]);
            string objID = PageValidate.GetTrim(Request["oID"]);
            string objName = PageValidate.GetTrim(Request["oNa"]);
            if (funcNo != "9999") AuthenticateUser();

            if (!String.IsNullOrEmpty(funcNo))
            {
                try
                {
                    SetExecMethods(funcNo, objID, objName);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

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
                Response.Write("<script language='javascript'>alert('����ʧ�ܣ���ʱ�˳��������µ�¼�����ԣ�');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }



        /// <summary>
        /// ����ִ�еķ���
        /// </summary>
        /// <param name="identityKeys"></param>
        private void SetExecMethods(string funcNo, string objID, string objName)
        {
            string returnMsg = string.Empty;

            switch (funcNo)
            {
                case "1.1.1": // ϵͳ��Ϣ��� 
                    IsExistSysMsg(m_UserID);
                    break;
                case "9999": // ǰ̨����
                    SetSearchPara(objName, objID);
                    break;
                case "8888": // ��������ѡ��
                    Response.Write(BIZ_Common.GetAreaData(objID, objName));
                    break;
                case "3.3.7": // ��ʾ��ҵ�������� 
                    Response.Write("����ʧ�ܣ��޴˽ӿڣ�");
                    break;
                case "GpyUpReInfo": // ��ȡ������ɨ���ϴ��ļ���Ϣ
                    GetGpyUpInfo(objID,objName);
                    break;
                default:
                    Response.Write("����ʧ�ܣ��޴˽ӿڣ�");
                    break;
            }
        }

        /// <summary>
        /// ��ȡ�������ϴ���Ϣ
        /// </summary>
        /// <param name="upFiles"></param>
        /// <param name="docName"></param>
        private void GetGpyUpInfo(string upFiles,string docName)
        {
            string returnVal = string.Empty;
            if (!string.IsNullOrEmpty(upFiles))
            {
                string webSvrsPath = System.Configuration.ConfigurationManager.AppSettings["SvrsWebPath"];
                string fullPath = "/Files/GpyFile/" + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) + "/" + DateTime.Now.Month.ToString("D2", CultureInfo.InvariantCulture) + "/";
                string fileID = string.Empty;
                //���浽���ݿ�
                try
                {
                    m_SqlParams = "INSERT INTO [BIZ_Docs](DocsName,DocsType,DocsPath,SourceName,SysNo) ";
                    m_SqlParams += "VALUES('" + HttpUtility.UrlDecode(docName) + "','.jpg', '" + fullPath + upFiles + "', '" + upFiles + "',1) ";
                    m_SqlParams += "SELECT SCOPE_IDENTITY()"; // SQL2005:OUTPUT INSERTED.ID
                    fileID = DbHelperSQL.GetSingle(m_SqlParams).ToString();

                    returnVal = fileID + "," + fullPath + upFiles;
                }
                catch (Exception ex)
                {
                    returnVal = "";
                }
                //����ֵ
                
            }
            else
            {
                returnVal = "";
            }
            Response.Write(returnVal);
        }

        // ������������
        private void SetSearchPara(string searchKey, string searchType)
        {
            // /SEARCH/(\w{3,20}?)/(9999)-(\d{1,8}?)-(\d{1,8}?)\
            string jbName = PageValidate.GetTrim(Request["jNa"]);
            if (string.IsNullOrEmpty(jbName)) jbName = "0";
            string navUrl = "/SEARCH/" + DESEncrypt.Encrypt(searchKey).ToLower() + "-" + jbName + "/9999-" + searchType + "-1." + m_FileExt;
            Response.Write(navUrl);
        }
        private void IsExistSysMsg(string userID)
        {
            string returnMsg = "NoMsg";
            try
            {
                m_SqlParams = "SELECT ParaValue FROM [SYS_Params] WHERE ParaCate=6 AND UserID=" + userID + " AND ParaCode='0'"; // 0��ʾ,1,����ʾ
                object objMsg = DbHelperSQL.GetSingle(m_SqlParams);
                if (objMsg == null || objMsg.ToString() == "0")
                {
                    CheckWorkLog(userID);//��־�±�����
                    //Thread _CheckLog = new Thread(new ThreadStart(CheckWorkLog));
                    //_CheckLog.IsBackground = true;
                    //_CheckLog.Start();

                    m_SqlParams = "SELECT TOP 1 MsgTitle FROM [SYS_Msg] WHERE MsgState=0 AND TargetUserID=" + userID;
                    returnMsg = DbHelperSQL.GetSingle(m_SqlParams).ToString();
                    if (!string.IsNullOrEmpty(returnMsg))
                    {
                        returnMsg = "OK" + returnMsg;
                    }
                    else
                    {
                        returnMsg = "NoMsg";
                    }
                    // �ͻ����յ�������
                    //SetNoticeInit(userID);
                    //SetScheduleInit(userID);
                    //SetScheduleInit(userID);//�ճ���ʾ
                    // SELECT TOP 1 * FROM [v_SysMsg] WHERE MsgState=0 AND TargetUserID=" + m_UserID + " ORDER BY MsgID
                }
                else { returnMsg = "NoMsg��ϵͳ��ʾ�����ã�"; }

            }
            catch (Exception ex)
            {
                returnMsg = "NoMsg��" + ex.Message;
            }
            Response.Write(returnMsg);
        }

        /// <summary>
        /// ��⹤����־���ܱ����±�����
        /// </summary>
        /// <param name="userID"></param>
        private void CheckWorkLog(string userID)
        {
            // DATEDIFF(WW,����,GETDATE())=1)  Day Month DATEDIFF(Week,CreateDate,GETDATE())=0 OR ClassCode = '0402' OR ClassCode = '0403'
            //string userID = m_UserID;
            if (String.IsNullOrEmpty(userID)) return;

            string timeNow = GetSysDateTime();
            string weekNow = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
            string startLog = System.Configuration.ConfigurationManager.AppSettings["StartDayLog"];
            string startWeek = System.Configuration.ConfigurationManager.AppSettings["StartWeeklyLog"];
            string startMonth = System.Configuration.ConfigurationManager.AppSettings["StartMonthlyLog"];

            startLog = timeNow.Substring(0, 11) + startLog;

            try
            {
                if (DateTime.Parse(timeNow) > DateTime.Parse(startLog))
                {
                    // �ж�ʱ����
                    TimeSpan ts = new TimeSpan();
                    m_SqlParams = "SELECT TOP 1 MsgSendTime FROM SYS_Msg WHERE MsgType=0 AND SourceUserID=1 AND TargetUserID=" + userID;
                    ts = DateTime.Now - DateTime.Parse(DbHelperSQL.GetSingle(m_SqlParams).ToString());
                    if (ts.Minutes == 10 || ts.Minutes > 10)
                    {
                        // �жϽ����Ƿ�д����־
                        m_SqlParams = "SELECT COUNT(*) FROM USER_WorkLog WHERE FuncNo = '0701' AND LogStatus!=4 AND UserID=" + userID + " AND DATEDIFF(Day,CreateDate,GETDATE())=0";
                        if (int.Parse(DbHelperSQL.GetSingle(m_SqlParams).ToString()) < 1)
                        {
                            m_SqlParams = "UPDATE SYS_Msg SET MsgState=0,MsgSendTime=GetDate(),DelFlag=Null WHERE MsgType=0 AND MsgTitle='��־׫д����' AND SourceUserID=1 AND TargetUserID=" + userID;
                            DbHelperSQL.ExecuteSql(m_SqlParams);
                        }
                        // �ܱ��ж�
                        //if (weekNow.Trim() == startWeek.Trim())
                        //{
                        //    m_SqlParams = "SELECT COUNT(*) FROM USER_WorkLog WHERE FuncNo = '0702' AND LogStatus!=4 AND UserID=" + userID + " AND DATEDIFF(Week,CreateDate,GETDATE())=0";
                        //    if (int.Parse(DbHelperSQL.GetSingle(m_SqlParams).ToString()) < 1)
                        //    {
                        //        m_SqlParams = "UPDATE SYS_Msg SET MsgState=0,MsgSendTime=GetDate(),DelFlag=Null WHERE MsgType=0 AND MsgTitle='�ܽ�׫д����' AND SourceUserID=1 AND TargetUserID=" + userID;
                        //        DbHelperSQL.ExecuteSql(m_SqlParams);
                        //    }
                        //}
                        // �±������ж�
                        ts = DateTime.Now.AddMonths(1).AddDays(-1) - DateTime.Now;
                        if (ts.Days > -1 && ts.Days < int.Parse(startMonth))
                        {
                            m_SqlParams = "SELECT COUNT(*) FROM USER_WorkLog WHERE FuncNo = '0702' AND LogStatus!=4 AND UserID=" + userID + " AND DATEDIFF(Month,CreateDate,GETDATE())=0";
                            if (int.Parse(DbHelperSQL.GetSingle(m_SqlParams).ToString()) < 1)
                            {
                                m_SqlParams = "UPDATE SYS_Msg SET MsgState=0,MsgSendTime=GetDate(),DelFlag=Null WHERE MsgType=0 AND MsgTitle='�ܽ�׫д����' AND SourceUserID=1 AND TargetUserID=" + userID;
                                DbHelperSQL.ExecuteSql(m_SqlParams);
                            }
                        }
                        // ��������,���ѵ����������
                        /*
                        if (userID == "6")
                        {
                            m_SqlParams = "SELECT UserID,UserName,UserBirthday,UserNotice FROM USER_BaseInfo WHERE ValidFlag=0";
                            DataTable tmpDt = new DataTable();
                            tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                            string bornUserID = string.Empty;
                            string bornDate = string.Empty;
                            string UserName = string.Empty;
                            string UserNotice = string.Empty;

                            join.pms.web.BIL.CommPage cp = new join.pms.web.BIL.CommPage();
                            for (int i = 0; i < tmpDt.Rows.Count; i++)
                            {
                                bornUserID = PageValidate.GetTrim(tmpDt.Rows[i]["UserID"].ToString());
                                UserName = PageValidate.GetTrim(tmpDt.Rows[i]["UserName"].ToString());
                                bornDate = PageValidate.GetTrim(tmpDt.Rows[i]["UserBirthday"].ToString());
                                UserNotice = PageValidate.GetTrim(tmpDt.Rows[i]["UserNotice"].ToString());
                                if (!string.IsNullOrEmpty(bornDate))
                                {
                                    if (DateTime.Parse(timeNow) > DateTime.Parse(GetUserBirthdayTime(DateTime.Parse(bornDate))).AddDays(-8) && UserNotice == "0")
                                    {
                                        // MsgType ��0,ϵͳ�㲥��1,ϵͳ��ʾ 2,վ�ڶ�Ѷ 3,����
                                        cp.SetSysMsg("ϵͳ��ʾ", "[ " + UserName + " ]�����գ�" + bornDate + "���쵽�ˣ�ȥ����Ʒ�ɣ�", "1", "1", "6");
                                    }
                                    if (DateTime.Parse(timeNow) > DateTime.Parse(bornDate))
                                    {
                                        m_SqlParams = "UPDATE USER_BaseInfo SET UserNotice=0 WHERE UserID=" + bornUserID;
                                        DbHelperSQL.ExecuteSql(m_SqlParams);
                                    }
                                }
                            }
                            cp = null;
                            tmpDt = null;
                        }*/
                    }
                }
            }
            catch { } //returnMsg = "NoMsg��" + ex.Message;}

            //Thread.Sleep(180000);
        }

        /// <summary>
        /// ��ȡ��ǰʱ��
        /// </summary>
        /// <returns></returns>
        private string GetSysDateTime()
        {
            string TmpStr = string.Empty;

            TmpStr += DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
            TmpStr += "/" + DateTime.Now.Month.ToString("D2", CultureInfo.InvariantCulture);
            TmpStr += "/" + DateTime.Now.Day.ToString("D2", CultureInfo.InvariantCulture);
            TmpStr += " " + DateTime.Now.Hour.ToString("D2", CultureInfo.InvariantCulture);
            TmpStr += ":" + DateTime.Now.Minute.ToString("D2", CultureInfo.InvariantCulture);
            TmpStr += ":" + DateTime.Now.Second.ToString("D2", CultureInfo.InvariantCulture);

            return TmpStr;
        }

        private string GetUserBirthdayTime(DateTime birthdayTime)
        {
            string TmpStr = string.Empty;

            TmpStr += DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
            TmpStr += "/" + birthdayTime.Month.ToString("D2", CultureInfo.InvariantCulture);
            TmpStr += "/" + birthdayTime.Day.ToString("D2", CultureInfo.InvariantCulture);

            return TmpStr;
        }

        /// <summary>
        /// �������Ѽ��Ƶ������
        /// </summary>
        private void SetNoticeInit(string userID)
        {
            TimeSpan ts = new TimeSpan();
            // MsgType ��0,ϵͳ�㲥��1,ϵͳ��ʾ 2,վ�ڶ�Ѷ 3,�ͻ��������� 4,�ճ̰���
            m_SqlParams = "SELECT TOP 1 MsgSendTime FROM SYS_Msg WHERE MsgType=3 AND SourceUserID=1 AND TargetUserID=" + userID;
            try
            {
                DataTable tmpDt = new DataTable();
                tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (tmpDt.Rows.Count > 0)
                {
                    ts = DateTime.Now - DateTime.Parse(tmpDt.Rows[0][0].ToString());
                    // 30��������һ��
                    if (ts.Minutes > 30)
                    {
                        SetPRNotice(userID);
                    }
                }
                else
                {
                    SetPRNotice(userID);
                }
            }
            catch
            { }
        }
        /// <summary>
        /// �ͻ���ϵά������,���յ���ǰ5������,
        /// </summary>
        /// <param name="userID"></param>
        private void SetPRNotice(string userID)
        {
            string clientID, ClientNa, Birthday, ValidDays, UserNotice = string.Empty;
            m_SqlParams = "SELECT [ID], [OnCompany]+'��'+[ContactName] As ClientNa, [Birthday],DATEDIFF(day,GETDATE(),Birthday) As ValidDays,UserNotice FROM [USER_AddressList] WHERE DATEDIFF(day,GETDATE(),Birthday)>0 AND  DATEDIFF(day,GETDATE(),Birthday) <6 AND UserID =" + userID;
            DataTable tmpDt = new DataTable();
            tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            join.pms.dal.CommPage cp = new join.pms.dal.CommPage();
            for (int i = 0; i < tmpDt.Rows.Count; i++)
            {
                clientID = PageValidate.GetTrim(tmpDt.Rows[i]["ID"].ToString());
                ClientNa = PageValidate.GetTrim(tmpDt.Rows[i]["ClientNa"].ToString());
                Birthday = PageValidate.GetTrim(tmpDt.Rows[i]["Birthday"].ToString());
                ValidDays = PageValidate.GetTrim(tmpDt.Rows[i]["ValidDays"].ToString());
                UserNotice = PageValidate.GetTrim(tmpDt.Rows[i]["UserNotice"].ToString());
                if (!string.IsNullOrEmpty(Birthday))
                {
                    if (UserNotice == "0")
                    {
                        Birthday = DateTime.Parse(Birthday).Month.ToString() + "��" + DateTime.Parse(Birthday).Day.ToString() + "��";
                        // MsgType ��0,ϵͳ�㲥��1,ϵͳ��ʾ 2,վ�ڶ�Ѷ 3,�ͻ��������� 4,�ճ̰���
                        cp.SetSysMsg("ϵͳ��ʾ", "[ " + ClientNa + " ]�����գ�" + Birthday + "���쵽�ˣ��ǲ���ȥ��ʾһ�£��͵�С���", "3", "1", userID);
                    }
                }
            }
            //�����ڼ�¼��ԭΪ��ʼ״̬
            m_SqlParams = "UPDATE USER_AddressList SET UserNotice=0 WHERE DATEDIFF(day,GETDATE(),Birthday)<0 AND UserID =" + userID;
            DbHelperSQL.ExecuteSql(m_SqlParams);
            cp = null;
            tmpDt = null;
        }

        /// <summary>
        /// �ճ����Ѽ��Ƶ������
        /// </summary>
        private void SetScheduleInit(string userID)
        {
            string ScheduleID = string.Empty;
            string targetUserID = string.Empty;
            string StartTime = string.Empty;
            string EventTitle = string.Empty;
            // MsgType ��0,ϵͳ�㲥��1,ϵͳ��ʾ 2,վ�ڶ�Ѷ 3,�ͻ��������� 4,�ճ̰���
            // m_SqlParams = "SELECT TOP 1 MsgSendTime FROM SYS_Msg WHERE MsgType=4 AND SourceUserID=1 AND TargetUserID=" + userID;
            m_SqlParams = "SELECT [ScheduleID], [UserID], EventTitle,EventBody,[StartTime], [AwokePreTime],(DATEADD(minute, CAST(AwokePreTime As int), StartTime)) As RealStartTime FROM [USER_Schedule] WHERE IsAwoke=0 AND DATEADD(minute, CAST(AwokePreTime As int), StartTime)<GetDate()";
            DataTable tmpDt = new DataTable();
            join.pms.dal.CommPage cp = new join.pms.dal.CommPage();
            try
            {

                tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                for (int i = 0; i < tmpDt.Rows.Count; i++)
                {
                    ScheduleID = PageValidate.GetTrim(tmpDt.Rows[i]["ScheduleID"].ToString());
                    targetUserID = PageValidate.GetTrim(tmpDt.Rows[i]["UserID"].ToString());
                    StartTime = PageValidate.GetTrim(tmpDt.Rows[i]["StartTime"].ToString());
                    EventTitle = PageValidate.GetTrim(tmpDt.Rows[i]["EventTitle"].ToString());
                    if (!string.IsNullOrEmpty(EventTitle))
                    {
                        // MsgType ��0,ϵͳ�㲥��1,ϵͳ��ʾ 2,վ�ڶ�Ѷ 3,�ͻ��������� 4,�ճ̰���
                        cp.SetSysMsg("ϵͳ��ʾ", "�����ճ̰���[ " + EventTitle + " ]ʱ��[" + StartTime + "]�쵽�ˣ���ϸ�������£�<br>" + PageValidate.GetTrim(tmpDt.Rows[i]["EventBody"].ToString()), "1", "1", targetUserID);
                        // ������ʾ״̬��0,Ĭ��δ��ʾ��1,����ʾ
                        DbHelperSQL.ExecuteSql("UPDATE USER_Schedule SET IsAwoke=1 WHERE ScheduleID=" + ScheduleID);
                    }
                }
            }
            catch { ; }
            tmpDt.Dispose();
        }
    }
}