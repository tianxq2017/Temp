<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysUsers.aspx.cs" Inherits="join.pms.web.SysAdmin.SysUsers" %>

<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>系统帐号管理</title>
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
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列信息后，点击“提交”按钮即可完成操作。</div>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" height="500" valign="top">

<!-- 编辑区 -->
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" class="zhengwen">
<tr>
  <td width="120" height="25" align="right" class="zhengwenjiacu">请输入登录帐号：</td>
  <td width="*" align="left"><asp:TextBox ID="txtUserAccount" runat="server" EnableViewState="False" MaxLength="16" Width="200px"/><span style="color: #ff1400">*</span>（6-16个字符，注册成功后不能修改）</td>
</tr>
<tr>
  <td height="25" align="right" class="zhengwenjiacu">登录密码：</td>
  <td width="*" align="left"><asp:TextBox ID="txtUserPwd" runat="server" EnableViewState="False" MaxLength="16" Width="200px"/><span style="color: #ff1400">*</span>(6-16个字符)</td>
</tr>
<tr>
  <td height="25" align="right" class="zhengwenjiacu">密码确认：</td>
  <td width="*" align="left"><asp:TextBox ID="txtUserPwdRe" runat="server" EnableViewState="False" MaxLength="16" Width="200px"/><span style="color: #ff1400">*</span>(6-16个字符)</td>
</tr>
<tr>
    <td align="right" height="25" class="zhengwenjiacu"></td>
    <td align="left" width="*"></td>
</tr>
<tr>
    <td align="right" height="25" class="zhengwenjiacu">操作员姓名：</td>
    <td align="left" width="*"><asp:TextBox ID="txtUserName" runat="server" EnableViewState="False" MaxLength="10" Width="200px"/><span style="color: #ff1400">*</span></td>
</tr>
<tr>
    <td align="right" height="25" class="zhengwenjiacu">
        归属区划：</td>
    <td align="left" width="*">
        <uc1:ucAreaSel ID="UcAreaSel1" runat="server" />
    </td>
</tr>
<tr>
    <td align="right" height="25" class="zhengwenjiacu">邮件地址：</td>
    <td align="left" width="*"><asp:TextBox ID="txtUserMail" runat="server" EnableViewState="False" MaxLength="50" Width="411px"/>（用于找回密码）</td>
</tr>
<tr>
    <td align="right" height="25" class="zhengwenjiacu">联系电话：</td>
    <td align="left" width="*"><asp:TextBox ID="txtUserTel" runat="server" EnableViewState="False" MaxLength="30" Width="200px"/><span style="color: #ff1400">*</span></td>
</tr>
<tr>
    <td align="right" height="25" class="zhengwenjiacu">联系地址：</td>
    <td align="left" width="*"><asp:TextBox ID="txtUserAddress" runat="server" EnableViewState="False" MaxLength="50" Width="411px"/></td>
</tr>
<tr>
    <td align="right" height="25" class="lvtCol"></td>
    <td align="left" width="*"></td>
</tr>
<tr>
    <td align="right" height="25" class="zhengwenjiacu">单位名称：</td>
    <td align="left" width="*"><asp:TextBox ID="txtUserUnitName" runat="server" EnableViewState="False" MaxLength="50" Width="411px"/></td>
</tr>
<tr>
    <td align="right" height="25" class="zhengwenjiacu">归属机构：</td>
    <td align="left" width="*"><asp:Literal ID="LiteralOrg" runat="server"></asp:Literal><span style="color: #ff1400">*</span></td>
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