<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03020101.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03020101" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Ԥ�����̲�ĸӤ���������±���</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/index.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" src="/includes/jquery-1.9.1.min.js" type="text/javascript"></script>
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
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
    <p style="font-size:18px; text-align:center; font-weight:bold">Ԥ�����̲�ĸӤ���������±���<input id="txt_RptTime" readonly="readonly"  size="10" onclick="JTC.setday({format:'yyyy��MM��', readOnly:true})" runat="server" />��</p>
    <p><%=str_AreaName%></p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
        <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">���λ</td>
            <td colspan="8">��ǰ����</td>
            <td colspan="4">����</td>
            <td colspan="9">סԺ����</td>
            <td colspan="8">��סԺ����</td>
            <td rowspan="3">������������</td>
            <td rowspan="3">�����������</td>
            <td rowspan="3">����</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td colspan="2">���ܻ�ǰ��������</td>
            <td colspan="2">���ܰ��̲���ѯ����</td>
            <td colspan="2">����HIV����������</td>
            <td colspan="2">HIV������������</td>
            <td rowspan="2">���ܳ��β�ǰ�������и���</td>
            <td rowspan="2">���ܰ��̲���ѯ�и���</td>
            <td rowspan="2">����HIV�������и���</td>
            <td rowspan="2">HIV���������и���</td>
            <td rowspan="2">סԺ���������</td>
            <td rowspan="2">���ڽ��ܰ��̲���ѯ������</td>
            <td rowspan="2">���ڽ���HIV�����������</td>
            <td rowspan="2">����ʱ���ܰ��̲���ѯ������</td>
            <td rowspan="2">����ʱ����HIV�����������</td>
            <td rowspan="2">����ʱHIV���������Բ�����</td>
            <td rowspan="2">HIV�������Բ�������</td>
            <td rowspan="2">סԺ��������</td>
            <td rowspan="2">HIV�������Բ������������</td>
            <td rowspan="2">��סԺ���������</td>
            <td rowspan="2">���ڽ��ܰ��̲���ѯ������</td>
            <td rowspan="2">���ڽ���HIV�����������</td>
            <td rowspan="2">����ʱ����HIV�����������</td>
            <td rowspan="2">����ʱHIV���������Բ�����</td>
            <td rowspan="2">HIV�������Բ�����</td>
            <td rowspan="2">��סԺ��������</td>
            <td rowspan="2">HIV�������Բ������������</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>��</td>
            <td>Ů</td>
            <td>��</td>
            <td>Ů</td>
            <td>��</td>
            <td>Ů</td>
            <td>��</td>
            <td>Ů</td>
          </tr>
           <tr style="text-align:center;">
            <td>�ϼ�</td>
             <%for (int i = 0; i < arrNum.Length; i++)
              { %>
            <td><%=arrNum[i] %></td>
            <%} %>
            <td>--</td>
          </tr>
        </thead>
    <asp:Repeater ID="rep_Data" runat="server" OnItemCommand="rep_Data_ItemCommand">
      <ItemTemplate>
        <tbody>
            <tr style="text-align:center;">
                <td width="100"><%# Eval("AreaName")%></td>
                <td><%# Eval("Fileds01")%></td>
                <td><%# Eval("Fileds02")%></td>
                <td><%# Eval("Fileds03")%></td>
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
                <td><%# Eval("Fileds14")%></td>
                <td><%# Eval("Fileds15")%></td>
                <td><%# Eval("Fileds16")%></td>
                <td><%# Eval("Fileds17")%></td>
                <td><%# Eval("Fileds18")%></td>
                <td><%# Eval("Fileds19")%></td>
                <td><%# Eval("Fileds20")%></td>                
                <td><%# Eval("Fileds21")%></td>
                <td><%# Eval("Fileds22")%></td>
                <td><%# Eval("Fileds23")%></td>
                <td><%# Eval("Fileds24")%></td>
                <td><%# Eval("Fileds25")%></td>
                <td><%# Eval("Fileds26")%></td>
                <td><%# Eval("Fileds27")%></td>
                <td><%# Eval("Fileds28")%></td>
                <td><%# Eval("Fileds29")%></td>
                <td><%# Eval("Fileds30")%></td>
                <td><%# Eval("Fileds31")%></td>
               <td width="70">
                    <asp:LinkButton ID="lbt_Update" runat="server" CommandName="Update" CommandArgument='<%#Eval("CommID") %>' Text="�༭" /> | 
                    <asp:LinkButton ID="lbt_Delete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("CommID") %>' Text="ɾ��" />
               </td>
            </tr>           
          </tbody>
    </ItemTemplate>  
    </asp:Repeater>
    </table>
     <p>�����ˣ�<asp:TextBox ID="txt_SldHeader" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;����ˣ�<asp:TextBox ID="txt_SldLeader" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;����ڣ�<input id="txt_OprateDate" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" /></p>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
</td></tr></table>
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellspacing="3" cellpadding="1" class="zhengwen">
  <tr class="zhengwen">
    <td align="right" width="100" bgcolor="#cccccc">��λ��</td>
    <td colspan="3" bgcolor="#cccccc"><asp:Label ID="lbl_DataAreaSel" runat="server"></asp:Label>&nbsp;<asp:DropDownList ID="dr_DataAreaSel" runat="server"></asp:DropDownList></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="8" align="right" bgcolor="#FFFFCC">��ǰ����</td>
    <td rowspan="2" align="right" bgcolor="#FFFFCC">���ܻ�ǰ��������</td>
    <td align="right" bgcolor="#FFFFCC">�У�</td>
    <td><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">Ů��</td>
    <td><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">���ܰ��̲���ѯ����</td>
    <td align="right" bgcolor="#FFFFCC">�У�</td>
    <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">Ů��</td>
    <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2"  width="200" align="right" bgcolor="#FFFFCC">����HIV����������</td>
    <td align="right"  width="100" bgcolor="#FFFFCC">�У�</td>
    <td><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">Ů��</td>
    <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">HIV������������</td>
    <td align="right" bgcolor="#FFFFCC">�У�</td>
    <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">Ů��</td>
    <td><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="4" align="right" bgcolor="#cccccc">����</td>
    <td colspan="2" align="right" bgcolor="#cccccc">���ܳ��β�ǰ�������и�����</td>
    <td><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">���ܰ��̲���ѯ�и�����</td>
    <td><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">����HIV�������и�����</td>
    <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">HIV���������и�����</td>
    <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="9" align="right" bgcolor="#FFFFCC">סԺ����</td>
    <td colspan="2" align="right" bgcolor="#FFFFCC">סԺ�����������</td>
    <td><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">���ڽ��ܰ��̲���ѯ��������</td>
    <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">���ڽ���HIV�������������</td>
    <td><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">����ʱ���ܰ��̲���ѯ��������</td>
    <td><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">����ʱ����HIV�������������</td>
    <td><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">����ʱHIV���������Բ�������</td>
    <td><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">HIV�������Բ���������</td>
    <td><asp:TextBox ID="txtFileds19" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">סԺ����������</td>
    <td><asp:TextBox ID="txtFileds20" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">HIV�������Բ��������������</td>
    <td><asp:TextBox ID="txtFileds21" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="8" align="right" bgcolor="#cccccc">��סԺ����</td>
    <td colspan="2" align="right" bgcolor="#cccccc">��סԺ�����������</td>
    <td><asp:TextBox ID="txtFileds22" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">���ڽ��ܰ��̲���ѯ��������</td>
    <td><asp:TextBox ID="txtFileds23" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">���ڽ���HIV�������������</td>
    <td><asp:TextBox ID="txtFileds24" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">����ʱ����HIV�������������</td>
    <td><asp:TextBox ID="txtFileds25" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">����ʱHIV���������Բ�������</td>
    <td><asp:TextBox ID="txtFileds26" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">HIV�������Բ�������</td>
    <td><asp:TextBox ID="txtFileds27" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">��סԺ����������</td>
    <td><asp:TextBox ID="txtFileds28" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">HIV�������Բ��������������</td>
    <td><asp:TextBox ID="txtFileds29" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="3" align="right" bgcolor="#FFFFCC">��������������</td>
    <td><asp:TextBox ID="txtFileds30" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="3" align="right" bgcolor="#FFFFCC">�������������</td>
    <td><asp:TextBox ID="txtFileds31" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
</table>
    <%=js_value %>
    <!-- LFileds_type �� LFileds_txt�ɶ�-->
    <!-- LFileds_type�ж�ֵ����  0���ַ����жϣ�1�����ֻ�С����-->
    <asp:Label ID="LFileds_type" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    <!-- LFileds_txt�ж�ֵ��������ʱ��ʾ����-->
    <asp:Label ID="LFileds_txt" runat="server"  Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>

</td></tr></table>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- ������ť m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="�� �ύ ��"  CssClass="submit6" OnClick="btnAdd_Click"></asp:Button>
<asp:HiddenField ID="hd_IsUp" runat="server"/>
<asp:HiddenField ID="hd_upId" runat="server"/>
<input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>

</div>
    </form>
</body>
</html>
