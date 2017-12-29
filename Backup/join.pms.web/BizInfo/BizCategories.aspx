<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizCategories.aspx.cs" Inherits="join.pms.web.BizInfo.BizCategories" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>业务介绍</title>
<link href="/css/right.css" type="text/css" rel="stylesheet"/>
<script language="javascript" src="/ckeditor4.4.5/ckeditor.js" type="text/javascript"></script>
<script language="javascript" src="/includes/CommSys.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server" EnableViewState="false" /></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列信息后，点击“提交”按钮即可完成业务介绍信息的操作！</div>
<!-- 页面参数 -->
<input type="hidden" name="txtUpFiles" id="txtUpFiles" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtCmsCID" id="txtCmsCID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone" class="part_bg">
<table class="lvtBg" width="100%" border="0" cellpadding="0" cellspacing="1"><tbody><tr><td style="padding: 20px 0;">
<!-- 编辑区 -->
<table width="800" border="0"  cellpadding="5" cellspacing="0" >
<tr>
  <td width="120" height="25" align="right" >业务事项：</td>
  <td width="*" align="left"><asp:Literal ID="LiteralBiz" runat="server" EnableViewState="false" /></td>
</tr>
<tr>
  <td width="120" height="25" align="right" >&nbsp;</td>
  <td width="*" align="left">&nbsp;</td>
</tr>
<tr>
  <td width="120"  align="right" >业务介绍：</td>
  <td width="*" align="left"><asp:TextBox id="objCKeditor" CssClass="ckeditor" TextMode="MultiLine"  runat="server" Height="360" Width="500" /></td>
</tr>
<tr >
    <td>&nbsp;</td>
    <td>&nbsp;</td>
</tr>
</table>
<!-- 主操作区 End-->
<br />
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width:120px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 30px">
<asp:Button ID="btnAdd" runat="server" Text="提交" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>
<input type="button" name="ButBackPage" value="返回" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
</td></tr></tbody></table>
</div>
</form>
<!-- 页脚信息 -->
</body>
</html>
