﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizPrt0105.aspx.cs" Inherits="join.pms.web.BizInfo.BizPrt0105" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>计划生育“少生快富”工程申请审核表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link href="/BizInfo/css/printAppTable.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">

<div class="print_0105">
  <div class="title">计划生育“少生快富”工程申请审核表</div>
  <asp:Literal ID="LiteralAreaName" runat="server" EnableViewState="false" />
  <div class="print_table">
	<asp:Literal ID="LiteralBizInfo" runat="server" EnableViewState="false" />
  </div>
  <p><img src="/BizInfo/images/info/0105.gif"></p>
  <!--<div class="bottom"><%=m_SiteName %></div>-->
  <asp:Literal ID="LiteralDocs" runat="server" EnableViewState="false" />
</div>

</form>
</body>
</html>
<link rel="stylesheet" href="/scripts/lightbox/lightbox.css" type="text/css" media="screen" />
<script src="/scripts/lightbox/jquery-1.8.3.min.js" type="text/javascript"></script>
<script src="/scripts/lightbox/lightbox.js" type="text/javascript"></script>