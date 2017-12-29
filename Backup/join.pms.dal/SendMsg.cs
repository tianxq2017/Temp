using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Data;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.dal
{
    public class SendMsg
    {
        private static string m_UserSign = System.Configuration.ConfigurationManager.AppSettings["UserSign"];
        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GetRondomCode()
        {
            string returnVa = string.Empty;
            int number;
            char code;
            System.Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                number = random.Next();
                //if (number % 2 == 0)
                code = (char)('0' + (char)(number % 10));
                //else
                //    code = (char)('a' + (char)(number % 26));

                returnVa += code.ToString();
            }
            return returnVa;
        }
        /// <summary>
        /// 发短讯
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public static string SendMsgByModem(string mobileNo, string msgBody)
        {
            // return SendMsgByWebResponse(mobileNo, msgBody);
            string sqlParams = string.Empty;
            string returnMsg = string.Empty;
            int SendNum = 0; int count = 0;
            string[] aryTels = null;
            if (!string.IsNullOrEmpty(mobileNo))
            {
                int Status = 0;//待发送
                mobileNo = CommPage.replaceNUM(mobileNo.Replace("-", "").Replace("—", "").Replace(" ", "").Replace("　", "").Replace("13988888888", ""));
                
                if (msgBody.Length > 490) msgBody = msgBody.Substring(0, 490) + "...";
                    msgBody += m_UserSign;
                    SendNum = (msgBody.Length / 67) + 1;
                    System.Collections.Generic.List<string> list = null;
                    if (mobileNo.Length > 20)
                    {
                        aryTels = mobileNo.Split(',');
                        list = new System.Collections.Generic.List<string>(aryTels.Length);
                        for (int j = 0; j < aryTels.Length; j++)
                        {

                            if (aryTels[j].ToString().Length != 11 || aryTels[j].Substring(0, 1) != "1")
                            {
                                Status = 2;//不是手机号的直接表示发送成功
                            }
                            list.Add("INSERT INTO [SMS](CellNumber,SMSContent,SendNum,Status) VALUES('" + aryTels[j] + "','" + msgBody + "'," + SendNum + "," + Status + ")");
                            count++;
                        }
                    }
                    else
                    {

                        if (mobileNo.ToString().Length != 11 || mobileNo.Substring(0, 1) != "1")
                        {
                            Status = 2;//不是手机号的直接表示发送成功
                        }
                        list = new System.Collections.Generic.List<string>(1);
                        list.Add("INSERT INTO [SMS](CellNumber,SMSContent,SendNum,Status) VALUES('" + mobileNo + "','" + msgBody + "'," + SendNum + "," + Status + ")");
                        count++;
                    }
                    try
                    {
                        if (DbHelperSQL.ExecuteSqlTran(list) > 0) { returnMsg = count.ToString(); }
                        list = null;
                    }
                    catch (Exception ex) { returnMsg = "操作失败：" + ex; list = null; }
                    list = null;
                
            }
            else
            {
                returnMsg = "操作失败：您没有输入手机号码；无法发送短讯！";
            }
            return returnMsg;
        }
    }
}

