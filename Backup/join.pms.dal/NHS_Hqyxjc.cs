using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using UNV.Comm.DataBase;
using System.Data.SqlClient;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_Hqyxjc。
    /// </summary>
    [Serializable]
	public partial class NHS_Hqyxjc
	{
		public NHS_Hqyxjc()
		{}
		#region Model
		private int _commid;
		private string _visitbh;
		private string _unvid;
		private string _visitsex;
		private string _qcfwzbm;
		private string _nameb;
		private string _cidb;
		private DateTime? _birthdayb;
		private string _zhiyeb;
		private string _wenhuab;
		private string _minzub;
		private string _registerareacodeb;
		private string _registerareanameb;
		private string _currentareacodeb;
		private string _currentareanameb;
		private string _currentareayb;
		private string _workunitb;
		private string _telb;
		private string _namea;
		private string _cida;
		private DateTime? _birthdaya;
		private string _zhiyea;
		private string _wenhuaa;
		private string _minzua;
		private string _registerareacodea;
		private string _registerareanamea;
		private string _currentareacodea;
		private string _currentareanamea;
		private string _currentareaya;
		private string _workunita;
		private string _tela;
		private DateTime? _checkdate;
		private string _xueyuan;
		private string _xueyuan_other;
		private string _history_jw;
		private string _history_jw_other;
		private string _history_ss;
		private string _history_ss_other;
		private string _history_other;
		private string _history_now;
		private string _history_now_other;
		private int? _chuchaoage;
		private string _yuejingweek;
		private string _yuejingliang;
		private string _tongjing;
		private DateTime? _mociyuejing;
		private string _history_jwhy;
		private string _zuyuezaochanliuchan;
		private string _child_boynum;
		private string _child_girlnum;
		private string _history_jz;
		private string _history_jz_other;
		private string _huanzhegx;
		private string _familyhunpei;
		private string _doctor1;
		private string _xueya;
		private string _teshetitai;
		private string _teshetitai_other;
		private string _jingshenzhuangtai;
		private string _jingshenzhuangtai_other;
		private string _teshumianrong;
		private string _teshumianrong_other;
		private string _zhili;
		private string _pifumaofa;
		private string _pifumaofa_other;
		private string _wuguan;
		private string _wuguan_other;
		private string _jiazhuangxian;
		private string _jiazhuangxian_other;
		private string _xinlv1;
		private string _xinlv2;
		private string _zayin;
		private string _zayin_other;
		private string _fei;
		private string _fei_other;
		private string _gan;
		private string _gan_other;
		private string _sizhijizhu;
		private string _sizhijizhu_other;
		private string _qita1;
		private string _doctor2;
		private string _houjie;
		private string _yinmao;
		private string _rufang;
		private string _rufang_other;
		private string _yinjinying;
		private string _baopi;
		private string _gaowantijil;
		private string _gaowantijir;
		private string _gaowanmmj;
		private string _fuwanjiejiel;
		private string _fuwanjiejier;
		private string _jsjmqz;
		private string _jsjmqz_buwei;
		private string _jsjmqz_chengdu;
		private string _waiyin;
		private string _fenmiwu;
		private string _zigong;
		private string _fujian;
		private string _waiyin_by;
		private string _yindao_by;
		private string _gongjing_by;
		private string _zigong_by;
		private string _fujian_bu;
		private string _qita2;
		private string _doctor3;
		private string _visitjieguo;
		private string _yichang;
		private string _jibingzhenduan;
		private string _yixueyijian;
		private string _hunqianzixun;
		private string _zhidaojieguo;
		private string _zhuanzhengyiyuan;
		private DateTime? _zhuanzhengdate;
		private DateTime? _yuyuedate;
		private DateTime? _chujudate;
		private string _doctor4;
		private DateTime? _tianxiedate;
		private DateTime? _createdate;
		private int? _createuser;
		private DateTime? _lastupdatedate;
		private int? _lastupdateuser;
		private string _userareacode;
        private string _yinjinying_other;
		/// <summary>
		/// 
		/// </summary>
		public int commid
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string VisitBH
		{
			set{ _visitbh=value;}
			get{return _visitbh;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UnvID
		{
			set{ _unvid=value;}
			get{return _unvid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string VisitSEX
		{
			set{ _visitsex=value;}
			get{return _visitsex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string QcfwzBm
		{
			set{ _qcfwzbm=value;}
			get{return _qcfwzbm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NameB
		{
			set{ _nameb=value;}
			get{return _nameb;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CIDB
		{
			set{ _cidb=value;}
			get{return _cidb;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? BirthdayB
		{
			set{ _birthdayb=value;}
			get{return _birthdayb;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZhiYeB
		{
			set{ _zhiyeb=value;}
			get{return _zhiyeb;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WenhuaB
		{
			set{ _wenhuab=value;}
			get{return _wenhuab;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MinZuB
		{
			set{ _minzub=value;}
			get{return _minzub;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegisterAreaCodeB
		{
			set{ _registerareacodeb=value;}
			get{return _registerareacodeb;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegisterAreaNameB
		{
			set{ _registerareanameb=value;}
			get{return _registerareanameb;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CurrentAreaCodeB
		{
			set{ _currentareacodeb=value;}
			get{return _currentareacodeb;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CurrentAreaNameB
		{
			set{ _currentareanameb=value;}
			get{return _currentareanameb;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CurrentAreaYB
		{
			set{ _currentareayb=value;}
			get{return _currentareayb;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WorkUnitB
		{
			set{ _workunitb=value;}
			get{return _workunitb;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TelB
		{
			set{ _telb=value;}
			get{return _telb;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NameA
		{
			set{ _namea=value;}
			get{return _namea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CIDA
		{
			set{ _cida=value;}
			get{return _cida;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? BirthdayA
		{
			set{ _birthdaya=value;}
			get{return _birthdaya;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZhiYeA
		{
			set{ _zhiyea=value;}
			get{return _zhiyea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WenhuaA
		{
			set{ _wenhuaa=value;}
			get{return _wenhuaa;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MinZuA
		{
			set{ _minzua=value;}
			get{return _minzua;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegisterAreaCodeA
		{
			set{ _registerareacodea=value;}
			get{return _registerareacodea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegisterAreaNameA
		{
			set{ _registerareanamea=value;}
			get{return _registerareanamea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CurrentAreaCodeA
		{
			set{ _currentareacodea=value;}
			get{return _currentareacodea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CurrentAreaNameA
		{
			set{ _currentareanamea=value;}
			get{return _currentareanamea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CurrentAreaYA
		{
			set{ _currentareaya=value;}
			get{return _currentareaya;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WorkUnitA
		{
			set{ _workunita=value;}
			get{return _workunita;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TelA
		{
			set{ _tela=value;}
			get{return _tela;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CheckDate
		{
			set{ _checkdate=value;}
			get{return _checkdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string XueYuan
		{
			set{ _xueyuan=value;}
			get{return _xueyuan;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string XueYuan_other
		{
			set{ _xueyuan_other=value;}
			get{return _xueyuan_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string History_Jw
		{
			set{ _history_jw=value;}
			get{return _history_jw;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string History_Jw_other
		{
			set{ _history_jw_other=value;}
			get{return _history_jw_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string History_ss
		{
			set{ _history_ss=value;}
			get{return _history_ss;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string History_ss_other
		{
			set{ _history_ss_other=value;}
			get{return _history_ss_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string History_other
		{
			set{ _history_other=value;}
			get{return _history_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string History_now
		{
			set{ _history_now=value;}
			get{return _history_now;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string History_now_other
		{
			set{ _history_now_other=value;}
			get{return _history_now_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ChuchaoAge
		{
			set{ _chuchaoage=value;}
			get{return _chuchaoage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YuejingWeek
		{
			set{ _yuejingweek=value;}
			get{return _yuejingweek;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Yuejingliang
		{
			set{ _yuejingliang=value;}
			get{return _yuejingliang;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TongJing
		{
			set{ _tongjing=value;}
			get{return _tongjing;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? MoCiYueJing
		{
			set{ _mociyuejing=value;}
			get{return _mociyuejing;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string History_Jwhy
		{
			set{ _history_jwhy=value;}
			get{return _history_jwhy;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZuYueZaoChanLiuChan
		{
			set{ _zuyuezaochanliuchan=value;}
			get{return _zuyuezaochanliuchan;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Child_boynum
		{
			set{ _child_boynum=value;}
			get{return _child_boynum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Child_girlnum
		{
			set{ _child_girlnum=value;}
			get{return _child_girlnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string History_Jz
		{
			set{ _history_jz=value;}
			get{return _history_jz;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string History_Jz_other
		{
			set{ _history_jz_other=value;}
			get{return _history_jz_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HuanzheGX
		{
			set{ _huanzhegx=value;}
			get{return _huanzhegx;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FamilyHunPei
		{
			set{ _familyhunpei=value;}
			get{return _familyhunpei;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Doctor1
		{
			set{ _doctor1=value;}
			get{return _doctor1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string XueYa
		{
			set{ _xueya=value;}
			get{return _xueya;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TeSheTiTai
		{
			set{ _teshetitai=value;}
			get{return _teshetitai;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TeSheTiTai_other
		{
			set{ _teshetitai_other=value;}
			get{return _teshetitai_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Jingshenzhuangtai
		{
			set{ _jingshenzhuangtai=value;}
			get{return _jingshenzhuangtai;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Jingshenzhuangtai_other
		{
			set{ _jingshenzhuangtai_other=value;}
			get{return _jingshenzhuangtai_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TeshuMianrong
		{
			set{ _teshumianrong=value;}
			get{return _teshumianrong;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TeshuMianrong_other
		{
			set{ _teshumianrong_other=value;}
			get{return _teshumianrong_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZhiLi
		{
			set{ _zhili=value;}
			get{return _zhili;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pifumaofa
		{
			set{ _pifumaofa=value;}
			get{return _pifumaofa;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pifumaofa_other
		{
			set{ _pifumaofa_other=value;}
			get{return _pifumaofa_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Wuguan
		{
			set{ _wuguan=value;}
			get{return _wuguan;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Wuguan_other
		{
			set{ _wuguan_other=value;}
			get{return _wuguan_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Jiazhuangxian
		{
			set{ _jiazhuangxian=value;}
			get{return _jiazhuangxian;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Jiazhuangxian_other
		{
			set{ _jiazhuangxian_other=value;}
			get{return _jiazhuangxian_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Xinlv1
		{
			set{ _xinlv1=value;}
			get{return _xinlv1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Xinlv2
		{
			set{ _xinlv2=value;}
			get{return _xinlv2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZaYin
		{
			set{ _zayin=value;}
			get{return _zayin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZaYin_other
		{
			set{ _zayin_other=value;}
			get{return _zayin_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Fei
		{
			set{ _fei=value;}
			get{return _fei;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Fei_other
		{
			set{ _fei_other=value;}
			get{return _fei_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Gan
		{
			set{ _gan=value;}
			get{return _gan;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Gan_other
		{
			set{ _gan_other=value;}
			get{return _gan_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SizhiJizhu
		{
			set{ _sizhijizhu=value;}
			get{return _sizhijizhu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SizhiJizhu_other
		{
			set{ _sizhijizhu_other=value;}
			get{return _sizhijizhu_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Qita1
		{
			set{ _qita1=value;}
			get{return _qita1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Doctor2
		{
			set{ _doctor2=value;}
			get{return _doctor2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Houjie
		{
			set{ _houjie=value;}
			get{return _houjie;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Yinmao
		{
			set{ _yinmao=value;}
			get{return _yinmao;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Rufang
		{
			set{ _rufang=value;}
			get{return _rufang;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Rufang_other
		{
			set{ _rufang_other=value;}
			get{return _rufang_other;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YinJinying
		{
			set{ _yinjinying=value;}
			get{return _yinjinying;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Baopi
		{
			set{ _baopi=value;}
			get{return _baopi;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GaoWanTIjiL
		{
			set{ _gaowantijil=value;}
			get{return _gaowantijil;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GaoWanTIjiR
		{
			set{ _gaowantijir=value;}
			get{return _gaowantijir;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GaoWanMMJ
		{
			set{ _gaowanmmj=value;}
			get{return _gaowanmmj;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FuWanJiejieL
		{
			set{ _fuwanjiejiel=value;}
			get{return _fuwanjiejiel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FuWanJiejieR
		{
			set{ _fuwanjiejier=value;}
			get{return _fuwanjiejier;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JSJMQZ
		{
			set{ _jsjmqz=value;}
			get{return _jsjmqz;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JSJMQZ_buwei
		{
			set{ _jsjmqz_buwei=value;}
			get{return _jsjmqz_buwei;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JSJMQZ_chengdu
		{
			set{ _jsjmqz_chengdu=value;}
			get{return _jsjmqz_chengdu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WaiYin
		{
			set{ _waiyin=value;}
			get{return _waiyin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FenMiWu
		{
			set{ _fenmiwu=value;}
			get{return _fenmiwu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZiGong
		{
			set{ _zigong=value;}
			get{return _zigong;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FuJian
		{
			set{ _fujian=value;}
			get{return _fujian;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WaiYin_by
		{
			set{ _waiyin_by=value;}
			get{return _waiyin_by;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YinDao_by
		{
			set{ _yindao_by=value;}
			get{return _yindao_by;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Gongjing_by
		{
			set{ _gongjing_by=value;}
			get{return _gongjing_by;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZiGong_by
		{
			set{ _zigong_by=value;}
			get{return _zigong_by;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FuJian_bu
		{
			set{ _fujian_bu=value;}
			get{return _fujian_bu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Qita2
		{
			set{ _qita2=value;}
			get{return _qita2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Doctor3
		{
			set{ _doctor3=value;}
			get{return _doctor3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string VisitJieGuo
		{
			set{ _visitjieguo=value;}
			get{return _visitjieguo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YiChang
		{
			set{ _yichang=value;}
			get{return _yichang;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JibingZhenduan
		{
			set{ _jibingzhenduan=value;}
			get{return _jibingzhenduan;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YiXueYiJian
		{
			set{ _yixueyijian=value;}
			get{return _yixueyijian;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HunQianZIXun
		{
			set{ _hunqianzixun=value;}
			get{return _hunqianzixun;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZhiDaoJieGuo
		{
			set{ _zhidaojieguo=value;}
			get{return _zhidaojieguo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZhuanZhengYiYuan
		{
			set{ _zhuanzhengyiyuan=value;}
			get{return _zhuanzhengyiyuan;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ZhuanZhengDate
		{
			set{ _zhuanzhengdate=value;}
			get{return _zhuanzhengdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? YuYueDate
		{
			set{ _yuyuedate=value;}
			get{return _yuyuedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ChuJuDate
		{
			set{ _chujudate=value;}
			get{return _chujudate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Doctor4
		{
			set{ _doctor4=value;}
			get{return _doctor4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? TianxieDate
		{
			set{ _tianxiedate=value;}
			get{return _tianxiedate;}
		}
		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 创建人
		/// </summary>
		public int? CreateUser
		{
			set{ _createuser=value;}
			get{return _createuser;}
		}
		/// <summary>
		/// 最后修改日期
		/// </summary>
		public DateTime? LastUpdateDate
		{
			set{ _lastupdatedate=value;}
			get{return _lastupdatedate;}
		}
		/// <summary>
		/// 最后修改人
		/// </summary>
		public int? LastUpdateUser
		{
			set{ _lastupdateuser=value;}
			get{return _lastupdateuser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserAreaCode
		{
			set{ _userareacode=value;}
			get{return _userareacode;}
		}
		/// <summary>
		/// 
		/// </summary>
        public string YinJinying_other
		{
            set { _yinjinying_other = value; }
            get { return _yinjinying_other; }
		}
		#endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NHS_Hqyxjc(string commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select commid,VisitBH,UnvID,VisitSEX,QcfwzBm,NameB,CIDB,BirthdayB,ZhiYeB,WenhuaB,MinZuB,RegisterAreaCodeB,RegisterAreaNameB,CurrentAreaCodeB,CurrentAreaNameB,CurrentAreaYB,WorkUnitB,TelB,NameA,CIDA,BirthdayA,ZhiYeA,WenhuaA,MinZuA,RegisterAreaCodeA,RegisterAreaNameA,CurrentAreaCodeA,CurrentAreaNameA,CurrentAreaYA,WorkUnitA,TelA,CheckDate,XueYuan,XueYuan_other,History_Jw,History_Jw_other,History_ss,History_ss_other,History_other,History_now,History_now_other,ChuchaoAge,YuejingWeek,Yuejingliang,TongJing,MoCiYueJing,History_Jwhy,ZuYueZaoChanLiuChan,Child_boynum,Child_girlnum,History_Jz,History_Jz_other,HuanzheGX,FamilyHunPei,Doctor1,XueYa,TeSheTiTai,TeSheTiTai_other,Jingshenzhuangtai,Jingshenzhuangtai_other,TeshuMianrong,TeshuMianrong_other,ZhiLi,Pifumaofa,Pifumaofa_other,Wuguan,Wuguan_other,Jiazhuangxian,Jiazhuangxian_other,Xinlv1,Xinlv2,ZaYin,ZaYin_other,Fei,Fei_other,Gan,Gan_other,SizhiJizhu,SizhiJizhu_other,Qita1,Doctor2,Houjie,Yinmao,Rufang,Rufang_other,YinJinying,Baopi,GaoWanTIjiL,GaoWanTIjiR,GaoWanMMJ,FuWanJiejieL,FuWanJiejieR,JSJMQZ,JSJMQZ_buwei,JSJMQZ_chengdu,WaiYin,FenMiWu,ZiGong,FuJian,WaiYin_by,YinDao_by,Gongjing_by,ZiGong_by,FuJian_bu,Qita2,Doctor3,VisitJieGuo,YiChang,JibingZhenduan,YiXueYiJian,HunQianZIXun,ZhiDaoJieGuo,ZhuanZhengYiYuan,ZhuanZhengDate,YuYueDate,ChuJuDate,Doctor4,TianxieDate,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser,UserAreaCode,YinJinying_other ");
            strSql.Append(" FROM [NHS_Hqyxjc] ");
            strSql.Append(" where commid=@commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@commid", SqlDbType.VarChar,50)};
            parameters[0].Value = commid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["commid"] != null && ds.Tables[0].Rows[0]["commid"].ToString() != "")
                {
                    this.commid = int.Parse(ds.Tables[0].Rows[0]["commid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VisitBH"] != null)
                {
                    this.VisitBH = ds.Tables[0].Rows[0]["VisitBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UnvID"] != null)
                {
                    this.UnvID = ds.Tables[0].Rows[0]["UnvID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["VisitSEX"] != null)
                {
                    this.VisitSEX = ds.Tables[0].Rows[0]["VisitSEX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QcfwzBm"] != null)
                {
                    this.QcfwzBm = ds.Tables[0].Rows[0]["QcfwzBm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NameB"] != null)
                {
                    this.NameB = ds.Tables[0].Rows[0]["NameB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CIDB"] != null)
                {
                    this.CIDB = ds.Tables[0].Rows[0]["CIDB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BirthdayB"] != null && ds.Tables[0].Rows[0]["BirthdayB"].ToString() != "")
                {
                    this.BirthdayB = DateTime.Parse(ds.Tables[0].Rows[0]["BirthdayB"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhiYeB"] != null)
                {
                    this.ZhiYeB = ds.Tables[0].Rows[0]["ZhiYeB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WenhuaB"] != null)
                {
                    this.WenhuaB = ds.Tables[0].Rows[0]["WenhuaB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MinZuB"] != null)
                {
                    this.MinZuB = ds.Tables[0].Rows[0]["MinZuB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegisterAreaCodeB"] != null)
                {
                    this.RegisterAreaCodeB = ds.Tables[0].Rows[0]["RegisterAreaCodeB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegisterAreaNameB"] != null)
                {
                    this.RegisterAreaNameB = ds.Tables[0].Rows[0]["RegisterAreaNameB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurrentAreaCodeB"] != null)
                {
                    this.CurrentAreaCodeB = ds.Tables[0].Rows[0]["CurrentAreaCodeB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurrentAreaNameB"] != null)
                {
                    this.CurrentAreaNameB = ds.Tables[0].Rows[0]["CurrentAreaNameB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurrentAreaYB"] != null)
                {
                    this.CurrentAreaYB = ds.Tables[0].Rows[0]["CurrentAreaYB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WorkUnitB"] != null)
                {
                    this.WorkUnitB = ds.Tables[0].Rows[0]["WorkUnitB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TelB"] != null)
                {
                    this.TelB = ds.Tables[0].Rows[0]["TelB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NameA"] != null)
                {
                    this.NameA = ds.Tables[0].Rows[0]["NameA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CIDA"] != null)
                {
                    this.CIDA = ds.Tables[0].Rows[0]["CIDA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BirthdayA"] != null && ds.Tables[0].Rows[0]["BirthdayA"].ToString() != "")
                {
                    this.BirthdayA = DateTime.Parse(ds.Tables[0].Rows[0]["BirthdayA"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhiYeA"] != null)
                {
                    this.ZhiYeA = ds.Tables[0].Rows[0]["ZhiYeA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WenhuaA"] != null)
                {
                    this.WenhuaA = ds.Tables[0].Rows[0]["WenhuaA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MinZuA"] != null)
                {
                    this.MinZuA = ds.Tables[0].Rows[0]["MinZuA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegisterAreaCodeA"] != null)
                {
                    this.RegisterAreaCodeA = ds.Tables[0].Rows[0]["RegisterAreaCodeA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegisterAreaNameA"] != null)
                {
                    this.RegisterAreaNameA = ds.Tables[0].Rows[0]["RegisterAreaNameA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurrentAreaCodeA"] != null)
                {
                    this.CurrentAreaCodeA = ds.Tables[0].Rows[0]["CurrentAreaCodeA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurrentAreaNameA"] != null)
                {
                    this.CurrentAreaNameA = ds.Tables[0].Rows[0]["CurrentAreaNameA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurrentAreaYA"] != null)
                {
                    this.CurrentAreaYA = ds.Tables[0].Rows[0]["CurrentAreaYA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WorkUnitA"] != null)
                {
                    this.WorkUnitA = ds.Tables[0].Rows[0]["WorkUnitA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TelA"] != null)
                {
                    this.TelA = ds.Tables[0].Rows[0]["TelA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CheckDate"] != null && ds.Tables[0].Rows[0]["CheckDate"].ToString() != "")
                {
                    this.CheckDate = DateTime.Parse(ds.Tables[0].Rows[0]["CheckDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XueYuan"] != null)
                {
                    this.XueYuan = ds.Tables[0].Rows[0]["XueYuan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XueYuan_other"] != null)
                {
                    this.XueYuan_other = ds.Tables[0].Rows[0]["XueYuan_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Jw"] != null)
                {
                    this.History_Jw = ds.Tables[0].Rows[0]["History_Jw"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Jw_other"] != null)
                {
                    this.History_Jw_other = ds.Tables[0].Rows[0]["History_Jw_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_ss"] != null)
                {
                    this.History_ss = ds.Tables[0].Rows[0]["History_ss"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_ss_other"] != null)
                {
                    this.History_ss_other = ds.Tables[0].Rows[0]["History_ss_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_other"] != null)
                {
                    this.History_other = ds.Tables[0].Rows[0]["History_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_now"] != null)
                {
                    this.History_now = ds.Tables[0].Rows[0]["History_now"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_now_other"] != null)
                {
                    this.History_now_other = ds.Tables[0].Rows[0]["History_now_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuchaoAge"] != null && ds.Tables[0].Rows[0]["ChuchaoAge"].ToString() != "")
                {
                    this.ChuchaoAge = int.Parse(ds.Tables[0].Rows[0]["ChuchaoAge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YuejingWeek"] != null)
                {
                    this.YuejingWeek = ds.Tables[0].Rows[0]["YuejingWeek"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Yuejingliang"] != null)
                {
                    this.Yuejingliang = ds.Tables[0].Rows[0]["Yuejingliang"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TongJing"] != null)
                {
                    this.TongJing = ds.Tables[0].Rows[0]["TongJing"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MoCiYueJing"] != null && ds.Tables[0].Rows[0]["MoCiYueJing"].ToString() != "")
                {
                    this.MoCiYueJing = DateTime.Parse(ds.Tables[0].Rows[0]["MoCiYueJing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["History_Jwhy"] != null)
                {
                    this.History_Jwhy = ds.Tables[0].Rows[0]["History_Jwhy"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuYueZaoChanLiuChan"] != null)
                {
                    this.ZuYueZaoChanLiuChan = ds.Tables[0].Rows[0]["ZuYueZaoChanLiuChan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Child_boynum"] != null)
                {
                    this.Child_boynum = ds.Tables[0].Rows[0]["Child_boynum"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Child_girlnum"] != null)
                {
                    this.Child_girlnum = ds.Tables[0].Rows[0]["Child_girlnum"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Jz"] != null)
                {
                    this.History_Jz = ds.Tables[0].Rows[0]["History_Jz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Jz_other"] != null)
                {
                    this.History_Jz_other = ds.Tables[0].Rows[0]["History_Jz_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HuanzheGX"] != null)
                {
                    this.HuanzheGX = ds.Tables[0].Rows[0]["HuanzheGX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FamilyHunPei"] != null)
                {
                    this.FamilyHunPei = ds.Tables[0].Rows[0]["FamilyHunPei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Doctor1"] != null)
                {
                    this.Doctor1 = ds.Tables[0].Rows[0]["Doctor1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XueYa"] != null)
                {
                    this.XueYa = ds.Tables[0].Rows[0]["XueYa"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TeSheTiTai"] != null)
                {
                    this.TeSheTiTai = ds.Tables[0].Rows[0]["TeSheTiTai"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TeSheTiTai_other"] != null)
                {
                    this.TeSheTiTai_other = ds.Tables[0].Rows[0]["TeSheTiTai_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Jingshenzhuangtai"] != null)
                {
                    this.Jingshenzhuangtai = ds.Tables[0].Rows[0]["Jingshenzhuangtai"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Jingshenzhuangtai_other"] != null)
                {
                    this.Jingshenzhuangtai_other = ds.Tables[0].Rows[0]["Jingshenzhuangtai_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TeshuMianrong"] != null)
                {
                    this.TeshuMianrong = ds.Tables[0].Rows[0]["TeshuMianrong"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TeshuMianrong_other"] != null)
                {
                    this.TeshuMianrong_other = ds.Tables[0].Rows[0]["TeshuMianrong_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhiLi"] != null)
                {
                    this.ZhiLi = ds.Tables[0].Rows[0]["ZhiLi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Pifumaofa"] != null)
                {
                    this.Pifumaofa = ds.Tables[0].Rows[0]["Pifumaofa"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Pifumaofa_other"] != null)
                {
                    this.Pifumaofa_other = ds.Tables[0].Rows[0]["Pifumaofa_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Wuguan"] != null)
                {
                    this.Wuguan = ds.Tables[0].Rows[0]["Wuguan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Wuguan_other"] != null)
                {
                    this.Wuguan_other = ds.Tables[0].Rows[0]["Wuguan_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Jiazhuangxian"] != null)
                {
                    this.Jiazhuangxian = ds.Tables[0].Rows[0]["Jiazhuangxian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Jiazhuangxian_other"] != null)
                {
                    this.Jiazhuangxian_other = ds.Tables[0].Rows[0]["Jiazhuangxian_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Xinlv1"] != null)
                {
                    this.Xinlv1 = ds.Tables[0].Rows[0]["Xinlv1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Xinlv2"] != null)
                {
                    this.Xinlv2 = ds.Tables[0].Rows[0]["Xinlv2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZaYin"] != null)
                {
                    this.ZaYin = ds.Tables[0].Rows[0]["ZaYin"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZaYin_other"] != null)
                {
                    this.ZaYin_other = ds.Tables[0].Rows[0]["ZaYin_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fei"] != null)
                {
                    this.Fei = ds.Tables[0].Rows[0]["Fei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fei_other"] != null)
                {
                    this.Fei_other = ds.Tables[0].Rows[0]["Fei_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Gan"] != null)
                {
                    this.Gan = ds.Tables[0].Rows[0]["Gan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Gan_other"] != null)
                {
                    this.Gan_other = ds.Tables[0].Rows[0]["Gan_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SizhiJizhu"] != null)
                {
                    this.SizhiJizhu = ds.Tables[0].Rows[0]["SizhiJizhu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SizhiJizhu_other"] != null)
                {
                    this.SizhiJizhu_other = ds.Tables[0].Rows[0]["SizhiJizhu_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qita1"] != null)
                {
                    this.Qita1 = ds.Tables[0].Rows[0]["Qita1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Doctor2"] != null)
                {
                    this.Doctor2 = ds.Tables[0].Rows[0]["Doctor2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Houjie"] != null)
                {
                    this.Houjie = ds.Tables[0].Rows[0]["Houjie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Yinmao"] != null)
                {
                    this.Yinmao = ds.Tables[0].Rows[0]["Yinmao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Rufang"] != null)
                {
                    this.Rufang = ds.Tables[0].Rows[0]["Rufang"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Rufang_other"] != null)
                {
                    this.Rufang_other = ds.Tables[0].Rows[0]["Rufang_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YinJinying"] != null)
                {
                    this.YinJinying = ds.Tables[0].Rows[0]["YinJinying"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Baopi"] != null)
                {
                    this.Baopi = ds.Tables[0].Rows[0]["Baopi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GaoWanTIjiL"] != null)
                {
                    this.GaoWanTIjiL = ds.Tables[0].Rows[0]["GaoWanTIjiL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GaoWanTIjiR"] != null)
                {
                    this.GaoWanTIjiR = ds.Tables[0].Rows[0]["GaoWanTIjiR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GaoWanMMJ"] != null)
                {
                    this.GaoWanMMJ = ds.Tables[0].Rows[0]["GaoWanMMJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuWanJiejieL"] != null)
                {
                    this.FuWanJiejieL = ds.Tables[0].Rows[0]["FuWanJiejieL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuWanJiejieR"] != null)
                {
                    this.FuWanJiejieR = ds.Tables[0].Rows[0]["FuWanJiejieR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JSJMQZ"] != null)
                {
                    this.JSJMQZ = ds.Tables[0].Rows[0]["JSJMQZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JSJMQZ_buwei"] != null)
                {
                    this.JSJMQZ_buwei = ds.Tables[0].Rows[0]["JSJMQZ_buwei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JSJMQZ_chengdu"] != null)
                {
                    this.JSJMQZ_chengdu = ds.Tables[0].Rows[0]["JSJMQZ_chengdu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WaiYin"] != null)
                {
                    this.WaiYin = ds.Tables[0].Rows[0]["WaiYin"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FenMiWu"] != null)
                {
                    this.FenMiWu = ds.Tables[0].Rows[0]["FenMiWu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZiGong"] != null)
                {
                    this.ZiGong = ds.Tables[0].Rows[0]["ZiGong"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuJian"] != null)
                {
                    this.FuJian = ds.Tables[0].Rows[0]["FuJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WaiYin_by"] != null)
                {
                    this.WaiYin_by = ds.Tables[0].Rows[0]["WaiYin_by"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YinDao_by"] != null)
                {
                    this.YinDao_by = ds.Tables[0].Rows[0]["YinDao_by"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Gongjing_by"] != null)
                {
                    this.Gongjing_by = ds.Tables[0].Rows[0]["Gongjing_by"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZiGong_by"] != null)
                {
                    this.ZiGong_by = ds.Tables[0].Rows[0]["ZiGong_by"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuJian_bu"] != null)
                {
                    this.FuJian_bu = ds.Tables[0].Rows[0]["FuJian_bu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qita2"] != null)
                {
                    this.Qita2 = ds.Tables[0].Rows[0]["Qita2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Doctor3"] != null)
                {
                    this.Doctor3 = ds.Tables[0].Rows[0]["Doctor3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["VisitJieGuo"] != null)
                {
                    this.VisitJieGuo = ds.Tables[0].Rows[0]["VisitJieGuo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YiChang"] != null)
                {
                    this.YiChang = ds.Tables[0].Rows[0]["YiChang"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JibingZhenduan"] != null)
                {
                    this.JibingZhenduan = ds.Tables[0].Rows[0]["JibingZhenduan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YiXueYiJian"] != null)
                {
                    this.YiXueYiJian = ds.Tables[0].Rows[0]["YiXueYiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HunQianZIXun"] != null)
                {
                    this.HunQianZIXun = ds.Tables[0].Rows[0]["HunQianZIXun"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhiDaoJieGuo"] != null)
                {
                    this.ZhiDaoJieGuo = ds.Tables[0].Rows[0]["ZhiDaoJieGuo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhuanZhengYiYuan"] != null)
                {
                    this.ZhuanZhengYiYuan = ds.Tables[0].Rows[0]["ZhuanZhengYiYuan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhuanZhengDate"] != null && ds.Tables[0].Rows[0]["ZhuanZhengDate"].ToString() != "")
                {
                    this.ZhuanZhengDate = DateTime.Parse(ds.Tables[0].Rows[0]["ZhuanZhengDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YuYueDate"] != null && ds.Tables[0].Rows[0]["YuYueDate"].ToString() != "")
                {
                    this.YuYueDate = DateTime.Parse(ds.Tables[0].Rows[0]["YuYueDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChuJuDate"] != null && ds.Tables[0].Rows[0]["ChuJuDate"].ToString() != "")
                {
                    this.ChuJuDate = DateTime.Parse(ds.Tables[0].Rows[0]["ChuJuDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Doctor4"] != null)
                {
                    this.Doctor4 = ds.Tables[0].Rows[0]["Doctor4"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TianxieDate"] != null && ds.Tables[0].Rows[0]["TianxieDate"].ToString() != "")
                {
                    this.TianxieDate = DateTime.Parse(ds.Tables[0].Rows[0]["TianxieDate"].ToString());
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
                if (ds.Tables[0].Rows[0]["UserAreaCode"] != null)
                {
                    this.UserAreaCode = ds.Tables[0].Rows[0]["UserAreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YinJinying_other"] != null)
                {
                    this.YinJinying_other = ds.Tables[0].Rows[0]["YinJinying_other"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_Hqyxjc]");
            strSql.Append(" where commid=@commid ");

            SqlParameter[] parameters = {
					new SqlParameter("@commid", SqlDbType.Int,4)};
            parameters[0].Value = commid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [NHS_Hqyxjc] (VisitBH,UnvID,VisitSEX,QcfwzBm,NameB,CIDB,BirthdayB,ZhiYeB,WenhuaB,MinZuB,RegisterAreaCodeB,RegisterAreaNameB,CurrentAreaCodeB,CurrentAreaNameB,CurrentAreaYB,WorkUnitB,TelB,NameA,CIDA,BirthdayA,ZhiYeA,WenhuaA,MinZuA,RegisterAreaCodeA,RegisterAreaNameA,CurrentAreaCodeA,CurrentAreaNameA,CurrentAreaYA,WorkUnitA,TelA,TianxieDate,CreateDate,CreateUser,UserAreaCode)");
            strSql.Append(" values (");
            strSql.Append("@VisitBH,@UnvID,@VisitSEX,@QcfwzBm,@NameB,@CIDB,@BirthdayB,@ZhiYeB,@WenhuaB,@MinZuB,@RegisterAreaCodeB,@RegisterAreaNameB,@CurrentAreaCodeB,@CurrentAreaNameB,@CurrentAreaYB,@WorkUnitB,@TelB,@NameA,@CIDA,@BirthdayA,@ZhiYeA,@WenhuaA,@MinZuA,@RegisterAreaCodeA,@RegisterAreaNameA,@CurrentAreaCodeA,@CurrentAreaNameA,@CurrentAreaYA,@WorkUnitA,@TelA,@TianxieDate,@CreateDate,@CreateUser,@UserAreaCode)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@VisitBH", SqlDbType.VarChar,50),
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@VisitSEX", SqlDbType.NChar,10),
					new SqlParameter("@QcfwzBm", SqlDbType.VarChar,50),
					new SqlParameter("@NameB", SqlDbType.NVarChar,20),
					new SqlParameter("@CIDB", SqlDbType.VarChar,20),
					new SqlParameter("@BirthdayB", SqlDbType.SmallDateTime),
					new SqlParameter("@ZhiYeB", SqlDbType.VarChar,4),
					new SqlParameter("@WenhuaB", SqlDbType.VarChar,4),
					new SqlParameter("@MinZuB", SqlDbType.NVarChar,20),
					new SqlParameter("@RegisterAreaCodeB", SqlDbType.VarChar,20),
					new SqlParameter("@RegisterAreaNameB", SqlDbType.VarChar,180),
					new SqlParameter("@CurrentAreaCodeB", SqlDbType.VarChar,20),
					new SqlParameter("@CurrentAreaNameB", SqlDbType.VarChar,180),
					new SqlParameter("@CurrentAreaYB", SqlDbType.VarChar,10),
					new SqlParameter("@WorkUnitB", SqlDbType.VarChar,180),
					new SqlParameter("@TelB", SqlDbType.VarChar,20),
					new SqlParameter("@NameA", SqlDbType.NVarChar,20),
					new SqlParameter("@CIDA", SqlDbType.VarChar,20),
					new SqlParameter("@BirthdayA", SqlDbType.SmallDateTime),
					new SqlParameter("@ZhiYeA", SqlDbType.VarChar,4),
					new SqlParameter("@WenhuaA", SqlDbType.VarChar,4),
					new SqlParameter("@MinZuA", SqlDbType.NVarChar,20),
					new SqlParameter("@RegisterAreaCodeA", SqlDbType.VarChar,20),
					new SqlParameter("@RegisterAreaNameA", SqlDbType.VarChar,180),
					new SqlParameter("@CurrentAreaCodeA", SqlDbType.VarChar,20),
					new SqlParameter("@CurrentAreaNameA", SqlDbType.VarChar,180),
					new SqlParameter("@CurrentAreaYA", SqlDbType.VarChar,10),
					new SqlParameter("@WorkUnitA", SqlDbType.VarChar,180),
					new SqlParameter("@TelA", SqlDbType.VarChar,20),
					new SqlParameter("@TianxieDate", SqlDbType.SmallDateTime),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.Int,4),
					new SqlParameter("@UserAreaCode", SqlDbType.VarChar,20)};
            parameters[0].Value = VisitBH;
            parameters[1].Value = UnvID;
            parameters[2].Value = VisitSEX;
            parameters[3].Value = QcfwzBm;
            parameters[4].Value = NameB;
            parameters[5].Value = CIDB;
            parameters[6].Value = BirthdayB;
            parameters[7].Value = ZhiYeB;
            parameters[8].Value = WenhuaB;
            parameters[9].Value = MinZuB;
            parameters[10].Value = RegisterAreaCodeB;
            parameters[11].Value = RegisterAreaNameB;
            parameters[12].Value = CurrentAreaCodeB;
            parameters[13].Value = CurrentAreaNameB;
            parameters[14].Value = CurrentAreaYB;
            parameters[15].Value = WorkUnitB;
            parameters[16].Value = TelB;
            parameters[17].Value = NameA;
            parameters[18].Value = CIDA;
            parameters[19].Value = BirthdayA;
            parameters[20].Value = ZhiYeA;
            parameters[21].Value = WenhuaA;
            parameters[22].Value = MinZuA;
            parameters[23].Value = RegisterAreaCodeA;
            parameters[24].Value = RegisterAreaNameA;
            parameters[25].Value = CurrentAreaCodeA;
            parameters[26].Value = CurrentAreaNameA;
            parameters[27].Value = CurrentAreaYA;
            parameters[28].Value = WorkUnitA;
            parameters[29].Value = TelA;
            parameters[30].Value = TianxieDate;
            parameters[31].Value = CreateDate;
            parameters[32].Value = CreateUser;
            parameters[33].Value = UserAreaCode;

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
        public bool Update0()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [NHS_Hqyxjc] set ");
            strSql.Append("VisitSEX=@VisitSEX,");
            strSql.Append("QcfwzBm=@QcfwzBm,");
            strSql.Append("NameB=@NameB,");
            strSql.Append("CIDB=@CIDB,");
            strSql.Append("BirthdayB=@BirthdayB,");
            strSql.Append("ZhiYeB=@ZhiYeB,");
            strSql.Append("WenhuaB=@WenhuaB,");
            strSql.Append("MinZuB=@MinZuB,");
            strSql.Append("RegisterAreaCodeB=@RegisterAreaCodeB,");
            strSql.Append("RegisterAreaNameB=@RegisterAreaNameB,");
            strSql.Append("CurrentAreaCodeB=@CurrentAreaCodeB,");
            strSql.Append("CurrentAreaNameB=@CurrentAreaNameB,");
            strSql.Append("CurrentAreaYB=@CurrentAreaYB,");
            strSql.Append("WorkUnitB=@WorkUnitB,");
            strSql.Append("TelB=@TelB,");
            strSql.Append("NameA=@NameA,");
            strSql.Append("CIDA=@CIDA,");
            strSql.Append("TianxieDate=@TianxieDate,");
            strSql.Append("LastUpdateDate=@LastUpdateDate,");
            strSql.Append("LastUpdateUser=@LastUpdateUser");
            strSql.Append(" where commid=@commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@VisitSEX", SqlDbType.NChar,10),
					new SqlParameter("@QcfwzBm", SqlDbType.VarChar,50),
					new SqlParameter("@NameB", SqlDbType.NVarChar,20),
					new SqlParameter("@CIDB", SqlDbType.VarChar,20),
					new SqlParameter("@BirthdayB", SqlDbType.SmallDateTime),
					new SqlParameter("@ZhiYeB", SqlDbType.VarChar,4),
					new SqlParameter("@WenhuaB", SqlDbType.VarChar,4),
					new SqlParameter("@MinZuB", SqlDbType.NVarChar,20),
					new SqlParameter("@RegisterAreaCodeB", SqlDbType.VarChar,20),
					new SqlParameter("@RegisterAreaNameB", SqlDbType.VarChar,180),
					new SqlParameter("@CurrentAreaCodeB", SqlDbType.VarChar,20),
					new SqlParameter("@CurrentAreaNameB", SqlDbType.VarChar,180),
					new SqlParameter("@CurrentAreaYB", SqlDbType.VarChar,10),
					new SqlParameter("@WorkUnitB", SqlDbType.VarChar,180),
					new SqlParameter("@TelB", SqlDbType.VarChar,20),
					new SqlParameter("@NameA", SqlDbType.NVarChar,20),
					new SqlParameter("@CIDA", SqlDbType.VarChar,20),
					new SqlParameter("@TianxieDate", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4),
					new SqlParameter("@commid", SqlDbType.Int,4)
            };
            parameters[0].Value = VisitSEX;
            parameters[1].Value = QcfwzBm;
            parameters[2].Value = NameB;
            parameters[3].Value = CIDB;
            parameters[4].Value = BirthdayB;
            parameters[5].Value = ZhiYeB;
            parameters[6].Value = WenhuaB;
            parameters[7].Value = MinZuB;
            parameters[8].Value = RegisterAreaCodeB;
            parameters[9].Value = RegisterAreaNameB;
            parameters[10].Value = CurrentAreaCodeB;
            parameters[11].Value = CurrentAreaNameB;
            parameters[12].Value = CurrentAreaYB;
            parameters[13].Value = WorkUnitB;
            parameters[14].Value = TelB;
            parameters[15].Value = NameA;
            parameters[16].Value = CIDA;
            parameters[17].Value = TianxieDate;
            parameters[18].Value = LastUpdateDate;
            parameters[19].Value = LastUpdateUser;
            parameters[20].Value = commid;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }/// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update1()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [NHS_Hqyxjc] set ");
            strSql.Append("VisitSEX=@VisitSEX,");
            strSql.Append("QcfwzBm=@QcfwzBm,");
            strSql.Append("NameB=@NameB,");
            strSql.Append("CIDB=@CIDB,");
            strSql.Append("BirthdayA=@BirthdayA,");
            strSql.Append("ZhiYeA=@ZhiYeA,");
            strSql.Append("WenhuaA=@WenhuaA,");
            strSql.Append("MinZuA=@MinZuA,");
            strSql.Append("RegisterAreaCodeA=@RegisterAreaCodeA,");
            strSql.Append("RegisterAreaNameA=@RegisterAreaNameA,");
            strSql.Append("CurrentAreaCodeA=@CurrentAreaCodeA,");
            strSql.Append("CurrentAreaNameA=@CurrentAreaNameA,");
            strSql.Append("CurrentAreaYA=@CurrentAreaYA,");
            strSql.Append("WorkUnitA=@WorkUnitA,");
            strSql.Append("TelA=@TelA,");
            strSql.Append("NameA=@NameA,");
            strSql.Append("CIDA=@CIDA,");
            strSql.Append("TianxieDate=@TianxieDate,");
            strSql.Append("LastUpdateDate=@LastUpdateDate,");
            strSql.Append("LastUpdateUser=@LastUpdateUser");
            strSql.Append(" where commid=@commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@VisitSEX", SqlDbType.NChar,10),
					new SqlParameter("@QcfwzBm", SqlDbType.VarChar,50),
					new SqlParameter("@NameB", SqlDbType.NVarChar,20),
					new SqlParameter("@CIDB", SqlDbType.VarChar,20),
					new SqlParameter("@BirthdayA", SqlDbType.SmallDateTime),
					new SqlParameter("@ZhiYeA", SqlDbType.VarChar,4),
					new SqlParameter("@WenhuaA", SqlDbType.VarChar,4),
					new SqlParameter("@MinZuA", SqlDbType.NVarChar,20),
					new SqlParameter("@RegisterAreaCodeA", SqlDbType.VarChar,20),
					new SqlParameter("@RegisterAreaNameA", SqlDbType.VarChar,180),
					new SqlParameter("@CurrentAreaCodeA", SqlDbType.VarChar,20),
					new SqlParameter("@CurrentAreaNameA", SqlDbType.VarChar,180),
					new SqlParameter("@CurrentAreaYA", SqlDbType.VarChar,10),
					new SqlParameter("@WorkUnitA", SqlDbType.VarChar,180),
					new SqlParameter("@TelA", SqlDbType.VarChar,20),
					new SqlParameter("@NameA", SqlDbType.NVarChar,20),
					new SqlParameter("@CIDA", SqlDbType.VarChar,20),
					new SqlParameter("@TianxieDate", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4),
					new SqlParameter("@commid", SqlDbType.Int,4)
            };
            parameters[0].Value = VisitSEX;
            parameters[1].Value = QcfwzBm;
            parameters[2].Value = NameB;
            parameters[3].Value = CIDB;
            parameters[4].Value = BirthdayA;
            parameters[5].Value = ZhiYeA;
            parameters[6].Value = WenhuaA;
            parameters[7].Value = MinZuA;
            parameters[8].Value = RegisterAreaCodeA;
            parameters[9].Value = RegisterAreaNameA;
            parameters[10].Value = CurrentAreaCodeA;
            parameters[11].Value = CurrentAreaNameA;
            parameters[12].Value = CurrentAreaYA;
            parameters[13].Value = WorkUnitA;
            parameters[14].Value = TelA;
            parameters[15].Value = NameA;
            parameters[16].Value = CIDA;
            parameters[17].Value = TianxieDate;
            parameters[18].Value = LastUpdateDate;
            parameters[19].Value = LastUpdateUser;
            parameters[20].Value = commid;

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
        
        public bool Update01()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [NHS_Hqyxjc] set ");
            strSql.Append("CheckDate=@CheckDate,");
            strSql.Append("XueYuan=@XueYuan,");
            strSql.Append("XueYuan_other=@XueYuan_other,");
            strSql.Append("History_Jw=@History_Jw,");
            strSql.Append("History_Jw_other=@History_Jw_other,");
            strSql.Append("History_ss=@History_ss,");
            strSql.Append("History_ss_other=@History_ss_other,");
            strSql.Append("History_other=@History_other,");
            strSql.Append("History_now=@History_now,");
            strSql.Append("History_now_other=@History_now_other,");
            strSql.Append("ChuchaoAge=@ChuchaoAge,");
            strSql.Append("YuejingWeek=@YuejingWeek,");
            strSql.Append("Yuejingliang=@Yuejingliang,");
            strSql.Append("TongJing=@TongJing,");
            strSql.Append("MoCiYueJing=@MoCiYueJing,");
            strSql.Append("History_Jwhy=@History_Jwhy,");
            strSql.Append("ZuYueZaoChanLiuChan=@ZuYueZaoChanLiuChan,");
            strSql.Append("Child_boynum=@Child_boynum,");
            strSql.Append("Child_girlnum=@Child_girlnum,");
            strSql.Append("History_Jz=@History_Jz,");
            strSql.Append("History_Jz_other=@History_Jz_other,");
            strSql.Append("HuanzheGX=@HuanzheGX,");
            strSql.Append("FamilyHunPei=@FamilyHunPei,");
            strSql.Append("Doctor1=@Doctor1,");
            strSql.Append("LastUpdateDate=@LastUpdateDate,");
            strSql.Append("LastUpdateUser=@LastUpdateUser");
            strSql.Append(" where commid=@commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@CheckDate", SqlDbType.SmallDateTime),
					new SqlParameter("@XueYuan", SqlDbType.VarChar,50),
					new SqlParameter("@XueYuan_other", SqlDbType.VarChar,50),
					new SqlParameter("@History_Jw", SqlDbType.VarChar,50),
					new SqlParameter("@History_Jw_other", SqlDbType.VarChar,50),
					new SqlParameter("@History_ss", SqlDbType.VarChar,50),
					new SqlParameter("@History_ss_other", SqlDbType.VarChar,50),
					new SqlParameter("@History_other", SqlDbType.VarChar,50),
					new SqlParameter("@History_now", SqlDbType.VarChar,50),
					new SqlParameter("@History_now_other", SqlDbType.VarChar,50),
					new SqlParameter("@ChuchaoAge", SqlDbType.Int,4),
					new SqlParameter("@YuejingWeek", SqlDbType.VarChar,20),
					new SqlParameter("@Yuejingliang", SqlDbType.VarChar,20),
					new SqlParameter("@TongJing", SqlDbType.VarChar,20),
					new SqlParameter("@MoCiYueJing", SqlDbType.SmallDateTime),
					new SqlParameter("@History_Jwhy", SqlDbType.VarChar,50),
					new SqlParameter("@ZuYueZaoChanLiuChan", SqlDbType.VarChar,50),
					new SqlParameter("@Child_boynum", SqlDbType.NChar,10),
					new SqlParameter("@Child_girlnum", SqlDbType.NChar,10),
					new SqlParameter("@History_Jz", SqlDbType.VarChar,50),
					new SqlParameter("@History_Jz_other", SqlDbType.VarChar,50),
					new SqlParameter("@HuanzheGX", SqlDbType.VarChar,20),
					new SqlParameter("@FamilyHunPei", SqlDbType.VarChar,50),
					new SqlParameter("@Doctor1", SqlDbType.VarChar,50),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4),
					new SqlParameter("@commid", SqlDbType.Int,4)};

            parameters[0].Value = CheckDate;
            parameters[1].Value = XueYuan;
            parameters[2].Value = XueYuan_other;
            parameters[3].Value = History_Jw;
            parameters[4].Value = History_Jw_other;
            parameters[5].Value = History_ss;
            parameters[6].Value = History_ss_other;
            parameters[7].Value = History_other;
            parameters[8].Value = History_now;
            parameters[9].Value = History_now_other;
            parameters[10].Value = ChuchaoAge;
            parameters[11].Value = YuejingWeek;
            parameters[12].Value = Yuejingliang;
            parameters[13].Value = TongJing;
            parameters[14].Value = MoCiYueJing;
            parameters[15].Value = History_Jwhy;
            parameters[16].Value = ZuYueZaoChanLiuChan;
            parameters[17].Value = Child_boynum;
            parameters[18].Value = Child_girlnum;
            parameters[19].Value = History_Jz;
            parameters[20].Value = History_Jz_other;
            parameters[21].Value = HuanzheGX;
            parameters[22].Value = FamilyHunPei;
            parameters[23].Value = Doctor1;
            parameters[24].Value = LastUpdateDate;
            parameters[25].Value = LastUpdateUser;
            parameters[26].Value = commid;

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

        public bool Update02()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [NHS_Hqyxjc] set ");
            strSql.Append("XueYa=@XueYa,");
            strSql.Append("TeSheTiTai=@TeSheTiTai,");
            strSql.Append("TeSheTiTai_other=@TeSheTiTai_other,");
            strSql.Append("Jingshenzhuangtai=@Jingshenzhuangtai,");
            strSql.Append("Jingshenzhuangtai_other=@Jingshenzhuangtai_other,");
            strSql.Append("TeshuMianrong=@TeshuMianrong,");
            strSql.Append("TeshuMianrong_other=@TeshuMianrong_other,");
            strSql.Append("ZhiLi=@ZhiLi,");
            strSql.Append("Pifumaofa=@Pifumaofa,");
            strSql.Append("Pifumaofa_other=@Pifumaofa_other,");
            strSql.Append("Wuguan=@Wuguan,");
            strSql.Append("Wuguan_other=@Wuguan_other,");
            strSql.Append("Jiazhuangxian=@Jiazhuangxian,");
            strSql.Append("Jiazhuangxian_other=@Jiazhuangxian_other,");
            strSql.Append("Xinlv1=@Xinlv1,");
            strSql.Append("Xinlv2=@Xinlv2,");
            strSql.Append("ZaYin=@ZaYin,");
            strSql.Append("ZaYin_other=@ZaYin_other,");
            strSql.Append("Fei=@Fei,");
            strSql.Append("Fei_other=@Fei_other,");
            strSql.Append("Gan=@Gan,");
            strSql.Append("Gan_other=@Gan_other,");
            strSql.Append("SizhiJizhu=@SizhiJizhu,");
            strSql.Append("SizhiJizhu_other=@SizhiJizhu_other,");
            strSql.Append("Qita1=@Qita1,");
            strSql.Append("Doctor2=@Doctor2,");
            strSql.Append("LastUpdateDate=@LastUpdateDate,");
            strSql.Append("LastUpdateUser=@LastUpdateUser");
            strSql.Append(" where commid=@commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@XueYa", SqlDbType.VarChar,50),
					new SqlParameter("@TeSheTiTai", SqlDbType.VarChar,50),
					new SqlParameter("@TeSheTiTai_other", SqlDbType.VarChar,50),
					new SqlParameter("@Jingshenzhuangtai", SqlDbType.VarChar,50),
					new SqlParameter("@Jingshenzhuangtai_other", SqlDbType.VarChar,50),
					new SqlParameter("@TeshuMianrong", SqlDbType.VarChar,50),
					new SqlParameter("@TeshuMianrong_other", SqlDbType.VarChar,50),
					new SqlParameter("@ZhiLi", SqlDbType.VarChar,50),
					new SqlParameter("@Pifumaofa", SqlDbType.VarChar,50),
					new SqlParameter("@Pifumaofa_other", SqlDbType.VarChar,50),
					new SqlParameter("@Wuguan", SqlDbType.VarChar,50),
					new SqlParameter("@Wuguan_other", SqlDbType.VarChar,50),
					new SqlParameter("@Jiazhuangxian", SqlDbType.VarChar,50),
					new SqlParameter("@Jiazhuangxian_other", SqlDbType.VarChar,50),
					new SqlParameter("@Xinlv1", SqlDbType.NChar,10),
					new SqlParameter("@Xinlv2", SqlDbType.NChar,10),
					new SqlParameter("@ZaYin", SqlDbType.VarChar,20),
					new SqlParameter("@ZaYin_other", SqlDbType.VarChar,50),
					new SqlParameter("@Fei", SqlDbType.VarChar,20),
					new SqlParameter("@Fei_other", SqlDbType.VarChar,50),
					new SqlParameter("@Gan", SqlDbType.VarChar,20),
					new SqlParameter("@Gan_other", SqlDbType.VarChar,50),
					new SqlParameter("@SizhiJizhu", SqlDbType.VarChar,50),
					new SqlParameter("@SizhiJizhu_other", SqlDbType.VarChar,50),
					new SqlParameter("@Qita1", SqlDbType.VarChar,100),
					new SqlParameter("@Doctor2", SqlDbType.VarChar,50),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4),
					new SqlParameter("@commid", SqlDbType.Int,4)};

            parameters[0].Value = XueYa;
            parameters[1].Value = TeSheTiTai;
            parameters[2].Value = TeSheTiTai_other;
            parameters[3].Value = Jingshenzhuangtai;
            parameters[4].Value = Jingshenzhuangtai_other;
            parameters[5].Value = TeshuMianrong;
            parameters[6].Value = TeshuMianrong_other;
            parameters[7].Value = ZhiLi;
            parameters[8].Value = Pifumaofa;
            parameters[9].Value = Pifumaofa_other;
            parameters[10].Value = Wuguan;
            parameters[11].Value = Wuguan_other;
            parameters[12].Value = Jiazhuangxian;
            parameters[13].Value = Jiazhuangxian_other;
            parameters[14].Value = Xinlv1;
            parameters[15].Value = Xinlv2;
            parameters[16].Value = ZaYin;
            parameters[17].Value = ZaYin_other;
            parameters[18].Value = Fei;
            parameters[19].Value = Fei_other;
            parameters[20].Value = Gan;
            parameters[21].Value = Gan_other;
            parameters[22].Value = SizhiJizhu;
            parameters[23].Value = SizhiJizhu_other;
            parameters[24].Value = Qita1;
            parameters[25].Value = Doctor2;
            parameters[26].Value = LastUpdateDate;
            parameters[27].Value = LastUpdateUser;
            parameters[28].Value = commid;

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

        public bool Update03()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [NHS_Hqyxjc] set ");
            strSql.Append("Houjie=@Houjie,");
            strSql.Append("Yinmao=@Yinmao,");
            strSql.Append("Rufang=@Rufang,");
            strSql.Append("Rufang_other=@Rufang_other,");
            strSql.Append("YinJinying=@YinJinying,");
            strSql.Append("Baopi=@Baopi,");
            strSql.Append("GaoWanTIjiL=@GaoWanTIjiL,");
            strSql.Append("GaoWanTIjiR=@GaoWanTIjiR,");
            strSql.Append("GaoWanMMJ=@GaoWanMMJ,");
            strSql.Append("FuWanJiejieL=@FuWanJiejieL,");
            strSql.Append("FuWanJiejieR=@FuWanJiejieR,");
            strSql.Append("JSJMQZ=@JSJMQZ,");
            strSql.Append("JSJMQZ_buwei=@JSJMQZ_buwei,");
            strSql.Append("JSJMQZ_chengdu=@JSJMQZ_chengdu,");
            strSql.Append("WaiYin=@WaiYin,");
            strSql.Append("FenMiWu=@FenMiWu,");
            strSql.Append("ZiGong=@ZiGong,");
            strSql.Append("FuJian=@FuJian,");
            strSql.Append("WaiYin_by=@WaiYin_by,");
            strSql.Append("YinDao_by=@YinDao_by,");
            strSql.Append("Gongjing_by=@Gongjing_by,");
            strSql.Append("ZiGong_by=@ZiGong_by,");
            strSql.Append("FuJian_bu=@FuJian_bu,");
            strSql.Append("Qita2=@Qita2,");
            strSql.Append("Doctor3=@Doctor3,");
            strSql.Append("LastUpdateDate=@LastUpdateDate,");
            strSql.Append("LastUpdateUser=@LastUpdateUser,");
            strSql.Append("YinJinying_other=@YinJinying_other");
            strSql.Append(" where commid=@commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Houjie", SqlDbType.VarChar,10),
					new SqlParameter("@Yinmao", SqlDbType.VarChar,10),
					new SqlParameter("@Rufang", SqlDbType.VarChar,50),
					new SqlParameter("@Rufang_other", SqlDbType.VarChar,50),
					new SqlParameter("@YinJinying", SqlDbType.VarChar,50),
					new SqlParameter("@Baopi", SqlDbType.VarChar,10),
					new SqlParameter("@GaoWanTIjiL", SqlDbType.VarChar,20),
					new SqlParameter("@GaoWanTIjiR", SqlDbType.VarChar,20),
					new SqlParameter("@GaoWanMMJ", SqlDbType.NChar,10),
					new SqlParameter("@FuWanJiejieL", SqlDbType.VarChar,20),
					new SqlParameter("@FuWanJiejieR", SqlDbType.VarChar,20),
					new SqlParameter("@JSJMQZ", SqlDbType.NChar,10),
					new SqlParameter("@JSJMQZ_buwei", SqlDbType.VarChar,20),
					new SqlParameter("@JSJMQZ_chengdu", SqlDbType.VarChar,20),
					new SqlParameter("@WaiYin", SqlDbType.NVarChar,50),
					new SqlParameter("@FenMiWu", SqlDbType.NVarChar,50),
					new SqlParameter("@ZiGong", SqlDbType.NVarChar,50),
					new SqlParameter("@FuJian", SqlDbType.NVarChar,50),
					new SqlParameter("@WaiYin_by", SqlDbType.NVarChar,50),
					new SqlParameter("@YinDao_by", SqlDbType.NVarChar,50),
					new SqlParameter("@Gongjing_by", SqlDbType.NVarChar,50),
					new SqlParameter("@ZiGong_by", SqlDbType.NVarChar,50),
					new SqlParameter("@FuJian_bu", SqlDbType.NVarChar,50),
					new SqlParameter("@Qita2", SqlDbType.VarChar,100),
					new SqlParameter("@Doctor3", SqlDbType.VarChar,50),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4),
					new SqlParameter("@YinJinying_other", SqlDbType.VarChar,50),
					new SqlParameter("@commid", SqlDbType.Int,4)};
            parameters[0].Value = Houjie;
            parameters[1].Value = Yinmao;
            parameters[2].Value = Rufang;
            parameters[3].Value = Rufang_other;
            parameters[4].Value = YinJinying;
            parameters[5].Value = Baopi;
            parameters[6].Value = GaoWanTIjiL;
            parameters[7].Value = GaoWanTIjiR;
            parameters[8].Value = GaoWanMMJ;
            parameters[9].Value = FuWanJiejieL;
            parameters[10].Value = FuWanJiejieR;
            parameters[11].Value = JSJMQZ;
            parameters[12].Value = JSJMQZ_buwei;
            parameters[13].Value = JSJMQZ_chengdu;
            parameters[14].Value = WaiYin;
            parameters[15].Value = FenMiWu;
            parameters[16].Value = ZiGong;
            parameters[17].Value = FuJian;
            parameters[18].Value = WaiYin_by;
            parameters[19].Value = YinDao_by;
            parameters[20].Value = Gongjing_by;
            parameters[21].Value = ZiGong_by;
            parameters[22].Value = FuJian_bu;
            parameters[23].Value = Qita2;
            parameters[24].Value = Doctor3;
            parameters[25].Value = LastUpdateDate;
            parameters[26].Value = LastUpdateUser;
            parameters[27].Value = YinJinying_other;
            parameters[28].Value = commid;

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

        public bool Update04()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [NHS_Hqyxjc] set ");
            strSql.Append("VisitJieGuo=@VisitJieGuo,");
            strSql.Append("YiChang=@YiChang,");
            strSql.Append("JibingZhenduan=@JibingZhenduan,");
            strSql.Append("YiXueYiJian=@YiXueYiJian,");
            strSql.Append("HunQianZIXun=@HunQianZIXun,");
            strSql.Append("ZhiDaoJieGuo=@ZhiDaoJieGuo,");
            strSql.Append("ZhuanZhengYiYuan=@ZhuanZhengYiYuan,");
            strSql.Append("ZhuanZhengDate=@ZhuanZhengDate,");
            strSql.Append("YuYueDate=@YuYueDate,");
            strSql.Append("ChuJuDate=@ChuJuDate,");
            strSql.Append("Doctor4=@Doctor4,");
            strSql.Append("LastUpdateDate=@LastUpdateDate,");
            strSql.Append("LastUpdateUser=@LastUpdateUser");
            strSql.Append(" where commid=@commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@VisitJieGuo", SqlDbType.NVarChar,20),
					new SqlParameter("@YiChang", SqlDbType.VarChar,200),
					new SqlParameter("@JibingZhenduan", SqlDbType.VarChar,200),
					new SqlParameter("@YiXueYiJian", SqlDbType.NChar,10),
					new SqlParameter("@HunQianZIXun", SqlDbType.VarChar,200),
					new SqlParameter("@ZhiDaoJieGuo", SqlDbType.NChar,10),
					new SqlParameter("@ZhuanZhengYiYuan", SqlDbType.NVarChar,50),
					new SqlParameter("@ZhuanZhengDate", SqlDbType.SmallDateTime),
					new SqlParameter("@YuYueDate", SqlDbType.SmallDateTime),
					new SqlParameter("@ChuJuDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Doctor4", SqlDbType.VarChar,50),
					new SqlParameter("@LastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateUser", SqlDbType.Int,4),
					new SqlParameter("@commid", SqlDbType.Int,4)};
            parameters[0].Value = VisitJieGuo;
            parameters[1].Value = YiChang;
            parameters[2].Value = JibingZhenduan;
            parameters[3].Value = YiXueYiJian;
            parameters[4].Value = HunQianZIXun;
            parameters[5].Value = ZhiDaoJieGuo;
            parameters[6].Value = ZhuanZhengYiYuan;
            parameters[7].Value = ZhuanZhengDate;
            parameters[8].Value = YuYueDate;
            parameters[9].Value = ChuJuDate;
            parameters[10].Value = Doctor4;
            parameters[11].Value = LastUpdateDate;
            parameters[12].Value = LastUpdateUser;
            parameters[13].Value = commid;

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
        public bool Delete(int commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [NHS_Hqyxjc] ");
            strSql.Append(" where commid=@commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@commid", SqlDbType.Int,4)};
            parameters[0].Value = commid;

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
        public void GetModel(int commid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select commid,VisitBH,UnvID,VisitSEX,QcfwzBm,NameB,CIDB,BirthdayB,ZhiYeB,WenhuaB,MinZuB,RegisterAreaCodeB,RegisterAreaNameB,CurrentAreaCodeB,CurrentAreaNameB,CurrentAreaYB,WorkUnitB,TelB,NameA,CIDA,BirthdayA,ZhiYeA,WenhuaA,MinZuA,RegisterAreaCodeA,RegisterAreaNameA,CurrentAreaCodeA,CurrentAreaNameA,CurrentAreaYA,WorkUnitA,TelA,CheckDate,XueYuan,XueYuan_other,History_Jw,History_Jw_other,History_ss,History_ss_other,History_other,History_now,History_now_other,ChuchaoAge,YuejingWeek,Yuejingliang,TongJing,MoCiYueJing,History_Jwhy,ZuYueZaoChanLiuChan,Child_boynum,Child_girlnum,History_Jz,History_Jz_other,HuanzheGX,FamilyHunPei,Doctor1,XueYa,TeSheTiTai,TeSheTiTai_other,Jingshenzhuangtai,Jingshenzhuangtai_other,TeshuMianrong,TeshuMianrong_other,ZhiLi,Pifumaofa,Pifumaofa_other,Wuguan,Wuguan_other,Jiazhuangxian,Jiazhuangxian_other,Xinlv1,Xinlv2,ZaYin,ZaYin_other,Fei,Fei_other,Gan,Gan_other,SizhiJizhu,SizhiJizhu_other,Qita1,Doctor2,Houjie,Yinmao,Rufang,Rufang_other,YinJinying,Baopi,GaoWanTIjiL,GaoWanTIjiR,GaoWanMMJ,FuWanJiejieL,FuWanJiejieR,JSJMQZ,JSJMQZ_buwei,JSJMQZ_chengdu,WaiYin,FenMiWu,ZiGong,FuJian,WaiYin_by,YinDao_by,Gongjing_by,ZiGong_by,FuJian_bu,Qita2,Doctor3,VisitJieGuo,YiChang,JibingZhenduan,YiXueYiJian,HunQianZIXun,ZhiDaoJieGuo,ZhuanZhengYiYuan,ZhuanZhengDate,YuYueDate,ChuJuDate,Doctor4,TianxieDate,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser,UserAreaCode ");
            strSql.Append(" FROM [NHS_Hqyxjc] ");
            strSql.Append(" where commid=@commid ");
            SqlParameter[] parameters = {
					new SqlParameter("@commid", SqlDbType.Int,4)};
            parameters[0].Value = commid;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["commid"] != null && ds.Tables[0].Rows[0]["commid"].ToString() != "")
                {
                    this.commid = int.Parse(ds.Tables[0].Rows[0]["commid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VisitBH"] != null)
                {
                    this.VisitBH = ds.Tables[0].Rows[0]["VisitBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UnvID"] != null)
                {
                    this.UnvID = ds.Tables[0].Rows[0]["UnvID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["VisitSEX"] != null)
                {
                    this.VisitSEX = ds.Tables[0].Rows[0]["VisitSEX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QcfwzBm"] != null)
                {
                    this.QcfwzBm = ds.Tables[0].Rows[0]["QcfwzBm"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NameB"] != null)
                {
                    this.NameB = ds.Tables[0].Rows[0]["NameB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CIDB"] != null)
                {
                    this.CIDB = ds.Tables[0].Rows[0]["CIDB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BirthdayB"] != null && ds.Tables[0].Rows[0]["BirthdayB"].ToString() != "")
                {
                    this.BirthdayB = DateTime.Parse(ds.Tables[0].Rows[0]["BirthdayB"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhiYeB"] != null)
                {
                    this.ZhiYeB = ds.Tables[0].Rows[0]["ZhiYeB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WenhuaB"] != null)
                {
                    this.WenhuaB = ds.Tables[0].Rows[0]["WenhuaB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MinZuB"] != null)
                {
                    this.MinZuB = ds.Tables[0].Rows[0]["MinZuB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegisterAreaCodeB"] != null)
                {
                    this.RegisterAreaCodeB = ds.Tables[0].Rows[0]["RegisterAreaCodeB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegisterAreaNameB"] != null)
                {
                    this.RegisterAreaNameB = ds.Tables[0].Rows[0]["RegisterAreaNameB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurrentAreaCodeB"] != null)
                {
                    this.CurrentAreaCodeB = ds.Tables[0].Rows[0]["CurrentAreaCodeB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurrentAreaNameB"] != null)
                {
                    this.CurrentAreaNameB = ds.Tables[0].Rows[0]["CurrentAreaNameB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurrentAreaYB"] != null)
                {
                    this.CurrentAreaYB = ds.Tables[0].Rows[0]["CurrentAreaYB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WorkUnitB"] != null)
                {
                    this.WorkUnitB = ds.Tables[0].Rows[0]["WorkUnitB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TelB"] != null)
                {
                    this.TelB = ds.Tables[0].Rows[0]["TelB"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NameA"] != null)
                {
                    this.NameA = ds.Tables[0].Rows[0]["NameA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CIDA"] != null)
                {
                    this.CIDA = ds.Tables[0].Rows[0]["CIDA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BirthdayA"] != null && ds.Tables[0].Rows[0]["BirthdayA"].ToString() != "")
                {
                    this.BirthdayA = DateTime.Parse(ds.Tables[0].Rows[0]["BirthdayA"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZhiYeA"] != null)
                {
                    this.ZhiYeA = ds.Tables[0].Rows[0]["ZhiYeA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WenhuaA"] != null)
                {
                    this.WenhuaA = ds.Tables[0].Rows[0]["WenhuaA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MinZuA"] != null)
                {
                    this.MinZuA = ds.Tables[0].Rows[0]["MinZuA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegisterAreaCodeA"] != null)
                {
                    this.RegisterAreaCodeA = ds.Tables[0].Rows[0]["RegisterAreaCodeA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegisterAreaNameA"] != null)
                {
                    this.RegisterAreaNameA = ds.Tables[0].Rows[0]["RegisterAreaNameA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurrentAreaCodeA"] != null)
                {
                    this.CurrentAreaCodeA = ds.Tables[0].Rows[0]["CurrentAreaCodeA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurrentAreaNameA"] != null)
                {
                    this.CurrentAreaNameA = ds.Tables[0].Rows[0]["CurrentAreaNameA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CurrentAreaYA"] != null)
                {
                    this.CurrentAreaYA = ds.Tables[0].Rows[0]["CurrentAreaYA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WorkUnitA"] != null)
                {
                    this.WorkUnitA = ds.Tables[0].Rows[0]["WorkUnitA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TelA"] != null)
                {
                    this.TelA = ds.Tables[0].Rows[0]["TelA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CheckDate"] != null && ds.Tables[0].Rows[0]["CheckDate"].ToString() != "")
                {
                    this.CheckDate = DateTime.Parse(ds.Tables[0].Rows[0]["CheckDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XueYuan"] != null)
                {
                    this.XueYuan = ds.Tables[0].Rows[0]["XueYuan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XueYuan_other"] != null)
                {
                    this.XueYuan_other = ds.Tables[0].Rows[0]["XueYuan_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Jw"] != null)
                {
                    this.History_Jw = ds.Tables[0].Rows[0]["History_Jw"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Jw_other"] != null)
                {
                    this.History_Jw_other = ds.Tables[0].Rows[0]["History_Jw_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_ss"] != null)
                {
                    this.History_ss = ds.Tables[0].Rows[0]["History_ss"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_ss_other"] != null)
                {
                    this.History_ss_other = ds.Tables[0].Rows[0]["History_ss_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_other"] != null)
                {
                    this.History_other = ds.Tables[0].Rows[0]["History_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_now"] != null)
                {
                    this.History_now = ds.Tables[0].Rows[0]["History_now"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_now_other"] != null)
                {
                    this.History_now_other = ds.Tables[0].Rows[0]["History_now_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChuchaoAge"] != null && ds.Tables[0].Rows[0]["ChuchaoAge"].ToString() != "")
                {
                    this.ChuchaoAge = int.Parse(ds.Tables[0].Rows[0]["ChuchaoAge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YuejingWeek"] != null)
                {
                    this.YuejingWeek = ds.Tables[0].Rows[0]["YuejingWeek"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Yuejingliang"] != null)
                {
                    this.Yuejingliang = ds.Tables[0].Rows[0]["Yuejingliang"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TongJing"] != null)
                {
                    this.TongJing = ds.Tables[0].Rows[0]["TongJing"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MoCiYueJing"] != null && ds.Tables[0].Rows[0]["MoCiYueJing"].ToString() != "")
                {
                    this.MoCiYueJing = DateTime.Parse(ds.Tables[0].Rows[0]["MoCiYueJing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["History_Jwhy"] != null)
                {
                    this.History_Jwhy = ds.Tables[0].Rows[0]["History_Jwhy"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZuYueZaoChanLiuChan"] != null)
                {
                    this.ZuYueZaoChanLiuChan = ds.Tables[0].Rows[0]["ZuYueZaoChanLiuChan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Child_boynum"] != null)
                {
                    this.Child_boynum = ds.Tables[0].Rows[0]["Child_boynum"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Child_girlnum"] != null)
                {
                    this.Child_girlnum = ds.Tables[0].Rows[0]["Child_girlnum"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Jz"] != null)
                {
                    this.History_Jz = ds.Tables[0].Rows[0]["History_Jz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Jz_other"] != null)
                {
                    this.History_Jz_other = ds.Tables[0].Rows[0]["History_Jz_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HuanzheGX"] != null)
                {
                    this.HuanzheGX = ds.Tables[0].Rows[0]["HuanzheGX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FamilyHunPei"] != null)
                {
                    this.FamilyHunPei = ds.Tables[0].Rows[0]["FamilyHunPei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Doctor1"] != null)
                {
                    this.Doctor1 = ds.Tables[0].Rows[0]["Doctor1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XueYa"] != null)
                {
                    this.XueYa = ds.Tables[0].Rows[0]["XueYa"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TeSheTiTai"] != null)
                {
                    this.TeSheTiTai = ds.Tables[0].Rows[0]["TeSheTiTai"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TeSheTiTai_other"] != null)
                {
                    this.TeSheTiTai_other = ds.Tables[0].Rows[0]["TeSheTiTai_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Jingshenzhuangtai"] != null)
                {
                    this.Jingshenzhuangtai = ds.Tables[0].Rows[0]["Jingshenzhuangtai"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Jingshenzhuangtai_other"] != null)
                {
                    this.Jingshenzhuangtai_other = ds.Tables[0].Rows[0]["Jingshenzhuangtai_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TeshuMianrong"] != null)
                {
                    this.TeshuMianrong = ds.Tables[0].Rows[0]["TeshuMianrong"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TeshuMianrong_other"] != null)
                {
                    this.TeshuMianrong_other = ds.Tables[0].Rows[0]["TeshuMianrong_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhiLi"] != null)
                {
                    this.ZhiLi = ds.Tables[0].Rows[0]["ZhiLi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Pifumaofa"] != null)
                {
                    this.Pifumaofa = ds.Tables[0].Rows[0]["Pifumaofa"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Pifumaofa_other"] != null)
                {
                    this.Pifumaofa_other = ds.Tables[0].Rows[0]["Pifumaofa_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Wuguan"] != null)
                {
                    this.Wuguan = ds.Tables[0].Rows[0]["Wuguan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Wuguan_other"] != null)
                {
                    this.Wuguan_other = ds.Tables[0].Rows[0]["Wuguan_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Jiazhuangxian"] != null)
                {
                    this.Jiazhuangxian = ds.Tables[0].Rows[0]["Jiazhuangxian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Jiazhuangxian_other"] != null)
                {
                    this.Jiazhuangxian_other = ds.Tables[0].Rows[0]["Jiazhuangxian_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Xinlv1"] != null)
                {
                    this.Xinlv1 = ds.Tables[0].Rows[0]["Xinlv1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Xinlv2"] != null)
                {
                    this.Xinlv2 = ds.Tables[0].Rows[0]["Xinlv2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZaYin"] != null)
                {
                    this.ZaYin = ds.Tables[0].Rows[0]["ZaYin"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZaYin_other"] != null)
                {
                    this.ZaYin_other = ds.Tables[0].Rows[0]["ZaYin_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fei"] != null)
                {
                    this.Fei = ds.Tables[0].Rows[0]["Fei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fei_other"] != null)
                {
                    this.Fei_other = ds.Tables[0].Rows[0]["Fei_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Gan"] != null)
                {
                    this.Gan = ds.Tables[0].Rows[0]["Gan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Gan_other"] != null)
                {
                    this.Gan_other = ds.Tables[0].Rows[0]["Gan_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SizhiJizhu"] != null)
                {
                    this.SizhiJizhu = ds.Tables[0].Rows[0]["SizhiJizhu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SizhiJizhu_other"] != null)
                {
                    this.SizhiJizhu_other = ds.Tables[0].Rows[0]["SizhiJizhu_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qita1"] != null)
                {
                    this.Qita1 = ds.Tables[0].Rows[0]["Qita1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Doctor2"] != null)
                {
                    this.Doctor2 = ds.Tables[0].Rows[0]["Doctor2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Houjie"] != null)
                {
                    this.Houjie = ds.Tables[0].Rows[0]["Houjie"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Yinmao"] != null)
                {
                    this.Yinmao = ds.Tables[0].Rows[0]["Yinmao"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Rufang"] != null)
                {
                    this.Rufang = ds.Tables[0].Rows[0]["Rufang"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Rufang_other"] != null)
                {
                    this.Rufang_other = ds.Tables[0].Rows[0]["Rufang_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YinJinying"] != null)
                {
                    this.YinJinying = ds.Tables[0].Rows[0]["YinJinying"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Baopi"] != null)
                {
                    this.Baopi = ds.Tables[0].Rows[0]["Baopi"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GaoWanTIjiL"] != null)
                {
                    this.GaoWanTIjiL = ds.Tables[0].Rows[0]["GaoWanTIjiL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GaoWanTIjiR"] != null)
                {
                    this.GaoWanTIjiR = ds.Tables[0].Rows[0]["GaoWanTIjiR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GaoWanMMJ"] != null)
                {
                    this.GaoWanMMJ = ds.Tables[0].Rows[0]["GaoWanMMJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuWanJiejieL"] != null)
                {
                    this.FuWanJiejieL = ds.Tables[0].Rows[0]["FuWanJiejieL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuWanJiejieR"] != null)
                {
                    this.FuWanJiejieR = ds.Tables[0].Rows[0]["FuWanJiejieR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JSJMQZ"] != null)
                {
                    this.JSJMQZ = ds.Tables[0].Rows[0]["JSJMQZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JSJMQZ_buwei"] != null)
                {
                    this.JSJMQZ_buwei = ds.Tables[0].Rows[0]["JSJMQZ_buwei"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JSJMQZ_chengdu"] != null)
                {
                    this.JSJMQZ_chengdu = ds.Tables[0].Rows[0]["JSJMQZ_chengdu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WaiYin"] != null)
                {
                    this.WaiYin = ds.Tables[0].Rows[0]["WaiYin"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FenMiWu"] != null)
                {
                    this.FenMiWu = ds.Tables[0].Rows[0]["FenMiWu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZiGong"] != null)
                {
                    this.ZiGong = ds.Tables[0].Rows[0]["ZiGong"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuJian"] != null)
                {
                    this.FuJian = ds.Tables[0].Rows[0]["FuJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WaiYin_by"] != null)
                {
                    this.WaiYin_by = ds.Tables[0].Rows[0]["WaiYin_by"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YinDao_by"] != null)
                {
                    this.YinDao_by = ds.Tables[0].Rows[0]["YinDao_by"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Gongjing_by"] != null)
                {
                    this.Gongjing_by = ds.Tables[0].Rows[0]["Gongjing_by"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZiGong_by"] != null)
                {
                    this.ZiGong_by = ds.Tables[0].Rows[0]["ZiGong_by"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FuJian_bu"] != null)
                {
                    this.FuJian_bu = ds.Tables[0].Rows[0]["FuJian_bu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Qita2"] != null)
                {
                    this.Qita2 = ds.Tables[0].Rows[0]["Qita2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Doctor3"] != null)
                {
                    this.Doctor3 = ds.Tables[0].Rows[0]["Doctor3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["VisitJieGuo"] != null)
                {
                    this.VisitJieGuo = ds.Tables[0].Rows[0]["VisitJieGuo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YiChang"] != null)
                {
                    this.YiChang = ds.Tables[0].Rows[0]["YiChang"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JibingZhenduan"] != null)
                {
                    this.JibingZhenduan = ds.Tables[0].Rows[0]["JibingZhenduan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YiXueYiJian"] != null)
                {
                    this.YiXueYiJian = ds.Tables[0].Rows[0]["YiXueYiJian"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HunQianZIXun"] != null)
                {
                    this.HunQianZIXun = ds.Tables[0].Rows[0]["HunQianZIXun"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhiDaoJieGuo"] != null)
                {
                    this.ZhiDaoJieGuo = ds.Tables[0].Rows[0]["ZhiDaoJieGuo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhuanZhengYiYuan"] != null)
                {
                    this.ZhuanZhengYiYuan = ds.Tables[0].Rows[0]["ZhuanZhengYiYuan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZhuanZhengDate"] != null && ds.Tables[0].Rows[0]["ZhuanZhengDate"].ToString() != "")
                {
                    this.ZhuanZhengDate = DateTime.Parse(ds.Tables[0].Rows[0]["ZhuanZhengDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YuYueDate"] != null && ds.Tables[0].Rows[0]["YuYueDate"].ToString() != "")
                {
                    this.YuYueDate = DateTime.Parse(ds.Tables[0].Rows[0]["YuYueDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChuJuDate"] != null && ds.Tables[0].Rows[0]["ChuJuDate"].ToString() != "")
                {
                    this.ChuJuDate = DateTime.Parse(ds.Tables[0].Rows[0]["ChuJuDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Doctor4"] != null)
                {
                    this.Doctor4 = ds.Tables[0].Rows[0]["Doctor4"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TianxieDate"] != null && ds.Tables[0].Rows[0]["TianxieDate"].ToString() != "")
                {
                    this.TianxieDate = DateTime.Parse(ds.Tables[0].Rows[0]["TianxieDate"].ToString());
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
                if (ds.Tables[0].Rows[0]["UserAreaCode"] != null)
                {
                    this.UserAreaCode = ds.Tables[0].Rows[0]["UserAreaCode"].ToString();
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
            strSql.Append(" FROM [NHS_Hqyxjc] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
