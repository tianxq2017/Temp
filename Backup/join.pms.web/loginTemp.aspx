<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loginTemp.aspx.cs" Inherits="join.pms.web.loginTemp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>勉县扶贫资金全程监测预警管理系统</title>
    <style type="text/css">
    
		BODY { color: #000000; background-color: white; font-family: Verdana; margin-left: 0px; margin-top: 0px; }
		#content { margin-left: 30px; font-size: .70em; padding-bottom: 2em; }
		A:link { color: #336699; font-weight: bold; text-decoration: underline; }
		A:visited { color: #6699cc; font-weight: bold; text-decoration: underline; }
		A:active { color: #336699; font-weight: bold; text-decoration: underline; }
		A:hover { color: cc3300; font-weight: bold; text-decoration: underline; }
		P { color: #000000; margin-top: 0px; margin-bottom: 12px; font-family: Verdana; }
		pre { background-color: #e5e5cc; padding: 5px; font-family: Courier New; font-size: x-small; margin-top: -5px; border: 1px #f0f0e0 solid; }
		td { color: #000000; font-family: Verdana; font-size: .9em; }
		h2 { font-size: 1.5em; font-weight: bold; margin-top: 25px; margin-bottom: 10px; border-top: 1px solid #003366; margin-left: -15px; color: #003366; }
		h3 { font-size: 1.1em; color: #000000; margin-left: -15px; margin-top: 10px; margin-bottom: 10px; }
		ul { margin-top: 10px; margin-left: 20px; }
		ol { margin-top: 10px; margin-left: 20px; }
		li { margin-top: 10px; color: #000000; }
		font.value { color: darkblue; font: bold; }
		font.key { color: darkgreen; font: bold; }
		font.error { color: darkred; font: bold; }
		.heading1 { color: #ffffff; font-family: Tahoma; font-size: 26px; font-weight: normal; background-color: #003366; margin-top: 0px; margin-bottom: 0px; margin-left: -30px; padding-top: 10px; padding-bottom: 10px; padding-left: 15px; width: 105%; }
		.button { background-color: #dcdcdc; font-family: Verdana; font-size: 1em; border-top: #cccccc 1px solid; border-bottom: #666666 1px solid; border-left: #cccccc 1px solid; border-right: #666666 1px solid; }
		.frmheader { color: #000000; background: #dcdcdc; font-family: Verdana; font-size: .7em; font-weight: normal; border-bottom: 1px solid #dcdcdc; padding-top: 2px; padding-bottom: 2px; }
		.frmtext { font-family: Verdana; font-size:.9em; margin-top: 8px; margin-bottom: 0px; margin-left: 32px; }
		.frmInput { font-family: Verdana; font-size: 1em; }
		.intro { margin-left: -15px; }
           
    </style>
    <script language="javascript" src="/includes/CommSys.js" type="text/javascript"></script>
</head>
<body>
    
    <div id="content">
      <p class="heading1">勉县扶贫资金全程监测预警管理系统</p><br>
      <span>
          <p class="intro">通辽市卫生和计划生育委员会</p>
          <h2>临时登录入口：</h2>
            请输入测试账号、密码、验证码以及临时认证码后，单击“登录”按钮。    
            <form id="form1" runat="server">  
            
                 
            <table cellspacing="0" cellpadding="4" frame="box" bordercolor="#dcdcdc" rules="none" style="border-collapse: collapse;width:500px;">

            <tr>
            <td class="frmHeader" background="#dcdcdc" style="border-right: 2px solid white;width:80px;"></td>
            <td class="frmHeader" background="#dcdcdc"></td>
            </tr>

            <tr>
            <td class="frmText" style="color: #000000; font-weight: normal;" align="right">用户名：</td>
            <td><input type="text" name="loginName" id="loginName"  autocomplete="off" runat="server" enableviewstate="false" maxlength="20" size="49"  /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
            <td class="frmText" style="color: #000000; font-weight: normal;" align="right">密&nbsp;&nbsp;码：</td>
            <td><input type="password" name="loginPass" id="loginPass"  autocomplete="off" runat="server" enableviewstate="false" maxlength="20" size="50"  /></td>
            </tr>
            <tr>
            <td class="frmText" style="color: #000000; font-weight: normal;" align="right">验证码：</td>
            <td><input name="txtCheckCode" type="text" id="txtCheckCode" size="9" autocomplete="off" maxlength="6"  /> &nbsp;<img id="CodeImg" onClick="document.getElementById('CodeImg').src='/userctrl/GetCheckCode.aspx?k='+Math.random()" src="/userctrl/GetCheckCode.aspx" alt="点击图片更换验证码" title="点击图片更换验证码" align="absmiddle" /></td>
            </tr>
            <tr>
            <td class="frmText" style="color: #000000; font-weight: normal;" align="right">临时认证码：</td>
            <td><input type="password" name="txtCerCode" id="txtCerCode"  runat="server" enableviewstate="false" maxlength="20" autocomplete="off"  /></td>
            </tr>

            <tr>
            <td></td>
            <td> <asp:Button ID="ButLogin" runat="server" Text="登录" CssClass="btnLogin" EnableViewState="False"  onMouseOver="this.style.cursor='hand';this.className='btnLoginX'" onMouseOut="this.className='btnLogin'" OnClick="ButLogin_Click"/></td>
            </tr>
            </table>
            <!--cursor:hand-->
            </form>
          <span>
          <br/>
              <pre>特别提示：此入口仅供研发测试用，正式上线后请禁用此入口……</pre>
              <h3>&copy; 2017 - 2018,西安兆友科技有限责任公司</h3>
          </span>
      </span>
     </div>
    
</body>
</html>
