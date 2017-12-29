<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizUpdate.aspx.cs" Inherits="join.pms.web.BizInfo.BizUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>业务状态变更</title>
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请输入业务编号,点击“查询”按钮核对业务信息，然后选择变更状态后点击“确认提交”按钮即可！</div>
<!-- 页面参数 -->
<input type="hidden" name="txtBizIDH" id="txtBizIDH" value="" runat="server" style="display:none;"/>
<input type="hidden" name="oldAttribs" id="oldAttribs" value="" runat="server" style="display:none;"/>
<!-- 主操作区 Start-->
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<div class="form_bg">
 <div  class="form_a">
  <p class="form_title"><b>业务状态变更</b></p>
  <div class="form_table">
	      <table width="0" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <th><span class="xing">*</span>业务编码：</th>
                <td class="text"><p><span><asp:TextBox ID="txtBizID" runat="server" EnableViewState="False" MaxLength="10" Width="100px"/></span></p>&nbsp;&nbsp;
    <asp:Button ID="btnAdd" runat="server" Text="查询" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>&nbsp;&nbsp;
    <asp:DropDownList ID="ddlType" runat="server" >  
                            <asp:ListItem Value="" Selected>请选择变更状态</asp:ListItem>  
                            <asp:ListItem Value="0">初始提交</asp:ListItem>
                            <asp:ListItem Value="1">审批中</asp:ListItem>
                            <asp:ListItem Value="2">通过（已办结）</asp:ListItem>  
                            <asp:ListItem Value="4">删除/撤销</asp:ListItem>
                            <asp:ListItem Value="5">注销</asp:ListItem>
                            <asp:ListItem Value="8">确认打印</asp:ListItem>
                            <asp:ListItem Value="9">归档</asp:ListItem>
                        </asp:DropDownList>
    &nbsp;&nbsp;
    <asp:Button ID="btnUpdate" runat="server" Text="确认提交" CssClass="submit6" OnClick="btnUpdate_Click"></asp:Button></td>
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
