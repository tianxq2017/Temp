<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyPwd.aspx.cs" Inherits="join.pms.wap.UserCenter.ModifyPwd" %>
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
<title>修改密码</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>
        <div class="block"> 
  
  <!--账号登录 -->
  <div class="part_05">
	<div class="part_form">
	  <ul>
		<li>
		  <p class="title">旧密码</p>
		  <p class="text"><asp:TextBox ID="txtUserPasswordOld" runat="server" EnableViewState="False" MaxLength="20"  TextMode="Password"/></p>
		</li>
		<li>
		  <p class="title"></p>
		  <p class="text"><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请正确填写密码"  ControlToValidate="txtUserPasswordOld" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtUserPasswordOld" ErrorMessage="请正确填写密码" ValidationExpression="^.{6,20}$" SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator></p>
		</li>
		<li>
		  <p class="title">新密码</p>
		  <p class="text"><asp:TextBox ID="txtUserPassword" runat="server" EnableViewState="False" MaxLength="20" TextMode="Password"/></p>
		</li>
		<li>
		  <p class="title"></p>
		  <p class="text"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请正确填写密码"  ControlToValidate="txtUserPassword" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtUserPassword" ErrorMessage="请正确填写密码" ValidationExpression="^.{6,20}$" SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator></p>
		</li>
		<li>
		  <p class="title">新密码确认</p>
		  <p class="text"><asp:TextBox ID="txtUserPasswordRe" runat="server" EnableViewState="False" MaxLength="20"  TextMode="Password" /></p>
		</li>
		<li>
		  <p class="title"></p>
		  <p class="text"><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="两次输入密码不一致" ControlToCompare="txtUserPassword" ControlToValidate="txtUserPasswordRe" SetFocusOnError="True" Display="Dynamic"></asp:CompareValidator></p>
		</li>
	  </ul>
	</div>
	<div class="part_button"><asp:Button ID="btnAdd" runat="server" Text="确认登录" OnClick="btnAdd_Click" EnableViewState="false"  /></div>
  </div>
  
</div>
       <uc2:Uc_Footer id="Uc_Footer1" runat="server"/>
    </form>
</body>
</html>
