using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using UNV.Comm.DataBase;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_YiMiaoInfo。
    /// </summary>
    [Serializable]
    public partial class NHS_YiMiaoInfo
    {
        public NHS_YiMiaoInfo()
        { }
        #region Model
        private string _drpbacid;
        private string _drpbacclass;
        private string _drpbacname;
        private string _drpbacclass1;
        private string _drpbacname1;
        private string _company;
        private string _drpbacphid;
        private string _drpbactype;
        private string _drpbacguige;
        private string _drpbaczhushe;
        private string _drpbacfangshi;
        private DateTime? _chuchangdate;
        private DateTime? _shixiaodate;
        private int? _isdel;
        private string _cityname;
        private string _areacode;
        private string _pointname;
        private int? _pointcommid;
        private int _commid;
        private int? _cpid;
        private DateTime? _createdate;
        private int? _createuser;
        private DateTime? _lastupdatedate;
        private int? _lastupdateuser;
        /// <summary>
        /// 
        /// </summary>
        public string DrpBacID
        {
            set { _drpbacid = value; }
            get { return _drpbacid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrpBacClass
        {
            set { _drpbacclass = value; }
            get { return _drpbacclass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrpBacName
        {
            set { _drpbacname = value; }
            get { return _drpbacname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrpBacClass1
        {
            set { _drpbacclass1 = value; }
            get { return _drpbacclass1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrpBacName1
        {
            set { _drpbacname1 = value; }
            get { return _drpbacname1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Company
        {
            set { _company = value; }
            get { return _company; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrpBacPHID
        {
            set { _drpbacphid = value; }
            get { return _drpbacphid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrpBacType
        {
            set { _drpbactype = value; }
            get { return _drpbactype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrpBacGuige
        {
            set { _drpbacguige = value; }
            get { return _drpbacguige; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrpBacZhushe
        {
            set { _drpbaczhushe = value; }
            get { return _drpbaczhushe; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrpBacFangShi
        {
            set { _drpbacfangshi = value; }
            get { return _drpbacfangshi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ChuChangDate
        {
            set { _chuchangdate = value; }
            get { return _chuchangdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ShiXiaoDate
        {
            set { _shixiaodate = value; }
            get { return _shixiaodate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Cityname
        {
            set { _cityname = value; }
            get { return _cityname; }
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
        public string PointName
        {
            set { _pointname = value; }
            get { return _pointname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PointCommid
        {
            set { _pointcommid = value; }
            get { return _pointcommid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CommID
        {
            set { _commid = value; }
            get { return _commid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Cpid
        {
            set { _cpid = value; }
            get { return _cpid; }
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
        public int? CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateDate
        {
            set { _lastupdatedate = value; }
            get { return _lastupdatedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? LastUpdateUser
        {
            set { _lastupdateuser = value; }
            get { return _lastupdateuser; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NHS_YiMiaoInfo(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DrpBacID,DrpBacClass,DrpBacName,DrpBacClass1,DrpBacName1,Company,DrpBacPHID,DrpBacType,DrpBacGuige,DrpBacZhushe,DrpBacFangShi,ChuChangDate,ShiXiaoDate,IsDel,Cityname,AreaCode,PointName,PointCommid,CommID,Cpid,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser ");
            strSql.Append(" FROM [NHS_YiMiaoInfo] ");
            strSql.Append(" where CommID=@CommID ");
            SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = CommID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["DrpBacID"] != null)
                {
                    this.DrpBacID = ds.Tables[0].Rows[0]["DrpBacID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacClass"] != null)
                {
                    this.DrpBacClass = ds.Tables[0].Rows[0]["DrpBacClass"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacName"] != null)
                {
                    this.DrpBacName = ds.Tables[0].Rows[0]["DrpBacName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacClass1"] != null)
                {
                    this.DrpBacClass1 = ds.Tables[0].Rows[0]["DrpBacClass1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacName1"] != null)
                {
                    this.DrpBacName1 = ds.Tables[0].Rows[0]["DrpBacName1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Company"] != null)
                {
                    this.Company = ds.Tables[0].Rows[0]["Company"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacPHID"] != null)
                {
                    this.DrpBacPHID = ds.Tables[0].Rows[0]["DrpBacPHID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacType"] != null)
                {
                    this.DrpBacType = ds.Tables[0].Rows[0]["DrpBacType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacGuige"] != null)
                {
                    this.DrpBacGuige = ds.Tables[0].Rows[0]["DrpBacGuige"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacZhushe"] != null)
                {
                    this.DrpBacZhushe = ds.Tables[0].Rows[0]["DrpBacZhushe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacFangShi"] != null)
                {
                    this.DrpBacFangShi = ds.Tables[0].Rows[0]["DrpBacFangShi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuChangDate"] != null && ds.Tables[0].Rows[0]["ChuChangDate"].ToString() != "")
                {
                    this.ChuChangDate = DateTime.Parse(ds.Tables[0].Rows[0]["ChuChangDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ShiXiaoDate"] != null && ds.Tables[0].Rows[0]["ShiXiaoDate"].ToString() != "")
                {
                    this.ShiXiaoDate = DateTime.Parse(ds.Tables[0].Rows[0]["ShiXiaoDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDel"] != null && ds.Tables[0].Rows[0]["IsDel"].ToString() != "")
                {
                    this.IsDel = int.Parse(ds.Tables[0].Rows[0]["IsDel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Cityname"] != null)
                {
                    this.Cityname = ds.Tables[0].Rows[0]["Cityname"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AreaCode"] != null)
                {
                    this.AreaCode = ds.Tables[0].Rows[0]["AreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PointName"] != null)
                {
                    this.PointName = ds.Tables[0].Rows[0]["PointName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PointCommid"] != null && ds.Tables[0].Rows[0]["PointCommid"].ToString() != "")
                {
                    this.PointCommid = int.Parse(ds.Tables[0].Rows[0]["PointCommid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CommID"] != null && ds.Tables[0].Rows[0]["CommID"].ToString() != "")
                {
                    this.CommID = int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Cpid"] != null && ds.Tables[0].Rows[0]["Cpid"].ToString() != "")
                {
                    this.Cpid = int.Parse(ds.Tables[0].Rows[0]["Cpid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateUser"] != null && ds.Tables[0].Rows[0]["CreateUser"].ToString() != "")
                {
                    this.CreateUser = int.Parse(ds.Tables[0].Rows[0]["CreateUser"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastUpdateDate"] != null && ds.Tables[0].Rows[0]["LastUpdateDate"].ToString() != "")
                {
                    this.LastUpdateDate = DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastUpdateUser"] != null && ds.Tables[0].Rows[0]["LastUpdateUser"].ToString() != "")
                {
                    this.LastUpdateUser = int.Parse(ds.Tables[0].Rows[0]["LastUpdateUser"].ToString());
                }
            }
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {

            return DbHelperSQL.GetMaxID("CommID", "NHS_YiMiaoInfo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_YiMiaoInfo]");
            strSql.Append(" where CommID=@CommID ");

            SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = CommID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [NHS_YiMiaoInfo] (");
            strSql.Append("DrpBacID,DrpBacClass,DrpBacName,DrpBacClass1,DrpBacName1,Company,DrpBacPHID,DrpBacType,DrpBacGuige,DrpBacZhushe,DrpBacFangShi,ChuChangDate,ShiXiaoDate,IsDel,Cityname,AreaCode,PointName,PointCommid,CommID,Cpid,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser)");
            strSql.Append(" values (");
            strSql.Append("@DrpBacID,@DrpBacClass,@DrpBacName,@DrpBacClass1,@DrpBacName1,@Company,@DrpBacPHID,@DrpBacType,@DrpBacGuige,@DrpBacZhushe,@DrpBacFangShi,@ChuChangDate,@ShiXiaoDate,@IsDel,@Cityname,@AreaCode,@PointName,@PointCommid,@CommID,@Cpid,@CreateDate,@CreateUser,@LastUpdateDate,@LastUpdateUser)");
            SqlParameter[] parameters = {
					new SqlParameter("@DrpBacID", SqlDbType.VarChar,20),
					new SqlParameter("@DrpBacClass", SqlDbType.VarChar,10),
					new SqlParameter("@DrpBacName", SqlDbType.NVarChar,50),
					new SqlParameter("@DrpBacClass1", SqlDbType.VarChar,10),
					new SqlParameter("@DrpBacName1", SqlDbType.NVarChar,50),
					new SqlParameter("@Company", SqlDbType.NVarChar,50),
					new SqlParameter("@DrpBacPHID", SqlDbType.VarChar,20),
					new SqlParameter("@DrpBacType", SqlDbType.VarChar,20),
					new SqlParameter("@DrpBacGuige", SqlDbType.VarChar,20),
					new SqlParameter("@DrpBacZhushe", SqlDbType.VarChar,20),
					new SqlParameter("@DrpBacFangShi", SqlDbType.VarChar,20),
					new SqlParameter("@ChuChangDate", SqlDbType.SmallDateTime),
					new SqlParameter("@ShiXiaoDate", SqlDbType.SmallDateTime),
					new SqlParameter("@IsDel", SqlDbType.TinyInt,1),
					new SqlParameter("@Cityname", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaCode", SqlDbType.VarChar,12),
					new SqlParameter("@PointName", SqlDbType.NVarChar,50),
					new SqlParameter("@PointCommid", SqlDbType.Int,4),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@Cpid", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4)};
            parameters[0].Value = DrpBacID;
            parameters[1].Value = DrpBacClass;
            parameters[2].Value = DrpBacName;
            parameters[3].Value = DrpBacClass1;
            parameters[4].Value = DrpBacName1;
            parameters[5].Value = Company;
            parameters[6].Value = DrpBacPHID;
            parameters[7].Value = DrpBacType;
            parameters[8].Value = DrpBacGuige;
            parameters[9].Value = DrpBacZhushe;
            parameters[10].Value = DrpBacFangShi;
            parameters[11].Value = ChuChangDate;
            parameters[12].Value = ShiXiaoDate;
            parameters[13].Value = IsDel;
            parameters[14].Value = Cityname;
            parameters[15].Value = AreaCode;
            parameters[16].Value = PointName;
            parameters[17].Value = PointCommid;
            parameters[18].Value = CommID;
            parameters[19].Value = Cpid;
            parameters[20].Value = CreateDate;
            parameters[21].Value = CreateUser;
            parameters[22].Value = LastUpdateDate;
            parameters[23].Value = LastUpdateUser;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [NHS_YiMiaoInfo] set ");
            strSql.Append("DrpBacID=@DrpBacID,");
            strSql.Append("DrpBacClass=@DrpBacClass,");
            strSql.Append("DrpBacName=@DrpBacName,");
            strSql.Append("DrpBacClass1=@DrpBacClass1,");
            strSql.Append("DrpBacName1=@DrpBacName1,");
            strSql.Append("Company=@Company,");
            strSql.Append("DrpBacPHID=@DrpBacPHID,");
            strSql.Append("DrpBacType=@DrpBacType,");
            strSql.Append("DrpBacGuige=@DrpBacGuige,");
            strSql.Append("DrpBacZhushe=@DrpBacZhushe,");
            strSql.Append("DrpBacFangShi=@DrpBacFangShi,");
            strSql.Append("ChuChangDate=@ChuChangDate,");
            strSql.Append("ShiXiaoDate=@ShiXiaoDate,");
            strSql.Append("IsDel=@IsDel,");
            strSql.Append("Cityname=@Cityname,");
            strSql.Append("AreaCode=@AreaCode,");
            strSql.Append("PointName=@PointName,");
            strSql.Append("PointCommid=@PointCommid,");
            strSql.Append("Cpid=@Cpid,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("LastUpdateDate=@LastUpdateDate,");
            strSql.Append("LastUpdateUser=@LastUpdateUser");
            strSql.Append(" where CommID=@CommID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DrpBacID", SqlDbType.VarChar,20),
					new SqlParameter("@DrpBacClass", SqlDbType.VarChar,10),
					new SqlParameter("@DrpBacName", SqlDbType.NVarChar,50),
					new SqlParameter("@DrpBacClass1", SqlDbType.VarChar,10),
					new SqlParameter("@DrpBacName1", SqlDbType.NVarChar,50),
					new SqlParameter("@Company", SqlDbType.NVarChar,50),
					new SqlParameter("@DrpBacPHID", SqlDbType.VarChar,20),
					new SqlParameter("@DrpBacType", SqlDbType.VarChar,20),
					new SqlParameter("@DrpBacGuige", SqlDbType.VarChar,20),
					new SqlParameter("@DrpBacZhushe", SqlDbType.VarChar,20),
					new SqlParameter("@DrpBacFangShi", SqlDbType.VarChar,20),
					new SqlParameter("@ChuChangDate", SqlDbType.SmallDateTime),
					new SqlParameter("@ShiXiaoDate", SqlDbType.SmallDateTime),
					new SqlParameter("@IsDel", SqlDbType.TinyInt,1),
					new SqlParameter("@Cityname", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaCode", SqlDbType.VarChar,12),
					new SqlParameter("@PointName", SqlDbType.NVarChar,50),
					new SqlParameter("@PointCommid", SqlDbType.Int,4),
					new SqlParameter("@Cpid", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4),
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = DrpBacID;
            parameters[1].Value = DrpBacClass;
            parameters[2].Value = DrpBacName;
            parameters[3].Value = DrpBacClass1;
            parameters[4].Value = DrpBacName1;
            parameters[5].Value = Company;
            parameters[6].Value = DrpBacPHID;
            parameters[7].Value = DrpBacType;
            parameters[8].Value = DrpBacGuige;
            parameters[9].Value = DrpBacZhushe;
            parameters[10].Value = DrpBacFangShi;
            parameters[11].Value = ChuChangDate;
            parameters[12].Value = ShiXiaoDate;
            parameters[13].Value = IsDel;
            parameters[14].Value = Cityname;
            parameters[15].Value = AreaCode;
            parameters[16].Value = PointName;
            parameters[17].Value = PointCommid;
            parameters[18].Value = Cpid;
            parameters[19].Value = CreateDate;
            parameters[20].Value = CreateUser;
            parameters[21].Value = LastUpdateDate;
            parameters[22].Value = LastUpdateUser;
            parameters[23].Value = CommID;

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
        public bool Delete(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [NHS_YiMiaoInfo] ");
            strSql.Append(" where CommID=@CommID ");
            SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = CommID;

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
        public void GetModel(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DrpBacID,DrpBacClass,DrpBacName,DrpBacClass1,DrpBacName1,Company,DrpBacPHID,DrpBacType,DrpBacGuige,DrpBacZhushe,DrpBacFangShi,ChuChangDate,ShiXiaoDate,IsDel,Cityname,AreaCode,PointName,PointCommid,CommID,Cpid,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser ");
            strSql.Append(" FROM [NHS_YiMiaoInfo] ");
            strSql.Append(" where CommID=@CommID ");
            SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = CommID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["DrpBacID"] != null)
                {
                    this.DrpBacID = ds.Tables[0].Rows[0]["DrpBacID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacClass"] != null)
                {
                    this.DrpBacClass = ds.Tables[0].Rows[0]["DrpBacClass"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacName"] != null)
                {
                    this.DrpBacName = ds.Tables[0].Rows[0]["DrpBacName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacClass1"] != null)
                {
                    this.DrpBacClass1 = ds.Tables[0].Rows[0]["DrpBacClass1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacName1"] != null)
                {
                    this.DrpBacName1 = ds.Tables[0].Rows[0]["DrpBacName1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Company"] != null)
                {
                    this.Company = ds.Tables[0].Rows[0]["Company"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacPHID"] != null)
                {
                    this.DrpBacPHID = ds.Tables[0].Rows[0]["DrpBacPHID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacType"] != null)
                {
                    this.DrpBacType = ds.Tables[0].Rows[0]["DrpBacType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacGuige"] != null)
                {
                    this.DrpBacGuige = ds.Tables[0].Rows[0]["DrpBacGuige"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacZhushe"] != null)
                {
                    this.DrpBacZhushe = ds.Tables[0].Rows[0]["DrpBacZhushe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DrpBacFangShi"] != null)
                {
                    this.DrpBacFangShi = ds.Tables[0].Rows[0]["DrpBacFangShi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuChangDate"] != null && ds.Tables[0].Rows[0]["ChuChangDate"].ToString() != "")
                {
                    this.ChuChangDate = DateTime.Parse(ds.Tables[0].Rows[0]["ChuChangDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ShiXiaoDate"] != null && ds.Tables[0].Rows[0]["ShiXiaoDate"].ToString() != "")
                {
                    this.ShiXiaoDate = DateTime.Parse(ds.Tables[0].Rows[0]["ShiXiaoDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDel"] != null && ds.Tables[0].Rows[0]["IsDel"].ToString() != "")
                {
                    this.IsDel = int.Parse(ds.Tables[0].Rows[0]["IsDel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Cityname"] != null)
                {
                    this.Cityname = ds.Tables[0].Rows[0]["Cityname"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AreaCode"] != null)
                {
                    this.AreaCode = ds.Tables[0].Rows[0]["AreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PointName"] != null)
                {
                    this.PointName = ds.Tables[0].Rows[0]["PointName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PointCommid"] != null && ds.Tables[0].Rows[0]["PointCommid"].ToString() != "")
                {
                    this.PointCommid = int.Parse(ds.Tables[0].Rows[0]["PointCommid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CommID"] != null && ds.Tables[0].Rows[0]["CommID"].ToString() != "")
                {
                    this.CommID = int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Cpid"] != null && ds.Tables[0].Rows[0]["Cpid"].ToString() != "")
                {
                    this.Cpid = int.Parse(ds.Tables[0].Rows[0]["Cpid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateUser"] != null && ds.Tables[0].Rows[0]["CreateUser"].ToString() != "")
                {
                    this.CreateUser = int.Parse(ds.Tables[0].Rows[0]["CreateUser"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastUpdateDate"] != null && ds.Tables[0].Rows[0]["LastUpdateDate"].ToString() != "")
                {
                    this.LastUpdateDate = DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastUpdateUser"] != null && ds.Tables[0].Rows[0]["LastUpdateUser"].ToString() != "")
                {
                    this.LastUpdateUser = int.Parse(ds.Tables[0].Rows[0]["LastUpdateUser"].ToString());
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
            strSql.Append(" FROM [NHS_YiMiaoInfo] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
