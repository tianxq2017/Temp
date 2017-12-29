<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysUserDepment.aspx.cs" Inherits="join.pms.web.SysAdmin.SysUserDepment" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>xx</title>
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
.butStyle {line-height: 20px; color: #000000; text-decoration: none; font-size: 12px;}
-->
</style>
</head>
<body>
<form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：<asp:Literal ID="LiteralInfo" runat="server"></asp:Literal></div>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" height="500" valign="top">

<!-- 编辑区 -->
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" class="zhengwen">
<tr>
  <td height="25" align="right" class="zhengwenjiacu" style="width: 130px">机构编码：</td>
  <td width="*" align="left"><input id="txtDeptCode" runat="server" class="yslInput" style="width: 239px" /></td>
</tr>
<tr>
  <td height="25" align="right" class="zhengwenjiacu" style="width: 130px">机构名称：</td>
  <td width="*" align="left"><input id="txtDeptName" runat="server" class="yslInput" style="width: 239px" /></td>
</tr>
<tr>
  <td height="25" align="right" class="zhengwenjiacu" style="width: 130px">所在地址：</td>
  <td width="*" align="left"><input id="txtDeptAddress" runat="server" class="yslInput" style="width: 239px" /></td>
</tr>
<tr>
  <td height="25" align="right" class="zhengwenjiacu" style="width: 130px">单位电话：</td>
  <td width="*" align="left"><input id="txtDeptTel" runat="server" class="yslInput" style="width: 239px" /></td>
</tr>
</table>
<!-- 操作按钮 -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0" align="center" class="zhengwen">
<tr><td align="right" style="width: 132px; height: 50px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>

<!--End-->
</td></tr></table>
</td></tr></table>
<br/><br/><br/><br/>
</form>
</body>
</html>