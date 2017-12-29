using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using UNV.Comm.DataBase;
using System.Data.SqlClient;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_WorkUser。
    /// </summary>
    [Serializable]
    public partial class NHS_WorkUser
    {
        public NHS_WorkUser()
        { }
        #region Model
        private int _commid;
        private int? _userid;
        private string _username;
        private int? _inoculationpointid;
        private string _inoculationpointname;
        private int? _id;
        private DateTime? _createdate;
        private int? _createuser;
        private DateTime? _lastupdatedate;
        private int? _lastupdateuser;
        private string _areacode;
        /// <summary>
        /// 
        /// </summary>
        public int Commid
        {
            set { _commid = value; }
            get { return _commid; }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int? UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 工作人员
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 接种点编号
        /// </summary>
        public int? InoculationPointID
        {
            set { _inoculationpointid = value; }
            get { return _inoculationpointid; }
        }
        /// <summary>
        /// 接种点名称
        /// </summary>
        public string InoculationPointName
        {
            set { _inoculationpointname = value; }
            get { return _inoculationpointname; }
        }
        /// <summary>
        /// 村庄编号
        /// </summary>
        public int? ID
        {
            set { _id = value; }
            get { return _id; }
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
        /// <summary>
        /// 
        /// </summary>
        public string AreaCode
        {
            set { _areacode = value; }
            get { return _areacode; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NHS_WorkUser(int Commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Commid,UserID,UserName,InoculationPointID,InoculationPointName,ID,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser,AreaCode ");
            strSql.Append(" FROM [NHS_WorkUser] ");
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
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InoculationPointID"] != null && ds.Tables[0].Rows[0]["InoculationPointID"].ToString() != "")
                {
                    this.InoculationPointID = int.Parse(ds.Tables[0].Rows[0]["InoculationPointID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InoculationPointName"] != null)
                {
                    this.InoculationPointName = ds.Tables[0].Rows[0]["InoculationPointName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
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
                if (ds.Tables[0].Rows[0]["AreaCode"] != null)
                {
                    this.AreaCode = ds.Tables[0].Rows[0]["AreaCode"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_WorkUser]");
            strSql.Append(" where Commid=@Commid ");

            SqlParameter[] parameters = {
					new SqlParameter("@Commid", SqlDbType.Int,4)};
            parameters[0].Value = Commid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [NHS_WorkUser] (");
            strSql.Append("UserID,UserName,InoculationPointID,InoculationPointName,ID,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser,AreaCode)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@UserName,@InoculationPointID,@InoculationPointName,@ID,@CreateDate,@CreateUser,@LastUpdateDate,@LastUpdateUser,@AreaCode)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,20),
					new SqlParameter("@InoculationPointID", SqlDbType.Int,4),
					new SqlParameter("@InoculationPointName", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4),
					new SqlParameter("@AreaCode", SqlDbType.VarChar,12)};
            parameters[0].Value = UserID;
            parameters[1].Value = UserName;
            parameters[2].Value = InoculationPointID;
            parameters[3].Value = InoculationPointName;
            parameters[4].Value = ID;
            parameters[5].Value = CreateDate;
            parameters[6].Value = CreateUser;
            parameters[7].Value = LastUpdateDate;
            parameters[8].Value = LastUpdateUser;
            parameters[9].Value = AreaCode;

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
            strSql.Append("update [NHS_WorkUser] set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("InoculationPointID=@InoculationPointID,");
            strSql.Append("InoculationPointName=@InoculationPointName,");
            strSql.Append("ID=@ID,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("LastUpdateDate=@LastUpdateDate,");
            strSql.Append("LastUpdateUser=@LastUpdateUser,");
            strSql.Append("AreaCode=@AreaCode");
            strSql.Append(" where Commid=@Commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,20),
					new SqlParameter("@InoculationPointID", SqlDbType.Int,4),
					new SqlParameter("@InoculationPointName", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4),
					new SqlParameter("@AreaCode", SqlDbType.VarChar,12),
					new SqlParameter("@Commid", SqlDbType.Int,4)};
            parameters[0].Value = UserID;
            parameters[1].Value = UserName;
            parameters[2].Value = InoculationPointID;
            parameters[3].Value = InoculationPointName;
            parameters[4].Value = ID;
            parameters[5].Value = CreateDate;
            parameters[6].Value = CreateUser;
            parameters[7].Value = LastUpdateDate;
            parameters[8].Value = LastUpdateUser;
            parameters[9].Value = AreaCode;
            parameters[10].Value = Commid;

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
            strSql.Append("delete from [NHS_WorkUser] ");
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
            strSql.Append("select Commid,UserID,UserName,InoculationPointID,InoculationPointName,ID,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser,AreaCode ");
            strSql.Append(" FROM [NHS_WorkUser] ");
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
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null)
                {
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InoculationPointID"] != null && ds.Tables[0].Rows[0]["InoculationPointID"].ToString() != "")
                {
                    this.InoculationPointID = int.Parse(ds.Tables[0].Rows[0]["InoculationPointID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InoculationPointName"] != null)
                {
                    this.InoculationPointName = ds.Tables[0].Rows[0]["InoculationPointName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
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
                if (ds.Tables[0].Rows[0]["AreaCode"] != null)
                {
                    this.AreaCode = ds.Tables[0].Rows[0]["AreaCode"].ToString();
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
            strSql.Append(" FROM [NHS_WorkUser] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
