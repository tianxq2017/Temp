<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata030305.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.chatdata030305" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>辅助决策基础数据填报</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/index.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
    <script language="javascript" src="/Scripts/datectrl.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列信息后；点击“提交”按钮（次月5日以前将上月情况提交）……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">

<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<br/>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr >
  <td align="right" height="25" class="zhengwenjiacu" width="120" bgcolor="#FFFFCC">数据日期：</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
      (填报时间,该数据实际的归属月份,选择到自然月之内即可)
    </td>
</tr><tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu"  bgcolor="#FFFFCC">数据单位：</td>
  <td align="left">
      <uc1:ucDataAreaSel ID="UcDataAreaSel1" runat="server" />
  </td>
</tr></table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0" cellspacing="3" cellpadding="1" class="zhengwen">
  <tr >
    <td align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">年度：</td>
    <td><asp:DropDownList ID="ddlFileds01" runat="server">
            <asp:ListItem Value="2008"></asp:ListItem>
            <asp:ListItem Value="2009"></asp:ListItem>
            <asp:ListItem Value="2010"></asp:ListItem>
            <asp:ListItem Value="2011"></asp:ListItem>
            <asp:ListItem Value="2012"></asp:ListItem>
            <asp:ListItem Value="2013"></asp:ListItem>
            <asp:ListItem Value="2014"></asp:ListItem>
            <asp:ListItem Value="2015"></asp:ListItem>
            <asp:ListItem Value="2016"></asp:ListItem>
            <asp:ListItem Value="2017"></asp:ListItem>
            <asp:ListItem Value="2018"></asp:ListItem>
        </asp:DropDownList>年</td>
  </tr>  <tr >
    <td align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">季度：</td>
    <td>
        <asp:DropDownList ID="ddlFileds02" runat="server">
            <asp:ListItem>一季度三查</asp:ListItem>
            <asp:ListItem>二季度三查</asp:ListItem>
            <asp:ListItem>三季度三查</asp:ListItem>
            <asp:ListItem>四季度三查</asp:ListItem>
        </asp:DropDownList></td>
  </tr> 
  <tr >
    <td width="120" align="right" bgcolor="#FFFFCC">应服务数：</td>
    <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/>*</td>
  </tr>
  <tr >
    <td width="120" align="right" bgcolor="#FFFFCC">已服务数：</td>
    <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/>*</td>
  </tr>
  <!--
  <tr >
    <td width="120" align="right" bgcolor="#FFFFCC">填报人：</td>
    <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr >
    <td width="120" align="right" bgcolor="#FFFFCC">负责人：</td>
    <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>-->
  </table>

<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- 数据年度,季度,应服务数,已服务数,服务率% -->
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