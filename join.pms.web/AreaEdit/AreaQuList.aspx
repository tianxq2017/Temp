<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaQuList.aspx.cs" Inherits="join.pms.web.AreaEdit.AreaQuList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>区划管理</title>
<link href="/css/right.css" rel="stylesheet" type="text/css" />
</head>

<body>
<form id="form1" runat="server">
<div class="mbx">当前位置：区划管理</div>
<div id="ec_bodyZone" class="part_bg2">
	<div class="form_bg">
		<div class="form_a">
			<p class="form_title"><b>区划管理</b></p>
			<div class="form_table">
				<fieldset style="width:850px">
				<legend>区划查询</legend>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th width="120">省：</th>
    <td width="250" class="select"><asp:DropDownList ID="DropDownListSheng" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListSheng_SelectedIndexChanged" Width="200"></asp:DropDownList></td>
    <th width="120">市：</th>
    <td class="select"><asp:DropDownList ID="DropDownListDiShi" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListDiShi_SelectedIndexChanged" Width="200"></asp:DropDownList></td>
  </tr>
  <tr>
    <th>区/县：</th>
    <td class="select"><asp:DropDownList ID="DropDownListQuXian" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListQuXian_SelectedIndexChanged" Width="200"></asp:DropDownList></td>
    <th>乡/镇：</th>
    <td class="select"><asp:DropDownList ID="DropDownListXiang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListXiang_SelectedIndexChanged" Width="200"></asp:DropDownList></td>
  </tr>
    <tr style=" display:none">
      <th>村/社区：</th>
      <td class="select">
    <asp:DropDownList ID="DropDownListCun" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListCun_SelectedIndexChanged" Width="200"></asp:DropDownList>    </td> 
    <th>ddd</th>
    <td>ddd</td>
    </tr> 
  <tr>
    <td colspan="4" align="center"><asp:Button runat="server" ID="btnSearch" Text=" 查 询 " OnClick="btnSearch_Click" CssClass="submit6" /></td>
  </tr>
</table>
				</fieldset>
				<fieldset style="margin:20px 10px 10px 0; width:850px">
				<legend>新增区划</legend>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr id="panelMarry">
      <th width="120">名称：</th>
      <td width="250" class="text"><p><span><asp:TextBox ID="txtName" runat="server"></asp:TextBox></span></p></td>
      <th width="120">编码：</th>
      <td class="text"><p><span><asp:TextBox ID="txtCode" runat="server" MaxLength="12"></asp:TextBox></span></p> <asp:Button ID="btnAdd" runat="server" Text="新增" OnClick="btnAdd_Click" CssClass="submit6" /></td>
    </tr>
    <tr>
      <th>提示：</th>
      <td colspan="3">村级后三位不能为0，镇级后六位不能为0，县级后九位不能为0</td>
    </tr>
</table>
				</fieldset>
				
				
				

			</div>
		</div>
	</div>
	<div class="clr20"></div>
	
	<div class="form_bg">
		<div class="form_a">
			<p class="form_title"><b>查询结果</b></p>
			<div class="form_table">
<asp:GridView ID="gv" CssClass="table_public_03" runat="server"  BorderWidth="1" BorderColor="Blue" AutoGenerateColumns="false" DataKeyNames="id" OnRowEditing="gv_RowEditing" OnRowDeleting="gv_RowDeleting"  OnRowUpdating="gv_RowUpdating" OnRowCancelingEdit="gv_RowCancelingEdit" Height="139px" Width="943px" >
        <Columns>
        <asp:TemplateField HeaderText="名称">
        <ItemTemplate>
        <%#Eval("AreaName")%>
        </ItemTemplate>        
        <EditItemTemplate>
        <asp:TextBox ID="txtName" runat="server" Text='<%#Eval("AreaName") %>'></asp:TextBox>
        </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="编码">
        <ItemTemplate>
        <%#Eval("AreaCode")%>
        </ItemTemplate>        
        <EditItemTemplate>
        <asp:TextBox ID="txtCode" runat="server" Text='<%#Eval("AreaCode") %>'></asp:TextBox>
        </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="全名称">
        <ItemTemplate>
        <%#Eval("AreaNameFull")%>
        </ItemTemplate>        
        <EditItemTemplate>
        <asp:TextBox ID="txtFullName" runat="server" Text='<%#Eval("AreaNameFull") %>'></asp:TextBox>
        </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="全编码" Visible=false>
        <ItemTemplate>
        <%#Eval("AreaCodeFull")%>
        </ItemTemplate>        
        <EditItemTemplate>
        <asp:TextBox ID="txtFullCode" runat="server" Text='<%#Eval("AreaCodeFull") %>' Visible=false></asp:TextBox>
        </EditItemTemplate>
        </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="False" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    <div style="text-align:center; padding:10px; width:900px;"><asp:Button ID="btnSave" Text="　保 存　" runat="server" OnClick="btnSave_Click" /></div>
			</div>
		</div>
	</div>
</div>


</form>
</body>
</html>
