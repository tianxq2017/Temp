<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editZaiJiuYe.aspx.cs" Inherits="AreWeb.CertificateInOne.PopInfo.editZaiJiuYe" %>

<%@ Register Src="../userctrl/ucAreaSelXian.ascx" TagName="ucAreaSelXian" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/index.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../includes/CommSys.js" type="text/javascript"></script>
    <style type="text/css">
    <!--
    body {
	    margin-top: 10px;
	    background-color: #FFFFFF;
	    margin-left: 10px;
	    margin-right: 10px;
	    margin-bottom: 0px;
	    background-image: url(/images/mainyouxiabg.jpg);
	    background-position:right bottom;
	    background-repeat:no-repeat;
    }
    .butStyle {line-height: 20px; color: #000000; text-decoration: none; font-size: 12px;}
    -->
    </style>
<script language="javascript" type="text/javascript">
function PopCalendar(obj){
    showx = event.screenX - event.offsetX - 4 - 10 ; // + deltaX;
    showy = event.screenY - event.offsetY -168; // + deltaY;
    newWINwidth = 210 + 4 + 18;
    window.showModalDialog("/includes/Calendar.htm",obj,"dialogWidth:200px; dialogHeight:210px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; ");    
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
<!--妇女姓名,出生年月,婚姻状况,登记日期,生育情况,身份证号码,户籍地,现居住地,店铺名称,店铺地址-->
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="100" bgcolor="#FFFFCC">数据日期：</td>
  <td align="left"><input id="txtReportDate" readonly="readonly" size="29" onclick="PopCalendar(txtReportDate);return false;"  runat="server" />
    <input id="Image2" onclick="PopCalendar(txtReportDate);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
    (填报时间,该数据实际的归属日期,请选择到该数据月之内)
    </td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu"  bgcolor="#FFFFCC">数据单位：</td>
  <td align="left"><asp:DropDownList ID="DDLReportArea" runat="server"><asp:ListItem>请选择</asp:ListItem></asp:DropDownList></td>
</tr></table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td align="right" height="25" class="zhengwenjiacu" width="100">姓名：</td>
    <td align="left" width="300"><asp:TextBox ID="txtFileds01" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
 <td align="right" height="25" class="zhengwenjiacu" width="100">身份证号：</td>
    <td align="left" width="*"><asp:TextBox ID="txtFileds02" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" >性别：</td>
  <td align="left"><asp:DropDownList ID="DDLFileds03" runat="server">
    <asp:ListItem>男</asp:ListItem>
    <asp:ListItem>女</asp:ListItem>
    </asp:DropDownList></td>
     <td align="right" height="25" class="zhengwenjiacu">民族：</td>
  <td align="left">
  <asp:DropDownList ID="DDLFileds04" runat="server">
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
  <td align="right" height="25" class="zhengwenjiacu">健康状况：</td>
  <td align="left">
   <asp:DropDownList ID="DDLFileds06" runat="server">
    <asp:ListItem>良好</asp:ListItem>
    <asp:ListItem>一般</asp:ListItem>
    <asp:ListItem>差</asp:ListItem>
  </asp:DropDownList></td>
  <td align="right" height="25" class="zhengwenjiacu">婚姻状况：</td>
  <td align="left">
  <asp:DropDownList ID="DDLFileds07" runat="server">
            <asp:ListItem>初婚</asp:ListItem>
            <asp:ListItem>再婚</asp:ListItem>
            <asp:ListItem>复婚</asp:ListItem>
            <asp:ListItem>离婚</asp:ListItem>
        </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">政治面貌：</td>
  <td align="left">
  <asp:DropDownList ID="DDLFileds08" runat="server">
    <asp:ListItem>党员</asp:ListItem>
    <asp:ListItem>团员</asp:ListItem>
      <asp:ListItem>民族党派人士</asp:ListItem>
      <asp:ListItem>无党派</asp:ListItem>
      <asp:ListItem>群众</asp:ListItem>
    </asp:DropDownList>
      </td>
 <td align="right" height="25" class="zhengwenjiacu">学历：</td>
  <td align="left">
     <asp:DropDownList ID="DDLFileds09" runat="server">
    <asp:ListItem>小学</asp:ListItem>
    <asp:ListItem>初中</asp:ListItem>
      <asp:ListItem>高中/职专</asp:ListItem>
      <asp:ListItem>大专</asp:ListItem>
      <asp:ListItem>本科</asp:ListItem>
      <asp:ListItem>研究生</asp:ListItem>
    </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">联系电话：</td>
  <td align="left"><asp:TextBox ID="txtFileds10" runat="server" EnableViewState="False" MaxLength="25" Width="200px"/></td>
  <td align="right" height="25" class="zhengwenjiacu">出生日期：</td>
  <td align="left">
    <input id="txtFileds05" readonly="readonly" size="29" onclick="PopCalendar(txtFileds05);return false;"  runat="server" />
    <input id="Image4" onclick="PopCalendar(txtFileds05);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" />
  </td></tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu" width="100">人员类型：</td>
  <td align="left" width="300">
   <asp:DropDownList ID="DDLFileds11" runat="server">
    <asp:ListItem>新市民</asp:ListItem>
    <asp:ListItem>下岗失业人员</asp:ListItem> 
    <asp:ListItem>被征地农民</asp:ListItem> 
    <asp:ListItem>低保户</asp:ListItem> 
    <asp:ListItem>大中专院校毕业生</asp:ListItem>  
    <asp:ListItem>特困人员</asp:ListItem>  
    <asp:ListItem>残疾人</asp:ListItem>   
    <asp:ListItem>退役专业复员军人</asp:ListItem>   
    <asp:ListItem>其他</asp:ListItem>    
    </asp:DropDownList></td> 
    <td align="right" height="25" class="zhengwenjiacu" width="100">就业登记状态：</td>
  <td align="left">
  <asp:DropDownList ID="DDLFileds12" runat="server">
    <asp:ListItem>未就业</asp:ListItem>
    <asp:ListItem>已就业</asp:ListItem>    
    </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">审核状态：</td>
  <td align="left">
  <asp:DropDownList ID="DDLFileds14" runat="server">
    <asp:ListItem>未审核</asp:ListItem>
    <asp:ListItem>已审核</asp:ListItem>    
    </asp:DropDownList></td>
      <td align="right" height="25" class="zhengwenjiacu">是否已经打印：</td>
  <td align="left">
    <asp:DropDownList ID="DDLFileds15" runat="server">
    <asp:ListItem>未打印</asp:ListItem>
    <asp:ListItem>已打印</asp:ListItem>    
    </asp:DropDownList></td>
</tr>
<tr class="zhengwen">
  <td align="right" height="25" class="zhengwenjiacu">登记日期：</td>
  <td align="left"><input id="txtFileds13" readonly="readonly" size="29" onclick="PopCalendar(txtFileds13);return false;"  runat="server" />
    <input id="Image5" onclick="PopCalendar(txtFileds13);return false;" tabindex="1" type="image" src="/images/calendar.gif" align="absmiddle" /></td>
  <td align="right" height="25" class="zhengwenjiacu"></td>
  <td align="left"></td>
</tr>
</table>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<br/>
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