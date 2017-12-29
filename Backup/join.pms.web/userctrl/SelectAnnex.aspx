<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectAnnex.aspx.cs" Inherits="join.pms.web.userctrl.SelectAnnex" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>系统组件选择上传文件</title>
    <base target="_self" />

    <script language="javascript" src="/scripts/CommUpload.js" type="text/javascript"></script>

    <script type="text/javascript">
        function selectImg(Id,title,val,type,imgname,txt_ID,txt_nameId)
        {
        
            document.getElementById("txtFileID").value = Id;
            document.getElementById("txtSourceFile").value = imgname;
            document.getElementById("txtSaveFile").value = val;
            document.getElementById("txtFileType").value = type;
            
            document.getElementById("txt_ID").value = txt_ID;
            document.getElementById("txt_nameId").value = txt_nameId;
        
        
            document.getElementById("show_Img").src = val;
            document.getElementById("lbl_img_title").innerHTML = "选择的附件是："+ title;
        }
    </script>

    <style type="text/css">
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
        <input type="hidden" name="txtFileID" id="txtFileID" value="" runat="server" style="display: none;" />
        <input type="hidden" name="txtSourceFile" id="txtSourceFile" value="" runat="server" style="display: none;" />
        <input type="hidden" name="txtSaveFile" id="txtSaveFile" value="" runat="server" style="display: none;" />
        <input type="hidden" name="txtFileType" id="txtFileType" value="" runat="server" style="display: none;" />
        <input type="hidden" name="txt_ID" id="txt_ID" value="" runat="server" style="display: none;" />
        <input type="hidden" name="txt_nameId" id="txt_nameId" value="" runat="server" style="display: none;" />
        <!-- 正文部分 -->
        <div class="form_table">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="ps" colspan="2">
                        请首先点附件标题，预览附件<br />
                        然后点击“确定”按钮进行选择……</td>
                </tr>
                <tr>
                    <td class="text">
                        <div style="overflow: auto; height: 350px; line-height:22px;">
                            <asp:Literal ID="ltr_a_Img" runat="server"></asp:Literal>
                        </div>
                    </td>
                    <td>
                        <div style="padding-left: 20px; width: 300px; height: 340px; padding-bottom: 30px;">
                            <img id="show_Img" height="100%" width="100%" alt="" src="" /><br />
                            <br />
                            <asp:Label ID="lbl_img_title" runat="server"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <input type="button" value=" 确定 " onClick="SelectAnnexEnd();"/> 
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
