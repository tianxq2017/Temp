using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using UNV.Comm.DataBase;
namespace join.pms.dal
{
    public class BIZ_PersonChildren
    {
        #region 字段信息
        public string CommID;
        public string PersonID;
        public string BizID;

        public string CommID1;
        public string ChildName1;
        public string ChildSex1;
        public string ChildBirthday1;
        public string ChildDeathday1;
        public string ChildDeathAudit1;
        public string ChildCardID1;
        public string ChildSource1;
        public string ChildPolicy1;
        public string ChildSurvivalStatus1;
        public string ChildNo1;
        public string FatherName1;
        public string MotherName1;
        public string Memos1;
        public string CreateDate1;

        public string CommID2;
        public string ChildName2;
        public string ChildSex2;
        public string ChildBirthday2;
        public string ChildDeathday2;
        public string ChildDeathAudit2;
        public string ChildCardID2;
        public string ChildSource2;
        public string ChildPolicy2;
        public string ChildSurvivalStatus2;
        public string ChildNo2;
        public string FatherName2;
        public string MotherName2;
        public string Memos2;
        public string CreateDate2;

        public string CommID3;
        public string ChildName3;
        public string ChildSex3;
        public string ChildBirthday3;
        public string ChildDeathday3;
        public string ChildDeathAudit3;
        public string ChildCardID3;
        public string ChildSource3;
        public string ChildPolicy3;
        public string ChildSurvivalStatus3;
        public string ChildNo3;
        public string FatherName3;
        public string MotherName3;
        public string Memos3;
        public string CreateDate3;

        public string CommID4;
        public string ChildName4;
        public string ChildSex4;
        public string ChildBirthday4;
        public string ChildDeathday4;
        public string ChildDeathAudit4;
        public string ChildCardID4;
        public string ChildSource4;
        public string ChildPolicy4;
        public string ChildSurvivalStatus4;
        public string ChildNo4;
        public string FatherName4;
        public string MotherName4;
        public string Memos4;
        public string CreateDate4;

        public string CommID5;
        public string ChildName5;
        public string ChildSex5;
        public string ChildBirthday5;
        public string ChildDeathday5;
        public string ChildDeathAudit5;
        public string ChildCardID5;
        public string ChildSource5;
        public string ChildPolicy5;
        public string ChildSurvivalStatus5;
        public string ChildNo5;
        public string FatherName5;
        public string MotherName5;
        public string Memos5;
        public string CreateDate5;

        public string CommID6;
        public string ChildName6;
        public string ChildSex6;
        public string ChildBirthday6;
        public string ChildDeathday6;
        public string ChildDeathAudit6;
        public string ChildCardID6;
        public string ChildSource6;
        public string ChildPolicy6;
        public string ChildSurvivalStatus6;
        public string ChildNo6;
        public string FatherName6;
        public string MotherName6;
        public string Memos6;
        public string CreateDate6;
        #endregion

        /// <summary>
        /// 获取条件的子女情侣
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public void Select(string strSql)
        {
            if (String.IsNullOrEmpty(strSql)) { strSql = "1=1"; }
            SqlDataReader sdr = null;
            try
            {
                string sqlParams = "SELECT TOP 6 CommID,PersonID,ChildName, ChildSex, ChildBirthday, ChildDeathday, ChildDeathAudit, ChildCardID, ChildSource, ChildPolicy, ChildSurvivalStatus,ChildNo, FatherName, MotherName, Memos, CreateDate FROM BIZ_PersonChildren WHERE  " + strSql;

                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    int i = 0;
                    while (sdr.Read())
                    {
                        if (i == 0)
                        {
                            CommID1 = sdr["CommID"].ToString();
                            ChildName1 = sdr["ChildName"].ToString();
                            ChildSex1 = sdr["ChildSex"].ToString();
                            ChildBirthday1 = BIZ_Common.FormatString(sdr["ChildBirthday"].ToString(), "2");
                            ChildDeathday1 = BIZ_Common.FormatString(sdr["ChildDeathday"].ToString(), "2");
                            ChildDeathAudit1 = sdr["ChildDeathAudit"].ToString();
                            ChildCardID1 = sdr["ChildCardID"].ToString();
                            ChildSource1 = sdr["ChildSource"].ToString();
                            ChildPolicy1 = sdr["ChildPolicy"].ToString();
                            ChildSurvivalStatus1 = sdr["ChildSurvivalStatus"].ToString();
                            ChildNo1 = sdr["ChildNo"].ToString();
                            FatherName1 = sdr["FatherName"].ToString();
                            MotherName1 = sdr["MotherName"].ToString();
                            Memos1 = sdr["Memos"].ToString();
                        }
                        else if (i == 1)
                        {
                            CommID2 = sdr["CommID"].ToString();
                            ChildName2 = sdr["ChildName"].ToString();
                            ChildSex2 = sdr["ChildSex"].ToString();
                            ChildBirthday2 = BIZ_Common.FormatString(sdr["ChildBirthday"].ToString(), "2");
                            ChildDeathday2 = BIZ_Common.FormatString(sdr["ChildDeathday"].ToString(), "2");
                            ChildDeathAudit2 = sdr["ChildDeathAudit"].ToString();
                            ChildCardID2 = sdr["ChildCardID"].ToString();
                            ChildSource2 = sdr["ChildSource"].ToString();
                            ChildPolicy2 = sdr["ChildPolicy"].ToString();
                            ChildSurvivalStatus2 = sdr["ChildSurvivalStatus"].ToString();
                            ChildNo2 = sdr["ChildNo"].ToString();
                            FatherName2 = sdr["FatherName"].ToString();
                            MotherName2 = sdr["MotherName"].ToString();
                            Memos2 = sdr["Memos"].ToString();
                        }
                        else if (i == 2)
                        {
                            CommID3 = sdr["CommID"].ToString();
                            ChildName3 = sdr["ChildName"].ToString();
                            ChildSex3 = sdr["ChildSex"].ToString();
                            ChildBirthday3 = BIZ_Common.FormatString(sdr["ChildBirthday"].ToString(), "2");
                            ChildDeathday3 = BIZ_Common.FormatString(sdr["ChildDeathday"].ToString(), "2");
                            ChildDeathAudit3 = sdr["ChildDeathAudit"].ToString();
                            ChildCardID3 = sdr["ChildCardID"].ToString();
                            ChildSource3 = sdr["ChildSource"].ToString();
                            ChildPolicy3 = sdr["ChildPolicy"].ToString();
                            ChildSurvivalStatus3 = sdr["ChildSurvivalStatus"].ToString();
                            ChildNo3 = sdr["ChildNo"].ToString();
                            FatherName3 = sdr["FatherName"].ToString();
                            MotherName3 = sdr["MotherName"].ToString();
                            Memos3 = sdr["Memos"].ToString();
                        }
                        else if (i == 3)
                        {
                            CommID4 = sdr["CommID"].ToString();
                            ChildName4 = sdr["ChildName"].ToString();
                            ChildSex4 = sdr["ChildSex"].ToString();
                            ChildBirthday4 = BIZ_Common.FormatString(sdr["ChildBirthday"].ToString(), "2");
                            ChildDeathday4 = BIZ_Common.FormatString(sdr["ChildDeathday"].ToString(), "2");
                            ChildDeathAudit4 = sdr["ChildDeathAudit"].ToString();
                            ChildCardID4 = sdr["ChildCardID"].ToString();
                            ChildSource4 = sdr["ChildSource"].ToString();
                            ChildPolicy4 = sdr["ChildPolicy"].ToString();
                            ChildSurvivalStatus4 = sdr["ChildSurvivalStatus"].ToString();
                            ChildNo4 = sdr["ChildNo"].ToString();
                            FatherName4 = sdr["FatherName"].ToString();
                            MotherName4 = sdr["MotherName"].ToString();
                            Memos4 = sdr["Memos"].ToString();
                        }
                        else if (i == 4)
                        {
                            CommID5 = sdr["CommID"].ToString();
                            ChildName5 = sdr["ChildName"].ToString();
                            ChildSex5 = sdr["ChildSex"].ToString();
                            ChildBirthday5 = BIZ_Common.FormatString(sdr["ChildBirthday"].ToString(), "2");
                            ChildDeathday5 = BIZ_Common.FormatString(sdr["ChildDeathday"].ToString(), "2");
                            ChildDeathAudit5 = sdr["ChildDeathAudit"].ToString();
                            ChildCardID5 = sdr["ChildCardID"].ToString();
                            ChildSource5 = sdr["ChildSource"].ToString();
                            ChildPolicy5 = sdr["ChildPolicy"].ToString();
                            ChildSurvivalStatus5 = sdr["ChildSurvivalStatus"].ToString();
                            ChildNo5 = sdr["ChildNo"].ToString();
                            FatherName5 = sdr["FatherName"].ToString();
                            MotherName5 = sdr["MotherName"].ToString();
                            Memos5 = sdr["Memos"].ToString();
                        }
                        else
                        {
                            CommID6 = sdr["CommID"].ToString();
                            ChildName6 = sdr["ChildName"].ToString();
                            ChildSex6 = sdr["ChildSex"].ToString();
                            ChildBirthday6 = BIZ_Common.FormatString(sdr["ChildBirthday"].ToString(), "2");
                            ChildDeathday6 = BIZ_Common.FormatString(sdr["ChildDeathday"].ToString(), "2");
                            ChildDeathAudit6 = sdr["ChildDeathAudit"].ToString();
                            ChildCardID6 = sdr["ChildCardID"].ToString();
                            ChildSource6 = sdr["ChildSource"].ToString();
                            ChildPolicy6 = sdr["ChildPolicy"].ToString();
                            ChildSurvivalStatus6 = sdr["ChildSurvivalStatus"].ToString();
                            ChildNo6 = sdr["ChildNo"].ToString();
                            FatherName6 = sdr["FatherName"].ToString();
                            MotherName6 = sdr["MotherName"].ToString();
                            Memos6 = sdr["Memos"].ToString();
                        }
                        i++;
                    }
                }
                sdr.Close(); sdr.Dispose();
            }
            catch { if (sdr != null) { sdr.Close(); sdr.Dispose(); } }

        }

        /// <summary>
        /// 获取指定人员的子女情侣
        /// </summary>
        /// <param name="personID">可以为空</param>
        /// <param name="bizID"></param>
        /// <returns></returns>
        public void Select(string personID, string bizID)
        {

            SqlDataReader sdr = null;
            try
            {
                string sqlParams = "SELECT TOP 6 CommID,PersonID,ChildName, ChildSex, ChildBirthday, ChildDeathday, ChildDeathAudit, ChildCardID, ChildSource, ChildPolicy, ChildSurvivalStatus,ChildNo, FatherName, MotherName, Memos, CreateDate FROM BIZ_PersonChildren WHERE  BizID=" + bizID;
                if (!string.IsNullOrEmpty(personID)) { sqlParams += " AND PersonID=" + personID; }
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    int i = 0;
                    while (sdr.Read())
                    {
                        if (i == 0)
                        {
                            CommID1 = sdr["CommID"].ToString();
                            ChildName1 = sdr["ChildName"].ToString();
                            ChildSex1 = sdr["ChildSex"].ToString();
                            ChildBirthday1 = BIZ_Common.FormatString(sdr["ChildBirthday"].ToString(), "2");
                            ChildDeathday1 = BIZ_Common.FormatString(sdr["ChildDeathday"].ToString(), "2");
                            ChildDeathAudit1 = sdr["ChildDeathAudit"].ToString();
                            ChildCardID1 = sdr["ChildCardID"].ToString();
                            ChildSource1 = sdr["ChildSource"].ToString();
                            ChildPolicy1 = sdr["ChildPolicy"].ToString();
                            ChildSurvivalStatus1 = sdr["ChildSurvivalStatus"].ToString();
                            ChildNo1 = sdr["ChildNo"].ToString();
                            FatherName1 = sdr["FatherName"].ToString();
                            MotherName1 = sdr["MotherName"].ToString();
                            Memos1 = sdr["Memos"].ToString();
                        }
                        else if (i == 1)
                        {
                            CommID2 = sdr["CommID"].ToString();
                            ChildName2 = sdr["ChildName"].ToString();
                            ChildSex2 = sdr["ChildSex"].ToString();
                            ChildBirthday2 = BIZ_Common.FormatString(sdr["ChildBirthday"].ToString(), "2");
                            ChildDeathday2 = BIZ_Common.FormatString(sdr["ChildDeathday"].ToString(), "2");
                            ChildDeathAudit2 = sdr["ChildDeathAudit"].ToString();
                            ChildCardID2 = sdr["ChildCardID"].ToString();
                            ChildSource2 = sdr["ChildSource"].ToString();
                            ChildPolicy2 = sdr["ChildPolicy"].ToString();
                            ChildSurvivalStatus2 = sdr["ChildSurvivalStatus"].ToString();
                            ChildNo2 = sdr["ChildNo"].ToString();
                            FatherName2 = sdr["FatherName"].ToString();
                            MotherName2 = sdr["MotherName"].ToString();
                            Memos2 = sdr["Memos"].ToString();
                        }
                        else if (i == 2)
                        {
                            CommID3 = sdr["CommID"].ToString();
                            ChildName3 = sdr["ChildName"].ToString();
                            ChildSex3 = sdr["ChildSex"].ToString();
                            ChildBirthday3 = BIZ_Common.FormatString(sdr["ChildBirthday"].ToString(), "2");
                            ChildDeathday3 = BIZ_Common.FormatString(sdr["ChildDeathday"].ToString(), "2");
                            ChildDeathAudit3 = sdr["ChildDeathAudit"].ToString();
                            ChildCardID3 = sdr["ChildCardID"].ToString();
                            ChildSource3 = sdr["ChildSource"].ToString();
                            ChildPolicy3 = sdr["ChildPolicy"].ToString();
                            ChildSurvivalStatus3 = sdr["ChildSurvivalStatus"].ToString();
                            ChildNo3 = sdr["ChildNo"].ToString();
                            FatherName3 = sdr["FatherName"].ToString();
                            MotherName3 = sdr["MotherName"].ToString();
                            Memos3 = sdr["Memos"].ToString();
                        }
                        else if (i == 3)
                        {
                            CommID4 = sdr["CommID"].ToString();
                            ChildName4 = sdr["ChildName"].ToString();
                            ChildSex4 = sdr["ChildSex"].ToString();
                            ChildBirthday4 = BIZ_Common.FormatString(sdr["ChildBirthday"].ToString(), "2");
                            ChildDeathday4 = BIZ_Common.FormatString(sdr["ChildDeathday"].ToString(), "2");
                            ChildDeathAudit4 = sdr["ChildDeathAudit"].ToString();
                            ChildCardID4 = sdr["ChildCardID"].ToString();
                            ChildSource4 = sdr["ChildSource"].ToString();
                            ChildPolicy4 = sdr["ChildPolicy"].ToString();
                            ChildSurvivalStatus4 = sdr["ChildSurvivalStatus"].ToString();
                            ChildNo4 = sdr["ChildNo"].ToString();
                            FatherName4 = sdr["FatherName"].ToString();
                            MotherName4 = sdr["MotherName"].ToString();
                            Memos4 = sdr["Memos"].ToString();
                        }
                        else if (i == 4)
                        {
                            CommID5 = sdr["CommID"].ToString();
                            ChildName5 = sdr["ChildName"].ToString();
                            ChildSex5 = sdr["ChildSex"].ToString();
                            ChildBirthday5 = BIZ_Common.FormatString(sdr["ChildBirthday"].ToString(), "2");
                            ChildDeathday5 = BIZ_Common.FormatString(sdr["ChildDeathday"].ToString(), "2");
                            ChildDeathAudit5 = sdr["ChildDeathAudit"].ToString();
                            ChildCardID5 = sdr["ChildCardID"].ToString();
                            ChildSource5 = sdr["ChildSource"].ToString();
                            ChildPolicy5 = sdr["ChildPolicy"].ToString();
                            ChildSurvivalStatus5 = sdr["ChildSurvivalStatus"].ToString();
                            ChildNo5 = sdr["ChildNo"].ToString();
                            FatherName5 = sdr["FatherName"].ToString();
                            MotherName5 = sdr["MotherName"].ToString();
                            Memos5 = sdr["Memos"].ToString();
                        }
                        else
                        {
                            CommID6 = sdr["CommID"].ToString();
                            ChildName6 = sdr["ChildName"].ToString();
                            ChildSex6 = sdr["ChildSex"].ToString();
                            ChildBirthday6 = BIZ_Common.FormatString(sdr["ChildBirthday"].ToString(), "2");
                            ChildDeathday6 = BIZ_Common.FormatString(sdr["ChildDeathday"].ToString(), "2");
                            ChildDeathAudit6 = sdr["ChildDeathAudit"].ToString();
                            ChildCardID6 = sdr["ChildCardID"].ToString();
                            ChildSource6 = sdr["ChildSource"].ToString();
                            ChildPolicy6 = sdr["ChildPolicy"].ToString();
                            ChildSurvivalStatus6 = sdr["ChildSurvivalStatus"].ToString();
                            ChildNo6 = sdr["ChildNo"].ToString();
                            FatherName6 = sdr["FatherName"].ToString();
                            MotherName6 = sdr["MotherName"].ToString();
                            Memos6 = sdr["Memos"].ToString();
                        }
                        i++;
                    }
                }
                sdr.Close(); sdr.Dispose();
            }
            catch { if (sdr != null) { sdr.Close(); sdr.Dispose(); } }

        }

        /// <summary>
        /// insert子女信息
        /// </summary>
        /// <param name="personID">群众ID 必填</param>
        /// <param name="bizID">业务ID 必填</param>
        /// <param name="birthNum">子女数 必填</param>
        public void Inser(string personID, string bizID, string birthNum)
        {
            string[] Childrens = new string[84];
            #region
            Childrens[0] = this.CommID1;
            Childrens[1] = this.ChildName1;
            Childrens[2] = this.ChildSex1;
            Childrens[3] = this.ChildBirthday1;
            Childrens[4] = this.ChildDeathday1;
            Childrens[5] = this.ChildDeathAudit1;
            Childrens[6] = this.ChildCardID1;
            Childrens[7] = this.ChildSource1;
            Childrens[8] = this.ChildPolicy1;
            Childrens[9] = this.ChildSurvivalStatus1;
            Childrens[10] = this.ChildNo1;
            Childrens[11] = this.FatherName1;
            Childrens[12] = this.MotherName1;
            Childrens[13] = this.Memos1;

            Childrens[14] = this.CommID2;
            Childrens[15] = this.ChildName2;
            Childrens[16] = this.ChildSex2;
            Childrens[17] = this.ChildBirthday2;
            Childrens[18] = this.ChildDeathday2;
            Childrens[19] = this.ChildDeathAudit2;
            Childrens[20] = this.ChildCardID2;
            Childrens[21] = this.ChildSource2;
            Childrens[22] = this.ChildPolicy2;
            Childrens[23] = this.ChildSurvivalStatus2;
            Childrens[24] = this.ChildNo2;
            Childrens[25] = this.FatherName2;
            Childrens[26] = this.MotherName2;
            Childrens[27] = this.Memos2;

            Childrens[28] = this.CommID3;
            Childrens[29] = this.ChildName3;
            Childrens[30] = this.ChildSex3;
            Childrens[31] = this.ChildBirthday3;
            Childrens[32] = this.ChildDeathday3;
            Childrens[33] = this.ChildDeathAudit3;
            Childrens[34] = this.ChildCardID3;
            Childrens[35] = this.ChildSource3;
            Childrens[36] = this.ChildPolicy3;
            Childrens[37] = this.ChildSurvivalStatus3;
            Childrens[38] = this.ChildNo3;
            Childrens[39] = this.FatherName3;
            Childrens[40] = this.MotherName3;
            Childrens[41] = this.Memos3;

            Childrens[42] = this.CommID4;
            Childrens[43] = this.ChildName4;
            Childrens[44] = this.ChildSex4;
            Childrens[45] = this.ChildBirthday4;
            Childrens[46] = this.ChildDeathday4;
            Childrens[47] = this.ChildDeathAudit4;
            Childrens[48] = this.ChildCardID4;
            Childrens[49] = this.ChildSource4;
            Childrens[50] = this.ChildPolicy4;
            Childrens[51] = this.ChildSurvivalStatus4;
            Childrens[52] = this.ChildNo4;
            Childrens[53] = this.FatherName4;
            Childrens[54] = this.MotherName4;
            Childrens[55] = this.Memos4;

            Childrens[56] = this.CommID5;
            Childrens[57] = this.ChildName5;
            Childrens[58] = this.ChildSex5;
            Childrens[59] = this.ChildBirthday5;
            Childrens[60] = this.ChildDeathday5;
            Childrens[61] = this.ChildDeathAudit5;
            Childrens[62] = this.ChildCardID5;
            Childrens[63] = this.ChildSource5;
            Childrens[64] = this.ChildPolicy5;
            Childrens[65] = this.ChildSurvivalStatus5;
            Childrens[66] = this.ChildNo5;
            Childrens[67] = this.FatherName5;
            Childrens[68] = this.MotherName5;
            Childrens[69] = this.Memos5;

            Childrens[70] = this.CommID6;
            Childrens[71] = this.ChildName6;
            Childrens[72] = this.ChildSex6;
            Childrens[73] = this.ChildBirthday6;
            Childrens[74] = this.ChildDeathday6;
            Childrens[75] = this.ChildDeathAudit6;
            Childrens[76] = this.ChildCardID6;
            Childrens[77] = this.ChildSource6;
            Childrens[78] = this.ChildPolicy6;
            Childrens[79] = this.ChildSurvivalStatus6;
            Childrens[80] = this.ChildNo6;
            Childrens[81] = this.FatherName6;
            Childrens[82] = this.MotherName6;
            Childrens[83] = this.Memos6;
            #endregion

            for (int i = 0; i < int.Parse(birthNum); i++)
            {
                SetPersonChildren(personID, bizID, Childrens[(i * 14) + 0], Childrens[(i * 14) + 1], Childrens[(i * 14) + 2], Childrens[(i * 14) + 3], Childrens[(i * 14) + 4], Childrens[(i * 14) + 5], Childrens[(i * 14) + 6], Childrens[(i * 14) + 7], Childrens[(i * 14) + 8], Childrens[(i * 14) + 9], Childrens[(i * 14) + 10], Childrens[(i * 14) + 11], Childrens[(i * 14) + 12], Childrens[(i * 14) + 13]);
            }
        }

        // ChildName, ChildSex, ChildBirthday, ChildDeathday, ChildDeathAudit, ChildCardID, ChildSource, ChildPolicy, ChildSurvivalStatus, FatherName, MotherName, Memos, CreateDate
        // 设置生育史
        public void SetPersonChildren(string personID, string bizID, string CommID, string ChildName, string ChildSex, string ChildBirthday, string ChildDeathday, string ChildDeathAudit, string ChildCardID, string ChildSource, string ChildPolicy, string ChildSurvivalStatus, string ChildNo, string FatherName, string MotherName, string Memos)
        {           
            try
            {
                if (!string.IsNullOrEmpty(personID) && !string.IsNullOrEmpty(bizID) && string.IsNullOrEmpty(CommID) && !string.IsNullOrEmpty(ChildName))
                {
                    string sqlParams = "SELECT CommID FROM BIZ_PersonChildren WHERE PersonID=" + personID + " AND BizID=" + bizID + " AND ChildName='" + ChildName.Trim() + "'";
                    CommID = CommPage.GetSingleVal(sqlParams);
                    if (string.IsNullOrEmpty(CommID))
                    {
                        sqlParams = "INSERT INTO [BIZ_PersonChildren](PersonID,BizID,ChildName, ChildSex, ChildBirthday, ChildDeathday, ChildDeathAudit, ChildCardID, ChildSource, ChildPolicy, ChildSurvivalStatus,ChildNo, FatherName, MotherName, Memos, CreateDate) ";
                        sqlParams += " Values(" + personID + "," + bizID + ",'" + ChildName + "','" + ChildSex + "','" + ChildBirthday + "','" + ChildDeathday + "','" + ChildDeathAudit + "','" + ChildCardID + "','" + ChildSource + "','" + ChildPolicy + "','" + ChildSurvivalStatus + "'," + ChildNo + ",'" + FatherName + "','" + MotherName + "','" + Memos + "',GETDATE()) ";
                        if (DbHelperSQL.ExecuteSql(sqlParams) > 0)
                        { }
                    }
                }
                else
                {
                    string sqlParams = "UPDATE [BIZ_PersonChildren] SET ";
                    sqlParams += " ChildName='" + ChildName + "', ChildSex='" + ChildSex + "', ChildBirthday='" + ChildBirthday + "', ChildDeathday='" + ChildDeathday + "',";
                    sqlParams += " ChildDeathAudit='" + ChildDeathAudit + "', ChildCardID='" + ChildCardID + "', ChildSource='" + ChildSource + "', ChildPolicy='" + ChildPolicy + "',";
                    sqlParams += " ChildSurvivalStatus='" + ChildSurvivalStatus + "',ChildNo=" + ChildNo + ", FatherName='" + FatherName + "', MotherName='" + MotherName + "', Memos='" + Memos + "'  ";
                    sqlParams += " WHERE BizID=" + bizID + " AND CommID=" + CommID;
                    if (DbHelperSQL.ExecuteSql(sqlParams) > 0)
                    { }
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bizID"></param>
        /// <param name="birthNum"></param>
        public void Update(string bizID, string birthNum)
        {
            string[] Childrens = new string[84];
            #region
            Childrens[0] = this.CommID1;
            Childrens[1] = this.ChildName1;
            Childrens[2] = this.ChildSex1;
            Childrens[3] = this.ChildBirthday1;
            Childrens[4] = this.ChildDeathday1;
            Childrens[5] = this.ChildDeathAudit1;
            Childrens[6] = this.ChildCardID1;
            Childrens[7] = this.ChildSource1;
            Childrens[8] = this.ChildPolicy1;
            Childrens[9] = this.ChildSurvivalStatus1;
            Childrens[10] = this.ChildNo1;
            Childrens[11] = this.FatherName1;
            Childrens[12] = this.MotherName1;
            Childrens[13] = this.Memos1;

            Childrens[14] = this.CommID2;
            Childrens[15] = this.ChildName2;
            Childrens[16] = this.ChildSex2;
            Childrens[17] = this.ChildBirthday2;
            Childrens[18] = this.ChildDeathday2;
            Childrens[19] = this.ChildDeathAudit2;
            Childrens[20] = this.ChildCardID2;
            Childrens[21] = this.ChildSource2;
            Childrens[22] = this.ChildPolicy2;
            Childrens[23] = this.ChildSurvivalStatus2;
            Childrens[24] = this.ChildNo2;
            Childrens[25] = this.FatherName2;
            Childrens[26] = this.MotherName2;
            Childrens[27] = this.Memos2;

            Childrens[28] = this.CommID3;
            Childrens[29] = this.ChildName3;
            Childrens[30] = this.ChildSex3;
            Childrens[31] = this.ChildBirthday3;
            Childrens[32] = this.ChildDeathday3;
            Childrens[33] = this.ChildDeathAudit3;
            Childrens[34] = this.ChildCardID3;
            Childrens[35] = this.ChildSource3;
            Childrens[36] = this.ChildPolicy3;
            Childrens[37] = this.ChildSurvivalStatus3;
            Childrens[38] = this.ChildNo3;
            Childrens[39] = this.FatherName3;
            Childrens[40] = this.MotherName3;
            Childrens[41] = this.Memos3;

            Childrens[42] = this.CommID4;
            Childrens[43] = this.ChildName4;
            Childrens[44] = this.ChildSex4;
            Childrens[45] = this.ChildBirthday4;
            Childrens[46] = this.ChildDeathday4;
            Childrens[47] = this.ChildDeathAudit4;
            Childrens[48] = this.ChildCardID4;
            Childrens[49] = this.ChildSource4;
            Childrens[50] = this.ChildPolicy4;
            Childrens[51] = this.ChildSurvivalStatus4;
            Childrens[52] = this.ChildNo4;
            Childrens[53] = this.FatherName4;
            Childrens[54] = this.MotherName4;
            Childrens[55] = this.Memos4;

            Childrens[56] = this.CommID5;
            Childrens[57] = this.ChildName5;
            Childrens[58] = this.ChildSex5;
            Childrens[59] = this.ChildBirthday5;
            Childrens[60] = this.ChildDeathday5;
            Childrens[61] = this.ChildDeathAudit5;
            Childrens[62] = this.ChildCardID5;
            Childrens[63] = this.ChildSource5;
            Childrens[64] = this.ChildPolicy5;
            Childrens[65] = this.ChildSurvivalStatus5;
            Childrens[66] = this.ChildNo5;
            Childrens[67] = this.FatherName5;
            Childrens[68] = this.MotherName5;
            Childrens[69] = this.Memos5;

            Childrens[70] = this.CommID6;
            Childrens[71] = this.ChildName6;
            Childrens[72] = this.ChildSex6;
            Childrens[73] = this.ChildBirthday6;
            Childrens[74] = this.ChildDeathday6;
            Childrens[75] = this.ChildDeathAudit6;
            Childrens[76] = this.ChildCardID6;
            Childrens[77] = this.ChildSource6;
            Childrens[78] = this.ChildPolicy6;
            Childrens[79] = this.ChildSurvivalStatus6;
            Childrens[80] = this.ChildNo6;
            Childrens[81] = this.FatherName6;
            Childrens[82] = this.MotherName6;
            Childrens[83] = this.Memos6;
            #endregion

            for (int i = 0; i < int.Parse(birthNum); i++)
            {
                UpdatePersonChildren(bizID, Childrens[(i * 14) + 0], Childrens[(i * 14) + 1], Childrens[(i * 14) + 2], Childrens[(i * 14) + 3], Childrens[(i * 14) + 4], Childrens[(i * 14) + 5], Childrens[(i * 14) + 6], Childrens[(i * 14) + 7], Childrens[(i * 14) + 8], Childrens[(i * 14) + 9], Childrens[(i * 14) + 10], Childrens[(i * 14) + 11], Childrens[(i * 14) + 12], Childrens[(i * 14) + 13]);
            }
        }

        // ChildName, ChildSex, ChildBirthday, ChildDeathday, ChildDeathAudit, ChildCardID, ChildSource, ChildPolicy, ChildSurvivalStatus, FatherName, MotherName, Memos, CreateDate
        // 设置生育史
        public void UpdatePersonChildren(string bizID, string CommID, string ChildName, string ChildSex, string ChildBirthday, string ChildDeathday, string ChildDeathAudit, string ChildCardID, string ChildSource, string ChildPolicy, string ChildSurvivalStatus, string ChildNo, string FatherName, string MotherName, string Memos)
        {
            string commID = string.Empty;
            bool returnVa = false;
            try
            {
                if (!string.IsNullOrEmpty(bizID) && !string.IsNullOrEmpty(CommID) && !string.IsNullOrEmpty(ChildName))
                {
                    string sqlParams = "UPDATE [BIZ_PersonChildren] SET ";
                    sqlParams += " ChildName='" + ChildName + "', ChildSex='" + ChildSex + "', ChildBirthday='" + ChildBirthday + "', ChildDeathday='" + ChildDeathday + "',";
                    sqlParams += " ChildDeathAudit='" + ChildDeathAudit + "', ChildCardID='" + ChildCardID + "', ChildSource='" + ChildSource + "', ChildPolicy='" + ChildPolicy + "',";
                    sqlParams += " ChildSurvivalStatus='" + ChildSurvivalStatus + "',ChildNo=" + ChildNo + ", FatherName='" + FatherName + "', MotherName='" + MotherName + "', Memos='" + Memos + "'  ";
                    sqlParams += " WHERE BizID=" + bizID + " AND CommID=" + CommID;
                    if (DbHelperSQL.ExecuteSql(sqlParams) > 0)
                    { }
                }
            }
            catch { }
        }

    }
}
