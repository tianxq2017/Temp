﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditRsYlbx.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.EditRsYlbx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>人社局-农村社会养老保险记录表</title>
    <link href="/Styles/index.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="/includes/CommSys.js" type="text/javascript"></script>
    <script language="javascript" src="/Scripts/datectrl.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列农村社会养老保险记录表后；点击“提交”按钮（次月5日以前将上月情况提交）……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="100"  bgcolor="#CCFFFF">数据日期：</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
    (填报时间,该数据实际的归属月份,请选择到该数据月之内)
    </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="100"  bgcolor="#CCFFFF">
      单位：</td>
  <td align="left"><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="100"  bgcolor="#CCFFFF">
      户名：</td>
  <td align="left"><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" Width="200px"/></td>
</tr>
<tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
        手册编号：</td>
        <td align="left">
    <asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" Width="200px"/></td>
    </tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu"  bgcolor="#CCFFFF">
      存入日期：</td>
  <td align="left">
            <asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" Width="200px"/></td>
</tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            个人缴纳：</td>
        <td align="left">
            <asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" Width="200px"/></td>
    </tr>
    
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            集体补贴：</td>
        <td align="left">
            <asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" Width="200px"/></td>
    </tr>
    <tr class="zhengwen">
    <td width="100" height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">
        存入合计：</td>
    <td align="left" colspan="3"><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">
        退付时间：</td>
    <td align="left" colspan="3" ><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" Width="200px"/></td>
</tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            退付金额：</td>
        <td align="left" colspan="3">
        <asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" Width="200px"/></td>
    </tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            领取时间：</td>
        <td align="left" colspan="3">
        <asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" Width="200px"/></td>
    </tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            领取金额：</td>
        <td align="left" colspan="3">
        <asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" Width="200px"/></td>
    </tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            户口所在地：</td>
        <td align="left" colspan="3">
        <asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" Width="200px"/></td>
    </tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            角码：</td>
        <td align="left" colspan="3">
        <asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" Width="200px"/></td>
    </tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            备注：</td>
        <td align="left" colspan="3">
        <asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" Width="200px"/></td>
    </tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>
