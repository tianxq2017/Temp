<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysSeal.aspx.cs" Inherits="join.pms.web.SysAdmin.SysSeal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link href="/css/right.css" type="text/css" rel="stylesheet"/>
<script language="javascript" src="/includes/commOA.js" type="text/javascript"></script>
<style type="text/css">
<!--
.butStyle {line-height: 20px; color: #000000; text-decoration: none; font-size: 12px;}
-->
</style>
</head>
<body>
<form id="form1" runat="server">
<input id="txtUserPwd" runat="server" type="hidden" />
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：<asp:Literal ID="LiteralInfo" runat="server"></asp:Literal></div>

<!--Start-->
<div id="ec_bodyZone" class="part_bg2">

<!-- 编辑区 -->
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" class="ihx_table">
<tr>
  <th>公章名称：</th>
  <td class="txt1"><asp:TextBox ID="txtSealName" runat="server"  MaxLength="25"  Width="160px" />*（可以为数字、字母或汉字，不能超过10个汉字）</td>
</tr>
<tr>
  <th>保存路径：</th>
  <td class="txt1"><asp:TextBox ID="txtSealPath" runat="server"  Text="/images/sign/" MaxLength="50"  Width="160px"/>*(请填写公章所在路径)</td>
</tr>
<tr>
  <th>签章密码：</th>
  <td class="txt1"><asp:TextBox ID="txtSealPass" runat="server"   MaxLength="20"  Width="160px" TextMode="Password"/>*</td>
</tr>
<tr>
  <th>分配对象：</th>
  <td class="txt1"><input id="txtUserID" name="txtUserID" type="hidden" runat="server" />
          <input id="txtUserName" name="txtUserName"  Check=1 Show="" runat="server"/>
          <input type="button"  value="选择.." onclick="SelectUsers('txtUserID','txtUserName')" id="Button1" class="btnSubmit"/>
    *</td>
</tr>
</table>
<div class="ihx_table_xt"></div>

<!-- 操作按钮 -->
<table border="0" cellpadding="0" cellspacing="0" class="ihx_table">
  <tr>
    <th>&nbsp;</th>
    <td><asp:Button ID="btnAdd" runat="server" Text=" 提交 " OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>
<input type="button" name="ButBackPage" value=" 返回 " id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" /></td>
  </tr>
</table>
<!--End-->
</div>
</form>
</body>
</html>