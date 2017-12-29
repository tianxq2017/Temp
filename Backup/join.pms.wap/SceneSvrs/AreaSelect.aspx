<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaSelect.aspx.cs" Inherits="join.pms.wap.SceneSvrs.AreaSelect" %>
<%@ Register Src="/userctrl/Uc_PageTop.ascx" TagName="Uc_PageTop" TagPrefix="uc1" %>
<%@ Register Src="/userctrl/Uc_Footer.ascx" TagName="Uc_Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta name="apple-mobile-web-app-capable" content="yes" />
<meta name="apple-mobile-web-app-status-bar-style" content="black" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0, maximum-scale=1.0,user-scalable=yes" />
<!--<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0 , maximum-scale=1.0, user-scalable=0">-->
<meta name="generator" content="bd-mobcard" />
<meta name="format-detection" content="telephone=yes" />
<meta http-equiv="content-type" content="application/vnd.wap.xhtml+xml;charset=UTF-8"/>
<title>区划选择</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" /> 
<script language="javascript" src="/Scripts/SearchData.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
 <uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>

<div class="block">
  <!--备注 -->
  <div class="part_03">
	<p class="title">操作帮助</p>
    <p class="sum">
        <%if (m_BizCode == "0101")%>
    <%{%>
通过网上申请办理的用户，申请人须为申请对象本人，请遵循以下原则选择申请地址，否则，由此造成登记不及时由本人承担。<br />
一、夫妇双方户籍为旗内同镇同村的，申请地选择户籍地。<br />
二、夫妇双方户籍为旗内同镇异村、异镇异村的，若现居住地在夫妇一方的户籍地居住，受理地选择现居住地；若现居住地均不在夫妇双方户籍地，申请地选择女方户籍地址。<br />
三、夫妇双方户籍一方为旗内户籍的，申请地为旗内户籍地一方。<br />
四、夫妇双方均为旗外户籍的，申请地为现居住地。
    <%}%>
    <%else if (m_BizCode == "0102")%>
    <%{%>
通过网上申请办理的用户，申请人须为申请对象本人，请遵循以下原则选择申请地址，否则，由此造成生育服务证领取不及时由本人承担。<br />
一、夫妇双方户籍为旗内同镇同村的，申请地选择户籍地。<br />
二、夫妇双方户籍为旗内同镇异村、异镇异村的，若现居住地在夫妇一方的户籍地居住，受理地选择现居住地；若现居住地均不在夫妇双方户籍地，申请地选择女方户籍地址。<br />
三、夫妇双方户籍一方为旗外户籍的，申请地为旗内户籍地一方。
    <%}%>
    <%else if (m_BizCode == "0105")%>
    <%{%>
通过网上申请办理的用户，申请人须为申请对象本人，请遵循以下原则选择申请地址，否则，由此造成审核不及时由本人承担。<br />
一、夫妇双方户籍为旗内同镇同村的，申请地选择户籍地。<br />
二、夫妇双方户籍为旗内同镇异村、异镇异村的，若现居住地在夫妇一方的户籍地居住，受理地选择现居住地；若现居住地均不在夫妇双方户籍地，申请地选择女方户籍地址。<br />
三、夫妇一方为旗外户籍的，申请地为旗内户籍地一方。
    <%}%>
    <%else if (m_BizCode == "0107")%>
    <%{%>
通过网上申请办理的用户，申请人须为申请对象本人，申请地址选择女方现居住地。否则，由此造成审核不及时由本人承担。
    <%}%>
    <%else if (m_BizCode == "0108")%>
    <%{%>
通过网上申请办理的用户，申请人须为申请对象本人，请遵循以下原则选择申请地址，否则，由此造成审核不及时由本人承担。<br />
一、夫妇双方户籍为旗内同镇同村的，申请地选择户籍地。<br />
二、夫妇双方户籍为旗内同镇异村、异镇异村的，若现居住地在夫妇一方的户籍地居住，受理地选择现居住地；若现居住地均不在夫妇双方户籍地，申请地选择女方户籍地址。<br />
三、夫妇双方户籍一方为旗内户籍的，申请地为旗内户籍地一方。
    <%}%>
    <%else if (m_BizCode == "0122")%>
    <%{%>
通过网上申请办理的用户，申请人须为申请对象本人，请遵循以下原则选择申请地址，否则，由此造成生育服务证领取不及时由本人承担。<br />
一、夫妇双方户籍为旗内同镇同村的，申请地选择户籍地。<br />
二、夫妇双方户籍为旗内同镇异村、异镇异村的，若现居住地在夫妇一方的户籍地居住，受理地选择现居住地；若现居住地均不在夫妇双方户籍地，申请地选择女方户籍地址。<br />
三、夫妇双方户籍一方为旗外户籍的，申请地为旗内户籍地一方。
    <%}%>
    <%else%>
    <%{%>
通过网上申请办理的用户，申请人须为申请对象本人，申请地选择申请人户籍地。否则，由此造成审核不及时由本人承担。
    <%}%>
<br />通辽市卫生和计划生育委员会：0475-8235645
	
	</p>
  </div>
  <div class="clr10"></div>
  
  <!--相关内容 -->
  <div class="part_04">
    <div class="fixed">
	  <div class="title">选择现居住地</div>
	  <div class="map"><img src="/images/map.gif" alt="地图" /></div>
	  <p>内蒙古自治区</p>
	  <div class="arrow"><img src="/images/gif_01.gif" alt="箭头" /></div>
	    <!--<p>通辽市</p>
	  <div class="arrow"><img src="/images/gif_01.gif" alt="箭头" /></div> -->
	</div>
    <div class="select_bg">
     <div class="select">
	 <asp:Literal ID="LiteralIII" runat="server" EnableViewState="false"></asp:Literal>
	  </div>
	  <div class="arrow"><img src="/images/gif_01.gif" alt="箭头" /></div>
	  <div class="select">
	  <div id="ClassIV" class="SelBorder2"><select size="4" name="SelClassI" id="SelClassI" style="margin:-2px;width:174px;height:250px;padding:0px;overflow:hidden;background:#FFFFFF" ><option value="" selected="selected">请选择所在社区</option></select></div>
	  </div>
	  <div class="arrow"><img src="/images/gif_01.gif" alt="箭头" /></div>
	  <div class="select">
	 <div id="ClassV"><div class="SelBorder2"><select size="4" name="SelClassII" id="SelClassII" style="margin:-2px;width:174px;height:250px;padding:0px;overflow:hidden;background:#FFFFFF" ><option value="" selected="selected">请选择所在社区</option></select></div></div>
	  </div>
	  <div class="arrow"><img src="/images/gif_01.gif" alt="箭头" /></div>
	</div>
	<div class="text" id="SelectTexts">当前选择：内蒙古自治区 &gt; 通辽市 &gt;</div> <div id="SelectCodes" style="display:none;"></div>
	<div class="button"><input type="button" name="butNextPage" id="butNextPage" value="下 一 步" onclick="GotoSelectDo();" onmousemove="this.style.cursor='hand'" /></div>
  </div>
</div>
<div id="ClassV" style="display:none;"></div>
<!-- 页面参数 -->
<input type="hidden" name="txtCurrentClass" id="txtCurrentClass" runat="server" style="display:none;"/>
<input type="hidden" name="txtW" id="txtW" runat="server" style="display:none;"/>
<input type="hidden" name="txtBizType" id="txtBizType" runat="server" style="display:none;"/>

<input type="hidden" name="txtSelCodeI" id="txtSelCodeI" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelTextI" id="txtSelTextI" runat="server" style="display:none;"/>

<input type="hidden" name="txtSelCodeII" id="txtSelCodeII" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelTextII" id="txtSelTextII" runat="server" style="display:none;" value="内蒙古自治区 &gt; 通辽市"/>

<input type="hidden" name="txtSelCodeIII" id="txtSelCodeIII" runat="server" style="display:none;" />
<input type="hidden" name="txtSelTextIII" id="txtSelTextIII" runat="server" style="display:none;" value="内蒙古自治区 &gt; 通辽市 &gt; 库伦旗" />

<input type="hidden" name="txtSelCodeIV" id="txtSelCodeIV" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelTextIV" id="txtSelTextIV" runat="server" style="display:none;"/>

<input type="hidden" name="txtSelCodeV" id="txtSelCodeV" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelTextV" id="txtSelTextV" runat="server" style="display:none;"/>

<input type="hidden" name="txtSelectArea" id="txtSelectArea" runat="server" style="display:none;" value="汉中市勉县库伦旗"/>

<uc2:Uc_Footer id="Uc_Footer1" runat="server"/>
    </form>
</body>
</html>
