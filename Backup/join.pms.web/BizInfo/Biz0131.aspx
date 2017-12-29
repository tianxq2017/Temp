<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Biz0131.aspx.cs" Inherits="join.pms.web.BizInfo.Biz0131" %>
<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc3" %>
<%@ Register Src="/userctrl/ucAreaSelXian.ascx" TagName="ucAreaSelXian" TagPrefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>婚前医学健康检查证明</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
    <script src="/Scripts/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jsdate.js" type="text/javascript"></script>
    <script src="/includes/dataGrid.js" type="text/javascript"></script>
<script type="text/javascript" language="JavaScript1.2">
    //读卡信息读取
    function ReadCardA_onclick()
    {
        var open;
        var read;
        var csex;
        //open=rdcard.openport();   //打开机具 rdcard.readcard()
        read=rdcard.readcard();   //开始读卡
        if(read==0)
        {
            csex = rdcard.Sex;
            if(csex==1){
                //姓名A
                document.getElementById('<%=txtPersonNameA.ClientID%>').value = rdcard.NameS;
                //身份证号A
                document.getElementById('<%=txtPersonCidA.ClientID%>').value = rdcard.CardNo;
                //获取全员数据 
                //GetQykMsg(rdcard.CardNo,document.getElementById("qykInfo"));
                GetPersonsInfo(rdcard.CardNo,'0131A','1');
            }
            else{
                alert("请放置男方身份证，然后再刷卡！");
            }
            //户籍地址
            //document.getElementById('UcAreaSelRegA_txtSelectArea').value = rdcard.Address;
            //民族
            //document.getElementById('txtNationsB').value = rdcard.NationL;
            rdcard.endread();
        }
        //rdcard.closeport();
    }
    
    //读取女方身份信息
    function ReadCardB_onclick()
    {
        var open;
        var read;
        var csex;
        //open=rdcard.openport();   //打开机具
        read=rdcard.readcard();   //开始读卡
        if(read==0)
        {
            csex = rdcard.Sex;
            if(csex!=1){
                //姓名A
                document.getElementById('<%=txtPersonNameB.ClientID%>').value = rdcard.NameS;
                //身份证号A
                document.getElementById('<%=txtPersonCidB.ClientID%>').value = rdcard.CardNo;
                //获取全员数据 
                //GetQykMsg(rdcard.CardNo,document.getElementById("qykInfo"));
                GetPersonsInfo(rdcard.CardNo,'0131B','1');
            }
            else{
                alert("请放置女方身份证，然后再刷卡！");
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
	        <p class="form_title"><b>本人信息</b></p>
		    <div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
   <tr>
    <th><span class="xing">*</span>身份证号：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidA" runat="server" EnableViewState="False" MaxLength="18" /></span></p>
    <input type="button" name="btnCardA" value="刷身份证" class="button button2" onclick="ReadCardA_onclick();"/>
    <input type="button" name="button" value="自动获取资料" class="button" onclick="GetPersonsInfo(document.getElementById('txtPersonCidA').value,'0131A','1');"/>
    </td>
  </tr>
  <tr>
    <th><span class="xing">*</span>姓名：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameA" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
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
  <tr>
    <th><span class="xing">*</span>婚姻状况：</th>
    <td class="select"><asp:DropDownList ID="ddlMarryTypeA" runat="server" onchange="ShowMarry0110();">
                            <asp:ListItem>初婚</asp:ListItem>
                            <asp:ListItem>再婚</asp:ListItem>
                            <asp:ListItem>复婚</asp:ListItem>
                            <asp:ListItem>离婚</asp:ListItem>
                            <asp:ListItem>丧偶</asp:ListItem>
                            <asp:ListItem>未婚</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>
  <tr id="divMarryChangDateA">
    <th><span class="xing">*</span>婚姻变动日期：</th>
    <td class="text"><p><span><input id="txtFileds34" runat="server"  onclick="SelectDate(document.getElementById('txtFileds34'),'yyyy-MM-dd')"   /></span></p></td>
  </tr>
   <tr>
    <th><span class="xing">*</span>户籍地址：</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelRegA" runat="server" /></td>
  </tr>
   <tr>
    <th><span class="xing">*</span>居住地址：</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurA" runat="server" /><input type="button" name="button" value="同户籍地" class="button" onclick="AreaCodeTB('UcAreaSelRegA_txtSelAreaCode','UcAreaSelRegA_txtSelectArea','UcAreaSelRegA_txtArea','UcAreaSelCurA_txtSelAreaCode','UcAreaSelCurA_txtSelectArea','UcAreaSelCurA_txtArea');"/></td>
  </tr> 
  <tr>
    <th>工作单位：</th>
    <td class="text"><p><span><asp:TextBox ID="txtWorkUnitA" runat="server" EnableViewState="False" MaxLength="25"/></span></p><b class="ps">无单位请填写“无”</b></td>
  </tr>  
  <tr style="display:none">
    <th>电子邮箱：</th>
    <td class="text"><p><span><asp:TextBox ID="txtMail" runat="server" EnableViewState="False" Width="300"/> </span></p></td>
  </tr>
    <tr style="display:none">
    <th><span class="xing">*</span>已生育子女数：</th>
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
	        <p class="form_title"><b>对方信息</b></p>
		    <div class="form_table">
		  <table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>身份证号：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonCidB" runat="server" EnableViewState="False" MaxLength="18"/></span></p>
    <input type="button" name="btnCardB" value="刷身份证" class="button button2" onclick="ReadCardB_onclick();"/>
    <input type="button" name="button" value="自动获取资料" class="button" onclick="GetPersonsInfo(document.getElementById('txtPersonCidB').value,'0131B','1');"/>
    </td>
  </tr>
  <tr>
    <th><span class="xing">*</span>姓名：</th>
    <td class="text"><p><span><asp:TextBox ID="txtPersonNameB" runat="server" EnableViewState="False" MaxLength="25"/></span></p></td>
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
   <tr>
    <th><span class="xing">*</span>婚姻状况：</th>
    <td class="select"><asp:DropDownList ID="ddlMarryType" runat="server">
                            <asp:ListItem>初婚</asp:ListItem>
                            <asp:ListItem>再婚</asp:ListItem>
                        </asp:DropDownList></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>婚姻变动日期：</th>
    <td class="text"><p><span><input id="txtFileds14" runat="server"  onclick="SelectDate(document.getElementById('txtFileds14'),'yyyy-MM-dd')"   /></span></p></td>
  </tr>
   <tr>
    <th><span class="xing">*</span>户籍地址：</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelRegB" runat="server" /></td>
  </tr>
  <tr>
    <th><span class="xing">*</span>居住地址：</th>
    <td class="select select_auto"><uc3:ucAreaSel ID="UcAreaSelCurB" runat="server" /><input type="button" name="button" value="同户籍地" class="button" onclick="AreaCodeTB('UcAreaSelRegB_txtSelAreaCode','UcAreaSelRegB_txtSelectArea','UcAreaSelRegB_txtArea','UcAreaSelCurB_txtSelAreaCode','UcAreaSelCurB_txtSelectArea','UcAreaSelCurB_txtArea');"/></td>
  </tr> 
  <tr>
    <th>工作单位：</th>
    <td class="text"><p><span><asp:TextBox ID="txtWorkUnitB" runat="server" EnableViewState="False" MaxLength="25"/></span></p><b class="ps">无单位请填写“无”</b></td>
  </tr>  
</table>
		    </div>
	    </div>
              	
        <div id="panelChildren" class="form_a" style="display:none"><!--style="display:none;" -->
          <p class="form_title"><b>生育信息</b></p>
          <div class="form_table">
          <table width="0" border="0" cellspacing="0" cellpadding="0" class="tdcolumns">	        
            <tr><th><span class="xing">*</span>姓名</th><th><span class="xing">*</span>性别</th><th><span class="xing">*</span>出生日期</th><th>政策属性</th><th>身份证号</th><th>是否亲生</th><th>社会抚养费征收情况</th></tr>
            <tr id="tableChild1">
                <td class="text"><p><span><asp:TextBox ID="txtChildName1" runat="server" EnableViewState="False" MaxLength="25" Width="50"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSex1" runat="server">
                       <asp:ListItem>男</asp:ListItem>
                       <asp:ListItem>女</asp:ListItem>           
                  </asp:DropDownList></td>
                <td class="text"><p><span><input id="txtChildBirthday1" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday1'),'yyyy-MM-dd')"   style="width:80px;"/></span></p></td> 
                <td class="select"><asp:DropDownList ID="ddlChildPolicy1" runat="server">
                       <asp:ListItem>政策内</asp:ListItem>
                       <asp:ListItem>政策外</asp:ListItem>
                  </asp:DropDownList></td>
                <td class="text"><p><span><asp:TextBox ID="txtChildCardID1" runat="server" EnableViewState="False" MaxLength="25" Width="160"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSource1" runat="server">
                       <asp:ListItem>本人及配偶亲生</asp:ListItem>
                       <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>
                       <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>
                       <asp:ListItem>非本人亲生非配偶亲生</asp:ListItem>
                  </asp:DropDownList></td>
                 <td class="select"><asp:DropDownList ID="ddlMemos1" runat="server">
                       <asp:ListItem>无</asp:ListItem>
                       <asp:ListItem>已征收结案</asp:ListItem>
                       <asp:ListItem>已征收未结案</asp:ListItem>
                       <asp:ListItem>未征收未结案</asp:ListItem>
                  </asp:DropDownList></td>
            </tr> 
            <tr id="tableChild2"><!--style="display:none;" -->
                <td class="text"><p><span><asp:TextBox ID="txtChildName2" runat="server" EnableViewState="False" MaxLength="25" Width="50"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSex2" runat="server">
                                       <asp:ListItem>男</asp:ListItem>
                                       <asp:ListItem>女</asp:ListItem>           
                                  </asp:DropDownList></td>
                <td class="text"><p><span><input id="txtChildBirthday2" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday2'),'yyyy-MM-dd')"   style="width:80px;"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildPolicy2" runat="server">
                                       <asp:ListItem>政策内</asp:ListItem>
                                       <asp:ListItem>政策外</asp:ListItem>
                                  </asp:DropDownList></td>
                <td class="text"><p><span><asp:TextBox ID="txtChildCardID2" runat="server" EnableViewState="False" MaxLength="25" Width="160"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSource2" runat="server">
                       <asp:ListItem>本人及配偶亲生</asp:ListItem>
                       <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>
                       <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>
                       <asp:ListItem>非本人亲生非配偶亲生</asp:ListItem>
                  </asp:DropDownList></td>
                 <td class="select"><asp:DropDownList ID="ddlMemos2" runat="server">
                       <asp:ListItem>无</asp:ListItem>
                       <asp:ListItem>已征收结案</asp:ListItem>
                       <asp:ListItem>已征收未结案</asp:ListItem>
                       <asp:ListItem>未征收未结案</asp:ListItem>
                  </asp:DropDownList></td>             
            </tr>
            <tr id="tableChild3"><!--style="display:none;" -->
                <td class="text"><p><span><asp:TextBox ID="txtChildName3" runat="server" EnableViewState="False" MaxLength="25" Width="50"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSex3" runat="server">
                       <asp:ListItem>男</asp:ListItem>
                       <asp:ListItem>女</asp:ListItem>           
                  </asp:DropDownList></td>
                <td class="text"><p><span><input id="txtChildBirthday3" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday3'),'yyyy-MM-dd')"   style="width:80px;"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildPolicy3" runat="server">
                                       <asp:ListItem>政策内</asp:ListItem>
                                       <asp:ListItem>政策外</asp:ListItem>
                                  </asp:DropDownList></td>
                <td class="text"><p><span><asp:TextBox ID="txtChildCardID3" runat="server" EnableViewState="False" MaxLength="25" Width="160"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSource3" runat="server">
                       <asp:ListItem>本人及配偶亲生</asp:ListItem>
                       <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>
                       <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>
                       <asp:ListItem>非本人亲生非配偶亲生</asp:ListItem>
                  </asp:DropDownList></td>
                 <td class="select"><asp:DropDownList ID="ddlMemos3" runat="server">
                       <asp:ListItem>无</asp:ListItem>
                       <asp:ListItem>已征收结案</asp:ListItem>
                       <asp:ListItem>已征收未结案</asp:ListItem>
                       <asp:ListItem>未征收未结案</asp:ListItem>
                  </asp:DropDownList></td>            
            </tr> 
            <tr id="tableChild4"><!--style="display:none;" -->
                <td class="text"><p><span><asp:TextBox ID="txtChildName4" runat="server" EnableViewState="False" MaxLength="25" Width="50"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSex4" runat="server">
                       <asp:ListItem>男</asp:ListItem>
                       <asp:ListItem>女</asp:ListItem>           
                  </asp:DropDownList></td>
                <td class="text"><p><span><input id="txtChildBirthday4" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday4'),'yyyy-MM-dd')"   style="width:80px;"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildPolicy4" runat="server">
                                       <asp:ListItem>政策内</asp:ListItem>
                                       <asp:ListItem>政策外</asp:ListItem>
                                  </asp:DropDownList></td>
                <td class="text"><p><span><asp:TextBox ID="txtChildCardID4" runat="server" EnableViewState="False" MaxLength="25" Width="160"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSource4" runat="server">
                       <asp:ListItem>本人及配偶亲生</asp:ListItem>
                       <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>
                       <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>
                       <asp:ListItem>非本人亲生非配偶亲生</asp:ListItem>
                  </asp:DropDownList></td>
                 <td class="select"><asp:DropDownList ID="ddlMemos4" runat="server">
                       <asp:ListItem>无</asp:ListItem>
                       <asp:ListItem>已征收结案</asp:ListItem>
                       <asp:ListItem>已征收未结案</asp:ListItem>
                       <asp:ListItem>未征收未结案</asp:ListItem>
                  </asp:DropDownList></td>            
            </tr> 
            <tr id="tableChild5"><!--style="display:none;" -->
                <td class="text"><p><span><asp:TextBox ID="txtChildName5" runat="server" EnableViewState="False" MaxLength="25" Width="50"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSex5" runat="server">
                       <asp:ListItem>男</asp:ListItem>
                       <asp:ListItem>女</asp:ListItem>           
                  </asp:DropDownList></td>
                <td class="text"><p><span><input id="txtChildBirthday5" runat="server"  onclick="SelectDate(document.getElementById('txtChildBirthday5'),'yyyy-MM-dd')"   style="width:80px;"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildPolicy5" runat="server">
                                       <asp:ListItem>政策内</asp:ListItem>
                                       <asp:ListItem>政策外</asp:ListItem>
                                  </asp:DropDownList></td>
                <td class="text"><p><span><asp:TextBox ID="txtChildCardID5" runat="server" EnableViewState="False" MaxLength="25" Width="160"/></span></p></td>
                <td class="select"><asp:DropDownList ID="ddlChildSource5" runat="server">
                       <asp:ListItem>本人及配偶亲生</asp:ListItem>
                       <asp:ListItem>本人亲生非配偶亲生</asp:ListItem>
                       <asp:ListItem>非本人亲生配偶亲生</asp:ListItem>
                       <asp:ListItem>非本人亲生非配偶亲生</asp:ListItem>
                  </asp:DropDownList></td>
                 <td class="select"><asp:DropDownList ID="ddlMemos5" runat="server">
                       <asp:ListItem>无</asp:ListItem>
                       <asp:ListItem>已征收结案</asp:ListItem>
                       <asp:ListItem>已征收未结案</asp:ListItem>
                       <asp:ListItem>未征收未结案</asp:ListItem>
                  </asp:DropDownList></td>            
            </tr>                  
          </table>         
            </div>
        </div>

        <div class="form_a">
          <p class="form_title"><b>其他情况</b></p>
          <div class="form_table">
            <table width="0" border="0" cellspacing="0" cellpadding="0">        
              <tr>
                <th>其他情况：</th>
                <td class="text"><p><span><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="100" Width="600px"  Text =""/></span></p></td>
              </tr>              
              </table>
          </div></div>


<div id="panelHY" class="form_a">
   <p class="form_title"><b>检查信息</b></p>
   <div class="form_table">
   <table width="0" border="0" cellspacing="0" cellpadding="0">
   <tr>
        <th>
            <span class="xing">*</span>检查时间：</th>
        <td class="text">
            <p><span><input id="txtFileds22" runat="server"  onclick="SelectDate(document.getElementById('txtFileds22'),'yyyy-MM-dd')"   /></span></p></td>
    </tr>
   <tr>
        <th>
            <span class="xing">*</span>直系、三代内旁系血亲关系：</th>
    <td class="select">
        <asp:DropDownList ID="txtFileds23" runat="server">
                                       <asp:ListItem Value="0">无</asp:ListItem>
                                       <asp:ListItem Value="1">有</asp:ListItem>
                                  </asp:DropDownList></td>
    </tr>
  <tr>
    <th><span class="xing">*</span>医学意见：</th>
    <td class="select">
        <asp:DropDownList ID="txtFileds24" runat="server">
                                       <asp:ListItem Value="">请选择</asp:ListItem>
                                       <asp:ListItem Value="1">议不宜结婚</asp:ListItem>
                                       <asp:ListItem Value="2">建议不宜生育</asp:ListItem>
                                       <asp:ListItem Value="3">建议暂缓结婚</asp:ListItem>   
                                       <asp:ListItem Value="4">未发现医学上不宜结婚的情形</asp:ListItem>  
                                       <asp:ListItem Value="5">建议采取医学措施，尊重受检者意愿</asp:ListItem>       
                                  </asp:DropDownList></td>
  </tr>
  <tr>
    <th>婚前医学检查结果：</th>
    <td class="text"><p><span><asp:TextBox ID="txtFileds25" runat="server" EnableViewState="False" MaxLength="100" Width="600px"  Text =""/></span></p></td>
  </tr> 
   </table></div></div>
         
         <div class="form_a" style=" display:none">
            <p class="form_title"><b>预领证地点</b></p>
		    <div class="form_table">
                <table width="0" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                       <th><span class="xing">*</span>预领证地点：</th>
                       <td class="select"><uc4:ucAreaSelXian ID="UcAreaSelXian1" runat="server" /> <b class="ps">请慎重选择领证地点，确定后不可变更</b></td>
                    </tr>
                </table>
            </div>    
	    </div>
 	   	<div class="form_a">
            <p class="form_title"><b>联系方式</b></p>
		    <div class="form_table">
                <table width="0" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th><span class="xing">*</span>联系电话：</th>
    <td class="text"><p><span><asp:TextBox ID="txtContactTelA" runat="server" EnableViewState="False" MaxLength="12"/></span></p><b class="ps">必须填写本人的手机号码，否则将无法收到短信提示</b></td>
  </tr> 
                </table>
            </div>    
	    </div>
	  </div>

<div class="check" style=" display:none"><asp:CheckBox ID="cbOk" runat="server" Checked /> 承诺：本人保证以上情况及提供的相关材料属实。如有不实，愿意承担由此引起的相应法律责任，并将记入国家诚信系统。</div>	
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
