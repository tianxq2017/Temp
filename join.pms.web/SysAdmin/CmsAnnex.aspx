<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CmsAnnex.aspx.cs" Inherits="join.pms.web.SysAdmin.CmsAnnex" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�ޱ���ҳ</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="../includes/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<!-- ҳ�浼����ʼ --> 
<table width="100%" border="0" cellpadding="0" cellspacing="0" align="center" class="level2Bg">
<tr><td width="3" style="height: 30px"></td><td align="left"  style="padding-left: 10px; padding-right: 50px;" class="moduleName" nowrap="nowrap"> 
����λ��:<asp:Literal ID="LiteralNav" runat="server" EnableViewState="false"></asp:Literal></td><td width="3">&nbsp;</td></tr> </table>
<!-- ҳ����� -->
<!-- �������� Start-->
<table width="98%" align="center" border="0" cellpadding="0" cellspacing="0"><tbody>
<tr><td class="small" colspan="3" height="30">��ʾ����ѡ����Ҫɾ���ĸ�����Ȼ������ɾ�������Ӽ�����ɲ�����</td></tr>
<tr><td colspan="3"></td></tr>
<tr><td valign="top"><img src="/images/showPanelTopLeft.gif"></td><td width="100%" valign="top" class="showPanelBg" style="padding: 10px;">
<div id="Div1" class="small" style="width: 100%; position: relative;">
<table class="lvtBg" width="100%" border="0" cellpadding="0" cellspacing="1"><tbody><tr><td align="left"><br/>
<!-- �༭�� -->
<asp:Literal ID="LiteralData" runat="server" EnableViewState="false"></asp:Literal>
<!-- ������ť -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="lvtCol">
<tr><td align="right" style="width: 132px; height: 50px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:history.go(-1);" class="crmbutton small save" />
</td></tr></table>
<!-- �������� End-->
</td></tr></tbody></table></div></td><td valign="top"><img src="/images/showPanelTopRight.gif"></td></tr></tbody></table>
</form>
<!-- ҳ����Ϣ -->
<!--#include virtual='/includes/copyright.inc' -->
</body>
</html>