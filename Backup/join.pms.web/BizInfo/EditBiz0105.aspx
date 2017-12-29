<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditBiz0105.aspx.cs" Inherits="join.pms.web.BizInfo.EditBiz0105" %>

<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc3" %>
<%@ Register Src="/userctrl/ucAreaSelXian.ascx" TagName="ucAreaSelXian" TagPrefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
    <script src="/Scripts/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jsdate.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列信息后；点击“提交”按钮……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtAttribs" id="txtAttribs" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
  <div class="form_bg">	  	
	    <div class="form_a">
	      <p class="form_title"><b>育龄妇女基本信息</b><span class="ps">女方必须在49周岁内才可以申请</span></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>姓名：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameB" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>身份证号：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18" onblur="CheckCID('1','txtPersonCidB');GetPersonsInfo(document.getElementById('txtPersonCidB').value,'0105B','1');"/></span></p><input type="button" name="button" value="自动获取资料" class="button" onclick="GetPersonsInfo(document.getElementById('txtPersonCidB').value,'0105B','1');"/></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>婚姻状况：</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeB" runat="server" onchange="MarryDateTB('ddlMarryTypeA','txtMarryDateA','ddlMarryTypeB','txtMarryDateB');">  
                            <asp:ListItem Selected>初婚</asp:ListItem>
                            <asp:ListItem>再婚</asp:ListItem>
                            <asp:ListItem>离婚</asp:ListItem>
                            <asp:ListItem>复婚</asp:ListItem>  
                            <asp:ListItem>丧偶</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>
    <tr>
      <th><span class="xing">*</span>婚姻时间：</th>
      <td class="text"><p><span><input id="txtMarryDateB" runat="server"  onclick="SelectDate(document.getElementById('txtMarryDateB'),'yyyy-MM-dd')"  onchange="MarryDateTB('ddlMarryTypeB','txtMarryDateB','ddlMarryTypeA','txtMarryDateA');" /></span></p></td>
    </tr>
  <tr>
    <th><span class="xing">*</span>民族：</th>
    <td class="select"><asp:Literal ID="LiteralNationsB" runat="server" EnableViewState="false"></asp:Literal>
    <input type="hidden" name="txtHNationsB" id="txtHNationsB" runat="server" style="display:none;"/></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>户口性质：</th>
    <td class="select"><asp:DropDownList ID="ddlRegisterTypeB" runat="server">
                        <asp:ListItem>农业</asp:ListItem>            
                       </asp:DropDownList></td>
  </tr>    
  </table>	    
		  </div>
	    </div>  
	    <div class="form_a">
	      <p class="form_title"><b>男方基本信息</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>姓名：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameA" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>身份证号：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18" onblur="CheckCID('0','txtPersonCidA');GetPersonsInfo(document.getElementById('txtPersonCidA').value,'0105A','1');"/></span></p><input type="button" name="button" value="自动获取资料" class="button" onclick="GetPersonsInfo(document.getElementById('txtPersonCidA').value,'0105A','1');"/></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>婚姻状况：</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeA" runat="server">
                            <asp:ListItem>初婚</asp:ListItem>
                            <asp:ListItem>再婚</asp:ListItem>
                            <asp:ListItem>离婚</asp:ListItem>
                            <asp:ListItem>复婚</asp:ListItem>  
                            <asp:ListItem>丧偶</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>  
  <tr>
      <th><span class="xing">*</span>婚姻时间：</th>
      <td class="text"><p><span><input id="txtMarryDateA" runat="server"  onclick="SelectDate(document.getElementById('txtMarryDateA'),'yyyy-MM-dd')"   /></span></p></td>
    </tr>
  <tr>
    <th><span class="xing">*</span>民族：</th>
    <td class="select"><asp:Literal ID="LiteralNationsA" runat="server" EnableViewState="false"></asp:Literal>
    <input type="hidden" name="txtHNationsA" id="txtHNationsA" runat="server" style="display:none;"/></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>户口性质：</th>
    <td class="select"><asp:DropDownList ID="ddlRegisterTypeA" runat="server">
                        <asp:ListItem>农业</asp:ListItem>            
                       </asp:DropDownList></td>
  </tr> 
</table>
		  </div>
	    </div>
  <div class="form_a">
	      <p class="form_title"><b>生育子女数</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>按政策可生育子女数：</th>
     <td class="select select_auto"><asp:DropDownList ID="ddlFileds40" runat="server"> 
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                    </asp:DropDownList></td>

  </tr>   
   <tr>
    <th><span class="xing">*</span>生育子女数（含收养）：</th>
     <td class="select select_auto"><asp:DropDownList ID="ddlFileds37" runat="server" onchange="ShowBirth0105();">                                       
                                        <asp:ListItem Value="0">0</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                    </asp:DropDownList><span class="t_you">男</span>
                                    <asp:DropDownList ID="ddlFileds38" runat="server" onchange="ShowBirth0105();">                                       
                                        <asp:ListItem Value="0">0</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                    </asp:DropDownList><span class="t_you">女</span></td>

  </tr> 
</table>
		  </div>
	    </div> 
<div id="panelChildren" style="display:none;" class="form_a">
  <p class="form_title"><b>新生儿信息</b></p>
  <div class="form_table">
  <table width="0" border="0" cellspacing="0" cellpadding="0" class="tdcolumns">	        
    <tr><th><span class="xing">*</span>姓名</th><th><span class="xing">*</span>性别</th><th><span class="xing">*</span>出生年月</th><th><span class="xing">*</span>存活状况</th><th><span class="xing">*</span>是否亲生</th></tr>
    <tr id="tableChild1">
        <td class="text"><p><span><asp:TextBox ID="txtChildName1" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex1" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday1" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday1'),'yyyy-MM-dd')"   /></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSurvivalStatus1" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></td>
        <td class="select"><asp:DropDownList ID="ddlChildSource1" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></td>
    </tr> <input type="hidden" name="txtChildID1" id="txtChildID1" value="" runat="server" style="display:none;"/>
    <tr id="tableChild2">
        <td class="text"><p><span><asp:TextBox ID="txtChildName2" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex2" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday2" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday2'),'yyyy-MM-dd')"   /></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSurvivalStatus2" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></td>
        <td class="select"><asp:DropDownList ID="ddlChildSource2" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></td>
    </tr>  <input type="hidden" name="txtChildID2" id="txtChildID2" value="" runat="server" style="display:none;"/>
    <tr id="tableChild3">
        <td class="text"><p><span><asp:TextBox ID="txtChildName3" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex3" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday3" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday3'),'yyyy-MM-dd')"   /></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSurvivalStatus3" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></td>
        <td class="select"><asp:DropDownList ID="ddlChildSource3" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></td>
    </tr> <input type="hidden" name="txtChildID3" id="txtChildID3" value="" runat="server" style="display:none;"/>
    <tr id="tableChild4">
        <td class="text"><p><span><asp:TextBox ID="txtChildName4" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex4" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday4" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday4'),'yyyy-MM-dd')"   /></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSurvivalStatus4" runat="server">
                               <asp:ListItem>存活</asp:ListItem>
                               <asp:ListItem>死亡</asp:ListItem>                    
                          </asp:DropDownList></td>
        <td class="select"><asp:DropDownList ID="ddlChildSource4" runat="server">
                               <asp:ListItem>本人及配偶亲生</asp:ListItem>
                               <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>    
                               <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>           
                               <asp:ListItem>非本人及配偶亲生</asp:ListItem>                  
                          </asp:DropDownList></td>
    </tr>   <input type="hidden" name="txtChildID4" id="txtChildID4" value="" runat="server" style="display:none;"/>     
  </table>         
    </div>
</div>  

  <div class="form_a">
	      <p class="form_title"><b>联系方式</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
 <tr>
    <th><span class="xing">*</span>户籍地址：</th>
    <td class="text"><p><span><asp:TextBox ID="txtAreaSelRegNameB" runat="server" EnableViewState="False" Width="300"/></span></p>
    <input type="hidden" name="txtAreaSelRegCodeB" id="txtAreaSelRegCodeB" runat="server" style="display:none;"/></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>联系电话：</th>
    <td class="text"><p><span><asp:TextBox ID="txtContactTelB" runat="server" EnableViewState="False" MaxLength="12"/></span></p><b class="ps">必须填写本人的手机号码，否则将无法收到短信提示</b></td>
  </tr> 
</table>
		  </div>
	    </div>
  <div class="form_a">
	      <p class="form_title"><b>避孕状况</b><span class="ps">育龄妇女或其丈夫选填一位即可</span></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>育龄妇女：</th>
     <td class="select"><asp:DropDownList ID="ddlFileds45" runat="server">                                       
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>女扎</asp:ListItem>
                                        <asp:ListItem>放环</asp:ListItem>
                                        <asp:ListItem>皮埋</asp:ListItem>
                                    </asp:DropDownList></td>

  </tr>   
  <tr>
    <th><span class="xing">*</span>育龄妇女丈夫：</th>
     <td class="select"><asp:DropDownList ID="ddlFileds46" runat="server">
                                        <asp:ListItem></asp:ListItem>                                       
                                        <asp:ListItem>男扎</asp:ListItem>
                                    </asp:DropDownList></td>

  </tr> 
  <tr>
    <th><span class="xing">*</span>避孕开始时期：</th>
    <td class="text"><p><span><input id="txtFileds44" runat="server"  onclick="SelectDate(document.getElementById('txtFileds44'),'yyyy-MM-dd')"   /></span></p></td>
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
        <td class="text"><p><span><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="100" Width="600px" Text="符合计划生育“少生快富”工程奖励条件"/></span></p></td>
      </tr>             
      </table>
  </div></div> 
  
  	   	  	<div class="form_a">
            <p class="form_title"><b>预办理地点</b></p>
		    <div class="form_table">
                <table width="0" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                       <th><span class="xing">*</span>预办理地点：</th>
                       <td class="select select_auto"><uc4:ucAreaSelXian ID="UcAreaSelXian1" runat="server" /><b class="ps">请慎重选择办理地点，确定后不可变更</b></td>
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
ShowBirth0105();
}
</script>