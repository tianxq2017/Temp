using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using UNV.Comm.DataBase;

namespace join.pms.dal
{
    public class PIS_Persons
    {
        public string P_ID;
        public string P_Name;
        public string P_Sex;
        public string P_CardID;
        public string P_Addres;
        public string P_Nation;
        public string P_Edu;
        public string P_Job;
        public string P_Marry;
        public string P_Tel;
        public string P_TransType;
        public string P_TransDate;
        public string P_FatherName;
        public string P_FatherCID;
        public string P_MotherName;
        public string P_MotherCID;
        public string Attribs;
        public string CreateDate;
        public string ReportDate;
        public string AreaCode;
        public string AreaName;

        // [P_ID], [AreaCode], [AreaName], [P_CardID], [OprateDate], [BirthDate], [MarryDate], 
        // 姓名0,性别1,身份证号2,婚姻状况3,户籍所在地4,现居住地5,配偶姓名,配偶身份证号,父亲姓名6,父亲身份证号,母亲姓名,母亲身份证号

        //public string[] aryTitles;
        public string[] aryAnalys;
        /// <summary>
        /// 该身份证号所关联的信息是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsPersonExist() {
            if (!string.IsNullOrEmpty(P_CardID)) {
                string sql = "SELECT COUNT(*) FROM PIS_Persons WHERE P_CardID='" + P_CardID+"'";
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
        /// 婚姻状况在核心库中比对
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        public bool IsPersonMarryOK()
        {
            string pMarry = string.Empty;
            object objTmp;
            bool returnVa = false;
            try
            {
                string sqlParams = "SELECT P_Marry FROM PIS_Persons WHERE P_CardID="+P_CardID;
                objTmp = DbHelperSQL.GetSingle(sqlParams);
                if (objTmp != null) {
                    pMarry = objTmp.ToString();
                    if (pMarry != this.P_Marry) { returnVa = false; }
                    else { returnVa = true; }
                }
            }
            catch { returnVa = false; }
            return returnVa;
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
                string sqlParams = "SELECT P_Name FROM PIS_Persons WHERE P_CardID=" + P_CardID;
                pName = DbHelperSQL.GetSingle(sqlParams).ToString();
                if (pName != this.P_Name) {returnVa = false;}
                else {returnVa = true;}
            }
            catch { returnVa = false; }
            return returnVa;
        }

        /// <summary>
        /// 在全员库查询数据中分析
        /// </summary>
        /// <param name="qykCode"></param>
        /// <returns></returns>
        public bool IsPersonQykExist(string qykCode)
        {
            if (!string.IsNullOrEmpty(P_CardID))
            {
                string sql = "SELECT COUNT(*) FROM PIS_QYK WHERE FuncNo='" + qykCode + "' AND P_CardID='" + P_CardID + "'";
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
        /// 在全员库查询数据中分析
        /// </summary>
        /// <param name="qykCode"></param>
        /// <param name="nameFields"></param>
        /// <returns></returns>
        public bool IsPersonNameQykOK(string qykCode,string nameFields)
        {
            string pName = string.Empty;
            bool returnVa = false;
            try
            {
                string sqlParams = "SELECT " + nameFields + " FROM PIS_QYK WHERE FuncNo='" + qykCode + "' AND P_CardID=" + P_CardID;
                pName = DbHelperSQL.GetSingle(sqlParams).ToString();
                if (pName != this.P_Name) { returnVa = false; }
                else { returnVa = true; }
            }
            catch { returnVa = false; }
            return returnVa;
        }

        public bool IsBaseQykExist(string qykCode)
        {
            if (!string.IsNullOrEmpty(P_CardID))
            {
                string sql = "SELECT COUNT(*) FROM PIS_QYK WHERE Attribs=0 AND FuncNo='" + qykCode + "' AND P_CardID='" + P_CardID + "'";
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

        public string GetBaseQykKeys(string qykCode)
        {
            string returnVa = string.Empty;
            // 姓名2,性别3,身份证号4,婚姻状况11,户籍所在地14,现居住地15,父亲姓名18,父亲身份证号19,母亲姓名20,母亲身份证号21
            string sqlParams = "SELECT Fileds02,Fileds03,Fileds04,Fileds11,Fileds14,Fileds15,Fileds18,Fileds19,Fileds20,Fileds21 FROM PIS_QYK WHERE FuncNo='0501' AND P_CardID='" + P_CardID + "'";
            SqlDataReader sdr = null;
            int i = 0;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                while (sdr.Read())
                {
                    returnVa = sdr[0].ToString().Trim()+",";
                    returnVa += sdr[1].ToString().Trim() + ",";
                    returnVa += sdr[2].ToString().Trim() + ",";
                    returnVa += sdr[3].ToString().Trim() + ",";
                    returnVa += sdr[4].ToString().Trim() + ",";
                    returnVa += sdr[5].ToString().Trim() + ",";
                    returnVa += sdr[6].ToString().Trim() + ",";
                    returnVa += sdr[7].ToString().Trim() + ",";
                    returnVa += sdr[8].ToString().Trim() + ",";
                    returnVa += sdr[9].ToString().Trim();
                }
                sdr.Close();
            }
            catch { if (sdr != null) sdr.Close(); }
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
            SqlParameter[] paras = new SqlParameter[18];

            paras[0] = new SqlParameter("@P_Name", SqlDbType.VarChar, 50);
            paras[0].Value = P_Name;
            paras[1] = new SqlParameter("@P_Sex", SqlDbType.VarChar, 2);
            paras[1].Value = P_Sex;
            paras[2] = new SqlParameter("@P_CardID", SqlDbType.VarChar, 20);
            paras[2].Value = P_CardID;
            paras[3] = new SqlParameter("@P_Addres", SqlDbType.VarChar, 100);
            paras[3].Value = P_Addres;
            paras[4] = new SqlParameter("@P_Nation", SqlDbType.VarChar, 50);
            paras[4].Value = P_Nation;

            paras[5] = new SqlParameter("@P_Edu", SqlDbType.VarChar, 50);
            paras[5].Value = P_Edu;
            paras[6] = new SqlParameter("@P_Job", SqlDbType.VarChar, 50);
            paras[6].Value = P_Job;
            paras[7] = new SqlParameter("@P_Marry", SqlDbType.VarChar, 50);
            paras[7].Value = P_Marry;
            paras[8] = new SqlParameter("@P_FatherName", SqlDbType.VarChar, 50);
            paras[8].Value = P_FatherName;
            paras[9] = new SqlParameter("@P_FatherCID", SqlDbType.VarChar, 20);
            paras[9].Value = P_FatherCID;

            paras[10] = new SqlParameter("@P_MotherName", SqlDbType.VarChar, 50);
            paras[10].Value = P_MotherName;
            paras[11] = new SqlParameter("@P_MotherCID", SqlDbType.VarChar, 20);
            paras[11].Value = P_MotherCID;
            paras[12] = new SqlParameter("@P_TransType", SqlDbType.VarChar, 20);
            paras[12].Value = P_TransType;
            paras[13] = new SqlParameter("@P_TransDate", SqlDbType.SmallDateTime, 4);
            paras[13].Value = P_TransDate;
            paras[14] = new SqlParameter("@P_Tel", SqlDbType.VarChar, 50);
            paras[14].Value = P_Tel;

            paras[15] = new SqlParameter("@AreaCode", SqlDbType.VarChar, 50);
            paras[15].Value = AreaCode;
            paras[16] = new SqlParameter("@AreaName", SqlDbType.VarChar, 80);
            paras[16].Value = AreaName;
            paras[17] = new SqlParameter("@ReportDate", SqlDbType.SmallDateTime, 4);
            paras[17].Value = ReportDate;
            /*
020401	新生儿落户信息
020402	迁移人员信息 
020403	暂住人员信息
             <Titles>姓名,性别,婚姻状况,户籍地址,身份证号,文化程度,现在职业,原居住地,现暂住地,随行人姓名,性别,出生年月,与本人关系,数据日期,状态</Titles>
	  <Fields>Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds11,Fileds12,Fileds13,Fileds14,ReportDate,AuditFlagCN</Fields>
             */
            #endregion
            #region SQL
            //string sql = "INSERT INTO  [PIS_Persons] (P_Name,P_Sex,P_CardID,P_Addres,P_Nation,P_Edu,P_Job,P_Marry,P_FatherName,P_FatherCID,P_MotherName,P_MotherCID) Values (@P_Name,@P_Sex,@P_CardID,@P_Addres,@P_Nation,@P_Edu,@P_Job,@P_Marry,@P_FatherName,@P_FatherCID,@P_MotherName,@P_MotherCID)";
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
                    returnB += "@"+aAnaly[i] + ",";
                }
            }
            if (returnA != "") returnA = returnA.Substring(0, returnA.Length-1);
            if (returnB != "") returnB = returnB.Substring(0, returnB.Length - 1);

            returnVal = "INSERT INTO  [PIS_Persons](" + returnA + ",AreaCode,AreaName,ReportDate) Values(" + returnB + ",@AreaCode,@AreaName,@ReportDate) ";

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
                    returnA += aAnaly[i] + "=@" + aAnaly[i] + ",";
                }
            }
            if (returnA != "") returnA = returnA.Substring(0, returnA.Length - 1);

            returnVal = "Update PIS_Persons Set " + returnA + ",AreaCode=@AreaCode,AreaName=@AreaCode,ReportDate=@AreaCode Where 1=1 and P_CardID = @P_CardID";
            return returnVal;
        }

        /// <summary>
        /// 更新表中的指定数据
        /// </summary>
        /// <param name="funcNo">功能编号</param>
        /// <returns></returns>
        public int Update()
        {
            #region SQL参数
            SqlParameter[] paras = new SqlParameter[18];

            paras[0] = new SqlParameter("@P_Name", SqlDbType.VarChar, 50);
            paras[0].Value = P_Name;
            paras[1] = new SqlParameter("@P_Sex", SqlDbType.VarChar, 2);
            paras[1].Value = P_Sex;
            paras[2] = new SqlParameter("@P_CardID", SqlDbType.VarChar, 20);
            paras[2].Value = P_CardID;
            paras[3] = new SqlParameter("@P_Addres", SqlDbType.VarChar, 100);
            paras[3].Value = P_Addres;
            paras[4] = new SqlParameter("@P_Nation", SqlDbType.VarChar, 50);
            paras[4].Value = P_Nation;

            paras[5] = new SqlParameter("@P_Edu", SqlDbType.VarChar, 50);
            paras[5].Value = P_Edu;
            paras[6] = new SqlParameter("@P_Job", SqlDbType.VarChar, 50);
            paras[6].Value = P_Job;
            paras[7] = new SqlParameter("@P_Marry", SqlDbType.VarChar, 50);
            paras[7].Value = P_Marry;
            paras[8] = new SqlParameter("@P_FatherName", SqlDbType.VarChar, 50);
            paras[8].Value = P_FatherName;
            paras[9] = new SqlParameter("@P_FatherCID", SqlDbType.VarChar, 20);
            paras[9].Value = P_FatherCID;

            paras[10] = new SqlParameter("@P_MotherName", SqlDbType.VarChar, 50);
            paras[10].Value = P_MotherName;
            paras[11] = new SqlParameter("@P_MotherCID", SqlDbType.VarChar, 20);
            paras[11].Value = P_MotherCID;
            paras[12] = new SqlParameter("@P_TransType", SqlDbType.VarChar, 20);
            paras[12].Value = P_TransType;
            paras[13] = new SqlParameter("@P_TransDate", SqlDbType.SmallDateTime, 4);
            paras[13].Value = P_TransDate;
            paras[14] = new SqlParameter("@P_Tel", SqlDbType.VarChar, 50);
            paras[14].Value = P_Tel;

            paras[15] = new SqlParameter("@AreaCode", SqlDbType.VarChar, 50);
            paras[15].Value = AreaCode;
            paras[16] = new SqlParameter("@AreaName", SqlDbType.VarChar, 80);
            paras[16].Value = AreaName;
            paras[17] = new SqlParameter("@ReportDate", SqlDbType.VarChar, 80);
            paras[17].Value = ReportDate;
            #endregion
            #region SQL
            // sql = "Update PIS_Persons Set P_Name=@P_Name,P_Sex=@P_Sex,P_Addres=@P_Addres,P_Edu=@P_Edu,P_Job=@P_Job,P_Marry=@P_Marry,AreaCode=@AreaCode,AreaName=@AreaName Where 1=1 and P_CardID = @P_CardID ";
            string sql = GetUpdateSQL(aryAnalys);
            
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

       

    }
}
