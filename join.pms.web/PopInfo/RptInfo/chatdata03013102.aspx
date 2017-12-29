<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03013102.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03013102" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>人口动态信息报告单一填报</title>
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
    <div style="font-size:28px;text-align:center; font-weight:bold"><%=str_RptTime%>份人口动态信息报告单（一）(<input id="txt_RptTime" readonly="readonly"  size="10" onclick="JTC.setday({format:'yyyy年MM月', readOnly:true})" runat="server" />月)</div><br/>
    <span style="font-size:14px;">单位：<%=str_UnitName%></span>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;background-color:#ffffff">
        <thead>
        <tr style="text-align:center; background-color:#cccccc;">
            <td colspan="14" height="22">婚姻、迁移、流动情况</td>
            <td colspan="5">死亡情况</td>
        </tr>
        <tr style="text-align:center;background-color:#cccccc">
            <td height="22">村名</td>
            <td>组别</td>
            <td>户口编码</td>
            <td>姓名</td>
            <td>公民身份证号</td>
            <td>变动类型</td>
            <td>民族</td>
            <td>文化程度</td>
            <td>户口性质</td>
            <td>婚姻状况</td>
            <td>初婚日期</td>
            <td>居住状况</td>
            <td>与户主关系</td>
            <td>配偶姓名</td>
            <td>姓名</td>
            <td>性别</td>
            <td>出生年月</td>
            <td>死亡日期</td>
            <td>死亡原因</td>
          </tr>
        </thead>
    <asp:Repeater ID="rep_Data" runat="server">
      <ItemTemplate>
        <tbody>
            <tr style="text-align:center;">
               <td style="background-color:#ccffff" height="22"><%# Eval("AreaName")%></td>
               <td style="background-color:#ccffff"><%# Eval("Fileds01")%></td>
               <td><%# Eval("Fileds02")%></td>
               <td style="background-color:#ffffcc"><%# Eval("Fileds03")%></td>
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
               <td style="background-color:#ffffcc"><%# Eval("Fileds14")%></td>
               <td><%# Eval("Fileds15")%></td>
               <td><%# Eval("Fileds16")%></td>
               <td><%# Eval("Fileds17")%></td>
               <td><%# Eval("Fileds18")%></td>
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
