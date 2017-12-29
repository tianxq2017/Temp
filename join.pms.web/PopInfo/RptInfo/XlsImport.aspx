<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XlsImport.aspx.cs" Inherits="AreWeb.JdcMmc.DataGather.RptInfo.XlsImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>导入选定的报表</title>
<link href="/css/right.css" type="text/css" rel="stylesheet">
<script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
function PopCalendar(obj){
    showx = event.screenX - event.offsetX - 4 - 10 ; // + deltaX;
    showy = event.screenY - event.offsetY -168; // + deltaY;
    newWINwidth = 210 + 4 + 18;
    window.showModalDialog("/includes/Calendar.htm",obj,"dialogWidth:200px; dialogHeight:210px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; ");    
}
function SetImportBut(){
    //form1.submit();
    //document.getElementById("butImport").disabled=true; 
    //return true;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server" EnableViewState="false"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一按步骤操作；数据导入时同一张报表，不允许存在重复的情况……<br/>导入数据前，请仔细检查待导入数据……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone" class="part_bg2">
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td>
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td align="left" valign="top">
<!-- 编辑区-->
<table width="880" border="0"  cellpadding="0" cellspacing="0"  class="zhengwen">
<tr class="zhengwen">
  <td width="150" height="25" align="right" class="zhengwenjiacu">1、选择文件：</td>
  <td width="*" align="left"><asp:FileUpload ID="upFiles" runat="server"  /></td>
</tr>
<tr class="zhengwen">
  <td align="right" class="zhengwenjiacu" style="height: 35px">2、上传文件：</td>
  <td width="*" align="left" style="height: 35px">
    <asp:Button ID="butUpLoad" runat="server" Text="上传..." OnClick="butUpLoad_Click" />
    <input type="hidden" name="sourceFile" id="sourceFile" value="" runat="server" style="display:none;width: 1px;height:1px;"/>
  </td>
</tr>
<tr>
    <td align="right" height="10" class="lvtCol"></td>
    <td align="left" width="*" class="zhengwen">
    <asp:Label ID="LabelMsg" runat="server" Text="如果数据量大，请耐心等待，最好以增量的方式导入数据,导入之前请检查数据准确性……"></asp:Label>
    </td>
</tr></table>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1"  class="zhengwen">
<tr class="zhengwen">
    <td width="150" align="right" height="30" class="zhengwenjiacu">3、执行导入：</td>
    <td align="left" width="*" class="small"><asp:Button ID="butImport" runat="server"  Text="导入..." OnClick="butImport_Click"   />&nbsp;    </td>
</tr>
</table>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="100%" border="0"  cellpadding="3" cellspacing="1"  class="zhengwen">
<tr class="zhengwen">
    <td width="150" align="right" height="50" class="zhengwenjiacu">导入提示：</td>
    <td align="left" width="*" class="small"><asp:Literal ID="LiteralMsg" runat="server" EnableViewState="false"></asp:Literal></td>
</tr>
</table>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<input type="button" name="ButBackPage" value="返回" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>