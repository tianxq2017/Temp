// 杨胜灵 2008/07/03
function ValidatorTrim(s) { 
    var m = s.match(/^\s*(\S+(\s+\S+)*)\s*$/); 
    return (m == null) ? "" : m[1]; 
} 
// 是否为正整数
function isUnsignedInteger(strInteger){  
    var  newPar=/^\d+$/  
    alert(newPar.test(strInteger));
} 
// 是否为整数
function isInteger(strInteger){  
    // /^(-|\+)?\d+$/ 
    var  newPar=/^(- ¦\+)?\d+$/;
    return newPar.test(strInteger); 
}  

// Grid 控制
function openWinows(url)
{
    var objWindow = window.open(url);
    if(objWindow){ }else{window.location.href = url;}
}
// 获取选中的 CheckBox 参数值
function GetChkItems(){ 
    
    var Items = new Array();
    //var itemsAry = document.form1.ItemChk;
    var itemsAry= window.document.getElementsByName("ItemChk");
    try{
        for(var i=0;i<itemsAry.length;i++)
        { 
            if(itemsAry[i].checked){
                //alert(document.getElementsByName("ItemChk")(i).value);
                Items[Items.length] = itemsAry[i].value; 
            }
        }
    }
    catch(err){}
    
    return Items;
    
}
// CheckBox 全选或全部取消    
function SelectAll()
{
    //var itemsAry = document.form1.ItemChk;
    var itemsAry = window.document.getElementsByName("ItemChk");
    for(i=0;i<itemsAry.length;i++)
    {
        if(document.form1.itemsi.checked)
        {
            itemsAry[i].checked=true;
        }
        else
        {
            itemsAry[i].checked=false;
        }
        //document.all("itemBot").checked = false;
    }
}
function SelectAllBot()
{
    for(i=0;i<document.getElementsByName("ItemChk").length;i++)
    {
        if(document.all("itemBot").checked)
        {
            document.getElementsByName("ItemChk")(i).checked=true;
        }
        else
        {
            document.getElementsByName("ItemChk")(i).checked=false;
        }
        document.all("itemsi").checked = false;
    }
}
// 使CheckBox只支持单选
function SetCheckBoxClear(objID){  
    var itemsAry = window.document.getElementsByName("ItemChk");
    for(var i=0; i<itemsAry.length; i++)     
    {         
        if(itemsAry[i].type=="checkbox" && itemsAry[i].value == objID)         
        {             
            itemsAry[i].checked = true; 
        }
        else
        {
            itemsAry[i].checked = false; 
        }
    } 
}

// 选中CheckBox
function SetCheckBoxClick(objID){  
    var itemsAry = window.document.getElementsByName("ItemChk");
    for(var i=0; i<itemsAry.length; i++)     
    {         
        if(itemsAry[i].type=="checkbox" && itemsAry[i].value == objID)         
        {             
            if(itemsAry[i].checked)
            {
                itemsAry[i].checked = false; 
            }
            else
            {
                itemsAry[i].checked = true; 
            }
        }
    } 
}
// 改变背景颜色
function ChangeBGColor(ObjGrid)
{
    for(var i=0;i<document.all("YGVtr").length;i++){ 
        document.all("YGVtr")(i).style.backgroundColor='';document.all("YGVtr")(i).style.color='';
    }
    ObjGrid.style.backgroundColor='#CCFF99';
    //ObjGrid.style.backgroundColor='#99ccff';ObjGrid.style.color='#ffffff';
}
// 引用
function SetQuite(objID)
{
    var quiteTxt = "<div style=\"background-color:#FFFFFF; border:#AAD2F3 1px solid;\" class=\"pad7 f12\">";
    quiteTxt += document.getElementById(objID).innerHTML;
    quiteTxt += "</div>";
    //alert(quiteTxt);
    var oEditor = FCKeditorAPI.GetInstance('txtForumContent') ;
    oEditor.SetHTML(quiteTxt) ; 
}
// 插入文档资料资料
function SetFCKeditorDocs(sourceFile,saveFile,fileType,oprateType)
{
    // ".gif", ".bmp", ".jpg", ".zip", ".rar", ".doc" ,".xls",".txt",".swf",".wmv"
    // txtUpFiles
    if(sourceFile.length>3 && saveFile.length>4 && fileType.length==4)
    {
        if(oprateType=="movie")
        {
            document.getElementById('txtUpFiles').value += saveFile +",";
        }
        else
        {
            var docsHtml = "";
            var serverUrl = "/";
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
                    docsHtml = "<img src=\""+serverUrl+"images/SiteAdmin/iconDocDown.gif\" align=\"absbottom\" /> <a href=\""+ serverUrl + saveFile +"\" target=\"_blank\">"+sourceFile+"</a>";
                    break;
            }
            document.getElementById('txtUpFiles').value += saveFile +",";
            var oEditor = FCKeditorAPI.GetInstance('txtContenttBody') ;
            var oldFckValue = oEditor.GetXHTML(true);
            oEditor.SetHTML(oldFckValue + docsHtml) ;
        }
    }
    
}
// 转到操作对象 txtUrl ChangeUrl('CMS/CMSEdit.aspx?action=edit&k=17','%e9%80%9a%e7%94%a8CMS%e9%85%8d%e7%bd%ae','edit');"> 
function ChangeUrl(objUrl,objName,objAction)
{
    if(objUrl.length > 0)
    {
        if(objUrl.indexOf("Url=true")>1)
        {
            window.location.href = objUrl;
        }
        else
        {
            var urlPageNo = document.getElementById('txtUrlPageNo').value;
            if(objAction == "del")
            {
                if(confirm("在删除之前，请您再确认一次，您真的要删除选中的数据吗？")==true)
                {
                    window.location.href = objUrl + "&p="+urlPageNo+"&oNa="+objName;
                }else{return;}
            }            
            else
            {
                window.location.href = objUrl + "&p="+urlPageNo+"&oNa="+objName;
            }
        }
    }
}

// 转到操作对象，包含选定的多行 action=initpwd'
function ChangeUrlMultCheck(objUrl,objName)
{
    if(objUrl.length > 0)
    {
        if(objUrl.indexOf("Url=true")>1)
        {
            window.location.href = objUrl;
        }
        else
        {
            var objArrayIDs = new Array();
            var selectIDs = "";
            var objFuncNo = document.getElementById('tbFuncNo').value;
            var objParams = document.getElementById('selectRowPara').value;
            var sourceUrl = document.getElementById('txtUrlParams').value;
            if(CheckChangeParams(objFuncNo,objParams)>0)
            {
                if(objUrl.indexOf("action=del")>1)
                {
                    if(confirm("在删除之前，请您再确认一次，您真的要删除选中的数据吗？")==true){}
                    else{return;}
                }
                if(objUrl.indexOf("action=initpwd")>1)
                {
                    if(confirm("在重置之前，请您再确认一次，您真的要重置修改选中登录名的密码吗？")==true){}
                    else{return;}
                }
                objArrayIDs = GetChkItems();
                //alert("选中的个数:"+objArrayIDs.length); 
                if(objArrayIDs.length>0)
                {
                    for(i=0;i<objArrayIDs.length;i++)
	                {
		                if(i==objArrayIDs.length -1){selectIDs +=objArrayIDs[i];}
		                else{selectIDs +=objArrayIDs[i] + "s" ;}
	                }
                    window.location.href = objUrl + "&k="+selectIDs+"&sourceUrl="+sourceUrl+"&oNa="+objName;
                }
                else if(objUrl.indexOf("xls")>1 || objUrl.indexOf("action=imp")>1 || objUrl.indexOf("action=exp")>1)
                {
                    //alert(sourceUrl); action=exp
                    window.location.href = objUrl + "&sourceUrl="+sourceUrl+"&oNa="+objName;
                }
                else if(objUrl.indexOf("action=dataimport")>1)              
                {
                    window.location.href = objUrl + "&sourceUrl="+sourceUrl+"&oNa="+objName;
                }
                else if(objUrl.indexOf("action=clear")>1)              
                {
                    if(confirm("在清理之前，请您再确认一次，您真的要清理该类数据（清理后不可恢复）吗？")==true){
                        window.location.href = objUrl + "&sourceUrl="+sourceUrl+"&oNa="+objName;
                    }
                    else{return;}
                }
                else
                {
                    alert("操作失败：请首先从列表中点击选择您想要操作的项目！");
                }
            }
        }
    }
}


// 判断转向参数 
function CheckChangeParams(funcNo,paraValue)
{
    var returnValue = 1;
    //alert(funcNo +" --- " +paraValue)
    if(funcNo.length > 2 && paraValue.length>0)
    {
        switch (funcNo)
        {
            case "4.2.1.2":// 询价单状态：0：询价发起 1：询价成功 2：询价拒绝 3：进入施工流程
                if(paraValue!="1")
                {
                    if(paraValue=="3"){returnValue=-1;alert("您所选定的关键词已经进入施工状态，禁止重复操作！");}
                    else{returnValue=-1;alert("您所选定的关键词尚未询价成功，禁止操作！");}
                }
	            break
	        case "4.2.2":// 预售单状态：0,待批准 1,处理中 2,销售完成 3,驳回
                if(paraValue!="0"){returnValue=-1;alert("您所选定的已经处理，禁止操作！");}
	            break
	        case "4.2.1.3":// 询价单状态：0,待批准 1,处理中 2,销售完成 3,驳回
                if(paraValue!="0"){returnValue=-1;alert("您所选定的关键词已经处理，禁止操作！");}
	            break
	        case "4.2.3":// 施工单状态：0,施工中 1,施工完成
                if(paraValue!="0"){returnValue=-1;alert("您所选定的已经处理，禁止操作！");}
	            break
            default:
                returnValue=1;
        }
    }
    return returnValue;
}

// 列表查询
function getQueryData(searchKeys,objUrl)
{
    if(searchKeys.length > 0 && objUrl.length > 0)
    {
        searchKeys = encodeURIComponent(searchKeys);
        var sUrl = objUrl + "&searchRange="+searchKeys;
        window.location.href = sUrl;
    }
    else
    { 
        alert("操作失败：查询条件不能为空！");
    }
}

// 
function goPages(papams,menus,pages,keys,objContainer)
{
    //var k = document.getElementById("searchKey").value;
    //if (keys!=null && keys!="") keys = encodeURIComponent(keys)
    if(papams.length != 0 && pages.length != 0 && menus.length !=0)
    {
        var s = "r="+Math.random()+"&identityKeys="+papams+"&p="+pages+"&k="+keys+"&m="+menus;
        ajaxLoadPage('../getData.aspx',s,"Get",objContainer,"")
    }
    else
    { 
        objContainer.value ="Loadding Failed...";
    }
}

// 获取数据
function getInnerData(funcNo,objContainer)
{
    if(funcNo.length > 2)
    {
        var urlParams = "r="+Math.random()+"&FuncNo="+funcNo+"&oID=&oNa=";
        switch (funcNo)
        {
            case "1.3.1":
                userLogin(funcNo,objContainer);
	            break
            default:
                ajaxLoadPage('sysInfo/getInnerData.aspx',urlParams,"Get",objContainer,funcNo)
        }
    }
    else
    { 
        objContainer.value ="加载数据失败 ...";
    }
}
// 提示信息显示
function ShowPromptMsg(funcNo,objContainer)
{
    // 检测是否有提示信息
    var urlParams = "r="+Math.random()+"&FuncNo="+funcNo+"&oID=0&oNa=Msg";
    ajaxLoadPage('/userctrl/GetInnerData.aspx',urlParams,"Get",objContainer,funcNo)
}
// 前台搜索  jbName
function SearchFromCMS(searchType,objContainer)
{
    var searchKey = document.getElementById("txtSearchKey").value;
    var jbName = "";
    if(searchKey.length > 0)
    {   
        if(searchType=="1") jbName = document.getElementById("jbName").value;
        searchKey = encodeURIComponent(searchKey);
        var urlParams = "r="+Math.random()+"&FuncNo=9999&oID="+searchType+"&oNa="+searchKey+"&jNa="+jbName;
        //alert(searchType +" || "+urlParams);
        ajaxLoadPage('/userctrl/GetInnerData.aspx',urlParams,"Get",objContainer,'9999')
    }
    else
    {
        alert("请输入搜索关键字！");
        return false;
    }
}

function getInnerDataByPara(funcNo,objContainer)
{
    if(funcNo.length > 2)
    {
        objID = document.getElementById('txtObjID').value;
        objName = document.getElementById('txtObjName').value;
        var urlParams = "r="+Math.random()+"&FuncNo="+funcNo+"&oID="+objID+"&oNa="+encodeURIComponent(objName);
        switch (funcNo)
        {
            case "1.3.1":
                userLogin(funcNo,objContainer);
	            break
            default:
                ajaxLoadPage('sysInfo/getInnerData.aspx',urlParams,"Get",objContainer,funcNo)
        }
    }
    else
    { 
        objContainer.value ="加载数据失败 ...";
    }
}

// 数据展示
function setInnerData(funcNo,returnText,objContainer)
{
    if(returnText.length>0)
    {
        switch (funcNo)
        {
            case "1.1.1":
                //alert(returnText.substring(0,2));
                //alert(returnText.substr(2));
                if(returnText.substring(0,2)=="OK")
                {
                    //ShowMsgWindow();
                    MSG_MSG_Show(returnText.substr(2));
                 }
                else
                {
                    //objContainer.innerHTML=returnText;
                }
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

var msgWin = null;
// 显示消息提示窗体
function  ShowMsgWindow(){ 
    if(msgWin==null || msgWin.closed)
    { 
        msgWin=window.open("/UnvCommMsg.aspx?r="+Math.random()+"&action=view","Chat","width=500,height=320,top=250,left=260,resizable=1,scrollbars=1");
        //
//        var msgWin = window.showModalDialog("UnvCommMsg.aspx?r="+Math.random()+"&action=view","Chat","dialogHeight=320px;dialogWidth=500px;resizable=No;status=1;scrollbars=1");
//	    if(msgWin!=null){
//	        document.getElementById(objID).value = r[0];
//	        document.getElementById(objName).value = r[1];

//	    }
    } 
    else
    { 
        //msgWin.focus(); 
    } 
} 


// 用户登录 
function userLogin(funcNo,objContainer)
{
    var userAcc = document.getElementById("userAccount").value;
    var userPwd = document.getElementById("userPwd").value;
    if(userAcc.length > 2 && userPwd.length > 4)
    {
        document.getElementById("userLoginSrc").style.visibility="hidden";//visible
        userAcc = encodeURIComponent(userAcc);
        userPwd = encodeURIComponent(userPwd);
        var s = "r="+Math.random()+"&FuncNo="+funcNo+"&u="+userAcc+"&p="+userPwd;
        ajaxLoadPage('sysInfo/getInnerData.aspx',s,"Get",objContainer,funcNo)
    }
    else
    { 
        alert("帐号或密码不能为空,并且密码长度至少6位!");
        //objContainer.value ="帐号或密码不能为空,并且长度不能小于5个字符!";
    }
}

// 获取搜索结果
function getSearchData(papams,menus,pages,keys,area,traceCate,cate,orders,objContainer)
{
    if(papams.length != 0 && pages.length != 0 && menus.length !=0)
    {
        var s = "search.aspx?r="+Math.random()+"&identityKeys="+papams+"&p="+pages+"&o="+orders+"&area="+area+"&t="+traceCate+"&c="+cate+"&k="+keys;
        window.location.href = s;
    }
    else
    { 
        objContainer.value ="加载数据失败 ...";
    }
}
// 资讯列表
function getDataList(papams,menus,pages,keys,orders,objContainer)
{
    if(papams.length != 0 && pages.length != 0 && menus.length !=0)
    {
        var s = "r="+Math.random()+"&identityKeys="+papams+"&p="+pages+"@"+orders+"@"+menus+"&k="+keys;
        ajaxLoadPage('getData.aspx',s,"Get",objContainer,"")
    }
    else
    { 
        objContainer.value ="加载数据失败 ...";
    }
}
// 搜索内容 "eb5b65d46d537950"
function goOASsearch(url)
{
    var searchKey = document.getElementById("txtSearchKey").value;
    var searchType = document.getElementsByName("searchType").value;
    // http://localhost:3708/INFO/Search/eb5b65d46d537950-1.shtml
    if(searchKey.length > 0 && searchType.length==1)
    {
        searchKey = encodeURIComponent(searchKey);
        window.location.href = url +"&k="+searchKey+"&searchType="+searchType;
    }
}
//Email;
function isEmail(s){
	s = trim(s); 
 	var p = /^[_\.0-9a-z-]+@([0-9a-z][0-9a-z-]+\.){1,4}[a-z]{2,3}$/i; 
 	return p.test(s);
}
//Error Msg;
function ErrMsg(s){
    //var s_info = "<table width='100%' border='0' cellspacing='0' cellpadding='0' ><tr><td width='20' height='30' >&nbsp;</td><td width='800' bgcolor='#FFFFCC' style='border-bottom: 1px solid #ff9966;border-top: 1px solid #ff9966;'><font color='#CC3300'>"+s+"</font></td><td  >&nbsp;</td></tr></table>";
    var s_info = "<table width='100%' border='0' cellspacing='0' cellpadding='0' ><tr><td  bgcolor='#FFFFCC' style='border-bottom: 1px solid #ff9966;border-top: 1px solid #ff9966;'><font color='#CC3300'>"+s+"</font></td><td  >&nbsp;</td></tr></table>";
 	return s_info;
}

//根据ID得到对象
function $(ID)
{
    return document.getElementById(ID);
}



function ajaxLoadPage(url,request,method,container,funcNo)
{
    var loading_msg='加载数据，请稍候 ...'
    if(funcNo=="1.1.1") loading_msg="";
	container.innerHTML=loading_msg;
    if (!window.XMLHttpRequest) {window.XMLHttpRequest=function (){return new ActiveXObject("Microsoft.XMLHTTP");}     }
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
// 将表单元素转换为请求字串
function formToRequestString(form_obj)
{
	var query_string='';
	var and='';
	for (i=0;i<form_obj.length ;i++ )
	{
		e=form_obj[i];
		if (e.name!='') 
		{
			if (e.type=='select-one')
			{
				element_value=e.options[e.selectedIndex].value;
			}
			else if (e.type=='checkbox' || e.type=='radio')
			{
				if (e.checked==false){continue;}
				element_value=e.value;
			}
			else
			{
			    if(e.name=="__VIEWSTATE"){element_value="";}
			    if(e.name=="FCKeditor1")
			    {
			        var oEditor = FCKeditorAPI.GetInstance('FCKeditor1') ;
			        element_value=oEditor.GetXHTML( true );
			    }
			    else{element_value=e.value;}
			}
			query_string+=and+e.name+'='+element_value.replace(/\&/g,"%26");
			and="&"
		}
	}
	return query_string;
}

function ajaxFormSubmit(form_obj,container)
{
    ajaxLoadPage('base-processdata.aspx',formToRequestString(form_obj),form_obj.method,container,"fck");
}


// 双击查看
function viewOnDblClick(id)
{
    var m = document.getElementById("K").value;
    var url="base-view.aspx";
    openWinows(url +"?t=Ysl_&m="+m+"&k="+ id);
}

// 删除
function del()
{
    //document.form1.v_ItemChk.value =GetChkItems()
    var k = document.getElementById("ClickID").value;
    var m = document.getElementById("K").value;
    if(k.length != 0)
    {
        if(confirm("在删除之前，请您再确认一次，您真的要删除选中的数据吗？")==true)
        {
            ExecSCommand(k,m,"",document.getElementById("MsgInfo"));
            window.location.reload(true);
        }
    }
    else
    { 
        alert("请单击选择您想要删除的数据行，然后再点击“删除”按钮。");
    }
}