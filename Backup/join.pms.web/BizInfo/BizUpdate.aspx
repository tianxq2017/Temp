<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizUpdate.aspx.cs" Inherits="join.pms.web.BizInfo.BizUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ҵ��״̬���</title>
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ��������ҵ����,�������ѯ����ť�˶�ҵ����Ϣ��Ȼ��ѡ����״̬������ȷ���ύ����ť���ɣ�</div>
<!-- ҳ����� -->
<input type="hidden" name="txtBizIDH" id="txtBizIDH" value="" runat="server" style="display:none;"/>
<input type="hidden" name="oldAttribs" id="oldAttribs" value="" runat="server" style="display:none;"/>
<!-- �������� Start-->
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- �༭��  -->
<div class="form_bg">
 <div  class="form_a">
  <p class="form_title"><b>ҵ��״̬���</b></p>
  <div class="form_table">
	      <table width="0" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <th><span class="xing">*</span>ҵ����룺</th>
                <td class="text"><p><span><asp:TextBox ID="txtBizID" runat="server" EnableViewState="False" MaxLength="10" Width="100px"/></span></p>&nbsp;&nbsp;
    <asp:Button ID="btnAdd" runat="server" Text="��ѯ" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>&nbsp;&nbsp;
    <asp:DropDownList ID="ddlType" runat="server" >  
                            <asp:ListItem Value="" Selected>��ѡ����״̬</asp:ListItem>  
                            <asp:ListItem Value="0">��ʼ�ύ</asp:ListItem>
                            <asp:ListItem Value="1">������</asp:ListItem>
                            <asp:ListItem Value="2">ͨ�����Ѱ�ᣩ</asp:ListItem>  
                            <asp:ListItem Value="4">ɾ��/����</asp:ListItem>
                            <asp:ListItem Value="5">ע��</asp:ListItem>
                            <asp:ListItem Value="8">ȷ�ϴ�ӡ</asp:ListItem>
                            <asp:ListItem Value="9">�鵵</asp:ListItem>
                        </asp:DropDownList>
    &nbsp;&nbsp;
    <asp:Button ID="btnUpdate" runat="server" Text="ȷ���ύ" CssClass="submit6" OnClick="btnUpdate_Click"></asp:Button></td>
            </tr>
            </table></div>
 </div>             
<asp:Literal ID="LiteralBizData" runat="server"></asp:Literal>                    
          
<!-- �༭�� -->
 </div>
<!--end------------------------>
<br/><br/>
<!-- ������ť -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0" align="center" class="zhengwen">
<tr><td align="right" style="width: 132px; height: 50px;">&nbsp;</td><td width="*" align="left" style="height: 25px">


</td></tr></table>
</td></tr></table>
<!-- �������� End--></div>
</form>

</body>
</html>
