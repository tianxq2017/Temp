<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editHouse.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.editHouse" %>

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
<div class="tsxx">提示信息：每季度第一月5日前将上季度情况提交到本系统；孩次超过3个的填写最小三个信息……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<br/>
<!--妇女姓名,出生年月,婚姻状况,登记日期,生育情况,身份证号码,户籍地,现居住地,店铺名称,店铺地址-->
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150"  bgcolor="#CCFFFF">数据日期：</td>
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
    <td align="right" height="25" class="zhengwenjiacu" width="150">业主姓名：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" style="width: 106px">性别：</td>
  <td align="left"><asp:DropDownList ID="DropDownList02" runat="server">
    <asp:ListItem>男</asp:ListItem>
    <asp:ListItem>女</asp:ListItem>
    </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">身份证号：</td>
  <td align="left"><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">详细住址：</td>
  <td align="left">
      <uc1:ucAreaSel ID="UcAreaSel04" runat="server" />
  </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">配偶姓名：</td>
  <td align="left"><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">身份证号：</td>
  <td align="left"><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">子女数：</td>
  <td align="left"><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="5" Width="200px" />(超过3个请特殊处理)</td>
</tr></table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>

<div id="Div1" style="display:none;">
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150">一孩姓名：</td>
  <td align="left"><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">性别：</td>
  <td align="left"><asp:DropDownList ID="DropDownList09" runat="server">
      <asp:ListItem>请选择</asp:ListItem><asp:ListItem>男</asp:ListItem><asp:ListItem>女</asp:ListItem></asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">身份证号：</td>
  <td align="left"><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr></table>
</div>
<div id="Div2" style="display:none;">
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150">二孩姓名：</td>
  <td align="left"><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">性别：</td>
  <td align="left"><asp:DropDownList ID="DropDownList12" runat="server">
      <asp:ListItem>请选择</asp:ListItem><asp:ListItem>男</asp:ListItem><asp:ListItem>女</asp:ListItem></asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">身份证号：</td>
  <td align="left"><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr></table>
</div>
<div id="Div3" style="display:none;">
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150">三孩姓名：</td>
  <td align="left"><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">性别：</td>
  <td align="left"><asp:DropDownList ID="DropDownList15" runat="server">
      <asp:ListItem>请选择</asp:ListItem><asp:ListItem>男</asp:ListItem><asp:ListItem>女</asp:ListItem></asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">身份证号：</td>
  <td align="left"><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr></table>
</div>
<div id="Div4" style="display:none;">
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150">四孩姓名：</td>
  <td align="left"><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">性别：</td>
  <td align="left"><asp:DropDownList ID="DropDownList18" runat="server">
      <asp:ListItem>请选择</asp:ListItem><asp:ListItem>男</asp:ListItem><asp:ListItem>女</asp:ListItem></asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">身份证号：</td>
  <td align="left"><asp:TextBox ID="txtFileds19" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr></table>
</div>
<div id="Div5" style="display:none;">
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150">五孩姓名：</td>
  <td align="left"><asp:TextBox ID="txtFileds20" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">性别：</td>
  <td align="left"><asp:DropDownList ID="DropDownList21" runat="server">
      <asp:ListItem>请选择</asp:ListItem><asp:ListItem>男</asp:ListItem><asp:ListItem>女</asp:ListItem></asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">身份证号：</td>
  <td align="left"><asp:TextBox ID="txtFileds22" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr></table>
</div>
<div id="Div6" style="display:none;">
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150">六孩姓名：</td>
  <td align="left"><asp:TextBox ID="txtFileds23" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">性别：</td>
  <td align="left"><asp:DropDownList ID="DropDownList24" runat="server">
      <asp:ListItem>请选择</asp:ListItem><asp:ListItem>男</asp:ListItem><asp:ListItem>女</asp:ListItem></asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">身份证号：</td>
  <td align="left"><asp:TextBox ID="txtFileds25" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
</table></div>
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
<script language="javascript" type="text/javascript">
function ShowDivsBy(divNo){

    if(isNaN(divNo)){
        alert("请输入数字格式后再试！");
        return;
    }

    if(divNo>6){
        alert("超生过多，请上报计生局特殊处理！");
        return;
    }
    switch (divNo)
    {
        case "1":
            document.getElementById("Div1").style.display = "block";
            document.getElementById("Div2").style.display = "none";
            document.getElementById("Div3").style.display = "none";
            document.getElementById("Div4").style.display = "none";
            document.getElementById("Div5").style.display = "none";
            document.getElementById("Div6").style.display = "none";
            break
        case "2":
            document.getElementById("Div1").style.display = "block";
            document.getElementById("Div2").style.display = "block";
            document.getElementById("Div3").style.display = "none";
            document.getElementById("Div4").style.display = "none";
            document.getElementById("Div5").style.display = "none";
            document.getElementById("Div6").style.display = "none";
            break
        case "3":
            document.getElementById("Div1").style.display = "block";
            document.getElementById("Div2").style.display = "block";
            document.getElementById("Div3").style.display = "block";
            document.getElementById("Div4").style.display = "none";
            document.getElementById("Div5").style.display = "none";
            document.getElementById("Div6").style.display = "none";
            break
        case "4":
            document.getElementById("Div1").style.display = "block";
            document.getElementById("Div2").style.display = "block";
            document.getElementById("Div3").style.display = "block";
            document.getElementById("Div4").style.display = "block";
            document.getElementById("Div5").style.display = "none";
            document.getElementById("Div6").style.display = "none";
            break
        case "5":
            document.getElementById("Div1").style.display = "block";
            document.getElementById("Div2").style.display = "block";
            document.getElementById("Div3").style.display = "block";
            document.getElementById("Div4").style.display = "block";
            document.getElementById("Div5").style.display = "block";
            document.getElementById("Div6").style.display = "none";
            break
        case "6":
            document.getElementById("Div1").style.display = "block";
            document.getElementById("Div2").style.display = "block";
            document.getElementById("Div3").style.display = "block";
            document.getElementById("Div4").style.display = "block";
            document.getElementById("Div5").style.display = "block";
            document.getElementById("Div6").style.display = "block";
            break
        default:
            document.getElementById("Div1").style.display = "none";
            document.getElementById("Div2").style.display = "none";
            document.getElementById("Div3").style.display = "none";
            document.getElementById("Div4").style.display = "none";
            document.getElementById("Div5").style.display = "none";
            document.getElementById("Div6").style.display = "none";
            break;
    }
}
//===============
    var defaultVar = document.getElementById('txtFileds07').value;
    if(isNaN(defaultVar)){
        alert("子女数非数字格式！");
    }
    else{
        ShowDivsBy(defaultVar);
    }
    
</script>