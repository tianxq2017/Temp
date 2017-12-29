<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03020202.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03020202" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>高危儿体弱儿转诊情况月报表</title>
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
    <div style="font-size:28px; text-align:center; font-weight:bold">高危儿体弱儿转诊情况月报表（<input id="txt_RptTime" readonly="readonly"  size="10" onclick="JTC.setday({format:'yyyy年MM月', readOnly:true})" runat="server" />月）</div>
    <span style="font-size:14px;">单位：<%=str_UnitName%></span><br />
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;background-color:#ffffff">
        <thead>
        <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="4">单位</td>
            <td colspan="6">出生</td>
            <td rowspan="4">死亡人数</td>
            <td colspan="6">怀孕</td>
            <td colspan="8">手术</td>
            <td rowspan="4">领证人数</td>
            <td colspan="2">女性初婚人数</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">人 数</td>
            <td colspan="5">其中</td>
            <td rowspan="3">人数</td>
            <td colspan="5">其中</td>
            <td rowspan="3">合计</td>
            <td colspan="7">其中</td>
            <td rowspan="3">合计</td>
            <td rowspan="3">其中二十三岁以上结婚数</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td colspan="2">计划内</td>
            <td colspan="3">计划外</td>
            <td colspan="2">计划内</td>
            <td colspan="3">计划外</td>
            <td rowspan="2">男扎</td>
            <td rowspan="2">女扎</td>
            <td colspan="2">上环</td>
            <td rowspan="2">取环</td>
            <td rowspan="2">人流</td>
            <td rowspan="2">引产</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>一孩</td>
            <td>二孩</td>
            <td>一孩</td>
            <td>二孩</td>
            <td>多孩</td>
            <td>一孩</td>
            <td>二孩</td>
            <td>一孩</td>
            <td>二孩</td>
            <td>多孩</td>
            <td>小计</td>
            <td>其中一孩上环</td>
          </tr>
          <tr style="text-align:center;background-color:#ffffcc">
            <td height="22">合计</td>
            <td><%=num1%></td>
            <td><%=num2%></td>
            <td><%=num3%></td>
            <td><%=num4%></td>
            <td><%=num5%></td>
            <td><%=num6%></td>
            <td><%=num7%></td>
            <td><%=num8%></td>
            <td><%=num9%></td>
            <td><%=num10%></td>
            <td><%=num11%></td>
            <td><%=num12%></td>
            <td><%=num13%></td>
            <td><%=num14%></td>
            <td><%=num15%></td>
            <td><%=num16%></td>
            <td><%=num17%></td>
            <td><%=num18%></td>
            <td><%=num19%></td>
            <td><%=num20%></td>
            <td><%=num21%></td>
            <td><%=num22%></td>
            <td><%=num23%></td>
            <td><%=num24%></td>
          </tr>
        </thead>
    <asp:Repeater ID="rep_Data" runat="server">
      <ItemTemplate>
        <tbody>
            <tr style="text-align:center;">
                <td style="background-color:#ccffff" height="22"><%# Eval("AreaName")%></td>
                <td style="background-color:#ccffcc"> <%#GetNumByCol(Eval("Fileds01"), Eval("Fileds02"), Eval("Fileds03"),Eval("Fileds04"),Eval("Fileds05"))%> </td>
                <td><%# Eval("Fileds01")%></td>
                <td><%# Eval("Fileds02")%></td>
                <td><%# Eval("Fileds03")%></td>
                <td><%# Eval("Fileds04")%></td>
                <td><%# Eval("Fileds05")%></td>
                <td style="background-color:#ffccff"><%# Eval("Fileds06")%></td>
                <td style="background-color:#ccffcc"> <%#GetNumByCol(Eval("Fileds07"), Eval("Fileds08"), Eval("Fileds09"),Eval("Fileds10"),Eval("Fileds11"))%> </td>
                <td><%# Eval("Fileds07")%></td>
                <td><%# Eval("Fileds08")%></td>
                <td ><%# Eval("Fileds09")%></td>
                <td><%# Eval("Fileds10")%></td>
                <td><%# Eval("Fileds11")%></td>
                 <td style="background-color:#ccffcc"> <%#GetNumByCol(Eval("Fileds12"), Eval("Fileds13"), Eval("Fileds14"), Eval("Fileds15"), Eval("Fileds16"), Eval("Fileds17"))%> </td>
                <td><%# Eval("Fileds12")%></td>
                <td><%# Eval("Fileds13")%></td>
                <td><%# Eval("Fileds14")%></td>
                <td><%# Eval("Fileds14")%></td>
                <td><%# Eval("Fileds15")%></td>
                <td><%# Eval("Fileds16")%></td>
                <td><%# Eval("Fileds17")%></td>
                <td><%# Eval("Fileds18")%></td>
                <td style="background-color:#ccffcc"><%# Eval("Fileds19")%></td>
                <td><%# Eval("Fileds19")%></td>
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
        <td>
           本周期尚未填报数据的单位：
        </td>
    </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="zhengwen" >
    <tr style="font-size:14px;">
    <td>
        <%if (IsReported == "0") {%>
        <asp:CheckBox ID="ck_IsCheck" runat="server" /><span style=" font-size:12px; color:#f60; margin-bottom:10px;">确认上报，上报后您所填报的数据将锁定，不能修改.</span><br/>
        <input type="submit"  name="submit1" class="submit6" onclick="check();return false;" value="确认上报"/>
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
    <script type="text/javascript">
        function check(){
          var checkbox = document.getElementById('ck_IsCheck');
          if(checkbox.checked){
            if (confirm('确认上报?上报后您所填报的数据将锁定，不能修改.'))
            {
                this.submit();
            } 
            else 
            {
                return false;
            } 
          }else{
            alert("请确认上报");
            return false;
          }
        }
    </script>
</body>
</html>
