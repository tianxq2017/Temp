<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditBiz0150.aspx.cs" Inherits="join.pms.web.BizInfo.EditBiz0150" %>
<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc3" %>
<%@ Register Src="/userctrl/ucAreaSelXian.ascx" TagName="ucAreaSelXian" TagPrefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>�ޱ���ҳ</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
    <script src="/Scripts/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jsdate.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ������һ��д������Ϣ�󣻵�����ύ����ť����</div>
<!-- ҳ����� -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtAttribs" id="txtAttribs" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- �༭��  -->
 <div class="form_bg">  	
	    <div class="form_a">
	      <p class="form_title"><b>Ů����Ϣ</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>Ů��������</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameB" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>֤�����</th>
    <td class="select"><asp:DropDownList ID="ddlCidTypeB" runat="server">
                            <asp:ListItem Value="1">���֤</asp:ListItem>
                            <asp:ListItem Value="2">����֤</asp:ListItem>
                            <asp:ListItem Value="3">����</asp:ListItem>
                            <asp:ListItem Value="4">����</asp:ListItem>
                        </asp:DropDownList></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>֤�����룺</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18"  onblur="CheckCID_1('ddlCidTypeB','1','txtPersonCidB');"/></span></p>
    </td>
  </tr>
    <tr>
        <th>
            <span class="xing">*</span>���壺</th>
        <td class="select">
            <asp:Literal ID="LiteralNationsB" runat="server" EnableViewState="false"></asp:Literal>
            <input type="hidden" name="txtHNationsB" id="txtHNationsB" runat="server" style="display:none;"/></td>
    </tr>
    <tr>
        <th>
            <span class="xing">*</span>������λ��</th>
        <td class="text">
            <p><span><asp:TextBox ID="txtWorkUnitB" runat="server" EnableViewState="False" MaxLength="25"/></span></p><b class="ps">�޵�λ����д���ޡ�</b></td>
    </tr>
  <tr>
    <th><span class="xing">*</span>�������ʣ�</th>
    <td class="select"><asp:DropDownList ID="ddlRegisterTypeB" runat="server">
                                    <asp:ListItem>ũҵ</asp:ListItem>
                                    <asp:ListItem>��ũҵ</asp:ListItem>
                                    <asp:ListItem>����</asp:ListItem>            
                       </asp:DropDownList></td>
  </tr> 
    <tr>
    <th><span class="xing">*</span>������ַ��</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelRegB" runat="server" /></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>��ס��ַ��</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurB" runat="server" /><input type="button" name="button" value="ͬ������" class="button" onclick="AreaCodeTB('UcAreaSelRegB_txtSelAreaCode','UcAreaSelRegB_txtSelectArea','UcAreaSelRegB_txtArea','UcAreaSelCurB_txtSelAreaCode','UcAreaSelCurB_txtSelectArea','UcAreaSelCurB_txtArea');"/></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>�����˿ڣ�</th>
    <td class="select"><asp:DropDownList ID="ddlFileds23" runat="server">
                                    <asp:ListItem>��</asp:ListItem>
                                    <asp:ListItem>��</asp:ListItem>          
                       </asp:DropDownList></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>����״����</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeB" runat="server">                           
                            <asp:ListItem>����</asp:ListItem>
                            <asp:ListItem>����</asp:ListItem>
                            <asp:ListItem>�ٻ�</asp:ListItem>
                            <asp:ListItem>���</asp:ListItem>
                            <asp:ListItem>ɥż</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>
  </table>	    
		  </div>
	    </div>  
	    <div class="form_a">
	      <p class="form_title"><b>�з���Ϣ</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>�ɷ�������</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameA" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>֤�����</th>
    <td class="select"><asp:DropDownList ID="ddlCidTypeA" runat="server">
                            <asp:ListItem Value="1">���֤</asp:ListItem>
                            <asp:ListItem Value="2">����֤</asp:ListItem>
                            <asp:ListItem Value="3">����</asp:ListItem>
                            <asp:ListItem Value="4">����</asp:ListItem>
                        </asp:DropDownList></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>֤�����룺</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18"  onblur="CheckCID_1('ddlCidTypeA','0','txtPersonCidA');"/></span></p>
    </td>
  </tr> 
    <tr>
        <th>
            <span class="xing">*</span>���壺</th>
        <td class="select">
            <asp:Literal ID="LiteralNationsA" runat="server" EnableViewState="false"></asp:Literal>
            <input type="hidden" name="txtHNationsA" id="txtHNationsA" runat="server" style="display:none;"/>
            </td>
    </tr>
    <tr>
        <th>
            <span class="xing">*</span>������λ��</th>
        <td class="text">
            <p><span><asp:TextBox ID="txtWorkUnitA" runat="server" EnableViewState="False" MaxLength="25"/></span></p><b class="ps">�޵�λ����д���ޡ�</b></td>
    </tr>
  <tr>
    <th><span class="xing">*</span>�������ʣ�</th>
    <td class="select"><asp:DropDownList ID="ddlRegisterTypeA" runat="server">
                                    <asp:ListItem>ũҵ</asp:ListItem>
                                    <asp:ListItem>��ũҵ</asp:ListItem>
                                    <asp:ListItem>����</asp:ListItem>            
                       </asp:DropDownList></td>
  </tr> 
    <tr>
        <th>
            <span class="xing">*</span>������ַ��</th>
        <td class="select select_auto">
            <uc3:ucAreaSel ID="UcAreaSelRegA" runat="server" />
        </td>
    </tr>
    <tr>
        <th>
            <span class="xing">*</span>�־�ס��ַ��</th>
        <td class="select select_auto">
            <uc3:ucAreaSel ID="UcAreaSelCurA" runat="server" /><input type="button" name="button" value="ͬ������" class="button" onclick="AreaCodeTB('UcAreaSelRegA_txtSelAreaCode','UcAreaSelRegA_txtSelectArea','UcAreaSelRegA_txtArea','UcAreaSelCurA_txtSelAreaCode','UcAreaSelCurA_txtSelectArea','UcAreaSelCurA_txtArea');"/></td>
    </tr>
  <tr>
    <th><span class="xing">*</span>�����˿ڣ�</th>
    <td class="select"><asp:DropDownList ID="ddlFileds24" runat="server">
                                    <asp:ListItem>��</asp:ListItem>
                                    <asp:ListItem>��</asp:ListItem>          
                       </asp:DropDownList></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>����״����</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeA" runat="server">                        
                            <asp:ListItem>����</asp:ListItem>
                            <asp:ListItem>����</asp:ListItem>
                            <asp:ListItem>�ٻ�</asp:ListItem>
                            <asp:ListItem>���</asp:ListItem>
                            <asp:ListItem>ɥż</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>  
</table>
		  </div>
	    </div>
  <div class="form_a">
	      <p class="form_title"><b>������Ϣ</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <th>
            <span class="xing">*</span>�������ڣ�</th>
        <td class="text">
            <p><span><input id="txtFileds14" runat="server"  onclick="SelectDate(document.getElementById('txtFileds14'),'yyyy-MM-dd')"   /></span></p><b class="ps">���һ��</b></td>
    </tr>
    <tr>
                <th><span class="xing">*</span>���֤�ţ�</th>
                <td class="text"><p><span><asp:TextBox ID="ddlFileds47" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
    </tr>
</table>
		  </div>
	    </div>
	       
  
  <div class="form_a"><p class="form_title"><b>��ϵ��Ϣ</b></p>
<div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <th>
        <span class="xing">*</span>��ϵ�ֻ���</th>
        <td class="text">
            <p><span><asp:TextBox ID="txtContactTelA" runat="server" EnableViewState="False" MaxLength="12"/></span></p><b class="ps">������д���˵��ֻ����룬�����޷��յ�������ʾ</b>
        </td>
    </tr>
</table>
</div>
</div>	
	  </div>
	  <div class="check" style=" display:none"><asp:CheckBox ID="cbOk" runat="server" Checked /> ��ŵ�����˱�֤����������ṩ����ز�����ʵ�����в�ʵ��Ը��е��ɴ��������Ӧ�������Σ�����������ҳ���ϵͳ��</div>
<br/>
<!-- ������ť m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="�� �ύ ��" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>
<script>
 window.onload=function(){
ShowBirth0101(); ShowFMName();
}
</script>