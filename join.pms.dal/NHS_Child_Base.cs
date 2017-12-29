using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using UNV.Comm.DataBase;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_Child_Base。
    /// </summary>
    [Serializable]
    public partial class NHS_Child_Base
    {
        public NHS_Child_Base()
        { }
        #region Model
        private string _unvid;
        private string _qcfwzbm;
        private string _childbm;
        private string _childname;
        private string _childsex;
        private DateTime? _birthday;
        private string _birthcerno;
        private string _childcid;
        private string _curareaname;
        private string _curareacode;
        private string _regareaname;
        private string _regareacode;
        private string _fathername;
        private string _fathercid;
        private string _fathertel;
        private string _mothername;
        private string _mothercid;
        private string _mothertel;
        private int? _userid;
        private string _jzunit;
        private DateTime? _createdate = DateTime.Now;
        private DateTime? _nextvisitdate;
        private int? _visitcount;
        private DateTime? _lastvisitdate;
        private string _lastvisititems;
        private string _ycfuserareacode;
        private string _cszh;
        private int? _inoculationpointid;
        private int? _villageid;
        private int? _jzsx;
        private decimal? _birthweight;
        private bool _ishaszsz;
        private int? _birthhospital;
        private string _jinjiyimiao;
        private string _memo;
        /// <summary>
        /// 
        /// </summary>
        public string UnvID
        {
            set { _unvid = value; }
            get { return _unvid; }
        }
        /// <summary>
        /// 全程服务证号
        /// </summary>
        public string QcfwzBm
        {
            set { _qcfwzbm = value; }
            get { return _qcfwzbm; }
        }
        /// <summary>
        /// 接种编号
        /// </summary>
        public string ChildBm
        {
            set { _childbm = value; }
            get { return _childbm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChildName
        {
            set { _childname = value; }
            get { return _childname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChildSex
        {
            set { _childsex = value; }
            get { return _childsex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? BirthDay
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BirthCerNo
        {
            set { _birthcerno = value; }
            get { return _birthcerno; }
        }
        /// <summary>
        /// 儿童身份证号
        /// </summary>
        public string ChildCID
        {
            set { _childcid = value; }
            get { return _childcid; }
        }
        /// <summary>
        /// 家庭地址
        /// </summary>
        public string CurAreaName
        {
            set { _curareaname = value; }
            get { return _curareaname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CurAreaCode
        {
            set { _curareacode = value; }
            get { return _curareacode; }
        }
        /// <summary>
        /// 户籍地址
        /// </summary>
        public string RegAreaName
        {
            set { _regareaname = value; }
            get { return _regareaname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RegAreaCode
        {
            set { _regareacode = value; }
            get { return _regareacode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FatherName
        {
            set { _fathername = value; }
            get { return _fathername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FatherCID
        {
            set { _fathercid = value; }
            get { return _fathercid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FatherTel
        {
            set { _fathertel = value; }
            get { return _fathertel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MotherName
        {
            set { _mothername = value; }
            get { return _mothername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MotherCID
        {
            set { _mothercid = value; }
            get { return _mothercid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MotherTel
        {
            set { _mothertel = value; }
            get { return _mothertel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 接种单位
        /// </summary>
        public string JzUnit
        {
            set { _jzunit = value; }
            get { return _jzunit; }
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
        public DateTime? NextVisitDate
        {
            set { _nextvisitdate = value; }
            get { return _nextvisitdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? VisitCount
        {
            set { _visitcount = value; }
            get { return _visitcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastVisitDate
        {
            set { _lastvisitdate = value; }
            get { return _lastvisitdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LastVisitItems
        {
            set { _lastvisititems = value; }
            get { return _lastvisititems; }
        }
        /// <summary>
        /// 镇级、县级权限
        /// </summary>
        public string YCFUserAreaCode
        {
            set { _ycfuserareacode = value; }
            get { return _ycfuserareacode; }
        }
        /// <summary>
        /// 出生证号
        /// </summary>
        public string CSZH
        {
            set { _cszh = value; }
            get { return _cszh; }
        }
        /// <summary>
        /// 接种地点（接种点）编号
        /// </summary>
        public int? InoculationPointID
        {
            set { _inoculationpointid = value; }
            get { return _inoculationpointid; }
        }
        /// <summary>
        /// 接种地点（村）编号
        /// </summary>
        public int? VillageID
        {
            set { _villageid = value; }
            get { return _villageid; }
        }
        /// <summary>
        /// 居住属性1常住2暂住
        /// </summary>
        public int? JZSX
        {
            set { _jzsx = value; }
            get { return _jzsx; }
        }
        /// <summary>
        /// 出生体重
        /// </summary>
        public decimal? BirthWeight
        {
            set { _birthweight = value; }
            get { return _birthweight; }
        }
        /// <summary>
        /// 是否有准生证
        /// </summary>
        public bool IsHasZSZ
        {
            set { _ishaszsz = value; }
            get { return _ishaszsz; }
        }
        /// <summary>
        /// 出生医院
        /// </summary>
        public int? BirthHospital
        {
            set { _birthhospital = value; }
            get { return _birthhospital; }
        }
        /// <summary>
        /// 禁忌疫苗
        /// </summary>
        public string JinJiYiMiao
        {
            set { _jinjiyimiao = value; }
            get { return _jinjiyimiao; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NHS_Child_Base(string UnvID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UnvID,QcfwzBm,ChildBm,ChildName,ChildSex,BirthDay,BirthCerNo,ChildCID,CurAreaName,CurAreaCode,RegAreaName,RegAreaCode,FatherName,FatherCID,FatherTel,MotherName,MotherCID,MotherTel,UserID,JzUnit,CreateDate,NextVisitDate,VisitCount,LastVisitDate,LastVisitItems,YCFUserAreaCode,CSZH,InoculationPointID,VillageID,JZSX,BirthWeight,IsHasZSZ,BirthHospital,JinJiYiMiao,Memo ");
            strSql.Append(" FROM [NHS_Child_Base] ");
            strSql.Append(" where UnvID=@UnvID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50)};
            parameters[0].Value = UnvID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UnvID"] != null)
                {
                    this.UnvID = ds.Tables[0].Rows[0]["UnvID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QcfwzBm"] != null)
                {
                    this.QcfwzBm = ds.Tables[0].Rows[0]["QcfwzBm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChildBm"] != null)
                {
                    this.ChildBm = ds.Tables[0].Rows[0]["ChildBm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChildName"] != null)
                {
                    this.ChildName = ds.Tables[0].Rows[0]["ChildName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChildSex"] != null)
                {
                    this.ChildSex = ds.Tables[0].Rows[0]["ChildSex"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BirthDay"] != null && ds.Tables[0].Rows[0]["BirthDay"].ToString() != "")
                {
                    this.BirthDay = DateTime.Parse(ds.Tables[0].Rows[0]["BirthDay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BirthCerNo"] != null)
                {
                    this.BirthCerNo = ds.Tables[0].Rows[0]["BirthCerNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChildCID"] != null)
                {
                    this.ChildCID = ds.Tables[0].Rows[0]["ChildCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurAreaName"] != null)
                {
                    this.CurAreaName = ds.Tables[0].Rows[0]["CurAreaName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurAreaCode"] != null)
                {
                    this.CurAreaCode = ds.Tables[0].Rows[0]["CurAreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegAreaName"] != null)
                {
                    this.RegAreaName = ds.Tables[0].Rows[0]["RegAreaName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegAreaCode"] != null)
                {
                    this.RegAreaCode = ds.Tables[0].Rows[0]["RegAreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FatherName"] != null)
                {
                    this.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FatherCID"] != null)
                {
                    this.FatherCID = ds.Tables[0].Rows[0]["FatherCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FatherTel"] != null)
                {
                    this.FatherTel = ds.Tables[0].Rows[0]["FatherTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherName"] != null)
                {
                    this.MotherName = ds.Tables[0].Rows[0]["MotherName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherCID"] != null)
                {
                    this.MotherCID = ds.Tables[0].Rows[0]["MotherCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherTel"] != null)
                {
                    this.MotherTel = ds.Tables[0].Rows[0]["MotherTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JzUnit"] != null)
                {
                    this.JzUnit = ds.Tables[0].Rows[0]["JzUnit"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NextVisitDate"] != null && ds.Tables[0].Rows[0]["NextVisitDate"].ToString() != "")
                {
                    this.NextVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["NextVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VisitCount"] != null && ds.Tables[0].Rows[0]["VisitCount"].ToString() != "")
                {
                    this.VisitCount = int.Parse(ds.Tables[0].Rows[0]["VisitCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastVisitDate"] != null && ds.Tables[0].Rows[0]["LastVisitDate"].ToString() != "")
                {
                    this.LastVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["LastVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastVisitItems"] != null)
                {
                    this.LastVisitItems = ds.Tables[0].Rows[0]["LastVisitItems"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YCFUserAreaCode"] != null)
                {
                    this.YCFUserAreaCode = ds.Tables[0].Rows[0]["YCFUserAreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CSZH"] != null)
                {
                    this.CSZH = ds.Tables[0].Rows[0]["CSZH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InoculationPointID"] != null && ds.Tables[0].Rows[0]["InoculationPointID"].ToString() != "")
                {
                    this.InoculationPointID = int.Parse(ds.Tables[0].Rows[0]["InoculationPointID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VillageID"] != null && ds.Tables[0].Rows[0]["VillageID"].ToString() != "")
                {
                    this.VillageID = int.Parse(ds.Tables[0].Rows[0]["VillageID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JZSX"] != null && ds.Tables[0].Rows[0]["JZSX"].ToString() != "")
                {
                    this.JZSX = int.Parse(ds.Tables[0].Rows[0]["JZSX"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BirthWeight"] != null && ds.Tables[0].Rows[0]["BirthWeight"].ToString() != "")
                {
                    this.BirthWeight = decimal.Parse(ds.Tables[0].Rows[0]["BirthWeight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsHasZSZ"] != null && ds.Tables[0].Rows[0]["IsHasZSZ"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsHasZSZ"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsHasZSZ"].ToString().ToLower() == "true"))
                    {
                        this.IsHasZSZ = true;
                    }
                    else
                    {
                        this.IsHasZSZ = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["BirthHospital"] != null && ds.Tables[0].Rows[0]["BirthHospital"].ToString() != "")
                {
                    this.BirthHospital = int.Parse(ds.Tables[0].Rows[0]["BirthHospital"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JinJiYiMiao"] != null)
                {
                    this.JinJiYiMiao = ds.Tables[0].Rows[0]["JinJiYiMiao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Memo"] != null)
                {
                    this.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                }
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string UnvID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_Child_Base]");
            strSql.Append(" where UnvID=@UnvID ");

            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50)};
            parameters[0].Value = UnvID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string QcfwzBm, bool isEdit, string UnvID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_Child_Base]");
            strSql.AppendFormat(" where QcfwzBm='{0}' ", QcfwzBm);
            if (isEdit)
            {
                strSql.AppendFormat(" and UnvID!='{0}' ", UnvID);
            }

            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 是否存在儿童身份证号
        /// </summary>
        public bool ExistsChildCID(string ChildCID, bool isEdit, string UnvID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_Child_Base]");
            strSql.AppendFormat(" where ChildCID='{0}' ", ChildCID);
            if (isEdit)
            {
                strSql.AppendFormat(" and UnvID!='{0}' ", UnvID);
            }

            return DbHelperSQL.Exists(strSql.ToString());
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [NHS_Child_Base] (");
            strSql.Append("UnvID,QcfwzBm,ChildBm,ChildName,ChildSex,BirthDay,BirthCerNo,ChildCID,CurAreaName,CurAreaCode,RegAreaName,RegAreaCode,FatherName,FatherCID,FatherTel,MotherName,MotherCID,MotherTel,UserID,JzUnit,CreateDate,NextVisitDate,VisitCount,LastVisitDate,LastVisitItems,YCFUserAreaCode,CSZH,InoculationPointID,VillageID,JZSX,BirthWeight,IsHasZSZ,BirthHospital,JinJiYiMiao,Memo)");
            strSql.Append(" values (");
            strSql.Append("@UnvID,@QcfwzBm,@ChildBm,@ChildName,@ChildSex,@BirthDay,@BirthCerNo,@ChildCID,@CurAreaName,@CurAreaCode,@RegAreaName,@RegAreaCode,@FatherName,@FatherCID,@FatherTel,@MotherName,@MotherCID,@MotherTel,@UserID,@JzUnit,@CreateDate,@NextVisitDate,@VisitCount,@LastVisitDate,@LastVisitItems,@YCFUserAreaCode,@CSZH,@InoculationPointID,@VillageID,@JZSX,@BirthWeight,@IsHasZSZ,@BirthHospital,@JinJiYiMiao,@Memo)");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@QcfwzBm", SqlDbType.VarChar,50),
					new SqlParameter("@ChildBm", SqlDbType.VarChar,50),
					new SqlParameter("@ChildName", SqlDbType.VarChar,20),
					new SqlParameter("@ChildSex", SqlDbType.VarChar,10),
					new SqlParameter("@BirthDay", SqlDbType.SmallDateTime),
					new SqlParameter("@BirthCerNo", SqlDbType.VarChar,20),
					new SqlParameter("@ChildCID", SqlDbType.VarChar,20),
					new SqlParameter("@CurAreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@CurAreaCode", SqlDbType.VarChar,20),
					new SqlParameter("@RegAreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@RegAreaCode", SqlDbType.VarChar,20),
					new SqlParameter("@FatherName", SqlDbType.VarChar,50),
					new SqlParameter("@FatherCID", SqlDbType.VarChar,20),
					new SqlParameter("@FatherTel", SqlDbType.VarChar,50),
					new SqlParameter("@MotherName", SqlDbType.VarChar,50),
					new SqlParameter("@MotherCID", SqlDbType.VarChar,20),
					new SqlParameter("@MotherTel", SqlDbType.VarChar,50),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@JzUnit", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@NextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@VisitCount", SqlDbType.Int,4),
					new SqlParameter("@LastVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@LastVisitItems", SqlDbType.VarChar,50),
					new SqlParameter("@YCFUserAreaCode", SqlDbType.VarChar,20),
					new SqlParameter("@CSZH", SqlDbType.VarChar,20),
					new SqlParameter("@InoculationPointID", SqlDbType.Int,4),
					new SqlParameter("@VillageID", SqlDbType.Int,4),
					new SqlParameter("@JZSX", SqlDbType.Int,4),
					new SqlParameter("@BirthWeight", SqlDbType.Float,8),
					new SqlParameter("@IsHasZSZ", SqlDbType.Bit,1),
					new SqlParameter("@BirthHospital", SqlDbType.Int,4),
					new SqlParameter("@JinJiYiMiao", SqlDbType.VarChar,30),
					new SqlParameter("@Memo", SqlDbType.NVarChar,500)};
            parameters[0].Value = UnvID;
            parameters[1].Value = QcfwzBm;
            parameters[2].Value = ChildBm;
            parameters[3].Value = ChildName;
            parameters[4].Value = ChildSex;
            parameters[5].Value = BirthDay;
            parameters[6].Value = BirthCerNo;
            parameters[7].Value = ChildCID;
            parameters[8].Value = CurAreaName;
            parameters[9].Value = CurAreaCode;
            parameters[10].Value = RegAreaName;
            parameters[11].Value = RegAreaCode;
            parameters[12].Value = FatherName;
            parameters[13].Value = FatherCID;
            parameters[14].Value = FatherTel;
            parameters[15].Value = MotherName;
            parameters[16].Value = MotherCID;
            parameters[17].Value = MotherTel;
            parameters[18].Value = UserID;
            parameters[19].Value = JzUnit;
            parameters[20].Value = CreateDate;
            parameters[21].Value = NextVisitDate;
            parameters[22].Value = VisitCount;
            parameters[23].Value = LastVisitDate;
            parameters[24].Value = LastVisitItems;
            parameters[25].Value = YCFUserAreaCode;
            parameters[26].Value = CSZH;
            parameters[27].Value = InoculationPointID;
            parameters[28].Value = VillageID;
            parameters[29].Value = JZSX;
            parameters[30].Value = BirthWeight;
            parameters[31].Value = IsHasZSZ;
            parameters[32].Value = BirthHospital;
            parameters[33].Value = JinJiYiMiao;
            parameters[34].Value = Memo;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [NHS_Child_Base] set ");
            strSql.Append("QcfwzBm=@QcfwzBm,");
            strSql.Append("ChildBm=@ChildBm,");
            strSql.Append("ChildName=@ChildName,");
            strSql.Append("ChildSex=@ChildSex,");
            strSql.Append("BirthDay=@BirthDay,");
            strSql.Append("BirthCerNo=@BirthCerNo,");
            strSql.Append("ChildCID=@ChildCID,");
            strSql.Append("CurAreaName=@CurAreaName,");
            strSql.Append("CurAreaCode=@CurAreaCode,");
            strSql.Append("RegAreaName=@RegAreaName,");
            strSql.Append("RegAreaCode=@RegAreaCode,");
            strSql.Append("FatherName=@FatherName,");
            strSql.Append("FatherCID=@FatherCID,");
            strSql.Append("FatherTel=@FatherTel,");
            strSql.Append("MotherName=@MotherName,");
            strSql.Append("MotherCID=@MotherCID,");
            strSql.Append("MotherTel=@MotherTel,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("JzUnit=@JzUnit,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("NextVisitDate=@NextVisitDate,");
            strSql.Append("VisitCount=@VisitCount,");
            strSql.Append("LastVisitDate=@LastVisitDate,");
            strSql.Append("LastVisitItems=@LastVisitItems,");
            strSql.Append("YCFUserAreaCode=@YCFUserAreaCode,");
            strSql.Append("CSZH=@CSZH,");
            strSql.Append("InoculationPointID=@InoculationPointID,");
            strSql.Append("VillageID=@VillageID,");
            strSql.Append("JZSX=@JZSX,");
            strSql.Append("BirthWeight=@BirthWeight,");
            strSql.Append("IsHasZSZ=@IsHasZSZ,");
            strSql.Append("BirthHospital=@BirthHospital,");
            strSql.Append("JinJiYiMiao=@JinJiYiMiao,");
            strSql.Append("Memo=@Memo");
            strSql.Append(" where UnvID=@UnvID ");
            SqlParameter[] parameters = {
					new SqlParameter("@QcfwzBm", SqlDbType.VarChar,50),
					new SqlParameter("@ChildBm", SqlDbType.VarChar,50),
					new SqlParameter("@ChildName", SqlDbType.VarChar,20),
					new SqlParameter("@ChildSex", SqlDbType.VarChar,10),
					new SqlParameter("@BirthDay", SqlDbType.SmallDateTime),
					new SqlParameter("@BirthCerNo", SqlDbType.VarChar,20),
					new SqlParameter("@ChildCID", SqlDbType.VarChar,20),
					new SqlParameter("@CurAreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@CurAreaCode", SqlDbType.VarChar,20),
					new SqlParameter("@RegAreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@RegAreaCode", SqlDbType.VarChar,20),
					new SqlParameter("@FatherName", SqlDbType.VarChar,50),
					new SqlParameter("@FatherCID", SqlDbType.VarChar,20),
					new SqlParameter("@FatherTel", SqlDbType.VarChar,50),
					new SqlParameter("@MotherName", SqlDbType.VarChar,50),
					new SqlParameter("@MotherCID", SqlDbType.VarChar,20),
					new SqlParameter("@MotherTel", SqlDbType.VarChar,50),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@JzUnit", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@NextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@VisitCount", SqlDbType.Int,4),
					new SqlParameter("@LastVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@LastVisitItems", SqlDbType.VarChar,50),
					new SqlParameter("@YCFUserAreaCode", SqlDbType.VarChar,20),
					new SqlParameter("@CSZH", SqlDbType.VarChar,20),
					new SqlParameter("@InoculationPointID", SqlDbType.Int,4),
					new SqlParameter("@VillageID", SqlDbType.Int,4),
					new SqlParameter("@JZSX", SqlDbType.Int,4),
					new SqlParameter("@BirthWeight", SqlDbType.Float,8),
					new SqlParameter("@IsHasZSZ", SqlDbType.Bit,1),
					new SqlParameter("@BirthHospital", SqlDbType.Int,4),
					new SqlParameter("@JinJiYiMiao", SqlDbType.VarChar,30),
					new SqlParameter("@Memo", SqlDbType.NVarChar,500),
					new SqlParameter("@UnvID", SqlDbType.VarChar,50)};
            parameters[0].Value = QcfwzBm;
            parameters[1].Value = ChildBm;
            parameters[2].Value = ChildName;
            parameters[3].Value = ChildSex;
            parameters[4].Value = BirthDay;
            parameters[5].Value = BirthCerNo;
            parameters[6].Value = ChildCID;
            parameters[7].Value = CurAreaName;
            parameters[8].Value = CurAreaCode;
            parameters[9].Value = RegAreaName;
            parameters[10].Value = RegAreaCode;
            parameters[11].Value = FatherName;
            parameters[12].Value = FatherCID;
            parameters[13].Value = FatherTel;
            parameters[14].Value = MotherName;
            parameters[15].Value = MotherCID;
            parameters[16].Value = MotherTel;
            parameters[17].Value = UserID;
            parameters[18].Value = JzUnit;
            parameters[19].Value = CreateDate;
            parameters[20].Value = NextVisitDate;
            parameters[21].Value = VisitCount;
            parameters[22].Value = LastVisitDate;
            parameters[23].Value = LastVisitItems;
            parameters[24].Value = YCFUserAreaCode;
            parameters[25].Value = CSZH;
            parameters[26].Value = InoculationPointID;
            parameters[27].Value = VillageID;
            parameters[28].Value = JZSX;
            parameters[29].Value = BirthWeight;
            parameters[30].Value = IsHasZSZ;
            parameters[31].Value = BirthHospital;
            parameters[32].Value = JinJiYiMiao;
            parameters[33].Value = Memo;
            parameters[34].Value = UnvID;

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
        public bool Delete(string UnvID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [NHS_Child_Base] ");
            strSql.Append(" where UnvID=@UnvID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50)};
            parameters[0].Value = UnvID;

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
        public void GetModel(string UnvID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UnvID,QcfwzBm,ChildBm,ChildName,ChildSex,BirthDay,BirthCerNo,ChildCID,CurAreaName,CurAreaCode,RegAreaName,RegAreaCode,FatherName,FatherCID,FatherTel,MotherName,MotherCID,MotherTel,UserID,JzUnit,CreateDate,NextVisitDate,VisitCount,LastVisitDate,LastVisitItems,YCFUserAreaCode,CSZH,InoculationPointID,VillageID,JZSX,BirthWeight,IsHasZSZ,BirthHospital,JinJiYiMiao,Memo ");
            strSql.Append(" FROM [NHS_Child_Base] ");
            strSql.Append(" where UnvID=@UnvID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50)};
            parameters[0].Value = UnvID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UnvID"] != null)
                {
                    this.UnvID = ds.Tables[0].Rows[0]["UnvID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QcfwzBm"] != null)
                {
                    this.QcfwzBm = ds.Tables[0].Rows[0]["QcfwzBm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChildBm"] != null)
                {
                    this.ChildBm = ds.Tables[0].Rows[0]["ChildBm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChildName"] != null)
                {
                    this.ChildName = ds.Tables[0].Rows[0]["ChildName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChildSex"] != null)
                {
                    this.ChildSex = ds.Tables[0].Rows[0]["ChildSex"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BirthDay"] != null && ds.Tables[0].Rows[0]["BirthDay"].ToString() != "")
                {
                    this.BirthDay = DateTime.Parse(ds.Tables[0].Rows[0]["BirthDay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BirthCerNo"] != null)
                {
                    this.BirthCerNo = ds.Tables[0].Rows[0]["BirthCerNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChildCID"] != null)
                {
                    this.ChildCID = ds.Tables[0].Rows[0]["ChildCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurAreaName"] != null)
                {
                    this.CurAreaName = ds.Tables[0].Rows[0]["CurAreaName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurAreaCode"] != null)
                {
                    this.CurAreaCode = ds.Tables[0].Rows[0]["CurAreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegAreaName"] != null)
                {
                    this.RegAreaName = ds.Tables[0].Rows[0]["RegAreaName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegAreaCode"] != null)
                {
                    this.RegAreaCode = ds.Tables[0].Rows[0]["RegAreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FatherName"] != null)
                {
                    this.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FatherCID"] != null)
                {
                    this.FatherCID = ds.Tables[0].Rows[0]["FatherCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FatherTel"] != null)
                {
                    this.FatherTel = ds.Tables[0].Rows[0]["FatherTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherName"] != null)
                {
                    this.MotherName = ds.Tables[0].Rows[0]["MotherName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherCID"] != null)
                {
                    this.MotherCID = ds.Tables[0].Rows[0]["MotherCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherTel"] != null)
                {
                    this.MotherTel = ds.Tables[0].Rows[0]["MotherTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JzUnit"] != null)
                {
                    this.JzUnit = ds.Tables[0].Rows[0]["JzUnit"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NextVisitDate"] != null && ds.Tables[0].Rows[0]["NextVisitDate"].ToString() != "")
                {
                    this.NextVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["NextVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VisitCount"] != null && ds.Tables[0].Rows[0]["VisitCount"].ToString() != "")
                {
                    this.VisitCount = int.Parse(ds.Tables[0].Rows[0]["VisitCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastVisitDate"] != null && ds.Tables[0].Rows[0]["LastVisitDate"].ToString() != "")
                {
                    this.LastVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["LastVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastVisitItems"] != null)
                {
                    this.LastVisitItems = ds.Tables[0].Rows[0]["LastVisitItems"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YCFUserAreaCode"] != null)
                {
                    this.YCFUserAreaCode = ds.Tables[0].Rows[0]["YCFUserAreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CSZH"] != null)
                {
                    this.CSZH = ds.Tables[0].Rows[0]["CSZH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InoculationPointID"] != null && ds.Tables[0].Rows[0]["InoculationPointID"].ToString() != "")
                {
                    this.InoculationPointID = int.Parse(ds.Tables[0].Rows[0]["InoculationPointID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VillageID"] != null && ds.Tables[0].Rows[0]["VillageID"].ToString() != "")
                {
                    this.VillageID = int.Parse(ds.Tables[0].Rows[0]["VillageID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JZSX"] != null && ds.Tables[0].Rows[0]["JZSX"].ToString() != "")
                {
                    this.JZSX = int.Parse(ds.Tables[0].Rows[0]["JZSX"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BirthWeight"] != null && ds.Tables[0].Rows[0]["BirthWeight"].ToString() != "")
                {
                    this.BirthWeight = decimal.Parse(ds.Tables[0].Rows[0]["BirthWeight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsHasZSZ"] != null && ds.Tables[0].Rows[0]["IsHasZSZ"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsHasZSZ"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsHasZSZ"].ToString().ToLower() == "true"))
                    {
                        this.IsHasZSZ = true;
                    }
                    else
                    {
                        this.IsHasZSZ = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["BirthHospital"] != null && ds.Tables[0].Rows[0]["BirthHospital"].ToString() != "")
                {
                    this.BirthHospital = int.Parse(ds.Tables[0].Rows[0]["BirthHospital"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JinJiYiMiao"] != null)
                {
                    this.JinJiYiMiao = ds.Tables[0].Rows[0]["JinJiYiMiao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Memo"] != null)
                {
                    this.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
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
            strSql.Append(" FROM [NHS_Child_Base] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }

}
