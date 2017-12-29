<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchPerson.aspx.cs" Inherits="AreWeb.OnlineCertificate.PopInfo.SearchPerson" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>人口信息综合查询</title>
    <link href="/Styles/index.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="/Scripts/AsynQuery.js" type="text/javascript"></script>
    <!--Waiting for query start-->
    <link href="/Styles/QueryWaiting.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="/Scripts/AsynLoading.js"></script>
    <script language="javascript" src="/Scripts/datectrl.js" type="text/javascript"></script>
    <!--Waiting for query end-->
</head>
<body>
<div id="query_hint" class="query_hint" style="display:none;">
    <img src="/images/Loading.gif" alt="请等待" />正在查询，请稍等……
</div>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请输入查询条件后，点击“查询”按钮，查询结果最多显示12条，如没有找到想要的信息，可缩小查询范围继续查询，直到找到为止……<br/>查询结果出现后，点击人员姓名所对应的身份证号，可查看该人员的所有关联信息……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<div class="form_a">
<p ><b>&nbsp;&nbsp;选择查询条件：</b></p>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<div class="form_table">
<table width="0" border="0" cellspacing="0" cellpadding="0">
   <tr> 
    <th width="80">姓名：</th>
    <td class="text"><asp:TextBox ID="txtName" runat="server" EnableViewState="False" MaxLength="25" Width="80px"/></td>
    <th width="100">身份证号：</th>
    <td width="170"><asp:TextBox ID="txtCid" runat="server" EnableViewState="False" MaxLength="18" Width="150px"/></td>
    <td><asp:Button ID="btnAdd" runat="server" Text="· 查询 ·" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button></td>
  </tr>  
  </table>	    
</div></div>  
<div class="form_a">
  <p><b>&nbsp;&nbsp;查询结果展示：</b></p>
  <table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
  <br/>
  <div class="form_table_list">
  <div id="pBaseInfo" style="display:block;">
  <asp:Literal ID="LiteralData" runat="server" EnableViewState="false" />
  </div>
  <br/>
  <div id="pDetailsInfo"></div>

</div></div>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>
