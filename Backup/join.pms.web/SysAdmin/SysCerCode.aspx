<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysCerCode.aspx.cs" Inherits="join.pms.web.SysAdmin.SysCerCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>�޸�����</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/right.css" type="text/css" rel="stylesheet">
</head>
<body>
<form id="form1" runat="server">
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ������������֤�롢����֤����ظ���֤��󣬵�����ύ����ť������ɲ�����</div>
<!--Start-->
<div id="ec_bodyZone" class="part_bg2">
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td>
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="500" valign="top">

<!-- �༭�� -->
<table width="600" border="0" cellpadding="0" cellspacing="0" class="zhengwen">
<tr>
  <td width="120" height="30" align="right" class="zhengwenjiacu">�������֤�룺</td>
  <td width="*" align="left" class="txt1"><asp:TextBox ID="txOldCode" runat="server" EnableViewState="False" MaxLength="16" Width="200px"/><span style="color: #ff1400">*</span>(��ԭ������֤��)</td>
</tr>
<tr>
  <td width="120" height="30" align="right" class="zhengwenjiacu">��������֤�룺</td>
  <td width="*" align="left" class="txt1"><asp:TextBox ID="txtUserCode" runat="server" EnableViewState="False" MaxLength="16" Width="200px"/><span style="color: #ff1400">*</span>(6-16���ַ�)</td>
</tr>
<tr>
    <td width="120" align="right" height="30" class="zhengwenjiacu">����֤��ȷ�ϣ�</td>
    <td align="left" width="*" class="txt1"><asp:TextBox ID="txtUserCodeRe" runat="server" EnableViewState="False" MaxLength="16" Width="200px"/><span style="color: #ff1400">*</span>����֤��</td></tr>
</table><br>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<!-- ������ť -->
<table cellSpacing="0" cellPadding="0" width="600" border="0" class="zhengwen">
<tr><td align="right" style="width: 132px; height: 50px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="�ύ" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>
<input name="btnCancel" type="reset" id="btnCancel" value="����" class="submit6"/>&nbsp;
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
</form>
</body>
</html>