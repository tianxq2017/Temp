<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Biz0105.aspx.cs" Inherits="join.pms.wap.SceneSvrs.Biz0105" %>
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
<title>计划生育“少生快富”工程申请审核</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jsdate.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>
    <div class="block">
    
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
    <div class="part_name">“少生快富”工程申请审核</div>
	
	<div class="part_form">
	  <div class="part_title"><span class="fr">∨</span>育龄妇女基本信息<span class="ps">女方必须在49周岁内才可以申请</span></div>
	  <ul>		
		<li>
		  <p class="title"><span class="xing">*</span>姓名</p>
		  <p class="text"><asp:TextBox ID="txtPersonNameB" runat="server" EnableViewState="False" MaxLength="25"/></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>身份证号</p>
		  <p class="text"><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18" onblur="CheckCID0105('1','txtPersonCidB');"/></p>
		</li>	
		<li>
		  <p class="title"><span class="xing">*</span>婚姻状况</p>
		  <p class="select"><asp:DropDownList ID="ddlMarryTypeB" runat="server" onchange="MarryDateTB('ddlMarryTypeA','txtMarryDateA','ddlMarryTypeB','txtMarryDateB');">  
                            <asp:ListItem Selected>初婚</asp:ListItem>
                            <asp:ListItem>再婚</asp:ListItem>
                            <asp:ListItem>离婚</asp:ListItem>
                            <asp:ListItem>复婚</asp:ListItem> 
                            <asp:ListItem>丧偶</asp:ListItem>
                        </asp:DropDownList></p>
	    </li>
		<li>
		  <p class="title"><span class="xing">*</span>婚姻时间</p>
		  <p class="select"><input id="txtMarryDateB" runat="server"  onclick="SelectDate(document.getElementById('txtMarryDateB'),'yyyy-MM-dd')" readonly="readonly" onchange="MarryDateTB('ddlMarryTypeB','txtMarryDateB','ddlMarryTypeA','txtMarryDateA');" /></p>
	    </li>
		<li>
		  <p class="title"><span class="xing">*</span>民族</p>
		  <p class="select"><asp:Literal ID="LiteralNationsB" runat="server" EnableViewState="false"></asp:Literal>
    <input type="hidden" name="txtHNationsB" id="txtHNationsB" runat="server" style="display:none;"/></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>户口性质</p>
		  <p class="select"><asp:DropDownList ID="ddlRegisterTypeB" runat="server">
                        <asp:ListItem>农业</asp:ListItem>            
                       </asp:DropDownList></p>
		</li>
	  </ul>
	</div>
	<div class="part_form">
	  <div class="part_title"><span class="fr">∨</span>男方基本信息</div>
	  <ul>		
		<li>
		  <p class="title"><span class="xing">*</span>姓名</p>
		  <p class="text"><asp:TextBox ID="txtPersonNameA" runat="server" EnableViewState="False" MaxLength="25"/></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>身份证号</p>
		  <p class="text"><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18" onblur="CheckCID0105('0','txtPersonCidA');"/></p>
		</li>	
		<li>
		  <p class="title"><span class="xing">*</span>婚姻状况</p>
		  <p class="select"><asp:DropDownList ID="ddlMarryTypeA" runat="server">
                            <asp:ListItem>初婚</asp:ListItem>
                            <asp:ListItem>再婚</asp:ListItem>
                            <asp:ListItem>离婚</asp:ListItem>
                            <asp:ListItem>复婚</asp:ListItem> 
                            <asp:ListItem>丧偶</asp:ListItem>
                        </asp:DropDownList></p>
	    </li>
		<li>
		  <p class="title"><span class="xing">*</span>婚姻时间</p>
		  <p class="select"><input id="txtMarryDateA" runat="server"  onclick="SelectDate(document.getElementById('txtMarryDateA'),'yyyy-MM-dd')" readonly="readonly"  /></p>
	    </li>
		<li>
		  <p class="title"><span class="xing">*</span>民族</p>
		  <p class="select"><asp:Literal ID="LiteralNationsA" runat="server" EnableViewState="false"></asp:Literal>
    <input type="hidden" name="txtHNationsA" id="txtHNationsA" runat="server" style="display:none;"/></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>户口性质</p>
		  <p class="select"><asp:DropDownList ID="ddlRegisterTypeA" runat="server">
                        <asp:ListItem>农业</asp:ListItem>            
                       </asp:DropDownList></p>
		</li>
	  </ul>
	</div>
	
	<div class="part_form">
	  <div class="part_title"><span class="fr">∨</span>生育子女数</div>
	 <ul>		
		<li>
		  <p class="title"><span class="xing">*</span>按政策可生育子女数</p>
		  <p class="select"><asp:DropDownList ID="ddlFileds40" runat="server">  
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                    </asp:DropDownList></p>
		</li>		
		<li>
		  <p class="title"><span class="xing">*</span>现有子女数（含收养）</p>
		  <p class="select"><asp:DropDownList ID="ddlFileds37" runat="server" onchange="ShowBirth0105();">                                       
                                        <asp:ListItem Value="0">0</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                    </asp:DropDownList>男&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlFileds38" runat="server" onchange="ShowBirth0105();">                                       
                                        <asp:ListItem Value="0">0</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                    </asp:DropDownList>女</p>
		</li>
	  </ul>
	</div>
	
	<div id="panelChildren" style="display:none;" class="part_form">
	  <div class="part_title"><span class="fr">∨</span>夫妇曾经生育子女情况<span class="ps">育龄妇女或其丈夫选填一位即可</span></div>
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
		  <p class="title"><span class="xing">*</span>子女1存活状况</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSurvivalStatus1" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></p>
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
		  <p class="title"><span class="xing">*</span>子女2存活状况</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSurvivalStatus2" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></p>
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
		  <p class="title"><span class="xing">*</span>子女3存活状况</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSurvivalStatus3" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></p>
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
		  <p class="title"><span class="xing">*</span>子女4存活状况</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSurvivalStatus4" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></p>
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
		  <p class="title"><span class="xing">*</span>子女5存活状况</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSurvivalStatus5" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></p>
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
		  <p class="title"><span class="xing">*</span>子女6存活状况</p>
		  <p class="select"><asp:DropDownList ID="ddlChildSurvivalStatus6" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></p>
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
	  </ul>
	</div>	
	<div class="part_form">
	  <div class="part_title"><span class="fr">∨</span>联系方式</div>
	  <ul>		
		<li>
		  <p class="title"><span class="xing">*</span>户籍地址</p>
		  <p class="text"><asp:TextBox ID="txtAreaSelRegNameB" runat="server" EnableViewState="False" /></p>
		</li>	<input type="hidden" name="txtAreaSelRegCodeB" id="txtAreaSelRegCodeB" runat="server" style="display:none;"/>		
		<li>
		  <p class="title"><span class="xing">*</span>联系电话<b class="ps">必须填写本人的手机号码，否则将无法收到短信提示</b></p>
		  <p class="text"><asp:TextBox ID="txtContactTelB" runat="server" EnableViewState="False" MaxLength="12"/></p>
		</li>
	  </ul>
	</div>
	<div class="part_form">
	  <div class="part_title"><span class="fr">∨</span>避孕状况</div>
	  <ul>		
		<li>
		  <p class="title"><span class="xing">*</span>育龄妇女</p>
		  <p class="select"><asp:DropDownList ID="ddlFileds45" runat="server">                                       
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>女扎</asp:ListItem>
                                        <asp:ListItem>放环</asp:ListItem>
                                        <asp:ListItem>皮埋</asp:ListItem>
                                    </asp:DropDownList></p>
		</li>		
		<li>
		  <p class="title"><span class="xing">*</span>育龄妇女丈夫</p>
		  <p class="select"><asp:DropDownList ID="ddlFileds46" runat="server">
                                        <asp:ListItem></asp:ListItem>                                       
                                        <asp:ListItem>男扎</asp:ListItem>
                                    </asp:DropDownList></p>
		</li>			
		<li>
		  <p class="title"><span class="xing">*</span>避孕开始时期</p>
		  <p class="text"><input id="txtFileds44" runat="server"  onclick="SelectDate(document.getElementById('txtFileds44'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
	  </ul>
	</div>	
	 <div class="part_form">
	  <div class="part_title"><span class="fr">∨</span>申请理由</div>
	 <ul>		
		<li>
		  <p class="title"><span class="xing">*</span>申请理由</p>
		  <p class="text"><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="100" Text="符合计划生育“少生快富”工程奖励条件"/></p>
		</li>
	  </ul>
	</div>
    <div class="part_form">
      <div class="part_title"><span class="fr">∨</span>预办理地点<span class="ps">请慎重选择办理地点，确定后不可变更</span></div>
      <ul>
        <li>
            <p class="title"><span class="xing">*</span>预办理地点</p>
            <p class="select"><uc4:ucAreaSelXian ID="UcAreaSelXian1" runat="server" /> </p>
        </li>	
      </ul>
    </div>  
	<div class="check"><asp:CheckBox ID="cbOk" runat="server" /> 承诺：本人保证以上情况及提供的相关材料属实。如有不实，愿意承担由此引起的相应法律责任，并将记入国家诚信系统。</div>
	 <div class="part_button"><asp:Button ID="btnAdd" runat="server" Text="提交申请" OnClick="btnAdd_Click"></asp:Button></div>
 
  </div>
</div>
    <uc2:Uc_Footer id="Uc_Footer1" runat="server"/>
    </form>
</body>
</html>
<script>
 window.onload=function(){
ShowBirth0105();
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