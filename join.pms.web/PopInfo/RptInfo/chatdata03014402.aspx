<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03014402.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03014402" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>���������߷������ͳ�Ʊ�</title>
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
    <div style="font-size:28px; text-align:center; font-weight:bold"><asp:Label ID="txt_RptTime" runat="server" Text="Label"></asp:Label>���������߷������ͳ�Ʊ�</div>
    <span style="font-size:14px;">��λ��<%=str_AreaName%></span><br />
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;background-color:#ffffff">
       <thead>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="4">����</td>
            <td rowspan="4">���举Ů����</td>
            <td colspan="20">�ѻ����举Ů�������</td>
            <td rowspan="4">����</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">�ϼ�</td>
            <td rowspan="3">�㺢��Ů</td>
            <td colspan="6">һ����Ů</td>
            <td colspan="7">������Ů</td>
            <td colspan="5">�ຢ��Ů</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="2">����</td>
            <td rowspan="2">���У����� ����</td>
            <td colspan="2">�ϻ���</td>
            <td rowspan="2">Ƥ������</td>
            <td rowspan="2">ҩ������</td>
            <td rowspan="2">����</td>
            <td rowspan="2">���У�˫Ů ����</td>
            <td colspan="2">������</td>
            <td rowspan="2">�ϻ�����</td>
            <td rowspan="2">Ƥ������</td>
            <td rowspan="2">ҩ������</td>
            <td rowspan="2">����</td>
            <td colspan="4">����</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>С��</td>
            <td>���У�����  ����</td>
            <td>С��</td>
            <td>���У�˫Ů  ����</td>
            <td>��������</td>
            <td>�ϻ�����</td>
            <td>Ƥ������</td>
            <td>ҩ������</td>
          </tr>
           </thead>
        <tbody>
        <asp:Literal ID="ltr_Content" runat="server"></asp:Literal>
        </tbody>
    </table>
    <br />
<br />
<!--����ҳ��-->
    <table width="100%" border="0" cellpadding="0" cellspacing="1" >
    <tr style="font-size:14px;">
    <td>�����ˣ�<asp:Label ID="txt_SldHeader" runat="server" Text="Label"></asp:Label></td>
    <td align="center">����ˣ�<asp:Label ID="txt_SldLeader" runat="server" Text="Label"></asp:Label></td>
    <td align="right">����ڣ�<asp:Label ID="txt_OprateDate" runat="server" Text="Label"></asp:Label>&nbsp;</td>
    </tr>
    </table>
    <br/>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" >
    <tr style="font-size:14px;">
        <td>
           ����������ݵĵ�λ����ӦΪ��<span style=" font-weight:bold;"><%=village_all_num%></span><br /> &nbsp;<span style=" font-weight:bold; color:green">���ϱ���<%=reported_num%></span><br /> &nbsp;<span style="color:#f60">δ�ϱ���<%=no_reported_num%>��<%=no_reported_name%>��</span>
        </td>
    </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="zhengwen" >
    <tr style="font-size:14px;">
    <td>
        <%if (IsReported == "0") {%>
        <asp:CheckBox ID="ck_IsCheck" runat="server" /><span style=" font-size:12px; color:#f60; margin-bottom:10px;">ȷ����ˣ���˺�����������ݽ������������޸�.</span><br/>
        <asp:Button ID="btnUPSH" runat="server" Text="�� ��� ��"  CssClass="submit6" OnClick="btnUPSH_Click"></asp:Button>
        <asp:Button ID="btnEdit" runat="server" Text="�� �༭ ��"  CssClass="submit6" OnClick="btnEdit_Click"></asp:Button>
        <%}%>
        <input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:history.go(-1);" class="submit6" />
    </td>
    </tr>
    </table>
     <br/>
     
</td></tr></table>
</td></tr></table>

</div>
    </form>
</body>
</html>
