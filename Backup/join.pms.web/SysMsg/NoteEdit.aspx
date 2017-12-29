<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoteEdit.aspx.cs" Inherits="join.pms.web.SysMsg.NoteEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link href="/css/right.css" type="text/css" rel="stylesheet"/>
<script language="javascript" src="/ckeditor4.4.5/ckeditor.js" type="text/javascript"></script>
<script language="javascript" src="/includes/CommSys.js" type="text/javascript"></script>
</head>
<body>
<form id="form1" runat="server" enctype="multipart/form-data">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列信息后，点击“提交”按钮即可完成主贴信息操作！</div>
<!-- 页面参数 -->
<input type="hidden" name="txtUpFiles" id="txtUpFiles" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtCmsCID" id="txtCmsCID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone" class="part_bg">
<table class="lvtBg" width="100%" border="0" cellpadding="0" cellspacing="1"><tbody><tr><td style="padding: 20px 0;">
<!-- 编辑区 -->
<table width="800" border="0"  cellpadding="5" cellspacing="0" >
<tr>
  <td width="120" height="25" align="right" >标题：</td>
  <td width="*" align="left"><asp:TextBox ID="txtMsgTitle" runat="server" EnableViewState="False" MaxLength="50" Width="500px"/><span style="color: #ff1400">*</span></td>
</tr>
<tr>
  <td width="120" height="25" align="right" >内容：</td>
  <td width="*" align="left"><asp:TextBox id="objCKeditor" CssClass="ckeditor" TextMode="MultiLine"  runat="server" Height="320" Width="500" /></td>
</tr>
<tr>
  <td width="120" height="25" align="right" >发布人：</td>
  <td width="*" align="left"><asp:TextBox ID="txtMsgUserName" runat="server" EnableViewState="False" MaxLength="10" Width="200px"/><span style="color: #ff1400">*</span></td>
</tr>
<tr>
  <td width="120" height="25">&nbsp;</td>
  <td width="*" align="left"><asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" OnClick="btnAdd_Click" CssClass="crmbutton small save"></asp:Button>
<input name="btnCancel" type="reset" id="btnCancel" value="· 重填 ·" class="crmbutton small cancel"/>
<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="crmbutton small save" /></td>
</tr>
</table>
<!-- 主操作区 End-->
</td></tr></tbody></table>
</div>
</form>
<!-- 页脚信息 -->
<br/><br/>
</body>
</html>