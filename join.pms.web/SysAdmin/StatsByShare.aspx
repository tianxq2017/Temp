<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatsByShare.aspx.cs" Inherits="join.pms.web.SysAdmin.StatsByShare" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>ͳ��</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/index.css" type="text/css" rel="stylesheet">
<script src="/Scripts/jsdate.js" type="text/javascript"></script>
</head>
<body>
<form id="form1" runat="server">
<!-- ҳ����� -->
<input type="hidden" name="txtUrlParams" id="txtUrlParams" value="" runat="server" style="display:none;"/>
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--������Ϣ-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" height="540" valign="top" class="zhengwen">
<!--start-->
    <table width="95%" border="0" cellspacing="0" cellpadding="0" class="small">
    <tr>
    <td width="70" class="fb01" bgcolor="#F4FAFA">&nbsp;��ʼ���ڣ�</td>
    <td width="145" align="left" bgcolor="#F4FAFA">
    <input id="txtStartTime" readonly="readonly" size="12"  onclick="SelectDate(document.getElementById('txtStartTime'),'yyyy-MM-dd')"   runat="server" />
    <input id="CalendarStart"  onclick="SelectDate(document.getElementById('txtStartTime'),'yyyy-MM-dd')"  tabIndex="2" type="image" src="/images/calendar.gif" align="absMiddle">
    </td>
    <td width="20" align="left" bgcolor="#F4FAFA">&nbsp;</td>
    <td width="70" class="fb01" bgcolor="#F4FAFA">&nbsp;��ֹ���ڣ�</td>
    <td width="150" align="left" bgcolor="#F4FAFA">
    <input id="txtEndTime" readonly="readonly" size="12"  onclick="SelectDate(document.getElementById('txtEndTime'),'yyyy-MM-dd')"   runat="server" />
    <input id="CalendarEnd"  onclick="SelectDate(document.getElementById('txtEndTime'),'yyyy-MM-dd')"  tabIndex="4" type="image"    src="/images/calendar.gif" align="absMiddle">
    </td>
    <td width="20" bgcolor="#F4FAFA">&nbsp;</td>
    <td align="left" bgcolor="#F4FAFA"><asp:Button ID="btnAdd" runat="server" Text="�� ��ѯ ��" class="cusersubmit" OnClick="btnAdd_Click"></asp:Button>
    <!--<asp:Button ID="butExport" runat="server" Text="�� ���� ��" class="cusersubmit" OnClick="butExport_Click" ></asp:Button>-->
    </td>
    </tr>
    </table><br/>
    
    <table width="95%" border="0" cellspacing="0" cellpadding="0" class="zhengwen"><tr>
    <td ><asp:Literal ID="LiteralResults" runat="server" EnableViewState="false"></asp:Literal></td>
    </tr></table>
<!--end-->
</td></tr></table>
</td></tr></table>
<br/><br/>
</form>
</body>
</html>