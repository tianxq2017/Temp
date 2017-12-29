<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizFlowApp.aspx.cs" Inherits="join.pms.web.BizInfo.BizFlowApp" %>

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
<!--jquery datepicker start-->
<link type="text/css" href="/DatePicker/css/smoothness/jquery-ui-1.7.custom.css" rel="stylesheet" />
<script type="text/javascript" src="/DatePicker/js/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="/DatePicker/js/jquery-ui-1.7.custom.min.js"></script>
<script type="text/javascript" src="/DatePicker/js/ui.datepicker-min.js"></script>
<script type="text/javascript" src="/DatePicker/js/ui.datepicker-zh-CN.js"></script>
<script type="text/javascript">
	$(document).ready(function(){
        var imgSrc = '/DatePicker/images/yellow2.png'; // datepicker bg: images/blue4.png DatePicker
	    var beginPlace = imgSrc.lastIndexOf("\/");
	    var endPlace = imgSrc.lastIndexOf("\.");
	    var linkValue = imgSrc.substring(beginPlace+1,endPlace);
	    //$("link:first").nextAll("link").remove();
	    $('\<link type=\"text\/css\" href=\"/DatePicker/development-bundle\/themes\/base\/' + linkValue + '\.css\" rel=\"stylesheet\" \/\>').insertAfter("link:first");
	});
</script>
<!--jquery datepicker end BizStepTotal -->
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列信息后；点击“提交”按钮……</div>
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
<tr class="zhengwen">
    <td width="120"  height="25" align="right" class="zhengwenjiacu">申请时间：</td>
    <td align="left"><asp:Literal ID="LiteralBizAppDate" runat="server" /></td>
</tr>
<tr class="zhengwen">
    <td width="120"  height="25" align="right" class="zhengwenjiacu">审核单位：</td>
    <td align="left"><asp:TextBox ID="txtAreaName" runat="server" EnableViewState="False" MaxLength="50" Width="500px" /></td>
</tr>
<tr class="zhengwen">
    <td width="120"  height="25" align="right" class="zhengwenjiacu">受理意见：</td>
    <td align="left"><asp:TextBox ID="txtComments" runat="server" EnableViewState="False" MaxLength="25" Width="500px" Rows="3" TextMode="MultiLine"/>(办理说明)</td>
</tr>
<tr class="zhengwen">
    <td width="120"  height="25" align="right" class="zhengwenjiacu">审核结论：</td>
    <td align="left">
    
    <asp:DropDownList ID="DDLPass" runat="server">
    <asp:ListItem Value="1">申请成功</asp:ListItem>
    <asp:ListItem Value="0">不符合条件，申请失败</asp:ListItem>
    </asp:DropDownList>
    
    </td>
</tr>
<tr class="zhengwen">
    <td  height="25" align="right" class="zhengwenjiacu">审 批 人：</td>
    <td align="left"><asp:TextBox ID="txtApproval" runat="server" EnableViewState="False" MaxLength="10" Width="200px" /></td>
</tr>
<tr class="zhengwen">
    <td height="25" align="right" class="zhengwenjiacu">处理日期：</td>
    <td align="left">
        <input id="txtOprateDate"  size="29"   runat="server" />
    <script type="text/javascript">
	$(document).ready(function(){
		$(function() {
			$("#txtOprateDate").datepicker();
		});
	});
	</script>
    </td>
 </tr>
</table>
<asp:Panel ID="PanelFaZheng" runat="server" Visible="False">
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td  height="25" align="right" class="zhengwenjiacu">证号：</td>
    <td align="left"><asp:TextBox ID="txtCertificateNoA" runat="server" EnableViewState="False" MaxLength="25" Width="200px" /></td>
</tr>
<tr class="zhengwen">
    <td width="120"  height="25" align="right" class="zhengwenjiacu">时间：</td>
    <td align="left">
    <asp:TextBox ID="txtCertificateMemo" runat="server" EnableViewState="False" MaxLength="80" Width="400px" />
    </td>
</tr>
</table>
</asp:Panel>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 审核 ·" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>
<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>