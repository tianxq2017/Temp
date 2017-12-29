function check()
{		
    var BizCNum=document.getElementById("txtBizCNum").value; 
    var strErr=''; 
    for(var i=0;i<BizCNum;i++)
    {
        var obj=document.getElementById("txtUploadFiles"+i);     
        var DocsID=obj.value;  
        var DocsName=decodeURIComponent(document.getElementById("txtDocsName"+i).value)
        var cbBiz=document.getElementById("cbBiz"+i).checked;  
        if((DocsID.length==0||DocsID==null||DocsID=="")&&!cbBiz)
        {strErr += "请上传" + DocsName+ "证件或现场提交！\n";}	
        else{if(DocsID.length>0){getPhotoSize(obj);}}        
    }
    var IsInnerArea=document.getElementById("txtIsInnerArea").value;
    if (IsInnerArea == "1")
    {
        var DocsIDIs =document.getElementById("txtDocsIDIs").value;      
        var cbBizIs=document.getElementById("cbBizIs").checked;  
        if ((DocsIDIs.length==0||DocsIDIs==null||DocsIDIs=="")&&!cbBizIs) { strErr += "请上传" +decodeURIComponent(document.getElementById("txtDocsNameIs").value) + "！\n"; }
    }
    var cbOk=document.getElementById("cbOk").checked; 
    if(!cbOk){strErr += "请确认承诺！\n";}
    if(strErr.length>0){alert(strErr);return false;}
}
function getPhotoSize(obj){
    photoExt=obj.value.substr(obj.value.lastIndexOf(".")).toLowerCase();//获得文件后缀名
   
    if(photoExt!='.jpg'&&photoExt!='.gif'&&photoExt!='.png'&&photoExt!='.bmp'){alert(obj.value);
        alert("请上传后缀名为jpg|gif|png|bmp的图片!");
        return false;
    }
    var fileSize = 0;
    var isIE = /msie/i.test(navigator.userAgent) && !window.opera;           
    if (isIE && !obj.files) {         
         var filePath = obj.value;           
         var fileSystem = new ActiveXObject("Scripting.FileSystemObject");  
         var file = fileSystem.GetFile (filePath);              
         fileSize = file.Size;        
    }else { 
         fileSize = obj.files[0].size;    
    }
    fileSize=fileSize/1024; //单位为M10240
    if(fileSize>=10240){
        alert("图片文件过大，请使用图像处理软件处理后再上传!");
        return false;
    }
}