<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xlsExport.aspx.cs" Inherits="AreWeb.OnlineCertificate.PopInfo.xlsExport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>xls格式数据导出</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/right.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
    <script language="javascript" src="/Scripts/datectrl.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请选择部门、日期范围和条件开始导出……；导出后的数据将自动保存在您的网盘中</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone" class="part_bg2">
<table width="100%" border="0" cellpadding="0" cellspacing="1"><tr><td>
<table width="100%" border="0" cellpadding="8" cellspacing="5"><tr><td align="left">
<!-- 编辑区--><br/>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1" class="zhengwen">
<tr class="zhengwen">
  <td width="200" height="30" align="right" class="zhengwenjiacu">请选择要导出的部门：</td>
  <td width="*" align="left">
      <asp:DropDownList ID="DDLReportArea" runat="server"></asp:DropDownList>(默认为全部)
      </td>
</tr>
<tr >
  <td align="right" height="30" class="zhengwenjiacu">选择数据日期范围：</td>
  <td align="left">从:
    <input id="txtStartDate" readonly="readonly" size="12" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:false})" runat="server" />(起始日期)
    &nbsp;&nbsp;到:
    <input id="txtEndDate" readonly="readonly" size="12" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:false})" runat="server" />(截止日期) 
    </td>
</tr>
<%if (m_FuncCode.Substring(0, 2) != "05" && m_FuncCode!="060101")
  {%>
<tr class="zhengwen">
  <td height="30" align="right" class="zhengwenjiacu">选择要导出的数据属性：</td>
  <td width="*" align="left">
      <asp:RadioButton ID="rb1" runat="server" Text="全部数据" GroupName="ExpAttribs" Checked="true" />
      <asp:RadioButton ID="rb2" runat="server" Text="未公开的数据" GroupName="ExpAttribs" />
      <asp:RadioButton ID="rb3" runat="server" Text="审核的数据(包括公开的)" GroupName="ExpAttribs" />
      <asp:RadioButton ID="rb4" runat="server" Text="公开的数据" GroupName="ExpAttribs" />
      <asp:RadioButton ID="rb5" runat="server" Text="差异数据" GroupName="ExpAttribs" />
      </td>
</tr>
<%} %>
<tr class="zhengwen">
  <td height="25" align="right" class="zhengwenjiacu">生成的excel文件：</td>
  <td width="*" align="left"><asp:Literal ID="LiteralFiles" runat="server"></asp:Literal></td>
</tr>
</table>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="导出" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>
<input type="button" name="ButBackPage" value="返回" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>