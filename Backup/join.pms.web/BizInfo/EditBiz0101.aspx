<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editbiz0101.aspx.cs" Inherits="join.pms.web.BizInfo.EditBiz0101" %>
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
	      <p class="form_title"><b>Ů��������Ϣ</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>������</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameB" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>֤�����</th>
    <td class="select"><asp:DropDownList ID="ddlCidTypeB" runat="server" >
                            <asp:ListItem Value="1">���֤</asp:ListItem>
                            <asp:ListItem Value="2">����֤</asp:ListItem>
                            <asp:ListItem Value="3">����</asp:ListItem>
                            <asp:ListItem Value="4">����</asp:ListItem>
                        </asp:DropDownList></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>���֤�ţ�</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18"  onblur="CheckCID_1('ddlCidTypeB','1','txtPersonCidB');"/></span></p></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>����״����</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeB" runat="server" onchange="ShowMarry0101();MarryDateTB('ddlMarryTypeA','txtMarryDateA','ddlMarryTypeB','txtMarryDateB');">  
                            <asp:ListItem>δ��</asp:ListItem>                         
                            <asp:ListItem Selected>����</asp:ListItem>
                            <asp:ListItem>�ٻ�</asp:ListItem>
                            <asp:ListItem>���</asp:ListItem>
                            <asp:ListItem>����</asp:ListItem>  
                            <asp:ListItem>ɥż</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>
    <tr id="panelMarry" style="display:none;">
      <th><span class="xing">*</span>����ʱ�䣺</th>
      <td class="text"><p><span><input id="txtMarryDateB" runat="server"  onclick="SelectDate(document.getElementById('txtMarryDateB'),'yyyy-MM-dd')"  onchange="MarryDateTB('ddlMarryTypeB','txtMarryDateB','ddlMarryTypeA','txtMarryDateA');" /></span></p></td>
    </tr>
  <tr>
    <th><span class="xing">*</span>���壺</th>
    <td class="select"><asp:Literal ID="LiteralNationsB" runat="server" EnableViewState="false"></asp:Literal>
    <input type="hidden" name="txtHNationsB" id="txtHNationsB" runat="server" style="display:none;"/></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>�������ʣ�</th>
    <td class="select"><asp:DropDownList ID="ddlRegisterTypeB" runat="server">
                        <asp:ListItem>ũҵ</asp:ListItem>
                        <asp:ListItem>��ũҵ</asp:ListItem>   
                        <asp:ListItem>����</asp:ListItem>           
                       </asp:DropDownList></td>
  </tr> 
  <tr style="display:none">
    <th>�Ƿ��Ƕ�����Ů��</th>
    <td class="select"><asp:DropDownList ID="ddlFileds16" runat="server">
                        <asp:ListItem Selected></asp:ListItem>    
                        <asp:ListItem>��</asp:ListItem>     
                        <asp:ListItem>��</asp:ListItem>       
                       </asp:DropDownList></td>
  </tr>
<tr>
    <th><span class="xing">*</span>������ַ��</th>
    <td class="text"><p><span><asp:TextBox ID="txtAreaSelRegNameB" runat="server" EnableViewState="False" Width="300"/></span></p>
    <input type="hidden" name="txtAreaSelRegCodeB" id="txtAreaSelRegCodeB" runat="server" style="display:none;"/></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>�־�ס��ַ��</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurB" runat="server" /><input type="button" name="button" value="ͬ������" class="button" onclick="AreaCodeTB('UcAreaSelRegB_txtSelAreaCode','UcAreaSelRegB_txtSelectArea','UcAreaSelCurB_txtSelAreaCode','UcAreaSelCurB_txtSelectArea');"/></td>
  </tr>
  <tr>
    <th>������λ��</th>
    <td class="text"><p><span><asp:TextBox ID="txtWorkUnitB" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>��ϵ�绰��</th>
    <td class="text"><p><span><asp:TextBox ID="txtContactTelB" runat="server" EnableViewState="False" MaxLength="12"/></span></p><b class="ps">������д���˵��ֻ����룬�����޷��յ�������ʾ</b></td>
  </tr>   </table>	    
		  </div>
	    </div>  
	    <div id="panelPeiou" style="display:none;" class="form_a">
	      <p class="form_title"><b>�з�������Ϣ</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>������</th>
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
    <th><span class="xing">*</span>���֤�ţ�</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18"  onblur="CheckCID_1('ddlCidTypeA','0','txtPersonCidA');"/></span></p></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>����״����</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeA" runat="server">
                            <asp:ListItem>����</asp:ListItem>
                            <asp:ListItem>�ٻ�</asp:ListItem>
                            <asp:ListItem>���</asp:ListItem>
                            <asp:ListItem>����</asp:ListItem>  
                            <asp:ListItem>ɥż</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>  
  <tr>
      <th><span class="xing">*</span>����ʱ�䣺</th>
      <td class="text"><p><span><input id="txtMarryDateA" runat="server"  onclick="SelectDate(document.getElementById('txtMarryDateA'),'yyyy-MM-dd')"   /></span></p></td>
    </tr>
  <tr>
    <th><span class="xing">*</span>���壺</th>
    <td class="select"><asp:Literal ID="LiteralNationsA" runat="server" EnableViewState="false"></asp:Literal>
    <input type="hidden" name="txtHNationsA" id="txtHNationsA" runat="server" style="display:none;"/></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>�������ʣ�</th>
    <td class="select"><asp:DropDownList ID="ddlRegisterTypeA" runat="server">
                        <asp:ListItem>ũҵ</asp:ListItem>
                        <asp:ListItem>��ũҵ</asp:ListItem>    
                        <asp:ListItem>����</asp:ListItem>          
                       </asp:DropDownList></td>
  </tr>
  <tr style="display:none">
    <th>�Ƿ��Ƕ�����Ů��</th>
    <td class="select"><asp:DropDownList ID="ddlFileds17" runat="server">
                        <asp:ListItem Selected></asp:ListItem> 
                        <asp:ListItem>��</asp:ListItem>
                        <asp:ListItem>��</asp:ListItem>            
                       </asp:DropDownList></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>������ַ��</th>
    <td class="text"><p><span><asp:TextBox ID="txtAreaSelRegNameA" runat="server" EnableViewState="False" Width="300"/></span></p>
    <input type="hidden" name="txtAreaSelRegCodeA" id="txtAreaSelRegCodeA" runat="server" style="display:none;"/></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>�־�ס��ַ��</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurA" runat="server" /><input type="button" name="button" value="ͬ������" class="button" onclick="AreaCodeTB('UcAreaSelRegA_txtSelAreaCode','UcAreaSelRegA_txtSelectArea','UcAreaSelCurA_txtSelAreaCode','UcAreaSelCurA_txtSelectArea');"/></td>
  </tr>
  <tr>
    <th>������λ��</th>
    <td class="text"><p><span><asp:TextBox ID="txtWorkUnitA" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr> 
  <tr>
    <th>��ϵ�绰��</th>
    <td class="text"><p><span><asp:TextBox ID="txtContactTelA" runat="server" EnableViewState="False" MaxLength="12"/></span></p></td>
  </tr> 
</table>
		  </div>
	    </div>
  	    
	    <div class="form_a">
	      <p class="form_title"><b>��Ů��Ϣ</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
                <th><span class="xing">*</span>��Ů����</th>
                <td class="select"><asp:DropDownList ID="ddlBirthNum" runat="server" onchange="ShowBirth0101();">
                                         <asp:ListItem Value="0">0</asp:ListItem>
                                         <asp:ListItem Value="1">1</asp:ListItem>
                                         <asp:ListItem Value="2">2</asp:ListItem>
                                         <asp:ListItem Value="3">3</asp:ListItem>
                                    </asp:DropDownList></td>
            </tr>   
  </table>	    
		  </div>
	    </div>  
<div id="panelChildren" style="display:none;" class="form_a">
  <p class="form_title"><b>��������Ϣ</b></p>
  <div class="form_table">
  <table width="0" border="0" cellspacing="0" cellpadding="0" class="tdcolumns">	        
    <tr><th><span class="xing">*</span>����</th><th><span class="xing">*</span>�Ա�</th><th><span class="xing">*</span>��������</th><th>���֤�Ż����ҽѧ֤��������֤��</th></tr>
    <tr id="tableChild1">
        <td class="text"><p><span><asp:TextBox ID="txtChildName1" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex1" runat="server">
                               <asp:ListItem>��</asp:ListItem>
                               <asp:ListItem>Ů</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday1" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday1'),'yyyy-MM-dd')"   /></span></p></td>
         <td class="text"><p><span><asp:TextBox ID="txtChildCardID1" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
    </tr> <input type="hidden" name="txtChildID1" id="txtChildID1" value="" runat="server" style="display:none;"/>
    <tr id="tableChild2">
        <td class="text"><p><span><asp:TextBox ID="txtChildName2" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex2" runat="server">
                               <asp:ListItem>��</asp:ListItem>
                               <asp:ListItem>Ů</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday2" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday2'),'yyyy-MM-dd')"   /></span></p></td>
         <td class="text"><p><span><asp:TextBox ID="txtChildCardID2" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
    </tr>  <input type="hidden" name="txtChildID2" id="txtChildID2" value="" runat="server" style="display:none;"/>
    <tr id="tableChild3">
        <td class="text"><p><span><asp:TextBox ID="txtChildName3" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex3" runat="server">
                               <asp:ListItem>��</asp:ListItem>
                               <asp:ListItem>Ů</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday3" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday3'),'yyyy-MM-dd')"   /></span></p></td>
         <td class="text"><p><span><asp:TextBox ID="txtChildCardID3" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
    </tr>   <input type="hidden" name="txtChildID3" id="txtChildID3" value="" runat="server" style="display:none;"/> 
  </table>         
    </div>
</div>  
<div class="form_a">
  <p class="form_title"><b>��������</b></p>
  <div class="form_table">
    <table width="0" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <th><span class="xing">*</span>�������ɣ�</th>
        <td class="text"><p><span>���ϡ����ɹ��������˿���ƻ������������涨��������������һ����<div style="display:none"><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="100" Width="600px" Text="���ϡ����ɹ��������˿���ƻ������������涨��������������һ����"/></div></span></p></td>
      </tr>             
      </table>
  </div></div>
    
      	    <div class="form_a">
            <p class="form_title"><b>Ԥ��֤�ص�</b></p>
		    <div class="form_table">
                <table width="0" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                       <th><span class="xing">*</span>Ԥ��֤�ص㣺</th>
                       <td class="select select_auto"><uc4:ucAreaSelXian ID="UcAreaSelXian1" runat="server" /><b class="ps">������ѡ����֤�ص㣬ȷ���󲻿ɱ��</b></td>
                    </tr>
                </table>
            </div>    
	    </div>
	  </div>
	  <div class="check"><asp:CheckBox ID="cbOk" runat="server" /> ��ŵ�����˱�֤����������ṩ����ز�����ʵ�����в�ʵ��Ը��е��ɴ��������Ӧ�������Σ�����������ҳ���ϵͳ��</div>
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
 ShowMarry0101();ShowBirth0101();
}
</script>