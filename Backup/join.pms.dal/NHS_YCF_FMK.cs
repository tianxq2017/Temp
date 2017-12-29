using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using UNV.Comm.DataBase;
using System.Data.SqlClient;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_YCF_FMK。
    /// </summary>
    [Serializable]
    public partial class NHS_YCF_FMK
    {
        public NHS_YCF_FMK()
        { }
        #region Model
        private int _commid;
        private string _unvid;
        private string _qcfwzbm;
        private DateTime? _mociyj;
        private DateTime? _yuchanqi;
        private int? _taici = 0;
        private string _sholijg;
        private DateTime? _sholidate = DateTime.Now;
        private string _sholiyj;
        private DateTime? _createdate = DateTime.Now;
        private int? _userid;
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
        /// 未次月经
        /// </summary>
        public DateTime? MoCiYj
        {
            set { _mociyj = value; }
            get { return _mociyj; }
        }
        /// <summary>
        /// 预产期
        /// </summary>
        public DateTime? YuChanQi
        {
            set { _yuchanqi = value; }
            get { return _yuchanqi; }
        }
        /// <summary>
        /// 孕次
        /// </summary>
        public int? TaiCi
        {
            set { _taici = value; }
            get { return _taici; }
        }
        /// <summary>
        /// 查检机构
        /// </summary>
        public string sholiJG
        {
            set { _sholijg = value; }
            get { return _sholijg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? sholiDate
        {
            set { _sholidate = value; }
            get { return _sholidate; }
        }
        /// <summary>
        /// 意见
        /// </summary>
        public string sholiYJ
        {
            set { _sholiyj = value; }
            get { return _sholiyj; }
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
        public NHS_YCF_FMK(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommID,UnvID,QcfwzBm,MoCiYj,YuChanQi,TaiCi,sholiJG,sholiDate,sholiYJ,CreateDate,UserID ");
            strSql.Append(" FROM [NHS_YCF_FMK] ");
            strSql.Append(" where CommID=@CommID ");
            SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = CommID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CommID"] != null && ds.Tables[0].Rows[0]["CommID"].ToString() != "")
                {
                    this.CommID = int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UnvID"] != null)
                {
                    this.UnvID = ds.Tables[0].Rows[0]["UnvID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QcfwzBm"] != null)
                {
                    this.QcfwzBm = ds.Tables[0].Rows[0]["QcfwzBm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MoCiYj"] != null && ds.Tables[0].Rows[0]["MoCiYj"].ToString() != "")
                {
                    this.MoCiYj = DateTime.Parse(ds.Tables[0].Rows[0]["MoCiYj"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YuChanQi"] != null && ds.Tables[0].Rows[0]["YuChanQi"].ToString() != "")
                {
                    this.YuChanQi = DateTime.Parse(ds.Tables[0].Rows[0]["YuChanQi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TaiCi"] != null && ds.Tables[0].Rows[0]["TaiCi"].ToString() != "")
                {
                    this.TaiCi = int.Parse(ds.Tables[0].Rows[0]["TaiCi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sholiJG"] != null)
                {
                    this.sholiJG = ds.Tables[0].Rows[0]["sholiJG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sholiDate"] != null && ds.Tables[0].Rows[0]["sholiDate"].ToString() != "")
                {
                    this.sholiDate = DateTime.Parse(ds.Tables[0].Rows[0]["sholiDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sholiYJ"] != null)
                {
                    this.sholiYJ = ds.Tables[0].Rows[0]["sholiYJ"].ToString();
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
        public bool Exists(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_YCF_FMK]");
            strSql.Append(" where CommID=@CommID ");

            SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = CommID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string UnvID, bool isEdit, int commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_YCF_FMK]");
            strSql.AppendFormat(" where UnvID='{0}' ", UnvID);
            if (isEdit)
            {
                strSql.AppendFormat(" and CommID!={0} ", commid);
            }

            return DbHelperSQL.Exists(strSql.ToString());
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [NHS_YCF_FMK] (");
            strSql.Append("UnvID,QcfwzBm,MoCiYj,YuChanQi,TaiCi,sholiJG,sholiDate,sholiYJ,CreateDate,UserID)");
            strSql.Append(" values (");
            strSql.Append("@UnvID,@QcfwzBm,@MoCiYj,@YuChanQi,@TaiCi,@sholiJG,@sholiDate,@sholiYJ,@CreateDate,@UserID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@QcfwzBm", SqlDbType.VarChar,50),
					new SqlParameter("@MoCiYj", SqlDbType.SmallDateTime),
					new SqlParameter("@YuChanQi", SqlDbType.SmallDateTime),
					new SqlParameter("@TaiCi", SqlDbType.TinyInt,1),
					new SqlParameter("@sholiJG", SqlDbType.VarChar,50),
					new SqlParameter("@sholiDate", SqlDbType.SmallDateTime),
					new SqlParameter("@sholiYJ", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = UnvID;
            parameters[1].Value = QcfwzBm;
            parameters[2].Value = MoCiYj;
            parameters[3].Value = YuChanQi;
            parameters[4].Value = TaiCi;
            parameters[5].Value = sholiJG;
            parameters[6].Value = sholiDate;
            parameters[7].Value = sholiYJ;
            parameters[8].Value = CreateDate;
            parameters[9].Value = UserID;

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
            strSql.Append("update [NHS_YCF_FMK] set ");
            strSql.Append("UnvID=@UnvID,");
            strSql.Append("QcfwzBm=@QcfwzBm,");
            strSql.Append("MoCiYj=@MoCiYj,");
            strSql.Append("YuChanQi=@YuChanQi,");
            strSql.Append("TaiCi=@TaiCi,");
            strSql.Append("sholiJG=@sholiJG,");
            strSql.Append("sholiDate=@sholiDate,");
            strSql.Append("sholiYJ=@sholiYJ,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UserID=@UserID");
            strSql.Append(" where CommID=@CommID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@QcfwzBm", SqlDbType.VarChar,50),
					new SqlParameter("@MoCiYj", SqlDbType.SmallDateTime),
					new SqlParameter("@YuChanQi", SqlDbType.SmallDateTime),
					new SqlParameter("@TaiCi", SqlDbType.TinyInt,1),
					new SqlParameter("@sholiJG", SqlDbType.VarChar,50),
					new SqlParameter("@sholiDate", SqlDbType.SmallDateTime),
					new SqlParameter("@sholiYJ", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = UnvID;
            parameters[1].Value = QcfwzBm;
            parameters[2].Value = MoCiYj;
            parameters[3].Value = YuChanQi;
            parameters[4].Value = TaiCi;
            parameters[5].Value = sholiJG;
            parameters[6].Value = sholiDate;
            parameters[7].Value = sholiYJ;
            parameters[8].Value = CreateDate;
            parameters[9].Value = UserID;
            parameters[10].Value = CommID;

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
            strSql.Append("delete from [NHS_YCF_FMK] ");
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
            strSql.Append("select CommID,UnvID,QcfwzBm,MoCiYj,YuChanQi,TaiCi,sholiJG,sholiDate,sholiYJ,CreateDate,UserID ");
            strSql.Append(" FROM [NHS_YCF_FMK] ");
            strSql.Append(" where CommID=@CommID ");
            SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = CommID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CommID"] != null && ds.Tables[0].Rows[0]["CommID"].ToString() != "")
                {
                    this.CommID = int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UnvID"] != null)
                {
                    this.UnvID = ds.Tables[0].Rows[0]["UnvID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QcfwzBm"] != null)
                {
                    this.QcfwzBm = ds.Tables[0].Rows[0]["QcfwzBm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MoCiYj"] != null && ds.Tables[0].Rows[0]["MoCiYj"].ToString() != "")
                {
                    this.MoCiYj = DateTime.Parse(ds.Tables[0].Rows[0]["MoCiYj"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YuChanQi"] != null && ds.Tables[0].Rows[0]["YuChanQi"].ToString() != "")
                {
                    this.YuChanQi = DateTime.Parse(ds.Tables[0].Rows[0]["YuChanQi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TaiCi"] != null && ds.Tables[0].Rows[0]["TaiCi"].ToString() != "")
                {
                    this.TaiCi = int.Parse(ds.Tables[0].Rows[0]["TaiCi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sholiJG"] != null)
                {
                    this.sholiJG = ds.Tables[0].Rows[0]["sholiJG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sholiDate"] != null && ds.Tables[0].Rows[0]["sholiDate"].ToString() != "")
                {
                    this.sholiDate = DateTime.Parse(ds.Tables[0].Rows[0]["sholiDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sholiYJ"] != null)
                {
                    this.sholiYJ = ds.Tables[0].Rows[0]["sholiYJ"].ToString();
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
            strSql.Append(" FROM [NHS_YCF_FMK] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
