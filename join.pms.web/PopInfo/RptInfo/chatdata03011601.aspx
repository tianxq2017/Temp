<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03011601.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03011601" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>�������ʡ�ƻ�������ͥ���ͳ�Ʊ�����һ�</title>
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
    <p style="font-size:18px; text-align:center; font-weight:bold"><input id="txt_RptTime" readonly="readonly"  size="4" onclick="JTC.setday({format:'yyyy��', readOnly:true})" runat="server" />�������ʡ�ƻ�������ͥ���ͳ�Ʊ�����</p>
    <p><%=str_AreaName%></p>    
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="4">��λ</td>
            <td colspan="9">һ����</td>
            <td colspan="4">������̥����ָ�껧</td>
            <td rowspan="4">����</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">�ϼ�</td>
            <td colspan="4">16�����£���16�꣩</td>
            <td colspan="4">16������</td>
            <td colspan="2">�ۼƻ���</td>
            <td colspan="2">�����</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td colspan="2">���л�</td>
            <td colspan="2">��Ů��</td>
            <td colspan="2">���л�</td>
            <td colspan="2">��Ů��</td>
            <td rowspan="2">�ܻ���</td>
            <td rowspan="2">�������ܽ�������</td>
            <td rowspan="2">�ܻ���</td>
            <td rowspan="2">�������ܽ�������</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>С��</td>
            <td>������֤</td>
            <td>С��</td>
            <td>������֤</td>
            <td>С��</td>
            <td>������֤</td>
            <td>С��</td>
            <td>������֤</td>
          </tr>
          <tr style="text-align:center;">
            <td>ȫ��ϼ�</td>
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
</td></tr></table>
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellspacing="3" cellpadding="1" class="zhengwen">
   <tr class="zhengwen">
    <td colspan="4" bgcolor="#cccccc">һ����</td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" width="260">��λ��</td>
    <td width="300"><asp:Label ID="lbl_DataAreaSel" runat="server"></asp:Label>&nbsp;<asp:DropDownList ID="dr_DataAreaSel" runat="server" AutoPostBack="true"></asp:DropDownList></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">�ϼƣ�</td>
    <td><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds01','txtFileds02,txtFileds04,txtFileds06,txtFileds08');"/></td>
    <td>��Դ��ҵ������<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds01%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16������(��16��))���л�-С�ƣ�</td>
    <td><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px" onBlur="get_Val('txtFileds02','txtFileds03');" Text="0"/></td>
    <td>��Դ��ҵ������<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds02%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16������(��16��))���л�-������֤��</td>
    <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
    <td>��Դ��ҵ������<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds03%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16������(��16��))��Ů��-С�ƣ�</td>
    <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px" onBlur="get_Val('txtFileds04','txtFileds05');" Text="0"/></td>
    <td>��Դ��ҵ������<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds04%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16������(��16��))��Ů��-������֤��</td>
    <td><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
    <td>��Դ��ҵ������<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds05%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16������)���л�-С�ƣ�</td>
    <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px" onBlur="get_Val('txtFileds06','txtFileds07');" Text="0"/></td>
    <td>��Դ��ҵ������<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds06%></span></td>
 </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16������)���л�-������֤��</td>
    <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
    <td>��Դ��ҵ������<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds07%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16������)��Ů��-С�ƣ�</td>
    <td><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px" onBlur="get_Val('txtFileds08','txtFileds09');" Text="0"/></td>
    <td>��Դ��ҵ������<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds08%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16������)��Ů��-������֤��</td>
    <td><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
    <td>��Դ��ҵ������<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds09%></span></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="4" bgcolor="#cccccc">������̥����ָ�껧</td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">�ۼƻ���-�ܻ�����</td>
    <td><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px" onBlur="get_Val('txtFileds10','txtFileds11');" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">�ۼƻ���-�������ܽ���������</td>
    <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">2012��-�ܻ�����</td>
    <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px" onBlur="get_Val('txtFileds12','txtFileds13');" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">2012��-�������ܽ���������</td>
    <td><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
</table>

    <%=js_value %>
    <!-- LFileds_type �� LFileds_txt�ɶ�-->
    <!-- LFileds_type�ж�ֵ����  0���ַ����жϣ�1�����ֻ�С����-->
    <asp:Label ID="LFileds_type" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    <!-- LFileds_txt�ж�ֵ��������ʱ��ʾ����-->
    <asp:Label ID="LFileds_txt" runat="server" Text="�ϼ�,(16������(��16��))���л�-С��,(16������(��16��))���л�-������֤,(16������(��16��))��Ů��-С��,(16������(��16��))��Ů��-������֤,(16������)���л�-С��,(16������)���л�-������֤,(16������)��Ů��-С��,(16������)��Ů��-������֤,�ۼƻ���-�ܻ���,�ۼƻ���-�������ܽ�������,2012��-�ܻ���,2012��-�������ܽ�������" style="display:none"></asp:Label>
    
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
