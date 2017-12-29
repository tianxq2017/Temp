<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgSend.aspx.cs" Inherits="join.pms.web.SysAdmin.MsgSend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>无标题页</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/index.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请输入要发送的人员姓名和手机(小灵通)号码后,点击“发送”按钮进行发送。群发最多支持20个号码；群发时“姓名”项的信息将不起作用。根据短信接口连接情况，操作可能需要一段时间，请您耐心等待……</div>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" height="500" valign="top">
<!-- 主操作区 Start-->
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" class="zhengwen">
<tr>
  <td height="25" align="right" class="zhengwenjiacu">手机号码：</td>
  <td width="*" align="left"><asp:TextBox ID="txtMsgMobile" runat="server" EnableViewState="False" MaxLength="250" Width="411px" Height="50px" Rows="3" TextMode="MultiLine"/><br/>（支持小灵通，多个手机号码请用英文“,”分隔）</td>
</tr>
<tr>
  <td height="25" align="right" class="zhengwenjiacu">消息内容：</td>
  <td width="*" align="left"><asp:TextBox ID="txtMsgBody" runat="server" EnableViewState="False" MaxLength="100" Width="411px" Height="50px" Rows="3" TextMode="MultiLine"></asp:TextBox></td>
</tr>
<tr>
    <td align="right" height="50" class="zhengwenjiacu">发送结果：</td>
    <td align="left" width="*" class="small"><asp:Literal ID="LiteralResults" runat="server" ></asp:Literal>&nbsp;
    </td>
</tr>
</table>
<!-- 操作按钮 -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0" align="center" class="zhengwen">
<tr><td align="right" style="width: 132px; height: 50px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 发送 ·" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>
<input name="btnCancel" type="reset" id="btnCancel" value="· 重置 ·" class="submit6"/>
<!--<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:history.go(-1);" class="crmbutton small save" />-->
</td></tr></table>

<!--End-->
</td></tr></table>
</td></tr></table>
<br/><br/><br/><br/>
</form>
</body>
</html>
