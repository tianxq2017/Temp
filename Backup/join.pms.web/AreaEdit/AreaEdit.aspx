<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaEdit.aspx.cs" Inherits="join.pms.web.AreaEdit.AreaEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>区划编辑</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
   
   
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--导航信息-->

<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列信息后；点击“提交”按钮……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
 <div class="form_bg">
	  	

	    <div class="form_a">
	      <p class="form_title"><b>区划增加</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th style="height: 23px">省：</th>
    <td class="select" style="height: 21px">
     <asp:DropDownList ID="DropDownListSheng" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListSheng_SelectedIndexChanged" Width="200"></asp:DropDownList>
    </td>
    <td>
    <asp:TextBox runat="server"  ID="txtSheng" ></asp:TextBox>
    </td>    
    <td>
    <asp:CheckBox runat="server" ID="chkSheng" />是否显示
    </td>
  </tr> 
  <tr>
    <th>市：</th>
    <td class="select" style="height: 21px">
    <asp:DropDownList ID="DropDownListDiShi" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListDiShi_SelectedIndexChanged" Width="200"></asp:DropDownList>
    </td>
    <td>
    <asp:TextBox runat="server"  ID="txtShi" ></asp:TextBox>
    </td>
    <td>
    <asp:CheckBox runat="server" ID="CheckBox1" />是否显示
    </td>
  </tr>
  <tr>
    <th>区/县：</th>
    <td class="select" style="height: 21px">
    <asp:DropDownList ID="DropDownListQuXian" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListQuXian_SelectedIndexChanged" Width="200"></asp:DropDownList>
    </td>
    <td>
    <asp:TextBox runat="server"  ID="txtQu" ></asp:TextBox>
    </td>
    <td>
    <asp:CheckBox runat="server" ID="CheckBox2" />是否显示
    </td>
  </tr> 
  <tr>
    <th style="height: 21px">乡/镇：</th>
    <td class="select" style="height: 21px">
    <asp:DropDownList ID="DropDownListXiang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListXiang_SelectedIndexChanged" Width="200"></asp:DropDownList>
    </td>
    <td>
    <asp:TextBox runat="server"  ID="txtZhen" ></asp:TextBox>
    </td>
    <td>
    <asp:CheckBox runat="server" ID="CheckBox3" />是否显示
    </td>
  </tr>
    <tr id="panelMarry">
      <th>村/社区：</th>
      <td class="select" style="height: 21px">
    <asp:DropDownList ID="DropDownListCun" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListCun_SelectedIndexChanged" Width="200"></asp:DropDownList>
    </td>
    <td>
    <asp:TextBox runat="server"  ID="txtCun" ></asp:TextBox>
    </td>
    <td>
    <asp:CheckBox runat="server" ID="CheckBox4" />是否显示
    </td>
    </tr>
  
</table>
		  </div>
	    </div>
  	    
	    

	  </div>
	  
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>
