<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03011702.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03011702" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>年度陕西省计划生育家庭情况统计表（农村）一查看</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/index.css" type="text/css" rel="stylesheet"/>
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
    <div style="font-size:28px; text-align:center; font-weight:bold"><asp:Label ID="txt_RptTime" runat="server" Text="Label"></asp:Label>年度陕西省计划生育家庭情况统计表（农村）</div>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" >
    <tr style="font-size:14px;">
    <td>机构：<%=str_AreaName%></td>
    <td align="right">单位：人&nbsp;</td>
    </tr>
    </table>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="4">单位</td>
            <td colspan="9">一孩户</td>
            <td colspan="4">放弃二胎生育指标户</td>
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
          <tr style="text-align:center; background-color:#ffffcc">
            <td>全镇合计</td>
            <%for (int i = 0; i < arrNum.Length; i++)
              { %>
            <td><%=arrNum[i] %></td>
            <%} %>
          </tr>
        </thead>
    <asp:Repeater ID="rep_Data" runat="server">
      <ItemTemplate>
        <tbody>
            <tr style="text-align:center;">
                <td style="background-color:#ccffff" height="22"><%# Eval("AreaName")%></td>
                <td style="background-color:#ccffcc"><%# Eval("Fileds01")%></td>
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
            </tr>           
          </tbody>
    </ItemTemplate>  
    </asp:Repeater>
    </table><br />
<!--报表页脚-->
    <table width="100%" border="0" cellpadding="0" cellspacing="1" >
    <tr style="font-size:14px;">
    <td>负责人：<asp:Label ID="txt_SldHeader" runat="server" Text="Label"></asp:Label></td>
    <td align="center">填表人：<asp:Label ID="txt_SldLeader" runat="server" Text="Label"></asp:Label></td>
    <td align="right">填报日期：<asp:Label ID="txt_OprateDate" runat="server" Text="Label"></asp:Label>&nbsp;</td>
    </tr>
    </table>
    <br/>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" >
    <tr style="font-size:14px;">
        <td>
           本周期填报数据的单位数量应为：<span style=" font-weight:bold;"><%=village_all_num%></span><br /> &nbsp;<span style=" font-weight:bold; color:green">已上报：<%=reported_num%></span><br /> &nbsp;<span style="color:#f60">未上报：<%=no_reported_num%>（<%=no_reported_name%>）</span>
        </td>
    </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="zhengwen" >
    <tr style="font-size:14px;">
    <td>
       <%if (IsReported == "0") {%>
        <asp:CheckBox ID="ck_IsCheck" runat="server" /><span style=" font-size:12px; color:#f60; margin-bottom:10px;">确认上报，上报后您所填报的数据将锁定，不能修改.</span><br/>
        <asp:Button ID="btnUPSH" runat="server" Text="· 上报 ·"  CssClass="submit6" OnClick="btnUPSH_Click"></asp:Button>
        <asp:Button ID="btnEdit" runat="server" Text="· 编辑 ·"  CssClass="submit6" OnClick="btnEdit_Click"></asp:Button>
        <%}%>
        <input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:history.go(-1);" class="submit6" />
    </td>
    </tr>
    </table>
     <br/>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
</td></tr></table>
</td></tr></table>

</div>
    </form>
</body>
</html>
