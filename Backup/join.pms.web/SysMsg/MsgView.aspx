<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgView.aspx.cs" Inherits="join.pms.web.SysMsg.MsgView" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>消息查看</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/index.css" type="text/css" rel="stylesheet">
</head>
<body>
<form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server" EnableViewState="false"></asp:Literal></div>
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" height="530" valign="top">
<!-- 表单信息 Start -->
<asp:Literal ID="LiteralMsgView" runat="server" ></asp:Literal><br/>
</td></tr></table>
</td></tr></table>
<br/><br/>
</form>
</form>
</body>
</html>