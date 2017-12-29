<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataImport.aspx.cs" Inherits="join.pms.web.SysAdmin.DataImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>无标题页</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="../includes/style.css" rel="stylesheet" type="text/css" />
<script language="javascript" src="/includes/Calendar.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
function PopCalendar(obj){
    showx = event.screenX - event.offsetX - 4 - 10 ; // + deltaX;
    showy = event.screenY - event.offsetY -168; // + deltaY;
    newWINwidth = 210 + 4 + 18;
    window.showModalDialog("/includes/Calendar.htm",obj,"dialogWidth:200px; dialogHeight:210px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; ");    
}
</script>
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
<tr><td class="small" colspan="3" height="30">提示：请选择您想要导入的Excel数据文件上传后,点击“导入”按钮执行导入。根据数据文件大小及网络情况，操作可能需要一段时间，请您耐心等待……</td></tr>
<tr><td colspan="3"></td></tr>
<tr><td valign="top"><img src="/images/showPanelTopLeft.gif" /></td><td width="100%" valign="top" class="showPanelBg" style="padding: 10px;">
<div id="Div1" class="small" style="width: 100%; position: relative;">
<table class="lvtBg" width="100%" border="0" cellpadding="0" cellspacing="1"><tbody><tr><td><br/>
<!-- 编辑区 -->
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" class="lvtCol" style="font-weight: bold">
<tr>
  <td width="120" height="25" align="right" class="lvtCol">1、选择文件：</td>
  <td width="*" align="left"><asp:FileUpload ID="upFiles" runat="server"  /></td>
</tr>
<tr>
  <td align="right" class="lvtCol" style="height: 35px">2、上传文件：</td>
  <td width="*" align="left" style="height: 35px"><asp:Button ID="butUpLoad" runat="server" Text="上传..." OnClick="butUpLoad_Click" />
                      <input type="hidden" name="sourceFile" id="sourceFile" value="" runat="server" style="display:none;width: 1px;height:1px;"/>                      </td>
</tr>
<tr>
    <td align="right" height="10" class="lvtCol"></td>
    <td align="left" width="*" class="small">&nbsp;</td>
</tr>
<tr>
    <td align="right" height="50" class="lvtCol">4、执行导入：</td>
    <td align="left" width="*" class="small"><asp:Button ID="butImport" runat="server"  Text="导入..." OnClick="butImport_Click"  />&nbsp;    </td>
</tr>
</table>
<!-- 操作按钮 -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0" align="center" class="lvtCol" style="font-weight: bold">
<tr><td align="right" style="width: 132px; height: 50px;">&nbsp;</td><td width="*" align="left" class="small">
<asp:Label ID="LabelMsg" runat="server" Text="如果数据量大，请耐心等待，建议以增量的方式导入数据,操作时间应尽量避免网络访问高峰时段！"></asp:Label>
</td></tr></table>
<!-- 主操作区 End-->
</td></tr></tbody></table></div></td><td valign="top"><img src="/images/showPanelTopRight.gif"></td></tr></tbody></table>
</form>
<!-- 页脚信息 -->
<!--#include virtual='/includes/copyright.inc' -->
</body>
</html>