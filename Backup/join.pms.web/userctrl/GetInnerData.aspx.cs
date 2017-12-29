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
        private string m_UserID; // 当前登录的操作用户编号
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
                Response.Write("<script language='javascript'>alert('操作失败：超时退出，请重新登录后再试！');parent.location.href='/Default.shtml?action=closewindow';</script>");
                Response.End();
            }
        }



        /// <summary>
        /// 设置执行的方法
        /// </summary>
        /// <param name="identityKeys"></param>
        private void SetExecMethods(string funcNo, string objID, string objName)
        {
            string returnMsg = string.Empty;

            switch (funcNo)
            {
                case "1.1.1": // 系统消息检测 
                    IsExistSysMsg(m_UserID);
                    break;
                case "9999": // 前台搜索
                    SetSearchPara(objName, objID);
                    break;
                case "8888": // 行政区划选择
                    Response.Write(BIZ_Common.GetAreaData(objID, objName));
                    break;
                case "3.3.7": // 显示行业分类主类 
                    Response.Write("调用失败：无此接口！");
                    break;
                case "GpyUpReInfo": // 获取高拍仪扫描上传文件信息
                    GetGpyUpInfo(objID,objName);
                    break;
                default:
                    Response.Write("调用失败：无此接口！");
                    break;
            }
        }

        /// <summary>
        /// 获取高拍仪上传信息
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
                //保存到数据库
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
                //返回值
                
            }
            else
            {
                returnVal = "";
            }
            Response.Write(returnVal);
        }

        // 设置搜索参数
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
                m_SqlParams = "SELECT ParaValue FROM [SYS_Params] WHERE ParaCate=6 AND UserID=" + userID + " AND ParaCode='0'"; // 0提示,1,不提示
                object objMsg = DbHelperSQL.GetSingle(m_SqlParams);
                if (objMsg == null || objMsg.ToString() == "0")
                {
                    CheckWorkLog(userID);//日志月报提醒
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
                    // 客户生日到期提醒
                    //SetNoticeInit(userID);
                    //SetScheduleInit(userID);
                    //SetScheduleInit(userID);//日程提示
                    // SELECT TOP 1 * FROM [v_SysMsg] WHERE MsgState=0 AND TargetUserID=" + m_UserID + " ORDER BY MsgID
                }
                else { returnMsg = "NoMsg：系统提示被禁用！"; }

            }
            catch (Exception ex)
            {
                returnMsg = "NoMsg：" + ex.Message;
            }
            Response.Write(returnMsg);
        }

        /// <summary>
        /// 检测工作日志、周报、月报提醒
        /// </summary>
        /// <param name="userID"></param>
        private void CheckWorkLog(string userID)
        {
            // DATEDIFF(WW,日期,GETDATE())=1)  Day Month DATEDIFF(Week,CreateDate,GETDATE())=0 OR ClassCode = '0402' OR ClassCode = '0403'
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
                    // 判断时间间隔
                    TimeSpan ts = new TimeSpan();
                    m_SqlParams = "SELECT TOP 1 MsgSendTime FROM SYS_Msg WHERE MsgType=0 AND SourceUserID=1 AND TargetUserID=" + userID;
                    ts = DateTime.Now - DateTime.Parse(DbHelperSQL.GetSingle(m_SqlParams).ToString());
                    if (ts.Minutes == 10 || ts.Minutes > 10)
                    {
                        // 判断今天是否写过日志
                        m_SqlParams = "SELECT COUNT(*) FROM USER_WorkLog WHERE FuncNo = '0701' AND LogStatus!=4 AND UserID=" + userID + " AND DATEDIFF(Day,CreateDate,GETDATE())=0";
                        if (int.Parse(DbHelperSQL.GetSingle(m_SqlParams).ToString()) < 1)
                        {
                            m_SqlParams = "UPDATE SYS_Msg SET MsgState=0,MsgSendTime=GetDate(),DelFlag=Null WHERE MsgType=0 AND MsgTitle='日志撰写提醒' AND SourceUserID=1 AND TargetUserID=" + userID;
                            DbHelperSQL.ExecuteSql(m_SqlParams);
                        }
                        // 周报判断
                        //if (weekNow.Trim() == startWeek.Trim())
                        //{
                        //    m_SqlParams = "SELECT COUNT(*) FROM USER_WorkLog WHERE FuncNo = '0702' AND LogStatus!=4 AND UserID=" + userID + " AND DATEDIFF(Week,CreateDate,GETDATE())=0";
                        //    if (int.Parse(DbHelperSQL.GetSingle(m_SqlParams).ToString()) < 1)
                        //    {
                        //        m_SqlParams = "UPDATE SYS_Msg SET MsgState=0,MsgSendTime=GetDate(),DelFlag=Null WHERE MsgType=0 AND MsgTitle='总结撰写提醒' AND SourceUserID=1 AND TargetUserID=" + userID;
                        //        DbHelperSQL.ExecuteSql(m_SqlParams);
                        //    }
                        //}
                        // 月报提醒判断
                        ts = DateTime.Now.AddMonths(1).AddDays(-1) - DateTime.Now;
                        if (ts.Days > -1 && ts.Days < int.Parse(startMonth))
                        {
                            m_SqlParams = "SELECT COUNT(*) FROM USER_WorkLog WHERE FuncNo = '0702' AND LogStatus!=4 AND UserID=" + userID + " AND DATEDIFF(Month,CreateDate,GETDATE())=0";
                            if (int.Parse(DbHelperSQL.GetSingle(m_SqlParams).ToString()) < 1)
                            {
                                m_SqlParams = "UPDATE SYS_Msg SET MsgState=0,MsgSendTime=GetDate(),DelFlag=Null WHERE MsgType=0 AND MsgTitle='总结撰写提醒' AND SourceUserID=1 AND TargetUserID=" + userID;
                                DbHelperSQL.ExecuteSql(m_SqlParams);
                            }
                        }
                        // 生日提醒,提醒到利绢的桌面
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
                                        // MsgType ：0,系统广播，1,系统提示 2,站内短讯 3,其它
                                        cp.SetSysMsg("系统提示", "[ " + UserName + " ]的生日（" + bornDate + "）快到了，去买礼品吧！", "1", "1", "6");
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
            catch { } //returnMsg = "NoMsg：" + ex.Message;}

            //Thread.Sleep(180000);
        }

        /// <summary>
        /// 获取当前时间
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
        /// 生日提醒间隔频率设置
        /// </summary>
        private void SetNoticeInit(string userID)
        {
            TimeSpan ts = new TimeSpan();
            // MsgType ：0,系统广播，1,系统提示 2,站内短讯 3,客户生日提醒 4,日程安排
            m_SqlParams = "SELECT TOP 1 MsgSendTime FROM SYS_Msg WHERE MsgType=3 AND SourceUserID=1 AND TargetUserID=" + userID;
            try
            {
                DataTable tmpDt = new DataTable();
                tmpDt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                if (tmpDt.Rows.Count > 0)
                {
                    ts = DateTime.Now - DateTime.Parse(tmpDt.Rows[0][0].ToString());
                    // 30分钟提醒一次
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
        /// 客户关系维持提醒,生日到期前5天提醒,
        /// </summary>
        /// <param name="userID"></param>
        private void SetPRNotice(string userID)
        {
            string clientID, ClientNa, Birthday, ValidDays, UserNotice = string.Empty;
            m_SqlParams = "SELECT [ID], [OnCompany]+'·'+[ContactName] As ClientNa, [Birthday],DATEDIFF(day,GETDATE(),Birthday) As ValidDays,UserNotice FROM [USER_AddressList] WHERE DATEDIFF(day,GETDATE(),Birthday)>0 AND  DATEDIFF(day,GETDATE(),Birthday) <6 AND UserID =" + userID;
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
                        Birthday = DateTime.Parse(Birthday).Month.ToString() + "月" + DateTime.Parse(Birthday).Day.ToString() + "日";
                        // MsgType ：0,系统广播，1,系统提示 2,站内短讯 3,客户生日提醒 4,日程安排
                        cp.SetSysMsg("系统提示", "[ " + ClientNa + " ]的生日（" + Birthday + "）快到了，是不是去表示一下，送点小礼物？", "3", "1", userID);
                    }
                }
            }
            //将过期记录还原为初始状态
            m_SqlParams = "UPDATE USER_AddressList SET UserNotice=0 WHERE DATEDIFF(day,GETDATE(),Birthday)<0 AND UserID =" + userID;
            DbHelperSQL.ExecuteSql(m_SqlParams);
            cp = null;
            tmpDt = null;
        }

        /// <summary>
        /// 日程提醒间隔频率设置
        /// </summary>
        private void SetScheduleInit(string userID)
        {
            string ScheduleID = string.Empty;
            string targetUserID = string.Empty;
            string StartTime = string.Empty;
            string EventTitle = string.Empty;
            // MsgType ：0,系统广播，1,系统提示 2,站内短讯 3,客户生日提醒 4,日程安排
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
                        // MsgType ：0,系统广播，1,系统提示 2,站内短讯 3,客户生日提醒 4,日程安排
                        cp.SetSysMsg("系统提示", "您的日程安排[ " + EventTitle + " ]时间[" + StartTime + "]快到了，详细内容如下：<br>" + PageValidate.GetTrim(tmpDt.Rows[i]["EventBody"].ToString()), "1", "1", targetUserID);
                        // 更改提示状态：0,默认未提示；1,已提示
                        DbHelperSQL.ExecuteSql("UPDATE USER_Schedule SET IsAwoke=1 WHERE ScheduleID=" + ScheduleID);
                    }
                }
            }
            catch { ; }
            tmpDt.Dispose();
        }
    }
}