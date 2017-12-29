/*common.js  
  by 2013.12.25/txq
*/
/*检查身份证号是否重复*/
function CheckUserCardID(UserCID)
{
   var JsonData={type:"CheckUserCardID",value01:encodeURIComponent(UserCID)};
   $.getJSON(
   "/userctrl/GetAjax.aspx",
   JsonData,
   function(dataObj){
     $("#divMsg1").html(dataObj.error);      
   }
   ); 
}
/*检查用户名是否重复*/
function CheckUserRegName(UserName)
{
   var JsonData={type:"CheckUserRegName",value01:encodeURIComponent(UserName)};
   $.getJSON(
   "/userctrl/GetAjax.aspx",
   JsonData,
   function(dataObj){
     $("#divMsg").html(dataObj.error);      
   }
   ); 
}
/*验证输入手机号 并发送短信动态码*/
function SendCheckCode(UserMobile){

    if(UserMobile ==''||UserMobile.length == null||UserMobile == null)
    { alert("请输入手机号码"); }
    else
    {   
        var userAccount=document.getElementById("txtUserName").value;
        var UserPwd=document.getElementById("txtUserPwd").value;
        if(userAccount ==''||userAccount.length == null||userAccount == null)
        {alert("请输入姓名");return false; }
         if(UserPwd ==''||UserPwd.length == null||UserPwd == null)
        {alert("请输入密码");return false; }
       
        if(!CheckFilter(document.getElementById("txtUserMobile"), "手机号码","1")){return false;}
        else{
            var JsonData={type:"SendCheckCode",value01:encodeURIComponent(UserMobile)};
            $.getJSON(
                "/userctrl/GetAjax.aspx",
                JsonData,
                function(dataObj){   
                    if(!dataObj.isError)
                    { alert(dataObj.data);SendTime();}
                    else
                    {alert(dataObj.error);} 
                }
            );
        }
    } 
}
/*验证输入手机号 并发送短信动态码*/
function SendCheckCodeD(UserMobile){  
    if(UserMobile ==''||UserMobile.length == null||UserMobile == null)
    { alert("请输入手机号码"); }
    else
    {  
        if(!CheckFilter(document.getElementById("txtUserMobile"), "手机号码","1")){return false;}
      
        var JsonData={type:"SendCheckCodeD",value01:encodeURIComponent(UserMobile)};
        $.getJSON("/userctrl/GetAjax.aspx",JsonData,
         function(dataObj){ 
            if(!dataObj.isError){ alert(dataObj.data);SendTime(); }           
            else{alert(dataObj.error);}
          });
         
    } 
}
/*发送短信动态码 倒计时*/
function SendTime(){
    //提交操作
    var btn = document.getElementById("ButSend")
    btn.disabled=true
    c(600)
}
function c(i){
    var btn = document.getElementById("ButSend")
    i--;
    if(i==0){
        btn.value = "发送短信";
        btn.disabled=false;
    }
    else{
        btn.value = "发送短信("+i+")";
        setTimeout("c("+i+")",1000);
    }
}
/*免费发送到手机*/
function SendMsg(msgType,servID){
        
    var JsonData={type:"SendMsg",value01:encodeURIComponent(msgType),value02:encodeURIComponent(servID)};
    jQuery.getJSON(
        "/userctrl/GetAjax.aspx",
        JsonData,
        function(dataObj){   
            if(!dataObj.isError)
            { alert(dataObj.data);}
            else{if(dataObj.error=="请您先登录!"){location.href="/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh.shtml";}
                else{ alert(dataObj.error);}}
        }
    );        
}
/*curObj:document.getElementById("xx");
  msg:xx；
  type：0字符串；1数字
*/
 function CheckElem(curObj, msg,type){
    if(msg==null) msg="";
    alert(curObj);
    if(curObj.value==''){
        alert(msg + "不可为空!");
        curObj.focus();
        return false;
    }
    else {
        if(type==0&&isNaN(curObj.value)){
            alert(msg + "必须为数字!");
            curObj.focus();
            return false;
        }
        else{return true;}      
    }
}

//搜藏
function AddFavorites(objName,objLink,objID,objType)
{
   var JsonData={type:"AddFavorites",value01:encodeURIComponent(objName),value02:encodeURIComponent(objLink),value03:encodeURIComponent(objID),value04:encodeURIComponent(objType)};
   jQuery.getJSON("/userctrl/GetAjax.aspx",JsonData,
             function(dataObj){ 
                if(!dataObj.isError){alert("添加成功!");}
                else{if(dataObj.error=="请您先登录!"){location.href="/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh.shtml";}
                     else{ alert(dataObj.error);}}
                 }
            );
}

/*证件 全选*/
 // 兼容IE FF的ByName方法
var getElementsByName = function(tag, name){
    var returns = document.getElementsByName(name);
    if(returns.length > 0) return returns;
    returns = new Array();
    var e = document.getElementsByTagName(tag);
    for(var i = 0; i < e.length; i++){
       if(e[i].getAttribute("name") == name){
       returns[returns.length] = e[i];
       }
    }
    return returns;
}
//alert(getElementsByName("div","CollapsiblePanels").length); // IE:4 FF:4
function SelectIdList()
{ 
  var IDs="";
  var CheckboxId=document.getElementsByName("CommID");
  for(var i=0;i<CheckboxId.length;i++)
  {
     if(CheckboxId[i].checked)
     {
        if(IDs=="")
        {
           IDs=CheckboxId[i].value;
        }
        else
        {      
           IDs=IDs+','+CheckboxId[i].value;
        }
     }
  }
  document.getElementById("txtCommIDs").value=IDs;
}
//全选或全不不选按钮
function CheckAlls(o)
{
  var CheckboxId=document.getElementsByName("CommID");
  for(var i=0;i<CheckboxId.length;i++)
  {
       if(o.checked)
       {  CheckboxId[i].checked=true;}
       else
       { CheckboxId[i].checked=false;}
  }
}
function BtnDel(commIDs,delType)
{//delType:0 证件；1 关注；
    if(commIDs=="")
    {SelectIdList();
    commIDs=document.getElementById("txtCommIDs").value; }
    if(commIDs ==''||commIDs == null) {alert('请选择信息'); return; }
    else
    { 
      var JsonData={type:"UserDel",value01:encodeURIComponent(commIDs),value02:encodeURIComponent(delType)};
       jQuery.getJSON("/userctrl/GetAjax.aspx",JsonData,
             function(dataObj){ 
                if(!dataObj.isError){window.location.reload();}
                else{if(dataObj.error=="请您先登录!"){location.href="/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh.shtml";}
                     else{ alert(dataObj.error);}}
                 });
   }
}
/*申请查看*/
 function ShowTracks(id,type){
        var tracks=document.getElementById("tr_Tracks"+id);
        var apply=document.getElementById("tr_Apply"+id);
        var img=document.getElementById("img_Tracks"+id);     
        var style="";        
        if (type == "1") { style = " fail_on"; }
        else{ style = ""; }
        
        if(tracks.className=="down"||tracks.className=="down"){ 
            tracks.className="down down_on"+style;
            apply.className="list list_on"+style;
            img.src="/images/user/user_12_t.gif";
        }
        else{
            tracks.className="down";        
            apply.className="list";
            img.src="/images/user/user_12_b.gif";
        }
}
//验证当前输入多少个字符
function textCounter(obj, showid, maxlimit) {
    var len =obj.value.length;
    var showobj = document.getElementById(showid);
    if(len > maxlimit) {
       obj.value = getStrbylen(obj.value, maxlimit);
       showobj.innerHTML = '0';
    } else {
       showobj.innerHTML = maxlimit - len;
    }
    if(maxlimit - len > 0) {
       showobj.parentNode.style.color = "";
    } else {
       showobj.parentNode.style.color = "red";
    }

}
function CheckFilter(curObj,msg,type){
    var filter;
    if(type=="0"){filter=/^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$|(^(13[0-9]|15[0-9]|18[0-9])\d{8}$)/;}
    else if(type=="1"){filter=/^(13[0-9]|15[0-9]|18[0-9])\d{8}$/;}
    else if(type=="2"){filter=/^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$/;}
    else if(type=="3"){filter=/^\s*([A-Za-z0-9_-]+(\.\w+)*@(\w+\.)+\w{2,5})\s*$/;}  
    else if(type=="4"){filter=/\d{6}/;}
    else{alert("请输入正确的type属性"); return false; }    
    
    if (!filter.test(curObj.value))   
    { alert("请输入正确的"+msg); return false;  }
    else{return true;}
}
//=====================业务办理相关=================================
/*农转非日期*/
function ShowRuralDate(selectObj,objID){
    var objVal=document.getElementById(selectObj).value;
    if(objVal=='农转城'){document.getElementById(objID).style.display = "";}
    else{document.getElementById(objID).style.display = "none";}
}
/*type:0 男；1 女；2 年龄*/
function CheckCID(type,objCid){
    var cid=document.getElementById(objCid).value;
    var obj;
    if(cid!=''&&cid!=null){
        if(type=='0'){
            obj=cid.substr(16,1);
            if(parseInt(obj)%2!=1){alert('男方身份证号与性别不符');document.getElementById(objCid).value='';return;}
        }
        else if(type=='1'){
            obj=cid.substr(16,1);
            if(parseInt(obj)%2!=0){alert('女方身份证号与性别不符');document.getElementById(objCid).value='';return;}
        }
        else if(type=='2'){
            var myDate = new Date();
            var month = myDate.getMonth() + 1;
            var day = myDate.getDate();

            var age = myDate.getFullYear() - cid.substring(6, 10) - 1;
            if (cid.substring(10, 12) < month || cid.substring(10, 12) == month && cid.substring(12, 14) <= day) {
                age++;
            } 
            if(parseInt(age)<18||parseInt(age)>49){alert('申请人年龄必须在18-49之间');document.getElementById(objCid).value='';return;}
        }
        else{}       
     }
}

/*根据选择婚姻状况特殊展示*/
function ShowMarry(){
    var MarryType=document.getElementById("ddlMarryType").value;
    document.getElementById("spanMarryTypeCN").innerHTML=MarryType;
    if(MarryType=='未婚'){       
         document.getElementById("panelMarry").style.display = "none";
         document.getElementById("panelBirth").style.display = "none";      
    }
    else{
         document.getElementById("panelMarry").style.display = "";
         document.getElementById("panelBirth").style.display = "";         
    } 
}
function ShowMarry0108(){
    var MarryType=document.getElementById("ddlMarryTypeA").value;
    if(MarryType=='初婚'||MarryType=='再婚'){ document.getElementById("panelPeiou").style.display = "";}   
    else{document.getElementById("panelPeiou").style.display = "none";}    
}
function ShowMarry0109(){
    var MarryType=document.getElementById("ddlMarryTypeA").value;
    if(MarryType=='初婚'||MarryType=='再婚'){ document.getElementById("panelPeiou").style.display = ""; }
    else{  document.getElementById("panelPeiou").style.display = "none";} 
}
function ShowMarry0110(){
    var MarryType=document.getElementById("ddlMarryTypeA").value;
    if(MarryType=='初婚'||MarryType=='再婚'){ document.getElementById("panelPeiou").style.display = ""; }
    else{  document.getElementById("panelPeiou").style.display = "none";} 
} 
/*根据选择生育情况特殊展示*/
function ShowBirthType(){
    var BirthType=document.getElementById("ddlBirthType").value;
    if(BirthType=='未生育'){       
        document.getElementById("panelBirthInfo").style.display = "none";
        document.getElementById("panelChildren").style.display = "none";
    }
    else{       
        document.getElementById("panelBirthInfo").style.display = "";
        document.getElementById("panelChildren").style.display = "";
    }  
} 
function ShowBirthType0109(){
    var BirthType=document.getElementById("ddlBirthType").value;
    if(BirthType=='未生育'){  document.getElementById("panelChildren").style.display = "none";}
    else{ document.getElementById("panelChildren").style.display = "";}     
}
/*根据选择婚现有家庭子女数量特殊展示*/
  function ShowBirth(){
    var BirthNum=document.getElementById("ddlBirthNum").value;    
    if(BirthNum>0){document.getElementById("panelChildren").style.display = "";}   
    switch (BirthNum)
    {
        case '1':
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "none";
            document.getElementById("tableChild3").style.display = "none";
            document.getElementById("tableChild4").style.display = "none";
            document.getElementById("tableChild5").style.display = "none";
            document.getElementById("tableChild6").style.display = "none";
            break
        case '2':
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "none";
            document.getElementById("tableChild4").style.display = "none";
            document.getElementById("tableChild5").style.display = "none";
            document.getElementById("tableChild6").style.display = "none";
            break
        case '3':
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "";
            document.getElementById("tableChild4").style.display = "none";
            document.getElementById("tableChild5").style.display = "none";
            document.getElementById("tableChild6").style.display = "none";
            break
        case '4':
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "";
            document.getElementById("tableChild4").style.display = "";
            document.getElementById("tableChild5").style.display = "none";
            document.getElementById("tableChild6").style.display = "none";
            break
        case '5':
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "";
            document.getElementById("tableChild4").style.display = "";
            document.getElementById("tableChild5").style.display = "";
            document.getElementById("tableChild6").style.display = "none";
            break
        case '6':
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "";
            document.getElementById("tableChild4").style.display = "";
            document.getElementById("tableChild5").style.display = "";
            document.getElementById("tableChild6").style.display = "";
            break
        default:
            document.getElementById("panelChildren").style.display = "none";
            break;
    }
}  

//以下是：各业务根据办证人的身份证号获取其相关信息
/* GetPersonsInfo(身份证号,性别：0,单;1,双) */
function GetPersonsInfo(personCardID,objType)
{
    if(personCardID==""||personCardID==null||personCardID.length!=18)
    {alert('请输入正确的身份证号码！'); return;}
    if(bizCode==""||bizCode==null)
    {alert('没有指定业务类型！'); return;}
    if(objType=="1"){
        if(document.getElementById("txtPersonCidA").value==document.getElementById("txtPersonCidB").value){alert('夫妻双方身份证号填写重复！'); return;}
    }   
}

//同步户籍地和现居住地
function AreaCodeTB(AreaCodeP,AreaNameP,AreaCodeS,AreaNameS)
{
    document.getElementById(AreaCodeS).value=document.getElementById(AreaCodeP).value;
    document.getElementById(AreaNameS).value=document.getElementById(AreaNameP).value;
}
//根据婚姻情况同步男女婚姻时间
function MarryDateTB(MarryTypeP,MarryDateP,MarryTypeS,MarryDateS)
{
    var MarryTypeValS= document.getElementById(MarryTypeS).value;
    //if(MarryTypeValS=="初婚"||MarryTypeValS=="再婚"){
//            var MarryTypeValP= document.getElementById(MarryTypeP).value;
//            if(MarryTypeValS==MarryTypeValP) {
                document.getElementById(MarryDateS).value=document.getElementById(MarryDateP).value;
            //}            
          
    //}    
}
/*一孩*/
function ShowMarry0101(){
    var MarryType=document.getElementById("ddlMarryTypeB").value;
    if(MarryType=='初婚'||MarryType=='再婚'){ document.getElementById("panelPeiou").style.display = "";}   
    else{document.getElementById("panelPeiou").style.display = "none";}    
    if(MarryType=='未婚'){document.getElementById("panelMarry").style.display = "none";}
    else{document.getElementById("panelMarry").style.display = "";}
}
 function ShowBirth0101(){
    var BirthNum=document.getElementById("ddlBirthNum").value;    
    if(BirthNum>0){document.getElementById("panelChildren").style.display = "";}   
    switch (parseInt(BirthNum))
    {
        case 1:
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "none";
            document.getElementById("tableChild3").style.display = "none";
            break
        case 2:
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "none";
            break
        case 3:
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "";
            break       
        default:
            document.getElementById("panelChildren").style.display = "none";
            break;
    }
}

/*奖励扶助*/
function CheckCID0103(objCid){
    var cid=document.getElementById(objCid).value;
    var obj=cid.substr(16,1);    
    if(cid!=''&&cid!=null){
        if(parseInt(obj)%2!=0){}
        else{
            var myDate = new Date();
            var month = myDate.getMonth() + 1;
            var day = myDate.getDate();
            
            var age = myDate.getFullYear() - cid.substring(6, 10) - 1;
            if (cid.substring(10, 12) < month || cid.substring(10, 12) == month && cid.substring(12, 14) <= day) {
                age++;
            } 
            if(parseInt(age)<59){alert('申请人必须年满59周岁才可以申请！');document.getElementById(objCid).value='';return;}
        }
    }
}
function ShowMarry0103(){
    var MarryType=document.getElementById("ddlMarryTypeA").value;

    if(MarryType=='初婚'||MarryType=='再婚'){ document.getElementById("panelPeiou").style.display = "";}   
    else{document.getElementById("panelPeiou").style.display = "none";}    
    if(MarryType=='未婚'){document.getElementById("panelMarry").style.display = "none";}
    else{document.getElementById("panelMarry").style.display = "";}
}
function ShowBirth0103(){
    var BirthNumNan=document.getElementById("ddlFileds20").value;    
    var BirthNumNv=document.getElementById("ddlFileds21").value;  
    document.getElementById("ddlFileds37").value=BirthNumNan;
    document.getElementById("ddlFileds38").value=BirthNumNv;
    
    var BirthNum=parseInt(BirthNumNan)+parseInt(BirthNumNv);    
    if(BirthNum>6){alert('曾生育子女数总和不能超过6个，否则请咨询申请！'); return;}
    if(BirthNum>0){document.getElementById("panelChildren").style.display = ""; }  
    
    switch (parseInt(BirthNum))
    {
        case 1:
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "none";
            document.getElementById("tableChild3").style.display = "none";
            document.getElementById("tableChild4").style.display = "none";
            document.getElementById("tableChild5").style.display = "none";
            document.getElementById("tableChild6").style.display = "none";
            break
        case 2:   
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "none";
            document.getElementById("tableChild4").style.display = "none";
            document.getElementById("tableChild5").style.display = "none";
            document.getElementById("tableChild6").style.display = "none";
            break
        case 3:
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "";
            document.getElementById("tableChild4").style.display = "none";
            document.getElementById("tableChild5").style.display = "none";
            document.getElementById("tableChild6").style.display = "none";
            break
        case 4:
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "";
            document.getElementById("tableChild4").style.display = "";
            document.getElementById("tableChild5").style.display = "none";
            document.getElementById("tableChild6").style.display = "none";
            break
        case 5:
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "";
            document.getElementById("tableChild4").style.display = "";
            document.getElementById("tableChild5").style.display = "";
            document.getElementById("tableChild6").style.display = "none";
            break
        case 6:
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "";
            document.getElementById("tableChild4").style.display = "";
            document.getElementById("tableChild5").style.display = "";
            document.getElementById("tableChild6").style.display = "";
            break
        default:
            document.getElementById("panelChildren").style.display = "none";
            break;
    }
} 
/*特别奖励扶助*/
function CheckCID0104(objCid){
    var cid=document.getElementById(objCid).value;
    var obj=cid.substr(16,1);
    if(cid!=''&&cid!=null){
        if(parseInt(obj)%2!=0){}
        else{
            var myDate = new Date();
            var month = myDate.getMonth() + 1;
            var day = myDate.getDate();
            
            var age = myDate.getFullYear() - cid.substring(6, 10) - 1;
            if (cid.substring(10, 12) < month || cid.substring(10, 12) == month && cid.substring(12, 14) <= day) {
                age++;
            } 
            if(parseInt(age)<49){alert('女方必须年满49周岁才可以申请！');document.getElementById(objCid).value='';return;}
        }
    }
}
function ShowBirthCH0104(){
    var BirthNumCHNan=document.getElementById("ddlFileds37").value;    
    var BirthNumCHNv=document.getElementById("ddlFileds38").value;  
    
    var BirthNumCH=parseInt(BirthNumCHNan)+parseInt(BirthNumCHNv);    
   
    if(BirthNumCH>0){document.getElementById("panelShangCan").style.display = ""; } 
    else{document.getElementById("panelShangCan").style.display = "none"; }   
}
/*少生快富*/
function ShowBirth0105(){
    var BirthNumNan=document.getElementById("ddlFileds37").value;    
    var BirthNumNv=document.getElementById("ddlFileds38").value;  
    
    var BirthNum=parseInt(BirthNumNan)+parseInt(BirthNumNv);    
    if(BirthNum>4){alert('曾生育子女数总和不能超过4个，否则请咨询申请！'); return;}
    if(BirthNum>0){document.getElementById("panelChildren").style.display = ""; }  
    
    switch (parseInt(BirthNum))
    {
        case 1:
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "none";
            document.getElementById("tableChild3").style.display = "none";
            document.getElementById("tableChild4").style.display = "none";
            break
        case 2:   
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "none";
            document.getElementById("tableChild4").style.display = "none";
            break
        case 3:
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "";
            document.getElementById("tableChild4").style.display = "none";
            break
        case 4:
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "";
            document.getElementById("tableChild4").style.display = "";
            break
        default:
            document.getElementById("panelChildren").style.display = "none";
            break;
    }
} 
function ShowFMName(){
    var NameB = document.getElementById("txtPersonNameB").value;
    var NameA = document.getElementById("txtPersonNameA").value;
    document.getElementById("txtFatherName1").value=NameA;
    document.getElementById("txtFatherName2").value=NameA;
    document.getElementById("txtFatherName3").value=NameA;
    document.getElementById("txtMotherName1").value=NameB;
    document.getElementById("txtMotherName2").value=NameB;
    document.getElementById("txtMotherName3").value=NameB;
}