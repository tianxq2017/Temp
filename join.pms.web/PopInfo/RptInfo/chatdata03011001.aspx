<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03011001.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03011001" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>���룩���ѻ����举Ů���Ρ�������� һ���ũ�壩</title>
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
    </asp:DropDownList>���ѻ����举Ů���Ρ����������ũ�壩 </p>
    <p><%=str_AreaName%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��λ���ˡ���</p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="4">����</td>
            <td rowspan="4">���举Ů����</td>
            <td colspan="20">�ѻ����举Ů�������</td>
            <td rowspan="4">����</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">�ϼ�</td>
            <td rowspan="3">�㺢��Ů</td>
            <td colspan="6">һ����Ů</td>
            <td colspan="7">������Ů</td>
            <td colspan="5">�ຢ��Ů</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="2">����</td>
            <td rowspan="2">���У����� ����</td>
            <td colspan="2">�ϻ���</td>
            <td rowspan="2">Ƥ������</td>
            <td rowspan="2">ҩ������</td>
            <td rowspan="2">����</td>
            <td rowspan="2">���У�˫Ů ����</td>
            <td colspan="2">������</td>
            <td rowspan="2">�ϻ�����</td>
            <td rowspan="2">Ƥ������</td>
            <td rowspan="2">ҩ������</td>
            <td rowspan="2">����</td>
            <td colspan="4">����</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>С��</td>
            <td>���У�����  ����</td>
            <td>С��</td>
            <td>���У�˫Ů  ����</td>
            <td>��������</td>
            <td>�ϻ�����</td>
            <td>Ƥ������</td>
            <td>ҩ������</td>
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
                <td><%# Eval("Fileds19")%></td>
                <td><%# Eval("Fileds20")%></td>
                <td><%# Eval("Fileds21")%></td>
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
    <td  align="right" bgcolor="#FFFFCC" colspan="4">������</td>
    <td colspan="2"><asp:Label ID="lbl_DataAreaSel" runat="server"></asp:Label>&nbsp;<asp:DropDownList ID="dr_DataAreaSel" runat="server" AutoPostBack="true"></asp:DropDownList></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" colspan="4">���举Ů������</td>
    <td width="300"><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
    <td>��Դ����Ϣ����<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds01%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" rowspan="20" width="150">�ѻ����举Ů�������</td>
    <td align="right" bgcolor="#FFFFCC" colspan="3">�ϼƣ�</td>
    <td><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds02','txtFileds04,txtFileds10,txtFileds17');" /></td>
    <td>��Դ����Ϣ����<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds02%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" colspan="3">�㺢��Ů��</td>
    <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds02','txtFileds03');"/></td>
    <td>��Դ����Ϣ����<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds03%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" rowspan="6" width="80">һ����Ů</td>
    <td align="right" bgcolor="#FFFFCC" colspan="2">������</td>
    <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds02','txtFileds04,txtFileds10,txtFileds17');"/></td>
    <td>��Դ��ҵ������<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds04%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" colspan="2">���У����� ������</td>
    <td><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds04','txtFileds05,txtFileds06,txtFileds07,txtFileds08,txtFileds09');"/></td>
    <td>��Դ��ҵ������<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds05%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" rowspan="2" width="60">�ϻ���</td>
    <td align="right" bgcolor="#FFFFCC" width="120">С�ƣ�</td>
    <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds04','txtFileds05,txtFileds06,txtFileds07,txtFileds08,txtFileds09');"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">���У�����  ������</td>
    <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds04','txtFileds05,txtFileds06,txtFileds07,txtFileds08,txtFileds09');"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" colspan="2">Ƥ��������</td>
    <td><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds04','txtFileds05,txtFileds06,txtFileds07,txtFileds08,txtFileds09');"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" colspan="2">ҩ��������</td>
    <td><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds04','txtFileds05,txtFileds06,txtFileds07,txtFileds08,txtFileds09');"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" rowspan="7">������Ů</td>
    <td align="right" bgcolor="#FFFFCC" colspan="2">������</td>
    <td><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds02','txtFileds04,txtFileds10,txtFileds17');"/></td>
    <td>��Դ��ҵ������<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds10%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" colspan="2">���У�˫Ů ������</td>
    <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds10','txtFileds11,txtFileds12,txtFileds13,txtFileds14,txtFileds15,txtFileds16');"/></td>
    <td>��Դ��ҵ������<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds11%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" rowspan="2">������</td>
    <td align="right" bgcolor="#FFFFCC">С�ƣ�</td>
    <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds10','txtFileds11,txtFileds12,txtFileds13,txtFileds14,txtFileds15,txtFileds16');"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">���У�˫Ů  ������</td>
    <td><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds10','txtFileds11,txtFileds12,txtFileds13,txtFileds14,txtFileds15,txtFileds16');"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" colspan="2">�ϻ�������</td>
    <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds10','txtFileds11,txtFileds12,txtFileds13,txtFileds14,txtFileds15,txtFileds16');"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" colspan="2">Ƥ��������</td>
    <td><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds10','txtFileds11,txtFileds12,txtFileds13,txtFileds14,txtFileds15,txtFileds16');"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" colspan="2">ҩ��������</td>
    <td><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds10','txtFileds11,txtFileds12,txtFileds13,txtFileds14,txtFileds15,txtFileds16');"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" rowspan="5">�ຢ��Ů</td>
    <td align="right" bgcolor="#FFFFCC" colspan="2">������</td>
    <td><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds02','txtFileds04,txtFileds10,txtFileds17');"/></td>
    <td>��Դ��ҵ������<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds17%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" rowspan="4">����</td>
    <td align="right" bgcolor="#FFFFCC">����������</td>
    <td><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds17','txtFileds18,txtFileds20,txtFileds21');"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">�ϻ�������</td>
    <td><asp:TextBox ID="txtFileds19" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds17','txtFileds18,txtFileds20,txtFileds21');"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">Ƥ��������</td>
    <td><asp:TextBox ID="txtFileds20" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds17','txtFileds18,txtFileds20,txtFileds21');"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">ҩ��������</td>
    <td><asp:TextBox ID="txtFileds21" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds17','txtFileds18,txtFileds20,txtFileds21');"/></td>
  </tr>
</table>
<%=js_value %>

    <!-- LFileds_type �� LFileds_txt�ɶ�-->
    <!-- LFileds_type�ж�ֵ����  0���ַ����жϣ�1�����ֻ�С����-->
    <asp:Label ID="LFileds_type" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    <!-- LFileds_txt�ж�ֵ��������ʱ��ʾ����-->
    <asp:Label ID="LFileds_txt" runat="server" Text="���举Ů����,�ϼ�,�㺢��Ů,һ����Ů����,һ����Ů���ж��л���,һ����Ů�ϻ���С��,һ����Ů�ϻ������ж��л���,һ����ŮƤ������,һ����Ůҩ������,������Ů����,������Ů����˫Ů����,������Ů������С��,������Ů����������˫Ů����,������Ů�ϻ�����,������ŮƤ������,������Ůҩ������,�ຢ��Ů����,�ຢ��Ů��������,�ຢ��Ů�ϻ�����,�ຢ��ŮƤ������,�ຢ��Ůҩ������" style="display:none"></asp:Label>
    
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
