<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucRoleUserSel2.ascx.cs" Inherits="join.pms.web.userctrl.ucRoleUserSel2" %>
<!-- ScriptManager控件请在页面添加 -->
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<!------start--------->
<div style="100%">
<style type="text/css">#CheckBoxList_CheckBoxList_Name input{ margin:0px 3px 0px 5px; }</style>
<table><tr><td>
<asp:DropDownList ID="DropDownListSheng" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListSheng_SelectedIndexChanged" Visible="false">
<asp:ListItem>请选择省</asp:ListItem></asp:DropDownList>
<asp:DropDownList ID="DropDownListDiShi" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListDiShi_SelectedIndexChanged"  >
    <asp:ListItem>请选择地市</asp:ListItem>
</asp:DropDownList>
<asp:DropDownList ID="DropDownListQuXian" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListQuXian_SelectedIndexChanged" >
    <asp:ListItem>请选择区县</asp:ListItem>
</asp:DropDownList>
</td></tr>
<tr><td>
<asp:CheckBoxList ID="CheckBoxList_Name" runat="server" RepeatDirection="Horizontal" RepeatColumns="5"  EnableTheming="True" RepeatLayout="Flow"></asp:CheckBoxList>  
</td></tr></table>
</div>                  

<asp:TextBox ID="txtSelectArea" style="display:none;" runat="server"  EnableViewState="False" MaxLength="35" />
<asp:TextBox ID="txtSelAreaCode" style="display:none;" runat="server"  EnableViewState="False" MaxLength="35" />
<!------end--------->
</ContentTemplate></asp:UpdatePanel>
