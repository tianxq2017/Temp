<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizCheXiao.aspx.cs" Inherits="join.pms.web.BizInfo.BizCheXiao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请输入业务编号,点击“查询”按钮核对业务信息，然后点击“强制撤销”按钮即可！</div>
<!-- 页面参数 -->
<input type="hidden" name="txtBizIDH" id="txtBizIDH" value="" runat="server" style="display:none;"/>
<!-- 主操作区 Start-->
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<div class="form_bg">
 <div  class="form_a">
  <p class="form_title"><b>业务强制撤销</b></p>
  <div class="form_table">
	      <table width="0" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <th><span class="xing">*</span>业务编码：</th>
                <td class="text"><p><span><asp:TextBox ID="txtBizID" runat="server" EnableViewState="False" MaxLength="10" Width="100px"/></span></p>&nbsp;&nbsp;
    <asp:Button ID="btnAdd" runat="server" Text="查询" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>&nbsp;&nbsp;
    <asp:Button ID="btnCheXiao" runat="server" Text="强制撤销" CssClass="submit6" OnClick="btnCheXiao_Click"></asp:Button></td>
            </tr>
            </table></div>
 </div>             
<asp:Literal ID="LiteralBizData" runat="server"></asp:Literal>                    
          
<!-- 编辑区 -->
 </div>
<!--end------------------------>
<br/><br/>
<!-- 操作按钮 -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0" align="center" class="zhengwen">
<tr><td align="right" style="width: 132px; height: 50px;">&nbsp;</td><td width="*" align="left" style="height: 25px">


</td></tr></table>
</td></tr></table>
<!-- 主操作区 End--></div>
</form>

</body>
</html>
