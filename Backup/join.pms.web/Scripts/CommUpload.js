// ***********************************************************
// 上传通用脚本
// 2014/11/12 By 杨胜灵
// ***********************************************************
//判断浏览器版本
var Sys = {};
var ua = navigator.userAgent.toLowerCase();
var s;
(s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :
(s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :
(s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :
(s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :
(s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;
/*以下进行测试
if (Sys.ie) document.write('IE: ' + Sys.ie);
if (Sys.firefox) document.write('Firefox: ' + Sys.firefox);
if (Sys.chrome) document.write('Chrome: ' + Sys.chrome);
if (Sys.opera) document.write('Opera: ' + Sys.opera);
if (Sys.safari) document.write('Safari: ' + Sys.safari);
*/        

// 屏蔽Enter键
document.onkeydown = function(event) {  
    var target, code, tag;  
    if (!event) {  
        event = window.event; //针对ie浏览器
        target = event.srcElement;  
        code = event.keyCode;  
        if (code == 13) {  
            tag = target.tagName;  
            //alert(tag);
            if (tag == "INPUT") { return true; }  
            else { return false; }  
        }  
    }  
    else {  
        target = event.target; //针对遵循w3c标准的浏览器，如Firefox  
        code = event.keyCode;  
        if (code == 13) {  
            tag = target.tagName;  
            if (tag == "INPUT") { return false; }  
            else { return true; }   
        }  
    }  
};
// Trim
function trim(str)
{
     return str.replace(/(^\s*)|(\s*$)/g, "");
}

//传入ID和名称
var paraOpenObjID; 
var paraOpenObjName;
//选择业务文件上传 
function SelecDocs(objID,objName,objCerName)
{
    paraOpenObjID = objID;
    paraOpenObjName = objName;
    if (!Sys.chrome){
        var r = window.showModalDialog("/userctrl/SeUploadBySysCompts.aspx?IDs=" + document.getElementById(objID).value + "&docsname="+objCerName+"&R=" + Math.random(),"SeUploadBySysCompts","dialogHeight=250px;dialogWidth=450px;resizable=No;status=0;scrollbars=0");
	    if(r!=null){
	        SetSonWinReVal(r[0],r[1],r[2],r[3]);
	    }
    }
    else{
        window.open("/userctrl/SeUploadBySysCompts.aspx?IDs=" + document.getElementById(objID).value + "&docsname="+objCerName+"&R=" + Math.random(),"SeUploadBySysCompts","top=150,left=200,height=250px;width=450px;toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
    }
}
//从高拍仪扫描上传 2015/12/05 by ysl 
function SelecDocsByGpy(objID,objName,objCerName)
{
    paraOpenObjID = objID;
    paraOpenObjName = objName;

    //window.open("/userctrl/SetUploadByGpy.aspx?IDs=" + document.getElementById(objID).value + "&docsname="+objCerName+"&R=" + Math.random(),"SetUploadByGpy","top=150,left=200,height=560px;width=650px;toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
    if (!Sys.chrome){
        var r = window.showModalDialog("/userctrl/SetUploadByGpy.aspx?IDs=" + document.getElementById(objID).value + "&docsname="+objCerName+"&R=" + Math.random(),"SetUploadByGpy","dialogHeight=560px;dialogWidth=650px;resizable=No;status=0;scrollbars=0");
	    if(r!=null){
	        SetSonWinReVal(r[0],r[1],r[2],r[3]);
	    }
    }
    else{
        window.open("/userctrl/SetUploadByGpy.aspx?IDs=" + document.getElementById(objID).value + "&docsname="+objCerName+"&R=" + Math.random(),"SetUploadByGpy","top=150,left=200,height=560px;width=650px;toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
    }
}
//选择CMS文件上传 
function SelecCmsDocs(objID,objName,objCerName)
{
    paraOpenObjID = objID;
    paraOpenObjName = objName;
    if (!Sys.chrome){
        var r = window.showModalDialog("/userctrl/SetCmsUploadCompts.aspx?IDs=" + document.getElementById(objID).value + "&docsname="+objCerName+"&R=" + Math.random(),"SetCmsUploadCompts","dialogHeight=250px;dialogWidth=450px;resizable=No;status=0;scrollbars=0");
	    if(r!=null){
	        SetCmsWindowReVal(r[0],r[1],r[2],r[3]);
	    }
    }
    else{
        window.open("/userctrl/SetCmsUploadCompts.aspx?IDs=" + document.getElementById(objID).value + "&docsname="+objCerName+"&R=" + Math.random(),"SetCmsUploadCompts","top=150,left=200,height=250px;width=450px;toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
    }
}
//选择MsgDocs文件上传 
function SelecMsgDocs(objID,objName,objCerName)
{
    paraOpenObjID = objID;
    paraOpenObjName = objName;
    if (!Sys.chrome){
        var r = window.showModalDialog("/userctrl/SetMsgUploadCompts.aspx?IDs=" + document.getElementById(objID).value + "&docsname="+objCerName+"&R=" + Math.random(),"SetMsgUploadCompts","dialogHeight=250px;dialogWidth=450px;resizable=No;status=0;scrollbars=0");
	    if(r!=null){
	        SetCmsWindowReVal(r[0],r[1],r[2],r[3]);
	    }
    }
    else{
        window.open("/userctrl/SetMsgUploadCompts.aspx?IDs=" + document.getElementById(objID).value + "&docsname="+objCerName+"&R=" + Math.random(),"SetMsgUploadCompts","top=150,left=200,height=250px;width=450px;toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
    }
}

function SetCmsWindowReVal(fileID,sourceFile,saveFile,fileType)
{
    document.getElementById(paraOpenObjID).value += fileID+",";
    document.getElementById(paraOpenObjName).value += sourceFile+",";
    
    try{
        SetCKeditorDocsHref(sourceFile,saveFile,fileType,"DESC","AAAA");
        //if(fileType==".jpg" || fileType==".gif") jsAddItemToSelect(document.getElementById("txtSelCmsPic"),sourceFile,fileType);
    }
    catch(err){
        //document.writeln("Error name: " + err.name)
        //document.writeln("Error message: " + err.message)
    }
}

//选择附件
function SelecAnnex(objID,txt_ID,txt_nameId)
{
    //if (!Sys.chrome){
        //window.showModalDialog("/userctrl/SelectAnnex.aspx?bizID=" + objID + "&txt_ID=" + txt_ID + "&txt_nameId=" + txt_nameId,"SelectAnnex","dialogHeight=400px;dialogWidth=600px;resizable=No;status=0;scrollbars=0");
    //}
    //else{
        window.open("/userctrl/SelectAnnex.aspx?bizID=" + objID + "&txt_ID=" + txt_ID + "&txt_nameId=" + txt_nameId,"SelectAnnex","top=150,left=200,height=520px;width=450px;toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
    //}
}

// 附件选择完毕
function SelectAnnexEnd()
{
    var fileID = document.getElementById("txtFileID").value;
	var sourceFile = document.getElementById("txtSourceFile").value;
	var saveFile = document.getElementById("txtSaveFile").value;
	var fileType = document.getElementById("txtFileType").value;
	var txt_ID = document.getElementById("txt_ID").value;
	var txt_nameId = document.getElementById("txt_nameId").value;

	if(fileID.length>0 && sourceFile.length>0 && saveFile.length>0 && fileType.length>0)
	{
	    if (!Sys.chrome){
	        
	        var aryKeys = new Array();
	        aryKeys[0]=fileID;
	        aryKeys[1]=sourceFile;
	        aryKeys[2]=saveFile;
	        aryKeys[3]=fileType;
    	    // txtSelCmsPic
	        window.returnValue=aryKeys;
	        window.opener.SelectAnneWinReVal(fileID,sourceFile,saveFile,fileType,txt_ID,txt_nameId)
	        window.close();
	    }
	    else
	    {
	        if(window.opener) {window.opener.SelectAnneWinReVal(fileID,sourceFile,saveFile,fileType,txt_ID,txt_nameId) ;}  
            window.close();  
	    }
	}
	else
	{
	    alert("操作失败：未能获取到上传结果，请重新选择，然后再点击“确定”按钮。");
	}
}

//设置子窗口返回值
function SetSonWinReVal(fileID,sourceFile,saveFile,fileType)
{
    document.getElementById(paraOpenObjID).value += fileID+",";
    document.getElementById(paraOpenObjName).value += sourceFile+",";
      //document.getElementById(paraOpenObjID).value = fileID+",";
      //document.getElementById(paraOpenObjName).value = sourceFile;
    
    try{
        //alert(fileID+" // "+sourceFile+" // "+saveFile+" // "+fileType);
        //SetDocsToEditor(sourceFile,saveFile,fileType,"DESC");
        //if(fileType==".jpg" || fileType==".gif") SetItemToSelect(document.getElementById("txtSelCmsPic"),sourceFile,fileType);
    }
    catch(err){
        //document.writeln("Error name: " + err.name)
        //document.writeln("Error message: " + err.message)
    }
}
// 附件选择完毕
function SelectDocsEnd()
{
    var fileID = document.getElementById("txtFileID").value;
	var sourceFile = document.getElementById("txtSourceFile").value;
	var saveFile = document.getElementById("txtSaveFile").value;
	var fileType = document.getElementById("txtFileType").value;
	
	if(fileID.length>0 && sourceFile.length>0 && saveFile.length>0 && fileType.length>0)
	{
	    if (!Sys.chrome){
	        var aryKeys = new Array();
	        aryKeys[0]=fileID;
	        aryKeys[1]=sourceFile;
	        aryKeys[2]=saveFile;
	        aryKeys[3]=fileType;
    	    // txtSelCmsPic
	        window.returnValue=aryKeys;
	        window.close();
	    }
	    else
	    {
	        if(window.opener) {window.opener.SetSonWinReVal(fileID,sourceFile,saveFile,fileType) ;}  
            window.close();  
	    }
	}
	else
	{
	    alert("操作失败：未能获取到上传结果，请重新“拍照上传”，然后再点击“确定”按钮。");
	}
}

function SelectGpyEnd()
{
    var fileID = document.getElementById("txtFileID").value;
	var sourceFile = document.getElementById("txtSourceFile").value;
	var saveFile = document.getElementById("txtSaveFile").value;
	var fileType = document.getElementById("txtFileType").value;

	if(fileID.length>0 && sourceFile.length>0 && saveFile.length>0 && fileType.length>0)
	{
	    if (!Sys.chrome){
	        var aryKeys = new Array();
	        aryKeys[0]=fileID;
	        aryKeys[1]=sourceFile;
	        aryKeys[2]=saveFile;
	        aryKeys[3]=fileType;
    	    // txtSelCmsPic
	        window.returnValue=aryKeys;
	        window.close();
	    }
	    else
	    {
	        if(window.opener) {window.opener.SetSonWinReVal(fileID,sourceFile,saveFile,fileType) ;}  
            window.close();  
	    }
	}
	else
	{
	    alert("操作失败：请首先选择上传附件，然后再点击“确定”按钮。");
	}
}

//插入编辑器 图片带连接
function SetCKeditorDocsHref(sourceFile,saveFile,fileType,fileDesc,oprateType)
{
    //alert(sourceFile +" -- "+saveFile+" -- "+fileType);
    if(sourceFile.length>3 && saveFile.length>4 && fileType.length>2)
    {
        if(oprateType=="movie")
        {
            document.getElementById('txtUpFiles').value += saveFile +",";
        }
        else
        {
            var docsHtml = "";
            var serverUrl = document.getElementById('txtFileUrl').value;
            //alert(fileType);
            switch (fileType)
            {
                case ".gif":
                    docsHtml = "<a href=\"" + serverUrl + saveFile + "\" target=\"_blank\"><img src=\"" + serverUrl + saveFile + "\"  alt=\""+fileDesc+"\" /></a>";
	                break
	            case ".bmp":
                    docsHtml = "<a href=\"" + serverUrl + saveFile + "\" target=\"_blank\"><img src=\"" + serverUrl + saveFile + "\" alt=\""+fileDesc+"\"/></a>";
	                break
	            case ".jpg":
                    docsHtml = "<a href=\"" + serverUrl + saveFile + "\" target=\"_blank\"><img src=\"" + serverUrl + saveFile + "\" alt=\""+fileDesc+"\"/></a>";
	                break
	            case ".png":
                    docsHtml = "<a href=\"" + serverUrl + saveFile + "\" target=\"_blank\"><img src=\"" + serverUrl + saveFile + "\"  alt=\""+fileDesc+"\" /></a>";
	                break
	            case ".swf":
                    docsHtml = "<object codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0\" width=\"400\" height=\"300\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\">";
                    docsHtml += "<param value=\"" + serverUrl + saveFile + "?vcastr_file=" + serverUrl + saveFile + "\" name=\"movie\" />";
                    docsHtml += "<param value=\"high\" name=\"quality\" /><param value=\"true\" name=\"allowFullScreen\" />";
                    docsHtml += "<embed pluginspage=\"http://www.macromedia.com/go/getflashplayer\" src=\"" + serverUrl + saveFile + "?vcastr_file=" + serverUrl + saveFile + "\"></embed>";
                    docsHtml += "</object>";
	                break
                default:
                    docsHtml = "<img src=\"/images/iconDocDown.gif\" align=\"absbottom\" /> <a href=\""+ serverUrl + saveFile +"\" target=\"_blank\">"+sourceFile+"</a>";
                    break;
            }
            document.getElementById('txtUpFiles').value += saveFile +",";
            //alert(docsHtml)  
            CKEDITOR.instances.objCKeditor.insertHtml(docsHtml);
        }
    }
}

//Ajax 获取上传文件信息
function GetFilesInfo(){
    var upFileName = document.getElementById('txtTmpUpFiles').value;
    var docName = document.getElementById('txtDocName').value;
    var urlParams = "r="+Math.random()+"&FuncNo=GpyUpReInfo&oID="+upFileName+"&oNa="+encodeURIComponent(docName);
    ajaxLoadPage('GetInnerData.aspx',urlParams,"Get",document.getElementById('LabelMsg'),"GpyUpReInfo")

}
//设置获取到信息，赋予返回值
function SetFilesInfo(funcNo,returnText,objContainer)
{
    if(returnText.length>0)
    {
        switch (funcNo)
        {
            case "GpyUpReInfo":
                var reFileID = returnText.substr(0,returnText.indexOf(","));
                var reFilePath = returnText.substr(returnText.indexOf(",")+1);
                
                //alert("fileID="+reFileID+" ；filePath="+reFilePath)

                document.getElementById('txtFileID').value = reFileID;
                document.getElementById('txtSourceFile').value = document.getElementById('txtTmpUpFiles').value;
                document.getElementById('txtSaveFile').value = reFilePath;
                document.getElementById('txtFileType').value = ".jpg";
                
                objContainer.innerHTML="<div id=\"oprateUpFiles\">操作提示："+document.getElementById('txtDocName').value+"文件[ " + reFilePath + " ]上传到服务器成功！<br/>请点击确定按钮返回继续其它操作……";
   	            break
            case "GetCardStats":
	            cStats = reTxt;
	            break
	        case "9999":
	            //openWinows(returnText);
	            window.location.href=returnText;
	            break
            default:
	            objContainer.innerHTML = returnText;
        }
    }
    else
    {
        //openWinows("userLogin.aspx");
    }
}
        
function ajaxLoadPage(url,request,method,container,funcNo)
{
    var loading_msg='获取上传文件信息，请稍候……'
	container.innerHTML=loading_msg;
    if (!window.XMLHttpRequest) {window.XMLHttpRequest=function (){return new ActiveXObject("Microsoft.XMLHTTP");}}
	method=method.toUpperCase();
	
	var loader=new XMLHttpRequest;
	
	if (method=='GET')
	{
		urls=url.split("?");
		if (urls[1]=='' || typeof urls[1]=='undefined'){url=urls[0]+"?"+request;}
		else{url=urls[0]+"?"+urls[1]+"&"+request;}
		request=null;
	}
    //loader.setHeader("charset","gb2312");
	loader.open(method,url,true);
	if (method=="POST")	{loader.setRequestHeader("Content-Type","application/x-www-form-urlencoded;charset=gb2312");}

	loader.onreadystatechange=function()
	{
		if (loader.readyState==1){container.innerHTML=loading_msg;}
		if (loader.readyState==4)
		{
		    if(loader.status==200){SetFilesInfo(funcNo,loader.responseText,container);}
		    else{container.innerHTML=loader.responseText;loader = null;}
		}
	}
	loader.send(request);
}
