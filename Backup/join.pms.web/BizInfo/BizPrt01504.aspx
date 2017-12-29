<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizPrt01504.aspx.cs" Inherits="join.pms.web.BizInfo.BizPrt01504" %>
<%@ Register Src="../userctrl/BizPrt0150Menu.ascx" TagName="BizPrt0150Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>孕产妇住院分娩卡、育龄妇女健康检查、避孕措施知情选择服务记录《婚育健康服务证》</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link href="/BizInfo/css/printPaper0150.css" rel="stylesheet" type="text/css" />
    <script src="/BizInfo/scripts/a0.js" type="text/javascript"></script>
</head>
<body>
<form id="form1" runat="server">
<uc1:BizPrt0150Menu ID="BizPrt0150Menu" runat="server" />
<div class="print">
  <asp:Literal ID="LiteralBizInfo" runat="server" EnableViewState="false" />
  <p><img src="/BizInfo/images/info/04.gif" /></p>
</div>
</form>
</body>
</html>