<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata12050901.aspx.cs" Inherits="AreWeb.OnlineCertificate.RptInfo.chatdata12050901" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>7�����¶�ͯ�����ͽ������������</title>
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
    <p style="font-size:18px; text-align:center; font-weight:bold">7�����¶�ͯ�����ͽ������������ 
                        <asp:DropDownList runat="server" ID="txt_RptTime" >
                        </asp:DropDownList>��</p>
    <p><%=str_AreaName%></p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
       <thead>
        <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">���λ</td>
            <td colspan="4" rowspan="2">�����</td>
            <td colspan="3">��ͯ��</td>
            <td colspan="15">5�����¶�ͯ�������</td>
            <td colspan="5">6������Ӥ��ĸ��ι�����</td>
            <td colspan="12">7�����¶�ͯ��������</td>
            <td colspan="14">5�����¶�ͯӪ������</td>
            <td rowspan="3">����</td>
        </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="2">7������</td>
            <td rowspan="2">5������</td>
            <td rowspan="2">3������</td>
            <td colspan="4">5�����¶�ͯ������</td>
            <td rowspan="2">5�����¶�ͯ�����ʣ��룩</td>
            <td colspan="4">Ӥ��������</td>
            <td rowspan="2">Ӥ�������ʣ��룩</td>
            <td colspan="4">������������</td>
            <td rowspan="2">�����������ʣ��룩</td>
            <td rowspan="2">��������</td>
            <td colspan="2">ĸ��ι��</td>
            <td colspan="2">��ĸ��ι��</td>
            <td colspan="2">����������</td>
            <td colspan="2">����������ͪ��֢ɸ��</td>
            <td colspan="2">��������״�ٹ��ܼ���֢ɸ��</td>
            <td colspan="2">����������ɸ��</td>
            <td colspan="2">7�����¶�ͯ��������</td>
            <td colspan="2">3�����¶�ͯ��������</td>
            <td colspan="9">��ߣ��������ؼ��</td>
            <td colspan="5">Ѫ�쵰�׼��</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>�ϼ�</td>
            <td>��</td>
            <td>Ů</td>
            <td>�Ա���</td>
            <td>�ϼ�</td>
            <td>��</td>
            <td>Ů</td>
            <td>�Ա���</td>
            <td>�ϼ�</td>
            <td>��</td>
            <td>Ů</td>
            <td>�Ա���</td>
            <td>�ϼ�</td>
            <td>��</td>
            <td>Ů</td>
            <td>�Ա���</td>
            <td>����</td>
            <td>��%</td>
            <td>����</td>
            <td>��%</td>
            <td>����</td>
            <td>��%</td>
            <td>����</td>
            <td>��%</td>
            <td>����</td>
            <td>��%</td>
            <td>����</td>
            <td>��%</td>
            <td>����</td>
            <td>��%</td>
            <td>����</td>
            <td>��%</td>
            <td>�������</td>
            <td>����������</td>
            <td>�������ʣ�%��</td>
            <td>�����ٻ�����</td>
            <td>�����ٻ��ʣ�%��</td>
            <td>��������</td>
            <td>�����ʣ�%��</td>
            <td>��������</td>
            <td>�����ʣ�%��</td>
            <td>�������</td>
            <td>ƶѪ��������</td>
            <td>ƶѪ�����ʣ�%��</td>
            <td>���ض�ƶѪ��������</td>
            <td>���ض�ƶѪ�����ʣ�%��</td>
          </tr>
        </thead>
          <tr style="text-align:center;">
            <td>�ϼ�</td>
            <%for (int i = 0; i < arrNum.Length; i++)
              { %>
            <td><%=arrNum[i] %></td>
            <%} %>
            <td>--</td>
          </tr>

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
                <td><%# Eval("Fileds32")%></td>
                <td><%# Eval("Fileds33")%></td>
                <td><%# Eval("Fileds34")%></td>
                <td><%# Eval("Fileds35")%></td>
                <td><%# Eval("Fileds36")%></td>
                <td><%# Eval("Fileds37")%></td>
                <td><%# Eval("Fileds38")%></td>
                <td><%# Eval("Fileds39")%></td>
                <td><%# Eval("Fileds40")%></td>
                <td><%# Eval("Fileds41")%></td>
                <td><%# Eval("Fileds42")%></td>
                <td><%# Eval("Fileds43")%></td>
                <td><%# Eval("Fileds44")%></td>
                <td><%# Eval("Fileds45")%></td>
                <td><%# Eval("Fileds46")%></td>
                <td><%# Eval("Fileds47")%></td>
                <td><%# Eval("Fileds48")%></td>
                <td><%# Eval("Fileds49")%></td>
                <td><%# Eval("Fileds50")%></td>
                <td><%# Eval("Fileds51")%></td>
                <td><%# Eval("Fileds52")%></td>
                <td><%# Eval("Fileds53")%></td>
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
            <td align="right" width="150" bgcolor="#cccccc">
                ��λ��</td>
            <td colspan="3" bgcolor="#cccccc"><asp:Label ID="lbl_DataAreaSel" runat="server"></asp:Label>&nbsp;<asp:DropDownList ID="dr_DataAreaSel" runat="server"></asp:DropDownList>
            </td>
          </tr>
          <tr class="zhengwen">
            <td colspan="2" rowspan="4" align="right" bgcolor="#CCCCCC">�����</td>
            <td align="right" bgcolor="#CCCCCC">�ϼƣ�</td>
            <td><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">�У�</td>
            <td><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">Ů��</td>
            <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">�Ա�����</td>
            <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC" rowspan="3"width="150" >��ͯ��</td>
            <td colspan="2" align="right" bgcolor="#FFFFCC">7�����£�</td>
            <td ><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td colspan="2" align="right" bgcolor="#FFFFCC" >5�����£�</td>
            <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td colspan="2" align="right" bgcolor="#FFFFCC">3�����£�</td>
            <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td rowspan="15" align="right" bgcolor="#CCCCCC">5�����¶�ͯ�������</td>
            <td width="122" rowspan="4" align="right" bgcolor="#CCCCCC">5�����¶�ͯ������</td>
            <td align="right" bgcolor="#CCCCCC">�ϼƣ�</td>
            <td><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
          </tr>
          <tr class="zhengwen">
            <td width="169" align="right" bgcolor="#CCCCCC">�У�</td>
            <td><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">Ů��</td>
             <td><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">�Ա�����</td>
           <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
             <td colspan="2" align="right" bgcolor="#CCCCCC">5�����¶�ͯ�����ʣ�%����</td>
             <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>   
          </tr>
          <tr class="zhengwen">
            <td rowspan="4" align="right" bgcolor="#CCCCCC">Ӥ��������</td>
            <td align="right" bgcolor="#CCCCCC">�ϼƣ�</td>
            <td><asp:TextBox ID="txtFileds13" runat="server"  EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">�У�</td>
           <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">Ů��</td>
            <td><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">�Ա�����</td>
           <td><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
             <td colspan="2" align="right" bgcolor="#CCCCCC">Ӥ�������ʣ�%����</td>
             <td><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>   
          </tr>
          <tr class="zhengwen">
            <td rowspan="4" align="right" bgcolor="#CCCCCC">���������� </td>
            <td align="right" bgcolor="#CCCCCC">�ϼƣ�</td>
            <td><asp:TextBox ID="txtFileds18" runat="server" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">�У�</td>
           <td><asp:TextBox ID="txtFileds19" runat="server"  EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">Ů��</td>
           <td><asp:TextBox ID="txtFileds20" runat="server"  EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">�Ա�����</td>
            <td><asp:TextBox ID="txtFileds21" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
             <td colspan="2" align="right" bgcolor="#CCCCCC">�����������ʣ��룩��</td>
             <td><asp:TextBox ID="txtFileds22" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>   
          </tr>
		  <tr class="zhengwen">
            <td rowspan="5" align="right" bgcolor="#FFFFCC">6������Ӥ��ĸ��ι�����</td>
            <td colspan="2" align="right" bgcolor="#FFFFCC">����������</td>
           <td><asp:TextBox ID="txtFileds23" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#FFFFCC">ĸ��ι��</td>
            <td align="right" bgcolor="#FFFFCC">������</td>
            <td><asp:TextBox ID="txtFileds24" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">�ʣ�%����</td>
            <td><asp:TextBox ID="txtFileds25" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#FFFFCC">��ĸ��ι��</td>
            <td align="right" bgcolor="#FFFFCC">������</td>
           <td><asp:TextBox ID="txtFileds26" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>   
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">�ʣ�%����</td>
            <td><asp:TextBox ID="txtFileds27" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td rowspan="13" align="right" bgcolor="#CCCCCC">7�����¶�ͯ��������</td>
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#CCCCCC">����������</td>
            <td align="right" bgcolor="#CCCCCC">������</td>
            <td><asp:TextBox ID="txtFileds28" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">�ʣ�%����</td>
            <td><asp:TextBox ID="txtFileds29" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#CCCCCC">��������ͪ��֢ɸ��</td>
            <td align="right" bgcolor="#CCCCCC">������</td>
            <td><asp:TextBox ID="txtFileds30" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">�ʣ�%����</td>
            <td><asp:TextBox ID="txtFileds31" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#CCCCCC">��������״�ٹ��ܼ���֢ɸ��</td>
            <td align="right" bgcolor="#CCCCCC">������</td>
            <td><asp:TextBox ID="txtFileds32" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">�ʣ�%����</td>
            <td><asp:TextBox ID="txtFileds33" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#CCCCCC">����������ɸ��</td>
            <td align="right" bgcolor="#CCCCCC">������</td>
            <td><asp:TextBox ID="txtFileds34" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">�ʣ�%����</td>
            <td><asp:TextBox ID="txtFileds35" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#CCCCCC">7�����¶�ͯ��������</td>
            <td align="right" bgcolor="#CCCCCC">������</td>
            <td><asp:TextBox ID="txtFileds36" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">�ʣ�%����</td>
            <td><asp:TextBox ID="txtFileds37" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#CCCCCC">3�����¶�ͯ��������</td>
            <td align="right" bgcolor="#CCCCCC">������</td>
            <td><asp:TextBox ID="txtFileds38" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">�ʣ�%����</td>
            <td><asp:TextBox ID="txtFileds39" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>

          <tr class="zhengwen">
            <td rowspan="14" align="right" bgcolor="#FFFFCC">5�����¶�ͯӪ������</td>
            <td rowspan="9" align="right" bgcolor="#FFFFCC">��ߣ��������ؼ��</td>
            <td align="right" bgcolor="#FFFFCC">���������</td>
           <td><asp:TextBox ID="txtFileds40" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">������������</td>
            <td><asp:TextBox ID="txtFileds41" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">�������ʣ�%����</td>
            <td><asp:TextBox ID="txtFileds42" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">�����ٻ�������</td>
            <td><asp:TextBox ID="txtFileds43" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">�����ٻ��ʣ�%����</td>
            <td><asp:TextBox ID="txtFileds44" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">����������</td>
           <td><asp:TextBox ID="txtFileds45" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">�����ʣ�%����</td>
           <td><asp:TextBox ID="txtFileds46" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">����������</td>
           <td><asp:TextBox ID="txtFileds47" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">�����ʣ�%����</td>
           <td><asp:TextBox ID="txtFileds48" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td rowspan="5" align="right" bgcolor="#FFFFCC">Ѫ�쵰�׼�飺</td>
            <td align="right" bgcolor="#FFFFCC">���������</td>
            <td><asp:TextBox ID="txtFileds49" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">ƶѪ����������</td>
            <td><asp:TextBox ID="txtFileds50" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">ƶѪ�����ʣ�%����</td>
            <td><asp:TextBox ID="txtFileds51" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">���ض�ƶѪ����������</td>
            <td><asp:TextBox ID="txtFileds52" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">���ض�ƶѪ������%����</td>
            <td><asp:TextBox ID="txtFileds53" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
    </table>
    <%=js_value %>
    <!-- LFileds_type �� LFileds_txt�ɶ�-->
    <!-- LFileds_type�ж�ֵ����  0���ַ����жϣ�1�����ֻ�С����-->
    <asp:Label ID="LFileds_type" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    <!-- LFileds_txt�ж�ֵ��������ʱ��ʾ����-->
    <asp:Label ID="LFileds_txt" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    
</td></tr></table>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- ������ť m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="�� �ύ ��"  CssClass="submit6" OnClick="btnAdd_Click"></asp:Button>
<asp:HiddenField ID="hd_IsUp" runat="server"/>
<asp:HiddenField ID="hd_upId" runat="server"/>
<asp:Button ID="btnUp" runat="server" Text="�� ��� ��"  CssClass="submit6" OnClick="btnUp_Click"></asp:Button>
<input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>

</div>
    </form>
</body>
</html>
