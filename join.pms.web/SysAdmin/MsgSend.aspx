<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgSend.aspx.cs" Inherits="join.pms.web.SysAdmin.MsgSend" %>

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
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ��������Ҫ���͵���Ա�������ֻ�(С��ͨ)�����,��������͡���ť���з��͡�Ⱥ�����֧��20�����룻Ⱥ��ʱ�������������Ϣ���������á����ݶ��Žӿ��������������������Ҫһ��ʱ�䣬�������ĵȴ�����</div>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" height="500" valign="top">
<!-- �������� Start-->
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" class="zhengwen">
<tr>
  <td height="25" align="right" class="zhengwenjiacu">�ֻ����룺</td>
  <td width="*" align="left"><asp:TextBox ID="txtMsgMobile" runat="server" EnableViewState="False" MaxLength="250" Width="411px" Height="50px" Rows="3" TextMode="MultiLine"/><br/>��֧��С��ͨ������ֻ���������Ӣ�ġ�,���ָ���</td>
</tr>
<tr>
  <td height="25" align="right" class="zhengwenjiacu">��Ϣ���ݣ�</td>
  <td width="*" align="left"><asp:TextBox ID="txtMsgBody" runat="server" EnableViewState="False" MaxLength="100" Width="411px" Height="50px" Rows="3" TextMode="MultiLine"></asp:TextBox></td>
</tr>
<tr>
    <td align="right" height="50" class="zhengwenjiacu">���ͽ����</td>
    <td align="left" width="*" class="small"><asp:Literal ID="LiteralResults" runat="server" ></asp:Literal>&nbsp;
    </td>
</tr>
</table>
<!-- ������ť -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0" align="center" class="zhengwen">
<tr><td align="right" style="width: 132px; height: 50px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="�� ���� ��" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>
<input name="btnCancel" type="reset" id="btnCancel" value="�� ���� ��" class="submit6"/>
<!--<input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:history.go(-1);" class="crmbutton small save" />-->
</td></tr></table>

<!--End-->
</td></tr></table>
</td></tr></table>
<br/><br/><br/><br/>
</form>
</body>
</html>
