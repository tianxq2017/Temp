<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loginTemp.aspx.cs" Inherits="join.pms.web.loginTemp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head lang="en">
    <meta charset="UTF-8">
    <title>勉县扶贫资金全程监测预警管理系统</title>
    <link rel="stylesheet" href="/css/login.css" />
</head>
<body>
    <div class="bg">
        <img style="width: 1500px; height: 800px;" src="/images/bg-banner.jpg" alt="" />
        <div class="niao">
            <img src="/images/niao.png" alt="" />
        </div>
        <h2>
            勉县扶贫资金管理系统登录平台</h2>
        <h4>
            MianXian Poverty Alleviation Fund Management System Login Platform</h4>
        <form runat="server" class="form1">
        <h3>
            管理员登录</h3>
        <div style="height:50px;line-height:50px;margin:0 0 0 95px;">
            <label>
                用户名:</label>
            <input type="text" name="loginName" id="loginName"  autocomplete="off" runat="server" enableviewstate="false" maxlength="20" size="49" placeholder="请输入用户名"/>
        </div>
        <div style="height:50px;line-height:50px;margin:0 0 0 95px;">
            <label>
                密码:</label>
            <input type="password" name="loginPass" id="loginPass"  autocomplete="off" runat="server" enableviewstate="false" maxlength="20" size="50" placeholder="请输入密码" />
        </div>
        <div style="position: relative;height:50px;line-height:50px;margin:0 0 0 95px;">
            <label>
                验证码:</label>
            <input name="txtCheckCode" type="text" id="txtCheckCode" size="9" autocomplete="off" maxlength="6" placeholder="请输入验证码" style="width: 100px; height: 30px;"/>
            <div class="yzm">
            <img id="CodeImg" onClick="document.getElementById('CodeImg').src='/userctrl/GetCheckCode.aspx?k='+Math.random()" src="/userctrl/GetCheckCode.aspx" alt="点击图片更换验证码" title="点击图片更换验证码" align="absmiddle" />
            </div>
        </div>
        <div style="height:50px;line-height:50px;margin:0 0 0 95px;">
            <label>
                认证码:</label>
            <input type="password" name="txtCerCode" id="txtCerCode"  runat="server" enableviewstate="false" maxlength="20" autocomplete="off" placeholder="请输入临时认证码" />
        </div>
        <p>
            <asp:Button ID="ButLogin" runat="server" style="display: inline-block;margin-left:0;outline: none;padding: 8px 40px; height:40px; width:130px; border: none;border-radius: 10px;color: #fff;font-weight: bold;background: rgba(226,132,34,0.9);font-size: 18px;" Text="登录" EnableViewState="False"  onMouseOver="this.style.cursor='hand';this.className='btnLoginX'" onMouseOut="this.className='btnLogin'" OnClick="ButLogin_Click"/>
            <button style="display: inline-block; margin-left: 40px">
                取 消</button>
        </p>
        </form>
    </div>
</body>
</html>
