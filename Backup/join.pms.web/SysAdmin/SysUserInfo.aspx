<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysUserInfo.aspx.cs" Inherits="join.pms.web.SysAdmin.SysUserInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>�ޱ���ҳ</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/index.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ
<div class="tsxx">��ʾ��Ϣ������</div>
--><!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" height="500" valign="top">
<!-- �������� Start-->
<asp:Literal ID="LiteralData" runat="server" EnableViewState="false"></asp:Literal>

<!-- ������ť -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0" align="center" class="zhengwen">
<tr><td align="right" style="width: 132px; height: 50px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:history.go(-1);" class="submit6" />
</td></tr></table>

<!--End-->
</td></tr></table>
</td></tr></table>
</div>
<br/><br/><br/><br/>
</form>
</body>
</html>
