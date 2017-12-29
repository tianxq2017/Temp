<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Biz0107.aspx.cs" Inherits="join.pms.web.BizInfo.Biz0107" %>
<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc3" %>
<%@ Register Src="/userctrl/ucAreaSelXian.ascx" TagName="ucAreaSelXian" TagPrefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>��һ����������������</title>
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
	              <p class="form_title"><b>Ů��������Ϣ</b></p>
		          <div class="form_table">
		          <table width="0" border="0" cellspacing="0" cellpadding="0">
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
                        <th><span class="xing">*</span>���֤�ţ�</th>
                        <td class="text"><p><span><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18" onblur="CheckCID('1','txtPersonCidB');"/></span></p>
                        <input type="button" name="btnCardB" value="ˢ���֤" class="button button2" onclick="ReadCardB_onclick();"/>
                        <input type="button" name="button" value="�Զ���ȡ����" class="button" onclick="GetPersonsInfo(document.getElementById('txtPersonCidB').value,'0107B','1');"/>
                        </td>
                      </tr>
                      <tr>
                        <th><span class="xing">*</span>�־ӵ�ַ��</th>
                        <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurB" runat="server" /></td>
                      </tr>
                       <tr>
                        <th><span class="xing">*</span>����̥�Σ�</th>
                        <td class="select"><asp:DropDownList ID="ddlFileds41" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>       
                           </asp:DropDownList>
                        </td>
                      </tr> 
                      <tr>
                        <th><span class="xing">*</span>ĩ���¾�ʱ�䣺</th>
                        <td class="text"><p><span><input id="txtFileds42" runat="server"  onclick="SelectDate(document.getElementById('txtFileds42'),'yyyy-MM-dd')"   /></span></p></td>
                      </tr>
                      <tr>
                        <th><span class="xing">*</span>ȷ�ϻ���ʱ�䣺</th>
                        <td class="text"><p><span><input id="txtFileds43" runat="server"  onclick="SelectDate(document.getElementById('txtFileds43'),'yyyy-MM-dd')"   /></span></p></td>
                      </tr>
                       <tr>
                        <th><span class="xing">*</span>ȷ�Ϸ�ʽ��</th>
                        <td class="select"><asp:DropDownList ID="ddlFileds29" runat="server">
                            <asp:ListItem>�������</asp:ListItem>
                            <asp:ListItem>����������</asp:ListItem>            
                           </asp:DropDownList>
                        </td>
                      </tr>
                       <tr>
                        <th><span class="xing">*</span>ȷ�ϵ�λ��</th>
                        <td class="select"><asp:DropDownList ID="ddlFileds30" runat="server">
                            <asp:ListItem>�򼶼�����ҽ��������λ</asp:ListItem>
                            <asp:ListItem>�򼶼����ϼƻ���������λ</asp:ListItem>
                           </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <th><span class="xing">*</span>��ϵ�绰��</th>
                        <td class="text"><p><span><asp:TextBox ID="txtContactTelB" runat="server" EnableViewState="False" MaxLength="12"/></span></p><b class="ps">������д���˵��ֻ����룬�����޷��յ�������ʾ</b></td>
                      </tr>
                </table>
            </div>
            </div>	  
	            <div class="form_a">
	              <p class="form_title"><b>�з�������Ϣ</b></p>
		          <div class="form_table">
                    <table width="0" border="0" cellspacing="0" cellpadding="0">
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
                        <th><span class="xing">*</span>���֤�ţ�</th>
                        <td class="text"><p><span><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18" onblur="CheckCID('0','txtPersonCidA');"/></span></p>
                        <input type="button" name="btnCardA" value="ˢ���֤" class="button button2" onclick="ReadCardA_onclick();"/>
                        <input type="button" name="button" value="�Զ���ȡ����" class="button" onclick="GetPersonsInfo(document.getElementById('txtPersonCidA').value,'0107A','1');"/>
                        </td>
                      </tr>
                      <tr>
                        <th><span class="xing">*</span>�־ӵ�ַ��</th>
                        <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurA" runat="server" /></td>
                      </tr>
                    </table>
		          </div>
	            </div>
	            
                <div class="form_a">
                  <p class="form_title"><b>���ʱ��</b></p>
                  <div class="form_table">
                    <table width="0" border="0" cellspacing="0" cellpadding="0">        
                      <tr>
                        <th><span class="xing">*</span>���ʱ�䣺</th>
                        <td class="text"><p><span><input id="txtFileds14" runat="server"  onclick="SelectDate(document.getElementById('txtFileds14'),'yyyy-MM-dd')"   /></span></p></td>
                      </tr>              
                      </table>
                  </div>
                </div>
                
                <div class="form_a">
                  <p class="form_title"><b>����֤��</b></p>
                  <div class="form_table">
                    <table width="0" border="0" cellspacing="0" cellpadding="0">        
                      <tr>
                        <th><span class="xing">*</span>����֤�ţ�</th>
                        <td class="text"><p><span><asp:TextBox ID="txtFileds21" runat="server" EnableViewState="False" MaxLength="50"/></span></p></td>
                      </tr>              
                    </table>
                  </div>
                </div>
                 	     
                <div class="form_a">
                  <p class="form_title"><b>��������</b></p>
                  <div class="form_table">
                    <table width="0" border="0" cellspacing="0" cellpadding="0">        
                      <tr>
                        <th><span class="xing">*</span>�������ɣ�</th>
                        <td class="text"><p><span><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="100" Width="600px"  Text ="���Ѱ����߻��У�����û�з���ţ�̵��κν��ɣ��������ܡ�һ���̡�������"/></span></p></td>
                      </tr>              
                      </table>
                  </div>
                </div>
                	           	  	    	    <div class="form_a">
            <p class="form_title"><b>Ԥ����ص�</b></p>
		    <div class="form_table">
                <table width="0" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                       <th><span class="xing">*</span>Ԥ����ص㣺</th>
                       <td class="select select_auto"><uc4:ucAreaSelXian ID="UcAreaSelXian1" runat="server" /> <b class="ps">������ѡ�����ص㣬ȷ���󲻿ɱ��</b></td>
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
<script language="javascript" type="text/javascript">
        Sys.Application.add_load(
             function() {
                 var form = Sys.WebForms.PageRequestManager.getInstance()._form;
                 form._initialAction = form.action = window.location.href;
             }
         );
</script>