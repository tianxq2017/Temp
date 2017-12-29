<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDataAreaSelVillage.ascx.cs" Inherits="join.pms.web.userctrl.ucDataAreaSelVillage" %>
<!-- ScriptManager控件请在页面添加 -->
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<!------start--------->
<asp:DropDownList ID="DropDownListSheng" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListSheng_SelectedIndexChanged" Visible="false" >
<asp:ListItem>请选择省</asp:ListItem></asp:DropDownList>
<asp:DropDownList ID="DropDownListDiShi" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListDiShi_SelectedIndexChanged" >
    <asp:ListItem>请选择地市</asp:ListItem>
</asp:DropDownList>
<asp:DropDownList ID="DropDownListQuXian" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListQuXian_SelectedIndexChanged" >
    <asp:ListItem>请选择区县</asp:ListItem>
</asp:DropDownList>
<asp:DropDownList ID="DropDownListXiang" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListXiang_SelectedIndexChanged" >
    <asp:ListItem>请选择乡镇/街办</asp:ListItem>
</asp:DropDownList>
<asp:DropDownList ID="DropDownListCun" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListCun_SelectedIndexChanged" >
    <asp:ListItem>请选择接种点</asp:ListItem>
</asp:DropDownList>
<asp:DropDownList ID="ddlVillage" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged"  >
    <asp:ListItem>请选择接种点位管辖村</asp:ListItem>
</asp:DropDownList>
&nbsp;
<br/>
<asp:TextBox ID="txtInoculationPoint" runat="server"  style="display:none;"  EnableViewState="False" MaxLength="35" Width="200px"/>
<input type="hidden" name="txtInoculationPointID" id="txtInoculationPointID" value="" runat="server" style="display:none;"/>

<asp:TextBox ID="txtSelectArea" runat="server"  style="display:none;" EnableViewState="False" MaxLength="35" Width="200px"/>
<input type="hidden" name="txtSelAreaCode" id="txtSelAreaCode" value="" runat="server" style="display:none;"/>

<input type="hidden" name="txtSelQuCode" id="txtSelQuCode" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelXiangCode" id="txtSelXiangCode" value="" runat="server" style="display:none;"/>

<input type="hidden" name="txtSelPointCode" id="txtSelPointCode" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelVillageCode" id="txtSelVillageCode" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtAreaCode" id="txtAreaCode" value="" runat="server" style="display:none;"/>
<!------end--------->
</ContentTemplate></asp:UpdatePanel>
