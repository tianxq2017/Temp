<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainRight.aspx.cs" Inherits="join.pms.web.MainRight" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>无标题页</title>
<link href="/css/left_r.css" type="text/css" rel="stylesheet">
<script language="JavaScript" type="text/javascript" id="Script1">
<!--
// 左右伸缩
var status = 1;
function switchSysBar(){
    if (1 == window.status){
        window.status = 0;
        parent.treeFrame_r.cols="*,15";
        document.getElementById("switchPoint_r").innerHTML = '<span class="ss_l"></span>';
        parent.rightFrame.document.getElementById("sysMenu_r").style.display="none";
    }
    else{
        window.status = 1;
        parent.treeFrame_r.cols="*,269";
        document.getElementById("switchPoint_r").innerHTML = '<span class="ss_r"></span>';
        parent.rightFrame.document.getElementById("sysMenu_r").style.display="block";
    }
}
//-->
</script>

</head>
<body style="OVERFLOW-X: hidden">
<div class="left_a_r_01">
<table class="left_r_01" height="100%" border="0" cellpadding="0" cellspacing="0" align="right">
  <tr>
    <td height="100%" valign="middle" class="left_ss_r"><span id="switchPoint_r" onClick="switchSysBar()" class="navpoint"><span class="ss_r"></span></span></td>
    <%--<td class="leftbg_r_b" height="100%"></td>--%>
    <td valign="top" class="leftbg_r" id="sysMenu_r" height="100%">
      <div class="admin_user_01 line_t">
	    <div class="admin_user_a">
	      <div class="a1"></div>
	      <div class="a2"><asp:Literal ID="LiteralUserInfo" runat="server" EnableViewState="false"></asp:Literal></div>
	      <div class="clr"></div>
	    </div>
	    <div class="admin_user_b">
	      <div class="a1">
		    <ul>
			  <li>业务总数：<%=m_StatsAup %></li>
			  <li>审核通过：<%=m_StatsPass %></li>
			  <li>审核驳回：<%=m_StatsBak %></li>
			  <li>尚未处理：<%=m_StatsAudit%></li>
			</ul>
			<div class="clr"></div>
		  </div>
	    </div>
	  </div>
      <div class="time_01 line_t">
        <%--<div class="a1">下午</div>--%>
        <div class="a2">
<SCRIPT language=javascript>
//时分代码
function CurentTime(){  
     var now = new Date();  
     var hh = now.getHours();  
     var mm = now.getMinutes();  
     var ss = now.getTime() % 60000;  
     ss = (ss - (ss % 1000)) / 1000;  
     var clock = hh+':';  
     if (mm < 10) clock += '0';  
     clock += mm;//clock += mm+':';
     //if (ss < 10) clock += '0';
     //clock += ss;
     return(clock);  
}
function refreshCalendarClock(){ 
document.all.calendarClock4.innerHTML = CurentTime();  
}
document.write('<span id="calendarClock4"></span>'); 
setInterval('refreshCalendarClock()',1000);
</SCRIPT>
        </div>
        <div class="a3"><%=m_CurDate%></div>
      </div>
      <div class="point_01 line_t">
        <div class="part">操作提示</div>
        <div class="process"><span>发起<br />申请</span><b></b><span>审核<br />处理</span><b></b><span>打印<br />发证</span></div>
        <div class="content">
            <asp:Literal ID="LiteralHelpDocs" runat="server" EnableViewState="false" />
（1）操作手册：<br /><a target="_blank" href="Files/SysHelp.pdf">《通辽市人口计生综合在线办理服务系统操作手册》PDF版</a><br />
（2）插件安装及登录说明：<br /><a target="_blank" href="Files/SysHelpForAdmin.pdf">《通辽市人口计生综合在线办理服务系统插件安装及系统登录操作说明书》PDF版</a><br />
（3）当前最新客户端版本号 1.0.10：<br /><a target="_blank" href="http://222.74.115.226:8083/">最新版本客户端程序下载地址(系统引导页)</a><br />
（4）客服问题提交：<br /><a target="_blank" href="http://kefu.wsxa.com/CustomerWorkOrder.aspx?CustomerKey=3F81E52536AE7FC8">日常使用过程中发现的问题、建议等可单击此处提交；系统客服人员定期回复汇报处理情况。</a><br />
        </div>
      </div>
      <div class="copyright_01 line_t">&copy; 2005-2016, 西安网是科技发展有限公司</div>
    </td>
  </tr>
</table>
</div>
</body>
</html>
