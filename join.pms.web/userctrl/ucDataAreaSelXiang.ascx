<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDataAreaSelXiang.ascx.cs" Inherits="join.pms.web.userctrl.ucDataAreaSelXiang" %>
<!-- ScriptManager�ؼ�����ҳ����� -->
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<!------start--------->
<asp:DropDownList ID="DropDownListSheng" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListSheng_SelectedIndexChanged" Visible="false" >
<asp:ListItem>��ѡ��ʡ</asp:ListItem></asp:DropDownList>
<asp:DropDownList ID="DropDownListDiShi" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListDiShi_SelectedIndexChanged"  >
    <asp:ListItem>��ѡ�����</asp:ListItem>
</asp:DropDownList>
<asp:DropDownList ID="DropDownListQuXian" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListQuXian_SelectedIndexChanged" >
    <asp:ListItem>��ѡ������</asp:ListItem>
</asp:DropDownList>
<asp:DropDownList ID="DropDownListXiang" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListXiang_SelectedIndexChanged" >
    <asp:ListItem>��ѡ������/�ְ�</asp:ListItem>
</asp:DropDownList>
<br/>
<div class="text text_t"><p><span><asp:TextBox ID="txtSelectArea" runat="server" EnableViewState="False" MaxLength="35" /></span></p></div>
<input type="hidden" name="txtSelAreaCode" id="txtSelAreaCode" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelQuCode" id="txtSelQuCode" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelXiangCode" id="txtSelXiangCode" value="" runat="server" style="display:none;"/>
<!------end--------->
</ContentTemplate></asp:UpdatePanel>
