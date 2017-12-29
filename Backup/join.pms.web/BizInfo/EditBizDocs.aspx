<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditBizDocs.aspx.cs" Inherits="join.pms.web.BizInfo.EditBizDocs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>上传证件</title>
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" src="/Scripts/CommUpload.js" type="text/javascript"></script>
    <script language="javascript" src="/Scripts/checkDocs.js" type="text/javascript"></script>
</head>
<body>
   <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列信息后；点击“提交”按钮……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtAreaCode" id="txtAreaCode" runat="server" style="display:none;"/>
<input type="hidden" name="txtRegAreaCodeA" id="txtRegAreaCodeA" runat="server" style="display:none;"/>
<input type="hidden" name="txtRegAreaCodeB" id="txtRegAreaCodeB" runat="server" style="display:none;"/>
<input type="hidden" name="txtIsInnerArea" id="txtIsInnerArea" runat="server" style="display:none;"/>
<input type="hidden" name="txtBizCNum" id="txtBizCNum" runat="server" style="display:none;"/>
<input type="hidden" name="txtBizGNum" id="txtBizGNum" runat="server" style="display:none;"/>

<input type="hidden" name="txtAttribs" id="txtAttribs" value="" runat="server" style="display:none;"/>

<input type="hidden" name="txtBizDocsIDiOld" id="txtBizDocsIDiOld" runat="server" style="display:none;"/>
<input type="hidden" name="txtBizDocsIDOld" id="txtBizDocsIDOld" runat="server" style="display:none;"/>


<input type="hidden" name="txtBizDocsIDIsOld" id="txtBizDocsIDIsOld" runat="server" style="display:none;"/>

<input type="hidden" name="txtPersonID" id="txtPersonID" runat="server" style="display:none;"/>

<input type="hidden" name="txtAction" id="txtAction" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<div class="column_00"> 
  <div class="column_c">
	<div class="apply_form">
	  <p class="column_title_b">提交所需证件电子版</p>
	  <div class="form_bg">
	    <div class="form_a form_b">
	      <p class="form_title"><b>友情提示：</b><span>所须证件资料包括以下原件及原件扫描的电子件,原件办理时须现场提交,原件扫描的电子件可申请时通过网络上传(可减少您的办理时间),也可提交原件时由计生部门代为上传。（允许的图片格式：jpg、gif、bmp、png）。</span></p>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <asp:Literal ID="LiteralBizCategoryLicense" runat="server" EnableViewState="false"></asp:Literal>
</table>
	    </div>
	  </div>	  
	  </div>
  </div>
</div>

<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 50px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·"   OnClientClick="return EditCheck();" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>
<link rel="stylesheet" href="/scripts/lightbox/lightbox.css" type="text/css" media="screen" />
<script src="/scripts/lightbox/jquery-1.8.3.min.js" type="text/javascript"></script>
<script src="/scripts/lightbox/lightbox.js" type="text/javascript"></script>
