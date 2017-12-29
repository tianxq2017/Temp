<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgEdit.aspx.cs" Inherits="join.pms.web.SysMsg.MsgEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>消息发布</title>
<link href="/css/right.css" type="text/css" rel="stylesheet" />
<script language="javascript" src="/Scripts/CommUpload.js" type="text/javascript"></script>
<script language="javascript" src="../includes/commOA.js" type="text/javascript"></script>
<!--载入 CKEditor JS 文件-->
<script language="javascript" src="/ckeditor4.4.5/ckeditor.js" type="text/javascript"></script>
</head>
<body>
<form id="form1" runat="server" enctype="multipart/form-data">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!-- 页面参数 -->
<input type="hidden" name="txtUpFiles" id="txtUpFiles" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtFileUrl" id="txtFileUrl" value="" runat="server" style="display:none;"/>
<!--提示信息-->
<div class="tsxx">提示信息：请操作消息标题和内容后，点击“提交”按钮即可完成操作。</div>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" height="500" valign="top">
<!-- 表单信息 Start -->
    <table cellspacing="0" cellpadding="0" width="95%" border="0" align="center" class="zhengwen">
    <tr><td width="80" style="height: 30px" class="zhengwenjiacu">消息标题：</td>
    <td width="*" align="left"><asp:TextBox ID="txtMsgTitle" runat="server" CssClass="butStyle" Width="400px" MaxLength="50" style="background-color:#fafafa"></asp:TextBox></td></tr>
    <tr><td width="80" style="height: 180px" class="zhengwenjiacu">消息内容：</td>
    <td width="*" align="left">
    <!--ckeditor 必须定义 class="ckeditor"-->
　　<asp:TextBox id="objCKeditor" class="ckeditor" TextMode="MultiLine"  runat="server" Height="350" Width="760"></asp:TextBox>
    </td></tr>
    <tr>
    <td width="80" style="height: 38px" class="zhengwenjiacu">消息附件：</td>
    <td width="*" align="left">
        <!-- 附件上传 Start -->
        <input id="txtDocsID" name="txtDocsID" type="hidden" />
        <input id="txtDocsName" name="txtDocsName" value="" type="hidden" />
        <input id="txtSourceName" name="txtSourceName" readonly="readonly"  style="width:300px"/>
        <input type="button" value="本地上传" onclick="SelecMsgDocs('txtDocsID','txtSourceName','')"/>
        <!-- 附件上传 End -->
    </td>
    </tr>
    
    <tr><td width="80" style="height: 30px" class="zhengwenjiacu">接 收 人：</td>
    <td width="*" align="left">
    <input id="txtExecUserID" name="txtExecUserID" type="hidden" runat="server"/>
    <input id="txtExecuteName" name="txtExecuteName" class="butStyle"  Check=1 Show="处理人" style="width: 300px" />
    <input type="button"  value=" 选 择 " onclick="SelectUsers('txtExecUserID','txtExecuteName')" />
    </td></tr><!--
    <tr><td width="100" style="height: 25px">&nbsp;</td>
    <td width="*" ></td></tr>-->
    <tr>
    <td width="80" style="height: 30px">&nbsp;</td>
    <td width="*" align="left"><br/></td>
    </tr>
    </table>	
    <!-- 表单信息 End --><br/>
<!-- 操作按钮 -->
<table cellSpacing="0" cellPadding="0" width="95%" border="0" align="center" class="zhengwen"><tr>
<td width="120" height="25" align="right" class="fb01">&nbsp;</td>
<td width="*" align="left"><asp:Button ID="btnAdd" runat="server" Text="提 交" OnClick="btnAdd_Click" class="submit6"></asp:Button>
<input name="btnCancel" type="reset" id="btnCancel" value="重 置" class="submit6"/>
<input type="button" name="ButBackPage" value="返 回" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6"/></td>
</tr></table><br/>
<!-- 主操作区 End-->

<!--End-->
</td></tr></table>
</td></tr></table>
<br/><br/><br/><br/>
</form>
</body>
</html>
