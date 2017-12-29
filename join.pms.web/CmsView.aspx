<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CmsView.aspx.cs" Inherits="join.pms.web.CmsView" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>CmsView</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<link href="/css/right.css" type="text/css" rel="stylesheet">
<script language="javascript" type="text/javascript">
function fontZoom(size)
{
   document.getElementById('fontzoom').style.fontSize=size+'px'
   document.getElementById('fontzoom').style.lineHeight=size+6+'px'
}
</script>
</head>
<body >
<form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server" EnableViewState="false"></asp:Literal></div>
<!--正文信息-->
<div id="ec_bodyZone" class="part_bg2">
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td>
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td valign="top" class="right_02">
<asp:Literal ID="LiteralData" runat="server" EnableViewState="false"></asp:Literal>
</td></tr></table>
</td></tr></table>
</div>
</form>
</body>
</html>
