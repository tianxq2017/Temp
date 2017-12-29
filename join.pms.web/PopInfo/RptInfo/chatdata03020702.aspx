<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03020702.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03020702" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>7岁以下儿童保健和健康情况季报表</title>
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
    <div style="font-size:28px; text-align:center; font-weight:bold">7岁以下儿童保健和健康情况季报表（<asp:Label ID="txt_RptTime" runat="server" Text="Label"></asp:Label>）</div>
    <span style="font-size:14px;">单位：<%=str_AreaName%></span><br />
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;background-color:#ffffff">
        <thead>
        <tr style="text-align:center; background-color:#cccccc">
          <td rowspan="3">填报单位</td>
            <td colspan="4" rowspan="2">活产数</td>
            <td colspan="3">儿童数</td>
            <td colspan="15">5岁以下儿童死亡情况</td>
            <td colspan="5">6个月内婴儿母乳喂养情况</td>
            <td colspan="12">7岁以下儿童保健服务</td>
            <td colspan="14">5岁以下儿童营养评价</td>
        </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="2">7岁以下</td>
            <td rowspan="2">5岁以下</td>
            <td rowspan="2">3岁以下</td>
            <td colspan="4">5岁以下儿童死亡数</td>
            <td rowspan="2">5岁以下儿童死亡率（‰）</td>
            <td colspan="4">婴儿死亡数</td>
            <td rowspan="2">婴儿死亡率（‰）</td>
            <td colspan="4">新生儿死亡数</td>
            <td rowspan="2">新生儿死亡率（‰）</td>
            <td rowspan="2">调查人数</td>
            <td colspan="2">母乳喂养</td>
            <td colspan="2">纯母乳喂养</td>
            <td colspan="2">新生儿访视</td>
            <td colspan="2">新生儿苯丙酮尿症筛查</td>
            <td colspan="2">新生儿甲状腺功能减低症筛查</td>
            <td colspan="2">新生儿听力筛查</td>
            <td colspan="2">7岁以下儿童保健覆盖</td>
            <td colspan="2">3岁以下儿童健康覆盖</td>
            <td colspan="9">身高（长）体重检查</td>
            <td colspan="5">血红蛋白检查</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>合计</td>
            <td>男</td>
            <td>女</td>
            <td>性别不明</td>
            <td>合计</td>
            <td>男</td>
            <td>女</td>
            <td>性别不明</td>
            <td>合计</td>
            <td>男</td>
            <td>女</td>
            <td>性别不明</td>
            <td>合计</td>
            <td>男</td>
            <td>女</td>
            <td>性别不明</td>
            <td>人数</td>
            <td>率%</td>
            <td>人数</td>
            <td>率%</td>
            <td>人数</td>
            <td>率%</td>
            <td>人数</td>
            <td>率%</td>
            <td>人数</td>
            <td>率%</td>
            <td>人数</td>
            <td>率%</td>
            <td>人数</td>
            <td>率%</td>
            <td>人数</td>
            <td>率%</td>
            <td>检查人数</td>
            <td>低体重人数</td>
            <td>低体重率（%）</td>
            <td>生长迟缓人数</td>
            <td>生长迟缓率（%）</td>
            <td>超重人数</td>
            <td>超重率（%）</td>
            <td>肥胖人数</td>
            <td>肥胖率（%）</td>
            <td>检查人数</td>
            <td>贫血患病人数</td>
            <td>贫血患病率（%）</td>
            <td>中重度贫血患病人数</td>
            <td>中重度贫血患病率（%）</td>
          </tr>
        </thead>
          <tr style="text-align:center;">
            <td>合计</td>
            <%for (int i = 0; i < arrNum.Length; i++)
              { %>
            <td><%=arrNum[i] %></td>
            <%} %>         
          </tr>
    <asp:Repeater ID="rep_Data" runat="server">
      <ItemTemplate>
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
            <td><%# Eval("Fileds37")%></td>
            <td><%# Eval("Fileds38")%></td>
            <td><%# Eval("Fileds39")%></td>
            <td><%# Eval("Fileds40")%></td>
            <td><%# Eval("Fileds41")%></td>
            <td><%# Eval("Fileds42")%></td>
            <td><%# Eval("Fileds43")%></td>
            <td><%# Eval("Fileds44")%></td>
            <td><%# Eval("Fileds45")%></td>
            <td><%# Eval("Fileds46")%></td>
            <td><%# Eval("Fileds47")%></td>
            <td><%# Eval("Fileds48")%></td>
            <td><%# Eval("Fileds49")%></td>
            <td><%# Eval("Fileds50")%></td>
            <td><%# Eval("Fileds51")%></td>
            <td><%# Eval("Fileds52")%></td>
            <td><%# Eval("Fileds53")%></td>
          </tr>
    </ItemTemplate>  
    </asp:Repeater>
    </table><br />
<!--报表页脚-->
    <table width="100%" border="0" cellpadding="0" cellspacing="1" >
    <tr style="font-size:14px;">
    <td>负责人：<asp:Label ID="txt_SldHeader" runat="server" Text="Label"></asp:Label></td>
    <td align="center">填表人：<asp:Label ID="txt_SldLeader" runat="server" Text="Label"></asp:Label></td>
    <td align="right">填报日期：<asp:Label ID="txt_OprateDate" runat="server" Text="Label"></asp:Label>&nbsp;</td></tr>
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
