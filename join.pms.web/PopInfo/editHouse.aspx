<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editHouse.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.editHouse" %>

<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>�ޱ���ҳ</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
    <style type="text/css">
    <!--
    body {
	    margin-top: 10px;
	    background-color: #FFFFFF;
	    margin-left: 10px;
	    margin-right: 10px;
	    margin-bottom: 0px;
	    background-image: url(/images/mainyouxiabg.jpg);
	    background-position:right bottom;
	    background-repeat:no-repeat;
    }
    .butStyle {line-height: 20px; color: #000000; text-decoration: none; font-size: 12px;}
    -->
    </style>
<script language="javascript" type="text/javascript">
function PopCalendar(obj){
    showx = event.screenX - event.offsetX - 4 - 10 ; // + deltaX;
    showy = event.screenY - event.offsetY -168; // + deltaY;
    newWINwidth = 210 + 4 + 18;
    window.showModalDialog("/includes/Calendar.htm",obj,"dialogWidth:200px; dialogHeight:210px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; ");    
}
</script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ��ÿ���ȵ�һ��5��ǰ���ϼ�������ύ����ϵͳ�����γ���3������д��С������Ϣ����</div>
<!-- ҳ����� -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- �༭��  -->
<br/>
<!--��Ů����,��������,����״��,�Ǽ�����,�������,���֤����,������,�־�ס��,��������,���̵�ַ-->
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150"  bgcolor="#CCFFFF">�������ڣ�</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="PopCalendar(txtReportDate);return false;"  runat="server" />
    <input id="Image1" onclick="PopCalendar(txtReportDate);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
    (�ʱ��,������ʵ�ʵĹ����·�,��ѡ�񵽸�������֮��)
    </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu"  bgcolor="#CCFFFF">���ݵ�λ��</td>
  <td align="left"><asp:DropDownList ID="DDLReportArea" runat="server"><asp:ListItem>��ѡ��</asp:ListItem></asp:DropDownList></td>
</tr></table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="150">ҵ��������</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" style="width: 106px">�Ա�</td>
  <td align="left"><asp:DropDownList ID="DropDownList02" runat="server">
    <asp:ListItem>��</asp:ListItem>
    <asp:ListItem>Ů</asp:ListItem>
    </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">���֤�ţ�</td>
  <td align="left"><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">��ϸסַ��</td>
  <td align="left">
      <uc1:ucAreaSel ID="UcAreaSel04" runat="server" />
  </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">��ż������</td>
  <td align="left"><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">���֤�ţ�</td>
  <td align="left"><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">��Ů����</td>
  <td align="left"><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="5" Width="200px" />(����3�������⴦��)</td>
</tr></table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>

<div id="Div1" style="display:none;">
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150">һ��������</td>
  <td align="left"><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">�Ա�</td>
  <td align="left"><asp:DropDownList ID="DropDownList09" runat="server">
      <asp:ListItem>��ѡ��</asp:ListItem><asp:ListItem>��</asp:ListItem><asp:ListItem>Ů</asp:ListItem></asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">���֤�ţ�</td>
  <td align="left"><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr></table>
</div>
<div id="Div2" style="display:none;">
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150">����������</td>
  <td align="left"><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">�Ա�</td>
  <td align="left"><asp:DropDownList ID="DropDownList12" runat="server">
      <asp:ListItem>��ѡ��</asp:ListItem><asp:ListItem>��</asp:ListItem><asp:ListItem>Ů</asp:ListItem></asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">���֤�ţ�</td>
  <td align="left"><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr></table>
</div>
<div id="Div3" style="display:none;">
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150">����������</td>
  <td align="left"><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">�Ա�</td>
  <td align="left"><asp:DropDownList ID="DropDownList15" runat="server">
      <asp:ListItem>��ѡ��</asp:ListItem><asp:ListItem>��</asp:ListItem><asp:ListItem>Ů</asp:ListItem></asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">���֤�ţ�</td>
  <td align="left"><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr></table>
</div>
<div id="Div4" style="display:none;">
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150">�ĺ�������</td>
  <td align="left"><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">�Ա�</td>
  <td align="left"><asp:DropDownList ID="DropDownList18" runat="server">
      <asp:ListItem>��ѡ��</asp:ListItem><asp:ListItem>��</asp:ListItem><asp:ListItem>Ů</asp:ListItem></asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">���֤�ţ�</td>
  <td align="left"><asp:TextBox ID="txtFileds19" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr></table>
</div>
<div id="Div5" style="display:none;">
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150">�庢������</td>
  <td align="left"><asp:TextBox ID="txtFileds20" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">�Ա�</td>
  <td align="left"><asp:DropDownList ID="DropDownList21" runat="server">
      <asp:ListItem>��ѡ��</asp:ListItem><asp:ListItem>��</asp:ListItem><asp:ListItem>Ů</asp:ListItem></asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">���֤�ţ�</td>
  <td align="left"><asp:TextBox ID="txtFileds22" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr></table>
</div>
<div id="Div6" style="display:none;">
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="150">����������</td>
  <td align="left"><asp:TextBox ID="txtFileds23" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">�Ա�</td>
  <td align="left"><asp:DropDownList ID="DropDownList24" runat="server">
      <asp:ListItem>��ѡ��</asp:ListItem><asp:ListItem>��</asp:ListItem><asp:ListItem>Ů</asp:ListItem></asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">���֤�ţ�</td>
  <td align="left"><asp:TextBox ID="txtFileds25" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
</table></div>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- ������ť m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="�� �ύ ��" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
</form>
</body>
</html>
<script language="javascript" type="text/javascript">
function ShowDivsBy(divNo){

    if(isNaN(divNo)){
        alert("���������ָ�ʽ�����ԣ�");
        return;
    }

    if(divNo>6){
        alert("�������࣬���ϱ����������⴦��");
        return;
    }
    switch (divNo)
    {
        case "1":
            document.getElementById("Div1").style.display = "block";
            document.getElementById("Div2").style.display = "none";
            document.getElementById("Div3").style.display = "none";
            document.getElementById("Div4").style.display = "none";
            document.getElementById("Div5").style.display = "none";
            document.getElementById("Div6").style.display = "none";
            break
        case "2":
            document.getElementById("Div1").style.display = "block";
            document.getElementById("Div2").style.display = "block";
            document.getElementById("Div3").style.display = "none";
            document.getElementById("Div4").style.display = "none";
            document.getElementById("Div5").style.display = "none";
            document.getElementById("Div6").style.display = "none";
            break
        case "3":
            document.getElementById("Div1").style.display = "block";
            document.getElementById("Div2").style.display = "block";
            document.getElementById("Div3").style.display = "block";
            document.getElementById("Div4").style.display = "none";
            document.getElementById("Div5").style.display = "none";
            document.getElementById("Div6").style.display = "none";
            break
        case "4":
            document.getElementById("Div1").style.display = "block";
            document.getElementById("Div2").style.display = "block";
            document.getElementById("Div3").style.display = "block";
            document.getElementById("Div4").style.display = "block";
            document.getElementById("Div5").style.display = "none";
            document.getElementById("Div6").style.display = "none";
            break
        case "5":
            document.getElementById("Div1").style.display = "block";
            document.getElementById("Div2").style.display = "block";
            document.getElementById("Div3").style.display = "block";
            document.getElementById("Div4").style.display = "block";
            document.getElementById("Div5").style.display = "block";
            document.getElementById("Div6").style.display = "none";
            break
        case "6":
            document.getElementById("Div1").style.display = "block";
            document.getElementById("Div2").style.display = "block";
            document.getElementById("Div3").style.display = "block";
            document.getElementById("Div4").style.display = "block";
            document.getElementById("Div5").style.display = "block";
            document.getElementById("Div6").style.display = "block";
            break
        default:
            document.getElementById("Div1").style.display = "none";
            document.getElementById("Div2").style.display = "none";
            document.getElementById("Div3").style.display = "none";
            document.getElementById("Div4").style.display = "none";
            document.getElementById("Div5").style.display = "none";
            document.getElementById("Div6").style.display = "none";
            break;
    }
}
//===============
    var defaultVar = document.getElementById('txtFileds07').value;
    if(isNaN(defaultVar)){
        alert("��Ů�������ָ�ʽ��");
    }
    else{
        ShowDivsBy(defaultVar);
    }
    
</script>