<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysRoles.aspx.cs" Inherits="join.pms.web.SysAdmin.SysRoles" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>ϵͳ��ɫ����</title>
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
-->
</style>
</head>
<body>
<form id="form1" runat="server">
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ���������ɫ���ƺ�������Ϣ�󣬵�����ύ����ť������ɲ�����</div>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" height="500" valign="top">

<!-- �༭�� -->
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" class="zhengwen">
<tr>
  <td width="120" height="25" align="right" class="zhengwenjiacu">��ɫ���ƣ�</td>
  <td width="*" align="left"><asp:TextBox ID="txtRoleName" runat="server" EnableViewState="False" MaxLength="10" Width="411px"/></td>
</tr>
<tr>
  <td height="25" align="right" class="zhengwenjiacu">��ɫ������</td>
  <td width="*" align="left"><asp:TextBox ID="txtRoleNotes" runat="server" EnableViewState="False" MaxLength="25" Width="411px"/></td>
</tr>
<tr>
    <td align="right" height="25" class="zhengwenjiacu"></td>
    <td align="left" width="*"></td>
</tr>
</table>
<!-- ������ť -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0" align="center" class="zhengwen">
<tr><td align="right" style="width: 132px; height: 50px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="�� �ύ ��" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!-- �������� End-->
<!--End-->
</td></tr></table>
</td></tr></table>
<br/><br/><br/><br/>
</form>
</body>
</html>