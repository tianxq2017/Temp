/*********************************************************************************
  ** 管理系统通用脚本
   * ("License"); You may not use this file except in compliance with the License
   * The Original Code is:  杨胜灵
   * The Initial Developer of the Original Code is Ysl.
   * 2008/12/11 By 杨胜灵.
   * All Rights Reserved.
  *
 ********************************************************************************/
// 获取地址栏参数

function getUrlPara(paraName){  
    var sUrl = location.href;
    var sReg = "(?:\\?|&){1}"+paraName+"=([^&]*)";
    var re = new RegExp(sReg,"gi");
    re.exec(sUrl);
    return RegExp.$1;
}

// 选择人员,弹出网页对话框
function SelectUsers(objID,objName)
{
    // document.getElementById(objID)
	var r = window.showModalDialog("../userctrl/UserSelect.aspx?IDs=" + document.getElementById(objID).value + "&R=" + Math.random(),"AccountWin","dialogHeight=350px;dialogWidth=550px;resizable=No;status=1;scrollbars=1");
	if(r!=null){
	    document.getElementById(objID).value = r[0];
	    document.getElementById(objName).value = r[1];
	}
}

function SelectFlowUsers(objID,objName)
{
	var r = window.showModalDialog("../userctrl/UserSelect.aspx?IDs=" + document.getElementById(objID).value + "&R=" + Math.random(),"AccountWin","dialogHeight=320px;dialogWidth=500px;resizable=No;status=1;scrollbars=1");
	if(r!=null){
	    document.getElementById(objID).value = r[0] +"~";
	    document.getElementById(objName).value = r[1];
	}
}
// 选择附件
function SelectAnnexDocs(objID,objName)
{
	var r = window.showModalDialog("../UC/AnnexDocSelect.aspx?IDs=" + document.getElementById(objID).value + "&R=" + Math.random(),"AnnexDocsWin","dialogHeight=250px;dialogWidth=500px;resizable=No;status=0;scrollbars=0");
	if(r!=null){
	    document.getElementById(objID).value = r[0];
	    document.getElementById(objName).value = r[1];
	    
	    SetFCKeditorDocs(r[1],r[2],r[3],"DFSDF");
	}
}
// 附件选择完毕 sourceFile,saveFile,fileType txtFileID
function SelectAnnexDocsEnd()
{
    var fileID = document.getElementById("txtFileID").value;
	var sourceFile = document.getElementById("txtSourceFile").value;
	var saveFile = document.getElementById("txtSaveFile").value;
	var fileType = document.getElementById("txtFileType").value;
	
	if(fileID.length>0 && sourceFile.length>0 && saveFile.length>0 && fileType.length>0)
	{
	    var aryKeys = new Array();
	    aryKeys[0]=fileID;
	    aryKeys[1]=sourceFile;
	    aryKeys[2]=saveFile;
	    aryKeys[3]=fileType;
    	
	    window.returnValue=aryKeys;
	    window.close();
	}
	else
	{
	    alert("操作失败：请首先选择上传附件，然后再点击“确定”按钮。");
	}
	
}

// 选择发送类型
function SelectSendType(objID){
    // sendNames selectUsers selectFlows

	if(objID == 1)
	{
	    // 普通发送
	    document.getElementById("sendNames").innerHTML = "选择接收人";
	    document.getElementById("selectUsers").style.display = "block";
	    document.getElementById("selectFlows").style.display = "none";
	}
	
	if(objID == 2)
	{
	    // 选择工作流发送
		document.getElementById("sendNames").innerHTML = "选择工作流";
	    document.getElementById("selectUsers").style.display = "none";
	    document.getElementById("selectFlows").style.display = "block";
	}
}

// 数据列表搜索
function showSearchDiv(objID)
{
    var x=document.getElementById(objID).style
    if (x.display=="none")
    {
        x.display="block"
    }
    else 
    {
        x.display="none"
    }
}

function moveMe(objID) {
    var posx = 0;
    var posy = 0;
    var e=document.getElementById(objID);

    if (!e) var e = window.event;

    if (e.pageX || e.pageY)
    {
        posx = e.pageX;
        posy = e.pageY;
    }
    else if (e.clientX || e.clientY)
    {
        posx = e.clientX + document.body.scrollLeft;
        posy = e.clientY + document.body.scrollTop;
    }
}

function MsgReply()
{
    var objID = document.getElementById("txtMsgID").value;
    var targetUserID = document.getElementById("txtTargetUserID").value;
    //window.open("UnvCommMsg.aspx?r="+Math.random()+"&action=view","Chat","width=500,height=320,top=250,left=260,resizable=1,scrollbars=1");
    if(objID.length>0 && targetUserID.length>0)
    {
        window.location.href = "UnvCommMsg.aspx?r="+Math.random()+"&action=reply&oID="+objID+"&uID="+targetUserID;
        document.getElementById("msgButRight").Disabled = "true";
        //document.getElementById("msgButRight").style.display = "none";   
    }
}

function GetIframeLink()
{
    var docsID = parent.document.getElementById('txtObjID').value;
    return "DocAuditRe.aspx?oID=<%=m_ObjID%>&R=" + Math.random();
}

// OA菜单控制

var m_MenuNum;
function ShowSubMenu(objID) 
{
    var objMenu = document.getElementById("menu"+objID);//eval('LM' + i);
    var objRootMenu = document.getElementById("root"+objID);
    if(objMenu!=null)
    {
        if (objMenu.style.display == 'none') 
        {
            if(objID < 9)
            {
                m_MenuNum = parseInt(document.getElementById("txtRootNum").value,10);
                HideAllMenu();
            }
            objMenu.style.display = "block";
            //document.getElementById("root"+objID).style.background-image="url('images/sysMenuBgRed.jpg')"; 
            if(objRootMenu!=null) objRootMenu.style.background="url('images/sysMenuBgRed.jpg')"; 
        }
        else 
        {
            objMenu.style.display = 'none';
        }
    }
}
    
function HideAllMenu()
{
    var menuObj;
    for (i=1;i<m_MenuNum+1;i++) 
    {
        menuObj = document.getElementById("menu"+i);
        if(menuObj!=null)
        {
            document.getElementById("menu"+i).style.display = 'none';
            document.getElementById("root"+i).style.background="url('images/sysMenuBgGray.jpg')"; 
        }
    }
}
// 菜单缩进控制
var isOpen = false;
function HideFrame()
{   
    if(!isOpen)
    {   
        parent.treeFrame.cols="10,*";
        document.getElementById("menuDisp").style.display="none";
        document.getElementById("menuHide").style.display="block";
        isOpen = true;
    }
    else
    {   
        parent.treeFrame.cols="200,*";
        document.getElementById("menuDisp").style.display="block";
        document.getElementById("menuHide").style.display="none"; 
        isOpen = false;
    } 
}
// 字体控制
function fontZoom(size)
{
 document.getElementById('fontzoom').style.fontSize=size+'px'
 document.getElementById('fontzoom').style.lineHeight=size+6+'px'
}

// 通用搜索
function loadThreadFollow(t_id,b_id){
	var targetImg =eval("document.all.followImg" + t_id);
	var targetDiv =eval("document.all.follow" + t_id);
	
	if ("object"==typeof(targetImg)){
		if (targetDiv.style.display!='none'){
			targetDiv.style.display="none";
			
		}else{
			targetDiv.style.display="block";
		}
	}
}
