<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucRoleUserSel.ascx.cs" Inherits="join.pms.web.userctrl.ucRoleUserSel" %>
<!-- ScriptManager�ؼ�����ҳ����� -->
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<!------start--------->
<asp:DropDownList ID="DropDownListSheng" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListSheng_SelectedIndexChanged"  >
<asp:ListItem>��ѡ��ʡ</asp:ListItem></asp:DropDownList>
<asp:DropDownList ID="DropDownListDiShi" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListDiShi_SelectedIndexChanged"  >
    <asp:ListItem>��ѡ�����</asp:ListItem>
</asp:DropDownList>

<p style="padding-top:10px;">
�˻���<asp:Literal runat="server" ID="litAccount" ></asp:Literal>
&nbsp;&nbsp;��ϵ��ʽ��<asp:Literal runat="server" ID="litContactTel" ></asp:Literal>
&nbsp;&nbsp;��ַ��<asp:Literal runat="server" ID="litAddress" ></asp:Literal>
</p>
<asp:TextBox ID="txtSelectArea" style="display:none;" runat="server"  EnableViewState="False" MaxLength="35" Width="200px"/>
<input type="hidden" name="txtSelAreaCode" id="txtSelAreaCode" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelRoleID" id="txtSelRoleID" value="" runat="server" style="display:none;"/>
<!------end--------->
</ContentTemplate></asp:UpdatePanel>
