<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Biz0106.aspx.cs" Inherits="join.pms.web.BizInfo.Biz0106" %>
<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>�ޱ���ҳ</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
    <script src="/Scripts/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jsdate.js" type="text/javascript"></script>
    <script type="text/javascript" language="JavaScript1.2">
    //������Ϣ��ȡ
    function ReadCardA_onclick()
    {
        var nRet;
        str = SynCardOcx1.FindReader();
  	    if (str > 0)
  	    {    	
            nRet = SynCardOcx1.ReadCardMsg();
            if(nRet==0)
            {
                //����A
                document.getElementById('<%=txtPersonNameA.ClientID%>').value = SynCardOcx1.NameA;
                //���֤��A
                document.getElementById('<%=txtPersonCidA.ClientID%>').value = SynCardOcx1.CardNo;
            }
        }
    }
    function ReadCardB_onclick()
    {
        var nRet;
        str = SynCardOcx1.FindReader();
  	    if (str > 0)
  	    {    	
            nRet = SynCardOcx1.ReadCardMsg();
            if(nRet==0)
            {
                //����A
                document.getElementById('<%=txtPersonNameB.ClientID%>').value = SynCardOcx1.NameA;
                //���֤��A
                document.getElementById('<%=txtPersonCidB.ClientID%>').value = SynCardOcx1.CardNo;
            }
        }
    }
    </script>
</head>
<body>
<object classid="clsid:46E4B248-8A41-45C5-B896-738ED44C1587" id="SynCardOcx1" codebase="SynCardOcx.CAB#version=1,0,0,1" width="0" height="0" ></object>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--������Ϣ-->
<div class="mbx">��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--��ʾ��Ϣ-->
<div class="tsxx">��ʾ��Ϣ������һ��д������Ϣ�󣻵�����ύ����ť����</div>
<!-- ҳ����� -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- �༭��  -->
 <div class="form_bg">
	  
	    <div class="form_a">
	      <p class="form_title"><b>�����˻�����Ϣ</b><span class="ps">Ů��������49�����ڲſ�������</span></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>������</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameA" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>���֤�ţ�</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18"  onblur="CheckCID0106('txtPersonCidA');"/></span></p>
    <input type="button" name="btnCardA" value="ˢ���֤" class="button button2" onclick="ReadCardA_onclick();"/>   
    <input type="button" name="button" value="�Զ���ȡ����" class="button" onclick="GetPersonsInfo(document.getElementById('txtPersonCidA').value,'0106A','1');"/>
    </td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>����״����</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeA" runat="server" onchange="MarryDateTB('ddlMarryTypeB','txtMarryDateB','ddlMarryTypeA','txtMarryDateA');">
                            <asp:ListItem>����</asp:ListItem>
                            <asp:ListItem>�ٻ�</asp:ListItem>
                            <asp:ListItem>���</asp:ListItem>
                            <asp:ListItem>����</asp:ListItem>  
                            <asp:ListItem>ɥż</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>  
  <tr>
      <th><span class="xing">*</span>����ʱ�䣺</th>
      <td class="text"><p><span><input id="txtMarryDateA" runat="server"  onclick="SelectDate(document.getElementById('txtMarryDateA'),'yyyy-MM-dd')"  onchange="MarryDateTB('ddlMarryTypeA','txtMarryDateA','ddlMarryTypeB','txtMarryDateB');"  /></span></p></td>
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
    <td class="text"><p><span><asp:TextBox ID="txtAreaSelRegNameA" runat="server" EnableViewState="False" Width="300"/></span></p>
    <input type="hidden" name="txtAreaSelRegCodeA" id="txtAreaSelRegCodeA" runat="server" style="display:none;"/></td>
  </tr>
  <tr>
    <th>�־�ס��ַ��</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurA" runat="server" /><input type="button" name="button" value="ͬ������" class="button" onclick="AreaCodeTB('txtAreaSelRegCodeA','txtAreaSelRegNameA','UcAreaSelCurA_txtSelAreaCode','UcAreaSelCurA_txtSelectArea');"/></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>��ϵ�绰��</th>
    <td class="text"><p><span><asp:TextBox ID="txtContactTelA" runat="server" EnableViewState="False" MaxLength="12"/></span></p><b class="ps">������д���˵��ֻ����룬�����޷��յ�������ʾ</b></td>
  </tr>  
  <tr>
                <th><span class="xing">*</span>��Ů����</th>
                <td class="select"><asp:DropDownList ID="ddlBirthNum" runat="server" onchange="ShowBirth();">
                                         <asp:ListItem Value="0">0</asp:ListItem>
                                         <asp:ListItem Value="1">1</asp:ListItem>
                                         <asp:ListItem Value="2">2</asp:ListItem>
                                         <asp:ListItem Value="3">3</asp:ListItem>
                                    </asp:DropDownList></td>
            </tr> 
</table>
		  </div>
	    </div>  
	    <div class="form_a">
	      <p class="form_title"><b>��ż������Ϣ</b><span class="ps">Ů��������49�����ڲſ�������</span></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>������</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameB" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>���֤�ţ�</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18" onblur="CheckCID0106('txtPersonCidB');"/></span></p>
    <input type="button" name="btnCardB" value="ˢ���֤" class="button button2" onclick="ReadCardB_onclick();"/>
    <input type="button" name="button" value="�Զ���ȡ����" class="button" onclick="GetPersonsInfo(document.getElementById('txtPersonCidB').value,'0106B','1');"/>
    </td>
  </tr>
  <tr>
    <th><span class="xing">*</span>����״����</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeB" runat="server">  
                            <asp:ListItem>����</asp:ListItem>
                            <asp:ListItem>�ٻ�</asp:ListItem>
                            <asp:ListItem>���</asp:ListItem>
                            <asp:ListItem>����</asp:ListItem>  
                            <asp:ListItem>ɥż</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>
    <tr>
      <th><span class="xing">*</span>����ʱ�䣺</th>
      <td class="text"><p><span><input id="txtMarryDateB" runat="server"  onclick="SelectDate(document.getElementById('txtMarryDateB'),'yyyy-MM-dd')"  /></span></p></td>
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
    <th><span class="xing">*</span>�־�ס��ַ��</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurB" runat="server" /><input type="button" name="button" value="ͬ������" class="button" onclick="AreaCodeTB('UcAreaSelRegB_txtSelAreaCode','UcAreaSelRegB_txtSelectArea','UcAreaSelCurB_txtSelAreaCode','UcAreaSelCurB_txtSelectArea');"/></td>
  </tr>
  <tr>
    <th>��ϵ�绰��</th>
    <td class="text"><p><span><asp:TextBox ID="txtContactTelB" runat="server" EnableViewState="False" MaxLength="12"/></span></p></td>
  </tr>    
  </table>	    
		  </div>
	    </div>
  	     <div class="form_a">
	      <p class="form_title"><b>���������</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>����ҽԺ��</th>
     <td class="text"><p><span><asp:TextBox ID="txtFileds27" runat="server" EnableViewState="False" MaxLength="12"/></span></p></td>
  </tr>   
   <tr>
    <th><span class="xing">*</span>�������ڣ�</th>
     <td class="text"><p><span><input id="txtFileds28" runat="server"  onclick="SelectDate(document.getElementById('txtFileds28'),'yyyy-MM-dd')"   /></span></p></td>
   </tr>  
</table>
		  </div>
	    </div> 
<div id="panelChildren" style="display:none;" class="form_a">
  <p class="form_title"><b>��������������������Ů���</b></p>
  <div class="form_table">
  <table width="0" border="0" cellspacing="0" cellpadding="0" class="tdcolumns">	        
    <tr><th><span class="xing">*</span>����</th><th><span class="xing">*</span>�Ա�</th><th><span class="xing">*</span>��������</th><th>��������</th><th>�Ƿ�����</th></tr>
    <tr id="tableChild1">
        <td class="text"><p><span><asp:TextBox ID="txtChildName1" runat="server" EnableViewState="False" MaxLength="25" style="width:80px;"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex1" runat="server">
                               <asp:ListItem>��</asp:ListItem>
                               <asp:ListItem>Ů</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday1" runat="server"  style="width:80px;" onclick="SelectDate(document.getElementById('txtChildBirthday1'),'yyyy-MM-dd')"   /></span></p></td>
         <td class="text"><p><span><input id="txtChildDeathday1" runat="server"  style="width:80px;" onclick="SelectDate(document.getElementById('txtChildDeathday1'),'yyyy-MM-dd')"   /></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSource1" runat="server">
                               <asp:ListItem>���˼���ż����</asp:ListItem>
                               <asp:ListItem>������������ż����</asp:ListItem>    
                               <asp:ListItem>�Ǳ���������ż����</asp:ListItem>           
                               <asp:ListItem>�Ǳ��˼���ż����</asp:ListItem>                  
                          </asp:DropDownList></td>
    </tr> 
    <tr id="tableChild2">
        <td class="text"><p><span><asp:TextBox ID="txtChildName2" runat="server" EnableViewState="False" MaxLength="25" style="width:80px;"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex2" runat="server">
                               <asp:ListItem>��</asp:ListItem>
                               <asp:ListItem>Ů</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday2" runat="server"  style="width:80px;" onclick="SelectDate(document.getElementById('txtChildBirthday2'),'yyyy-MM-dd')"   /></span></p></td>
        <td class="text"><p><span><input id="txtChildDeathday2" runat="server" style="width:80px;"  onclick="SelectDate(document.getElementById('txtChildDeathday2'),'yyyy-MM-dd')"   /></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSource2" runat="server">
                               <asp:ListItem>���˼���ż����</asp:ListItem>
                               <asp:ListItem>������������ż����</asp:ListItem>    
                               <asp:ListItem>�Ǳ���������ż����</asp:ListItem>           
                               <asp:ListItem>�Ǳ��˼���ż����</asp:ListItem>                  
                          </asp:DropDownList></td>
    </tr>  
    <tr id="tableChild3">
        <td class="text"><p><span><asp:TextBox ID="txtChildName3" runat="server" EnableViewState="False" MaxLength="25" style="width:80px;"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex3" runat="server">
                               <asp:ListItem>��</asp:ListItem>
                               <asp:ListItem>Ů</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday3" runat="server"  style="width:80px;" onclick="SelectDate(document.getElementById('txtChildBirthday3'),'yyyy-MM-dd')"   /></span></p></td>
         <td class="text"><p><span><input id="txtChildDeathday3" runat="server" style="width:80px;"  onclick="SelectDate(document.getElementById('txtChildDeathday3'),'yyyy-MM-dd')"   /></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSource3" runat="server">
                               <asp:ListItem>���˼���ż����</asp:ListItem>
                               <asp:ListItem>������������ż����</asp:ListItem>    
                               <asp:ListItem>�Ǳ���������ż����</asp:ListItem>           
                               <asp:ListItem>�Ǳ��˼���ż����</asp:ListItem>                  
                          </asp:DropDownList></td>
    </tr> 
    <tr id="tableChild4">
        <td class="text"><p><span><asp:TextBox ID="txtChildName4" runat="server" EnableViewState="False" MaxLength="25" style="width:80px;"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex4" runat="server">
                               <asp:ListItem>��</asp:ListItem>
                               <asp:ListItem>Ů</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday4" runat="server"  style="width:80px;" onclick="SelectDate(document.getElementById('txtChildBirthday4'),'yyyy-MM-dd')"   /></span></p></td>
         <td class="text"><p><span><input id="txtChildDeathday4" runat="server" style="width:80px;" onclick="SelectDate(document.getElementById('txtChildDeathday4'),'yyyy-MM-dd')"   /></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSource4" runat="server">
                               <asp:ListItem>���˼���ż����</asp:ListItem>
                               <asp:ListItem>������������ż����</asp:ListItem>    
                               <asp:ListItem>�Ǳ���������ż����</asp:ListItem>           
                               <asp:ListItem>�Ǳ��˼���ż����</asp:ListItem>                  
                          </asp:DropDownList></td>
    </tr> 
    <tr id="tableChild5">
        <td class="text"><p><span><asp:TextBox ID="txtChildName5" runat="server" EnableViewState="False" MaxLength="25" style="width:80px;"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex5" runat="server">
                               <asp:ListItem>��</asp:ListItem>
                               <asp:ListItem>Ů</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday5" runat="server"  style="width:80px;" onclick="SelectDate(document.getElementById('txtChildBirthday5'),'yyyy-MM-dd')"   /></span></p></td>
         <td class="text"><p><span><input id="txtChildDeathday5" runat="server" style="width:80px;"  onclick="SelectDate(document.getElementById('txtChildDeathday5'),'yyyy-MM-dd')"   /></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSource5" runat="server">
                               <asp:ListItem>���˼���ż����</asp:ListItem>
                               <asp:ListItem>������������ż����</asp:ListItem>    
                               <asp:ListItem>�Ǳ���������ż����</asp:ListItem>           
                               <asp:ListItem>�Ǳ��˼���ż����</asp:ListItem>                  
                          </asp:DropDownList></td>
    </tr> 
    <tr id="tableChild6">
        <td class="text"><p><span><asp:TextBox ID="txtChildName6" runat="server" EnableViewState="False" MaxLength="25" style="width:80px;"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex6" runat="server">
                               <asp:ListItem>��</asp:ListItem>
                               <asp:ListItem>Ů</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday6" runat="server"  style="width:80px;" onclick="SelectDate(document.getElementById('txtChildBirthday6'),'yyyy-MM-dd')"   /></span></p></td>
         <td class="text"><p><span><input id="txtChildDeathday6" runat="server" style="width:80px;" onclick="SelectDate(document.getElementById('txtChildDeathday6'),'yyyy-MM-dd')"   /></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSource6" runat="server">
                               <asp:ListItem>���˼���ż����</asp:ListItem>
                               <asp:ListItem>������������ż����</asp:ListItem>    
                               <asp:ListItem>�Ǳ���������ż����</asp:ListItem>           
                               <asp:ListItem>�Ǳ��˼���ż����</asp:ListItem>                  
                          </asp:DropDownList></td>
    </tr> 
  </table>        
    </div>
</div>  
<div class="form_a">
  <p class="form_title"><b>�걨����</b></p>
  <div class="form_table">
    <table width="0" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <th><span class="xing">*</span>�걨���ɣ�</th>
        <td class="text"><p><span><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="100" Width="600px" Text="���Ͻ�����ͥ��������"/></span></p></td>
      </tr>             
      </table>
  </div></div>
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
</html><script>
 window.onload=function(){
ShowBirth();
}
</script>