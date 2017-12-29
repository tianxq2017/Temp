<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NetworkDisk.aspx.cs" Inherits="join.pms.web.Disk.NetworkDisk" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>共享文件</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/index.css" type="text/css" rel="stylesheet">
<link href="/css/main.css" type="text/css" rel="stylesheet">
<link href="/css/list.css" type="text/css" rel="stylesheet">
<script language="javascript" type="text/javascript">
 //实现全选
function SelectAll()
{
    //var cbxAry = document.getElementsByTagName("input"); <input id="0" name="itemsi" onclick="javascript:SelectAll();" type="checkbox" />
    //var cbxAry = document.form1.itemsCheck;
    var cbxAry = document.getElementsByName("itemsCheck");
    for(i=0;i<cbxAry.length;i++)
    {
        if(document.form1.itemsi.checked)
        {
            cbxAry[i].checked=true;
        }
        else
        {
            cbxAry[i].checked=false;
        }
    }
}
// 行选择 <input value="5" id="0" name="itemsCheck" type="checkbox" />
function SetCheckBoxClear(objID){  
    var cbx = document.getElementsByName("itemsCheck");
    //var cbx = document.getElementsByTagName("input"); window.document.getElementsByName("ItemChk");
    //var cbx = document.form1.itemsCheck;
    for(var i=0; i<cbx.length; i++)     
    {         
        if(cbx[i].type=="checkbox" && cbx[i].value == objID)         
        {             
            cbx[i].checked = true; 
        }
        else
        {
            cbx[i].checked = false; 
        }
    } 
}

// 选中CheckBox
function SetCheckBoxClick(objID){  
    var itemsAry = window.document.getElementsByName("itemsCheck");
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


//文件夹/文件重命名
function RenameObj()
{
    if(!UserHasPower())
    {
        alert("操作失败：没有授权，您被禁止执行此操作！");
        return;
    }
    var selectedItems = "";
    var itemsType = "";
    var aryItems = new Array();
    var aryItemsType = new Array();
    var funcNo = document.getElementById('txtFuncNo').value;
    var funcUser = document.getElementById('txtFuncUser').value;
    
    //var itemsAry = document.form1.itemsCheck;
    var itemsAry = document.getElementsByName("itemsCheck");
    var hiddenAry = document.getElementsByName("txtHidden");
    for(var i=0;i<itemsAry.length;i++)
    { 
        if(itemsAry[i].checked)
        {
            selectedItems += itemsAry[i].value +",";
            itemsType += hiddenAry[i].value +",";
        }
    }
    if(selectedItems=="")
    {
        alert("请选择要命名的文件夹或文件名！");
        return;
    }
    
    aryItems = selectedItems.split(",");
    aryItemsType = itemsType.split(",");
    if(aryItems.length>2)
    {
        alert("操作失败：更改名称时不能多选！");
    }
    else
    {
        var url = "Rename.aspx?r="+Math.random()+"&FuncCode="+funcNo+"&FuncUser="+funcUser+"&id="+aryItems[0]+"&action="+aryItemsType[0];
        var msgWin = window.showModalDialog(url,"newWin","dialogHeight=200px;dialogWidth=350px;resizable=No;status=0;scrollbars=0");
        if(msgWin!=null){document.location.reload();}
    }
 }
//实现类似于浏览器中的返回
function go()
{
    history.go(-1);
}
//文件删除
function Del()
{
    if(UserHasPower())
    {
        var selectedItems = "";
        var itemsType = "";
        var aryItems = new Array();
        var aryItemsType = new Array();
        var funcNo = document.getElementById('txtFuncNo').value;
        var funcUser = document.getElementById('txtFuncUser').value;
        
        var itemsAry = window.document.getElementsByName("itemsCheck");
        var hiddenAry = document.getElementsByName("txtHidden"); //document.form1.txtHidden;
        var isFloder = false;
        //alert(itemsAry);
        for(var i=0;i<itemsAry.length;i++)
        { 
            if(itemsAry[i].checked)
            {
                selectedItems += itemsAry[i].value +",";
                itemsType += hiddenAry[i].value +",";
                if(hiddenAry[i].value=="1") isFloder = true;
            }
        }
        //alert(selectedItems);
        if(selectedItems=="")
        {
            alert("请选择要操作文件或文件夹！");
            return;
        }
        
        aryItems = selectedItems.split(",");
        aryItemsType = itemsType.split(",");
        //alert(aryItems);
        //alert(aryItemsType);
        if(aryItems.length>2 && isFloder)
        {
            alert("操作失败：考虑到系统安全，删除文件夹的操作不可以多选！");
        }
        else
        {
            if(confirm("操作提示：在操作之前，请你再确认一次，您真的要删除吗？")==true)
            {
                var url = "NetworkDisk.aspx?FuncCode="+funcNo+"&FuncUser="+funcUser+"&ID="+selectedItems+"&action="+aryItemsType[0];
                window.location.href =url;
            }
        }
    }
    else
    {
        alert("操作失败：没有授权，您被禁止执行此操作！");
    }
    
}

//文件夹移动 
function Move()
{
    if(!UserHasPower())
    {
        alert("操作失败：没有授权，您被禁止执行此操作！");
        return;
    }
    var selectedItems = "";
    var itemsType = "";
    var aryItems = new Array();
    var aryItemsType = new Array();
    var funcNo = document.getElementById('txtFuncNo').value;
    var funcUser = document.getElementById('txtFuncUser').value;
    // document.getElementsByName("ItemChk")(i).checked=true;
    var itemsAry = document.getElementsByName("itemsCheck");
    var hiddenAry = document.getElementsByName("txtHidden");
    for(var i=0;i<itemsAry.length;i++)
    { 
        if(itemsAry[i].checked)
        {
            selectedItems += itemsAry[i].value +",";
            itemsType += hiddenAry[i].value +",";
        }
    }
    if(selectedItems=="")
    {
        alert("请选择要移动的文件！");
        return;
    }
    
    aryItems = selectedItems.split(",");
    aryItemsType = itemsType.split(",");
    if(aryItems.length>2)
    {
        alert("操作失败：移动文件称时不能多选！");
    }
    else
    {
        var url = "MoveFile.aspx?r="+Math.random()+"&FuncCode="+funcNo+"&FuncUser="+funcUser+"&id="+aryItems[0]+"&action="+aryItemsType[0];
        var msgWin = window.showModalDialog(url,"newWin","dialogHeight=200px;dialogWidth=350px;resizable=No;status=0;scrollbars=0");
        if(msgWin!=null){document.location.reload();}
    }
}
// 新建目录
function AddNewDir(directoryID)
{
    if(UserHasPower())
    {
        var funcNo = document.getElementById('txtFuncNo').value;
        var funcUser = document.getElementById('txtFuncUser').value;
        if(directoryID!=null && directoryID!="")
        {
            var url = "NewAddDirectory.aspx?r="+Math.random()+"&FuncCode="+funcNo+"&FuncUser="+funcUser+"&id="+directoryID+"&action=addnew";
            var msgWin = window.showModalDialog(url,"newWin","dialogHeight=200px;dialogWidth=350px;resizable=No;status=0;scrollbars=0");
            if(msgWin!=null){document.location.reload();}
        }
    }
    else
    {
        alert("操作失败：没有授权，您被禁止执行此操作！");
    }
}
// 后退
function GoBack()
{
    var directoryID = document.getElementById('selDir').value;
    if(directoryID!=null && directoryID!="")
    {
        if(directoryID=="1")
        {
            return;
        }
        else
        {
            window.history.go(-1);
        }
    }
}
//跳转
function gets(code,dirID)
{
    var funcUser = document.getElementById('txtFuncUser').value;
     var url="NetworkDisk.aspx?FuncCode="+code+"&FuncUser="+funcUser+"&ID="+dirID+"&action=changeDir";
     window.location.href = url;
}
function UserHasPower()
{
    var funcPowers = document.getElementById('txtFuncPowers').value;
    var aryPowers = funcPowers.split(",");
    if(aryPowers[0]=="1" && aryPowers[1]=="1" && aryPowers[2]=="1" && aryPowers[3] =="1")
    {
        return true;
    }
    else
    {
        return false;
    }
}
</script>
</head>
<body>
<form id="form1" runat="server" enctype="multipart/form-data">
<!-- 页面参数 -->
<input type="hidden" name="txtUrlParams" id="txtUrlParams" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtFuncNo" id="txtFuncNo" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtFuncUser" id="txtFuncUser" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtFuncPowers" id="txtFuncPowers" value="" runat="server" style="display:none;"/>
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!-- 页面布局 -->

<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td>
<!--  编辑-->
<div class="rightb"></div><div class="rightb">
<table width="100%" border="0" cellspacing="0" cellpadding="0"><tr><td>
<table width="100%" border="0" cellspacing="0" cellpadding="0"><tr><td width="100">
<table width="100" border="0" cellpadding="0" cellspacing="0" bgcolor="#26724B"><tr>
<td width="10"><img src="../images/cnav03.jpg" width="10" height="27" /></td>
<td align="center" bgcolor="#30915E" class="fw" style="color:#ffffff"><strong><asp:Literal ID="LiteralTitle" runat="server"></asp:Literal></strong></td>
<td width="10" align="right" ><img src="../images/cnav04.jpg" width="10" height="27" /></td></tr></table>
</td><td width="10" align="center">&nbsp;</td><td width="85" align="center" class="gnbtn">
<table width="85%" border="0" cellspacing="1" cellpadding="0"><tr>
<td width="30" align="center"><img src="../images/icon_refresh.gif" width="23" height="20" /></td>
<td align="left"><a href="#" onclick="javascript:document.location.reload();">刷新</a></td></tr></table>
</td><td>&nbsp;</td></tr></table></td>
</tr>
<tr>
  <td height="400" align="left" valign="top" bgcolor="#FFFFFF" class="clistborder"><br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0"><tr>
    <td width="30" height="30" align="center" bgcolor="#F4FAFA" class="fb"><img src="../images/add.gif" width="12" height="12" /></td>
    <td align="left" bgcolor="#F4FAFA" class="page"><span class="fb"><strong><asp:Literal ID="LiteralFuncName" runat="server"></asp:Literal></strong></span></td>
    </tr></table>
    <!-- 表单信息 Start -->
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td height="30" class="fb01">
            <table width="600" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td >路径：<asp:Literal ID="LiteralDirList" runat="server" ></asp:Literal></td>
                <td width="285"><div id="funcUploadFiles"><input id="UploadFiles" contenteditable="false" name="UploadFiles" style="width: 280px" type="file" class="cuserinput" /></div></td>
                <td width="75"><div id="funcUploadFileBut"><asp:Button ID="ButUploadFile" runat="server"  OnClick="ButUploadFile_Click" OnLoad="ButUploadFile_Load" Text=" 上传 " CssClass="cusersubmit" ToolTip="在当前路径下上传文件" /></div></td>
              </tr>
            </table>
                </td>
        </tr>
        <tr>
            <td height="30">
                <div id="funtNoPowers"><img style="border:0px" src="../images/NetHD_Back.gif" alt="向上" align="absbottom" onclick="GoBack()" onmouseover="this.style.cursor='hand'"/></div>
                <div id="funcButtons">
                <img src="../images/NetHD_Back.gif" alt="向上" align="absbottom" onclick="GoBack()" onmouseover="this.style.cursor='hand'"/>
                <img src="../images/NetHD_Next.gif" onclick="Move()" onmouseover="this.style.cursor='hand'" alt="移动" align="absbottom"/>
                <img src="../images/NetHD_ReName.gif" onclick="RenameObj()" onmouseover="this.style.cursor='hand'"  alt="重命名" align="absbottom"/>
                <img src="../images/NetHD_Del.gif"  onclick="Del()" onmouseover="this.style.cursor='hand'" alt="删除" align="absbottom"/>
                <img src="../images/NetHD_AddNew.gif" alt="新建" onclick="AddNewDir(document.getElementById('selDir').value)" onmouseover="this.style.cursor='hand'" align="absbottom"/></div>
            </td>
        </tr>
        <tr>
            <td><asp:Literal ID="LiteralList" runat="server" ></asp:Literal></td>
        </tr>
    </table><br/>
<!-- 表单信息 End --> 
</td></tr></table>
</div><div class="rightb"></div>
<!-- End -->
</td></tr></table>
<script language="javascript" type="text/javascript">
    var funcPowers = document.getElementById('txtFuncPowers').value;
    var aryPowers = funcPowers.split(",");
    if(aryPowers[0]=="1" && aryPowers[1]=="1" && aryPowers[2]=="1" && aryPowers[3] =="1")
    {
        document.getElementById("funtNoPowers").style.display = 'none';
    }
    else
    {
        document.getElementById("funcUploadFiles").style.display = 'none';
        document.getElementById("funcUploadFileBut").style.display = 'none';
        document.getElementById("funcButtons").style.display = 'none';
        document.getElementById("funtNoPowers").style.display = 'block';
    }
</script>
</form>
</body>
</html>
