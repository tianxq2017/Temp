<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editMove.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.editMove" %>

<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
    <style type="text/css">
    <!--
    body {
	    margin-top: 10px;
	    background-color: #FFFFFF;
	    margin-left: 10px;
	    margin-right: 10px;
	    margin-bottom: 0px;
	    background-image: url(/images/mainyouxiabg.jpg);
	    background-position:right bottom;
	    background-repeat:no-repeat;
    }
    .butStyle {line-height: 20px; color: #000000; text-decoration: none; font-size: 12px;}
    -->
    </style>
<script language="javascript" type="text/javascript">
function PopCalendar(obj){
    showx = event.screenX - event.offsetX - 4 - 10 ; // + deltaX;
    showy = event.screenY - event.offsetY -168; // + deltaY;
    newWINwidth = 210 + 4 + 18;
    window.showModalDialog("/includes/Calendar.htm",obj,"dialogWidth:200px; dialogHeight:210px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; ");    
}
</script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列信息后；点击“提交”按钮（次月5日以前将上月情况提交）……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<br/>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="100" bgcolor="#FFFFCC">数据日期：</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="PopCalendar(txtReportDate);return false;"  runat="server" />
    <input id="Image1" onclick="PopCalendar(txtReportDate);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
    (填报时间,该数据实际的归属日期,请选择到该数据月之内)
    </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu"  bgcolor="#FFFFCC">数据单位：</td>
  <td align="left"><asp:DropDownList ID="DDLReportArea" runat="server"><asp:ListItem>请选择</asp:ListItem></asp:DropDownList></td>
</tr></table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="100" bgcolor="#CCFFFF">迁移日期：</td>
    <td align="left" width="*"><input id="txtFileds01" readonly="readonly" size="29" onclick="PopCalendar(txtFileds01);return false;"  runat="server" />
    <input id="CalendarStart" onclick="PopCalendar(txtFileds01);return false;" tabIndex="1" type="image" src="/images/calendar.gif" align="absMiddle"></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" bgcolor="#CCFFFF">迁移类型：</td>
  <td align="left"><asp:DropDownList ID="DDLFileds02" runat="server">
    <asp:ListItem>迁入</asp:ListItem>
    <asp:ListItem>迁出</asp:ListItem>
    </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" bgcolor="#CCFFFF">姓名：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" bgcolor="#CCFFFF">性别：</td>
  <td align="left"><asp:DropDownList ID="DDLFileds04" runat="server">
    <asp:ListItem>男</asp:ListItem>
    <asp:ListItem>女</asp:ListItem>
    </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" bgcolor="#CCFFFF">身份证号：</td>
  <td align="left"><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" bgcolor="#CCFFFF">原来地址：</td>
  <td align="left"><uc1:ucAreaSel ID="UcAreaSel06" runat="server" />(迁入或迁出的原始地址)</td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" bgcolor="#CCFFFF">迁往地址：</td>
  <td align="left"><uc1:ucAreaSel　ID="UcAreaSel07" runat="server" />(迁移目的地)
  </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" bgcolor="#CCFFFF">迁徙原因：</td>
  <td align="left"><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" bgcolor="#CCFFFF">婚姻状况：</td>
  <td align="left"><asp:DropDownList ID="DDLFileds09" runat="server">
            <asp:ListItem>未婚</asp:ListItem>
            <asp:ListItem>初婚</asp:ListItem>
            <asp:ListItem>再婚</asp:ListItem>
            <asp:ListItem>复婚</asp:ListItem>
            <asp:ListItem>离婚</asp:ListItem>
            <asp:ListItem>丧偶</asp:ListItem>
        </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" bgcolor="#CCFFFF">联系电话：</td>
  <td align="left"><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>


</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
</form>
</body>
</html>