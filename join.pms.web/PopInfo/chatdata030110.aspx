<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata030110.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.chatdata030110" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>（半）年已婚育龄妇女孩次、节育情况(农村）</title>
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
  <td align="right" width="142" height="25" class="zhengwenjiacu" width="150" bgcolor="#FFFFCC">报出日期：</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
      (填报时间,该数据实际的归属月份,选择到自然月之内即可)
    </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" bgcolor="#FFFFCC">数据单位：</td>
  <td align="left">
      <uc1:ucDataAreaSel ID="UcDataAreaSel1" runat="server" />
  </td>
</tr></table>
<table width="1100" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="1100" border="0" cellspacing="3" cellpadding="1" class="zhengwen">
  <tr>
    <td align="right" bgcolor="#FFFFCC" colspan="3">年份：</td>
    <td colspan="5">
        <asp:DropDownList ID="txtFileds01" runat="server">
            <asp:ListItem>2015</asp:ListItem>
            <asp:ListItem>2016</asp:ListItem>
            <asp:ListItem>2017</asp:ListItem>
            <asp:ListItem>2018</asp:ListItem>
            <asp:ListItem>2019</asp:ListItem>
            <asp:ListItem>2020</asp:ListItem>
        </asp:DropDownList>  
    </td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC" colspan="3">地区：</td>
    <td>
        <uc1:ucDataAreaSel ID="UcDataAreaSel2" runat="server" />
    </td>
    <td align="right" bgcolor="#FFFFCC"  rowspan="6">一孩妇女</td>
    <td align="right" bgcolor="#FFFFCC"  colspan="2">总数：</td>
    <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC"  colspan="3">育龄妇女人数：</td>
    <td><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC"  colspan="2">其中：独男户数</td>
    <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC"  colspan="3">合计：</td>
    <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" rowspan="2">绝育数</td>
    <td align="right" bgcolor="#FFFFCC">小计：</td>
    <td><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC"  colspan="3">零孩妇女：</td>
    <td><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" >其中：独男户数</td>
    <td><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC" rowspan="7">二孩妇女</td>
    <td align="right" bgcolor="#FFFFCC"  colspan="2">总数：</td>
    <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC"  colspan="2">皮埋人数：</td>
    <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td  align="right" bgcolor="#FFFFCC" colspan="2">其中：双女户数</td>
    <td><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td  align="right" bgcolor="#FFFFCC" colspan="2">药具人数：</td>
    <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC" rowspan="2">上环数：</td>
    <td align="right" bgcolor="#FFFFCC" >小计</td>
    <td><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" rowspan="5">多孩妇女</td>
    <td align="right" bgcolor="#FFFFCC" colspan="2">总数：</td>
    <td><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC" >其中：双女户数</td>
    <td><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC"  rowspan="4">其中：</td>
    <td align="right" bgcolor="#FFFFCC" >绝育人数：</td>
    <td><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC"  colspan="2">上环人数：</td>
    <td><asp:TextBox ID="txtFileds19" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC">上环人数：</td>
    <td><asp:TextBox ID="txtFileds20" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC"  colspan="2">皮埋人数：</td>
    <td><asp:TextBox ID="txtFileds21" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" >皮埋人数：</td>
    <td><asp:TextBox ID="txtFileds22" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC"  colspan="2">药具人数：</td>
    <td><asp:TextBox ID="txtFileds23" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" >药具人数：</td>
    <td><asp:TextBox ID="txtFileds24" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr>
    <td align="right" bgcolor="#FFFFCC" >合    计：</td>
    <td colspan="2"><asp:TextBox ID="txtFileds25" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" >统计负责人：</td>
    <td colspan="2"><asp:TextBox ID="txtFileds26" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" >填表人： </td>
    <td><asp:TextBox ID="txtFileds27" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
</table>
<table width="1100" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
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
