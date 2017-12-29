<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainDesk.aspx.cs" Inherits="join.pms.web.MainDesk" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>个人桌面</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312"/>
<link href="/css/right.css" type="text/css" rel="stylesheet">
<link href="/css/index01.css" rel="stylesheet" type="text/css">
<script language="javascript" src="/includes/huadongmen.js" type="text/javascript"></script>
</head>
<body >
<form id="form1" runat="server">
<!--[if lte IE 6]>
<div class="ie6_bg">
  <div class="ie6 clearfix">
    <ul>
	  <li><a href="http://rj.baidu.com/soft/detail/23253.html" title="IE浏览器" target="_blank"></a></li>
	  <li><a href="http://www.firefox.com.cn/" title="火狐浏览器" target="_blank"></a></li>
	  <li><a href="http://rj.baidu.com/soft/detail/14744.html?ald" title="谷歌浏览器" target="_blank"></a></li>
	  <li><a href="http://www.maxthon.cn/" title="遨游浏览器" target="_blank"></a></li>
	  <li><a href="http://chrome.360.cn/" title="360急速浏览器" target="_blank"></a></li>
	  <li><a href="http://se.360.cn/index_main.html" title="360安全浏览器" target="_blank"></a></li>
	</ul>
  </div>
</div>
<![endif]-->
<!--导航信息-->
<div class="mbx">当前位置：起始页</div>
<!--正文信息-->
<asp:Literal ID="LiteralBiz" runat="server" EnableViewState="false" />
<asp:Literal ID="LiteralNHS1" runat="server" EnableViewState="false" />
<asp:Literal ID="LiteralNHS2" runat="server" EnableViewState="false" />

<div class="block_index" style="display:<%=none1 %>;">
    <!--div class="index_l" style="display:none">
        <div class="index_01">
            <div class="index_t"><p>共享数据</p></div>
            <div class="index_b">
                <div class="total">
                    <p class="title">本系统数据共享总量：</p>
                    <p class="number">201</p>
                </div>
                <div class="list">
                    <ul>
                        <li>卫计局：<b>80</b></li>
                        <li>民政局：<b>11</b></li>
                        <li>公安局：<b>20</b></li>
                        <li>人社局：<b>40</b></li>
                        <li>教体局：<b>50</b></li>
                        <li>统计局：<b>60</b></li>
                    </ul>
                </div>
            </div>
        </div>
    </div-->
    <div>
        <div class="index_02">
            <div class="index_t"><p><asp:Literal runat="server" ID="LiteraAre" EnableViewState="false"></asp:Literal>业务数据</p></div>
            <div class="index_b">
                <div class="index_b_l">
                    <table border="0" cellspacing="0" cellpadding="0">
	                    <tr>
		                    <th>业务名称</th>
		                    <th>办结数</th>
		                    <th>预约数</th>
	                    </tr>
	                    <tr>
		                    <td>婚育健康服务证办理</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ015001" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ015002" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>一孩生育登记</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010101" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010102" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>二孩生育登记</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010201" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010202" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>再生育服务审批</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ012201" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ012202" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>婚育情况证明</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ011001" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ011002" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>《独生子女父母光荣证》审批</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010801" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010802" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>终止妊娠申请审批</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ011101" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ011102" EnableViewState="false"></asp:Literal></td>
	                    </tr>
                    </table>
                </div>
                <div class="index_b_r">
                    <table border="0" cellspacing="0" cellpadding="0">
	                    <tr>
		                    <th>业务名称</th>
		                    <th>办结数</th>
		                    <th>预约数</th>
	                    </tr>
	                    <tr>
		                    <td>计生家庭奖励扶助对象申报审批</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010301" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010302" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>计生家庭特别扶助对象申请审批</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010401" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010402" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>“少生快富”工程申请审批 </td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010501" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010502" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>结扎家庭奖励申请审批</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010601" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010602" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>“一杯奶”受益对象申请审批</td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010701" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010702" EnableViewState="false"></asp:Literal></td>
	                    </tr>
	                    <tr>
		                    <td>《流动人口婚育证明》申请审批 </td>
		                    <td><b><asp:Literal runat="server" ID="litBJ010901" EnableViewState="false"></asp:Literal></b></td>
		                    <td><asp:Literal runat="server" ID="litBJ010902" EnableViewState="false"></asp:Literal></td>
	                    </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="clr"></div>
<div class="clr10"></div>
</div>

<!--div class="block_index">
    <div class="index_03">
               
    </div>
</div>
<div class="clr10"></div-->

<%-- style="display:<%=none2 %>;"--%>
<div class="part_12_02" style="display:none;">
  <asp:Literal ID="LiteralCms" runat="server" EnableViewState="false"></asp:Literal>
<div class="clr10"></div>
</div>

<!--div class="block_index">
    <div class="index_04">
        <ul>
            <li class="a1"><b>8888</b><span>全县总户数</span></li>
            <li class="a2"><b>88888</b><span>全县总人口</span></li>
            <li class="a3"><b>3333</b><span>全县女性总数</span></li>
            <li class="a4"><b>2222</b><span>婚育健康全程<br />服务证已发放</span></li>
            <li class="a5"><b>4444</b><span>参与保健妇女数</span></li>
            <li class="a6"><b>555</b><span>参与保健儿童数</span></li>
        </ul>
    </div>
</div-->

<%--<div class="block_03">
	<div class="area_t">统计信息</div>
	<div class="area_l">
	    <div class="part_05">
	        <ul>
	            <li class="a1"><b>268963</b><span>全县总户数</span></li>
	            <li class="a2"><b>268963</b><span>全县总人口</span></li>
	            <li class="a3"><b>268963</b><span>全县女性总数</span></li>
	            <li class="a4"><b>268963</b><span>婚育健康全程<br />服务证已发放</span></li>
	        </ul>
	    </div>
	</div>
	<div class="area_r">
	    <div class="part_06">
		    <ul>
	            <li><span>一孩生育服务登记：</span><b>15466</b></li>
	            <li><span>二孩生育服务登记：</span><b>15466</b></li>
	            <li><span>再生育服务审批：</span><b>15466</b></li>
	            <li><span>流动人口婚育证明：</span><b>15466</b></li>
	            <li><span>户籍人口婚育证明：</span><b>15466</b></li>
	            <li><span>独生子女父母光荣证：</span><b>15466</b></li>
	            <li><span>婚前医学检查证明：</span><b>15466</b></li>
	            <li><span>医疗机构执业许可证：</span><b>15466</b></li>
	            <li><span>放射诊疗卫生许可证：</span><b>15466</b></li>
	            <li><span>公共场所卫生许可证：</span><b>15466</b></li>
	            <li><span>生活饮用水供水单位卫生许可证：</span><b>15466</b></li>
	            <li><span>流动人口婚育情况证明：</span><b>15466</b></li>
	        </ul>
	    </div>
	</div>
	<div class="clr"></div>
</div>--%>
<%--<div class="block_01">
	<div class="area_l">
	    <div class="index_01"><a href="#"><i>8</i><span>我的消息</span></a></div>
	</div>
	<div class="area_c">
	    <div class="index_01 index_01b"><a href="#"><i>521</i><span>通知公告</span></a></div>
	</div>
	<div class="area_r">
	    <div class="index_01 index_01c"><a href="#"><i>23</i><span>镇办信息</span></a></div>
	</div>
	<div class="clr"></div>
</div>--%>
<%--<div class="part_12_01">
  <ul>
    <li class="a1"><a href="#" onclick="parent.leftFrame.ShowSubMenu(2)">
      <p class="pic clearfix"><b></b><span>共享数据</span></p>
      <p class="content">数据共享是各职能部门所提交的基础数据，让在不同地方使用不同计算机的不同用户能够查看他人数据并进行各种操作...</p></a>
    </li>
    <li class="a2"><a href="#" onclick="parent.leftFrame.ShowSubMenu(4)">
      <p class="pic clearfix"><b></b><span>差异数据</span></p>
      <p class="content">依据比对规则，将各职能部门所提交的共享数据和人口基础数据进行萃取、清洗、比对分析后，形成的存在差异的数据...</p></a>
    </li>
    <li class="a3"><a href="#" onclick="parent.leftFrame.ShowSubMenu(3)">
      <p class="pic clearfix"><b></b><span>标准数据</span></p>
      <p class="content">各职能部门所提交的共享数据和人口基础数据进行萃取、清洗、比对分析后，形成的无差异或差异不较小的数据...</p></a>
    </li>
    <li class="a4"><a href="#" onclick="parent.leftFrame.ShowSubMenu(5);parent.leftFrame.ShowSubMenu(6);">
      <p class="pic clearfix"><b></b><span>网络硬盘</span></p>
      <p class="content">网盘，又称网络U盘、网络硬盘，是由共享平台提供的在线存储服务，向用户提供文件的存储、访问、备份...</p></a>
    </li>
  </ul>
  <div class="clr"></div>
</div>--%>
<%--<div class="block_02">
	<div class="area_l">
	    <div class="part_04">
	        <div class="part_t"><span><a href="#">更多 &gt;&gt;</a></span><b>通知公告</b></div>
	        <div class="part_c">
		        <ul>
		            <li><span class="time">【2014-11-25】</span><a href="#" title="标题">信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题</a></li>
		            <li><span class="time">【2014-11-25】</span><a href="#" title="标题">信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题</a></li>
		            <li><span class="time">【2014-11-25】</span><a href="#" title="标题">信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题</a></li>
		            <li><span class="time">【2014-11-25】</span><a href="#" title="标题">信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题</a></li>
		            <li><span class="time">【2014-11-25】</span><a href="#" title="标题">信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题</a></li>
		        </ul>
	        </div>
	    </div>
	</div>
	<div class="area_r">
	    <div class="part_04 part_04b">
	        <div class="part_t"><span><a href="#">更多 &gt;&gt;</a></span><b>镇办信息</b></div>
	        <div class="part_c">
		        <ul>
		            <li><span class="time">【城关镇】</span><a href="#" title="标题">信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题</a></li>
		            <li><span class="time">【城关镇】</span><a href="#" title="标题">信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题</a></li>
		            <li><span class="time">【城关镇】</span><a href="#" title="标题">信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题</a></li>
		            <li><span class="time">【城关镇】</span><a href="#" title="标题">信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题</a></li>
		            <li><span class="time">【城关镇】</span><a href="#" title="标题">信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题信息标题</a></li>
		        </ul>
	        </div>
	    </div>
	</div>
	<div class="clr"></div>
</div>--%>
<!--div class="block_01" style="display: none">
	<div class="area_l">
	    <div class="part_01">
	    
	    </div>
	</div>
	<div class="area_c">
	    <div class="part_02">
	    </div>
	</div>
	<div class="area_r">
	    <div class="part_03">
	    <aspLiteral ID="LiteralBaobiao" runat="server" EnableViewState="false" />
	    </div>
	</div>
	<div class="clr"></div>
</div-->
<div class="part_12_03" style="display:<%=none3 %>;">
  <div class="part_title">
    <p>统计概览</p>
  </div>
  <div class="part_12_03a">
    <ul>
    <asp:Literal ID="LiteralTotalStats" runat="server" EnableViewState="false"></asp:Literal>
    </ul>
    <div class="clr"></div>
  </div>
  <div class="part_12_03b">
    <p>
      <asp:Literal ID="LiteralStats" runat="server" EnableViewState="false"></asp:Literal>
    </p>
  </div>
</div>
</form>
<br/>
</body>
</html>
