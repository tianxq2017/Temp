<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDataAreaSelVillage.ascx.cs" Inherits="join.pms.web.userctrl.ucDataAreaSelVillage" %>
<!-- ScriptManager�ؼ�����ҳ����� -->
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<!------start--------->
<asp:DropDownList ID="DropDownListSheng" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListSheng_SelectedIndexChanged" Visible="false" >
<asp:ListItem>��ѡ��ʡ</asp:ListItem></asp:DropDownList>
<asp:DropDownList ID="DropDownListDiShi" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListDiShi_SelectedIndexChanged" >
    <asp:ListItem>��ѡ�����</asp:ListItem>
</asp:DropDownList>
<asp:DropDownList ID="DropDownListQuXian" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListQuXian_SelectedIndexChanged" >
    <asp:ListItem>��ѡ������</asp:ListItem>
</asp:DropDownList>
<asp:DropDownList ID="DropDownListXiang" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListXiang_SelectedIndexChanged" >
    <asp:ListItem>��ѡ������/�ְ�</asp:ListItem>
</asp:DropDownList>
<asp:DropDownList ID="DropDownListCun" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="DropDownListCun_SelectedIndexChanged" >
    <asp:ListItem>��ѡ����ֵ�</asp:ListItem>
</asp:DropDownList>
<asp:DropDownList ID="ddlVillage" runat="server" AutoPostBack="True" CssClass="font" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged"  >
    <asp:ListItem>��ѡ����ֵ�λ��Ͻ��</asp:ListItem>
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
