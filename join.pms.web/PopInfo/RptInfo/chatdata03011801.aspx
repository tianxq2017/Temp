<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03011801.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03011801" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>陕西省实施全面两孩政策育龄妇女生育情况统计表</title>
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
    <p style="font-size:18px; text-align:center; font-weight:bold">陕西省实施全面两孩政策育龄妇女生育情况统计表（<asp:DropDownList runat="server" ID="txt_RptTime" >
    </asp:DropDownList>）</p>
    <p><%=str_AreaName%></p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
        <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="4">单位</td>
            <td colspan="3">全面两孩出生情况累计数</td>
            <td colspan="16">本月两孩育龄妇女情况及出生两孩人口数量</td>
            <td rowspan="4">当月新增怀孕两孩人数</td>
            <td rowspan="4">操作</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">总计</td>
            <td rowspan="3">男</td>
            <td rowspan="3">女</td>
            <td colspan="3">孩子出生情况</td>
            <td colspan="3">育龄妇女户口性质</td>
            <td colspan="5">育龄妇女年龄结构</td>
            <td colspan="5">第一个孩子情况</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="2">小计</td>
            <td rowspan="2">男</td>
            <td rowspan="2">女</td>
            <td rowspan="2">小计</td>
            <td rowspan="2">城镇居民</td>
            <td rowspan="2">农村居民</td>
            <td rowspan="2">小计</td>
            <td rowspan="2">8岁-25岁</td>
            <td rowspan="2">26岁-35岁</td>
            <td rowspan="2">36岁-45岁</td>
            <td rowspan="2">46岁以上</td>
            <td colspan="2">性别</td>
            <td colspan="3">年龄分组</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>男</td>
            <td>女</td>
            <td>0-4岁</td>
            <td>5-9岁</td>
            <td>10岁以上</td>
          </tr>
          <tr style="text-align:center;">
            <td>合计</td>
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
                <td><%# Eval("Fileds14")%></td>
                <td><%# Eval("Fileds15")%></td>
                <td><%# Eval("Fileds16")%></td>
                <td><%# Eval("Fileds17")%></td>
                <td><%# Eval("Fileds18")%></td>
                <td><%# Eval("Fileds19")%></td>
                <td><%# Eval("Fileds20")%></td>
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
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
</td></tr></table>
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellspacing="3" cellpadding="1" class="zhengwen">
      <tr  bgcolor="#cccccc" class="zhengwen">
        <td align="right">单位：</td>
        <td colspan="6"><asp:Label ID="lbl_DataAreaSel" runat="server"></asp:Label>&nbsp;<asp:DropDownList ID="dr_DataAreaSel" runat="server"></asp:DropDownList></td>
      </tr>
      <tr class="zhengwen">
        <td colspan="3" rowspan="3" align="right" bgcolor="#FFFFCC">全面两孩出生情况累计数</td>
        <td align="right" bgcolor="#FFFFCC">总计：</td>
        <td width="300"><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
        <td width="260">来源：信息共享<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds01%></span></td>
        <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds01_1%></span></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">男：</td>
        <td><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
        <td>来源：信息共享<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds02%></span></td>
        <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds02_1%></span></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">女：</td>
        <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
        <td>来源：信息共享<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds03%></span></td>
        <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds03_1%></span></td>
      </tr>
      <tr class="zhengwen">
        <td rowspan="16" bgcolor="#FFFFCC">本月两孩育龄妇女情况<br/>及出生两孩人口数量</td>
        <td colspan="2" rowspan="3" align="right" bgcolor="#FFFFCC">孩子出生情况</td>
        <td align="right" bgcolor="#FFFFCC">小计：</td>
        <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
        <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds04%></span></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">男：</td>
        <td><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
        <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds05%></span></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">女：</td>
        <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
        <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds06%></span></td>
      </tr>
      <tr class="zhengwen">
        <td colspan="2" rowspan="3" align="right" bgcolor="#FFFFCC">育龄妇女户口性质</td>
        <td align="right" bgcolor="#FFFFCC">小计：</td>
        <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">城镇居民：</td>
        <td><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">农村居民：</td>
        <td><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
      </tr>
      <tr class="zhengwen">
        <td colspan="2" rowspan="5" align="right" bgcolor="#FFFFCC">育龄妇女年龄结构</td>
        <td align="right" bgcolor="#FFFFCC">小计：</td>
        <td><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">8岁-25岁：</td>
        <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
        <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds11%></span></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">26岁-35岁：</td>
        <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
        <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds12%></span></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">36岁-45岁：</td>
        <td><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
        <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds13%></span></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">46岁以上：</td>
        <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
        <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> <%=lbl_txtFileds14%></span></td>
      </tr>
      <tr class="zhengwen">
        <td rowspan="5" align="right" bgcolor="#FFFFCC">第一个孩子情况</td>
        <td rowspan="2" align="right" bgcolor="#FFFFCC">性别</td>
        <td align="right" bgcolor="#FFFFCC">男：</td>
        <td><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
        <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> 0</span></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">女：</td>
        <td><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
        <td>来源：业务事项<span style=" color:#ff9600; font-weight:bold"> 0</span></td>
      </tr>
      <tr class="zhengwen">
        <td rowspan="3" align="right" bgcolor="#FFFFCC">年龄分组</td>
        <td align="right" bgcolor="#FFFFCC">0-4岁：</td>
        <td><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">5-9岁：</td>
        <td><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">10岁以上：</td>
        <td><asp:TextBox ID="txtFileds19" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
      </tr>
      <tr class="zhengwen">
        <td colspan="4" bgcolor="#cccccc" align="right" style=" width:22%">当月新增怀孕两孩人数：</td>
        <td><asp:TextBox ID="txtFileds20" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
      </tr>
    </table>
<%=js_value %>

    <!-- LFileds_type 与 LFileds_txt成对-->
    <!-- LFileds_type判断值类型  0，字符不判断；1，数字或小数；-->
    <asp:Label ID="LFileds_type" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    <!-- LFileds_txt判断值类型有误时提示标题-->
    <asp:Label ID="LFileds_txt" runat="server" Text="全面二孩出生情况累计数总计,全面二孩出生情况累计数男,全面二孩出生情况累计数女,孩子出生情况小计,孩子出生情况男,孩子出生情况女,育龄妇女户口性质小计,育龄妇女户口性质城镇居民,育龄妇女户口性质农村居民,育龄妇女年龄结构小计,18岁-25岁,26岁-35岁,36岁-45岁,46岁以上,第一个孩子情况男,第一个孩子情况女,0-4岁,5-9岁,10岁以上,当月新增怀孕两孩人数" style="display:none"></asp:Label>
    
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
