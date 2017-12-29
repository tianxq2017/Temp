<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTjGmqy.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.EditTjGmqy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>人社局-规模企业信息</title>
    <link href="/Styles/index.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="/includes/CommSys.js" type="text/javascript"></script>
    <script language="javascript" src="/Scripts/datectrl.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列规模企业信息后；点击“提交”按钮（次月5日以前将上月情况提交）……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="100"  bgcolor="#CCFFFF">数据日期：</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
    (填报时间,该数据实际的归属月份,请选择到该数据月之内)
    </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="100"  bgcolor="#CCFFFF">
      企业名称：</td>
  <td align="left"><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu"  bgcolor="#CCFFFF">
            组织机构代码：</td>
  <td align="left"><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" Width="200px"/></td>
</tr>
  <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
      税务登记证号：</td>
        <td align="left">
    <asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" Width="200px"/></td>
   </tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            法人姓名：</td>
        <td align="left">
            <asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" Width="200px"/></td>
    </tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            性别：</td>
        <td align="left" id="TD1" runat="server"><asp:DropDownList ID="DDLFileds05" runat="server">
            <asp:ListItem>男</asp:ListItem>
            <asp:ListItem>女</asp:ListItem>
        </asp:DropDownList></td>
    </tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            身份证号：</td>
        <td align="left">
            <asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" Width="200px"/></td>
    </tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            联系电话：</td>
        <td align="left">
            <asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" Width="200px"/></td>
    </tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            经营地址：</td>
        <td align="left"><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" Width="500px"/></td>
    </tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            经济类型：</td>
        <td align="left">
            <asp:DropDownList ID="DDLFileds09" runat="server">
            <asp:ListItem>国有经济</asp:ListItem>
            <asp:ListItem>集体经济</asp:ListItem>
            <asp:ListItem>私营经济</asp:ListItem>
            <asp:ListItem>个体经济</asp:ListItem>
            <asp:ListItem>联营经济</asp:ListItem>
            <asp:ListItem>股份制经济</asp:ListItem>
            <asp:ListItem>外商投资经济</asp:ListItem>
            <asp:ListItem>港、澳、台投资经济</asp:ListItem>
        </asp:DropDownList></td>
    </tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            经营行业：</td>
        <td align="left">
            <asp:DropDownList ID="DDLFileds10" runat="server">
            <asp:ListItem>农、林、牧、渔业</asp:ListItem>
            <asp:ListItem>采矿业</asp:ListItem>
            <asp:ListItem>制造业</asp:ListItem>
            <asp:ListItem>电力、热力、燃气及水生产和供应业</asp:ListItem>
            <asp:ListItem>建筑业</asp:ListItem>
            <asp:ListItem>批发和零售业</asp:ListItem>
            <asp:ListItem>交通运输、仓储和邮政业</asp:ListItem>
            <asp:ListItem>住宿和餐饮业</asp:ListItem>
            <asp:ListItem>信息传输、软件和信息技术服务业</asp:ListItem>
            <asp:ListItem>金融业</asp:ListItem>
            <asp:ListItem>房地产业</asp:ListItem>
            <asp:ListItem>租赁和商务服务业</asp:ListItem>
            <asp:ListItem>科学研究和技术服务业</asp:ListItem>
            <asp:ListItem>水利、环境和公共设施管理业</asp:ListItem>
            <asp:ListItem>居民服务、修理和其他服务业</asp:ListItem>
            <asp:ListItem>教育</asp:ListItem>
            <asp:ListItem>卫生和社会工作</asp:ListItem>
            <asp:ListItem>文化、体育和娱乐业</asp:ListItem>
            <asp:ListItem>公共管理、社会保障和社会组织</asp:ListItem>
            <asp:ListItem>国际组织</asp:ListItem>
        </asp:DropDownList></td>
    </tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            单位性质：</td>
        <td align="left">
            <asp:DropDownList ID="DDLFileds11" runat="server">
            <asp:ListItem>机关法人</asp:ListItem>
            <asp:ListItem>机关非法人</asp:ListItem>
            <asp:ListItem>企业法人</asp:ListItem>
            <asp:ListItem>企业非法人</asp:ListItem>
            <asp:ListItem>社会团体法人</asp:ListItem>
            <asp:ListItem>社会团体非法人</asp:ListItem>
            <asp:ListItem>事业法人</asp:ListItem>
            <asp:ListItem>工会法人</asp:ListItem>
            <asp:ListItem>个体工商户</asp:ListItem>
            <asp:ListItem>民办非企业单位</asp:ListItem>
            <asp:ListItem>其他机构</asp:ListItem>
        </asp:DropDownList></td>
    </tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            注册资本：</td>
        <td align="left"><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" Width="200px"/></td>
    </tr>
    <tr class="zhengwen">
        <td align="right" bgcolor="#ccffff" class="zhengwenjiacu" height="25">
            从业人数：</td>
        <td align="left">
            <asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" Width="200px"/></td>
    </tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" OnClick="btnAdd_Click" CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>
