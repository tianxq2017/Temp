<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editMarriage.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.editMarriage" %>

<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
    window.showModalDialog("/includes/Calendar.htm",obj,"dialogWidth:200px; dialogHeight:210px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; ");    
}
</script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ������һ��д���л����Ǽ���Ϣ�󣻵�����ύ����ť������5����ǰ����������ύ������</div>
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
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="PopCalendar(txtReportDate);return false;"  runat="server" />
    <input id="Image2" onclick="PopCalendar(txtReportDate);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
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
    <td align="right" height="25" class="zhengwenjiacu" width="100">ҵ�����ͣ�</td>
    <td align="left" width="*"><asp:DropDownList ID="DDLFileds01" runat="server">
            <asp:ListItem>���Ǽ�</asp:ListItem>
            <asp:ListItem>���Ǽ�</asp:ListItem>
        </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="100">֤�����룺</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="100">�Ǽ����ڣ�</td>
    <td align="left" width="*"><input id="txtFileds03" readonly="readonly" size="29" onclick="PopCalendar(txtFileds03);return false;"  runat="server" />
    <input id="Image3" onclick="PopCalendar(txtFileds03);return false;" tabIndex="1" type="image" src="/images/calendar.gif" align="absMiddle"></td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td width="100" height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">�з�������</td>
    <td align="left" width="300"><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td width="100" align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">Ů��������</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<!--
ҵ������,֤������,�Ǽ�ʱ��,
Fileds01,Fileds02,Fileds03,
�з�����,���֤��,ְҵ,��ͥ��ַ,����,�Ļ��̶�,����״��,��������,
Fileds04,Fileds05,Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,Fileds11,
Ů������,���֤��,ְҵ,��ͥ��ַ,����,�Ļ��̶�,����״��,������λ
Fileds12,Fileds13,Fileds14,Fileds15,Fileds16,Fileds17,Fileds18,Fileds19
-->
<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">���֤�ţ�</td>
    <td align="left" width="300"><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">���֤�ţ�</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>

<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">ְҵ��</td>
    <td align="left" width="300" >
    <asp:DropDownList ID="DDLFileds06" runat="server">
<asp:ListItem>���һ��ء���Ⱥ��֯������ҵ��λ������</asp:ListItem>
<asp:ListItem>רҵ������Ա</asp:ListItem>
<asp:ListItem>����Ա��������Ա���й���Ա</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>�̷�</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>װ��</asp:ListItem>
<asp:ListItem>������ҵ������Ա</asp:ListItem>
<asp:ListItem>ũ���֡������桢ˮ��ҵ������Ա</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>���������������豸������Ա���й���Ա</asp:ListItem>
<asp:ListItem>�޹̶�ְҵ</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
</asp:DropDownList>
    </td>
    <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">ְҵ��</td>
    <td align="left" width="*" >
    <asp:DropDownList ID="DDLFileds14" runat="server">
<asp:ListItem>���һ��ء���Ⱥ��֯������ҵ��λ������</asp:ListItem>
<asp:ListItem>רҵ������Ա</asp:ListItem>
<asp:ListItem>����Ա��������Ա���й���Ա</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>�̷�</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>װ��</asp:ListItem>
<asp:ListItem>������ҵ������Ա</asp:ListItem>
<asp:ListItem>ũ���֡������桢ˮ��ҵ������Ա</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>���������������豸������Ա���й���Ա</asp:ListItem>
<asp:ListItem>�޹̶�ְҵ</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
</asp:DropDownList>
    </td>
</tr>

<tr class="zhengwen">
  <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">���壺</td>
  <td align="left" width="300" >
  <asp:DropDownList ID="DDLFileds08" runat="server">
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>�ɹ���</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>ά�����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>׳��</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>��������</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>���</asp:ListItem>
<asp:ListItem>��ɽ��</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>ˮ��</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>�¶�������</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>Ǽ��</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>ë����</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>��������</asp:ListItem>
<asp:ListItem>ŭ��</asp:ListItem>
<asp:ListItem>���α����</asp:ListItem>
<asp:ListItem>����˹��</asp:ListItem>
<asp:ListItem>���¿���</asp:ListItem>
<asp:ListItem>�°���</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>ԣ����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>��������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>���״���</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>�Ű���</asp:ListItem>
<asp:ListItem>�����</asp:ListItem>
<asp:ListItem>��ŵ��</asp:ListItem>
<asp:ListItem>δʶ��</asp:ListItem>
<asp:ListItem>�����м�</asp:ListItem>
</asp:DropDownList>

  </td>
  <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">���壺</td>
  <td align="left" width="*" ><asp:DropDownList ID="DDLFileds16" runat="server">
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>�ɹ���</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>ά�����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>׳��</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>��������</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>���</asp:ListItem>
<asp:ListItem>��ɽ��</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>ˮ��</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>�¶�������</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>Ǽ��</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>ë����</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>��������</asp:ListItem>
<asp:ListItem>ŭ��</asp:ListItem>
<asp:ListItem>���α����</asp:ListItem>
<asp:ListItem>����˹��</asp:ListItem>
<asp:ListItem>���¿���</asp:ListItem>
<asp:ListItem>�°���</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>ԣ����</asp:ListItem>
<asp:ListItem>����</asp:ListItem>
<asp:ListItem>��������</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>���״���</asp:ListItem>
<asp:ListItem>������</asp:ListItem>
<asp:ListItem>�Ű���</asp:ListItem>
<asp:ListItem>�����</asp:ListItem>
<asp:ListItem>��ŵ��</asp:ListItem>
<asp:ListItem>δʶ��</asp:ListItem>
<asp:ListItem>�����м�</asp:ListItem>
</asp:DropDownList>
</td>
</tr>

<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">�Ļ��̶ȣ�</td>
    <td align="left" width="300" >
    <asp:DropDownList ID="DDLFileds09" runat="server">
    <asp:ListItem>Сѧ</asp:ListItem>
    <asp:ListItem>����</asp:ListItem>
      <asp:ListItem>����/ְר</asp:ListItem>
      <asp:ListItem>��ר</asp:ListItem>
      <asp:ListItem>����</asp:ListItem>
      <asp:ListItem>�о���</asp:ListItem>
    </asp:DropDownList>
    </td>
    <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">�Ļ��̶ȣ�</td>
    <td align="left" width="*" ><asp:DropDownList ID="DDLFileds17" runat="server">
    <asp:ListItem>Сѧ</asp:ListItem>
    <asp:ListItem>����</asp:ListItem>
      <asp:ListItem>����/ְר</asp:ListItem>
      <asp:ListItem>��ר</asp:ListItem>
      <asp:ListItem>����</asp:ListItem>
      <asp:ListItem>�о���</asp:ListItem>
    </asp:DropDownList></td>
</tr>

<tr class="zhengwen">
  <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">����״����</td>
  <td align="left" width="300" ><asp:DropDownList ID="DDLFileds10" runat="server">
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>�ٻ�</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>���</asp:ListItem>
        </asp:DropDownList></td>
  <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">����״����</td>
  <td align="left" width="*" ><asp:DropDownList ID="DDLFileds18" runat="server">
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>�ٻ�</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>���</asp:ListItem>
        </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">�������ڣ�</td>
    <td align="left" width="300">
    <input id="txtFileds11" readonly="readonly" size="29" onclick="PopCalendar(txtFileds11);return false;"  runat="server" />
    <input id="Image1" onclick="PopCalendar(txtFileds11);return false;" tabIndex="1" type="image" src="/images/calendar.gif" align="absMiddle">
    </td>
    <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">�������ڣ�</td>
    <td align="left" width="*">
    <input id="txtFileds19" readonly="readonly" size="29" onclick="PopCalendar(txtFileds19);return false;"  runat="server" />
    <input id="Image4" onclick="PopCalendar(txtFileds19);return false;" tabIndex="1" type="image" src="/images/calendar.gif" align="absMiddle">
    </td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="100">�з���ϸ��ַ��</td>
    <td align="left" width="*">
        <uc1:ucAreaSel ID="UcAreaSel07" runat="server" />
    </td>
</tr>
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="100">Ů����ϸ��ַ��</td>
    <td align="left" width="*">
        <uc1:ucAreaSel ID="UcAreaSel15" runat="server" />
    </td>
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
