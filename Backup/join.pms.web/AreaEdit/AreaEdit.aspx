<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaEdit.aspx.cs" Inherits="join.pms.web.AreaEdit.AreaEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>�����༭</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
   
   
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--������Ϣ-->

<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ������һ��д������Ϣ�󣻵�����ύ����ť����</div>
<!-- ҳ����� -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- �༭��  -->
 <div class="form_bg">
	  	

	    <div class="form_a">
	      <p class="form_title"><b>��������</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th style="height: 23px">ʡ��</th>
    <td class="select" style="height: 21px">
     <asp:DropDownList ID="DropDownListSheng" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListSheng_SelectedIndexChanged" Width="200"></asp:DropDownList>
    </td>
    <td>
    <asp:TextBox runat="server"  ID="txtSheng" ></asp:TextBox>
    </td>    
    <td>
    <asp:CheckBox runat="server" ID="chkSheng" />�Ƿ���ʾ
    </td>
  </tr> 
  <tr>
    <th>�У�</th>
    <td class="select" style="height: 21px">
    <asp:DropDownList ID="DropDownListDiShi" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListDiShi_SelectedIndexChanged" Width="200"></asp:DropDownList>
    </td>
    <td>
    <asp:TextBox runat="server"  ID="txtShi" ></asp:TextBox>
    </td>
    <td>
    <asp:CheckBox runat="server" ID="CheckBox1" />�Ƿ���ʾ
    </td>
  </tr>
  <tr>
    <th>��/�أ�</th>
    <td class="select" style="height: 21px">
    <asp:DropDownList ID="DropDownListQuXian" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListQuXian_SelectedIndexChanged" Width="200"></asp:DropDownList>
    </td>
    <td>
    <asp:TextBox runat="server"  ID="txtQu" ></asp:TextBox>
    </td>
    <td>
    <asp:CheckBox runat="server" ID="CheckBox2" />�Ƿ���ʾ
    </td>
  </tr> 
  <tr>
    <th style="height: 21px">��/��</th>
    <td class="select" style="height: 21px">
    <asp:DropDownList ID="DropDownListXiang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListXiang_SelectedIndexChanged" Width="200"></asp:DropDownList>
    </td>
    <td>
    <asp:TextBox runat="server"  ID="txtZhen" ></asp:TextBox>
    </td>
    <td>
    <asp:CheckBox runat="server" ID="CheckBox3" />�Ƿ���ʾ
    </td>
  </tr>
    <tr id="panelMarry">
      <th>��/������</th>
      <td class="select" style="height: 21px">
    <asp:DropDownList ID="DropDownListCun" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListCun_SelectedIndexChanged" Width="200"></asp:DropDownList>
    </td>
    <td>
    <asp:TextBox runat="server"  ID="txtCun" ></asp:TextBox>
    </td>
    <td>
    <asp:CheckBox runat="server" ID="CheckBox4" />�Ƿ���ʾ
    </td>
    </tr>
  
</table>
		  </div>
	    </div>
  	    
	    

	  </div>
	  
<!-- ������ť m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="�� �ύ ��" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>
