<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Biz0131.aspx.cs" Inherits="join.pms.web.BizInfo.Biz0131" %>
<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc3" %>
<%@ Register Src="/userctrl/ucAreaSelXian.ascx" TagName="ucAreaSelXian" TagPrefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>��ǰҽѧ�������֤��</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
    <script src="/Scripts/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jsdate.js" type="text/javascript"></script>
    <script src="/includes/dataGrid.js" type="text/javascript"></script>
<script type="text/javascript" language="JavaScript1.2">
    //������Ϣ��ȡ
    function ReadCardA_onclick()
    {
        var open;
        var read;
        var csex;
        //open=rdcard.openport();   //�򿪻��� rdcard.readcard()
        read=rdcard.readcard();   //��ʼ����
        if(read==0)
        {
            csex = rdcard.Sex;
            if(csex==1){
                //����A
                document.getElementById('<%=txtPersonNameA.ClientID%>').value = rdcard.NameS;
                //���֤��A
                document.getElementById('<%=txtPersonCidA.ClientID%>').value = rdcard.CardNo;
                //��ȡȫԱ���� 
                //GetQykMsg(rdcard.CardNo,document.getElementById("qykInfo"));
                GetPersonsInfo(rdcard.CardNo,'0131A','1');
            }
            else{
                alert("������з����֤��Ȼ����ˢ����");
            }
            //������ַ
            //document.getElementById('UcAreaSelRegA_txtSelectArea').value = rdcard.Address;
            //����
            //document.getElementById('txtNationsB').value = rdcard.NationL;
            rdcard.endread();
        }
        //rdcard.closeport();
    }
    
    //��ȡŮ�������Ϣ
    function ReadCardB_onclick()
    {
        var open;
        var read;
        var csex;
        //open=rdcard.openport();   //�򿪻���
        read=rdcard.readcard();   //��ʼ����
        if(read==0)
        {
            csex = rdcard.Sex;
            if(csex!=1){
                //����A
                document.getElementById('<%=txtPersonNameB.ClientID%>').value = rdcard.NameS;
                //���֤��A
                document.getElementById('<%=txtPersonCidB.ClientID%>').value = rdcard.CardNo;
                //��ȡȫԱ���� 
                //GetQykMsg(rdcard.CardNo,document.getElementById("qykInfo"));
                GetPersonsInfo(rdcard.CardNo,'0131B','1');
            }
            else{
                alert("�����Ů�����֤��Ȼ����ˢ����");
            }
            rdcard.endread();
        }
    }
    //rdcard.closeport();
</script>
</head>
<body>
<object classid="clsid:F1317711-6BDE-4658-ABAA-39E31D3704D3" codebase="/Files/SDRdCard.cab#version=1,3,6,4" width="0" height="0" id="rdcard" name="rdcard"></object>
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
	        <p class="form_title"><b>������Ϣ</b></p>
		    <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
   <tr>
    <th><span class="xing">*</span>���֤�ţ�</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18" /></span></p>
    <input type="button" name="btnCardA" value="ˢ���֤" class="button button2" onclick="ReadCardA_onclick();"/>
    <input type="button" name="button" value="�Զ���ȡ����" class="button" onclick="GetPersonsInfo(document.getElementById('txtPersonCidA').value,'0131A','1');"/>
    </td>
  </tr>
  <tr>
    <th><span class="xing">*</span>������</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameA" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
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
  <tr>
    <th><span class="xing">*</span>����״����</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeA" runat="server" onchange="ShowMarry0110();">
                            <asp:ListItem>����</asp:ListItem>
                            <asp:ListItem>�ٻ�</asp:ListItem>
                            <asp:ListItem>����</asp:ListItem>
                            <asp:ListItem>���</asp:ListItem>
                            <asp:ListItem>ɥż</asp:ListItem>
                            <asp:ListItem>δ��</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>
  <tr id="divMarryChangDateA">
    <th><span class="xing">*</span>�����䶯���ڣ�</th>
    <td class="text"><p><span><input id="txtFileds34" runat="server"  onclick="SelectDate(document.getElementById('txtFileds34'),'yyyy-MM-dd')"   /></span></p></td>
  </tr>
   <tr>
    <th><span class="xing">*</span>������ַ��</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelRegA" runat="server" /></td>
  </tr>
   <tr>
    <th><span class="xing">*</span>��ס��ַ��</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurA" runat="server" /><input type="button" name="button" value="ͬ������" class="button" onclick="AreaCodeTB('UcAreaSelRegA_txtSelAreaCode','UcAreaSelRegA_txtSelectArea','UcAreaSelRegA_txtArea','UcAreaSelCurA_txtSelAreaCode','UcAreaSelCurA_txtSelectArea','UcAreaSelCurA_txtArea');"/></td>
  </tr> 
  <tr>
    <th>������λ��</th>
    <td class="text"><p><span><asp:TextBox ID="txtWorkUnitA" runat="server" EnableViewState="False" MaxLength="25"/></span></p><b class="ps">�޵�λ����д���ޡ�</b></td>
  </tr>  
  <tr style="display:none">
    <th>�������䣺</th>
    <td class="text"><p><span><asp:TextBox ID="txtMail" runat="server" EnableViewState="False" Width="300"/> </span></p></td>
  </tr>
    <tr style="display:none">
    <th><span class="xing">*</span>��������Ů����</th>
     <td class="select"><asp:DropDownList ID="ddlBirthNum" runat="server" onchange="ShowBirth0110();">
                            <asp:ListItem Value="0">0</asp:ListItem>
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                   </asp:DropDownList></td>
  </tr>  
 
</table>
            </div>
</div> 
	  
            <div id="panelPeiou"  class="form_a">
	        <p class="form_title"><b>�Է���Ϣ</b></p>
		    <div class="form_table">
		  <table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>���֤�ţ�</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18"/></span></p>
    <input type="button" name="btnCardB" value="ˢ���֤" class="button button2" onclick="ReadCardB_onclick();"/>
    <input type="button" name="button" value="�Զ���ȡ����" class="button" onclick="GetPersonsInfo(document.getElementById('txtPersonCidB').value,'0131B','1');"/>
    </td>
  </tr>
  <tr>
    <th><span class="xing">*</span>������</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameB" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
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
   <tr>
    <th><span class="xing">*</span>����״����</th>
    <td class="select"><asp:DropDownList ID="ddlMarryType" runat="server">
                            <asp:ListItem>����</asp:ListItem>
                            <asp:ListItem>�ٻ�</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>�����䶯���ڣ�</th>
    <td class="text"><p><span><input id="txtFileds14" runat="server"  onclick="SelectDate(document.getElementById('txtFileds14'),'yyyy-MM-dd')"   /></span></p></td>
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
    <th>������λ��</th>
    <td class="text"><p><span><asp:TextBox ID="txtWorkUnitB" runat="server" EnableViewState="False" MaxLength="25"/></span></p><b class="ps">�޵�λ����д���ޡ�</b></td>
  </tr>  
</table>
		    </div>
	    </div>
              	
        <div id="panelChildren" class="form_a" style="display:none"><!--style="display:none;" -->
          <p class="form_title"><b>������Ϣ</b></p>
          <div class="form_table">
          <table width="0" border="0" cellspacing="0" cellpadding="0" class="tdcolumns">	        
            <tr><th><span class="xing">*</span>����</th><th><span class="xing">*</span>�Ա�</th><th><span class="xing">*</span>��������</th><th>��������</th><th>���֤��</th><th>�Ƿ�����</th><th>��ḧ�����������</th></tr>
            <tr id="tableChild1">
                <td class="text"><p><span><asp:TextBox ID="txtChildName1" runat="server" EnableViewState="False" MaxLength="25" Width="50"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSex1" runat="server">
                       <asp:ListItem>��</asp:ListItem>
                       <asp:ListItem>Ů</asp:ListItem>           
                  </asp:DropDownList></td>
                <td class="text"><p><span><input id="txtChildBirthday1" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday1'),'yyyy-MM-dd')"   style="width:80px;"/></span></p></td> 
                <td class="select"><asp:DropDownList ID="ddlChildPolicy1" runat="server">
                       <asp:ListItem>������</asp:ListItem>
                       <asp:ListItem>������</asp:ListItem>
                  </asp:DropDownList></td>
                <td class="text"><p><span><asp:TextBox ID="txtChildCardID1" runat="server" EnableViewState="False" MaxLength="25" Width="160"/></span></p></td>
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
            <tr id="tableChild2"><!--style="display:none;" -->
                <td class="text"><p><span><asp:TextBox ID="txtChildName2" runat="server" EnableViewState="False" MaxLength="25" Width="50"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSex2" runat="server">
                                       <asp:ListItem>��</asp:ListItem>
                                       <asp:ListItem>Ů</asp:ListItem>           
                                  </asp:DropDownList></td>
                <td class="text"><p><span><input id="txtChildBirthday2" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday2'),'yyyy-MM-dd')"   style="width:80px;"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildPolicy2" runat="server">
                                       <asp:ListItem>������</asp:ListItem>
                                       <asp:ListItem>������</asp:ListItem>
                                  </asp:DropDownList></td>
                <td class="text"><p><span><asp:TextBox ID="txtChildCardID2" runat="server" EnableViewState="False" MaxLength="25" Width="160"/></span></p></td>
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
            <tr id="tableChild3"><!--style="display:none;" -->
                <td class="text"><p><span><asp:TextBox ID="txtChildName3" runat="server" EnableViewState="False" MaxLength="25" Width="50"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSex3" runat="server">
                       <asp:ListItem>��</asp:ListItem>
                       <asp:ListItem>Ů</asp:ListItem>           
                  </asp:DropDownList></td>
                <td class="text"><p><span><input id="txtChildBirthday3" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday3'),'yyyy-MM-dd')"   style="width:80px;"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildPolicy3" runat="server">
                                       <asp:ListItem>������</asp:ListItem>
                                       <asp:ListItem>������</asp:ListItem>
                                  </asp:DropDownList></td>
                <td class="text"><p><span><asp:TextBox ID="txtChildCardID3" runat="server" EnableViewState="False" MaxLength="25" Width="160"/></span></p></td>
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
            <tr id="tableChild4"><!--style="display:none;" -->
                <td class="text"><p><span><asp:TextBox ID="txtChildName4" runat="server" EnableViewState="False" MaxLength="25" Width="50"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSex4" runat="server">
                       <asp:ListItem>��</asp:ListItem>
                       <asp:ListItem>Ů</asp:ListItem>           
                  </asp:DropDownList></td>
                <td class="text"><p><span><input id="txtChildBirthday4" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday4'),'yyyy-MM-dd')"   style="width:80px;"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildPolicy4" runat="server">
                                       <asp:ListItem>������</asp:ListItem>
                                       <asp:ListItem>������</asp:ListItem>
                                  </asp:DropDownList></td>
                <td class="text"><p><span><asp:TextBox ID="txtChildCardID4" runat="server" EnableViewState="False" MaxLength="25" Width="160"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSource4" runat="server">
                       <asp:ListItem>���˼���ż����</asp:ListItem>
                       <asp:ListItem>������������ż����</asp:ListItem>
                       <asp:ListItem>�Ǳ���������ż����</asp:ListItem>
                       <asp:ListItem>�Ǳ�����������ż����</asp:ListItem>
                  </asp:DropDownList></td>
                 <td class="select"><asp:DropDownList ID="ddlMemos4" runat="server">
                       <asp:ListItem>��</asp:ListItem>
                       <asp:ListItem>�����ս᰸</asp:ListItem>
                       <asp:ListItem>������δ�᰸</asp:ListItem>
                       <asp:ListItem>δ����δ�᰸</asp:ListItem>
                  </asp:DropDownList></td>            
            </tr> 
            <tr id="tableChild5"><!--style="display:none;" -->
                <td class="text"><p><span><asp:TextBox ID="txtChildName5" runat="server" EnableViewState="False" MaxLength="25" Width="50"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSex5" runat="server">
                       <asp:ListItem>��</asp:ListItem>
                       <asp:ListItem>Ů</asp:ListItem>           
                  </asp:DropDownList></td>
                <td class="text"><p><span><input id="txtChildBirthday5" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday5'),'yyyy-MM-dd')"   style="width:80px;"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildPolicy5" runat="server">
                                       <asp:ListItem>������</asp:ListItem>
                                       <asp:ListItem>������</asp:ListItem>
                                  </asp:DropDownList></td>
                <td class="text"><p><span><asp:TextBox ID="txtChildCardID5" runat="server" EnableViewState="False" MaxLength="25" Width="160"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSource5" runat="server">
                       <asp:ListItem>���˼���ż����</asp:ListItem>
                       <asp:ListItem>������������ż����</asp:ListItem>
                       <asp:ListItem>�Ǳ���������ż����</asp:ListItem>
                       <asp:ListItem>�Ǳ�����������ż����</asp:ListItem>
                  </asp:DropDownList></td>
                 <td class="select"><asp:DropDownList ID="ddlMemos5" runat="server">
                       <asp:ListItem>��</asp:ListItem>
                       <asp:ListItem>�����ս᰸</asp:ListItem>
                       <asp:ListItem>������δ�᰸</asp:ListItem>
                       <asp:ListItem>δ����δ�᰸</asp:ListItem>
                  </asp:DropDownList></td>            
            </tr>                  
          </table>         
            </div>
        </div>

        <div class="form_a">
          <p class="form_title"><b>�������</b></p>
          <div class="form_table">
            <table width="0" border="0" cellspacing="0" cellpadding="0">        
              <tr>
                <th>���������</th>
                <td class="text"><p><span><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="100" Width="600px"  Text =""/></span></p></td>
              </tr>              
              </table>
          </div></div>


<div id="panelHY" class="form_a">
   <p class="form_title"><b>�����Ϣ</b></p>
   <div class="form_table">
   <table width="0" border="0" cellspacing="0" cellpadding="0">
   <tr>
        <th>
            <span class="xing">*</span>���ʱ�䣺</th>
        <td class="text">
            <p><span><input id="txtFileds22" runat="server"  onclick="SelectDate(document.getElementById('txtFileds22'),'yyyy-MM-dd')"   /></span></p></td>
    </tr>
   <tr>
        <th>
            <span class="xing">*</span>ֱϵ����������ϵѪ�׹�ϵ��</th>
    <td class="select">
        <asp:DropDownList ID="txtFileds23" runat="server">
                                       <asp:ListItem Value="0">��</asp:ListItem>
                                       <asp:ListItem Value="1">��</asp:ListItem>
                                  </asp:DropDownList></td>
    </tr>
  <tr>
    <th><span class="xing">*</span>ҽѧ�����</th>
    <td class="select">
        <asp:DropDownList ID="txtFileds24" runat="server">
                                       <asp:ListItem Value="">��ѡ��</asp:ListItem>
                                       <asp:ListItem Value="1">�鲻�˽��</asp:ListItem>
                                       <asp:ListItem Value="2">���鲻������</asp:ListItem>
                                       <asp:ListItem Value="3">�����ݻ����</asp:ListItem>   
                                       <asp:ListItem Value="4">δ����ҽѧ�ϲ��˽�������</asp:ListItem>  
                                       <asp:ListItem Value="5">�����ȡҽѧ��ʩ�������ܼ�����Ը</asp:ListItem>       
                                  </asp:DropDownList></td>
  </tr>
  <tr>
    <th>��ǰҽѧ�������</th>
    <td class="text"><p><span><asp:TextBox ID="txtFileds25" runat="server" EnableViewState="False" MaxLength="100" Width="600px"  Text =""/></span></p></td>
  </tr> 
   </table></div></div>
         
         <div class="form_a" style=" display:none">
            <p class="form_title"><b>Ԥ��֤�ص�</b></p>
		    <div class="form_table">
                <table width="0" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                       <th><span class="xing">*</span>Ԥ��֤�ص㣺</th>
                       <td class="select"><uc4:ucAreaSelXian ID="UcAreaSelXian1" runat="server" /> <b class="ps">������ѡ����֤�ص㣬ȷ���󲻿ɱ��</b></td>
                    </tr>
                </table>
            </div>    
	    </div>
 	   	<div class="form_a">
            <p class="form_title"><b>��ϵ��ʽ</b></p>
		    <div class="form_table">
                <table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>��ϵ�绰��</th>
    <td class="text"><p><span><asp:TextBox ID="txtContactTelA" runat="server" EnableViewState="False" MaxLength="12"/></span></p><b class="ps">������д���˵��ֻ����룬�����޷��յ�������ʾ</b></td>
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
<div class="tsxx" id="qykInfo"></div>
</body>
</html>
<script>
 window.onload=function(){
 ShowBirth0101();ShowMarry0110();
}

function Showjc1(obj1,obj2,obj3){
    var IsHY=document.getElementById(obj1).value;
    if(IsHY=='1'){ document.getElementById(obj2).style.display = "";}
    else{  document.getElementById(obj2).style.display = "none";    document.getElementById(obj3).value=""; }
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
