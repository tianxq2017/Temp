<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit020204.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.edit020204" %>

<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>�ޱ���ҳ</title>
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
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150" bgcolor="#FFFFCC">�������ڣ�</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
    (�ʱ��,������ʵ�ʵĹ����·�,��ѡ�񵽸�������֮��)
    </td>
</tr><tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu"  bgcolor="#FFFFCC">���ݵ�λ��</td>
  <td align="left"><asp:DropDownList ID="DDLReportArea" runat="server"><asp:ListItem>��ѡ��</asp:ListItem></asp:DropDownList></td>
</tr></table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td width="150" height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">����֤���룺</td>
    <td align="left"><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">����������</td>
    <td align="left"><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="20" Width="200px"/></td>
 </tr>
 <tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">���֤�ţ�</td>
    <td align="left"><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="18" Width="200px"/></td>
 </tr>
<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">�������ʣ�</td>
    <td align="left"><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
 </tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td width="150" height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">����Ա������</td>
    <td align="left"><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">����Ա�Ա�</td>
    <td align="left"><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="18" Width="200px"/></td>
 </tr>
 <tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">�������ڣ�</td>
    <td align="left"><input id="txtFileds07" readonly="readonly" size="29" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" /></td>
 </tr>
<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">�뻧����ϵ��</td>
    <td align="left">
    <asp:DropDownList ID="DropDownList08" runat="server">
    <asp:ListItem>��ż</asp:ListItem>
    <asp:ListItem>��Ů</asp:ListItem>
    <asp:ListItem>ϱ��</asp:ListItem>
    <asp:ListItem>��ĸ</asp:ListItem>
    <asp:ListItem>�ֵܽ���</asp:ListItem>
    <asp:ListItem>�ﱲ</asp:ListItem>
    <asp:ListItem>(��)�游ĸ</asp:ListItem>
    </asp:DropDownList>
    </td>
 </tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" bgcolor="#FFFFCC">��ͥ���ͣ�</td>
  <td align="left"><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="10" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="150" bgcolor="#FFFFCC">��ͥסַ��</td>
    <td align="left" width="*"><uc1:ucAreaSel ID="UcAreaSel10" runat="server" /></td>
</tr>
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" bgcolor="#FFFFCC">�ɷѲ�����</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="10" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" bgcolor="#FFFFCC">ҽ�ƺ�����</td>
  <td align="left"><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="10" Width="200px"/></td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- ������ť m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="�� �ύ ��" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>