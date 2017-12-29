<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03011601.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03011601" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>年度陕西省计划生育家庭情况统计表（城镇）一填报</title>
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
    <p style="font-size:18px; text-align:center; font-weight:bold"><input id="txt_RptTime" readonly="readonly"  size="4" onclick="JTC.setday({format:'yyyy年', readOnly:true})" runat="server" />年度陕西省计划生育家庭情况统计表（城镇）</p>
    <p><%=str_AreaName%></p>    
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="4">单位</td>
            <td colspan="9">一孩户</td>
            <td colspan="4">放弃二胎生育指标户</td>
            <td rowspan="4">操作</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">合计</td>
            <td colspan="4">16岁以下（含16岁）</td>
            <td colspan="4">16岁以上</td>
            <td colspan="2">累计户数</td>
            <td colspan="2">上年度</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td colspan="2">独男户</td>
            <td colspan="2">独女户</td>
            <td colspan="2">独男户</td>
            <td colspan="2">独女户</td>
            <td rowspan="2">总户数</td>
            <td rowspan="2">其中享受奖励户数</td>
            <td rowspan="2">总户数</td>
            <td rowspan="2">其中享受奖励户数</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>小计</td>
            <td>其中领证</td>
            <td>小计</td>
            <td>其中领证</td>
            <td>小计</td>
            <td>其中领证</td>
            <td>小计</td>
            <td>其中领证</td>
          </tr>
          <tr style="text-align:center;">
            <td>全镇合计</td>
            <%for (int i = 0; i < arrNum.Length; i++)
              { %>
            <td><%=arrNum[i] %></td>
            <%} %>
            <td>--</td>
          </tr>
        </thead>
    <asp:Repeater ID="rep_Data" runat="server" OnItemCommand="rep_Data_ItemCommand">
      <ItemTemplate>
        <tbody>
            <tr style="text-align:center;">
                <td><%# Eval("AreaName")%></td>
                <td><%# Eval("Fileds01")%></td>
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
               <td>
                    <asp:LinkButton ID="lbt_Update" runat="server" CommandName="Update" CommandArgument='<%#Eval("CommID") %>' Text="编辑" /> | 
                    <asp:LinkButton ID="lbt_Delete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("CommID") %>' Text="删除" />
               </td>
            </tr>           
          </tbody>
    </ItemTemplate>  
    </asp:Repeater>
    </table>
    <p>负责人：<asp:TextBox ID="txt_SldHeader" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;填表人：<asp:TextBox ID="txt_SldLeader" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;填报日期：<input id="txt_OprateDate" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" /></p>
</td></tr></table>
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellspacing="3" cellpadding="1" class="zhengwen">
   <tr class="zhengwen">
    <td colspan="4" bgcolor="#cccccc">一孩户</td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC" width="260">单位：</td>
    <td width="300"><asp:Label ID="lbl_DataAreaSel" runat="server"></asp:Label>&nbsp;<asp:DropDownList ID="dr_DataAreaSel" runat="server" AutoPostBack="true"></asp:DropDownList></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">合计：</td>
    <td><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" onBlur="get_Val('txtFileds01','txtFileds02,txtFileds04,txtFileds06,txtFileds08');"/></td>
    <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds01%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16岁以下(含16岁))独男户-小计：</td>
    <td><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px" onBlur="get_Val('txtFileds02','txtFileds03');" Text="0"/></td>
    <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds02%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16岁以下(含16岁))独男户-其中领证：</td>
    <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
    <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds03%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16岁以下(含16岁))独女户-小计：</td>
    <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px" onBlur="get_Val('txtFileds04','txtFileds05');" Text="0"/></td>
    <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds04%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16岁以下(含16岁))独女户-其中领证：</td>
    <td><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
    <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds05%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16岁以上)独男户-小计：</td>
    <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px" onBlur="get_Val('txtFileds06','txtFileds07');" Text="0"/></td>
    <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds06%></span></td>
 </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16岁以上)独男户-其中领证：</td>
    <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
    <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds07%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16岁以上)独女户-小计：</td>
    <td><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px" onBlur="get_Val('txtFileds08','txtFileds09');" Text="0"/></td>
    <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds08%></span></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">(16岁以上)独女户-其中领证：</td>
    <td><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
    <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds09%></span></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="4" bgcolor="#cccccc">放弃二胎生育指标户</td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">累计户数-总户数：</td>
    <td><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px" onBlur="get_Val('txtFileds10','txtFileds11');" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">累计户数-其中享受奖励户数：</td>
    <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">2012年-总户数：</td>
    <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px" onBlur="get_Val('txtFileds12','txtFileds13');" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">2012年-其中享受奖励户数：</td>
    <td><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
</table>

    <%=js_value %>
    <!-- LFileds_type 与 LFileds_txt成对-->
    <!-- LFileds_type判断值类型  0，字符不判断；1，数字或小数；-->
    <asp:Label ID="LFileds_type" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    <!-- LFileds_txt判断值类型有误时提示标题-->
    <asp:Label ID="LFileds_txt" runat="server" Text="合计,(16岁以下(含16岁))独男户-小计,(16岁以下(含16岁))独男户-其中领证,(16岁以下(含16岁))独女户-小计,(16岁以下(含16岁))独女户-其中领证,(16岁以上)独男户-小计,(16岁以上)独男户-其中领证,(16岁以上)独女户-小计,(16岁以上)独女户-其中领证,累计户数-总户数,累计户数-其中享受奖励户数,2012年-总户数,2012年-其中享受奖励户数" style="display:none"></asp:Label>
    
</td></tr></table>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·"  CssClass="submit6" OnClick="btnAdd_Click"></asp:Button>
<asp:HiddenField ID="hd_IsUp" runat="server"/>
<asp:HiddenField ID="hd_upId" runat="server"/>
<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>

</div>
    </form>
</body>
</html>
