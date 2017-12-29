<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizView.aspx.cs" Inherits="join.pms.wap.UserCenter.BizView" %>
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
<title>业务详细</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>
    <div class="block">
  <!--相关内容 -->
  <div class="part_05">
   <asp:Literal ID="LiteralBizView" runat="server" EnableViewState="false"></asp:Literal>	
	
	<div class="part_button"><input type="button" name="ButBackPage" value="返  回" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';"/></div>
  </div>
</div>
    <uc2:Uc_Footer id="Uc_Footer1" runat="server"/>
    </form>
</body>
</html>
