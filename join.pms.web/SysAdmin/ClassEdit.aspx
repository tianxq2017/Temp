<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassEdit.aspx.cs" Inherits="join.pms.web.SysAdmin.ClassEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�ޱ���ҳ</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312"/>
	<link href="../includes/dataGrid.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
<!-- ҳ����� -->
<input type="hidden" name="txtMenuID" id="txtMenuID" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtMenuCount" id="txtMenuCount" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtMenuCode" id="txtMenuCode" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtMaxCode" id="txtMaxCode" value="" runat="server" style="display:none;"/>
<!-- ѡ���ڵ� -->
<table width="480" border="0" cellspacing="0" cellpadding="0">
<tr>
	<td bgcolor="#ffffcc" style="height: 20px">
        ��ǰѡ���ڵ�:<font color="#cc0000"><asp:label id="LabelSelectNode" runat="server" EnableViewState="false"></asp:label></font>
	</td>
</tr>
</table>

<p style="font-size:12px">һ���Ե�ǰѡ�нڵ�����޸ġ�ɾ�����������ޱ�Ҫ��������д˲�����</p>

<table width="480" cellSpacing="0"  cellPadding="0" border="0">
<tbody>
<tr bgcolor="#cccccc">
<td width="120" height="25" class="titleLine">&nbsp;�ڵ�����</td>
<td width="120" class="titleLine">Ӣ������</td>
<td width="120" class="titleLine">�ڵ�����</td>
<td class="titleLine">����&nbsp;</td>
</tr>
<tr>
<td height="30" class="tdLine"><asp:TextBox ID="txtClassName" runat="server" Width="85px" MaxLength="25"></asp:TextBox></td>
<td class="tdLine"><asp:TextBox ID="txtClassEName" runat="server" MaxLength="20" Width="80px"></asp:TextBox></td>
<td class="tdLine"><asp:TextBox ID="txtUpdateClassIndex" runat="server" MaxLength="1" Width="15px"></asp:TextBox></td>
<td class="tdLine">
<asp:button id="ButUpdate" runat="server" Text=" ���� " onclick="ButUpdate_Click"></asp:button>
<asp:button id="ButDelete" runat="server" Text=" ɾ�� " onclick="ButDelete_Click"></asp:button>
</td>
</tr>
</tbody>
</table>


<P style="font-size:12px">�����ڴ˽ڵ��������ӽڵ�</P>

<table width="480" cellSpacing="0"  cellPadding="0" border="0">
<tbody>
<tr bgcolor="#cccccc">
<td width="120" height="25" class="titleLine">&nbsp;�ڵ�����</td>
<td width="120" class="titleLine">Ӣ�ı���</td>
<td width="120" class="titleLine">�ڵ�����</td>
<td class="titleLine">����&nbsp;</td>
</tr>
<tr>
<td height="30" class="tdLine"><asp:TextBox ID="txtAddClassName" runat="server" Width="85px" MaxLength="20"></asp:TextBox></td>
<td class="tdLine"><asp:TextBox ID="txtAddClassEName" runat="server" Width="80px" MaxLength="20" ></asp:TextBox></td>
<td class="tdLine"><asp:TextBox ID="txtAddClassIndex" runat="server" Width="15px" MaxLength="1" ></asp:TextBox></td>
<td class="tdLine"><asp:button id="ButAddNew" runat="server" Text=" ���� " onclick="ButAddNew_Click"></asp:button></td>
</tr>
</tbody>
</table>
<!-- ��ʾ�ӽڵ� --><br/>
<table width="480" cellSpacing="0"  cellPadding="0" border="0">
<tbody>
<tr bgcolor="#cccccc">
<td width="100" height="25" class="titleLine">&nbsp;�ڵ���</td>
<td width="120" class="titleLine">���ڵ���</td>
<td width="70" class="titleLine">�ڵ����</td>
<td width="60"class="titleLine">�ڵ�����</td>
<td width="70"class="titleLine">�ڵ�����</td>
<td width="70"class="titleLine">Ӣ�ı���</td>
<td width="70"class="titleLine">״̬��־</td>
</tr><asp:Literal ID="LiteralArea" runat="server"></asp:Literal></tbody></table>

    </form>
</body>
</html>
