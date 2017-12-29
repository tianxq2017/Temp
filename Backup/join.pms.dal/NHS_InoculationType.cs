using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using UNV.Comm.DataBase;

namespace join.pms.dal
{
    /// <summary>
    /// ��NHS_InoculationType��
    /// </summary>
    [Serializable]
    public partial class NHS_InoculationType
    {
        public NHS_InoculationType()
        { }
        #region Model
        private int _commid;
        private int? _type;
        private string _tpyevalue;
        private string _inoculationpointid;
        private DateTime? _createdate;
        private int? _createuser;
        private DateTime? _lastupdatedate;
        private int? _lastupdateuser;
        /// <summary>
        /// 
        /// </summary>
        public int Commid
        {
            set { _commid = value; }
            get { return _commid; }
        }
        /// <summary>
        /// �������ͣ�1�¡�2Ѯ��3�ܡ�4�գ�
        /// </summary>
        public int? Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// �£�1-30��
        ///   Ѯ��1-10��
        ///   �ܣ���һ-���죩
        ///   ��
        /// </summary>
        public string TpyeValue
        {
            set { _tpyevalue = value; }
            get { return _tpyevalue; }
        }
        /// <summary>
        /// ���ֵ���
        /// </summary>
        public string InoculationPointID
        {
            set { _inoculationpointid = value; }
            get { return _inoculationpointid; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public int? CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// ����޸�����
        /// </summary>
        public DateTime? LastUpdateDate
        {
            set { _lastupdatedate = value; }
            get { return _lastupdatedate; }
        }
        /// <summary>
        /// ����޸���
        /// </summary>
        public int? LastUpdateUser
        {
            set { _lastupdateuser = value; }
            get { return _lastupdateuser; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public NHS_InoculationType(int Commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Commid,Type,TpyeValue,InoculationPointID,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser ");
            strSql.Append(" FROM [NHS_InoculationType] ");
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
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    this.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TpyeValue"] != null)
                {
                    this.TpyeValue = ds.Tables[0].Rows[0]["TpyeValue"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InoculationPointID"] != null)
                {
                    this.InoculationPointID = ds.Tables[0].Rows[0]["InoculationPointID"].ToString();
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
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int Commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_InoculationType]");
            strSql.Append(" where Commid=@Commid ");

            SqlParameter[] parameters = {
					new SqlParameter("@Commid", SqlDbType.Int,4)};
            parameters[0].Value = Commid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [NHS_InoculationType] (");
            strSql.Append("Type,TpyeValue,InoculationPointID,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser)");
            strSql.Append(" values (");
            strSql.Append("@Type,@TpyeValue,@InoculationPointID,@CreateDate,@CreateUser,@LastUpdateDate,@LastUpdateUser)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@TpyeValue", SqlDbType.NVarChar,200),
					new SqlParameter("@InoculationPointID", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4)};
            parameters[0].Value = Type;
            parameters[1].Value = TpyeValue;
            parameters[2].Value = InoculationPointID;
            parameters[3].Value = CreateDate;
            parameters[4].Value = CreateUser;
            parameters[5].Value = LastUpdateDate;
            parameters[6].Value = LastUpdateUser;

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
        /// ����һ������
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [NHS_InoculationType] set ");
            strSql.Append("Type=@Type,");
            strSql.Append("TpyeValue=@TpyeValue,");
            strSql.Append("InoculationPointID=@InoculationPointID,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("LastUpdateDate=@LastUpdateDate,");
            strSql.Append("LastUpdateUser=@LastUpdateUser");
            strSql.Append(" where Commid=@Commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@TpyeValue", SqlDbType.NVarChar,200),
					new SqlParameter("@InoculationPointID", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4),
					new SqlParameter("@Commid", SqlDbType.Int,4)};
            parameters[0].Value = Type;
            parameters[1].Value = TpyeValue;
            parameters[2].Value = InoculationPointID;
            parameters[3].Value = CreateDate;
            parameters[4].Value = CreateUser;
            parameters[5].Value = LastUpdateDate;
            parameters[6].Value = LastUpdateUser;
            parameters[7].Value = Commid;

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
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int Commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [NHS_InoculationType] ");
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
        /// �õ�һ������ʵ��
        /// </summary>
        public void GetModel(int Commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Commid,Type,TpyeValue,InoculationPointID,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser ");
            strSql.Append(" FROM [NHS_InoculationType] ");
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
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    this.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TpyeValue"] != null)
                {
                    this.TpyeValue = ds.Tables[0].Rows[0]["TpyeValue"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InoculationPointID"] != null)
                {
                    this.InoculationPointID = ds.Tables[0].Rows[0]["InoculationPointID"].ToString();
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
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [NHS_InoculationType] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
