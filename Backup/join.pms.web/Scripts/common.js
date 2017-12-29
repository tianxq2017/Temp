/*common.js  
  by 2013.12.25/txq
*/
document.onkeypress = function(){  
    if(event.keyCode==13)  
    {  
        event.keyCode=0;  
        event.cancelBubble   =   true  
        event.returnValue   =   false;  
        return   false;  
    }  
};
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
//=====================业务办理相关=================================

function ShowMarryCid(CidDiv,CidValue){
    if(CidValue!='1'){
            //document.getElementById(CidDiv).style.display = "none";
    }
    else{
            //document.getElementById(CidDiv).style.display = "";
    }  
} 

/*农转非日期*/
function ShowRuralDate(selectObj,objID){
    var objVal=document.getElementById(selectObj).value;
    if(objVal=='农转城'){document.getElementById(objID).style.display = "";}
    else{document.getElementById(objID).style.display = "none";}
}
/*type:0 男；1 女；2 年龄*/

function CheckCID_1(CIDtype,type,objCid){
    //1为身份证
    var CIDt=document.getElementById(CIDtype).value;
    //alert(CIDtype);
    if(CIDt=='1'){
       CheckCID(type,objCid);
    }
}

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
        else if(type=='3'){
            var myDate = new Date();
            var month = myDate.getMonth() + 1;
            var day = myDate.getDate();

           var sex=cid.substr(16,1);
            if(parseInt(sex)%2!=0){alert('请输入女性身份证号');document.getElementById(objCid).value='';return;}
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
    if(MarryType=='未婚' || MarryType=='离婚' || MarryType=='丧偶')
    {       
         document.getElementById("panelMarry").style.display = "none";
         document.getElementById("panelBirth").style.display = "none";      
    }
    else{
         document.getElementById("panelMarry").style.display = "";
         document.getElementById("panelBirth").style.display = "";         
    } 
}
/*独生子女 根据选择婚姻状况特殊展示配偶信息*/
function ShowMarry0108(){
    var MarryType=document.getElementById("ddlMarryTypeA").value;
    if(MarryType=='初婚'||MarryType=='再婚'||MarryType=='复婚'){ document.getElementById("panelPeiou").style.display = "";}   
    else{document.getElementById("panelPeiou").style.display = "none";}
}
function ShowMarry0109(){
    var MarryType=document.getElementById("ddlMarryTypeA").value;
    if(MarryType=='初婚'||MarryType=='再婚'||MarryType=='复婚'){ document.getElementById("panelPeiou").style.display = ""; }
    else{  document.getElementById("panelPeiou").style.display = "none";} 
}
function ShowMarry0110(){
    var MarryType=document.getElementById("ddlMarryTypeA").value;
    if(MarryType=='初婚'||MarryType=='再婚'||MarryType=='复婚'){ document.getElementById("panelPeiou").style.display = ""; }
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
function ShowBirth0109(){ 
    var BirthNum=document.getElementById("ddlBirthNum").value;//现有子女个数 
    if(parseInt(BirthNum)>0){document.getElementById("panelChildren").style.display = ""; }
    else{document.getElementById("panelChildren").style.display = "none";}
}

//以下是：各业务根据办证人的身份证号获取其相关信息

/* GetPersonsInfo(身份证号,业务编号,性别：0,单;1,双) */
function GetPersonsInfo(personCardID,bizCode,objType)
{
    if(personCardID==""||personCardID==null||personCardID.length<5)
    {alert('请输入正确的身份证号码！'); return;}
    if(bizCode==""||bizCode==null)
    {alert('没有指定业务类型！'); return;}
    if(objType=="1"){
        if(document.getElementById("txtPersonCidA").value==document.getElementById("txtPersonCidB").value){alert('男女双方身份证号填写重复！'); return;}
    }
    var JsonData={type:"GetPersonsInfo",value01:encodeURIComponent(personCardID)};
    jQuery.getJSON("/userctrl/GetAjax.aspx",JsonData,
         function(dataObj){ 
            if(!dataObj.isError){      
                if(dataObj.data[4]!=""&&dataObj.data[4]!=null){
                    SetPersonsInfo(dataObj.data,bizCode,objType);
                }      
            }
            else{if(dataObj.error=="请您先登录！"){location.href="/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh.shtml";}
                 else{ alert(dataObj.error);}}
             });
}
/* GetFwzInfo(服务证号,业务编号) */
function GetFwzInfo(fwzhCard,bizCodeb,bizCodea,objType)
{
    if(fwzhCard==""||fwzhCard==null||fwzhCard.length<5)
    {alert('请输入正确的服务证号！'); return;}
    //if(bizCodeb==""||bizCodeb==null)
    //{alert('没有指定业务类型！'); return;}  
    var JsonData={type:"GetFwzInfo",value01:encodeURIComponent(fwzhCard)};
    jQuery.getJSON("/userctrl/GetAjax.aspx",JsonData,
         function(dataObj){ 
            if(!dataObj.isError){  
                document.getElementById("txtfwzh").value=dataObj.data[4];
                if(dataObj.data[0]!=""&&dataObj.data[0]!=null&&bizCodeb!=""){
                 //女方信息
                    document.getElementById("txtPersonCidB").value=dataObj.data[0];
                    GetPersonsInfo(dataObj.data[0],bizCodeb,objType)
                }      
                if(dataObj.data[2]!=""&&dataObj.data[2]!=null&&bizCodea!=""){
                //男方信息
                    document.getElementById("txtPersonCidA").value=dataObj.data[2];
                    GetPersonsInfo(dataObj.data[2],bizCodea,objType)
                } 
            }
            else{if(dataObj.error=="请您先登录！"){location.href="/OqZXiaLfhvEzcqI7c58ZyE79ieaIWaOhqAV9enaGYJLngfoBOn8dQS6kIzPdZdjh.shtml";}
                 else{ alert(dataObj.error);}}
             });
}
/*根据业务类型 设置办证人基础信息
SetPersonsInfo()
*/
function SetPersonsInfo(personsInfo,bizCode,objType)
{
    var MarryType;
    if(personsInfo.length!=23){alert('获取信息出错，请联系系统管理员！'); return;}
    switch(bizCode)
    { 
        case "0150A":/*一孩 A*/  
             document.getElementById("txtPersonCidA").value=personsInfo[4];
            if(document.getElementById("txtContactTelA").value==""){
             document.getElementById("txtContactTelA").value=personsInfo[1];
             }
             document.getElementById("txtPersonNameA").value=personsInfo[3];          
             document.getElementById("txtNationsA").value=personsInfo[11];           
             document.getElementById("txtWorkUnitA").value=personsInfo[12];
             document.getElementById("ddlRegisterTypeA").value=personsInfo[20];
             document.getElementById("UcAreaSelRegA_txtSelAreaCode").value=personsInfo[14];
             document.getElementById("UcAreaSelRegA_txtSelectArea").value=personsInfo[15];//.substr(0, personsInfo[15].indexOf('&'));
             //document.getElementById("UcAreaSelRegA_txtArea").value=personsInfo[15].split('&')[1];
             document.getElementById("UcAreaSelCurA_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurA_txtSelectArea").value=personsInfo[17];//.substr(0, personsInfo[17].indexOf('&'));
             //document.getElementById("UcAreaSelCurA_txtArea").value=personsInfo[17].split('&')[1]; 
             document.getElementById("ddlMarryTypeA").value=personsInfo[7]; 
             //配偶信息库不全时
            if(document.getElementById("txtNationsA").value==""){
             document.getElementById("txtNationsA").value=document.getElementById("txtNationsB").value;
             }
            if(document.getElementById("ddlMarryTypeA").value==""){
             document.getElementById("ddlMarryTypeA").value=document.getElementById("ddlMarryTypeB").value;
             }
            if(document.getElementById("txtWorkUnitA").value==""){
             document.getElementById("txtWorkUnitA").value=document.getElementById("txtWorkUnitB").value;
             }
            if(document.getElementById("UcAreaSelRegA_txtSelAreaCode").value==""){
             document.getElementById("UcAreaSelRegA_txtSelAreaCode").value=document.getElementById("UcAreaSelRegB_txtSelAreaCode").value;
             }
            if(document.getElementById("UcAreaSelRegA_txtSelectArea").value==""){
             document.getElementById("UcAreaSelRegA_txtSelectArea").value=document.getElementById("UcAreaSelRegB_txtSelectArea").value;
             }
            if(document.getElementById("UcAreaSelRegA_txtArea").value==""){
             document.getElementById("UcAreaSelRegA_txtArea").value=document.getElementById("UcAreaSelRegB_txtArea").value;
             }
             
            if(document.getElementById("UcAreaSelCurA_txtSelAreaCode").value==""){
             document.getElementById("UcAreaSelCurA_txtSelAreaCode").value=document.getElementById("UcAreaSelCurB_txtSelAreaCode").value;
             }
            if(document.getElementById("UcAreaSelCurA_txtSelectArea").value==""){
             document.getElementById("UcAreaSelCurA_txtSelectArea").value=document.getElementById("UcAreaSelCurB_txtSelectArea").value;
             }
            if(document.getElementById("UcAreaSelCurA_txtArea").value==""){
             document.getElementById("UcAreaSelCurA_txtArea").value=document.getElementById("UcAreaSelCurB_txtArea").value;
             }   
                             
             if(document.getElementById("ddlFileds47").value==""){
             document.getElementById("ddlFileds47").value=personsInfo[21];
             }                
             if(document.getElementById("txtFileds14").value==""){
             document.getElementById("txtFileds14").value=personsInfo[8]; 
             }
             if((personsInfo[19]!="" || personsInfo[19]!=null) && document.getElementById("txtPersonCidB").value==""){
             GetPersonsInfo(personsInfo[19],'0150B','1');
             }  
            break;             
         case "0150B":/*B*/
             document.getElementById("txtPersonCidB").value=personsInfo[4];
             document.getElementById("txtContactTelA").value=personsInfo[1];
             document.getElementById("txtPersonNameB").value=personsInfo[3];  
             document.getElementById("txtNationsB").value=personsInfo[11];
             document.getElementById("txtWorkUnitB").value=personsInfo[12];   
             document.getElementById("ddlRegisterTypeB").value=personsInfo[20];  
             document.getElementById("UcAreaSelRegB_txtSelAreaCode").value=personsInfo[14];
             document.getElementById("UcAreaSelRegB_txtSelectArea").value=personsInfo[15];//.substr(0, personsInfo[15].indexOf('&'));
             //document.getElementById("UcAreaSelRegB_txtArea").value=personsInfo[15].split('&')[1];
             document.getElementById("UcAreaSelCurB_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurB_txtSelectArea").value=personsInfo[17];//.substr(0, personsInfo[17].indexOf('&'));
             //document.getElementById("UcAreaSelCurB_txtArea").value=personsInfo[17].split('&')[1];   
             document.getElementById("ddlMarryTypeB").value=personsInfo[7];                  
             document.getElementById("txtPersonNameA").value=personsInfo[18];           
             if(document.getElementById("ddlFileds47").value==""){
             document.getElementById("ddlFileds47").value=personsInfo[21]; 
             }           
             if(document.getElementById("txtFileds14").value==""){
             document.getElementById("txtFileds14").value=personsInfo[8]; 
             }
             if((personsInfo[19]!="" || personsInfo[19]!=null) && document.getElementById("txtPersonCidA").value==""){
             GetPersonsInfo(personsInfo[19],'0150A','1');
             }
            break;
        case "0101A":/*一孩 A*/  
             document.getElementById("txtContactTelA").value=personsInfo[1];
             document.getElementById("txtPersonNameA").value=personsInfo[3];  
             MarryType=personsInfo[7];         
             if(MarryType==""||MarryType==null)
             {
                MarryType="初婚";
             }

             document.getElementById("ddlMarryTypeA").value=MarryType;  
             try{   
             document.getElementById("txtMarryDateA").value=personsInfo[8];    
       }catch(e){}
             document.getElementById("txtNationsA").value=personsInfo[11];           
             document.getElementById("ddlRegisterTypeA").value=personsInfo[20];
             document.getElementById("txtWorkUnitA").value=personsInfo[12];
             document.getElementById("UcAreaSelCurA_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurA_txtSelectArea").value=personsInfo[17];            
            break;             
         case "0101B":/*一孩B*/
         try{
             document.getElementById("txtContactTelB").value=personsInfo[1];
             document.getElementById("txtMarryDateB").value=personsInfo[8];
             }catch(e){}      
             document.getElementById("txtPersonNameB").value=personsInfo[3];       
             MarryType=personsInfo[7];     
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryTypeB").value=MarryType;
             
             document.getElementById("txtNationsB").value=personsInfo[11];
             document.getElementById("ddlRegisterTypeB").value=personsInfo[20];
             document.getElementById("txtWorkUnitB").value=personsInfo[12];     
             document.getElementById("UcAreaSelCurB_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurB_txtSelectArea").value=personsInfo[17];                
             document.getElementById("txtPersonNameA").value=personsInfo[18];
             document.getElementById("txtPersonCidA").value=personsInfo[19];  
             GetPersonsInfo(personsInfo[19],'0101A','1');              
              ShowMarry0101();
              ShowBirth0101();     
              
            break;         
       case "0102A":/*二孩 A*/ 
             document.getElementById("txtContactTelA").value=personsInfo[1];
             document.getElementById("txtPersonNameA").value=personsInfo[3];  
             MarryType=personsInfo[7];         
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryTypeA").value=MarryType;     
             document.getElementById("txtMarryDateA").value=personsInfo[8];           
             document.getElementById("txtNationsA").value=personsInfo[11];           
             document.getElementById("ddlRegisterTypeA").value=personsInfo[20];
             document.getElementById("txtWorkUnitA").value=personsInfo[12];
             document.getElementById("UcAreaSelCurA_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurA_txtSelectArea").value=personsInfo[17];            
            break; 
      case "0102B":/*二孩B*/
             document.getElementById("txtContactTelB").value=personsInfo[1];
             document.getElementById("txtPersonNameB").value=personsInfo[3];       
             MarryType=personsInfo[7];     
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryType").value=MarryType;
             document.getElementById("txtMarryDate").value=personsInfo[8];
             document.getElementById("txtNationsB").value=personsInfo[11];
             document.getElementById("ddlRegisterTypeB").value=personsInfo[20];
             document.getElementById("txtWorkUnitB").value=personsInfo[12];     
             document.getElementById("UcAreaSelCurB_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurB_txtSelectArea").value=personsInfo[17];                
             document.getElementById("txtPersonNameA").value=personsInfo[18];
             document.getElementById("txtPersonCidA").value=personsInfo[19];           
             ShowBirth0101();
            break;   
       case "0103A":/*奖励扶助 A*/  
             document.getElementById("txtContactTelA").value=personsInfo[1];
             document.getElementById("txtPersonNameA").value=personsInfo[3];  
             MarryType=personsInfo[7];         
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryTypeA").value=MarryType;     
             document.getElementById("txtMarryDateA").value=personsInfo[8];          
             document.getElementById("txtNationsA").value=personsInfo[11];     
             document.getElementById("ddlRegisterTypeA").value=personsInfo[20];               
             document.getElementById("txtPersonNameB").value=personsInfo[18];
             document.getElementById("txtPersonCidB").value=personsInfo[19];           
              ShowMarry0103();ShowBirth0103();                 
            break;             
         case "0103B":/*奖励扶助B*/
             document.getElementById("txtContactTelB").value=personsInfo[1];
             document.getElementById("txtPersonNameB").value=personsInfo[3];       
             MarryType=personsInfo[7];     
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryTypeB").value=MarryType;
             document.getElementById("txtMarryDateB").value=personsInfo[8];        
             document.getElementById("txtNationsB").value=personsInfo[11];    
             document.getElementById("ddlRegisterTypeB").value=personsInfo[20];           
            break;  
       case "0104A":/*特别扶助 A*/  
             document.getElementById("txtContactTelA").value=personsInfo[1];
             document.getElementById("txtPersonNameA").value=personsInfo[3];  
             MarryType=personsInfo[7];         
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryTypeA").value=MarryType;     
             document.getElementById("txtMarryDateA").value=personsInfo[8];   
             document.getElementById("ddlRegisterTypeA").value=personsInfo[20];               
             document.getElementById("txtPersonNameB").value=personsInfo[18];
             document.getElementById("txtPersonCidB").value=personsInfo[19];           
              ShowMarry0103();ShowBirth0103();                 
            break;             
         case "0104B":/*特别扶助B*/
             document.getElementById("txtContactTelB").value=personsInfo[1];
             document.getElementById("txtPersonNameB").value=personsInfo[3];       
             MarryType=personsInfo[7];     
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryTypeB").value=MarryType;
             document.getElementById("txtMarryDateB").value=personsInfo[8];
             document.getElementById("ddlRegisterTypeB").value=personsInfo[20];           
            break; 
       case "0105A":/*少生快富 A*/  
             document.getElementById("txtPersonNameA").value=personsInfo[3];           
             document.getElementById("txtNationsA").value=personsInfo[11];    
             MarryType=personsInfo[7];         
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryTypeA").value=MarryType;     
             document.getElementById("txtMarryDateA").value=personsInfo[8];   
             document.getElementById("ddlRegisterTypeA").value=personsInfo[20];               
             document.getElementById("txtPersonNameB").value=personsInfo[18];
             document.getElementById("txtPersonCidB").value=personsInfo[19];    
            break;             
         case "0105B":/*少生快富B*/
             document.getElementById("txtContactTelB").value=personsInfo[1];
             document.getElementById("txtPersonNameB").value=personsInfo[3];             
             document.getElementById("txtNationsB").value=personsInfo[11];       
             MarryType=personsInfo[7];     
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryTypeB").value=MarryType;
             document.getElementById("txtMarryDateB").value=personsInfo[8];
             document.getElementById("ddlRegisterTypeB").value=personsInfo[20];
             document.getElementById("txtPersonNameA").value=personsInfo[18];
             document.getElementById("txtPersonCidA").value=personsInfo[19];              
             ShowBirth0105();     
            break;    
         case "0106A":/*结扎家庭奖扶 A*/  
             document.getElementById("txtContactTelA").value=personsInfo[1];
             document.getElementById("txtPersonNameA").value=personsInfo[3];  
             MarryType=personsInfo[7];         
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryTypeA").value=MarryType;     
             document.getElementById("txtMarryDateA").value=personsInfo[8];         
             document.getElementById("ddlRegisterTypeA").value=personsInfo[20];
             document.getElementById("UcAreaSelCurA_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurA_txtSelectArea").value=personsInfo[17];              
             document.getElementById("txtPersonNameB").value=personsInfo[18];
             document.getElementById("txtPersonCidB").value=personsInfo[19];           
             ShowBirth();         
            break;             
         case "0106B":/*结扎家庭奖扶 B*/
             document.getElementById("txtContactTelB").value=personsInfo[1];
             document.getElementById("txtPersonNameB").value=personsInfo[3];       
             MarryType=personsInfo[7];     
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryTypeB").value=MarryType;
             document.getElementById("txtMarryDateB").value=personsInfo[8];
             document.getElementById("ddlRegisterTypeB").value=personsInfo[20];
             document.getElementById("UcAreaSelCurB_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurB_txtSelectArea").value=personsInfo[17];   
            break; 
        case "0107A":/*一杯奶 A*/
             document.getElementById("txtNationsA").value=personsInfo[11];
             document.getElementById("UcAreaSelCurA_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurA_txtSelectArea").value=personsInfo[17];                   
            break;  
        case "0107B":/*一杯奶 B*/
             document.getElementById("txtPersonNameB").value=personsInfo[3];  
             document.getElementById("txtNationsB").value=personsInfo[11];
             document.getElementById("UcAreaSelCurB_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurB_txtSelectArea").value=personsInfo[17];  
             document.getElementById("txtPersonNameA").value=personsInfo[18];
             document.getElementById("txtPersonCidA").value=personsInfo[19];
             document.getElementById("txtFileds14").value=personsInfo[8];
            break;                                           
        case "0108A":/*独生子女 A*/
             document.getElementById("txtContactTelA").value=personsInfo[1];
             document.getElementById("txtPersonNameA").value=personsInfo[3];     
             document.getElementById("txtNationsA").value=personsInfo[11];
             document.getElementById("txtWorkUnitA").value=personsInfo[12];
             document.getElementById("UcAreaSelCurA_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurA_txtSelectArea").value=personsInfo[17]; 
             document.getElementById("ddlRegisterTypeA").value=personsInfo[20];               
             document.getElementById("txtPersonNameB").value=personsInfo[18];
             document.getElementById("txtPersonCidB").value=personsInfo[19];          
            break;  
       case "0108B":/*独生子女 B*/
             document.getElementById("txtContactTelB").value=personsInfo[1];
             document.getElementById("txtPersonNameB").value=personsInfo[3];                 
             document.getElementById("txtNationsB").value=personsInfo[11];
             document.getElementById("txtWorkUnitB").value=personsInfo[12];
             document.getElementById("UcAreaSelCurB_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurB_txtSelectArea").value=personsInfo[17];  
             document.getElementById("ddlRegisterTypeB").value=personsInfo[20];  
             document.getElementById("txtPersonNameA").value=personsInfo[18];
             document.getElementById("txtPersonCidA").value=personsInfo[19];
             break;        
        case "0109A":/*流动人口 A*/
             document.getElementById("txtContactTelA").value=personsInfo[1];
             document.getElementById("txtPersonNameA").value=personsInfo[3];  
             MarryType=personsInfo[7];         
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryTypeA").value=MarryType;            
             document.getElementById("ddlRegisterTypeA").value=personsInfo[20];
             document.getElementById("UcAreaSelCurA_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurA_txtSelectArea").value=personsInfo[17];        
             document.getElementById("txtPersonNameB").value=personsInfo[18];
             document.getElementById("txtPersonCidB").value=personsInfo[19];
             ShowBirth0101();     
             break;    
        case "0109B":/*流动人口 B*/
             //document.getElementById("txtContactTelB").value=personsInfo[1];
             document.getElementById("txtPersonNameB").value=personsInfo[3];       
             MarryType=personsInfo[7];     
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryType").value=MarryType;
             document.getElementById("ddlRegisterTypeB").value=personsInfo[20];
             document.getElementById("UcAreaSelCurB_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurB_txtSelectArea").value=personsInfo[17]; 
             document.getElementById("txtPersonNameA").value=personsInfo[18];
             document.getElementById("txtPersonCidA").value=personsInfo[19];
             break;
        case "0110A":/*婚育情况证明 A*/
             document.getElementById("txtContactTelA").value=personsInfo[1];
             document.getElementById("txtPersonNameA").value=personsInfo[3];  
             MarryType=personsInfo[7];         
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryTypeA").value=MarryType;            
             document.getElementById("ddlRegisterTypeA").value=personsInfo[20];
             document.getElementById("UcAreaSelCurA_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurA_txtSelectArea").value=personsInfo[17];              
             document.getElementById("txtPersonNameB").value=personsInfo[18];
             document.getElementById("txtPersonCidB").value=personsInfo[19];
             ShowBirth0101();     
             break;    
        case "0110B":/*婚育情况证明 B*/
             //document.getElementById("txtContactTelB").value=personsInfo[1];
             document.getElementById("txtPersonNameB").value=personsInfo[3];       
             MarryType=personsInfo[7];     
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryType").value=MarryType;
             document.getElementById("ddlRegisterTypeB").value=personsInfo[20];
             document.getElementById("UcAreaSelCurB_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurB_txtSelectArea").value=personsInfo[17];
             document.getElementById("txtPersonNameA").value=personsInfo[18];
             document.getElementById("txtPersonCidA").value=personsInfo[19];
             break; 
       case "0111A":/*終止妊娠 A*/ 
             document.getElementById("txtContactTelA").value=personsInfo[1];
             document.getElementById("txtPersonNameA").value=personsInfo[3];  
             MarryType=personsInfo[7];         
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryTypeA").value=MarryType;  
             document.getElementById("txtNationsA").value=personsInfo[11];           
             document.getElementById("ddlRegisterTypeA").value=personsInfo[20];
             document.getElementById("UcAreaSelCurA_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurA_txtSelectArea").value=personsInfo[17]; 
             document.getElementById("txtPersonNameB").value=personsInfo[18];
             document.getElementById("txtPersonCidB").value=personsInfo[19];
            break; 
      case "0111B":/*終止妊娠 B*/
             document.getElementById("txtContactTelB").value=personsInfo[1];
             document.getElementById("txtPersonNameB").value=personsInfo[3];       
             MarryType=personsInfo[7];     
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryTypeB").value=MarryType;
             document.getElementById("txtNationsB").value=personsInfo[11];
             document.getElementById("ddlRegisterTypeB").value=personsInfo[20];    
             document.getElementById("UcAreaSelCurB_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurB_txtSelectArea").value=personsInfo[17];                
             document.getElementById("txtPersonNameA").value=personsInfo[18];
             document.getElementById("txtPersonCidA").value=personsInfo[19];           
             ShowBirth0111();
            break; 
            
        case "0131A":/*婚育情况证明 A*/
             document.getElementById("txtPersonCidA").value=personsInfo[4];
             document.getElementById("txtContactTelA").value=personsInfo[1];
             document.getElementById("txtPersonNameA").value=personsInfo[3];               
             document.getElementById("txtNationsA").value=personsInfo[11];  
             document.getElementById("txtWorkUnitA").value=personsInfo[12];
             document.getElementById("UcAreaSelRegA_txtSelAreaCode").value=personsInfo[14];
             document.getElementById("UcAreaSelRegA_txtSelectArea").value=personsInfo[15].substr(0, personsInfo[15].indexOf('&'));
             document.getElementById("UcAreaSelRegA_txtArea").value=personsInfo[15].split('&')[1];
             document.getElementById("UcAreaSelCurA_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurA_txtSelectArea").value=personsInfo[17].substr(0, personsInfo[17].indexOf('&'));
             document.getElementById("UcAreaSelCurA_txtArea").value=personsInfo[17].split('&')[1];      
             document.getElementById("ddlRegisterTypeA").value=personsInfo[20];
             document.getElementById("ddlMarryTypeA").value=personsInfo[7]; 
             document.getElementById("txtFileds34").value=personsInfo[8];   
             document.getElementById("txtPersonNameB").value=personsInfo[18];   
             if((personsInfo[19]!="" || personsInfo[19]!=null) && document.getElementById("txtPersonCidB").value==""){
             GetPersonsInfo(personsInfo[19],'0131B','1');
             }   
             ShowBirth0101();     
             break;    
        case "0131B":/*婚育情况证明 B*/
             document.getElementById("txtPersonCidB").value=personsInfo[4];
            if(document.getElementById("txtContactTelA").value==""){
             document.getElementById("txtContactTelA").value=personsInfo[1];
             }
             document.getElementById("txtPersonNameB").value=personsInfo[3];               
             document.getElementById("txtNationsB").value=personsInfo[11];
             document.getElementById("txtWorkUnitB").value=personsInfo[12];
             document.getElementById("UcAreaSelRegB_txtSelAreaCode").value=personsInfo[14];
             document.getElementById("UcAreaSelRegB_txtSelectArea").value=personsInfo[15].substr(0, personsInfo[15].indexOf('&'));
             document.getElementById("UcAreaSelRegB_txtArea").value=personsInfo[15].split('&')[1];
             document.getElementById("UcAreaSelCurB_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurB_txtSelectArea").value=personsInfo[17].substr(0, personsInfo[17].indexOf('&'));
             document.getElementById("UcAreaSelCurB_txtArea").value=personsInfo[17].split('&')[1];  
             document.getElementById("ddlRegisterTypeB").value=personsInfo[20];
             document.getElementById("ddlMarryType").value=personsInfo[7]; 
             document.getElementById("txtFileds14").value=personsInfo[8];
              //配偶信息库不全时
             if(document.getElementById("txtFileds14").value==""){
             document.getElementById("txtFileds14").value=document.getElementById("txtFileds34").value;
             }
            if(document.getElementById("txtNationsB").value==""){
             document.getElementById("txtNationsB").value=document.getElementById("txtNationsA").value;
             }
            if(document.getElementById("ddlRegisterTypeB").value==""){
             document.getElementById("ddlRegisterTypeB").value=document.getElementById("ddlRegisterTypeA").value;
             }
            if(document.getElementById("UcAreaSelRegB_txtSelAreaCode").value==""){
             document.getElementById("UcAreaSelRegB_txtSelAreaCode").value=document.getElementById("UcAreaSelRegA_txtSelAreaCode").value;
             }
            if(document.getElementById("UcAreaSelRegB_txtSelectArea").value==""){
             document.getElementById("UcAreaSelRegB_txtSelectArea").value=document.getElementById("UcAreaSelRegA_txtSelectArea").value;
             }
            if(document.getElementById("UcAreaSelRegB_txtArea").value==""){
             document.getElementById("UcAreaSelRegB_txtArea").value=document.getElementById("UcAreaSelRegA_txtArea").value;
             }
             
            if(document.getElementById("UcAreaSelCurB_txtSelAreaCode").value==""){
             document.getElementById("UcAreaSelCurB_txtSelAreaCode").value=document.getElementById("UcAreaSelCurA_txtSelAreaCode").value;
             }
            if(document.getElementById("UcAreaSelCurB_txtSelectArea").value==""){
             document.getElementById("UcAreaSelCurB_txtSelectArea").value=document.getElementById("UcAreaSelCurA_txtSelectArea").value;
             }
            if(document.getElementById("UcAreaSelCurB_txtArea").value==""){
             document.getElementById("UcAreaSelCurB_txtArea").value=document.getElementById("UcAreaSelCurA_txtArea").value;
             }
             
             if((personsInfo[19]!="" || personsInfo[19]!=null) && document.getElementById("txtPersonCidA").value==""){
             GetPersonsInfo(personsInfo[19],'0131A','1');
             }   
             break;            
        case "ServApp":/*申请申请表头A*/
             document.getElementById("txtContactTelA").value=personsInfo[1];
             document.getElementById("txtPersonNameA").value=personsInfo[3];
             MarryType=personsInfo[7];         
             if(MarryType==""||MarryType==null){MarryType="初婚";}
             document.getElementById("ddlMarryType").value=MarryType;
             document.getElementById("txtMarryDate").value=personsInfo[8]; 
             document.getElementById("txtNationsA").value=personsInfo[11];
             document.getElementById("txtWorkUnitA").value=personsInfo[12];
             document.getElementById("UcAreaSelCurA_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurA_txtSelectArea").value=personsInfo[17];  
        break;
        case "ServAppA":/*申请申请表头B A*/
             document.getElementById("txtContactTelA").value=personsInfo[1];
             document.getElementById("txtPersonNameA").value=personsInfo[3];
             document.getElementById("txtNationsA").value=personsInfo[11];
             document.getElementById("txtWorkUnitA").value=personsInfo[12];
             document.getElementById("UcAreaSelCurA_txtSelAreaCode").value=personsInfo[16];
             document.getElementById("UcAreaSelCurA_txtSelectArea").value=personsInfo[17];            
        break;
        case "ServAppB":/*申请申请表头B*/
         document.getElementById("txtContactTelB").value=personsInfo[1];
         document.getElementById("txtPersonNameB").value=personsInfo[3];
         //document.getElementById("ddlPersonSexB").value=personsInfo[5];         
         MarryType=personsInfo[7];     
         if(MarryType==""||MarryType==null){MarryType="未婚";}
         document.getElementById("ddlMarryType").value=MarryType;
         document.getElementById("txtMarryDate").value=personsInfo[8];
         var BirthType=personsInfo[9];
         if(BirthType==""||BirthType==null){BirthType="未生育";}
         document.getElementById("ddlBirthType").value=BirthType;
         document.getElementById("ddlBirthNum").value=personsInfo[10];
         document.getElementById("txtNationsB").value=personsInfo[11];
         document.getElementById("txtWorkUnitB").value=personsInfo[12];
//         document.getElementById("UcAreaSelRegB_txtSelAreaCode").value=personsInfo[14];
//         document.getElementById("UcAreaSelRegB_txtSelectArea").value=personsInfo[15];
         document.getElementById("UcAreaSelCurB_txtSelAreaCode").value=personsInfo[16];
         document.getElementById("UcAreaSelCurB_txtSelectArea").value=personsInfo[17];
         ShowBirth('');
         ShowBirthType('');
         ShowMarry(); 
        break;
        default:
        alert('没有指定业务类型');
        break;
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
try{
    var MarryType=document.getElementById("ddlMarryTypeB").value;
    if(MarryType=='初婚'||MarryType=='再婚'||MarryType=='复婚')
    { 
        document.getElementById("panelPeiou").style.display = "";
    }   
    else
    {
        document.getElementById("panelPeiou").style.display = "none";
    }    
    if(MarryType=='未婚')
    {
        document.getElementById("panelMarry").style.display = "none";
    }
    else
    {
        document.getElementById("panelMarry").style.display = "";
    }
   }
   catch(e){}
}
 function ShowBirth0101(){
  try{
    var BirthNum=document.getElementById("ddlBirthNum").value;    
    if(BirthNum>0){document.getElementById("panelChildren").style.display = "";}   
    switch (BirthNum)
    {
        case '1':
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "none";
            document.getElementById("tableChild3").style.display = "none";
            break
        case '2':
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "none";
            break
        case '3':
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
            document.getElementById("tableChild3").style.display = "";
            break       
        default:
            document.getElementById("panelChildren").style.display = "none";
            break;
    }
   }catch(e){}
}

 function ShowBirth0122(){
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
 function ShowBirth0111(){
    var BirthNum=document.getElementById("ddlBirthNum").value;    
    if(BirthNum>0){document.getElementById("panelChildren").style.display = "";}   
    switch (BirthNum)
    {
        case '1':
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "none";
            break
        case '2':
            document.getElementById("tableChild1").style.display = "";
            document.getElementById("tableChild2").style.display = "";
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
function ShowMarry0103(){
    var MarryType=document.getElementById("ddlMarryTypeA").value;

    if(MarryType=='初婚'||MarryType=='再婚'||MarryType=='复婚'){ document.getElementById("panelPeiou").style.display = "";}   
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
function CheckCID0105(objCid,type){
    var cid=document.getElementById(objCid).value;
    var obj=cid.substr(16,1);
    if(cid!=''&&cid!=null){
        if(type=='0'){
            obj=cid.substr(16,1);
            if(parseInt(obj)%2!=1){alert('男方身份证号与性别不符');document.getElementById(objCid).value='';return;}
        }
        else if(type=='1'){
            obj=cid.substr(16,1);
            if(parseInt(obj)%2!=0){alert('女方身份证号与性别不符');document.getElementById(objCid).value='';return;}
            else{
                var myDate = new Date();
                var month = myDate.getMonth() + 1;
                var day = myDate.getDate();
                
                var age = myDate.getFullYear() - cid.substring(6, 10) - 1;
                if (cid.substring(10, 12) < month || cid.substring(10, 12) == month && cid.substring(12, 14) <= day) {
                    age++;
                } 
                if(parseInt(age)>49){alert('女方必须在49周岁内才可以申请！');document.getElementById(objCid).value='';return;}
            } 
       }
   }
}
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
/*结扎奖励*/
function CheckCID0106(objCid){
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
            if(parseInt(age)>49){alert('女方必须在49周岁内才可以申请！');document.getElementById(objCid).value='';return;}
        }
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
    
    document.getElementById("txtFatherName4").value=NameA;
    document.getElementById("txtFatherName5").value=NameA;
    document.getElementById("txtFatherName6").value=NameA;
    document.getElementById("txtMotherName4").value=NameB;
    document.getElementById("txtMotherName5").value=NameB;
    document.getElementById("txtMotherName6").value=NameB;
}
function ShowFiled18(){
    var PolicyB = document.getElementById("ddlPolicyB").value;
    if(PolicyB=="政策内")
    {
      document.getElementById("txtFileds18").value="政策内申请终止妊娠。";  
    }
    else
    {
        document.getElementById("txtFileds18").value="政策外申请终止妊娠。";    
    }
}

function ShowChildBirth(objTXT,objDDL){
    var IsBirth=document.getElementById("ddlIsBirth").value;
    if(IsBirth=='是'){ document.getElementById("panelBirth").style.display = "";}
    else{  document.getElementById("panelBirth").style.display = "none";}
    document.getElementById(objTXT).value="";obj = document.getElementById(objDDL);obj[0].selected = true; 
}
function subNum()
    {
        if(document.getElementById("txtPersonCidC").value != "")
        {
            var PersonCidC = document.getElementById("txtPersonCidC").value; 
            var num = PersonCidC.substr(6, 8);
            var num1 = num.substr(0,4);   //年
            var num2 = num.substr(4,2);  //月
            var num3 = num.substr(6,2);  //日
             
            var PersonNum = "";
            if(num1!="" && num2 != "" && num3 != "")
            {
                PersonNum = num1+"-"+num2+"-"+num3;
            }
             
            document.getElementById("txtFileds20").value = PersonNum; 
        }
        else
        {
            document.getElementById("txtFileds20").value = "";
        }
    }
function ShowHY(objTXT){
    var IsHY=document.getElementById("ddlIsHY").value;
    if(IsHY=='是'){ document.getElementById("panelHY").style.display = "";}
    else{  document.getElementById("panelHY").style.display = "none";}
    document.getElementById(objTXT).value=""; 
}
//身份证读卡器民族判断
function SelectNation(objid,Nationid)
{ 
    var x="其他";//57  其他
    switch (Nationid)
    {
        case "01":
          x="汉族";
          break;
        case "02":
          x="蒙古族";
          break;
        case "03":
          x="回族";
          break;
        case "04":
          x="藏族";
          break;
        case "05":
          x="维吾尔族";
          break;
        case "06":
          x="苗族";
          break;
        case "07":
          x="彝族";
          break;
        case "08":
          x="壮族";
          break;
        case "09":
          x="布依族";
          break;
        case "10":
          x="朝鲜族";
          break;
        case "11":
          x="满族";
          break;
        case "12":
          x="侗族";
          break;
        case "13":
          x="瑶族";
          break;
        case "14":
          x="白族";
          break;
        case "15":
          x="土家族";
          break;
        case "16":
          x="哈尼族";
          break;
        case "17":
          x="哈萨克族";
          break;
        case "18":
          x="傣族";
          break;
        case "19":
          x="黎族";
          break;
        case "20":
          x="傈僳族";
          break;
        case "21":
          x="佤族";
          break;
        case "22":
          x="畲族";
          break;
        case "23":
          x="高山族";
          break;
        case "24":
          x="拉祜族";
          break;
        case "25":
          x="水族";
          break;
        case "26":
          x="东乡族";
          break;
        case "27":
          x="纳西族";
          break;
        case "28":
          x="景颇族";
          break;
        case "29":
          x="柯尔克孜族";
          break;
        case "30":
          x="土族";
          break;
        case "31":
          x="达斡尔族";
          break;
        case "32":
          x="仫佬族";
          break;
        case "33":
          x="羌族";
          break;
        case "34":
          x="布朗族";
          break;
        case "35":
          x="撒拉族";
          break;
        case "36":
          x="毛难族";
          break;
        case "37":
          x="仡佬族";
          break;
        case "38":
          x="锡伯族";
          break;
        case "39":
          x="阿昌族";
          break;
        case "40":
          x="普米族";
          break;
        case "41":
          x="塔吉克族";
          break;
        case "42":
          x="怒族";
          break;
        case "43":
          x="乌孜别克族";
          break;
        case "44":
          x="俄罗斯族";
          break;
        case "45":
          x="鄂温克族";
          break;
        case "46":
          x="崩龙族";
          break;
        case "47":
          x="保安族";
          break;
        case "48":
          x="裕固族";
          break;
        case "49":
          x="京族";
          break;
        case "50":
          x="塔塔尔族";
          break;
        case "51":
          x="独龙族";
          break;
        case "52":
          x="鄂伦春族";
          break;
        case "53":
          x="赫哲族";
          break;
        case "54":
          x="门巴族";
          break;
        case "55":
          x="珞巴族";
          break;
        case "56":
          x="基诺族";
          break;
        case "57":
          x="其他";
          break;
        case "58":
          x="外国血统";
          break;
        default:
          x="其他";
    }
    document.getElementById(objid).value = x;
    //alert(x);
    //var s = document.getElementById(objid);
    //var ops = s.options;
    //for(var i=0;i<ops.length; i++){
        //var tempValue = ops[i].value;
       // if(tempValue == x) //这里是你要选的值
        //{
        //ops[i].selected = true;
       // break;
        //}
    //}
}