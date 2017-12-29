/*首次添加时使用*/
function check()
{		
    var BizCNum=document.getElementById("txtBizCNum").value; 
    var strErr='';  
    for(var i=0;i<BizCNum;i++)
    {
        var DocsID=document.getElementById("txtDocsID"+i).value;  
        ////20150630 取消现场提交，不上传默认现场提交      
//        var cbBiz=document.getElementById("cbBiz"+i).checked;  
//        if((DocsID.length==0||DocsID==null||DocsID=="")&&!cbBiz)
//        {strErr += "请上传" + decodeURIComponent(document.getElementById("txtDocsName"+i).value) + "证件或勾选现场提交！\n";}		
    }
    var IsInnerArea=document.getElementById("txtIsInnerArea").value;
    if (IsInnerArea == "1")
    {
        var DocsIDIs =document.getElementById("txtDocsIDIs").value;
        if (DocsIDIs.length==0||DocsIDIs==null||DocsIDIs=="") { strErr += "请上传" +decodeURIComponent(document.getElementById("txtDocsNameIs").value) + "！\n"; }
    }
    //var cbOk=document.getElementById("cbOk").checked; 
    //if(!cbOk){strErr += "请确认自检完成！\n";}
    if(strErr.length>0){alert(strErr);return false;}
}

/*修改时使用*/
function EditCheck()
{	
    var BizDocsIDiOld =document.getElementById("txtBizDocsIDiOld").value+",";//已上传的图片所处的索引	
    var BizCNum=document.getElementById("txtBizCNum").value; 
    var Action=document.getElementById("txtAction").value;//Attach 需要补正
    var strErr=''; 
    for(var i=0;i<BizCNum;i++)
    {
        var DocsID=document.getElementById("txtDocsID"+i).value;  
        var DocsName=decodeURIComponent(document.getElementById("txtDocsName"+i).value) 
//        if(Action=="Attach"&&DocsName=="收件回执单"){        
//            if((DocsID.length==0||DocsID==null||DocsID=="")&&!contains(BizDocsIDiOld.split(','),""+i+""))
//            {  strErr += "请上传" + DocsName + "！\n";}		
//        }   
//        else{
            //20150630 取消现场提交，不上传默认现场提交
//            var cbBiz=document.getElementById("cbBiz"+i).checked;       
//            if((DocsID.length==0||DocsID==null||DocsID=="")&&!cbBiz&&!contains(BizDocsIDiOld.split(','),""+i+""))
//            {  strErr += "请上传" + DocsName + "证件或勾选现场提交！\n";}	
//        }
    }
    var IsInnerArea=document.getElementById("txtIsInnerArea").value;
    if (IsInnerArea == "1")
    {
        var BizDocsIDIsOld=document.getElementById("txtBizDocsIDIsOld").value;
        var DocsIDIs =document.getElementById("txtDocsIDIs").value;       
        if ((DocsIDIs.length==0||DocsIDIs==null||DocsIDIs=="")&&(BizDocsIDIsOld.length==0||BizDocsIDIsOld==null||BizDocsIDIsOld=="")) { strErr += "请上传" +decodeURIComponent(document.getElementById("txtDocsNameIs").value) + "！\n"; }
    }
    //var cbOk=document.getElementById("cbOk").checked; 
    //if(!cbOk){strErr += "请确认自检完成！\n";}
     
    if(strErr.length>0){alert(strErr);return false;}
}
function contains(a, obj) {
    var i = a.length;
    while (i--) {
       if (a[i] === obj) {
           return true;
       }
    }
    return false;
}
//function contains(a, obj) {  
//    for(var i=0;i<a.length;i++){
//	if(a[i] === obj){return true;}
//	}
//return false;
//}