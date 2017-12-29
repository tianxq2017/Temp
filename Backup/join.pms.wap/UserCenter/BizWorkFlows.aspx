<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizWorkFlows.aspx.cs" Inherits="join.pms.wap.UserCenter.BizWorkFlows" %>
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
<title>业务流程</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>
    <div class="block">
  <!--备注 -->
  <div class="part_03">
	<p class="title">温馨提示</p>
    <p class="sum">该工作流程中，绿色表示已处理的审核流程；灰色表示尚未处理！</p>
  </div>
  <div class="clr10"></div>
  
  <!--相关内容 -->
  <div class="part_05">
	<div class="part_form">
	  <div class="handle_progress">
        <ul>  <asp:Literal ID="LiteralFlows" runat="server" EnableViewState="false"/> 
		  
		</ul>
	  </div>
	</div>
	
	<div class="part_button"><!--<input type="submit" name="Submit" value="我要申请" /> --><a href="/">返回首页</a></div>
  </div>
</div>
      <uc2:Uc_Footer id="Uc_Footer1" runat="server"/>
    </form>
</body>
</html>
