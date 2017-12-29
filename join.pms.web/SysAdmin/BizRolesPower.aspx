<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizRolesPower.aspx.cs" Inherits="join.pms.web.SysAdmin.BizRolesPower" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>业务权限</title>
<link href="/css/index.css" type="text/css" rel="stylesheet">
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
function SelectAllFunc()
{
    for(i=0;i<document.getElementsByName("cbxSel").length;i++)
    {
        if(document.all("selectAll").checked)
        {
            document.getElementsByName("cbxSel")(i).checked=true;
        }
        else
        {
            document.getElementsByName("cbxSel")(i).checked=false;
        }
    }
}
// 列选择
function SelectColsFunc(objColsIndex)
{
    for(i=0;i<document.getElementsByName("cbx"+objColsIndex).length;i++)
    {
        if(document.all("selectAll"+objColsIndex).checked)
        {
            document.getElementsByName("cbx"+objColsIndex)(i).checked=true;
        }
        else
        {
            document.getElementsByName("cbx"+objColsIndex)(i).checked=false;
        }
    }
}
// 行选择
function SetCBXClick(objID){  
    var cbx = document.getElementsByName("cbxSel");
    for(var i=0; i<cbx.length; i++)     
    {         
        if(cbx[i].type=="checkbox" && cbx[i].value == objID)         
        {             
            if(cbx[i].checked)
            {
                cbx[i].checked = false; 
            }
            else
            {
                cbx[i].checked = true; 
            }
        }
    } 
}
var trCol=null;
function rowColorOn(obj)
{
    //trCol=obj.bgColor;
    //obj.bgColor="red";
    alert("over");
    obj.style.backgroundColor='red';
}

function rowColorOff(obj)
{
    obj.bgColor=trCol;
}
// >
</script>
</head>
<body>
<form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal> -- <asp:Literal ID="LiteralRoleName" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请操作业务信息后，点击“提交”按钮即可完成操作。</div>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" height="500" valign="top">

<!-- 编辑区 -->
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" class="zhengwen">
<tr>
  <td height="25" class="zhengwenjiacu">系统权限树：</td>
</tr>
</table>
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" class="zhengwen">
<tr>
  <td align="left" class="small">
  <asp:Literal ID="LiteralFuncTable" runat="server"></asp:Literal>
  </td>
</tr>
</table>
<!-- 操作按钮 -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0" align="center" class="zhengwen">
<tr><td align="right" style="width: 132px; height: 50px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>
<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!-- 主操作区 End-->
<!--End-->
</td></tr></table>
</td></tr></table>
<br/><br/>
</form>
</body>
</html>