<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NetworkDisk.aspx.cs" Inherits="join.pms.web.Disk.NetworkDisk" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>�����ļ�</title>
<meta http-equiv="Content-type" content="text/html; charset=gb2312" />
<link href="/css/index.css" type="text/css" rel="stylesheet">
<link href="/css/main.css" type="text/css" rel="stylesheet">
<link href="/css/list.css" type="text/css" rel="stylesheet">
<script language="javascript" type="text/javascript">
 //ʵ��ȫѡ
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
// ��ѡ�� <input value="5" id="0" name="itemsCheck" type="checkbox" />
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

// ѡ��CheckBox
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


//�ļ���/�ļ�������
function RenameObj()
{
    if(!UserHasPower())
    {
        alert("����ʧ�ܣ�û����Ȩ��������ִֹ�д˲�����");
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
        alert("��ѡ��Ҫ�������ļ��л��ļ�����");
        return;
    }
    
    aryItems = selectedItems.split(",");
    aryItemsType = itemsType.split(",");
    if(aryItems.length>2)
    {
        alert("����ʧ�ܣ���������ʱ���ܶ�ѡ��");
    }
    else
    {
        var url = "Rename.aspx?r="+Math.random()+"&FuncCode="+funcNo+"&FuncUser="+funcUser+"&id="+aryItems[0]+"&action="+aryItemsType[0];
        var msgWin = window.showModalDialog(url,"newWin","dialogHeight=200px;dialogWidth=350px;resizable=No;status=0;scrollbars=0");
        if(msgWin!=null){document.location.reload();}
    }
 }
//ʵ��������������еķ���
function go()
{
    history.go(-1);
}
//�ļ�ɾ��
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
            alert("��ѡ��Ҫ�����ļ����ļ��У�");
            return;
        }
        
        aryItems = selectedItems.split(",");
        aryItemsType = itemsType.split(",");
        //alert(aryItems);
        //alert(aryItemsType);
        if(aryItems.length>2 && isFloder)
        {
            alert("����ʧ�ܣ����ǵ�ϵͳ��ȫ��ɾ���ļ��еĲ��������Զ�ѡ��");
        }
        else
        {
            if(confirm("������ʾ���ڲ���֮ǰ��������ȷ��һ�Σ������Ҫɾ����")==true)
            {
                var url = "NetworkDisk.aspx?FuncCode="+funcNo+"&FuncUser="+funcUser+"&ID="+selectedItems+"&action="+aryItemsType[0];
                window.location.href =url;
            }
        }
    }
    else
    {
        alert("����ʧ�ܣ�û����Ȩ��������ִֹ�д˲�����");
    }
    
}

//�ļ����ƶ� 
function Move()
{
    if(!UserHasPower())
    {
        alert("����ʧ�ܣ�û����Ȩ��������ִֹ�д˲�����");
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
        alert("��ѡ��Ҫ�ƶ����ļ���");
        return;
    }
    
    aryItems = selectedItems.split(",");
    aryItemsType = itemsType.split(",");
    if(aryItems.length>2)
    {
        alert("����ʧ�ܣ��ƶ��ļ���ʱ���ܶ�ѡ��");
    }
    else
    {
        var url = "MoveFile.aspx?r="+Math.random()+"&FuncCode="+funcNo+"&FuncUser="+funcUser+"&id="+aryItems[0]+"&action="+aryItemsType[0];
        var msgWin = window.showModalDialog(url,"newWin","dialogHeight=200px;dialogWidth=350px;resizable=No;status=0;scrollbars=0");
        if(msgWin!=null){document.location.reload();}
    }
}
// �½�Ŀ¼
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
        alert("����ʧ�ܣ�û����Ȩ��������ִֹ�д˲�����");
    }
}
// ����
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
//��ת
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
<!-- ҳ����� -->
<input type="hidden" name="txtUrlParams" id="txtUrlParams" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtFuncNo" id="txtFuncNo" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtFuncUser" id="txtFuncUser" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtFuncPowers" id="txtFuncPowers" value="" runat="server" style="display:none;"/>
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!-- ҳ�沼�� -->

<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td>
<!--  �༭-->
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
<td align="left"><a href="#" onclick="javascript:document.location.reload();">ˢ��</a></td></tr></table>
</td><td>&nbsp;</td></tr></table></td>
</tr>
<tr>
  <td height="400" align="left" valign="top" bgcolor="#FFFFFF" class="clistborder"><br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0"><tr>
    <td width="30" height="30" align="center" bgcolor="#F4FAFA" class="fb"><img src="../images/add.gif" width="12" height="12" /></td>
    <td align="left" bgcolor="#F4FAFA" class="page"><span class="fb"><strong><asp:Literal ID="LiteralFuncName" runat="server"></asp:Literal></strong></span></td>
    </tr></table>
    <!-- ����Ϣ Start -->
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td height="30" class="fb01">
            <table width="600" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td >·����<asp:Literal ID="LiteralDirList" runat="server" ></asp:Literal></td>
                <td width="285"><div id="funcUploadFiles"><input id="UploadFiles" contenteditable="false" name="UploadFiles" style="width: 280px" type="file" class="cuserinput" /></div></td>
                <td width="75"><div id="funcUploadFileBut"><asp:Button ID="ButUploadFile" runat="server"  OnClick="ButUploadFile_Click" OnLoad="ButUploadFile_Load" Text=" �ϴ� " CssClass="cusersubmit" ToolTip="�ڵ�ǰ·�����ϴ��ļ�" /></div></td>
              </tr>
            </table>
                </td>
        </tr>
        <tr>
            <td height="30">
                <div id="funtNoPowers"><img style="border:0px" src="../images/NetHD_Back.gif" alt="����" align="absbottom" onclick="GoBack()" onmouseover="this.style.cursor='hand'"/></div>
                <div id="funcButtons">
                <img src="../images/NetHD_Back.gif" alt="����" align="absbottom" onclick="GoBack()" onmouseover="this.style.cursor='hand'"/>
                <img src="../images/NetHD_Next.gif" onclick="Move()" onmouseover="this.style.cursor='hand'" alt="�ƶ�" align="absbottom"/>
                <img src="../images/NetHD_ReName.gif" onclick="RenameObj()" onmouseover="this.style.cursor='hand'"  alt="������" align="absbottom"/>
                <img src="../images/NetHD_Del.gif"  onclick="Del()" onmouseover="this.style.cursor='hand'" alt="ɾ��" align="absbottom"/>
                <img src="../images/NetHD_AddNew.gif" alt="�½�" onclick="AddNewDir(document.getElementById('selDir').value)" onmouseover="this.style.cursor='hand'" align="absbottom"/></div>
            </td>
        </tr>
        <tr>
            <td><asp:Literal ID="LiteralList" runat="server" ></asp:Literal></td>
        </tr>
    </table><br/>
<!-- ����Ϣ End --> 
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
