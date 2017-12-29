<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizDocs.aspx.cs" Inherits="join.pms.wap.SceneSvrs.BizDocs" %>
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
<title>证照上传</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
<script language="javascript" src="/Scripts/checkDocs.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>
    <div class="block">
  <!--申请步骤 -->
  <div class="flow_pic clearfix">
    <ul>
	  <li><p><b>1</b>填写申请资料</p></li>
	  <li class="on"><p><b>2</b>提交所需证件</p></li>
	  <li><p><b>3</b>申请成功</p></li>
	</ul>
  </div>
  
  <!--备注 -->
  <div class="part_03">
	<p class="title">备注说明</p>
    <p class="sum">
	1、为了节约您取证时的宝贵时间，请尽量使用“本地上传”电子版证件；
	2、取证时，请携带原件；
	3、仅支持.jpg|.gif|.bmp|.png格式的图片。
	</p>
  </div>
  <div class="clr10"></div>
  
  <!--相关内容 -->
  <div class="part_05">
    <div class="part_name">提交所需证件电子版</div>	
	<asp:Literal ID="LiteralBizCategoryLicense" runat="server" EnableViewState="false"></asp:Literal>
	<div class="check"><asp:CheckBox ID="cbOk" runat="server" /> 承诺：本人保证以上情况及提供的相关材料属实。如有不实，愿意承担由此引起的相应法律责任，并将记入国家诚信系统。</div>
	<div class="part_button"><asp:Button ID="btnAdd" runat="server" Text="确认提交"  OnClientClick="return check();" OnClick="btnAdd_Click"></asp:Button></div>
    </div>
</div>
     <!-- 页面参数 -->
<input type="hidden" name="txtAreaCode" id="txtAreaCode" runat="server" style="display:none;"/>
<input type="hidden" name="txtRegAreaCodeA" id="txtRegAreaCodeA" runat="server" style="display:none;"/>
<input type="hidden" name="txtRegAreaCodeB" id="txtRegAreaCodeB" runat="server" style="display:none;"/>
<input type="hidden" name="txtIsInnerArea" id="txtIsInnerArea" runat="server" style="display:none;"/>

<input type="hidden" name="txtBizCNum" id="txtBizCNum" runat="server" style="display:none;"/>
<input type="hidden" name="txtBizGNum" id="txtBizGNum" runat="server" style="display:none;"/>
      <uc2:Uc_Footer id="Uc_Footer1" runat="server"/>
    </form>
</body>
</html>
