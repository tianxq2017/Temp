<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03020702.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03020702" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>7�����¶�ͯ�����ͽ������������</title>
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
    <div style="font-size:28px; text-align:center; font-weight:bold">7�����¶�ͯ�����ͽ������������<asp:Label ID="txt_RptTime" runat="server" Text="Label"></asp:Label>��</div>
    <span style="font-size:14px;">��λ��<%=str_AreaName%></span><br />
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;background-color:#ffffff">
        <thead>
        <tr style="text-align:center; background-color:#cccccc">
          <td rowspan="3">���λ</td>
            <td colspan="4" rowspan="2">�����</td>
            <td colspan="3">��ͯ��</td>
            <td colspan="15">5�����¶�ͯ�������</td>
            <td colspan="5">6������Ӥ��ĸ��ι�����</td>
            <td colspan="12">7�����¶�ͯ��������</td>
            <td colspan="14">5�����¶�ͯӪ������</td>
        </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="2">7������</td>
            <td rowspan="2">5������</td>
            <td rowspan="2">3������</td>
            <td colspan="4">5�����¶�ͯ������</td>
            <td rowspan="2">5�����¶�ͯ�����ʣ��룩</td>
            <td colspan="4">Ӥ��������</td>
            <td rowspan="2">Ӥ�������ʣ��룩</td>
            <td colspan="4">������������</td>
            <td rowspan="2">�����������ʣ��룩</td>
            <td rowspan="2">��������</td>
            <td colspan="2">ĸ��ι��</td>
            <td colspan="2">��ĸ��ι��</td>
            <td colspan="2">����������</td>
            <td colspan="2">����������ͪ��֢ɸ��</td>
            <td colspan="2">��������״�ٹ��ܼ���֢ɸ��</td>
            <td colspan="2">����������ɸ��</td>
            <td colspan="2">7�����¶�ͯ��������</td>
            <td colspan="2">3�����¶�ͯ��������</td>
            <td colspan="9">��ߣ��������ؼ��</td>
            <td colspan="5">Ѫ�쵰�׼��</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>�ϼ�</td>
            <td>��</td>
            <td>Ů</td>
            <td>�Ա���</td>
            <td>�ϼ�</td>
            <td>��</td>
            <td>Ů</td>
            <td>�Ա���</td>
            <td>�ϼ�</td>
            <td>��</td>
            <td>Ů</td>
            <td>�Ա���</td>
            <td>�ϼ�</td>
            <td>��</td>
            <td>Ů</td>
            <td>�Ա���</td>
            <td>����</td>
            <td>��%</td>
            <td>����</td>
            <td>��%</td>
            <td>����</td>
            <td>��%</td>
            <td>����</td>
            <td>��%</td>
            <td>����</td>
            <td>��%</td>
            <td>����</td>
            <td>��%</td>
            <td>����</td>
            <td>��%</td>
            <td>����</td>
            <td>��%</td>
            <td>�������</td>
            <td>����������</td>
            <td>�������ʣ�%��</td>
            <td>�����ٻ�����</td>
            <td>�����ٻ��ʣ�%��</td>
            <td>��������</td>
            <td>�����ʣ�%��</td>
            <td>��������</td>
            <td>�����ʣ�%��</td>
            <td>�������</td>
            <td>ƶѪ��������</td>
            <td>ƶѪ�����ʣ�%��</td>
            <td>���ض�ƶѪ��������</td>
            <td>���ض�ƶѪ�����ʣ�%��</td>
          </tr>
        </thead>
          <tr style="text-align:center;">
            <td>�ϼ�</td>
            <%for (int i = 0; i < arrNum.Length; i++)
              { %>
            <td><%=arrNum[i] %></td>
            <%} %>         
          </tr>
    <asp:Repeater ID="rep_Data" runat="server">
      <ItemTemplate>
        <tr style="text-align:center;">
            <td><%# Eval("AreaName")%></td>
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
            <td><%# Eval("Fileds17")%></td>
            <td><%# Eval("Fileds18")%></td>
            <td><%# Eval("Fileds19")%></td>
            <td><%# Eval("Fileds20")%></td>
            <td><%# Eval("Fileds21")%></td>
            <td><%# Eval("Fileds22")%></td>
            <td><%# Eval("Fileds23")%></td>
            <td><%# Eval("Fileds24")%></td>
            <td><%# Eval("Fileds25")%></td>
            <td><%# Eval("Fileds26")%></td>
            <td><%# Eval("Fileds27")%></td>
            <td><%# Eval("Fileds28")%></td>
            <td><%# Eval("Fileds29")%></td>
            <td><%# Eval("Fileds30")%></td>
            <td><%# Eval("Fileds31")%></td>
            <td><%# Eval("Fileds32")%></td>
            <td><%# Eval("Fileds33")%></td>
            <td><%# Eval("Fileds34")%></td>
            <td><%# Eval("Fileds35")%></td>
            <td><%# Eval("Fileds36")%></td>
            <td><%# Eval("Fileds37")%></td>
            <td><%# Eval("Fileds38")%></td>
            <td><%# Eval("Fileds39")%></td>
            <td><%# Eval("Fileds40")%></td>
            <td><%# Eval("Fileds41")%></td>
            <td><%# Eval("Fileds42")%></td>
            <td><%# Eval("Fileds43")%></td>
            <td><%# Eval("Fileds44")%></td>
            <td><%# Eval("Fileds45")%></td>
            <td><%# Eval("Fileds46")%></td>
            <td><%# Eval("Fileds47")%></td>
            <td><%# Eval("Fileds48")%></td>
            <td><%# Eval("Fileds49")%></td>
            <td><%# Eval("Fileds50")%></td>
            <td><%# Eval("Fileds51")%></td>
            <td><%# Eval("Fileds52")%></td>
            <td><%# Eval("Fileds53")%></td>
          </tr>
    </ItemTemplate>  
    </asp:Repeater>
    </table><br />
<!--����ҳ��-->
    <table width="100%" border="0" cellpadding="0" cellspacing="1" >
    <tr style="font-size:14px;">
    <td>�����ˣ�<asp:Label ID="txt_SldHeader" runat="server" Text="Label"></asp:Label></td>
    <td align="center">����ˣ�<asp:Label ID="txt_SldLeader" runat="server" Text="Label"></asp:Label></td>
    <td align="right">����ڣ�<asp:Label ID="txt_OprateDate" runat="server" Text="Label"></asp:Label>&nbsp;</td></tr>
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
