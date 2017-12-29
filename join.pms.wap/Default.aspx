<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="join.pms.wap._Default" %>
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
<title>勉县扶贫资金全程监测预警管理系统</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <!--首页头部 -->
<uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>
<div class="index_block">
  <!--顶部banner -->
  <!-- #include file="scripts/banner/banner.asp"-->
  <div class="clr10"></div>
  
  <!--进度查询 -->
  <!--<div class="part_01 clearfix">
	<p class="text">请您点击相应办事项目进行网上申请办事，您足不出户就能提交办事申请，享受到方便快捷的办事服务！ </p>
    <p class="button"><a href="#"><img src="/images/png_01.png" alt="查询" /></a></p>
  </div> -->
  
  <!--事项申请 -->
  <!--<div class="part_01 clearfix">
	<p class="ps"><b>友情提示：</b>欢迎使用本平台申请办理以下事项，申请时请点击相应图标</p>
  </div> -->
  
  <div class="part_07">
    <div class="part_t">
	  <span class="ps">选择事项进行业务在线办理</span>
	  <p class="title">在线办理</p>
	</div>
    <div class="part_c">
<div class="wap_02">
  <ul>
    <li class="a1"><p><a href="/Svrs-BizCategories/0101-1.shtml">一孩生育登记</a></p></li>
    <li class="a2"><p><a href="/Svrs-BizCategories/0102-1.shtml">二孩生育登记</a></p></li>
    <li class="a7"><p><a href="/Svrs-BizCategories/0122-1.shtml">再生育审批</a></p></li>
    <li class="a3"><p><a href="/Svrs-BizCategories/0103-1.shtml">奖励扶助</a></p></li>
    <li class="a4"><p><a href="/Svrs-BizCategories/0104-1.shtml">特别扶助</a></p></li>
    <li class="a5"><p><a href="/Svrs-BizCategories/0105-1.shtml">少生快富</a></p></li>
    <!--li class="a6"><p><a href="/Svrs-BizCategories/0107-1.shtml">一杯奶</a></p></li-->
    <li class="a8"><p><a href="/Svrs-BizCategories/0109-1.shtml">流动人口婚育证明</a></p></li>
    <li class="a9"><p><a href="/Svrs-BizCategories/0110-1.shtml">婚育情况证明</a></p></li>
  </ul>
  <div class="clr"></div>
</div>
	</div>
  </div>
  
  <div class="part_09">
    <div class="part_t">
	  <span class="ps">查看办事指南提前准备资料</span>
	  <p class="title">办事指南</p>
	</div>
    <div class="part_c">
<table border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td><a href="/Svrs-BizCategories/0112-3.shtml"><img src="/images/but/but_10.jpg" /></a></td>
    <td><a href="/Svrs-BizCategories/0113-3.shtml"><img src="/images/but/but_11.jpg" /></a></td>
    <td class="off"><a href="/Svrs-BizCategories/0114-3.shtml"><img src="/images/but/but_12.jpg" /></a></td>
  </tr>
</table>
	</div>
  </div>
  
</div>
<uc2:Uc_Footer id="Uc_Footer1" runat="server"/>   
    </form>
</body>
</html>