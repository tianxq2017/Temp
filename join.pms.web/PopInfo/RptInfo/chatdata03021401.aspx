<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03021401.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03021401" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>�ƻ������±���һ�</title>
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
    <p style="font-size:18px; text-align:center; font-weight:bold">�ƻ������±���<input id="txt_RptTime" readonly="readonly"  size="10" onclick="JTC.setday({format:'yyyy��MM��', readOnly:true})" runat="server" />�£�</p>
    <p><%=str_UnitName%></p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
        <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="4">��λ</td>
            <td colspan="6">����</td>
            <td rowspan="4">��������</td>
            <td colspan="6">����</td>
            <td colspan="8">����</td>
            <td rowspan="4">��֤����</td>
            <td colspan="2">Ů�Գ�������</td>
            <td rowspan="4">����</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">�� ��</td>
            <td colspan="5">����</td>
            <td rowspan="3">����</td>
            <td colspan="5">����</td>
            <td rowspan="3">�ϼ�</td>
            <td colspan="7">����</td>
            <td rowspan="3">�ϼ�</td>
            <td rowspan="3">���ж�ʮ�������Ͻ����</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td colspan="2">�ƻ���</td>
            <td colspan="3">�ƻ���</td>
            <td colspan="2">�ƻ���</td>
            <td colspan="3">�ƻ���</td>
            <td rowspan="2">����</td>
            <td rowspan="2">Ů��</td>
            <td colspan="2">�ϻ�</td>
            <td rowspan="2">ȡ��</td>
            <td rowspan="2">����</td>
            <td rowspan="2">����</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>һ��</td>
            <td>����</td>
            <td>һ��</td>
            <td>����</td>
            <td>�ຢ</td>
            <td>һ��</td>
            <td>����</td>
            <td>һ��</td>
            <td>����</td>
            <td>�ຢ</td>
            <td>С��</td>
            <td>����һ���ϻ�</td>
          </tr>
          <tr style="text-align:center;">
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
            <td><%=num21%></td>
            <td><%=num22%></td>
            <td><%=num23%></td>
            <td><%=num24%></td>
            <td>--</td>
          </tr>
        </thead>
    <asp:Repeater ID="rep_Data" runat="server" OnItemCommand="rep_Data_ItemCommand">
      <ItemTemplate>
        <tbody>
            <tr style="text-align:center;">
                <td><%# Eval("AreaName")%></td>
                <td> <%#GetNumByCol(Eval("Fileds01"), Eval("Fileds02"), Eval("Fileds03"),Eval("Fileds04"),Eval("Fileds05"))%> </td>
                <td><%# Eval("Fileds01")%></td>
                <td><%# Eval("Fileds02")%></td>
                <td><%# Eval("Fileds03")%></td>
                <td><%# Eval("Fileds04")%></td>
                <td><%# Eval("Fileds05")%></td>
                <td><%# Eval("Fileds06")%></td>
                <td> <%#GetNumByCol(Eval("Fileds07"), Eval("Fileds08"), Eval("Fileds09"),Eval("Fileds10"),Eval("Fileds11"))%> </td>
                <td><%# Eval("Fileds07")%></td>
                <td><%# Eval("Fileds08")%></td>
                <td><%# Eval("Fileds09")%></td>
                <td><%# Eval("Fileds10")%></td>
                <td><%# Eval("Fileds11")%></td>
                 <td> <%#GetNumByCol(Eval("Fileds12"), Eval("Fileds13"), Eval("Fileds14"), Eval("Fileds15"), Eval("Fileds16"), Eval("Fileds17"))%> </td>
                <td><%# Eval("Fileds12")%></td>
                <td><%# Eval("Fileds13")%></td>
                <td><%# Eval("Fileds14")%></td>
                <td><%# Eval("Fileds14")%></td>
                <td><%# Eval("Fileds15")%></td>
                <td><%# Eval("Fileds16")%></td>
                <td><%# Eval("Fileds17")%></td>
                <td><%# Eval("Fileds18")%></td>
                <td><%# Eval("Fileds19")%></td>
                <td><%# Eval("Fileds19")%></td>
               <td>
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
            <td align="right" bgcolor="#FFFFCC" rowspan="5">����</td>
            <td align="right" bgcolor="#FFFFCC" width="120" rowspan="2">�ƻ��ڣ�</td>
            <td align="right" bgcolor="#FFFFCC" width="120">һ����</td>
            <td><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">������</td>
            <td><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC" rowspan="3">�ƻ��⣺</td>
            <td align="right" bgcolor="#FFFFCC">һ����</td>
            <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">������</td>
            <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">�ຢ��</td>
            <td><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#cccccc">����������</td>
            <td colspan="3" bgcolor="#cccccc"><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC" rowspan="5">����</td>
            <td align="right" bgcolor="#FFFFCC" rowspan="2">�ƻ��ڣ�</td>
            <td align="right" bgcolor="#FFFFCC">һ̥��</td>
            <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">��̥��</td>
            <td><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC" rowspan="3">�ƻ��⣺</td>
            <td align="right" bgcolor="#FFFFCC">һ̥��</td>
            <td><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">��̥��</td>
            <td><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">��̥��</td>
            <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC" rowspan="6">����</td>
            <td align="right" bgcolor="#FFFFCC" colspan="2">������</td>
            <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC" colspan="2">Ů����</td>
            <td><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">�ϻ���</td>
            <td align="right" bgcolor="#FFFFCC">����һ���ϻ���</td>
            <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC" colspan="2">ȡ����</td>
            <td><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC" colspan="2">������</td>
            <td><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC" colspan="2">������</td>
            <td><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#cccccc">��֤������</td>
            <td colspan="3" bgcolor="#cccccc"><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">Ů�Գ�������</td>
            <td align="right" colspan="2" bgcolor="#FFFFCC">���ж�ʮ�������Ͻ������</td>
            <td colspan="2"><asp:TextBox ID="txtFileds19" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
    </table>
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
