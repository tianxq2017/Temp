<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizCategories.aspx.cs" Inherits="join.pms.wap.SceneSvrs.BizCategories" %>
<%@ Register Src="/userctrl/Uc_PageTop.ascx" TagName="Uc_PageTop" TagPrefix="uc1" %>
<%@ Register Src="/userctrl/Uc_Footer.ascx" TagName="Uc_Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta name="apple-mobile-web-app-capable" content="yes" />
<meta name="apple-mobile-web-app-status-bar-style" content="black" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0, maximum-scale=1.0,user-scalable=yes" />
<!--<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0 , maximum-scale=1.0, user-scalable=0">-->
<meta name="generator" content="bd-mobcard" />
<meta name="format-detection" content="telephone=yes" />
<meta http-equiv="content-type" content="application/vnd.wap.xhtml+xml;charset=UTF-8"/>
<title>业务介绍</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ShowSQ()
        {
           var check= document.getElementById("ckTL").checked;
           if(check){document.getElementById("divSQ").style.display="";}
           else{document.getElementById("divSQ").style.display="none";}
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
 <uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>

<div class="block">
  <!--备注 -->
 <!-- <div class="column_00">
	<div class="column_t">
      <p class="column_title">
	    <span class="ico"><img src="/images/ico/biz0101.png"></span>
		<span class="title_bg">
		  <span class="title">独生子女父母光荣证</span>
		  <!--<span class="crumb"><a href="/">首页</a><b>&gt;</b>业务申请</span> -->
		 <!--</span>
	  </p> 
	</div>
  </div>
  <div class="clr10"></div> -->
  
  <!--备注 -->
  <div class="part_03">
	<p class="title">办理须知</p>
    <p class="sum">
	请认真阅读以下须知，符合条件的请继续申请。
	</p>
  </div>
  <div class="clr10"></div>
  
  <!--相关内容 -->
  <div class="part_05">
	<asp:Literal ID="LiteralCategoriesInfo" runat="server" EnableViewState="false"></asp:Literal> 	
  </div>
</div>

<uc2:Uc_Footer id="Uc_Footer1" runat="server"/>
    </form>
</body>
</html>
