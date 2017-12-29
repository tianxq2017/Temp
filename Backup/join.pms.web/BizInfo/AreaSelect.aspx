<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaSelect.aspx.cs" Inherits="join.pms.web.BizInfo.AreaSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择行政区划</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/right.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" src="/Scripts/SearchData.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">

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

</div>

<!-- 页面参数 -->
<input type="hidden" name="txtCurrentClass" id="txtCurrentClass" runat="server" style="display:none;"/>
<input type="hidden" name="txtW" id="txtW" runat="server" style="display:none;"/>

<input type="hidden" name="txtSelCodeI" id="txtSelCodeI" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelTextI" id="txtSelTextI" runat="server" style="display:none;"/>

<input type="hidden" name="txtSelCodeII" id="txtSelCodeII" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelTextII" id="txtSelTextII" runat="server" style="display:none;" value="内蒙古自治区 &gt; 通辽市"/>

<input type="hidden" name="txtSelCodeIII" id="txtSelCodeIII" runat="server" style="display:none;" />
<input type="hidden" name="txtSelTextIII" id="txtSelTextIII" runat="server" style="display:none;" />

<input type="hidden" name="txtSelCodeIV" id="txtSelCodeIV" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelTextIV" id="txtSelTextIV" runat="server" style="display:none;"/>

<input type="hidden" name="txtSelCodeV" id="txtSelCodeV" runat="server" style="display:none;"/>
<input type="hidden" name="txtSelTextV" id="txtSelTextV" runat="server" style="display:none;"/>

<input type="hidden" name="txtSelectArea" id="txtSelectArea" runat="server" style="display:none;" value="汉中市勉县"/>

<input type="hidden" name="txtUrlParams" id="txtUrlParams" value="" runat="server" style="display:none;"/>

<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<div class="column_00">  
  <div class="column_c">
	<div class="area_list">
	  <div class="area_list_t">
	  <div style=" width:590px;">
	    <div class="area clearfix">
		  <ul>
		   <li>
		    <asp:Literal ID="LiteralIII" runat="server"></asp:Literal>
			</li>
		    <li id="ClassIV">
			<select size="4" name="SelClassI" id="SelClassI" style="width:147px;height:214px;" ><option value="">请选择所在镇街</option></select>
			</li>
		    <li id="ClassV" class="off">
			<select size="4" name="SelClassII" id="SelClassII" style="width:147px;height:214px;" ><option value="">请选择所在社区</option></select>
			</li>
		  </ul>
		</div>
		</div>
	  </div>
	  <div class="area_list_c">
		<b>您当前选择的是：</b><b id="SelectTexts">内蒙古自治区 &gt; 通辽市 &gt; </b>
	    <div id="SelectCodes" style="display:none;"></div><span><input type="button" name="butNextPage" id="butNextPage" value="下一步" onclick="GotoSelectDo();"/></span>
	    
	  </div>
    </div>
  </div>
</div>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>
