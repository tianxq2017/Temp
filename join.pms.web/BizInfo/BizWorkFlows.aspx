<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizWorkFlows.aspx.cs" Inherits="join.pms.web.BizInfo.BizWorkFlows" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link href="/css/right.css" type="text/css" rel="stylesheet" />
<script language="javascript" src="/includes/CommSys.js" type="text/javascript"></script>
<style type="text/css">
<!--
body {min-width:1350px;}
-->
</style>
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：该工作流程中，绿色表示已处理通过的审核流程；桔色表示审核驳回；灰色表示尚未处理……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">

<div class="flow_path clearfix">
  <ul>
     <asp:Literal ID="LiteralFlows" runat="server" />
  </ul>
</div>

<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />


</div>
    </form>
</body>
</html>
<link rel="stylesheet" href="/scripts/lightbox/lightbox.css" type="text/css" media="screen" />
<script src="/scripts/lightbox/jquery-1.8.3.min.js"></script>
<script src="/scripts/lightbox/lightbox.js"></script>