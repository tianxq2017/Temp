<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditWjYcbj.aspx.cs" Inherits="AreWeb.OnlineCertificate.PopInfo.EditWjYcbj" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>�в�������������Ϣ</title>
    <link href="/Styles/index.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="/includes/CommSys.js" type="text/javascript"></script>
    <script language="javascript" src="/Scripts/datectrl.js" type="text/javascript"></script>
    <script language="jscript" type="text/javascript">
function DoKeyPress(maxLen)
{ 
    var str=event.srcElement.value; 
    var oSR=document.selection.createRange(); 
    if(str.length<maxLen)
    { 
        event.returnValue = true; 
    } 
    else if(oSR.text.length>0)
    { 
        event.returnValue = true; 
    }
    else
    { 
        event.returnValue = false; 
    } 
} 
function CheckPaste(maxLen)
{ 
    var str=event.srcElement.value; 
    var oSR=document.selection.createRange(); 
    var strData=clipboardData.getData('text'); 
    var iLenPaste=maxLen-str.length+oSR.text.length; 
    oSR.text=strData.substring(0,iLenPaste); 
    event.returnValue = false; 
} 
</script>
</head>
<body>
    <form id="form1" runat="server">
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ������һ��д�����в�������������Ϣ�󣻵�����ύ����ť������5����ǰ����������ύ������</div>
<!-- ҳ����� -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- �༭��  -->
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="100"  bgcolor="#CCFFFF">�������ڣ�</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
    (�ʱ��,������ʵ�ʵĹ����·�,��ѡ�񵽸�������֮��)
    </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu"  bgcolor="#CCFFFF">���ݵ�λ��</td>
  <td align="left"><asp:DropDownList ID="DDLReportArea" runat="server"><asp:ListItem>��ѡ��</asp:ListItem></asp:DropDownList></td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">�в���������</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">���֤�ţ�</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">��ϵ�绰��</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">����״����</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">����������</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">�����������</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" TextMode="MultiLine" onkeydown="DoKeyPress(50)" onPaste="CheckPaste(50)" Height="80px" Width="500px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">����ָ������</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" TextMode="MultiLine" onkeydown="DoKeyPress(50)" onPaste="CheckPaste(50)" Height="80px" Width="500px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">��ס��ַ��</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">�ɷ�������</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">���֤�ţ�</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">��ϵ�绰��</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
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
