<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03013102.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03013102" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>�˿ڶ�̬��Ϣ���浥һ�</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/index.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
    <script language="javascript" src="/Scripts/datectrl.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ������һ��д������Ϣ�󣻵�����ύ����ť������5����ǰ����������ύ������</div>
<!-- ҳ����� -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">

<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- �༭��  -->
    <br/>
    <div style="font-size:28px;text-align:center; font-weight:bold"><%=str_RptTime%>���˿ڶ�̬��Ϣ���浥��һ��(<input id="txt_RptTime" readonly="readonly"  size="10" onclick="JTC.setday({format:'yyyy��MM��', readOnly:true})" runat="server" />��)</div><br/>
    <span style="font-size:14px;">��λ��<%=str_UnitName%></span>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;background-color:#ffffff">
        <thead>
        <tr style="text-align:center; background-color:#cccccc;">
            <td colspan="14" height="22">������Ǩ�ơ��������</td>
            <td colspan="5">�������</td>
        </tr>
        <tr style="text-align:center;background-color:#cccccc">
            <td height="22">����</td>
            <td>���</td>
            <td>���ڱ���</td>
            <td>����</td>
            <td>�������֤��</td>
            <td>�䶯����</td>
            <td>����</td>
            <td>�Ļ��̶�</td>
            <td>��������</td>
            <td>����״��</td>
            <td>��������</td>
            <td>��ס״��</td>
            <td>�뻧����ϵ</td>
            <td>��ż����</td>
            <td>����</td>
            <td>�Ա�</td>
            <td>��������</td>
            <td>��������</td>
            <td>����ԭ��</td>
          </tr>
        </thead>
    <asp:Repeater ID="rep_Data" runat="server">
      <ItemTemplate>
        <tbody>
            <tr style="text-align:center;">
               <td style="background-color:#ccffff" height="22"><%# Eval("AreaName")%></td>
               <td style="background-color:#ccffff"><%# Eval("Fileds01")%></td>
               <td><%# Eval("Fileds02")%></td>
               <td style="background-color:#ffffcc"><%# Eval("Fileds03")%></td>
               <td><%# Eval("Fileds04")%></td>
               <td><%# Eval("Fileds05")%></td>
               <td><%# Eval("Fileds06")%></td>
               <td><%# Eval("Fileds07")%></td>
               <td><%# Eval("Fileds08")%></td>
               <td><%# Eval("Fileds09")%></td>
               <td><%# Eval("Fileds10")%></td>
               <td><%# Eval("Fileds11")%></td>
               <td><%# Eval("Fileds12")%></td>
               <td><%# Eval("Fileds13")%></td>
               <td style="background-color:#ffffcc"><%# Eval("Fileds14")%></td>
               <td><%# Eval("Fileds15")%></td>
               <td><%# Eval("Fileds16")%></td>
               <td><%# Eval("Fileds17")%></td>
               <td><%# Eval("Fileds18")%></td>
            </tr>           
          </tbody>
    </ItemTemplate>  
    </asp:Repeater>
    </table><br />
<!--����ҳ��-->
    <table width="100%" border="0" cellpadding="0" cellspacing="1" >
    <tr style="font-size:14px;">
    <td>�����ˣ�<asp:TextBox ID="txt_SldHeader" runat="server"></asp:TextBox></td>
    <td align="center">����ˣ�<asp:TextBox ID="txt_SldLeader" runat="server"></asp:TextBox></td>
    <td align="right">����ڣ�<input id="txt_OprateDate" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />&nbsp;</td>
    </tr>
    </table>
     <br/>
     
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="zhengwen" >
    <tr style="font-size:14px;">
    <td>
        <%if (IsReported == "0") {%>
        <asp:CheckBox ID="ck_IsCheck" runat="server" /><span style=" font-size:12px; color:#f60; margin-bottom:10px;">ȷ���ϱ����ϱ�������������ݽ������������޸�.</span><br/>
        <input type="submit"  name="submit1" class="submit6" onclick="check();return false;" value="ȷ���ϱ�"/>
        <%}%>
        <input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:history.go(-1);" class="submit6" />
    </td>
    </tr>
    </table>
     <br/>
    
</td></tr></table>
</td></tr></table>
</div>
    </form>
    <script type="text/javascript">
        function check(){
          var checkbox = document.getElementById('ck_IsCheck');
          if(checkbox.checked){
            if (confirm('ȷ���ϱ�?�ϱ�������������ݽ������������޸�.'))
            {
                this.submit();
            } 
            else 
            {
                return false;
            } 
          }else{
            alert("��ȷ���ϱ�");
            return false;
          }
        }
    </script>
</body>
</html>
