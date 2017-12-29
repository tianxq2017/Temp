using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using UNV.Comm.DataBase;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_Child_XsrFs。
    /// </summary>
    [Serializable]
    public partial class NHS_Child_XsrFs
    {
        public NHS_Child_XsrFs()
        { }
        #region Model
        private int _commid;
        private string _unvid;
        private string _fathername;
        private string _fatherjob;
        private string _fathertel;
        private DateTime? _fatherbirthday;
        private string _mothername;
        private string _motherjob;
        private string _mothertel;
        private DateTime? _motherbirthday;
        private int? _birthyz;
        private string _motherhbqk;
        private string _motherhbqk_other;
        private string _zcjgname;
        private string _birthqk;
        private string _birthqk_other;
        private int? _xsezx;
        private string _apgarscore;
        private int? _jixing;
        private string _jixing_other;
        private int? _hearing;
        private int? _disease;
        private string _disease_other;
        private decimal? _weightborn;
        private decimal? _weightnow;
        private decimal? _heightborn;
        private int? _feedingmode;
        private decimal? _feedingamount;
        private int? _feedingnum;
        private int? _vomit;
        private int? _shit;
        private int? _shitnum;
        private decimal? _temperature;
        private int? _pr;
        private int? _breathingrate;
        private int? _face;
        private string _face_other;
        private int? _huangdan;
        private int? _qx;
        private decimal? _qxcmx;
        private decimal? _qxcm;
        private int? _qxstatus;
        private string _qxstatus_other;
        private int? _eyes;
        private string _eyes_other;
        private int? _limbs;
        private string _limbs_other;
        private int? _ears;
        private string _ears_other;
        private int? _neck;
        private string _neck_other;
        private int? _nose;
        private string _nose_other;
        private int? _skin;
        private string _skin_other;
        private int? _mouth;
        private string _mouth_other;
        private int? _anus;
        private string _anus_other;
        private int? _heartlung;
        private string _heartlung_other;
        private int? _genital;
        private string _genital_other;
        private int? _abdomen;
        private string _abdomen_other;
        private int? _spine;
        private string _spine_other;
        private int? _navel;
        private string _navel_other;
        private int? _zz_jy;
        private string _zz_yy;
        private string _zz_jg;
        private string _guide;
        private string _guide_other;
        private DateTime? _visitdate;
        private DateTime? _nextvisitdate;
        private string _nextvisitaddress;
        private string _doctor;
        private string _sfjg;
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
        public string FatherName
        {
            set { _fathername = value; }
            get { return _fathername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FatherJob
        {
            set { _fatherjob = value; }
            get { return _fatherjob; }
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
        public DateTime? FatherBirthDay
        {
            set { _fatherbirthday = value; }
            get { return _fatherbirthday; }
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
        public string MotherJob
        {
            set { _motherjob = value; }
            get { return _motherjob; }
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
        public DateTime? MotherBirthDay
        {
            set { _motherbirthday = value; }
            get { return _motherbirthday; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? BirthYz
        {
            set { _birthyz = value; }
            get { return _birthyz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MotherHbqk
        {
            set { _motherhbqk = value; }
            get { return _motherhbqk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MotherHbqk_other
        {
            set { _motherhbqk_other = value; }
            get { return _motherhbqk_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZcjgName
        {
            set { _zcjgname = value; }
            get { return _zcjgname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BirthQk
        {
            set { _birthqk = value; }
            get { return _birthqk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BirthQk_other
        {
            set { _birthqk_other = value; }
            get { return _birthqk_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? XseZx
        {
            set { _xsezx = value; }
            get { return _xsezx; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ApgarScore
        {
            set { _apgarscore = value; }
            get { return _apgarscore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? JiXing
        {
            set { _jixing = value; }
            get { return _jixing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JiXing_other
        {
            set { _jixing_other = value; }
            get { return _jixing_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Hearing
        {
            set { _hearing = value; }
            get { return _hearing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Disease
        {
            set { _disease = value; }
            get { return _disease; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Disease_other
        {
            set { _disease_other = value; }
            get { return _disease_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? WeightBorn
        {
            set { _weightborn = value; }
            get { return _weightborn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? WeightNow
        {
            set { _weightnow = value; }
            get { return _weightnow; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? HeightBorn
        {
            set { _heightborn = value; }
            get { return _heightborn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FeedingMode
        {
            set { _feedingmode = value; }
            get { return _feedingmode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FeedingAmount
        {
            set { _feedingamount = value; }
            get { return _feedingamount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FeedingNum
        {
            set { _feedingnum = value; }
            get { return _feedingnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Vomit
        {
            set { _vomit = value; }
            get { return _vomit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Shit
        {
            set { _shit = value; }
            get { return _shit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ShitNum
        {
            set { _shitnum = value; }
            get { return _shitnum; }
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
        public int? PR
        {
            set { _pr = value; }
            get { return _pr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? BreathingRate
        {
            set { _breathingrate = value; }
            get { return _breathingrate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Face
        {
            set { _face = value; }
            get { return _face; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Face_other
        {
            set { _face_other = value; }
            get { return _face_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? HuangDan
        {
            set { _huangdan = value; }
            get { return _huangdan; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qx
        {
            set { _qx = value; }
            get { return _qx; }
        }
        /// <summary>
        /// 前囟
        /// </summary>
        public decimal? QxCmx
        {
            set { _qxcmx = value; }
            get { return _qxcmx; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? QxCm
        {
            set { _qxcm = value; }
            get { return _qxcm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? QxStatus
        {
            set { _qxstatus = value; }
            get { return _qxstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QxStatus_other
        {
            set { _qxstatus_other = value; }
            get { return _qxstatus_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Eyes
        {
            set { _eyes = value; }
            get { return _eyes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Eyes_other
        {
            set { _eyes_other = value; }
            get { return _eyes_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Limbs
        {
            set { _limbs = value; }
            get { return _limbs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Limbs_other
        {
            set { _limbs_other = value; }
            get { return _limbs_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Ears
        {
            set { _ears = value; }
            get { return _ears; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Ears_other
        {
            set { _ears_other = value; }
            get { return _ears_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Neck
        {
            set { _neck = value; }
            get { return _neck; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Neck_other
        {
            set { _neck_other = value; }
            get { return _neck_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Nose
        {
            set { _nose = value; }
            get { return _nose; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Nose_other
        {
            set { _nose_other = value; }
            get { return _nose_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Skin
        {
            set { _skin = value; }
            get { return _skin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Skin_other
        {
            set { _skin_other = value; }
            get { return _skin_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Mouth
        {
            set { _mouth = value; }
            get { return _mouth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Mouth_other
        {
            set { _mouth_other = value; }
            get { return _mouth_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Anus
        {
            set { _anus = value; }
            get { return _anus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Anus_other
        {
            set { _anus_other = value; }
            get { return _anus_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? HeartLung
        {
            set { _heartlung = value; }
            get { return _heartlung; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HeartLung_other
        {
            set { _heartlung_other = value; }
            get { return _heartlung_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Genital
        {
            set { _genital = value; }
            get { return _genital; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Genital_other
        {
            set { _genital_other = value; }
            get { return _genital_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Abdomen
        {
            set { _abdomen = value; }
            get { return _abdomen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Abdomen_other
        {
            set { _abdomen_other = value; }
            get { return _abdomen_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Spine
        {
            set { _spine = value; }
            get { return _spine; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Spine_other
        {
            set { _spine_other = value; }
            get { return _spine_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Navel
        {
            set { _navel = value; }
            get { return _navel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Navel_other
        {
            set { _navel_other = value; }
            get { return _navel_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Zz_Jy
        {
            set { _zz_jy = value; }
            get { return _zz_jy; }
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
        public string Guide
        {
            set { _guide = value; }
            get { return _guide; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Guide_other
        {
            set { _guide_other = value; }
            get { return _guide_other; }
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
        public DateTime? NextVisitDate
        {
            set { _nextvisitdate = value; }
            get { return _nextvisitdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NextVisitAddress
        {
            set { _nextvisitaddress = value; }
            get { return _nextvisitaddress; }
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
        public string Sfjg
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
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NHS_Child_XsrFs(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommID,UnvID,FatherName,FatherJob,FatherTel,FatherBirthDay,MotherName,MotherJob,MotherTel,MotherBirthDay,BirthYz,MotherHbqk,MotherHbqk_other,ZcjgName,BirthQk,BirthQk_other,XseZx,ApgarScore,JiXing,JiXing_other,Hearing,Disease,Disease_other,WeightBorn,WeightNow,HeightBorn,FeedingMode,FeedingAmount,FeedingNum,Vomit,Shit,ShitNum,Temperature,PR,BreathingRate,Face,Face_other,HuangDan,Qx,QxCmx,QxCm,QxStatus,QxStatus_other,Eyes,Eyes_other,Limbs,Limbs_other,Ears,Ears_other,Neck,Neck_other,Nose,Nose_other,Skin,Skin_other,Mouth,Mouth_other,Anus,Anus_other,HeartLung,HeartLung_other,Genital,Genital_other,Abdomen,Abdomen_other,Spine,Spine_other,Navel,Navel_other,Zz_Jy,Zz_Yy,Zz_Jg,Guide,Guide_other,VisitDate,NextVisitDate,NextVisitAddress,Doctor,Sfjg,CreateDate,UserID ");
            strSql.Append(" FROM [NHS_Child_XsrFs] ");
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
                if (ds.Tables[0].Rows[0]["FatherName"] != null)
                {
                    this.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FatherJob"] != null)
                {
                    this.FatherJob = ds.Tables[0].Rows[0]["FatherJob"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FatherTel"] != null)
                {
                    this.FatherTel = ds.Tables[0].Rows[0]["FatherTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FatherBirthDay"] != null && ds.Tables[0].Rows[0]["FatherBirthDay"].ToString() != "")
                {
                    this.FatherBirthDay = DateTime.Parse(ds.Tables[0].Rows[0]["FatherBirthDay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MotherName"] != null)
                {
                    this.MotherName = ds.Tables[0].Rows[0]["MotherName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherJob"] != null)
                {
                    this.MotherJob = ds.Tables[0].Rows[0]["MotherJob"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherTel"] != null)
                {
                    this.MotherTel = ds.Tables[0].Rows[0]["MotherTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherBirthDay"] != null && ds.Tables[0].Rows[0]["MotherBirthDay"].ToString() != "")
                {
                    this.MotherBirthDay = DateTime.Parse(ds.Tables[0].Rows[0]["MotherBirthDay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BirthYz"] != null && ds.Tables[0].Rows[0]["BirthYz"].ToString() != "")
                {
                    this.BirthYz = int.Parse(ds.Tables[0].Rows[0]["BirthYz"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MotherHbqk"] != null)
                {
                    this.MotherHbqk = ds.Tables[0].Rows[0]["MotherHbqk"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherHbqk_other"] != null)
                {
                    this.MotherHbqk_other = ds.Tables[0].Rows[0]["MotherHbqk_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZcjgName"] != null)
                {
                    this.ZcjgName = ds.Tables[0].Rows[0]["ZcjgName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BirthQk"] != null)
                {
                    this.BirthQk = ds.Tables[0].Rows[0]["BirthQk"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BirthQk_other"] != null)
                {
                    this.BirthQk_other = ds.Tables[0].Rows[0]["BirthQk_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XseZx"] != null && ds.Tables[0].Rows[0]["XseZx"].ToString() != "")
                {
                    this.XseZx = int.Parse(ds.Tables[0].Rows[0]["XseZx"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ApgarScore"] != null)
                {
                    this.ApgarScore = ds.Tables[0].Rows[0]["ApgarScore"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JiXing"] != null && ds.Tables[0].Rows[0]["JiXing"].ToString() != "")
                {
                    this.JiXing = int.Parse(ds.Tables[0].Rows[0]["JiXing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JiXing_other"] != null)
                {
                    this.JiXing_other = ds.Tables[0].Rows[0]["JiXing_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Hearing"] != null && ds.Tables[0].Rows[0]["Hearing"].ToString() != "")
                {
                    this.Hearing = int.Parse(ds.Tables[0].Rows[0]["Hearing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Disease"] != null && ds.Tables[0].Rows[0]["Disease"].ToString() != "")
                {
                    this.Disease = int.Parse(ds.Tables[0].Rows[0]["Disease"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Disease_other"] != null)
                {
                    this.Disease_other = ds.Tables[0].Rows[0]["Disease_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WeightBorn"] != null && ds.Tables[0].Rows[0]["WeightBorn"].ToString() != "")
                {
                    this.WeightBorn = decimal.Parse(ds.Tables[0].Rows[0]["WeightBorn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WeightNow"] != null && ds.Tables[0].Rows[0]["WeightNow"].ToString() != "")
                {
                    this.WeightNow = decimal.Parse(ds.Tables[0].Rows[0]["WeightNow"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HeightBorn"] != null && ds.Tables[0].Rows[0]["HeightBorn"].ToString() != "")
                {
                    this.HeightBorn = decimal.Parse(ds.Tables[0].Rows[0]["HeightBorn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FeedingMode"] != null && ds.Tables[0].Rows[0]["FeedingMode"].ToString() != "")
                {
                    this.FeedingMode = int.Parse(ds.Tables[0].Rows[0]["FeedingMode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FeedingAmount"] != null && ds.Tables[0].Rows[0]["FeedingAmount"].ToString() != "")
                {
                    this.FeedingAmount = decimal.Parse(ds.Tables[0].Rows[0]["FeedingAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FeedingNum"] != null && ds.Tables[0].Rows[0]["FeedingNum"].ToString() != "")
                {
                    this.FeedingNum = int.Parse(ds.Tables[0].Rows[0]["FeedingNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Vomit"] != null && ds.Tables[0].Rows[0]["Vomit"].ToString() != "")
                {
                    this.Vomit = int.Parse(ds.Tables[0].Rows[0]["Vomit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Shit"] != null && ds.Tables[0].Rows[0]["Shit"].ToString() != "")
                {
                    this.Shit = int.Parse(ds.Tables[0].Rows[0]["Shit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ShitNum"] != null && ds.Tables[0].Rows[0]["ShitNum"].ToString() != "")
                {
                    this.ShitNum = int.Parse(ds.Tables[0].Rows[0]["ShitNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Temperature"] != null && ds.Tables[0].Rows[0]["Temperature"].ToString() != "")
                {
                    this.Temperature = decimal.Parse(ds.Tables[0].Rows[0]["Temperature"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PR"] != null && ds.Tables[0].Rows[0]["PR"].ToString() != "")
                {
                    this.PR = int.Parse(ds.Tables[0].Rows[0]["PR"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BreathingRate"] != null && ds.Tables[0].Rows[0]["BreathingRate"].ToString() != "")
                {
                    this.BreathingRate = int.Parse(ds.Tables[0].Rows[0]["BreathingRate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Face"] != null && ds.Tables[0].Rows[0]["Face"].ToString() != "")
                {
                    this.Face = int.Parse(ds.Tables[0].Rows[0]["Face"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Face_other"] != null)
                {
                    this.Face_other = ds.Tables[0].Rows[0]["Face_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HuangDan"] != null && ds.Tables[0].Rows[0]["HuangDan"].ToString() != "")
                {
                    this.HuangDan = int.Parse(ds.Tables[0].Rows[0]["HuangDan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qx"] != null && ds.Tables[0].Rows[0]["Qx"].ToString() != "")
                {
                    this.Qx = int.Parse(ds.Tables[0].Rows[0]["Qx"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QxCmx"] != null && ds.Tables[0].Rows[0]["QxCmx"].ToString() != "")
                {
                    this.QxCmx = decimal.Parse(ds.Tables[0].Rows[0]["QxCmx"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QxCm"] != null && ds.Tables[0].Rows[0]["QxCm"].ToString() != "")
                {
                    this.QxCm = decimal.Parse(ds.Tables[0].Rows[0]["QxCm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QxStatus"] != null && ds.Tables[0].Rows[0]["QxStatus"].ToString() != "")
                {
                    this.QxStatus = int.Parse(ds.Tables[0].Rows[0]["QxStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QxStatus_other"] != null)
                {
                    this.QxStatus_other = ds.Tables[0].Rows[0]["QxStatus_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Eyes"] != null && ds.Tables[0].Rows[0]["Eyes"].ToString() != "")
                {
                    this.Eyes = int.Parse(ds.Tables[0].Rows[0]["Eyes"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Eyes_other"] != null)
                {
                    this.Eyes_other = ds.Tables[0].Rows[0]["Eyes_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Limbs"] != null && ds.Tables[0].Rows[0]["Limbs"].ToString() != "")
                {
                    this.Limbs = int.Parse(ds.Tables[0].Rows[0]["Limbs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Limbs_other"] != null)
                {
                    this.Limbs_other = ds.Tables[0].Rows[0]["Limbs_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Ears"] != null && ds.Tables[0].Rows[0]["Ears"].ToString() != "")
                {
                    this.Ears = int.Parse(ds.Tables[0].Rows[0]["Ears"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ears_other"] != null)
                {
                    this.Ears_other = ds.Tables[0].Rows[0]["Ears_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Neck"] != null && ds.Tables[0].Rows[0]["Neck"].ToString() != "")
                {
                    this.Neck = int.Parse(ds.Tables[0].Rows[0]["Neck"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Neck_other"] != null)
                {
                    this.Neck_other = ds.Tables[0].Rows[0]["Neck_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Nose"] != null && ds.Tables[0].Rows[0]["Nose"].ToString() != "")
                {
                    this.Nose = int.Parse(ds.Tables[0].Rows[0]["Nose"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Nose_other"] != null)
                {
                    this.Nose_other = ds.Tables[0].Rows[0]["Nose_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Skin"] != null && ds.Tables[0].Rows[0]["Skin"].ToString() != "")
                {
                    this.Skin = int.Parse(ds.Tables[0].Rows[0]["Skin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Skin_other"] != null)
                {
                    this.Skin_other = ds.Tables[0].Rows[0]["Skin_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Mouth"] != null && ds.Tables[0].Rows[0]["Mouth"].ToString() != "")
                {
                    this.Mouth = int.Parse(ds.Tables[0].Rows[0]["Mouth"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Mouth_other"] != null)
                {
                    this.Mouth_other = ds.Tables[0].Rows[0]["Mouth_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Anus"] != null && ds.Tables[0].Rows[0]["Anus"].ToString() != "")
                {
                    this.Anus = int.Parse(ds.Tables[0].Rows[0]["Anus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Anus_other"] != null)
                {
                    this.Anus_other = ds.Tables[0].Rows[0]["Anus_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HeartLung"] != null && ds.Tables[0].Rows[0]["HeartLung"].ToString() != "")
                {
                    this.HeartLung = int.Parse(ds.Tables[0].Rows[0]["HeartLung"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HeartLung_other"] != null)
                {
                    this.HeartLung_other = ds.Tables[0].Rows[0]["HeartLung_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Genital"] != null && ds.Tables[0].Rows[0]["Genital"].ToString() != "")
                {
                    this.Genital = int.Parse(ds.Tables[0].Rows[0]["Genital"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Genital_other"] != null)
                {
                    this.Genital_other = ds.Tables[0].Rows[0]["Genital_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Abdomen"] != null && ds.Tables[0].Rows[0]["Abdomen"].ToString() != "")
                {
                    this.Abdomen = int.Parse(ds.Tables[0].Rows[0]["Abdomen"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Abdomen_other"] != null)
                {
                    this.Abdomen_other = ds.Tables[0].Rows[0]["Abdomen_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Spine"] != null && ds.Tables[0].Rows[0]["Spine"].ToString() != "")
                {
                    this.Spine = int.Parse(ds.Tables[0].Rows[0]["Spine"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Spine_other"] != null)
                {
                    this.Spine_other = ds.Tables[0].Rows[0]["Spine_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Navel"] != null && ds.Tables[0].Rows[0]["Navel"].ToString() != "")
                {
                    this.Navel = int.Parse(ds.Tables[0].Rows[0]["Navel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Navel_other"] != null)
                {
                    this.Navel_other = ds.Tables[0].Rows[0]["Navel_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zz_Jy"] != null && ds.Tables[0].Rows[0]["Zz_Jy"].ToString() != "")
                {
                    this.Zz_Jy = int.Parse(ds.Tables[0].Rows[0]["Zz_Jy"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Zz_Yy"] != null)
                {
                    this.Zz_Yy = ds.Tables[0].Rows[0]["Zz_Yy"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zz_Jg"] != null)
                {
                    this.Zz_Jg = ds.Tables[0].Rows[0]["Zz_Jg"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Guide"] != null)
                {
                    this.Guide = ds.Tables[0].Rows[0]["Guide"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Guide_other"] != null)
                {
                    this.Guide_other = ds.Tables[0].Rows[0]["Guide_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["VisitDate"] != null && ds.Tables[0].Rows[0]["VisitDate"].ToString() != "")
                {
                    this.VisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["VisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NextVisitDate"] != null && ds.Tables[0].Rows[0]["NextVisitDate"].ToString() != "")
                {
                    this.NextVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["NextVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NextVisitAddress"] != null)
                {
                    this.NextVisitAddress = ds.Tables[0].Rows[0]["NextVisitAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Doctor"] != null)
                {
                    this.Doctor = ds.Tables[0].Rows[0]["Doctor"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Sfjg"] != null)
                {
                    this.Sfjg = ds.Tables[0].Rows[0]["Sfjg"].ToString();
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
            strSql.Append("select count(1) from [NHS_Child_XsrFs]");
            strSql.Append(" where CommID=@CommID ");

            SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = CommID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [NHS_Child_XsrFs] (");
            strSql.Append("UnvID,FatherName,FatherJob,FatherTel,FatherBirthDay,MotherName,MotherJob,MotherTel,MotherBirthDay,BirthYz,MotherHbqk,MotherHbqk_other,ZcjgName,BirthQk,BirthQk_other,XseZx,ApgarScore,JiXing,JiXing_other,Hearing,Disease,Disease_other,WeightBorn,WeightNow,HeightBorn,FeedingMode,FeedingAmount,FeedingNum,Vomit,Shit,ShitNum,Temperature,PR,BreathingRate,Face,Face_other,HuangDan,Qx,QxCmx,QxCm,QxStatus,QxStatus_other,Eyes,Eyes_other,Limbs,Limbs_other,Ears,Ears_other,Neck,Neck_other,Nose,Nose_other,Skin,Skin_other,Mouth,Mouth_other,Anus,Anus_other,HeartLung,HeartLung_other,Genital,Genital_other,Abdomen,Abdomen_other,Spine,Spine_other,Navel,Navel_other,Zz_Jy,Zz_Yy,Zz_Jg,Guide,Guide_other,VisitDate,NextVisitDate,NextVisitAddress,Doctor,Sfjg,CreateDate,UserID)");
            strSql.Append(" values (");
            strSql.Append("@UnvID,@FatherName,@FatherJob,@FatherTel,@FatherBirthDay,@MotherName,@MotherJob,@MotherTel,@MotherBirthDay,@BirthYz,@MotherHbqk,@MotherHbqk_other,@ZcjgName,@BirthQk,@BirthQk_other,@XseZx,@ApgarScore,@JiXing,@JiXing_other,@Hearing,@Disease,@Disease_other,@WeightBorn,@WeightNow,@HeightBorn,@FeedingMode,@FeedingAmount,@FeedingNum,@Vomit,@Shit,@ShitNum,@Temperature,@PR,@BreathingRate,@Face,@Face_other,@HuangDan,@Qx,@QxCmx,@QxCm,@QxStatus,@QxStatus_other,@Eyes,@Eyes_other,@Limbs,@Limbs_other,@Ears,@Ears_other,@Neck,@Neck_other,@Nose,@Nose_other,@Skin,@Skin_other,@Mouth,@Mouth_other,@Anus,@Anus_other,@HeartLung,@HeartLung_other,@Genital,@Genital_other,@Abdomen,@Abdomen_other,@Spine,@Spine_other,@Navel,@Navel_other,@Zz_Jy,@Zz_Yy,@Zz_Jg,@Guide,@Guide_other,@VisitDate,@NextVisitDate,@NextVisitAddress,@Doctor,@Sfjg,@CreateDate,@UserID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@FatherName", SqlDbType.VarChar,50),
					new SqlParameter("@FatherJob", SqlDbType.VarChar,50),
					new SqlParameter("@FatherTel", SqlDbType.VarChar,50),
					new SqlParameter("@FatherBirthDay", SqlDbType.SmallDateTime),
					new SqlParameter("@MotherName", SqlDbType.VarChar,50),
					new SqlParameter("@MotherJob", SqlDbType.VarChar,50),
					new SqlParameter("@MotherTel", SqlDbType.VarChar,50),
					new SqlParameter("@MotherBirthDay", SqlDbType.SmallDateTime),
					new SqlParameter("@BirthYz", SqlDbType.TinyInt,1),
					new SqlParameter("@MotherHbqk", SqlDbType.VarChar,50),
					new SqlParameter("@MotherHbqk_other", SqlDbType.VarChar,50),
					new SqlParameter("@ZcjgName", SqlDbType.NVarChar,50),
					new SqlParameter("@BirthQk", SqlDbType.VarChar,50),
					new SqlParameter("@BirthQk_other", SqlDbType.VarChar,50),
					new SqlParameter("@XseZx", SqlDbType.Int,4),
					new SqlParameter("@ApgarScore", SqlDbType.VarChar,50),
					new SqlParameter("@JiXing", SqlDbType.Int,4),
					new SqlParameter("@JiXing_other", SqlDbType.VarChar,50),
					new SqlParameter("@Hearing", SqlDbType.Int,4),
					new SqlParameter("@Disease", SqlDbType.Int,4),
					new SqlParameter("@Disease_other", SqlDbType.VarChar,50),
					new SqlParameter("@WeightBorn", SqlDbType.Float,8),
					new SqlParameter("@WeightNow", SqlDbType.Float,8),
					new SqlParameter("@HeightBorn", SqlDbType.Float,8),
					new SqlParameter("@FeedingMode", SqlDbType.Int,4),
					new SqlParameter("@FeedingAmount", SqlDbType.Float,8),
					new SqlParameter("@FeedingNum", SqlDbType.TinyInt,1),
					new SqlParameter("@Vomit", SqlDbType.Int,4),
					new SqlParameter("@Shit", SqlDbType.Int,4),
					new SqlParameter("@ShitNum", SqlDbType.TinyInt,1),
					new SqlParameter("@Temperature", SqlDbType.Float,8),
					new SqlParameter("@PR", SqlDbType.TinyInt,1),
					new SqlParameter("@BreathingRate", SqlDbType.TinyInt,1),
					new SqlParameter("@Face", SqlDbType.Int,4),
					new SqlParameter("@Face_other", SqlDbType.VarChar,50),
					new SqlParameter("@HuangDan", SqlDbType.Int,4),
					new SqlParameter("@Qx", SqlDbType.Int,4),
					new SqlParameter("@QxCmx", SqlDbType.Float,8),
					new SqlParameter("@QxCm", SqlDbType.Float,8),
					new SqlParameter("@QxStatus", SqlDbType.Int,4),
					new SqlParameter("@QxStatus_other", SqlDbType.VarChar,50),
					new SqlParameter("@Eyes", SqlDbType.Int,4),
					new SqlParameter("@Eyes_other", SqlDbType.VarChar,50),
					new SqlParameter("@Limbs", SqlDbType.Int,4),
					new SqlParameter("@Limbs_other", SqlDbType.VarChar,50),
					new SqlParameter("@Ears", SqlDbType.Int,4),
					new SqlParameter("@Ears_other", SqlDbType.VarChar,50),
					new SqlParameter("@Neck", SqlDbType.Int,4),
					new SqlParameter("@Neck_other", SqlDbType.VarChar,50),
					new SqlParameter("@Nose", SqlDbType.Int,4),
					new SqlParameter("@Nose_other", SqlDbType.VarChar,50),
					new SqlParameter("@Skin", SqlDbType.Int,4),
					new SqlParameter("@Skin_other", SqlDbType.VarChar,50),
					new SqlParameter("@Mouth", SqlDbType.Int,4),
					new SqlParameter("@Mouth_other", SqlDbType.VarChar,50),
					new SqlParameter("@Anus", SqlDbType.Int,4),
					new SqlParameter("@Anus_other", SqlDbType.VarChar,50),
					new SqlParameter("@HeartLung", SqlDbType.Int,4),
					new SqlParameter("@HeartLung_other", SqlDbType.VarChar,50),
					new SqlParameter("@Genital", SqlDbType.Int,4),
					new SqlParameter("@Genital_other", SqlDbType.VarChar,50),
					new SqlParameter("@Abdomen", SqlDbType.Int,4),
					new SqlParameter("@Abdomen_other", SqlDbType.VarChar,50),
					new SqlParameter("@Spine", SqlDbType.Int,4),
					new SqlParameter("@Spine_other", SqlDbType.VarChar,50),
					new SqlParameter("@Navel", SqlDbType.Int,4),
					new SqlParameter("@Navel_other", SqlDbType.VarChar,50),
					new SqlParameter("@Zz_Jy", SqlDbType.Int,4),
					new SqlParameter("@Zz_Yy", SqlDbType.NVarChar,50),
					new SqlParameter("@Zz_Jg", SqlDbType.NVarChar,50),
					new SqlParameter("@Guide", SqlDbType.VarChar,50),
					new SqlParameter("@Guide_other", SqlDbType.VarChar,50),
					new SqlParameter("@VisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@NextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@NextVisitAddress", SqlDbType.VarChar,50),
					new SqlParameter("@Doctor", SqlDbType.VarChar,20),
					new SqlParameter("@Sfjg", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = UnvID;
            parameters[1].Value = FatherName;
            parameters[2].Value = FatherJob;
            parameters[3].Value = FatherTel;
            parameters[4].Value = FatherBirthDay;
            parameters[5].Value = MotherName;
            parameters[6].Value = MotherJob;
            parameters[7].Value = MotherTel;
            parameters[8].Value = MotherBirthDay;
            parameters[9].Value = BirthYz;
            parameters[10].Value = MotherHbqk;
            parameters[11].Value = MotherHbqk_other;
            parameters[12].Value = ZcjgName;
            parameters[13].Value = BirthQk;
            parameters[14].Value = BirthQk_other;
            parameters[15].Value = XseZx;
            parameters[16].Value = ApgarScore;
            parameters[17].Value = JiXing;
            parameters[18].Value = JiXing_other;
            parameters[19].Value = Hearing;
            parameters[20].Value = Disease;
            parameters[21].Value = Disease_other;
            parameters[22].Value = WeightBorn;
            parameters[23].Value = WeightNow;
            parameters[24].Value = HeightBorn;
            parameters[25].Value = FeedingMode;
            parameters[26].Value = FeedingAmount;
            parameters[27].Value = FeedingNum;
            parameters[28].Value = Vomit;
            parameters[29].Value = Shit;
            parameters[30].Value = ShitNum;
            parameters[31].Value = Temperature;
            parameters[32].Value = PR;
            parameters[33].Value = BreathingRate;
            parameters[34].Value = Face;
            parameters[35].Value = Face_other;
            parameters[36].Value = HuangDan;
            parameters[37].Value = Qx;
            parameters[38].Value = QxCmx;
            parameters[39].Value = QxCm;
            parameters[40].Value = QxStatus;
            parameters[41].Value = QxStatus_other;
            parameters[42].Value = Eyes;
            parameters[43].Value = Eyes_other;
            parameters[44].Value = Limbs;
            parameters[45].Value = Limbs_other;
            parameters[46].Value = Ears;
            parameters[47].Value = Ears_other;
            parameters[48].Value = Neck;
            parameters[49].Value = Neck_other;
            parameters[50].Value = Nose;
            parameters[51].Value = Nose_other;
            parameters[52].Value = Skin;
            parameters[53].Value = Skin_other;
            parameters[54].Value = Mouth;
            parameters[55].Value = Mouth_other;
            parameters[56].Value = Anus;
            parameters[57].Value = Anus_other;
            parameters[58].Value = HeartLung;
            parameters[59].Value = HeartLung_other;
            parameters[60].Value = Genital;
            parameters[61].Value = Genital_other;
            parameters[62].Value = Abdomen;
            parameters[63].Value = Abdomen_other;
            parameters[64].Value = Spine;
            parameters[65].Value = Spine_other;
            parameters[66].Value = Navel;
            parameters[67].Value = Navel_other;
            parameters[68].Value = Zz_Jy;
            parameters[69].Value = Zz_Yy;
            parameters[70].Value = Zz_Jg;
            parameters[71].Value = Guide;
            parameters[72].Value = Guide_other;
            parameters[73].Value = VisitDate;
            parameters[74].Value = NextVisitDate;
            parameters[75].Value = NextVisitAddress;
            parameters[76].Value = Doctor;
            parameters[77].Value = Sfjg;
            parameters[78].Value = CreateDate;
            parameters[79].Value = UserID;

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
            strSql.Append("update [NHS_Child_XsrFs] set ");
            strSql.Append("UnvID=@UnvID,");
            strSql.Append("FatherName=@FatherName,");
            strSql.Append("FatherJob=@FatherJob,");
            strSql.Append("FatherTel=@FatherTel,");
            strSql.Append("FatherBirthDay=@FatherBirthDay,");
            strSql.Append("MotherName=@MotherName,");
            strSql.Append("MotherJob=@MotherJob,");
            strSql.Append("MotherTel=@MotherTel,");
            strSql.Append("MotherBirthDay=@MotherBirthDay,");
            strSql.Append("BirthYz=@BirthYz,");
            strSql.Append("MotherHbqk=@MotherHbqk,");
            strSql.Append("MotherHbqk_other=@MotherHbqk_other,");
            strSql.Append("ZcjgName=@ZcjgName,");
            strSql.Append("BirthQk=@BirthQk,");
            strSql.Append("BirthQk_other=@BirthQk_other,");
            strSql.Append("XseZx=@XseZx,");
            strSql.Append("ApgarScore=@ApgarScore,");
            strSql.Append("JiXing=@JiXing,");
            strSql.Append("JiXing_other=@JiXing_other,");
            strSql.Append("Hearing=@Hearing,");
            strSql.Append("Disease=@Disease,");
            strSql.Append("Disease_other=@Disease_other,");
            strSql.Append("WeightBorn=@WeightBorn,");
            strSql.Append("WeightNow=@WeightNow,");
            strSql.Append("HeightBorn=@HeightBorn,");
            strSql.Append("FeedingMode=@FeedingMode,");
            strSql.Append("FeedingAmount=@FeedingAmount,");
            strSql.Append("FeedingNum=@FeedingNum,");
            strSql.Append("Vomit=@Vomit,");
            strSql.Append("Shit=@Shit,");
            strSql.Append("ShitNum=@ShitNum,");
            strSql.Append("Temperature=@Temperature,");
            strSql.Append("PR=@PR,");
            strSql.Append("BreathingRate=@BreathingRate,");
            strSql.Append("Face=@Face,");
            strSql.Append("Face_other=@Face_other,");
            strSql.Append("HuangDan=@HuangDan,");
            strSql.Append("Qx=@Qx,");
            strSql.Append("QxCmx=@QxCmx,");
            strSql.Append("QxCm=@QxCm,");
            strSql.Append("QxStatus=@QxStatus,");
            strSql.Append("QxStatus_other=@QxStatus_other,");
            strSql.Append("Eyes=@Eyes,");
            strSql.Append("Eyes_other=@Eyes_other,");
            strSql.Append("Limbs=@Limbs,");
            strSql.Append("Limbs_other=@Limbs_other,");
            strSql.Append("Ears=@Ears,");
            strSql.Append("Ears_other=@Ears_other,");
            strSql.Append("Neck=@Neck,");
            strSql.Append("Neck_other=@Neck_other,");
            strSql.Append("Nose=@Nose,");
            strSql.Append("Nose_other=@Nose_other,");
            strSql.Append("Skin=@Skin,");
            strSql.Append("Skin_other=@Skin_other,");
            strSql.Append("Mouth=@Mouth,");
            strSql.Append("Mouth_other=@Mouth_other,");
            strSql.Append("Anus=@Anus,");
            strSql.Append("Anus_other=@Anus_other,");
            strSql.Append("HeartLung=@HeartLung,");
            strSql.Append("HeartLung_other=@HeartLung_other,");
            strSql.Append("Genital=@Genital,");
            strSql.Append("Genital_other=@Genital_other,");
            strSql.Append("Abdomen=@Abdomen,");
            strSql.Append("Abdomen_other=@Abdomen_other,");
            strSql.Append("Spine=@Spine,");
            strSql.Append("Spine_other=@Spine_other,");
            strSql.Append("Navel=@Navel,");
            strSql.Append("Navel_other=@Navel_other,");
            strSql.Append("Zz_Jy=@Zz_Jy,");
            strSql.Append("Zz_Yy=@Zz_Yy,");
            strSql.Append("Zz_Jg=@Zz_Jg,");
            strSql.Append("Guide=@Guide,");
            strSql.Append("Guide_other=@Guide_other,");
            strSql.Append("VisitDate=@VisitDate,");
            strSql.Append("NextVisitDate=@NextVisitDate,");
            strSql.Append("NextVisitAddress=@NextVisitAddress,");
            strSql.Append("Doctor=@Doctor,");
            strSql.Append("Sfjg=@Sfjg,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UserID=@UserID");
            strSql.Append(" where CommID=@CommID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@FatherName", SqlDbType.VarChar,50),
					new SqlParameter("@FatherJob", SqlDbType.VarChar,50),
					new SqlParameter("@FatherTel", SqlDbType.VarChar,50),
					new SqlParameter("@FatherBirthDay", SqlDbType.SmallDateTime),
					new SqlParameter("@MotherName", SqlDbType.VarChar,50),
					new SqlParameter("@MotherJob", SqlDbType.VarChar,50),
					new SqlParameter("@MotherTel", SqlDbType.VarChar,50),
					new SqlParameter("@MotherBirthDay", SqlDbType.SmallDateTime),
					new SqlParameter("@BirthYz", SqlDbType.TinyInt,1),
					new SqlParameter("@MotherHbqk", SqlDbType.VarChar,50),
					new SqlParameter("@MotherHbqk_other", SqlDbType.VarChar,50),
					new SqlParameter("@ZcjgName", SqlDbType.NVarChar,50),
					new SqlParameter("@BirthQk", SqlDbType.VarChar,50),
					new SqlParameter("@BirthQk_other", SqlDbType.VarChar,50),
					new SqlParameter("@XseZx", SqlDbType.Int,4),
					new SqlParameter("@ApgarScore", SqlDbType.VarChar,50),
					new SqlParameter("@JiXing", SqlDbType.Int,4),
					new SqlParameter("@JiXing_other", SqlDbType.VarChar,50),
					new SqlParameter("@Hearing", SqlDbType.Int,4),
					new SqlParameter("@Disease", SqlDbType.Int,4),
					new SqlParameter("@Disease_other", SqlDbType.VarChar,50),
					new SqlParameter("@WeightBorn", SqlDbType.Float,8),
					new SqlParameter("@WeightNow", SqlDbType.Float,8),
					new SqlParameter("@HeightBorn", SqlDbType.Float,8),
					new SqlParameter("@FeedingMode", SqlDbType.Int,4),
					new SqlParameter("@FeedingAmount", SqlDbType.Float,8),
					new SqlParameter("@FeedingNum", SqlDbType.TinyInt,1),
					new SqlParameter("@Vomit", SqlDbType.Int,4),
					new SqlParameter("@Shit", SqlDbType.Int,4),
					new SqlParameter("@ShitNum", SqlDbType.TinyInt,1),
					new SqlParameter("@Temperature", SqlDbType.Float,8),
					new SqlParameter("@PR", SqlDbType.TinyInt,1),
					new SqlParameter("@BreathingRate", SqlDbType.TinyInt,1),
					new SqlParameter("@Face", SqlDbType.Int,4),
					new SqlParameter("@Face_other", SqlDbType.VarChar,50),
					new SqlParameter("@HuangDan", SqlDbType.Int,4),
					new SqlParameter("@Qx", SqlDbType.Int,4),
					new SqlParameter("@QxCmx", SqlDbType.Float,8),
					new SqlParameter("@QxCm", SqlDbType.Float,8),
					new SqlParameter("@QxStatus", SqlDbType.Int,4),
					new SqlParameter("@QxStatus_other", SqlDbType.VarChar,50),
					new SqlParameter("@Eyes", SqlDbType.Int,4),
					new SqlParameter("@Eyes_other", SqlDbType.VarChar,50),
					new SqlParameter("@Limbs", SqlDbType.Int,4),
					new SqlParameter("@Limbs_other", SqlDbType.VarChar,50),
					new SqlParameter("@Ears", SqlDbType.Int,4),
					new SqlParameter("@Ears_other", SqlDbType.VarChar,50),
					new SqlParameter("@Neck", SqlDbType.Int,4),
					new SqlParameter("@Neck_other", SqlDbType.VarChar,50),
					new SqlParameter("@Nose", SqlDbType.Int,4),
					new SqlParameter("@Nose_other", SqlDbType.VarChar,50),
					new SqlParameter("@Skin", SqlDbType.Int,4),
					new SqlParameter("@Skin_other", SqlDbType.VarChar,50),
					new SqlParameter("@Mouth", SqlDbType.Int,4),
					new SqlParameter("@Mouth_other", SqlDbType.VarChar,50),
					new SqlParameter("@Anus", SqlDbType.Int,4),
					new SqlParameter("@Anus_other", SqlDbType.VarChar,50),
					new SqlParameter("@HeartLung", SqlDbType.Int,4),
					new SqlParameter("@HeartLung_other", SqlDbType.VarChar,50),
					new SqlParameter("@Genital", SqlDbType.Int,4),
					new SqlParameter("@Genital_other", SqlDbType.VarChar,50),
					new SqlParameter("@Abdomen", SqlDbType.Int,4),
					new SqlParameter("@Abdomen_other", SqlDbType.VarChar,50),
					new SqlParameter("@Spine", SqlDbType.Int,4),
					new SqlParameter("@Spine_other", SqlDbType.VarChar,50),
					new SqlParameter("@Navel", SqlDbType.Int,4),
					new SqlParameter("@Navel_other", SqlDbType.VarChar,50),
					new SqlParameter("@Zz_Jy", SqlDbType.Int,4),
					new SqlParameter("@Zz_Yy", SqlDbType.NVarChar,50),
					new SqlParameter("@Zz_Jg", SqlDbType.NVarChar,50),
					new SqlParameter("@Guide", SqlDbType.VarChar,50),
					new SqlParameter("@Guide_other", SqlDbType.VarChar,50),
					new SqlParameter("@VisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@NextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@NextVisitAddress", SqlDbType.VarChar,50),
					new SqlParameter("@Doctor", SqlDbType.VarChar,20),
					new SqlParameter("@Sfjg", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = UnvID;
            parameters[1].Value = FatherName;
            parameters[2].Value = FatherJob;
            parameters[3].Value = FatherTel;
            parameters[4].Value = FatherBirthDay;
            parameters[5].Value = MotherName;
            parameters[6].Value = MotherJob;
            parameters[7].Value = MotherTel;
            parameters[8].Value = MotherBirthDay;
            parameters[9].Value = BirthYz;
            parameters[10].Value = MotherHbqk;
            parameters[11].Value = MotherHbqk_other;
            parameters[12].Value = ZcjgName;
            parameters[13].Value = BirthQk;
            parameters[14].Value = BirthQk_other;
            parameters[15].Value = XseZx;
            parameters[16].Value = ApgarScore;
            parameters[17].Value = JiXing;
            parameters[18].Value = JiXing_other;
            parameters[19].Value = Hearing;
            parameters[20].Value = Disease;
            parameters[21].Value = Disease_other;
            parameters[22].Value = WeightBorn;
            parameters[23].Value = WeightNow;
            parameters[24].Value = HeightBorn;
            parameters[25].Value = FeedingMode;
            parameters[26].Value = FeedingAmount;
            parameters[27].Value = FeedingNum;
            parameters[28].Value = Vomit;
            parameters[29].Value = Shit;
            parameters[30].Value = ShitNum;
            parameters[31].Value = Temperature;
            parameters[32].Value = PR;
            parameters[33].Value = BreathingRate;
            parameters[34].Value = Face;
            parameters[35].Value = Face_other;
            parameters[36].Value = HuangDan;
            parameters[37].Value = Qx;
            parameters[38].Value = QxCmx;
            parameters[39].Value = QxCm;
            parameters[40].Value = QxStatus;
            parameters[41].Value = QxStatus_other;
            parameters[42].Value = Eyes;
            parameters[43].Value = Eyes_other;
            parameters[44].Value = Limbs;
            parameters[45].Value = Limbs_other;
            parameters[46].Value = Ears;
            parameters[47].Value = Ears_other;
            parameters[48].Value = Neck;
            parameters[49].Value = Neck_other;
            parameters[50].Value = Nose;
            parameters[51].Value = Nose_other;
            parameters[52].Value = Skin;
            parameters[53].Value = Skin_other;
            parameters[54].Value = Mouth;
            parameters[55].Value = Mouth_other;
            parameters[56].Value = Anus;
            parameters[57].Value = Anus_other;
            parameters[58].Value = HeartLung;
            parameters[59].Value = HeartLung_other;
            parameters[60].Value = Genital;
            parameters[61].Value = Genital_other;
            parameters[62].Value = Abdomen;
            parameters[63].Value = Abdomen_other;
            parameters[64].Value = Spine;
            parameters[65].Value = Spine_other;
            parameters[66].Value = Navel;
            parameters[67].Value = Navel_other;
            parameters[68].Value = Zz_Jy;
            parameters[69].Value = Zz_Yy;
            parameters[70].Value = Zz_Jg;
            parameters[71].Value = Guide;
            parameters[72].Value = Guide_other;
            parameters[73].Value = VisitDate;
            parameters[74].Value = NextVisitDate;
            parameters[75].Value = NextVisitAddress;
            parameters[76].Value = Doctor;
            parameters[77].Value = Sfjg;
            parameters[78].Value = CreateDate;
            parameters[79].Value = UserID;
            parameters[80].Value = CommID;

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
            strSql.Append("delete from [NHS_Child_XsrFs] ");
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
            strSql.Append("select CommID,UnvID,FatherName,FatherJob,FatherTel,FatherBirthDay,MotherName,MotherJob,MotherTel,MotherBirthDay,BirthYz,MotherHbqk,MotherHbqk_other,ZcjgName,BirthQk,BirthQk_other,XseZx,ApgarScore,JiXing,JiXing_other,Hearing,Disease,Disease_other,WeightBorn,WeightNow,HeightBorn,FeedingMode,FeedingAmount,FeedingNum,Vomit,Shit,ShitNum,Temperature,PR,BreathingRate,Face,Face_other,HuangDan,Qx,QxCmx,QxCm,QxStatus,QxStatus_other,Eyes,Eyes_other,Limbs,Limbs_other,Ears,Ears_other,Neck,Neck_other,Nose,Nose_other,Skin,Skin_other,Mouth,Mouth_other,Anus,Anus_other,HeartLung,HeartLung_other,Genital,Genital_other,Abdomen,Abdomen_other,Spine,Spine_other,Navel,Navel_other,Zz_Jy,Zz_Yy,Zz_Jg,Guide,Guide_other,VisitDate,NextVisitDate,NextVisitAddress,Doctor,Sfjg,CreateDate,UserID ");
            strSql.Append(" FROM [NHS_Child_XsrFs] ");
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
                if (ds.Tables[0].Rows[0]["FatherName"] != null)
                {
                    this.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FatherJob"] != null)
                {
                    this.FatherJob = ds.Tables[0].Rows[0]["FatherJob"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FatherTel"] != null)
                {
                    this.FatherTel = ds.Tables[0].Rows[0]["FatherTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FatherBirthDay"] != null && ds.Tables[0].Rows[0]["FatherBirthDay"].ToString() != "")
                {
                    this.FatherBirthDay = DateTime.Parse(ds.Tables[0].Rows[0]["FatherBirthDay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MotherName"] != null)
                {
                    this.MotherName = ds.Tables[0].Rows[0]["MotherName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherJob"] != null)
                {
                    this.MotherJob = ds.Tables[0].Rows[0]["MotherJob"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherTel"] != null)
                {
                    this.MotherTel = ds.Tables[0].Rows[0]["MotherTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherBirthDay"] != null && ds.Tables[0].Rows[0]["MotherBirthDay"].ToString() != "")
                {
                    this.MotherBirthDay = DateTime.Parse(ds.Tables[0].Rows[0]["MotherBirthDay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BirthYz"] != null && ds.Tables[0].Rows[0]["BirthYz"].ToString() != "")
                {
                    this.BirthYz = int.Parse(ds.Tables[0].Rows[0]["BirthYz"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MotherHbqk"] != null)
                {
                    this.MotherHbqk = ds.Tables[0].Rows[0]["MotherHbqk"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MotherHbqk_other"] != null)
                {
                    this.MotherHbqk_other = ds.Tables[0].Rows[0]["MotherHbqk_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZcjgName"] != null)
                {
                    this.ZcjgName = ds.Tables[0].Rows[0]["ZcjgName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BirthQk"] != null)
                {
                    this.BirthQk = ds.Tables[0].Rows[0]["BirthQk"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BirthQk_other"] != null)
                {
                    this.BirthQk_other = ds.Tables[0].Rows[0]["BirthQk_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XseZx"] != null && ds.Tables[0].Rows[0]["XseZx"].ToString() != "")
                {
                    this.XseZx = int.Parse(ds.Tables[0].Rows[0]["XseZx"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ApgarScore"] != null)
                {
                    this.ApgarScore = ds.Tables[0].Rows[0]["ApgarScore"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JiXing"] != null && ds.Tables[0].Rows[0]["JiXing"].ToString() != "")
                {
                    this.JiXing = int.Parse(ds.Tables[0].Rows[0]["JiXing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JiXing_other"] != null)
                {
                    this.JiXing_other = ds.Tables[0].Rows[0]["JiXing_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Hearing"] != null && ds.Tables[0].Rows[0]["Hearing"].ToString() != "")
                {
                    this.Hearing = int.Parse(ds.Tables[0].Rows[0]["Hearing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Disease"] != null && ds.Tables[0].Rows[0]["Disease"].ToString() != "")
                {
                    this.Disease = int.Parse(ds.Tables[0].Rows[0]["Disease"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Disease_other"] != null)
                {
                    this.Disease_other = ds.Tables[0].Rows[0]["Disease_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WeightBorn"] != null && ds.Tables[0].Rows[0]["WeightBorn"].ToString() != "")
                {
                    this.WeightBorn = decimal.Parse(ds.Tables[0].Rows[0]["WeightBorn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WeightNow"] != null && ds.Tables[0].Rows[0]["WeightNow"].ToString() != "")
                {
                    this.WeightNow = decimal.Parse(ds.Tables[0].Rows[0]["WeightNow"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HeightBorn"] != null && ds.Tables[0].Rows[0]["HeightBorn"].ToString() != "")
                {
                    this.HeightBorn = decimal.Parse(ds.Tables[0].Rows[0]["HeightBorn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FeedingMode"] != null && ds.Tables[0].Rows[0]["FeedingMode"].ToString() != "")
                {
                    this.FeedingMode = int.Parse(ds.Tables[0].Rows[0]["FeedingMode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FeedingAmount"] != null && ds.Tables[0].Rows[0]["FeedingAmount"].ToString() != "")
                {
                    this.FeedingAmount = decimal.Parse(ds.Tables[0].Rows[0]["FeedingAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FeedingNum"] != null && ds.Tables[0].Rows[0]["FeedingNum"].ToString() != "")
                {
                    this.FeedingNum = int.Parse(ds.Tables[0].Rows[0]["FeedingNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Vomit"] != null && ds.Tables[0].Rows[0]["Vomit"].ToString() != "")
                {
                    this.Vomit = int.Parse(ds.Tables[0].Rows[0]["Vomit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Shit"] != null && ds.Tables[0].Rows[0]["Shit"].ToString() != "")
                {
                    this.Shit = int.Parse(ds.Tables[0].Rows[0]["Shit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ShitNum"] != null && ds.Tables[0].Rows[0]["ShitNum"].ToString() != "")
                {
                    this.ShitNum = int.Parse(ds.Tables[0].Rows[0]["ShitNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Temperature"] != null && ds.Tables[0].Rows[0]["Temperature"].ToString() != "")
                {
                    this.Temperature = decimal.Parse(ds.Tables[0].Rows[0]["Temperature"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PR"] != null && ds.Tables[0].Rows[0]["PR"].ToString() != "")
                {
                    this.PR = int.Parse(ds.Tables[0].Rows[0]["PR"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BreathingRate"] != null && ds.Tables[0].Rows[0]["BreathingRate"].ToString() != "")
                {
                    this.BreathingRate = int.Parse(ds.Tables[0].Rows[0]["BreathingRate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Face"] != null && ds.Tables[0].Rows[0]["Face"].ToString() != "")
                {
                    this.Face = int.Parse(ds.Tables[0].Rows[0]["Face"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Face_other"] != null)
                {
                    this.Face_other = ds.Tables[0].Rows[0]["Face_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HuangDan"] != null && ds.Tables[0].Rows[0]["HuangDan"].ToString() != "")
                {
                    this.HuangDan = int.Parse(ds.Tables[0].Rows[0]["HuangDan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qx"] != null && ds.Tables[0].Rows[0]["Qx"].ToString() != "")
                {
                    this.Qx = int.Parse(ds.Tables[0].Rows[0]["Qx"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QxCmx"] != null && ds.Tables[0].Rows[0]["QxCmx"].ToString() != "")
                {
                    this.QxCmx = decimal.Parse(ds.Tables[0].Rows[0]["QxCmx"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QxCm"] != null && ds.Tables[0].Rows[0]["QxCm"].ToString() != "")
                {
                    this.QxCm = decimal.Parse(ds.Tables[0].Rows[0]["QxCm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QxStatus"] != null && ds.Tables[0].Rows[0]["QxStatus"].ToString() != "")
                {
                    this.QxStatus = int.Parse(ds.Tables[0].Rows[0]["QxStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QxStatus_other"] != null)
                {
                    this.QxStatus_other = ds.Tables[0].Rows[0]["QxStatus_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Eyes"] != null && ds.Tables[0].Rows[0]["Eyes"].ToString() != "")
                {
                    this.Eyes = int.Parse(ds.Tables[0].Rows[0]["Eyes"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Eyes_other"] != null)
                {
                    this.Eyes_other = ds.Tables[0].Rows[0]["Eyes_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Limbs"] != null && ds.Tables[0].Rows[0]["Limbs"].ToString() != "")
                {
                    this.Limbs = int.Parse(ds.Tables[0].Rows[0]["Limbs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Limbs_other"] != null)
                {
                    this.Limbs_other = ds.Tables[0].Rows[0]["Limbs_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Ears"] != null && ds.Tables[0].Rows[0]["Ears"].ToString() != "")
                {
                    this.Ears = int.Parse(ds.Tables[0].Rows[0]["Ears"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ears_other"] != null)
                {
                    this.Ears_other = ds.Tables[0].Rows[0]["Ears_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Neck"] != null && ds.Tables[0].Rows[0]["Neck"].ToString() != "")
                {
                    this.Neck = int.Parse(ds.Tables[0].Rows[0]["Neck"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Neck_other"] != null)
                {
                    this.Neck_other = ds.Tables[0].Rows[0]["Neck_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Nose"] != null && ds.Tables[0].Rows[0]["Nose"].ToString() != "")
                {
                    this.Nose = int.Parse(ds.Tables[0].Rows[0]["Nose"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Nose_other"] != null)
                {
                    this.Nose_other = ds.Tables[0].Rows[0]["Nose_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Skin"] != null && ds.Tables[0].Rows[0]["Skin"].ToString() != "")
                {
                    this.Skin = int.Parse(ds.Tables[0].Rows[0]["Skin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Skin_other"] != null)
                {
                    this.Skin_other = ds.Tables[0].Rows[0]["Skin_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Mouth"] != null && ds.Tables[0].Rows[0]["Mouth"].ToString() != "")
                {
                    this.Mouth = int.Parse(ds.Tables[0].Rows[0]["Mouth"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Mouth_other"] != null)
                {
                    this.Mouth_other = ds.Tables[0].Rows[0]["Mouth_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Anus"] != null && ds.Tables[0].Rows[0]["Anus"].ToString() != "")
                {
                    this.Anus = int.Parse(ds.Tables[0].Rows[0]["Anus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Anus_other"] != null)
                {
                    this.Anus_other = ds.Tables[0].Rows[0]["Anus_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HeartLung"] != null && ds.Tables[0].Rows[0]["HeartLung"].ToString() != "")
                {
                    this.HeartLung = int.Parse(ds.Tables[0].Rows[0]["HeartLung"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HeartLung_other"] != null)
                {
                    this.HeartLung_other = ds.Tables[0].Rows[0]["HeartLung_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Genital"] != null && ds.Tables[0].Rows[0]["Genital"].ToString() != "")
                {
                    this.Genital = int.Parse(ds.Tables[0].Rows[0]["Genital"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Genital_other"] != null)
                {
                    this.Genital_other = ds.Tables[0].Rows[0]["Genital_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Abdomen"] != null && ds.Tables[0].Rows[0]["Abdomen"].ToString() != "")
                {
                    this.Abdomen = int.Parse(ds.Tables[0].Rows[0]["Abdomen"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Abdomen_other"] != null)
                {
                    this.Abdomen_other = ds.Tables[0].Rows[0]["Abdomen_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Spine"] != null && ds.Tables[0].Rows[0]["Spine"].ToString() != "")
                {
                    this.Spine = int.Parse(ds.Tables[0].Rows[0]["Spine"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Spine_other"] != null)
                {
                    this.Spine_other = ds.Tables[0].Rows[0]["Spine_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Navel"] != null && ds.Tables[0].Rows[0]["Navel"].ToString() != "")
                {
                    this.Navel = int.Parse(ds.Tables[0].Rows[0]["Navel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Navel_other"] != null)
                {
                    this.Navel_other = ds.Tables[0].Rows[0]["Navel_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zz_Jy"] != null && ds.Tables[0].Rows[0]["Zz_Jy"].ToString() != "")
                {
                    this.Zz_Jy = int.Parse(ds.Tables[0].Rows[0]["Zz_Jy"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Zz_Yy"] != null)
                {
                    this.Zz_Yy = ds.Tables[0].Rows[0]["Zz_Yy"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zz_Jg"] != null)
                {
                    this.Zz_Jg = ds.Tables[0].Rows[0]["Zz_Jg"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Guide"] != null)
                {
                    this.Guide = ds.Tables[0].Rows[0]["Guide"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Guide_other"] != null)
                {
                    this.Guide_other = ds.Tables[0].Rows[0]["Guide_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["VisitDate"] != null && ds.Tables[0].Rows[0]["VisitDate"].ToString() != "")
                {
                    this.VisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["VisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NextVisitDate"] != null && ds.Tables[0].Rows[0]["NextVisitDate"].ToString() != "")
                {
                    this.NextVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["NextVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NextVisitAddress"] != null)
                {
                    this.NextVisitAddress = ds.Tables[0].Rows[0]["NextVisitAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Doctor"] != null)
                {
                    this.Doctor = ds.Tables[0].Rows[0]["Doctor"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Sfjg"] != null)
                {
                    this.Sfjg = ds.Tables[0].Rows[0]["Sfjg"].ToString();
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
            strSql.Append(" FROM [NHS_Child_XsrFs] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

