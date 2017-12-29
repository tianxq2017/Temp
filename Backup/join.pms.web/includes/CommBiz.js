
var BizEditWin = null;
// 显示业务操作窗体
function  ShowBizEditWindow(objUrl){ 
    if(BizEditWin==null || BizEditWin.closed)
    { 
        var scWidth = screen.width - 20;
        BizEditWin=window.open(objUrl,"BizWin","scrollbars=yes,resizable=yes,width="+scWidth+",height=600,top=60,left=0");
        // scrollbars=yes,resizable=yes, width="+screen.width+",height="+screen.height
    } 
    else
    { 
        BizEditWin.focus(); 
    } 
} 
// 设置业务地址
function SetBizUrl(objUrl,objName,objAction)
{
    if(objUrl.length > 0)
    {
            var sourceUrl = document.getElementById('txtUrlParams').value;
            if(objAction == "add")
            {
                window.location.href = objUrl + "&sourceUrl="+sourceUrl;
            }
            else if(objAction == "edit")
            {
                //ShowBizEditWindow(objUrl + "&sourceUrl="+sourceUrl);
                window.location.href = objUrl + "&sourceUrl="+sourceUrl;
            }
            else if(objAction == "del")
            {
                if(confirm("在删除之前，请您再确认一次，您真的要删除选中的数据吗？")==true)
                {
                    window.location.href = objUrl + "&sourceUrl="+sourceUrl+"&oNa="+objName;
                }else{return;}
            }
            else if(objAction == "view")
            {
                window.location.href = objUrl + "&sourceUrl="+sourceUrl+"&oNa="+objName;
            }
            else if(objAction == "data")
            {
                window.location.href = objUrl + "&sourceUrl="+sourceUrl+"&oNa="+objName;
            }  
            else
            {
                
                var objID = document.getElementById('selectRowID').value;
                var objFuncNo = document.getElementById('tbFuncNo').value;
                var objParams = document.getElementById('selectRowPara').value;
                if(CheckChangeParams(objFuncNo,objParams)>0)
                {
                    if(isInteger(objID))
                    {
                        window.location.href = objUrl + "&sourceUrl="+sourceUrl+"&k="+objID+"&oNa="+objName;
                    }
                    else
                    {
                        alert("操作失败：请首先从列表中点击选择您想要操作的项目！");
                    }
                }
            }
    }
}

function SetButUrl(objUrl)
{
    if(objUrl.length > 0)
    {
         //window.location.href = objUrl;
         window.location = objUrl; // 兼容FireFox
    }
}