using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Globalization;

namespace join.pms.dal
{
    /// <summary>
    /// 通用业务逻辑处理类库
    /// </summary>
    public class CommBiz
    {
        ~CommBiz() { }

        /// <summary>
        /// 返回参数值
        /// </summary>
        /// <param name="inParaStr">参数串</param>
        /// <param name="keyWord">参数名</param>
        /// <returns>参数值</returns>
        public static string AnalysisParas(string inParaStr, string keyWord)
        {
            if (String.IsNullOrEmpty(inParaStr)) return "";
            if (String.IsNullOrEmpty(keyWord)) return "";
            string[] s = inParaStr.Split('&');
            string r = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].ToString().IndexOf(keyWord) > -1)
                {
                    r = s[i].ToString().Replace(keyWord, "").Replace("=", "").Trim();
                }
            }
            return r;
        }
        #region 敏感关键词过滤
        private static string _FilterChr = System.Configuration.ConfigurationManager.AppSettings["SiteFilter"];
        /// <summary>
        /// 字符过滤
        /// </summary>
        /// <param name="inChar">传入字符串</param>
        /// <param name="isExist">是否存在</param>
        /// <returns>返回的字符</returns>
        public static string SetFilter(string inValues, ref bool isExist)
        {
            isExist = false;
            if (!string.IsNullOrEmpty(inValues) && inValues.Trim() != "")
            {
                string[] aryKeys = _FilterChr.ToLower().Split(',');
                for (int i = 0; i < aryKeys.Length; i++)
                {
                    if (inValues.ToUpper().IndexOf(aryKeys[i]) > -1)
                    {
                        isExist = true;
                        //break;
                        inValues = inValues.Replace(aryKeys[i], " <font color=red>□□</font> ");
                    }
                }
            }

            return inValues;
        }
        #endregion
        /// <summary>
        /// 系统关键词展示
        /// </summary>
        /// <param name="inValues"></param>
        /// <returns></returns>
        public static string SetKeyWordFilter(string inValues)
        {
            // "和生|www.ehesheng.com/,人民网|www.person.cn/,\r\n网易|www.163.com/,网是科技|areweb.com.cn"
            if (!string.IsNullOrEmpty(inValues) && inValues.Trim() != "")
            {
                string[] aryKeys = null;// AreWeb.Pigeons.Dal.CommDal.GetSysKeyWords().ToLower().Split(',');
                string keyName = "", keyUrl = "";
                int pos = -1;
                string leftVal = "", rightVal = "";
                for (int i = 0; i < aryKeys.Length; i++)
                {
                    if (!string.IsNullOrEmpty(aryKeys[i]))
                    {
                        keyName = aryKeys[i].Substring(0, aryKeys[i].IndexOf("|"));
                        keyUrl = aryKeys[i].Substring(aryKeys[i].IndexOf("|") + 1);

                        if (keyUrl.IndexOf("tp://") < 0) keyUrl = "http://" + keyUrl;

                        pos = inValues.ToLower().IndexOf(keyName);
                        if (pos > -1)
                        {
                            leftVal = inValues.Substring(0, pos);
                            rightVal = inValues.Substring(pos + keyName.Length);
                            inValues = leftVal + " <a href=\"" + keyUrl + "\" target=\"_blank\">" + keyName + "</a> " + rightVal;
                            //if (inValues.ToLower().IndexOf(keyName) > -1)
                            //{
                            //    inValues = inValues.Replace(keyName, " <a href=\"" + keyUrl + "\" target=\"_blank\">" + keyName + "</a> ");
                            //}
                        }
                    }
                }
            }

            return inValues;
        }

        #region 生成静态页面
        public static string getUrltoHtml(string Url)
        {
            string errorMsg = "";
            try
            {
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                // Get the response instance. 
                System.Net.WebResponse wResp = wReq.GetResponse();
                // Read an HTTP-specific property 
                //if (wResp.GetType() ==HttpWebResponse) 
                //{ 
                //DateTime updated =((System.Net.HttpWebResponse)wResp).LastModified; 
                //} 
                // Get the response stream. 
                System.IO.Stream respStream = wResp.GetResponseStream();
                // Dim reader As StreamReader = New StreamReader(respStream) 
                System.IO.StreamReader reader = new System.IO.StreamReader(respStream, System.Text.Encoding.GetEncoding("gb2312"));
                return reader.ReadToEnd();
            }
            catch (System.Exception ex)
            {
                errorMsg = ex.Message;
            }
            return "";
        }

        /// <summary>
        /// 生成静态页面
        /// </summary>
        /// <param name="sourceUrl">待生成的网页路径</param>
        /// <param name="saveFiles">生成的静态文件 Server.MapPath("/") + "index.html"</param>
        /// <returns></returns>
        public static bool SetHtmlByUrl(string sourceUrl, string saveFiles)
        {
            //根据Url地址生成静态页保持   
            Encoding code = Encoding.GetEncoding("utf-8");
            //Encoding code = Encoding.Default;
            StreamReader sr = null;
            StreamWriter sw = null;
            string sHtml = null;
            bool returnVal = false;
            //读取远程路径   
            //WebRequest temp = WebRequest.Create("http://127.0.0.1:6543/index.aspx");
            WebRequest wr = WebRequest.Create(sourceUrl);
            WebResponse myWr = wr.GetResponse();
            sr = new StreamReader(myWr.GetResponseStream(), code);
            //读取   
            try
            {
                sr = new StreamReader(myWr.GetResponseStream(), code);
                sHtml = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sr.Close();
            }
            //写入   
            try
            {
                sw = new StreamWriter(saveFiles, false, code);
                sw.Write(sHtml);
                sw.Flush();
                returnVal = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sw.Close();
            }
            return returnVal;
        }

        #endregion
        private const string StrRegex = @"\b(and|or)\b.{1,6}?(=|>|<|\bin\b|\blike\b)|/\*.+?\*/|<\s*script\b|\bEXEC\b|UNION.+?SELECT|UPDATE.+?SET|INSERT\s+INTO.+?VALUES|(SELECT|DELETE).+?FROM|(CREATE|ALTER|DROP|TRUNCATE)\s+(TABLE|DATABASE)";
        /// <summary>
        /// 防Sql注入
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool CheckData(string inputData)
        {
            if (Regex.IsMatch(inputData, StrRegex))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // *************************************************
        // 检验数字类型、日期类型、电子邮件的正则表达式
        // *************************************************
        /// <summary>
        /// 判断是否为数字型 ^\d+$ 
        /// </summary>
        /// <param name="strIn">要检测的字符串</param>
        /// <returns>返回是否数字</returns>
        public static bool IsNumeric(string strIn)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strIn, @"^[0-9]\d*[.]?\d*$");
        }
        /// <summary>
        /// 返回正整数
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsInteger(string strIn)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strIn, @"^\d+$");
        }
        /// <summary>
        /// 判断是否为日期型，日期格式：yyyy-MM-dd HH:mm:ss，从 1900-01-01 00:00:00 到 9999-12-31 23:59:59
        /// </summary>
        /// <param name="strIn">日期字符串</param>
        /// <returns>是否日期型</returns>
        public static bool IsDateTime(string strIn)
        {
            // Checks for the format yyyy-MM-dd HH:mm:ss also known as SortableDateTimePattern (conforms to ISO 8601) using local time. From 1900-01-01 00:00:00 to 9999-12-31 23:59:59.
            return System.Text.RegularExpressions.Regex.IsMatch(strIn, @"^(19[0-9]{2}|[2-9][0-9]{3})-((0(1|3|5|7|8)|10|12)-(0[1-9]|1[0-9]|2[0-9]|3[0-1])|(0(4|6|9)|11)-(0[1-9]|1[0-9]|2[0-9]|30)|(02)-(0[1-9]|1[0-9]|2[0-9]))\x20(0[0-9]|1[0-9]|2[0-3])(:[0-5][0-9]){2}$");
        }

        /// <summary>
        /// 判断是否为正确的电子邮件格式
        /// </summary>
        /// <param name="strIn">邮件地址</param>
        /// <returns>是否邮件地址格式</returns>
        public static bool IsValidEmail(string strIn)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 判断IPAddress为正确的IP格式
        /// </summary>
        /// <param name="strIn">IP地址</param>
        /// <returns>是否IP地址格式</returns>
        public static bool IsValidIPAddress(string strIn)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strIn, @"(((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))\.){3}((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))");
        }

        #region 身份证号信息操作
        /// <summary>
        /// 通过身份证号获取年龄
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static int GetAgeByID(string cid)
        {
            if (!string.IsNullOrEmpty(cid))
            {
                string uBirth = cid.Substring(6, 8).Insert(4, "-").Insert(7, "-");	//提取出生年月日
                TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(uBirth));
                return ts.Days / 365;
            }
            else { return 0; }
        }
        /// <summary>
        /// 通过身份证号获取生日
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static string GetBirthdayByID(string cid)
        {
            //性别代码为偶数是女性奇数为男性
            string returnVal = string.Empty;
            if (!string.IsNullOrEmpty(cid))
            {
                if (cid.Length == 15)
                {
                    returnVal = "19" + cid.Substring(6, 2) + "-" + cid.Substring(8, 2) + "-" + cid.Substring(10, 2);
                }
                else if (cid.Length == 18)
                {
                    returnVal = cid.Substring(6, 8).Insert(4, "-").Insert(7, "-");
                    //birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);
                }
                else
                {
                    returnVal = "";
                }
            }
            return returnVal;
        }
        /// <summary>
        /// 通过身份证号获取性别
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static string GetSexByID(string cid)
        {
            string returnVal = string.Empty;
            if (!string.IsNullOrEmpty(cid))
            {
                if (cid.Length == 15)
                {
                    returnVal = cid.Substring(12, 3);
                    if (int.Parse(returnVal) % 2 == 0) { returnVal = "女"; }
                    else { returnVal = "男"; }
                }
                else if (cid.Length == 18)
                {
                    returnVal = cid.Substring(14, 3);
                    if (int.Parse(returnVal) % 2 == 0) { returnVal = "女"; }
                    else { returnVal = "男"; }
                }
                else
                {
                    returnVal = "";
                }
            }

            return returnVal;
        }
        /// <summary>
        /// 通过身份证号获取性别和年龄
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static string GetSexAndAge(string cid,ref int age)
        {
            string returnVal = string.Empty;
            if (!string.IsNullOrEmpty(cid))
            {
                if (cid.Length == 15)
                {
                    returnVal = cid.Substring(12, 3);
                    if (int.Parse(returnVal) % 2 == 0) { returnVal = "女"; }
                    else { returnVal = "男"; }
                }
                else if (cid.Length == 18)
                {
                    returnVal = cid.Substring(14, 3);
                    if (int.Parse(returnVal) % 2 == 0) { returnVal = "女"; }
                    else { returnVal = "男"; }
                }
                else
                {
                    returnVal = "";
                } 
                if (!String.IsNullOrEmpty(returnVal))
                {
                    string uBirth = cid.Substring(6, 8).Insert(4, "-").Insert(7, "-");	//提取出生年月日
                    TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(uBirth));
                    age= ts.Days / 365;
                }
            }

            return returnVal;
        }
        /// <summary>
        /// 通过身份证号获取生日和性别
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="userSex"></param>
        /// <returns></returns>
        public static string GetBirthdayAndSex(string cid, ref string userSex)
        {
            string returnVal = string.Empty;
            if (!string.IsNullOrEmpty(cid))
            {
                if (cid.Length == 15)
                {
                    returnVal = "19" + cid.Substring(6, 2) + "-" + cid.Substring(8, 2) + "-" + cid.Substring(10, 2);
                    userSex = cid.Substring(12, 3);
                    if (int.Parse(userSex) % 2 == 0) { userSex = "女"; }
                    else { userSex = "男"; }
                }
                else if (cid.Length == 18)
                {
                    returnVal = cid.Substring(6, 8).Insert(4, "-").Insert(7, "-");
                    //birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);
                    userSex = cid.Substring(14, 3);
                    if (int.Parse(userSex) % 2 == 0) { userSex = "女"; }
                    else { userSex = "男"; }
                }
                else
                {
                    returnVal = "";
                    userSex = "";
                }
            }
            return returnVal;
        }
        #endregion

        /// <summary>
        /// /获取标签的值
        /// </summary>
        /// <param name="htmlParams">参数串</param>
        /// <param name="keyName">属性名</param>
        /// <returns>属性值</returns>
        public static string GetTagsValue(string htmlParams, string keyName)
        {
            // {AreWeb.AD code="0" location="0-a" type="0" width="980" height="80" alt="首页宣传广告一"/}
            string returnVa = string.Empty;
            string regTxt = string.Empty;
            if (!string.IsNullOrEmpty(htmlParams))
            {
                if (htmlParams.IndexOf("AreWeb.AD") > 0)
                {
                    //广告
                    regTxt = @"(?i){AreWeb.AD\s*((?<key>[^=]+)=""(?<value>[^""]+)"")+?\s*/}";
                }
                else if (htmlParams.IndexOf("AreWeb.Pic") > 0)
                {
                    //图文
                    regTxt = @"(?i){AreWeb.Pic\s*((?<key>[^=]+)=""(?<value>[^""]+)"")+?\s*/}";
                }
                else
                {
                }

                foreach (Match m in Regex.Matches(htmlParams, regTxt))
                {
                    if (m.Groups["key"].Value == keyName)
                    {
                        returnVa = m.Groups["value"].Value;
                        break;
                    }
                }
            }

            return returnVa;
        }



        /// <summary>
        /// 获取当前年、月、日、时、分、秒的时间字符串
        /// </summary>
        /// <param name="isHasMillisecond">是否返回毫秒值</param>
        /// <returns></returns>
        public static string GetCurDateTimeStr(bool isHasMillisecond)
        {
            string TmpStr = string.Empty;

            TmpStr += DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
            TmpStr += DateTime.Now.Month.ToString("D2", CultureInfo.InvariantCulture);
            TmpStr += DateTime.Now.Day.ToString("D2", CultureInfo.InvariantCulture);
            TmpStr += DateTime.Now.Hour.ToString("D2", CultureInfo.InvariantCulture);
            TmpStr += DateTime.Now.Minute.ToString("D2", CultureInfo.InvariantCulture);
            TmpStr += DateTime.Now.Second.ToString("D2", CultureInfo.InvariantCulture);

            if (isHasMillisecond)
            {
                TmpStr += DateTime.Now.Millisecond.ToString(CultureInfo.InvariantCulture);
            }

            return TmpStr;
        }
        /// <summary>
        /// 去输入字符两边的空格
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static string GetTrim(string strIn)
        {
            if (String.IsNullOrEmpty(strIn))
            {
                return strIn;
            }
            else
            {
                return strIn.Trim();
            }
        }

        public static string GetNumberTrim(string strIn)
        {
            if (String.IsNullOrEmpty(strIn))
            {
                return "0";
            }
            else
            {
                return strIn.Trim();
            }
        }

        /// <summary>
        /// 用户输入字符处理
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        public static string EncodeUserInput(string inStr)
        {
            inStr = inStr.Replace("<br>", "");
            inStr = inStr.Replace(">", "&gt;");
            inStr = inStr.Replace("<", "&lt;");
            inStr = inStr.Replace(" ", "&nbsp;");
            inStr = inStr.Replace("\"", "");
            inStr = inStr.Replace("\'", "");
            inStr = inStr.Replace("\n", "<br>");
            inStr = inStr.Replace("\r", "");

            return inStr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        public static string DecodeToInput(string inStr)
        {
            inStr = inStr.Replace("<br>", "\r\n");
            return inStr;
        }
        /// <summary>
        /// 判断奇偶数
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsEven(int i)
        {
            return (i & 1) == 0;
        }
    }
}
