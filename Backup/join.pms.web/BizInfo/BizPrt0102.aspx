﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizPrt0102.aspx.cs" Inherits="join.pms.web.BizInfo.BizPrt0102" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>二孩生育登记表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link href="/BizInfo/css/printAppTable.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">

<div class="print_0102">
  <div class="title">二孩生育登记表</div>
  <asp:Literal ID="LiteralBizInfo" runat="server" EnableViewState="false" />
  <p><img src="/BizInfo/images/info/0102.gif" /></p>
  <!--<div class="bottom"><%=m_SiteName %></div>-->
  <asp:Literal ID="LiteralDocs" runat="server" EnableViewState="false" />
</div>

</form>
</body>
</html>
<link rel="stylesheet" href="/scripts/lightbox/lightbox.css" type="text/css" media="screen" />
<script src="/scripts/lightbox/jquery-1.8.3.min.js" type="text/javascript"></script>
<script src="/scripts/lightbox/lightbox.js" type="text/javascript"></script>