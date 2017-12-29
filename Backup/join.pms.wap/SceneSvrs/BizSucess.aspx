<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizSucess.aspx.cs" Inherits="join.pms.wap.SceneSvrs.BizSucess" %>
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
<title>提交成功</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
    <uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>
   <div class="block">
  <!--申请步骤 -->
  <div class="flow_pic clearfix">
    <ul>
	  <li><p><b>1</b>填写申请资料</p></li>
	  <li><p><b>2</b>提交所需证件</p></li>
	  <li class="on"><p><b>3</b>申请成功</p></li>
	</ul>
  </div>
  
  <!--相关内容 -->
  <div class="part_06">
	<p class="title">系统提示</p>
    <p class="sum">恭喜您，您已经申请成功，请及时关注个人业务办理状态，审核成功后请携带相关证件资料到指定地点办理。</p>
    <p class="button"><a href="/">返回首页</a></p>
  </div>
</div><uc2:Uc_Footer id="Uc_Footer1" runat="server"/>
    </form>
</body>
</html>
