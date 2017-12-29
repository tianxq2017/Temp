using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Globalization;

namespace join.pms.dal
{
    public class BizAuditInfo
    {
        ~BizAuditInfo() { }

        
        // ҵ����
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

        /// <summary>
        /// ֤����Ч����
        /// </summary>
        /// <param name="BizCode"></param>
        /// <returns></returns>
        public static string GetValidDate(string BizCode) {
            string returnVa = string.Empty;
            switch (BizCode)
            {
                case "0101": 
                    returnVa = "����";
                    break;
                case "0102":
                    returnVa = "һ��";
                    break;
                default:
                    returnVa = "";
                    break;
            }
            return returnVa;
        }

        /*
0101	3	һ�������Ǽ�
0102	4	���������Ǽ����
0103	3	������ͥ�������������걨���
0104	3	������ͥ�ر���������������
0105	3	�������츻�������������
0106	4	������ͥ�����������
0107	3	��һ���̡���������������
0108	3	��������Ů��ĸ����֤�����
0109	3	�������˿ڻ���֤�����������
0110	3	�������֤������
0111	6	��ֹ�����������
            */
        public string GetAuditInfo(string stepTotal)
        {
            StringBuilder s = new StringBuilder();
            s.Append("<select name=\"selAuditComments\" id=\"selAuditComments\" onchange=\" SetTxtCommVal(this.options[this.options.selectedIndex].value)\">");
            s.Append("<option value=\"\">��ѡ������������</option>");
            if (!string.IsNullOrEmpty(this.BizCode) && !string.IsNullOrEmpty(this.BizStep))
            {
                switch (this.BizCode)
                {
                    case "0150":// 0150	1	��������������֤���Ǽ�
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ��Ǽǡ�\">�����ʵ��ͬ��ǼǷ�֤��</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        break;
                    case "0101":// 0101	3	һ�������Ǽ�
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        //else if (this.BizStep == "1"){}
                        else
                        {
                            s.Append("<option value=\"��ѡ�����������\">���ݡ����ɹ��������˿���ƻ������������� * ���Ĺ涨����������������ͬ��Ǽǡ�</option>");
                            s.Append("<option value=\"�����ϡ����ɹ��������˿���ƻ������������涨�������������������跢֤��\">�����ϡ����ɹ��������˿���ƻ������������涨�������������������跢֤��</option>");
                            s.Append("<option value=\"֤�����ϲ��룬֪ͨ�����˲�����\">֤�����ϲ��룬֪ͨ�����˲�����</option>");
                        }
                        break;
                    case "0102":// 0102	4	���������Ǽ�
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        //else if (this.BizStep == "1"){}
                        else
                        {
                            s.Append("<option value=\"��ѡ�����������\">���ݡ����ɹ��������˿���ƻ������������� * ���Ĺ涨����������������ͬ��Ǽǡ�</option>");
                            s.Append("<option value=\"�����ϡ����ɹ��������˿���ƻ������������涨�������������������跢֤��\">�����ϡ����ɹ��������˿���ƻ������������涨�������������������跢֤��</option>");
                            s.Append("<option value=\"֤�����ϲ��룬֪ͨ�����˲�����\">֤�����ϲ��룬֪ͨ�����˲�����</option>");
                        }
                        break;

                        //if (this.BizStep == "1" || this.BizStep == "2")
                        //{
                        //    s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                        //    s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        //}
                        //else if (this.BizStep == "3")
                        //{
                        //    s.Append("<option value=\"ͬ���ϱ���ˡ�\">ͬ���ϱ���ˡ�</option>");
                        //    s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        //}
                        //else
                        //{
                        //    s.Append("<option value=\"��ѡ�����������\">����    �涨��   ������������ͬ�֤ⷢ��</option>");
                        //    s.Append("<option value=\"���ݡ��й��������Ժ����ʵʩȫ���������߸ĸ����Ƽƻ������������ľ������������ԵǼǷ�֤\">���ݡ��й��������Ժ����ʵʩȫ���������߸ĸ����Ƽƻ������������ľ������������ԵǼǷ�֤</option>");
                        //    //s.Append("<option value=\"����    �涨��   ������������ͬ�֤ⷢ��\">����    �涨��   ������������ͬ�֤ⷢ��</option>");
                        //    //s.Append("<option value=\"������     �涨��   �������������������跢֤��\">������     �涨��   �������������������跢֤��</option>");
                        //    s.Append("<option value=\"֤�����ϲ��룬֪ͨ�����˲�����\">֤�����ϲ��룬֪ͨ�����˲�����</option>");
                        //}
                        //break;
                    case "0103":// 0103	3	������ͥ�������������걨���
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"׼�����ܼƻ�������ͥ�����������ߡ�\">׼�����ܼƻ�������ͥ�����������ߡ�</option>");
                            s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                        }
                        break;
                    case "0104":// 0104	3	������ͥ�ر���������������
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"׼�����ܼƻ�������ͥ�ر�������ߡ�\">׼�����ܼƻ�������ͥ�ر�������ߡ�</option>");
                            s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                        }
                        break;
                    case "0105":// 0105	3	�������츻�������������
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"׼�����ܼƻ������������츻�����̷������ߡ�\">׼�����ܼƻ������������츻�����̷������ߡ�</option>");
                            s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                        }
                        break;
                    case "0106":// 0106	4	������ͥ�����������
                        if (this.BizStep == "1" || this.BizStep == "2" || this.BizStep == "3")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        //else if (this.BizStep == "3"){}
                        else
                        {
                            s.Append("<option value=\"׼�����ܼƻ���������   ��˫Ů���ɹ�����Ů����������������������ߡ�\">׼�����ܼƻ���������   ��˫Ů���ɹ�����Ů����������������������ߡ�</option>");
                            s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                        }
                        break;
                    case "0107":// 0107	3	��һ���̡���������������
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"׼�����ܡ�\">׼�����ܡ�</option>");
                            s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                        }
                        break;
                    case "0108":// 0108	3	��������Ů��ĸ����֤�����
                        if (stepTotal == "3")
                        {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"׼�跢֤��\">׼�跢֤��</option>");
                                s.Append("<option value=\"֤�����ϲ��룬֪ͨ�����˲�����\">֤�����ϲ��룬֪ͨ�����˲�����</option>");
                            }
                        }
                        else
                        {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"׼�跢֤��\">׼�跢֤��</option>");
                                s.Append("<option value=\"֤�����ϲ��룬֪ͨ�����˲�����\">֤�����ϲ��룬֪ͨ�����˲�����</option>");
                            }
                        }
                        break;
                    case "0109":// 0109	3	�������˿ڻ���֤�����������
                        if (stepTotal == "2")
                        {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���֤��\">�����ʵ��ͬ���֤��</option>");
                                s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                            }
                        }
                        else
                        {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���֤��\">�����ʵ��ͬ���֤��</option>");
                                s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                            }
                        }
                        break;
                    case "0110": // 0110	3	�������֤������
                        if (stepTotal == "3") {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"�����ʵ��\">�����ʵ��</option>");
                                s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                            }
                        } 
                        else {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"�����ʵ��\">�����ʵ��</option>");
                                s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                            }
                        }
                        break;
                    case "0111":// 0111	6	��ֹ����������� �²�/��/��,��/��ľ/��/�ְ�/��,ҽԺ����,ҽԺ����,ҽԺ������,������
                        if (stepTotal == "3") {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"��������ֹ��������������֪ͨ������\">��������ֹ��������������֪ͨ������</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"ͬ����ֹ���\">ͬ����ֹ���</option>");
                                s.Append("<option value=\"��ͬ����ֹ���\">��ͬ����ֹ���</option>");
                            }
                        } 
                        else {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"������     ��\">������     ��</option>");
                            }
                            else if (this.BizStep == "2")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                            }
                            else if (this.BizStep == "3" || this.BizStep == "4" || this.BizStep == "5")
                            {
                                s.Append("<option value=\"̥����̥\">̥����̥</option>");
                                s.Append("<option value=\"̥������\">̥������</option>");
                                s.Append("<option value=\"���ھ���֢\">���ھ���֢</option>");
                                s.Append("<option value=\"���ܼ�������\">���ܼ�������</option>");
                                s.Append("<option value=\"�������ಡ\">�������ಡ</option>");
                                s.Append("<option value=\"���߸�Ѫѹ\">���߸�Ѫѹ</option>");
                                s.Append("<option value=\"�������Ը�Ⱦ\">�������Ը�Ⱦ</option>");
                                s.Append("<option value=\"���������ۺ�֢\">���������ۺ�֢</option>");
                                s.Append("<option value=\"��������ƶѪ\">��������ƶѪ</option>");
                                s.Append("<option value=\"����\">����</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"ͬ����ֹ���\">ͬ����ֹ���</option>");
                                s.Append("<option value=\"��������ֹ������������ͬ����ֹ���\">��������ֹ������������ͬ����ֹ���</option>");
                            }
                        }
                        break;
                    case "0122":
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        else if (this.BizStep == "3")
                        {
                            s.Append("<option value=\"ͬ���ϱ���ˡ�\">ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"��ѡ�����������\">����    �涨��   ������������ͬ�֤ⷢ��</option>");
                            //s.Append("<option value=\"����    �涨��   ������������ͬ�֤ⷢ��\">����    �涨��   ������������ͬ�֤ⷢ��</option>");
                            //s.Append("<option value=\"������     �涨��   �������������������跢֤��\">������     �涨��   �������������������跢֤��</option>");
                            s.Append("<option value=\"֤�����ϲ��룬֪ͨ�����˲�����\">֤�����ϲ��룬֪ͨ�����˲�����</option>");
                        }
                        break;
                    /*
�����⣺
�弶��
1.�����ʵ��ͬ���ϱ���ˡ�
2.��������ֹ��������������֪ͨ�����ˡ�
�򼶣�
1. �����ʵ��ͬ���ϱ���ˡ�
2. ��������ֹ��������������֪ͨ�����ˡ�
�켶��
1.ͬ����ֹ����
2.��ͬ����ֹ����
�������ɣ�������������ֹ����/������������ֹ����

                     */
                    default:
                        s.Append("<option value=\"\">��ҵ��������������</option>");
                        break;
                }

            }
            else
            {
                s.Append("<option value=\"\">��ҵ��������������</option>");
            }
            s.Append("</select>");


            // ��������
            if(this.BizCode=="0101")
            {
                if (this.BizStep == "2" || this.BizStep == "3")
                {
                    s.Append("<br/>");
                    s.Append("<select name=\"selAuditItems\" id=\"selAuditItems\" onchange=\" SetTxtCommValByFilter(this.options[this.options.selectedIndex].value)\">");
                    s.Append("<option value=\"\">��ѡ��һ�������Ǽ����������</option>");
                    s.Append("<option value=\"��ʮ������һ��\">�����ͥ����ʮ������һ�һ�Է���ֻ����һ����Ů</option>");
                    s.Append("</select>");
                }
            }
            else if (this.BizCode == "0102") {
                if (this.BizStep == "2" || this.BizStep == "3") {
                    s.Append("<br/>");
                    s.Append("<select name=\"selAuditItems\" id=\"selAuditItems\" onchange=\" SetTxtCommValByFilter(this.options[this.options.selectedIndex].value)\">");
                    s.Append("<option value=\"\">��ѡ����������Ǽ����������</option>");
                    s.Append("<option value=\"��ʮ������һ��\">�����ͥ����ʮ������һ�һ�Է���ֻ����һ����Ů</option>");
                    s.Append("</select>");
                }
            }
            else if (this.BizCode == "0122")
            {
                if (this.BizStep == "3" || this.BizStep == "4")
                {
                    s.Append("<br/>");
                    s.Append("<select name=\"selAuditItems\" id=\"selAuditItems\" onchange=\" SetTxtCommValByFilter(this.options[this.options.selectedIndex].value)\">");
                    s.Append("<option value=\"\">��ѡ���������������������</option>");
                    s.Append("<option value=\"��ʮ������һ��\"></option>");
                    s.Append("<option value=\"��ʮ�����ڶ���\"></option>");
                    s.Append("<option value=\"��ʮ����������\"></option>");
                    s.Append("<option value=\"��ʮ����������\"></option>");
                    s.Append("</select>");
                }
            }
            else { }

            return s.ToString();
        }

        public string GetAuditInfo(string stepTotal,string bizID)
        {
            StringBuilder s = new StringBuilder();
            s.Append("<select name=\"selAuditComments\" id=\"selAuditComments\" onchange=\" SetTxtCommVal(this.options[this.options.selectedIndex].value)\">");
            s.Append("<option value=\"\">��ѡ������������</option>");
            if (!string.IsNullOrEmpty(this.BizCode) && !string.IsNullOrEmpty(this.BizStep))
            {
                switch (this.BizCode)
                {
                    case "0150":// 0150	1	��������������֤���Ǽ�
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ��Ǽǡ�\">�����ʵ��ͬ��ǼǷ�֤��</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        break;
                    case "0101":// 0101	3	һ�������Ǽ�
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        //else if (this.BizStep == "1"){}
                        else
                        {
                            s.Append("<option value=\"��ѡ�����������\">���ݡ����ɹ��������˿���ƻ������������� * ���Ĺ涨����������������ͬ��Ǽǡ�</option>");
                            s.Append("<option value=\"�����ϡ����ɹ��������˿���ƻ������������涨�������������������跢֤��\">�����ϡ����ɹ��������˿���ƻ������������涨�������������������跢֤��</option>");
                            s.Append("<option value=\"֤�����ϲ��룬֪ͨ�����˲�����\">֤�����ϲ��룬֪ͨ�����˲�����</option>");
                        }
                        break;
                    case "0102":// 0102	4	���������Ǽ����
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        //else if (this.BizStep == "1"){}
                        else
                        {
                            s.Append("<option value=\"��ѡ�����������\">���ݡ����ɹ��������˿���ƻ������������� * ���Ĺ涨����������������ͬ��Ǽǡ�</option>");
                            s.Append("<option value=\"�����ϡ����ɹ��������˿���ƻ������������涨�������������������跢֤��\">�����ϡ����ɹ��������˿���ƻ������������涨�������������������跢֤��</option>");
                            s.Append("<option value=\"֤�����ϲ��룬֪ͨ�����˲�����\">֤�����ϲ��룬֪ͨ�����˲�����</option>");
                        }
                        break;
                        //if (this.BizStep == "1" || this.BizStep == "2")
                        //{
                        //    s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                        //    s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        //}
                        //else if (this.BizStep == "3")
                        //{
                        //    s.Append("<option value=\"ͬ���ϱ���ˡ�\">ͬ���ϱ���ˡ�</option>");
                        //    s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        //}
                        //else
                        //{
                        //    string babyNum = CommPage.GetSingleVal("SELECT Fileds18 FROM BIZ_Contents WHERE BizID=" + bizID);
                        //    s.Append("<option value=\"����    �涨��" + babyNum + "������������ͬ�֤ⷢ��\">����    �涨��" + babyNum + "������������ͬ�֤ⷢ��</option>");
                        //    s.Append("<option value=\"������     �涨��" + babyNum + "�������������������跢֤��\">������     �涨��" + babyNum + "�������������������跢֤��</option>");
                        //    s.Append("<option value=\"֤�����ϲ��룬֪ͨ�����˲�����\">֤�����ϲ��룬֪ͨ�����˲�����</option>");
                        //}
                        //break;
                    case "0103":// 0103	3	������ͥ�������������걨���
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"׼�����ܼƻ�������ͥ�����������ߡ�\">׼�����ܼƻ�������ͥ�����������ߡ�</option>");
                            s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                        }
                        break;
                    case "0104":// 0104	3	������ͥ�ر���������������
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"׼�����ܼƻ�������ͥ�ر�������ߡ�\">׼�����ܼƻ�������ͥ�ر�������ߡ�</option>");
                            s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                        }
                        break;
                    case "0105":// 0105	3	�������츻�������������
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"׼�����ܼƻ������������츻�����̷������ߡ�\">׼�����ܼƻ������������츻�����̷������ߡ�</option>");
                            s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                        }
                        break;
                    case "0106":// 0106	4	������ͥ�����������
                        if (this.BizStep == "1" || this.BizStep == "2" || this.BizStep == "3")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        //else if (this.BizStep == "3"){}
                        else
                        {
                            s.Append("<option value=\"׼�����ܼƻ���������   ��˫Ů���ɹ�����Ů����������������������ߡ�\">׼�����ܼƻ���������   ��˫Ů���ɹ�����Ů����������������������ߡ�</option>");
                            s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                        }
                        break;
                    case "0107":// 0107	3	��һ���̡���������������
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"׼�����ܡ�\">׼�����ܡ�</option>");
                            s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                        }
                        break;
                    case "0108":// 0108	3	��������Ů��ĸ����֤�����
                        if (stepTotal == "3")
                        {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"׼�跢֤��\">׼�跢֤��</option>");
                                s.Append("<option value=\"֤�����ϲ��룬֪ͨ�����˲�����\">֤�����ϲ��룬֪ͨ�����˲�����</option>");
                            }
                        }
                        else
                        {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"׼�跢֤��\">׼�跢֤��</option>");
                                s.Append("<option value=\"֤�����ϲ��룬֪ͨ�����˲�����\">֤�����ϲ��룬֪ͨ�����˲�����</option>");
                            }
                        }
                        break;
                    case "0109":// 0109	3	�������˿ڻ���֤�����������
                        if (stepTotal == "2")
                        {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���֤��\">�����ʵ��ͬ���֤��</option>");
                                s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                            }
                        }
                        else
                        {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���֤��\">�����ʵ��ͬ���֤��</option>");
                                s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                            }
                        }
                        break;
                    case "0110": // 0110	3	�������֤������
                        if (stepTotal == "3")
                        {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"�����ʵ��\">�����ʵ��</option>");
                                s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                            }
                        }
                        else
                        {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"�����ʵ��\">�����ʵ��</option>");
                                s.Append("<option value=\"��������ع涨��\">��������ع涨��</option>");
                            }
                        }
                        break;
                    case "0111":// 0111	6	��ֹ����������� �²�/��/��,��/��ľ/��/�ְ�/��,ҽԺ����,ҽԺ����,ҽԺ������,������
                        if (stepTotal == "3")
                        {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"��������ֹ��������������֪ͨ������\">��������ֹ��������������֪ͨ������</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"ͬ����ֹ���\">ͬ����ֹ���</option>");
                                s.Append("<option value=\"��ͬ����ֹ���\">��ͬ����ֹ���</option>");
                            }
                        }
                        else
                        {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"������     ��\">������     ��</option>");
                            }
                            else if (this.BizStep == "2")
                            {
                                s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                                s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                            }
                            else if (this.BizStep == "3" || this.BizStep == "4" || this.BizStep == "5")
                            {
                                s.Append("<option value=\"̥����̥\">̥����̥</option>");
                                s.Append("<option value=\"̥������\">̥������</option>");
                                s.Append("<option value=\"���ھ���֢\">���ھ���֢</option>");
                                s.Append("<option value=\"���ܼ�������\">���ܼ�������</option>");
                                s.Append("<option value=\"�������ಡ\">�������ಡ</option>");
                                s.Append("<option value=\"���߸�Ѫѹ\">���߸�Ѫѹ</option>");
                                s.Append("<option value=\"�������Ը�Ⱦ\">�������Ը�Ⱦ</option>");
                                s.Append("<option value=\"���������ۺ�֢\">���������ۺ�֢</option>");
                                s.Append("<option value=\"��������ƶѪ\">��������ƶѪ</option>");
                                s.Append("<option value=\"����\">����</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"ͬ����ֹ���\">ͬ����ֹ���</option>");
                                s.Append("<option value=\"��������ֹ������������ͬ����ֹ���\">��������ֹ������������ͬ����ֹ���</option>");
                            }
                        }
                        break;
                    case "0122":// 0102	4	�������Ǽ����
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"�����ʵ��ͬ���ϱ���ˡ�\">�����ʵ��ͬ���ϱ���ˡ�</option>");
                            s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        }
                        //else if (this.BizStep == "3")
                        //{
                        //    s.Append("<option value=\"ͬ���ϱ���ˡ�\">ͬ���ϱ���ˡ�</option>");
                        //    s.Append("<option value=\"֤�����ϲ�ȫ��֪ͨ�����˲��䡣\">֤�����ϲ�ȫ��֪ͨ�����˲��䡣</option>");
                        //}
                        else
                        {
                            s.Append("<option value=\"��ѡ�����������\">���ݡ����ɹ��������˿���ƻ������������� * ���Ĺ涨����������������ͬ�֤ⷢ��</option>");
                            s.Append("<option value=\"�����ϡ����ɹ��������˿���ƻ������������涨�������������������跢֤��\">�����ϡ����ɹ��������˿���ƻ������������涨�������������������跢֤��</option>");
                            s.Append("<option value=\"֤�����ϲ��룬֪ͨ�����˲�����\">֤�����ϲ��룬֪ͨ�����˲�����</option>");
                        
                            //string babyNum = CommPage.GetSingleVal("SELECT Fileds18 FROM BIZ_Contents WHERE BizID=" + bizID);
                            //s.Append("<option value=\"����    �涨��" + babyNum + "������������ͬ�֤ⷢ��\">����    �涨��" + babyNum + "������������ͬ�֤ⷢ��</option>");
                            //s.Append("<option value=\"������     �涨��" + babyNum + "�������������������跢֤��\">������     �涨��" + babyNum + "�������������������跢֤��</option>");
                            //s.Append("<option value=\"֤�����ϲ��룬֪ͨ�����˲�����\">֤�����ϲ��룬֪ͨ�����˲�����</option>");
                        }
                        break;
                    /*
�����⣺
�弶��
1.�����ʵ��ͬ���ϱ���ˡ�
2.��������ֹ��������������֪ͨ�����ˡ�
�򼶣�
1. �����ʵ��ͬ���ϱ���ˡ�
2. ��������ֹ��������������֪ͨ�����ˡ�
�켶��
1.ͬ����ֹ����
2.��ͬ����ֹ����
�������ɣ�������������ֹ����/������������ֹ����

                     */
                    default:
                        s.Append("<option value=\"\">��ҵ��������������</option>");
                        break;
                }

            }
            else
            {
                s.Append("<option value=\"\">��ҵ��������������</option>");
            }
            s.Append("</select>");


            // ��������
            if (this.BizCode == "0101")
            {
                if (this.BizStep == "2" || this.BizStep == "3")
                {
                    s.Append("<br/>");
                    s.Append("<select name=\"selAuditItems\" id=\"selAuditItems\" onchange=\" SetTxtCommValByFilter(this.options[this.options.selectedIndex].value)\">");
                    s.Append("<option value=\"\">��ѡ��һ�������Ǽ����������</option>");
                    //s.Append("<option value=\"��ʮ����\">�����ͥ����ʮ������һ�һ�Է���ֻ����һ����Ů</option>");
                    //s.Append("<option value=\"��ʮ����\">�ɹ����ͥ����ʮ�������ɹ��幫��һ�Է��޿�������������Ů��</option>");
                    s.Append("<option value=\"��ʮ����\">��ʮ���� �ᳫһ�Է�������������Ů�����ش��Ӷ��塢���¿��塢���״��幫���������Ը�� </option>");
                    s.Append("</select>");
                }
            }
            else if (this.BizCode == "0102")
            {
                if (this.BizStep == "2" || this.BizStep == "3")
                {
                    s.Append("<br/>");
                    s.Append("<select name=\"selAuditItems\" id=\"selAuditItems\" onchange=\" SetTxtCommValByFilter(this.options[this.options.selectedIndex].value)\">");
                    s.Append("<option value=\"\">��ѡ����������Ǽ����������</option>");
                    //s.Append("<option value=\"��ʮ������һ��\">��ʮ������һ��</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ����һ��\">��ʮ�����ڶ����һ��</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ���ڶ���\">��ʮ�����ڶ���ڶ���</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ��������\">��ʮ�����ڶ��������</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ��������\">��ʮ�����ڶ��������</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ��������\">��ʮ�����ڶ��������</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ��������\">��ʮ�����ڶ��������</option>");
                    //s.Append("<option value=\"��ʮ������һ��\">��ʮ������һ��</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ���\">��ʮ�����ڶ���</option>");
                    //s.Append("<option value=\"��ʮ����������\">��ʮ����������</option>");
                    //s.Append("<option value=\"��ʮ�������Ŀ�\">��ʮ�������Ŀ�</option>");
                    //s.Append("<option value=\"��ʮ������һ���һ��\">��ʮ������һ���һ��</option>");
                    //s.Append("<option value=\"��ʮ������һ��ڶ���\">��ʮ������һ��ڶ���</option>");
                    //s.Append("<option value=\"��ʮ������һ�������\">��ʮ������һ�������</option>");
                    //s.Append("<option value=\"��ʮ������һ�������\">��ʮ������һ�������</option>");
                    //s.Append("<option value=\"��ʮ������һ�������\">��ʮ������һ�������</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ����һ��\">��ʮ�����ڶ����һ��</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ���ڶ���\">��ʮ�����ڶ���ڶ���</option>");
                    s.Append("<option value=\"��ʮ����\">��ʮ���� �ᳫһ�Է�������������Ů�����ش��Ӷ��塢���¿��塢���״��幫���������Ը�� </option>");
                    s.Append("</select>");
                }
            }
            else if (this.BizCode == "0122")
            {
                if (this.BizStep == "3" || this.BizStep == "4")
                {
                    s.Append("<br/>");
                    s.Append("<select name=\"selAuditItems\" id=\"selAuditItems\" onchange=\" SetTxtCommValByFilter(this.options[this.options.selectedIndex].value)\">");
                    s.Append("<option value=\"\">��ѡ���������������������</option>");
                    //s.Append("<option value=\"��ʮ������һ��\">��ʮ������һ��</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ����һ��\">��ʮ�����ڶ����һ��</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ���ڶ���\">��ʮ�����ڶ���ڶ���</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ��������\">��ʮ�����ڶ��������</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ��������\">��ʮ�����ڶ��������</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ��������\">��ʮ�����ڶ��������</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ��������\">��ʮ�����ڶ��������</option>");
                    //s.Append("<option value=\"��ʮ������һ��\">��ʮ������һ��</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ���\">��ʮ�����ڶ���</option>");
                    //s.Append("<option value=\"��ʮ����������\">��ʮ����������</option>");
                    //s.Append("<option value=\"��ʮ�������Ŀ�\">��ʮ�������Ŀ�</option>");
                    //s.Append("<option value=\"��ʮ������һ���һ��\">��ʮ������һ���һ��</option>");
                    //s.Append("<option value=\"��ʮ������һ��ڶ���\">��ʮ������һ��ڶ���</option>");
                    //s.Append("<option value=\"��ʮ������һ�������\">��ʮ������һ�������</option>");
                    //s.Append("<option value=\"��ʮ������һ�������\">��ʮ������һ�������</option>");
                    //s.Append("<option value=\"��ʮ������һ�������\">��ʮ������һ�������</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ����һ��\">��ʮ�����ڶ����һ��</option>");
                    //s.Append("<option value=\"��ʮ�����ڶ���ڶ���\">��ʮ�����ڶ���ڶ���</option>");
                    s.Append("<option value=\"��ʮ������һ��\">��ʮ������һ�� ������Ů���в��ж������ܳɳ�Ϊ�����Ͷ�����ҽѧ����Ϊ���޿�����������</option>");
                    s.Append("<option value=\"��ʮ�����ڶ���\">��ʮ�����ڶ��� �ٻ���ޣ��������飩���ٻ�ǰ�ϼ�������������������Ů���ٻ��δ������Ů��</option>");
                    s.Append("<option value=\"��ʮ����������\">��ʮ���������� �ٻ���ޣ��������飩���ٻ�ǰ�ϼ�ֻ������һ����Ů���ٻ������һ����Ů��</option>");
                    s.Append("<option value=\"��ʮ����������\">��ʮ���������� �ɹ��幫�񣬷���˫����Ϊ�ǳ��򻧼��Ҵ���ũ��ҵ����������������Ů��ΪŮ����</option>");
                    s.Append("<option value=\"��ʮ����������\">��ʮ���������� ��������������������</option>");

                    s.Append("</select>");
                }
            }
            else { }

            return s.ToString();
        }
        /*
         ���ѡ��(5,10Ԫ),���ŷ�ʽ(��Ů����5Ԫ,Ů�����ڵ�λ����,�з����ڵ�λ����)������ʱ��(Ĭ�Ϸ�֤����,2015��1�µ���Ů����ʱ����14��,����)
         */
        // SELECT Fileds18 FROM BIZ_Contents WHERE BizID=
        public string GetCertificateMemo(string childBirthday) {
            string startDate = DateTime.Parse(childBirthday).ToString("yyyy��MM��");
            string endDate = DateTime.Parse(childBirthday).AddYears(14).ToString("yyyy��MM��");
            startDate = DateTime.Now.ToString("yyyy��MM��");//�ӵ�ǰ���ʱ�俪ʼ
            StringBuilder s = new StringBuilder();
            s.Append("<select name=\"selCerMome\" id=\"selCerMome\">");
            s.Append("<option value=\"\">��ѡ�񱣽��ѷ��Ž���Լ����ŷ�ʽ����</option>");
            s.Append("<option value=\"ÿ��10Ԫ��ÿ���У�Ů��������5Ԫ������" + startDate + "��" + endDate + "ֹ��\">�����ѣ�ÿ��10Ԫ��ÿ���У�Ů��������5Ԫ������" + startDate + "��" + endDate + "ֹ��</option>");
            s.Append("<option value=\"ÿ��10Ԫ�����з����ڵ�λ����10Ԫ����" + startDate + "��" + endDate + "ֹ��\">�����ѣ�ÿ��10Ԫ�����з����ڵ�λ����10Ԫ����" + startDate + "��" + endDate + "ֹ��</option>");
            s.Append("<option value=\"ÿ��10Ԫ����Ů�����ڵ�λ����10Ԫ����" + startDate + "��" + endDate + "ֹ��\">�����ѣ�ÿ��10Ԫ����Ů�����ڵ�λ����10Ԫ����" + startDate + "��" + endDate + "ֹ��</option>");
            s.Append("</select>");
            return s.ToString();
        }
         
    }
}
