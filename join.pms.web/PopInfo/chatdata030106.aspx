<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata030106.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.chatdata030106" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>���������߷������������</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/index.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
    <script language="javascript" src="/Scripts/datectrl.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ������һ��д������Ϣ�󣻵�����ύ����ť������5����ǰ����������ύ������</div>
<!-- ҳ����� -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">

<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- �༭��  -->
<br/>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr >
  <td align="right" width="80" height="25" class="zhengwenjiacu">������ڣ�</td>
  <td align="left" width="124"><input id="txtReportDate" readonly="readonly" size="7" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
    </td>
    <td align="right" width="80" height="25" class="zhengwenjiacu">�����λ��</td>
  <td align="left" width="124">
      <asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/>
  </td>
    <td align="right" width="80" height="25" class="zhengwenjiacu">��ȣ�</td>
  <td align="left" width="124">
      <asp:DropDownList ID="dd_Year" runat="server">
        <asp:ListItem>2015</asp:ListItem>
        <asp:ListItem>2016</asp:ListItem>
        <asp:ListItem>2017</asp:ListItem>
        <asp:ListItem>2018</asp:ListItem>
        <asp:ListItem>2019</asp:ListItem>
        <asp:ListItem>2020</asp:ListItem>
    </asp:DropDownList>
  </td>
    <td align="right" width="80" height="25" class="zhengwenjiacu">���ȣ�</td>
  <td align="left" width="124">
      <asp:DropDownList ID="dd_Month" runat="server">
        <asp:ListItem>һ����</asp:ListItem>
        <asp:ListItem>������</asp:ListItem>
        <asp:ListItem>������</asp:ListItem>
        <asp:ListItem>�ļ���</asp:ListItem>
    </asp:DropDownList>
  </td>
</tr>
<tr>
    <td align="right" width="80" height="25" class="zhengwenjiacu">��λ��</td>
    <td align="left" colspan="7">
        <uc1:ucDataAreaSel ID="UcDataAreaSel1" runat="server" />
    </td>
    
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td height="25" align="center" colspan="8" bgcolor="#FFFFCC" class="zhengwenjiacu">��ʡ����</td>
</tr>
<tr class="zhengwen">
    <td height="25" align="center" colspan="2" bgcolor="#FFFFCC" class="zhengwenjiacu">�ܳ�������</td>
    <td height="25" align="center" colspan="2" bgcolor="#FFFFCC" class="zhengwenjiacu">һ����������</td>
    <td height="25" align="center" colspan="2" bgcolor="#FFFFCC" class="zhengwenjiacu">������������</td>
    <td height="25" align="center" colspan="2" bgcolor="#FFFFCC" class="zhengwenjiacu">�ຢ��������</td>
</tr>
<tr class="zhengwen">
    <td width="100" height="25" align="right" bgcolor="#CCFFFF">С�ƣ�</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
    <td width="100" align="right" bgcolor="#CCFFFF"">С�ƣ�</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
    <td width="100" align="right" bgcolor="#CCFFFF">С�ƣ�</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
    <td width="100" align="right" bgcolor="#CCFFFF">С�ƣ�</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
</tr>
<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF">�У�</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
    <td align="right" bgcolor="#CCFFFF">�У�</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
    <td align="right" bgcolor="#CCFFFF">�У�</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
    <td align="right" bgcolor="#CCFFFF">�У�</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
</tr>

<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF">Ů��</td>
    <td align="left" width="*" ><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
    <td align="right" bgcolor="#CCFFFF">Ů��</td>
    <td align="left" width="*" ><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
    <td align="right" bgcolor="#CCFFFF">Ů��</td>
    <td align="left" width="*" ><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
    <td align="right" bgcolor="#CCFFFF">Ů��</td>
    <td align="left" width="*" ><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
</tr>
<tr class="zhengwen">
  <td height="25" align="right" bgcolor="#CCFFFF">�������ߣ�</td>
  <td align="left" width="140"><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
  <td align="right" bgcolor="#CCFFFF">�������ߣ�</td>
  <td align="left" width="140" ><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
<td align="right" bgcolor="#CCFFFF">�������ߣ�</td>
  <td align="left" width="140" ><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
<td align="right" bgcolor="#CCFFFF">�������ߣ�</td>
  <td align="left" width="140" ><asp:TextBox ID="txtFileds19" runat="server" EnableViewState="False" MaxLength="25" Width="100px"/></td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- ������ť m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="�� �ύ ��" OnClick="btnAdd_Click"  CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>