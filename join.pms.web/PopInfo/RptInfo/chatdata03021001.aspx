<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03021001.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03021001" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>婚前保健情况年报表</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/index.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" src="/includes/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
    <script language="javascript" src="/Scripts/datectrl.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列信息后；点击“提交”按钮（次月5日以前将上月情况提交）……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display:none;"/>
<!--Start-->
<div id="ec_bodyZone">

<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<br/>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
    <p style="font-size:18px; text-align:center; font-weight:bold">婚前保健情况年报表（<asp:DropDownList runat="server" ID="txt_RptTime" ></asp:DropDownList>）</p><p><%=str_AreaName%></p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
        <tr style="text-align:center; background-color:#cccccc">
          <td rowspan="4">单位</td>
          <td colspan="6">结婚登记与婚前医学保健情况</td>
          <td colspan="14">检出疾病分类</td>
          <td colspan="2" rowspan="3">对影响婚育疾病的医学指导意见</td>
          <td rowspan="4">操作</td>
        </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td colspan="2" rowspan="2">结婚登记</td>
            <td colspan="2" rowspan="2">婚前医学检查</td>
            <td colspan="2" rowspan="2">婚前卫生咨询</td>
            <td colspan="2" rowspan="2">检出疾病人数</td>
            <td colspan="4">指定传染病</td>
            <td colspan="2" rowspan="2">严重遗传病</td>
            <td colspan="2" rowspan="2">精神病</td>
            <td colspan="2" rowspan="2">生殖系统病</td>
            <td colspan="2" rowspan="2">内科系统病</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td colspan="2">总计</td>
            <td colspan="2">性病</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>男</td>
            <td>女</td>
            <td>男</td>
            <td>女</td>
            <td>男</td>
            <td>女</td>
            <td>男</td>
            <td>女</td>
            <td>男</td>
            <td>女</td>
            <td>男</td>
            <td>女</td>
            <td>男</td>
            <td>女</td>
            <td>男</td>
            <td>女</td>
            <td>男</td>
            <td>女</td>
            <td>男</td>
            <td>女</td>
            <td>男</td>
            <td>女</td>
          </tr>
</thead>
          <tr style="text-align:center;">
            <td>合计</td>
            <%for (int i = 0; i < arrNum.Length; i++)
              { %>
            <td><%=arrNum[i] %></td>
            <%} %>
            <td>--</td>
          </tr>
        
    <asp:Repeater ID="rep_Data" runat="server" OnItemCommand="rep_Data_ItemCommand">
      <ItemTemplate>
        <tbody>
            <tr style="text-align:center;">
                <td><%# Eval("AreaName")%></td>
                <td><%# Eval("Fileds01")%></td>
                <td><%# Eval("Fileds02")%></td>
                <td><%# Eval("Fileds03")%></td>
                <td><%# Eval("Fileds04")%></td>
                <td><%# Eval("Fileds05")%></td>
                <td><%# Eval("Fileds06")%></td>
                <td><%# Eval("Fileds07")%></td>
                <td><%# Eval("Fileds08")%></td>
                <td><%# Eval("Fileds09")%></td>
                <td><%# Eval("Fileds10")%></td>
                <td><%# Eval("Fileds11")%></td>
                <td><%# Eval("Fileds12")%></td>
                <td><%# Eval("Fileds13")%></td>
                <td><%# Eval("Fileds14")%></td>
                <td><%# Eval("Fileds15")%></td>
                <td><%# Eval("Fileds16")%></td>
                <td><%# Eval("Fileds17")%></td>
                <td><%# Eval("Fileds18")%></td>
                <td><%# Eval("Fileds19")%></td>
                <td><%# Eval("Fileds20")%></td>
                <td><%# Eval("Fileds21")%></td>
                <td><%# Eval("Fileds22")%></td>
               <td>
                    <asp:LinkButton ID="lbt_Update" runat="server" CommandName="Update" CommandArgument='<%#Eval("CommID") %>' Text="编辑" /> | 
                    <asp:LinkButton ID="lbt_Delete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("CommID") %>' Text="删除" />
               </td>
            </tr>           
          </tbody>
    </ItemTemplate>  
    </asp:Repeater>
    </table>
     <p>负责人：<asp:TextBox ID="txt_SldHeader" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;填表人：<asp:TextBox ID="txt_SldLeader" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;填报日期：<input id="txt_OprateDate" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" /></p>
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
</td></tr></table>
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellspacing="3" cellpadding="1" class="zhengwen">
<tr class="zhengwen">
            <td align="right" width="201" bgcolor="#cccccc">
                单位：</td>
            <td colspan="6" bgcolor="#cccccc"><asp:Label ID="lbl_DataAreaSel" runat="server"></asp:Label>&nbsp;<asp:DropDownList ID="dr_DataAreaSel" runat="server" OnSelectedIndexChanged="dr_DataAreaSel_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
          </tr>
          <tr class="zhengwen">
            <td rowspan="6" align="right" bgcolor="#FFFFCC" >结婚登记与婚前医学保健情况</td>
            <td colspan="3" rowspan="2" align="right" bgcolor="#FFFFCC" >结婚登记</td>
            <td width="26" align="right" bgcolor="#FFFFCC" >男：</td>
            <td width="250" ><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td width="50%" ><asp:Label ID="txtLabel01" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC" >女：</td>
            <td ><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel02" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td colspan="3" rowspan="2" align="right" bgcolor="#FFFFCC" >婚前医学检查</td>
            <td align="right" bgcolor="#FFFFCC" >男：</td>
            <td ><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel03" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC" >女：</td>
            <td ><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel04" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td colspan="3" rowspan="2" align="right" bgcolor="#FFFFCC" >婚前卫生咨询</td>
            <td align="right" bgcolor="#FFFFCC" >男：</td>
            <td ><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel05" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#FFFFCC" >女：</td>
            <td ><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel06" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td rowspan="14" align="right" bgcolor="#CCCCCC">检出疾病分类</td>
            <td colspan="3" rowspan="2" align="right" bgcolor="#CCCCCC">检出疾病人数</td>
            <td align="right" bgcolor="#CCCCCC" >男：</td>
            <td ><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
           <td ><asp:Label ID="txtLabel07" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC" >女：</td>
            <td ><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel08" runat="server"></asp:Label></td>
          </tr>
          
          <tr class="zhengwen">
            <td rowspan="4" align="right" bgcolor="#CCCCCC">指定传染病</td>
            <td colspan="2" rowspan="2" align="right" bgcolor="#CCCCCC">总计</td>
            <td align="right" bgcolor="#CCCCCC" >男：</td>
            <td ><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel09" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC" >女：</td>
            <td ><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel10" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td colspan="2" rowspan="2" align="right" bgcolor="#CCCCCC">性病</td>
            <td align="right" bgcolor="#CCCCCC" >男：</td>
            <td ><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel11" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC" >女：</td>
            <td ><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel12" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td colspan="3" rowspan="2" align="right" bgcolor="#CCCCCC">严重遗传病</td>
            <td align="right" bgcolor="#CCCCCC" >男：</td>
            <td ><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel13" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC" >女：</td>
            <td ><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel14" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td colspan="3" rowspan="2" align="right" bgcolor="#CCCCCC">精神病</td>
            <td align="right" bgcolor="#CCCCCC" >男：</td>
            <td ><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel15" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC" >女：</td>
            <td ><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel16" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td colspan="3" rowspan="2" align="right" bgcolor="#CCCCCC">生殖系统病</td>
            <td align="right" bgcolor="#CCCCCC" >男：</td>
            <td ><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel17" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC" >女：</td>
            <td ><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel18" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td colspan="3" rowspan="2" align="right" bgcolor="#CCCCCC">内科系统病</td>
            <td align="right" bgcolor="#CCCCCC" >男：</td>
            <td ><asp:TextBox ID="txtFileds19" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel19" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td align="right" bgcolor="#CCCCCC" >女：</td>
            <td ><asp:TextBox ID="txtFileds20" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel20" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td rowspan="2" align="right" bgcolor="#FFFFCC">对影响婚育疾病的医学指导意见</td>
            <td colspan="4" align="right" bgcolor="#FFFFCC">男：</td>
            <td><asp:TextBox ID="txtFileds21" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel21" runat="server"></asp:Label></td>
          </tr>
          <tr class="zhengwen">
            <td colspan="4" align="right" bgcolor="#FFFFCC">女：</td>
            <td><asp:TextBox ID="txtFileds22" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
            <td ><asp:Label ID="txtLabel22" runat="server"></asp:Label></td>
          </tr>
    </table>
</td></tr></table>

    <%=js_value %>
    <%=js_value2 %>
 <!-- LFileds_type 与 LFileds_txt成对-->
    <!-- LFileds_type判断值类型  0，字符不判断；1，数字或小数；-->
    <asp:Label ID="LFileds_type" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    <!-- LFileds_txt判断值类型有误时提示标题-->
    <asp:Label ID="LFileds_txt" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    
<table width="100%" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·"  CssClass="submit6" OnClick="btnAdd_Click"></asp:Button>
<asp:HiddenField ID="hd_IsUp" runat="server"/>
<asp:HiddenField ID="hd_upId" runat="server"/>
<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>

</div>
    </form>
</body>
</html>
