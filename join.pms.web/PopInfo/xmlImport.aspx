<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xmlImport.aspx.cs" Inherits="AreWeb.OnlineCertificate.PopInfo.xmlImport" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>�ޱ���ҳ</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/index.css" type="text/css" rel="stylesheet">
<script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
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
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ������һ��д������Ϣ�󣻵�����ύ����ť����</div>
<!-- ҳ����� -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- �༭��--><br/>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1"  class="zhengwen">
<tr class="zhengwen">
  <td width="150" height="25" align="right" class="zhengwenjiacu">1��ѡ���ļ���</td>
  <td width="*" align="left"><asp:FileUpload ID="upFiles" runat="server"  /></td>
</tr>
<tr class="zhengwen">
  <td align="right" class="zhengwenjiacu" style="height: 35px">2���ϴ��ļ���</td>
  <td width="*" align="left" style="height: 35px">
    <asp:Button ID="butUpLoad" runat="server" Text="�ϴ�..." OnClick="butUpLoad_Click" />
    <input type="hidden" name="sourceFile" id="sourceFile" value="" runat="server" style="display:none;width: 1px;height:1px;"/>
  </td>
</tr>
<tr>
    <td align="right" height="10" class="lvtCol"></td>
    <td align="left" width="*" class="zhengwen">
    <asp:Label ID="LabelMsg" runat="server" Text="����������������ĵȴ������ݱ����������ķ�ʽ��������,,����ʱ��Ӧ��������������ʸ߷�ʱ�Σ�"></asp:Label>
    </td>
</tr></table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1"  class="zhengwen">
<!--
<tr class="zhengwen">
  <td width="150" align="right" height="25" class="zhengwenjiacu">3��ѡ�����ڣ�</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="PopCalendar(txtReportDate);return false;"  runat="server" />
    <input id="Image1" onclick="PopCalendar(txtReportDate);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
    (�ʱ��,������ʵ�ʵĹ�������,����ѡ�񵽸�������֮��)
    </td>
</tr>-->
<tr class="zhengwen">
    <td width="150" align="right" height="50" class="zhengwenjiacu">3��ִ�е��룺</td>
    <td align="left" width="*" class="small"><asp:Button ID="butImport" runat="server"  Text="����..." OnClick="butImport_Click"  />&nbsp;    </td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<br/>
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>