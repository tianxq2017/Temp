<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Biz0110.aspx.cs" Inherits="join.pms.wap.SceneSvrs.Biz0110" %>
<%@ Register Src="/userctrl/Uc_PageTop.ascx" TagName="Uc_PageTop" TagPrefix="uc1" %>
<%@ Register Src="/userctrl/Uc_Footer.ascx" TagName="Uc_Footer" TagPrefix="uc2" %>
<%@ Register Src="/userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc3" %>
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
<title>婚育情况证明申请</title>
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
            <div class="part_name">婚育情况证明申请</div>
	    
	     <div class="part_form">
              <div class="part_title"><span class="fr">∨</span>证明机关</div>
              <ul>
                <li>
                    <p class="title"><span class="xing">*</span>证明机关</p>
                    <p class="select"><asp:DropDownList ID="ddlBizStep" runat="server">
                               <asp:ListItem Value="3">旗镇村三级证明</asp:ListItem>
                               <asp:ListItem Value="2">镇村两级证明</asp:ListItem>           
                          </asp:DropDownList></p><b class="ps">通辽市内镇村两级；通辽市外的两级或三级</b>
                </li>	
              </ul>
            </div>
   
	       
            <div class="part_form">
              <div class="part_title"><span class="fr">∨</span>本人信息</div>
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
                    <p class="title"><span class="xing">*</span>户口性质</p>
                    <p class="select"><asp:DropDownList ID="ddlRegisterTypeA" runat="server">
                                    <asp:ListItem>农业</asp:ListItem>
                                    <asp:ListItem>非农业</asp:ListItem>
                                    <asp:ListItem>其他</asp:ListItem>                              
                                       </asp:DropDownList></p>
                  </li>
                   <li>
                    <p class="title"><span class="xing">*</span>身份证号</p>
                    <p class="text"><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18" /></p>
                  </li>
                  <li>
                    <p class="title"><span class="xing">*</span>联系电话<b class="ps">必须填写本人的手机号码，否则将无法收到短信提示</b></p>
                    <p class="text"><asp:TextBox ID="txtContactTelA" runat="server" EnableViewState="False" MaxLength="12"/></p>
                  </li> 
                  <li>
                    <p class="title"><span class="xing">*</span>婚姻状况</p>
                    <p class="select"><asp:DropDownList ID="ddlMarryTypeA" runat="server"  onchange="ShowMarry0110();">
                                            <asp:ListItem>未婚</asp:ListItem>
                                            <asp:ListItem>初婚</asp:ListItem>
                                            <asp:ListItem>再婚</asp:ListItem>
                                            <asp:ListItem>离婚</asp:ListItem>
                                            <asp:ListItem>复婚</asp:ListItem> 
                                            <asp:ListItem>丧偶</asp:ListItem>
                                        </asp:DropDownList></p>
                  </li>
                      <li>
                    <p class="title"><span class="xing">*</span>户籍地址</p>
                    <p class="text"><asp:TextBox ID="txtAreaSelRegNameA" runat="server" EnableViewState="False" />  
                    <input type="hidden" name="txtAreaSelRegCodeA" id="txtAreaSelRegCodeA" runat="server" style="display:none;"/></p>
                  </li>
                  <li>
                    <p class="title">电子邮件</p>
                    <p class="text"><asp:TextBox ID="txtMail" runat="server" EnableViewState="False" /></p>
                  </li>
                   <li>
                    <p class="title"><span class="xing">*</span>居住地址</p>
                    <p class="select"><uc3:ucAreaSel ID="UcAreaSelCurA" runat="server" /><input type="button" name="button" value="同户籍地" class="button" onclick="AreaCodeTB('txtAreaSelRegCodeA','txtAreaSelRegNameA','UcAreaSelCurA_txtSelAreaCode','UcAreaSelCurA_txtSelectArea');"/></p>
                  </li>
                    <li>
                    <p class="title"><span class="xing">*</span>已生育子女数<b class="ps">超过三个咨询办理</b></p>
                    <p class="select"><asp:DropDownList ID="ddlBirthNum" runat="server" onchange="ShowBirth0101();">
                                            <asp:ListItem Value="0">0</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                   </asp:DropDownList></p>
                  </li>  
               </ul>
            </div>
	  
            <div id="panelPeiou" style="display:none;"  class="part_form">
              <div class="part_title"><span class="fr">∨</span>配偶信息</div>
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
                    <p class="title"><span class="xing">*</span>户口性质</p>
                    <p class="select"><asp:DropDownList ID="ddlRegisterTypeB" runat="server">
                                    <asp:ListItem>农业</asp:ListItem>
                                    <asp:ListItem>非农业</asp:ListItem>
                                    <asp:ListItem>其他</asp:ListItem>       
                                       </asp:DropDownList></p>
                  </li> 
                  <li>
                    <p class="title"><span class="xing">*</span>身份证号</p>
                    <p class="text"><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18" /></p>
                  </li>
                  <li>
                    <p class="title"><span class="xing">*</span>联系电话</p>
                    <p class="text"><asp:TextBox ID="txtContactTelB" runat="server" EnableViewState="False" MaxLength="12"/></p>
                  </li>
                   <li>
                    <p class="title"><span class="xing">*</span>婚姻状况</p>
                    <p class="select"><asp:DropDownList ID="ddlMarryType" runat="server">
                                            <asp:ListItem>未婚</asp:ListItem>                                
                                            <asp:ListItem>初婚</asp:ListItem>
                                            <asp:ListItem>再婚</asp:ListItem>
                                            <asp:ListItem>离婚</asp:ListItem>
                                            <asp:ListItem>复婚</asp:ListItem> 
                                            <asp:ListItem>丧偶</asp:ListItem>
                                        </asp:DropDownList></p>
                  </li>
                   <li>
                    <p class="title"><span class="xing">*</span>户籍地址</p>
                    <p class="select"><uc3:ucAreaSel ID="UcAreaSelRegB" runat="server" /></p>
                  </li>
                  <li>
                    <p class="title"><span class="xing">*</span>居住地址</p>
                    <p class="select"><uc3:ucAreaSel ID="UcAreaSelCurB" runat="server" /><input type="button" name="button" value="同户籍地" class="button" onclick="AreaCodeTB('UcAreaSelRegB_txtSelAreaCode','UcAreaSelRegB_txtSelectArea','UcAreaSelCurB_txtSelAreaCode','UcAreaSelCurB_txtSelectArea');"/></p>
                  </li>
               </ul>
            </div>
            
           <div id="panelChildren" style="display:none;" class="part_form">
              <div class="part_title"><span class="fr">∨</span>生育信息</div>
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
                        <p class="text"><input id="txtChildBirthday1" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday1'),'yyyy-MM-dd')" readonly="readonly"/></p>
                    </li>
	                <li>
	                    <p class="title">子女1政策属性</p>
                        <p class="select"><asp:DropDownList ID="ddlChildPolicy1" runat="server">
                                           <asp:ListItem>政策内</asp:ListItem>
                                           <asp:ListItem>政策外</asp:ListItem>
                                      </asp:DropDownList></p>
                    </li>    
                    <li>
	                    <p class="title">子女1身份证号</p>
                        <p class="text"><asp:TextBox ID="txtChildCardID1" runat="server" EnableViewState="False" MaxLength="25"/></p>
                    </li>
	                <li>
	                    <p class="title">子女1是否亲生</p>
                        <p class="select"><asp:DropDownList ID="ddlChildSource1" runat="server">
                                       <asp:ListItem>本人及配偶亲生</asp:ListItem>
                                       <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>
                                       <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>
                                       <asp:ListItem>非本人亲生非配偶亲生</asp:ListItem>
                                        </asp:DropDownList></p>
                    </li> 
	                <li>
	                    <p class="title">子女1社会抚养费征收情况</p>
                        <p class="select"><asp:DropDownList ID="ddlMemos1" runat="server">
                           <asp:ListItem>无</asp:ListItem>
                           <asp:ListItem>已征收结案</asp:ListItem>
                           <asp:ListItem>已征收未结案</asp:ListItem>
                           <asp:ListItem>未征收未结案</asp:ListItem>
                        </asp:DropDownList></p>
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
                        <p class="text"><input id="txtChildBirthday2" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday2'),'yyyy-MM-dd')" readonly="readonly"/></p>
                    </li>
	                <li>
	                    <p class="title">子女2政策属性</p>
                        <p class="select"><asp:DropDownList ID="ddlChildPolicy2" runat="server">
                                           <asp:ListItem>政策内</asp:ListItem>
                                           <asp:ListItem>政策外</asp:ListItem>
                                      </asp:DropDownList></p>
                    </li>    
                    <li>
	                    <p class="title">子女2身份证号</p>
                        <p class="text"><asp:TextBox ID="txtChildCardID2" runat="server" EnableViewState="False" MaxLength="25"/></p>
                    </li>
	                <li>
	                    <p class="title">子女2是否亲生</p>
                        <p class="select"><asp:DropDownList ID="ddlChildSource2" runat="server">
                                       <asp:ListItem>本人及配偶亲生</asp:ListItem>
                                       <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>
                                       <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>
                                       <asp:ListItem>非本人亲生非配偶亲生</asp:ListItem>
                                        </asp:DropDownList></p>
                    </li> 
	                <li>
	                    <p class="title">子女2社会抚养费征收情况</p>
                        <p class="select"><asp:DropDownList ID="ddlMemos2" runat="server">
                           <asp:ListItem>无</asp:ListItem>
                           <asp:ListItem>已征收结案</asp:ListItem>
                           <asp:ListItem>已征收未结案</asp:ListItem>
                           <asp:ListItem>未征收未结案</asp:ListItem>
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
                        <p class="text"><input id="txtChildBirthday3" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday3'),'yyyy-MM-dd')" readonly="readonly"/></p>
                    </li>
	                <li>
	                    <p class="title">子女3政策属性</p>
                        <p class="select"><asp:DropDownList ID="ddlChildPolicy3" runat="server">
                                           <asp:ListItem>政策内</asp:ListItem>
                                           <asp:ListItem>政策外</asp:ListItem>
                                      </asp:DropDownList></p>
                    </li>    
                    <li>
	                    <p class="title">子女3身份证号</p>
                        <p class="text"><asp:TextBox ID="txtChildCardID3" runat="server" EnableViewState="False" MaxLength="25"/></p>
                    </li>
	                <li>
	                    <p class="title">子女3是否亲生</p>
                        <p class="select"><asp:DropDownList ID="ddlChildSource3" runat="server">
                                       <asp:ListItem>本人及配偶亲生</asp:ListItem>
                                       <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>
                                       <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>
                                       <asp:ListItem>非本人亲生非配偶亲生</asp:ListItem>
                                        </asp:DropDownList></p>
                    </li> 
	                <li>
	                    <p class="title">子女3社会抚养费征收情况</p>
                        <p class="select"><asp:DropDownList ID="ddlMemos3" runat="server">
                           <asp:ListItem>无</asp:ListItem>
                           <asp:ListItem>已征收结案</asp:ListItem>
                           <asp:ListItem>已征收未结案</asp:ListItem>
                           <asp:ListItem>未征收未结案</asp:ListItem>
                        </asp:DropDownList></p>
                    </li>
                  </ul>      
                </div>
        
     	    <div class="part_form">
              <div class="part_title"><span class="fr">∨</span>其他情况</div>
              <ul>
                <li>
                  <p class="text"><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="100" Text=""/></p>
                </li>	
              </ul>
            </div>
     <div class="part_form">
      <div class="part_title"><span class="fr">∨</span>预办理地点<span class="ps">请慎重选择办理地点，确定后不可变更</span></div>
      <ul>
        <li>
            <p class="title"><span class="xing">*</span>预办理地点</p>
            <p class="select"><uc4:ucAreaSelXian ID="UcAreaSelXian1" runat="server" /></p>
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
<script>
 window.onload=function(){
 ShowBirth0101();ShowMarry0110();
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
