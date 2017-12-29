<%@ Page Language="C#" AutoEventWireup="true" Codebehind="MainLeft.aspx.cs" Inherits="join.pms.web.MainLeft" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>栏目</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<link href="css/left.css" type="text/css" rel="stylesheet">
<script language="JavaScript" type="text/javascript" id="clientEventHandlersJS">
<!--
// 菜单控制 ShowSubMenu(6)
var m_MenuNum;
function ShowSubMenu(objID) 
{
    var objMenu = document.getElementById("menu"+objID);//eval('LM' + i);
    var objRootMenu = document.getElementById("root"+objID);
    if(objMenu!=null)
    {
        if (objMenu.style.display == 'none') 
        {
            if(objID < 12)
            {
                m_MenuNum = parseInt(document.getElementById("txtRootNum").value,10);
                HideAllMenu();
            }
            objMenu.style.display = "block";
			//objRootMenu.style.background="url('images/leftnavbg.gif') no-repeat"; 
			if(objRootMenu!=null) objRootMenu.className="lm_yi lm_yi_hover"; 
        }
        else 
        {
            //objRootMenu.className="lm_yi"; 
			objMenu.style.display = 'none';
			
        }
    }
    if(objID==4){
        //差异数据展开
        ShowSubMenu(410);
        ShowSubMenu(420);
        ShowSubMenu(430);
        ShowSubMenu(440);
        ShowSubMenu(450);
        ShowSubMenu(460);
        ShowSubMenu(470);
        ShowSubMenu(480);
        ShowSubMenu(490);
        ShowSubMenu(4100);
    }
}

// ShowSubMenu(3)
function HideAllMenu()
{
    var menuObj;
    for (i=1;i<m_MenuNum+1;i++) 
    {
        menuObj = document.getElementById("menu"+i);
        if(menuObj!=null)
        {
            document.getElementById("menu"+i).style.display = 'none';
            //document.getElementById("root"+i).style.background="url('images/line01.gif')"; 
		    document.getElementById("root"+i).className="lm_yi"; 
        }
    }
}

function HideRightFrm(){
        parent.treeFrame_r.cols="*,15";
        parent.rightFrame.document.getElementById("switchPoint_r").innerHTML = '<span class="ss_l"></span>';
        parent.rightFrame.document.getElementById("sysMenu_r").style.display="none";
}
//-->
</script>

<script language="JavaScript" type="text/javascript" id="Script1">
<!--
// 左右伸缩
var status = 1;
function switchSysBar(){
    if (1 == window.status){
        window.status = 0;
        parent.treeFrame.cols="20,*";
        document.getElementById("switchPoint").innerHTML = '<span class="ss_l"></span>';
        //document.getElementById("sysMenu").style.display="none";
        //document.getElementById("sysMenu2").style.display="none";
        //parent.mainFrame.document.getElementById("body").style.margin="0 10px 10px 12px";
        parent.leftFrame.document.getElementById("sysMenu").style.display="none";
    }
    else{
        window.status = 1;
        parent.treeFrame.cols="242,*";
        document.getElementById("switchPoint").innerHTML = '<span class="ss_r"></span>';
        //document.getElementById("sysMenu").style.display="block";
        //document.getElementById("sysMenu2").style.display="block";
        parent.leftFrame.document.getElementById("sysMenu").style.display="block";
    }
}
//-->
</script>

<!--zuoyou IE Chrome-->
<script language="JavaScript" type="text/javascript"> 
document.oncontextmenu=new Function("event.returnValue=false;"); 
document.onselectstart=new Function("event.returnValue=false;"); 
</script>
</head>
<body style="OVERFLOW-X: hidden">
<div id="sysMenu_01">
<table class="left_01" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td valign="top" class="leftbg" id="sysMenu" height="100%"><asp:Literal ID="LiteralSysMenu" runat="server" EnableViewState="false"></asp:Literal></td>
    <td height="100%" valign="middle" class="left_ss"><span id="switchPoint" onClick="switchSysBar()" class="navpoint"><span class="ss_r"></span></span></td>
  </tr>
</table>
</div>
</body>
</html>
<script language="JavaScript" type="text/javascript">
  <!-- 
  // 菜单显示控制
  var _MenuType ="<%=m_MenuType%>";
  if(_MenuType=="01"){
      ShowSubMenu(1);
      ShowSubMenu(110);
  }
  else if(_MenuType=="02"){ShowSubMenu(1);}
  else if(_MenuType=="03"){ShowSubMenu(1);}
  else if(_MenuType=="04"){ShowSubMenu(3);}
  else{
      ShowSubMenu(1);
      //ShowSubMenu(2);
  }
  
function killErrors() {return true;}window.onerror = killErrors;
 //-->
</script>