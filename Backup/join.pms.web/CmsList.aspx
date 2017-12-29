<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CmsList.aspx.cs" Inherits="join.pms.web.CmsList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>CMSlist</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<script language="javascript" src="includes/commSys.js" type="text/javascript"></script>
<script language="javascript" src="includes/CommBiz.js" type="text/javascript"></script>
<script language="javascript" src="includes/dataGrid.js" type="text/javascript"></script>
<script language="javascript" src="/includes/Calendar.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
function PopCalendar(obj){
    showx = event.screenX - event.offsetX - 4 - 10 ; // + deltaX;
    showy = event.screenY - event.offsetY -168; // + deltaY;
    newWINwidth = 210 + 4 + 18;
    window.showModalDialog("/includes/Calendar.htm",obj,"dialogWidth:200px; dialogHeight:210px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; ");    
}
</script>
<link href="images/css.css" rel="stylesheet" type="text/css" />
<link href="includes/style.css" rel="stylesheet" type="text/css" />
</head>
<body style="background:url(images/tu03.jpg) bottom left no-repeat #FFFFFF; padding:16px;">
<form id="form1" runat="server">
<!-- 页面参数 -->
<input type="hidden" name="txtUrlParams" id="txtUrlParams" value="" runat="server" style="display:none;"/>
<input type="hidden" name="tbFuncNo" id="tbFuncNo" value="" runat="server" style="display:none;"/>
<input type="hidden" name="selectRowPara" id="selectRowPara" value="" style="display:none;"/>
<input type="hidden" name="selectRowID" id="selectRowID" value=""style="display:none;" />
<table width="100%" height="160" border="0" cellpadding="0" cellspacing="0"><tr>
<td width="98%" valign="top" bgcolor="#FFFFFF">
<!--nav info-->
<asp:Literal ID="LiteralNav" runat="server" EnableViewState="false"></asp:Literal>
<!--page & data info-->
<asp:Literal ID="LiteralDataList" runat="server" EnableViewState="false"></asp:Literal>
</td></tr></table>
</form>
</body>
</html>