using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;
namespace join.pms.dal
{
    public class BIZ_Common
    {
        private static string m_SqlParams;
        private static string m_SvrUrl = ConfigurationManager.AppSettings["SvrUrl"];
        private static string m_FileExt = ConfigurationManager.AppSettings["FileExtension"];
        private static string m_SiteArea = System.Configuration.ConfigurationManager.AppSettings["SiteArea"];

        #region ��ȡָ���ֶ���Ϣ
        /// <summary>
        /// �ж��Ƿ�����û�
        /// </summary>
        /// <param name="objVal"></param>
        /// <param name="type">1���˻���2���֤�ţ�3�ֻ���</param>
        /// <returns></returns>
        public static bool GetPersonCount(string objVal, string type)
        {
            string strSql = string.Empty;
            if (type == "1") { strSql = " AND PersonAcc='" + objVal + "'"; }
            else if (type == "2") { strSql = " AND PersonCardID='" + objVal + "'"; }
            else if (type == "3") { strSql = " AND PersonTel='" + objVal + "'"; }

            if (!string.IsNullOrEmpty(objVal))
            {
                string sql = "SELECT COUNT(*) FROM BIZ_Persons WHERE 1=1 " + strSql;
                string reCount = string.Empty;
                try
                {
                    reCount = DbHelperSQL.GetSingle(sql).ToString();
                    if (reCount == "0") { return false; }
                    else { return true; }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            else { return true; }
        }
        /// <summary>
        /// ��ȡ��¼�û���Ϣ����ʽ���˻���������
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        public static string GetPersonFullName(string personID)
        {
            string returnVal = string.Empty;
            if (!string.IsNullOrEmpty(personID)) { returnVal = CommPage.GetSingleVal("SELECT PersonAcc+'('+PersonName+')' FROM BIZ_Persons WHERE PersonID=" + personID); }
            return returnVal;
        }
        /// <summary>
        /// ��ȡ��¼�û����֤����
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        public static string GetPersonCardID(string personID)
        {
            string returnVal = string.Empty;
            if (!string.IsNullOrEmpty(personID)) { returnVal = CommPage.GetSingleVal("SELECT PersonCardID FROM BIZ_Persons WHERE PersonID=" + personID); }
            return returnVal;
        }
        /// <summary>
        /// ��ȡ��¼�û����µ�ҵ������
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        public static string GetBizContentsFlowsID(string personID, string personCardID)
        {
            string returnVal = string.Empty;
            if (!string.IsNullOrEmpty(personID)) { returnVal = CommPage.GetSingleVal("SELECT BizID FROM BIZ_Contents WHERE (PersonID=" + personID + " AND InitDirection=0) OR PersonCidA='" + personCardID + "' OR PersonCidB='" + personCardID + "' ORDER BY BizID DESC"); }
            return returnVal;
        }
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="lit">�ؼ�</param>
        /// <param name="nations"></param>
        /// <param name="ctr"></param>
        public static void GetNations(Literal lit, string ctr, string nations)
        {
            if (String.IsNullOrEmpty(nations)) { nations = "�ɹ���"; }
            lit.Text = CustomerControls.CreateSelCtrl(ctr, "", nations, "", "SELECT MzName,MzName FROM SYS_Nations");
        }
        #endregion

        #region ����ҵ��ʱ����������

        /// <summary>
        /// ��ȡ��֤��λ�̶��绰
        /// </summary>
        /// <returns></returns>
        public static string GetUserTel(string areaCode)
        {
            string returnVal = string.Empty;
            if (!string.IsNullOrEmpty(areaCode)) { returnVal = CommPage.GetSingleVal("SELECT UserTel FROM v_UserList WHERE UserAreaCode=='" + areaCode + "'"); }
            return returnVal;
        }
        /// <summary>
        /// ��ȡ��ר���ֻ�����
        /// </summary>
        /// <param name="areaCode"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static string GetAreaMobile(string areaCode, ref string uName, ref string areaName)
        {
            SqlDataReader sdr = null;
            string userMobile = string.Empty;
            try
            {
                sdr = DbHelperSQL.ExecuteReader("SELECT TOP 1 UserName,UserTel,UserAreaName FROM USER_BaseInfo WHERE UserAreaCode='" + areaCode + "' AND ValidFlag=1");
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        userMobile = sdr["UserTel"].ToString();
                        areaName = sdr["UserAreaName"].ToString();
                        uName = sdr["UserName"].ToString();
                    }
                }
                sdr.Close(); sdr.Dispose();
            }
            catch { if (sdr != null) { sdr.Close(); sdr.Dispose(); } }
            return userMobile;
        }
        /// <summary>
        /// ҵ����ʱ��ר�ɷ��Ͷ���
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="type">0,����</param>
        public static void SendMsgToZG(string bizID, string bizName, string personName)
        {
            SqlDataReader sdr = null;
            string areaCode1 = "", userName1 = "", areaName1 = "", userTel1 = string.Empty;
            string areaCode2 = "", userName2 = "", areaName2 = "", userTel2 = string.Empty;
            string msg1 = "", msg2 = string.Empty;
            try
            {
                sdr = DbHelperSQL.ExecuteReader("SELECT top 2 AreaCode FROM dbo.BIZ_WorkFlows WHERE BizID=" + bizID + " AND SUBSTRING(AreaCode,9,3)!='000' AND Attribs=9 ORDER BY BizStep");
                if (sdr.HasRows)
                {
                    int i = 0;
                    while (sdr.Read())
                    {
                        if (i == 0) { areaCode1 = sdr["AreaCode"].ToString(); }
                        else { areaCode2 = sdr["AreaCode"].ToString(); }
                    }
                }
                sdr.Close(); sdr.Dispose();

                if (areaCode1 == areaCode2)
                {
                    userTel1 = BIZ_Common.GetAreaMobile(areaCode1, ref userName1, ref areaName1);
                    msg1 = userName1 + "ר�����ã�����" + userName1 + "�������[" + bizName + "]��������ʱ�����˲��������Ϣ������ʱ�ظ�������졣";
                    if (!string.IsNullOrEmpty(userTel1) && !string.IsNullOrEmpty(msg1)) { SendMsg.SendMsgByModem(userTel1, msg1); }
                }
                else
                {
                    userTel1 = BIZ_Common.GetAreaMobile(areaCode1, ref userName1, ref areaName1);
                    msg1 = userName1 + "ר�����ã�����" + userName1 + "�������[" + bizName + "]��������ʱ�����˲��������Ϣ������ʱ�ظ�������졣";
                    if (!string.IsNullOrEmpty(userTel1) && !string.IsNullOrEmpty(msg1)) { SendMsg.SendMsgByModem(userTel1, msg1); }

                    userTel2 = BIZ_Common.GetAreaMobile(areaCode2, ref userName2, ref areaName2);
                    msg2 = userName2 + "ר�����ã�����" + userName2 + "�������[" + bizName + "]��������ʱ�����˲��������Ϣ������ʱ�ظ�������졣";
                    if (!string.IsNullOrEmpty(userTel2) && !string.IsNullOrEmpty(msg2)) { SendMsg.SendMsgByModem(userTel2, msg2); }
               
                }

            }
            catch { if (sdr != null) { sdr.Close(); sdr.Dispose(); } }


        }
        /// <summary>
        /// ҵ����ʱ��Ⱥ�ڷ��Ͷ���
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="type">0,����</param>
        public static void SendMsgToPerson(string bizID, string type, bool IsSendZG)
        {
            SqlDataReader sdr = null;
            string userTel = "", userName = "", bizName = "", contactTelA = "", contactTelB = "", fileds01 = "", fileds08 = string.Empty;
            string msg = string.Empty;
            try
            {
                sdr = DbHelperSQL.ExecuteReader("SELECT BizName,ContactTelA,ContactTelB,Fileds01,Fileds08 FROM BIZ_Contents WHERE BizID=" + bizID);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        bizName = sdr["BizName"].ToString();
                        contactTelA = sdr["ContactTelA"].ToString();
                        contactTelB = sdr["ContactTelB"].ToString();
                        fileds01 = sdr["Fileds01"].ToString();
                        fileds08 = sdr["Fileds08"].ToString();
                    }
                }
                sdr.Close(); sdr.Dispose();

                if (!string.IsNullOrEmpty(contactTelB)) { userTel = contactTelB; userName = fileds08; }
                else { userTel = contactTelA; userName = fileds01; }
                // ��Ůʿ,���ύ��[ҵ������]�������������¼ƽ̨���û���Ϣ������鿴������ȡ�������������֡�(50��)
                // ���������ã�����ĳĳ����[ҵ������]���ύ����˲������Ϣ����ʱ��ˡ�
                if (type == "0") { msg = userName + "�����ύ��[" + bizName + "]�������������¼ƽ̨���û���Ϣ������鿴������ȡ�"; }
                if (!string.IsNullOrEmpty(userTel) && !string.IsNullOrEmpty(msg)) { SendMsg.SendMsgByModem(userTel, msg); }
            }
            catch { if (sdr != null) { sdr.Close(); sdr.Dispose(); } }

            if (IsSendZG) { SendMsgToZG(bizID, bizName,userName); }
        }

        /// <summary>
        /// ���ʹ�ר���ֻ���Ѷ
        /// </summary>
        /// <param name="bizName"></param>
        /// <param name="areaCode"></param>
        public static void SetMsgToAuditer(string bizName, string appUserName, string areaCode)
        {
            // 
            string msgBody = string.Empty;
            string uName = "", uTel = string.Empty;
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT UserName,UserTel FROM SYS_Sign WHERE UserAccount='+areaCode+'";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    uName = sdr["BizName"].ToString();
                    uTel = sdr["Fileds01"].ToString();

                    msgBody = uName + "���ã�����" + appUserName + "����[" + bizName + "]���ύ����˲������Ϣ����ʱ��ˡ�";
                    SendMsg.SendMsgByModem(uTel, msgBody);
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }
        }
        /// <summary>
        /// ��ȡ��ǰ��������
        /// </summary>
        /// <param name="areaCode"></param>
        /// <param name="type">0,ֻ��ȡ��ǰ�������ƣ�1,��ȡ��ǰ����ȫ���磺xxʡxx��xx��xx��xx����</param>
        /// <returns></returns>
        public static string GetAreaName(string areaCode, string type)
        {
            string returnVal = string.Empty;
            //if (type == "1") { returnVal = CommPage.GetSingleVal("SELECT (SELECT AreaName FROM AreaDetailCN WHERE AreaCode='" + areaCode.Substring(0, 2) + "0000000000')+(SELECT AreaName FROM AreaDetailCN WHERE AreaCode='" + areaCode.Substring(0, 4) + "00000000' AND AreaCode!='" + areaCode.Substring(0, 2) + "0000000000')+(SELECT AreaName FROM AreaDetailCN WHERE AreaCode='" + areaCode.Substring(0, 6) + "000000')+(SELECT AreaName FROM AreaDetailCN WHERE AreaCode='" + areaCode.Substring(0, 9) + "000')+AreaName FROM AreaDetailCN WHERE AreaCode='" + areaCode + "'"); }
            //else { returnVal = CommPage.GetSingleVal("SELECT AreaName FROM AreaDetailCN WHERE AreaCode='" + areaCode + "'"); }
            return returnVal;
        }
        /// <summary>
        /// ���븽����
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="userId"></param>
        /// <param name="cerName"></param>
        /// <param name="cerPath"></param>
        /// <param name="SysNo">0,Ⱥ�ڣ�1��̨</param>
        public static void InsetBizDocs(string objID, string userId, string cerName, string[] cerInfo, string SysNo)
        {
            //cerInfo={"DocsType","DocsPath","SourceName"}
            try
            {
                DbHelperSQL.ExecuteSql("INSERT INTO BIZ_Docs(BizID,PersonID,DocsName,DocsType,DocsPath,SourceName,SysNo) VALUES(" + objID + "," + userId + ",'" + cerName + "','" + cerInfo[0] + "','" + cerInfo[1] + "','" + cerInfo[2] + "'," + SysNo + ")");
            }
            catch { }
        }
        /// <summary>
        /// ���븽����
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="userId"></param>
        /// <param name="cerName"></param>
        /// <param name="cerPath"></param>
        public static void InsetBizDocs(string objID, string userId, string cerName, string[] cerInfo)
        {
            //cerInfo={"DocsType","DocsPath","SourceName"}
            try
            {
                DbHelperSQL.ExecuteSql("INSERT INTO BIZ_Docs(BizID,PersonID,DocsName,DocsType,DocsPath,SourceName) VALUES(" + objID + "," + userId + ",'" + cerName + "','" + cerInfo[0] + "','" + cerInfo[1] + "','" + cerInfo[2] + "')");
            }
            catch { }
        }
        /// <summary>
        /// ���븽����
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="cerName"></param>
        /// <param name="cerPath"></param>
        public static void InsetBizDocsAB(string objID, string PersonIDA, string PersonIDB, string cerName, string[] cerInfo)
        {
            //cerInfo={"DocsType","DocsPath","SourceName"}
            try
            {
                if (!string.IsNullOrEmpty(PersonIDA))
                {
                    DbHelperSQL.ExecuteSql("INSERT INTO BIZ_Docs(BizID,PersonID,DocsName,DocsType,DocsPath,SourceName) VALUES(" + objID + "," + PersonIDA + ",'" + cerName + "','" + cerInfo[0] + "','" + cerInfo[1] + "','" + cerInfo[2] + "')");
                }
                //if (!string.IsNullOrEmpty(PersonIDB))
                //{
                //    DbHelperSQL.ExecuteSql("INSERT INTO BIZ_Docs(BizID,PersonID,DocsName,DocsType,DocsPath,SourceName) VALUES(" + objID + "," + PersonIDB + ",'" + cerName + "','" + cerInfo[0] + "','" + cerInfo[1] + "','" + cerInfo[2] + "')");
                //}
            }
            catch { }
        }
        /// <summary>
        /// �޸ĸ�����
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="cerName"></param>
        /// <param name="cerPath"></param>
        public static void UpdateBizDocs(string objID, string PersonIDA, string PersonIDB, string cerName, string[] cerInfo)
        {
            //cerInfo={"DocsType","DocsPath","SourceName"}
            try
            {
                if (!string.IsNullOrEmpty(PersonIDA))
                {
                    DbHelperSQL.ExecuteSql("UPDATE BIZ_Docs SET DocsType='" + cerInfo[0] + "',DocsPath='" + cerInfo[1] + "',SourceName='" + cerInfo[2] + "' WHERE BizID=" + objID + " AND PersonID=" + PersonIDA + " AND DocsName='" + cerName + "");
                }
                //if (!string.IsNullOrEmpty(PersonIDB))
                //{
                //    DbHelperSQL.ExecuteSql("INSERT INTO BIZ_Docs(BizID,PersonID,DocsName,DocsType,DocsPath,SourceName) VALUES(" + objID + "," + PersonIDB + ",'" + cerName + "','" + cerInfo[0] + "','" + cerInfo[1] + "','" + cerInfo[2] + "')");
                //}
            }
            catch { }
        }
        /// <summary>
        /// �������̱�
        /// </summary>
        /// <param name="bizCode">ҵ�����ͱ���</param>
        /// <param name="bizStep">ҵ�����Ͳ���</param>
        /// <param name="bizID">ҵ����ID</param>
        /// <param name="areaCodeA">��</param>
        /// <param name="areaCodeB">Ů</param>
        /// <param name="areaCode">������������һ��������λ</param>
        public static string InsetBizWorkFlows(string bizCode, string bizStep, string bizID, string areaCodeA, string areaCodeB)
        {
            string returnVal = string.Empty;
            string areaCodeZhen = "", areaCodeXian = string.Empty;
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(int.Parse(bizStep));
            try
            {
                string SelAreaCode = DbHelperSQL.GetSingle("SELECT TOP 1 SelAreaCode FROM BIZ_Contents WHERE BizID=" + bizID).ToString();
                
                switch (bizCode)
                {
                    case "0150":
                        if (SelAreaCode.Substring(9) != "000")
                        {
                            //����Ǵ弶����������
                            SelAreaCode = SelAreaCode.Substring(0, 9) + "000";
                        }
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0101":
                        //Ů������ �л�����   ����λ��
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeB + "','" + GetAreaName(areaCodeB, "0") + "',GETDATE())");
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");
                        areaCodeZhen = areaCodeA.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "',GETDATE())");

                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0102":
                        //Ů�� �д� Ů�� ����  һ����֤����Ϊ ѡ���������� ��
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeB + "','" + GetAreaName(areaCodeB, "0") + "',GETDATE())");
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");

                        areaCodeZhen = areaCodeB.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "',GETDATE())");
                        //areaCodeZhen = areaCodeA.Substring(0, 9) + "000";
                        //list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",4,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "',GETDATE())");

                        ////һ����֤����Ϊ ѡ���������� ��
                        //areaCodeXian = areaCodeB.Substring(0, 9) + "000";
                        //list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",5,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "��������',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0103":
                        //Ů�� �д� Ů�� ���� Ů��
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeB + "','" + GetAreaName(areaCodeB, "0") + "',GETDATE())");
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");

                        areaCodeZhen = areaCodeB.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "',GETDATE())");
                        areaCodeZhen = areaCodeA.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",4,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "',GETDATE())");

                        //������������֤����Ϊ ѡ���������� ��
                        areaCodeXian = areaCodeB.Substring(0, 6) + "000000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",5,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "�˿ںͼƻ�������',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0104":
                        if (!string.IsNullOrEmpty(areaCodeB) && bizStep == "5")
                        {
                            //�����˴� ��ż�� �������� ��ż�� ��������
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeB + "','" + GetAreaName(areaCodeB, "0") + "',GETDATE())");
                            areaCodeZhen = areaCodeA.Substring(0, 9) + "000";
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "',GETDATE())");
                            areaCodeZhen = areaCodeB.Substring(0, 9) + "000";
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",4,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "',GETDATE())");
                            //��֤��λ���������ؼ�����
                            areaCodeXian = areaCodeA.Substring(0, 6) + "000000";
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",5,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "�˿ںͼƻ�������',GETDATE())");
                        }
                        else
                        { //�����˴� �������� ��������
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");
                            areaCodeZhen = areaCodeA.Substring(0, 9) + "000";
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "',GETDATE())");
                            //��֤��λ���������ؼ�����
                            areaCodeXian = areaCodeA.Substring(0, 6) + "000000";
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "�˿ںͼƻ�������',GETDATE())");
                        }
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0112":
                        //Ů�� Ů��
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");
                        areaCodeZhen = areaCodeA.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0122":
                        //Ů�� �д� Ů�� ����  һ����֤����Ϊ ѡ���������� ��
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeB + "','" + GetAreaName(areaCodeB, "0") + "',GETDATE())");
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");

                        areaCodeZhen = areaCodeB.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "',GETDATE())");
                        areaCodeZhen = areaCodeA.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",4,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "',GETDATE())");

                        ////һ����֤����Ϊ ѡ���������� ��
                        //areaCodeXian = areaCodeB.Substring(0, 9) + "000";
                        //list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",5,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "��������',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0131":
                        //��ǰ��飬������֤�����ذ�֤����Э�����ҵ��
                        if (SelAreaCode.Substring(9) != "000")
                        {
                            //����Ǵ弶��������������
                            SelAreaCode = SelAreaCode.Substring(0, 9) + "000";
                        }
                        areaCodeXian = SelAreaCode;
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "�����ͼƻ�������',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "AppA":
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + ",1,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    default:
                        returnVal = "δָ��ҵ������";
                        break;
                }
                list = null;
            }
            catch
            {
                returnVal = "";
                list = null;
            }
            return returnVal;
        }
       
        /// <summary>
        /// �������̱�-����
        /// </summary>
        /// <param name="bizCode">ҵ�����ͱ���</param>
        /// <param name="bizStep">ҵ�����Ͳ���</param>
        /// <param name="bizID">ҵ����ID</param>
        /// <param name="areaCodeA">��</param>
        /// <param name="areaCodeB">Ů</param>
        /// <param name="areaCode">������������һ��������λ</param>
        public static string InsetBizWorkFlows(string bizCode, string bizStep, string bizID, string areaCodeA, string areaCodeB, string areaCode)
        {
            string returnVal = string.Empty;
            string areaCodeZhen = "", areaCodeXian = string.Empty;
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(int.Parse(bizStep));
            try
            {
                string SelAreaCode = DbHelperSQL.GetSingle("SELECT TOP 1 SelAreaCode FROM BIZ_Contents WHERE BizID=" + bizID).ToString();
                
                switch (bizCode)
                {
                    case "0150":
                        if (SelAreaCode.Substring(9) != "000")
                        {
                            //����Ǵ弶����������
                            SelAreaCode = SelAreaCode.Substring(0, 9) + "000";
                        }
                        areaCodeZhen = SelAreaCode; 
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;

                    case "0101":
                        if (!string.IsNullOrEmpty(areaCodeB) && bizStep == "3")
                        {
                            // ���������ã�����ĳĳ����[ҵ������]���ύ����˲������Ϣ����ʱ��ˡ�
                            //Ů������ �л�����   ����λ��
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeB + "','" + GetAreaName(areaCodeB, "0") + "',GETDATE())");
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");
                            areaCodeZhen = areaCode.Substring(0, 9) + "000";
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "������',GETDATE())");
                        }
                        else
                        {
                            //Ů������  ����λ��
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");
                            areaCodeZhen = areaCode.Substring(0, 9) + "000";
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "������',GETDATE())");
                        }
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0102":
                        //Ů�� �д� Ů�� ����  һ����֤����Ϊ ѡ���������� ��
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeB + "','" + GetAreaName(areaCodeB, "0") + "',GETDATE())");
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");

                        areaCodeZhen = areaCode.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "������',GETDATE())");
                        //areaCodeZhen = areaCodeA.Substring(0, 9) + "000";
                        //list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",4,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "',GETDATE())");

                        //һ����֤����Ϊ ѡ���������� ��
                        //areaCodeXian = areaCode.Substring(0, 6) + "000000";
                        //list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",4,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "�˿ںͼƻ�������',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0103":
                        //�����˴� �������� ��������
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCode + "','" + GetAreaName(areaCode, "0") + "',GETDATE())");
                        areaCodeZhen = areaCode.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "������',GETDATE())");

                        areaCodeXian = areaCode.Substring(0, 6) + "000000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "�˿ںͼƻ�������',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0104":
                        //�����˴� �������� ��������
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCode + "','" + GetAreaName(areaCode, "0") + "',GETDATE())");
                        areaCodeZhen = areaCode.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "������',GETDATE())");

                        areaCodeXian = areaCode.Substring(0, 6) + "000000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "�˿ںͼƻ�������',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0105":
                        //�����˴� �������� ��������
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCode + "','" + GetAreaName(areaCode, "0") + "',GETDATE())");
                        areaCodeZhen = areaCode.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "������',GETDATE())");

                        areaCodeXian = areaCode.Substring(0, 6) + "000000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "�˿ںͼƻ�������',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0106":
                        //�����˴� ��ż�� �������� ��������
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeB + "','" + GetAreaName(areaCodeB, "0") + "',GETDATE())");

                        areaCodeZhen = areaCodeA.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "������',GETDATE())");
                        areaCodeXian = areaCodeA.Substring(0, 6) + "000000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",4,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "�˿ںͼƻ�������',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0107":
                        //Ů����  ѡ���� 
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeB + "','" + GetAreaName(areaCodeB, "0") + "',GETDATE())");
                        //list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");

                        areaCodeZhen = areaCode.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "������',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0108":
                        //Ů���� �з��� ѡ���� 
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeB + "','" + GetAreaName(areaCodeB, "0") + "',GETDATE())");
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");

                        areaCodeZhen = areaCode.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "������',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0109":
                        //Ů���� �з��� ѡ���� 
                        if (!string.IsNullOrEmpty(areaCodeB) && bizStep == "3")
                        {
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeB + "','" + GetAreaName(areaCodeB, "0") + "',GETDATE())");
                            areaCodeZhen = areaCode.Substring(0, 9) + "000";
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "������',GETDATE())");
                        }
                        else
                        {
                            //Ů������  ����λ��
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");
                            areaCodeZhen = areaCode.Substring(0, 9) + "000";
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "������',GETDATE())");
                        }
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0110":
                        //�����˴� �������� ��������
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCode + "','" + GetAreaName(areaCode, "0") + "',GETDATE())");
                        areaCodeZhen = areaCode.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "������',GETDATE())");

                        if (bizStep == "3")
                        {
                            areaCodeXian = areaCode.Substring(0, 6) + "000000";
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "�˿ںͼƻ�������',GETDATE())");
                        }
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0111":
                        //Ů�� Ů�� ���� ���� ������ ��
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCode + "','" + GetAreaName(areaCode, "0") + "',GETDATE())");
                        areaCodeZhen = areaCode.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "������',GETDATE())");

                        areaCodeXian = areaCode.Substring(0, 6) + "000000";
                        if (bizStep == "6")
                        {
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeZhen + "','ҽԺ���ﲿ',GETDATE())");
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",4,'" + areaCodeZhen + "','ҽԺ����',GETDATE())");
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",5,'" + areaCodeZhen + "','ҽԺ������',GETDATE())");
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",6,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "�˿ںͼƻ�������',GETDATE())");
                        }
                        else
                        {
                            list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "�˿ںͼƻ�������',GETDATE())");
                        }

                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0122":
                        //Ů�� �д� Ů�� ����  һ����֤����Ϊ ѡ���������� ��
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeB + "','" + GetAreaName(areaCodeB, "0") + "',GETDATE())");
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",2,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");

                        areaCodeZhen = areaCode.Substring(0, 9) + "000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",3,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "������',GETDATE())");
                        //areaCodeZhen = areaCodeA.Substring(0, 9) + "000";
                        //list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",4,'" + areaCodeZhen + "','" + GetAreaName(areaCodeZhen, "0") + "',GETDATE())");

                        
                        areaCodeXian = areaCode.Substring(0, 6) + "000000";
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",4,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "�˿ںͼƻ�������',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    case "0131":
                        //��ǰ��飬������֤�����ذ�֤����Э�����ҵ��
                        if (SelAreaCode.Substring(9) != "000")
                        {
                            //����Ǵ弶��������������
                            SelAreaCode = SelAreaCode.Substring(0, 9) + "000";
                        }
                        areaCodeXian = SelAreaCode;
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStepTotal,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + "," + bizStep + ",1,'" + areaCodeXian + "','" + GetAreaName(areaCodeXian, "0") + "�����ͼƻ�������',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                        
                    case "AppA":
                        list.Add("INSERT INTO BIZ_WorkFlows(BizID,BizStep,AreaCode,AreaName,CreateDate) VALUES(" + bizID + ",1,'" + areaCodeA + "','" + GetAreaName(areaCodeA, "0") + "',GETDATE())");
                        returnVal = DbHelperSQL.ExecuteSqlTran(list).ToString();
                        break;
                    default:
                        returnVal = "δָ��ҵ������";
                        break;
                }
                list = null;
            }
            catch
            {
                returnVal = "";
                list = null;
            }
            return returnVal;
        }
        /// <summary>
        /// �ж��Ƿ���Ͻ����
        /// </summary>
        /// <param name="areaCode"></param>
        /// <returns>true Ͻ���⣻false Ͻ����</returns>
        public static bool IsThisAreaCode(string areaCode)
        {
            bool returnVal = true;
            if (m_SiteArea.Substring(0, 4) == areaCode.Substring(0, 4))
            { returnVal = false; }
            return returnVal;
        }
        #endregion

        #region Ⱥ������ҵ��������ʾ
        /// <summary>
        /// Ⱥ������ҵ��������ʾ
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="personID"></param>
        /// <param name="personCardID"></param>
        /// <returns></returns>
        public static string GetBizWorkFlows(string objID, string personID, string personCardID)
        {
            DataTable dt = new DataTable();
            StringBuilder sHtml = new StringBuilder();
            string Attribs, CommMemo, Comments = string.Empty;
            string strStyle = string.Empty;
            if (!string.IsNullOrEmpty(objID))
            {
                try
                {
                    string count = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE ((PersonID=" + personID + " AND InitDirection=0) OR PersonCidA='" + personCardID + "' OR PersonCidB='" + personCardID + "') AND BizID=" + objID).ToString();
                    if (int.Parse(count) > 0)
                    {
                        m_SqlParams = "SELECT BizStep,AreaName,Comments,Approval,Signature,CreateDate,OprateDate,Attribs,CommMemo FROM BIZ_WorkFlows WHERE  BizID=" + objID + " ORDER BY BizStep";
                        dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                        int rowLen = dt.Rows.Count;
                        if (rowLen > 0)
                        {
                            for (int i = 0; i < rowLen; i++)
                            {
                                Comments = dt.Rows[i]["Comments"].ToString();
                                CommMemo = dt.Rows[i]["CommMemo"].ToString();//��Ҫʲô����
                                Attribs = dt.Rows[i]["Attribs"].ToString(); // 0,����1,��˳ɹ� 9,Ĭ��
                                string CommMemostr = "";
                                if (Attribs == "0") {
                                    strStyle = " class=\"unpass\"";
                                    if (!string.IsNullOrEmpty(CommMemo))
                                    {
                                        CommMemostr = "<br>�貹���ύ�Ĳ������£�";
                                        string[] aryDocs = null;
                                        aryDocs = CommMemo.Split(',');
                                        for (int k = 0; k < aryDocs.Length; k++)
                                        {
                                            CommMemostr += "<br>" + (k + 1).ToString() + "��" + aryDocs[k];
                                        }
                                    }                                
                                }
                                else if (Attribs == "1") { strStyle = " class=\"pass\""; }
                                else { strStyle = ""; }

                                sHtml.Append("<li" + strStyle + ">");
                                sHtml.Append("<div class=\"flow_bg\">");
                                sHtml.Append("<div class=\"flow_r\">");
                                sHtml.Append("<p class=\"view\">���������</p>");
                                sHtml.Append("<p class=\"sum\">" + Comments + CommMemostr+"</p>");
                                sHtml.Append("</div>");
                                sHtml.Append("<div class=\"flow_l\">");
                                sHtml.Append("<p class=\"number\">" + (i + 1) + "</p>");
                                sHtml.Append("<p class=\"section\">" + dt.Rows[i]["AreaName"].ToString() + "</p>");
                                sHtml.Append("<p class=\"name\">�����:" + dt.Rows[i]["Approval"].ToString() + "<br />ʱ��:" + dt.Rows[i]["OprateDate"].ToString() + "</p>");
                                sHtml.Append("</div>");
                                if (i < (rowLen - 1)) { sHtml.Append("<p class=\"flow_ico\"></p>"); }
                                sHtml.Append("</div>");
                                sHtml.Append("</li>");
                            }
                        }
                        dt.Clear(); dt.Dispose();
                    }
                    else
                    {
                        sHtml = new StringBuilder(); sHtml.Append("����ʧ�ܣ������߱��鿴����ҵ��İ������̵�Ȩ�ޣ�");
                    }

                }
                catch
                {
                    if (dt != null)
                    {
                        dt.Clear(); dt.Dispose();
                        sHtml = new StringBuilder(); sHtml.Append("��ȡ������Ϣ����");
                    }
                }
            }
            else
            { sHtml.Append("��Ŀǰû�а����κ�ҵ��"); }
            return sHtml.ToString();
        }

        /// <summary>
        /// Ⱥ������ҵ��������ʾ
        /// </summary>
        /// <param name="objID"></param>
        /// <param name="personID"></param>
        /// <param name="personCardID"></param>
        /// <returns></returns>
        public static string GetBizWorkFlowsWap(string objID, string personID, string personCardID)
        {
            DataTable dt = new DataTable();
            StringBuilder sHtml = new StringBuilder();
            string Attribs, CommMemo, Comments = string.Empty;
            string strStyle = string.Empty;
            if (!string.IsNullOrEmpty(objID))
            {
                try
                {
                    string count = DbHelperSQL.GetSingle("SELECT COUNT(0) FROM BIZ_Contents WHERE ((PersonID=" + personID + " AND InitDirection=0) OR PersonCidA='" + personCardID + "' OR PersonCidB='" + personCardID + "') AND BizID=" + objID).ToString();
                    if (int.Parse(count) > 0)
                    {
                        m_SqlParams = "SELECT BizStep,AreaName,Comments,Approval,Signature,CreateDate,OprateDate,Attribs,CommMemo FROM BIZ_WorkFlows WHERE  BizID=" + objID + " ORDER BY BizStep";
                        dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                        int rowLen = dt.Rows.Count;
                        if (rowLen > 0)
                        {
                            for (int i = 0; i < rowLen; i++)
                            {
                                Comments = dt.Rows[i]["Comments"].ToString();
                                CommMemo = dt.Rows[i]["CommMemo"].ToString();//��Ҫʲô����
                                Attribs = dt.Rows[i]["Attribs"].ToString(); // 0,����1,��˳ɹ� 9,Ĭ��
                                string CommMemostr = "";
                                if (Attribs == "0") { 
                                    strStyle = " class=\"unpass\"";
                                    if (!string.IsNullOrEmpty(CommMemo))
                                    {
                                        CommMemostr = "<br>�貹���ύ�Ĳ������£�";
                                        string[] aryDocs = null;
                                        aryDocs = CommMemo.Split(',');
                                        for (int k = 0; k < aryDocs.Length; k++)
                                        {
                                            CommMemostr += "<br>" + (k + 1).ToString() + "��" + aryDocs[k];
                                        }
                                    }
                                }
                                else if (Attribs == "1") { 
                                    strStyle = " class=\"pass\""; 
                                }
                                else { strStyle = ""; }


                                sHtml.Append("<li" + strStyle + ">");
                                sHtml.Append("<div class=\"handle_title clearfix\">");
                                sHtml.Append("<i>" + (i + 1) + "</i>");
                                sHtml.Append("<p><b>" + dt.Rows[i]["AreaName"].ToString() + "</b><span>����ˣ�" + dt.Rows[i]["Approval"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;" + dt.Rows[i]["OprateDate"].ToString() + "</span></p>");
                                sHtml.Append("</div>");
                                sHtml.Append("<div class=\"sum\"><b>���������</b>" + Comments +CommMemostr+ "</div>");
                                sHtml.Append("</li>");
                            }
                        }
                        dt.Clear(); dt.Dispose();
                    }
                    else
                    {
                        sHtml = new StringBuilder(); sHtml.Append("����ʧ�ܣ������߱��鿴����ҵ��İ������̵�Ȩ�ޣ�");
                    }

                }
                catch
                {
                    if (dt != null)
                    {
                        dt.Clear(); dt.Dispose();
                        sHtml = new StringBuilder(); sHtml.Append("��ȡ������Ϣ����");
                    }
                }
            }
            else
            { sHtml.Append("��Ŀǰû�а����κ�ҵ��"); }
            return sHtml.ToString();
        }
        #endregion

        #region Ⱥ���û�����
        #region ��ҳ
        /// <summary>
        /// ��ȡ�û�����������Ϣ
        /// </summary>
        public static string GetUserData(string personID)
        {
            string svrsSum = CommPage.GetSingleValue("SELECT COUNT(0) FROM BIZ_Contents WHERE Attribs!=4 AND  PersonID=" + personID);
            string svrsIngSum = CommPage.GetSingleValue("SELECT COUNT(0) FROM BIZ_Contents WHERE Attribs<3 AND PersonID=" + personID);
            string svrsEndSum = CommPage.GetSingleValue("SELECT COUNT(0) FROM BIZ_Contents WHERE Attribs=3 AND PersonID=" + personID);
            string smsReadSum = string.Empty;
            //
            StringBuilder sHtml = new StringBuilder();
            SqlDataReader sdr = null;
            string personTel = "", personCardID = "", createDate = string.Empty;
            string strStyle, navUrl = string.Empty;
            try
            {
                m_SqlParams = "SELECT PersonAcc,PersonTel,PersonCardID,CreateDate FROM BIZ_Persons WHERE PersonID=" + personID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        personTel = sdr["PersonTel"].ToString();
                        createDate = FormatString(sdr["CreateDate"].ToString(), "2");
                        personCardID = sdr["PersonCardID"].ToString();
                        personTel = sdr["PersonTel"].ToString();
                        smsReadSum = CommPage.GetSingleValue("SELECT COUNT(0) FROM SMS WHERE IsRead=0 AND CellNumber='" + personTel + "'");

                        sHtml.Append("<!--ͷ�� -->");
                        sHtml.Append("<div class=\"user_head\">");
                        sHtml.Append("<p class=\"pic\"><img src=\"/UserCenter/images/head.gif\" alt=\"ͷ��\" /></p>");
                        sHtml.Append("<p class=\"button\"><a href=\"/OC/UserInfo." + m_FileExt + "\">�޸ĸ�������</a></p>");
                        sHtml.Append("</div>");

                        sHtml.Append("<!--�û���Ϣ -->");
                        sHtml.Append("<div class=\"user_info\">");
                        //sHtml.Append("<p class=\"name\"><b>" + sdr["PersonAcc"].ToString() + "</b>��ӭ����</p>");
                        sHtml.Append("<p class=\"name\"><span>ע��ʱ�䣺" + sdr["CreateDate"].ToString() + "</span><b>" + sdr["PersonAcc"].ToString() + "</b>��ӭ����</p>");
                        sHtml.Append("<p class=\"info\">");
                        sHtml.Append("<span class=\"a1\">�ֻ����룺<b>" + FormatString(personTel, "1") + "</b><a href=\"/OC/UserTel." + m_FileExt + "\">�޸��ֻ���</a></span>");
                        sHtml.Append("<span class=\"a2\">���֤���룺<b>" + FormatString(personCardID, "0") + "</b></span>");
                        sHtml.Append("</p>");
                        sHtml.Append("<p class=\"state\">");
                        //sHtml.Append("<span>ע��ʱ�䣺" + sdr["CreateDate"].ToString() + "</span>");
                        sHtml.Append("<span>������ҵ��<a href=\"/OC/02-1." + m_FileExt + "\"><b>��" + svrsSum + "��</b></a></span>");
                        sHtml.Append("<span>���ڰ����ҵ��<a href=\"/UserCenter/BizWorkFlows.aspx?action=view\"><b>��" + svrsIngSum + "��</b></a></span>");
                        sHtml.Append("<span>�Ѱ���ҵ��<a href=\"/OC/02-1." + m_FileExt + "\"><b>��" + svrsEndSum + "��</b></a></span>");
                        sHtml.Append("<span>δ�Ķ�������Ϣ<a href=\"/OC/04-1." + m_FileExt + "\"><b>��" + smsReadSum + "��</b></a></span>");
                        sHtml.Append("</p>");
                        sHtml.Append("</div>");

                    }
                }
                sdr = null;
                sHtml.Append("<div class=\"clr20\"></div>");
                //<!--ҵ������¼ -->
                sHtml.Append("<div class=\"user_list\">");
                sHtml.Append("<div class=\"user_list_title\">ҵ������¼</div>");
                sHtml.Append("<div class=\"user_list_c\">");
                sHtml.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                sHtml.Append("<tr>");
                sHtml.Append("<th>��������</th>");
                sHtml.Append("<th width=\"150\">�ύʱ��</th>");
                sHtml.Append("<th width=\"100\">״̬</th>");
                sHtml.Append("<th width=\"100\">�����</th>");
                sHtml.Append("</tr> ");
                string bizID, bizName, attribs, attribsCN, currentStepNa, startDate = string.Empty;


                m_SqlParams = "SELECT TOP 6 BizID,BizName,Attribs,AttribsCN,CurrentStepNa,StartDate FROM v_BizList WHERE BizCode!='0106' AND BizCode!='0111' AND ((PersonID=" + personID + " AND InitDirection=0) OR PersonCidA='" + personCardID + "' OR PersonCidB='" + personCardID + "') AND Attribs!=4  ORDER BY BizID DESC";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        bizID = sdr["BizID"].ToString();
                        bizName = sdr["BizName"].ToString();
                        attribs = sdr["attribs"].ToString();
                        attribsCN = sdr["AttribsCN"].ToString();
                        currentStepNa = sdr["CurrentStepNa"].ToString();
                        startDate = FormatString(sdr["StartDate"].ToString(), "2");

                        if (attribs == "0") { strStyle = " class=\"a1\""; }
                        else { strStyle = ""; }
                        navUrl = "/OC/Svrs-" + bizID + "." + m_FileExt;
                        navUrl = "/UserCenter/BizView.aspx?action=view&RID=" + bizID + "&oCode=02";

                        sHtml.Append("<tr>");
                        sHtml.Append("<td><a href=\"" + navUrl + "\" title=\"" + bizName + "\">" + bizName + "</a></td>");
                        sHtml.Append("<td>" + startDate + "</td>");
                        sHtml.Append("<td" + strStyle + ">" + attribsCN + "</td>");
                        sHtml.Append("<td>" + currentStepNa + "</td>");
                        sHtml.Append("</tr>");
                    }
                }
                else
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<td colspan=\"4\">����������Ϣ</td>");
                    sHtml.Append("</tr>");
                }
                sdr = null;
                sHtml.Append("</table>");
                sHtml.Append("</div>");
                sHtml.Append("</div>");
                sHtml.Append("<div class=\"clr20\"></div>");

                //<!--������Ϣ -->
                sHtml.Append("<div class=\"user_list\">");
                sHtml.Append("<div class=\"user_list_title\">������Ϣ</div>");
                sHtml.Append("<div class=\"user_list_c\">");
                sHtml.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                sHtml.Append("<tr>");
                sHtml.Append("<th>��Ϣ����</th>");
                sHtml.Append("<th width=\"150\">����ʱ��</th>");
                sHtml.Append("</tr>");
                if (!String.IsNullOrEmpty(personTel))
                {
                    string sysNo, sMSContent, createTime = string.Empty;

                    m_SqlParams = "SELECT TOP 6 SysNo,SMSContent,CreateTime FROM SMS WHERE CellNumber='" + personTel + "'";
                    sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            sysNo = sdr["SysNo"].ToString();
                            sMSContent = sdr["SMSContent"].ToString();
                            createTime = sdr["CreateTime"].ToString();
                            if (!String.IsNullOrEmpty(createTime)) { createTime = DateTime.Parse(createTime).ToString("yyyy-MM-dd HH:mm"); }

                            navUrl = "/UserCenter/SMSView.aspx?action=view&RID=" + sysNo + "&oCode=04";

                            sHtml.Append("<tr>");
                            sHtml.Append("<td><a href=\"" + navUrl + "\" title=\"" + sMSContent + "\">" + sMSContent + "</a></td>");
                            sHtml.Append("<td>" + createTime + "</td>");
                            sHtml.Append("</tr>");
                        }
                    }

                    else
                    {
                        sHtml.Append("<tr>");
                        sHtml.Append("<td colspan=\"2\">����������Ϣ</td>");
                        sHtml.Append("</tr>");
                    }
                    sdr.Close(); sdr.Dispose();

                }

                sHtml.Append("</table>");
                sHtml.Append("</div>");
                sHtml.Append("</div>");
            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                sHtml = new StringBuilder(); sHtml.Append("<div class=\"user_info\">��ȡ������Ϣ����</div>");
            }
            return sHtml.ToString();
        }
        #endregion
        #region ��ȡҵ��������Ϣ
        /// <summary>
        /// ��ȡҵ��������Ϣ
        /// </summary>
        /// <param name="objID"></param>
        public static string GetBizCritic(string bizID, string personID, ref bool IsHasData)
        {
            string starLevel = "", Comments = string.Empty;
            StringBuilder sHtml = new StringBuilder();
            string Attribs = CommPage.GetSingleVal("SELECT Attribs FROM BIZ_Contents WHERE BizID=" + bizID);
            if (Attribs != "2" && Attribs != "9")
            {
                sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");

                sHtml.Append("<tr>");
                sHtml.Append("<th>��</th>");
                sHtml.Append("<td class=\"text\">������ʾ����ȴ�ҵ���������</td>");
                sHtml.Append("</tr>");
                sHtml.Append("</table>");
            }
            else
            {
                SqlDataReader sdr = null;
                try
                {
                    sdr = DbHelperSQL.ExecuteReader("SELECT StarLevel,Comments FROM BIZ_Critic WHERE BizID=" + bizID + " AND PersonID=" + personID);
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            starLevel = sdr["StarLevel"].ToString();
                            Comments = PageValidate.Decode(sdr["Comments"].ToString());
                        }
                        IsHasData = true;
                    }
                    else { starLevel = "5"; }
                    sdr.Close(); sdr.Dispose();
                }
                catch { if (sdr != null) { sdr.Close(); sdr.Dispose(); } }


                string[] arrStarLevel ={ "5", "4", "3", "1", "0" };
                string[] arrStarLevelText ={ "5�� ������,��������,�ܵ���ϸ,�ǳ�����", "4�� ����,���¹��̻���˳��,����ͦ�����", "3�� һ�㣬����һ�㣬û�������ĺ�", "1�� �����⣬����̬�Ȱ������񣬲�̫����", "0�� �ܲ�����" };
                sHtml.Append("<table width=\"0\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                /*
                 5�� ������,����λ,�ǳ�����
                 4�� ����,���¹��̻���˳��,����ͦ�����
                 3�� һ��,
                 1�� ������
                 0�� �ܲ�����
                 */
                for (int i = 0; i < 5; i++)
                {
                    sHtml.Append("<tr>");
                    sHtml.Append("<th></th><td>");
                    if (arrStarLevel[i] == starLevel)
                    { sHtml.Append("<input type=\"radio\" name=\"rdStarLevel\" value=\"" + arrStarLevel[i] + "\" checked=\"checked\" /> " + arrStarLevelText[i]); }
                    else { sHtml.Append("<input type=\"radio\" name=\"rdStarLevel\" value=\"" + arrStarLevel[i] + "\" /> " + arrStarLevelText[i]); }
                    sHtml.Append("</td></tr>");
                    sHtml.Append("<tr>");
                }
                sHtml.Append("<tr>");
                sHtml.Append("<th>���ۣ�</th>");
                sHtml.Append("<td class=\"text\"><textarea name=\"txtComments\" id=\"txtComments\" cols=\"30\" rows=\"3\">" + Comments + "</textarea><br/>150����</td>");
                sHtml.Append("</tr>");
                sHtml.Append("</table>");
            }
            return sHtml.ToString();
        }
        #endregion
        #region ��ѶϢ�鿴
        /// <summary>
        /// �鿴
        /// </summary>
        /// <param name="objID"></param>
        public static string GetSMSInfo(string objID)
        {
            SqlDataReader sdr = null;
            StringBuilder sHtml = new StringBuilder();
            try
            {
                m_SqlParams = "SELECT SysNo, CellNumber, SMSContent,SendNum ,Priority, RetryCount, CreateTime, HandleTime, StatusCN,IsReadCN, ToCom, SendTime FROM v_SMS WHERE SysNo=" + objID;
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        sHtml.Append("<div class=\"user_column_title\">ҵ��������Ϣ</div>");
                        sHtml.Append("<div class=\"user_view\">");
                        sHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">   ");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th>ϵͳ��ţ�</th>");
                        sHtml.Append("<td>&nbsp;" + sdr["SysNo"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th>Ŀ���ֻ����룺</th>");
                        sHtml.Append("<td>&nbsp;" + sdr["CellNumber"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th>��������</th>");
                        sHtml.Append("<td>&nbsp;" + sdr["SMSContent"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th>���ŷ�����</th>");
                        sHtml.Append("<td>&nbsp;" + sdr["SendNum"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th>���Ŵ���ʱ��</th>");
                        sHtml.Append("<td>&nbsp;" + sdr["CreateTime"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th>���ŷ���ʱ��</th>");
                        sHtml.Append("<td>&nbsp;" + sdr["HandleTime"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th>���ŷ���״̬</th>");
                        sHtml.Append("<td>&nbsp;" + sdr["StatusCN"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("<tr>");
                        sHtml.Append("<th>�Ķ�״̬</th>");
                        sHtml.Append("<td>&nbsp;" + sdr["IsReadCN"].ToString() + "</td>");
                        sHtml.Append("</tr>");
                        sHtml.Append("</table>");
                        sHtml.Append("</div>");
                        DbHelperSQL.ExecuteSql("UPDATE SMS SET IsRead=1 WHERE SysNo=" + objID);
                    }
                }
                sdr.Close(); sdr.Dispose();
            }
            catch
            {
                if (sdr != null)
                {
                    sdr.Close(); sdr.Dispose();
                    sHtml = new StringBuilder(); sHtml.Append("��ȡ������Ϣ����");
                }
            }
            return sHtml.ToString();
        }
        #endregion
        #endregion

        #region ��������ѡ��
        /// <summary>
        /// ��ȡ���з��� ajax
        /// </summary>
        /// <returns></returns>
        public static string GetAreaData(string parentCode, string selectClass)
        {
            string AreaCode = string.Empty;
            string AreaName = string.Empty;
            string ctrlName = string.Empty;
            string nextClass = string.Empty;
            string containerName = string.Empty;
            switch (selectClass)
            {
                case "2": // ��ʾ�ڶ���
                    nextClass = "3";
                    containerName = "ClassIII";
                    break;
                case "3": // ��ʾ������
                    nextClass = "4";
                    containerName = "ClassIV";
                    break;
                case "4": // ��ʾ���ļ�
                    nextClass = "5";
                    containerName = "ClassV";
                    break;
                case "5": // ��ʾ���弶
                    nextClass = "6";
                    containerName = "ClassVI";
                    break;
                default://Ĭ��
                    nextClass = "3";
                    containerName = "ClassIII";
                    break;
            }
            StringBuilder sHtml = new StringBuilder();
            m_SqlParams = "SELECT [AreaCode], [AreaName], [ParentCode] FROM [AreaDetailCN] WHERE ParentCode = '" + parentCode + "' ORDER BY AreaCode";
            DataTable dt = new DataTable();
            try
            {
                dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                // SetNextArea(parentCode,parentText,currentClass,objContainer)
                sHtml.Append("<select size=\"4\" name=\"Sel" + containerName + "\" id=\"Sel" + containerName + "\" style=\"width:148px;height:214px;\" onchange=\"Javascript:SetNextArea(this.value,this.options[this.selectedIndex].text,'" + nextClass + "',document.getElementById('" + containerName + "'))\">");

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AreaCode = PageValidate.GetTrim(dt.Rows[i]["AreaCode"].ToString());
                        AreaName = PageValidate.GetTrim(dt.Rows[i]["AreaName"].ToString());
                        sHtml.Append("<option value=\"" + AreaCode + "\">" + AreaName + "</option>");// " + AreaCode + "-" + AreaName + "
                    }
                }
                sHtml.Append("</select>");
            }
            catch (Exception ex) { sHtml.Append(ex.Message); }

            dt.Dispose();

            return sHtml.ToString();
        }

        /// <summary>
        /// ��ȡ���з���
        /// </summary>
        /// <returns></returns>
        public static void GetAreaData(string parentCode)
        {
            SqlDataReader sdr = null;
            string AreaCode = string.Empty;
            string AreaName = string.Empty;
            StringBuilder sHtml = new StringBuilder();
            m_SqlParams = "SELECT [AreaCode], [AreaName], [ParentCode] FROM [AreaDetailCN] WHERE ParentCode = '" + parentCode + "' ORDER BY AreaCode";
            try
            {
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                // disabled=\"disabled\"
                sHtml.Append("<select size=\"4\" name=\"SelClassI\" id=\"SelClassI\" style=\"width:148px;height:214px;\">");//onchange=\"Javascript:SetNextArea(this.value,this.options[this.selectedIndex].text,'2',document.getElementById('ClassII'))\"
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        AreaCode = PageValidate.GetTrim(sdr["AreaCode"].ToString());
                        AreaName = PageValidate.GetTrim(sdr["AreaName"].ToString());
                        if (AreaCode == parentCode)
                        {
                            sHtml.Append("<option value=\"" + AreaCode + "\" selected>" + AreaName + "</option>");
                        }
                        else
                        {
                            sHtml.Append("<option value=\"" + AreaCode + "\">" + AreaName + "</option>");
                        }
                    }
                }
                sdr.Close(); sdr.Dispose();
                sHtml.Append("</select>");
            }
            catch (Exception ex)
            {
                sHtml = new StringBuilder(); sHtml.Append("��ȡ������Ϣ����");
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }
            }
            //this.LiteralI.Text = sHtml.ToString();
        }

        /*//this.ButNext.Attributes.Add("onmouseover", "this.style.cursor='hand'");
          ��ȡһ������з��� 610100000000������ 610115000000��ɽ��
         GetAreaData("530000000000");
          // �������ط���
          //GetAreaData(this.LiteralII, "611022000000", "2");
          // �����ְ���� 610122000000
          //GetAreaData(this.LiteralIII, m_SiteArea, "3");
          // �ļ������ 610122000000*/
        /// <summary>
        /// ��ȡ���з���
        /// </summary>
        /// <returns></returns>
        public static void GetAreaData(Literal ctrl, string parentCode, string selectClass)
        {
            SqlDataReader sdr = null;
            StringBuilder sHtml = new StringBuilder();
            string AreaCode = string.Empty;
            string AreaName = string.Empty;
            string ctrlName = string.Empty;
            string nextClass = string.Empty;
            string containerName = string.Empty;
            switch (selectClass)
            {
                case "2": // ��ʾ�ڶ���
                    nextClass = "3";
                    containerName = "ClassIII";
                    break;
                case "3": // ��ʾ������
                    nextClass = "4";
                    containerName = "ClassIV";
                    break;
                case "4": // ��ʾ���ļ�
                    nextClass = "5";
                    containerName = "ClassV";
                    break;
                default:
                    sHtml.Append("��ȡ������Ϣ����");
                    break;
            }
            m_SqlParams = "SELECT [AreaCode], [AreaName], [ParentCode] FROM [AreaDetailCN] WHERE ParentCode = '" + parentCode + "' ORDER BY AreaCode";
            try
            {
                // SetNextArea(parentCode,parentText,currentClass,objContainer)
                if (int.Parse(selectClass) > 2)
                {
                    sHtml.Append("<select size=\"4\" name=\"Sel" + containerName + "\" id=\"Sel" + containerName + "\" style=\"width:148px;height:214px;\" onchange=\"Javascript:SetNextArea(this.value,this.options[this.selectedIndex].text,'" + nextClass + "',document.getElementById('" + containerName + "'))\">");
                }
                else
                {
                    sHtml.Append("<select size=\"4\" name=\"Sel" + containerName + "\" id=\"Sel" + containerName + "\" style=\"width:148px;height:214px;\">");
                }

                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        AreaCode = PageValidate.GetTrim(sdr["AreaCode"].ToString());
                        AreaName = PageValidate.GetTrim(sdr["AreaName"].ToString());
                        if (AreaCode == m_SiteArea) { sHtml.Append("<option value=\"" + AreaCode + "\" selected>" + AreaName + "</option>"); }
                        else { sHtml.Append("<option value=\"" + AreaCode + "\">" + AreaName + "</option>"); }
                    }
                }
                sdr.Close(); sdr.Dispose();
                sHtml.Append("</select>");
            }
            catch (Exception ex)
            {
                sHtml = new StringBuilder(); sHtml.Append("��ȡ������Ϣ����");
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }
            }
            ctrl.Text = sHtml.ToString();
        }
        #endregion

        #region ��ʽת��

        /// <summary>
        /// ��ȡ���֤�ź��ֻ��ż���
        /// </summary>
        /// <param name="val"></param>
        /// <param name="type">0�����֤�ţ�1���ֻ��ţ�2�����ڣ�3��ͼƬ</param>
        /// <returns></returns>
        public static string FormatString(string InStr, string sType)
        {
            string returnVal = string.Empty;
            if (!string.IsNullOrEmpty(InStr))
            {
                try
                {
                    switch (sType)
                    {
                        case "0": // ���֤��
                            returnVal = InStr.Substring(0, 4) + "***********" + InStr.Substring((InStr.Length - 3), 3);
                            break;
                        case "1": // �ֻ���
                            returnVal = InStr.Substring(0, 3) + "****" + InStr.Substring((InStr.Length - 4), 4);
                            break;
                        case "2": // ����
                            if (InStr == "1900/1/1 0:00:00") { InStr = ""; }
                            if (UNV.Comm.Web.PageValidate.IsDateTime(InStr))
                            {
                                returnVal = DateTime.Parse(InStr).ToString("yyyy-MM-dd");
                            }
                            else { returnVal = InStr; }
                            break;
                        case "3": // ͼƬ
                            returnVal = m_SvrUrl + InStr;
                            break;
                        default:
                            returnVal = InStr;
                            break;
                    }
                }
                catch { returnVal = ""; }
            }
            return returnVal;
        }
        #endregion
    }
}
