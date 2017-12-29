<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata12050901.aspx.cs" Inherits="AreWeb.OnlineCertificate.RptInfo.chatdata12050901" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>7岁以下儿童保健和健康情况季报表</title>
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
    <p style="font-size:18px; text-align:center; font-weight:bold">7岁以下儿童保健和健康情况季报表（ 
                        <asp:DropDownList runat="server" ID="txt_RptTime" >
                        </asp:DropDownList>）</p>
    <p><%=str_AreaName%></p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
       <thead>
        <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">填报单位</td>
            <td colspan="4" rowspan="2">活产数</td>
            <td colspan="3">儿童数</td>
            <td colspan="15">5岁以下儿童死亡情况</td>
            <td colspan="5">6个月内婴儿母乳喂养情况</td>
            <td colspan="12">7岁以下儿童保健服务</td>
            <td colspan="14">5岁以下儿童营养评价</td>
            <td rowspan="3">操作</td>
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
            <td>--</td>
          </tr>

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

 <tr class="zhengwen">
            <td align="right" width="150" bgcolor="#cccccc">
                单位：</td>
            <td colspan="3" bgcolor="#cccccc"><asp:Label ID="lbl_DataAreaSel" runat="server"></asp:Label>&nbsp;<asp:DropDownList ID="dr_DataAreaSel" runat="server"></asp:DropDownList>
            </td>
          </tr>
          <tr class="zhengwen">
            <td colspan="2" rowspan="4" align="right" bgcolor="#CCCCCC">活产数</td>
            <td align="right" bgcolor="#CCCCCC">合计：</td>
            <td><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">男：</td>
            <td><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">女：</td>
            <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">性别不明：</td>
            <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC" rowspan="3"width="150" >儿童数</td>
            <td colspan="2" align="right" bgcolor="#FFFFCC">7岁以下：</td>
            <td ><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td colspan="2" align="right" bgcolor="#FFFFCC" >5岁以下：</td>
            <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td colspan="2" align="right" bgcolor="#FFFFCC">3岁以下：</td>
            <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td rowspan="15" align="right" bgcolor="#CCCCCC">5岁以下儿童死亡情况</td>
            <td width="122" rowspan="4" align="right" bgcolor="#CCCCCC">5岁以下儿童死亡数</td>
            <td align="right" bgcolor="#CCCCCC">合计：</td>
            <td><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
          </tr>
          <tr class="zhengwen">
            <td width="169" align="right" bgcolor="#CCCCCC">男：</td>
            <td><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">女：</td>
             <td><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">性别不明：</td>
           <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
             <td colspan="2" align="right" bgcolor="#CCCCCC">5岁以下儿童死亡率（%）：</td>
             <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>   
          </tr>
          <tr class="zhengwen">
            <td rowspan="4" align="right" bgcolor="#CCCCCC">婴儿死亡数</td>
            <td align="right" bgcolor="#CCCCCC">合计：</td>
            <td><asp:TextBox ID="txtFileds13" runat="server"  EnableViewState="False" MaxLength="25" Width="200px" Text="0" /></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">男：</td>
           <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">女：</td>
            <td><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">性别不明：</td>
           <td><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
             <td colspan="2" align="right" bgcolor="#CCCCCC">婴儿死亡率（%）：</td>
             <td><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>   
          </tr>
          <tr class="zhengwen">
            <td rowspan="4" align="right" bgcolor="#CCCCCC">新生儿死亡 </td>
            <td align="right" bgcolor="#CCCCCC">合计：</td>
            <td><asp:TextBox ID="txtFileds18" runat="server" MaxLength="25" Width="200px" Text="0"/></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">男：</td>
           <td><asp:TextBox ID="txtFileds19" runat="server"  EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">女：</td>
           <td><asp:TextBox ID="txtFileds20" runat="server"  EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">性别不明：</td>
            <td><asp:TextBox ID="txtFileds21" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
             <td colspan="2" align="right" bgcolor="#CCCCCC">新生儿死亡率（‰）：</td>
             <td><asp:TextBox ID="txtFileds22" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>   
          </tr>
		  <tr class="zhengwen">
            <td rowspan="5" align="right" bgcolor="#FFFFCC">6个月内婴儿母乳喂养情况</td>
            <td colspan="2" align="right" bgcolor="#FFFFCC">调查人数：</td>
           <td><asp:TextBox ID="txtFileds23" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#FFFFCC">母乳喂养</td>
            <td align="right" bgcolor="#FFFFCC">人数：</td>
            <td><asp:TextBox ID="txtFileds24" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">率（%）：</td>
            <td><asp:TextBox ID="txtFileds25" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#FFFFCC">纯母乳喂养</td>
            <td align="right" bgcolor="#FFFFCC">人数：</td>
           <td><asp:TextBox ID="txtFileds26" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>   
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">率（%）：</td>
            <td><asp:TextBox ID="txtFileds27" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td rowspan="13" align="right" bgcolor="#CCCCCC">7岁以下儿童保健服务</td>
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#CCCCCC">新生儿访视</td>
            <td align="right" bgcolor="#CCCCCC">人数：</td>
            <td><asp:TextBox ID="txtFileds28" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">率（%）：</td>
            <td><asp:TextBox ID="txtFileds29" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#CCCCCC">新生儿丙酮尿症筛查</td>
            <td align="right" bgcolor="#CCCCCC">人数：</td>
            <td><asp:TextBox ID="txtFileds30" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">率（%）：</td>
            <td><asp:TextBox ID="txtFileds31" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#CCCCCC">新生儿甲状腺功能减低症筛查</td>
            <td align="right" bgcolor="#CCCCCC">人数：</td>
            <td><asp:TextBox ID="txtFileds32" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">率（%）：</td>
            <td><asp:TextBox ID="txtFileds33" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#CCCCCC">新生儿听力筛查</td>
            <td align="right" bgcolor="#CCCCCC">人数：</td>
            <td><asp:TextBox ID="txtFileds34" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">率（%）：</td>
            <td><asp:TextBox ID="txtFileds35" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#CCCCCC">7岁以下儿童健康覆盖</td>
            <td align="right" bgcolor="#CCCCCC">人数：</td>
            <td><asp:TextBox ID="txtFileds36" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">率（%）：</td>
            <td><asp:TextBox ID="txtFileds37" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#CCCCCC">3岁以下儿童健康覆盖</td>
            <td align="right" bgcolor="#CCCCCC">人数：</td>
            <td><asp:TextBox ID="txtFileds38" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC">率（%）：</td>
            <td><asp:TextBox ID="txtFileds39" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>     
          </tr>

          <tr class="zhengwen">
            <td rowspan="14" align="right" bgcolor="#FFFFCC">5岁以下儿童营养评价</td>
            <td rowspan="9" align="right" bgcolor="#FFFFCC">身高（长）体重检查</td>
            <td align="right" bgcolor="#FFFFCC">检查人数：</td>
           <td><asp:TextBox ID="txtFileds40" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">低体重人数：</td>
            <td><asp:TextBox ID="txtFileds41" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">低体重率（%）：</td>
            <td><asp:TextBox ID="txtFileds42" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">生长迟缓人数：</td>
            <td><asp:TextBox ID="txtFileds43" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">生长迟缓率（%）：</td>
            <td><asp:TextBox ID="txtFileds44" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">超重人数：</td>
           <td><asp:TextBox ID="txtFileds45" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">超重率（%）：</td>
           <td><asp:TextBox ID="txtFileds46" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">肥胖人数：</td>
           <td><asp:TextBox ID="txtFileds47" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">肥胖率（%）：</td>
           <td><asp:TextBox ID="txtFileds48" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td rowspan="5" align="right" bgcolor="#FFFFCC">血红蛋白检查：</td>
            <td align="right" bgcolor="#FFFFCC">检查人数：</td>
            <td><asp:TextBox ID="txtFileds49" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">贫血患病人数：</td>
            <td><asp:TextBox ID="txtFileds50" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">贫血患病率（%）：</td>
            <td><asp:TextBox ID="txtFileds51" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">中重度贫血患病人数：</td>
            <td><asp:TextBox ID="txtFileds52" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC">中重度贫血患病（%）：</td>
            <td><asp:TextBox ID="txtFileds53" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>       
          </tr>
    </table>
    <%=js_value %>
    <!-- LFileds_type 与 LFileds_txt成对-->
    <!-- LFileds_type判断值类型  0，字符不判断；1，数字或小数；-->
    <asp:Label ID="LFileds_type" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    <!-- LFileds_txt判断值类型有误时提示标题-->
    <asp:Label ID="LFileds_txt" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    
</td></tr></table>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·"  CssClass="submit6" OnClick="btnAdd_Click"></asp:Button>
<asp:HiddenField ID="hd_IsUp" runat="server"/>
<asp:HiddenField ID="hd_upId" runat="server"/>
<asp:Button ID="btnUp" runat="server" Text="· 审核 ·"  CssClass="submit6" OnClick="btnUp_Click"></asp:Button>
<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>

</div>
    </form>
</body>
</html>
