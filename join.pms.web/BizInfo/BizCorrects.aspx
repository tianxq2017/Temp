<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizCorrects.aspx.cs" Inherits="join.pms.web.BizInfo.BizCorrects" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>补正选择</title>
    <link href="/css/index.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" src="/includes/CommSys.js" type="text/javascript"></script>
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
    <script src="/Scripts/jsdate.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请选择需要补充的证明材料后；点击“提交”按钮……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtTargetUrl" id="txtTargetUrl" value="" runat="server" style="display:none;"/>

<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<br/>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<asp:Literal ID="LiteralSelDocs" runat="server" />
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td width="120"  height="25" align="right" class="zhengwenjiacu">其它材料：</td>
    <td align="left"><asp:TextBox ID="txtOtherDocs" runat="server" EnableViewState="False" MaxLength="25" Width="500px" Rows="3" TextMode="MultiLine"/></td>
</tr>
<tr class="zhengwen">
    <td width="120"  height="25" align="right" class="zhengwenjiacu">补正时间：</td>
    <td align="left">
    <input id="txtBzDate"  size="29" onclick="SelectDate(document.getElementById('txtBzDate'),'yyyy-MM-dd')"  runat="server" />
    </td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<asp:Literal ID="LiteralDocs" runat="server" />
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
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
<link rel="stylesheet" href="/scripts/lightbox/lightbox.css" type="text/css" media="screen" />
<script src="/scripts/lightbox/jquery-1.8.3.min.js"></script>
<script src="/scripts/lightbox/lightbox.js"></script>
</body>
</html>