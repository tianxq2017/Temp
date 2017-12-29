using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using UNV.Comm.DataBase;

namespace join.pms.dal
{
    public class CommPage
    {
        private string m_SqlParams;
        private DataSet m_Ds;
        private DataTable m_Dt;

        private string m_FuncInfo;
        private string m_Titles;
        private string m_Fields;

        private static string m_FilterChar = System.Configuration.ConfigurationManager.AppSettings["SiteFilter"];
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
                string[] aryKeys = m_FilterChar.ToLower().Split(',');
                for (int i = 0; i < aryKeys.Length; i++)
                {
                    if (inValues.ToUpper().IndexOf(aryKeys[i]) > -1)
                    {
                        isExist = true;
                        //break;
                        inValues = inValues.Replace(aryKeys[i], "<font color=red> □□ </font>");
                    }
                }
            }

            return inValues;
        }
        /// <summary>
        /// 判断字符是否是日期格式
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static bool IsDate(string strDate)
        {
            try
            {
                DateTime.Parse(strDate);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 通用方法--检测数据状态 2014/10/30 by Ysl CheckAttribs("BIZ_Contents","Attribs","","",ref ddd);
        /// </summary>
        /// <param name="tableName">表明</param>
        /// <param name="atttibKey">状态字段名</param>
        /// <param name="filterSQL">过滤条件</param>
        /// <param name="setVal">设置的状态值</param>
        /// <param name="getVal">返回的统一状态值</param>
        /// <returns></returns>
        public static bool CheckAttribs(string tableName, string atttibKey, string filterSQL, string setVal, ref string getVal)
        {
            string sqlParams = string.Empty;
            string firstFlag = string.Empty;
            string currentFlag = string.Empty;
            bool isChecked = true;
            SqlDataReader sdr = null;
            int i = 0;
            try
            {
                sqlParams = "SELECT " + atttibKey + " FROM " + tableName + " WHERE " + filterSQL;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    i++;
                    currentFlag = sdr[0].ToString();
                    if (i == 1) firstFlag = sdr[0].ToString();
                    if (currentFlag != firstFlag)
                    {
                        isChecked = false;
                        break;
                    }
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            getVal = firstFlag;

            return isChecked;
        }
        /// <summary>
        /// 检测撤销的许可状态
        /// </summary>
        /// <param name="filterSQL"></param>
        /// <returns></returns>
        public static bool CheckBizDelAttribs(string filterSQL)
        {
            string sqlParams = string.Empty;
            string currentFlag = string.Empty;
            string InitDirec = string.Empty;
            bool isChecked = true;
            SqlDataReader sdr = null;
            int i = 0;
            try
            {
                // InitDirection --> 0,前向;1,后向
                // Attribs: 0,初始提交;1,审核中 2,通过 3,补正 4,删除 5,注销 9,归档
                sqlParams = "SELECT Attribs,InitDirection FROM BIZ_Contents WHERE " + filterSQL;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    i++;
                    currentFlag = sdr[0].ToString();
                    InitDirec = sdr[1].ToString();
                    if (currentFlag == "9" || currentFlag == "2" || currentFlag == "1")
                    {
                        isChecked = false;
                        break;
                    }
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            return isChecked;
        }

        /// <summary>
        /// 检测注销的许可状态
        /// </summary>
        /// <param name="filterSQL"></param>
        /// <returns></returns>
        public static bool CheckLogOffAttribs(string filterSQL)
        {
            string sqlParams = string.Empty;
            string currentFlag = string.Empty;
            bool isChecked = true;
            SqlDataReader sdr = null;
            int i = 0;
            try
            {
                // Attribs: 0,初始提交;1,审核中 2,通过 3,补正 4,删除 5,注销 9,归档
                sqlParams = "SELECT Attribs FROM BIZ_Contents WHERE " + filterSQL;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    i++;
                    currentFlag = sdr[0].ToString();
                    if (currentFlag == "0" || currentFlag == "1" || currentFlag == "3" || currentFlag == "4")
                    {
                        isChecked = false;
                        break;
                    }
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            return isChecked;
        }

        /// <summary>
        /// 获取修改部门的用户ID
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public static string GetTargetUser(string funcCode)
        {
            try
            {
                funcCode = funcCode.Substring(0, 4);
                string deptCode = string.Empty;
                if (funcCode.Substring(0, 2) == "06") { deptCode = "02" + funcCode.Substring(2); }
                else { deptCode = funcCode; }
                return DbHelperSQL.GetSingle("SELECT TOP 1 UserID FROM USER_BaseInfo WHERE DeptCode='" + deptCode + "'").ToString();
            }
            catch
            {
                return "1"; // 不存在则发送到系统管理员
            }
        }

        public static string GetSingleValue(string sqlParams)
        {
            try
            {
                return DbHelperSQL.GetSingle(sqlParams).ToString();
            }
            catch { return ""; }
        }

        /// <summary>
        /// 获取当前用户的区域信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        public static string GeUserAreaInfo(string userID, ref string areaCode)
        {
            string returnVa = string.Empty;
            string sqlParams = "SELECT UserAreaCode,UserAreaName FROM USER_BaseInfo WHERE UserID=" + userID;
            SqlDataReader sdr = null;
            int i = 0;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    areaCode = sdr[0].ToString();
                    returnVa = sdr[1].ToString();
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            return returnVa;
        }

        /// <summary>
        /// 获取当前业务的流程
        /// </summary>
        /// <param name="bizCode"></param>
        /// <param name="stepNames"></param>
        /// <returns></returns>
        public static string GeBizSteps(string bizCode, ref string stepNames)
        {
            string returnVa = string.Empty;
            string sqlParams = "SELECT BizStep,BizStepNames FROM BIZ_Categories WHERE BizStep>1 AND BizCode=" + bizCode;
            SqlDataReader sdr = null;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    returnVa = sdr[0].ToString();
                    stepNames = sdr[1].ToString();
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            return returnVa;
        }

        /// <summary>
        /// 分析修改时那个字段被修改
        /// </summary>
        /// <param name="funcNo"></param>
        /// <param name="pisID"></param>
        /// <param name="aryPisTitle"></param>
        /// <param name="updateVal"></param>
        /// <returns></returns>
        public string AnalysisFields(string funcNo, string pisID, string configFile, string[] updateVal)
        {
            string reVal = string.Empty;
            string sqlParams = string.Empty;
            SqlDataReader sdr = null;
            try
            {
                GetConfigParams(funcNo, configFile, ref reVal);
                if (string.IsNullOrEmpty(reVal))
                {
                    string[] aryPisTitle = m_Titles.Split(',');
                    sqlParams = "SELECT Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19,Fileds20,Fileds21,Fileds22,Fileds23,Fileds24,Fileds25 FROM [PIS_BaseInfo] WHERE CommID=" + pisID;
                    sdr = DbHelperSQL.ExecuteReader(sqlParams);
                    while (sdr.Read())
                    {
                        for (int i = 0; i < aryPisTitle.Length - 3; i++)
                        {
                            if (updateVal[i].Trim() != sdr[i].ToString().Trim()) reVal += aryPisTitle[i] + "由[ " + sdr[i].ToString() + " ]修改为[ " + updateVal[i] + " ]；";
                        }
                    }
                    sdr.Close();
                }
            }
            catch { if (sdr != null) sdr.Close(); }

            return reVal;
        }

        public static string GetAreaMatch(string areaNo, string areaVal, string funcNo, string areaName)
        {
            string returnVal = "1=2";
            string[] aryNo = areaNo.Split(',');
            string[] aryVal = areaVal.Split(',');
            if (areaName.Length > 2) areaName = areaName.Substring(0, 2);
            for (int i = 0; i < aryNo.Length; i++)
            {
                if (funcNo == aryNo[i].Trim())
                {
                    returnVal = aryVal[i].Trim() + " LIKE '%" + areaName + "%'";
                    break;
                }
            }
            return returnVal;
        }

        #region  获取配置文件参数

        /// <summary>
        /// 配置数据集
        /// </summary>
        private void ConfigDataSet()
        {
            m_Ds = new DataSet();
            m_Ds.Locale = System.Globalization.CultureInfo.InvariantCulture;
        }
        /// <summary>
        /// 获取配置文件参数
        /// </summary>
        /// <param name="funcNo">功能号</param>
        /// <param name="configFile">配置文件路径</param>
        /// <returns></returns>
        public bool GetConfigParams(string funcNo, string configFile, ref string errorMsg)
        {
            try
            {
                ConfigDataSet();

                m_Ds.ReadXml(configFile, XmlReadMode.ReadSchema);
                DataRow[] dr = m_Ds.Tables[0].Select("FuncNo='" + funcNo + "'");

                this.m_FuncInfo = dr[0][1].ToString();
                this.m_Titles = dr[0][2].ToString();
                this.m_Fields = dr[0][3].ToString();

                m_Ds = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = "获取配置文件参数失败：" + ex.Message;
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 公开前属性检测
        /// </summary>
        /// <param name="objID">选中的ID</param>
        /// <param name="?">当前选中的属性值</param>
        public static bool CheckPubFlag(string objID, ref string curFlag)
        {
            // 0 默认;1 审核; 3 屏蔽; 4 删除; 9 公开
            string auditFlag = string.Empty;
            string sqlParams = string.Empty;
            bool isChecked = true;
            SqlDataReader sdr = null;
            int i = 0;
            try
            {
                sqlParams = "SELECT AuditFlag FROM [PIS_BaseInfo] WHERE CommID IN(" + objID + ")";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    i++;
                    auditFlag = sdr[0].ToString();
                    if (i == 1) curFlag = auditFlag;
                    if (auditFlag != "1" && auditFlag != "9") { isChecked = false; break; }
                    if (auditFlag != curFlag) { isChecked = false; break; }
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            if (curFlag == "1") { curFlag = "9"; } else { curFlag = "1"; }

            return isChecked;
        }

        /// <summary>
        /// 审核前检测
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="curFlag"></param>
        /// <returns></returns>
        public static bool CheckAuditFlag(string objID, ref string curFlag)
        {
            // 0 默认;1 审核; 3 屏蔽; 4 删除; 9 公开; 2修改
            string auditFlag = string.Empty;
            string sqlParams = string.Empty;
            bool isChecked = true;
            SqlDataReader sdr = null;
            int i = 0;
            try
            {
                sqlParams = "SELECT AuditFlag FROM [PIS_BaseInfo] WHERE CommID IN(" + objID + ")";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    i++;
                    auditFlag = sdr[0].ToString();
                    if (i == 1) curFlag = auditFlag;
                    if (auditFlag != "0" && auditFlag != "1" && auditFlag != "2") { isChecked = false; break; }
                    if (auditFlag != curFlag) { isChecked = false; break; }
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            if (curFlag == "0") { curFlag = "1"; } else { curFlag = "0"; }

            return isChecked;
        }
        public static bool CheckAuditFlag(string objID, string tableName, string keyName, ref string curFlag)
        {
            // 0 默认;1 审核; 3 屏蔽; 4 删除; 9 公开; 2修改
            string auditFlag = string.Empty;
            string sqlParams = string.Empty;
            bool isChecked = true;
            SqlDataReader sdr = null;
            int i = 0;
            try
            {
                sqlParams = "SELECT AuditFlag FROM [" + tableName + "] WHERE [" + keyName + "] IN(" + objID + ")";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    i++;
                    auditFlag = sdr[0].ToString();
                    if (i == 1) curFlag = auditFlag;
                    if (auditFlag != "0" && auditFlag != "1" && auditFlag != "2") { isChecked = false; break; }
                    if (auditFlag != curFlag) { isChecked = false; break; }
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            if (curFlag == "0") { curFlag = "1"; } else { curFlag = "0"; }

            return isChecked;
        }
        /// <summary>
        /// 删除前检测
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        public static bool CheckDelFlag(string objID)
        {
            // 0 默认;1 审核;2,修改  3 屏蔽; 4 删除; 9 公开
            string auditFlag = string.Empty;
            string sqlParams = string.Empty;
            bool isChecked = true;
            SqlDataReader sdr = null;
            int i = 0;
            try
            {
                sqlParams = "SELECT AuditFlag FROM [PIS_BaseInfo] WHERE CommID IN(" + objID + ")";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    auditFlag = sdr[0].ToString();
                    if (auditFlag == "1" || auditFlag == "2" || auditFlag == "9") { isChecked = false; break; }
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            return isChecked;
        }

        /// <summary>
        /// 删除前检测
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        public static bool CheckDelFlag(string objID, string tableName, string keyName)
        {
            // 0 默认;1 审核;2,修改  3 屏蔽; 4 删除; 9 公开
            string auditFlag = string.Empty;
            string sqlParams = string.Empty;
            bool isChecked = true;
            SqlDataReader sdr = null;
            int i = 0;
            try
            {
                sqlParams = "SELECT AuditFlag FROM [" + tableName + "] WHERE " + keyName + " IN(" + objID + ")";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    auditFlag = sdr[0].ToString();
                    if (auditFlag == "1" || auditFlag == "2" || auditFlag == "9") { isChecked = false; break; }
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            return isChecked;
        }
        /// <summary>
        /// 获取用户可操作的功能权限
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetUserPower(string userID)
        {
            string returnVa = string.Empty;
            m_SqlParams = "SELECT [FuncCode] FROM v_UserRolesPower WHERE UserID=" + userID;

            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            if (m_Dt.Rows.Count > 0)
            {
                for (int i = 0; i < m_Dt.Rows.Count; i++)
                {
                    returnVa += m_Dt.Rows[i][0].ToString() + ",";
                }

            }
            m_Dt = null;

            if (!String.IsNullOrEmpty(returnVa) && returnVa.IndexOf(",") > 0)
            {
                returnVa = returnVa.Substring(0, returnVa.Length - 1);
            }

            return returnVa;
        }

        /// <summary>
        /// 获取用户业务权限
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetUserBizPower(string userID)
        {
            string returnVa = string.Empty;
            string sqlParams = string.Empty;
            SqlDataReader sdr = null;
            try
            {
                sqlParams = "SELECT BizCode FROM v_UserBizPower WHERE UserID=" + userID;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        returnVa += sdr[0].ToString() + ",";
                    }
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            if (!String.IsNullOrEmpty(returnVa) && returnVa.IndexOf(",") > 0)
            {
                returnVa = returnVa.Substring(0, returnVa.Length - 1);
            }

            return returnVa;
        }

        /// <summary>
        /// 获取用户业务权限
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="bizNo"></param>
        /// <returns></returns>
        public static string GetUserBizPower(string userID, string bizNo)
        {
            string returnVa = string.Empty;
            string sqlParams = string.Empty;
            try
            {
                sqlParams = "SELECT BizPowers FROM v_UserBizPower WHERE UserID=" + userID + " AND BizCode='" + bizNo + "'";
                returnVa = DbHelperSQL.GetSingle(sqlParams).ToString();
            }
            catch { returnVa = "0,0,0,0,0,0,0,0,0"; }

            return returnVa;
        }

        /// <summary>
        /// 根据地址编码获取地址名称
        /// </summary>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        public string GetAreaNameByCode(string areaCode)
        {
            string returnVa = string.Empty;

            m_SqlParams = GetAreaSQL(areaCode);
            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            if (m_Dt.Rows.Count > 0)
            {
                for (int i = 0; i < m_Dt.Rows.Count; i++)
                {
                    returnVa += m_Dt.Rows[i][0].ToString();
                }
            }
            m_Dt = null;

            return returnVa;
        }

        private string GetAreaSQL(string areaCode)
        {
            string returnVa = string.Empty;
            if (areaCode.Substring(9) != "000")
            {
                returnVa = "SELECT AreaName FROM [AreaDetailCN] WHERE AreaCode='" + areaCode + "' OR AreaCode='" + areaCode.Substring(0, 9) + "000' OR AreaCode='" + areaCode.Substring(0, 6) + "000000' OR AreaCode='" + areaCode.Substring(0, 4) + "00000000' OR AreaCode='" + areaCode.Substring(0, 2) + "0000000000' ORDER BY AreaCode";
            }
            else if (areaCode.Substring(6, 3) != "000")
            {
                returnVa = "SELECT AreaName FROM [AreaDetailCN] WHERE AreaCode='" + areaCode + "' OR AreaCode='" + areaCode.Substring(0, 6) + "000000' OR AreaCode='" + areaCode.Substring(0, 4) + "00000000' OR AreaCode='" + areaCode.Substring(0, 2) + "0000000000' ORDER BY AreaCode";
            }
            else if (areaCode.Substring(4, 2) != "00")
            {
                returnVa = "SELECT AreaName FROM [AreaDetailCN] WHERE AreaCode='" + areaCode + "' OR AreaCode='" + areaCode.Substring(0, 4) + "00000000' OR AreaCode='" + areaCode.Substring(0, 2) + "0000000000' ORDER BY AreaCode";
            }
            else if (areaCode.Substring(2, 2) != "00")
            {
                returnVa = "SELECT AreaName FROM [AreaDetailCN] WHERE AreaCode='" + areaCode + "' OR AreaCode='" + areaCode.Substring(0, 2) + "0000000000' ORDER BY AreaCode";
            }
            else { returnVa = "SELECT AreaName FROM [AreaDetailCN] WHERE AreaCode='" + areaCode + "'"; }

            return returnVa;
        }

        /// <summary>
        /// 获取单为下的人员标识SELECT DeptCode FROM USER_BaseInfo WHERE UserID=1
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public static string GetUserByUnit(string deptCode)
        {
            string returnVa = string.Empty;
            string sqlParams = "SELECT [UserID] FROM [USER_BaseInfo] WHERE DeptCode=" + deptCode;

            DataTable dt = new DataTable();
            dt = DbHelperSQL.Query(sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    returnVa += dt.Rows[i][0].ToString() + ",";
                }

            }
            dt = null;

            if (!String.IsNullOrEmpty(returnVa) && returnVa.IndexOf(",") > 0)
            {
                returnVa = returnVa.Substring(0, returnVa.Length - 1);
            }

            return returnVa;
        }

        /// <summary>
        /// 获取机构编码
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public static string GetUnitCodeByUser(string userID)
        {
            string returnVa = string.Empty;
            string sqlParams = "SELECT DeptCode FROM USER_BaseInfo WHERE UserID=" + userID;

            try { returnVa = DbHelperSQL.GetSingle(sqlParams).ToString(); }
            catch { }

            return returnVa;
        }

        /// <summary>
        /// 获取机构编码及区划编码
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        public static string GetUnitCodeByUser(string userID, ref string areaCode)
        {
            string returnVa = string.Empty;
            string sqlParams = "SELECT DeptCode,UserAreaCode FROM USER_BaseInfo WHERE UserID=" + userID;
            // SELECT CommMemo FROM USER_Department WHERE DeptCode='0301' UserAreaName
            SqlDataReader sdr = null;
            int i = 0;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    returnVa = sdr[0].ToString();
                    areaCode = sdr[1].ToString();
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            return returnVa;
        }

        public static string GetUnitCodeByUser(string userID, ref string areaCode, ref string areaName)
        {
            string returnVa = string.Empty;
            string sqlParams = "SELECT DeptCode,UserAreaCode,UserAreaName FROM USER_BaseInfo WHERE UserID=" + userID;
            SqlDataReader sdr = null;
            int i = 0;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    returnVa = sdr[0].ToString();
                    areaCode = sdr[1].ToString();
                    areaName = sdr[2].ToString();
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            return returnVa;
        }

        /// <summary>
        /// 获取用户信息 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static bool GetUserInfoByID(string userID, ref string[] userInfo)
        {
            bool returnVal = false;
            string sqlParams = "SELECT RoleID,UserAccount,UserName,DeptCode,DeptName,UserUnitName,UserAreaCode,UserAreaName FROM v_UserList WHERE UserID=" + userID;
            SqlDataReader sdr = null;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    userInfo[0] = sdr[0].ToString();
                    userInfo[1] = sdr[1].ToString();
                    userInfo[2] = sdr[2].ToString();
                    userInfo[3] = sdr[3].ToString();
                    userInfo[4] = sdr[4].ToString();
                    userInfo[5] = sdr[5].ToString();
                    userInfo[6] = sdr[6].ToString();
                    userInfo[7] = sdr[7].ToString();
                }
                sdr.Close();

                returnVal = true;
            }
            catch { if (sdr != null) sdr.Close(); }

            return returnVal;
        }

        /// <summary>
        /// 获取人员的角色和区划信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        public static string GetUserRoleAndArea(string userID, ref string areaCode)
        {
            string returnVal = string.Empty;
            string sqlParams = "SELECT RoleID,UserAreaCode FROM v_UserList WHERE UserID=" + userID;
            SqlDataReader sdr = null;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    returnVal = sdr[0].ToString();
                    areaCode = sdr[1].ToString();
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            return returnVal;
        }

        /// <summary>
        /// 获取机构名称
        /// </summary>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public static string GetUnitNameByCode(string unitCode)
        {
            string returnVa = string.Empty;
            string sqlParams = "SELECT DeptName FROM USER_Department WHERE DeptCode='" + unitCode + "'";

            try { returnVa = DbHelperSQL.GetSingle(sqlParams).ToString(); }
            catch { }

            return returnVa;
        }

        /// <summary>
        /// 获取机构名称及镇办区划
        /// </summary>
        /// <param name="unitCode"></param>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        public static string GetUnitNameByCode(string unitCode, ref string areaCode)
        {
            string returnVa = string.Empty;
            string sqlParams = "SELECT DeptName,CommMemo FROM USER_Department WHERE DeptCode='" + unitCode + "'";
            // SELECT CommMemo FROM USER_Department WHERE DeptCode='0301'
            SqlDataReader sdr = null;
            int i = 0;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    returnVa = sdr[0].ToString();
                    areaCode = sdr[1].ToString();
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }

            return returnVa;
        }

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetUserName(string userID)
        {
            string returnVa = string.Empty;
            string sqlParams = "SELECT UserName FROM USER_BaseInfo WHERE UserID=" + userID;

            try { returnVa = DbHelperSQL.GetSingle(sqlParams).ToString(); }
            catch { }

            return returnVa;
        }

        public string GetFuncPower(string userID, string funcCode)
        {
            string returnVa = string.Empty;
            m_SqlParams = "SELECT [FuncCode],[FuncPowers] FROM v_UserRolesPower WHERE UserID=" + userID + " AND FuncCode='" + funcCode + "'";

            m_Dt = new DataTable();
            m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
            if (m_Dt.Rows.Count == 1)
            {
                returnVa += m_Dt.Rows[0][1].ToString();
            }
            m_Dt = null;

            return returnVa;
        }

        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public static string GetSingleVal(string sqlParams)
        {
            try
            {
                return DbHelperSQL.GetSingle(sqlParams).ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 插入系统消息
        /// </summary>
        /// <param name="msgTitle"></param>
        /// <param name="msgBody"></param>
        /// <param name="msgType"></param>
        /// <param name="sourceUserID"></param>
        /// <param name="targetUserIDs"></param>
        /// <returns></returns>
        public bool SetSysMsg(string msgTitle, string msgBody, string msgType, string sourceUserID, string targetUserIDs)
        {
            bool returnVa = false;

            try
            {
                System.Collections.Generic.List<string> list = null; ;
                string[] aryUsers = targetUserIDs.Split(',');
                list = new System.Collections.Generic.List<string>(aryUsers.Length + 2);
                for (int i = 0; i < aryUsers.Length; i++)
                {
                    list.Add("INSERT INTO [SYS_Msg]([SourceUserID], [TargetUserID], [MsgTitle], [MsgBody], [MsgType]) VALUES(" + sourceUserID + ", " + aryUsers[i] + ", '" + msgTitle + "', '" + msgBody + "', " + msgType + ")");
                }

                if (DbHelperSQL.ExecuteSqlTran(list) > 0)
                {
                    list = null;
                    returnVa = true;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message);
            }

            return returnVa;
        }

        /// <summary>
        /// 设置业务日志
        /// </summary>
        /// <param name="BizID"></param>
        /// <param name="userID"></param>
        /// <param name="oprateTitle"></param>
        /// <param name="oprateContents"></param>
        public static void SetBizLog(string bizID, string userID, string oprateTitle, string oprateContents)
        {
            try
            {
                string[] aryBiz = bizID.Split(',');
                System.Collections.Generic.List<string> list = null; ;
                list = new System.Collections.Generic.List<string>(aryBiz.Length);
                for (int i = 0; i < aryBiz.Length; i++)
                {
                    list.Add("INSERT INTO [BIZ_OprateLogs](BizID,OprateUserID,OprateTitle,OprateContents) VALUES(" + aryBiz[i] + ", " + userID + ", '" + oprateTitle + "', '" + oprateContents + "')");
                }

                DbHelperSQL.ExecuteSqlTran(list);
                list = null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message);
            }
        }

        // INSERT INTO BIZ_OprateLogs(BizID,OprateUserID,OprateTitle,OprateContents)
        /// <summary>
        /// 获取导航级别名称
        /// </summary>
        /// <param name="funcCode"></param>
        /// <returns></returns>
        public static string GetAllTreeName(string funcCode, bool isReal)
        {
            string sqlParams = string.Empty;
            string returnVa = string.Empty;
            if (funcCode.Length == 2)
            {
                sqlParams = "";
            }
            else if (funcCode.Length == 4)
            {
                sqlParams = "SELECT [FuncCode], [FuncName] FROM [SYS_Function] WHERE FuncCode ='" + funcCode.Substring(0, 2) + "' OR FuncCode ='" + funcCode + "' ORDER BY FuncCode";
            }
            else if (funcCode.Length == 6)
            {
                sqlParams = "SELECT [FuncCode], [FuncName] FROM [SYS_Function] WHERE FuncCode ='" + funcCode.Substring(0, 2) + "' OR FuncCode ='" + funcCode.Substring(0, 4) + "' OR FuncCode ='" + funcCode + "' ORDER BY FuncCode";
            }
            else
            {
                sqlParams = "SELECT [FuncCode], [FuncName] FROM [SYS_Function] WHERE FuncCode ='" + funcCode.Substring(0, 2) + "' OR FuncCode ='" + funcCode.Substring(0, 4) + "' OR FuncCode ='" + funcCode.Substring(0, 6) + "' OR FuncCode ='" + funcCode + "' ORDER BY FuncCode";
            }

            if (!String.IsNullOrEmpty(sqlParams))
            {
                DataTable dt = new DataTable();
                dt = DbHelperSQL.Query(sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    returnVa += dt.Rows[i][1].ToString() + " &gt; ";
                }
                dt = null;
            }
            return returnVa;
        }

        /// <summary>
        /// 获取导航级别名称
        /// </summary>
        /// <param name="funcCode"></param>
        /// <returns></returns>
        public static string GetAllTreeName(string funcCode)
        {
            string sqlParams = string.Empty;
            string returnVa = string.Empty;
            if (funcCode.Length == 2)
            {
                sqlParams = "";
            }
            else if (funcCode.Length == 4)
            {
                sqlParams = "SELECT [FuncCode], [FuncName] FROM [SYS_Function] WHERE FuncCode ='" + funcCode.Substring(0, 2) + "' ORDER BY FuncCode";
            }
            else if (funcCode.Length == 6)
            {
                sqlParams = "SELECT [FuncCode], [FuncName] FROM [SYS_Function] WHERE FuncCode ='" + funcCode.Substring(0, 2) + "' OR FuncCode ='" + funcCode.Substring(0, 4) + "' ORDER BY FuncCode";
            }
            else
            {
                sqlParams = "SELECT [FuncCode], [FuncName] FROM [SYS_Function] WHERE FuncCode ='" + funcCode.Substring(0, 2) + "' OR FuncCode ='" + funcCode.Substring(0, 4) + "' OR FuncCode ='" + funcCode.Substring(0, 6) + "' ORDER BY FuncCode";
            }

            if (!String.IsNullOrEmpty(sqlParams))
            {
                DataTable dt = new DataTable();
                dt = DbHelperSQL.Query(sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    returnVa += dt.Rows[i][1].ToString() + " &gt; ";
                }
                dt = null;
            }
            return returnVa;
        }

        /// <summary>
        /// 获取业务层级名称
        /// </summary>
        /// <param name="bizCode"></param>
        /// <returns></returns>
        public static string GetAllBizName(string bizCode)
        {
            string sqlParams = string.Empty;
            string returnVa = string.Empty;
            if (bizCode.Length == 2)
            {
                sqlParams = "";
            }
            else if (bizCode.Length == 4)
            {
                sqlParams = "SELECT BizCode,BizNameAB FROM BIZ_Categories WHERE BizCode ='" + bizCode.Substring(0, 2) + "' OR BizCode ='" + bizCode + "' ORDER BY BizCode";
            }
            else if (bizCode.Length == 6)
            {
                sqlParams = "SELECT BizCode,BizNameAB FROM BIZ_Categories WHERE BizCode ='" + bizCode.Substring(0, 2) + "' OR BizCode ='" + bizCode.Substring(0, 4) + "' OR BizCode ='" + bizCode + "' ORDER BY BizCode";
            }
            else
            {
                sqlParams = "SELECT BizCode,BizNameAB FROM BIZ_Categories WHERE BizCode ='" + bizCode.Substring(0, 2) + "' OR BizCode ='" + bizCode.Substring(0, 4) + "' OR BizCode ='" + bizCode.Substring(0, 6) + "' OR BizCode ='" + bizCode + "' ORDER BY BizCode";
            }

            if (!String.IsNullOrEmpty(sqlParams))
            {
                DataTable dt = new DataTable();
                dt = DbHelperSQL.Query(sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    returnVa += dt.Rows[i][1].ToString() + " &gt; ";
                }
                dt = null;
            }
            return returnVa;
        }



        /// <summary>
        /// 获取功能名称
        /// </summary>
        /// <param name="funcCode"></param>
        /// <returns></returns>
        public static string GetFuncName(string funcCode)
        {
            string sqlParams = string.Empty;
            string returnVa = string.Empty;
            if (!string.IsNullOrEmpty(funcCode) && funcCode.Length > 1)
            {
                sqlParams = "SELECT [FuncCode], [FuncName] FROM [SYS_Function] WHERE FuncCode ='" + funcCode + "' ORDER BY FuncCode";

                if (!String.IsNullOrEmpty(sqlParams))
                {
                    DataTable dt = new DataTable();
                    dt = DbHelperSQL.Query(sqlParams).Tables[0];
                    returnVa += dt.Rows[0][1].ToString();
                    dt = null;
                }
            }
            return returnVa;
        }

        #region 短信息接口

        /// <summary>
        /// 通过短讯猫发消息
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public static string SendMsgByModem(string mobileNo, string msgBody)
        {
            // return SendMsgByWebResponse(mobileNo, msgBody);
            string sqlParams = string.Empty;
            string returnMsg = string.Empty;
            string[] aryTels = null;
            if (!string.IsNullOrEmpty(mobileNo))
            {
                int Status = 0;//待发送
                mobileNo = CommPage.replaceNUM(mobileNo.Replace("-", "").Replace("—", "").Replace(" ", "").Replace("　", "").Replace("13988888888", ""));

                if (msgBody.Length > 500) msgBody = msgBody.Substring(0, 500) + "...";
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
                        list.Add("INSERT INTO [SMS](CellNumber,SMSContent,Status) VALUES('" + aryTels[j] + "','" + msgBody + "'," + Status + ")");
                    }
                }
                else
                {
                    if (mobileNo.ToString().Length != 11 || mobileNo.Substring(0, 1) != "1")
                    {
                        Status = 2;//不是手机号的直接表示发送成功
                    }
                    list = new System.Collections.Generic.List<string>(1);
                    list.Add("INSERT INTO [SMS](CellNumber,SMSContent,Status) VALUES('" + mobileNo + "','" + msgBody + "'," + Status + ")");
                }
                try
                {
                    if (DbHelperSQL.ExecuteSqlTran(list) > 0) { returnMsg = "OK"; }
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

        private static string m_UserTel = System.Configuration.ConfigurationManager.AppSettings["UserTel"];
        private static string m_IdentifyKey = System.Configuration.ConfigurationManager.AppSettings["IdentifyKey"];
        /// <summary>
        /// 发送短消息
        /// </summary>
        /// <param name="mobileNo">手机号</param>
        /// <param name="msgBody">短信内容</param>
        /// <returns></returns>
        public static string SendMsg(string mobileNo, string msgBody)
        {
            string returnMsg = string.Empty;
            string url = "http://vphone.xaonline.com/vphoneserver_co/sms.aspx";
            //要提交的字符串数据。
            string postString = "action=send&ver=1.0&userid=" + m_UserTel + "&keys=" + m_IdentifyKey + "&called=" + mobileNo + "&content=" + msgBody + "";
            //初始化WebClient
            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //将字符串转换成字节数组
            byte[] postData = Encoding.GetEncoding("gb18030").GetBytes(postString);
            //上传数据，返回页面的字节数组
            byte[] responseData = webClient.UploadData(url, "POST", postData);
            //返回的将字节数组转换成字符串(HTML)
            string srcString = Encoding.GetEncoding("gb18030").GetString(responseData);

            if (srcString == "成功")
            {
                returnMsg = "OK";// Response.Write("OK");//15:20
            }
            else
            {
                returnMsg = srcString;
            }
            return returnMsg;
            /*
             //要提交表单的URL字符串。
string url="http://vphone.xaonline.com/vphoneserver_co/sms.aspx";
//要提交的字符串数据。
string postString = "action=send&ver=1.0&userid=02912345678&keys=xxxxxxxxxxxxxxxxxxxxxxxx&called=02912345678&content=test";
//初始化WebClient
System.Net.WebClient webClient = new System.Net.WebClient();
webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
//将字符串转换成字节数组
byte[] postData = Encoding.GetEncoding("gb18030").GetBytes(postString);
//上传数据，返回页面的字节数组
byte[] responseData = webClient.UploadData(url, "POST", postData);
//返回的将字节数组转换成字符串(HTML)
string srcString = Encoding. GetEncoding("gb18030").GetString(responseData);
if(srcString=="成功")
{
	//成功
}
else
{
	//失败
	Response.Write(srcString);
}

             */
        }

        /// <summary>
        /// 根据userID发送短消息
        /// </summary>
        /// <param name="userID">多个用户可使用","号分隔</param>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public static string SendMsg(string mobileNo, string msgBody, string userID)
        {
            string returnMsg = string.Empty;
            string url = "http://vphone.xaonline.com/vphoneserver_co/sms.aspx";
            // 提取手机号码 http://vphone.xaonline.com/vphoneserver_co/sms.aspx
            DataTable tmpDt = new DataTable();
            try
            {
                tmpDt = DbHelperSQL.Query("SELECT UserMobile FROM USER_BaseInfo WHERE UserMobile Is Not Null AND UserID IN(" + userID + ")").Tables[0];
                for (int i = 0; i < tmpDt.Rows.Count; i++)
                {
                    if (i == tmpDt.Rows.Count - 1) { mobileNo += tmpDt.Rows[i][0].ToString(); }
                    else { mobileNo += tmpDt.Rows[i][0].ToString() + ","; }
                }
            }
            catch { }
            tmpDt = null;

            //要提交的字符串数据。
            string postString = "action=send&ver=1.0&userid=" + m_UserTel + "&keys=" + m_IdentifyKey + "&called=" + mobileNo + "&content=" + msgBody + "";
            //初始化WebClient
            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //将字符串转换成字节数组
            byte[] postData = Encoding.GetEncoding("gb18030").GetBytes(postString);
            //上传数据，返回页面的字节数组
            byte[] responseData = webClient.UploadData(url, "POST", postData);
            //返回的将字节数组转换成字符串(HTML)
            string srcString = Encoding.GetEncoding("gb18030").GetString(responseData);

            if (srcString == "成功")
            {
                returnMsg = "OK";// Response.Write("OK");//15:20
            }
            else
            {
                returnMsg = srcString;
            }
            return returnMsg;
        }
        #endregion

        #region 获取8位
        /// <summary>
        /// 获取8位，按系统编号走,不足8位前补0
        /// </summary>
        /// <returns></returns>
        public static string GetUserNo()
        {
            string count = GetSingleVal("SELECT COUNT(0) FROM BIZ_Persons");
            string returnVal = string.Empty;
            switch (count.Length)
            {
                case 1:
                    returnVal = "0000000" + count;
                    break;
                case 2:
                    returnVal = "000000" + count;
                    break;
                case 3:
                    returnVal = "00000" + count;
                    break;
                case 4:
                    returnVal = "0000" + count;
                    break;
                case 5:
                    returnVal = "000" + count;
                    break;
                case 6:
                    returnVal = "00" + count;
                    break;
                case 7:
                    returnVal = "0" + count;
                    break;
                case 8:
                    returnVal = count;
                    break;
                default:
                    returnVal = count;
                    break;
            }
            return "AreWeb" + returnVal;
        }
        #endregion

        #region 判断是否提交过申请
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PersonCidA">男身份证号</param>
        /// <param name="PersonCidB">女身份证号</param>
        /// <returns></returns>
        public static bool IsHasBiz(string bizCode, string PersonCidA, string PersonCidB, ref string msg)
        {
            bool returnVal = false;
            string Attribs = string.Empty;
            DateTime finalDate;
            SqlDataReader sdr = null;
            string strSql = " BizCode='" + bizCode + "'";
            if (!String.IsNullOrEmpty(PersonCidA)) { strSql += " AND PersonCidA='" + PersonCidA + "'"; }
            if (!String.IsNullOrEmpty(PersonCidB)) { strSql += " AND PersonCidB='" + PersonCidB + "'"; }
            /*
0101	一孩生育登记 一直有效
0102	二孩生育登记 两年后
0107	一杯奶 3月后
0108	独生子女父母光荣证 3月后
0109	《流动人口婚育证明》 3月后
0110	婚育情况证明 3月后
0111	终止妊娠审核 3月后  FinalDate
             */
            //母子健康手册不受限制直接返回，因为如果是多胎儿时同时可能办理多本证件
            if (bizCode == "0150") { returnVal = false; }

            string sqlParams = "SELECT BizID,AttribsCN,Attribs,FinalDate FROM v_BizList WHERE " + strSql;
            // BIZ_Contents Attribs:  Attribs: 0,初始提交;1,审核中 2,通过 3,补正 4,撤销 5,注销 6,等待审核,9,归档
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        Attribs = sdr["Attribs"].ToString();
                        if (Attribs == "0" || Attribs == "6")
                        { 
                            DbHelperSQL.ExecuteSql("UPDATE BIZ_Contents SET Attribs=4 WHERE BizID=" + sdr["BizID"].ToString()); 
                        }
                        else if (Attribs == "4" || Attribs == "5")
                        {
                            returnVal = false;
                        }
                        else if (Attribs == "2" || Attribs == "9")
                        {
                            finalDate = DateTime.Parse(sdr["FinalDate"].ToString());
                            //办结的证件，重复发起条件限制
                            //if (bizCode == "0101")
                            //{
                            //    if (finalDate.AddYears(1) < DateTime.Now)
                            //    {
                            //        returnVal = true;
                            //        msg = "操作提示：一孩生育申请从办结之日起一年内,禁止重复发起！";
                            //        break;
                            //    }
                            //    else { }
                            //}
                            //else 
                            if (bizCode == "0102" || bizCode == "0122")
                            {
                                if (finalDate.AddYears(2) < DateTime.Now)
                                {
                                    returnVal = true;
                                    msg = "操作提示：二孩生育登记办理业务从办结之日起两年内,禁止重复发起！";
                                    break;
                                }
                            }
                            else if (bizCode == "0107")
                            {
                                if (finalDate.AddMonths(3) < DateTime.Now)
                                {
                                    returnVal = true;
                                    msg = "操作提示：一杯奶业务从办结之日起三个月内,禁止重复发起！";
                                    break;
                                }
                            }
                            else if (bizCode == "0108")
                            {
                                if (finalDate.AddMonths(3) < DateTime.Now)
                                {
                                    returnVal = true;
                                    msg = "操作提示：独生子女父母光荣证从办结之日起三个月内,禁止重复发起！";
                                    break;
                                }
                            }
                            else if (bizCode == "0109")
                            {
                                if (finalDate.AddMonths(3) < DateTime.Now)
                                {
                                    returnVal = true;
                                    msg = "操作提示：流动人口婚育证明从办结之日起三个月内,禁止重复发起！";
                                    break;
                                }
                            }
                            else if (bizCode == "0110")
                            {
                                if (finalDate.AddMonths(3) < DateTime.Now)
                                {
                                    returnVal = true;
                                    msg = "操作提示：婚育情况证明从办结之日起三个月内,禁止重复发起！";
                                    break;
                                }
                            }
                            else if (bizCode == "0111")
                            {
                                if (finalDate.AddMonths(3) < DateTime.Now)
                                {
                                    returnVal = true;
                                    msg = "操作提示：终止妊娠审核从办结之日起三个月内,禁止重复发起！";
                                    break;
                                }
                            }
                            else { }
                        }
                        else
                        {
                            returnVal = true;
                            msg = "操作提示：您提交的信息处于[ “" + sdr["AttribsCN"].ToString() + "” ]状态,禁止重复发起！";
                            break;
                        }
                    }
                }
                sdr.Close(); sdr.Dispose();
            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                returnVal = false;
                msg = "操作提示：操作失败，请联系系统管理员！";
            }

            return returnVal;
        }
        #endregion

        #region 判断选择区划是否到村
        /// <summary>
        /// 
        /// </summary>
        /// <param name="areacode"></param>
        /// <param name="type">0,到村；1,到镇</param>
        /// <returns></returns>
        public static bool IsAreaCode(string areacode, string type)
        {
            bool returnVal = false;
            if (!String.IsNullOrEmpty(areacode))
            {
                if (type == "1") { if ((areacode.Substring(6) != "000" && areacode.Substring(9) == "000") || areacode=="900000000000") { returnVal = true; } }
                else { if (areacode.Substring(9) != "000" || areacode=="900000000000") { returnVal = true; } }
            }
            return returnVal;
        }
        #endregion

        #region 尝试登陆次数记录
        /// <summary>
        /// insert SYS_TryLogin
        /// </summary>
        /// <param name="LoginAcc"></param>
        /// <param name="LoginIP"></param>
        /// <param name="LoginStatus"></param>
        public static void SetSysTryLogin(string LoginAcc, string LoginIP, string LoginStatus)
        {
            try
            {
                DbHelperSQL.ExecuteSql("INSERT INTO SYS_TryLogin(LoginAcc,LoginIP,LoginDate,LoginStatus) VALUES('" + LoginAcc + "','" + LoginIP + "',getdate(),'" + LoginStatus + "')");
            }
            catch { }
        }
        /// <summary>
        /// 判断登陆次数，超过5次禁止登陆
        /// </summary>
        /// <param name="LoginAcc"></param>
        /// <param name="LoginIP"></param>
        /// <returns></returns>
        public static bool IsAllowLogin(string LoginAcc, string LoginIP)
        {
            try
            {
                string dateNow = DateTime.Now.ToString("yyyy-MM-dd");
                string count = CommPage.GetSingleVal("SELECT COUNT(0) FROM SYS_TryLogin WHERE LoginAcc='" + LoginAcc + "' AND LoginIP='" + LoginIP + "' AND LoginStatus>='" + dateNow + " 00:00:00' AND LoginStatus<'" + dateNow + " 23:59:59'").ToString();
                if (int.Parse(count) < 5) { return true; }
                else { return false; }
            }
            catch { return true; }
        }
        #endregion

        #region 判断数组中是否有重复的数据
        /// <summary>
        /// IsRepeat
        /// </summary>
        public static bool IsRepeat(string[] yourValue)
        {
            Hashtable ht = new Hashtable();
            for (int i = 0; i < yourValue.Length - 1; i++)
            {
                if (ht.Contains(yourValue[i]))
                {
                    return true;
                }
                else
                {
                    ht.Add(yourValue[i], yourValue[i]);
                }
            }
            return false;
        }
        #endregion

        #region 数字转换
        public static string replaceNUM(string STR)
        {
            if (!string.IsNullOrEmpty(STR))
            {
                string nums = "０,１,２,３,４,５,６,７,８,９";

                string[] aryKeys = nums.ToLower().Split(',');
                for (int i = 0; i < aryKeys.Length; i++)
                {
                    STR = STR.Replace(aryKeys[i], i.ToString());
                }
            }
            else
            {
                STR = "0";
            }
            return STR;
        }
        #endregion 
       #region 复选框是否选择
        public static bool IsCheckBox(string DataValue, string CheckValue)
        {
            bool returnVal = false;
            if (DataValue.IndexOf(CheckValue) > -1) {
                returnVal = true;
            }
            return returnVal;
        }
        #endregion
        #region 判断服务证号是否存在或者被其他业务以使用
        public static string QcfwzBm_fun(string STR)
        {
            string returnstr = "";
            if (!String.IsNullOrEmpty(STR))
            {
                if (String.IsNullOrEmpty(CommPage.GetSingleVal(" select top 1  QcfwzBm from BIZ_Contents  WHERE BizCode='0150' and QcfwzBm='" + STR + "' and  Attribs IN(2,8,9)  order by BizID desc "))) { returnstr = "母子健康手册证号不存在！\\n"; }
                else
                {
                    string s_BizName = "", s_BizID = "", s_BAttribs = "";
                    string sqlApp = " select top 1  BizName,BizID,Attribs from BIZ_Contents  WHERE BizCode in(0101,0102,0122) and QcfwzBm='" + STR + "'   order by BizID desc ";//and  Attribs IN(2,8,9)
                    DataTable dtApp = new DataTable();
                    try
                    {
                        dtApp = DbHelperSQL.Query(sqlApp).Tables[0];
                        if (dtApp.Rows.Count == 1)
                        {
                            s_BizName = dtApp.Rows[0]["BizName"].ToString();
                            s_BizID = dtApp.Rows[0]["BizID"].ToString();
                            s_BAttribs = dtApp.Rows[0]["BizID"].ToString();
                            if (s_BAttribs == "4" || s_BAttribs == "5")
                            {
                                //请空以撤销或注销的业务中服务证号
                                DbHelperSQL.ExecuteSql("UPDATE BIZ_Contents SET QcfwzBm='D'+QcfwzBm WHERE BizID=" + s_BizID);
                            }
                            else
                            {
                                if (s_BAttribs == "0" || s_BAttribs == "1" || s_BAttribs == "3" || s_BAttribs == "6")
                                {
                                    s_BAttribs = "待审核的";
                                }
                                else
                                {
                                    s_BAttribs = "审核的";
                                }
                                dtApp = null;
                                returnstr = "母子健康手册证号已经被编号为" + s_BizID + "，状态为" + s_BizName + "业务占用！\\n";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    dtApp = null;
                }
            }
            else { returnstr = "母子健康手册证号不能为空！\\n"; }
            return returnstr;
        }
             #endregion 
    }
}
