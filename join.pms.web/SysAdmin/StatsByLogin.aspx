<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatsByLogin.aspx.cs" Inherits="join.pms.web.SysAdmin.StatsByLogin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>统计</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/index.css" type="text/css" rel="stylesheet">
<style type="text/css">
<!--
body {
	margin-top: 10px;
	background-color: #FFFFFF;
	margin-left: 10px;
	margin-right: 10px;
	margin-bottom: 0px;
	background-image: url(/images/mainyouxiabg.jpg);
	background-position:right bottom;
	background-repeat:no-repeat;
}
-->
</style>
</head>
<body>
<form id="form1" runat="server">
<!-- 页面参数 -->
<input type="hidden" name="txtUrlParams" id="txtUrlParams" value="" runat="server" style="display:none;"/>
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--正文信息-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" height="540" valign="top" class="zhengwen">
请单击<asp:Button ID="butExport" runat="server" Text="· 导出 ·" class="cusersubmit" OnClick="butExport_Click" ></asp:Button>按钮导出统计结果……<br/>
    
<table width="95%" border="0" cellspacing="0" cellpadding="0" class="zhengwen"><tr>
<td ><asp:Literal ID="LiteralResults" runat="server" EnableViewState="false"></asp:Literal></td>
</tr></table>
    
</td></tr></table>
</td></tr></table>
<br/><br/>
</form>
</body>
</html>