<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata030110.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.chatdata030110" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>���룩���ѻ����举Ů���Ρ��������(ũ�壩</title>
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
  <td align="right" width="142" height="25" class="zhengwenjiacu" width="150" bgcolor="#FFFFCC">�������ڣ�</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
      (�ʱ��,������ʵ�ʵĹ����·�,ѡ����Ȼ��֮�ڼ���)
    </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" bgcolor="#FFFFCC">���ݵ�λ��</td>
  <td align="left">
      <uc1:ucDataAreaSel ID="UcDataAreaSel1" runat="server" />
  </td>
</tr></table>
<table width="1100" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="1100" border="0" cellspacing="3" cellpadding="1" class="zhengwen">
  <tr>
    <td align="right" bgcolor="#FFFFCC" colspan="3">��ݣ�</td>
    <td colspan="5">
        <asp:DropDownList ID="txtFileds01" runat="server">
            <asp:ListItem>2015</asp:ListItem>
            <asp:ListItem>2016</asp:ListItem>
            <asp:ListItem>2017</asp:ListItem>
            <asp:ListItem>2018</asp:ListItem>
            <asp:ListItem>2019</asp:ListItem>
            <asp:ListItem>2020</asp:ListItem>
        </asp:DropDownList>  
    </td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC" colspan="3">������</td>
    <td>
        <uc1:ucDataAreaSel ID="UcDataAreaSel2" runat="server" />
    </td>
    <td align="right" bgcolor="#FFFFCC"  rowspan="6">һ����Ů</td>
    <td align="right" bgcolor="#FFFFCC"  colspan="2">������</td>
    <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC"  colspan="3">���举Ů������</td>
    <td><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC"  colspan="2">���У����л���</td>
    <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC"  colspan="3">�ϼƣ�</td>
    <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" rowspan="2">������</td>
    <td align="right" bgcolor="#FFFFCC">С�ƣ�</td>
    <td><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC"  colspan="3">�㺢��Ů��</td>
    <td><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" >���У����л���</td>
    <td><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC" rowspan="7">������Ů</td>
    <td align="right" bgcolor="#FFFFCC"  colspan="2">������</td>
    <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC"  colspan="2">Ƥ��������</td>
    <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td  align="right" bgcolor="#FFFFCC" colspan="2">���У�˫Ů����</td>
    <td><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td  align="right" bgcolor="#FFFFCC" colspan="2">ҩ��������</td>
    <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC" rowspan="2">�ϻ�����</td>
    <td align="right" bgcolor="#FFFFCC" >С��</td>
    <td><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" rowspan="5">�ຢ��Ů</td>
    <td align="right" bgcolor="#FFFFCC" colspan="2">������</td>
    <td><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC" >���У�˫Ů����</td>
    <td><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC"  rowspan="4">���У�</td>
    <td align="right" bgcolor="#FFFFCC" >����������</td>
    <td><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC"  colspan="2">�ϻ�������</td>
    <td><asp:TextBox ID="txtFileds19" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC">�ϻ�������</td>
    <td><asp:TextBox ID="txtFileds20" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC"  colspan="2">Ƥ��������</td>
    <td><asp:TextBox ID="txtFileds21" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" >Ƥ��������</td>
    <td><asp:TextBox ID="txtFileds22" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC"  colspan="2">ҩ��������</td>
    <td><asp:TextBox ID="txtFileds23" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" >ҩ��������</td>
    <td><asp:TextBox ID="txtFileds24" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC" >��    �ƣ�</td>
    <td colspan="2"><asp:TextBox ID="txtFileds25" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" >ͳ�Ƹ����ˣ�</td>
    <td colspan="2"><asp:TextBox ID="txtFileds26" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" >����ˣ� </td>
    <td><asp:TextBox ID="txtFileds27" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
</table>
<table width="1100" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
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
