<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03010101.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03010101" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
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
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
    <p style="font-size:18px; text-align:center; font-weight:bold"><input id="txt_RptTime" readonly="readonly"  size="10" onclick="JTC.setday({format:'yyyy��MM��', readOnly:true})" runat="server" />���˿ڶ�̬��Ϣ���浥<һ>�������</p>
    <p><%=str_AreaName%></p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
        <tr style="text-align:center; background-color:#cccccc">
            <td colspan="13">������Ǩ�ơ��������</td>
            <td colspan="5">�������</td>
            <td rowspan="2">����</td>
        </tr>
        <tr style="text-align:center;background-color:#cccccc">
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
    <asp:Repeater ID="rep_Data" runat="server" OnItemCommand="rep_Data_ItemCommand">
      <ItemTemplate>
        <tbody>
            <tr style="text-align:center;">
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
      <tr >
        <td width="144" colspan="4" bgcolor="#cccccc">������Ǩ�ơ��������</td>
      </tr>
      <tr >
        <td align="right" bgcolor="#FFFFCC">���</td>
        <td><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td align="right" bgcolor="#FFFFCC">���ڱ�ţ�</td>
        <td><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">������</td>
        <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td align="right" bgcolor="#FFFFCC">�������֤�ţ�</td>
        <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">�䶯���ͣ�</td>
        <td>
            <asp:DropDownList ID="txtFileds05" runat="server">
                <asp:ListItem>--��ѡ��--</asp:ListItem>
                <asp:ListItem>����</asp:ListItem>
                <asp:ListItem>�ٻ�</asp:ListItem>
                <asp:ListItem>���</asp:ListItem>
                <asp:ListItem>ɥż</asp:ListItem>
                <asp:ListItem>Ǩ��</asp:ListItem>
                <asp:ListItem>Ǩ��</asp:ListItem>
                <asp:ListItem>����</asp:ListItem>
                <asp:ListItem>����</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right" bgcolor="#FFFFCC">���壺</td>
        <td>
            <asp:DropDownList ID="txtFileds06" runat="server">
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>�ɹ���</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>ά�����</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>׳��</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>��������</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>���</asp:ListItem>
            <asp:ListItem>��ɽ��</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>ˮ��</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>�¶�������</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>Ǽ��</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>ë����</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>��������</asp:ListItem>
            <asp:ListItem>ŭ��</asp:ListItem>
            <asp:ListItem>���α����</asp:ListItem>
            <asp:ListItem>����˹��</asp:ListItem>
            <asp:ListItem>���¿���</asp:ListItem>
            <asp:ListItem>�°���</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>ԣ����</asp:ListItem>
            <asp:ListItem>����</asp:ListItem>
            <asp:ListItem>��������</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>���״���</asp:ListItem>
            <asp:ListItem>������</asp:ListItem>
            <asp:ListItem>�Ű���</asp:ListItem>
            <asp:ListItem>�����</asp:ListItem>
            <asp:ListItem>��ŵ��</asp:ListItem>
            <asp:ListItem>δʶ��</asp:ListItem>
            <asp:ListItem>�����м�</asp:ListItem>
            </asp:DropDownList>
        </td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">�Ļ��̶ȣ�</td>
        <td>
            <asp:DropDownList ID="txtFileds07" runat="server">
                <asp:ListItem>Сѧ</asp:ListItem>
                <asp:ListItem>����</asp:ListItem>
                <asp:ListItem>����/ְר</asp:ListItem>
                <asp:ListItem>��ר</asp:ListItem>
                <asp:ListItem>����</asp:ListItem>
                <asp:ListItem>�о���</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right" bgcolor="#FFFFCC">�������ʣ�</td>
        <td>
            <asp:DropDownList ID="txtFileds08" runat="server">
                <asp:ListItem>��ѡ��</asp:ListItem>
                <asp:ListItem>ũҵ</asp:ListItem>
                <asp:ListItem>��ũҵ</asp:ListItem>
            </asp:DropDownList>
        </td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">����״����</td>
        <td>
            <asp:DropDownList ID="txtFileds09" runat="server">
                <asp:ListItem>����</asp:ListItem>
                <asp:ListItem>�ٻ�</asp:ListItem>
                <asp:ListItem>����</asp:ListItem>
                <asp:ListItem>���</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right" bgcolor="#FFFFCC">�������ڣ�</td>
        <td>
            <input id="txtFileds10" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
        </td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">��ס״����</td>
        <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td align="right" bgcolor="#FFFFCC">�뻧����ϵ��</td>
        <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">��ż������</td>
        <td><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td align="right" bgcolor="#FFFFCC">��������</td>
        <td><asp:Label ID="lbl_DataAreaSel" runat="server"></asp:Label></td>
      </tr>
      <tr >
        <td width="144" colspan="4" bgcolor="#cccccc">�������</td>
      </tr>
      <tr >
        <td align="right" bgcolor="#FFFFCC">������</td>
        <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td align="right" bgcolor="#FFFFCC">�Ա�</td>
        <td>
            <asp:DropDownList ID="txtFileds15" runat="server">
                <asp:ListItem>��ѡ��</asp:ListItem>
                <asp:ListItem>��</asp:ListItem>
                <asp:ListItem>Ů</asp:ListItem>
            </asp:DropDownList>
        </td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">�������£�</td>
        <td><input id="txtFileds16" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" /></td>
        <td align="right" bgcolor="#FFFFCC">�������ڣ�</td>
        <td><input id="txtFileds17" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" /></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">����ԭ��</td>
        <td colspan="3"><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
      </tr>
    </table>
    <%=js_value %>
    <!-- LFileds_type �� LFileds_txt�ɶ�-->
    <!-- LFileds_type�ж�ֵ����  0���ַ����жϣ�1�����ֻ�С����-->
    <asp:Label ID="LFileds_type" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    <!-- LFileds_txt�ж�ֵ��������ʱ��ʾ����-->
    <asp:Label ID="LFileds_txt" runat="server" Text="���,���ڱ��,����,�������֤��,�䶯����,����,�Ļ��̶�,��������,����״��,��������,��ס״��,�뻧����ϵ,��ż����,,,,," style="display:none"></asp:Label>

    
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
