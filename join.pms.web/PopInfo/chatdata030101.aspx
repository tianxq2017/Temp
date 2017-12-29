<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatdata030101.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.chatdata030101" %>

<%@ Register Src="../userctrl/ucDataAreaSel.ascx" TagName="ucDataAreaSel" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>人口动态信息报告单一（流动情况）</title>
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
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr >
  <td align="right" width="142" height="25" class="zhengwenjiacu" width="150" bgcolor="#FFFFCC">数据日期：</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
      (填报时间,该数据实际的归属月份,选择到自然月之内即可)
    </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" bgcolor="#FFFFCC">数据单位：</td>
  <td align="left">
      <uc1:ucDataAreaSel ID="UcDataAreaSel1" runat="server" />
  </td>
</tr></table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0" cellspacing="3" cellpadding="1" class="zhengwen">
  <tr >
  <td width="144" rowspan="17" align="center" bgcolor="#FFFFCC">婚姻、迁移、流动情况</td>
  <td align="right" width="100" height="25" bgcolor="#FFFFCC">年度：</td>
  <td align="left">
    <asp:DropDownList ID="dd_Year" runat="server">
        <asp:ListItem>2015</asp:ListItem>
        <asp:ListItem>2016</asp:ListItem>
        <asp:ListItem>2017</asp:ListItem>
        <asp:ListItem>2018</asp:ListItem>
        <asp:ListItem>2019</asp:ListItem>
        <asp:ListItem>2020</asp:ListItem>
    </asp:DropDownList>  
  </td>
</tr>
<tr >
  <td align="right" height="25" bgcolor="#FFFFCC">月份：</td>
  <td align="left">
    <asp:DropDownList ID="dd_Month" runat="server">
        <asp:ListItem>1</asp:ListItem>
        <asp:ListItem>2</asp:ListItem>
        <asp:ListItem>3</asp:ListItem>
        <asp:ListItem>4</asp:ListItem>
        <asp:ListItem>5</asp:ListItem>
        <asp:ListItem>6</asp:ListItem>
        <asp:ListItem>7</asp:ListItem>
        <asp:ListItem>8</asp:ListItem>
        <asp:ListItem>9</asp:ListItem>
        <asp:ListItem>10</asp:ListItem>
        <asp:ListItem>11</asp:ListItem>
        <asp:ListItem>12</asp:ListItem>
    </asp:DropDownList>  
  </td>
</tr>
  <tr >
    <td align="right" bgcolor="#FFFFCC">组别：</td>
    <td><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">户口编号：</td>
    <td><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
    <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">姓名：</td>
    <td><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
    <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">公民身份证号：</td>
    <td><asp:TextBox ID="txtFileds07" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
    <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">变动类型：</td>
    <td><asp:TextBox ID="txtFileds08" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
    <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">民族：</td>
    <td>
        <asp:DropDownList ID="DDLFileds09" runat="server">
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
        <asp:DropDownList ID="DDLFileds10" runat="server">
            <asp:ListItem>小学</asp:ListItem>
            <asp:ListItem>初中</asp:ListItem>
            <asp:ListItem>高中/职专</asp:ListItem>
            <asp:ListItem>大专</asp:ListItem>
            <asp:ListItem>本科</asp:ListItem>
            <asp:ListItem>研究生</asp:ListItem>
        </asp:DropDownList>
    </td>
  </tr>
    <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">户口性质：</td>
    <td><asp:TextBox ID="txtFileds11" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
    <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">婚姻状况：</td>
    <td>
        <asp:DropDownList ID="DDLFileds12" runat="server">
            <asp:ListItem>初婚</asp:ListItem>
            <asp:ListItem>再婚</asp:ListItem>
            <asp:ListItem>复婚</asp:ListItem>
            <asp:ListItem>离婚</asp:ListItem>
        </asp:DropDownList>
    </td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">初婚日期sss：</td>
    <td>
        <input id="txtFileds13" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
    </td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">居住状况：</td>
    <td><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr> 
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">与户主关系：</td>
    <td><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">配偶姓名：</td>
    <td><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">负责人：</td>
    <td><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
  <tr class="zhengwen">
    <td align="right" bgcolor="#FFFFCC">填表人：</td>
    <td><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  </tr>
</table>

<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
<!-- 操作按钮 m_TargetUrl -->
<table cellSpacing="0" cellPadding="0" width="100%" border="0"  class="zhengwen">
<tr><td align="right" style="width: 100px; height: 30px;">&nbsp;</td><td width="*" align="left" style="height: 25px">
<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" OnClick="btnAdd_Click"  CssClass="submit6"></asp:Button>

<input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';" class="submit6" />
</td></tr></table>
<!--End-->
</td></tr></table>
</td></tr></table>
</div>
    </form>
</body>
</html>
