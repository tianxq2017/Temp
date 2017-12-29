<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="join.pms.wap.UserCenter.UserLogin" %>
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
    <form id="form1" runat="server">
     <uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>
     <div class="block">
  <!--登录方式 -->
  <div class="login_tab clearfix">
    <ul>
	 <li class="on"><p><a href="/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh.shtml">用户名登录</a></p></li>
	  <li><p><a href="/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjhbytel.shtml">手机号登录</a></p></li>	
	</ul>
  </div>
  
  <!--账号登录 -->
  <div class="part_05">
	<div class="part_form">
	  <ul>
		<li>
		  <p class="title">用户名</p>
		  <p class="text"><asp:TextBox ID="txtPersonAcc"  runat="server" MaxLength="20" EnableViewState="false" autocomplete="off"></asp:TextBox></p>
		</li>
		<li>
		  <p class="title">密码</p>
		  <p class="text"><asp:TextBox ID="txtPersonPwd" runat="server" MaxLength="20" TextMode="Password" EnableViewState="false"  onblur='if (value ==""){value="",this.className=""}' autocomplete="off"></asp:TextBox></p>
		</li>
		<li>
		  <p class="title">验证码</p>
		  <p class="text"><img id="CodeImg" onclick="document.getElementById('CodeImg').src='/userctrl/GetCheckCode.aspx?k='+Math.random()" src="/userctrl/GetCheckCode.aspx" class="fr" alt="点击图片更换验证码" title="点击图片更换验证码" height="31"/><input name="txtCheckCode" type="text" id="txtCheckCode" size="5" style="width: 70%;" autocomplete="off"/></p>
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