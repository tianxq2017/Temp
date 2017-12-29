<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03020102.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03020102" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>预防艾滋病母婴传播工作月报表</title>
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
    <div style="font-size:28px; text-align:center; font-weight:bold">预防艾滋病母婴传播工作月报表（<input id="txt_RptTime" readonly="readonly"  size="10" onclick="JTC.setday({format:'yyyy年MM月', readOnly:true})" runat="server" />）</div>
    <span style="font-size:14px;">单位：<%=str_AreaName%></span><br />
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;background-color:#ffffff">
        <thead>
        <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">填报单位</td>
            <td colspan="8">婚前保健</td>
            <td colspan="4">孕期</td>
            <td colspan="9">住院分娩</td>
            <td colspan="8">非住院分娩</td>
            <td rowspan="3">地区产妇总数</td>
            <td rowspan="3">地区活产总数</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td colspan="2">接受婚前保健人数</td>
            <td colspan="2">接受艾滋病咨询人数</td>
            <td colspan="2">接受HIV抗体检测人数</td>
            <td colspan="2">HIV抗体阳性人数</td>
            <td rowspan="2">接受初次产前保健的孕妇数</td>
            <td rowspan="2">接受艾滋病咨询孕妇数</td>
            <td rowspan="2">接受HIV抗体检测孕妇数</td>
            <td rowspan="2">HIV抗体阳性孕妇数</td>
            <td rowspan="2">住院分娩产妇数</td>
            <td rowspan="2">孕期接受艾滋病咨询产妇数</td>
            <td rowspan="2">孕期接受HIV抗体检测产妇数</td>
            <td rowspan="2">仅产时接受艾滋病咨询产妇数</td>
            <td rowspan="2">仅产时接受HIV抗体检测产妇数</td>
            <td rowspan="2">仅产时HIV抗体检测阳性产妇数</td>
            <td rowspan="2">HIV抗体阳性产妇总数</td>
            <td rowspan="2">住院分娩活产数</td>
            <td rowspan="2">HIV抗体阳性产妇所生活产数</td>
            <td rowspan="2">非住院分娩产妇数</td>
            <td rowspan="2">孕期接受艾滋病咨询产妇数</td>
            <td rowspan="2">孕期接受HIV抗体检测产妇数</td>
            <td rowspan="2">仅产时接受HIV抗体检测产妇数</td>
            <td rowspan="2">仅产时HIV抗体检测阳性产妇数</td>
            <td rowspan="2">HIV抗体阳性产妇数</td>
            <td rowspan="2">非住院分娩活产数</td>
            <td rowspan="2">HIV抗体阳性产妇所生活产数</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>男</td>
            <td>女</td>
            <td>男</td>
            <td>女</td>
            <td>男</td>
            <td>女</td>
            <td>男</td>
            <td>女</td>
          </tr>
          <tr style="text-align:center;background-color:#ffffcc">
            <td>合计</td>
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
                <td width="100"><%# Eval("AreaName")%></td>
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
            </tr>           
          </tbody>
    </ItemTemplate>  
    </asp:Repeater>
    </table><br />
<!--报表页脚-->
    <table width="100%" border="0" cellpadding="0" cellspacing="1" >
    <tr style="font-size:14px;">
    <td>负责人：<asp:TextBox ID="txt_SldHeader" runat="server"></asp:TextBox></td>
    <td align="center">填表人：<asp:TextBox ID="txt_SldLeader" runat="server"></asp:TextBox></td>
    <td align="right">填报日期：<input id="txt_OprateDate" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />&nbsp;</td>
    </tr>
    </table>
    <br/>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" >
    <tr style="font-size:14px;">
        <td>友情提示：该表填报数据的单位数量应为：<span style=" font-weight:bold;"><%=village_all_num%></span><br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style=" font-weight:bold; color:green">已上报：<%=reported_num%></span><br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#f60">未上报：<%=no_reported_num%>（<%=no_reported_name%>）</span>
        </td>
    </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="zhengwen" >
    <tr style="font-size:14px;">
    <td>
        <%if (IsReported == "0") {%>
        <asp:CheckBox ID="ck_IsCheck" runat="server" /><span style=" font-size:12px; color:#f60; margin-bottom:10px;">确认审核，审核后您所填报的数据将锁定，不能修改.</span><br/>
        <asp:Button ID="btnUPSH" runat="server" Text="· 审核 ·"  CssClass="submit6" OnClick="btnUPSH_Click"></asp:Button>
        <asp:Button ID="btnEdit" runat="server" Text="· 编辑 ·"  CssClass="submit6" OnClick="btnEdit_Click"></asp:Button>
        <%}%>
        <input type="button" name="ButBackPage" value="· 返回 ·" id="Button1" onclick="javascript:history.go(-1);" class="submit6" />
    </td>
    </tr>
    </table>
     <br/>

</td></tr></table>
</td></tr></table>

</div>
    </form>
</body>
</html>
