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

        
        // 业务编号
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

        /// <summary>
        /// 证件有效期限
        /// </summary>
        /// <param name="BizCode"></param>
        /// <returns></returns>
        public static string GetValidDate(string BizCode) {
            string returnVa = string.Empty;
            switch (BizCode)
            {
                case "0101": 
                    returnVa = "三年";
                    break;
                case "0102":
                    returnVa = "一年";
                    break;
                default:
                    returnVa = "";
                    break;
            }
            return returnVa;
        }

        /*
0101	3	一孩生育登记
0102	4	二孩生育登记审核
0103	3	计生家庭奖励扶助对象申报审核
0104	3	计生家庭特别扶助对象申请审核
0105	3	“少生快富”工程申请审核
0106	4	结扎家庭奖励申请审核
0107	3	“一杯奶”受益对象申请审核
0108	3	《独生子女父母光荣证》审核
0109	3	《流动人口婚育证明》申请审核
0110	3	婚育情况证明申请
0111	6	终止妊娠申请审核
            */
        public string GetAuditInfo(string stepTotal)
        {
            StringBuilder s = new StringBuilder();
            s.Append("<select name=\"selAuditComments\" id=\"selAuditComments\" onchange=\" SetTxtCommVal(this.options[this.options.selectedIndex].value)\">");
            s.Append("<option value=\"\">请选择审核意见……</option>");
            if (!string.IsNullOrEmpty(this.BizCode) && !string.IsNullOrEmpty(this.BizStep))
            {
                switch (this.BizCode)
                {
                    case "0150":// 0150	1	《婚育健康服务证》登记
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"情况属实，同意登记。\">情况属实，同意登记发证。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        break;
                    case "0101":// 0101	3	一孩生育登记
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        //else if (this.BizStep == "1"){}
                        else
                        {
                            s.Append("<option value=\"请选择引用条款……\">根据《内蒙古自治区人口与计划生育条例》第 * 条的规定，符合生育条件，同意登记。</option>");
                            s.Append("<option value=\"不符合《内蒙古自治区人口与计划生育条例》规定的生育条件，决定不予发证。\">不符合《内蒙古自治区人口与计划生育条例》规定的生育条件，决定不予发证。</option>");
                            s.Append("<option value=\"证明材料不齐，通知申请人补正。\">证明材料不齐，通知申请人补正。</option>");
                        }
                        break;
                    case "0102":// 0102	4	二孩生育登记
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        //else if (this.BizStep == "1"){}
                        else
                        {
                            s.Append("<option value=\"请选择引用条款……\">根据《内蒙古自治区人口与计划生育条例》第 * 条的规定，符合生育条件，同意登记。</option>");
                            s.Append("<option value=\"不符合《内蒙古自治区人口与计划生育条例》规定的生育条件，决定不予发证。\">不符合《内蒙古自治区人口与计划生育条例》规定的生育条件，决定不予发证。</option>");
                            s.Append("<option value=\"证明材料不齐，通知申请人补正。\">证明材料不齐，通知申请人补正。</option>");
                        }
                        break;

                        //if (this.BizStep == "1" || this.BizStep == "2")
                        //{
                        //    s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                        //    s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        //}
                        //else if (this.BizStep == "3")
                        //{
                        //    s.Append("<option value=\"同意上报审核。\">同意上报审核。</option>");
                        //    s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        //}
                        //else
                        //{
                        //    s.Append("<option value=\"请选择引用条款……\">符合    规定的   孩生育条件；同意发证。</option>");
                        //    s.Append("<option value=\"根据《中共中央国务院关于实施全面两孩政策改革完善计划生育服务管理的决定》精神，予以登记发证\">根据《中共中央国务院关于实施全面两孩政策改革完善计划生育服务管理的决定》精神，予以登记发证</option>");
                        //    //s.Append("<option value=\"符合    规定的   孩生育条件；同意发证。\">符合    规定的   孩生育条件；同意发证。</option>");
                        //    //s.Append("<option value=\"不符合     规定的   孩生育条件，决定不予发证。\">不符合     规定的   孩生育条件，决定不予发证。</option>");
                        //    s.Append("<option value=\"证明材料不齐，通知申请人补正。\">证明材料不齐，通知申请人补正。</option>");
                        //}
                        //break;
                    case "0103":// 0103	3	计生家庭奖励扶助对象申报审核
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"准予享受计划生育家庭奖励扶助政策。\">准予享受计划生育家庭奖励扶助政策。</option>");
                            s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                        }
                        break;
                    case "0104":// 0104	3	计生家庭特别扶助对象申请审核
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"准予享受计划生育家庭特别扶助政策。\">准予享受计划生育家庭特别扶助政策。</option>");
                            s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                        }
                        break;
                    case "0105":// 0105	3	“少生快富”工程申请审核
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"准予享受计划生育“少生快富”工程扶持政策。\">准予享受计划生育“少生快富”工程扶持政策。</option>");
                            s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                        }
                        break;
                    case "0106":// 0106	4	结扎家庭奖励申请审核
                        if (this.BizStep == "1" || this.BizStep == "2" || this.BizStep == "3")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        //else if (this.BizStep == "3"){}
                        else
                        {
                            s.Append("<option value=\"准予享受计划生育结扎   （双女，蒙古族三女，二孩）结扎奖励相关政策。\">准予享受计划生育结扎   （双女，蒙古族三女，二孩）结扎奖励相关政策。</option>");
                            s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                        }
                        break;
                    case "0107":// 0107	3	“一杯奶”受益对象申请审核
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"准予享受。\">准予享受。</option>");
                            s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                        }
                        break;
                    case "0108":// 0108	3	《独生子女父母光荣证》审核
                        if (stepTotal == "3")
                        {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"准予发证。\">准予发证。</option>");
                                s.Append("<option value=\"证明材料不齐，通知申请人补正。\">证明材料不齐，通知申请人补正。</option>");
                            }
                        }
                        else
                        {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"准予发证。\">准予发证。</option>");
                                s.Append("<option value=\"证明材料不齐，通知申请人补正。\">证明材料不齐，通知申请人补正。</option>");
                            }
                        }
                        break;
                    case "0109":// 0109	3	《流动人口婚育证明》申请审核
                        if (stepTotal == "2")
                        {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"情况属实，同意办证。\">情况属实，同意办证。</option>");
                                s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                            }
                        }
                        else
                        {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"情况属实，同意办证。\">情况属实，同意办证。</option>");
                                s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                            }
                        }
                        break;
                    case "0110": // 0110	3	婚育情况证明申请
                        if (stepTotal == "3") {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"情况属实。\">情况属实。</option>");
                                s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                            }
                        } 
                        else {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"情况属实。\">情况属实。</option>");
                                s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                            }
                        }
                        break;
                    case "0111":// 0111	6	终止妊娠申请审核 嘎查/村/居,镇/苏木/乡/街办/场,医院门诊,医院儿科,医院妇产科,库伦旗
                        if (stepTotal == "3") {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"不符合终止妊娠条件（）请通知申请人\">不符合终止妊娠条件（）请通知申请人</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"同意终止妊娠。\">同意终止妊娠。</option>");
                                s.Append("<option value=\"不同意终止妊娠。\">不同意终止妊娠。</option>");
                            }
                        } 
                        else {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"不符合     。\">不符合     。</option>");
                            }
                            else if (this.BizStep == "2")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                            }
                            else if (this.BizStep == "3" || this.BizStep == "4" || this.BizStep == "5")
                            {
                                s.Append("<option value=\"胎儿死胎\">胎儿死胎</option>");
                                s.Append("<option value=\"胎儿畸形\">胎儿畸形</option>");
                                s.Append("<option value=\"宫内窘迫症\">宫内窘迫症</option>");
                                s.Append("<option value=\"不能继续妊娠\">不能继续妊娠</option>");
                                s.Append("<option value=\"孕者心脏病\">孕者心脏病</option>");
                                s.Append("<option value=\"孕者高血压\">孕者高血压</option>");
                                s.Append("<option value=\"孕者慢性感染\">孕者慢性感染</option>");
                                s.Append("<option value=\"孕者肾病综合症\">孕者肾病综合症</option>");
                                s.Append("<option value=\"孕者严重贫血\">孕者严重贫血</option>");
                                s.Append("<option value=\"其它\">其它</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"同意终止妊娠。\">同意终止妊娠。</option>");
                                s.Append("<option value=\"不符合终止妊娠条件，不同意终止妊娠。\">不符合终止妊娠条件，不同意终止妊娠。</option>");
                            }
                        }
                        break;
                    case "0122":
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        else if (this.BizStep == "3")
                        {
                            s.Append("<option value=\"同意上报审核。\">同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"请选择引用条款……\">符合    规定的   孩生育条件；同意发证。</option>");
                            //s.Append("<option value=\"符合    规定的   孩生育条件；同意发证。\">符合    规定的   孩生育条件；同意发证。</option>");
                            //s.Append("<option value=\"不符合     规定的   孩生育条件，决定不予发证。\">不符合     规定的   孩生育条件，决定不予发证。</option>");
                            s.Append("<option value=\"证明材料不齐，通知申请人补正。\">证明材料不齐，通知申请人补正。</option>");
                        }
                        break;
                    /*
政策外：
村级：
1.情况属实，同意上报审核。
2.不符合终止妊娠条件（）请通知申请人。
镇级：
1. 情况属实，同意上报审核。
2. 不符合终止妊娠条件（）请通知申请人。
旗级：
1.同意终止妊娠
2.不同意终止妊娠
申请理由：政策内申请终止妊娠/政策外申请终止妊娠

                     */
                    default:
                        s.Append("<option value=\"\">该业务审核意见不存在</option>");
                        break;
                }

            }
            else
            {
                s.Append("<option value=\"\">该业务审核意见不存在</option>");
            }
            s.Append("</select>");


            // 引用条款
            if(this.BizCode=="0101")
            {
                if (this.BizStep == "2" || this.BizStep == "3")
                {
                    s.Append("<br/>");
                    s.Append("<select name=\"selAuditItems\" id=\"selAuditItems\" onchange=\" SetTxtCommValByFilter(this.options[this.options.selectedIndex].value)\">");
                    s.Append("<option value=\"\">请选择一孩生育登记引用条款……</option>");
                    s.Append("<option value=\"第十七条第一款\">汉族家庭：第十七条第一款，一对夫妻只生育一个子女</option>");
                    s.Append("</select>");
                }
            }
            else if (this.BizCode == "0102") {
                if (this.BizStep == "2" || this.BizStep == "3") {
                    s.Append("<br/>");
                    s.Append("<select name=\"selAuditItems\" id=\"selAuditItems\" onchange=\" SetTxtCommValByFilter(this.options[this.options.selectedIndex].value)\">");
                    s.Append("<option value=\"\">请选择二孩生育登记引用条款……</option>");
                    s.Append("<option value=\"第十七条第一款\">汉族家庭：第十七条第一款，一对夫妻只生育一个子女</option>");
                    s.Append("</select>");
                }
            }
            else if (this.BizCode == "0122")
            {
                if (this.BizStep == "3" || this.BizStep == "4")
                {
                    s.Append("<br/>");
                    s.Append("<select name=\"selAuditItems\" id=\"selAuditItems\" onchange=\" SetTxtCommValByFilter(this.options[this.options.selectedIndex].value)\">");
                    s.Append("<option value=\"\">请选择再生育审批引用条款……</option>");
                    s.Append("<option value=\"第十八条第一项\"></option>");
                    s.Append("<option value=\"第十八条第二项\"></option>");
                    s.Append("<option value=\"第十八条第三项\"></option>");
                    s.Append("<option value=\"第十八条第四项\"></option>");
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
            s.Append("<option value=\"\">请选择审核意见……</option>");
            if (!string.IsNullOrEmpty(this.BizCode) && !string.IsNullOrEmpty(this.BizStep))
            {
                switch (this.BizCode)
                {
                    case "0150":// 0150	1	《婚育健康服务证》登记
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"情况属实，同意登记。\">情况属实，同意登记发证。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        break;
                    case "0101":// 0101	3	一孩生育登记
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        //else if (this.BizStep == "1"){}
                        else
                        {
                            s.Append("<option value=\"请选择引用条款……\">根据《内蒙古自治区人口与计划生育条例》第 * 条的规定，符合生育条件，同意登记。</option>");
                            s.Append("<option value=\"不符合《内蒙古自治区人口与计划生育条例》规定的生育条件，决定不予发证。\">不符合《内蒙古自治区人口与计划生育条例》规定的生育条件，决定不予发证。</option>");
                            s.Append("<option value=\"证明材料不齐，通知申请人补正。\">证明材料不齐，通知申请人补正。</option>");
                        }
                        break;
                    case "0102":// 0102	4	二孩生育登记审核
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        //else if (this.BizStep == "1"){}
                        else
                        {
                            s.Append("<option value=\"请选择引用条款……\">根据《内蒙古自治区人口与计划生育条例》第 * 条的规定，符合生育条件，同意登记。</option>");
                            s.Append("<option value=\"不符合《内蒙古自治区人口与计划生育条例》规定的生育条件，决定不予发证。\">不符合《内蒙古自治区人口与计划生育条例》规定的生育条件，决定不予发证。</option>");
                            s.Append("<option value=\"证明材料不齐，通知申请人补正。\">证明材料不齐，通知申请人补正。</option>");
                        }
                        break;
                        //if (this.BizStep == "1" || this.BizStep == "2")
                        //{
                        //    s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                        //    s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        //}
                        //else if (this.BizStep == "3")
                        //{
                        //    s.Append("<option value=\"同意上报审核。\">同意上报审核。</option>");
                        //    s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        //}
                        //else
                        //{
                        //    string babyNum = CommPage.GetSingleVal("SELECT Fileds18 FROM BIZ_Contents WHERE BizID=" + bizID);
                        //    s.Append("<option value=\"符合    规定的" + babyNum + "孩生育条件；同意发证。\">符合    规定的" + babyNum + "孩生育条件；同意发证。</option>");
                        //    s.Append("<option value=\"不符合     规定的" + babyNum + "孩生育条件，决定不予发证。\">不符合     规定的" + babyNum + "孩生育条件，决定不予发证。</option>");
                        //    s.Append("<option value=\"证明材料不齐，通知申请人补正。\">证明材料不齐，通知申请人补正。</option>");
                        //}
                        //break;
                    case "0103":// 0103	3	计生家庭奖励扶助对象申报审核
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"准予享受计划生育家庭奖励扶助政策。\">准予享受计划生育家庭奖励扶助政策。</option>");
                            s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                        }
                        break;
                    case "0104":// 0104	3	计生家庭特别扶助对象申请审核
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"准予享受计划生育家庭特别扶助政策。\">准予享受计划生育家庭特别扶助政策。</option>");
                            s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                        }
                        break;
                    case "0105":// 0105	3	“少生快富”工程申请审核
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"准予享受计划生育“少生快富”工程扶持政策。\">准予享受计划生育“少生快富”工程扶持政策。</option>");
                            s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                        }
                        break;
                    case "0106":// 0106	4	结扎家庭奖励申请审核
                        if (this.BizStep == "1" || this.BizStep == "2" || this.BizStep == "3")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        //else if (this.BizStep == "3"){}
                        else
                        {
                            s.Append("<option value=\"准予享受计划生育结扎   （双女，蒙古族三女，二孩）结扎奖励相关政策。\">准予享受计划生育结扎   （双女，蒙古族三女，二孩）结扎奖励相关政策。</option>");
                            s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                        }
                        break;
                    case "0107":// 0107	3	“一杯奶”受益对象申请审核
                        if (this.BizStep == "1")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        else
                        {
                            s.Append("<option value=\"准予享受。\">准予享受。</option>");
                            s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                        }
                        break;
                    case "0108":// 0108	3	《独生子女父母光荣证》审核
                        if (stepTotal == "3")
                        {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"准予发证。\">准予发证。</option>");
                                s.Append("<option value=\"证明材料不齐，通知申请人补正。\">证明材料不齐，通知申请人补正。</option>");
                            }
                        }
                        else
                        {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"准予发证。\">准予发证。</option>");
                                s.Append("<option value=\"证明材料不齐，通知申请人补正。\">证明材料不齐，通知申请人补正。</option>");
                            }
                        }
                        break;
                    case "0109":// 0109	3	《流动人口婚育证明》申请审核
                        if (stepTotal == "2")
                        {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"情况属实，同意办证。\">情况属实，同意办证。</option>");
                                s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                            }
                        }
                        else
                        {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"情况属实，同意办证。\">情况属实，同意办证。</option>");
                                s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                            }
                        }
                        break;
                    case "0110": // 0110	3	婚育情况证明申请
                        if (stepTotal == "3")
                        {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"情况属实。\">情况属实。</option>");
                                s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                            }
                        }
                        else
                        {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"情况属实。\">情况属实。</option>");
                                s.Append("<option value=\"不符合相关规定。\">不符合相关规定。</option>");
                            }
                        }
                        break;
                    case "0111":// 0111	6	终止妊娠申请审核 嘎查/村/居,镇/苏木/乡/街办/场,医院门诊,医院儿科,医院妇产科,库伦旗
                        if (stepTotal == "3")
                        {
                            if (this.BizStep == "1" || this.BizStep == "2")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"不符合终止妊娠条件（）请通知申请人\">不符合终止妊娠条件（）请通知申请人</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"同意终止妊娠。\">同意终止妊娠。</option>");
                                s.Append("<option value=\"不同意终止妊娠。\">不同意终止妊娠。</option>");
                            }
                        }
                        else
                        {
                            if (this.BizStep == "1")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"不符合     。\">不符合     。</option>");
                            }
                            else if (this.BizStep == "2")
                            {
                                s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                                s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                            }
                            else if (this.BizStep == "3" || this.BizStep == "4" || this.BizStep == "5")
                            {
                                s.Append("<option value=\"胎儿死胎\">胎儿死胎</option>");
                                s.Append("<option value=\"胎儿畸形\">胎儿畸形</option>");
                                s.Append("<option value=\"宫内窘迫症\">宫内窘迫症</option>");
                                s.Append("<option value=\"不能继续妊娠\">不能继续妊娠</option>");
                                s.Append("<option value=\"孕者心脏病\">孕者心脏病</option>");
                                s.Append("<option value=\"孕者高血压\">孕者高血压</option>");
                                s.Append("<option value=\"孕者慢性感染\">孕者慢性感染</option>");
                                s.Append("<option value=\"孕者肾病综合症\">孕者肾病综合症</option>");
                                s.Append("<option value=\"孕者严重贫血\">孕者严重贫血</option>");
                                s.Append("<option value=\"其它\">其它</option>");
                            }
                            else
                            {
                                s.Append("<option value=\"同意终止妊娠。\">同意终止妊娠。</option>");
                                s.Append("<option value=\"不符合终止妊娠条件，不同意终止妊娠。\">不符合终止妊娠条件，不同意终止妊娠。</option>");
                            }
                        }
                        break;
                    case "0122":// 0102	4	再生育登记审核
                        if (this.BizStep == "1" || this.BizStep == "2")
                        {
                            s.Append("<option value=\"情况属实，同意上报审核。\">情况属实，同意上报审核。</option>");
                            s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        }
                        //else if (this.BizStep == "3")
                        //{
                        //    s.Append("<option value=\"同意上报审核。\">同意上报审核。</option>");
                        //    s.Append("<option value=\"证明材料不全，通知申请人补充。\">证明材料不全，通知申请人补充。</option>");
                        //}
                        else
                        {
                            s.Append("<option value=\"请选择引用条款……\">根据《内蒙古自治区人口与计划生育条例》第 * 条的规定，符合生育条件，同意发证。</option>");
                            s.Append("<option value=\"不符合《内蒙古自治区人口与计划生育条例》规定的生育条件，决定不予发证。\">不符合《内蒙古自治区人口与计划生育条例》规定的生育条件，决定不予发证。</option>");
                            s.Append("<option value=\"证明材料不齐，通知申请人补正。\">证明材料不齐，通知申请人补正。</option>");
                        
                            //string babyNum = CommPage.GetSingleVal("SELECT Fileds18 FROM BIZ_Contents WHERE BizID=" + bizID);
                            //s.Append("<option value=\"符合    规定的" + babyNum + "孩生育条件；同意发证。\">符合    规定的" + babyNum + "孩生育条件；同意发证。</option>");
                            //s.Append("<option value=\"不符合     规定的" + babyNum + "孩生育条件，决定不予发证。\">不符合     规定的" + babyNum + "孩生育条件，决定不予发证。</option>");
                            //s.Append("<option value=\"证明材料不齐，通知申请人补正。\">证明材料不齐，通知申请人补正。</option>");
                        }
                        break;
                    /*
政策外：
村级：
1.情况属实，同意上报审核。
2.不符合终止妊娠条件（）请通知申请人。
镇级：
1. 情况属实，同意上报审核。
2. 不符合终止妊娠条件（）请通知申请人。
旗级：
1.同意终止妊娠
2.不同意终止妊娠
申请理由：政策内申请终止妊娠/政策外申请终止妊娠

                     */
                    default:
                        s.Append("<option value=\"\">该业务审核意见不存在</option>");
                        break;
                }

            }
            else
            {
                s.Append("<option value=\"\">该业务审核意见不存在</option>");
            }
            s.Append("</select>");


            // 引用条款
            if (this.BizCode == "0101")
            {
                if (this.BizStep == "2" || this.BizStep == "3")
                {
                    s.Append("<br/>");
                    s.Append("<select name=\"selAuditItems\" id=\"selAuditItems\" onchange=\" SetTxtCommValByFilter(this.options[this.options.selectedIndex].value)\">");
                    s.Append("<option value=\"\">请选择一孩生育登记引用条款……</option>");
                    //s.Append("<option value=\"第十七条\">汉族家庭：第十七条第一款，一对夫妻只生育一个子女</option>");
                    //s.Append("<option value=\"第十八条\">蒙古族家庭：第十八条，蒙古族公民，一对夫妻可以生育两个子女。</option>");
                    s.Append("<option value=\"第十七条\">第十七条 提倡一对夫妻生育两个子女。尊重达斡尔族、鄂温克族、鄂伦春族公民的生育意愿。 </option>");
                    s.Append("</select>");
                }
            }
            else if (this.BizCode == "0102")
            {
                if (this.BizStep == "2" || this.BizStep == "3")
                {
                    s.Append("<br/>");
                    s.Append("<select name=\"selAuditItems\" id=\"selAuditItems\" onchange=\" SetTxtCommValByFilter(this.options[this.options.selectedIndex].value)\">");
                    s.Append("<option value=\"\">请选择二孩生育登记引用条款……</option>");
                    //s.Append("<option value=\"第十七条第一款\">第十七条第一款</option>");
                    //s.Append("<option value=\"第十七条第二款第一项\">第十七条第二款第一项</option>");
                    //s.Append("<option value=\"第十七条第二款第二项\">第十七条第二款第二项</option>");
                    //s.Append("<option value=\"第十七条第二款第三项\">第十七条第二款第三项</option>");
                    //s.Append("<option value=\"第十七条第二款第四项\">第十七条第二款第四项</option>");
                    //s.Append("<option value=\"第十七条第二款第五项\">第十七条第二款第五项</option>");
                    //s.Append("<option value=\"第十七条第二款第六项\">第十七条第二款第六项</option>");
                    //s.Append("<option value=\"第十八条第一款\">第十八条第一款</option>");
                    //s.Append("<option value=\"第十八条第二款\">第十八条第二款</option>");
                    //s.Append("<option value=\"第十八条第三款\">第十八条第三款</option>");
                    //s.Append("<option value=\"第十八条第四款\">第十八条第四款</option>");
                    //s.Append("<option value=\"第十九条第一款第一项\">第十九条第一款第一项</option>");
                    //s.Append("<option value=\"第十九条第一款第二项\">第十九条第一款第二项</option>");
                    //s.Append("<option value=\"第十九条第一款第三项\">第十九条第一款第三项</option>");
                    //s.Append("<option value=\"第十九条第一款第四项\">第十九条第一款第四项</option>");
                    //s.Append("<option value=\"第十九条第一款第五项\">第十九条第一款第五项</option>");
                    //s.Append("<option value=\"第十九条第二款第一项\">第十九条第二款第一项</option>");
                    //s.Append("<option value=\"第十九条第二款第二项\">第十九条第二款第二项</option>");
                    s.Append("<option value=\"第十七条\">第十七条 提倡一对夫妻生育两个子女。尊重达斡尔族、鄂温克族、鄂伦春族公民的生育意愿。 </option>");
                    s.Append("</select>");
                }
            }
            else if (this.BizCode == "0122")
            {
                if (this.BizStep == "3" || this.BizStep == "4")
                {
                    s.Append("<br/>");
                    s.Append("<select name=\"selAuditItems\" id=\"selAuditItems\" onchange=\" SetTxtCommValByFilter(this.options[this.options.selectedIndex].value)\">");
                    s.Append("<option value=\"\">请选择再生育审批引用条款……</option>");
                    //s.Append("<option value=\"第十七条第一款\">第十七条第一款</option>");
                    //s.Append("<option value=\"第十七条第二款第一项\">第十七条第二款第一项</option>");
                    //s.Append("<option value=\"第十七条第二款第二项\">第十七条第二款第二项</option>");
                    //s.Append("<option value=\"第十七条第二款第三项\">第十七条第二款第三项</option>");
                    //s.Append("<option value=\"第十七条第二款第四项\">第十七条第二款第四项</option>");
                    //s.Append("<option value=\"第十七条第二款第五项\">第十七条第二款第五项</option>");
                    //s.Append("<option value=\"第十七条第二款第六项\">第十七条第二款第六项</option>");
                    //s.Append("<option value=\"第十八条第一款\">第十八条第一款</option>");
                    //s.Append("<option value=\"第十八条第二款\">第十八条第二款</option>");
                    //s.Append("<option value=\"第十八条第三款\">第十八条第三款</option>");
                    //s.Append("<option value=\"第十八条第四款\">第十八条第四款</option>");
                    //s.Append("<option value=\"第十九条第一款第一项\">第十九条第一款第一项</option>");
                    //s.Append("<option value=\"第十九条第一款第二项\">第十九条第一款第二项</option>");
                    //s.Append("<option value=\"第十九条第一款第三项\">第十九条第一款第三项</option>");
                    //s.Append("<option value=\"第十九条第一款第四项\">第十九条第一款第四项</option>");
                    //s.Append("<option value=\"第十九条第一款第五项\">第十九条第一款第五项</option>");
                    //s.Append("<option value=\"第十九条第二款第一项\">第十九条第二款第一项</option>");
                    //s.Append("<option value=\"第十九条第二款第二项\">第十九条第二款第二项</option>");
                    s.Append("<option value=\"第十八条第一项\">第十八条第一项 两个子女中有病残儿、不能成长为正常劳动力，医学上认为夫妻可以再生育的</option>");
                    s.Append("<option value=\"第十八条第二项\">第十八条第二项 再婚夫妻（不含复婚），再婚前合计生育过两个及以上子女，再婚后未生育子女的</option>");
                    s.Append("<option value=\"第十八条第三项\">第十八条第三项 再婚夫妻（不含复婚），再婚前合计只生育过一个子女，再婚后生育一个子女的</option>");
                    s.Append("<option value=\"第十八条第四项\">第十八条第四项 蒙古族公民，夫妻双方均为非城镇户籍且从事农牧业生产，已有两个子女均为女孩的</option>");
                    s.Append("<option value=\"第十八条第五项\">第十八条第五项 其他可以再生育的情形</option>");

                    s.Append("</select>");
                }
            }
            else { }

            return s.ToString();
        }
        /*
         金额选择(5,10元),发放方式(男女各发5元,女方所在单位发放,男方所在单位发放)，发放时间(默认发证年月,2015年1月到子女出生时间推14年,年月)
         */
        // SELECT Fileds18 FROM BIZ_Contents WHERE BizID=
        public string GetCertificateMemo(string childBirthday) {
            string startDate = DateTime.Parse(childBirthday).ToString("yyyy年MM月");
            string endDate = DateTime.Parse(childBirthday).AddYears(14).ToString("yyyy年MM月");
            startDate = DateTime.Now.ToString("yyyy年MM月");//从当前审核时间开始
            StringBuilder s = new StringBuilder();
            s.Append("<select name=\"selCerMome\" id=\"selCerMome\">");
            s.Append("<option value=\"\">请选择保健费发放金额以及发放方式……</option>");
            s.Append("<option value=\"每月10元，每月男（女）方各发5元整。从" + startDate + "至" + endDate + "止。\">保健费：每月10元，每月男（女）方各发5元整。从" + startDate + "至" + endDate + "止。</option>");
            s.Append("<option value=\"每月10元，由男方所在单位发放10元。从" + startDate + "至" + endDate + "止。\">保健费：每月10元，由男方所在单位发放10元。从" + startDate + "至" + endDate + "止。</option>");
            s.Append("<option value=\"每月10元，由女方所在单位发放10元。从" + startDate + "至" + endDate + "止。\">保健费：每月10元，由女方所在单位发放10元。从" + startDate + "至" + endDate + "止。</option>");
            s.Append("</select>");
            return s.ToString();
        }
         
    }
}
