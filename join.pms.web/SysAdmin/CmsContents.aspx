<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CmsContents.aspx.cs" Inherits="join.pms.web.SysAdmin.CmsContents" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>���ݷ�������</title>
<link href="/css/right.css" type="text/css" rel="stylesheet" />
<script language="javascript" src="/ckeditor4.4.5/ckeditor.js" type="text/javascript"></script>
<script language="javascript" src="/Scripts/CommUpload.js" type="text/javascript"></script>
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
<form id="form1" runat="server" enctype="multipart/form-data">
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ����������⡢���ġ����͸����󣬵�����ύ����ť������ɲ�����</div>
<!-- ҳ����� -->
<input type="hidden" name="txtUpFiles" id="txtUpFiles" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtFileUrl" id="txtFileUrl" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtCmsCID" id="txtCmsCID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone" class="part_bg">
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td>
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td align="left">
<!-- �༭��  -->
<table width="880" border="0"  cellpadding="3" cellspacing="0">
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="100">��Ϣ���⣺</td>
    <td align="left" width="*" class="txt1"><asp:TextBox ID="txtCmsTitle" runat="server" EnableViewState="False" MaxLength="50" Width="500px" /></td>
</tr>
<tr class="zhengwen">
    <td align="right" class="zhengwenjiacu">��Ϣ���ģ�</td>
    <td align="left" width="*">
    <!--ckeditor ���붨�� class="ckeditor"-->
����<asp:TextBox id="objCKeditor" class="ckeditor" TextMode="MultiLine"  runat="server" Height="350" Width="760"></asp:TextBox>
    </td>
</tr>
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="100">�������ڣ�</td>
    <td align="left" width="*" class="txt1">
    <input id="txtOprateDate" readonly="readonly" size="29" onclick="PopCalendar(txtOprateDate);return false;"  runat="server" />
    <input id="Image1" onclick="PopCalendar(txtOprateDate);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
    </td>
</tr>
<tr class="zhengwen">
    <td align="right" valign="top" style="padding-top: 17px;" class="zhengwenjiacu">���ݸ�����</td>
    <td align="left" width="*" style="padding-top: 13px;">
        <!-- �����ϴ� Start -->
        <input id="txtDocsID" name="txtDocsID" type="hidden" />
        <input id="txtDocsName" name="txtDocsName" value="" type="hidden" />
        <input id="txtSourceName" name="txtSourceName" readonly="readonly"  style="width:300px"/>
        <input type="button" value="�����ϴ�"  onclick="SelecCmsDocs('txtDocsID','txtSourceName','')"/>
        <!-- �����ϴ� End -->
    </td>
</tr>
</table>
<!-- ������ť m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="�ύ" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>
<input name="btnCancel" type="reset" id="btnCancel" value="����" class="submit6"/>
<input type="button" name="ButBackPage" value="����" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
</form>
<!-- ҳ����Ϣ -->
<br/><br/>
</body>
</html>
          
            
