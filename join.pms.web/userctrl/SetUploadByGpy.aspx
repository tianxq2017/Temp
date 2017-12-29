<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetUploadByGpy.aspx.cs" Inherits="join.pms.web.userctrl.SetUploadByGpy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>从高拍仪扫描上传</title>
    <base target="_self" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <script language="javascript" src="/Scripts/CommUpload.js" type="text/javascript"></script>
    <script language="javascript" src="/Scripts/EloamGpaiyi.js" type="text/javascript"></script>
    
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
<body onload="Load()" onunload="Unload()">
<form id="form1" runat="server">
<!--页面参数-->
<input type="hidden" name="txtFileID" id="txtFileID" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSourceFile" id="txtSourceFile" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSaveFile" id="txtSaveFile" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtFileType" id="txtFileType" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtDocName" id="txtDocName" value="" runat="server" style="display:none;"/>
<!--本地临时保存的扫描文件-->
<input type="hidden" name="txtTmpUpFiles" id="txtTmpUpFiles" runat="server" style="display:none;"/>
<!--扫描拍照-->
<div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">     
  <tr>            
    <td class="ps">等待高拍仪启动后，摆正想要扫描拍照的资料，然后点击“拍照上传”按钮，即可上传……<br/>上传完毕后（查看界面提示信息），请点击点击“确定”按钮返回……</td>           
  </tr>
  <tr>
    <td class="text"><object id="GpyView" type="application/x-eloamplugin" width="600" height="300" name="GpyView"></object></td>	        
  </tr>
  <tr>
    <td><asp:Label ID="LabelMsg" runat="server"></asp:Label></td>
  </tr>
  <tr>            
    <td align="center">
    分辨率：<select id="selRes1" style="width: 90px" name="selRes" value="1600*1200"></select> 
    <!--
    <input class="submit_01" type="button" value="打开视频" onclick="OpenVideo()" />-->
    <input class="button" type="button" value="拍照上传" onclick="return ScanToUpload()" id="ButUploadFile" name="ButUploadFile" />
    <!--
    <input class="submit_01" type="button" value="关闭视频" onclick="CloseVideo()" />-->
    <input type="button" value=" 确定 " class="button" onclick="SelectGpyEnd();" />  
    </td>
    <td>&nbsp;</td>
  </tr>
</table>   
</div>  
<!--扫描拍照end-->   
<div style="display:none;">

<object id="thumb1" type="application/x-eloamplugin" width="1208" height="150" name="thumb" ></object><br />

<input class="submit_01" type="button" value="拍照"	onclick="Scan()" />
<br />
<input id="AddText" type="checkbox" value="" onclick="AddText(this)" checked="checked" />文字
<br />
<input class="submit_01" type="button" value="上传本地文件"	onclick="UploadToHttpServer()" />
<input class="submit_01" type="button" value="扫描直接上传"	onclick="ScanToHttpServer()" />
<!--
<input class="submit_01" type="button" value="上传本地文件"	onclick="UploadToHttpServer()" />
<input class="submit_01" type="button" value="条码/二维码识别"	onclick="DiscernBarcode()" />
-->
<br />
<input type="text" id="message" size = "195"/>
<input type="text" id="idcard" size = "195"/>
<input type="text" id="biokey" size = "195"/>
<input type="text" id="reader" size = "195"/>
<input type="text" id="mag" size = "195"/>
</div>
    
    </form>
</body>
</html>
