<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03010701.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03010701" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>���룩����˿���������һ�</title>
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
    <p style="font-size:18px; text-align:center; font-weight:bold"><asp:DropDownList runat="server" ID="txt_RptTime" >
    </asp:DropDownList>����˿���������</p>
    <p><%=str_AreaName%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��λ����</p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">����</td>
            <td rowspan="3">�ڳ���ס�˿���</td>
            <td rowspan="3">��δ��ס�˿���</td>
            <td rowspan="3">��δ�ѻ����举Ů����</td>
            <td rowspan="3">��δ��ȡ������Ů֤����</td>
            <td colspan="9">�����ڡ����������ˡ���</td>
            <td rowspan="3">�������δ������</td>
            <td colspan="3">Ů�Գ���</td>
            <td rowspan="3">��������</td>
            <td rowspan="3">����</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td colspan="3">һ������</td>
            <td colspan="3">��������</td>
            <td colspan="3">�ຢ����</td>
            <td rowspan="2">����</td>
            <td rowspan="2">19�꼰��������</td>
            <td rowspan="2">23�꼰��������</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>��</td>
            <td>Ů</td>
            <td>���У��ƻ���</td>
            <td>��</td>
            <td>Ů</td>
            <td>���У��ƻ���</td>
            <td>��</td>
            <td>Ů</td>
            <td>���У��ƻ���</td>
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
                <td><%# Eval("AreaName")%></td>
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
    <td colspan="6" bgcolor="#cccccc">���룩����˿���������</td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right"bgcolor="#FFFFCC">������</td>
    <td><asp:Label ID="lbl_DataAreaSel" runat="server"></asp:Label>&nbsp;<asp:DropDownList ID="dr_DataAreaSel" runat="server" OnSelectedIndexChanged="dr_DataAreaSel_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">�ڳ���ס�˿�����</td>
    <td><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
    <td>��Դ��������ĩ��ס�˿���<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds01%></span></td>
  </tr>
  <tr class="zhengwen">
    <td  colspan="2"align="right" bgcolor="#FFFFCC">��δ��ס�˿�����</td>
    <td><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
    <td>��Դ����Ϣ����<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds02%></span></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">��δ�ѻ����举Ů������</td>
    <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
    <td>��Դ����Ϣ����<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds03%></span></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">��δ��ȡ������Ů֤������</td>
    <td colspan="3"><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
  </tr>
  <tr class="zhengwen">
    <td bgcolor="#cccccc" colspan="6">���ڳ�������</td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" rowspan="3" width="120">һ������</td>
    <td align="right" bgcolor="#FFFFCC" width="120">�У�</td>
    <td width="300"><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
    <td>��Դ����Ϣ����<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds05%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">Ů��</td>
    <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
    <td>��Դ����Ϣ����<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds06%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">���У��ƻ���</td>
    <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" rowspan="3">��������</td>
    <td align="right" bgcolor="#FFFFCC">�У�</td>
    <td><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
    <td>��Դ����Ϣ����<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds08%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">Ů��</td>
    <td><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
    <td>��Դ����Ϣ����<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds09%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">���У��ƻ���</td>
    <td><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" rowspan="3">�ຢ����</td>
    <td align="right" bgcolor="#FFFFCC">�У�</td>
    <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
    <td>��Դ����Ϣ����<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds11%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">Ů��</td>
    <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
    <td>��Դ����Ϣ����<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds12%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">���У��ƻ���</td>
    <td><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">�������δ��������</td>
    <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" rowspan="3">Ů�Գ���</td>
    <td align="right" bgcolor="#FFFFCC">������</td>
    <td><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
    <td>��Դ����Ϣ����<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds15%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">19�꼰����������</td>
    <td><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
    <td>��Դ����Ϣ����<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds16%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">23�꼰����������</td>
    <td><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
    <td>��Դ����Ϣ����<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds17%></span></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">����������</td>
    <td><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
    <td>��Դ����Ϣ����<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds18%></span></td>
  </tr>    
</table>
    <%=js_value %>
    <!-- LFileds_type �� LFileds_txt�ɶ�-->
    <!-- LFileds_type�ж�ֵ����  0���ַ����жϣ�1�����ֻ�С����-->
    <asp:Label ID="LFileds_type" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    <!-- LFileds_txt�ж�ֵ��������ʱ��ʾ����-->
    <asp:Label ID="LFileds_txt" runat="server" Text="�ڳ���ס�˿���,��δ��ס�˿���,��δ�ѻ����举Ů����,��δ��ȡ������Ů֤����,���ڳ�������һ����������,���ڳ�������һ������Ů��,���ڳ�������һ�����������мƻ���,���ڳ�������������������,���ڳ���������������Ů��,���ڳ��������������������мƻ���,���ڳ��������ຢ��������,���ڳ��������ຢ����Ů��,���ڳ��������ຢ���������мƻ���,�������δ������,Ů�Գ�������,19�꼰��������,23�꼰��������,��������" style="display:none"></asp:Label>
    
</td></tr></table>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- ������ť m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="�� �ύ ��"  CssClass="submit6" OnClick="btnAdd_Click"></asp:Button>
<asp:HiddenField ID="hd_IsUp" runat="server"/>
<asp:HiddenField ID="hd_upId" runat="server"/>
<asp:Button ID="btnUp" runat="server" Text="�� �ϱ� ��"  CssClass="submit6" OnClick="btnUp_Click"></asp:Button>
<input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>

</div>
    </form>
</body>
</html>
