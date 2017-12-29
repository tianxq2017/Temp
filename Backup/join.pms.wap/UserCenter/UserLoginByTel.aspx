<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLoginByTel.aspx.cs" Inherits="join.pms.wap.UserCenter.UserLoginByTel" %>
<%@ Register Src="/userctrl/Uc_PageTop.ascx" TagName="Uc_PageTop" TagPrefix="uc1" %>
<%@ Register Src="/userctrl/Uc_Footer.ascx" TagName="Uc_Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta name="apple-mobile-web-app-capable" content="yes" />
<meta name="apple-mobile-web-app-status-bar-style" content="black" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0, maximum-scale=1.0,user-scalable=yes" />
<!--<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0 , maximum-scale=1.0, user-scalable=0">-->
<meta name="generator" content="bd-mobcard" />
<meta name="format-detection" content="telephone=yes" />
<meta http-equiv="content-type" content="application/vnd.wap.xhtml+xml;charset=UTF-8"/>
<title>群众登录</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery-1.11.3.min.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">     <uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>
    <div class="block">
  <!--登录方式 -->
  <div class="login_tab clearfix">
    <ul>
	  <li><p><a href="/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh.shtml">用户名登录</a></p></li>
	  <li class="on"><p><a href="/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjhbytel.shtml">手机号登录</a></p></li>
	</ul>
  </div>
  
  <!--账号登录 -->
  <div class="part_05">
	<div class="part_form">
	  <ul>
		<li>
		  <p class="title">手机号码</p>
		  <p class="text"><asp:TextBox ID="txtUserMobile" runat="server" EnableViewState="False" MaxLength="11" Text="" autocomplete="off"/></p>
		  <p class="ps"><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtUserMobile" Display="Dynamic" ErrorMessage="手机不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtUserMobile"  Display="Dynamic" ErrorMessage="手机填写不正确" ValidationExpression="(^(13[0-9]|14[0-9]|15[0-9]|18[0-9])\d{8}$)" SetFocusOnError="True"></asp:RegularExpressionValidator></p>
		</li>
		<li>
		  <p class="title">验证码</p>
		  <p class="text"><input type="button" value="获取验证码" id="ButSend" onclick="SendCheckCodeD(document.getElementById('txtUserMobile').value);"  class="sms"><input id="txtUserSmsCode" name="txtUserSmsCode" type="text" size="6" maxlength="12" style="width:60%;" runat="server" autocomplete="off" enableviewstate="false" value="" /></p>
		</li>
	  </ul>
	</div>
	<div class="part_button"><asp:Button ID="btnLogin" runat="server" Text="确认登录" OnClick="btnLogin_Click" EnableViewState="false"  /></div>
  </div> 
</div>
<uc2:Uc_Footer id="Uc_Footer1" runat="server"/>
    </form>
</body>
</html>
<!--容错 -->
<script language="JavaScript">
    <!-- Hide
    function killErrors() {
    return true;
    }
    window.onerror = killErrors;
    // -->
</script>