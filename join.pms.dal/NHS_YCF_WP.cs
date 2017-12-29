using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using UNV.Comm.DataBase;
using System.Data.SqlClient;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_YCF_WP。
    /// </summary>
    [Serializable]
    public partial class NHS_YCF_WP
    {
        public NHS_YCF_WP()
        { }
        #region Model
        private int _commid;
        private string _unvid;
        private string _qcfwzbm;
        private string _fnname;
        private string _fncid;
        private string _minzu;
        private string _fnregaddress;
        private string _fnregaddresscode;
        private string _fnaddress;
        private string _fnaddresscode;
        private string _zfname;
        private string _zfcid;
        private int? _wp;
        private string _contacktel;
        private int? _ffsl;
        private string _ffry;
        private DateTime? _ffrq;
        private string _gljg;
        private string _telephone;
        private string _sfry;
        private DateTime? _sfrq;
        private DateTime? _createdate;
        private int? _userid;
        private DateTime? _fwrq;
        private string _kdx;
        private bool _sffw;
        private bool _ssfykdxy;
        private bool _sfhy;
        private bool _sfzq;
        private bool _sfgwcf;
        private string _lqr;
        /// <summary>
        /// 
        /// </summary>
        public int CommId
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
        public string FnName
        {
            set { _fnname = value; }
            get { return _fnname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FnCID
        {
            set { _fncid = value; }
            get { return _fncid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Minzu
        {
            set { _minzu = value; }
            get { return _minzu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FnRegAddress
        {
            set { _fnregaddress = value; }
            get { return _fnregaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FnRegAddressCode
        {
            set { _fnregaddresscode = value; }
            get { return _fnregaddresscode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FnAddress
        {
            set { _fnaddress = value; }
            get { return _fnaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FnAddressCode
        {
            set { _fnaddresscode = value; }
            get { return _fnaddresscode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZfName
        {
            set { _zfname = value; }
            get { return _zfname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZfCID
        {
            set { _zfcid = value; }
            get { return _zfcid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? WP
        {
            set { _wp = value; }
            get { return _wp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ContackTel
        {
            set { _contacktel = value; }
            get { return _contacktel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FFSL
        {
            set { _ffsl = value; }
            get { return _ffsl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FFRY
        {
            set { _ffry = value; }
            get { return _ffry; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FFRQ
        {
            set { _ffrq = value; }
            get { return _ffrq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GLJG
        {
            set { _gljg = value; }
            get { return _gljg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Telephone
        {
            set { _telephone = value; }
            get { return _telephone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SFRY
        {
            set { _sfry = value; }
            get { return _sfry; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SFRQ
        {
            set { _sfrq = value; }
            get { return _sfrq; }
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
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FWRQ
        {
            set { _fwrq = value; }
            get { return _fwrq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string KDX
        {
            set { _kdx = value; }
            get { return _kdx; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool SFFW
        {
            set { _sffw = value; }
            get { return _sffw; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool SSFYKDXY
        {
            set { _ssfykdxy = value; }
            get { return _ssfykdxy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool SFHY
        {
            set { _sfhy = value; }
            get { return _sfhy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool SFZQ
        {
            set { _sfzq = value; }
            get { return _sfzq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool SFGWCF
        {
            set { _sfgwcf = value; }
            get { return _sfgwcf; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LQR
        {
            set { _lqr = value; }
            get { return _lqr; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NHS_YCF_WP(int CommId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommId,UnvID,QcfwzBm,FnName,FnCID,Minzu,FnRegAddress,FnRegAddressCode,FnAddress,FnAddressCode,ZfName,ZfCID,WP,ContackTel,FFSL,FFRY,FFRQ,GLJG,Telephone,SFRY,SFRQ,CreateDate,UserID,FWRQ,KDX,SFFW,SSFYKDXY,SFHY,SFZQ,SFGWCF,LQR ");
            strSql.Append(" FROM [NHS_YCF_WP] ");
            strSql.Append(" where CommId=@CommId ");
            SqlParameter[] parameters = {
					new SqlParameter("@CommId", SqlDbType.Int,4)};
            parameters[0].Value = CommId;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CommId"] != null && ds.Tables[0].Rows[0]["CommId"].ToString() != "")
                {
                    this.CommId = int.Parse(ds.Tables[0].Rows[0]["CommId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UnvID"] != null)
                {
                    this.UnvID = ds.Tables[0].Rows[0]["UnvID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QcfwzBm"] != null)
                {
                    this.QcfwzBm = ds.Tables[0].Rows[0]["QcfwzBm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnName"] != null)
                {
                    this.FnName = ds.Tables[0].Rows[0]["FnName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnCID"] != null)
                {
                    this.FnCID = ds.Tables[0].Rows[0]["FnCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Minzu"] != null)
                {
                    this.Minzu = ds.Tables[0].Rows[0]["Minzu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnRegAddress"] != null)
                {
                    this.FnRegAddress = ds.Tables[0].Rows[0]["FnRegAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnRegAddressCode"] != null)
                {
                    this.FnRegAddressCode = ds.Tables[0].Rows[0]["FnRegAddressCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnAddress"] != null)
                {
                    this.FnAddress = ds.Tables[0].Rows[0]["FnAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnAddressCode"] != null)
                {
                    this.FnAddressCode = ds.Tables[0].Rows[0]["FnAddressCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZfName"] != null)
                {
                    this.ZfName = ds.Tables[0].Rows[0]["ZfName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZfCID"] != null)
                {
                    this.ZfCID = ds.Tables[0].Rows[0]["ZfCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WP"] != null && ds.Tables[0].Rows[0]["WP"].ToString() != "")
                {
                    this.WP = int.Parse(ds.Tables[0].Rows[0]["WP"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ContackTel"] != null)
                {
                    this.ContackTel = ds.Tables[0].Rows[0]["ContackTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FFSL"] != null && ds.Tables[0].Rows[0]["FFSL"].ToString() != "")
                {
                    this.FFSL = int.Parse(ds.Tables[0].Rows[0]["FFSL"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FFRY"] != null)
                {
                    this.FFRY = ds.Tables[0].Rows[0]["FFRY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FFRQ"] != null && ds.Tables[0].Rows[0]["FFRQ"].ToString() != "")
                {
                    this.FFRQ = DateTime.Parse(ds.Tables[0].Rows[0]["FFRQ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GLJG"] != null)
                {
                    this.GLJG = ds.Tables[0].Rows[0]["GLJG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Telephone"] != null)
                {
                    this.Telephone = ds.Tables[0].Rows[0]["Telephone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFRY"] != null)
                {
                    this.SFRY = ds.Tables[0].Rows[0]["SFRY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFRQ"] != null && ds.Tables[0].Rows[0]["SFRQ"].ToString() != "")
                {
                    this.SFRQ = DateTime.Parse(ds.Tables[0].Rows[0]["SFRQ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FWRQ"] != null && ds.Tables[0].Rows[0]["FWRQ"].ToString() != "")
                {
                    this.FWRQ = DateTime.Parse(ds.Tables[0].Rows[0]["FWRQ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["KDX"] != null)
                {
                    this.KDX = ds.Tables[0].Rows[0]["KDX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFFW"] != null && ds.Tables[0].Rows[0]["SFFW"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["SFFW"].ToString() == "1") || (ds.Tables[0].Rows[0]["SFFW"].ToString().ToLower() == "true"))
                    {
                        this.SFFW = true;
                    }
                    else
                    {
                        this.SFFW = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["SSFYKDXY"] != null && ds.Tables[0].Rows[0]["SSFYKDXY"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["SSFYKDXY"].ToString() == "1") || (ds.Tables[0].Rows[0]["SSFYKDXY"].ToString().ToLower() == "true"))
                    {
                        this.SSFYKDXY = true;
                    }
                    else
                    {
                        this.SSFYKDXY = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["SFHY"] != null && ds.Tables[0].Rows[0]["SFHY"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["SFHY"].ToString() == "1") || (ds.Tables[0].Rows[0]["SFHY"].ToString().ToLower() == "true"))
                    {
                        this.SFHY = true;
                    }
                    else
                    {
                        this.SFHY = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["SFZQ"] != null && ds.Tables[0].Rows[0]["SFZQ"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["SFZQ"].ToString() == "1") || (ds.Tables[0].Rows[0]["SFZQ"].ToString().ToLower() == "true"))
                    {
                        this.SFZQ = true;
                    }
                    else
                    {
                        this.SFZQ = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["SFGWCF"] != null && ds.Tables[0].Rows[0]["SFGWCF"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["SFGWCF"].ToString() == "1") || (ds.Tables[0].Rows[0]["SFGWCF"].ToString().ToLower() == "true"))
                    {
                        this.SFGWCF = true;
                    }
                    else
                    {
                        this.SFGWCF = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["LQR"] != null)
                {
                    this.LQR = ds.Tables[0].Rows[0]["LQR"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CommId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_YCF_WP]");
            strSql.Append(" where CommId=@CommId ");

            SqlParameter[] parameters = {
					new SqlParameter("@CommId", SqlDbType.Int,4)};
            parameters[0].Value = CommId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [NHS_YCF_WP] (");
            strSql.Append("UnvID,QcfwzBm,FnName,FnCID,Minzu,FnRegAddress,FnRegAddressCode,FnAddress,FnAddressCode,ZfName,ZfCID,WP,ContackTel,FFSL,FFRY,FFRQ,GLJG,Telephone,SFRY,SFRQ,CreateDate,UserID,FWRQ,KDX,SFFW,SSFYKDXY,SFHY,SFZQ,SFGWCF,LQR)");
            strSql.Append(" values (");
            strSql.Append("@UnvID,@QcfwzBm,@FnName,@FnCID,@Minzu,@FnRegAddress,@FnRegAddressCode,@FnAddress,@FnAddressCode,@ZfName,@ZfCID,@WP,@ContackTel,@FFSL,@FFRY,@FFRQ,@GLJG,@Telephone,@SFRY,@SFRQ,@CreateDate,@UserID,@FWRQ,@KDX,@SFFW,@SSFYKDXY,@SFHY,@SFZQ,@SFGWCF,@LQR)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@QcfwzBm", SqlDbType.VarChar,50),
					new SqlParameter("@FnName", SqlDbType.NVarChar,20),
					new SqlParameter("@FnCID", SqlDbType.VarChar,20),
					new SqlParameter("@Minzu", SqlDbType.NVarChar,50),
					new SqlParameter("@FnRegAddress", SqlDbType.NVarChar,90),
					new SqlParameter("@FnRegAddressCode", SqlDbType.VarChar,20),
					new SqlParameter("@FnAddress", SqlDbType.NVarChar,90),
					new SqlParameter("@FnAddressCode", SqlDbType.VarChar,20),
					new SqlParameter("@ZfName", SqlDbType.NVarChar,20),
					new SqlParameter("@ZfCID", SqlDbType.VarChar,20),
					new SqlParameter("@WP", SqlDbType.TinyInt,1),
					new SqlParameter("@ContackTel", SqlDbType.VarChar,50),
					new SqlParameter("@FFSL", SqlDbType.TinyInt,1),
					new SqlParameter("@FFRY", SqlDbType.VarChar,20),
					new SqlParameter("@FFRQ", SqlDbType.DateTime),
					new SqlParameter("@GLJG", SqlDbType.NVarChar,50),
					new SqlParameter("@Telephone", SqlDbType.VarChar,20),
					new SqlParameter("@SFRY", SqlDbType.VarChar,20),
					new SqlParameter("@SFRQ", SqlDbType.DateTime),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@FWRQ", SqlDbType.DateTime),
					new SqlParameter("@KDX", SqlDbType.NVarChar,50),
					new SqlParameter("@SFFW", SqlDbType.Bit,1),
					new SqlParameter("@SSFYKDXY", SqlDbType.Bit,1),
					new SqlParameter("@SFHY", SqlDbType.Bit,1),
					new SqlParameter("@SFZQ", SqlDbType.Bit,1),
					new SqlParameter("@SFGWCF", SqlDbType.Bit,1),
					new SqlParameter("@LQR", SqlDbType.VarChar,20)};
            parameters[0].Value = UnvID;
            parameters[1].Value = QcfwzBm;
            parameters[2].Value = FnName;
            parameters[3].Value = FnCID;
            parameters[4].Value = Minzu;
            parameters[5].Value = FnRegAddress;
            parameters[6].Value = FnRegAddressCode;
            parameters[7].Value = FnAddress;
            parameters[8].Value = FnAddressCode;
            parameters[9].Value = ZfName;
            parameters[10].Value = ZfCID;
            parameters[11].Value = WP;
            parameters[12].Value = ContackTel;
            parameters[13].Value = FFSL;
            parameters[14].Value = FFRY;
            parameters[15].Value = FFRQ;
            parameters[16].Value = GLJG;
            parameters[17].Value = Telephone;
            parameters[18].Value = SFRY;
            parameters[19].Value = SFRQ;
            parameters[20].Value = CreateDate;
            parameters[21].Value = UserID;
            parameters[22].Value = FWRQ;
            parameters[23].Value = KDX;
            parameters[24].Value = SFFW;
            parameters[25].Value = SSFYKDXY;
            parameters[26].Value = SFHY;
            parameters[27].Value = SFZQ;
            parameters[28].Value = SFGWCF;
            parameters[29].Value = LQR;

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
            strSql.Append("update [NHS_YCF_WP] set ");
            strSql.Append("UnvID=@UnvID,");
            strSql.Append("QcfwzBm=@QcfwzBm,");
            strSql.Append("FnName=@FnName,");
            strSql.Append("FnCID=@FnCID,");
            strSql.Append("Minzu=@Minzu,");
            strSql.Append("FnRegAddress=@FnRegAddress,");
            strSql.Append("FnRegAddressCode=@FnRegAddressCode,");
            strSql.Append("FnAddress=@FnAddress,");
            strSql.Append("FnAddressCode=@FnAddressCode,");
            strSql.Append("ZfName=@ZfName,");
            strSql.Append("ZfCID=@ZfCID,");
            strSql.Append("WP=@WP,");
            strSql.Append("ContackTel=@ContackTel,");
            strSql.Append("FFSL=@FFSL,");
            strSql.Append("FFRY=@FFRY,");
            strSql.Append("FFRQ=@FFRQ,");
            strSql.Append("GLJG=@GLJG,");
            strSql.Append("Telephone=@Telephone,");
            strSql.Append("SFRY=@SFRY,");
            strSql.Append("SFRQ=@SFRQ,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("FWRQ=@FWRQ,");
            strSql.Append("KDX=@KDX,");
            strSql.Append("SFFW=@SFFW,");
            strSql.Append("SSFYKDXY=@SSFYKDXY,");
            strSql.Append("SFHY=@SFHY,");
            strSql.Append("SFZQ=@SFZQ,");
            strSql.Append("SFGWCF=@SFGWCF,");
            strSql.Append("LQR=@LQR");
            strSql.Append(" where CommId=@CommId ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@QcfwzBm", SqlDbType.VarChar,50),
					new SqlParameter("@FnName", SqlDbType.NVarChar,20),
					new SqlParameter("@FnCID", SqlDbType.VarChar,20),
					new SqlParameter("@Minzu", SqlDbType.NVarChar,50),
					new SqlParameter("@FnRegAddress", SqlDbType.NVarChar,90),
					new SqlParameter("@FnRegAddressCode", SqlDbType.VarChar,20),
					new SqlParameter("@FnAddress", SqlDbType.NVarChar,90),
					new SqlParameter("@FnAddressCode", SqlDbType.VarChar,20),
					new SqlParameter("@ZfName", SqlDbType.NVarChar,20),
					new SqlParameter("@ZfCID", SqlDbType.VarChar,20),
					new SqlParameter("@WP", SqlDbType.TinyInt,1),
					new SqlParameter("@ContackTel", SqlDbType.VarChar,50),
					new SqlParameter("@FFSL", SqlDbType.TinyInt,1),
					new SqlParameter("@FFRY", SqlDbType.VarChar,20),
					new SqlParameter("@FFRQ", SqlDbType.DateTime),
					new SqlParameter("@GLJG", SqlDbType.NVarChar,50),
					new SqlParameter("@Telephone", SqlDbType.VarChar,20),
					new SqlParameter("@SFRY", SqlDbType.VarChar,20),
					new SqlParameter("@SFRQ", SqlDbType.DateTime),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@FWRQ", SqlDbType.DateTime),
					new SqlParameter("@KDX", SqlDbType.NVarChar,50),
					new SqlParameter("@SFFW", SqlDbType.Bit,1),
					new SqlParameter("@SSFYKDXY", SqlDbType.Bit,1),
					new SqlParameter("@SFHY", SqlDbType.Bit,1),
					new SqlParameter("@SFZQ", SqlDbType.Bit,1),
					new SqlParameter("@SFGWCF", SqlDbType.Bit,1),
					new SqlParameter("@LQR", SqlDbType.VarChar,20),
					new SqlParameter("@CommId", SqlDbType.Int,4)};
            parameters[0].Value = UnvID;
            parameters[1].Value = QcfwzBm;
            parameters[2].Value = FnName;
            parameters[3].Value = FnCID;
            parameters[4].Value = Minzu;
            parameters[5].Value = FnRegAddress;
            parameters[6].Value = FnRegAddressCode;
            parameters[7].Value = FnAddress;
            parameters[8].Value = FnAddressCode;
            parameters[9].Value = ZfName;
            parameters[10].Value = ZfCID;
            parameters[11].Value = WP;
            parameters[12].Value = ContackTel;
            parameters[13].Value = FFSL;
            parameters[14].Value = FFRY;
            parameters[15].Value = FFRQ;
            parameters[16].Value = GLJG;
            parameters[17].Value = Telephone;
            parameters[18].Value = SFRY;
            parameters[19].Value = SFRQ;
            parameters[20].Value = CreateDate;
            parameters[21].Value = UserID;
            parameters[22].Value = FWRQ;
            parameters[23].Value = KDX;
            parameters[24].Value = SFFW;
            parameters[25].Value = SSFYKDXY;
            parameters[26].Value = SFHY;
            parameters[27].Value = SFZQ;
            parameters[28].Value = SFGWCF;
            parameters[29].Value = LQR;
            parameters[30].Value = CommId;

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
        public bool Delete(int CommId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [NHS_YCF_WP] ");
            strSql.Append(" where CommId=@CommId ");
            SqlParameter[] parameters = {
					new SqlParameter("@CommId", SqlDbType.Int,4)};
            parameters[0].Value = CommId;

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
        public void GetModel(int CommId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommId,UnvID,QcfwzBm,FnName,FnCID,Minzu,FnRegAddress,FnRegAddressCode,FnAddress,FnAddressCode,ZfName,ZfCID,WP,ContackTel,FFSL,FFRY,FFRQ,GLJG,Telephone,SFRY,SFRQ,CreateDate,UserID,FWRQ,KDX,SFFW,SSFYKDXY,SFHY,SFZQ,SFGWCF,LQR ");
            strSql.Append(" FROM [NHS_YCF_WP] ");
            strSql.Append(" where CommId=@CommId ");
            SqlParameter[] parameters = {
					new SqlParameter("@CommId", SqlDbType.Int,4)};
            parameters[0].Value = CommId;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CommId"] != null && ds.Tables[0].Rows[0]["CommId"].ToString() != "")
                {
                    this.CommId = int.Parse(ds.Tables[0].Rows[0]["CommId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UnvID"] != null)
                {
                    this.UnvID = ds.Tables[0].Rows[0]["UnvID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QcfwzBm"] != null)
                {
                    this.QcfwzBm = ds.Tables[0].Rows[0]["QcfwzBm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnName"] != null)
                {
                    this.FnName = ds.Tables[0].Rows[0]["FnName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnCID"] != null)
                {
                    this.FnCID = ds.Tables[0].Rows[0]["FnCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Minzu"] != null)
                {
                    this.Minzu = ds.Tables[0].Rows[0]["Minzu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnRegAddress"] != null)
                {
                    this.FnRegAddress = ds.Tables[0].Rows[0]["FnRegAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnRegAddressCode"] != null)
                {
                    this.FnRegAddressCode = ds.Tables[0].Rows[0]["FnRegAddressCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnAddress"] != null)
                {
                    this.FnAddress = ds.Tables[0].Rows[0]["FnAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnAddressCode"] != null)
                {
                    this.FnAddressCode = ds.Tables[0].Rows[0]["FnAddressCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZfName"] != null)
                {
                    this.ZfName = ds.Tables[0].Rows[0]["ZfName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZfCID"] != null)
                {
                    this.ZfCID = ds.Tables[0].Rows[0]["ZfCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WP"] != null && ds.Tables[0].Rows[0]["WP"].ToString() != "")
                {
                    this.WP = int.Parse(ds.Tables[0].Rows[0]["WP"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ContackTel"] != null)
                {
                    this.ContackTel = ds.Tables[0].Rows[0]["ContackTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FFSL"] != null && ds.Tables[0].Rows[0]["FFSL"].ToString() != "")
                {
                    this.FFSL = int.Parse(ds.Tables[0].Rows[0]["FFSL"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FFRY"] != null)
                {
                    this.FFRY = ds.Tables[0].Rows[0]["FFRY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FFRQ"] != null && ds.Tables[0].Rows[0]["FFRQ"].ToString() != "")
                {
                    this.FFRQ = DateTime.Parse(ds.Tables[0].Rows[0]["FFRQ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GLJG"] != null)
                {
                    this.GLJG = ds.Tables[0].Rows[0]["GLJG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Telephone"] != null)
                {
                    this.Telephone = ds.Tables[0].Rows[0]["Telephone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFRY"] != null)
                {
                    this.SFRY = ds.Tables[0].Rows[0]["SFRY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFRQ"] != null && ds.Tables[0].Rows[0]["SFRQ"].ToString() != "")
                {
                    this.SFRQ = DateTime.Parse(ds.Tables[0].Rows[0]["SFRQ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FWRQ"] != null && ds.Tables[0].Rows[0]["FWRQ"].ToString() != "")
                {
                    this.FWRQ = DateTime.Parse(ds.Tables[0].Rows[0]["FWRQ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["KDX"] != null)
                {
                    this.KDX = ds.Tables[0].Rows[0]["KDX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFFW"] != null && ds.Tables[0].Rows[0]["SFFW"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["SFFW"].ToString() == "1") || (ds.Tables[0].Rows[0]["SFFW"].ToString().ToLower() == "true"))
                    {
                        this.SFFW = true;
                    }
                    else
                    {
                        this.SFFW = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["SSFYKDXY"] != null && ds.Tables[0].Rows[0]["SSFYKDXY"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["SSFYKDXY"].ToString() == "1") || (ds.Tables[0].Rows[0]["SSFYKDXY"].ToString().ToLower() == "true"))
                    {
                        this.SSFYKDXY = true;
                    }
                    else
                    {
                        this.SSFYKDXY = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["SFHY"] != null && ds.Tables[0].Rows[0]["SFHY"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["SFHY"].ToString() == "1") || (ds.Tables[0].Rows[0]["SFHY"].ToString().ToLower() == "true"))
                    {
                        this.SFHY = true;
                    }
                    else
                    {
                        this.SFHY = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["SFZQ"] != null && ds.Tables[0].Rows[0]["SFZQ"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["SFZQ"].ToString() == "1") || (ds.Tables[0].Rows[0]["SFZQ"].ToString().ToLower() == "true"))
                    {
                        this.SFZQ = true;
                    }
                    else
                    {
                        this.SFZQ = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["SFGWCF"] != null && ds.Tables[0].Rows[0]["SFGWCF"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["SFGWCF"].ToString() == "1") || (ds.Tables[0].Rows[0]["SFGWCF"].ToString().ToLower() == "true"))
                    {
                        this.SFGWCF = true;
                    }
                    else
                    {
                        this.SFGWCF = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["LQR"] != null)
                {
                    this.LQR = ds.Tables[0].Rows[0]["LQR"].ToString();
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
            strSql.Append(" FROM [NHS_YCF_WP] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
