<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03010202.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03010202" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>人口动态信息报告单二填报</title>
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
    <div style="font-size:28px; text-align:center; font-weight:bold"><asp:Label ID="txt_RptTime" runat="server" Text="Label"></asp:Label>份人口动态信息报告单<二>（存根）</div>
    <span style="font-size:14px;">单位：<%=str_AreaName%></span>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;background-color:#ffffff">
        <thead>
           <tr style="text-align:center; background-color:#cccccc">
            <td colspan="11">生育情况</td>
            <td colspan="6">避孕节育情况</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">组别</td>
            <td rowspan="3">妇女姓名</td>
            <td rowspan="3">身份证号码</td>
            <td rowspan="3">丈夫姓名</td>
            <td colspan="7">孩子情况</td>
            <td rowspan="3">组别</td>
            <td rowspan="3">姓名</td>
            <td rowspan="3">身份证号码</td>
            <td rowspan="3">措施日期</td>
            <td rowspan="3">措施名称</td>
            <td rowspan="3">施术单位</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="2">孩次</td>
            <td rowspan="2">孩子姓名</td>
            <td colspan="2">身份证号或出生年月</td>
            <td rowspan="2">政策属性</td>
            <td rowspan="2">健康状况</td>
            <td rowspan="2">血缘关系</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>性别</td>
            <td>出生年月</td>
          </tr>
        </thead>
    <asp:Repeater ID="rep_Data" runat="server">
      <ItemTemplate>
        <tbody>
            <tr style="text-align:center;">
               <td style="background-color:#ccffff" height="22"><%# Eval("Fileds01")%></td>
               <td style="background-color:#ffffcc"><%# Eval("Fileds02")%></td>
               <td><%# Eval("Fileds03")%></td>
               <td><%# Eval("Fileds04")%></td>
               <td><%# Eval("Fileds05")%></td>
               <td><%# Eval("Fileds06")%></td>
               <td><%# Eval("Fileds07")%></td>
               <td><%# Eval("Fileds08")%></td>
               <td><%# Eval("Fileds09")%></td>
               <td><%# Eval("Fileds10")%></td>
               <td><%# Eval("Fileds11")%></td>
               <td style="background-color:#ccffff"><%# Eval("Fileds12")%></td>
               <td style="background-color:#ffffcc"><%# Eval("Fileds13")%></td>
               <td><%# Eval("Fileds14")%></td>
               <td><%# Eval("Fileds15")%></td>
               <td><%# Eval("Fileds16")%></td>
               <td><%# Eval("Fileds17")%></td>
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
    <td align="right">填报日期：<asp:Label ID="txt_OprateDate" runat="server" Text="Label"></asp:Label>&nbsp;</td>
    </tr>
    </table>
     <br/>
     
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
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>
