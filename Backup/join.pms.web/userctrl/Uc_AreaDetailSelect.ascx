<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Uc_AreaDetailSelect.ascx.cs" Inherits="join.pms.web.userctrl.Uc_AreaDetailSelect" %>
<!-- ScriptManager控件请在页面添加 -->

<!--地市610900000000--><select name="txtDiShi" id="txtDiShi" class="inputdd"><option value="610725000000" selected>通辽市</option></select>
<!--区县-->&nbsp;&nbsp;<select name="txtQuXian" id="txtQuXian" class="inputdd"><option value="150524000000" selected>库伦旗</option></select>
<!--街道办事处-->&nbsp;&nbsp;
<asp:DropDownList ID="DropDownListState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListState_SelectedIndexChanged" CssClass="inputdd">
<asp:ListItem Value="">请选择街办</asp:ListItem>
</asp:DropDownList>
<!--社区/村-->&nbsp;&nbsp;
<asp:DropDownList ID="DropDownListCity" runat="server" CssClass="inputdd">
<asp:ListItem Value="">请选择社区(村)</asp:ListItem>
</asp:DropDownList>
