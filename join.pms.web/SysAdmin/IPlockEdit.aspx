<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IPlockEdit.aspx.cs" Inherits="join.pms.web.SysAdmin.IPlockEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link href="/css/right.css" type="text/css" rel="stylesheet"/>
<script language="javascript" src="/includes/commOA.js" type="text/javascript"></script>
<style type="text/css">
<!--
.butStyle {line-height: 20px; color: #000000; text-decoration: none; font-size: 12px;}
-->
</style>
</head>
<body>
<form id="form1" runat="server">
<input id="txtUserPwd" runat="server" type="hidden" />
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：<br />1、限制单个IP仅需填写起始IP；<br />
            2、限制IP段时需填写起始IP和结尾IP，同时注意起始IP要小于结尾IP；<br />
            3、输入IP时请注意IP的合法性；<br />
            4、请操作IP限制参数信息后，点击“提交”按钮即可完成操作。</div>

<!--Start-->
<div id="ec_bodyZone" class="part_bg2">

<!-- 编辑区 -->
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" class="ihx_table">
<tr>
  <th>名称：</th>
  <td class="txt1"><asp:TextBox ID="txtIpName" runat="server" EnableViewState="False" MaxLength="25" Width="200px" />*</td>
</tr>
<tr>
  <th>限制类型：</th>
  <td class="txt1"> <script type="text/javascript">  
    function IsOpen(obj)
    {   
       if(obj.value=="0"){document.getElementById("trIpEnd").style.display="none";}
       else{document.getElementById("trIpEnd").style.display="block"; }
       }                                 
        </script>

        <input id="rdSingle" runat="server" type="radio" name="rdOpen" value="0" onclick="IsOpen(this)" checked />单个IP
        <input id="rdMore" runat="server" type="radio" name="rdOpen" value="1" onclick="IsOpen(this)" />IP段</td>
</tr>
<tr>
  <th>限制IP：</th>
  <td class="txt1"><div style ="float:left; width:180px;">起始IP：<asp:TextBox ID="txtIpStart" runat="server" EnableViewState="False" MaxLength="15" Width="100px" /><span style="color: #ff1400">*</span></div>
      <div id="trIpEnd" runat="server" style="display:none; float:left;">结尾IP：<asp:TextBox ID="txtIpEnd" runat="server" EnableViewState="False" MaxLength="15" Width="100px" /></div></td>
</tr>
</table>
<div class="ihx_table_xt"></div>

<!-- 操作按钮 -->
<table border="0" cellpadding="0" cellspacing="0" class="ihx_table">
  <tr>
    <th>&nbsp;</th>
    <td><asp:Button ID="btnAdd" runat="server" Text=" 提交 " OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>
<input type="button" name="ButBackPage" value=" 返回 " id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" /></td>
  </tr>
</table>
<!--End-->
</div>
</form>
</body>
</html>