using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using UNV.Comm.DataBase;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_YCF_CHFS。
    /// </summary>
    [Serializable]
    public partial class NHS_YCF_CHFS
    {
        public NHS_YCF_CHFS()
        { }
        #region Model
        private int _commid;
        private string _unvid;
        private int? _visititems;
        private DateTime? _visitdate;
        private decimal? _temperature;
        private string _health;
        private string _psychologic;
        private decimal? _xymax;
        private decimal? _xymin;
        private bool _breast;
        private string _breastsm;
        private bool _lochia;
        private string _lochiasm;
        private bool _uterus;
        private string _uterussm;
        private bool _wound;
        private string _woundsm;
        private string _others;
        private string _classifysm;
        private bool _classify;
        private string _guide;
        private string _guideqt;
        private bool _isdone;
        private bool _zz_yw;
        private string _zz_yy;
        private string _zz_jg;
        private DateTime? _nextvisitdate;
        private string _doctor;
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
        /// 访视项目：1,2,产后42天
        /// </summary>
        public int? VisitItems
        {
            set { _visititems = value; }
            get { return _visititems; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? VisitDate
        {
            set { _visitdate = value; }
            get { return _visitdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Temperature
        {
            set { _temperature = value; }
            get { return _temperature; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Health
        {
            set { _health = value; }
            get { return _health; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Psychologic
        {
            set { _psychologic = value; }
            get { return _psychologic; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? XyMax
        {
            set { _xymax = value; }
            get { return _xymax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? XyMin
        {
            set { _xymin = value; }
            get { return _xymin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Breast
        {
            set { _breast = value; }
            get { return _breast; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BreastSm
        {
            set { _breastsm = value; }
            get { return _breastsm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Lochia
        {
            set { _lochia = value; }
            get { return _lochia; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LochiaSm
        {
            set { _lochiasm = value; }
            get { return _lochiasm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Uterus
        {
            set { _uterus = value; }
            get { return _uterus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UterusSm
        {
            set { _uterussm = value; }
            get { return _uterussm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Wound
        {
            set { _wound = value; }
            get { return _wound; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WoundSm
        {
            set { _woundsm = value; }
            get { return _woundsm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Others
        {
            set { _others = value; }
            get { return _others; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClassifySm
        {
            set { _classifysm = value; }
            get { return _classifysm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Classify
        {
            set { _classify = value; }
            get { return _classify; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Guide
        {
            set { _guide = value; }
            get { return _guide; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GuideQt
        {
            set { _guideqt = value; }
            get { return _guideqt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDone
        {
            set { _isdone = value; }
            get { return _isdone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Zz_Yw
        {
            set { _zz_yw = value; }
            get { return _zz_yw; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Zz_Yy
        {
            set { _zz_yy = value; }
            get { return _zz_yy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Zz_Jg
        {
            set { _zz_jg = value; }
            get { return _zz_jg; }
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
        public string Doctor
        {
            set { _doctor = value; }
            get { return _doctor; }
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
        public NHS_YCF_CHFS(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommID,UnvID,VisitItems,VisitDate,Temperature,Health,Psychologic,XyMax,XyMin,Breast,BreastSm,Lochia,LochiaSm,Uterus,UterusSm,Wound,WoundSm,Others,ClassifySm,Classify,Guide,GuideQt,IsDone,Zz_Yw,Zz_Yy,Zz_Jg,NextVisitDate,Doctor,CreateDate,UserID ");
            strSql.Append(" FROM [NHS_YCF_CHFS] ");
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
                if (ds.Tables[0].Rows[0]["VisitItems"] != null && ds.Tables[0].Rows[0]["VisitItems"].ToString() != "")
                {
                    this.VisitItems = int.Parse(ds.Tables[0].Rows[0]["VisitItems"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VisitDate"] != null && ds.Tables[0].Rows[0]["VisitDate"].ToString() != "")
                {
                    this.VisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["VisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Temperature"] != null && ds.Tables[0].Rows[0]["Temperature"].ToString() != "")
                {
                    this.Temperature = decimal.Parse(ds.Tables[0].Rows[0]["Temperature"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Health"] != null)
                {
                    this.Health = ds.Tables[0].Rows[0]["Health"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Psychologic"] != null)
                {
                    this.Psychologic = ds.Tables[0].Rows[0]["Psychologic"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XyMax"] != null && ds.Tables[0].Rows[0]["XyMax"].ToString() != "")
                {
                    this.XyMax = decimal.Parse(ds.Tables[0].Rows[0]["XyMax"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XyMin"] != null && ds.Tables[0].Rows[0]["XyMin"].ToString() != "")
                {
                    this.XyMin = decimal.Parse(ds.Tables[0].Rows[0]["XyMin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Breast"] != null && ds.Tables[0].Rows[0]["Breast"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Breast"].ToString() == "1") || (ds.Tables[0].Rows[0]["Breast"].ToString().ToLower() == "true"))
                    {
                        this.Breast = true;
                    }
                    else
                    {
                        this.Breast = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["BreastSm"] != null)
                {
                    this.BreastSm = ds.Tables[0].Rows[0]["BreastSm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Lochia"] != null && ds.Tables[0].Rows[0]["Lochia"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Lochia"].ToString() == "1") || (ds.Tables[0].Rows[0]["Lochia"].ToString().ToLower() == "true"))
                    {
                        this.Lochia = true;
                    }
                    else
                    {
                        this.Lochia = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["LochiaSm"] != null)
                {
                    this.LochiaSm = ds.Tables[0].Rows[0]["LochiaSm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Uterus"] != null && ds.Tables[0].Rows[0]["Uterus"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Uterus"].ToString() == "1") || (ds.Tables[0].Rows[0]["Uterus"].ToString().ToLower() == "true"))
                    {
                        this.Uterus = true;
                    }
                    else
                    {
                        this.Uterus = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["UterusSm"] != null)
                {
                    this.UterusSm = ds.Tables[0].Rows[0]["UterusSm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Wound"] != null && ds.Tables[0].Rows[0]["Wound"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Wound"].ToString() == "1") || (ds.Tables[0].Rows[0]["Wound"].ToString().ToLower() == "true"))
                    {
                        this.Wound = true;
                    }
                    else
                    {
                        this.Wound = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["WoundSm"] != null)
                {
                    this.WoundSm = ds.Tables[0].Rows[0]["WoundSm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Others"] != null)
                {
                    this.Others = ds.Tables[0].Rows[0]["Others"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ClassifySm"] != null)
                {
                    this.ClassifySm = ds.Tables[0].Rows[0]["ClassifySm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Classify"] != null && ds.Tables[0].Rows[0]["Classify"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Classify"].ToString() == "1") || (ds.Tables[0].Rows[0]["Classify"].ToString().ToLower() == "true"))
                    {
                        this.Classify = true;
                    }
                    else
                    {
                        this.Classify = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Guide"] != null)
                {
                    this.Guide = ds.Tables[0].Rows[0]["Guide"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GuideQt"] != null)
                {
                    this.GuideQt = ds.Tables[0].Rows[0]["GuideQt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsDone"] != null && ds.Tables[0].Rows[0]["IsDone"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsDone"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsDone"].ToString().ToLower() == "true"))
                    {
                        this.IsDone = true;
                    }
                    else
                    {
                        this.IsDone = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Zz_Yw"] != null && ds.Tables[0].Rows[0]["Zz_Yw"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Zz_Yw"].ToString() == "1") || (ds.Tables[0].Rows[0]["Zz_Yw"].ToString().ToLower() == "true"))
                    {
                        this.Zz_Yw = true;
                    }
                    else
                    {
                        this.Zz_Yw = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Zz_Yy"] != null)
                {
                    this.Zz_Yy = ds.Tables[0].Rows[0]["Zz_Yy"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zz_Jg"] != null)
                {
                    this.Zz_Jg = ds.Tables[0].Rows[0]["Zz_Jg"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NextVisitDate"] != null && ds.Tables[0].Rows[0]["NextVisitDate"].ToString() != "")
                {
                    this.NextVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["NextVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Doctor"] != null)
                {
                    this.Doctor = ds.Tables[0].Rows[0]["Doctor"].ToString();
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
            strSql.Append("select count(1) from [NHS_YCF_CHFS]");
            strSql.Append(" where CommID=@CommID ");

            SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = CommID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string UnvID, string VisitItems, bool isEdit, int commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_YCF_CHFS]");
            strSql.AppendFormat(" where UnvID='{0}' and VisitItems={1} ", UnvID, VisitItems);
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
            strSql.Append("insert into [NHS_YCF_CHFS] (");
            strSql.Append("UnvID,VisitItems,VisitDate,Temperature,Health,Psychologic,XyMax,XyMin,Breast,BreastSm,Lochia,LochiaSm,Uterus,UterusSm,Wound,WoundSm,Others,ClassifySm,Classify,Guide,GuideQt,IsDone,Zz_Yw,Zz_Yy,Zz_Jg,NextVisitDate,Doctor,CreateDate,UserID)");
            strSql.Append(" values (");
            strSql.Append("@UnvID,@VisitItems,@VisitDate,@Temperature,@Health,@Psychologic,@XyMax,@XyMin,@Breast,@BreastSm,@Lochia,@LochiaSm,@Uterus,@UterusSm,@Wound,@WoundSm,@Others,@ClassifySm,@Classify,@Guide,@GuideQt,@IsDone,@Zz_Yw,@Zz_Yy,@Zz_Jg,@NextVisitDate,@Doctor,@CreateDate,@UserID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@VisitItems", SqlDbType.Int,4),
					new SqlParameter("@VisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Temperature", SqlDbType.Float,8),
					new SqlParameter("@Health", SqlDbType.NVarChar,50),
					new SqlParameter("@Psychologic", SqlDbType.NVarChar,50),
					new SqlParameter("@XyMax", SqlDbType.Float,8),
					new SqlParameter("@XyMin", SqlDbType.Float,8),
					new SqlParameter("@Breast", SqlDbType.Bit,1),
					new SqlParameter("@BreastSm", SqlDbType.VarChar,50),
					new SqlParameter("@Lochia", SqlDbType.Bit,1),
					new SqlParameter("@LochiaSm", SqlDbType.VarChar,50),
					new SqlParameter("@Uterus", SqlDbType.Bit,1),
					new SqlParameter("@UterusSm", SqlDbType.VarChar,50),
					new SqlParameter("@Wound", SqlDbType.Bit,1),
					new SqlParameter("@WoundSm", SqlDbType.VarChar,50),
					new SqlParameter("@Others", SqlDbType.VarChar,50),
					new SqlParameter("@ClassifySm", SqlDbType.VarChar,50),
					new SqlParameter("@Classify", SqlDbType.Bit,1),
					new SqlParameter("@Guide", SqlDbType.VarChar,50),
					new SqlParameter("@GuideQt", SqlDbType.VarChar,50),
					new SqlParameter("@IsDone", SqlDbType.Bit,1),
					new SqlParameter("@Zz_Yw", SqlDbType.Bit,1),
					new SqlParameter("@Zz_Yy", SqlDbType.NVarChar,50),
					new SqlParameter("@Zz_Jg", SqlDbType.NVarChar,50),
					new SqlParameter("@NextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Doctor", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = UnvID;
            parameters[1].Value = VisitItems;
            parameters[2].Value = VisitDate;
            parameters[3].Value = Temperature;
            parameters[4].Value = Health;
            parameters[5].Value = Psychologic;
            parameters[6].Value = XyMax;
            parameters[7].Value = XyMin;
            parameters[8].Value = Breast;
            parameters[9].Value = BreastSm;
            parameters[10].Value = Lochia;
            parameters[11].Value = LochiaSm;
            parameters[12].Value = Uterus;
            parameters[13].Value = UterusSm;
            parameters[14].Value = Wound;
            parameters[15].Value = WoundSm;
            parameters[16].Value = Others;
            parameters[17].Value = ClassifySm;
            parameters[18].Value = Classify;
            parameters[19].Value = Guide;
            parameters[20].Value = GuideQt;
            parameters[21].Value = IsDone;
            parameters[22].Value = Zz_Yw;
            parameters[23].Value = Zz_Yy;
            parameters[24].Value = Zz_Jg;
            parameters[25].Value = NextVisitDate;
            parameters[26].Value = Doctor;
            parameters[27].Value = CreateDate;
            parameters[28].Value = UserID;

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
            strSql.Append("update [NHS_YCF_CHFS] set ");
            strSql.Append("UnvID=@UnvID,");
            strSql.Append("VisitItems=@VisitItems,");
            strSql.Append("VisitDate=@VisitDate,");
            strSql.Append("Temperature=@Temperature,");
            strSql.Append("Health=@Health,");
            strSql.Append("Psychologic=@Psychologic,");
            strSql.Append("XyMax=@XyMax,");
            strSql.Append("XyMin=@XyMin,");
            strSql.Append("Breast=@Breast,");
            strSql.Append("BreastSm=@BreastSm,");
            strSql.Append("Lochia=@Lochia,");
            strSql.Append("LochiaSm=@LochiaSm,");
            strSql.Append("Uterus=@Uterus,");
            strSql.Append("UterusSm=@UterusSm,");
            strSql.Append("Wound=@Wound,");
            strSql.Append("WoundSm=@WoundSm,");
            strSql.Append("Others=@Others,");
            strSql.Append("ClassifySm=@ClassifySm,");
            strSql.Append("Classify=@Classify,");
            strSql.Append("Guide=@Guide,");
            strSql.Append("GuideQt=@GuideQt,");
            strSql.Append("IsDone=@IsDone,");
            strSql.Append("Zz_Yw=@Zz_Yw,");
            strSql.Append("Zz_Yy=@Zz_Yy,");
            strSql.Append("Zz_Jg=@Zz_Jg,");
            strSql.Append("NextVisitDate=@NextVisitDate,");
            strSql.Append("Doctor=@Doctor,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UserID=@UserID");
            strSql.Append(" where CommID=@CommID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@VisitItems", SqlDbType.Int,4),
					new SqlParameter("@VisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Temperature", SqlDbType.Float,8),
					new SqlParameter("@Health", SqlDbType.NVarChar,50),
					new SqlParameter("@Psychologic", SqlDbType.NVarChar,50),
					new SqlParameter("@XyMax", SqlDbType.Float,8),
					new SqlParameter("@XyMin", SqlDbType.Float,8),
					new SqlParameter("@Breast", SqlDbType.Bit,1),
					new SqlParameter("@BreastSm", SqlDbType.VarChar,50),
					new SqlParameter("@Lochia", SqlDbType.Bit,1),
					new SqlParameter("@LochiaSm", SqlDbType.VarChar,50),
					new SqlParameter("@Uterus", SqlDbType.Bit,1),
					new SqlParameter("@UterusSm", SqlDbType.VarChar,50),
					new SqlParameter("@Wound", SqlDbType.Bit,1),
					new SqlParameter("@WoundSm", SqlDbType.VarChar,50),
					new SqlParameter("@Others", SqlDbType.VarChar,50),
					new SqlParameter("@ClassifySm", SqlDbType.VarChar,50),
					new SqlParameter("@Classify", SqlDbType.Bit,1),
					new SqlParameter("@Guide", SqlDbType.VarChar,50),
					new SqlParameter("@GuideQt", SqlDbType.VarChar,50),
					new SqlParameter("@IsDone", SqlDbType.Bit,1),
					new SqlParameter("@Zz_Yw", SqlDbType.Bit,1),
					new SqlParameter("@Zz_Yy", SqlDbType.NVarChar,50),
					new SqlParameter("@Zz_Jg", SqlDbType.NVarChar,50),
					new SqlParameter("@NextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Doctor", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = UnvID;
            parameters[1].Value = VisitItems;
            parameters[2].Value = VisitDate;
            parameters[3].Value = Temperature;
            parameters[4].Value = Health;
            parameters[5].Value = Psychologic;
            parameters[6].Value = XyMax;
            parameters[7].Value = XyMin;
            parameters[8].Value = Breast;
            parameters[9].Value = BreastSm;
            parameters[10].Value = Lochia;
            parameters[11].Value = LochiaSm;
            parameters[12].Value = Uterus;
            parameters[13].Value = UterusSm;
            parameters[14].Value = Wound;
            parameters[15].Value = WoundSm;
            parameters[16].Value = Others;
            parameters[17].Value = ClassifySm;
            parameters[18].Value = Classify;
            parameters[19].Value = Guide;
            parameters[20].Value = GuideQt;
            parameters[21].Value = IsDone;
            parameters[22].Value = Zz_Yw;
            parameters[23].Value = Zz_Yy;
            parameters[24].Value = Zz_Jg;
            parameters[25].Value = NextVisitDate;
            parameters[26].Value = Doctor;
            parameters[27].Value = CreateDate;
            parameters[28].Value = UserID;
            parameters[29].Value = CommID;

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
            strSql.Append("delete from [NHS_YCF_CHFS] ");
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
            strSql.Append("select CommID,UnvID,VisitItems,VisitDate,Temperature,Health,Psychologic,XyMax,XyMin,Breast,BreastSm,Lochia,LochiaSm,Uterus,UterusSm,Wound,WoundSm,Others,ClassifySm,Classify,Guide,GuideQt,IsDone,Zz_Yw,Zz_Yy,Zz_Jg,NextVisitDate,Doctor,CreateDate,UserID ");
            strSql.Append(" FROM [NHS_YCF_CHFS] ");
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
                if (ds.Tables[0].Rows[0]["VisitItems"] != null && ds.Tables[0].Rows[0]["VisitItems"].ToString() != "")
                {
                    this.VisitItems = int.Parse(ds.Tables[0].Rows[0]["VisitItems"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VisitDate"] != null && ds.Tables[0].Rows[0]["VisitDate"].ToString() != "")
                {
                    this.VisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["VisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Temperature"] != null && ds.Tables[0].Rows[0]["Temperature"].ToString() != "")
                {
                    this.Temperature = decimal.Parse(ds.Tables[0].Rows[0]["Temperature"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Health"] != null)
                {
                    this.Health = ds.Tables[0].Rows[0]["Health"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Psychologic"] != null)
                {
                    this.Psychologic = ds.Tables[0].Rows[0]["Psychologic"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XyMax"] != null && ds.Tables[0].Rows[0]["XyMax"].ToString() != "")
                {
                    this.XyMax = decimal.Parse(ds.Tables[0].Rows[0]["XyMax"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XyMin"] != null && ds.Tables[0].Rows[0]["XyMin"].ToString() != "")
                {
                    this.XyMin = decimal.Parse(ds.Tables[0].Rows[0]["XyMin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Breast"] != null && ds.Tables[0].Rows[0]["Breast"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Breast"].ToString() == "1") || (ds.Tables[0].Rows[0]["Breast"].ToString().ToLower() == "true"))
                    {
                        this.Breast = true;
                    }
                    else
                    {
                        this.Breast = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["BreastSm"] != null)
                {
                    this.BreastSm = ds.Tables[0].Rows[0]["BreastSm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Lochia"] != null && ds.Tables[0].Rows[0]["Lochia"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Lochia"].ToString() == "1") || (ds.Tables[0].Rows[0]["Lochia"].ToString().ToLower() == "true"))
                    {
                        this.Lochia = true;
                    }
                    else
                    {
                        this.Lochia = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["LochiaSm"] != null)
                {
                    this.LochiaSm = ds.Tables[0].Rows[0]["LochiaSm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Uterus"] != null && ds.Tables[0].Rows[0]["Uterus"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Uterus"].ToString() == "1") || (ds.Tables[0].Rows[0]["Uterus"].ToString().ToLower() == "true"))
                    {
                        this.Uterus = true;
                    }
                    else
                    {
                        this.Uterus = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["UterusSm"] != null)
                {
                    this.UterusSm = ds.Tables[0].Rows[0]["UterusSm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Wound"] != null && ds.Tables[0].Rows[0]["Wound"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Wound"].ToString() == "1") || (ds.Tables[0].Rows[0]["Wound"].ToString().ToLower() == "true"))
                    {
                        this.Wound = true;
                    }
                    else
                    {
                        this.Wound = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["WoundSm"] != null)
                {
                    this.WoundSm = ds.Tables[0].Rows[0]["WoundSm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Others"] != null)
                {
                    this.Others = ds.Tables[0].Rows[0]["Others"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ClassifySm"] != null)
                {
                    this.ClassifySm = ds.Tables[0].Rows[0]["ClassifySm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Classify"] != null && ds.Tables[0].Rows[0]["Classify"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Classify"].ToString() == "1") || (ds.Tables[0].Rows[0]["Classify"].ToString().ToLower() == "true"))
                    {
                        this.Classify = true;
                    }
                    else
                    {
                        this.Classify = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Guide"] != null)
                {
                    this.Guide = ds.Tables[0].Rows[0]["Guide"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GuideQt"] != null)
                {
                    this.GuideQt = ds.Tables[0].Rows[0]["GuideQt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsDone"] != null && ds.Tables[0].Rows[0]["IsDone"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsDone"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsDone"].ToString().ToLower() == "true"))
                    {
                        this.IsDone = true;
                    }
                    else
                    {
                        this.IsDone = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Zz_Yw"] != null && ds.Tables[0].Rows[0]["Zz_Yw"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Zz_Yw"].ToString() == "1") || (ds.Tables[0].Rows[0]["Zz_Yw"].ToString().ToLower() == "true"))
                    {
                        this.Zz_Yw = true;
                    }
                    else
                    {
                        this.Zz_Yw = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Zz_Yy"] != null)
                {
                    this.Zz_Yy = ds.Tables[0].Rows[0]["Zz_Yy"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zz_Jg"] != null)
                {
                    this.Zz_Jg = ds.Tables[0].Rows[0]["Zz_Jg"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NextVisitDate"] != null && ds.Tables[0].Rows[0]["NextVisitDate"].ToString() != "")
                {
                    this.NextVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["NextVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Doctor"] != null)
                {
                    this.Doctor = ds.Tables[0].Rows[0]["Doctor"].ToString();
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
            strSql.Append(" FROM [NHS_YCF_CHFS] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
