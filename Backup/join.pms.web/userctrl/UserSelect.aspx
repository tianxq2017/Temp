<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserSelect.aspx.cs" Inherits="join.pms.web.userctrl.UserSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>人员选择</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<style>
<!--
td       { font-size: 12px;}
select       { font-size: 12px; width: 200px }
input        {	BORDER-RIGHT: #666666 1px solid; BORDER-TOP: #666666 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #666666 1px solid; COLOR: #666666; BORDER-BOTTOM: #666666 1px solid; HEIGHT: 18px; BACKGROUND-COLOR: #ffffff
}
-->
</style>
<script language="javascript" type="text/javascript">
var deptStr = "<%=m_StrDept%>";
var allidStr = "<%=m_StrName%>";
var selidStr = "<%=m_StrIDs%>";
var arrDept  = deptStr.split(",");
var arrAllID = allidStr.split(",");
var arrSelID = selidStr.split(",");
var IsSelSon = false

function GetParentID(ID){
	for(var i=0;i<arrDept.length;i+=3){
		if(arrDept[i]==ID){
			return arrDept[i+1]
		}
	}
	return -1
}

function GetSonDeptIDs(ID){
	var arr = new Array()
	for(var i=0;i<arrDept.length;i+=3){
		if(arrDept[i+1]==ID){
			arr[arr.length]=arrDept[i]
		}
	}
	return arr
}

function GetDeptName(ID){
	for(var i=0;i<arrDept.length;i+=3){
		if(arrDept[i]==ID)return arrDept[i+2]
	}
	return ""
}


var StrDEPT=""
var IntNullCount = 0
function WriteDeptOption(PID){
	if(GetSonDeptIDs(PID).length!=0){
		var arrD = GetSonDeptIDs(PID)
		for(var i=0;i<arrD.length;i++){
			StrDEPT+="<option value=" + arrD[i] + ">" + ReNull(IntNullCount) + GetDeptName(arrD[i]) + "</option>"
			IntNullCount ++
			WriteDeptOption(arrD[i])
			IntNullCount --
		}
	}
}


var arrSonD = new Array()
function GetAllSonDeptIDs(PID){
	if(GetSonDeptIDs(PID).length!=0){
		var arrD = GetSonDeptIDs(PID)
		for(var i=0;i<arrD.length;i++){
			arrSonD[arrSonD.length] = arrD[i]
			GetAllSonDeptIDs(arrD[i])
		}
	}
}


function SetArrSonD(PID){
	var d = new Array(PID)
	arrSonD = d
	GetAllSonDeptIDs(PID)
}


function ReNull(t){
	var k = ""
	for(var i=1;i<=t;i++){
		k +="-"
	}
	return k
}

function WD(){
	var s = '<select size="1" name="DeptID" onchange="ChangeDept(this.value)"><option value=0>请选择部门</option>'
	WriteDeptOption('0')
	s += StrDEPT
	s += "</select>"
	tdSel0.innerHTML = s
}





function GetOption(IDs){
	var s=""
	for(var i=0;i<IDs.length;i++){
		if(!GetName(IDs[i])=="")
		s += "<option value=" + IDs[i] + ">" + GetName(IDs[i]) + "</option>"
	}
	return s
}

function GetName(ID){
	for(var i=0;i<arrAllID.length;i+=3){
		if(arrAllID[i]==ID)return arrAllID[i+2]
	}
	return ""
}

function GetDeptIDs(DeptID){
	var arr = new Array()
	for(var i=0;i<arrAllID.length;i+=3){
		if(arrAllID[i+1]==DeptID)arr[arr.length]=arrAllID[i]
	}
	return arr
}

function ChangeDept(ID){
    //alert(ID);
	var s = "<select name=Sel1 size=15  <%=m_Multiple%> ondblclick=\"doAdd();\">"
	if(IsSelSon){
		SetArrSonD(ID)
		for(var i=0;i<arrSonD.length;i++){
			s+= GetOption(GetDeptIDs(arrSonD[i]))
		}
	}
	else{
		s+= GetOption(GetDeptIDs(ID))
	}
	s+= "</select>"
	tdSel1.innerHTML = s  
}

function AddToSel(ID){
 <%if(IsSelOne){%>
	arrSelID[0]=ID
	return
 <%}%>
 	for(var i=0;i<arrSelID.length;i++){
		if(arrSelID[i]==ID){
			return;
		}
	}
	arrSelID[arrSelID.length]=ID
}

function DelFromSel(ID){
	for(var i=0;i<arrSelID.length;i++){
		if(arrSelID[i]==ID)delete arrSelID[i];
	}
}

function doReWriteSel(){
	var s = "<select name=Sel2 size=16 <%=m_Multiple%> ondblclick=\"doDel();\">"
	s+= GetOption(arrSelID)
	s+= "</select>"
	tdSel2.innerHTML = s 
}

function doAdd(){
	f = document.forms[0]
	for(var i=0;i<f.Sel1.length;i++){
		if(f.Sel1[i].selected)AddToSel(f.Sel1[i].value)
	}
	doReWriteSel()
}
// 全选
function doAllAdd(){
	f = document.forms[0]
	for(var i=0;i<f.Sel1.length;i++){
		AddToSel(f.Sel1[i].value);
	}
	doReWriteSel()
}

function doDel(){
	f = document.forms[0]
	for(var i=0;i<f.Sel2.length;i++){
		if(f.Sel2[i].selected)DelFromSel(f.Sel2[i].value)
	}
	doReWriteSel()
}
// 全去
function doAllDel(){
	f = document.forms[0]
	for(var i=0;i<f.Sel2.length;i++){
		DelFromSel(f.Sel2[i].value);
	}
	doReWriteSel()
}

function doEnd(){
	var k1=k2=""
	f = document.forms[0]
	for(var i=0;i<f.Sel2.length-1;i++){
		k1 += f.Sel2[i].value + ","
		k2 += f.Sel2[i].text + ","
	}
	if(f.Sel2.length>0){
		k1 += f.Sel2[i].value
		k2 += f.Sel2[i].text
	}
	var k = new Array()
	k[0]=k1
	k[1]=k2
	window.returnValue=k
	window.close()
}
</script>
</head>
<body topmargin="0" leftmargin="10"  onload="doReWriteSel()" bgcolor="#dedfde">
<!--
onload="WD();doReWriteSel()"
<select size="1" name="DeptID" onChange="ChangeDept(this.value)" style="width: 200" style="display:none;"></select>
-->
<form method="POST" webbot-action="--WEBBOT-SELF--" >
<br>
  <table border="0" cellpadding="0" cellspacing="0" width=98% align=right>
    <tr>
      <td colspan="3" ><!--部门选择 id=tdSel0-->请选择人员名称,单击“选择”选择按钮；或直接双击您要选择的人员也可。选择完毕后请按“确定”按钮确认。</td>
    </tr>
    <tr>
      <td id="tdSel1"><select size="15" name="Sel1" style="width: 200" ></select></td>
      <td width="80">
        <p align="center">
        <input type="button" value="全选->" name="B0" onClick="doAllAdd()"/>
        <br/><br/>
        <input type="button" value="选择->" name="B1" onClick="doAdd()"/>
        <br/><br/>
        <input type="button" value="<-删除" name="B1" onClick="doDel()"/>
        <br/><br/>
        <input type="button" value="<-全去" name="B1" onClick="doAllDel()"/>
        </p>
      </td>
      <td id="tdSel2"><select size="16" id="Sel2" name="Sel2" style="width: 200px" ></select></td>
    </tr>
    <tr>
      <td><!--<input type=checkbox id=checkbox1 name=checkbox1 onClick="IsSelSon=this.checked;ChangeDept(DeptID.value)">--><!--包含子部门人员--></td>
      <td></td>
      <td ><br/><input type="button" value=" 确定 " onClick="doEnd()" name="B2"> </td>
    </tr>
  </table>
  <p>　</p>
</form>
<script language="javascript" type="text/javascript">

IsSelSon =true;
ChangeDept('0');
</script>
</body>
</html>
