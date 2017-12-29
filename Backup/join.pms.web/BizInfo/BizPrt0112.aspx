<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizPrt0112.aspx.cs" Inherits="join.pms.web.BizInfo.BizPrt0112" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="/BizInfo/css/print.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">

<div class="print_0112">
  <div class="title">婚　育　情　况　证　明</div>
  <div class="title2">（人口计生行政审核专用）</div>
  <div class="print_0112_table">
   <asp:Literal ID="LiteralBizInfo" runat="server" />
	
  </div>
  <p><img src="/BizInfo/images/info/0112.gif"/></p>
  <div class="bottom"><%=m_SiteName %></div>
  <asp:Literal ID="LiteralDocs" runat="server" EnableViewState="false" />
</div>

</form>
</body>
</html>
<link rel="stylesheet" href="/scripts/lightbox/lightbox.css" type="text/css" media="screen" />
<script src="/scripts/lightbox/jquery-1.8.3.min.js" type="text/javascript"></script>
<script src="/scripts/lightbox/lightbox.js" type="text/javascript"></script>