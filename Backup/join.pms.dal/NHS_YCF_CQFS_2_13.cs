using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using UNV.Comm.DataBase;
using System.Data.SqlClient;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_YCF_CQFS_2_13。
    /// </summary>
    [Serializable]
    public partial class NHS_YCF_CQFS_2_13
    {
        public NHS_YCF_CQFS_2_13()
        { }
        #region Model
        private int _commid;
        private string _unvid;
        private int? _visititems;
        private DateTime? _visitdate;
        private int? _yunzhou;
        private string _complaint;
        private decimal? _weight;
        private decimal? _ckjc_gdgd;
        private decimal? _ckjc_fw;
        private string _ckjc_tfw;
        private decimal? _ckjc_txl;
        private string _ckjc_xl;
        private decimal? _xymax;
        private decimal? _xymin;
        private decimal? _xhdbz;
        private string _ndb;
        private string _qtfzjc;
        private bool _classify;
        private string _guide;
        private bool _zz_yw;
        private string _zz_yy;
        private string _zz_jg;
        private DateTime? _nextvisitdate;
        private string _doctor;
        private string _sfjg;
        private DateTime? _createdate = DateTime.Now;
        private int? _userid;
        private string _classyc;
        private string _guideqt;
        private bool _isgwcf;
        private string _zdyj;
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
        public int? YunZhou
        {
            set { _yunzhou = value; }
            get { return _yunzhou; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Complaint
        {
            set { _complaint = value; }
            get { return _complaint; }
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
        public decimal? Ckjc_Gdgd
        {
            set { _ckjc_gdgd = value; }
            get { return _ckjc_gdgd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Ckjc_Fw
        {
            set { _ckjc_fw = value; }
            get { return _ckjc_fw; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Ckjc_Tfw
        {
            set { _ckjc_tfw = value; }
            get { return _ckjc_tfw; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Ckjc_Txl
        {
            set { _ckjc_txl = value; }
            get { return _ckjc_txl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Ckjc_Xl
        {
            set { _ckjc_xl = value; }
            get { return _ckjc_xl; }
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
        public decimal? Xhdbz
        {
            set { _xhdbz = value; }
            get { return _xhdbz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Ndb
        {
            set { _ndb = value; }
            get { return _ndb; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qtfzjc
        {
            set { _qtfzjc = value; }
            get { return _qtfzjc; }
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
        public string SFJG
        {
            set { _sfjg = value; }
            get { return _sfjg; }
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
        public string ClassYC
        {
            set { _classyc = value; }
            get { return _classyc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GuideQT
        {
            set { _guideqt = value; }
            get { return _guideqt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsGWCF
        {
            set { _isgwcf = value; }
            get { return _isgwcf; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZDYJ
        {
            set { _zdyj = value; }
            get { return _zdyj; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NHS_YCF_CQFS_2_13(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommID,UnvID,VisitItems,VisitDate,YunZhou,Complaint,Weight,Ckjc_Gdgd,Ckjc_Fw,Ckjc_Tfw,Ckjc_Txl,Ckjc_Xl,XyMax,XyMin,Xhdbz,Ndb,Qtfzjc,Classify,Guide,Zz_Yw,Zz_Yy,Zz_Jg,NextVisitDate,Doctor,SFJG,CreateDate,UserID,ClassYC,GuideQT,IsGWCF,ZDYJ ");
            strSql.Append(" FROM [NHS_YCF_CQFS_2_13] ");
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
                if (ds.Tables[0].Rows[0]["YunZhou"] != null && ds.Tables[0].Rows[0]["YunZhou"].ToString() != "")
                {
                    this.YunZhou = int.Parse(ds.Tables[0].Rows[0]["YunZhou"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Complaint"] != null)
                {
                    this.Complaint = ds.Tables[0].Rows[0]["Complaint"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Weight"] != null && ds.Tables[0].Rows[0]["Weight"].ToString() != "")
                {
                    this.Weight = decimal.Parse(ds.Tables[0].Rows[0]["Weight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ckjc_Gdgd"] != null && ds.Tables[0].Rows[0]["Ckjc_Gdgd"].ToString() != "")
                {
                    this.Ckjc_Gdgd = decimal.Parse(ds.Tables[0].Rows[0]["Ckjc_Gdgd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ckjc_Fw"] != null && ds.Tables[0].Rows[0]["Ckjc_Fw"].ToString() != "")
                {
                    this.Ckjc_Fw = decimal.Parse(ds.Tables[0].Rows[0]["Ckjc_Fw"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ckjc_Tfw"] != null)
                {
                    this.Ckjc_Tfw = ds.Tables[0].Rows[0]["Ckjc_Tfw"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Ckjc_Txl"] != null && ds.Tables[0].Rows[0]["Ckjc_Txl"].ToString() != "")
                {
                    this.Ckjc_Txl = decimal.Parse(ds.Tables[0].Rows[0]["Ckjc_Txl"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ckjc_Xl"] != null)
                {
                    this.Ckjc_Xl = ds.Tables[0].Rows[0]["Ckjc_Xl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XyMax"] != null && ds.Tables[0].Rows[0]["XyMax"].ToString() != "")
                {
                    this.XyMax = decimal.Parse(ds.Tables[0].Rows[0]["XyMax"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XyMin"] != null && ds.Tables[0].Rows[0]["XyMin"].ToString() != "")
                {
                    this.XyMin = decimal.Parse(ds.Tables[0].Rows[0]["XyMin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Xhdbz"] != null && ds.Tables[0].Rows[0]["Xhdbz"].ToString() != "")
                {
                    this.Xhdbz = decimal.Parse(ds.Tables[0].Rows[0]["Xhdbz"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ndb"] != null)
                {
                    this.Ndb = ds.Tables[0].Rows[0]["Ndb"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qtfzjc"] != null)
                {
                    this.Qtfzjc = ds.Tables[0].Rows[0]["Qtfzjc"].ToString();
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
                if (ds.Tables[0].Rows[0]["SFJG"] != null)
                {
                    this.SFJG = ds.Tables[0].Rows[0]["SFJG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClassYC"] != null)
                {
                    this.ClassYC = ds.Tables[0].Rows[0]["ClassYC"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GuideQT"] != null)
                {
                    this.GuideQT = ds.Tables[0].Rows[0]["GuideQT"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsGWCF"] != null && ds.Tables[0].Rows[0]["IsGWCF"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsGWCF"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsGWCF"].ToString().ToLower() == "true"))
                    {
                        this.IsGWCF = true;
                    }
                    else
                    {
                        this.IsGWCF = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["ZDYJ"] != null)
                {
                    this.ZDYJ = ds.Tables[0].Rows[0]["ZDYJ"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_YCF_CQFS_2_13]");
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
            strSql.Append("select count(1) from [NHS_YCF_CQFS_2_13]");
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
            strSql.Append("insert into [NHS_YCF_CQFS_2_13] (");
            strSql.Append("UnvID,VisitItems,VisitDate,YunZhou,Complaint,Weight,Ckjc_Gdgd,Ckjc_Fw,Ckjc_Tfw,Ckjc_Txl,Ckjc_Xl,XyMax,XyMin,Xhdbz,Ndb,Qtfzjc,Classify,Guide,Zz_Yw,Zz_Yy,Zz_Jg,NextVisitDate,Doctor,SFJG,CreateDate,UserID,ClassYC,GuideQT,IsGWCF,ZDYJ)");
            strSql.Append(" values (");
            strSql.Append("@UnvID,@VisitItems,@VisitDate,@YunZhou,@Complaint,@Weight,@Ckjc_Gdgd,@Ckjc_Fw,@Ckjc_Tfw,@Ckjc_Txl,@Ckjc_Xl,@XyMax,@XyMin,@Xhdbz,@Ndb,@Qtfzjc,@Classify,@Guide,@Zz_Yw,@Zz_Yy,@Zz_Jg,@NextVisitDate,@Doctor,@SFJG,@CreateDate,@UserID,@ClassYC,@GuideQT,@IsGWCF,@ZDYJ)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@VisitItems", SqlDbType.Int,4),
					new SqlParameter("@VisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@YunZhou", SqlDbType.TinyInt,1),
					new SqlParameter("@Complaint", SqlDbType.NVarChar,200),
					new SqlParameter("@Weight", SqlDbType.Float,8),
					new SqlParameter("@Ckjc_Gdgd", SqlDbType.Float,8),
					new SqlParameter("@Ckjc_Fw", SqlDbType.Float,8),
					new SqlParameter("@Ckjc_Tfw", SqlDbType.VarChar,50),
					new SqlParameter("@Ckjc_Txl", SqlDbType.Float,8),
					new SqlParameter("@Ckjc_Xl", SqlDbType.VarChar,50),
					new SqlParameter("@XyMax", SqlDbType.Float,8),
					new SqlParameter("@XyMin", SqlDbType.Float,8),
					new SqlParameter("@Xhdbz", SqlDbType.Float,8),
					new SqlParameter("@Ndb", SqlDbType.VarChar,50),
					new SqlParameter("@Qtfzjc", SqlDbType.VarChar,50),
					new SqlParameter("@Classify", SqlDbType.Bit,1),
					new SqlParameter("@Guide", SqlDbType.VarChar,50),
					new SqlParameter("@Zz_Yw", SqlDbType.Bit,1),
					new SqlParameter("@Zz_Yy", SqlDbType.NVarChar,50),
					new SqlParameter("@Zz_Jg", SqlDbType.NVarChar,50),
					new SqlParameter("@NextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Doctor", SqlDbType.VarChar,50),
					new SqlParameter("@SFJG", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@ClassYC", SqlDbType.VarChar,50),
					new SqlParameter("@GuideQT", SqlDbType.VarChar,50),
					new SqlParameter("@IsGWCF", SqlDbType.Bit,1),
					new SqlParameter("@ZDYJ", SqlDbType.NVarChar,500)};
            parameters[0].Value = UnvID;
            parameters[1].Value = VisitItems;
            parameters[2].Value = VisitDate;
            parameters[3].Value = YunZhou;
            parameters[4].Value = Complaint;
            parameters[5].Value = Weight;
            parameters[6].Value = Ckjc_Gdgd;
            parameters[7].Value = Ckjc_Fw;
            parameters[8].Value = Ckjc_Tfw;
            parameters[9].Value = Ckjc_Txl;
            parameters[10].Value = Ckjc_Xl;
            parameters[11].Value = XyMax;
            parameters[12].Value = XyMin;
            parameters[13].Value = Xhdbz;
            parameters[14].Value = Ndb;
            parameters[15].Value = Qtfzjc;
            parameters[16].Value = Classify;
            parameters[17].Value = Guide;
            parameters[18].Value = Zz_Yw;
            parameters[19].Value = Zz_Yy;
            parameters[20].Value = Zz_Jg;
            parameters[21].Value = NextVisitDate;
            parameters[22].Value = Doctor;
            parameters[23].Value = SFJG;
            parameters[24].Value = CreateDate;
            parameters[25].Value = UserID;
            parameters[26].Value = ClassYC;
            parameters[27].Value = GuideQT;
            parameters[28].Value = IsGWCF;
            parameters[29].Value = ZDYJ;

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
            strSql.Append("update [NHS_YCF_CQFS_2_13] set ");
            strSql.Append("UnvID=@UnvID,");
            strSql.Append("VisitItems=@VisitItems,");
            strSql.Append("VisitDate=@VisitDate,");
            strSql.Append("YunZhou=@YunZhou,");
            strSql.Append("Complaint=@Complaint,");
            strSql.Append("Weight=@Weight,");
            strSql.Append("Ckjc_Gdgd=@Ckjc_Gdgd,");
            strSql.Append("Ckjc_Fw=@Ckjc_Fw,");
            strSql.Append("Ckjc_Tfw=@Ckjc_Tfw,");
            strSql.Append("Ckjc_Txl=@Ckjc_Txl,");
            strSql.Append("Ckjc_Xl=@Ckjc_Xl,");
            strSql.Append("XyMax=@XyMax,");
            strSql.Append("XyMin=@XyMin,");
            strSql.Append("Xhdbz=@Xhdbz,");
            strSql.Append("Ndb=@Ndb,");
            strSql.Append("Qtfzjc=@Qtfzjc,");
            strSql.Append("Classify=@Classify,");
            strSql.Append("Guide=@Guide,");
            strSql.Append("Zz_Yw=@Zz_Yw,");
            strSql.Append("Zz_Yy=@Zz_Yy,");
            strSql.Append("Zz_Jg=@Zz_Jg,");
            strSql.Append("NextVisitDate=@NextVisitDate,");
            strSql.Append("Doctor=@Doctor,");
            strSql.Append("SFJG=@SFJG,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("ClassYC=@ClassYC,");
            strSql.Append("GuideQT=@GuideQT,");
            strSql.Append("IsGWCF=@IsGWCF,");
            strSql.Append("ZDYJ=@ZDYJ");
            strSql.Append(" where CommID=@CommID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@VisitItems", SqlDbType.Int,4),
					new SqlParameter("@VisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@YunZhou", SqlDbType.TinyInt,1),
					new SqlParameter("@Complaint", SqlDbType.NVarChar,200),
					new SqlParameter("@Weight", SqlDbType.Float,8),
					new SqlParameter("@Ckjc_Gdgd", SqlDbType.Float,8),
					new SqlParameter("@Ckjc_Fw", SqlDbType.Float,8),
					new SqlParameter("@Ckjc_Tfw", SqlDbType.VarChar,50),
					new SqlParameter("@Ckjc_Txl", SqlDbType.Float,8),
					new SqlParameter("@Ckjc_Xl", SqlDbType.VarChar,50),
					new SqlParameter("@XyMax", SqlDbType.Float,8),
					new SqlParameter("@XyMin", SqlDbType.Float,8),
					new SqlParameter("@Xhdbz", SqlDbType.Float,8),
					new SqlParameter("@Ndb", SqlDbType.VarChar,50),
					new SqlParameter("@Qtfzjc", SqlDbType.VarChar,50),
					new SqlParameter("@Classify", SqlDbType.Bit,1),
					new SqlParameter("@Guide", SqlDbType.VarChar,50),
					new SqlParameter("@Zz_Yw", SqlDbType.Bit,1),
					new SqlParameter("@Zz_Yy", SqlDbType.NVarChar,50),
					new SqlParameter("@Zz_Jg", SqlDbType.NVarChar,50),
					new SqlParameter("@NextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Doctor", SqlDbType.VarChar,50),
					new SqlParameter("@SFJG", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@ClassYC", SqlDbType.VarChar,50),
					new SqlParameter("@GuideQT", SqlDbType.VarChar,50),
					new SqlParameter("@IsGWCF", SqlDbType.Bit,1),
					new SqlParameter("@ZDYJ", SqlDbType.NVarChar,500),
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = UnvID;
            parameters[1].Value = VisitItems;
            parameters[2].Value = VisitDate;
            parameters[3].Value = YunZhou;
            parameters[4].Value = Complaint;
            parameters[5].Value = Weight;
            parameters[6].Value = Ckjc_Gdgd;
            parameters[7].Value = Ckjc_Fw;
            parameters[8].Value = Ckjc_Tfw;
            parameters[9].Value = Ckjc_Txl;
            parameters[10].Value = Ckjc_Xl;
            parameters[11].Value = XyMax;
            parameters[12].Value = XyMin;
            parameters[13].Value = Xhdbz;
            parameters[14].Value = Ndb;
            parameters[15].Value = Qtfzjc;
            parameters[16].Value = Classify;
            parameters[17].Value = Guide;
            parameters[18].Value = Zz_Yw;
            parameters[19].Value = Zz_Yy;
            parameters[20].Value = Zz_Jg;
            parameters[21].Value = NextVisitDate;
            parameters[22].Value = Doctor;
            parameters[23].Value = SFJG;
            parameters[24].Value = CreateDate;
            parameters[25].Value = UserID;
            parameters[26].Value = ClassYC;
            parameters[27].Value = GuideQT;
            parameters[28].Value = IsGWCF;
            parameters[29].Value = ZDYJ;
            parameters[30].Value = CommID;

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
            strSql.Append("delete from [NHS_YCF_CQFS_2_13] ");
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
            strSql.Append("select CommID,UnvID,VisitItems,VisitDate,YunZhou,Complaint,Weight,Ckjc_Gdgd,Ckjc_Fw,Ckjc_Tfw,Ckjc_Txl,Ckjc_Xl,XyMax,XyMin,Xhdbz,Ndb,Qtfzjc,Classify,Guide,Zz_Yw,Zz_Yy,Zz_Jg,NextVisitDate,Doctor,SFJG,CreateDate,UserID,ClassYC,GuideQT,IsGWCF,ZDYJ ");
            strSql.Append(" FROM [NHS_YCF_CQFS_2_13] ");
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
                if (ds.Tables[0].Rows[0]["YunZhou"] != null && ds.Tables[0].Rows[0]["YunZhou"].ToString() != "")
                {
                    this.YunZhou = int.Parse(ds.Tables[0].Rows[0]["YunZhou"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Complaint"] != null)
                {
                    this.Complaint = ds.Tables[0].Rows[0]["Complaint"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Weight"] != null && ds.Tables[0].Rows[0]["Weight"].ToString() != "")
                {
                    this.Weight = decimal.Parse(ds.Tables[0].Rows[0]["Weight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ckjc_Gdgd"] != null && ds.Tables[0].Rows[0]["Ckjc_Gdgd"].ToString() != "")
                {
                    this.Ckjc_Gdgd = decimal.Parse(ds.Tables[0].Rows[0]["Ckjc_Gdgd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ckjc_Fw"] != null && ds.Tables[0].Rows[0]["Ckjc_Fw"].ToString() != "")
                {
                    this.Ckjc_Fw = decimal.Parse(ds.Tables[0].Rows[0]["Ckjc_Fw"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ckjc_Tfw"] != null)
                {
                    this.Ckjc_Tfw = ds.Tables[0].Rows[0]["Ckjc_Tfw"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Ckjc_Txl"] != null && ds.Tables[0].Rows[0]["Ckjc_Txl"].ToString() != "")
                {
                    this.Ckjc_Txl = decimal.Parse(ds.Tables[0].Rows[0]["Ckjc_Txl"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ckjc_Xl"] != null)
                {
                    this.Ckjc_Xl = ds.Tables[0].Rows[0]["Ckjc_Xl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XyMax"] != null && ds.Tables[0].Rows[0]["XyMax"].ToString() != "")
                {
                    this.XyMax = decimal.Parse(ds.Tables[0].Rows[0]["XyMax"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XyMin"] != null && ds.Tables[0].Rows[0]["XyMin"].ToString() != "")
                {
                    this.XyMin = decimal.Parse(ds.Tables[0].Rows[0]["XyMin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Xhdbz"] != null && ds.Tables[0].Rows[0]["Xhdbz"].ToString() != "")
                {
                    this.Xhdbz = decimal.Parse(ds.Tables[0].Rows[0]["Xhdbz"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ndb"] != null)
                {
                    this.Ndb = ds.Tables[0].Rows[0]["Ndb"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qtfzjc"] != null)
                {
                    this.Qtfzjc = ds.Tables[0].Rows[0]["Qtfzjc"].ToString();
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
                if (ds.Tables[0].Rows[0]["SFJG"] != null)
                {
                    this.SFJG = ds.Tables[0].Rows[0]["SFJG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClassYC"] != null)
                {
                    this.ClassYC = ds.Tables[0].Rows[0]["ClassYC"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GuideQT"] != null)
                {
                    this.GuideQT = ds.Tables[0].Rows[0]["GuideQT"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsGWCF"] != null && ds.Tables[0].Rows[0]["IsGWCF"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsGWCF"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsGWCF"].ToString().ToLower() == "true"))
                    {
                        this.IsGWCF = true;
                    }
                    else
                    {
                        this.IsGWCF = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["ZDYJ"] != null)
                {
                    this.ZDYJ = ds.Tables[0].Rows[0]["ZDYJ"].ToString();
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
            strSql.Append(" FROM [NHS_YCF_CQFS_2_13] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
