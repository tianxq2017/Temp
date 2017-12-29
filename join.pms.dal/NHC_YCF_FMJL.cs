using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using UNV.Comm.DataBase;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_YCF_FMJL。
    /// </summary>
    [Serializable]
    public partial class NHS_YCF_FMJL
    {
        public NHS_YCF_FMJL()
        { }
        #region Model
        private int _commid;
        private string _unvid;
        private string _zyh;
        private DateTime? _zyrq;
        private DateTime? _cyrq;
        private DateTime? _fmrq;
        private int? _yunzhou;
        private decimal? _zcc;
        private decimal? _minxy;
        private decimal? _maxxy;
        private decimal? _cxl;
        private int? _ts;
        private bool _jthfm;
        private int? _fmfs;
        private string _sssyz;
        private int? _hyqk;
        private string _wfsx;
        private string _ckhbz;
        private string _kkhbzqt = "getdate";
        private int? _gender;
        private decimal? _weight;
        private decimal? _height;
        private decimal? _touwei;
        private decimal? _xiongwei;
        private string _jixing;
        private decimal? _appar;
        private decimal? _appar1;
        private decimal? _appar5;
        private int? _csqk;
        private DateTime? _xseswsj;
        private string _swyy;
        private bool _xishun;
        private bool _buru;
        private bool _muruweiyang;
        private bool _muyingtongshi;
        private string _zzyy;
        private bool _kajie;
        private string _wjzyy;
        private bool _ygjz;
        private bool _jbsc;
        private string _jzdoctors;
        private string _doctors;
        private int? _userid;
        private DateTime? _createdate;
        private string _hospital;
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
        public string ZYH
        {
            set { _zyh = value; }
            get { return _zyh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ZYRQ
        {
            set { _zyrq = value; }
            get { return _zyrq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CYRQ
        {
            set { _cyrq = value; }
            get { return _cyrq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FMRQ
        {
            set { _fmrq = value; }
            get { return _fmrq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Yunzhou
        {
            set { _yunzhou = value; }
            get { return _yunzhou; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ZCC
        {
            set { _zcc = value; }
            get { return _zcc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MinXY
        {
            set { _minxy = value; }
            get { return _minxy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MaxXY
        {
            set { _maxxy = value; }
            get { return _maxxy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CXL
        {
            set { _cxl = value; }
            get { return _cxl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TS
        {
            set { _ts = value; }
            get { return _ts; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool JTHFM
        {
            set { _jthfm = value; }
            get { return _jthfm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FMFS
        {
            set { _fmfs = value; }
            get { return _fmfs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SSSYZ
        {
            set { _sssyz = value; }
            get { return _sssyz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? HYQK
        {
            set { _hyqk = value; }
            get { return _hyqk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WFSX
        {
            set { _wfsx = value; }
            get { return _wfsx; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CKHBZ
        {
            set { _ckhbz = value; }
            get { return _ckhbz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string KKHBZQt
        {
            set { _kkhbzqt = value; }
            get { return _kkhbzqt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Gender
        {
            set { _gender = value; }
            get { return _gender; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Weight
        {
            set { _weight = value; }
            get { return _weight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Height
        {
            set { _height = value; }
            get { return _height; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TouWei
        {
            set { _touwei = value; }
            get { return _touwei; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? XiongWei
        {
            set { _xiongwei = value; }
            get { return _xiongwei; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JiXing
        {
            set { _jixing = value; }
            get { return _jixing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Appar
        {
            set { _appar = value; }
            get { return _appar; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Appar1
        {
            set { _appar1 = value; }
            get { return _appar1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Appar5
        {
            set { _appar5 = value; }
            get { return _appar5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CSQK
        {
            set { _csqk = value; }
            get { return _csqk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? XSESWSJ
        {
            set { _xseswsj = value; }
            get { return _xseswsj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SWYY
        {
            set { _swyy = value; }
            get { return _swyy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool XiShun
        {
            set { _xishun = value; }
            get { return _xishun; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool BuRu
        {
            set { _buru = value; }
            get { return _buru; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool MuRuWeiYang
        {
            set { _muruweiyang = value; }
            get { return _muruweiyang; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool MuyingTongshi
        {
            set { _muyingtongshi = value; }
            get { return _muyingtongshi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZZYY
        {
            set { _zzyy = value; }
            get { return _zzyy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Kajie
        {
            set { _kajie = value; }
            get { return _kajie; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WJZYY
        {
            set { _wjzyy = value; }
            get { return _wjzyy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool YGJZ
        {
            set { _ygjz = value; }
            get { return _ygjz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool JBSC
        {
            set { _jbsc = value; }
            get { return _jbsc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JZDoctors
        {
            set { _jzdoctors = value; }
            get { return _jzdoctors; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Doctors
        {
            set { _doctors = value; }
            get { return _doctors; }
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
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Hospital
        {
            set { _hospital = value; }
            get { return _hospital; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NHS_YCF_FMJL(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommID,UnvID,ZYH,ZYRQ,CYRQ,FMRQ,Yunzhou,ZCC,MinXY,MaxXY,CXL,TS,JTHFM,FMFS,SSSYZ,HYQK,WFSX,CKHBZ,KKHBZQt,Gender,Weight,Height,TouWei,XiongWei,JiXing,Appar,Appar1,Appar5,CSQK,XSESWSJ,SWYY,XiShun,BuRu,MuRuWeiYang,MuyingTongshi,ZZYY,Kajie,WJZYY,YGJZ,JBSC,JZDoctors,Doctors,UserID,CreateDate,Hospital ");
            strSql.Append(" FROM [NHS_YCF_FMJL] ");
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
                if (ds.Tables[0].Rows[0]["ZYH"] != null)
                {
                    this.ZYH = ds.Tables[0].Rows[0]["ZYH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZYRQ"] != null && ds.Tables[0].Rows[0]["ZYRQ"].ToString() != "")
                {
                    this.ZYRQ = DateTime.Parse(ds.Tables[0].Rows[0]["ZYRQ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CYRQ"] != null && ds.Tables[0].Rows[0]["CYRQ"].ToString() != "")
                {
                    this.CYRQ = DateTime.Parse(ds.Tables[0].Rows[0]["CYRQ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FMRQ"] != null && ds.Tables[0].Rows[0]["FMRQ"].ToString() != "")
                {
                    this.FMRQ = DateTime.Parse(ds.Tables[0].Rows[0]["FMRQ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Yunzhou"] != null && ds.Tables[0].Rows[0]["Yunzhou"].ToString() != "")
                {
                    this.Yunzhou = int.Parse(ds.Tables[0].Rows[0]["Yunzhou"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZCC"] != null && ds.Tables[0].Rows[0]["ZCC"].ToString() != "")
                {
                    this.ZCC = decimal.Parse(ds.Tables[0].Rows[0]["ZCC"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MinXY"] != null && ds.Tables[0].Rows[0]["MinXY"].ToString() != "")
                {
                    this.MinXY = decimal.Parse(ds.Tables[0].Rows[0]["MinXY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaxXY"] != null && ds.Tables[0].Rows[0]["MaxXY"].ToString() != "")
                {
                    this.MaxXY = decimal.Parse(ds.Tables[0].Rows[0]["MaxXY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CXL"] != null && ds.Tables[0].Rows[0]["CXL"].ToString() != "")
                {
                    this.CXL = decimal.Parse(ds.Tables[0].Rows[0]["CXL"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TS"] != null && ds.Tables[0].Rows[0]["TS"].ToString() != "")
                {
                    this.TS = int.Parse(ds.Tables[0].Rows[0]["TS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JTHFM"] != null && ds.Tables[0].Rows[0]["JTHFM"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["JTHFM"].ToString() == "1") || (ds.Tables[0].Rows[0]["JTHFM"].ToString().ToLower() == "true"))
                    {
                        this.JTHFM = true;
                    }
                    else
                    {
                        this.JTHFM = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["FMFS"] != null && ds.Tables[0].Rows[0]["FMFS"].ToString() != "")
                {
                    this.FMFS = int.Parse(ds.Tables[0].Rows[0]["FMFS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SSSYZ"] != null)
                {
                    this.SSSYZ = ds.Tables[0].Rows[0]["SSSYZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HYQK"] != null && ds.Tables[0].Rows[0]["HYQK"].ToString() != "")
                {
                    this.HYQK = int.Parse(ds.Tables[0].Rows[0]["HYQK"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WFSX"] != null)
                {
                    this.WFSX = ds.Tables[0].Rows[0]["WFSX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CKHBZ"] != null)
                {
                    this.CKHBZ = ds.Tables[0].Rows[0]["CKHBZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["KKHBZQt"] != null)
                {
                    this.KKHBZQt = ds.Tables[0].Rows[0]["KKHBZQt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Gender"] != null && ds.Tables[0].Rows[0]["Gender"].ToString() != "")
                {
                    this.Gender = int.Parse(ds.Tables[0].Rows[0]["Gender"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Weight"] != null && ds.Tables[0].Rows[0]["Weight"].ToString() != "")
                {
                    this.Weight = decimal.Parse(ds.Tables[0].Rows[0]["Weight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Height"] != null && ds.Tables[0].Rows[0]["Height"].ToString() != "")
                {
                    this.Height = decimal.Parse(ds.Tables[0].Rows[0]["Height"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TouWei"] != null && ds.Tables[0].Rows[0]["TouWei"].ToString() != "")
                {
                    this.TouWei = decimal.Parse(ds.Tables[0].Rows[0]["TouWei"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XiongWei"] != null && ds.Tables[0].Rows[0]["XiongWei"].ToString() != "")
                {
                    this.XiongWei = decimal.Parse(ds.Tables[0].Rows[0]["XiongWei"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JiXing"] != null)
                {
                    this.JiXing = ds.Tables[0].Rows[0]["JiXing"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Appar"] != null && ds.Tables[0].Rows[0]["Appar"].ToString() != "")
                {
                    this.Appar = decimal.Parse(ds.Tables[0].Rows[0]["Appar"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Appar1"] != null && ds.Tables[0].Rows[0]["Appar1"].ToString() != "")
                {
                    this.Appar1 = decimal.Parse(ds.Tables[0].Rows[0]["Appar1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Appar5"] != null && ds.Tables[0].Rows[0]["Appar5"].ToString() != "")
                {
                    this.Appar5 = decimal.Parse(ds.Tables[0].Rows[0]["Appar5"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CSQK"] != null && ds.Tables[0].Rows[0]["CSQK"].ToString() != "")
                {
                    this.CSQK = int.Parse(ds.Tables[0].Rows[0]["CSQK"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XSESWSJ"] != null && ds.Tables[0].Rows[0]["XSESWSJ"].ToString() != "")
                {
                    this.XSESWSJ = DateTime.Parse(ds.Tables[0].Rows[0]["XSESWSJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SWYY"] != null)
                {
                    this.SWYY = ds.Tables[0].Rows[0]["SWYY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XiShun"] != null && ds.Tables[0].Rows[0]["XiShun"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["XiShun"].ToString() == "1") || (ds.Tables[0].Rows[0]["XiShun"].ToString().ToLower() == "true"))
                    {
                        this.XiShun = true;
                    }
                    else
                    {
                        this.XiShun = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["BuRu"] != null && ds.Tables[0].Rows[0]["BuRu"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["BuRu"].ToString() == "1") || (ds.Tables[0].Rows[0]["BuRu"].ToString().ToLower() == "true"))
                    {
                        this.BuRu = true;
                    }
                    else
                    {
                        this.BuRu = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["MuRuWeiYang"] != null && ds.Tables[0].Rows[0]["MuRuWeiYang"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["MuRuWeiYang"].ToString() == "1") || (ds.Tables[0].Rows[0]["MuRuWeiYang"].ToString().ToLower() == "true"))
                    {
                        this.MuRuWeiYang = true;
                    }
                    else
                    {
                        this.MuRuWeiYang = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["MuyingTongshi"] != null && ds.Tables[0].Rows[0]["MuyingTongshi"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["MuyingTongshi"].ToString() == "1") || (ds.Tables[0].Rows[0]["MuyingTongshi"].ToString().ToLower() == "true"))
                    {
                        this.MuyingTongshi = true;
                    }
                    else
                    {
                        this.MuyingTongshi = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["ZZYY"] != null)
                {
                    this.ZZYY = ds.Tables[0].Rows[0]["ZZYY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Kajie"] != null && ds.Tables[0].Rows[0]["Kajie"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Kajie"].ToString() == "1") || (ds.Tables[0].Rows[0]["Kajie"].ToString().ToLower() == "true"))
                    {
                        this.Kajie = true;
                    }
                    else
                    {
                        this.Kajie = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["WJZYY"] != null)
                {
                    this.WJZYY = ds.Tables[0].Rows[0]["WJZYY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YGJZ"] != null && ds.Tables[0].Rows[0]["YGJZ"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["YGJZ"].ToString() == "1") || (ds.Tables[0].Rows[0]["YGJZ"].ToString().ToLower() == "true"))
                    {
                        this.YGJZ = true;
                    }
                    else
                    {
                        this.YGJZ = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["JBSC"] != null && ds.Tables[0].Rows[0]["JBSC"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["JBSC"].ToString() == "1") || (ds.Tables[0].Rows[0]["JBSC"].ToString().ToLower() == "true"))
                    {
                        this.JBSC = true;
                    }
                    else
                    {
                        this.JBSC = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["JZDoctors"] != null)
                {
                    this.JZDoctors = ds.Tables[0].Rows[0]["JZDoctors"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Doctors"] != null)
                {
                    this.Doctors = ds.Tables[0].Rows[0]["Doctors"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Hospital"] != null)
                {
                    this.Hospital = ds.Tables[0].Rows[0]["Hospital"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_YCF_FMJL]");
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
            strSql.Append("select count(1) from [NHS_YCF_FMJL]");
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
            strSql.Append("insert into [NHS_YCF_FMJL] (");
            strSql.Append("UnvID,ZYH,ZYRQ,CYRQ,FMRQ,Yunzhou,ZCC,MinXY,MaxXY,CXL,TS,JTHFM,FMFS,SSSYZ,HYQK,WFSX,CKHBZ,KKHBZQt,Gender,Weight,Height,TouWei,XiongWei,JiXing,Appar,Appar1,Appar5,CSQK,XSESWSJ,SWYY,XiShun,BuRu,MuRuWeiYang,MuyingTongshi,ZZYY,Kajie,WJZYY,YGJZ,JBSC,JZDoctors,Doctors,UserID,CreateDate,Hospital)");
            strSql.Append(" values (");
            strSql.Append("@UnvID,@ZYH,@ZYRQ,@CYRQ,@FMRQ,@Yunzhou,@ZCC,@MinXY,@MaxXY,@CXL,@TS,@JTHFM,@FMFS,@SSSYZ,@HYQK,@WFSX,@CKHBZ,@KKHBZQt,@Gender,@Weight,@Height,@TouWei,@XiongWei,@JiXing,@Appar,@Appar1,@Appar5,@CSQK,@XSESWSJ,@SWYY,@XiShun,@BuRu,@MuRuWeiYang,@MuyingTongshi,@ZZYY,@Kajie,@WJZYY,@YGJZ,@JBSC,@JZDoctors,@Doctors,@UserID,@CreateDate,@Hospital)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@ZYH", SqlDbType.VarChar,50),
					new SqlParameter("@ZYRQ", SqlDbType.DateTime),
					new SqlParameter("@CYRQ", SqlDbType.DateTime),
					new SqlParameter("@FMRQ", SqlDbType.DateTime),
					new SqlParameter("@Yunzhou", SqlDbType.Int,4),
					new SqlParameter("@ZCC", SqlDbType.Float,8),
					new SqlParameter("@MinXY", SqlDbType.Float,8),
					new SqlParameter("@MaxXY", SqlDbType.Float,8),
					new SqlParameter("@CXL", SqlDbType.Float,8),
					new SqlParameter("@TS", SqlDbType.Int,4),
					new SqlParameter("@JTHFM", SqlDbType.Bit,1),
					new SqlParameter("@FMFS", SqlDbType.Int,4),
					new SqlParameter("@SSSYZ", SqlDbType.VarChar,50),
					new SqlParameter("@HYQK", SqlDbType.Int,4),
					new SqlParameter("@WFSX", SqlDbType.VarChar,50),
					new SqlParameter("@CKHBZ", SqlDbType.VarChar,50),
					new SqlParameter("@KKHBZQt", SqlDbType.VarChar,50),
					new SqlParameter("@Gender", SqlDbType.Int,4),
					new SqlParameter("@Weight", SqlDbType.Float,8),
					new SqlParameter("@Height", SqlDbType.Float,8),
					new SqlParameter("@TouWei", SqlDbType.Float,8),
					new SqlParameter("@XiongWei", SqlDbType.Float,8),
					new SqlParameter("@JiXing", SqlDbType.VarChar,50),
					new SqlParameter("@Appar", SqlDbType.Float,8),
					new SqlParameter("@Appar1", SqlDbType.Float,8),
					new SqlParameter("@Appar5", SqlDbType.Float,8),
					new SqlParameter("@CSQK", SqlDbType.Int,4),
					new SqlParameter("@XSESWSJ", SqlDbType.DateTime),
					new SqlParameter("@SWYY", SqlDbType.NVarChar,50),
					new SqlParameter("@XiShun", SqlDbType.Bit,1),
					new SqlParameter("@BuRu", SqlDbType.Bit,1),
					new SqlParameter("@MuRuWeiYang", SqlDbType.Bit,1),
					new SqlParameter("@MuyingTongshi", SqlDbType.Bit,1),
					new SqlParameter("@ZZYY", SqlDbType.NVarChar,50),
					new SqlParameter("@Kajie", SqlDbType.Bit,1),
					new SqlParameter("@WJZYY", SqlDbType.NVarChar,50),
					new SqlParameter("@YGJZ", SqlDbType.Bit,1),
					new SqlParameter("@JBSC", SqlDbType.Bit,1),
					new SqlParameter("@JZDoctors", SqlDbType.NVarChar,20),
					new SqlParameter("@Doctors", SqlDbType.NVarChar,20),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@Hospital", SqlDbType.NVarChar,50)};
            parameters[0].Value = UnvID;
            parameters[1].Value = ZYH;
            parameters[2].Value = ZYRQ;
            parameters[3].Value = CYRQ;
            parameters[4].Value = FMRQ;
            parameters[5].Value = Yunzhou;
            parameters[6].Value = ZCC;
            parameters[7].Value = MinXY;
            parameters[8].Value = MaxXY;
            parameters[9].Value = CXL;
            parameters[10].Value = TS;
            parameters[11].Value = JTHFM;
            parameters[12].Value = FMFS;
            parameters[13].Value = SSSYZ;
            parameters[14].Value = HYQK;
            parameters[15].Value = WFSX;
            parameters[16].Value = CKHBZ;
            parameters[17].Value = KKHBZQt;
            parameters[18].Value = Gender;
            parameters[19].Value = Weight;
            parameters[20].Value = Height;
            parameters[21].Value = TouWei;
            parameters[22].Value = XiongWei;
            parameters[23].Value = JiXing;
            parameters[24].Value = Appar;
            parameters[25].Value = Appar1;
            parameters[26].Value = Appar5;
            parameters[27].Value = CSQK;
            parameters[28].Value = XSESWSJ;
            parameters[29].Value = SWYY;
            parameters[30].Value = XiShun;
            parameters[31].Value = BuRu;
            parameters[32].Value = MuRuWeiYang;
            parameters[33].Value = MuyingTongshi;
            parameters[34].Value = ZZYY;
            parameters[35].Value = Kajie;
            parameters[36].Value = WJZYY;
            parameters[37].Value = YGJZ;
            parameters[38].Value = JBSC;
            parameters[39].Value = JZDoctors;
            parameters[40].Value = Doctors;
            parameters[41].Value = UserID;
            parameters[42].Value = CreateDate;
            parameters[43].Value = Hospital;

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
            strSql.Append("update [NHS_YCF_FMJL] set ");
            strSql.Append("UnvID=@UnvID,");
            strSql.Append("ZYH=@ZYH,");
            strSql.Append("ZYRQ=@ZYRQ,");
            strSql.Append("CYRQ=@CYRQ,");
            strSql.Append("FMRQ=@FMRQ,");
            strSql.Append("Yunzhou=@Yunzhou,");
            strSql.Append("ZCC=@ZCC,");
            strSql.Append("MinXY=@MinXY,");
            strSql.Append("MaxXY=@MaxXY,");
            strSql.Append("CXL=@CXL,");
            strSql.Append("TS=@TS,");
            strSql.Append("JTHFM=@JTHFM,");
            strSql.Append("FMFS=@FMFS,");
            strSql.Append("SSSYZ=@SSSYZ,");
            strSql.Append("HYQK=@HYQK,");
            strSql.Append("WFSX=@WFSX,");
            strSql.Append("CKHBZ=@CKHBZ,");
            strSql.Append("KKHBZQt=@KKHBZQt,");
            strSql.Append("Gender=@Gender,");
            strSql.Append("Weight=@Weight,");
            strSql.Append("Height=@Height,");
            strSql.Append("TouWei=@TouWei,");
            strSql.Append("XiongWei=@XiongWei,");
            strSql.Append("JiXing=@JiXing,");
            strSql.Append("Appar=@Appar,");
            strSql.Append("Appar1=@Appar1,");
            strSql.Append("Appar5=@Appar5,");
            strSql.Append("CSQK=@CSQK,");
            strSql.Append("XSESWSJ=@XSESWSJ,");
            strSql.Append("SWYY=@SWYY,");
            strSql.Append("XiShun=@XiShun,");
            strSql.Append("BuRu=@BuRu,");
            strSql.Append("MuRuWeiYang=@MuRuWeiYang,");
            strSql.Append("MuyingTongshi=@MuyingTongshi,");
            strSql.Append("ZZYY=@ZZYY,");
            strSql.Append("Kajie=@Kajie,");
            strSql.Append("WJZYY=@WJZYY,");
            strSql.Append("YGJZ=@YGJZ,");
            strSql.Append("JBSC=@JBSC,");
            strSql.Append("JZDoctors=@JZDoctors,");
            strSql.Append("Doctors=@Doctors,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("Hospital=@Hospital");
            strSql.Append(" where CommID=@CommID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@ZYH", SqlDbType.VarChar,50),
					new SqlParameter("@ZYRQ", SqlDbType.DateTime),
					new SqlParameter("@CYRQ", SqlDbType.DateTime),
					new SqlParameter("@FMRQ", SqlDbType.DateTime),
					new SqlParameter("@Yunzhou", SqlDbType.Int,4),
					new SqlParameter("@ZCC", SqlDbType.Float,8),
					new SqlParameter("@MinXY", SqlDbType.Float,8),
					new SqlParameter("@MaxXY", SqlDbType.Float,8),
					new SqlParameter("@CXL", SqlDbType.Float,8),
					new SqlParameter("@TS", SqlDbType.Int,4),
					new SqlParameter("@JTHFM", SqlDbType.Bit,1),
					new SqlParameter("@FMFS", SqlDbType.Int,4),
					new SqlParameter("@SSSYZ", SqlDbType.VarChar,50),
					new SqlParameter("@HYQK", SqlDbType.Int,4),
					new SqlParameter("@WFSX", SqlDbType.VarChar,50),
					new SqlParameter("@CKHBZ", SqlDbType.VarChar,50),
					new SqlParameter("@KKHBZQt", SqlDbType.VarChar,50),
					new SqlParameter("@Gender", SqlDbType.Int,4),
					new SqlParameter("@Weight", SqlDbType.Float,8),
					new SqlParameter("@Height", SqlDbType.Float,8),
					new SqlParameter("@TouWei", SqlDbType.Float,8),
					new SqlParameter("@XiongWei", SqlDbType.Float,8),
					new SqlParameter("@JiXing", SqlDbType.VarChar,50),
					new SqlParameter("@Appar", SqlDbType.Float,8),
					new SqlParameter("@Appar1", SqlDbType.Float,8),
					new SqlParameter("@Appar5", SqlDbType.Float,8),
					new SqlParameter("@CSQK", SqlDbType.Int,4),
					new SqlParameter("@XSESWSJ", SqlDbType.DateTime),
					new SqlParameter("@SWYY", SqlDbType.NVarChar,50),
					new SqlParameter("@XiShun", SqlDbType.Bit,1),
					new SqlParameter("@BuRu", SqlDbType.Bit,1),
					new SqlParameter("@MuRuWeiYang", SqlDbType.Bit,1),
					new SqlParameter("@MuyingTongshi", SqlDbType.Bit,1),
					new SqlParameter("@ZZYY", SqlDbType.NVarChar,50),
					new SqlParameter("@Kajie", SqlDbType.Bit,1),
					new SqlParameter("@WJZYY", SqlDbType.NVarChar,50),
					new SqlParameter("@YGJZ", SqlDbType.Bit,1),
					new SqlParameter("@JBSC", SqlDbType.Bit,1),
					new SqlParameter("@JZDoctors", SqlDbType.NVarChar,20),
					new SqlParameter("@Doctors", SqlDbType.NVarChar,20),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@Hospital", SqlDbType.NVarChar,50),
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = UnvID;
            parameters[1].Value = ZYH;
            parameters[2].Value = ZYRQ;
            parameters[3].Value = CYRQ;
            parameters[4].Value = FMRQ;
            parameters[5].Value = Yunzhou;
            parameters[6].Value = ZCC;
            parameters[7].Value = MinXY;
            parameters[8].Value = MaxXY;
            parameters[9].Value = CXL;
            parameters[10].Value = TS;
            parameters[11].Value = JTHFM;
            parameters[12].Value = FMFS;
            parameters[13].Value = SSSYZ;
            parameters[14].Value = HYQK;
            parameters[15].Value = WFSX;
            parameters[16].Value = CKHBZ;
            parameters[17].Value = KKHBZQt;
            parameters[18].Value = Gender;
            parameters[19].Value = Weight;
            parameters[20].Value = Height;
            parameters[21].Value = TouWei;
            parameters[22].Value = XiongWei;
            parameters[23].Value = JiXing;
            parameters[24].Value = Appar;
            parameters[25].Value = Appar1;
            parameters[26].Value = Appar5;
            parameters[27].Value = CSQK;
            parameters[28].Value = XSESWSJ;
            parameters[29].Value = SWYY;
            parameters[30].Value = XiShun;
            parameters[31].Value = BuRu;
            parameters[32].Value = MuRuWeiYang;
            parameters[33].Value = MuyingTongshi;
            parameters[34].Value = ZZYY;
            parameters[35].Value = Kajie;
            parameters[36].Value = WJZYY;
            parameters[37].Value = YGJZ;
            parameters[38].Value = JBSC;
            parameters[39].Value = JZDoctors;
            parameters[40].Value = Doctors;
            parameters[41].Value = UserID;
            parameters[42].Value = CreateDate;
            parameters[43].Value = Hospital;
            parameters[44].Value = CommID;

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
            strSql.Append("delete from [NHS_YCF_FMJL] ");
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
            strSql.Append("select CommID,UnvID,ZYH,ZYRQ,CYRQ,FMRQ,Yunzhou,ZCC,MinXY,MaxXY,CXL,TS,JTHFM,FMFS,SSSYZ,HYQK,WFSX,CKHBZ,KKHBZQt,Gender,Weight,Height,TouWei,XiongWei,JiXing,Appar,Appar1,Appar5,CSQK,XSESWSJ,SWYY,XiShun,BuRu,MuRuWeiYang,MuyingTongshi,ZZYY,Kajie,WJZYY,YGJZ,JBSC,JZDoctors,Doctors,UserID,CreateDate,Hospital ");
            strSql.Append(" FROM [NHS_YCF_FMJL] ");
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
                if (ds.Tables[0].Rows[0]["ZYH"] != null)
                {
                    this.ZYH = ds.Tables[0].Rows[0]["ZYH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZYRQ"] != null && ds.Tables[0].Rows[0]["ZYRQ"].ToString() != "")
                {
                    this.ZYRQ = DateTime.Parse(ds.Tables[0].Rows[0]["ZYRQ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CYRQ"] != null && ds.Tables[0].Rows[0]["CYRQ"].ToString() != "")
                {
                    this.CYRQ = DateTime.Parse(ds.Tables[0].Rows[0]["CYRQ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FMRQ"] != null && ds.Tables[0].Rows[0]["FMRQ"].ToString() != "")
                {
                    this.FMRQ = DateTime.Parse(ds.Tables[0].Rows[0]["FMRQ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Yunzhou"] != null && ds.Tables[0].Rows[0]["Yunzhou"].ToString() != "")
                {
                    this.Yunzhou = int.Parse(ds.Tables[0].Rows[0]["Yunzhou"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZCC"] != null && ds.Tables[0].Rows[0]["ZCC"].ToString() != "")
                {
                    this.ZCC = decimal.Parse(ds.Tables[0].Rows[0]["ZCC"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MinXY"] != null && ds.Tables[0].Rows[0]["MinXY"].ToString() != "")
                {
                    this.MinXY = decimal.Parse(ds.Tables[0].Rows[0]["MinXY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaxXY"] != null && ds.Tables[0].Rows[0]["MaxXY"].ToString() != "")
                {
                    this.MaxXY = decimal.Parse(ds.Tables[0].Rows[0]["MaxXY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CXL"] != null && ds.Tables[0].Rows[0]["CXL"].ToString() != "")
                {
                    this.CXL = decimal.Parse(ds.Tables[0].Rows[0]["CXL"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TS"] != null && ds.Tables[0].Rows[0]["TS"].ToString() != "")
                {
                    this.TS = int.Parse(ds.Tables[0].Rows[0]["TS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JTHFM"] != null && ds.Tables[0].Rows[0]["JTHFM"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["JTHFM"].ToString() == "1") || (ds.Tables[0].Rows[0]["JTHFM"].ToString().ToLower() == "true"))
                    {
                        this.JTHFM = true;
                    }
                    else
                    {
                        this.JTHFM = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["FMFS"] != null && ds.Tables[0].Rows[0]["FMFS"].ToString() != "")
                {
                    this.FMFS = int.Parse(ds.Tables[0].Rows[0]["FMFS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SSSYZ"] != null)
                {
                    this.SSSYZ = ds.Tables[0].Rows[0]["SSSYZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HYQK"] != null && ds.Tables[0].Rows[0]["HYQK"].ToString() != "")
                {
                    this.HYQK = int.Parse(ds.Tables[0].Rows[0]["HYQK"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WFSX"] != null)
                {
                    this.WFSX = ds.Tables[0].Rows[0]["WFSX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CKHBZ"] != null)
                {
                    this.CKHBZ = ds.Tables[0].Rows[0]["CKHBZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["KKHBZQt"] != null)
                {
                    this.KKHBZQt = ds.Tables[0].Rows[0]["KKHBZQt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Gender"] != null && ds.Tables[0].Rows[0]["Gender"].ToString() != "")
                {
                    this.Gender = int.Parse(ds.Tables[0].Rows[0]["Gender"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Weight"] != null && ds.Tables[0].Rows[0]["Weight"].ToString() != "")
                {
                    this.Weight = decimal.Parse(ds.Tables[0].Rows[0]["Weight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Height"] != null && ds.Tables[0].Rows[0]["Height"].ToString() != "")
                {
                    this.Height = decimal.Parse(ds.Tables[0].Rows[0]["Height"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TouWei"] != null && ds.Tables[0].Rows[0]["TouWei"].ToString() != "")
                {
                    this.TouWei = decimal.Parse(ds.Tables[0].Rows[0]["TouWei"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XiongWei"] != null && ds.Tables[0].Rows[0]["XiongWei"].ToString() != "")
                {
                    this.XiongWei = decimal.Parse(ds.Tables[0].Rows[0]["XiongWei"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JiXing"] != null)
                {
                    this.JiXing = ds.Tables[0].Rows[0]["JiXing"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Appar"] != null && ds.Tables[0].Rows[0]["Appar"].ToString() != "")
                {
                    this.Appar = decimal.Parse(ds.Tables[0].Rows[0]["Appar"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Appar1"] != null && ds.Tables[0].Rows[0]["Appar1"].ToString() != "")
                {
                    this.Appar1 = decimal.Parse(ds.Tables[0].Rows[0]["Appar1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Appar5"] != null && ds.Tables[0].Rows[0]["Appar5"].ToString() != "")
                {
                    this.Appar5 = decimal.Parse(ds.Tables[0].Rows[0]["Appar5"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CSQK"] != null && ds.Tables[0].Rows[0]["CSQK"].ToString() != "")
                {
                    this.CSQK = int.Parse(ds.Tables[0].Rows[0]["CSQK"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XSESWSJ"] != null && ds.Tables[0].Rows[0]["XSESWSJ"].ToString() != "")
                {
                    this.XSESWSJ = DateTime.Parse(ds.Tables[0].Rows[0]["XSESWSJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SWYY"] != null)
                {
                    this.SWYY = ds.Tables[0].Rows[0]["SWYY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XiShun"] != null && ds.Tables[0].Rows[0]["XiShun"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["XiShun"].ToString() == "1") || (ds.Tables[0].Rows[0]["XiShun"].ToString().ToLower() == "true"))
                    {
                        this.XiShun = true;
                    }
                    else
                    {
                        this.XiShun = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["BuRu"] != null && ds.Tables[0].Rows[0]["BuRu"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["BuRu"].ToString() == "1") || (ds.Tables[0].Rows[0]["BuRu"].ToString().ToLower() == "true"))
                    {
                        this.BuRu = true;
                    }
                    else
                    {
                        this.BuRu = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["MuRuWeiYang"] != null && ds.Tables[0].Rows[0]["MuRuWeiYang"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["MuRuWeiYang"].ToString() == "1") || (ds.Tables[0].Rows[0]["MuRuWeiYang"].ToString().ToLower() == "true"))
                    {
                        this.MuRuWeiYang = true;
                    }
                    else
                    {
                        this.MuRuWeiYang = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["MuyingTongshi"] != null && ds.Tables[0].Rows[0]["MuyingTongshi"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["MuyingTongshi"].ToString() == "1") || (ds.Tables[0].Rows[0]["MuyingTongshi"].ToString().ToLower() == "true"))
                    {
                        this.MuyingTongshi = true;
                    }
                    else
                    {
                        this.MuyingTongshi = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["ZZYY"] != null)
                {
                    this.ZZYY = ds.Tables[0].Rows[0]["ZZYY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Kajie"] != null && ds.Tables[0].Rows[0]["Kajie"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Kajie"].ToString() == "1") || (ds.Tables[0].Rows[0]["Kajie"].ToString().ToLower() == "true"))
                    {
                        this.Kajie = true;
                    }
                    else
                    {
                        this.Kajie = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["WJZYY"] != null)
                {
                    this.WJZYY = ds.Tables[0].Rows[0]["WJZYY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YGJZ"] != null && ds.Tables[0].Rows[0]["YGJZ"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["YGJZ"].ToString() == "1") || (ds.Tables[0].Rows[0]["YGJZ"].ToString().ToLower() == "true"))
                    {
                        this.YGJZ = true;
                    }
                    else
                    {
                        this.YGJZ = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["JBSC"] != null && ds.Tables[0].Rows[0]["JBSC"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["JBSC"].ToString() == "1") || (ds.Tables[0].Rows[0]["JBSC"].ToString().ToLower() == "true"))
                    {
                        this.JBSC = true;
                    }
                    else
                    {
                        this.JBSC = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["JZDoctors"] != null)
                {
                    this.JZDoctors = ds.Tables[0].Rows[0]["JZDoctors"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Doctors"] != null)
                {
                    this.Doctors = ds.Tables[0].Rows[0]["Doctors"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Hospital"] != null)
                {
                    this.Hospital = ds.Tables[0].Rows[0]["Hospital"].ToString();
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
            strSql.Append(" FROM [NHS_YCF_FMJL] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
