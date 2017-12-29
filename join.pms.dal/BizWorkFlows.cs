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
        // ҵ����
        private string _BizID;
        public string BizID
        {
            set { _BizID = value; }
            get { return _BizID; }
        }
        // ҵ�����
        private string _BizCode;
        public string BizCode
        {
            set { _BizCode = value; }
            get { return _BizCode; }
        }
        // ҵ����
        private string _BizStep;
        public string BizStep
        {
            set { _BizStep = value; }
            get { return _BizStep; }
        }

        // ��������
        private string _FilterSQL;
        public string FilterSQL
        {
            set { _FilterSQL = value; }
            get { return _FilterSQL; }
        }

        private string m_SqlParams;
        private DataTable m_Dt;

        #region ��ȡҵ������ͼ
        /// <summary>
        /// ��ȡҵ��������Ϣ
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
                        // BIZ_WorkFlows Attribs:0,���أ�1,ͨ����9 Ĭ��δ����
                        // BIZ_Contents Attribs: 0,��ʼ�ύ;1,����� 2,ͨ�� 3,���� 4,���� 5,ע�� 6,�ȴ����,9,�鵵
                        Comments = m_Dt.Rows[i][3].ToString();
                        CommMemo = m_Dt.Rows[i]["CommMemo"].ToString();//��Ҫʲô����
                        Attribs = m_Dt.Rows[i]["Attribs"].ToString(); 

                        if (Attribs == "1")
                        {
                            //���ͨ��
                            s.Append("<li class=\"pass\">");
                            s.Append("<div class=\"flow_bg\">");
                            s.Append("<p class=\"number\">" + f.ToString() + "</p>");
                            // ͬ��������Ůʶ��
                            if (this.BizCode == "0101" || this.BizCode == "0102" || this.BizCode == "0106" || this.BizCode == "0108" || this.BizCode == "0109" || this.BizCode == "0122")
                            {
                                areaB = m_Dt.Rows[1][1].ToString();
                                if (areaA == areaB)
                                {
                                    if (f == 1) { s.Append("<p class=\"section\">" + m_Dt.Rows[i][2].ToString() + "(Ů��)</p>"); }
                                    else if (f == 2) { s.Append("<p class=\"section\">" + m_Dt.Rows[i][2].ToString() + "(�з�)</p>"); }
                                    else { s.Append("<p class=\"section\">" + m_Dt.Rows[i][2].ToString() + "</p>"); }
                                }
                                else { s.Append("<p class=\"section\">" + m_Dt.Rows[i][2].ToString() + "</p>"); }
                            }
                            else{s.Append("<p class=\"section\">" + m_Dt.Rows[i][2].ToString() + "</p>");}
                            /*
0101 һ�� ���3
0102 ���� ���3
0107 һ���� ���2
0111 ��ֹ���� ��6/��3
                             * 
                           
                            */
                            //����˫����˵����
                            if (this.BizCode == "0101" || this.BizCode == "0108")
                            {
                                if (f == 3) { s.Append("<p class=\"name2\">������:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "��<br />������:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">�����:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0102" || this.BizCode == "0106" || this.BizCode == "0122")
                            {
                                if (f == 3 || f == 4) { s.Append("<p class=\"name2\">������:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "��<br />������:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">�����:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0103" || this.BizCode == "0104" || this.BizCode == "0105") {
                                if (f == 2 || f == 3) { s.Append("<p class=\"name2\">������:" + m_Dt.Rows[i][4].ToString() + "��<br />������:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">�����:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0107") {
                                if (f == 2) { s.Append("<p class=\"name2\">������:" + m_Dt.Rows[i][4].ToString() + "��<br />������:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">�����:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0111") {
                                if (stepTotal == 6 && f == 6) { s.Append("<p class=\"name2\">������:" + m_Dt.Rows[i][4].ToString() + "��<br />������:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else if (stepTotal == 3 && f == 3) { s.Append("<p class=\"name2\">������:" + m_Dt.Rows[i][4].ToString() + "��<br />������:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">�����:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else { s.Append("<p class=\"name\">�����:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }

                            
                            s.Append("<p class=\"view\">���������</p>");
                            s.Append("<p class=\"sum\">" + Comments + "</p>");
                            s.Append("</div>");
                            if (i != stepTotal - 1) s.Append("<p class=\"flow_ico\"></p>");
                            s.Append("</li>");
                        }
                        else if (Attribs == "0")
                        {
                            //����
                            s.Append("<li class=\"unpass\">");
                            s.Append("<div class=\"flow_bg\">");
                            s.Append("<p class=\"number\">" + f.ToString() + "</p>");
                            s.Append("<p class=\"section\">" + m_Dt.Rows[i][2].ToString() + "</p>");
                            //s.Append("<p class=\"name\">�����:" + m_Dt.Rows[i][4].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>");
                            //����˫����˵����
                            if (this.BizCode == "0101" || this.BizCode == "0108")
                            {
                                if (f == 3) { s.Append("<p class=\"name2\">������:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "��<br />������:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">�����:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0102" || this.BizCode == "0106" || this.BizCode == "0122")
                            {
                                if (f == 3 || f == 4) { s.Append("<p class=\"name2\">������:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "��<br />������:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">�����:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0103" || this.BizCode == "0104" || this.BizCode == "0105")
                            {
                                if (f == 2 || f == 3) { s.Append("<p class=\"name2\">������:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "��<br />������:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">�����:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0107")
                            {
                                if (f == 2) { s.Append("<p class=\"name2\">������:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "��<br />������:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">�����:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else if (this.BizCode == "0111")
                            {
                                if (stepTotal == 6 && f == 6) { s.Append("<p class=\"name2\">������:" + m_Dt.Rows[i][4].ToString() + "��<br />������:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else if (stepTotal == 3 && f == 3) { s.Append("<p class=\"name2\">������:" + m_Dt.Rows[i][4].ToString() + "��<br />������:" + m_Dt.Rows[i]["AuditUserSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                                else { s.Append("<p class=\"name\">�����:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            }
                            else { s.Append("<p class=\"name\">�����:" + m_Dt.Rows[i]["ApprovalSignName"].ToString() + "<br />ʱ��:" + m_Dt.Rows[i][7].ToString() + "</p>"); }
                            s.Append("<p class=\"view\">���������</p>");
                            string CommMemostr="";
                            if (!string.IsNullOrEmpty(CommMemo))
                            {
                                CommMemostr = "<br>�貹���ύ�Ĳ������£�";
                                string[] aryDocs = null;
                                aryDocs = CommMemo.Split(',');
                                    for (int k = 0; k < aryDocs.Length; k++)
                                    {
                                        CommMemostr+="<br>" + (k + 1).ToString() + "��" + aryDocs[k];
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
                            //s.Append("<p class=\"name\">�����:<br />ʱ��:</p>");
                            /*
0103 ���� ��������2,3
0104 �ط� ��������2,3
0105 �����츻 �������� 2,3
0106 ��������  �������� 3,4
0108 ����֤ �� 3
            */
                            //����˫����˵����
                            if (this.BizCode == "0101" || this.BizCode == "0108")
                            {
                                if (f == 3) { s.Append("<p class=\"name2\">�����:<br />������:<br />ʱ��:</p>"); }
                                else { s.Append("<p class=\"name\">�����:<br />ʱ��:</p>"); }
                            }
                            else if (this.BizCode == "0102" || this.BizCode == "0106" || this.BizCode == "0122")
                            {
                                if (f == 3 || f == 4) { s.Append("<p class=\"name2\">�����:<br />������:<br />ʱ��:</p>"); }
                                else { s.Append("<p class=\"name\">�����:<br />ʱ��:</p>"); }
                            }
                            else if (this.BizCode == "0103" || this.BizCode == "0104" || this.BizCode == "0105")
                            {
                                if (f == 2 || f == 3) { s.Append("<p class=\"name2\">�����:<br />������:<br />ʱ��:</p>"); }
                                else { s.Append("<p class=\"name\">�����:<br />ʱ��:</p>"); }
                            }
                            else if (this.BizCode == "0107")
                            {
                                if (f == 2) { s.Append("<p class=\"name2\">�����:<br />������:<br />ʱ��:</p>"); }
                                else { s.Append("<p class=\"name\">�����:<br />ʱ��:</p>"); }
                            }
                            else if (this.BizCode == "0111")
                            {
                                if (stepTotal == 6 && f == 6) { s.Append("<p class=\"name2\">�����:<br />������:<br />ʱ��:</p>"); }
                                else if (stepTotal == 3 && f == 3) { s.Append("<p class=\"name2\">�����:<br />������:<br />ʱ��:</p>"); }
                                else { s.Append("<p class=\"name\">�����:<br />ʱ��:</p>"); }
                            }
                            else { s.Append("<p class=\"name\">�����:<br />ʱ��:</p>"); }
                            s.Append("<p class=\"view\">���������</p>");
                            s.Append("<p class=\"sum\">����</p>");
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
        /// ��ȡ֤������
        /// </summary>
        /// <param name="bizCode"></param>
        /// <param name="areaCode"></param>
        /// <param name="childNum"></param>
        public string GetCertificateNo(string bizCode, string areaCode, string childNum)
        {
            /*
            ֤�����.���鰴�����¹���:
            ���յ�λ����+���+4λ��ˮ��,����:A15052220020150001
            һ��:Y ����:Z ��ͥ����:J ������Ů����֤:D ����֤��:H
            
            a,����֤��ţ���������+���+��ˮ�� ��Ч��3��
            SELECT COUNT() FROM BIZ_Certificates WHERE BizCode='0101' AND CreateDate >'2015/01/01 00:00:00' AND CreateDate <'2016/01/01 00:00:00'
            b,����֤��ǩ�´���һ��[����] Ԥ����+[����] ,Ԥ���ڲ��Ǳ�����,
            ����֤��ţ���������9+���4+����1+��ˮ��(4)
            c,����֪ͨ�飺���+3λ��ˮ
             */
            /*
           0101	һ�������Ǽ� 3 
           0102	���������Ǽ� 4
           0103	�������� 3
           0108	������Ů��ĸ����֤ 3
           0109	�������˿ڻ���֤���� 3
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
                    //������9λ+���2λ+����˫����ȡ����֤�������ڶ���Ϊ2���� 
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
        /// ��ȡ��ǰҵ���������Ϣ
        /// </summary>
        public void GetWorkFlowsByBizID()
        {
            if (!string.IsNullOrEmpty(this.BizID))
            {
                // IsInnerArea: 0Ͻ����; 1Ͻ����
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
        /// ���ö�ά��
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
                    //string CertificateName = "�����˿ڻ���֤�������";
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
