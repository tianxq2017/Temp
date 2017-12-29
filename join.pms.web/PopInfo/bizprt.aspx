<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bizprt.aspx.cs" Inherits="AreWeb.OnlineCertificate.PopInfo.bizprt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
    body {
	    margin-top: 10px;
	    background-color: #FFFFFF;
	    margin-left: 10px;
	    margin-right: 10px;
	    margin-bottom: 0px;
    }
.font18 {font-size:18px;}
.font19 {font-size:19px;}
.font20 {font-size:20px;}
/*一孩生育（怀孕）证明 农村*/
.dy1101 {padding-top:80px;margin:0 auto;width:650px;}
.dy1101 .bt_01 {font-size: 30px;padding-bottom:30px;}
.dy1101 .bt_02 td {font-size: 16px;padding-bottom:5px;}
.dy1101 .bt_03 {padding-top:20px;}
.dy1101 .bt_04 {padding-top:8px;}
.dy1101 .table_01 td {text-align:center;height:90px;padding:0 5px;}
.dy1101 .table_01 .t_l {text-align: left;}
.dy1101 .table_01 {border-top:1px solid #000;border-left:1px solid #000;}
.dy1101 .table_01 td {border-right:1px solid #000;border-bottom:1px solid #000;}
.dy1101 .table_01 td td {border-right: none;border-bottom: none;}
.dy1101 .table_01 table {border-top:none;border-left:none;}
.dy1101 .table_01 .x_r {border-right:1px solid #000;}
/*一孩生育（怀孕）证明 城镇*/
.dy1102 {padding-top:80px;margin:0 auto;width:650px;}
.dy1102 .bt_01 {font-size: 30px;padding-bottom:30px;}
.dy1102 .bt_02 td {font-size: 16px;padding-bottom:5px;}
.dy1102 .bt_03 {padding-top:20px;}
.dy1102 .bt_04 {padding-top:8px;}
.dy1102 .table_01 td {text-align:center;height:90px;padding:0 5px;}
.dy1102 .table_01 .t_l {text-align: left;}
.dy1102 .table_01 {border-top:1px solid #000;border-left:1px solid #000;}
.dy1102 .table_01 td {border-right:1px solid #000;border-bottom:1px solid #000;}
.dy1102 .table_01 td td {border-right: none;border-bottom: none;}
.dy1102 .table_01 table {border-top:none;border-left:none;}
.dy1102 .table_01 .x_r {border-right:1px solid #000;}
/*终止妊娠手术审批表*/
.dy1103 {padding-top:30px;margin:0 auto;width:650px;}
.dy1103 p {margin:0;}
.dy1103 .bt_01 {font-size: 30px;padding-bottom:20px;}
.dy1103 .bt_02 td {font-size: 16px;padding-bottom:5px;}
.dy1103 .bt_03 {padding-top:10px;text-align: left;line-height:25px;}
.dy1103 .table_01 td {text-align:center;height:60px;padding:0 5px;}
.dy1103 .table_01 .t_l {text-align: left;}
.dy1103 .table_01 .t_r {text-align: right;}
.dy1103 .table_01 {border-top:1px solid #000;border-left:1px solid #000;}
.dy1103 .table_01 td {border-right:1px solid #000;border-bottom:1px solid #000;}
.dy1103 .table_01 td td {border-right: none;	border-bottom: none;}
.dy1103 .table_01 table {border-top:none;border-left:none;}
.dy1103 .table_01 .x_r {border-right:1px solid #000;}
/*终止妊娠通知单*/
.dy1104 {/*width:100%;*/padding-top:80px;margin:0 auto;width:1000px;}
.dy1104 td {font-size:20px;}
.dy1104 .xt {width:100%;border-right:1px dotted #000;}
.dy1104 .left {border-right:1px dotted #000;padding-right:30px;width:50%;}
.dy1104 .right {text-align:right;padding-left:30px;width:50%;}
.dy1104 .bt_01 {font-size: 30px;padding-bottom:15px;font-weight:bold;}
.dy1104 .bt_02 {font-size: 25px;height:80px;}
.dy1104 .bt_03 {padding-top:80px;}
.dy1104 .bt_04 {padding-top:8px;}
.dy1104 .table_01 {line-height:45px;}
.dy1104 .table_01 span {padding:0 5px;text-decoration:underline;}
.dy1104 .table_01 .a2 {text-align:center;}
.dy1104 .table_01 .sj {text-indent: 2em;}
/*出生婴儿实名登记单(乡镇)*/
.dy1105 {/*width:100%;*/padding-top:80px;margin:0 auto;width:1000px;}
.dy1105 .cssTitle {font-size: 16px;}
.dy1105 .xt {width:100%;border-right:1px dotted #000;}
.dy1105 .left {border-right:1px dotted #000;padding-right:30px;}
.dy1105 .right {text-align:right;padding-left:30px;}
.dy1105 .bt_01 {font-size: 20px;padding-bottom:20px;}
.dy1105 .bt_02 td {font-size: 16px;padding-bottom:5px;}
.dy1105 .bt_03 {padding-top:20px;}
.dy1105 .bt_04 {padding-top:8px;}
.dy1105 .table_01 td {text-align:center;height:80px;}
/*出生婴儿实名登记单(县局)*/
.dy1106 {/*width:100%;*/	padding-top:80px;margin:0 auto;width:1000px;}
.dy1106 .cssTitle {font-size: 16px;}
.dy1106 {/*width:100%;*/padding-top:80px;margin:0 auto;width:1000px;}
.dy1106 .xt {width:100%;border-right:1px dotted #000;}
.dy1106 .left {border-right:1px dotted #000;padding-right:30px;}
.dy1106 .right {text-align:right;padding-left:30px;}
.dy1106 .bt_01 {font-size: 20px;padding-bottom:20px;}
.dy1106 .bt_02 td {font-size: 16px;padding-bottom:5px;}
.dy1106 .bt_03 {padding-top:20px;}
.dy1106 .bt_04 {padding-top:8px;}
.dy1106 .table_01 td {text-align:center;height:80px;}
 
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Literal ID="LiteralData" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
