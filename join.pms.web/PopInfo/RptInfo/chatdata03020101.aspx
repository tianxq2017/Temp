<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03020101.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03020101" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>预防艾滋病母婴传播工作月报表</title>
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
    <p style="font-size:18px; text-align:center; font-weight:bold">预防艾滋病母婴传播工作月报表（<input id="txt_RptTime" readonly="readonly"  size="10" onclick="JTC.setday({format:'yyyy年MM月', readOnly:true})" runat="server" />）</p>
    <p><%=str_AreaName%></p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
        <tr style="text-align:center; background-color:#cccccc">
            <td rowspan="3">填报单位</td>
            <td colspan="8">婚前保健</td>
            <td colspan="4">孕期</td>
            <td colspan="9">住院分娩</td>
            <td colspan="8">非住院分娩</td>
            <td rowspan="3">地区产妇总数</td>
            <td rowspan="3">地区活产总数</td>
            <td rowspan="3">操作</td>
          </tr>
          <tr style="text-align:center; background-color:#cccccc">
            <td colspan="2">接受婚前保健人数</td>
            <td colspan="2">接受艾滋病咨询人数</td>
            <td colspan="2">接受HIV抗体检测人数</td>
            <td colspan="2">HIV抗体阳性人数</td>
            <td rowspan="2">接受初次产前保健的孕妇数</td>
            <td rowspan="2">接受艾滋病咨询孕妇数</td>
            <td rowspan="2">接受HIV抗体检测孕妇数</td>
            <td rowspan="2">HIV抗体阳性孕妇数</td>
            <td rowspan="2">住院分娩产妇数</td>
            <td rowspan="2">孕期接受艾滋病咨询产妇数</td>
            <td rowspan="2">孕期接受HIV抗体检测产妇数</td>
            <td rowspan="2">仅产时接受艾滋病咨询产妇数</td>
            <td rowspan="2">仅产时接受HIV抗体检测产妇数</td>
            <td rowspan="2">仅产时HIV抗体检测阳性产妇数</td>
            <td rowspan="2">HIV抗体阳性产妇总数</td>
            <td rowspan="2">住院分娩活产数</td>
            <td rowspan="2">HIV抗体阳性产妇所生活产数</td>
            <td rowspan="2">非住院分娩产妇数</td>
            <td rowspan="2">孕期接受艾滋病咨询产妇数</td>
            <td rowspan="2">孕期接受HIV抗体检测产妇数</td>
            <td rowspan="2">仅产时接受HIV抗体检测产妇数</td>
            <td rowspan="2">仅产时HIV抗体检测阳性产妇数</td>
            <td rowspan="2">HIV抗体阳性产妇数</td>
            <td rowspan="2">非住院分娩活产数</td>
            <td rowspan="2">HIV抗体阳性产妇所生活产数</td>
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
          </tr>
           <tr style="text-align:center;">
            <td>合计</td>
             <%for (int i = 0; i < arrNum.Length; i++)
              { %>
            <td><%=arrNum[i] %></td>
            <%} %>
            <td>--</td>
          </tr>
        </thead>
    <asp:Repeater ID="rep_Data" runat="server" OnItemCommand="rep_Data_ItemCommand">
      <ItemTemplate>
        <tbody>
            <tr style="text-align:center;">
                <td width="100"><%# Eval("AreaName")%></td>
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
                <td><%# Eval("Fileds23")%></td>
                <td><%# Eval("Fileds24")%></td>
                <td><%# Eval("Fileds25")%></td>
                <td><%# Eval("Fileds26")%></td>
                <td><%# Eval("Fileds27")%></td>
                <td><%# Eval("Fileds28")%></td>
                <td><%# Eval("Fileds29")%></td>
                <td><%# Eval("Fileds30")%></td>
                <td><%# Eval("Fileds31")%></td>
               <td width="70">
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
    <td align="right" width="100" bgcolor="#cccccc">单位：</td>
    <td colspan="3" bgcolor="#cccccc"><asp:Label ID="lbl_DataAreaSel" runat="server"></asp:Label>&nbsp;<asp:DropDownList ID="dr_DataAreaSel" runat="server"></asp:DropDownList></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="8" align="right" bgcolor="#FFFFCC">婚前保健</td>
    <td rowspan="2" align="right" bgcolor="#FFFFCC">接受婚前保健人数</td>
    <td align="right" bgcolor="#FFFFCC">男：</td>
    <td><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">女：</td>
    <td><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">接受艾滋病咨询人数</td>
    <td align="right" bgcolor="#FFFFCC">男：</td>
    <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">女：</td>
    <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2"  width="200" align="right" bgcolor="#FFFFCC">接受HIV抗体检测人数</td>
    <td align="right"  width="100" bgcolor="#FFFFCC">男：</td>
    <td><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">女：</td>
    <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="2" align="right" bgcolor="#FFFFCC">HIV抗体阳性人数</td>
    <td align="right" bgcolor="#FFFFCC">男：</td>
    <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">女：</td>
    <td><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="4" align="right" bgcolor="#cccccc">孕期</td>
    <td colspan="2" align="right" bgcolor="#cccccc">接受初次产前保健的孕妇数：</td>
    <td><asp:TextBox ID="txtFileds09" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">接受艾滋病咨询孕妇数：</td>
    <td><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">接受HIV抗体检测孕妇数：</td>
    <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">HIV抗体阳性孕妇数：</td>
    <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="9" align="right" bgcolor="#FFFFCC">住院分娩</td>
    <td colspan="2" align="right" bgcolor="#FFFFCC">住院分娩产妇数：</td>
    <td><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">孕期接受艾滋病咨询产妇数：</td>
    <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">孕期接受HIV抗体检测产妇数：</td>
    <td><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">仅产时接受艾滋病咨询产妇数：</td>
    <td><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">仅产时接受HIV抗体检测产妇数：</td>
    <td><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">仅产时HIV抗体检测阳性产妇数：</td>
    <td><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">HIV抗体阳性产妇总数：</td>
    <td><asp:TextBox ID="txtFileds19" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">住院分娩活产数：</td>
    <td><asp:TextBox ID="txtFileds20" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#FFFFCC">HIV抗体阳性产妇所生活产数：</td>
    <td><asp:TextBox ID="txtFileds21" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td rowspan="8" align="right" bgcolor="#cccccc">非住院分娩</td>
    <td colspan="2" align="right" bgcolor="#cccccc">非住院分娩产妇数：</td>
    <td><asp:TextBox ID="txtFileds22" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">孕期接受艾滋病咨询产妇数：</td>
    <td><asp:TextBox ID="txtFileds23" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">孕期接受HIV抗体检测产妇数：</td>
    <td><asp:TextBox ID="txtFileds24" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">仅产时接受HIV抗体检测产妇数：</td>
    <td><asp:TextBox ID="txtFileds25" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">仅产时HIV抗体检测阳性产妇数：</td>
    <td><asp:TextBox ID="txtFileds26" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">HIV抗体阳性产妇数：</td>
    <td><asp:TextBox ID="txtFileds27" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">非住院分娩活产数：</td>
    <td><asp:TextBox ID="txtFileds28" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="2" align="right" bgcolor="#cccccc">HIV抗体阳性产妇所生活产数：</td>
    <td><asp:TextBox ID="txtFileds29" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="3" align="right" bgcolor="#FFFFCC">地区产妇总数：</td>
    <td><asp:TextBox ID="txtFileds30" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
  <tr class="zhengwen">
    <td colspan="3" align="right" bgcolor="#FFFFCC">地区活产总数：</td>
    <td><asp:TextBox ID="txtFileds31" runat="server" EnableViewState="False" MaxLength="25" Width="200px" Text="0"/></td>
  </tr>
</table>
    <%=js_value %>
    <!-- LFileds_type 与 LFileds_txt成对-->
    <!-- LFileds_type判断值类型  0，字符不判断；1，数字或小数；-->
    <asp:Label ID="LFileds_type" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    <!-- LFileds_txt判断值类型有误时提示标题-->
    <asp:Label ID="LFileds_txt" runat="server"  Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>

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
