<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditBiz0109.aspx.cs" Inherits="join.pms.web.BizInfo.EditBiz0109" %>
<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc3" %>
<%@ Register Src="/userctrl/ucAreaSelXian.ascx" TagName="ucAreaSelXian" TagPrefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>�������˿ڻ���֤���������</title>
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
	        <p class="form_title"><b>��֤����Ϣ</b></p>
		    <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>������</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameA" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>����״����</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeA" runat="server" onchange="ShowMarry0109();">
                            <asp:ListItem>δ��</asp:ListItem>
                            <asp:ListItem>����</asp:ListItem>
                            <asp:ListItem>�ٻ�</asp:ListItem>
                            <asp:ListItem>���</asp:ListItem>
                            <asp:ListItem>����</asp:ListItem>  
                            <asp:ListItem>ɥż</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>
    <tr>
    <th><span class="xing">*</span>�����䶯���ڣ�</th>
    <td class="text"><p><span><input id="txtFileds34" runat="server"  onclick="SelectDate(document.getElementById('txtFileds34'),'yyyy-MM-dd')"   /></span></p></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>����֤�ţ�</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18" /></span></p></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>��ϵ�绰��</th>
    <td class="text"><p><span><asp:TextBox ID="txtContactTelA" runat="server" EnableViewState="False" MaxLength="12"/></span></p><b class="ps">������д���˵��ֻ����룬�����޷��յ�������ʾ</b></td>
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
    <th><span class="xing">*</span>������ַ��</th>
    <td class="text"><p><span><asp:TextBox ID="txtAreaSelRegNameA" runat="server" EnableViewState="False" Width="300"/> </span></p>   
    <input type="hidden" name="txtAreaSelRegCodeA" id="txtAreaSelRegCodeA" runat="server" style="display:none;"/></td>
  </tr>

   <tr>
    <th><span class="xing">*</span>��ס��ַ��</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurA" runat="server" /><input type="button" name="button" value="ͬ������" class="button" onclick="AreaCodeTB('txtAreaSelRegCodeA','txtAreaSelRegNameA','UcAreaSelCurA_txtSelAreaCode','UcAreaSelCurA_txtSelectArea');"/></td>
  </tr>
    <tr>
    <th><span class="xing">*</span>��������Ů����</th>
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
	  
        <div id="panelPeiou" class="form_a">
	        <p class="form_title"><b>��ż��Ϣ</b></p>
		    <div class="form_table">
		  <table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>������</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameB" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr>
    <tr>
    <th><span class="xing">*</span>����״����</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeB" runat="server">
                            <asp:ListItem>����</asp:ListItem>
                            <asp:ListItem>�ٻ�</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>�����䶯���ڣ�</th>
    <td class="text"><p><span><input id="txtFileds14" runat="server"  onclick="SelectDate(document.getElementById('txtFileds14'),'yyyy-MM-dd')"   /></span></p></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>��ϵ�绰��</th>
    <td class="text"><p><span><asp:TextBox ID="txtContactTelB" runat="server" EnableViewState="False" MaxLength="12"/></span></p></td>
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
    <th><span class="xing">*</span>����֤�ţ�</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18" /></span></p></td>
  </tr>
    <tr>
    <th><span class="xing">*</span>������ַ��</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelRegB" runat="server" /></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>��ס��ַ��</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurB" runat="server" /><input type="button" name="button" value="ͬ������" class="button" onclick="AreaCodeTB('UcAreaSelRegB_txtSelAreaCode','UcAreaSelRegB_txtSelectArea','UcAreaSelCurB_txtSelAreaCode','UcAreaSelCurB_txtSelectArea');"/></td>
  </tr>
</table>
		    </div>
	    </div>

        <div class="form_a">
          <p class="form_title"><b>���д�ʩ</b></p>
          <div class="form_table">
            <table width="0" border="0" cellspacing="0" cellpadding="0">        
              <tr>
                <th><span class="xing">*</span>���д�ʩ��</th>
                  <td class="select"><asp:DropDownList ID="ddlFileds45" runat="server">
                       <asp:ListItem>��</asp:ListItem>
                       <asp:ListItem>�ϻ�</asp:ListItem> 
                       <asp:ListItem>Ů��</asp:ListItem> 
                       <asp:ListItem>Ƥ��</asp:ListItem> 
                       <asp:ListItem>ҩ��</asp:ListItem>          
                    </asp:DropDownList></td>
              </tr>              
              </table>
          </div></div>
              	
        <div id="panelChildren" style="display:none;" class="form_a">
          <p class="form_title"><b>������Ϣ</b></p>
          <div class="form_table">
          <table width="0" border="0" cellspacing="0" cellpadding="0" class="tdcolumns">	        
            <tr><th><span class="xing">*</span>����</th><th>����֤��</th><th><span class="xing">*</span>�Ա�</th><th><span class="xing">*</span>��������</th><th>��������</th><th>�Ƿ�����</th><th>��ḧ�����������</th></tr>
            <tr id="tableChild1">
                <td class="text"><p><span><asp:TextBox ID="txtChildName1" runat="server" EnableViewState="False" MaxLength="25" Width="50"/></span></p></td>
                <td class="text"><p><span><asp:TextBox ID="txtChildCardID1" runat="server" EnableViewState="False" MaxLength="25" Width="160"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSex1" runat="server">
                       <asp:ListItem>��</asp:ListItem>
                       <asp:ListItem>Ů</asp:ListItem>           
                  </asp:DropDownList></td>
                <td class="text"><p><span><input id="txtChildBirthday1" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday1'),'yyyy-MM-dd')"   style="width:80px;"/></span></p></td> 
                <td class="select"><asp:DropDownList ID="ddlChildPolicy1" runat="server">
                       <asp:ListItem>������</asp:ListItem>
                       <asp:ListItem>������</asp:ListItem>
                  </asp:DropDownList></td>
                <td class="select"><asp:DropDownList ID="ddlChildSource1" runat="server">
                       <asp:ListItem>���˼���ż����</asp:ListItem>
                       <asp:ListItem>������������ż����</asp:ListItem>
                       <asp:ListItem>�Ǳ���������ż����</asp:ListItem>
                       <asp:ListItem>�Ǳ�����������ż����</asp:ListItem>
                  </asp:DropDownList></td>
                 <td class="select"><asp:DropDownList ID="ddlMemos1" runat="server">
                       <asp:ListItem>��</asp:ListItem>
                       <asp:ListItem>�����ս᰸</asp:ListItem>
                       <asp:ListItem>������δ�᰸</asp:ListItem>
                       <asp:ListItem>δ����δ�᰸</asp:ListItem>
                  </asp:DropDownList></td>
            </tr>
            <input type="hidden" name="txtChildID1" id="txtChildID1" value="" runat="server" style="display:none;"/>
            <tr id="tableChild2"style="display:none;" >
                <td class="text"><p><span><asp:TextBox ID="txtChildName2" runat="server" EnableViewState="False" MaxLength="25" Width="50"/></span></p></td>
                <td class="text"><p><span><asp:TextBox ID="txtChildCardID2" runat="server" EnableViewState="False" MaxLength="25" Width="160"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSex2" runat="server">
                                       <asp:ListItem>��</asp:ListItem>
                                       <asp:ListItem>Ů</asp:ListItem>           
                                  </asp:DropDownList></td>
                <td class="text"><p><span><input id="txtChildBirthday2" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday2'),'yyyy-MM-dd')"   style="width:80px;"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildPolicy2" runat="server">
                                       <asp:ListItem>������</asp:ListItem>
                                       <asp:ListItem>������</asp:ListItem>
                                  </asp:DropDownList></td>
                <td class="select"><asp:DropDownList ID="ddlChildSource2" runat="server">
                       <asp:ListItem>���˼���ż����</asp:ListItem>
                       <asp:ListItem>������������ż����</asp:ListItem>
                       <asp:ListItem>�Ǳ���������ż����</asp:ListItem>
                       <asp:ListItem>�Ǳ�����������ż����</asp:ListItem>
                  </asp:DropDownList></td>
                <td class="select"><asp:DropDownList ID="ddlMemos2" runat="server">
                       <asp:ListItem>��</asp:ListItem>
                       <asp:ListItem>�����ս᰸</asp:ListItem>
                       <asp:ListItem>������δ�᰸</asp:ListItem>
                       <asp:ListItem>δ����δ�᰸</asp:ListItem>
                  </asp:DropDownList></td>             
            </tr>
            <input type="hidden" name="txtChildID2" id="txtChildID2" value="" runat="server" style="display:none;"/>
            <tr id="tableChild3"style="display:none;" >
                <td class="text"><p><span><asp:TextBox ID="txtChildName3" runat="server" EnableViewState="False" MaxLength="25" Width="50"/></span></p></td>
                <td class="text"><p><span><asp:TextBox ID="txtChildCardID3" runat="server" EnableViewState="False" MaxLength="25" Width="160"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSex3" runat="server">
                       <asp:ListItem>��</asp:ListItem>
                       <asp:ListItem>Ů</asp:ListItem>           
                  </asp:DropDownList></td>
                <td class="text"><p><span><input id="txtChildBirthday3" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday3'),'yyyy-MM-dd')"   style="width:80px;"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildPolicy3" runat="server">
                                       <asp:ListItem>������</asp:ListItem>
                                       <asp:ListItem>������</asp:ListItem>
                                  </asp:DropDownList></td>
                <td class="select"><asp:DropDownList ID="ddlChildSource3" runat="server">
                       <asp:ListItem>���˼���ż����</asp:ListItem>
                       <asp:ListItem>������������ż����</asp:ListItem>
                       <asp:ListItem>�Ǳ���������ż����</asp:ListItem>
                       <asp:ListItem>�Ǳ�����������ż����</asp:ListItem>
                  </asp:DropDownList></td>
                <td class="select"><asp:DropDownList ID="ddlMemos3" runat="server">
                       <asp:ListItem>��</asp:ListItem>
                       <asp:ListItem>�����ս᰸</asp:ListItem>
                       <asp:ListItem>������δ�᰸</asp:ListItem>
                       <asp:ListItem>δ����δ�᰸</asp:ListItem>
                  </asp:DropDownList></td>            
            </tr> 
             <input type="hidden" name="txtChildID3" id="txtChildID3" value="" runat="server" style="display:none;"/>          
          </table>         
            </div>
        </div>
         
        <div class="form_a">
          <p class="form_title"><b>��ŵ</b></p>
          <div class="form_table">
            <table width="0" border="0" cellspacing="0" cellpadding="0">        
              <tr>
                <th><span class="xing">*</span>��ŵ��</th>
                <td class="text"><p><span><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="100" Width="600px"  Text ="���˱�֤�����ṩ����������ʵ�����в�ʵ����֤��Ը��е��ɴ��������Ӧ�������Ρ�"/></span></p></td>
              </tr>              
              </table>
          </div></div>
         
 	  	<div class="form_a">
            <p class="form_title"><b>Ԥ�����ص�</b></p>
		    <div class="form_table">
                <table width="0" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                       <th><span class="xing">*</span>Ԥ�����ص㣺</th>
                       <td class="select select_auto"><uc4:ucAreaSelXian ID="UcAreaSelXian1" runat="server" /><b class="ps">������ѡ������ص㣬ȷ���󲻿ɱ��</b></td>
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
 ShowBirth0101();
}
</script>
<script language="javascript" type="text/javascript">
        Sys.Application.add_load(
             function() {
                 var form = Sys.WebForms.PageRequestManager.getInstance()._form;
                 form._initialAction = form.action = window.location.href;
             }
         );
</script>