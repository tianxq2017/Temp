using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using UNV.Comm.DataBase;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_Child_JKJC。
    /// </summary>
    [Serializable]
    public partial class NHS_Child_JKJC
    {
        public NHS_Child_JKJC()
        { }
        #region Model
        private int _commid;
        private string _unvid;
        private int? _visititems;
        private DateTime? _visitdate;
        private int? _wyfs;
        private decimal? _outdoors;
        private decimal? _weight;
        private int? _weight_other;
        private decimal? _height;
        private int? _height_other;
        private decimal? _touwei;
        private string _qgfypj;
        private int? _qgjc_face;
        private string _qgjc_face_other;
        private int? _qgjc_skin;
        private string _qgjc_skin_other;
        private int? _qgjc_qianxin;
        private decimal? _qgjc_qianxincmx;
        private decimal? _qgjc_qianxincm;
        private int? _qgjc_neck;
        private string _qgjc_neck_other;
        private int? _qgjc_eyes;
        private string _qgjc_eyes_other;
        private int? _qgjc_ears;
        private string _qgjc_ears_other;
        private int? _qgjc_hearing;
        private string _qgjc_hearing_other;
        private int? _qgjc_mouth;
        private string _qgjc_mouth_other;
        private int? _qgjc_teethnum;
        private int? _qgjc_teethnum2;
        private int? _qgjc_heartlung;
        private string _qgjc_heartlung_other;
        private int? _qgjc_abdomen;
        private string _qgjc_abdomen_other;
        private int? _qgjc_navel;
        private string _qgjc_navel_other;
        private int? _qgjc_limbs;
        private string _qgjc_limbs_other;
        private string _qgjc_ricketssymptom;
        private string _qgjc_ricketssymptom_other;
        private string _qgjc_ricketsbody;
        private int? _qgjc_genital;
        private string _qgjc_genital_other;
        private int? _qgjc_anus;
        private string _qgjc_anus_other;
        private decimal? _qgjc_xhdb;
        private decimal? _vitamind;
        private string _diseases;
        private string _fayupinggu;
        private string _others;
        private int? _zz_yj;
        private string _zz_yy;
        private string _zz_jg;
        private DateTime? _nextvisitdate;
        private string _doctor;
        private DateTime? _createdate = DateTime.Now;
        private int? _userid;
        private string _guide;
        private string _nextvistaddress;
        private string _sfjg;
        private string _guideqt;
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
        /// 随访的项目、月龄
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
        public int? wyfs
        {
            set { _wyfs = value; }
            get { return _wyfs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Outdoors
        {
            set { _outdoors = value; }
            get { return _outdoors; }
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
        public int? Weight_other
        {
            set { _weight_other = value; }
            get { return _weight_other; }
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
        public int? Height_other
        {
            set { _height_other = value; }
            get { return _height_other; }
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
        public string QgfyPj
        {
            set { _qgfypj = value; }
            get { return _qgfypj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qgjc_Face
        {
            set { _qgjc_face = value; }
            get { return _qgjc_face; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_Face_other
        {
            set { _qgjc_face_other = value; }
            get { return _qgjc_face_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qgjc_Skin
        {
            set { _qgjc_skin = value; }
            get { return _qgjc_skin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_Skin_other
        {
            set { _qgjc_skin_other = value; }
            get { return _qgjc_skin_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qgjc_QianXin
        {
            set { _qgjc_qianxin = value; }
            get { return _qgjc_qianxin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Qgjc_QianXinCmx
        {
            set { _qgjc_qianxincmx = value; }
            get { return _qgjc_qianxincmx; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Qgjc_QianXinCm
        {
            set { _qgjc_qianxincm = value; }
            get { return _qgjc_qianxincm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qgjc_Neck
        {
            set { _qgjc_neck = value; }
            get { return _qgjc_neck; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_Neck_other
        {
            set { _qgjc_neck_other = value; }
            get { return _qgjc_neck_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qgjc_Eyes
        {
            set { _qgjc_eyes = value; }
            get { return _qgjc_eyes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_Eyes_other
        {
            set { _qgjc_eyes_other = value; }
            get { return _qgjc_eyes_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qgjc_Ears
        {
            set { _qgjc_ears = value; }
            get { return _qgjc_ears; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_Ears_other
        {
            set { _qgjc_ears_other = value; }
            get { return _qgjc_ears_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qgjc_Hearing
        {
            set { _qgjc_hearing = value; }
            get { return _qgjc_hearing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_Hearing_other
        {
            set { _qgjc_hearing_other = value; }
            get { return _qgjc_hearing_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qgjc_Mouth
        {
            set { _qgjc_mouth = value; }
            get { return _qgjc_mouth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_Mouth_other
        {
            set { _qgjc_mouth_other = value; }
            get { return _qgjc_mouth_other; }
        }
        /// <summary>
        /// 出牙数
        /// </summary>
        public int? Qgjc_teethnum
        {
            set { _qgjc_teethnum = value; }
            get { return _qgjc_teethnum; }
        }
        /// <summary>
        /// 龋齿
        /// </summary>
        public int? Qgjc_teethnum2
        {
            set { _qgjc_teethnum2 = value; }
            get { return _qgjc_teethnum2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qgjc_HeartLung
        {
            set { _qgjc_heartlung = value; }
            get { return _qgjc_heartlung; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_HeartLung_other
        {
            set { _qgjc_heartlung_other = value; }
            get { return _qgjc_heartlung_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qgjc_Abdomen
        {
            set { _qgjc_abdomen = value; }
            get { return _qgjc_abdomen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_Abdomen_other
        {
            set { _qgjc_abdomen_other = value; }
            get { return _qgjc_abdomen_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qgjc_Navel
        {
            set { _qgjc_navel = value; }
            get { return _qgjc_navel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_Navel_other
        {
            set { _qgjc_navel_other = value; }
            get { return _qgjc_navel_other; }
        }
        /// <summary>
        /// 四肢
        /// </summary>
        public int? Qgjc_Limbs
        {
            set { _qgjc_limbs = value; }
            get { return _qgjc_limbs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_Limbs_other
        {
            set { _qgjc_limbs_other = value; }
            get { return _qgjc_limbs_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_RicketsSymptom
        {
            set { _qgjc_ricketssymptom = value; }
            get { return _qgjc_ricketssymptom; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_RicketsSymptom_other
        {
            set { _qgjc_ricketssymptom_other = value; }
            get { return _qgjc_ricketssymptom_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_RicketsBody
        {
            set { _qgjc_ricketsbody = value; }
            get { return _qgjc_ricketsbody; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qgjc_Genital
        {
            set { _qgjc_genital = value; }
            get { return _qgjc_genital; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_Genital_other
        {
            set { _qgjc_genital_other = value; }
            get { return _qgjc_genital_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Qgjc_Anus
        {
            set { _qgjc_anus = value; }
            get { return _qgjc_anus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Qgjc_Anus_other
        {
            set { _qgjc_anus_other = value; }
            get { return _qgjc_anus_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Qgjc_Xhdb
        {
            set { _qgjc_xhdb = value; }
            get { return _qgjc_xhdb; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? VitaminD
        {
            set { _vitamind = value; }
            get { return _vitamind; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Diseases
        {
            set { _diseases = value; }
            get { return _diseases; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FaYuPingGu
        {
            set { _fayupinggu = value; }
            get { return _fayupinggu; }
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
        public int? Zz_Yj
        {
            set { _zz_yj = value; }
            get { return _zz_yj; }
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
        public string NextVistAddress
        {
            set { _nextvistaddress = value; }
            get { return _nextvistaddress; }
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
        public string GuideQt
        {
            set { _guideqt = value; }
            get { return _guideqt; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NHS_Child_JKJC(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommID,UnvID,VisitItems,VisitDate,wyfs,Outdoors,Weight,Weight_other,Height,Height_other,TouWei,QgfyPj,Qgjc_Face,Qgjc_Face_other,Qgjc_Skin,Qgjc_Skin_other,Qgjc_QianXin,Qgjc_QianXinCmx,Qgjc_QianXinCm,Qgjc_Neck,Qgjc_Neck_other,Qgjc_Eyes,Qgjc_Eyes_other,Qgjc_Ears,Qgjc_Ears_other,Qgjc_Hearing,Qgjc_Hearing_other,Qgjc_Mouth,Qgjc_Mouth_other,Qgjc_teethnum,Qgjc_teethnum2,Qgjc_HeartLung,Qgjc_HeartLung_other,Qgjc_Abdomen,Qgjc_Abdomen_other,Qgjc_Navel,Qgjc_Navel_other,Qgjc_Limbs,Qgjc_Limbs_other,Qgjc_RicketsSymptom,Qgjc_RicketsSymptom_other,Qgjc_RicketsBody,Qgjc_Genital,Qgjc_Genital_other,Qgjc_Anus,Qgjc_Anus_other,Qgjc_Xhdb,VitaminD,Diseases,FaYuPingGu,Others,Zz_Yj,Zz_Yy,Zz_Jg,NextVisitDate,Doctor,CreateDate,UserID,Guide,NextVistAddress,SFJG,GuideQt ");
            strSql.Append(" FROM [NHS_Child_JKJC] ");
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
                if (ds.Tables[0].Rows[0]["wyfs"] != null && ds.Tables[0].Rows[0]["wyfs"].ToString() != "")
                {
                    this.wyfs = int.Parse(ds.Tables[0].Rows[0]["wyfs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Outdoors"] != null && ds.Tables[0].Rows[0]["Outdoors"].ToString() != "")
                {
                    this.Outdoors = decimal.Parse(ds.Tables[0].Rows[0]["Outdoors"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Weight"] != null && ds.Tables[0].Rows[0]["Weight"].ToString() != "")
                {
                    this.Weight = decimal.Parse(ds.Tables[0].Rows[0]["Weight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Weight_other"] != null && ds.Tables[0].Rows[0]["Weight_other"].ToString() != "")
                {
                    this.Weight_other = int.Parse(ds.Tables[0].Rows[0]["Weight_other"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Height"] != null && ds.Tables[0].Rows[0]["Height"].ToString() != "")
                {
                    this.Height = decimal.Parse(ds.Tables[0].Rows[0]["Height"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Height_other"] != null && ds.Tables[0].Rows[0]["Height_other"].ToString() != "")
                {
                    this.Height_other = int.Parse(ds.Tables[0].Rows[0]["Height_other"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TouWei"] != null && ds.Tables[0].Rows[0]["TouWei"].ToString() != "")
                {
                    this.TouWei = decimal.Parse(ds.Tables[0].Rows[0]["TouWei"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QgfyPj"] != null)
                {
                    this.QgfyPj = ds.Tables[0].Rows[0]["QgfyPj"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Face"] != null && ds.Tables[0].Rows[0]["Qgjc_Face"].ToString() != "")
                {
                    this.Qgjc_Face = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Face"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Face_other"] != null)
                {
                    this.Qgjc_Face_other = ds.Tables[0].Rows[0]["Qgjc_Face_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Skin"] != null && ds.Tables[0].Rows[0]["Qgjc_Skin"].ToString() != "")
                {
                    this.Qgjc_Skin = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Skin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Skin_other"] != null)
                {
                    this.Qgjc_Skin_other = ds.Tables[0].Rows[0]["Qgjc_Skin_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_QianXin"] != null && ds.Tables[0].Rows[0]["Qgjc_QianXin"].ToString() != "")
                {
                    this.Qgjc_QianXin = int.Parse(ds.Tables[0].Rows[0]["Qgjc_QianXin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_QianXinCmx"] != null && ds.Tables[0].Rows[0]["Qgjc_QianXinCmx"].ToString() != "")
                {
                    this.Qgjc_QianXinCmx = decimal.Parse(ds.Tables[0].Rows[0]["Qgjc_QianXinCmx"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_QianXinCm"] != null && ds.Tables[0].Rows[0]["Qgjc_QianXinCm"].ToString() != "")
                {
                    this.Qgjc_QianXinCm = decimal.Parse(ds.Tables[0].Rows[0]["Qgjc_QianXinCm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Neck"] != null && ds.Tables[0].Rows[0]["Qgjc_Neck"].ToString() != "")
                {
                    this.Qgjc_Neck = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Neck"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Neck_other"] != null)
                {
                    this.Qgjc_Neck_other = ds.Tables[0].Rows[0]["Qgjc_Neck_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Eyes"] != null && ds.Tables[0].Rows[0]["Qgjc_Eyes"].ToString() != "")
                {
                    this.Qgjc_Eyes = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Eyes"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Eyes_other"] != null)
                {
                    this.Qgjc_Eyes_other = ds.Tables[0].Rows[0]["Qgjc_Eyes_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Ears"] != null && ds.Tables[0].Rows[0]["Qgjc_Ears"].ToString() != "")
                {
                    this.Qgjc_Ears = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Ears"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Ears_other"] != null)
                {
                    this.Qgjc_Ears_other = ds.Tables[0].Rows[0]["Qgjc_Ears_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Hearing"] != null && ds.Tables[0].Rows[0]["Qgjc_Hearing"].ToString() != "")
                {
                    this.Qgjc_Hearing = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Hearing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Hearing_other"] != null)
                {
                    this.Qgjc_Hearing_other = ds.Tables[0].Rows[0]["Qgjc_Hearing_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Mouth"] != null && ds.Tables[0].Rows[0]["Qgjc_Mouth"].ToString() != "")
                {
                    this.Qgjc_Mouth = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Mouth"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Mouth_other"] != null)
                {
                    this.Qgjc_Mouth_other = ds.Tables[0].Rows[0]["Qgjc_Mouth_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_teethnum"] != null && ds.Tables[0].Rows[0]["Qgjc_teethnum"].ToString() != "")
                {
                    this.Qgjc_teethnum = int.Parse(ds.Tables[0].Rows[0]["Qgjc_teethnum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_teethnum2"] != null && ds.Tables[0].Rows[0]["Qgjc_teethnum2"].ToString() != "")
                {
                    this.Qgjc_teethnum2 = int.Parse(ds.Tables[0].Rows[0]["Qgjc_teethnum2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_HeartLung"] != null && ds.Tables[0].Rows[0]["Qgjc_HeartLung"].ToString() != "")
                {
                    this.Qgjc_HeartLung = int.Parse(ds.Tables[0].Rows[0]["Qgjc_HeartLung"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_HeartLung_other"] != null)
                {
                    this.Qgjc_HeartLung_other = ds.Tables[0].Rows[0]["Qgjc_HeartLung_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Abdomen"] != null && ds.Tables[0].Rows[0]["Qgjc_Abdomen"].ToString() != "")
                {
                    this.Qgjc_Abdomen = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Abdomen"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Abdomen_other"] != null)
                {
                    this.Qgjc_Abdomen_other = ds.Tables[0].Rows[0]["Qgjc_Abdomen_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Navel"] != null && ds.Tables[0].Rows[0]["Qgjc_Navel"].ToString() != "")
                {
                    this.Qgjc_Navel = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Navel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Navel_other"] != null)
                {
                    this.Qgjc_Navel_other = ds.Tables[0].Rows[0]["Qgjc_Navel_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Limbs"] != null && ds.Tables[0].Rows[0]["Qgjc_Limbs"].ToString() != "")
                {
                    this.Qgjc_Limbs = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Limbs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Limbs_other"] != null)
                {
                    this.Qgjc_Limbs_other = ds.Tables[0].Rows[0]["Qgjc_Limbs_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_RicketsSymptom"] != null)
                {
                    this.Qgjc_RicketsSymptom = ds.Tables[0].Rows[0]["Qgjc_RicketsSymptom"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_RicketsSymptom_other"] != null)
                {
                    this.Qgjc_RicketsSymptom_other = ds.Tables[0].Rows[0]["Qgjc_RicketsSymptom_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_RicketsBody"] != null)
                {
                    this.Qgjc_RicketsBody = ds.Tables[0].Rows[0]["Qgjc_RicketsBody"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Genital"] != null && ds.Tables[0].Rows[0]["Qgjc_Genital"].ToString() != "")
                {
                    this.Qgjc_Genital = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Genital"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Genital_other"] != null)
                {
                    this.Qgjc_Genital_other = ds.Tables[0].Rows[0]["Qgjc_Genital_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Anus"] != null && ds.Tables[0].Rows[0]["Qgjc_Anus"].ToString() != "")
                {
                    this.Qgjc_Anus = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Anus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Anus_other"] != null)
                {
                    this.Qgjc_Anus_other = ds.Tables[0].Rows[0]["Qgjc_Anus_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Xhdb"] != null && ds.Tables[0].Rows[0]["Qgjc_Xhdb"].ToString() != "")
                {
                    this.Qgjc_Xhdb = decimal.Parse(ds.Tables[0].Rows[0]["Qgjc_Xhdb"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VitaminD"] != null && ds.Tables[0].Rows[0]["VitaminD"].ToString() != "")
                {
                    this.VitaminD = decimal.Parse(ds.Tables[0].Rows[0]["VitaminD"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Diseases"] != null)
                {
                    this.Diseases = ds.Tables[0].Rows[0]["Diseases"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FaYuPingGu"] != null)
                {
                    this.FaYuPingGu = ds.Tables[0].Rows[0]["FaYuPingGu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Others"] != null)
                {
                    this.Others = ds.Tables[0].Rows[0]["Others"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zz_Yj"] != null && ds.Tables[0].Rows[0]["Zz_Yj"].ToString() != "")
                {
                    this.Zz_Yj = int.Parse(ds.Tables[0].Rows[0]["Zz_Yj"].ToString());
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
                if (ds.Tables[0].Rows[0]["Guide"] != null)
                {
                    this.Guide = ds.Tables[0].Rows[0]["Guide"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NextVistAddress"] != null)
                {
                    this.NextVistAddress = ds.Tables[0].Rows[0]["NextVistAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFJG"] != null)
                {
                    this.SFJG = ds.Tables[0].Rows[0]["SFJG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GuideQt"] != null)
                {
                    this.GuideQt = ds.Tables[0].Rows[0]["GuideQt"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_Child_JKJC]");
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
            strSql.Append("select count(1) from [NHS_Child_JKJC]");
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
            strSql.Append("insert into [NHS_Child_JKJC] (");
            strSql.Append("UnvID,VisitItems,VisitDate,wyfs,Outdoors,Weight,Weight_other,Height,Height_other,TouWei,QgfyPj,Qgjc_Face,Qgjc_Face_other,Qgjc_Skin,Qgjc_Skin_other,Qgjc_QianXin,Qgjc_QianXinCmx,Qgjc_QianXinCm,Qgjc_Neck,Qgjc_Neck_other,Qgjc_Eyes,Qgjc_Eyes_other,Qgjc_Ears,Qgjc_Ears_other,Qgjc_Hearing,Qgjc_Hearing_other,Qgjc_Mouth,Qgjc_Mouth_other,Qgjc_teethnum,Qgjc_teethnum2,Qgjc_HeartLung,Qgjc_HeartLung_other,Qgjc_Abdomen,Qgjc_Abdomen_other,Qgjc_Navel,Qgjc_Navel_other,Qgjc_Limbs,Qgjc_Limbs_other,Qgjc_RicketsSymptom,Qgjc_RicketsSymptom_other,Qgjc_RicketsBody,Qgjc_Genital,Qgjc_Genital_other,Qgjc_Anus,Qgjc_Anus_other,Qgjc_Xhdb,VitaminD,Diseases,FaYuPingGu,Others,Zz_Yj,Zz_Yy,Zz_Jg,NextVisitDate,Doctor,CreateDate,UserID,Guide,NextVistAddress,SFJG,GuideQt)");
            strSql.Append(" values (");
            strSql.Append("@UnvID,@VisitItems,@VisitDate,@wyfs,@Outdoors,@Weight,@Weight_other,@Height,@Height_other,@TouWei,@QgfyPj,@Qgjc_Face,@Qgjc_Face_other,@Qgjc_Skin,@Qgjc_Skin_other,@Qgjc_QianXin,@Qgjc_QianXinCmx,@Qgjc_QianXinCm,@Qgjc_Neck,@Qgjc_Neck_other,@Qgjc_Eyes,@Qgjc_Eyes_other,@Qgjc_Ears,@Qgjc_Ears_other,@Qgjc_Hearing,@Qgjc_Hearing_other,@Qgjc_Mouth,@Qgjc_Mouth_other,@Qgjc_teethnum,@Qgjc_teethnum2,@Qgjc_HeartLung,@Qgjc_HeartLung_other,@Qgjc_Abdomen,@Qgjc_Abdomen_other,@Qgjc_Navel,@Qgjc_Navel_other,@Qgjc_Limbs,@Qgjc_Limbs_other,@Qgjc_RicketsSymptom,@Qgjc_RicketsSymptom_other,@Qgjc_RicketsBody,@Qgjc_Genital,@Qgjc_Genital_other,@Qgjc_Anus,@Qgjc_Anus_other,@Qgjc_Xhdb,@VitaminD,@Diseases,@FaYuPingGu,@Others,@Zz_Yj,@Zz_Yy,@Zz_Jg,@NextVisitDate,@Doctor,@CreateDate,@UserID,@Guide,@NextVistAddress,@SFJG,@GuideQt)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@VisitItems", SqlDbType.Int,4),
					new SqlParameter("@VisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@wyfs", SqlDbType.Int,4),
					new SqlParameter("@Outdoors", SqlDbType.Float,8),
					new SqlParameter("@Weight", SqlDbType.Float,8),
					new SqlParameter("@Weight_other", SqlDbType.Int,4),
					new SqlParameter("@Height", SqlDbType.Float,8),
					new SqlParameter("@Height_other", SqlDbType.Int,4),
					new SqlParameter("@TouWei", SqlDbType.Float,8),
					new SqlParameter("@QgfyPj", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Face", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Face_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Skin", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Skin_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_QianXin", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_QianXinCmx", SqlDbType.Float,8),
					new SqlParameter("@Qgjc_QianXinCm", SqlDbType.Float,8),
					new SqlParameter("@Qgjc_Neck", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Neck_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Eyes", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Eyes_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Ears", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Ears_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Hearing", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Hearing_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Mouth", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Mouth_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_teethnum", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_teethnum2", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_HeartLung", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_HeartLung_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Abdomen", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Abdomen_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Navel", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Navel_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Limbs", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Limbs_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_RicketsSymptom", SqlDbType.VarChar,20),
					new SqlParameter("@Qgjc_RicketsSymptom_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_RicketsBody", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Genital", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Genital_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Anus", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Anus_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Xhdb", SqlDbType.Float,8),
					new SqlParameter("@VitaminD", SqlDbType.Float,8),
					new SqlParameter("@Diseases", SqlDbType.VarChar,50),
					new SqlParameter("@FaYuPingGu", SqlDbType.VarChar,50),
					new SqlParameter("@Others", SqlDbType.VarChar,50),
					new SqlParameter("@Zz_Yj", SqlDbType.Int,4),
					new SqlParameter("@Zz_Yy", SqlDbType.NVarChar,50),
					new SqlParameter("@Zz_Jg", SqlDbType.NVarChar,50),
					new SqlParameter("@NextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Doctor", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Guide", SqlDbType.VarChar,50),
					new SqlParameter("@NextVistAddress", SqlDbType.VarChar,50),
					new SqlParameter("@SFJG", SqlDbType.VarChar,50),
					new SqlParameter("@GuideQt", SqlDbType.VarChar,50)};
            parameters[0].Value = UnvID;
            parameters[1].Value = VisitItems;
            parameters[2].Value = VisitDate;
            parameters[3].Value = wyfs;
            parameters[4].Value = Outdoors;
            parameters[5].Value = Weight;
            parameters[6].Value = Weight_other;
            parameters[7].Value = Height;
            parameters[8].Value = Height_other;
            parameters[9].Value = TouWei;
            parameters[10].Value = QgfyPj;
            parameters[11].Value = Qgjc_Face;
            parameters[12].Value = Qgjc_Face_other;
            parameters[13].Value = Qgjc_Skin;
            parameters[14].Value = Qgjc_Skin_other;
            parameters[15].Value = Qgjc_QianXin;
            parameters[16].Value = Qgjc_QianXinCmx;
            parameters[17].Value = Qgjc_QianXinCm;
            parameters[18].Value = Qgjc_Neck;
            parameters[19].Value = Qgjc_Neck_other;
            parameters[20].Value = Qgjc_Eyes;
            parameters[21].Value = Qgjc_Eyes_other;
            parameters[22].Value = Qgjc_Ears;
            parameters[23].Value = Qgjc_Ears_other;
            parameters[24].Value = Qgjc_Hearing;
            parameters[25].Value = Qgjc_Hearing_other;
            parameters[26].Value = Qgjc_Mouth;
            parameters[27].Value = Qgjc_Mouth_other;
            parameters[28].Value = Qgjc_teethnum;
            parameters[29].Value = Qgjc_teethnum2;
            parameters[30].Value = Qgjc_HeartLung;
            parameters[31].Value = Qgjc_HeartLung_other;
            parameters[32].Value = Qgjc_Abdomen;
            parameters[33].Value = Qgjc_Abdomen_other;
            parameters[34].Value = Qgjc_Navel;
            parameters[35].Value = Qgjc_Navel_other;
            parameters[36].Value = Qgjc_Limbs;
            parameters[37].Value = Qgjc_Limbs_other;
            parameters[38].Value = Qgjc_RicketsSymptom;
            parameters[39].Value = Qgjc_RicketsSymptom_other;
            parameters[40].Value = Qgjc_RicketsBody;
            parameters[41].Value = Qgjc_Genital;
            parameters[42].Value = Qgjc_Genital_other;
            parameters[43].Value = Qgjc_Anus;
            parameters[44].Value = Qgjc_Anus_other;
            parameters[45].Value = Qgjc_Xhdb;
            parameters[46].Value = VitaminD;
            parameters[47].Value = Diseases;
            parameters[48].Value = FaYuPingGu;
            parameters[49].Value = Others;
            parameters[50].Value = Zz_Yj;
            parameters[51].Value = Zz_Yy;
            parameters[52].Value = Zz_Jg;
            parameters[53].Value = NextVisitDate;
            parameters[54].Value = Doctor;
            parameters[55].Value = CreateDate;
            parameters[56].Value = UserID;
            parameters[57].Value = Guide;
            parameters[58].Value = NextVistAddress;
            parameters[59].Value = SFJG;
            parameters[60].Value = GuideQt;

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
            strSql.Append("update [NHS_Child_JKJC] set ");
            strSql.Append("UnvID=@UnvID,");
            strSql.Append("VisitItems=@VisitItems,");
            strSql.Append("VisitDate=@VisitDate,");
            strSql.Append("wyfs=@wyfs,");
            strSql.Append("Outdoors=@Outdoors,");
            strSql.Append("Weight=@Weight,");
            strSql.Append("Weight_other=@Weight_other,");
            strSql.Append("Height=@Height,");
            strSql.Append("Height_other=@Height_other,");
            strSql.Append("TouWei=@TouWei,");
            strSql.Append("QgfyPj=@QgfyPj,");
            strSql.Append("Qgjc_Face=@Qgjc_Face,");
            strSql.Append("Qgjc_Face_other=@Qgjc_Face_other,");
            strSql.Append("Qgjc_Skin=@Qgjc_Skin,");
            strSql.Append("Qgjc_Skin_other=@Qgjc_Skin_other,");
            strSql.Append("Qgjc_QianXin=@Qgjc_QianXin,");
            strSql.Append("Qgjc_QianXinCmx=@Qgjc_QianXinCmx,");
            strSql.Append("Qgjc_QianXinCm=@Qgjc_QianXinCm,");
            strSql.Append("Qgjc_Neck=@Qgjc_Neck,");
            strSql.Append("Qgjc_Neck_other=@Qgjc_Neck_other,");
            strSql.Append("Qgjc_Eyes=@Qgjc_Eyes,");
            strSql.Append("Qgjc_Eyes_other=@Qgjc_Eyes_other,");
            strSql.Append("Qgjc_Ears=@Qgjc_Ears,");
            strSql.Append("Qgjc_Ears_other=@Qgjc_Ears_other,");
            strSql.Append("Qgjc_Hearing=@Qgjc_Hearing,");
            strSql.Append("Qgjc_Hearing_other=@Qgjc_Hearing_other,");
            strSql.Append("Qgjc_Mouth=@Qgjc_Mouth,");
            strSql.Append("Qgjc_Mouth_other=@Qgjc_Mouth_other,");
            strSql.Append("Qgjc_teethnum=@Qgjc_teethnum,");
            strSql.Append("Qgjc_teethnum2=@Qgjc_teethnum2,");
            strSql.Append("Qgjc_HeartLung=@Qgjc_HeartLung,");
            strSql.Append("Qgjc_HeartLung_other=@Qgjc_HeartLung_other,");
            strSql.Append("Qgjc_Abdomen=@Qgjc_Abdomen,");
            strSql.Append("Qgjc_Abdomen_other=@Qgjc_Abdomen_other,");
            strSql.Append("Qgjc_Navel=@Qgjc_Navel,");
            strSql.Append("Qgjc_Navel_other=@Qgjc_Navel_other,");
            strSql.Append("Qgjc_Limbs=@Qgjc_Limbs,");
            strSql.Append("Qgjc_Limbs_other=@Qgjc_Limbs_other,");
            strSql.Append("Qgjc_RicketsSymptom=@Qgjc_RicketsSymptom,");
            strSql.Append("Qgjc_RicketsSymptom_other=@Qgjc_RicketsSymptom_other,");
            strSql.Append("Qgjc_RicketsBody=@Qgjc_RicketsBody,");
            strSql.Append("Qgjc_Genital=@Qgjc_Genital,");
            strSql.Append("Qgjc_Genital_other=@Qgjc_Genital_other,");
            strSql.Append("Qgjc_Anus=@Qgjc_Anus,");
            strSql.Append("Qgjc_Anus_other=@Qgjc_Anus_other,");
            strSql.Append("Qgjc_Xhdb=@Qgjc_Xhdb,");
            strSql.Append("VitaminD=@VitaminD,");
            strSql.Append("Diseases=@Diseases,");
            strSql.Append("FaYuPingGu=@FaYuPingGu,");
            strSql.Append("Others=@Others,");
            strSql.Append("Zz_Yj=@Zz_Yj,");
            strSql.Append("Zz_Yy=@Zz_Yy,");
            strSql.Append("Zz_Jg=@Zz_Jg,");
            strSql.Append("NextVisitDate=@NextVisitDate,");
            strSql.Append("Doctor=@Doctor,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("Guide=@Guide,");
            strSql.Append("NextVistAddress=@NextVistAddress,");
            strSql.Append("SFJG=@SFJG,");
            strSql.Append("GuideQt=@GuideQt");
            strSql.Append(" where CommID=@CommID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@VisitItems", SqlDbType.Int,4),
					new SqlParameter("@VisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@wyfs", SqlDbType.Int,4),
					new SqlParameter("@Outdoors", SqlDbType.Float,8),
					new SqlParameter("@Weight", SqlDbType.Float,8),
					new SqlParameter("@Weight_other", SqlDbType.Int,4),
					new SqlParameter("@Height", SqlDbType.Float,8),
					new SqlParameter("@Height_other", SqlDbType.Int,4),
					new SqlParameter("@TouWei", SqlDbType.Float,8),
					new SqlParameter("@QgfyPj", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Face", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Face_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Skin", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Skin_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_QianXin", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_QianXinCmx", SqlDbType.Float,8),
					new SqlParameter("@Qgjc_QianXinCm", SqlDbType.Float,8),
					new SqlParameter("@Qgjc_Neck", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Neck_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Eyes", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Eyes_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Ears", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Ears_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Hearing", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Hearing_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Mouth", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Mouth_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_teethnum", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_teethnum2", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_HeartLung", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_HeartLung_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Abdomen", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Abdomen_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Navel", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Navel_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Limbs", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Limbs_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_RicketsSymptom", SqlDbType.VarChar,20),
					new SqlParameter("@Qgjc_RicketsSymptom_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_RicketsBody", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Genital", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Genital_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Anus", SqlDbType.Int,4),
					new SqlParameter("@Qgjc_Anus_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qgjc_Xhdb", SqlDbType.Float,8),
					new SqlParameter("@VitaminD", SqlDbType.Float,8),
					new SqlParameter("@Diseases", SqlDbType.VarChar,50),
					new SqlParameter("@FaYuPingGu", SqlDbType.VarChar,50),
					new SqlParameter("@Others", SqlDbType.VarChar,50),
					new SqlParameter("@Zz_Yj", SqlDbType.Int,4),
					new SqlParameter("@Zz_Yy", SqlDbType.NVarChar,50),
					new SqlParameter("@Zz_Jg", SqlDbType.NVarChar,50),
					new SqlParameter("@NextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Doctor", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Guide", SqlDbType.VarChar,50),
					new SqlParameter("@NextVistAddress", SqlDbType.VarChar,50),
					new SqlParameter("@SFJG", SqlDbType.VarChar,50),
					new SqlParameter("@GuideQt", SqlDbType.VarChar,50),
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = UnvID;
            parameters[1].Value = VisitItems;
            parameters[2].Value = VisitDate;
            parameters[3].Value = wyfs;
            parameters[4].Value = Outdoors;
            parameters[5].Value = Weight;
            parameters[6].Value = Weight_other;
            parameters[7].Value = Height;
            parameters[8].Value = Height_other;
            parameters[9].Value = TouWei;
            parameters[10].Value = QgfyPj;
            parameters[11].Value = Qgjc_Face;
            parameters[12].Value = Qgjc_Face_other;
            parameters[13].Value = Qgjc_Skin;
            parameters[14].Value = Qgjc_Skin_other;
            parameters[15].Value = Qgjc_QianXin;
            parameters[16].Value = Qgjc_QianXinCmx;
            parameters[17].Value = Qgjc_QianXinCm;
            parameters[18].Value = Qgjc_Neck;
            parameters[19].Value = Qgjc_Neck_other;
            parameters[20].Value = Qgjc_Eyes;
            parameters[21].Value = Qgjc_Eyes_other;
            parameters[22].Value = Qgjc_Ears;
            parameters[23].Value = Qgjc_Ears_other;
            parameters[24].Value = Qgjc_Hearing;
            parameters[25].Value = Qgjc_Hearing_other;
            parameters[26].Value = Qgjc_Mouth;
            parameters[27].Value = Qgjc_Mouth_other;
            parameters[28].Value = Qgjc_teethnum;
            parameters[29].Value = Qgjc_teethnum2;
            parameters[30].Value = Qgjc_HeartLung;
            parameters[31].Value = Qgjc_HeartLung_other;
            parameters[32].Value = Qgjc_Abdomen;
            parameters[33].Value = Qgjc_Abdomen_other;
            parameters[34].Value = Qgjc_Navel;
            parameters[35].Value = Qgjc_Navel_other;
            parameters[36].Value = Qgjc_Limbs;
            parameters[37].Value = Qgjc_Limbs_other;
            parameters[38].Value = Qgjc_RicketsSymptom;
            parameters[39].Value = Qgjc_RicketsSymptom_other;
            parameters[40].Value = Qgjc_RicketsBody;
            parameters[41].Value = Qgjc_Genital;
            parameters[42].Value = Qgjc_Genital_other;
            parameters[43].Value = Qgjc_Anus;
            parameters[44].Value = Qgjc_Anus_other;
            parameters[45].Value = Qgjc_Xhdb;
            parameters[46].Value = VitaminD;
            parameters[47].Value = Diseases;
            parameters[48].Value = FaYuPingGu;
            parameters[49].Value = Others;
            parameters[50].Value = Zz_Yj;
            parameters[51].Value = Zz_Yy;
            parameters[52].Value = Zz_Jg;
            parameters[53].Value = NextVisitDate;
            parameters[54].Value = Doctor;
            parameters[55].Value = CreateDate;
            parameters[56].Value = UserID;
            parameters[57].Value = Guide;
            parameters[58].Value = NextVistAddress;
            parameters[59].Value = SFJG;
            parameters[60].Value = GuideQt;
            parameters[61].Value = CommID;

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
            strSql.Append("delete from [NHS_Child_JKJC] ");
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
            strSql.Append("select CommID,UnvID,VisitItems,VisitDate,wyfs,Outdoors,Weight,Weight_other,Height,Height_other,TouWei,QgfyPj,Qgjc_Face,Qgjc_Face_other,Qgjc_Skin,Qgjc_Skin_other,Qgjc_QianXin,Qgjc_QianXinCmx,Qgjc_QianXinCm,Qgjc_Neck,Qgjc_Neck_other,Qgjc_Eyes,Qgjc_Eyes_other,Qgjc_Ears,Qgjc_Ears_other,Qgjc_Hearing,Qgjc_Hearing_other,Qgjc_Mouth,Qgjc_Mouth_other,Qgjc_teethnum,Qgjc_teethnum2,Qgjc_HeartLung,Qgjc_HeartLung_other,Qgjc_Abdomen,Qgjc_Abdomen_other,Qgjc_Navel,Qgjc_Navel_other,Qgjc_Limbs,Qgjc_Limbs_other,Qgjc_RicketsSymptom,Qgjc_RicketsSymptom_other,Qgjc_RicketsBody,Qgjc_Genital,Qgjc_Genital_other,Qgjc_Anus,Qgjc_Anus_other,Qgjc_Xhdb,VitaminD,Diseases,FaYuPingGu,Others,Zz_Yj,Zz_Yy,Zz_Jg,NextVisitDate,Doctor,CreateDate,UserID,Guide,NextVistAddress,SFJG,GuideQt ");
            strSql.Append(" FROM [NHS_Child_JKJC] ");
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
                if (ds.Tables[0].Rows[0]["wyfs"] != null && ds.Tables[0].Rows[0]["wyfs"].ToString() != "")
                {
                    this.wyfs = int.Parse(ds.Tables[0].Rows[0]["wyfs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Outdoors"] != null && ds.Tables[0].Rows[0]["Outdoors"].ToString() != "")
                {
                    this.Outdoors = decimal.Parse(ds.Tables[0].Rows[0]["Outdoors"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Weight"] != null && ds.Tables[0].Rows[0]["Weight"].ToString() != "")
                {
                    this.Weight = decimal.Parse(ds.Tables[0].Rows[0]["Weight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Weight_other"] != null && ds.Tables[0].Rows[0]["Weight_other"].ToString() != "")
                {
                    this.Weight_other = int.Parse(ds.Tables[0].Rows[0]["Weight_other"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Height"] != null && ds.Tables[0].Rows[0]["Height"].ToString() != "")
                {
                    this.Height = decimal.Parse(ds.Tables[0].Rows[0]["Height"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Height_other"] != null && ds.Tables[0].Rows[0]["Height_other"].ToString() != "")
                {
                    this.Height_other = int.Parse(ds.Tables[0].Rows[0]["Height_other"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TouWei"] != null && ds.Tables[0].Rows[0]["TouWei"].ToString() != "")
                {
                    this.TouWei = decimal.Parse(ds.Tables[0].Rows[0]["TouWei"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QgfyPj"] != null)
                {
                    this.QgfyPj = ds.Tables[0].Rows[0]["QgfyPj"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Face"] != null && ds.Tables[0].Rows[0]["Qgjc_Face"].ToString() != "")
                {
                    this.Qgjc_Face = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Face"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Face_other"] != null)
                {
                    this.Qgjc_Face_other = ds.Tables[0].Rows[0]["Qgjc_Face_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Skin"] != null && ds.Tables[0].Rows[0]["Qgjc_Skin"].ToString() != "")
                {
                    this.Qgjc_Skin = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Skin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Skin_other"] != null)
                {
                    this.Qgjc_Skin_other = ds.Tables[0].Rows[0]["Qgjc_Skin_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_QianXin"] != null && ds.Tables[0].Rows[0]["Qgjc_QianXin"].ToString() != "")
                {
                    this.Qgjc_QianXin = int.Parse(ds.Tables[0].Rows[0]["Qgjc_QianXin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_QianXinCmx"] != null && ds.Tables[0].Rows[0]["Qgjc_QianXinCmx"].ToString() != "")
                {
                    this.Qgjc_QianXinCmx = decimal.Parse(ds.Tables[0].Rows[0]["Qgjc_QianXinCmx"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_QianXinCm"] != null && ds.Tables[0].Rows[0]["Qgjc_QianXinCm"].ToString() != "")
                {
                    this.Qgjc_QianXinCm = decimal.Parse(ds.Tables[0].Rows[0]["Qgjc_QianXinCm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Neck"] != null && ds.Tables[0].Rows[0]["Qgjc_Neck"].ToString() != "")
                {
                    this.Qgjc_Neck = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Neck"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Neck_other"] != null)
                {
                    this.Qgjc_Neck_other = ds.Tables[0].Rows[0]["Qgjc_Neck_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Eyes"] != null && ds.Tables[0].Rows[0]["Qgjc_Eyes"].ToString() != "")
                {
                    this.Qgjc_Eyes = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Eyes"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Eyes_other"] != null)
                {
                    this.Qgjc_Eyes_other = ds.Tables[0].Rows[0]["Qgjc_Eyes_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Ears"] != null && ds.Tables[0].Rows[0]["Qgjc_Ears"].ToString() != "")
                {
                    this.Qgjc_Ears = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Ears"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Ears_other"] != null)
                {
                    this.Qgjc_Ears_other = ds.Tables[0].Rows[0]["Qgjc_Ears_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Hearing"] != null && ds.Tables[0].Rows[0]["Qgjc_Hearing"].ToString() != "")
                {
                    this.Qgjc_Hearing = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Hearing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Hearing_other"] != null)
                {
                    this.Qgjc_Hearing_other = ds.Tables[0].Rows[0]["Qgjc_Hearing_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Mouth"] != null && ds.Tables[0].Rows[0]["Qgjc_Mouth"].ToString() != "")
                {
                    this.Qgjc_Mouth = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Mouth"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Mouth_other"] != null)
                {
                    this.Qgjc_Mouth_other = ds.Tables[0].Rows[0]["Qgjc_Mouth_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_teethnum"] != null && ds.Tables[0].Rows[0]["Qgjc_teethnum"].ToString() != "")
                {
                    this.Qgjc_teethnum = int.Parse(ds.Tables[0].Rows[0]["Qgjc_teethnum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_teethnum2"] != null && ds.Tables[0].Rows[0]["Qgjc_teethnum2"].ToString() != "")
                {
                    this.Qgjc_teethnum2 = int.Parse(ds.Tables[0].Rows[0]["Qgjc_teethnum2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_HeartLung"] != null && ds.Tables[0].Rows[0]["Qgjc_HeartLung"].ToString() != "")
                {
                    this.Qgjc_HeartLung = int.Parse(ds.Tables[0].Rows[0]["Qgjc_HeartLung"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_HeartLung_other"] != null)
                {
                    this.Qgjc_HeartLung_other = ds.Tables[0].Rows[0]["Qgjc_HeartLung_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Abdomen"] != null && ds.Tables[0].Rows[0]["Qgjc_Abdomen"].ToString() != "")
                {
                    this.Qgjc_Abdomen = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Abdomen"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Abdomen_other"] != null)
                {
                    this.Qgjc_Abdomen_other = ds.Tables[0].Rows[0]["Qgjc_Abdomen_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Navel"] != null && ds.Tables[0].Rows[0]["Qgjc_Navel"].ToString() != "")
                {
                    this.Qgjc_Navel = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Navel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Navel_other"] != null)
                {
                    this.Qgjc_Navel_other = ds.Tables[0].Rows[0]["Qgjc_Navel_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Limbs"] != null && ds.Tables[0].Rows[0]["Qgjc_Limbs"].ToString() != "")
                {
                    this.Qgjc_Limbs = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Limbs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Limbs_other"] != null)
                {
                    this.Qgjc_Limbs_other = ds.Tables[0].Rows[0]["Qgjc_Limbs_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_RicketsSymptom"] != null)
                {
                    this.Qgjc_RicketsSymptom = ds.Tables[0].Rows[0]["Qgjc_RicketsSymptom"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_RicketsSymptom_other"] != null)
                {
                    this.Qgjc_RicketsSymptom_other = ds.Tables[0].Rows[0]["Qgjc_RicketsSymptom_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_RicketsBody"] != null)
                {
                    this.Qgjc_RicketsBody = ds.Tables[0].Rows[0]["Qgjc_RicketsBody"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Genital"] != null && ds.Tables[0].Rows[0]["Qgjc_Genital"].ToString() != "")
                {
                    this.Qgjc_Genital = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Genital"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Genital_other"] != null)
                {
                    this.Qgjc_Genital_other = ds.Tables[0].Rows[0]["Qgjc_Genital_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Anus"] != null && ds.Tables[0].Rows[0]["Qgjc_Anus"].ToString() != "")
                {
                    this.Qgjc_Anus = int.Parse(ds.Tables[0].Rows[0]["Qgjc_Anus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Anus_other"] != null)
                {
                    this.Qgjc_Anus_other = ds.Tables[0].Rows[0]["Qgjc_Anus_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qgjc_Xhdb"] != null && ds.Tables[0].Rows[0]["Qgjc_Xhdb"].ToString() != "")
                {
                    this.Qgjc_Xhdb = decimal.Parse(ds.Tables[0].Rows[0]["Qgjc_Xhdb"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VitaminD"] != null && ds.Tables[0].Rows[0]["VitaminD"].ToString() != "")
                {
                    this.VitaminD = decimal.Parse(ds.Tables[0].Rows[0]["VitaminD"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Diseases"] != null)
                {
                    this.Diseases = ds.Tables[0].Rows[0]["Diseases"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FaYuPingGu"] != null)
                {
                    this.FaYuPingGu = ds.Tables[0].Rows[0]["FaYuPingGu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Others"] != null)
                {
                    this.Others = ds.Tables[0].Rows[0]["Others"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zz_Yj"] != null && ds.Tables[0].Rows[0]["Zz_Yj"].ToString() != "")
                {
                    this.Zz_Yj = int.Parse(ds.Tables[0].Rows[0]["Zz_Yj"].ToString());
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
                if (ds.Tables[0].Rows[0]["Guide"] != null)
                {
                    this.Guide = ds.Tables[0].Rows[0]["Guide"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NextVistAddress"] != null)
                {
                    this.NextVistAddress = ds.Tables[0].Rows[0]["NextVistAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SFJG"] != null)
                {
                    this.SFJG = ds.Tables[0].Rows[0]["SFJG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GuideQt"] != null)
                {
                    this.GuideQt = ds.Tables[0].Rows[0]["GuideQt"].ToString();
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
            strSql.Append(" FROM [NHS_Child_JKJC] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
