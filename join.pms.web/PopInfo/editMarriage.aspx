<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editMarriage.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.editMarriage" %>

<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
    <style type="text/css">
    <!--
    body {
	    margin-top: 10px;
	    background-color: #FFFFFF;
	    margin-left: 10px;
	    margin-right: 10px;
	    margin-bottom: 0px;
	    background-image: url(/images/mainyouxiabg.jpg);
	    background-position:right bottom;
	    background-repeat:no-repeat;
    }
    .butStyle {line-height: 20px; color: #000000; text-decoration: none; font-size: 12px;}
    -->
    </style>
<script language="javascript" type="text/javascript">
function PopCalendar(obj){
    showx = event.screenX - event.offsetX - 4 - 10 ; // + deltaX;
    showy = event.screenY - event.offsetY -168; // + deltaY;
    newWINwidth = 210 + 4 + 18;
    window.showModalDialog("/includes/Calendar.htm",obj,"dialogWidth:200px; dialogHeight:210px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; ");    
}
</script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列婚姻登记信息后；点击“提交”按钮（次月5日以前将上月情况提交）……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="100"  bgcolor="#CCFFFF">数据日期：</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="PopCalendar(txtReportDate);return false;"  runat="server" />
    <input id="Image2" onclick="PopCalendar(txtReportDate);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
    (填报时间,该数据实际的归属月份,请选择到该数据月之内)
    </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu"  bgcolor="#CCFFFF">数据单位：</td>
  <td align="left"><asp:DropDownList ID="DDLReportArea" runat="server"><asp:ListItem>请选择</asp:ListItem></asp:DropDownList></td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="100">业务类型：</td>
    <td align="left" width="*"><asp:DropDownList ID="DDLFileds01" runat="server">
            <asp:ListItem>结婚登记</asp:ListItem>
            <asp:ListItem>离婚登记</asp:ListItem>
        </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="100">证件号码：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="100">登记日期：</td>
    <td align="left" width="*"><input id="txtFileds03" readonly="readonly" size="29" onclick="PopCalendar(txtFileds03);return false;"  runat="server" />
    <input id="Image3" onclick="PopCalendar(txtFileds03);return false;" tabIndex="1" type="image" src="/images/calendar.gif" align="absMiddle"></td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td width="100" height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">男方姓名：</td>
    <td align="left" width="300"><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td width="100" align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">女方姓名：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<!--
业务类型,证件号码,登记时间,
Fileds01,Fileds02,Fileds03,
男方姓名,身份证号,职业,家庭地址,民族,文化程度,婚姻状况,出生日期,
Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,
女方姓名,身份证号,职业,家庭地址,民族,文化程度,婚姻状况,工作单位
Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19
-->
<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">身份证号：</td>
    <td align="left" width="300"><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">身份证号：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>

<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">职业：</td>
    <td align="left" width="300" >
    <asp:DropDownList ID="DDLFileds06" runat="server">
<asp:ListItem>国家机关、党群组织、企事业单位负责人</asp:ListItem>
<asp:ListItem>专业技术人员</asp:ListItem>
<asp:ListItem>公务员、办事人员和有关人员</asp:ListItem>
<asp:ListItem>经商</asp:ListItem>
<asp:ListItem>商贩</asp:ListItem>
<asp:ListItem>餐饮</asp:ListItem>
<asp:ListItem>家政</asp:ListItem>
<asp:ListItem>保洁</asp:ListItem>
<asp:ListItem>保安</asp:ListItem>
<asp:ListItem>装修</asp:ListItem>
<asp:ListItem>其它商业服务人员</asp:ListItem>
<asp:ListItem>农、林、牧、渔、水利业生产人员</asp:ListItem>
<asp:ListItem>生产</asp:ListItem>
<asp:ListItem>运输</asp:ListItem>
<asp:ListItem>建筑</asp:ListItem>
<asp:ListItem>其他生产、运输设备操作人员及有关人员</asp:ListItem>
<asp:ListItem>无固定职业</asp:ListItem>
<asp:ListItem>其它</asp:ListItem>
</asp:DropDownList>
    </td>
    <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">职业：</td>
    <td align="left" width="*" >
    <asp:DropDownList ID="DDLFileds14" runat="server">
<asp:ListItem>国家机关、党群组织、企事业单位负责人</asp:ListItem>
<asp:ListItem>专业技术人员</asp:ListItem>
<asp:ListItem>公务员、办事人员和有关人员</asp:ListItem>
<asp:ListItem>经商</asp:ListItem>
<asp:ListItem>商贩</asp:ListItem>
<asp:ListItem>餐饮</asp:ListItem>
<asp:ListItem>家政</asp:ListItem>
<asp:ListItem>保洁</asp:ListItem>
<asp:ListItem>保安</asp:ListItem>
<asp:ListItem>装修</asp:ListItem>
<asp:ListItem>其它商业服务人员</asp:ListItem>
<asp:ListItem>农、林、牧、渔、水利业生产人员</asp:ListItem>
<asp:ListItem>生产</asp:ListItem>
<asp:ListItem>运输</asp:ListItem>
<asp:ListItem>建筑</asp:ListItem>
<asp:ListItem>其他生产、运输设备操作人员及有关人员</asp:ListItem>
<asp:ListItem>无固定职业</asp:ListItem>
<asp:ListItem>其它</asp:ListItem>
</asp:DropDownList>
    </td>
</tr>

<tr class="zhengwen">
  <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">民族：</td>
  <td align="left" width="300" >
  <asp:DropDownList ID="DDLFileds08" runat="server">
<asp:ListItem>汉族</asp:ListItem>
<asp:ListItem>蒙古族</asp:ListItem>
<asp:ListItem>回族</asp:ListItem>
<asp:ListItem>藏族</asp:ListItem>
<asp:ListItem>维吾尔族</asp:ListItem>
<asp:ListItem>苗族</asp:ListItem>
<asp:ListItem>彝族</asp:ListItem>
<asp:ListItem>壮族</asp:ListItem>
<asp:ListItem>布衣族</asp:ListItem>
<asp:ListItem>朝鲜族</asp:ListItem>
<asp:ListItem>满族</asp:ListItem>
<asp:ListItem>侗族</asp:ListItem>
<asp:ListItem>瑶族</asp:ListItem>
<asp:ListItem>白族</asp:ListItem>
<asp:ListItem>土家族</asp:ListItem>
<asp:ListItem>哈尼族</asp:ListItem>
<asp:ListItem>哈萨克族</asp:ListItem>
<asp:ListItem>傣族</asp:ListItem>
<asp:ListItem>黎族</asp:ListItem>
<asp:ListItem>傈僳族</asp:ListItem>
<asp:ListItem>佤族</asp:ListItem>
<asp:ListItem>畲族</asp:ListItem>
<asp:ListItem>高山族</asp:ListItem>
<asp:ListItem>拉祜族</asp:ListItem>
<asp:ListItem>水族</asp:ListItem>
<asp:ListItem>东乡族</asp:ListItem>
<asp:ListItem>纳西族</asp:ListItem>
<asp:ListItem>景颇族</asp:ListItem>
<asp:ListItem>柯尔克孜族</asp:ListItem>
<asp:ListItem>土族</asp:ListItem>
<asp:ListItem>达斡族</asp:ListItem>
<asp:ListItem>仫佬族</asp:ListItem>
<asp:ListItem>羌族</asp:ListItem>
<asp:ListItem>布朗族</asp:ListItem>
<asp:ListItem>撒拉族</asp:ListItem>
<asp:ListItem>毛南族</asp:ListItem>
<asp:ListItem>仡佬族</asp:ListItem>
<asp:ListItem>锡伯族</asp:ListItem>
<asp:ListItem>阿昌族</asp:ListItem>
<asp:ListItem>普米族</asp:ListItem>
<asp:ListItem>塔吉克族</asp:ListItem>
<asp:ListItem>怒族</asp:ListItem>
<asp:ListItem>乌孜别克族</asp:ListItem>
<asp:ListItem>俄罗斯族</asp:ListItem>
<asp:ListItem>鄂温克族</asp:ListItem>
<asp:ListItem>德昂族</asp:ListItem>
<asp:ListItem>保安族</asp:ListItem>
<asp:ListItem>裕固族</asp:ListItem>
<asp:ListItem>京族</asp:ListItem>
<asp:ListItem>塔塔尔族</asp:ListItem>
<asp:ListItem>独龙族</asp:ListItem>
<asp:ListItem>鄂伦春族</asp:ListItem>
<asp:ListItem>赫哲族</asp:ListItem>
<asp:ListItem>门巴族</asp:ListItem>
<asp:ListItem>珞巴族</asp:ListItem>
<asp:ListItem>基诺族</asp:ListItem>
<asp:ListItem>未识别</asp:ListItem>
<asp:ListItem>外入中籍</asp:ListItem>
</asp:DropDownList>

  </td>
  <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">民族：</td>
  <td align="left" width="*" ><asp:DropDownList ID="DDLFileds16" runat="server">
<asp:ListItem>汉族</asp:ListItem>
<asp:ListItem>蒙古族</asp:ListItem>
<asp:ListItem>回族</asp:ListItem>
<asp:ListItem>藏族</asp:ListItem>
<asp:ListItem>维吾尔族</asp:ListItem>
<asp:ListItem>苗族</asp:ListItem>
<asp:ListItem>彝族</asp:ListItem>
<asp:ListItem>壮族</asp:ListItem>
<asp:ListItem>布衣族</asp:ListItem>
<asp:ListItem>朝鲜族</asp:ListItem>
<asp:ListItem>满族</asp:ListItem>
<asp:ListItem>侗族</asp:ListItem>
<asp:ListItem>瑶族</asp:ListItem>
<asp:ListItem>白族</asp:ListItem>
<asp:ListItem>土家族</asp:ListItem>
<asp:ListItem>哈尼族</asp:ListItem>
<asp:ListItem>哈萨克族</asp:ListItem>
<asp:ListItem>傣族</asp:ListItem>
<asp:ListItem>黎族</asp:ListItem>
<asp:ListItem>傈僳族</asp:ListItem>
<asp:ListItem>佤族</asp:ListItem>
<asp:ListItem>畲族</asp:ListItem>
<asp:ListItem>高山族</asp:ListItem>
<asp:ListItem>拉祜族</asp:ListItem>
<asp:ListItem>水族</asp:ListItem>
<asp:ListItem>东乡族</asp:ListItem>
<asp:ListItem>纳西族</asp:ListItem>
<asp:ListItem>景颇族</asp:ListItem>
<asp:ListItem>柯尔克孜族</asp:ListItem>
<asp:ListItem>土族</asp:ListItem>
<asp:ListItem>达斡族</asp:ListItem>
<asp:ListItem>仫佬族</asp:ListItem>
<asp:ListItem>羌族</asp:ListItem>
<asp:ListItem>布朗族</asp:ListItem>
<asp:ListItem>撒拉族</asp:ListItem>
<asp:ListItem>毛南族</asp:ListItem>
<asp:ListItem>仡佬族</asp:ListItem>
<asp:ListItem>锡伯族</asp:ListItem>
<asp:ListItem>阿昌族</asp:ListItem>
<asp:ListItem>普米族</asp:ListItem>
<asp:ListItem>塔吉克族</asp:ListItem>
<asp:ListItem>怒族</asp:ListItem>
<asp:ListItem>乌孜别克族</asp:ListItem>
<asp:ListItem>俄罗斯族</asp:ListItem>
<asp:ListItem>鄂温克族</asp:ListItem>
<asp:ListItem>德昂族</asp:ListItem>
<asp:ListItem>保安族</asp:ListItem>
<asp:ListItem>裕固族</asp:ListItem>
<asp:ListItem>京族</asp:ListItem>
<asp:ListItem>塔塔尔族</asp:ListItem>
<asp:ListItem>独龙族</asp:ListItem>
<asp:ListItem>鄂伦春族</asp:ListItem>
<asp:ListItem>赫哲族</asp:ListItem>
<asp:ListItem>门巴族</asp:ListItem>
<asp:ListItem>珞巴族</asp:ListItem>
<asp:ListItem>基诺族</asp:ListItem>
<asp:ListItem>未识别</asp:ListItem>
<asp:ListItem>外入中籍</asp:ListItem>
</asp:DropDownList>
</td>
</tr>

<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">文化程度：</td>
    <td align="left" width="300" >
    <asp:DropDownList ID="DDLFileds09" runat="server">
    <asp:ListItem>小学</asp:ListItem>
    <asp:ListItem>初中</asp:ListItem>
      <asp:ListItem>高中/职专</asp:ListItem>
      <asp:ListItem>大专</asp:ListItem>
      <asp:ListItem>本科</asp:ListItem>
      <asp:ListItem>研究生</asp:ListItem>
    </asp:DropDownList>
    </td>
    <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">文化程度：</td>
    <td align="left" width="*" ><asp:DropDownList ID="DDLFileds17" runat="server">
    <asp:ListItem>小学</asp:ListItem>
    <asp:ListItem>初中</asp:ListItem>
      <asp:ListItem>高中/职专</asp:ListItem>
      <asp:ListItem>大专</asp:ListItem>
      <asp:ListItem>本科</asp:ListItem>
      <asp:ListItem>研究生</asp:ListItem>
    </asp:DropDownList></td>
</tr>

<tr class="zhengwen">
  <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">婚姻状况：</td>
  <td align="left" width="300" ><asp:DropDownList ID="DDLFileds10" runat="server">
            <asp:ListItem>初婚</asp:ListItem>
            <asp:ListItem>再婚</asp:ListItem>
            <asp:ListItem>复婚</asp:ListItem>
            <asp:ListItem>离婚</asp:ListItem>
        </asp:DropDownList></td>
  <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">婚姻状况：</td>
  <td align="left" width="*" ><asp:DropDownList ID="DDLFileds18" runat="server">
            <asp:ListItem>初婚</asp:ListItem>
            <asp:ListItem>再婚</asp:ListItem>
            <asp:ListItem>复婚</asp:ListItem>
            <asp:ListItem>离婚</asp:ListItem>
        </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">出生日期：</td>
    <td align="left" width="300">
    <input id="txtFileds11" readonly="readonly" size="29" onclick="PopCalendar(txtFileds11);return false;"  runat="server" />
    <input id="Image1" onclick="PopCalendar(txtFileds11);return false;" tabIndex="1" type="image" src="/images/calendar.gif" align="absMiddle">
    </td>
    <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">出生日期：</td>
    <td align="left" width="*">
    <input id="txtFileds19" readonly="readonly" size="29" onclick="PopCalendar(txtFileds19);return false;"  runat="server" />
    <input id="Image4" onclick="PopCalendar(txtFileds19);return false;" tabIndex="1" type="image" src="/images/calendar.gif" align="absMiddle">
    </td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="100">男方详细地址：</td>
    <td align="left" width="*">
        <uc1:ucAreaSel ID="UcAreaSel07" runat="server" />
    </td>
</tr>
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="100">女方详细地址：</td>
    <td align="left" width="*">
        <uc1:ucAreaSel ID="UcAreaSel15" runat="server" />
    </td>
</tr>

</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>
