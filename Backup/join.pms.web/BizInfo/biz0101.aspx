<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="biz0101.aspx.cs" Inherits="join.pms.web.BizInfo.biz0101" %>
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
          <p class="form_title"><b>母子健康手册证号</b></p>
          <div class="form_table">
            <table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>服务证号：</th>
    <td class="text"><p><span><asp:TextBox ID="txtfwzh" runat="server" EnableViewState="False" MaxLength="20"  onblur="GetFwzInfo(document.getElementById('txtfwzh').value,'0101B','0101A','1');" /></span></p>
    <input type="button" name="button" value="自动获取资料" class="button"  onclick="GetFwzInfo(document.getElementById('txtfwzh').value,'0101B','0101A','1');"/>
    </td>
  </tr>
  </table>
          </div>
          </div>

	    <div class="form_a">
	      <p class="form_title"><b>女方基本信息</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>姓名：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameB" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>证件类别：</th>
    <td class="select"><asp:DropDownList ID="ddlCidTypeB" runat="server" onchange="ShowMarryCid('PersonCidB',this.value); ">
                            <asp:ListItem Value="1">身份证</asp:ListItem>
                            <asp:ListItem Value="2">军管证</asp:ListItem>
                            <asp:ListItem Value="3">护照</asp:ListItem>
                            <asp:ListItem Value="4">其它</asp:ListItem>
                        </asp:DropDownList></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>身份证号：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18"  onblur="CheckCID_1('ddlCidTypeB','1','txtPersonCidB');"/></span></p>
    <div id="PersonCidB" style="display:;">
    <input type="button" name="btnCardB" value="刷身份证" class="button button2" onclick="ReadCardB_onclick();"/>
    <input type="button" name="button" value="自动获取资料" class="button" onclick="GetPersonsInfo(document.getElementById('txtPersonCidB').value,'0101B','1');"/>
    </div>
    </td>
  </tr>
  <tr>
    <th><span class="xing">*</span>婚姻状况：</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeB" runat="server" onchange="ShowMarry0101();MarryDateTB('ddlMarryTypeA','txtMarryDateA','ddlMarryTypeB','txtMarryDateB');">  
                            <asp:ListItem Selected>初婚</asp:ListItem>
                            <asp:ListItem>再婚</asp:ListItem>
                            <asp:ListItem>离婚</asp:ListItem>
                            <asp:ListItem>复婚</asp:ListItem>       
                            <asp:ListItem>丧偶</asp:ListItem>
<asp:ListItem>未婚</asp:ListItem>                         
                            
                        </asp:DropDownList></td>
  </tr>
    <tr id="panelMarry" style="display:none;">
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
                        <asp:ListItem>非农业</asp:ListItem>  
                        <asp:ListItem>其他</asp:ListItem>            
                       </asp:DropDownList></td>
  </tr> 
  <tr style="display:none">
    <th>是否是独生子女：</th>
    <td class="select"><asp:DropDownList ID="ddlFileds16" runat="server">
                        <asp:ListItem Selected></asp:ListItem>  
                        <asp:ListItem>否</asp:ListItem>
                        <asp:ListItem>是</asp:ListItem>            
                       </asp:DropDownList></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>户籍地址：</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelRegB" runat="server" /></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>现居住地址：</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurB" runat="server" /><input type="button" name="button" value="同户籍地" class="button" onclick="AreaCodeTB('UcAreaSelRegB_txtSelAreaCode','UcAreaSelRegB_txtSelectArea','UcAreaSelCurB_txtSelAreaCode','UcAreaSelCurB_txtSelectArea');"/></td>
  </tr>
  <tr>
    <th>工作单位：</th>
    <td class="text"><p><span><asp:TextBox ID="txtWorkUnitB" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>联系电话：</th>
    <td class="text"><p><span><asp:TextBox ID="txtContactTelB" runat="server" EnableViewState="False" MaxLength="12"/></span></p><b class="ps">必须填写本人的手机号码，否则将无法收到短信提示</b></td>
  </tr>  </table>	    
		  </div>
	    </div>  
	    <div id="panelPeiou" style="display:none;" class="form_a">
	      <p class="form_title"><b>男方基本信息</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>姓名：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameA" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>证件类别：</th>
    <td class="select"><asp:DropDownList ID="ddlCidTypeA" runat="server" onchange="ShowMarryCid('PersonCidA',this.value); ">
                            <asp:ListItem Value="1">身份证</asp:ListItem>
                            <asp:ListItem Value="2">军管证</asp:ListItem>
                            <asp:ListItem Value="3">护照</asp:ListItem>
                            <asp:ListItem Value="4">其它</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>身份证号：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18"  onblur="CheckCID_1('ddlCidTypeA','0','txtPersonCidA');"/></span></p>
    <div id="PersonCidA" style="display:;">
    <input type="button" name="btnCardA" value="刷身份证" class="button button2" onclick="ReadCardA_onclick();"/>
    <input type="button" name="button" value="自动获取资料" class="button" onclick="GetPersonsInfo(document.getElementById('txtPersonCidA').value,'0101A','1');"/>
    </div>
    </td>
  </tr> 
  <tr>
    <th>外籍身份证号：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidWA" runat="server" EnableViewState="False" MaxLength="18"/></span></p></td>
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
                        <asp:ListItem>非农业</asp:ListItem>   
                        <asp:ListItem>其他</asp:ListItem>           
                       </asp:DropDownList></td>
  </tr>
  <tr style=" display:none">
    <th>是否是独生子女：</th>
    <td class="select"><asp:DropDownList ID="ddlFileds17" runat="server">
                        <asp:ListItem Selected></asp:ListItem>  
                        <asp:ListItem>是</asp:ListItem>
                        <asp:ListItem>否</asp:ListItem>            
                       </asp:DropDownList></td>
  </tr> 
  <tr>
    <th><span class="xing">*</span>户籍地址：</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelRegA" runat="server" /></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>现居住地址：</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurA" runat="server" /><input type="button" name="button" value="同户籍地" class="button" onclick="AreaCodeTB('UcAreaSelRegA_txtSelAreaCode','UcAreaSelRegA_txtSelectArea','UcAreaSelCurA_txtSelAreaCode','UcAreaSelCurA_txtSelectArea');"/></td>
  </tr>
  <tr>
    <th>工作单位：</th>
    <td class="text"><p><span><asp:TextBox ID="txtWorkUnitA" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
  </tr> 
  <tr>
    <th>联系电话：</th>
    <td class="text"><p><span><asp:TextBox ID="txtContactTelA" runat="server" EnableViewState="False" MaxLength="12"/></span></p></td>
  </tr> 
</table>
		  </div>
	    </div>
  	    
	    <div class="form_a">
	      <p class="form_title"><b>子女信息</b></p>
		  <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
                <th><span class="xing">*</span>子女数：</th>
                <td class="select"><asp:DropDownList ID="ddlBirthNum" runat="server" onchange="ShowBirth0101();">
                                         <asp:ListItem Value="0">0</asp:ListItem>
                                         <asp:ListItem Value="1">1</asp:ListItem>
                                         <asp:ListItem Value="2">2</asp:ListItem>
                                         <asp:ListItem Value="3">3</asp:ListItem>
                                    </asp:DropDownList></td>
            </tr>   
  </table>	    
		  </div>
	    </div>  
<div id="panelChildren" style="display:none;" class="form_a">
  <p class="form_title"><b>新生儿信息</b></p>
  <div class="form_table">
  <table width="0" border="0" cellspacing="0" cellpadding="0" class="tdcolumns">	        
    <tr><th><span class="xing">*</span>姓名</th><th><span class="xing">*</span>性别</th><th><span class="xing">*</span>出生年月</th><th>身份证号或出生医学证明或收养证号</th></tr>
    <tr id="tableChild1">
        <td class="text"><p><span><asp:TextBox ID="txtChildName1" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex1" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday1" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday1'),'yyyy-MM-dd')"  /></span></p></td>
         <td class="text"><p><span><asp:TextBox ID="txtChildCardID1" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
    </tr> 
    <tr id="tableChild2">
        <td class="text"><p><span><asp:TextBox ID="txtChildName2" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex2" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday2" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday2'),'yyyy-MM-dd')"  /></span></p></td>
         <td class="text"><p><span><asp:TextBox ID="txtChildCardID2" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
    </tr>  
    <tr id="tableChild3">
        <td class="text"><p><span><asp:TextBox ID="txtChildName3" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
        <td class="select"><asp:DropDownList ID="ddlChildSex3" runat="server">
                               <asp:ListItem>男</asp:ListItem>
                               <asp:ListItem>女</asp:ListItem>           
                          </asp:DropDownList></td>
        <td class="text"><p><span><input id="txtChildBirthday3" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday3'),'yyyy-MM-dd')"  /></span></p></td>
         <td class="text"><p><span><asp:TextBox ID="txtChildCardID3" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
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
        <td><p><span>符合《内蒙古自治区人口与计划生育条例》规定，政策内生育第一孩。<div style="display:none"><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="100" Width="600px" Text="符合《内蒙古自治区人口与计划生育条例》规定，政策内生育第一孩。"/></div></span></p></td>
      </tr>             
      </table>
  </div></div>  
                                	    <div class="form_a">
            <p class="form_title"><b>预领证地点</b></p>
		    <div class="form_table">
                <table width="0" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                       <th><span class="xing">*</span>预领证地点：</th>
                       <td class="select select_auto"><uc4:ucAreaSelXian ID="UcAreaSelXian1" runat="server" /><b class="ps">请慎重选择领证地点，确定后不可变更</b></td>
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
 ShowMarry0101();ShowBirth0101();
}
</script>