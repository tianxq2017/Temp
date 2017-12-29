using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using UNV.Comm.DataBase;

namespace join.pms.dal
{
    public class SysSeal
    {
        public string SealUserID;
        public string SealID;
        public string SealPath;
        public string SealPass;
        public string UserAccount;
        public string UserName;
        public string UserAreaCode;
        public string SealName;
        public string UserTel;


        /// <summary>
        /// 获取当前用户的签章信息
        /// </summary>
        public void GetSealByUser()
        {
            if (!string.IsNullOrEmpty(SealUserID)) {
                SqlDataReader sdr = null;
                string sqlParams = "SELECT SealID,SealPath,SealPass,UserAccount,UserName,UserAreaCode,SealName,UserTel FROM v_SYS_Seal WHERE SealUserID=" + SealUserID;
                try
                {
                    sdr = DbHelperSQL.ExecuteReader(sqlParams);
                    while (sdr.Read())
                    {
                        SealID = CommBiz.GetTrim(sdr[0].ToString());
                        SealPath = CommBiz.GetTrim(sdr[1].ToString());
                        SealPass = CommBiz.GetTrim(sdr[2].ToString());
                        UserAccount = CommBiz.GetTrim(sdr[3].ToString());
                        UserName = CommBiz.GetTrim(sdr[4].ToString());
                        UserAreaCode = CommBiz.GetTrim(sdr[5].ToString());
                        SealName = CommBiz.GetTrim(sdr[6].ToString());
                        UserTel = CommBiz.GetTrim(sdr[7].ToString());
                    }
                    sdr.Close();
                }
                catch { if (sdr != null) sdr.Close(); }
            }
        }
    }
}
