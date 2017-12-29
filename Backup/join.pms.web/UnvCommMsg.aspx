<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnvCommMsg.aspx.cs" Inherits="join.pms.web.UnvCommMsg" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title><asp:Literal id="LiteralMsgHead" runat="server"></asp:Literal></title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<script language="javascript" src="includes/dataGrid.js" type="text/javascript"></script>
<script language="javascript" src="includes/commOA.js" type="text/javascript"></script>
</head>
<body class="00" bgcolor="#FFFFFF">
<form id="form1" runat="server">
<!-- 页面参数 -->
<input type="hidden" name="txtMsgID" id="txtMsgID" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtTargetUserID" id="txtTargetUserID" value="" runat="server" style="display:none;"/>
<!-- 正文部分 -->
<table width="550" border="0" cellpadding="0" cellspacing="0" background="images/AI_cd6861_01271.jpg" bgcolor="#D7F2F9" class="zhengwen">
  <tr>
    <td height="300" valign="top" class="dxx">
        <div id="Div1" style="width: 100%; position: relative;">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td height="30" align="right">&nbsp;</td>
            <td colspan="2">&nbsp;</td>
          </tr>
          <tr>
            <td width="80" align="right" class="font" style="border-bottom:1px solid #f4f4f4"><span id="Span1">消息来自：</span></td>
            <td height="30" class="font"  style="border-bottom:1px solid #f4f4f4">
            <asp:Literal ID="LiteralMsgTitle" runat="server"></asp:Literal>
            <div id="selectUsers" style="display:none;">
            <input id="txtExecUserID" name="txtExecUserID" type="hidden" runat="server" /><input id="txtExecuteName" name="txtExecuteName" class="cuserinput" Check=1 Show="处理人" style="border:1px solid #cccccc; height:20px; line-height:20px;"/><input type="button"  value="选择.." onclick="SelectUsers('txtExecUserID','txtExecuteName')" id="Button1" /></div>
            </td>
          </tr>
          <tr>
            <td align="right" class="font" style="border-bottom:1px solid #f4f4f4">发送标题：</td>
            <td height="38" colspan="2" style="border-bottom:1px solid #f4f4f4">
            <asp:TextBox ID="txtMsgTitle" runat="server" Width="330px" MaxLength="50" style="border:1px solid #cccccc; height:20px; line-height:20px;"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td height="185" align="right" class="font" style="border-bottom:1px solid #f4f4f4">发送内容：</td>
            <td style="border-bottom:1px solid #f4f4f4">
            <asp:TextBox ID="txtMsgBody" runat="server" Height="168px" TextMode="MultiLine" Width="330px" MaxLength="500" style="border:1px solid #cccccc"></asp:TextBox>
         </td>
          </tr>
          <tr>
            <td align="right" class="page">&nbsp;</td>
            <td height="50" colspan="2" >
            <input id="msgButLeft" type="button" value=" 确 定 " class="cusersubmit" runat="server" onserverclick="msgButLeft_ServerClick"/> <input id="msgButRight" type="button" value=" 回 复 " onclick="MsgReply();" runat="server" class="cuserreset"/>
            </td>
          </tr>
        </table>
        </div>
      </td>
  </tr>
</table>
</form>
<script language="javascript" type="text/javascript">
if(getUrlPara("action")=="reply")
{
    document.getElementById("MsgUserDisp").innerHTML = "回复对象：";
}
if(getUrlPara("action")=="add")
{
    document.getElementById("selectUsers").style.display = "block";
    document.getElementById("MsgUserDisp").innerHTML = "发送对象：";
}
</script>
</body>
</html>
