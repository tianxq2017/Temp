<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="html02.aspx.cs" Inherits="AreWeb.PopInfoShareSys.Wap.html02" %>

<%@ Register Src="/userctrl/Uc_PageTop.ascx" TagName="Uc_PageTop" TagPrefix="uc1" %>
<%@ Register Src="/userctrl/Uc_Footer.ascx" TagName="Uc_Footer" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<meta name="apple-mobile-web-app-status-bar-style" content="black" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0, maximum-scale=1.0,user-scalable=no" /> 
<meta name="generator" content="bd-mobcard" />
<meta name="format-detection" content="telephone=yes" />
<title>静态页面</title>
<link href="/styles/css.css" rel="stylesheet" type="text/css" />
</head>

<body>
<uc1:Uc_PageTop id="Uc_PageTop1" runat="server"/>

<div class="block">
  <!--备注 -->
  <!--<div class="part_03">
	<p class="title">备注说明</p>
    <p class="sum">
	办理生育登记服务证时必须带：<br />
	1、夫妻双方户口簿、身份证、结婚证(原件)；<br />
	2、二寸夫妻合影照片2张(可网络上传)；<br />
	3、县外户籍的须在户籍所在地开出县(区)计生局、乡(镇、街道办事处)计生办、村(居)委会三级签章的婚育、收养情况证明。<br />
	4、再婚一方须带上离婚协议原件。
	</p>
  </div>
  <div class="clr10"></div> -->
  
  <!--申请步骤 -->
  <div class="flow_pic clearfix">
    <ul>
	  <li class="on"><p><b>1</b>填写申请资料</p></li>
	  <li><p><b>2</b>提交所需证件</p></li>
	  <li><p><b>3</b>申请成功</p></li>
	</ul>
  </div>
  
  <!--相关内容 -->
  <div class="part_05">
    <div class="part_name">填写申请资料</div>
	
	<div class="part_form">
	  <div class="part_title"><span class="fr">∨</span>女方信息</div>
	  <ul>		
		<li>
		  <p class="title">文本框</p>
		  <p class="text"><input type="text" name="textfield" /></p>
		</li>
		<li>
		  <p class="title">现居地</p> 
		  <p class="select">	
<a href="#" class="button">同上</a></p>
		</li>		
		<li>
		  <p class="title">选择框</p>
		  <p class="select"><select>
	<option value="初婚">初婚</option>
	<option value="再婚">再婚</option>
	<option value="复婚">复婚</option>

</select></p>
		</li>
	  </ul>
	</div>
	
	<div class="part_form">
	  <div class="part_title"><span class="fr">∨</span>男方信息</div>
	 <ul>		
		<li>
		  <p class="title">文本框</p>
		  <p class="text"><input type="text" name="textfield" /></p>
		</li>
		<li>
		  <p class="title">现居地</p> 
		  <p class="select">	
<a href="#" class="button">同上</a></p>
		</li>		
		<li>
		  <p class="title">选择框</p>
		  <p class="select"><select>
	<option value="初婚">初婚</option>
	<option value="再婚">再婚</option>
	<option value="复婚">复婚</option>

</select></p>
		</li>
	  </ul>
	</div>
	
	<div class="check"><input type="checkbox" name="cbOk" id="cbOk"> 我已经自检完成，确认资料准确无误！</div>
	
	<div class="part_button"><input type="submit" name="Submit" value="下一步，请继续！" /></div>
  </div>
</div>

<uc2:Uc_Footer id="Uc_Footer1" runat="server"/>
</body>
</html>
