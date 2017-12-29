<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetUploadForCms.aspx.cs" Inherits="join.pms.web.userctrl.SetUploadForCms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link href="/Styles/admin.css" rel="stylesheet" type="text/css" />
    <base target="_self" />
    <script language="javascript" src="/Scripts/dataGrid.js" type="text/javascript"></script>
    <script language="javascript" src="/Scripts/CommSys.js" type="text/javascript"></script>
    <style type="text/css">
    body{margin:0px;}
    </style>
</head>
<body>
<form id="form1" runat="server" enctype="multipart/form-data"><!-- webbot-action="--WEBBOT-SELF--" -->
<!-- 页面参数 sourceFile,saveFile,fileType-->
<input type="hidden" name="txtFileID" id="txtFileID" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSourceFile" id="txtSourceFile" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSaveFile" id="txtSaveFile" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtFileType" id="txtFileType" value="" runat="server" style="display:none;"/>

<!-- 正文部分 -->
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td height="250" valign="top" class="dxx">
        <div id="Div1" style="width: 100%; position: relative;">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td height="30" width="30" align="right">&nbsp;</td>
            <td >&nbsp;</td>
            <td width="30">&nbsp;</td>
          </tr>
          <tr>
            <td align="right" class="page"></td>
            <td height="40" class="page">请首先点击“浏览”按钮，选择浏览您想要上传的图片或文件后，<br/>然后点击“上传”按钮进行上传……</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td height="50" align="right" class="page"></td>
            <td valign="middle"><input type="file" size="40" name="UploadFiles" id="UploadFiles" contenteditable="false" class="inputMu"/></td>
	    <td>&nbsp;</td>
          </tr>
          <tr>
            <td height="50" align="right" class="page"></td>
            <td valign="top" class="page"><asp:Label ID="LabelMsg" runat="server"></asp:Label></td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td align="right" class="page">&nbsp;</td>
            <td height="50">
            <asp:Button id="ButUploadFile" runat="server" Text=" 上传 " OnClick="ButUploadFile_Click" CssClass="cuserreset" ></asp:Button>
            <input type="button" value=" 确定 " onClick="SelectActDocsEnd()" class="cusersubmit"/>
            </td>
            <td>&nbsp;</td>
          </tr>
        </table></div>
        </td>
  </tr>
</table>
</form>
</body>
</html>

