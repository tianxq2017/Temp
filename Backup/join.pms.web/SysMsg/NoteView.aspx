<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoteView.aspx.cs" Inherits="join.pms.web.SysMsg.NoteView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请审阅留言信息，根据情况输入回复内容后,点击“提交”按钮即可完成处理操作！</div>
<!-- 页面参数 -->
<!-- 主操作区 Start-->
<!--Start-->
<div id="ec_bodyZone" class="part_bg2">
<table class="lvtBg" width="100%" border="0" cellpadding="0" cellspacing="1"><tbody><tr><td>
<!-- 编辑区 -->
<!--start 主贴-------------------
<table width="560" border="0" cellspacing="0" cellpadding="0" style="border:1px #ffff00 solid;">
  <tr>
    <td height="22">主贴(作者)</td>
    <td>ip</td>
    <td width="120">发布时间</td>
  </tr>
</table>
<table width="560" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="50" height="50">&nbsp;</td>
    <td>内容</td>
  </tr>
</table>
<table width="800" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table><br/>
<table width="560" border="0" cellspacing="0" cellpadding="0">
  <tr >
    <td width="50" height="22">&nbsp;跟贴1</td><td>内容</td>
  </tr>
    <tr>
    <td width="50" height="5">&nbsp;</td><td></td>
  </tr>
  <tr>
    <td width="50" >&nbsp;跟贴2</td><td>内容</td>
  </tr>
</table>
----->
<asp:Literal ID="LiteralNotes" runat="server"></asp:Literal>
<!--end------------------------>
<br/><br/>
<asp:Panel ID="PanelRe" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr >
    <td width="80" height="30" align="center">咨询回复：</td>
    <td><asp:TextBox ID="txtReplyBody" runat="server" EnableViewState="False" MaxLength="500" Width="500px" Height="100px" Rows="10" TextMode="MultiLine"/></td>
  </tr>
</table>
<br/><br/>
</asp:Panel>
<!-- 操作按钮 -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0" align="center" >
<tr><td style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 回复 ·" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>
<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!-- 主操作区 End-->
</td></tr></tbody></table></div>
</form>
<!-- 页脚信息 -->
<!--#include virtual='/includes/copyright.inc' -->
</body>
</html>