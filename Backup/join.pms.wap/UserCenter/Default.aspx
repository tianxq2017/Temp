<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="join.pms.wap.UserCenter.Default" %>
<%@ Register Src="/userctrl/Uc_PageTop.ascx" TagName="Uc_PageTop" TagPrefix="uc1" %>
<%@ Register Src="/userctrl/Uc_Footer.ascx" TagName="Uc_Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta name="apple-mobile-web-app-capable" content="yes" />
<meta name="apple-mobile-web-app-status-bar-style" content="black" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0, maximum-scale=1.0,user-scalable=yes" />
<!--<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0 , maximum-scale=1.0, user-scalable=0">-->
<meta name="generator" content="bd-mobcard" />
<meta name="format-detection" content="telephone=yes" />
<meta http-equiv="content-type" content="application/vnd.wap.xhtml+xml;charset=UTF-8"/>
<title>群众中心</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
  <uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>
  <div class="block">
    <!--会员卡 -->
  <div class="part_12">
    <div class="pic">
	  <img src="/images/common/user_card.jpg" />
	  <div class="title">  <asp:Literal ID="LiteralUserInfo" runat="server" EnableViewState="false"></asp:Literal>  	
	    
	  </div>
	</div>
  </div>
  <div class="clr10"></div>
  
  <!--会员管理 -->
  <div class="part_11">
    <ul>
	  <li class="a1"><a href="/">返回提交申请</a></li>
	  <li><a href="/OC/02-1.<%=m_FileExt %>">业务办理记录</a></li>
	  <li><a href="/UserCenter/BizWorkFlows.aspx?action=view">业务办理进度</a></li>
	  <li><a href="/OC/ModifyPwd.<%=m_FileExt %>">登录密码管理</a></li>
    </ul>
  </div>
</div>
  <uc2:Uc_Footer id="Uc_Footer1" runat="server"/>
    </form>
</body>
</html>
