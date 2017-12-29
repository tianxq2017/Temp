<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xlsExport.aspx.cs" Inherits="join.pms.web.SysAdmin.xlsExport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>日志导出</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
    <style type="text/css">
    <!--
    body {
	    margin-top: 10px;
	    background-color: #FFFFFF;
	    margin-left: 10px;
	    margin-right: 10px;
	    margin-bottom: 0px;
	    background-image: url(/images/mainyouxiabg.jpg);
	    background-position:right bottom;
	    background-repeat:no-repeat;
    }
    .butStyle {line-height: 20px; color: #000000; text-decoration: none; font-size: 12px;}
    -->
    </style>
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
<!--导航信息-->
<div class="mbx">当前位置：起始页 &gt;&gt; 操作日志导出/清理</div>
<!--提示信息-->
<div class="tsxx">提示信息：请选择日期范围开始导出；导出后的数据将自动清理删除并备份在您的网盘中</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区--><br/>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1" class="zhengwen">
<tr >
  <td width="200" align="right" height="30" class="zhengwenjiacu">选择数据日期范围：</td>
  <td align="left">从:
    <input id="txtStartDate" readonly="readonly" size="12" onclick="PopCalendar(txtStartDate);return false;"  runat="server" />
    <input id="Image1" onclick="PopCalendar(txtStartDate);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />(起始日期)
    &nbsp;&nbsp;到:
    <input id="txtEndDate" readonly="readonly" size="12" onclick="PopCalendar(txtEndDate);return false;"  runat="server" />
    <input id="Image2" onclick="PopCalendar(txtEndDate);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />(截止日期) 
    </td>
</tr>
<tr class="zhengwen">
  <td width="120" height="25" align="right" class="zhengwenjiacu">生成的excel文件：</td>
  <td width="*" align="left"><asp:Literal ID="LiteralFiles" runat="server"></asp:Literal></td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 导出 ·" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>
<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>