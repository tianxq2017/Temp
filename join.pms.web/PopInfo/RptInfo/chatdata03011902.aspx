<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03011902.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03011902" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>����ʡʵʩȫ�������������举Ů�������ͳ�Ʊ��أ�һ�</title>
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
    <div style="font-size:28px; text-align:center; font-weight:bold">����ʡʵʩȫ�������������举Ů�������ͳ�Ʊ�<asp:DropDownList ID="dr_RptTime" runat="server">
                                                                                        <asp:ListItem>1</asp:ListItem>
                                                                                        <asp:ListItem>2</asp:ListItem>
                                                                                        <asp:ListItem>3</asp:ListItem>
                                                                                        <asp:ListItem>4</asp:ListItem>
                                                                                        <asp:ListItem>5</asp:ListItem>
                                                                                        <asp:ListItem>6</asp:ListItem>
                                                                                        <asp:ListItem>7</asp:ListItem>
                                                                                        <asp:ListItem>8</asp:ListItem>
                                                                                        <asp:ListItem>9</asp:ListItem>
                                                                                        <asp:ListItem>10</asp:ListItem>
                                                                                        <asp:ListItem>11</asp:ListItem>
                                                                                        <asp:ListItem>12</asp:ListItem>
                                                                                       </asp:DropDownList> <%=str_RptTime%>�£�</div>
    <p><%=str_UnitName%></p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
        <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="4">��λ</td>
            <td colspan="3">ȫ��������������ۼ���</td>
            <td colspan="16">�����������举Ů��������������˿�����</td>
            <td rowspan="4">��������������������</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">�ܼ�</td>
            <td rowspan="3">��</td>
            <td rowspan="3">Ů</td>
            <td colspan="3">���ӳ������</td>
            <td colspan="3">���举Ů��������</td>
            <td colspan="5">���举Ů����ṹ</td>
            <td colspan="5">��һ���������</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="2">С��</td>
            <td rowspan="2">��</td>
            <td rowspan="2">Ů</td>
            <td rowspan="2">С��</td>
            <td rowspan="2">�������</td>
            <td rowspan="2">ũ�����</td>
            <td rowspan="2">С��</td>
            <td rowspan="2">8��-25��</td>
            <td rowspan="2">26��-35��</td>
            <td rowspan="2">36��-45��</td>
            <td rowspan="2">46������</td>
            <td colspan="2">�Ա�</td>
            <td colspan="3">�������</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>��</td>
            <td>Ů</td>
            <td>0-4��</td>
            <td>5-9��</td>
            <td>10������</td>
          </tr>
          <tr style="text-align:center;background-color:#ffffcc">
            <td>�ϼ�</td>
            <td><%=num1%></td>
            <td><%=num2%></td>
            <td><%=num3%></td>
            <td><%=num4%></td>
            <td><%=num5%></td>
            <td><%=num6%></td>
            <td><%=num7%></td>
            <td><%=num8%></td>
            <td><%=num9%></td>
            <td><%=num10%></td>
            <td><%=num11%></td>
            <td><%=num12%></td>
            <td><%=num13%></td>
            <td><%=num14%></td>
            <td><%=num15%></td>
            <td><%=num16%></td>
            <td><%=num17%></td>
            <td><%=num18%></td>
            <td><%=num19%></td>
            <td><%=num20%></td>
          </tr>
        </thead>
        <tbody>
        <asp:Literal ID="ltr_Content" runat="server"></asp:Literal>
        </tbody>
    </table><br/>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" >
    <tr style="font-size:14px;">
    <td>�����ˣ�<asp:TextBox ID="txt_SldHeader" runat="server"></asp:TextBox></td>
    <td align="center">����ˣ�<asp:TextBox ID="txt_SldLeader" runat="server"></asp:TextBox></td>
    <td align="right">����ڣ�<input id="txt_OprateDate" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />&nbsp;</td>
    </tr>
    </table>
    
    <%if (IsReported == "0") {%>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="zhengwen" >
    <tr style="font-size:14px;">
    <td>
        <asp:CheckBox ID="ck_IsCheck" runat="server" /><span style=" font-size:12px; color:#f60; margin-bottom:10px;">ȷ����ˣ���˺�����������ݽ������������޸�.</span><br/>
        <input type="submit"  name="submit1" class="submit6" onclick="check();return false;" value="ȷ�����"/>
    </td>
    </tr>
    </table>
     <br/>
     <%}%>
</td></tr></table>
</td></tr></table>

</div>
    </form>
    <script type="text/javascript">
        function check(){
          var checkbox = document.getElementById('ck_IsCheck');
          if(checkbox.checked){
            if (confirm('ȷ�����?��˺�����������ݽ������������޸�.'))
            {
                this.submit();
            } 
            else 
            {
                return false;
            } 
          }else{
            alert("��ȷ�����");
            return false;
          }
        }
    </script>
</body>
</html>
