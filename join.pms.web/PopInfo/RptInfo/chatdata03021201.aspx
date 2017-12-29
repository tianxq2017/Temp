<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03021201.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03021201" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>孕产妇保健和健康情况年报表</title>
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
    <p style="font-size:18px; text-align:center; font-weight:bold">孕产妇保健和健康情况年报表（<asp:DropDownList runat="server" ID="txt_RptTime" ></asp:DropDownList>）</p>
    <p><%=str_AreaName%></p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
        <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="4">填报单位</td>
            <td colspan="4" rowspan="2">活产数</td>
            <td colspan="3" rowspan="2">产妇数</td>
            <td colspan="37">孕产妇管理</td>
            <td colspan="8">接生情况</td>
            <td colspan="6">孕产妇高危管理</td>
            <td colspan="12">孕产妇死亡</td>
            <td colspan="12">围产儿情况</td>
            <td rowspan="4">操作</td>
        </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td colspan="2">产妇建卡</td>
            <td colspan="6">产妇产前检查情况</td>
            <td colspan="5">产妇孕产期血红蛋白检测</td>
            <td colspan="4">孕产妇艾滋病病毒检测</td>
            <td colspan="4">孕产妇梅毒检测</td>
            <td colspan="4">产妇乙肝表面抗原检测</td>
            <td colspan="4">孕产妇产前筛查</td>
            <td colspan="4">孕产妇产前诊断</td>
            <td colspan="2">产妇产后访视</td>
            <td colspan="2">产妇系统管理</td>
            <td colspan="2">住院分娩</td>
            <td colspan="2">刨宫产</td>
            <td colspan="2">非住院分娩中新法接生</td>
            <td colspan="2">新法接生</td>
            <td rowspan="3">高危产妇人数</td>
            <td rowspan="3">高危产妇数%</td>
            <td colspan="2">高危产妇管理</td>
            <td colspan="2">高危产妇住院分娩</td>
            <td rowspan="3">死亡人数</td>
            <td rowspan="3">死亡率1/10万</td>
            <td colspan="2">产科出血</td>
            <td colspan="2">妊娠高血压疾病</td>
            <td colspan="2">内科合并症</td>
            <td colspan="2">羊水栓塞</td>
            <td colspan="2">其它原因</td>
            <td rowspan="3">低出生体重儿数</td>
            <td rowspan="3">低出生体重儿百分比%</td>
            <td rowspan="3">巨大儿数</td>
            <td rowspan="3">巨大儿百分比%</td>
            <td rowspan="3">早产儿数</td>
            <td rowspan="3">早产率%</td>
            <td rowspan="3">死胎死产数</td>
            <td colspan="4">早期新生儿死亡数</td>
            <td rowspan="3">围产儿死亡率‰</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="2">合计</td>
            <td rowspan="2">男</td>
            <td rowspan="2">女</td>
            <td rowspan="2">性别不明</td>
            <td rowspan="2">合计</td>
            <td rowspan="2">非农业户籍</td>
            <td rowspan="2">农业户籍</td>
            <td rowspan="2">人数</td>
            <td rowspan="2">%</td>
            <td colspan="2">产检</td>
            <td colspan="2">产检≥5次</td>
            <td colspan="2">早检</td>
            <td rowspan="2">检测人数</td>
            <td colspan="2">贫血</td>
            <td colspan="2">中重度贫血</td>
            <td rowspan="2">检测人数</td>
            <td rowspan="2">%</td>
            <td colspan="2">感染</td>
            <td rowspan="2">检测人数</td>
            <td rowspan="2">%</td>
            <td colspan="2">感染</td>
            <td rowspan="2">检测人数</td>
            <td rowspan="2">%</td>
            <td colspan="2">阳性</td>
            <td rowspan="2">筛查人数</td>
            <td rowspan="2">%</td>
            <td colspan="2">高危</td>
            <td rowspan="2">诊断人数</td>
            <td rowspan="2">%</td>
            <td colspan="2">确诊</td>
            <td rowspan="2">人数</td>
            <td rowspan="2">%</td>
            <td rowspan="2">人数</td>
            <td rowspan="2">%</td>
            <td rowspan="2">活产数</td>
            <td rowspan="2">%</td>
            <td rowspan="2">活产数</td>
            <td rowspan="2">%</td>
            <td rowspan="2">活产数</td>
            <td rowspan="2">%</td>
            <td rowspan="2">活产数</td>
            <td rowspan="2">%</td>
            <td rowspan="2">人数</td>
            <td rowspan="2">%</td>
            <td rowspan="2">人数</td>
            <td rowspan="2">%</td>
            <td rowspan="2">人数</td>
            <td rowspan="2">%</td>
            <td rowspan="2">人数</td>
            <td rowspan="2">%</td>
            <td rowspan="2">人数</td>
            <td rowspan="2">%</td>
            <td rowspan="2">人数</td>
            <td rowspan="2">%</td>
            <td rowspan="2">人数</td>
            <td rowspan="2">%</td>
            <td rowspan="2">合计</td>
            <td rowspan="2">男</td>
            <td rowspan="2">女</td>
            <td rowspan="2">性别不明</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>人数</td>
            <td>%</td>
            <td>人数</td>
            <td>%</td>
            <td>人数</td>
            <td>%</td>
            <td>人数</td>
            <td>%</td>
            <td>人数</td>
            <td>%</td>
            <td>人数</td>
            <td>1/10万</td>
            <td>人数</td>
            <td>%</td>
            <td>人数</td>
            <td>%</td>
            <td>人数</td>
            <td>%</td>
            <td>人数</td>
            <td>%</td>
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
                <td><%# Eval("Fileds54")%></td>
                <td><%# Eval("Fileds55")%></td>
                <td><%# Eval("Fileds56")%></td>
                <td><%# Eval("Fileds57")%></td>
                <td><%# Eval("Fileds58")%></td>
                <td><%# Eval("Fileds59")%></td>
                <td><%# Eval("Fileds60")%></td>
                <td><%# Eval("Fileds61")%></td>
                <td><%# Eval("Fileds62")%></td>
                <td><%# Eval("Fileds63")%></td>
                <td><%# Eval("Fileds64")%></td>
                <td><%# Eval("Fileds65")%></td>
                <td><%# Eval("Fileds66")%></td>
                <td><%# Eval("Fileds67")%></td>
                <td><%# Eval("Fileds68")%></td>
                <td><%# Eval("Fileds69")%></td>
                <td><%# Eval("Fileds70")%></td>
                <td><%# Eval("Fileds71")%></td>
                <td><%# Eval("Fileds72")%></td>
                <td><%# Eval("Fileds73")%></td>
                <td><%# Eval("Fileds74")%></td>
                <td><%# Eval("Fileds75")%></td>
                <td><%# Eval("Fileds76")%></td>
                <td><%# Eval("Fileds77")%></td>
                <td><%# Eval("Fileds78")%></td>
                <td><%# Eval("Fileds79")%></td>
                <td><%# Eval("Fileds80")%></td>
                <td><%# Eval("Fileds81")%></td>
                <td><%# Eval("Fileds82")%></td>
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
    <td colspan="2"  align="right" width="120" bgcolor="#cccccc">单位：</td>
    <td colspan="3" bgcolor="#cccccc"><asp:Label ID="lbl_DataAreaSel" runat="server"></asp:Label>&nbsp;<asp:DropDownList ID="dr_DataAreaSel" runat="server"></asp:DropDownList></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" rowspan="4" align="right" bgcolor="#FFFFCC">活产数</td>
    <td colspan="2" align="right" bgcolor="#FFFFCC">合计：</td>
    <td><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">男：</td>
    <td><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">女：</td>
    <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">性别不明：</td>
    <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" rowspan="3"  align="right" bgcolor="#cccccc">产妇数</td>
    <td colspan="2" align="right" bgcolor="#cccccc">合计：</td>
    <td><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">非农业户籍：</td>
    <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">农业户籍：</td>
    <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="37" width="130" align="right" bgcolor="#FFFFCC">孕产妇管理</td>
    <td rowspan="2" align="right" bgcolor="#FFFFCC">产妇建卡</td>
    <td colspan="2" align="right" bgcolor="#FFFFCC">人数：</td>
    <td><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="6" align="right" bgcolor="#FFFFCC">产妇产前检查情况</td>
    <td rowspan="2" align="right" bgcolor="#FFFFCC">产检</td>
    <td align="right" bgcolor="#FFFFCC">人数：</td>
    <td><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" width="100" bgcolor="#FFFFCC">产检≥5次</td>
    <td align="right" bgcolor="#FFFFCC">人数：</td>
    <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">早检</td>
    <td align="right" width="100" bgcolor="#FFFFCC">人数：</td>
    <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="5" align="right"  width="180" bgcolor="#FFFFCC">产妇孕产期血红蛋白检测</td>
    <td colspan="2" align="right" bgcolor="#FFFFCC">检测人数：</td>
    <td><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">贫血</td>
    <td align="right" bgcolor="#FFFFCC">人数：</td>
    <td><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">中重度贫血</td>
    <td align="right" bgcolor="#FFFFCC">人数：</td>
    <td><asp:TextBox ID="txtFileds19" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds20" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="4" align="right" bgcolor="#FFFFCC">孕产妇艾滋病病毒检测</td>
    <td colspan="2" align="right" bgcolor="#FFFFCC">检测人数：</td>
    <td><asp:TextBox ID="txtFileds21" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds22" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">感染</td>
    <td align="right" bgcolor="#FFFFCC">人数：</td>
    <td><asp:TextBox ID="txtFileds23" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">1/10万：</td>
    <td><asp:TextBox ID="txtFileds24" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="4" align="right" bgcolor="#FFFFCC">孕产妇梅毒检测</td>
    <td colspan="2" align="right" bgcolor="#FFFFCC">检测人数：</td>
    <td><asp:TextBox ID="txtFileds25" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds26" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">感染</td>
    <td align="right" bgcolor="#FFFFCC">人数：</td>
    <td><asp:TextBox ID="txtFileds27" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds28" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="4" align="right" bgcolor="#FFFFCC">产妇乙肝表面抗原检测</td>
    <td colspan="2" align="right" bgcolor="#FFFFCC">检测人数：</td>
    <td><asp:TextBox ID="txtFileds29" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds30" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">阳性</td>
    <td align="right" bgcolor="#FFFFCC">人数：</td>
    <td><asp:TextBox ID="txtFileds31" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds32" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="4" align="right" bgcolor="#FFFFCC">孕产妇产前筛查</td>
    <td colspan="2" align="right" bgcolor="#FFFFCC">筛查人数：</td>
    <td><asp:TextBox ID="txtFileds33" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds34" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">高危</td>
    <td align="right" bgcolor="#FFFFCC">人数：</td>
    <td><asp:TextBox ID="txtFileds35" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds36" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="4" align="right" bgcolor="#FFFFCC">孕产妇产前诊断</td>
    <td colspan="2" align="right" bgcolor="#FFFFCC">诊断人数：</td>
    <td><asp:TextBox ID="txtFileds37" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds38" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">确诊</td>
    <td align="right" bgcolor="#FFFFCC">人数：</td>
    <td><asp:TextBox ID="txtFileds39" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds40" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">产妇产后访视</td>
    <td colspan="2" align="right" bgcolor="#FFFFCC">人数：</td>
    <td><asp:TextBox ID="txtFileds41" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds42" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">产妇系统管理</td>
    <td colspan="2" align="right" bgcolor="#FFFFCC">人数：</td>
    <td><asp:TextBox ID="txtFileds43" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds44" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="8" align="right" bgcolor="#cccccc">接生情况</td>
    <td rowspan="2" align="right" bgcolor="#cccccc">住院分娩</td>
    <td colspan="2" align="right" bgcolor="#cccccc">活产数：</td>
    <td><asp:TextBox ID="txtFileds45" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">%：</td>
    <td><asp:TextBox ID="txtFileds46" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#cccccc">刨宫产</td>
    <td colspan="2" align="right" bgcolor="#cccccc">活产数：</td>
    <td><asp:TextBox ID="txtFileds47" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">%：</td>
    <td><asp:TextBox ID="txtFileds48" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#cccccc">非住院分娩中新法接生</td>
    <td colspan="2" align="right" bgcolor="#cccccc">活产数：</td>
    <td><asp:TextBox ID="txtFileds49" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">%：</td>
    <td><asp:TextBox ID="txtFileds50" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#cccccc">新法接生</td>
    <td colspan="2" align="right" bgcolor="#cccccc">活产数：</td>
    <td><asp:TextBox ID="txtFileds51" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">%：</td>
    <td><asp:TextBox ID="txtFileds52" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="6" align="right" bgcolor="#FFFFCC">孕产妇高危管理</td>
    <td colspan="3" align="right" bgcolor="#FFFFCC">高危产妇人数：</td>
    <td><asp:TextBox ID="txtFileds53" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="3" align="right" bgcolor="#FFFFCC">高危产妇数%：</td>
    <td><asp:TextBox ID="txtFileds54" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">高危产妇管理</td>
    <td colspan="2" align="right" bgcolor="#FFFFCC">人数：</td>
    <td><asp:TextBox ID="txtFileds55" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds56" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">高危产妇住院分娩</td>
    <td colspan="2" align="right" bgcolor="#FFFFCC">人数：</td>
    <td><asp:TextBox ID="txtFileds57" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">%：</td>
    <td><asp:TextBox ID="txtFileds58" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="12" align="right" bgcolor="#cccccc">孕产妇死亡</td>
    <td colspan="3" align="right" bgcolor="#cccccc">死亡人数：</td>
    <td><asp:TextBox ID="txtFileds59" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="3" align="right" bgcolor="#cccccc">死亡率1/10万：</td>
    <td><asp:TextBox ID="txtFileds60" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#cccccc">产科出血</td>
    <td colspan="2" align="right" bgcolor="#cccccc">人数：</td>
    <td><asp:TextBox ID="txtFileds61" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">%：</td>
    <td><asp:TextBox ID="txtFileds62" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#cccccc">妊娠高血压疾病</td>
    <td colspan="2" align="right" bgcolor="#cccccc">人数：</td>
    <td><asp:TextBox ID="txtFileds63" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">%：</td>
    <td><asp:TextBox ID="txtFileds64" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#cccccc">内科合并症</td>
    <td colspan="2" align="right" bgcolor="#cccccc">人数：</td>
    <td><asp:TextBox ID="txtFileds65" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">%：</td>
    <td><asp:TextBox ID="txtFileds66" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#cccccc">羊水栓塞</td>
    <td colspan="2" align="right" bgcolor="#cccccc">人数：</td>
    <td><asp:TextBox ID="txtFileds67" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">%：</td>
    <td><asp:TextBox ID="txtFileds68" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#cccccc">其它原因</td>
    <td colspan="2" align="right" bgcolor="#cccccc">人数：</td>
    <td><asp:TextBox ID="txtFileds69" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">%：</td>
    <td><asp:TextBox ID="txtFileds70" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="12" align="right" bgcolor="#FFFFCC">围产儿情况</td>
    <td colspan="3" align="right" bgcolor="#FFFFCC">低出生体重儿数：</td>
    <td><asp:TextBox ID="txtFileds71" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="3" align="right" bgcolor="#FFFFCC">低出生体重儿百分比%：</td>
    <td><asp:TextBox ID="txtFileds72" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="3" align="right" bgcolor="#FFFFCC">巨大儿数：</td>
    <td><asp:TextBox ID="txtFileds73" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="3" align="right" bgcolor="#FFFFCC">巨大儿百分比%：</td>
    <td><asp:TextBox ID="txtFileds74" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="3" align="right" bgcolor="#FFFFCC">早产儿数：</td>
    <td><asp:TextBox ID="txtFileds75" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="3" align="right" bgcolor="#FFFFCC">早产率%：</td>
    <td><asp:TextBox ID="txtFileds76" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="3" align="right" bgcolor="#FFFFCC">死胎死产数：</td>
    <td><asp:TextBox ID="txtFileds77" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="4" align="right" bgcolor="#FFFFCC">早期新生儿死亡数</td>
    <td colspan="2" align="right" bgcolor="#FFFFCC">合计：</td>
    <td><asp:TextBox ID="txtFileds78" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">男：</td>
    <td><asp:TextBox ID="txtFileds79" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">女：</td>
    <td><asp:TextBox ID="txtFileds80" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">性别不明：</td>
    <td><asp:TextBox ID="txtFileds81" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="3" align="right" bgcolor="#FFFFCC">围产儿死亡率‰：</td>
    <td><asp:TextBox ID="txtFileds82" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
</table>
    <%=js_value %>
    <!-- LFileds_type 与 LFileds_txt成对-->
    <!-- LFileds_type判断值类型  0，字符不判断；1，数字或小数；-->
    <asp:Label ID="LFileds_type" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    <!-- LFileds_txt判断值类型有误时提示标题-->
    <asp:Label ID="LFileds_txt" runat="server"  Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    
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
