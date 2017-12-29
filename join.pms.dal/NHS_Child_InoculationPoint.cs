using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using UNV.Comm.DataBase;
using System.Data.SqlClient;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_Child_InoculationPoint。
    /// </summary>
    [Serializable]
    public partial class NHS_Child_InoculationPoint
    {
        public NHS_Child_InoculationPoint()
        { }
        #region Model
        private int _commid;
        private string _name;
        private string _areacode;
        private string _type;
        private string _inoculationpointcode;
        private string _contacttel;
        private string _coordinate;
        private string _pinyincode;
        private DateTime? _createdate;
        private int? _createuser;
        private DateTime? _lastupdatedate;
        private int? _lastupdateuser;
        private string _areaname;
        /// <summary>
        /// 
        /// </summary>
        public int Commid
        {
            set { _commid = value; }
            get { return _commid; }
        }
        /// <summary>
        /// 接种点名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 区域编码
        /// </summary>
        public string AreaCode
        {
            set { _areacode = value; }
            get { return _areacode; }
        }
        /// <summary>
        /// 接种类型
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 接种点编码
        /// </summary>
        public string InoculationPointCode
        {
            set { _inoculationpointcode = value; }
            get { return _inoculationpointcode; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel
        {
            set { _contacttel = value; }
            get { return _contacttel; }
        }
        /// <summary>
        /// 坐标
        /// </summary>
        public string Coordinate
        {
            set { _coordinate = value; }
            get { return _coordinate; }
        }
        /// <summary>
        /// 汉语简拼
        /// </summary>
        public string PinYinCode
        {
            set { _pinyincode = value; }
            get { return _pinyincode; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int? CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 最后修改日期
        /// </summary>
        public DateTime? LastUpdateDate
        {
            set { _lastupdatedate = value; }
            get { return _lastupdatedate; }
        }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public int? LastUpdateUser
        {
            set { _lastupdateuser = value; }
            get { return _lastupdateuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AreaName
        {
            set { _areaname = value; }
            get { return _areaname; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NHS_Child_InoculationPoint(int Commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Commid,Name,AreaCode,Type,InoculationPointCode,ContactTel,Coordinate,PinYinCode,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser,AreaName ");
            strSql.Append(" FROM [NHS_Child_InoculationPoint] ");
            strSql.Append(" where Commid=@Commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Commid", SqlDbType.Int,4)};
            parameters[0].Value = Commid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Commid"] != null && ds.Tables[0].Rows[0]["Commid"].ToString() != "")
                {
                    this.Commid = int.Parse(ds.Tables[0].Rows[0]["Commid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AreaCode"] != null)
                {
                    this.AreaCode = ds.Tables[0].Rows[0]["AreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Type"] != null)
                {
                    this.Type = ds.Tables[0].Rows[0]["Type"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InoculationPointCode"] != null)
                {
                    this.InoculationPointCode = ds.Tables[0].Rows[0]["InoculationPointCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ContactTel"] != null)
                {
                    this.ContactTel = ds.Tables[0].Rows[0]["ContactTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Coordinate"] != null)
                {
                    this.Coordinate = ds.Tables[0].Rows[0]["Coordinate"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PinYinCode"] != null)
                {
                    this.PinYinCode = ds.Tables[0].Rows[0]["PinYinCode"].ToString();
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
                if (ds.Tables[0].Rows[0]["AreaName"] != null)
                {
                    this.AreaName = ds.Tables[0].Rows[0]["AreaName"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_Child_InoculationPoint]");
            strSql.Append(" where Commid=@Commid ");

            SqlParameter[] parameters = {
					new SqlParameter("@Commid", SqlDbType.Int,4)};
            parameters[0].Value = Commid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsName(string name, int commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_Child_InoculationPoint]");
            strSql.AppendFormat(" where Name='{0}' ", name);
            if (commid != 0)
            {
                strSql.AppendFormat(" and CommID!={0} ", commid);
            }

            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsCode(string code, int CommId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_Child_InoculationPoint]");
            strSql.AppendFormat(" where InoculationPointCode='{0}' ", code);
            if (CommId != 0)
            {
                strSql.AppendFormat(" and CommID!={0} ", CommId);
            }

            return DbHelperSQL.Exists(strSql.ToString());
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [NHS_Child_InoculationPoint] (");
            strSql.Append("Name,AreaCode,Type,InoculationPointCode,ContactTel,Coordinate,PinYinCode,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser,AreaName)");
            strSql.Append(" values (");
            strSql.Append("@Name,@AreaCode,@Type,@InoculationPointCode,@ContactTel,@Coordinate,@PinYinCode,@CreateDate,@CreateUser,@LastUpdateDate,@LastUpdateUser,@AreaName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaCode", SqlDbType.VarChar,12),
					new SqlParameter("@Type", SqlDbType.VarChar,20),
					new SqlParameter("@InoculationPointCode", SqlDbType.VarChar,11),
					new SqlParameter("@ContactTel", SqlDbType.VarChar,13),
					new SqlParameter("@Coordinate", SqlDbType.VarChar,23),
					new SqlParameter("@PinYinCode", SqlDbType.VarChar,20),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,50)};
            parameters[0].Value = Name;
            parameters[1].Value = AreaCode;
            parameters[2].Value = Type;
            parameters[3].Value = InoculationPointCode;
            parameters[4].Value = ContactTel;
            parameters[5].Value = Coordinate;
            parameters[6].Value = PinYinCode;
            parameters[7].Value = CreateDate;
            parameters[8].Value = CreateUser;
            parameters[9].Value = LastUpdateDate;
            parameters[10].Value = LastUpdateUser;
            parameters[11].Value = AreaName;

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
            strSql.Append("update [NHS_Child_InoculationPoint] set ");
            strSql.Append("Name=@Name,");
            strSql.Append("AreaCode=@AreaCode,");
            strSql.Append("Type=@Type,");
            strSql.Append("InoculationPointCode=@InoculationPointCode,");
            strSql.Append("ContactTel=@ContactTel,");
            strSql.Append("Coordinate=@Coordinate,");
            strSql.Append("PinYinCode=@PinYinCode,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("LastUpdateDate=@LastUpdateDate,");
            strSql.Append("LastUpdateUser=@LastUpdateUser,");
            strSql.Append("AreaName=@AreaName");
            strSql.Append(" where Commid=@Commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaCode", SqlDbType.VarChar,12),
					new SqlParameter("@Type", SqlDbType.VarChar,20),
					new SqlParameter("@InoculationPointCode", SqlDbType.VarChar,11),
					new SqlParameter("@ContactTel", SqlDbType.VarChar,13),
					new SqlParameter("@Coordinate", SqlDbType.VarChar,23),
					new SqlParameter("@PinYinCode", SqlDbType.VarChar,20),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@Commid", SqlDbType.Int,4)};
            parameters[0].Value = Name;
            parameters[1].Value = AreaCode;
            parameters[2].Value = Type;
            parameters[3].Value = InoculationPointCode;
            parameters[4].Value = ContactTel;
            parameters[5].Value = Coordinate;
            parameters[6].Value = PinYinCode;
            parameters[7].Value = CreateDate;
            parameters[8].Value = CreateUser;
            parameters[9].Value = LastUpdateDate;
            parameters[10].Value = LastUpdateUser;
            parameters[11].Value = AreaName;
            parameters[12].Value = Commid;

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
        public bool Delete(int Commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [NHS_Child_InoculationPoint] ");
            strSql.Append(" where Commid=@Commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Commid", SqlDbType.Int,4)};
            parameters[0].Value = Commid;

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
        public void GetModel(int Commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Commid,Name,AreaCode,Type,InoculationPointCode,ContactTel,Coordinate,PinYinCode,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser,AreaName ");
            strSql.Append(" FROM [NHS_Child_InoculationPoint] ");
            strSql.Append(" where Commid=@Commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Commid", SqlDbType.Int,4)};
            parameters[0].Value = Commid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Commid"] != null && ds.Tables[0].Rows[0]["Commid"].ToString() != "")
                {
                    this.Commid = int.Parse(ds.Tables[0].Rows[0]["Commid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Name"] != null)
                {
                    this.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AreaCode"] != null)
                {
                    this.AreaCode = ds.Tables[0].Rows[0]["AreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Type"] != null)
                {
                    this.Type = ds.Tables[0].Rows[0]["Type"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InoculationPointCode"] != null)
                {
                    this.InoculationPointCode = ds.Tables[0].Rows[0]["InoculationPointCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ContactTel"] != null)
                {
                    this.ContactTel = ds.Tables[0].Rows[0]["ContactTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Coordinate"] != null)
                {
                    this.Coordinate = ds.Tables[0].Rows[0]["Coordinate"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PinYinCode"] != null)
                {
                    this.PinYinCode = ds.Tables[0].Rows[0]["PinYinCode"].ToString();
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
                if (ds.Tables[0].Rows[0]["AreaName"] != null)
                {
                    this.AreaName = ds.Tables[0].Rows[0]["AreaName"].ToString();
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
            strSql.Append(" FROM [NHS_Child_InoculationPoint] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
