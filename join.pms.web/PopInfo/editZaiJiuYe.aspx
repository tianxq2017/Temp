<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editZaiJiuYe.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.editZaiJiuYe" %>

<%@ Register Src="../userctrl/ucAreaSelXian.ascx" TagName="ucAreaSelXian" TagPrefix="uc1" %>

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
    window.showModalDialog("/includes/Calendar.htm",obj,"dialogWidth:200px; dialogHeight:210px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; ");    
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
<!--��Ů����,��������,����״��,�Ǽ�����,�������,����֤����,������,�־�ס��,��������,���̵�ַ-->
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="100" bgcolor="#FFFFCC">�������ڣ�</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="PopCalendar(txtReportDate);return false;"  runat="server" />
    <input id="Image2" onclick="PopCalendar(txtReportDate);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
    (�ʱ��,������ʵ�ʵĹ�������,��ѡ�񵽸�������֮��)
    </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu"  bgcolor="#FFFFCC">���ݵ�λ��</td>
  <td align="left"><asp:DropDownList ID="DDLReportArea" runat="server"><asp:ListItem>��ѡ��</asp:ListItem></asp:DropDownList></td>
</tr></table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="100">������</td>
    <td align="left" width="300"><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
 <td align="right" height="25" class="zhengwenjiacu" width="100">����֤�ţ�</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" >�Ա�</td>
  <td align="left"><asp:DropDownList ID="DDLFileds03" runat="server">
    <asp:ListItem>��</asp:ListItem>
    <asp:ListItem>Ů</asp:ListItem>
    </asp:DropDownList></td>
     <td align="right" height="25" class="zhengwenjiacu">���壺</td>
  <td align="left">
  <asp:DropDownList ID="DDLFileds04" runat="server">
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
</asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">����״����</td>
  <td align="left">
   <asp:DropDownList ID="DDLFileds06" runat="server">
    <asp:ListItem>����</asp:ListItem>
    <asp:ListItem>һ��</asp:ListItem>
    <asp:ListItem>��</asp:ListItem>
  </asp:DropDownList></td>
  <td align="right" height="25" class="zhengwenjiacu">����״����</td>
  <td align="left">
  <asp:DropDownList ID="DDLFileds07" runat="server">
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>�ٻ�</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>���</asp:ListItem>
        </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">������ò��</td>
  <td align="left">
  <asp:DropDownList ID="DDLFileds08" runat="server">
    <asp:ListItem>��Ա</asp:ListItem>
    <asp:ListItem>��Ա</asp:ListItem>
      <asp:ListItem>���嵳����ʿ</asp:ListItem>
      <asp:ListItem>�޵���</asp:ListItem>
      <asp:ListItem>Ⱥ��</asp:ListItem>
    </asp:DropDownList>
      </td>
 <td align="right" height="25" class="zhengwenjiacu">ѧ����</td>
  <td align="left">
     <asp:DropDownList ID="DDLFileds09" runat="server">
    <asp:ListItem>Сѧ</asp:ListItem>
    <asp:ListItem>����</asp:ListItem>
      <asp:ListItem>����/ְר</asp:ListItem>
      <asp:ListItem>��ר</asp:ListItem>
      <asp:ListItem>����</asp:ListItem>
      <asp:ListItem>�о���</asp:ListItem>
    </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">��ϵ�绰��</td>
  <td align="left"><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  <td align="right" height="25" class="zhengwenjiacu">�������ڣ�</td>
  <td align="left">
    <input id="txtFileds05" readonly="readonly" size="29" onclick="PopCalendar(txtFileds05);return false;"  runat="server" />
    <input id="Image4" onclick="PopCalendar(txtFileds05);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
  </td></tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="100">��Ա���ͣ�</td>
  <td align="left" width="300">
   <asp:DropDownList ID="DDLFileds11" runat="server">
    <asp:ListItem>������</asp:ListItem>
    <asp:ListItem>�¸�ʧҵ��Ա</asp:ListItem> 
    <asp:ListItem>������ũ��</asp:ListItem> 
    <asp:ListItem>�ͱ���</asp:ListItem> 
    <asp:ListItem>����רԺУ��ҵ��</asp:ListItem>  
    <asp:ListItem>������Ա</asp:ListItem>  
    <asp:ListItem>�м���</asp:ListItem>   
    <asp:ListItem>����רҵ��Ա����</asp:ListItem>   
    <asp:ListItem>����</asp:ListItem>    
    </asp:DropDownList></td> 
    <td align="right" height="25" class="zhengwenjiacu" width="100">��ҵ�Ǽ�״̬��</td>
  <td align="left">
  <asp:DropDownList ID="DDLFileds12" runat="server">
    <asp:ListItem>δ��ҵ</asp:ListItem>
    <asp:ListItem>�Ѿ�ҵ</asp:ListItem>    
    </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">���״̬��</td>
  <td align="left">
  <asp:DropDownList ID="DDLFileds14" runat="server">
    <asp:ListItem>δ���</asp:ListItem>
    <asp:ListItem>�����</asp:ListItem>    
    </asp:DropDownList></td>
      <td align="right" height="25" class="zhengwenjiacu">�Ƿ��Ѿ���ӡ��</td>
  <td align="left">
    <asp:DropDownList ID="DDLFileds15" runat="server">
    <asp:ListItem>δ��ӡ</asp:ListItem>
    <asp:ListItem>�Ѵ�ӡ</asp:ListItem>    
    </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">�Ǽ����ڣ�</td>
  <td align="left"><input id="txtFileds13" readonly="readonly" size="29" onclick="PopCalendar(txtFileds13);return false;"  runat="server" />
    <input id="Image5" onclick="PopCalendar(txtFileds13);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" /></td>
  <td align="right" height="25" class="zhengwenjiacu"></td>
  <td align="left"></td>
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