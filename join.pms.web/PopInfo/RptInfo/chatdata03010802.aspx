<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03010802.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03010802" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>���룩��Ƚ������ һ�鿴</title>
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
    <div style="font-size:28px; text-align:center; font-weight:bold"><asp:Label ID="txt_RptTime" runat="server" Text="Label"></asp:Label>��Ƚ������ </div>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" >
    <tr style="font-size:14px;">
    <td>����������<%=str_AreaName%></td>
    <td align="right">��λ���ˡ���&nbsp;</td>
    </tr>
    </table>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;background-color:#ffffff">
        <thead>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="2">����</td>
            <td colspan="9">ѡ�ø��ֱ��з�������</td>
            <td colspan="7">����ʵ�мƻ�������������</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>С��</td>
            <td>���Ծ���</td>
            <td>Ů�Ծ���</td>
            <td>���ù��ڽ�����</td>
            <td>Ƥ����ֲ</td>
            <td>�ڷ���ע�����ҩ</td>
            <td>������</td>
            <td>����ҩ</td>
            <td>����</td>
            <td>С��</td>
            <td>���Ծ���</td>
            <td>Ů�Ծ���</td>
            <td>���ù��ڽ�����</td>
            <td>ȡ�����ڽ�����</td>
            <td>�˹�����</td>
            <td>Ƥ����ֲ</td>
          </tr>
          <tr style="text-align:center;background-color:#ffffcc">
            <td height="22">�ϼ�</td>
            <%for (int i = 0; i < arrNum.Length; i++)
              { %>
            <td><%=arrNum[i] %></td>
            <%} %>
          </tr>
        </thead>
    <asp:Repeater ID="rep_Data" runat="server">
      <ItemTemplate>
        <tbody>
            <tr style="text-align:center;">
                <td style="background-color:#ccffff" height="22"><%# Eval("AreaName")%></td>
                <td><%# Eval("Fileds01")%></td>
                <td><%# Eval("Fileds02")%></td>
                <td><%# Eval("Fileds03")%></td>
                <td><%# Eval("Fileds04")%></td>
                <td><%# Eval("Fileds05")%></td>
                <td><%# Eval("Fileds06")%></td>
                <td><%# Eval("Fileds07")%></td>
                <td><%# Eval("Fileds08")%></td>
                <td><%# Eval("Fileds09")%></td>
                <td><%# Eval("Fileds10")%></td>
                <td><%# Eval("Fileds11")%></td>
                <td><%# Eval("Fileds12")%></td>
                <td><%# Eval("Fileds13")%></td>
                <td><%# Eval("Fileds14")%></td>
                <td><%# Eval("Fileds15")%></td>
                <td><%# Eval("Fileds16")%></td>
            </tr>           
          </tbody>
    </ItemTemplate>  
    </asp:Repeater>
    </table>
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
        <asp:CheckBox ID="ck_IsCheck" runat="server" /><span style=" font-size:12px; color:#f60; margin-bottom:10px;">ȷ���ϱ����ϱ�������������ݽ������������޸�.</span><br/>
        <asp:Button ID="btnUPSH" runat="server" Text="�� �ϱ� ��"  CssClass="submit6" OnClick="btnUPSH_Click"></asp:Button>
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