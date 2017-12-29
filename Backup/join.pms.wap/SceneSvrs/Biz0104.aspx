<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Biz0104.aspx.cs" Inherits="join.pms.wap.SceneSvrs.Biz0104" %>
<%@ Register Src="/userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc3" %>
<%@ Register Src="/userctrl/Uc_PageTop.ascx" TagName="Uc_PageTop" TagPrefix="uc1" %>
<%@ Register Src="/userctrl/Uc_Footer.ascx" TagName="Uc_Footer" TagPrefix="uc2" %>
<%@ Register Src="/userctrl/ucAreaSelXian.ascx" TagName="ucAreaSelXian" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta name="apple-mobile-web-app-capable" content="yes" />
<meta name="apple-mobile-web-app-status-bar-style" content="black" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0, maximum-scale=1.0,user-scalable=yes" />
<!--<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0 , maximum-scale=1.0, user-scalable=0">-->
<meta name="generator" content="bd-mobcard" />
<meta name="format-detection" content="telephone=yes" />
<meta http-equiv="content-type" content="application/vnd.wap.xhtml+xml;charset=UTF-8"/>
<title>计划生育家庭特别扶助对象申请审核</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jsdate.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>

<div class="block">
  <!--备注 -->
  
  <!--申请步骤 -->
  <div class="flow_pic clearfix">
    <ul>
	  <li class="on"><p><b>1</b>填写申请资料</p></li>
	  <li><p><b>2</b>提交所需证件</p></li>
	  <li><p><b>3</b>申请成功</p></li>
	</ul>
  </div>
  
  <!--相关内容 -->
  <div class="part_05">
    <div class="part_name">计生家庭特别扶助对象申请审核</div>

	<div class="part_form">
	  <div class="part_title"><span class="fr">∨</span>申请人基本信息<span class="ps">女方必须年满49周岁才可以申请</span></div>
	  <ul>		
		<li>
		  <p class="title"><span class="xing">*</span>姓名</p>
		  <p class="text"><asp:TextBox ID="txtPersonNameA" runat="server" EnableViewState="False" MaxLength="25"/></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>身份证号</p>
		  <p class="text"><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18" onblur="CheckCID0104('txtPersonCidA');" /></p>
		</li>	
		<li>
		  <p class="title"><span class="xing">*</span>婚姻状况</p>
		  <p class="select"><asp:DropDownList ID="ddlMarryTypeA" runat="server" onchange="ShowMarry0103();MarryDateTB('ddlMarryTypeB','txtMarryDateB','ddlMarryTypeA','txtMarryDateA');">
                            <asp:ListItem>未婚</asp:ListItem>  
                            <asp:ListItem Selected>初婚</asp:ListItem>
                            <asp:ListItem>再婚</asp:ListItem>
                            <asp:ListItem>离婚</asp:ListItem>
                            <asp:ListItem>复婚</asp:ListItem> 
                            <asp:ListItem>丧偶</asp:ListItem>
                        </asp:DropDownList></p>
		</li>
		<li id="panelMarry" style="display:none;">
		  <p class="title"><span class="xing">*</span>婚变日期</p>
		  <p class="text"><input id="txtMarryDateA" runat="server"  onclick="SelectDate(document.getElementById('txtMarryDateA'),'yyyy-MM-dd')" readonly="readonly" onchange="MarryDateTB('ddlMarryTypeA','txtMarryDateA','ddlMarryTypeB','txtMarryDateB');"  /></p>
		</li>	
		<li>
		  <p class="title"><span class="xing">*</span>户口性质</p>
		  <p class="select"><asp:DropDownList ID="ddlRegisterTypeA" runat="server">
                        <asp:ListItem>农业</asp:ListItem>
                        <asp:ListItem>非农业</asp:ListItem>   
                        <asp:ListItem>其他</asp:ListItem>           
                       </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>户籍地址</p> 
		  <p class="select"><asp:TextBox ID="txtAreaSelRegNameA" runat="server" EnableViewState="False" /></p>
		</li>	<input type="hidden" name="txtAreaSelRegCodeA" id="txtAreaSelRegCodeA" runat="server" style="display:none;"/>
		<li>
		  <p class="title"><span class="xing">*</span>联系电话<b class="ps">必须填写本人的手机号码，否则将无法收到短信提示</b></p>
		  <p class="text"><asp:TextBox ID="txtContactTelA" runat="server" EnableViewState="False" MaxLength="12"/></p>
		</li>	
	  </ul>
	</div>
	
	<div id="panelPeiou" style="display:none;" class="part_form">
	  <div class="part_title"><span class="fr">∨</span>配偶基本信息<span class="ps">女方必须年满49周岁才可以申请</span></div>
	 <ul>		
		<li>
		  <p class="title"><span class="xing">*</span>姓名</p>
		  <p class="text"><asp:TextBox ID="txtPersonNameB" runat="server" EnableViewState="False" MaxLength="25"/></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>身份证号</p>
		  <p class="text"><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18" onblur="CheckCID0104('txtPersonCidB');"/></p>
		</li>	
		<li>
		  <p class="title"><span class="xing">*</span>婚姻状况</p>
		  <p class="select"><asp:DropDownList ID="ddlMarryTypeB" runat="server">  
                            <asp:ListItem>初婚</asp:ListItem>
                            <asp:ListItem>再婚</asp:ListItem>
                            <asp:ListItem>离婚</asp:ListItem>
                            <asp:ListItem>复婚</asp:ListItem> 
                            <asp:ListItem>丧偶</asp:ListItem>
                        </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>婚变日期</p>
		  <p class="text"><input id="txtMarryDateB" runat="server"  onclick="SelectDate(document.getElementById('txtMarryDateB'),'yyyy-MM-dd')" readonly="readonly" /></p>
		</li>	
		<li>
		  <p class="title"><span class="xing">*</span>户口性质</p>
		  <p class="select"><asp:DropDownList ID="ddlRegisterTypeB" runat="server">
                        <asp:ListItem>农业</asp:ListItem>
                        <asp:ListItem>非农业</asp:ListItem>    
                        <asp:ListItem>其他</asp:ListItem>          
                       </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title">联系电话</p>
		  <p class="text"><asp:TextBox ID="txtContactTelB" runat="server" EnableViewState="False" MaxLength="12"/></p>
		</li>
	  </ul>
	</div>	
	
	<div class="part_form">
	  <div class="part_title"><span class="fr">∨</span>生育子女数</div>
	 <ul>			
		<li>
		  <p class="title"><span class="xing">*</span>夫妇曾生育子女数（含收养）</p>
		  <p class="select"><asp:DropDownList ID="ddlFileds20" runat="server" onchange="ShowBirth0103();">                                       
                                        <asp:ListItem Value="0">0</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                    </asp:DropDownList>男&nbsp;&nbsp;<asp:DropDownList ID="ddlFileds21" runat="server" onchange="ShowBirth0103();">                                       
                                        <asp:ListItem Value="0">0</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                    </asp:DropDownList>女</p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>夫妇现有存活子女数（含收养）</p>
		  <p class="select"><asp:DropDownList ID="ddlFileds37" runat="server" onchange="ShowBirthCH0104();">                                       
                                        <asp:ListItem Value="0">0</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                    </asp:DropDownList>男&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlFileds38" runat="server" onchange="ShowBirthCH0104();">                                       
                                        <asp:ListItem Value="0">0</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                    </asp:DropDownList>女</p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>是否领取独生子女证</p>
		  <p class="select"><asp:DropDownList ID="ddlFileds22" runat="server">                                       
                                        <asp:ListItem>是</asp:ListItem>
                                        <asp:ListItem>否</asp:ListItem>
                                    </asp:DropDownList></p>
		</li>
	  </ul>
	</div>
	
	<div id="panelChildren" style="display:none;" class="part_form">
	  <div class="part_title"><span class="fr">∨</span>夫妇曾经生育子女情况</div>
	 <ul id="tableChild1">		
		<li>
		  <p class="title"><span class="xing">*</span>子女1姓名</p>
		  <p class="text"><asp:TextBox ID="txtChildName1" runat="server" EnableViewState="False" MaxLength="25"/></p>
		</li>	
		<li>
		  <p class="title"><span class="xing">*</span>子女1性别</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSex1" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>子女1出生年月</p>
		  <p class="text"><input id="txtChildBirthday1" runat="server" onclick="SelectDate(document.getElementById('txtChildBirthday1'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
		<li>
		  <p class="title">子女1是否亲生</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSource1" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>子女1存活状况</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSurvivalStatus1" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title">子女1死亡日期</p>
		  <p class="text"><input id="txtChildDeathday1" runat="server" onclick="SelectDate(document.getElementById('txtChildDeathday1'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
		<li>
		  <p class="title">子女1死/残确认单位</p>
		  <p class="select"><asp:DropDownList ID="ddlChildDeathAudit1" runat="server">
                               <asp:ListItem>村委会</asp:ListItem>
                               <asp:ListItem>公安部门</asp:ListItem>                    
                          </asp:DropDownList></p>
		</li>
	  </ul>
	   <ul id="tableChild2">		
		<li>
		  <p class="title"><span class="xing">*</span>子女2姓名</p>
		  <p class="text"><asp:TextBox ID="txtChildName2" runat="server" EnableViewState="False" MaxLength="25" /></p>
		</li>	
		<li>
		  <p class="title"><span class="xing">*</span>子女2性别</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSex2" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>子女2出生年月</p>
		  <p class="text"><input id="txtChildBirthday2" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday2'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
		<li>
		  <p class="title">子女2是否亲生</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSource2" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>子女2存活状况</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSurvivalStatus2" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title">子女2死亡日期</p>
		  <p class="text"><input id="txtChildDeathday2" runat="server" onclick="SelectDate(document.getElementById('txtChildDeathday2'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
		<li>
		  <p class="title">子女2死/残确认单位</p>
		  <p class="select"><asp:DropDownList ID="ddlChildDeathAudit2" runat="server">
                               <asp:ListItem>村委会</asp:ListItem>
                               <asp:ListItem>公安部门</asp:ListItem>                    
                          </asp:DropDownList></p>
		</li>
	  </ul>
	   <ul id="tableChild3">		
		<li>
		  <p class="title"><span class="xing">*</span>子女3姓名</p>
		  <p class="text"><asp:TextBox ID="txtChildName3" runat="server" EnableViewState="False" MaxLength="25"/></p>
		</li>	
		<li>
		  <p class="title"><span class="xing">*</span>子女3性别</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSex3" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>子女3出生年月</p>
		  <p class="text"><input id="txtChildBirthday3" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday3'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
		<li>
		  <p class="title">子女3是否亲生</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSource3" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>子女3存活状况</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSurvivalStatus3" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title">子女3死亡日期</p>
		  <p class="text"><input id="txtChildDeathday3" runat="server"  onclick="SelectDate(document.getElementById('txtChildDeathday3'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
		<li>
		  <p class="title">子女3死/残确认单位</p>
		  <p class="select"><asp:DropDownList ID="ddlChildDeathAudit3" runat="server">
                               <asp:ListItem>村委会</asp:ListItem>
                               <asp:ListItem>公安部门</asp:ListItem>                    
                          </asp:DropDownList></p>
		</li>
	  </ul>
	   <ul id="tableChild4">		
		<li>
		  <p class="title"><span class="xing">*</span>子女4姓名</p>
		  <p class="text"><asp:TextBox ID="txtChildName4" runat="server" EnableViewState="False" MaxLength="25"/></p>
		</li>	
		<li>
		  <p class="title"><span class="xing">*</span>子女4性别</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSex4" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>子女4出生年月</p>
		  <p class="text"><input id="txtChildBirthday4" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday4'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
		<li>
		  <p class="title">子女4是否亲生</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSource4" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>子女4存活状况</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSurvivalStatus4" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title">子女4死亡日期</p>
		  <p class="text"><input id="txtChildDeathday4" runat="server" onclick="SelectDate(document.getElementById('txtChildDeathday4'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
		<li>
		  <p class="title">子女4死/残确认单位</p>
		  <p class="select"><asp:DropDownList ID="ddlChildDeathAudit4" runat="server">
                               <asp:ListItem>村委会</asp:ListItem>
                               <asp:ListItem>公安部门</asp:ListItem>                    
                          </asp:DropDownList></p>
		</li>
	  </ul>
	   <ul id="tableChild5">		
		<li>
		  <p class="title"><span class="xing">*</span>子女5姓名</p>
		  <p class="text"><asp:TextBox ID="txtChildName5" runat="server" EnableViewState="False" MaxLength="25"/></p>
		</li>	
		<li>
		  <p class="title"><span class="xing">*</span>子女5性别</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSex5" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>子女5出生年月</p>
		  <p class="text"><input id="txtChildBirthday5" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday5'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
		<li>
		  <p class="title">子女5是否亲生</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSource5" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>子女5存活状况</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSurvivalStatus5" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title">子女5死亡日期</p>
		  <p class="text"><input id="txtChildDeathday5" runat="server"  onclick="SelectDate(document.getElementById('txtChildDeathday5'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
		<li>
		  <p class="title">子女5死/残确认单位</p>
		  <p class="select"><asp:DropDownList ID="ddlChildDeathAudit5" runat="server">
                               <asp:ListItem>村委会</asp:ListItem>
                               <asp:ListItem>公安部门</asp:ListItem>                    
                          </asp:DropDownList></p>
		</li>
	  </ul>
	   <ul id="tableChild6">		
		<li>
		  <p class="title"><span class="xing">*</span>子女6姓名</p>
		  <p class="text"><asp:TextBox ID="txtChildName6" runat="server" EnableViewState="False" MaxLength="25"/></p>
		</li>	
		<li>
		  <p class="title"><span class="xing">*</span>子女6性别</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSex6" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>子女6出生年月</p>
		  <p class="text"><input id="txtChildBirthday6" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday6'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>子女6是否亲生</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSource6" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>子女6存活状况</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSurvivalStatus6" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title">子女6死/残日期</p>
		  <p class="text"><input id="txtChildDeathday6" runat="server" onclick="SelectDate(document.getElementById('txtChildDeathday6'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
		<li>
		  <p class="title">子女6死/残确认单位</p>
		  <p class="select"><asp:DropDownList ID="ddlChildDeathAudit6" runat="server">
                               <asp:ListItem>村委会</asp:ListItem>
                               <asp:ListItem>公安部门</asp:ListItem>                    
                          </asp:DropDownList></p>
		</li>
	  </ul>
	</div>	
	<div id="panelShangCan" style="display:none;"  class="part_form">
	  <div class="part_title"><span class="fr">∨</span>伤残信息</div>
	 <ul>	
	     <li>
		  <p class="title"><span class="xing">*</span>残疾证号码</p>
		  <p class="text"><asp:TextBox ID="txtFileds23" runat="server" EnableViewState="False" MaxLength="12"/></p>
		</li>	
		<li>
		  <p class="title"><span class="xing">*</span>残疾类型</p>
		  <p class="select"><asp:DropDownList ID="ddlFileds24" runat="server">                                       
                                        <asp:ListItem>视力残疾</asp:ListItem>
                                        <asp:ListItem>听力语言残疾</asp:ListItem>
                                        <asp:ListItem>智力残疾</asp:ListItem>
                                        <asp:ListItem>肢体残疾</asp:ListItem>
                                        <asp:ListItem>精神残疾</asp:ListItem>
                                        <asp:ListItem>其它</asp:ListItem>
                                    </asp:DropDownList></p>
		</li>		
		<li>
		  <p class="title"><span class="xing">*</span>残疾等级</p>
		  <p class="select"><asp:DropDownList ID="ddlFileds26" runat="server">                                       
                                        <asp:ListItem>一级</asp:ListItem>
                                        <asp:ListItem>二级</asp:ListItem>
                                        <asp:ListItem>三级</asp:ListItem>
                                    </asp:DropDownList></p>
		</li>
	  </ul>
	</div>
	<div class="part_form">
	  <div class="part_title"><span class="fr">∨</span>申请理由</div>
	 <ul>		
		<li>
		  <p class="title"><span class="xing">*</span>申请理由</p>
		  <p class="text"><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="100" Text="符合计划生育家庭特别扶助政策"/></p>
		</li>
	  </ul>
	</div>
	
	<div class="part_form">
              <div class="part_title"><span class="fr">∨</span>预领证地点<span class="ps">请慎重选择领证地点，确定后不可变更</span></div>
              <ul>
                <li>
                    <p class="title"><span class="xing">*</span>预领证地点</p>
                    <p class="select"><uc4:ucAreaSelXian ID="UcAreaSelXian1" runat="server" /> </p>
                </li>	
              </ul>
            </div>
	<div class="check"><asp:CheckBox ID="cbOk" runat="server" /> 承诺：本人保证以上情况及提供的相关材料属实。如有不实，愿意承担由此引起的相应法律责任，并将记入国家诚信系统。</div>
	  <div class="part_button"><asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="下一步，请继续！"></asp:Button></div>
 </div>
</div>

<uc2:Uc_Footer id="Uc_Footer1" runat="server"/>
    </form>
</body>
</html>
<script>
 window.onload=function(){
ShowMarry0103();ShowBirth0103();ShowBirthCH0104();
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