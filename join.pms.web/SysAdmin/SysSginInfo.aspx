<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysSginInfo.aspx.cs" Inherits="join.pms.web.SysAdmin.SysSginInfo" %>
<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>�ޱ���ҳ</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/right.css" type="text/css" rel="stylesheet">
</head>
<body>
<style type="text/css">
#UcAreaSel1_txtSelectArea { display:none}
</style>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ
<div class="tsxx">��ʾ��Ϣ������</div>
--><!--Start-->
<div id="ec_bodyZone">
    <table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td><table border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td>ѡ��������</td><td><uc1:ucAreaSel ID="UcAreaSel1" runat="server" /></td><td><asp:Button ID="btnAdd" runat="server" Text="�� ��ѯ ��" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button></td></tr></table></td></tr></table>
    <div style="padding:0 0 20px;"><asp:Literal ID="LiteralData" runat="server" EnableViewState="false"></asp:Literal></div>
    <div style="text-align:center; padding:0 0 20px;"><input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:history.go(-1);" class="submit6" /></div>
</div>

</form>
</body>
</html>
