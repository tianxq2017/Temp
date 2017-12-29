using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
using UNV.Comm.Web;

namespace join.pms.dal
{
    public class BIZ_Persons
    {
        #region 字段
        public string PersonID;
        public string PersonAcc;
        public string PersonPwd;
        public string PersonTel;
        public string PersonMail;
        public string PersonName;
        public string PersonCardID;
        public string PersonSex;
        public string PersonPhoto;
        public string PersonBirthday;
        public string AreaCode;
        public string AreaName;
        public string MarryType;
        public string MarryDate;
        public string MarryCardID;
        public string BirthType;
        public string BirthNum;
        public string Nations;
        public string WorkUnit;
        public string Address;
        public string RegisterType;
        public string RegisterAreaCode;
        public string RegisterAreaName;
        public string CurrentAreaCode;
        public string CurrentAreaName;
        public string MateName;
        public string MateCardID;
        public string CreateDate;
        public string Attribs;
        #endregion
        //
        public string MarryDateEnd;

        //public string[] aryTitles;
        public string[] aryAnalys;
        public string SearchWhere;

        #region 发起时相关

        #region 判断群众基础表、婚姻表、子女表中是否存在相关信息及对应情况下处理事务
        /*以下：1.判断群众基础表中是否存在，不存在插入，存在更新  --判断依据：身份证号
                2.判断婚姻表中是否存在该用户信息，不存在插入，存在不跟新  --判断依据：群众编号
                3.判断子女表中是否存在该用户现有家庭子女信息，不存在插入，存在不更新  --判断依据：群众编号
         */
        /// <summary>
        /// 仅更新个案信息及婚姻情况
        /// </summary>
        /// <param name="bizPersons"></param>
        public string ExecBizPersons()
        {
            string personID = string.Empty;
            if (!string.IsNullOrEmpty(PersonCardID))
            {
                if (IsPersonExist())
                {
                    //更新
                    personID = IsPersonNeedUpdate();
                }
                else
                {
                    //不存在                
                    PersonAcc = PersonCardID;//CommPage.GetUserNo();
                    PersonPwd = DESEncrypt.GetMD5_32(PersonName);
                    int len = PersonCardID.Length - 6;
                    PersonPwd = PersonCardID.Substring(len, 6);//DESEncrypt.GetMD5_32(PersonName);
                    Insert(ref personID);
                }
                //非未婚人员需 判断婚姻表、子女表中是否存在该用户信息
                if (MarryType != "未婚") { SetPersonnMarryRec(personID, MarryType, MarryDate, MateName, MateCardID); }
            }
            return personID;
        }
        /// <summary>
        /// 更新个案信息、婚姻情况及子女信息
        /// </summary>
        /// <param name="childrens">子女信息</param>
        public string ExecBizPersons(string[] childrens)
        {
            string personID = string.Empty;
            if (!string.IsNullOrEmpty(PersonCardID))
            {
                if (IsPersonExist())
                {
                    //更新
                    personID = IsPersonNeedUpdate();
                }
                else
                {
                    //不存在                
                    PersonAcc = PersonCardID;//CommPage.GetUserNo();
                    PersonPwd = DESEncrypt.GetMD5_32(PersonName);
                    int len = PersonCardID.Length - 6;
                    PersonPwd = PersonCardID.Substring(len, 6);//DESEncrypt.GetMD5_32(PersonName);
                    Insert(ref personID);
                }
                //非未婚人员需 判断婚姻表、子女表中是否存在该用户信息
                if (MarryType != "未婚")
                {
                    SetPersonnMarryRec(personID, "初婚", MarryDate, MateName, MateCardID);
                    if (MarryType != "初婚") { SetPersonnMarryRec(personID, MarryType, MarryDateEnd, MateName, MateCardID); }

                }
            }
            return personID;
        }
        #endregion

        /// <summary>
        /// 各业务根据办证人的身份证号获取其相关信息
        /// </summary>
        /// <returns></returns>
        public void GetPersonsInfo(string userID)
        {
            SqlDataReader sdr = null;
            try
            {
                string sql = " 1=1 ";
                //0 PersonID,1 PersonTel,2 PersonMail,3 PersonName,4 PersonCardID,5 PersonSex,6 PersonBirthday,7 MarryType,8 MarryDate,9 BirthType,10 BirthNum,11 Nations,12 WorkUnit,13 Address,14 RegisterAreaCode,15 RegisterAreaName,16 CurrentAreaCode,17 CurrentAreaName,18 MateName,19 MateCardID,20 RegisterType
                if (ValidIDCard.VerifyIDCard(userID)) { sql += " AND PersonCardID='" + userID + "'"; }
                else { sql += " AND PersonID=" + userID + ""; }
                string sqlParams = "SELECT top 1 PersonID,PersonTel,PersonMail,PersonName,PersonCardID,PersonSex,PersonBirthday,MarryType,MarryDate,MarryCardID,BirthType,BirthNum,Nations,WorkUnit,Address,RegisterAreaCode,RegisterAreaName,CurrentAreaCode,CurrentAreaName,MateName,MateCardID,RegisterType FROM BIZ_Persons WHERE " + sql;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        PersonID = PageValidate.GetTrim(sdr["PersonID"].ToString());
                        PersonTel = PageValidate.GetTrim(sdr["PersonTel"].ToString());
                        PersonMail = PageValidate.GetTrim(sdr["PersonMail"].ToString());
                        PersonName = PageValidate.GetTrim(sdr["PersonName"].ToString());
                        PersonCardID = PageValidate.GetTrim(sdr["PersonCardID"].ToString());
                        PersonSex = PageValidate.GetTrim(sdr["PersonSex"].ToString());
                        PersonBirthday = CommBiz.GetTrim(sdr["PersonBirthday"].ToString());
                        if (!String.IsNullOrEmpty(PersonBirthday) && UNV.Comm.Web.PageValidate.IsDateTime(PersonBirthday)) { PersonBirthday = DateTime.Parse(PersonBirthday).ToString("yyyy-MM-dd"); }
                        MarryType = PageValidate.GetTrim(sdr["MarryType"].ToString());
                        MarryDate = CommBiz.GetTrim(sdr["MarryDate"].ToString());
                        if (MarryDate == "1900/1/1 0:00:00") { MarryDate = " "; }
                        if (!String.IsNullOrEmpty(MarryDate) && UNV.Comm.Web.PageValidate.IsDateTime(MarryDate)) { MarryDate = DateTime.Parse(MarryDate).ToString("yyyy-MM-dd"); }
                        MarryCardID = PageValidate.GetTrim(sdr["MarryCardID"].ToString());
                        BirthType = PageValidate.GetTrim(sdr["BirthType"].ToString());
                        BirthNum = PageValidate.GetTrim(sdr["BirthNum"].ToString());
                        if (BirthNum == "0") { BirthNum = " "; }
                        Nations = PageValidate.GetTrim(sdr["Nations"].ToString());
                        WorkUnit = PageValidate.GetTrim(sdr["WorkUnit"].ToString());
                        Address = PageValidate.GetTrim(sdr["Address"].ToString());
                        RegisterAreaCode = PageValidate.GetTrim(sdr["RegisterAreaCode"].ToString());
                        RegisterAreaName = PageValidate.GetTrim(sdr["RegisterAreaName"].ToString());
                        CurrentAreaCode = PageValidate.GetTrim(sdr["CurrentAreaCode"].ToString());
                        CurrentAreaName = PageValidate.GetTrim(sdr["CurrentAreaName"].ToString());
                        MateName = PageValidate.GetTrim(sdr["MateName"].ToString());
                        MateCardID = PageValidate.GetTrim(sdr["MateCardID"].ToString());
                        RegisterType = PageValidate.GetTrim(sdr["RegisterType"].ToString());
                    }
                }
                sdr.Close(); sdr.Dispose();
            }
            catch (Exception ex)
            { if (sdr != null) { sdr.Close(); sdr.Dispose(); } }

        }
        #endregion

        #region Person相关

        /// <summary>
        /// 该身份证号所关联的信息是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsPersonExist()
        {
            if (!string.IsNullOrEmpty(PersonCardID))
            {
                string sql = "SELECT COUNT(*) FROM BIZ_Persons WHERE PersonCardID='" + PersonCardID + "'";
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
        /// 该身份证号所关联的信息是否需要更新 update
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        public bool IsPersonNeedUpdate(ref string personID)
        {
            bool returnVa = true;
            if (!string.IsNullOrEmpty(PersonCardID))
            {
                string sqlParams = "SELECT PersonID,PersonName,PersonTel,RegisterAreaCode,CurrentAreaCode FROM BIZ_Persons WHERE PersonCardID='" + PersonCardID + "'";
                SqlDataReader sdr = null;
                int i = 0;
                try
                {
                    sdr = DbHelperSQL.ExecuteReader(sqlParams);
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            personID = sdr[0].ToString();
                            if (!string.IsNullOrEmpty(sdr[0].ToString()) && !string.IsNullOrEmpty(sdr[1].ToString()) && !string.IsNullOrEmpty(sdr[2].ToString()) && !string.IsNullOrEmpty(sdr[3].ToString()) && !string.IsNullOrEmpty(sdr[4].ToString()))
                            {
                                returnVa = false;
                            }
                        }
                    }
                    else { returnVa = false; }

                    sdr.Close();
                }
                catch { if (sdr != null) sdr.Close(); }
                return returnVa;
            }
            else { return true; }
        }
        /// <summary>
        /// 该身份证号所关联的信息是否需要更新 update 并更新
        /// </summary>
        /// <returns></returns>
        public string IsPersonNeedUpdate()
        {
            string personID = string.Empty;
            if (!String.IsNullOrEmpty(PersonCardID) || !String.IsNullOrEmpty(PersonID))
            {
                string sql = " 1=1 ";
                if (!string.IsNullOrEmpty(PersonCardID)) { sql += " AND PersonCardID='" + PersonCardID + "'"; }
                if (!string.IsNullOrEmpty(PersonID)) { sql += " AND PersonID=" + PersonID + ""; }

                string updateFileds = string.Empty;
                string fileds = "PersonMail, PersonName, PersonCardID, PersonSex, PersonPhoto, PersonBirthday, AreaCode, AreaName, MarryType, MarryDate,MarryCardID, BirthType, BirthNum, Nations, WorkUnit, Address, RegisterType, RegisterAreaCode, RegisterAreaName, CurrentAreaCode, CurrentAreaName, MateName, MateCardID";
                string[] arrFileds = fileds.Split(',');
                string[] values ={ PersonMail, PersonName, PersonCardID, PersonSex, PersonPhoto, PersonBirthday, AreaCode, AreaName, MarryType, MarryDate, MarryCardID, BirthType, BirthNum, Nations, WorkUnit, Address, RegisterType, RegisterAreaCode, RegisterAreaName, CurrentAreaCode, CurrentAreaName, MateName, MateCardID, PersonID };
                string sqlParams = "SELECT " + fileds + ",PersonID FROM BIZ_Persons WHERE " + sql;
                string sdrVal = string.Empty;
                SqlDataReader sdr = null;
                try
                {
                    sdr = DbHelperSQL.ExecuteReader(sqlParams);
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            personID = sdr["PersonID"].ToString();
                            PersonID = personID;
                            for (int i = 0; i < arrFileds.Length; i++)
                            {
                                sdrVal = sdr[i].ToString();
                                if (i == 5 || i == 9)
                                {
                                    sdrVal = BIZ_Common.FormatString(sdrVal, "2");
                                    values[i] = BIZ_Common.FormatString(values[i], "2");
                                }
                                if (!string.IsNullOrEmpty(values[i]) && (sdrVal != values[i]))
                                { updateFileds += arrFileds[i] + ","; }
                            }
                        }
                    }
                    sdr.Close();
                }
                catch { if (sdr != null) sdr.Close(); }
                if (updateFileds != "")
                {
                    updateFileds = updateFileds.Substring(0, updateFileds.Length - 1);
                    aryAnalys = updateFileds.Split(',');
                    Update();
                }
            }
            return personID;
        }
        /// <summary>
        /// 身份证号和姓名差异化比对
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        public bool IsPersonNameOK()
        {
            string pName = string.Empty;
            bool returnVa = false;
            try
            {
                string sqlParams = "SELECT PersonName FROM BIZ_Persons WHERE PersonCardID=" + PersonCardID;
                pName = DbHelperSQL.GetSingle(sqlParams).ToString();
                if (pName != this.PersonName) { returnVa = false; }
                else { returnVa = true; }
            }
            catch { returnVa = false; }
            return returnVa;
        }
        /// <summary>
        /// 获取指定孩子性别的孩子数量
        /// </summary>
        /// <param name="childSex"></param>
        /// <returns></returns>
        public string SetPersonChildrenNum(string tPersonID, string childSex)
        {
            string returnVa = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(tPersonID) && !string.IsNullOrEmpty(childSex))
                {
                    returnVa = CommPage.GetSingleValue("SELECT COUNT(0) FROM BIZ_PersonChildren WHERE PersonID=" + tPersonID + " AND ChildSex='" + childSex + "'");
                }
            }
            catch { returnVa = ""; }
            return returnVa;
        }
        /// <summary>
        /// 获取指定群众的孩子数量
        /// </summary>
        /// <param name="childBirthMax">最大孩子的出生日期</param>
        /// <param name="childBirthMin">最小孩子的出生日期</param>
        /// <returns></returns>
        public string SetPersonChildrenNum(string tPersonID, ref string childBirthMax, ref string childBirthMin)
        {
            string returnVa = string.Empty;
            if (!string.IsNullOrEmpty(tPersonID))
            {
                string sqlParams = "SELECT COUNT(0), MIN(ChildBirthday), MAX(ChildBirthday) FROM BIZ_PersonChildren WHERE PersonID=" + tPersonID;
                SqlDataReader sdr = null;
                try
                {
                    sdr = DbHelperSQL.ExecuteReader(sqlParams);
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            returnVa = sdr[0].ToString();
                            childBirthMax = sdr[1].ToString();
                            childBirthMin = sdr[2].ToString();
                        }
                    }
                    else { returnVa = "0"; }
                }
                catch { returnVa = ""; }
            }
            return returnVa;
        }

        // SELECT CommID,PersonID,MarryType,MarryDate,MateName,MateCardID,CreateDate FROM [BIZ_PersonMarryRec]
        // 设置婚姻史
        public bool SetPersonnMarryRec(string tPersonID, string MarryType, string MarryDate, string MateName, string MateCardID)
        {
            string commID = string.Empty;
            bool returnVa = false;
            try
            {
                if (!string.IsNullOrEmpty(tPersonID) && !string.IsNullOrEmpty(MarryType))
                {
                    string sqlParams = "SELECT CommID FROM BIZ_PersonMarryRec WHERE PersonID=" + tPersonID + " AND MarryType='" + MarryType.Trim() + "' AND MarryDate='" + MarryDate + "'";
                    commID = CommPage.GetSingleVal(sqlParams);
                    if (string.IsNullOrEmpty(commID))
                    {
                        sqlParams = "INSERT INTO [BIZ_PersonMarryRec](PersonID,MarryType,MarryDate,MateName,MateCardID) Values(" + tPersonID + ",'" + MarryType + "','" + MarryDate + "','" + MateName + "','" + MateCardID + "') ";
                        if (DbHelperSQL.ExecuteSql(sqlParams) > 0)
                        {
                            returnVa = true;
                        }
                    }
                }
            }
            catch { returnVa = false; }
            return returnVa;
        }


        /// <summary>
        /// 向表插入数据
        /// </summary>
        /// <param name="funcNo">功能号</param>
        /// <returns></returns>
        public int Insert()
        {
            #region SQL参数
            SqlParameter[] paras = new SqlParameter[23];

            paras[0] = new SqlParameter("@PersonAcc", SqlDbType.VarChar, 60);
            paras[0].Value = PersonAcc;
            paras[1] = new SqlParameter("@PersonPwd", SqlDbType.VarChar, 120);
            paras[1].Value = PersonPwd;
            paras[2] = new SqlParameter("@PersonTel", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(PersonTel)) { PersonTel = ""; }
            else { if (BIZ_Common.GetPersonCount(PersonTel, "3")) { PersonTel = ""; } }
            paras[2].Value = "PersonTel";
            paras[3] = new SqlParameter("@PersonMail", SqlDbType.VarChar, 180);
            if (String.IsNullOrEmpty(PersonMail)) { PersonMail = ""; }
            paras[3].Value = PersonMail;
            paras[4] = new SqlParameter("@PersonName", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(PersonName)) { PersonName = ""; }
            paras[4].Value = PersonName;

            paras[5] = new SqlParameter("@PersonCardID", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(PersonCardID)) { PersonCardID = ""; }
            paras[5].Value = PersonCardID;
            paras[6] = new SqlParameter("@PersonSex", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(PersonSex)) { PersonSex = ""; }
            paras[6].Value = PersonSex;
            paras[7] = new SqlParameter("@PersonPhoto", SqlDbType.VarChar, 90);
            if (String.IsNullOrEmpty(PersonPhoto)) { PersonPhoto = ""; }
            paras[7].Value = PersonPhoto;

            if (String.IsNullOrEmpty(PersonBirthday))
            {
                PersonBirthday = "";
                paras[8] = new SqlParameter("@PersonBirthday", SqlDbType.VarChar, 4);
                paras[8].Value = PersonBirthday;
            }
            else
            {
                paras[8] = new SqlParameter("@PersonBirthday", SqlDbType.SmallDateTime, 4);
                paras[8].Value = BIZ_Common.FormatString(PersonBirthday, "2"); ;
            }

            paras[9] = new SqlParameter("@MarryType", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(MarryType)) { MarryType = ""; }
            paras[9].Value = MarryType;

            if (String.IsNullOrEmpty(MarryDate))
            {
                MarryDate = "";
                paras[10] = new SqlParameter("@MarryDate", SqlDbType.VarChar, 4);
                paras[10].Value = MarryDate;
            }
            else
            {
                paras[10] = new SqlParameter("@MarryDate", SqlDbType.SmallDateTime, 4);
                paras[10].Value = BIZ_Common.FormatString(MarryDate, "2"); ;
            }

            paras[11] = new SqlParameter("@BirthType", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(BirthType)) { BirthType = ""; }
            paras[11].Value = BirthType;
            paras[12] = new SqlParameter("@BirthNum", SqlDbType.VarChar, 1);
            if (String.IsNullOrEmpty(BirthNum)) { BirthNum = "0"; }
            paras[12].Value = BirthNum;
            paras[13] = new SqlParameter("@Nations", SqlDbType.VarChar, 90);
            if (String.IsNullOrEmpty(Nations)) { Nations = ""; }
            paras[13].Value = Nations;
            paras[14] = new SqlParameter("@WorkUnit", SqlDbType.VarChar, 180);
            if (String.IsNullOrEmpty(WorkUnit)) { WorkUnit = ""; }
            paras[14].Value = WorkUnit;

            paras[15] = new SqlParameter("@Address", SqlDbType.VarChar, 180);
            if (String.IsNullOrEmpty(Address)) { Address = ""; }
            paras[15].Value = Address;
            paras[16] = new SqlParameter("@RegisterType", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(RegisterType)) { RegisterType = ""; }
            paras[16].Value = RegisterType;
            paras[17] = new SqlParameter("@RegisterAreaCode", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(RegisterAreaCode)) { RegisterAreaCode = ""; }
            paras[17].Value = RegisterAreaCode;
            paras[18] = new SqlParameter("@RegisterAreaName", SqlDbType.VarChar, 180);
            if (String.IsNullOrEmpty(RegisterAreaName)) { RegisterAreaName = ""; }
            paras[18].Value = RegisterAreaName;
            paras[19] = new SqlParameter("@CurrentAreaCode", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(CurrentAreaCode)) { CurrentAreaCode = ""; }
            paras[19].Value = CurrentAreaCode;
            paras[20] = new SqlParameter("@CurrentAreaName", SqlDbType.VarChar, 180);
            if (String.IsNullOrEmpty(CurrentAreaName)) { CurrentAreaName = ""; }
            paras[20].Value = CurrentAreaName;

            paras[21] = new SqlParameter("@MateName", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(MateName)) { MateName = ""; }
            paras[21].Value = MateName;
            paras[22] = new SqlParameter("@MateCardID", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(MateCardID)) { MateCardID = ""; }
            paras[22].Value = MateCardID;

            paras[23] = new SqlParameter("@MarryCardID", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(MarryCardID)) { MarryCardID = ""; }
            paras[23].Value = MarryCardID;
            #endregion
            #region SQL
            // 根据插入字段的不同可以注释 Fields ,从外部传入aryAnalys
            string Fields = "PersonAcc,PersonPwd,PersonTel,PersonMail,PersonName,PersonCardID,PersonSex,PersonPhoto,PersonBirthday,MarryType,MarryDate,MarryCardID,MateName,MateCardID,BirthType,BirthNum,Nations,WorkUnit,Address,RegisterType,RegisterAreaCode,RegisterAreaName,CurrentAreaCode,CurrentAreaName";
            aryAnalys = Fields.Split(',');
            //string sql = "INSERT INTO  [BIZ_Persons] (PersonName,P_Sex,PersonCardID,P_Addres,P_Nation,P_Edu,P_Job,P_Marry,P_FatherName,P_FatherCID,P_MotherName,P_MotherCID) Values (@PersonName,@P_Sex,@PersonCardID,@P_Addres,@P_Nation,@P_Edu,@P_Job,@P_Marry,@P_FatherName,@P_FatherCID,@P_MotherName,@P_MotherCID)";
            string sql = GetInsertSQL(aryAnalys);

            #endregion
            try
            {
                return SqlHelper.ExecuteNonQuery(DbHelperSQL.connectionString, CommandType.Text, sql, paras);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 向表插入数据
        /// </summary>
        /// <param name="personID">返回ID</param>
        /// <returns></returns>
        public int Insert(ref string personID)
        {
            #region SQL参数
            SqlParameter[] paras = new SqlParameter[24];

            paras[0] = new SqlParameter("@PersonAcc", SqlDbType.VarChar, 60);
            paras[0].Value = PersonAcc;
            paras[1] = new SqlParameter("@PersonPwd", SqlDbType.VarChar, 120);
            paras[1].Value = PersonPwd;
            paras[2] = new SqlParameter("@PersonTel", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(PersonTel)) { PersonTel = ""; }
            else { if (BIZ_Common.GetPersonCount(PersonTel, "3")) { PersonTel = ""; } }
            paras[2].Value = PersonTel;
            paras[3] = new SqlParameter("@PersonMail", SqlDbType.VarChar, 180);
            if (String.IsNullOrEmpty(PersonMail)) { PersonMail = ""; }
            paras[3].Value = PersonMail;
            paras[4] = new SqlParameter("@PersonName", SqlDbType.VarChar, 20);
            paras[4].Value = PersonName;

            paras[5] = new SqlParameter("@PersonCardID", SqlDbType.VarChar, 20);
            paras[5].Value = PersonCardID;
            paras[6] = new SqlParameter("@PersonSex", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(PersonSex)) { PersonSex = ""; }
            paras[6].Value = PersonSex;
            paras[7] = new SqlParameter("@PersonPhoto", SqlDbType.VarChar, 90);
            if (String.IsNullOrEmpty(PersonPhoto)) { PersonPhoto = ""; }
            paras[7].Value = PersonPhoto;

            if (String.IsNullOrEmpty(PersonBirthday))
            {
                PersonBirthday = "";
                paras[8] = new SqlParameter("@PersonBirthday", SqlDbType.VarChar, 4);
                paras[8].Value = PersonBirthday;
            }
            else
            {
                paras[8] = new SqlParameter("@PersonBirthday", SqlDbType.SmallDateTime, 4);
                paras[8].Value = BIZ_Common.FormatString(PersonBirthday, "2"); ;
            }

            paras[9] = new SqlParameter("@MarryType", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(MarryType)) { MarryType = ""; }
            paras[9].Value = MarryType;

            if (String.IsNullOrEmpty(MarryDate))
            {
                MarryDate = "";
                paras[10] = new SqlParameter("@MarryDate", SqlDbType.VarChar, 4);
                paras[10].Value = MarryDate;
            }
            else
            {
                paras[10] = new SqlParameter("@MarryDate", SqlDbType.SmallDateTime, 4);
                paras[10].Value = BIZ_Common.FormatString(MarryDate, "2"); ;
            }

            paras[11] = new SqlParameter("@BirthType", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(BirthType)) { BirthType = ""; }
            paras[11].Value = BirthType;
            paras[12] = new SqlParameter("@BirthNum", SqlDbType.VarChar, 1);
            if (String.IsNullOrEmpty(BirthNum)) { BirthNum = "0"; }
            paras[12].Value = BirthNum;
            paras[13] = new SqlParameter("@Nations", SqlDbType.VarChar, 90);
            if (String.IsNullOrEmpty(Nations)) { Nations = ""; }
            paras[13].Value = Nations;
            paras[14] = new SqlParameter("@WorkUnit", SqlDbType.VarChar, 180);
            if (String.IsNullOrEmpty(WorkUnit)) { WorkUnit = ""; }
            paras[14].Value = WorkUnit;

            paras[15] = new SqlParameter("@Address", SqlDbType.VarChar, 180);
            if (String.IsNullOrEmpty(Address)) { Address = ""; }
            paras[15].Value = Address;
            paras[16] = new SqlParameter("@RegisterType", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(RegisterType)) { RegisterType = ""; }
            paras[16].Value = RegisterType;
            paras[17] = new SqlParameter("@RegisterAreaCode", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(RegisterAreaCode)) { RegisterAreaCode = ""; }
            paras[17].Value = RegisterAreaCode;
            paras[18] = new SqlParameter("@RegisterAreaName", SqlDbType.VarChar, 180);
            if (String.IsNullOrEmpty(RegisterAreaName)) { RegisterAreaName = ""; }
            paras[18].Value = RegisterAreaName;
            paras[19] = new SqlParameter("@CurrentAreaCode", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(CurrentAreaCode)) { CurrentAreaCode = ""; }
            paras[19].Value = CurrentAreaCode;
            paras[20] = new SqlParameter("@CurrentAreaName", SqlDbType.VarChar, 180);
            if (String.IsNullOrEmpty(CurrentAreaName)) { CurrentAreaName = ""; }
            paras[20].Value = CurrentAreaName;

            paras[21] = new SqlParameter("@MateName", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(MateName)) { MateName = ""; }
            paras[21].Value = MateName;
            paras[22] = new SqlParameter("@MateCardID", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(MateCardID)) { MateCardID = ""; }
            paras[22].Value = MateCardID;


            paras[23] = new SqlParameter("@MarryCardID", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(MarryCardID)) { MarryCardID = ""; }
            paras[23].Value = MarryCardID;
            #endregion
            #region SQL
            // 根据插入字段的不同可以注释 Fields ,从外部传入aryAnalys
            string Fields = "PersonAcc,PersonPwd,PersonTel,PersonMail,PersonName,PersonCardID,PersonSex,PersonPhoto,PersonBirthday,MarryType,MarryDate,MarryCardID,MateName,MateCardID,BirthType,BirthNum,Nations,WorkUnit,Address,RegisterType,RegisterAreaCode,RegisterAreaName,CurrentAreaCode,CurrentAreaName";
            aryAnalys = Fields.Split(',');
            //string sql = "INSERT INTO  [BIZ_Persons] (PersonName,P_Sex,PersonCardID,P_Addres,P_Nation,P_Edu,P_Job,P_Marry,P_FatherName,P_FatherCID,P_MotherName,P_MotherCID) Values (@PersonName,@P_Sex,@PersonCardID,@P_Addres,@P_Nation,@P_Edu,@P_Job,@P_Marry,@P_FatherName,@P_FatherCID,@P_MotherName,@P_MotherCID)";
            string sql = GetInsertSQL(aryAnalys);

            #endregion
            try
            {
                personID = DbHelperSQL.GetSingle(sql, paras).ToString();
                return 1;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        } /// <summary>
        /// 获取查询的字段All
        /// </summary>
        /// <returns></returns>
        public void SelectAll()
        {
            string sql = string.Empty;
            if (String.IsNullOrEmpty(SearchWhere))
            {
                sql = "SELECT * FROM BIZ_Persons WHERE PersonID=" + this.PersonID;
            }
            else
            {
                sql = "SELECT * FROM BIZ_Persons WHERE " + SearchWhere;
            }
            SqlDataReader sdr = null;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sql);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        PersonID = sdr["PersonID"].ToString();
                        PersonAcc = sdr["PersonAcc"].ToString();
                        PersonPwd = sdr["PersonPwd"].ToString();
                        PersonTel = BIZ_Common.FormatString(sdr["PersonTel"].ToString(), "1");
                        PersonMail = sdr["PersonMail"].ToString();
                        PersonName = sdr["PersonName"].ToString();
                        PersonCardID = BIZ_Common.FormatString(sdr["PersonCardID"].ToString(), "0");
                        PersonSex = sdr["PersonSex"].ToString();
                        PersonPhoto = sdr["PersonPhoto"].ToString();
                        PersonBirthday = BIZ_Common.FormatString(sdr["PersonBirthday"].ToString(), "2");
                        AreaCode = sdr["AreaCode"].ToString();
                        AreaName = sdr["AreaName"].ToString();
                        MarryType = sdr["MarryType"].ToString();
                        MarryDate = BIZ_Common.FormatString(sdr["MarryDate"].ToString(), "2");
                        MarryCardID = sdr["MarryCardID"].ToString();
                        BirthType = sdr["BirthType"].ToString();
                        BirthNum = sdr["BirthNum"].ToString();
                        Nations = sdr["Nations"].ToString();
                        WorkUnit = sdr["WorkUnit"].ToString();
                        Address = sdr["Address"].ToString();
                        RegisterType = sdr["RegisterType"].ToString();
                        RegisterAreaCode = sdr["RegisterAreaCode"].ToString();
                        RegisterAreaName = sdr["RegisterAreaName"].ToString();
                        CurrentAreaCode = sdr["CurrentAreaCode"].ToString();
                        CurrentAreaName = sdr["CurrentAreaName"].ToString();
                        MateName = sdr["MateName"].ToString();
                        MateCardID = BIZ_Common.FormatString(sdr["MateCardID"].ToString(), "0");
                        CreateDate = sdr["CreateDate"].ToString();
                        Attribs = sdr["Attribs"].ToString();
                    }
                }
                sdr.Close(); sdr.Dispose();
            }
            catch (Exception ex)
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                throw (ex);
            }
        }
        /// <summary>
        /// 获取查询的字段
        /// </summary>
        /// <param name="aAnaly"></param>
        /// <returns></returns>
        private string GetSelectSQL(string[] aAnaly)
        {
            string returnVal = string.Empty;
            string returnA = "";
            for (int i = 0; i < aAnaly.Length; i++)
            {
                if (aAnaly[i] != "null")
                {
                    returnA += aAnaly[i] + ",";
                }
            }
            if (returnA != "") returnA = returnA.Substring(0, returnA.Length - 1);
            //returnVal = "INSERT INTO  [BIZ_Persons](" + returnA + ",AreaCode,AreaName,ReportDate) Values(" + returnB + ",@AreaCode,@AreaName,@ReportDate) ";
            returnVal = returnA;

            return returnVal;
        }

        /// <summary>
        /// 获取新增的字段
        /// </summary>
        /// <param name="aAnaly"></param>
        /// <returns></returns>
        private string GetInsertSQL(string[] aAnaly)
        {
            string returnVal = string.Empty;
            string returnA = "";
            string returnB = "";
            for (int i = 0; i < aAnaly.Length; i++)
            {
                if (aAnaly[i] != "null")
                {
                    returnA += aAnaly[i] + ",";
                    returnB += "@" + aAnaly[i].Trim() + ",";
                }
            }
            if (returnA != "") returnA = returnA.Substring(0, returnA.Length - 1);
            if (returnB != "") returnB = returnB.Substring(0, returnB.Length - 1);

            //returnVal = "INSERT INTO  [BIZ_Persons](" + returnA + ",AreaCode,AreaName,ReportDate) Values(" + returnB + ",@AreaCode,@AreaName,@ReportDate) ";
            returnVal = "INSERT INTO  [BIZ_Persons](" + returnA + ") Values(" + returnB + ") SELECT SCOPE_IDENTITY();";

            return returnVal;
        }

        /// <summary>
        /// 获取更新的字段信息
        /// </summary>
        /// <param name="aAnaly"></param>
        /// <returns></returns>
        private string GetUpdateSQL(string[] aAnaly)
        {
            string returnVal = string.Empty;
            string returnA = "";
            for (int i = 0; i < aAnaly.Length; i++)
            {
                if (aAnaly[i] != "null")
                {
                    returnA += aAnaly[i] + "=@" + aAnaly[i].Trim() + ",";
                }
            }
            if (returnA != "") returnA = returnA.Substring(0, returnA.Length - 1);

            //returnVal = "Update BIZ_Persons Set " + returnA + ",AreaCode=@AreaCode,AreaName=@AreaCode,ReportDate=@AreaCode Where 1=1 and PersonCardID = @PersonCardID";
            returnVal = "Update BIZ_Persons Set " + returnA + " Where 1=1 and PersonCardID = @PersonCardID OR PersonID=@PersonID";
            return returnVal;
        }
        /*
       // SELECT 
       * PersonID;
       * PersonAcc;PersonPwd;PersonTel;PersonMail;PersonName;
       * PersonCardID;PersonSex;PersonPhoto;PersonBirthday;AreaCode;AreaName;
       * MarryType;MarryDate;BirthType;BirthNum;Nations;
       * WorkUnit;Address;RegisterType;RegisterAreaCode;RegisterAreaName;CurrentAreaCode;CurrentAreaName;
       * MateName;MateCardID;CreateDate;Attribs
       * 
       * ] FROM [BIZ_Persons]

         
       * INSERT INTO [BIZ_Persons]([
       PersonID,PersonAcc,PersonPwd,PersonTel,PersonMail,PersonName,
       PersonCardID,PersonSex,PersonPhoto,PersonBirthday,AreaCode,AreaName,
       MarryType,MarryDate,BirthType,BirthNum,Nations,WorkUnit,Address,RegisterType,RegisterAreaCode,RegisterAreaName,CurrentAreaCode,CurrentAreaName
       ,MateName,MateCardID,CreateDate,Attribs])
VALUES(
       <PersonID,int,>, <PersonAcc,varchar(60),>, <PersonPwd,varchar(120),>, <PersonTel,varchar(50),>, <PersonMail,varchar(180),>, <PersonName,varchar(20),>, 
       <PersonCardID,varchar(20),>, <PersonSex,varchar(20),>, <PersonPhoto,varchar(90),>, <PersonBirthday,smalldatetime,>, <AreaCode,varchar(20),>, <AreaName,varchar(60),>, 
       <MarryType,varchar(50),>, <MarryDate,smalldatetime,>, <BirthType,varchar(50),>, <BirthNum,tinyint,>, <Nations,varchar(90),>, <WorkUnit,varchar(180),>, 
       <Address,varchar(180),>, <RegisterType,varchar(50),>, <RegisterAreaCode,varchar(20),>, <RegisterAreaName,varchar(180),>, <CurrentAreaCode,varchar(20),>, <CurrentAreaName,varchar(180),>, 
       <MateName,varchar(20),>, <MateCardID,varchar(20),>, <CreateDate,smalldatetime,>, <Attribs,tinyint,>)
       */
        /// <summary>
        /// 更新表中的指定数据
        /// </summary>
        /// <param name="funcNo">功能编号</param>
        /// <returns></returns>
        public int Update()
        {
            #region SQL参数
            SqlParameter[] paras = new SqlParameter[22];
            paras[0] = new SqlParameter("@PersonID", SqlDbType.Int, 4);
            paras[0].Value = PersonID;

            paras[1] = new SqlParameter("@PersonMail", SqlDbType.VarChar, 180);
            if (String.IsNullOrEmpty(PersonMail)) { PersonMail = ""; }
            paras[1].Value = PersonMail;
            paras[2] = new SqlParameter("@PersonName", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(PersonName)) { PersonName = ""; }
            paras[2].Value = PersonName;
            paras[3] = new SqlParameter("@PersonCardID", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(PersonCardID)) { PersonCardID = ""; }
            paras[3].Value = PersonCardID;
            paras[4] = new SqlParameter("@PersonSex", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(PersonSex)) { PersonSex = ""; }
            paras[4].Value = PersonSex;

            if (String.IsNullOrEmpty(PersonBirthday))
            {
                PersonBirthday = "";
                paras[5] = new SqlParameter("@PersonBirthday", SqlDbType.VarChar, 4);
                paras[5].Value = PersonBirthday;
            }
            else
            {
                paras[5] = new SqlParameter("@PersonBirthday", SqlDbType.SmallDateTime, 4);
                paras[5].Value = BIZ_Common.FormatString(PersonBirthday, "2"); ;
            }

            paras[6] = new SqlParameter("@MarryType", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(MarryType)) { MarryType = ""; }
            paras[6].Value = MarryType;

            if (String.IsNullOrEmpty(MarryDate))
            {
                MarryDate = "";
                paras[7] = new SqlParameter("@MarryDate", SqlDbType.VarChar, 4);
                paras[7].Value = MarryDate;
            }
            else
            {
                paras[7] = new SqlParameter("@MarryDate", SqlDbType.SmallDateTime, 4);
                paras[7].Value = BIZ_Common.FormatString(MarryDate, "2"); ;
            }

            paras[8] = new SqlParameter("@BirthType", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(BirthType)) { BirthType = ""; }
            paras[8].Value = BirthType;

            paras[9] = new SqlParameter("@BirthNum", SqlDbType.VarChar, 1);
            if (String.IsNullOrEmpty(BirthNum)) { BirthNum = "0"; }
            paras[9].Value = BirthNum;
            paras[10] = new SqlParameter("@Nations", SqlDbType.VarChar, 90);
            if (String.IsNullOrEmpty(Nations)) { Nations = ""; }
            paras[10].Value = Nations;
            paras[11] = new SqlParameter("@WorkUnit", SqlDbType.VarChar, 180);
            if (String.IsNullOrEmpty(WorkUnit)) { WorkUnit = ""; }
            paras[11].Value = WorkUnit;
            paras[12] = new SqlParameter("@Address", SqlDbType.VarChar, 180);
            if (String.IsNullOrEmpty(Address)) { Address = ""; }
            paras[12].Value = Address;
            paras[13] = new SqlParameter("@RegisterType", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(RegisterType)) { RegisterType = ""; }
            paras[13].Value = RegisterType;

            paras[14] = new SqlParameter("@RegisterAreaCode", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(RegisterAreaCode)) { RegisterAreaCode = ""; }
            paras[14].Value = RegisterAreaCode;
            paras[15] = new SqlParameter("@RegisterAreaName", SqlDbType.VarChar, 180);
            if (String.IsNullOrEmpty(RegisterAreaName)) { RegisterAreaName = ""; }
            paras[15].Value = RegisterAreaName;
            paras[16] = new SqlParameter("@CurrentAreaCode", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(CurrentAreaCode)) { CurrentAreaCode = ""; }
            paras[16].Value = CurrentAreaCode;
            paras[17] = new SqlParameter("@CurrentAreaName", SqlDbType.VarChar, 180);
            if (String.IsNullOrEmpty(CurrentAreaName)) { CurrentAreaName = ""; }
            paras[17].Value = CurrentAreaName;

            paras[18] = new SqlParameter("@MateName", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(MateName)) { MateName = ""; }
            paras[18].Value = MateName;
            paras[19] = new SqlParameter("@MateCardID", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(MateCardID)) { MateCardID = ""; }
            paras[19].Value = MateCardID;
            paras[20] = new SqlParameter("@PersonTel", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(PersonTel)) { PersonTel = ""; }
            paras[20].Value = PersonTel;

            paras[21] = new SqlParameter("@MarryCardID", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(MarryCardID)) { MarryCardID = ""; }
            paras[21].Value = MarryCardID;

            StringBuilder sb = new StringBuilder();
            string wherestr = "";
            foreach (SqlParameter sq in paras)
            {
                string sq_value = "NULL";
                if (!string.IsNullOrEmpty(sq.Value.ToString()))
                {
                    sq_value = sq.Value.ToString();
                }
                if (sq.ParameterName != "@PersonID" && !string.IsNullOrEmpty(sq.Value.ToString()))
                {
                    if (sq.SqlDbType == SqlDbType.SmallDateTime || sq.SqlDbType == SqlDbType.DateTime || sq.SqlDbType == SqlDbType.VarChar)
                    {
                        sb.Append(sq.ParameterName.TrimStart('@') + "='" + sq_value + "',");
                    }
                    else
                    {
                        sb.Append(sq.ParameterName.TrimStart('@') + "=" + sq_value + ",");
                    }
                }
                if (sq.ParameterName == "@PersonID")
                {

                    if (!string.IsNullOrEmpty(sq.Value.ToString()))
                    {
                        wherestr = " where PersonID=" + sq_value;
                        continue;
                    }
                    else
                    {
                        return 0;
                    }
                    continue;
                }
            }

            #endregion
            #region SQL
            // sql = "Update BIZ_Persons Set PersonName=@PersonName,P_Sex=@P_Sex,P_Addres=@P_Addres,P_Edu=@P_Edu,P_Job=@P_Job,P_Marry=@P_Marry,AreaCode=@AreaCode,AreaName=@AreaName Where 1=1 and PersonCardID = @PersonCardID ";
            // string Fields = "PersonTel,PersonMail,PersonName,PersonCardID,PersonSex,PersonBirthday,MarryType,MarryDate,BirthType,BirthNum,Nations,WorkUnit,Address,RegisterType,RegisterAreaCode,RegisterAreaName,CurrentAreaCode,CurrentAreaName";
            //aryAnalys = Fields.Split(','); 
            //string sql = GetUpdateSQL(aryAnalys);

            #endregion
            try
            {
                DbHelperSQL.ExecuteSql("Update BIZ_Persons Set " + sb.ToString().TrimEnd(',') + wherestr + " ");
                return 1;
                //return SqlHelper.ExecuteNonQuery(DbHelperSQL.connectionString, CommandType.Text, sql, paras);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

    }
}
