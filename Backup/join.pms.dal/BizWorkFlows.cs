using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using UNV.Comm.DataBase;

namespace join.pms.dal
{
    public class BizWorkFlows
    {
        ~BizWorkFlows() { }

        public string WorkFlowsID;
        public string BizStepTotal;
        //public string BizStep;
        public string AreaCode;
        public string AreaName;
        public string Comments;
        public string ApprovalUserID;
        public string Approval;
        public string ApprovalSignName;
        public string Signature;
        public string Attribs;
        public string CreateDate;
        public string OprateDate;

        public string ApprovalUserTel;
        public string CertificateNoA;
        public string CertificateNoB;
        public string CertificateMemo;
        public string QRCodeFiles;

        public string CertificateDateStart;
        public string CertificateDateEnd;

        // CertificateDateStart,CertificateDateEnd
        // ApprovalUserTel,CertificateNoA,CertificateNoB,CertificateMemo
        // "SELECT AreaName,Comments,Approval,ApprovalUserTel,Signature,OprateDate FROM BIZ_WorkFlows WHERE BizID=" + bizID + " ORDER BY BizStep";
        // CommID,BizStepTotal,BizStep,AreaCode,AreaName,Comments,ApprovalUserID,Approval,Signature,Attribs,CreateDate,OprateDate
        // 业务编号
        private string _BizID;
        public string BizID
        {
            set { _BizID = value; }
            get { return _BizID; }
        }
        // 业务编码
        private string _BizCode;
        public string BizCode
        {
            set { _BizCode = value; }
            get { return _BizCode; }
        }
        // 业务步骤
        private string _BizStep;
        public string BizStep
        {
            set { _BizStep = value; }
            get { return _BizStep; }
        }

        // 过滤条件
        private string _FilterSQL;
        public string FilterSQL
        {
            set { _FilterSQL = value; }
            get { return _FilterSQL; }
        }

        private string m_SqlParams;
        private DataTable m_Dt;

        #region 获取业务流程图
        /// <summary>
        /// 获取业务流程信息
        /// </summary>
        /// <param name="bizID"></param>
        public string GetBizFlows()
        {
            string Comments,CommMemo, BizComments = string.Empty;
            string Attribs = string.Empty;
            string areaA = "", areaB = string.Empty;
            StringBuilder s = new StringBuilder();
            int stepTotal = 0;
            int f = 0;
            try
            {
                m_SqlParams = "SELECT BizStep,AreaCode,AreaName,Comments,Approval,Signature,CreateDate,OprateDate,Attribs,AuditUser,ApprovalSignName,AuditUserSignName,CommMemo FROM BIZ_WorkFlows WHERE BizID=" + this.BizID + " ORDER BY BizStep";
                m_Dt = new DataTable();
                m_Dt = DbHelperSQL.Query(m_SqlParams).Tables[0];
                stepTotal = m_Dt.Rows.Count;
                if (m_Dt.Rows.Count > 0)
                {
                    areaA = m_Dt.Rows[0][1].ToString();
                    //areaB = m_Dt.Rows[1][1].ToString();
                    for (int i = 0; i < stepTotal; i++)
                    {
                        f++;
                        // BIZ_WorkFlows Attribs:0,驳回；1,通过；9 默认未处理
                        // BIZ_Contents Attribs: 0,初始提交;1,审核中 2,通过 3,补正 4,撤销 5,注销 6,等待审核,9,归档
                        Comments = m_Dt.Rows[i][3].ToString();
                        CommMemo = m_Dt.Rows[i]["CommMemo"].ToString();//需要什么材料
                        Attribs = m_Dt.Rows[i]["Attribs"].ToString(); 

                        if (Attribs == "1")
                        {
                            //审核通过
                            s.Append("<li class=\"pass\">");
                            s.Append("<div class=\"flow_bg\">");
                            s.Append("<p class=\"number\">" + f.ToString() + "</p>");
                            // 同村增加男女识别
                            if (this.BizCode == "0101" || this.BizCode == "0102" || this.BizCode == "0106" || this.BizCode == "0108" || this.BizCode == "0109" || this.BizCode == "0122")
                            {
                                areaB = m_Dt.Rows[1][1].ToString();
                                if (areaA == areaB)
                                {
                                    if (f == 1) { s.Append("<p class=\"section\">" + m_Dt.Rows[i][2].ToString() + "(女方)</p>"); }
                                    else if (f == 2) { s.Append("<p class=\"section\">" + m_Dt.Rows[i][2].ToString() + "(男方)</p>"); }
                                    else { s.Append("<p class=\"section\">" + m_Dt.Rows[i][2].ToString() + "</p>"); }
                                }
                                else { s.Append("<p class=\"section\">" + m_Dt.Rows[i][2].ToString() + "</p>"); }
                            }
                            else{s.Append("<p class=\"section\">" + m_Dt.Rows[i][2].ToString() + "</p>");}
                            /*
0101 一孩 镇办3
0102 二孩 镇办3
0107 一杯奶 镇办2
0111 终止妊娠 旗6/旗3
                             * 
                           
                            */
                            //存在双重审核的情况
                            if (this.BizCode == "0101" || this.BizCode == "0108")
                            {
                                if (f == 3) { s.Append("<p class=\"name2\">经办人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "；<br />责任人:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">审核人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0102" || this.BizCode == "0106" || this.BizCode == "0122")
                            {
                                if (f == 3 || f == 4) { s.Append("<p class=\"name2\">经办人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "；<br />责任人:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">审核人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0103" || this.BizCode == "0104" || this.BizCode == "0105") {
                                if (f == 2 || f == 3) { s.Append("<p class=\"name2\">经办人:" + m_Dt.Rows[i][4].ToString() + "；<br />责任人:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">审核人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0107") {
                                if (f == 2) { s.Append("<p class=\"name2\">经办人:" + m_Dt.Rows[i][4].ToString() + "；<br />责任人:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">审核人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0111") {
                                if (stepTotal == 6 && f == 6) { s.Append("<p class=\"name2\">经办人:" + m_Dt.Rows[i][4].ToString() + "；<br />责任人:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else if (stepTotal == 3 && f == 3) { s.Append("<p class=\"name2\">经办人:" + m_Dt.Rows[i][4].ToString() + "；<br />责任人:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">审核人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else { s.Append("<p class=\"name\">审核人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }

                            
                            s.Append("<p class=\"view\">处理意见：</p>");
                            s.Append("<p class=\"sum\">" + Comments + "</p>");
                            s.Append("</div>");
                            if (i != stepTotal - 1) s.Append("<p class=\"flow_ico\"></p>");
                            s.Append("</li>");
                        }
                        else if (Attribs == "0")
                        {
                            //驳回
                            s.Append("<li class=\"unpass\">");
                            s.Append("<div class=\"flow_bg\">");
                            s.Append("<p class=\"number\">" + f.ToString() + "</p>");
                            s.Append("<p class=\"section\">" + m_Dt.Rows[i][2].ToString() + "</p>");
                            //s.Append("<p class=\"name\">审核人:" + m_Dt.Rows[i][4].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>");
                            //存在双重审核的情况
                            if (this.BizCode == "0101" || this.BizCode == "0108")
                            {
                                if (f == 3) { s.Append("<p class=\"name2\">经办人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "；<br />责任人:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">审核人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0102" || this.BizCode == "0106" || this.BizCode == "0122")
                            {
                                if (f == 3 || f == 4) { s.Append("<p class=\"name2\">经办人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "；<br />责任人:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">审核人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0103" || this.BizCode == "0104" || this.BizCode == "0105")
                            {
                                if (f == 2 || f == 3) { s.Append("<p class=\"name2\">经办人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "；<br />责任人:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">审核人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0107")
                            {
                                if (f == 2) { s.Append("<p class=\"name2\">经办人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "；<br />责任人:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">审核人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0111")
                            {
                                if (stepTotal == 6 && f == 6) { s.Append("<p class=\"name2\">经办人:" + m_Dt.Rows[i][4].ToString() + "；<br />责任人:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else if (stepTotal == 3 && f == 3) { s.Append("<p class=\"name2\">经办人:" + m_Dt.Rows[i][4].ToString() + "；<br />责任人:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">审核人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else { s.Append("<p class=\"name\">审核人:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />时间:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            s.Append("<p class=\"view\">处理意见：</p>");
                            string CommMemostr="";
                            if (!string.IsNullOrEmpty(CommMemo))
                            {
                                CommMemostr = "<br>需补充提交的材料如下：";
                                string[] aryDocs = null;
                                aryDocs = CommMemo.Split(',');
                                    for (int k = 0; k < aryDocs.Length; k++)
                                    {
                                        CommMemostr+="<br>" + (k + 1).ToString() + "、" + aryDocs[k];
                                    }
                            }
                            s.Append("<p class=\"sum\">" + Comments + CommMemostr + "</p>");

                            s.Append("</div>");
                            if (i != stepTotal - 1) s.Append("<p class=\"flow_ico\"></p>");
                            s.Append("</li>");
                        }
                        else
                        {
                            s.Append("<li>");
                            s.Append("<div class=\"flow_bg\">");
                            s.Append("<p class=\"number\">" + f.ToString() + "</p>");
                            s.Append("<p class=\"section\">" + m_Dt.Rows[i][2].ToString() + "</p>");
                            //s.Append("<p class=\"name\">审核人:<br />时间:</p>");
                            /*
0103 奖扶 旗镇两级2,3
0104 特扶 旗镇两级2,3
0105 少生快富 旗镇两级 2,3
0106 结扎奖励  旗镇两级 3,4
0108 独子证 镇级 3
            */
                            //存在双重审核的情况
                            if (this.BizCode == "0101" || this.BizCode == "0108")
                            {
                                if (f == 3) { s.Append("<p class=\"name2\">审核人:<br />责任人:<br />时间:</p>"); }
                                else { s.Append("<p class=\"name\">审核人:<br />时间:</p>"); }
                            }
                            else if (this.BizCode == "0102" || this.BizCode == "0106" || this.BizCode == "0122")
                            {
                                if (f == 3 || f == 4) { s.Append("<p class=\"name2\">审核人:<br />责任人:<br />时间:</p>"); }
                                else { s.Append("<p class=\"name\">审核人:<br />时间:</p>"); }
                            }
                            else if (this.BizCode == "0103" || this.BizCode == "0104" || this.BizCode == "0105")
                            {
                                if (f == 2 || f == 3) { s.Append("<p class=\"name2\">审核人:<br />责任人:<br />时间:</p>"); }
                                else { s.Append("<p class=\"name\">审核人:<br />时间:</p>"); }
                            }
                            else if (this.BizCode == "0107")
                            {
                                if (f == 2) { s.Append("<p class=\"name2\">审核人:<br />责任人:<br />时间:</p>"); }
                                else { s.Append("<p class=\"name\">审核人:<br />时间:</p>"); }
                            }
                            else if (this.BizCode == "0111")
                            {
                                if (stepTotal == 6 && f == 6) { s.Append("<p class=\"name2\">审核人:<br />责任人:<br />时间:</p>"); }
                                else if (stepTotal == 3 && f == 3) { s.Append("<p class=\"name2\">审核人:<br />责任人:<br />时间:</p>"); }
                                else { s.Append("<p class=\"name\">审核人:<br />时间:</p>"); }
                            }
                            else { s.Append("<p class=\"name\">审核人:<br />时间:</p>"); }
                            s.Append("<p class=\"view\">处理意见：</p>");
                            s.Append("<p class=\"sum\">……</p>");
                            s.Append("</div>");
                            if (i != stepTotal - 1) s.Append("<p class=\"flow_ico\"></p>");
                            s.Append("</li>");
                        }
                    }
                }
                m_Dt = null;

            }
            catch (Exception ex) { s.Append(ex.Message); }
            m_Dt = null;

            return s.ToString();

        }

        #endregion

        /// <summary>
        /// 获取证件号码
        /// </summary>
        /// <param name="bizCode"></param>
        /// <param name="areaCode"></param>
        /// <param name="childNum"></param>
        public string GetCertificateNo(string bizCode, string areaCode, string childNum)
        {
            /*
            证件编号.详情按照以下规则:
            按照单位区划+年度+4位流水号,例如:A15052220020150001
            一孩:Y 二孩:Z 家庭奖扶:J 独生子女光荣证:D 婚育证明:H
            
            a,婚育证编号：行政区划+年度+流水号 有效期3年
            SELECT COUNT() FROM BIZ_Certificates WHERE BizCode='0101' AND CreateDate >'2015/01/01 00:00:00' AND CreateDate <'2016/01/01 00:00:00'
            b,生育证，签章处加一行[补正] 预产期+[补正] ,预产期不是必填项,
            生育证编号：区划代码9+年度4+孩次1+流水号(4)
            c,补正通知书：年度+3位流水
             */
            /*
           0101	一孩生育登记 3 
           0102	二孩生育登记 4
           0103	奖励扶助 3
           0108	独生子女父母光荣证 3
           0109	《流动人口婚育证明》 3
            */
            string returnVal = string.Empty;
            string flowNo = string.Empty;
            string cYear = string.Empty, nYear = string.Empty;
            if (bizCode == "0101")
            {
                returnVal = "Y";
            }
            else if (bizCode == "0102")
            {
                returnVal = "Z";
            }
            else if (bizCode == "0103")
            {
                returnVal = "J";
            }
            else if (bizCode == "0108")
            {
                returnVal = "D";
            }
            else if (bizCode == "0109")
            {
                returnVal = "H";
            }
            else if (bizCode == "0122")
            {
                returnVal = "Z";
            }
            else if (bizCode == "0131")
            {
                returnVal = "Q";
            }
            else if (bizCode == "0150")
            {
                returnVal = "";
            }
            if (bizCode == "0101" || bizCode == "0102" || bizCode == "0103" || bizCode == "0108" || bizCode == "0109" || bizCode == "0122" || bizCode == "0131" || bizCode == "0150")
            {
                cYear = DateTime.Now.ToString("yyyy");
                nYear = DateTime.Now.AddYears(1).ToString("yyyy");
                flowNo = CommPage.GetSingleVal("SELECT COUNT(*)+1 FROM BIZ_Certificates WHERE BizCode='" + bizCode + "' AND AreaCode LIKE '" + areaCode.Substring(0, 9) + "%' AND CertificateType=2 AND CreateDate >'" + cYear + "/01/01 00:00:00' AND CreateDate <'" + nYear + "/01/01 00:00:00'");
                if (bizCode == "0150")
                {
                    //6109220001610001
                    //镇区划9位+年份2位+夫妻双方领取服务证数量，第二次为2…… 
                    returnVal = areaCode.Substring(0, 9) + cYear.Substring(2) + childNum + flowNo.PadLeft(4, '0');
                }
                else
                {
                    returnVal = returnVal + areaCode.Substring(0, 9) + cYear + bizCode.Substring(bizCode.Length - 1) + flowNo.PadLeft(4, '0');
                }
            }
            //else if (bizCode == "0102")
            //{
            //    cYear = DateTime.Now.ToString("yyyy");
            //    nYear = DateTime.Now.AddYears(1).ToString("yyyy");
            //    flowNo = CommPage.GetSingleVal("SELECT COUNT(*)+1 FROM BIZ_Certificates WHERE BizCode='" + bizCode + "' AND AreaCode LIKE '" + areaCode.Substring(0, 9) + "%' AND CertificateType=2 AND CreateDate >'" + cYear + "/01/01 00:00:00' AND CreateDate <'" + nYear + "/01/01 00:00:00'");
            //    returnVal = areaCode.Substring(0, 9) + cYear + "1" + flowNo.PadLeft(4, '0');
            //}
            //else if (bizCode == "0103")
            //{
            //    if (!string.IsNullOrEmpty(childNum))
            //    {
            //        childNum = childNum.Trim();
            //    }
            //    else
            //    {
            //        childNum = "2";
            //    }
            //    cYear = DateTime.Now.ToString("yyyy");
            //    nYear = DateTime.Now.AddYears(1).ToString("yyyy");
            //    flowNo = CommPage.GetSingleVal("SELECT COUNT(*)+1 FROM BIZ_Certificates WHERE BizCode='" + bizCode + "' AND AreaCode LIKE '" + areaCode.Substring(0, 6) + "%' AND CertificateType=2 AND CreateDate >'" + cYear + "/01/01 00:00:00' AND CreateDate <'" + nYear + "/01/01 00:00:00'");
            //    returnVal = areaCode.Substring(0, 6) + "000" + cYear + childNum + flowNo.PadLeft(4, '0');
            //}
            //else if (bizCode == "0104") { }
            //else if (bizCode == "0112") { }
            else { }
            return GetCertificateNos(returnVal, 0);
        }

        public string GetCertificateNos(string returnVal, int onNUM)
        {
            string CertificateNoA_t6 = CommPage.GetSingleVal("SELECT count(*) FROM BIZ_WorkFlows WHERE CertificateNoA='" + returnVal + "'");
            if (int.Parse(CertificateNoA_t6) > 0)
            {
                onNUM = onNUM + 1;
                returnVal = returnVal.Substring(0, returnVal.Length - 4) + Convert.ToString(int.Parse(returnVal.Substring(returnVal.Length - 4)) + onNUM).PadLeft(4, '0');
                GetCertificateNos(returnVal, onNUM);
            }
            return returnVal;
        }
        /// <summary>
        /// 获取当前业务的流程信息
        /// </summary>
        public void GetWorkFlowsByBizID()
        {
            if (!string.IsNullOrEmpty(this.BizID))
            {
                // IsInnerArea: 0辖区内; 1辖区外
                SqlDataReader sdr = null;
                string sqlParams = string.Empty;
                if (!string.IsNullOrEmpty(FilterSQL))
                {
                    sqlParams = "SELECT CommID,BizStepTotal,BizStep,AreaCode,AreaName,Comments,ApprovalUserID,Approval,Signature,Attribs,CreateDate,OprateDate,ApprovalUserTel,CertificateNoA,CertificateNoB,CertificateMemo,QRCodeFiles,CertificateDateStart,CertificateDateEnd,ApprovalSignName FROM BIZ_WorkFlows WHERE " + FilterSQL;
                }
                else {
                    sqlParams = "SELECT TOP 1 CommID,BizStepTotal,BizStep,AreaCode,AreaName,Comments,ApprovalUserID,Approval,Signature,Attribs,CreateDate,OprateDate,CertificateDateStart,CertificateDateEnd FROM BIZ_WorkFlows WHERE BizID=" + this.BizID + "   AND Attribs=9 AND IsInnerArea=0 ORDER BY BizStep";
                }
                try
                {
                    sdr = DbHelperSQL.ExecuteReader(sqlParams);
                    while (sdr.Read())
                    {
                        WorkFlowsID = CommBiz.GetTrim(sdr[0].ToString());
                        BizStepTotal = CommBiz.GetTrim(sdr[1].ToString());
                        BizStep = CommBiz.GetTrim(sdr[2].ToString());
                        AreaCode = CommBiz.GetTrim(sdr[3].ToString());
                        AreaName = CommBiz.GetTrim(sdr[4].ToString());

                        Comments = CommBiz.GetTrim(sdr[5].ToString());
                        ApprovalUserID = CommBiz.GetTrim(sdr[6].ToString());
                        Approval = CommBiz.GetTrim(sdr[7].ToString());
                        Signature = CommBiz.GetTrim(sdr[8].ToString());
                        Attribs = CommBiz.GetTrim(sdr[9].ToString());

                        CreateDate = CommBiz.GetTrim(sdr[10].ToString());
                        OprateDate = CommBiz.GetTrim(sdr[11].ToString());
                        // ApprovalUserTel,CertificateNoA,CertificateNoB,CertificateMemo QRCodeFiles
                        if (!string.IsNullOrEmpty(FilterSQL)){
                            ApprovalUserTel = CommBiz.GetTrim(sdr[12].ToString());
                            CertificateNoA = CommBiz.GetTrim(sdr[13].ToString());
                            CertificateNoB = CommBiz.GetTrim(sdr[14].ToString());
                            CertificateMemo = CommBiz.GetTrim(sdr[15].ToString());
                            QRCodeFiles = CommBiz.GetTrim(sdr[16].ToString());
                            if (!string.IsNullOrEmpty(sdr[17].ToString())) CertificateDateStart = DateTime.Parse(sdr[17].ToString()).ToString("yyyy/MM/dd");
                            if (!string.IsNullOrEmpty(sdr[18].ToString())) CertificateDateEnd = DateTime.Parse(sdr[18].ToString()).ToString("yyyy/MM/dd");
                            ApprovalSignName = CommBiz.GetTrim(sdr[19].ToString());
                        }
                            
                        
                    }
                    sdr.Close();
                }
                catch { if (sdr != null) sdr.Close(); }
            }
        }
        /// <summary>
        /// 设置二维码
        /// </summary>
        /// <param name="workFlowID"></param>
        /// <param name="qrFiles"></param>
        public void SettWorkFlowsQrCode(string workFlowID,string qrFiles) {
            try
            {
                m_SqlParams = "UPDATE BIZ_WorkFlows SET QRCodeFiles='" + qrFiles + "' WHERE CommID=" + workFlowID;
                DbHelperSQL.ExecuteSql(m_SqlParams);
            }
            catch { }
            
        }

        public void SetCertificateLog(string BizName, string pName, string pCid, string certificateName)
        {
            SqlDataReader sdr = null;
            try
            {
                m_SqlParams = "SELECT TOP 1 AreaCode,AreaName,DeptName,Approval,CertificateNoA,CertificateNoB,Attribs,CertificateDateStart,CertificateDateEnd FROM BIZ_WorkFlows WHERE BizID=" + this.BizID + " ORDER BY BizStep DESC";
                sdr = DbHelperSQL.ExecuteReader(m_SqlParams);
                while (sdr.Read())
                {
                    string CertificateNo = sdr["CertificateNoA"].ToString();
                    //string CertificateName = "流动人口婚育证明申请表";
                    string CertificateGovName = sdr["DeptName"].ToString();
                    string StartDate = sdr["CertificateDateStart"].ToString();
                    string AreaCode = sdr["AreaCode"].ToString();

                    m_SqlParams = "SELECT COUNT(*) FROM BIZ_Certificates WHERE CertificateType=1 AND BizCode='" + this.BizCode + "' AND BizID=" + this.BizID;
                    if (CommPage.GetSingleVal(m_SqlParams) == "0")
                    {
                        m_SqlParams = "INSERT INTO BIZ_Certificates (";
                        m_SqlParams += "BizID,BizCode,BizName,CertificateNo,CertificateName,AreaCode,";
                        m_SqlParams += "PersonName,PersonCid,CertificateGovName,StartDate,CertificateType";
                        m_SqlParams += ") VALUES(";
                        m_SqlParams += "" + this.BizID + ",'" + this.BizCode + "','" + BizName + "','-','" + certificateName + "','" + AreaCode + "',";
                        m_SqlParams += "'" + pName + "','" + pCid + "','" + CertificateGovName + "','" + StartDate + "',1";
                        m_SqlParams += ")";

                        DbHelperSQL.ExecuteSql(m_SqlParams);
                    }

                }
                sdr.Close();
            }
            catch
            {
                if (sdr != null) sdr.Close();
            }
        }
    }
}
