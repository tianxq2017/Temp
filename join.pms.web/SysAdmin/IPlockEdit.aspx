<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IPlockEdit.aspx.cs" Inherits="join.pms.web.SysAdmin.IPlockEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�ޱ���ҳ</title>
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
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ��<br />1�����Ƶ���IP������д��ʼIP��<br />
            2������IP��ʱ����д��ʼIP�ͽ�βIP��ͬʱע����ʼIPҪС�ڽ�βIP��<br />
            3������IPʱ��ע��IP�ĺϷ��ԣ�<br />
            4�������IP���Ʋ�����Ϣ�󣬵�����ύ����ť������ɲ�����</div>

<!--Start-->
<div id="ec_bodyZone" class="part_bg2">

<!-- �༭�� -->
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" class="ihx_table">
<tr>
  <th>���ƣ�</th>
  <td class="txt1"><asp:TextBox ID="txtIpName" runat="server" EnableViewState="False" MaxLength="25" Width="200px" />*</td>
</tr>
<tr>
  <th>�������ͣ�</th>
  <td class="txt1"> <script type="text/javascript">  
    function IsOpen(obj)
    {   
       if(obj.value=="0"){document.getElementById("trIpEnd").style.display="none";}
       else{document.getElementById("trIpEnd").style.display="block"; }
       }                                 
        </script>

        <input id="rdSingle" runat="server" type="radio" name="rdOpen" value="0" onclick="IsOpen(this)" checked />����IP
        <input id="rdMore" runat="server" type="radio" name="rdOpen" value="1" onclick="IsOpen(this)" />IP��</td>
</tr>
<tr>
  <th>����IP��</th>
  <td class="txt1"><div style ="float:left; width:180px;">��ʼIP��<asp:TextBox ID="txtIpStart" runat="server" EnableViewState="False" MaxLength="15" Width="100px" /><span style="color: #ff1400">*</span></div>
      <div id="trIpEnd" runat="server" style="display:none; float:left;">��βIP��<asp:TextBox ID="txtIpEnd" runat="server" EnableViewState="False" MaxLength="15" Width="100px" /></div></td>
</tr>
</table>
<div class="ihx_table_xt"></div>

<!-- ������ť -->
<table border="0" cellpadding="0" cellspacing="0" class="ihx_table">
  <tr>
    <th>&nbsp;</th>
    <td><asp:Button ID="btnAdd" runat="server" Text=" �ύ " OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>
<input type="button" name="ButBackPage" value=" ���� " id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" /></td>
  </tr>
</table>
<!--End-->
</div>
</form>
</body>
</html>