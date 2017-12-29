<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rename.aspx.cs" Inherits="join.pms.web.Disk.Rename" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>文件夹重命名</title>
<base target="_self">
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="../css/list.css" rel="stylesheet" type="text/css" />
<style>
body{margin:0px;}
.dxx{background:url(../images/dxxbg.jpg) repeat-x left top;}
.multieditbox2{
    background:#ffffff url(../images/dxx.gif) no-repeat right bottom;
    border-bottom:  #006634 1px solid;
    border-left:  #006634 1px solid;
    border-right: #006634 1px solid;
    border-top:  #006634 1px solid;
    color:#006634;
    cursor: text;
    font-family: "宋体";
    font-size: 12px;
    padding: 1px; 
}
</style>
</head>
<body>
<form id="form1" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr><!-- cuserinput cusersubmit cuserreset multieditbox2-->
    <td height="180" valign="top" class="dxx">
        <div id="ListViewContents" style="width: 100%; position: relative;">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td height="30" align="right">&nbsp;</td>
            <td colspan="2">&nbsp;</td>
          </tr>
          <tr>
            <td width="80" align="right" class="page"></td>
            <td height="30" class="page">请输入要重命名的文件夹名称：</td>
          </tr>
          <tr>
            <td width="80" align="right" class="page"></td>
            <td height="30"><asp:TextBox ID="txtDirectoryName" runat="server" CssClass="cuserinput"></asp:TextBox></td>
          </tr>
          <tr>
            <td height="30" align="right">&nbsp;</td>
            <td colspan="2">&nbsp;</td>
          </tr>
          <tr>
            <td align="right" class="page">&nbsp;</td>
            <td height="30" >
            <asp:Button ID="butSubmit" runat="server" Text=" 确定 " OnClick="butSubmit_Click" CssClass="cusersubmit" />
            <input id="Button3" onclick="javascript:window.close();" type="button" value=" 取消 " class="cuserreset" />
            </td>
          </tr>
        </table></div>
        </td>
  </tr>
</table>
</form>
</body>
</html>
