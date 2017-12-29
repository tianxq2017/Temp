<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucYiMiaoSel.ascx.cs" Inherits="join.pms.web.userctrl.ucYiMiaoSel" %>
<!-- ScriptManager控件请在页面添加 -->
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<!------start--------->
<asp:DropDownList ID="DropDownListSheng" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListSheng_SelectedIndexChanged"  >
<asp:ListItem>请选择疫苗</asp:ListItem></asp:DropDownList>
<asp:DropDownList ID="DropDownListDiShi" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListDiShi_SelectedIndexChanged"  >
    <asp:ListItem>请选择</asp:ListItem>
</asp:DropDownList>


<asp:TextBox ID="txtDrpBacClass" style="display:none;" runat="server"  EnableViewState="False" MaxLength="35" />
<asp:TextBox ID="txtDrpBacName" style="display:none;" runat="server"  EnableViewState="False" MaxLength="35" />
<asp:TextBox ID="txtDrpBacClass1" style="display:none;" runat="server"  EnableViewState="False" MaxLength="35" />
<asp:TextBox ID="txtDrpBacName1" style="display:none;" runat="server"  EnableViewState="False" MaxLength="35" />
<!------end--------->
</ContentTemplate></asp:UpdatePanel>
