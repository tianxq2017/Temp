<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgSendView.aspx.cs" Inherits="join.pms.web.SysAdmin.MsgSendView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>�ޱ���ҳ</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/right.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ
<div class="tsxx">��ʾ��Ϣ��������Ҫ���͵���Ա�������ֻ�(С��ͨ)�����,��������͡���ť���з��͡�Ⱥ�����֧��20�����룻Ⱥ��ʱ�������������Ϣ���������á����ݶ��Žӿ��������������������Ҫһ��ʱ�䣬�������ĵȴ�����</div>
-->
<div id="ec_bodyZone" style="padding-top:15px;">
<asp:Literal ID="LiteralData" runat="server" EnableViewState="false"></asp:Literal>
<div style="padding-top:15px;"><input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:history.go(-1);" class="submit6" /></div>
</div>
</form>
</body>
</html>
