<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03014502.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03014502" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title> ����ѻ����举Ů��ֳ�����������</title>
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
    <div style="font-size:28px; text-align:center; font-weight:bold"><asp:Label ID="txt_RptTime" runat="server" Text="Label"></asp:Label>����ѻ����举Ů��ֳ�����������</div>
    <span style="font-size:14px;">��λ��<%=str_AreaName%></span><br />
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">��λ</td>
            <td colspan="7">Ӧ���˴�</td>
            <td colspan="11">ʵ���˴�</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="2">С��</td>
            <td rowspan="2">39�꼰�����ϻ�</td>
            <td rowspan="2">ʹ��ҩ��</td>
            <td rowspan="2">δ��ȡ��ʩ</td>
            <td rowspan="2">�����������</td>
            <td rowspan="2">40�꼰�����ϻ���</td>
            <td rowspan="2">��ȡƤ���ʩ</td>
            <td rowspan="2">С��</td>
            <td rowspan="2">39�꼰�����ϻ�</td>
            <td rowspan="2">ʹ��ҩ��</td>
            <td rowspan="2">δ��ȡ  ��ʩ</td>
            <td rowspan="2">�����������</td>
            <td rowspan="2">40�꼰�����ϻ�</td>
            <td rowspan="2">��ȡƤ���ʩ</td>
            <td colspan="4">����</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>С��</td>
            <td>ũ��</td>
            <td>������ҵ��Ա</td>
            <td>�����˿�</td>
          </tr>
        </thead>
        <tbody>
            <asp:Literal ID="ltr_Content" runat="server"></asp:Literal>
          </tbody>
    </table><br />
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
        <td>������ʾ���ñ�����ݵĵ�λ����ӦΪ��<span style=" font-weight:bold;"><%=village_all_num%></span><br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style=" font-weight:bold; color:green">���ϱ���<%=reported_num%></span><br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#f60">δ�ϱ���<%=no_reported_num%>��<%=no_reported_name%>��</span>
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
