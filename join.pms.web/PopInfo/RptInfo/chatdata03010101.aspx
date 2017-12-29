<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata03010101.aspx.cs" Inherits="join.pms.web.RptInfo.chatdata03010101" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1"  %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>人口动态信息报告单一填报</title>
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
    <p style="font-size:18px; text-align:center; font-weight:bold"><input id="txt_RptTime" readonly="readonly"  size="10" onclick="JTC.setday({format:'yyyy年MM月', readOnly:true})" runat="server" />份人口动态信息报告单<一>（存根）</p>
    <p><%=str_AreaName%></p>
    <table width="100%" border="1" cellpadding="3" cellspacing="1"  style="border-collapse:collapse;bordercolor=#000000">
        <thead>
        <tr style="text-align:center; background-color:#cccccc">
            <td colspan="13">婚姻、迁移、流动情况</td>
            <td colspan="5">死亡情况</td>
            <td rowspan="2">操作</td>
        </tr>
        <tr style="text-align:center;background-color:#cccccc">
            <td>组别</td>
            <td>户口编码</td>
            <td>姓名</td>
            <td>公民身份证号</td>
            <td>变动类型</td>
            <td>民族</td>
            <td>文化程度</td>
            <td>户口性质</td>
            <td>婚姻状况</td>
            <td>初婚日期</td>
            <td>居住状况</td>
            <td>与户主关系</td>
            <td>配偶姓名</td>
            <td>姓名</td>
            <td>性别</td>
            <td>出生年月</td>
            <td>死亡日期</td>
            <td>死亡原因</td>
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
               <td><%# Eval("Fileds18")%></td>
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
        <td width="144" colspan="4" bgcolor="#cccccc">婚姻、迁移、流动情况</td>
      </tr>
      <tr >
        <td align="right" bgcolor="#FFFFCC">组别：</td>
        <td><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td align="right" bgcolor="#FFFFCC">户口编号：</td>
        <td><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">姓名：</td>
        <td><asp:TextBox ID="txtFileds03" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td align="right" bgcolor="#FFFFCC">公民身份证号：</td>
        <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">变动类型：</td>
        <td>
            <asp:DropDownList ID="txtFileds05" runat="server">
                <asp:ListItem>--请选择--</asp:ListItem>
                <asp:ListItem>初婚</asp:ListItem>
                <asp:ListItem>再婚</asp:ListItem>
                <asp:ListItem>离婚</asp:ListItem>
                <asp:ListItem>丧偶</asp:ListItem>
                <asp:ListItem>迁入</asp:ListItem>
                <asp:ListItem>迁出</asp:ListItem>
                <asp:ListItem>流入</asp:ListItem>
                <asp:ListItem>流出</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right" bgcolor="#FFFFCC">民族：</td>
        <td>
            <asp:DropDownList ID="txtFileds06" runat="server">
            <asp:ListItem>汉族</asp:ListItem>
            <asp:ListItem>蒙古族</asp:ListItem>
            <asp:ListItem>回族</asp:ListItem>
            <asp:ListItem>藏族</asp:ListItem>
            <asp:ListItem>维吾尔族</asp:ListItem>
            <asp:ListItem>苗族</asp:ListItem>
            <asp:ListItem>彝族</asp:ListItem>
            <asp:ListItem>壮族</asp:ListItem>
            <asp:ListItem>布衣族</asp:ListItem>
            <asp:ListItem>朝鲜族</asp:ListItem>
            <asp:ListItem>满族</asp:ListItem>
            <asp:ListItem>侗族</asp:ListItem>
            <asp:ListItem>瑶族</asp:ListItem>
            <asp:ListItem>白族</asp:ListItem>
            <asp:ListItem>土家族</asp:ListItem>
            <asp:ListItem>哈尼族</asp:ListItem>
            <asp:ListItem>哈萨克族</asp:ListItem>
            <asp:ListItem>傣族</asp:ListItem>
            <asp:ListItem>黎族</asp:ListItem>
            <asp:ListItem>傈僳族</asp:ListItem>
            <asp:ListItem>佤族</asp:ListItem>
            <asp:ListItem>畲族</asp:ListItem>
            <asp:ListItem>高山族</asp:ListItem>
            <asp:ListItem>拉祜族</asp:ListItem>
            <asp:ListItem>水族</asp:ListItem>
            <asp:ListItem>东乡族</asp:ListItem>
            <asp:ListItem>纳西族</asp:ListItem>
            <asp:ListItem>景颇族</asp:ListItem>
            <asp:ListItem>柯尔克孜族</asp:ListItem>
            <asp:ListItem>土族</asp:ListItem>
            <asp:ListItem>达斡族</asp:ListItem>
            <asp:ListItem>仫佬族</asp:ListItem>
            <asp:ListItem>羌族</asp:ListItem>
            <asp:ListItem>布朗族</asp:ListItem>
            <asp:ListItem>撒拉族</asp:ListItem>
            <asp:ListItem>毛南族</asp:ListItem>
            <asp:ListItem>仡佬族</asp:ListItem>
            <asp:ListItem>锡伯族</asp:ListItem>
            <asp:ListItem>阿昌族</asp:ListItem>
            <asp:ListItem>普米族</asp:ListItem>
            <asp:ListItem>塔吉克族</asp:ListItem>
            <asp:ListItem>怒族</asp:ListItem>
            <asp:ListItem>乌孜别克族</asp:ListItem>
            <asp:ListItem>俄罗斯族</asp:ListItem>
            <asp:ListItem>鄂温克族</asp:ListItem>
            <asp:ListItem>德昂族</asp:ListItem>
            <asp:ListItem>保安族</asp:ListItem>
            <asp:ListItem>裕固族</asp:ListItem>
            <asp:ListItem>京族</asp:ListItem>
            <asp:ListItem>塔塔尔族</asp:ListItem>
            <asp:ListItem>独龙族</asp:ListItem>
            <asp:ListItem>鄂伦春族</asp:ListItem>
            <asp:ListItem>赫哲族</asp:ListItem>
            <asp:ListItem>门巴族</asp:ListItem>
            <asp:ListItem>珞巴族</asp:ListItem>
            <asp:ListItem>基诺族</asp:ListItem>
            <asp:ListItem>未识别</asp:ListItem>
            <asp:ListItem>外入中籍</asp:ListItem>
            </asp:DropDownList>
        </td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">文化程度：</td>
        <td>
            <asp:DropDownList ID="txtFileds07" runat="server">
                <asp:ListItem>小学</asp:ListItem>
                <asp:ListItem>初中</asp:ListItem>
                <asp:ListItem>高中/职专</asp:ListItem>
                <asp:ListItem>大专</asp:ListItem>
                <asp:ListItem>本科</asp:ListItem>
                <asp:ListItem>研究生</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right" bgcolor="#FFFFCC">户口性质：</td>
        <td>
            <asp:DropDownList ID="txtFileds08" runat="server">
                <asp:ListItem>请选择</asp:ListItem>
                <asp:ListItem>农业</asp:ListItem>
                <asp:ListItem>非农业</asp:ListItem>
            </asp:DropDownList>
        </td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">婚姻状况：</td>
        <td>
            <asp:DropDownList ID="txtFileds09" runat="server">
                <asp:ListItem>初婚</asp:ListItem>
                <asp:ListItem>再婚</asp:ListItem>
                <asp:ListItem>复婚</asp:ListItem>
                <asp:ListItem>离婚</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right" bgcolor="#FFFFCC">初婚日期：</td>
        <td>
            <input id="txtFileds10" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
        </td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">居住状况：</td>
        <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td align="right" bgcolor="#FFFFCC">与户主关系：</td>
        <td><asp:TextBox ID="txtFileds12" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">配偶姓名：</td>
        <td><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td align="right" bgcolor="#FFFFCC">所属区域：</td>
        <td><asp:Label ID="lbl_DataAreaSel" runat="server"></asp:Label></td>
      </tr>
      <tr >
        <td width="144" colspan="4" bgcolor="#cccccc">死亡情况</td>
      </tr>
      <tr >
        <td align="right" bgcolor="#FFFFCC">姓名：</td>
        <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
        <td align="right" bgcolor="#FFFFCC">性别：</td>
        <td>
            <asp:DropDownList ID="txtFileds15" runat="server">
                <asp:ListItem>请选择</asp:ListItem>
                <asp:ListItem>男</asp:ListItem>
                <asp:ListItem>女</asp:ListItem>
            </asp:DropDownList>
        </td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">出生年月：</td>
        <td><input id="txtFileds16" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" /></td>
        <td align="right" bgcolor="#FFFFCC">死亡日期：</td>
        <td><input id="txtFileds17" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" /></td>
      </tr>
      <tr class="zhengwen">
        <td align="right" bgcolor="#FFFFCC">死亡原因：</td>
        <td colspan="3"><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
      </tr>
    </table>
    <%=js_value %>
    <!-- LFileds_type 与 LFileds_txt成对-->
    <!-- LFileds_type判断值类型  0，字符不判断；1，数字或小数；-->
    <asp:Label ID="LFileds_type" runat="server" Text="1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1" style="display:none"></asp:Label>
    <!-- LFileds_txt判断值类型有误时提示标题-->
    <asp:Label ID="LFileds_txt" runat="server" Text="组别,户口编号,姓名,公民身份证号,变动类型,民族,文化程度,户口性质,婚姻状况,初婚日期,居住状况,与户主关系,配偶姓名,,,,," style="display:none"></asp:Label>

    
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
