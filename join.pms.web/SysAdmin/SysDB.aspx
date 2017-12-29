<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysDB.aspx.cs" Inherits="join.pms.web.SysAdmin.SysDB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>数据库备份</title>
<link href="/css/index.css" type="text/css" rel="stylesheet"/>
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
-->
</style>
</head>
<body>
<form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请点击按钮完成操作，数据库操作尽可能地等待系统访问量少的时候。</div>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" height="400" valign="top">

<!-- 编辑区 -->
<br/><br/><br/>
<div id="Div1" class="small" style="width: 100%; position: relative;">
<table class="lvtBg" width="100%" border="0" cellpadding="0" cellspacing="1"><tbody><tr><td>
<!-- 编辑区 -->
<table width="900" border="0" cellpadding="0" cellspacing="0" >
            <tr>
              <td  height="26" class="small">请点击“数据库备份”按钮进行系统数据库备份工作……   </td>
              </tr>
            <tr>
              <td height="1" bgcolor="#BBD3EA"></td>
            </tr>
            <tr>
              <td style="padding-left:1px;">
              <br/>
                  <asp:Button ID="ButtonDbBak" runat="server" OnClick="ButtonDbBak_Click" Text="数据库备份" /><br/><br/>
                  <asp:Literal ID="LiteralMsg" runat="server" Text="提示信息……"></asp:Literal><br/><br/>
                <br/></td>
            </tr>
          </table>
<!-- 主操作区 End-->
</td></tr></tbody></table></div>

<!-- 主操作区 End-->
<!--End-->
</td></tr></table>
</td></tr></table>
<br/><br/>
</form>
</body>
</html>