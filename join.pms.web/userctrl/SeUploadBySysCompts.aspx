<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeUploadBySysCompts.aspx.cs" Inherits="join.pms.web.userctrl.SeUploadBySysCompts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>系统组件上传文件</title>
    <base target="_self" />
    <script language="javascript" src="/scripts/CommUpload.js" type="text/javascript"></script>
    <style>
body{ color: #333;
    font-family: "Arial","宋体";
    font-size: 13px;}
.form_table {
	padding:15px 0 15px 25px;
}
table th {
	text-align:right;
	font-weight:normal;
}
 td {
	padding:5px 0;
}
.ps{padding:15px;
	border:1px solid #FF9B59;
	background:#FFFAF7;
	border-radius:3px;}
.text p {
	display:inline-block; *display:inline; zoom:1;
	background:url(/images/form/form_01.gif) no-repeat;
	padding-left:8px;
}
.text span {
	display:inline-block; *display:inline; zoom:1;
	background:url(/images/form/form_01.gif) no-repeat right 0;
	padding-right:8px;
	height:25px;
}
.text p input {
	background: none; border:none; margin:0; padding:0;
	width:100%; height: 25px;line-height: 25px;
}
.button {
    background:url(/images/form/form_02.gif) repeat-x 0 bottom; border: none; margin:0 0 0 8px; padding:0 12px; cursor:pointer; overflow: visible; height:25px;
    line-height:25px; font-size:12px; color:#FFF; font-family:"微软雅黑","黑体"; border-radius:3px;
}
.button:hover {
    background:url(/images/form/form_02.gif) repeat-x 0 -40px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
<!-- 页面参数-->
<input type="hidden" name="txtFileID" id="txtFileID" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSourceFile" id="txtSourceFile" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSaveFile" id="txtSaveFile" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtFileType" id="txtFileType" value="" runat="server" style="display:none;"/>
<!-- 正文部分 --><div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">     
          <tr>            
            <td class="ps">请首先点击“浏览”按钮，选择浏览您想要上传的图片后，<br/>然后点击“上传”按钮进行上传……</td>           
          </tr>
          <tr>
            <td class="text"><p><span><input type="file" size="40" name="txtUploadFiles" id="txtUploadFiles" /></span></p></td>	        
          </tr>
          <tr>
            <td><asp:Label ID="LabelMsg" runat="server"></asp:Label></td>
          </tr>
          <tr>            
            <td align="center">
            <asp:Button id="ButUploadFile" class="button" runat="server" Text=" 上传 " OnClick="ButUploadFile_Click" ></asp:Button>
             <input type="button" value=" 确定 " onClick="SelectDocsEnd();"/> 
            </td>
            <td>&nbsp;</td>
          </tr>
        </table>   </div>  
    </form>
</body>
</html>
