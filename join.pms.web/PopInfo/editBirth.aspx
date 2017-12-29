<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editBirth.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.editBirth" %>

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
    window.showModalDialog("/includes/Calendar.htm",obj,"dialogWidth:200px; dialogHeight:192px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; ");    
}
</script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列住院分娩实名登记信息后；点击“提交”按钮（次月5日以前将上月情况提交）……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4" class="zhengwen"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="100"  bgcolor="#CCFFFF">数据日期：</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="PopCalendar(txtReportDate);return false;"  runat="server" />
    <input id="Image1" onclick="PopCalendar(txtReportDate);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
    (填报时间,该数据实际的归属月份,请选择到该数据月之内)
    </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu"  bgcolor="#CCFFFF">数据单位：</td>
  <td align="left"><asp:DropDownList ID="DDLReportArea" runat="server"><asp:ListItem>请选择</asp:ListItem></asp:DropDownList></td>
</tr></table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td width="100" height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">出生证编号：</td>
    <td align="left" width="300"><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td width="100" align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">新生儿姓名：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">性别：</td>
    <td align="left" width="300">
    <asp:DropDownList ID="DDLFileds03" runat="server">
    <asp:ListItem>男</asp:ListItem>
    <asp:ListItem>女</asp:ListItem>
    </asp:DropDownList>
    </td>
    <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">出生日期：</td>
    <td align="left" width="*">
    <input id="txtFileds04" readonly="readonly" size="29" onclick="PopCalendar(txtFileds04);return false;"  runat="server" />
    <input id="Image3" onclick="PopCalendar(txtFileds04);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
    </td>
</tr>
<tr class="zhengwen">
  <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">健康状况：</td>
  <td align="left" width="300">
  <asp:DropDownList ID="DDLFileds05" runat="server">
    <asp:ListItem>良好</asp:ListItem>
    <asp:ListItem>一般</asp:ListItem>
    <asp:ListItem>差</asp:ListItem>
  </asp:DropDownList>
  </td>
  <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">体重：</td>
  <td align="left" width="*"><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">身长：</td>
  <td align="left" width="300"><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">出生孕周：</td>
  <td align="left" width="*"><asp:TextBox ID="txtFileds24" runat="server" EnableViewState="False" MaxLength="10" Width="200px"/></td>
</tr>
</table>

<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<!--母亲姓名,年龄,国籍,民族,身份证号,户籍地址,现居住地-->
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td height="25" align="right"  class="zhengwenjiacu" width="100">母亲姓名：</td>
  <td align="left" width="300"><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
 <td width="100" height="25" align="right" class="zhengwenjiacu">父亲姓名：</td>
  <td align="left"><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen"><td align="right"  class="zhengwenjiacu">身份证号：</td>
  <td align="left"><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
 <td align="right" height="25" class="zhengwenjiacu">身份证号：</td>
  <td align="left"><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" >国籍：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px">中国</asp:TextBox></td>
 <td align="right" height="25" class="zhengwenjiacu">国籍：</td>
  <td align="left"><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px">中国</asp:TextBox></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">民族：</td>
  <td align="left"><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px">汉族</asp:TextBox></td>
 <td align="right" height="25" class="zhengwenjiacu">民族：</td>
  <td align="left"><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px">汉族</asp:TextBox></td>
</tr>
</table>
<!--父亲姓名,年龄,国籍,民族,身份证号,-->
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td width="100" align="right" height="25" class="zhengwenjiacu">出生地址：</td>
  <td align="left"><uc1:ucAreaSel ID="UcAreaSel08" runat="server" /></td>
</tr>
<tr class="zhengwen">
  <td width="100" align="right" height="25" class="zhengwenjiacu">家庭地址：</td>
  <td align="left"><uc1:ucAreaSel ID="UcAreaSel19" runat="server" /></td>
</tr>
<tr class="zhengwen"><td align="right"  class="zhengwenjiacu">出生地点分类：</td>
  <td align="left">
      <asp:DropDownList ID="DDLFileds20" runat="server">
        <asp:ListItem>医院</asp:ListItem>
        <asp:ListItem>妇幼保健院</asp:ListItem>
        <asp:ListItem>家庭</asp:ListItem>
        <asp:ListItem>其它</asp:ListItem>
      </asp:DropDownList>
  </td>
</tr>
<tr class="zhengwen">
  <td height="25" align="right" class="zhengwenjiacu">接生机构：</td>
  <td align="left"><asp:TextBox ID="txtFileds21" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td height="25" align="right" class="zhengwenjiacu">分娩方式：</td>
  <td align="left">
  <asp:TextBox ID="txtFileds22" runat="server" EnableViewState="False" MaxLength="25" Width="200px">顺产</asp:TextBox></td>
</tr>
<tr class="zhengwen">
  <td height="25" align="right" class="zhengwenjiacu">胎数（孩次）：</td>
  <td align="left"><asp:TextBox ID="txtFileds23" runat="server" EnableViewState="False" MaxLength="10" Width="200px">1</asp:TextBox></td>
</tr>
<tr class="zhengwen">
  <td height="25" align="right" class="zhengwenjiacu">接生人：</td>
  <td align="left"><asp:TextBox ID="txtFileds25" runat="server" EnableViewState="False" MaxLength="10" Width="200px"/></td>
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
