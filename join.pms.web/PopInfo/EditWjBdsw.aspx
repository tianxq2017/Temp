<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditWjBdsw.aspx.cs" Inherits="AreWeb.OnlineCertificate.PopInfo.EditWjBdsw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>卫计全员人口变动死亡信息</title>
    <link href="/Styles/index.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="/includes/CommSys.js" type="text/javascript"></script>
    <script language="javascript" src="/Scripts/datectrl.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请逐一填写下列全员人口变动死亡信息后；点击“提交”按钮（次月5日以前将上月情况提交）……</div>
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
  <td align="left"><input id="txtReportDate" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" />
    (填报时间,该数据实际的归属月份,请选择到该数据月之内)
    </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu"  bgcolor="#CCFFFF">数据单位：</td>
  <td align="left"><asp:DropDownList ID="DDLReportArea" runat="server"><asp:ListItem>请选择</asp:ListItem></asp:DropDownList></td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">户人编号：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">姓名：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">性别：</td>
    <td align="left" width="*"><asp:DropDownList ID="DDLFileds03" runat="server">
<asp:ListItem>男</asp:ListItem>
<asp:ListItem>女</asp:ListItem>
</asp:DropDownList></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">身份证号：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds04" runat="server" EnableViewState="False" MaxLength="25" Width="200px" onblur="showBirthday('txtFileds04','txtFileds07')"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">小区或组名称：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds05" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">门牌号：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds06" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">出生日期：</td>
    <td align="left" width="*"><input id="txtFileds07" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" /></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">民族：</td>
    <td align="left" width="*"><asp:DropDownList ID="DDLFileds08" runat="server">
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
</asp:DropDownList></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">文化程度：</td>
    <td align="left" width="*"> <asp:DropDownList ID="DDLFileds09" runat="server">
    <asp:ListItem>小学</asp:ListItem>
    <asp:ListItem>初中</asp:ListItem>
      <asp:ListItem>高中/职专</asp:ListItem>
      <asp:ListItem>大专</asp:ListItem>
      <asp:ListItem>本科</asp:ListItem>
      <asp:ListItem>研究生</asp:ListItem>
    </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">户口性质：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">婚姻状况：</td>
    <td align="left" width="*"><asp:DropDownList ID="DDLFileds11" runat="server">
            <asp:ListItem>初婚</asp:ListItem>
            <asp:ListItem>再婚</asp:ListItem>
            <asp:ListItem>复婚</asp:ListItem>
            <asp:ListItem>离婚</asp:ListItem>
        </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">初婚日期：</td>
    <td align="left" width="*"><input id="txtFileds12" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" /></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">与户主关系：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds13" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">户籍所在地：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds14" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">现居住地：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds15" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">配偶姓名：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds16" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">配偶公民身份号码：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds17" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<!--
户人编号,姓名,性别,身份证号,小区或组名称,
Fileds01,Fileds02,Fileds03,Fileds04,Fileds05,
门牌号,出生日期,民族,文化程度,户口性质,
Fileds06,Fileds07,Fileds08,Fileds09,Fileds10,
婚姻状况,初婚日期,与户主关系,户籍所在地,现居住地,
Fileds11,Fileds12,Fileds13,Fileds14,Fileds15
配偶姓名,配偶公民身份号码,父亲姓名,父亲公民身份号码,母亲姓名,
Fileds16,Fileds17,Fileds18,Fileds19,Fileds20
母亲公民身份号码,居住状况,退出系统日期,退出系统原因
Fileds21,Fileds22,Fileds23,Fileds24
-->
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">居住状况：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds22" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">退出系统日期：</td>
    <td align="left" width="*"><input id="txtFileds23" readonly="readonly" size="28" onclick="JTC.setday({format:'yyyy-MM-dd', readOnly:true})" runat="server" /></td>
</tr>
<tr class="zhengwen">
    <td align="right" bgcolor="#CCFFFF" height="25" class="zhengwenjiacu" width="100">退出系统原因：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds24" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td width="100" height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">父亲姓名：</td>
    <td align="left" width="300"><asp:TextBox ID="txtFileds18" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td width="100" align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">母亲姓名：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds20" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
    <td height="25" align="right" bgcolor="#CCFFFF" class="zhengwenjiacu">身份证号：</td>
    <td align="left" width="300"><asp:TextBox ID="txtFileds19" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
    <td align="right" bgcolor="#FFFFCC" class="zhengwenjiacu">身份证号：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds21" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<!-- 操作按钮 m_TargetUrl -->
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
