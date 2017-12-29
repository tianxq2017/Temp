// *********************************************************
// 行政区划信息选择
// 杨胜灵 2009/09/30
// *********************************************************
var m_FileExt = "shtml";
// 设置并显示下一级区域信息
function SetNextArea(parentCode,parentText,currentClass,objContainer)
{
    if(parentCode!=null && parentCode.length>0)
    {
        var selectCodes = "";
        var selectTexts = "";
        var selectArea="";
        document.getElementById("txtCurrentClass").value =currentClass;
        switch (currentClass)
        {
	        case "2":
                document.getElementById("SelectCodes").innerHTML = parentCode;
                document.getElementById("SelectTexts").innerHTML = parentText;
                document.getElementById("txtSelectArea").value= parentText;
                document.getElementById("txtSelCodeI").value = parentCode;
                document.getElementById("txtSelTextI").value = parentText;
	            break
	        case "3":
	            selectCodes = document.getElementById("txtSelCodeI").value;
                selectTexts = document.getElementById("txtSelTextI").value;
                selectArea= document.getElementById("txtSelectArea").value;
                document.getElementById("SelectCodes").innerHTML = selectCodes + "@" + parentCode;
                document.getElementById("SelectTexts").innerHTML = selectTexts + " > " + parentText;
                document.getElementById("txtSelectArea").value= selectArea + parentText;
                document.getElementById("txtSelCodeII").value = selectCodes + "@" + parentCode;
                document.getElementById("txtSelTextII").value = selectTexts + " >"  + parentText;
	            break
	        case "4":
	            selectCodes = document.getElementById("txtSelCodeII").value;
                selectTexts = document.getElementById("txtSelTextII").value;                
                selectArea= document.getElementById("txtSelectArea").value;
                document.getElementById("SelectCodes").innerHTML = selectCodes + "@" + parentCode;
                document.getElementById("SelectTexts").innerHTML = selectTexts + " > " + parentText;
                document.getElementById("txtSelectArea").value=selectArea + parentText;
                document.getElementById("txtSelCodeIII").value = selectCodes + "@" + parentCode;
                document.getElementById("txtSelTextIII").value = selectTexts + " > " + parentText;
	            break
	        case "5":	  
	            selectCodes = document.getElementById("txtSelCodeIII").value;
                selectTexts = document.getElementById("txtSelTextIII").value;               
                selectArea= document.getElementById("txtSelectArea").value;                
                document.getElementById("SelectCodes").innerHTML = selectCodes + "@" + parentCode;
                document.getElementById("SelectTexts").innerHTML = selectTexts + " > " + parentText;
                document.getElementById("txtSelectArea").value= selectArea + parentText;
                document.getElementById("txtSelCodeIV").value = parentCode; // selectCodes + "@" + parentCode;
                document.getElementById("txtSelTextIV").value = parentText; // selectTexts + ">" + parentText;
	            break
	        case "6":	  
	            selectCodes = document.getElementById("txtSelCodeIII").value;
                selectTexts = document.getElementById("txtSelTextIII").value;               
                selectArea= document.getElementById("txtSelectArea").value;                
                document.getElementById("SelectCodes").innerHTML = selectCodes + "@" + parentCode;
                document.getElementById("SelectTexts").innerHTML = selectTexts + " > " + parentText;
                document.getElementById("txtSelectArea").value= selectArea + parentText;
                document.getElementById("txtSelCodeV").value = parentCode; // selectCodes + "@" + parentCode;
                document.getElementById("txtSelTextV").value = parentText; // selectTexts + ">" + parentText;
	            break
            default:
	            break;
        }
        if(parseInt(currentClass)<6) 
        {
            var urlParams = "r="+Math.random()+"&FuncNo=8888&oID="+parentCode+"&oNa="+currentClass;
           //alert(urlParams);          
            ajaxLoadPage('/userctrl/GetInnerData.aspx',urlParams,"Get",objContainer,"8888")
        }
    }
}
// 选定行政区域，进行下一步
function GotoSelectDo()
{
    var navUrl;
    var w = document.getElementById("txtW").value;
    var t = document.getElementById("txtBizType").value;
    var selectInfo = document.getElementById("txtSelectArea").value;
    // var x = document.getElementById("txtSelCodeIV").value; //县级 document.getElementById("SelectCodes").innerHTML;
     var x = document.getElementById("txtSelCodeV").value; //市\州级
    var currentClass = document.getElementById("txtCurrentClass").value;
    //alert(currentClass);  
    //if(selectInfo.length>2 && parseInt(currentClass)==5)//县
    if(selectInfo.length>2 && parseInt(currentClass)==6)//市\州级
    {
        //selectInfo = encodeURIComponent(selectInfo);
        //if(t=="0"){navUrl = "/Svrs-Biz/"+w+"-"+x+"."+m_FileExt;}
        if(t=="1"){navUrl = "/Svrs-Biz"+w+"/"+x+"."+m_FileExt;}
        else if(t=="2"){navUrl = "/Svrs-AppA/"+w+"-"+x+"."+m_FileExt;}
        else if(t=="3"){navUrl = "/Svrs-AppB/"+w+"-"+x+"."+m_FileExt;}
        else{navUrl="#";}
        window.location.href = navUrl;
    }else{alert("操作失败：请选择旗、镇、村，再按“下一步”按钮。");}
}
// 执行申请
function GotoServicesDo()
{
    var w = document.getElementById("txtW").value;
    var x = document.getElementById("txtX").value; 
    if(u.length>0 && v.length>0 && w.length>0 && x.length>0)
    {
        //var navUrl = "/Application/"+u+"/"+v+"/"+w+"/"+x+"."+m_FileExt;
        //alert(navUrl);
        //window.location.href = navUrl;
    }else{alert("操作失败：参数丢失！");return false;}
    return true;
}

// 数据展示
function SetInnerData(funcNo,returnText,objContainer)
{
    if(returnText.length>0)
    {
        switch (funcNo)
        {
            case "1.1.1":
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
    var loading_msg='Loading……'
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
	loader.open(method,url,true);
	if (method=="POST"){loader.setRequestHeader("Content-Type","application/x-www-form-urlencoded;charset=gb2312");}
	loader.onreadystatechange=function(){
		if (loader.readyState==1){container.innerHTML=loading_msg;}
		if (loader.readyState==4){
		    if(loader.status==200){SetInnerData(funcNo,loader.responseText,container);}
		    else{container.innerHTML=loader.responseText;loader = null;}
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
			if (e.type=='select-one'){element_value=e.options[e.selectedIndex].value;}
			else if (e.type=='checkbox' || e.type=='radio'){
				if (e.checked==false){continue;}
				element_value=e.value;
			}
			else{
			    if(e.name=="__VIEWSTATE"){element_value="";}
			    if(e.name=="FCKeditor1"){var oEditor = FCKeditorAPI.GetInstance('FCKeditor1');element_value=oEditor.GetXHTML( true );}
			    else{element_value=e.value;}
			}
			query_string+=and+e.name+'='+element_value.replace(/\&/g,"%26");
			and="&"
		}
	}
	return query_string;
}