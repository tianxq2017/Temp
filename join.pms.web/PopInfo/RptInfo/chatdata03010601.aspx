<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03010601.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03010601" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>出生及政策符合情况季报表一填报</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/index.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" src="/includes/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
    <script language="javascript" src="/Scripts/datectrl.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列信息后；点击“提交”按钮（次月5日以前将上月情况提交）……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">

<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<br/>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
    <p style="font-size:18px; text-align:center; font-weight:bold">
    <asp:DropDownList runat="server" ID="txt_RptTime" >
    </asp:DropDownList>出生及政策符合情况统计表</p>
    <p><%=str_AreaName%></p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
        <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">单位</td>
            <td rowspan="3">初期总人口</td>
            <td rowspan="3">期末总人口</td>
            <td rowspan="3">出生总人口</td>
            <td rowspan="3">死亡人口</td>
            <td colspan="16">本省户籍人数</td>
            <td colspan="16">非本省户籍人数</td>
            <td rowspan="3">操作</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td colspan="4">总出生人数</td>
            <td colspan="4">一孩出生人数</td>
            <td colspan="4">二孩出生人数</td>
            <td colspan="4">多孩出生人数</td>
            <td colspan="4">总出生人数</td>
            <td colspan="4">一孩出生人数</td>
            <td colspan="4">二孩出生人数</td>
            <td colspan="4">多孩出生人数</td>
          </tr>
          
          <tr style="text-align:center; background-color:#cccccc">
            <td>小计</td>
            <td>男</td>
            <td>女</td>
            <td>符合政策</td>
            <td>小计</td>
            <td>男</td>
            <td>女</td>
            <td>符合政策</td>
            <td>小计</td>
            <td>男</td>
            <td>女</td>
            <td>符合政策</td>
            <td>小计</td>
            <td>男</td>
            <td>女</td>
            <td>符合政策</td>
            <td>小计</td>
            <td>男</td>
            <td>女</td>
            <td>符合政策</td>
            <td>小计</td>
            <td>男</td>
            <td>女</td>
            <td>符合政策</td>
            <td>小计</td>
            <td>男</td>
            <td>女</td>
            <td>符合政策</td>
            <td>小计</td>
            <td>男</td>
            <td>女</td>
            <td>符合政策</td>
          </tr>
        </thead>
          <tr style="text-align:center;">
            <td>合计</td>
            <%for (int i = 0; i < arrNum.Length; i++)
              { %>
            <td><%=arrNum[i] %></td>
            <%} %>
            <td>--</td>
          </tr>
    <asp:Repeater ID="rep_Data" runat="server">
      <ItemTemplate>
        <tbody>
            <tr style="text-align:center;">
                <td><%# Eval("AreaName")%></td>
                <td> <%#Eval("Fileds01")%></td>
                <td><%# Eval("Fileds02")%></td>
                <td><%# Eval("Fileds03")%></td>
                <td><%# Eval("Fileds04")%></td>
                <td><%# Eval("Fileds05")%></td>
                <td><%# Eval("Fileds06")%></td>
                <td><%# Eval("Fileds07")%></td>
                <td><%# Eval("Fileds08")%></td>
                <td><%# Eval("Fileds09")%></td>
                <td><%# Eval("Fileds10")%></td>
                <td><%# Eval("Fileds11")%></td>
                <td><%# Eval("Fileds12")%></td>
                <td><%# Eval("Fileds13")%></td>
                <td><%# Eval("Fileds14")%></td>
                <td><%# Eval("Fileds15")%></td>
                <td><%# Eval("Fileds16")%></td>
                <td><%# Eval("Fileds17")%></td>
                <td><%# Eval("Fileds18")%></td>
                <td><%# Eval("Fileds19")%></td>
                <td><%# Eval("Fileds20")%></td>
                <td><%# Eval("Fileds21")%></td>
                <td><%# Eval("Fileds22")%></td>
                <td><%# Eval("Fileds23")%></td>
                <td><%# Eval("Fileds24")%></td>
                <td><%# Eval("Fileds25")%></td>
                <td><%# Eval("Fileds26")%></td>
                <td><%# Eval("Fileds27")%></td>
                <td><%# Eval("Fileds28")%></td>
                <td><%# Eval("Fileds29")%></td>
                <td><%# Eval("Fileds30")%></td>
                <td><%# Eval("Fileds31")%></td>
                <td><%# Eval("Fileds32")%></td>
                <td><%# Eval("Fileds33")%></td>
                <td><%# Eval("Fileds34")%></td>
                <td><%# Eval("Fileds35")%></td>
                <td><%# Eval("Fileds36")%></td>
               <td>
                    <asp:LinkButton ID="lbt_Update" runat="server" CommandName="Update" CommandArgument='' Text="编辑" /> | 
                    <asp:LinkButton ID="lbt_Delete" runat="server" CommandName="Delete" CommandArgument='' Text="删除" />
               </td>
            </tr>           
          </tbody>
    </ItemTemplate>  
    </asp:Repeater>
    </table>
     <p>负责人：<asp:TextBox ID="txt_SldHeader" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;填表人：<asp:TextBox ID="txt_SldLeader" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;填报日期：<input id="txt_OprateDate" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" /></p>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
</td></tr></table>
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellspacing="3" cellpadding="1" class="zhengwen"  onmouseover="get_Val('txtFileds03','txtFileds05,txtFileds09,txtFileds13,txtFileds17,txtFileds21,txtFileds25,txtFileds29,txtFileds33');">
          <tr class="zhengwen">
            <td align="right" width="100" bgcolor="#cccccc">单位：</td>
            <td colspan="4" bgcolor="#cccccc"><asp:Label ID="lbl_DataAreaSel" runat="server"></asp:Label>&nbsp;<asp:DropDownList ID="dr_DataAreaSel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dr_DataAreaSel_OnSelectedIndexChanged"></asp:DropDownList></td>
          </tr>
          <tr class="zhengwen">
            <td rowspan="4" align="right" bgcolor="#FFFFCC">&nbsp;</td>
            <td colspan="2" align="right" bgcolor="#FFFFCC">初期总人口：</td>
            <td width="320"><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
            <td>来源：本表上一季末总人口<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds01%></span></td>
          </tr>
          <tr class="zhengwen">
            <td colspan="2" align="right" bgcolor="#FFFFCC">期末总人口：</td>
            <td><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
            <td>来源：信息共享<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds02%></span></td>
          </tr>
          <tr class="zhengwen">
            <td colspan="2" align="right" bgcolor="#FFFFCC">出生总人口：</td>
            <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" ReadOnly="true" /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds03%></span></td>
          </tr>
          <tr class="zhengwen">
            <td colspan="2" align="right" bgcolor="#FFFFCC">死亡人口：</td>
            <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
            <td>来源：信息共享<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds04%></span></td>
          </tr>
          <tr class="zhengwen">
            <td rowspan="16" align="right" bgcolor="#cccccc">本省户籍人数</td>
            <td width="140" rowspan="4" align="right" bgcolor="#cccccc">总出生人数</td>
            <td width="100" align="right" bgcolor="#cccccc">小计：</td>
            <td><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" ReadOnly="true" /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds05%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#cccccc">男：</td>
            <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds05','txtFileds06,txtFileds07');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds06%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#cccccc">女：</td>
            <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds05','txtFileds06,txtFileds07');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds07%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#cccccc">符合政策：</td>
            <td><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
          </tr>
          <tr class="zhengwen">
            <td rowspan="4" align="right" bgcolor="#cccccc">一孩出生人数</td>
            <td align="right" bgcolor="#cccccc">小计：</td>
            <td><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" ReadOnly="true" /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds09%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#cccccc">男：</td>
            <td><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds09','txtFileds10,txtFileds11');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds10%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#cccccc">女：</td>
            <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds09','txtFileds10,txtFileds11');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds11%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#cccccc">符合政策：</td>
            <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
          </tr>
          <tr class="zhengwen">
            <td rowspan="4" align="right" bgcolor="#cccccc">二孩出生人数</td>
            <td align="right" bgcolor="#cccccc">小计：</td>
            <td><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" ReadOnly="true" /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds13%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#cccccc">男：</td>
            <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds13','txtFileds14,txtFileds15');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds14%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#cccccc">女：</td>
            <td><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds13','txtFileds14,txtFileds15');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds15%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#cccccc">符合政策：</td>
            <td><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
          </tr>
          <tr class="zhengwen">
            <td rowspan="4" align="right" bgcolor="#cccccc">多孩出生人数</td>
            <td align="right" bgcolor="#cccccc">小计：</td>
            <td><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" ReadOnly="true" /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds17%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#cccccc">男：</td>
            <td><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds17','txtFileds18,txtFileds19');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds18%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#cccccc">女：</td>
            <td><asp:TextBox ID="txtFileds19" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds17','txtFileds18,txtFileds19');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds19%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#cccccc">符合政策：</td>
            <td><asp:TextBox ID="txtFileds20" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC" rowspan="18">非本省户籍人数</td>
            <td width="140" rowspan="4" align="right" bgcolor="#FFFFCC">总出生人数</td>
            <td align="right" bgcolor="#FFFFCC">小计：</td>
            <td><asp:TextBox ID="txtFileds21" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" ReadOnly="true" /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds21%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">男：</td>
            <td><asp:TextBox ID="txtFileds22" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds21','txtFileds22,txtFileds23');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds22%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">女：</td>
            <td><asp:TextBox ID="txtFileds23" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds21','txtFileds22,txtFileds23');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds23%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">符合政策：</td>
            <td><asp:TextBox ID="txtFileds24" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
          </tr>
          <tr class="zhengwen">
            <td rowspan="4" align="right" bgcolor="#FFFFCC">一孩出生人数</td>
            <td align="right" bgcolor="#FFFFCC">小计：</td>
            <td><asp:TextBox ID="txtFileds25" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" ReadOnly="true" /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds25%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">男：</td>
            <td><asp:TextBox ID="txtFileds26" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds25','txtFileds26,txtFileds27');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds26%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">女：</td>
            <td><asp:TextBox ID="txtFileds27" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds25','txtFileds26,txtFileds27');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds27%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">符合政策：</td>
            <td><asp:TextBox ID="txtFileds28" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
          </tr>
          <tr class="zhengwen">
            <td rowspan="4" align="right" bgcolor="#FFFFCC">二孩出生人数</td>
            <td align="right" bgcolor="#FFFFCC">小计：</td>
            <td><asp:TextBox ID="txtFileds29" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" ReadOnly="true" /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds29%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">男：</td>
            <td><asp:TextBox ID="txtFileds30" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds29','txtFileds30,txtFileds31');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds30%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">女：</td>
            <td><asp:TextBox ID="txtFileds31" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds29','txtFileds30,txtFileds31');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds31%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">符合政策：</td>
            <td><asp:TextBox ID="txtFileds32" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
          </tr>
          <tr class="zhengwen">
            <td rowspan="6" align="right" bgcolor="#FFFFCC">多孩出生人数</td>
            <td align="right" bgcolor="#FFFFCC">小计：</td>
            <td><asp:TextBox ID="txtFileds33" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" ReadOnly="true" /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds33%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">男：</td>
            <td><asp:TextBox ID="txtFileds34" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds33','txtFileds34,txtFileds35');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds34%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">女：</td>
            <td><asp:TextBox ID="txtFileds35" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds33','txtFileds34,txtFileds35');"  /></td>
            <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds35%></span></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">符合政策：</td>
            <td><asp:TextBox ID="txtFileds36" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
          </tr>
    </table>
    <%=js_value %>
    <!-- LFileds_type 与 LFileds_txt成对-->
    <!-- LFileds_type判断值类型  0，字符不判断；1，数字或小数；-->
    <asp:Label ID="LFileds_type" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    <!-- LFileds_txt判断值类型有误时提示标题-->
    <asp:Label ID="LFileds_txt" runat="server" Text="初期总人口,期末总人口,出生总人口,死亡人口,本省总出生人数小计,本省总出生人数男,本省总出生人数女,本省总出生人数符合政策,本省一孩出生人数小计,本省一孩出生人数男,本省一孩出生人数女,本省一孩出生人数符合政策,本省二孩出生人数小计,本省二孩出生人数男,本省二孩出生人数女,本省二孩出生人数符合政策,本省多孩出生人数小计,本省多孩出生人数男,本省多孩出生人数女,本省多孩出生人数符合政策,非本省总出生人数小计,非本省总出生人数男,非本省总出生人数女,非本省总出生人数符合政策,非本省一孩出生人数小计,非本省一孩出生人数男,非本省一孩出生人数女,非本省一孩出生人数符合政策,非本省二孩出生人数小计,非本省二孩出生人数男,非本省二孩出生人数女,非本省二孩出生人数符合政策,非本省多孩出生人数小计,非本省多孩出生人数男,非本省多孩出生人数女,非本省多孩出生人数符合政策" style="display:none"></asp:Label>
    
</td></tr></table>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·"  CssClass="submit6" OnClick="btnAdd_Click"></asp:Button>
<asp:HiddenField ID="hd_IsUp" runat="server"/>
<asp:HiddenField ID="hd_upId" runat="server"/>
<asp:Button ID="btnUp" runat="server" Text="· 上报 ·"  CssClass="submit6" OnClick="btnUp_Click"></asp:Button>
<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>

</div>
    </form>
</body>
</html>
