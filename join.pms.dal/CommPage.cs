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
        /// �ַ�����
        /// </summary>
        /// <param name="inChar">�����ַ���</param>
        /// <param name="isExist">�Ƿ����</param>
        /// <returns>���ص��ַ�</returns>
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
                        inValues = inValues.Replace(aryKeys[i], "<font color=red> ���� </font>");
                    }
                }
            }

            return inValues;
        }
        /// <summary>
        /// �ж��ַ��Ƿ������ڸ�ʽ
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
        /// ͨ�÷���--�������״̬ 2014/10/30 by Ysl CheckAttribs("BIZ_Contents","Attribs","","",ref ddd);
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="atttibKey">״̬�ֶ���</param>
        /// <param name="filterSQL">��������</param>
        /// <param name="setVal">���õ�״ֵ̬</param>
        /// <param name="getVal">���ص�ͳһ״ֵ̬</param>
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
        /// ��⳷�������״̬
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
                // InitDirection --> 0,ǰ��;1,����
                // Attribs: 0,��ʼ�ύ;1,����� 2,ͨ�� 3,���� 4,ɾ�� 5,ע�� 9,�鵵
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
        /// ���ע�������״̬
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
                // Attribs: 0,��ʼ�ύ;1,����� 2,ͨ�� 3,���� 4,ɾ�� 5,ע�� 9,�鵵
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
        /// ��ȡ�޸Ĳ��ŵ��û�ID
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
                return "1"; // ���������͵�ϵͳ����Ա
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
        /// ��ȡ��ǰ�û���������Ϣ
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
        /// ��ȡ��ǰҵ�������
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
        /// �����޸�ʱ�Ǹ��ֶα��޸�
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
                            if (updateVal[i].Trim() != sdr[i].ToString().Trim()) reVal += aryPisTitle[i] + "��[ " + sdr[i].ToString() + " ]�޸�Ϊ[ " + updateVal[i] + " ]��";
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

        #region  ��ȡ�����ļ�����

        /// <summary>
        /// �������ݼ�
        /// </summary>
        private void ConfigDataSet()
        {
            m_Ds = new DataSet();
            m_Ds.Locale = System.Globalization.CultureInfo.InvariantCulture;
        }
        /// <summary>
        /// ��ȡ�����ļ�����
        /// </summary>
        /// <param name="funcNo">���ܺ�</param>
        /// <param name="configFile">�����ļ�·��</param>
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
                errorMsg = "��ȡ�����ļ�����ʧ�ܣ�" + ex.Message;
                return false;
            }
        }
        #endregion

        /// <summary>
        /// ����ǰ���Լ��
        /// </summary>
        /// <param name="objID">ѡ�е�ID</param>
        /// <param name="?">��ǰѡ�е�����ֵ</param>
        public static bool CheckPubFlag(string objID, ref string curFlag)
        {
            // 0 Ĭ��;1 ���; 3 ����; 4 ɾ��; 9 ����
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
        /// ���ǰ���
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="curFlag"></param>
        /// <returns></returns>
        public static bool CheckAuditFlag(string objID, ref string curFlag)
        {
            // 0 Ĭ��;1 ���; 3 ����; 4 ɾ��; 9 ����; 2�޸�
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
            // 0 Ĭ��;1 ���; 3 ����; 4 ɾ��; 9 ����; 2�޸�
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
        /// ɾ��ǰ���
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        public static bool CheckDelFlag(string objID)
        {
            // 0 Ĭ��;1 ���;2,�޸�  3 ����; 4 ɾ��; 9 ����
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
        /// ɾ��ǰ���
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        public static bool CheckDelFlag(string objID, string tableName, string keyName)
        {
            // 0 Ĭ��;1 ���;2,�޸�  3 ����; 4 ɾ��; 9 ����
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
        /// ��ȡ�û��ɲ����Ĺ���Ȩ��
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
        /// ��ȡ�û�ҵ��Ȩ��
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
        /// ��ȡ�û�ҵ��Ȩ��
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
        /// ���ݵ�ַ�����ȡ��ַ����
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
        /// ��ȡ��Ϊ�µ���Ա��ʶSELECT DeptCode FROM USER_BaseInfo WHERE UserID=1
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
        /// ��ȡ��������
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
        /// ��ȡ�������뼰��������
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
        /// ��ȡ�û���Ϣ 
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
        /// ��ȡ��Ա�Ľ�ɫ��������Ϣ
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
        /// ��ȡ��������
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
        /// ��ȡ�������Ƽ��������
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
        /// ��ȡ�û���
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
        /// ��ȡ����ֵ
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
        /// ����ϵͳ��Ϣ
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
        /// ����ҵ����־
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
        /// ��ȡ������������
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
        /// ��ȡ������������
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
        /// ��ȡҵ��㼶����
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
        /// ��ȡ��������
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

        #region ����Ϣ�ӿ�

        /// <summary>
        /// ͨ����Ѷè����Ϣ
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
                int Status = 0;//������
                mobileNo = CommPage.replaceNUM(mobileNo.Replace("-", "").Replace("��", "").Replace(" ", "").Replace("��", "").Replace("13988888888", ""));

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
                            Status = 2;//�����ֻ��ŵ�ֱ�ӱ�ʾ���ͳɹ�
                        }
                        list.Add("INSERT INTO [SMS](CellNumber,SMSContent,Status) VALUES('" + aryTels[j] + "','" + msgBody + "'," + Status + ")");
                    }
                }
                else
                {
                    if (mobileNo.ToString().Length != 11 || mobileNo.Substring(0, 1) != "1")
                    {
                        Status = 2;//�����ֻ��ŵ�ֱ�ӱ�ʾ���ͳɹ�
                    }
                    list = new System.Collections.Generic.List<string>(1);
                    list.Add("INSERT INTO [SMS](CellNumber,SMSContent,Status) VALUES('" + mobileNo + "','" + msgBody + "'," + Status + ")");
                }
                try
                {
                    if (DbHelperSQL.ExecuteSqlTran(list) > 0) { returnMsg = "OK"; }
                    list = null;
                }
                catch (Exception ex) { returnMsg = "����ʧ�ܣ�" + ex; list = null; }
                list = null;
            }
            else
            {
                returnMsg = "����ʧ�ܣ���û�������ֻ����룻�޷����Ͷ�Ѷ��";
            }
            return returnMsg;
        }

        private static string m_UserTel = System.Configuration.ConfigurationManager.AppSettings["UserTel"];
        private static string m_IdentifyKey = System.Configuration.ConfigurationManager.AppSettings["IdentifyKey"];
        /// <summary>
        /// ���Ͷ���Ϣ
        /// </summary>
        /// <param name="mobileNo">�ֻ���</param>
        /// <param name="msgBody">��������</param>
        /// <returns></returns>
        public static string SendMsg(string mobileNo, string msgBody)
        {
            string returnMsg = string.Empty;
            string url = "http://vphone.xaonline.com/vphoneserver_co/sms.aspx";
            //Ҫ�ύ���ַ������ݡ�
            string postString = "action=send&ver=1.0&userid=" + m_UserTel + "&keys=" + m_IdentifyKey + "&called=" + mobileNo + "&content=" + msgBody + "";
            //��ʼ��WebClient
            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //���ַ���ת�����ֽ�����
            byte[] postData = Encoding.GetEncoding("gb18030").GetBytes(postString);
            //�ϴ����ݣ�����ҳ����ֽ�����
            byte[] responseData = webClient.UploadData(url, "POST", postData);
            //���صĽ��ֽ�����ת�����ַ���(HTML)
            string srcString = Encoding.GetEncoding("gb18030").GetString(responseData);

            if (srcString == "�ɹ�")
            {
                returnMsg = "OK";// Response.Write("OK");//15:20
            }
            else
            {
                returnMsg = srcString;
            }
            return returnMsg;
            /*
             //Ҫ�ύ����URL�ַ�����
string url="http://vphone.xaonline.com/vphoneserver_co/sms.aspx";
//Ҫ�ύ���ַ������ݡ�
string postString = "action=send&ver=1.0&userid=02912345678&keys=xxxxxxxxxxxxxxxxxxxxxxxx&called=02912345678&content=test";
//��ʼ��WebClient
System.Net.WebClient webClient = new System.Net.WebClient();
webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
//���ַ���ת�����ֽ�����
byte[] postData = Encoding.GetEncoding("gb18030").GetBytes(postString);
//�ϴ����ݣ�����ҳ����ֽ�����
byte[] responseData = webClient.UploadData(url, "POST", postData);
//���صĽ��ֽ�����ת�����ַ���(HTML)
string srcString = Encoding. GetEncoding("gb18030").GetString(responseData);
if(srcString=="�ɹ�")
{
	//�ɹ�
}
else
{
	//ʧ��
	Response.Write(srcString);
}

             */
        }

        /// <summary>
        /// ����userID���Ͷ���Ϣ
        /// </summary>
        /// <param name="userID">����û���ʹ��","�ŷָ�</param>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public static string SendMsg(string mobileNo, string msgBody, string userID)
        {
            string returnMsg = string.Empty;
            string url = "http://vphone.xaonline.com/vphoneserver_co/sms.aspx";
            // ��ȡ�ֻ����� http://vphone.xaonline.com/vphoneserver_co/sms.aspx
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

            //Ҫ�ύ���ַ������ݡ�
            string postString = "action=send&ver=1.0&userid=" + m_UserTel + "&keys=" + m_IdentifyKey + "&called=" + mobileNo + "&content=" + msgBody + "";
            //��ʼ��WebClient
            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //���ַ���ת�����ֽ�����
            byte[] postData = Encoding.GetEncoding("gb18030").GetBytes(postString);
            //�ϴ����ݣ�����ҳ����ֽ�����
            byte[] responseData = webClient.UploadData(url, "POST", postData);
            //���صĽ��ֽ�����ת�����ַ���(HTML)
            string srcString = Encoding.GetEncoding("gb18030").GetString(responseData);

            if (srcString == "�ɹ�")
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

        #region ��ȡ8λ
        /// <summary>
        /// ��ȡ8λ����ϵͳ�����,����8λǰ��0
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

        #region �ж��Ƿ��ύ������
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PersonCidA">�����֤��</param>
        /// <param name="PersonCidB">Ů���֤��</param>
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
0101	һ�������Ǽ� һֱ��Ч
0102	���������Ǽ� �����
0107	һ���� 3�º�
0108	������Ů��ĸ����֤ 3�º�
0109	�������˿ڻ���֤���� 3�º�
0110	�������֤�� 3�º�
0111	��ֹ������� 3�º�  FinalDate
             */
            //ĸ�ӽ����ֲ᲻������ֱ�ӷ��أ���Ϊ����Ƕ�̥��ʱͬʱ���ܰ���౾֤��
            if (bizCode == "0150") { returnVal = false; }

            string sqlParams = "SELECT BizID,AttribsCN,Attribs,FinalDate FROM v_BizList WHERE " + strSql;
            // BIZ_Contents Attribs:  Attribs: 0,��ʼ�ύ;1,����� 2,ͨ�� 3,���� 4,���� 5,ע�� 6,�ȴ����,9,�鵵
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
                            //����֤�����ظ�������������
                            //if (bizCode == "0101")
                            //{
                            //    if (finalDate.AddYears(1) < DateTime.Now)
                            //    {
                            //        returnVal = true;
                            //        msg = "������ʾ��һ����������Ӱ��֮����һ����,��ֹ�ظ�����";
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
                                    msg = "������ʾ�����������Ǽǰ���ҵ��Ӱ��֮����������,��ֹ�ظ�����";
                                    break;
                                }
                            }
                            else if (bizCode == "0107")
                            {
                                if (finalDate.AddMonths(3) < DateTime.Now)
                                {
                                    returnVal = true;
                                    msg = "������ʾ��һ����ҵ��Ӱ��֮������������,��ֹ�ظ�����";
                                    break;
                                }
                            }
                            else if (bizCode == "0108")
                            {
                                if (finalDate.AddMonths(3) < DateTime.Now)
                                {
                                    returnVal = true;
                                    msg = "������ʾ��������Ů��ĸ����֤�Ӱ��֮������������,��ֹ�ظ�����";
                                    break;
                                }
                            }
                            else if (bizCode == "0109")
                            {
                                if (finalDate.AddMonths(3) < DateTime.Now)
                                {
                                    returnVal = true;
                                    msg = "������ʾ�������˿ڻ���֤���Ӱ��֮������������,��ֹ�ظ�����";
                                    break;
                                }
                            }
                            else if (bizCode == "0110")
                            {
                                if (finalDate.AddMonths(3) < DateTime.Now)
                                {
                                    returnVal = true;
                                    msg = "������ʾ���������֤���Ӱ��֮������������,��ֹ�ظ�����";
                                    break;
                                }
                            }
                            else if (bizCode == "0111")
                            {
                                if (finalDate.AddMonths(3) < DateTime.Now)
                                {
                                    returnVal = true;
                                    msg = "������ʾ����ֹ������˴Ӱ��֮������������,��ֹ�ظ�����";
                                    break;
                                }
                            }
                            else { }
                        }
                        else
                        {
                            returnVal = true;
                            msg = "������ʾ�����ύ����Ϣ����[ ��" + sdr["AttribsCN"].ToString() + "�� ]״̬,��ֹ�ظ�����";
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
                msg = "������ʾ������ʧ�ܣ�����ϵϵͳ����Ա��";
            }

            return returnVal;
        }
        #endregion

        #region �ж�ѡ�������Ƿ񵽴�
        /// <summary>
        /// 
        /// </summary>
        /// <param name="areacode"></param>
        /// <param name="type">0,���壻1,����</param>
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

        #region ���Ե�½������¼
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
        /// �жϵ�½����������5�ν�ֹ��½
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

        #region �ж��������Ƿ����ظ�������
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

        #region ����ת��
        public static string replaceNUM(string STR)
        {
            if (!string.IsNullOrEmpty(STR))
            {
                string nums = "��,��,��,��,��,��,��,��,��,��";

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
       #region ��ѡ���Ƿ�ѡ��
        public static bool IsCheckBox(string DataValue, string CheckValue)
        {
            bool returnVal = false;
            if (DataValue.IndexOf(CheckValue) > -1) {
                returnVal = true;
            }
            return returnVal;
        }
        #endregion
        #region �жϷ���֤���Ƿ���ڻ��߱�����ҵ����ʹ��
        public static string QcfwzBm_fun(string STR)
        {
            string returnstr = "";
            if (!String.IsNullOrEmpty(STR))
            {
                if (String.IsNullOrEmpty(CommPage.GetSingleVal(" select top 1  QcfwzBm from BIZ_Contents  WHERE BizCode='0150' and QcfwzBm='" + STR + "' and  Attribs IN(2,8,9)  order by BizID desc "))) { returnstr = "ĸ�ӽ����ֲ�֤�Ų����ڣ�\\n"; }
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
                                //����Գ�����ע����ҵ���з���֤��
                                DbHelperSQL.ExecuteSql("UPDATE BIZ_Contents SET QcfwzBm='D'+QcfwzBm WHERE BizID=" + s_BizID);
                            }
                            else
                            {
                                if (s_BAttribs == "0" || s_BAttribs == "1" || s_BAttribs == "3" || s_BAttribs == "6")
                                {
                                    s_BAttribs = "����˵�";
                                }
                                else
                                {
                                    s_BAttribs = "��˵�";
                                }
                                dtApp = null;
                                returnstr = "ĸ�ӽ����ֲ�֤���Ѿ������Ϊ" + s_BizID + "��״̬Ϊ" + s_BizName + "ҵ��ռ�ã�\\n";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    dtApp = null;
                }
            }
            else { returnstr = "ĸ�ӽ����ֲ�֤�Ų���Ϊ�գ�\\n"; }
            return returnstr;
        }
             #endregion 
    }
}
