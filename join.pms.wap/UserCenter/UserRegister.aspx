<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegister.aspx.cs" Inherits="join.pms.wap.UserCenter.UserRegister" %>
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
<title>群众注册</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>
<div class="block">
  <!--注册提示 -->
  <!--<div class="part_06">
	<p class="title">注册提示</p>
    <p class="sum">对不起，手机版不能注册，请使用电脑通过网页版进行注册。给您带来的不便，我们深表歉意。</p>
    <p class="button"><a href="/">返回首页</a></p>
  </div>
  <div class="clr10"></div> -->
  
  <!--相关内容 -->
  <div class="part_05">
	<div class="part_form">
	  <ul>
		<li>
		  <p class="title">登录名</p>
		  <p class="text"><asp:TextBox ID="txtUserAccount" runat="server" EnableViewState="False" MaxLength="20" onchange="CheckUserRegName(document.getElementById('txtUserAccount').value);"/><span class="ps">数字加字母组合<span id="divMsg" class="hongse"></span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="用户名不能为空" ControlToValidate="txtUserAccount" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtUserAccount" Display="Dynamic" ErrorMessage="只能输入2-25个字符" ValidationExpression="^[\u4E00-\u9FA5-A-Za-z0-9]{2,20}$" ></asp:RegularExpressionValidator></span></p>
		</li>
		<li>
		  <p class="title">姓名</p>
		  <p class="text"><asp:TextBox ID="txtUserName" runat="server" EnableViewState="False" MaxLength="25"  autocomplete="off"/><span class="ps"><!--请填写真实姓名 --> <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="真实姓名不能为空" ControlToValidate="txtUserName" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span></p>
		</li>
		<li>
		  <p class="title">身份证号</p>
		  <p class="text"><asp:TextBox ID="txtUserCardID" runat="server" EnableViewState="False" MaxLength="18" autocomplete="off"/><span class="ps"><!--请输入真实准确的18位身份证号 --> <span id="divMsg1" class="hongse"></span><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="身份证号不能为空" ControlToValidate="txtUserCardID" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator4"
            runat="server" ErrorMessage="请输入正确的身份证号" ValidationExpression="\d{17}[\d|X]|\d{15}" ControlToValidate="txtUserCardID" Display="Dynamic"></asp:RegularExpressionValidator></span></p>
		</li>
		<li>
		  <p class="title">密码</p>
		  <p class="text"><asp:TextBox ID="txtUserPwd" runat="server" EnableViewState="False" MaxLength="20" TextMode="Password" autocomplete="off" /><span class="ps">6-20位字母数字组合<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请正确填写密码"  ControlToValidate="txtUserPwd" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtUserPwd"  Display="Dynamic" ErrorMessage="请正确填写密码" ValidationExpression="^(?=[a-zA-Z0-9]*(?:[a-zA-Z][0-9]|[0-9][a-zA-Z]))[a-zA-Z0-9]{6,20}$" SetFocusOnError="True"></asp:RegularExpressionValidator></span></p>
		</li>
		<li>
		  <p class="title">确认密码</p>
		  <p class="text"><asp:TextBox ID="txtUserPwdRe" runat="server" EnableViewState="False" MaxLength="20" TextMode="Password" autocomplete="off" /><span class="ps">6-20位字母数字组合<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="两次输入密码不一致" ControlToCompare="txtUserPwd" ControlToValidate="txtUserPwdRe" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator></span></p>
		</li>
		<li>
		  <p class="title">手机号</p>
		  <p class="text"><asp:TextBox ID="txtUserMobile" runat="server" EnableViewState="False" MaxLength="11" onchange="CheckUserTel(document.getElementById('txtUserMobile').value,'0');" autocomplete="off"/><span id="divMsg2" class="hongse"></span> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="手机号码不能为空" ControlToValidate="txtUserMobile" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtUserMobile" ErrorMessage="手机号码填写不正确" ValidationExpression="(^(13[0-9]|14[0-9]|15[0-9]|17[0-9]|18[0-9])\d{8}$)" Display="Dynamic"></asp:RegularExpressionValidator></p>
		</li>
		<li>
		  <p class="title">手机验证码</p>
		  <p class="text"><input type="button" value="获取验证码" id="ButSend" onclick="SendCheckCode(document.getElementById('txtUserMobile').value);"  class="sms"><input id="txtUserSmsCode" name="txtUserSmsCode" type="text" size="6" maxlength="12" style="width:60%;" runat="server" autocomplete="off" enableviewstate="false" value="" /></p>
		</li>
		<li>
		  <p class="title">注册协议</p>
		  <p class="text"><textarea style="width:100%; height:150px; line-height:24px; padding:5px;">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;如果您注册或以其它方式使用了本系统业务产品，则视为您已同意下列条款，并已和通辽市卫生和计划生育委员会（以下简称“通辽市卫计委”）签定了本《用户注册许可协议》（以下简称“协议”）。如您不同意本协议中的条款，请不要在本系统注册。
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;如果您通过合法注册并应用本系统，则您同意协议中的如下应用要求：
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1、本系统中填写的所有内容真实准确，如有虚假或隐瞒，愿意承担法律责任。
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2、遵守国家法律法规，遵守户籍地/居住地的相关计划生育规定要求。
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3、遵守系统应用的信息保密要求。
                </textarea></p>
		</li>
		<li>
		  <p class="title"></p>
		  <p><asp:CheckBox ID="cbOk" runat="server" /> 我已经阅读并同意《用户注册许可协议》</p>
		</li>
	  </ul>
	</div>
	<div class="part_button"><asp:Button ID="btnReg" runat="server" Text="注册" OnClick="btnReg_Click" EnableViewState="false" /></div>
  </div>
</div>
   <uc2:Uc_Footer id="Uc_Footer1" runat="server"/>
    </form>
</body>
</html>
