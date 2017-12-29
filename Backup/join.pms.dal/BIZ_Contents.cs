using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using UNV.Comm.DataBase;

namespace join.pms.dal
{
    /*
     业务相关方法 by tianxq 2015/4/21
     */
    public class BIZ_Contents
    {
        #region 字段
        public string BizID;
        public string BizCode;
        public string BizName;
        public string BizStep;
        public string CurrentStep;
        public string CurrentStepNa;
        public string PersonID;
        public string PersonPhotos;
        public string PersonCidA;
        public string PersonCidB;
        public string AddressID;
        public string ContactTelA;
        public string ContactTelB;
        public string RegAreaCodeA;
        public string RegAreaCodeB;
        public string RegAreaNameA;
        public string RegAreaNameB;
        public string CurAreaCodeA;
        public string CurAreaCodeB;
        public string CurAreaNameA;
        public string CurAreaNameB;
        public string SelAreaCode;
        public string SelAreaName;
        public string AdminUserID;
        public string Initiator;
        public string InitDirection;
        public string StartDate;
        public string FinalDate;
        public string Comments;
        public string Attribs;
        public string Fileds01;
        public string Fileds02;
        public string Fileds03;
        public string Fileds04;
        public string Fileds05;
        public string Fileds06;
        public string Fileds07;
        public string Fileds08;
        public string Fileds09;
        public string Fileds10;
        public string Fileds11;
        public string Fileds12;
        public string Fileds13;
        public string Fileds14;
        public string Fileds15;
        public string Fileds16;
        public string Fileds17;
        public string Fileds18;
        public string Fileds19;
        public string Fileds20;
        public string Fileds21;
        public string Fileds22;
        public string Fileds23;
        public string Fileds24;
        public string Fileds25;
        public string Fileds26;
        public string Fileds27;
        public string Fileds28;
        public string Fileds29;
        public string Fileds30;
        public string Fileds31;
        public string Fileds32;
        public string Fileds33;
        public string Fileds34;
        public string Fileds35;
        public string Fileds36;
        public string Fileds37;
        public string Fileds38;
        public string Fileds39;
        public string Fileds40;
        public string Fileds41;
        public string Fileds42;
        public string Fileds43;
        public string Fileds44;
        public string Fileds45;
        public string Fileds46;
        public string Fileds47;
        public string Fileds48;
        public string Fileds49;
        public string Fileds50;
        public string QcfwzBm;

        public string GetAreaCode;
        public string GetAreaName;
        #endregion

        public string UserID;
        public string PersonCardID;
        public string SearchWhere;
        public string[] aryAnalys;

        #region 通用SQL
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
            string[] values ={ GetAreaCode, GetAreaName, BizCode, BizName, BizStep, CurrentStep, CurrentStepNa, PersonID, PersonPhotos, PersonCidA, PersonCidB, AddressID, ContactTelA, ContactTelB, RegAreaCodeA, RegAreaCodeB, RegAreaNameA, RegAreaNameB, CurAreaCodeA, CurAreaCodeB, CurAreaNameA, CurAreaNameB, SelAreaCode, SelAreaName, AdminUserID, Initiator, InitDirection, StartDate, FinalDate, Comments, Attribs, Fileds01, Fileds02, Fileds03, Fileds04, Fileds05, Fileds06, Fileds07, Fileds08, Fileds09, Fileds10, Fileds11, Fileds12, Fileds13, Fileds14, Fileds15, Fileds16, Fileds17, Fileds18, Fileds19, Fileds20, Fileds21, Fileds22, Fileds23, Fileds24, Fileds25, Fileds26, Fileds27, Fileds28, Fileds29, Fileds30, Fileds31, Fileds32, Fileds33, Fileds34, Fileds35, Fileds36, Fileds37, Fileds38, Fileds39, Fileds40, Fileds41, Fileds42, Fileds43, Fileds44, Fileds45, Fileds46, Fileds47, Fileds48, Fileds49, Fileds50, QcfwzBm };

            for (int i = 0; i < aAnaly.Length; i++)
            {
                if (!string.IsNullOrEmpty(values[i]) && values[i] != null)
                {
                    returnA += aAnaly[i] + ",";
                    returnB += "@" + aAnaly[i].Trim() + ",";
                }
            }
            if (returnA != "") returnA = returnA.Substring(0, returnA.Length - 1);
            if (returnB != "") returnB = returnB.Substring(0, returnB.Length - 1);

            returnVal = "INSERT INTO [BIZ_Contents](" + returnA + ") Values(" + returnB + ") SELECT SCOPE_IDENTITY();";
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
            string[] values ={ GetAreaCode, GetAreaName, PersonPhotos, PersonCidA, PersonCidB, ContactTelA, ContactTelB, RegAreaCodeA, RegAreaCodeB, RegAreaNameA, RegAreaNameB, CurAreaCodeA, CurAreaCodeB, CurAreaNameA, CurAreaNameB, SelAreaCode, SelAreaName, Fileds01, Fileds02, Fileds03, Fileds04, Fileds05, Fileds06, Fileds07, Fileds08, Fileds09, Fileds10, Fileds11, Fileds12, Fileds13, Fileds14, Fileds15, Fileds16, Fileds17, Fileds18, Fileds19, Fileds20, Fileds21, Fileds22, Fileds23, Fileds24, Fileds25, Fileds26, Fileds27, Fileds28, Fileds29, Fileds30, Fileds31, Fileds32, Fileds33, Fileds34, Fileds35, Fileds36, Fileds37, Fileds38, Fileds39, Fileds40, Fileds41, Fileds42, Fileds43, Fileds44, Fileds45, Fileds46, Fileds47, Fileds48, Fileds49, Fileds50, QcfwzBm };

            for (int i = 0; i < aAnaly.Length; i++)
            {
                if (!string.IsNullOrEmpty(values[i]) && values[i] != null)
                {
                    returnA += aAnaly[i] + "=@" + aAnaly[i].Trim() + ",";
                }
            }
            if (returnA != "") returnA = returnA.Substring(0, returnA.Length - 1);
            returnVal = "Update BIZ_Contents Set " + returnA + " Where 1=1 and BizID = @BizID";
            return returnVal;
        }

        /// <summary>
        /// 向表插入数据
        /// </summary>
        /// <param name="bizID">返回ID</param>
        /// <returns></returns>
        public string Insert()
        {
            #region SQL参数
            SqlParameter[] paras = new SqlParameter[82];
            //BizID,BizCode, BizName, BizStep, CurrentStep, 
            paras[0] = new SqlParameter("@BizID", SqlDbType.Int, 4);
            paras[0].Value = BizID;
            paras[1] = new SqlParameter("@BizCode", SqlDbType.VarChar, 20);
            paras[1].Value = BizCode;
            paras[2] = new SqlParameter("@BizName", SqlDbType.VarChar, 50);
            paras[2].Value = BizName;
            paras[3] = new SqlParameter("@BizStep", SqlDbType.TinyInt, 1);
            paras[3].Value = BizStep;
            paras[4] = new SqlParameter("@CurrentStep", SqlDbType.TinyInt, 1);
            paras[4].Value = CurrentStep;

            //CurrentStepNa, PersonID, PersonPhotos, PersonCidA, PersonCidB, 
            paras[5] = new SqlParameter("@CurrentStepNa", SqlDbType.VarChar, 50);
            paras[5].Value = CurrentStepNa;
            paras[6] = new SqlParameter("@PersonID", SqlDbType.Int, 4);
            paras[6].Value = PersonID;
            paras[7] = new SqlParameter("@PersonPhotos", SqlDbType.VarChar, 100);
            paras[7].Value = PersonPhotos;
            paras[8] = new SqlParameter("@PersonCidA", SqlDbType.VarChar, 20);
            paras[8].Value = PersonCidA;
            paras[9] = new SqlParameter("@PersonCidB", SqlDbType.VarChar, 20);
            paras[9].Value = PersonCidB;

            //AddressID,ContactTelA, ContactTelB, RegAreaCodeA, RegAreaCodeB, 
            paras[10] = new SqlParameter("@AddressID", SqlDbType.Int, 4);
            paras[10].Value = AddressID;
            paras[11] = new SqlParameter("@ContactTelA", SqlDbType.VarChar, 50);
            paras[11].Value = ContactTelA;
            paras[12] = new SqlParameter("@ContactTelB", SqlDbType.VarChar, 50);
            paras[12].Value = ContactTelB;
            paras[13] = new SqlParameter("@RegAreaCodeA", SqlDbType.VarChar, 20);
            paras[13].Value = RegAreaCodeA;
            paras[14] = new SqlParameter("@RegAreaCodeB", SqlDbType.VarChar, 20);
            paras[14].Value = RegAreaCodeB;

            // RegAreaNameA,RegAreaNameB, CurAreaCodeA, CurAreaCodeB, CurAreaNameA, 
            paras[15] = new SqlParameter("@RegAreaNameA", SqlDbType.VarChar, 50);
            paras[15].Value = RegAreaNameA;
            paras[16] = new SqlParameter("@RegAreaNameB", SqlDbType.VarChar, 50);
            paras[16].Value = RegAreaNameB;
            paras[17] = new SqlParameter("@CurAreaCodeA", SqlDbType.VarChar, 20);
            paras[17].Value = CurAreaCodeA;
            paras[18] = new SqlParameter("@CurAreaCodeB", SqlDbType.VarChar, 20);
            paras[18].Value = CurAreaCodeB;
            paras[19] = new SqlParameter("@CurAreaNameA", SqlDbType.VarChar, 50);
            paras[19].Value = CurAreaNameA;

            //CurAreaNameB, SelAreaCode, SelAreaName, AdminUserID, Initiator,
            paras[20] = new SqlParameter("@CurAreaNameB", SqlDbType.VarChar, 50);
            paras[20].Value = CurAreaNameB;
            paras[21] = new SqlParameter("@SelAreaCode", SqlDbType.VarChar, 20);
            paras[21].Value = SelAreaCode;
            paras[22] = new SqlParameter("@SelAreaName", SqlDbType.VarChar, 50);
            paras[22].Value = SelAreaName;
            paras[23] = new SqlParameter("@AdminUserID", SqlDbType.Int, 4);
            paras[23].Value = AdminUserID;
            paras[24] = new SqlParameter("@Initiator", SqlDbType.VarChar, 50);
            paras[24].Value = Initiator;

            // InitDirection, StartDate, FinalDate, Comments, Attribs, 
            paras[25] = new SqlParameter("@InitDirection", SqlDbType.TinyInt, 1);
            paras[25].Value = InitDirection;
            paras[26] = new SqlParameter("@StartDate", SqlDbType.SmallDateTime, 4);
            paras[26].Value = StartDate;
            paras[27] = new SqlParameter("@FinalDate", SqlDbType.SmallDateTime, 4);
            paras[27].Value = FinalDate;
            paras[28] = new SqlParameter("@Comments", SqlDbType.VarChar, 500);
            paras[28].Value = Comments;
            paras[29] = new SqlParameter("@Attribs", SqlDbType.TinyInt, 1);
            paras[29].Value = Attribs;

            //Fileds01, Fileds02, Fileds03, Fileds04, Fileds05, 
            paras[30] = new SqlParameter("@Fileds01", SqlDbType.VarChar, 50);
            paras[30].Value = Fileds01;
            paras[31] = new SqlParameter("@Fileds02", SqlDbType.VarChar, 50);
            paras[31].Value = Fileds02;
            paras[32] = new SqlParameter("@Fileds03", SqlDbType.VarChar, 50);
            paras[32].Value = Fileds03;
            paras[33] = new SqlParameter("@Fileds04", SqlDbType.VarChar, 50);
            paras[33].Value = Fileds04;
            paras[34] = new SqlParameter("@Fileds05", SqlDbType.VarChar, 50);
            paras[34].Value = Fileds05;

            //Fileds06, Fileds07, Fileds08, Fileds09, Fileds10, 
            paras[35] = new SqlParameter("@Fileds06", SqlDbType.VarChar, 50);
            paras[35].Value = Fileds06;
            paras[36] = new SqlParameter("@Fileds07", SqlDbType.VarChar, 50);
            paras[36].Value = Fileds07;
            paras[37] = new SqlParameter("@Fileds08", SqlDbType.VarChar, 50);
            paras[37].Value = Fileds08;
            paras[38] = new SqlParameter("@Fileds09", SqlDbType.VarChar, 50);
            paras[38].Value = Fileds09;
            paras[39] = new SqlParameter("@Fileds10", SqlDbType.VarChar, 50);
            paras[39].Value = Fileds10;

            //Fileds11, Fileds12, Fileds13, Fileds14, Fileds15,
            paras[40] = new SqlParameter("@Fileds11", SqlDbType.VarChar, 50);
            paras[40].Value = Fileds11;
            paras[41] = new SqlParameter("@Fileds12", SqlDbType.VarChar, 50);
            paras[41].Value = Fileds12;
            paras[42] = new SqlParameter("@Fileds13", SqlDbType.VarChar, 50);
            paras[42].Value = Fileds13;
            paras[43] = new SqlParameter("@Fileds14", SqlDbType.VarChar, 50);
            paras[43].Value = Fileds14;
            paras[44] = new SqlParameter("@Fileds15", SqlDbType.VarChar, 50);
            paras[44].Value = Fileds15;

            // Fileds16,Fileds17, Fileds18, Fileds19, Fileds20,
            paras[45] = new SqlParameter("@Fileds16", SqlDbType.VarChar, 50);
            paras[45].Value = Fileds16;
            paras[46] = new SqlParameter("@Fileds17", SqlDbType.VarChar, 50);
            paras[46].Value = Fileds17;
            paras[47] = new SqlParameter("@Fileds18", SqlDbType.VarChar, 50);
            paras[47].Value = Fileds18;
            paras[48] = new SqlParameter("@Fileds19", SqlDbType.VarChar, 50);
            paras[48].Value = Fileds19;
            paras[49] = new SqlParameter("@Fileds20", SqlDbType.VarChar, 50);
            paras[49].Value = Fileds20;

            // Fileds21,Fileds22, Fileds23, Fileds24, Fileds25, 
            paras[50] = new SqlParameter("@Fileds21", SqlDbType.VarChar, 50);
            paras[50].Value = Fileds21;
            paras[51] = new SqlParameter("@Fileds22", SqlDbType.VarChar, 50);
            paras[51].Value = Fileds22;
            paras[52] = new SqlParameter("@Fileds23", SqlDbType.VarChar, 50);
            paras[52].Value = Fileds23;
            paras[53] = new SqlParameter("@Fileds24", SqlDbType.VarChar, 50);
            paras[53].Value = Fileds24;
            paras[54] = new SqlParameter("@Fileds25", SqlDbType.VarChar, 50);
            paras[54].Value = Fileds25;

            //Fileds26, Fileds27, Fileds28, Fileds29, Fileds30, 
            paras[55] = new SqlParameter("@Fileds26", SqlDbType.VarChar, 50);
            paras[55].Value = Fileds26;
            paras[56] = new SqlParameter("@Fileds27", SqlDbType.VarChar, 50);
            paras[56].Value = Fileds27;
            paras[57] = new SqlParameter("@Fileds28", SqlDbType.VarChar, 50);
            paras[57].Value = Fileds28;
            paras[58] = new SqlParameter("@Fileds29", SqlDbType.VarChar, 50);
            paras[58].Value = Fileds29;
            paras[59] = new SqlParameter("@Fileds30", SqlDbType.VarChar, 50);
            paras[59].Value = Fileds30;

            //Fileds31,Fileds32, Fileds33, Fileds34, Fileds35, 
            paras[60] = new SqlParameter("@Fileds31", SqlDbType.VarChar, 50);
            paras[60].Value = Fileds31;
            paras[61] = new SqlParameter("@Fileds32", SqlDbType.VarChar, 50);
            paras[61].Value = Fileds32;
            paras[62] = new SqlParameter("@Fileds33", SqlDbType.VarChar, 50);
            paras[62].Value = Fileds33;
            paras[63] = new SqlParameter("@Fileds34", SqlDbType.VarChar, 50);
            paras[63].Value = Fileds34;
            paras[64] = new SqlParameter("@Fileds35", SqlDbType.VarChar, 50);
            paras[64].Value = Fileds35;

            //Fileds36, Fileds37, Fileds38, Fileds39, Fileds40, 
            paras[65] = new SqlParameter("@Fileds36", SqlDbType.VarChar, 50);
            paras[65].Value = Fileds36;
            paras[66] = new SqlParameter("@Fileds37", SqlDbType.VarChar, 50);
            paras[66].Value = Fileds37;
            paras[67] = new SqlParameter("@Fileds38", SqlDbType.VarChar, 50);
            paras[67].Value = Fileds38;
            paras[68] = new SqlParameter("@Fileds39", SqlDbType.VarChar, 50);
            paras[68].Value = Fileds39;
            paras[69] = new SqlParameter("@Fileds40", SqlDbType.VarChar, 50);
            paras[69].Value = Fileds40;

            //Fileds41,Fileds42, Fileds43, Fileds44, Fileds45, 
            paras[70] = new SqlParameter("@Fileds41", SqlDbType.VarChar, 50);
            paras[70].Value = Fileds41;
            paras[71] = new SqlParameter("@Fileds42", SqlDbType.VarChar, 50);
            paras[71].Value = Fileds42;
            paras[72] = new SqlParameter("@Fileds43", SqlDbType.VarChar, 50);
            paras[72].Value = Fileds43;
            paras[73] = new SqlParameter("@Fileds44", SqlDbType.VarChar, 50);
            paras[73].Value = Fileds44;
            paras[74] = new SqlParameter("@Fileds45", SqlDbType.VarChar, 50);
            paras[74].Value = Fileds45;

            //Fileds46, Fileds47, Fileds48, Fileds49, Fileds50
            paras[75] = new SqlParameter("@Fileds46", SqlDbType.VarChar, 50);
            paras[75].Value = Fileds46;
            paras[76] = new SqlParameter("@Fileds47", SqlDbType.VarChar, 50);
            paras[76].Value = Fileds47;
            paras[77] = new SqlParameter("@Fileds48", SqlDbType.VarChar, 50);
            paras[77].Value = Fileds48;
            paras[78] = new SqlParameter("@Fileds49", SqlDbType.VarChar, 50);
            paras[78].Value = Fileds49;
            paras[79] = new SqlParameter("@Fileds50", SqlDbType.VarChar, 50);
            paras[79].Value = Fileds50;

            paras[80] = new SqlParameter("@GetAreaCode", SqlDbType.VarChar, 20);
            paras[80].Value = GetAreaCode;
            paras[81] = new SqlParameter("@GetAreaName", SqlDbType.VarChar, 50);
            paras[81].Value = GetAreaName;
            paras[82] = new SqlParameter("@QcfwzBm", SqlDbType.VarChar, 50);
            paras[82].Value = QcfwzBm;
            #endregion
            #region SQL
            // 根据插入字段的不同可以注释 Fields ,从外部传入aryAnalys
            string fields = " GetAreaCode, GetAreaName,BizCode, BizName, BizStep, CurrentStep, CurrentStepNa, PersonID, PersonPhotos, PersonCidA, PersonCidB, AddressID, ContactTelA, ContactTelB, RegAreaCodeA, RegAreaCodeB, RegAreaNameA, RegAreaNameB, CurAreaCodeA, CurAreaCodeB, CurAreaNameA, CurAreaNameB, SelAreaCode, SelAreaName, AdminUserID, Initiator, InitDirection, StartDate, FinalDate, Comments, Attribs, Fileds01, Fileds02, Fileds03, Fileds04, Fileds05, Fileds06, Fileds07, Fileds08, Fileds09, Fileds10, Fileds11, Fileds12, Fileds13, Fileds14, Fileds15, Fileds16, Fileds17, Fileds18, Fileds19, Fileds20, Fileds21, Fileds22, Fileds23, Fileds24, Fileds25, Fileds26, Fileds27, Fileds28, Fileds29, Fileds30, Fileds31, Fileds32, Fileds33, Fileds34, Fileds35, Fileds36, Fileds37, Fileds38, Fileds39, Fileds40, Fileds41, Fileds42, Fileds43, Fileds44, Fileds45, Fileds46, Fileds47, Fileds48, Fileds49, Fileds50, QcfwzBm ";
            aryAnalys = fields.Split(',');
            string sql = GetInsertSQL(aryAnalys);

            #endregion
            try
            {
                return DbHelperSQL.GetSingle(sql, paras).ToString();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 更新表中的指定数据
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            #region SQL参数
            SqlParameter[] paras = new SqlParameter[68];
            //BizID,
            paras[0] = new SqlParameter("@BizID", SqlDbType.Int, 4);
            paras[0].Value = BizID;

            //PersonPhotos, PersonCidA, PersonCidB, 

            paras[1] = new SqlParameter("@PersonPhotos", SqlDbType.VarChar, 100);
            if (String.IsNullOrEmpty(PersonPhotos)) { PersonPhotos = ""; }
            paras[1].Value = PersonPhotos;
            paras[2] = new SqlParameter("@PersonCidA", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(PersonCidA)) { PersonCidA = ""; }
            paras[2].Value = PersonCidA;
            paras[3] = new SqlParameter("@PersonCidB", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(PersonCidB)) { PersonCidB = ""; }
            paras[3].Value = PersonCidB;

            //ContactTelA, ContactTelB, RegAreaCodeA, RegAreaCodeB,             
            paras[4] = new SqlParameter("@ContactTelA", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(ContactTelA)) { ContactTelA = ""; }
            paras[4].Value = ContactTelA;
            paras[5] = new SqlParameter("@ContactTelB", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(ContactTelB)) { ContactTelB = ""; }
            paras[5].Value = ContactTelB;
            paras[6] = new SqlParameter("@RegAreaCodeA", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(RegAreaCodeA)) { RegAreaCodeA = ""; }
            paras[6].Value = RegAreaCodeA;
            paras[7] = new SqlParameter("@RegAreaCodeB", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(RegAreaCodeB)) { RegAreaCodeB = ""; }
            paras[7].Value = RegAreaCodeB;

            // RegAreaNameA,RegAreaNameB, CurAreaCodeA, CurAreaCodeB, CurAreaNameA, 
            paras[8] = new SqlParameter("@RegAreaNameA", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(RegAreaNameA)) { RegAreaNameA = ""; }
            paras[8].Value = RegAreaNameA;
            paras[9] = new SqlParameter("@RegAreaNameB", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(RegAreaNameB)) { RegAreaNameB = ""; }
            paras[9].Value = RegAreaNameB;
            paras[10] = new SqlParameter("@CurAreaCodeA", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(CurAreaCodeA)) { CurAreaCodeA = ""; }
            paras[10].Value = CurAreaCodeA;
            paras[11] = new SqlParameter("@CurAreaCodeB", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(CurAreaCodeB)) { CurAreaCodeB = ""; }
            paras[11].Value = CurAreaCodeB;
            paras[12] = new SqlParameter("@CurAreaNameA", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(CurAreaNameA)) { CurAreaNameA = ""; }
            paras[12].Value = CurAreaNameA;

            //CurAreaNameB, SelAreaCode, SelAreaName,
            paras[13] = new SqlParameter("@CurAreaNameB", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(CurAreaNameB)) { CurAreaNameB = ""; }
            paras[13].Value = CurAreaNameB;
            paras[14] = new SqlParameter("@SelAreaCode", SqlDbType.VarChar, 20);
            if (String.IsNullOrEmpty(SelAreaCode)) { SelAreaCode = ""; }
            paras[14].Value = SelAreaCode;
            paras[15] = new SqlParameter("@SelAreaName", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(SelAreaName)) { SelAreaName = ""; }
            paras[15].Value = SelAreaName;

            //Fileds01, Fileds02, Fileds03, Fileds04, Fileds05, 
            paras[16] = new SqlParameter("@Fileds01", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds01)) { Fileds01 = ""; }
            paras[16].Value = Fileds01;
            paras[17] = new SqlParameter("@Fileds02", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds02)) { Fileds02 = ""; }
            paras[17].Value = Fileds02;
            paras[18] = new SqlParameter("@Fileds03", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds03)) { Fileds03 = ""; }
            paras[18].Value = Fileds03;
            paras[19] = new SqlParameter("@Fileds04", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds04)) { Fileds04 = ""; }
            paras[19].Value = Fileds04;
            paras[20] = new SqlParameter("@Fileds05", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds05)) { Fileds05 = ""; }
            paras[20].Value = Fileds05;

            //Fileds06, Fileds07, Fileds08, Fileds09, Fileds10, 
            paras[21] = new SqlParameter("@Fileds06", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds06)) { Fileds06 = ""; }
            paras[21].Value = Fileds06;
            paras[22] = new SqlParameter("@Fileds07", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds07)) { Fileds07 = ""; }
            paras[22].Value = Fileds07;
            paras[23] = new SqlParameter("@Fileds08", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds08)) { Fileds08 = ""; }
            paras[23].Value = Fileds08;
            paras[24] = new SqlParameter("@Fileds09", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds09)) { Fileds09 = ""; }
            paras[24].Value = Fileds09;
            paras[25] = new SqlParameter("@Fileds10", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds10)) { Fileds10 = ""; }
            paras[25].Value = Fileds10;

            //Fileds11, Fileds12, Fileds13, Fileds14, Fileds15,
            paras[26] = new SqlParameter("@Fileds11", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds11)) { Fileds11 = ""; }
            paras[26].Value = Fileds11;
            paras[27] = new SqlParameter("@Fileds12", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds12)) { Fileds12 = ""; }
            paras[27].Value = Fileds12;
            paras[28] = new SqlParameter("@Fileds13", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds13)) { Fileds13 = ""; }
            paras[28].Value = Fileds13;
            paras[29] = new SqlParameter("@Fileds14", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds14)) { Fileds14 = ""; }
            paras[29].Value = Fileds14;
            paras[30] = new SqlParameter("@Fileds15", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds15)) { Fileds15 = ""; }
            paras[30].Value = Fileds15;

            // Fileds16,Fileds17, Fileds18, Fileds19, Fileds20,
            paras[31] = new SqlParameter("@Fileds16", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds16)) { Fileds16 = ""; }
            paras[31].Value = Fileds16;
            paras[32] = new SqlParameter("@Fileds17", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds17)) { Fileds17 = ""; }
            paras[32].Value = Fileds17;
            paras[33] = new SqlParameter("@Fileds18", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds18)) { Fileds18 = ""; }
            paras[33].Value = Fileds18;
            paras[34] = new SqlParameter("@Fileds19", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds19)) { Fileds19 = ""; }
            paras[34].Value = Fileds19;
            paras[35] = new SqlParameter("@Fileds20", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds20)) { Fileds20 = ""; }
            paras[35].Value = Fileds20;

            // Fileds21,Fileds22, Fileds23, Fileds24, Fileds25, 
            paras[36] = new SqlParameter("@Fileds21", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds21)) { Fileds21 = ""; }
            paras[36].Value = Fileds21;
            paras[37] = new SqlParameter("@Fileds22", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds22)) { Fileds22 = ""; }
            paras[37].Value = Fileds22;
            paras[38] = new SqlParameter("@Fileds23", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds23)) { Fileds23 = ""; }
            paras[38].Value = Fileds23;
            paras[39] = new SqlParameter("@Fileds24", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds24)) { Fileds24 = ""; }
            paras[39].Value = Fileds24;
            paras[40] = new SqlParameter("@Fileds25", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds25)) { Fileds25 = ""; }
            paras[40].Value = Fileds25;

            //Fileds26, Fileds27, Fileds28, Fileds29, Fileds30, 
            paras[41] = new SqlParameter("@Fileds26", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds26)) { Fileds26 = ""; }
            paras[41].Value = Fileds26;
            paras[42] = new SqlParameter("@Fileds27", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds27)) { Fileds27 = ""; }
            paras[42].Value = Fileds27;
            paras[43] = new SqlParameter("@Fileds28", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds28)) { Fileds28 = ""; }
            paras[43].Value = Fileds28;
            paras[44] = new SqlParameter("@Fileds29", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds29)) { Fileds29 = ""; }
            paras[44].Value = Fileds29;
            paras[45] = new SqlParameter("@Fileds30", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds30)) { Fileds30 = ""; }
            paras[45].Value = Fileds30;

            //Fileds31,Fileds32, Fileds33, Fileds34, Fileds35, 
            paras[46] = new SqlParameter("@Fileds31", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds31)) { Fileds31 = ""; }
            paras[46].Value = Fileds31;
            paras[47] = new SqlParameter("@Fileds32", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds32)) { Fileds32 = ""; }
            paras[47].Value = Fileds32;
            paras[48] = new SqlParameter("@Fileds33", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds33)) { Fileds33 = ""; }
            paras[48].Value = Fileds33;
            paras[49] = new SqlParameter("@Fileds34", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds34)) { Fileds34 = ""; }
            paras[49].Value = Fileds34;
            paras[50] = new SqlParameter("@Fileds35", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds35)) { Fileds35 = ""; }
            paras[50].Value = Fileds35;

            //Fileds36, Fileds37, Fileds38, Fileds39, Fileds40, 
            paras[51] = new SqlParameter("@Fileds36", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds36)) { Fileds36 = ""; }
            paras[51].Value = Fileds36;
            paras[52] = new SqlParameter("@Fileds37", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds37)) { Fileds37 = ""; }
            paras[52].Value = Fileds37;
            paras[53] = new SqlParameter("@Fileds38", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds38)) { Fileds38 = ""; }
            paras[53].Value = Fileds38;
            paras[54] = new SqlParameter("@Fileds39", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds39)) { Fileds39 = ""; }
            paras[54].Value = Fileds39;
            paras[55] = new SqlParameter("@Fileds40", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds40)) { Fileds40 = ""; }
            paras[55].Value = Fileds40;

            //Fileds41,Fileds42, Fileds43, Fileds44, Fileds45, 
            paras[56] = new SqlParameter("@Fileds41", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds41)) { Fileds41 = ""; }
            paras[56].Value = Fileds41;
            paras[57] = new SqlParameter("@Fileds42", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds42)) { Fileds42 = ""; }
            paras[57].Value = Fileds42;
            paras[58] = new SqlParameter("@Fileds43", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds43)) { Fileds43 = ""; }
            paras[58].Value = Fileds43;
            paras[59] = new SqlParameter("@Fileds44", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds44)) { Fileds44 = ""; }
            paras[59].Value = Fileds44;
            paras[60] = new SqlParameter("@Fileds45", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds45)) { Fileds45 = ""; }
            paras[60].Value = Fileds45;

            //Fileds46, Fileds47, Fileds48, Fileds49, Fileds50
            paras[61] = new SqlParameter("@Fileds46", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds46)) { Fileds46 = ""; }
            paras[61].Value = Fileds46;
            paras[62] = new SqlParameter("@Fileds47", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds47)) { Fileds47 = ""; }
            paras[62].Value = Fileds47;
            paras[63] = new SqlParameter("@Fileds48", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds48)) { Fileds48 = ""; }
            paras[63].Value = Fileds48;
            paras[64] = new SqlParameter("@Fileds49", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds49)) { Fileds49 = ""; }
            paras[64].Value = Fileds49;
            paras[65] = new SqlParameter("@Fileds50", SqlDbType.VarChar, 50);
            if (String.IsNullOrEmpty(Fileds50)) { Fileds50 = ""; }
            paras[65].Value = Fileds50;

            paras[66] = new SqlParameter("@GetAreaCode", SqlDbType.VarChar, 20);
            paras[66].Value = GetAreaCode;
            paras[67] = new SqlParameter("@GetAreaName", SqlDbType.VarChar, 50);
            paras[67].Value = GetAreaName;
            #endregion
            #region SQL
            string fields = "GetAreaCode, GetAreaName,PersonPhotos, PersonCidA, PersonCidB, ContactTelA, ContactTelB, RegAreaCodeA, RegAreaCodeB, RegAreaNameA, RegAreaNameB, CurAreaCodeA, CurAreaCodeB, CurAreaNameA, CurAreaNameB, SelAreaCode, SelAreaName, Fileds01, Fileds02, Fileds03, Fileds04, Fileds05, Fileds06, Fileds07, Fileds08, Fileds09, Fileds10, Fileds11, Fileds12, Fileds13, Fileds14, Fileds15, Fileds16, Fileds17, Fileds18, Fileds19, Fileds20, Fileds21, Fileds22, Fileds23, Fileds24, Fileds25, Fileds26, Fileds27, Fileds28, Fileds29, Fileds30, Fileds31, Fileds32, Fileds33, Fileds34, Fileds35, Fileds36, Fileds37, Fileds38, Fileds39, Fileds40, Fileds41, Fileds42, Fileds43, Fileds44, Fileds45, Fileds46, Fileds47, Fileds48, Fileds49, Fileds50";
            aryAnalys = fields.Split(',');
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
        /// <summary>
        /// 获取查询的字段All
        /// </summary>
        /// <param name="IsDe">身份证号和手机号是否加密</param>
        public void SelectAll(bool IsDe)
        {
            string sql = string.Empty;
            if (String.IsNullOrEmpty(SearchWhere))
            {
                sql = "SELECT * FROM BIZ_Contents WHERE ((PersonID=" + this.UserID + " AND InitDirection=0) OR PersonCidA='" + this.PersonCardID + "' OR PersonCidB='" + this.PersonCardID + "') AND BizID=" + this.BizID;
            }
            else
            {
                sql = "SELECT * FROM BIZ_Contents WHERE " + SearchWhere;
            }
            SqlDataReader sdr = null;
            try
            {
                sdr = DbHelperSQL.ExecuteReader(sql);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        BizID = sdr["BizID"].ToString();
                        BizCode = CommBiz.GetTrim(sdr["BizCode"].ToString());
                        BizName = CommBiz.GetTrim(sdr["BizName"].ToString());
                        BizStep = CommBiz.GetTrim(sdr["BizStep"].ToString());
                        CurrentStep = CommBiz.GetTrim(sdr["CurrentStep"].ToString());
                        CurrentStepNa = sdr["CurrentStepNa"].ToString();
                        PersonID = CommBiz.GetTrim(sdr["PersonID"].ToString());
                        PersonPhotos = BIZ_Common.FormatString(sdr["PersonPhotos"].ToString(), "3"); // 已添加web服务地址
                        AddressID = sdr["AddressID"].ToString();

                        GetAreaCode = sdr["GetAreaCode"].ToString();
                        GetAreaName = sdr["GetAreaName"].ToString();

                        if (IsDe)
                        {
                            PersonCidA = BIZ_Common.FormatString(sdr["PersonCidA"].ToString(), "0");
                            PersonCidB = BIZ_Common.FormatString(sdr["PersonCidB"].ToString(), "0");
                            ContactTelA = BIZ_Common.FormatString(sdr["ContactTelA"].ToString(), "1");
                            ContactTelB = BIZ_Common.FormatString(sdr["ContactTelB"].ToString(), "1");
                        }
                        else
                        {
                            PersonCidA = CommBiz.GetTrim(sdr["PersonCidA"].ToString());
                            PersonCidB = CommBiz.GetTrim(sdr["PersonCidB"].ToString());
                            ContactTelA = CommBiz.GetTrim(sdr["ContactTelA"].ToString());
                            ContactTelB = CommBiz.GetTrim(sdr["ContactTelB"].ToString());
                        }
                        RegAreaCodeA = CommBiz.GetTrim(sdr["RegAreaCodeA"].ToString());
                        RegAreaCodeB = CommBiz.GetTrim(sdr["RegAreaCodeB"].ToString());
                        RegAreaNameA = CommBiz.GetTrim(sdr["RegAreaNameA"].ToString());
                        RegAreaNameB = CommBiz.GetTrim(sdr["RegAreaNameB"].ToString());
                        CurAreaCodeA = CommBiz.GetTrim(sdr["CurAreaCodeA"].ToString());
                        CurAreaCodeB = CommBiz.GetTrim(sdr["CurAreaCodeB"].ToString());
                        CurAreaNameA = CommBiz.GetTrim(sdr["CurAreaNameA"].ToString());
                        CurAreaNameB = CommBiz.GetTrim(sdr["CurAreaNameB"].ToString());
                        SelAreaCode = CommBiz.GetTrim(sdr["SelAreaCode"].ToString());
                        SelAreaName = CommBiz.GetTrim(sdr["SelAreaName"].ToString());
                        AdminUserID = CommBiz.GetTrim(sdr["AdminUserID"].ToString());
                        Initiator = CommBiz.GetTrim(sdr["Initiator"].ToString());
                        InitDirection = CommBiz.GetTrim(sdr["InitDirection"].ToString());
                        StartDate = CommBiz.GetTrim(sdr["StartDate"].ToString());
                        FinalDate = CommBiz.GetTrim(sdr["FinalDate"].ToString());
                        Comments = CommBiz.GetTrim(sdr["Comments"].ToString());
                        Attribs = sdr["Attribs"].ToString();
                        Fileds01 = sdr["Fileds01"].ToString();
                        Fileds02 = sdr["Fileds02"].ToString();
                        Fileds03 = sdr["Fileds03"].ToString();
                        Fileds04 = sdr["Fileds04"].ToString();
                        Fileds05 = sdr["Fileds05"].ToString();
                        Fileds06 = sdr["Fileds06"].ToString();
                        Fileds07 = sdr["Fileds07"].ToString();
                        Fileds08 = sdr["Fileds08"].ToString();
                        Fileds09 = sdr["Fileds09"].ToString();
                        Fileds10 = sdr["Fileds10"].ToString();
                        Fileds11 = sdr["Fileds11"].ToString();
                        Fileds12 = sdr["Fileds12"].ToString();
                        Fileds13 = sdr["Fileds13"].ToString();
                        Fileds14 = sdr["Fileds14"].ToString();
                        Fileds15 = sdr["Fileds15"].ToString();
                        Fileds16 = sdr["Fileds16"].ToString();
                        Fileds17 = sdr["Fileds17"].ToString();
                        Fileds18 = sdr["Fileds18"].ToString();
                        Fileds19 = sdr["Fileds19"].ToString();
                        Fileds20 = sdr["Fileds20"].ToString();
                        Fileds21 = sdr["Fileds21"].ToString();
                        Fileds22 = sdr["Fileds22"].ToString();
                        Fileds23 = sdr["Fileds23"].ToString();
                        Fileds24 = sdr["Fileds24"].ToString();
                        Fileds25 = sdr["Fileds25"].ToString();
                        Fileds26 = sdr["Fileds26"].ToString();
                        Fileds27 = sdr["Fileds27"].ToString();
                        Fileds28 = sdr["Fileds28"].ToString();
                        Fileds29 = sdr["Fileds29"].ToString();
                        Fileds30 = sdr["Fileds30"].ToString();
                        Fileds31 = sdr["Fileds31"].ToString();
                        Fileds32 = sdr["Fileds32"].ToString();
                        Fileds33 = sdr["Fileds33"].ToString();
                        Fileds34 = sdr["Fileds34"].ToString();
                        Fileds35 = sdr["Fileds35"].ToString();
                        Fileds36 = sdr["Fileds36"].ToString();
                        Fileds37 = sdr["Fileds37"].ToString();
                        Fileds38 = sdr["Fileds38"].ToString();
                        Fileds39 = sdr["Fileds39"].ToString();
                        Fileds40 = sdr["Fileds40"].ToString();
                        Fileds41 = sdr["Fileds41"].ToString();
                        Fileds42 = sdr["Fileds42"].ToString();
                        Fileds43 = sdr["Fileds43"].ToString();
                        Fileds44 = sdr["Fileds44"].ToString();
                        Fileds45 = sdr["Fileds45"].ToString();
                        Fileds46 = sdr["Fileds46"].ToString();
                        Fileds47 = sdr["Fileds47"].ToString();
                        Fileds48 = sdr["Fileds48"].ToString();
                        Fileds49 = sdr["Fileds49"].ToString();
                        Fileds50 = sdr["Fileds50"].ToString();
                        QcfwzBm = sdr["QcfwzBm"].ToString();

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
        /// 撤销
        /// </summary>
        /// <returns></returns>
        public string Delete()
        {
            string retutnVal = string.Empty;
            try
            {
                string Attribs = CommPage.GetSingleValue("SELECT Attribs FROM BIZ_Contents WHERE ((PersonID=" + this.UserID + " AND InitDirection=0) OR PersonCidA='" + this.PersonCardID + "' OR PersonCidB='" + this.PersonCardID + "') AND InitDirection=0 AND BizID=" + this.BizID);
                if (!String.IsNullOrEmpty(Attribs) && (Attribs == "0" || Attribs == "6"))
                {
                    DbHelperSQL.ExecuteSql("UPDATE BIZ_Contents SET Attribs=4 WHERE BizID =" + this.BizID);
                    retutnVal = "操作成功：您所选择的业务撤销操作成功！";
                }
                else
                {
                    retutnVal = "操作提示：已经开始审核或撤销或不是您本人提交的的业务数据禁止撤销操作！";
                }
            }
            catch (Exception ex)
            {
                retutnVal = "操作失败：" + ex.Message;
            }
            return retutnVal;
        }
        /// <summary>
        /// 催办
        /// </summary>
        /// <param name="bizID"></param>
        public string CuiBan()
        {
            string retutnVal = string.Empty;
            string PersonName = "", PersonTel = string.Empty;
            string BUserID = "", MsgTitle = "", MsgBody = string.Empty;
            SqlDataReader sdr = null;
            try
            {
                string sqlParams = "SELECT PersonName,PersonTel FROM BIZ_Persons WHERE PersonID=" + this.UserID;
                sdr = DbHelperSQL.ExecuteReader(sqlParams);
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        PersonName = sdr["PersonName"].ToString();
                        PersonTel = sdr["PersonTel"].ToString();
                    }
                }
                sdr.Close();

                // BIZ_Contents --> Attribs: 0,初始提交;1,审核中 2,通过 3,补正 4,撤销, 5,注销 9,归档
                string Attribs = CommPage.GetSingleValue("SELECT Attribs FROM BIZ_Contents WHERE (PersonID=" + this.UserID + "  OR PersonCidA='" + PersonCardID + "' OR PersonCidB='" + PersonCardID + "') AND BizID=" + this.BizID);
                if (!String.IsNullOrEmpty(Attribs) && (Attribs == "1" || Attribs == "6"))
                {
                    sqlParams = "SELECT TOP 1 AreaCode,(SELECT UserID FROM USER_BaseInfo WHERE UserAccount=A.AreaCode) AS UserID,(SELECT BizName FROM BIZ_Contents WHERE BizID=" + this.BizID + ") AS BizName FROM BIZ_WorkFlows A WHERE Attribs=9 AND BizID=" + this.BizID + " ORDER BY BizStep";
                    sdr = DbHelperSQL.ExecuteReader(sqlParams);
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            BUserID = sdr["UserID"].ToString();
                            MsgTitle = sdr["BizName"].ToString() + "催办";
                            MsgBody = "请尽快办理！催办人：" + PersonName + "  联系电话：" + PersonTel;
                        }
                    }
                    sdr.Close(); sdr.Dispose();
                    sqlParams = "INSERT INTO [SYS_Msg]([SourceUserID], [TargetUserID], [MsgTitle], [MsgBody], [MsgType]) VALUES(1, " + BUserID + ", '" + MsgTitle + "', '" + MsgBody + "', 2)";
                    if (DbHelperSQL.ExecuteSql(sqlParams) > 0)
                    {
                        retutnVal = "操作提示：您所选择的业务催办操作成功！";
                    }
                }
                else
                {
                    retutnVal= "操作提示：仅待审核或审核中的业务才可以执行催办操作！";
                }
            }
            catch (Exception ex)
            {
                if (sdr != null) { sdr.Close(); sdr.Dispose(); }
                retutnVal= "操作提示：" + ex.Message;
            }
            return retutnVal;
        }
        public void UpdateLog()
        { }
        #endregion

        #region 其他操作
        /// <summary>
        /// 证照上传完毕后，更新业务状态
        /// </summary>
        /// <param name="bizID"></param>
        public void UpdateAttribs()
        {
            try
            {
                if (!String.IsNullOrEmpty(Attribs) && Attribs != null)
                {
                    if (Attribs == "0") { DbHelperSQL.ExecuteSql("UPDATE BIZ_Contents SET Comments='" + Comments + "',Attribs=6 WHERE BizID=" + BizID); }
                    else if (Attribs == "3")
                    {
                        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(2);
                        list.Add("UPDATE BIZ_Contents SET Attribs=1 WHERE BizID=" + BizID); // 处理业务状态
                        list.Add("UPDATE BIZ_WorkFlows SET Attribs=9 WHERE BizID=" + BizID + " AND Attribs=0"); //处理流程状态
                        DbHelperSQL.ExecuteSqlTran(list);
                        list = null;
                    }
                    else { DbHelperSQL.ExecuteSql("UPDATE BIZ_Contents SET Comments='" + Comments + "' WHERE BizID=" + BizID); }
                }
                else
                {
                    DbHelperSQL.ExecuteSql("UPDATE BIZ_Contents SET Comments='" + Comments + "',Attribs=6 WHERE BizID=" + BizID);
                }
            }
            catch { }
        }
        #endregion

    }
}
