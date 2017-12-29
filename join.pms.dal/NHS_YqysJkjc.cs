using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using UNV.Comm.DataBase;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_YqysJkjc。
    /// </summary>
    [Serializable]
    public partial class NHS_YqysJkjc
    {
        public NHS_YqysJkjc()
        { }
        #region Model
        private int _commid;
        private string _unvid;
        private string _qcfwzbm;
        private string _wifename;
        private string _wifeage;
        private string _wifetel;
        private string _wifecid;
        private string _manname;
        private string _manage;
        private string _mantel;
        private string _mancid;
        private string _homeaddress;
        private string _areacode;
        private string _results;
        private string _advice;
        private string _doctor;
        private DateTime? _checkdate;
        private string _checkunit;
        private DateTime? _createdate;
        private int? _userid;
        /// <summary>
        /// 
        /// </summary>
        public int commid
        {
            set { _commid = value; }
            get { return _commid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UnvID
        {
            set { _unvid = value; }
            get { return _unvid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QcfwzBm
        {
            set { _qcfwzbm = value; }
            get { return _qcfwzbm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WifeName
        {
            set { _wifename = value; }
            get { return _wifename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WifeAge
        {
            set { _wifeage = value; }
            get { return _wifeage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WifeTel
        {
            set { _wifetel = value; }
            get { return _wifetel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WifeCID
        {
            set { _wifecid = value; }
            get { return _wifecid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ManName
        {
            set { _manname = value; }
            get { return _manname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ManAge
        {
            set { _manage = value; }
            get { return _manage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ManTel
        {
            set { _mantel = value; }
            get { return _mantel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ManCID
        {
            set { _mancid = value; }
            get { return _mancid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HomeAddress
        {
            set { _homeaddress = value; }
            get { return _homeaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AreaCode
        {
            set { _areacode = value; }
            get { return _areacode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Results
        {
            set { _results = value; }
            get { return _results; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Advice
        {
            set { _advice = value; }
            get { return _advice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Doctor
        {
            set { _doctor = value; }
            get { return _doctor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckUnit
        {
            set { _checkunit = value; }
            get { return _checkunit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NHS_YqysJkjc(int commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select commid,UnvID,QcfwzBm,WifeName,WifeAge,WifeTel,WifeCID,ManName,ManAge,ManTel,ManCID,HomeAddress,AreaCode,Results,Advice,Doctor,CheckDate,CheckUnit,CreateDate,UserID ");
            strSql.Append(" FROM [NHS_YqysJkjc] ");
            strSql.Append(" where commid=@commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@commid", SqlDbType.Int,4)};
            parameters[0].Value = commid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["commid"] != null && ds.Tables[0].Rows[0]["commid"].ToString() != "")
                {
                    this.commid = int.Parse(ds.Tables[0].Rows[0]["commid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UnvID"] != null)
                {
                    this.UnvID = ds.Tables[0].Rows[0]["UnvID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QcfwzBm"] != null)
                {
                    this.QcfwzBm = ds.Tables[0].Rows[0]["QcfwzBm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WifeName"] != null)
                {
                    this.WifeName = ds.Tables[0].Rows[0]["WifeName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WifeAge"] != null)
                {
                    this.WifeAge = ds.Tables[0].Rows[0]["WifeAge"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WifeTel"] != null)
                {
                    this.WifeTel = ds.Tables[0].Rows[0]["WifeTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WifeCID"] != null)
                {
                    this.WifeCID = ds.Tables[0].Rows[0]["WifeCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ManName"] != null)
                {
                    this.ManName = ds.Tables[0].Rows[0]["ManName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ManAge"] != null)
                {
                    this.ManAge = ds.Tables[0].Rows[0]["ManAge"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ManTel"] != null)
                {
                    this.ManTel = ds.Tables[0].Rows[0]["ManTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ManCID"] != null)
                {
                    this.ManCID = ds.Tables[0].Rows[0]["ManCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HomeAddress"] != null)
                {
                    this.HomeAddress = ds.Tables[0].Rows[0]["HomeAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AreaCode"] != null)
                {
                    this.AreaCode = ds.Tables[0].Rows[0]["AreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Results"] != null)
                {
                    this.Results = ds.Tables[0].Rows[0]["Results"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Advice"] != null)
                {
                    this.Advice = ds.Tables[0].Rows[0]["Advice"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Doctor"] != null)
                {
                    this.Doctor = ds.Tables[0].Rows[0]["Doctor"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CheckDate"] != null && ds.Tables[0].Rows[0]["CheckDate"].ToString() != "")
                {
                    this.CheckDate = DateTime.Parse(ds.Tables[0].Rows[0]["CheckDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CheckUnit"] != null)
                {
                    this.CheckUnit = ds.Tables[0].Rows[0]["CheckUnit"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_YqysJkjc]");
            strSql.Append(" where commid=@commid ");

            SqlParameter[] parameters = {
					new SqlParameter("@commid", SqlDbType.Int,4)};
            parameters[0].Value = commid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [NHS_YqysJkjc] (");
            strSql.Append("UnvID,QcfwzBm,WifeName,WifeAge,WifeTel,WifeCID,ManName,ManAge,ManTel,ManCID,HomeAddress,AreaCode,Results,Advice,Doctor,CheckDate,CheckUnit,CreateDate,UserID)");
            strSql.Append(" values (");
            strSql.Append("@UnvID,@QcfwzBm,@WifeName,@WifeAge,@WifeTel,@WifeCID,@ManName,@ManAge,@ManTel,@ManCID,@HomeAddress,@AreaCode,@Results,@Advice,@Doctor,@CheckDate,@CheckUnit,@CreateDate,@UserID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@QcfwzBm", SqlDbType.VarChar,50),
					new SqlParameter("@WifeName", SqlDbType.VarChar,50),
					new SqlParameter("@WifeAge", SqlDbType.VarChar,10),
					new SqlParameter("@WifeTel", SqlDbType.VarChar,50),
					new SqlParameter("@WifeCID", SqlDbType.VarChar,20),
					new SqlParameter("@ManName", SqlDbType.VarChar,50),
					new SqlParameter("@ManAge", SqlDbType.VarChar,10),
					new SqlParameter("@ManTel", SqlDbType.VarChar,50),
					new SqlParameter("@ManCID", SqlDbType.VarChar,20),
					new SqlParameter("@HomeAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaCode", SqlDbType.VarChar,20),
					new SqlParameter("@Results", SqlDbType.NVarChar,50),
					new SqlParameter("@Advice", SqlDbType.NVarChar,50),
					new SqlParameter("@Doctor", SqlDbType.VarChar,50),
					new SqlParameter("@CheckDate", SqlDbType.SmallDateTime),
					new SqlParameter("@CheckUnit", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = UnvID;
            parameters[1].Value = QcfwzBm;
            parameters[2].Value = WifeName;
            parameters[3].Value = WifeAge;
            parameters[4].Value = WifeTel;
            parameters[5].Value = WifeCID;
            parameters[6].Value = ManName;
            parameters[7].Value = ManAge;
            parameters[8].Value = ManTel;
            parameters[9].Value = ManCID;
            parameters[10].Value = HomeAddress;
            parameters[11].Value = AreaCode;
            parameters[12].Value = Results;
            parameters[13].Value = Advice;
            parameters[14].Value = Doctor;
            parameters[15].Value = CheckDate;
            parameters[16].Value = CheckUnit;
            parameters[17].Value = CreateDate;
            parameters[18].Value = UserID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [NHS_YqysJkjc] set ");
            strSql.Append("UnvID=@UnvID,");
            strSql.Append("QcfwzBm=@QcfwzBm,");
            strSql.Append("WifeName=@WifeName,");
            strSql.Append("WifeAge=@WifeAge,");
            strSql.Append("WifeTel=@WifeTel,");
            strSql.Append("WifeCID=@WifeCID,");
            strSql.Append("ManName=@ManName,");
            strSql.Append("ManAge=@ManAge,");
            strSql.Append("ManTel=@ManTel,");
            strSql.Append("ManCID=@ManCID,");
            strSql.Append("HomeAddress=@HomeAddress,");
            strSql.Append("AreaCode=@AreaCode,");
            strSql.Append("Results=@Results,");
            strSql.Append("Advice=@Advice,");
            strSql.Append("Doctor=@Doctor,");
            strSql.Append("CheckDate=@CheckDate,");
            strSql.Append("CheckUnit=@CheckUnit,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UserID=@UserID");
            strSql.Append(" where commid=@commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@QcfwzBm", SqlDbType.VarChar,50),
					new SqlParameter("@WifeName", SqlDbType.VarChar,50),
					new SqlParameter("@WifeAge", SqlDbType.VarChar,10),
					new SqlParameter("@WifeTel", SqlDbType.VarChar,50),
					new SqlParameter("@WifeCID", SqlDbType.VarChar,20),
					new SqlParameter("@ManName", SqlDbType.VarChar,50),
					new SqlParameter("@ManAge", SqlDbType.VarChar,10),
					new SqlParameter("@ManTel", SqlDbType.VarChar,50),
					new SqlParameter("@ManCID", SqlDbType.VarChar,20),
					new SqlParameter("@HomeAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaCode", SqlDbType.VarChar,20),
					new SqlParameter("@Results", SqlDbType.NVarChar,50),
					new SqlParameter("@Advice", SqlDbType.NVarChar,50),
					new SqlParameter("@Doctor", SqlDbType.VarChar,50),
					new SqlParameter("@CheckDate", SqlDbType.SmallDateTime),
					new SqlParameter("@CheckUnit", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@commid", SqlDbType.Int,4)};
            parameters[0].Value = UnvID;
            parameters[1].Value = QcfwzBm;
            parameters[2].Value = WifeName;
            parameters[3].Value = WifeAge;
            parameters[4].Value = WifeTel;
            parameters[5].Value = WifeCID;
            parameters[6].Value = ManName;
            parameters[7].Value = ManAge;
            parameters[8].Value = ManTel;
            parameters[9].Value = ManCID;
            parameters[10].Value = HomeAddress;
            parameters[11].Value = AreaCode;
            parameters[12].Value = Results;
            parameters[13].Value = Advice;
            parameters[14].Value = Doctor;
            parameters[15].Value = CheckDate;
            parameters[16].Value = CheckUnit;
            parameters[17].Value = CreateDate;
            parameters[18].Value = UserID;
            parameters[19].Value = commid;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [NHS_YqysJkjc] ");
            strSql.Append(" where commid=@commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@commid", SqlDbType.Int,4)};
            parameters[0].Value = commid;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select commid,UnvID,QcfwzBm,WifeName,WifeAge,WifeTel,WifeCID,ManName,ManAge,ManTel,ManCID,HomeAddress,AreaCode,Results,Advice,Doctor,CheckDate,CheckUnit,CreateDate,UserID ");
            strSql.Append(" FROM [NHS_YqysJkjc] ");
            strSql.Append(" where commid=@commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@commid", SqlDbType.Int,4)};
            parameters[0].Value = commid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["commid"] != null && ds.Tables[0].Rows[0]["commid"].ToString() != "")
                {
                    this.commid = int.Parse(ds.Tables[0].Rows[0]["commid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UnvID"] != null)
                {
                    this.UnvID = ds.Tables[0].Rows[0]["UnvID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QcfwzBm"] != null)
                {
                    this.QcfwzBm = ds.Tables[0].Rows[0]["QcfwzBm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WifeName"] != null)
                {
                    this.WifeName = ds.Tables[0].Rows[0]["WifeName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WifeAge"] != null)
                {
                    this.WifeAge = ds.Tables[0].Rows[0]["WifeAge"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WifeTel"] != null)
                {
                    this.WifeTel = ds.Tables[0].Rows[0]["WifeTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WifeCID"] != null)
                {
                    this.WifeCID = ds.Tables[0].Rows[0]["WifeCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ManName"] != null)
                {
                    this.ManName = ds.Tables[0].Rows[0]["ManName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ManAge"] != null)
                {
                    this.ManAge = ds.Tables[0].Rows[0]["ManAge"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ManTel"] != null)
                {
                    this.ManTel = ds.Tables[0].Rows[0]["ManTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ManCID"] != null)
                {
                    this.ManCID = ds.Tables[0].Rows[0]["ManCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HomeAddress"] != null)
                {
                    this.HomeAddress = ds.Tables[0].Rows[0]["HomeAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AreaCode"] != null)
                {
                    this.AreaCode = ds.Tables[0].Rows[0]["AreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Results"] != null)
                {
                    this.Results = ds.Tables[0].Rows[0]["Results"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Advice"] != null)
                {
                    this.Advice = ds.Tables[0].Rows[0]["Advice"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Doctor"] != null)
                {
                    this.Doctor = ds.Tables[0].Rows[0]["Doctor"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CheckDate"] != null && ds.Tables[0].Rows[0]["CheckDate"].ToString() != "")
                {
                    this.CheckDate = DateTime.Parse(ds.Tables[0].Rows[0]["CheckDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CheckUnit"] != null)
                {
                    this.CheckUnit = ds.Tables[0].Rows[0]["CheckUnit"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [NHS_YqysJkjc] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
