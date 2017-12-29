<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="errors.aspx.cs" Inherits="join.pms.wap.errors" %>

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
    <title>操作提示</title>   
    <link href="/styles/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css"> 

    .block_01{background: #fafafa none repeat scroll 0 0;
    /*border-bottom: 1px solid #d8d8d8;*/
    border-top: 1px solid #d8d8d8;
    padding: 60px 0 30px;}
    .errors {  margin: 0 auto;
   width: 100%;
    } 
    .errors_bg {
        padding: 15px;
        min-height:150px;
    }
    .errors_bg .title{ font-size: 14px;
    font-weight: bold;}
    .errors_bg .list{  padding: 30px 0 10px; line-height: 22px;}
    a{
        color: #e01801;
        text-decoration: none;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">

<uc1:Uc_PageTop id="Uc_PageTop1" runat="server" />
<div class="block_01">
<div class="errors">
  <div class="errors_bg">
    <asp:Literal ID="LiteralMsg" EnableViewState="false" runat="server" />
  </div>
</div>
</div>
<!--pageFooter-->
<uc2:Uc_Footer id="Uc_Footer1" runat="server"/> 
</form>
</body>
</html>

