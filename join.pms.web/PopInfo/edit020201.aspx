<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit020201.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.edit020201" %>

<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>�ޱ���ҳ</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
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
    .butStyle {line-height: 20px; color: #000000; text-decoration: none; font-size: 12px;}
    -->
</style>
<script language="javascript" type="text/javascript">
function PopCalendar(obj){
    showx = event.screenX - event.offsetX - 4 - 10 ; // + deltaX;
    showy = event.screenY - event.offsetY -168; // + deltaY;
    newWINwidth = 210 + 4 + 18;
    window.showModalDialog("/includes/Calendar.htm",obj,"dialogWidth:200px; dialogHeight:192px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; ");    
}
</script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ������һ��д����סԺ����ʵ���Ǽ���Ϣ�󣻵�����ύ����ť������5����ǰ����������ύ������</div>
<!-- ҳ����� -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4" class="zhengwen"><tr><td bgcolor="#F3FCFF" align="left">
<!-- �༭��  -->
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150"  bgcolor="#CCFFFF">�������ڣ�</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="PopCalendar(txtReportDate);return false;"  runat="server" />
    <input id="Image1" onclick="PopCalendar(txtReportDate);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
    (�ʱ��,������ʵ�ʵĹ����·�,��ѡ�񵽸�������֮��)
    </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu"  bgcolor="#CCFFFF">���ݵ�λ��</td>
  <td align="left"><asp:DropDownList ID="DDLReportArea" runat="server"><asp:ListItem>��ѡ��</asp:ListItem></asp:DropDownList></td>
</tr></table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<!--ĸ������,����,����,����,����֤��,������ַ,�־�ס��-->
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td height="25" align="right"  class="zhengwenjiacu" width="150">����������</td>
  <td align="left" ><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="20" Width="200px"/></td>
</tr>
<tr class="zhengwen"><td align="right"  class="zhengwenjiacu">֤�����ͣ�</td>
  <td align="left"><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" >֤�����룺</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="18" Width="200px" /></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">���Σ�</td>
  <td align="left"><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="2" Width="100px" /></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">����ʱ�䣺</td>
  <td align="left">
    <input id="txtFileds05" readonly="readonly" size="29" onclick="PopCalendar(txtFileds05);return false;"  runat="server" />
    <input id="Image3" onclick="PopCalendar(txtFileds05);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
  </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">����ص㣺</td>
  <td align="left"><uc1:ucAreaSel ID="UcAreaSel06" runat="server" /></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">����Ա��</td>
  <td align="left"><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="20" Width="200px" /></td>
</tr>
</table>
<!--��������,֤������,֤������,����,����ʱ��,����ص�,����Ա,�ɷ�����,֤������,֤������,����������,�Ա�,����״��,��ͥ��ַ,�Ǽ���Ա,�Ǽ�ʱ��-->
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td width="150" align="right" height="25" class="zhengwenjiacu">�ɷ�������</td>
  <td align="left"><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="20" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">֤�����ͣ�</td>
  <td align="left"><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen"><td align="right"  class="zhengwenjiacu">֤�����룺</td>
  <td align="left"><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="18" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td height="25" align="right" class="zhengwenjiacu">������������</td>
  <td align="left"><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td height="25" align="right" class="zhengwenjiacu">�Ա�</td>
  <td align="left">
  <asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="5" Width="200px" /></td>
</tr>
<tr class="zhengwen">
  <td height="25" align="right" class="zhengwenjiacu">����״����</td>
  <td align="left">
  <asp:DropDownList ID="DDLFileds13" runat="server">
    <asp:ListItem>����</asp:ListItem>
    <asp:ListItem>һ��</asp:ListItem>
    <asp:ListItem>��</asp:ListItem>
  </asp:DropDownList>
  </td>
</tr>
<!--������,�־�ס��-->
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">���������أ�</td>
  <td align="left"><uc1:ucAreaSel ID="UcAreaSel14" runat="server" /></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">�־�ס�أ�</td>
  <td align="left"><uc1:ucAreaSel ID="UcAreaSel15" runat="server" /></td>
</tr>
<tr class="zhengwen">
  <td height="25" align="right" class="zhengwenjiacu">�Ǽ���Ա��</td>
  <td align="left"><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="16" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td height="25" align="right" class="zhengwenjiacu">�Ǽ�ʱ�䣺</td>
  <td align="left">
    <input id="txtFileds17" readonly="readonly" size="29" onclick="PopCalendar(txtFileds17);return false;"  runat="server" />
    <input id="Image2" onclick="PopCalendar(txtFileds17);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
  </td>
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
