<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysParaEdit.aspx.cs" Inherits="join.pms.web.SysAdmin.SysParaEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>系统参数管理</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="../includes/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
<!-- 页面导航开始 -->
<table width="100%" border="0" cellpadding="0" cellspacing="0" align="center" class="level2Bg">
<tr><td width="3" style="height: 30px"></td><td align="left"  style="padding-left: 10px; padding-right: 50px;" class="moduleName" nowrap="nowrap"> 
您的位置:<asp:Literal ID="LiteralNav" runat="server"></asp:Literal>
</td><td width="3">&nbsp;</td></tr> </table>
<!-- 页面参数 -->
<!-- 主操作区 Start-->
<table width="98%" align="center" border="0" cellpadding="0" cellspacing="0"><tbody>
<tr><td class="small" colspan="3" height="30">提示：</td></tr>
<tr><td colspan="3"></td></tr>
<tr><td valign="top"><img src="/images/showPanelTopLeft.gif"></td><td width="100%" valign="top" class="showPanelBg" style="padding: 10px;">
<div id="Div1" class="small" style="width: 100%; position: relative;">
<table class="lvtBg" width="100%" border="0" cellpadding="0" cellspacing="1"><tbody><tr><td><br/>
<!-- 编辑区 -->
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" class="lvtCol">
<tr>
  <td width="120" height="25" align="right" class="lvtCol">参数值：</td>
  <td width="*" align="left"><asp:TextBox ID="txtParaValue" runat="server" EnableViewState="False" MaxLength="50" Width="411px"/>
      <asp:Label ID="labClassCode" runat="server" Visible="False" Width="89px"></asp:Label>
      <asp:Label ID="labURL" runat="server" Visible="False" Width="89px"></asp:Label>
      <asp:Label ID="labActionName" runat="server" Visible="False" Width="89px"></asp:Label></td>
</tr>
<tr>
  <td height="25" align="right" class="lvtCol">参数说明：</td>
  <td width="*" align="left"><asp:TextBox ID="txtParaSM" runat="server" EnableViewState="False" MaxLength="50" Width="411px" Height="26px" TextMode="MultiLine"/></td>
</tr>
<tr>
    <td align="right" height="25" class="lvtCol">参数编码：</td>
    <td align="left" width="*"><asp:TextBox ID="txtCode" runat="server" EnableViewState="False" MaxLength="50" Width="103px"/></td>
</tr>
<tr>
    <td align="right" height="25" class="lvtCol">参数分类：</td>
    <td align="left" width="*"><asp:TextBox ID="txtCate" runat="server" EnableViewState="False" MaxLength="50" Width="45px"/></td>
</tr>
</table>
<!-- 操作按钮 -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0" align="center" class="lvtCol">
<tr><td align="right" style="width: 132px; height: 50px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" OnClick="btnAdd_Click" CssClass="crmbutton small save"></asp:Button>
<input name="btnCancel" type="reset" id="btnCancel" value="· 重填 ·" class="crmbutton small cancel"/>
<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:history.go(-1);" class="crmbutton small save" />
</td></tr></table>
<!-- 主操作区 End-->
</td></tr></tbody></table></div></td><td valign="top"><img src="/images/showPanelTopRight.gif"></td></tr></tbody></table>
</form>
<!-- 页脚信息 -->
<!--#include virtual='/includes/copyright.inc' -->
</body>
</html>