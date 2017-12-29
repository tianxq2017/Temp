<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnvCommChart.aspx.cs" Inherits="join.pms.web.UnvCommChart" %>

<%@ Register Assembly="Chartlet" Namespace="FanG" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>统计图</title>
    <link href="/css/index.css" type="text/css" rel="stylesheet">
    <link href="/css/right.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="/includes/dataGrid.js" type="text/javascript"></script>
<script language="javascript" src="/includes/commOA.js" type="text/javascript"></script>
<script language="javascript" src="/includes/CommBiz.js" type="text/javascript"></script>
  <script src="/Scripts/jsdate.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
<!--页面参数 -->
<input type="hidden" name="txtUrlParams" id="txtUrlParams" value="" runat="server" style="display:none;"/>
<input type="hidden" name="tbFuncNo" id="tbFuncNo" value="" runat="server" style="display:none;"/>
<!--页面参数 -->
    
<div class="mbx"><asp:Literal ID="LiteralNav" runat="server" /></div>
    <!--Start-->
<div id="Div1" class="small" style="width: 100%; position: relative;">
<table class="lvtBg" width="100%" border="0" cellpadding="0" cellspacing="1"><tbody><tr><td>

<!--查询条件-->
<table width="95%" border="0" cellspacing="0" cellpadding="0" class="small">
    <tr>
    <td width="70" height="30" align="left" bgcolor="#F4FAFA">图形属性：</td><td width="20" height="30" align="left" bgcolor="#F4FAFA">
    <asp:DropDownList ID="DDLShape" runat="server">
        <asp:ListItem Value="Bar">Bar （柱状图）</asp:ListItem>
		<asp:ListItem Value="Line">Line（折线图）</asp:ListItem>
		<asp:ListItem Value="Pie">Pie（饼图）</asp:ListItem>
    </asp:DropDownList>
    <!--
		<asp:ListItem Value="Stack">Stack (堆栈形状)</asp:ListItem>
		<asp:ListItem Value="HBar">HBar</asp:ListItem>
		<asp:ListItem Value="Trend">Trend</asp:ListItem>
		<asp:ListItem Value="Bubble">Bubble</asp:ListItem>
		<asp:ListItem Value="FloatBar">FloatBar</asp:ListItem>
		<asp:ListItem Value="Linear">Linear</asp:ListItem>
		<asp:ListItem Value="Histogram">Histogram</asp:ListItem>
		<asp:ListItem Value="BoxPlot">BoxPlot</asp:ListItem>
		-->
    </td><td width="20" align="left" bgcolor="#F4FAFA">&nbsp;</td>
    <td width="70" class="fb01" bgcolor="#F4FAFA">&nbsp;<!--维度：--></td>
    <td width="145" align="left" bgcolor="#F4FAFA"><!--
    <asp:DropDownList ID="DDLDimension" runat="server">
        <asp:ListItem Value="Chart2D">Chart2D（平面图）</asp:ListItem>
		<asp:ListItem Value="Chart3D">Chart3D（三维立体图像）</asp:ListItem>
    </asp:DropDownList>-->
    </td>
    <!--start-->
    <td width="20" align="left" bgcolor="#F4FAFA">&nbsp;</td>
    <td width="70" class="fb01" bgcolor="#F4FAFA">&nbsp;外观样式：</td>
    <td width="145" align="left" bgcolor="#F4FAFA">
    <asp:DropDownList ID="DDLStyle" runat="server"> 
		<asp:ListItem Value="Bar_2D_Breeze_NoCrystal_Glow_NoBorder">水晶发光</asp:ListItem>
		<asp:ListItem Value="Line_2D_StarryNight_ThickRound_NoGlow_NoBorder">无光晕无边框</asp:ListItem>
		<asp:ListItem Value="Pie_3D_Breeze_NoCrystal_NoGlow_NoBorder">无光晕</asp:ListItem>
    </asp:DropDownList>
    </td>
    <!--end-->
    <td width="20" align="left" bgcolor="#F4FAFA">&nbsp;</td>
    <td width="20" bgcolor="#F4FAFA">&nbsp;</td>
    <td align="left" bgcolor="#F4FAFA">&nbsp;</td>
    </tr>
    <tr>
    <td width="70" height="30" align="left" bgcolor="#F4FAFA">数据单位：</td><td width="20" height="30" align="left" bgcolor="#F4FAFA">
    <asp:DropDownList ID="DDLArea" runat="server"></asp:DropDownList>
    </td><td width="20" align="left" bgcolor="#F4FAFA">&nbsp;</td>
    <td width="70" class="fb01" bgcolor="#F4FAFA">&nbsp;开始日期：</td>
    <td width="145" align="left" bgcolor="#F4FAFA">
    <input id="txtStartTime" runat="server"  onclick="SelectDate(document.getElementById('txtStartTime'),'yyyy-MM-dd')" readonly="readonly"  />
    </td>
    <!--start-->
    <td width="20" align="left" bgcolor="#F4FAFA">&nbsp;</td>
    <td width="70" class="fb01" bgcolor="#F4FAFA">&nbsp;截止日期：</td>
    <td width="145" align="left" bgcolor="#F4FAFA">
     <input id="txtEndTime" runat="server"  onclick="SelectDate(document.getElementById('txtEndTime'),'yyyy-MM-dd')" readonly="readonly"  />
    </td>
    <!--end-->
    <td width="20" align="left" bgcolor="#F4FAFA">&nbsp;</td>
    <td width="20" bgcolor="#F4FAFA">&nbsp;</td>
    <td align="left" bgcolor="#F4FAFA">
    <asp:Button ID="btnAdd" runat="server" Text="图形查看" class="submit6" OnClick="btnAdd_Click"></asp:Button>
    <%--<%if (m_FuncCode != "0711" && m_FuncCode != "0712")
      { %>--%>
    <input type="button" name="ButBackPage" value="报表查看" id="ButBackPage" onclick="javascript:window.location.href='/UnvCommReport.aspx?<%=m_UrlParams %>';" class="submit6" />
    <%--<%} %>--%>
    </td>
    </tr>
    
</table>
<!--结果展示-->
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" >
<tr>
  <td height="25" align="right"></td>
  <td width="*" align="left"><cc1:chartlet id="Chartlet1" runat="server"></cc1:chartlet></td>
</tr>
<tr>
    <td align="right" height="10" ></td>
    <td align="left" width="*"></td>
</tr>
</table>
<!-- 操作按钮 -->

<!-- 主操作区 End-->
</td></tr></tbody></table></div>

    </form>
</body>
</html>