<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAreaSelXian.ascx.cs" Inherits="join.pms.wap.userctrl.ucAreaSelXian" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<!------start--------->
<asp:DropDownList ID="DropDownListXiang" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListXiang_SelectedIndexChanged" >
    <asp:ListItem>请选择县/旗</asp:ListItem>
</asp:DropDownList>
<asp:DropDownList ID="DropDownListCun" runat="server" AutoPostBack="True" CssClass="font" >
    <asp:ListItem>请选择镇/街道</asp:ListItem>
</asp:DropDownList>&nbsp;
<!------end--------->
</ContentTemplate></asp:UpdatePanel>
