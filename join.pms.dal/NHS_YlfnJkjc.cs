using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using UNV.Comm.DataBase;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_YlfnJkjc。
    /// </summary>
    [Serializable]
    public partial class NHS_YlfnJkjc
    {
        public NHS_YlfnJkjc()
        { }
        #region Model
        private int _commid;
        private string _unvid;
        private string _qcfwzbm;
        private string _fnname;
        private string _fncid;
        private int? _fnage;
        private int? _jhage;
        private string _fntel;
        private int? _fnhyzk;
        private string _homeaddress;
        private string _areacode;
        private DateTime? _mociyj;
        private int? _bycs;
        private int? _bjage;
        private int? _yun;
        private int? _chan;
        private int? _sex1;
        private int? _sex0;
        private int? _zuyue;
        private int? _paofu;
        private int? _zaochan;
        private int? _chansi;
        private int? _sitai;
        private int? _yaoliu;
        private int? _renliu;
        private int? _ziliu;
        private int? _qita;
        private decimal? _tige_height;
        private decimal? _tige_weight;
        private decimal? _tige_xymax;
        private decimal? _tige_xymin;
        private int? _tige_maibo;
        private bool _ruxian_left;
        private string _ruxian_left_qt;
        private bool _ruxian_right;
        private string _ruxian_right_qt;
        private string _fkjc_wy;
        private string _fkjc_wy_zl;
        private string _fkjc_yd;
        private string _fkjc_gj;
        private int? _fkjc_gj_ml;
        private string _fkjc_gj_zl;
        private decimal? _fkjc_gj_yd;
        private string _fkjc_gj_zt;
        private string _fkjc_gj_qt;
        private decimal? _fkjc_gt_big;
        private string _fkjc_gt_map;
        private int? _fkjc_gt_hyd;
        private int? _fkjc_gt_tc;
        private string _fkjc_gt_zl;
        private string _fkjc_gt_qt;
        private bool _fkjc_fj_left;
        private string _fkjc_fj_left_qt;
        private bool _fkjc_fj_right;
        private string _fkjc_fj_right_qt;
        private string _penqiang_b;
        private string _jc_doctor1;
        private string _fzjc_bdtp;
        private string _fzjc_gjgp;
        private string _jc_doctor2;
        private string _dzydj;
        private string _jc_doctor3;
        private int? _cbzd_rx_e1;
        private int? _cbzd_rx_e2;
        private int? _cbzd_rx_e3;
        private int? _cbzd_fk_e4;
        private int? _cbzd_fk_e5;
        private int? _cbzd_fk_e6;
        private int? _cbzd_fk_e7;
        private int? _cbzd_fk_e8;
        private int? _cbzd_fk_e9;
        private int? _cbzd_fk_e10;
        private int? _cbzd_fk_e11;
        private int? _cbzd_fk_e12;
        private int? _cbzd_fk_e13;
        private int? _cbzd_fk_e14;
        private int? _cbzd_fk_e15;
        private int? _cbzd_fk_e16;
        private string _cbzd_jielun;
        private string _jc_doctor4;
        private string _gzlyj;
        private string _zlcs;
        private string _jc_doctor5;
        private string _zlxg;
        private string _jc_doctor6;
        private DateTime? _checkdate;
        private string _checkjg;
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
        /// 服务证编号
        /// </summary>
        public string QcfwzBm
        {
            set { _qcfwzbm = value; }
            get { return _qcfwzbm; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string FnName
        {
            set { _fnname = value; }
            get { return _fnname; }
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string FnCID
        {
            set { _fncid = value; }
            get { return _fncid; }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? FnAge
        {
            set { _fnage = value; }
            get { return _fnage; }
        }
        /// <summary>
        /// 结婚年龄
        /// </summary>
        public int? JHAge
        {
            set { _jhage = value; }
            get { return _jhage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FnTel
        {
            set { _fntel = value; }
            get { return _fntel; }
        }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public int? FnHyzk
        {
            set { _fnhyzk = value; }
            get { return _fnhyzk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HomeAddress
        {
            set { _homeaddress = value; }
            get { return _homeaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AreaCode
        {
            set { _areacode = value; }
            get { return _areacode; }
        }
        /// <summary>
        /// 未次月经
        /// </summary>
        public DateTime? MoCiYj
        {
            set { _mociyj = value; }
            get { return _mociyj; }
        }
        /// <summary>
        /// 避孕措施
        /// </summary>
        public int? Bycs
        {
            set { _bycs = value; }
            get { return _bycs; }
        }
        /// <summary>
        /// 闭经年龄
        /// </summary>
        public int? BJAge
        {
            set { _bjage = value; }
            get { return _bjage; }
        }
        /// <summary>
        /// 孕
        /// </summary>
        public int? Yun
        {
            set { _yun = value; }
            get { return _yun; }
        }
        /// <summary>
        /// 产
        /// </summary>
        public int? Chan
        {
            set { _chan = value; }
            get { return _chan; }
        }
        /// <summary>
        /// 男
        /// </summary>
        public int? Sex1
        {
            set { _sex1 = value; }
            get { return _sex1; }
        }
        /// <summary>
        /// 女
        /// </summary>
        public int? Sex0
        {
            set { _sex0 = value; }
            get { return _sex0; }
        }
        /// <summary>
        /// 足月
        /// </summary>
        public int? Zuyue
        {
            set { _zuyue = value; }
            get { return _zuyue; }
        }
        /// <summary>
        /// 剖腹
        /// </summary>
        public int? Paofu
        {
            set { _paofu = value; }
            get { return _paofu; }
        }
        /// <summary>
        /// 早产
        /// </summary>
        public int? Zaochan
        {
            set { _zaochan = value; }
            get { return _zaochan; }
        }
        /// <summary>
        /// 产死
        /// </summary>
        public int? Chansi
        {
            set { _chansi = value; }
            get { return _chansi; }
        }
        /// <summary>
        /// 死胎龄
        /// </summary>
        public int? Sitai
        {
            set { _sitai = value; }
            get { return _sitai; }
        }
        /// <summary>
        /// 药流
        /// </summary>
        public int? Yaoliu
        {
            set { _yaoliu = value; }
            get { return _yaoliu; }
        }
        /// <summary>
        /// 人流
        /// </summary>
        public int? Renliu
        {
            set { _renliu = value; }
            get { return _renliu; }
        }
        /// <summary>
        /// 自流
        /// </summary>
        public int? Ziliu
        {
            set { _ziliu = value; }
            get { return _ziliu; }
        }
        /// <summary>
        /// 其它
        /// </summary>
        public int? Qita
        {
            set { _qita = value; }
            get { return _qita; }
        }
        /// <summary>
        /// 身高
        /// </summary>
        public decimal? Tige_height
        {
            set { _tige_height = value; }
            get { return _tige_height; }
        }
        /// <summary>
        /// 体重
        /// </summary>
        public decimal? Tige_weight
        {
            set { _tige_weight = value; }
            get { return _tige_weight; }
        }
        /// <summary>
        /// 血压
        /// </summary>
        public decimal? Tige_XyMax
        {
            set { _tige_xymax = value; }
            get { return _tige_xymax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Tige_XyMin
        {
            set { _tige_xymin = value; }
            get { return _tige_xymin; }
        }
        /// <summary>
        /// 脉搏
        /// </summary>
        public int? Tige_MaiBo
        {
            set { _tige_maibo = value; }
            get { return _tige_maibo; }
        }
        /// <summary>
        /// 左乳腺是否异常
        /// </summary>
        public bool Ruxian_left
        {
            set { _ruxian_left = value; }
            get { return _ruxian_left; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Ruxian_left_qt
        {
            set { _ruxian_left_qt = value; }
            get { return _ruxian_left_qt; }
        }
        /// <summary>
        /// 右乳腺是否异常
        /// </summary>
        public bool Ruxian_right
        {
            set { _ruxian_right = value; }
            get { return _ruxian_right; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Ruxian_right_qt
        {
            set { _ruxian_right_qt = value; }
            get { return _ruxian_right_qt; }
        }
        /// <summary>
        /// 外阴状况
        /// </summary>
        public string FKJC_wy
        {
            set { _fkjc_wy = value; }
            get { return _fkjc_wy; }
        }
        /// <summary>
        /// 外阴肿瘤数
        /// </summary>
        public string FKJC_wy_zl
        {
            set { _fkjc_wy_zl = value; }
            get { return _fkjc_wy_zl; }
        }
        /// <summary>
        /// 外阴状况
        /// </summary>
        public string FKJC_yd
        {
            set { _fkjc_yd = value; }
            get { return _fkjc_yd; }
        }
        /// <summary>
        /// 宫颈状况
        /// </summary>
        public string FKJC_gj
        {
            set { _fkjc_gj = value; }
            get { return _fkjc_gj; }
        }
        /// <summary>
        /// 宫颈糜烂度数
        /// </summary>
        public int? FKJC_gj_ml
        {
            set { _fkjc_gj_ml = value; }
            get { return _fkjc_gj_ml; }
        }
        /// <summary>
        /// 宫颈肿瘤数
        /// </summary>
        public string FKJC_gj_zl
        {
            set { _fkjc_gj_zl = value; }
            get { return _fkjc_gj_zl; }
        }
        /// <summary>
        /// 宫颈硬度
        /// </summary>
        public decimal? FKJC_gj_yd
        {
            set { _fkjc_gj_yd = value; }
            get { return _fkjc_gj_yd; }
        }
        /// <summary>
        /// 宫颈形态
        /// </summary>
        public string FKJC_gj_zt
        {
            set { _fkjc_gj_zt = value; }
            get { return _fkjc_gj_zt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FKJC_gj_qt
        {
            set { _fkjc_gj_qt = value; }
            get { return _fkjc_gj_qt; }
        }
        /// <summary>
        /// 宫体大小
        /// </summary>
        public decimal? FKJC_gt_big
        {
            set { _fkjc_gt_big = value; }
            get { return _fkjc_gt_big; }
        }
        /// <summary>
        /// 位置
        /// </summary>
        public string FKJC_gt_map
        {
            set { _fkjc_gt_map = value; }
            get { return _fkjc_gt_map; }
        }
        /// <summary>
        /// 活跃度
        /// </summary>
        public int? FKJC_gt_hyd
        {
            set { _fkjc_gt_hyd = value; }
            get { return _fkjc_gt_hyd; }
        }
        /// <summary>
        /// 脱垂度
        /// </summary>
        public int? FKJC_gt_tc
        {
            set { _fkjc_gt_tc = value; }
            get { return _fkjc_gt_tc; }
        }
        /// <summary>
        /// 肿瘤数
        /// </summary>
        public string FKJC_gt_zl
        {
            set { _fkjc_gt_zl = value; }
            get { return _fkjc_gt_zl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FKJC_gt_qt
        {
            set { _fkjc_gt_qt = value; }
            get { return _fkjc_gt_qt; }
        }
        /// <summary>
        /// 左附件异常
        /// </summary>
        public bool FKJC_fj_left
        {
            set { _fkjc_fj_left = value; }
            get { return _fkjc_fj_left; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FKJC_fj_left_qt
        {
            set { _fkjc_fj_left_qt = value; }
            get { return _fkjc_fj_left_qt; }
        }
        /// <summary>
        /// 右附件异常
        /// </summary>
        public bool FKJC_fj_right
        {
            set { _fkjc_fj_right = value; }
            get { return _fkjc_fj_right; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FKJC_fj_right_qt
        {
            set { _fkjc_fj_right_qt = value; }
            get { return _fkjc_fj_right_qt; }
        }
        /// <summary>
        /// B超
        /// </summary>
        public string Penqiang_B
        {
            set { _penqiang_b = value; }
            get { return _penqiang_b; }
        }
        /// <summary>
        /// 医生
        /// </summary>
        public string JC_Doctor1
        {
            set { _jc_doctor1 = value; }
            get { return _jc_doctor1; }
        }
        /// <summary>
        /// 白带涂片
        /// </summary>
        public string FZJC_bdtp
        {
            set { _fzjc_bdtp = value; }
            get { return _fzjc_bdtp; }
        }
        /// <summary>
        /// 宫颈刮片
        /// </summary>
        public string FZJC_gjgp
        {
            set { _fzjc_gjgp = value; }
            get { return _fzjc_gjgp; }
        }
        /// <summary>
        /// 医生
        /// </summary>
        public string JC_Doctor2
        {
            set { _jc_doctor2 = value; }
            get { return _jc_doctor2; }
        }
        /// <summary>
        /// 电子阴道镜
        /// </summary>
        public string DZYDJ
        {
            set { _dzydj = value; }
            get { return _dzydj; }
        }
        /// <summary>
        /// 医生
        /// </summary>
        public string JC_Doctor3
        {
            set { _jc_doctor3 = value; }
            get { return _jc_doctor3; }
        }
        /// <summary>
        /// 初步诊断
        /// </summary>
        public int? CBZD_RX_E1
        {
            set { _cbzd_rx_e1 = value; }
            get { return _cbzd_rx_e1; }
        }
        /// <summary>
        /// 初步诊断
        /// </summary>
        public int? CBZD_RX_E2
        {
            set { _cbzd_rx_e2 = value; }
            get { return _cbzd_rx_e2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CBZD_RX_E3
        {
            set { _cbzd_rx_e3 = value; }
            get { return _cbzd_rx_e3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CBZD_FK_E4
        {
            set { _cbzd_fk_e4 = value; }
            get { return _cbzd_fk_e4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CBZD_FK_E5
        {
            set { _cbzd_fk_e5 = value; }
            get { return _cbzd_fk_e5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CBZD_FK_E6
        {
            set { _cbzd_fk_e6 = value; }
            get { return _cbzd_fk_e6; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CBZD_FK_E7
        {
            set { _cbzd_fk_e7 = value; }
            get { return _cbzd_fk_e7; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CBZD_FK_E8
        {
            set { _cbzd_fk_e8 = value; }
            get { return _cbzd_fk_e8; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CBZD_FK_E9
        {
            set { _cbzd_fk_e9 = value; }
            get { return _cbzd_fk_e9; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CBZD_FK_E10
        {
            set { _cbzd_fk_e10 = value; }
            get { return _cbzd_fk_e10; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CBZD_FK_E11
        {
            set { _cbzd_fk_e11 = value; }
            get { return _cbzd_fk_e11; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CBZD_FK_E12
        {
            set { _cbzd_fk_e12 = value; }
            get { return _cbzd_fk_e12; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CBZD_FK_E13
        {
            set { _cbzd_fk_e13 = value; }
            get { return _cbzd_fk_e13; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CBZD_FK_E14
        {
            set { _cbzd_fk_e14 = value; }
            get { return _cbzd_fk_e14; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CBZD_FK_E15
        {
            set { _cbzd_fk_e15 = value; }
            get { return _cbzd_fk_e15; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CBZD_FK_E16
        {
            set { _cbzd_fk_e16 = value; }
            get { return _cbzd_fk_e16; }
        }
        /// <summary>
        /// 初步诊断结论
        /// </summary>
        public string CBZD_jielun
        {
            set { _cbzd_jielun = value; }
            get { return _cbzd_jielun; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JC_Doctor4
        {
            set { _jc_doctor4 = value; }
            get { return _jc_doctor4; }
        }
        /// <summary>
        /// 治疗意见
        /// </summary>
        public string Gzlyj
        {
            set { _gzlyj = value; }
            get { return _gzlyj; }
        }
        /// <summary>
        /// 治疗措施
        /// </summary>
        public string Zlcs
        {
            set { _zlcs = value; }
            get { return _zlcs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JC_Doctor5
        {
            set { _jc_doctor5 = value; }
            get { return _jc_doctor5; }
        }
        /// <summary>
        /// 治疗效果
        /// </summary>
        public string Zlxg
        {
            set { _zlxg = value; }
            get { return _zlxg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JC_Doctor6
        {
            set { _jc_doctor6 = value; }
            get { return _jc_doctor6; }
        }
        /// <summary>
        /// 检查日期
        /// </summary>
        public DateTime? CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        /// <summary>
        /// 查检机构
        /// </summary>
        public string CheckJG
        {
            set { _checkjg = value; }
            get { return _checkjg; }
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
        public NHS_YlfnJkjc(int CommID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommID,UnvID,QcfwzBm,FnName,FnCID,FnAge,JHAge,FnTel,FnHyzk,HomeAddress,AreaCode,MoCiYj,Bycs,BJAge,Yun,Chan,Sex1,Sex0,Zuyue,Paofu,Zaochan,Chansi,Sitai,Yaoliu,Renliu,Ziliu,Qita,Tige_height,Tige_weight,Tige_XyMax,Tige_XyMin,Tige_MaiBo,Ruxian_left,Ruxian_left_qt,Ruxian_right,Ruxian_right_qt,FKJC_wy,FKJC_wy_zl,FKJC_yd,FKJC_gj,FKJC_gj_ml,FKJC_gj_zl,FKJC_gj_yd,FKJC_gj_zt,FKJC_gj_qt,FKJC_gt_big,FKJC_gt_map,FKJC_gt_hyd,FKJC_gt_tc,FKJC_gt_zl,FKJC_gt_qt,FKJC_fj_left,FKJC_fj_left_qt,FKJC_fj_right,FKJC_fj_right_qt,Penqiang_B,JC_Doctor1,FZJC_bdtp,FZJC_gjgp,JC_Doctor2,DZYDJ,JC_Doctor3,CBZD_RX_E1,CBZD_RX_E2,CBZD_RX_E3,CBZD_FK_E4,CBZD_FK_E5,CBZD_FK_E6,CBZD_FK_E7,CBZD_FK_E8,CBZD_FK_E9,CBZD_FK_E10,CBZD_FK_E11,CBZD_FK_E12,CBZD_FK_E13,CBZD_FK_E14,CBZD_FK_E15,CBZD_FK_E16,CBZD_jielun,JC_Doctor4,Gzlyj,Zlcs,JC_Doctor5,Zlxg,JC_Doctor6,CheckDate,CheckJG,CreateDate,UserID ");
            strSql.Append(" FROM [NHS_YlfnJkjc] ");
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
                if (ds.Tables[0].Rows[0]["FnAge"] != null && ds.Tables[0].Rows[0]["FnAge"].ToString() != "")
                {
                    this.FnAge = int.Parse(ds.Tables[0].Rows[0]["FnAge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JHAge"] != null && ds.Tables[0].Rows[0]["JHAge"].ToString() != "")
                {
                    this.JHAge = int.Parse(ds.Tables[0].Rows[0]["JHAge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FnTel"] != null)
                {
                    this.FnTel = ds.Tables[0].Rows[0]["FnTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnHyzk"] != null && ds.Tables[0].Rows[0]["FnHyzk"].ToString() != "")
                {
                    this.FnHyzk = int.Parse(ds.Tables[0].Rows[0]["FnHyzk"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HomeAddress"] != null)
                {
                    this.HomeAddress = ds.Tables[0].Rows[0]["HomeAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AreaCode"] != null)
                {
                    this.AreaCode = ds.Tables[0].Rows[0]["AreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MoCiYj"] != null && ds.Tables[0].Rows[0]["MoCiYj"].ToString() != "")
                {
                    this.MoCiYj = DateTime.Parse(ds.Tables[0].Rows[0]["MoCiYj"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Bycs"] != null && ds.Tables[0].Rows[0]["Bycs"].ToString() != "")
                {
                    this.Bycs = int.Parse(ds.Tables[0].Rows[0]["Bycs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BJAge"] != null && ds.Tables[0].Rows[0]["BJAge"].ToString() != "")
                {
                    this.BJAge = int.Parse(ds.Tables[0].Rows[0]["BJAge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Yun"] != null && ds.Tables[0].Rows[0]["Yun"].ToString() != "")
                {
                    this.Yun = int.Parse(ds.Tables[0].Rows[0]["Yun"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Chan"] != null && ds.Tables[0].Rows[0]["Chan"].ToString() != "")
                {
                    this.Chan = int.Parse(ds.Tables[0].Rows[0]["Chan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sex1"] != null && ds.Tables[0].Rows[0]["Sex1"].ToString() != "")
                {
                    this.Sex1 = int.Parse(ds.Tables[0].Rows[0]["Sex1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sex0"] != null && ds.Tables[0].Rows[0]["Sex0"].ToString() != "")
                {
                    this.Sex0 = int.Parse(ds.Tables[0].Rows[0]["Sex0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Zuyue"] != null && ds.Tables[0].Rows[0]["Zuyue"].ToString() != "")
                {
                    this.Zuyue = int.Parse(ds.Tables[0].Rows[0]["Zuyue"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Paofu"] != null && ds.Tables[0].Rows[0]["Paofu"].ToString() != "")
                {
                    this.Paofu = int.Parse(ds.Tables[0].Rows[0]["Paofu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Zaochan"] != null && ds.Tables[0].Rows[0]["Zaochan"].ToString() != "")
                {
                    this.Zaochan = int.Parse(ds.Tables[0].Rows[0]["Zaochan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Chansi"] != null && ds.Tables[0].Rows[0]["Chansi"].ToString() != "")
                {
                    this.Chansi = int.Parse(ds.Tables[0].Rows[0]["Chansi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sitai"] != null && ds.Tables[0].Rows[0]["Sitai"].ToString() != "")
                {
                    this.Sitai = int.Parse(ds.Tables[0].Rows[0]["Sitai"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Yaoliu"] != null && ds.Tables[0].Rows[0]["Yaoliu"].ToString() != "")
                {
                    this.Yaoliu = int.Parse(ds.Tables[0].Rows[0]["Yaoliu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Renliu"] != null && ds.Tables[0].Rows[0]["Renliu"].ToString() != "")
                {
                    this.Renliu = int.Parse(ds.Tables[0].Rows[0]["Renliu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ziliu"] != null && ds.Tables[0].Rows[0]["Ziliu"].ToString() != "")
                {
                    this.Ziliu = int.Parse(ds.Tables[0].Rows[0]["Ziliu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qita"] != null && ds.Tables[0].Rows[0]["Qita"].ToString() != "")
                {
                    this.Qita = int.Parse(ds.Tables[0].Rows[0]["Qita"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Tige_height"] != null && ds.Tables[0].Rows[0]["Tige_height"].ToString() != "")
                {
                    this.Tige_height = decimal.Parse(ds.Tables[0].Rows[0]["Tige_height"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Tige_weight"] != null && ds.Tables[0].Rows[0]["Tige_weight"].ToString() != "")
                {
                    this.Tige_weight = decimal.Parse(ds.Tables[0].Rows[0]["Tige_weight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Tige_XyMax"] != null && ds.Tables[0].Rows[0]["Tige_XyMax"].ToString() != "")
                {
                    this.Tige_XyMax = decimal.Parse(ds.Tables[0].Rows[0]["Tige_XyMax"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Tige_XyMin"] != null && ds.Tables[0].Rows[0]["Tige_XyMin"].ToString() != "")
                {
                    this.Tige_XyMin = decimal.Parse(ds.Tables[0].Rows[0]["Tige_XyMin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Tige_MaiBo"] != null && ds.Tables[0].Rows[0]["Tige_MaiBo"].ToString() != "")
                {
                    this.Tige_MaiBo = int.Parse(ds.Tables[0].Rows[0]["Tige_MaiBo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ruxian_left"] != null && ds.Tables[0].Rows[0]["Ruxian_left"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Ruxian_left"].ToString() == "1") || (ds.Tables[0].Rows[0]["Ruxian_left"].ToString().ToLower() == "true"))
                    {
                        this.Ruxian_left = true;
                    }
                    else
                    {
                        this.Ruxian_left = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Ruxian_left_qt"] != null)
                {
                    this.Ruxian_left_qt = ds.Tables[0].Rows[0]["Ruxian_left_qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Ruxian_right"] != null && ds.Tables[0].Rows[0]["Ruxian_right"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Ruxian_right"].ToString() == "1") || (ds.Tables[0].Rows[0]["Ruxian_right"].ToString().ToLower() == "true"))
                    {
                        this.Ruxian_right = true;
                    }
                    else
                    {
                        this.Ruxian_right = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Ruxian_right_qt"] != null)
                {
                    this.Ruxian_right_qt = ds.Tables[0].Rows[0]["Ruxian_right_qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_wy"] != null)
                {
                    this.FKJC_wy = ds.Tables[0].Rows[0]["FKJC_wy"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_wy_zl"] != null)
                {
                    this.FKJC_wy_zl = ds.Tables[0].Rows[0]["FKJC_wy_zl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_yd"] != null)
                {
                    this.FKJC_yd = ds.Tables[0].Rows[0]["FKJC_yd"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_gj"] != null)
                {
                    this.FKJC_gj = ds.Tables[0].Rows[0]["FKJC_gj"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_gj_ml"] != null && ds.Tables[0].Rows[0]["FKJC_gj_ml"].ToString() != "")
                {
                    this.FKJC_gj_ml = int.Parse(ds.Tables[0].Rows[0]["FKJC_gj_ml"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FKJC_gj_zl"] != null)
                {
                    this.FKJC_gj_zl = ds.Tables[0].Rows[0]["FKJC_gj_zl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_gj_yd"] != null && ds.Tables[0].Rows[0]["FKJC_gj_yd"].ToString() != "")
                {
                    this.FKJC_gj_yd = decimal.Parse(ds.Tables[0].Rows[0]["FKJC_gj_yd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FKJC_gj_zt"] != null)
                {
                    this.FKJC_gj_zt = ds.Tables[0].Rows[0]["FKJC_gj_zt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_gj_qt"] != null)
                {
                    this.FKJC_gj_qt = ds.Tables[0].Rows[0]["FKJC_gj_qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_gt_big"] != null && ds.Tables[0].Rows[0]["FKJC_gt_big"].ToString() != "")
                {
                    this.FKJC_gt_big = decimal.Parse(ds.Tables[0].Rows[0]["FKJC_gt_big"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FKJC_gt_map"] != null)
                {
                    this.FKJC_gt_map = ds.Tables[0].Rows[0]["FKJC_gt_map"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_gt_hyd"] != null && ds.Tables[0].Rows[0]["FKJC_gt_hyd"].ToString() != "")
                {
                    this.FKJC_gt_hyd = int.Parse(ds.Tables[0].Rows[0]["FKJC_gt_hyd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FKJC_gt_tc"] != null && ds.Tables[0].Rows[0]["FKJC_gt_tc"].ToString() != "")
                {
                    this.FKJC_gt_tc = int.Parse(ds.Tables[0].Rows[0]["FKJC_gt_tc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FKJC_gt_zl"] != null)
                {
                    this.FKJC_gt_zl = ds.Tables[0].Rows[0]["FKJC_gt_zl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_gt_qt"] != null)
                {
                    this.FKJC_gt_qt = ds.Tables[0].Rows[0]["FKJC_gt_qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_fj_left"] != null && ds.Tables[0].Rows[0]["FKJC_fj_left"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["FKJC_fj_left"].ToString() == "1") || (ds.Tables[0].Rows[0]["FKJC_fj_left"].ToString().ToLower() == "true"))
                    {
                        this.FKJC_fj_left = true;
                    }
                    else
                    {
                        this.FKJC_fj_left = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["FKJC_fj_left_qt"] != null)
                {
                    this.FKJC_fj_left_qt = ds.Tables[0].Rows[0]["FKJC_fj_left_qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_fj_right"] != null && ds.Tables[0].Rows[0]["FKJC_fj_right"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["FKJC_fj_right"].ToString() == "1") || (ds.Tables[0].Rows[0]["FKJC_fj_right"].ToString().ToLower() == "true"))
                    {
                        this.FKJC_fj_right = true;
                    }
                    else
                    {
                        this.FKJC_fj_right = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["FKJC_fj_right_qt"] != null)
                {
                    this.FKJC_fj_right_qt = ds.Tables[0].Rows[0]["FKJC_fj_right_qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Penqiang_B"] != null)
                {
                    this.Penqiang_B = ds.Tables[0].Rows[0]["Penqiang_B"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JC_Doctor1"] != null)
                {
                    this.JC_Doctor1 = ds.Tables[0].Rows[0]["JC_Doctor1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FZJC_bdtp"] != null)
                {
                    this.FZJC_bdtp = ds.Tables[0].Rows[0]["FZJC_bdtp"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FZJC_gjgp"] != null)
                {
                    this.FZJC_gjgp = ds.Tables[0].Rows[0]["FZJC_gjgp"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JC_Doctor2"] != null)
                {
                    this.JC_Doctor2 = ds.Tables[0].Rows[0]["JC_Doctor2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DZYDJ"] != null)
                {
                    this.DZYDJ = ds.Tables[0].Rows[0]["DZYDJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JC_Doctor3"] != null)
                {
                    this.JC_Doctor3 = ds.Tables[0].Rows[0]["JC_Doctor3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CBZD_RX_E1"] != null && ds.Tables[0].Rows[0]["CBZD_RX_E1"].ToString() != "")
                {
                    this.CBZD_RX_E1 = int.Parse(ds.Tables[0].Rows[0]["CBZD_RX_E1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_RX_E2"] != null && ds.Tables[0].Rows[0]["CBZD_RX_E2"].ToString() != "")
                {
                    this.CBZD_RX_E2 = int.Parse(ds.Tables[0].Rows[0]["CBZD_RX_E2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_RX_E3"] != null && ds.Tables[0].Rows[0]["CBZD_RX_E3"].ToString() != "")
                {
                    this.CBZD_RX_E3 = int.Parse(ds.Tables[0].Rows[0]["CBZD_RX_E3"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E4"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E4"].ToString() != "")
                {
                    this.CBZD_FK_E4 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E4"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E5"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E5"].ToString() != "")
                {
                    this.CBZD_FK_E5 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E5"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E6"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E6"].ToString() != "")
                {
                    this.CBZD_FK_E6 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E6"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E7"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E7"].ToString() != "")
                {
                    this.CBZD_FK_E7 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E7"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E8"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E8"].ToString() != "")
                {
                    this.CBZD_FK_E8 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E8"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E9"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E9"].ToString() != "")
                {
                    this.CBZD_FK_E9 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E9"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E10"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E10"].ToString() != "")
                {
                    this.CBZD_FK_E10 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E10"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E11"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E11"].ToString() != "")
                {
                    this.CBZD_FK_E11 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E11"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E12"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E12"].ToString() != "")
                {
                    this.CBZD_FK_E12 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E12"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E13"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E13"].ToString() != "")
                {
                    this.CBZD_FK_E13 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E13"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E14"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E14"].ToString() != "")
                {
                    this.CBZD_FK_E14 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E14"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E15"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E15"].ToString() != "")
                {
                    this.CBZD_FK_E15 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E15"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E16"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E16"].ToString() != "")
                {
                    this.CBZD_FK_E16 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E16"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_jielun"] != null)
                {
                    this.CBZD_jielun = ds.Tables[0].Rows[0]["CBZD_jielun"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JC_Doctor4"] != null)
                {
                    this.JC_Doctor4 = ds.Tables[0].Rows[0]["JC_Doctor4"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Gzlyj"] != null)
                {
                    this.Gzlyj = ds.Tables[0].Rows[0]["Gzlyj"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zlcs"] != null)
                {
                    this.Zlcs = ds.Tables[0].Rows[0]["Zlcs"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JC_Doctor5"] != null)
                {
                    this.JC_Doctor5 = ds.Tables[0].Rows[0]["JC_Doctor5"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zlxg"] != null)
                {
                    this.Zlxg = ds.Tables[0].Rows[0]["Zlxg"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JC_Doctor6"] != null)
                {
                    this.JC_Doctor6 = ds.Tables[0].Rows[0]["JC_Doctor6"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CheckDate"] != null && ds.Tables[0].Rows[0]["CheckDate"].ToString() != "")
                {
                    this.CheckDate = DateTime.Parse(ds.Tables[0].Rows[0]["CheckDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CheckJG"] != null)
                {
                    this.CheckJG = ds.Tables[0].Rows[0]["CheckJG"].ToString();
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
            strSql.Append("select count(1) from [NHS_YlfnJkjc]");
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
            strSql.Append("insert into [NHS_YlfnJkjc] (");
            strSql.Append("UnvID,QcfwzBm,FnName,FnCID,FnAge,JHAge,FnTel,FnHyzk,HomeAddress,AreaCode,MoCiYj,Bycs,BJAge,Yun,Chan,Sex1,Sex0,Zuyue,Paofu,Zaochan,Chansi,Sitai,Yaoliu,Renliu,Ziliu,Qita,Tige_height,Tige_weight,Tige_XyMax,Tige_XyMin,Tige_MaiBo,Ruxian_left,Ruxian_left_qt,Ruxian_right,Ruxian_right_qt,FKJC_wy,FKJC_wy_zl,FKJC_yd,FKJC_gj,FKJC_gj_ml,FKJC_gj_zl,FKJC_gj_yd,FKJC_gj_zt,FKJC_gj_qt,FKJC_gt_big,FKJC_gt_map,FKJC_gt_hyd,FKJC_gt_tc,FKJC_gt_zl,FKJC_gt_qt,FKJC_fj_left,FKJC_fj_left_qt,FKJC_fj_right,FKJC_fj_right_qt,Penqiang_B,JC_Doctor1,FZJC_bdtp,FZJC_gjgp,JC_Doctor2,DZYDJ,JC_Doctor3,CBZD_RX_E1,CBZD_RX_E2,CBZD_RX_E3,CBZD_FK_E4,CBZD_FK_E5,CBZD_FK_E6,CBZD_FK_E7,CBZD_FK_E8,CBZD_FK_E9,CBZD_FK_E10,CBZD_FK_E11,CBZD_FK_E12,CBZD_FK_E13,CBZD_FK_E14,CBZD_FK_E15,CBZD_FK_E16,CBZD_jielun,JC_Doctor4,Gzlyj,Zlcs,JC_Doctor5,Zlxg,JC_Doctor6,CheckDate,CheckJG,CreateDate,UserID)");
            strSql.Append(" values (");
            strSql.Append("@UnvID,@QcfwzBm,@FnName,@FnCID,@FnAge,@JHAge,@FnTel,@FnHyzk,@HomeAddress,@AreaCode,@MoCiYj,@Bycs,@BJAge,@Yun,@Chan,@Sex1,@Sex0,@Zuyue,@Paofu,@Zaochan,@Chansi,@Sitai,@Yaoliu,@Renliu,@Ziliu,@Qita,@Tige_height,@Tige_weight,@Tige_XyMax,@Tige_XyMin,@Tige_MaiBo,@Ruxian_left,@Ruxian_left_qt,@Ruxian_right,@Ruxian_right_qt,@FKJC_wy,@FKJC_wy_zl,@FKJC_yd,@FKJC_gj,@FKJC_gj_ml,@FKJC_gj_zl,@FKJC_gj_yd,@FKJC_gj_zt,@FKJC_gj_qt,@FKJC_gt_big,@FKJC_gt_map,@FKJC_gt_hyd,@FKJC_gt_tc,@FKJC_gt_zl,@FKJC_gt_qt,@FKJC_fj_left,@FKJC_fj_left_qt,@FKJC_fj_right,@FKJC_fj_right_qt,@Penqiang_B,@JC_Doctor1,@FZJC_bdtp,@FZJC_gjgp,@JC_Doctor2,@DZYDJ,@JC_Doctor3,@CBZD_RX_E1,@CBZD_RX_E2,@CBZD_RX_E3,@CBZD_FK_E4,@CBZD_FK_E5,@CBZD_FK_E6,@CBZD_FK_E7,@CBZD_FK_E8,@CBZD_FK_E9,@CBZD_FK_E10,@CBZD_FK_E11,@CBZD_FK_E12,@CBZD_FK_E13,@CBZD_FK_E14,@CBZD_FK_E15,@CBZD_FK_E16,@CBZD_jielun,@JC_Doctor4,@Gzlyj,@Zlcs,@JC_Doctor5,@Zlxg,@JC_Doctor6,@CheckDate,@CheckJG,@CreateDate,@UserID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,20),
					new SqlParameter("@QcfwzBm", SqlDbType.VarChar,50),
					new SqlParameter("@FnName", SqlDbType.NVarChar,20),
					new SqlParameter("@FnCID", SqlDbType.VarChar,20),
					new SqlParameter("@FnAge", SqlDbType.TinyInt,1),
					new SqlParameter("@JHAge", SqlDbType.TinyInt,1),
					new SqlParameter("@FnTel", SqlDbType.VarChar,50),
					new SqlParameter("@FnHyzk", SqlDbType.TinyInt,1),
					new SqlParameter("@HomeAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaCode", SqlDbType.VarChar,50),
					new SqlParameter("@MoCiYj", SqlDbType.SmallDateTime),
					new SqlParameter("@Bycs", SqlDbType.TinyInt,1),
					new SqlParameter("@BJAge", SqlDbType.TinyInt,1),
					new SqlParameter("@Yun", SqlDbType.TinyInt,1),
					new SqlParameter("@Chan", SqlDbType.TinyInt,1),
					new SqlParameter("@Sex1", SqlDbType.TinyInt,1),
					new SqlParameter("@Sex0", SqlDbType.TinyInt,1),
					new SqlParameter("@Zuyue", SqlDbType.TinyInt,1),
					new SqlParameter("@Paofu", SqlDbType.TinyInt,1),
					new SqlParameter("@Zaochan", SqlDbType.TinyInt,1),
					new SqlParameter("@Chansi", SqlDbType.TinyInt,1),
					new SqlParameter("@Sitai", SqlDbType.TinyInt,1),
					new SqlParameter("@Yaoliu", SqlDbType.TinyInt,1),
					new SqlParameter("@Renliu", SqlDbType.TinyInt,1),
					new SqlParameter("@Ziliu", SqlDbType.TinyInt,1),
					new SqlParameter("@Qita", SqlDbType.TinyInt,1),
					new SqlParameter("@Tige_height", SqlDbType.Float,8),
					new SqlParameter("@Tige_weight", SqlDbType.Float,8),
					new SqlParameter("@Tige_XyMax", SqlDbType.Float,8),
					new SqlParameter("@Tige_XyMin", SqlDbType.Float,8),
					new SqlParameter("@Tige_MaiBo", SqlDbType.TinyInt,1),
					new SqlParameter("@Ruxian_left", SqlDbType.Bit,1),
					new SqlParameter("@Ruxian_left_qt", SqlDbType.VarChar,50),
					new SqlParameter("@Ruxian_right", SqlDbType.Bit,1),
					new SqlParameter("@Ruxian_right_qt", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_wy", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_wy_zl", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_yd", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_gj", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_gj_ml", SqlDbType.TinyInt,1),
					new SqlParameter("@FKJC_gj_zl", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_gj_yd", SqlDbType.Float,8),
					new SqlParameter("@FKJC_gj_zt", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_gj_qt", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_gt_big", SqlDbType.Float,8),
					new SqlParameter("@FKJC_gt_map", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_gt_hyd", SqlDbType.TinyInt,1),
					new SqlParameter("@FKJC_gt_tc", SqlDbType.TinyInt,1),
					new SqlParameter("@FKJC_gt_zl", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_gt_qt", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_fj_left", SqlDbType.Bit,1),
					new SqlParameter("@FKJC_fj_left_qt", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_fj_right", SqlDbType.Bit,1),
					new SqlParameter("@FKJC_fj_right_qt", SqlDbType.VarChar,50),
					new SqlParameter("@Penqiang_B", SqlDbType.NVarChar,50),
					new SqlParameter("@JC_Doctor1", SqlDbType.VarChar,20),
					new SqlParameter("@FZJC_bdtp", SqlDbType.NVarChar,50),
					new SqlParameter("@FZJC_gjgp", SqlDbType.NVarChar,50),
					new SqlParameter("@JC_Doctor2", SqlDbType.VarChar,20),
					new SqlParameter("@DZYDJ", SqlDbType.NVarChar,50),
					new SqlParameter("@JC_Doctor3", SqlDbType.VarChar,20),
					new SqlParameter("@CBZD_RX_E1", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_RX_E2", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_RX_E3", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E4", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E5", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E6", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E7", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E8", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E9", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E10", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E11", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E12", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E13", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E14", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E15", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E16", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_jielun", SqlDbType.NVarChar,50),
					new SqlParameter("@JC_Doctor4", SqlDbType.VarChar,20),
					new SqlParameter("@Gzlyj", SqlDbType.NVarChar,50),
					new SqlParameter("@Zlcs", SqlDbType.NVarChar,50),
					new SqlParameter("@JC_Doctor5", SqlDbType.VarChar,20),
					new SqlParameter("@Zlxg", SqlDbType.NVarChar,50),
					new SqlParameter("@JC_Doctor6", SqlDbType.VarChar,20),
					new SqlParameter("@CheckDate", SqlDbType.SmallDateTime),
					new SqlParameter("@CheckJG", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = UnvID;
            parameters[1].Value = QcfwzBm;
            parameters[2].Value = FnName;
            parameters[3].Value = FnCID;
            parameters[4].Value = FnAge;
            parameters[5].Value = JHAge;
            parameters[6].Value = FnTel;
            parameters[7].Value = FnHyzk;
            parameters[8].Value = HomeAddress;
            parameters[9].Value = AreaCode;
            parameters[10].Value = MoCiYj;
            parameters[11].Value = Bycs;
            parameters[12].Value = BJAge;
            parameters[13].Value = Yun;
            parameters[14].Value = Chan;
            parameters[15].Value = Sex1;
            parameters[16].Value = Sex0;
            parameters[17].Value = Zuyue;
            parameters[18].Value = Paofu;
            parameters[19].Value = Zaochan;
            parameters[20].Value = Chansi;
            parameters[21].Value = Sitai;
            parameters[22].Value = Yaoliu;
            parameters[23].Value = Renliu;
            parameters[24].Value = Ziliu;
            parameters[25].Value = Qita;
            parameters[26].Value = Tige_height;
            parameters[27].Value = Tige_weight;
            parameters[28].Value = Tige_XyMax;
            parameters[29].Value = Tige_XyMin;
            parameters[30].Value = Tige_MaiBo;
            parameters[31].Value = Ruxian_left;
            parameters[32].Value = Ruxian_left_qt;
            parameters[33].Value = Ruxian_right;
            parameters[34].Value = Ruxian_right_qt;
            parameters[35].Value = FKJC_wy;
            parameters[36].Value = FKJC_wy_zl;
            parameters[37].Value = FKJC_yd;
            parameters[38].Value = FKJC_gj;
            parameters[39].Value = FKJC_gj_ml;
            parameters[40].Value = FKJC_gj_zl;
            parameters[41].Value = FKJC_gj_yd;
            parameters[42].Value = FKJC_gj_zt;
            parameters[43].Value = FKJC_gj_qt;
            parameters[44].Value = FKJC_gt_big;
            parameters[45].Value = FKJC_gt_map;
            parameters[46].Value = FKJC_gt_hyd;
            parameters[47].Value = FKJC_gt_tc;
            parameters[48].Value = FKJC_gt_zl;
            parameters[49].Value = FKJC_gt_qt;
            parameters[50].Value = FKJC_fj_left;
            parameters[51].Value = FKJC_fj_left_qt;
            parameters[52].Value = FKJC_fj_right;
            parameters[53].Value = FKJC_fj_right_qt;
            parameters[54].Value = Penqiang_B;
            parameters[55].Value = JC_Doctor1;
            parameters[56].Value = FZJC_bdtp;
            parameters[57].Value = FZJC_gjgp;
            parameters[58].Value = JC_Doctor2;
            parameters[59].Value = DZYDJ;
            parameters[60].Value = JC_Doctor3;
            parameters[61].Value = CBZD_RX_E1;
            parameters[62].Value = CBZD_RX_E2;
            parameters[63].Value = CBZD_RX_E3;
            parameters[64].Value = CBZD_FK_E4;
            parameters[65].Value = CBZD_FK_E5;
            parameters[66].Value = CBZD_FK_E6;
            parameters[67].Value = CBZD_FK_E7;
            parameters[68].Value = CBZD_FK_E8;
            parameters[69].Value = CBZD_FK_E9;
            parameters[70].Value = CBZD_FK_E10;
            parameters[71].Value = CBZD_FK_E11;
            parameters[72].Value = CBZD_FK_E12;
            parameters[73].Value = CBZD_FK_E13;
            parameters[74].Value = CBZD_FK_E14;
            parameters[75].Value = CBZD_FK_E15;
            parameters[76].Value = CBZD_FK_E16;
            parameters[77].Value = CBZD_jielun;
            parameters[78].Value = JC_Doctor4;
            parameters[79].Value = Gzlyj;
            parameters[80].Value = Zlcs;
            parameters[81].Value = JC_Doctor5;
            parameters[82].Value = Zlxg;
            parameters[83].Value = JC_Doctor6;
            parameters[84].Value = CheckDate;
            parameters[85].Value = CheckJG;
            parameters[86].Value = CreateDate;
            parameters[87].Value = UserID;

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
            strSql.Append("update [NHS_YlfnJkjc] set ");
            strSql.Append("UnvID=@UnvID,");
            strSql.Append("QcfwzBm=@QcfwzBm,");
            strSql.Append("FnName=@FnName,");
            strSql.Append("FnCID=@FnCID,");
            strSql.Append("FnAge=@FnAge,");
            strSql.Append("JHAge=@JHAge,");
            strSql.Append("FnTel=@FnTel,");
            strSql.Append("FnHyzk=@FnHyzk,");
            strSql.Append("HomeAddress=@HomeAddress,");
            strSql.Append("AreaCode=@AreaCode,");
            strSql.Append("MoCiYj=@MoCiYj,");
            strSql.Append("Bycs=@Bycs,");
            strSql.Append("BJAge=@BJAge,");
            strSql.Append("Yun=@Yun,");
            strSql.Append("Chan=@Chan,");
            strSql.Append("Sex1=@Sex1,");
            strSql.Append("Sex0=@Sex0,");
            strSql.Append("Zuyue=@Zuyue,");
            strSql.Append("Paofu=@Paofu,");
            strSql.Append("Zaochan=@Zaochan,");
            strSql.Append("Chansi=@Chansi,");
            strSql.Append("Sitai=@Sitai,");
            strSql.Append("Yaoliu=@Yaoliu,");
            strSql.Append("Renliu=@Renliu,");
            strSql.Append("Ziliu=@Ziliu,");
            strSql.Append("Qita=@Qita,");
            strSql.Append("Tige_height=@Tige_height,");
            strSql.Append("Tige_weight=@Tige_weight,");
            strSql.Append("Tige_XyMax=@Tige_XyMax,");
            strSql.Append("Tige_XyMin=@Tige_XyMin,");
            strSql.Append("Tige_MaiBo=@Tige_MaiBo,");
            strSql.Append("Ruxian_left=@Ruxian_left,");
            strSql.Append("Ruxian_left_qt=@Ruxian_left_qt,");
            strSql.Append("Ruxian_right=@Ruxian_right,");
            strSql.Append("Ruxian_right_qt=@Ruxian_right_qt,");
            strSql.Append("FKJC_wy=@FKJC_wy,");
            strSql.Append("FKJC_wy_zl=@FKJC_wy_zl,");
            strSql.Append("FKJC_yd=@FKJC_yd,");
            strSql.Append("FKJC_gj=@FKJC_gj,");
            strSql.Append("FKJC_gj_ml=@FKJC_gj_ml,");
            strSql.Append("FKJC_gj_zl=@FKJC_gj_zl,");
            strSql.Append("FKJC_gj_yd=@FKJC_gj_yd,");
            strSql.Append("FKJC_gj_zt=@FKJC_gj_zt,");
            strSql.Append("FKJC_gj_qt=@FKJC_gj_qt,");
            strSql.Append("FKJC_gt_big=@FKJC_gt_big,");
            strSql.Append("FKJC_gt_map=@FKJC_gt_map,");
            strSql.Append("FKJC_gt_hyd=@FKJC_gt_hyd,");
            strSql.Append("FKJC_gt_tc=@FKJC_gt_tc,");
            strSql.Append("FKJC_gt_zl=@FKJC_gt_zl,");
            strSql.Append("FKJC_gt_qt=@FKJC_gt_qt,");
            strSql.Append("FKJC_fj_left=@FKJC_fj_left,");
            strSql.Append("FKJC_fj_left_qt=@FKJC_fj_left_qt,");
            strSql.Append("FKJC_fj_right=@FKJC_fj_right,");
            strSql.Append("FKJC_fj_right_qt=@FKJC_fj_right_qt,");
            strSql.Append("Penqiang_B=@Penqiang_B,");
            strSql.Append("JC_Doctor1=@JC_Doctor1,");
            strSql.Append("FZJC_bdtp=@FZJC_bdtp,");
            strSql.Append("FZJC_gjgp=@FZJC_gjgp,");
            strSql.Append("JC_Doctor2=@JC_Doctor2,");
            strSql.Append("DZYDJ=@DZYDJ,");
            strSql.Append("JC_Doctor3=@JC_Doctor3,");
            strSql.Append("CBZD_RX_E1=@CBZD_RX_E1,");
            strSql.Append("CBZD_RX_E2=@CBZD_RX_E2,");
            strSql.Append("CBZD_RX_E3=@CBZD_RX_E3,");
            strSql.Append("CBZD_FK_E4=@CBZD_FK_E4,");
            strSql.Append("CBZD_FK_E5=@CBZD_FK_E5,");
            strSql.Append("CBZD_FK_E6=@CBZD_FK_E6,");
            strSql.Append("CBZD_FK_E7=@CBZD_FK_E7,");
            strSql.Append("CBZD_FK_E8=@CBZD_FK_E8,");
            strSql.Append("CBZD_FK_E9=@CBZD_FK_E9,");
            strSql.Append("CBZD_FK_E10=@CBZD_FK_E10,");
            strSql.Append("CBZD_FK_E11=@CBZD_FK_E11,");
            strSql.Append("CBZD_FK_E12=@CBZD_FK_E12,");
            strSql.Append("CBZD_FK_E13=@CBZD_FK_E13,");
            strSql.Append("CBZD_FK_E14=@CBZD_FK_E14,");
            strSql.Append("CBZD_FK_E15=@CBZD_FK_E15,");
            strSql.Append("CBZD_FK_E16=@CBZD_FK_E16,");
            strSql.Append("CBZD_jielun=@CBZD_jielun,");
            strSql.Append("JC_Doctor4=@JC_Doctor4,");
            strSql.Append("Gzlyj=@Gzlyj,");
            strSql.Append("Zlcs=@Zlcs,");
            strSql.Append("JC_Doctor5=@JC_Doctor5,");
            strSql.Append("Zlxg=@Zlxg,");
            strSql.Append("JC_Doctor6=@JC_Doctor6,");
            strSql.Append("CheckDate=@CheckDate,");
            strSql.Append("CheckJG=@CheckJG,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UserID=@UserID");
            strSql.Append(" where CommID=@CommID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,20),
					new SqlParameter("@QcfwzBm", SqlDbType.VarChar,50),
					new SqlParameter("@FnName", SqlDbType.NVarChar,20),
					new SqlParameter("@FnCID", SqlDbType.VarChar,20),
					new SqlParameter("@FnAge", SqlDbType.TinyInt,1),
					new SqlParameter("@JHAge", SqlDbType.TinyInt,1),
					new SqlParameter("@FnTel", SqlDbType.VarChar,50),
					new SqlParameter("@FnHyzk", SqlDbType.TinyInt,1),
					new SqlParameter("@HomeAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaCode", SqlDbType.VarChar,50),
					new SqlParameter("@MoCiYj", SqlDbType.SmallDateTime),
					new SqlParameter("@Bycs", SqlDbType.TinyInt,1),
					new SqlParameter("@BJAge", SqlDbType.TinyInt,1),
					new SqlParameter("@Yun", SqlDbType.TinyInt,1),
					new SqlParameter("@Chan", SqlDbType.TinyInt,1),
					new SqlParameter("@Sex1", SqlDbType.TinyInt,1),
					new SqlParameter("@Sex0", SqlDbType.TinyInt,1),
					new SqlParameter("@Zuyue", SqlDbType.TinyInt,1),
					new SqlParameter("@Paofu", SqlDbType.TinyInt,1),
					new SqlParameter("@Zaochan", SqlDbType.TinyInt,1),
					new SqlParameter("@Chansi", SqlDbType.TinyInt,1),
					new SqlParameter("@Sitai", SqlDbType.TinyInt,1),
					new SqlParameter("@Yaoliu", SqlDbType.TinyInt,1),
					new SqlParameter("@Renliu", SqlDbType.TinyInt,1),
					new SqlParameter("@Ziliu", SqlDbType.TinyInt,1),
					new SqlParameter("@Qita", SqlDbType.TinyInt,1),
					new SqlParameter("@Tige_height", SqlDbType.Float,8),
					new SqlParameter("@Tige_weight", SqlDbType.Float,8),
					new SqlParameter("@Tige_XyMax", SqlDbType.Float,8),
					new SqlParameter("@Tige_XyMin", SqlDbType.Float,8),
					new SqlParameter("@Tige_MaiBo", SqlDbType.TinyInt,1),
					new SqlParameter("@Ruxian_left", SqlDbType.Bit,1),
					new SqlParameter("@Ruxian_left_qt", SqlDbType.VarChar,50),
					new SqlParameter("@Ruxian_right", SqlDbType.Bit,1),
					new SqlParameter("@Ruxian_right_qt", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_wy", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_wy_zl", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_yd", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_gj", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_gj_ml", SqlDbType.TinyInt,1),
					new SqlParameter("@FKJC_gj_zl", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_gj_yd", SqlDbType.Float,8),
					new SqlParameter("@FKJC_gj_zt", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_gj_qt", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_gt_big", SqlDbType.Float,8),
					new SqlParameter("@FKJC_gt_map", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_gt_hyd", SqlDbType.TinyInt,1),
					new SqlParameter("@FKJC_gt_tc", SqlDbType.TinyInt,1),
					new SqlParameter("@FKJC_gt_zl", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_gt_qt", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_fj_left", SqlDbType.Bit,1),
					new SqlParameter("@FKJC_fj_left_qt", SqlDbType.VarChar,50),
					new SqlParameter("@FKJC_fj_right", SqlDbType.Bit,1),
					new SqlParameter("@FKJC_fj_right_qt", SqlDbType.VarChar,50),
					new SqlParameter("@Penqiang_B", SqlDbType.NVarChar,50),
					new SqlParameter("@JC_Doctor1", SqlDbType.VarChar,20),
					new SqlParameter("@FZJC_bdtp", SqlDbType.NVarChar,50),
					new SqlParameter("@FZJC_gjgp", SqlDbType.NVarChar,50),
					new SqlParameter("@JC_Doctor2", SqlDbType.VarChar,20),
					new SqlParameter("@DZYDJ", SqlDbType.NVarChar,50),
					new SqlParameter("@JC_Doctor3", SqlDbType.VarChar,20),
					new SqlParameter("@CBZD_RX_E1", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_RX_E2", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_RX_E3", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E4", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E5", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E6", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E7", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E8", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E9", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E10", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E11", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E12", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E13", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E14", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E15", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_FK_E16", SqlDbType.TinyInt,1),
					new SqlParameter("@CBZD_jielun", SqlDbType.NVarChar,50),
					new SqlParameter("@JC_Doctor4", SqlDbType.VarChar,20),
					new SqlParameter("@Gzlyj", SqlDbType.NVarChar,50),
					new SqlParameter("@Zlcs", SqlDbType.NVarChar,50),
					new SqlParameter("@JC_Doctor5", SqlDbType.VarChar,20),
					new SqlParameter("@Zlxg", SqlDbType.NVarChar,50),
					new SqlParameter("@JC_Doctor6", SqlDbType.VarChar,20),
					new SqlParameter("@CheckDate", SqlDbType.SmallDateTime),
					new SqlParameter("@CheckJG", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@CommID", SqlDbType.Int,4)};
            parameters[0].Value = UnvID;
            parameters[1].Value = QcfwzBm;
            parameters[2].Value = FnName;
            parameters[3].Value = FnCID;
            parameters[4].Value = FnAge;
            parameters[5].Value = JHAge;
            parameters[6].Value = FnTel;
            parameters[7].Value = FnHyzk;
            parameters[8].Value = HomeAddress;
            parameters[9].Value = AreaCode;
            parameters[10].Value = MoCiYj;
            parameters[11].Value = Bycs;
            parameters[12].Value = BJAge;
            parameters[13].Value = Yun;
            parameters[14].Value = Chan;
            parameters[15].Value = Sex1;
            parameters[16].Value = Sex0;
            parameters[17].Value = Zuyue;
            parameters[18].Value = Paofu;
            parameters[19].Value = Zaochan;
            parameters[20].Value = Chansi;
            parameters[21].Value = Sitai;
            parameters[22].Value = Yaoliu;
            parameters[23].Value = Renliu;
            parameters[24].Value = Ziliu;
            parameters[25].Value = Qita;
            parameters[26].Value = Tige_height;
            parameters[27].Value = Tige_weight;
            parameters[28].Value = Tige_XyMax;
            parameters[29].Value = Tige_XyMin;
            parameters[30].Value = Tige_MaiBo;
            parameters[31].Value = Ruxian_left;
            parameters[32].Value = Ruxian_left_qt;
            parameters[33].Value = Ruxian_right;
            parameters[34].Value = Ruxian_right_qt;
            parameters[35].Value = FKJC_wy;
            parameters[36].Value = FKJC_wy_zl;
            parameters[37].Value = FKJC_yd;
            parameters[38].Value = FKJC_gj;
            parameters[39].Value = FKJC_gj_ml;
            parameters[40].Value = FKJC_gj_zl;
            parameters[41].Value = FKJC_gj_yd;
            parameters[42].Value = FKJC_gj_zt;
            parameters[43].Value = FKJC_gj_qt;
            parameters[44].Value = FKJC_gt_big;
            parameters[45].Value = FKJC_gt_map;
            parameters[46].Value = FKJC_gt_hyd;
            parameters[47].Value = FKJC_gt_tc;
            parameters[48].Value = FKJC_gt_zl;
            parameters[49].Value = FKJC_gt_qt;
            parameters[50].Value = FKJC_fj_left;
            parameters[51].Value = FKJC_fj_left_qt;
            parameters[52].Value = FKJC_fj_right;
            parameters[53].Value = FKJC_fj_right_qt;
            parameters[54].Value = Penqiang_B;
            parameters[55].Value = JC_Doctor1;
            parameters[56].Value = FZJC_bdtp;
            parameters[57].Value = FZJC_gjgp;
            parameters[58].Value = JC_Doctor2;
            parameters[59].Value = DZYDJ;
            parameters[60].Value = JC_Doctor3;
            parameters[61].Value = CBZD_RX_E1;
            parameters[62].Value = CBZD_RX_E2;
            parameters[63].Value = CBZD_RX_E3;
            parameters[64].Value = CBZD_FK_E4;
            parameters[65].Value = CBZD_FK_E5;
            parameters[66].Value = CBZD_FK_E6;
            parameters[67].Value = CBZD_FK_E7;
            parameters[68].Value = CBZD_FK_E8;
            parameters[69].Value = CBZD_FK_E9;
            parameters[70].Value = CBZD_FK_E10;
            parameters[71].Value = CBZD_FK_E11;
            parameters[72].Value = CBZD_FK_E12;
            parameters[73].Value = CBZD_FK_E13;
            parameters[74].Value = CBZD_FK_E14;
            parameters[75].Value = CBZD_FK_E15;
            parameters[76].Value = CBZD_FK_E16;
            parameters[77].Value = CBZD_jielun;
            parameters[78].Value = JC_Doctor4;
            parameters[79].Value = Gzlyj;
            parameters[80].Value = Zlcs;
            parameters[81].Value = JC_Doctor5;
            parameters[82].Value = Zlxg;
            parameters[83].Value = JC_Doctor6;
            parameters[84].Value = CheckDate;
            parameters[85].Value = CheckJG;
            parameters[86].Value = CreateDate;
            parameters[87].Value = UserID;
            parameters[88].Value = CommID;

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
            strSql.Append("delete from [NHS_YlfnJkjc] ");
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
            strSql.Append("select CommID,UnvID,QcfwzBm,FnName,FnCID,FnAge,JHAge,FnTel,FnHyzk,HomeAddress,AreaCode,MoCiYj,Bycs,BJAge,Yun,Chan,Sex1,Sex0,Zuyue,Paofu,Zaochan,Chansi,Sitai,Yaoliu,Renliu,Ziliu,Qita,Tige_height,Tige_weight,Tige_XyMax,Tige_XyMin,Tige_MaiBo,Ruxian_left,Ruxian_left_qt,Ruxian_right,Ruxian_right_qt,FKJC_wy,FKJC_wy_zl,FKJC_yd,FKJC_gj,FKJC_gj_ml,FKJC_gj_zl,FKJC_gj_yd,FKJC_gj_zt,FKJC_gj_qt,FKJC_gt_big,FKJC_gt_map,FKJC_gt_hyd,FKJC_gt_tc,FKJC_gt_zl,FKJC_gt_qt,FKJC_fj_left,FKJC_fj_left_qt,FKJC_fj_right,FKJC_fj_right_qt,Penqiang_B,JC_Doctor1,FZJC_bdtp,FZJC_gjgp,JC_Doctor2,DZYDJ,JC_Doctor3,CBZD_RX_E1,CBZD_RX_E2,CBZD_RX_E3,CBZD_FK_E4,CBZD_FK_E5,CBZD_FK_E6,CBZD_FK_E7,CBZD_FK_E8,CBZD_FK_E9,CBZD_FK_E10,CBZD_FK_E11,CBZD_FK_E12,CBZD_FK_E13,CBZD_FK_E14,CBZD_FK_E15,CBZD_FK_E16,CBZD_jielun,JC_Doctor4,Gzlyj,Zlcs,JC_Doctor5,Zlxg,JC_Doctor6,CheckDate,CheckJG,CreateDate,UserID ");
            strSql.Append(" FROM [NHS_YlfnJkjc] ");
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
                if (ds.Tables[0].Rows[0]["FnAge"] != null && ds.Tables[0].Rows[0]["FnAge"].ToString() != "")
                {
                    this.FnAge = int.Parse(ds.Tables[0].Rows[0]["FnAge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JHAge"] != null && ds.Tables[0].Rows[0]["JHAge"].ToString() != "")
                {
                    this.JHAge = int.Parse(ds.Tables[0].Rows[0]["JHAge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FnTel"] != null)
                {
                    this.FnTel = ds.Tables[0].Rows[0]["FnTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnHyzk"] != null && ds.Tables[0].Rows[0]["FnHyzk"].ToString() != "")
                {
                    this.FnHyzk = int.Parse(ds.Tables[0].Rows[0]["FnHyzk"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HomeAddress"] != null)
                {
                    this.HomeAddress = ds.Tables[0].Rows[0]["HomeAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AreaCode"] != null)
                {
                    this.AreaCode = ds.Tables[0].Rows[0]["AreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MoCiYj"] != null && ds.Tables[0].Rows[0]["MoCiYj"].ToString() != "")
                {
                    this.MoCiYj = DateTime.Parse(ds.Tables[0].Rows[0]["MoCiYj"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Bycs"] != null && ds.Tables[0].Rows[0]["Bycs"].ToString() != "")
                {
                    this.Bycs = int.Parse(ds.Tables[0].Rows[0]["Bycs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BJAge"] != null && ds.Tables[0].Rows[0]["BJAge"].ToString() != "")
                {
                    this.BJAge = int.Parse(ds.Tables[0].Rows[0]["BJAge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Yun"] != null && ds.Tables[0].Rows[0]["Yun"].ToString() != "")
                {
                    this.Yun = int.Parse(ds.Tables[0].Rows[0]["Yun"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Chan"] != null && ds.Tables[0].Rows[0]["Chan"].ToString() != "")
                {
                    this.Chan = int.Parse(ds.Tables[0].Rows[0]["Chan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sex1"] != null && ds.Tables[0].Rows[0]["Sex1"].ToString() != "")
                {
                    this.Sex1 = int.Parse(ds.Tables[0].Rows[0]["Sex1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sex0"] != null && ds.Tables[0].Rows[0]["Sex0"].ToString() != "")
                {
                    this.Sex0 = int.Parse(ds.Tables[0].Rows[0]["Sex0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Zuyue"] != null && ds.Tables[0].Rows[0]["Zuyue"].ToString() != "")
                {
                    this.Zuyue = int.Parse(ds.Tables[0].Rows[0]["Zuyue"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Paofu"] != null && ds.Tables[0].Rows[0]["Paofu"].ToString() != "")
                {
                    this.Paofu = int.Parse(ds.Tables[0].Rows[0]["Paofu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Zaochan"] != null && ds.Tables[0].Rows[0]["Zaochan"].ToString() != "")
                {
                    this.Zaochan = int.Parse(ds.Tables[0].Rows[0]["Zaochan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Chansi"] != null && ds.Tables[0].Rows[0]["Chansi"].ToString() != "")
                {
                    this.Chansi = int.Parse(ds.Tables[0].Rows[0]["Chansi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sitai"] != null && ds.Tables[0].Rows[0]["Sitai"].ToString() != "")
                {
                    this.Sitai = int.Parse(ds.Tables[0].Rows[0]["Sitai"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Yaoliu"] != null && ds.Tables[0].Rows[0]["Yaoliu"].ToString() != "")
                {
                    this.Yaoliu = int.Parse(ds.Tables[0].Rows[0]["Yaoliu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Renliu"] != null && ds.Tables[0].Rows[0]["Renliu"].ToString() != "")
                {
                    this.Renliu = int.Parse(ds.Tables[0].Rows[0]["Renliu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ziliu"] != null && ds.Tables[0].Rows[0]["Ziliu"].ToString() != "")
                {
                    this.Ziliu = int.Parse(ds.Tables[0].Rows[0]["Ziliu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Qita"] != null && ds.Tables[0].Rows[0]["Qita"].ToString() != "")
                {
                    this.Qita = int.Parse(ds.Tables[0].Rows[0]["Qita"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Tige_height"] != null && ds.Tables[0].Rows[0]["Tige_height"].ToString() != "")
                {
                    this.Tige_height = decimal.Parse(ds.Tables[0].Rows[0]["Tige_height"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Tige_weight"] != null && ds.Tables[0].Rows[0]["Tige_weight"].ToString() != "")
                {
                    this.Tige_weight = decimal.Parse(ds.Tables[0].Rows[0]["Tige_weight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Tige_XyMax"] != null && ds.Tables[0].Rows[0]["Tige_XyMax"].ToString() != "")
                {
                    this.Tige_XyMax = decimal.Parse(ds.Tables[0].Rows[0]["Tige_XyMax"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Tige_XyMin"] != null && ds.Tables[0].Rows[0]["Tige_XyMin"].ToString() != "")
                {
                    this.Tige_XyMin = decimal.Parse(ds.Tables[0].Rows[0]["Tige_XyMin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Tige_MaiBo"] != null && ds.Tables[0].Rows[0]["Tige_MaiBo"].ToString() != "")
                {
                    this.Tige_MaiBo = int.Parse(ds.Tables[0].Rows[0]["Tige_MaiBo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ruxian_left"] != null && ds.Tables[0].Rows[0]["Ruxian_left"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Ruxian_left"].ToString() == "1") || (ds.Tables[0].Rows[0]["Ruxian_left"].ToString().ToLower() == "true"))
                    {
                        this.Ruxian_left = true;
                    }
                    else
                    {
                        this.Ruxian_left = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Ruxian_left_qt"] != null)
                {
                    this.Ruxian_left_qt = ds.Tables[0].Rows[0]["Ruxian_left_qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Ruxian_right"] != null && ds.Tables[0].Rows[0]["Ruxian_right"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Ruxian_right"].ToString() == "1") || (ds.Tables[0].Rows[0]["Ruxian_right"].ToString().ToLower() == "true"))
                    {
                        this.Ruxian_right = true;
                    }
                    else
                    {
                        this.Ruxian_right = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Ruxian_right_qt"] != null)
                {
                    this.Ruxian_right_qt = ds.Tables[0].Rows[0]["Ruxian_right_qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_wy"] != null)
                {
                    this.FKJC_wy = ds.Tables[0].Rows[0]["FKJC_wy"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_wy_zl"] != null)
                {
                    this.FKJC_wy_zl = ds.Tables[0].Rows[0]["FKJC_wy_zl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_yd"] != null)
                {
                    this.FKJC_yd = ds.Tables[0].Rows[0]["FKJC_yd"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_gj"] != null)
                {
                    this.FKJC_gj = ds.Tables[0].Rows[0]["FKJC_gj"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_gj_ml"] != null && ds.Tables[0].Rows[0]["FKJC_gj_ml"].ToString() != "")
                {
                    this.FKJC_gj_ml = int.Parse(ds.Tables[0].Rows[0]["FKJC_gj_ml"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FKJC_gj_zl"] != null)
                {
                    this.FKJC_gj_zl = ds.Tables[0].Rows[0]["FKJC_gj_zl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_gj_yd"] != null && ds.Tables[0].Rows[0]["FKJC_gj_yd"].ToString() != "")
                {
                    this.FKJC_gj_yd = decimal.Parse(ds.Tables[0].Rows[0]["FKJC_gj_yd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FKJC_gj_zt"] != null)
                {
                    this.FKJC_gj_zt = ds.Tables[0].Rows[0]["FKJC_gj_zt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_gj_qt"] != null)
                {
                    this.FKJC_gj_qt = ds.Tables[0].Rows[0]["FKJC_gj_qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_gt_big"] != null && ds.Tables[0].Rows[0]["FKJC_gt_big"].ToString() != "")
                {
                    this.FKJC_gt_big = decimal.Parse(ds.Tables[0].Rows[0]["FKJC_gt_big"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FKJC_gt_map"] != null)
                {
                    this.FKJC_gt_map = ds.Tables[0].Rows[0]["FKJC_gt_map"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_gt_hyd"] != null && ds.Tables[0].Rows[0]["FKJC_gt_hyd"].ToString() != "")
                {
                    this.FKJC_gt_hyd = int.Parse(ds.Tables[0].Rows[0]["FKJC_gt_hyd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FKJC_gt_tc"] != null && ds.Tables[0].Rows[0]["FKJC_gt_tc"].ToString() != "")
                {
                    this.FKJC_gt_tc = int.Parse(ds.Tables[0].Rows[0]["FKJC_gt_tc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FKJC_gt_zl"] != null)
                {
                    this.FKJC_gt_zl = ds.Tables[0].Rows[0]["FKJC_gt_zl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_gt_qt"] != null)
                {
                    this.FKJC_gt_qt = ds.Tables[0].Rows[0]["FKJC_gt_qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_fj_left"] != null && ds.Tables[0].Rows[0]["FKJC_fj_left"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["FKJC_fj_left"].ToString() == "1") || (ds.Tables[0].Rows[0]["FKJC_fj_left"].ToString().ToLower() == "true"))
                    {
                        this.FKJC_fj_left = true;
                    }
                    else
                    {
                        this.FKJC_fj_left = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["FKJC_fj_left_qt"] != null)
                {
                    this.FKJC_fj_left_qt = ds.Tables[0].Rows[0]["FKJC_fj_left_qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FKJC_fj_right"] != null && ds.Tables[0].Rows[0]["FKJC_fj_right"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["FKJC_fj_right"].ToString() == "1") || (ds.Tables[0].Rows[0]["FKJC_fj_right"].ToString().ToLower() == "true"))
                    {
                        this.FKJC_fj_right = true;
                    }
                    else
                    {
                        this.FKJC_fj_right = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["FKJC_fj_right_qt"] != null)
                {
                    this.FKJC_fj_right_qt = ds.Tables[0].Rows[0]["FKJC_fj_right_qt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Penqiang_B"] != null)
                {
                    this.Penqiang_B = ds.Tables[0].Rows[0]["Penqiang_B"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JC_Doctor1"] != null)
                {
                    this.JC_Doctor1 = ds.Tables[0].Rows[0]["JC_Doctor1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FZJC_bdtp"] != null)
                {
                    this.FZJC_bdtp = ds.Tables[0].Rows[0]["FZJC_bdtp"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FZJC_gjgp"] != null)
                {
                    this.FZJC_gjgp = ds.Tables[0].Rows[0]["FZJC_gjgp"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JC_Doctor2"] != null)
                {
                    this.JC_Doctor2 = ds.Tables[0].Rows[0]["JC_Doctor2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DZYDJ"] != null)
                {
                    this.DZYDJ = ds.Tables[0].Rows[0]["DZYDJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JC_Doctor3"] != null)
                {
                    this.JC_Doctor3 = ds.Tables[0].Rows[0]["JC_Doctor3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CBZD_RX_E1"] != null && ds.Tables[0].Rows[0]["CBZD_RX_E1"].ToString() != "")
                {
                    this.CBZD_RX_E1 = int.Parse(ds.Tables[0].Rows[0]["CBZD_RX_E1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_RX_E2"] != null && ds.Tables[0].Rows[0]["CBZD_RX_E2"].ToString() != "")
                {
                    this.CBZD_RX_E2 = int.Parse(ds.Tables[0].Rows[0]["CBZD_RX_E2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_RX_E3"] != null && ds.Tables[0].Rows[0]["CBZD_RX_E3"].ToString() != "")
                {
                    this.CBZD_RX_E3 = int.Parse(ds.Tables[0].Rows[0]["CBZD_RX_E3"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E4"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E4"].ToString() != "")
                {
                    this.CBZD_FK_E4 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E4"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E5"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E5"].ToString() != "")
                {
                    this.CBZD_FK_E5 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E5"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E6"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E6"].ToString() != "")
                {
                    this.CBZD_FK_E6 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E6"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E7"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E7"].ToString() != "")
                {
                    this.CBZD_FK_E7 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E7"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E8"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E8"].ToString() != "")
                {
                    this.CBZD_FK_E8 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E8"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E9"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E9"].ToString() != "")
                {
                    this.CBZD_FK_E9 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E9"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E10"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E10"].ToString() != "")
                {
                    this.CBZD_FK_E10 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E10"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E11"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E11"].ToString() != "")
                {
                    this.CBZD_FK_E11 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E11"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E12"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E12"].ToString() != "")
                {
                    this.CBZD_FK_E12 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E12"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E13"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E13"].ToString() != "")
                {
                    this.CBZD_FK_E13 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E13"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E14"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E14"].ToString() != "")
                {
                    this.CBZD_FK_E14 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E14"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E15"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E15"].ToString() != "")
                {
                    this.CBZD_FK_E15 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E15"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_FK_E16"] != null && ds.Tables[0].Rows[0]["CBZD_FK_E16"].ToString() != "")
                {
                    this.CBZD_FK_E16 = int.Parse(ds.Tables[0].Rows[0]["CBZD_FK_E16"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBZD_jielun"] != null)
                {
                    this.CBZD_jielun = ds.Tables[0].Rows[0]["CBZD_jielun"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JC_Doctor4"] != null)
                {
                    this.JC_Doctor4 = ds.Tables[0].Rows[0]["JC_Doctor4"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Gzlyj"] != null)
                {
                    this.Gzlyj = ds.Tables[0].Rows[0]["Gzlyj"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zlcs"] != null)
                {
                    this.Zlcs = ds.Tables[0].Rows[0]["Zlcs"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JC_Doctor5"] != null)
                {
                    this.JC_Doctor5 = ds.Tables[0].Rows[0]["JC_Doctor5"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zlxg"] != null)
                {
                    this.Zlxg = ds.Tables[0].Rows[0]["Zlxg"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JC_Doctor6"] != null)
                {
                    this.JC_Doctor6 = ds.Tables[0].Rows[0]["JC_Doctor6"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CheckDate"] != null && ds.Tables[0].Rows[0]["CheckDate"].ToString() != "")
                {
                    this.CheckDate = DateTime.Parse(ds.Tables[0].Rows[0]["CheckDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CheckJG"] != null)
                {
                    this.CheckJG = ds.Tables[0].Rows[0]["CheckJG"].ToString();
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
            strSql.Append(" FROM [NHS_YlfnJkjc] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
