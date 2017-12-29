<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata030117.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.chatdata030117" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>年度陕西省计划生育家庭情况统计表(农村)</title>
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
<table width="980" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="980" border="0"  cellpadding="3" cellspacing="1">
<tr >
  <td align="right" width="80" height="25" class="zhengwenjiacu">填报日期：</td>
  <td align="left" width="124"><input id="txtReportDate" readonly="readonly" size="7" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
    </td>
    <td align="right" width="80" height="25" class="zhengwenjiacu">填表单位：</td>
  <td align="left" width="124">
      <asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="80px" Text="宜川县"/>
  </td>
    <td align="right" width="80" height="25" class="zhengwenjiacu">年度：</td>
  <td align="left" width="124">
      <asp:DropDownList ID="dd_Year" runat="server">
        <asp:ListItem>2015</asp:ListItem>
        <asp:ListItem>2016</asp:ListItem>
        <asp:ListItem>2017</asp:ListItem>
        <asp:ListItem>2018</asp:ListItem>
        <asp:ListItem>2019</asp:ListItem>
        <asp:ListItem>2020</asp:ListItem>
    </asp:DropDownList>
  </td>
    <td align="right" width="80" height="25" class="zhengwenjiacu"></td>
  <td align="left" width="124">
      
  </td>
</tr>
<tr>
    <td align="right" width="80" height="25" class="zhengwenjiacu">单位：</td>
    <td align="left">
        <asp:DropDownList ID="DDLReportArea" runat="server"><asp:ListItem>请选择</asp:ListItem></asp:DropDownList>
    </td>
    <td align="right" width="80" height="25" class="zhengwenjiacu">统计负责人：</td>
  <td align="left">
      <asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="80px"/>
  </td>
  <td align="right" width="80" height="25" class="zhengwenjiacu">填表人：</td>
  <td align="left" colspan="3">
      <asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="80px"/>
  </td>    
</tr>
</table>
<table width="980" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="980" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td height="25" align="center" colspan="6" bgcolor="#FFFFCC" class="zhengwenjiacu">一孩户</td>
    <td height="25" align="center" colspan="4" bgcolor="#FFFFCC" class="zhengwenjiacu">放弃二胎生育指标户</td>
</tr>
<tr class="zhengwen">
    <td height="25" align="center" colspan="2" bgcolor="#FFFFCC" class="zhengwenjiacu">合计</td>
    <td height="25" align="center" colspan="2" bgcolor="#FFFFCC" class="zhengwenjiacu">16岁以下（含16岁）</td>
    <td height="25" align="center" colspan="2" bgcolor="#FFFFCC" class="zhengwenjiacu">16岁以上</td>
    <td height="25" align="center" colspan="2" bgcolor="#FFFFCC" class="zhengwenjiacu">累计户数</td>
    <td height="25" align="center" colspan="2" bgcolor="#FFFFCC" class="zhengwenjiacu">2012年</td>
</tr>
<tr class="zhengwen">
    <td width="140" height="25" align="right" rowspan="4" colspan="2" class="zhengwenjiacu"><asp:Label ID="txtFileds03" runat="server"></asp:Label></td>
    <td width="140" align="right" bgcolor="#CCFFFF"">独男户小计：</td>
    <td align="left" width="120"><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="80px"/></td>
    <td width="140" align="right" bgcolor="#CCFFFF">独男户小计：</td>
    <td align="left" width="120"><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="80px"/></td>
    <td width="140" align="right" bgcolor="#CCFFFF">总户数：</td>
    <td align="left" width="120"><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="80px"/></td> 
    <td width="140" align="right" bgcolor="#CCFFFF">总户数：</td>
    <td align="left" width="120"><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="80px"/></td> 
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF">其中领证：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="80px"/></td>
    <td align="right" bgcolor="#CCFFFF">其中领证：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="80px"/></td>
    <td align="right" width="160" bgcolor="#CCFFFF">享受奖励户数：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="80px"/></td>
    <td align="right" width="160" bgcolor="#CCFFFF">享受奖励户数：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="80px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF">独女户小计：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="80px"/></td>
    <td align="right" width="160" bgcolor="#CCFFFF">独女户小计：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="80px"/></td>
    <td align="right" width="160"></td>
    <td align="left" width="*"></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF">其中领证：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="80px"/></td>
    <td align="right" width="160" bgcolor="#CCFFFF">其中领证：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="80px"/></td>
    <td align="right" width="160"></td>
    <td align="left" width="*"></td>
</tr>
</table>
<table width="980" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" OnClick="btnAdd_Click"  CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>
