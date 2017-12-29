<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysUserPwd.aspx.cs" Inherits="join.pms.web.SysAdmin.SysUserPwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>修改密码</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/right.css" type="text/css" rel="stylesheet"/>
</head>
<body>
<form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请操输入旧密码、新密码和确认密码后，点击“提交”按钮即可完成操作。</div>
<!--Start-->
<div id="ec_bodyZone" class="part_bg2">
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td>
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="500" valign="top">

<!-- 编辑区 -->
<table width="600" border="0" cellpadding="0" cellspacing="0" class="zhengwen">
<tr>
  <td width="120" height="30" align="right" class="zhengwenjiacu">输入旧密码：</td>
  <td width="*" align="left" class="txt1"><asp:TextBox ID="txOldPwd" runat="server" EnableViewState="False" MaxLength="16" Width="200px" TextMode="Password"/><span style="color: #ff1400">*</span>(您原来的密码)</td>
</tr>
<tr>
    <td width="120" height="30" align="right" class="zhengwenjiacu">您的新密码：</td>
    <td width="*" align="left" class="txt1"><asp:TextBox ID="txtUserPwd" runat="server" EnableViewState="False" MaxLength="16" Width="200px"  TextMode="Password" autocomplete="off" />
        <span class="ps">6-16位字母数字组合
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请正确填写密码"  ControlToValidate="txtUserPwd" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtUserPwd"  Display="Dynamic" ErrorMessage="请正确填写密码" ValidationExpression="^(?=[a-zA-Z0-9]*(?:[a-zA-Z][0-9]|[0-9][a-zA-Z]))[a-zA-Z0-9]{6,16}$" SetFocusOnError="True"></asp:RegularExpressionValidator>
        </span>
    </td>
</tr>
<tr>
    <td width="120" align="right" height="30" class="zhengwenjiacu">新密码确认：</td>
    <td align="left" width="*" class="txt1"><asp:TextBox ID="txtUserPwdRe" runat="server" EnableViewState="False" MaxLength="16" Width="200px" TextMode="Password"/><span style="color: #ff1400">*</span></td></tr>
</table><br>
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
