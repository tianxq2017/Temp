<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03021202.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03021202" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>孕产妇保健和健康情况年报表</title>
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
    <div style="font-size:28px; text-align:center; font-weight:bold">孕产妇保健和健康情况年报表（<input id="txt_RptTime" readonly="readonly"  size="10" onclick="JTC.setday({format:'yyyy年MM月', readOnly:true})" runat="server" />年）</div>
    <span style="font-size:14px;">单位：<%=str_AreaName%></span><br />
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;background-color:#ffffff">
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
            
          </tr>
      
    <asp:Repeater ID="rep_Data" runat="server">
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
