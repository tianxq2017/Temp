<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizFlowAudit.aspx.cs" Inherits="join.pms.web.BizInfo.BizFlowAudit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="/css/index.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" src="/includes/CommSys.js" type="text/javascript"></script>
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
<script src="/Scripts/jsdate.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
<!--导航信息-->
<div class="mbx">当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
<!--提示信息-->
<div class="tsxx">提示信息：请填写您的审核意见后；点击“提交”按钮……</div>
<!-- 页面参数 -->
<input type="hidden" name="txtBizFlowID" id="txtBizFlowID" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtBizStep" id="txtBizStep" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtBizStepNames" id="txtBizStepNames" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtBizStepTotal" id="txtBizStepTotal" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSealPath" id="txtSealPath" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtSealPass" id="txtSealPass" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtUserAcc" id="txtUserAcc" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtUserAreaCode" id="txtUserAreaCode" value="" runat="server" style="display:none;"/>
<input type="hidden" name="txtUserRoleID" id="txtUserRoleID" value="" runat="server" style="display:none;"/>
<div id="ec_bodyZone">
<table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C"><tr><td bgcolor="#F2F9EC">
<table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4"><tr><td bgcolor="#F3FCFF" align="left">
<!-- 编辑区  -->
<br/>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen">
    <td width="120"  height="25" align="right" class="zhengwenjiacu">申请时间：</td>
    <td align="left"><asp:Literal ID="LiteralBizAppDate" runat="server" /></td>
</tr>
<tr class="zhengwen">
    <td width="120"  height="25" align="right" class="zhengwenjiacu">审核单位：</td>
    <td align="left"><asp:TextBox ID="txtDeptName" runat="server" EnableViewState="False" MaxLength="50" Width="500px" /><font color="red">*</font></td>
</tr>
<tr class="zhengwen">
    <td width="120"  height="25" align="right" class="zhengwenjiacu">处理意见：</td>
    <td align="left">
    <asp:Literal ID="LiteralComments" runat="server" /><font color="red">*</font><br/>
    <script language="javascript"  type="text/javascript">
function SetTxtCommVal(selValue){
    // SetTxtCommVal(this.options[this.options.selectedIndex].value)
    document.getElementById('txtComments').value = selValue;
    document.all.selAuditComments.options[0].selected=true;
    // document.getElementById("selAuditComments");
    
    switch (selValue)
            {
            case "情况属实，同意上报审核。":
              document.getElementById("DDLPass").value = "1";
              document.getElementById("txtMsg").style.display="none";
              break;
            case "请选择引用条款……":
              document.getElementById("DDLPass").value = "1";
              document.getElementById("txtMsg").style.display="none";
              break;
            case "同意上报审核。":
              document.getElementById("DDLPass").value = "1";
              document.getElementById("txtMsg").style.display="none";
              break;
            case "准予享受计划生育家庭奖励扶助政策。" :
              document.getElementById("DDLPass").value = "1";
              document.getElementById("txtMsg").style.display="none";
              break;
            case "准予享受计划生育家庭特别扶助政策。":
              document.getElementById("DDLPass").value = "1";
              document.getElementById("txtMsg").style.display="none";
              break;
            case "准予享受计划生育“少生快富”工程扶持政策。":
              document.getElementById("DDLPass").value = "1";
              document.getElementById("txtMsg").style.display="none";
              break;
            case "准予享受计划生育结扎   （双女，蒙古族三女，二孩）结扎奖励相关政策。":
              document.getElementById("DDLPass").value = "1";
              document.getElementById("txtMsg").style.display="none";
              break;
            case "准予享受。":
              document.getElementById("DDLPass").value = "1";
              document.getElementById("txtMsg").style.display="none";
              break;
            case "准予发证。":
              document.getElementById("DDLPass").value = "1";
              document.getElementById("txtMsg").style.display="none";
              break;
            case "情况属实。":
              document.getElementById("DDLPass").value = "1";
              document.getElementById("txtMsg").style.display="none";
              break;
            case "情况属实，同意办证。":
              document.getElementById("DDLPass").value = "1";
              document.getElementById("txtMsg").style.display="none";
              break;
            case "同意终止妊娠。":
              document.getElementById("DDLPass").value = "1";
              document.getElementById("txtMsg").style.display="none";
              break;
            default:
              document.getElementById("DDLPass").value = "0";
              document.getElementById("txtMsg").style.display="table-row";
              break;
            }
    
}

function SetTxtCommValByFilter(selValue){
    // SetTxtCommVal(this.options[this.options.selectedIndex].value)
    alert(selValue);
    var commVal = document.getElementById('txtComments').value;
    if(commVal=="请选择引用条款……" || commVal.indexOf("内蒙古自治区人口与计划生育条例") > 0){
        document.getElementById('txtComments').value="根据《内蒙古自治区人口与计划生育条例》"+selValue+"的规定，符合生育条件，同意登记。";
    }
    else{
        if(commVal.length>5){
            if(commVal.substring(0,2)=="符合") document.getElementById('txtComments').value="符合"+selValue+""+commVal.substring(2);
            if(commVal.substring(0,3)=="不符合") document.getElementById('txtComments').value="不符合"+selValue+""+commVal.substring(3);
        }
        
    }
    document.all.selAuditItems.options[0].selected=true;
}

function showMsg()
{
    var txttype = document.getElementById("DDLPass").value;
    var obj1 = document.getElementById("txtMsg");
    if(txttype=="0")
	{
	    obj1.style.display="table-row";
	}
	else
	{
	    obj1.style.display="none";
	}
}

function change_tel(i)
{
    var txttype = "";
    if(i=="1"){
    txttype = document.getElementById("DDLApproval").value; 
    }
    if(i=="2"){
    txttype = document.getElementById("DDLAuditUser").value;
    }
	if(txttype.indexOf(",")>0){
	var str= new Array();
	str=txttype.split(",");
	if(str.length==2) {
    document.getElementById("txtApprovalUserTel").value=str[1];
    }
	}
	else{
	    if(txttype!=""){
		alert("您选择的用户不存在或签名信息不完善，请完善后再操作！");
		window.location.href="/SysAdmin/SysUserInfo.aspx?1=1&FuncCode=1012&FuncNa=我的用户信息";
		}
		//document.form1.txtApprovalUserTel.focus(); 
	}
}
</script>
    <asp:TextBox ID="txtComments" runat="server" EnableViewState="False" MaxLength="25" Width="500px" Rows="3" TextMode="MultiLine"/>
    </td>
</tr>
<tr class="zhengwen" >
    <td width="120"  height="25" align="right" class="zhengwenjiacu">处理结论：</td>
    <td align="left">
    <!---->
    <asp:DropDownList ID="DDLPass" runat="server" onchange="showMsg()">
    <asp:ListItem Value="4">请选择……</asp:ListItem>
    <asp:ListItem Value="1">审核通过</asp:ListItem>
    <asp:ListItem Value="0">审核驳回</asp:ListItem>
    </asp:DropDownList>
    <font color="red">*</font>
    </td>
</tr>
<tr id="txtMsg" style="display:none;" >
    <td height="25" align="right" class="zhengwenjiacu">驳回意见：</td>
    <td align="left"><asp:TextBox ID="txtBHMsg" runat="server" EnableViewState="False" MaxLength="10" Width="500px" Rows="2" TextMode="MultiLine"/></td>
</tr>
<asp:Panel ID="PanelApproval" runat="server" Visible="False">
<tr class="zhengwen">
    <td  height="25" align="right" class="zhengwenjiacu">经 办 人：</td>
    <td align="left">
        <asp:TextBox ID="txtApproval" runat="server" EnableViewState="False" MaxLength="10" Width="200px"/>
        
        <asp:DropDownList ID="DDLApproval" runat="server" onchange="change_tel('1');"></asp:DropDownList>
        
        <font color="red">*</font>
    </td>
</tr>
<tr class="zhengwen">
    <td  height="25" align="right" class="zhengwenjiacu">签名密码：</td>
    <td align="left"><asp:TextBox ID="txtApprovalPass" runat="server" EnableViewState="False" MaxLength="20" Width="200px" TextMode="Password"/><font color="red">*</font></td>
</tr>
</asp:Panel>
<asp:Panel ID="PanelAudit" runat="server" Visible="False">
    <tr class="zhengwen">
        <td  height="25" align="right" class="zhengwenjiacu">责 任 人：</td>
        <td align="left">
            <asp:TextBox ID="txtAuditUser" runat="server" EnableViewState="False" MaxLength="10" Width="200px" />
            <asp:DropDownList ID="DDLAuditUser" runat="server" onchange="change_tel('2');"></asp:DropDownList>
            <font color="red">*</font>
         </td>
    </tr>
    <tr class="zhengwen">
        <td  height="25" align="right" class="zhengwenjiacu">签名密码：</td>
        <td align="left"><asp:TextBox ID="txtAuditUserPass" runat="server" EnableViewState="False" MaxLength="20" Width="200px" TextMode="Password"/><font color="red">*</font></td>
    </tr>
</asp:Panel>
<asp:Panel ID="PanelAllowDate" runat="server" Visible="False">
<tr class="zhengwen">
    <td  height="25" align="right" class="zhengwenjiacu">准生期限：</td>
    <td align="left">
    <span>
        <input id="txtStartDate"  size="12" onclick="SelectDate(document.getElementById('txtStartDate'),'yyyy-MM-dd')"  runat="server" />
        &nbsp;&nbsp;-&nbsp;&nbsp;<input id="txtEndDate"  size="12" onclick="SelectDate(document.getElementById('txtEndDate'),'yyyy-MM-dd')" runat="server" />
    </span><font color="red">*</font></td>
</tr> 
</asp:Panel>
<tr class="zhengwen" >
    <td  height="25" align="right" class="zhengwenjiacu">联系电话：</td>
    <td align="left"><asp:TextBox ID="txtApprovalUserTel" runat="server" EnableViewState="False" MaxLength="11" Width="200px" /><font color="red">*</font></td>
</tr>
<asp:Panel ID="PanelSeal" runat="server" Visible="true">
    <tr class="zhengwen">
        <td  height="25" align="right" class="zhengwenjiacu">签章密码：</td>
        <td align="left"><asp:TextBox ID="txtSealPwd" runat="server" EnableViewState="False" MaxLength="20" Width="200px" TextMode="Password"/><font color="red">*</font></td>
    </tr>
</asp:Panel>
<tr class="zhengwen">
    <td height="25" align="right" class="zhengwenjiacu">处理日期：</td>
    <td align="left">
        <input id="txtOprateDate"  size="29" onclick="SelectDate(document.getElementById('txtOprateDate'),'yyyy-MM-dd')"  runat="server" /><font color="red">*</font>
    </td>
 </tr>
</table>
<asp:Panel ID="PanelFaZheng" runat="server" Visible="False">
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<table width="880" border="0"  cellpadding="3" cellspacing="1">
<tr class="zhengwen" style=" display:none">
    <td  height="25" align="right" class="zhengwenjiacu">证件编号：</td>
    <td align="left"><input id="txtCertificateNoA" runat="server" size="29"   /><font color="red">*</font></td>
</tr>
<tr class="zhengwen" style=" display:none">
    <td width="120"  height="25" align="right" class="zhengwenjiacu">时间：</td>
    <td align="left">
    <input id="txtCertificateDateStart"  size="29" onclick="SelectDate(document.getElementById('txtCertificateDateStart'),'yyyy-MM-dd')"  runat="server" /> 
    </td>
</tr>
<!--
<tr class="zhengwen">
    <td height="25" align="right" class="zhengwenjiacu">有效期：</td>
    <td align="left">
    <asp:Literal ID="LiteralValidDate" runat="server" />
    </td>
 </tr>
 -->
</table>
</asp:Panel>
<table width="880" border="0"  cellpadding="0" cellspacing="0"><tr><td height="4" background="/images/xuxian.gif" ></td></tr></table>
<asp:Literal ID="LiteralDocs" runat="server" />
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
<link rel="stylesheet" href="/scripts/lightbox/lightbox.css" type="text/css" media="screen" />
<script src="/scripts/lightbox/jquery-1.8.3.min.js" type="text/javascript"></script>
<script src="/scripts/lightbox/lightbox.js" type="text/javascript"></script>
</html>