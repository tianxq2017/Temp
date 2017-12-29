<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Biz0101.aspx.cs" Inherits="join.pms.wap.SceneSvrs.Biz0101" %>
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
<title>一孩生育登记表</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jsdate.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>
   <div class="block">
  <!--备注 -->
 <!-- <div class="part_03">
	<p class="title">说明</p>
    <p class="sum">
	1、请如实填写信息，否则由此引起的相应法律责任由个人承担；<br />
	2、如手机无法申请，请登录网页版申请。
	</p>
  </div>
  <div class="clr10"></div>-->
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
    <div class="part_name">一孩生育登记表</div>	

	<div class="part_form">
	  <div class="part_title"><span class="fr">∨</span>女方基本信息</div>
	  <ul>
		<li>
		  <p class="title"><span class="xing">*</span>姓名</p>
		  <p class="text"><asp:TextBox ID="txtPersonNameB" runat="server" EnableViewState="False" MaxLength="25"/></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>身份证号</p>
		  <p class="text"><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18" onblur="CheckCID('1','txtPersonCidB');"/></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>婚姻状况</p>
		  <p class="select"><asp:DropDownList ID="ddlMarryTypeB" runat="server" onchange="ShowMarry0101();MarryDateTB('ddlMarryTypeA','txtMarryDateA','ddlMarryTypeB','txtMarryDateB');">  
                            <asp:ListItem>未婚</asp:ListItem>                         
                            <asp:ListItem Selected>初婚</asp:ListItem>
                            <asp:ListItem>再婚</asp:ListItem>
                            <asp:ListItem>离婚</asp:ListItem>
                            <asp:ListItem>复婚</asp:ListItem>  
                            <asp:ListItem>丧偶</asp:ListItem>
                        </asp:DropDownList></p>
		</li>
		<li id="panelMarry" style="display:none;">
		  <p class="title"><span class="xing">*</span>婚姻时间</p>
		  <p class="text"><input id="txtMarryDateB" runat="server"  onclick="SelectDate(document.getElementById('txtMarryDateB'),'yyyy-MM-dd')" readonly="readonly" onchange="MarryDateTB('ddlMarryTypeB','txtMarryDateB','ddlMarryTypeA','txtMarryDateA');" /></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>民族</p>
		  <div class="select"><asp:Literal ID="LiteralNationsB" runat="server" EnableViewState="false"></asp:Literal>
    <input type="hidden" name="txtHNationsB" id="txtHNationsB" runat="server" style="display:none;"/></div>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>户口性质</p>
		  <div class="select"><asp:DropDownList ID="ddlRegisterTypeB" runat="server">
                        <asp:ListItem>农业</asp:ListItem>
                        <asp:ListItem>非农业</asp:ListItem>    
                        <asp:ListItem>其他</asp:ListItem>          
                       </asp:DropDownList></div>
		</li>
		<li style="display:none">
		  <p class="title">是否是独生子女</p>
		  <p class="select"><asp:DropDownList ID="ddlFileds16" runat="server">
                        <asp:ListItem Selected></asp:ListItem>
                        <asp:ListItem>是</asp:ListItem>
                        <asp:ListItem>否</asp:ListItem>            
                       </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>户籍地址</p>
		  <p class="select"><uc3:ucAreaSel ID="UcAreaSelRegB" runat="server" /></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>现居住地址</p>
		  <p class="select"><uc3:ucAreaSel ID="UcAreaSelCurB" runat="server" /><a href="javascript:void(0);" class="button" onclick="AreaCodeTB('UcAreaSelRegB_txtSelAreaCode','UcAreaSelRegB_txtSelectArea','UcAreaSelCurB_txtSelAreaCode','UcAreaSelCurB_txtSelectArea');">同上</a></p>
		</li>
		<li>
		  <p class="title">工作单位</p>
		  <p class="text"><asp:TextBox ID="txtWorkUnitB" runat="server" EnableViewState="False" MaxLength="25"/></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>联系电话<b class="ps">必须填写本人的手机号码，否则将无法收到短信提示</b></p>
		  <p class="text"><asp:TextBox ID="txtContactTelB" runat="server" EnableViewState="False" MaxLength="12"/></p>
		</li>  </ul>
	</div>
	
	<div id="panelPeiou" style="display:none;" class="part_form">
	  <div class="part_title"><span class="fr">∨</span>男方基本信息</div>
	  <ul>
		<li>
		  <p class="title"><span class="xing">*</span>姓名</p>
		  <p class="text"><asp:TextBox ID="txtPersonNameA" runat="server" EnableViewState="False" MaxLength="25"/></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>身份证号</p>
		  <p class="text"><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18" onblur="CheckCID('0','txtPersonCidA');"/></p>
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
		  <p class="text"><input id="txtMarryDateA" runat="server"  onclick="SelectDate(document.getElementById('txtMarryDateA'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>民族</p>
		  <div class="select"><asp:Literal ID="LiteralNationsA" runat="server" EnableViewState="false"></asp:Literal>
    <input type="hidden" name="txtHNationsA" id="txtHNationsA" runat="server" style="display:none;"/></div>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>户口性质</p>
		  <div class="select"><asp:DropDownList ID="ddlRegisterTypeA" runat="server">
                        <asp:ListItem>农业</asp:ListItem>
                        <asp:ListItem>非农业</asp:ListItem>    
                        <asp:ListItem>其他</asp:ListItem>          
                       </asp:DropDownList></div>
		</li>
		<li style="display:none">
		  <p class="title">是否是独生子女</p>
		  <p class="select"><asp:DropDownList ID="ddlFileds17" runat="server">
                        <asp:ListItem Selected></asp:ListItem>
                        <asp:ListItem>是</asp:ListItem>
                        <asp:ListItem>否</asp:ListItem>            
                       </asp:DropDownList></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>户籍地址</p>
		  <p class="select"><uc3:ucAreaSel ID="UcAreaSelRegA" runat="server" /></p>
		</li>
		<li>
		  <p class="title"><span class="xing">*</span>现居住地址</p>
		  <p class="select"><uc3:ucAreaSel ID="UcAreaSelCurA" runat="server" /><a href="javascript:void(0);" class="button" onclick="AreaCodeTB('UcAreaSelRegA_txtSelAreaCode','UcAreaSelRegA_txtSelectArea','UcAreaSelCurA_txtSelAreaCode','UcAreaSelCurA_txtSelectArea');">同上</a></p>
		</li>
		<li>
		  <p class="title">工作单位</p>
		  <p class="text"><asp:TextBox ID="txtWorkUnitA" runat="server" EnableViewState="False" MaxLength="25"/></p>
		</li>
		<li>
		  <p class="title">联系电话</p>
		  <p class="text"><asp:TextBox ID="txtContactTelA" runat="server" EnableViewState="False" MaxLength="12"/></p>
		</li>
	  </ul>
	</div>
	
	<div class="part_form">
	  <div class="part_title"><span class="fr">∨</span>子女信息</div>
	  <ul>
		<li>
		  <p class="title"><span class="xing">*</span>子女数</p>
		  <p class="select"><asp:DropDownList ID="ddlBirthNum" runat="server" onchange="ShowBirth0101();">
                                         <asp:ListItem Value="0">0</asp:ListItem>
                                         <asp:ListItem Value="1">1</asp:ListItem>
                                         <asp:ListItem Value="2">2</asp:ListItem>
                                         <asp:ListItem Value="3">3</asp:ListItem>
                                    </asp:DropDownList></p>
		</li>
	  </ul>
	</div>
	<div id="panelChildren" style="display:none;" class="part_form">
	  <div class="part_title"><span class="fr">∨</span>新生儿信息</div>
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
		  <p class="text"><input id="txtChildBirthday1" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday1'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
		<li>
		  <p class="title">子女1身份证号或出生医学证明或收养证号</p>
		  <p class="text"><asp:TextBox ID="txtChildCardID1" runat="server" EnableViewState="False" MaxLength="25"/></p>
		</li>
	  </ul>
	  <ul id="tableChild2">
		<li>
		  <p class="title"><span class="xing">*</span>子女2姓名</p>
		  <p class="text"><asp:TextBox ID="txtChildName2" runat="server" EnableViewState="False" MaxLength="25"/></p>
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
		  <p class="title">子女2身份证号或出生医学证明或收养证号</p>
		  <p class="text"><asp:TextBox ID="txtChildCardID2" runat="server" EnableViewState="False" MaxLength="25"/></p>
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
		  <p class="text"><input id="txtChildBirthday3" runat="server" onclick="SelectDate(document.getElementById('txtChildBirthday3'),'yyyy-MM-dd')" readonly="readonly"  /></p>
		</li>
		<li>
		  <p class="title">子女3身份证号或出生医学证明或收养证号</p>
		  <p class="text"><asp:TextBox ID="txtChildCardID3" runat="server" EnableViewState="False" MaxLength="25"/></p>
		</li>
	  </ul>
	</div>
	
	<div class="part_form">
	  <div class="part_title"><span class="fr">∨</span><span class="xing">*</span>申请理由</div>
	  <ul>
		<li>
		  <p class="text"><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="100" Text="符合《内蒙古自治区人口与计划生育条例》规定，政策内生育第一孩。"/></p>
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
	 <div class="part_button"><asp:Button ID="btnAdd" runat="server" Text="提交申请" OnClick="btnAdd_Click"></asp:Button></div>
  </div>
</div>
   <uc2:Uc_Footer id="Uc_Footer1" runat="server"/>
    </form>
</body>
</html>
<script>
 window.onload=function(){
 ShowMarry0101();ShowBirth0101();
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