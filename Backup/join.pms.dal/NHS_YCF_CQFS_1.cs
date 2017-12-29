using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using UNV.Comm.DataBase;
using System.Data.SqlClient;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_YCF_CQFS_1。
    /// </summary>
    [Serializable]
    public partial class NHS_YCF_CQFS_1
    {
        public NHS_YCF_CQFS_1()
        { }
        #region Model
        private int _commid;
        private string _unvid;
        private decimal? _fnweight;
        private decimal? _fntzzs;
        private decimal? _fnxymax;
        private decimal? _fnxymin;
        private bool _tzxz;
        private string _tzxz_other;
        private bool _tzfb;
        private string _tzfb_other;
        private bool _fkjc_wy;
        private string _fkjc_wy_other;
        private bool _fkjc_yd;
        private string _fkjc_yd_other;
        private bool _fkjc_gj;
        private string _fkjc_gj_other;
        private bool _fkjc_zg;
        private string _fkjc_zg_other;
        private bool _fkjc_fj;
        private string _fkjc_fj_other;
        private decimal _fzjc_xcg_xhdb;
        private decimal? _fzjc_xcg_bxb;
        private decimal? _fzjc_xcg_xxb;
        private string _fzjc_xcg_qt;
        private int? _fzjc_ncg_ndb;
        private int? _fzjc_ncg_nt;
        private int? _fzjc_ncg_ntt;
        private int? _fzjc_ncg_nqx;
        private string _fzjc_ncg_qt;
        private string _fzjc_xx_abo;
        private string _fzjc_xx_rh;
        private decimal? _fzjc_xt;
        private decimal? _fzjc_ggn_xqgbzam;
        private decimal? _fzjc_ggn_xqgczam;
        private decimal? _fzjc_ggn_xdb;
        private decimal? _fzjc_ggn_zdhs;
        private decimal? _fzjc_ggn_jhdhs;
        private decimal? _fzjc_sgn_xqjq;
        private decimal? _fzjc_sgn_xnsd;
        private decimal? _fzjc_sgn_xjnd;
        private decimal? _fzjc_sgn_xnnd;
        private string _fzjc_ydfmw;
        private string _fzjc_ydfmw_qt;
        private int? _fzjc_ydqjd;
        private bool _fzjc_yg_bmky;
        private bool _fzjc_yg_bmkt;
        private bool _fzjc_yg_eky;
        private bool _fzjc_yg_ekt;
        private bool _fzjc_yg_hxkt;
        private int? _fzjc_mdxqxsy;
        private bool _fzjc_mdxqxsy_sfgw;
        private string _fzjc_mdxqxsy_gwsm;
        private int? _fzjc_hiv_jc;
        private int? _fzjc_mdjc;
        private int? _fzjc_hiv;
        private string _fzjc_bc;
        private string _ztpg;
        private string _bjzd;
        private bool _zz_yw;
        private string _zz_yy;
        private string _zz_jg;
        private DateTime? _nextvisitdate;
        private string _doctor;
        private string _sfjg;
        private DateTime? _createdate;
        private int? _userid;
        private DateTime? _curvisitdate;
        private string _bjzdqt;
        private string _ztpgqt;
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
        public decimal? FnWeight
        {
            set { _fnweight = value; }
            get { return _fnweight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FnTzzs
        {
            set { _fntzzs = value; }
            get { return _fntzzs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FnXyMax
        {
            set { _fnxymax = value; }
            get { return _fnxymax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FnXyMin
        {
            set { _fnxymin = value; }
            get { return _fnxymin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool TzXz
        {
            set { _tzxz = value; }
            get { return _tzxz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TzXz_Other
        {
            set { _tzxz_other = value; }
            get { return _tzxz_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool TzFb
        {
            set { _tzfb = value; }
            get { return _tzfb; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TzFb_Other
        {
            set { _tzfb_other = value; }
            get { return _tzfb_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Fkjc_Wy
        {
            set { _fkjc_wy = value; }
            get { return _fkjc_wy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fkjc_Wy_Other
        {
            set { _fkjc_wy_other = value; }
            get { return _fkjc_wy_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Fkjc_Yd
        {
            set { _fkjc_yd = value; }
            get { return _fkjc_yd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fkjc_Yd_Other
        {
            set { _fkjc_yd_other = value; }
            get { return _fkjc_yd_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Fkjc_Gj
        {
            set { _fkjc_gj = value; }
            get { return _fkjc_gj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fkjc_Gj_Other
        {
            set { _fkjc_gj_other = value; }
            get { return _fkjc_gj_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Fkjc_Zg
        {
            set { _fkjc_zg = value; }
            get { return _fkjc_zg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fkjc_Zg_Other
        {
            set { _fkjc_zg_other = value; }
            get { return _fkjc_zg_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Fkjc_Fj
        {
            set { _fkjc_fj = value; }
            get { return _fkjc_fj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fkjc_Fj_Other
        {
            set { _fkjc_fj_other = value; }
            get { return _fkjc_fj_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Fzjc_Xcg_Xhdb
        {
            set { _fzjc_xcg_xhdb = value; }
            get { return _fzjc_xcg_xhdb; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Fzjc_Xcg_Bxb
        {
            set { _fzjc_xcg_bxb = value; }
            get { return _fzjc_xcg_bxb; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Fzjc_Xcg_Xxb
        {
            set { _fzjc_xcg_xxb = value; }
            get { return _fzjc_xcg_xxb; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fzjc_Xcg_Qt
        {
            set { _fzjc_xcg_qt = value; }
            get { return _fzjc_xcg_qt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Fzjc_Ncg_Ndb
        {
            set { _fzjc_ncg_ndb = value; }
            get { return _fzjc_ncg_ndb; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Fzjc_Ncg_Nt
        {
            set { _fzjc_ncg_nt = value; }
            get { return _fzjc_ncg_nt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Fzjc_Ncg_Ntt
        {
            set { _fzjc_ncg_ntt = value; }
            get { return _fzjc_ncg_ntt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Fzjc_Ncg_Nqx
        {
            set { _fzjc_ncg_nqx = value; }
            get { return _fzjc_ncg_nqx; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fzjc_Ncg_Qt
        {
            set { _fzjc_ncg_qt = value; }
            get { return _fzjc_ncg_qt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fzjc_Xx_ABO
        {
            set { _fzjc_xx_abo = value; }
            get { return _fzjc_xx_abo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fzjc_Xx_RH
        {
            set { _fzjc_xx_rh = value; }
            get { return _fzjc_xx_rh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Fzjc_Xt
        {
            set { _fzjc_xt = value; }
            get { return _fzjc_xt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Fzjc_Ggn_Xqgbzam
        {
            set { _fzjc_ggn_xqgbzam = value; }
            get { return _fzjc_ggn_xqgbzam; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Fzjc_Ggn_Xqgczam
        {
            set { _fzjc_ggn_xqgczam = value; }
            get { return _fzjc_ggn_xqgczam; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Fzjc_Ggn_Xdb
        {
            set { _fzjc_ggn_xdb = value; }
            get { return _fzjc_ggn_xdb; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Fzjc_Ggn_Zdhs
        {
            set { _fzjc_ggn_zdhs = value; }
            get { return _fzjc_ggn_zdhs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Fzjc_Ggn_Jhdhs
        {
            set { _fzjc_ggn_jhdhs = value; }
            get { return _fzjc_ggn_jhdhs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Fzjc_Sgn_Xqjq
        {
            set { _fzjc_sgn_xqjq = value; }
            get { return _fzjc_sgn_xqjq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Fzjc_Sgn_Xnsd
        {
            set { _fzjc_sgn_xnsd = value; }
            get { return _fzjc_sgn_xnsd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Fzjc_Sgn_Xjnd
        {
            set { _fzjc_sgn_xjnd = value; }
            get { return _fzjc_sgn_xjnd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Fzjc_Sgn_Xnnd
        {
            set { _fzjc_sgn_xnnd = value; }
            get { return _fzjc_sgn_xnnd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fzjc_YdFmw
        {
            set { _fzjc_ydfmw = value; }
            get { return _fzjc_ydfmw; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fzjc_YdFmw_Qt
        {
            set { _fzjc_ydfmw_qt = value; }
            get { return _fzjc_ydfmw_qt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Fzjc_YdQjd
        {
            set { _fzjc_ydqjd = value; }
            get { return _fzjc_ydqjd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Fzjc_Yg_Bmky
        {
            set { _fzjc_yg_bmky = value; }
            get { return _fzjc_yg_bmky; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Fzjc_Yg_Bmkt
        {
            set { _fzjc_yg_bmkt = value; }
            get { return _fzjc_yg_bmkt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Fzjc_Yg_Eky
        {
            set { _fzjc_yg_eky = value; }
            get { return _fzjc_yg_eky; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Fzjc_Yg_Ekt
        {
            set { _fzjc_yg_ekt = value; }
            get { return _fzjc_yg_ekt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Fzjc_Yg_Hxkt
        {
            set { _fzjc_yg_hxkt = value; }
            get { return _fzjc_yg_hxkt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Fzjc_Mdxqxsy
        {
            set { _fzjc_mdxqxsy = value; }
            get { return _fzjc_mdxqxsy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Fzjc_Mdxqxsy_sfgw
        {
            set { _fzjc_mdxqxsy_sfgw = value; }
            get { return _fzjc_mdxqxsy_sfgw; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fzjc_Mdxqxsy_gwsm
        {
            set { _fzjc_mdxqxsy_gwsm = value; }
            get { return _fzjc_mdxqxsy_gwsm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Fzjc_HIV_Jc
        {
            set { _fzjc_hiv_jc = value; }
            get { return _fzjc_hiv_jc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Fzjc_mdjc
        {
            set { _fzjc_mdjc = value; }
            get { return _fzjc_mdjc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Fzjc_Hiv
        {
            set { _fzjc_hiv = value; }
            get { return _fzjc_hiv; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fzjc_Bc
        {
            set { _fzjc_bc = value; }
            get { return _fzjc_bc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZtPg
        {
            set { _ztpg = value; }
            get { return _ztpg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BjZd
        {
            set { _bjzd = value; }
            get { return _bjzd; }
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
        public DateTime? CurVisitDate
        {
            set { _curvisitdate = value; }
            get { return _curvisitdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BjZdQT
        {
            set { _bjzdqt = value; }
            get { return _bjzdqt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZtPgQT
        {
            set { _ztpgqt = value; }
            get { return _ztpgqt; }
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
        public NHS_YCF_CQFS_1(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommID,UnvID,FnWeight,FnTzzs,FnXyMax,FnXyMin,TzXz,TzXz_Other,TzFb,TzFb_Other,Fkjc_Wy,Fkjc_Wy_Other,Fkjc_Yd,Fkjc_Yd_Other,Fkjc_Gj,Fkjc_Gj_Other,Fkjc_Zg,Fkjc_Zg_Other,Fkjc_Fj,Fkjc_Fj_Other,Fzjc_Xcg_Xhdb,Fzjc_Xcg_Bxb,Fzjc_Xcg_Xxb,Fzjc_Xcg_Qt,Fzjc_Ncg_Ndb,Fzjc_Ncg_Nt,Fzjc_Ncg_Ntt,Fzjc_Ncg_Nqx,Fzjc_Ncg_Qt,Fzjc_Xx_ABO,Fzjc_Xx_RH,Fzjc_Xt,Fzjc_Ggn_Xqgbzam,Fzjc_Ggn_Xqgczam,Fzjc_Ggn_Xdb,Fzjc_Ggn_Zdhs,Fzjc_Ggn_Jhdhs,Fzjc_Sgn_Xqjq,Fzjc_Sgn_Xnsd,Fzjc_Sgn_Xjnd,Fzjc_Sgn_Xnnd,Fzjc_YdFmw,Fzjc_YdFmw_Qt,Fzjc_YdQjd,Fzjc_Yg_Bmky,Fzjc_Yg_Bmkt,Fzjc_Yg_Eky,Fzjc_Yg_Ekt,Fzjc_Yg_Hxkt,Fzjc_Mdxqxsy,Fzjc_Mdxqxsy_sfgw,Fzjc_Mdxqxsy_gwsm,Fzjc_HIV_Jc,Fzjc_mdjc,Fzjc_Hiv,Fzjc_Bc,ZtPg,BjZd,Zz_Yw,Zz_Yy,Zz_Jg,NextVisitDate,Doctor,SFJG,CreateDate,UserID,CurVisitDate,BjZdQT,ZtPgQT,IsGWCF,ZDYJ ");
            strSql.Append(" FROM [NHS_YCF_CQFS_1] ");
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
                if (ds.Tables[0].Rows[0]["FnWeight"] != null && ds.Tables[0].Rows[0]["FnWeight"].ToString() != "")
                {
                    this.FnWeight = decimal.Parse(ds.Tables[0].Rows[0]["FnWeight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FnTzzs"] != null && ds.Tables[0].Rows[0]["FnTzzs"].ToString() != "")
                {
                    this.FnTzzs = decimal.Parse(ds.Tables[0].Rows[0]["FnTzzs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FnXyMax"] != null && ds.Tables[0].Rows[0]["FnXyMax"].ToString() != "")
                {
                    this.FnXyMax = decimal.Parse(ds.Tables[0].Rows[0]["FnXyMax"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FnXyMin"] != null && ds.Tables[0].Rows[0]["FnXyMin"].ToString() != "")
                {
                    this.FnXyMin = decimal.Parse(ds.Tables[0].Rows[0]["FnXyMin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TzXz"] != null && ds.Tables[0].Rows[0]["TzXz"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["TzXz"].ToString() == "1") || (ds.Tables[0].Rows[0]["TzXz"].ToString().ToLower() == "true"))
                    {
                        this.TzXz = true;
                    }
                    else
                    {
                        this.TzXz = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["TzXz_Other"] != null)
                {
                    this.TzXz_Other = ds.Tables[0].Rows[0]["TzXz_Other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TzFb"] != null && ds.Tables[0].Rows[0]["TzFb"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["TzFb"].ToString() == "1") || (ds.Tables[0].Rows[0]["TzFb"].ToString().ToLower() == "true"))
                    {
                        this.TzFb = true;
                    }
                    else
                    {
                        this.TzFb = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["TzFb_Other"] != null)
                {
                    this.TzFb_Other = ds.Tables[0].Rows[0]["TzFb_Other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fkjc_Wy"] != null && ds.Tables[0].Rows[0]["Fkjc_Wy"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fkjc_Wy"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fkjc_Wy"].ToString().ToLower() == "true"))
                    {
                        this.Fkjc_Wy = true;
                    }
                    else
                    {
                        this.Fkjc_Wy = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Fkjc_Wy_Other"] != null)
                {
                    this.Fkjc_Wy_Other = ds.Tables[0].Rows[0]["Fkjc_Wy_Other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fkjc_Yd"] != null && ds.Tables[0].Rows[0]["Fkjc_Yd"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fkjc_Yd"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fkjc_Yd"].ToString().ToLower() == "true"))
                    {
                        this.Fkjc_Yd = true;
                    }
                    else
                    {
                        this.Fkjc_Yd = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Fkjc_Yd_Other"] != null)
                {
                    this.Fkjc_Yd_Other = ds.Tables[0].Rows[0]["Fkjc_Yd_Other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fkjc_Gj"] != null && ds.Tables[0].Rows[0]["Fkjc_Gj"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fkjc_Gj"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fkjc_Gj"].ToString().ToLower() == "true"))
                    {
                        this.Fkjc_Gj = true;
                    }
                    else
                    {
                        this.Fkjc_Gj = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Fkjc_Gj_Other"] != null)
                {
                    this.Fkjc_Gj_Other = ds.Tables[0].Rows[0]["Fkjc_Gj_Other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fkjc_Zg"] != null && ds.Tables[0].Rows[0]["Fkjc_Zg"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fkjc_Zg"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fkjc_Zg"].ToString().ToLower() == "true"))
                    {
                        this.Fkjc_Zg = true;
                    }
                    else
                    {
                        this.Fkjc_Zg = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Fkjc_Zg_Other"] != null)
                {
                    this.Fkjc_Zg_Other = ds.Tables[0].Rows[0]["Fkjc_Zg_Other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fkjc_Fj"] != null && ds.Tables[0].Rows[0]["Fkjc_Fj"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fkjc_Fj"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fkjc_Fj"].ToString().ToLower() == "true"))
                    {
                        this.Fkjc_Fj = true;
                    }
                    else
                    {
                        this.Fkjc_Fj = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Fkjc_Fj_Other"] != null)
                {
                    this.Fkjc_Fj_Other = ds.Tables[0].Rows[0]["Fkjc_Fj_Other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Xcg_Xhdb"] != null && ds.Tables[0].Rows[0]["Fzjc_Xcg_Xhdb"].ToString() != "")
                {
                    this.Fzjc_Xcg_Xhdb = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Xcg_Xhdb"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Xcg_Bxb"] != null && ds.Tables[0].Rows[0]["Fzjc_Xcg_Bxb"].ToString() != "")
                {
                    this.Fzjc_Xcg_Bxb = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Xcg_Bxb"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Xcg_Xxb"] != null && ds.Tables[0].Rows[0]["Fzjc_Xcg_Xxb"].ToString() != "")
                {
                    this.Fzjc_Xcg_Xxb = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Xcg_Xxb"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Xcg_Qt"] != null)
                {
                    this.Fzjc_Xcg_Qt = ds.Tables[0].Rows[0]["Fzjc_Xcg_Qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ncg_Ndb"] != null && ds.Tables[0].Rows[0]["Fzjc_Ncg_Ndb"].ToString() != "")
                {
                    this.Fzjc_Ncg_Ndb = int.Parse(ds.Tables[0].Rows[0]["Fzjc_Ncg_Ndb"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ncg_Nt"] != null && ds.Tables[0].Rows[0]["Fzjc_Ncg_Nt"].ToString() != "")
                {
                    this.Fzjc_Ncg_Nt = int.Parse(ds.Tables[0].Rows[0]["Fzjc_Ncg_Nt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ncg_Ntt"] != null && ds.Tables[0].Rows[0]["Fzjc_Ncg_Ntt"].ToString() != "")
                {
                    this.Fzjc_Ncg_Ntt = int.Parse(ds.Tables[0].Rows[0]["Fzjc_Ncg_Ntt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ncg_Nqx"] != null && ds.Tables[0].Rows[0]["Fzjc_Ncg_Nqx"].ToString() != "")
                {
                    this.Fzjc_Ncg_Nqx = int.Parse(ds.Tables[0].Rows[0]["Fzjc_Ncg_Nqx"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ncg_Qt"] != null)
                {
                    this.Fzjc_Ncg_Qt = ds.Tables[0].Rows[0]["Fzjc_Ncg_Qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Xx_ABO"] != null)
                {
                    this.Fzjc_Xx_ABO = ds.Tables[0].Rows[0]["Fzjc_Xx_ABO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Xx_RH"] != null)
                {
                    this.Fzjc_Xx_RH = ds.Tables[0].Rows[0]["Fzjc_Xx_RH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Xt"] != null && ds.Tables[0].Rows[0]["Fzjc_Xt"].ToString() != "")
                {
                    this.Fzjc_Xt = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Xt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ggn_Xqgbzam"] != null && ds.Tables[0].Rows[0]["Fzjc_Ggn_Xqgbzam"].ToString() != "")
                {
                    this.Fzjc_Ggn_Xqgbzam = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Ggn_Xqgbzam"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ggn_Xqgczam"] != null && ds.Tables[0].Rows[0]["Fzjc_Ggn_Xqgczam"].ToString() != "")
                {
                    this.Fzjc_Ggn_Xqgczam = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Ggn_Xqgczam"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ggn_Xdb"] != null && ds.Tables[0].Rows[0]["Fzjc_Ggn_Xdb"].ToString() != "")
                {
                    this.Fzjc_Ggn_Xdb = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Ggn_Xdb"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ggn_Zdhs"] != null && ds.Tables[0].Rows[0]["Fzjc_Ggn_Zdhs"].ToString() != "")
                {
                    this.Fzjc_Ggn_Zdhs = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Ggn_Zdhs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ggn_Jhdhs"] != null && ds.Tables[0].Rows[0]["Fzjc_Ggn_Jhdhs"].ToString() != "")
                {
                    this.Fzjc_Ggn_Jhdhs = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Ggn_Jhdhs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Sgn_Xqjq"] != null && ds.Tables[0].Rows[0]["Fzjc_Sgn_Xqjq"].ToString() != "")
                {
                    this.Fzjc_Sgn_Xqjq = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Sgn_Xqjq"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Sgn_Xnsd"] != null && ds.Tables[0].Rows[0]["Fzjc_Sgn_Xnsd"].ToString() != "")
                {
                    this.Fzjc_Sgn_Xnsd = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Sgn_Xnsd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Sgn_Xjnd"] != null && ds.Tables[0].Rows[0]["Fzjc_Sgn_Xjnd"].ToString() != "")
                {
                    this.Fzjc_Sgn_Xjnd = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Sgn_Xjnd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Sgn_Xnnd"] != null && ds.Tables[0].Rows[0]["Fzjc_Sgn_Xnnd"].ToString() != "")
                {
                    this.Fzjc_Sgn_Xnnd = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Sgn_Xnnd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_YdFmw"] != null)
                {
                    this.Fzjc_YdFmw = ds.Tables[0].Rows[0]["Fzjc_YdFmw"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_YdFmw_Qt"] != null)
                {
                    this.Fzjc_YdFmw_Qt = ds.Tables[0].Rows[0]["Fzjc_YdFmw_Qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_YdQjd"] != null && ds.Tables[0].Rows[0]["Fzjc_YdQjd"].ToString() != "")
                {
                    this.Fzjc_YdQjd = int.Parse(ds.Tables[0].Rows[0]["Fzjc_YdQjd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Yg_Bmky"] != null && ds.Tables[0].Rows[0]["Fzjc_Yg_Bmky"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fzjc_Yg_Bmky"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fzjc_Yg_Bmky"].ToString().ToLower() == "true"))
                    {
                        this.Fzjc_Yg_Bmky = true;
                    }
                    else
                    {
                        this.Fzjc_Yg_Bmky = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Fzjc_Yg_Bmkt"] != null && ds.Tables[0].Rows[0]["Fzjc_Yg_Bmkt"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fzjc_Yg_Bmkt"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fzjc_Yg_Bmkt"].ToString().ToLower() == "true"))
                    {
                        this.Fzjc_Yg_Bmkt = true;
                    }
                    else
                    {
                        this.Fzjc_Yg_Bmkt = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Fzjc_Yg_Eky"] != null && ds.Tables[0].Rows[0]["Fzjc_Yg_Eky"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fzjc_Yg_Eky"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fzjc_Yg_Eky"].ToString().ToLower() == "true"))
                    {
                        this.Fzjc_Yg_Eky = true;
                    }
                    else
                    {
                        this.Fzjc_Yg_Eky = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Fzjc_Yg_Ekt"] != null && ds.Tables[0].Rows[0]["Fzjc_Yg_Ekt"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fzjc_Yg_Ekt"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fzjc_Yg_Ekt"].ToString().ToLower() == "true"))
                    {
                        this.Fzjc_Yg_Ekt = true;
                    }
                    else
                    {
                        this.Fzjc_Yg_Ekt = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Fzjc_Yg_Hxkt"] != null && ds.Tables[0].Rows[0]["Fzjc_Yg_Hxkt"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fzjc_Yg_Hxkt"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fzjc_Yg_Hxkt"].ToString().ToLower() == "true"))
                    {
                        this.Fzjc_Yg_Hxkt = true;
                    }
                    else
                    {
                        this.Fzjc_Yg_Hxkt = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy"] != null && ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy"].ToString() != "")
                {
                    this.Fzjc_Mdxqxsy = int.Parse(ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy_sfgw"] != null && ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy_sfgw"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy_sfgw"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy_sfgw"].ToString().ToLower() == "true"))
                    {
                        this.Fzjc_Mdxqxsy_sfgw = true;
                    }
                    else
                    {
                        this.Fzjc_Mdxqxsy_sfgw = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy_gwsm"] != null)
                {
                    this.Fzjc_Mdxqxsy_gwsm = ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy_gwsm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_HIV_Jc"] != null && ds.Tables[0].Rows[0]["Fzjc_HIV_Jc"].ToString() != "")
                {
                    this.Fzjc_HIV_Jc = int.Parse(ds.Tables[0].Rows[0]["Fzjc_HIV_Jc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_mdjc"] != null && ds.Tables[0].Rows[0]["Fzjc_mdjc"].ToString() != "")
                {
                    this.Fzjc_mdjc = int.Parse(ds.Tables[0].Rows[0]["Fzjc_mdjc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Hiv"] != null && ds.Tables[0].Rows[0]["Fzjc_Hiv"].ToString() != "")
                {
                    this.Fzjc_Hiv = int.Parse(ds.Tables[0].Rows[0]["Fzjc_Hiv"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Bc"] != null)
                {
                    this.Fzjc_Bc = ds.Tables[0].Rows[0]["Fzjc_Bc"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZtPg"] != null)
                {
                    this.ZtPg = ds.Tables[0].Rows[0]["ZtPg"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BjZd"] != null)
                {
                    this.BjZd = ds.Tables[0].Rows[0]["BjZd"].ToString();
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
                if (ds.Tables[0].Rows[0]["CurVisitDate"] != null && ds.Tables[0].Rows[0]["CurVisitDate"].ToString() != "")
                {
                    this.CurVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["CurVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BjZdQT"] != null)
                {
                    this.BjZdQT = ds.Tables[0].Rows[0]["BjZdQT"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZtPgQT"] != null)
                {
                    this.ZtPgQT = ds.Tables[0].Rows[0]["ZtPgQT"].ToString();
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
        public bool Exists(string UnvID, bool isEdit, int commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_YCF_CQFS_1]");
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
            strSql.Append("insert into [NHS_YCF_CQFS_1] (");
            strSql.Append("UnvID,FnWeight,FnTzzs,FnXyMax,FnXyMin,TzXz,TzXz_Other,TzFb,TzFb_Other,Fkjc_Wy,Fkjc_Wy_Other,Fkjc_Yd,Fkjc_Yd_Other,Fkjc_Gj,Fkjc_Gj_Other,Fkjc_Zg,Fkjc_Zg_Other,Fkjc_Fj,Fkjc_Fj_Other,Fzjc_Xcg_Xhdb,Fzjc_Xcg_Bxb,Fzjc_Xcg_Xxb,Fzjc_Xcg_Qt,Fzjc_Ncg_Ndb,Fzjc_Ncg_Nt,Fzjc_Ncg_Ntt,Fzjc_Ncg_Nqx,Fzjc_Ncg_Qt,Fzjc_Xx_ABO,Fzjc_Xx_RH,Fzjc_Xt,Fzjc_Ggn_Xqgbzam,Fzjc_Ggn_Xqgczam,Fzjc_Ggn_Xdb,Fzjc_Ggn_Zdhs,Fzjc_Ggn_Jhdhs,Fzjc_Sgn_Xqjq,Fzjc_Sgn_Xnsd,Fzjc_Sgn_Xjnd,Fzjc_Sgn_Xnnd,Fzjc_YdFmw,Fzjc_YdFmw_Qt,Fzjc_YdQjd,Fzjc_Yg_Bmky,Fzjc_Yg_Bmkt,Fzjc_Yg_Eky,Fzjc_Yg_Ekt,Fzjc_Yg_Hxkt,Fzjc_Mdxqxsy,Fzjc_Mdxqxsy_sfgw,Fzjc_Mdxqxsy_gwsm,Fzjc_HIV_Jc,Fzjc_mdjc,Fzjc_Hiv,Fzjc_Bc,ZtPg,BjZd,Zz_Yw,Zz_Yy,Zz_Jg,NextVisitDate,Doctor,SFJG,CreateDate,UserID,CurVisitDate,BjZdQT,ZtPgQT,IsGWCF,ZDYJ)");
            strSql.Append(" values (");
            strSql.Append("@UnvID,@FnWeight,@FnTzzs,@FnXyMax,@FnXyMin,@TzXz,@TzXz_Other,@TzFb,@TzFb_Other,@Fkjc_Wy,@Fkjc_Wy_Other,@Fkjc_Yd,@Fkjc_Yd_Other,@Fkjc_Gj,@Fkjc_Gj_Other,@Fkjc_Zg,@Fkjc_Zg_Other,@Fkjc_Fj,@Fkjc_Fj_Other,@Fzjc_Xcg_Xhdb,@Fzjc_Xcg_Bxb,@Fzjc_Xcg_Xxb,@Fzjc_Xcg_Qt,@Fzjc_Ncg_Ndb,@Fzjc_Ncg_Nt,@Fzjc_Ncg_Ntt,@Fzjc_Ncg_Nqx,@Fzjc_Ncg_Qt,@Fzjc_Xx_ABO,@Fzjc_Xx_RH,@Fzjc_Xt,@Fzjc_Ggn_Xqgbzam,@Fzjc_Ggn_Xqgczam,@Fzjc_Ggn_Xdb,@Fzjc_Ggn_Zdhs,@Fzjc_Ggn_Jhdhs,@Fzjc_Sgn_Xqjq,@Fzjc_Sgn_Xnsd,@Fzjc_Sgn_Xjnd,@Fzjc_Sgn_Xnnd,@Fzjc_YdFmw,@Fzjc_YdFmw_Qt,@Fzjc_YdQjd,@Fzjc_Yg_Bmky,@Fzjc_Yg_Bmkt,@Fzjc_Yg_Eky,@Fzjc_Yg_Ekt,@Fzjc_Yg_Hxkt,@Fzjc_Mdxqxsy,@Fzjc_Mdxqxsy_sfgw,@Fzjc_Mdxqxsy_gwsm,@Fzjc_HIV_Jc,@Fzjc_mdjc,@Fzjc_Hiv,@Fzjc_Bc,@ZtPg,@BjZd,@Zz_Yw,@Zz_Yy,@Zz_Jg,@NextVisitDate,@Doctor,@SFJG,@CreateDate,@UserID,@CurVisitDate,@BjZdQT,@ZtPgQT,@IsGWCF,@ZDYJ)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@FnWeight", SqlDbType.Float,8),
					new SqlParameter("@FnTzzs", SqlDbType.Float,8),
					new SqlParameter("@FnXyMax", SqlDbType.Float,8),
					new SqlParameter("@FnXyMin", SqlDbType.Float,8),
					new SqlParameter("@TzXz", SqlDbType.Bit,1),
					new SqlParameter("@TzXz_Other", SqlDbType.VarChar,50),
					new SqlParameter("@TzFb", SqlDbType.Bit,1),
					new SqlParameter("@TzFb_Other", SqlDbType.VarChar,50),
					new SqlParameter("@Fkjc_Wy", SqlDbType.Bit,1),
					new SqlParameter("@Fkjc_Wy_Other", SqlDbType.VarChar,50),
					new SqlParameter("@Fkjc_Yd", SqlDbType.Bit,1),
					new SqlParameter("@Fkjc_Yd_Other", SqlDbType.VarChar,50),
					new SqlParameter("@Fkjc_Gj", SqlDbType.Bit,1),
					new SqlParameter("@Fkjc_Gj_Other", SqlDbType.VarChar,50),
					new SqlParameter("@Fkjc_Zg", SqlDbType.Bit,1),
					new SqlParameter("@Fkjc_Zg_Other", SqlDbType.VarChar,50),
					new SqlParameter("@Fkjc_Fj", SqlDbType.Bit,1),
					new SqlParameter("@Fkjc_Fj_Other", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_Xcg_Xhdb", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Xcg_Bxb", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Xcg_Xxb", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Xcg_Qt", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_Ncg_Ndb", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Ncg_Nt", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Ncg_Ntt", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Ncg_Nqx", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Ncg_Qt", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_Xx_ABO", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_Xx_RH", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_Xt", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Ggn_Xqgbzam", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Ggn_Xqgczam", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Ggn_Xdb", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Ggn_Zdhs", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Ggn_Jhdhs", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Sgn_Xqjq", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Sgn_Xnsd", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Sgn_Xjnd", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Sgn_Xnnd", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_YdFmw", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_YdFmw_Qt", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_YdQjd", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Yg_Bmky", SqlDbType.Bit,1),
					new SqlParameter("@Fzjc_Yg_Bmkt", SqlDbType.Bit,1),
					new SqlParameter("@Fzjc_Yg_Eky", SqlDbType.Bit,1),
					new SqlParameter("@Fzjc_Yg_Ekt", SqlDbType.Bit,1),
					new SqlParameter("@Fzjc_Yg_Hxkt", SqlDbType.Bit,1),
					new SqlParameter("@Fzjc_Mdxqxsy", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Mdxqxsy_sfgw", SqlDbType.Bit,1),
					new SqlParameter("@Fzjc_Mdxqxsy_gwsm", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_HIV_Jc", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_mdjc", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Hiv", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Bc", SqlDbType.VarChar,50),
					new SqlParameter("@ZtPg", SqlDbType.NVarChar,200),
					new SqlParameter("@BjZd", SqlDbType.VarChar,50),
					new SqlParameter("@Zz_Yw", SqlDbType.Bit,1),
					new SqlParameter("@Zz_Yy", SqlDbType.NVarChar,50),
					new SqlParameter("@Zz_Jg", SqlDbType.NVarChar,50),
					new SqlParameter("@NextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Doctor", SqlDbType.VarChar,50),
					new SqlParameter("@SFJG", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@CurVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@BjZdQT", SqlDbType.VarChar,50),
					new SqlParameter("@ZtPgQT", SqlDbType.VarChar,50),
					new SqlParameter("@IsGWCF", SqlDbType.Bit,1),
					new SqlParameter("@ZDYJ", SqlDbType.NVarChar,500)};
            parameters[0].Value = UnvID;
            parameters[1].Value = FnWeight;
            parameters[2].Value = FnTzzs;
            parameters[3].Value = FnXyMax;
            parameters[4].Value = FnXyMin;
            parameters[5].Value = TzXz;
            parameters[6].Value = TzXz_Other;
            parameters[7].Value = TzFb;
            parameters[8].Value = TzFb_Other;
            parameters[9].Value = Fkjc_Wy;
            parameters[10].Value = Fkjc_Wy_Other;
            parameters[11].Value = Fkjc_Yd;
            parameters[12].Value = Fkjc_Yd_Other;
            parameters[13].Value = Fkjc_Gj;
            parameters[14].Value = Fkjc_Gj_Other;
            parameters[15].Value = Fkjc_Zg;
            parameters[16].Value = Fkjc_Zg_Other;
            parameters[17].Value = Fkjc_Fj;
            parameters[18].Value = Fkjc_Fj_Other;
            parameters[19].Value = Fzjc_Xcg_Xhdb;
            parameters[20].Value = Fzjc_Xcg_Bxb;
            parameters[21].Value = Fzjc_Xcg_Xxb;
            parameters[22].Value = Fzjc_Xcg_Qt;
            parameters[23].Value = Fzjc_Ncg_Ndb;
            parameters[24].Value = Fzjc_Ncg_Nt;
            parameters[25].Value = Fzjc_Ncg_Ntt;
            parameters[26].Value = Fzjc_Ncg_Nqx;
            parameters[27].Value = Fzjc_Ncg_Qt;
            parameters[28].Value = Fzjc_Xx_ABO;
            parameters[29].Value = Fzjc_Xx_RH;
            parameters[30].Value = Fzjc_Xt;
            parameters[31].Value = Fzjc_Ggn_Xqgbzam;
            parameters[32].Value = Fzjc_Ggn_Xqgczam;
            parameters[33].Value = Fzjc_Ggn_Xdb;
            parameters[34].Value = Fzjc_Ggn_Zdhs;
            parameters[35].Value = Fzjc_Ggn_Jhdhs;
            parameters[36].Value = Fzjc_Sgn_Xqjq;
            parameters[37].Value = Fzjc_Sgn_Xnsd;
            parameters[38].Value = Fzjc_Sgn_Xjnd;
            parameters[39].Value = Fzjc_Sgn_Xnnd;
            parameters[40].Value = Fzjc_YdFmw;
            parameters[41].Value = Fzjc_YdFmw_Qt;
            parameters[42].Value = Fzjc_YdQjd;
            parameters[43].Value = Fzjc_Yg_Bmky;
            parameters[44].Value = Fzjc_Yg_Bmkt;
            parameters[45].Value = Fzjc_Yg_Eky;
            parameters[46].Value = Fzjc_Yg_Ekt;
            parameters[47].Value = Fzjc_Yg_Hxkt;
            parameters[48].Value = Fzjc_Mdxqxsy;
            parameters[49].Value = Fzjc_Mdxqxsy_sfgw;
            parameters[50].Value = Fzjc_Mdxqxsy_gwsm;
            parameters[51].Value = Fzjc_HIV_Jc;
            parameters[52].Value = Fzjc_mdjc;
            parameters[53].Value = Fzjc_Hiv;
            parameters[54].Value = Fzjc_Bc;
            parameters[55].Value = ZtPg;
            parameters[56].Value = BjZd;
            parameters[57].Value = Zz_Yw;
            parameters[58].Value = Zz_Yy;
            parameters[59].Value = Zz_Jg;
            parameters[60].Value = NextVisitDate;
            parameters[61].Value = Doctor;
            parameters[62].Value = SFJG;
            parameters[63].Value = CreateDate;
            parameters[64].Value = UserID;
            parameters[65].Value = CurVisitDate;
            parameters[66].Value = BjZdQT;
            parameters[67].Value = ZtPgQT;
            parameters[68].Value = IsGWCF;
            parameters[69].Value = ZDYJ;

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
            strSql.Append("update [NHS_YCF_CQFS_1] set ");
            strSql.Append("UnvID=@UnvID,");
            strSql.Append("FnWeight=@FnWeight,");
            strSql.Append("FnTzzs=@FnTzzs,");
            strSql.Append("FnXyMax=@FnXyMax,");
            strSql.Append("FnXyMin=@FnXyMin,");
            strSql.Append("TzXz=@TzXz,");
            strSql.Append("TzXz_Other=@TzXz_Other,");
            strSql.Append("TzFb=@TzFb,");
            strSql.Append("TzFb_Other=@TzFb_Other,");
            strSql.Append("Fkjc_Wy=@Fkjc_Wy,");
            strSql.Append("Fkjc_Wy_Other=@Fkjc_Wy_Other,");
            strSql.Append("Fkjc_Yd=@Fkjc_Yd,");
            strSql.Append("Fkjc_Yd_Other=@Fkjc_Yd_Other,");
            strSql.Append("Fkjc_Gj=@Fkjc_Gj,");
            strSql.Append("Fkjc_Gj_Other=@Fkjc_Gj_Other,");
            strSql.Append("Fkjc_Zg=@Fkjc_Zg,");
            strSql.Append("Fkjc_Zg_Other=@Fkjc_Zg_Other,");
            strSql.Append("Fkjc_Fj=@Fkjc_Fj,");
            strSql.Append("Fkjc_Fj_Other=@Fkjc_Fj_Other,");
            strSql.Append("Fzjc_Xcg_Xhdb=@Fzjc_Xcg_Xhdb,");
            strSql.Append("Fzjc_Xcg_Bxb=@Fzjc_Xcg_Bxb,");
            strSql.Append("Fzjc_Xcg_Xxb=@Fzjc_Xcg_Xxb,");
            strSql.Append("Fzjc_Xcg_Qt=@Fzjc_Xcg_Qt,");
            strSql.Append("Fzjc_Ncg_Ndb=@Fzjc_Ncg_Ndb,");
            strSql.Append("Fzjc_Ncg_Nt=@Fzjc_Ncg_Nt,");
            strSql.Append("Fzjc_Ncg_Ntt=@Fzjc_Ncg_Ntt,");
            strSql.Append("Fzjc_Ncg_Nqx=@Fzjc_Ncg_Nqx,");
            strSql.Append("Fzjc_Ncg_Qt=@Fzjc_Ncg_Qt,");
            strSql.Append("Fzjc_Xx_ABO=@Fzjc_Xx_ABO,");
            strSql.Append("Fzjc_Xx_RH=@Fzjc_Xx_RH,");
            strSql.Append("Fzjc_Xt=@Fzjc_Xt,");
            strSql.Append("Fzjc_Ggn_Xqgbzam=@Fzjc_Ggn_Xqgbzam,");
            strSql.Append("Fzjc_Ggn_Xqgczam=@Fzjc_Ggn_Xqgczam,");
            strSql.Append("Fzjc_Ggn_Xdb=@Fzjc_Ggn_Xdb,");
            strSql.Append("Fzjc_Ggn_Zdhs=@Fzjc_Ggn_Zdhs,");
            strSql.Append("Fzjc_Ggn_Jhdhs=@Fzjc_Ggn_Jhdhs,");
            strSql.Append("Fzjc_Sgn_Xqjq=@Fzjc_Sgn_Xqjq,");
            strSql.Append("Fzjc_Sgn_Xnsd=@Fzjc_Sgn_Xnsd,");
            strSql.Append("Fzjc_Sgn_Xjnd=@Fzjc_Sgn_Xjnd,");
            strSql.Append("Fzjc_Sgn_Xnnd=@Fzjc_Sgn_Xnnd,");
            strSql.Append("Fzjc_YdFmw=@Fzjc_YdFmw,");
            strSql.Append("Fzjc_YdFmw_Qt=@Fzjc_YdFmw_Qt,");
            strSql.Append("Fzjc_YdQjd=@Fzjc_YdQjd,");
            strSql.Append("Fzjc_Yg_Bmky=@Fzjc_Yg_Bmky,");
            strSql.Append("Fzjc_Yg_Bmkt=@Fzjc_Yg_Bmkt,");
            strSql.Append("Fzjc_Yg_Eky=@Fzjc_Yg_Eky,");
            strSql.Append("Fzjc_Yg_Ekt=@Fzjc_Yg_Ekt,");
            strSql.Append("Fzjc_Yg_Hxkt=@Fzjc_Yg_Hxkt,");
            strSql.Append("Fzjc_Mdxqxsy=@Fzjc_Mdxqxsy,");
            strSql.Append("Fzjc_Mdxqxsy_sfgw=@Fzjc_Mdxqxsy_sfgw,");
            strSql.Append("Fzjc_Mdxqxsy_gwsm=@Fzjc_Mdxqxsy_gwsm,");
            strSql.Append("Fzjc_HIV_Jc=@Fzjc_HIV_Jc,");
            strSql.Append("Fzjc_mdjc=@Fzjc_mdjc,");
            strSql.Append("Fzjc_Hiv=@Fzjc_Hiv,");
            strSql.Append("Fzjc_Bc=@Fzjc_Bc,");
            strSql.Append("ZtPg=@ZtPg,");
            strSql.Append("BjZd=@BjZd,");
            strSql.Append("Zz_Yw=@Zz_Yw,");
            strSql.Append("Zz_Yy=@Zz_Yy,");
            strSql.Append("Zz_Jg=@Zz_Jg,");
            strSql.Append("NextVisitDate=@NextVisitDate,");
            strSql.Append("Doctor=@Doctor,");
            strSql.Append("SFJG=@SFJG,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("CurVisitDate=@CurVisitDate,");
            strSql.Append("BjZdQT=@BjZdQT,");
            strSql.Append("ZtPgQT=@ZtPgQT,");
            strSql.Append("IsGWCF=@IsGWCF,");
            strSql.Append("ZDYJ=@ZDYJ");
            strSql.Append(" where CommID=@CommID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@FnWeight", SqlDbType.Float,8),
					new SqlParameter("@FnTzzs", SqlDbType.Float,8),
					new SqlParameter("@FnXyMax", SqlDbType.Float,8),
					new SqlParameter("@FnXyMin", SqlDbType.Float,8),
					new SqlParameter("@TzXz", SqlDbType.Bit,1),
					new SqlParameter("@TzXz_Other", SqlDbType.VarChar,50),
					new SqlParameter("@TzFb", SqlDbType.Bit,1),
					new SqlParameter("@TzFb_Other", SqlDbType.VarChar,50),
					new SqlParameter("@Fkjc_Wy", SqlDbType.Bit,1),
					new SqlParameter("@Fkjc_Wy_Other", SqlDbType.VarChar,50),
					new SqlParameter("@Fkjc_Yd", SqlDbType.Bit,1),
					new SqlParameter("@Fkjc_Yd_Other", SqlDbType.VarChar,50),
					new SqlParameter("@Fkjc_Gj", SqlDbType.Bit,1),
					new SqlParameter("@Fkjc_Gj_Other", SqlDbType.VarChar,50),
					new SqlParameter("@Fkjc_Zg", SqlDbType.Bit,1),
					new SqlParameter("@Fkjc_Zg_Other", SqlDbType.VarChar,50),
					new SqlParameter("@Fkjc_Fj", SqlDbType.Bit,1),
					new SqlParameter("@Fkjc_Fj_Other", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_Xcg_Xhdb", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Xcg_Bxb", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Xcg_Xxb", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Xcg_Qt", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_Ncg_Ndb", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Ncg_Nt", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Ncg_Ntt", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Ncg_Nqx", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Ncg_Qt", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_Xx_ABO", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_Xx_RH", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_Xt", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Ggn_Xqgbzam", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Ggn_Xqgczam", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Ggn_Xdb", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Ggn_Zdhs", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Ggn_Jhdhs", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Sgn_Xqjq", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Sgn_Xnsd", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Sgn_Xjnd", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_Sgn_Xnnd", SqlDbType.Float,8),
					new SqlParameter("@Fzjc_YdFmw", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_YdFmw_Qt", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_YdQjd", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Yg_Bmky", SqlDbType.Bit,1),
					new SqlParameter("@Fzjc_Yg_Bmkt", SqlDbType.Bit,1),
					new SqlParameter("@Fzjc_Yg_Eky", SqlDbType.Bit,1),
					new SqlParameter("@Fzjc_Yg_Ekt", SqlDbType.Bit,1),
					new SqlParameter("@Fzjc_Yg_Hxkt", SqlDbType.Bit,1),
					new SqlParameter("@Fzjc_Mdxqxsy", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Mdxqxsy_sfgw", SqlDbType.Bit,1),
					new SqlParameter("@Fzjc_Mdxqxsy_gwsm", SqlDbType.VarChar,50),
					new SqlParameter("@Fzjc_HIV_Jc", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_mdjc", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Hiv", SqlDbType.Int,4),
					new SqlParameter("@Fzjc_Bc", SqlDbType.VarChar,50),
					new SqlParameter("@ZtPg", SqlDbType.NVarChar,200),
					new SqlParameter("@BjZd", SqlDbType.VarChar,50),
					new SqlParameter("@Zz_Yw", SqlDbType.Bit,1),
					new SqlParameter("@Zz_Yy", SqlDbType.NVarChar,50),
					new SqlParameter("@Zz_Jg", SqlDbType.NVarChar,50),
					new SqlParameter("@NextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Doctor", SqlDbType.VarChar,50),
					new SqlParameter("@SFJG", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@CurVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@BjZdQT", SqlDbType.VarChar,50),
					new SqlParameter("@ZtPgQT", SqlDbType.VarChar,50),
					new SqlParameter("@IsGWCF", SqlDbType.Bit,1),
					new SqlParameter("@ZDYJ", SqlDbType.NVarChar,500),
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = UnvID;
            parameters[1].Value = FnWeight;
            parameters[2].Value = FnTzzs;
            parameters[3].Value = FnXyMax;
            parameters[4].Value = FnXyMin;
            parameters[5].Value = TzXz;
            parameters[6].Value = TzXz_Other;
            parameters[7].Value = TzFb;
            parameters[8].Value = TzFb_Other;
            parameters[9].Value = Fkjc_Wy;
            parameters[10].Value = Fkjc_Wy_Other;
            parameters[11].Value = Fkjc_Yd;
            parameters[12].Value = Fkjc_Yd_Other;
            parameters[13].Value = Fkjc_Gj;
            parameters[14].Value = Fkjc_Gj_Other;
            parameters[15].Value = Fkjc_Zg;
            parameters[16].Value = Fkjc_Zg_Other;
            parameters[17].Value = Fkjc_Fj;
            parameters[18].Value = Fkjc_Fj_Other;
            parameters[19].Value = Fzjc_Xcg_Xhdb;
            parameters[20].Value = Fzjc_Xcg_Bxb;
            parameters[21].Value = Fzjc_Xcg_Xxb;
            parameters[22].Value = Fzjc_Xcg_Qt;
            parameters[23].Value = Fzjc_Ncg_Ndb;
            parameters[24].Value = Fzjc_Ncg_Nt;
            parameters[25].Value = Fzjc_Ncg_Ntt;
            parameters[26].Value = Fzjc_Ncg_Nqx;
            parameters[27].Value = Fzjc_Ncg_Qt;
            parameters[28].Value = Fzjc_Xx_ABO;
            parameters[29].Value = Fzjc_Xx_RH;
            parameters[30].Value = Fzjc_Xt;
            parameters[31].Value = Fzjc_Ggn_Xqgbzam;
            parameters[32].Value = Fzjc_Ggn_Xqgczam;
            parameters[33].Value = Fzjc_Ggn_Xdb;
            parameters[34].Value = Fzjc_Ggn_Zdhs;
            parameters[35].Value = Fzjc_Ggn_Jhdhs;
            parameters[36].Value = Fzjc_Sgn_Xqjq;
            parameters[37].Value = Fzjc_Sgn_Xnsd;
            parameters[38].Value = Fzjc_Sgn_Xjnd;
            parameters[39].Value = Fzjc_Sgn_Xnnd;
            parameters[40].Value = Fzjc_YdFmw;
            parameters[41].Value = Fzjc_YdFmw_Qt;
            parameters[42].Value = Fzjc_YdQjd;
            parameters[43].Value = Fzjc_Yg_Bmky;
            parameters[44].Value = Fzjc_Yg_Bmkt;
            parameters[45].Value = Fzjc_Yg_Eky;
            parameters[46].Value = Fzjc_Yg_Ekt;
            parameters[47].Value = Fzjc_Yg_Hxkt;
            parameters[48].Value = Fzjc_Mdxqxsy;
            parameters[49].Value = Fzjc_Mdxqxsy_sfgw;
            parameters[50].Value = Fzjc_Mdxqxsy_gwsm;
            parameters[51].Value = Fzjc_HIV_Jc;
            parameters[52].Value = Fzjc_mdjc;
            parameters[53].Value = Fzjc_Hiv;
            parameters[54].Value = Fzjc_Bc;
            parameters[55].Value = ZtPg;
            parameters[56].Value = BjZd;
            parameters[57].Value = Zz_Yw;
            parameters[58].Value = Zz_Yy;
            parameters[59].Value = Zz_Jg;
            parameters[60].Value = NextVisitDate;
            parameters[61].Value = Doctor;
            parameters[62].Value = SFJG;
            parameters[63].Value = CreateDate;
            parameters[64].Value = UserID;
            parameters[65].Value = CurVisitDate;
            parameters[66].Value = BjZdQT;
            parameters[67].Value = ZtPgQT;
            parameters[68].Value = IsGWCF;
            parameters[69].Value = ZDYJ;
            parameters[70].Value = CommID;

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
            strSql.Append("delete from [NHS_YCF_CQFS_1] ");
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
            strSql.Append("select CommID,UnvID,FnWeight,FnTzzs,FnXyMax,FnXyMin,TzXz,TzXz_Other,TzFb,TzFb_Other,Fkjc_Wy,Fkjc_Wy_Other,Fkjc_Yd,Fkjc_Yd_Other,Fkjc_Gj,Fkjc_Gj_Other,Fkjc_Zg,Fkjc_Zg_Other,Fkjc_Fj,Fkjc_Fj_Other,Fzjc_Xcg_Xhdb,Fzjc_Xcg_Bxb,Fzjc_Xcg_Xxb,Fzjc_Xcg_Qt,Fzjc_Ncg_Ndb,Fzjc_Ncg_Nt,Fzjc_Ncg_Ntt,Fzjc_Ncg_Nqx,Fzjc_Ncg_Qt,Fzjc_Xx_ABO,Fzjc_Xx_RH,Fzjc_Xt,Fzjc_Ggn_Xqgbzam,Fzjc_Ggn_Xqgczam,Fzjc_Ggn_Xdb,Fzjc_Ggn_Zdhs,Fzjc_Ggn_Jhdhs,Fzjc_Sgn_Xqjq,Fzjc_Sgn_Xnsd,Fzjc_Sgn_Xjnd,Fzjc_Sgn_Xnnd,Fzjc_YdFmw,Fzjc_YdFmw_Qt,Fzjc_YdQjd,Fzjc_Yg_Bmky,Fzjc_Yg_Bmkt,Fzjc_Yg_Eky,Fzjc_Yg_Ekt,Fzjc_Yg_Hxkt,Fzjc_Mdxqxsy,Fzjc_Mdxqxsy_sfgw,Fzjc_Mdxqxsy_gwsm,Fzjc_HIV_Jc,Fzjc_mdjc,Fzjc_Hiv,Fzjc_Bc,ZtPg,BjZd,Zz_Yw,Zz_Yy,Zz_Jg,NextVisitDate,Doctor,SFJG,CreateDate,UserID,CurVisitDate,BjZdQT,ZtPgQT,IsGWCF,ZDYJ ");
            strSql.Append(" FROM [NHS_YCF_CQFS_1] ");
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
                if (ds.Tables[0].Rows[0]["FnWeight"] != null && ds.Tables[0].Rows[0]["FnWeight"].ToString() != "")
                {
                    this.FnWeight = decimal.Parse(ds.Tables[0].Rows[0]["FnWeight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FnTzzs"] != null && ds.Tables[0].Rows[0]["FnTzzs"].ToString() != "")
                {
                    this.FnTzzs = decimal.Parse(ds.Tables[0].Rows[0]["FnTzzs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FnXyMax"] != null && ds.Tables[0].Rows[0]["FnXyMax"].ToString() != "")
                {
                    this.FnXyMax = decimal.Parse(ds.Tables[0].Rows[0]["FnXyMax"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FnXyMin"] != null && ds.Tables[0].Rows[0]["FnXyMin"].ToString() != "")
                {
                    this.FnXyMin = decimal.Parse(ds.Tables[0].Rows[0]["FnXyMin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TzXz"] != null && ds.Tables[0].Rows[0]["TzXz"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["TzXz"].ToString() == "1") || (ds.Tables[0].Rows[0]["TzXz"].ToString().ToLower() == "true"))
                    {
                        this.TzXz = true;
                    }
                    else
                    {
                        this.TzXz = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["TzXz_Other"] != null)
                {
                    this.TzXz_Other = ds.Tables[0].Rows[0]["TzXz_Other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TzFb"] != null && ds.Tables[0].Rows[0]["TzFb"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["TzFb"].ToString() == "1") || (ds.Tables[0].Rows[0]["TzFb"].ToString().ToLower() == "true"))
                    {
                        this.TzFb = true;
                    }
                    else
                    {
                        this.TzFb = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["TzFb_Other"] != null)
                {
                    this.TzFb_Other = ds.Tables[0].Rows[0]["TzFb_Other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fkjc_Wy"] != null && ds.Tables[0].Rows[0]["Fkjc_Wy"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fkjc_Wy"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fkjc_Wy"].ToString().ToLower() == "true"))
                    {
                        this.Fkjc_Wy = true;
                    }
                    else
                    {
                        this.Fkjc_Wy = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Fkjc_Wy_Other"] != null)
                {
                    this.Fkjc_Wy_Other = ds.Tables[0].Rows[0]["Fkjc_Wy_Other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fkjc_Yd"] != null && ds.Tables[0].Rows[0]["Fkjc_Yd"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fkjc_Yd"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fkjc_Yd"].ToString().ToLower() == "true"))
                    {
                        this.Fkjc_Yd = true;
                    }
                    else
                    {
                        this.Fkjc_Yd = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Fkjc_Yd_Other"] != null)
                {
                    this.Fkjc_Yd_Other = ds.Tables[0].Rows[0]["Fkjc_Yd_Other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fkjc_Gj"] != null && ds.Tables[0].Rows[0]["Fkjc_Gj"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fkjc_Gj"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fkjc_Gj"].ToString().ToLower() == "true"))
                    {
                        this.Fkjc_Gj = true;
                    }
                    else
                    {
                        this.Fkjc_Gj = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Fkjc_Gj_Other"] != null)
                {
                    this.Fkjc_Gj_Other = ds.Tables[0].Rows[0]["Fkjc_Gj_Other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fkjc_Zg"] != null && ds.Tables[0].Rows[0]["Fkjc_Zg"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fkjc_Zg"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fkjc_Zg"].ToString().ToLower() == "true"))
                    {
                        this.Fkjc_Zg = true;
                    }
                    else
                    {
                        this.Fkjc_Zg = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Fkjc_Zg_Other"] != null)
                {
                    this.Fkjc_Zg_Other = ds.Tables[0].Rows[0]["Fkjc_Zg_Other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fkjc_Fj"] != null && ds.Tables[0].Rows[0]["Fkjc_Fj"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fkjc_Fj"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fkjc_Fj"].ToString().ToLower() == "true"))
                    {
                        this.Fkjc_Fj = true;
                    }
                    else
                    {
                        this.Fkjc_Fj = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Fkjc_Fj_Other"] != null)
                {
                    this.Fkjc_Fj_Other = ds.Tables[0].Rows[0]["Fkjc_Fj_Other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Xcg_Xhdb"] != null && ds.Tables[0].Rows[0]["Fzjc_Xcg_Xhdb"].ToString() != "")
                {
                    this.Fzjc_Xcg_Xhdb = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Xcg_Xhdb"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Xcg_Bxb"] != null && ds.Tables[0].Rows[0]["Fzjc_Xcg_Bxb"].ToString() != "")
                {
                    this.Fzjc_Xcg_Bxb = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Xcg_Bxb"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Xcg_Xxb"] != null && ds.Tables[0].Rows[0]["Fzjc_Xcg_Xxb"].ToString() != "")
                {
                    this.Fzjc_Xcg_Xxb = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Xcg_Xxb"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Xcg_Qt"] != null)
                {
                    this.Fzjc_Xcg_Qt = ds.Tables[0].Rows[0]["Fzjc_Xcg_Qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ncg_Ndb"] != null && ds.Tables[0].Rows[0]["Fzjc_Ncg_Ndb"].ToString() != "")
                {
                    this.Fzjc_Ncg_Ndb = int.Parse(ds.Tables[0].Rows[0]["Fzjc_Ncg_Ndb"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ncg_Nt"] != null && ds.Tables[0].Rows[0]["Fzjc_Ncg_Nt"].ToString() != "")
                {
                    this.Fzjc_Ncg_Nt = int.Parse(ds.Tables[0].Rows[0]["Fzjc_Ncg_Nt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ncg_Ntt"] != null && ds.Tables[0].Rows[0]["Fzjc_Ncg_Ntt"].ToString() != "")
                {
                    this.Fzjc_Ncg_Ntt = int.Parse(ds.Tables[0].Rows[0]["Fzjc_Ncg_Ntt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ncg_Nqx"] != null && ds.Tables[0].Rows[0]["Fzjc_Ncg_Nqx"].ToString() != "")
                {
                    this.Fzjc_Ncg_Nqx = int.Parse(ds.Tables[0].Rows[0]["Fzjc_Ncg_Nqx"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ncg_Qt"] != null)
                {
                    this.Fzjc_Ncg_Qt = ds.Tables[0].Rows[0]["Fzjc_Ncg_Qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Xx_ABO"] != null)
                {
                    this.Fzjc_Xx_ABO = ds.Tables[0].Rows[0]["Fzjc_Xx_ABO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Xx_RH"] != null)
                {
                    this.Fzjc_Xx_RH = ds.Tables[0].Rows[0]["Fzjc_Xx_RH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Xt"] != null && ds.Tables[0].Rows[0]["Fzjc_Xt"].ToString() != "")
                {
                    this.Fzjc_Xt = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Xt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ggn_Xqgbzam"] != null && ds.Tables[0].Rows[0]["Fzjc_Ggn_Xqgbzam"].ToString() != "")
                {
                    this.Fzjc_Ggn_Xqgbzam = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Ggn_Xqgbzam"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ggn_Xqgczam"] != null && ds.Tables[0].Rows[0]["Fzjc_Ggn_Xqgczam"].ToString() != "")
                {
                    this.Fzjc_Ggn_Xqgczam = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Ggn_Xqgczam"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ggn_Xdb"] != null && ds.Tables[0].Rows[0]["Fzjc_Ggn_Xdb"].ToString() != "")
                {
                    this.Fzjc_Ggn_Xdb = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Ggn_Xdb"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ggn_Zdhs"] != null && ds.Tables[0].Rows[0]["Fzjc_Ggn_Zdhs"].ToString() != "")
                {
                    this.Fzjc_Ggn_Zdhs = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Ggn_Zdhs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Ggn_Jhdhs"] != null && ds.Tables[0].Rows[0]["Fzjc_Ggn_Jhdhs"].ToString() != "")
                {
                    this.Fzjc_Ggn_Jhdhs = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Ggn_Jhdhs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Sgn_Xqjq"] != null && ds.Tables[0].Rows[0]["Fzjc_Sgn_Xqjq"].ToString() != "")
                {
                    this.Fzjc_Sgn_Xqjq = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Sgn_Xqjq"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Sgn_Xnsd"] != null && ds.Tables[0].Rows[0]["Fzjc_Sgn_Xnsd"].ToString() != "")
                {
                    this.Fzjc_Sgn_Xnsd = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Sgn_Xnsd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Sgn_Xjnd"] != null && ds.Tables[0].Rows[0]["Fzjc_Sgn_Xjnd"].ToString() != "")
                {
                    this.Fzjc_Sgn_Xjnd = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Sgn_Xjnd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Sgn_Xnnd"] != null && ds.Tables[0].Rows[0]["Fzjc_Sgn_Xnnd"].ToString() != "")
                {
                    this.Fzjc_Sgn_Xnnd = decimal.Parse(ds.Tables[0].Rows[0]["Fzjc_Sgn_Xnnd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_YdFmw"] != null)
                {
                    this.Fzjc_YdFmw = ds.Tables[0].Rows[0]["Fzjc_YdFmw"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_YdFmw_Qt"] != null)
                {
                    this.Fzjc_YdFmw_Qt = ds.Tables[0].Rows[0]["Fzjc_YdFmw_Qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_YdQjd"] != null && ds.Tables[0].Rows[0]["Fzjc_YdQjd"].ToString() != "")
                {
                    this.Fzjc_YdQjd = int.Parse(ds.Tables[0].Rows[0]["Fzjc_YdQjd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Yg_Bmky"] != null && ds.Tables[0].Rows[0]["Fzjc_Yg_Bmky"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fzjc_Yg_Bmky"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fzjc_Yg_Bmky"].ToString().ToLower() == "true"))
                    {
                        this.Fzjc_Yg_Bmky = true;
                    }
                    else
                    {
                        this.Fzjc_Yg_Bmky = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Yg_Bmkt"] != null && ds.Tables[0].Rows[0]["Fzjc_Yg_Bmkt"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fzjc_Yg_Bmkt"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fzjc_Yg_Bmkt"].ToString().ToLower() == "true"))
                    {
                        this.Fzjc_Yg_Bmkt = true;
                    }
                    else
                    {
                        this.Fzjc_Yg_Bmkt = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Yg_Eky"] != null && ds.Tables[0].Rows[0]["Fzjc_Yg_Eky"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fzjc_Yg_Eky"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fzjc_Yg_Eky"].ToString().ToLower() == "true"))
                    {
                        this.Fzjc_Yg_Eky = true;
                    }
                    else
                    {
                        this.Fzjc_Yg_Eky = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Yg_Ekt"] != null && ds.Tables[0].Rows[0]["Fzjc_Yg_Ekt"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fzjc_Yg_Ekt"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fzjc_Yg_Ekt"].ToString().ToLower() == "true"))
                    {
                        this.Fzjc_Yg_Ekt = true;
                    }
                    else
                    {
                        this.Fzjc_Yg_Ekt = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Yg_Hxkt"] != null && ds.Tables[0].Rows[0]["Fzjc_Yg_Hxkt"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fzjc_Yg_Hxkt"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fzjc_Yg_Hxkt"].ToString().ToLower() == "true"))
                    {
                        this.Fzjc_Yg_Hxkt = true;
                    }
                    else
                    {
                        this.Fzjc_Yg_Hxkt = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy"] != null && ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy"].ToString() != "")
                {
                    this.Fzjc_Mdxqxsy = int.Parse(ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy_sfgw"] != null && ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy_sfgw"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy_sfgw"].ToString() == "1") || (ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy_sfgw"].ToString().ToLower() == "true"))
                    {
                        this.Fzjc_Mdxqxsy_sfgw = true;
                    }
                    else
                    {
                        this.Fzjc_Mdxqxsy_sfgw = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy_gwsm"] != null)
                {
                    this.Fzjc_Mdxqxsy_gwsm = ds.Tables[0].Rows[0]["Fzjc_Mdxqxsy_gwsm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fzjc_HIV_Jc"] != null && ds.Tables[0].Rows[0]["Fzjc_HIV_Jc"].ToString() != "")
                {
                    this.Fzjc_HIV_Jc = int.Parse(ds.Tables[0].Rows[0]["Fzjc_HIV_Jc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_mdjc"] != null && ds.Tables[0].Rows[0]["Fzjc_mdjc"].ToString() != "")
                {
                    this.Fzjc_mdjc = int.Parse(ds.Tables[0].Rows[0]["Fzjc_mdjc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Hiv"] != null && ds.Tables[0].Rows[0]["Fzjc_Hiv"].ToString() != "")
                {
                    this.Fzjc_Hiv = int.Parse(ds.Tables[0].Rows[0]["Fzjc_Hiv"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fzjc_Bc"] != null)
                {
                    this.Fzjc_Bc = ds.Tables[0].Rows[0]["Fzjc_Bc"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZtPg"] != null)
                {
                    this.ZtPg = ds.Tables[0].Rows[0]["ZtPg"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BjZd"] != null)
                {
                    this.BjZd = ds.Tables[0].Rows[0]["BjZd"].ToString();
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
                if (ds.Tables[0].Rows[0]["CurVisitDate"] != null && ds.Tables[0].Rows[0]["CurVisitDate"].ToString() != "")
                {
                    this.CurVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["CurVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BjZdQT"] != null)
                {
                    this.BjZdQT = ds.Tables[0].Rows[0]["BjZdQT"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZtPgQT"] != null)
                {
                    this.ZtPgQT = ds.Tables[0].Rows[0]["ZtPgQT"].ToString();
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
            strSql.Append(" FROM [NHS_YCF_CQFS_1] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
