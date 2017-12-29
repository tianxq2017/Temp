<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xlsImport.aspx.cs" Inherits="AreWeb.OnlineCertificate.PopInfo.xlsImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>无标题页</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/right.css" type="text/css" rel="stylesheet">
<script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
<script language="javascript" src="/Scripts/datectrl.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server" EnableViewState="false"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一按步骤操作；数据导入时相同数据日期的同一类数据，不允许存在重复的情况。(全员库查询数据导入无需选择数据日期和数据单位)……<br/>导入数据默认为审核，所以请导入先仔细检查待导入数据……</div>
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
    <asp:Label ID="LabelMsg" runat="server" Text="如果数据量大，请耐心等待，数据必须以增量的方式导入数据,导入之前请检查是否存在重复数据(存在重复数据请清理后再导入),操作时间应尽量避免网络访问高峰时段！"></asp:Label>
    </td>
</tr></table>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1"  class="zhengwen">
<%if (m_FuncCode.Substring(0, 2) != "05")  {%>
<tr class="zhengwen">
  <td width="150" height="30" align="right" class="zhengwenjiacu">3、数据单位：</td>
  <td width="*" align="left">
      <asp:DropDownList ID="DDLReportArea" runat="server"></asp:DropDownList>(区分数据归属区划单位,以有效识别数据归属区划)
      </td>
</tr>
<%} %>
<tr class="zhengwen">
  <td width="150" align="right" height="25" class="zhengwenjiacu">4、数据日期：</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
    (该数据实际的填报日期,请选择到该数据归属月份之内任意一天即可)
    </td>
</tr>
<tr class="zhengwen">
    <td width="150" align="right" height="30" class="zhengwenjiacu">5、执行导入：</td>
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