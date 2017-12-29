<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainTop.aspx.cs" Inherits="join.pms.web.MainTop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>个人桌面</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312"/>
<link href="/css/top.css" type="text/css" rel="stylesheet" />
<script language="javascript" type="text/javascript">
function SetColorToMenu(objID){
    SetColorToNull();
    document.getElementById(objID).className="on";
}
function SetColorToNull()
{
    var menuObj;
    for (i=1;i<9;i++) 
    {
        menuObj = document.getElementById("Menu0"+i);
        if(menuObj!=null) menuObj.className="";
    }
}
</script>
</head>
<body>
<form id="form1" runat="server">
<div class="header_01 clearfix">
  <div class="logo"><a href="MainDesk.aspx" target="mainFrame" title="起始页" onclick="ShowRightFrm();">勉县扶贫资金全程监测预警管理系统</a></div>
  <div class="tab">
    <ul class="clearfix">    
        <li id="Menu02" onclick="javascript:SetColorToMenu('Menu02');" class="on" ><a href="MainLeft.aspx?c=00" target="leftFrame" >日常工作</a></li>
        <%--<li id="Menu07" onclick="javascript:SetColorToMenu('Menu07');"><a href="MainLeft.aspx?c=12" target="leftFrame" >妇幼保健</a></li>
        <!--li id="Menu08" onclick="javascript:SetColorToMenu('Menu08');"><a href="MainLeft.aspx?c=08" target="leftFrame" >网格管理</a></li-->
        <li id="Menu01" onclick="javascript:SetColorToMenu('Menu01');"><a href="MainLeft.aspx?c=01" target="leftFrame" >数据交换</a></li>
        <li id="Menu03" onclick="javascript:SetColorToMenu('Menu03');"><a href="MainLeft.aspx?c=03" target="leftFrame" >业务报表</a></li>--%>
        <li id="Menu05" onclick="javascript:SetColorToMenu('Menu05');"><a href="MainLeft.aspx?c=05" target="leftFrame" >辅助决策</a></li>
        <li id="Menu04" onclick="javascript:SetColorToMenu('Menu04');"><a href="MainLeft.aspx?c=04" target="leftFrame" >公共信息</a></li>
        <li id="Menu06" onclick="javascript:SetColorToMenu('Menu06');"><a href="MainLeft.aspx?c=06" target="leftFrame" >系统管理</a></li>

    </ul>
  </div>
  <div class="top_nav">
    <ul>
	  <li class="a1"><a href="MainDesk.aspx" target="mainFrame" title="起始页" onclick="ShowRightFrm();"></a></li>
	  <li class="a2"><asp:Literal ID="LiteralMsg" runat="server" EnableViewState="false"></asp:Literal></li>
	  <li class="a3"><a href="/SysAdmin/TelList.aspx" target="mainFrame" title="通讯录"></a></li>
	  <li class="a4"><a href="/SysAdmin/SysUserPwd.aspx?Action=Ysl" target="mainFrame" title="设置"></a></li>
	  <li class="a5"><a href="MainTop.aspx?action=Logout" target="_parent" title="安全退出"></a></li>
	</ul>
    <%--<p><a href="Files/syshelp.pdf" target="_blank" class="linkbai">使用帮助</a> | <a href="/SysAdmin/TelList.aspx" target="mainFrame" class="linkbai">通讯录</a> | <a href="/SysAdmin/SysUserPwd.aspx?Action=Ysl" target="mainFrame" class="linkbai">密码设置</a> | <a href="/SysAdmin/SysMsgSet.aspx?Action=Ysl" target="mainFrame" class="linkbai">提醒设置</a> | <a href="MainTop.aspx?action=Logout" target="_parent" class="linkhuang">安全退出</a></p>--%>
  </div>
</div>
</form>
</body>
</html>
<script language="JavaScript" type="text/javascript"> 
function ShowRightFrm(){
        parent.treeFrame_r.cols="*,269";
        parent.rightFrame.document.getElementById("switchPoint_r").innerHTML = '<span class="ss_r"></span>';
        parent.rightFrame.document.getElementById("sysMenu_r").style.display="block";
}
document.oncontextmenu=new Function("event.returnValue=false;"); 
document.onselectstart=new Function("event.returnValue=false;"); 
</script>
