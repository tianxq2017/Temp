<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysMsgSet.aspx.cs" Inherits="join.pms.web.SysAdmin.SysMsgSet" %>

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
<div class="tsxx">��ʾ��Ϣ�������˵��������������ύ����ť������ɲ�����</div>
<!--Start-->
<div id="ec_bodyZone" class="part_bg2">
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td>
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="500" valign="top">

<!-- �༭�� -->
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="zhengwen">
<tr>
  <td width="160" height="25" align="right" class="zhengwenjiacu">�������ϵͳ��ʾ��</td>
  <td width="*" align="left"><input name="cbxClearMsg" type="checkbox" id="cbxClearMsg" value="1" class="cuserinput" runat="server"/>ѡ��֮���ύ���ý��������е�ϵͳ��ʾ��Ϣ</td>
</tr>
<tr>
  <td height="25" align="right" class="zhengwenjiacu">�ر�����ϵͳ��ʾ��</td>
  <td width="*" align="left"><input name="cbxCloseMsg" type="checkbox" id="cbxCloseMsg" value="1" class="cuserinput" runat="server"/>ѡ�������ϵͳ��ʾ��������ʾ</td>
</tr>
<tr>
    <td align="right" height="25" ></td>
    <td align="left" width="*">&nbsp;</td></tr>
</table>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<!-- ������ť -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0" align="center" class="zhengwen">
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
