<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xlsImport.aspx.cs" Inherits="AreWeb.OnlineCertificate.PopInfo.xlsImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>�ޱ���ҳ</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/right.css" type="text/css" rel="stylesheet">
<script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
<script language="javascript" src="/Scripts/datectrl.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server" EnableViewState="false"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ������һ��������������ݵ���ʱ��ͬ�������ڵ�ͬһ�����ݣ�����������ظ��������(ȫԱ���ѯ���ݵ�������ѡ���������ں����ݵ�λ)����<br/>��������Ĭ��Ϊ��ˣ������뵼������ϸ�����������ݡ���</div>
<!-- ҳ����� -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone" class="part_bg2">
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td>
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td align="left" valign="top">
<!-- �༭��-->
<table width="880" border="0"  cellpadding="0" cellspacing="0"  class="zhengwen">
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
    <asp:Label ID="LabelMsg" runat="server" Text="����������������ĵȴ������ݱ����������ķ�ʽ��������,����֮ǰ�����Ƿ�����ظ�����(�����ظ�������������ٵ���),����ʱ��Ӧ��������������ʸ߷�ʱ�Σ�"></asp:Label>
    </td>
</tr></table>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1"  class="zhengwen">
<%if (m_FuncCode.Substring(0, 2) != "05")  {%>
<tr class="zhengwen">
  <td width="150" height="30" align="right" class="zhengwenjiacu">3�����ݵ�λ��</td>
  <td width="*" align="left">
      <asp:DropDownList ID="DDLReportArea" runat="server"></asp:DropDownList>(�������ݹ���������λ,����Чʶ�����ݹ�������)
      </td>
</tr>
<%} %>
<tr class="zhengwen">
  <td width="150" align="right" height="25" class="zhengwenjiacu">4���������ڣ�</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
    (������ʵ�ʵ������,��ѡ�񵽸����ݹ����·�֮������һ�켴��)
    </td>
</tr>
<tr class="zhengwen">
    <td width="150" align="right" height="30" class="zhengwenjiacu">5��ִ�е��룺</td>
    <td align="left" width="*" class="small"><asp:Button ID="butImport" runat="server"  Text="����..." OnClick="butImport_Click"   />&nbsp;    </td>
</tr>
</table>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="100%" border="0"  cellpadding="3" cellspacing="1"  class="zhengwen">
<tr class="zhengwen">
    <td width="150" align="right" height="50" class="zhengwenjiacu">������ʾ��</td>
    <td align="left" width="*" class="small"><asp:Literal ID="LiteralMsg" runat="server" EnableViewState="false"></asp:Literal></td>
</tr>
</table>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<input type="button" name="ButBackPage" value="����" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>