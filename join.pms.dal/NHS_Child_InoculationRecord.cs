using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using UNV.Comm.DataBase;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_Child_InoculationRecord。
    /// </summary>
    [Serializable]
    public partial class NHS_Child_InoculationRecord
    {
        public NHS_Child_InoculationRecord()
        { }
        #region Model
        private int _commid;
        private string _childid;
        private string _childname;
        private string _fathername;
        private string _mothername;
        private string _zhuzhitype;
        private string _contacttel;
        private string _address;
        private string _addresscode;
        private int? _yimiaoid;
        private string _yimiaoname;
        private string _inoculationpart;
        private string _pihao;
        private string _company;
        private DateTime? _inoculationdate;
        private int? _inputway;
        private int? _inoculationdoctor;
        private int? _inoculationpointid;
        private string _inoculationpointname;
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
        /// 儿童ID卡号
        /// </summary>
        public string ChildID
        {
            set { _childid = value; }
            get { return _childid; }
        }
        /// <summary>
        /// 儿童姓名
        /// </summary>
        public string ChildName
        {
            set { _childname = value; }
            get { return _childname; }
        }
        /// <summary>
        /// 父亲姓名
        /// </summary>
        public string FatherName
        {
            set { _fathername = value; }
            get { return _fathername; }
        }
        /// <summary>
        /// 母亲姓名
        /// </summary>
        public string MotherName
        {
            set { _mothername = value; }
            get { return _mothername; }
        }
        /// <summary>
        /// 住址类型（本地、本市他县、外地）
        /// </summary>
        public string ZhuzhiType
        {
            set { _zhuzhitype = value; }
            get { return _zhuzhitype; }
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
        /// 家庭住址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 住址区划
        /// </summary>
        public string AddressCode
        {
            set { _addresscode = value; }
            get { return _addresscode; }
        }
        /// <summary>
        /// 疫苗编号
        /// </summary>
        public int? YiMiaoID
        {
            set { _yimiaoid = value; }
            get { return _yimiaoid; }
        }
        /// <summary>
        /// 疫苗名称
        /// </summary>
        public string YiMiaoName
        {
            set { _yimiaoname = value; }
            get { return _yimiaoname; }
        }
        /// <summary>
        /// 接种部位
        ///   左上臂
        ///   右上臂
        /// </summary>
        public string InoculationPart
        {
            set { _inoculationpart = value; }
            get { return _inoculationpart; }
        }
        /// <summary>
        /// 批号
        /// </summary>
        public string PiHao
        {
            set { _pihao = value; }
            get { return _pihao; }
        }
        /// <summary>
        /// 生产企业
        /// </summary>
        public string Company
        {
            set { _company = value; }
            get { return _company; }
        }
        /// <summary>
        /// 接种日期
        /// </summary>
        public DateTime? InoculationDate
        {
            set { _inoculationdate = value; }
            get { return _inoculationdate; }
        }
        /// <summary>
        /// 录入方式(0正常录入、1补录、2补录)
        /// </summary>
        public int? InputWay
        {
            set { _inputway = value; }
            get { return _inputway; }
        }
        /// <summary>
        /// 接种医生
        /// </summary>
        public int? InoculationDoctor
        {
            set { _inoculationdoctor = value; }
            get { return _inoculationdoctor; }
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
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NHS_Child_InoculationRecord(int Commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Commid,ChildID,ChildName,FatherName,MotherName,ZhuzhiType,ContactTel,Address,AddressCode,YiMiaoID,YiMiaoName,InoculationPart,PiHao,Company,InoculationDate,InputWay,InoculationDoctor,InoculationPointID,InoculationPointName,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser ");
            strSql.Append(" FROM [NHS_Child_InoculationRecord] ");
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
                if (ds.Tables[0].Rows[0]["ChildID"] != null)
                {
                    this.ChildID = ds.Tables[0].Rows[0]["ChildID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChildName"] != null)
                {
                    this.ChildName = ds.Tables[0].Rows[0]["ChildName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FatherName"] != null)
                {
                    this.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherName"] != null)
                {
                    this.MotherName = ds.Tables[0].Rows[0]["MotherName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhuzhiType"] != null)
                {
                    this.ZhuzhiType = ds.Tables[0].Rows[0]["ZhuzhiType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ContactTel"] != null)
                {
                    this.ContactTel = ds.Tables[0].Rows[0]["ContactTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Address"] != null)
                {
                    this.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AddressCode"] != null)
                {
                    this.AddressCode = ds.Tables[0].Rows[0]["AddressCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YiMiaoID"] != null && ds.Tables[0].Rows[0]["YiMiaoID"].ToString() != "")
                {
                    this.YiMiaoID = int.Parse(ds.Tables[0].Rows[0]["YiMiaoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YiMiaoName"] != null)
                {
                    this.YiMiaoName = ds.Tables[0].Rows[0]["YiMiaoName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InoculationPart"] != null)
                {
                    this.InoculationPart = ds.Tables[0].Rows[0]["InoculationPart"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PiHao"] != null)
                {
                    this.PiHao = ds.Tables[0].Rows[0]["PiHao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Company"] != null)
                {
                    this.Company = ds.Tables[0].Rows[0]["Company"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InoculationDate"] != null && ds.Tables[0].Rows[0]["InoculationDate"].ToString() != "")
                {
                    this.InoculationDate = DateTime.Parse(ds.Tables[0].Rows[0]["InoculationDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InputWay"] != null && ds.Tables[0].Rows[0]["InputWay"].ToString() != "")
                {
                    this.InputWay = int.Parse(ds.Tables[0].Rows[0]["InputWay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InoculationDoctor"] != null && ds.Tables[0].Rows[0]["InoculationDoctor"].ToString() != "")
                {
                    this.InoculationDoctor = int.Parse(ds.Tables[0].Rows[0]["InoculationDoctor"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InoculationPointID"] != null && ds.Tables[0].Rows[0]["InoculationPointID"].ToString() != "")
                {
                    this.InoculationPointID = int.Parse(ds.Tables[0].Rows[0]["InoculationPointID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InoculationPointName"] != null)
                {
                    this.InoculationPointName = ds.Tables[0].Rows[0]["InoculationPointName"].ToString();
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
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_Child_InoculationRecord]");
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
            strSql.Append("insert into [NHS_Child_InoculationRecord] (");
            strSql.Append("ChildID,ChildName,FatherName,MotherName,ZhuzhiType,ContactTel,Address,AddressCode,YiMiaoID,YiMiaoName,InoculationPart,PiHao,Company,InoculationDate,InputWay,InoculationDoctor,InoculationPointID,InoculationPointName,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser)");
            strSql.Append(" values (");
            strSql.Append("@ChildID,@ChildName,@FatherName,@MotherName,@ZhuzhiType,@ContactTel,@Address,@AddressCode,@YiMiaoID,@YiMiaoName,@InoculationPart,@PiHao,@Company,@InoculationDate,@InputWay,@InoculationDoctor,@InoculationPointID,@InoculationPointName,@CreateDate,@CreateUser,@LastUpdateDate,@LastUpdateUser)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ChildID", SqlDbType.VarChar,20),
					new SqlParameter("@ChildName", SqlDbType.NVarChar,20),
					new SqlParameter("@FatherName", SqlDbType.NVarChar,20),
					new SqlParameter("@MotherName", SqlDbType.NVarChar,20),
					new SqlParameter("@ZhuzhiType", SqlDbType.VarChar,11),
					new SqlParameter("@ContactTel", SqlDbType.VarChar,13),
					new SqlParameter("@Address", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressCode", SqlDbType.VarChar,10),
					new SqlParameter("@YiMiaoID", SqlDbType.Int,4),
					new SqlParameter("@YiMiaoName", SqlDbType.NVarChar,20),
					new SqlParameter("@InoculationPart", SqlDbType.NVarChar,8),
					new SqlParameter("@PiHao", SqlDbType.VarChar,20),
					new SqlParameter("@Company", SqlDbType.NVarChar,20),
					new SqlParameter("@InoculationDate", SqlDbType.DateTime),
					new SqlParameter("@InputWay", SqlDbType.TinyInt,1),
					new SqlParameter("@InoculationDoctor", SqlDbType.Int,4),
					new SqlParameter("@InoculationPointID", SqlDbType.Int,4),
					new SqlParameter("@InoculationPointName", SqlDbType.NVarChar,25),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4)};
            parameters[0].Value = ChildID;
            parameters[1].Value = ChildName;
            parameters[2].Value = FatherName;
            parameters[3].Value = MotherName;
            parameters[4].Value = ZhuzhiType;
            parameters[5].Value = ContactTel;
            parameters[6].Value = Address;
            parameters[7].Value = AddressCode;
            parameters[8].Value = YiMiaoID;
            parameters[9].Value = YiMiaoName;
            parameters[10].Value = InoculationPart;
            parameters[11].Value = PiHao;
            parameters[12].Value = Company;
            parameters[13].Value = InoculationDate;
            parameters[14].Value = InputWay;
            parameters[15].Value = InoculationDoctor;
            parameters[16].Value = InoculationPointID;
            parameters[17].Value = InoculationPointName;
            parameters[18].Value = CreateDate;
            parameters[19].Value = CreateUser;
            parameters[20].Value = LastUpdateDate;
            parameters[21].Value = LastUpdateUser;

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
            strSql.Append("update [NHS_Child_InoculationRecord] set ");
            strSql.Append("ChildID=@ChildID,");
            strSql.Append("ChildName=@ChildName,");
            strSql.Append("FatherName=@FatherName,");
            strSql.Append("MotherName=@MotherName,");
            strSql.Append("ZhuzhiType=@ZhuzhiType,");
            strSql.Append("ContactTel=@ContactTel,");
            strSql.Append("Address=@Address,");
            strSql.Append("AddressCode=@AddressCode,");
            strSql.Append("YiMiaoID=@YiMiaoID,");
            strSql.Append("YiMiaoName=@YiMiaoName,");
            strSql.Append("InoculationPart=@InoculationPart,");
            strSql.Append("PiHao=@PiHao,");
            strSql.Append("Company=@Company,");
            strSql.Append("InoculationDate=@InoculationDate,");
            strSql.Append("InputWay=@InputWay,");
            strSql.Append("InoculationDoctor=@InoculationDoctor,");
            strSql.Append("InoculationPointID=@InoculationPointID,");
            strSql.Append("InoculationPointName=@InoculationPointName,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("LastUpdateDate=@LastUpdateDate,");
            strSql.Append("LastUpdateUser=@LastUpdateUser");
            strSql.Append(" where Commid=@Commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@ChildID", SqlDbType.VarChar,20),
					new SqlParameter("@ChildName", SqlDbType.NVarChar,20),
					new SqlParameter("@FatherName", SqlDbType.NVarChar,20),
					new SqlParameter("@MotherName", SqlDbType.NVarChar,20),
					new SqlParameter("@ZhuzhiType", SqlDbType.VarChar,11),
					new SqlParameter("@ContactTel", SqlDbType.VarChar,13),
					new SqlParameter("@Address", SqlDbType.NVarChar,50),
					new SqlParameter("@AddressCode", SqlDbType.VarChar,10),
					new SqlParameter("@YiMiaoID", SqlDbType.Int,4),
					new SqlParameter("@YiMiaoName", SqlDbType.NVarChar,20),
					new SqlParameter("@InoculationPart", SqlDbType.NVarChar,8),
					new SqlParameter("@PiHao", SqlDbType.VarChar,20),
					new SqlParameter("@Company", SqlDbType.NVarChar,20),
					new SqlParameter("@InoculationDate", SqlDbType.DateTime),
					new SqlParameter("@InputWay", SqlDbType.TinyInt,1),
					new SqlParameter("@InoculationDoctor", SqlDbType.Int,4),
					new SqlParameter("@InoculationPointID", SqlDbType.Int,4),
					new SqlParameter("@InoculationPointName", SqlDbType.NVarChar,25),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.Int,4),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4),
					new SqlParameter("@Commid", SqlDbType.Int,4)};
            parameters[0].Value = ChildID;
            parameters[1].Value = ChildName;
            parameters[2].Value = FatherName;
            parameters[3].Value = MotherName;
            parameters[4].Value = ZhuzhiType;
            parameters[5].Value = ContactTel;
            parameters[6].Value = Address;
            parameters[7].Value = AddressCode;
            parameters[8].Value = YiMiaoID;
            parameters[9].Value = YiMiaoName;
            parameters[10].Value = InoculationPart;
            parameters[11].Value = PiHao;
            parameters[12].Value = Company;
            parameters[13].Value = InoculationDate;
            parameters[14].Value = InputWay;
            parameters[15].Value = InoculationDoctor;
            parameters[16].Value = InoculationPointID;
            parameters[17].Value = InoculationPointName;
            parameters[18].Value = CreateDate;
            parameters[19].Value = CreateUser;
            parameters[20].Value = LastUpdateDate;
            parameters[21].Value = LastUpdateUser;
            parameters[22].Value = Commid;

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
            strSql.Append("delete from [NHS_Child_InoculationRecord] ");
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
            strSql.Append("select Commid,ChildID,ChildName,FatherName,MotherName,ZhuzhiType,ContactTel,Address,AddressCode,YiMiaoID,YiMiaoName,InoculationPart,PiHao,Company,InoculationDate,InputWay,InoculationDoctor,InoculationPointID,InoculationPointName,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser ");
            strSql.Append(" FROM [NHS_Child_InoculationRecord] ");
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
                if (ds.Tables[0].Rows[0]["ChildID"] != null)
                {
                    this.ChildID = ds.Tables[0].Rows[0]["ChildID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChildName"] != null)
                {
                    this.ChildName = ds.Tables[0].Rows[0]["ChildName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FatherName"] != null)
                {
                    this.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherName"] != null)
                {
                    this.MotherName = ds.Tables[0].Rows[0]["MotherName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhuzhiType"] != null)
                {
                    this.ZhuzhiType = ds.Tables[0].Rows[0]["ZhuzhiType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ContactTel"] != null)
                {
                    this.ContactTel = ds.Tables[0].Rows[0]["ContactTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Address"] != null)
                {
                    this.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AddressCode"] != null)
                {
                    this.AddressCode = ds.Tables[0].Rows[0]["AddressCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YiMiaoID"] != null && ds.Tables[0].Rows[0]["YiMiaoID"].ToString() != "")
                {
                    this.YiMiaoID = int.Parse(ds.Tables[0].Rows[0]["YiMiaoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YiMiaoName"] != null)
                {
                    this.YiMiaoName = ds.Tables[0].Rows[0]["YiMiaoName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InoculationPart"] != null)
                {
                    this.InoculationPart = ds.Tables[0].Rows[0]["InoculationPart"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PiHao"] != null)
                {
                    this.PiHao = ds.Tables[0].Rows[0]["PiHao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Company"] != null)
                {
                    this.Company = ds.Tables[0].Rows[0]["Company"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InoculationDate"] != null && ds.Tables[0].Rows[0]["InoculationDate"].ToString() != "")
                {
                    this.InoculationDate = DateTime.Parse(ds.Tables[0].Rows[0]["InoculationDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InputWay"] != null && ds.Tables[0].Rows[0]["InputWay"].ToString() != "")
                {
                    this.InputWay = int.Parse(ds.Tables[0].Rows[0]["InputWay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InoculationDoctor"] != null && ds.Tables[0].Rows[0]["InoculationDoctor"].ToString() != "")
                {
                    this.InoculationDoctor = int.Parse(ds.Tables[0].Rows[0]["InoculationDoctor"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InoculationPointID"] != null && ds.Tables[0].Rows[0]["InoculationPointID"].ToString() != "")
                {
                    this.InoculationPointID = int.Parse(ds.Tables[0].Rows[0]["InoculationPointID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InoculationPointName"] != null)
                {
                    this.InoculationPointName = ds.Tables[0].Rows[0]["InoculationPointName"].ToString();
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
            strSql.Append(" FROM [NHS_Child_InoculationRecord] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
