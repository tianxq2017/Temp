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
//选择文件上传 
function SelecDocs(objID,objName,objCerName)
{
    paraOpenObjID = objID;
    paraOpenObjName = objName;
    //alert(document.getElementById(objID).value);
    if (!Sys.chrome){
        var r = window.showModalDialog("/userctrl/SeUploadBySysCompts.aspx?IDs=" + document.getElementById(objID).value + "&docsname="+objCerName+"&R=" + Math.random(),"SeUploadBySysCompts","dialogHeight=240px;dialogWidth=410px;resizable=No;status=0;scrollbars=0");
	    if(r!=null){
	        SetSonWinReVal(r[0],r[1],r[2],r[3]);
	    }
    }
    else{
        //alert('chrome: ' + Sys.chrome);
        window.open("/userctrl/SeUploadBySysCompts.aspx?IDs=" + document.getElementById(objID).value + "&docsname="+objCerName+"&R=" + Math.random(),"SeUploadBySysCompts","top=150,left=200,height=240px;width=410px;toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no");
    }
}
//设置子窗口返回值
function SetSonWinReVal(fileID,sourceFile,saveFile,fileType)
{
//    document.getElementById(paraOpenObjID).value += fileID+",";
//    document.getElementById(paraOpenObjName).value += sourceFile+",";
      document.getElementById(paraOpenObjID).value = fileID+",";
      document.getElementById(paraOpenObjName).value = sourceFile;

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
	    alert("操作失败：请首先选择上传附件，然后再点击“确定”按钮。");
	}
}