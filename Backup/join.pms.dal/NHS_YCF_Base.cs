using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using UNV.Comm.DataBase;
using System.Data.SqlClient;

namespace join.pms.dal
{
    /// <summary>
    /// 类NHS_YCF_Base。
    /// </summary>
    [Serializable]
    public partial class NHS_YCF_Base
    {
        public NHS_YCF_Base()
        { }
        #region Model
        private string _unvid;
        private string _qcfwzbm;
        private string _fnname;
        private string _fncid;
        private string _fndeptment;
        private string _fnaddress;
        private string _fnaddresscode;
        private string _fntel;
        private DateTime? _visitdate;
        private string _tbyz;
        private int? _fnage;
        private string _zfname;
        private int? _zfage;
        private string _zftel;
        private string _zfcid;
        private int? _yunci = 0;
        private int? _chanci_yd = 0;
        private int? _chanci_pg = 0;
        private DateTime? _mociyj;
        private DateTime? _yuchanqi;
        private string _history_jw;
        private string _history_jz;
        private string _history_gr;
        private string _history_fkss;
        private string _history_yc;
        private int? _fnheight;
        private DateTime? _createdate = DateTime.Now;
        private int? _userid;
        private DateTime? _cqnextvisitdate;
        private int? _cqvisitcount;
        private DateTime? _chnextvisitdate;
        private int? _chvisitcount;
        private int? _fmvcount;
        private string _history_jw_other;
        private string _history_jz_other;
        private string _history_gr_other;
        private string _history_fkss_other;
        private string _history_yc_other;
        private DateTime? _lastvisitdate;
        private int? _lastvisititems;
        private string _ycfuserareacode;
        private string _minzu;
        private string _hzylzh;
        private string _xydz;
        private string _department;
        private string _childname;
        private string _zhiye;
        private string _wenhua;
        private string _regarea;
        private string _regareacode;
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
        public string FnDeptment
        {
            set { _fndeptment = value; }
            get { return _fndeptment; }
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
        public string FnTel
        {
            set { _fntel = value; }
            get { return _fntel; }
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
        public string TbYz
        {
            set { _tbyz = value; }
            get { return _tbyz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FnAge
        {
            set { _fnage = value; }
            get { return _fnage; }
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
        public int? ZfAge
        {
            set { _zfage = value; }
            get { return _zfage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZfTel
        {
            set { _zftel = value; }
            get { return _zftel; }
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
        public int? YunCi
        {
            set { _yunci = value; }
            get { return _yunci; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ChanCi_Yd
        {
            set { _chanci_yd = value; }
            get { return _chanci_yd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ChanCi_Pg
        {
            set { _chanci_pg = value; }
            get { return _chanci_pg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? MoCiYj
        {
            set { _mociyj = value; }
            get { return _mociyj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? YuChanQi
        {
            set { _yuchanqi = value; }
            get { return _yuchanqi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string History_Jw
        {
            set { _history_jw = value; }
            get { return _history_jw; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string History_Jz
        {
            set { _history_jz = value; }
            get { return _history_jz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string History_Gr
        {
            set { _history_gr = value; }
            get { return _history_gr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string History_Fkss
        {
            set { _history_fkss = value; }
            get { return _history_fkss; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string History_Yc
        {
            set { _history_yc = value; }
            get { return _history_yc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FnHeight
        {
            set { _fnheight = value; }
            get { return _fnheight; }
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
        public DateTime? CQNextVisitDate
        {
            set { _cqnextvisitdate = value; }
            get { return _cqnextvisitdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CQVisitCount
        {
            set { _cqvisitcount = value; }
            get { return _cqvisitcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CHNextVisitDate
        {
            set { _chnextvisitdate = value; }
            get { return _chnextvisitdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CHVisitCount
        {
            set { _chvisitcount = value; }
            get { return _chvisitcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FMVCount
        {
            set { _fmvcount = value; }
            get { return _fmvcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string History_Jw_other
        {
            set { _history_jw_other = value; }
            get { return _history_jw_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string History_Jz_other
        {
            set { _history_jz_other = value; }
            get { return _history_jz_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string History_Gr_other
        {
            set { _history_gr_other = value; }
            get { return _history_gr_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string History_Fkss_other
        {
            set { _history_fkss_other = value; }
            get { return _history_fkss_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string History_Yc_other
        {
            set { _history_yc_other = value; }
            get { return _history_yc_other; }
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
        public int? LastVisitItems
        {
            set { _lastvisititems = value; }
            get { return _lastvisititems; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string YCFUserAreaCode
        {
            set { _ycfuserareacode = value; }
            get { return _ycfuserareacode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MinZu
        {
            set { _minzu = value; }
            get { return _minzu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HZYLZH
        {
            set { _hzylzh = value; }
            get { return _hzylzh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XYDZ
        {
            set { _xydz = value; }
            get { return _xydz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
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
        public string Zhiye
        {
            set { _zhiye = value; }
            get { return _zhiye; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Wenhua
        {
            set { _wenhua = value; }
            get { return _wenhua; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RegArea
        {
            set { _regarea = value; }
            get { return _regarea; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RegAreaCode
        {
            set { _regareacode = value; }
            get { return _regareacode; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NHS_YCF_Base(string UnvID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UnvID,QcfwzBm,FnName,FnCID,FnDeptment,FnAddress,FnAddressCode,FnTel,VisitDate,TbYz,FnAge,ZfName,ZfAge,ZfTel,ZfCID,YunCi,ChanCi_Yd,ChanCi_Pg,MoCiYj,YuChanQi,History_Jw,History_Jz,History_Gr,History_Fkss,History_Yc,FnHeight,CreateDate,UserID,CQNextVisitDate,CQVisitCount,CHNextVisitDate,CHVisitCount,FMVCount,History_Jw_other,History_Jz_other,History_Gr_other,History_Fkss_other,History_Yc_other,LastVisitDate,LastVisitItems,YCFUserAreaCode,MinZu,HZYLZH,XYDZ,Department,ChildName,Zhiye,Wenhua,RegArea,RegAreaCode ");
            strSql.Append(" FROM [NHS_YCF_Base] ");
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
                if (ds.Tables[0].Rows[0]["FnName"] != null)
                {
                    this.FnName = ds.Tables[0].Rows[0]["FnName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnCID"] != null)
                {
                    this.FnCID = ds.Tables[0].Rows[0]["FnCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnDeptment"] != null)
                {
                    this.FnDeptment = ds.Tables[0].Rows[0]["FnDeptment"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnAddress"] != null)
                {
                    this.FnAddress = ds.Tables[0].Rows[0]["FnAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnAddressCode"] != null)
                {
                    this.FnAddressCode = ds.Tables[0].Rows[0]["FnAddressCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnTel"] != null)
                {
                    this.FnTel = ds.Tables[0].Rows[0]["FnTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["VisitDate"] != null && ds.Tables[0].Rows[0]["VisitDate"].ToString() != "")
                {
                    this.VisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["VisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TbYz"] != null)
                {
                    this.TbYz = ds.Tables[0].Rows[0]["TbYz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnAge"] != null && ds.Tables[0].Rows[0]["FnAge"].ToString() != "")
                {
                    this.FnAge = int.Parse(ds.Tables[0].Rows[0]["FnAge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZfName"] != null)
                {
                    this.ZfName = ds.Tables[0].Rows[0]["ZfName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZfAge"] != null && ds.Tables[0].Rows[0]["ZfAge"].ToString() != "")
                {
                    this.ZfAge = int.Parse(ds.Tables[0].Rows[0]["ZfAge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZfTel"] != null)
                {
                    this.ZfTel = ds.Tables[0].Rows[0]["ZfTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZfCID"] != null)
                {
                    this.ZfCID = ds.Tables[0].Rows[0]["ZfCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YunCi"] != null && ds.Tables[0].Rows[0]["YunCi"].ToString() != "")
                {
                    this.YunCi = int.Parse(ds.Tables[0].Rows[0]["YunCi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChanCi_Yd"] != null && ds.Tables[0].Rows[0]["ChanCi_Yd"].ToString() != "")
                {
                    this.ChanCi_Yd = int.Parse(ds.Tables[0].Rows[0]["ChanCi_Yd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChanCi_Pg"] != null && ds.Tables[0].Rows[0]["ChanCi_Pg"].ToString() != "")
                {
                    this.ChanCi_Pg = int.Parse(ds.Tables[0].Rows[0]["ChanCi_Pg"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MoCiYj"] != null && ds.Tables[0].Rows[0]["MoCiYj"].ToString() != "")
                {
                    this.MoCiYj = DateTime.Parse(ds.Tables[0].Rows[0]["MoCiYj"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YuChanQi"] != null && ds.Tables[0].Rows[0]["YuChanQi"].ToString() != "")
                {
                    this.YuChanQi = DateTime.Parse(ds.Tables[0].Rows[0]["YuChanQi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["History_Jw"] != null)
                {
                    this.History_Jw = ds.Tables[0].Rows[0]["History_Jw"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Jz"] != null)
                {
                    this.History_Jz = ds.Tables[0].Rows[0]["History_Jz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Gr"] != null)
                {
                    this.History_Gr = ds.Tables[0].Rows[0]["History_Gr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Fkss"] != null)
                {
                    this.History_Fkss = ds.Tables[0].Rows[0]["History_Fkss"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Yc"] != null)
                {
                    this.History_Yc = ds.Tables[0].Rows[0]["History_Yc"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnHeight"] != null && ds.Tables[0].Rows[0]["FnHeight"].ToString() != "")
                {
                    this.FnHeight = int.Parse(ds.Tables[0].Rows[0]["FnHeight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CQNextVisitDate"] != null && ds.Tables[0].Rows[0]["CQNextVisitDate"].ToString() != "")
                {
                    this.CQNextVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["CQNextVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CQVisitCount"] != null && ds.Tables[0].Rows[0]["CQVisitCount"].ToString() != "")
                {
                    this.CQVisitCount = int.Parse(ds.Tables[0].Rows[0]["CQVisitCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CHNextVisitDate"] != null && ds.Tables[0].Rows[0]["CHNextVisitDate"].ToString() != "")
                {
                    this.CHNextVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["CHNextVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CHVisitCount"] != null && ds.Tables[0].Rows[0]["CHVisitCount"].ToString() != "")
                {
                    this.CHVisitCount = int.Parse(ds.Tables[0].Rows[0]["CHVisitCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FMVCount"] != null && ds.Tables[0].Rows[0]["FMVCount"].ToString() != "")
                {
                    this.FMVCount = int.Parse(ds.Tables[0].Rows[0]["FMVCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["History_Jw_other"] != null)
                {
                    this.History_Jw_other = ds.Tables[0].Rows[0]["History_Jw_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Jz_other"] != null)
                {
                    this.History_Jz_other = ds.Tables[0].Rows[0]["History_Jz_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Gr_other"] != null)
                {
                    this.History_Gr_other = ds.Tables[0].Rows[0]["History_Gr_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Fkss_other"] != null)
                {
                    this.History_Fkss_other = ds.Tables[0].Rows[0]["History_Fkss_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Yc_other"] != null)
                {
                    this.History_Yc_other = ds.Tables[0].Rows[0]["History_Yc_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LastVisitDate"] != null && ds.Tables[0].Rows[0]["LastVisitDate"].ToString() != "")
                {
                    this.LastVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["LastVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastVisitItems"] != null && ds.Tables[0].Rows[0]["LastVisitItems"].ToString() != "")
                {
                    this.LastVisitItems = int.Parse(ds.Tables[0].Rows[0]["LastVisitItems"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YCFUserAreaCode"] != null)
                {
                    this.YCFUserAreaCode = ds.Tables[0].Rows[0]["YCFUserAreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MinZu"] != null)
                {
                    this.MinZu = ds.Tables[0].Rows[0]["MinZu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HZYLZH"] != null)
                {
                    this.HZYLZH = ds.Tables[0].Rows[0]["HZYLZH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XYDZ"] != null)
                {
                    this.XYDZ = ds.Tables[0].Rows[0]["XYDZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Department"] != null)
                {
                    this.Department = ds.Tables[0].Rows[0]["Department"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChildName"] != null)
                {
                    this.ChildName = ds.Tables[0].Rows[0]["ChildName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zhiye"] != null)
                {
                    this.Zhiye = ds.Tables[0].Rows[0]["Zhiye"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Wenhua"] != null)
                {
                    this.Wenhua = ds.Tables[0].Rows[0]["Wenhua"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegArea"] != null)
                {
                    this.RegArea = ds.Tables[0].Rows[0]["RegArea"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegAreaCode"] != null)
                {
                    this.RegAreaCode = ds.Tables[0].Rows[0]["RegAreaCode"].ToString();
                }
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string UnvID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [NHS_YCF_Base]");
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
            strSql.Append("select count(1) from [NHS_YCF_Base]");
            strSql.AppendFormat(" where QcfwzBm='{0}' ", QcfwzBm);
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
            strSql.Append("insert into [NHS_YCF_Base] (");
            strSql.Append("UnvID,QcfwzBm,FnName,FnCID,FnDeptment,FnAddress,FnAddressCode,FnTel,VisitDate,TbYz,FnAge,ZfName,ZfAge,ZfTel,ZfCID,YunCi,ChanCi_Yd,ChanCi_Pg,MoCiYj,YuChanQi,History_Jw,History_Jz,History_Gr,History_Fkss,History_Yc,FnHeight,CreateDate,UserID,CQNextVisitDate,CQVisitCount,CHNextVisitDate,CHVisitCount,FMVCount,History_Jw_other,History_Jz_other,History_Gr_other,History_Fkss_other,History_Yc_other,LastVisitDate,LastVisitItems,YCFUserAreaCode,MinZu,HZYLZH,XYDZ,Department,ChildName,Zhiye,Wenhua,RegArea,RegAreaCode)");
            strSql.Append(" values (");
            strSql.Append("@UnvID,@QcfwzBm,@FnName,@FnCID,@FnDeptment,@FnAddress,@FnAddressCode,@FnTel,@VisitDate,@TbYz,@FnAge,@ZfName,@ZfAge,@ZfTel,@ZfCID,@YunCi,@ChanCi_Yd,@ChanCi_Pg,@MoCiYj,@YuChanQi,@History_Jw,@History_Jz,@History_Gr,@History_Fkss,@History_Yc,@FnHeight,@CreateDate,@UserID,@CQNextVisitDate,@CQVisitCount,@CHNextVisitDate,@CHVisitCount,@FMVCount,@History_Jw_other,@History_Jz_other,@History_Gr_other,@History_Fkss_other,@History_Yc_other,@LastVisitDate,@LastVisitItems,@YCFUserAreaCode,@MinZu,@HZYLZH,@XYDZ,@Department,@ChildName,@Zhiye,@Wenhua,@RegArea,@RegAreaCode)");
            SqlParameter[] parameters = {
					new SqlParameter("@UnvID", SqlDbType.VarChar,50),
					new SqlParameter("@QcfwzBm", SqlDbType.VarChar,50),
					new SqlParameter("@FnName", SqlDbType.NVarChar,20),
					new SqlParameter("@FnCID", SqlDbType.VarChar,20),
					new SqlParameter("@FnDeptment", SqlDbType.NVarChar,50),
					new SqlParameter("@FnAddress", SqlDbType.NVarChar,90),
					new SqlParameter("@FnAddressCode", SqlDbType.VarChar,20),
					new SqlParameter("@FnTel", SqlDbType.VarChar,50),
					new SqlParameter("@VisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@TbYz", SqlDbType.VarChar,10),
					new SqlParameter("@FnAge", SqlDbType.TinyInt,1),
					new SqlParameter("@ZfName", SqlDbType.NVarChar,20),
					new SqlParameter("@ZfAge", SqlDbType.TinyInt,1),
					new SqlParameter("@ZfTel", SqlDbType.VarChar,50),
					new SqlParameter("@ZfCID", SqlDbType.VarChar,20),
					new SqlParameter("@YunCi", SqlDbType.TinyInt,1),
					new SqlParameter("@ChanCi_Yd", SqlDbType.TinyInt,1),
					new SqlParameter("@ChanCi_Pg", SqlDbType.TinyInt,1),
					new SqlParameter("@MoCiYj", SqlDbType.SmallDateTime),
					new SqlParameter("@YuChanQi", SqlDbType.SmallDateTime),
					new SqlParameter("@History_Jw", SqlDbType.VarChar,50),
					new SqlParameter("@History_Jz", SqlDbType.VarChar,50),
					new SqlParameter("@History_Gr", SqlDbType.VarChar,50),
					new SqlParameter("@History_Fkss", SqlDbType.VarChar,50),
					new SqlParameter("@History_Yc", SqlDbType.VarChar,50),
					new SqlParameter("@FnHeight", SqlDbType.SmallInt,2),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@CQNextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@CQVisitCount", SqlDbType.Int,4),
					new SqlParameter("@CHNextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@CHVisitCount", SqlDbType.Int,4),
					new SqlParameter("@FMVCount", SqlDbType.Int,4),
					new SqlParameter("@History_Jw_other", SqlDbType.VarChar,50),
					new SqlParameter("@History_Jz_other", SqlDbType.VarChar,50),
					new SqlParameter("@History_Gr_other", SqlDbType.VarChar,50),
					new SqlParameter("@History_Fkss_other", SqlDbType.VarChar,50),
					new SqlParameter("@History_Yc_other", SqlDbType.VarChar,50),
					new SqlParameter("@LastVisitDate", SqlDbType.DateTime),
					new SqlParameter("@LastVisitItems", SqlDbType.Int,4),
					new SqlParameter("@YCFUserAreaCode", SqlDbType.VarChar,20),
					new SqlParameter("@MinZu", SqlDbType.NVarChar,20),
					new SqlParameter("@HZYLZH", SqlDbType.NVarChar,50),
					new SqlParameter("@XYDZ", SqlDbType.NVarChar,50),
					new SqlParameter("@Department", SqlDbType.NVarChar,30),
					new SqlParameter("@ChildName", SqlDbType.NVarChar,50),
					new SqlParameter("@Zhiye", SqlDbType.VarChar,4),
					new SqlParameter("@Wenhua", SqlDbType.VarChar,4),
					new SqlParameter("@RegArea", SqlDbType.NVarChar,90),
					new SqlParameter("@RegAreaCode", SqlDbType.VarChar,20)};
            parameters[0].Value = UnvID;
            parameters[1].Value = QcfwzBm;
            parameters[2].Value = FnName;
            parameters[3].Value = FnCID;
            parameters[4].Value = FnDeptment;
            parameters[5].Value = FnAddress;
            parameters[6].Value = FnAddressCode;
            parameters[7].Value = FnTel;
            parameters[8].Value = VisitDate;
            parameters[9].Value = TbYz;
            parameters[10].Value = FnAge;
            parameters[11].Value = ZfName;
            parameters[12].Value = ZfAge;
            parameters[13].Value = ZfTel;
            parameters[14].Value = ZfCID;
            parameters[15].Value = YunCi;
            parameters[16].Value = ChanCi_Yd;
            parameters[17].Value = ChanCi_Pg;
            parameters[18].Value = MoCiYj;
            parameters[19].Value = YuChanQi;
            parameters[20].Value = History_Jw;
            parameters[21].Value = History_Jz;
            parameters[22].Value = History_Gr;
            parameters[23].Value = History_Fkss;
            parameters[24].Value = History_Yc;
            parameters[25].Value = FnHeight;
            parameters[26].Value = CreateDate;
            parameters[27].Value = UserID;
            parameters[28].Value = CQNextVisitDate;
            parameters[29].Value = CQVisitCount;
            parameters[30].Value = CHNextVisitDate;
            parameters[31].Value = CHVisitCount;
            parameters[32].Value = FMVCount;
            parameters[33].Value = History_Jw_other;
            parameters[34].Value = History_Jz_other;
            parameters[35].Value = History_Gr_other;
            parameters[36].Value = History_Fkss_other;
            parameters[37].Value = History_Yc_other;
            parameters[38].Value = LastVisitDate;
            parameters[39].Value = LastVisitItems;
            parameters[40].Value = YCFUserAreaCode;
            parameters[41].Value = MinZu;
            parameters[42].Value = HZYLZH;
            parameters[43].Value = XYDZ;
            parameters[44].Value = Department;
            parameters[45].Value = ChildName;
            parameters[46].Value = Zhiye;
            parameters[47].Value = Wenhua;
            parameters[48].Value = RegArea;
            parameters[49].Value = RegAreaCode;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [NHS_YCF_Base] set ");
            strSql.Append("QcfwzBm=@QcfwzBm,");
            strSql.Append("FnName=@FnName,");
            strSql.Append("FnCID=@FnCID,");
            strSql.Append("FnDeptment=@FnDeptment,");
            strSql.Append("FnAddress=@FnAddress,");
            strSql.Append("FnAddressCode=@FnAddressCode,");
            strSql.Append("FnTel=@FnTel,");
            strSql.Append("VisitDate=@VisitDate,");
            strSql.Append("TbYz=@TbYz,");
            strSql.Append("FnAge=@FnAge,");
            strSql.Append("ZfName=@ZfName,");
            strSql.Append("ZfAge=@ZfAge,");
            strSql.Append("ZfTel=@ZfTel,");
            strSql.Append("ZfCID=@ZfCID,");
            strSql.Append("YunCi=@YunCi,");
            strSql.Append("ChanCi_Yd=@ChanCi_Yd,");
            strSql.Append("ChanCi_Pg=@ChanCi_Pg,");
            strSql.Append("MoCiYj=@MoCiYj,");
            strSql.Append("YuChanQi=@YuChanQi,");
            strSql.Append("History_Jw=@History_Jw,");
            strSql.Append("History_Jz=@History_Jz,");
            strSql.Append("History_Gr=@History_Gr,");
            strSql.Append("History_Fkss=@History_Fkss,");
            strSql.Append("History_Yc=@History_Yc,");
            strSql.Append("FnHeight=@FnHeight,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("CQNextVisitDate=@CQNextVisitDate,");
            strSql.Append("CQVisitCount=@CQVisitCount,");
            strSql.Append("CHNextVisitDate=@CHNextVisitDate,");
            strSql.Append("CHVisitCount=@CHVisitCount,");
            strSql.Append("FMVCount=@FMVCount,");
            strSql.Append("History_Jw_other=@History_Jw_other,");
            strSql.Append("History_Jz_other=@History_Jz_other,");
            strSql.Append("History_Gr_other=@History_Gr_other,");
            strSql.Append("History_Fkss_other=@History_Fkss_other,");
            strSql.Append("History_Yc_other=@History_Yc_other,");
            strSql.Append("LastVisitDate=@LastVisitDate,");
            strSql.Append("LastVisitItems=@LastVisitItems,");
            strSql.Append("YCFUserAreaCode=@YCFUserAreaCode,");
            strSql.Append("MinZu=@MinZu,");
            strSql.Append("HZYLZH=@HZYLZH,");
            strSql.Append("XYDZ=@XYDZ,");
            strSql.Append("Department=@Department,");
            strSql.Append("ChildName=@ChildName,");
            strSql.Append("Zhiye=@Zhiye,");
            strSql.Append("Wenhua=@Wenhua,");
            strSql.Append("RegArea=@RegArea,");
            strSql.Append("RegAreaCode=@RegAreaCode");
            strSql.Append(" where UnvID=@UnvID ");
            SqlParameter[] parameters = {
					new SqlParameter("@QcfwzBm", SqlDbType.VarChar,50),
					new SqlParameter("@FnName", SqlDbType.NVarChar,20),
					new SqlParameter("@FnCID", SqlDbType.VarChar,20),
					new SqlParameter("@FnDeptment", SqlDbType.NVarChar,50),
					new SqlParameter("@FnAddress", SqlDbType.NVarChar,90),
					new SqlParameter("@FnAddressCode", SqlDbType.VarChar,20),
					new SqlParameter("@FnTel", SqlDbType.VarChar,50),
					new SqlParameter("@VisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@TbYz", SqlDbType.VarChar,10),
					new SqlParameter("@FnAge", SqlDbType.TinyInt,1),
					new SqlParameter("@ZfName", SqlDbType.NVarChar,20),
					new SqlParameter("@ZfAge", SqlDbType.TinyInt,1),
					new SqlParameter("@ZfTel", SqlDbType.VarChar,50),
					new SqlParameter("@ZfCID", SqlDbType.VarChar,20),
					new SqlParameter("@YunCi", SqlDbType.TinyInt,1),
					new SqlParameter("@ChanCi_Yd", SqlDbType.TinyInt,1),
					new SqlParameter("@ChanCi_Pg", SqlDbType.TinyInt,1),
					new SqlParameter("@MoCiYj", SqlDbType.SmallDateTime),
					new SqlParameter("@YuChanQi", SqlDbType.SmallDateTime),
					new SqlParameter("@History_Jw", SqlDbType.VarChar,50),
					new SqlParameter("@History_Jz", SqlDbType.VarChar,50),
					new SqlParameter("@History_Gr", SqlDbType.VarChar,50),
					new SqlParameter("@History_Fkss", SqlDbType.VarChar,50),
					new SqlParameter("@History_Yc", SqlDbType.VarChar,50),
					new SqlParameter("@FnHeight", SqlDbType.SmallInt,2),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@CQNextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@CQVisitCount", SqlDbType.Int,4),
					new SqlParameter("@CHNextVisitDate", SqlDbType.SmallDateTime),
					new SqlParameter("@CHVisitCount", SqlDbType.Int,4),
					new SqlParameter("@FMVCount", SqlDbType.Int,4),
					new SqlParameter("@History_Jw_other", SqlDbType.VarChar,50),
					new SqlParameter("@History_Jz_other", SqlDbType.VarChar,50),
					new SqlParameter("@History_Gr_other", SqlDbType.VarChar,50),
					new SqlParameter("@History_Fkss_other", SqlDbType.VarChar,50),
					new SqlParameter("@History_Yc_other", SqlDbType.VarChar,50),
					new SqlParameter("@LastVisitDate", SqlDbType.DateTime),
					new SqlParameter("@LastVisitItems", SqlDbType.Int,4),
					new SqlParameter("@YCFUserAreaCode", SqlDbType.VarChar,20),
					new SqlParameter("@MinZu", SqlDbType.NVarChar,20),
					new SqlParameter("@HZYLZH", SqlDbType.NVarChar,50),
					new SqlParameter("@XYDZ", SqlDbType.NVarChar,50),
					new SqlParameter("@Department", SqlDbType.NVarChar,30),
					new SqlParameter("@ChildName", SqlDbType.NVarChar,50),
					new SqlParameter("@Zhiye", SqlDbType.VarChar,4),
					new SqlParameter("@Wenhua", SqlDbType.VarChar,4),
					new SqlParameter("@RegArea", SqlDbType.NVarChar,90),
					new SqlParameter("@RegAreaCode", SqlDbType.VarChar,20),
					new SqlParameter("@UnvID", SqlDbType.VarChar,50)};
            parameters[0].Value = QcfwzBm;
            parameters[1].Value = FnName;
            parameters[2].Value = FnCID;
            parameters[3].Value = FnDeptment;
            parameters[4].Value = FnAddress;
            parameters[5].Value = FnAddressCode;
            parameters[6].Value = FnTel;
            parameters[7].Value = VisitDate;
            parameters[8].Value = TbYz;
            parameters[9].Value = FnAge;
            parameters[10].Value = ZfName;
            parameters[11].Value = ZfAge;
            parameters[12].Value = ZfTel;
            parameters[13].Value = ZfCID;
            parameters[14].Value = YunCi;
            parameters[15].Value = ChanCi_Yd;
            parameters[16].Value = ChanCi_Pg;
            parameters[17].Value = MoCiYj;
            parameters[18].Value = YuChanQi;
            parameters[19].Value = History_Jw;
            parameters[20].Value = History_Jz;
            parameters[21].Value = History_Gr;
            parameters[22].Value = History_Fkss;
            parameters[23].Value = History_Yc;
            parameters[24].Value = FnHeight;
            parameters[25].Value = CreateDate;
            parameters[26].Value = UserID;
            parameters[27].Value = CQNextVisitDate;
            parameters[28].Value = CQVisitCount;
            parameters[29].Value = CHNextVisitDate;
            parameters[30].Value = CHVisitCount;
            parameters[31].Value = FMVCount;
            parameters[32].Value = History_Jw_other;
            parameters[33].Value = History_Jz_other;
            parameters[34].Value = History_Gr_other;
            parameters[35].Value = History_Fkss_other;
            parameters[36].Value = History_Yc_other;
            parameters[37].Value = LastVisitDate;
            parameters[38].Value = LastVisitItems;
            parameters[39].Value = YCFUserAreaCode;
            parameters[40].Value = MinZu;
            parameters[41].Value = HZYLZH;
            parameters[42].Value = XYDZ;
            parameters[43].Value = Department;
            parameters[44].Value = ChildName;
            parameters[45].Value = Zhiye;
            parameters[46].Value = Wenhua;
            parameters[47].Value = RegArea;
            parameters[48].Value = RegAreaCode;
            parameters[49].Value = UnvID;

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
            strSql.Append("delete from [NHS_YCF_Base] ");
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
            strSql.Append("select UnvID,QcfwzBm,FnName,FnCID,FnDeptment,FnAddress,FnAddressCode,FnTel,VisitDate,TbYz,FnAge,ZfName,ZfAge,ZfTel,ZfCID,YunCi,ChanCi_Yd,ChanCi_Pg,MoCiYj,YuChanQi,History_Jw,History_Jz,History_Gr,History_Fkss,History_Yc,FnHeight,CreateDate,UserID,CQNextVisitDate,CQVisitCount,CHNextVisitDate,CHVisitCount,FMVCount,History_Jw_other,History_Jz_other,History_Gr_other,History_Fkss_other,History_Yc_other,LastVisitDate,LastVisitItems,YCFUserAreaCode,MinZu,HZYLZH,XYDZ,Department,ChildName,Zhiye,Wenhua,RegArea,RegAreaCode ");
            strSql.Append(" FROM [NHS_YCF_Base] ");
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
                if (ds.Tables[0].Rows[0]["FnName"] != null)
                {
                    this.FnName = ds.Tables[0].Rows[0]["FnName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnCID"] != null)
                {
                    this.FnCID = ds.Tables[0].Rows[0]["FnCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnDeptment"] != null)
                {
                    this.FnDeptment = ds.Tables[0].Rows[0]["FnDeptment"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnAddress"] != null)
                {
                    this.FnAddress = ds.Tables[0].Rows[0]["FnAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnAddressCode"] != null)
                {
                    this.FnAddressCode = ds.Tables[0].Rows[0]["FnAddressCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnTel"] != null)
                {
                    this.FnTel = ds.Tables[0].Rows[0]["FnTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["VisitDate"] != null && ds.Tables[0].Rows[0]["VisitDate"].ToString() != "")
                {
                    this.VisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["VisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TbYz"] != null)
                {
                    this.TbYz = ds.Tables[0].Rows[0]["TbYz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnAge"] != null && ds.Tables[0].Rows[0]["FnAge"].ToString() != "")
                {
                    this.FnAge = int.Parse(ds.Tables[0].Rows[0]["FnAge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZfName"] != null)
                {
                    this.ZfName = ds.Tables[0].Rows[0]["ZfName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZfAge"] != null && ds.Tables[0].Rows[0]["ZfAge"].ToString() != "")
                {
                    this.ZfAge = int.Parse(ds.Tables[0].Rows[0]["ZfAge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZfTel"] != null)
                {
                    this.ZfTel = ds.Tables[0].Rows[0]["ZfTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZfCID"] != null)
                {
                    this.ZfCID = ds.Tables[0].Rows[0]["ZfCID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["YunCi"] != null && ds.Tables[0].Rows[0]["YunCi"].ToString() != "")
                {
                    this.YunCi = int.Parse(ds.Tables[0].Rows[0]["YunCi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChanCi_Yd"] != null && ds.Tables[0].Rows[0]["ChanCi_Yd"].ToString() != "")
                {
                    this.ChanCi_Yd = int.Parse(ds.Tables[0].Rows[0]["ChanCi_Yd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChanCi_Pg"] != null && ds.Tables[0].Rows[0]["ChanCi_Pg"].ToString() != "")
                {
                    this.ChanCi_Pg = int.Parse(ds.Tables[0].Rows[0]["ChanCi_Pg"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MoCiYj"] != null && ds.Tables[0].Rows[0]["MoCiYj"].ToString() != "")
                {
                    this.MoCiYj = DateTime.Parse(ds.Tables[0].Rows[0]["MoCiYj"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YuChanQi"] != null && ds.Tables[0].Rows[0]["YuChanQi"].ToString() != "")
                {
                    this.YuChanQi = DateTime.Parse(ds.Tables[0].Rows[0]["YuChanQi"].ToString());
                }
                if (ds.Tables[0].Rows[0]["History_Jw"] != null)
                {
                    this.History_Jw = ds.Tables[0].Rows[0]["History_Jw"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Jz"] != null)
                {
                    this.History_Jz = ds.Tables[0].Rows[0]["History_Jz"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Gr"] != null)
                {
                    this.History_Gr = ds.Tables[0].Rows[0]["History_Gr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Fkss"] != null)
                {
                    this.History_Fkss = ds.Tables[0].Rows[0]["History_Fkss"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Yc"] != null)
                {
                    this.History_Yc = ds.Tables[0].Rows[0]["History_Yc"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnHeight"] != null && ds.Tables[0].Rows[0]["FnHeight"].ToString() != "")
                {
                    this.FnHeight = int.Parse(ds.Tables[0].Rows[0]["FnHeight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateDate"] != null && ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    this.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CQNextVisitDate"] != null && ds.Tables[0].Rows[0]["CQNextVisitDate"].ToString() != "")
                {
                    this.CQNextVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["CQNextVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CQVisitCount"] != null && ds.Tables[0].Rows[0]["CQVisitCount"].ToString() != "")
                {
                    this.CQVisitCount = int.Parse(ds.Tables[0].Rows[0]["CQVisitCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CHNextVisitDate"] != null && ds.Tables[0].Rows[0]["CHNextVisitDate"].ToString() != "")
                {
                    this.CHNextVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["CHNextVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CHVisitCount"] != null && ds.Tables[0].Rows[0]["CHVisitCount"].ToString() != "")
                {
                    this.CHVisitCount = int.Parse(ds.Tables[0].Rows[0]["CHVisitCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FMVCount"] != null && ds.Tables[0].Rows[0]["FMVCount"].ToString() != "")
                {
                    this.FMVCount = int.Parse(ds.Tables[0].Rows[0]["FMVCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["History_Jw_other"] != null)
                {
                    this.History_Jw_other = ds.Tables[0].Rows[0]["History_Jw_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Jz_other"] != null)
                {
                    this.History_Jz_other = ds.Tables[0].Rows[0]["History_Jz_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Gr_other"] != null)
                {
                    this.History_Gr_other = ds.Tables[0].Rows[0]["History_Gr_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Fkss_other"] != null)
                {
                    this.History_Fkss_other = ds.Tables[0].Rows[0]["History_Fkss_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["History_Yc_other"] != null)
                {
                    this.History_Yc_other = ds.Tables[0].Rows[0]["History_Yc_other"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LastVisitDate"] != null && ds.Tables[0].Rows[0]["LastVisitDate"].ToString() != "")
                {
                    this.LastVisitDate = DateTime.Parse(ds.Tables[0].Rows[0]["LastVisitDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastVisitItems"] != null && ds.Tables[0].Rows[0]["LastVisitItems"].ToString() != "")
                {
                    this.LastVisitItems = int.Parse(ds.Tables[0].Rows[0]["LastVisitItems"].ToString());
                }
                if (ds.Tables[0].Rows[0]["YCFUserAreaCode"] != null)
                {
                    this.YCFUserAreaCode = ds.Tables[0].Rows[0]["YCFUserAreaCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MinZu"] != null)
                {
                    this.MinZu = ds.Tables[0].Rows[0]["MinZu"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HZYLZH"] != null)
                {
                    this.HZYLZH = ds.Tables[0].Rows[0]["HZYLZH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["XYDZ"] != null)
                {
                    this.XYDZ = ds.Tables[0].Rows[0]["XYDZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Department"] != null)
                {
                    this.Department = ds.Tables[0].Rows[0]["Department"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChildName"] != null)
                {
                    this.ChildName = ds.Tables[0].Rows[0]["ChildName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Zhiye"] != null)
                {
                    this.Zhiye = ds.Tables[0].Rows[0]["Zhiye"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Wenhua"] != null)
                {
                    this.Wenhua = ds.Tables[0].Rows[0]["Wenhua"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegArea"] != null)
                {
                    this.RegArea = ds.Tables[0].Rows[0]["RegArea"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RegAreaCode"] != null)
                {
                    this.RegAreaCode = ds.Tables[0].Rows[0]["RegAreaCode"].ToString();
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
            strSql.Append(" FROM [NHS_YCF_Base] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
