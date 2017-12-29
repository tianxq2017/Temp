<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03011902.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03011902" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>陕西省实施全面两孩政策育龄妇女生育情况统计表（县）一填报</title>
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
    <div style="font-size:28px; text-align:center; font-weight:bold">陕西省实施全面两孩政策育龄妇女生育情况统计表（<asp:DropDownList ID="dr_RptTime" runat="server">
                                                                                        <asp:ListItem>1</asp:ListItem>
                                                                                        <asp:ListItem>2</asp:ListItem>
                                                                                        <asp:ListItem>3</asp:ListItem>
                                                                                        <asp:ListItem>4</asp:ListItem>
                                                                                        <asp:ListItem>5</asp:ListItem>
                                                                                        <asp:ListItem>6</asp:ListItem>
                                                                                        <asp:ListItem>7</asp:ListItem>
                                                                                        <asp:ListItem>8</asp:ListItem>
                                                                                        <asp:ListItem>9</asp:ListItem>
                                                                                        <asp:ListItem>10</asp:ListItem>
                                                                                        <asp:ListItem>11</asp:ListItem>
                                                                                        <asp:ListItem>12</asp:ListItem>
                                                                                       </asp:DropDownList> <%=str_RptTime%>月）</div>
    <p><%=str_UnitName%></p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
        <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="4">单位</td>
            <td colspan="3">全面两孩出生情况累计数</td>
            <td colspan="16">本月两孩育龄妇女情况及出生两孩人口数量</td>
            <td rowspan="4">当月新增怀孕两孩人数</td>
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
          <tr style="text-align:center;background-color:#ffffcc">
            <td>合计</td>
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
          </tr>
        </thead>
        <tbody>
        <asp:Literal ID="ltr_Content" runat="server"></asp:Literal>
        </tbody>
    </table><br/>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" >
    <tr style="font-size:14px;">
    <td>负责人：<asp:TextBox ID="txt_SldHeader" runat="server"></asp:TextBox></td>
    <td align="center">填表人：<asp:TextBox ID="txt_SldLeader" runat="server"></asp:TextBox></td>
    <td align="right">填报日期：<input id="txt_OprateDate" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />&nbsp;</td>
    </tr>
    </table>
    
    <%if (IsReported == "0") {%>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="zhengwen" >
    <tr style="font-size:14px;">
    <td>
        <asp:CheckBox ID="ck_IsCheck" runat="server" /><span style=" font-size:12px; color:#f60; margin-bottom:10px;">确认审核，审核后您所填报的数据将锁定，不能修改.</span><br/>
        <input type="submit"  name="submit1" class="submit6" onclick="check();return false;" value="确认审核"/>
    </td>
    </tr>
    </table>
     <br/>
     <%}%>
</td></tr></table>
</td></tr></table>

</div>
    </form>
    <script type="text/javascript">
        function check(){
          var checkbox = document.getElementById('ck_IsCheck');
          if(checkbox.checked){
            if (confirm('确认审核?审核后您所填报的数据将锁定，不能修改.'))
            {
                this.submit();
            } 
            else 
            {
                return false;
            } 
          }else{
            alert("请确认审核");
            return false;
          }
        }
    </script>
</body>
</html>
