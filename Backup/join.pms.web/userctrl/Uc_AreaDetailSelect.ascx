<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Uc_AreaDetailSelect.ascx.cs" Inherits="join.pms.web.userctrl.Uc_AreaDetailSelect" %>
<!-- ScriptManager�ؼ�����ҳ����� -->

<!--����610900000000--><select name="txtDiShi" id="txtDiShi" class="inputdd"><option value="610725000000" selected>ͨ����</option></select>
<!--����-->&nbsp;&nbsp;<select name="txtQuXian" id="txtQuXian" class="inputdd"><option value="150524000000" selected>������</option></select>
<!--�ֵ����´�-->&nbsp;&nbsp;
<asp:DropDownList ID="DropDownListState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListState_SelectedIndexChanged" CssClass="inputdd">
<asp:ListItem Value="">��ѡ��ְ�</asp:ListItem>
</asp:DropDownList>
<!--����/��-->&nbsp;&nbsp;
<asp:DropDownList ID="DropDownListCity" runat="server" CssClass="inputdd">
<asp:ListItem Value="">��ѡ������(��)</asp:ListItem>
</asp:DropDownList>
