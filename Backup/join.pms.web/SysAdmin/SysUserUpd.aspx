<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysUserUpd.aspx.cs" Inherits="join.pms.web.SysAdmin.SysUserUpd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>修改用户信息</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/right.css" type="text/css" rel="stylesheet"/>
</head>
<body>
<form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请输入修改信息，点击“提交”按钮即可完成操作。</div>
<!--Start-->
<div id="ec_bodyZone" class="part_bg2">
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td>
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="500" valign="top">

<!-- 编辑区 -->
<table width="600" border="0" cellpadding="0" cellspacing="0" class="zhengwen">
<tr>
  <td width="120" height="30" align="right" class="zhengwenjiacu">输入信息：</td>
  <td width="*" align="left" class="txt1"><asp:TextBox ID="txtInfo" runat="server" EnableViewState="False" MaxLength="16" Width="200px"/><span style="color: #ff1400">*</span></td>
</tr>
<br>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<!-- 操作按钮 -->
<table cellSpacing="0" cellPadding="0" width="600" border="0" class="zhengwen">
<tr><td align="right" style="width: 132px; height: 50px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="提交" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>
<input name="btnCancel" type="reset" id="btnCancel" value="重填" class="submit6"/>&nbsp;
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
</form>
</body>
</html>
