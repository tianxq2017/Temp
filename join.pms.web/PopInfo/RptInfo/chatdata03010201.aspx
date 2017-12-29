<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03010201.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03010201" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>人口动态信息报告单二填报</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/index.css" type="text/css" rel="stylesheet"/>
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
    <p style="font-size:18px; text-align:center; font-weight:bold"><input id="txt_RptTime" readonly="readonly"  size="10" onclick="JTC.setday({format:'yyyy年MM月', readOnly:true})" runat="server" />份人口动态信息报告单<二>（存根）</p>
    <p><%=str_AreaName%></p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
           <tr style="text-align:center; background-color:#cccccc">
            <td colspan="11">生育情况</td>
            <td colspan="6">避孕节育情况</td>
            <td rowspan="4">操作</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">组别</td>
            <td rowspan="3">妇女姓名</td>
            <td rowspan="3">身份证号码</td>
            <td rowspan="3">丈夫姓名</td>
            <td colspan="7">孩子情况</td>
            <td rowspan="3">组别</td>
            <td rowspan="3">姓名</td>
            <td rowspan="3">身份证号码</td>
            <td rowspan="3">措施日期</td>
            <td rowspan="3">措施名称</td>
            <td rowspan="3">施术单位</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="2">孩次</td>
            <td rowspan="2">孩子姓名</td>
            <td colspan="2">身份证号或出生年月</td>
            <td rowspan="2">政策属性</td>
            <td rowspan="2">健康状况</td>
            <td rowspan="2">血缘关系</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td>性别</td>
            <td>出生年月</td>
          </tr>
        </thead>
    <asp:Repeater ID="rep_Data" runat="server" OnItemCommand="rep_Data_ItemCommand">
      <ItemTemplate>
        <tbody>
            <tr style="text-align:center;">
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
      <tr >
        <td width="144" colspan="4" bgcolor="#cccccc">生育情况</td>
      </tr>
      <tr >
        <td width="180" align="right" bgcolor="#FFFFCC">组别：</td>
        <td width="250"><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td width="180" align="right" bgcolor="#FFFFCC">妇女姓名：</td>
        <td><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">身份证号码：</td>
        <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td align="right" bgcolor="#FFFFCC">丈夫姓名：</td>
        <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
      </tr>
      <tr class="zhengwen">
        <td colspan="4" bgcolor="#FFFFCC">孩子生育情况</td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">孩次：</td>
        <td><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td align="right" bgcolor="#FFFFCC">孩子姓名：</td>
        <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" rowspan="2" bgcolor="#FFFFCC">身份证号或出生日期：</td>
        <td colspan="3" align="left">性&nbsp;&nbsp;别：<asp:DropDownList ID="txtFileds07" runat="server">
                <asp:ListItem>请选择</asp:ListItem>
                <asp:ListItem>男</asp:ListItem>
                <asp:ListItem>女</asp:ListItem>
            </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td colspan="3" align="left">出生年月：<input id="txtFileds08" readonly="readonly" size="20" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" /></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">政策属性：</td>
        <td>
            <asp:DropDownList ID="txtFileds09" runat="server">
                <asp:ListItem>政策内</asp:ListItem>
                <asp:ListItem>政策外</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right" bgcolor="#FFFFCC">健康状况：</td>
        <td>
            <asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/>
        </td>
      </tr>
      <tr>
        <td align="right" bgcolor="#FFFFCC">血缘关系：</td>
        <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td align="right" bgcolor="#FFFFCC">所属地区：</td>
        <td><asp:Label ID="lbl_DataAreaSel" runat="server"></asp:Label></td>
      </tr>
      <tr >
        <td width="144" colspan="4" bgcolor="#cccccc">避孕情况</td>
      </tr>
      <tr >
        <td align="right" bgcolor="#FFFFCC">组别：</td>
        <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td align="right" bgcolor="#FFFFCC">姓名：</td>
        <td>
            <asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/>
        </td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">身份证号码：</td>
        <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td align="right" bgcolor="#FFFFCC">措施日期：</td>
        <td><input id="txtFileds15" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" /></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">措施名称：</td>
        <td>
            <asp:DropDownList ID="txtFileds16" runat="server">
                <asp:ListItem>无</asp:ListItem>
                <asp:ListItem>避孕套</asp:ListItem>
                <asp:ListItem>避孕针</asp:ListItem>
                <asp:ListItem>子宫环</asp:ListItem>
                <asp:ListItem>避孕帖</asp:ListItem>
                <asp:ListItem>计算排卵期</asp:ListItem>
            </asp:DropDownList>
          </td>
        <td align="right" bgcolor="#FFFFCC">施术单位：</td>
        <td><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
      </tr>
    </table>
    
    <%=js_value %>
    <!-- LFileds_type 与 LFileds_txt成对-->
    <!-- LFileds_type判断值类型  0，字符不判断；1，数字或小数；-->
    <asp:Label ID="LFileds_type" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    <!-- LFileds_txt判断值类型有误时提示标题-->
    <asp:Label ID="LFileds_txt" runat="server" Text="组别,妇女姓名,身份证号码,丈夫姓名,孩次,孩子姓名,姓别,出生年月,政策属性,健康状况,血缘关系,,,,,," style="display:none"></asp:Label>
        
</td></tr></table>
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
