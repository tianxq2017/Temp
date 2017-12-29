<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataList.aspx.cs" Inherits="join.pms.wap.UserCenter.DataList" %>
<%@ Register Src="/userctrl/Uc_PageTop.ascx" TagName="Uc_PageTop" TagPrefix="uc1" %>
<%@ Register Src="/userctrl/Uc_Footer.ascx" TagName="Uc_Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta name="apple-mobile-web-app-capable" content="yes" />
<meta name="apple-mobile-web-app-status-bar-style" content="black" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0, maximum-scale=1.0,user-scalable=yes" />
<!--<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0 , maximum-scale=1.0, user-scalable=0">-->
<meta name="generator" content="bd-mobcard" />
<meta name="format-detection" content="telephone=yes" />
<meta http-equiv="content-type" content="application/vnd.wap.xhtml+xml;charset=UTF-8"/>
<title>用户列表</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
<script language="javascript" src="/Scripts/dataGrid.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
  <uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>
<div class="block">
  <!--备注 -->
  <!--<div class="part_03">
	<p class="title">温馨提示</p>
    <p class="sum">相关说明</p>
  </div>
  <div class="clr10"></div> -->
  
  <!--相关内容 -->
    <asp:Literal ID="LiteralDataList" runat="server" EnableViewState="False"/>
</div>
  <uc2:Uc_Footer id="Uc_Footer1" runat="server"/>
<!-- 页面参数 -->
<input type="hidden" id="txtUrlPageNo" name="txtUrlPageNo" value="" runat="server" style="display:none;"/>
    </form>
</body>
</html>
