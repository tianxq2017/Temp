<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Biz0107.aspx.cs" Inherits="join.pms.wap.SceneSvrs.Biz0107" %>
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
<title>“一杯奶”受益对象申请审核</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jsdate.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
            <div class="part_name">“一杯奶”受益对象申请审核</div>
	          
            <div class="part_form">
              <div class="part_title"><span class="fr">∨</span>女方基本信息</div>
              <ul>
                      <li>
                        <p class="title"><span class="xing">*</span>姓名</p>
                        <p class="text"><asp:TextBox ID="txtPersonNameB" runat="server" EnableViewState="False" MaxLength="25"/></p>
                      </li> 
                      <li>
                        <p class="title"><span class="xing">*</span>民族</p>
                        <p class="select"><asp:Literal ID="LiteralNationsB" runat="server" EnableViewState="false"></asp:Literal>
                        <input type="hidden" name="txtHNationsB" id="txtHNationsB" runat="server" style="display:none;"/></p>
                      </li> 
                      <li>
                        <p class="title"><span class="xing">*</span>身份证号</p>
                        <p class="text"><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18" onblur="CheckCID('1','txtPersonCidB');"/></p>
                      </li>
                      <li>
                        <p class="title"><span class="xing">*</span>现居地址</p>
                        <p class="select"><uc3:ucAreaSel ID="UcAreaSelCurB" runat="server" /></p>
                      </li>
                       <li>
                        <p class="title"><span class="xing">*</span>怀孕胎次</p>
                        <p class="select"><asp:DropDownList ID="ddlFileds41" runat="server">
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
                        </p>
                      </li> 
                      <li>
                        <p class="title"><span class="xing">*</span>末次月经时间</p>
                        <p class="text"><input id="txtFileds42" runat="server"  onclick="SelectDate(document.getElementById('txtFileds42'),'yyyy-MM-dd')" readonly="readonly"  /></p>
                      </li>
                      <li>
                        <p class="title"><span class="xing">*</span>确认怀孕时间</p>
                        <p class="text"><input id="txtFileds43" runat="server"  onclick="SelectDate(document.getElementById('txtFileds43'),'yyyy-MM-dd')" readonly="readonly"  /></p>
                      </li>
                       <li>
                        <p class="title"><span class="xing">*</span>确认方式</p>
                        <p class="select"><asp:DropDownList ID="ddlFileds29" runat="server">
                            <asp:ListItem>超声诊断</asp:ListItem>
                            <asp:ListItem>尿妊娠试验</asp:ListItem>            
                           </asp:DropDownList>
                        </p>
                      </li>
                       <li>
                        <p class="title"><span class="xing">*</span>确认单位</p>
                        <p class="select"><asp:DropDownList ID="ddlFileds30" runat="server">
                            <asp:ListItem>镇级及以上医疗卫生单位</asp:ListItem>
                            <asp:ListItem>镇级及以上计划生育服务单位</asp:ListItem>
                           </asp:DropDownList>
                        </p>
                      </li>
                      <li>
                        <p class="title"><span class="xing">*</span>联系电话<b class="ps">必须填写本人的手机号码，否则将无法收到短信提示</b></p>
                        <p class="text"><asp:TextBox ID="txtContactTelB" runat="server" EnableViewState="False" MaxLength="12"/></p>
                      </li>
              </ul>
            </div>
            	  
            <div id="panelPeiou" class="part_form">
              <div class="part_title"><span class="fr">∨</span>男方基本信息</div>
              <ul>
                      <li>
                        <p class="title"><span class="xing">*</span>姓名</p>
                        <p class="text"><asp:TextBox ID="txtPersonNameA" runat="server" EnableViewState="False" MaxLength="25"/></p>
                      </li> 
                      <li>
                        <p class="title"><span class="xing">*</span>民族</p>
                        <p class="select"><asp:Literal ID="LiteralNationsA" runat="server" EnableViewState="false"></asp:Literal>
                        <input type="hidden" name="txtHNationsA" id="txtHNationsA" runat="server" style="display:none;"/></p>
                      </li>
                      <li>
                        <p class="title"><span class="xing">*</span>身份证号</p>
                        <p class="text"><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18" onblur="CheckCID('0','txtPersonCidA');"/></p>
                      </li>
                      <li>
                        <p class="title"><span class="xing">*</span>现居地址</p>
                        <p class="select"><uc3:ucAreaSel ID="UcAreaSelCurA" runat="server" /></p>
                      </li>
              </ul>
            </div>
            
	        <div class="part_form">
              <div class="part_title"><span class="fr">∨</span><span class="xing">*</span>结婚时间</div>
              <ul>
                <li>
                    <p class="text"><input id="txtFileds14" runat="server"  onclick="SelectDate(document.getElementById('txtFileds14'),'yyyy-MM-dd')" readonly="readonly"  /></p>
                </li>	
              </ul>
            </div>

	        <div class="part_form">
              <div class="part_title"><span class="fr">∨</span><span class="xing">*</span>服务证号</div>
              <ul>
                <li>
                    <p class="text"><asp:TextBox ID="txtFileds21" runat="server" EnableViewState="False" MaxLength="50"/></p>
                </li>	
              </ul>
            </div>
                    
	        <div class="part_form">
              <div class="part_title"><span class="fr">∨</span><span class="xing">*</span>申请理由</div>
              <ul>
                <li>
                    <p class="text"><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="100" Text ="我已按政策怀孕，本人没有服用牛奶的任何禁忌，申请享受“一杯奶”待遇。"/></p>
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
	          <div class="check"><asp:CheckBox ID="cbOk" runat="server" /> 承诺本人保证以上情况及提供的相关材料属实。如有不实，愿意承担由此引起的相应法律责任，并将记入国家诚信系统。</div>
	          <div class="part_button"><asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="提交申请"></asp:Button></div>
        </div>
  
    </div>
    <uc2:Uc_Footer id="Uc_Footer1" runat="server"/> 
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
