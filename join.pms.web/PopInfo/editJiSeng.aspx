<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editJiSeng.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.editJiSeng" %>

<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：次月5日前将上月情况提交到本系统；镇级计生服务站落实节育措施情况由县站统一收集上报……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<br/>
<!--妇女姓名,出生年月,婚姻状况,登记日期,生育情况,身份证号码,户籍地,现居住地,店铺名称,店铺地址-->
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="120"  bgcolor="#CCFFFF">数据日期：</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="PopCalendar(txtReportDate);return false;"  runat="server" />
    <input id="Image2" onclick="PopCalendar(txtReportDate);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
    (填报时间,该数据实际的归属月份,请选择到该数据月之内)
    </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu"  bgcolor="#CCFFFF">数据单位：</td>
  <td align="left"><asp:DropDownList ID="DDLReportArea" runat="server"><asp:ListItem>请选择</asp:ListItem></asp:DropDownList></td>
</tr></table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="120">姓名：</td>
    <td align="left" width="300"><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
 <td align="right" height="25" class="zhengwenjiacu" width="120">身份证号：</td>
  <td align="left"><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">出生日期：</td>
  <td align="left"><input id="txtFileds03" readonly="readonly" size="29" onclick="PopCalendar(txtFileds03);return false;"  runat="server" />
    <input id="Image3" onclick="PopCalendar(txtFileds03);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" /></td>
<td align="right" height="25" class="zhengwenjiacu"></td>
  <td align="left"></td>
</tr></table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="120">管理类型：</td>
  <td align="left" width="300"><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
<td align="right" height="25" class="zhengwenjiacu" width="120">本次免费项目：</td>
  <td align="left"><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">免费结算标准金额：</td>
  <td align="left"><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
 <td align="right" height="25" class="zhengwenjiacu">有效时间：</td>
  <td align="left"><input id="txtFileds07" readonly="readonly" size="29" onclick="PopCalendar(txtFileds07);return false;"  runat="server" />
    <input id="Image4" onclick="PopCalendar(txtFileds07);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" /></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">审核人：</td>
  <td align="left"><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  <td align="right" height="25" class="zhengwenjiacu">服务单位：</td>
  <td align="left"><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">回执人：</td>
  <td align="left"><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
 <td align="right" height="25" class="zhengwenjiacu">服务时间：</td>
  <td align="left"><input id="txtFileds11" readonly="readonly" size="29" onclick="PopCalendar(txtFileds11);return false;"  runat="server" />
    <input id="Image5" onclick="PopCalendar(txtFileds11);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" /></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">服务医生：</td>
  <td align="left"><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
<td align="right" height="25" class="zhengwenjiacu">服务机构：</td>
  <td align="left"><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">单位负责人：</td>
  <td align="left"><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
 <td align="right" height="25" class="zhengwenjiacu">详细：</td>
  <td align="left"><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px"></asp:TextBox></td>
</tr></table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">现居住地：</td>
  <td align="left">  
      <uc1:ucAreaSel id="UcAreaSel15" runat="server">
      </uc1:ucAreaSel></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">户籍所在地：</td>
  <td align="left">  
      <uc1:ucAreaSel id="UcAreaSel16" runat="server">
      </uc1:ucAreaSel></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">所属地区：</td>
  <td align="left">  
      <uc1:ucAreaSel id="UcAreaSel17" runat="server">
      </uc1:ucAreaSel></td>
</tr>
<tr class="zhengwen">
 </tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
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