<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizRating.aspx.cs" Inherits="join.pms.web.BizInfo.BizRating" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
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
<div class="tsxx">提示信息：……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtBizFlowID" id="txtBizFlowID" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtBizStep" id="txtBizStep" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtBizStepNames" id="txtBizStepNames" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtBizStepTotal" id="txtBizStepTotal" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSealPath" id="txtSealPath" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSealPass" id="txtSealPass" value="" runat="server" style="display:none;"/>
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<br/>
<!--,BizStep,AreaCode,AreaName,Comments,Approval,Signature,CreateDate,OprateDate-->
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>

<table width="880" border="0"  cellpadding="3" cellspacing="1">
<asp:Literal ID="LiteralSvrsRating" runat="server" />

</table>

<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>