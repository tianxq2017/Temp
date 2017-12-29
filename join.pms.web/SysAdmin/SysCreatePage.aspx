<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysCreatePage.aspx.cs" Inherits="join.pms.web.SysAdmin.SysCreatePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>静态页面生成</title>
<link href="/css/index.css" type="text/css" rel="stylesheet"/>
<style type="text/css">
<!--
body {
	margin-top: 10px;
	background-color: #FFFFFF;
	margin-left: 10px;
	margin-right: 10px;
	margin-bottom: 0px;
	background-image: url(/images/mainyouxiabg.jpg);
	background-position:right bottom;
	background-repeat:no-repeat;
}
-->
</style>
</head>
<body>
<form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请选择要生成的名称后，点击“生成”按钮即可完成操作。静态页面生成时需要写操作权限。</div>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" height="400" valign="top">

<!-- 编辑区 -->
<br/><br/><br/>
<table width="100%" border="0"  cellpadding="3" cellspacing="1" style="min-width: 900px;" class="admin_02a">
<tr>
  <td width="70" height="25" align="right" class="zhengwenjiacu">选择名称：</td>
  <td width="380" align="left"><asp:Literal ID="LiteralItems" runat="server"></asp:Literal></td>
  <td width="*" align="left" style="color:Red"><asp:Literal ID="LiteralInfo" runat="server" EnableViewState="false"></asp:Literal></td>
</tr>
</table>
<!-- 操作按钮 -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0" align="center" >
<tr><td align="right" style="width: 132px; height: 50px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 生成单个 ·" OnClick="btnAdd_Click" CssClass="crmbutton small save"></asp:Button>
<asp:Button ID="btnCreateAll" runat="server" Text="&middot; 生成全部 &middot;"  CssClass="crmbutton small save" OnClick="btnCreateAll_Click"></asp:Button>
</td></tr></table>

<!-- 主操作区 End-->
<!--End-->
</td></tr></table>
</td></tr></table>
<br/><br/>
</form>
</body>
</html>