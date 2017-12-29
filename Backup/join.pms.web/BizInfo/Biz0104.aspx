<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Biz0104.aspx.cs" Inherits="join.pms.web.BizInfo.Biz0104" %>
<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc3" %>
<%@ Register Src="/userctrl/ucAreaSelXian.ascx" TagName="ucAreaSelXian" TagPrefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>计生家庭特别扶助对象申请审批</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
    <script src="/Scripts/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jsdate.js" type="text/javascript"></script>
    <script type="text/javascript" language="JavaScript1.2">
    //读卡信息读取
    function ReadCardA_onclick()
    {
        var nRet;
        str = SynCardOcx1.FindReader();
  	    if (str > 0)
  	    {    	
            nRet = SynCardOcx1.ReadCardMsg();
            if(nRet==0)
            {
                //姓名A
                document.getElementById('<%=txtPersonNameA.ClientID%>').value = SynCardOcx1.NameA;
                //身份证号A
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
                //姓名A
                document.getElementById('<%=txtPersonNameB.ClientID%>').value = SynCardOcx1.NameA;
                //身份证号A
                document.getElementById('<%=txtPersonCidB.ClientID%>').value = SynCardOcx1.CardNo;
            }
        }
    }
    </script>
</head>
<body>
<object classid="clsid:46E4B248-8A41-45C5-B896-738ED44C1587" id="SynCardOcx1" codebase="SynCardOcx.CAB#version=1,0,0,1" width="0" height="0" ></object>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列信息后；点击“提交”按钮……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
 <div class="form_bg">


	    <div class="form_a">
	      <p class="form_title"><b>申请人基本信息</b><span class="ps">女方必须年满49周岁才可以申请</span></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>姓名：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameA" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>身份证号：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18"  onblur="CheckCID0104('txtPersonCidA');"/></span></p>
    <input type="button" name="btnCardA" value="刷身份证" class="button button2" onclick="ReadCardA_onclick();"/>
    <input type="button" name="button" value="自动获取资料" class="button" onclick="GetPersonsInfo(document.getElementById('txtPersonCidA').value,'0104A','1');"/>
    </td>
  </tr> 
  <tr>
    <th>外籍身份证号：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidWB" runat="server" EnableViewState="False" MaxLength="18"/></span></p></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>婚姻状况：</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeA" runat="server" onchange="ShowMarry0103();MarryDateTB('ddlMarryTypeB','txtMarryDateB','ddlMarryTypeA','txtMarryDateA');">
                              
                            <asp:ListItem Selected>初婚</asp:ListItem>
<asp:ListItem>再婚</asp:ListItem>
                            <asp:ListItem>离婚</asp:ListItem>
                            <asp:ListItem>复婚</asp:ListItem>  
                            <asp:ListItem>丧偶</asp:ListItem>
                            <asp:ListItem>未婚</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>  
  <tr id="panelMarry" style="display:none;">
      <th><span class="xing">*</span>婚变日期：</th>
      <td class="text"><p><span><input id="txtMarryDateA" runat="server"  onclick="SelectDate(document.getElementById('txtMarryDateA'),'yyyy-MM-dd')"  onchange="MarryDateTB('ddlMarryTypeA','txtMarryDateA','ddlMarryTypeB','txtMarryDateB');"  /></span></p></td>
    </tr>
  <tr>
    <th><span class="xing">*</span>户口性质：</th>
    <td class="select"><asp:DropDownList ID="ddlRegisterTypeA" runat="server">
                        <asp:ListItem>农业</asp:ListItem>
                        <asp:ListItem>非农业</asp:ListItem>   
                        <asp:ListItem>其他</asp:ListItem>           
                       </asp:DropDownList></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>户籍地址：</th>
    <td class="text"><p><span><asp:TextBox ID="txtAreaSelRegNameA" runat="server" EnableViewState="False" Width="300"/></span></p>
    <input type="hidden" name="txtAreaSelRegCodeA" id="txtAreaSelRegCodeA" runat="server" style="display:none;"/></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>联系电话：</th>
    <td class="text"><p><span><asp:TextBox ID="txtContactTelA" runat="server" EnableViewState="False" MaxLength="12"/></span></p><b class="ps">必须填写本人的手机号码，否则将无法收到短信提示</b></td>
  </tr> 
</table>
		  </div>
	    </div>  
	    <div id="panelPeiou" style="display:none;" class="form_a">
	      <p class="form_title"><b>配偶基本信息</b><span class="ps">女方必须年满49周岁才可以申请</span></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>姓名：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameB" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>身份证号：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18"/></span></p>
    <input type="button" name="btnCardB" value="刷身份证" class="button button2" onclick="ReadCardB_onclick();"/>
    <input type="button" name="button" value="自动获取资料" class="button" onclick="GetPersonsInfo(document.getElementById('txtPersonCidB').value,'0104B','1');"/>
    </td>
  </tr>
  <tr>
    <th>外籍身份证号：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidWA" runat="server" EnableViewState="False" MaxLength="18"/></span></p></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>婚姻状况：</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeB" runat="server">  
                            <asp:ListItem>初婚</asp:ListItem>
                            <asp:ListItem>再婚</asp:ListItem>
                            <asp:ListItem>离婚</asp:ListItem>
                            <asp:ListItem>复婚</asp:ListItem>  
                            <asp:ListItem>丧偶</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>
    <tr>
      <th><span class="xing">*</span>婚变日期：</th>
      <td class="text"><p><span><input id="txtMarryDateB" runat="server"  onclick="SelectDate(document.getElementById('txtMarryDateB'),'yyyy-MM-dd')"  /></span></p></td>
    </tr>
  <tr>
    <th><span class="xing">*</span>户口性质：</th>
    <td class="select"><asp:DropDownList ID="ddlRegisterTypeB" runat="server">
                        <asp:ListItem>农业</asp:ListItem>
                        <asp:ListItem>非农业</asp:ListItem>  
                        <asp:ListItem>其他</asp:ListItem>            
                       </asp:DropDownList></td>
  </tr> 
  <tr>
    <th>联系电话：</th>
    <td class="text"><p><span><asp:TextBox ID="txtContactTelB" runat="server" EnableViewState="False" MaxLength="12"/></span></p></td>
  </tr>    
  </table>	    
		  </div>
	    </div>
  	     <div class="form_a">
	      <p class="form_title"><b>生育子女数</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>夫妇曾生育子女数（含收养）：</th>
     <td class="select select_auto"><asp:DropDownList ID="ddlFileds20" runat="server" onchange="ShowBirth0103();">                                       
                                        <asp:ListItem Value="0">0</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                    </asp:DropDownList><span class="t_you">男</span>
                                    <asp:DropDownList ID="ddlFileds21" runat="server" onchange="ShowBirth0103();">                                       
                                        <asp:ListItem Value="0">0</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                    </asp:DropDownList><span class="t_you">女</span></td>

  </tr>   
   <tr>
    <th><span class="xing">*</span>夫妇现有存活子女数（含收养）：</th>
     <td class="select select_auto"><asp:DropDownList ID="ddlFileds37" runat="server" onchange="ShowBirthCH0104();">                                       
                                        <asp:ListItem Value="0">0</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                    </asp:DropDownList><span class="t_you">男</span>
                                    <asp:DropDownList ID="ddlFileds38" runat="server" onchange="ShowBirthCH0104();">                                       
                                        <asp:ListItem Value="0">0</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                    </asp:DropDownList><span class="t_you">女</span></td>

  </tr>   
  <tr>
    <th><span class="xing">*</span>是否领取独生子女证：</th>
     <td class="select select_auto"><asp:DropDownList ID="ddlFileds22" runat="server">                                       
                                        <asp:ListItem>是</asp:ListItem>
                                        <asp:ListItem>否</asp:ListItem>
                                    </asp:DropDownList></td>

  </tr>   
</table>
		  </div>
	    </div> 
<div id="panelChildren" style="display:none;" class="form_a">
  <p class="form_title"><b>夫妇曾经生育子女情况</b></p>
  <div class="form_table">
  <table width="0" border="0" cellspacing="0" cellpadding="0" class="tdcolumns">	        
    <tr><th><span class="xing">*</span>姓名</th><th><span class="xing">*</span>性别</th><th><span class="xing">*</span>出生年月</th><th><span class="xing">*</span>是否亲生</th><th><span class="xing">*</span>存活状况</th><th>死/残日期</th><th>死/残确认单位</th></tr>
    <tr id="tableChild1">
        <td class="text"><p><span><asp:TextBox ID="txtChildName1" runat="server" EnableViewState="False" MaxLength="25" style="width:80px;"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex1" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday1" runat="server"  style="width:80px;" onclick="SelectDate(document.getElementById('txtChildBirthday1'),'yyyy-MM-dd')"   /></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSource1" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></td>
        <td class="select"><asp:DropDownList ID="ddlChildSurvivalStatus1" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></td>
         <td class="text"><p><span><input id="txtChildDeathday1" runat="server"  style="width:80px;" onclick="SelectDate(document.getElementById('txtChildDeathday1'),'yyyy-MM-dd')"   /></span></p></td>
         <td class="select"><asp:DropDownList ID="ddlChildDeathAudit1" runat="server">
                               <asp:ListItem>村委会</asp:ListItem>
                               <asp:ListItem>公安部门</asp:ListItem>                    
                          </asp:DropDownList></td>
    </tr> 
    <tr id="tableChild2">
        <td class="text"><p><span><asp:TextBox ID="txtChildName2" runat="server" EnableViewState="False" MaxLength="25" style="width:80px;"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex2" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday2" runat="server"  style="width:80px;" onclick="SelectDate(document.getElementById('txtChildBirthday2'),'yyyy-MM-dd')"   /></span></p></td>
         <td class="select"><asp:DropDownList ID="ddlChildSource2" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></td>
        <td class="select"><asp:DropDownList ID="ddlChildSurvivalStatus2" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildDeathday2" runat="server" style="width:80px;"  onclick="SelectDate(document.getElementById('txtChildDeathday2'),'yyyy-MM-dd')"   /></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildDeathAudit2" runat="server">
                               <asp:ListItem>村委会</asp:ListItem>
                               <asp:ListItem>公安部门</asp:ListItem>                    
                          </asp:DropDownList></td>
    </tr>  
    <tr id="tableChild3">
        <td class="text"><p><span><asp:TextBox ID="txtChildName3" runat="server" EnableViewState="False" MaxLength="25" style="width:80px;"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex3" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday3" runat="server"  style="width:80px;" onclick="SelectDate(document.getElementById('txtChildBirthday3'),'yyyy-MM-dd')"   /></span></p></td>
          <td class="select"><asp:DropDownList ID="ddlChildSource3" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></td>
        <td class="select"><asp:DropDownList ID="ddlChildSurvivalStatus3" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></td>
       <td class="text"><p><span><input id="txtChildDeathday3" runat="server" style="width:80px;"  onclick="SelectDate(document.getElementById('txtChildDeathday3'),'yyyy-MM-dd')"   /></span></p></td>
       <td class="select"><asp:DropDownList ID="ddlChildDeathAudit3" runat="server">
                               <asp:ListItem>村委会</asp:ListItem>
                               <asp:ListItem>公安部门</asp:ListItem>                    
                          </asp:DropDownList></td>
    </tr> 
    <tr id="tableChild4">
        <td class="text"><p><span><asp:TextBox ID="txtChildName4" runat="server" EnableViewState="False" MaxLength="25" style="width:80px;"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex4" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday4" runat="server"  style="width:80px;" onclick="SelectDate(document.getElementById('txtChildBirthday4'),'yyyy-MM-dd')"   /></span></p></td>
         <td class="select"><asp:DropDownList ID="ddlChildSource4" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></td>
        <td class="select"><asp:DropDownList ID="ddlChildSurvivalStatus4" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></td>
       <td class="text"><p><span><input id="txtChildDeathday4" runat="server" style="width:80px;" onclick="SelectDate(document.getElementById('txtChildDeathday4'),'yyyy-MM-dd')"   /></span></p></td>
       <td class="select"><asp:DropDownList ID="ddlChildDeathAudit4" runat="server">
                               <asp:ListItem>村委会</asp:ListItem>
                               <asp:ListItem>公安部门</asp:ListItem>                    
                          </asp:DropDownList></td>
    </tr> 
    <tr id="tableChild5">
        <td class="text"><p><span><asp:TextBox ID="txtChildName5" runat="server" EnableViewState="False" MaxLength="25" style="width:80px;"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex5" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday5" runat="server"  style="width:80px;" onclick="SelectDate(document.getElementById('txtChildBirthday5'),'yyyy-MM-dd')"   /></span></p></td>
         <td class="select"><asp:DropDownList ID="ddlChildSource5" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></td>
        <td class="select"><asp:DropDownList ID="ddlChildSurvivalStatus5" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></td>
       <td class="text"><p><span><input id="txtChildDeathday5" runat="server" style="width:80px;"  onclick="SelectDate(document.getElementById('txtChildDeathday5'),'yyyy-MM-dd')"   /></span></p></td>
       <td class="select"><asp:DropDownList ID="ddlChildDeathAudit5" runat="server">
                               <asp:ListItem>村委会</asp:ListItem>
                               <asp:ListItem>公安部门</asp:ListItem>                    
                          </asp:DropDownList></td>
    </tr> 
    <tr id="tableChild6">
        <td class="text"><p><span><asp:TextBox ID="txtChildName6" runat="server" EnableViewState="False" MaxLength="25" style="width:80px;"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex6" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday6" runat="server"  style="width:80px;" onclick="SelectDate(document.getElementById('txtChildBirthday6'),'yyyy-MM-dd')"   /></span></p></td>
         <td class="select"><asp:DropDownList ID="ddlChildSource6" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></td>
        <td class="select"><asp:DropDownList ID="ddlChildSurvivalStatus6" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildDeathday6" runat="server" style="width:80px;" onclick="SelectDate(document.getElementById('txtChildDeathday6'),'yyyy-MM-dd')"   /></span></p></td>
       <td class="select"><asp:DropDownList ID="ddlChildDeathAudit6" runat="server">
                               <asp:ListItem>村委会</asp:ListItem>
                               <asp:ListItem>公安部门</asp:ListItem>                    
                          </asp:DropDownList></td>
    </tr> 
  </table>         
    </div>
</div>  
 <div id="panelShangCan" style="display:none;"  class="form_a">
	      <p class="form_title"><b>伤残信息</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>残疾证号码：</th>
    <td class="text"><p><span><asp:TextBox ID="txtFileds23" runat="server" EnableViewState="False" MaxLength="12"/></span></p></td>
  </tr>   
   <tr>
    <th><span class="xing">*</span>残疾类型：</th>
     <td class="select"><asp:DropDownList ID="ddlFileds24" runat="server">                                       
                                        <asp:ListItem>视力残疾</asp:ListItem>
                                        <asp:ListItem>听力语言残疾</asp:ListItem>
                                        <asp:ListItem>智力残疾</asp:ListItem>
                                        <asp:ListItem>肢体残疾</asp:ListItem>
                                        <asp:ListItem>精神残疾</asp:ListItem>
                                        <asp:ListItem>其它</asp:ListItem>
                                    </asp:DropDownList></td>

  </tr>   
   <tr>
    <th><span class="xing">*</span>残疾等级：</th>
     <td class="select"><asp:DropDownList ID="ddlFileds26" runat="server">                                       
                                        <asp:ListItem>一级</asp:ListItem>
                                        <asp:ListItem>二级</asp:ListItem>
                                        <asp:ListItem>三级</asp:ListItem>
                                    </asp:DropDownList></td>

  </tr>   
</table>
		  </div>
	    </div>
<div class="form_a">
  <p class="form_title"><b>申请理由</b></p>
  <div class="form_table">
    <table width="0" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <th><span class="xing">*</span>申请理由：</th>
        <td class="text"><p><span><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="100" Width="600px" Text="符合计划生育家庭特别扶助政策"/></span></p></td>
      </tr>             
      </table>
  </div></div>
 
 	  <div class="form_a">
            <p class="form_title"><b>预领证地点</b></p>
		    <div class="form_table">
                <table width="0" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                       <th><span class="xing">*</span>预领证地点：</th>
                       <td class="select select_auto"><uc4:ucAreaSelXian ID="UcAreaSelXian1" runat="server" /> <b class="ps">请慎重选择领证地点，确定后不可变更</b></td>
                    </tr>
                </table>
            </div>    
	    </div>
	  </div>
	  <div class="check"><asp:CheckBox ID="cbOk" runat="server" /> 承诺：本人保证以上情况及提供的相关材料属实。如有不实，愿意承担由此引起的相应法律责任，并将记入国家诚信系统。</div>
	<br/>
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
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
ShowMarry0103();ShowBirth0103();ShowBirthCH0104();
}
</script>