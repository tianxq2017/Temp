using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using System.Text;
using System.Data.SqlClient;

using Jayrock.Json;
using UNV.Comm.DataBase;
using UNV.Comm.Web;
namespace join.pms.dal
{
    public class CommAjax
    {
        public string CAUserID;
        public string CAType;
        public string CAValue01;
        public string CAValue02;
        public string CAValue03;
        public string CAValue04;
        public string CAValue05;
        public string RetValue01;
        public string RetValue02;
        #region ����ҳͷ��Ϣ��������
        /// <summary>
        /// Ajaxִ�й���
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            string strReturn = "";
            switch (CAType.ToLower())
            {
                case "sendcheckcode":
                    strReturn = this.SendCheckCode();
                    break;
                case "sendcheckcodeup":
                    strReturn = this.SendCheckCode();
                    break;
                case "sendcheckcoded":
                    strReturn = this.SendCheckCodeD();
                    break;
                case "checkuserregname":
                    strReturn = this.CheckUserRegName();
                    break;
                case "checkusercardid":
                    strReturn = this.CheckUserCardID();
                    break;
                case "checkusertel":
                    strReturn = this.CheckUserTel();
                    break;
                case "getpersonsinfo":
                    strReturn = this.GetPersonsInfo();
                    break;
                case "getfwzinfo":
                    strReturn = this.GetFwzInfo();
                    break;
                case "getpersonchildren":
                    strReturn = this.GetPersonChildren();
                    break;
                case "addfavorites"://�����ע
                    strReturn = this.AddFavorites();
                    break;
                default:
                    strReturn = "";//this.MyError("δָ����ȷ���ͣ�" + strType);                   
                    break;
            }
            return strReturn;
        }
        #endregion

        #region �������

        #region ����û���\���֤��\�ֻ����Ƿ��ظ�
        /// <summary>
        /// ����û����Ƿ��ظ�
        /// </summary>
        /// <returns></returns>
        private string CheckUserRegName()
        {
            ;
            try
            {
                if (BIZ_Common.GetPersonCount(CAValue01, "1"))
                { return this.MyError("�Բ��𣬸��û��Ѿ����ڣ�"); }
                else
                { return this.MyError("���û�������ʹ�ã�"); }
            }
            catch (Exception ex)
            {
                return this.MyError(ex.Message.ToString());
            }
        }
        /// <summary>
        /// ��������֤���Ƿ��ظ�
        /// </summary>
        /// <returns></returns>
        private string CheckUserCardID()
        {
            try
            {
                if (BIZ_Common.GetPersonCount(CAValue01, "2"))
                { return this.MyError("�Բ��𣬸����֤���Ѿ����ڣ���ֱ�ӵ�½��"); }
                else
                { return this.MyError(""); }
            }
            catch (Exception ex)
            {
                return this.MyError(ex.Message.ToString());
            }
        }
        /// <summary>
        /// ����ֻ����Ƿ��ظ�
        /// </summary>
        /// <returns></returns>
        private string CheckUserTel()
        {
            try
            {
                if (BIZ_Common.GetPersonCount(CAValue01, "3"))
                { return this.MyError("���ֻ����Ѿ����ڣ�"); }
                else { return this.MyError(""); }
            }
            catch (Exception ex)
            {
                return this.MyError(ex.Message.ToString());
            }
        }
        #endregion

        #region �����ֻ���֤��

        private string m_CheckCodeTimeOut = ConfigurationManager.AppSettings["CheckCodeTimeOut"];
        /// <summary>
        /// �����ֻ���֤��
        /// </summary>
        /// <returns></returns>
        private string SendCheckCode()
        {
            if (BIZ_Common.GetPersonCount(CAValue01, "3"))
            { return this.MyError("���ֻ����Ѿ����ڣ���ֱ�ӵ�¼��"); }
            else
            {
                try
                {

                    // �������
                    string mobilePass = SendMsg.GetRondomCode();
                    string msgBody = "���ã������β����Ķ�̬��֤��Ϊ��" + mobilePass + "����֤����Ч��Ϊ" + m_CheckCodeTimeOut + "�룻����ʧЧ��";
                    string returnMsg = SendMsg.SendMsgByModem(CAValue01, msgBody);
                    if (!string.IsNullOrEmpty(returnMsg) && int.Parse(returnMsg) > 0)
                    {
                        RetValue01 = mobilePass;
                        RetValue02 = m_CheckCodeTimeOut;
                        string mCAValue01 = CAValue01.Substring(0, 3) + "****" + CAValue01.Substring(7, 4);
                        return this.Success("��̬���ſ����Ѿ������������ֻ�" + mCAValue01 + "��������6λ���ֵ��ֻ���֤�룺" + mobilePass + "!");
                        //return this.Success("��̬���ſ����Ѿ������������ֻ�" + mCAValue01 + "��������6λ���ֵ��ֻ���֤��!");
                    }
                    else { return this.MyError("���ڷ��ͣ���ע�����!"); }

                }
                catch (Exception ex)
                {
                    return this.MyError(ex.Message.ToString());
                }
            }
        }

        /// <summary>
        /// �����ֻ���֤��
        /// </summary>
        /// <returns></returns>
        private string SendCheckCodeD()
        {
            if (BIZ_Common.GetPersonCount(CAValue01, "3"))
            {
                try
                {
                    // �������
                    string mobilePass = SendMsg.GetRondomCode();
                    string msgBody = "���ã������β����Ķ�̬��֤��Ϊ��" + mobilePass + "����֤����Ч��Ϊ" + m_CheckCodeTimeOut + "�룻����ʧЧ��";
                    string returnMsg = SendMsg.SendMsgByModem(CAValue01, msgBody);
                    if (!string.IsNullOrEmpty(returnMsg) && int.Parse(returnMsg) > 0)
                    {
                        RetValue01 = mobilePass;
                        RetValue02 = m_CheckCodeTimeOut;
                        string mCAValue01 = CAValue01.Substring(0, 3) + "****" + CAValue01.Substring(7, 4);
                          return this.Success("��̬���ſ����Ѿ������������ֻ�" + mCAValue01 + "��������6λ���ֵ��ֻ���֤�룺" + mobilePass + "!");
                        //return this.Success("��̬���ſ����Ѿ������������ֻ�" + mCAValue01 + "��������6λ���ֵ��ֻ���֤��!");
                    }
                    else { return this.MyError("���ڷ��ͣ���ע�����!"); }
                }
                catch (Exception ex)
                {
                    return this.MyError(ex.Message.ToString());
                }
            }
            else { return this.MyError("���ֻ��Ų����ڣ���ʹ�õ���ע����½!"); }
        }
        #endregion

        #region ��ӵ��û��ղ�
        /// <summary>
        /// ��ӵ��û��ղ�
        /// </summary>
        /// <returns></returns>
        private string AddFavorites()
        {
            string returnVal = string.Empty;
            if (String.IsNullOrEmpty(CAUserID))
            {
                return this.MyError("�����ȵ�¼!");
            }
            else
            {
                string name = CAValue01;
                string link = CAValue02;
                string outID = CAValue03;
                string favType = CAValue04;//0ҵ��1Cms��2����
                try
                {
                    string sqlParams = "SELECT COUNT(0) FROM BIZ_PersonFavorites WHERE FavoriteUrl='" + link + "' AND PersonID=" + CAUserID;
                    int count = (int)(DbHelperSQL.GetSingle(sqlParams));
                    if (count == 0)
                    {
                        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                        list.Add("UPDATE BIZ_Categories SET BizFavNum=BizFavNum+1 WHERE BizCode='" + outID + "'");
                        list.Add("INSERT INTO BIZ_PersonFavorites(PersonID,FavoriteType,FavoriteOutID,FavoriteTitle,FavoriteUrl,CreateDate)VALUES(" + CAUserID + "," + favType + "," + outID + ",'" + name + "','" + link + "',GetDate())");
                        DbHelperSQL.ExecuteSqlTran(list);
                        list = null;
                        return this.Success();
                    }
                    else
                    { returnVal = this.MyError("���Ѿ��ղع��������ϣ�"); }
                }
                catch (Exception ex)
                { returnVal = this.MyError(ex.Message.ToString()); }
                return returnVal;
            }
        }
        #endregion

        #region ��ҵ����ݰ�֤�˵����֤�Ż�ȡ�������Ϣ
        /// <summary>
        /// ��ҵ����ݰ�֤�˵����֤�Ż�ȡ�������Ϣ
        /// </summary>
        /// <returns></returns>
        private string GetPersonsInfo()
        {
            string PersonCardID = CAValue01;
            if (!ValidIDCard.VerifyIDCard(PersonCardID)) { return this.MyError("���֤��������"); }
            else
            {
                string[] PersonsInfo = new string[23];

                try
                {
                    BIZ_Persons bizPer = new BIZ_Persons();
                    bizPer.GetPersonsInfo(PersonCardID);
                    if (!string.IsNullOrEmpty(bizPer.PersonID))
                    {
                        PersonsInfo[0] = bizPer.PersonID;
                        PersonsInfo[1] = bizPer.PersonTel;
                        PersonsInfo[2] = bizPer.PersonMail;
                        PersonsInfo[3] = bizPer.PersonName;
                        PersonsInfo[4] = bizPer.PersonCardID;
                        PersonsInfo[5] = bizPer.PersonSex;
                        PersonsInfo[6] = bizPer.PersonBirthday;
                        PersonsInfo[7] = bizPer.MarryType;
                        PersonsInfo[8] = bizPer.MarryDate;
                        PersonsInfo[9] = bizPer.BirthType;
                        PersonsInfo[10] = bizPer.BirthNum;
                        PersonsInfo[11] = bizPer.Nations;
                        PersonsInfo[12] = bizPer.WorkUnit;
                        PersonsInfo[13] = bizPer.Address;
                        PersonsInfo[14] = bizPer.RegisterAreaCode;
                        PersonsInfo[15] = bizPer.RegisterAreaName;
                        PersonsInfo[16] = bizPer.CurrentAreaCode;
                        PersonsInfo[17] = bizPer.CurrentAreaName;
                        PersonsInfo[18] = bizPer.MateName;
                        PersonsInfo[19] = bizPer.MateCardID;
                        PersonsInfo[20] = bizPer.RegisterType;
                        PersonsInfo[21] = bizPer.MarryCardID;
                        PersonsInfo[22] = "";
                        //��ȡ����0150����֤��
                        string QcfwzBm_where = "";
                        if (!string.IsNullOrEmpty(PersonsInfo[4]))
                        {
                            QcfwzBm_where = " and (PersonCidB='" + PersonsInfo[4] + "' or PersonCidA='" + PersonsInfo[4] + "') ";
                        }
                        if (!string.IsNullOrEmpty(PersonsInfo[19]))
                        {
                            QcfwzBm_where = QcfwzBm_where + " and (PersonCidB='" + PersonsInfo[19] + "' or PersonCidA='" + PersonsInfo[19] + "')  ";
                        }
                        PersonsInfo[22] = CommPage.GetSingleVal(" select top 1  QcfwzBm from BIZ_Contents  WHERE BizCode='0150' and QcfwzBm!='' and  Attribs IN(2,8,9) " + QcfwzBm_where + " order by BizID desc ");

                        bizPer = null;
                    }
                    else { GetQYKPersonsInfo(PersonCardID, ref PersonsInfo); }
                    return this.Success(PersonsInfo);
                }
                catch (Exception ex)
                {
                    return "";
                    //return this.MyError("ϵͳ��������ϵϵͳ����Ա��");
                }
            }
        }
        #endregion
        #region ����ĸ�ӽ����ֲ�֤�Ż�ȡ�������Ϣ
        /// <summary>
        /// ����ĸ�ӽ����ֲ�֤�Ż�ȡ�������Ϣ
        /// </summary>
        /// <returns></returns>
        private string GetFwzInfo()
        {
            string fwzhCard = CAValue01;
            if (string.IsNullOrEmpty(fwzhCard)) { return this.MyError("ĸ�ӽ����ֲ�֤�Ų���Ϊ�գ�"); }
            else
            {
                string[] PersonsInfo = new string[5];
                SqlDataReader sdr = null;
                try
                {
                    string sqlParams = "SELECT Fileds01,Fileds08,PersonCidB,PersonCidA,QcfwzBm FROM BIZ_Contents WHERE  QcfwzBmV='" + fwzhCard + "' or QcfwzBm='" + fwzhCard + "'";
                    sdr = DbHelperSQL.ExecuteReader(sqlParams);
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            PersonsInfo[0] = PageValidate.GetTrim(sdr["PersonCidB"].ToString());
                            PersonsInfo[1] = PageValidate.GetTrim(sdr["Fileds08"].ToString());
                            PersonsInfo[2] = PageValidate.GetTrim(sdr["PersonCidA"].ToString());
                            PersonsInfo[3] = PageValidate.GetTrim(sdr["Fileds01"].ToString());
                            PersonsInfo[4] = PageValidate.GetTrim(sdr["QcfwzBm"].ToString());
                        }
                    }
                    return this.Success(PersonsInfo);
                }
                catch (Exception ex)
                {
                    return "";
                    //return this.MyError("ϵͳ��������ϵϵͳ����Ա��");
                }
            }
        }
        #endregion
        #region ��ȡȫԱ�˿ڿ���Ϣ
        /// <summary>
        /// ��ȡȫԱ����Ϣ
        /// </summary>
        /// <param name="PersonCardID"></param>
        /// <param name="PersonsInfo"></param>
        private void GetQYKPersonsInfo(string PersonCardID, ref string[] PersonsInfo)
        {
            SqlDataReader sdr = null;
            try
            {
                //0 PersonName,1 PersonSex,2 PersonCID,3 PersonBirthday,4 PersonNation,5 MaritalStatus,6 WeddingDate,7 MateName,8 MateCID,9 PersonRegType
                string sqlParams = "SELECT TOP 1 PersonName,PersonSex,PersonCID,PersonBirthday,PersonNation,MaritalStatus,WeddingDate,MateName,MateCID,PersonRegType  FROM QYK_Persons WHERE PersonCID='" + PersonCardID + "'";
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        PersonsInfo[0] = " ";
                        PersonsInfo[1] = " ";
                        PersonsInfo[2] = " ";
                        PersonsInfo[3] = PageValidate.GetTrim(sdr["PersonName"].ToString());
                        PersonsInfo[4] = PageValidate.GetTrim(sdr["PersonCID"].ToString());
                        PersonsInfo[5] = PageValidate.GetTrim(sdr["PersonSex"].ToString());
                        string PersonBirthday = CommBiz.GetTrim(sdr["PersonBirthday"].ToString());
                        if (!String.IsNullOrEmpty(PersonBirthday) && UNV.Comm.Web.PageValidate.IsDateTime(PersonBirthday)) { PersonBirthday = DateTime.Parse(PersonBirthday).ToString("yyyy-MM-dd"); }
                        PersonsInfo[6] = PersonBirthday;
                        PersonsInfo[7] = PageValidate.GetTrim(sdr["MaritalStatus"].ToString());
                        string MarryDate = CommBiz.GetTrim(sdr["WeddingDate"].ToString());
                        if (MarryDate == "1900/1/1 0:00:00") { MarryDate = ""; }
                        if (!String.IsNullOrEmpty(MarryDate) && UNV.Comm.Web.PageValidate.IsDateTime(MarryDate)) { MarryDate = DateTime.Parse(MarryDate).ToString("yyyy-MM-dd"); }
                        PersonsInfo[8] = MarryDate;
                        PersonsInfo[9] = " ";
                        PersonsInfo[10] = " ";
                        PersonsInfo[11] = PageValidate.GetTrim(sdr["PersonNation"].ToString());
                        PersonsInfo[12] = " ";
                        PersonsInfo[13] = " ";
                        PersonsInfo[14] = " ";
                        PersonsInfo[15] = " ";
                        PersonsInfo[16] = " ";
                        PersonsInfo[17] = " ";
                        PersonsInfo[18] = PageValidate.GetTrim(sdr["MateName"].ToString());
                        PersonsInfo[19] = PageValidate.GetTrim(sdr["MateCID"].ToString());
                        PersonsInfo[20] = PageValidate.GetTrim(sdr["PersonRegType"].ToString());
                        PersonsInfo[21] = " ";
                        PersonsInfo[22] = " ";
                    }
                }
                sdr.Close(); sdr.Dispose();
            }
            catch
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }
            }
        }
        #endregion

        #region ��ҵ����ݰ�֤�˵�PersonID��ȡ����Ů��Ϣ
        /// <summary>
        /// ��ҵ����ݰ�֤�˵�PersonID��ȡ����Ů��Ϣ
        /// </summary>
        /// <returns></returns>
        private string GetPersonChildren()
        {
            string PersonID = CAValue01;
            if (String.IsNullOrEmpty(PersonID)) { return this.MyError("û��ָ����ʶ��"); }
            else
            {
                string[] PersonChildrenInfo = new string[27];

                try
                {
                    BIZ_PersonChildren bizChild = new BIZ_PersonChildren();
                    //bizChild.Select(" PersonID=" + PersonID);
                    PersonChildrenInfo[0] = bizChild.CommID1;
                    PersonChildrenInfo[1] = bizChild.PersonID;
                    PersonChildrenInfo[2] = bizChild.ChildName1;
                    PersonChildrenInfo[3] = bizChild.ChildSex1;
                    PersonChildrenInfo[4] = bizChild.ChildBirthday1;
                    PersonChildrenInfo[5] = bizChild.ChildCardID1;
                    PersonChildrenInfo[6] = bizChild.ChildSource1;
                    PersonChildrenInfo[7] = bizChild.ChildPolicy1;
                    PersonChildrenInfo[8] = bizChild.CreateDate1;


                    PersonChildrenInfo[9] = bizChild.CommID2;
                    PersonChildrenInfo[10] = bizChild.PersonID;
                    PersonChildrenInfo[11] = bizChild.ChildName2;
                    PersonChildrenInfo[12] = bizChild.ChildSex2;
                    PersonChildrenInfo[13] = bizChild.ChildBirthday2;
                    PersonChildrenInfo[14] = bizChild.ChildCardID2;
                    PersonChildrenInfo[15] = bizChild.ChildSource2;
                    PersonChildrenInfo[16] = bizChild.ChildPolicy2;
                    PersonChildrenInfo[17] = bizChild.CreateDate2;


                    PersonChildrenInfo[18] = bizChild.CommID3;
                    PersonChildrenInfo[19] = bizChild.PersonID;
                    PersonChildrenInfo[20] = bizChild.ChildName3;
                    PersonChildrenInfo[21] = bizChild.ChildSex3;
                    PersonChildrenInfo[22] = bizChild.ChildBirthday3;
                    PersonChildrenInfo[23] = bizChild.ChildCardID3;
                    PersonChildrenInfo[24] = bizChild.ChildSource3;
                    PersonChildrenInfo[25] = bizChild.ChildPolicy3;
                    PersonChildrenInfo[26] = bizChild.CreateDate3;

                    return this.Success(PersonChildrenInfo);
                }
                catch (Exception ex)
                {

                    //return this.MyError("ϵͳ��������ϵϵͳ����Ա��");
                    return "";
                }
            }
        }
        #endregion
        #endregion

        #region ���ؽ��
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="strError"></param>
        /// <param name="isAlert">�Ƿ񵯳�ϵͳ�Ի���</param>
        public string MyError(string strError, bool isAlert)
        {
            JsonObject jsonObj = new JsonObject();
            jsonObj.Put("isError", true);
            jsonObj.Put("error", strError);
            jsonObj.Put("isAlert", isAlert);
            return jsonObj.ToString();
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="strError"></param>
        /// <returns></returns>
        public string MyError(string strError)
        {
            return this.MyError(strError, false);
        }

        /// <summary>
        /// �ɹ����ؽ��
        /// </summary>
        /// <param name="obj"></param>
        public string Success(object obj)
        {
            JsonObject jsonObj = new JsonObject();
            jsonObj.Put("isError", false);
            jsonObj.Put("data", obj);
            return jsonObj.ToString();
        }

        /// <summary>
        /// �ɹ����ؽ��
        /// </summary>
        public string Success()
        {
            return this.Success(null);
        }
        #endregion
    }
}
