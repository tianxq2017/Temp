<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgSendView.aspx.cs" Inherits="join.pms.web.SysAdmin.MsgSendView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>无标题页</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/right.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息
<div class="tsxx">提示信息：请输入要发送的人员姓名和手机(小灵通)号码后,点击“发送”按钮进行发送。群发最多支持20个号码；群发时“姓名”项的信息将不起作用。根据短信接口连接情况，操作可能需要一段时间，请您耐心等待……</div>
-->
<div id="ec_bodyZone" style="padding-top:15px;">
<asp:Literal ID="LiteralData" runat="server" EnableViewState="false"></asp:Literal>
<div style="padding-top:15px;"><input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:history.go(-1);" class="submit6" /></div>
</div>
</form>
</body>
</html>
