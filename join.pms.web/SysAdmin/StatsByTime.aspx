<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatsByTime.aspx.cs" Inherits="join.pms.web.SysAdmin.StatsByTime" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>ͳ��</title>
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
<script language="javascript" type="text/javascript">
function PopCalendar(obj){
    showx = event.screenX - event.offsetX - 4 - 10 ; // + deltaX;
    showy = event.screenY - event.offsetY -168; // + deltaY;
    newWINwidth = 210 + 4 + 18;
    window.showModalDialog("/includes/Calendar.htm",obj,"dialogWidth:200px; dialogHeight:210px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; ");    
}
</script>
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

    <!-- ������Ϣ Start -->
    <table width="95%" border="0" cellspacing="0" cellpadding="0" class="small">
    <tr><td width="20" height="30" align="left">&nbsp;</td><td width="70" class="fb01" bgcolor="#F4FAFA">&nbsp;��ʼ���ڣ�</td>
    <td width="145" align="left" bgcolor="#F4FAFA">
    <input id="txtStartTime" readonly="readonly" size="12" onclick="PopCalendar(txtStartTime);return false;"  runat="server" />
    <input id="CalendarStart" onclick="PopCalendar(txtStartTime);return false;" tabIndex="2" type="image" src="/images/calendar.gif" align="absMiddle">
    </td>
    <td width="20" align="left" bgcolor="#F4FAFA">&nbsp;</td>
    <td width="70" class="fb01" bgcolor="#F4FAFA">&nbsp;��ֹ���ڣ�</td>
    <td width="150" align="left" bgcolor="#F4FAFA">
    <input id="txtEndTime" readonly="readonly" size="12" onclick="PopCalendar(txtEndTime);return false;"  runat="server" />
    <input id="CalendarEnd" onclick="PopCalendar(txtEndTime);return false;" tabIndex="4" type="image"    src="/images/calendar.gif" align="absMiddle">
    </td>
    <td width="20" bgcolor="#F4FAFA">&nbsp;</td>
    <td align="left" bgcolor="#F4FAFA"><asp:Button ID="btnAdd" runat="server" Text="�� ��ѯ ��" class="cusersubmit" OnClick="btnAdd_Click"></asp:Button>
    <asp:Button ID="butExport" runat="server" Text="�� ���� ��" class="cusersubmit" OnClick="butExport_Click" ></asp:Button>
    </td>
    </tr>
    </table><br/>
    <table width="95%" border="0" cellspacing="0" cellpadding="0" class="small"><tr>
    <td width="20" align="left">&nbsp;</td><td width="*" class="fb01"><asp:Literal ID="LiteralResults" runat="server" EnableViewState="false"></asp:Literal></td>
    </tr></table>
    <!-- ������Ϣ End -->
    
</td></tr></table>
</td></tr></table>
<!-- ҳ����Ϣ -->
<br/><br/>
</form>
</body>
</html>