<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnvCommReport.aspx.cs" Inherits="join.pms.web.UnvCommReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�ޱ���ҳ</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/index.css" type="text/css" rel="stylesheet">
<link rel="stylesheet" type="text/css" href="/css/right.css">
  <script src="/Scripts/jsdate.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ����ѡ��ͳ�Ʋ����󣻵������ѯ����ť�鿴ͳ�Ʊ������������������ť�������ݡ���</div>
<!-- ҳ����� -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="Div1" class="small" style="width: 100%; position: relative;">
<table class="lvtBg" width="100%" border="0" cellpadding="0" cellspacing="1"><tbody><tr><td><br/>

<!--��ѯ����-->
<table width="95%" border="0" cellspacing="0" cellpadding="0" class="small">
    <tr>
    <td width="70" height="30" align="left">&nbsp;���ݵ�λ��</td><td width="20" height="30" align="left">
    <asp:DropDownList ID="DDLArea" runat="server"></asp:DropDownList>
    </td><td width="20" align="left" bgcolor="#F4FAFA">&nbsp;</td>
    <td width="70" class="fb01" bgcolor="#F4FAFA">&nbsp;��ʼ���ڣ�</td>
    <td width="145" align="left" bgcolor="#F4FAFA">
    <input id="txtStartTime" runat="server"  onclick="SelectDate(document.getElementById('txtStartTime'),'yyyy-MM-dd')" readonly="readonly"  />
   </td>
    <!--start-->
    <td width="20" align="left" bgcolor="#F4FAFA">&nbsp;</td>
    <td width="70" class="fb01" bgcolor="#F4FAFA">&nbsp;��ֹ���ڣ�</td>
    <td width="145" align="left" bgcolor="#F4FAFA">
     <input id="txtEndTime" runat="server"  onclick="SelectDate(document.getElementById('txtEndTime'),'yyyy-MM-dd')" readonly="readonly"  />
   </td>
    <!--end-->
    <td width="20" align="left" bgcolor="#F4FAFA">&nbsp;</td>
    <td width="20" bgcolor="#F4FAFA">&nbsp;</td>
    <td align="left" bgcolor="#F4FAFA"><asp:Button ID="btnAdd" runat="server" Text="�� ��ѯ ��" class="submit6" OnClick="btnAdd_Click"></asp:Button>
    <!--<asp:Button ID="butExport" runat="server" Text="�� ���� ��" class="cusersubmit" OnClick="butExport_Click" ></asp:Button>-->
    <input type="button" name="ButBackPage" value="ͼ�η���" id="ButBackPage" onclick="javascript:window.location.href='<%=m_UrlParams %>';" class="submit6" />
    </td>
    </tr>
</table>
<!--���չʾ-->
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" >
<tr>
  <td height="25" align="right"></td>
  <td width="*" align="left"><asp:Literal ID="LiteralResults" runat="server" EnableViewState = "false"></asp:Literal></td>
</tr>
<tr>
    <td align="right" height="10" ></td>
    <td align="left" width="*"></td>
</tr>
</table>
<!-- ������ť -->

<!-- �������� End-->
</td></tr></tbody></table></div>

    </form>
</body>
</html>