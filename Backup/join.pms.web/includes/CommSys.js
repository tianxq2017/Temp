// ***********************************************************
// 商城系统通用脚本
// 2009/06/11 By 杨胜灵
// ***********************************************************
// 屏蔽Enter键


//报表合计
function get_Val(vid,aid)
{
    v_value="0";
    for (var i=0;i<aid.split(",").length;i++)
    {
      v_value =parseInt(v_value)+parseInt(document.getElementById(aid.split(",")[i]).value);
    }
    document.getElementById(vid).value=v_value;
}

$(document).ready(function(){ 
$(".zhengwen input").blur(function(){ 
    if($(this).val()==""){
    $(this).val(0);
    }
}); 

$(".zhengwen input").focus(function(){ 
    if($(this).val()=="0"){
    $(this).val("");
    }
}); 

}); 

document.onkeypress = function(){  
    if(event.keyCode==13)  
    {  
        event.keyCode=0;  
        event.cancelBubble   =   true  
        event.returnValue   =   false;  
        return   false;  
    }  
};
// 获取地址栏参数
function getUrlPara(paraName){  
    var sUrl = location.href;
    var sReg = "(?:\\?|&){1}"+paraName+"=([^&]*)";
    var re = new RegExp(sReg,"gi");
    re.exec(sUrl);
    return RegExp.$1;
}
//验证查询关键字
function doValidate(value)
{
    var checkKey=false;
    vkeyWords=/^[^`~!@#$%^&*()+=|\\\][\]\{\}:;'\,.<>/?]{1}[^`~!@$%^&()+=|\\\][\]\{\}:;'\,.<>?]{0,19}$/; 
    if(!vkeyWords.test(value)){
        checkKey=false;
    }
    else{
       checkKey=true; 
    }  
    return checkKey;
}

function OffPageTopSearch()
{
    if(event.keyCode==13 && event.srcElement.type!='button' && event.srcElement.type!='submit' && event.srcElement.type!='reset' && event.srcElement.type!='textarea' && event.srcElement.type!='')
    event.keyCode=9;
    var sKey=document.getElementById("txtkey").value;
    if(sKey.length==0 && sKey!=null)
    {
        alert("请选择搜索关键字");  
        return false;
    }
    if(doValidate(sKey)==true)
    {
      sKey=sKey.replace("-","");
      sKey=encodeURIComponent(sKey);
      var navUrl = "/OfficialMenu/" + sKey + "/1/2.shtml";
      window.location.href = navUrl;
    }
    else{alert("查询关键词有非法字符！");}
}
function PageTopSearch()
{
    var sCode=document.getElementById("SerchCode").value;
    var sKey=document.getElementById("txtSearch").value;
    var aryCode = sCode.split(",");
    var searchType=aryCode[0];
    var eName=aryCode[1];
    if(searchType=="00")
    {
        alert("请选择搜索类别"); 
        return false;
    }
    if(sKey.length==0 && sKey!=null)
    {
        alert("请选择搜索关键字");  
        return false;
    }
    if(eName.length==0 && eName==null)
    {
        alert("非法访问，操作被终止！");  
        return false; 
    }
    if(doValidate(sKey)==true)
    {
     sKey=sKey.replace("-","");
     sKey=encodeURIComponent(sKey);
     var navUrl = "/TRADE/" + eName + "/" + searchType + "--1--0/0-"+sKey+".shtml";
     window.location.href = navUrl;
    }
    else{alert("查询关键词有非法字符！");}
 }
// 验证用户登录信息
function ValidateUsers()
{
    var userAcc = document.getElementById("loginName").value;
    var userPwd = document.getElementById("loginPass").value;
    if(userAcc=="" || userAcc==null)
    {
        alert("请输入用户登录名！");
        document.getElementById("loginName").focus();
        return false;
    }
    if(userPwd=="" || userPwd==null)
    {
        alert("请输入用户登录密码！");
        document.getElementById("loginPass").focus();
        return false;
    }
//    if(userAcc.length<5 || userAcc.length>16 )
//    {
//        alert("用户登录名长度不能小于6个字符且不能大于16个字符！");
//        document.getElementById("UserName").focus();
//        return false;
//    }
    if(userPwd.length<5 || userPwd.length>16 )
    {
        alert("用户密码长度 6-16位，请检查您的输入后再试！");
        document.getElementById("loginPass").focus();
        return false;
    }
    return true;
}

function ValidateXieCha(objCode)
{
    if(document.getElementById("txtNationalName").value==null || document.getElementById("txtNationalName").value=="")
    {
        alert("请输入姓名！");
        document.getElementById("txtNationalName").focus();
        return false;
    }
    if(document.getElementById("txtNationalID").value==null || document.getElementById("txtNationalID").value=="")
    {
        alert("请输入身份证号码！");
        document.getElementById("txtNationalID").focus();
        return false;
    }
    if(!isIdCardNo(document.getElementById("txtNationalID").value))
    {
        alert("身份证号码输入错误！");
        document.getElementById("txtNationalID").focus();
        return false;
    }
     return true;
}
function ValidateGeAn(objCode)
{
    if(document.getElementById("txtPISField001").value==null || document.getElementById("txtPISField001").value=="")
    {
        alert("请输入姓名！");
        document.getElementById("txtPISField001").focus();
        return false;
    }
    if(document.getElementById("txtPISField012").value==null || document.getElementById("txtPISField012").value=="")
    {
        alert("请选择出生日期！");
        document.getElementById("txtPISField012").focus();
        return false;
    }
    if(document.getElementById("txtPISField011").value==null || document.getElementById("txtPISField011").value=="")
    {
        alert("请输入身份证号码！");
        document.getElementById("txtPISField011").focus();
        return false;
    }
    if(!isIdCardNo(document.getElementById("txtPISField011").value))
    {
        alert("身份证号码输入错误！");
        document.getElementById("txtPISField011").focus();
        return false;
    }
     if(document.getElementById("Uc_AreaDetailSelect1_DropDownListCity").value==null || document.getElementById("Uc_AreaDetailSelect1_DropDownListCity").value=="")
    {
        alert("请选择户籍所在地！");
        return false;
    }
     if(document.getElementById("Uc_AreaDetailSelect2_DropDownListCity").value==null || document.getElementById("Uc_AreaDetailSelect2_DropDownListCity").value=="")
    {
        alert("请选择现居住地！");
        return false;
    }
     return true;
}
// 身份证号码验证
function isIdCardNo(num) 
{
    var factorArr = new Array(7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2,1);
    var error;
    var varArray = new Array();
    var intValue;
    var lngProduct = 0;
    var intCheckDigit;
    var intStrLen = num.length;
    var idNumber = num;    
    
    if ((intStrLen != 15) && (intStrLen != 18)) return false;
 
    // check and set value
    for(i=0;i<intStrLen;i++) {
        varArray[i] = idNumber.charAt(i);
        if ((varArray[i] < '0' || varArray[i] > '9') && (i != 17)) {return false;} 
        else if (i < 17) {varArray[i] = varArray[i]*factorArr[i];}
    }
    if (intStrLen == 18) {
        //check date
        var date8 = idNumber.substring(6,14);
        if (checkDate(date8) == false) {
         //error = "身份证中日期信息不正确！.";
         //alert(error);
         return false;
        }        
        // calculate the sum of the products
        for(i=0;i<17;i++) {
         lngProduct = lngProduct + varArray[i];
        }        
        // calculate the check digit
        intCheckDigit = 12 - lngProduct % 11;
        switch (intCheckDigit) {
         case 10:
             intCheckDigit = 'X';
             break;
         case 11:
             intCheckDigit = 0;
             break;
         case 12:
             intCheckDigit = 1;
             break;
        }        
        // check last digit
        if (varArray[17].toUpperCase() != intCheckDigit) {
         //error = "身份证效验位错误!正确为： " + intCheckDigit + ".";
         //alert(error);
         return false;
        }
    } 
    else{        //length is 15
        //check date
        var date6 = idNumber.substring(6,12);
        if (checkDate(date6) == false) {
         //alert("身份证日期信息有误！.");
         return false;
        }
    }
    //alert ("Correct.");
    return true;
 }
 
 // 是否为整数
function checkDate(strInteger){  
    // /^(-|\+)?\d+$/ 
    //var  newPar=/^(- ¦\+)?\d+$/;
    //return newPar.test(strInteger); 
    return true;
} 
// 插入文档资料资料
function SetFCKeditorDocs(sourceFile,saveFile,fileType,oprateType)
{
    // ".gif", ".bmp", ".jpg", ".zip", ".rar", ".doc" ,".xls",".txt",".swf",".wmv"
    if(sourceFile.length>3 && saveFile.length>4 && fileType.length>2)
    {
        if(oprateType=="movie")
        {
            document.getElementById('txtUpFiles').value += saveFile +",";
        }
        else
        {
            //alert(fileType);
            var docsHtml = "";
            var serverUrl = "";//http://192.168.0.10/
            switch (fileType)
            {
                case ".gif":
                    docsHtml = "<img src=\"" + serverUrl + saveFile + "\" />";
	                break
	            case ".bmp":
                    docsHtml = "<img src=\"" + serverUrl + saveFile + "\" />";
	                break
	            case ".jpg":
                    docsHtml = "<img src=\"" + serverUrl + saveFile + "\" />";
	                break
	            case ".swf":
                    docsHtml = "<object codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0\" width=\"400\" height=\"300\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\">";
                    docsHtml += "<param value=\"" + serverUrl + saveFile + "?vcastr_file=" + serverUrl + saveFile + "\" name=\"movie\" />";
                    docsHtml += "<param value=\"high\" name=\"quality\" /><param value=\"true\" name=\"allowFullScreen\" />";
                    docsHtml += "<embed pluginspage=\"http://www.macromedia.com/go/getflashplayer\" src=\"" + serverUrl + saveFile + "?vcastr_file=" + serverUrl + saveFile + "\"></embed>";
                    docsHtml += "</object>";
	                break
	            case ".wmv":
                    docsHtml = "<object type=\"video/x-ms-wmv\" data=\"" + serverUrl + saveFile + "\" width=\"320\" height=\"260\">";
                    docsHtml +="<param name=\"src\" value=\"" + serverUrl + saveFile + "\" />";
                    docsHtml +="<param name=\"autostart\" value=\"false\" /><param name=\"controller\" value=\"true\" />";
                    docsHtml +="</object>";
	                break
                default:
                    docsHtml = "<img src=\"/"+serverUrl+"images/iconDocDown.gif\" align=\"absbottom\" /> <a href=\""+ serverUrl + saveFile +"\" target=\"_blank\">"+sourceFile+"</a>";
                    break;
            }
            document.getElementById('txtUpFiles').value += saveFile +",";
            var oEditor = FCKeditorAPI.GetInstance('FCKeditor') ;
            var oldFckValue = oEditor.GetXHTML(true);
            oEditor.SetHTML(oldFckValue + docsHtml) ;
        }
    }
}
// 插入文档资料资料
function SetCKeditorDocs(sourceFile,saveFile,fileType,fileDesc,oprateType)
{
    if(sourceFile.length>3 && saveFile.length>4 && fileType.length>2)
    {
        if(oprateType=="movie")
        {
            document.getElementById('txtUpFiles').value += saveFile +",";
        }
        else
        {
            //alert(fileType);
            var docsHtml = "";
            var serverUrl = "";
            switch (fileType)
            {
                case ".gif":
                    docsHtml = "<img src=\"" + serverUrl + saveFile + "\"  alt=\""+fileDesc+"\" />";
	                break
	            case ".bmp":
                    docsHtml = "<img src=\"" + serverUrl + saveFile + "\" alt=\""+fileDesc+"\"/>";
	                break
	            case ".jpg":
                    docsHtml = "<img src=\"" + serverUrl + saveFile + "\" alt=\""+fileDesc+"\"/>";
	                break
	            case ".swf":
                    docsHtml = "<object codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0\" width=\"400\" height=\"300\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\">";
                    docsHtml += "<param value=\"" + serverUrl + saveFile + "?vcastr_file=" + serverUrl + saveFile + "\" name=\"movie\" />";
                    docsHtml += "<param value=\"high\" name=\"quality\" /><param value=\"true\" name=\"allowFullScreen\" />";
                    docsHtml += "<embed pluginspage=\"http://www.macromedia.com/go/getflashplayer\" src=\"" + serverUrl + saveFile + "?vcastr_file=" + serverUrl + saveFile + "\"></embed>";
                    docsHtml += "</object>";
	                break
	            case ".wmv":
                    docsHtml = "<object type=\"video/x-ms-wmv\" data=\"" + serverUrl + saveFile + "\" width=\"320\" height=\"260\">";
                    docsHtml +="<param name=\"src\" value=\"" + serverUrl + saveFile + "\" />";
                    docsHtml +="<param name=\"autostart\" value=\"false\" /><param name=\"controller\" value=\"true\" />";
                    docsHtml +="</object>";
	                break
                default:
                    docsHtml = "<img src=\"/"+serverUrl+"images/iconDocDown.gif\" align=\"absbottom\" /> <a href=\""+ serverUrl + saveFile +"\" target=\"_blank\">"+sourceFile+"</a>";
                    break;
            }
            document.getElementById('txtUpFiles').value += saveFile +",";
            
            var objEditor = CKEDITOR.instances.objCKeditor ;
            objEditor.insertHtml(docsHtml);
        }
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
// 搜索
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
//购买数量是否为数字
function CheckIsNum(oNum)
{
     if(!oNum) return false;
     var strP=/^\d+(\.\d+)?$/;
     if(!strP.test(oNum)) return false;
     try{
        if(parseFloat(oNum)!=oNum) return false;
     }
     catch(ex){
        return false;
     }
     return true;
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

function fontZoom(size)
{
 document.getElementById('fontzoom').style.fontSize=size+'px'
 document.getElementById('fontzoom').style.lineHeight=size+6+'px'
}