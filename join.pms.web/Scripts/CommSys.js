// ***********************************************************
// 商城系统通用脚本
// 2009/06/11 By 杨胜灵
// ***********************************************************
function SetMenuUrls(objUrl){
    if(objUrl != null) window.location = objUrl;
}
function openUrl(url)
{
    var objWin = window.open(url);
    if(objWin){ }else{window.location.href = url;}
}
// 屏蔽Enter键
document.onkeydown = function(event) {  
    var target, code, tag;  
    if (!event) {  
        event = window.event; //针对ie浏览器
        target = event.srcElement;  
        code = event.keyCode;  
        if (code == 13) {  
            tag = target.tagName;  
           
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
// 获取地址栏参数
function getUrlPara(paraName){  
    var sUrl = location.href;
    var sReg = "(?:\\?|&){1}"+paraName+"=([^&]*)";
    var re = new RegExp(sReg,"gi");
    re.exec(sUrl);
    return RegExp.$1;
}

//选择上传CMS附件
function SelectCmsDocs(objID,objName)
{
	var r = window.showModalDialog("/userctrl/SetUploadForCms.aspx?IDs=" + document.getElementById(objID).value + "&R=" + Math.random(),"SetUploadFroActWin","dialogHeight=250px;dialogWidth=500px;resizable=No;status=0;scrollbars=0");
	if(r!=null){
	    document.getElementById(objID).value += r[0]+",";
	    document.getElementById(objName).value += r[1]+",";
	    SetCKeditorDocs(r[1],r[2],r[3],"DESC","AAAA");
	    if(r[3]==".jpg" || r[3]==".gif") jsAddItemToSelect(document.getElementById("txtSelCmsPic"),r[1],r[2]);
	}
}

//选择上传CMS附件
function SelectCmsDocs_1(objID,objName)
{
    var r;
    if (window.showModalDialog!=null)//IE判断
    {
        r = window.showModalDialog("/userctrl/SetUploadForCms_1.aspx?IDs=" + document.getElementById(objID).value + "&R=" + Math.random(),"SetUploadFroActWin","dialogHeight=250px;dialogWidth=500px;resizable=No;status=0;scrollbars=0");
    }
	else
    {
        r = window.open("/userctrl/SetUploadForCms_1.aspx?IDs=" + document.getElementById(objID).value + "&R=" + Math.random(),"SetUploadFroActWin","Height=250px;Width=500px;resizable=No;status=0;scrollbars=0");
    }
	if(r!=null){
	    document.getElementById(objID).value += r[0]+",";
	    document.getElementById(objName).value += r[1]+",";
	    SetCKeditorDocs(r[1],r[2],r[3],"DESC","AAAA");
	    if(r[3]==".jpg" || r[3]==".gif") jsAddItemToSelect(document.getElementById("txtSelCmsPic"),r[1],r[2]);
	}
}

//选择上传商品图片等附件
function SelectTradeDocs(objID,objName)
{
	var r = window.showModalDialog("/userctrl/SetUploadForTrade.aspx?IDs=" + document.getElementById(objID).value + "&R=" + Math.random(),"SetUploadFroActWin","dialogHeight=250px;dialogWidth=500px;resizable=No;status=0;scrollbars=0");
	if(r!=null){
	    document.getElementById(objID).value += r[0]+",";
	    document.getElementById(objName).value += r[1]+",";
	    SetCKeditorDocs(r[1],r[2],r[3],"DESC","AAAA");
	    if(r[3]==".jpg" || r[3]==".gif") jsAddItemToSelect(document.getElementById("txtSelCmsPic"),r[1],r[2]);
	}
}
//选择上传拍卖品附件
function SelectActDocs(objID,objName)
{
	var r = window.showModalDialog("/userctrl/SetUploadFroAct.aspx?IDs=" + document.getElementById(objID).value + "&R=" + Math.random(),"SetUploadFroActWin","dialogHeight=250px;dialogWidth=500px;resizable=No;status=0;scrollbars=0");
	if(r!=null){
	    document.getElementById(objID).value += r[0]+",";
	    document.getElementById(objName).value += r[1]+",";
	    SetCKeditorDocs(r[1],r[2],r[3],"DESC","AAAA");
	    if(r[3]==".jpg" || r[3]==".gif") jsAddItemToSelect(document.getElementById("txtSelCmsPic"),r[1],r[2]);
	}
}
// 附件选择完毕
function SelectActDocsEnd()
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
    	// txtSelCmsPic
	    window.returnValue=aryKeys;
	    window.close();
	}
	else
	{
	    alert("操作失败：请首先选择上传附件，然后再点击“确定”按钮。");
	}
}

// 向select选项中 加入一个Item        
function jsAddItemToSelect(objSelect, objItemText, objItemValue) {        
    //判断是否存在        
    if (jsSelectIsExitItem(objSelect, objItemValue)) {        
        //alert("该Item的Value值已经存在");        
    } else {        
        var varItem = new Option(objItemText, objItemValue);      
        objSelect.options.add(varItem);     
        //alert("成功加入");     
    }        
} 
// 判断select选项中 是否存在一个Item
function jsSelectIsExitItem(objSelect, objItemValue) {        
    var isExit = false;        
    for (var i = 0; i < objSelect.options.length; i++) {        
        if (objSelect.options[i].value == objItemValue) {        
            isExit = true;        
            break;        
        }        
    }        
    return isExit;        
} 


//插入编辑器
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
            var serverUrl ="";
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
	            case ".flv":                    
                    docsHtml ="<embed width=\"500\" height=\"420\" src=\"/scripts/jwplayer.swf\" flashvars=\"file=" + serverUrl + saveFile + "&showplayer=always&showiconplay=true&usefullscreen=true\" autostart=\"true\" allowfullscreen=\"true\">";
	                break	   
                default:
                    docsHtml = "<img src=\"/"+serverUrl+"images/iconDocDown.gif\" align=\"absbottom\" /> <a href=\""+ serverUrl + saveFile +"\" target=\"_blank\">"+sourceFile+"</a>";
                    break;
            }
            document.getElementById('txtUpFiles').value += saveFile +",";                    
            var oEditor = CKEDITOR.instances.objCKeditor;          
            oEditor.insertHtml(docsHtml); 
        }
    }
}
// 选择栏目分类
function SelectCmsClass(objID,objName)
{
	var r = window.showModalDialog("../userctrl/SelCmsClass.aspx?IDs=" + document.getElementById(objID).value + "&R=" + Math.random(),"AccountWin","dialogHeight=350px;dialogWidth=520px;resizable=No;status=1;scrollbars=1");
	if(r!=null){
	    document.getElementById(objID).value = r[0];
	    document.getElementById(objName).value = r[1];
	}
}
// 选择人员
function SelectUsers(objID,objName)
{
	var r = window.showModalDialog("../userctrl/SelUsers.aspx?IDs=" + document.getElementById(objID).value + "&R=" + Math.random(),"AccountWin","dialogHeight=350px;dialogWidth=550px;resizable=No;status=1;scrollbars=1");
	if(r!=null){
	    document.getElementById(objID).value = r[0];
	    document.getElementById(objName).value = r[1];
	}
}
//选择行业分类主类
function SelectTrades(objID,objName)
{
	var r = window.showModalDialog("/userctrl/SelTrade.aspx?IDs=" + document.getElementById(objID).value + "&R=" + Math.random(),"AccountWin","dialogHeight=350px;dialogWidth=520px;resizable=No;status=1;scrollbars=1");
	if(r!=null){
	    document.getElementById(objID).value = r[0];
	    document.getElementById(objName).value = r[1];
	}
}
//选择品牌 
function SelectBrands(objID,objName)
{
	var r = window.showModalDialog("/userctrl/SelBrand.aspx?IDs=" + document.getElementById(objID).value + "&R=" + Math.random(),"AccountWin","dialogHeight=350px;dialogWidth=520px;resizable=No;status=1;scrollbars=1");
	if(r!=null){
	    document.getElementById(objID).value = r[0];
	    document.getElementById(objName).value = r[1];
	}
}

// 选择人员,弹出网页对话框
function SelectComs(objID,objName)
{
    // document.getElementById(objID)
	var r = window.showModalDialog("/userctrl/ComSelect.aspx?IDs=" + document.getElementById(objID).value + "&R=" + Math.random(),"AccountWin","dialogHeight=350px;dialogWidth=520px;resizable=No;status=1;scrollbars=1");
	if(r!=null){
	    document.getElementById(objID).value = r[0];
	    document.getElementById(objName).value = r[1];
	}
}

//页顶搜索 By ysl 2012/08/13
//searchKey:搜索关键字
//searchjArea:搜索地区范围;
//searchType:1资讯 2商户 3产品 4供求 
function SiteTopSearch(searchKey,searchjArea,searchType)
{
    //alert(searchKey+"---"+searchjArea+"---"+searchType);
    if(event.keyCode==13) return;
    
    var navUrl = "#";
    //alert(searchType.length);
    if(searchKey.length > 0 && searchType.length>0)
    {
        //alert(searchKey);
        searchKey = trim(searchKey);
        //alert(searchKey);
        if(searchKey.length>1 && searchKey.length<11){
            if(doValidate(searchKey)==true)
            {
                searchKey=searchKey.replace("-","");
                searchKey = searchKey.replace(" ","OR");
                searchKey=encodeURIComponent(searchKey);
                //资讯 商户 产品 供求 Info/7C21C087D9E5311276463BCC6169D8C0-syskey-1.shtml
                if(searchType==1){
                    
                    navUrl = "/Info/000000-"+searchKey+"-1.shtml";
                    window.location.href = navUrl;
                }
                if(searchType==2){
                    navUrl = "/Shop/00-0-"+searchKey+"-1.shtml"; // Shop/01-0-syskey-1.shtml
                    window.location.href = navUrl;
                }
                if(searchType==3){
                    navUrl = "/TRADE/00-0-"+searchKey+"-1.shtml"; //Products/00-0-0-syskey-1.shtml
                    window.location.href = navUrl;
                }
                if(searchType==4){
                    navUrl = "/Notes/0-"+searchKey+"-1.shtml"; ///Notes/4-syskey-1.shtml
                    window.location.href = navUrl;
                }
            }
            else{
                alert("查询关键词有非法字符！");
            }
        }
        else{
            alert("查询关键词不能小于2个字或大于10个字！");
        }
        
    }
}
/*
 if(isNaN(defaultVar)){
        alert("非数字格式！");
    }
    else{
        ShowDivsBy(defaultVar);
    }
*/

// 验证用户登录信息
function ValidateUsers()
{
    var userAcc = document.getElementById("UserName").value;
    var userPwd = document.getElementById("UserPwd").value;
    if(userAcc=="" || userAcc==null)
    {
        alert("请输入用户登录名！");
        document.getElementById("UserName").focus();
        return false;
    }
    if(userPwd=="" || userPwd==null)
    {
        alert("请输入用户登录密码！");
        document.getElementById("UserPwd").focus();
        return false;
    }
    if(userAcc.length<2 || userAcc.length>10 )
    {
        alert("用户登录名长度不能小于3个字符且不能大于12个字符！");
        document.getElementById("UserName").focus();
        return false;
    }
    if(userPwd.length<5 || userPwd.length>12 )
    {
        alert("用户密码长度 6-12位，请检查您的输入后再试！");
        document.getElementById("UserPwd").focus();
        return false;
    }
    return true;
}

//插入产品图片
function SetProPhotos(photoFile)
{
    document.getElementById('txtProPhotos').value=photoFile;
    document.getElementById('tmpMsg').innerText = "成功插入产品面图片";
}
// 手机号码校验
function ValidateMobile()
{
    var mobileNo=document.getElementById("txtMobileNo");
    if (mobileNo.length ==11)
    {
        var reg=/1[3,5,8][3,4,5,6,7,8,9]\d{8}/;
        if ( mobileNo.match(reg)== null)
        {
            alert("操作提示：手机号码输入错误！");
            document.getElementById("txtMobileNo").focus();
            return false;
        }
    }
    else{return false;}
    
    return true;
}

function ValidatePreUsers()
{
    var userAcc = document.getElementById("txtUserName").value;
    var userPwd = document.getElementById("txtUserPwd").value;
    if(userAcc=="" || userAcc==null)
    {
        alert("请输入用户登录名！");
        document.getElementById("txtUserName").focus();
        return false;
    }
    if(userPwd=="" || userPwd==null)
    {
        alert("请输入用户登录密码！");
        document.getElementById("txtUserPwd").focus();
        return false;
    }
    if(userAcc.length<2 || userAcc.length>10 )
    {
        alert("用户登录名长度不能小于3个字符且不能大于12个字符！");
        document.getElementById("txtUserName").focus();
        return false;
    }
    if(userPwd.length<5 || userPwd.length>12 )
    {
        alert("用户密码长度 6-12位，请检查您的输入后再试！");
        document.getElementById("txtUserPwd").focus();
        return false;
    }
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
                    docsHtml = "<img src=\"/"+serverUrl+"/images/iconDocDown.gif\" align=\"absbottom\" /> <a href=\""+ serverUrl + saveFile +"\" target=\"_blank\">"+sourceFile+"</a>";
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
function CheckNum()
{
    var bodyGG=document.getElementById("txtTradeGG").value
    var bodyColor=document.getElementById("txtTradeColor").value
    var tradeCount = document.getElementById("textOrderNum").value;//购买数量
    var tradeNum=document.getElementById("txtTradeNum").value//库存数量
    var photosNum = document.getElementById("txtTradePtosNum").value//小图数量
    if(tradeNum=="0" || tradeNum==null)
    {
      alert("该商品已经销售完，暂时不能购买！");
      return false;  
    } 
    if(tradeCount==null || CheckIsNum(tradeCount)==false || tradeCount=="")
    {
      alert("购买数量必须为数字！");
      return false;  
    }
    if(tradeCount=="0")
    {
      alert("购买数量不能为零！");
      return false;   
    }
    if(eval(tradeCount)>eval(tradeNum))
    {
       alert("购买数量必须小于等于库存数！");
       return false;
    }
    if(bodyGG=="")
    {
        alert("请选择商品规格！");
        return false;
    }
    if(photosNum != null && photosNum!="0" && bodyColor=="")
    {
        alert("请选择商品颜色！");
        return false;
    }
}

function SetTradePho(phoID)
{
   var body=document.getElementById("hidTrade").value;
   var aryPho=body.split("|");
   var htmlCount;
   for(var i=0;i<aryPho.length;i++)
   {
        if(aryPho[i]==phoID)
        {
            document.getElementById(aryPho[i]).className="curr";
            htmlCount="已选择：'"+aryPho[i].replace("trade","")+"'";
            document.getElementById("htmlColorBody").innerHTML = htmlCount;
            document.getElementById("txtShopColor").value = aryPho[i].replace("trade","");
        }
        else
        {
            document.getElementById(aryPho[i]).className="";
        }
   }
}
function SetTradeInfo(arryID,hrefID)
{
    var aryHref = arryID.split("|");
    var htmlCount;
    for(var i=0;i<aryHref.length;i++)
    {
        if("pho"+aryHref[i]==hrefID)
        {
         document.getElementById(hrefID).className="curr";
         htmlCount="已选择：'"+hrefID.replace("pho","")+"'";
         document.getElementById("txtShopGG").value = hrefID.replace("pho","");
        }
        else
        {
         document.getElementById("pho"+aryHref[i]).className="";
        }
    }
    document.getElementById("htmlGGBody").innerHTML = htmlCount;
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
// 添加优惠券收藏
function ForFavorites(proID,userID,type,url)
{   
     if(userID.length != 0){
        if(proID.length != 0 && userID.length != 0 )
        {  
            var s = "/userctrl/GetInnerData.aspx?r="+Math.random()+"&pID="+proID+"&uID="+userID+"&type="+type+"&FuncNo=forFav";
            window.location.href = s;
        }
        else
        {  alert("加载数据失败 ...");
        }
    }
    else{alert("您还没有登录，请登录被系统后再进行产品收藏 ..."); window.location.href = url;}
}

//更改广告点击次数
//
function AdClick(AdId,objContainer)
{  
    if(AdId!=null)
    {          
        var urlParams = "r="+Math.random()+"&FuncNo=AdClick&oID="+AdId;
         ajaxLoadPage('/userctrl/GetInnerData.aspx',urlParams,"Get",objContainer,"AdClick");

    }
}

//验证回帖 非法字符
function ValidateMsgRe()
{   
    var msgBody=document.getElementById('pNotes').value;   
    if(doValidate1(msgBody)==false)
    { 
       document.getElementById('pNotes').value="";
       alert("请不要输入非法字符！");
    }   
}
//验证查询关键字
function doValidate1(value)
{
    var checkKey=false;
    vkeyWords=/^[^`~!@#$%^&*()+=|\\\][\]\{\}:;'\,.<>/?]{1}[^`~!@$%^&()+=|\\\][\]\{\}:;'\,.<>?]/; 
    if(!vkeyWords.test(value)){
        checkKey=false;
    }
    else{
       checkKey=true; 
    }  
    return checkKey;
}


function ajaxLoadPage(url,request,method,container,funcNo)
{
    var loading_msg='加载数据，请稍候 ...'
    if(funcNo=="1.1.1" || funcNo=="8686"|| funcNo=="8888") loading_msg="";
	container.innerHTML=loading_msg;
    if (!window.XMLHttpRequest)
    {
      window.XMLHttpRequest=function (){return new ActiveXObject("Microsoft.XMLHTTP");}     
    }
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
	//loader.setHeader("charset","gb2312");
	if (method=="POST")
	{
		loader.setRequestHeader("Content-Type","application/x-www-form-urlencoded;charset=gb2312");
	}

	loader.onreadystatechange=function()
	{
		if (loader.readyState==1){container.innerHTML=loading_msg;}
		if (loader.readyState==4)
		{
		    if(loader.status==200)
		    {
		        setInnerData(funcNo,loader.responseText,container);
		    }else{container.innerHTML=loader.responseText;loader = null;} 
		}
	}
	loader.send(request);
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