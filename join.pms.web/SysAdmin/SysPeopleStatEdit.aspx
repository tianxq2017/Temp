<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysPeopleStatEdit.aspx.cs" Inherits="join.pms.web.SysAdmin.SysPeopleStatEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>�ǼǱ�</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="../css/main.css" rel="stylesheet" type="text/css" />
<link href="../css/list.css" rel="stylesheet" type="text/css" />
<script language="javascript" src="../includes/dataGrid.js" type="text/javascript"></script>
<script language="javascript" src="../includes/commOA.js" type="text/javascript"></script>
<script language="javascript" src="../includes/calendarHasTime.js" type="text/javascript"></script>
</head>
<body>
<form id="form1" runat="server" enctype="multipart/form-data">
<!-- ҳ����� -->
<input type="hidden" name="txtUrlParams" id="txtUrlParams" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtUpFiles" id="txtUpFiles" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtFuncNo" id="txtFuncNo" value="" runat="server" style="display:none;"/>
<!--uploadParams-->
<input type="hidden" name="txtFileID" id="txtFileID" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSourceFile" id="txtSourceFile" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSaveFile" id="txtSaveFile" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtFileType" id="txtFileType" value="" runat="server" style="display:none;"/>
<!-- ҳ�沼�� -->
<table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#F3F7F6"><tr><td>
<!-- ҳ�浼�� -->
<asp:Literal ID="LiteralNav" runat="server"></asp:Literal>
<!--  �༭-->
<div class="rightb"></div><div class="rightb">
<table width="100%" border="0" cellspacing="0" cellpadding="0"><tr><td>
    <table width="95%" border="0" cellspacing="0" cellpadding="0"><tr><td width="100">
    <table width="220" border="0" cellpadding="0" cellspacing="0" bgcolor="#26724B"><tr>
    <td width="10"><img src="/images/cnav03.jpg" width="10" height="27" /></td>
    <td align="center" bgcolor="#E6E0D1" class="fw"><strong><asp:Literal ID="LiteralTitle" runat="server"></asp:Literal></strong></td>
    <td width="10" align="right" ><img src="/images/cnav04.jpg" width="10" height="27" /></td></tr></table>
    </td><td width="10" align="center">&nbsp;</td><td width="85" align="center" class="gnbtn">
    <table width="85%" border="0" cellspacing="1" cellpadding="0"><tr>
    <td width="30" align="center"><img src="/images/icon_refresh.gif" width="23" height="20" /></td>
    <td align="left"><a href="#" onclick="javascript:document.location.reload();">ˢ��</a></td></tr></table>
    </td><td>&nbsp;</td></tr></table></td>
</tr>
<tr>
  <td height="400" align="left" valign="top" bgcolor="#FFFFFF" class="clistborder"><br />
    <table width="95%" border="0" cellspacing="0" cellpadding="0"><tr>
    <td width="20" height="30" align="left">&nbsp;</td>
    <td width="30" align="center" bgcolor="#E8E3D4" class="fb"><img src="../images/add.gif" width="12" height="12" /></td>
    <td align="left" bgcolor="#E8E3D4" class="page"><span class="fb"><strong><asp:Literal ID="LiteralFuncName" runat="server"></asp:Literal></strong></span></td>
    </tr></table>
    <!-- ����Ϣ Start -->
<!-- �༭�û���Ϣ -->
<table width="80%" border="0" cellpadding="0" cellspacing="0"> 
<tr>
  <td width="130" height="30" align="right" class="fb01">
      ��λ��</td>
  <td  align="left"><asp:TextBox ID="txtUintName" runat="server" CssClass="cuserinput"  MaxLength="30" Width="150px" style="background-color:#fafafa"/>*</td>
 <td width="135" height="30" align="right" class="fb01">
      ��ĩ��ס�˿ڣ�</td>
  <td  align="left"><asp:TextBox ID="txtCZRenKou" runat="server" CssClass="cuserinput"  MaxLength="10" Width="100px" style="background-color:#fafafa"/>*</td>
  <td width="110" height="30" align="right" class="fb01">
      ��ĩ���举Ů������</td>
  <td  align="left"><asp:TextBox ID="txtYLFunNv" runat="server" CssClass="cuserinput"  MaxLength="10" Width="100px" style="background-color:#fafafa"/></td>
</tr><tr>
  <td width="130" height="30" align="right" class="fb01">
      ��ĩ�ѻ����举Ů����</td>
  <td  align="left"><asp:TextBox ID="txtYHFunNv" runat="server" CssClass="cuserinput"  MaxLength="10" Width="104px" style="background-color:#fafafa"/>*</td>
 <td width="135" height="30" align="right" class="fb01">
      ��ȡ���ֱ��д�ʩ������</td>
  <td  align="left"><asp:TextBox ID="txtBYRenShu" runat="server" CssClass="cuserinput"  MaxLength="10" Width="101px" style="background-color:#fafafa"/>*</td>
  <td width="110" height="30" align="right" class="fb01">
      ����������</td>
  <td  align="left"><asp:TextBox ID="txtCSRenKou" runat="server" CssClass="cuserinput"  MaxLength="10" Width="98px" style="background-color:#fafafa"/></td>
</tr><tr>
  <td width="130" height="30" align="right" class="fb01">
      ����������</td>
  <td  align="left"><asp:TextBox ID="txtSWRenKou" runat="server" CssClass="cuserinput"  MaxLength="10" Width="105px" style="background-color:#fafafa"/>*</td>
 <td width="135" height="30" align="right" class="fb01">
      �����˿ڣ�</td>
  <td  align="left"><asp:TextBox ID="txtLCRenKou" runat="server" CssClass="cuserinput"  MaxLength="10" Width="102px" style="background-color:#fafafa"/>*</td>
  <td width="110" height="30" align="right" class="fb01">
      �����˿ڣ�</td>
  <td  align="left"><asp:TextBox ID="txtLRRenKou" runat="server" CssClass="cuserinput"  MaxLength="10" Width="101px" style="background-color:#fafafa"/></td>
</tr><tr>
  <td width="125" height="30" align="right" class="fb01" >
      ��ע��</td>
  <td  align="left" colspan="5"><asp:TextBox ID="txtRemarks" runat="server" CssClass="cuserinput"  MaxLength="200" Width="584px" style="background-color:#fafafa" Height="28px" TextMode="MultiLine"/>��200���ڣ�</td>
</tr>
</table>
<br/>
<!-- ������ť -->
<table cellSpacing="0" cellPadding="0" width="95%" border="0" ><tr>
<td width="100" height="25" align="right" class="fb01">&nbsp;</td>
<td width="*" align="left"><asp:Button ID="btnAdd" runat="server" Text="�� �ύ ��" OnClick="btnAdd_Click" class="cusersubmit"></asp:Button>
<input name="btnCancel" type="reset" id="Reset1" value="�� ���� ��" class="cuserreset"/></td>
</tr></table><br/>
<!-- ����Ϣ End -->
</td></tr></table>
</div><div class="rightb"></div>
<!-- End -->
</td></tr></table>
</form>
<!-- ҳ����Ϣ -->
</body>
</html>
